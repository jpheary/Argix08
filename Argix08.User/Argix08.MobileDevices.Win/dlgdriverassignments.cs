using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Terminals {
	//
	public class dlgDriverAssignments : System.Windows.Forms.Form {
		//Members
		private LocalDriver mDriver=null;
        private BatteryItemAssignments mDriverAssignments=null;
        #region Controls

		private System.Windows.Forms.TextBox txtBatteryInput;
        private System.Windows.Forms.Button btnClose;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdAssignments;
		private Infragistics.Win.UltraWinEditors.UltraDateTimeEditor dtGrid;
        private System.Windows.Forms.CheckBox _chkShowUnassigned;
        private BindingSource mAssignments;
        private Button btnOK;
        private IContainer components;
        #endregion

        //Interface
		public dlgDriverAssignments(LocalDriver driver) {
			//Constructor
			try {
				InitializeComponent();
				#region Window docking
				this.grdAssignments.Controls.AddRange(new Control[]{this.txtBatteryInput, this._chkShowUnassigned});
				this.txtBatteryInput.Top = 0;
				this.txtBatteryInput.Left = 96;
				this._chkShowUnassigned.Top = 3;
				this._chkShowUnassigned.Left = this.grdAssignments.Width - this._chkShowUnassigned.Width - 3;
				#endregion

				//Initialize members
				this.mDriver = driver;
				this.Text = "Battery Assignments for " + this.mDriver.FullName + ".";
			} 
			catch(Exception ex) { throw new ApplicationException("Failed to create new Driver Assignments dialog", ex); }
		}
        public BatteryItemAssignments DriverAssignments { get { return this.mDriverAssignments; } }
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Assignments",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comments");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ItemID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgDriverAssignments));
            this.btnClose = new System.Windows.Forms.Button();
            this.txtBatteryInput = new System.Windows.Forms.TextBox();
            this._chkShowUnassigned = new System.Windows.Forms.CheckBox();
            this.grdAssignments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mAssignments = new System.Windows.Forms.BindingSource(this.components);
            this.dtGrid = new Infragistics.Win.UltraWinEditors.UltraDateTimeEditor();
            this.btnOK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.SystemColors.Control;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClose.Location = new System.Drawing.Point(411,312);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96,23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // txtBatteryInput
            // 
            this.txtBatteryInput.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBatteryInput.Location = new System.Drawing.Point(108,3);
            this.txtBatteryInput.Name = "txtBatteryInput";
            this.txtBatteryInput.Size = new System.Drawing.Size(120,21);
            this.txtBatteryInput.TabIndex = 0;
            this.txtBatteryInput.TextChanged += new System.EventHandler(this.OnDeviceIDEntered);
            // 
            // _chkShowUnassigned
            // 
            this._chkShowUnassigned.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._chkShowUnassigned.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._chkShowUnassigned.ForeColor = System.Drawing.SystemColors.HighlightText;
            this._chkShowUnassigned.Location = new System.Drawing.Point(360,6);
            this._chkShowUnassigned.Name = "_chkShowUnassigned";
            this._chkShowUnassigned.Size = new System.Drawing.Size(144,16);
            this._chkShowUnassigned.TabIndex = 8;
            this._chkShowUnassigned.Text = "Show Unassigned Batteries";
            this._chkShowUnassigned.UseVisualStyleBackColor = false;
            this._chkShowUnassigned.CheckedChanged += new System.EventHandler(this.OnShowUnassignedBatteries);
            // 
            // grdAssignments
            // 
            this.grdAssignments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAssignments.DataMember = "Assignments";
            this.grdAssignments.DataSource = this.mAssignments;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Format = "MM/dd/yyyy";
            ultraGridColumn1.Header.Caption = "Assigned On";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn1.Width = 96;
            ultraGridColumn2.Header.VisiblePosition = 3;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 4;
            ultraGridColumn3.Width = 288;
            ultraGridColumn4.Header.VisiblePosition = 2;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.Caption = "Battery ID";
            ultraGridColumn5.Header.VisiblePosition = 0;
            ultraGridColumn5.Width = 120;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6});
            this.grdAssignments.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.CaptionAppearance = appearance2;
            this.grdAssignments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAssignments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdAssignments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAssignments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdAssignments.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdAssignments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAssignments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAssignments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAssignments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAssignments.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAssignments.Location = new System.Drawing.Point(3,3);
            this.grdAssignments.Name = "grdAssignments";
            this.grdAssignments.Size = new System.Drawing.Size(507,294);
            this.grdAssignments.TabIndex = 7;
            this.grdAssignments.Text = "Battery Input";
            this.grdAssignments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdAssignments.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdAssignments.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnGridBeforeCellActivate);
            this.grdAssignments.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdAssignments.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellChange);
            // 
            // mAssignments
            // 
            this.mAssignments.DataSource = typeof(Argix.Terminals.LocalDriver);
            // 
            // dtGrid
            // 
            this.dtGrid.DateTime = new System.DateTime(2003,5,3,5,0,0,0);
            this.dtGrid.DropDownButtonDisplayStyle = Infragistics.Win.ButtonDisplayStyle.OnMouseEnter;
            this.dtGrid.FormatString = "MM/dd/yyyy";
            this.dtGrid.Location = new System.Drawing.Point(9,309);
            this.dtGrid.MaskClipMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
            this.dtGrid.MaskDisplayMode = Infragistics.Win.UltraWinMaskedEdit.MaskMode.Raw;
            this.dtGrid.MaskInput = "";
            this.dtGrid.MaxDate = new System.DateTime(2030,12,31,0,0,0,0);
            this.dtGrid.MinDate = new System.DateTime(2003,1,1,0,0,0,0);
            this.dtGrid.Name = "dtGrid";
            this.dtGrid.PromptChar = ' ';
            this.dtGrid.Size = new System.Drawing.Size(84,22);
            this.dtGrid.TabIndex = 95;
            this.dtGrid.Value = new System.DateTime(2003,5,3,5,0,0,0);
            this.dtGrid.Visible = false;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(309,312);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96,23);
            this.btnOK.TabIndex = 96;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // dlgDriverAssignments
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(513,340);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtBatteryInput);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this._chkShowUnassigned);
            this.Controls.Add(this.grdAssignments);
            this.Controls.Add(this.dtGrid);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgDriverAssignments";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Driver Assignments";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.GotFocus += new System.EventHandler(this.OnDeviceIDEntered);
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {				
				//Show early
				this.Visible = true;
				Application.DoEvents();
				#region Grid control
				this.grdAssignments.DisplayLayout.CaptionAppearance.FontData.SizeInPoints = 8;
				this.grdAssignments.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdAssignments.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdAssignments.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdAssignments.DisplayLayout.Bands[0].Columns["Assign"].CellActivation = Activation.AllowEdit;
				this.grdAssignments.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdAssignments.DisplayLayout.Bands[0].Columns["AssignedDate"].SortIndicator = SortIndicator.Descending;
				this.grdAssignments.DisplayLayout.Bands[0].Columns["AssignedDate"].EditorComponent = this.dtGrid;
				this.grdAssignments.DisplayLayout.Bands[0].Columns["AssignedDate"].CellActivation = Activation.AllowEdit;
				#endregion
				this._chkShowUnassigned.Checked = this.mDriver.IsActive == 1;
                this._chkShowUnassigned.Enabled = this.mDriver.IsActive == 1;

                this.mDriverAssignments = new BatteryItemAssignments();
                for(int i=0;i<this.mDriver.Assignments.Count;i++) {
                    BatteryItemAssignment assignment = new BatteryItemAssignment();
                    assignment.ItemID = this.mDriver.Assignments[i].ItemID;
                    assignment.DriverID = this.mDriver.Assignments[i].DriverID;
                    assignment.AssignedDate = this.mDriver.Assignments[i].AssignedDate;
                    assignment.AssignedUser = this.mDriver.Assignments[i].AssignedUser;
                    assignment.Comments = this.mDriver.Assignments[i].Comments;
                    assignment.RowVersion = this.mDriver.Assignments[i].RowVersion;
                    this.mDriverAssignments.Add(assignment);
                }

                BatteryItems items = MobileDevicesProxy.GetUnassignedBatteryItems(this.mDriver.TerminalID);
                for(int i=0;i<items.Count;i++) {
                    BatteryItemAssignment assignment = new BatteryItemAssignment();
                    assignment.ItemID = items[i].ItemID;
                    assignment.Comments = items[i].Comments;
                    assignment.RowVersion = items[i].RowVersion;
                    this.mDriverAssignments.Add(assignment);
                }
                this.mAssignments.DataSource = this.mDriverAssignments;
                OnShowUnassignedBatteries(null,null);
				this.txtBatteryInput.Focus();
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.btnOK.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnShowUnassignedBatteries(object sender, System.EventArgs e)  {
			//Show\hide unassigned batteries
			try {
				//Clear filter, set for all assigned, then unassigned if applicable
                this.grdAssignments.DisplayLayout.Bands[0].ColumnFilters["Assign"].FilterConditions.Clear();
                this.grdAssignments.DisplayLayout.Bands[0].ColumnFilters["Assign"].FilterConditions.Add(FilterComparisionOperator.Equals,true);
                if(this._chkShowUnassigned.Checked) {
                    this.grdAssignments.DisplayLayout.Bands[0].ColumnFilters["Assign"].FilterConditions.Add(FilterComparisionOperator.Equals,false);
                    this.grdAssignments.DisplayLayout.Bands[0].ColumnFilters["Assign"].LogicalOperator = FilterLogicalOperator.Or;
                }
				this.grdAssignments.DisplayLayout.RefreshFilters();
				this.txtBatteryInput.Focus();
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
        }
        #region Grid Services: OnGridInitializeLayout(), OnBatteryAssignmentChanged(), OnChangeAssignedDate(), OnAssignmentChanged()
        private void OnGridInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //Event handler for grid layout initialization
            try {
                e.Layout.Bands[0].Columns.Insert(0,"Assign");
                e.Layout.Bands[0].Columns["Assign"].DataType = typeof(bool);
                e.Layout.Bands[0].Columns["Assign"].Width = 72;
                e.Layout.Bands[0].Columns["Assign"].Header.Appearance.TextHAlign = HAlign.Center;
                e.Layout.Bands[0].Columns["Assign"].CellAppearance.TextHAlign = HAlign.Center;
            }
            catch(Exception ex) { App.ReportError(ex,false,Argix.Terminals.LogLevel.Warning); }
        }
        private void OnGridInitializeRow(object sender,Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //Event handler for grid row initialization
            try {
                DateTime dt = DateTime.Parse(e.Row.Cells["AssignedDate"].Value.ToString());
                e.Row.Cells["Assign"].Value = dt > DateTime.MinValue ? true : false;
            }
            catch(Exception ex) { App.ReportError(ex,false,Argix.Terminals.LogLevel.Warning); }
        }
        private void OnGridBeforeCellActivate(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for assigned cell activated
			DateTime dt;
			try {
				//Set date limits
				switch(e.Cell.Column.Key.ToString()) {
					case "Assign":
						e.Cell.Activation = Activation.AllowEdit;
						break;
					case "AssignedDate": 
						//Set assigned only if battery is assigned
						if(Convert.ToBoolean(e.Cell.Row.Cells["Assign"].Text)) {
							e.Cell.Activation = Activation.AllowEdit;
							
							//Set date limits for assignment
							this.dtGrid.MinDate = DateTime.Today.AddMonths(-1);
							this.dtGrid.MaxDate = DateTime.Today.AddMonths(1);
							
							//Validate (and correct) assigned value
							if(e.Cell.Value!=DBNull.Value) {
								dt = Convert.ToDateTime(e.Cell.Value);
								if(dt.Ticks<this.dtGrid.MinDate.Ticks)
									e.Cell.Value = this.dtGrid.MinDate;
								else if(dt.Ticks>this.dtGrid.MaxDate.Ticks)
									e.Cell.Value = this.dtGrid.MaxDate;
							}
						}
						else 
							e.Cell.Activation = Activation.NoEdit;
						break;
					default:	e.Cell.Activation = Activation.NoEdit; break;
				}
				this.dtGrid.Value = this.dtGrid.MinDate;
			} 
			catch(Exception) { }
		}
        private void OnGridCellChange(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a cell value
            try {
                //If "Assign" cell, set or clear "AssignedDate" cell required value
                if(e.Cell.Column.Key.ToString()=="Assign") {
                    if(Convert.ToBoolean(e.Cell.Text)==true)
                        e.Cell.Row.Cells["AssignedDate"].Value = DateTime.Today;
                    else
                        e.Cell.Row.Cells["AssignedDate"].Value = DateTime.MinValue;
                    this.OnValidateForm(null,null);
                }
            }
            catch(Exception) { }
        }
        private void OnGridAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //Validate; update assignment order by assignment
            try {
                //Update assignment
                this.grdAssignments.DisplayLayout.Bands[0].Columns["Assign"].ResetSortIndicator();
                this.grdAssignments.DisplayLayout.Bands[0].Columns["Assign"].SortIndicator = SortIndicator.Descending;
                this.grdAssignments.Refresh();
            }
            catch(Exception) { }
        }
        #endregion
		#region Device Search
		private void OnDeviceIDEntered(object sender, System.EventArgs e) {
			//Event handler for user enters a device id
			string deviceID = "";
			int iNewAssignments = 0;
			bool bAssigned = false, hasDriver = false, bFound = false;
			
			try {
				//Validate 11 characters before checking for assignment
				if(this.txtBatteryInput.Text.Length == 12) {
					//Eliminate leading 0 from Barcode 128C
					this.txtBatteryInput.Text = this.txtBatteryInput.Text.Substring(1, 11);
					
					//Search for an assigned or unassigned battery
					for(int i=0; i<this.grdAssignments.Rows.Count; i++) {
						//Find entered device id
						deviceID = this.grdAssignments.Rows[i].Cells["DeviceID"].Value.ToString();
						if(deviceID==this.txtBatteryInput.Text) {
							//Battery found- assign, unassign, or invalidate
							if(Convert.ToBoolean(this.grdAssignments.Rows[i].Cells["Assign"].Value)==true) {
								//An unassignment- unassign a current assignment or a new assignment
								this.grdAssignments.Rows[i].Cells["Assign"].Value = false;
								this.grdAssignments.ActiveRow = this.grdAssignments.Rows[i];
								this.grdAssignments.ActiveRow.Selected = true;
							}
							else {
								//An assignment- re-assign a current assignment or assign a new assignemnt
								hasDriver = (this.grdAssignments.Rows[i].Cells["DriverID"].Value!=DBNull.Value) ? true : false;
								if(hasDriver) {
									//Re-assign the current assignment (ie Undo unassign)
									this.grdAssignments.Rows[i].Cells["Assign"].Value = true;
									this.grdAssignments.ActiveRow = this.grdAssignments.Rows[i];
									this.grdAssignments.ActiveRow.Selected = true;
								}
								else {
									//New assignment- cannot exceed 2 new assignments
									iNewAssignments = 0;
									for(int j=0; j<this.grdAssignments.Rows.Count; j++) {
										//Count new assignments
										bAssigned = Convert.ToBoolean(this.grdAssignments.Rows[j].Cells["Assign"].Value);
										hasDriver = (this.grdAssignments.Rows[j].Cells["DriverID"].Value!=DBNull.Value) ? true : false;
										if(bAssigned && !hasDriver)  iNewAssignments++;

									}
									if(iNewAssignments<2) {
										this.grdAssignments.Rows[i].Cells["Assign"].Value = true;
										this.grdAssignments.ActiveRow = this.grdAssignments.Rows[i];
										this.grdAssignments.ActiveRow.Selected = true;
									}
								}
							}
							bFound = true;
							this.txtBatteryInput.Text = "";
							this.txtBatteryInput.Focus();
							break;
						}
					}
					if(!bFound) 
						//Notify user
						MessageBox.Show(this, "Entered device ID could not be found as an exisiting battery (either assigned or unassigned).", this.Text, MessageBoxButtons.OK);
				}
			}
			catch(Exception) { }		
		}
		#endregion
		private void OnValidateForm(object sender, System.EventArgs e) { 
			//Event handler for changes to control data
			try {
                this.btnOK.Enabled = true;
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) { 
			//Command button handler
            bool assign=false, hasDriver=false;
            int iAssigned=0;
			try {
				Button btn = (Button)sender;
				switch(btn.Name) {
					case "btnClose":		this.DialogResult = DialogResult.Cancel; this.Close(); break;
					case "btnOK":
						//Validate unassignments allow for assignments
                        for(int i=0;i<this.mDriverAssignments.Count;i++) {
                            //Count assigned and unassigned batteries for this driver
                            assign = this.mDriverAssignments[i].AssignedDate>DateTime.MinValue;
                            hasDriver = this.mDriverAssignments[i].DriverID > 0 ? true : false;
                            if(assign) iAssigned++;
                        }
                        if(iAssigned<=App.Config.AssignmentsMax) {						
                            //Create an assign/unassign list
                            int count = this.mDriverAssignments.Count;
                            for(int i=count-1;i>=0;i--) {
                                assign = this.mDriverAssignments[i].AssignedDate>DateTime.MinValue;
                                hasDriver = this.mDriverAssignments[i].DriverID > 0 ? true : false;
                                if(assign && !hasDriver) {
                                    //For assignment- leave in list
                                }
                                else if((assign && hasDriver) || (!assign && !hasDriver)) {
                                    //Current assignment or unassigned battery- remove from list
                                    this.mDriverAssignments.Remove(this.mDriverAssignments[i]);
                                }
                                else if(!assign && hasDriver) {
                                    //For unassignment- leave in list
                                }
                            }
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                            MessageBox.Show(this,"The total of current and new battery assignments cannot exceed " + App.Config.AssignmentsMax.ToString() + ".",this.Text,MessageBoxButtons.OK);
						break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
		}
	}
}