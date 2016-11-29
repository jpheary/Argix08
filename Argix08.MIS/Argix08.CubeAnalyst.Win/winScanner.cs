using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Windows;

namespace Argix.MIS {
	//
	public class winScanner : System.Windows.Forms.Form {
		//Members
		private Scanner mScanner=null;
		private UltraGridSvc mStatGridSvc=null;
		private UltraGridSvc mDetailGridSvc=null;
		private UltraGridSvc mLogGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private string mContext="";
		
		public event EventHandler ServiceStatesChanged=null;
		
		#region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdStats;
		private Argix.MIS.ScannerDS mCubeStatsDS;
        private Argix.MIS.ScannerDS mEventLogDS;
		private System.Windows.Forms.TabPage tabDetails;
		private System.Windows.Forms.TabPage tabEvents;
		private Argix.MIS.ScannerDS mCubeDetailsDS;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdDetails;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdEvents;
        private System.Windows.Forms.TabControl tabMain;
		private Infragistics.Win.UltraWinChart.UltraChart chtStats;
        private Argix.MIS.ScannerDS mCubeStatsSummary;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem csRefresh;
        private ToolStripSeparator csSep1;
        private ToolStripMenuItem csCut;
        private ToolStripMenuItem csCopy;
        private ToolStripMenuItem csPaste;
        private TabPage tabSummary;
        private Splitter splitterH;
        private RadioButton rdoBrowse;
        private SearchButton sbSearch;
        private DateTimePicker dtpSearchEnd;
        private DateTimePicker dtpStart;
        private GroupBox grpBrowse;
        private Label lblEnd;
        private Label lblStart;
        private RadioButton rdoSearch;
        private GroupBox grpSearch;
        private Button btnBrowse;
        private DateTimePicker dtpStatsStart;
        private DateTimePicker dtpStatsEnd;
        private DateTimePicker dtpLogEnd;
        private DateTimePicker dtpLogStart;
        private IContainer components;
        #endregion
        
        //Interface
		public winScanner(Scanner scanner) {
			//
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				
				//Bind to scanner and register for scanner events
				this.mScanner = scanner;
				this.mStatGridSvc = new UltraGridSvc(this.grdStats);
				this.mDetailGridSvc = new UltraGridSvc(this.grdDetails);
				this.mLogGridSvc = new UltraGridSvc(this.grdEvents);
				this.mToolTip = new System.Windows.Forms.ToolTip();

                this.tabSummary.Controls.AddRange(new Control[] { this.grdStats, this.splitterH,this.chtStats });
				this.grdStats.Controls.AddRange(new Control[]{this.dtpStatsStart, this.dtpStatsEnd});
				this.dtpStatsEnd.Left = this.grdStats.Width - this.dtpStatsEnd.Width;
                this.dtpStatsStart.Left = this.grdStats.Width - this.dtpStatsEnd.Width - this.dtpStatsStart.Width - 24;

                this.grdEvents.Controls.AddRange(new Control[] { this.dtpLogStart,this.dtpLogEnd });
                this.dtpLogEnd.Left = this.grdEvents.Width - this.dtpLogEnd.Width;
                this.dtpLogStart.Left = this.grdEvents.Width - this.dtpLogEnd.Width - this.dtpLogStart.Width - 24;
            }
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		public string TerminalName { get { return this.mScanner.TerminalName; } }
		public bool CanSaveAs { get { return true; } }
		public void SaveAs(string fileName) { this.mCubeStatsDS.WriteXml(fileName, XmlWriteMode.WriteSchema); }
		public void PageSettings() { UltraGridSvc.PageSettings(); }
		public bool CanPrint { get { return false; } }
		public void Print() { UltraGridSvc.Print(this.grdStats, true); }
        public void PrintPreview() { UltraGridSvc.PrintPreview(this.grdStats); }
        public bool CanCut { get { return this.csCut.Enabled; } }
		public void Cut() { this.csCut.PerformClick(); }
		public bool CanCopy { get { return this.csCopy.Enabled; } }
		public void Copy() { this.csCopy.PerformClick(); }
		public bool CanPaste { get { return this.csPaste.Enabled; } }
		public void Paste() { this.csPaste.PerformClick(); }
		public override void Refresh() { this.csRefresh.PerformClick(); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CubeStatisticsTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DATE", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HOUR", -1, null, 1, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SOURCE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GOOD");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NOTFOUND");
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BADREAD");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BADCUBE");
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OTHER");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.UltraChart.Resources.Appearance.PaintElement paintElement1 = new Infragistics.UltraChart.Resources.Appearance.PaintElement();
            Infragistics.UltraChart.Resources.Appearance.LineChartAppearance lineChartAppearance1 = new Infragistics.UltraChart.Resources.Appearance.LineChartAppearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CubeDetailsTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubeDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Source");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Scan");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LabelSeqNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Result");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Message");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ArgixLogTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Level");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Date");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Source");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Category");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Event");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Computer");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword3");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Message");
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winScanner));
            this.grdStats = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csCut = new System.Windows.Forms.ToolStripMenuItem();
            this.csCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.csPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mCubeStatsDS = new Argix.MIS.ScannerDS();
            this.mEventLogDS = new Argix.MIS.ScannerDS();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.dtpStatsEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStatsStart = new System.Windows.Forms.DateTimePicker();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.chtStats = new Infragistics.Win.UltraWinChart.UltraChart();
            this.mCubeStatsSummary = new Argix.MIS.ScannerDS();
            this.tabDetails = new System.Windows.Forms.TabPage();
            this.grpSearch = new System.Windows.Forms.GroupBox();
            this.sbSearch = new Argix.SearchButton();
            this.grpBrowse = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpSearchEnd = new System.Windows.Forms.DateTimePicker();
            this.rdoSearch = new System.Windows.Forms.RadioButton();
            this.rdoBrowse = new System.Windows.Forms.RadioButton();
            this.grdDetails = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mCubeDetailsDS = new Argix.MIS.ScannerDS();
            this.tabEvents = new System.Windows.Forms.TabPage();
            this.dtpLogEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpLogStart = new System.Windows.Forms.DateTimePicker();
            this.grdEvents = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.grdStats)).BeginInit();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCubeStatsDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEventLogDS)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chtStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCubeStatsSummary)).BeginInit();
            this.tabDetails.SuspendLayout();
            this.grpSearch.SuspendLayout();
            this.grpBrowse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCubeDetailsDS)).BeginInit();
            this.tabEvents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdEvents)).BeginInit();
            this.SuspendLayout();
            // 
            // grdStats
            // 
            this.grdStats.ContextMenuStrip = this.csMain;
            this.grdStats.DataMember = "CubeStatisticsTable";
            this.grdStats.DataSource = this.mCubeStatsDS;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdStats.DisplayLayout.Appearance = appearance5;
            ultraGridColumn1.Format = "MM/dd/yyyy";
            ultraGridColumn1.Header.Caption = "Date";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 96;
            ultraGridColumn2.Header.Caption = "Hour";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 48;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            appearance6.TextHAlignAsString = "Right";
            ultraGridColumn4.CellAppearance = appearance6;
            appearance7.TextHAlignAsString = "Center";
            ultraGridColumn4.Header.Appearance = appearance7;
            ultraGridColumn4.Header.Caption = "Good";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 72;
            appearance8.TextHAlignAsString = "Right";
            ultraGridColumn5.CellAppearance = appearance8;
            appearance9.TextHAlignAsString = "Center";
            ultraGridColumn5.Header.Appearance = appearance9;
            ultraGridColumn5.Header.Caption = "Not Found";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 72;
            appearance10.TextHAlignAsString = "Right";
            ultraGridColumn6.CellAppearance = appearance10;
            appearance11.TextHAlignAsString = "Center";
            ultraGridColumn6.Header.Appearance = appearance11;
            ultraGridColumn6.Header.Caption = "Bad Read";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 72;
            appearance12.TextHAlignAsString = "Right";
            ultraGridColumn7.CellAppearance = appearance12;
            appearance13.TextHAlignAsString = "Center";
            ultraGridColumn7.Header.Appearance = appearance13;
            ultraGridColumn7.Header.Caption = "Bad Cube";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 72;
            appearance14.TextHAlignAsString = "Right";
            ultraGridColumn8.CellAppearance = appearance14;
            appearance15.TextHAlignAsString = "Center";
            ultraGridColumn8.Header.Appearance = appearance15;
            ultraGridColumn8.Header.Caption = "Other";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 72;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8});
            this.grdStats.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance16.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance16.FontData.BoldAsString = "True";
            appearance16.FontData.Name = "Verdana";
            appearance16.FontData.SizeInPoints = 8F;
            appearance16.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance16.TextHAlignAsString = "Left";
            this.grdStats.DisplayLayout.CaptionAppearance = appearance16;
            this.grdStats.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdStats.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdStats.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdStats.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance17.BackColor = System.Drawing.SystemColors.Control;
            appearance17.FontData.BoldAsString = "True";
            appearance17.FontData.Name = "Verdana";
            appearance17.FontData.SizeInPoints = 8F;
            appearance17.TextHAlignAsString = "Left";
            this.grdStats.DisplayLayout.Override.HeaderAppearance = appearance17;
            this.grdStats.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdStats.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance18.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdStats.DisplayLayout.Override.RowAppearance = appearance18;
            this.grdStats.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdStats.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdStats.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdStats.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdStats.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdStats.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStats.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdStats.Location = new System.Drawing.Point(0, 0);
            this.grdStats.Name = "grdStats";
            this.grdStats.Size = new System.Drawing.Size(774, 145);
            this.grdStats.TabIndex = 3;
            this.grdStats.Text = "Cube Statisics";
            this.grdStats.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdStats.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdStats.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdStats.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdStats.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnCubeStatsEntrySelected);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csRefresh,
            this.csSep1,
            this.csCut,
            this.csCopy,
            this.csPaste});
            this.csMain.Name = "contextMenuStrip1";
            this.csMain.Size = new System.Drawing.Size(114, 98);
            // 
            // csRefresh
            // 
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(113, 22);
            this.csRefresh.Text = "&Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(110, 6);
            // 
            // csCut
            // 
            this.csCut.Name = "csCut";
            this.csCut.Size = new System.Drawing.Size(113, 22);
            this.csCut.Text = "Cu&t";
            this.csCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCopy
            // 
            this.csCopy.Name = "csCopy";
            this.csCopy.Size = new System.Drawing.Size(113, 22);
            this.csCopy.Text = "&Copy";
            this.csCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csPaste
            // 
            this.csPaste.Name = "csPaste";
            this.csPaste.Size = new System.Drawing.Size(113, 22);
            this.csPaste.Text = "&Paste";
            this.csPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mCubeStatsDS
            // 
            this.mCubeStatsDS.DataSetName = "ScannerDS";
            this.mCubeStatsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mCubeStatsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mEventLogDS
            // 
            this.mEventLogDS.DataSetName = "ScannerDS";
            this.mEventLogDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mEventLogDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabSummary);
            this.tabMain.Controls.Add(this.tabDetails);
            this.tabMain.Controls.Add(this.tabEvents);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(782, 363);
            this.tabMain.TabIndex = 6;
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.dtpStatsEnd);
            this.tabSummary.Controls.Add(this.dtpStatsStart);
            this.tabSummary.Controls.Add(this.splitterH);
            this.tabSummary.Controls.Add(this.grdStats);
            this.tabSummary.Controls.Add(this.chtStats);
            this.tabSummary.Location = new System.Drawing.Point(4, 4);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Size = new System.Drawing.Size(774, 337);
            this.tabSummary.TabIndex = 2;
            this.tabSummary.Text = "Summary";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // dtpStatsEnd
            // 
            this.dtpStatsEnd.Location = new System.Drawing.Point(531, 0);
            this.dtpStatsEnd.Name = "dtpStatsEnd";
            this.dtpStatsEnd.Size = new System.Drawing.Size(240, 21);
            this.dtpStatsEnd.TabIndex = 8;
            // 
            // dtpStatsStart
            // 
            this.dtpStatsStart.Location = new System.Drawing.Point(267, 0);
            this.dtpStatsStart.Name = "dtpStatsStart";
            this.dtpStatsStart.Size = new System.Drawing.Size(240, 21);
            this.dtpStatsStart.TabIndex = 7;
            // 
            // splitterH
            // 
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 139);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(774, 6);
            this.splitterH.TabIndex = 6;
            this.splitterH.TabStop = false;
            // 
            //			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
            //			'ChartType' must be persisted ahead of any Axes change made in design time.
            //		
            this.chtStats.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.LineChart;
            // 
            // chtStats
            // 
            this.chtStats.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            paintElement1.ElementType = Infragistics.UltraChart.Shared.Styles.PaintElementType.None;
            paintElement1.Fill = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.chtStats.Axis.PE = paintElement1;
            this.chtStats.Axis.X.Extent = 12;
            this.chtStats.Axis.X.Labels.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chtStats.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL:MM-dd>";
            this.chtStats.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.X.Labels.SeriesLabels.FormatString = "";
            this.chtStats.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.chtStats.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.X.LineThickness = 1;
            this.chtStats.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.chtStats.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.X.MajorGridLines.Visible = true;
            this.chtStats.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.chtStats.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.X.MinorGridLines.Visible = false;
            this.chtStats.Axis.X.TickmarkInterval = 7;
            this.chtStats.Axis.X.TickmarkIntervalType = Infragistics.UltraChart.Shared.Styles.AxisIntervalType.Days;
            this.chtStats.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.DataInterval;
            this.chtStats.Axis.X.Visible = true;
            this.chtStats.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.chtStats.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.chtStats.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.chtStats.Axis.X2.Labels.SeriesLabels.FormatString = "";
            this.chtStats.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.chtStats.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
            this.chtStats.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.X2.Labels.Visible = false;
            this.chtStats.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.chtStats.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.X2.MajorGridLines.Visible = true;
            this.chtStats.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.chtStats.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.X2.MinorGridLines.Visible = false;
            this.chtStats.Axis.X2.Visible = false;
            this.chtStats.Axis.Y.Extent = 18;
            this.chtStats.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.chtStats.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.chtStats.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Y.Labels.SeriesLabels.FormatString = "";
            this.chtStats.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.chtStats.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Y.LineThickness = 1;
            this.chtStats.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.chtStats.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Y.MajorGridLines.Visible = true;
            this.chtStats.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.chtStats.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Y.MinorGridLines.Visible = false;
            this.chtStats.Axis.Y.RangeMax = 100;
            this.chtStats.Axis.Y.RangeType = Infragistics.UltraChart.Shared.Styles.AxisRangeType.Custom;
            this.chtStats.Axis.Y.Visible = true;
            this.chtStats.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.chtStats.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            this.chtStats.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Y2.Labels.Visible = false;
            this.chtStats.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.chtStats.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Y2.MajorGridLines.Visible = true;
            this.chtStats.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.chtStats.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Y2.MinorGridLines.Visible = false;
            this.chtStats.Axis.Y2.Visible = false;
            this.chtStats.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.Z.Labels.ItemFormatString = "";
            this.chtStats.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.chtStats.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Z.MajorGridLines.Visible = true;
            this.chtStats.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.chtStats.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Z.MinorGridLines.Visible = false;
            this.chtStats.Axis.Z.Visible = false;
            this.chtStats.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.Z2.Labels.ItemFormatString = "";
            this.chtStats.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.chtStats.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.chtStats.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.chtStats.Axis.Z2.Labels.Visible = false;
            this.chtStats.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.chtStats.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Z2.MajorGridLines.Visible = true;
            this.chtStats.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.chtStats.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.chtStats.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.chtStats.Axis.Z2.MinorGridLines.Visible = false;
            this.chtStats.Axis.Z2.Visible = false;
            this.chtStats.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.chtStats.Border.Raised = true;
            this.chtStats.ColorModel.AlphaLevel = ((byte)(150));
            this.chtStats.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            this.chtStats.ColorModel.Scaling = Infragistics.UltraChart.Shared.Styles.ColorScaling.Random;
            this.chtStats.Data.DataMember = "CubeStatisticsSummaryTable";
            this.chtStats.Data.MaxValue = 100;
            this.chtStats.Data.MinValue = 0;
            this.chtStats.Data.SwapRowsAndColumns = true;
            this.chtStats.DataMember = "CubeStatisticsSummaryTable";
            this.chtStats.DataSource = this.mCubeStatsSummary.CubeStatisticsSummaryTable;
            this.chtStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.chtStats.ForeColor = System.Drawing.SystemColors.ControlText;
            this.chtStats.Legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F);
            this.chtStats.Legend.Margins.Bottom = 3;
            this.chtStats.Legend.Margins.Left = 3;
            this.chtStats.Legend.Margins.Right = 3;
            this.chtStats.Legend.Margins.Top = 3;
            this.chtStats.Legend.SpanPercentage = 13;
            this.chtStats.Legend.Visible = true;
            lineChartAppearance1.Thickness = 1;
            this.chtStats.LineChart = lineChartAppearance1;
            this.chtStats.Location = new System.Drawing.Point(0, 145);
            this.chtStats.Name = "chtStats";
            this.chtStats.Size = new System.Drawing.Size(774, 192);
            this.chtStats.TabIndex = 0;
            this.chtStats.TitleBottom.Visible = false;
            this.chtStats.TitleTop.Extent = 24;
            this.chtStats.TitleTop.Font = new System.Drawing.Font("Verdana", 9.75F);
            this.chtStats.TitleTop.Margins.Bottom = 1;
            this.chtStats.TitleTop.Margins.Left = 1;
            this.chtStats.TitleTop.Margins.Right = 1;
            this.chtStats.TitleTop.Margins.Top = 1;
            this.chtStats.TitleTop.Text = "Summary";
            this.chtStats.Tooltips.FormatString = "<DATA_VALUE:00.#>";
            // 
            // mCubeStatsSummary
            // 
            this.mCubeStatsSummary.DataSetName = "ScannerDS";
            this.mCubeStatsSummary.Locale = new System.Globalization.CultureInfo("en-US");
            this.mCubeStatsSummary.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabDetails
            // 
            this.tabDetails.BackColor = System.Drawing.SystemColors.Control;
            this.tabDetails.Controls.Add(this.grpSearch);
            this.tabDetails.Controls.Add(this.grpBrowse);
            this.tabDetails.Controls.Add(this.rdoSearch);
            this.tabDetails.Controls.Add(this.rdoBrowse);
            this.tabDetails.Controls.Add(this.grdDetails);
            this.tabDetails.Location = new System.Drawing.Point(4, 4);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.Size = new System.Drawing.Size(774, 337);
            this.tabDetails.TabIndex = 0;
            this.tabDetails.Text = "Details";
            // 
            // grpSearch
            // 
            this.grpSearch.Controls.Add(this.sbSearch);
            this.grpSearch.Location = new System.Drawing.Point(466, 35);
            this.grpSearch.Name = "grpSearch";
            this.grpSearch.Size = new System.Drawing.Size(269, 100);
            this.grpSearch.TabIndex = 12;
            this.grpSearch.TabStop = false;
            // 
            // sbSearch
            // 
            this.sbSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sbSearch.Location = new System.Drawing.Point(12, 25);
            this.sbSearch.Name = "sbSearch";
            this.sbSearch.Size = new System.Drawing.Size(240, 27);
            this.sbSearch.TabIndex = 8;
            this.sbSearch.Click += new System.EventHandler(this.OnSearchDetail);
            this.sbSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnSearchButtonKeyUp);
            // 
            // grpBrowse
            // 
            this.grpBrowse.Controls.Add(this.btnBrowse);
            this.grpBrowse.Controls.Add(this.lblEnd);
            this.grpBrowse.Controls.Add(this.lblStart);
            this.grpBrowse.Controls.Add(this.dtpStart);
            this.grpBrowse.Controls.Add(this.dtpSearchEnd);
            this.grpBrowse.Location = new System.Drawing.Point(12, 35);
            this.grpBrowse.Name = "grpBrowse";
            this.grpBrowse.Size = new System.Drawing.Size(432, 100);
            this.grpBrowse.TabIndex = 11;
            this.grpBrowse.TabStop = false;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(344, 60);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(72, 20);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.OnBrowseDetail);
            // 
            // lblEnd
            // 
            this.lblEnd.Location = new System.Drawing.Point(12, 60);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(72, 20);
            this.lblEnd.TabIndex = 9;
            this.lblEnd.Text = "End";
            this.lblEnd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStart
            // 
            this.lblStart.Location = new System.Drawing.Point(12, 24);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(72, 20);
            this.lblStart.TabIndex = 8;
            this.lblStart.Text = "Start";
            this.lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(90, 24);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(240, 21);
            this.dtpStart.TabIndex = 6;
            // 
            // dtpSearchEnd
            // 
            this.dtpSearchEnd.Location = new System.Drawing.Point(90, 60);
            this.dtpSearchEnd.Name = "dtpSearchEnd";
            this.dtpSearchEnd.Size = new System.Drawing.Size(240, 21);
            this.dtpSearchEnd.TabIndex = 7;
            // 
            // rdoSearch
            // 
            this.rdoSearch.Location = new System.Drawing.Point(466, 12);
            this.rdoSearch.Name = "rdoSearch";
            this.rdoSearch.Size = new System.Drawing.Size(192, 20);
            this.rdoSearch.TabIndex = 10;
            this.rdoSearch.Text = "Search  by Label Sequence#";
            this.rdoSearch.UseVisualStyleBackColor = true;
            // 
            // rdoBrowse
            // 
            this.rdoBrowse.Checked = true;
            this.rdoBrowse.Location = new System.Drawing.Point(12, 12);
            this.rdoBrowse.Name = "rdoBrowse";
            this.rdoBrowse.Size = new System.Drawing.Size(192, 20);
            this.rdoBrowse.TabIndex = 9;
            this.rdoBrowse.TabStop = true;
            this.rdoBrowse.Text = "Browse by Date Range";
            this.rdoBrowse.UseVisualStyleBackColor = true;
            this.rdoBrowse.CheckedChanged += new System.EventHandler(this.OnBrowseOrSearch);
            // 
            // grdDetails
            // 
            this.grdDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDetails.ContextMenuStrip = this.csMain;
            this.grdDetails.DataMember = "CubeDetailsTable";
            this.grdDetails.DataSource = this.mCubeDetailsDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdDetails.DisplayLayout.Appearance = appearance1;
            ultraGridColumn9.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn9.Header.VisiblePosition = 0;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn10.Format = "MM-dd-yyyy HH:mm:ss";
            ultraGridColumn10.Header.Caption = "Date";
            ultraGridColumn10.Header.VisiblePosition = 1;
            ultraGridColumn10.Width = 144;
            ultraGridColumn11.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn11.Header.VisiblePosition = 2;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn11.Width = 120;
            ultraGridColumn12.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn12.Header.VisiblePosition = 3;
            ultraGridColumn12.Width = 291;
            ultraGridColumn13.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn13.Header.Caption = "Label Seq#";
            ultraGridColumn13.Header.VisiblePosition = 4;
            ultraGridColumn13.Width = 219;
            ultraGridColumn14.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn14.Header.VisiblePosition = 5;
            ultraGridColumn14.Width = 48;
            ultraGridColumn15.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn15.Header.VisiblePosition = 6;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn16.Header.VisiblePosition = 7;
            ultraGridColumn16.Width = 288;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16});
            this.grdDetails.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdDetails.DisplayLayout.CaptionAppearance = appearance2;
            this.grdDetails.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDetails.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDetails.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdDetails.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDetails.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdDetails.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdDetails.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdDetails.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdDetails.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdDetails.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdDetails.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDetails.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDetails.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDetails.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdDetails.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdDetails.Location = new System.Drawing.Point(-4, 141);
            this.grdDetails.Name = "grdDetails";
            this.grdDetails.Size = new System.Drawing.Size(778, 196);
            this.grdDetails.TabIndex = 5;
            this.grdDetails.Text = "Cube Details";
            this.grdDetails.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdDetails.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnCubeDetailsEntrySelected);
            // 
            // mCubeDetailsDS
            // 
            this.mCubeDetailsDS.DataSetName = "ScannerDS";
            this.mCubeDetailsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mCubeDetailsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabEvents
            // 
            this.tabEvents.Controls.Add(this.dtpLogEnd);
            this.tabEvents.Controls.Add(this.dtpLogStart);
            this.tabEvents.Controls.Add(this.grdEvents);
            this.tabEvents.Location = new System.Drawing.Point(4, 4);
            this.tabEvents.Name = "tabEvents";
            this.tabEvents.Size = new System.Drawing.Size(774, 337);
            this.tabEvents.TabIndex = 1;
            this.tabEvents.Text = "Diagnostics";
            this.tabEvents.UseVisualStyleBackColor = true;
            // 
            // dtpLogEnd
            // 
            this.dtpLogEnd.Location = new System.Drawing.Point(534, 0);
            this.dtpLogEnd.Name = "dtpLogEnd";
            this.dtpLogEnd.Size = new System.Drawing.Size(240, 21);
            this.dtpLogEnd.TabIndex = 116;
            // 
            // dtpLogStart
            // 
            this.dtpLogStart.Location = new System.Drawing.Point(270, 0);
            this.dtpLogStart.Name = "dtpLogStart";
            this.dtpLogStart.Size = new System.Drawing.Size(240, 21);
            this.dtpLogStart.TabIndex = 115;
            // 
            // grdEvents
            // 
            this.grdEvents.ContextMenuStrip = this.csMain;
            this.grdEvents.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdEvents.DataMember = "ArgixLogTable";
            this.grdEvents.DataSource = this.mEventLogDS;
            appearance19.BackColor = System.Drawing.SystemColors.Window;
            appearance19.FontData.Name = "Verdana";
            appearance19.FontData.SizeInPoints = 8F;
            appearance19.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance19.TextHAlignAsString = "Left";
            this.grdEvents.DisplayLayout.Appearance = appearance19;
            ultraGridBand3.AddButtonCaption = "TLViewTable";
            ultraGridColumn17.Header.VisiblePosition = 0;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.VisiblePosition = 2;
            ultraGridColumn18.Width = 120;
            ultraGridColumn19.Header.VisiblePosition = 4;
            ultraGridColumn19.Width = 48;
            ultraGridColumn20.Format = "MM/dd/yyyy HH:mm:ss tt";
            ultraGridColumn20.Header.VisiblePosition = 1;
            ultraGridColumn20.Width = 144;
            ultraGridColumn21.Header.VisiblePosition = 3;
            ultraGridColumn21.Width = 120;
            ultraGridColumn22.Header.VisiblePosition = 11;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn22.Width = 72;
            ultraGridColumn23.Header.VisiblePosition = 12;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn23.Width = 48;
            ultraGridColumn24.Header.VisiblePosition = 10;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn24.Width = 96;
            ultraGridColumn25.Header.VisiblePosition = 9;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn25.Width = 96;
            ultraGridColumn26.Header.VisiblePosition = 6;
            ultraGridColumn26.Width = 96;
            ultraGridColumn27.Header.VisiblePosition = 7;
            ultraGridColumn27.Width = 96;
            ultraGridColumn28.Header.VisiblePosition = 8;
            ultraGridColumn28.Width = 96;
            ultraGridColumn29.Header.VisiblePosition = 5;
            ultraGridColumn29.Width = 287;
            ultraGridBand3.Columns.AddRange(new object[] {
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
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29});
            this.grdEvents.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdEvents.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance20.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance20.FontData.BoldAsString = "True";
            appearance20.FontData.Name = "Verdana";
            appearance20.FontData.SizeInPoints = 8F;
            appearance20.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance20.TextHAlignAsString = "Left";
            this.grdEvents.DisplayLayout.CaptionAppearance = appearance20;
            this.grdEvents.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdEvents.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdEvents.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdEvents.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance21.BackColor = System.Drawing.SystemColors.Control;
            appearance21.FontData.BoldAsString = "True";
            appearance21.FontData.Name = "Verdana";
            appearance21.FontData.SizeInPoints = 8F;
            appearance21.TextHAlignAsString = "Left";
            this.grdEvents.DisplayLayout.Override.HeaderAppearance = appearance21;
            this.grdEvents.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdEvents.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance22.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdEvents.DisplayLayout.Override.RowAppearance = appearance22;
            this.grdEvents.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdEvents.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdEvents.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdEvents.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdEvents.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdEvents.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdEvents.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdEvents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEvents.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdEvents.Location = new System.Drawing.Point(0, 0);
            this.grdEvents.Name = "grdEvents";
            this.grdEvents.Size = new System.Drawing.Size(774, 337);
            this.grdEvents.TabIndex = 114;
            this.grdEvents.Text = "Argix Log";
            this.grdEvents.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdEvents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdEvents.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnEventLogEntrySelected);
            // 
            // winScanner
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(782, 363);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winScanner";
            this.Text = "Cube Scanner";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdStats)).EndInit();
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mCubeStatsDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mEventLogDS)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chtStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCubeStatsSummary)).EndInit();
            this.tabDetails.ResumeLayout(false);
            this.grpSearch.ResumeLayout(false);
            this.grpBrowse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCubeDetailsDS)).EndInit();
            this.tabEvents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdEvents)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load
				this.Visible = true;
				Application.DoEvents();
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
				
				//Display scanner properties
				this.Text = this.mScanner.SourceName;
				this.grdStats.Text = this.mScanner.SourceName + " Statistics";
				this.chtStats.TitleTop.Text = "Summary";
				this.grdDetails.Text = this.mScanner.SourceName + " Details";
				this.grdEvents.Text = this.mScanner.SourceName + " Events";
				
				this.dtpStatsStart.MinDate = this.dtpStatsEnd.MinDate = DateTime.Today.AddDays(-365);
                this.dtpStatsStart.MaxDate = this.dtpStatsEnd.MaxDate = DateTime.Today;
                this.dtpStatsEnd.Value = DateTime.Today;
                this.dtpStatsStart.ValueChanged += new System.EventHandler(this.OnStatsDatesChanged);
                this.dtpStatsEnd.ValueChanged += new System.EventHandler(this.OnStatsDatesChanged);
                this.dtpStatsStart.Value = DateTime.Today.AddDays(-30);

                this.dtpStart.MinDate = this.dtpSearchEnd.MinDate = DateTime.Today.AddDays(-365);
                this.dtpStart.MaxDate = this.dtpSearchEnd.MaxDate = DateTime.Today;
                this.dtpSearchEnd.Value = DateTime.Today;
                this.dtpStart.Value = DateTime.Today.AddDays(-1);
                this.dtpStart.ValueChanged += new System.EventHandler(this.OnStatsDatesChanged);
                this.dtpSearchEnd.ValueChanged += new System.EventHandler(this.OnStatsDatesChanged);

                this.dtpLogStart.MinDate = this.dtpLogEnd.MinDate = DateTime.Today.AddDays(-365);
                this.dtpLogStart.MaxDate = this.dtpLogEnd.MaxDate = DateTime.Today;
                this.dtpLogEnd.Value = DateTime.Today;
                this.dtpLogStart.Value = DateTime.Today.AddDays(-30);
                this.dtpLogStart.ValueChanged += new System.EventHandler(this.OnLogDatesChanged);
                this.dtpLogEnd.ValueChanged += new System.EventHandler(this.OnLogDatesChanged);
               
                OnBrowseOrSearch(null,EventArgs.Empty);
				
				#region Grid initialization
				this.grdStats.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdStats.DisplayLayout.Bands[0].Columns["DATE"].SortIndicator = SortIndicator.Descending;
				this.grdStats.DisplayLayout.Bands[0].Columns["HOUR"].SortIndicator = SortIndicator.Descending;
				this.grdDetails.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdDetails.DisplayLayout.Bands[0].Columns["CubeDate"].SortIndicator = SortIndicator.Descending;
				this.grdEvents.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdEvents.DisplayLayout.Bands[0].Columns["Date"].SortIndicator = SortIndicator.Descending;
				#endregion
                OnFormResize(null,EventArgs.Empty);
			}
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { 
		    //Event handler for form resize event
            this.dtpStatsEnd.Left = this.grdStats.Width - this.dtpStatsEnd.Width;
            this.dtpStatsStart.Left = this.grdStats.Width - this.dtpStatsEnd.Width - this.dtpStatsStart.Width - 24;
            
            this.dtpLogEnd.Left = this.grdEvents.Width - this.dtpLogEnd.Width;
            this.dtpLogStart.Left = this.grdEvents.Width - this.dtpLogEnd.Width - this.dtpLogStart.Width - 24;
        }
        private void OnStatsDatesChanged(object sender,EventArgs e) {
            //Event handler for change in stats dates
			this.Cursor = Cursors.WaitCursor;
			const int DAYSSPREAD=365;
			try {
			    //Validate
                DateTimePicker dtp = (DateTimePicker)sender;
                DateTime dtFrom = this.dtpStatsStart.Value;
                DateTime dtTo = this.dtpStatsEnd.Value;
                if(dtp == this.dtpStatsStart) {
                    //Validate spread- adjust To date as required to validate
                    if(dtFrom.CompareTo(dtTo) > 0)
                        this.dtpStatsEnd.Value = this.dtpStatsStart.Value;
                    else if(dtFrom.CompareTo(dtTo.AddDays(-DAYSSPREAD)) < 0)
                        this.dtpStatsEnd.Value = dtFrom.AddDays(DAYSSPREAD);
                }
                else {
                    //Validate spread- adjust From date as required to validate
                    if(dtTo.CompareTo(dtFrom) < 0)
                        this.dtpStatsStart.Value = this.dtpStatsEnd.Value;
                    else if(dtTo.CompareTo(dtFrom.AddDays(DAYSSPREAD)) > 0)
                        this.dtpStatsStart.Value = dtTo.AddDays(-DAYSSPREAD);
                }
                this.mCubeStatsDS.Clear();
                this.mCubeStatsDS.Merge(this.mScanner.GetCubeStats(this.dtpStatsStart.Value, this.dtpStatsEnd.Value));

                this.mCubeStatsSummary.Clear();
                this.mCubeStatsSummary.Merge(this.mScanner.GetCubeStatsSummary(this.dtpStatsStart.Value,this.dtpStatsEnd.Value));
                this.chtStats.Data.DataBind();
            }
            catch(Exception ex) { App.ReportError(ex, true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnCubeStatsEntrySelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected data entry
			setUserServices();
		}
		private void OnCubeDetailsEntrySelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected data entry
			setUserServices();
		}
        private void OnBrowseOrSearch(object sender,EventArgs e) {
            //Event handler for change in browse/search
            this.grpBrowse.Enabled = this.rdoBrowse.Checked;
            this.grpSearch.Enabled = this.rdoSearch.Checked;
        }
        private void OnSearchDatesChanged(object sender,EventArgs e) {
            //Event handler for change in stats dates
            this.Cursor = Cursors.WaitCursor;
            const int DAYSSPREAD = 7;
            try {
                //Validate
                DateTimePicker dtp = (DateTimePicker)sender;
                DateTime dtFrom = this.dtpStart.Value;
                DateTime dtTo = this.dtpSearchEnd.Value;
                if(dtp == this.dtpStart) {
                    //Validate spread- adjust To date as required to validate
                    if(dtFrom.CompareTo(dtTo) > 0)
                        this.dtpSearchEnd.Value = this.dtpStart.Value;
                    else if(dtFrom.CompareTo(dtTo.AddDays(-DAYSSPREAD)) < 0)
                        this.dtpSearchEnd.Value = dtFrom.AddDays(DAYSSPREAD);
                }
                else {
                    //Validate spread- adjust From date as required to validate
                    if(dtTo.CompareTo(dtFrom) < 0)
                        this.dtpStart.Value = this.dtpSearchEnd.Value;
                    else if(dtTo.CompareTo(dtFrom.AddDays(DAYSSPREAD)) > 0)
                        this.dtpStart.Value = dtTo.AddDays(-DAYSSPREAD);
                }
                this.mCubeStatsDS.Clear();
                this.mCubeStatsDS.Merge(this.mScanner.GetCubeStats(this.dtpStatsStart.Value,this.dtpStatsEnd.Value));

                this.mCubeStatsSummary.Clear();
                this.mCubeStatsSummary.Merge(this.mScanner.GetCubeStatsSummary(this.dtpStatsStart.Value,this.dtpStatsEnd.Value));
                this.chtStats.Data.DataBind();
            }
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnBrowseDetail(object sender,EventArgs e) {
            //Event handler for browsing cube details
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mCubeDetailsDS.Clear();
                this.mCubeDetailsDS.Merge(this.mScanner.GetCubeDetails(this.dtpStart.Value,this.dtpSearchEnd.Value));
            }
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnSearchButtonKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event: search on F3
            this.Cursor = Cursors.WaitCursor;
            if(e.KeyCode == Keys.Enter && this.sbSearch.Text.Length > 0) {
                try {
                    this.mCubeDetailsDS.Clear();
                    this.mCubeDetailsDS.Merge(this.mScanner.GetCubeDetails(this.sbSearch.Text));
                }
                catch(Exception ex) { App.ReportError(ex,true); }
                finally { setUserServices(); this.Cursor = Cursors.Default; }
            }
        }
        private void OnSearchDetail(object sender,EventArgs e) {
            //
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mCubeDetailsDS.Clear();
                this.mCubeDetailsDS.Merge(this.mScanner.GetCubeDetails(this.sbSearch.Text));
            }
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnLogDatesChanged(object sender,EventArgs e) {
            //Event handler for change in log dates
            this.Cursor = Cursors.WaitCursor;
            const int DAYSSPREAD = 365;
            try {
                //Validate
                DateTimePicker dtp = (DateTimePicker)sender;
                DateTime dtFrom = this.dtpLogStart.Value;
                DateTime dtTo = this.dtpLogEnd.Value;
                if(dtp == this.dtpLogStart) {
                    //Validate spread- adjust To date as required to validate
                    if(dtFrom.CompareTo(dtTo) > 0)
                        this.dtpLogEnd.Value = this.dtpLogStart.Value;
                    else if(dtFrom.CompareTo(dtTo.AddDays(-DAYSSPREAD)) < 0)
                        this.dtpLogEnd.Value = dtFrom.AddDays(DAYSSPREAD);
                }
                else {
                    //Validate spread- adjust From date as required to validate
                    if(dtTo.CompareTo(dtFrom) < 0)
                        this.dtpLogStart.Value = this.dtpLogEnd.Value;
                    else if(dtTo.CompareTo(dtFrom.AddDays(DAYSSPREAD)) > 0)
                        this.dtpLogStart.Value = dtTo.AddDays(-DAYSSPREAD);
                }
                this.mEventLogDS.Clear();
                this.mEventLogDS.Merge(this.mScanner.GetEventLog(this.dtpLogStart.Value,this.dtpLogEnd.Value));
            }
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnEventLogEntrySelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for change in selected data entry
            setUserServices();
        }
        #region Grid Support: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridMouseDown()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
			try {
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "Total Ctns");
				e.Layout.Bands[0].Columns["Total Ctns"].DataType = typeof(int);
				e.Layout.Bands[0].Columns["Total Ctns"].Format = "#0";
				e.Layout.Bands[0].Columns["Total Ctns"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["Total Ctns"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Good");
				e.Layout.Bands[0].Columns["%Good"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Good"].Format = "#0";
				e.Layout.Bands[0].Columns["%Good"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Good"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Not Found");
				e.Layout.Bands[0].Columns["%Not Found"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Not Found"].Format = "#0";
				e.Layout.Bands[0].Columns["%Not Found"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Not Found"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Bad Read");
				e.Layout.Bands[0].Columns["%Bad Read"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Bad Read"].Format = "#0";
				e.Layout.Bands[0].Columns["%Bad Read"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Bad Read"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Bad Cube");
				e.Layout.Bands[0].Columns["%Bad Cube"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Bad Cube"].Format = "#0";
				e.Layout.Bands[0].Columns["%Bad Cube"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Bad Cube"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Other");
				e.Layout.Bands[0].Columns["%Other"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Other"].Format = "#0";
				e.Layout.Bands[0].Columns["%Other"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Other"].CellAppearance.TextHAlign = HAlign.Right;
			}
            catch(Exception ex) { App.ReportError(ex,true); }
        }
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//Calculate derived columns
				int totalCartons =	Convert.ToInt32(e.Row.Cells["GOOD"].Value) + Convert.ToInt32(e.Row.Cells["NOTFOUND"].Value) + 
					Convert.ToInt32(e.Row.Cells["BADREAD"].Value) + Convert.ToInt32(e.Row.Cells["BADCUBE"].Value) + 
					Convert.ToInt32(e.Row.Cells["OTHER"].Value);
				e.Row.Cells["Total Ctns"].Value = totalCartons;
				e.Row.Cells["%Good"].Value = (Convert.ToSingle(e.Row.Cells["GOOD"].Value) / totalCartons) * 100;
				e.Row.Cells["%Not Found"].Value = (Convert.ToSingle(e.Row.Cells["NOTFOUND"].Value) / totalCartons) * 100;
				e.Row.Cells["%Bad Read"].Value = (Convert.ToSingle(e.Row.Cells["BADREAD"].Value) / totalCartons) * 100;
				e.Row.Cells["%Bad Cube"].Value = (Convert.ToSingle(e.Row.Cells["BADCUBE"].Value) / totalCartons) * 100;
				e.Row.Cells["%Other"].Value = (Convert.ToSingle(e.Row.Cells["OTHER"].Value) / totalCartons) * 100;
			}
            catch(Exception ex) { App.ReportError(ex,true); }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
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
                            oRow.Activate();
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
            catch(Exception ex) { App.ReportError(ex,false); }
            finally { setUserServices(); }
        }
        #endregion
		#region User services: OnItemClick()
		private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			this.Cursor = Cursors.WaitCursor;
			try  {
                ToolStripItem item = (ToolStripItem)sender;
				switch(item.Name)  {
					case "csRefresh":	
                        switch(this.mContext) {
                            case "grdStats":    
                                OnStatsDatesChanged(null, EventArgs.Empty);
                                break;
                            case "chtStats":
                                OnStatsDatesChanged(null,EventArgs.Empty);
                                break;
                            case "grdEvents":
                                OnLogDatesChanged(null,EventArgs.Empty);
                                break;
                        }
						break;
					case "csCut":			break;
					case "csCopy":		
					    switch(this.mContext) {
						    case "grdDetails":
							    if(this.grdDetails.Selected != null) 
								    Clipboard.SetDataObject(this.grdDetails.Selected.Rows[0].Cells["LabelSeqNumber"].Value.ToString(), false);
							    break;
					    }
						break;
					case "csPaste":			break;
					case "msDelete":		break;
				}
			}
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local services: setUserServices()
		private void setUserServices() {
			//Set user services
			try {
				this.mContext = "";
				if(this.grdStats.Focused)
					this.mContext = "grdStats";
                else if(this.chtStats.Focused)
                    this.mContext = "chtStats";
                else if(this.grdDetails.Focused)
					this.mContext = "grdDetails";
				else if(this.grdEvents.Focused)
					this.mContext = "grdEvents";

                this.csRefresh.Enabled = this.mContext.Length > 0;
                this.csCut.Enabled = false;
                this.csCopy.Enabled = this.grdDetails.Focused && this.grdDetails.Selected.Rows.Count > 0;
                this.csPaste.Enabled = false;
			}
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(this,new EventArgs()); }
		}
		#endregion
    }
}
