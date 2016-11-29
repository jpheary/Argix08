//	File:	main.cs
//	Author:	J. Heary
//	Date:	04/26/06
//	Desc:	Main window for system administration.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Tsort.Data;
using Tsort.Enterprise;
using Tsort.Freight;
using Tsort.Transportation;
using Tsort.Windows;

namespace Tsort {
	//Class definition
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private static bool ITEventOn=false;
		
		private EnterpriseTerminal mEntTerminal=null;
		private IItem mItem=null;
		private Mediator mMediator=null;
		private PageSettings mPageSettings = null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private winTrace mTrace=null;
		
		private bool mReadOnly=true;
		private string mMISPassword="";
		private NameValueCollection mHelpItems=null;
		
		#region Constants
		//Main menu and context menu captions (serves as identifier in handlers)
		private const string MNU_FILE_NEW = "&New...";
		private const string MNU_FILE_OPEN = "&Open...";
		private const string MNU_FILE_CLOSE = "&Close...";
		private const string MNU_FILE_SAVE = "&Save";
		private const string MNU_FILE_SAVEAS = "Save &As...";
		private const string MNU_FILE_PRINT_SETTINGS = "Page Set&up...";
		private const string MNU_FILE_PRINT = "&Print...";
		private const string MNU_FILE_PREVIEW = "Print Pre&view...";
		private const string MNU_FILE_EXIT = "E&xit";
		private const string MNU_EDIT_CUT = "Cu&t";
		private const string MNU_EDIT_COPY = "&Copy";
		private const string MNU_EDIT_PASTE = "&Paste";
		private const string MNU_EDIT_FIND = "&Search...";
		private const string MNU_EDIT_DELETE = "&Delete";
		private const string MNU_VIEW_REFRESH = "&Refresh";
		private const string MNU_VIEW_REFRESHFILTERS = "Refresh &Filters";
		private const string MNU_VIEW_TOOLBAR = "&Toolbar";
		private const string MNU_VIEW_STATUSBAR = "&Status Bar";
		private const string MNU_TOOLS_FIND = "&Find and Replace";
		private const string MNU_TOOLS_CONSIGNEE = "Consignee &DeliveryLocationDS";
		private const string MNU_TOOLS_SETTINGS = "&Settings";
		private const string MNU_TOOLS_DIAGNOSTICS = "&Diagnostics";
		private const string MNU_TOOLS_TRACE = "&Trace";
		private const string MNU_TOOLS_USEWEBSVC = "&Use Web Services...";
		private const string MNU_HELP_ABOUT = "&About System Admin";
		
		private const string MNU_TRV_EXPAND = "&Expand";
		private const string MNU_TRV_COLLAPSE = "&Collapse";
		private const string MNU_TRV_PROPS = "P&roperties";
		
		private const int TLB_NEW = 0;
		private const int TLB_OPEN = 1;
		//Sep1
		private const int TLB_SAVE = 3;
		//Sep2
		private const int TLB_PRINT = 5;
		private const int TLB_PREVIEW = 6;
		//Sep3
		private const int TLB_CUT = 8;
		private const int TLB_COPY = 9;
		private const int TLB_PASTE = 10;
		//Sep4
		private const int TLB_SEARCH = 12;
		private const int TLB_DELETE = 13;
		//Sep5
		private const int TLB_REFRESH = 15;
		private const string REGKEY_USERSETTINGS = "Software\\LTA\\T2\\SystemAdmin";
		#endregion
		#region Controls

		private System.Windows.Forms.MainMenu mnuMain;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.ToolBar tlbMain;
		private System.Windows.Forms.ImageList imgMain;
		private System.Windows.Forms.MenuItem mnuEdit;
		private Tsort.Windows.TsortStatusBar stbMain;
		private System.Windows.Forms.ListView lsvMain;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.ToolBarButton btnRefresh;
		private System.Windows.Forms.ContextMenu mnuTRV;
		private System.Windows.Forms.MenuItem mnuTRVOpen;
		private System.Windows.Forms.MenuItem mnuTRVSep0;
		private System.Windows.Forms.MenuItem mnuTRVProps;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuView;
		private System.Windows.Forms.MenuItem mnuViewRefresh;
		private System.Windows.Forms.TabPage Terminal;
		private System.Windows.Forms.TabPage Business;
		private System.Windows.Forms.TreeView trvTerminal;
		private System.Windows.Forms.TreeView trvBusiness;
		private System.Windows.Forms.MenuItem mnuToolsSep1;
		private System.Windows.Forms.MenuItem mnuToolsSettings;
		private System.Windows.Forms.MenuItem mnuTools;
		private System.Windows.Forms.MenuItem mnuWin;
		private System.Windows.Forms.MenuItem mnuViewRefreshFilters;
		private System.Windows.Forms.MenuItem mnuToolsConsignee;
		private System.Windows.Forms.MenuItem mnuViewToolbar;
		private System.Windows.Forms.MenuItem mnuViewStatusBar;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mnuFileNew;
		private System.Windows.Forms.MenuItem mnuToolsUseWebService;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileSep2;
		private System.Windows.Forms.MenuItem mnuFileSep3;
		private System.Windows.Forms.MenuItem mnuFileSave;
		private System.Windows.Forms.MenuItem mnuFileClose;
		private System.Windows.Forms.MenuItem mnuFilePrint;
		private System.Windows.Forms.MenuItem mnuFilePreview;
		private System.Windows.Forms.MenuItem mnuFileSetup;
		private System.Windows.Forms.MenuItem mnuEditCut;
		private System.Windows.Forms.MenuItem mnuEditCopy;
		private System.Windows.Forms.MenuItem mnuEditPaste;
		private System.Windows.Forms.MenuItem mnuEditSep1;
		private System.Windows.Forms.MenuItem mnuEditDelete;
		private System.Windows.Forms.MenuItem mnuToolsDiagnostics;
		private System.Windows.Forms.MenuItem mnuEditSep2;
		private System.Windows.Forms.MenuItem mnuEditFind;
		private System.Windows.Forms.MenuItem mnuToolsTrace;
		private System.Windows.Forms.MenuItem mnuTooleSep2;
		private System.Windows.Forms.ToolBarButton btnNew;
		private System.Windows.Forms.ToolBarButton btnOpen;
		private System.Windows.Forms.ToolBarButton btnSave;
		private System.Windows.Forms.ToolBarButton btnPrint;
		private System.Windows.Forms.ToolBarButton btnPreview;
		private System.Windows.Forms.ToolBarButton btnCut;
		private System.Windows.Forms.ToolBarButton btnCopy;
		private System.Windows.Forms.ToolBarButton btnPaste;
		private System.Windows.Forms.ToolBarButton btnFind;
		private System.Windows.Forms.ToolBarButton btnSep1;
		private System.Windows.Forms.ToolBarButton btnSep2;
		private System.Windows.Forms.ToolBarButton btnSep3;
		private System.Windows.Forms.ToolBarButton btnSep4;
		private System.Windows.Forms.ToolBarButton btnSep5;
		private System.Windows.Forms.ToolBarButton btnDelete;
		private System.ComponentModel.IContainer components;
		
		#endregion
		//Events
		//Interface
		public frmMain() {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				readUserSettings();
				this.Text = "Argix Direct " + App.Product;
				#region Set menu identities and behavior
				this.mnuFileNew.Text = MNU_FILE_NEW;
				this.mnuFileOpen.Text = MNU_FILE_OPEN;
				this.mnuFileClose.Text = MNU_FILE_CLOSE;
				this.mnuFileSave.Text = MNU_FILE_SAVE;
				this.mnuFileSetup.Text = MNU_FILE_PRINT_SETTINGS;
				this.mnuFilePrint.Text = MNU_FILE_PRINT;
				this.mnuFilePreview.Text = MNU_FILE_PREVIEW;
				this.mnuFileExit.Text = MNU_FILE_EXIT;
				this.mnuEditCut.Text = MNU_EDIT_CUT;
				this.mnuEditCopy.Text = MNU_EDIT_COPY;
				this.mnuEditPaste.Text = MNU_EDIT_PASTE;
				this.mnuEditFind.Text = MNU_EDIT_FIND;
				this.mnuViewRefresh.Text = MNU_VIEW_REFRESH;
				this.mnuViewRefreshFilters.Text = MNU_VIEW_REFRESHFILTERS;
				this.mnuViewToolbar.Text = MNU_VIEW_TOOLBAR;
				this.mnuViewStatusBar.Text = MNU_VIEW_STATUSBAR;
				this.mnuToolsDiagnostics.Text = MNU_TOOLS_DIAGNOSTICS;
				this.mnuToolsTrace.Text = MNU_TOOLS_TRACE;
				this.mnuToolsConsignee.Text = MNU_TOOLS_CONSIGNEE;				
				this.mnuToolsSettings.Text = MNU_TOOLS_SETTINGS;
				this.mnuToolsUseWebService.Text = MNU_TOOLS_USEWEBSVC;
				this.mnuHelpAbout.Text = MNU_HELP_ABOUT;
				this.mnuTRVOpen.Text = MNU_TRV_EXPAND;
				this.mnuTRVProps.Text = MNU_TRV_PROPS;
				#endregion
				#region Splash screen; Diagnostics support
				Splash.ITEvent += new EventHandler(OnITEvent);
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				if(ITEventOn) {
					Splash.Close();
					this.mnuToolsDiagnostics.PerformClick();
				}
				#endregion
				#region Window Docking
				this.splitter.MinExtra = 96;
				this.splitter.MinSize = 96;
				this.tlbMain.Dock = DockStyle.Top;
				this.tabMain.Dock = DockStyle.Left;
				this.splitter.Dock = DockStyle.Left;
				this.lsvMain.Dock = DockStyle.Fill;
				this.stbMain.Dock = DockStyle.Bottom;
				this.Controls.AddRange(new Control[]{this.lsvMain, this.splitter, this.tabMain, this.tlbMain, this.stbMain});
				#endregion
				
				//Create data and UI services
				Mediator.TraceLogSPName = App.USP_TRACE;
				Mediator.EventLogName = App.EventLogName;
				bool useWebSvc = ConfigurationSettings.AppSettings["UseWebSvc"]=="true";
				this.mMediator = useWebSvc ? (Mediator)new WebSvcMediator() : (Mediator)new SQLMediator();
				this.mMediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
				this.mPageSettings = new PageSettings();
				this.mPageSettings.Landscape = true;
				this.mnuToolsUseWebService.Checked = useWebSvc;
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 3000);
				this.mTrace = new winTrace();
				
				//Set application configuration
				configApplication();
			}
			catch(Exception ex) { Splash.Close(); if(!frmMain.ITEventOn) throw new ApplicationException("Startup Failure", ex); }
		}
		~frmMain() {
			//Destructor
			this.mTrace.Close();
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.stbMain = new Tsort.Windows.TsortStatusBar();
			this.mnuTRV = new System.Windows.Forms.ContextMenu();
			this.mnuTRVOpen = new System.Windows.Forms.MenuItem();
			this.mnuTRVSep0 = new System.Windows.Forms.MenuItem();
			this.mnuTRVProps = new System.Windows.Forms.MenuItem();
			this.mnuMain = new System.Windows.Forms.MainMenu();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuFileNew = new System.Windows.Forms.MenuItem();
			this.mnuFileOpen = new System.Windows.Forms.MenuItem();
			this.mnuFileClose = new System.Windows.Forms.MenuItem();
			this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
			this.mnuFileSave = new System.Windows.Forms.MenuItem();
			this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
			this.mnuFileSetup = new System.Windows.Forms.MenuItem();
			this.mnuFilePrint = new System.Windows.Forms.MenuItem();
			this.mnuFilePreview = new System.Windows.Forms.MenuItem();
			this.mnuFileSep3 = new System.Windows.Forms.MenuItem();
			this.mnuFileExit = new System.Windows.Forms.MenuItem();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuEditCut = new System.Windows.Forms.MenuItem();
			this.mnuEditCopy = new System.Windows.Forms.MenuItem();
			this.mnuEditPaste = new System.Windows.Forms.MenuItem();
			this.mnuEditSep1 = new System.Windows.Forms.MenuItem();
			this.mnuEditDelete = new System.Windows.Forms.MenuItem();
			this.mnuEditSep2 = new System.Windows.Forms.MenuItem();
			this.mnuEditFind = new System.Windows.Forms.MenuItem();
			this.mnuView = new System.Windows.Forms.MenuItem();
			this.mnuViewRefresh = new System.Windows.Forms.MenuItem();
			this.mnuViewRefreshFilters = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuViewToolbar = new System.Windows.Forms.MenuItem();
			this.mnuViewStatusBar = new System.Windows.Forms.MenuItem();
			this.mnuTools = new System.Windows.Forms.MenuItem();
			this.mnuToolsDiagnostics = new System.Windows.Forms.MenuItem();
			this.mnuToolsTrace = new System.Windows.Forms.MenuItem();
			this.mnuToolsSep1 = new System.Windows.Forms.MenuItem();
			this.mnuToolsSettings = new System.Windows.Forms.MenuItem();
			this.mnuToolsConsignee = new System.Windows.Forms.MenuItem();
			this.mnuTooleSep2 = new System.Windows.Forms.MenuItem();
			this.mnuToolsUseWebService = new System.Windows.Forms.MenuItem();
			this.mnuWin = new System.Windows.Forms.MenuItem();
			this.mnuHelp = new System.Windows.Forms.MenuItem();
			this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
			this.tlbMain = new System.Windows.Forms.ToolBar();
			this.btnRefresh = new System.Windows.Forms.ToolBarButton();
			this.imgMain = new System.Windows.Forms.ImageList(this.components);
			this.trvTerminal = new System.Windows.Forms.TreeView();
			this.lsvMain = new System.Windows.Forms.ListView();
			this.splitter = new System.Windows.Forms.Splitter();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.Business = new System.Windows.Forms.TabPage();
			this.trvBusiness = new System.Windows.Forms.TreeView();
			this.Terminal = new System.Windows.Forms.TabPage();
			this.btnNew = new System.Windows.Forms.ToolBarButton();
			this.btnOpen = new System.Windows.Forms.ToolBarButton();
			this.btnSave = new System.Windows.Forms.ToolBarButton();
			this.btnPrint = new System.Windows.Forms.ToolBarButton();
			this.btnPreview = new System.Windows.Forms.ToolBarButton();
			this.btnCut = new System.Windows.Forms.ToolBarButton();
			this.btnCopy = new System.Windows.Forms.ToolBarButton();
			this.btnPaste = new System.Windows.Forms.ToolBarButton();
			this.btnFind = new System.Windows.Forms.ToolBarButton();
			this.btnSep1 = new System.Windows.Forms.ToolBarButton();
			this.btnSep2 = new System.Windows.Forms.ToolBarButton();
			this.btnSep3 = new System.Windows.Forms.ToolBarButton();
			this.btnSep4 = new System.Windows.Forms.ToolBarButton();
			this.btnSep5 = new System.Windows.Forms.ToolBarButton();
			this.btnDelete = new System.Windows.Forms.ToolBarButton();
			this.tabMain.SuspendLayout();
			this.Business.SuspendLayout();
			this.Terminal.SuspendLayout();
			this.SuspendLayout();
			// 
			// stbMain
			// 
			this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.stbMain.Location = new System.Drawing.Point(0, 337);
			this.stbMain.Name = "stbMain";
			this.stbMain.Size = new System.Drawing.Size(718, 24);
			this.stbMain.StatusText = "";
			this.stbMain.TabIndex = 17;
			this.stbMain.TerminalText = "Station #";
			// 
			// mnuTRV
			// 
			this.mnuTRV.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																				   this.mnuTRVOpen,
																				   this.mnuTRVSep0,
																				   this.mnuTRVProps});
			// 
			// mnuTRVOpen
			// 
			this.mnuTRVOpen.Enabled = false;
			this.mnuTRVOpen.Index = 0;
			this.mnuTRVOpen.Text = "&Expand";
			this.mnuTRVOpen.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuTRVSep0
			// 
			this.mnuTRVSep0.Index = 1;
			this.mnuTRVSep0.Text = "-";
			// 
			// mnuTRVProps
			// 
			this.mnuTRVProps.Enabled = false;
			this.mnuTRVProps.Index = 2;
			this.mnuTRVProps.Text = "P&roperties";
			this.mnuTRVProps.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuMain
			// 
			this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuFile,
																					this.mnuEdit,
																					this.mnuView,
																					this.mnuTools,
																					this.mnuWin,
																					this.mnuHelp});
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 0;
			this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuFileNew,
																					this.mnuFileOpen,
																					this.mnuFileClose,
																					this.mnuFileSep1,
																					this.mnuFileSave,
																					this.mnuFileSep2,
																					this.mnuFileSetup,
																					this.mnuFilePrint,
																					this.mnuFilePreview,
																					this.mnuFileSep3,
																					this.mnuFileExit});
			this.mnuFile.Text = "&File";
			// 
			// mnuFileNew
			// 
			this.mnuFileNew.Index = 0;
			this.mnuFileNew.Text = "&New...";
			this.mnuFileNew.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuFileOpen
			// 
			this.mnuFileOpen.DefaultItem = true;
			this.mnuFileOpen.Enabled = false;
			this.mnuFileOpen.Index = 1;
			this.mnuFileOpen.Text = "&Open...";
			this.mnuFileOpen.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuFileClose
			// 
			this.mnuFileClose.Index = 2;
			this.mnuFileClose.Text = "&Close";
			this.mnuFileClose.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuFileSep1
			// 
			this.mnuFileSep1.Index = 3;
			this.mnuFileSep1.Text = "-";
			// 
			// mnuFileSave
			// 
			this.mnuFileSave.Index = 4;
			this.mnuFileSave.Text = "&Save...";
			this.mnuFileSave.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuFileSep2
			// 
			this.mnuFileSep2.Index = 5;
			this.mnuFileSep2.Text = "-";
			// 
			// mnuFileSetup
			// 
			this.mnuFileSetup.Index = 6;
			this.mnuFileSetup.Text = "Page Set&up...";
			this.mnuFileSetup.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuFilePrint
			// 
			this.mnuFilePrint.Index = 7;
			this.mnuFilePrint.Text = "&Print...";
			this.mnuFilePrint.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuFilePreview
			// 
			this.mnuFilePreview.Index = 8;
			this.mnuFilePreview.Text = "Pre&view...";
			this.mnuFilePreview.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuFileSep3
			// 
			this.mnuFileSep3.Index = 9;
			this.mnuFileSep3.Text = "-";
			// 
			// mnuFileExit
			// 
			this.mnuFileExit.Index = 10;
			this.mnuFileExit.Text = "E&xit";
			this.mnuFileExit.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuEdit
			// 
			this.mnuEdit.Index = 1;
			this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuEditCut,
																					this.mnuEditCopy,
																					this.mnuEditPaste,
																					this.mnuEditSep1,
																					this.mnuEditDelete,
																					this.mnuEditSep2,
																					this.mnuEditFind});
			this.mnuEdit.Text = "&Edit";
			// 
			// mnuEditCut
			// 
			this.mnuEditCut.Index = 0;
			this.mnuEditCut.Text = "Cut";
			this.mnuEditCut.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuEditCopy
			// 
			this.mnuEditCopy.Index = 1;
			this.mnuEditCopy.Text = "Copy";
			this.mnuEditCopy.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuEditPaste
			// 
			this.mnuEditPaste.Index = 2;
			this.mnuEditPaste.Text = "Paste";
			this.mnuEditPaste.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuEditSep1
			// 
			this.mnuEditSep1.Index = 3;
			this.mnuEditSep1.Text = "-";
			// 
			// mnuEditDelete
			// 
			this.mnuEditDelete.Index = 4;
			this.mnuEditDelete.Text = "Delete";
			this.mnuEditDelete.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuEditSep2
			// 
			this.mnuEditSep2.Index = 5;
			this.mnuEditSep2.Text = "-";
			// 
			// mnuEditFind
			// 
			this.mnuEditFind.Index = 6;
			this.mnuEditFind.Text = "Find";
			this.mnuEditFind.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuView
			// 
			this.mnuView.Index = 2;
			this.mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuViewRefresh,
																					this.mnuViewRefreshFilters,
																					this.menuItem3,
																					this.mnuViewToolbar,
																					this.mnuViewStatusBar});
			this.mnuView.Text = "&View";
			// 
			// mnuViewRefresh
			// 
			this.mnuViewRefresh.Index = 0;
			this.mnuViewRefresh.Text = "&Refresh";
			this.mnuViewRefresh.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuViewRefreshFilters
			// 
			this.mnuViewRefreshFilters.Index = 1;
			this.mnuViewRefreshFilters.Text = "Refresh &Filters";
			this.mnuViewRefreshFilters.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// mnuViewToolbar
			// 
			this.mnuViewToolbar.Index = 3;
			this.mnuViewToolbar.Text = "Toolbar";
			this.mnuViewToolbar.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuViewStatusBar
			// 
			this.mnuViewStatusBar.Index = 4;
			this.mnuViewStatusBar.Text = "StatusBar";
			this.mnuViewStatusBar.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuTools
			// 
			this.mnuTools.Index = 3;
			this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.mnuToolsDiagnostics,
																					 this.mnuToolsTrace,
																					 this.mnuToolsSep1,
																					 this.mnuToolsSettings,
																					 this.mnuToolsConsignee,
																					 this.mnuTooleSep2,
																					 this.mnuToolsUseWebService});
			this.mnuTools.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
			this.mnuTools.Text = "&Tools";
			// 
			// mnuToolsDiagnostics
			// 
			this.mnuToolsDiagnostics.Index = 0;
			this.mnuToolsDiagnostics.Text = "Diagnostics";
			this.mnuToolsDiagnostics.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuToolsTrace
			// 
			this.mnuToolsTrace.Index = 1;
			this.mnuToolsTrace.Text = "Trace";
			this.mnuToolsTrace.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuToolsSep1
			// 
			this.mnuToolsSep1.Index = 2;
			this.mnuToolsSep1.Text = "-";
			// 
			// mnuToolsSettings
			// 
			this.mnuToolsSettings.Index = 3;
			this.mnuToolsSettings.MergeOrder = 3;
			this.mnuToolsSettings.Text = "&Settings";
			this.mnuToolsSettings.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuToolsConsignee
			// 
			this.mnuToolsConsignee.Index = 4;
			this.mnuToolsConsignee.Text = "Consignee &DeliveryLocationDS";
			this.mnuToolsConsignee.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuTooleSep2
			// 
			this.mnuTooleSep2.Index = 5;
			this.mnuTooleSep2.Text = "-";
			// 
			// mnuToolsUseWebService
			// 
			this.mnuToolsUseWebService.Index = 6;
			this.mnuToolsUseWebService.Text = "Use Web Service";
			this.mnuToolsUseWebService.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuWin
			// 
			this.mnuWin.Index = 4;
			this.mnuWin.Text = "&Window";
			// 
			// mnuHelp
			// 
			this.mnuHelp.Index = 5;
			this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuHelpAbout});
			this.mnuHelp.Text = "&Help";
			// 
			// mnuHelpAbout
			// 
			this.mnuHelpAbout.Index = 0;
			this.mnuHelpAbout.Text = "&About System Admin";
			this.mnuHelpAbout.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// tlbMain
			// 
			this.tlbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.tlbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.btnNew,
																					   this.btnOpen,
																					   this.btnSep1,
																					   this.btnSave,
																					   this.btnSep2,
																					   this.btnPrint,
																					   this.btnPreview,
																					   this.btnSep3,
																					   this.btnCut,
																					   this.btnCopy,
																					   this.btnPaste,
																					   this.btnSep4,
																					   this.btnFind,
																					   this.btnDelete,
																					   this.btnSep5,
																					   this.btnRefresh});
			this.tlbMain.ButtonSize = new System.Drawing.Size(16, 16);
			this.tlbMain.DropDownArrows = true;
			this.tlbMain.ImageList = this.imgMain;
			this.tlbMain.Location = new System.Drawing.Point(0, 0);
			this.tlbMain.Name = "tlbMain";
			this.tlbMain.ShowToolTips = true;
			this.tlbMain.Size = new System.Drawing.Size(718, 28);
			this.tlbMain.TabIndex = 89;
			this.tlbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.OnToolbarButtonClick);
			// 
			// btnRefresh
			// 
			this.btnRefresh.ImageIndex = 13;
			this.btnRefresh.ToolTipText = "Refresh";
			// 
			// imgMain
			// 
			this.imgMain.ImageSize = new System.Drawing.Size(16, 16);
			this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
			this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// trvTerminal
			// 
			this.trvTerminal.ContextMenu = this.mnuTRV;
			this.trvTerminal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvTerminal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.trvTerminal.ImageIndex = -1;
			this.trvTerminal.Location = new System.Drawing.Point(0, 0);
			this.trvTerminal.Name = "trvTerminal";
			this.trvTerminal.SelectedImageIndex = -1;
			this.trvTerminal.Size = new System.Drawing.Size(212, 283);
			this.trvTerminal.TabIndex = 90;
			this.trvTerminal.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvAfterExpand);
			this.trvTerminal.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.trvAfterCollapse);
			this.trvTerminal.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvAfterSelect);
			// 
			// lsvMain
			// 
			this.lsvMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvMain.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lsvMain.LabelWrap = false;
			this.lsvMain.Location = new System.Drawing.Point(223, 28);
			this.lsvMain.MultiSelect = false;
			this.lsvMain.Name = "lsvMain";
			this.lsvMain.Size = new System.Drawing.Size(495, 309);
			this.lsvMain.TabIndex = 91;
			this.lsvMain.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvColumnClick);
			// 
			// splitter
			// 
			this.splitter.Location = new System.Drawing.Point(220, 28);
			this.splitter.Name = "splitter";
			this.splitter.Size = new System.Drawing.Size(3, 309);
			this.splitter.TabIndex = 92;
			this.splitter.TabStop = false;
			// 
			// tabMain
			// 
			this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabMain.Controls.Add(this.Business);
			this.tabMain.Controls.Add(this.Terminal);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Left;
			this.tabMain.Location = new System.Drawing.Point(0, 28);
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.ShowToolTips = true;
			this.tabMain.Size = new System.Drawing.Size(220, 309);
			this.tabMain.TabIndex = 93;
			this.tabMain.TabStop = false;
			this.tabMain.SelectedIndexChanged += new System.EventHandler(this.OnTabChanged);
			// 
			// Business
			// 
			this.Business.Controls.Add(this.trvBusiness);
			this.Business.Location = new System.Drawing.Point(4, 4);
			this.Business.Name = "Business";
			this.Business.Size = new System.Drawing.Size(212, 283);
			this.Business.TabIndex = 1;
			this.Business.Text = "Business Admin";
			this.Business.ToolTipText = "Business Administration Management";
			// 
			// trvBusiness
			// 
			this.trvBusiness.ContextMenu = this.mnuTRV;
			this.trvBusiness.Dock = System.Windows.Forms.DockStyle.Fill;
			this.trvBusiness.ForeColor = System.Drawing.SystemColors.ControlText;
			this.trvBusiness.ImageIndex = -1;
			this.trvBusiness.Location = new System.Drawing.Point(0, 0);
			this.trvBusiness.Name = "trvBusiness";
			this.trvBusiness.SelectedImageIndex = -1;
			this.trvBusiness.Size = new System.Drawing.Size(212, 283);
			this.trvBusiness.TabIndex = 91;
			this.trvBusiness.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.trvAfterExpand);
			this.trvBusiness.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.trvAfterCollapse);
			this.trvBusiness.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.trvAfterSelect);
			// 
			// Terminal
			// 
			this.Terminal.BackColor = System.Drawing.Color.Gainsboro;
			this.Terminal.Controls.Add(this.trvTerminal);
			this.Terminal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Terminal.Location = new System.Drawing.Point(4, 4);
			this.Terminal.Name = "Terminal";
			this.Terminal.Size = new System.Drawing.Size(212, 283);
			this.Terminal.TabIndex = 0;
			this.Terminal.Text = "Terminal Mgt";
			this.Terminal.ToolTipText = "Terminal Management Admnistration";
			// 
			// btnNew
			// 
			this.btnNew.ImageIndex = 3;
			this.btnNew.ToolTipText = "New";
			// 
			// btnOpen
			// 
			this.btnOpen.ImageIndex = 4;
			this.btnOpen.ToolTipText = "Open";
			// 
			// btnSave
			// 
			this.btnSave.ImageIndex = 5;
			this.btnSave.ToolTipText = "Save";
			// 
			// btnPrint
			// 
			this.btnPrint.ImageIndex = 6;
			this.btnPrint.ToolTipText = "Print";
			// 
			// btnPreview
			// 
			this.btnPreview.ImageIndex = 7;
			this.btnPreview.ToolTipText = "Preview";
			// 
			// btnCut
			// 
			this.btnCut.ImageIndex = 8;
			this.btnCut.ToolTipText = "Cut";
			// 
			// btnCopy
			// 
			this.btnCopy.ImageIndex = 9;
			this.btnCopy.ToolTipText = "Copy";
			// 
			// btnPaste
			// 
			this.btnPaste.ImageIndex = 10;
			this.btnPaste.ToolTipText = "Paste";
			// 
			// btnFind
			// 
			this.btnFind.ImageIndex = 11;
			this.btnFind.ToolTipText = "Find";
			// 
			// btnSep1
			// 
			this.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnSep2
			// 
			this.btnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnSep3
			// 
			this.btnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnSep4
			// 
			this.btnSep4.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnSep5
			// 
			this.btnSep5.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			// 
			// btnDelete
			// 
			this.btnDelete.ImageIndex = 12;
			this.btnDelete.ToolTipText = "Delete";
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(718, 361);
			this.Controls.Add(this.lsvMain);
			this.Controls.Add(this.splitter);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.tlbMain);
			this.Controls.Add(this.stbMain);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mnuMain;
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "System Administration";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Closed += new System.EventHandler(this.OnFormClosed);
			this.tabMain.ResumeLayout(false);
			this.Business.ResumeLayout(false);
			this.Terminal.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		[STAThread]
		static void Main() {
			//The main entry point for the application
			try {
				//Start app
				Process appInstance = AppServices.RunningInstance("Argix Direct " + App.Product);
				if(appInstance==null) 
					Application.Run(new frmMain());
				else {
					MessageBox.Show("Another instance of this application is already running.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
					AppServices.ShowWindow(appInstance.MainWindowHandle, 1);
					AppServices.SetForegroundWindow(appInstance.MainWindowHandle);
				}
			}
			catch(Exception ex) {
				MessageBox.Show("FATAL ERROR\n\n" + ex.ToString() + "\n\n Application will be closed. Please contact the IT department for help.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
				TsortTrace.WriteLine(new TraceMessage(ex.ToString(), App.EventLogName, LogLevel.Error, "Startup Failure"));
				Application.Exit();
			}		
		}
		private static void OnITEvent(object o, EventArgs e) { ITEventOn = true; }
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				Splash.Close();
				this.Visible = true;
				Application.DoEvents();
				#region Set user preferences
				this.Height = (int) (Screen.PrimaryScreen.WorkingArea.Size.Height * 0.9);
				this.Width = (int)(Screen.PrimaryScreen.WorkingArea.Size.Width * 0.9);
				this.Left = Screen.PrimaryScreen.WorkingArea.Left  + (int)(Screen.PrimaryScreen.WorkingArea.Size.Width * 0.05); 
				this.Top = Screen.PrimaryScreen.WorkingArea.Top  + (int)(Screen.PrimaryScreen.WorkingArea.Size.Height * 0.05); 
				this.tlbMain.Visible = this.mnuViewToolbar.Checked = true;
				this.stbMain.Visible = this.mnuViewStatusBar.Checked = true;
				Application.DoEvents();
				#endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				//this.mToolTip.SetToolTip(this.txtSearch, "Search for a trip.");
				#endregion
				
				//Set control defaults
				#region Init treeview and listview
				this.trvBusiness.Indent = 24;
				this.trvBusiness.HideSelection = false;
				this.trvBusiness.ImageList = this.imgMain;
				this.trvBusiness.Scrollable = true;
				this.trvTerminal.Indent = 24;
				this.trvTerminal.HideSelection = false;
				this.trvTerminal.ImageList = this.imgMain;
				this.trvTerminal.Scrollable = true;
				this.lsvMain.HeaderStyle = ColumnHeaderStyle.Clickable;
				this.lsvMain.FullRowSelect = false;
				this.lsvMain.HideSelection = false;
				this.lsvMain.LargeImageList = this.imgMain;
				this.lsvMain.SmallImageList = this.imgMain;
				this.lsvMain.View = View.Details;
				this.lsvMain.Sorting = SortOrder.None;
				#endregion
				this.stbMain.SetTerminalPanel(this.mEntTerminal.TerminalID.ToString(), this.mEntTerminal.Description);
				this.tabMain.SelectedTab = this.tabMain.TabPages[1];
				this.mnuViewRefresh.PerformClick();
				this.tabMain.SelectedTab = this.tabMain.TabPages[0];
				this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { reportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for form size changes
			try {
				if(this.WindowState != FormWindowState.Minimized) 
					this.mTrace.Hide();
			} 
			catch(Exception) { }
		}
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for after form is closed; save user preferences
			this.mMessageMgr.AddMessage("Saving settings...");
			saveUserSettings();
			TsortTrace.WriteLine(new TraceMessage(App.Version, App.EventLogName, LogLevel.Information, "App Stopped"));
		}
		private void OnTabChanged(object sender, System.EventArgs e) {
			//Event handler for tab change
			IItem item = null;
			try {
				//Determine current item for this tab and open in listview
				switch(this.tabMain.SelectedTab.Name) {
					case "Business": item = (IItem)this.trvBusiness.SelectedNode; break;
					case "Terminal": item = (IItem)this.trvTerminal.SelectedNode; break;
				}
				openItem(item);
			} 
			catch(Exception ex) { reportError(ex, true, LogLevel.Warning); }
		}
		#region AdminNode::IItem Services: OnOpenAdminItem(), OnAdminItemListChanged(), openItem()
		private void OnOpenAdminItem(object sender, System.EventArgs e) {
			//Event handler to receive notification from an AdminNode object that
			//it requests to open a selected child AdminNode in the listview
			try {
				//Select node in treeview (selection opens node into listview)
				TreeNode node = (TreeNode)sender;
				switch(this.tabMain.SelectedTab.Name) {
					case "Business": this.trvBusiness.SelectedNode = node; break;
					case "Terminal": this.trvTerminal.SelectedNode = node; break;
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnAdminItemListChanged(object sender, System.EventArgs e) {
			//Event handler to receive notification from an AdminNode object that
			//it has a change in its list of child AdminNode objects
			try {
				//Handle list updates- update node and re-select into listview
				INode node = (INode)sender;
				node.loadChildNodes();
				openItem((IItem)sender);
			}
			catch(Exception ex) { reportError(ex); }
		}		
		private void openItem(IItem item) {
			//Open item in listview
			if(item==null)
				return;
			//Clear current event handlers
			if(this.mItem!=null) {
				this.lsvMain.SelectedIndexChanged -= new System.EventHandler(this.mItem.OnItemSelected);
				this.lsvMain.DoubleClick -= new System.EventHandler(this.mItem.OnItemDoubleClicked);
			}
			
			//Set listview column headers, listitems, and services menu
			this.lsvMain.Sorting = SortOrder.None;
			this.lsvMain.Columns.Clear();
			this.lsvMain.Items.Clear();
			this.lsvMain.ContextMenu = item.menu();
			this.lsvMain.Columns.AddRange(item.header());
			this.lsvMain.Items.AddRange(item.list());
			if(this.lsvMain.Items.Count>0)
				this.lsvMain.Items[0].Selected = true;
			
			//Set new event handlers; retain current IItem
			this.mItem = item;
			this.lsvMain.SelectedIndexChanged += new System.EventHandler(this.mItem.OnItemSelected);
			this.lsvMain.DoubleClick += new System.EventHandler(this.mItem.OnItemDoubleClicked);
			
			//Set event handler for item open and list updates
			this.mItem.OpenSelectedItem += new System.EventHandler(this.OnOpenAdminItem);
			this.mItem.ListChanged += new System.EventHandler(this.OnAdminItemListChanged);
		}
		#endregion
		#region Treeview Services: trvAfterCollapse(), trvAfterExpand(), trvAfterSelect()
		private void trvAfterCollapse(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node collapsed - child nodes need to unload [stale] data
			try {
				INode node = (INode)e.Node;
				node.collapseNode();
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void trvAfterExpand(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node expanded - child nodes need to load [fresh] data
			try {
				INode node = (INode)e.Node;
				node.expandNode();
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void trvAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node needs to display in the listview
			try {
				openItem((IItem)e.Node);
			}
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Listview Services: lsvColumnClick()
		private void lsvColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e) {
			//Response to listview column click event
			try {
				//Sort this column
				this.lsvMain.Sorting = (this.lsvMain.Sorting==SortOrder.Descending) ? SortOrder.Ascending : this.lsvMain.Sorting = SortOrder.Descending;
				this.lsvMain.Sort();
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region User Services: OnMenuClick(), OnToolbarButtonClick()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu item clicked-apply selected service
			AdminNode node = null;
			DialogResult res;
			dlgLogin login=null;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_FILE_NEW:				break;
					case MNU_FILE_OPEN:				break;
					case MNU_FILE_CLOSE:				break;
					case MNU_FILE_SAVE:				break;
					case MNU_FILE_SAVEAS:			break;
					case MNU_FILE_PRINT_SETTINGS:	UltraGridPrinter.PageSettings(); break;
					case MNU_FILE_PRINT:			UltraGridPrinter.Print(null, "", true); break;
					case MNU_FILE_PREVIEW:			UltraGridPrinter.PrintPreview(null, ""); break;
					case MNU_FILE_EXIT:				this.Close(); break;
					case MNU_EDIT_CUT:				break;
					case MNU_EDIT_COPY:				break;
					case MNU_EDIT_PASTE:			break;
					case MNU_EDIT_FIND:				break;
					case MNU_EDIT_DELETE:			break;
					case MNU_VIEW_REFRESH:
						//
						this.Cursor = Cursors.WaitCursor;
						switch(this.tabMain.SelectedTab.Name) {
							case "Business":
								this.trvBusiness.Nodes.Clear();
								this.mItem = null;
								node = new BusinessAdminRootNode("Argix", 0, 1);
								this.trvBusiness.Nodes.Add(node);
								node.loadChildNodes();
								this.trvBusiness.SelectedNode = this.trvBusiness.Nodes[0];
								this.trvBusiness.Nodes[0].Expand();
								break;
							case "Terminal":
								//Load treeview root node
								this.trvTerminal.Nodes.Clear();
								this.mItem = null;
								node = new TerminalMgtRootNode("Argix", 0, 1);
								this.trvTerminal.Nodes.Add(node);
								node.loadChildNodes();
								this.trvTerminal.SelectedNode = this.trvTerminal.Nodes[0];
								this.trvTerminal.Nodes[0].Expand();
								break;
						}
						break;
					case MNU_VIEW_REFRESHFILTERS:	break;
					case MNU_VIEW_TOOLBAR:		this.tlbMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case MNU_VIEW_STATUSBAR:	this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
					case MNU_TOOLS_FIND: break;
					case MNU_TOOLS_CONSIGNEE:
						dlgConsigneeOverride dlgConsOver = null;
						dlgConsOver = new dlgConsigneeOverride();
						res = dlgConsOver.ShowDialog(this);
						if(res==DialogResult.OK) {
							//Request a new pickup appointment
							MessageBox.Show(this, "Just a test", this.Text, MessageBoxButtons.OK);
							this.mnuViewRefresh.PerformClick();
						}
						break;
					case MNU_TOOLS_SETTINGS:		break;
					case MNU_TOOLS_DIAGNOSTICS:
						#region Diagnostics Mode
						login = new dlgLogin("argix" + DateTime.Today.DayOfYear);
						login.StartPosition = FormStartPosition.CenterScreen;
						login.ValidateEntry();
						if(login.IsValid) {
							dlgData dlg = new dlgData(Assembly.GetExecutingAssembly(), App.Product);
							dlg.ShowDialog(this);
						}
						#endregion
						break;
					case MNU_TOOLS_TRACE:		
						login = new dlgLogin(this.mMISPassword);
						login.ValidateEntry();
						if(login.IsValid) this.mTrace.Show();
						break;
					case MNU_TOOLS_USEWEBSVC: 
						login = new dlgLogin(this.mMISPassword);
						login.ValidateEntry();
						if(login.IsValid) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Resetting the application configuration...");
							this.mEntTerminal = null;
							this.mMediator = null;
							Application.DoEvents();
							this.mnuToolsUseWebService.Checked = (!this.mnuToolsUseWebService.Checked);
							if(this.mnuToolsUseWebService.Checked)
								this.mMediator = new WebSvcMediator();
							else 
								this.mMediator = new SQLMediator();
							this.mMediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
							TsortTrace.WriteLine(new TraceMessage("Use web service: " + this.mnuToolsUseWebService.Checked.ToString(), App.EventLogName, LogLevel.Information, "frmMain::OnMenuClick()", "Configuration", "Change"));
							configApplication();
							this.stbMain.SetTerminalPanel(this.mEntTerminal.TerminalID.ToString(), this.mEntTerminal.Description);
							this.mnuViewRefresh.PerformClick();
						}
						break;
					case MNU_HELP_ABOUT:
						dlgAbout about = new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration);
						about.ShowDialog(this);
						break;
				}
			}
			catch(Exception ex) { reportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnToolbarButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//Toolbar handler
			try {
				switch(tlbMain.Buttons.IndexOf(e.Button)) {
					case TLB_NEW:		break;
					case TLB_OPEN:		this.mnuFileOpen.PerformClick(); break;
					case TLB_SAVE:		break;
					case TLB_PRINT:		UltraGridPrinter.Print(null, "", false); break;
					case TLB_PREVIEW:	this.mnuFilePreview.PerformClick(); break;
					case TLB_CUT:		this.mnuEditCut.PerformClick(); break;
					case TLB_COPY:		this.mnuEditCopy.PerformClick(); break;
					case TLB_PASTE:		this.mnuEditPaste.PerformClick(); break;
					case TLB_SEARCH:	this.mnuEditFind.PerformClick(); break;
					case TLB_DELETE:	break;
					case TLB_REFRESH:	this.mnuViewRefresh.PerformClick(); break;
				}
			}
			catch(Exception ex) { reportError(ex, false, LogLevel.Error); }
		}
		#endregion
		#region Local Services: configApplication(), setUserServices(), reportMessage(), reportError(), buildHelpMenu(), OnHelpMenuClick(), OnDataStatusUpdate(), OnErrorMessage()
		private void configApplication() {
			try {
				//Query application configuration
				DataSet configDS = this.mMediator.ReadConfig(App.USP_CONFIGURATION, App.TBL_CONFIGURATION, App.Product, Environment.MachineName);
				Config config = new Config(configDS);
				
				//Create event log and database trace listeners
				try {
					LogLevel level = (LogLevel)config.GetValueAsInteger(App.KEY_TRACELEVEL);
					TsortTrace.AddListener(new ArgixEventLogTraceListener(level, App.EventLogName));
					TsortTrace.AddListener(new DBTraceListener(level, (ITraceDB)this.mMediator));
				}
				catch {
					TsortTrace.AddListener(new ArgixEventLogTraceListener(LogLevel.Debug, App.EventLogName));
					TsortTrace.AddListener(new DBTraceListener(LogLevel.Debug, (ITraceDB)this.mMediator));
					TsortTrace.WriteLine(new TraceMessage("Log level not found; setting log levels to Debug.", App.EventLogName, LogLevel.Warning, "frmMain::configApplication()", "Configuration", "Log Level"));
				}
				TsortTrace.WriteLine(new TraceMessage(App.Version, App.EventLogName, LogLevel.Information, "App Started"));
				
				//Create business objects with configuration values
				EnterpriseFactory.Mediator = this.mMediator;
				FreightFactory.Mediator = this.mMediator;
				TransportationFactory.Mediator = this.mMediator;
				this.mEntTerminal = new EnterpriseTerminal(this.mMediator);
				this.mReadOnly = config.GetValueAsBoolean(App.KEY_READONLY);
				this.mMISPassword = config.GetValue(App.KEY_MISPASSWORD);
			}
			catch(ApplicationException ex) { throw ex; } 
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services depending upon an item selected in the grid
			try {				
				//Set main menu and context menu states
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
				this.mnuFileClose.Enabled = false;
				this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
				this.mnuFilePrint.Enabled = this.btnPrint.Enabled = false;
				this.mnuFilePreview.Enabled = this.btnPreview.Enabled = false;
				this.mnuFileExit.Enabled = true;
				this.mnuEditCut.Enabled = this.btnCut.Enabled = false;
				this.mnuEditCopy.Enabled = this.btnCopy.Enabled = false;
				this.mnuEditPaste.Enabled = this.btnPaste.Enabled = false;
				this.mnuEditFind.Enabled = this.btnFind.Enabled = false;
				this.mnuEditDelete.Enabled = false;
				this.mnuViewRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuToolsSettings.Enabled = false;
				this.mnuToolsConsignee.Enabled = false;
				this.mnuToolsDiagnostics.Enabled = frmMain.ITEventOn;
				this.mnuToolsTrace.Enabled = true;
				this.mnuToolsUseWebService.Enabled = true;
				this.mnuHelpAbout.Enabled = true;
			}
			catch(Exception ex) { reportError(ex, false, LogLevel.Error); }
			finally { Application.DoEvents(); }
		}
		private void reportError(Exception ex) { reportError(ex, true, LogLevel.None); }
		private void reportError(Exception ex, bool displayMessage) { reportError(ex, displayMessage, LogLevel.None); }
		private void reportError(Exception ex, bool displayMessage, LogLevel level) {
			//Report an exception to the user
			try {
				string src = (ex.Source != null) ? ex.Source + "-\n" : "";
				string msg = src + ex.Message;
				if(ex.InnerException != null) {
					if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
					msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
				}
				if(displayMessage) 
					MessageBox.Show(this, msg, App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
				if(level != LogLevel.None) 
					TsortTrace.WriteLine(new TraceMessage(ex.ToString(), App.EventLogName, level));
			}
			catch(Exception) { }
		}
		private void buildHelpMenu() {
			//Build dynamic help menu from configuration file
			try {
				//Read help menu configuration from app.config
				this.mHelpItems = (NameValueCollection)ConfigurationSettings.GetConfig("menu/help");				
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
			catch(Exception ex) { reportError(ex, false, LogLevel.Warning); }
		}
		private void OnHelpMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for configurable help menu items
			try {
				MenuItem mnu = (MenuItem)sender;
				Help.ShowHelp(this, this.mHelpItems.GetValues(mnu.Text)[0]);
			}
			catch(Exception ex) { reportError(ex, false, LogLevel.Warning); }
		}
		public void OnDataStatusUpdate(object sender, DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
		private void OnErrorMessage(object source, ErrorEventArgs e) {
			//Event handler for error messages from dialogs
			reportError(e.Exception, true, LogLevel.Error);
		}
		#endregion
		#region User Preferences: readUserSettings(), saveUserSettings()
		private void readUserSettings() {
			//Read user settings from the registry
			RegistryKey key = null;
			object oValue = null;
			try {
				//Use current user key
				key = Registry.CurrentUser.CreateSubKey(REGKEY_USERSETTINGS);
				if(key!=null) {
					oValue = key.GetValue("WindPos");
					key.Close();
				}
			}
			catch(Exception ex) { reportError(ex, false, LogLevel.Warning); }
		}
		private void saveUserSettings() {
			//Save user settings from the registry
			RegistryKey key = null;
			string entry = "";
			try {
				//Use current user key
				key = Registry.CurrentUser.CreateSubKey(REGKEY_USERSETTINGS);
				if(key!=null) {
					entry = this.WindowState.ToString() + "," + this.Left.ToString() + "," + this.Top.ToString() + "," + this.Width.ToString() + "," + this.Height.ToString();
					key.SetValue("WindPos", entry);
					key.SetValue("SplitterPos", this.splitter.Left.ToString());
					key.Close();
				}			
			}
			catch(Exception ex) { reportError(ex, false, LogLevel.Warning); }
		}
		#endregion
	}
}
