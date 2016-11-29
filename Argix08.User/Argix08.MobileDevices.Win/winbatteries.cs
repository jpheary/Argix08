using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
	//Application main window
	public class winBatteries : System.Windows.Forms.Form {
		//Members		
		private BatteryItem mItem=null;
		private LocalDriver mDriver=null;
		private BrotherPT2300 mPrinter=null;
		private UltraGridSvc mGridSvc=null;
		private UltraGridSvc mAssignmentGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		
		private const string TERMINAL_ALL = "All";		
		private const int DISCHARGED = 0;
		private const int LOWCHARGE = 1;
		private const int CHARGING = 2;
		private const int CHARGED = 3;
		private const int CHARGECOMPLETE = 4;
		private const int INACTIVE = 5;

        public event EventHandler UpdateServices=null;
        #region Controls

        private System.Windows.Forms.Button btnPinInputs;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label _lblInputsDriver;
		private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label _lblInputs;
		private System.Windows.Forms.Button btnEndCharge;
		private System.Windows.Forms.Button btnStartCharge;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdItems;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDriverAssignments;
		private System.Windows.Forms.Panel pnlBatterys;
		private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.ComboBox cboTerminals;
		private System.Windows.Forms.TextBox txtBatteryScan;
		private System.Windows.Forms.CheckBox chkShowInActive;
        private ContextMenuStrip ctxBatteries;
        private ToolStripMenuItem ctxCreate;
        private ToolStripMenuItem ctxUpdate;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem ctxStartCharge;
        private ToolStripMenuItem ctxEndCharge;
        private ToolStripSeparator ctxSep2;
        private ToolStripMenuItem ctxAssignments;
        private ToolStripSeparator ctxSep3;
        private ToolStripMenuItem ctxRefresh;
        private BindingSource mTerminals;
        private BindingSource mItems;
        private BindingSource mAssignments;
        private System.Windows.Forms.Label _lblBatteryFind;

		#endregion
				
		//Interface
		public winBatteries(BrotherPT2300 barcodePrinter) {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				this.Text = "Mobile Battery Items";
				#region Window docking
				this.splitterH.MinExtra = 48;
				this.splitterH.MinSize = 72;
				this.pnlBatterys.Dock = DockStyle.Fill;
					this.grdItems.Dock = DockStyle.Fill;
					this.splitterH.Dock = DockStyle.Bottom;
					this.grdDriverAssignments.Dock = DockStyle.Bottom;
				this.grdItems.Controls.AddRange(new Control[]{this.cboTerminals});
				this.pnlBatterys.Controls.AddRange(new Control[]{this.grdItems, this.splitterH, this.grdDriverAssignments});
				this.grdItems.Controls.AddRange(new Control[]{this._lblBatteryFind, this.txtBatteryScan});
				this._lblBatteryFind.Top = (this.txtBatteryScan.Top = this.grdItems.Top) + 3;
				this.txtBatteryScan.Left = this.grdItems.Width - this.txtBatteryScan.Width;
				this._lblBatteryFind.Left = this.txtBatteryScan.Left - this._lblBatteryFind.Width - 6;
				this.grdDriverAssignments.Controls.AddRange(new Control[]{this.chkShowInActive});
				this.chkShowInActive.Top = 2;	//this.grdDriverAssignments.Top;
				this.chkShowInActive.Left = this.grdDriverAssignments.Width - this.chkShowInActive.Width - 2;
				this.Controls.AddRange(new Control[]{this.pnlBatterys});
				#endregion
				
				//Create services
				this.mPrinter = barcodePrinter;
				this.mGridSvc = new UltraGridSvc(this.grdItems);
				this.mAssignmentGridSvc = new UltraGridSvc(this.grdDriverAssignments);
				this.mToolTip = new System.Windows.Forms.ToolTip();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new Batteries window", ex); }
		}
        public bool CanNew { get { return false; } }
        public void New() { }
        public bool CanOpen { get { return false; } }
        public void Open() { }
        public bool CanExport { get { return ((this.grdItems.Focused && this.grdItems.Rows.VisibleRowCount > 0) || (this.grdDriverAssignments.Focused && this.grdDriverAssignments.Rows.VisibleRowCount > 0)); } }
        public void Export() {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.AddExtension = true;
            dlgSave.Filter = "Text Files (*.xml) | *.xml";
            dlgSave.FilterIndex = 0;
            dlgSave.Title = "Export Data As...";
            dlgSave.FileName = (this.grdItems.Focused ? "Batterys" : "BatteryAssignments");
            dlgSave.OverwritePrompt = true;
            if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                FileStream fs = new FileStream(dlgSave.FileName,FileMode.Create,FileAccess.Write);
                System.Runtime.Serialization.DataContractSerializer dcs=null;
                if(this.grdItems.Focused) {
                    dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(BatteryItems));
                    dcs.WriteObject(fs,this.mItems.DataSource);
                }
                else if(this.grdDriverAssignments.Focused) {
                    dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(LocalDrivers));
                    dcs.WriteObject(fs,this.mAssignments.DataSource);
                }
                fs.Flush();
                fs.Close();
            }
        }
        public void PageSettings() { UltraGridPrinter.PageSettings(); }
        public void PrintPreview() { 
            if(this.grdItems.Focused)
                UltraGridPrinter.PrintPreview(this.grdItems,"Battery Items");
            else if(this.grdDriverAssignments.Focused)
                UltraGridPrinter.PrintPreview(this.grdDriverAssignments,"Driver Assignments");
        }
        public bool CanPrint { get { return ((this.grdItems.Focused && this.grdItems.Rows.VisibleRowCount > 0) || (this.grdDriverAssignments.Focused && this.grdDriverAssignments.Rows.VisibleRowCount > 0)); } }
        public void Print(bool showDialog) { 
            if(this.grdItems.Focused)
                UltraGridPrinter.Print(this.grdItems,"Battery Items",showDialog);
            else if(this.grdDriverAssignments.Focused)
                UltraGridPrinter.Print(this.grdDriverAssignments,"Driver Assignments",showDialog);
        }
        public bool CanPrintLabel { get { return this.mItem != null; } }
        public void PrintLabel() { this.mPrinter.Print("",this.mItem.ItemID,true); }
        public void Refresh2() {
            this.mGridSvc.CaptureState();
            try {
                setStatusMessage("Refreshing view of battery items...");
                this.mItems.DataSource = MobileDevicesProxy.GetBatteryItems();
                this.grdItems.DisplayLayout.Bands[0].SortedColumns.RefreshSort(false);
                this.txtBatteryScan.Focus();
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
            finally {
                this.mGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdItems,null);
            }
            this.mAssignmentGridSvc.CaptureState();
            try {
                setStatusMessage("Loading driver assignments...");
                this.mAssignments.DataSource = MobileDevicesProxy.GetBatteryItemAssignments();
                this.grdDriverAssignments.DisplayLayout.Bands[0].SortedColumns.RefreshSort(false);
                this.txtBatteryScan.Focus();
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
            finally {
                this.mAssignmentGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdDriverAssignments,null);
            }
        }
        public bool CanCreate { get { return (!App.Config.ReadOnly && this.grdItems.Focused && this.cboTerminals.Text != "All"); } }
        public void Create() {
            BatteryItem item = MobileDevicesProxy.GetBatteryItem("");
            item.TerminalID = Convert.ToInt64(this.cboTerminals.SelectedValue);
            item.Terminal = this.cboTerminals.Text;
            dlgBatteryItem dlgBattery = new dlgBatteryItem(item);
            DialogResult res = dlgBattery.ShowDialog(this);
            if(res==DialogResult.OK) {
                //Create a new battery item
                int index = (this.grdItems.Selected.Rows.Count > 0) ? this.grdItems.Selected.Rows[0].VisibleIndex : -1;
                try {
                    setStatusMessage("Creating new battery item " + item.ItemID + "...");
                    if(MobileDevicesProxy.SaveBatteryItem(item)) {
                        setStatusMessage("Battery " + item.ItemID + " for " + item.Terminal.Trim() + " was created.");
                        MobileDevicesProxy.ExportBatteryItem(item);
                        if((this.cboTerminals.Text != TERMINAL_ALL) && (item.TerminalID != Convert.ToInt64(this.cboTerminals.SelectedValue))) {
                            if(MessageBox.Show(this,"Battery was created in " + item.Terminal.Trim() + " terminal. Would you like to switch terminals?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes) {
                                this.cboTerminals.SelectedValue = item.TerminalID;
                                OnTerminalChanged(null,null);
                            }
                        }
                    }
                    else
                        MessageBox.Show(this,"Battery " + item.ItemID + " could not be created. Please try again.",this.Text,MessageBoxButtons.OK);
                }
                catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
                finally {
                    if(this.grdItems.Rows.VisibleRowCount > 0) {
                        if(index >= 0 && index < this.grdItems.Rows.VisibleRowCount)
                            this.grdItems.Rows.GetRowAtVisibleIndex(index).Selected = true;
                        else
                            this.grdItems.Rows.GetRowAtVisibleIndex(0).Selected = true;
                        this.grdItems.Selected.Rows[0].Activate();
                    }
                    OnGridSelectionChanged(this.grdItems,null);
                }
            }
        }
        public bool CanEdit { 
            get {
                if(!App.Config.ReadOnly && this.grdItems.Focused && this.grdItems.Selected.Rows.Count > 0 && this.mItem != null) {
                    double chargetime = convertTimeToHours(this.mItem.ElapsedTimeCharging);
                    double chargeremain = convertTimeToHours(this.mItem.RemainingTimeCharging);
                    int oncharge = this.mItem.CycleComplete;
                    double runtime = convertTimeToHours(this.mItem.TimeElapsedSinceComplete);
                    bool inactive = (this.mItem.IsActive == 0);
                    bool issued = (this.mItem.Status == MobileDevicesProxy.ISSUED);
                    bool charging = ((!issued && oncharge==0 && chargetime>=0 && chargeremain>0) || (issued && oncharge==0 && chargetime>=0 && chargeremain>0));
                    bool charged = ((!issued && (oncharge==0 && chargetime > 0 && chargeremain<=0)) || (issued && (oncharge==0 && chargetime > 0 && chargeremain<=0)));
                    return ((inactive || !issued) && (!charging && !charged));
                }
                else 
                    return false; 
            } 
        }
        public void Edit() {
            BatteryItem item = this.mItem;
            dlgBatteryItem dlgBattery = new dlgBatteryItem(item);
            DialogResult res = dlgBattery.ShowDialog(this);
            if(res==DialogResult.OK) {
                //Update exisiting battery
                int index = (this.grdItems.Selected.Rows.Count > 0) ? this.grdItems.Selected.Rows[0].VisibleIndex : -1;
                try {
                    setStatusMessage("Updating battery item " + item.ItemID + "...");
                    if(MobileDevicesProxy.SaveBatteryItem(item)) {
                        setStatusMessage("Battery " + item.ItemID + " was updated.");
                        if((this.cboTerminals.Text != TERMINAL_ALL) && (item.TerminalID != Convert.ToInt64(this.cboTerminals.SelectedValue))) {
                            if(MessageBox.Show(this,"Battery was moved to " + item.Terminal.Trim() + " terminal. Would you like to switch terminals?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1)==DialogResult.Yes) {
                                this.cboTerminals.SelectedValue = item.TerminalID;
                                OnTerminalChanged(null,null);
                            }
                        }
                    }
                    else
                        MessageBox.Show(this,"Battery " + item.ItemID + " could not be updated. Please try again.",this.Text,MessageBoxButtons.OK);
                }
                catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
                finally {
                    if(this.grdItems.Rows.VisibleRowCount > 0) {
                        if(index >= 0 && index < this.grdItems.Rows.VisibleRowCount)
                            this.grdItems.Rows.GetRowAtVisibleIndex(index).Selected = true;
                        else
                            this.grdItems.Rows.GetRowAtVisibleIndex(0).Selected = true;
                        this.grdItems.Selected.Rows[0].Activate();
                    }
                    OnGridSelectionChanged(this.grdItems,null);
                }
            }
        }
        public bool CanStartCharge { 
            get {
                if(!App.Config.ReadOnly && this.grdItems.Focused && this.grdItems.Selected.Rows.Count > 0 && this.mItem != null) {
                    double chargetime = convertTimeToHours(this.mItem.ElapsedTimeCharging);
                    double chargeremain = convertTimeToHours(this.mItem.RemainingTimeCharging);
                    int oncharge = this.mItem.CycleComplete;
                    double runtime = convertTimeToHours(this.mItem.TimeElapsedSinceComplete);
                    bool inactive = (this.mItem.IsActive == 0);
                    bool issued = (this.mItem.Status == MobileDevicesProxy.ISSUED);
                    bool charging = ((!issued && oncharge==0 && chargetime>=0 && chargeremain>0) || (issued && oncharge==0 && chargetime>=0 && chargeremain>0));
                    bool charged = ((!issued && (oncharge==0 && chargetime > 0 && chargeremain<=0)) || (issued && (oncharge==0 && chargetime > 0 && chargeremain<=0)));
                    return (!inactive && (!charging && !charged));
                }
                else
                    return false;
            } 
        }
        public void StartCharge() {
            BatteryItem item = this.mItem;
            DialogResult res = MessageBox.Show(this,"Start battery " + item.ItemID + " charge cycle?",this.Name,MessageBoxButtons.OKCancel);
            if(res==DialogResult.OK) {
                //Read existing details for row version and request cancel
                int index = (this.grdItems.Selected.Rows.Count > 0) ? this.grdItems.Selected.Rows[0].VisibleIndex : -1;
                try {
                    setStatusMessage("Starting charge cycle for " + item.ItemID + "...");
                    if(MobileDevicesProxy.StartBatteryItemChargeCycle(item))
                        setStatusMessage("Charge cycle for " + item.ItemID + " was started.");
                    else
                        MessageBox.Show(this,"Charge cycle for " + item.ItemID + " could not be started. Please try again.",this.Text,MessageBoxButtons.OK);
                }
                catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
                finally {
                    if(this.grdItems.Rows.VisibleRowCount > 0) {
                        if(index >= 0 && index < this.grdItems.Rows.VisibleRowCount)
                            this.grdItems.Rows.GetRowAtVisibleIndex(index).Selected = true;
                        else
                            this.grdItems.Rows.GetRowAtVisibleIndex(0).Selected = true;
                        this.grdItems.Selected.Rows[0].Activate();
                    }
                    OnGridSelectionChanged(this.grdItems,null);
                }
            }
        }
        public bool CanEndCharge { 
            get {
                if(!App.Config.ReadOnly && this.grdItems.Focused && this.grdItems.Selected.Rows.Count > 0 && this.mItem != null) {
                    double chargetime = convertTimeToHours(this.mItem.ElapsedTimeCharging);
                    double chargeremain = convertTimeToHours(this.mItem.RemainingTimeCharging);
                    int oncharge = this.mItem.CycleComplete;
                    double runtime = convertTimeToHours(this.mItem.TimeElapsedSinceComplete);
                    bool inactive = (this.mItem.IsActive == 0);
                    bool issued = (this.mItem.Status == MobileDevicesProxy.ISSUED);
                    bool charging = ((!issued && oncharge==0 && chargetime>=0 && chargeremain>0) || (issued && oncharge==0 && chargetime>=0 && chargeremain>0));
                    bool charged = ((!issued && (oncharge==0 && chargetime > 0 && chargeremain<=0)) || (issued && (oncharge==0 && chargetime > 0 && chargeremain<=0)));
                    return (!inactive && (charging || charged));
                }
                else
                    return false;
            } 
        }
        public void EndCharge() {
            BatteryItem item = this.mItem;
            DialogResult res = MessageBox.Show(this,"End battery " + item.ItemID + " charge cycle?",this.Name,MessageBoxButtons.OKCancel);
            if(res==DialogResult.OK) {
                //Read existing details for row version and request cancel
                int index = (this.grdItems.Selected.Rows.Count > 0) ? this.grdItems.Selected.Rows[0].VisibleIndex : -1;
                try {
                    setStatusMessage("Ending charge cycle for " + item.ItemID + "...");
                    if(MobileDevicesProxy.EndBatteryItemChargeCycle(item))
                        setStatusMessage("Charge cycle for " + item.ItemID + " was ended.");
                    else
                        MessageBox.Show(this,"Charge cycle for " + item.ItemID + " could not be ended. Please try again.",this.Text,MessageBoxButtons.OK);
                }
                catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
                finally {
                    if(this.grdItems.Rows.VisibleRowCount > 0) {
                        if(index >= 0 && index < this.grdItems.Rows.VisibleRowCount)
                            this.grdItems.Rows.GetRowAtVisibleIndex(index).Selected = true;
                        else
                            this.grdItems.Rows.GetRowAtVisibleIndex(0).Selected = true;
                        this.grdItems.Selected.Rows[0].Activate();
                    }
                    OnGridSelectionChanged(this.grdItems,null);
                }
            }
        }
        public bool CanChangeAssignments { 
            get {
                if(!App.Config.ReadOnly && this.grdItems.Focused && this.grdItems.Selected.Rows.Count > 0 && this.mItem != null) {
                    bool inactive = (this.mItem.IsActive == 0);
                    bool issued = (this.mItem.Status == MobileDevicesProxy.ISSUED);
                    return (!inactive && issued);
                }
                else if(!App.Config.ReadOnly && this.grdDriverAssignments.Focused && this.grdDriverAssignments.Selected.Rows.Count > 0 && this.mDriver != null) {
                    if(this.grdDriverAssignments.Selected.Rows.Count > 0)
                        return !this.grdDriverAssignments.Selected.Rows[0].HasParent();
                    else
                        return false;
                }
                else
                    return false; 
            } 
        }
        public void ChangeAssignments() {
            if(this.grdItems.Focused) {
                //Select driver in assignment pane if user has selected an assigned battery
                string sDriver = this.grdItems.Selected.Rows[0].Cells["DriverName"].Value.ToString();
                for(int i=0;i<this.grdDriverAssignments.Rows.Count;i++) {
                    if(this.grdDriverAssignments.Rows[i].Cells["FullName"].Value.ToString() == sDriver) {
                        this.grdDriverAssignments.Rows[i].Selected = true;
                        this.grdDriverAssignments.Rows[i].Activate();
                        this.grdDriverAssignments.Rows[i].Expanded = true;
                        break;
                    }
                }
            }
            try {
                LocalDriver driver = this.mDriver;
                dlgDriverAssignments dlgAssignments = new dlgDriverAssignments(driver);
                DialogResult res = dlgAssignments.ShowDialog(this);
                if(res==DialogResult.OK) {
                    //Change the battery assignments for the selected driver
                    this.mGridSvc.CaptureState();
                    this.mAssignmentGridSvc.CaptureState();
                    setStatusMessage("Changing battery assignment for " + driver.FullName + "...");
                    BatteryItemAssignments assignments = dlgAssignments.DriverAssignments;
                    for(int i=0;i<assignments.Count;i++) {
                        bool assign = assignments[i].AssignedDate>DateTime.MinValue;
                        bool hasDriver = assignments[i].DriverID > 0 ? true : false;
                        if(assign && !hasDriver) {
                            //For assign
                            assignments[i].AssignedUser = Environment.UserName;
                            assignments[i].DriverID = this.mDriver.DriverID;
                            if(MobileDevicesProxy.AssignBatteryItem(assignments[i]))
                                setStatusMessage("Battery " + assignments[i].ItemID + " assigned to " + driver.FullName + ".");
                            else
                                MessageBox.Show(this,"Battery " + assignments[i].ItemID + " could not be assigned to " + driver.FullName + ". Please try again.",this.Text,MessageBoxButtons.OK);
                        }
                        else if(!assign && hasDriver) {
                            //For unassign
                            assignments[i].AssignedUser = Environment.UserName;
                            assignments[i].AssignedDate = DateTime.Now;
                            if(MobileDevicesProxy.UnassignBatteryItem(assignments[i]))
                                setStatusMessage("Battery " + assignments[i].ItemID + " unassigned from " + driver.FullName + ".");
                            else
                                MessageBox.Show(this,"Battery " + assignments[i].ItemID + " could not be unassigned from " + driver.FullName + ". Please try again.",this.Text,MessageBoxButtons.OK);
                        }
                    }
                    Refresh2();
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
            finally {
                this.mGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdItems,null);
                this.mAssignmentGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdDriverAssignments,null);
            }
        }

        protected override void Dispose(bool disposing) { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winBatteries));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("BatteryItem",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleEnd");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CycleStart");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeviceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ElapsedTimeCharging");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InServiceDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MinHoursToCharge");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NumberOfCycles");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RemainingTimeCharging");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TimeElapsedSinceComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comments");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Created");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CreatedUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Driver");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn85 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn86 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn87 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StatusList");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn88 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Terminal");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn89 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn90 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn91 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("LocalDriver",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Assignments");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NumberOfBatteries");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BadgeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FullName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Phone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Terminal");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Assignments",0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comments");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            this.pnlBatterys = new System.Windows.Forms.Panel();
            this.cboTerminals = new System.Windows.Forms.ComboBox();
            this.mTerminals = new System.Windows.Forms.BindingSource(this.components);
            this.chkShowInActive = new System.Windows.Forms.CheckBox();
            this._lblBatteryFind = new System.Windows.Forms.Label();
            this.txtBatteryScan = new System.Windows.Forms.TextBox();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdItems = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxBatteries = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxStartCharge = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxEndCharge = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxAssignments = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mItems = new System.Windows.Forms.BindingSource(this.components);
            this.grdDriverAssignments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mAssignments = new System.Windows.Forms.BindingSource(this.components);
            this.btnEndCharge = new System.Windows.Forms.Button();
            this.btnStartCharge = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this._lblInputsDriver = new System.Windows.Forms.Label();
            this.btnPinInputs = new System.Windows.Forms.Button();
            this._lblInputs = new System.Windows.Forms.Label();
            this.pnlBatterys.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.ctxBatteries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverAssignments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignments)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBatterys
            // 
            this.pnlBatterys.Controls.Add(this.cboTerminals);
            this.pnlBatterys.Controls.Add(this.chkShowInActive);
            this.pnlBatterys.Controls.Add(this._lblBatteryFind);
            this.pnlBatterys.Controls.Add(this.txtBatteryScan);
            this.pnlBatterys.Controls.Add(this.splitterH);
            this.pnlBatterys.Controls.Add(this.grdItems);
            this.pnlBatterys.Controls.Add(this.grdDriverAssignments);
            this.pnlBatterys.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBatterys.Location = new System.Drawing.Point(0,0);
            this.pnlBatterys.Name = "pnlBatterys";
            this.pnlBatterys.Size = new System.Drawing.Size(472,233);
            this.pnlBatterys.TabIndex = 13;
            // 
            // cboTerminals
            // 
            this.cboTerminals.DataSource = this.mTerminals;
            this.cboTerminals.DisplayMember = "TerminalName";
            this.cboTerminals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminals.Location = new System.Drawing.Point(93,0);
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
            // chkShowInActive
            // 
            this.chkShowInActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowInActive.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.chkShowInActive.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.chkShowInActive.Location = new System.Drawing.Point(325,107);
            this.chkShowInActive.Name = "chkShowInActive";
            this.chkShowInActive.Size = new System.Drawing.Size(144,18);
            this.chkShowInActive.TabIndex = 90;
            this.chkShowInActive.Text = "Show InActive";
            this.chkShowInActive.UseVisualStyleBackColor = false;
            this.chkShowInActive.CheckedChanged += new System.EventHandler(this.OnShowInActiveDrivers);
            // 
            // _lblBatteryFind
            // 
            this._lblBatteryFind.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblBatteryFind.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._lblBatteryFind.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this._lblBatteryFind.Image = ((System.Drawing.Image)(resources.GetObject("_lblBatteryFind.Image")));
            this._lblBatteryFind.Location = new System.Drawing.Point(300,3);
            this._lblBatteryFind.Name = "_lblBatteryFind";
            this._lblBatteryFind.Size = new System.Drawing.Size(16,16);
            this._lblBatteryFind.TabIndex = 91;
            this._lblBatteryFind.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBatteryScan
            // 
            this.txtBatteryScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBatteryScan.Location = new System.Drawing.Point(324,0);
            this.txtBatteryScan.Name = "txtBatteryScan";
            this.txtBatteryScan.Size = new System.Drawing.Size(144,21);
            this.txtBatteryScan.TabIndex = 89;
            this.txtBatteryScan.TextChanged += new System.EventHandler(this.OnBatteryScanned);
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0,101);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(472,3);
            this.splitterH.TabIndex = 8;
            this.splitterH.TabStop = false;
            // 
            // grdItems
            // 
            this.grdItems.ContextMenuStrip = this.ctxBatteries;
            this.grdItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdItems.DataSource = this.mItems;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdItems.DisplayLayout.Appearance = appearance1;
            ultraGridColumn23.Header.VisiblePosition = 17;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.Caption = "Charged";
            ultraGridColumn24.Header.VisiblePosition = 13;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn24.Width = 48;
            ultraGridColumn25.Format = "MM/dd/yyyy HH:mm";
            ultraGridColumn25.Header.Caption = "Charge End";
            ultraGridColumn25.Header.VisiblePosition = 12;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn25.Width = 120;
            ultraGridColumn26.Format = "MM/dd/yyyy HH:mm";
            ultraGridColumn26.Header.Caption = "Charge Start";
            ultraGridColumn26.Header.VisiblePosition = 9;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn26.Width = 120;
            ultraGridColumn27.Header.VisiblePosition = 7;
            ultraGridColumn27.Hidden = true;
            ultraGridColumn28.Header.VisiblePosition = 3;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.Caption = "Chg Elapsed";
            ultraGridColumn29.Header.VisiblePosition = 10;
            ultraGridColumn29.Width = 72;
            ultraGridColumn30.Header.VisiblePosition = 23;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 15;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.Caption = "Cycles";
            ultraGridColumn32.Header.VisiblePosition = 14;
            ultraGridColumn32.Width = 48;
            ultraGridColumn33.Header.Caption = "Chg Remain";
            ultraGridColumn33.Header.VisiblePosition = 11;
            ultraGridColumn33.Width = 72;
            ultraGridColumn34.Header.Caption = "Runtime";
            ultraGridColumn34.Header.VisiblePosition = 8;
            ultraGridColumn34.Width = 72;
            ultraGridColumn35.Header.VisiblePosition = 18;
            ultraGridColumn35.Width = 192;
            ultraGridColumn36.Header.VisiblePosition = 21;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 22;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.VisiblePosition = 16;
            ultraGridColumn38.Hidden = true;
            ultraGridColumn39.Header.Caption = "Driver";
            ultraGridColumn39.Header.VisiblePosition = 6;
            ultraGridColumn39.Width = 168;
            ultraGridColumn40.Header.VisiblePosition = 19;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.Caption = "Battery ID";
            ultraGridColumn41.Header.VisiblePosition = 0;
            ultraGridColumn41.Width = 96;
            ultraGridColumn42.Header.VisiblePosition = 25;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn85.Header.VisiblePosition = 26;
            ultraGridColumn85.Hidden = true;
            ultraGridColumn86.Header.VisiblePosition = 1;
            ultraGridColumn86.Hidden = true;
            ultraGridColumn86.Width = 72;
            ultraGridColumn87.Header.VisiblePosition = 20;
            ultraGridColumn87.Hidden = true;
            ultraGridColumn88.Header.VisiblePosition = 5;
            ultraGridColumn88.Width = 168;
            ultraGridColumn89.Header.VisiblePosition = 4;
            ultraGridColumn89.Hidden = true;
            ultraGridColumn90.Header.Caption = "Type";
            ultraGridColumn90.Header.VisiblePosition = 2;
            ultraGridColumn90.Hidden = true;
            ultraGridColumn90.Width = 96;
            ultraGridColumn91.Header.VisiblePosition = 24;
            ultraGridColumn91.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn85,
            ultraGridColumn86,
            ultraGridColumn87,
            ultraGridColumn88,
            ultraGridColumn89,
            ultraGridColumn90,
            ultraGridColumn91});
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
            this.grdItems.Size = new System.Drawing.Size(472,104);
            this.grdItems.TabIndex = 11;
            this.grdItems.Text = "Batteries";
            this.grdItems.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdItems.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdItems.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
            this.grdItems.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdItems.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // ctxBatteries
            // 
            this.ctxBatteries.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCreate,
            this.ctxUpdate,
            this.ctxSep1,
            this.ctxStartCharge,
            this.ctxEndCharge,
            this.ctxSep2,
            this.ctxAssignments,
            this.ctxSep3,
            this.ctxRefresh});
            this.ctxBatteries.Name = "ctxBatteries";
            this.ctxBatteries.Size = new System.Drawing.Size(196,154);
            // 
            // ctxCreate
            // 
            this.ctxCreate.Name = "ctxCreate";
            this.ctxCreate.Size = new System.Drawing.Size(195,22);
            this.ctxCreate.Text = "&Create...";
            this.ctxCreate.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxUpdate
            // 
            this.ctxUpdate.Name = "ctxUpdate";
            this.ctxUpdate.Size = new System.Drawing.Size(195,22);
            this.ctxUpdate.Text = "&Update...";
            this.ctxUpdate.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(192,6);
            // 
            // ctxStartCharge
            // 
            this.ctxStartCharge.Name = "ctxStartCharge";
            this.ctxStartCharge.Size = new System.Drawing.Size(195,22);
            this.ctxStartCharge.Text = "&Start Charge Cycle";
            this.ctxStartCharge.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxEndCharge
            // 
            this.ctxEndCharge.Name = "ctxEndCharge";
            this.ctxEndCharge.Size = new System.Drawing.Size(195,22);
            this.ctxEndCharge.Text = "&End Charge Cycle";
            this.ctxEndCharge.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(192,6);
            // 
            // ctxAssignments
            // 
            this.ctxAssignments.Name = "ctxAssignments";
            this.ctxAssignments.Size = new System.Drawing.Size(195,22);
            this.ctxAssignments.Text = "Change Assignments...";
            this.ctxAssignments.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxSep3
            // 
            this.ctxSep3.Name = "ctxSep3";
            this.ctxSep3.Size = new System.Drawing.Size(192,6);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(195,22);
            this.ctxRefresh.Text = "&Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mItems
            // 
            this.mItems.DataSource = typeof(Argix.Terminals.BatteryItems);
            // 
            // grdDriverAssignments
            // 
            this.grdDriverAssignments.ContextMenuStrip = this.ctxBatteries;
            this.grdDriverAssignments.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdDriverAssignments.DataSource = this.mAssignments;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdDriverAssignments.DisplayLayout.Appearance = appearance5;
            ultraGridColumn1.Header.VisiblePosition = 15;
            ultraGridColumn2.Header.Caption = "# of Batteries";
            ultraGridColumn2.Header.VisiblePosition = 10;
            ultraGridColumn2.Width = 120;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 6;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.VisiblePosition = 0;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Header.VisiblePosition = 1;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.Caption = "Driver";
            ultraGridColumn8.Header.VisiblePosition = 5;
            ultraGridColumn8.Width = 192;
            ultraGridColumn9.Header.Caption = "Active";
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.Width = 96;
            ultraGridColumn10.Header.VisiblePosition = 3;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 12;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 13;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.VisiblePosition = 8;
            ultraGridColumn14.Width = 168;
            ultraGridColumn15.Header.VisiblePosition = 7;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 14;
            ultraGridColumn16.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
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
            ultraGridColumn16});
            ultraGridColumn17.Format = "MM/dd/yyyy";
            ultraGridColumn17.Header.Caption = "Assigned On";
            ultraGridColumn17.Header.VisiblePosition = 2;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.Caption = "Assigned By";
            ultraGridColumn18.Header.VisiblePosition = 1;
            ultraGridColumn18.Width = 168;
            ultraGridColumn19.Header.VisiblePosition = 3;
            ultraGridColumn19.Width = 120;
            ultraGridColumn20.Header.VisiblePosition = 4;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn21.Header.Caption = "Battery ID";
            ultraGridColumn21.Header.VisiblePosition = 0;
            ultraGridColumn22.Header.VisiblePosition = 5;
            ultraGridColumn22.Hidden = true;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22});
            this.grdDriverAssignments.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdDriverAssignments.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdDriverAssignments.DisplayLayout.CaptionAppearance = appearance6;
            this.grdDriverAssignments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDriverAssignments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDriverAssignments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDriverAssignments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdDriverAssignments.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdDriverAssignments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdDriverAssignments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdDriverAssignments.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdDriverAssignments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdDriverAssignments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDriverAssignments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDriverAssignments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDriverAssignments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdDriverAssignments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdDriverAssignments.Location = new System.Drawing.Point(0,104);
            this.grdDriverAssignments.Name = "grdDriverAssignments";
            this.grdDriverAssignments.Size = new System.Drawing.Size(472,129);
            this.grdDriverAssignments.TabIndex = 12;
            this.grdDriverAssignments.Text = "Battery Assigments";
            this.grdDriverAssignments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdDriverAssignments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdDriverAssignments.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
            this.grdDriverAssignments.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // mAssignments
            // 
            this.mAssignments.DataSource = typeof(Argix.Terminals.LocalDriver);
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
            // winBatteries
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(472,233);
            this.Controls.Add(this.pnlBatterys);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winBatteries";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batteries";
            this.Deactivate += new System.EventHandler(this.OnFormDeactivated);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.pnlBatterys.ResumeLayout(false);
            this.pnlBatterys.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.ctxBatteries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdDriverAssignments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignments)).EndInit();
            this.ResumeLayout(false);

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
				this.mToolTip.SetToolTip(this.txtBatteryScan, "Scan a battery");
				#endregion
				#region Grid Initialization
				this.grdItems.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdItems.DisplayLayout.Bands[0].Columns["State"].SortIndicator = SortIndicator.Ascending;
				this.grdDriverAssignments.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdDriverAssignments.DisplayLayout.Bands[0].Columns["FullName"].SortIndicator = SortIndicator.Ascending;
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
                OnTerminalChanged(null,null);
                Refresh2();
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { setServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormActivated(object sender,System.EventArgs e) { if(UpdateServices != null) UpdateServices(this,EventArgs.Empty); }
        private void OnFormDeactivated(object sender,System.EventArgs e) { if(UpdateServices != null) UpdateServices(this,EventArgs.Empty); }
        private void OnFormResize(object sender,System.EventArgs e) {
			//Event hanlder for change in form size
			if(this.WindowState != FormWindowState.Minimized) {
				if(this.grdItems.Height < this.splitterH.MinExtra)
					this.grdDriverAssignments.Height = this.pnlBatterys.Height - this.splitterH.MinExtra;
				else if(this.grdDriverAssignments.Height < this.splitterH.MinSize)
					this.grdDriverAssignments.Height = this.splitterH.MinSize;
			}
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) { }
		private void OnTerminalChanged(object sender, System.EventArgs e) {
			//Terminal changed event handler
			try {
				//Set valid filter configuration; clear filters and reset
                this.mGridSvc.CaptureState();
                this.grdItems.DisplayLayout.Bands[0].ColumnFilters["Terminal"].FilterConditions.Clear();
				if(this.cboTerminals.Text != "All") 
					this.grdItems.DisplayLayout.Bands[0].ColumnFilters["Terminal"].FilterConditions.Add(FilterComparisionOperator.Equals, this.cboTerminals.Text);
				this.grdItems.DisplayLayout.RefreshFilters();
                this.mGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdItems,null);
			}
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
			finally { setServices(); }
			OnShowInActiveDrivers(null,null);
		}
		#region Grid Support: OnGridInitializeLayout(), OnGridInitializeRow(), GridSelectionChanged(), GridMouseDown(), GridDoubleClick(), OnShowInActiveDrivers()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
			try {
				e.Layout.Bands[0].Columns.Insert(1, "State");
				e.Layout.Bands[0].Columns["State"].DataType = typeof(int);
				e.Layout.Bands[0].Columns["State"].Width = 48;
				e.Layout.Bands[0].Columns["State"].Header.Appearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["State"].CellAppearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["State"].SortIndicator = SortIndicator.Ascending;
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//Calculate battery state				
				string status = e.Row.Cells["Status"].Value.ToString();
				bool issued = e.Row.Cells["DriverName"].Value.ToString().Length > 0;
				//string chargestart = e.Row.Cells["CycleStart"].Value.ToString();
				double chargetime = convertTimeToHours(e.Row.Cells["ElapsedTimeCharging"].Value);
				double chargeremain = convertTimeToHours(e.Row.Cells["RemainingTimeCharging"].Value);
				//string chargeend = e.Row.Cells["CycleEnd"].Value.ToString();
				int oncharge = (e.Row.Cells["CycleComplete"].Value != DBNull.Value) ? Convert.ToInt32(e.Row.Cells["CycleComplete"].Value) : 0;
				double runtime = convertTimeToHours(e.Row.Cells["TimeElapsedSinceComplete"].Value);
				
				bool inactive = (status.ToLower()=="inactive");
				bool discharged = !inactive && ((oncharge==0 && runtime<= 0 && chargetime<=0 && chargeremain<=0) || (!issued && oncharge==1 && runtime>App.Config.BatteryRunTimeAvailable) || (issued && oncharge==1 && runtime>App.Config.BatteryRunTimeIssued));
				bool charging = !inactive && ((!issued && oncharge==0 && chargetime>=0 && chargeremain>0) || (issued && oncharge==0 && chargetime>=0 && chargeremain>0));
				bool charged = !inactive && ((!issued && (oncharge==0 && chargeremain<=0)) || (issued && (oncharge==0 && chargeremain<=0)));
				bool chargecomplete = !inactive && ((!issued && (oncharge==1 && runtime<(App.Config.BatteryRunTimeAvailable-App.Config.BatteryRunTimeWarning))) || (issued && (oncharge==1 && runtime<(App.Config.BatteryRunTimeIssued-App.Config.BatteryRunTimeWarning))));
				bool lowcharge = !inactive && ((!issued && oncharge==1 && runtime>=(App.Config.BatteryRunTimeAvailable-App.Config.BatteryRunTimeWarning) && runtime<App.Config.BatteryRunTimeAvailable) || (issued && oncharge==1 && runtime>=(App.Config.BatteryRunTimeIssued-App.Config.BatteryRunTimeWarning) && runtime<App.Config.BatteryRunTimeIssued));
				if(inactive) {
					e.Row.Cells["State"].Value = INACTIVE;
					e.Row.Cells["State"].Appearance.BackColor = System.Drawing.Color.White;
				}
				else if(discharged) {
					e.Row.Cells["State"].Value = DISCHARGED;
					e.Row.Cells["State"].Appearance.BackColor = System.Drawing.Color.Red;
				}
				else if(charging) {
					e.Row.Cells["State"].Value = CHARGING;
					e.Row.Cells["State"].Appearance.BackColor = System.Drawing.Color.Yellow;
				}
				else if(charged) {
					e.Row.Cells["State"].Value = CHARGED;
					e.Row.Cells["State"].Appearance.BackColor = System.Drawing.Color.Green;
				}
				else if(chargecomplete) {
					e.Row.Cells["State"].Value = CHARGECOMPLETE;
					e.Row.Cells["State"].Appearance.BackColor = System.Drawing.Color.White;
				}
				else if(lowcharge) {
					e.Row.Cells["State"].Value = LOWCHARGE;
					e.Row.Cells["State"].Appearance.BackColor = System.Drawing.Color.Orange;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			try {
				//Select grid and forward to update
				UltraGrid grid = (UltraGrid)sender;
				switch(grid.Name) {
					case "grdItems": 
						this.mItem = null;
						if(this.grdItems.Selected.Rows.Count > 0) {
							string itemID = this.grdItems.Selected.Rows[0].Cells["ItemID"].Value.ToString();
							this.mItem = MobileDevicesProxy.GetBatteryItem(itemID);
						}
						break;
					case "grdDriverAssignments": 
						this.mDriver = null;
						if(this.grdDriverAssignments.Selected.Rows.Count > 0) {
							int driverID = Convert.ToInt32(this.grdDriverAssignments.Selected.Rows[0].Cells["DriverID"].Value);
                            this.mDriver = MobileDevicesProxy.GetDriver(driverID);
						}
						break;
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
					switch(grid.Name) {
						case "grdItems":				if(CanEdit) Edit(); break;
                        case "grdDriverAssignments": if(CanChangeAssignments) ChangeAssignments(); break; 
					}
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnShowInActiveDrivers(object sender, System.EventArgs e) {
			//Event handler for filtering active\inactive drivers
			int index=-1;
			try {
				//
				index = -1;
				bool expanded=false;
				if(this.grdDriverAssignments.Selected.Rows.Count > 0) {
					UltraGridRow row = this.grdDriverAssignments.Selected.Rows[0];
					index = (row.HasParent()) ? row.ParentRow.VisibleIndex : row.VisibleIndex;
					expanded = (row.HasParent()) ? row.ParentRow.IsExpanded : row.IsExpanded;
				}
				this.grdDriverAssignments.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
				if(this.cboTerminals.Text != "All") {
					this.grdDriverAssignments.DisplayLayout.Bands[0].ColumnFilters["Terminal"].FilterConditions.Add(FilterComparisionOperator.Equals, this.cboTerminals.Text);
					this.grdDriverAssignments.DisplayLayout.Bands[0].ColumnFilters["Terminal"].LogicalOperator = FilterLogicalOperator.Or;
				}
				if(!this.chkShowInActive.Checked) {
					this.grdDriverAssignments.DisplayLayout.Bands[0].ColumnFilters["IsActive"].FilterConditions.Add(FilterComparisionOperator.Equals, 1);
					this.grdDriverAssignments.DisplayLayout.Bands[0].ColumnFilters["IsActive"].LogicalOperator = FilterLogicalOperator.Or;
				}
				this.grdDriverAssignments.DisplayLayout.RefreshFilters();
				if(this.grdDriverAssignments.Rows.VisibleRowCount > 0) {
					if(index >=0 && index < this.grdDriverAssignments.Rows.VisibleRowCount) 
						this.grdDriverAssignments.Rows.GetRowAtVisibleIndex(index).Selected = true;
					else
						this.grdDriverAssignments.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdDriverAssignments.Selected.Rows[0].Activate();
					this.grdDriverAssignments.Selected.Rows[0].Expanded = expanded;;
				}
				OnGridSelectionChanged(this.grdDriverAssignments, null);
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private double convertTimeToHours(object o) {
			if(o != DBNull.Value) {
				string s = o.ToString().Replace(":", ".");
				if(s.Length > 0)
					return Convert.ToDouble(s);
				else
					return 0.0;
			}
			else
				return 0.0;
		}
		#endregion
		#region Scan Support: BatteryScanned()
		private void OnBatteryScanned(object sender, System.EventArgs e) {
			//Evemt hanlder for battery barcode scan
			UltraGridRow oRow=null;
			try {
				if(this.txtBatteryScan.Text.Length == App.Config.BatteryScanLength) {
					//Eliminate leading 0 from Barcode 128C; find battery
					this.txtBatteryScan.Text = this.txtBatteryScan.Text.Substring(1, App.Config.BatteryScanLength-1);
					for(int i=0; i<this.grdItems.Rows.Count; i++) {
						if(this.grdItems.Rows[i].Cells["ItemID"].Value.ToString() == this.txtBatteryScan.Text) {
							oRow = this.grdItems.Rows[i];
							break;
						}
					}
					if(oRow != null) {
						//Existing battery
						bool bGo = (oRow.VisibleIndex >= 0);
						if(oRow.VisibleIndex < 0) {
							//Query to change terminal
							long lTerminal = Convert.ToInt64(oRow.Cells["TerminalID"].Value);
							string sTerminal = oRow.Cells["Terminal"].Value.ToString().Trim();
							if(MessageBox.Show(this, "Battery is located in " + sTerminal + " terminal. Would you like to switch terminals?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)==DialogResult.Yes) {
								this.cboTerminals.SelectedValue = lTerminal;
								OnTerminalChanged(null, null);
								bGo = true;
							}
						}
						if(bGo) {
							//Select device; launch action
							oRow.Selected = true;
							oRow.Activate();
							string sState = oRow.Cells["Status"].Value.ToString();
							switch(sState) {
                                case "Available": Edit(); break;
								case "Issued":		
									this.grdItems.Focus();	//Flag for next statement to work
									ChangeAssignments(); 
									break;
								case "Discharged":	StartCharge(); break;
                                case "Charging": EndCharge(); break;
								case "InActive":	Edit(); break;
							}
						}
					}
					else {
						//New battery
						if(CanCreate) Create();
					}
					this.txtBatteryScan.Text = "";
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		#endregion
        private void OnMenuItemClicked(object sender,System.EventArgs e) {
			//Menu itemclicked-apply selected service
			try  {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "ctxCreate": Create(); break;
                    case "ctxUpdate": Edit(); break;
                    case "ctxStartCharge": StartCharge(); break;
                    case "ctxEndCharge": EndCharge(); break;
                    case "ctxAssignments": ChangeAssignments(); break;
                    case "ctxRefresh": Refresh2(); break;
                }
            }
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { setServices(); this.Cursor = Cursors.Default; }
		}
        private void setServices() {
			//Set user services depending upon an item selected in the grid
            this.cboTerminals.Enabled = ((this.cboTerminals.Items.Count>0));
            this.ctxCreate.Enabled = CanCreate;
            this.ctxUpdate.Enabled = CanEdit;
            this.ctxStartCharge.Enabled = CanStartCharge;
			this.ctxEndCharge.Enabled = CanEndCharge;
			this.ctxAssignments.Enabled = CanChangeAssignments;
            this.ctxRefresh.Enabled = true;
            if(UpdateServices != null) UpdateServices(this,EventArgs.Empty);
        }
        private void setStatusMessage(string message) { ((winMain)this.Parent.Parent).SetStatusMessage(message); }
    }
}
