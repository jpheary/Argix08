using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Argix;
using Argix.Windows;
using Argix.SQLReportServer;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.AgentLineHaul {
	//
	public class dlgSubscriptions : System.Windows.Forms.Form {
		//Members
		private string mReportName="";
		private ShipSchedule mSchedule=null;

        private const string PARAM_SORTCENTERID = "SortCenterID";
        private const string PARAM_SCHEDULEDATE = "ScheduleDate";
        private const string PARAM_CARRIERID = "CarrierID";
        private const string PARAM_AGENTID = "AgentNumber";
        private const string PARAM_TERMINALID = "TerminalID";
        
        private Infragistics.Win.UltraWinGrid.UltraGrid grdSubscriptions;
		private Argix.SubscriptionDS mSubscriptionDS;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRefresh;
		/// <summary>Required designer variable. </summary>
        private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Button btnSend;
				
		//Interface
		public dlgSubscriptions(string reportName, ShipSchedule schedule) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.mReportName = reportName;
				this.mSchedule = schedule;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgSubscriptions instance.", ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SubscriptionTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Send");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Report");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubscriptionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EventType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastRun");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Active");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliverySettings");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MatchData");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Parameters");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierAgentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSubscriptions));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.grdSubscriptions = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSubscriptionDS = new Argix.SubscriptionDS();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubscriptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubscriptionDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(649,228);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96,24);
            this.btnClose.TabIndex = 122;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(547,228);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(96,24);
            this.btnRefresh.TabIndex = 123;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.OnRefresh);
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Location = new System.Drawing.Point(437,228);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(96,24);
            this.btnSend.TabIndex = 124;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.OnSend);
            // 
            // grdSubscriptions
            // 
            this.grdSubscriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSubscriptions.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSubscriptions.DataMember = "SubscriptionTable";
            this.grdSubscriptions.DataSource = this.mSubscriptionDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdSubscriptions.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "TLViewTable";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 39;
            ultraGridColumn2.Header.VisiblePosition = 9;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 24;
            ultraGridColumn3.Header.Caption = "ID";
            ultraGridColumn3.Header.VisiblePosition = 10;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 24;
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Width = 192;
            ultraGridColumn5.Header.Caption = "Trigger";
            ultraGridColumn5.Header.VisiblePosition = 11;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 96;
            ultraGridColumn6.Format = "MM-dd-yyyy";
            ultraGridColumn6.Header.Caption = "Last Run";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 84;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 288;
            ultraGridColumn8.Header.VisiblePosition = 12;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn8.Width = 52;
            ultraGridColumn9.Header.Caption = "Delivery Settings";
            ultraGridColumn9.Header.VisiblePosition = 13;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn9.Width = 24;
            ultraGridColumn10.Header.Caption = "Match Data";
            ultraGridColumn10.Header.VisiblePosition = 14;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 24;
            ultraGridColumn11.Header.VisiblePosition = 15;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn11.Width = 24;
            ultraGridColumn12.Header.VisiblePosition = 1;
            ultraGridColumn12.Width = 288;
            ultraGridColumn13.Header.Caption = "Sort Center ID";
            ultraGridColumn13.Header.VisiblePosition = 7;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn13.Width = 120;
            ultraGridColumn14.Format = "MM-dd-yyyy";
            ultraGridColumn14.Header.Caption = "Schedule Date";
            ultraGridColumn14.Header.VisiblePosition = 8;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn14.Width = 96;
            ultraGridColumn15.Header.Caption = "Carrier/Agent";
            ultraGridColumn15.Header.VisiblePosition = 3;
            ultraGridColumn15.Width = 96;
            ultraGridColumn16.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn16.Header.Caption = "Terminal ID";
            ultraGridColumn16.Header.VisiblePosition = 2;
            ultraGridColumn16.Width = 144;
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
            ultraGridColumn16});
            this.grdSubscriptions.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdSubscriptions.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Inset;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdSubscriptions.DisplayLayout.CaptionAppearance = appearance2;
            this.grdSubscriptions.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSubscriptions.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdSubscriptions.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdSubscriptions.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSubscriptions.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdSubscriptions.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdSubscriptions.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdSubscriptions.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.grdSubscriptions.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSubscriptions.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSubscriptions.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSubscriptions.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSubscriptions.Location = new System.Drawing.Point(0,0);
            this.grdSubscriptions.Name = "grdSubscriptions";
            this.grdSubscriptions.Size = new System.Drawing.Size(753,222);
            this.grdSubscriptions.TabIndex = 121;
            this.grdSubscriptions.Text = "Subscriptions for Jamesburg 08/28/07 Schedule";
            this.grdSubscriptions.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdSubscriptions.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdSubscriptions.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdSubscriptions.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnGridBeforeCellActivate);
            this.grdSubscriptions.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdSubscriptions.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdSubscriptions.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellChange);
            // 
            // mSubscriptionDS
            // 
            this.mSubscriptionDS.DataSetName = "SubscriptionDS";
            this.mSubscriptionDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mSubscriptionDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dlgSubscriptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
            this.ClientSize = new System.Drawing.Size(752,256);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grdSubscriptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "dlgSubscriptions";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Subscriptions";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdSubscriptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubscriptionDS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				this.Text = "Subscriptions for " + this.mReportName + " report";
				this.grdSubscriptions.Text = "Schedule: " + this.mSchedule.SortCenter + " (" + this.mSchedule.SortCenterID.ToString() + "), " + this.mSchedule.ScheduleDate.ToShortDateString();
                #region Grid customizations from normal layout (to support cell editing)
                this.grdSubscriptions.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdSubscriptions.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdSubscriptions.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdSubscriptions.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdSubscriptions.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdSubscriptions.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdSubscriptions.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdSubscriptions.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdSubscriptions.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdSubscriptions.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdSubscriptions.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdSubscriptions.DisplayLayout.Bands[0].Columns["Send"].SortIndicator = SortIndicator.Descending;
                #endregion
                refresh();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
        private void OnClose(object sender,System.EventArgs e) {
            //Event handler for close button clicked
            this.Close();
        }
        private void OnRefresh(object sender,System.EventArgs e) {
            //Event handler for refresh button clicked
            this.Cursor = Cursors.WaitCursor;
            try {
                refresh();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnSend(object sender,System.EventArgs e) {
			//Event handler for sending subscriptions
			if(MessageBox.Show(this, "Do you want to send all checked subscriptions?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
				this.Cursor = Cursors.WaitCursor;
				try {
					sendReport();
					System.Threading.Thread.Sleep(10000);
					refresh();
				}
				catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
				finally { this.Cursor = Cursors.Default; }
			}
		}
        public void SendReport() {
            //
            refresh();
            sendReport();
        }
        #region Grid Servces: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridBeforeRowFilterDropDownPopulate(), OnGridMouseDown(), OnGridBeforeCellActivate(), OnGridCellChange()
        private void OnGridInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
        }
        private void OnGridInitializeRow(object sender,Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //
            if(e.Row.Band.Key == "SubscriptionTable") {
                bool allow=false;
                string terminalID = e.Row.Cells["TerminalID"].Value.ToString().Trim();
                if(terminalID.Length > 0) {
                    //Carrier subscriptions
                    string carrierID = e.Row.Cells["CarrierAgentID"].Value.ToString().Trim();
                    allow = (terminalID == this.mSchedule.SortCenterID.ToString() && (carrierID.Length > 0 && this.mSchedule.Trips.ShipScheduleTable.Select("carrierID=" + carrierID).Length > 0));
                }
                else {
                    //Agent subscriptions
                    string agentNumber = e.Row.Cells["CarrierAgentID"].Value.ToString().Trim();
                    allow = ((agentNumber.Length > 0) && (this.mSchedule.Trips.ShipScheduleTable.Select("AgentNumber='" + agentNumber + "'").Length > 0) || this.mSchedule.Trips.ShipScheduleTable.Select("S2AgentNumber='" + agentNumber + "'").Length > 0);
                }
                e.Row.Cells["Send"].Activation = allow ? Activation.AllowEdit : Activation.Disabled;
                e.Row.Cells["Report"].Activation = Activation.NoEdit;
                e.Row.Cells["SubscriptionID"].Activation = Activation.NoEdit;
                e.Row.Cells["Description"].Activation = Activation.NoEdit;
                e.Row.Cells["EventType"].Activation = Activation.NoEdit;
                e.Row.Cells["LastRun"].Activation = Activation.NoEdit;
                e.Row.Cells["Status"].Activation = Activation.NoEdit;
                e.Row.Cells["Active"].Activation = Activation.NoEdit;
                e.Row.Cells["DeliverySettings"].Activation = Activation.NoEdit;
                e.Row.Cells["MatchData"].Activation = Activation.NoEdit;
                e.Row.Cells["Parameters"].Activation = Activation.NoEdit;
                e.Row.Cells["Subject"].Activation = Activation.NoEdit;
                e.Row.Cells["SortCenterID"].Activation = Activation.NoEdit;
                e.Row.Cells["ScheduleDate"].Activation = Activation.NoEdit;
                e.Row.Cells["CarrierAgentID"].Activation = Activation.NoEdit;
                e.Row.Cells["TerminalID"].Activation = Activation.NoEdit;
            }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event for all grids
			try {
				//Ensure focus when user mouses (embedded child objects sometimes hold focus)
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				
				//Determine grid element pointed to by the mouse
				UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(uiElement != null) {
					//Determine if user selected a grid row
					object context = uiElement.GetContext(typeof(UltraGridRow));
					if(context != null) {
						//Row was selected- if mouse button is:
						// left: forward to mouse move event handler
						//right: clear all (multi-)selected rows and select a single row
						if(e.Button == MouseButtons.Left) {
						}
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow row = (UltraGridRow)context;
							if(!row.Selected) grid.Selected.Rows.Clear();
							row.Selected = true;
						}
					}
					else {
						//Deselect rows in the white space of the grid or deactivate the active   
						//row when in a scroll region to prevent double-click action
						if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement))
							grid.Selected.Rows.Clear();
						else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
							if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
					}
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			//finally { setUserServices(); }
		}
        private void OnGridBeforeCellActivate(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for data entry cell activating
            try {
                //Enable\disable cell editing
                switch(e.Cell.Column.Key.ToString()) {
                    //Allow send change if applicable subscription
                    case "Send":
                        bool allow=false;
                        string terminalID = e.Cell.Row.Cells["TerminalID"].Value.ToString().Trim();
                        if(terminalID.Length > 0) {
                            //Carrier subscriptions
                            string carrierID = e.Cell.Row.Cells["CarrierAgentID"].Value.ToString().Trim();
                            allow = (terminalID == this.mSchedule.SortCenterID.ToString() && (carrierID.Length > 0 && this.mSchedule.Trips.ShipScheduleTable.Select("carrierID=" + carrierID).Length > 0));
                        }
                        else {
                            //Agent subscriptions
                            string agentNumber = e.Cell.Row.Cells["CarrierAgentID"].Value.ToString().Trim();
                            allow = ((agentNumber.Length > 0) && (this.mSchedule.Trips.ShipScheduleTable.Select("AgentNumber='" + agentNumber + "'").Length > 0) || this.mSchedule.Trips.ShipScheduleTable.Select("S2AgentNumber='" + agentNumber + "'").Length > 0);
                        }
                        e.Cell.Activation = allow ? Activation.AllowEdit : Activation.Disabled;
                        break;
                }
                e.Cell.Selected = true;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridCellChange(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a data entry cell value
            try {
                //
                //if(e.Cell.Row.Cells["Application"].Value.ToString() == "")
                //    e.Cell.Row.Cells["Application"].Value = this.mTsortApp.DBConfiguration.ProductName;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion
        #region Local Services: refresh(), sendReport()
        private void refresh() {
            //Refresh subscription status
            try {
                //Create reporting service web client proxy
                ReportingService2010 rs = new ReportingService2010();
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Request all subscriptions for the specified report
                this.mSubscriptionDS.Clear();
                Subscription[] subscriptions = rs.ListSubscriptions(this.mReportName);
                if(subscriptions != null) {
                    //Enumerate all subscriptions
                    for(int i=0;i<subscriptions.Length;i++) {
                        //Get subscription properties
                        Subscription sub = subscriptions[i];
                        ExtensionSettings extSettings=null;
                        ActiveState active=null;
                        string desc="",status="",eventType="",matchData="";
                        ParameterValue[] paramValues=null;
                        rs.GetSubscriptionProperties(subscriptions[i].SubscriptionID,out extSettings,out desc,out active,out status,out eventType,out matchData,out paramValues);

                        //Determine if this subscription is of the selected report type (i.e. Carrier or Agent)
                        if (paramValues != null && sub.Path == global::Argix.Properties.Settings.Default.CarrierReportPath) {
                            //Carrier:  Select subscription if the carrier has a uncancelled load on the ship schedule
                            //          and the subscription has TerminalID = schedules' SortCenterID
                            string terminalID = getParamValue(paramValues,PARAM_TERMINALID);
                            string carrierID = getParamValue(paramValues,PARAM_CARRIERID);
                            if(terminalID != null && terminalID == this.mSchedule.SortCenterID.ToString() && carrierID != null && carrierID.Length > 0) {
                                bool openExists=false;
                                ShipScheduleDS.ShipScheduleTableRow[] rows = (ShipScheduleDS.ShipScheduleTableRow[])this.mSchedule.Trips.ShipScheduleTable.Select("carrierID=" + carrierID);
                                for(int k=0;k<rows.Length;k++) { if(rows[k].IsCanceledNull()) openExists = true; }
                                this.mSubscriptionDS.SubscriptionTable.AddSubscriptionTableRow(openExists,sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings),"","",carrierID,terminalID);
                            }
                            else
                                this.mSubscriptionDS.SubscriptionTable.AddSubscriptionTableRow(false,sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings),"","",carrierID,terminalID);
                        }
                        else if (paramValues != null && subscriptions[i].Path == global::Argix.Properties.Settings.Default.AgentReportPath) {
                            //Agent: subscriptions are not filtered by Sort Center ID (i.e. report TerminalID parameter)
                            string agentNumber = getParamValue(paramValues,PARAM_AGENTID);
                            if(agentNumber != null && agentNumber.Length > 0) {
                                bool openExists=false;
                                ShipScheduleDS.ShipScheduleTableRow[] rows = (ShipScheduleDS.ShipScheduleTableRow[])this.mSchedule.Trips.ShipScheduleTable.Select("AgentNumber='" + agentNumber + "' OR S2AgentNumber='" + agentNumber + "'");
                                for(int k=0;k<rows.Length;k++) { if(rows[k].IsCanceledNull()) openExists = true; }
                                this.mSubscriptionDS.SubscriptionTable.AddSubscriptionTableRow(openExists,sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings),"","",agentNumber,"");
                            }
                            else
                                this.mSubscriptionDS.SubscriptionTable.AddSubscriptionTableRow(false,sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings),"","",agentNumber,"");
                        }
                    }
                    this.mSubscriptionDS.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh ship schedule subscriptions.",ex); }
        }
        private void sendReport() {
            //
            bool subscriptionsExist=false;
            try {
                //Create reporting service web client proxy
                ReportingService2010 rs = new ReportingService2010();
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Request all subscriptions for the specified report
                Subscription[] subscriptions = rs.ListSubscriptions(this.mReportName);
                if(subscriptions != null) {
                    //Enumerate all subscriptions
                    for(int i=0;i<subscriptions.Length;i++) {
                        //Get subscription properties
                        Subscription sub = subscriptions[i];
                        ExtensionSettings extSettings=null;
                        ActiveState active=null;
                        string desc="",status="",eventType="",matchData="";
                        ParameterValue[] paramValues=null;
                        rs.GetSubscriptionProperties(sub.SubscriptionID,out extSettings,out desc,out active,out status,out eventType,out matchData,out paramValues);

                        //Update subscription to "TimedSubscription" and a future date so it does not run on a snapshot update
                        rs.SetSubscriptionProperties(sub.SubscriptionID,extSettings,desc,"TimedSubscription",setMatchData(),paramValues);

                        //Setup applicable subscriptions for snapshot update
                        if (paramValues != null && sub.Path == global::Argix.Properties.Settings.Default.CarrierReportPath) {
                            //Carrier:  Select subscription if the carrier has a uncancelled load on the ship schedule
                            //          and the subscription has TerminalID = schedules' SortCenterID
                            string terminalID = getParamValue(paramValues,PARAM_TERMINALID);
                            string carrierID = getParamValue(paramValues,PARAM_CARRIERID);
                            if(terminalID == this.mSchedule.SortCenterID.ToString() && carrierID != null && this.mSchedule.Trips.ShipScheduleTable.Select("carrierID=" + carrierID).Length > 0) {
                                //Determine if the subscription is still checked for send (no user override)
                                SubscriptionDS.SubscriptionTableRow[] rows = (SubscriptionDS.SubscriptionTableRow[])this.mSubscriptionDS.SubscriptionTable.Select("SubscriptionID='" + sub.SubscriptionID + "'");
                                if(rows.Length > 0 && rows[0].Send == true) {
                                    //Set subject to reflect Sort Center and schedule date; update subscription to run on a snapshot update
                                    setSubjectLine(extSettings,this.mSchedule.SortCenter.Trim() + " " + this.mSchedule.ScheduleDate.ToString("MM-dd"));
                                    rs.SetSubscriptionProperties(sub.SubscriptionID,extSettings,desc,"SnapshotUpdated",null,paramValues);
                                    if(!subscriptionsExist) subscriptionsExist = true;
                                }
                            }
                        }
                        else if (paramValues != null && subscriptions[i].Path == global::Argix.Properties.Settings.Default.AgentReportPath) {
                            //Agent: Select subscription if the agent has an uncancelled load on the ship schedule
                            string agentNumber = getParamValue(paramValues,PARAM_AGENTID);
                            if((agentNumber != null) && (this.mSchedule.Trips.ShipScheduleTable.Select("AgentNumber=" + agentNumber).Length > 0 || this.mSchedule.Trips.ShipScheduleTable.Select("S2AgentNumber=" + agentNumber).Length > 0)) {
                                //Determine if the subscription is still checked for send (no user override)
                                SubscriptionDS.SubscriptionTableRow[] rows = (SubscriptionDS.SubscriptionTableRow[])this.mSubscriptionDS.SubscriptionTable.Select("SubscriptionID='" + sub.SubscriptionID + "'");
                                if(rows.Length > 0 && rows[0].Send == true) {
                                    //Set subject to reflect Sort Center and schedule date; update subscription to run on a snapshot update
                                    setSubjectLine(extSettings,this.mSchedule.SortCenter.Trim() + " " + this.mSchedule.ScheduleDate.ToString("MM-dd"));
                                    rs.SetSubscriptionProperties(subscriptions[i].SubscriptionID,extSettings,desc,"SnapshotUpdated",null,paramValues);
                                    if(!subscriptionsExist) subscriptionsExist = true;
                                }
                            }
                        }
                    }

                    if(subscriptionsExist) {
                        //Set report parameter values with a this schedules' SortCenterID and ScheduleDate
                        //NOTE: Other report parameters (i.e. CarrierID,AgnetNumber, TerminalID) are set by each subscription
                        ItemParameter[] reportParams = rs.GetItemParameters(this.mReportName,null,false,null,null);
                        for(int i=0;i<reportParams.Length;i++) {
                            if(reportParams[i].Name == "SortCenterID") reportParams[i].DefaultValues = new string[1] { this.mSchedule.SortCenterID.ToString() };
                            if(reportParams[i].Name == "ScheduleDate") reportParams[i].DefaultValues = new string[1] { this.mSchedule.ScheduleDate.ToString() };
                        }
                        rs.SetItemParameters(this.mReportName,reportParams);

                        //Update the snapshot so that subscriptions will be executed
                        rs.UpdateItemExecutionSnapshot(this.mReportName);
                    }
                    else
                        throw new ApplicationException("No emails will be sent. There are no matching subscriptions for this schedule.");
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to send ship schedule subscriptions.",ex); }
        }
        #endregion
        #region Report Services: getSubjectLine(), setSubjectLine(), getParamValue(), getExtSettings(), getMatchData(), setMatchData(), getParamValues()
        private string getSubjectLine(ExtensionSettings extSettings) {
			//
			string subjectLine="";
			ParameterValueOrFieldReference[] extensionParams = extSettings.ParameterValues;
			if(extensionParams != null) {
				foreach(ParameterValueOrFieldReference extensionParam in extensionParams) {
					if(((ParameterValue)extensionParam).Name.ToLower() == "subject") {
						subjectLine = ((ParameterValue)extensionParam).Value;
						break;
					}
				}
			}
			return subjectLine;
		}
		private void setSubjectLine(ExtensionSettings extSettings, string subject) {
			ParameterValueOrFieldReference[] extensionParams = extSettings.ParameterValues;
			string existingValue;
			int indexOfVariableChar;
			if(extensionParams != null) {
				foreach (ParameterValueOrFieldReference extensionParam in extensionParams) {
					if (((ParameterValue)extensionParam).Name.ToLower() == "subject") {
						existingValue = ((ParameterValue)extensionParam).Value;
						indexOfVariableChar = existingValue.IndexOf("~");
						if (indexOfVariableChar >= 0)
							((ParameterValue)extensionParam).Value = subject + existingValue.Substring(indexOfVariableChar - 1) ;
						else
							((ParameterValue)extensionParam).Value = subject + " ~ " + existingValue;
						break;
					}
				}
			}
		}
		private string getParamValue(ParameterValue[] paramValues, string paramName) {
			//
			string paramValue="";
			for(int i=0; i<paramValues.Length; i++) {
				if(paramValues[i].Name == paramName) {
					paramValue = paramValues[i].Value;
					break;
				}
			}
			return paramValue;
		}
		private string getExtSettings(ExtensionSettings extSettings) {
			//
			string extensions="";
			for(int i=0; i<extSettings.ParameterValues.Length; i++) 
				extensions += ((ParameterValue)extSettings.ParameterValues[i]).Name + ": " + ((ParameterValue)extSettings.ParameterValues[i]).Value + "; ";
			return extensions;
		}
		private string getMatchData(string matchData) {
			//
			//<ScheduleDefinition>
			//	<StartDateTime>2008-08-23T09:00:00-08:00</StartDateTime>
			//	<WeeklyRecurrence>
			//		<WeeksInterval>1</WeeksInterval>
			//		<DaysOfWeek><Monday>true</Monday></DaysOfWeek>
			//	</WeeklyRecurrence>
			//</ScheduleDefinition>
			return matchData;
		}
		private string setMatchData() {
			//Set the schedule in the future so that subscription does not execute. 
			//Alternate way to disable a schedule without losing default parameters.
			//<ScheduleDefinition>
			//	<StartDateTime>2008-08-23T09:00:00-08:00</StartDateTime>
			//	<WeeklyRecurrence>
			//		<WeeksInterval>1</WeeksInterval>
			//		<DaysOfWeek><Monday>true</Monday></DaysOfWeek>
			//	</WeeklyRecurrence>
			//</ScheduleDefinition>
			string startDate = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd") + "T09:00:00-08:00";
			string scheduleXml = @"<ScheduleDefinition>";
			scheduleXml += @"<StartDateTime>" + startDate + "</StartDateTime><WeeklyRecurrence><WeeksInterval>1</WeeksInterval>";
			scheduleXml += @"<DaysOfWeek><Monday>True</Monday></DaysOfWeek>";
			scheduleXml += @"</WeeklyRecurrence></ScheduleDefinition>";
			return scheduleXml;
		}
		private string getParamValues(ParameterValue[] paramValues) {
			//
			string parameters="";
			for(int i=0; i<paramValues.Length; i++) 
				parameters += paramValues[i].Name + ": " + paramValues[i].Value + "; ";
			return parameters;		
		}
		#endregion
	}
}
