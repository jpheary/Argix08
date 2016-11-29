using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Argix.Windows;
using Argix.Windows.Printers;

namespace Argix.Terminals {
	//Application main window
	public class winMain : System.Windows.Forms.Form {
		//Members
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private BrotherPT2300 mPrinter=null;
		private NameValueCollection mHelpItems=null;
		
		#region Controls
        private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.Button btnPinInputs;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Label _lblInputsDriver;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label _lblInputs;
        private System.Windows.Forms.TreeView trvNav;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileSettings;
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
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelp;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripMenuItem mnuViewDevices;
        private ToolStripMenuItem mnuViewBatteries;
        private ToolStripMenuItem mnuToolsBatteryReport;
        private ToolStripMenuItem mnuToolsBatteryAssignReport;
        private ToolStripSeparator mnuToolsSep1;
        private ToolStripMenuItem mnuWindows;
        private ToolStripMenuItem mnuWinCascade;
        private ToolStripMenuItem mnuWinTileH;
        private ToolStripMenuItem mnuWinTileV;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnSave;
        private ToolStripButton btnExport;
        private ToolStripButton btnPrint;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnSearch;
        private ToolStripSeparator btnSep4;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnViewDevices;
        private ToolStripButton btnViewBatteries;
        private ToolStripButton btnViewTypes;
        private ToolStripPanel tspMain;
        private ToolStripMenuItem mnuFileExport;
        private ToolStripMenuItem mnuDevices;
        private ToolStripMenuItem mnuDevicesCreate;
        private ToolStripMenuItem mnuDevicesUpdate;
        private ToolStripSeparator mnudevicesSep1;
        private ToolStripMenuItem mnuDevicesAssign;
        private ToolStripMenuItem mnuDevicesUnassign;
        private ToolStripMenuItem mnuBatteries;
        private ToolStripMenuItem mnuTypes;
        private ToolStripMenuItem mnuTypesCreate;
        private ToolStripMenuItem mnuTypesUpdate;
        private ToolStripMenuItem mnuBatteriesCreate;
        private ToolStripMenuItem mnuBatteriesUpdate;
        private ToolStripSeparator mnuBatteriesSep1;
        private ToolStripMenuItem mnuBatteriesStartCharge;
        private ToolStripMenuItem mnuBatteriesEndCharge;
        private ToolStripSeparator mnuBatteriesSep2;
        private ToolStripMenuItem mnuBatteriesAssignments;
        private ToolStripMenuItem mnuFilePrintLabel;
        private ToolStrip tsDevices;
        private ToolStripButton btnDevicesCreate;
        private ToolStripButton btnDevicesUpdate;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripButton btnDevicesAssign;
        private ToolStripButton btnDevicesUnassign;
        private ToolStrip tsBatteries;
        private ToolStripButton btnBatteriesCreate;
        private ToolStripButton btnBatteriesUpdate;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripButton btnBatteriesStartCharge;
        private ToolStripButton btnBatteriesEndCharge;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton btnBatteriesAssignments;
        private ToolStrip tsTypes;
        private ToolStripButton btnTypesCreate;
        private ToolStripButton btnTypesUpdate;
        private ToolStripMenuItem mnuViewTypes;

		#endregion
		
		//Interface
		public winMain() {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
                this.Text = "Argix Direct " + App.Product;
                buildHelpMenu();
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);
                #region Window docking
                this.msMain.Dock = DockStyle.Top;
                this.tspMain.Dock = DockStyle.Top;
                this.stbMain.Dock = DockStyle.Bottom;
				this.trvNav.Dock = DockStyle.Left;
				this.Controls.AddRange(new Control[]{this.trvNav, this.tspMain, this.msMain, this.stbMain});
				#endregion
				
				//Create services
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 1500, 3000);
				configApplication();
			}
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose(bool disposing) { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winMain));
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this._lblInputsDriver = new System.Windows.Forms.Label();
            this.btnPinInputs = new System.Windows.Forms.Button();
            this._lblInputs = new System.Windows.Forms.Label();
            this.trvNav = new System.Windows.Forms.TreeView();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintLabel = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuViewDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewBatteries = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevices = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnudevicesSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDevicesAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDevicesUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatteries = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatteriesCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatteriesUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatteriesSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuBatteriesStartCharge = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatteriesEndCharge = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBatteriesSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuBatteriesAssignments = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTypesCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTypesUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsBatteryReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsBatteryAssignReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinTileH = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWinTileV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnViewDevices = new System.Windows.Forms.ToolStripButton();
            this.btnViewBatteries = new System.Windows.Forms.ToolStripButton();
            this.btnViewTypes = new System.Windows.Forms.ToolStripButton();
            this.tspMain = new System.Windows.Forms.ToolStripPanel();
            this.tsDevices = new System.Windows.Forms.ToolStrip();
            this.btnDevicesCreate = new System.Windows.Forms.ToolStripButton();
            this.btnDevicesUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnDevicesAssign = new System.Windows.Forms.ToolStripButton();
            this.btnDevicesUnassign = new System.Windows.Forms.ToolStripButton();
            this.tsBatteries = new System.Windows.Forms.ToolStrip();
            this.btnBatteriesCreate = new System.Windows.Forms.ToolStripButton();
            this.btnBatteriesUpdate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBatteriesStartCharge = new System.Windows.Forms.ToolStripButton();
            this.btnBatteriesEndCharge = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnBatteriesAssignments = new System.Windows.Forms.ToolStripButton();
            this.tsTypes = new System.Windows.Forms.ToolStrip();
            this.btnTypesCreate = new System.Windows.Forms.ToolStripButton();
            this.btnTypesUpdate = new System.Windows.Forms.ToolStripButton();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tspMain.SuspendLayout();
            this.tsDevices.SuspendLayout();
            this.tsBatteries.SuspendLayout();
            this.tsTypes.SuspendLayout();
            this.SuspendLayout();
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,209);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(574,24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 11;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Navy;
            this.btnCancel.Location = new System.Drawing.Point(72,256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64,23);
            this.btnCancel.TabIndex = 153;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.ForeColor = System.Drawing.Color.Navy;
            this.btnOK.Location = new System.Drawing.Point(8,256);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64,23);
            this.btnOK.TabIndex = 152;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // _lblInputsDriver
            // 
            this._lblInputsDriver.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblInputsDriver.ForeColor = System.Drawing.Color.Navy;
            this._lblInputsDriver.Location = new System.Drawing.Point(8,32);
            this._lblInputsDriver.Name = "_lblInputsDriver";
            this._lblInputsDriver.Size = new System.Drawing.Size(54,18);
            this._lblInputsDriver.TabIndex = 109;
            this._lblInputsDriver.Text = "Driver";
            this._lblInputsDriver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnPinInputs
            // 
            this.btnPinInputs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPinInputs.BackColor = System.Drawing.SystemColors.Control;
            this.btnPinInputs.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPinInputs.Font = new System.Drawing.Font("Arial",9.75F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnPinInputs.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPinInputs.Location = new System.Drawing.Point(138,0);
            this.btnPinInputs.Name = "btnPinInputs";
            this.btnPinInputs.Size = new System.Drawing.Size(18,18);
            this.btnPinInputs.TabIndex = 18;
            this.btnPinInputs.UseVisualStyleBackColor = false;
            // 
            // _lblInputs
            // 
            this._lblInputs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._lblInputs.BackColor = System.Drawing.SystemColors.Control;
            this._lblInputs.ForeColor = System.Drawing.Color.Navy;
            this._lblInputs.Location = new System.Drawing.Point(0,0);
            this._lblInputs.Name = "_lblInputs";
            this._lblInputs.Size = new System.Drawing.Size(156,18);
            this._lblInputs.TabIndex = 17;
            this._lblInputs.Text = "Inputs";
            this._lblInputs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trvNav
            // 
            this.trvNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.trvNav.Location = new System.Drawing.Point(0,74);
            this.trvNav.Name = "trvNav";
            this.trvNav.Size = new System.Drawing.Size(121,135);
            this.trvNav.TabIndex = 88;
            this.trvNav.Visible = false;
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuDevices,
            this.mnuBatteries,
            this.mnuTypes,
            this.mnuTools,
            this.mnuWindows,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0,0);
            this.msMain.MdiWindowListItem = this.mnuWindows;
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(574,24);
            this.msMain.TabIndex = 90;
            this.msMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileExport,
            this.mnuFileSep2,
            this.mnuFileSettings,
            this.mnuFilePrint,
            this.mnuFilePrintLabel,
            this.mnuFilePreview,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37,20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.Document;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(154,22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(154,22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(151,6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(154,22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(154,22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(154,22);
            this.mnuFileExport.Text = "Export";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(151,6);
            // 
            // mnuFileSettings
            // 
            this.mnuFileSettings.Image = global::Argix.Properties.Resources.PageSetup;
            this.mnuFileSettings.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuFileSettings.Name = "mnuFileSettings";
            this.mnuFileSettings.Size = new System.Drawing.Size(154,22);
            this.mnuFileSettings.Text = "Page Settings...";
            this.mnuFileSettings.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(154,22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFilePrintLabel
            // 
            this.mnuFilePrintLabel.Image = global::Argix.Properties.Resources.BarCode;
            this.mnuFilePrintLabel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrintLabel.Name = "mnuFilePrintLabel";
            this.mnuFilePrintLabel.Size = new System.Drawing.Size(154,22);
            this.mnuFilePrintLabel.Text = "Print Label";
            this.mnuFilePrintLabel.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(154,22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(151,6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(154,22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClicked);
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
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(109,22);
            this.mnuEditCut.Text = "Cut";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(109,22);
            this.mnuEditCopy.Text = "Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(109,22);
            this.mnuEditPaste.Text = "Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(106,6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Search;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.Size = new System.Drawing.Size(109,22);
            this.mnuEditSearch.Text = "Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewDevices,
            this.mnuViewBatteries,
            this.mnuViewTypes,
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44,20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewDevices
            // 
            this.mnuViewDevices.Image = global::Argix.Properties.Resources.devices;
            this.mnuViewDevices.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuViewDevices.Name = "mnuViewDevices";
            this.mnuViewDevices.Size = new System.Drawing.Size(172,22);
            this.mnuViewDevices.Text = "Devices";
            this.mnuViewDevices.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewBatteries
            // 
            this.mnuViewBatteries.Image = global::Argix.Properties.Resources.batteries;
            this.mnuViewBatteries.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuViewBatteries.Name = "mnuViewBatteries";
            this.mnuViewBatteries.Size = new System.Drawing.Size(172,22);
            this.mnuViewBatteries.Text = "Batteries";
            this.mnuViewBatteries.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewTypes
            // 
            this.mnuViewTypes.Image = global::Argix.Properties.Resources.types;
            this.mnuViewTypes.Name = "mnuViewTypes";
            this.mnuViewTypes.Size = new System.Drawing.Size(172,22);
            this.mnuViewTypes.Text = "Component Types";
            this.mnuViewTypes.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(172,22);
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(169,6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(172,22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(172,22);
            this.mnuViewStatusBar.Text = "StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuDevices
            // 
            this.mnuDevices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDevicesCreate,
            this.mnuDevicesUpdate,
            this.mnudevicesSep1,
            this.mnuDevicesAssign,
            this.mnuDevicesUnassign});
            this.mnuDevices.Name = "mnuDevices";
            this.mnuDevices.Size = new System.Drawing.Size(59,20);
            this.mnuDevices.Text = "&Devices";
            // 
            // mnuDevicesCreate
            // 
            this.mnuDevicesCreate.Image = global::Argix.Properties.Resources.Document;
            this.mnuDevicesCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDevicesCreate.Name = "mnuDevicesCreate";
            this.mnuDevicesCreate.Size = new System.Drawing.Size(185,22);
            this.mnuDevicesCreate.Text = "New...";
            this.mnuDevicesCreate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuDevicesUpdate
            // 
            this.mnuDevicesUpdate.Image = global::Argix.Properties.Resources.Open;
            this.mnuDevicesUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDevicesUpdate.Name = "mnuDevicesUpdate";
            this.mnuDevicesUpdate.Size = new System.Drawing.Size(185,22);
            this.mnuDevicesUpdate.Text = "Update...";
            this.mnuDevicesUpdate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnudevicesSep1
            // 
            this.mnudevicesSep1.Name = "mnudevicesSep1";
            this.mnudevicesSep1.Size = new System.Drawing.Size(182,6);
            // 
            // mnuDevicesAssign
            // 
            this.mnuDevicesAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.mnuDevicesAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDevicesAssign.Name = "mnuDevicesAssign";
            this.mnuDevicesAssign.Size = new System.Drawing.Size(185,22);
            this.mnuDevicesAssign.Text = "Assign to Driver...";
            this.mnuDevicesAssign.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuDevicesUnassign
            // 
            this.mnuDevicesUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.mnuDevicesUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuDevicesUnassign.Name = "mnuDevicesUnassign";
            this.mnuDevicesUnassign.Size = new System.Drawing.Size(185,22);
            this.mnuDevicesUnassign.Text = "Unassign from Driver";
            this.mnuDevicesUnassign.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuBatteries
            // 
            this.mnuBatteries.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBatteriesCreate,
            this.mnuBatteriesUpdate,
            this.mnuBatteriesSep1,
            this.mnuBatteriesStartCharge,
            this.mnuBatteriesEndCharge,
            this.mnuBatteriesSep2,
            this.mnuBatteriesAssignments});
            this.mnuBatteries.Name = "mnuBatteries";
            this.mnuBatteries.Size = new System.Drawing.Size(64,20);
            this.mnuBatteries.Text = "&Batteries";
            // 
            // mnuBatteriesCreate
            // 
            this.mnuBatteriesCreate.Image = global::Argix.Properties.Resources.Document;
            this.mnuBatteriesCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuBatteriesCreate.Name = "mnuBatteriesCreate";
            this.mnuBatteriesCreate.Size = new System.Drawing.Size(195,22);
            this.mnuBatteriesCreate.Text = "New...";
            this.mnuBatteriesCreate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuBatteriesUpdate
            // 
            this.mnuBatteriesUpdate.Image = global::Argix.Properties.Resources.Open;
            this.mnuBatteriesUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuBatteriesUpdate.Name = "mnuBatteriesUpdate";
            this.mnuBatteriesUpdate.Size = new System.Drawing.Size(195,22);
            this.mnuBatteriesUpdate.Text = "Update...";
            this.mnuBatteriesUpdate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuBatteriesSep1
            // 
            this.mnuBatteriesSep1.Name = "mnuBatteriesSep1";
            this.mnuBatteriesSep1.Size = new System.Drawing.Size(192,6);
            // 
            // mnuBatteriesStartCharge
            // 
            this.mnuBatteriesStartCharge.Image = global::Argix.Properties.Resources.Play;
            this.mnuBatteriesStartCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuBatteriesStartCharge.Name = "mnuBatteriesStartCharge";
            this.mnuBatteriesStartCharge.Size = new System.Drawing.Size(195,22);
            this.mnuBatteriesStartCharge.Text = "Start Charge Cycle";
            this.mnuBatteriesStartCharge.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuBatteriesEndCharge
            // 
            this.mnuBatteriesEndCharge.Image = global::Argix.Properties.Resources.Stop;
            this.mnuBatteriesEndCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuBatteriesEndCharge.Name = "mnuBatteriesEndCharge";
            this.mnuBatteriesEndCharge.Size = new System.Drawing.Size(195,22);
            this.mnuBatteriesEndCharge.Text = "End Charge Cycle";
            this.mnuBatteriesEndCharge.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuBatteriesSep2
            // 
            this.mnuBatteriesSep2.Name = "mnuBatteriesSep2";
            this.mnuBatteriesSep2.Size = new System.Drawing.Size(192,6);
            // 
            // mnuBatteriesAssignments
            // 
            this.mnuBatteriesAssignments.Image = global::Argix.Properties.Resources.Relationships;
            this.mnuBatteriesAssignments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuBatteriesAssignments.Name = "mnuBatteriesAssignments";
            this.mnuBatteriesAssignments.Size = new System.Drawing.Size(195,22);
            this.mnuBatteriesAssignments.Text = "Change Assignments...";
            this.mnuBatteriesAssignments.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuTypes
            // 
            this.mnuTypes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTypesCreate,
            this.mnuTypesUpdate});
            this.mnuTypes.Name = "mnuTypes";
            this.mnuTypes.Size = new System.Drawing.Size(117,20);
            this.mnuTypes.Text = "&Component Types";
            // 
            // mnuTypesCreate
            // 
            this.mnuTypesCreate.Image = global::Argix.Properties.Resources.Document;
            this.mnuTypesCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuTypesCreate.Name = "mnuTypesCreate";
            this.mnuTypesCreate.Size = new System.Drawing.Size(121,22);
            this.mnuTypesCreate.Text = "New...";
            this.mnuTypesCreate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuTypesUpdate
            // 
            this.mnuTypesUpdate.Image = global::Argix.Properties.Resources.Open;
            this.mnuTypesUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuTypesUpdate.Name = "mnuTypesUpdate";
            this.mnuTypesUpdate.Size = new System.Drawing.Size(121,22);
            this.mnuTypesUpdate.Text = "Update...";
            this.mnuTypesUpdate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsBatteryReport,
            this.mnuToolsBatteryAssignReport,
            this.mnuToolsSep1,
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48,20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsBatteryReport
            // 
            this.mnuToolsBatteryReport.Name = "mnuToolsBatteryReport";
            this.mnuToolsBatteryReport.Size = new System.Drawing.Size(215,22);
            this.mnuToolsBatteryReport.Text = "Battery Report";
            this.mnuToolsBatteryReport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuToolsBatteryAssignReport
            // 
            this.mnuToolsBatteryAssignReport.Name = "mnuToolsBatteryAssignReport";
            this.mnuToolsBatteryAssignReport.Size = new System.Drawing.Size(215,22);
            this.mnuToolsBatteryAssignReport.Text = "Battery Assignment Report";
            this.mnuToolsBatteryAssignReport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuToolsSep1
            // 
            this.mnuToolsSep1.Name = "mnuToolsSep1";
            this.mnuToolsSep1.Size = new System.Drawing.Size(212,6);
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(215,22);
            this.mnuToolsConfig.Text = "Configuration";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuWindows
            // 
            this.mnuWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuWinCascade,
            this.mnuWinTileH,
            this.mnuWinTileV});
            this.mnuWindows.Name = "mnuWindows";
            this.mnuWindows.Size = new System.Drawing.Size(63,20);
            this.mnuWindows.Text = "&Window";
            // 
            // mnuWinCascade
            // 
            this.mnuWinCascade.Name = "mnuWinCascade";
            this.mnuWinCascade.Size = new System.Drawing.Size(160,22);
            this.mnuWinCascade.Text = "Cascade";
            this.mnuWinCascade.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuWinTileH
            // 
            this.mnuWinTileH.Name = "mnuWinTileH";
            this.mnuWinTileH.Size = new System.Drawing.Size(160,22);
            this.mnuWinTileH.Text = "Tile Horizontally";
            this.mnuWinTileH.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuWinTileV
            // 
            this.mnuWinTileV.Name = "mnuWinTileV";
            this.mnuWinTileV.Size = new System.Drawing.Size(160,22);
            this.mnuWinTileV.Text = "Tile Vertically";
            this.mnuWinTileV.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44,20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(199,22);
            this.mnuHelpAbout.Text = "&About Mobile Devices...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(196,6);
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.toolStripSeparator1,
            this.btnSave,
            this.btnExport,
            this.btnPrint,
            this.toolStripSeparator2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnSep3,
            this.btnSearch,
            this.btnSep4,
            this.btnRefresh,
            this.btnViewDevices,
            this.btnViewBatteries,
            this.btnViewTypes});
            this.tsMain.Location = new System.Drawing.Point(3,0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(335,25);
            this.tsMain.TabIndex = 91;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.Document;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23,22);
            this.btnNew.ToolTipText = "New";
            this.btnNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23,22);
            this.btnOpen.ToolTipText = "Open";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6,25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23,22);
            this.btnSave.ToolTipText = "Save";
            this.btnSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23,22);
            this.btnExport.ToolTipText = "Export";
            this.btnExport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23,22);
            this.btnPrint.ToolTipText = "Print";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6,25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = global::Argix.Properties.Resources.Cut;
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23,22);
            this.btnCut.ToolTipText = "Cut";
            this.btnCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::Argix.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23,22);
            this.btnCopy.ToolTipText = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::Argix.Properties.Resources.Paste;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23,22);
            this.btnPaste.ToolTipText = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6,25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Search;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23,22);
            this.btnSearch.ToolTipText = "Search";
            this.btnSearch.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnSep4
            // 
            this.btnSep4.Name = "btnSep4";
            this.btnSep4.Size = new System.Drawing.Size(6,25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23,22);
            this.btnRefresh.ToolTipText = "Refresh the current view";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnViewDevices
            // 
            this.btnViewDevices.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewDevices.Image = global::Argix.Properties.Resources.devices;
            this.btnViewDevices.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnViewDevices.Name = "btnViewDevices";
            this.btnViewDevices.Size = new System.Drawing.Size(23,22);
            this.btnViewDevices.ToolTipText = "View mobile devices";
            this.btnViewDevices.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnViewBatteries
            // 
            this.btnViewBatteries.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewBatteries.Image = global::Argix.Properties.Resources.batteries;
            this.btnViewBatteries.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnViewBatteries.Name = "btnViewBatteries";
            this.btnViewBatteries.Size = new System.Drawing.Size(23,22);
            this.btnViewBatteries.ToolTipText = "View batteries and driver battery assignments";
            this.btnViewBatteries.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnViewTypes
            // 
            this.btnViewTypes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnViewTypes.Image = global::Argix.Properties.Resources.types;
            this.btnViewTypes.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnViewTypes.Name = "btnViewTypes";
            this.btnViewTypes.Size = new System.Drawing.Size(23,22);
            this.btnViewTypes.ToolTipText = "View component types";
            this.btnViewTypes.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tspMain
            // 
            this.tspMain.Controls.Add(this.tsMain);
            this.tspMain.Controls.Add(this.tsDevices);
            this.tspMain.Controls.Add(this.tsBatteries);
            this.tspMain.Controls.Add(this.tsTypes);
            this.tspMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tspMain.Location = new System.Drawing.Point(0,24);
            this.tspMain.Name = "tspMain";
            this.tspMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.tspMain.RowMargin = new System.Windows.Forms.Padding(3,0,0,0);
            this.tspMain.Size = new System.Drawing.Size(574,50);
            // 
            // tsDevices
            // 
            this.tsDevices.Dock = System.Windows.Forms.DockStyle.None;
            this.tsDevices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDevicesCreate,
            this.btnDevicesUpdate,
            this.toolStripSeparator5,
            this.btnDevicesAssign,
            this.btnDevicesUnassign});
            this.tsDevices.Location = new System.Drawing.Point(3,25);
            this.tsDevices.Name = "tsDevices";
            this.tsDevices.Size = new System.Drawing.Size(110,25);
            this.tsDevices.TabIndex = 92;
            // 
            // btnDevicesCreate
            // 
            this.btnDevicesCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDevicesCreate.Image = global::Argix.Properties.Resources.Document;
            this.btnDevicesCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDevicesCreate.Name = "btnDevicesCreate";
            this.btnDevicesCreate.Size = new System.Drawing.Size(23,22);
            this.btnDevicesCreate.ToolTipText = "Create a new device";
            this.btnDevicesCreate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnDevicesUpdate
            // 
            this.btnDevicesUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDevicesUpdate.Image = global::Argix.Properties.Resources.Open;
            this.btnDevicesUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDevicesUpdate.Name = "btnDevicesUpdate";
            this.btnDevicesUpdate.Size = new System.Drawing.Size(23,22);
            this.btnDevicesUpdate.ToolTipText = "Update the selected device";
            this.btnDevicesUpdate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6,25);
            // 
            // btnDevicesAssign
            // 
            this.btnDevicesAssign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDevicesAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.btnDevicesAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDevicesAssign.Name = "btnDevicesAssign";
            this.btnDevicesAssign.Size = new System.Drawing.Size(23,22);
            this.btnDevicesAssign.ToolTipText = "Assign the selected device to a driver";
            this.btnDevicesAssign.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnDevicesUnassign
            // 
            this.btnDevicesUnassign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDevicesUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.btnDevicesUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDevicesUnassign.Name = "btnDevicesUnassign";
            this.btnDevicesUnassign.Size = new System.Drawing.Size(23,22);
            this.btnDevicesUnassign.ToolTipText = "Unassign the selected device from its\' driver";
            this.btnDevicesUnassign.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsBatteries
            // 
            this.tsBatteries.Dock = System.Windows.Forms.DockStyle.None;
            this.tsBatteries.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnBatteriesCreate,
            this.btnBatteriesUpdate,
            this.toolStripSeparator3,
            this.btnBatteriesStartCharge,
            this.btnBatteriesEndCharge,
            this.toolStripSeparator4,
            this.btnBatteriesAssignments});
            this.tsBatteries.Location = new System.Drawing.Point(147,25);
            this.tsBatteries.Name = "tsBatteries";
            this.tsBatteries.Size = new System.Drawing.Size(139,25);
            this.tsBatteries.TabIndex = 93;
            // 
            // btnBatteriesCreate
            // 
            this.btnBatteriesCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBatteriesCreate.Image = global::Argix.Properties.Resources.Document;
            this.btnBatteriesCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatteriesCreate.Name = "btnBatteriesCreate";
            this.btnBatteriesCreate.Size = new System.Drawing.Size(23,22);
            this.btnBatteriesCreate.ToolTipText = "Create a new battery";
            this.btnBatteriesCreate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnBatteriesUpdate
            // 
            this.btnBatteriesUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBatteriesUpdate.Image = global::Argix.Properties.Resources.Open;
            this.btnBatteriesUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatteriesUpdate.Name = "btnBatteriesUpdate";
            this.btnBatteriesUpdate.Size = new System.Drawing.Size(23,22);
            this.btnBatteriesUpdate.ToolTipText = "Update the selected battery";
            this.btnBatteriesUpdate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6,25);
            // 
            // btnBatteriesStartCharge
            // 
            this.btnBatteriesStartCharge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBatteriesStartCharge.Image = global::Argix.Properties.Resources.Play;
            this.btnBatteriesStartCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatteriesStartCharge.Name = "btnBatteriesStartCharge";
            this.btnBatteriesStartCharge.Size = new System.Drawing.Size(23,22);
            this.btnBatteriesStartCharge.ToolTipText = "Start a charge cycle for the selected battery";
            this.btnBatteriesStartCharge.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnBatteriesEndCharge
            // 
            this.btnBatteriesEndCharge.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBatteriesEndCharge.Image = global::Argix.Properties.Resources.Stop;
            this.btnBatteriesEndCharge.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatteriesEndCharge.Name = "btnBatteriesEndCharge";
            this.btnBatteriesEndCharge.Size = new System.Drawing.Size(23,22);
            this.btnBatteriesEndCharge.ToolTipText = "Stop the charge cycle for the selected battery";
            this.btnBatteriesEndCharge.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6,25);
            // 
            // btnBatteriesAssignments
            // 
            this.btnBatteriesAssignments.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBatteriesAssignments.Image = global::Argix.Properties.Resources.Relationships;
            this.btnBatteriesAssignments.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatteriesAssignments.Name = "btnBatteriesAssignments";
            this.btnBatteriesAssignments.Size = new System.Drawing.Size(23,22);
            this.btnBatteriesAssignments.ToolTipText = "Change battery assignments for the selected driver";
            this.btnBatteriesAssignments.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsTypes
            // 
            this.tsTypes.Dock = System.Windows.Forms.DockStyle.None;
            this.tsTypes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnTypesCreate,
            this.btnTypesUpdate});
            this.tsTypes.Location = new System.Drawing.Point(333,25);
            this.tsTypes.Name = "tsTypes";
            this.tsTypes.Size = new System.Drawing.Size(58,25);
            this.tsTypes.TabIndex = 94;
            // 
            // btnTypesCreate
            // 
            this.btnTypesCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTypesCreate.Image = global::Argix.Properties.Resources.Document;
            this.btnTypesCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTypesCreate.Name = "btnTypesCreate";
            this.btnTypesCreate.Size = new System.Drawing.Size(23,22);
            this.btnTypesCreate.ToolTipText = "Create a new component type";
            this.btnTypesCreate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnTypesUpdate
            // 
            this.btnTypesUpdate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnTypesUpdate.Image = global::Argix.Properties.Resources.Open;
            this.btnTypesUpdate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTypesUpdate.Name = "btnTypesUpdate";
            this.btnTypesUpdate.Size = new System.Drawing.Size(23,22);
            this.btnTypesUpdate.ToolTipText = "Update the selected component type";
            this.btnTypesUpdate.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // winMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(574,233);
            this.Controls.Add(this.trvNav);
            this.Controls.Add(this.tspMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "winMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Mobile Devices";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tspMain.ResumeLayout(false);
            this.tspMain.PerformLayout();
            this.tsDevices.ResumeLayout(false);
            this.tsDevices.PerformLayout();
            this.tsBatteries.ResumeLayout(false);
            this.tsBatteries.PerformLayout();
            this.tsTypes.ResumeLayout(false);
            this.tsTypes.PerformLayout();
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
                    this.mnuViewToolbar.Checked = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,Argix.Terminals.LogLevel.Error); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion

                Argix.Terminals.TerminalInfo t = MobileDevicesProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.stbMain.User1Panel.Width = 144;
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { OnUpdateServices(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Ask only if there are detail forms open
            if(!e.Cancel) {
                //Save settings
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnFormResize(object sender,System.EventArgs e) { }
        public void SetStatusMessage(string message) { this.mMessageMgr.AddMessage(message); }
        #region User Services: OnItemClicked(), OnHelpMenuClick()
        private void OnItemClicked(object sender,EventArgs e) {
            winDevices winD = this.ActiveMdiChild is winDevices ? (winDevices)this.ActiveMdiChild : null;
            winBatteries winB = this.ActiveMdiChild is winBatteries ? (winBatteries)this.ActiveMdiChild : null;
            winTypes winT = this.ActiveMdiChild is winTypes ? (winTypes)this.ActiveMdiChild : null;
            Form frm=null;
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "mnuFileNew": 
                    case "btnNew":
                        if(winD != null) winD.New();
                        else if(winB != null) winB.New();
                        break;
                    case "mnuFileOpen":
                    case "btnOpen":
                        if(winD != null) winD.Open();
                        else if(winB != null) winB.Open();
                        break;
                    case "mnuFileSave":
                    case "btnSave":
                        break;
                    case "mnuFileSaveAs":
                        break;
                    case "mnuFileExport":
                    case "btnExport":
                        if(winD != null) winD.Export();
                        else if(winB != null) winB.Export();
                        else if(winT != null) winT.Export();
                        break;
                    case "mnuFileSettings":
                        if(winD != null) winD.PageSettings();
                        else if(winB != null) winB.PageSettings();
                        else if(winT != null) winT.PageSettings();
                        break;
                    case "mnuFilePrint":
                        if(winD != null) winD.Print(true);
                        else if(winB != null) winB.Print(true);
                        else if(winT != null) winT.Print(true);
                        break;
                    case "btnPrint":
                        if(winD != null) winD.Print(false);
                        else if(winB != null) winB.Print(false);
                        else if(winT != null) winT.Print(false);
                        break;
                    case "mnuFilePreview":
                        if(winD != null) winD.PrintPreview();
                        else if(winB != null) winB.PrintPreview();
                        else if(winT != null) winT.PrintPreview();
                        break;
                    case "mnuFilePrintLabel":
                        if(winD != null) winD.PrintLabel();
                        else if(winB != null) winB.PrintLabel();
                        break;
                    case "mnuFileExit": this.Close(); break;
                    case "mnuViewDevices":
                    case "btnViewDevices":
                        for(int i=0;i< this.MdiChildren.Length;i++) {
                            if(this.MdiChildren[i].Text == "Mobile Device Items") { frm = this.MdiChildren[i]; break; }
                        }
                        if(frm == null) {
                            this.mMessageMgr.AddMessage("Loading mobile device items...");
                            winDevices win = new winDevices(this.mPrinter);
                            win.UpdateServices += new EventHandler(OnUpdateServices);
                            win.WindowState = (this.MdiChildren.Length > 0) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized;
                            win.MdiParent = this;
                            win.Show();
                        }
                        else
                            frm.Activate();
                        break;
                    case "mnuViewBatteries":
                    case "btnViewBatteries":
                        for(int i=0;i< this.MdiChildren.Length;i++) {
                            if(this.MdiChildren[i].Text == "Mobile Battery Items") { frm = this.MdiChildren[i]; break; }
                        }
                        if(frm == null) {
                            this.mMessageMgr.AddMessage("Loading mobile battery items...");
                            winBatteries win = new winBatteries(this.mPrinter);
                            win.UpdateServices += new EventHandler(OnUpdateServices);
                            win.WindowState = (this.MdiChildren.Length > 0) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized;
                            win.MdiParent = this;
                            win.Show();
                        }
                        else
                            frm.Activate();
                        break;
                    case "mnuViewTypes":
                    case "btnViewTypes":
                        for(int i=0;i< this.MdiChildren.Length;i++) {
                            if(this.MdiChildren[i].Text == "Component Types") { frm = this.MdiChildren[i]; break; }
                        }
                        if(frm == null) {
                            this.mMessageMgr.AddMessage("Loading component types...");
                            winTypes win = new winTypes();
                            win.UpdateServices += new EventHandler(OnUpdateServices);
                            win.WindowState = (this.MdiChildren.Length > 0) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized;
                            win.MdiParent = this;
                            win.Show();
                        }
                        else
                            frm.Activate();
                        break;
                    case "mnuViewToolbar": this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
                    case "mnuViewStatusBar": this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuViewRefresh":
                    case "btnRefresh":
                        if(winD != null) winD.Refresh2();
                        else if(winB != null) winB.Refresh2();
                        else if(winT != null) winT.Refresh2();
                        break;
                    case "mnuDevicesCreate":
                    case "btnDevicesCreate":
                        if(winD != null) winD.Create();
                        break;
                    case "mnuDevicesUpdate":
                    case "btnDevicesUpdate":
                        if(winD != null) winD.Edit(); 
                        break;
                    case "mnuDevicesAssign":
                    case "btnDevicesAssign":
                        if(winD != null) winD.Assign(); 
                        break;
                    case "mnuDevicesUnassign":
                    case "btnDevicesUnassign":
                        if(winD != null) winD.Unassign(); 
                        break;
                    case "mnuBatteriesCreate":
                    case "btnBatteriesCreate":
                        if(winB != null) winB.Create(); 
                        break;
                    case "mnuBatteriesUpdate":
                    case "btnBatteriesUpdate":
                        if(winB != null) winB.Edit(); 
                        break;
                    case "mnuBatteriesStartCharge":
                    case "btnBatteriesStartCharge":
                        if(winB != null) winB.StartCharge(); 
                        break;
                    case "mnuBatteriesEndCharge":
                    case "btnBatteriesEndCharge":
                        if(winB != null) winB.EndCharge(); 
                        break;
                    case "mnuBatteriesAssignments":
                    case "btnBatteriesAssignments":
                        if(winB != null) winB.ChangeAssignments(); 
                        break;
                    case "mnuTypesCreate":
                    case "btnTypesCreate":
                        if(winT != null) winT.Create(); 
                        break;
                    case "mnuTypesUpdate":
                    case "btnTypesUpdate":
                        if(winT != null) winT.Edit(); 
                        break;
                    case "mnuToolsBatteryReport":
                        frmBatteryReport rpt1 = new frmBatteryReport();
                        rpt1.WindowState = (this.MdiChildren.Length > 0) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized;
                        rpt1.MdiParent = this;
                        rpt1.Show();
                        break;
                    case "mnuToolsBatteryAssignReport":
                        frmBatteryAssignReport rpt2 = new frmBatteryAssignReport();
                        rpt2.WindowState = (this.MdiChildren.Length > 0) ? this.ActiveMdiChild.WindowState : FormWindowState.Maximized;
                        rpt2.MdiParent = this;
                        rpt2.Show();
                        break;
                    case "mnuToolsConfig": App.ShowConfig(); break;
                    case "mnuWinCascade": this.LayoutMdi(MdiLayout.Cascade); break;
                    case "mnuWinTileH": this.LayoutMdi(MdiLayout.TileHorizontal); break;
                    case "mnuWinTileV": this.LayoutMdi(MdiLayout.TileVertical); break;
                    case "mnuHelpAbout": new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { OnUpdateServices(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
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
        #region Local Services: configApplication(), OnUpdateServices(), buildHelpMenu()
        private void configApplication() {
			try {				
				//Create business objects with configuration values
                this.mPrinter = new BrotherPT2300(App.Config.PrinterName,App.Config.PrinterFontName,(float)App.Config.PrinterFontSize);
                switch(App.Config.Barcode128Subset.ToLower()) {
					case "a": this.mPrinter.Subset = Barcode128.A; break;
					case "b": this.mPrinter.Subset = Barcode128.B; break;
					case "c": this.mPrinter.Subset = Barcode128.C; break;
				}
			}
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void OnUpdateServices(object sender, EventArgs e) {
			//Set user services depending upon an item selected in the grid
			try {
				//Determine appointment cancelled status
                winDevices winD = this.ActiveMdiChild is winDevices ? (winDevices)this.ActiveMdiChild : null;
                winBatteries winB = this.ActiveMdiChild is winBatteries ? (winBatteries)this.ActiveMdiChild : null;
                winTypes winT = this.ActiveMdiChild is winTypes ? (winTypes)this.ActiveMdiChild : null;
                
                this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
                this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
                this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = this.btnSave.Enabled = false;
                this.mnuFileExport.Enabled = this.btnExport.Enabled = ((winD != null && winD.CanExport) || (winB != null && winB.CanExport) || (winT != null && winT.CanExport));
                this.mnuFileSettings.Enabled = true;
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = ((winD != null && winD.CanPrint) || (winB != null && winB.CanPrint) || (winT != null && winT.CanPrint));
                this.mnuFilePrintLabel.Enabled = ((winD != null && winD.CanPrintLabel) || (winB != null && winB.CanPrintLabel));
                this.mnuFilePreview.Enabled = true;
                this.mnuFileExit.Enabled = true;
                this.mnuEditCut.Enabled = this.btnCut.Enabled = false;
                this.mnuEditCopy.Enabled = this.btnCopy.Enabled = false;
                this.mnuEditPaste.Enabled = this.btnPaste.Enabled = false;
                this.mnuEditSearch.Enabled = this.btnSearch.Enabled = false;
                this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewDevices.Enabled = this.mnuViewBatteries.Enabled = this.mnuViewTypes.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuDevicesCreate.Enabled = this.btnDevicesCreate.Enabled = (winD != null && winD.CanCreate);
                this.mnuDevicesUpdate.Enabled = this.btnDevicesUpdate.Enabled = (winD != null && winD.CanEdit);
                this.mnuDevicesAssign.Enabled = this.btnDevicesAssign.Enabled = (winD != null && winD.CanAssign);
                this.mnuDevicesUnassign.Enabled = this.btnDevicesUnassign.Enabled = (winD != null && winD.CanUnassign);
                this.mnuBatteriesCreate.Enabled = this.btnBatteriesCreate.Enabled = (winB != null && winB.CanCreate);
                this.mnuBatteriesUpdate.Enabled = this.btnBatteriesUpdate.Enabled = (winB != null && winB.CanEdit);
                this.mnuBatteriesStartCharge.Enabled = this.btnBatteriesStartCharge.Enabled = (winB != null && winB.CanStartCharge);
                this.mnuBatteriesEndCharge.Enabled = this.btnBatteriesEndCharge.Enabled = (winB != null && winB.CanEndCharge);
                this.mnuBatteriesAssignments.Enabled = this.btnBatteriesAssignments.Enabled = (winB != null && winB.CanChangeAssignments);
                this.mnuTypesCreate.Enabled = this.btnTypesCreate.Enabled = (winT != null && winT.CanCreate);
                this.mnuTypesUpdate.Enabled = this.btnTypesUpdate.Enabled = (winT != null && winT.CanEdit);
                this.mnuToolsBatteryReport.Enabled = this.mnuToolsBatteryAssignReport.Enabled = winB != null;
				this.mnuWinCascade.Enabled = this.mnuWinTileH.Enabled = this.mnuWinTileV.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(MobileDevicesProxy.ServiceState,MobileDevicesProxy.ServiceAddress));
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
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
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
	}
}
