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
using Argix.Windows;

namespace Argix.Terminals {
	//
	public class winTypes : System.Windows.Forms.Form {
		//Members
		private ComponentType mType=null;
		private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;

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
        private Infragistics.Win.UltraWinGrid.UltraGrid grdComponents;
        private ContextMenuStrip ctxTypes;
        private ToolStripMenuItem ctxCreate;
        private ToolStripMenuItem ctxUpdate;
        private ToolStripSeparator ctxSep1;
        private BindingSource mTypes;
        private ToolStripMenuItem ctxRefresh;

		#endregion
				
		//Interface
		public winTypes() {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				this.Text = "Component Types";
				#region Window docking
				this.grdComponents.Dock = DockStyle.Fill;
				this.Controls.AddRange(new Control[]{this.grdComponents});
				#endregion
				
				//Create services
				this.mGridSvc = new UltraGridSvc(this.grdComponents);
				this.mToolTip = new System.Windows.Forms.ToolTip();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new Types window", ex); }
		}
        public bool CanNew { get { return false; } }
        public void New() { }
        public bool CanOpen { get { return false; } }
        public void Open() { }
        public bool CanExport { get { return this.grdComponents.Rows.VisibleRowCount > 0; } }
        public void Export() {
            SaveFileDialog dlgSave = new SaveFileDialog();
            dlgSave.AddExtension = true;
            dlgSave.Filter = "Text Files (*.xml) | *.xml";
            dlgSave.FilterIndex = 0;
            dlgSave.Title = "Export Data As...";
            dlgSave.FileName = "Components";
            dlgSave.OverwritePrompt = true;
            if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(ComponentTypes));
                FileStream fs = new FileStream(dlgSave.FileName,FileMode.Create,FileAccess.Write);
                dcs.WriteObject(fs,this.mTypes.DataSource);
                fs.Flush();
                fs.Close();
            }
        }
        public void PageSettings() { UltraGridPrinter.PageSettings(); }
        public void PrintPreview() { UltraGridPrinter.PrintPreview(this.grdComponents,"Component Types"); }
        public bool CanPrint { get { return this.grdComponents.Rows.VisibleRowCount > 0; } }
        public void Print(bool showDialog) { UltraGridPrinter.Print(this.grdComponents,"Component Types",showDialog); }
        public bool CanPrintLabel { get { return false; } }
        public void PrintLabel() { }
        public void Refresh2() {
            this.mGridSvc.CaptureState();
            try {
                setStatusMessage("Refreshing view of component types...");
                this.mTypes.DataSource = MobileDevicesProxy.GetComponentTypes();
                this.grdComponents.DisplayLayout.Bands[0].SortedColumns.RefreshSort(false);
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
            finally {
                this.mGridSvc.RestoreState();
                OnGridSelectionChanged(this.grdComponents,null);
            }
        }
        public bool CanCreate { get { return !App.Config.ReadOnly; } }
        public void Create() {
            try {
                ComponentType type = MobileDevicesProxy.GetComponentType("");
                dlgComponentType dlgType = new dlgComponentType(type);
                if(dlgType.ShowDialog(this) == DialogResult.OK) {
                    //Create a new component type
                    setStatusMessage("Creating new component type " + type.TypeID + "...");
                    if(MobileDevicesProxy.SaveComponentType(type)) {
                        setStatusMessage("Component type " + type.TypeID + " was created.");
                        Refresh2();
                    }
                    else
                        MessageBox.Show(this,"Component type " + type.TypeID + " could not be created. Please try again.",this.Text,MessageBoxButtons.OK);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
        }
        public bool CanEdit { get { return !App.Config.ReadOnly && this.grdComponents.Selected.Rows.Count > 0 && this.mType != null; } }
        public void Edit() {
            try {
                ComponentType type = this.mType;
                dlgComponentType dlgType = new dlgComponentType(type);
                if(dlgType.ShowDialog(this) == DialogResult.OK) {
                    //Update exisiting component type
                    setStatusMessage("Updating component type " + type.TypeID + "...");
                    if(MobileDevicesProxy.SaveComponentType(type)) {
                        setStatusMessage("Component type " + type.TypeID + " was updated.");
                        Refresh2();
                    }
                    else
                        MessageBox.Show(this,"Component type " + type.TypeID + " could not be updated. Please try again.",this.Text,MessageBoxButtons.OK);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
        }
        protected override void Dispose(bool disposing) { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ComponentType",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CategoryID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsNew");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winTypes));
            this.grdComponents = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxTypes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mTypes = new System.Windows.Forms.BindingSource(this.components);
            this.btnEndCharge = new System.Windows.Forms.Button();
            this.btnStartCharge = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this._lblInputsDriver = new System.Windows.Forms.Label();
            this.btnPinInputs = new System.Windows.Forms.Button();
            this._lblInputs = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdComponents)).BeginInit();
            this.ctxTypes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // grdComponents
            // 
            this.grdComponents.ContextMenuStrip = this.ctxTypes;
            this.grdComponents.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdComponents.DataSource = this.mTypes;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdComponents.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Category";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 96;
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 384;
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.VisiblePosition = 7;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Header.Caption = "Type";
            ultraGridColumn7.Header.VisiblePosition = 1;
            ultraGridColumn7.Width = 144;
            ultraGridColumn8.Header.VisiblePosition = 6;
            ultraGridColumn8.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8});
            this.grdComponents.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdComponents.DisplayLayout.CaptionAppearance = appearance2;
            this.grdComponents.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdComponents.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponents.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponents.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdComponents.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdComponents.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdComponents.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdComponents.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdComponents.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponents.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdComponents.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdComponents.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdComponents.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdComponents.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdComponents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdComponents.Location = new System.Drawing.Point(0,0);
            this.grdComponents.Name = "grdComponents";
            this.grdComponents.Size = new System.Drawing.Size(472,233);
            this.grdComponents.TabIndex = 8;
            this.grdComponents.Text = "Component Types";
            this.grdComponents.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdComponents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdComponents.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
            this.grdComponents.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // ctxTypes
            // 
            this.ctxTypes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCreate,
            this.ctxUpdate,
            this.ctxSep1,
            this.ctxRefresh});
            this.ctxTypes.Name = "ctxTypes";
            this.ctxTypes.Size = new System.Drawing.Size(114,76);
            // 
            // ctxCreate
            // 
            this.ctxCreate.Name = "ctxCreate";
            this.ctxCreate.Size = new System.Drawing.Size(113,22);
            this.ctxCreate.Text = "&Create";
            this.ctxCreate.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxUpdate
            // 
            this.ctxUpdate.Name = "ctxUpdate";
            this.ctxUpdate.Size = new System.Drawing.Size(113,22);
            this.ctxUpdate.Text = "&Update";
            this.ctxUpdate.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(110,6);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(113,22);
            this.ctxRefresh.Text = "&Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mTypes
            // 
            this.mTypes.DataSource = typeof(Argix.Terminals.ComponentTypes);
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
            // winTypes
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(472,233);
            this.Controls.Add(this.grdComponents);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winTypes";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Component Types";
            this.Deactivate += new System.EventHandler(this.OnFormDeactivated);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdComponents)).EndInit();
            this.ctxTypes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTypes)).EndInit();
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
				#endregion
				#region Grid Initialization
				this.grdComponents.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdComponents.DisplayLayout.Bands[0].Columns["TypeID"].SortIndicator = SortIndicator.Ascending;
				#endregion
                Refresh2();
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { setServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormActivated(object sender,System.EventArgs e) { if(UpdateServices != null) UpdateServices(this,EventArgs.Empty); }
        private void OnFormDeactivated(object sender,System.EventArgs e) { if(UpdateServices != null) UpdateServices(this,EventArgs.Empty); }
        private void OnFormResize(object sender,System.EventArgs e) { }
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) { }
        #region Grid Support: OnGridSelectionChanged(), GridMouseDown(), GridDoubleClick()
        private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			try {
				//Select grid and forward to update
				this.mType = null;
				if(this.grdComponents.Selected.Rows.Count > 0) {
					string typeID = this.grdComponents.Selected.Rows[0].Cells["TypeID"].Value.ToString();
					this.mType = MobileDevicesProxy.GetComponentType(typeID);
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
        private void OnMenuItemClicked(object sender,System.EventArgs e) {
            //Menu itemclicked-apply selected service
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "ctxCreate": Create(); break;
                    case "ctxUpdate": Edit(); break;
                    case "ctxRefresh": Refresh2(); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
            finally { setServices(); this.Cursor = Cursors.Default; }
        }
        private void setServices() {
			//Set user services
            this.ctxCreate.Enabled = CanCreate;
            this.ctxUpdate.Enabled = CanEdit;
			this.ctxRefresh.Enabled = true;
            if(UpdateServices != null) UpdateServices(this,EventArgs.Empty);
        }
        private void setStatusMessage(string message) { ((winMain)this.Parent.Parent).SetStatusMessage(message); }
    }
}
