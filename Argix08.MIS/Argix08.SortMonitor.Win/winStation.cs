//	File:	winstation.cs
//	Author:	J. Heary
//	Date:	10/28/04
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Argix;
using Tsort.Enterprise;
using Tsort.Printers;
using Tsort.Sort;

namespace Tsort.Tools {
	//
	public class winStation : System.Windows.Forms.Form {
		//Members
        protected StationFreightAssignment mAssignment=null;
		protected SortStation mStation=null;
        private ToolTip mToolTip=null;
		
		private const string MNU_REFRESH = "Refresh";
		#region Controls

        protected System.Windows.Forms.Label lblPrinter;
        private System.Windows.Forms.Label _lblPrinter;
		protected System.Windows.Forms.ContextMenu ctxStation;
        private System.Windows.Forms.MenuItem ctxStationRefresh;
        protected Tsort.Enterprise.ArgixLogDS mLogDS;
        protected Tsort.Freight.OutboundFreightDS mItemsDS;
        private Label _lblStation;
        protected Label lblStation;
        protected TabControl tabDialog;
        protected TabPage tabItems;
        protected DataGrid grdItems;
        protected TabPage tabLog;
        protected ComboBox cboFilter;
        protected DataGrid grdLog;
        protected TabPage tabStats;
        private DataGridTableStyle dataGridTableStyle1;
        private DataGridTextBoxColumn dataGridTextBoxColumn1;
        private DataGridTextBoxColumn dataGridTextBoxColumn4;
        private DataGridTextBoxColumn dataGridTextBoxColumn5;
        private DataGridTextBoxColumn dataGridTextBoxColumn6;
        private DataGridTextBoxColumn dataGridTextBoxColumn7;
        private DataGridTextBoxColumn dataGridTextBoxColumn8;
        private DataGridTextBoxColumn dataGridTextBoxColumn9;
        private Label _lblStart;
        private Label _lblEnd;
        private DateTimePicker dtpStopDate;
        private DateTimePicker dtpStartDate;
        private GroupBox groupBox1;
		
		private System.ComponentModel.Container components = null;		//Required designer variable
		#endregion
		
        public event StatusEventHandler StatusMessage = null;
        public event ErrorEventHandler ErrorMessage = null;
        public event EventHandler ServiceStatesChanged = null;
		
		//Interface
        public winStation() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                #region Set menu identities (used for onclick handlers) 
				this.ctxStationRefresh.Text = MNU_REFRESH;
				#endregion

				this.mToolTip = new ToolTip();
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new winStation instance.", ex); }
        }
        public winStation(StationFreightAssignment assignment): this() { 
            this.mAssignment = assignment;
            this.mStation = assignment.Station;
            this.mStation.LogEventsRefreshed += new EventHandler(OnLogEventsRefreshed);
            this.mStation.ItemsRefreshed += new EventHandler(OnItemsRefreshed);
        }
		public winStation(SortStation station): this() { 
            this.mStation = station; 
            this.mStation.LogEventsRefreshed += new EventHandler(OnLogEventsRefreshed);
            this.mStation.ItemsRefreshed += new EventHandler(OnItemsRefreshed);
        }
        protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
        public override void Refresh() {
			//Request a refresh of all views
            if(this.mStation != null) {
                this.mStation.RefreshLogEvents(this.dtpStartDate.Value,this.dtpStopDate.Value);
                this.mStation.RefreshItems(this.dtpStartDate.Value,this.dtpStopDate.Value);
            }
            refresh();
		}
        public StationFreightAssignment Assignment { get { return this.mAssignment; } }
        public SortStation Station { get { return this.mStation; } }
        public DateTime StartDate { get { return this.dtpStartDate.Value; } }
        public DateTime EndDate { get { return this.dtpStopDate.Value; } }
        
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winStation));
            this.ctxStation = new System.Windows.Forms.ContextMenu();
            this.ctxStationRefresh = new System.Windows.Forms.MenuItem();
            this.lblPrinter = new System.Windows.Forms.Label();
            this._lblPrinter = new System.Windows.Forms.Label();
            this.mItemsDS = new Tsort.Freight.OutboundFreightDS();
            this.mLogDS = new Tsort.Enterprise.ArgixLogDS();
            this._lblStation = new System.Windows.Forms.Label();
            this.lblStation = new System.Windows.Forms.Label();
            this.tabDialog = new System.Windows.Forms.TabControl();
            this.tabItems = new System.Windows.Forms.TabPage();
            this.grdItems = new System.Windows.Forms.DataGrid();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.grdLog = new System.Windows.Forms.DataGrid();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn6 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn7 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn8 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn9 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.tabStats = new System.Windows.Forms.TabPage();
            this._lblStart = new System.Windows.Forms.Label();
            this._lblEnd = new System.Windows.Forms.Label();
            this.dtpStopDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.mItemsDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mLogDS)).BeginInit();
            this.tabDialog.SuspendLayout();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLog)).BeginInit();
            this.SuspendLayout();
            // 
            // ctxStation
            // 
            this.ctxStation.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ctxStationRefresh});
            // 
            // ctxStationRefresh
            // 
            this.ctxStationRefresh.Index = 0;
            this.ctxStationRefresh.Text = "Refresh";
            this.ctxStationRefresh.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // lblPrinter
            // 
            this.lblPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPrinter.Location = new System.Drawing.Point(494,3);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new System.Drawing.Size(168,18);
            this.lblPrinter.TabIndex = 6;
            this.lblPrinter.Text = "Printer";
            this.lblPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblPrinter
            // 
            this._lblPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblPrinter.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblPrinter.Location = new System.Drawing.Point(416,3);
            this._lblPrinter.Name = "_lblPrinter";
            this._lblPrinter.Size = new System.Drawing.Size(72,18);
            this._lblPrinter.TabIndex = 19;
            this._lblPrinter.Text = "Printer: ";
            this._lblPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mItemsDS
            // 
            this.mItemsDS.DataSetName = "OutboundFreightDS";
            this.mItemsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mLogDS
            // 
            this.mLogDS.DataSetName = "ArgixLogDS";
            this.mLogDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mLogDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblStation
            // 
            this._lblStation.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblStation.Location = new System.Drawing.Point(-1,3);
            this._lblStation.Name = "_lblStation";
            this._lblStation.Size = new System.Drawing.Size(72,18);
            this._lblStation.TabIndex = 25;
            this._lblStation.Text = "Station: ";
            this._lblStation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStation
            // 
            this.lblStation.Location = new System.Drawing.Point(77,3);
            this.lblStation.Name = "lblStation";
            this.lblStation.Size = new System.Drawing.Size(192,18);
            this.lblStation.TabIndex = 24;
            this.lblStation.Text = "[station]";
            this.lblStation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabDialog
            // 
            this.tabDialog.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabDialog.Controls.Add(this.tabItems);
            this.tabDialog.Controls.Add(this.tabLog);
            this.tabDialog.Controls.Add(this.tabStats);
            this.tabDialog.Location = new System.Drawing.Point(2,76);
            this.tabDialog.Multiline = true;
            this.tabDialog.Name = "tabDialog";
            this.tabDialog.SelectedIndex = 0;
            this.tabDialog.Size = new System.Drawing.Size(660,272);
            this.tabDialog.TabIndex = 26;
            // 
            // tabItems
            // 
            this.tabItems.Controls.Add(this.grdItems);
            this.tabItems.Location = new System.Drawing.Point(4,4);
            this.tabItems.Name = "tabItems";
            this.tabItems.Padding = new System.Windows.Forms.Padding(3);
            this.tabItems.Size = new System.Drawing.Size(652,246);
            this.tabItems.TabIndex = 0;
            this.tabItems.Text = "Sorted Items";
            this.tabItems.UseVisualStyleBackColor = true;
            // 
            // grdItems
            // 
            this.grdItems.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdItems.CaptionText = "Sorted Items";
            this.grdItems.ContextMenu = this.ctxStation;
            this.grdItems.DataMember = "";
            this.grdItems.DataSource = this.mItemsDS;
            this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdItems.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.grdItems.Location = new System.Drawing.Point(3,3);
            this.grdItems.Name = "grdItems";
            this.grdItems.ReadOnly = true;
            this.grdItems.RowHeadersVisible = false;
            this.grdItems.RowHeaderWidth = 32;
            this.grdItems.Size = new System.Drawing.Size(646,240);
            this.grdItems.TabIndex = 5;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.cboFilter);
            this.tabLog.Controls.Add(this.grdLog);
            this.tabLog.Location = new System.Drawing.Point(4,4);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(652,246);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Event Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // cboFilter
            // 
            this.cboFilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.cboFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFilter.Location = new System.Drawing.Point(457,3);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(192,21);
            this.cboFilter.Sorted = true;
            this.cboFilter.TabIndex = 25;
            // 
            // grdLog
            // 
            this.grdLog.BackgroundColor = System.Drawing.SystemColors.Window;
            this.grdLog.CaptionText = "Trace Log";
            this.grdLog.ContextMenu = this.ctxStation;
            this.grdLog.DataMember = "ArgixLogTable";
            this.grdLog.DataSource = this.mLogDS;
            this.grdLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLog.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.grdLog.Location = new System.Drawing.Point(3,3);
            this.grdLog.Name = "grdLog";
            this.grdLog.PreferredColumnWidth = 72;
            this.grdLog.ReadOnly = true;
            this.grdLog.RowHeadersVisible = false;
            this.grdLog.RowHeaderWidth = 30;
            this.grdLog.Size = new System.Drawing.Size(646,240);
            this.grdLog.TabIndex = 24;
            this.grdLog.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.grdLog;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn6,
            this.dataGridTextBoxColumn7,
            this.dataGridTextBoxColumn8,
            this.dataGridTextBoxColumn9});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "ArgixLogTable";
            this.dataGridTableStyle1.ReadOnly = true;
            this.dataGridTableStyle1.RowHeadersVisible = false;
            this.dataGridTableStyle1.RowHeaderWidth = 30;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "PC";
            this.dataGridTextBoxColumn5.MappingName = "Computer";
            this.dataGridTextBoxColumn5.Width = 24;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "MM/dd/yyyy HH:mm:ss";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Date";
            this.dataGridTextBoxColumn4.MappingName = "Date";
            this.dataGridTextBoxColumn4.Width = 132;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Level";
            this.dataGridTextBoxColumn1.MappingName = "Level";
            this.dataGridTextBoxColumn1.Width = 24;
            // 
            // dataGridTextBoxColumn6
            // 
            this.dataGridTextBoxColumn6.Format = "";
            this.dataGridTextBoxColumn6.FormatInfo = null;
            this.dataGridTextBoxColumn6.HeaderText = "Key1";
            this.dataGridTextBoxColumn6.MappingName = "Keyword1";
            this.dataGridTextBoxColumn6.Width = 48;
            // 
            // dataGridTextBoxColumn7
            // 
            this.dataGridTextBoxColumn7.Format = "";
            this.dataGridTextBoxColumn7.FormatInfo = null;
            this.dataGridTextBoxColumn7.HeaderText = "Key2";
            this.dataGridTextBoxColumn7.MappingName = "Keyword2";
            this.dataGridTextBoxColumn7.Width = 48;
            // 
            // dataGridTextBoxColumn8
            // 
            this.dataGridTextBoxColumn8.Format = "";
            this.dataGridTextBoxColumn8.FormatInfo = null;
            this.dataGridTextBoxColumn8.HeaderText = "Key3";
            this.dataGridTextBoxColumn8.MappingName = "Keyword3";
            this.dataGridTextBoxColumn8.Width = 48;
            // 
            // dataGridTextBoxColumn9
            // 
            this.dataGridTextBoxColumn9.Format = "";
            this.dataGridTextBoxColumn9.FormatInfo = null;
            this.dataGridTextBoxColumn9.HeaderText = "Message";
            this.dataGridTextBoxColumn9.MappingName = "Message";
            this.dataGridTextBoxColumn9.Width = 288;
            // 
            // tabStats
            // 
            this.tabStats.Location = new System.Drawing.Point(4,4);
            this.tabStats.Name = "tabStats";
            this.tabStats.Size = new System.Drawing.Size(652,246);
            this.tabStats.TabIndex = 2;
            this.tabStats.Text = "Statistics";
            this.tabStats.UseVisualStyleBackColor = true;
            // 
            // _lblStart
            // 
            this._lblStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblStart.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblStart.Location = new System.Drawing.Point(416,32);
            this._lblStart.Name = "_lblStart";
            this._lblStart.Size = new System.Drawing.Size(72,18);
            this._lblStart.TabIndex = 28;
            this._lblStart.Text = "Start: ";
            this._lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblEnd
            // 
            this._lblEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblEnd.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblEnd.Location = new System.Drawing.Point(416,55);
            this._lblEnd.Name = "_lblEnd";
            this._lblEnd.Size = new System.Drawing.Size(72,18);
            this._lblEnd.TabIndex = 30;
            this._lblEnd.Text = "End: ";
            this._lblEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStopDate
            // 
            this.dtpStopDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStopDate.CausesValidation = false;
            this.dtpStopDate.CustomFormat = "MM-dd-yyyy HH:mm:ss";
            this.dtpStopDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStopDate.Location = new System.Drawing.Point(494,53);
            this.dtpStopDate.MaxDate = new System.DateTime(2028,12,31,0,0,0,0);
            this.dtpStopDate.MinDate = new System.DateTime(2004,1,1,0,0,0,0);
            this.dtpStopDate.Name = "dtpStopDate";
            this.dtpStopDate.ShowUpDown = true;
            this.dtpStopDate.Size = new System.Drawing.Size(164,21);
            this.dtpStopDate.TabIndex = 128;
            this.dtpStopDate.ValueChanged += new System.EventHandler(this.OnStopDateChanged);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.CausesValidation = false;
            this.dtpStartDate.CustomFormat = "MM-dd-yyyy HH:mm:ss";
            this.dtpStartDate.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(494,29);
            this.dtpStartDate.MaxDate = new System.DateTime(2028,12,31,0,0,0,0);
            this.dtpStartDate.MinDate = new System.DateTime(2004,1,1,0,0,0,0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowUpDown = true;
            this.dtpStartDate.Size = new System.Drawing.Size(164,21);
            this.dtpStartDate.TabIndex = 127;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.OnStartDateChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Location = new System.Drawing.Point(2,24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(660,3);
            this.groupBox1.TabIndex = 129;
            this.groupBox1.TabStop = false;
            // 
            // winStation
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(664,350);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dtpStopDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this._lblEnd);
            this.Controls.Add(this._lblStart);
            this.Controls.Add(this.tabDialog);
            this.Controls.Add(this._lblStation);
            this.Controls.Add(this.lblStation);
            this.Controls.Add(this._lblPrinter);
            this.Controls.Add(this.lblPrinter);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winStation";
            this.Padding = new System.Windows.Forms.Padding(2,0,2,2);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sort Station 000";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mItemsDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mLogDS)).EndInit();
            this.tabDialog.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.tabLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLog)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
				
				//Display station properties
				this.Text = "Sort Station " + this.mStation.TerminalID.ToString() + "-" + this.mStation.Number;
				this.lblStation.Text = this.mStation.Name + " (" + this.mStation.Description + ")";
				this.lblPrinter.Text = this.mStation.PrinterType;

				//Set log event filter entries
				this.cboFilter.Items.Add("All");
                this.cboFilter.SelectedIndex = 0;
				
				//Get initial views
                this.grdLog.DataSource = this.mStation.Log;
                this.grdItems.DataSource = this.mStation.Items;
                this.dtpStartDate.MinDate = DateTime.Today.AddYears(-1);
                this.dtpStopDate.MaxDate = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,23,59,59);
                if(this.mAssignment != null) {
                    //Real time: today
                    this.dtpStartDate.Value = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,0,0,0);
                    this.dtpStopDate.Value = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,23,59,59);
                }
                else {
                    //Historical: yesterday
                    this.dtpStartDate.Value = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,0,0,0).AddDays(-1);
                    this.dtpStopDate.Value = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,23,59,59).AddDays(-1);
                }
			} 
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default;  }
		}
        private void OnStartDateChanged(object sender,System.EventArgs e) {
            //Event handler for change in historical sort date
            this.dtpStopDate.MinDate = this.dtpStartDate.Value;
        }
        private void OnStopDateChanged(object sender,System.EventArgs e) {
            //Event handler for change in historical sort date
            this.dtpStartDate.MaxDate = this.dtpStopDate.Value;
        }
        void OnItemsRefreshed(object sender,EventArgs e) {
            //Event handler for itens refreshed
        }
        void OnLogEventsRefreshed(object sender,EventArgs e) {
            //Event handler fro log events refreshed
        }
        protected virtual void refresh() { }
		#region User Services: OnMenuClick()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			this.Cursor = Cursors.WaitCursor;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_REFRESH:   Refresh(); break;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: setUserServices(), reportError()
		protected void setUserServices() {
			//Set user services
			try {
				//Set main menu and context menu states
				this.ctxStationRefresh.Enabled = true;
                this.dtpStartDate.Enabled = (this.mAssignment == null);
                this.dtpStopDate.Enabled = (this.mAssignment == null);
			}
			catch(Exception ex) { reportError(ex); }
			finally { if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this, new EventArgs()); }
		}
		protected void reportError(Exception ex) { reportError(ex, false, LogLevel.None); }
		protected void reportError(Exception ex, bool displayMessage, LogLevel level) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,displayMessage,level));
		}
        private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this,e); }
        #endregion
	}
}
