//	File:	main.cs
//	Author:	J. Heary
//	Date:	09/20/05
//	Desc:	Main MDI window for Dispatch application.
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
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix;
using Argix.Configuration;
using Argix.Data;
using Argix.Windows;


namespace Argix.Dispatch {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private static bool ITEventOn = false;
		private winSchedule mActiveSchedule=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		
		#region Constants
		//Main menu and context menu captions (serves as identifier in handlers)
		private const string MNU_FILE_NEW = "&New...";
		private const string MNU_FILE_OPEN = "&Open";
		private const string MNU_FILE_SAVE = "&Save...";
		private const string MNU_FILE_SAVEAS = "Save &As...";
		private const string MNU_FILE_COPY = "&Copy...";
		private const string MNU_FILE_ARCHIVE = "&Archive...";
		private const string MNU_FILE_PAGE_SETTINGS = "Page Set&up";
		private const string MNU_FILE_PRINT = "&Print";
		private const string MNU_FILE_PROPERTIES = " P&roperties";
		private const string MNU_FILE_EXIT = "E&xit";		
		private const string MNU_EDIT_UNDO = "&Undo";
		private const string MNU_EDIT_CUT = "Cu&t";
		private const string MNU_EDIT_COPY = "&Copy";
		private const string MNU_EDIT_PASTE = "&Paste";
		private const string MNU_EDIT_DELETE = "&Delete";
		private const string MNU_EDIT_MOVETOFOLDER = "&Move To Folder...";
		private const string MNU_EDIT_COPYTOFOLDER = "&Copy To Folder...";
		private const string MNU_VIEW_DEFINEVIEWS = "&Define Views...";
		private const string MNU_VIEW_CURRENT = "Current View";
		private const string MNU_VIEW_CUSTOMIZE = "&Customize Current View...";
		private const string MNU_VIEW_REFRESH = "&Refresh";
		private const string MNU_VIEW_TOOLBAR = "&Toolbar";
		private const string MNU_VIEW_STATUSBAR = "&Status Bar";
		private const string MNU_WIN_CASCADE = "&Cascade";
		private const string MNU_WIN_TILEHORIZ = "Tile Hori&zontally";
		private const string MNU_WIN_TILEVERT = "Tile &Vertically";
		private const string MNU_TOOLS_FIND = "&Find...";
		private const string MNU_TOOLS_USEWEBSVC = "Use Web Services";
		private const string MNU_HELP_ABOUT = "&About Dispatch";
		
		//Toolbar constants
		private const int TLB_NEW = 0;
		private const int TLB_OPEN = 1;
		private const int TLB_SAVE = 2;
		//Sep1
		private const int TLB_PRINT = 4;
		//Sep2
		private const int TLB_UNDO = 6;
		private const int TLB_REDO = 7;
		private const int TLB_CUT = 8;
		private const int TLB_COPY = 9;
		private const int TLB_PASTE = 10;
		//Sep3
		private const int TLB_FIND = 12;
		#endregion
		private const int KEYSTATE_SHIFT = 5;
		private const int KEYSTATE_CTL = 9;
		#region Components
		private System.Windows.Forms.MainMenu mnuMain;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuView;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.ImageList imgMain;
		private System.Windows.Forms.ToolBar tlbMain;
		private System.Windows.Forms.MenuItem mnuHelpSep0;
		private System.Windows.Forms.MenuItem mnuHelpAbout;
		private System.Windows.Forms.MenuItem mnuWindow;
		private System.Windows.Forms.MenuItem mnuWinCascade;
		private System.Windows.Forms.MenuItem mnuWinTileHoriz;
		private System.Windows.Forms.MenuItem mnuWinTileVert;
		private System.Windows.Forms.MenuItem mnuViewToolbar;
		private System.Windows.Forms.MenuItem mnuViewStatusBar;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuFileOpen;
		private System.Windows.Forms.MenuItem mnuFileSave;
		private System.Windows.Forms.MenuItem mnuFilePrint;
		private System.Windows.Forms.MenuItem mnuTools;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileSep2;
		private System.Windows.Forms.MenuItem mnuFilePageSettings;
		private System.Windows.Forms.MenuItem mnuEditSep1;
		private System.Windows.Forms.MenuItem mnuEditCut;
		private System.Windows.Forms.MenuItem mnuEditCopy;
		private System.Windows.Forms.MenuItem mnuEditPaste;
		private System.Windows.Forms.MenuItem mnuEditUndo;
		private System.Windows.Forms.MenuItem mnuEditSep2;
		private System.Windows.Forms.MenuItem mnuFileNew;
		private System.Windows.Forms.MenuItem mnuFileSaveAs;
		private System.Windows.Forms.MenuItem mnuFileSep4;
		private System.Windows.Forms.ToolBarButton btnOpen;
		private System.Windows.Forms.ToolBarButton btnNew;
		private System.Windows.Forms.ToolBarButton btnSave;
		private System.Windows.Forms.ToolBarButton btnPrint;
		private System.Windows.Forms.ToolBarButton btnSep1;
		private System.Windows.Forms.ToolBarButton btnSep2;
		private System.Windows.Forms.ToolBarButton btnUndo;
		private System.Windows.Forms.ToolBarButton btnRedo;
		private System.Windows.Forms.ToolBarButton btnCut;
		private System.Windows.Forms.ToolBarButton btnCopy;
		private System.Windows.Forms.ToolBarButton btnPaste;
		private System.Windows.Forms.ToolBarButton btnFind;
		private System.Windows.Forms.ToolBarButton btnSep3;
		private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.TreeView trvNav;
		private System.Windows.Forms.Splitter splitter;
		private System.Windows.Forms.MenuItem mnuViewSep1;
		private System.Windows.Forms.Panel pnlNav;
		private System.Windows.Forms.ContextMenu ctxNav;
		private System.Windows.Forms.MenuItem ctxNavOpen;
		private System.Windows.Forms.MenuItem ctxNavProps;
		private System.Windows.Forms.MenuItem ctxNavSep1;
		private System.Windows.Forms.MenuItem ctxNavSep2;
		private System.Windows.Forms.MenuItem mnuToolsUseWebService;
		private System.Windows.Forms.MenuItem mnuViewRefresh;
		private Argix.Dispatch.DispatchDS mPickups;
		private System.Windows.Forms.MenuItem ctxNavCopy;
		private System.Windows.Forms.MenuItem mnuFileCopy;
		private System.Windows.Forms.MenuItem mnuEditCopyToFolder;
		private System.Windows.Forms.MenuItem mnuEditDelete;
		private System.Windows.Forms.MenuItem mnuEditMoveToFolder;
		private System.Windows.Forms.MenuItem mnuToolsFind;
		private System.Windows.Forms.MenuItem mnuToolsSep1;
		private System.Windows.Forms.MenuItem mnuFileArchive;
		private System.Windows.Forms.MenuItem mnuFileSep3;
		private System.Windows.Forms.MenuItem mnuViewCustomize;
		private System.Windows.Forms.MenuItem mnuViewSep2;
		private System.Windows.Forms.MenuItem mnuViewCurrent;
		private System.Windows.Forms.MenuItem mnuViewDefineViews;
        private MenuItem mnuToolsDiagnostics;
		private System.ComponentModel.IContainer components;
		
		#endregion
		
		//Operations
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
                #region Set menu identities (used for onclick handlers)
                this.mnuFileNew.Text = MNU_FILE_NEW;
                this.mnuFileOpen.Text = this.ctxNavOpen.Text = MNU_FILE_OPEN;
                this.mnuFileSave.Text = MNU_FILE_SAVE;
                this.mnuFileSaveAs.Text = MNU_FILE_SAVEAS;
                this.mnuFileCopy.Text = this.ctxNavCopy.Text = MNU_FILE_COPY;
                this.mnuFileArchive.Text = MNU_FILE_ARCHIVE;
                this.mnuFilePageSettings.Text = MNU_FILE_PAGE_SETTINGS;
                this.mnuFilePrint.Text = MNU_FILE_PRINT;
                this.ctxNavProps.Text = MNU_FILE_PROPERTIES;
                this.mnuFileExit.Text = MNU_FILE_EXIT;
                this.mnuEditUndo.Text = MNU_EDIT_UNDO;
                this.mnuEditCut.Text = MNU_EDIT_CUT;
                this.mnuEditCopy.Text = MNU_EDIT_COPY;
                this.mnuEditPaste.Text = MNU_EDIT_PASTE;
                this.mnuEditDelete.Text = MNU_EDIT_DELETE;
                this.mnuEditMoveToFolder.Text = MNU_EDIT_MOVETOFOLDER;
                this.mnuEditCopyToFolder.Text = MNU_EDIT_COPYTOFOLDER;
                this.mnuViewDefineViews.Text = MNU_VIEW_DEFINEVIEWS;
                this.mnuViewCurrent.Text = MNU_VIEW_CURRENT;
                this.mnuViewCustomize.Text = MNU_VIEW_CUSTOMIZE;
                this.mnuViewRefresh.Text = MNU_VIEW_REFRESH;
                this.mnuViewToolbar.Text = MNU_VIEW_TOOLBAR;
                this.mnuViewStatusBar.Text = MNU_VIEW_STATUSBAR;
                this.mnuToolsFind.Text = MNU_TOOLS_FIND;
                this.mnuWinCascade.Text = MNU_WIN_CASCADE;
                this.mnuWinTileHoriz.Text = MNU_WIN_TILEHORIZ;
                this.mnuWinTileVert.Text = MNU_WIN_TILEVERT;
                this.mnuHelpAbout.Text = MNU_HELP_ABOUT;
                buildHelpMenu();
                #endregion
                #region Splash Screen Support
                Splash.ITEvent += new EventHandler(OnITEvent);
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);
                if(ITEventOn) {
                    Splash.Close();
                    this.mnuToolsDiagnostics.PerformClick();
                }
                #endregion
                #region Window docking
				this.tlbMain.Dock = DockStyle.Top;
				this.splitter.MinExtra = 96;
				this.splitter.MinSize = 96;
				this.splitter.Dock = DockStyle.Left;
				this.pnlNav.Dock = DockStyle.Left;
					this.trvNav.Dock = DockStyle.Fill;
					this.pnlNav.Controls.AddRange(new Control[]{this.trvNav});
				this.stbMain.Dock = DockStyle.Bottom;
				this.Controls.AddRange(new Control[]{this.splitter, this.pnlNav, this.tlbMain, this.stbMain});
				#endregion
				
				//Create UI services
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.stbMain.Panels[0],3000);

                //Set application configuration
                configApplication();
            }
            catch(Exception ex) { Splash.Close(); if(!frmMain.ITEventOn) throw new ApplicationException("Startup Failure",ex); }
        }
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mnuMain = new System.Windows.Forms.MainMenu(this.components);
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFileNew = new System.Windows.Forms.MenuItem();
            this.mnuFileOpen = new System.Windows.Forms.MenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
            this.mnuFileSave = new System.Windows.Forms.MenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.MenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
            this.mnuFileCopy = new System.Windows.Forms.MenuItem();
            this.mnuFileArchive = new System.Windows.Forms.MenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.MenuItem();
            this.mnuFilePageSettings = new System.Windows.Forms.MenuItem();
            this.mnuFilePrint = new System.Windows.Forms.MenuItem();
            this.mnuFileSep4 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuEditUndo = new System.Windows.Forms.MenuItem();
            this.mnuEditSep1 = new System.Windows.Forms.MenuItem();
            this.mnuEditCut = new System.Windows.Forms.MenuItem();
            this.mnuEditCopy = new System.Windows.Forms.MenuItem();
            this.mnuEditPaste = new System.Windows.Forms.MenuItem();
            this.mnuEditSep2 = new System.Windows.Forms.MenuItem();
            this.mnuEditDelete = new System.Windows.Forms.MenuItem();
            this.mnuEditMoveToFolder = new System.Windows.Forms.MenuItem();
            this.mnuEditCopyToFolder = new System.Windows.Forms.MenuItem();
            this.mnuView = new System.Windows.Forms.MenuItem();
            this.mnuViewDefineViews = new System.Windows.Forms.MenuItem();
            this.mnuViewCurrent = new System.Windows.Forms.MenuItem();
            this.mnuViewCustomize = new System.Windows.Forms.MenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.MenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.MenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.MenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.MenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.MenuItem();
            this.mnuTools = new System.Windows.Forms.MenuItem();
            this.mnuToolsFind = new System.Windows.Forms.MenuItem();
            this.mnuToolsSep1 = new System.Windows.Forms.MenuItem();
            this.mnuToolsUseWebService = new System.Windows.Forms.MenuItem();
            this.mnuWindow = new System.Windows.Forms.MenuItem();
            this.mnuWinCascade = new System.Windows.Forms.MenuItem();
            this.mnuWinTileHoriz = new System.Windows.Forms.MenuItem();
            this.mnuWinTileVert = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpSep0 = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.splitter = new System.Windows.Forms.Splitter();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.tlbMain = new System.Windows.Forms.ToolBar();
            this.btnNew = new System.Windows.Forms.ToolBarButton();
            this.btnOpen = new System.Windows.Forms.ToolBarButton();
            this.btnSave = new System.Windows.Forms.ToolBarButton();
            this.btnSep1 = new System.Windows.Forms.ToolBarButton();
            this.btnPrint = new System.Windows.Forms.ToolBarButton();
            this.btnSep2 = new System.Windows.Forms.ToolBarButton();
            this.btnUndo = new System.Windows.Forms.ToolBarButton();
            this.btnRedo = new System.Windows.Forms.ToolBarButton();
            this.btnCut = new System.Windows.Forms.ToolBarButton();
            this.btnCopy = new System.Windows.Forms.ToolBarButton();
            this.btnPaste = new System.Windows.Forms.ToolBarButton();
            this.btnSep3 = new System.Windows.Forms.ToolBarButton();
            this.btnFind = new System.Windows.Forms.ToolBarButton();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.trvNav = new System.Windows.Forms.TreeView();
            this.ctxNav = new System.Windows.Forms.ContextMenu();
            this.ctxNavOpen = new System.Windows.Forms.MenuItem();
            this.ctxNavSep1 = new System.Windows.Forms.MenuItem();
            this.ctxNavCopy = new System.Windows.Forms.MenuItem();
            this.ctxNavSep2 = new System.Windows.Forms.MenuItem();
            this.ctxNavProps = new System.Windows.Forms.MenuItem();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.mPickups = new Argix.Dispatch.DispatchDS();
            this.mnuToolsDiagnostics = new System.Windows.Forms.MenuItem();
            this.pnlNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPickups)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuWindow,
            this.mnuHelp});
            // 
            // mnuFile
            // 
            this.mnuFile.Index = 0;
            this.mnuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileSep2,
            this.mnuFileCopy,
            this.mnuFileArchive,
            this.mnuFileSep3,
            this.mnuFilePageSettings,
            this.mnuFilePrint,
            this.mnuFileSep4,
            this.mnuFileExit});
            this.mnuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Index = 0;
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Enabled = false;
            this.mnuFileOpen.Index = 1;
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Index = 2;
            this.mnuFileSep1.MergeOrder = 10;
            this.mnuFileSep1.Text = "-";
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Enabled = false;
            this.mnuFileSave.Index = 3;
            this.mnuFileSave.Text = "&Save...";
            this.mnuFileSave.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Index = 4;
            this.mnuFileSaveAs.Text = "Save &As";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Index = 5;
            this.mnuFileSep2.Text = "-";
            // 
            // mnuFileCopy
            // 
            this.mnuFileCopy.Index = 6;
            this.mnuFileCopy.Text = "Copy...";
            this.mnuFileCopy.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileArchive
            // 
            this.mnuFileArchive.Index = 7;
            this.mnuFileArchive.Text = "Archive...";
            this.mnuFileArchive.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Index = 8;
            this.mnuFileSep3.Text = "-";
            // 
            // mnuFilePageSettings
            // 
            this.mnuFilePageSettings.Enabled = false;
            this.mnuFilePageSettings.Index = 9;
            this.mnuFilePageSettings.Text = "Page Settings";
            this.mnuFilePageSettings.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Enabled = false;
            this.mnuFilePrint.Index = 10;
            this.mnuFilePrint.Text = "Print";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSep4
            // 
            this.mnuFileSep4.Index = 11;
            this.mnuFileSep4.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 12;
            this.mnuFileExit.MergeOrder = 11;
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuEditUndo,
            this.mnuEditSep1,
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditSep2,
            this.mnuEditDelete,
            this.mnuEditMoveToFolder,
            this.mnuEditCopyToFolder});
            this.mnuEdit.MergeOrder = 1;
            this.mnuEdit.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditUndo
            // 
            this.mnuEditUndo.Index = 0;
            this.mnuEditUndo.Text = "&Undo";
            this.mnuEditUndo.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Index = 1;
            this.mnuEditSep1.Text = "-";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Index = 2;
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Index = 3;
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Index = 4;
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditSep2
            // 
            this.mnuEditSep2.Index = 5;
            this.mnuEditSep2.Text = "-";
            // 
            // mnuEditDelete
            // 
            this.mnuEditDelete.Index = 6;
            this.mnuEditDelete.Text = "Delete";
            this.mnuEditDelete.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditMoveToFolder
            // 
            this.mnuEditMoveToFolder.Index = 7;
            this.mnuEditMoveToFolder.Text = "Move To Folder";
            this.mnuEditMoveToFolder.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditCopyToFolder
            // 
            this.mnuEditCopyToFolder.Index = 8;
            this.mnuEditCopyToFolder.Text = "Copy To Folder";
            this.mnuEditCopyToFolder.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuView
            // 
            this.mnuView.Index = 2;
            this.mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuViewDefineViews,
            this.mnuViewCurrent,
            this.mnuViewCustomize,
            this.mnuViewSep1,
            this.mnuViewRefresh,
            this.mnuViewSep2,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.MergeOrder = 2;
            this.mnuView.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuView.Text = "&View";
            this.mnuView.Popup += new System.EventHandler(this.OnCurrentViewPopup);
            // 
            // mnuViewDefineViews
            // 
            this.mnuViewDefineViews.Index = 0;
            this.mnuViewDefineViews.Text = "&Define Views...";
            this.mnuViewDefineViews.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuViewCurrent
            // 
            this.mnuViewCurrent.Index = 1;
            this.mnuViewCurrent.Text = "Current View";
            // 
            // mnuViewCustomize
            // 
            this.mnuViewCustomize.Index = 2;
            this.mnuViewCustomize.Text = "Customize Current View...";
            this.mnuViewCustomize.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Index = 3;
            this.mnuViewSep1.Text = "-";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Index = 4;
            this.mnuViewRefresh.Text = "&Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Index = 5;
            this.mnuViewSep2.Text = "-";
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Enabled = false;
            this.mnuViewToolbar.Index = 6;
            this.mnuViewToolbar.MergeOrder = 16;
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Enabled = false;
            this.mnuViewStatusBar.Index = 7;
            this.mnuViewStatusBar.MergeOrder = 17;
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuTools
            // 
            this.mnuTools.Index = 3;
            this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuToolsDiagnostics,
            this.mnuToolsFind,
            this.mnuToolsSep1,
            this.mnuToolsUseWebService});
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsFind
            // 
            this.mnuToolsFind.Index = 1;
            this.mnuToolsFind.Text = "Find and Replace";
            this.mnuToolsFind.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuToolsSep1
            // 
            this.mnuToolsSep1.Index = 2;
            this.mnuToolsSep1.Text = "-";
            // 
            // mnuToolsUseWebService
            // 
            this.mnuToolsUseWebService.Index = 3;
            this.mnuToolsUseWebService.Text = "Use Web Service";
            this.mnuToolsUseWebService.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuWindow
            // 
            this.mnuWindow.Index = 4;
            this.mnuWindow.MdiList = true;
            this.mnuWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuWinCascade,
            this.mnuWinTileHoriz,
            this.mnuWinTileVert});
            this.mnuWindow.MergeOrder = 5;
            this.mnuWindow.MergeType = System.Windows.Forms.MenuMerge.Replace;
            this.mnuWindow.Text = "Window";
            // 
            // mnuWinCascade
            // 
            this.mnuWinCascade.Index = 0;
            this.mnuWinCascade.MergeOrder = 11;
            this.mnuWinCascade.Text = "Cascade";
            this.mnuWinCascade.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuWinTileHoriz
            // 
            this.mnuWinTileHoriz.Index = 1;
            this.mnuWinTileHoriz.MergeOrder = 12;
            this.mnuWinTileHoriz.Text = "Tile Horizontally";
            this.mnuWinTileHoriz.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuWinTileVert
            // 
            this.mnuWinTileVert.Index = 2;
            this.mnuWinTileVert.MergeOrder = 13;
            this.mnuWinTileVert.Text = "Tile Vertically";
            this.mnuWinTileVert.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 5;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpSep0,
            this.mnuHelpAbout});
            this.mnuHelp.MergeOrder = 6;
            this.mnuHelp.MergeType = System.Windows.Forms.MenuMerge.Replace;
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpSep0
            // 
            this.mnuHelpSep0.Index = 0;
            this.mnuHelpSep0.Text = "-";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Enabled = false;
            this.mnuHelpAbout.Index = 1;
            this.mnuHelpAbout.Text = "&About Dispatch";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // splitter
            // 
            this.splitter.Location = new System.Drawing.Point(192,28);
            this.splitter.Name = "splitter";
            this.splitter.Size = new System.Drawing.Size(3,277);
            this.splitter.TabIndex = 3;
            this.splitter.TabStop = false;
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
            this.imgMain.Images.SetKeyName(8,"");
            this.imgMain.Images.SetKeyName(9,"");
            this.imgMain.Images.SetKeyName(10,"");
            this.imgMain.Images.SetKeyName(11,"");
            this.imgMain.Images.SetKeyName(12,"");
            this.imgMain.Images.SetKeyName(13,"");
            // 
            // tlbMain
            // 
            this.tlbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tlbMain.BackColor = System.Drawing.SystemColors.Control;
            this.tlbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnSep1,
            this.btnPrint,
            this.btnSep2,
            this.btnUndo,
            this.btnRedo,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnSep3,
            this.btnFind});
            this.tlbMain.ButtonSize = new System.Drawing.Size(16,16);
            this.tlbMain.DropDownArrows = true;
            this.tlbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tlbMain.ForeColor = System.Drawing.Color.Navy;
            this.tlbMain.ImageList = this.imgMain;
            this.tlbMain.Location = new System.Drawing.Point(0,0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.ShowToolTips = true;
            this.tlbMain.Size = new System.Drawing.Size(664,28);
            this.tlbMain.TabIndex = 86;
            this.tlbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.OnToolbarButtonClicked);
            // 
            // btnNew
            // 
            this.btnNew.ImageIndex = 0;
            this.btnNew.Name = "btnNew";
            this.btnNew.ToolTipText = "New...";
            // 
            // btnOpen
            // 
            this.btnOpen.ImageIndex = 1;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.ToolTipText = "Open...";
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 2;
            this.btnSave.Name = "btnSave";
            this.btnSave.ToolTipText = "Save...";
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 3;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ToolTipText = "Print";
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnUndo
            // 
            this.btnUndo.ImageIndex = 4;
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.ToolTipText = "Undo last change";
            // 
            // btnRedo
            // 
            this.btnRedo.ImageIndex = 5;
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.ToolTipText = "Redo last change";
            // 
            // btnCut
            // 
            this.btnCut.ImageIndex = 6;
            this.btnCut.Name = "btnCut";
            this.btnCut.ToolTipText = "Cut";
            // 
            // btnCopy
            // 
            this.btnCopy.ImageIndex = 7;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.ToolTipText = "Copy";
            // 
            // btnPaste
            // 
            this.btnPaste.ImageIndex = 8;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.ToolTipText = "Paste";
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnFind
            // 
            this.btnFind.ImageIndex = 9;
            this.btnFind.Name = "btnFind";
            this.btnFind.ToolTipText = "Find";
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,305);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(664,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 100;
            this.stbMain.TerminalText = "Terminal";
            // 
            // trvNav
            // 
            this.trvNav.AllowDrop = true;
            this.trvNav.ContextMenu = this.ctxNav;
            this.trvNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvNav.HideSelection = false;
            this.trvNav.Indent = 18;
            this.trvNav.ItemHeight = 18;
            this.trvNav.Location = new System.Drawing.Point(0,0);
            this.trvNav.Name = "trvNav";
            this.trvNav.Size = new System.Drawing.Size(192,277);
            this.trvNav.Sorted = true;
            this.trvNav.TabIndex = 102;
            this.trvNav.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeCollapsed);
            this.trvNav.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.trvNav.DoubleClick += new System.EventHandler(this.OnDoubleClick);
            this.trvNav.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnTreeNodeMouseUp);
            this.trvNav.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.trvNav.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnAfterSelect);
            this.trvNav.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTreeNodeMouseDown);
            this.trvNav.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.trvNav.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.OnBeforeSelect);
            this.trvNav.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeExpanded);
            this.trvNav.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            // 
            // ctxNav
            // 
            this.ctxNav.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ctxNavOpen,
            this.ctxNavSep1,
            this.ctxNavCopy,
            this.ctxNavSep2,
            this.ctxNavProps});
            this.ctxNav.Popup += new System.EventHandler(this.OnNavMenuPopup);
            // 
            // ctxNavOpen
            // 
            this.ctxNavOpen.Index = 0;
            this.ctxNavOpen.Text = "Open";
            this.ctxNavOpen.Click += new System.EventHandler(this.OnNavMenuItemClicked);
            // 
            // ctxNavSep1
            // 
            this.ctxNavSep1.Index = 1;
            this.ctxNavSep1.Text = "-";
            // 
            // ctxNavCopy
            // 
            this.ctxNavCopy.Index = 2;
            this.ctxNavCopy.Text = "Copy";
            this.ctxNavCopy.Click += new System.EventHandler(this.OnNavMenuItemClicked);
            // 
            // ctxNavSep2
            // 
            this.ctxNavSep2.Index = 3;
            this.ctxNavSep2.Text = "-";
            // 
            // ctxNavProps
            // 
            this.ctxNavProps.Index = 4;
            this.ctxNavProps.Text = "Properties";
            this.ctxNavProps.Click += new System.EventHandler(this.OnNavMenuItemClicked);
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.trvNav);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0,28);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(192,277);
            this.pnlNav.TabIndex = 105;
            // 
            // mPickups
            // 
            this.mPickups.DataSetName = "DispatchDS";
            this.mPickups.Locale = new System.Globalization.CultureInfo("en-US");
            this.mPickups.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mnuToolsDiagnostics
            // 
            this.mnuToolsDiagnostics.Index = 0;
            this.mnuToolsDiagnostics.Text = "Diagnostics";
            this.mnuToolsDiagnostics.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(664,329);
            this.Controls.Add(this.splitter);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Direct Dispatch";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closed += new System.EventHandler(this.OnFormClosed);
            this.MdiChildActivate += new System.EventHandler(this.OnMdiChildActivate);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.pnlNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mPickups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        private static void OnITEvent(object o,EventArgs e) { ITEventOn = true; }
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				Splash.Close();
				this.Visible = true;
				Application.DoEvents();
				#region Set user preferences
				this.WindowState = FormWindowState.Normal;
				this.Left = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Left + 0.1 * Screen.PrimaryScreen.WorkingArea.Width);
				this.Top = Convert.ToInt32(Screen.PrimaryScreen.WorkingArea.Top + 0.1 * Screen.PrimaryScreen.WorkingArea.Height);
				this.Width = Convert.ToInt32(0.8 * Screen.PrimaryScreen.WorkingArea.Width);
				this.Height = Convert.ToInt32(0.8 * Screen.PrimaryScreen.WorkingArea.Height);
				//this.grdSortedItems.Height = 192;
				this.tlbMain.Visible = this.mnuViewToolbar.Checked = true;
				this.stbMain.Visible = this.mnuViewStatusBar.Checked = true;
				Application.DoEvents();
				#endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				//this.mToolTip.SetToolTip(this.dtpPickupDate, "Select a date for pickups.");
				#endregion				
				//Initialize controls
				#region TreeView Initialization
				this.trvNav.FullRowSelect = true;
				this.trvNav.Indent = 18;
				this.trvNav.ItemHeight = 18;
				this.trvNav.HideSelection = false;
				this.trvNav.ImageList = this.imgMain;
				this.trvNav.Scrollable = true;
				this.trvNav.Nodes.Clear();
				#endregion
				
				//Create a pickup log; wrap in a custom treenode
				this.mMessageMgr.AddMessage("Loading...");
				DispatchNode dispatchNode = new DispatchNode("Dispatch", 13, 13, App.Mediator);
				this.trvNav.Nodes.Add(dispatchNode);
				dispatchNode.LoadChildNodes();
				dispatchNode.Expand();
				this.trvNav.SelectedNode = this.trvNav.Nodes[0];
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally {  setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for change in form size
		}
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for after form is closed
			try {
				this.mMessageMgr.AddMessage("Saving settings...");
			}
			catch(Exception) { }
		}
		private void OnCurrentViewPopup(object sender, System.EventArgs e) {
			//Event handler for View\Current View menu popup
			this.mnuViewCurrent.MenuItems.Clear();
			this.mnuViewCurrent.MenuItems.AddRange(this.mActiveSchedule.ViewMenuItems);
		}
		#region Navigation Services: OnNavMenuItemClicked(), OnTreeNodeCollapsed(), OnTreeNodeExpanded(), OnTreeNodeMouseDown(), OnAfterSelect()
		private void OnNavMenuPopup(object sender, System.EventArgs e) {
			//Event handler for context menu popup
			setUserServices();
		}
		private void OnNavMenuItemClicked(object sender, System.EventArgs e) {
			//Event handler for nav item menu services
			ScheduleNode node=null;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_FILE_OPEN:			
						node = (ScheduleNode)this.trvNav.SelectedNode;
						this.Cursor = Cursors.WaitCursor;
						openSchedule(node);
						break;
					case MNU_FILE_PROPERTIES:	
						node = (ScheduleNode)this.trvNav.SelectedNode;
						node.Properties();
						break;
				}
			}
			catch(Exception ex)  { App.ReportError(ex); }
			finally  { 
				setUserServices();
				this.Cursor = Cursors.Default; 
			}
		}
		private void OnTreeNodeCollapsed(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node collapsed - child nodes need to unload [stale] data
			try { TsortNode node = (TsortNode)e.Node; node.CollapseNode(); } catch { }
		}
		private void OnTreeNodeExpanded(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node expanded - child nodes need to load [fresh] data
			try { TsortNode node = (TsortNode)e.Node; node.ExpandNode(); } catch { }
		}
		private void OnTreeNodeMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) { 
			//Event handler for mouse down on a tree node
			try { 
				if(e.Button == System.Windows.Forms.MouseButtons.Right) {
					//Select node on right click
					TreeNode node = this.trvNav.GetNodeAt(e.X, e.Y);
					this.trvNav.SelectedNode = node;
				}
			}
			catch(Exception ex)  { App.ReportError(ex); }
		}
		private void OnTreeNodeMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse up on a tree node
		}
		private void OnBeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e) {
			//Event handler for before node selected
		}
		private void OnAfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Event handler for after node selected
		}
		private void OnDoubleClick(object sender, System.EventArgs e) {
			//Event handler for treeview double-clicked
			try {
				//Display selected schedule
				ScheduleNode node=null;
				try {
					node = (ScheduleNode)this.trvNav.SelectedNode;
				}
				catch(InvalidCastException) { }
				if(node != null) openSchedule(node);
			}
			catch {}
		}
		private void openSchedule(ScheduleNode node) {
			//Open the selected schedule
			Form frm=null;
			try  {
				//Display selected schedule
				if(node != null) {
					for(int i=0; i< this.MdiChildren.Length; i++) {
						if(this.MdiChildren[i].Text == node.Schedule.ScheduleName) {
							frm = this.MdiChildren[i];
							break;
						}
					}
					if(frm == null) {
						mMessageMgr.AddMessage("Loading " + node.Schedule.ScheduleName + "...");
						winSchedule win = new winSchedule(node, App.Mediator);
						if(this.MdiChildren.Length > 0) 
							win.WindowState = this.ActiveMdiChild.WindowState;
						else
							win.WindowState = FormWindowState.Maximized;
						win.MdiParent = this;
						win.Activated += new EventHandler(OnScheduleActivated);
						win.Deactivate += new EventHandler(OnScheduleDeactivated);
						win.Closing += new CancelEventHandler(OnScheduleClosing);
						win.Disposed += new EventHandler(OnScheduleClosed);
						win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);					
						win.StatusMessage += new StatusEventHandler(OnStatusMessage);					
						win.Show();
					}
					else
						frm.Activate();
				}
			}
			catch(Exception ex)  { App.ReportError(ex); }
			finally  { setUserServices(); }
		}
		#endregion
		#region User Services: OnMenuItemClicked(), OnToolbarButtonClicked()
		private void OnMenuItemClicked(object sender, System.EventArgs e) {
			//Menu itemclicked-apply selected service
			this.Cursor = Cursors.WaitCursor;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_FILE_NEW:				this.mActiveSchedule.New(); break;
					case MNU_FILE_OPEN:				this.mActiveSchedule.Open(); break;
					case MNU_FILE_SAVE:				break;	//N/A
					case MNU_FILE_SAVEAS:			
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.Filter = "Text Files (*.xml) | *.xml";
						dlgSave.FilterIndex = 0;
						dlgSave.Title = "Save Selected Items As...";
						dlgSave.FileName = this.mActiveSchedule.Name;
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) 
							this.mActiveSchedule.SaveAs(dlgSave.FileName);
						break;
					case MNU_FILE_COPY:				break;	//N/A
					case MNU_FILE_ARCHIVE:			break;	//N/A
					case MNU_FILE_PAGE_SETTINGS:	this.mActiveSchedule.PageSettings(); break;
					case MNU_FILE_PRINT:			this.mActiveSchedule.Print(); break;
					case MNU_FILE_EXIT:				this.Close(); break;
					case MNU_EDIT_UNDO:				this.mActiveSchedule.Undo(); break;
					case MNU_EDIT_CUT:				this.mActiveSchedule.Cut(); break;
					case MNU_EDIT_COPY:				this.mActiveSchedule.Copy(); break;
					case MNU_EDIT_PASTE:			this.mActiveSchedule.Paste(); break;
					case MNU_EDIT_DELETE:			this.mActiveSchedule.Delete(); break;
					case MNU_EDIT_MOVETOFOLDER:		this.mActiveSchedule.MoveToFolder(); break;
					case MNU_EDIT_COPYTOFOLDER:		this.mActiveSchedule.CopyToFolder(); break;
					case MNU_VIEW_DEFINEVIEWS:		this.mActiveSchedule.DefineViews(); break;
					case MNU_VIEW_CUSTOMIZE:		this.mActiveSchedule.CustomizeCurrentView(); break;
					case MNU_VIEW_REFRESH:			this.mActiveSchedule.Refresh(); break;
					case MNU_VIEW_TOOLBAR:			this.tlbMain.Visible = (this.mnuViewToolbar.Checked = !this.mnuViewToolbar.Checked); break;
					case MNU_VIEW_STATUSBAR:		this.stbMain.Visible = (this.mnuViewStatusBar.Checked = !this.mnuViewStatusBar.Checked); break;
					case MNU_TOOLS_FIND:			break;
					case MNU_WIN_CASCADE:			this.LayoutMdi(MdiLayout.Cascade); break;
					case MNU_WIN_TILEHORIZ:			this.LayoutMdi(MdiLayout.TileHorizontal); break;
					case MNU_WIN_TILEVERT:			this.LayoutMdi(MdiLayout.TileVertical); break;
					case MNU_TOOLS_USEWEBSVC: 
                        //dlgLogin oLogin = new dlgLogin(App.Config.MISPassword);
                        //oLogin.ValidateEntry();
                        //if(oLogin.IsValid) {
                        //    this.Cursor = Cursors.WaitCursor;
                        //    this.mMessageMgr.AddMessage("Resetting the application configuration...");
                        //    Application.DoEvents();
                        //    this.mnuToolsUseWebService.Checked = (!this.mnuToolsUseWebService.Checked);
                        //    App.UseWebSvc = this.mnuToolsUseWebService.Checked;
                        //    configApplication();
                        //    this.mnuViewRefresh.PerformClick();
                        //}
						break;
					case MNU_HELP_ABOUT:
						dlgAbout about = new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration);
						about.ShowDialog(this);
						break;
				}
			}
			catch(Exception ex)  { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnToolbarButtonClicked(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//Toolbar handler
			try {
				switch(tlbMain.Buttons.IndexOf(e.Button)) {
					case TLB_NEW:		this.mnuFileNew.PerformClick(); break;
					case TLB_OPEN:		this.mnuFileOpen.PerformClick(); break;
					case TLB_SAVE:		this.mnuFileSave.PerformClick(); break;
					case TLB_PRINT:		this.mnuFilePrint.PerformClick(); break;
					case TLB_UNDO:		this.mnuEditUndo.PerformClick(); break;
					case TLB_REDO:		break;
					case TLB_CUT:		this.mnuEditCut.PerformClick(); break;
					case TLB_COPY:		this.mnuEditCopy.PerformClick(); break;
					case TLB_PASTE:		this.mnuEditPaste.PerformClick(); break;
					case TLB_FIND:		this.mnuToolsFind.PerformClick(); break;
					default:	break;
				}
			}
			catch(Exception ex)  {  App.ReportError(ex); }
		}
		#endregion
		#region Local Services: configApplication(), setUserServices(), App.ReportError(), buildHelpMenu(), OnHelpMenuClick(), OnNetStatusUpdate()
		private void configApplication() {
            try {
                //Create event log and database trace listeners, and log application as started
                try {
                    LogLevel level = (LogLevel)App.Config.TraceLevel;
                    ArgixTrace.AddListener(new ArgixEventLogTraceListener(level,App.EventLogName));
                    ArgixTrace.AddListener(new DBTraceListener(level,App.Mediator,App.USP_TRACE,App.EventLogName));
                }
                catch {
                    ArgixTrace.AddListener(new ArgixEventLogTraceListener(LogLevel.Debug,App.EventLogName));
                    ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug,App.Mediator,App.USP_TRACE,App.EventLogName));
                    ArgixTrace.WriteLine(new TraceMessage("Log level not found; setting log levels to Debug.",App.EventLogName,LogLevel.Warning,"Log Level"));
                }
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.EventLogName,LogLevel.Information,"App Started"));

                //Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                this.stbMain.SetTerminalPanel(App.Mediator.TerminalID.ToString(),App.Mediator.Description);
                bool createError = App.Config.ReadOnly;

            }
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); }
		}
		private void setUserServices() {
			//Set user services depending upon an item selected in the grid
			bool canNew=false, canOpen=false, canSaveAs=false, canPrint=false;
			bool canUndo=false, canCut=false, canCopy=false, canPaste=false;
			bool canDelete=false, canMoveTo=false, canCopyTo=false;
			try {
				//Determine context
				if(this.mActiveSchedule != null) {
					canNew = this.mActiveSchedule.CanNew;
					canOpen = this.mActiveSchedule.CanOpen;
					canSaveAs = this.mActiveSchedule.CanSaveAs;
					canPrint = this.mActiveSchedule.CanPrint;
					canUndo = this.mActiveSchedule.CanUndo;
					canCut = this.mActiveSchedule.CanCut;
					canCopy = this.mActiveSchedule.CanCopy;
					canPaste = this.mActiveSchedule.CanPaste;
					canDelete = this.mActiveSchedule.CanDelete;
					canMoveTo = this.mActiveSchedule.CanMoveToFolder;
					canCopyTo = this.mActiveSchedule.CanCopyToFolder;
				}
				TsortNode node=null;
				try { node = (TsortNode)this.trvNav.SelectedNode; } catch { }
				
				//Set main menu and context menu states
				this.mnuFileNew.Enabled = this.btnNew.Enabled = canNew;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = canOpen;
				this.ctxNavOpen.Enabled = node.CanOpen;
				this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
				this.mnuFileSaveAs.Enabled = canSaveAs;
				this.mnuFileCopy.Enabled = this.ctxNavCopy.Enabled = false;
				this.mnuFileArchive.Enabled = false;
				this.mnuFilePageSettings.Enabled = (this.mActiveSchedule != null);
				this.mnuFilePrint.Enabled = this.btnPrint.Enabled = canPrint;
				this.ctxNavProps.Enabled = false;
				this.mnuFileExit.Enabled = true;
				this.mnuEditUndo.Enabled = this.btnUndo.Enabled = canUndo;
				this.btnRedo.Enabled = false;
				this.mnuEditCut.Enabled = this.btnCut.Enabled = canCut;
				this.mnuEditCopy.Enabled = this.btnCopy.Enabled = canCopy;
				this.mnuEditPaste.Enabled = this.btnPaste.Enabled = canPaste;
				this.mnuEditDelete.Enabled = canDelete;
				this.mnuEditMoveToFolder.Enabled = canMoveTo;
				this.mnuEditCopyToFolder.Enabled = canCopyTo;
				this.mnuViewDefineViews.Enabled = this.mnuViewCurrent.Enabled = this.mnuViewCustomize.Enabled = (this.mActiveSchedule != null);
				this.mnuViewRefresh.Enabled = (this.mActiveSchedule != null);
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuToolsFind.Enabled = this.btnFind.Enabled = false;
				this.mnuWinCascade.Enabled = this.mnuWinTileHoriz.Enabled = this.mnuWinTileVert.Enabled = true;
				this.mnuHelpAbout.Enabled = true;
			}
			catch(Exception ex)  { App.ReportError(ex); }
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
			catch(Exception) { }
		}
		private void OnHelpMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for configurable help menu items
			try {
				MenuItem mnu = (MenuItem)sender;
				Help.ShowHelp(this, this.mHelpItems.GetValues(mnu.Text)[0]);
			}
			catch(Exception) { }
		}
		public void OnDataStatusUpdate(object sender, DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
		#endregion
		#region Schedule Control: OnMdiChildActivate(), OnScheduleActivated(), OnScheduleDeactivated(), OnScheduleClosing(), OnScheduleClosed(), OnStatusMessage(), OnServiceStatesChanged()
		private void OnMdiChildActivate(object sender, EventArgs e) {
			//Event handler for change in mdi child collection
			setUserServices();
		}
		private void OnScheduleActivated(object sender, System.EventArgs e) {
			//Event handler for activaton of a schedule window
			try { 
				this.mActiveSchedule = (winSchedule)sender; 
			} catch { } finally { setUserServices(); }
		}						
		private void OnScheduleDeactivated(object sender, System.EventArgs e) {
			//Event handler for deactivaton of a schedule window
			try { 
				this.mActiveSchedule = null; 
			} catch { } finally { setUserServices(); }
		}
		private void OnScheduleClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing via control box; e.Cancel=true keeps window open
		}
		private void OnScheduleClosed(object sender, System.EventArgs e) {
			//Event handler for closing of a schedule window
			setUserServices();
		}
		private void OnStatusMessage(object source, StatusEventArgs e) {
			//Event handler for status messages from schedule windows
			try { this.mMessageMgr.AddMessage(e.Message); } catch { }
		}
		private void OnServiceStatesChanged(object source, EventArgs e) {
			//Event handler for change in service states of active schedule window
			setUserServices();
		}
		#endregion
		#region Drag/Drop: OnDragDrop(), OnDragEnter(), OnDragLeave(), OnDragOver()
		private void OnDragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dropping onto the window
			try {
				//Manage data
				DataObject oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.Serializable, true)) {
					DispatchDS ds = (DispatchDS)oData.GetData(DataFormats.Serializable);
					ScheduleNode node = (ScheduleNode)this.trvNav.SelectedNode;
					node.Schedule.AddList(ds);
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnDragEnter(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging into the window
			try {
				//On drag enter, turn on appropriate drag drop effect
				DataObject oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.Serializable, true)) {
					switch(e.KeyState) {
						case KEYSTATE_SHIFT:	e.Effect = DragDropEffects.Move; break;
						case KEYSTATE_CTL:		e.Effect = DragDropEffects.Copy; break;
						default:				e.Effect = DragDropEffects.Copy; break;
					}
				}
				else
					e.Effect = DragDropEffects.None;
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnDragLeave(object sender, System.EventArgs e) { }
		private void OnDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging over the window
			try {
				//
				this.trvNav.SelectedNode = this.trvNav.GetNodeAt(e.X-this.Left, e.Y-this.Top);
				
				//Retrieve drag drop data
				DataObject oData = (DataObject)e.Data;
				if(oData.GetDataPresent(DataFormats.Serializable, true)) {
					switch(e.KeyState) {
						case KEYSTATE_SHIFT:	e.Effect = DragDropEffects.Move; break;
						case KEYSTATE_CTL:		e.Effect = DragDropEffects.Copy; break;
						default:				e.Effect = DragDropEffects.Copy; break;
					}
				}
				else
					e.Effect = DragDropEffects.None;
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
	}
}
