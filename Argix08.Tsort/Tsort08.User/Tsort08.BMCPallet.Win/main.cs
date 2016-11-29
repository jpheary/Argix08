//	File:	main.cs
//	Author:	J. Heary + MK
//	Date:	03/09
//	Desc:	
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
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Argix;
using Argix.Data;
using Argix.Windows;
using Tsort.Enterprise;
using Tsort.Sort;

namespace Tsort.Sort {
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private static bool ITEventOn=false;
        private EnterpriseTerminal mLocalTerminal = null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;

        //mk
        private Pallet mPallet = null;
        private string userProcess = "";
		
		#region Constants
		private const string MNU_FILE_NEW_PALLET = "&New Pallet";
		private const string MNU_FILE_REMOVECARTON = "&Remove Carton from pallet";
		private const string MNU_FILE_EXIT = "E&xit";
		private const string MNU_VIEW_TOOLBAR = "&Toolbar";
		private const string MNU_VIEW_STATUSBAR = "Status&Bar";
        private const string MNU_TOOLS_CONFIG = "&Configuration";
		private const string MNU_TOOLS_DIAGNOSTICS = "&Diagnostics";
		private const string MNU_TOOLS_TRACE = "&Trace";
        private const string MNU_TOOLS_USEWEBSVC = "Use &Web Services...";
        private const string MNU_HELP_ABOUT = "&About BMCPallet...";
				
		private const int KEYSTATE_SHIFT = 5;
		private const int KEYSTATE_CTL = 9;

        private const string USER_PROCESS_NEW_PALLET = "NewPallet";
        private const string USER_PROCESS_SCAN_CARTON = "ScanCarton";
        private const string USER_PROCESS_REMOVE_CARTON = "RemoveCarton";
        private const int ARGIX_BARCODE_LENGTH = 24;
        private const string CLIENT_NUMBER_VALIDATION = "101"; 

		#endregion
		#region Controls

        private System.Windows.Forms.ToolBarButton btnEdit;
        private Argix.Windows.ArgixStatusBar stbMain;
        private System.ComponentModel.IContainer components;
        private ToolStrip tlbMain;
        private ToolStripButton btnRemoveCarton;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuHelp;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuToolsDiagnostics;
        private ToolStripMenuItem mnuToolsTrace;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuNewPallet;
        private ToolStripMenuItem mnuRemoveCarton;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileExit;
        private ContextMenuStrip ctxMain;
        private ToolStripMenuItem ctxRefresh;
        private ToolStripSeparator ctxSep1;
        private ToolStripSeparator mnuToolsSep1;
        private ToolStripMenuItem mnuToolsUseWebSvc;
        private TextBox txbPallet;
        private Label lblStore;
        private Label label1;
        private GroupBox groupBoxPallet;
        private Label label2;
        private GroupBox groupBoxCarton;
        private TextBox txbCarton;
        private Label label4;
        private Label label3;
        private ListBox lsbCartons;
        private GroupBox groupBoxRemoveCarton;
        private Label label5;
        private TextBox txbDeleteCarton;
        private ToolStripButton btnNewPallet;
        private Button btnRemoveCaronCancel;
        private Label lblZone;
        private Label label6;
        private MenuStrip mnuMain;
		
		#endregion
		//Interface
		public frmMain() {
			try {
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
                #region Menu identities: maps menu item to OnMenuClick() handler
                this.mnuNewPallet.Text = MNU_FILE_NEW_PALLET;
				this.mnuRemoveCarton.Text = MNU_FILE_REMOVECARTON;
				this.mnuFileExit.Text = MNU_FILE_EXIT;
				this.mnuViewToolbar.Text = MNU_VIEW_TOOLBAR;
				this.mnuViewStatusBar.Text = MNU_VIEW_STATUSBAR;
                this.mnuToolsConfig.Text = MNU_TOOLS_CONFIG;
				this.mnuToolsDiagnostics.Text = MNU_TOOLS_DIAGNOSTICS;
				this.mnuToolsTrace.Text = MNU_TOOLS_TRACE;
				this.mnuToolsUseWebSvc.Text = MNU_TOOLS_USEWEBSVC;
				this.mnuHelpAbout.Text = MNU_HELP_ABOUT;
				buildHelpMenu();
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
				#region Window docking
                this.mnuMain.Dock = DockStyle.Top;
				this.tlbMain.Dock = DockStyle.Top;
                this.Controls.AddRange(new Control[] { this.tlbMain,this.stbMain,this.mnuMain });
				#endregion
				
				//Create data and UI services
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 3000);
				
				//Set application configuration
				configApplication();
			}
			catch(Exception ex) { Splash.Close(); if(!frmMain.ITEventOn) throw new ApplicationException("Startup Failure", ex); }
		}
		~frmMain() {
			//Destructor
			App.HideTrace();
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
            this.ctxMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.btnNewPallet = new System.Windows.Forms.ToolStripButton();
            this.btnRemoveCarton = new System.Windows.Forms.ToolStripButton();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNewPallet = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRemoveCarton = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsDiagnostics = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsTrace = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuToolsUseWebSvc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.txbPallet = new System.Windows.Forms.TextBox();
            this.lblStore = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxPallet = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxCarton = new System.Windows.Forms.GroupBox();
            this.lsbCartons = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txbCarton = new System.Windows.Forms.TextBox();
            this.groupBoxRemoveCarton = new System.Windows.Forms.GroupBox();
            this.btnRemoveCaronCancel = new System.Windows.Forms.Button();
            this.txbDeleteCarton = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblZone = new System.Windows.Forms.Label();
            this.ctxMain.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.groupBoxPallet.SuspendLayout();
            this.groupBoxCarton.SuspendLayout();
            this.groupBoxRemoveCarton.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Name = "btnEdit";
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 576);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(655, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 1;
            this.stbMain.TerminalText = "Terminal";
            // 
            // ctxMain
            // 
            this.ctxMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRefresh,
            this.ctxSep1});
            this.ctxMain.Name = "ctxMain";
            this.ctxMain.Size = new System.Drawing.Size(113, 32);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(112, 22);
            this.ctxRefresh.Text = "Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(109, 6);
            // 
            // tlbMain
            // 
            this.tlbMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewPallet,
            this.btnRemoveCarton});
            this.tlbMain.Location = new System.Drawing.Point(0, 24);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(655, 25);
            this.tlbMain.Stretch = true;
            this.tlbMain.TabIndex = 115;
            this.tlbMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnToolbarItemClicked);
            // 
            // btnNewPallet
            // 
            this.btnNewPallet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNewPallet.Image = global::Tsort.Properties.Resources.DocumentHS;
            this.btnNewPallet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewPallet.Name = "btnNewPallet";
            this.btnNewPallet.Size = new System.Drawing.Size(23, 22);
            this.btnNewPallet.ToolTipText = "New Pallet";
            // 
            // btnRemoveCarton
            // 
            this.btnRemoveCarton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRemoveCarton.Image = global::Tsort.Properties.Resources.Delete;
            this.btnRemoveCarton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRemoveCarton.Name = "btnRemoveCarton";
            this.btnRemoveCarton.Size = new System.Drawing.Size(23, 22);
            this.btnRemoveCarton.ToolTipText = "remove Carton";
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuTools,
            this.mnuView,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Size = new System.Drawing.Size(655, 24);
            this.mnuMain.TabIndex = 117;
            this.mnuMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuNewPallet,
            this.mnuFileSep1,
            this.mnuRemoveCarton,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(35, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuNewPallet
            // 
            this.mnuNewPallet.Image = global::Tsort.Properties.Resources.DocumentHS;
            this.mnuNewPallet.Name = "mnuNewPallet";
            this.mnuNewPallet.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuNewPallet.Size = new System.Drawing.Size(203, 22);
            this.mnuNewPallet.Text = "New Pallet";
            this.mnuNewPallet.ToolTipText = "New Pallet";
            this.mnuNewPallet.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(200, 6);
            // 
            // mnuRemoveCarton
            // 
            this.mnuRemoveCarton.Image = global::Tsort.Properties.Resources.Delete;
            this.mnuRemoveCarton.Name = "mnuRemoveCarton";
            this.mnuRemoveCarton.Size = new System.Drawing.Size(203, 22);
            this.mnuRemoveCarton.Text = "Remove Carton from Pallet";
            this.mnuRemoveCarton.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(200, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(203, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig,
            this.mnuToolsDiagnostics,
            this.mnuToolsTrace,
            this.mnuToolsSep1,
            this.mnuToolsUseWebSvc});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(44, 20);
            this.mnuTools.Text = "Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(155, 22);
            this.mnuToolsConfig.Text = "Configuration";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuToolsDiagnostics
            // 
            this.mnuToolsDiagnostics.Name = "mnuToolsDiagnostics";
            this.mnuToolsDiagnostics.Size = new System.Drawing.Size(155, 22);
            this.mnuToolsDiagnostics.Text = "Diagnostics";
            this.mnuToolsDiagnostics.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuToolsTrace
            // 
            this.mnuToolsTrace.Name = "mnuToolsTrace";
            this.mnuToolsTrace.Size = new System.Drawing.Size(155, 22);
            this.mnuToolsTrace.Text = "Trace";
            this.mnuToolsTrace.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuToolsSep1
            // 
            this.mnuToolsSep1.Name = "mnuToolsSep1";
            this.mnuToolsSep1.Size = new System.Drawing.Size(152, 6);
            // 
            // mnuToolsUseWebSvc
            // 
            this.mnuToolsUseWebSvc.Name = "mnuToolsUseWebSvc";
            this.mnuToolsUseWebSvc.Size = new System.Drawing.Size(155, 22);
            this.mnuToolsUseWebSvc.Text = "Use Web Service";
            this.mnuToolsUseWebSvc.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewSep1,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(41, 20);
            this.mnuView.Text = "View";
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(121, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(124, 22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(124, 22);
            this.mnuViewStatusBar.Text = "Status Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(40, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(103, 22);
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(100, 6);
            // 
            // txbPallet
            // 
            this.txbPallet.Location = new System.Drawing.Point(158, 19);
            this.txbPallet.Name = "txbPallet";
            this.txbPallet.Size = new System.Drawing.Size(289, 26);
            this.txbPallet.TabIndex = 118;
            this.txbPallet.TextChanged += new System.EventHandler(this.txbPallet_TextChanged);
            // 
            // lblStore
            // 
            this.lblStore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblStore.Location = new System.Drawing.Point(73, 58);
            this.lblStore.Name = "lblStore";
            this.lblStore.Size = new System.Drawing.Size(59, 22);
            this.lblStore.TabIndex = 119;
            this.lblStore.Text = "Store#";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 120;
            this.label1.Text = "Label Seq. # (24)";
            // 
            // groupBoxPallet
            // 
            this.groupBoxPallet.Controls.Add(this.lblZone);
            this.groupBoxPallet.Controls.Add(this.label6);
            this.groupBoxPallet.Controls.Add(this.label2);
            this.groupBoxPallet.Controls.Add(this.txbPallet);
            this.groupBoxPallet.Controls.Add(this.label1);
            this.groupBoxPallet.Controls.Add(this.lblStore);
            this.groupBoxPallet.Location = new System.Drawing.Point(22, 71);
            this.groupBoxPallet.Name = "groupBoxPallet";
            this.groupBoxPallet.Size = new System.Drawing.Size(601, 131);
            this.groupBoxPallet.TabIndex = 121;
            this.groupBoxPallet.TabStop = false;
            this.groupBoxPallet.Text = "Pallet";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 121;
            this.label2.Text = "Store";
            // 
            // groupBoxCarton
            // 
            this.groupBoxCarton.Controls.Add(this.lsbCartons);
            this.groupBoxCarton.Controls.Add(this.label4);
            this.groupBoxCarton.Controls.Add(this.label3);
            this.groupBoxCarton.Controls.Add(this.txbCarton);
            this.groupBoxCarton.Location = new System.Drawing.Point(22, 230);
            this.groupBoxCarton.Name = "groupBoxCarton";
            this.groupBoxCarton.Size = new System.Drawing.Size(601, 190);
            this.groupBoxCarton.TabIndex = 122;
            this.groupBoxCarton.TabStop = false;
            this.groupBoxCarton.Text = "Scan Carton into Pallet";
            // 
            // lsbCartons
            // 
            this.lsbCartons.BackColor = System.Drawing.SystemColors.Control;
            this.lsbCartons.FormattingEnabled = true;
            this.lsbCartons.ItemHeight = 20;
            this.lsbCartons.Location = new System.Drawing.Point(158, 25);
            this.lsbCartons.Name = "lsbCartons";
            this.lsbCartons.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lsbCartons.Size = new System.Drawing.Size(289, 104);
            this.lsbCartons.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Last 5 Scanned ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 1;
            this.label3.Text = "Label Seq. # (24)";
            // 
            // txbCarton
            // 
            this.txbCarton.Location = new System.Drawing.Point(156, 142);
            this.txbCarton.Name = "txbCarton";
            this.txbCarton.Size = new System.Drawing.Size(289, 26);
            this.txbCarton.TabIndex = 0;
            this.txbCarton.TextChanged += new System.EventHandler(this.txbCarton_TextChanged);
            // 
            // groupBoxRemoveCarton
            // 
            this.groupBoxRemoveCarton.Controls.Add(this.btnRemoveCaronCancel);
            this.groupBoxRemoveCarton.Controls.Add(this.txbDeleteCarton);
            this.groupBoxRemoveCarton.Controls.Add(this.label5);
            this.groupBoxRemoveCarton.Location = new System.Drawing.Point(22, 447);
            this.groupBoxRemoveCarton.Name = "groupBoxRemoveCarton";
            this.groupBoxRemoveCarton.Size = new System.Drawing.Size(601, 74);
            this.groupBoxRemoveCarton.TabIndex = 123;
            this.groupBoxRemoveCarton.TabStop = false;
            this.groupBoxRemoveCarton.Text = "Remove Carton from Pallet";
            // 
            // btnRemoveCaronCancel
            // 
            this.btnRemoveCaronCancel.Location = new System.Drawing.Point(490, 34);
            this.btnRemoveCaronCancel.Name = "btnRemoveCaronCancel";
            this.btnRemoveCaronCancel.Size = new System.Drawing.Size(83, 26);
            this.btnRemoveCaronCancel.TabIndex = 2;
            this.btnRemoveCaronCancel.Text = "Cancel";
            this.btnRemoveCaronCancel.UseVisualStyleBackColor = true;
            this.btnRemoveCaronCancel.Click += new System.EventHandler(this.btnRemoveCaronCancel_Click);
            // 
            // txbDeleteCarton
            // 
            this.txbDeleteCarton.Location = new System.Drawing.Point(158, 34);
            this.txbDeleteCarton.Name = "txbDeleteCarton";
            this.txbDeleteCarton.Size = new System.Drawing.Size(289, 26);
            this.txbDeleteCarton.TabIndex = 1;
            this.txbDeleteCarton.TextChanged += new System.EventHandler(this.txbDeleteCarton_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 40);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Label Seq. # (24)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(211, 58);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 20);
            this.label6.TabIndex = 122;
            this.label6.Text = "Zone";
            // 
            // lblZone
            // 
            this.lblZone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblZone.Location = new System.Drawing.Point(263, 58);
            this.lblZone.Name = "lblZone";
            this.lblZone.Size = new System.Drawing.Size(58, 22);
            this.lblZone.TabIndex = 123;
            this.lblZone.Text = "Zone#";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
            this.ClientSize = new System.Drawing.Size(655, 600);
            this.Controls.Add(this.groupBoxCarton);
            this.Controls.Add(this.groupBoxRemoveCarton);
            this.Controls.Add(this.groupBoxPallet);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.mnuMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(45, 45);
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.Text = "Argix Direct BMCPallet Application";
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ctxMain.ResumeLayout(false);
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.groupBoxPallet.ResumeLayout(false);
            this.groupBoxPallet.PerformLayout();
            this.groupBoxCarton.ResumeLayout(false);
            this.groupBoxCarton.PerformLayout();
            this.groupBoxRemoveCarton.ResumeLayout(false);
            this.groupBoxRemoveCarton.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
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
                try {
                    this.WindowState = global::Tsort.Properties.Settings.Default.WindowState;
                    switch(this.WindowState) {
                        case FormWindowState.Maximized: break;
                        case FormWindowState.Minimized: break;
                        case FormWindowState.Normal:
                            this.Location = global::Tsort.Properties.Settings.Default.Location;
                            this.Size = global::Tsort.Properties.Settings.Default.Size;
                            break;
                    }
                    this.mnuViewToolbar.Checked = this.tlbMain.Visible = Convert.ToBoolean(global::Tsort.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Tsort.Properties.Settings.Default.StatusBar);
                    //if(global::Tsort.Properties.Settings.Default.LastVersion != App.Version) {
                    //    //New release
                    //    App.ReportError(new ApplicationException("This is an updated version of BMCPallet. Please refer to Help\\Release Notes for release information."), true, LogLevel.None);
                    //}
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
                userProcess = USER_PROCESS_NEW_PALLET;
                this.setUserServices();
                
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
                #region Save user preferences
                global::Tsort.Properties.Settings.Default.WindowState = this.WindowState;
                global::Tsort.Properties.Settings.Default.Location = this.Location;
                global::Tsort.Properties.Settings.Default.Size = this.Size;
                global::Tsort.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Tsort.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Tsort.Properties.Settings.Default.LastVersion = App.Version;
                global::Tsort.Properties.Settings.Default.Save();
                #endregion
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.EventLogName,LogLevel.Information,"App Stopped"));
            }
		}
        private void OnFormResize(object sender,System.EventArgs e) { 
			//Event handler for form resized event
			if(this.WindowState == FormWindowState.Minimized) {
				App.HideTrace();
			}
        }
		#region User Services: OnMenuClick(), OnToolbarButtonClick()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                switch(menu.Text) {
                    case MNU_FILE_NEW_PALLET:       this.mPallet = null; this.userProcess = "NewPallet"; break;
                    case MNU_FILE_REMOVECARTON:     this.userProcess = "RemoveCarton";  break;
					case MNU_FILE_EXIT:				this.Close(); Application.Exit(); break;
					case MNU_VIEW_TOOLBAR:			this.tlbMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case MNU_VIEW_STATUSBAR:		this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case MNU_TOOLS_CONFIG:          App.ShowConfig(); break;
                    case MNU_TOOLS_DIAGNOSTICS:     App.ShowDiagnostics(); break;
                    case MNU_TOOLS_TRACE:           App.ShowTrace();  break;
					case MNU_TOOLS_USEWEBSVC:
                        dlgLogin login = new dlgLogin(App.Config.MISPassword);
                        login.ValidateEntry();
                        if(login.IsValid) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Resetting the application configuration...");
                            Application.DoEvents();
                            App.UseWebSvc = this.mnuToolsUseWebSvc.Checked = (!this.mnuToolsUseWebSvc.Checked);
                            configApplication();
                           // this.mnuViewRefresh.PerformClick();
                            this.setUserServices(); //MK not sure if this is needed, have one in finally 
                        } 
                        break;
					case MNU_HELP_ABOUT:			new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally {setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnToolbarItemClicked(object sender,ToolStripItemClickedEventArgs e) {
            //Toolbar handler - forward to main menu handler
            try {
                switch(e.ClickedItem.Name) {
                    case "btnNewPallet": this.mnuNewPallet.PerformClick(); break;
                    case "btnRemoveCarton": this.mnuRemoveCarton.PerformClick(); break;
                 }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
		#endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu(), OnHelpMenuClick(), OnDataStatusUpdate()
		private void configApplication() {
			try {
                //Create event log and database trace listeners, and log application as started
                ArgixTrace.ClearListeners();
                ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel, App.Mediator, App.USP_TRACE, App.EventLogName));

				ArgixTrace.WriteLine(new TraceMessage(App.Version, App.EventLogName, LogLevel.Information, "App Started"));
				
				//Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                this.mLocalTerminal = new EnterpriseTerminal();
                this.stbMain.SetTerminalPanel(this.mLocalTerminal.TerminalID.ToString(),this.mLocalTerminal.Description);
                SortBMCFactory.Mediator = App.Mediator;
            }
			catch(ApplicationException ex) { throw ex; } 
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {
                this.adjustUIProcessFields();
    			this.mnuFileExit.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
                this.mnuToolsDiagnostics.Enabled = frmMain.ITEventOn;
				this.mnuToolsTrace.Enabled = true;
                this.mnuToolsUseWebSvc.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError (ex, false, LogLevel.Error); }
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
        #region MK Program logic
        private void adjustUIProcessFields()
        {
            groupBoxCarton.Enabled = false;
            groupBoxPallet.Enabled = false;
            groupBoxRemoveCarton.Visible = false;
            btnNewPallet.Enabled = false;
            mnuNewPallet.Enabled = false;
            switch (this.userProcess)
            {
                case USER_PROCESS_NEW_PALLET :
                     groupBoxPallet.Enabled = true; txbPallet.Text = ""; txbPallet.Focus();
                     updatePalletFields(); 
                    break;
                case USER_PROCESS_SCAN_CARTON: groupBoxCarton.Enabled = btnNewPallet.Enabled = mnuNewPallet.Enabled = true; txbCarton.Text = ""; txbCarton.Focus(); break;
                case USER_PROCESS_REMOVE_CARTON: groupBoxRemoveCarton.Visible = true; txbDeleteCarton.Text = ""; txbDeleteCarton.Focus(); break;
            }
        }


        private void updatePalletFields()
        {
            lblStore.Text = mPallet == null ? "" : mPallet.Store.ToString();
            lblZone.Text = mPallet == null ? "" : mPallet.Zone;
        }

        private void txbPallet_TextChanged(object sender, EventArgs e)
        {
            if (txbPallet.Text.Length == ARGIX_BARCODE_LENGTH)
            {
                try
                {
                    mPallet = SortBMCFactory.GetPallet(txbPallet.Text.Substring(10,13));
                    if (mPallet == null)
                        throw new Exception("Pallet " + txbPallet.Text + " not found or not available for sort");
                    updatePalletFields();
                    //Pallet good - startscanning cartons
                    this.userProcess = USER_PROCESS_SCAN_CARTON;

                }  catch (Exception ex)
                {
                    App.ReportError(ex, true, LogLevel.Warning); 
                }
                this.adjustUIProcessFields();
            }
        }
       

        private void txbCarton_TextChanged(object sender, EventArgs e)
        {
            string labelBarcode = txbCarton.Text;
            try
            {
                if (labelBarcode.Length == ARGIX_BARCODE_LENGTH)
                {
                   //validate
                    if (labelBarcode.Substring(0,3) != CLIENT_NUMBER_VALIDATION) 
                        throw new Exception("Not valid client number");
                    if (Convert.ToInt32(labelBarcode.Substring(5,5)) != mPallet.Store)
                        throw new Exception("Not valid store number");
                    // sort
                       SortBMCFactory.PalletCartonNew(mPallet.LabelSequenceNumber, labelBarcode.Substring(10,13));
                    // update ui
                    if (lsbCartons.Items.Count == 5)
                        lsbCartons.Items.RemoveAt(0);
                    lsbCartons.Items.Add(txbCarton.Text);
                    txbCarton.Text = "";
                }
            }
            catch (Exception ex) { App.ReportError(ex, true, LogLevel.Warning); txbCarton.SelectAll(); }
        }

        private void txbDeleteCarton_TextChanged(object sender, EventArgs e)
        {
           try
           {
               if (txbDeleteCarton.Text.Length == ARGIX_BARCODE_LENGTH)
                {
                    SortBMCFactory.PalletCartonDelete(txbDeleteCarton.Text.Substring(10,13));
                    this.btnRemoveCaronCancel_Click(null, null);
                }
            }
            catch (Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
        }


        private void btnRemoveCaronCancel_Click(object sender, EventArgs e)
        {
            if (this.mPallet == null)
                this.userProcess = USER_PROCESS_NEW_PALLET;
            else
                this.userProcess = USER_PROCESS_SCAN_CARTON;
            adjustUIProcessFields();
        }
        
        #endregion


    }
}
