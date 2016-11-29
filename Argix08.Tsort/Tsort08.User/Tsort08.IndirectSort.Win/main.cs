using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Argix.Data;
using Argix.Enterprise;
using Argix.Windows;

namespace Argix.Freight {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private StationOperator mStationOperator=null;
		private MessageManager mMessageMgr=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private NameValueCollection mHelpItems=null;
		private Icon icon_idle=null, icon_on=null, icon_off=null;
		
		private const int TLB_SEARCH = -1;
		private const int TLB_TRIP_REFRESH = 0;
		private const int TLB_CARTON_DELETE = 2;
		#region Controls
		//Required designer variable
        private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ListBox lstScannedCartons;
		private System.Windows.Forms.Label lblTotalScanned;
		private System.Windows.Forms.TextBox txtLabelScan;
		private System.Windows.Forms.Label lblTripNumber;
		private System.Windows.Forms.Label lblCartons;
		private System.Windows.Forms.Label lblTrailer;
        private System.Windows.Forms.Label lblCarrier;
        private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.Label _lblTrip;
		private System.Windows.Forms.Label _lblCartons;
		private System.Windows.Forms.Label _lblCarrier;
		private System.Windows.Forms.Label _lblTrailer;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.Label _lblTotalScanned;
        private System.Windows.Forms.Label _lblBarcode;
        private ToolStrip tsMain;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnDelete;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuEditDelete;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripMenuItem mnuFile;
		#endregion
		public frmMain() {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Set window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.stbMain.Dock = DockStyle.Bottom;
				this.Controls.AddRange(new Control[]{this.tsMain, this.msMain, this.stbMain});
				#endregion
				
				//Create data and UI services
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 3000);
				this.icon_idle = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources._idle.ico"));
                this.icon_on = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources._on.ico"));
                this.icon_off = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources._off.ico"));
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
            this._lblTrip = new System.Windows.Forms.Label();
            this._lblCartons = new System.Windows.Forms.Label();
            this._lblCarrier = new System.Windows.Forms.Label();
            this._lblTrailer = new System.Windows.Forms.Label();
            this.lblTripNumber = new System.Windows.Forms.Label();
            this.lblCartons = new System.Windows.Forms.Label();
            this.lblCarrier = new System.Windows.Forms.Label();
            this.lblTrailer = new System.Windows.Forms.Label();
            this.txtLabelScan = new System.Windows.Forms.TextBox();
            this.lstScannedCartons = new System.Windows.Forms.ListBox();
            this._lblTotalScanned = new System.Windows.Forms.Label();
            this.lblTotalScanned = new System.Windows.Forms.Label();
            this._lblBarcode = new System.Windows.Forms.Label();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnDelete = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblTrip
            // 
            this._lblTrip.Location = new System.Drawing.Point(9,6);
            this._lblTrip.Name = "_lblTrip";
            this._lblTrip.Size = new System.Drawing.Size(96,24);
            this._lblTrip.TabIndex = 0;
            this._lblTrip.Text = "Trip #";
            // 
            // _lblCartons
            // 
            this._lblCartons.Location = new System.Drawing.Point(111,6);
            this._lblCartons.Name = "_lblCartons";
            this._lblCartons.Size = new System.Drawing.Size(96,24);
            this._lblCartons.TabIndex = 1;
            this._lblCartons.Text = "Cartons";
            // 
            // _lblCarrier
            // 
            this._lblCarrier.Location = new System.Drawing.Point(225,6);
            this._lblCarrier.Name = "_lblCarrier";
            this._lblCarrier.Size = new System.Drawing.Size(96,24);
            this._lblCarrier.TabIndex = 2;
            this._lblCarrier.Text = "Carrier";
            // 
            // _lblTrailer
            // 
            this._lblTrailer.Location = new System.Drawing.Point(327,6);
            this._lblTrailer.Name = "_lblTrailer";
            this._lblTrailer.Size = new System.Drawing.Size(96,24);
            this._lblTrailer.TabIndex = 3;
            this._lblTrailer.Text = "Trailer #";
            // 
            // lblTripNumber
            // 
            this.lblTripNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTripNumber.Location = new System.Drawing.Point(9,36);
            this.lblTripNumber.Name = "lblTripNumber";
            this.lblTripNumber.Size = new System.Drawing.Size(96,24);
            this.lblTripNumber.TabIndex = 4;
            // 
            // lblCartons
            // 
            this.lblCartons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCartons.Location = new System.Drawing.Point(111,36);
            this.lblCartons.Name = "lblCartons";
            this.lblCartons.Size = new System.Drawing.Size(96,24);
            this.lblCartons.TabIndex = 5;
            // 
            // lblCarrier
            // 
            this.lblCarrier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCarrier.Location = new System.Drawing.Point(225,36);
            this.lblCarrier.Name = "lblCarrier";
            this.lblCarrier.Size = new System.Drawing.Size(96,24);
            this.lblCarrier.TabIndex = 6;
            // 
            // lblTrailer
            // 
            this.lblTrailer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTrailer.Location = new System.Drawing.Point(327,36);
            this.lblTrailer.Name = "lblTrailer";
            this.lblTrailer.Size = new System.Drawing.Size(96,24);
            this.lblTrailer.TabIndex = 7;
            // 
            // txtLabelScan
            // 
            this.txtLabelScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtLabelScan.Font = new System.Drawing.Font("Microsoft Sans Serif",20.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtLabelScan.Location = new System.Drawing.Point(9,379);
            this.txtLabelScan.Name = "txtLabelScan";
            this.txtLabelScan.Size = new System.Drawing.Size(415,38);
            this.txtLabelScan.TabIndex = 8;
            this.txtLabelScan.TextChanged += new System.EventHandler(this.OnScanChanged);
            // 
            // lstScannedCartons
            // 
            this.lstScannedCartons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstScannedCartons.ItemHeight = 16;
            this.lstScannedCartons.Location = new System.Drawing.Point(9,72);
            this.lstScannedCartons.Name = "lstScannedCartons";
            this.lstScannedCartons.Size = new System.Drawing.Size(415,244);
            this.lstScannedCartons.TabIndex = 9;
            // 
            // _lblTotalScanned
            // 
            this._lblTotalScanned.Location = new System.Drawing.Point(438,72);
            this._lblTotalScanned.Name = "_lblTotalScanned";
            this._lblTotalScanned.Size = new System.Drawing.Size(96,48);
            this._lblTotalScanned.TabIndex = 10;
            this._lblTotalScanned.Text = "Total Scanned";
            this._lblTotalScanned.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalScanned
            // 
            this.lblTotalScanned.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotalScanned.Location = new System.Drawing.Point(438,126);
            this.lblTotalScanned.Name = "lblTotalScanned";
            this.lblTotalScanned.Size = new System.Drawing.Size(96,24);
            this.lblTotalScanned.TabIndex = 11;
            this.lblTotalScanned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblBarcode
            // 
            this._lblBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblBarcode.Location = new System.Drawing.Point(6,352);
            this._lblBarcode.Name = "_lblBarcode";
            this._lblBarcode.Size = new System.Drawing.Size(96,24);
            this._lblBarcode.TabIndex = 12;
            this._lblBarcode.Text = "Barcode";
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0,475);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(648,36);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 17;
            this.stbMain.TerminalText = "Station #";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblCarrier);
            this.pnlMain.Controls.Add(this.lblTrailer);
            this.pnlMain.Controls.Add(this._lblTrip);
            this.pnlMain.Controls.Add(this._lblCartons);
            this.pnlMain.Controls.Add(this._lblCarrier);
            this.pnlMain.Controls.Add(this._lblTrailer);
            this.pnlMain.Controls.Add(this.lblTripNumber);
            this.pnlMain.Controls.Add(this.lblCartons);
            this.pnlMain.Controls.Add(this.lstScannedCartons);
            this.pnlMain.Controls.Add(this.txtLabelScan);
            this.pnlMain.Controls.Add(this._lblTotalScanned);
            this.pnlMain.Controls.Add(this.lblTotalScanned);
            this.pnlMain.Controls.Add(this._lblBarcode);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Font = new System.Drawing.Font("Verdana",9.75F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.pnlMain.Location = new System.Drawing.Point(0,49);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(3);
            this.pnlMain.Size = new System.Drawing.Size(648,426);
            this.pnlMain.TabIndex = 15;
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh,
            this.btnDelete});
            this.tsMain.Location = new System.Drawing.Point(0,24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(648,25);
            this.tsMain.TabIndex = 13;
            this.tsMain.Text = "toolStrip1";
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23,22);
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnDelete
            // 
            this.btnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDelete.Image = global::Argix.Properties.Resources.Delete;
            this.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(23,22);
            this.btnDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0,0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(648,24);
            this.msMain.TabIndex = 18;
            this.msMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37,20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152,22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditDelete});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39,20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditDelete
            // 
            this.mnuEditDelete.Image = global::Argix.Properties.Resources.Delete;
            this.mnuEditDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditDelete.Name = "mnuEditDelete";
            this.mnuEditDelete.Size = new System.Drawing.Size(152,22);
            this.mnuEditDelete.Text = "&Delete...";
            this.mnuEditDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.toolStripMenuItem1,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44,20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(179,22);
            this.mnuViewRefresh.Text = "&Refresh Assignment";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(176,6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(179,22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(179,22);
            this.mnuViewStatusBar.Text = "&StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48,20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157,22);
            this.mnuToolsConfig.Text = "&Configuration...";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44,20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(183,22);
            this.mnuHelpAbout.Text = "&About Indirect Sort...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(9,20);
            this.ClientSize = new System.Drawing.Size(648,511);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana",12F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.Text = "Argix Direct Indirect Sort";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
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
                    switch (this.WindowState) {
                        case FormWindowState.Maximized: break;
                        case FormWindowState.Minimized: break;
                        case FormWindowState.Normal:
                            this.Location = global::Argix.Properties.Settings.Default.Location;
                            this.Size = global::Argix.Properties.Settings.Default.Size;
                            break;
                    }
                    this.mnuViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    App.CheckVersion();
                }
                catch (Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				//this.mToolTip.SetToolTip(this.txtSearch, "Search for a trip.");
				#endregion
								
				//Set control defaults
				this.txtLabelScan.MaxLength = StationOperator.ScanSize;
				this.txtLabelScan.Focus();
				OnPrinterChanged(this.mStationOperator.Station.Printer, EventArgs.Empty);
				this.mStationOperator.StartWork();
				this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { 
			//Event handler for form resized event
		}
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            if (!e.Cancel) {
                #region Save user preferences
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
                ArgixTrace.WriteLine(new TraceMessage(App.Version, App.Product, LogLevel.Information, "App Stopped"));
            }
        }
        private void OnAssignmentChanged(object sender, EventArgs e) {
			//Current station assignment changed
			try {
				if(this.mStationOperator.TripAssignment != null) {
					//Enable\disable carton scanning; and display trip details
					this.lblTripNumber.Text = this.mStationOperator.TripAssignment.Number;
					this.lblCartons.Text = this.mStationOperator.TripAssignment.CartonCount.ToString();
					this.lblCarrier.Text = this.mStationOperator.TripAssignment.Carrier;
					this.lblTrailer.Text = this.mStationOperator.TripAssignment.TrailerNumber;		
					this.lblTotalScanned.Text = this.mStationOperator.CartonsScanned.ToString();
                    ArgixTrace.WriteLine(new TraceMessage("Current assignment=" + this.mStationOperator.TripAssignment.Number,App.Product,LogLevel.Debug));
				}
				else {
					//Enable\disable carton scanning; and display trip details
					this.lblTripNumber.Text = "";
					this.lblCartons.Text = this.mStationOperator.isTripAssigned ? this.mStationOperator.TripAssignment.CartonCount.ToString() : "";
					this.lblCarrier.Text = "";
					this.lblTrailer.Text = "";		
					this.lblTotalScanned.Text = this.mStationOperator.CartonsScanned.ToString();
                    ArgixTrace.WriteLine(new TraceMessage("Current assignment=null",App.Product,LogLevel.Debug));
				}
			}
			catch (Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); }
		}
		private void OnScanChanged(object sender, System.EventArgs e) {
			//Event handler for change in scanned value
			if(this.txtLabelScan.Text.Length == this.txtLabelScan.MaxLength) {
				this.Cursor = Cursors.WaitCursor;
				try {
					//Process new carton
					this.mMessageMgr.AddMessage("Processing carton scan...");
					this.mStationOperator.ProcessCarton(this.txtLabelScan.Text);
				}
				catch(WorkflowException ex) {	App.ReportError(ex, true, LogLevel.None); }
				catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
				finally {
					this.txtLabelScan.Text = "";
					this.txtLabelScan.Focus();
					setUserServices();
					this.Cursor = Cursors.Default;
				}
			}
		}
		private void OnCartonCreated(object sender, CartonEventArgs e) {
			//Event handler for a created carton
			try {
				//Update display of last 10 processed cartons
				if(this.lstScannedCartons.Items.Count > 10)
					this.lstScannedCartons.Items.RemoveAt(0);
				this.lstScannedCartons.Items.Add(e.CartonScan);
				
				//Update display of total cartons scanned
				this.lblTotalScanned.Text = this.mStationOperator.CartonsScanned.ToString();
			} 
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
		}
		private void OnCartonDeleted(object sender, CartonEventArgs e) {
			//Event handler for a deleted carton
			int index=0;
			try {
				//Delete the carton if in the list
				if(this.lstScannedCartons.Items.Count > 0) {
					//Search for the deleted carton
					if((index = this.lstScannedCartons.Items.IndexOf(e.CartonScan)) > -1) 
						this.lstScannedCartons.Items.RemoveAt(index);
				}
				
				//Update display of total cartons scanned
				this.lblTotalScanned.Text = this.mStationOperator.CartonsScanned.ToString();
			} 
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
		}
		#region Devices: OnPrinterChanged()
		private void OnPrinterChanged(object sender, EventArgs e) {
			//Event handler for change to the active printer
			try { 
				if(this.mStationOperator.Station.Printer != null) {
					//Configure for a new printer type
					this.stbMain.Panels[1].Icon = this.mStationOperator.Station.Printer.On ? this.icon_on : this.icon_off;
					this.stbMain.Panels[1].ToolTipText = this.mStationOperator.Station.PrinterType + " (" + this.mStationOperator.Station.Printer.Settings.PortName + ")";
                    this.mMessageMgr.AddMessage("Label printer set. Printer type= " + this.mStationOperator.Station.PrinterType);
				}
				else {
					//Disable printer features
					this.stbMain.Panels[1].Icon = this.icon_idle;
					this.stbMain.Panels[1].ToolTipText = "Printer is not set.";
                    this.mMessageMgr.AddMessage("Label printer is not set. Printer type= null");
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
        #region User Services: OnItemClick(), OnHelpMenuClick(), OnDataStatusUpdate()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
                ToolStripItem item = (ToolStripItem)sender;
				switch(item.Name) {
					case "mnuFileExit":         this.Close(); break;
					case "mnuEditDelete":	
					case "btnDelete":
                        //Allow station operator to delete any carton in the system
                        dlgCartonDelete dlg = new dlgCartonDelete(this.mStationOperator);
                        dlg.ShowDialog();
                        break;
                    case "mnuViewRefresh":
					case "btnRefresh":
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Refreshing trip assignments...");
						this.mStationOperator.RefreshTripAssignment();
						break;
					case "mnuViewToolbar":		this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case "mnuViewStatusBar":	this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
					case "mnuToolsConfig":      App.ShowConfig(); break;
					case "mnuHelpAbout":        new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnHelpMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for configurable help menu items
			try {
                ToolStripItem item = (ToolStripItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(item.Name)[0]);
			}
			catch(Exception) { }
		}
        private void OnDataStatusUpdate(object sender, DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
		#endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu()
		private void configApplication() {
			try {
				//Create event log and database trace listeners
				try {
                    ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel, App.Mediator, App.USP_TRACE, App.EventLogName));
				}
				catch {
					ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug, App.Mediator, App.USP_TRACE, App.EventLogName));
				}
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Started"));

				//Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
				StationOperator.ScanSize = App.Config.ScanSize;
                StationOperator.LabelTypeOverrideRegular = App.Config.LabelOverrideRegular;
                StationOperator.LabelTypeOverrideReturns = App.Config.LabelOverrideReturn;
                StationOperator.ValidateLane = App.Config.ValidateLane;
                StationOperator.ValidateSmallLane = App.Config.ValidateSmallLane;
                Carton.LanePrefix = App.Config.LanePrefix;
				this.mStationOperator = new StationOperator();
				this.mStationOperator.AssignmentChanged += new EventHandler(this.OnAssignmentChanged);
				this.mStationOperator.CartonCreated +=new CartonEventHandler(OnCartonCreated);
				this.mStationOperator.CartonDeleted += new CartonEventHandler(OnCartonDeleted);
				this.mStationOperator.Station.PrinterChanged += new EventHandler(this.OnPrinterChanged);
			}
			catch(ApplicationException ex) { throw ex; } 
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services depending upon an item selected in the grid
			try {				
				//Set main menu and context menu states
				this.mnuFileExit.Enabled = true;
                this.mnuEditDelete.Enabled = this.btnDelete.Enabled = !App.Config.ReadOnly;
                this.mnuViewRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;
				this.txtLabelScan.Enabled = (!App.Config.ReadOnly && this.mStationOperator.isTripAssigned);

                this.stbMain.SetTerminalPanel(App.Mediator.TerminalID.ToString(), App.Mediator.Description);
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError(ex); } finally { Application.DoEvents(); }
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
            catch(Exception) { }
        }
        #endregion
	}
}
