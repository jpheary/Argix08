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
using Argix.Windows;

namespace Argix.MIS {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private winScanner mActiveScanner=null;
		private ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		
		#region Components

        private Argix.Windows.ArgixStatusBar stbMain;
        private System.Windows.Forms.Panel pnlNav;
		private System.Windows.Forms.TreeView trvMain;
        private System.Windows.Forms.Splitter splitterV;
        private System.ComponentModel.IContainer components;
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
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewRefesh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuWindow;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuWindowCascade;
        private ToolStripMenuItem mnuWindowTileH;
        private ToolStripMenuItem mnuWindowTileV;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator toolStripMenuItem8;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem csOpen;
        private ToolStripSeparator csSep1;
        private ToolStripMenuItem csRefresh;
        private ToolStripSeparator csSep2;
        private ToolStripMenuItem csCut;
        private ToolStripMenuItem csCopy;
        private ToolStripMenuItem csPaste;
        private ToolStripSeparator csSep3;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnSave;
        private ToolStripButton btnPrint;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripSeparator btnSep3;
        private ImageList imgMain;
        #endregion

        //Interface
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.Text = "Argix Logistics " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
				this.splitterV.MinExtra = 0;
				this.splitterV.MinSize = 18;
				this.tsMain.Dock = DockStyle.Top;
				this.splitterV.Dock = DockStyle.Left;
				this.pnlNav.Dock = DockStyle.Left;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.splitterV,this.pnlNav,this.tsMain,this.msMain,this.stbMain });
				#endregion

				this.mToolTip = new ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
                configApplication();
			} 
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
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
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.splitterV = new System.Windows.Forms.Splitter();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.trvMain = new System.Windows.Forms.TreeView();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csCut = new System.Windows.Forms.ToolStripMenuItem();
            this.csCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.csPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
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
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefesh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowTileH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindowTileV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.pnlNav.SuspendLayout();
            this.csMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,347);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(610,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 100;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // splitterV
            // 
            this.splitterV.Location = new System.Drawing.Point(225,49);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(3,298);
            this.splitterV.TabIndex = 116;
            this.splitterV.TabStop = false;
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.trvMain);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0,49);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(225,298);
            this.pnlNav.TabIndex = 117;
            // 
            // trvMain
            // 
            this.trvMain.ContextMenuStrip = this.csMain;
            this.trvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvMain.ImageIndex = 0;
            this.trvMain.ImageList = this.imgMain;
            this.trvMain.Location = new System.Drawing.Point(0,0);
            this.trvMain.Name = "trvMain";
            this.trvMain.SelectedImageIndex = 0;
            this.trvMain.Size = new System.Drawing.Size(225,298);
            this.trvMain.TabIndex = 118;
            this.trvMain.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeCollapsed);
            this.trvMain.DoubleClick += new System.EventHandler(this.OnTreeNodeDoubleClicked);
            this.trvMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeSelected);
            this.trvMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTreeviewMouseDown);
            this.trvMain.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeExpanded);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csOpen,
            this.csSep1,
            this.csRefresh,
            this.csSep2,
            this.csCut,
            this.csCopy,
            this.csPaste,
            this.csSep3});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(153,154);
            // 
            // csOpen
            // 
            this.csOpen.Image = global::Argix.Properties.Resources.Open;
            this.csOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csOpen.Name = "csOpen";
            this.csOpen.Size = new System.Drawing.Size(152,22);
            this.csOpen.Text = "&Open";
            this.csOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(149,6);
            // 
            // csRefresh
            // 
            this.csRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(152,22);
            this.csRefresh.Text = "&Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csSep2
            // 
            this.csSep2.Name = "csSep2";
            this.csSep2.Size = new System.Drawing.Size(149,6);
            // 
            // csCut
            // 
            this.csCut.Image = global::Argix.Properties.Resources.Cut;
            this.csCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCut.Name = "csCut";
            this.csCut.Size = new System.Drawing.Size(152,22);
            this.csCut.Text = "Cu&t";
            this.csCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csCopy
            // 
            this.csCopy.Image = global::Argix.Properties.Resources.Copy;
            this.csCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCopy.Name = "csCopy";
            this.csCopy.Size = new System.Drawing.Size(152,22);
            this.csCopy.Text = "&Copy";
            this.csCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csPaste
            // 
            this.csPaste.Image = global::Argix.Properties.Resources.Paste;
            this.csPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csPaste.Name = "csPaste";
            this.csPaste.Size = new System.Drawing.Size(152,22);
            this.csPaste.Text = "&Paste";
            this.csPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csSep3
            // 
            this.csSep3.Name = "csSep3";
            this.csSep3.Size = new System.Drawing.Size(149,6);
            // 
            // imgMain
            // 
            this.imgMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgMain.ImageStream")));
            this.imgMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imgMain.Images.SetKeyName(0,"EARTH.ICO");
            this.imgMain.Images.SetKeyName(1,"FACTORY.ICO");
            this.imgMain.Images.SetKeyName(2,"SINEWAVE.ICO");
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuWindow,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0,0);
            this.msMain.MdiWindowListItem = this.mnuWindow;
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(610,24);
            this.msMain.TabIndex = 119;
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
            this.mnuFile.Size = new System.Drawing.Size(37,20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(152,22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152,22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(152,22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(152,22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = global::Argix.Properties.Resources.PageUp;
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(152,22);
            this.mnuFileSetup.Text = "Page Se&tup...";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(152,22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(152,22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(149,6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152,22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39,20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(152,22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(152,22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(152,22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefesh,
            this.mnuViewSep1,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44,20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefesh
            // 
            this.mnuViewRefesh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefesh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefesh.Name = "mnuViewRefesh";
            this.mnuViewRefesh.Size = new System.Drawing.Size(126,22);
            this.mnuViewRefesh.Text = "&Refresh";
            this.mnuViewRefesh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(123,6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(126,22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(126,22);
            this.mnuViewStatusBar.Text = "&Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuTools
            // 
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48,20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuWindow
            // 
            this.mnuWindow.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWindowCascade,
            this.mnuWindowTileH,
            this.mnuWindowTileV});
            this.mnuWindow.Name = "mnuWindow";
            this.mnuWindow.Size = new System.Drawing.Size(63,20);
            this.mnuWindow.Text = "&Window";
            // 
            // mnuWindowCascade
            // 
            this.mnuWindowCascade.Name = "mnuWindowCascade";
            this.mnuWindowCascade.Size = new System.Drawing.Size(160,22);
            this.mnuWindowCascade.Text = "&Cascade";
            this.mnuWindowCascade.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuWindowTileH
            // 
            this.mnuWindowTileH.Name = "mnuWindowTileH";
            this.mnuWindowTileH.Size = new System.Drawing.Size(160,22);
            this.mnuWindowTileH.Text = "Tile &Horizontally";
            this.mnuWindowTileH.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuWindowTileV
            // 
            this.mnuWindowTileV.Name = "mnuWindowTileV";
            this.mnuWindowTileV.Size = new System.Drawing.Size(160,22);
            this.mnuWindowTileV.Text = "Tile &Vertically";
            this.mnuWindowTileV.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.toolStripMenuItem8});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44,20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(107,22);
            this.mnuHelpAbout.Text = "&About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(104,6);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnSave,
            this.btnPrint,
            this.btnSep2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnSep3,
            this.btnRefresh});
            this.tsMain.Location = new System.Drawing.Point(0,24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(610,25);
            this.tsMain.TabIndex = 121;
            this.tsMain.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23,22);
            this.btnNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23,22);
            this.btnOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6,25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23,22);
            this.btnSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23,22);
            this.btnPrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6,25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = global::Argix.Properties.Resources.Cut;
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23,22);
            this.btnCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::Argix.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23,22);
            this.btnCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::Argix.Properties.Resources.Paste;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23,22);
            this.btnPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6,25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23,22);
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(610,371);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Cube Analyst";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closed += new System.EventHandler(this.OnFormClosed);
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.pnlNav.ResumeLayout(false);
            this.csMain.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				Splash.Close();
				this.Visible = true;
				Application.DoEvents();
                #region Set user preferences
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

                if(global::Argix.Properties.Settings.Default.LastVersion != App.Version) {
                    //New release
                    App.ReportError(new ApplicationException("This is a new release of " + App.Product + ". Please contact the IT department immediately if you have a problem."),true);
                }
                #endregion
                #region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
								
				//Initialize controls
				this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(false, ""));
				this.stbMain.SetTerminalPanel("", "");
				
				//Populate the treeview
				#region TreeView Initialization
				this.trvMain.FullRowSelect = true;
				this.trvMain.Indent = 18;
				this.trvMain.ItemHeight = 18;
				this.trvMain.HideSelection = false;
                //this.trvMain.ImageList = this.imgMain;
				this.trvMain.Scrollable = true;
				this.trvMain.Nodes.Clear();
				#endregion
				this.mMessageMgr.AddMessage("Loading Tsort cube scanners...");
				Enterprise node = new Enterprise("Argix Cube Scanners", App.ICON_ARGIX, App.ICON_ARGIX);
				this.trvMain.Nodes.Add(node);
				node.LoadChildNodes();
				this.trvMain.SelectedNode = this.trvMain.Nodes[0];
				this.trvMain.Nodes[0].Expand();
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormActivated(object sender, System.EventArgs e) {
			//Event handler for form activated event
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for change in form size
		}
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for after form is closed
			try {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
			}
			catch(Exception) { }
		}
		#region TreeNode Support: OnTreeNodeCollapsed(), OnTreeNodeExpanded(), OnTreeNodeSelected(), OnTreeNodeDoubleClicked()
		private void OnTreeviewMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for treeview mousedown event
			try {
				if(e.Button == MouseButtons.Right) {
					TreeNode node = this.trvMain.GetNodeAt(new Point(e.X, e.Y));
					this.trvMain.SelectedNode = node;
				}
			} 
			catch(Exception) { }
		}
		private void OnTreeNodeCollapsed(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node collapsed - child nodes need to unload [stale] data
			try {
				TsortNode node = (TsortNode)e.Node;
				node.CollapseNode();
			}
			catch(Exception ex) {  App.ReportError(ex); }
		}
		private void OnTreeNodeExpanded(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Node expanded - child nodes need to load [fresh] data
			try {
				TsortNode node = (TsortNode)e.Node;
				node.ExpandNode();
			}
			catch(Exception ex) {  App.ReportError(ex); }
		}
        private void OnBeforeSelect(object sender,TreeViewCancelEventArgs e) {
            //Event handler for 
            setUserServices();
        }
        private void OnTreeNodeSelected(object sender,System.Windows.Forms.TreeViewEventArgs e) {
			//Event handler for node selected
			setUserServices();
		}
		private void OnTreeNodeDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for tree node double-clicked
			if(this.mnuFileOpen.Enabled) this.mnuFileOpen.PerformClick();
		}
		#endregion		
        #region User Services: OnItemClicked(), OnHelpMenuClick(), OnSearchButtonKeyUp(), OnIconDoubleClick()
        private void OnItemClicked(object sender, System.EventArgs e) {
			//Menu itemclicked-apply selected service
			winScanner win=null;
			try  {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
					case "mnuFileNew": 
                    case "csNew": 
                    case "btnNew":
                        break;
					case "mnuFileOpen": 
                    case "csOpen": 
                    case "btnOpen":
                        if(this.trvMain.SelectedNode != null) {
						    this.Cursor = Cursors.WaitCursor;						
						    Form frm=null;
						    Scanner oScanner = (Scanner)this.trvMain.SelectedNode;
						    for(int i=0; i< this.MdiChildren.Length; i++) {
							    if(this.MdiChildren[i].Text == oScanner.SourceName) {
								    frm = this.MdiChildren[i];
								    break;
							    }
						    }
						    if(frm == null) {
							    this.mMessageMgr.AddMessage("Loading " + oScanner.SourceName + " scanner...");
							    win = new winScanner(oScanner);
							    if(this.MdiChildren.Length > 0) 
								    win.WindowState = this.ActiveMdiChild.WindowState;
							    else
								    win.WindowState = FormWindowState.Maximized;
							    win.MdiParent = this;
							    win.Activated += new EventHandler(OnScannerActivated);
							    win.Deactivate += new EventHandler(OnScannerDeactivated);
							    win.Closing += new CancelEventHandler(OnScannerClosing);
							    win.Disposed += new EventHandler(OnScannerClosed);
							    win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);					
							    win.Show();
						    }
						    else
							    frm.Activate();
						}
						break;
					case "mnuFileSave":	
                    case "btnSave":
                        break;
					case "mnuFileSaveAs":			
						//Save to user specified file
						win = (winScanner)this.ActiveMdiChild;
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.Filter = "Scanner File (*.xml) | *.xml";
						dlgSave.FilterIndex = 0;
						dlgSave.Title = "Save Scanner As...";
						dlgSave.FileName = "";
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            win.SaveAs(dlgSave.FileName);
						}
						break;
					case "mnuFileSetup":	    
                        UltraGridSvc.PageSettings(); 
                        break;
					case "mnuFilePrint": 
                    case "btnPrint":
                        this.Cursor = Cursors.WaitCursor;
                        win = (winScanner)this.ActiveMdiChild; win.Print(); 
                        break;
                    case "mnuFilePreview":
                        win = (winScanner)this.ActiveMdiChild; win.PrintPreview();
                        break;
					case "mnuFileExit":				
                        this.Close(); 
                        break;
					case "mnuEditCut": 
                    case "csCut":
                    case "btnCut":
                        this.Cursor = Cursors.WaitCursor;
                        win = (winScanner)this.ActiveMdiChild; win.Cut(); 
                        break;
					case "mnuEditCopy":	
                    case "csCopy":
                    case "btnCopy":
                        this.Cursor = Cursors.WaitCursor;
                        win = (winScanner)this.ActiveMdiChild; win.Copy(); 
                        break;
					case "mnuEditPaste": 
                    case "csPaste": 
                    case "btnPaste":
                        this.Cursor = Cursors.WaitCursor;
                        win = (winScanner)this.ActiveMdiChild; win.Paste(); 
                        break;
					case "mnuViewRefresh": 
                    case "csRefresh": 
                    case "btnRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        win = (winScanner)this.ActiveMdiChild; win.Refresh(); 
                        break;
					case "mnuViewToolbar":			this.tsMain.Visible = (this.mnuViewToolbar.Checked = !this.mnuViewToolbar.Checked); break;
					case "mnuViewStatusBar":		this.stbMain.Visible = (this.mnuViewStatusBar.Checked = !this.mnuViewStatusBar.Checked); break;
					case "mnuWinCascade":			this.LayoutMdi(MdiLayout.Cascade); break;
					case "mnuWinTileHoriz":			this.LayoutMdi(MdiLayout.TileHorizontal); break;
					case "mnuWinTileVert":			this.LayoutMdi(MdiLayout.TileVertical); break;
					case "mnuHelpAbout":            new dlgAbout(App.Product + " Tool", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false); }
        }
		#endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu()
		private void configApplication() {
			try {
			}
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {
                this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
                this.mnuFileOpen.Enabled = this.btnOpen.Enabled = this.csOpen.Enabled = (this.trvMain.Focused && this.trvMain.SelectedNode != null && this.trvMain.SelectedNode is Scanner);
				this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = (this.mActiveScanner != null && this.mActiveScanner.CanSaveAs);
				this.mnuFileSetup.Enabled = true;
                this.mnuFilePreview.Enabled = (this.mActiveScanner != null && this.mActiveScanner.CanPrint);
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.mActiveScanner != null && this.mActiveScanner.CanPrint);
				this.mnuFileExit.Enabled = true;
                this.mnuEditCut.Enabled = this.csCut.Enabled = this.btnCut.Enabled = (this.mActiveScanner != null && this.mActiveScanner.CanCut);
                this.mnuEditCopy.Enabled = this.csCopy.Enabled = this.btnCopy.Enabled = (this.mActiveScanner != null && this.mActiveScanner.CanCopy);
                this.mnuEditPaste.Enabled = this.csPaste.Enabled = this.btnPaste.Enabled = (this.mActiveScanner != null && this.mActiveScanner.CanPaste);
                this.mnuViewRefesh.Enabled = this.btnRefresh.Enabled = this.csRefresh.Enabled = (!this.trvMain.Focused && this.mActiveScanner != null);
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuWindowCascade.Enabled = this.mnuWindowTileH.Enabled = this.mnuWindowTileV.Enabled = true;
				this.mnuHelpAbout.Enabled = true;
            }
			catch(Exception ex) { App.ReportError(ex, false); }
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
            catch(Exception ex) { App.ReportError(ex,false); }
        }
		#endregion
		#region Scanner window: OnMdiChildActivate(), ...
		private void OnMdiChildActivate(object sender, EventArgs e) {
			//Event handler for change in mdi child collection
			setUserServices();
		}
		private void OnScannerActivated(object sender, System.EventArgs e) {
			//Event handler for activaton of a viewer child window
			winScanner win = (winScanner)sender;
			this.mActiveScanner = win;
            this.stbMain.SetTerminalPanel("",this.mActiveScanner.TerminalName);
            this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(true,""));
            setUserServices();
		}						
		private void OnScannerDeactivated(object sender, System.EventArgs e) {
			//Event handler for deactivaton of a viewer child window
			this.mActiveScanner = null;
			setUserServices();
		}
		private void OnScannerClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing via control box; e.Cancel=true keeps window open
			e.Cancel = false;
		}
		private void OnScannerClosed(object sender, System.EventArgs e) {
			//Event handler for closing of a viewer child window
			setUserServices();
		}
		private void OnServiceStatesChanged(object source, EventArgs e) {
			//Event handler for change in service states of active child window
			setUserServices();
		}
		#endregion
	}
}
