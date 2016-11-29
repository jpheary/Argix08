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
using Argix;
using Argix.Data;
using Argix.Net;
using Argix.Windows;

namespace Argix.Freight {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private StationOperator mOperator=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMsgMgr=null;

        #region Controls
        //Required designer variable
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label _lblVendor;
        private System.Windows.Forms.ListBox lstScannedBooks;
        private System.Windows.Forms.GroupBox grpCarton;
		private System.Windows.Forms.TextBox txtBookCount;
		private System.Windows.Forms.GroupBox grpBooks;
		private System.Windows.Forms.Label _lblBookScan;
		private System.Windows.Forms.TextBox txtBookScan;
        private System.Windows.Forms.Label _lblScannedBooks;
        private System.Windows.Forms.Label _lblBookCount;
		private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.Label _lblCartonNumber;
        private System.Windows.Forms.TextBox txtCartonNumber;
        private ComboBox cboVendor;
        private PictureBox picDamage;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnSave;
        private ShipperDS mShipperDS;
        private ToolStripButton btnExport;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnSend;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileExport;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileSendExport;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuEditDamaged;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripSeparator mnuToolsSep1;
        private ToolStripMenuItem mnuToolsImportVendors;
        private ToolStripMenuItem mnuToolsConvert;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        #endregion

        //Interface
		public frmMain() {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Set window docking
				this.pnlMain.Dock = DockStyle.Fill;
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
				this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.pnlMain,this.tsMain,this.msMain, this.stbMain });
				#endregion
				
				//Create data and UI services
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMsgMgr = new MessageManager(this.stbMain.StatusPanel, 500, 3000);
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this._lblVendor = new System.Windows.Forms.Label();
            this.lstScannedBooks = new System.Windows.Forms.ListBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.grpBooks = new System.Windows.Forms.GroupBox();
            this.picDamage = new System.Windows.Forms.PictureBox();
            this._lblScannedBooks = new System.Windows.Forms.Label();
            this._lblBookScan = new System.Windows.Forms.Label();
            this.txtBookScan = new System.Windows.Forms.TextBox();
            this.grpCarton = new System.Windows.Forms.GroupBox();
            this.cboVendor = new System.Windows.Forms.ComboBox();
            this.mShipperDS = new Argix.ShipperDS();
            this._lblCartonNumber = new System.Windows.Forms.Label();
            this.txtCartonNumber = new System.Windows.Forms.TextBox();
            this.txtBookCount = new System.Windows.Forms.TextBox();
            this._lblBookCount = new System.Windows.Forms.Label();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSend = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSendExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDamaged = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuToolsImportVendors = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConvert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain.SuspendLayout();
            this.grpBooks.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDamage)).BeginInit();
            this.grpCarton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mShipperDS)).BeginInit();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblVendor
            // 
            this._lblVendor.Location = new System.Drawing.Point(3, 33);
            this._lblVendor.Name = "_lblVendor";
            this._lblVendor.Size = new System.Drawing.Size(120, 24);
            this._lblVendor.TabIndex = 1;
            this._lblVendor.Text = "Vendor:";
            this._lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstScannedBooks
            // 
            this.lstScannedBooks.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lstScannedBooks.ItemHeight = 18;
            this.lstScannedBooks.Location = new System.Drawing.Point(129, 66);
            this.lstScannedBooks.Name = "lstScannedBooks";
            this.lstScannedBooks.ScrollAlwaysVisible = true;
            this.lstScannedBooks.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.lstScannedBooks.Size = new System.Drawing.Size(288, 58);
            this.lstScannedBooks.TabIndex = 1;
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.grpBooks);
            this.pnlMain.Controls.Add(this.grpCarton);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 65);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(592, 301);
            this.pnlMain.TabIndex = 15;
            // 
            // grpBooks
            // 
            this.grpBooks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpBooks.Controls.Add(this.picDamage);
            this.grpBooks.Controls.Add(this._lblScannedBooks);
            this.grpBooks.Controls.Add(this._lblBookScan);
            this.grpBooks.Controls.Add(this.txtBookScan);
            this.grpBooks.Controls.Add(this.lstScannedBooks);
            this.grpBooks.Location = new System.Drawing.Point(6, 153);
            this.grpBooks.Name = "grpBooks";
            this.grpBooks.Size = new System.Drawing.Size(580, 142);
            this.grpBooks.TabIndex = 14;
            this.grpBooks.TabStop = false;
            this.grpBooks.Text = "Books";
            // 
            // picDamage
            // 
            this.picDamage.Image = global::Argix.Properties.Resources.Flag_red;
            this.picDamage.Location = new System.Drawing.Point(444, 28);
            this.picDamage.Name = "picDamage";
            this.picDamage.Size = new System.Drawing.Size(24, 24);
            this.picDamage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDamage.TabIndex = 16;
            this.picDamage.TabStop = false;
            this.picDamage.Visible = false;
            // 
            // _lblScannedBooks
            // 
            this._lblScannedBooks.Location = new System.Drawing.Point(3, 66);
            this._lblScannedBooks.Name = "_lblScannedBooks";
            this._lblScannedBooks.Size = new System.Drawing.Size(120, 24);
            this._lblScannedBooks.TabIndex = 15;
            this._lblScannedBooks.Text = "Scanned:";
            this._lblScannedBooks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblBookScan
            // 
            this._lblBookScan.Location = new System.Drawing.Point(3, 27);
            this._lblBookScan.Name = "_lblBookScan";
            this._lblBookScan.Size = new System.Drawing.Size(120, 24);
            this._lblBookScan.TabIndex = 14;
            this._lblBookScan.Text = "Current:";
            this._lblBookScan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBookScan
            // 
            this.txtBookScan.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookScan.Location = new System.Drawing.Point(129, 27);
            this.txtBookScan.Name = "txtBookScan";
            this.txtBookScan.Size = new System.Drawing.Size(288, 27);
            this.txtBookScan.TabIndex = 0;
            this.txtBookScan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnBookScanKeyUp);
            // 
            // grpCarton
            // 
            this.grpCarton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCarton.Controls.Add(this.cboVendor);
            this.grpCarton.Controls.Add(this._lblCartonNumber);
            this.grpCarton.Controls.Add(this.txtCartonNumber);
            this.grpCarton.Controls.Add(this.txtBookCount);
            this.grpCarton.Controls.Add(this._lblBookCount);
            this.grpCarton.Controls.Add(this._lblVendor);
            this.grpCarton.Location = new System.Drawing.Point(6, 6);
            this.grpCarton.Name = "grpCarton";
            this.grpCarton.Size = new System.Drawing.Size(580, 144);
            this.grpCarton.TabIndex = 13;
            this.grpCarton.TabStop = false;
            this.grpCarton.Text = "Carton";
            // 
            // cboVendor
            // 
            this.cboVendor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboVendor.DataSource = this.mShipperDS;
            this.cboVendor.DisplayMember = "VendorTable.DESCRIPTION";
            this.cboVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVendor.FormattingEnabled = true;
            this.cboVendor.Location = new System.Drawing.Point(129, 33);
            this.cboVendor.Name = "cboVendor";
            this.cboVendor.Size = new System.Drawing.Size(384, 26);
            this.cboVendor.TabIndex = 0;
            this.cboVendor.ValueMember = "VendorTable.NUMBER";
            this.cboVendor.SelectionChangeCommitted += new System.EventHandler(this.OnVendorChanged);
            // 
            // mShipperDS
            // 
            this.mShipperDS.DataSetName = "ShipperDS";
            this.mShipperDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblCartonNumber
            // 
            this._lblCartonNumber.Location = new System.Drawing.Point(3, 69);
            this._lblCartonNumber.Name = "_lblCartonNumber";
            this._lblCartonNumber.Size = new System.Drawing.Size(120, 24);
            this._lblCartonNumber.TabIndex = 17;
            this._lblCartonNumber.Text = "Carton#:";
            this._lblCartonNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCartonNumber
            // 
            this.txtCartonNumber.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCartonNumber.Location = new System.Drawing.Point(129, 69);
            this.txtCartonNumber.Name = "txtCartonNumber";
            this.txtCartonNumber.Size = new System.Drawing.Size(288, 27);
            this.txtCartonNumber.TabIndex = 1;
            this.txtCartonNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnCartonNumberKeyUp);
            this.txtCartonNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnCartonNumberKeyPress);
            // 
            // txtBookCount
            // 
            this.txtBookCount.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBookCount.Location = new System.Drawing.Point(129, 105);
            this.txtBookCount.Name = "txtBookCount";
            this.txtBookCount.Size = new System.Drawing.Size(48, 27);
            this.txtBookCount.TabIndex = 2;
            this.txtBookCount.Text = "0";
            this.txtBookCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtBookCount.WordWrap = false;
            this.txtBookCount.Leave += new System.EventHandler(this.OnBookCountLeave);
            this.txtBookCount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnBookCountKeyUp);
            this.txtBookCount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnBookCountKeyPress);
            // 
            // _lblBookCount
            // 
            this._lblBookCount.Location = new System.Drawing.Point(3, 105);
            this._lblBookCount.Name = "_lblBookCount";
            this._lblBookCount.Size = new System.Drawing.Size(120, 24);
            this._lblBookCount.TabIndex = 14;
            this._lblBookCount.Text = "# of Books:";
            this._lblBookCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 366);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(592, 32);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 16;
            this.stbMain.TerminalText = "Terminal";
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnSep1,
            this.btnSave,
            this.btnExport,
            this.btnSep2,
            this.btnSend});
            this.tsMain.Location = new System.Drawing.Point(0, 26);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(592, 39);
            this.tsMain.TabIndex = 18;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.Document;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(36, 36);
            this.btnNew.ToolTipText = "New (reset) sample";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep1
            // 
            this.btnSep1.AutoSize = false;
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(15, 39);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 36);
            this.btnSave.ToolTipText = "Save sample...";
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(36, 36);
            this.btnExport.ToolTipText = "Export sample data...";
            this.btnExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.AutoSize = false;
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(15, 39);
            // 
            // btnSend
            // 
            this.btnSend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSend.Image = global::Argix.Properties.Resources.Send;
            this.btnSend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(36, 36);
            this.btnSend.ToolTipText = "Send sample data (ftp)...";
            this.btnSend.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msMain
            // 
            this.msMain.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(592, 26);
            this.msMain.TabIndex = 19;
            this.msMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileSep1,
            this.mnuFileSave,
            this.mnuFileExport,
            this.mnuFileSep2,
            this.mnuFileSendExport,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(49, 22);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.Document;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuFileNew.Size = new System.Drawing.Size(306, 22);
            this.mnuFileNew.Text = "&New (Reset) Sample";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(303, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(306, 22);
            this.mnuFileSave.Text = "&Save Sample...";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(306, 22);
            this.mnuFileExport.Text = "&Export Sample Data...";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(303, 6);
            // 
            // mnuFileSendExport
            // 
            this.mnuFileSendExport.Image = global::Argix.Properties.Resources.Send;
            this.mnuFileSendExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSendExport.Name = "mnuFileSendExport";
            this.mnuFileSendExport.Size = new System.Drawing.Size(306, 22);
            this.mnuFileSendExport.Text = "Send Sample Data (FTP)...";
            this.mnuFileSendExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(303, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(306, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditDamaged});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(52, 22);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditDamaged
            // 
            this.mnuEditDamaged.Image = global::Argix.Properties.Resources.Flag_red;
            this.mnuEditDamaged.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditDamaged.Name = "mnuEditDamaged";
            this.mnuEditDamaged.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.mnuEditDamaged.Size = new System.Drawing.Size(215, 22);
            this.mnuEditDamaged.Text = "&Damaged";
            this.mnuEditDamaged.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(59, 22);
            this.mnuView.Text = "&View";
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(156, 22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(156, 22);
            this.mnuViewStatusBar.Text = "&StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig,
            this.mnuToolsSep1,
            this.mnuToolsImportVendors,
            this.mnuToolsConvert});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(64, 22);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(300, 22);
            this.mnuToolsConfig.Text = "&Configuration";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuToolsSep1
            // 
            this.mnuToolsSep1.Name = "mnuToolsSep1";
            this.mnuToolsSep1.Size = new System.Drawing.Size(297, 6);
            // 
            // mnuToolsImportVendors
            // 
            this.mnuToolsImportVendors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuToolsImportVendors.Name = "mnuToolsImportVendors";
            this.mnuToolsImportVendors.Size = new System.Drawing.Size(300, 22);
            this.mnuToolsImportVendors.Text = "&Manage Vendors...";
            this.mnuToolsImportVendors.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuToolsConvert
            // 
            this.mnuToolsConvert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuToolsConvert.Name = "mnuToolsConvert";
            this.mnuToolsConvert.Size = new System.Drawing.Size(300, 22);
            this.mnuToolsConvert.Text = "&EAN/ISBN Converter Tool...";
            this.mnuToolsConvert.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(57, 22);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(312, 22);
            this.mnuHelpAbout.Text = "&About Statistical Sampling...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(9, 20);
            this.ClientSize = new System.Drawing.Size(592, 398);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Direct Statistical Sampling";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.pnlMain.ResumeLayout(false);
            this.grpBooks.ResumeLayout(false);
            this.grpBooks.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDamage)).EndInit();
            this.grpCarton.ResumeLayout(false);
            this.grpCarton.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mShipperDS)).EndInit();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
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
                    this.mnuViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                }
                catch(Exception ex) { App.ReportError(ex,true); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				this.mToolTip.SetToolTip(this.txtCartonNumber, "Enter a vendor carton number if not provided.");
                this.mToolTip.SetToolTip(this.txtBookCount, "Enter the expected book count (1 - " + global::Argix.Properties.Settings.Default.BookCountMax.ToString() + ")");
				this.mToolTip.SetToolTip(this.txtBookScan, "Scan or enter an EAN or ISBN book number.");
				#endregion
				
				//Set control defaults
				this.mMsgMgr.AddMessage("Loading application...");
                this.stbMain.SetTerminalPanel(Environment.UserName, Environment.MachineName);
                //this.txtBookCount.MaxLength = Carton.BookCountMax.ToString().Length;
                this.txtBookScan.MaxLength = global::Argix.Properties.Settings.Default.EANScanSize;
                this.txtCartonNumber.MaxLength = 32;
				this.txtCartonNumber.Enabled = false;
                loadVendors();
                this.mnuFileNew.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, true); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            if (!e.Cancel) {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnVendorChanged(object sender, EventArgs e) {
            //Event handler fo change in vendor
            try {
                //Enter the vendor/carton number
                if(this.cboVendor.SelectedValue != null && this.txtCartonNumber.Text.Length > 0)
                    this.mOperator.ValidateCartonNumber(this.txtCartonNumber.Text,this.cboVendor.SelectedValue.ToString());
            }
            catch(Exception ex) { App.ReportError(ex,false); }
        }        
        private void OnCartonNumberKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            //Event handler for book count textbox
            try {
                e.Handled = true;
                if(e.KeyChar == 8 || (e.KeyChar >= 48 && e.KeyChar <= 57))
                    e.Handled = false;
            }
            catch(Exception ex) { App.ReportError(ex,false); }
        }
        private void OnCartonNumberKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for carton number textbox
            try {
                //Enter the carton number if the user hits the 'Enter' key
                if(e.KeyCode == Keys.Enter && this.cboVendor.SelectedValue != null) {
                    if (this.txtCartonNumber.Text.Length == global::Argix.Properties.Settings.Default.CartonNumberLength)
                        this.mOperator.ValidateCartonNumber(this.txtCartonNumber.Text,this.cboVendor.SelectedValue.ToString());
                    else
                        throw new ApplicationException("Invalid carton scan length (" + this.txtCartonNumber.Text.Length.ToString() + "); must be " + global::Argix.Properties.Settings.Default.CartonNumberLength.ToString() + " charcters.");
                }
            }
            catch(WorkflowException ex) { this.txtCartonNumber.Clear(); App.ReportError(ex,true); }
            catch(Exception ex) { App.ReportError(ex,true); }
        }
        private void OnBookCountKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
			//Event handler for book count textbox
			try {
				e.Handled = true;
				if(e.KeyChar == 8 || (e.KeyChar >= 48 && e.KeyChar <= 57))
					e.Handled = false;
			}
			catch(Exception ex) { App.ReportError(ex, false); }
		}
		private void OnBookCountKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for book count textbox
			try {
				//Enter the book count if the user hits the 'Enter' key
                if(e.KeyCode == Keys.Enter && this.txtBookCount.Text.Length > 0) {
                    if(this.txtBookCount.Text.Length < 4)
                        this.mOperator.EnterBookCount(Convert.ToInt32(this.txtBookCount.Text));
                    else
                        throw new ApplicationException("Invalid book count (" + this.txtBookCount.Text + "); must be <= 999.");
                }
			}
			catch(Exception ex) { App.ReportError(ex, true); }
		}
		private void OnBookCountLeave(object sender, System.EventArgs e) {
			//Event handler for book count textbox lost focus
			try {
				//Enter the book count if the user leaves the control
				if(this.txtBookCount.Text.Length > 0) 
					this.mOperator.EnterBookCount(Convert.ToInt32(this.txtBookCount.Text));
			}
			catch(Exception ex) { App.ReportError(ex, true); }
		}
        private void OnBookScanKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for book keyed in
        	this.Cursor = Cursors.WaitCursor;
			try {
                //Enter the carton number if the user hits the 'Enter' key
                if(e.KeyCode == Keys.Enter && this.txtBookScan.Text.Length > 0) {
                    //Validate scan length
                    if (this.txtBookScan.Text.Length == global::Argix.Properties.Settings.Default.ISBNScanSize && this.txtBookScan.Text.Substring(0, 3) != global::Argix.Properties.Settings.Default.EANPrefix) {
                        //Process ISBN new book
                        this.mMsgMgr.AddMessage("Processing ISBN book scan...");
                        this.mOperator.ProcessBook(this.txtBookScan.Text,this.mnuEditDamaged.Checked);
                    }
                    else if (this.txtBookScan.Text.Length == global::Argix.Properties.Settings.Default.UPCScanSize) {
                        //Process UPC new book
                        this.mMsgMgr.AddMessage("Processing UPC book scan...");
                        this.mOperator.ProcessBook(this.txtBookScan.Text,this.mnuEditDamaged.Checked);
                    }
                    else if (this.txtBookScan.Text.Length == global::Argix.Properties.Settings.Default.EANScanSize && this.txtBookScan.Text.Substring(0, 3) == global::Argix.Properties.Settings.Default.EANPrefix) {
                        //Process EAN new book
                        this.mMsgMgr.AddMessage("Processing EAN book scan...");
                        this.mOperator.ProcessBook(this.txtBookScan.Text,this.mnuEditDamaged.Checked);
                    }
                    else {
                        throw new ApplicationException("Invalid book scan length (" + this.txtBookScan.Text.Length.ToString() + ")");
                    }
                }
			}
			catch(Exception ex) { this.txtBookScan.Clear(); App.ReportError(ex, true); }
			finally { this.Cursor = Cursors.Default; }
}
        private void OnBookScanChanged(object sender,System.EventArgs e) {
			//Event handler for change in scanned value
            //Code moved to OnBookScanKeyUp()- now handled by Enter from keyboard or programmed scanner
        }
        #region Workflow: OnResetCarton(), OnCartonNumberRequired(), OnCartonNumberValidated(), OnBooksCounted(), OnBookScanned(), OnValidBookSample(), OnInvalidBookSample(), OnCartonSaved()
        private void OnResetCarton(object sender, EventArgs e) {
			//Event handler for station operator resset carton event
			try {
				//Reset the workflow
				this.mMsgMgr.AddMessage("Resetting for a new carton sample...");
                this.cboVendor.Enabled = true;
                this.txtCartonNumber.Text = "";
                this.txtCartonNumber.Enabled = this.cboVendor.SelectedValue != null;
                this.txtBookCount.Text = "0";
				this.txtBookCount.Enabled = false;
				this.txtBookScan.Text = "";
				this.txtBookScan.Enabled = false;
				this.lstScannedBooks.Items.Clear();
                if(this.mnuEditDamaged.Checked) this.mnuEditDamaged.PerformClick();
                this.cboVendor.Focus();
            }
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { setUserServices(); }
		}
		private void OnCartonNumberRequired(object sender, EventArgs e) {
			//Event handler for station operator carton number required event
			try {
                //Next activity: setup for entering carton number
				this.mMsgMgr.AddMessage("Please enter a carton number...");
                this.txtCartonNumber.Enabled = this.cboVendor.SelectedValue != null;
                this.txtCartonNumber.Focus();
			}
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { setUserServices(); }
		}
		private void OnCartonNumberValidated(object sender, EventArgs e) {
			//Event handler for station operator carton validated event
			try {
                //Next activity: setup for entering expected book count
				this.mMsgMgr.AddMessage("Carton was validated for sampling...");
                this.cboVendor.Enabled = this.txtCartonNumber.Enabled = false;
				this.txtBookCount.Enabled = true;
				this.txtBookScan.Enabled = false;
				this.txtBookCount.Focus();
			}
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { setUserServices(); }
		}
		private void OnBooksCounted(object sender, EventArgs e) {
			//Event handler for station operator books counted event
			try {
                //Next activity: setup for book scanning
				this.mMsgMgr.AddMessage("Book count accepted; begin scanning books...");
				this.txtBookCount.Enabled = false;
				this.txtBookScan.Enabled = true;
				this.txtBookScan.Focus();
			}
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { setUserServices(); }
		}
		private void OnBookScanned(object sender, EventArgs e) {
			//Event handler for station operator books scanned event
			try {
				//Clear damage state; update display of last 2 processed books
				this.mMsgMgr.AddMessage("Book scan accepted; updating history list...");
                if(this.mnuEditDamaged.Checked) this.mnuEditDamaged.PerformClick();
                //if(this.lstScannedBooks.Items.Count > 1) this.lstScannedBooks.Items.RemoveAt(0);
                //this.lstScannedBooks.Items.Add(this.txtBookScan.Text);
                this.lstScannedBooks.Items.Insert(0, this.txtBookScan.Text);
			}
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { this.txtBookScan.Clear(); setUserServices(); }
		}
		private void OnValidBookSample(object sender, EventArgs e) {
			//Event handler for station operator books validated event
			try {
				this.mMsgMgr.AddMessage("Carton sample validated. Saving sample results...");
				this.mOperator.SaveCartonSample();
			}
            catch(Exception ex) { App.ReportError(ex,true); }
			finally { setUserServices(); }
		}
		private void OnInvalidBookSample(object sender, EventArgs e) {
			//Event handler for station operator books validated event
			try {
				this.mMsgMgr.AddMessage("Carton sample failed validation. Resetting books...");
				this.txtBookScan.Clear();
				this.lstScannedBooks.Items.Clear();
				this.txtBookScan.Focus();
			}
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { setUserServices(); }
		}
		private void OnCartonSaved(object sender, EventArgs e) {
			//Event handler for station operator carton saved event
			try {
				MessageBox.Show(this, "Carton sample was succesfully saved.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				this.Focus();
				this.mOperator.Reset();
            }
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { setUserServices(); }
		}
		#endregion
        #region User Services: OnItemClick(), OnDataStatusUpdate()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
                this.Enabled = false;
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "mnuFileNew":			
                    case "btnNew":
						this.Cursor = Cursors.WaitCursor;
						this.mOperator.Reset(); 
						break;
					case "mnuFileSave":			
					case "btnSave":
						this.Cursor = Cursors.WaitCursor;
						this.mOperator.ValidateCartonSample();
                        break;
                    case "mnuFileExport":
                    case "btnExport":
                        new dlgSamples().ShowDialog();
                        break;
                    case "mnuFileSendExport":
                    case "btnSend":
                        //Send an export file to an ftp server
                        this.mMsgMgr.AddMessage("Send export file...");
                        OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.InitialDirectory = Environment.CurrentDirectory;
                        dlgOpen.Filter = "Export file (*.txt)|*.txt";
                        dlgOpen.FilterIndex = 1;
                        dlgOpen.RestoreDirectory = true;
                        dlgOpen.Title = "Sampling Export File";
                        if(dlgOpen.ShowDialog() == DialogResult.OK && dlgOpen.FileName.Length > 0) {
                            this.Cursor = Cursors.WaitCursor;
                            if(sendExportFile(dlgOpen.FileName))
                                MessageBox.Show(this,"Send complete.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                            else
                                MessageBox.Show(this,"Send failed.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        break;
                    case "mnuFileExit":             this.Close(); break;
                    case "mnuEditDamaged":          this.picDamage.Visible = (this.mnuEditDamaged.Checked = (!this.mnuEditDamaged.Checked)); break;
                    case "mnuViewToolbar":          this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case "mnuViewStatusBar":	    this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuToolsConvert":         new dlgConvert().ShowDialog(); break;
                    case "mnuToolsImportVendors":   new dlgVendors().ShowDialog(); loadVendors(); break;
                    case "mnuToolsConfig":          App.ShowConfig(); this.mnuFileNew.PerformClick(); break;
                    case "mnuHelpAbout":            new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, true); }
			finally { this.Enabled = true; this.Cursor = Cursors.Default; setUserServices(); }
		}
        private void OnDataStatusUpdate(object sender, DataStatusArgs e) {
            //Event handler for notifications from mediator
            this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
        }
        #endregion
        #region Local Services: configApplication(), setUserServices(), loadVendors(), sendExportFile()
        private void configApplication() {
			try {				
				//Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
				this.mOperator = new StationOperator();
				this.mOperator.CartonNumberValidated += new EventHandler(OnCartonNumberValidated);
				this.mOperator.CartonNumberRequired += new EventHandler(OnCartonNumberRequired);
				this.mOperator.BooksCounted += new EventHandler(OnBooksCounted);
				this.mOperator.BookScanned += new EventHandler(OnBookScanned);
				this.mOperator.ValidBookSample += new EventHandler(OnValidBookSample);
				this.mOperator.InvalidBookSample += new EventHandler(OnInvalidBookSample);
				this.mOperator.CartonSaved += new EventHandler(OnCartonSaved);
				this.mOperator.ResetCarton += new EventHandler(OnResetCarton);
			}
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services depending upon an item selected in the grid
			try {				
				//Set main menu and context menu states
				this.mnuFileNew.Enabled = this.btnNew.Enabled = true;
				this.mnuFileSave.Enabled = this.btnSave.Enabled = this.mOperator.CanSaveCarton;
                this.mnuFileExport.Enabled = this.btnExport.Enabled = true;
                this.mnuFileSendExport.Enabled = this.btnSend.Enabled = true;
				this.mnuFileExit.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsImportVendors.Enabled = this.mnuToolsConvert.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.User2Panel.Text = this.lstScannedBooks.Items.Count.ToString();
                this.stbMain.User2Panel.ToolTipText = "# of books scanned";
            }
			catch(Exception ex) { App.ReportError(ex, false); }
			finally { Application.DoEvents(); }
		}
        private void loadVendors() {
            //Load vendors
            this.mShipperDS.Clear();
            ShipperDS ds = new ShipperDS();
            ds.Merge(App.Mediator.FillDataset(App.USP_VENDOR_GETLIST,App.TBL_VENDOR,null));
            this.mShipperDS.Merge(ds.VendorTable.Select("STATUS='A'"));
            if(this.cboVendor.Items.Count > 0) this.cboVendor.SelectedIndex = 0;
        }
        private bool sendExportFile(string exportFile) {
            //Execute this service
            FtpClient ftpClient = null;
            bool ret = false;
            try {
                //Perform file operations in source folder
                ftpClient = new FtpClient(global::Argix.Properties.Settings.Default.FTPServername.Trim(),
                                          global::Argix.Properties.Settings.Default.FTPUsername.Trim(),
                                          global::Argix.Properties.Settings.Default.FTPPassword.Trim());
                ftpClient.RemotePath = global::Argix.Properties.Settings.Default.FTPRemotePath.Trim();
                //ftpClient = new FtpClient("tpjheary2", "anonymous", "jheary@argixdirect.com");
                //ftpClient.RemotePath = global::Argix.Properties.Settings.Default.FTPRemotePath.Trim();
                ftpClient.Login();
                ftpClient.Upload(exportFile);
                ret = true;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while sending export file to " + "." + " on " + "localhost" + " ftp server.",ex); }
            finally { ftpClient.Close(); }
            return ret;
        }
        #endregion
    }
}
