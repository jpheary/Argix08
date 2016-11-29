using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Argix.Windows;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Excel=Microsoft.Office.Interop.Excel;

namespace Argix.Finance {
    //
    public class frmMain:System.Windows.Forms.Form {
        //Members
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvc=null;
        private System.Windows.Forms.ToolTip mToolTip=null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
        private string mQuoteFile="";

        private const string MSG_TITLE = "Argix Direct Rate Quotes";

        #region Controls

        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtFloorMin;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdRates;
        private System.Windows.Forms.ComboBox cboClassCode;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.Label _lblClassCode;
        private System.Windows.Forms.Label _lblFloorMin;
        private System.Windows.Forms.Label _lblOrigin;
        private System.Windows.Forms.Label _lblDiscount;
        private System.Windows.Forms.ListBox lstZips;
        private System.Windows.Forms.TextBox txtZips;
        private System.Windows.Forms.TextBox txtOrigin;
        private System.Windows.Forms.ComboBox cboTariff;
        private System.Windows.Forms.GroupBox grpTariffs;
        private System.Windows.Forms.GroupBox grpZips;
        private Button btnAddTariffs;
        private ContextMenuStrip cmsMain;
        private ToolStripMenuItem ctxDelete;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuHelp;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnSave;
        private ToolStripButton btnPrint;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton btnRefresh;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripMenuItem mnuFileExport;
        private ToolStripSeparator muFileSep2;
        private ToolStripMenuItem mnuFileSettings;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripButton btnExport;
        private Label label1;
        private BindingSource mRates;
        private ArgixStatusBar stbMain;
        private ToolStripMenuItem mnuToolsConfig;
        private Button btnFill;
        private Label _lblHigh;
        private Label _lblLow;
        private NumericUpDown updHigh;
        private NumericUpDown updLow;
        private Button btnClear;
        private GroupBox grpAuto;
        private IContainer components;
        #endregion

        //Interface
        public frmMain() {
            //Constructor
            InitializeComponent();
            this.Text = "Argix Direct " + App.Product;
            buildHelpMenu();
            Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
            Thread.Sleep(3000);
            
            //Create data and UI services
            this.mPageSettings = new PageSettings();
            this.mPageSettings.Landscape = true;
            this.mGridSvc = new UltraGridSvc(this.grdRates);
            this.mToolTip = new System.Windows.Forms.ToolTip();
            this.mMessageMgr = new MessageManager(this.stbMain.Panels[0],1000,3000);
            configApplication();
        }
        protected override void Dispose(bool disposing) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose(disposing); }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Rate", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DestZip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MinCharge");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrgZip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate10001");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate1001");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate20001");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate2001");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate5001");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate501");
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            this._lblOrigin = new System.Windows.Forms.Label();
            this.txtZips = new System.Windows.Forms.TextBox();
            this._lblClassCode = new System.Windows.Forms.Label();
            this._lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this._lblFloorMin = new System.Windows.Forms.Label();
            this.txtFloorMin = new System.Windows.Forms.TextBox();
            this.grdRates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mRates = new System.Windows.Forms.BindingSource(this.components);
            this.cboClassCode = new System.Windows.Forms.ComboBox();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.txtOrigin = new System.Windows.Forms.TextBox();
            this.grpZips = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.grpAuto = new System.Windows.Forms.GroupBox();
            this.updLow = new System.Windows.Forms.NumericUpDown();
            this.btnFill = new System.Windows.Forms.Button();
            this._lblHigh = new System.Windows.Forms.Label();
            this.updHigh = new System.Windows.Forms.NumericUpDown();
            this._lblLow = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstZips = new System.Windows.Forms.ListBox();
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.grpTariffs = new System.Windows.Forms.GroupBox();
            this.btnAddTariffs = new System.Windows.Forms.Button();
            this.cboTariff = new System.Windows.Forms.ComboBox();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.muFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.grdRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRates)).BeginInit();
            this.grpProperties.SuspendLayout();
            this.grpZips.SuspendLayout();
            this.grpAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updHigh)).BeginInit();
            this.cmsMain.SuspendLayout();
            this.grpTariffs.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblOrigin
            // 
            this._lblOrigin.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOrigin.Location = new System.Drawing.Point(6, 24);
            this._lblOrigin.Name = "_lblOrigin";
            this._lblOrigin.Size = new System.Drawing.Size(96, 16);
            this._lblOrigin.TabIndex = 0;
            this._lblOrigin.Text = "Origin";
            this._lblOrigin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZips
            // 
            this.txtZips.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtZips.Location = new System.Drawing.Point(50, 28);
            this.txtZips.Name = "txtZips";
            this.txtZips.Size = new System.Drawing.Size(112, 22);
            this.txtZips.TabIndex = 10;
            this.txtZips.Leave += new System.EventHandler(this.OnDestinationLeave);
            this.txtZips.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnDestinationKeyPress);
            // 
            // _lblClassCode
            // 
            this._lblClassCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblClassCode.Location = new System.Drawing.Point(6, 56);
            this._lblClassCode.Name = "_lblClassCode";
            this._lblClassCode.Size = new System.Drawing.Size(96, 16);
            this._lblClassCode.TabIndex = 8;
            this._lblClassCode.Text = "Class Code";
            this._lblClassCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDiscount
            // 
            this._lblDiscount.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDiscount.Location = new System.Drawing.Point(6, 88);
            this._lblDiscount.Name = "_lblDiscount";
            this._lblDiscount.Size = new System.Drawing.Size(96, 16);
            this._lblDiscount.TabIndex = 15;
            this._lblDiscount.Text = "Discount %";
            this._lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(109, 88);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(96, 21);
            this.txtDiscount.TabIndex = 6;
            this.txtDiscount.TextChanged += new System.EventHandler(this.OnDiscountChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnDiscountKeyPress);
            // 
            // _lblFloorMin
            // 
            this._lblFloorMin.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblFloorMin.Location = new System.Drawing.Point(6, 120);
            this._lblFloorMin.Name = "_lblFloorMin";
            this._lblFloorMin.Size = new System.Drawing.Size(96, 16);
            this._lblFloorMin.TabIndex = 17;
            this._lblFloorMin.Text = "Floor Min";
            this._lblFloorMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFloorMin
            // 
            this.txtFloorMin.Location = new System.Drawing.Point(109, 120);
            this.txtFloorMin.Name = "txtFloorMin";
            this.txtFloorMin.Size = new System.Drawing.Size(96, 21);
            this.txtFloorMin.TabIndex = 8;
            this.txtFloorMin.TextChanged += new System.EventHandler(this.OnFloorMinChanged);
            this.txtFloorMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnFloorMinKeyPress);
            // 
            // grdRates
            // 
            this.grdRates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdRates.DataSource = this.mRates;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.FontData.Name = "Verdana";
            appearance12.FontData.SizeInPoints = 8F;
            appearance12.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance12.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.Appearance = appearance12;
            ultraGridColumn11.Header.Caption = "Dest Zip";
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn11.Width = 72;
            ultraGridColumn12.Header.Caption = "Min Chg";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn12.Width = 72;
            ultraGridColumn13.Header.Caption = "Org Zip";
            ultraGridColumn13.Header.VisiblePosition = 0;
            ultraGridColumn13.Width = 72;
            ultraGridColumn14.Header.Caption = "0-499";
            ultraGridColumn14.Header.VisiblePosition = 3;
            ultraGridColumn14.Width = 96;
            ultraGridColumn15.Header.Caption = "10000-19999";
            ultraGridColumn15.Header.VisiblePosition = 8;
            ultraGridColumn15.Width = 96;
            ultraGridColumn16.Header.Caption = "1000-1999";
            ultraGridColumn16.Header.VisiblePosition = 5;
            ultraGridColumn16.Width = 96;
            ultraGridColumn17.Header.Caption = ">20000";
            ultraGridColumn17.Header.VisiblePosition = 9;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.Caption = "2000-4999";
            ultraGridColumn18.Header.VisiblePosition = 6;
            ultraGridColumn18.Width = 96;
            ultraGridColumn19.Header.Caption = "5000-9999";
            ultraGridColumn19.Header.VisiblePosition = 7;
            ultraGridColumn19.Width = 96;
            ultraGridColumn20.Header.Caption = "500-999";
            ultraGridColumn20.Header.VisiblePosition = 4;
            ultraGridColumn20.Width = 96;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20});
            this.grdRates.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance13.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance13.FontData.BoldAsString = "True";
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance13.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.CaptionAppearance = appearance13;
            this.grdRates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdRates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance14.BackColor = System.Drawing.SystemColors.Control;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.Override.HeaderAppearance = appearance14;
            this.grdRates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdRates.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance15.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdRates.DisplayLayout.Override.RowAppearance = appearance15;
            this.grdRates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdRates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdRates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdRates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdRates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdRates.Location = new System.Drawing.Point(8, 286);
            this.grdRates.Name = "grdRates";
            this.grdRates.Size = new System.Drawing.Size(680, 171);
            this.grdRates.TabIndex = 22;
            this.grdRates.Text = "Rate Chart";
            this.grdRates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mRates
            // 
            this.mRates.DataSource = typeof(Argix.Finance.Rates);
            // 
            // cboClassCode
            // 
            this.cboClassCode.Location = new System.Drawing.Point(109, 56);
            this.cboClassCode.Name = "cboClassCode";
            this.cboClassCode.Size = new System.Drawing.Size(216, 21);
            this.cboClassCode.TabIndex = 4;
            this.cboClassCode.TextChanged += new System.EventHandler(this.OnClassCodeChanged);
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this._lblOrigin);
            this.grpProperties.Controls.Add(this._lblFloorMin);
            this.grpProperties.Controls.Add(this.txtDiscount);
            this.grpProperties.Controls.Add(this._lblDiscount);
            this.grpProperties.Controls.Add(this._lblClassCode);
            this.grpProperties.Controls.Add(this.txtFloorMin);
            this.grpProperties.Controls.Add(this.cboClassCode);
            this.grpProperties.Controls.Add(this.txtOrigin);
            this.grpProperties.Location = new System.Drawing.Point(8, 124);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(332, 156);
            this.grpProperties.TabIndex = 0;
            this.grpProperties.TabStop = false;
            this.grpProperties.Text = "Fixed Properties";
            // 
            // txtOrigin
            // 
            this.txtOrigin.AcceptsReturn = true;
            this.txtOrigin.Location = new System.Drawing.Point(109, 24);
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.Size = new System.Drawing.Size(96, 21);
            this.txtOrigin.TabIndex = 2;
            this.txtOrigin.TextChanged += new System.EventHandler(this.OnOriginChanged);
            this.txtOrigin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnOriginKeyPress);
            // 
            // grpZips
            // 
            this.grpZips.Controls.Add(this.btnClear);
            this.grpZips.Controls.Add(this.grpAuto);
            this.grpZips.Controls.Add(this.label1);
            this.grpZips.Controls.Add(this.lstZips);
            this.grpZips.Controls.Add(this.txtZips);
            this.grpZips.Location = new System.Drawing.Point(360, 54);
            this.grpZips.Name = "grpZips";
            this.grpZips.Size = new System.Drawing.Size(324, 226);
            this.grpZips.TabIndex = 10;
            this.grpZips.TabStop = false;
            this.grpZips.Text = "Destination Zip Codes";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(180, 197);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 23);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.OnClearZips);
            // 
            // grpAuto
            // 
            this.grpAuto.Controls.Add(this.updLow);
            this.grpAuto.Controls.Add(this.btnFill);
            this.grpAuto.Controls.Add(this._lblHigh);
            this.grpAuto.Controls.Add(this.updHigh);
            this.grpAuto.Controls.Add(this._lblLow);
            this.grpAuto.Location = new System.Drawing.Point(180, 60);
            this.grpAuto.Name = "grpAuto";
            this.grpAuto.Size = new System.Drawing.Size(138, 119);
            this.grpAuto.TabIndex = 19;
            this.grpAuto.TabStop = false;
            this.grpAuto.Text = "Auto Zip";
            // 
            // updLow
            // 
            this.updLow.Location = new System.Drawing.Point(53, 27);
            this.updLow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updLow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updLow.Name = "updLow";
            this.updLow.Size = new System.Drawing.Size(70, 21);
            this.updLow.TabIndex = 15;
            this.updLow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updLow.ValueChanged += new System.EventHandler(this.OnLowHighChanged);
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(53, 91);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(70, 23);
            this.btnFill.TabIndex = 14;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.OnAddZips);
            // 
            // _lblHigh
            // 
            this._lblHigh.Location = new System.Drawing.Point(8, 59);
            this._lblHigh.Name = "_lblHigh";
            this._lblHigh.Size = new System.Drawing.Size(39, 21);
            this._lblHigh.TabIndex = 18;
            this._lblHigh.Text = "High";
            this._lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // updHigh
            // 
            this.updHigh.Location = new System.Drawing.Point(53, 59);
            this.updHigh.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updHigh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updHigh.Name = "updHigh";
            this.updHigh.Size = new System.Drawing.Size(70, 21);
            this.updHigh.TabIndex = 16;
            this.updHigh.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.updHigh.ValueChanged += new System.EventHandler(this.OnLowHighChanged);
            // 
            // _lblLow
            // 
            this._lblLow.Location = new System.Drawing.Point(8, 27);
            this._lblLow.Name = "_lblLow";
            this._lblLow.Size = new System.Drawing.Size(39, 21);
            this._lblLow.TabIndex = 17;
            this._lblLow.Text = "Low";
            this._lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "Zip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstZips
            // 
            this.lstZips.ContextMenuStrip = this.cmsMain;
            this.lstZips.Location = new System.Drawing.Point(50, 60);
            this.lstZips.Name = "lstZips";
            this.lstZips.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstZips.Size = new System.Drawing.Size(112, 160);
            this.lstZips.Sorted = true;
            this.lstZips.TabIndex = 12;
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxDelete});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(108, 26);
            // 
            // ctxDelete
            // 
            this.ctxDelete.Name = "ctxDelete";
            this.ctxDelete.Size = new System.Drawing.Size(107, 22);
            this.ctxDelete.Text = "Delete";
            this.ctxDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // grpTariffs
            // 
            this.grpTariffs.Controls.Add(this.btnAddTariffs);
            this.grpTariffs.Controls.Add(this.cboTariff);
            this.grpTariffs.Location = new System.Drawing.Point(6, 54);
            this.grpTariffs.Name = "grpTariffs";
            this.grpTariffs.Size = new System.Drawing.Size(334, 64);
            this.grpTariffs.TabIndex = 29;
            this.grpTariffs.TabStop = false;
            this.grpTariffs.Text = "Available Tariffs";
            // 
            // btnAddTariffs
            // 
            this.btnAddTariffs.Enabled = false;
            this.btnAddTariffs.Location = new System.Drawing.Point(296, 26);
            this.btnAddTariffs.Name = "btnAddTariffs";
            this.btnAddTariffs.Size = new System.Drawing.Size(31, 21);
            this.btnAddTariffs.TabIndex = 1;
            this.btnAddTariffs.Text = "...";
            this.btnAddTariffs.UseVisualStyleBackColor = true;
            this.btnAddTariffs.Click += new System.EventHandler(this.OnAddTariffs);
            // 
            // cboTariff
            // 
            this.cboTariff.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTariff.Location = new System.Drawing.Point(12, 27);
            this.cboTariff.Name = "cboTariff";
            this.cboTariff.Size = new System.Drawing.Size(278, 21);
            this.cboTariff.TabIndex = 0;
            this.cboTariff.SelectedIndexChanged += new System.EventHandler(this.OnTariffSelected);
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
            this.msMain.Size = new System.Drawing.Size(696, 24);
            this.msMain.TabIndex = 30;
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
            this.muFileSep2,
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
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(159, 22);
            this.mnuFileNew.Text = "&New Quote";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(159, 22);
            this.mnuFileOpen.Text = "&Open Quote...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(156, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(159, 22);
            this.mnuFileSave.Text = "&Save Quote";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(159, 22);
            this.mnuFileSaveAs.Text = "Save Quote &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(159, 22);
            this.mnuFileExport.Text = "&Export Rates...";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // muFileSep2
            // 
            this.muFileSep2.Name = "muFileSep2";
            this.muFileSep2.Size = new System.Drawing.Size(156, 6);
            // 
            // mnuFileSettings
            // 
            this.mnuFileSettings.Image = global::Argix.Properties.Resources.PageSetup;
            this.mnuFileSettings.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuFileSettings.Name = "mnuFileSettings";
            this.mnuFileSettings.Size = new System.Drawing.Size(159, 22);
            this.mnuFileSettings.Text = "Page &Settings...";
            this.mnuFileSettings.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(159, 22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(159, 22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(156, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(159, 22);
            this.mnuFileExit.Text = "E&xit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
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
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.FormulaEvaluator;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(154, 22);
            this.mnuViewRefresh.Text = "&Calculate Rates";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(151, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(154, 22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(154, 22);
            this.mnuViewStatusBar.Text = "&StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
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
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(183, 22);
            this.mnuHelpAbout.Text = "&About Rate Quotes...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(180, 6);
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.toolStripSeparator1,
            this.btnSave,
            this.btnExport,
            this.btnPrint,
            this.toolStripSeparator2,
            this.btnRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(696, 25);
            this.tsMain.TabIndex = 32;
            this.tsMain.Text = "toolStrip1";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.ToolTipText = "New rate quote";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.ToolTipText = "Open an existing rate quote";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.ToolTipText = "Save the current rate quote";
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.ToolTipText = "Export the current rates to Excel";
            this.btnExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.ToolTipText = "Print the current rates";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.FormulaEvaluator;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.ToolTipText = "Calculate rates for this quote";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 458);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(696, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 33;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(696, 482);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.grpTariffs);
            this.Controls.Add(this.grpProperties);
            this.Controls.Add(this.grpZips);
            this.Controls.Add(this.grdRates);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Rate Quotes";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.grdRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRates)).EndInit();
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.grpZips.ResumeLayout(false);
            this.grpZips.PerformLayout();
            this.grpAuto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updHigh)).EndInit();
            this.cmsMain.ResumeLayout(false);
            this.grpTariffs.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
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
                    this.mnuViewToolbar.Checked = this.msMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
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
                this.mToolTip.SetToolTip(this.txtZips,"Enter 5-digit zip code and press Enter.");
				#endregion
				
				//Set control defaults
				#region Grid customizations from normal layout (to support cell editing)
				this.grdRates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                //this.grdRates.DisplayLayout.Bands[0].Columns["SortCenter"].SortIndicator = SortIndicator.Ascending;
				#endregion

                TerminalInfo t = RateWareProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.stbMain.User1Panel.Width = 144;
                this.cboClassCode.DataSource = RateWareProxy.GetClassCodes();
                this.cboClassCode.DisplayMember = "Description";
                this.cboClassCode.ValueMember = "Class";

                string[] tariffs = RateWareProxy.GetTariffs();
                if(tariffs.Length == 0)
                    MessageBox.Show(this,"No CzarLite tariffs Installed.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Stop);
                else {
                    for(int i=0;i<tariffs.Length;i++)
                        this.cboTariff.Items.Add(tariffs[i]);
                    this.cboTariff.SelectedIndex = 0;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Event handler for form closing event
            if(!e.Cancel) {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnTariffSelected(object sender,System.EventArgs e) {
            this.mRates.Clear();
            setUserServices();
        }
        private void OnOriginKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            //char 8 = Backspace key
            if(!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8))
                e.Handled = true;
            setUserServices();
        }
        private void OnOriginChanged(object sender,System.EventArgs e) {
            this.mRates.Clear();
            setUserServices();
        }
        private void OnClassCodeChanged(object sender,System.EventArgs e) {
            this.mRates.Clear();
            setUserServices();
        }
        private void OnDiscountKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            if(!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8 || e.KeyChar == '.'))
                e.Handled = true;
            setUserServices();
        }
        private void OnDiscountChanged(object sender,System.EventArgs e) {
            this.mRates.Clear();
            setUserServices();
        }
        private void OnFloorMinKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            if(!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8))
                e.Handled = true;
            setUserServices();
        }
        private void OnFloorMinChanged(object sender,System.EventArgs e) {
            this.mRates.Clear();
            setUserServices();
        }
        private void OnDestinationKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            //Trap Enter key and add entered Zip to the list box
            if(e.KeyChar == (char)13) {
                OnDestinationLeave(this.txtZips, EventArgs.Empty);
                e.Handled = true;
            }
            setUserServices();
        }
        private void OnDestinationLeave(object sender,System.EventArgs e) {
            //Add a valid zip code to the list
            string zip = this.txtZips.Text;
            if(zip.Length == 5 || zip.Length == 3) {	
                //Check if it already exists in the list
                if(this.lstZips.FindStringExact(zip) == -1) {
                    this.lstZips.Items.Add(zip);
                    this.txtZips.Text = "";
                }
                else
                    this.txtZips.Text = "";
            }
            setUserServices();
        }
        private void OnAddTariffs(object sender,EventArgs e) {
            //New stuff: not implemented yet
            setUserServices();
        }
        private void OnLowHighChanged(object sender, EventArgs e) {
            //Event handler for change in up/down values
            //Prevent crossover
            NumericUpDown upd = (NumericUpDown)sender;
            if (upd.Name == "updLow")
                if (upd.Value > this.updHigh.Value) this.updHigh.Value = upd.Value;
                else
                    if (upd.Value < this.updLow.Value) this.updLow.Value = upd.Value;
        }
        private void OnAddZips(object sender, EventArgs e) {
            for (int i = (int)this.updLow.Value; i < (int)this.updHigh.Value; i++) { this.lstZips.Items.Add(i.ToString("000") + "00"); }
            setUserServices();
        }
        private void OnClearZips(object sender, EventArgs e) { this.lstZips.Items.Clear(); }
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Event handler for menu selection
            SaveFileDialog dlgSave=null;
            RateQuoteDS quote=null;
            string classCode="";
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "mnuFileNew":
                    case "btnNew":
                        this.txtOrigin.Text = "";
                        if(this.cboClassCode.Items.Count > 0) this.cboClassCode.SelectedIndex=0;
                        this.txtDiscount.Text = "";
                        this.txtFloorMin.Text = "";
                        this.lstZips.Items.Clear();
                        this.mQuoteFile = "";
                        break;
                    case "mnuFileOpen":
                    case "btnOpen":
                        OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.DefaultExt = "xml";
                        dlgOpen.Filter = "XML Files (*.xml)|*.xml";
                        dlgOpen.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        dlgOpen.Title = "Choose path and file name";
                        if(dlgOpen.ShowDialog() == DialogResult.OK) {
                            if(!dlgOpen.FileName.EndsWith("xml")) {
                                MessageBox.Show(this,"Make sure it's a valid xml file saved from this program.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                return;
                            }
                            this.mQuoteFile = dlgOpen.FileName;
                            quote = new RateQuoteDS();
                            quote.ReadXml(dlgOpen.FileName);
                            this.txtOrigin.Text = !quote.RateQuoteTable[0].IsOriginNull() ? quote.RateQuoteTable[0].Origin : "";
                            this.cboClassCode.Text = !quote.RateQuoteTable[0].IsClassCodeNull() ? quote.RateQuoteTable[0].ClassCode : "";
                            this.txtDiscount.Text = !quote.RateQuoteTable[0].IsDiscountNull() ? quote.RateQuoteTable[0].Discount : "";
                            this.txtFloorMin.Text = !quote.RateQuoteTable[0].IsFloorMinNull() ? quote.RateQuoteTable[0].FloorMin : "";
                            this.lstZips.Items.Clear();
                            for(int i=0;i<quote.ZipCodeTable.Rows.Count;i++) {
                                this.lstZips.Items.Add(quote.ZipCodeTable[i].ZipCode);
                            }
                        }
                        break;
                    case "mnuFileSave":
                    case "btnSave":
                        quote = new RateQuoteDS();
                        classCode = this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text;
                        quote.RateQuoteTable.AddRateQuoteTableRow(this.cboTariff.SelectedItem.ToString(),this.txtOrigin.Text,classCode,this.txtDiscount.Text,this.txtFloorMin.Text);
                        for(int i=0;i<this.lstZips.Items.Count;i++) {
                            quote.ZipCodeTable.AddZipCodeTableRow(this.lstZips.Items[i].ToString());
                        }
                        quote.WriteXml(this.mQuoteFile);
                        break;
                    case "mnuFileSaveAs":
                        if(this.lstZips.Items.Count > 0) {
                            dlgSave = new SaveFileDialog();
                            dlgSave.AddExtension = true;
                            dlgSave.DefaultExt = "xml";
                            dlgSave.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            dlgSave.Title = "Choose path and file name";
                            dlgSave.Filter = "XML Files (*.xml)|*.xml";
                            dlgSave.AddExtension = true;
                            if(dlgSave.ShowDialog() == DialogResult.OK) {
                                this.mQuoteFile = dlgSave.FileName;
                                quote = new RateQuoteDS();
                                classCode = this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text;
                                quote.RateQuoteTable.AddRateQuoteTableRow(this.cboTariff.SelectedItem.ToString(),this.txtOrigin.Text,classCode,this.txtDiscount.Text,this.txtFloorMin.Text);
                                for(int i=0;i<this.lstZips.Items.Count;i++) {
                                    quote.ZipCodeTable.AddZipCodeTableRow(this.lstZips.Items[i].ToString());
                                }
                                quote.WriteXml(this.mQuoteFile);
                            }
                        }
                        else
                            MessageBox.Show(this,"There are no Zip Codes to save.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        break;
                    case "mnuFileExport":
                    case "btnExport":
                        this.Cursor = Cursors.WaitCursor;
                        if(mRates.Count > 0) {
                            dlgSave = new SaveFileDialog();
                            dlgSave.DefaultExt = "xls";
                            dlgSave.AddExtension = true;
                            dlgSave.FileName = "Rates.xls";
                            dlgSave.Filter = "XSL Files (*.xsl)|*.xsl";
                            dlgSave.ValidateNames = true;
                            dlgSave.OverwritePrompt = false;
                            dlgSave.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                            if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                                Cursor.Current = Cursors.WaitCursor;
                                exportToExcel(dlgSave.FileName);
                            }
                        }
                        else
                            MessageBox.Show(this,"There are no Rates no save.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        break;
                    case "mnuFileSetup": UltraGridPrinter.PageSettings(); break;
                    case "mnuFilePrint": 
                        //UltraGridPrinter.Print(this.grdRates,"Rate Quote" +  DateTime.Today.ToString("dd-MMM-yyyy"),true);
                        this.grdRates.Print();
                        break;
                    case "btnPrint": UltraGridPrinter.Print(this.grdRates,"Rate Quote" +  DateTime.Today.ToString("dd-MMM-yyyy"),true); break;
                    case "mnuFilePreview": UltraGridPrinter.PrintPreview(this.grdRates,"Rate Quote" +  DateTime.Today.ToString("dd-MMM-yyyy")); break;
                    case "mnuFileExit": this.Close(); Application.Exit(); break;
                    case "mnuEditCut": case "ctxCut": case "btnCut":
                        break;
                    case "mnuEditCopy": case "ctxCopy": case "btnCopy":
                        break;
                    case "mnuEditPaste": case "ctxPaste": case "btnPaste":
                        break;
                    case "ctxDelete":
                        if(this.lstZips.SelectedItems.Count > 0) {
                            object[] lzips = new object[this.lstZips.SelectedItems.Count];
                            this.lstZips.SelectedItems.CopyTo(lzips,0);
                            foreach(object zip in lzips) this.lstZips.Items.Remove(zip);
                            this.mRates.Clear();
                        }
                        break;
                    case "mnuEditSearch": case "btnSearch": break;
                    case "mnuViewRefresh": 
                    case "ctxRefresh": 
                    case "btnRefresh": 
                        //Calcualate rates
                        string[] _zips = new string[this.lstZips.Items.Count];
                        this.lstZips.Items.CopyTo(_zips,0);
                        this.mRates.DataSource = RateWareProxy.CalculateRates(this.cboTariff.SelectedItem.ToString(),
                                                                        this.txtOrigin.Text.Trim(),
                                                                        (this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text),
                                                                        Convert.ToDouble(this.txtDiscount.Text),
                                                                        Convert.ToInt32(this.txtFloorMin.Text),
                                                                        _zips);
                        break;
                    case "mnuViewToolbar": this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); break;
                    case "mnuViewStatusBar": this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); break;
                    case "mnuToolsTrace":
                        break;
                    case "mnuToolsConfig": App.ShowConfig(); break;
                    case "mnuHelpAbout": new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
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
        #endregion
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu(), exportToExcel()
        private void configApplication() {
            try {
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); }
        }
        private void setUserServices() {
            //Set user services
            try {
                this.mnuFileNew.Enabled = this.btnNew.Enabled = true;
                this.mnuFileOpen.Enabled = this.btnOpen.Enabled = true;
                this.mnuFileSave.Enabled = this.btnSave.Enabled = this.mQuoteFile.Length > 0;
                this.mnuFileSaveAs.Enabled = (this.lstZips.Items.Count > 0);
                this.mnuFileExport.Enabled  = this.btnExport.Enabled = (this.grdRates.Rows.Count > 0);
                this.mnuFileSettings.Enabled = true;
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.grdRates.Rows.Count > 0);
                this.mnuFilePreview.Enabled = (this.grdRates.Rows.Count > 0);
                this.mnuFileExit.Enabled = true;

                string classCode = this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text;
                this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = this.txtOrigin.Text.Trim().Length > 0 && classCode.Length > 0 && this.txtDiscount.Text.Trim().Length > 0 && this.txtFloorMin.Text.Trim().Length > 0 && this.lstZips.Items.Count > 0;
                
                this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
                this.mnuHelpAbout.Enabled = true;

                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(RateWareProxy.ServiceState,RateWareProxy.ServiceAddress));
                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Error); }
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
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void exportToExcel(string fileName) {
            //Write rates to Excel
            Excel.Application excel = new Excel.ApplicationClass();
            Excel.Workbooks books = (Excel.Workbooks)excel.Workbooks;
            Excel._Workbook book = (Excel._Workbook)(books.Add(Missing.Value));
            Excel.Sheets sheets = (Excel.Sheets)book.Worksheets;
            Excel._Worksheet sheet = (Excel._Worksheet)(sheets.get_Item(1));

            //Title
            Excel.Range range = sheet.get_Range("A1",Missing.Value);
            range.Value2 = "Argix Direct Quotations";
            range.Font.Bold = true;
            range.Font.Size = 14;

            //Sub-title
            range = sheet.get_Range("A2",Missing.Value);
            string efcRec = this.cboTariff.SelectedItem.ToString();
            range.Value2 = "Tariffs Used: " + efcRec.Substring(12,2) + "/" + efcRec.Substring(14,2) + "/" + efcRec.Substring(8,4);
            range.Font.Bold = false;
            range.Font.Size = 10;

            //Fixed Fields
            range = sheet.get_Range("A4",Missing.Value);
            range = range.get_Resize(3,2);
            range.Font.Bold = true;
            range.Font.Size = 10;
            range.NumberFormat = "@";
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            object[,] fields = new object[3,2];
            fields[0,0] = "Origin Zip"; fields[0,1] = this.txtOrigin.Text.Trim();
            fields[1,0] = "Class Code"; fields[1,1] = (this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text);
            fields[2,0] = "Floor Charge"; fields[2,1] = Convert.ToInt32(this.txtFloorMin.Text).ToString("C");
            range.Value2 = fields;

            //Table Header
            object[] headers = { "Dest. Zip Code","Min Charge","Rate 0-499","  500-999","1000-1999","2000-2999","5000-9999","10,000-19,999","20,000+" };
            range = sheet.get_Range("A9","I9");
            range.Value2 = headers;
            range.Font.Bold = true;
            range.ColumnWidth = 25;

            //Create an array from the dataset
            object[,] data = new object[this.mRates.Count,10];
            for(int r=0;r< this.mRates.Count;r++) {
                Rate rate = (Rate)this.mRates[r];
                data[r,0] = rate.DestZip;
                data[r,1] = rate.MinCharge.ToString();
                data[r,2] = rate.Rate1.ToString();
                data[r,3] = rate.Rate501.ToString();
                data[r,4] = rate.Rate1001.ToString();
                data[r,5] = rate.Rate2001.ToString();
                data[r,6] = rate.Rate5001.ToString();
                data[r,7] = rate.Rate10001.ToString();
                data[r,8] = rate.Rate20001.ToString();
            }
            range = sheet.get_Range("A10",Missing.Value);
            range = range.get_Resize(this.mRates.Count,1);
            range.NumberFormat = "@";
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            
            //Get range based to rows and columns before setting the data
            range = range.get_Resize(this.mRates.Count,10);
            range.Value2 = data;
            
            //Format using AutoFormat function
            range = sheet.get_Range("A9",Missing.Value);
            range = range.get_Resize(this.mRates.Count +1,10);
            range.AutoFormat(Excel.XlRangeAutoFormat.xlRangeAutoFormatList1, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            //Save the workbook and quit excel
            book.SaveAs(fileName, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            book.Close(false,fileName, Missing.Value);
            excel.Quit();
        }
        #endregion

    }
}
