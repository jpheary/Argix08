using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Data;
using Argix.Windows;

namespace Argix.Freight {
	//Freight Assignment main application window
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private IBShipment mSelectedFreight = null;
        private StationAssignment mSelectedAssignment=null;
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvcShipments = null,mGridSvcAssignments = null,mGridSvcHistory = null;
        private System.Windows.Forms.ToolTip mToolTip=null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
		
		#region Controls

        private Argix.Windows.ArgixStatusBar stbMain;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdShipments;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdAssignments;
        private Argix.Freight.InboundFreightDS mShipmentsDS;
        private Argix.Freight.FreightAssignDS mAssignmentsDS;
		private System.Windows.Forms.CheckBox chkShowSorted;
        private System.Windows.Forms.Label _lblDays;
        private System.Windows.Forms.NumericUpDown updSortedDays;
		private System.Windows.Forms.TabControl tabAssignments;
		private System.Windows.Forms.TabPage tabRealTime;
		private System.Windows.Forms.TabPage tabHistory;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdHistory;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripSeparator mnuEditSep1;
        private ToolStripMenuItem mnuEditSearch;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewBoldFonts;
        private ToolStripSeparator mnuViewSep2;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuFreight;
        private ToolStripMenuItem mnuFreightAssign;
        private ToolStripMenuItem mnuFreightUnassign;
        private ToolStripSeparator mnuFreightSep1;
        private ToolStripMenuItem mnuFreightStopSort;
        private ToolStripMenuItem mnuAssignments;
        private ToolStripMenuItem mnuAssignmentsUnassign;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ContextMenuStrip csFreight;
        private ToolStripMenuItem ctxFAssign;
        private ToolStripMenuItem ctxFUnassign;
        private ToolStripSeparator ctxFSep1;
        private ToolStripMenuItem ctxFStopSort;
        private ContextMenuStrip csAssignments;
        private ToolStripMenuItem ctxAUnassign;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnSave;
        private ToolStripButton btnPrint;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnSearch;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnAssign;
        private ToolStripButton btnUnassign;
        private ToolStripButton btnStopSort;
        private ToolStripSeparator btnSep4;
        private ToolStripButton btnUnassign2;
        private ToolStripTextBox txtSearch;
        private Splitter splitterH;
        private ComboBox cboFreightType;
        private ToolStripMenuItem ctxARefresh;
        private ToolStripMenuItem ctxAClear;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator btnSep5;
        private ToolStripSeparator btnSep6;
        private ToolStripMenuItem mnuViewHistory;
        private ToolStripMenuItem mnuAssignmentsClearHistory;
        private ToolStripMenuItem ctxFRefresh;
        private ToolStripSeparator ctxFSep2;
		private System.ComponentModel.IContainer components;
		
		#endregion
		
		//Interface
        public frmMain() {
			//Constructor			
			this.Cursor = Cursors.WaitCursor;
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.Text = "Argix Direct " + App.Product;
                buildHelpMenu();
                #region Window docking
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
                //this.grdShipments.Controls.AddRange(new Control[] { this._lblDays,this.updSortedDays,this._lblDays });
                this.grdShipments.Dock = DockStyle.Fill;
                this.splitterH.MinExtra = this.splitterH.MinSize = 96;
				this.splitterH.Dock = DockStyle.Bottom;
				this.tabAssignments.Dock = DockStyle.Bottom;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.grdShipments,this.splitterH,this.tabAssignments,this.tsMain,this.msMain,this.stbMain });
				#endregion
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);

                //Create data and UI services
                this.mPageSettings = new PageSettings();
                this.mPageSettings.Landscape = true;
                this.mGridSvcShipments = new UltraGridSvc(this.grdShipments);
                this.mGridSvcAssignments = new UltraGridSvc(this.grdAssignments);
                this.mGridSvcHistory = new UltraGridSvc(this.grdHistory);
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.stbMain.Panels[0],500,3000);
                configApplication();
			}
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("StationFreightAssignmentTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WorkStationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Client");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Shipper");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pickup");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Result");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("FreightAssignmentHistoryTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Date");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Client");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationNumbers");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Time");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lead");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("InboundFreightTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CurrentLocation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StorageTrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pickup");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FloorStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SealNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UnloadedStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorKey");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReceiveDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsSortable");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewBoldFonts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFreight = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFreightAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFreightUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFreightSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFreightStopSort = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAssignments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAssignmentsUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAssignmentsClearHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.csFreight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxFRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxFSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxFAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxFUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxFSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxFStopSort = new System.Windows.Forms.ToolStripMenuItem();
            this.csAssignments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxARefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxAClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxAUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAssign = new System.Windows.Forms.ToolStripButton();
            this.btnUnassign = new System.Windows.Forms.ToolStripButton();
            this.btnSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStopSort = new System.Windows.Forms.ToolStripButton();
            this.btnSep6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnassign2 = new System.Windows.Forms.ToolStripButton();
            this.tabAssignments = new System.Windows.Forms.TabControl();
            this.tabRealTime = new System.Windows.Forms.TabPage();
            this.grdAssignments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mAssignmentsDS = new Argix.Freight.FreightAssignDS();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.grdHistory = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdShipments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cboFreightType = new System.Windows.Forms.ComboBox();
            this._lblDays = new System.Windows.Forms.Label();
            this.updSortedDays = new System.Windows.Forms.NumericUpDown();
            this.chkShowSorted = new System.Windows.Forms.CheckBox();
            this.mShipmentsDS = new Argix.Freight.InboundFreightDS();
            this.msMain.SuspendLayout();
            this.csFreight.SuspendLayout();
            this.csAssignments.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tabAssignments.SuspendLayout();
            this.tabRealTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentsDS)).BeginInit();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipments)).BeginInit();
            this.grdShipments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updSortedDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipmentsDS)).BeginInit();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuFreight,
            this.mnuAssignments,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(0);
            this.msMain.Size = new System.Drawing.Size(760, 24);
            this.msMain.TabIndex = 5;
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileSep2,
            this.mnuFileSetup,
            this.mnuFilePrint,
            this.mnuFilePreview,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 24);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(152, 22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSetup.Text = "Page Set&up...";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
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
            this.mnuEditSearch});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 24);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCopy.Text = "C&opy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(109, 22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(106, 6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.Size = new System.Drawing.Size(109, 22);
            this.mnuEditSearch.Text = "&Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewBoldFonts,
            this.mnuViewHistory,
            this.mnuViewSep2,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 24);
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(130, 22);
            this.mnuViewRefresh.Text = "&Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(127, 6);
            // 
            // mnuViewBoldFonts
            // 
            this.mnuViewBoldFonts.Checked = true;
            this.mnuViewBoldFonts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuViewBoldFonts.Name = "mnuViewBoldFonts";
            this.mnuViewBoldFonts.Size = new System.Drawing.Size(130, 22);
            this.mnuViewBoldFonts.Text = "Bold Fonts";
            this.mnuViewBoldFonts.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewHistory
            // 
            this.mnuViewHistory.Checked = true;
            this.mnuViewHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuViewHistory.Name = "mnuViewHistory";
            this.mnuViewHistory.Size = new System.Drawing.Size(130, 22);
            this.mnuViewHistory.Text = "History";
            this.mnuViewHistory.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(127, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(130, 22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(130, 22);
            this.mnuViewStatusBar.Text = "&Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFreight
            // 
            this.mnuFreight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFreightAssign,
            this.mnuFreightUnassign,
            this.mnuFreightSep1,
            this.mnuFreightStopSort});
            this.mnuFreight.Name = "mnuFreight";
            this.mnuFreight.Size = new System.Drawing.Size(56, 24);
            this.mnuFreight.Text = "F&reight";
            // 
            // mnuFreightAssign
            // 
            this.mnuFreightAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.mnuFreightAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFreightAssign.Name = "mnuFreightAssign";
            this.mnuFreightAssign.Size = new System.Drawing.Size(207, 22);
            this.mnuFreightAssign.Text = "&Assign To Stations...";
            this.mnuFreightAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFreightUnassign
            // 
            this.mnuFreightUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.mnuFreightUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFreightUnassign.Name = "mnuFreightUnassign";
            this.mnuFreightUnassign.Size = new System.Drawing.Size(207, 22);
            this.mnuFreightUnassign.Text = "&Unassign From Stations...";
            this.mnuFreightUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFreightSep1
            // 
            this.mnuFreightSep1.Name = "mnuFreightSep1";
            this.mnuFreightSep1.Size = new System.Drawing.Size(204, 6);
            // 
            // mnuFreightStopSort
            // 
            this.mnuFreightStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.mnuFreightStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFreightStopSort.Name = "mnuFreightStopSort";
            this.mnuFreightStopSort.Size = new System.Drawing.Size(207, 22);
            this.mnuFreightStopSort.Text = "Stop Sorting Shipment";
            this.mnuFreightStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuAssignments
            // 
            this.mnuAssignments.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAssignmentsUnassign,
            this.mnuAssignmentsClearHistory});
            this.mnuAssignments.Name = "mnuAssignments";
            this.mnuAssignments.Size = new System.Drawing.Size(122, 24);
            this.mnuAssignments.Text = "Freight &Assignment";
            // 
            // mnuAssignmentsUnassign
            // 
            this.mnuAssignmentsUnassign.Image = global::Argix.Properties.Resources.Delete;
            this.mnuAssignmentsUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAssignmentsUnassign.Name = "mnuAssignmentsUnassign";
            this.mnuAssignmentsUnassign.Size = new System.Drawing.Size(142, 22);
            this.mnuAssignmentsUnassign.Text = "&Unassign";
            this.mnuAssignmentsUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuAssignmentsClearHistory
            // 
            this.mnuAssignmentsClearHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAssignmentsClearHistory.Name = "mnuAssignmentsClearHistory";
            this.mnuAssignmentsClearHistory.Size = new System.Drawing.Size(142, 22);
            this.mnuAssignmentsClearHistory.Text = "Clear History";
            this.mnuAssignmentsClearHistory.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 24);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(148, 22);
            this.mnuToolsConfig.Text = "&Configuration";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 24);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(185, 22);
            this.mnuHelpAbout.Text = "&About Freight Assign";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(182, 6);
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 305);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(760, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 3;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // csFreight
            // 
            this.csFreight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxFRefresh,
            this.ctxFSep1,
            this.ctxFAssign,
            this.ctxFUnassign,
            this.ctxFSep2,
            this.ctxFStopSort});
            this.csFreight.Name = "ctxFreight";
            this.csFreight.Size = new System.Drawing.Size(199, 104);
            // 
            // ctxFRefresh
            // 
            this.ctxFRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxFRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxFRefresh.Name = "ctxFRefresh";
            this.ctxFRefresh.Size = new System.Drawing.Size(198, 22);
            this.ctxFRefresh.Text = "&Refresh";
            this.ctxFRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxFSep1
            // 
            this.ctxFSep1.Name = "ctxFSep1";
            this.ctxFSep1.Size = new System.Drawing.Size(195, 6);
            // 
            // ctxFAssign
            // 
            this.ctxFAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.ctxFAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxFAssign.Name = "ctxFAssign";
            this.ctxFAssign.Size = new System.Drawing.Size(198, 22);
            this.ctxFAssign.Text = "&Assign To Stations";
            this.ctxFAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxFUnassign
            // 
            this.ctxFUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.ctxFUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxFUnassign.Name = "ctxFUnassign";
            this.ctxFUnassign.Size = new System.Drawing.Size(198, 22);
            this.ctxFUnassign.Text = "&Unassign From Stations";
            this.ctxFUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxFSep2
            // 
            this.ctxFSep2.Name = "ctxFSep2";
            this.ctxFSep2.Size = new System.Drawing.Size(195, 6);
            // 
            // ctxFStopSort
            // 
            this.ctxFStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.ctxFStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxFStopSort.Name = "ctxFStopSort";
            this.ctxFStopSort.Size = new System.Drawing.Size(198, 22);
            this.ctxFStopSort.Text = "Stop Sorting Shipment";
            this.ctxFStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csAssignments
            // 
            this.csAssignments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxARefresh,
            this.ctxAClear,
            this.toolStripMenuItem1,
            this.ctxAUnassign});
            this.csAssignments.Name = "ctxAssignments";
            this.csAssignments.Size = new System.Drawing.Size(123, 76);
            // 
            // ctxARefresh
            // 
            this.ctxARefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxARefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxARefresh.Name = "ctxARefresh";
            this.ctxARefresh.Size = new System.Drawing.Size(122, 22);
            this.ctxARefresh.Text = "Refresh";
            this.ctxARefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxAClear
            // 
            this.ctxAClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxAClear.Name = "ctxAClear";
            this.ctxAClear.Size = new System.Drawing.Size(122, 22);
            this.ctxAClear.Text = "Clear";
            this.ctxAClear.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(119, 6);
            // 
            // ctxAUnassign
            // 
            this.ctxAUnassign.Image = global::Argix.Properties.Resources.Delete;
            this.ctxAUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxAUnassign.Name = "ctxAUnassign";
            this.ctxAUnassign.Size = new System.Drawing.Size(122, 22);
            this.ctxAUnassign.Text = "&Unassign";
            this.ctxAUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnSave,
            this.btnPrint,
            this.btnSep2,
            this.btnSearch,
            this.txtSearch,
            this.btnSep3,
            this.btnRefresh,
            this.btnSep4,
            this.btnAssign,
            this.btnUnassign,
            this.btnSep5,
            this.btnStopSort,
            this.btnSep6,
            this.btnUnassign2});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0);
            this.tsMain.Size = new System.Drawing.Size(760, 25);
            this.tsMain.TabIndex = 6;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.ToolTipText = "New...";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.ToolTipText = "Open...";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.ToolTipText = "Save...";
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.ToolTipText = "Print...";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Find;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.ToolTipText = "Search for a TDS";
            this.btnSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 8.25F);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(96, 25);
            this.txtSearch.ToolTipText = "Search for a TDS";
            this.txtSearch.TextChanged += new System.EventHandler(this.OnSearchTextChanged);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.ToolTipText = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep4
            // 
            this.btnSep4.Name = "btnSep4";
            this.btnSep4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAssign
            // 
            this.btnAssign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.btnAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(23, 22);
            this.btnAssign.ToolTipText = "Assign freight to sort station";
            this.btnAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnUnassign
            // 
            this.btnUnassign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.btnUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnassign.Name = "btnUnassign";
            this.btnUnassign.Size = new System.Drawing.Size(23, 22);
            this.btnUnassign.ToolTipText = "Unassign freight from sort station";
            this.btnUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep5
            // 
            this.btnSep5.Name = "btnSep5";
            this.btnSep5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnStopSort
            // 
            this.btnStopSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.btnStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopSort.Name = "btnStopSort";
            this.btnStopSort.Size = new System.Drawing.Size(23, 22);
            this.btnStopSort.ToolTipText = "Stop sorting freight";
            this.btnStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep6
            // 
            this.btnSep6.Name = "btnSep6";
            this.btnSep6.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUnassign2
            // 
            this.btnUnassign2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnassign2.Image = global::Argix.Properties.Resources.Delete;
            this.btnUnassign2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnassign2.Name = "btnUnassign2";
            this.btnUnassign2.Size = new System.Drawing.Size(23, 22);
            this.btnUnassign2.ToolTipText = "Unassign freight from selected sort station";
            this.btnUnassign2.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabAssignments
            // 
            this.tabAssignments.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabAssignments.Controls.Add(this.tabRealTime);
            this.tabAssignments.Controls.Add(this.tabHistory);
            this.tabAssignments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabAssignments.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAssignments.Location = new System.Drawing.Point(0, 170);
            this.tabAssignments.Name = "tabAssignments";
            this.tabAssignments.SelectedIndex = 0;
            this.tabAssignments.ShowToolTips = true;
            this.tabAssignments.Size = new System.Drawing.Size(760, 135);
            this.tabAssignments.TabIndex = 2;
            this.tabAssignments.SelectedIndexChanged += new System.EventHandler(this.OnAssignmentTabChanged);
            // 
            // tabRealTime
            // 
            this.tabRealTime.Controls.Add(this.grdAssignments);
            this.tabRealTime.Location = new System.Drawing.Point(4, 4);
            this.tabRealTime.Name = "tabRealTime";
            this.tabRealTime.Size = new System.Drawing.Size(752, 109);
            this.tabRealTime.TabIndex = 0;
            this.tabRealTime.Text = "Current";
            this.tabRealTime.UseVisualStyleBackColor = true;
            // 
            // grdAssignments
            // 
            this.grdAssignments.ContextMenuStrip = this.csAssignments;
            this.grdAssignments.DataMember = "StationFreightAssignmentTable";
            this.grdAssignments.DataSource = this.mAssignmentsDS;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.FontData.Name = "Verdana";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Appearance = appearance9;
            ultraGridColumn1.Header.VisiblePosition = 9;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "Station#";
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.VisiblePosition = 8;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.Caption = "Type";
            ultraGridColumn4.Header.VisiblePosition = 1;
            ultraGridColumn4.Width = 60;
            ultraGridColumn5.Header.VisiblePosition = 11;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.Caption = "Sort Type";
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 72;
            ultraGridColumn7.Header.Caption = "TDS#";
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridColumn7.Width = 96;
            ultraGridColumn8.Header.Caption = "Trailer#";
            ultraGridColumn8.Header.VisiblePosition = 4;
            ultraGridColumn8.Width = 72;
            ultraGridColumn9.Header.VisiblePosition = 5;
            ultraGridColumn9.Width = 192;
            ultraGridColumn10.Header.Caption = "Vendor\\Agent";
            ultraGridColumn10.Header.VisiblePosition = 6;
            ultraGridColumn10.Width = 192;
            ultraGridColumn11.Header.VisiblePosition = 7;
            ultraGridColumn11.Width = 120;
            ultraGridColumn12.Header.VisiblePosition = 10;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 12;
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
            ultraGridColumn13});
            this.grdAssignments.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance10.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Verdana";
            appearance10.FontData.SizeInPoints = 9F;
            appearance10.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            appearance10.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.CaptionAppearance = appearance10;
            this.grdAssignments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAssignments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.FontData.BoldAsString = "True";
            appearance11.FontData.Name = "Verdana";
            appearance11.FontData.SizeInPoints = 8F;
            appearance11.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grdAssignments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAssignments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance12.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdAssignments.DisplayLayout.Override.RowAppearance = appearance12;
            this.grdAssignments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAssignments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAssignments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAssignments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAssignments.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAssignments.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAssignments.Location = new System.Drawing.Point(0, 0);
            this.grdAssignments.Name = "grdAssignments";
            this.grdAssignments.Size = new System.Drawing.Size(752, 109);
            this.grdAssignments.TabIndex = 0;
            this.grdAssignments.Text = "Station Assignments";
            this.grdAssignments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdAssignments.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnAssignmentSelected);
            // 
            // mAssignmentsDS
            // 
            this.mAssignmentsDS.DataSetName = "FreightAssignDS";
            this.mAssignmentsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mAssignmentsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.grdHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 4);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(752, 109);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // grdHistory
            // 
            this.grdHistory.ContextMenuStrip = this.csAssignments;
            this.grdHistory.DataMember = "FreightAssignmentHistoryTable";
            this.grdHistory.DataSource = this.mAssignmentsDS;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdHistory.DisplayLayout.Appearance = appearance13;
            ultraGridColumn14.Format = "MM-dd-yyyy";
            ultraGridColumn14.Header.VisiblePosition = 0;
            ultraGridColumn14.Width = 96;
            ultraGridColumn15.Header.Caption = "TDS#";
            ultraGridColumn15.Header.VisiblePosition = 3;
            ultraGridColumn15.Width = 96;
            ultraGridColumn16.Header.VisiblePosition = 4;
            ultraGridColumn16.Width = 240;
            ultraGridColumn17.Header.Caption = "Station#";
            ultraGridColumn17.Header.VisiblePosition = 2;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Format = "HH:mm";
            ultraGridColumn18.Header.VisiblePosition = 1;
            ultraGridColumn18.Width = 60;
            ultraGridColumn19.Header.Caption = "Comments";
            ultraGridColumn19.Header.VisiblePosition = 5;
            ultraGridColumn19.Width = 96;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19});
            this.grdHistory.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance14.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 9F;
            appearance14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdHistory.DisplayLayout.CaptionAppearance = appearance14;
            this.grdHistory.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdHistory.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.grdHistory.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.grdHistory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdHistory.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdHistory.DisplayLayout.Override.RowAppearance = appearance16;
            this.grdHistory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdHistory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdHistory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdHistory.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdHistory.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdHistory.Location = new System.Drawing.Point(0, 0);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.Size = new System.Drawing.Size(752, 109);
            this.grdHistory.TabIndex = 1;
            this.grdHistory.Text = "Assignment History";
            this.grdHistory.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdHistory.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnAssignmentHistorySelected);
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 167);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(760, 3);
            this.splitterH.TabIndex = 7;
            this.splitterH.TabStop = false;
            // 
            // grdShipments
            // 
            this.grdShipments.ContextMenuStrip = this.csFreight;
            this.grdShipments.Controls.Add(this.cboFreightType);
            this.grdShipments.Controls.Add(this._lblDays);
            this.grdShipments.Controls.Add(this.updSortedDays);
            this.grdShipments.Controls.Add(this.chkShowSorted);
            this.grdShipments.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdShipments.DataMember = "InboundFreightTable";
            this.grdShipments.DataSource = this.mShipmentsDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdShipments.DisplayLayout.Appearance = appearance1;
            ultraGridColumn20.Header.VisiblePosition = 19;
            ultraGridColumn20.Width = 120;
            ultraGridColumn21.Header.VisiblePosition = 20;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.Header.Caption = "Location";
            ultraGridColumn22.Header.VisiblePosition = 0;
            ultraGridColumn22.Width = 72;
            ultraGridColumn23.Header.Caption = "TDS#";
            ultraGridColumn23.Header.VisiblePosition = 1;
            ultraGridColumn23.Width = 96;
            ultraGridColumn24.Header.Caption = "Trailer#";
            ultraGridColumn24.Header.VisiblePosition = 2;
            ultraGridColumn24.Width = 60;
            ultraGridColumn25.Header.Caption = "St. Trailer#";
            ultraGridColumn25.Header.VisiblePosition = 3;
            ultraGridColumn25.Width = 60;
            ultraGridColumn26.Header.Caption = "Client#";
            ultraGridColumn26.Header.VisiblePosition = 4;
            ultraGridColumn26.Width = 60;
            ultraGridColumn27.Header.Caption = "Client";
            ultraGridColumn27.Header.VisiblePosition = 5;
            ultraGridColumn27.Width = 168;
            ultraGridColumn28.Header.Caption = "Shipper#";
            ultraGridColumn28.Header.VisiblePosition = 6;
            ultraGridColumn28.Width = 60;
            ultraGridColumn29.Header.Caption = "Shipper";
            ultraGridColumn29.Header.VisiblePosition = 7;
            ultraGridColumn29.Width = 168;
            ultraGridColumn30.Header.VisiblePosition = 8;
            ultraGridColumn30.Width = 96;
            ultraGridColumn31.Header.VisiblePosition = 9;
            ultraGridColumn31.Width = 96;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn32.CellAppearance = appearance2;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn32.Header.Appearance = appearance3;
            ultraGridColumn32.Header.VisiblePosition = 10;
            ultraGridColumn32.Width = 72;
            appearance4.TextHAlignAsString = "Right";
            ultraGridColumn33.CellAppearance = appearance4;
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn33.Header.Appearance = appearance5;
            ultraGridColumn33.Header.VisiblePosition = 11;
            ultraGridColumn33.Width = 72;
            ultraGridColumn34.Header.Caption = "Carrier#";
            ultraGridColumn34.Header.VisiblePosition = 12;
            ultraGridColumn34.Width = 60;
            ultraGridColumn35.Header.Caption = "Driver#";
            ultraGridColumn35.Header.VisiblePosition = 13;
            ultraGridColumn35.Width = 60;
            ultraGridColumn36.Header.Caption = "Floor Status";
            ultraGridColumn36.Header.VisiblePosition = 14;
            ultraGridColumn36.Width = 72;
            ultraGridColumn37.Header.Caption = "Seal#";
            ultraGridColumn37.Header.VisiblePosition = 15;
            ultraGridColumn37.Width = 60;
            ultraGridColumn38.Header.Caption = "Unloaded";
            ultraGridColumn38.Header.VisiblePosition = 16;
            ultraGridColumn38.Width = 72;
            ultraGridColumn39.Header.Caption = "Vendor Key";
            ultraGridColumn39.Header.VisiblePosition = 17;
            ultraGridColumn39.Width = 84;
            ultraGridColumn40.Header.Caption = "Received";
            ultraGridColumn40.Header.VisiblePosition = 18;
            ultraGridColumn40.Width = 84;
            ultraGridColumn41.Header.VisiblePosition = 21;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn42.Header.Caption = "Sortable";
            ultraGridColumn42.Header.VisiblePosition = 22;
            ultraGridColumn42.Width = 60;
            ultraGridBand3.Columns.AddRange(new object[] {
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
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42});
            this.grdShipments.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            appearance6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 9F;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdShipments.DisplayLayout.CaptionAppearance = appearance6;
            this.grdShipments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdShipments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdShipments.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdShipments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdShipments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdShipments.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdShipments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdShipments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdShipments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdShipments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdShipments.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdShipments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShipments.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdShipments.Location = new System.Drawing.Point(0, 49);
            this.grdShipments.Name = "grdShipments";
            this.grdShipments.Size = new System.Drawing.Size(760, 118);
            this.grdShipments.TabIndex = 0;
            this.grdShipments.Text = "Inbound Freight ";
            this.grdShipments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdShipments.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnFreightSelected);
            // 
            // cboFreightType
            // 
            this.cboFreightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFreightType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFreightType.Font = new System.Drawing.Font("Verdana", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboFreightType.FormattingEnabled = true;
            this.cboFreightType.ItemHeight = 13;
            this.cboFreightType.Items.AddRange(new object[] {
            "Regular",
            "Returns"});
            this.cboFreightType.Location = new System.Drawing.Point(126, 1);
            this.cboFreightType.Name = "cboFreightType";
            this.cboFreightType.Size = new System.Drawing.Size(96, 21);
            this.cboFreightType.TabIndex = 8;
            this.cboFreightType.SelectionChangeCommitted += new System.EventHandler(this.OnFreightTypeChanged);
            // 
            // _lblDays
            // 
            this._lblDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblDays.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._lblDays.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDays.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._lblDays.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._lblDays.Location = new System.Drawing.Point(723, 4);
            this._lblDays.Name = "_lblDays";
            this._lblDays.Size = new System.Drawing.Size(33, 16);
            this._lblDays.TabIndex = 4;
            this._lblDays.Text = "days";
            this._lblDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // updSortedDays
            // 
            this.updSortedDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updSortedDays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.updSortedDays.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updSortedDays.Location = new System.Drawing.Point(687, 3);
            this.updSortedDays.Name = "updSortedDays";
            this.updSortedDays.Size = new System.Drawing.Size(33, 17);
            this.updSortedDays.TabIndex = 3;
            this.updSortedDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updSortedDays.ValueChanged += new System.EventHandler(this.OnSortedDaysChanged);
            this.updSortedDays.Leave += new System.EventHandler(this.OnSortedDaysChanged);
            // 
            // chkShowSorted
            // 
            this.chkShowSorted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowSorted.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.chkShowSorted.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkShowSorted.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkShowSorted.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.chkShowSorted.Location = new System.Drawing.Point(552, 4);
            this.chkShowSorted.Name = "chkShowSorted";
            this.chkShowSorted.Size = new System.Drawing.Size(132, 16);
            this.chkShowSorted.TabIndex = 2;
            this.chkShowSorted.Text = "Show sorted for";
            this.chkShowSorted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowSorted.UseVisualStyleBackColor = false;
            this.chkShowSorted.CheckedChanged += new System.EventHandler(this.OnShowSortedClick);
            // 
            // mShipmentsDS
            // 
            this.mShipmentsDS.DataSetName = "InboundFreightDS";
            this.mShipmentsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mShipmentsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(7, 16);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(760, 329);
            this.Controls.Add(this.grdShipments);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.tabAssignments);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Station Freight Assignment";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.csFreight.ResumeLayout(false);
            this.csAssignments.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tabAssignments.ResumeLayout(false);
            this.tabRealTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentsDS)).EndInit();
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipments)).EndInit();
            this.grdShipments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updSortedDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipmentsDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
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
                    this.mnuViewHistory.Checked = !Convert.ToBoolean(global::Argix.Properties.Settings.Default.History);
                    this.mnuViewBoldFonts.Checked = !Convert.ToBoolean(global::Argix.Properties.Settings.Default.BoldFonts);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
                this.mToolTip.SetToolTip(this.cboFreightType,"Select the freight type to display.");
				this.mToolTip.SetToolTip(this.chkShowSorted, "Include sorted freight in the view of inbound freight.");
				this.mToolTip.SetToolTip(this.updSortedDays, "Include sorted freight for the last " + this.updSortedDays.Value + " days.");
				#endregion

                //Set control defaults
                #region Grid Initialization
                this.grdShipments.DisplayLayout.Bands[0].Columns["TDSNumber"].SortIndicator = SortIndicator.Ascending;
				this.grdAssignments.DisplayLayout.Bands[0].Columns["StationNumber"].SortIndicator = SortIndicator.Ascending;
				this.grdHistory.DisplayLayout.Bands[0].Columns["Date"].SortIndicator = SortIndicator.Ascending;
                this.grdShipments.DataSource = FreightFactory.InboundFreight;
                this.grdAssignments.DataSource = FreightFactory.StationAssignments;
                this.grdHistory.DataSource = FreightFactory.StationAssignmentHistory;
                #endregion
                this.cboFreightType.SelectedIndex = 0;
				OnFreightTypeChanged(null, EventArgs.Empty);
				this.chkShowSorted.Checked = false;
				OnShowSortedClick(null, EventArgs.Empty);
				this.updSortedDays.Minimum = 1;
                this.updSortedDays.Maximum = (App.Config.SortedDaysMax > 0 ? App.Config.SortedDaysMax : 1);
                this.updSortedDays.Value = FreightFactory.SortedRange;
                this.mnuViewHistory.PerformClick();
                this.mnuViewBoldFonts.PerformClick();
                this.mnuViewRefresh.PerformClick();
                this.grdShipments.Focus();
            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); } finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            if(!e.Cancel) {
                #region Save user preferences
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.History = this.mnuViewHistory.Checked;
                global::Argix.Properties.Settings.Default.BoldFonts = this.mnuViewBoldFonts.Checked;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Stopped"));
            }
        }
        private void OnSearchTextChanged(object sender,EventArgs e) {
            //Event handler for change in search text value
            try {
                //Get specifics for search word and grid
                this.mGridSvcShipments.FindRow(0,this.grdShipments.Tag.ToString(),this.txtSearch.Text);
                this.txtSearch.Focus();
                this.txtSearch.SelectionStart = this.txtSearch.Text.Length;
                setUserServices();
            }
            catch(Exception) { }
        }
        private void OnAssignmentTabChanged(object sender,EventArgs e) {
            //Event handler for change in selected assignment tab
            try {
                switch(this.tabAssignments.SelectedTab.Name) {
                    case "tabRealTime": this.grdAssignments.Focus(); break;
                    case "tabHistory":  this.grdHistory.Focus(); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        #region Freight Services: OnFreightTypeChanged(), OnShowSortedClick(), OnSortedDaysChanged(), OnFreightChanged(), OnFreightSelected()
		private void OnFreightTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change in freight type
			try {
				//Apply filters as applicable
                string freightType = this.cboFreightType.Text.ToLower();
				this.grdShipments.DisplayLayout.Bands[0].ColumnFilters["FreightType"].FilterConditions.Clear();
                this.grdShipments.DisplayLayout.Bands[0].ColumnFilters["FreightType"].FilterConditions.Add(FilterComparisionOperator.Equals,freightType);
				this.grdShipments.DisplayLayout.RefreshFilters();
                this.grdShipments.DisplayLayout.Bands[0].Columns["ShipperNumber"].Header.Caption = (freightType == "regular") ? "Vendor#" : "Agent#";
                this.grdShipments.DisplayLayout.Bands[0].Columns["ShipperName"].Header.Caption = (freightType == "regular") ? "Vendor" : "Agent";
				int index = (this.grdShipments.Selected.Rows.Count > 0) ? this.grdShipments.Selected.Rows[0].VisibleIndex : 0;
				if(this.grdShipments.Rows.VisibleRowCount > 0) {
					if(index >=0 && index < this.grdShipments.Rows.VisibleRowCount) 
						this.grdShipments.Rows.GetRowAtVisibleIndex(index).Selected = true;
					else
						this.grdShipments.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdShipments.Selected.Rows[0].Activate();
					this.grdShipments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdShipments.Selected.Rows[0]);
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnShowSortedClick(object sender, System.EventArgs e) {
			//Event handler to enable\disable 'sorted' shipments in the view
			try {				
				//Change shipment filter to include\exclude sorted shipments
				this.grdShipments.DisplayLayout.Bands[0].ColumnFilters["Status"].FilterConditions.Clear();
				if(!this.chkShowSorted.Checked) 
					this.grdShipments.DisplayLayout.Bands[0].ColumnFilters["Status"].FilterConditions.Add(FilterComparisionOperator.NotEquals, "SORTED");
				int index = (this.grdShipments.Selected.Rows.Count > 0) ? this.grdShipments.Selected.Rows[0].VisibleIndex : 0;
				if(this.grdShipments.Rows.VisibleRowCount > 0) {
					if(index >=0 && index < this.grdShipments.Rows.VisibleRowCount) 
						this.grdShipments.Rows.GetRowAtVisibleIndex(index).Selected = true;
					else
						this.grdShipments.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdShipments.Selected.Rows[0].Activate();
					this.grdShipments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdShipments.Selected.Rows[0]);
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnSortedDaysChanged(object sender, System.EventArgs e) {
			//Event handler for sorted days changed
			try {
                FreightFactory.SortedRange = Convert.ToInt32(this.updSortedDays.Value);
				this.mToolTip.SetToolTip(this.updSortedDays, "Include sorted freight for the last " + this.updSortedDays.Value.ToString() + " days.");
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnFreightChanged(object sender, System.EventArgs e) {
			//Event handler for change in inbound freight collection
			//No visible rows:							no selection
			//Visible rows; no prior selection:			select row at visible index = 0
			//Visible rows; prior selection visible:	select row of prior selection
			//Visible rows; prior selection hidden:		select row at visible index = 0
			try {
				//Select a freight entry (if any are visible)
                this.mMessageMgr.AddMessage("Loading freight for " + App.Mediator.Description + "...");
                int index = -1;
				if(this.grdShipments.Rows.VisibleRowCount > 0) {
					index = 0;
					if(this.mSelectedFreight != null) {
						//Determine index of last freight selection if still visible
						for(int i=0; i<this.grdShipments.Rows.Count; i++) {
							if(this.grdShipments.Rows[i].Cells["FreightID"].Value.ToString() == this.mSelectedFreight.FreightID) {
								if(this.grdShipments.Rows[i].VisibleIndex > 0)
									index = this.grdShipments.Rows[i].VisibleIndex;
								break;
							}
						}
					}
					this.grdShipments.Rows.GetRowAtVisibleIndex(index).Selected = true;
					this.grdShipments.Selected.Rows[0].Activate();
					this.grdShipments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdShipments.Selected.Rows[0]);
				}
				else 
					OnFreightSelected(null, null);
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnFreightSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected freight (inbound shipment)
			try {
				//Clear reference to prior shipment object
				this.mSelectedFreight = null;
				if(this.grdShipments.Selected.Rows.Count > 0) {
					//Get a shipment object for the selected shipment
					string id = this.grdShipments.Selected.Rows[0].Cells["FreightID"].Value.ToString();
                    this.mSelectedFreight = FreightFactory.GetShipment(id);
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#endregion
        #region Assignment Services: OnAssignmentsChanged(), OnAssignmentSelected(), OnAssignmentHistoryChanged(), OnAssignmentHistorySelected()
        private void OnAssignmentsChanged(object sender, System.EventArgs e) {
			//Event handler for change in station assignments collection
			//No visible rows:							no selection
			//Visible rows; no prior selection:			select row at visible index = 0
			//Visible rows; prior selection removed:	select row with nearest station number
			try {
				this.mMessageMgr.AddMessage("Loading station assignments for " + App.Mediator.Description + "...");
				this.grdAssignments.Refresh();
				Application.DoEvents();
				int index = -1;
				if(this.grdAssignments.Rows.VisibleRowCount > 0) {
					index = 0;
					if(this.mSelectedAssignment != null) {
						//Determine index of next visible row; if at end, take last
						int i=0;
						for(i=0; i<this.grdAssignments.Rows.Count; i++) {
							if(Convert.ToInt32(this.grdAssignments.Rows[i].Cells["StationNumber"].Value) >= Convert.ToInt32(this.mSelectedAssignment.SortStation.Number)) {
								index = this.grdAssignments.Rows[i].VisibleIndex;
								break;
							}
						}
						if(i == this.grdAssignments.Rows.Count && index == 0) index = this.grdAssignments.Rows.VisibleRowCount - 1;
					}
					this.grdAssignments.Rows.GetRowAtVisibleIndex(index).Selected = true;
					this.grdAssignments.Selected.Rows[0].Activate();
					this.grdAssignments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdAssignments.Selected.Rows[0]);
				}
				else
					OnAssignmentSelected(null, null);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnAssignmentSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected assignment
			try {
				//Create new instance of StationAssignmnet after selection changed
				this.mSelectedAssignment = null;
				if(this.grdAssignments.Selected.Rows.Count > 0) {
                    WorkstationDS.WorkstationTableRow ws = new WorkstationDS().WorkstationTable.NewWorkstationTableRow();
                    ws.TerminalID = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["TerminalID"].Value);
                    ws.WorkStationID = this.grdAssignments.Selected.Rows[0].Cells["WorkStationID"].Value.ToString();
                    ws.Number = this.grdAssignments.Selected.Rows[0].Cells["StationNumber"].Value.ToString();
                    Workstation workstation = new Workstation(ws);
                    InboundFreightDS.InboundFreightTableRow ibf = new InboundFreightDS().InboundFreightTable.NewInboundFreightTableRow();
                    ibf.TerminalID = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["TerminalID"].Value);
                    ibf.FreightID = this.grdAssignments.Selected.Rows[0].Cells["FreightID"].Value.ToString();
                    ibf.FreightType = this.grdAssignments.Selected.Rows[0].Cells["FreightType"].Value.ToString();
                    ibf.TDSNumber = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["TDSNumber"].Value);
                    IBShipment shipment = new IBShipment(ibf);
                    int sortTypeID = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["SortTypeID"].Value);
					this.mSelectedAssignment = new StationAssignment(workstation, shipment, sortTypeID);
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnAssignmentHistoryChanged(object sender, System.EventArgs e) {
			//Event handler for change in station assignments history collection
			try {
				this.mMessageMgr.AddMessage("Updating assignment history...");
                this.grdHistory.Refresh();
                Application.DoEvents();
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
        private void OnAssignmentHistorySelected(object sender,AfterSelectChangeEventArgs e) {
            //Event handler for change in history selection
            setUserServices();
        }
        #endregion
		#region Grid Support: OnGridMouseDown()
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			try {
				//Set menu and toolbar services
				if(e.Button == MouseButtons.Right) {
					UltraGrid oGrid = (UltraGrid)sender;
					UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
					object oContext = oUIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
					if(oContext != null) {
						//On row
						UltraGridCell oCell = (UltraGridCell)oContext;
						oGrid.ActiveRow = oCell.Row;
						oGrid.ActiveRow.Selected = true;
					}
					else {
						//Off row
						oContext = oUIElement.GetContext(typeof(RowScrollRegion));
						if(oContext != null) {
							oGrid.ActiveRow = null;
							if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Selected = false;
						}
					}
					oGrid.Focus();
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#endregion
		#region User Services: OnItemClick(), OnHelpMenuClick(), OnDataStatusUpdate()
		private void OnItemClick(object sender, System.EventArgs e) {
			//Menu services
            dlgAssignmentDetail dlgAssignment = null;
            DialogResult res = DialogResult.None;
            bool bSorted = true,hasAssignments = false;
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
					case "mnuFileNew":
					case "btnNew":
					    break;
					case "mnuFileOpen":
					case "btnOpen":
					    break;
					case "mnuFileSave":
				    case "btnSave":
					    break;
					case "mnuFileSaveAs":
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.Filter = "Export Files (*.xml) | *.xml";
						dlgSave.FilterIndex = 0;
						dlgSave.Title = "Save Freight As...";
						dlgSave.FileName = App.Mediator.Description + ", " + DateTime.Today.ToLongDateString();
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
							Application.DoEvents();
                            FreightFactory.InboundFreight.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
						}
						break;
					case "mnuFileSetup":	UltraGridPrinter.PageSettings(); break;
					case "mnuFilePrint":
                        if(this.grdShipments.Focused)
                            UltraGridPrinter.Print(this.grdShipments,App.Mediator.Description.Trim().ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString(),true);
                        else if(this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused)
                            UltraGridPrinter.Print(this.grdHistory,App.Mediator.Description.Trim().ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString(),true);
                        break;
                    case "btnPrint":
                        if(this.grdShipments.Focused)
                            UltraGridPrinter.Print(this.grdShipments, App.Mediator.Description.Trim().ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString(), false);
                        else if (this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused)
                            UltraGridPrinter.Print(this.grdHistory, App.Mediator.Description.Trim().ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString(), false);
                        break;
                    case "mnuFilePreview":
                    case "btnPreview":
                        if(this.grdShipments.Focused)
                            UltraGridPrinter.PrintPreview(this.grdShipments,App.Mediator.Description.Trim().ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString());
                        else if(this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused)
                            UltraGridPrinter.PrintPreview(this.grdHistory,App.Mediator.Description.Trim().ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString());
                        break;
                    case "mnuFileExit": this.Close(); Application.Exit(); break;
					case "mnuEditCut":
                    case "btnCut":
						break;
					case "mnuEditCopy":
                    case "btnCopy":
						break;
					case "mnuEditPaste":
                    case "btnPaste":
						break;
					case "mnuEditSearch":	
					case "btnSearch":
                        this.txtSearch.Focus(); 
					    break;
					case "mnuViewRefresh":
                    case "btnRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        FreightFactory.RefreshFreight();
                        FreightFactory.RefreshStationAssignments();
						break;
                    case "ctxFRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        FreightFactory.RefreshFreight();
                        break;
                    case "ctxARefresh":
                        this.Cursor = Cursors.WaitCursor;
                        FreightFactory.RefreshStationAssignments();
                        break;
                    case "mnuViewBoldFonts":
						this.mnuViewBoldFonts.Checked = !this.mnuViewBoldFonts.Checked;
						Font font = new Font("Verdana", 8.25F, (!this.mnuViewBoldFonts.Checked ? FontStyle.Regular : FontStyle.Bold), GraphicsUnit.Point, ((System.Byte)(0)));
						this.grdShipments.Font = this.grdAssignments.Font = this.grdHistory.Font = this.cboFreightType.Font = font;
						break;
                    case "mnuViewHistory":
                        this.mnuViewHistory.Checked = !this.mnuViewHistory.Checked;
                        if(this.mnuViewHistory.Checked) {
                            if(this.tabHistory.Parent == null) this.tabAssignments.TabPages.Add(this.tabHistory);
                        }
                        else
                            this.tabAssignments.TabPages.Remove(this.tabHistory);
                        break;
					case "mnuViewToolbar":		this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case "mnuViewStatusBar":	this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
					case "mnuFreightAssign":
					case "ctxFAssign":
                    case "btnAssign":
                        //Assign shipment to one or more sort stations
						if(this.mSelectedFreight.IsSortable) {
							dlgAssignment = new dlgAssignmentDetail(DialogActionEnum.DialogActionAssign, this.mSelectedFreight, "");
							res = dlgAssignment.ShowDialog(this);
							if(res==DialogResult.OK)
                                FreightFactory.RefreshFreight();
						}
						else
							MessageBox.Show("Freight cannot be assigned because all TDS arrival information has not been entered.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
					case "mnuFreightUnassign":
					case "ctxFUnassign":
                    case "btnUnassign":
                        //Unassign shipment from all applicable stations
						res=DialogResult.Cancel;
                        dlgAssignment = new dlgAssignmentDetail(DialogActionEnum.DialogActionUnassignAny,this.mSelectedFreight,"");
						res = dlgAssignment.ShowDialog(this);
						if(res == DialogResult.OK) {
							//If no other assignments for this freight and NOT 'sorted', allow user to set freight status to sorted
                            bSorted = FreightFactory.IsSortStopped(this.mSelectedFreight);
                            hasAssignments = FreightFactory.StationAssignments.StationFreightAssignmentTable.Select("FreightID = '" + this.mSelectedFreight.FreightID + "'").Length > 0;
							if(!hasAssignments && !bSorted) {
								res = MessageBox.Show(this, "Selected freight is not being sorted on any stations. Would you like to stop sort for this freight?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
								if(res == DialogResult.Yes) {
									this.Cursor = Cursors.WaitCursor;
                                    if(FreightFactory.StopSort(this.mSelectedFreight)) {
										MessageBox.Show("Sorting was stopped for the selected freight.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        FreightFactory.RefreshFreight();
									}
									else
										MessageBox.Show("Sorting could not be stopped for selected freight.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
								}
							}
						}
						break;
					case "mnuFreightStopSort":
					case "ctxFStopSort":
                    case "btnStopSort":
                        //Stop sort for the selected shipment
						res = MessageBox.Show(this, "Stop sorting the selected freight?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if(res==DialogResult.Yes) {
							this.Cursor = Cursors.WaitCursor;
                            if(FreightFactory.StopSort(this.mSelectedFreight)) {
								MessageBox.Show("Sorting was stopped for the selected freight.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                FreightFactory.RefreshFreight();
							}
							else
								MessageBox.Show("Sorting could not be stopped for selected freight.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
						break;
					case "mnuAssignmentsUnassign":
                    case "ctxAUnassign":
                    case "btnUnassign2":
                        //Unassign shipment from selected station
						res=DialogResult.Cancel;
						IBShipment shipment = this.mSelectedAssignment.InboundFreight;
                        dlgAssignment = new dlgAssignmentDetail(DialogActionEnum.DialogActionUnassign,shipment,this.mSelectedAssignment.SortStation.WorkStationID);
						res = dlgAssignment.ShowDialog(this);
						if(res == DialogResult.OK) {
							//If no other assignemnts for this freight and NOT 'sorted', allow user to set freight status to sorted
							bSorted = FreightFactory.IsSortStopped(shipment);
                            hasAssignments = FreightFactory.StationAssignments.StationFreightAssignmentTable.Select("FreightID = '" + shipment.FreightID + "'").Length > 0;
							if(!hasAssignments && !bSorted) { 
								//Select associated shipment
								foreach(UltraGridRow row in this.grdShipments.Rows) {
									if(row.Cells["FreightID"].Value.ToString() == shipment.FreightID) {
										row.Selected = true;
										this.grdShipments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(row);
										break;
									}
								}
								res = MessageBox.Show(this, "Freight #" + shipment.TDSNumber + " is not being sorted on any stations. Would you like to stop sort for this freight?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
								if(res == DialogResult.Yes) 
									this.mnuFreightStopSort.PerformClick();
							}
						}
						break;
                    case "mnuAssignmentsClearHistory":     
                    case "ctxAClear":
                        FreightFactory.StationAssignmentHistory.Clear(); 
                        break;
					case "mnuToolsConfig":			App.ShowConfig(); break;
					case "mnuHelpAbout":            new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception) { }
        }
        private void OnDataStatusUpdate(object sender,DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
       #endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu()
		private void configApplication() {
            try {
                //Create event log database trace listeners, and log application as started
                try {
                    ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel, App.Mediator, App.USP_TRACE, App.EventLogName));
                }
                catch {
                    ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug,App.Mediator,App.USP_TRACE,App.EventLogName));
                }
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Started"));

                //Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                FreightFactory.FreightChanged += new EventHandler(this.OnFreightChanged);
                FreightFactory.AssignmentsChanged += new EventHandler(this.OnAssignmentsChanged);
                FreightFactory.AssignmentHistoryChanged += new EventHandler(this.OnAssignmentHistoryChanged);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); } 
		}
		private void setUserServices() {
			//Set user services
			bool canAssign=false, canUnassignAll=false, canStopSorting=false, canUnassign=false;
            try {
				//Set menu states
                bool isFreight = (this.grdShipments.Focused);
                bool isCurrent = (this.tabAssignments.SelectedTab.Name == this.tabRealTime.Name && this.grdAssignments.Focused);
                bool isHistory = (this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused);
                if(!App.Config.ReadOnly && isFreight && this.mSelectedFreight != null) {
                    bool hasAssignments = FreightFactory.HasAssignments(this.mSelectedFreight.FreightID);
					canAssign = true;
					canUnassignAll = hasAssignments;
                    canStopSorting = (this.mSelectedFreight.Status==ShipmentStatusEnum.Sorting && !hasAssignments);
				}
                canUnassign = (!App.Config.ReadOnly && isCurrent && this.mSelectedAssignment != null);
				
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
				this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = isFreight;
				this.mnuFileSetup.Enabled = true;
                this.mnuFilePreview.Enabled = isFreight || isHistory;
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = isFreight || isHistory;
				this.mnuFileExit.Enabled = true;
                this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = this.ctxARefresh.Enabled = true;    // isFreight || isCurrent;
                this.mnuEditCut.Enabled = this.mnuEditCopy.Enabled = this.mnuEditPaste.Enabled = false;
				this.mnuFreightAssign.Enabled = this.ctxFAssign.Enabled = this.btnAssign.Enabled = canAssign;
				this.mnuFreightUnassign.Enabled = this.ctxFUnassign.Enabled = this.btnUnassign.Enabled = canUnassignAll;
				this.mnuFreightStopSort.Enabled = this.ctxFStopSort.Enabled = this.btnStopSort.Enabled = canStopSorting;
				this.mnuAssignmentsUnassign.Enabled = this.ctxAUnassign.Enabled = this.btnUnassign2.Enabled = canUnassign;
                this.mnuAssignmentsClearHistory.Enabled = this.ctxAClear.Enabled = isHistory;
                this.mnuEditSearch.Enabled = this.txtSearch.Enabled = (this.grdShipments.Rows.VisibleRowCount > 0);
				this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;
				
				this.updSortedDays.Enabled = this.chkShowSorted.Checked;
                this.stbMain.SetTerminalPanel(App.Mediator.TerminalID.ToString(),App.Mediator.Description);
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { Application.DoEvents(); }
		}
        private void buildHelpMenu() {
            //Build dynamic help menu from configuration file
            try {
                //Read help menu configuration from app.config
                this.mHelpItems = (NameValueCollection)ConfigurationManager.GetSection("menu/help");
                for(int i = 0;i < this.mHelpItems.Count;i++) {
                    string sKey = this.mHelpItems.GetKey(i);
                    string sValue = this.mHelpItems.GetValues(sKey)[0];
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    //item.Name = "mnuHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception) { }
        }
		#endregion
	}
}
