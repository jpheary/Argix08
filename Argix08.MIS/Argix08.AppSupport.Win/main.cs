using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.IO;
using Argix.Data;
using Argix.Windows;

namespace Argix.MIS {
    //
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private winApp mActiveApp=null;
		private TrayIcon mTrayIcon=null;
		private ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		
		private string mContext="";
		private NameValueCollection mHelpItems=null;
		
		#region Constants
		//Main menu and context menu captions (serves as identifier in handlers)
		private const string MNU_FILE_NEW = "&New...";
		private const string MNU_FILE_OPEN = "&Open";
		private const string MNU_FILE_SAVE = "&Save";
		private const string MNU_FILE_SAVEAS = "Save &As...";
		private const string MNU_FILE_PRINT_SETTINGS = "Page Set&up...";
		private const string MNU_FILE_PRINT = "&Print...";
		private const string MNU_FILE_PROPERTIES = "Properties...";
		private const string MNU_FILE_EXIT = "E&xit";
		private const string MNU_EDIT_CUT = "Cu&t";
		private const string MNU_EDIT_COPY = "&Copy";
		private const string MNU_EDIT_PASTE = "&Paste";
		private const string MNU_EDIT_DELETE = "Delete";
		private const string MNU_EDIT_FIND = "&Find...";
		private const string MNU_VIEW_REFRESH = "Refresh";
		private const string MNU_VIEW_TOOLBAR = "&Toolbar";
		private const string MNU_VIEW_STATUSBAR = "&Status Bar";
		private const string MNU_WIN_CASCADE = "&Cascade";
		private const string MNU_WIN_TILEHORIZ = "Tile Hori&zontally";
		private const string MNU_WIN_TILEVERT = "Tile &Vertically";
		private const string MNU_HELP_CONTENTS = "&Contents";
		private const string MNU_HELP_ABOUT = "&About Tsort Applications Support";
		
		//Toolbar constants
		private const int TLB_NEW = 0;
		private const int TLB_OPEN = 1;
		private const int TLB_SAVE = 2;
		//Sep1
		private const int TLB_PRINT = 4;
		//Sep2
		private const int TLB_CUT = 6;
		private const int TLB_COPY = 7;
		private const int TLB_PASTE = 8;
		private const int TLB_FIND = 9;
		//Sep3
		private const int TLB_REFRESH = 11;
		
		private const string CONTEXT_TERMINAL = "Terminal";
		private const string CONTEXT_APPNODE = "AppNode";
		private const string CONTEXT_APPWIN = "AppWin";
		
		private const string MNU_ICON_HIDEWHENMINIMIZED = "Hide When Minimized";
		private const string MNU_ICON_SHOW = "Open Tsort Applications Support Tool";
		#endregion
		#region Components
		private System.Windows.Forms.MainMenu mnuMain;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuFileExit;
		private System.Windows.Forms.MenuItem mnuView;
		private System.Windows.Forms.MenuItem mnuHelp;
		private System.Windows.Forms.Splitter splitterV;
		private System.Windows.Forms.ImageList imgMain;
		private System.Windows.Forms.ToolBar tlbMain;
		private Argix.Windows.ArgixStatusBar stbMain;
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
		private System.Windows.Forms.MenuItem mnuFilePrint;
		private System.Windows.Forms.MenuItem mnuViewSep0;
		private System.Windows.Forms.MenuItem mnuTools;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileSep2;
		private System.Windows.Forms.MenuItem mnuFilePageSettings;
		private System.Windows.Forms.ToolBarButton btnSep1;
		private System.Windows.Forms.ToolBarButton btnRefresh;
		private System.Windows.Forms.MenuItem mnuViewRefresh;
		private System.Windows.Forms.ContextMenu ctxApp;
		private System.Windows.Forms.MenuItem ctxAppRefresh;
		private System.Windows.Forms.TreeView trvMain;
		private System.Windows.Forms.MenuItem mnuFileNew;
		private System.Windows.Forms.MenuItem mnuFileSave;
		private System.Windows.Forms.MenuItem mnuFileSaveAs;
		private System.Windows.Forms.MenuItem mnuFileSep3;
		private System.Windows.Forms.ToolBarButton btnOpen;
		private System.Windows.Forms.ToolBarButton btnNew;
		private System.Windows.Forms.ToolBarButton btnSave;
		private System.Windows.Forms.ToolBarButton btnPrint;
		private System.Windows.Forms.ToolBarButton btnSep2;
		private System.Windows.Forms.ToolBarButton btnCut;
		private System.Windows.Forms.ToolBarButton btnCopy;
		private System.Windows.Forms.ToolBarButton btnPaste;
		private System.Windows.Forms.ToolBarButton btnSep3;
		private System.Windows.Forms.MenuItem mnuEditCut;
		private System.Windows.Forms.MenuItem mnuEditCopy;
		private System.Windows.Forms.MenuItem mnuEditPaste;
		private System.Windows.Forms.MenuItem ctxAppDelete;
		private System.Windows.Forms.MenuItem ctxAppSep1;
		private System.Windows.Forms.MenuItem ctxAppSep2;
		private System.Windows.Forms.MenuItem mnuEditSep1;
		private System.Windows.Forms.MenuItem mnuEditFind;
		private System.Windows.Forms.ToolBarButton btnFind;
		private System.Windows.Forms.MenuItem ctxAppNew;
		private System.Windows.Forms.MenuItem ctxAppOpen;
		private System.Windows.Forms.MenuItem mnuEditDelete;
		private System.Windows.Forms.MenuItem mnuEditSep2;
		private System.Windows.Forms.MenuItem ctxAppProps;
		private System.Windows.Forms.MenuItem ctxAppSep3;
		private System.ComponentModel.IContainer components;
		
		#endregion
		//Events
		public static event DataStatusHandler DataStatusUpdate=null;
				
		//Interface
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				UserSettings.RegistryKey = App.RegistryKey;
				#region Menu identities (used for onclick handlers) 
				this.mnuFileNew.Text = this.ctxAppNew.Text = MNU_FILE_NEW;
				this.mnuFileOpen.Text = this.ctxAppOpen.Text = MNU_FILE_OPEN;
				this.mnuFileSave.Text = MNU_FILE_SAVE;
				this.mnuFileSaveAs.Text = MNU_FILE_SAVEAS;
				this.mnuFilePageSettings.Text = MNU_FILE_PRINT_SETTINGS;
				this.mnuFilePrint.Text = MNU_FILE_PRINT;
				this.ctxAppProps.Text = MNU_FILE_PROPERTIES;
				this.mnuFileExit.Text = MNU_FILE_EXIT;
				this.mnuEditCut.Text = MNU_EDIT_CUT;
				this.mnuEditCopy.Text = MNU_EDIT_COPY;
				this.mnuEditPaste.Text = MNU_EDIT_PASTE;
				this.mnuEditDelete.Text = this.ctxAppDelete.Text = MNU_EDIT_DELETE;
				this.mnuEditFind.Text = MNU_EDIT_FIND;
				this.mnuViewRefresh.Text = this.ctxAppRefresh.Text = MNU_VIEW_REFRESH;
				this.mnuViewToolbar.Text = MNU_VIEW_TOOLBAR;
				this.mnuViewStatusBar.Text = MNU_VIEW_STATUSBAR;
				this.mnuWinCascade.Text = MNU_WIN_CASCADE;
				this.mnuWinTileHoriz.Text = MNU_WIN_TILEHORIZ;
				this.mnuWinTileVert.Text = MNU_WIN_TILEVERT;
				this.mnuHelpAbout.Text = MNU_HELP_ABOUT;
				buildHelpMenu();
				#endregion
				#region Window docking
				this.splitterV.MinExtra = 0;
				this.splitterV.MinSize = 18;
				this.tlbMain.Dock = DockStyle.Top;
				this.splitterV.Dock = DockStyle.Left;
				this.trvMain.Dock = DockStyle.Left;
				this.stbMain.Dock = DockStyle.Bottom;
				this.Controls.AddRange(new Control[]{this.splitterV, this.trvMain, this.tlbMain, this.stbMain});
				#endregion
				
				//Create data and UI services
				frmMain.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate2);
				this.mTrayIcon = new TrayIcon("App Support", this.Icon);
				MenuItem ctxHide = new MenuItem(MNU_ICON_HIDEWHENMINIMIZED, new System.EventHandler(this.OnMenuItemClicked));
				ctxHide.Index = 0;
				ctxHide.Checked = true;
				MenuItem ctxShow = new MenuItem(MNU_ICON_SHOW, new System.EventHandler(this.OnMenuItemClicked));
				ctxShow.Index = 1;
				ctxShow.DefaultItem = true;
				this.mTrayIcon.MenuItems.AddRange(new MenuItem[] {ctxHide, ctxShow});
				this.mTrayIcon.DoubleClick += new System.EventHandler(OnIconDoubleClick);
				this.mToolTip = new ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 3000);
				
				//Set application configuration
				configApplication();
			} 
			catch(Exception ex) {  App.ReportError(ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
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
            this.mnuFilePageSettings = new System.Windows.Forms.MenuItem();
            this.mnuFilePrint = new System.Windows.Forms.MenuItem();
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
            this.mnuViewSep0 = new System.Windows.Forms.MenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.MenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.MenuItem();
            this.mnuTools = new System.Windows.Forms.MenuItem();
            this.mnuWindow = new System.Windows.Forms.MenuItem();
            this.mnuWinCascade = new System.Windows.Forms.MenuItem();
            this.mnuWinTileHoriz = new System.Windows.Forms.MenuItem();
            this.mnuWinTileVert = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpSep0 = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.splitterV = new System.Windows.Forms.Splitter();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.tlbMain = new System.Windows.Forms.ToolBar();
            this.btnNew = new System.Windows.Forms.ToolBarButton();
            this.btnOpen = new System.Windows.Forms.ToolBarButton();
            this.btnSave = new System.Windows.Forms.ToolBarButton();
            this.btnSep1 = new System.Windows.Forms.ToolBarButton();
            this.btnPrint = new System.Windows.Forms.ToolBarButton();
            this.btnSep2 = new System.Windows.Forms.ToolBarButton();
            this.btnCut = new System.Windows.Forms.ToolBarButton();
            this.btnCopy = new System.Windows.Forms.ToolBarButton();
            this.btnPaste = new System.Windows.Forms.ToolBarButton();
            this.btnFind = new System.Windows.Forms.ToolBarButton();
            this.btnSep3 = new System.Windows.Forms.ToolBarButton();
            this.btnRefresh = new System.Windows.Forms.ToolBarButton();
            this.ctxApp = new System.Windows.Forms.ContextMenu();
            this.ctxAppNew = new System.Windows.Forms.MenuItem();
            this.ctxAppOpen = new System.Windows.Forms.MenuItem();
            this.ctxAppSep1 = new System.Windows.Forms.MenuItem();
            this.ctxAppDelete = new System.Windows.Forms.MenuItem();
            this.ctxAppSep2 = new System.Windows.Forms.MenuItem();
            this.ctxAppRefresh = new System.Windows.Forms.MenuItem();
            this.ctxAppSep3 = new System.Windows.Forms.MenuItem();
            this.ctxAppProps = new System.Windows.Forms.MenuItem();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.trvMain = new System.Windows.Forms.TreeView();
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
            this.mnuFilePageSettings,
            this.mnuFilePrint,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Enabled = false;
            this.mnuFileNew.Index = 0;
            this.mnuFileNew.Text = "New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Enabled = false;
            this.mnuFileOpen.Index = 1;
            this.mnuFileOpen.Text = "Open...";
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
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Enabled = false;
            this.mnuFileSaveAs.Index = 4;
            this.mnuFileSaveAs.Text = "Save &As";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Index = 5;
            this.mnuFileSep2.Text = "-";
            // 
            // mnuFilePageSettings
            // 
            this.mnuFilePageSettings.Enabled = false;
            this.mnuFilePageSettings.Index = 6;
            this.mnuFilePageSettings.Text = "Page Settings...";
            this.mnuFilePageSettings.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Enabled = false;
            this.mnuFilePrint.Index = 7;
            this.mnuFilePrint.Text = "Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Index = 8;
            this.mnuFileSep3.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 9;
            this.mnuFileExit.MergeOrder = 11;
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnMenuItemClicked);
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
            this.mnuEdit.MergeOrder = 1;
            this.mnuEdit.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Index = 0;
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Index = 1;
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Index = 2;
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnMenuItemClicked);
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
            this.mnuEditDelete.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuEditSep2
            // 
            this.mnuEditSep2.Index = 5;
            this.mnuEditSep2.Text = "-";
            // 
            // mnuEditFind
            // 
            this.mnuEditFind.Index = 6;
            this.mnuEditFind.Text = "Find...";
            this.mnuEditFind.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuView
            // 
            this.mnuView.Index = 2;
            this.mnuView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep0,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.MergeOrder = 2;
            this.mnuView.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Index = 0;
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuViewSep0
            // 
            this.mnuViewSep0.Index = 1;
            this.mnuViewSep0.Text = "-";
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Enabled = false;
            this.mnuViewToolbar.Index = 2;
            this.mnuViewToolbar.MergeOrder = 16;
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Enabled = false;
            this.mnuViewStatusBar.Index = 3;
            this.mnuViewStatusBar.MergeOrder = 17;
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // mnuTools
            // 
            this.mnuTools.Index = 3;
            this.mnuTools.Text = "&Tools";
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
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // splitterV
            // 
            this.splitterV.Location = new System.Drawing.Point(0,28);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(3,277);
            this.splitterV.TabIndex = 3;
            this.splitterV.TabStop = false;
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
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnFind,
            this.btnSep3,
            this.btnRefresh});
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
            // 
            // btnOpen
            // 
            this.btnOpen.ImageIndex = 1;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.ToolTipText = "Open application";
            // 
            // btnSave
            // 
            this.btnSave.ImageIndex = 2;
            this.btnSave.Name = "btnSave";
            // 
            // btnSep1
            // 
            this.btnSep1.ImageIndex = 1;
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 3;
            this.btnPrint.Name = "btnPrint";
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnCut
            // 
            this.btnCut.ImageIndex = 4;
            this.btnCut.Name = "btnCut";
            this.btnCut.ToolTipText = "Cut";
            // 
            // btnCopy
            // 
            this.btnCopy.ImageIndex = 5;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.ToolTipText = "Copy";
            // 
            // btnPaste
            // 
            this.btnPaste.ImageIndex = 6;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.ToolTipText = "Paste";
            // 
            // btnFind
            // 
            this.btnFind.ImageIndex = 7;
            this.btnFind.Name = "btnFind";
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 8;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ToolTipText = "Refresh application";
            // 
            // ctxApp
            // 
            this.ctxApp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ctxAppNew,
            this.ctxAppOpen,
            this.ctxAppSep1,
            this.ctxAppDelete,
            this.ctxAppSep2,
            this.ctxAppRefresh,
            this.ctxAppSep3,
            this.ctxAppProps});
            // 
            // ctxAppNew
            // 
            this.ctxAppNew.Index = 0;
            this.ctxAppNew.Text = "New";
            this.ctxAppNew.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxAppOpen
            // 
            this.ctxAppOpen.DefaultItem = true;
            this.ctxAppOpen.Index = 1;
            this.ctxAppOpen.Text = "Open";
            this.ctxAppOpen.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxAppSep1
            // 
            this.ctxAppSep1.Index = 2;
            this.ctxAppSep1.Text = "-";
            // 
            // ctxAppDelete
            // 
            this.ctxAppDelete.Index = 3;
            this.ctxAppDelete.Text = "Delete";
            this.ctxAppDelete.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxAppSep2
            // 
            this.ctxAppSep2.Index = 4;
            this.ctxAppSep2.Text = "-";
            // 
            // ctxAppRefresh
            // 
            this.ctxAppRefresh.Index = 5;
            this.ctxAppRefresh.Text = "Refresh";
            this.ctxAppRefresh.Click += new System.EventHandler(this.OnMenuItemClicked);
            // 
            // ctxAppSep3
            // 
            this.ctxAppSep3.Index = 6;
            this.ctxAppSep3.Text = "-";
            // 
            // ctxAppProps
            // 
            this.ctxAppProps.Index = 7;
            this.ctxAppProps.Text = "Properties";
            this.ctxAppProps.Click += new System.EventHandler(this.OnMenuItemClicked);
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
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // trvMain
            // 
            this.trvMain.ContextMenu = this.ctxApp;
            this.trvMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvMain.HideSelection = false;
            this.trvMain.Indent = 18;
            this.trvMain.ItemHeight = 18;
            this.trvMain.Location = new System.Drawing.Point(3,28);
            this.trvMain.Name = "trvMain";
            this.trvMain.Size = new System.Drawing.Size(288,277);
            this.trvMain.Sorted = true;
            this.trvMain.TabIndex = 102;
            this.trvMain.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeCollapsed);
            this.trvMain.DoubleClick += new System.EventHandler(this.OnTreeNodeDoubleClicked);
            this.trvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeSelected);
            this.trvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTreeviewMouseDown);
            this.trvMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeExpanded);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(664,329);
            this.Controls.Add(this.trvMain);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tsort Applications Support Tool";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closed += new System.EventHandler(this.OnFormClosed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			string sWindowSettings="";
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize
				this.Visible = true;
				Application.DoEvents();
				#region Set user preferences
				sWindowSettings = (string)UserSettings.Read("WindowSettings", "Normal,56,35,925,628");
				char[] token = {Convert.ToChar(",")};
				string[] settings = sWindowSettings.Split(token, 5);
				switch(settings[0]) {
					case "Maximized":	this.WindowState = FormWindowState.Maximized; break;
					case "Minimized":	this.WindowState = FormWindowState.Minimized; break;
					default:
						this.WindowState = FormWindowState.Normal;
						this.Left = Convert.ToInt32(settings[1]);
						this.Top = Convert.ToInt32(settings[2]);
						this.Width = Convert.ToInt32(settings[3]);
						this.Height = Convert.ToInt32(settings[4]);
						break;
				}
				this.mnuViewToolbar.Checked = this.tlbMain.Visible = Convert.ToBoolean(UserSettings.Read("Toolbar", true));
				this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(UserSettings.Read("StatusBar", true));
				Application.DoEvents();
				#endregion
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
				this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(true, ""));
				this.stbMain.SetTerminalPanel("", Environment.MachineName);
				
				//Load TsortNodes into treeview
				this.mMessageMgr.AddMessage("Loading production application deployments...");
				this.stbMain.Refresh();
				#region TreeView Initialization
				this.trvMain.FullRowSelect = true;
				this.trvMain.Indent = 18;
				this.trvMain.ItemHeight = 18;
				this.trvMain.HideSelection = false;
				this.trvMain.ImageList = this.imgMain;
				this.trvMain.Scrollable = true;
				this.trvMain.Nodes.Clear();
				#endregion
				Enterprise node = new Enterprise("Argix08 Applications", App.ICON_OPEN, App.ICON_CLOSED);
				this.trvMain.Nodes.Add(node);
				node.LoadChildNodes();
				this.trvMain.SelectedNode = this.trvMain.Nodes[0];
                this.trvMain.Nodes[0].Expand();
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for change in form size
			try {
			if(this.WindowState == FormWindowState.Minimized) this.Visible = !this.mTrayIcon.MenuItems[0].Checked;
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing
			try {
				for(int i=0; i<this.MdiChildren.Length; i++) 
					this.MdiChildren[i].Close();
			}
			catch(Exception ex) {  App.ReportError(ex); }
		}
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for after form is closed
			try {
				this.mMessageMgr.AddMessage("Saving settings...");
				UserSettings.Write("WindowSettings", this.WindowState.ToString() + "," + this.Left + "," + this.Top + "," + this.Width + "," + this.Height);
				UserSettings.Write("Toolbar", this.mnuViewToolbar.Checked);
				UserSettings.Write("StatusBar", this.mnuViewStatusBar.Checked);
				this.mTrayIcon.Visible = false;
			}
			catch(Exception ex) {  App.ReportError(ex); }
		}
		#region TreeNode Support: OnTreeviewMouseDown(), OnTreeNodeCollapsed(), OnTreeNodeExpanded(), OnTreeNodeSelected(), OnTreeNodeDoubleClicked()
		private void OnTreeviewMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for treeview mousedown event
			try {
				if(e.Button == MouseButtons.Right) {
					TreeNode node = this.trvMain.GetNodeAt(new Point(e.X, e.Y));
					this.trvMain.SelectedNode = node;
				}
			} 
			catch(Exception ex) {  App.ReportError(ex); }
		}
		private void OnTreeNodeCollapsed(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node collapsed - child nodes need to unload [stale] data
			try {
				AppNode node = (AppNode)e.Node;
				node.CollapseNode();
			}
			catch(Exception ex) {  App.ReportError(ex); }
		}
		private void OnTreeNodeExpanded(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node expanded - child nodes need to load [fresh] data
			try {
				AppNode node = (AppNode)e.Node;
				node.ExpandNode();
			}
			catch(Exception ex) {  App.ReportError(ex); }
		}
		private void OnTreeNodeSelected(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Event handler for node selected
			setUserServices();
		}
		private void OnTreeNodeDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for tree node double-clicked
			this.mnuFileOpen.PerformClick();
		}
		#endregion
		#region User Services: OnMenuItemClicked(), OnToolbarButtonClicked(), OnIconDoubleClick()
		private void OnMenuItemClicked(object sender, System.EventArgs e) {
			//Menu itemclicked-apply selected service
			winApp win=null;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_FILE_NEW:	break;
					case MNU_FILE_OPEN:				
						switch(this.mContext) {
							case CONTEXT_APPNODE: 
								this.Cursor = Cursors.WaitCursor;
								try {
									Form frm=null;
                                    Deployment oApp = (Deployment)this.trvMain.SelectedNode;
									for(int i=0; i< this.MdiChildren.Length; i++) {
										if(this.MdiChildren[i].Text == oApp.DisplayName) {
											frm = this.MdiChildren[i];
											break;
										}
									}
									if(frm == null) {
										this.mMessageMgr.AddMessage("Loading " + oApp.Name + " trace log...");
										win = new winApp(ref oApp);
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
										win.ErrorMessage += new ErrorEventHandler(OnErrorMessage);					
										win.Show();
									}
									else
										frm.Activate();
								}
								catch(InvalidCastException) { }
								break;
						}
						break;
					case MNU_FILE_SAVE:				
						this.mActiveApp.Save();
						break;
					case MNU_FILE_SAVEAS:			
						switch(this.mContext) {
							case CONTEXT_APPWIN: 
								this.mActiveApp.SaveAs();
								break;
						}
						break;
					case MNU_FILE_PRINT_SETTINGS:	this.mActiveApp.PageSettings(); break;
					case MNU_FILE_PRINT:			
						switch(this.mContext) {
							case CONTEXT_APPWIN: 
								this.Cursor = Cursors.WaitCursor;
								this.mActiveApp.Print(); 
								break;
						}
						break;
					case MNU_FILE_PROPERTIES:		
						switch(this.mContext) {
							case CONTEXT_APPNODE: break;
						}
						break;
					case MNU_FILE_EXIT:				this.Close(); break;
					case MNU_EDIT_CUT:				
						switch(this.mContext) {
							case CONTEXT_APPWIN: 
								this.mActiveApp.Cut(); 
								break;
						}
						break;
					case MNU_EDIT_COPY:				
						switch(this.mContext) {
							case CONTEXT_APPWIN: 
								this.mActiveApp.Copy(); 
								break;
						}
						break;
					case MNU_EDIT_PASTE:			
						switch(this.mContext) {
							case CONTEXT_APPWIN: 
								this.mActiveApp.Paste(); 
								break;
						}
						break;
					case MNU_EDIT_DELETE:			
						switch(this.mContext) {
							case CONTEXT_APPWIN: 
								this.mActiveApp.Delete();
								break;
						}
						break;
					case MNU_EDIT_FIND:				break;
					case MNU_VIEW_REFRESH:			
						this.Cursor = Cursors.WaitCursor;
						AppNode node = (AppNode)this.trvMain.SelectedNode; 
						node.Refresh(); 
						break;
					case MNU_VIEW_TOOLBAR:			this.tlbMain.Visible = (this.mnuViewToolbar.Checked = !this.mnuViewToolbar.Checked); break;
					case MNU_VIEW_STATUSBAR:		this.stbMain.Visible = (this.mnuViewStatusBar.Checked = !this.mnuViewStatusBar.Checked); break;
					case MNU_WIN_CASCADE:			this.LayoutMdi(MdiLayout.Cascade); break;
					case MNU_WIN_TILEHORIZ:			this.LayoutMdi(MdiLayout.TileHorizontal); break;
					case MNU_WIN_TILEVERT:			this.LayoutMdi(MdiLayout.TileVertical); break;
					case MNU_HELP_ABOUT:			new dlgAbout(App.Product + " Tool", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
					case MNU_ICON_HIDEWHENMINIMIZED:
						this.mTrayIcon.MenuItems[0].Checked = !this.mTrayIcon.MenuItems[0].Checked;
						if(this.WindowState == FormWindowState.Minimized) 
							this.Visible = !this.mTrayIcon.MenuItems[0].Checked;
						break;
					case MNU_ICON_SHOW:
						this.WindowState = FormWindowState.Maximized;
						this.Visible = true;
						this.Activate();
						break;
				}
			}
			catch(Exception ex)  {  App.ReportError(ex); }
			finally  { 
				setUserServices();
				this.Cursor = Cursors.Default; 
			}
		}
		private void OnToolbarButtonClicked(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//Toolbar handler
			try {
				switch(tlbMain.Buttons.IndexOf(e.Button)) {
					case TLB_NEW:		this.mnuFileNew.PerformClick(); break;
					case TLB_OPEN:		this.mnuFileOpen.PerformClick(); break;
					case TLB_SAVE:		this.mnuFileSave.PerformClick(); break;
					case TLB_PRINT:		this.mnuFilePrint.PerformClick(); break;
					case TLB_CUT:		this.mnuEditCut.PerformClick(); break;
					case TLB_COPY:		this.mnuEditCopy.PerformClick(); break;
					case TLB_PASTE:		this.mnuEditPaste.PerformClick(); break;
					case TLB_FIND:		this.mnuEditFind.PerformClick(); break;
					case TLB_REFRESH:	this.mnuViewRefresh.PerformClick(); break;
					default:			break;
				}
			}
			catch(Exception ex)  {  App.ReportError(ex); }
		}
		private void OnIconDoubleClick(object Sender, EventArgs e) {
			//Show the form when the user double clicks on the notify icon
			// Set the WindowState to normal if the form is minimized.
			this.WindowState = FormWindowState.Maximized;
			this.Visible = true;
			this.Activate();
		}
		#endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu(), onHelpMenuClick(), OnDataStatusUpdate()
		private void configApplication() {
			try {
				//Query application configuration
				//N/A
				
				//Create business objects with configuration values
				//N/A
			}
			catch(Exception ex) { throw ex; } 
		}
		private void setUserServices() {
			//Set user services
			bool canNew=false, canOpen=false, canSave=false, canSaveAs=false, canPrint=false;
			bool canCut=false, canCopy=false, canPaste=false;
			bool canProps=false, canDelete=false;
			try {
				//Determine active object
				this.mContext = "";
                if(this.trvMain.SelectedNode is Terminal)
					this.mContext = CONTEXT_TERMINAL;
                else if(this.trvMain.SelectedNode is Department)
					this.mContext = CONTEXT_TERMINAL;
                else if(this.trvMain.SelectedNode is ArgixApp)
					this.mContext = CONTEXT_TERMINAL;
                else if(this.trvMain.SelectedNode is DeploymentManifest)
                    this.mContext = CONTEXT_APPNODE;
                else if(this.trvMain.SelectedNode is Deployment)
					this.mContext = CONTEXT_APPNODE;

                if(this.mActiveApp != null && (this.mActiveApp.Focused || this.mActiveApp.ActiveControl.Focused)) 
					this.mContext = CONTEXT_APPWIN;
					
				//Set menu states based upon context
				try {
					this.stbMain.Panels[1].Width = 96;
					this.stbMain.Panels[1].Text = this.mContext;
				} catch { }
				switch(this.mContext) {
					case CONTEXT_TERMINAL: 
						break;
					case CONTEXT_APPNODE: 
						canOpen = true;
						canProps = true;
						break;
					case CONTEXT_APPWIN: 
						canOpen = true;
						if(this.mActiveApp != null) {
							canSave = this.mActiveApp.CanSave;
							canSaveAs = this.mActiveApp.CanSaveAs;
							canPrint = this.mActiveApp.CanPrint;
							canCut = this.mActiveApp.CanCut;
							canCopy = this.mActiveApp.CanCopy;
							canPaste = this.mActiveApp.CanPaste;
							canDelete = this.mActiveApp.CanDelete;
							canProps = false;
						}
						break;
				}
				//Set main menu and context menu states
				this.mnuFileNew.Enabled = this.btnNew.Enabled = this.ctxAppNew.Enabled = canNew;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = this.ctxAppOpen.Enabled = canOpen;
				this.mnuFileSave.Enabled = this.btnSave.Enabled = canSave;
				this.mnuFileSaveAs.Enabled = canSaveAs;
				this.mnuFilePageSettings.Enabled = this.mActiveApp != null;
				this.mnuFilePrint.Enabled = this.btnPrint.Enabled = canPrint;
				this.ctxAppProps.Enabled = canProps;
				this.mnuFileExit.Enabled = true;
				this.mnuEditCut.Enabled = this.btnCut.Enabled = canCut;
				this.mnuEditCopy.Enabled = this.btnCopy.Enabled = canCopy;
				this.mnuEditPaste.Enabled = this.btnPaste.Enabled = canPaste;
				this.mnuEditDelete.Enabled = this.ctxAppDelete.Enabled = canDelete;
				this.mnuEditFind.Enabled = this.btnFind.Enabled = false;
				this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = this.ctxAppRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuWinCascade.Enabled = this.mnuWinTileHoriz.Enabled = this.mnuWinTileVert.Enabled = true;
				this.mnuHelpAbout.Enabled = true;
			}
			catch(Exception ex)  {  App.ReportError(ex); }
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
					mnu.Click += new System.EventHandler(this.onHelpMenuClick);
					mnu.Enabled = (sValue != "");
					this.mnuHelp.MenuItems.Add(i, mnu);
				}
			}
			catch(Exception) { }
		}
		private void onHelpMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for configurable help menu items
			try {
				MenuItem mnu = (MenuItem)sender;
				Help.ShowHelp(this, this.mHelpItems.GetValues(mnu.Text)[0]);
			}
			catch(Exception) { }
		}
		public static void OnDataStatusUpdate(object sender, DataStatusArgs e) {
			//Event handler for notifications from child windows
			//Notify clients of refresh
			if(frmMain.DataStatusUpdate!=null) frmMain.DataStatusUpdate(null, e);
		}
		public void OnDataStatusUpdate2(object sender, DataStatusArgs e) {
			//Event handler for notifications from (global) frmMain class
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
		#endregion
		#region Log Window Control: OnMdiChildActivate(), OnScheduleActivated(), OnScheduleDeactivated(), OnScheduleClosing(), OnScheduleClosing()
		private void OnMdiChildActivate(object sender, EventArgs e) {
			//Event handler for change in mdi child collection
			setUserServices();
		}
		private void OnScheduleActivated(object sender, System.EventArgs e) {
			//Event handler for activaton of a viewer child window
			winApp win = (winApp)sender;
			this.mActiveApp = win;
			setUserServices();
		}						
		private void OnScheduleDeactivated(object sender, System.EventArgs e) {
			//Event handler for deactivaton of a viewer child window
			this.mActiveApp = null;
			setUserServices();
		}
		private void OnScheduleClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing via control box; e.Cancel=true keeps window open
			e.Cancel = false;
		}
		private void OnScheduleClosed(object sender, System.EventArgs e) {
			//Event handler for closing of a viewer child window
			setUserServices();
		}
		#endregion
		#region Log Window Services: OnServiceStatesChanged(), OnErrorMessage()
		private void OnServiceStatesChanged(object source, EventArgs e) { setUserServices(); }
		private void OnErrorMessage(object source, ErrorEventArgs e) { App.ReportError(e.Exception, e.DisplayMessage, e.Level); }
		#endregion
	}
}
