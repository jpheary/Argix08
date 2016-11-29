using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Data;
using Argix.Enterprise;
using Argix.Security;
using Argix.Windows;

namespace Argix.AgentLineHaul {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private winSchedule mActiveSchedule=null;
		private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		private bool mIsDragging=false;
		
		private const int KEYSTATE_SHIFT = 5;
		private const int KEYSTATE_CTL = 9;
		#region Controls

        private System.Windows.Forms.ToolBarButton btnEdit;
		private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.Splitter splitterV;
        private System.ComponentModel.IContainer components;
		private Argix.AgentLineHaul.ShipScheduleDS mScheduleDS;
		private System.Windows.Forms.Panel pnlTemplates;
		private System.Windows.Forms.Panel pnlTemplateHeader;
		private System.Windows.Forms.Label lblCloseTemplates;
		private System.Windows.Forms.Label lblTemplateHeader;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdTemplates;
        private System.Windows.Forms.Splitter splitterH;
        private Argix.AgentLineHaul.ShipScheduleDS mTemplateDS;
		private System.Windows.Forms.Panel pnlNav;
		private System.Windows.Forms.TabControl tabNav;
		private System.Windows.Forms.TabPage tabBrowse;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdSchedules;
		private System.Windows.Forms.TabPage tabSearch;
		private System.Windows.Forms.Label _lblDate;
		private System.Windows.Forms.Label _lblSortCenter;
		private System.Windows.Forms.ComboBox cboSortCenter;
		private System.Windows.Forms.MonthCalendar calDate;
		private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Label lblNavHeader;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnExport;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnPrint;
        private ToolStripButton btnPreview;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripButton btnSearch;
        private ToolStripSeparator btnSep4;
        private ToolStripButton btnAdd;
        private ToolStripButton btnCancel;
        private ToolStripSeparator btnSep5;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnFullScreen;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuWin;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewTemplates;
        private ToolStripSeparator mnuViewSep2;
        private ToolStripMenuItem mnuViewFullScreen;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuWinCascade;
        private ToolStripMenuItem mnuWinTileH;
        private ToolStripMenuItem mnuWinTileV;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileExport;
        private ToolStripMenuItem mnuFileEmail;
        private ToolStripMenuItem mnuFileEmailCarriers;
        private ToolStripMenuItem mnuFileEmailAgents;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripSeparator mnuFileSep4;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripSeparator mnuEditSep1;
        private ToolStripMenuItem mnuEditSearch;
        private ToolStripSeparator mnuEditSep2;
        private ToolStripMenuItem mnuEditAdd;
        private ToolStripMenuItem mnuEditCancel;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem ctxRefresh;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem ctxNew;
        private ToolStripMenuItem ctxOpen;
        private ToolStripSeparator ctxSep2;
        private ToolStripMenuItem ctxExport;
        private ContextMenuStrip csTemplates;
        private ToolStripMenuItem ctxAddLoads;
        private ToolStripDropDownButton btnEmail;
        private ToolStripMenuItem btnSendCarriers;
        private ToolStripMenuItem btnSendAgents;
        private MenuStrip msMain;
		
		#endregion
		//Interface
		public frmMain() {
			try {
				InitializeComponent();
				this.Text = "Argix Direct " + Program.TerminalCode + " " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				#region Window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.splitterV.MinExtra = 192;
				this.splitterV.MinSize = 48;
				this.splitterV.Dock = DockStyle.Left;
				this.pnlNav.Dock = DockStyle.Left;
				this.splitterH.MinExtra = 96;
				this.splitterH.MinSize = 48;
				this.splitterH.Dock = DockStyle.Bottom;
				this.pnlTemplates.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.splitterH,this.pnlTemplates,this.splitterV,this.pnlNav,this.tsMain,this.stbMain,this.msMain });
				#endregion
				
				//Create data and UI services
				this.mGridSvc = new UltraGridSvc(this.grdSchedules);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 3000);
				configApplication();
			}
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TemplateViewTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayOfTheWeek");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDeparture");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateLastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateRowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleViewTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mScheduleDS = new Argix.AgentLineHaul.ShipScheduleDS();
            this.btnEdit = new System.Windows.Forms.ToolBarButton();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.splitterV = new System.Windows.Forms.Splitter();
            this.pnlTemplates = new System.Windows.Forms.Panel();
            this.grdTemplates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csTemplates = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxAddLoads = new System.Windows.Forms.ToolStripMenuItem();
            this.mTemplateDS = new Argix.AgentLineHaul.ShipScheduleDS();
            this.pnlTemplateHeader = new System.Windows.Forms.Panel();
            this.lblCloseTemplates = new System.Windows.Forms.Label();
            this.lblTemplateHeader = new System.Windows.Forms.Label();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.lblNavHeader = new System.Windows.Forms.Label();
            this.tabNav = new System.Windows.Forms.TabControl();
            this.tabBrowse = new System.Windows.Forms.TabPage();
            this.grdSchedules = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this._lblDate = new System.Windows.Forms.Label();
            this._lblSortCenter = new System.Windows.Forms.Label();
            this.cboSortCenter = new System.Windows.Forms.ComboBox();
            this.calDate = new System.Windows.Forms.MonthCalendar();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnEmail = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnSendCarriers = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSendAgents = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdd = new System.Windows.Forms.ToolStripButton();
            this.btnCancel = new System.Windows.Forms.ToolStripButton();
            this.btnSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnFullScreen = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileEmailCarriers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileEmailAgents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinTileH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinTileV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.mScheduleDS)).BeginInit();
            this.pnlTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).BeginInit();
            this.csTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTemplateDS)).BeginInit();
            this.pnlTemplateHeader.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.tabNav.SuspendLayout();
            this.tabBrowse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedules)).BeginInit();
            this.csMain.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mScheduleDS
            // 
            this.mScheduleDS.DataSetName = "ShipScheduleDS";
            this.mScheduleDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mScheduleDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnEdit
            // 
            this.btnEdit.Name = "btnEdit";
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,353);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(666,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 1;
            this.stbMain.TerminalText = "Terminal";
            // 
            // splitterV
            // 
            this.splitterV.Location = new System.Drawing.Point(258,49);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(3,304);
            this.splitterV.TabIndex = 2;
            this.splitterV.TabStop = false;
            // 
            // pnlTemplates
            // 
            this.pnlTemplates.Controls.Add(this.grdTemplates);
            this.pnlTemplates.Controls.Add(this.pnlTemplateHeader);
            this.pnlTemplates.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTemplates.Location = new System.Drawing.Point(261,233);
            this.pnlTemplates.Name = "pnlTemplates";
            this.pnlTemplates.Size = new System.Drawing.Size(405,120);
            this.pnlTemplates.TabIndex = 108;
            // 
            // grdTemplates
            // 
            this.grdTemplates.ContextMenuStrip = this.csTemplates;
            this.grdTemplates.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdTemplates.DataMember = "TemplateViewTable";
            this.grdTemplates.DataSource = this.mTemplateDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "TLViewTable";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "Sort Center";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 120;
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.Caption = "Day";
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Width = 48;
            ultraGridColumn5.Header.Caption = "Zone";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Width = 48;
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn6.Width = 48;
            ultraGridColumn7.Header.Caption = "Agent#";
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.Width = 48;
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn10.Width = 144;
            ultraGridColumn11.Format = "MM/dd/yy HH:mm";
            ultraGridColumn11.Header.Caption = "Close Date";
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.Width = 120;
            ultraGridColumn12.Format = "MM/dd/yy HH:mm";
            ultraGridColumn12.Header.Caption = "Depart Date";
            ultraGridColumn12.Header.VisiblePosition = 12;
            ultraGridColumn12.Width = 120;
            ultraGridColumn13.Header.VisiblePosition = 13;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.Caption = "Stop#";
            ultraGridColumn14.Header.VisiblePosition = 14;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Format = "MM/dd/yy HH:mm";
            ultraGridColumn15.Header.Caption = "Arrival Date";
            ultraGridColumn15.Header.VisiblePosition = 15;
            ultraGridColumn15.Width = 120;
            ultraGridColumn16.Format = "MM/dd/yy HH:mm";
            ultraGridColumn16.Header.Caption = "OFD1 Date";
            ultraGridColumn16.Header.VisiblePosition = 16;
            ultraGridColumn16.Width = 120;
            ultraGridColumn17.Header.VisiblePosition = 17;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.Caption = "Man?";
            ultraGridColumn18.Header.VisiblePosition = 18;
            ultraGridColumn18.Width = 24;
            ultraGridColumn19.Header.Caption = "Act?";
            ultraGridColumn19.Header.VisiblePosition = 19;
            ultraGridColumn19.Width = 24;
            ultraGridColumn20.Header.Caption = "S2 Zone";
            ultraGridColumn20.Header.VisiblePosition = 20;
            ultraGridColumn20.Width = 48;
            ultraGridColumn21.Header.Caption = "S2 Tag";
            ultraGridColumn21.Header.VisiblePosition = 21;
            ultraGridColumn21.Width = 48;
            ultraGridColumn22.Header.Caption = "S2 Agent#";
            ultraGridColumn22.Header.VisiblePosition = 22;
            ultraGridColumn22.Width = 48;
            ultraGridColumn23.Header.VisiblePosition = 23;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 24;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.Caption = "S2 Stop#";
            ultraGridColumn25.Header.VisiblePosition = 25;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Format = "MM/dd/yy HH:mm";
            ultraGridColumn26.Header.Caption = "S2 Arrival Date";
            ultraGridColumn26.Header.VisiblePosition = 26;
            ultraGridColumn26.Width = 120;
            ultraGridColumn27.Format = "MM/dd/yy HH:mm";
            ultraGridColumn27.Header.Caption = "S2 OFD1 Date";
            ultraGridColumn27.Header.VisiblePosition = 27;
            ultraGridColumn27.Width = 120;
            ultraGridColumn28.Header.Caption = "S2 Notes";
            ultraGridColumn28.Header.VisiblePosition = 28;
            ultraGridColumn28.Width = 96;
            ultraGridColumn29.Header.VisiblePosition = 29;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.Header.VisiblePosition = 30;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 31;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 32;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 33;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.Header.VisiblePosition = 34;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.Header.VisiblePosition = 35;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.Header.VisiblePosition = 36;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 37;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.Caption = "";
            ultraGridColumn38.Header.VisiblePosition = 0;
            ultraGridColumn38.Width = 24;
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
            ultraGridColumn38});
            this.grdTemplates.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTemplates.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.CaptionAppearance = appearance2;
            this.grdTemplates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTemplates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdTemplates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTemplates.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTemplates.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdTemplates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdTemplates.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdTemplates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTemplates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTemplates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTemplates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTemplates.Location = new System.Drawing.Point(0,24);
            this.grdTemplates.Name = "grdTemplates";
            this.grdTemplates.Size = new System.Drawing.Size(405,96);
            this.grdTemplates.TabIndex = 119;
            this.grdTemplates.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdTemplates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseDown);
            this.grdTemplates.Enter += new System.EventHandler(this.OnEnterTemplates);
            this.grdTemplates.Leave += new System.EventHandler(this.OnLeaveTemplates);
            this.grdTemplates.SelectionDrag += new System.ComponentModel.CancelEventHandler(this.OnSelectionDrag);
            this.grdTemplates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            this.grdTemplates.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.OnQueryContinueDrag);
            this.grdTemplates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTemplateSelected);
            this.grdTemplates.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            // 
            // csTemplates
            // 
            this.csTemplates.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxAddLoads});
            this.csTemplates.Name = "ctxTemplates";
            this.csTemplates.Size = new System.Drawing.Size(131,26);
            // 
            // ctxAddLoads
            // 
            this.ctxAddLoads.Image = global::Argix.Properties.Resources.AddTable;
            this.ctxAddLoads.Name = "ctxAddLoads";
            this.ctxAddLoads.Size = new System.Drawing.Size(130,22);
            this.ctxAddLoads.Text = "Add Loads";
            this.ctxAddLoads.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mTemplateDS
            // 
            this.mTemplateDS.DataSetName = "TemplateDS";
            this.mTemplateDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mTemplateDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pnlTemplateHeader
            // 
            this.pnlTemplateHeader.Controls.Add(this.lblCloseTemplates);
            this.pnlTemplateHeader.Controls.Add(this.lblTemplateHeader);
            this.pnlTemplateHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateHeader.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.pnlTemplateHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlTemplateHeader.Location = new System.Drawing.Point(0,0);
            this.pnlTemplateHeader.Name = "pnlTemplateHeader";
            this.pnlTemplateHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTemplateHeader.Size = new System.Drawing.Size(405,24);
            this.pnlTemplateHeader.TabIndex = 118;
            this.pnlTemplateHeader.Leave += new System.EventHandler(this.OnLeaveTemplates);
            this.pnlTemplateHeader.Enter += new System.EventHandler(this.OnEnterTemplates);
            // 
            // lblCloseTemplates
            // 
            this.lblCloseTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseTemplates.BackColor = System.Drawing.SystemColors.Control;
            this.lblCloseTemplates.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblCloseTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCloseTemplates.Location = new System.Drawing.Point(384,4);
            this.lblCloseTemplates.Name = "lblCloseTemplates";
            this.lblCloseTemplates.Size = new System.Drawing.Size(16,16);
            this.lblCloseTemplates.TabIndex = 115;
            this.lblCloseTemplates.Text = "X";
            this.lblCloseTemplates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseTemplates.Click += new System.EventHandler(this.OnCloseTemplates);
            this.lblCloseTemplates.Leave += new System.EventHandler(this.OnLeaveTemplates);
            this.lblCloseTemplates.Enter += new System.EventHandler(this.OnEnterTemplates);
            // 
            // lblTemplateHeader
            // 
            this.lblTemplateHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblTemplateHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTemplateHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTemplateHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTemplateHeader.Location = new System.Drawing.Point(3,3);
            this.lblTemplateHeader.Name = "lblTemplateHeader";
            this.lblTemplateHeader.Size = new System.Drawing.Size(399,18);
            this.lblTemplateHeader.TabIndex = 113;
            this.lblTemplateHeader.Text = "Template Loads";
            this.lblTemplateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTemplateHeader.Leave += new System.EventHandler(this.OnLeaveTemplates);
            this.lblTemplateHeader.Enter += new System.EventHandler(this.OnEnterTemplates);
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(261,230);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(405,3);
            this.splitterH.TabIndex = 109;
            this.splitterH.TabStop = false;
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.lblNavHeader);
            this.pnlNav.Controls.Add(this.tabNav);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0,49);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Padding = new System.Windows.Forms.Padding(3);
            this.pnlNav.Size = new System.Drawing.Size(258,304);
            this.pnlNav.TabIndex = 113;
            // 
            // lblNavHeader
            // 
            this.lblNavHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblNavHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNavHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNavHeader.Location = new System.Drawing.Point(3,3);
            this.lblNavHeader.Name = "lblNavHeader";
            this.lblNavHeader.Size = new System.Drawing.Size(252,18);
            this.lblNavHeader.TabIndex = 114;
            this.lblNavHeader.Text = "Ship Schedules";
            this.lblNavHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabNav
            // 
            this.tabNav.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabNav.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabNav.Controls.Add(this.tabBrowse);
            this.tabNav.Controls.Add(this.tabSearch);
            this.tabNav.Location = new System.Drawing.Point(3,24);
            this.tabNav.Name = "tabNav";
            this.tabNav.Padding = new System.Drawing.Point(0,0);
            this.tabNav.SelectedIndex = 0;
            this.tabNav.ShowToolTips = true;
            this.tabNav.Size = new System.Drawing.Size(255,275);
            this.tabNav.TabIndex = 112;
            this.tabNav.Leave += new System.EventHandler(this.OnLeaveNav);
            this.tabNav.Enter += new System.EventHandler(this.OnEnterNav);
            // 
            // tabBrowse
            // 
            this.tabBrowse.Controls.Add(this.grdSchedules);
            this.tabBrowse.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabBrowse.Location = new System.Drawing.Point(4,4);
            this.tabBrowse.Name = "tabBrowse";
            this.tabBrowse.Size = new System.Drawing.Size(247,249);
            this.tabBrowse.TabIndex = 0;
            this.tabBrowse.Text = "Active";
            this.tabBrowse.ToolTipText = "Browse active ship schedules";
            this.tabBrowse.Leave += new System.EventHandler(this.OnLeaveNav);
            this.tabBrowse.Enter += new System.EventHandler(this.OnEnterNav);
            // 
            // grdSchedules
            // 
            this.grdSchedules.ContextMenuStrip = this.csMain;
            this.grdSchedules.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSchedules.DataMember = "ShipScheduleViewTable";
            this.grdSchedules.DataSource = this.mScheduleDS;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdSchedules.DisplayLayout.Appearance = appearance5;
            ultraGridColumn45.Header.VisiblePosition = 0;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn45.Width = 85;
            ultraGridColumn46.Header.VisiblePosition = 1;
            ultraGridColumn46.Hidden = true;
            ultraGridColumn47.Header.Caption = "Sort Center";
            ultraGridColumn47.Header.VisiblePosition = 2;
            ultraGridColumn47.Width = 132;
            ultraGridColumn48.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn48.Format = "MM/dd/yyyy";
            ultraGridColumn48.Header.Caption = "Date";
            ultraGridColumn48.Header.VisiblePosition = 3;
            ultraGridColumn48.Width = 75;
            ultraGridColumn49.Header.VisiblePosition = 4;
            ultraGridColumn49.Hidden = true;
            ultraGridColumn50.Header.VisiblePosition = 5;
            ultraGridColumn50.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50});
            this.grdSchedules.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdSchedules.DisplayLayout.CaptionAppearance = appearance6;
            this.grdSchedules.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSchedules.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdSchedules.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdSchedules.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdSchedules.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSchedules.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdSchedules.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdSchedules.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdSchedules.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSchedules.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSchedules.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSchedules.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSchedules.Font = new System.Drawing.Font("Verdana",9.75F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.grdSchedules.Location = new System.Drawing.Point(0,0);
            this.grdSchedules.Name = "grdSchedules";
            this.grdSchedules.Size = new System.Drawing.Size(247,249);
            this.grdSchedules.TabIndex = 0;
            this.grdSchedules.UseAppStyling = false;
            this.grdSchedules.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdSchedules.Enter += new System.EventHandler(this.OnEnterNav);
            this.grdSchedules.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnGridAfterRowFilterChanged);
            this.grdSchedules.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdSchedules.Leave += new System.EventHandler(this.OnLeaveNav);
            this.grdSchedules.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
            this.grdSchedules.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdSchedules.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdSchedules.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRefresh,
            this.ctxSep1,
            this.ctxNew,
            this.ctxOpen,
            this.ctxSep2,
            this.ctxExport});
            this.csMain.Name = "ctxMain";
            this.csMain.Size = new System.Drawing.Size(155,104);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(154,22);
            this.ctxRefresh.Text = "Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(151,6);
            // 
            // ctxNew
            // 
            this.ctxNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.ctxNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxNew.Name = "ctxNew";
            this.ctxNew.Size = new System.Drawing.Size(154,22);
            this.ctxNew.Text = "New Schedule";
            this.ctxNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxOpen
            // 
            this.ctxOpen.Image = global::Argix.Properties.Resources.Open;
            this.ctxOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxOpen.Name = "ctxOpen";
            this.ctxOpen.Size = new System.Drawing.Size(154,22);
            this.ctxOpen.Text = "Open Schedule";
            this.ctxOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(151,6);
            // 
            // ctxExport
            // 
            this.ctxExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.ctxExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxExport.Name = "ctxExport";
            this.ctxExport.Size = new System.Drawing.Size(154,22);
            this.ctxExport.Text = "Export";
            this.ctxExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this._lblDate);
            this.tabSearch.Controls.Add(this._lblSortCenter);
            this.tabSearch.Controls.Add(this.cboSortCenter);
            this.tabSearch.Controls.Add(this.calDate);
            this.tabSearch.Controls.Add(this.cmdSearch);
            this.tabSearch.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabSearch.Location = new System.Drawing.Point(4,4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(247,249);
            this.tabSearch.TabIndex = 1;
            this.tabSearch.Text = "Archive";
            this.tabSearch.ToolTipText = "Search prior ship schedules";
            this.tabSearch.Visible = false;
            this.tabSearch.Leave += new System.EventHandler(this.OnLeaveNav);
            this.tabSearch.Enter += new System.EventHandler(this.OnEnterNav);
            // 
            // _lblDate
            // 
            this._lblDate.Location = new System.Drawing.Point(6,54);
            this._lblDate.Name = "_lblDate";
            this._lblDate.Size = new System.Drawing.Size(72,19);
            this._lblDate.TabIndex = 10;
            this._lblDate.Text = "Date";
            this._lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblSortCenter
            // 
            this._lblSortCenter.Location = new System.Drawing.Point(6,6);
            this._lblSortCenter.Name = "_lblSortCenter";
            this._lblSortCenter.Size = new System.Drawing.Size(72,18);
            this._lblSortCenter.TabIndex = 9;
            this._lblSortCenter.Text = "Agent";
            this._lblSortCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSortCenter
            // 
            this.cboSortCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSortCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSortCenter.Location = new System.Drawing.Point(6,24);
            this.cboSortCenter.Name = "cboSortCenter";
            this.cboSortCenter.Size = new System.Drawing.Size(227,21);
            this.cboSortCenter.TabIndex = 8;
            this.cboSortCenter.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            this.cboSortCenter.Leave += new System.EventHandler(this.OnLeaveNav);
            this.cboSortCenter.Enter += new System.EventHandler(this.OnEnterNav);
            // 
            // calDate
            // 
            this.calDate.Location = new System.Drawing.Point(6,72);
            this.calDate.MaxSelectionCount = 1;
            this.calDate.Name = "calDate";
            this.calDate.ShowTodayCircle = false;
            this.calDate.TabIndex = 7;
            this.calDate.Leave += new System.EventHandler(this.OnLeaveNav);
            this.calDate.Enter += new System.EventHandler(this.OnEnterNav);
            this.calDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.OnDateSelected);
            // 
            // cmdSearch
            // 
            this.cmdSearch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cmdSearch.Location = new System.Drawing.Point(6,234);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(96,24);
            this.cmdSearch.TabIndex = 6;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.Click += new System.EventHandler(this.OnSearchClick);
            this.cmdSearch.Leave += new System.EventHandler(this.OnLeaveNav);
            this.cmdSearch.Enter += new System.EventHandler(this.OnEnterNav);
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnSep1,
            this.btnExport,
            this.btnEmail,
            this.btnSep2,
            this.btnPrint,
            this.btnPreview,
            this.btnSep3,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnSearch,
            this.btnSep4,
            this.btnAdd,
            this.btnCancel,
            this.btnSep5,
            this.btnRefresh,
            this.btnFullScreen});
            this.tsMain.Location = new System.Drawing.Point(0,24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(666,25);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 115;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23,22);
            this.btnNew.ToolTipText = "New...";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23,22);
            this.btnOpen.ToolTipText = "Open...";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23,22);
            this.btnSave.ToolTipText = "Save";
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6,25);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23,22);
            this.btnExport.ToolTipText = "Export...";
            this.btnExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnEmail
            // 
            this.btnEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSendCarriers,
            this.btnSendAgents});
            this.btnEmail.Image = global::Argix.Properties.Resources.Send;
            this.btnEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(29,22);
            this.btnEmail.Text = "toolStripDropDownButton1";
            this.btnEmail.ToolTipText = "Email ship schedule";
            // 
            // btnSendCarriers
            // 
            this.btnSendCarriers.Name = "btnSendCarriers";
            this.btnSendCarriers.Size = new System.Drawing.Size(114,22);
            this.btnSendCarriers.Text = "Carriers";
            this.btnSendCarriers.ToolTipText = "Email ship schedule to carriers";
            this.btnSendCarriers.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSendAgents
            // 
            this.btnSendAgents.Name = "btnSendAgents";
            this.btnSendAgents.Size = new System.Drawing.Size(114,22);
            this.btnSendAgents.Text = "Agents";
            this.btnSendAgents.ToolTipText = "Email ship schedule to agents";
            this.btnSendAgents.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6,25);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23,22);
            this.btnPrint.ToolTipText = "Print ship schedule...";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPreview
            // 
            this.btnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(23,22);
            this.btnPreview.ToolTipText = "Print preview ship schedule...";
            this.btnPreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6,25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = global::Argix.Properties.Resources.Cut;
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23,22);
            this.btnCut.ToolTipText = "Cut text";
            this.btnCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::Argix.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23,22);
            this.btnCopy.ToolTipText = "Copy text";
            this.btnCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::Argix.Properties.Resources.Paste;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23,22);
            this.btnPaste.ToolTipText = "Paste text";
            this.btnPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Find;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23,22);
            this.btnSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep4
            // 
            this.btnSep4.Name = "btnSep4";
            this.btnSep4.Size = new System.Drawing.Size(6,25);
            // 
            // btnAdd
            // 
            this.btnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAdd.Image = global::Argix.Properties.Resources.AddTable;
            this.btnAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(23,22);
            this.btnAdd.ToolTipText = "Add a new load...";
            this.btnAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnCancel
            // 
            this.btnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancel.Image = global::Argix.Properties.Resources.Delete;
            this.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(23,22);
            this.btnCancel.ToolTipText = "Cancel selected load";
            this.btnCancel.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep5
            // 
            this.btnSep5.Name = "btnSep5";
            this.btnSep5.Size = new System.Drawing.Size(6,25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23,22);
            this.btnRefresh.ToolTipText = "Refresh ship schedule";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnFullScreen
            // 
            this.btnFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFullScreen.Image = global::Argix.Properties.Resources.FullScreen;
            this.btnFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFullScreen.Name = "btnFullScreen";
            this.btnFullScreen.Size = new System.Drawing.Size(23,22);
            this.btnFullScreen.ToolTipText = "Full screen view";
            this.btnFullScreen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuWin,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0,0);
            this.msMain.MdiWindowListItem = this.mnuWin;
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(666,24);
            this.msMain.TabIndex = 117;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSaveAs,
            this.mnuFileSep2,
            this.mnuFileExport,
            this.mnuFileEmail,
            this.mnuFileSep3,
            this.mnuFileSetup,
            this.mnuFilePrint,
            this.mnuFilePreview,
            this.mnuFileSep4,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37,20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuFileNew.Size = new System.Drawing.Size(152,22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152,22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(152,22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(152,22);
            this.mnuFileExport.Text = "&Export...";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileEmail
            // 
            this.mnuFileEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileEmailCarriers,
            this.mnuFileEmailAgents});
            this.mnuFileEmail.Image = global::Argix.Properties.Resources.Send;
            this.mnuFileEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileEmail.Name = "mnuFileEmail";
            this.mnuFileEmail.Size = new System.Drawing.Size(152,22);
            this.mnuFileEmail.Text = "E&mail";
            // 
            // mnuFileEmailCarriers
            // 
            this.mnuFileEmailCarriers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileEmailCarriers.Name = "mnuFileEmailCarriers";
            this.mnuFileEmailCarriers.Size = new System.Drawing.Size(123,22);
            this.mnuFileEmailCarriers.Text = "Carriers...";
            this.mnuFileEmailCarriers.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileEmailAgents
            // 
            this.mnuFileEmailAgents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileEmailAgents.Name = "mnuFileEmailAgents";
            this.mnuFileEmailAgents.Size = new System.Drawing.Size(123,22);
            this.mnuFileEmailAgents.Text = "Agents...";
            this.mnuFileEmailAgents.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(152,22);
            this.mnuFileSetup.Text = "Page Set&up...";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(152,22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(152,22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep4
            // 
            this.mnuFileSep4.Name = "mnuFileSep4";
            this.mnuFileSep4.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152,22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditSep1,
            this.mnuEditSearch,
            this.mnuEditSep2,
            this.mnuEditAdd,
            this.mnuEditCancel});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39,20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(118,22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(118,22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(118,22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(115,6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.Size = new System.Drawing.Size(118,22);
            this.mnuEditSearch.Text = "&Search...";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep2
            // 
            this.mnuEditSep2.Name = "mnuEditSep2";
            this.mnuEditSep2.Size = new System.Drawing.Size(115,6);
            // 
            // mnuEditAdd
            // 
            this.mnuEditAdd.Image = global::Argix.Properties.Resources.AddTable;
            this.mnuEditAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditAdd.Name = "mnuEditAdd";
            this.mnuEditAdd.Size = new System.Drawing.Size(118,22);
            this.mnuEditAdd.Text = "&Add...";
            this.mnuEditAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCancel
            // 
            this.mnuEditCancel.Image = global::Argix.Properties.Resources.Delete;
            this.mnuEditCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCancel.Name = "mnuEditCancel";
            this.mnuEditCancel.Size = new System.Drawing.Size(118,22);
            this.mnuEditCancel.Text = "Ca&ncel";
            this.mnuEditCancel.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewTemplates,
            this.mnuViewSep2,
            this.mnuViewFullScreen,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44,20);
            this.mnuView.Text = "View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(152,22);
            this.mnuViewRefresh.Text = "&Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(149,6);
            // 
            // mnuViewTemplates
            // 
            this.mnuViewTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewTemplates.Name = "mnuViewTemplates";
            this.mnuViewTemplates.Size = new System.Drawing.Size(152,22);
            this.mnuViewTemplates.Text = "&Templates";
            this.mnuViewTemplates.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(149,6);
            // 
            // mnuViewFullScreen
            // 
            this.mnuViewFullScreen.Image = global::Argix.Properties.Resources.FullScreen;
            this.mnuViewFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewFullScreen.Name = "mnuViewFullScreen";
            this.mnuViewFullScreen.Size = new System.Drawing.Size(152,22);
            this.mnuViewFullScreen.Text = "&Full Screen";
            this.mnuViewFullScreen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(152,22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(152,22);
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48,20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157,22);
            this.mnuToolsConfig.Text = "&Configuration...";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuWin
            // 
            this.mnuWin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWinCascade,
            this.mnuWinTileH,
            this.mnuWinTileV});
            this.mnuWin.Name = "mnuWin";
            this.mnuWin.Size = new System.Drawing.Size(63,20);
            this.mnuWin.Text = "Window";
            // 
            // mnuWinCascade
            // 
            this.mnuWinCascade.Image = global::Argix.Properties.Resources.CascadeWindows;
            this.mnuWinCascade.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuWinCascade.Name = "mnuWinCascade";
            this.mnuWinCascade.Size = new System.Drawing.Size(160,22);
            this.mnuWinCascade.Text = "Cascade";
            this.mnuWinCascade.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuWinTileH
            // 
            this.mnuWinTileH.Image = global::Argix.Properties.Resources.ArrangeWindows;
            this.mnuWinTileH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuWinTileH.Name = "mnuWinTileH";
            this.mnuWinTileH.Size = new System.Drawing.Size(160,22);
            this.mnuWinTileH.Text = "Tile Horizontally";
            this.mnuWinTileH.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuWinTileV
            // 
            this.mnuWinTileV.Image = global::Argix.Properties.Resources.ArrangeSideBySide;
            this.mnuWinTileV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuWinTileV.Name = "mnuWinTileV";
            this.mnuWinTileV.Size = new System.Drawing.Size(160,22);
            this.mnuWinTileV.Text = "Tile Vertically";
            this.mnuWinTileV.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44,20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(193,22);
            this.mnuHelpAbout.Text = "&About Ship Schedule...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(190,6);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(666,377);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.pnlTemplates);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.Text = "Argix Direct Ship Schedule";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.mScheduleDS)).EndInit();
            this.pnlTemplates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).EndInit();
            this.csTemplates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTemplateDS)).EndInit();
            this.pnlTemplateHeader.ResumeLayout(false);
            this.pnlNav.ResumeLayout(false);
            this.tabNav.ResumeLayout(false);
            this.tabBrowse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedules)).EndInit();
            this.csMain.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				Splash.Close();
				this.Visible = true;
				Application.DoEvents();
				#region Set user preferences
                try {
                    this.WindowState = global::Argix.Properties.Settings.Default.WindowState;
                    switch(this.WindowState) {
                        case FormWindowState.Maximized: break;
                        case FormWindowState.Minimized: break;
                        case FormWindowState.Normal:
                            this.Location = global::Argix.Properties.Settings.Default.Location;
                            this.Size = global::Argix.Properties.Settings.Default.Size;
                            break;
                    }
                    this.mnuViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    this.pnlTemplates.Visible = this.mnuViewTemplates.Checked = Convert.ToBoolean(global::Argix.Properties.Settings.Default.TemplatesWindow);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
				#endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				//this.mToolTip.SetToolTip(this.cboTerminals, "Select an enterprise terminal for the TL and Agent Summary views.");
				#endregion
				
				//Set control defaults
				#region Grid customizations from normal layout (to support cell editing)
				this.grdSchedules.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdSchedules.DisplayLayout.Bands[0].Columns["ScheduleDate"].SortIndicator = SortIndicator.Ascending;
				this.grdTemplates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdTemplates.DisplayLayout.Bands[0].Columns["MainZone"].SortIndicator = SortIndicator.Ascending;
				#endregion
				if(!AppSecurity.UserCanAddSchedule) this.grdTemplates.DisplayLayout.Bands[0].Override.SelectTypeRow = SelectType.None;
                this.grdSchedules.DataMember = ShipScheduleFactory.TBL_SCHEDULES;
                this.grdSchedules.DataSource = ShipScheduleFactory.Schedules;
                this.cboSortCenter.DisplayMember = "TerminalTable.Description";
                this.cboSortCenter.ValueMember = "TerminalTable.ID";
                this.cboSortCenter.DataSource = EnterpriseFactory.GetTerminals(global::Argix.Properties.ArgixSettings.Default.IsShipperSchedule);
                if (this.cboSortCenter.Items.Count > 0) this.cboSortCenter.SelectedIndex = 0;
                this.calDate.MinDate = DateTime.Today.AddYears(-1);
				this.calDate.MaxDate = DateTime.Today;
				this.cmdSearch.Enabled = false;
                ShipScheduleFactory.RefreshSchedules();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Ask only if there are detail forms open
			if(this.MdiChildren.Length > 0) {
				if(MessageBox.Show("Are you sure you want to close the application.?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
					e.Cancel = true;
			}
            if(!e.Cancel) {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.TemplatesWindow = this.pnlTemplates.Visible;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
		}
        private void OnFormResize(object sender,System.EventArgs e) { 
			//Event handler for form resized event
		}
		private void OnSchedulesChanged(object sender, EventArgs e) {
			//Event handler for change in ship schedule list
			try {
				this.mMessageMgr.AddMessage("Loading ship schedules...");
				OnScheduleActivated(this.mActiveSchedule, EventArgs.Empty);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
		}
		#region Navigation Services: OnValidateForm(), OnDateSelected(), OnSearchClick(), OnEnterNav(), OnLeaveNav()
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in form data- validate OK service
			try { this.cmdSearch.Enabled = (this.cboSortCenter.SelectedIndex >= 0); } catch(Exception) { }
		}
		private void OnDateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			//Event handler for date selected
			OnValidateForm(null, null);
		}
		private void OnSearchClick(object sender, System.EventArgs e) {
			//Event handler for seacrh button clicked
			this.Cursor = Cursors.WaitCursor;
			winSchedule win=null;
			try {
				//Search for a schedule; validate it is not open already
				long sortCenterID = Convert.ToInt64(this.cboSortCenter.SelectedValue);
				string sortCenter = this.cboSortCenter.Text;
				DateTime scheduleDate = calDate.SelectionStart;
				for(int i=0; i< this.MdiChildren.Length; i++) {
					win = (winSchedule)this.MdiChildren[i];
					if(win.Schedule.SortCenterID == sortCenterID && win.Schedule.ScheduleDate == scheduleDate) {
						//Already open; bring to forefront
						win.Activate();
						if(win.WindowState == FormWindowState.Minimized) win.WindowState = FormWindowState.Normal;
						break;
					}
					else
						win = null;
				}
				if(win == null) {
					//Not open; open and register for events
                    ShipSchedule schedule = ShipScheduleFactory.SchedulesArchiveItem(sortCenterID, sortCenter, scheduleDate);
					if(schedule == null) {
						MessageBox.Show(this,"A schedule for this date does not exist.", App.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
					else {
						this.mMessageMgr.AddMessage("Opening " + schedule.SortCenter + " ship schedule for " + schedule.ScheduleDate + "...");
						win = new winSchedule(schedule);
						win.WindowState = (this.MdiChildren.Length > 0) ? this.MdiChildren[0].WindowState : FormWindowState.Maximized;
						win.MdiParent = this;
						win.Activated += new EventHandler(OnScheduleActivated);
						win.Deactivate += new EventHandler(OnScheduleDeactivated);
						win.Closing += new CancelEventHandler(OnScheduleClosing);
						win.Closed += new EventHandler(OnScheduleClosed);
						win.StatusMessage += new StatusEventHandler(OnStatusMessage);
						win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
						win.Show();
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnEnterNav(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			try { 
				this.lblNavHeader.BackColor = SystemColors.ActiveCaption;
				this.lblNavHeader.ForeColor = SystemColors.ActiveCaptionText;
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnLeaveNav(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			try { 
				this.lblNavHeader.BackColor = SystemColors.Control;
				this.lblNavHeader.ForeColor = SystemColors.ControlText;
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
		#region Nav Grid Initialization: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridBeforeRowFilterDropDownPopulate(), OnGridAfterRowFilterChanged()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
			try {
                e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count,"WeekDay");
                e.Layout.Bands[0].Columns["WeekDay"].DataType = typeof(string);
                e.Layout.Bands[0].Columns["WeekDay"].Width = 48;
                e.Layout.Bands[0].Columns["WeekDay"].Header.Caption = "Day";
                e.Layout.Bands[0].Columns["WeekDay"].Header.Appearance.TextHAlign = HAlign.Left;
                e.Layout.Bands[0].Columns["WeekDay"].CellAppearance.TextHAlign = HAlign.Left;
                e.Layout.Bands[0].Columns["WeekDay"].SortIndicator = SortIndicator.None;
			} 
			catch(ArgumentException ex) { App.ReportError(ex, false, LogLevel.None); }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid layout initialization
			try {
                DateTime schDate = Convert.ToDateTime(e.Row.Cells["ScheduleDate"].Value.ToString());
                e.Row.Cells["WeekDay"].Value = schDate.DayOfWeek.ToString().Substring(0,3);
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
			//Removes only (Blanks) and Non Blanks default filter
			try {
				e.ValueList.ValueListItems.Remove(3);
				e.ValueList.ValueListItems.Remove(2);
				e.ValueList.ValueListItems.Remove(1);
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnGridAfterRowFilterChanged(object sender, Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs e) {
			//	
			try {
				OnScheduleActivated(this.mActiveSchedule, EventArgs.Empty);
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		#endregion
		#region Nav Grid Support: GridSelectionChanged(), GridMouseDown(), GridDoubleClick()
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			try { } 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
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
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnGridDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for double-click event
			try {
				//Select grid and forward to update
				UltraGrid grid = (UltraGrid)sender;
				if(grid.ActiveRow != null && grid.Selected.Rows.Count > 0) {
					if(this.mnuFileOpen.Enabled) this.mnuFileOpen.PerformClick();
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		#endregion
		#region Template Grid Drag/Drop Services: OnDragDropMouseDown(), OnDragDropMouseMove(), OnDragDropMouseUp(), OnQueryContinueDrag(), OnSelectionDrag()
		private void OnDragDropMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event for all grids
			try {				
				//Select rows on right click
				UltraGrid oGrid = (UltraGrid)sender;
				UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(oUIElement != null) {
					object oContext = oUIElement.GetContext(typeof(UltraGridRow));
					if(oContext != null) {
						if(e.Button == MouseButtons.Left) {
							this.mIsDragging = true; 
						}
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow oRow = (UltraGridRow)oContext;
							if(!oRow.Selected) oGrid.Selected.Rows.Clear();
							oRow.Selected = true;
						}
					}
					else {
						//Deselect rows in the white space of the grid or deactivate the active   
						//row when in a scroll region to prevent double-click action
						if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
							oGrid.Selected.Rows.Clear();
						else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
							if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Activated = false;
					}
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnDragDropMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Start drag\drop if user is dragging
			DataObject oData=null;
			try {
				switch(e.Button) {
					case MouseButtons.Left:
						UltraGrid oGrid = (UltraGrid)sender;
						if(this.mIsDragging) {
							//Initiate drag drop operation from the grid source
							if(oGrid.Focused && oGrid.Selected.Rows.Count > 0) {
								oData = new DataObject();
								oData.SetData("");
								DragDropEffects effect = oGrid.DoDragDrop(oData, DragDropEffects.All);
								this.mIsDragging = false; 
								
								//After the drop- handled by drop code
								switch(effect) {
									case DragDropEffects.Move:	break;
									case DragDropEffects.Copy:	break;
								}
							}
						}
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnDragDropMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) { 
			this.mIsDragging = false; 
		}
		private void OnQueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e) { 
			//if(!this.mIsDragging) e.Action = DragAction.Cancel; 
		}
		private void OnSelectionDrag(object sender, System.ComponentModel.CancelEventArgs e) { 
			//e.Cancel = !this.mIsDragging; 
		}
		#endregion
        #region User Services: OnItemClick(), OnHelpItemClick(), OnDataStatusUpdate()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
					case "mnuFileNew":
                    case "ctxNew":
                    case "btnNew": 
					    newSchedule(); 
					    break;
					case "mnuFileOpen":
                    case "ctxOpen":
                    case "btnOpen": 
					    openSchedule(); 
					    break;
					case "mnuFileSave":
                    case "btnSave": 
					    break;
					case "mnuFileSaveAs":			
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.Filter = "Export Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
						dlgSave.FilterIndex = 0;
						dlgSave.Title = "Save Schedule As...";
						dlgSave.FileName = this.mActiveSchedule.Schedule.ScheduleID;
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
							Application.DoEvents();
							if(dlgSave.FileName.EndsWith("xls")) {
								new Argix.ExcelFormat().Transform(this.mActiveSchedule.Schedule.ToDataSet(), dlgSave.FileName);
                            }
							else {
								this.mActiveSchedule.Schedule.ToDataSet().WriteXml(dlgSave.FileName, XmlWriteMode.WriteSchema);
							}
						}
						break;
					case "mnuFileExport":
                    case "ctxExport":
                    case "btnExport": 
						this.Cursor = Cursors.WaitCursor;
						DataSet ds = new DataSet();
                        string xfile = global::Argix.Properties.ArgixSettings.Default.ExportDefinitionFile + "ExportDS.xsd";
                        this.mMessageMgr.AddMessage("Reading export definition from " + xfile);
                        ds.ReadXml(xfile,XmlReadMode.Auto);
						if(this.grdSchedules.Focused) {
							this.mMessageMgr.AddMessage("Exporting selected schedules to Microsoft Excel...");
							for(int i=0; i<this.grdSchedules.Selected.Rows.Count; i++) {
								string scheduleID = this.grdSchedules.Selected.Rows[i].Cells["ScheduleID"].Value.ToString();
                                ShipSchedule schedule = ShipScheduleFactory.SchedulesItem(scheduleID);
								ds.Merge(schedule.ToDataSet(), true, MissingSchemaAction.Ignore);
							}
						}
						else if(this.mActiveSchedule != null) {
							this.mMessageMgr.AddMessage("Exporting active schedule to Microsoft Excel...");
							ds.Merge(this.mActiveSchedule.Schedule.ToDataSet(), true, MissingSchemaAction.Ignore);
						}
                        Argix.ExcelFormat oExcel = new Argix.ExcelFormat();
						oExcel.Transform(ds);
						break;
				    case "mnuFileEmailCarriers":        this.mActiveSchedule.EmailCarriers(true); break;
                    case "btnSendCarriers":             this.mActiveSchedule.EmailCarriers(false); break;
                    case "mnuFileEmailAgents":          this.mActiveSchedule.EmailAgents(true); break;
                    case "btnSendAgents":               this.mActiveSchedule.EmailAgents(false); break;
                    case "mnuFileSetup":                UltraGridPrinter.PageSettings(); break;
					case "mnuFilePrint":                this.mActiveSchedule.Print(true); break;
                    case "btnPrint":                    this.mActiveSchedule.Print(false); break;
                    case "mnuFilePreview":
                    case "btnPreview":                  this.mActiveSchedule.PrintPreview(); break;
					case "mnuFileExit":                 this.Close(); Application.Exit(); break;
					case "mnuEditCut":
                    case "btnCut":                      this.mActiveSchedule.Cut();  break;
					case "mnuEditCopy":
                    case "btnCopy":                     this.mActiveSchedule.Copy(); break;
					case "mnuEditPaste":
                    case "btnPaste":                    this.mActiveSchedule.Paste(); break;
					case "mnuEditAdd":
					case "ctxAddLoads": 
                    case "btnAdd":                      this.mActiveSchedule.AddLoads(); break;
					case "mnuEditCancel":
                    case "btnCancel":                   this.mActiveSchedule.CancelLoad(); break;
					case "mnuEditSearch":
                    case "btnSearch":                   this.tabNav.SelectedIndex = 1; this.cboSortCenter.Focus(); break;
					case "mnuViewRefresh":
                    case "ctxRefresh":
                    case "btnRefresh": 
						//Refresh schedule
						if(this.grdSchedules.Focused) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Refreshing schedule list...");
                            ShipScheduleFactory.RefreshSchedules();
						}
						else if(this.mActiveSchedule != null) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Refreshing current schedule...");
							this.mActiveSchedule.Refresh();
						}
						break;
					case "mnuViewFullScreen":
                    case "btnFullScreen":           this.pnlNav.Visible = this.splitterV.Visible = !(this.btnFullScreen.Checked = this.mnuViewFullScreen.Checked = (!this.mnuViewFullScreen.Checked)); break;
					case "mnuViewTemplates":        this.pnlTemplates.Visible = (this.mnuViewTemplates.Checked = !this.mnuViewTemplates.Checked); break;
					case "mnuViewToolbar":			this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case "mnuViewStatusBar":		this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
					case "mnuWinCascade":			this.LayoutMdi(MdiLayout.Cascade); break;
					case "mnuWinTileH":			    this.LayoutMdi(MdiLayout.TileHorizontal); break;
					case "mnuWinTileV":			    this.LayoutMdi(MdiLayout.TileVertical); break;
                    case "mnuToolsConfig":          App.ShowConfig(); break;
					case "mnuHelpAbout":			new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpItemClick(object sender, System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                Help.ShowHelp(this, this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch (Exception) { }
        }
        private void OnDataStatusUpdate(object sender, DataStatusArgs e) {
            //Event handler for notifications from mediator
            this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
        }
        #endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu()
		private void configApplication() {
			try {				
				//Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                ShipScheduleFactory.SchedulesChanged += new EventHandler(OnSchedulesChanged);
            }
			catch(ApplicationException ex) { throw ex; } 
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {
				this.mnuFileNew.Enabled = this.ctxNew.Enabled = this.btnNew.Enabled = (this.grdSchedules.Focused && AppSecurity.UserCanAddSchedule);
				this.mnuFileOpen.Enabled = this.ctxOpen.Enabled = this.btnOpen.Enabled = (this.grdSchedules.Focused && this.grdSchedules.Selected.Rows.Count > 0);
				this.btnSave.Enabled = false;
				this.mnuFileSaveAs.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanSave);
				if(this.grdSchedules.Focused)
					this.mnuFileExport.Enabled = this.ctxExport.Enabled = this.btnExport.Enabled = (this.grdSchedules.Selected.Rows.Count > 0);
				else if(this.mActiveSchedule != null)
					this.mnuFileExport.Enabled = this.ctxExport.Enabled = this.btnExport.Enabled = (this.mActiveSchedule != null);
				this.mnuFileEmailCarriers.Enabled = this.btnSendCarriers.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanEmailCarriers);
				this.mnuFileEmailAgents.Enabled = this.btnSendAgents.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanEmailAgents);
				this.mnuFileSetup.Enabled = true;
				this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanPrint);
				this.mnuFilePreview.Enabled = this.btnPreview.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanPreview);
				this.mnuFileExit.Enabled = true;
				this.mnuEditCut.Enabled = this.btnCut.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanCut);
				this.mnuEditCopy.Enabled = this.btnCopy.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanCopy);
				this.mnuEditPaste.Enabled = this.btnPaste.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanPaste);
				this.mnuEditAdd.Enabled = this.ctxAddLoads.Enabled = this.btnAdd.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanAddLoad && (this.grdTemplates.Selected.Rows.Count > 0));
				this.mnuEditCancel.Enabled = this.btnCancel.Enabled = (this.mActiveSchedule != null && this.mActiveSchedule.CanCancelLoad);
				this.mnuEditCancel.Checked = (this.mActiveSchedule != null && this.mActiveSchedule.IsCancelledLoad);
				this.mnuEditSearch.Enabled = this.btnSearch.Enabled = true;
				this.mnuViewRefresh.Enabled = this.ctxRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewFullScreen.Enabled = this.btnFullScreen.Enabled = (this.MdiChildren.Length > 0);
				this.mnuViewTemplates.Enabled = AppSecurity.UserCanAddSchedule;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.SetTerminalPanel(App.Mediator.TerminalID.ToString(),App.Mediator.Description);
                this.stbMain.User1Panel.Width = 144;
                this.stbMain.User1Panel.Text = App.Config.Role;
                this.stbMain.User1Panel.ToolTipText = "User role";
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
			finally { Application.DoEvents(); }
		}
        private void buildHelpMenu() {
            //Build dynamic help menu from configuration file
            try {
                //Read help menu configuration from app.config
                this.mHelpItems = (NameValueCollection)ConfigurationManager.GetSection("menu/help");
                for(int i = 0; i < this.mHelpItems.Count; i++) {
                    string sKey = this.mHelpItems.GetKey(i);
                    string sValue = this.mHelpItems.GetValues(sKey)[0];
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    //item.Name = "mnuHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpItemClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
		#endregion
		#region Ship Schedule Services: newSchedule(), openSchedule()
		private void newSchedule() {
			//Create a new ship schedule
			dlgSelectDate dlg = new dlgSelectDate();
			if(dlg.ShowDialog() == DialogResult.OK) {
				//Validate that there isn't a schedule for the selected sortcenter and date
                if(ShipScheduleFactory.SchedulesItem(dlg.SortCenterID,dlg.ScheduleDate) != null) {
					//Existing schedule; allow option to edit
					if(MessageBox.Show(this,"Schedule for this date already exists. Would you like to edit it?", "Manage Ship Schedule", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
						//Find the schedule entry, get the scheduleID, and open the schedule
						this.Cursor = Cursors.WaitCursor;
						for(int i=0; i<this.grdSchedules.Rows.VisibleRowCount; i++) {
							if( this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["SortCenterID"].Value.ToString() == dlg.SortCenterID.ToString() && 
								this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["ScheduleDate"].Value.ToString() == dlg.ScheduleDate.ToString()) {
								this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Selected = true;
								this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Activate();
								openSchedule();
							}
						}
					}
				}
				else {
					//New schedule; request a new schedule instance
					this.Cursor = Cursors.WaitCursor;
                    ShipSchedule schedule = ShipScheduleFactory.SchedulesAdd(dlg.SortCenterID,dlg.SortCenter,dlg.ScheduleDate);
					//Find the new schedule entry, get the scheduleID, and open the schedule
					for(int i=0; i<this.grdSchedules.Rows.VisibleRowCount; i++) {
						if( this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["SortCenterID"].Value.ToString() == dlg.SortCenterID.ToString() && 
							this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["ScheduleDate"].Value.ToString() == dlg.ScheduleDate.ToString()) {
							this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Selected = true;
							this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Activate();
							openSchedule();
						}
					}
				}
			}
		}
		private void openSchedule() {
			//Open an existing schedule
			this.Cursor = Cursors.WaitCursor;
			string scheduleID="";
			winSchedule win=null;
			//if(this.grdSchedules.Selected.Rows.Count > 0) {
			for(int k=0; k<this.grdSchedules.Selected.Rows.Count; k++) {
				//Open the selected schedule; validate it is not open already
				scheduleID = this.grdSchedules.Selected.Rows[k].Cells["ScheduleID"].Value.ToString();
				for(int i=0; i< this.MdiChildren.Length; i++) {
					win = (winSchedule)this.MdiChildren[i];
					if(win.Schedule.ScheduleID == scheduleID) {
						//Already open; bring to forefront
						win.Activate();
						if(win.WindowState == FormWindowState.Minimized) win.WindowState = FormWindowState.Normal;
						break;
					}
					else
						win = null;
				}
				if(win == null) {
					//Not open; open and register for events
                    ShipSchedule schedule = ShipScheduleFactory.SchedulesItem(scheduleID);
					this.mMessageMgr.AddMessage("Opening " + schedule.SortCenter + " ship schedule for " + schedule.ScheduleDate + "...");
					win = new winSchedule(schedule);
					win.WindowState = (this.MdiChildren.Length > 0) ? this.MdiChildren[0].WindowState : FormWindowState.Maximized;
					win.MdiParent = this;
					win.Activated += new EventHandler(OnScheduleActivated);
					win.Deactivate += new EventHandler(OnScheduleDeactivated);
					win.Closing += new CancelEventHandler(OnScheduleClosing);
					win.Closed += new EventHandler(OnScheduleClosed);
					win.StatusMessage += new StatusEventHandler(OnStatusMessage);
					win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
					win.Show();
				}		
			}
		}
		#endregion
		#region Schedule Window Mgt: OnScheduleActivated(), OnScheduleDeactivated(), OnScheduleClosing(), OnScheduleClosed()
		private void OnScheduleActivated(object sender, System.EventArgs e) {
			//Event handler for activaton of a viewer child window
			try {
				this.mActiveSchedule = null;
				if(sender != null) {
					winSchedule frm = (winSchedule)sender;
					this.mActiveSchedule = frm;
					this.grdTemplates.DataSource = this.mActiveSchedule.Schedule.Templates;
					for(int i=0; i<this.grdSchedules.Rows.VisibleRowCount; i++) {
						string sortCenterID = this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["SortCenterID"].Value.ToString();
						string scheduleDate = this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["ScheduleDate"].Value.ToString();
						if(sortCenterID == this.mActiveSchedule.Schedule.SortCenterID.ToString() && scheduleDate == this.mActiveSchedule.Schedule.ScheduleDate.ToString()) {
							this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Selected = true;
							this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Activate();
						}
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}						
		private void OnScheduleDeactivated(object sender, System.EventArgs e) {
			//Event handler for deactivaton of a viewer child window
			this.mActiveSchedule = null;
			this.grdTemplates.DataSource = this.mTemplateDS;
			this.grdSchedules.Selected.Rows.Clear();
			setUserServices();
		}
		private void OnScheduleClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing via control box; e.Cancel=true keeps window open
			try {
				e.Cancel = false;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnScheduleClosed(object sender, System.EventArgs e) {
			//Event handler for closing of a viewer child window
			if(this.MdiChildren.Length == 1 && this.btnFullScreen.Checked) this.mnuViewFullScreen.PerformClick();
			setUserServices();
		}
		private void OnServiceStatesChanged(object sender, System.EventArgs e) { setUserServices(); }
		private void OnStatusMessage(object sender, StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
		#endregion
        #region Template Services: OnTemplateSelected(), OnCloseTemplates(), OnEnterTemplates(), OnLeaveTemplates()
        private void OnTemplateSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in template row selections
			try {
				//Clear current selections
				for(int i=0; i<this.mActiveSchedule.Schedule.Templates.TemplateViewTable.Rows.Count; i++) 
					this.mActiveSchedule.Schedule.Templates.TemplateViewTable[i].Selected = false;
				
				//Update all selected load templates as selected for Add
				for(int i=0; i<this.grdTemplates.Selected.Rows.Count; i++) {
					string templateID = this.grdTemplates.Selected.Rows[i].Cells["TemplateID"].Value.ToString();
					for(int j=0; j<this.mActiveSchedule.Schedule.Templates.TemplateViewTable.Rows.Count; j++) {
						if(this.mActiveSchedule.Schedule.Templates.TemplateViewTable[j].TemplateID == templateID) {
							this.mActiveSchedule.Schedule.Templates.TemplateViewTable[j].Selected = true;
							break;
						}
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnCloseTemplates(object sender, System.EventArgs e) {
			//Event handler to close log windows
			this.mnuViewTemplates.PerformClick();
		}
		private void OnEnterTemplates(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			try { 
				this.lblTemplateHeader.BackColor = this.lblCloseTemplates.BackColor = SystemColors.ActiveCaption;
				this.lblTemplateHeader.ForeColor = this.lblCloseTemplates.ForeColor = SystemColors.ActiveCaptionText;
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnLeaveTemplates(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			try { 
				this.lblTemplateHeader.BackColor = this.lblCloseTemplates.BackColor = SystemColors.Control;
				this.lblTemplateHeader.ForeColor = this.lblCloseTemplates.ForeColor = SystemColors.ControlText;
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
	}
}
