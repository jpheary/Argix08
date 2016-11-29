//	File:	main.cs
//	Author:	J. Heary
//	Date:	06/16/08
//	Desc:	Main window for InStoreLabels utility includes view of B&N stores
//          and ability to print a location label for each store.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Argix.Data;
using Argix.Windows;
using Tsort.Devices;
using Tsort.Devices.Printers;
using Argix.Enterprise;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Freight {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private EnterpriseTerminal mEntTerminal=null;
		private Workstation mWorkstation=null;
        private StoreLabelTemplate mLabelTemplate = null;
		
        private UltraGridSvc mStoreGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
        private PrinterStatusHandler mPrinterStatusDelegate = null;
		private Icon icon_idle=null, icon_on=null, icon_off=null;

        #region Constants
        private const string MNU_FILE_NEW = "&New...";
        private const string MNU_FILE_OPEN = "&Open...";
        private const string MNU_FILE_PRINT = "&Print Labels...";
        private const string MNU_FILE_PAGESETUP = "Printer Setup...";
        private const string MNU_FILE_EXIT = "E&xit";
        private const string MNU_EDIT_FIND = "&Search";
        private const string MNU_VIEW_TOOLBAR = "&Toolbar";
        private const string MNU_VIEW_STATUSBAR = "Status&Bar";
        private const string MNU_VIEW_REFRESH = "&Refresh";
        private const string MNU_TOOLS_CONFIG = "&Configuration";
        private const string MNU_TOOLS_DIAGNOSTICS = "&Diagnostics";
        private const string MNU_TOOLS_TRACE = "&Trace";
        private const string MNU_TOOLS_USEWEBSVC = "&Use Web Services";
        private const string MNU_HELP_CONTENTS = "&Contents...";
        private const string MNU_HELP_ABOUT = "&About InStore Labels...";

        private const int TLB_NEW = 0;
        private const int TLB_OPEN = 1;
        //Sep1
        private const int TLB_PRINT = 3;
        //Sep2
        private const int TLB_FIND = 5;
        //Sep3
        private const int TLB_REFRESH = 7;
        #endregion
        private delegate void PrinterStatusHandler(bool state, string status);

		#region Controls

		private System.Windows.Forms.MainMenu mnuMain;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuView;
		private System.Windows.Forms.MenuItem mnuViewSep1;
		private System.Windows.Forms.MenuItem mnuViewToolbar;
		private System.Windows.Forms.MenuItem mnuViewStatusBar;
		private System.Windows.Forms.MenuItem mnuTools;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuHelpSep1;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.ImageList imgMain;
        private System.Windows.Forms.ToolBar tlbMain;
		private System.Windows.Forms.ContextMenu ctxMain;
        private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.ToolBarButton btnPrint;
		private System.Windows.Forms.ToolBarButton btnRefresh;
		private System.Windows.Forms.ToolBarButton btnSep1;
		private System.Windows.Forms.ToolBarButton btnSep2;
		private System.Windows.Forms.MenuItem mnuFilePrint;
		private System.Windows.Forms.MenuItem mnuFilePrintSetup;
		private System.Windows.Forms.MenuItem mnuFileSep2;
        private System.Windows.Forms.MenuItem mnuViewRefresh;
        private Argix.Enterprise.StoreDS mStoresDS;
        private System.Windows.Forms.MenuItem ctxPrint;
		private System.Windows.Forms.MenuItem mnuEditSearch;
		private System.Windows.Forms.ToolBarButton btnFind;
        private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.MenuItem mnuFileNew;
		private System.Windows.Forms.ToolBarButton btnNew;
        private System.Windows.Forms.ToolBarButton btnOpen;
		private System.ComponentModel.IContainer components;
        
        private UltraGrid grdStores;
        private Label _lblSearch1;
        private ToolBarButton btnSep3;
        private MenuItem mnuToolsConfig;
        private TextBox txtSearch1;
        #endregion

        //Interface
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(2000);
				#region Set window docking
				this.tlbMain.Dock = DockStyle.Top;
				this.grdStores.Dock = DockStyle.Fill;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.grdStores,this.tlbMain,this.stbMain });
				#endregion
				
				//Create data and UI services
				this.mStoreGridSvc = new UltraGridSvc(this.grdStores, this.txtSearch1);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
                this.mPrinterStatusDelegate = new PrinterStatusHandler(onPrinterStatus);
				this.icon_idle = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources._idle.ico"));
                this.icon_on = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources._on.ico"));
                this.icon_off = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources._off.ico"));
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("StoreTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Region");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RegionDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("District");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DistrictDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StoreNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("City");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("State");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFileNew = new System.Windows.Forms.MenuItem();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
            this.mnuFilePrintSetup = new System.Windows.Forms.MenuItem();
            this.mnuFilePrint = new System.Windows.Forms.MenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuEditSearch = new System.Windows.Forms.MenuItem();
            this.mnuView = new System.Windows.Forms.MenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.MenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.MenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.MenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.MenuItem();
            this.mnuTools = new System.Windows.Forms.MenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.tlbMain = new System.Windows.Forms.ToolBar();
            this.btnNew = new System.Windows.Forms.ToolBarButton();
            this.btnOpen = new System.Windows.Forms.ToolBarButton();
            this.btnSep1 = new System.Windows.Forms.ToolBarButton();
            this.btnPrint = new System.Windows.Forms.ToolBarButton();
            this.btnSep2 = new System.Windows.Forms.ToolBarButton();
            this.btnFind = new System.Windows.Forms.ToolBarButton();
            this.btnSep3 = new System.Windows.Forms.ToolBarButton();
            this.btnRefresh = new System.Windows.Forms.ToolBarButton();
            this.ctxMain = new System.Windows.Forms.ContextMenu();
            this.ctxPrint = new System.Windows.Forms.MenuItem();
            this.mStoresDS = new Argix.Enterprise.StoreDS();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.grdStores = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this._lblSearch1 = new System.Windows.Forms.Label();
            this.txtSearch1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mStoresDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).BeginInit();
            this.grdStores.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFilePrintSetup,
            this.mnuFilePrint,
            this.mnuFileSep2,
            this.mnuFileExit});
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Index = 0;
            this.mnuFileNew.Text = "New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Index = 1;
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Index = 2;
            this.mnuFileSep1.Text = "-";
            // 
            // mnuFilePrintSetup
            // 
            this.mnuFilePrintSetup.Index = 3;
            this.mnuFilePrintSetup.Text = "Page Setup...";
            this.mnuFilePrintSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Index = 4;
            this.mnuFilePrint.Text = "Print Labels";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Index = 5;
            this.mnuFileSep2.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 6;
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEditSearch});
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Index = 0;
            this.mnuEditSearch.Text = "Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.Index = 2;
            this.mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Index = 0;
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Index = 1;
            this.mnuViewSep1.Text = "-";
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Checked = true;
            this.mnuViewToolbar.Index = 2;
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Checked = true;
            this.mnuViewStatusBar.Index = 3;
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.Index = 3;
            this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Index = 0;
            this.mnuToolsConfig.Text = "Configuration";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 4;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpSep1,
            this.mnuHelpAbout});
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Index = 0;
            this.mnuHelpSep1.Text = "-";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Index = 1;
            this.mnuHelpAbout.Text = "&About InStoreLabels...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0,"");
            this.imgMain.Images.SetKeyName(1,"");
            this.imgMain.Images.SetKeyName(2,"");
            this.imgMain.Images.SetKeyName(3,"");
            this.imgMain.Images.SetKeyName(4,"");
            this.imgMain.Images.SetKeyName(5,"");
            this.imgMain.Images.SetKeyName(6,"");
            this.imgMain.Images.SetKeyName(7,"");
            // 
            // tlbMain
            // 
            this.tlbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tlbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnPrint,
            this.btnSep2,
            this.btnFind,
            this.btnSep3,
            this.btnRefresh});
            this.tlbMain.ButtonSize = new System.Drawing.Size(16,16);
            this.tlbMain.DropDownArrows = true;
            this.tlbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tlbMain.ImageList = this.imgMain;
            this.tlbMain.Location = new System.Drawing.Point(0,0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.ShowToolTips = true;
            this.tlbMain.Size = new System.Drawing.Size(664,28);
            this.tlbMain.TabIndex = 7;
            // 
            // btnNew
            // 
            this.btnNew.Enabled = false;
            this.btnNew.ImageIndex = 0;
            this.btnNew.Name = "btnNew";
            this.btnNew.ToolTipText = "New";
            // 
            // btnOpen
            // 
            this.btnOpen.Enabled = false;
            this.btnOpen.ImageIndex = 1;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.ToolTipText = "Open";
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnPrint
            // 
            this.btnPrint.Enabled = false;
            this.btnPrint.ImageIndex = 3;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ToolTipText = "Print";
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnFind
            // 
            this.btnFind.ImageIndex = 4;
            this.btnFind.Name = "btnFind";
            this.btnFind.ToolTipText = "Search...";
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 5;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ToolTipText = "Refresh";
            // 
            // ctxMain
            // 
            this.ctxMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ctxPrint});
            // 
            // ctxPrint
            // 
            this.ctxPrint.Index = 0;
            this.ctxPrint.Text = "Print Labels";
            this.ctxPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mStoresDS
            // 
            this.mStoresDS.DataSetName = "StoreDS";
            this.mStoresDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mStoresDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // splitterH
            // 
            this.splitterH.BackColor = System.Drawing.SystemColors.Control;
            this.splitterH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Location = new System.Drawing.Point(0,28);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(3,277);
            this.splitterH.TabIndex = 14;
            this.splitterH.TabStop = false;
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,305);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(664,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 17;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // grdStores
            // 
            this.grdStores.CausesValidation = false;
            this.grdStores.ContextMenu = this.ctxMain;
            this.grdStores.Controls.Add(this._lblSearch1);
            this.grdStores.Controls.Add(this.txtSearch1);
            this.grdStores.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdStores.DataMember = "StoreTable";
            this.grdStores.DataSource = this.mStoresDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grdStores.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.Caption = "Region ";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 72;
            ultraGridColumn4.Header.Caption = "District ";
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Width = 96;
            ultraGridColumn5.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn5.Header.Caption = "Store#";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Width = 48;
            ultraGridColumn6.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn6.Header.Caption = "Store Name";
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn6.Width = 192;
            ultraGridColumn7.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn7.Header.Caption = "Address Line 1";
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.Width = 168;
            ultraGridColumn8.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn8.Header.Caption = "Address Line 2";
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Width = 120;
            ultraGridColumn9.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.Width = 120;
            ultraGridColumn10.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn10.Width = 48;
            ultraGridColumn11.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.Width = 72;
            ultraGridColumn12.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn12.Header.VisiblePosition = 0;
            ultraGridColumn12.Width = 48;
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
            ultraGridColumn12});
            appearance2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            ultraGridBand1.Override.ActiveRowAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.ForeColor = System.Drawing.SystemColors.ControlText;
            appearance3.TextHAlignAsString = "Left";
            ultraGridBand1.Override.HeaderAppearance = appearance3;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.FontData.Name = "Verdana";
            appearance4.FontData.SizeInPoints = 8F;
            appearance4.ForeColor = System.Drawing.SystemColors.WindowText;
            ultraGridBand1.Override.RowAlternateAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            ultraGridBand1.Override.RowAppearance = appearance5;
            this.grdStores.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdStores.DisplayLayout.CaptionAppearance = appearance6;
            this.grdStores.DisplayLayout.ColumnScrollbarSmallChange = 5;
            this.grdStores.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdStores.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdStores.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdStores.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdStores.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Solid;
            this.grdStores.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance7.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdStores.DisplayLayout.Override.CellAppearance = appearance7;
            this.grdStores.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdStores.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
            this.grdStores.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdStores.DisplayLayout.Override.MaxSelectedCells = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdStores.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdStores.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdStores.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.grdStores.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None;
            this.grdStores.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdStores.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdStores.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdStores.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdStores.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdStores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdStores.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.grdStores.Location = new System.Drawing.Point(3,28);
            this.grdStores.Name = "grdStores";
            this.grdStores.Size = new System.Drawing.Size(661,277);
            this.grdStores.TabIndex = 18;
            this.grdStores.Text = "Argix Stores";
            this.grdStores.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdStores.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            // 
            // _lblSearch1
            // 
            this._lblSearch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblSearch1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._lblSearch1.Image = ((System.Drawing.Image)(resources.GetObject("_lblSearch1.Image")));
            this._lblSearch1.Location = new System.Drawing.Point(494,2);
            this._lblSearch1.Name = "_lblSearch1";
            this._lblSearch1.Size = new System.Drawing.Size(18,18);
            this._lblSearch1.TabIndex = 6;
            this._lblSearch1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSearch1
            // 
            this.txtSearch1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch1.Location = new System.Drawing.Point(516,0);
            this.txtSearch1.Name = "txtSearch1";
            this.txtSearch1.Size = new System.Drawing.Size(144,21);
            this.txtSearch1.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(664,329);
            this.Controls.Add(this.grdStores);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "InStore Labels";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closed += new System.EventHandler(this.OnFormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.mStoresDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdStores)).EndInit();
            this.grdStores.ResumeLayout(false);
            this.grdStores.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event hadler for form load event
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				Splash.Close();
				this.Visible = true;
				Application.DoEvents();
				#region Set user preferences
				this.WindowState = FormWindowState.Normal;
				this.Left = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Left + 0.05 * Screen.PrimaryScreen.WorkingArea.Width);
				this.Top = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Top + 0.05 * Screen.PrimaryScreen.WorkingArea.Height);
				this.Width = Convert.ToInt32(0.9 * Screen.PrimaryScreen.WorkingArea.Width);
				this.Height = Convert.ToInt32(0.9 * Screen.PrimaryScreen.WorkingArea.Height);
				this.tlbMain.Visible = this.mnuViewToolbar.Checked = true;
				this.stbMain.Visible = this.mnuViewStatusBar.Checked = true;
				Application.DoEvents();
				#endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
				
				//Set control defaults
				this.grdStores.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdStores.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
                this.stbMain.User1Panel.Text = this.mLabelTemplate.LabelType;
                this.stbMain.User1Panel.ToolTipText = "Label#";
                this.stbMain.User1Panel.Width = 48;
                onPrinterStatus(false,"Printer is not set.");
                this.mWorkstation.LabelPrinter.TurnOn();
				this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { 
			//Event handler for form resized event
		}
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for form closed event
			try {
				this.mMessageMgr.AddMessage("Saving settings...");
				this.mWorkstation.LabelPrinter.TurnOff();
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Stopped"));
			}
			catch(Exception) { }
        }
        #region Event Handlers: OnGridBeforeRowFilterDropDownPopulate(), OnStoresChanged(), OnStoreSelected()
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnStoresChanged(object sender,EventArgs e) {
			//Event handler for change in stores collection
			this.Cursor = Cursors.WaitCursor;
			try {
				this.mMessageMgr.AddMessage("Updating store view....");
				this.grdStores.DataSource = this.mEntTerminal.Stores;
				this.grdStores.Refresh();
				if(this.grdStores.Rows.Count>0) {
					this.grdStores.Rows[this.grdStores.Rows.GetRowAtVisibleIndex(0).Index].Selected = true;
					this.grdStores.ActiveRow = this.grdStores.Rows[this.grdStores.Rows.GetRowAtVisibleIndex(0).Index];
				}
                OnStoreSelected(this.grdStores,(Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs)EventArgs.Empty);
				this.mMessageMgr.AddMessage(this.mEntTerminal.Stores.StoreTable.Rows.Count.ToString() + " stores.");
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnStoreSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in grid selections
            setUserServices();
		}
		#endregion
		#region Printer Support: OnPrinterChanged(), OnPrinterTurnedOn(), OnPrinterTurnedOff(), OnPrinterSettingsChanged()
		private void OnPrinterChanged(object sender, EventArgs e) {
			//Event handler for change to the active printer
			try { 
				//Configure for a new printer type
				if(this.mWorkstation.LabelPrinter != null)
                    ArgixTrace.WriteLine(new TraceMessage("Printer type= " + this.mWorkstation.LabelPrinter.Type,App.Product,LogLevel.Debug));
				else
                    ArgixTrace.WriteLine(new TraceMessage("Printer type= null",App.Product,LogLevel.Debug));
				OnPrinterEventChanged(sender, null);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnPrinterTurnedOn(object source, EventArgs e) { }
		private void OnPrinterTurnedOff(object source, EventArgs e) { }
		private void OnPrinterSettingsChanged(object source, EventArgs e) {
            //Event handler for change in printer settings
            if(this.mWorkstation.LabelPrinter != null) 
                this.stbMain.Invoke(this.mPrinterStatusDelegate,new object[] { this.mWorkstation.LabelPrinter.On,this.mWorkstation.LabelPrinter.Type + " (" + this.mWorkstation.LabelPrinter.Settings.PortName + ")" });
            else
                this.stbMain.Invoke(this.mPrinterStatusDelegate,new object[] { false,"Printer is not set." });
        }
		private void OnPrinterEventChanged(object source, PrinterEventArgs e) {
			//Event handler for change in printer status
            if(this.mWorkstation.LabelPrinter != null) 
                this.stbMain.Invoke(this.mPrinterStatusDelegate,new object[] { this.mWorkstation.LabelPrinter.On,this.mWorkstation.LabelPrinter.Type + " (" + this.mWorkstation.LabelPrinter.Settings.PortName + ")" });
			else 
                this.stbMain.Invoke(this.mPrinterStatusDelegate,new object[] { false,"Printer is not set." });
		}
        private void onPrinterStatus(bool state, string status) {
            try {
                if(this.mWorkstation.LabelPrinter != null) {
                    //Configure for a new printer type
                    this.stbMain.User2Panel.Icon = state ? this.icon_on : this.icon_off;
                    this.stbMain.User2Panel.ToolTipText = status;
                }
                else {
                    //Disable printer features
                    this.stbMain.User2Panel.Icon = this.icon_idle;
                    this.stbMain.User2Panel.ToolTipText = "Printer is not set.";
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        #endregion
		#region Local Services: OnItemClick(), OnHelpMenuClick(), OnDataStatusUpdate(
		private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			dlgLogin login=null;
			try {
				MenuItem mnu = (MenuItem)sender;
				switch(mnu.Text) {
					case MNU_FILE_NEW:		break;
					case MNU_FILE_OPEN:		break;
					case MNU_FILE_PAGESETUP:
                        login = new dlgLogin(App.Config.MISPassword);
						login.ValidateEntry();
						if(login.IsValid) 
							this.mWorkstation.ConfigureLabelPrinter();
						break;
                    case MNU_FILE_PRINT:    printLabels(); break;
                    case MNU_FILE_EXIT:     this.Close(); break;
					case MNU_EDIT_FIND:     this.txtSearch1.Focus(); this.txtSearch1.SelectAll(); break;
					case MNU_VIEW_REFRESH: 
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Updating stores view...");
						this.mEntTerminal.RefreshStores();
						break;
					case MNU_VIEW_TOOLBAR:      this.tlbMain.Visible = (this.mnuViewToolbar.Checked = !this.mnuViewToolbar.Checked); break;
					case MNU_VIEW_STATUSBAR:    this.stbMain.Visible = (this.mnuViewStatusBar.Checked = !this.mnuViewStatusBar.Checked); break;
                    case MNU_TOOLS_CONFIG:      App.ShowConfig(); break;
					case MNU_HELP_ABOUT:        new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnHelpMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for configurable help menu items
			try {
				MenuItem mnu = (MenuItem)sender;
				Help.ShowHelp(this, this.mHelpItems.GetValues(mnu.Text)[0]);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
        private void OnDataStatusUpdate(object sender, DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
		#endregion
        #region Local Services: printLabels(), configApplication(), setUserServices(), buildHelpMenu())
        private void printLabels() {
            //Print labels for each selected store
            this.Cursor = Cursors.WaitCursor;
            try {
                StoreLabelMaker labelMaker = new StoreLabelMaker();
                for(int j = 0; j < this.grdStores.Selected.Rows.Count; j++) {
                    try {
                        labelMaker.StoreNumber = this.grdStores.Selected.Rows[j].Cells["StoreNumber"].Value.ToString().PadLeft(5,'0');
                        Tsort.Labels.Label label = new Tsort.Labels.Label(this.mLabelTemplate,labelMaker);
                        this.mWorkstation.LabelPrinter.Print(label.LabelFormat);
                        Thread.Sleep(500);
                    }
                    catch(Exception ex) {
                        ArgixTrace.WriteLine(new TraceMessage(ex.Message,App.Product,LogLevel.Error));
                        this.mMessageMgr.AddMessage("Unexpected error while printing label for store# " + labelMaker.StoreNumber + "...");
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void configApplication() {
			try {
				//Create event log and database trace listeners; and log application as started
				try {
					ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel, App.Mediator,App.USP_TRACE, App.EventLogName));
				}
				catch {
					ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug, App.Mediator,App.USP_TRACE,App.EventLogName));
				}
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Started"));
				
				//Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
				this.mEntTerminal.StoresChanged += new EventHandler(OnStoresChanged);
				this.mWorkstation = new Workstation();
				this.mWorkstation.PrinterChanged += new EventHandler(OnPrinterChanged);
                this.mWorkstation.SetLabelPrinter(App.Config.PrinterType,App.Config.PrinterPort);
				this.mWorkstation.LabelPrinter.PrinterTurnedOn += new EventHandler(OnPrinterTurnedOn);
				this.mWorkstation.LabelPrinter.PrinterTurnedOff += new EventHandler(OnPrinterTurnedOff);
				this.mWorkstation.LabelPrinter.PrinterSettingsChanged += new EventHandler(OnPrinterSettingsChanged);
				this.mWorkstation.LabelPrinter.PrinterEventChanged += new PrinterEventHandler(OnPrinterEventChanged);

                Tsort.Labels.LabelDS.LabelDetailTableRow template = this.mEntTerminal.GetLabelTemplate(App.Config.LabelType,App.Config.PrinterType);
				this.mLabelTemplate = new StoreLabelTemplate(template, App.Mediator);
			}
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			bool canPrint=false;
			try {
				if(this.mWorkstation.LabelPrinter != null)
                    canPrint = !App.Config.ReadOnly && this.mWorkstation.LabelPrinter.On;
				
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
				this.mnuFilePrint.Enabled = this.ctxPrint.Enabled = this.btnPrint.Enabled = canPrint && (this.grdStores.Selected.Rows.Count>0);
				this.mnuFilePrintSetup.Enabled = true;
				this.mnuFileExit.Enabled = true;
				this.mnuEditSearch.Enabled = this.btnFind.Enabled = true;
				this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort._readonly.ico")) : null;
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
				for(int i=0; i<this.mHelpItems.Count; i++) {
					string sKey = this.mHelpItems.GetKey(i);
					string sValue = this.mHelpItems.GetValues(sKey)[0];
					MenuItem mnu = new MenuItem();
					mnu.Index = i;
					mnu.Text = sKey;
					mnu.Click += new System.EventHandler(this.OnHelpMenuClick);
					mnu.Enabled = (sValue != "");
					this.mnuHelp.MenuItems.Add(i, mnu);
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		#endregion
	}
}
