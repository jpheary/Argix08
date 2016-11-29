using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Terminals;
using Argix.Windows;
using Argix.Windows.Printers;

namespace Argix.Terminals {
	//
	public class winDevices : System.Windows.Forms.Form {
		//Members
		private static DeviceItem ItemTemplate=null;
		private DeviceItem mItem=null;
		private BrotherPT2300 mPrinter=null;
		private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		
		private const string TERMINAL_ALL = "All";
        public event EventHandler UpdateServices=null;
		#region Controls

        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Button btnPinInputs;
		private System.Windows.Forms.Label _lblInputsDriver;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label _lblInputs;
		private System.Windows.Forms.Button btnEndCharge;
		private System.Windows.Forms.Button btnStartCharge;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdItems;
		private System.Windows.Forms.ComboBox cboTerminals;
        private System.Windows.Forms.TextBox txtDeviceScan;
        private ContextMenuStrip ctxDevices;
        private ToolStripMenuItem ctxCreate;
        private ToolStripMenuItem ctxUpdate;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem ctxAssign;
        private ToolStripMenuItem ctxUnassign;
        private ToolStripSeparator ctxSep2;
        private ToolStripMenuItem ctxRefresh;
        private BindingSource mItems;
        private BindingSource mTerminals;
		private System.Windows.Forms.Label _lblDeviceFind;

		#endregion
				
		//Interface
		public winDevices(BrotherPT2300 barcodePrinter) {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				this.Text = "Mobile Device Items";
				#region Window docking
				this.grdItems.Dock = DockStyle.Fill;
				this.grdItems.Controls.AddRange(new Control[]{this.cboTerminals, this._lblDeviceFind, this.txtDeviceScan});
				this._lblDeviceFind.Top = (this.txtDeviceScan.Top = this.grdItems.Top) + 3;
				this.txtDeviceScan.Left = this.grdItems.Width - this.txtDeviceScan.Width;
				this._lblDeviceFind.Left = this.txtDeviceScan.Left - this._lblDeviceFind.Width - 6;
				this.Controls.AddRange(new Control[]{this.grdItems});
				#endregion
				
				//Create services
				this.mPrinter = barcodePrinter;
				this.mGridSvc = new UltraGridSvc(this.grdItems, this.txtDeviceScan);
				this.mToolTip = new System.Windows.Forms.ToolTip();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new Devices window", ex); }
		}
        public bool CanNew { get { return false; } }
        public void New() { }
        public bool CanOpen { get { return false; } }
        public void Open() { }
        public bool CanExport { get { return this.grdItems.Rows.VisibleRowCount > 0; } }
        public void Export() {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.AddExtension = true;
            dlgSave.Filter = "Text Files (*.xml) | *.xml";
            dlgSave.FilterIndex = 0;
            dlgSave.Title = "Export Data As...";
            dlgSave.FileName = "Devices";
            dlgSave.OverwritePrompt = true;
            if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(DeviceItems));
                FileStream fs = new FileStream(dlgSave.FileName,FileMode.Create,FileAccess.Write);
                dcs.WriteObject(fs,this.mItems.DataSource);
                fs.Flush();
                fs.Close();
            }
        }
        public void PageSettings() { UltraGridPrinter.PageSettings(); }
        public void PrintPreview() { UltraGridPrinter.PrintPreview(this.grdItems,"Device Items"); }
        public bool CanPrint { get { return this.grdItems.Rows.VisibleRowCount > 0; } }
        public void Print(bool showDialog) { UltraGridPrinter.Print(this.grdItems,"Device Items",showDialog); }
        public bool CanPrintLabel { get { return this.mItem != null; } }
        public void PrintLabel() { this.mPrinter.Print("",this.mItem.ItemID,true); }
        public void Refresh2() {
            this.mGridSvc.CaptureState();
            try {
                setStatusMessage("Refreshing view of device items...");
                this.mItems.DataSource = MobileDevicesProxy.GetDeviceItems();
                this.grdItems.DisplayLayout.Bands[0].SortedColumns.RefreshSort(false);
                this.txtDeviceScan.Focus();
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
            finally {
                this.mGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdItems,null);
            }
        }
        public bool CanCreate { get { return !App.Config.ReadOnly && (this.cboTerminals.Text != TERMINAL_ALL); } }
        public void Create() {
            try {
                DeviceItem item = MobileDevicesProxy.GetDeviceItem("");
                item.TerminalID = Convert.ToInt64(this.cboTerminals.SelectedValue);
                item.Terminal = this.cboTerminals.Text;
                #region Update new device with templte attributes
                if(ItemTemplate != null) {
                    item.TypeID = ItemTemplate.TypeID;
                    item.Status = ItemTemplate.Status;
                    item.ModelNumber = ItemTemplate.ModelNumber;
                    item.FirmWareVersion = ItemTemplate.FirmWareVersion;
                    item.SoftWareVersion = ItemTemplate.SoftWareVersion;
                    item.ServiceExpiration = ItemTemplate.ServiceExpiration;
                    item.AccountID = ItemTemplate.AccountID;
                    item.PriorAccountID = ItemTemplate.PriorAccountID;
                }
                #endregion
                dlgDeviceItem dlgDevice = new dlgDeviceItem(item);
                if(dlgDevice.ShowDialog(this) == DialogResult.OK) {
                    //Create a new mobile device
                    setStatusMessage("Creating new device item " + item.DeviceID + "...");
                    if(MobileDevicesProxy.SaveDeviceItem(item)) {
                        setStatusMessage("Mobile device " + item.DeviceID + " for " + item.Terminal.Trim() + " was created.");
                        Refresh2();

                        ItemTemplate = this.mItem;
                        if((this.cboTerminals.Text != TERMINAL_ALL) && (item.TerminalID != Convert.ToInt64(this.cboTerminals.SelectedValue))) {
                            if(MessageBox.Show(this,"Mobile device was created in " + item.Terminal.Trim() + " terminal. Would you like to switch terminals?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes) {
                                this.cboTerminals.SelectedValue = item.TerminalID;
                                OnTerminalChanged(null,EventArgs.Empty);
                            }
                        }
                    }
                    else
                        MessageBox.Show(this,"Mobile device " + item.DeviceID + " could not be created. Please try again.",this.Text,MessageBoxButtons.OK);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
        }
        public bool CanEdit { 
            get {
                if(!App.Config.ReadOnly && this.grdItems.Selected.Rows.Count > 0 && this.mItem != null) {
                    bool inactive = (this.mItem.IsActive == 0);
                    bool issued = (this.mItem.Status == MobileDevicesProxy.ISSUED);
                    bool available = (!inactive && !issued);
                    return (available || inactive);
                }
                else
                    return false;
            } 
        }
        public void Edit() {
            try {
                DeviceItem item = this.mItem;
                dlgDeviceItem dlgDevice = new dlgDeviceItem(item);
                if(dlgDevice.ShowDialog(this) == DialogResult.OK) {
                    //Update exisitng device
                    setStatusMessage("Updating device item " + item.DeviceID + "...");
                    if(MobileDevicesProxy.SaveDeviceItem(item)) {
                        setStatusMessage("Mobile device " + item.DeviceID + " was updated.");
                        Refresh2();
                        
                        if((this.cboTerminals.Text != TERMINAL_ALL) && (item.TerminalID != Convert.ToInt64(this.cboTerminals.SelectedValue))) {
                            if(MessageBox.Show(this,"Mobile device was moved to " + item.Terminal.Trim() + " terminal. Would you like to switch terminals?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes) {
                                this.cboTerminals.SelectedValue = item.TerminalID;
                                OnTerminalChanged(null,null);
                            }
                        }
                    }
                    else
                        MessageBox.Show(this,"Mobile device " + item.DeviceID + " could not be updated. Please try again.",this.Text,MessageBoxButtons.OK);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
        }
        public bool CanAssign { 
            get {
                if(!App.Config.ReadOnly && this.grdItems.Selected.Rows.Count > 0 && this.mItem != null) {
                    bool inactive = (this.mItem.IsActive == 0);
                    bool issued = (this.mItem.Status == MobileDevicesProxy.ISSUED);
                    bool available = (!inactive && !issued);
                    return available;
                }
                else
                    return false;
            } 
        }
        public void Assign() {
            try {
                DeviceItem item = this.mItem;
                dlgDeviceAssignment dlgAssignment = new dlgDeviceAssignment(item);
                if(dlgAssignment.ShowDialog(this) == DialogResult.OK) {
                    //Assign mobile device to driver
                    setStatusMessage("Assigning device item " + item.DeviceID + " to " + dlgAssignment.DriverName + "...");
                    if(MobileDevicesProxy.AssignDeviceItem(item,dlgAssignment.DriverID,dlgAssignment.InstallationType,dlgAssignment.InstallationNumber)) {
                        setStatusMessage("Mobile device " + item.DeviceID + " is now assigned to " + dlgAssignment.DriverName + ".");
                        Refresh2();
                    }
                    else
                        MessageBox.Show(this,"Mobile device for driver " + item.DeviceID + " could not be assigned. Please try again.",this.Text,MessageBoxButtons.OK);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
        }
        public bool CanUnassign { 
            get {
                if(!App.Config.ReadOnly && this.grdItems.Selected.Rows.Count > 0 && this.mItem != null) {
                    bool issued = (this.mItem.Status == MobileDevicesProxy.ISSUED);
                    return issued;
                }
                else
                    return false;
            } 
        }
        public void Unassign() {
            try {
                DeviceItem item = this.mItem;
                if(MessageBox.Show(this,"Unassign device " + item.DeviceID + " from current driver?",this.Name,MessageBoxButtons.OKCancel) == DialogResult.OK) {
                    //Read existing details for row version and request cancel
                    setStatusMessage("Unassigning device item " + item.DeviceID + "...");
                    if(MobileDevicesProxy.UnassignDeviceItem(item)) {
                        setStatusMessage("Device " + item.DeviceID + " was unassigned.");
                        Refresh2();
                    }
                    else
                        MessageBox.Show(this,"Device " + item.DeviceID + " could not be unassigned. Please try again.",this.Name,MessageBoxButtons.OK);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
        }
        
        protected override void Dispose( bool disposing )  { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeviceItem",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AccountID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeviceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirmWareVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InstallationNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InstallationType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ModelNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PriorAccountID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PriorDeviceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PriorItemID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceExpiration");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SoftWareVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comments");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Created");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreatedUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Driver");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StatusList");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Terminal");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winDevices));
            this.grdItems = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxDevices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mItems = new System.Windows.Forms.BindingSource(this.components);
            this.btnEndCharge = new System.Windows.Forms.Button();
            this.btnStartCharge = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this._lblInputsDriver = new System.Windows.Forms.Label();
            this.btnPinInputs = new System.Windows.Forms.Button();
            this._lblInputs = new System.Windows.Forms.Label();
            this.cboTerminals = new System.Windows.Forms.ComboBox();
            this.mTerminals = new System.Windows.Forms.BindingSource(this.components);
            this.txtDeviceScan = new System.Windows.Forms.TextBox();
            this._lblDeviceFind = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.ctxDevices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).BeginInit();
            this.SuspendLayout();
            // 
            // grdItems
            // 
            this.grdItems.ContextMenuStrip = this.ctxDevices;
            this.grdItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdItems.DataSource = this.mItems;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdItems.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 12;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 8;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Caption = "Device ID";
            ultraGridColumn3.Header.VisiblePosition = 1;
            ultraGridColumn3.Width = 120;
            ultraGridColumn4.Header.Caption = "FirmWare";
            ultraGridColumn4.Header.VisiblePosition = 10;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn4.Width = 96;
            ultraGridColumn5.Header.Caption = "Install#";
            ultraGridColumn5.Header.VisiblePosition = 14;
            ultraGridColumn5.Width = 96;
            ultraGridColumn6.Header.Caption = "Install Type";
            ultraGridColumn6.Header.VisiblePosition = 15;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn6.Width = 96;
            ultraGridColumn7.Header.Caption = "Model#";
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Width = 96;
            ultraGridColumn8.Header.VisiblePosition = 13;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.Caption = "Prior Device ID";
            ultraGridColumn9.Header.VisiblePosition = 19;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn9.Width = 96;
            ultraGridColumn10.Header.VisiblePosition = 18;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Format = "MM/dd/yyyy";
            ultraGridColumn11.Header.Caption = "Service Exp";
            ultraGridColumn11.Header.VisiblePosition = 9;
            ultraGridColumn11.Width = 96;
            ultraGridColumn12.Header.Caption = "SoftWare";
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn12.Width = 96;
            ultraGridColumn13.Header.VisiblePosition = 17;
            ultraGridColumn13.Width = 144;
            ultraGridColumn14.Header.VisiblePosition = 22;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Header.VisiblePosition = 23;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 16;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.Caption = "Driver";
            ultraGridColumn17.Header.VisiblePosition = 5;
            ultraGridColumn17.Width = 143;
            ultraGridColumn18.Header.VisiblePosition = 21;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.Caption = "Item ID";
            ultraGridColumn19.Header.VisiblePosition = 0;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn19.Width = 96;
            ultraGridColumn20.Header.VisiblePosition = 25;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn21.Header.VisiblePosition = 26;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.Header.VisiblePosition = 2;
            ultraGridColumn22.Width = 72;
            ultraGridColumn23.Header.VisiblePosition = 20;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 4;
            ultraGridColumn24.Width = 192;
            ultraGridColumn25.Header.VisiblePosition = 3;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Header.Caption = "Type";
            ultraGridColumn26.Header.VisiblePosition = 6;
            ultraGridColumn26.Width = 96;
            ultraGridColumn27.Header.VisiblePosition = 24;
            ultraGridColumn27.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27});
            this.grdItems.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdItems.DisplayLayout.CaptionAppearance = appearance2;
            this.grdItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdItems.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdItems.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdItems.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdItems.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdItems.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdItems.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdItems.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdItems.Location = new System.Drawing.Point(0,0);
            this.grdItems.Name = "grdItems";
            this.grdItems.Size = new System.Drawing.Size(472,233);
            this.grdItems.TabIndex = 10;
            this.grdItems.Text = "Devices";
            this.grdItems.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdItems.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
            this.grdItems.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // ctxDevices
            // 
            this.ctxDevices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCreate,
            this.ctxUpdate,
            this.ctxSep1,
            this.ctxAssign,
            this.ctxUnassign,
            this.ctxSep2,
            this.ctxRefresh});
            this.ctxDevices.Name = "ctxDevices";
            this.ctxDevices.Size = new System.Drawing.Size(186,126);
            // 
            // ctxCreate
            // 
            this.ctxCreate.Name = "ctxCreate";
            this.ctxCreate.Size = new System.Drawing.Size(185,22);
            this.ctxCreate.Text = "&Create";
            this.ctxCreate.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxUpdate
            // 
            this.ctxUpdate.Name = "ctxUpdate";
            this.ctxUpdate.Size = new System.Drawing.Size(185,22);
            this.ctxUpdate.Text = "&Update";
            this.ctxUpdate.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(182,6);
            // 
            // ctxAssign
            // 
            this.ctxAssign.Name = "ctxAssign";
            this.ctxAssign.Size = new System.Drawing.Size(185,22);
            this.ctxAssign.Text = "&Assign to Driver";
            this.ctxAssign.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxUnassign
            // 
            this.ctxUnassign.Name = "ctxUnassign";
            this.ctxUnassign.Size = new System.Drawing.Size(185,22);
            this.ctxUnassign.Text = "U&nassign from Driver";
            this.ctxUnassign.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(182,6);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(185,22);
            this.ctxRefresh.Text = "&Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mItems
            // 
            this.mItems.DataSource = typeof(Argix.Terminals.DeviceItems);
            // 
            // btnEndCharge
            // 
            this.btnEndCharge.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnEndCharge.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnEndCharge.ForeColor = System.Drawing.Color.Navy;
            this.btnEndCharge.Location = new System.Drawing.Point(8,224);
            this.btnEndCharge.Name = "btnEndCharge";
            this.btnEndCharge.Size = new System.Drawing.Size(128,23);
            this.btnEndCharge.TabIndex = 157;
            this.btnEndCharge.Text = "&End Charge Cycle";
            this.btnEndCharge.UseVisualStyleBackColor = false;
            this.btnEndCharge.Visible = false;
            // 
            // btnStartCharge
            // 
            this.btnStartCharge.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnStartCharge.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStartCharge.ForeColor = System.Drawing.Color.Navy;
            this.btnStartCharge.Location = new System.Drawing.Point(8,192);
            this.btnStartCharge.Name = "btnStartCharge";
            this.btnStartCharge.Size = new System.Drawing.Size(128,23);
            this.btnStartCharge.TabIndex = 156;
            this.btnStartCharge.Text = "S&tart Charge Cycle";
            this.btnStartCharge.UseVisualStyleBackColor = false;
            this.btnStartCharge.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Navy;
            this.btnCancel.Location = new System.Drawing.Point(72,256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64,23);
            this.btnCancel.TabIndex = 153;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.ForeColor = System.Drawing.Color.Navy;
            this.btnOK.Location = new System.Drawing.Point(8,256);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64,23);
            this.btnOK.TabIndex = 152;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // _lblInputsDriver
            // 
            this._lblInputsDriver.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblInputsDriver.ForeColor = System.Drawing.Color.Navy;
            this._lblInputsDriver.Location = new System.Drawing.Point(8,32);
            this._lblInputsDriver.Name = "_lblInputsDriver";
            this._lblInputsDriver.Size = new System.Drawing.Size(54,18);
            this._lblInputsDriver.TabIndex = 109;
            this._lblInputsDriver.Text = "Driver";
            this._lblInputsDriver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPinInputs
            // 
            this.btnPinInputs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPinInputs.BackColor = System.Drawing.SystemColors.Control;
            this.btnPinInputs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPinInputs.Font = new System.Drawing.Font("Arial",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnPinInputs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPinInputs.Location = new System.Drawing.Point(138,0);
            this.btnPinInputs.Name = "btnPinInputs";
            this.btnPinInputs.Size = new System.Drawing.Size(18,18);
            this.btnPinInputs.TabIndex = 18;
            this.btnPinInputs.UseVisualStyleBackColor = false;
            // 
            // _lblInputs
            // 
            this._lblInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblInputs.BackColor = System.Drawing.SystemColors.Control;
            this._lblInputs.ForeColor = System.Drawing.Color.Navy;
            this._lblInputs.Location = new System.Drawing.Point(0,0);
            this._lblInputs.Name = "_lblInputs";
            this._lblInputs.Size = new System.Drawing.Size(156,18);
            this._lblInputs.TabIndex = 17;
            this._lblInputs.Text = "Inputs";
            this._lblInputs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTerminals
            // 
            this.cboTerminals.DataSource = this.mTerminals;
            this.cboTerminals.DisplayMember = "TerminalName";
            this.cboTerminals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminals.Location = new System.Drawing.Point(92,0);
            this.cboTerminals.MaxDropDownItems = 4;
            this.cboTerminals.Name = "cboTerminals";
            this.cboTerminals.Size = new System.Drawing.Size(192,21);
            this.cboTerminals.TabIndex = 87;
            this.cboTerminals.ValueMember = "TerminalID";
            this.cboTerminals.SelectionChangeCommitted += new System.EventHandler(this.OnTerminalChanged);
            // 
            // mTerminals
            // 
            this.mTerminals.DataSource = typeof(Argix.Terminals.LocalTerminals);
            // 
            // txtDeviceScan
            // 
            this.txtDeviceScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDeviceScan.Location = new System.Drawing.Point(324,0);
            this.txtDeviceScan.MaxLength = 20;
            this.txtDeviceScan.Name = "txtDeviceScan";
            this.txtDeviceScan.Size = new System.Drawing.Size(144,21);
            this.txtDeviceScan.TabIndex = 88;
            this.txtDeviceScan.TextChanged += new System.EventHandler(this.OnDeviceScanned);
            // 
            // _lblDeviceFind
            // 
            this._lblDeviceFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblDeviceFind.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._lblDeviceFind.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this._lblDeviceFind.Image = ((System.Drawing.Image)(resources.GetObject("_lblDeviceFind.Image")));
            this._lblDeviceFind.Location = new System.Drawing.Point(303,3);
            this._lblDeviceFind.Name = "_lblDeviceFind";
            this._lblDeviceFind.Size = new System.Drawing.Size(16,16);
            this._lblDeviceFind.TabIndex = 89;
            // 
            // winDevices
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(472,233);
            this.Controls.Add(this.cboTerminals);
            this.Controls.Add(this._lblDeviceFind);
            this.Controls.Add(this.txtDeviceScan);
            this.Controls.Add(this.grdItems);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winDevices";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Devices";
            this.Deactivate += new System.EventHandler(this.OnFormDeactivated);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.ctxDevices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				this.Visible = true;
				Application.DoEvents();
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				this.mToolTip.SetToolTip(this.txtDeviceScan, "Scan a device");
				#endregion
				#region Grid Initialization
				this.grdItems.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdItems.DisplayLayout.Bands[0].Columns["DeviceID"].SortIndicator = SortIndicator.Ascending;
				#endregion
				
				//Get selection lists and set control defaults
				this.mTerminals.DataSource = MobileDevicesProxy.GetLocalTerminals();
                this.mTerminals.Sort = "TerminalName ASC";
                LocalTerminal t = new LocalTerminal();
                t.TerminalID = 0;
                t.TerminalName = TERMINAL_ALL;
                this.mTerminals.Insert(0,t);
                if(this.cboTerminals.Items.Count > 0) this.cboTerminals.SelectedIndex = 0;
				this.cboTerminals.Enabled = (this.cboTerminals.Items.Count>0);
				OnTerminalChanged(null, null);
                Refresh2();
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { setServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormActivated(object sender,System.EventArgs e) { if(UpdateServices != null) UpdateServices(this,EventArgs.Empty); }
        private void OnFormDeactivated(object sender,System.EventArgs e) { if(UpdateServices != null) UpdateServices(this,EventArgs.Empty); }
		private void OnFormResize(object sender, System.EventArgs e) { }
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) { }
		private void OnTerminalChanged(object sender, System.EventArgs e) {
			//Terminal changed event handler
			try {
				//Set valid filter configuration; clear filters and reset
                this.mGridSvc.CaptureState();
				this.grdItems.DisplayLayout.Bands[0].ColumnFilters["Terminal"].FilterConditions.Clear();
				if(this.cboTerminals.Text != TERMINAL_ALL) {
					this.grdItems.DisplayLayout.Bands[0].ColumnFilters["Terminal"].FilterConditions.Add(FilterComparisionOperator.Equals, this.cboTerminals.Text);
				}
				this.grdItems.DisplayLayout.RefreshFilters();
                this.mGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdItems,null);
			}
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
			finally { setServices(); }
		}
		#region Grid Support: GridSelectionChanged(), GridMouseDown(), GridDoubleClick()
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			try {
				//Select grid and forward to update
				this.mItem = null;
				if(this.grdItems.Rows.VisibleRowCount > 0 && this.grdItems.Selected.Rows.Count > 0) {
					string itemID = this.grdItems.Selected.Rows[0].Cells["ItemID"].Value.ToString();
					this.mItem = MobileDevicesProxy.GetDeviceItem(itemID);
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
			finally { setServices(); }
		}
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			try {
				//Set menu and toolbar services
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(oUIElement != null) {
					object oContext = oUIElement.GetContext(typeof(UltraGridRow));
					if(oContext != null) {
						if(e.Button == MouseButtons.Left) {
							//OnDragDropMouseDown(sender, e);
						}
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow oRow = (UltraGridRow)oContext;
							if(!oRow.Selected) grid.Selected.Rows.Clear();
							oRow.Selected = true;
						}
					}
					else {
						//Deselect rows in the white space of the grid or deactivate the active   
						//row when in a scroll region to prevent double-click action
						if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
							grid.Selected.Rows.Clear();
						else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
							if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
					}
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
			finally { setServices(); }
		}
		private void OnGridDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for double-click event
			try {
				//Select grid and forward to update
				UltraGrid grid = (UltraGrid)sender;
				if(grid.ActiveRow != null && grid.Selected.Rows.Count > 0) {
					if(CanEdit) Edit();
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		#endregion
		#region Scan Support: DeviceScanned()
		private void OnDeviceScanned(object sender, System.EventArgs e) {
			//Event handler for device barcode scanned
//			UltraGridRow oRow=null;
//			try {
//				if(this.txtDeviceScan.Text.Length == DeviceItem.ScanLength) {
//					//Eliminate leading 0 from Barcode 128C
//					this.txtDeviceScan.Text = this.txtDeviceScan.Text.Substring(1, App.Config.DeviceScanLength-1);
//					
//					//Find device
//					for(int i=0; i<this.grdItems.Rows.Count; i++) {
//						if(this.grdItems.Rows[i].Cells["ItemID"].Value.ToString() == this.txtDeviceScan.Text) {
//							oRow = this.grdItems.Rows[i];
//							break;
//						}
//					}
//					if(oRow != null) {
//						//Existing device
//						bool bGo = (oRow.VisibleIndex >= 0);
//						if(oRow.VisibleIndex < 0) {
//							//Query to change terminal
//							long lTerminal = Convert.ToInt64(oRow.Cells["TerminalID"].Value);
//							string sTerminal = oRow.Cells["Terminal"].Value.ToString().Trim();
//							if(MessageBox.Show(this, "Device is located in " + sTerminal + " terminal. Would you like to switch terminals?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)==DialogResult.Yes) {
//								this.cboTerminals.SelectedValue = lTerminal;
//								OnTerminalChanged(null, null);
//								bGo = true;
//							}
//						}
//						if(bGo) {
//							//Select device; launch action
//							oRow.Selected = true;
//							oRow.Activate();
//							string sState = oRow.Cells["Status"].Value.ToString();
//							switch(sState) {
//								case DeviceItem.AVAILABLE:	this.mnuDeviceAssign.PerformClick(); break;
//								case DeviceItem.ISSUED:		this.mnuDeviceUnassign.PerformClick(); break;
//								case DeviceItem.INACTIVE:	this.mnuItemUpdate.PerformClick(); break;
//							}
//						}
//					}
//					else {
//						//New device
//						if(this.mnuItemCreate.Enabled) this.mnuItemCreate.PerformClick();
//					}
//					this.txtDeviceScan.Text = "";
//				}
//			}
//			catch(Exception ex) { reportError(ex, false, LogLevel.Warning); }
        }
		#endregion
		private void OnMenuItemClicked(object sender, System.EventArgs e)  {
			//Menu itemclicked-apply selected service
			try  {
                ToolStripItem menu = (ToolStripItem)sender;
				switch(menu.Name) {
                    case "ctxCreate": Create(); break;
                    case "ctxUpdate": Edit(); break;
                    case "ctxAssign": Assign(); break;
                    case "ctxUnassign": Unassign(); break;
                    case "ctxRefresh": Refresh2(); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { setServices(); this.Cursor = Cursors.Default; }
		}
        private void setServices() {
			//Set user services depending upon the selected device item
			try {
				//Determine state for the selected device (if applicable)
				this.cboTerminals.Enabled = ((this.cboTerminals.Items.Count > 0));
				this.ctxCreate.Enabled = CanCreate;
				this.ctxUpdate.Enabled = CanEdit;
				this.ctxAssign.Enabled = CanAssign;
				this.ctxUnassign.Enabled = CanUnassign;
				this.ctxRefresh.Enabled = true;
                if(UpdateServices != null) UpdateServices(this,EventArgs.Empty);
            }
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
        private void setStatusMessage(string message) { ((winMain)this.Parent.Parent).SetStatusMessage(message); }
    }
}
