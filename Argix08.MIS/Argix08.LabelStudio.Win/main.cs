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
using Argix.Data;
using Argix.Windows;
using Tsort.Devices;
using Tsort.Devices.Printers;
using Tsort.Labels;

namespace Tsort.Tools {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members		
		private LabelMaker mLabelMaker=null;
		private ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
        
        private const string LOG_ID_PRINTER = "PRINTER";
        private const string LOG_ID_PORT = "PORT";
        private const string LOG_ID_RS232 = "RS232";

		#region Components

        private System.Windows.Forms.ImageList imgMain;
		private System.Windows.Forms.Timer tmrAutoHide;
        private System.Windows.Forms.ImageList imgRS232;
        private System.Windows.Forms.Splitter splitterH;
		private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.TreeView trvStores;
		private System.Windows.Forms.Splitter splitterVL;
        private System.Windows.Forms.Splitter splitterVR;
		private System.Windows.Forms.Panel pnlNav;
		private System.Windows.Forms.TabControl tabLabelMaker;
		private System.Windows.Forms.TabPage tabTokens;
		private System.Windows.Forms.TabPage tabValues;
		private System.Windows.Forms.Splitter splitterHH;
		private System.Windows.Forms.PropertyGrid grdTokens;
		private System.Windows.Forms.ComboBox cboLabelMakers;
		private System.Windows.Forms.PropertyGrid grdValues;
		private System.Windows.Forms.Panel pnlLabelMaker;
		private System.Windows.Forms.Label lblLabelMaker;
		private System.Windows.Forms.Panel pnlStatusLog;
		private System.Windows.Forms.Panel pnlLog;
		private System.Windows.Forms.Label lblLog;
		private System.Windows.Forms.TabControl tabLog;
		private System.Windows.Forms.TabPage tabStatusLog;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Label lblClose;
		private System.Windows.Forms.Label lblCloseLog;
		private System.Windows.Forms.Panel pnlToolbox;
		private System.Windows.Forms.Label lblToolbox;
		private System.Windows.Forms.Label lblPin;
		private System.Windows.Forms.Panel pnlToolboxTitlebar;
		private System.Windows.Forms.TabControl tabToolbox;
        private System.Windows.Forms.TabPage tabPort;
		private System.Windows.Forms.TabPage tabRS232;
		private System.Windows.Forms.ComboBox cboHandshaking;
		private System.Windows.Forms.Label _lblHandshaking;
		private System.Windows.Forms.GroupBox grpPrinter;
		private System.Windows.Forms.PictureBox picRI;
		private System.Windows.Forms.PictureBox picCD;
		private System.Windows.Forms.PictureBox picCTS;
		private System.Windows.Forms.PictureBox picDSR;
		private System.Windows.Forms.Label _lblCTS;
		private System.Windows.Forms.Label _lblCD;
		private System.Windows.Forms.Label _lblRI;
		private System.Windows.Forms.Label _lblDSR;
		private System.Windows.Forms.GroupBox grpHost;
		private System.Windows.Forms.CheckBox chkRTS;
		private System.Windows.Forms.CheckBox chkDTR;
		private System.Windows.Forms.TabPage tabPrinter;
		private System.Windows.Forms.GroupBox grpHostInfo;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Label lblResolution;
		private System.Windows.Forms.Label lblMemory;
		private System.Windows.Forms.GroupBox grpHostStatus;
		private System.Windows.Forms.Label lblBufferCount;
		private System.Windows.Forms.Label _lblBufferCount;
		private System.Windows.Forms.PictureBox picLabelWaiting;
		private System.Windows.Forms.PictureBox picRibbonOut;
		private System.Windows.Forms.PictureBox picHeadUp;
		private System.Windows.Forms.Label _lblRibbonOut;
		private System.Windows.Forms.Label _lblLabelWaiting;
		private System.Windows.Forms.Label _lblHeadUp;
		private System.Windows.Forms.PictureBox picOverTemp;
		private System.Windows.Forms.PictureBox picUnderTemp;
		private System.Windows.Forms.PictureBox picCorruptRAM;
		private System.Windows.Forms.PictureBox picPartialFormat;
		private System.Windows.Forms.Label _lblCorruptRAM;
		private System.Windows.Forms.Label _lblUnderTemp;
		private System.Windows.Forms.Label _lblOverTemp;
		private System.Windows.Forms.Label _lblPartialFormat;
		private System.Windows.Forms.PictureBox picBufferFull;
		private System.Windows.Forms.PictureBox picPauseOn;
		private System.Windows.Forms.PictureBox picPaperOut;
		private System.Windows.Forms.Label _lblPauseOn;
		private System.Windows.Forms.Label _lblBufferFull;
		private System.Windows.Forms.Label _lblPaperOut;
		private System.Windows.Forms.GroupBox grpHostMemoryStatus;
		private System.Windows.Forms.Label lblRAMMax;
		private System.Windows.Forms.Label lblRAMAvailable;
        private System.Windows.Forms.Label lblRAMTotal;
		private System.ComponentModel.IContainer components;
		
        private LabelPrinters ctlPrinters;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSave;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFileSettings;
        private ToolStripMenuItem msFilePrintLabel;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileExit;
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msEditUndo;
        private ToolStripMenuItem msEditRedo;
        private ToolStripSeparator msEditSep1;
        private ToolStripMenuItem msEditCut;
        private ToolStripMenuItem msEditCopy;
        private ToolStripMenuItem msEditPaste;
        private ToolStripSeparator msEditSep2;
        private ToolStripMenuItem msEditFind;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msViewLabelMaker;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msZebra;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msWindows;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ToolStripMenuItem msWindowsCascade;
        private ToolStripMenuItem msViewSerialPort;
        private ToolStripMenuItem msViewRS232;
        private ToolStripMenuItem msViewZebraPrinter;
        private ToolStripSeparator msViewSep2;
        private ToolStripMenuItem msViewStatusLog;
        private ToolStripSeparator msViewSep3;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripMenuItem msZebraHostStatus;
        private ToolStripMenuItem msWindowsTileH;
        private ToolStripMenuItem msWindowsTileV;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsSave;
        private ToolStripButton tsPrint;
        private ContextMenuStrip csLog;
        private ToolStripMenuItem csLogClear;
        private ToolStripMenuItem csNavOpen;
        private ToolStripSeparator csNavSep1;
        private ToolStripMenuItem csNavProperties;
        private ToolStripMenuItem msFileOpenPort;
        private ToolStripMenuItem msFileClosePort;
        private ToolStripSeparator msFileSep4;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsCut;
        private ToolStripButton tsCopy;
        private ToolStripButton tsPaste;
        private ToolStripButton tsFind;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripButton tsRefresh;
        private ToolStripMenuItem csNavRefresh;
        private ToolStripMenuItem msFilePrint;
        private ToolStripButton tsPrintLabel;
        private ToolStripButton tsUndo;
        private ToolStripButton tsRedo;
        private ToolStripSeparator tsSep3;
        private ToolStripSeparator tsSep4;
        private ContextMenuStrip csNav;
		#endregion
		
        //Interface
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
				this.splitterVL.MinExtra = 96;
				this.splitterVL.MinSize = 96;
				this.splitterVR.MinExtra = 96;
				this.splitterVR.MinSize = 18;
				this.splitterH.MinExtra = 96;
				this.splitterH.MinSize = 96;
				this.splitterHH.MinExtra = 96;
				this.splitterHH.MinSize = 96;
				this.tsMain.Dock = DockStyle.Top;
				this.splitterVL.Dock = DockStyle.Left;
				this.pnlNav.Dock = DockStyle.Left;
					this.trvStores.Dock = DockStyle.Fill;
					this.splitterHH.Dock = DockStyle.Bottom;
					this.pnlLabelMaker.Dock = DockStyle.Bottom;
						this.lblLabelMaker.Dock = DockStyle.Top;
						this.tabLabelMaker.Dock = DockStyle.Fill;
						this.pnlLabelMaker.Controls.AddRange(new Control[]{this.tabLabelMaker, this.lblLabelMaker});
						this.pnlLabelMaker.Height = 288;
					this.pnlNav.Controls.AddRange(new Control[]{this.trvStores, this.splitterHH, this.pnlLabelMaker});
				this.splitterVR.Dock = DockStyle.Right;
				this.pnlToolbox.Dock = DockStyle.Right;
				this.splitterH.Dock = DockStyle.Bottom;
				this.pnlStatusLog.Dock = DockStyle.Bottom;
					this.pnlLog.Dock = DockStyle.Top;
						this.lblLog.Dock = DockStyle.Fill;
						this.pnlLog.Controls.AddRange(new Control[]{this.lblLog});
					this.tabLog.Dock = DockStyle.Fill;
					this.pnlStatusLog.Controls.AddRange(new Control[]{this.tabLog, this.pnlLog});
					this.pnlStatusLog.Height = 144;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.splitterH,this.pnlStatusLog,this.splitterVL,this.pnlNav,this.splitterVR,this.pnlToolbox,this.tsMain,this.msMain,this.stbMain });
				#endregion
				InitializeToolbox();
				this.mToolTip = new ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
            } 
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
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
            this.splitterVL = new System.Windows.Forms.Splitter();
            this.imgMain = new System.Windows.Forms.ImageList(this.components);
            this.tmrAutoHide = new System.Windows.Forms.Timer(this.components);
            this.imgRS232 = new System.Windows.Forms.ImageList(this.components);
            this.splitterH = new System.Windows.Forms.Splitter();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.trvStores = new System.Windows.Forms.TreeView();
            this.csNav = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csNavOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.csNavRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csNavSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csNavProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.splitterVR = new System.Windows.Forms.Splitter();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.splitterHH = new System.Windows.Forms.Splitter();
            this.pnlLabelMaker = new System.Windows.Forms.Panel();
            this.lblClose = new System.Windows.Forms.Label();
            this.lblLabelMaker = new System.Windows.Forms.Label();
            this.tabLabelMaker = new System.Windows.Forms.TabControl();
            this.tabTokens = new System.Windows.Forms.TabPage();
            this.grdTokens = new System.Windows.Forms.PropertyGrid();
            this.tabValues = new System.Windows.Forms.TabPage();
            this.grdValues = new System.Windows.Forms.PropertyGrid();
            this.cboLabelMakers = new System.Windows.Forms.ComboBox();
            this.pnlStatusLog = new System.Windows.Forms.Panel();
            this.tabLog = new System.Windows.Forms.TabControl();
            this.tabStatusLog = new System.Windows.Forms.TabPage();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.csLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csLogClear = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.lblCloseLog = new System.Windows.Forms.Label();
            this.lblLog = new System.Windows.Forms.Label();
            this.pnlToolbox = new System.Windows.Forms.Panel();
            this.tabToolbox = new System.Windows.Forms.TabControl();
            this.tabPort = new System.Windows.Forms.TabPage();
            this.ctlPrinters = new Tsort.Devices.Printers.LabelPrinters();
            this.tabRS232 = new System.Windows.Forms.TabPage();
            this.cboHandshaking = new System.Windows.Forms.ComboBox();
            this._lblHandshaking = new System.Windows.Forms.Label();
            this.grpPrinter = new System.Windows.Forms.GroupBox();
            this.picRI = new System.Windows.Forms.PictureBox();
            this.picCD = new System.Windows.Forms.PictureBox();
            this.picCTS = new System.Windows.Forms.PictureBox();
            this.picDSR = new System.Windows.Forms.PictureBox();
            this._lblCTS = new System.Windows.Forms.Label();
            this._lblCD = new System.Windows.Forms.Label();
            this._lblRI = new System.Windows.Forms.Label();
            this._lblDSR = new System.Windows.Forms.Label();
            this.grpHost = new System.Windows.Forms.GroupBox();
            this.chkRTS = new System.Windows.Forms.CheckBox();
            this.chkDTR = new System.Windows.Forms.CheckBox();
            this.tabPrinter = new System.Windows.Forms.TabPage();
            this.grpHostInfo = new System.Windows.Forms.GroupBox();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblResolution = new System.Windows.Forms.Label();
            this.lblMemory = new System.Windows.Forms.Label();
            this.grpHostStatus = new System.Windows.Forms.GroupBox();
            this.lblBufferCount = new System.Windows.Forms.Label();
            this._lblBufferCount = new System.Windows.Forms.Label();
            this.picLabelWaiting = new System.Windows.Forms.PictureBox();
            this.picRibbonOut = new System.Windows.Forms.PictureBox();
            this.picHeadUp = new System.Windows.Forms.PictureBox();
            this._lblRibbonOut = new System.Windows.Forms.Label();
            this._lblLabelWaiting = new System.Windows.Forms.Label();
            this._lblHeadUp = new System.Windows.Forms.Label();
            this.picOverTemp = new System.Windows.Forms.PictureBox();
            this.picUnderTemp = new System.Windows.Forms.PictureBox();
            this.picCorruptRAM = new System.Windows.Forms.PictureBox();
            this.picPartialFormat = new System.Windows.Forms.PictureBox();
            this._lblCorruptRAM = new System.Windows.Forms.Label();
            this._lblUnderTemp = new System.Windows.Forms.Label();
            this._lblOverTemp = new System.Windows.Forms.Label();
            this._lblPartialFormat = new System.Windows.Forms.Label();
            this.picBufferFull = new System.Windows.Forms.PictureBox();
            this.picPauseOn = new System.Windows.Forms.PictureBox();
            this.picPaperOut = new System.Windows.Forms.PictureBox();
            this._lblPauseOn = new System.Windows.Forms.Label();
            this._lblBufferFull = new System.Windows.Forms.Label();
            this._lblPaperOut = new System.Windows.Forms.Label();
            this.grpHostMemoryStatus = new System.Windows.Forms.GroupBox();
            this.lblRAMMax = new System.Windows.Forms.Label();
            this.lblRAMAvailable = new System.Windows.Forms.Label();
            this.lblRAMTotal = new System.Windows.Forms.Label();
            this.pnlToolboxTitlebar = new System.Windows.Forms.Panel();
            this.lblPin = new System.Windows.Forms.Label();
            this.lblToolbox = new System.Windows.Forms.Label();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileOpenPort = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileClosePort = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrintLabel = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewLabelMaker = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSerialPort = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRS232 = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewZebraPrinter = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewStatusLog = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msZebra = new System.Windows.Forms.ToolStripMenuItem();
            this.msZebraHostStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.msWindowsCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.msWindowsTileH = new System.Windows.Forms.ToolStripMenuItem();
            this.msWindowsTileV = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsPrintLabel = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsUndo = new System.Windows.Forms.ToolStripButton();
            this.tsRedo = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCut = new System.Windows.Forms.ToolStripButton();
            this.tsCopy = new System.Windows.Forms.ToolStripButton();
            this.tsPaste = new System.Windows.Forms.ToolStripButton();
            this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsFind = new System.Windows.Forms.ToolStripButton();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.csNav.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.pnlLabelMaker.SuspendLayout();
            this.tabLabelMaker.SuspendLayout();
            this.tabTokens.SuspendLayout();
            this.tabValues.SuspendLayout();
            this.pnlStatusLog.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabStatusLog.SuspendLayout();
            this.csLog.SuspendLayout();
            this.pnlLog.SuspendLayout();
            this.pnlToolbox.SuspendLayout();
            this.tabToolbox.SuspendLayout();
            this.tabPort.SuspendLayout();
            this.tabRS232.SuspendLayout();
            this.grpPrinter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCTS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDSR)).BeginInit();
            this.grpHost.SuspendLayout();
            this.tabPrinter.SuspendLayout();
            this.grpHostInfo.SuspendLayout();
            this.grpHostStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLabelWaiting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRibbonOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeadUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOverTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUnderTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCorruptRAM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPartialFormat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBufferFull)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPauseOn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPaperOut)).BeginInit();
            this.grpHostMemoryStatus.SuspendLayout();
            this.pnlToolboxTitlebar.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitterVL
            // 
            this.splitterVL.Location = new System.Drawing.Point(225,49);
            this.splitterVL.Name = "splitterVL";
            this.splitterVL.Size = new System.Drawing.Size(3,481);
            this.splitterVL.TabIndex = 3;
            this.splitterVL.TabStop = false;
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
            // 
            // imgRS232
            // 
            this.imgRS232.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgRS232.ImageStream")));
            this.imgRS232.TransparentColor = System.Drawing.Color.Transparent;
            this.imgRS232.Images.SetKeyName(0,"");
            this.imgRS232.Images.SetKeyName(1,"");
            this.imgRS232.Images.SetKeyName(2,"");
            this.imgRS232.Images.SetKeyName(3,"");
            this.imgRS232.Images.SetKeyName(4,"");
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterH.Location = new System.Drawing.Point(754,49);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(3,481);
            this.splitterH.TabIndex = 96;
            this.splitterH.TabStop = false;
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,530);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(760,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 100;
            this.stbMain.TerminalText = "Terminal";
            // 
            // trvStores
            // 
            this.trvStores.ContextMenuStrip = this.csNav;
            this.trvStores.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trvStores.FullRowSelect = true;
            this.trvStores.HideSelection = false;
            this.trvStores.ImageIndex = 0;
            this.trvStores.ImageList = this.imgMain;
            this.trvStores.Indent = 18;
            this.trvStores.ItemHeight = 18;
            this.trvStores.Location = new System.Drawing.Point(0,0);
            this.trvStores.Name = "trvStores";
            this.trvStores.SelectedImageIndex = 0;
            this.trvStores.Size = new System.Drawing.Size(225,166);
            this.trvStores.Sorted = true;
            this.trvStores.TabIndex = 102;
            this.trvStores.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeCollapsed);
            this.trvStores.DoubleClick += new System.EventHandler(this.OnTreeNodeDoubleClicked);
            this.trvStores.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeSelected);
            this.trvStores.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnTreeNodeMouseDown);
            this.trvStores.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.OnTreeNodeExpanded);
            // 
            // csNav
            // 
            this.csNav.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csNavOpen,
            this.csNavRefresh,
            this.csNavSep1,
            this.csNavProperties});
            this.csNav.Name = "csNav";
            this.csNav.Size = new System.Drawing.Size(128,76);
            // 
            // csNavOpen
            // 
            this.csNavOpen.Image = global::Tsort.Properties.Resources.Open;
            this.csNavOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csNavOpen.Name = "csNavOpen";
            this.csNavOpen.Size = new System.Drawing.Size(127,22);
            this.csNavOpen.Text = "&Open";
            this.csNavOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csNavRefresh
            // 
            this.csNavRefresh.Image = global::Tsort.Properties.Resources.Refresh;
            this.csNavRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csNavRefresh.Name = "csNavRefresh";
            this.csNavRefresh.Size = new System.Drawing.Size(127,22);
            this.csNavRefresh.Text = "Refresh";
            this.csNavRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csNavSep1
            // 
            this.csNavSep1.Name = "csNavSep1";
            this.csNavSep1.Size = new System.Drawing.Size(124,6);
            // 
            // csNavProperties
            // 
            this.csNavProperties.Image = global::Tsort.Properties.Resources.Properties;
            this.csNavProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csNavProperties.Name = "csNavProperties";
            this.csNavProperties.Size = new System.Drawing.Size(127,22);
            this.csNavProperties.Text = "&Properties";
            this.csNavProperties.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // splitterVR
            // 
            this.splitterVR.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitterVR.Location = new System.Drawing.Point(757,49);
            this.splitterVR.Name = "splitterVR";
            this.splitterVR.Size = new System.Drawing.Size(3,481);
            this.splitterVR.TabIndex = 103;
            this.splitterVR.TabStop = false;
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.trvStores);
            this.pnlNav.Controls.Add(this.splitterHH);
            this.pnlNav.Controls.Add(this.pnlLabelMaker);
            this.pnlNav.Controls.Add(this.tabLabelMaker);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0,49);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Size = new System.Drawing.Size(225,481);
            this.pnlNav.TabIndex = 105;
            // 
            // splitterHH
            // 
            this.splitterHH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterHH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterHH.Location = new System.Drawing.Point(0,166);
            this.splitterHH.Name = "splitterHH";
            this.splitterHH.Size = new System.Drawing.Size(225,3);
            this.splitterHH.TabIndex = 103;
            this.splitterHH.TabStop = false;
            // 
            // pnlLabelMaker
            // 
            this.pnlLabelMaker.Controls.Add(this.lblClose);
            this.pnlLabelMaker.Controls.Add(this.lblLabelMaker);
            this.pnlLabelMaker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLabelMaker.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.pnlLabelMaker.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlLabelMaker.Location = new System.Drawing.Point(0,169);
            this.pnlLabelMaker.Name = "pnlLabelMaker";
            this.pnlLabelMaker.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLabelMaker.Size = new System.Drawing.Size(225,24);
            this.pnlLabelMaker.TabIndex = 117;
            this.pnlLabelMaker.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.pnlLabelMaker.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // lblClose
            // 
            this.lblClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblClose.BackColor = System.Drawing.SystemColors.Control;
            this.lblClose.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblClose.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblClose.Location = new System.Drawing.Point(201,4);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(16,16);
            this.lblClose.TabIndex = 114;
            this.lblClose.Text = "X";
            this.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblClose.Click += new System.EventHandler(this.OnCloseLabelMaker);
            this.lblClose.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.lblClose.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // lblLabelMaker
            // 
            this.lblLabelMaker.BackColor = System.Drawing.SystemColors.Control;
            this.lblLabelMaker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLabelMaker.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLabelMaker.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblLabelMaker.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLabelMaker.Location = new System.Drawing.Point(3,3);
            this.lblLabelMaker.Name = "lblLabelMaker";
            this.lblLabelMaker.Size = new System.Drawing.Size(219,18);
            this.lblLabelMaker.TabIndex = 113;
            this.lblLabelMaker.Text = "Label Maker";
            this.lblLabelMaker.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLabelMaker.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.lblLabelMaker.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // tabLabelMaker
            // 
            this.tabLabelMaker.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabLabelMaker.Controls.Add(this.tabTokens);
            this.tabLabelMaker.Controls.Add(this.tabValues);
            this.tabLabelMaker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabLabelMaker.ItemSize = new System.Drawing.Size(53,24);
            this.tabLabelMaker.Location = new System.Drawing.Point(0,193);
            this.tabLabelMaker.Name = "tabLabelMaker";
            this.tabLabelMaker.SelectedIndex = 0;
            this.tabLabelMaker.ShowToolTips = true;
            this.tabLabelMaker.Size = new System.Drawing.Size(225,288);
            this.tabLabelMaker.TabIndex = 0;
            this.tabLabelMaker.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.tabLabelMaker.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // tabTokens
            // 
            this.tabTokens.Controls.Add(this.grdTokens);
            this.tabTokens.Location = new System.Drawing.Point(4,4);
            this.tabTokens.Name = "tabTokens";
            this.tabTokens.Size = new System.Drawing.Size(217,256);
            this.tabTokens.TabIndex = 0;
            this.tabTokens.Text = "Tokens";
            this.tabTokens.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.tabTokens.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // grdTokens
            // 
            this.grdTokens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTokens.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.grdTokens.Location = new System.Drawing.Point(0,0);
            this.grdTokens.Name = "grdTokens";
            this.grdTokens.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.grdTokens.Size = new System.Drawing.Size(217,256);
            this.grdTokens.TabIndex = 0;
            this.grdTokens.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.grdTokens.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // tabValues
            // 
            this.tabValues.Controls.Add(this.grdValues);
            this.tabValues.Controls.Add(this.cboLabelMakers);
            this.tabValues.Location = new System.Drawing.Point(4,4);
            this.tabValues.Name = "tabValues";
            this.tabValues.Size = new System.Drawing.Size(217,256);
            this.tabValues.TabIndex = 1;
            this.tabValues.Text = "Values";
            this.tabValues.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.tabValues.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // grdValues
            // 
            this.grdValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdValues.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.grdValues.Location = new System.Drawing.Point(0,22);
            this.grdValues.Name = "grdValues";
            this.grdValues.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
            this.grdValues.Size = new System.Drawing.Size(217,233);
            this.grdValues.TabIndex = 1;
            this.grdValues.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.grdValues.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // cboLabelMakers
            // 
            this.cboLabelMakers.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboLabelMakers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLabelMakers.Items.AddRange(new object[] {
            "User Specified"});
            this.cboLabelMakers.Location = new System.Drawing.Point(0,0);
            this.cboLabelMakers.Name = "cboLabelMakers";
            this.cboLabelMakers.Size = new System.Drawing.Size(217,21);
            this.cboLabelMakers.TabIndex = 0;
            this.cboLabelMakers.SelectionChangeCommitted += new System.EventHandler(this.OnLabelMakerSelected);
            this.cboLabelMakers.Leave += new System.EventHandler(this.OnLeaveLabelMaker);
            this.cboLabelMakers.Enter += new System.EventHandler(this.OnEnterLabelMaker);
            // 
            // pnlStatusLog
            // 
            this.pnlStatusLog.Controls.Add(this.tabLog);
            this.pnlStatusLog.Controls.Add(this.pnlLog);
            this.pnlStatusLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatusLog.Location = new System.Drawing.Point(228,410);
            this.pnlStatusLog.Name = "pnlStatusLog";
            this.pnlStatusLog.Size = new System.Drawing.Size(526,120);
            this.pnlStatusLog.TabIndex = 107;
            this.pnlStatusLog.Leave += new System.EventHandler(this.OnLeaveStatusLog);
            this.pnlStatusLog.Enter += new System.EventHandler(this.OnEnterStatusLog);
            // 
            // tabLog
            // 
            this.tabLog.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabLog.Controls.Add(this.tabStatusLog);
            this.tabLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabLog.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabLog.ItemSize = new System.Drawing.Size(42,24);
            this.tabLog.Location = new System.Drawing.Point(0,24);
            this.tabLog.Multiline = true;
            this.tabLog.Name = "tabLog";
            this.tabLog.SelectedIndex = 0;
            this.tabLog.ShowToolTips = true;
            this.tabLog.Size = new System.Drawing.Size(526,96);
            this.tabLog.TabIndex = 119;
            this.tabLog.Leave += new System.EventHandler(this.OnLeaveStatusLog);
            this.tabLog.Enter += new System.EventHandler(this.OnEnterStatusLog);
            // 
            // tabStatusLog
            // 
            this.tabStatusLog.BackColor = System.Drawing.SystemColors.Control;
            this.tabStatusLog.Controls.Add(this.txtLog);
            this.tabStatusLog.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabStatusLog.ForeColor = System.Drawing.Color.Navy;
            this.tabStatusLog.Location = new System.Drawing.Point(4,4);
            this.tabStatusLog.Name = "tabStatusLog";
            this.tabStatusLog.Size = new System.Drawing.Size(518,64);
            this.tabStatusLog.TabIndex = 0;
            this.tabStatusLog.Text = "Comm Log";
            this.tabStatusLog.Leave += new System.EventHandler(this.OnLeaveStatusLog);
            this.tabStatusLog.Enter += new System.EventHandler(this.OnEnterStatusLog);
            // 
            // txtLog
            // 
            this.txtLog.AutoWordSelection = true;
            this.txtLog.ContextMenuStrip = this.csLog;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.HideSelection = false;
            this.txtLog.Location = new System.Drawing.Point(0,0);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.txtLog.ShowSelectionMargin = true;
            this.txtLog.Size = new System.Drawing.Size(518,64);
            this.txtLog.TabIndex = 111;
            this.txtLog.Text = "";
            this.txtLog.Enter += new System.EventHandler(this.OnEnterStatusLog);
            this.txtLog.Leave += new System.EventHandler(this.OnLeaveStatusLog);
            // 
            // csLog
            // 
            this.csLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csLogClear});
            this.csLog.Name = "csLog";
            this.csLog.Size = new System.Drawing.Size(102,26);
            // 
            // csLogClear
            // 
            this.csLogClear.Name = "csLogClear";
            this.csLogClear.Size = new System.Drawing.Size(101,22);
            this.csLogClear.Text = "&Clear";
            this.csLogClear.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // pnlLog
            // 
            this.pnlLog.Controls.Add(this.lblCloseLog);
            this.pnlLog.Controls.Add(this.lblLog);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLog.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.pnlLog.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlLog.Location = new System.Drawing.Point(0,0);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Padding = new System.Windows.Forms.Padding(3);
            this.pnlLog.Size = new System.Drawing.Size(526,24);
            this.pnlLog.TabIndex = 118;
            this.pnlLog.Leave += new System.EventHandler(this.OnLeaveStatusLog);
            this.pnlLog.Enter += new System.EventHandler(this.OnEnterStatusLog);
            // 
            // lblCloseLog
            // 
            this.lblCloseLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseLog.BackColor = System.Drawing.SystemColors.Control;
            this.lblCloseLog.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblCloseLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCloseLog.Location = new System.Drawing.Point(501,4);
            this.lblCloseLog.Name = "lblCloseLog";
            this.lblCloseLog.Size = new System.Drawing.Size(16,16);
            this.lblCloseLog.TabIndex = 115;
            this.lblCloseLog.Text = "X";
            this.lblCloseLog.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseLog.Click += new System.EventHandler(this.OnCloseLog);
            this.lblCloseLog.Leave += new System.EventHandler(this.OnLeaveStatusLog);
            this.lblCloseLog.Enter += new System.EventHandler(this.OnEnterStatusLog);
            // 
            // lblLog
            // 
            this.lblLog.BackColor = System.Drawing.SystemColors.Control;
            this.lblLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLog.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblLog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLog.Location = new System.Drawing.Point(3,3);
            this.lblLog.Name = "lblLog";
            this.lblLog.Size = new System.Drawing.Size(520,18);
            this.lblLog.TabIndex = 113;
            this.lblLog.Text = "Log";
            this.lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblLog.Leave += new System.EventHandler(this.OnLeaveStatusLog);
            this.lblLog.Enter += new System.EventHandler(this.OnEnterStatusLog);
            // 
            // pnlToolbox
            // 
            this.pnlToolbox.Controls.Add(this.tabToolbox);
            this.pnlToolbox.Controls.Add(this.pnlToolboxTitlebar);
            this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlToolbox.Location = new System.Drawing.Point(554,49);
            this.pnlToolbox.Name = "pnlToolbox";
            this.pnlToolbox.Size = new System.Drawing.Size(200,361);
            this.pnlToolbox.TabIndex = 109;
            // 
            // tabToolbox
            // 
            this.tabToolbox.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabToolbox.Controls.Add(this.tabPort);
            this.tabToolbox.Controls.Add(this.tabRS232);
            this.tabToolbox.Controls.Add(this.tabPrinter);
            this.tabToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabToolbox.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabToolbox.ItemSize = new System.Drawing.Size(42,24);
            this.tabToolbox.Location = new System.Drawing.Point(0,24);
            this.tabToolbox.Multiline = true;
            this.tabToolbox.Name = "tabToolbox";
            this.tabToolbox.SelectedIndex = 0;
            this.tabToolbox.ShowToolTips = true;
            this.tabToolbox.Size = new System.Drawing.Size(200,337);
            this.tabToolbox.TabIndex = 119;
            // 
            // tabPort
            // 
            this.tabPort.BackColor = System.Drawing.SystemColors.Control;
            this.tabPort.Controls.Add(this.ctlPrinters);
            this.tabPort.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.tabPort.ForeColor = System.Drawing.Color.Navy;
            this.tabPort.Location = new System.Drawing.Point(4,4);
            this.tabPort.Name = "tabPort";
            this.tabPort.Size = new System.Drawing.Size(168,329);
            this.tabPort.TabIndex = 0;
            this.tabPort.Text = "Serial Port";
            // 
            // ctlPrinters
            // 
            this.ctlPrinters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctlPrinters.Location = new System.Drawing.Point(0,0);
            this.ctlPrinters.Margin = new System.Windows.Forms.Padding(0);
            this.ctlPrinters.Name = "ctlPrinters";
            this.ctlPrinters.Padding = new System.Windows.Forms.Padding(0,0,18,0);
            this.ctlPrinters.Size = new System.Drawing.Size(168,329);
            this.ctlPrinters.TabIndex = 0;
            this.ctlPrinters.PrinterChanged += new System.EventHandler(this.OnPrinterChanged);
            // 
            // tabRS232
            // 
            this.tabRS232.Controls.Add(this.cboHandshaking);
            this.tabRS232.Controls.Add(this._lblHandshaking);
            this.tabRS232.Controls.Add(this.grpPrinter);
            this.tabRS232.Controls.Add(this.grpHost);
            this.tabRS232.Location = new System.Drawing.Point(4,4);
            this.tabRS232.Name = "tabRS232";
            this.tabRS232.Size = new System.Drawing.Size(168,329);
            this.tabRS232.TabIndex = 1;
            this.tabRS232.Text = "RS-232";
            this.tabRS232.Visible = false;
            // 
            // cboHandshaking
            // 
            this.cboHandshaking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboHandshaking.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHandshaking.Items.AddRange(new object[] {
            "None",
            "RTS/CTS",
            "XON/XOFF"});
            this.cboHandshaking.Location = new System.Drawing.Point(6,252);
            this.cboHandshaking.MaxDropDownItems = 4;
            this.cboHandshaking.Name = "cboHandshaking";
            this.cboHandshaking.Size = new System.Drawing.Size(152,21);
            this.cboHandshaking.TabIndex = 118;
            this.cboHandshaking.SelectionChangeCommitted += new System.EventHandler(this.OnRS232HandshakeChanged);
            // 
            // _lblHandshaking
            // 
            this._lblHandshaking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblHandshaking.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblHandshaking.Location = new System.Drawing.Point(6,234);
            this._lblHandshaking.Name = "_lblHandshaking";
            this._lblHandshaking.Size = new System.Drawing.Size(152,18);
            this._lblHandshaking.TabIndex = 117;
            this._lblHandshaking.Text = "Handshaking";
            // 
            // grpPrinter
            // 
            this.grpPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpPrinter.Controls.Add(this.picRI);
            this.grpPrinter.Controls.Add(this.picCD);
            this.grpPrinter.Controls.Add(this.picCTS);
            this.grpPrinter.Controls.Add(this.picDSR);
            this.grpPrinter.Controls.Add(this._lblCTS);
            this.grpPrinter.Controls.Add(this._lblCD);
            this.grpPrinter.Controls.Add(this._lblRI);
            this.grpPrinter.Controls.Add(this._lblDSR);
            this.grpPrinter.Location = new System.Drawing.Point(6,96);
            this.grpPrinter.Name = "grpPrinter";
            this.grpPrinter.Size = new System.Drawing.Size(152,129);
            this.grpPrinter.TabIndex = 114;
            this.grpPrinter.TabStop = false;
            this.grpPrinter.Text = "Printer Status (DTE)";
            // 
            // picRI
            // 
            this.picRI.Image = ((System.Drawing.Image)(resources.GetObject("picRI.Image")));
            this.picRI.Location = new System.Drawing.Point(84,93);
            this.picRI.Name = "picRI";
            this.picRI.Size = new System.Drawing.Size(18,18);
            this.picRI.TabIndex = 119;
            this.picRI.TabStop = false;
            // 
            // picCD
            // 
            this.picCD.Image = ((System.Drawing.Image)(resources.GetObject("picCD.Image")));
            this.picCD.Location = new System.Drawing.Point(84,69);
            this.picCD.Name = "picCD";
            this.picCD.Size = new System.Drawing.Size(18,18);
            this.picCD.TabIndex = 118;
            this.picCD.TabStop = false;
            // 
            // picCTS
            // 
            this.picCTS.Image = ((System.Drawing.Image)(resources.GetObject("picCTS.Image")));
            this.picCTS.Location = new System.Drawing.Point(84,45);
            this.picCTS.Name = "picCTS";
            this.picCTS.Size = new System.Drawing.Size(18,18);
            this.picCTS.TabIndex = 117;
            this.picCTS.TabStop = false;
            // 
            // picDSR
            // 
            this.picDSR.Image = ((System.Drawing.Image)(resources.GetObject("picDSR.Image")));
            this.picDSR.Location = new System.Drawing.Point(84,21);
            this.picDSR.Name = "picDSR";
            this.picDSR.Size = new System.Drawing.Size(18,18);
            this.picDSR.TabIndex = 116;
            this.picDSR.TabStop = false;
            // 
            // _lblCTS
            // 
            this._lblCTS.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblCTS.Location = new System.Drawing.Point(12,45);
            this._lblCTS.Name = "_lblCTS";
            this._lblCTS.Size = new System.Drawing.Size(48,18);
            this._lblCTS.TabIndex = 115;
            this._lblCTS.Text = "CTS";
            this._lblCTS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblCD
            // 
            this._lblCD.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblCD.Location = new System.Drawing.Point(12,69);
            this._lblCD.Name = "_lblCD";
            this._lblCD.Size = new System.Drawing.Size(48,18);
            this._lblCD.TabIndex = 114;
            this._lblCD.Text = "CD";
            this._lblCD.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblRI
            // 
            this._lblRI.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblRI.Location = new System.Drawing.Point(12,93);
            this._lblRI.Name = "_lblRI";
            this._lblRI.Size = new System.Drawing.Size(48,18);
            this._lblRI.TabIndex = 113;
            this._lblRI.Text = "RI";
            this._lblRI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblDSR
            // 
            this._lblDSR.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblDSR.Location = new System.Drawing.Point(12,21);
            this._lblDSR.Name = "_lblDSR";
            this._lblDSR.Size = new System.Drawing.Size(48,18);
            this._lblDSR.TabIndex = 112;
            this._lblDSR.Text = "DSR";
            this._lblDSR.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpHost
            // 
            this.grpHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHost.Controls.Add(this.chkRTS);
            this.grpHost.Controls.Add(this.chkDTR);
            this.grpHost.Location = new System.Drawing.Point(6,6);
            this.grpHost.Name = "grpHost";
            this.grpHost.Size = new System.Drawing.Size(152,84);
            this.grpHost.TabIndex = 113;
            this.grpHost.TabStop = false;
            this.grpHost.Text = "Host Status (DTE)";
            // 
            // chkRTS
            // 
            this.chkRTS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkRTS.Location = new System.Drawing.Point(12,53);
            this.chkRTS.Name = "chkRTS";
            this.chkRTS.Size = new System.Drawing.Size(131,18);
            this.chkRTS.TabIndex = 1;
            this.chkRTS.Text = "RTS";
            this.chkRTS.CheckedChanged += new System.EventHandler(this.OnRS232DTEChanged);
            // 
            // chkDTR
            // 
            this.chkDTR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkDTR.Location = new System.Drawing.Point(12,29);
            this.chkDTR.Name = "chkDTR";
            this.chkDTR.Size = new System.Drawing.Size(131,18);
            this.chkDTR.TabIndex = 0;
            this.chkDTR.Text = "DTR";
            this.chkDTR.CheckedChanged += new System.EventHandler(this.OnRS232DTEChanged);
            // 
            // tabPrinter
            // 
            this.tabPrinter.Controls.Add(this.grpHostInfo);
            this.tabPrinter.Controls.Add(this.grpHostStatus);
            this.tabPrinter.Controls.Add(this.grpHostMemoryStatus);
            this.tabPrinter.Location = new System.Drawing.Point(4,4);
            this.tabPrinter.Name = "tabPrinter";
            this.tabPrinter.Size = new System.Drawing.Size(168,329);
            this.tabPrinter.TabIndex = 2;
            this.tabPrinter.Text = "Printer";
            this.tabPrinter.Visible = false;
            // 
            // grpHostInfo
            // 
            this.grpHostInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHostInfo.Controls.Add(this.lblVersion);
            this.grpHostInfo.Controls.Add(this.lblModel);
            this.grpHostInfo.Controls.Add(this.lblResolution);
            this.grpHostInfo.Controls.Add(this.lblMemory);
            this.grpHostInfo.Location = new System.Drawing.Point(6,6);
            this.grpHostInfo.Name = "grpHostInfo";
            this.grpHostInfo.Size = new System.Drawing.Size(152,84);
            this.grpHostInfo.TabIndex = 115;
            this.grpHostInfo.TabStop = false;
            this.grpHostInfo.Text = "Host Information";
            // 
            // lblVersion
            // 
            this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVersion.Location = new System.Drawing.Point(12,53);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(128,18);
            this.lblVersion.TabIndex = 115;
            this.lblVersion.Text = "Version: ";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblModel
            // 
            this.lblModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblModel.Location = new System.Drawing.Point(12,29);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(128,18);
            this.lblModel.TabIndex = 112;
            this.lblModel.Text = "Model: ";
            this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResolution
            // 
            this.lblResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResolution.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblResolution.Location = new System.Drawing.Point(12,128);
            this.lblResolution.Name = "lblResolution";
            this.lblResolution.Size = new System.Drawing.Size(128,18);
            this.lblResolution.TabIndex = 114;
            this.lblResolution.Text = "Resolution: ";
            this.lblResolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblMemory
            // 
            this.lblMemory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMemory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblMemory.Location = new System.Drawing.Point(12,184);
            this.lblMemory.Name = "lblMemory";
            this.lblMemory.Size = new System.Drawing.Size(128,18);
            this.lblMemory.TabIndex = 113;
            this.lblMemory.Text = "Memory: ";
            this.lblMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpHostStatus
            // 
            this.grpHostStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHostStatus.Controls.Add(this.lblBufferCount);
            this.grpHostStatus.Controls.Add(this._lblBufferCount);
            this.grpHostStatus.Controls.Add(this.picLabelWaiting);
            this.grpHostStatus.Controls.Add(this.picRibbonOut);
            this.grpHostStatus.Controls.Add(this.picHeadUp);
            this.grpHostStatus.Controls.Add(this._lblRibbonOut);
            this.grpHostStatus.Controls.Add(this._lblLabelWaiting);
            this.grpHostStatus.Controls.Add(this._lblHeadUp);
            this.grpHostStatus.Controls.Add(this.picOverTemp);
            this.grpHostStatus.Controls.Add(this.picUnderTemp);
            this.grpHostStatus.Controls.Add(this.picCorruptRAM);
            this.grpHostStatus.Controls.Add(this.picPartialFormat);
            this.grpHostStatus.Controls.Add(this._lblCorruptRAM);
            this.grpHostStatus.Controls.Add(this._lblUnderTemp);
            this.grpHostStatus.Controls.Add(this._lblOverTemp);
            this.grpHostStatus.Controls.Add(this._lblPartialFormat);
            this.grpHostStatus.Controls.Add(this.picBufferFull);
            this.grpHostStatus.Controls.Add(this.picPauseOn);
            this.grpHostStatus.Controls.Add(this.picPaperOut);
            this.grpHostStatus.Controls.Add(this._lblPauseOn);
            this.grpHostStatus.Controls.Add(this._lblBufferFull);
            this.grpHostStatus.Controls.Add(this._lblPaperOut);
            this.grpHostStatus.Location = new System.Drawing.Point(6,96);
            this.grpHostStatus.Name = "grpHostStatus";
            this.grpHostStatus.Size = new System.Drawing.Size(152,291);
            this.grpHostStatus.TabIndex = 117;
            this.grpHostStatus.TabStop = false;
            this.grpHostStatus.Text = "Host Status";
            // 
            // lblBufferCount
            // 
            this.lblBufferCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblBufferCount.Location = new System.Drawing.Point(105,21);
            this.lblBufferCount.Name = "lblBufferCount";
            this.lblBufferCount.Size = new System.Drawing.Size(32,18);
            this.lblBufferCount.TabIndex = 139;
            this.lblBufferCount.Text = "000";
            this.lblBufferCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblBufferCount
            // 
            this._lblBufferCount.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblBufferCount.Location = new System.Drawing.Point(12,21);
            this._lblBufferCount.Name = "_lblBufferCount";
            this._lblBufferCount.Size = new System.Drawing.Size(81,18);
            this._lblBufferCount.TabIndex = 138;
            this._lblBufferCount.Text = "Buffer Count";
            this._lblBufferCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picLabelWaiting
            // 
            this.picLabelWaiting.Image = ((System.Drawing.Image)(resources.GetObject("picLabelWaiting.Image")));
            this.picLabelWaiting.Location = new System.Drawing.Point(114,93);
            this.picLabelWaiting.Name = "picLabelWaiting";
            this.picLabelWaiting.Size = new System.Drawing.Size(18,18);
            this.picLabelWaiting.TabIndex = 136;
            this.picLabelWaiting.TabStop = false;
            // 
            // picRibbonOut
            // 
            this.picRibbonOut.Image = ((System.Drawing.Image)(resources.GetObject("picRibbonOut.Image")));
            this.picRibbonOut.Location = new System.Drawing.Point(114,261);
            this.picRibbonOut.Name = "picRibbonOut";
            this.picRibbonOut.Size = new System.Drawing.Size(18,18);
            this.picRibbonOut.TabIndex = 135;
            this.picRibbonOut.TabStop = false;
            // 
            // picHeadUp
            // 
            this.picHeadUp.Image = ((System.Drawing.Image)(resources.GetObject("picHeadUp.Image")));
            this.picHeadUp.Location = new System.Drawing.Point(114,213);
            this.picHeadUp.Name = "picHeadUp";
            this.picHeadUp.Size = new System.Drawing.Size(18,18);
            this.picHeadUp.TabIndex = 134;
            this.picHeadUp.TabStop = false;
            // 
            // _lblRibbonOut
            // 
            this._lblRibbonOut.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblRibbonOut.Location = new System.Drawing.Point(12,261);
            this._lblRibbonOut.Name = "_lblRibbonOut";
            this._lblRibbonOut.Size = new System.Drawing.Size(90,18);
            this._lblRibbonOut.TabIndex = 133;
            this._lblRibbonOut.Text = "Ribbon Out";
            this._lblRibbonOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLabelWaiting
            // 
            this._lblLabelWaiting.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblLabelWaiting.Location = new System.Drawing.Point(12,93);
            this._lblLabelWaiting.Name = "_lblLabelWaiting";
            this._lblLabelWaiting.Size = new System.Drawing.Size(90,18);
            this._lblLabelWaiting.TabIndex = 132;
            this._lblLabelWaiting.Text = "Label Waiting";
            this._lblLabelWaiting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblHeadUp
            // 
            this._lblHeadUp.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblHeadUp.Location = new System.Drawing.Point(12,213);
            this._lblHeadUp.Name = "_lblHeadUp";
            this._lblHeadUp.Size = new System.Drawing.Size(90,18);
            this._lblHeadUp.TabIndex = 130;
            this._lblHeadUp.Text = "Head Up";
            this._lblHeadUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picOverTemp
            // 
            this.picOverTemp.Image = ((System.Drawing.Image)(resources.GetObject("picOverTemp.Image")));
            this.picOverTemp.Location = new System.Drawing.Point(114,189);
            this.picOverTemp.Name = "picOverTemp";
            this.picOverTemp.Size = new System.Drawing.Size(18,18);
            this.picOverTemp.TabIndex = 127;
            this.picOverTemp.TabStop = false;
            // 
            // picUnderTemp
            // 
            this.picUnderTemp.Image = ((System.Drawing.Image)(resources.GetObject("picUnderTemp.Image")));
            this.picUnderTemp.Location = new System.Drawing.Point(114,165);
            this.picUnderTemp.Name = "picUnderTemp";
            this.picUnderTemp.Size = new System.Drawing.Size(18,18);
            this.picUnderTemp.TabIndex = 126;
            this.picUnderTemp.TabStop = false;
            // 
            // picCorruptRAM
            // 
            this.picCorruptRAM.Image = ((System.Drawing.Image)(resources.GetObject("picCorruptRAM.Image")));
            this.picCorruptRAM.Location = new System.Drawing.Point(114,141);
            this.picCorruptRAM.Name = "picCorruptRAM";
            this.picCorruptRAM.Size = new System.Drawing.Size(18,18);
            this.picCorruptRAM.TabIndex = 125;
            this.picCorruptRAM.TabStop = false;
            // 
            // picPartialFormat
            // 
            this.picPartialFormat.Image = ((System.Drawing.Image)(resources.GetObject("picPartialFormat.Image")));
            this.picPartialFormat.Location = new System.Drawing.Point(114,69);
            this.picPartialFormat.Name = "picPartialFormat";
            this.picPartialFormat.Size = new System.Drawing.Size(18,18);
            this.picPartialFormat.TabIndex = 124;
            this.picPartialFormat.TabStop = false;
            // 
            // _lblCorruptRAM
            // 
            this._lblCorruptRAM.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblCorruptRAM.Location = new System.Drawing.Point(12,141);
            this._lblCorruptRAM.Name = "_lblCorruptRAM";
            this._lblCorruptRAM.Size = new System.Drawing.Size(90,18);
            this._lblCorruptRAM.TabIndex = 123;
            this._lblCorruptRAM.Text = "Corrupt RAM";
            this._lblCorruptRAM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblUnderTemp
            // 
            this._lblUnderTemp.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblUnderTemp.Location = new System.Drawing.Point(12,165);
            this._lblUnderTemp.Name = "_lblUnderTemp";
            this._lblUnderTemp.Size = new System.Drawing.Size(90,18);
            this._lblUnderTemp.TabIndex = 122;
            this._lblUnderTemp.Text = "Under Temp";
            this._lblUnderTemp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblOverTemp
            // 
            this._lblOverTemp.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblOverTemp.Location = new System.Drawing.Point(12,189);
            this._lblOverTemp.Name = "_lblOverTemp";
            this._lblOverTemp.Size = new System.Drawing.Size(90,18);
            this._lblOverTemp.TabIndex = 121;
            this._lblOverTemp.Text = "Over Temp";
            this._lblOverTemp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblPartialFormat
            // 
            this._lblPartialFormat.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblPartialFormat.Location = new System.Drawing.Point(12,69);
            this._lblPartialFormat.Name = "_lblPartialFormat";
            this._lblPartialFormat.Size = new System.Drawing.Size(90,18);
            this._lblPartialFormat.TabIndex = 120;
            this._lblPartialFormat.Text = "Part Format";
            this._lblPartialFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picBufferFull
            // 
            this.picBufferFull.Image = ((System.Drawing.Image)(resources.GetObject("picBufferFull.Image")));
            this.picBufferFull.Location = new System.Drawing.Point(114,45);
            this.picBufferFull.Name = "picBufferFull";
            this.picBufferFull.Size = new System.Drawing.Size(18,18);
            this.picBufferFull.TabIndex = 118;
            this.picBufferFull.TabStop = false;
            // 
            // picPauseOn
            // 
            this.picPauseOn.Image = ((System.Drawing.Image)(resources.GetObject("picPauseOn.Image")));
            this.picPauseOn.Location = new System.Drawing.Point(114,117);
            this.picPauseOn.Name = "picPauseOn";
            this.picPauseOn.Size = new System.Drawing.Size(18,18);
            this.picPauseOn.TabIndex = 117;
            this.picPauseOn.TabStop = false;
            // 
            // picPaperOut
            // 
            this.picPaperOut.Image = ((System.Drawing.Image)(resources.GetObject("picPaperOut.Image")));
            this.picPaperOut.Location = new System.Drawing.Point(114,237);
            this.picPaperOut.Name = "picPaperOut";
            this.picPaperOut.Size = new System.Drawing.Size(18,18);
            this.picPaperOut.TabIndex = 116;
            this.picPaperOut.TabStop = false;
            // 
            // _lblPauseOn
            // 
            this._lblPauseOn.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblPauseOn.Location = new System.Drawing.Point(12,117);
            this._lblPauseOn.Name = "_lblPauseOn";
            this._lblPauseOn.Size = new System.Drawing.Size(90,18);
            this._lblPauseOn.TabIndex = 115;
            this._lblPauseOn.Text = "Pause On";
            this._lblPauseOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblBufferFull
            // 
            this._lblBufferFull.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblBufferFull.Location = new System.Drawing.Point(12,45);
            this._lblBufferFull.Name = "_lblBufferFull";
            this._lblBufferFull.Size = new System.Drawing.Size(90,18);
            this._lblBufferFull.TabIndex = 114;
            this._lblBufferFull.Text = "Buffer Full";
            this._lblBufferFull.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblPaperOut
            // 
            this._lblPaperOut.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblPaperOut.Location = new System.Drawing.Point(12,237);
            this._lblPaperOut.Name = "_lblPaperOut";
            this._lblPaperOut.Size = new System.Drawing.Size(90,18);
            this._lblPaperOut.TabIndex = 112;
            this._lblPaperOut.Text = "Paper Out";
            this._lblPaperOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // grpHostMemoryStatus
            // 
            this.grpHostMemoryStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpHostMemoryStatus.Controls.Add(this.lblRAMMax);
            this.grpHostMemoryStatus.Controls.Add(this.lblRAMAvailable);
            this.grpHostMemoryStatus.Controls.Add(this.lblRAMTotal);
            this.grpHostMemoryStatus.Location = new System.Drawing.Point(6,393);
            this.grpHostMemoryStatus.Name = "grpHostMemoryStatus";
            this.grpHostMemoryStatus.Size = new System.Drawing.Size(152,105);
            this.grpHostMemoryStatus.TabIndex = 116;
            this.grpHostMemoryStatus.TabStop = false;
            this.grpHostMemoryStatus.Text = "Host Memory";
            // 
            // lblRAMMax
            // 
            this.lblRAMMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRAMMax.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRAMMax.Location = new System.Drawing.Point(12,53);
            this.lblRAMMax.Name = "lblRAMMax";
            this.lblRAMMax.Size = new System.Drawing.Size(128,18);
            this.lblRAMMax.TabIndex = 115;
            this.lblRAMMax.Text = "Max: ";
            this.lblRAMMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRAMAvailable
            // 
            this.lblRAMAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRAMAvailable.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRAMAvailable.Location = new System.Drawing.Point(12,77);
            this.lblRAMAvailable.Name = "lblRAMAvailable";
            this.lblRAMAvailable.Size = new System.Drawing.Size(128,18);
            this.lblRAMAvailable.TabIndex = 114;
            this.lblRAMAvailable.Text = "Available: ";
            this.lblRAMAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRAMTotal
            // 
            this.lblRAMTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRAMTotal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRAMTotal.Location = new System.Drawing.Point(12,29);
            this.lblRAMTotal.Name = "lblRAMTotal";
            this.lblRAMTotal.Size = new System.Drawing.Size(128,18);
            this.lblRAMTotal.TabIndex = 112;
            this.lblRAMTotal.Text = "Total: ";
            this.lblRAMTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.lblToolbox.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblToolbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblToolbox.Location = new System.Drawing.Point(3,3);
            this.lblToolbox.Name = "lblToolbox";
            this.lblToolbox.Size = new System.Drawing.Size(168,18);
            this.lblToolbox.TabIndex = 119;
            this.lblToolbox.Text = "Toolbox";
            this.lblToolbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.msEdit,
            this.msView,
            this.msZebra,
            this.msTools,
            this.msWindows,
            this.msHelp});
            this.msMain.Location = new System.Drawing.Point(0,0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(760,24);
            this.msMain.TabIndex = 111;
            this.msMain.Text = "menuStrip1";
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileOpen,
            this.msFileSep1,
            this.msFileSave,
            this.msFileSaveAs,
            this.msFileSep2,
            this.msFileOpenPort,
            this.msFileClosePort,
            this.msFileSep3,
            this.msFileSettings,
            this.msFilePrint,
            this.msFilePrintLabel,
            this.msFileSep4,
            this.msFileExit});
            this.msFile.Name = "msFile";
            this.msFile.Size = new System.Drawing.Size(37,20);
            this.msFile.Text = "&File";
            // 
            // msFileNew
            // 
            this.msFileNew.Image = global::Tsort.Properties.Resources.NewDocument;
            this.msFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.Size = new System.Drawing.Size(145,22);
            this.msFileNew.Text = "&New";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Tsort.Properties.Resources.Open;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(145,22);
            this.msFileOpen.Text = "&Open";
            this.msFileOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(142,6);
            // 
            // msFileSave
            // 
            this.msFileSave.Image = global::Tsort.Properties.Resources.Save;
            this.msFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSave.Name = "msFileSave";
            this.msFileSave.Size = new System.Drawing.Size(145,22);
            this.msFileSave.Text = "&Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(145,22);
            this.msFileSaveAs.Text = "Save &As";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(142,6);
            // 
            // msFileOpenPort
            // 
            this.msFileOpenPort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpenPort.Name = "msFileOpenPort";
            this.msFileOpenPort.Size = new System.Drawing.Size(145,22);
            this.msFileOpenPort.Text = "Open Port";
            this.msFileOpenPort.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileClosePort
            // 
            this.msFileClosePort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileClosePort.Name = "msFileClosePort";
            this.msFileClosePort.Size = new System.Drawing.Size(145,22);
            this.msFileClosePort.Text = "Close Port";
            this.msFileClosePort.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(142,6);
            // 
            // msFileSettings
            // 
            this.msFileSettings.Image = global::Tsort.Properties.Resources.PrintSetup;
            this.msFileSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSettings.Name = "msFileSettings";
            this.msFileSettings.Size = new System.Drawing.Size(145,22);
            this.msFileSettings.Text = "Page &Settings";
            this.msFileSettings.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Tsort.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(145,22);
            this.msFilePrint.Text = "&Print";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFilePrintLabel
            // 
            this.msFilePrintLabel.Image = global::Tsort.Properties.Resources.Envelope;
            this.msFilePrintLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrintLabel.Name = "msFilePrintLabel";
            this.msFilePrintLabel.Size = new System.Drawing.Size(145,22);
            this.msFilePrintLabel.Text = "&Print Label";
            this.msFilePrintLabel.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep4
            // 
            this.msFileSep4.Name = "msFileSep4";
            this.msFileSep4.Size = new System.Drawing.Size(142,6);
            // 
            // msFileExit
            // 
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(145,22);
            this.msFileExit.Text = "E&xit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEdit
            // 
            this.msEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msEditUndo,
            this.msEditRedo,
            this.msEditSep1,
            this.msEditCut,
            this.msEditCopy,
            this.msEditPaste,
            this.msEditSep2,
            this.msEditFind});
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39,20);
            this.msEdit.Text = "&Edit";
            // 
            // msEditUndo
            // 
            this.msEditUndo.Image = global::Tsort.Properties.Resources.Edit_Undo;
            this.msEditUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditUndo.Name = "msEditUndo";
            this.msEditUndo.Size = new System.Drawing.Size(164,22);
            this.msEditUndo.Text = "&Undo";
            this.msEditUndo.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditRedo
            // 
            this.msEditRedo.Image = global::Tsort.Properties.Resources.Edit_Redo;
            this.msEditRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditRedo.Name = "msEditRedo";
            this.msEditRedo.Size = new System.Drawing.Size(164,22);
            this.msEditRedo.Text = "&Redo";
            this.msEditRedo.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditSep1
            // 
            this.msEditSep1.Name = "msEditSep1";
            this.msEditSep1.Size = new System.Drawing.Size(161,6);
            // 
            // msEditCut
            // 
            this.msEditCut.Name = "msEditCut";
            this.msEditCut.Size = new System.Drawing.Size(164,22);
            this.msEditCut.Text = "C&ut";
            this.msEditCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditCopy
            // 
            this.msEditCopy.Name = "msEditCopy";
            this.msEditCopy.Size = new System.Drawing.Size(164,22);
            this.msEditCopy.Text = "&Copy";
            this.msEditCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditPaste
            // 
            this.msEditPaste.Name = "msEditPaste";
            this.msEditPaste.Size = new System.Drawing.Size(164,22);
            this.msEditPaste.Text = "&Paste";
            this.msEditPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditSep2
            // 
            this.msEditSep2.Name = "msEditSep2";
            this.msEditSep2.Size = new System.Drawing.Size(161,6);
            // 
            // msEditFind
            // 
            this.msEditFind.Name = "msEditFind";
            this.msEditFind.Size = new System.Drawing.Size(164,22);
            this.msEditFind.Text = "&Find and Replace";
            this.msEditFind.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewLabelMaker,
            this.msViewSerialPort,
            this.msViewRS232,
            this.msViewZebraPrinter,
            this.msViewSep2,
            this.msViewStatusLog,
            this.msViewSep3,
            this.msViewToolbar,
            this.msViewStatusBar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44,20);
            this.msView.Text = "&View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Image = global::Tsort.Properties.Resources.Refresh;
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(142,22);
            this.msViewRefresh.Text = "Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(139,6);
            // 
            // msViewLabelMaker
            // 
            this.msViewLabelMaker.Name = "msViewLabelMaker";
            this.msViewLabelMaker.Size = new System.Drawing.Size(142,22);
            this.msViewLabelMaker.Text = "Label &Maker";
            this.msViewLabelMaker.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSerialPort
            // 
            this.msViewSerialPort.Name = "msViewSerialPort";
            this.msViewSerialPort.Size = new System.Drawing.Size(142,22);
            this.msViewSerialPort.Text = "Serial &Port";
            this.msViewSerialPort.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewRS232
            // 
            this.msViewRS232.Name = "msViewRS232";
            this.msViewRS232.Size = new System.Drawing.Size(142,22);
            this.msViewRS232.Text = "&RS-232";
            this.msViewRS232.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewZebraPrinter
            // 
            this.msViewZebraPrinter.Name = "msViewZebraPrinter";
            this.msViewZebraPrinter.Size = new System.Drawing.Size(142,22);
            this.msViewZebraPrinter.Text = "&Zebra Printer";
            this.msViewZebraPrinter.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(139,6);
            // 
            // msViewStatusLog
            // 
            this.msViewStatusLog.Name = "msViewStatusLog";
            this.msViewStatusLog.Size = new System.Drawing.Size(142,22);
            this.msViewStatusLog.Text = "Status &Log";
            this.msViewStatusLog.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep3
            // 
            this.msViewSep3.Name = "msViewSep3";
            this.msViewSep3.Size = new System.Drawing.Size(139,6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(142,22);
            this.msViewToolbar.Text = "&Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(142,22);
            this.msViewStatusBar.Text = "&Status Bar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msZebra
            // 
            this.msZebra.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msZebraHostStatus});
            this.msZebra.Name = "msZebra";
            this.msZebra.Size = new System.Drawing.Size(49,20);
            this.msZebra.Text = "&Zebra";
            // 
            // msZebraHostStatus
            // 
            this.msZebraHostStatus.Name = "msZebraHostStatus";
            this.msZebraHostStatus.Size = new System.Drawing.Size(134,22);
            this.msZebraHostStatus.Text = "&Host Status";
            this.msZebraHostStatus.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msTools
            // 
            this.msTools.Name = "msTools";
            this.msTools.Size = new System.Drawing.Size(48,20);
            this.msTools.Text = "&Tools";
            // 
            // msWindows
            // 
            this.msWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msWindowsCascade,
            this.msWindowsTileH,
            this.msWindowsTileV});
            this.msWindows.Name = "msWindows";
            this.msWindows.Size = new System.Drawing.Size(63,20);
            this.msWindows.Text = "&Window";
            // 
            // msWindowsCascade
            // 
            this.msWindowsCascade.Name = "msWindowsCascade";
            this.msWindowsCascade.Size = new System.Drawing.Size(160,22);
            this.msWindowsCascade.Text = "&Cascade";
            this.msWindowsCascade.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msWindowsTileH
            // 
            this.msWindowsTileH.Name = "msWindowsTileH";
            this.msWindowsTileH.Size = new System.Drawing.Size(160,22);
            this.msWindowsTileH.Text = "Tile &Horizontally";
            this.msWindowsTileH.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msWindowsTileV
            // 
            this.msWindowsTileV.Name = "msWindowsTileV";
            this.msWindowsTileV.Size = new System.Drawing.Size(160,22);
            this.msWindowsTileV.Text = "Tile &Vertically";
            this.msWindowsTileV.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout,
            this.msHelpSep1});
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44,20);
            this.msHelp.Text = "&Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(107,22);
            this.msHelpAbout.Text = "&About";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(104,6);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsOpen,
            this.tsSep1,
            this.tsSave,
            this.tsPrint,
            this.tsPrintLabel,
            this.tsSep2,
            this.tsUndo,
            this.tsRedo,
            this.tsSep3,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsSep4,
            this.tsFind,
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0,24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(760,25);
            this.tsMain.TabIndex = 113;
            this.tsMain.Text = "toolStrip1";
            // 
            // tsNew
            // 
            this.tsNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsNew.Image = global::Tsort.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(23,22);
            this.tsNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsOpen
            // 
            this.tsOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsOpen.Image = global::Tsort.Properties.Resources.Open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(23,22);
            this.tsOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6,25);
            // 
            // tsSave
            // 
            this.tsSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsSave.Image = global::Tsort.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(23,22);
            this.tsSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsPrint
            // 
            this.tsPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrint.Image = global::Tsort.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(23,22);
            this.tsPrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsPrintLabel
            // 
            this.tsPrintLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPrintLabel.Image = global::Tsort.Properties.Resources.Envelope;
            this.tsPrintLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrintLabel.Name = "tsPrintLabel";
            this.tsPrintLabel.Size = new System.Drawing.Size(23,22);
            this.tsPrintLabel.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6,25);
            // 
            // tsUndo
            // 
            this.tsUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsUndo.Image = global::Tsort.Properties.Resources.Edit_Undo;
            this.tsUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUndo.Name = "tsUndo";
            this.tsUndo.Size = new System.Drawing.Size(23,22);
            this.tsUndo.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsRedo
            // 
            this.tsRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRedo.Image = global::Tsort.Properties.Resources.Edit_Redo;
            this.tsRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRedo.Name = "tsRedo";
            this.tsRedo.Size = new System.Drawing.Size(23,22);
            this.tsRedo.Text = "toolStripButton2";
            this.tsRedo.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6,25);
            // 
            // tsCut
            // 
            this.tsCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsCut.Image = global::Tsort.Properties.Resources.Cut;
            this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCut.Name = "tsCut";
            this.tsCut.Size = new System.Drawing.Size(23,22);
            this.tsCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsCopy
            // 
            this.tsCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsCopy.Image = global::Tsort.Properties.Resources.Copy;
            this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopy.Name = "tsCopy";
            this.tsCopy.Size = new System.Drawing.Size(23,22);
            this.tsCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsPaste
            // 
            this.tsPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsPaste.Image = global::Tsort.Properties.Resources.Paste;
            this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.Size = new System.Drawing.Size(23,22);
            this.tsPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep4
            // 
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6,25);
            // 
            // tsFind
            // 
            this.tsFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsFind.Image = global::Tsort.Properties.Resources.Find;
            this.tsFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFind.Name = "tsFind";
            this.tsFind.Size = new System.Drawing.Size(23,22);
            this.tsFind.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsRefresh
            // 
            this.tsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRefresh.Image = global::Tsort.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(23,22);
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(760,554);
            this.Controls.Add(this.pnlToolbox);
            this.Controls.Add(this.pnlStatusLog);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.splitterVL);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.splitterVR);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Label Studio";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closed += new System.EventHandler(this.OnFormClosed);
            this.MdiChildActivate += new System.EventHandler(this.OnMdiChildActivate);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.csNav.ResumeLayout(false);
            this.pnlNav.ResumeLayout(false);
            this.pnlLabelMaker.ResumeLayout(false);
            this.tabLabelMaker.ResumeLayout(false);
            this.tabTokens.ResumeLayout(false);
            this.tabValues.ResumeLayout(false);
            this.pnlStatusLog.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabStatusLog.ResumeLayout(false);
            this.csLog.ResumeLayout(false);
            this.pnlLog.ResumeLayout(false);
            this.pnlToolbox.ResumeLayout(false);
            this.tabToolbox.ResumeLayout(false);
            this.tabPort.ResumeLayout(false);
            this.tabRS232.ResumeLayout(false);
            this.grpPrinter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picRI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCTS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDSR)).EndInit();
            this.grpHost.ResumeLayout(false);
            this.tabPrinter.ResumeLayout(false);
            this.grpHostInfo.ResumeLayout(false);
            this.grpHostStatus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLabelWaiting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRibbonOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeadUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOverTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picUnderTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCorruptRAM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPartialFormat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picBufferFull)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPauseOn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPaperOut)).EndInit();
            this.grpHostMemoryStatus.ResumeLayout(false);
            this.pnlToolboxTitlebar.ResumeLayout(false);
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
                #region User preferences
                //string sWindowSettings = (string)UserSettings.Read("WindowSettings", "Normal,56,35,925,628");
                //char[] token = {Convert.ToChar(",")};
                //string[] settings = sWindowSettings.Split(token, 5);
                //switch(settings[0]) {
                //    case "Maximized":	this.WindowState = FormWindowState.Maximized; break;
                //    case "Minimized":	this.WindowState = FormWindowState.Minimized; break;
                //    default:
                //        this.WindowState = FormWindowState.Normal;
                //        this.Left = Convert.ToInt32(settings[1]);
                //        this.Top = Convert.ToInt32(settings[2]);
                //        this.Width = Convert.ToInt32(settings[3]);
                //        this.Height = Convert.ToInt32(settings[4]);
                //        break;
                //}
                //this.msViewLabelMaker.Checked = this.msViewSerialPort.Checked = this.msViewRS232.Checked = this.msViewZebraPrinter.Checked = this.mnuViewEventLog.Checked = false;
                //this.tabToolbox.TabPages.Clear();
                //this.msViewLabelMaker.Checked = !Convert.ToBoolean(UserSettings.Read("ViewLabelMaker", true)); 
                //this.msViewLabelMaker.PerformClick();
                //if(Convert.ToBoolean(UserSettings.Read("ViewPort", true))) this.msViewSerialPort.PerformClick();
                //if(Convert.ToBoolean(UserSettings.Read("ViewRS232", true))) this.msViewRS232.PerformClick();
                //if(Convert.ToBoolean(UserSettings.Read("ViewPrinter", true))) this.msViewZebraPrinter.PerformClick();
                //this.mnuViewEventLog.Checked = !Convert.ToBoolean(UserSettings.Read("ViewEventLog", true)); 
                //this.mnuViewEventLog.PerformClick();
                //this.mnuViewToolbar.Checked = this.tlbMain.Visible = Convert.ToBoolean(UserSettings.Read("Toolbar", true));
                //this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(UserSettings.Read("StatusBar", true));
                //this.pnlLabelMaker.Height = Convert.ToInt32(UserSettings.Read("LabelMakerHeight", "288"));
                //this.pnlStatusLog.Height = Convert.ToInt32(UserSettings.Read("StatusLogHeight", "144"));
                //Application.DoEvents();
                #endregion
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				this.mToolTip.SetToolTip(this.stbMain, "Click to clear message (messages stored in the Event Viewer T2Log log).");
				this.mToolTip.SetToolTip(this.lblClose, "Close");
				this.mToolTip.SetToolTip(this.lblCloseLog, "Close");
				#endregion
				
				//Initialize controls
				this.trvStores.Nodes.Clear();
				this.grdTokens.SelectedObject = new TokenLibrary();
				this.cboLabelMakers.SelectedIndex = 0;
                OnLabelMakerSelected(null,null);
				OnRS232DCEChanged(null, null);
				this.mMessageMgr.AddMessage("Loading label stores...");
				EnterpriseNode node = new EnterpriseNode("Label Template Stores", App.ICON_CLOSED, App.ICON_CLOSED);
				this.trvStores.Nodes.Add(node);
				node.LoadChildNodes();
				this.trvStores.SelectedNode = this.trvStores.Nodes[0];
				this.trvStores.Nodes[0].Expand();
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { }
		private void OnFormClosed(object sender, System.EventArgs e) {
			//Event handler for after form is closed
			try {
				if(this.ctlPrinters.Printer.On) this.ctlPrinters.Printer.TurnOff();
				#region Update registry settings
                //UserSettings.Write("ViewLabelMaker", this.msViewLabelMaker.Checked);
                //UserSettings.Write("ViewPort", this.msViewSerialPort.Checked);
                //UserSettings.Write("ViewRS232", this.msViewRS232.Checked);
                //UserSettings.Write("ViewPrinter", this.msViewZebraPrinter.Checked);
                //UserSettings.Write("ViewEventLog", this.mnuViewEventLog.Checked);
                //UserSettings.Write("Toolbar", this.mnuViewToolbar.Checked);
                //UserSettings.Write("StatusBar", this.mnuViewStatusBar.Checked);
                //UserSettings.Write("LabelMakerHeight", this.pnlLabelMaker.Height.ToString());
                //UserSettings.Write("StatusLogHeight", this.pnlStatusLog.Height.ToString());
                //UserSettings.Write("WindowSettings", this.WindowState.ToString() + "," + this.Left + "," + this.Top + "," + this.Width + "," + this.Height);
				#endregion
			}
			catch(Exception) { }
		}
        private void OnMdiChildActivate(object sender,EventArgs e) { setUserServices(); }
		private void OnServiceStatesChanged(object sender, System.EventArgs e) { setUserServices(); }
		#region Navigation Services: OnTreeNodeCollapsed(), OnTreeNodeExpanded(), OnTreeNodeMouseDown(), OnTreeNodeSelected(), OnTreeNodeDoubleClicked()
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
		private void OnTreeNodeMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down on a tree node
			try { 
				if(e.Button == System.Windows.Forms.MouseButtons.Right) 
					this.trvStores.SelectedNode = this.trvStores.GetNodeAt(e.X, e.Y);
			}
			catch(Exception ex)  { App.ReportError(ex); }
		}
		private void OnTreeNodeSelected(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Event handler for node selected
            setUserServices();
		}
		private void OnTreeNodeDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for tree node double-clicked
			this.msFileOpen.PerformClick();
		}
		#endregion
		#region LabelMaker Services: OnLabelMakerSelected(), OnCloseLabelMaker(), OnEnterLabelMaker(), OnLeaveLabelMaker()
		private void OnLabelMakerSelected(object sender, System.EventArgs e) {
			//Event handler for change in selected label maker
			try {
				//Update selected labelmaker
				this.mLabelMaker = null;
				switch(this.cboLabelMakers.Text) {
					case "User Specified":	this.mLabelMaker = new ManualLabelMaker(); break;
					default:				this.mLabelMaker = null; break;
				}
				this.grdValues.SelectedObject = this.mLabelMaker;
				
				//Change labelmaker in open label templates
				for(int i=0; i<this.MdiChildren.Length; i++) {
					winLabel win = (winLabel)this.MdiChildren[i];
					win.LabelMaker = this.mLabelMaker;
				}
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnCloseLabelMaker(object sender, System.EventArgs e) {
			//Event handler to close labelmaker windows
			this.msViewLabelMaker.PerformClick();
		}
		private void OnEnterLabelMaker(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			this.lblLabelMaker.BackColor = this.lblClose.BackColor = SystemColors.ActiveCaption;
			this.lblLabelMaker.ForeColor = this.lblClose.ForeColor = SystemColors.ActiveCaptionText;
		}
		private void OnLeaveLabelMaker(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			this.lblLabelMaker.BackColor = this.lblClose.BackColor = SystemColors.Control;
			this.lblLabelMaker.ForeColor = this.lblClose.ForeColor = SystemColors.ControlText;
		}
		#endregion
		#region Toolbox Services: Serial Port, RS232 Comm, Printer Status, Toolbox Support Code
		#region Serial Port
        private void OnPrinterChanged(object sender,EventArgs e) {
            //
            setUserServices();
        }
        #endregion
		#region RS232 Communications
		private void OnRS232DTEChanged(object sender, EventArgs e) {
			//Event handler for user changes to host DTR/RTS value
			try {
				if(this.ctlPrinters.Printer != null && this.ctlPrinters.Printer.On) {
                    CheckBox chk = (CheckBox)sender;
                    switch(chk.Name) {
                        case "chkDTR":
                            this.ctlPrinters.Printer.DtrEnable = chk.Checked;
                            LogEvent(LOG_ID_RS232 + ": DTR= " + (this.ctlPrinters.Printer.DtrEnable ? "High" : "Low") + "\n");
                            break;
                        case "chkRTS":
                            this.ctlPrinters.Printer.RtsEnable = chk.Checked;
                            LogEvent(LOG_ID_RS232 + ": RTS= " + (this.ctlPrinters.Printer.RtsEnable ? "High" : "Low") + "\n");
                            break;
                    }
				}
			} 
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
        private void OnRS232DCEChanged(object sender,System.IO.Ports.SerialPinChangedEventArgs e) {
			//Event handler for RS232 port pin change notification from te printer
			try {
				if(this.ctlPrinters.Printer != null) {
					//Update DTE control
                    this.chkDTR.Checked = this.ctlPrinters.Printer.DtrEnable;
                    this.chkRTS.Checked = this.ctlPrinters.Printer.RtsEnable;
					
					//Update DCE control: printer must be on (COM port open)
					this.picDSR.Image = this.picCTS.Image = this.picCD.Image = this.picRI.Image = this.imgRS232.Images[0];
					if(this.ctlPrinters.Printer.On) {
                        this.picDSR.Image = this.ctlPrinters.Printer.DsrHolding ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
                        this.picCTS.Image = this.ctlPrinters.Printer.CtsHolding ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
                        this.picCD.Image = this.ctlPrinters.Printer.CDHolding ? this.imgRS232.Images[2] : this.imgRS232.Images[1];

                        if(e!=null && (e.EventType & System.IO.Ports.SerialPinChange.DsrChanged) > 0)
                            LogEvent(LOG_ID_RS232 + ": " + ((this.ctlPrinters.Printer.DsrHolding) ? "DSR High" : "DSR Low") + " \n");
                        if(e!=null && (e.EventType & System.IO.Ports.SerialPinChange.CDChanged) > 0)
                            LogEvent(LOG_ID_RS232 + ": " + ((this.ctlPrinters.Printer.CDHolding) ? "CD High" : "CD Low") + " \n");
                        if(e!=null && (e.EventType & System.IO.Ports.SerialPinChange.CtsChanged) > 0)
                            LogEvent(LOG_ID_RS232 + ": " + ((this.ctlPrinters.Printer.CtsHolding) ? "CTS High" : "CTS Low") + " \n");
                        if(e!=null && (e.EventType & System.IO.Ports.SerialPinChange.Ring) > 0)
                            LogEvent(LOG_ID_RS232 + ": Ring detected \n");
                    }
					refreshPrinterStatus();
				}
				else {
					this.chkDTR.CheckState = this.chkRTS.CheckState = CheckState.Indeterminate;
					this.picDSR.Image = this.picCTS.Image = this.picCD.Image = this.picRI.Image = this.imgRS232.Images[0];
				}
			} 
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
        private void OnRS232HandshakeChanged(object sender,EventArgs e) {
			//Event handler for user changed handshaking
			try {
				if(this.ctlPrinters.Printer != null && this.ctlPrinters.Printer.On) {
                    PortSettings settings = this.ctlPrinters.Printer.Settings;
					switch(this.cboHandshaking.Text.ToLower()) {
                        case "rts/cts":     settings.Handshake = System.IO.Ports.Handshake.RequestToSend; break;
                        case "xon/xoff":    settings.Handshake = System.IO.Ports.Handshake.XOnXOff; break;
                        default:            settings.Handshake = System.IO.Ports.Handshake.None; break;
					}
                    this.ctlPrinters.Printer.Settings = settings;
                    LogEvent(LOG_ID_RS232 + ": Handshaking= " + this.ctlPrinters.Printer.Settings.Handshake.ToString() + "\n");
				}
			} 
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
        private void OnRS232Error(object sender,System.IO.Ports.SerialErrorReceivedEventArgs e) {
			//Event handler for RS232 error notification from the printer
			try {
				LogEvent(LOG_ID_RS232 + ": Comm Error- " + (int)e.EventType + "\n");
			} 
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
		#endregion
		#region Printer Status
		private void refreshPrinterStatus() {
			//
			try {
				OnHostInfoChanged(null, new PrinterInfoArgs("","","","",""));
				OnHostMemoryStatusChanged(null, new PrinterMemoryStatusArgs("","",""));
				OnHostStatusChanged(null, new PrinterStatusArgs("","","","","","","","","","","","","","","","","","","","","","","","",""));
                if(this.ctlPrinters.Printer != null && this.ctlPrinters.Printer.On) {
                    if(this.ctlPrinters.Printer is Zebra) {
                        Zebra p = (Zebra)this.ctlPrinters.Printer;
                        p.GetHostInfo();
                    }
                }
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
		private void OnPrinterReceivedData(object sender, PrinterDataArgs e) { LogEvent(LOG_ID_PRINTER + ": Received= " + e.Data + "\n"); }
		private void OnPrinterSentData(object sender, PrinterDataArgs e) { LogEvent(LOG_ID_PRINTER + ": Sent= " + e.Data + "\n"); }
		private void OnHostInfoChanged(object sender, PrinterInfoArgs e) {
			//Event handler for change in host information
			this.lblModel.Text = "Model: " + e.Model;
			this.lblVersion.Text = "Version: " + e.Version;
			this.lblResolution.Text = "Resolution: " + e.Resolution;
			this.lblMemory.Text = "Memory: " + e.Memory;
			LogEvent(LOG_ID_PRINTER + ": Host Info Updated...\n");
		}
		private void OnHostMemoryStatusChanged(object sender, PrinterMemoryStatusArgs e) {
			//Event handler for change in host memory
			this.lblRAMTotal.Text = "Total: " + e.RAMTotal + "kB";
			this.lblRAMMax.Text = "Maxim: " + e.RAMMax + "kB";
			this.lblRAMAvailable.Text = "Avail: " + e.RAMAvailable + "kB";
			LogEvent(LOG_ID_PRINTER + ": Host Memory Status Updated...\n");
		}
		private void OnHostStatusChanged(object sender, PrinterStatusArgs e) {
			//Event handler for change in host status
			if(sender==null) {
				this.lblBufferCount.Text = "";
				this.picPaperOut.Image = this.imgRS232.Images[0];
				this.picPauseOn.Image = this.imgRS232.Images[0];
				this.picBufferFull.Image = this.imgRS232.Images[0];
				this.picPartialFormat.Image = this.imgRS232.Images[0];
				this.picCorruptRAM.Image = this.imgRS232.Images[0];
				this.picUnderTemp.Image = this.imgRS232.Images[0];
				this.picOverTemp.Image = this.imgRS232.Images[0];
				this.picHeadUp.Image = this.imgRS232.Images[0];
				this.picRibbonOut.Image = this.imgRS232.Images[0];
				this.picLabelWaiting.Image = this.imgRS232.Images[0];
			}
			else {
				this.picPaperOut.Image = (e.PaperOut=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picPauseOn.Image = (e.Pause=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.lblBufferCount.Text = e.FormatsInReceiveBuffer;
				this.picBufferFull.Image = (e.BufferFull=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picPartialFormat.Image = (e.PartialFormat=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picCorruptRAM.Image = (e.CorruptRAM=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picUnderTemp.Image = (e.UnderTemp=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picOverTemp.Image = (e.OverTemp=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picHeadUp.Image = (e.HeadUp=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picRibbonOut.Image = (e.RibbonOut=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
				this.picLabelWaiting.Image = (e.LabelWaiting=="1") ? this.imgRS232.Images[2] : this.imgRS232.Images[1];
			}
			LogEvent(LOG_ID_PRINTER + ": Host Status Updated...\n");
		}
		#endregion
		#region Toolbox Support Code: InitializeToolbox(), OnToolboxResize(), ...
		private const string TABTLB_AUTOHIDE_OFF = "X";
		private const string TABTLB_AUTOHIDE_ON = "-";
		private void InitializeToolbox() {
			//Configure toolbox size, state, and event handlers
			try {
				//Set parent tab control, splitter, and pin button event handlers
				this.tabToolbox.Enter += new System.EventHandler(this.OnEnterToolbox);
				this.tabToolbox.Leave += new System.EventHandler(this.OnLeaveToolbox);
				this.tabToolbox.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
				this.tabToolbox.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
				this.splitterVR.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
				this.splitterVR.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
				this.tabToolbox.SizeChanged += new System.EventHandler(this.OnToolboxResize);
			
				//Set event handlers for each control contained within the tab control
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
				this.pnlToolbox.Width = 192;
				this.lblPin.Text = TABTLB_AUTOHIDE_OFF;
				this.lblPin.Click += new System.EventHandler(this.OnToggleAutoHide);
				this.tmrAutoHide.Interval = 1000;
				this.tmrAutoHide.Tick += new System.EventHandler(this.OnAutoHideToolbox);

				//Show toolbar as inactive
				this.lblToolbox.BackColor = System.Drawing.SystemColors.Control;
				this.lblToolbox.ForeColor = System.Drawing.SystemColors.ControlText;
				this.lblPin.BackColor = System.Drawing.SystemColors.Control;
				this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnToolboxResize(object sender, System.EventArgs e) {
			//Toolbox size changed event handler
			try {
				//Max at 360px
				if(this.pnlToolbox.Width>=384) this.pnlToolbox.Width = 384;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnEnterToolbox(object sender, System.EventArgs e) {
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
		private void OnLeaveToolbox(object sender, System.EventArgs e) {
			//Occurs when the control is no longer the active control on the form
			try {
				//Enable auto-hide when inactive and not pinned; show toolbar as inactive
				if(this.lblPin.Text==TABTLB_AUTOHIDE_ON) {
					this.tmrAutoHide.Enabled = true;
					this.tmrAutoHide.Start();
				}
                this.lblToolbox.BackColor = this.lblPin.BackColor = System.Drawing.SystemColors.Control;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnMouseEnterToolbox(object sender, System.EventArgs e) {
			//Occurs when the mouse enters the visible part of the control
			try {
				//Auto-open if not pinned and toolbar is closed; disable auto-hide if on
				if(this.lblPin.Text==TABTLB_AUTOHIDE_ON && this.pnlToolbox.Width==24) {
					this.pnlToolbox.Width = 192;
					this.splitterVR.Visible = true;
				}
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
				if(this.lblToolbox.BackColor==SystemColors.Control && this.lblPin.Text==TABTLB_AUTOHIDE_ON) {
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
				bool pinned = (this.lblPin.Text==TABTLB_AUTOHIDE_OFF);
				this.lblPin.Text = pinned ? TABTLB_AUTOHIDE_ON : TABTLB_AUTOHIDE_OFF;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnAutoHideToolbox(object sender, System.EventArgs e) {
			//Toolbox timer event handler
			try {
				//Auto-close timer
				this.tmrAutoHide.Stop();
				this.tmrAutoHide.Enabled = false;
				this.pnlToolbox.Width = 24;
				this.splitterVR.Visible = false;
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
		#endregion
		#region Status Log Services: LogEvent(), OnCloseLog(), OnEnterStatusLog(), OnLeaveStatusLog()
		private void LogEvent(string message) {
			//Append a message to the message window
			try {
				this.txtLog.AppendText(DateTime.Now.ToString("MM-dd-yyyy HH:mm:ss") + "\t" + message);
				this.txtLog.Focus();
				this.txtLog.ScrollToCaret();
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnCloseLog(object sender, System.EventArgs e) {
			//Event handler to close log windows
			this.msViewStatusLog.PerformClick();
		}
		private void OnEnterStatusLog(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			this.lblLog.BackColor = this.lblCloseLog.BackColor = SystemColors.ActiveCaption;
			this.lblLog.ForeColor = this.lblCloseLog.ForeColor = SystemColors.ActiveCaptionText;
		}
		private void OnLeaveStatusLog(object sender, System.EventArgs e) {
			//Event handler for enter and leave events
			this.lblLog.BackColor = this.lblCloseLog.BackColor = SystemColors.Control;
			this.lblLog.ForeColor = this.lblCloseLog.ForeColor = SystemColors.ControlText;
		}
		#endregion
		#region User Services: OnItemClicked()
		private void OnItemClicked(object sender, System.EventArgs e) {
			//Menu item clicked-apply selected service
			winLabel win=null;
			try  {
                ToolStripItem item = (ToolStripItem)sender;
				switch(item.Name)  {
					case "msFileNew": 
                    case "tsNew": 
						TsortNode oNode = (TsortNode)this.trvStores.SelectedNode;
						oNode.New();
						break;
					case "msFileOpen": 
                    case "tsOpen": 
						this.Cursor = Cursors.WaitCursor;
						try {
							Form frm=null;
							LabelTemplateNode node = (LabelTemplateNode)this.trvStores.SelectedNode;
							for(int i=0; i< this.MdiChildren.Length; i++) {
								if(this.MdiChildren[i].Text.IndexOf(node.FullPath) > 0) {
									frm = this.MdiChildren[i];
									break;
								}
							}
							if(frm == null) {
								this.mMessageMgr.AddMessage("Loading " + node.Text + " configuration...");
								win = new winLabel(node, this.mLabelMaker);
                                win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
								win.WindowState = this.MdiChildren.Length > 0 ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized;
                                win.MdiParent = this;
                                win.Show();
							}
							else
								frm.Activate();
						}
						catch(InvalidCastException) { }
						break;
					case "msFileSave": 
                    case "tsSave": 
						//Save to application specified file
						win = (winLabel)this.ActiveMdiChild;
						win.SaveLabelTemplate();
						break;
					case "msFileSaveAs": 
						//Save to user specified file
						win = (winLabel)this.ActiveMdiChild;
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.Filter = "Label Template Text File (*.txt; *.xml) | *.txt; *.xml";
						dlgSave.FilterIndex = 0;
						dlgSave.Title = "Save Label Template As...";
						dlgSave.FileName = "";
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) 
							win.ExportLabelTemplate(dlgSave.FileName);
						break;
					case "msFileOpenPort":
						try {
							this.ctlPrinters.Printer.TurnOn();
                            OnRS232DCEChanged(null,null);
							refreshPrinterStatus();
						}
						catch(System.IO.FileNotFoundException ex) { throw new Exception(ex.FileName); }
						break;
					case "msFileClosePort":
						if(this.ctlPrinters.Printer.On) {
							this.ctlPrinters.Printer.TurnOff();
                            OnRS232DCEChanged(null,null);
							refreshPrinterStatus();
						}
						break;
					case "msFileSettings":	break;
					case "msFilePrint": 
                    case "tsPrint":
                        break;
                    case "msFilePrintLabel": 
                    case "tsPrintLabel": 
						if(this.ActiveMdiChild != null) {
							win = (winLabel)this.ActiveMdiChild;
							win.PrintLabel((ILabelPrinter)this.ctlPrinters.Printer);
						}
						break;
					case "msFileExit":      this.Close(); break;
					case "msEditUndo": 
                    case "tsUndo": 
                        break;
					case "msEditRedo":				
                    case "tsRedo": 
                        break;
					case "msEditCut": 
                    case "tsCut": 
                        win = (winLabel)this.ActiveMdiChild; win.Cut(); 
                        break;
					case "msEditCopy": 
                    case "tsCopy": 
                        win = (winLabel)this.ActiveMdiChild; win.Copy(); 
                        break;
					case "msEditPaste": 
                    case "tsPaste": 
                        win = (winLabel)this.ActiveMdiChild; win.Paste(); 
                        break;
					case "msEditFind":	 
                    case "tsFind": 
                        break;
                    case "msViewRefresh": 
                    case "tsRefresh": 
                    case "csNavRefresh":
                        if(this.ActiveMdiChild != null) {
                            win = (winLabel)this.ActiveMdiChild;
                            win.RefreshLabelTemplate();
                        }
                        break;
					case "msViewLabelMaker":	    
                        this.pnlLabelMaker.Visible = (this.msViewLabelMaker.Checked = !this.msViewLabelMaker.Checked); 
                        break;
					case "msViewSerialPort": 
						this.msViewSerialPort.Checked = (!this.msViewSerialPort.Checked);
						if(this.msViewSerialPort.Checked) this.tabToolbox.TabPages.Add(this.tabPort); else this.tabToolbox.TabPages.Remove(this.tabPort);
						break;
					case "msViewRS232": 
						this.msViewRS232.Checked = (!this.msViewRS232.Checked);
						if(this.msViewRS232.Checked) this.tabToolbox.TabPages.Add(this.tabRS232); else this.tabToolbox.TabPages.Remove(this.tabRS232);
						break;
					case "msViewZebraPrinter": 
						this.msViewZebraPrinter.Checked = (!this.msViewZebraPrinter.Checked);
						if(this.msViewZebraPrinter.Checked) this.tabToolbox.TabPages.Add(this.tabPrinter); else this.tabToolbox.TabPages.Remove(this.tabPrinter);
						break;
                    case "msViewStatusLog":     this.pnlStatusLog.Visible = (this.msViewStatusLog.Checked = !this.msViewStatusLog.Checked); break;
                    case "msViewToolbar":       this.tsMain.Visible = (this.msViewToolbar.Checked = !this.msViewToolbar.Checked); break;
                    case "msViewStatusBar":     this.stbMain.Visible = (this.msViewStatusBar.Checked = !this.msViewStatusBar.Checked); break;
					case "msZebraHostStatus":	refreshPrinterStatus(); break;
					case "msWindowsCascade":	this.LayoutMdi(MdiLayout.Cascade); break;
					case "msWindowsTileH":		this.LayoutMdi(MdiLayout.TileHorizontal); break;
					case "msWindowsTileV":		this.LayoutMdi(MdiLayout.TileVertical); break;
					case "msHelpAbout":         new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
					case "csLogClear":			this.txtLog.Clear(); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: setUserServices()
		private void setUserServices() {
			//Set user services depending upon an item selected in the grid            
			try {
                TsortNode node = this.trvStores.SelectedNode != null ? (TsortNode)this.trvStores.SelectedNode : null;
                winLabel win = this.ActiveMdiChild != null ? (winLabel)this.ActiveMdiChild : null;
                
                this.msFileNew.Enabled = this.tsNew.Enabled = (node != null && node.CanNew);
				this.msFileOpen.Enabled = this.tsOpen.Enabled = this.csNavOpen.Enabled = (node != null && node.CanOpen);
                this.msFileSave.Enabled = this.tsSave.Enabled = (win != null && win.CanSave);
				this.msFileSaveAs.Enabled = win != null;
				this.msFileOpenPort.Enabled = !this.ctlPrinters.Printer.On;
                this.msFileClosePort.Enabled = this.ctlPrinters.Printer != null && this.ctlPrinters.Printer.On;
				this.msFileSettings.Enabled = false;
                this.msFilePrint.Enabled = this.tsPrint.Enabled = false;
                this.msFilePrintLabel.Enabled = this.tsPrintLabel.Enabled = (this.ctlPrinters.Printer != null && this.ctlPrinters.Printer.On) && win != null;
                this.csNavProperties.Enabled = (node != null && node.HasProperties);
				this.msFileExit.Enabled = true;
                this.msEditUndo.Enabled = this.tsUndo.Enabled = this.msEditRedo.Enabled = this.tsRedo.Enabled = false;
                this.msEditCut.Enabled = this.tsCut.Enabled = (win != null && win.CanCut);
                this.msEditCopy.Enabled = this.tsCopy.Enabled = (win != null && win.CanCopy);
                this.msEditPaste.Enabled = this.tsPaste.Enabled = (win != null && win.CanPaste);
                this.msEditFind.Enabled = this.tsFind.Enabled = false;
				this.msViewLabelMaker.Enabled = true;
				this.msViewSerialPort.Enabled = this.msViewRS232.Enabled = this.msViewZebraPrinter.Enabled = true;
				this.msViewStatusLog.Enabled = true;
				this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
				this.csNavRefresh.Enabled = win != null;
                this.msWindowsCascade.Enabled = this.msWindowsTileH.Enabled = this.msWindowsTileV.Enabled = true;
				this.msHelpAbout.Enabled = true;
				
				this.chkDTR.Enabled = this.chkRTS.Enabled = this.ctlPrinters.Printer.On;
				this.cboHandshaking.Enabled = this.ctlPrinters.Printer.On;
				
				this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(this.ctlPrinters.Printer.On, this.ctlPrinters.Printer.Settings.ToString()));
				this.stbMain.SetTerminalPanel(this.ctlPrinters.Printer.Type, "");
				
				this.csLogClear.Enabled = this.txtLog.TextLength>0;
			}
			catch(Exception ex) { App.ReportError(ex, false); }
		}
		#endregion
	}
}
