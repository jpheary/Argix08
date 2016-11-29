//	File:	main.cs
//	Author:	J. Heary
//	Date:	01/07/09
//	Desc:	Application main MDI window.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Argix.Support;
using Argix.Windows;

namespace Argix.Customers {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
        private NotifyIcon mTrayIcon=null;
		
		#region Constants
		private const string MNU_FILE_NEW = "&New Issue...";
		private const string MNU_FILE_OPEN = "&Open...";
        private const string MNU_FILE_ACTIONNEW = "New &Action...";
        private const string MNU_FILE_SAVE = "&Save...";
		private const string MNU_FILE_SAVEAS = "Save &As...";
		private const string MNU_FILE_PRINT_SETTINGS = "Page Set&up...";
		private const string MNU_FILE_PRINT = "&Print...";
		private const string MNU_FILE_PREVIEW = "Print Pre&view...";
		private const string MNU_FILE_EXIT = "E&xit";
		private const string MNU_EDIT_CUT = "Cu&t";
		private const string MNU_EDIT_COPY = "&Copy";
		private const string MNU_EDIT_PASTE = "&Paste";
		private const string MNU_EDIT_FIND = "&Search...";
        private const string MNU_VIEW_REFRESH = "&Refresh";
        private const string MNU_VIEW_REFRESHCACHE = "Refresh &Cache";
        private const string MNU_VIEW_NAVOPEN = "&Current Issues";
        private const string MNU_VIEW_NAVHISTORY = "&History";
        private const string MNU_VIEW_FONT = "&Font...";
        private const string MNU_VIEW_TOOLBAR = "&Toolbar";
		private const string MNU_VIEW_STATUSBAR = "Status&Bar";
        private const string MNU_TOOLS_CONFIG = "&Configuration";
		private const string MNU_TOOLS_TRACE = "&Trace";
        private const string MNU_HELP_ABOUT = "&About Issue Management...";
				
		private const int KEYSTATE_SHIFT = 5;
		private const int KEYSTATE_CTL = 9;
        private const string MNU_ICON_SHOWNEWISSUEALERT = "Show New Issue Desktop Alert";
        private const string MNU_ICON_HIDEWHENMINIMIZED = "Hide When Minimized";
        private const string MNU_ICON_SHOW = "Open Issue Mgt";

		#endregion
		#region Controls

        private System.Windows.Forms.ToolBarButton btnEdit;
        private Argix.Windows.ArgixStatusBar stbMain;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuToolsTrace;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripSeparator mnuEditSep1;
        private ToolStripMenuItem mnuEditSearch;
        private Splitter splitterH;
        private ToolStripMenuItem mnuSave;
        private ToolStripMenuItem mnuFileActionNew;
        private ToolStripSeparator mnuFileSep4;
        private IssueExplorer mExplorer;
        private ToolStripMenuItem mnuWin;
        private ToolStripMenuItem mnuWinCascade;
        private ToolStripMenuItem mnuWinTileH;
        private ToolStripMenuItem mnuWinTileV;
        private ToolStripSeparator mnuWindowSep1;
        private ToolStripMenuItem mnuViewFont;
        private ToolStripSeparator mnuViewSep2;
        private IssueInspector mInspector;
        private ToolStripMenuItem mnuViewShowAlert;
        private ToolStripMenuItem mnuViewHideWhenMin;
        private ToolStripSeparator mnuViewSep3;
        private Panel pnlToolbox;
        private TabControl tabToolbox;
        private TabPage tabSearch;
        private Panel pnlToolboxTitlebar;
        private Label lblPin;
        private Label lblToolbox;
        private Splitter splitterV;
        private System.Windows.Forms.Timer tmrAutoHide;
        private TextBox txtZone;
        private Label _lblZone;
        private TextBox txtCoordinator;
        private Label _lblCoordinator;
        private TextBox txtOriginator;
        private Label _lblOriginator;
        private TextBox txtContact;
        private Label _lblContact;
        private TextBox txtSubject;
        private Label _lblSubject;
        private TextBox txtReceived;
        private Label _lblReceived;
        private TextBox txtAction;
        private Label _lblAction;
        private TextBox txtType;
        private Label _lblType;
        private TextBox txtCompany;
        private Label _lblCompany;
        private TextBox txtAgent;
        private Label _lblAgent;
        private TextBox txtStore;
        private Label _lblStore;
        private Button btnSearch;
        private Button btnReset;
        private ImageList imlMain;
        private MenuStrip mnuMain;
		
		#endregion
		//Interface
		public frmMain() {
			try {
				InitializeComponent();
                InitializeToolbox();
				this.Text = "Argix Direct " + App.Product;
				#region Menu identities
				this.mnuFileNew.Text = MNU_FILE_NEW;
				this.mnuFileOpen.Text = MNU_FILE_OPEN;
                this.mnuFileActionNew.Text = MNU_FILE_ACTIONNEW;
                this.mnuSave.Text = MNU_FILE_SAVE;
				this.mnuFileSaveAs.Text = MNU_FILE_SAVEAS;
				this.mnuFileSetup.Text = MNU_FILE_PRINT_SETTINGS;
				this.mnuFilePrint.Text = MNU_FILE_PRINT;
				this.mnuFilePreview.Text = MNU_FILE_PREVIEW;
				this.mnuFileExit.Text = MNU_FILE_EXIT;
				this.mnuEditCut.Text = MNU_EDIT_CUT;
				this.mnuEditCopy.Text = MNU_EDIT_COPY;
				this.mnuEditPaste.Text = MNU_EDIT_PASTE;
				this.mnuEditSearch.Text = MNU_EDIT_FIND;
				this.mnuViewRefresh.Text = MNU_VIEW_REFRESH;
                this.mnuViewFont.Text = MNU_VIEW_FONT;
                this.mnuViewShowAlert.Text = MNU_ICON_SHOWNEWISSUEALERT;
                this.mnuViewHideWhenMin.Text = MNU_ICON_HIDEWHENMINIMIZED;
                this.mnuViewToolbar.Text = MNU_VIEW_TOOLBAR;
				this.mnuViewStatusBar.Text = MNU_VIEW_STATUSBAR;
                this.mnuToolsConfig.Text = MNU_TOOLS_CONFIG;
				this.mnuToolsTrace.Text = MNU_TOOLS_TRACE;
                this.mnuHelpAbout.Text = MNU_HELP_ABOUT;
				buildHelpMenu();
				#endregion
				#region Window docking
                this.mnuMain.Dock = DockStyle.Top;
                this.Controls.AddRange(new Control[] { this.stbMain,this.mnuMain });
				#endregion
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);

                //Create data and UI services
                #region Tray Icon
                this.mTrayIcon = new NotifyIcon();
                this.mTrayIcon.Text = "Issue Mgt";
                this.mTrayIcon.Icon = this.Icon;
                this.mTrayIcon.Visible = true;
                MenuItem ctxAlert = new MenuItem(MNU_ICON_SHOWNEWISSUEALERT,new System.EventHandler(this.OnMenuItemClicked));
                ctxAlert.Index = 0;
                ctxAlert.Checked = true;
                MenuItem ctxSep1 = new MenuItem("-");
                ctxSep1.Index = 1;
                MenuItem ctxHide = new MenuItem(MNU_ICON_HIDEWHENMINIMIZED,new System.EventHandler(this.OnMenuItemClicked));
                ctxHide.Index = 2;
                ctxHide.Checked = true;
                MenuItem ctxShow = new MenuItem(MNU_ICON_SHOW,new System.EventHandler(this.OnMenuItemClicked));
                ctxShow.Index = 3;
                ctxShow.DefaultItem = true;
                this.mTrayIcon.ContextMenu = new ContextMenu(new MenuItem[] { ctxAlert,ctxSep1,ctxHide,ctxShow });
                this.mTrayIcon.DoubleClick += new System.EventHandler(OnIconDoubleClick);
                this.mTrayIcon.BalloonTipClicked += new EventHandler(OnBalloonTipClicked);
                #endregion
                this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500,3000);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnEdit = new System.Windows.Forms.ToolBarButton();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileActionNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewShowAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewHideWhenMin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsTrace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinTileH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinTileV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.pnlToolbox = new System.Windows.Forms.Panel();
            this.tabToolbox = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtCoordinator = new System.Windows.Forms.TextBox();
            this._lblCoordinator = new System.Windows.Forms.Label();
            this.txtOriginator = new System.Windows.Forms.TextBox();
            this._lblOriginator = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this._lblContact = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this._lblSubject = new System.Windows.Forms.Label();
            this.txtReceived = new System.Windows.Forms.TextBox();
            this._lblReceived = new System.Windows.Forms.Label();
            this.txtAction = new System.Windows.Forms.TextBox();
            this._lblAction = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this._lblType = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this._lblCompany = new System.Windows.Forms.Label();
            this.txtAgent = new System.Windows.Forms.TextBox();
            this._lblAgent = new System.Windows.Forms.Label();
            this.txtStore = new System.Windows.Forms.TextBox();
            this._lblStore = new System.Windows.Forms.Label();
            this.txtZone = new System.Windows.Forms.TextBox();
            this._lblZone = new System.Windows.Forms.Label();
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolboxTitlebar = new System.Windows.Forms.Panel();
            this.lblPin = new System.Windows.Forms.Label();
            this.lblToolbox = new System.Windows.Forms.Label();
            this.splitterV = new System.Windows.Forms.Splitter();
            this.tmrAutoHide = new System.Windows.Forms.Timer(this.components);
            this.mInspector = new Argix.Customers.IssueInspector();
            this.mExplorer = new Argix.Customers.IssueExplorer();
            this.mnuMain.SuspendLayout();
            this.pnlToolbox.SuspendLayout();
            this.tabToolbox.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.pnlToolboxTitlebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Name = "btnEdit";
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,428);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(792,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 1;
            this.stbMain.TerminalText = "Terminal";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuWin,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0,0);
            this.mnuMain.MdiWindowListItem = this.mnuWin;
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(792,24);
            this.mnuMain.TabIndex = 117;
            this.mnuMain.Text = "Standard";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileActionNew,
            this.mnuFileSep2,
            this.mnuSave,
            this.mnuFileSaveAs,
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
            this.mnuFileNew.Size = new System.Drawing.Size(178,22);
            this.mnuFileNew.Text = "New";
            this.mnuFileNew.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(178,22);
            this.mnuFileOpen.Text = "Open";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(175,6);
            // 
            // mnuFileActionNew
            // 
            this.mnuFileActionNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileActionNew.Image")));
            this.mnuFileActionNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileActionNew.Name = "mnuFileActionNew";
            this.mnuFileActionNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuFileActionNew.Size = new System.Drawing.Size(178,22);
            this.mnuFileActionNew.Text = "New Action";
            this.mnuFileActionNew.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(175,6);
            // 
            // mnuSave
            // 
            this.mnuSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuSave.Image")));
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuSave.Size = new System.Drawing.Size(178,22);
            this.mnuSave.Text = "&Save";
            this.mnuSave.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(178,22);
            this.mnuFileSaveAs.Text = "Save As";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(175,6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileSetup.Image")));
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(178,22);
            this.mnuFileSetup.Text = "Page Setup";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = ((System.Drawing.Image)(resources.GetObject("mnuFilePrint.Image")));
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuFilePrint.Size = new System.Drawing.Size(178,22);
            this.mnuFilePrint.Text = "Print";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = ((System.Drawing.Image)(resources.GetObject("mnuFilePreview.Image")));
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(178,22);
            this.mnuFilePreview.Text = "Print Preview";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep4
            // 
            this.mnuFileSep4.Name = "mnuFileSep4";
            this.mnuFileSep4.Size = new System.Drawing.Size(175,6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(178,22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnMenuClick);
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
            this.mnuEdit.Size = new System.Drawing.Size(39,20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCut.Image")));
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEditCut.Size = new System.Drawing.Size(149,22);
            this.mnuEditCut.Text = "Cut";
            this.mnuEditCut.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCopy.Image")));
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEditCopy.Size = new System.Drawing.Size(149,22);
            this.mnuEditCopy.Text = "Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditPaste.Image")));
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEditPaste.Size = new System.Drawing.Size(149,22);
            this.mnuEditPaste.Text = "Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(146,6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuEditSearch.Size = new System.Drawing.Size(149,22);
            this.mnuEditSearch.Text = "Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewFont,
            this.mnuViewSep2,
            this.mnuViewShowAlert,
            this.mnuViewHideWhenMin,
            this.mnuViewSep3,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44,20);
            this.mnuView.Text = "View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mnuViewRefresh.Image")));
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuViewRefresh.Size = new System.Drawing.Size(192,22);
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(189,6);
            // 
            // mnuViewFont
            // 
            this.mnuViewFont.Name = "mnuViewFont";
            this.mnuViewFont.Size = new System.Drawing.Size(192,22);
            this.mnuViewFont.Text = "Font...";
            this.mnuViewFont.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(189,6);
            // 
            // mnuViewShowAlert
            // 
            this.mnuViewShowAlert.Name = "mnuViewShowAlert";
            this.mnuViewShowAlert.Size = new System.Drawing.Size(192,22);
            this.mnuViewShowAlert.Text = "Show Desktop Alert";
            this.mnuViewShowAlert.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuViewHideWhenMin
            // 
            this.mnuViewHideWhenMin.Name = "mnuViewHideWhenMin";
            this.mnuViewHideWhenMin.Size = new System.Drawing.Size(192,22);
            this.mnuViewHideWhenMin.Text = "Hide When Minimized";
            this.mnuViewHideWhenMin.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuViewSep3
            // 
            this.mnuViewSep3.Name = "mnuViewSep3";
            this.mnuViewSep3.Size = new System.Drawing.Size(189,6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(192,22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(192,22);
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig,
            this.mnuToolsTrace});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48,20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(148,22);
            this.mnuToolsConfig.Text = "Configuration";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuToolsTrace
            // 
            this.mnuToolsTrace.Name = "mnuToolsTrace";
            this.mnuToolsTrace.Size = new System.Drawing.Size(148,22);
            this.mnuToolsTrace.Text = "Trace";
            this.mnuToolsTrace.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuWin
            // 
            this.mnuWin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWinCascade,
            this.mnuWinTileH,
            this.mnuWinTileV,
            this.mnuWindowSep1});
            this.mnuWin.Name = "mnuWin";
            this.mnuWin.Size = new System.Drawing.Size(63,20);
            this.mnuWin.Text = "Window";
            this.mnuWin.Visible = false;
            // 
            // mnuWinCascade
            // 
            this.mnuWinCascade.Image = global::Argix.Properties.Resources.CascadeWindows;
            this.mnuWinCascade.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuWinCascade.Name = "mnuWinCascade";
            this.mnuWinCascade.Size = new System.Drawing.Size(160,22);
            this.mnuWinCascade.Text = "Cascade";
            this.mnuWinCascade.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuWinTileH
            // 
            this.mnuWinTileH.Image = global::Argix.Properties.Resources.ArrangeWindows;
            this.mnuWinTileH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuWinTileH.Name = "mnuWinTileH";
            this.mnuWinTileH.Size = new System.Drawing.Size(160,22);
            this.mnuWinTileH.Text = "Tile Horizontally";
            this.mnuWinTileH.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuWinTileV
            // 
            this.mnuWinTileV.Image = global::Argix.Properties.Resources.ArrangeSideBySide;
            this.mnuWinTileV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuWinTileV.Name = "mnuWinTileV";
            this.mnuWinTileV.Size = new System.Drawing.Size(160,22);
            this.mnuWinTileV.Text = "Tile Vertically";
            this.mnuWinTileV.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuWindowSep1
            // 
            this.mnuWindowSep1.Name = "mnuWindowSep1";
            this.mnuWindowSep1.Size = new System.Drawing.Size(157,6);
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
            this.mnuHelpAbout.Size = new System.Drawing.Size(107,22);
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(104,6);
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterH.Location = new System.Drawing.Point(0,165);
            this.splitterH.MinExtra = 96;
            this.splitterH.MinSize = 96;
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(589,3);
            this.splitterH.TabIndex = 119;
            this.splitterH.TabStop = false;
            // 
            // pnlToolbox
            // 
            this.pnlToolbox.Controls.Add(this.tabToolbox);
            this.pnlToolbox.Controls.Add(this.pnlToolboxTitlebar);
            this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlToolbox.Location = new System.Drawing.Point(592,24);
            this.pnlToolbox.Name = "pnlToolbox";
            this.pnlToolbox.Size = new System.Drawing.Size(200,404);
            this.pnlToolbox.TabIndex = 127;
            // 
            // tabToolbox
            // 
            this.tabToolbox.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabToolbox.Controls.Add(this.tabSearch);
            this.tabToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabToolbox.ImageList = this.imlMain;
            this.tabToolbox.ItemSize = new System.Drawing.Size(42,24);
            this.tabToolbox.Location = new System.Drawing.Point(0,24);
            this.tabToolbox.Multiline = true;
            this.tabToolbox.Name = "tabToolbox";
            this.tabToolbox.SelectedIndex = 0;
            this.tabToolbox.ShowToolTips = true;
            this.tabToolbox.Size = new System.Drawing.Size(200,380);
            this.tabToolbox.TabIndex = 119;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.btnReset);
            this.tabSearch.Controls.Add(this.btnSearch);
            this.tabSearch.Controls.Add(this.txtCoordinator);
            this.tabSearch.Controls.Add(this._lblCoordinator);
            this.tabSearch.Controls.Add(this.txtOriginator);
            this.tabSearch.Controls.Add(this._lblOriginator);
            this.tabSearch.Controls.Add(this.txtContact);
            this.tabSearch.Controls.Add(this._lblContact);
            this.tabSearch.Controls.Add(this.txtSubject);
            this.tabSearch.Controls.Add(this._lblSubject);
            this.tabSearch.Controls.Add(this.txtReceived);
            this.tabSearch.Controls.Add(this._lblReceived);
            this.tabSearch.Controls.Add(this.txtAction);
            this.tabSearch.Controls.Add(this._lblAction);
            this.tabSearch.Controls.Add(this.txtType);
            this.tabSearch.Controls.Add(this._lblType);
            this.tabSearch.Controls.Add(this.txtCompany);
            this.tabSearch.Controls.Add(this._lblCompany);
            this.tabSearch.Controls.Add(this.txtAgent);
            this.tabSearch.Controls.Add(this._lblAgent);
            this.tabSearch.Controls.Add(this.txtStore);
            this.tabSearch.Controls.Add(this._lblStore);
            this.tabSearch.Controls.Add(this.txtZone);
            this.tabSearch.Controls.Add(this._lblZone);
            this.tabSearch.ImageKey = "search.gif";
            this.tabSearch.Location = new System.Drawing.Point(4,4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(168,372);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Search";
            this.tabSearch.ToolTipText = "Search issue headers";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(87,319);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75,23);
            this.btnReset.TabIndex = 23;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.OnSearch);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6,319);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75,23);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // txtCoordinator
            // 
            this.txtCoordinator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCoordinator.Location = new System.Drawing.Point(81,282);
            this.txtCoordinator.Name = "txtCoordinator";
            this.txtCoordinator.Size = new System.Drawing.Size(81,20);
            this.txtCoordinator.TabIndex = 21;
            this.txtCoordinator.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblCoordinator
            // 
            this._lblCoordinator.Location = new System.Drawing.Point(3,282);
            this._lblCoordinator.Name = "_lblCoordinator";
            this._lblCoordinator.Size = new System.Drawing.Size(78,21);
            this._lblCoordinator.TabIndex = 20;
            this._lblCoordinator.Text = "Coordinato";
            this._lblCoordinator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOriginator
            // 
            this.txtOriginator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOriginator.Location = new System.Drawing.Point(81,255);
            this.txtOriginator.Name = "txtOriginator";
            this.txtOriginator.Size = new System.Drawing.Size(81,20);
            this.txtOriginator.TabIndex = 19;
            this.txtOriginator.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblOriginator
            // 
            this._lblOriginator.Location = new System.Drawing.Point(3,255);
            this._lblOriginator.Name = "_lblOriginator";
            this._lblOriginator.Size = new System.Drawing.Size(72,21);
            this._lblOriginator.TabIndex = 18;
            this._lblOriginator.Text = "Originator";
            this._lblOriginator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContact
            // 
            this.txtContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContact.Location = new System.Drawing.Point(81,228);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(81,20);
            this.txtContact.TabIndex = 17;
            this.txtContact.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblContact
            // 
            this._lblContact.Location = new System.Drawing.Point(3,228);
            this._lblContact.Name = "_lblContact";
            this._lblContact.Size = new System.Drawing.Size(72,21);
            this._lblContact.TabIndex = 16;
            this._lblContact.Text = "Contact";
            this._lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubject
            // 
            this.txtSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSubject.Location = new System.Drawing.Point(81,201);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(81,20);
            this.txtSubject.TabIndex = 15;
            this.txtSubject.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblSubject
            // 
            this._lblSubject.Location = new System.Drawing.Point(3,201);
            this._lblSubject.Name = "_lblSubject";
            this._lblSubject.Size = new System.Drawing.Size(72,21);
            this._lblSubject.TabIndex = 14;
            this._lblSubject.Text = "Subject";
            this._lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceived
            // 
            this.txtReceived.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceived.Location = new System.Drawing.Point(81,174);
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.Size = new System.Drawing.Size(81,20);
            this.txtReceived.TabIndex = 13;
            this.txtReceived.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblReceived
            // 
            this._lblReceived.Location = new System.Drawing.Point(3,174);
            this._lblReceived.Name = "_lblReceived";
            this._lblReceived.Size = new System.Drawing.Size(72,21);
            this._lblReceived.TabIndex = 12;
            this._lblReceived.Text = "Received";
            this._lblReceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAction
            // 
            this.txtAction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAction.Location = new System.Drawing.Point(81,147);
            this.txtAction.Name = "txtAction";
            this.txtAction.Size = new System.Drawing.Size(81,20);
            this.txtAction.TabIndex = 11;
            this.txtAction.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblAction
            // 
            this._lblAction.Location = new System.Drawing.Point(3,147);
            this._lblAction.Name = "_lblAction";
            this._lblAction.Size = new System.Drawing.Size(72,21);
            this._lblAction.TabIndex = 10;
            this._lblAction.Text = "Action";
            this._lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtType
            // 
            this.txtType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtType.Location = new System.Drawing.Point(81,120);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(81,20);
            this.txtType.TabIndex = 9;
            this.txtType.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblType
            // 
            this._lblType.Location = new System.Drawing.Point(3,120);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(72,21);
            this._lblType.TabIndex = 8;
            this._lblType.Text = "Type";
            this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCompany
            // 
            this.txtCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCompany.Location = new System.Drawing.Point(81,93);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(81,20);
            this.txtCompany.TabIndex = 7;
            this.txtCompany.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblCompany
            // 
            this._lblCompany.Location = new System.Drawing.Point(3,93);
            this._lblCompany.Name = "_lblCompany";
            this._lblCompany.Size = new System.Drawing.Size(72,21);
            this._lblCompany.TabIndex = 6;
            this._lblCompany.Text = "Company";
            this._lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAgent
            // 
            this.txtAgent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAgent.Location = new System.Drawing.Point(81,66);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(81,20);
            this.txtAgent.TabIndex = 5;
            this.txtAgent.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblAgent
            // 
            this._lblAgent.Location = new System.Drawing.Point(3,66);
            this._lblAgent.Name = "_lblAgent";
            this._lblAgent.Size = new System.Drawing.Size(72,21);
            this._lblAgent.TabIndex = 4;
            this._lblAgent.Text = "Agent";
            this._lblAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStore
            // 
            this.txtStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStore.Location = new System.Drawing.Point(81,39);
            this.txtStore.Name = "txtStore";
            this.txtStore.Size = new System.Drawing.Size(81,20);
            this.txtStore.TabIndex = 3;
            this.txtStore.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblStore
            // 
            this._lblStore.Location = new System.Drawing.Point(3,39);
            this._lblStore.Name = "_lblStore";
            this._lblStore.Size = new System.Drawing.Size(72,21);
            this._lblStore.TabIndex = 2;
            this._lblStore.Text = "Store";
            this._lblStore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZone
            // 
            this.txtZone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtZone.Location = new System.Drawing.Point(81,12);
            this.txtZone.Name = "txtZone";
            this.txtZone.Size = new System.Drawing.Size(81,20);
            this.txtZone.TabIndex = 1;
            this.txtZone.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblZone
            // 
            this._lblZone.Location = new System.Drawing.Point(3,12);
            this._lblZone.Name = "_lblZone";
            this._lblZone.Size = new System.Drawing.Size(72,21);
            this._lblZone.TabIndex = 0;
            this._lblZone.Text = "Zone";
            this._lblZone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0,"findreplace.gif");
            this.imlMain.Images.SetKeyName(1,"search.gif");
            // 
            // pnlToolboxTitlebar
            // 
            this.pnlToolboxTitlebar.Controls.Add(this.lblPin);
            this.pnlToolboxTitlebar.Controls.Add(this.lblToolbox);
            this.pnlToolboxTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolboxTitlebar.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.pnlToolboxTitlebar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlToolboxTitlebar.Location = new System.Drawing.Point(0,0);
            this.pnlToolboxTitlebar.Name = "pnlToolboxTitlebar";
            this.pnlToolboxTitlebar.Padding = new System.Windows.Forms.Padding(3);
            this.pnlToolboxTitlebar.Size = new System.Drawing.Size(200,24);
            this.pnlToolboxTitlebar.TabIndex = 118;
            // 
            // lblPin
            // 
            this.lblPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPin.BackColor = System.Drawing.SystemColors.Control;
            this.lblPin.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPin.Location = new System.Drawing.Point(150,4);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(16,16);
            this.lblPin.TabIndex = 120;
            this.lblPin.Text = "X";
            this.lblPin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToolbox
            // 
            this.lblToolbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToolbox.BackColor = System.Drawing.SystemColors.Control;
            this.lblToolbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblToolbox.Font = new System.Drawing.Font("Verdana",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblToolbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblToolbox.Location = new System.Drawing.Point(3,3);
            this.lblToolbox.Name = "lblToolbox";
            this.lblToolbox.Size = new System.Drawing.Size(168,18);
            this.lblToolbox.TabIndex = 119;
            this.lblToolbox.Text = "Toolbox";
            this.lblToolbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitterV
            // 
            this.splitterV.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitterV.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterV.Location = new System.Drawing.Point(589,24);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(3,404);
            this.splitterV.TabIndex = 126;
            this.splitterV.TabStop = false;
            // 
            // mInspector
            // 
            this.mInspector.Cursor = System.Windows.Forms.Cursors.Default;
            this.mInspector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mInspector.Location = new System.Drawing.Point(0,168);
            this.mInspector.Name = "mInspector";
            this.mInspector.Size = new System.Drawing.Size(589,260);
            this.mInspector.TabIndex = 125;
            this.mInspector.Error += new Argix.ControlErrorEventHandler(this.OnExplorerError);
            this.mInspector.IssueChanged += new System.EventHandler(this.OnInspectorIssueChanged);
            // 
            // mExplorer
            // 
            this.mExplorer.ColumnFilters = resources.GetString("mExplorer.ColumnFilters");
            this.mExplorer.ColumnHeaders = resources.GetString("mExplorer.ColumnHeaders");
            this.mExplorer.Cursor = System.Windows.Forms.Cursors.Default;
            this.mExplorer.Dock = System.Windows.Forms.DockStyle.Top;
            this.mExplorer.LastNewIssueTime = new System.DateTime(2010,5,5,15,51,18,562);
            this.mExplorer.Location = new System.Drawing.Point(0,24);
            this.mExplorer.Name = "mExplorer";
            this.mExplorer.ReadOnly = false;
            this.mExplorer.SelectedID = ((long)(0));
            this.mExplorer.Size = new System.Drawing.Size(589,141);
            this.mExplorer.TabIndex = 124;
            this.mExplorer.ToolStripVisible = true;
            this.mExplorer.NewIssue += new Argix.Customers.NewIssueEventHandler(this.OnNewIssue);
            this.mExplorer.IssueSelected += new System.EventHandler(this.OnIssueSelected);
            this.mExplorer.Error += new Argix.ControlErrorEventHandler(this.OnExplorerError);
            this.mExplorer.ServiceStatesChanged += new System.EventHandler(this.OnExplorerStateChanged);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
            this.ClientSize = new System.Drawing.Size(792,452);
            this.Controls.Add(this.mInspector);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.mExplorer);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.pnlToolbox);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.stbMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Direct Issue Management";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.pnlToolbox.ResumeLayout(false);
            this.tabToolbox.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.pnlToolboxTitlebar.ResumeLayout(false);
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
                    this.mnuViewToolbar.Checked = this.mExplorer.ToolStripVisible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    this.Font = this.mnuMain.Font = this.stbMain.Font = this.mExplorer.Font = global::Argix.Properties.Settings.Default.Font;
                    this.mExplorer.LastNewIssueTime = global::Argix.Properties.Settings.Default.LastRefresh;
                    this.mExplorer.ColumnHeaders = global::Argix.Properties.Settings.Default.ColumnHeaders;
                    this.mExplorer.ColumnFilters = global::Argix.Properties.Settings.Default.ColumnFilters;
                    this.mInspector.MaxView = global::Argix.Properties.Settings.Default.MaxView;
                    this.mnuViewShowAlert.Checked = this.mTrayIcon.ContextMenu.MenuItems[0].Checked = true;
                    this.mnuViewHideWhenMin.Checked = this.mTrayIcon.ContextMenu.MenuItems[2].Checked = true;
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
				#endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;
				this.mToolTip.SetToolTip(this.lblPin, "Auto Hide");
                #endregion
				
				//Set control defaults
                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(CustomerProxy.ServiceState,CustomerProxy.ServiceAddress));
                this.mExplorer.ReadOnly = App.Config.ReadOnly;
                if(App.Config.AutoRefreshOn) 
                    this.mExplorer.StartAuto();
                else
                    this.mnuViewRefresh.PerformClick();

            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Ask only if there are detail forms open
            if(!e.Cancel) {
                //Kill tray icon
                this.mTrayIcon.Visible = false;

                //Save settings
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.LastRefresh = DateTime.Now;
                global::Argix.Properties.Settings.Default.ColumnHeaders = this.mExplorer.ColumnHeaders;
                global::Argix.Properties.Settings.Default.ColumnFilters = this.mExplorer.ColumnFilters;
                global::Argix.Properties.Settings.Default.MaxView = this.mInspector.MaxView;
                //global::Argix.Properties.Settings.Default.ToolboxWidth = this.pnlToolbox.Width;
                global::Argix.Properties.Settings.Default.ToolboxAutoHide = this.lblPin.Text != AUTOHIDE_OFF;
                global::Argix.Properties.Settings.Default.Save();
            }
		}
        private void OnFormResize(object sender,System.EventArgs e) { 
			//Event handler for form resized event
            if(this.WindowState == FormWindowState.Minimized) this.Visible = !this.mTrayIcon.ContextMenu.MenuItems[2].Checked;
        }
        private void OnIssueSelected(object sender,EventArgs e) {
            //
            try {
                //Only when the issue changes
                if(this.mExplorer.SelectedIssue == null || this.mInspector.CurrentIssue == null || (this.mExplorer.SelectedIssue.ID != this.mInspector.CurrentIssue.ID)) {
                    this.mInspector.CurrentIssue = this.mExplorer.SelectedIssue;
                    if(this.mExplorer.SearchText.Length > 0) this.mInspector.Search(this.mExplorer.SearchText);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        private void OnExplorerStateChanged(object sender,EventArgs e) { setUserServices(); }
        private void OnExplorerError(object source,ControlErrorEventArgs e) {
            //Event handler for errors from Issue Explorer/Inspector
            App.ReportError(e.Exception, true, LogLevel.Warning);
        }
        private void OnInspectorIssueChanged(object sender,EventArgs e) {
            //Event handler for change in inspector issue (i.e. new action)
            this.mExplorer.Refresh();
        }
        #region Toolbox Support Code: InitializeToolbox(), OnToolboxResize(), ...
        private const string AUTOHIDE_OFF = "X";
        private const string AUTOHIDE_ON = "-";
        private void InitializeToolbox() {
            //Configure toolbox size, state, and event handlers
            try {
                //Set parent tab control, splitter, and pin button event handlers
                this.tabToolbox.Enter += new System.EventHandler(this.OnEnterToolbox);
                this.tabToolbox.Leave += new System.EventHandler(this.OnLeaveToolbox);
                this.tabToolbox.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                this.tabToolbox.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                this.splitterV.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                this.splitterV.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                this.tabToolbox.SizeChanged += new System.EventHandler(this.OnToolboxResize);
                foreach(Control ctl1 in this.pnlToolbox.Controls) {
                    foreach(Control ctl2 in ctl1.Controls) {
                        foreach(Control ctl3 in ctl2.Controls) {
                            foreach(Control ctl4 in ctl3.Controls) {
                                ctl3.Enter += new System.EventHandler(this.OnEnterToolbox);
                                ctl4.Leave += new System.EventHandler(this.OnLeaveToolbox);
                                ctl4.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                                ctl4.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                            }
                            ctl3.Enter += new System.EventHandler(this.OnEnterToolbox);
                            ctl3.Leave += new System.EventHandler(this.OnLeaveToolbox);
                            ctl3.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                            ctl3.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                        }
                        ctl2.Enter += new System.EventHandler(this.OnEnterToolbox);
                        ctl2.Leave += new System.EventHandler(this.OnLeaveToolbox);
                        ctl2.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                        ctl2.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                    }
                    ctl1.Enter += new System.EventHandler(this.OnEnterToolbox);
                    ctl1.Leave += new System.EventHandler(this.OnLeaveToolbox);
                    ctl1.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                    ctl1.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                }

                //Configure auto-hide
                this.pnlToolbox.Width = global::Argix.Properties.Settings.Default.ToolboxWidth;
                this.lblPin.Text = global::Argix.Properties.Settings.Default.ToolboxAutoHide ? AUTOHIDE_ON : AUTOHIDE_OFF;
                this.lblPin.Click += new System.EventHandler(this.OnToggleAutoHide);
                this.tmrAutoHide.Interval = 500;
                this.tmrAutoHide.Tick += new System.EventHandler(this.OnAutoHideToolbox);

                //Show toolbar as inactive
                this.lblToolbox.BackColor = System.Drawing.SystemColors.Control;
                this.lblToolbox.ForeColor = System.Drawing.SystemColors.ControlText;
                this.lblPin.BackColor = System.Drawing.SystemColors.Control;
                this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnToolboxResize(object sender,System.EventArgs e) {
            //Toolbox size changed event handler
            try {
                //Max at 360px
                if(this.pnlToolbox.Width<24) this.pnlToolbox.Width = 24;
                if(this.pnlToolbox.Width>384) this.pnlToolbox.Width = 384;
                if(this.lblPin.Text==AUTOHIDE_OFF || (this.lblPin.Text==AUTOHIDE_ON && this.pnlToolbox.Width>24)) 
                    global::Argix.Properties.Settings.Default.ToolboxWidth = this.pnlToolbox.Width;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnEnterToolbox(object sender,System.EventArgs e) {
            //Occurs when the control becomes the active control on the form
            try {
                //Disable auto-hide when active; show toolbar as active
                if(this.tmrAutoHide.Enabled) {
                    this.tmrAutoHide.Stop();
                    this.tmrAutoHide.Enabled = false;
                }
                this.lblToolbox.BackColor = this.lblPin.BackColor = SystemColors.ActiveCaption;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnLeaveToolbox(object sender,System.EventArgs e) {
            //Occurs when the control is no longer the active control on the form
            try {
                //Enable auto-hide when inactive and not pinned; show toolbar as inactive
                if(this.lblPin.Text==AUTOHIDE_ON) {
                    this.tmrAutoHide.Enabled = true;
                    this.tmrAutoHide.Start();
                }
                this.lblToolbox.BackColor = this.lblPin.BackColor = System.Drawing.SystemColors.Control;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnMouseEnterToolbox(object sender,System.EventArgs e) {
            //Occurs when the mouse enters the visible part of the control
            try {
                //Auto-open if not pinned and toolbar is closed; disable auto-hide if on
                if(this.lblPin.Text==AUTOHIDE_ON && this.pnlToolbox.Width==24) {
                    this.pnlToolbox.Width = global::Argix.Properties.Settings.Default.ToolboxWidth;
                    this.splitterV.Visible = true;
                }
                if(this.tmrAutoHide.Enabled) {
                    this.tmrAutoHide.Stop();
                    this.tmrAutoHide.Enabled = false;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnMouseLeaveToolbox(object sender,System.EventArgs e) {
            //Occurs when the mouse leaves the visible part of the control
            try {
                //Enable auto-hide when inactive and unpinned
                if(this.lblToolbox.BackColor==SystemColors.Control && this.lblPin.Text==AUTOHIDE_ON) {
                    this.tmrAutoHide.Enabled = true;
                    this.tmrAutoHide.Start();
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnToggleAutoHide(object sender,System.EventArgs e) {
            //
            try {
                //Pin or unpin all pin buttons
                this.lblPin.Text = this.lblPin.Text==AUTOHIDE_OFF ? AUTOHIDE_ON : AUTOHIDE_OFF;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnAutoHideToolbox(object sender,System.EventArgs e) {
            //Toolbox timer event handler
            try {
                //Auto-close timer
                this.tmrAutoHide.Stop();
                this.tmrAutoHide.Enabled = false;
                this.pnlToolbox.Width = 24;
                this.splitterV.Visible = false;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion
        #region User Services: OnMenuClick(), OnToolbarButtonClick()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                switch(menu.Text) {
					case MNU_FILE_NEW:              this.mExplorer.New(); break;
                    case MNU_FILE_OPEN:             this.mExplorer.Open(); break;
                    case MNU_FILE_ACTIONNEW:        break;
                    case MNU_FILE_SAVE:             break;
					case MNU_FILE_SAVEAS:           this.mExplorer.SaveAs(); break;
					case MNU_FILE_PRINT_SETTINGS:   this.mExplorer.PageSetup(); break;
					case MNU_FILE_PRINT:            this.mExplorer.Print(); break;
					case MNU_FILE_PREVIEW:          this.mExplorer.Preview(); break;
					case MNU_FILE_EXIT:				this.Close(); Application.Exit(); break;
					case MNU_EDIT_CUT:				break;
					case MNU_EDIT_COPY:				break;
					case MNU_EDIT_PASTE:			break;
                    case MNU_EDIT_FIND:             this.mExplorer.Search(); break;
					case MNU_VIEW_REFRESH:          this.mExplorer.Refresh(); break;
                    case MNU_VIEW_FONT:
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if(fd.ShowDialog() == DialogResult.OK) {
                            this.Font = this.mnuMain.Font = this.stbMain.Font = fd.Font;
                            this.mExplorer.Font = fd.Font;
                        }
                        break;
                    case MNU_ICON_SHOWNEWISSUEALERT: 
                        this.mTrayIcon.ContextMenu.MenuItems[0].PerformClick();
                        this.mnuViewShowAlert.Checked = this.mTrayIcon.ContextMenu.MenuItems[0].Checked;
                        break;
                    case MNU_ICON_HIDEWHENMINIMIZED: 
                        this.mTrayIcon.ContextMenu.MenuItems[2].PerformClick();
                        this.mnuViewHideWhenMin.Checked = this.mTrayIcon.ContextMenu.MenuItems[2].Checked;
                        break;
                    case MNU_VIEW_TOOLBAR:      this.mExplorer.ToolStripVisible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case MNU_VIEW_STATUSBAR:    this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case MNU_TOOLS_CONFIG:          
                        App.ShowConfig();
                        CustomerProxy.TempFolder = App.Config.TempFolder;
                        if(App.Config.AutoRefreshOn) this.mExplorer.StartAuto(); else this.mExplorer.StopAuto();
                        this.mExplorer.Refresh();
                        break;
                    case MNU_TOOLS_TRACE:       break;
                    case MNU_HELP_ABOUT:        new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnToolbarItemClicked(object sender,ToolStripItemClickedEventArgs e) {
            //Toolbar handler - forward to main menu handler
            try {
                switch(e.ClickedItem.Name) {
                    case "btnNew":      this.mnuFileNew.PerformClick(); break;
                    case "btnOpen":     this.mnuFileOpen.PerformClick(); break;
                    case "btnSave":     this.mnuSave.PerformClick(); break;
                    case "btnPrint": this.mExplorer.Print(); break;
                    case "btnPreview": this.mnuFilePreview.PerformClick(); break;
                    case "btnCut": this.mnuEditCut.PerformClick(); break;
                    case "btnCopy": this.mnuEditCopy.PerformClick(); break;
                    case "btnPaste": this.mnuEditPaste.PerformClick(); break;
                    case "btnSearch": this.mnuEditSearch.PerformClick(); break;
                    case "btnRefresh": this.mnuViewRefresh.PerformClick(); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
		#endregion
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu(), OnHelpMenuClick(), OnDataStatusUpdate(), OnMonitorCountUpdate()
        private void configApplication() {
			try {				
				//Create business objects with configuration values
                CustomerProxy.IssueDaysBack = App.Config.IssueDaysBack;
                CustomerProxy.TempFolder = App.Config.TempFolder;
            }
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {
                this.mnuFileNew.Enabled = !App.Config.ReadOnly && this.mExplorer.CanNew;
                this.mnuFileOpen.Enabled = this.mExplorer.CanOpen;
                this.mnuFileActionNew.Enabled = false;
                this.mnuSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = this.mExplorer.CanSaveAs;
                this.mnuFileSetup.Enabled = true;
                this.mnuFilePrint.Enabled = this.mExplorer.CanPrint;
                this.mnuFilePreview.Enabled = this.mExplorer.CanPreview;
                this.mnuFileExit.Enabled = true;
                this.mnuEditCut.Enabled = this.mnuEditCopy.Enabled = this.mnuEditPaste.Enabled = false;
				this.mnuEditSearch.Enabled = this.mExplorer.CanSearch;
                this.mnuViewRefresh.Enabled = true;
                this.mnuViewFont.Enabled = true;
                this.mnuViewShowAlert.Enabled = this.mnuViewHideWhenMin.Enabled = true;
                this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsConfig.Enabled = this.mnuToolsTrace.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                TerminalInfo t = CustomerProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.stbMain.User1Panel.Width = 144;
                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(CustomerProxy.ServiceState,CustomerProxy.ServiceAddress));
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
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
			//Event hanlder for configurable help menu items
			try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
			}
			catch(Exception) { }
		}
        #endregion
        #region Tray Icon: OnMenuItemClicked(), OnIconDoubleClick(), OnNewIssue()
        private void OnMenuItemClicked(object sender,System.EventArgs e) {
            //Menu itemclicked-apply selected service
            try {
                MenuItem menu = (MenuItem)sender;
                switch(menu.Text) {
                    case MNU_ICON_SHOWNEWISSUEALERT:
                        this.mTrayIcon.ContextMenu.MenuItems[0].Checked = !this.mTrayIcon.ContextMenu.MenuItems[0].Checked;
                        break;
                    case MNU_ICON_HIDEWHENMINIMIZED:
                        this.mTrayIcon.ContextMenu.MenuItems[2].Checked = !this.mTrayIcon.ContextMenu.MenuItems[2].Checked;
                        if(this.WindowState == FormWindowState.Minimized)
                            this.Visible = !this.mTrayIcon.ContextMenu.MenuItems[2].Checked;
                        break;
                    case MNU_ICON_SHOW:
                        this.WindowState = FormWindowState.Maximized;
                        this.Visible = true;
                        this.Activate();
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); }
        }
        private void OnIconDoubleClick(object Sender,EventArgs e) {
            //Show the form when the user double clicks on the notify icon
            // Set the WindowState to normal if the form is minimized.
            this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
            this.Activate();
        }
        private void OnNewIssue(object source,NewIssueEventArgs e) {
            //Notify user of new issue
            try {
                if(this.mTrayIcon.ContextMenu.MenuItems[0].Checked) {
                    string tipText = "\nType: " + e.Issue.Type + "\nAction: " + e.Action.Comment + "\n\nZone: " + e.Issue.Zone + "\nStore#: " + e.Issue.StoreNumber.ToString() + "\nCompany: " + e.Issue.CompanyName + "\nAgent: " + (e.Issue.AgentNumber != null ? e.Issue.AgentNumber : "");
                    this.mTrayIcon.ShowBalloonTip(5000,e.Issue.Subject,tipText,ToolTipIcon.Info);
                }
            }
            catch {}
        }
        private void OnBalloonTipClicked(object sender,EventArgs e) {
            //Event handler for balloon tip clicked
        }
        #endregion

        private void OnSearchChanged(object sender,EventArgs e) {
            //Event handler for change in search criteria
            //Validate
            this.btnSearch.Enabled = true;
        }
        private void OnSearch(object sender,EventArgs e) {
            //Event handler for search/reset button clicked
            Button btn = (Button)sender;
            switch(btn.Name) {
                case "btnReset": 
                    this.txtZone.Text=this.txtStore.Text=this.txtAgent.Text="";
                    this.txtCompany.Text="";
                    this.txtType.Text=this.txtAction.Text=this.txtReceived.Text="";
                    this.txtSubject.Text=this.txtContact.Text="";
                    this.txtOriginator.Text=this.txtCoordinator.Text="";
                    break;
                case "btnSearch":
                    //Search
                    this.mExplorer.SearchAdvanced(new object[] { 
                        (this.txtZone.Text.Length > 0?this.txtZone.Text:null),
                        (this.txtStore.Text.Length > 0?this.txtStore.Text:null),
                        (this.txtAgent.Text.Length > 0?this.txtAgent.Text:null),
                        (this.txtCompany.Text.Length > 0?this.txtCompany.Text:null),
                        (this.txtType.Text.Length > 0?this.txtType.Text:null),
                        (this.txtAction.Text.Length > 0?this.txtAction.Text:null),
                        (this.txtReceived.Text.Length > 0?this.txtReceived.Text:null),
                        (this.txtSubject.Text.Length > 0?this.txtSubject.Text:null),
                        (this.txtContact.Text.Length > 0?this.txtContact.Text:null),
                        (this.txtOriginator.Text.Length > 0?this.txtOriginator.Text:null),
                        (this.txtCoordinator.Text.Length > 0?this.txtCoordinator.Text:null) });
                    break;
            }
        }
    }
}
