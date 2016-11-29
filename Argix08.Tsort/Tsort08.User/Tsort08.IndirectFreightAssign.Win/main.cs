using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Data;
using Argix.Windows;

namespace Argix.Freight {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private BearwareTrip mSelectedTrip=null;
		private StationAssignment mSelectedAssignment=null;
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvcTrips = null, mGridSvcAssignments = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
        private MessageManager mMessageMgr = null;
        private NameValueCollection mHelpItems = null;
				
		#region Controls

        private Infragistics.Win.UltraWinGrid.UltraGrid grdTrips;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdAssignments;
        private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.Panel pnlControls;
		private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.Label lblSort;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.NumericUpDown updSortedDays;
		private BearwareDS mTripsDS;
        private BearwareDS mStationTripDS;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuTrip;
        private ToolStripMenuItem mnuStation;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuEditFind;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewBoldFonts;
        private ToolStripSeparator mnuViewSep2;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuTripAssign;
        private ToolStripSeparator mnuTripSep1;
        private ToolStripMenuItem mnuTripStartSort;
        private ToolStripMenuItem mnuTripStopSort;
        private ToolStripSeparator mnuTripSep2;
        private ToolStripMenuItem mnuTripExport;
        private ToolStripMenuItem mnuStationUnassign;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuFileImportFrom;
        private ToolStripMenuItem mnuFileImport;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripSeparator mnuFileSep4;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripSeparator mnuEditSep1;
        private ContextMenuStrip csTrip;
        private ToolStripMenuItem ctxTAssign;
        private ToolStripSeparator ctxTSep1;
        private ToolStripMenuItem ctxTStartSort;
        private ToolStripMenuItem ctxTStopSort;
        private ToolStripSeparator ctxTSep2;
        private ToolStripMenuItem ctxTExport;
        private ContextMenuStrip csStation;
        private ToolStripMenuItem ctxSUnassign;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnSave;
        private ToolStripButton btnImport;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnPrint;
        private ToolStripButton btnFind;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnRefresh;
        private ToolStripSeparator btnSep4;
        private ToolStripButton btnAssign;
        private ToolStripButton btnStartSort;
        private ToolStripButton btnStopSort;
        private ToolStripButton btnExport;
        private ToolStripSeparator btnSep5;
        private ToolStripButton btnUnassign;
        private ToolStripMenuItem ctxTRefresh;
        private ToolStripSeparator ctxTSep3;
        private ToolStripMenuItem ctxSRefresh;
        private ToolStripSeparator ctxSSep1;
		
		private System.ComponentModel.IContainer components;
		#endregion
		//Interface
		public frmMain() {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
				buildHelpMenu();
				#region Window docking
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
				this.pnlControls.DockPadding.All = 3;
				this.pnlControls.Dock = DockStyle.Top;
				this.pnlMain.DockPadding.All = 3;
				this.pnlMain.Dock = DockStyle.Fill;
				this.splitterH.MinExtra = 48;
				this.splitterH.MinSize = 48;
				this.splitterH.Dock = DockStyle.Bottom;
				this.grdTrips.Dock = DockStyle.Fill;
				this.grdAssignments.Dock = DockStyle.Bottom;
				this.stbMain.Dock = DockStyle.Bottom;
				this.pnlMain.Controls.AddRange(new Control[]{this.grdTrips, this.splitterH, this.grdAssignments});
                this.Controls.AddRange(new Control[] { this.pnlMain,this.pnlControls,this.tsMain,this.msMain,this.stbMain });
				this.grdTrips.Controls.AddRange(new Control[]{this.updSortedDays});
				#endregion
                Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
                Thread.Sleep(2000);
				
				//Create data and UI services
                this.mPageSettings = new PageSettings();
                this.mPageSettings.Landscape = true;
                this.mGridSvcTrips = new UltraGridSvc(this.grdTrips, this.txtSearch, this.lblSort);
				this.mGridSvcAssignments = new UltraGridSvc(this.grdAssignments);
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
				configApplication();
			}
            catch (Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
        }
		protected override void Dispose(bool disposing) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("BwareTripTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonCount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Started");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stopped");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Exported");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Imported");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Scanned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDSend");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Received");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsExported");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("BwareStationTripTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonCount");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdTrips = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csTrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxTRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxTAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxTStartSort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTStopSort = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxTSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxTExport = new System.Windows.Forms.ToolStripMenuItem();
            this.updSortedDays = new System.Windows.Forms.NumericUpDown();
            this.mTripsDS = new Argix.Freight.BearwareDS();
            this.grdAssignments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csStation = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxSRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxSUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.mStationTripDS = new Argix.Freight.BearwareDS();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.pnlControls = new System.Windows.Forms.Panel();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnImport = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAssign = new System.Windows.Forms.ToolStripButton();
            this.btnStartSort = new System.Windows.Forms.ToolStripButton();
            this.btnStopSort = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUnassign = new System.Windows.Forms.ToolStripButton();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSort = new System.Windows.Forms.Label();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileImportFrom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileImport = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewBoldFonts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTrip = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTripAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTripSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTripStartSort = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTripStopSort = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTripSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTripExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStationUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTrips)).BeginInit();
            this.grdTrips.SuspendLayout();
            this.csTrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updSortedDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTripsDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).BeginInit();
            this.csStation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mStationTripDS)).BeginInit();
            this.pnlControls.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.AutoScroll = true;
            this.pnlMain.Controls.Add(this.splitterH);
            this.pnlMain.Controls.Add(this.grdTrips);
            this.pnlMain.Controls.Add(this.grdAssignments);
            this.pnlMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlMain.Location = new System.Drawing.Point(0, 54);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(675, 255);
            this.pnlMain.TabIndex = 5;
            // 
            // splitterH
            // 
            this.splitterH.BackColor = System.Drawing.SystemColors.Control;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 144);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(675, 3);
            this.splitterH.TabIndex = 7;
            this.splitterH.TabStop = false;
            // 
            // grdTrips
            // 
            this.grdTrips.ContextMenuStrip = this.csTrip;
            this.grdTrips.Controls.Add(this.updSortedDays);
            this.grdTrips.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdTrips.DataMember = "BwareTripTable";
            this.grdTrips.DataSource = this.mTripsDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdTrips.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 96;
            ultraGridColumn2.Header.Caption = "Cartons";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 72;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 72;
            ultraGridColumn4.Header.Caption = "Trailer#";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 72;
            ultraGridColumn5.Format = "MM-dd-yyyy HH:mm";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Width = 132;
            ultraGridColumn6.Format = "MM-dd-yyyy HH:mm";
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn6.Width = 132;
            ultraGridColumn7.Format = "MM-dd-yyyy HH:mm";
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.Width = 132;
            ultraGridColumn8.Format = "MM-dd-yyyy HH:mm";
            ultraGridColumn8.Header.VisiblePosition = 4;
            ultraGridColumn8.Width = 132;
            ultraGridColumn9.Format = "MM-dd-yyyy HH:mm";
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.Width = 132;
            ultraGridColumn10.Format = "MM-dd-yyyy HH:mm";
            ultraGridColumn10.Header.Caption = "OSD Sent";
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn10.Width = 132;
            ultraGridColumn11.Format = "MM-dd-yyyy HH:mm";
            ultraGridColumn11.Header.VisiblePosition = 8;
            ultraGridColumn11.Width = 132;
            ultraGridColumn12.Header.Caption = "Exported";
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn12.Width = 72;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12});
            this.grdTrips.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdTrips.DisplayLayout.CaptionAppearance = appearance2;
            this.grdTrips.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTrips.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTrips.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTrips.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdTrips.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdTrips.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTrips.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTrips.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdTrips.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTrips.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdTrips.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTrips.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTrips.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTrips.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTrips.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdTrips.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTrips.Location = new System.Drawing.Point(0, 0);
            this.grdTrips.Name = "grdTrips";
            this.grdTrips.Size = new System.Drawing.Size(675, 141);
            this.grdTrips.TabIndex = 0;
            this.grdTrips.Text = "Trips- imports for the last             days";
            this.grdTrips.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTrips.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdTrips.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTripSelected);
            // 
            // csTrip
            // 
            this.csTrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxTRefresh,
            this.ctxTSep1,
            this.ctxTAssign,
            this.ctxTSep2,
            this.ctxTStartSort,
            this.ctxTStopSort,
            this.ctxTSep3,
            this.ctxTExport});
            this.csTrip.Name = "csTrip";
            this.csTrip.Size = new System.Drawing.Size(123, 132);
            // 
            // ctxTRefresh
            // 
            this.ctxTRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxTRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTRefresh.Name = "ctxTRefresh";
            this.ctxTRefresh.Size = new System.Drawing.Size(122, 22);
            this.ctxTRefresh.Text = "&Refresh";
            this.ctxTRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTSep1
            // 
            this.ctxTSep1.Name = "ctxTSep1";
            this.ctxTSep1.Size = new System.Drawing.Size(119, 6);
            // 
            // ctxTAssign
            // 
            this.ctxTAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.ctxTAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTAssign.Name = "ctxTAssign";
            this.ctxTAssign.Size = new System.Drawing.Size(122, 22);
            this.ctxTAssign.Text = "&Assign";
            this.ctxTAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTSep2
            // 
            this.ctxTSep2.Name = "ctxTSep2";
            this.ctxTSep2.Size = new System.Drawing.Size(119, 6);
            // 
            // ctxTStartSort
            // 
            this.ctxTStartSort.Image = global::Argix.Properties.Resources.Play;
            this.ctxTStartSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTStartSort.Name = "ctxTStartSort";
            this.ctxTStartSort.Size = new System.Drawing.Size(122, 22);
            this.ctxTStartSort.Text = "&Start Sort";
            this.ctxTStartSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTStopSort
            // 
            this.ctxTStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.ctxTStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTStopSort.Name = "ctxTStopSort";
            this.ctxTStopSort.Size = new System.Drawing.Size(122, 22);
            this.ctxTStopSort.Text = "S&top Sort";
            this.ctxTStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxTSep3
            // 
            this.ctxTSep3.Name = "ctxTSep3";
            this.ctxTSep3.Size = new System.Drawing.Size(119, 6);
            // 
            // ctxTExport
            // 
            this.ctxTExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.ctxTExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxTExport.Name = "ctxTExport";
            this.ctxTExport.Size = new System.Drawing.Size(122, 22);
            this.ctxTExport.Text = "&Export";
            this.ctxTExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // updSortedDays
            // 
            this.updSortedDays.BackColor = System.Drawing.SystemColors.Window;
            this.updSortedDays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.updSortedDays.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.updSortedDays.ForeColor = System.Drawing.SystemColors.WindowText;
            this.updSortedDays.Location = new System.Drawing.Point(174, 4);
            this.updSortedDays.Maximum = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.updSortedDays.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updSortedDays.Name = "updSortedDays";
            this.updSortedDays.Size = new System.Drawing.Size(36, 17);
            this.updSortedDays.TabIndex = 9;
            this.updSortedDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updSortedDays.ValueChanged += new System.EventHandler(this.OnSortedDaysChanged);
            this.updSortedDays.Leave += new System.EventHandler(this.OnSortedDaysChanged);
            // 
            // mTripsDS
            // 
            this.mTripsDS.DataSetName = "BearwareDS";
            this.mTripsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mTripsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdAssignments
            // 
            this.grdAssignments.ContextMenuStrip = this.csStation;
            this.grdAssignments.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdAssignments.DataMember = "BwareStationTripTable";
            this.grdAssignments.DataSource = this.mStationTripDS;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Appearance = appearance5;
            ultraGridColumn13.Header.Caption = "Station#";
            ultraGridColumn13.Header.VisiblePosition = 0;
            ultraGridColumn13.Width = 96;
            ultraGridColumn14.Header.Caption = "Trip#";
            ultraGridColumn14.Header.VisiblePosition = 1;
            ultraGridColumn14.Width = 96;
            ultraGridColumn15.Header.Caption = "Cartons";
            ultraGridColumn15.Header.VisiblePosition = 2;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.Header.VisiblePosition = 3;
            ultraGridColumn16.Width = 72;
            ultraGridColumn17.Header.Caption = "Trailer#";
            ultraGridColumn17.Header.VisiblePosition = 4;
            ultraGridColumn17.Width = 72;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17});
            this.grdAssignments.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.CaptionAppearance = appearance6;
            this.grdAssignments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAssignments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdAssignments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAssignments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdAssignments.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdAssignments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAssignments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAssignments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAssignments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAssignments.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAssignments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdAssignments.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdAssignments.Location = new System.Drawing.Point(0, 147);
            this.grdAssignments.Name = "grdAssignments";
            this.grdAssignments.Size = new System.Drawing.Size(675, 108);
            this.grdAssignments.TabIndex = 0;
            this.grdAssignments.Text = "Station Assignments";
            this.grdAssignments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdAssignments.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnAssignmentSelected);
            // 
            // csStation
            // 
            this.csStation.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxSRefresh,
            this.ctxSSep1,
            this.ctxSUnassign});
            this.csStation.Name = "csStation";
            this.csStation.Size = new System.Drawing.Size(123, 54);
            // 
            // ctxSRefresh
            // 
            this.ctxSRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxSRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxSRefresh.Name = "ctxSRefresh";
            this.ctxSRefresh.Size = new System.Drawing.Size(122, 22);
            this.ctxSRefresh.Text = "&Refresh";
            this.ctxSRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSSep1
            // 
            this.ctxSSep1.Name = "ctxSSep1";
            this.ctxSSep1.Size = new System.Drawing.Size(119, 6);
            // 
            // ctxSUnassign
            // 
            this.ctxSUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.ctxSUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxSUnassign.Name = "ctxSUnassign";
            this.ctxSUnassign.Size = new System.Drawing.Size(122, 22);
            this.ctxSUnassign.Text = "Unassign";
            this.ctxSUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mStationTripDS
            // 
            this.mStationTripDS.DataSetName = "BearwareDS";
            this.mStationTripDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mStationTripDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // stbMain
            // 
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 305);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(676, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 9;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // pnlControls
            // 
            this.pnlControls.Controls.Add(this.tsMain);
            this.pnlControls.Controls.Add(this.txtSearch);
            this.pnlControls.Controls.Add(this.lblSort);
            this.pnlControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlControls.Location = new System.Drawing.Point(0, 24);
            this.pnlControls.Name = "pnlControls";
            this.pnlControls.Size = new System.Drawing.Size(676, 26);
            this.pnlControls.TabIndex = 10;
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnSave,
            this.btnImport,
            this.btnPrint,
            this.btnSep2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnFind,
            this.btnSep3,
            this.btnRefresh,
            this.btnSep4,
            this.btnAssign,
            this.btnStartSort,
            this.btnStopSort,
            this.btnExport,
            this.btnSep5,
            this.btnUnassign});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(676, 25);
            this.tsMain.TabIndex = 13;
            this.tsMain.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Text = "toolStripButton1";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Text = "toolStripButton2";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "toolStripButton3";
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnImport
            // 
            this.btnImport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImport.Image = global::Argix.Properties.Resources.ImportXML;
            this.btnImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(23, 22);
            this.btnImport.Text = "toolStripButton4";
            this.btnImport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Text = "toolStripButton5";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCut
            // 
            this.btnCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCut.Image = global::Argix.Properties.Resources.Cut;
            this.btnCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCut.Name = "btnCut";
            this.btnCut.Size = new System.Drawing.Size(23, 22);
            this.btnCut.Text = "toolStripButton7";
            this.btnCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::Argix.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Text = "toolStripButton8";
            this.btnCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::Argix.Properties.Resources.Paste;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.Text = "toolStripButton9";
            this.btnPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnFind
            // 
            this.btnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFind.Image = global::Argix.Properties.Resources.Search;
            this.btnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(23, 22);
            this.btnFind.Text = "toolStripButton6";
            this.btnFind.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.Text = "toolStripButton10";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep4
            // 
            this.btnSep4.Name = "btnSep4";
            this.btnSep4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnAssign
            // 
            this.btnAssign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.btnAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(23, 22);
            this.btnAssign.Text = "toolStripButton11";
            this.btnAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnStartSort
            // 
            this.btnStartSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStartSort.Image = global::Argix.Properties.Resources.Play;
            this.btnStartSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStartSort.Name = "btnStartSort";
            this.btnStartSort.Size = new System.Drawing.Size(23, 22);
            this.btnStartSort.Text = "toolStripButton12";
            this.btnStartSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnStopSort
            // 
            this.btnStopSort.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.btnStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStopSort.Name = "btnStopSort";
            this.btnStopSort.Size = new System.Drawing.Size(23, 22);
            this.btnStopSort.Text = "toolStripButton13";
            this.btnStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.Text = "toolStripButton14";
            this.btnExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep5
            // 
            this.btnSep5.Name = "btnSep5";
            this.btnSep5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnUnassign
            // 
            this.btnUnassign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.btnUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnassign.Name = "btnUnassign";
            this.btnUnassign.Size = new System.Drawing.Size(23, 22);
            this.btnUnassign.Text = "toolStripButton15";
            this.btnUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSearch.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtSearch.Location = new System.Drawing.Point(3, 3);
            this.txtSearch.MaxLength = 128;
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(135, 21);
            this.txtSearch.TabIndex = 11;
            // 
            // lblSort
            // 
            this.lblSort.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSort.Location = new System.Drawing.Point(138, 3);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(108, 21);
            this.lblSort.TabIndex = 12;
            this.lblSort.Text = "Location";
            this.lblSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTrip,
            this.mnuStation,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(676, 24);
            this.msMain.TabIndex = 11;
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
            this.mnuFileSep2,
            this.mnuFileImportFrom,
            this.mnuFileImport,
            this.mnuFileSep3,
            this.mnuFileSetup,
            this.mnuFilePrint,
            this.mnuFilePreview,
            this.mnuFileSep4,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(179, 22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(179, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(176, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(179, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(179, 22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(176, 6);
            // 
            // mnuFileImportFrom
            // 
            this.mnuFileImportFrom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileImportFrom.Name = "mnuFileImportFrom";
            this.mnuFileImportFrom.Size = new System.Drawing.Size(179, 22);
            this.mnuFileImportFrom.Text = "Import Trips &From...";
            this.mnuFileImportFrom.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileImport
            // 
            this.mnuFileImport.Image = global::Argix.Properties.Resources.ImportXML;
            this.mnuFileImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileImport.Name = "mnuFileImport";
            this.mnuFileImport.Size = new System.Drawing.Size(179, 22);
            this.mnuFileImport.Text = "&Import Trips";
            this.mnuFileImport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(176, 6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(179, 22);
            this.mnuFileSetup.Text = "Page Set&up...";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(179, 22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(179, 22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep4
            // 
            this.mnuFileSep4.Name = "mnuFileSep4";
            this.mnuFileSep4.Size = new System.Drawing.Size(176, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(179, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditSep1,
            this.mnuEditFind});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCut.Text = "C&ut";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(109, 22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(106, 6);
            // 
            // mnuEditFind
            // 
            this.mnuEditFind.Image = global::Argix.Properties.Resources.Search;
            this.mnuEditFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditFind.Name = "mnuEditFind";
            this.mnuEditFind.Size = new System.Drawing.Size(109, 22);
            this.mnuEditFind.Text = "&Search";
            this.mnuEditFind.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewBoldFonts,
            this.mnuViewSep2,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(130, 22);
            this.mnuViewRefresh.Text = "&Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(127, 6);
            // 
            // mnuViewBoldFonts
            // 
            this.mnuViewBoldFonts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewBoldFonts.Name = "mnuViewBoldFonts";
            this.mnuViewBoldFonts.Size = new System.Drawing.Size(130, 22);
            this.mnuViewBoldFonts.Text = "&Bold Fonts";
            this.mnuViewBoldFonts.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(127, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(130, 22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(130, 22);
            this.mnuViewStatusBar.Text = "StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTrip
            // 
            this.mnuTrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuTripAssign,
            this.mnuTripSep1,
            this.mnuTripStartSort,
            this.mnuTripStopSort,
            this.mnuTripSep2,
            this.mnuTripExport});
            this.mnuTrip.Name = "mnuTrip";
            this.mnuTrip.Size = new System.Drawing.Size(40, 20);
            this.mnuTrip.Text = "&Trip";
            // 
            // mnuTripAssign
            // 
            this.mnuTripAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.mnuTripAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuTripAssign.Name = "mnuTripAssign";
            this.mnuTripAssign.Size = new System.Drawing.Size(145, 22);
            this.mnuTripAssign.Text = "&Assign Trip...";
            this.mnuTripAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTripSep1
            // 
            this.mnuTripSep1.Name = "mnuTripSep1";
            this.mnuTripSep1.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuTripStartSort
            // 
            this.mnuTripStartSort.Image = global::Argix.Properties.Resources.Play;
            this.mnuTripStartSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuTripStartSort.Name = "mnuTripStartSort";
            this.mnuTripStartSort.Size = new System.Drawing.Size(145, 22);
            this.mnuTripStartSort.Text = "&Start Sort";
            this.mnuTripStartSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTripStopSort
            // 
            this.mnuTripStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.mnuTripStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuTripStopSort.Name = "mnuTripStopSort";
            this.mnuTripStopSort.Size = new System.Drawing.Size(145, 22);
            this.mnuTripStopSort.Text = "Sto&p Sort";
            this.mnuTripStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTripSep2
            // 
            this.mnuTripSep2.Name = "mnuTripSep2";
            this.mnuTripSep2.Size = new System.Drawing.Size(142, 6);
            // 
            // mnuTripExport
            // 
            this.mnuTripExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuTripExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuTripExport.Name = "mnuTripExport";
            this.mnuTripExport.Size = new System.Drawing.Size(145, 22);
            this.mnuTripExport.Text = "&Export Trips...";
            this.mnuTripExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuStation
            // 
            this.mnuStation.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStationUnassign});
            this.mnuStation.Name = "mnuStation";
            this.mnuStation.Size = new System.Drawing.Size(56, 20);
            this.mnuStation.Text = "&Station";
            // 
            // mnuStationUnassign
            // 
            this.mnuStationUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.mnuStationUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStationUnassign.Name = "mnuStationUnassign";
            this.mnuStationUnassign.Size = new System.Drawing.Size(146, 22);
            this.mnuStationUnassign.Text = "&Unassign Trip";
            this.mnuStationUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.mnuToolsConfig.Text = "&Configuration...";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(265, 22);
            this.mnuHelpAbout.Text = "&About Indirect Freight Assignment...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(262, 6);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(676, 329);
            this.Controls.Add(this.pnlControls);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.msMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.Text = "Indirect Freight Assignment";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTrips)).EndInit();
            this.grdTrips.ResumeLayout(false);
            this.csTrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updSortedDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTripsDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).EndInit();
            this.csStation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mStationTripDS)).EndInit();
            this.pnlControls.ResumeLayout(false);
            this.pnlControls.PerformLayout();
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
                    this.mnuViewBoldFonts.Checked = !Convert.ToBoolean(global::Argix.Properties.Settings.Default.BoldFonts);
                    App.CheckVersion();
                }
                catch (Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				this.mToolTip.SetToolTip(this.txtSearch, "Search for a trip.");
				#endregion
				
				//Set control defaults
                #region Grid Initialization
                this.grdTrips.DisplayLayout.Bands[0].Columns["Number"].SortIndicator = SortIndicator.Ascending;
				this.grdAssignments.DisplayLayout.Bands[0].Columns["StationNumber"].SortIndicator = SortIndicator.Ascending;
                #endregion
                this.updSortedDays.Minimum = 1;
				this.updSortedDays.Maximum = 14;
				this.updSortedDays.Value = FreightFactory.SortedDays;
				this.updSortedDays.Enabled = true;
				FreightFactory.RefreshFreight();
				FreightFactory.RefreshStationAssignments();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); } finally { setUserServices(); this.Cursor = Cursors.Default; }
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
                global::Argix.Properties.Settings.Default.BoldFonts = this.mnuViewBoldFonts.Checked;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
                ArgixTrace.WriteLine(new TraceMessage(App.Version, App.Product, LogLevel.Information, "App Stopped"));
            }
        }
        #region Freight: OnSortedDaysChanged(), OnFreightChanged(), OnTripSelected()
		private void OnSortedDaysChanged(object sender, System.EventArgs e) {
			//Event handler for change in sorted days value
			try {
				FreightFactory.SortedDays = Convert.ToInt32(this.updSortedDays.Value);
				ArgixTrace.WriteLine(new TraceMessage("Sorted days changed to " + FreightFactory.SortedDays.ToString(), App.EventLogName, LogLevel.Debug));
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnFreightChanged(object sender, System.EventArgs e) {
			//Rebind
			try {
				this.mMessageMgr.AddMessage("Loading trips for " + App.Mediator.Description + "...");
				int iIndex = (this.grdTrips.Selected.Rows.Count > 0) ? this.grdTrips.Selected.Rows[0].VisibleIndex : 0;
				this.mTripsDS.Clear();
				this.mTripsDS.Merge(FreightFactory.InboundFreight);
				if(this.grdTrips.Rows.VisibleRowCount > 0) {
					if(iIndex >=0 && iIndex < this.grdTrips.Rows.VisibleRowCount) 
						this.grdTrips.Rows.GetRowAtVisibleIndex(iIndex).Selected = true;
					else
						this.grdTrips.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdTrips.Selected.Rows[0].Activate();
					this.grdTrips.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdTrips.Selected.Rows[0]);
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnTripSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected trip
			try {
				//Create new selected trip instance after selection change
				this.mSelectedTrip = null;
				if(this.grdTrips.Selected.Rows.Count > 0) {
					string tripNumber = this.grdTrips.Selected.Rows[0].Cells["Number"].Value.ToString().TrimEnd();
					this.mSelectedTrip = FreightFactory.GetFreight(tripNumber);
					ArgixTrace.WriteLine(new TraceMessage("Trip #" + this.mSelectedTrip.Number + " selected", App.Product, LogLevel.Debug));
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
		#endregion
		#region Assignments: OnAssignmentsChanged(), OnAssignmentSelected()
		private void OnAssignmentsChanged(object sender, System.EventArgs e) {
			//Rebind
			try {
				this.mMessageMgr.AddMessage("Loading station assignments for " + App.Mediator.Description + "...");
				int iIndex = (this.grdAssignments.Selected.Rows.Count > 0) ? this.grdAssignments.Selected.Rows[0].VisibleIndex : 0;
				this.mStationTripDS.Clear();
				this.mStationTripDS.Merge(FreightFactory.StationAssignments);
				if(this.grdAssignments.Rows.VisibleRowCount > 0) {
					if(iIndex >=0 && iIndex < this.grdAssignments.Rows.VisibleRowCount) 
						this.grdAssignments.Rows.GetRowAtVisibleIndex(iIndex).Selected = true;
					else
						this.grdAssignments.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdAssignments.Selected.Rows[0].Activate();
					this.grdAssignments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdAssignments.Selected.Rows[0]);
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnAssignmentSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in sleected assignment
			try {
				//Create new instance of StationTripAssignmnet after selection changed
				this.mSelectedAssignment = null;
				if(this.grdAssignments.Selected.Rows.Count > 0) {
					string stationNumber = this.grdAssignments.Selected.Rows[0].Cells["StationNumber"].Value.ToString();
					string tripNumber = this.grdAssignments.Selected.Rows[0].Cells["TripNumber"].Value.ToString();
					this.mSelectedAssignment =  FreightFactory.GetAssignment(stationNumber, tripNumber);
                    ArgixTrace.WriteLine(new TraceMessage("Assignment #" +  this.mSelectedAssignment.SortStation.Number + "-" + this.mSelectedAssignment.InboundFreight.Number + " selected",App.Product,LogLevel.Debug));
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
		#endregion
		#region Grid Services: OnGridMouseDown()
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			try {
				//Set menu and toolbar services
				UltraGrid oGrid = (UltraGrid)sender;
				UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				object oContext = oUIElement.GetContext(typeof(UltraGridCell));
				if(oContext != null) {
					//On row
					UltraGridCell oCell = (UltraGridCell)oContext;
					oGrid.ActiveRow = oCell.Row;
					oGrid.ActiveRow.Selected = true;
				}
				else {
					//Off row
					oGrid.ActiveRow = null;
					if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Selected = false;
				}
				oGrid.Focus();
			} 
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); }
		}
		#endregion
		#region User Services: OnItemClick(), OnHelpMenuClick(), OnDataStatusUpdate()
		private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
					case "mnuFileNew":
					case "btnNew":
						//Import a trip to database from user specified values
						this.mMessageMgr.AddMessage("Adding a new trip...");
						BearwareTrip trip = FreightFactory.GetFreight("");
						dlgTrip view = new dlgTrip(ref trip);
						if(view.ShowDialog() == DialogResult.OK) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Creating trip# " + trip.Number + "...");
							trip.Create();
						}
						break;
					case "mnuFileOpen":
					case "btnOpen": 
					    break;
                    case "mnuFileSave":
                    case "btnSave":
                        break;
                    case "mnuFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Trip Files (*.xml) | *.xml";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Trips As...";
                        dlgSave.FileName = App.Mediator.Description + ", " + DateTime.Today.ToLongDateString();
                        dlgSave.OverwritePrompt = true;
                        if (dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
                            Application.DoEvents();
                            FreightFactory.InboundFreight.WriteXml(dlgSave.FileName, XmlWriteMode.WriteSchema);
                        }
                        break;
                    case "mnuFileImportFrom":
                        //Import trips to database from a configuration-based import file
                        int iRows = 0;
                        this.mMessageMgr.AddMessage("Import trips from " + BearwareTrip.ImportFile + "...");
                        if (File.Exists(BearwareTrip.ImportFile)) {
                            this.Cursor = Cursors.WaitCursor;
                            iRows = importTrips(BearwareTrip.ImportFile);
                            MessageBox.Show(this, "Imported " + iRows.ToString() + " records.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show(this, "Import file " + BearwareTrip.ImportFile + " not found.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    case "mnuFileImport":
                    case "btnImport":
                        //Import trips to database from a local file specified by the user
                        int rowsRead = 0;
                        this.mMessageMgr.AddMessage("Import trips from a user specified location...");
                        OpenFileDialog dlgFileOpen = new OpenFileDialog();
                        dlgFileOpen.InitialDirectory = Environment.CurrentDirectory;
                        dlgFileOpen.Filter = "Trip files (*.xml)|*.xml";
                        dlgFileOpen.FilterIndex = 1;
                        dlgFileOpen.RestoreDirectory = true;
                        dlgFileOpen.Title = "Import Trips";
                        if (dlgFileOpen.ShowDialog() == DialogResult.OK && dlgFileOpen.FileName.Length > 0) {
                            this.Cursor = Cursors.WaitCursor;
                            rowsRead = importTrips(dlgFileOpen.FileName);
                            MessageBox.Show(this, "Imported " + rowsRead.ToString() + " records.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "mnuFileSetup": UltraGridPrinter.PageSettings(); break;
                    case "mnuFilePrint":
                        if (this.grdTrips.Focused)
                            UltraGridPrinter.Print(this.grdTrips, App.Mediator.Description.Trim().ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString(), true);
                        else if (this.grdAssignments.Focused)
                            UltraGridPrinter.Print(this.grdAssignments, App.Mediator.Description.Trim().ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString(), true);
                        break;
                    case "btnPrint":
                        if (this.grdTrips.Focused)
                            UltraGridPrinter.Print(this.grdTrips, App.Mediator.Description.Trim().ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString(), false);
                        else if (this.grdAssignments.Focused)
                            UltraGridPrinter.Print(this.grdAssignments, App.Mediator.Description.Trim().ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString(), false);
                        break;
                    case "mnuFilePreview":
                    case "btnPreview":
                        if (this.grdTrips.Focused)
                            UltraGridPrinter.PrintPreview(this.grdTrips, App.Mediator.Description.Trim().ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString());
                        else if (this.grdAssignments.Focused)
                            UltraGridPrinter.PrintPreview(this.grdAssignments, App.Mediator.Description.Trim().ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString());
                        break;
                    case "mnuFileExit": this.Close(); Application.Exit(); break;
                    case "mnuEditCut":
                    case "btnCut":
                        break;
                    case "mnuEditCopy":
                    case "btnCopy":
                        break;
                    case "mnuEditPaste":
                    case "btnPaste":
                        break;
                    case "mnuEditFind":
                    case "btnFind":
						this.txtSearch.Focus();
						this.txtSearch.SelectAll();
						break;
					case "mnuViewRefresh":
					case "btnRefresh":
						this.Cursor = Cursors.WaitCursor;
						FreightFactory.RefreshFreight();
						FreightFactory.RefreshStationAssignments();
						break;
                    case "ctxTRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        FreightFactory.RefreshFreight();
                        break;
                    case "ctxSRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        FreightFactory.RefreshStationAssignments();
                        break;
                    case "mnuViewBoldFonts":
                        this.mnuViewBoldFonts.Checked = !this.mnuViewBoldFonts.Checked;
                        Font font = new Font("Verdana", 8.25F, (!this.mnuViewBoldFonts.Checked ? FontStyle.Regular : FontStyle.Bold), GraphicsUnit.Point, ((System.Byte)(0)));
                        this.grdTrips.Font = this.grdAssignments.Font = this.txtSearch.Font = font;
                        break;
					case "mnuViewToolbar":		this.tsMain.Visible = (this.mnuViewToolbar.Checked = !this.mnuViewToolbar.Checked); break;
					case "mnuViewStatusBar":	this.stbMain.Visible = (this.mnuViewStatusBar.Checked = !this.mnuViewStatusBar.Checked); break;
					case "mnuTripAssign":
					case "ctxTAssign":
					case "btnAssign":
						//Prompt user to select stations(from available) and assign selected trip if trip is not startred - start
						BearwareTrip selectedTrip = this.mSelectedTrip;
						WorkstationDS oStations = new WorkstationDS();
						dlgAssignment dlgAssign = new dlgAssignment(oStations);
						if(dlgAssign.ShowDialog(this) == DialogResult.OK) {
							this.Cursor = Cursors.WaitCursor;
							if(!selectedTrip.IsStarted) this.mnuTripStartSort.PerformClick();
							foreach(WorkstationDS.WorkstationDetailTableRow row in oStations.WorkstationDetailTable) {
								//Create each assignment; continue with next on create failure
								try {
									this.mMessageMgr.AddMessage("Creating an assignment for trip# " + selectedTrip.Number + " on station# " + row.Number.ToString() + "...");
									StationAssignment assignment = FreightFactory.GetAssignment(row.Number.ToString(), selectedTrip.Number);
									assignment.Create();
								}
								catch(Exception ex) { App.ReportError(ex); }
							}
						}
						break;
					case "mnuTripStartSort":
                    case "ctxTStartSort":
                    case "btnStartSort":
						//Start sort for the selected trip
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Starting sort for trip# " + this.mSelectedTrip.Number + "...");
						this.mSelectedTrip.StartSort();
						break;
					case "mnuTripStopSort":
                    case "ctxTStopSort":
                    case "btnStopSort":
						//Stop sort for the selected trip
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Stopping sort for trip# " + this.mSelectedTrip.Number + "...");
						this.mSelectedTrip.StopSort();
						break;
					case "mnuTripExport":
                    case "ctxTExport":
                    case "btnExport":
						//Export cartons sorted for the selected trip to xml file
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Exporting trip# " + this.mSelectedTrip.Number + "...");
						int iRecords = this.mSelectedTrip.Export();
						MessageBox.Show(this, "Exported " + iRecords.ToString() + " records to " + BearwareTrip.ExportPath, App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
						break;
					case "mnuStationUnassign":
                    case "ctxSUnassign":
                    case "btnUnassign":
						//Unasign trip from station - one assignment
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Unassigning trip# " + this.mSelectedAssignment.InboundFreight.Number + " from station# " + this.mSelectedAssignment.SortStation.Number + "...");
						this.mSelectedAssignment.Delete();
						break;
					case "mnuToolsConfig": App.ShowConfig(); break;
					case "mnuHelpAbout":
						new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this);
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem item = (ToolStripItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(item.Text)[0]);
            }
            catch(Exception) { }
        }
        private void OnDataStatusUpdate(object sender,DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
		}
		#endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu()
		private void configApplication() {
			try {
                try {
                    ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel, App.Mediator, App.USP_TRACE, App.EventLogName));
                }
                catch {
                    ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug, App.Mediator, App.USP_TRACE, App.EventLogName));
                }
                ArgixTrace.WriteLine(new TraceMessage(App.Version, App.Product, LogLevel.Information, "App Started"));

                //Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
				FreightFactory.SortedDays = App.Config.TripDays;
				FreightFactory.FreightChanged += new EventHandler(this.OnFreightChanged);
				FreightFactory.AssignmentsChanged += new EventHandler(this.OnAssignmentsChanged);
                BearwareTrip.AllowNew = App.Config.ManualImport;
                BearwareTrip.AllowImport = App.Config.ManualImport;
                BearwareTrip.ImportFile = App.Config.ImportFile;
                BearwareTrip.ExportPath = App.Config.ExportPath;
			}
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException("Configuration Failure", ex); }
        }
		private void setUserServices() {
			//Set user services
			bool canAssign=false, canStart=false, canStop=false, canExport=false, canUnassign=false;
			try {
				if(!App.Config.ReadOnly && this.grdTrips.Focused && this.mSelectedTrip != null) {
					canAssign = (!this.mSelectedTrip.IsOSDSent);
					canStart = (!this.mSelectedTrip.IsStarted);
					canStop = (this.mSelectedTrip.IsStarted && !this.mSelectedTrip.IsStopped);
					canExport = (this.mSelectedTrip.IsStopped && !this.mSelectedTrip.IsOSDSent);
				}
				canUnassign = (!App.Config.ReadOnly && this.grdAssignments.Focused && this.mSelectedAssignment != null);

                this.mnuFileNew.Enabled = this.btnNew.Enabled = (!App.Config.ReadOnly && BearwareTrip.AllowNew);
                this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
                this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = this.grdTrips.Focused;
                this.mnuFileSetup.Enabled = true;
                this.mnuFilePreview.Enabled = this.grdTrips.Focused || this.grdAssignments.Focused;
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = this.grdTrips.Focused || this.grdAssignments.Focused;
                this.mnuFileImportFrom.Enabled = this.btnImport.Enabled = (!App.Config.ReadOnly && BearwareTrip.ImportFile.Length > 0);
				this.mnuFileImport.Enabled = (!App.Config.ReadOnly && BearwareTrip.AllowImport);
				this.mnuFileExit.Enabled = true;
                this.mnuEditCut.Enabled = this.btnCut.Enabled = false;
                this.mnuEditCopy.Enabled = this.btnCopy.Enabled = false;
                this.mnuEditPaste.Enabled = this.btnPaste.Enabled = false;
                this.mnuEditFind.Enabled = this.btnFind.Enabled = false;
				this.mnuViewRefresh.Enabled = this.ctxTRefresh.Enabled = this.ctxSRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
				this.mnuTripAssign.Enabled = this.ctxTAssign.Enabled = this.btnAssign.Enabled = canAssign;
				this.mnuTripStartSort.Enabled = this.ctxTStartSort.Enabled = this.btnStartSort.Enabled = canStart;
				this.mnuTripStopSort.Enabled = this.ctxTStopSort.Enabled = this.btnStopSort.Enabled = canStop;
				this.mnuTripExport.Enabled = this.ctxTExport.Enabled = this.btnExport.Enabled = canExport;
				this.mnuStationUnassign.Enabled = this.ctxSUnassign.Enabled = this.btnUnassign.Enabled = canUnassign;
                this.mnuToolsConfig.Enabled = true;
                this.mnuHelpAbout.Enabled = true;
				
                this.stbMain.SetTerminalPanel(App.Mediator.TerminalID.ToString(), App.Mediator.Description);
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
            catch (Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
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
		private int importTrips(string importFile) {
			//Import trips from the specified xml file
			BearwareTrip trip=null;
			int iRows=0;
			try {
				//Import the entire trip file as xml
				TripSummary dsImports = new TripSummary();
				try {
					//dsImports.EnforceConstraints = true;
					dsImports.ReadXml(importFile);
				}
				catch(Exception ex) { App.ReportError(new ApplicationException("Exception inporting trip file " + importFile, ex), true, LogLevel.Error); }
				foreach(TripSummary.tripRow row in dsImports.trip.Rows) {
					//Import each trip; continue with next record on create failure
					try {
						this.mMessageMgr.AddMessage("Creating trip# " + row.tripNumber + "...");
						trip = FreightFactory.GetFreight("");
						trip.Number = row.tripNumber;
						trip.CartonCount = row.cartonCount;
						trip.Carrier = row.carrier;
						trip.TrailerNumber = row.trailer;
						trip.Imported = DateTime.Now;
						if(!row.IstrailerArrivalNull()) trip.Received = row.trailerArrival;
						if(!row.IsfirstScanTimeNull()) trip.Scanned = row.firstScanTime; 
						if(!row.IsosdCreatedNull()) trip.OSDSend = row.osdCreated;
						trip.Create();
						iRows++;
					}
					catch(Exception ex) { App.ReportError(new ApplicationException("Exception creating trip# " + trip.Number, ex), true, LogLevel.Error); }
				}
			}
			catch(Exception ex) { throw ex; }
			return iRows;
		}
	}
}
