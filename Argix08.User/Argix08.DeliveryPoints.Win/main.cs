using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix;
using Argix.Windows;

namespace Argix.Terminals {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members		
        private DateTime mStartDate=DateTime.Today, mLastUpated=DateTime.Today;
        private string mFilename="";
		private bool mCalendarOpen=false;
		private bool mGridDirty=false;
        private RoadshowDS mCustomers = null;
		
        private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		
        #region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        //		private System.Windows.Forms.MenuItem mnuHelpContents;
        private Argix.Windows.ArgixStatusBar stbMain;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnSave;
        private ToolStripButton btnExport;
        private ToolStripButton btnPrint;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripSeparator btnSep3;
        private ToolStripButton btnSearch;
        private ToolStripSeparator btnSep4;
        private ToolStripButton btnRefresh;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripMenuItem mnuFileExport;
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
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuToolsConfig;
        private ContextMenuStrip cmsMain;
        private ToolStripMenuItem cmsCut;
        private ToolStripMenuItem cmsCopy;
        private ToolStripMenuItem cmsPaste;
        private BindingSource mDeliveryPoints;
		private System.ComponentModel.IContainer components;
		#endregion
		
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
				this.tsMain.Dock = DockStyle.Top;
				this.Controls.AddRange(new Control[]{this.grdMain, this.tsMain, this.msMain, this.stbMain});
				this.grdMain.Controls.AddRange(new Control[]{this.dtpStartDate});
				#endregion
				
				//Create data and UI services
				this.mGridSvc = new UltraGridSvc(this.grdMain);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeliveryPoint", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Account");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Appt");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Building");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Command");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NickName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Phone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Route");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceTimeFactor");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SetupTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopAddress");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCity");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopComment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNickName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpen");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopPhone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopState");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopZip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Unit");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.mDeliveryPoints = new System.Windows.Forms.BindingSource(this.components);
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.tsMain = new System.Windows.Forms.ToolStrip();
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
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
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
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.grdMain.SuspendLayout();
            this.cmsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mDeliveryPoints)).BeginInit();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.CausesValidation = false;
            this.grdMain.ContextMenuStrip = this.cmsMain;
            this.grdMain.Controls.Add(this.dtpStartDate);
            this.grdMain.DataSource = this.mDeliveryPoints;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.ColHeaderLines = 3;
            ultraGridColumn1.Header.Caption = "Customer \r\nAccount#";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 96;
            ultraGridColumn2.Header.Caption = "Customer \r\nComment";
            ultraGridColumn2.Header.VisiblePosition = 13;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.Caption = "Customer \r\nBuilding";
            ultraGridColumn3.Header.VisiblePosition = 4;
            ultraGridColumn3.Width = 144;
            ultraGridColumn4.Header.Caption = "Customer \r\nWindow \r\nClose";
            ultraGridColumn4.Header.VisiblePosition = 9;
            ultraGridColumn4.Width = 60;
            ultraGridColumn5.Header.Caption = "Import \r\nCommand";
            ultraGridColumn5.Header.VisiblePosition = 0;
            ultraGridColumn5.Width = 72;
            ultraGridColumn6.Format = "MM/dd/yyyy hh:mm tt";
            ultraGridColumn6.Header.Caption = "Last \r\nUpdated";
            ultraGridColumn6.Header.VisiblePosition = 25;
            ultraGridColumn6.Width = 144;
            ultraGridColumn7.Header.Caption = "Customer \r\nName";
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridColumn7.Width = 144;
            ultraGridColumn8.Header.Caption = "Customer \r\nNickname";
            ultraGridColumn8.Header.VisiblePosition = 2;
            ultraGridColumn8.Width = 72;
            ultraGridColumn9.Format = "MM/dd/yyyy";
            ultraGridColumn9.Header.Caption = "Customer \r\nOpen \r\nDate";
            ultraGridColumn9.Header.VisiblePosition = 7;
            ultraGridColumn9.Width = 72;
            ultraGridColumn10.Header.Caption = "Customer \r\nWindow \r\nOpen";
            ultraGridColumn10.Header.VisiblePosition = 8;
            ultraGridColumn10.Width = 60;
            ultraGridColumn11.Header.Caption = "Customer \r\nPhone#";
            ultraGridColumn11.Header.VisiblePosition = 6;
            ultraGridColumn11.Width = 84;
            ultraGridColumn12.Header.Caption = "Customer \r\nRoute";
            ultraGridColumn12.Header.VisiblePosition = 5;
            ultraGridColumn12.Width = 96;
            ultraGridColumn13.Header.Caption = "Customer \r\nSvc Time \r\nFactor";
            ultraGridColumn13.Header.VisiblePosition = 10;
            ultraGridColumn13.Width = 60;
            ultraGridColumn14.Header.Caption = "Customer \r\nSetup";
            ultraGridColumn14.Header.VisiblePosition = 12;
            ultraGridColumn14.Width = 48;
            ultraGridColumn15.Header.VisiblePosition = 24;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.Caption = "Stop \r\nAddress";
            ultraGridColumn16.Header.VisiblePosition = 17;
            ultraGridColumn16.Width = 120;
            ultraGridColumn17.Header.Caption = "Stop \r\nCity";
            ultraGridColumn17.Header.VisiblePosition = 18;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.Caption = "Stop \r\nClose";
            ultraGridColumn18.Header.VisiblePosition = 22;
            ultraGridColumn18.Width = 48;
            ultraGridColumn19.Header.Caption = "Stop \r\nComment";
            ultraGridColumn19.Header.VisiblePosition = 23;
            ultraGridColumn19.Width = 96;
            ultraGridColumn20.Header.Caption = "Stop \r\nName";
            ultraGridColumn20.Header.VisiblePosition = 14;
            ultraGridColumn20.Width = 144;
            ultraGridColumn21.Header.Caption = "Stop \r\nNickName";
            ultraGridColumn21.Header.VisiblePosition = 15;
            ultraGridColumn21.Width = 72;
            ultraGridColumn22.Header.Caption = "Stop \r\nOpen";
            ultraGridColumn22.Header.VisiblePosition = 21;
            ultraGridColumn22.Width = 48;
            ultraGridColumn23.Header.Caption = "Stop \r\nPhone";
            ultraGridColumn23.Header.VisiblePosition = 16;
            ultraGridColumn23.Width = 96;
            ultraGridColumn24.Header.Caption = "Stop \r\nState";
            ultraGridColumn24.Header.VisiblePosition = 19;
            ultraGridColumn24.Width = 48;
            ultraGridColumn25.Header.Caption = "Stop \r\nZip";
            ultraGridColumn25.Header.VisiblePosition = 20;
            ultraGridColumn25.Width = 60;
            ultraGridColumn26.Header.Caption = "Customer \r\nType \r\nUnit";
            ultraGridColumn26.Header.VisiblePosition = 11;
            ultraGridColumn26.Width = 48;
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
            ultraGridColumn13,
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
            ultraGridColumn26});
            this.grdMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.CaptionAppearance = appearance2;
            this.grdMain.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdMain.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            appearance3.TextVAlignAsString = "Top";
            this.grdMain.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdMain.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdMain.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdMain.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdMain.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdMain.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdMain.Location = new System.Drawing.Point(0, 49);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(664, 179);
            this.grdMain.TabIndex = 1;
            this.grdMain.Text = "Delivery Points";
            this.grdMain.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdMain.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnCellChanged);
            this.grdMain.AfterEnterEditMode += new System.EventHandler(this.OnGridAfterEnterEditMode);
            this.grdMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnInitializeRow);
            this.grdMain.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdMain.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnCellActivating);
            this.grdMain.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnBeforeRowFilterDropDownPopulate);
            this.grdMain.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnCellChanged);
            this.grdMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            this.grdMain.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsCut,
            this.cmsCopy,
            this.cmsPaste});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(103, 70);
            // 
            // cmsCut
            // 
            this.cmsCut.Name = "cmsCut";
            this.cmsCut.Size = new System.Drawing.Size(102, 22);
            this.cmsCut.Text = "Cu&t";
            this.cmsCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // cmsCopy
            // 
            this.cmsCopy.Name = "cmsCopy";
            this.cmsCopy.Size = new System.Drawing.Size(102, 22);
            this.cmsCopy.Text = "&Copy";
            this.cmsCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // cmsPaste
            // 
            this.cmsPaste.Name = "cmsPaste";
            this.cmsPaste.Size = new System.Drawing.Size(102, 22);
            this.cmsPaste.Text = "&Paste";
            this.cmsPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.CausesValidation = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtpStartDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(507, 1);
            this.dtpStartDate.MaxDate = new System.DateTime(2031, 12, 31, 0, 0, 0, 0);
            this.dtpStartDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowUpDown = true;
            this.dtpStartDate.Size = new System.Drawing.Size(156, 21);
            this.dtpStartDate.TabIndex = 2;
            this.dtpStartDate.DropDown += new System.EventHandler(this.OnCalendarOpened);
            this.dtpStartDate.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            // 
            // mDeliveryPoints
            // 
            this.mDeliveryPoints.DataSource = typeof(Argix.Terminals.DeliveryPoints);
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 228);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(664, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 11;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
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
            this.btnSearch,
            this.btnSep4,
            this.btnRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(664, 25);
            this.tsMain.TabIndex = 13;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.Document;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.ToolTipText = "New delivery points file";
            this.btnNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.ToolTipText = "Open and import an existing delivery points file";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClicked);
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
            this.btnSave.ToolTipText = "Save the current delivery points to a user specified file";
            this.btnSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.ToolTipText = "Export the current delivery points to the export file and update the LastUpdated " +
                "time";
            this.btnExport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.ToolTipText = "Print the current delivery points";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClicked);
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
            this.btnCut.ToolTipText = "Cut the selected text";
            this.btnCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::Argix.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.ToolTipText = "Copy the selected text";
            this.btnCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::Argix.Properties.Resources.Paste;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.ToolTipText = "Paste text from the clipboard to the selected cell";
            this.btnPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Search;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.ToolTipText = "Search";
            this.btnSearch.Click += new System.EventHandler(this.OnItemClicked);
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
            this.btnRefresh.ToolTipText = "Refresh the delivery points";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(664, 24);
            this.msMain.TabIndex = 14;
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
            this.mnuFilePreview,
            this.mnuFileSep3,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.Document;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(154, 22);
            this.mnuFileNew.Text = "&New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(154, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(154, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(154, 22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(154, 22);
            this.mnuFileExport.Text = "&Export";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuFileSettings
            // 
            this.mnuFileSettings.Image = global::Argix.Properties.Resources.PageSetup;
            this.mnuFileSettings.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuFileSettings.Name = "mnuFileSettings";
            this.mnuFileSettings.Size = new System.Drawing.Size(154, 22);
            this.mnuFileSettings.Text = "Page Settings...";
            this.mnuFileSettings.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(154, 22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(154, 22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(154, 22);
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
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCut.Text = "Cut";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCopy.Text = "Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.Size = new System.Drawing.Size(109, 22);
            this.mnuEditPaste.Text = "Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(106, 6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Search;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.Size = new System.Drawing.Size(109, 22);
            this.mnuEditSearch.Text = "Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
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
            this.mnuViewRefresh.Size = new System.Drawing.Size(123, 22);
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(123, 22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(123, 22);
            this.mnuViewStatusBar.Text = "StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClicked);
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
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.mnuToolsConfig.Text = "Configuration...";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(197, 22);
            this.mnuHelpAbout.Text = "&About Delivery Points...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(194, 6);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(664, 252);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Delivery Points";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.grdMain.ResumeLayout(false);
            this.cmsMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mDeliveryPoints)).EndInit();
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
                    this.mnuViewToolbar.Checked = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
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
				this.mToolTip.SetToolTip(this.dtpStartDate, "Select a start date for the oldest delivery point.");
				#endregion
				
				//Set control defaults
				#region Grid Overrides
				this.grdMain.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdMain.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdMain.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdMain.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdMain.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
				this.grdMain.DisplayLayout.Bands[0].Columns["Command"].AllowRowFiltering = DefaultableBoolean.True;
				this.grdMain.DisplayLayout.Bands[0].Columns["NickName"].CellActivation = Activation.AllowEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["OpenDate"].CellActivation = Activation.NoEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["ServiceTimeFactor"].CellActivation = Activation.AllowEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["Unit"].CellActivation = Activation.AllowEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["SetupTime"].CellActivation = Activation.AllowEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["StopNickName"].CellActivation = Activation.AllowEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["StopOpen"].CellActivation = Activation.AllowEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["StopClose"].CellActivation = Activation.AllowEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["LastUpdated"].CellActivation = Activation.NoEdit;
				this.grdMain.DisplayLayout.Bands[0].Columns["Account"].SortIndicator = SortIndicator.Descending;
				#endregion

                try {
                    //Set start date for delivery point records to last updated datetime
                    this.mLastUpated = DeliveryPointsProxy.GetExportDate();
                } catch { }
                this.mStartDate = this.mLastUpated;
               
                this.dtpStartDate.MinDate = new DateTime(1990,1,1,0,0,0,0);
				this.dtpStartDate.MaxDate = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,23,59,59,999);
				this.dtpStartDate.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
				this.dtpStartDate.Value = this.mStartDate;
                TerminalInfo t = DeliveryPointsProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.stbMain.User1Panel.Width = 144;
                this.stbMain.User2Panel.Width = 32;

                this.mCustomers = RoadshowGateway.GetCustomers();
                this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { 
			//Event handler for form resized event
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
        #region Calendar Support: OnCalendarOpened(), OnCalendarClosed(), OnCalendarValueChanged()
		private void OnCalendarOpened(object sender, System.EventArgs e) {
			//Event handler for calendar dropped down
			this.mCalendarOpen = true;
		}
		private void OnCalendarClosed(object sender, System.EventArgs e) {
			//Event handler for date picker calendar closed
			try {
				//Allow calendar to close
				this.dtpStartDate.Refresh();
				Application.DoEvents();
				
				//Flag calendar as closed; sync calendars & change terminal pickup date
				this.mCalendarOpen = false;
                OnCalendarValueChanged(null,EventArgs.Empty);
            }
			catch(Exception ex) { App.ReportError(ex); }
        }
		private void OnCalendarValueChanged(object sender, System.EventArgs e) {
			//Event handler for pickup date changed
			try {
				//Change terminal start date if the calendar is closed
                if(!this.mCalendarOpen) {
                    if(DateTime.Compare(this.dtpStartDate.Value,this.mStartDate) != 0) {
                        DialogResult refresh = DialogResult.Yes;
				        if(this.mGridDirty) 
					        refresh = MessageBox.Show(this, "The current data has been modified. Do you want to overwrite these changes?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				        if(refresh == DialogResult.Yes) {
                            this.mStartDate = this.dtpStartDate.Value;
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Refreshing...");
                            this.mDeliveryPoints.DataSource = DeliveryPointsProxy.GetDeliveryPoints(this.mStartDate,this.mLastUpated);
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mDeliveryPoints.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                        }
                        else {
                            this.dtpStartDate.ValueChanged -= new System.EventHandler(this.OnCalendarValueChanged);
                            this.dtpStartDate.Value = this.mStartDate;
                            this.dtpStartDate.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
                        }
                    }
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
        #region Grid Support: OnInitializeRow(), OnBeforeRowFilterDropDownPopulate(), OnGridSelectionChanged(), OnGridMouseDown(), OnCellActivating(), OnGridAfterEnterEditMode(), OnGridKeyUp(), OnCellChanged(), OnGridAfterRowUpdate()
        private void OnInitializeRow(object sender, InitializeRowEventArgs e) {
            //Event handler for intialize row event
            try {
                string accountID = e.Row.Cells["Account"].Value.ToString();
                RoadshowDS.CustomerTableRow customer = null;
                try {
                    customer = (RoadshowDS.CustomerTableRow)this.mCustomers.CustomerTable.Select("AccountID = '" + accountID + "'")[0];
                }
                catch { }
                if (customer != null) {
                    if (e.Row.Cells["Building"].Value.ToString().Trim() != customer.CustomerAddress.Trim()) e.Row.Cells["Building"].Appearance.BackColor = Color.Red;
                    if (int.Parse(e.Row.Cells["OpenTime"].Value.ToString()) != customer.CustomerWindowOpen) e.Row.Cells["OpenTime"].Appearance.BackColor = Color.Red;
                    if (int.Parse(e.Row.Cells["CloseTime"].Value.ToString()) != customer.CustomerWindowClose) e.Row.Cells["CloseTime"].Appearance.BackColor = Color.Red;

                    if (e.Row.Cells["StopAddress"].Value.ToString().Trim() != customer.StopAddress.Trim()) e.Row.Cells["StopAddress"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["StopCity"].Value.ToString().Trim() != customer.StopCity.Trim()) e.Row.Cells["StopCity"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["StopState"].Value.ToString().Trim() != customer.StopState.Trim()) e.Row.Cells["StopState"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["StopZip"].Value.ToString().Trim() != customer.StopZip.Trim()) e.Row.Cells["StopZip"].Appearance.BackColor = Color.Red;
                    if (int.Parse(e.Row.Cells["StopOpen"].Value.ToString()) != customer.StopWindowOpen) e.Row.Cells["StopOpen"].Appearance.BackColor = Color.Red;
                    if (int.Parse(e.Row.Cells["StopClose"].Value.ToString()) != customer.StopWindowClose) e.Row.Cells["StopClose"].Appearance.BackColor = Color.Red;
                }
                else
                    e.Row.Appearance.BackColor = Color.Yellow;
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Event handler for before row filter drop down populates
            try {
                //Removes only blanks and non-blanks from default filter
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for selection change
            setUserServices();
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            //UltraGridRow oRow = (UltraGridRow)oContext;
                            //if(!oRow.Selected) grid.Selected.Rows.Clear();
                            //oRow.Selected = true;
                            //oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnCellActivating(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for cell activated
            try {
                //Set cell editing
                switch(e.Cell.Column.Key.ToString()) {
                    case "NickName":
                    case "ServiceTimeFactor":
                    case "Unit":
                    case "SetupTime":
                    case "StopNickName":
                    case "StopOpen":
                    case "StopClose":
                        e.Cell.Activation = Activation.AllowEdit;
                        break;
                    default:
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); }
        }
        private void OnGridAfterEnterEditMode(object sender,System.EventArgs e) {
            //Event handler for 
            setUserServices();
        }
        private void OnGridKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            if(e.KeyCode == Keys.Enter) {
                //Update row on Enter
                this.grdMain.ActiveRow.Update();
                e.Handled = true;
            }
        }
        private void OnCellChanged(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a cell value
            try {
                //Flag data as dirty
                this.mGridDirty = true;

                //Apply cell rules
                switch(e.Cell.Column.Key.ToString()) {
                    case "NickName":
                    case "StopNickName":
                        //Max 8 chars (i.e. 3 char mnemonic plus 5 char store#)
                        if(e.Cell.Text.Length > 8) {
                            e.Cell.Value = e.Cell.Text.Substring(0,8);
                            e.Cell.SelStart = 8;
                        }
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) { 
            //
            this.grdMain.Update();
        }
        #endregion
        #region User Services: OnItemClicked(), OnHelpMenuClick()
        private void OnItemClicked(object sender,EventArgs e) {
            //Event handler for manu item clicked
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "mnuFileNew":
                    case "btnNew":
                        break;
                    case "mnuFileOpen":
                    case "btnOpen":
                        DialogResult open = DialogResult.Yes;
                        if(this.mGridDirty)
                            open = MessageBox.Show(this,"The current data has been modified. Do you want to overwrite these changes?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if(open == DialogResult.Yes) {
                            OpenFileDialog dlgOpen = new OpenFileDialog();
                            dlgOpen.AddExtension = true;
                            dlgOpen.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                            dlgOpen.FilterIndex = 0;
                            dlgOpen.Title = "Open " + App.Product + " File...";
                            dlgOpen.FileName = App.Config.ExportFile;
                            if(dlgOpen.ShowDialog(this)==DialogResult.OK) {
                                //Open file
                                this.Cursor = Cursors.WaitCursor;
                                this.mDeliveryPoints.Clear();
                                this.mDeliveryPoints.DataSource = importDeliveryPoints(dlgOpen.FileName);
                                this.mGridDirty = false;
                                this.mMessageMgr.AddMessage(this.mDeliveryPoints.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                                this.grdMain.Refresh();
                            }
                        }
                        break;
                    case "mnuFileSave":
                    case "btnSave":
                        if(this.mFilename.Length > 0) {
                            this.Cursor = Cursors.WaitCursor;
                            exportDeliveryPoints(this.mFilename,this.mLastUpated);
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mDeliveryPoints.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                            this.mMessageMgr.AddMessage("Saved to " + this.mFilename + ".");
                        }
                        break;
                    case "mnuFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Text Files (*.txt) | *.txt";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save " + App.Product + " As...";
                        dlgSave.FileName = (this.mFilename.Length > 0) ? this.mFilename : App.Config.ExportFile;
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mFilename = dlgSave.FileName;
                            exportDeliveryPoints(this.mFilename,this.mLastUpated);
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mDeliveryPoints.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                            this.mMessageMgr.AddMessage("Saved to " + this.mFilename + ".");
                        }
                        break;
                    case "mnuFileExport":
                    case "btnExport":
                        this.Cursor = Cursors.WaitCursor;
                        try {
                            this.mLastUpated = exportDeliveryPoints("",this.mLastUpated);
                            DeliveryPointsProxy.UpdateExportDate(this.mLastUpated);
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mDeliveryPoints.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                            this.mMessageMgr.AddMessage("Delivery points exported to " + App.Config.ExportFile + ".");
                        }
                        catch(Exception ex) { App.ReportError(ex); }
                        break;
                    case "mnuFileSettings": UltraGridPrinter.PageSettings(); break;
                    case "mnuFilePreview": UltraGridPrinter.PrintPreview(this.grdMain,"Delivery Points"); break;
                    case "mnuFilePrint": UltraGridPrinter.Print(this.grdMain,"Delivery Points",true); break;
                    case "btnPrint": UltraGridPrinter.Print(this.grdMain,"Delivery Points",false); break;
                    case "mnuFileExit": this.Close(); Application.Exit(); break;
                    case "mnuEditCut":
                    case "cmsCut":
                    case "btnCut":
                        Clipboard.SetDataObject(this.grdMain.ActiveCell.SelText,false);
                        this.grdMain.ActiveCell.Value = this.grdMain.ActiveCell.Text.Remove(this.grdMain.ActiveCell.SelStart,this.grdMain.ActiveCell.SelLength);
                        break;
                    case "mnuEditCopy":
                    case "cmsCopy":
                    case "btnCopy":
                        Clipboard.SetDataObject(this.grdMain.ActiveCell.SelText,false);
                        break;
                    case "mnuEditPaste":
                    case "cmsPaste":
                    case "btnPaste":
                        IDataObject o = Clipboard.GetDataObject();
                        this.grdMain.ActiveCell.Value = this.grdMain.ActiveCell.Text.Remove(this.grdMain.ActiveCell.SelStart,this.grdMain.ActiveCell.SelLength).Insert(this.grdMain.ActiveCell.SelStart,(string)o.GetData("Text"));
                        break;
                    case "mnuEditSearch": 
                    case "btnSearch":
                        break;
                    case "mnuViewRefresh":
                    case "btnRefresh":
                        //Refresh pickups collection
                        DialogResult refresh = DialogResult.Yes;
                        if(this.mGridDirty)
                            refresh = MessageBox.Show(this,"The current data has been modified. Do you want to overwrite these changes?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if(refresh == DialogResult.Yes) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Refreshing...");
                            this.mDeliveryPoints.Clear();
                            this.mDeliveryPoints.DataSource = DeliveryPointsProxy.GetDeliveryPoints(this.mStartDate,this.mLastUpated);
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mDeliveryPoints.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                        }
                        break;
                    case "mnuViewToolbar": this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
                    case "mnuViewStatusBar": this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuToolsConfig": App.ShowConfig(); break;
                    case "mnuHelpAbout":
                        new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this);
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
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
        #region Local Services: importDeliveryPoints(), exportDeliveryPoints(), setUserServices(), buildHelpMenu()
        private DeliveryPoints importDeliveryPoints(string filename) {
            //Import delivery points from a file into the current dataset
            DeliveryPoints points=null;
            StreamReader reader=null;
            const string CSV_DELIM = ",";
            try {
                //Clear dataset and fetch new data from the export file
                points = new DeliveryPoints();
                reader = new StreamReader(filename,System.Text.Encoding.ASCII);
                string line = reader.ReadLine();
                while(line != null) {
                    string[] tokens = line.Split(Convert.ToChar(CSV_DELIM));
                    DeliveryPoint point = new DeliveryPoint();
                    #region Copy tokens to datarow
                    point.Command = tokens[0];
                    point.Account = tokens[1];
                    point.NickName = tokens[2];
                    point.Name = tokens[3];
                    point.Building = tokens[4];
                    point.Route = tokens[5];
                    point.Phone = tokens[6];
                    point.OpenDate = Convert.ToDateTime(tokens[7]);
                    point.OpenTime = tokens[8];
                    point.CloseTime = tokens[9];
                    point.ServiceTimeFactor = Convert.ToDecimal(tokens[10]);
                    point.Unit = tokens[11];
                    point.SetupTime = Convert.ToInt32(tokens[12]);
                    point.Appt = tokens[13];

                    Stop stop = new Stop();
                    stop.Name = tokens[14];
                    stop.NickName = tokens[15];
                    stop.Phone = tokens[16];
                    stop.Address = tokens[17];
                    stop.City = tokens[18];
                    stop.State = tokens[19];
                    stop.Zip = tokens[20];
                    stop.Open = tokens[21];
                    stop.Close = tokens[22];
                    stop.Comment = tokens[23];
                    point.Stop = stop;

                    point.LastUpdated = Convert.ToDateTime(tokens[24]);
                    #endregion
                    points.Add(point);
                    line = reader.ReadLine();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while importing delivery points from " + filename + ".",ex); }
            finally { if(reader != null) reader.Close(); }
            return points;
        }
        private DateTime exportDeliveryPoints(string filename,DateTime lastUpdated) {
            //Exports delivery points dataset to a file
            DateTime lastUpdate=lastUpdated;
            StreamWriter writer=null;
            const string CSV_DELIM = ",";

            //Determine export filename (application or user specified)
            string exportFile = filename.Length > 0 ? filename : App.Config.ExportFile;
            try {
                //Create the export file and save to disk
                writer = new StreamWriter(new FileStream(exportFile,FileMode.Create,FileAccess.ReadWrite));
                writer.BaseStream.Seek(0,SeekOrigin.Begin);
                for(int i=0;i<this.mDeliveryPoints.Count;i++) {
                    //Create a delivery point and persist as CSV
                    DeliveryPoint point = (DeliveryPoint)this.mDeliveryPoints[i];
                    #region Copy datarow to csv record
                    string line =   point.Command + CSV_DELIM + 
                                    point.Account + CSV_DELIM + 
                                    point.NickName + CSV_DELIM + 
                                    point.Name + CSV_DELIM + 
                                    point.Building + CSV_DELIM + 
                                    point.Route + CSV_DELIM + 
                                    point.Phone + CSV_DELIM + 
                                    point.OpenDate + CSV_DELIM + 
                                    point.OpenTime + CSV_DELIM + 
                                    point.CloseTime + CSV_DELIM + 
                                    point.ServiceTimeFactor + CSV_DELIM + 
                                    point.Unit + CSV_DELIM + 
                                    point.SetupTime + CSV_DELIM + 
                                    point.Appt + CSV_DELIM + 
                                    point.Stop.Name + CSV_DELIM + 
                                    point.Stop.NickName + CSV_DELIM + 
                                    point.Stop.Phone + CSV_DELIM + 
                                    point.Stop.Address + CSV_DELIM + 
                                    point.Stop.City + CSV_DELIM + 
                                    point.Stop.State + CSV_DELIM + 
                                    point.Stop.Zip + CSV_DELIM + 
                                    point.Stop.Open + CSV_DELIM + 
                                    point.Stop.Close + CSV_DELIM + 
                                    point.Stop.Comment + CSV_DELIM + 
                                    point.LastUpdated;
                    #endregion
                    writer.WriteLine(line);

                    //Capture for most recent last updated datetime
                    if(point.LastUpdated.CompareTo(lastUpdate) > 0) lastUpdate = point.LastUpdated;
                }
                writer.Flush();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting delivery points to " + exportFile + ".",ex); }
            finally { if(writer != null) writer.Close(); }
            return lastUpdate;
        }
		private void setUserServices() {
			//Set user services
			try {
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = true;
				this.mnuFileSave.Enabled = this.btnSave.Enabled = (this.mGridDirty && this.mFilename.Length > 0);
				this.mnuFileSaveAs.Enabled = (!App.Config.ReadOnly && this.mDeliveryPoints.Count > 0);
				this.mnuFileExport.Enabled = this.btnExport.Enabled = (!App.Config.ReadOnly && this.mDeliveryPoints.Count > 0);
				this.mnuFileSettings.Enabled = true;
                this.mnuFilePreview.Enabled = this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.mDeliveryPoints.Count > 0);
				this.mnuFileExit.Enabled = true;
				this.mnuEditCut.Enabled = this.cmsCut.Enabled = this.btnCut.Enabled = (this.grdMain.ActiveCell != null && this.grdMain.ActiveCell.IsInEditMode);
                this.mnuEditCopy.Enabled = this.cmsCopy.Enabled = this.btnCopy.Enabled = (this.grdMain.ActiveCell != null && this.grdMain.ActiveCell.IsInEditMode);
				this.mnuEditPaste.Enabled = this.cmsPaste.Enabled = this.btnPaste.Enabled = (this.grdMain.ActiveCell != null && this.grdMain.ActiveCell.IsInEditMode && Clipboard.GetDataObject() != null);
				this.mnuEditSearch.Enabled = this.btnSearch.Enabled = false;
				this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = true;
				this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(DeliveryPointsProxy.ServiceState,DeliveryPointsProxy.ServiceAddress));
                this.stbMain.User1Panel.ToolTipText = "Last updated datetime";
                this.stbMain.User1Panel.Text = this.mLastUpated.ToString("MM/dd/yyyy hh:mm tt");
                if(App.Config.ReadOnly) {
                    this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                    this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
                }
                else {
                    this.stbMain.User2Panel.Icon = null;
                    this.stbMain.User2Panel.ToolTipText = "# of delivery points";
                    this.stbMain.User2Panel.Text = this.mDeliveryPoints.Count.ToString();
                }
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
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