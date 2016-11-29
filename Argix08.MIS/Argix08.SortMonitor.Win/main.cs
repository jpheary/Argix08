//	File:	main.cs
//	Author:	J. Heary
//	Date:	10/28/04
//	Desc:	.
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
using System.Windows.Forms;
using System.IO;
using Argix.Data;
using Tsort.Enterprise;
using Tsort.Freight;
using Tsort.Sort;
using Argix;
using Argix.Windows;
using Argix.Windows.Printers;

namespace Tsort.Tools {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private winStation mActiveStation = null;
        private TrayIcon mTrayIcon = null;
		private WinPrinter mPrinter=null;
		private ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private string mHelpContents="";
		
		#region Constants
		//Main menu and context menu captions (serves as identifier in handlers)
        private const string MNU_FILE_NEW = "&New...";
        private const string MNU_FILE_OPEN = "&Open...";
		private const string MNU_FILE_PRINT_SETTINGS = "Page Set&up";
		private const string MNU_FILE_PRINT = "&Print Label";
		private const string MNU_FILE_EXIT = "E&xit";
		private const string MNU_VIEW_REFRESH = "Refresh";
		private const string MNU_VIEW_TOOLBAR = "&Toolbar";
		private const string MNU_VIEW_STATUSBAR = "&Status Bar";
		private const string MNU_TOOLS_FIND = "&Find...";
		private const string MNU_WIN_CASCADE = "&Cascade";
		private const string MNU_WIN_TILEHORIZ = "Tile Hori&zontally";
		private const string MNU_WIN_TILEVERT = "Tile &Vertically";
		private const string MNU_HELP_CONTENTS = "&Contents";
		private const string MNU_HELP_ABOUT = "&About Sort Monitor";
		
		private const string MNU_LOG_COPY = "Cop&y";
		private const string MNU_LOG_CLEAR = "Clear Al&l";
		
		private const string MNU_ICON_HIDEWHENMINIMIZED = "Hide When Minimized";
		private const string MNU_ICON_SHOW = "Open Sort Montor";
		
		//Toolbar constants
        private const int TLB_NEW = 0;
        private const int TLB_OPEN = 1;
        //Sep1
		private const int TLB_PRINT = 3;
        //Sep2
        private const int TLB_FIND = 5;
        private const int TLB_REFRESH = 6;
        
        private const string TABTLB_AUTOHIDE_OFF = "x";
		private const string TABTLB_AUTOHIDE_ON = "-";
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
		private System.Windows.Forms.StatusBarPanel pnlPortStatus;
		private System.Windows.Forms.MenuItem mnuHelpContents;
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
		private System.Windows.Forms.TabControl tabFlyout;
		private System.Windows.Forms.TabPage tabRealTime;
		private System.Windows.Forms.Button btnPinRealTime;
		private System.Windows.Forms.Label _lblRealTime;
		private System.Windows.Forms.Timer tmrAutoHide;
		private System.Windows.Forms.ToolBarButton btnOpen;
		private System.Windows.Forms.StatusBarPanel pnlRS232Status;
		private System.Windows.Forms.MenuItem mnuFilePrint;
		private System.Windows.Forms.MenuItem mnuViewSep0;
		private System.Windows.Forms.MenuItem mnuTools;
		private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.TabControl tabOther;
		private System.Windows.Forms.RichTextBox txtSearch;
		private System.Windows.Forms.ContextMenu ctxLog;
		private System.Windows.Forms.MenuItem ctxLogClear;
		private System.Windows.Forms.MenuItem mnuFileSep1;
		private System.Windows.Forms.MenuItem mnuFileSep2;
		private System.Windows.Forms.MenuItem mnuFilePageSettings;
        private System.Windows.Forms.StatusBarPanel pnlMessage;
        private System.Windows.Forms.ComboBox cboTerminal;
        private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.Label _lblAssignments;
        private System.Windows.Forms.TabPage tabHistorical;
		private System.Windows.Forms.Button btnPinHistorical;
		private System.Windows.Forms.Label _lblHistorical;
		private System.Windows.Forms.Label _lblStation;
		private System.Windows.Forms.TabPage tabSearchResults;
		private System.Windows.Forms.TabPage tabSearch;
		private System.Windows.Forms.Label _lblSearchIn;
		private System.Windows.Forms.Label _lblScan;
		private System.Windows.Forms.ComboBox cboSearchIn;
		private System.Windows.Forms.Button btnPinSearch;
		private System.Windows.Forms.Label _lblSearch;
		private System.Windows.Forms.TextBox txtScan;
		private System.Windows.Forms.Button cmdFindScan;
		private System.Windows.Forms.ToolBarButton btnSep1;
		private System.Windows.Forms.ToolBarButton btnRefresh;
		private System.Windows.Forms.MenuItem mnuViewRefresh;
		private System.Windows.Forms.MenuItem mnuToolsFind;
		private System.Windows.Forms.ContextMenu ctxAssignments;
		private System.Windows.Forms.MenuItem ctxAssignmentsRefresh;
		private System.Windows.Forms.Label _lblTerminal2;
		private System.Windows.Forms.Label lblTerminal;
		private System.Windows.Forms.ListView lsvStations;
		private System.Windows.Forms.ComboBox cboLogEvent;
		private System.Windows.Forms.Button cmdLogEvent;
		private System.Windows.Forms.Label _lblLogEvent;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker dtpLogStartDate;
		private System.Windows.Forms.MenuItem ctxLogCopy;
        private System.Windows.Forms.MenuItem ctxLogSep1;
		private System.Windows.Forms.TabControl tabAssignments;
		private System.Windows.Forms.TabPage tabDirect;
		private System.Windows.Forms.TabPage tabIndirect;
		private System.Windows.Forms.ListView lsvIndirectAssignments;
        private System.Windows.Forms.ListView lsvDirectAssignments;
        private MenuItem mnuFileNew;
        private ToolBarButton btnNew;
        private ToolBarButton btnPrint;
        private ToolBarButton btnSep2;
        private ToolBarButton btnFind;
		private System.ComponentModel.IContainer components;
		
		#endregion
		
		//Interface
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				UserSettings.RegistryKey = App.RegistryKey;
				#region Set menu identities (used for onclick handlers) 
                this.mnuFileNew.Text = MNU_FILE_NEW;
				this.mnuFileOpen.Text = MNU_FILE_OPEN;
				this.mnuFilePageSettings.Text = MNU_FILE_PRINT_SETTINGS;
				this.mnuFilePrint.Text = MNU_FILE_PRINT;
				this.mnuFileExit.Text = MNU_FILE_EXIT;
				this.mnuViewRefresh.Text = this.ctxAssignmentsRefresh.Text = MNU_VIEW_REFRESH;
				this.mnuViewToolbar.Text = MNU_VIEW_TOOLBAR;
				this.mnuViewStatusBar.Text = MNU_VIEW_STATUSBAR;
				this.mnuToolsFind.Text = MNU_TOOLS_FIND;
				this.mnuWinCascade.Text = MNU_WIN_CASCADE;
				this.mnuWinTileHoriz.Text = MNU_WIN_TILEHORIZ;
				this.mnuWinTileVert.Text = MNU_WIN_TILEVERT;
				this.mnuHelpContents.Text = MNU_HELP_CONTENTS;
				this.mnuHelpAbout.Text = MNU_HELP_ABOUT;
				
				this.ctxLogCopy.Text = MNU_LOG_COPY;
				this.ctxLogClear.Text = MNU_LOG_CLEAR;
				#endregion
				this.tabOther.Height = (int)UserSettings.Read("SearchHeight", "144");
				#region Window docking
				this.splitterV.MinExtra = 0;
				this.splitterV.MinSize = 18;
				this.splitterH.MinExtra = 24;
				this.splitterH.MinSize = 24;
				this.tlbMain.Dock = DockStyle.Top;
				this.splitterH.Dock = DockStyle.Bottom;
				this.tabOther.Dock = DockStyle.Bottom;
				this.splitterV.Dock = DockStyle.Right;
				this.tabFlyout.Dock = DockStyle.Right;
				this.stbMain.Dock = DockStyle.Bottom;
				this.Controls.AddRange(new Control[]{this.splitterH, this.tabOther, this.splitterV, this.tabFlyout, this.tlbMain, this.stbMain});
				#endregion
				InitializeToolbox();
				
				//Create services
				this.mTrayIcon = new TrayIcon("Sort Monitor", this.Icon);
				#region Tray Icon Context menu
				MenuItem ctxHide = new MenuItem(MNU_ICON_HIDEWHENMINIMIZED, new System.EventHandler(this.OnMenuClick));
				ctxHide.Index = 0;
				ctxHide.Checked = true;
				MenuItem ctxShow = new MenuItem(MNU_ICON_SHOW, new System.EventHandler(this.OnMenuClick));
				ctxShow.Index = 1;
				ctxShow.DefaultItem = true;
				this.mTrayIcon.MenuItems.AddRange(new MenuItem[] {ctxHide, ctxShow});
				#endregion
				this.mTrayIcon.DoubleClick += new System.EventHandler(OnIconDoubleClick);
				this.mPrinter = new WinPrinter("");
				this.mToolTip = new ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 5000, 5000);
				this.mHelpContents = ConfigurationManager.AppSettings.Get("HelpContents");
				
				//Set application configuration
				configApplication();
			} 
			catch(Exception ex) {  App.ReportError(ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
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
            this.mnuFilePageSettings = new System.Windows.Forms.MenuItem();
            this.mnuFilePrint = new System.Windows.Forms.MenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
            this.mnuFileExit = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuView = new System.Windows.Forms.MenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.MenuItem();
            this.mnuViewSep0 = new System.Windows.Forms.MenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.MenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.MenuItem();
            this.mnuTools = new System.Windows.Forms.MenuItem();
            this.mnuToolsFind = new System.Windows.Forms.MenuItem();
            this.mnuWindow = new System.Windows.Forms.MenuItem();
            this.mnuWinCascade = new System.Windows.Forms.MenuItem();
            this.mnuWinTileHoriz = new System.Windows.Forms.MenuItem();
            this.mnuWinTileVert = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpContents = new System.Windows.Forms.MenuItem();
            this.mnuHelpSep0 = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.splitterV = new System.Windows.Forms.Splitter();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.tlbMain = new System.Windows.Forms.ToolBar();
            this.btnOpen = new System.Windows.Forms.ToolBarButton();
            this.btnSep1 = new System.Windows.Forms.ToolBarButton();
            this.btnRefresh = new System.Windows.Forms.ToolBarButton();
            this.pnlMessage = new System.Windows.Forms.StatusBarPanel();
            this.pnlRS232Status = new System.Windows.Forms.StatusBarPanel();
            this.pnlPortStatus = new System.Windows.Forms.StatusBarPanel();
            this.tabFlyout = new System.Windows.Forms.TabControl();
            this.tabRealTime = new System.Windows.Forms.TabPage();
            this.tabAssignments = new System.Windows.Forms.TabControl();
            this.tabDirect = new System.Windows.Forms.TabPage();
            this.lsvDirectAssignments = new System.Windows.Forms.ListView();
            this.ctxAssignments = new System.Windows.Forms.ContextMenu();
            this.ctxAssignmentsRefresh = new System.Windows.Forms.MenuItem();
            this.tabIndirect = new System.Windows.Forms.TabPage();
            this.lsvIndirectAssignments = new System.Windows.Forms.ListView();
            this._lblAssignments = new System.Windows.Forms.Label();
            this._lblTerminal = new System.Windows.Forms.Label();
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this.btnPinRealTime = new System.Windows.Forms.Button();
            this._lblRealTime = new System.Windows.Forms.Label();
            this.tabHistorical = new System.Windows.Forms.TabPage();
            this.lsvStations = new System.Windows.Forms.ListView();
            this.lblTerminal = new System.Windows.Forms.Label();
            this._lblTerminal2 = new System.Windows.Forms.Label();
            this._lblStation = new System.Windows.Forms.Label();
            this.btnPinHistorical = new System.Windows.Forms.Button();
            this._lblHistorical = new System.Windows.Forms.Label();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpLogStartDate = new System.Windows.Forms.DateTimePicker();
            this.cmdLogEvent = new System.Windows.Forms.Button();
            this._lblLogEvent = new System.Windows.Forms.Label();
            this.cboLogEvent = new System.Windows.Forms.ComboBox();
            this.cmdFindScan = new System.Windows.Forms.Button();
            this.txtScan = new System.Windows.Forms.TextBox();
            this._lblSearchIn = new System.Windows.Forms.Label();
            this._lblScan = new System.Windows.Forms.Label();
            this.cboSearchIn = new System.Windows.Forms.ComboBox();
            this.btnPinSearch = new System.Windows.Forms.Button();
            this._lblSearch = new System.Windows.Forms.Label();
            this.tmrAutoHide = new System.Windows.Forms.Timer(this.components);
            this.splitterH = new System.Windows.Forms.Splitter();
            this.tabOther = new System.Windows.Forms.TabControl();
            this.tabSearchResults = new System.Windows.Forms.TabPage();
            this.txtSearch = new System.Windows.Forms.RichTextBox();
            this.ctxLog = new System.Windows.Forms.ContextMenu();
            this.ctxLogCopy = new System.Windows.Forms.MenuItem();
            this.ctxLogSep1 = new System.Windows.Forms.MenuItem();
            this.ctxLogClear = new System.Windows.Forms.MenuItem();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.btnNew = new System.Windows.Forms.ToolBarButton();
            this.btnSep2 = new System.Windows.Forms.ToolBarButton();
            this.btnPrint = new System.Windows.Forms.ToolBarButton();
            this.btnFind = new System.Windows.Forms.ToolBarButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMessage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRS232Status)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPortStatus)).BeginInit();
            this.tabFlyout.SuspendLayout();
            this.tabRealTime.SuspendLayout();
            this.tabAssignments.SuspendLayout();
            this.tabDirect.SuspendLayout();
            this.tabIndirect.SuspendLayout();
            this.tabHistorical.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabOther.SuspendLayout();
            this.tabSearchResults.SuspendLayout();
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
            this.mnuFilePageSettings,
            this.mnuFilePrint,
            this.mnuFileSep2,
            this.mnuFileExit});
            this.mnuFile.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Enabled = false;
            this.mnuFileNew.Index = 0;
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Enabled = false;
            this.mnuFileOpen.Index = 1;
            this.mnuFileOpen.Text = "Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Index = 2;
            this.mnuFileSep1.MergeOrder = 10;
            this.mnuFileSep1.Text = "-";
            // 
            // mnuFilePageSettings
            // 
            this.mnuFilePageSettings.Enabled = false;
            this.mnuFilePageSettings.Index = 3;
            this.mnuFilePageSettings.Text = "Page Settings...";
            this.mnuFilePageSettings.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Enabled = false;
            this.mnuFilePrint.Index = 4;
            this.mnuFilePrint.Text = "Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Index = 5;
            this.mnuFileSep2.Text = "-";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Index = 6;
            this.mnuFileExit.MergeOrder = 11;
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.MergeOrder = 1;
            this.mnuEdit.MergeType = System.Windows.Forms.MenuMerge.MergeItems;
            this.mnuEdit.Text = "&Edit";
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
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnMenuClick);
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
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Enabled = false;
            this.mnuViewStatusBar.Index = 3;
            this.mnuViewStatusBar.MergeOrder = 17;
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuTools
            // 
            this.mnuTools.Index = 3;
            this.mnuTools.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuToolsFind});
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsFind
            // 
            this.mnuToolsFind.Enabled = false;
            this.mnuToolsFind.Index = 0;
            this.mnuToolsFind.Text = "Find...";
            this.mnuToolsFind.Click += new System.EventHandler(this.OnMenuClick);
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
            this.mnuWinCascade.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuWinTileHoriz
            // 
            this.mnuWinTileHoriz.Index = 1;
            this.mnuWinTileHoriz.MergeOrder = 12;
            this.mnuWinTileHoriz.Text = "Tile Horizontally";
            this.mnuWinTileHoriz.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuWinTileVert
            // 
            this.mnuWinTileVert.Index = 2;
            this.mnuWinTileVert.MergeOrder = 13;
            this.mnuWinTileVert.Text = "Tile Vertically";
            this.mnuWinTileVert.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.Index = 5;
            this.mnuHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuHelpContents,
            this.mnuHelpSep0,
            this.mnuHelpAbout});
            this.mnuHelp.MergeOrder = 6;
            this.mnuHelp.MergeType = System.Windows.Forms.MenuMerge.Replace;
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpContents
            // 
            this.mnuHelpContents.Index = 0;
            this.mnuHelpContents.Text = "&Contents";
            this.mnuHelpContents.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuHelpSep0
            // 
            this.mnuHelpSep0.Index = 1;
            this.mnuHelpSep0.Text = "-";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Enabled = false;
            this.mnuHelpAbout.Index = 2;
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // splitterV
            // 
            this.splitterV.Location = new System.Drawing.Point(0,28);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(3,325);
            this.splitterV.TabIndex = 3;
            this.splitterV.TabStop = false;
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0,"NEW.BMP");
            this.imgMain.Images.SetKeyName(1,"OPEN.BMP");
            this.imgMain.Images.SetKeyName(2,"PRINT.BMP");
            this.imgMain.Images.SetKeyName(3,"FIND.BMP");
            this.imgMain.Images.SetKeyName(4,"");
            this.imgMain.Images.SetKeyName(5,"");
            this.imgMain.Images.SetKeyName(6,"");
            // 
            // tlbMain
            // 
            this.tlbMain.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.tlbMain.BackColor = System.Drawing.SystemColors.Control;
            this.tlbMain.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnPrint,
            this.btnSep2,
            this.btnFind,
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
            this.tlbMain.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.OnToolbarButtonClick);
            // 
            // btnOpen
            // 
            this.btnOpen.ImageIndex = 1;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.btnOpen.ToolTipText = "Open station";
            // 
            // btnSep1
            // 
            this.btnSep1.ImageIndex = 1;
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnRefresh
            // 
            this.btnRefresh.ImageIndex = 4;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.ToolTipText = "Refresh sort station";
            // 
            // pnlMessage
            // 
            this.pnlMessage.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.pnlMessage.Name = "pnlMessage";
            this.pnlMessage.Width = 343;
            // 
            // pnlRS232Status
            // 
            this.pnlRS232Status.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlRS232Status.Icon")));
            this.pnlRS232Status.Name = "pnlRS232Status";
            this.pnlRS232Status.Text = "RS232:";
            this.pnlRS232Status.Width = 192;
            // 
            // pnlPortStatus
            // 
            this.pnlPortStatus.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlPortStatus.Icon")));
            this.pnlPortStatus.MinWidth = 96;
            this.pnlPortStatus.Name = "pnlPortStatus";
            this.pnlPortStatus.Text = "Port:";
            this.pnlPortStatus.Width = 192;
            // 
            // tabFlyout
            // 
            this.tabFlyout.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabFlyout.Controls.Add(this.tabRealTime);
            this.tabFlyout.Controls.Add(this.tabHistorical);
            this.tabFlyout.Controls.Add(this.tabSearch);
            this.tabFlyout.Dock = System.Windows.Forms.DockStyle.Right;
            this.tabFlyout.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabFlyout.ItemSize = new System.Drawing.Size(42,24);
            this.tabFlyout.Location = new System.Drawing.Point(472,28);
            this.tabFlyout.Multiline = true;
            this.tabFlyout.Name = "tabFlyout";
            this.tabFlyout.SelectedIndex = 0;
            this.tabFlyout.ShowToolTips = true;
            this.tabFlyout.Size = new System.Drawing.Size(192,325);
            this.tabFlyout.TabIndex = 94;
            // 
            // tabRealTime
            // 
            this.tabRealTime.BackColor = System.Drawing.SystemColors.Control;
            this.tabRealTime.Controls.Add(this.tabAssignments);
            this.tabRealTime.Controls.Add(this._lblAssignments);
            this.tabRealTime.Controls.Add(this._lblTerminal);
            this.tabRealTime.Controls.Add(this.cboTerminal);
            this.tabRealTime.Controls.Add(this.btnPinRealTime);
            this.tabRealTime.Controls.Add(this._lblRealTime);
            this.tabRealTime.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabRealTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabRealTime.Location = new System.Drawing.Point(4,4);
            this.tabRealTime.Name = "tabRealTime";
            this.tabRealTime.Size = new System.Drawing.Size(160,317);
            this.tabRealTime.TabIndex = 0;
            this.tabRealTime.Text = "Real Time";
            // 
            // tabAssignments
            // 
            this.tabAssignments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabAssignments.Controls.Add(this.tabDirect);
            this.tabAssignments.Controls.Add(this.tabIndirect);
            this.tabAssignments.Location = new System.Drawing.Point(3,103);
            this.tabAssignments.Name = "tabAssignments";
            this.tabAssignments.SelectedIndex = 0;
            this.tabAssignments.Size = new System.Drawing.Size(153,212);
            this.tabAssignments.TabIndex = 117;
            // 
            // tabDirect
            // 
            this.tabDirect.Controls.Add(this.lsvDirectAssignments);
            this.tabDirect.Location = new System.Drawing.Point(4,22);
            this.tabDirect.Name = "tabDirect";
            this.tabDirect.Size = new System.Drawing.Size(145,186);
            this.tabDirect.TabIndex = 0;
            this.tabDirect.Text = "Direct";
            this.tabDirect.UseVisualStyleBackColor = true;
            // 
            // lsvDirectAssignments
            // 
            this.lsvDirectAssignments.ContextMenu = this.ctxAssignments;
            this.lsvDirectAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvDirectAssignments.Location = new System.Drawing.Point(0,0);
            this.lsvDirectAssignments.Name = "lsvDirectAssignments";
            this.lsvDirectAssignments.Size = new System.Drawing.Size(145,186);
            this.lsvDirectAssignments.TabIndex = 114;
            this.lsvDirectAssignments.UseCompatibleStateImageBehavior = false;
            this.lsvDirectAssignments.DoubleClick += new System.EventHandler(this.OnAssignmentSelected);
            // 
            // ctxAssignments
            // 
            this.ctxAssignments.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ctxAssignmentsRefresh});
            // 
            // ctxAssignmentsRefresh
            // 
            this.ctxAssignmentsRefresh.Index = 0;
            this.ctxAssignmentsRefresh.Text = "Refresh";
            this.ctxAssignmentsRefresh.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // tabIndirect
            // 
            this.tabIndirect.Controls.Add(this.lsvIndirectAssignments);
            this.tabIndirect.Location = new System.Drawing.Point(4,22);
            this.tabIndirect.Name = "tabIndirect";
            this.tabIndirect.Size = new System.Drawing.Size(145,186);
            this.tabIndirect.TabIndex = 1;
            this.tabIndirect.Text = "Indirect";
            this.tabIndirect.UseVisualStyleBackColor = true;
            // 
            // lsvIndirectAssignments
            // 
            this.lsvIndirectAssignments.ContextMenu = this.ctxAssignments;
            this.lsvIndirectAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvIndirectAssignments.Location = new System.Drawing.Point(0,0);
            this.lsvIndirectAssignments.Name = "lsvIndirectAssignments";
            this.lsvIndirectAssignments.Size = new System.Drawing.Size(145,186);
            this.lsvIndirectAssignments.TabIndex = 113;
            this.lsvIndirectAssignments.UseCompatibleStateImageBehavior = false;
            this.lsvIndirectAssignments.DoubleClick += new System.EventHandler(this.OnAssignmentSelected);
            // 
            // _lblAssignments
            // 
            this._lblAssignments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblAssignments.Location = new System.Drawing.Point(3,82);
            this._lblAssignments.Name = "_lblAssignments";
            this._lblAssignments.Size = new System.Drawing.Size(153,18);
            this._lblAssignments.TabIndex = 116;
            this._lblAssignments.Text = "Assignments";
            // 
            // _lblTerminal
            // 
            this._lblTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblTerminal.Location = new System.Drawing.Point(3,30);
            this._lblTerminal.Name = "_lblTerminal";
            this._lblTerminal.Size = new System.Drawing.Size(153,18);
            this._lblTerminal.TabIndex = 114;
            this._lblTerminal.Text = "Terminal";
            // 
            // cboTerminal
            // 
            this.cboTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminal.Location = new System.Drawing.Point(3,48);
            this.cboTerminal.Name = "cboTerminal";
            this.cboTerminal.Size = new System.Drawing.Size(153,21);
            this.cboTerminal.TabIndex = 112;
            this.cboTerminal.SelectionChangeCommitted += new System.EventHandler(this.OnTerminalChanged);
            // 
            // btnPinRealTime
            // 
            this.btnPinRealTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPinRealTime.BackColor = System.Drawing.SystemColors.Control;
            this.btnPinRealTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPinRealTime.Font = new System.Drawing.Font("Arial",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnPinRealTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPinRealTime.Location = new System.Drawing.Point(138,0);
            this.btnPinRealTime.Name = "btnPinRealTime";
            this.btnPinRealTime.Size = new System.Drawing.Size(18,18);
            this.btnPinRealTime.TabIndex = 110;
            this.btnPinRealTime.UseVisualStyleBackColor = false;
            // 
            // _lblRealTime
            // 
            this._lblRealTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblRealTime.BackColor = System.Drawing.SystemColors.Control;
            this._lblRealTime.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblRealTime.Location = new System.Drawing.Point(0,0);
            this._lblRealTime.Name = "_lblRealTime";
            this._lblRealTime.Size = new System.Drawing.Size(156,18);
            this._lblRealTime.TabIndex = 0;
            this._lblRealTime.Text = "Real Time";
            this._lblRealTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabHistorical
            // 
            this.tabHistorical.Controls.Add(this.lsvStations);
            this.tabHistorical.Controls.Add(this.lblTerminal);
            this.tabHistorical.Controls.Add(this._lblTerminal2);
            this.tabHistorical.Controls.Add(this._lblStation);
            this.tabHistorical.Controls.Add(this.btnPinHistorical);
            this.tabHistorical.Controls.Add(this._lblHistorical);
            this.tabHistorical.Location = new System.Drawing.Point(4,4);
            this.tabHistorical.Name = "tabHistorical";
            this.tabHistorical.Size = new System.Drawing.Size(160,317);
            this.tabHistorical.TabIndex = 1;
            this.tabHistorical.Text = "Historical";
            // 
            // lsvStations
            // 
            this.lsvStations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvStations.ContextMenu = this.ctxAssignments;
            this.lsvStations.Location = new System.Drawing.Point(3,103);
            this.lsvStations.Name = "lsvStations";
            this.lsvStations.Size = new System.Drawing.Size(153,212);
            this.lsvStations.TabIndex = 125;
            this.lsvStations.UseCompatibleStateImageBehavior = false;
            this.lsvStations.DoubleClick += new System.EventHandler(this.OnStationSelected);
            // 
            // lblTerminal
            // 
            this.lblTerminal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTerminal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTerminal.Location = new System.Drawing.Point(3,48);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new System.Drawing.Size(153,21);
            this.lblTerminal.TabIndex = 124;
            this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTerminal2
            // 
            this._lblTerminal2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblTerminal2.Location = new System.Drawing.Point(3,30);
            this._lblTerminal2.Name = "_lblTerminal2";
            this._lblTerminal2.Size = new System.Drawing.Size(153,18);
            this._lblTerminal2.TabIndex = 123;
            this._lblTerminal2.Text = "Terminal";
            // 
            // _lblStation
            // 
            this._lblStation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblStation.Location = new System.Drawing.Point(3,82);
            this._lblStation.Name = "_lblStation";
            this._lblStation.Size = new System.Drawing.Size(153,18);
            this._lblStation.TabIndex = 121;
            this._lblStation.Text = "Station";
            // 
            // btnPinHistorical
            // 
            this.btnPinHistorical.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPinHistorical.BackColor = System.Drawing.SystemColors.Control;
            this.btnPinHistorical.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPinHistorical.Font = new System.Drawing.Font("Arial",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnPinHistorical.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPinHistorical.Location = new System.Drawing.Point(138,0);
            this.btnPinHistorical.Name = "btnPinHistorical";
            this.btnPinHistorical.Size = new System.Drawing.Size(18,18);
            this.btnPinHistorical.TabIndex = 118;
            this.btnPinHistorical.UseVisualStyleBackColor = false;
            // 
            // _lblHistorical
            // 
            this._lblHistorical.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblHistorical.BackColor = System.Drawing.SystemColors.Control;
            this._lblHistorical.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblHistorical.Location = new System.Drawing.Point(0,0);
            this._lblHistorical.Name = "_lblHistorical";
            this._lblHistorical.Size = new System.Drawing.Size(156,18);
            this._lblHistorical.TabIndex = 117;
            this._lblHistorical.Text = "Historical";
            this._lblHistorical.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.label1);
            this.tabSearch.Controls.Add(this.dtpLogStartDate);
            this.tabSearch.Controls.Add(this.cmdLogEvent);
            this.tabSearch.Controls.Add(this._lblLogEvent);
            this.tabSearch.Controls.Add(this.cboLogEvent);
            this.tabSearch.Controls.Add(this.cmdFindScan);
            this.tabSearch.Controls.Add(this.txtScan);
            this.tabSearch.Controls.Add(this._lblSearchIn);
            this.tabSearch.Controls.Add(this._lblScan);
            this.tabSearch.Controls.Add(this.cboSearchIn);
            this.tabSearch.Controls.Add(this.btnPinSearch);
            this.tabSearch.Controls.Add(this._lblSearch);
            this.tabSearch.Location = new System.Drawing.Point(4,4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(160,317);
            this.tabSearch.TabIndex = 2;
            this.tabSearch.Text = "Search";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(3,240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153,18);
            this.label1.TabIndex = 134;
            this.label1.Text = "Search From";
            // 
            // dtpLogStartDate
            // 
            this.dtpLogStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpLogStartDate.CausesValidation = false;
            this.dtpLogStartDate.CustomFormat = "MM-dd-yyyy";
            this.dtpLogStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpLogStartDate.Location = new System.Drawing.Point(3,258);
            this.dtpLogStartDate.MaxDate = new System.DateTime(2028,12,31,0,0,0,0);
            this.dtpLogStartDate.MinDate = new System.DateTime(2004,1,1,0,0,0,0);
            this.dtpLogStartDate.Name = "dtpLogStartDate";
            this.dtpLogStartDate.Size = new System.Drawing.Size(153,21);
            this.dtpLogStartDate.TabIndex = 133;
            // 
            // cmdLogEvent
            // 
            this.cmdLogEvent.Location = new System.Drawing.Point(3,288);
            this.cmdLogEvent.Name = "cmdLogEvent";
            this.cmdLogEvent.Size = new System.Drawing.Size(96,24);
            this.cmdLogEvent.TabIndex = 132;
            this.cmdLogEvent.Text = "Find Events";
            this.cmdLogEvent.Click += new System.EventHandler(this.OnFindEvent);
            // 
            // _lblLogEvent
            // 
            this._lblLogEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblLogEvent.Location = new System.Drawing.Point(3,192);
            this._lblLogEvent.Name = "_lblLogEvent";
            this._lblLogEvent.Size = new System.Drawing.Size(153,18);
            this._lblLogEvent.TabIndex = 131;
            this._lblLogEvent.Text = "Log Event";
            // 
            // cboLogEvent
            // 
            this.cboLogEvent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLogEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLogEvent.Location = new System.Drawing.Point(3,210);
            this.cboLogEvent.Name = "cboLogEvent";
            this.cboLogEvent.Size = new System.Drawing.Size(153,21);
            this.cboLogEvent.Sorted = true;
            this.cboLogEvent.TabIndex = 130;
            // 
            // cmdFindScan
            // 
            this.cmdFindScan.Location = new System.Drawing.Point(3,126);
            this.cmdFindScan.Name = "cmdFindScan";
            this.cmdFindScan.Size = new System.Drawing.Size(96,24);
            this.cmdFindScan.TabIndex = 129;
            this.cmdFindScan.Text = "Find Scans";
            this.cmdFindScan.Click += new System.EventHandler(this.OnFindScan);
            // 
            // txtScan
            // 
            this.txtScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScan.Location = new System.Drawing.Point(3,48);
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(153,21);
            this.txtScan.TabIndex = 128;
            this.txtScan.TextChanged += new System.EventHandler(this.OnScanChanged);
            // 
            // _lblSearchIn
            // 
            this._lblSearchIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblSearchIn.Location = new System.Drawing.Point(3,78);
            this._lblSearchIn.Name = "_lblSearchIn";
            this._lblSearchIn.Size = new System.Drawing.Size(153,18);
            this._lblSearchIn.TabIndex = 127;
            this._lblSearchIn.Text = "Search In";
            // 
            // _lblScan
            // 
            this._lblScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblScan.Location = new System.Drawing.Point(3,30);
            this._lblScan.Name = "_lblScan";
            this._lblScan.Size = new System.Drawing.Size(153,18);
            this._lblScan.TabIndex = 126;
            this._lblScan.Text = "Carton Scan";
            // 
            // cboSearchIn
            // 
            this.cboSearchIn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSearchIn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSearchIn.Location = new System.Drawing.Point(3,96);
            this.cboSearchIn.Name = "cboSearchIn";
            this.cboSearchIn.Size = new System.Drawing.Size(153,21);
            this.cboSearchIn.Sorted = true;
            this.cboSearchIn.TabIndex = 125;
            this.cboSearchIn.SelectionChangeCommitted += new System.EventHandler(this.OnSearchChanged);
            // 
            // btnPinSearch
            // 
            this.btnPinSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPinSearch.BackColor = System.Drawing.SystemColors.Control;
            this.btnPinSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPinSearch.Font = new System.Drawing.Font("Arial",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnPinSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPinSearch.Location = new System.Drawing.Point(138,0);
            this.btnPinSearch.Name = "btnPinSearch";
            this.btnPinSearch.Size = new System.Drawing.Size(18,18);
            this.btnPinSearch.TabIndex = 124;
            this.btnPinSearch.UseVisualStyleBackColor = false;
            // 
            // _lblSearch
            // 
            this._lblSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblSearch.BackColor = System.Drawing.SystemColors.Control;
            this._lblSearch.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblSearch.Location = new System.Drawing.Point(0,0);
            this._lblSearch.Name = "_lblSearch";
            this._lblSearch.Size = new System.Drawing.Size(156,18);
            this._lblSearch.TabIndex = 123;
            this._lblSearch.Text = "Search";
            this._lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Location = new System.Drawing.Point(3,28);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(3,325);
            this.splitterH.TabIndex = 96;
            this.splitterH.TabStop = false;
            // 
            // tabOther
            // 
            this.tabOther.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabOther.Controls.Add(this.tabSearchResults);
            this.tabOther.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabOther.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabOther.ItemSize = new System.Drawing.Size(42,24);
            this.tabOther.Location = new System.Drawing.Point(6,209);
            this.tabOther.Multiline = true;
            this.tabOther.Name = "tabOther";
            this.tabOther.SelectedIndex = 0;
            this.tabOther.ShowToolTips = true;
            this.tabOther.Size = new System.Drawing.Size(466,144);
            this.tabOther.TabIndex = 98;
            this.tabOther.Resize += new System.EventHandler(this.OnSearchTabResized);
            // 
            // tabSearchResults
            // 
            this.tabSearchResults.BackColor = System.Drawing.SystemColors.Control;
            this.tabSearchResults.Controls.Add(this.txtSearch);
            this.tabSearchResults.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabSearchResults.ForeColor = System.Drawing.Color.Navy;
            this.tabSearchResults.Location = new System.Drawing.Point(4,4);
            this.tabSearchResults.Name = "tabSearchResults";
            this.tabSearchResults.Size = new System.Drawing.Size(458,112);
            this.tabSearchResults.TabIndex = 0;
            this.tabSearchResults.Text = "Search Results";
            // 
            // txtSearch
            // 
            this.txtSearch.AutoWordSelection = true;
            this.txtSearch.ContextMenu = this.ctxLog;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Courier New",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtSearch.Location = new System.Drawing.Point(0,0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.ReadOnly = true;
            this.txtSearch.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.txtSearch.ShowSelectionMargin = true;
            this.txtSearch.Size = new System.Drawing.Size(458,112);
            this.txtSearch.TabIndex = 111;
            this.txtSearch.Text = "";
            this.txtSearch.WordWrap = false;
            // 
            // ctxLog
            // 
            this.ctxLog.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.ctxLogCopy,
            this.ctxLogSep1,
            this.ctxLogClear});
            // 
            // ctxLogCopy
            // 
            this.ctxLogCopy.Index = 0;
            this.ctxLogCopy.Text = "Cop&y";
            this.ctxLogCopy.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxLogSep1
            // 
            this.ctxLogSep1.Index = 1;
            this.ctxLogSep1.Text = "-";
            // 
            // ctxLogClear
            // 
            this.ctxLogClear.Index = 2;
            this.ctxLogClear.Text = "Clear Al&l";
            this.ctxLogClear.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,353);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(664,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 100;
            this.stbMain.TerminalText = "";
            // 
            // btnNew
            // 
            this.btnNew.ImageIndex = 0;
            this.btnNew.Name = "btnNew";
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // btnPrint
            // 
            this.btnPrint.ImageIndex = 2;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.ToolTipText = "Print...";
            // 
            // btnFind
            // 
            this.btnFind.ImageIndex = 3;
            this.btnFind.Name = "btnFind";
            this.btnFind.ToolTipText = "Find...";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(664,377);
            this.Controls.Add(this.tabOther);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.tabFlyout);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sort Monitor";
            this.Closed += new System.EventHandler(this.OnFormClosed);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMessage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRS232Status)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlPortStatus)).EndInit();
            this.tabFlyout.ResumeLayout(false);
            this.tabRealTime.ResumeLayout(false);
            this.tabAssignments.ResumeLayout(false);
            this.tabDirect.ResumeLayout(false);
            this.tabIndirect.ResumeLayout(false);
            this.tabHistorical.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.tabOther.ResumeLayout(false);
            this.tabSearchResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		[STAThread]
		static void Main() {
			//The main entry point for the application
			try {
				Application.Run(new frmMain());
			}
			catch(Exception ex) { MessageBox.Show(ex.ToString()); }
		}

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			string sWindowSettings="";
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
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
				
                #region RealTime Configuration
				this.mMessageMgr.AddMessage("Configuring real-time views...");
				this.lsvDirectAssignments.Columns.Add("Station#", 72, HorizontalAlignment.Left);
				this.lsvDirectAssignments.Columns.Add("FreightID", 96, HorizontalAlignment.Left);
				this.lsvDirectAssignments.Columns.Add("SortType", 72, HorizontalAlignment.Left);
				this.lsvDirectAssignments.View = View.Details;
				this.lsvIndirectAssignments.Columns.Add("Station#", 72, HorizontalAlignment.Left);
				this.lsvIndirectAssignments.Columns.Add("Trip#", 120, HorizontalAlignment.Left);
				this.lsvIndirectAssignments.View = View.Details;
                #endregion
                #region Historical Configuration
				this.mMessageMgr.AddMessage("Configuring historical views...");
				this.mMessageMgr.AddMessage("Loading stations for " + this.cboTerminal.Text + " terminal...");
				this.lsvStations.Columns.Add("#", 72, HorizontalAlignment.Left);
				this.lsvStations.Columns.Add("Name", 96, HorizontalAlignment.Left);
				this.lsvStations.View = View.Details;
                #endregion
                #region Search Configuration
				this.mMessageMgr.AddMessage("Configuring search views...");
                this.cboSearchIn.Items.Add(App.SEARCH_SORTEDITEMS);
                this.cboSearchIn.Items.Add(App.SEARCH_SORTEDITEMSARCHIVE);
				this.cboSearchIn.Items.Add(App.SEARCH_BEARWARESCANS);
				this.cboSearchIn.Items.Add(App.SEARCH_ARGIXLOG);
				this.cboSearchIn.SelectedIndex = 0;
				this.cmdFindScan.Enabled = false;

				//Set log event search entries
                //Hashtable oFilters = EnterpriseFactory.LogEvents;
                //oEnum = oFilters.GetEnumerator();
                //while(oEnum.MoveNext()) {
                //    DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
                //    this.cboLogEvent.Items.Add(entry.Value.ToString());
                //}
                //this.cboLogEvent.SelectedIndex = 0;
                #endregion
            }
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
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
			catch { }
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for change in form size
			try {
				if(this.WindowState == FormWindowState.Minimized)
					this.Visible = !this.mTrayIcon.MenuItems[0].Checked;
			} catch { }
		}
        private void OnTerminalsChanged(object sender, EventArgs e) {
            //Event handler for change in terminal selections
            this.mMessageMgr.AddMessage("Loading terminals...");
        	this.cboTerminal.Items.Clear();
            Hashtable terminals = EnterpriseFactory.Terminals;
            IDictionaryEnumerator oEnum = terminals.GetEnumerator();
            while(oEnum.MoveNext()) {
                DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
                EnterpriseTerminal terminal = (EnterpriseTerminal)entry.Value;
                this.cboTerminal.Items.Add(terminal);
            }
            this.cboTerminal.DisplayMember = "Description";
            this.cboTerminal.ValueMember = "TerminalID";
			this.cboTerminal.Refresh();
            if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
            OnTerminalChanged(this.cboTerminal, EventArgs.Empty);
        }
		private void OnTerminalChanged(object sender, System.EventArgs e) {
			//Event handler for change in selected terminal
			this.Cursor = Cursors.WaitCursor;
			try {
				//
				if(this.cboTerminal.SelectedItem != null) {
                    this.mMessageMgr.AddMessage("Configuring for " + this.cboTerminal.Text + " terminal...");
                    EnterpriseTerminal terminal = (EnterpriseTerminal)this.cboTerminal.SelectedItem;
                    if(terminal.GetType().Name == "TsortTerminal") {
                        TsortTerminal tterminal = (TsortTerminal)terminal;
                        tterminal.DirectAssignmentsChanged -= new EventHandler(this.OnAssignmentsChanged);
                        tterminal.DirectAssignmentsChanged += new EventHandler(this.OnAssignmentsChanged);
                        tterminal.RefreshDirectAssignments();
                        if(this.tabAssignments.TabPages.Contains(this.tabIndirect)) this.tabAssignments.TabPages.Remove(this.tabIndirect);
                    }
                    else if(terminal.GetType().Name == "LocalTerminal") {
                        LocalTerminal lterminal = (LocalTerminal)terminal;
                        lterminal.DirectAssignmentsChanged -= new EventHandler(this.OnAssignmentsChanged);
                        lterminal.DirectAssignmentsChanged += new EventHandler(this.OnAssignmentsChanged);
                        lterminal.RefreshDirectAssignments();
                        lterminal.IndirectAssignmentsChanged -= new EventHandler(this.OnAssignmentsChanged);
                        lterminal.IndirectAssignmentsChanged += new EventHandler(this.OnAssignmentsChanged);
                        lterminal.RefreshIndirectAssignments();
                        if(!this.tabAssignments.TabPages.Contains(this.tabIndirect)) this.tabAssignments.TabPages.Add(this.tabIndirect);                   
                    }
                    this.stbMain.SetTerminalPanel(terminal.TerminalID.ToString(), terminal.Description);
                    this.lblTerminal.Text = this.cboTerminal.Text;

                    this.mMessageMgr.AddMessage("Refreshing " + this.cboTerminal.Text + " sort stations...");
                    EnterpriseFactory.RefreshSortStations();
                }
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#region Real Time: OnAssignmentsChanged(), OnAssignmentSelected()
		private void OnAssignmentsChanged(object sender, EventArgs e) {
			//View active station assignments for the selected terminal
            if(sender.GetType().Name == "TsortTerminal") {
                TsortTerminal tterminal = (TsortTerminal)sender;
			    this.mMessageMgr.AddMessage("Loading direct station assignments for " + tterminal.Description + "...");
                this.lsvDirectAssignments.Items.Clear();
		        IDictionaryEnumerator oEnum = tterminal.DirectAssignments.GetEnumerator();
		        while(oEnum.MoveNext()) {
			        DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
			        StationFreightAssignment assignment = (StationFreightAssignment)entry.Value;
			        ListViewItem item = this.lsvDirectAssignments.Items.Add(assignment.Station.Number);
			        item.SubItems.AddRange(new string[]{assignment.Freight.FreightID, assignment.SortType});
		        }
		        this.lsvDirectAssignments.Sorting = SortOrder.Ascending;
		        this.lsvDirectAssignments.Sort();
		        this.lsvDirectAssignments.Refresh();
            }
            else if(sender.GetType().Name == "LocalTerminal") {
                LocalTerminal lterminal = (LocalTerminal)sender;
			    this.mMessageMgr.AddMessage("Loading direct station assignments for " + lterminal.Description + "...");
                this.lsvDirectAssignments.Items.Clear();
		        IDictionaryEnumerator oEnum = lterminal.DirectAssignments.GetEnumerator();
		        while(oEnum.MoveNext()) {
			        DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
			        StationFreightAssignment assignment = (StationFreightAssignment)entry.Value;
			        ListViewItem item = this.lsvDirectAssignments.Items.Add(assignment.Station.Number);
			        item.SubItems.AddRange(new string[]{assignment.Freight.FreightID, assignment.SortType});
		        }
		        this.lsvDirectAssignments.Sorting = SortOrder.Ascending;
		        this.lsvDirectAssignments.Sort();
		        this.lsvDirectAssignments.Refresh();
			    
                this.mMessageMgr.AddMessage("Loading indirect station assignments for " + lterminal.Description + "...");
                this.lsvIndirectAssignments.Items.Clear();
		        oEnum = lterminal.IndirectAssignments.GetEnumerator();
		        while(oEnum.MoveNext()) {
			        DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
			        StationFreightAssignment assignment = (StationFreightAssignment)entry.Value;
			        ListViewItem item = this.lsvIndirectAssignments.Items.Add(assignment.Station.Number);
			        item.SubItems.Add(assignment.Freight.FreightID);
		        }
		        this.lsvIndirectAssignments.Sorting = SortOrder.Ascending;
		        this.lsvIndirectAssignments.Sort();
		        this.lsvIndirectAssignments.Refresh();
            }		    
		}
        private void OnAssignmentSelected(object sender, System.EventArgs e) {
			//Event handler for assignment selected
			this.Cursor = Cursors.WaitCursor;
			try {
                //Determine the listview that had a selection
                ListView lsv=null;
            	Form frm=null;
                StationFreightAssignment assignment=null;
                if(this.tabAssignments.SelectedTab == this.tabDirect) {
                    TsortTerminal tterminal = (TsortTerminal)this.cboTerminal.SelectedItem;
                    lsv = this.lsvDirectAssignments;
                    
                    //Check if a real-time window is open for this assignment & station; otherwise create
                    string stationNumber = lsv.SelectedItems[0].Text.Trim();
                    string freightID = lsv.SelectedItems[0].SubItems[1].Text.Trim();
                    for(int i=0; i< this.MdiChildren.Length; i++) {
                        winStation win = (winStation)this.MdiChildren[i];
				        if(win.Assignment != null && win.Assignment.Freight.FreightID == freightID && win.Station.Number == stationNumber) {
					        frm = win;
					        break;
				        }
			        }
                    if(frm == null) assignment = (StationFreightAssignment)tterminal.DirectAssignments[stationNumber + freightID];
                }
                else if(this.tabAssignments.SelectedTab == this.tabIndirect) {
                    LocalTerminal lterminal = (LocalTerminal)this.cboTerminal.SelectedItem;
                    lsv = this.lsvIndirectAssignments;
                    
                    //Check if a real-time window is open for this assignment & station; otherwise create
                    string stationNumber = lsv.SelectedItems[0].Text.Trim();
                    string freightID = lsv.SelectedItems[0].SubItems[1].Text.Trim();
                    for(int i=0; i< this.MdiChildren.Length; i++) {
                        winStation win = (winStation)this.MdiChildren[i];
				        if(win.Assignment != null && win.Assignment.Freight.FreightID == freightID && win.Station.Number == stationNumber) {
					        frm = win;
					        break;
				        }
			        }
                    if(frm == null) assignment = (StationFreightAssignment)lterminal.IndirectAssignments[stationNumber + freightID];
                }
                
                if(frm == null) {
                    //this.mMessageMgr.AddMessage("Loading station assignment for freight " + freightID + "...");
                	winStation win=null;
                    if(this.tabAssignments.SelectedTab == this.tabDirect) 
                        win = (assignment.Station.Description.ToLower() == "panda" ? (winStation)new winPanda(assignment) : (winStation)new winDirect(assignment));
                    else if(this.tabAssignments.SelectedTab == this.tabIndirect)
                        win = new winIndirect(assignment);
                    win.WindowState = ((this.MdiChildren.Length > 0 && this.ActiveMdiChild != null) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized);
                    win.WindowState = ((this.MdiChildren.Length > 0 && this.ActiveMdiChild != null) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized);
                    win.MdiParent = this;
                    win.Activated += new EventHandler(OnStationActivated);
                    win.Deactivate += new EventHandler(OnStationDeactivated);
                    win.Closing += new CancelEventHandler(OnStationClosing);
                    win.Closed += new EventHandler(OnStationClosed);
                    win.StatusMessage += new StatusEventHandler(OnStatusMessage);
                    win.ErrorMessage += new ErrorEventHandler(OnErrorMessage);
                    win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                    win.Show();
                }
			    else
				    frm.Activate();
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
        #endregion
		#region Historical: OnStationsChanged(), OnStationSelected()
		private void OnStationsChanged(object sender, EventArgs e) {
			//View active station assignments for the selected terminal
			this.mMessageMgr.AddMessage("Loading station for " + this.cboTerminal.Text + "...");
			this.lsvStations.Items.Clear();
            EnterpriseTerminal terminal = (EnterpriseTerminal)this.cboTerminal.SelectedItem;
			Hashtable stations = EnterpriseFactory.SortStations;
			IDictionaryEnumerator oEnum = stations.GetEnumerator();
			while(oEnum.MoveNext()) {
				//Filter by terminal
                DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
				SortStation station = (SortStation)entry.Value;
                if(station.TerminalID == terminal.TerminalID) {
				    ListViewItem item = this.lsvStations.Items.Add(entry.Key.ToString());
				    item.SubItems.Add(station.Name);
                }
			}
			this.lsvStations.Sorting = SortOrder.Ascending;
			this.lsvStations.Sort();
			this.lsvStations.Refresh();
		}
        private void OnStationSelected(object sender, System.EventArgs e) {
			//Event handler for historical sort station selection
			this.Cursor = Cursors.WaitCursor;
			try {
                //Check if a historical window is open for this station (assignment=null); otherwise create
				string stationNumber = this.lsvStations.SelectedItems[0].Text;
				Form frm=null;
                for(int i=0; i< this.MdiChildren.Length; i++) {
                    winStation win = (winStation)this.MdiChildren[i];
				    if(win.Assignment == null && win.Station.Number == stationNumber) {
						frm = win;
						break;
					}
				}
				if(frm == null) {
					this.mMessageMgr.AddMessage("Loading sort station #" + stationNumber + "...");
					SortStation station = EnterpriseFactory.GetStation(stationNumber);
					winStation win=null;
                    if(this.tabAssignments.SelectedTab == this.tabDirect) 
                        win = (station.Description.ToLower() == "panda" ? (winStation)new winPanda(station) : (winStation)new winDirect(station));
			        else if(this.tabAssignments.SelectedTab == this.tabIndirect) 
                        win = new winIndirect(station);
                    win.WindowState = ((this.MdiChildren.Length > 0 && this.ActiveMdiChild != null) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized);
                    win.MdiParent = this;
                    win.Activated += new EventHandler(OnStationActivated);
                    win.Deactivate += new EventHandler(OnStationDeactivated);
                    win.Closing += new CancelEventHandler(OnStationClosing);
                    win.Closed += new EventHandler(OnStationClosed);
                    win.StatusMessage += new StatusEventHandler(OnStatusMessage);
                    win.ErrorMessage += new ErrorEventHandler(OnErrorMessage);
                    win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                    win.Show();
				}
				else
					frm.Activate();
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Search Services: OnSearchTabResized(), OnScanChanged(), OnSearchChanged(), OnFindScan(), OnFindEvent(), appendEvent(), clearEvents()
		private void OnSearchTabResized(object sender, System.EventArgs e) { UserSettings.Write("SearchHeight", this.tabOther.Height); }
		private void OnScanChanged(object sender, System.EventArgs e) {
			//Event handler for change in scan
			this.cmdFindScan.Enabled = false;
			switch(this.cboSearchIn.Text) {
                case App.SEARCH_SORTEDITEMS:        this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 23); break;
                case App.SEARCH_SORTEDITEMSARCHIVE: this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 23); break;
			    case App.SEARCH_BEARWARESCANS:	    this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 23); break;
			    case App.SEARCH_ARGIXLOG:		    this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 24); break;
		    }
		}
		private void OnSearchChanged(object sender, System.EventArgs e) {
			//Event handler for change in search in  selection
			this.cmdFindScan.Enabled = false;
			switch(this.cboSearchIn.Text) {
                case App.SEARCH_SORTEDITEMS:        this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 23); break;
                case App.SEARCH_SORTEDITEMSARCHIVE: this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 23); break;
                case App.SEARCH_BEARWARESCANS:	    this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 23); break;
				case App.SEARCH_ARGIXLOG:		    this.cmdFindScan.Enabled = (this.txtScan.Text.Length == 24); break;
			}
		}
		private void OnFindScan(object sender, System.EventArgs e) {
			//Event handler to search for a carton scan
			this.Cursor = Cursors.WaitCursor;
			try {
				string find = ""; //FreightFactory.FindScan(this.txtScan.Text, this.cboSearchIn.Text);
				appendEvent(find + "\n");
				this.txtScan.Clear();
			}
			catch(Exception ex)  { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFindEvent(object sender, System.EventArgs e) {
			//Event handler for search for a log event
			this.Cursor = Cursors.WaitCursor;
			try {
				string find = ""; //SortStation.FindLogEvents(this.cboLogEvent.Text, this.dtpLogStartDate.Value);
				appendEvent(find + "\n");
			}
			catch(Exception ex)  { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void appendEvent(string text) { this.txtSearch.AppendText(text); this.txtSearch.Focus(); this.txtSearch.ScrollToCaret(); }
		private void clearEvents() { this.txtSearch.Clear(); }
		#endregion
		#region User Services: OnMenuClick(), OnToolbarButtonClick(), OnIconDoubleClick()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu itemclicked-apply selected service
			this.Cursor = Cursors.WaitCursor;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
                    case MNU_FILE_NEW:              break;
					case MNU_FILE_OPEN:				
						if(this.tabFlyout.SelectedTab == this.tabRealTime) 
							OnAssignmentSelected(null, EventArgs.Empty);
						else if(this.tabFlyout.SelectedTab == this.tabHistorical) 
							OnStationSelected(null, EventArgs.Empty);
						break;
					case MNU_FILE_PRINT_SETTINGS:   this.mPrinter.PageSetup(); break;
					case MNU_FILE_PRINT:            this.mPrinter.Print(App.Product,new string[] { "" },new string[] { this.txtSearch.Text },true); break;
					case MNU_FILE_EXIT:				this.Close(); break;
					case MNU_VIEW_REFRESH:
                        if(this.lsvDirectAssignments.Focused || this.lsvIndirectAssignments.Focused || this.lsvStations.Focused) 
                            OnTerminalChanged(this.cboTerminal,EventArgs.Empty); 
                        else if(this.mActiveStation != null) 
                            this.mActiveStation.Refresh();
                        break;
					case MNU_VIEW_TOOLBAR:			this.tlbMain.Visible = (this.mnuViewToolbar.Checked = !this.mnuViewToolbar.Checked); break;
					case MNU_VIEW_STATUSBAR:		this.stbMain.Visible = (this.mnuViewStatusBar.Checked = !this.mnuViewStatusBar.Checked); break;
					case MNU_TOOLS_FIND:			break;
					case MNU_WIN_CASCADE:			this.LayoutMdi(MdiLayout.Cascade); break;
					case MNU_WIN_TILEHORIZ:			this.LayoutMdi(MdiLayout.TileHorizontal); break;
					case MNU_WIN_TILEVERT:			this.LayoutMdi(MdiLayout.TileVertical); break;
					case MNU_HELP_CONTENTS:			Help.ShowHelp(this, this.mHelpContents); break;
					case MNU_HELP_ABOUT:            new dlgAbout(App.Product + " Application", App.Version, App.Copyright).ShowDialog(this); break;
					case MNU_LOG_CLEAR:				clearEvents(); break;
					case MNU_LOG_COPY:				Clipboard.SetDataObject(this.txtSearch.SelectedText); break;
					case MNU_ICON_HIDEWHENMINIMIZED:
						this.mTrayIcon.MenuItems[0].Checked = !this.mTrayIcon.MenuItems[0].Checked;
						if(this.WindowState == FormWindowState.Minimized) this.Visible = !this.mTrayIcon.MenuItems[0].Checked;
						break;
					case MNU_ICON_SHOW:
						this.WindowState = FormWindowState.Maximized;
						this.Visible = true;
						this.Activate();
						break;
				}
			}
			catch(Exception ex)  { App.ReportError(ex); }
			finally  { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnToolbarButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e) {
			//Toolbar handler
			try {
				switch(tlbMain.Buttons.IndexOf(e.Button)) {
                    case TLB_NEW:       this.mnuFileNew.PerformClick(); break;
                    case TLB_OPEN:      this.mnuFileOpen.PerformClick(); break;
                    case TLB_PRINT:     break;
                    case TLB_FIND:      this.mnuToolsFind.PerformClick(); break;
                    case TLB_REFRESH:	this.mnuViewRefresh.PerformClick(); break;
					default:	break;
				}
			}
			catch(Exception ex)  { App.ReportError(ex); }
		}
		private void OnIconDoubleClick(object Sender, EventArgs e) {
			//Show the form when the user double clicks on the notify icon
			// Set the WindowState to normal if the form is minimized.
			this.WindowState = FormWindowState.Maximized;
			this.Visible = true;
			this.Activate();
		}
		#endregion
		#region Local Services: configApplication(), setUserServices(), OnDataStatusUpdate()
		private void configApplication() {
			try {
				//Query application configuration
				
				//Create business objects with configuration values
				EnterpriseFactory.TerminalsChanged += new EventHandler(this.OnTerminalsChanged);
                EnterpriseFactory.SortStationsChanged += new EventHandler(this.OnStationsChanged);
                EnterpriseFactory.DataConnectionDropped += new EventHandler(OnDataConnectionDropped);
                EnterpriseFactory.RefreshCache();
			}
			catch(Exception ex) { throw new ApplicationException("Configuration error.", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {
				//Set main menu and context menu states
                this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = ((this.lsvDirectAssignments.SelectedItems.Count > 0) || (this.lsvIndirectAssignments.SelectedItems.Count > 0) || (this.lsvStations.SelectedItems.Count > 0));
				this.mnuFilePageSettings.Enabled = true;
				this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.txtSearch.Focused && this.txtSearch.Text != "");
				this.mnuFileExit.Enabled = true;
				this.mnuViewRefresh.Enabled = this.ctxAssignmentsRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuToolsFind.Enabled = true;
				this.mnuWinCascade.Enabled = this.mnuWinTileHoriz.Enabled = this.mnuWinTileVert.Enabled = true;
				this.mnuHelpContents.Enabled = (this.mHelpContents != "");
				this.mnuHelpAbout.Enabled = true;
				
				this.ctxLogCopy.Enabled = (this.txtSearch.Text != "");
				this.ctxLogClear.Enabled = (this.txtSearch.Text != "");
			}
			catch(Exception ex)  { App.ReportError(ex); }
		}
        public void OnDataStatusUpdate(object sender,DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
        void OnDataConnectionDropped(object sender, EventArgs e) { OnDataStatusUpdate(sender, new DataStatusArgs(false, "")); }
		#endregion
        #region Station Window Mgt: OnStationActivated(), OnStationDeactivated(), OnStationClosing(), OnStationClosed()
        private void OnStationActivated(object sender,System.EventArgs e) {
            //Event handler for activaton of a viewer child window
            try {
                this.mActiveStation = null;
                if(sender != null) {
                    winStation frm = (winStation)sender;
                    this.mActiveStation = frm;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnStationDeactivated(object sender,System.EventArgs e) {
            //Event handler for deactivaton of a viewer child window
            this.mActiveStation = null;
            setUserServices();
        }
        private void OnStationClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                e.Cancel = false;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnStationClosed(object sender,System.EventArgs e) {
            //Event handler for closing of a viewer child window
            setUserServices();
        }
        private void OnServiceStatesChanged(object sender,System.EventArgs e) { setUserServices(); }
        private void OnStatusMessage(object sender,StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
        private void OnErrorMessage(object sender,ErrorEventArgs e) { App.ReportError(e.Exception,e.DisplayMessage,e.Level); }
        #endregion
		#region Toolbox Control: InitializeToolbox(), OnToolboxResize(), ...
		private void InitializeToolbox() {
			//Configure toolbox size, state, and event handlers
			try {
				//Set parent tab control, splitter, and pin button event handlers
				this.tabFlyout.Enter += new System.EventHandler(this.OnEnterToolbox);
				this.tabFlyout.Leave += new System.EventHandler(this.OnLeaveToolbox);
				this.tabFlyout.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
				this.tabFlyout.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
				this.tabFlyout.SizeChanged += new System.EventHandler(this.OnToolboxResize);
				//this.splitterV.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
				//this.splitterV.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
			
				//Set event handlers for each control contained within the tab control
				foreach(Control ctl1 in this.tabFlyout.Controls) {
					if(ctl1.HasChildren) {
						foreach(Control ctl2 in ctl1.Controls) {
							if(ctl2.HasChildren) {
								foreach(Control ctl3 in ctl2.Controls) {
									ctl3.Enter += new System.EventHandler(this.OnEnterToolbox);
									ctl3.Leave += new System.EventHandler(this.OnLeaveToolbox);
									ctl3.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
									ctl3.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
								}
							}
							ctl2.Enter += new System.EventHandler(this.OnEnterToolbox);
							ctl2.Leave += new System.EventHandler(this.OnLeaveToolbox);
							ctl2.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
							ctl2.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
						}
					}
					ctl1.Enter += new System.EventHandler(this.OnEnterToolbox);
					ctl1.Leave += new System.EventHandler(this.OnLeaveToolbox);
					ctl1.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
					ctl1.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
				}
			
				//Configure auto-hide
				this.tabFlyout.Width = Convert.ToBoolean(UserSettings.Read("AutoHide", false)) ? 24 : (int)UserSettings.Read("ToolboxWidth", 192);
				this.btnPinRealTime.Text = Convert.ToBoolean(UserSettings.Read("AutoHide", false)) ? TABTLB_AUTOHIDE_ON : TABTLB_AUTOHIDE_OFF;
				this.btnPinHistorical.Text = Convert.ToBoolean(UserSettings.Read("AutoHide", false)) ? TABTLB_AUTOHIDE_ON : TABTLB_AUTOHIDE_OFF;
				this.btnPinSearch.Text = Convert.ToBoolean(UserSettings.Read("AutoHide", false)) ? TABTLB_AUTOHIDE_ON : TABTLB_AUTOHIDE_OFF;
				this.btnPinRealTime.Click += new System.EventHandler(this.OnToggleAutoHide);
				this.btnPinHistorical.Click += new System.EventHandler(this.OnToggleAutoHide);
				this.btnPinSearch.Click += new System.EventHandler(this.OnToggleAutoHide);
				this.tmrAutoHide.Interval = 1000;
				this.tmrAutoHide.Tick += new System.EventHandler(this.OnAutoHideToolbox);

				//Show toolbar as inactive
				this._lblRealTime.BackColor = this._lblHistorical.BackColor = this._lblSearch.BackColor = System.Drawing.SystemColors.Control;
				this._lblRealTime.ForeColor = this._lblHistorical.ForeColor = this._lblSearch.ForeColor = System.Drawing.SystemColors.ControlText;
				this.btnPinRealTime.BackColor = this.btnPinHistorical.BackColor = this.btnPinSearch.BackColor = System.Drawing.SystemColors.Control;
				this.btnPinRealTime.ForeColor = this.btnPinHistorical.ForeColor = this.btnPinSearch.ForeColor = System.Drawing.SystemColors.ControlText;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnToolboxResize(object sender, System.EventArgs e) {
			//Toolbox size changed event handler
			try {
				//Max at 360px
				if(this.tabFlyout.Width>=384)
					this.tabFlyout.Width = 384;
				if(this.btnPinRealTime.Text==TABTLB_AUTOHIDE_OFF) 
					UserSettings.Write("ToolboxWidth", this.tabFlyout.Width);
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnEnterToolbox(object sender, System.EventArgs e) {
			//Occurs when the control becomes the active control on the form
			try {
				//Disable auto-hide when active
				if(this.tmrAutoHide.Enabled) {
					this.tmrAutoHide.Stop();
					this.tmrAutoHide.Enabled = false;
				}
				this._lblRealTime.BackColor = this._lblHistorical.BackColor = this._lblSearch.BackColor = System.Drawing.SystemColors.Highlight;
				this._lblRealTime.ForeColor = this._lblHistorical.ForeColor = this._lblSearch.ForeColor = System.Drawing.SystemColors.HighlightText;
				this.btnPinRealTime.BackColor = this.btnPinHistorical.BackColor = this.btnPinSearch.BackColor = System.Drawing.SystemColors.Highlight;
				this.btnPinRealTime.ForeColor = this.btnPinHistorical.ForeColor = this.btnPinSearch.ForeColor = System.Drawing.SystemColors.HighlightText;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnLeaveToolbox(object sender, System.EventArgs e) {
			//Occurs when the control is no longer the active control on the form
			try {
				//Enable auto-hide when inactive and not pinned
				if(this.btnPinRealTime.Text==TABTLB_AUTOHIDE_ON) {
					this.tmrAutoHide.Enabled = true;
					this.tmrAutoHide.Start();
				}
				this._lblRealTime.BackColor = this._lblHistorical.BackColor = this._lblSearch.BackColor = System.Drawing.SystemColors.Control;
				this._lblRealTime.ForeColor = this._lblHistorical.ForeColor = this._lblSearch.ForeColor = System.Drawing.SystemColors.ControlText;
				this.btnPinRealTime.BackColor = this.btnPinHistorical.BackColor = this.btnPinSearch.BackColor = System.Drawing.SystemColors.Control;
				this.btnPinRealTime.ForeColor = this.btnPinHistorical.ForeColor = this.btnPinSearch.ForeColor = System.Drawing.SystemColors.ControlText;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnMouseEnterToolbox(object sender, System.EventArgs e) {
			//Occurs when the mouse enters the visible part of the control
			try {
				//Auto-open if not pinned and toolbar is closed; disable auto-hide if on
				if(this.btnPinRealTime.Text==TABTLB_AUTOHIDE_ON && this.tabFlyout.Width==24) 
					this.tabFlyout.Width = (int)UserSettings.Read("ToolboxWidth", 192);
				if(this.tmrAutoHide.Enabled) {
					this.tmrAutoHide.Stop();
					this.tmrAutoHide.Enabled = false;
				}
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnMouseLeaveToolbox(object sender, System.EventArgs e) {
			//Occurs when the mouse leaves the visible part of the control
			try {
				//Enable auto-hide when inactive and unpinned
				if(this._lblRealTime.BackColor==SystemColors.Control && this.btnPinRealTime.Text==TABTLB_AUTOHIDE_ON) {
					this.tmrAutoHide.Enabled = true;
					this.tmrAutoHide.Start();
				}
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnToggleAutoHide(object sender, System.EventArgs e) {
			//
			try {
				//Pin or unpin all pin buttons
				Button btn = (Button)sender;
				this.btnPinRealTime.Text = this.btnPinHistorical.Text = this.btnPinSearch.Text = (btn.Text==TABTLB_AUTOHIDE_OFF) ? TABTLB_AUTOHIDE_ON : TABTLB_AUTOHIDE_OFF;
				UserSettings.Write("AutoHide", (btn.Text==TABTLB_AUTOHIDE_ON));
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnAutoHideToolbox(object sender, System.EventArgs e) {
			//Toolbox timer event handler
			try {
				//Auto-close timer
				this.tmrAutoHide.Stop();
				this.tmrAutoHide.Enabled = false;
				this.tabFlyout.Width = 24;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
	}
}
