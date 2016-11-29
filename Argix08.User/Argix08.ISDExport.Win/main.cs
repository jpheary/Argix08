using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Drawing.Printing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Data;
using Argix.Enterprise;
using Argix.Windows;

namespace Argix.AgentLineHaul {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private Pickup mPickup=null;
		private UltraGridSvc mGridSvcPickups=null, mGridSvcCartons=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		private bool mCalendarOpen=false;
		
		#region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdPickups;
		private System.Windows.Forms.DateTimePicker dtpPickupDate;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdSortedItems;
		private Argix.Enterprise.SortedItemDS mSortedItemsDS;
		private System.Windows.Forms.Splitter splitterH;
        private Argix.Enterprise.PickupDS mPickupsDS;
        private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.Panel pnlPickupDate;
		private System.Windows.Forms.MonthCalendar calPickupDate;
        private System.Windows.Forms.Panel pnlTop;
        private MenuStrip mnuMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripMenuItem mnuFileExport;
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
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuHelp;
        private ToolStripSeparator mnuEditSep1;
        private ToolStripMenuItem mnuEditSearch;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripMenuItem mnuViewCartons;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewCalendar;
        private ToolStripSeparator mnuViewSep2;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuHelpAbout;
        private Panel pnlCalHeader;
        private Label lblCloseCal;
        private Label lblCalHeader;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStrip tlbMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnSave;
        private ToolStripButton btnExport;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnPrint;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripButton btnFind;
        private ToolStripButton btnRefresh;
        private ToolStripSeparator btnSep4;
        private ToolStripButton btnSortedItems;
        private ToolStripMenuItem mnuViewClientConfig;
        private ContextMenuStrip ctxMain;
        private ToolStripMenuItem ctxCut;
        private ToolStripMenuItem ctxCopy;
        private ToolStripMenuItem ctxPaste;
		private System.ComponentModel.IContainer components;
		#endregion
		
		public frmMain() {
			//Constructor			
			try {
				InitializeComponent();
				this.Text = App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
                this.mnuMain.Dock = DockStyle.Top;
				this.tlbMain.Dock = DockStyle.Top;
				this.grdPickups.Dock = DockStyle.Fill;
                this.pnlCalHeader.Dock = DockStyle.Top;
                this.calPickupDate.Dock = DockStyle.Fill;
                this.pnlPickupDate.Controls.AddRange(new Control[] { this.calPickupDate,this.pnlCalHeader });
				this.pnlPickupDate.Dock = DockStyle.Left;
                this.grdPickups.Controls.AddRange(new Control[] { this.dtpPickupDate });
				this.pnlTop.Controls.AddRange(new Control[]{this.grdPickups, this.pnlPickupDate});
				this.pnlTop.Dock = DockStyle.Fill;
				this.splitterH.MinExtra = 192;
				this.splitterH.MinSize = 144;
				this.splitterH.Dock = DockStyle.Bottom;
				this.grdSortedItems.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.pnlTop,this.splitterH,this.grdSortedItems,this.tlbMain,this.mnuMain, this.stbMain });
				this.grdPickups.Controls.AddRange(new Control[]{this.dtpPickupDate});
				#endregion
				
				//Create data and UI services
				this.mGridSvcPickups = new UltraGridSvc(this.grdPickups);
				this.mGridSvcCartons = new UltraGridSvc(this.grdSortedItems);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PickupTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DivisionNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickUpDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorKey");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SealNumber");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SortedItemTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LABEL_SEQ_NUMBER", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CLIENT_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CLIENT_DIV_NUM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AGENT_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VENDOR_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SORTED_LOCATION");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DAMAGE_CODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PICKUP_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PICKUP_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STORE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ZONE_CODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TRAILER_LOAD_NUM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ITEM_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ITEM_WEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VENDOR_KEY");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VENDOR_ITEM_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RETURN_FLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RETURN_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SHIFT_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SHIFT_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("END_TIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ARC_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STATION");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ITEM_CUBE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SORT_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SAN_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ELAPSED_SECONDS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DOWN_SECONDS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PO_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OS_TRACKING_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SHIPPING_METHOD");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SAMPLE_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScanString");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InboundLabelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grdPickups = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpPickupDate = new System.Windows.Forms.DateTimePicker();
            this.mPickupsDS = new Argix.Enterprise.PickupDS();
            this.grdSortedItems = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSortedItemsDS = new Argix.Enterprise.SortedItemDS();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.pnlPickupDate = new System.Windows.Forms.Panel();
            this.pnlCalHeader = new System.Windows.Forms.Panel();
            this.lblCloseCal = new System.Windows.Forms.Label();
            this.lblCalHeader = new System.Windows.Forms.Label();
            this.calPickupDate = new System.Windows.Forms.MonthCalendar();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.mnuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewCartons = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewClientConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewCalendar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnFind = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSortedItems = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickups)).BeginInit();
            this.grdPickups.SuspendLayout();
            this.ctxMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPickupsDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSortedItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortedItemsDS)).BeginInit();
            this.pnlPickupDate.SuspendLayout();
            this.pnlCalHeader.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.mnuMain.SuspendLayout();
            this.tlbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdPickups
            // 
            this.grdPickups.CausesValidation = false;
            this.grdPickups.ContextMenuStrip = this.ctxMain;
            this.grdPickups.Controls.Add(this.dtpPickupDate);
            this.grdPickups.DataMember = "PickupTable";
            this.grdPickups.DataSource = this.mPickupsDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "TLViewTable";
            ultraGridColumn1.Header.Caption = "Client#";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.Caption = "Div#";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 48;
            ultraGridColumn3.Header.Caption = "Client Name";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 168;
            ultraGridColumn4.Header.Caption = "Shipper#";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 72;
            ultraGridColumn5.Header.Caption = "Shipper Name";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 168;
            ultraGridColumn6.Header.Caption = "Pickup Date";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 96;
            ultraGridColumn7.Header.Caption = "Pickup#";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 72;
            ultraGridColumn8.Header.Caption = "Freight Type";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 96;
            ultraGridColumn9.Header.Caption = "TDS#";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 72;
            ultraGridColumn10.Header.Caption = "Vendor Key";
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Width = 72;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Width = 96;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn13.Hidden = true;
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
            ultraGridColumn12,
            ultraGridColumn13});
            this.grdPickups.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.CaptionAppearance = appearance2;
            this.grdPickups.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdPickups.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdPickups.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdPickups.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdPickups.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdPickups.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdPickups.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdPickups.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdPickups.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdPickups.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdPickups.Location = new System.Drawing.Point(245, 6);
            this.grdPickups.Name = "grdPickups";
            this.grdPickups.Size = new System.Drawing.Size(400, 147);
            this.grdPickups.TabIndex = 1;
            this.grdPickups.Text = "Pickups for ";
            this.grdPickups.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnPickupSelected);
            // 
            // ctxMain
            // 
            this.ctxMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCut,
            this.ctxCopy,
            this.ctxPaste});
            this.ctxMain.Name = "ctxMain";
            this.ctxMain.Size = new System.Drawing.Size(103, 70);
            // 
            // ctxCut
            // 
            this.ctxCut.Image = global::Argix.Properties.Resources.Cut;
            this.ctxCut.Name = "ctxCut";
            this.ctxCut.Size = new System.Drawing.Size(102, 22);
            this.ctxCut.Text = "Cu&t";
            this.ctxCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCopy
            // 
            this.ctxCopy.Image = global::Argix.Properties.Resources.Copy;
            this.ctxCopy.Name = "ctxCopy";
            this.ctxCopy.Size = new System.Drawing.Size(102, 22);
            this.ctxCopy.Text = "&Copy";
            this.ctxCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxPaste
            // 
            this.ctxPaste.Image = global::Argix.Properties.Resources.Paste;
            this.ctxPaste.Name = "ctxPaste";
            this.ctxPaste.Size = new System.Drawing.Size(102, 22);
            this.ctxPaste.Text = "&Paste";
            this.ctxPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // dtpPickupDate
            // 
            this.dtpPickupDate.CausesValidation = false;
            this.dtpPickupDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPickupDate.Location = new System.Drawing.Point(80, 0);
            this.dtpPickupDate.MaxDate = new System.DateTime(2031, 12, 31, 0, 0, 0, 0);
            this.dtpPickupDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpPickupDate.Name = "dtpPickupDate";
            this.dtpPickupDate.Size = new System.Drawing.Size(216, 21);
            this.dtpPickupDate.TabIndex = 2;
            this.dtpPickupDate.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpPickupDate.DropDown += new System.EventHandler(this.OnCalendarOpened);
            this.dtpPickupDate.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            // 
            // mPickupsDS
            // 
            this.mPickupsDS.DataSetName = "PickupDS";
            this.mPickupsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mPickupsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdSortedItems
            // 
            this.grdSortedItems.CausesValidation = false;
            this.grdSortedItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSortedItems.DataSource = this.mSortedItemsDS.SortedItemTable;
            appearance5.BackColor = System.Drawing.SystemColors.Control;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdSortedItems.DisplayLayout.Appearance = appearance5;
            ultraGridBand2.AddButtonCaption = "FreightAssignmentDetailTable";
            ultraGridColumn14.Header.Caption = "Label Seqnce#";
            ultraGridColumn14.Header.VisiblePosition = 8;
            ultraGridColumn14.Width = 120;
            ultraGridColumn15.Header.Caption = "Client#";
            ultraGridColumn15.Header.VisiblePosition = 0;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.Header.Caption = "Div#";
            ultraGridColumn16.Header.VisiblePosition = 1;
            ultraGridColumn16.Width = 48;
            ultraGridColumn17.Header.VisiblePosition = 3;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.Caption = "Vendor#";
            ultraGridColumn18.Header.VisiblePosition = 2;
            ultraGridColumn18.Width = 72;
            ultraGridColumn19.Header.VisiblePosition = 6;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.Header.Caption = "Damage Code";
            ultraGridColumn20.Header.VisiblePosition = 14;
            ultraGridColumn20.Width = 72;
            ultraGridColumn21.Header.Caption = "Pickup Date";
            ultraGridColumn21.Header.VisiblePosition = 4;
            ultraGridColumn21.Width = 96;
            ultraGridColumn22.Header.Caption = "Pickup#";
            ultraGridColumn22.Header.VisiblePosition = 5;
            ultraGridColumn22.Width = 72;
            ultraGridColumn23.Header.Caption = "Store";
            ultraGridColumn23.Header.VisiblePosition = 10;
            ultraGridColumn23.Width = 60;
            ultraGridColumn24.Header.VisiblePosition = 12;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.VisiblePosition = 15;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Header.VisiblePosition = 16;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn27.Header.Caption = "Weight";
            ultraGridColumn27.Header.VisiblePosition = 9;
            ultraGridColumn27.Width = 60;
            ultraGridColumn28.Header.VisiblePosition = 18;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.Caption = "Vendor Item#";
            ultraGridColumn29.Header.VisiblePosition = 7;
            ultraGridColumn29.Width = 120;
            ultraGridColumn30.Header.VisiblePosition = 19;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 20;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 21;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 22;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.Format = "hh:mm tt";
            ultraGridColumn34.Header.Caption = "End Time";
            ultraGridColumn34.Header.VisiblePosition = 13;
            ultraGridColumn34.Width = 72;
            ultraGridColumn35.Header.VisiblePosition = 23;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.Header.VisiblePosition = 24;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 25;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Format = "MM/dd/yyyy";
            ultraGridColumn38.Header.Caption = "Sort Date";
            ultraGridColumn38.Header.VisiblePosition = 11;
            ultraGridColumn38.Width = 96;
            ultraGridColumn39.Header.VisiblePosition = 26;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn40.Header.VisiblePosition = 27;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.VisiblePosition = 28;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn42.Header.VisiblePosition = 29;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn43.Header.VisiblePosition = 30;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.Header.VisiblePosition = 31;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn45.Header.VisiblePosition = 32;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn46.Header.VisiblePosition = 33;
            ultraGridColumn46.Hidden = true;
            ultraGridColumn47.Header.VisiblePosition = 34;
            ultraGridColumn47.Hidden = true;
            ultraGridColumn48.Header.Caption = "Freight Type";
            ultraGridColumn48.Header.VisiblePosition = 17;
            ultraGridColumn48.Width = 96;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48});
            this.grdSortedItems.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdSortedItems.DisplayLayout.CaptionAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdSortedItems.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.grdSortedItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSortedItems.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortedItems.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortedItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdSortedItems.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.FontData.BoldAsString = "True";
            appearance8.FontData.Name = "Verdana";
            appearance8.FontData.SizeInPoints = 8F;
            appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
            appearance8.TextHAlignAsString = "Left";
            this.grdSortedItems.DisplayLayout.Override.HeaderAppearance = appearance8;
            this.grdSortedItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSortedItems.DisplayLayout.Override.MaxSelectedCells = 1;
            this.grdSortedItems.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.grdSortedItems.DisplayLayout.Override.RowAppearance = appearance9;
            this.grdSortedItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortedItems.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdSortedItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSortedItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSortedItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSortedItems.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSortedItems.Location = new System.Drawing.Point(3, 246);
            this.grdSortedItems.Name = "grdSortedItems";
            this.grdSortedItems.Size = new System.Drawing.Size(654, 156);
            this.grdSortedItems.TabIndex = 4;
            this.grdSortedItems.Text = "Sorted Items";
            this.grdSortedItems.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mSortedItemsDS
            // 
            this.mSortedItemsDS.DataSetName = "SortedItemDS";
            this.mSortedItemsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mSortedItemsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // splitterH
            // 
            this.splitterH.BackColor = System.Drawing.SystemColors.Control;
            this.splitterH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 406);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(664, 3);
            this.splitterH.TabIndex = 8;
            this.splitterH.TabStop = false;
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 409);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(664, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 11;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // pnlPickupDate
            // 
            this.pnlPickupDate.BackColor = System.Drawing.SystemColors.Window;
            this.pnlPickupDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlPickupDate.Controls.Add(this.pnlCalHeader);
            this.pnlPickupDate.Controls.Add(this.calPickupDate);
            this.pnlPickupDate.Location = new System.Drawing.Point(6, 6);
            this.pnlPickupDate.Margin = new System.Windows.Forms.Padding(0);
            this.pnlPickupDate.Name = "pnlPickupDate";
            this.pnlPickupDate.Size = new System.Drawing.Size(225, 183);
            this.pnlPickupDate.TabIndex = 16;
            this.pnlPickupDate.Visible = false;
            this.pnlPickupDate.Leave += new System.EventHandler(this.OnLeaveCalendar);
            this.pnlPickupDate.Enter += new System.EventHandler(this.OnEnterCalendar);
            // 
            // pnlCalHeader
            // 
            this.pnlCalHeader.Controls.Add(this.lblCloseCal);
            this.pnlCalHeader.Controls.Add(this.lblCalHeader);
            this.pnlCalHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCalHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlCalHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlCalHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlCalHeader.Name = "pnlCalHeader";
            this.pnlCalHeader.Padding = new System.Windows.Forms.Padding(2);
            this.pnlCalHeader.Size = new System.Drawing.Size(221, 22);
            this.pnlCalHeader.TabIndex = 119;
            this.pnlCalHeader.Leave += new System.EventHandler(this.OnLeaveCalendar);
            this.pnlCalHeader.Enter += new System.EventHandler(this.OnEnterCalendar);
            // 
            // lblCloseCal
            // 
            this.lblCloseCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseCal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblCloseCal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCloseCal.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblCloseCal.Location = new System.Drawing.Point(201, 3);
            this.lblCloseCal.Name = "lblCloseCal";
            this.lblCloseCal.Size = new System.Drawing.Size(16, 16);
            this.lblCloseCal.TabIndex = 115;
            this.lblCloseCal.Text = "X";
            this.lblCloseCal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseCal.Click += new System.EventHandler(this.OnCloseCalendar);
            this.lblCloseCal.Leave += new System.EventHandler(this.OnLeaveCalendar);
            this.lblCloseCal.Enter += new System.EventHandler(this.OnEnterCalendar);
            // 
            // lblCalHeader
            // 
            this.lblCalHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblCalHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCalHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCalHeader.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCalHeader.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblCalHeader.Location = new System.Drawing.Point(2, 2);
            this.lblCalHeader.Name = "lblCalHeader";
            this.lblCalHeader.Size = new System.Drawing.Size(217, 18);
            this.lblCalHeader.TabIndex = 113;
            this.lblCalHeader.Text = "Pickup Date";
            this.lblCalHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCalHeader.Leave += new System.EventHandler(this.OnLeaveCalendar);
            this.lblCalHeader.Enter += new System.EventHandler(this.OnEnterCalendar);
            // 
            // calPickupDate
            // 
            this.calPickupDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calPickupDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.calPickupDate.Location = new System.Drawing.Point(0, 0);
            this.calPickupDate.MaxSelectionCount = 1;
            this.calPickupDate.Name = "calPickupDate";
            this.calPickupDate.TabIndex = 14;
            this.calPickupDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.OnPickupDateSelected);
            this.calPickupDate.Leave += new System.EventHandler(this.OnLeaveCalendar);
            this.calPickupDate.Enter += new System.EventHandler(this.OnEnterCalendar);
            this.calPickupDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.OnPickupDateChanged);
            this.calPickupDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPickupDateKeyed);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlPickupDate);
            this.pnlTop.Controls.Add(this.grdPickups);
            this.pnlTop.Location = new System.Drawing.Point(3, 52);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(651, 188);
            this.pnlTop.TabIndex = 17;
            // 
            // mnuMain
            // 
            this.mnuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
            this.mnuMain.Location = new System.Drawing.Point(0, 0);
            this.mnuMain.Name = "mnuMain";
            this.mnuMain.Padding = new System.Windows.Forms.Padding(0);
            this.mnuMain.Size = new System.Drawing.Size(664, 24);
            this.mnuMain.TabIndex = 18;
            this.mnuMain.Text = "RDS Menu";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileSaveAs,
            this.mnuFileExport,
            this.mnuFileSep2,
            this.mnuFileSetup,
            this.mnuFilePrint,
            this.mnuFilePreview,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 24);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(152, 22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSaveAs.Text = "Save&As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.AddTable;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExport.Text = "Export...";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSetup.Text = "Page Setup...";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePrint.Text = "Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePreview.Text = "Print Preview...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
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
            this.mnuEditSearch});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 24);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCut.Text = "Cut";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCopy.Text = "Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(109, 22);
            this.mnuEditPaste.Text = "Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(106, 6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.Size = new System.Drawing.Size(109, 22);
            this.mnuEditSearch.Text = "Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewCartons,
            this.mnuViewSep1,
            this.mnuViewClientConfig,
            this.mnuViewSep2,
            this.mnuViewCalendar,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 24);
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(191, 22);
            this.mnuViewRefresh.Text = "Refresh Pickups";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewCartons
            // 
            this.mnuViewCartons.Image = global::Argix.Properties.Resources.BarCode;
            this.mnuViewCartons.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewCartons.Name = "mnuViewCartons";
            this.mnuViewCartons.Size = new System.Drawing.Size(191, 22);
            this.mnuViewCartons.Text = "Sorted Items";
            this.mnuViewCartons.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuViewClientConfig
            // 
            this.mnuViewClientConfig.Name = "mnuViewClientConfig";
            this.mnuViewClientConfig.Size = new System.Drawing.Size(191, 22);
            this.mnuViewClientConfig.Text = "Client Configuration...";
            this.mnuViewClientConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(188, 6);
            // 
            // mnuViewCalendar
            // 
            this.mnuViewCalendar.Image = global::Argix.Properties.Resources.Calendar_schedule;
            this.mnuViewCalendar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewCalendar.Name = "mnuViewCalendar";
            this.mnuViewCalendar.Size = new System.Drawing.Size(191, 22);
            this.mnuViewCalendar.Text = "Calendar";
            this.mnuViewCalendar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(191, 22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(191, 22);
            this.mnuViewStatusBar.Text = "StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 24);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.mnuToolsConfig.Text = "Configuration...";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 24);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(172, 22);
            this.mnuHelpAbout.Text = "About ISD Export...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(169, 6);
            // 
            // tlbMain
            // 
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnSave,
            this.btnExport,
            this.btnPrint,
            this.btnSep2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnSep3,
            this.btnFind,
            this.btnSep4,
            this.btnRefresh,
            this.btnSortedItems});
            this.tlbMain.Location = new System.Drawing.Point(0, 24);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(664, 25);
            this.tlbMain.TabIndex = 19;
            this.tlbMain.Text = "Main";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.ToolTipText = "New...";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.ToolTipText = "Open...";
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
            this.btnSave.ToolTipText = "Save...";
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.AddTable;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.ToolTipText = "Export sorted items...";
            this.btnExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.ToolTipText = "Print sorted items...";
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
            this.btnCut.ToolTipText = "Cut";
            this.btnCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::Argix.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.ToolTipText = "Copy";
            this.btnCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::Argix.Properties.Resources.Paste;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.ToolTipText = "Paste";
            this.btnPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnFind
            // 
            this.btnFind.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnFind.Image = global::Argix.Properties.Resources.Find;
            this.btnFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(23, 22);
            this.btnFind.ToolTipText = "Search...";
            this.btnFind.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep4
            // 
            this.btnSep4.Name = "btnSep4";
            this.btnSep4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.ToolTipText = "Refresh pickups";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSortedItems
            // 
            this.btnSortedItems.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSortedItems.Image = global::Argix.Properties.Resources.BarCode;
            this.btnSortedItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSortedItems.Name = "btnSortedItems";
            this.btnSortedItems.Size = new System.Drawing.Size(23, 22);
            this.btnSortedItems.ToolTipText = "Display sorted items";
            this.btnSortedItems.Click += new System.EventHandler(this.OnItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(664, 433);
            this.Controls.Add(this.tlbMain);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.grdSortedItems);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.mnuMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mnuMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inbound Scan Data Export";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdPickups)).EndInit();
            this.grdPickups.ResumeLayout(false);
            this.ctxMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mPickupsDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSortedItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortedItemsDS)).EndInit();
            this.pnlPickupDate.ResumeLayout(false);
            this.pnlCalHeader.ResumeLayout(false);
            this.pnlTop.ResumeLayout(false);
            this.mnuMain.ResumeLayout(false);
            this.mnuMain.PerformLayout();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
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
                    this.mnuViewToolbar.Checked = this.tlbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				this.mToolTip.SetToolTip(this.dtpPickupDate, "Select a date for pickups.");
				#endregion
				
				//Set control defaults
				#region Grid Overrides
				this.grdPickups.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdPickups.DisplayLayout.Bands[0].Columns["ClientNumber"].SortIndicator = SortIndicator.Ascending;
				this.grdSortedItems.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdSortedItems.DisplayLayout.Bands[0].Columns["LABEL_SEQ_NUMBER"].SortIndicator = SortIndicator.Ascending;
				#endregion
				this.calPickupDate.MaxSelectionCount = 1;
				this.calPickupDate.MinDate = this.dtpPickupDate.MinDate = DateTime.Today.AddDays(-App.Config.DateDaysBack);
				this.calPickupDate.MaxDate = DateTime.Today;
				this.dtpPickupDate.MaxDate = this.dtpPickupDate.Value = DateTime.Today;
				this.mnuViewCalendar.Checked = false;
				this.mnuViewCalendar.PerformClick();
				this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for form size changes
			try {
				if(this.WindowState != FormWindowState.Minimized) {
					//Resize bottom panel to keep top panel visible; when bottom panel 
					//reaches minimum height, then eat the top panel
					if(this.grdSortedItems.Height <= this.splitterH.MinSize)
						this.grdSortedItems.Height = this.splitterH.MinSize;
					else if(this.pnlTop.Height < this.splitterH.MinExtra)
						this.grdSortedItems.Height = (this.ClientSize.Height - this.tlbMain.Height - this.stbMain.Height) - this.splitterH.MinExtra;
				}
			} 
			catch(Exception) { }
		}
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Ask only if there are detail forms open
            if(!e.Cancel) {
                #region Save user preferences
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
                ArgixTrace.WriteLine(new TraceMessage(App.Version,App.Product,LogLevel.Information,"App Stopped"));
            }
        }
		#region Calendar Support: OnCalendarOpened(), OnCalendarClosed(), OnCalendarValueChanged()
		private void OnCalendarOpened(object sender, System.EventArgs e) {
			//Event handler for calendar dropped down
			this.mCalendarOpen = true;
		}
		private void OnCalendarClosed(object sender, System.EventArgs e) {
			//Event handler for date picker calendar closed
			try {
				//Allow calendar to close
				this.dtpPickupDate.Refresh();
				Application.DoEvents();
				
				//Flag calendar as closed; sync calendars & change terminal pickup date
				this.mCalendarOpen = false;
				this.calPickupDate.SetDate(this.dtpPickupDate.Value);
                this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnCalendarValueChanged(object sender, System.EventArgs e) {
			//Event handler for pickup date changed
			try {
				//Sync calendars & change terminal pickup date if the calendar is closed
				if(!this.mCalendarOpen) {
					this.calPickupDate.SetDate(this.dtpPickupDate.Value);
					this.mnuViewRefresh.PerformClick();
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
		#region DateTimePicker Support: OnPickupDateChanged(), OnPickupDateSelected(), OnPickupDateKeyed()
		private void OnPickupDateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			//Event handler for pickup date changed
            setUserServices();
		}
		private void OnPickupDateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			//Event handler for pickup date selected
			try {
				this.dtpPickupDate.Value = this.calPickupDate.SelectionRange.Start;
			}
			catch(Exception ex) { App.ReportError(ex); }
        }
		private void OnPickupDateKeyed(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for key down in the calendar
			try {
				if(e.KeyCode == Keys.Enter)
					this.dtpPickupDate.Value = this.calPickupDate.SelectionRange.Start;
			}
			catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnCloseCalendar(object sender,System.EventArgs e) {
            //Event handler to close routes window
            this.mnuViewCalendar.PerformClick();
            setUserServices();
        }
        private void OnEnterCalendar(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblCalHeader.BackColor = this.lblCloseCal.BackColor = SystemColors.ActiveCaption;
                this.lblCalHeader.ForeColor = this.lblCloseCal.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnLeaveCalendar(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblCalHeader.BackColor = this.lblCloseCal.BackColor = SystemColors.InactiveCaption;
                this.lblCalHeader.ForeColor = this.lblCloseCal.ForeColor = SystemColors.InactiveCaptionText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion
		private void OnPickupSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected pickup
			try {
				//Clear reference to prior pickup object
				this.mPickup = null;
                this.mSortedItemsDS.Clear();
                if(this.grdPickups.Selected.Rows.Count > 0) {
					//Get a pickup object for the selected pickup
					this.grdPickups.Focus();
					string id = this.grdPickups.Selected.Rows[0].Cells["ID"].Value.ToString();
                    PickupDS.PickupTableRow row = (PickupDS.PickupTableRow)this.mPickupsDS.PickupTable.Select("ID='" + id + "'")[0];
                    this.mPickup = new Pickup(row);
				}
			} 
			catch(Exception ex) { App.ReportError(ex); } finally { setUserServices(); }
            this.mnuViewCartons.PerformClick();
        }
		private void OnSortedItemSelected(object sender, System.EventArgs e) {
			//Event handler for sorted item selection
			setUserServices();
        }
        #region User Services: OnItemClick(), OnHelpItemClick(), OnDataStatusUpdate()
        private void OnItemClick(object sender,EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
					case "mnuFileNew":
                    case "btnNew": 
                        break;
					case "mnuFileOpen":		
	                case "btnOpen":
                        break;
                    case "mnuFileSave":
                    case "btnSave": 
                        break;
                    case "mnuFileSaveAs":		
                        break;
					case "mnuFileExport":       
                    case "btnExport": 
                        exportSortedItems(); 
                        break;
					case "mnuFileSetup":	    UltraGridPrinter.PageSettings(); break;
					case "mnuFilePrint":		UltraGridPrinter.Print(this.grdSortedItems, "Sorted Items", true); break;
                    case "btnPrint":            UltraGridPrinter.Print(this.grdSortedItems,"Sorted Items",false); break;
                    case "mnuFilePreview": UltraGridPrinter.PrintPreview(this.grdSortedItems,"Sorted Items"); break;
					case "mnuFileExit":			this.Close(); Application.Exit(); break;
					case "mnuEditCut":		
	                case "btnCut": 
                    case "ctxCut": 
                        break;
					case "mnuEditCopy":			
                    case "btnCopy": 
                    case "ctxCuopy": 
                        break;
					case "mnuEditPaste":		
                    case "btnPaste": 
                    case "ctxPaste": 
                        break;
					case "mnuEditSearch":		
                    case "btnFind": 
                        break;
					case "mnuViewRefresh":
                    case "btnRefresh": 						
                        this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Refreshing pickups...");
                        this.grdPickups.Text = "Pickups for  " + this.dtpPickupDate.Value.ToShortDateString();
                        this.mPickupsDS.Clear();
                        this.mPickupsDS.Merge(EnterpriseFactory.GetPickups(this.dtpPickupDate.Value));
                        if(this.grdPickups.Rows.Count > 0) 
                            this.grdPickups.Rows[0].Selected = true;
                        else
                            OnPickupSelected(null,null);
                        this.mMessageMgr.AddMessage(this.mPickupsDS.PickupTable.Rows.Count.ToString() + " pickups for " + this.dtpPickupDate.Value.ToShortDateString() + ".");
                        break;
					case "mnuViewCartons": 
                    case "btnSortedItems": 
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Requesting sorted items for pickup#" + this.mPickup.ID + "....");
                        this.mSortedItemsDS.Clear();
                        DataSet ds = EnterpriseFactory.GetSortedItems(this.mPickup.ID);
                        if(ds != null) this.mSortedItemsDS.Merge(ds,false,MissingSchemaAction.Ignore);
                        this.mMessageMgr.AddMessage(this.mSortedItemsDS.SortedItemTable.Rows.Count.ToString() + " sorted items.");
                        break;
                    case "mnuViewClientConfig":
                        new dlgClientConfig().ShowDialog(this);
                        break;
					case "mnuViewCalendar": 
						this.pnlPickupDate.Visible = (this.mnuViewCalendar.Checked = (!this.mnuViewCalendar.Checked)); 
						this.dtpPickupDate.Visible = (!this.pnlPickupDate.Visible); 
						break;
					case "mnuViewToolbar":			this.tlbMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
					case "mnuViewStatusBar":		this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuToolsConfig":          App.ShowConfig(); break;
					case "mnuHelpAbout":            new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpItemClick(object sender, System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                Help.ShowHelp(this, this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch (Exception) { }
        }
        private void OnDataStatusUpdate(object sender, DataStatusArgs e) {
            //Event handler for notifications from mediator
            this.stbMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(e.Online, e.Connection));
        }
        #endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu()
		private void configApplication() {
			try {
                //Create event log and database trace listeners, and log application as started
                try {
                    ArgixTrace.AddListener(new DBTraceListener((LogLevel)App.Config.TraceLevel, App.Mediator, App.USP_TRACE, App.EventLogName));
                }
                catch {
                    ArgixTrace.AddListener(new DBTraceListener(LogLevel.Debug,App.Mediator,App.USP_TRACE,App.EventLogName));
                }
                
                //Create business objects with configuration values
                App.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                this.stbMain.SetTerminalPanel(App.Mediator.TerminalID.ToString(),App.Mediator.Description);
            }
			catch(ApplicationException ex) { throw ex; } 
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {
                this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
                this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
                this.mnuFileSaveAs.Enabled = this.btnSave.Enabled = false;
                this.mnuFileExport.Enabled = this.btnExport.Enabled = (!App.Config.ReadOnly && this.mPickup != null && this.mSortedItemsDS.SortedItemTable.Count > 0);
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.mPickup != null && this.mSortedItemsDS.SortedItemTable.Count > 0);
                this.mnuFilePreview.Enabled = (this.mPickup != null && this.mSortedItemsDS.SortedItemTable.Count > 0);
				this.mnuFileExit.Enabled = true;
                this.mnuEditCut.Enabled = this.ctxCut.Enabled = this.btnCut.Enabled = false;
                this.mnuEditCopy.Enabled = this.ctxCopy.Enabled = this.btnCopy.Enabled = false;
                this.mnuEditPaste.Enabled = this.ctxPaste.Enabled = this.btnPaste.Enabled = false;
				this.mnuEditSearch.Enabled = this.btnFind.Enabled = false;
				this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewCartons.Enabled = this.btnSortedItems.Enabled = (this.mPickup != null);
                this.mnuViewClientConfig.Enabled = true;
				this.mnuViewCalendar.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.User1Panel.Icon = null;
                this.stbMain.User1Panel.ToolTipText = "";
                if(this.mSortedItemsDS.SortedItemTable.Rows.Count > 0) {
                    this.stbMain.User1Panel.Icon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.sorteditem.ico"));
                    this.stbMain.User1Panel.ToolTipText = this.mSortedItemsDS.SortedItemTable.Rows.Count.ToString() + " sorted items.";
                }
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
				this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
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
                    //item.Name = "mnuHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpItemClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
			}
			catch(Exception) { }
		}
		#endregion
        private void exportSortedItems() {
            //Exports sorted items dataset to a file specified by the database
            try {
                //Get filename and export type
                Exporter exporter = null;
                ISDClientDS ds = EnterpriseFactory.GetClients(this.mPickup.ClientNumber);
                for (int i = 0; i < ds.ExportTable.Rows.Count; i++) {
                    string exportType = ds.ExportTable[i].ExportFormat.Trim().ToLower();
                    switch (exportType.ToLower()) {
                        case "rds3": exporter = new RDS3Exporter(); break;
                        case "rds4": exporter = new RDS4Exporter(); break;
                        case "pcs": exporter = new PCSExporter(); break;
                        default:
                            throw new ApplicationException(exportType + " is an unknown export format.");
                    }
                    string file = ds.ExportTable[i].ExportPath + EnterpriseFactory.GetExportFilename(ds.ExportTable[i].CounterKey);
                    string client = ds.ExportTable[i].Client;
                    string scanner = ds.ExportTable[i].Scanner;
                    string userID = ds.ExportTable[i].UserID;

                    bool exported = exporter.Export(file, client, scanner, userID, this.mPickup, this.mSortedItemsDS);
                    if (exported)
                        this.mMessageMgr.AddMessage(this.mSortedItemsDS.SortedItemTable.Count.ToString() + " records exported to " + file + ".");
                }
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while exporting cartons.", ex); }
        }
    }
}