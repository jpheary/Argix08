using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Freight;
using Argix.Windows;

namespace Argix.Freight {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private UltraGridSvc mGridSvc = null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
        #region Controls
		private System.ComponentModel.IContainer components;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdTLs;
		private System.Windows.Forms.TextBox txtTLCartons;
		private System.Windows.Forms.TextBox txtTLPallets;
		private System.Windows.Forms.TextBox txtTLWeight;
        private System.Windows.Forms.TextBox txtTLCube;
		private System.Windows.Forms.TextBox txtISACube;
		private System.Windows.Forms.TextBox txtGrandTotalWeight;
		private System.Windows.Forms.TextBox txtGrandTotalCube;
		private System.Windows.Forms.TextBox txtLoadWeightRatio;
		private System.Windows.Forms.TextBox txtTrailerCubeRatio;
		private System.Windows.Forms.Label lblSelectedTLs;
		private System.Windows.Forms.ComboBox cboTerminals;
		private System.Windows.Forms.GroupBox grpTotals;
		private System.Windows.Forms.Label _lblTrailerLoad;
		private System.Windows.Forms.Label _lblTotals;
		private System.Windows.Forms.Label _lblISA;
        private System.Windows.Forms.Label _TLS;
        private System.Windows.Forms.GroupBox grpLine;
		private System.Windows.Forms.GroupBox grpLine11;
        private System.Windows.Forms.GroupBox grpLine12;
		private System.Windows.Forms.Label _lblLoadCube;
		private System.Windows.Forms.Label _lblLoadWeight;
		private System.Windows.Forms.Label _lblTotalCube;
		private System.Windows.Forms.Label _lblTotalWeight;
		private System.Windows.Forms.Label _lblISAWeight;
		private System.Windows.Forms.Label _lblISACube;
		private System.Windows.Forms.Label _lblTLCube;
		private System.Windows.Forms.Label _lblTLWeight;
		private System.Windows.Forms.Label _lblTLPallets;
        private System.Windows.Forms.Label _lblTLCartons;
        private Argix.Windows.ArgixStatusBar stbMain;
        private System.Windows.Forms.TextBox txtSearchSort;
        private System.Windows.Forms.PictureBox _picSearch;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFilePageSetup;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuEditSearch;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewAgentSummary;
        private ToolStripSeparator mnuViewSep2;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripButton btnPrint;
        private ToolStripButton btnSearch;
        private ToolStripButton btnRefresh;
        private ToolStripButton btnAgentSummary;
        private ToolStripSeparator btnSep1;
        private ToolStripSeparator btnSep2;
        private ToolStripMenuItem mnuFileSaveAs;
        private BindingSource mTerminals;
        private BindingSource mTLs;
        private NumericUpDown updISAWeight;
        #endregion

        //Interface
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
                buildHelpMenu();
				#region Splash Screen Support
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#endregion
				#region Set window docking
				this.grdTLs.Controls.AddRange(new Control[]{this.cboTerminals, this._picSearch, this.txtSearchSort});
				this.cboTerminals.Top = this.txtSearchSort.Top = 1;
				this._picSearch.Top = 3;
				this.cboTerminals.Left = 72;
				this._picSearch.Left = this.grdTLs.Width - _picSearch.Width - this.txtSearchSort.Width - 5;
				this.txtSearchSort.Left = this.grdTLs.Width - this.txtSearchSort.Width - 2;
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
                this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tsMain,this.msMain,this.stbMain });
                #endregion
				
				//Create data and UI servicesv
				this.mGridSvc = new UltraGridSvc(this.grdTLs, this.txtSearchSort);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 1000, 3000);
				configApplication();
			}
            catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure",ex); }
        }
		protected override void Dispose( bool disposing ) { if(disposing) { if(components!=null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TL", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubePercent");
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallLane");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WeightPercent");
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grdTLs = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cboTerminals = new System.Windows.Forms.ComboBox();
            this.mTerminals = new System.Windows.Forms.BindingSource(this.components);
            this._picSearch = new System.Windows.Forms.PictureBox();
            this.txtSearchSort = new System.Windows.Forms.TextBox();
            this.mTLs = new System.Windows.Forms.BindingSource(this.components);
            this.grpTotals = new System.Windows.Forms.GroupBox();
            this.updISAWeight = new System.Windows.Forms.NumericUpDown();
            this.grpLine12 = new System.Windows.Forms.GroupBox();
            this.grpLine11 = new System.Windows.Forms.GroupBox();
            this.grpLine = new System.Windows.Forms.GroupBox();
            this.lblSelectedTLs = new System.Windows.Forms.Label();
            this._lblLoadCube = new System.Windows.Forms.Label();
            this._lblLoadWeight = new System.Windows.Forms.Label();
            this._lblTrailerLoad = new System.Windows.Forms.Label();
            this._lblTotalCube = new System.Windows.Forms.Label();
            this._lblTotalWeight = new System.Windows.Forms.Label();
            this._lblTotals = new System.Windows.Forms.Label();
            this._lblISAWeight = new System.Windows.Forms.Label();
            this._lblISA = new System.Windows.Forms.Label();
            this._TLS = new System.Windows.Forms.Label();
            this.txtTrailerCubeRatio = new System.Windows.Forms.TextBox();
            this.txtLoadWeightRatio = new System.Windows.Forms.TextBox();
            this.txtGrandTotalCube = new System.Windows.Forms.TextBox();
            this.txtGrandTotalWeight = new System.Windows.Forms.TextBox();
            this.txtTLPallets = new System.Windows.Forms.TextBox();
            this.txtISACube = new System.Windows.Forms.TextBox();
            this.txtTLCartons = new System.Windows.Forms.TextBox();
            this.txtTLCube = new System.Windows.Forms.TextBox();
            this.txtTLWeight = new System.Windows.Forms.TextBox();
            this._lblISACube = new System.Windows.Forms.Label();
            this._lblTLCube = new System.Windows.Forms.Label();
            this._lblTLWeight = new System.Windows.Forms.Label();
            this._lblTLPallets = new System.Windows.Forms.Label();
            this._lblTLCartons = new System.Windows.Forms.Label();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewAgentSummary = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
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
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnAgentSummary = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).BeginInit();
            this.grdTLs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._picSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLs)).BeginInit();
            this.grpTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updISAWeight)).BeginInit();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdTLs
            // 
            this.grdTLs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTLs.Controls.Add(this.cboTerminals);
            this.grdTLs.Controls.Add(this._picSearch);
            this.grdTLs.Controls.Add(this.txtSearchSort);
            this.grdTLs.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdTLs.DataSource = this.mTLs;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.FontData.Name = "Verdana";
            appearance10.FontData.SizeInPoints = 8F;
            appearance10.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance10.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Appearance = appearance10;
            ultraGridColumn1.Header.VisiblePosition = 4;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "Agent";
            ultraGridColumn2.Header.VisiblePosition = 3;
            ultraGridColumn2.Width = 60;
            appearance1.TextHAlignAsString = "Right";
            ultraGridColumn3.CellAppearance = appearance1;
            ultraGridColumn3.Format = "#,0";
            ultraGridColumn3.Header.VisiblePosition = 10;
            ultraGridColumn3.Width = 60;
            ultraGridColumn4.Header.Caption = "Client";
            ultraGridColumn4.Header.VisiblePosition = 6;
            ultraGridColumn4.Width = 144;
            ultraGridColumn5.Header.Caption = "Client#";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Width = 60;
            ultraGridColumn6.Header.Caption = "Close#";
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 51;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn7.CellAppearance = appearance2;
            ultraGridColumn7.Header.VisiblePosition = 13;
            ultraGridColumn7.Width = 60;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn8.CellAppearance = appearance3;
            ultraGridColumn8.Format = "#0";
            ultraGridColumn8.Header.Caption = "Cube%";
            ultraGridColumn8.Header.VisiblePosition = 15;
            ultraGridColumn8.Width = 72;
            appearance4.TextHAlignAsString = "Right";
            ultraGridColumn9.CellAppearance = appearance4;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 48;
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn10.CellAppearance = appearance5;
            ultraGridColumn10.Format = "#,0";
            ultraGridColumn10.Header.VisiblePosition = 11;
            ultraGridColumn10.Width = 60;
            ultraGridColumn11.Header.VisiblePosition = 17;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 18;
            ultraGridColumn12.Hidden = true;
            appearance6.TextHAlignAsString = "Right";
            ultraGridColumn13.CellAppearance = appearance6;
            ultraGridColumn13.Header.Caption = "Small Lane";
            ultraGridColumn13.Header.VisiblePosition = 9;
            ultraGridColumn13.Width = 77;
            ultraGridColumn14.Format = "MM/dd/yyyy";
            ultraGridColumn14.Header.Caption = "TL Date";
            ultraGridColumn14.Header.VisiblePosition = 1;
            ultraGridColumn14.Width = 75;
            ultraGridColumn15.Header.Caption = "TL#";
            ultraGridColumn15.Header.VisiblePosition = 0;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.Header.VisiblePosition = 16;
            ultraGridColumn16.Hidden = true;
            appearance7.TextHAlignAsString = "Right";
            ultraGridColumn17.CellAppearance = appearance7;
            ultraGridColumn17.Format = "#,0";
            ultraGridColumn17.Header.VisiblePosition = 12;
            ultraGridColumn17.Width = 72;
            appearance8.TextHAlignAsString = "Right";
            ultraGridColumn18.CellAppearance = appearance8;
            ultraGridColumn18.Format = "#0";
            ultraGridColumn18.Header.Caption = "Weight%";
            ultraGridColumn18.Header.VisiblePosition = 14;
            ultraGridColumn18.Width = 72;
            ultraGridColumn19.Header.VisiblePosition = 7;
            ultraGridColumn19.Width = 48;
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
            ultraGridColumn19});
            this.grdTLs.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTLs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance16.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance16.FontData.BoldAsString = "True";
            appearance16.FontData.Name = "Verdana";
            appearance16.FontData.SizeInPoints = 9F;
            appearance16.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance16.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.CaptionAppearance = appearance16;
            this.grdTLs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTLs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance17.BackColor = System.Drawing.SystemColors.Control;
            appearance17.FontData.BoldAsString = "True";
            appearance17.FontData.Name = "Verdana";
            appearance17.FontData.SizeInPoints = 8F;
            appearance17.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Override.HeaderAppearance = appearance17;
            this.grdTLs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdTLs.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance18.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTLs.DisplayLayout.Override.RowAppearance = appearance18;
            this.grdTLs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdTLs.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdTLs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTLs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTLs.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTLs.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTLs.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdTLs.Location = new System.Drawing.Point(222, 51);
            this.grdTLs.Name = "grdTLs";
            this.grdTLs.Size = new System.Drawing.Size(534, 409);
            this.grdTLs.TabIndex = 2;
            this.grdTLs.Text = "TL\'s for ";
            this.grdTLs.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdTLs.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdTLs.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTLsSelected);
            // 
            // cboTerminals
            // 
            this.cboTerminals.DataSource = this.mTerminals;
            this.cboTerminals.DisplayMember = "Description";
            this.cboTerminals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminals.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTerminals.Location = new System.Drawing.Point(60, 1);
            this.cboTerminals.MaxDropDownItems = 5;
            this.cboTerminals.Name = "cboTerminals";
            this.cboTerminals.Size = new System.Drawing.Size(192, 21);
            this.cboTerminals.TabIndex = 1;
            this.cboTerminals.ValueMember = "TerminalID";
            this.cboTerminals.SelectionChangeCommitted += new System.EventHandler(this.OnTerminalSelected);
            // 
            // mTerminals
            // 
            this.mTerminals.DataSource = typeof(Argix.Freight.Terminals);
            // 
            // _picSearch
            // 
            this._picSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._picSearch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._picSearch.Image = ((System.Drawing.Image)(resources.GetObject("_picSearch.Image")));
            this._picSearch.Location = new System.Drawing.Point(416, 2);
            this._picSearch.Name = "_picSearch";
            this._picSearch.Size = new System.Drawing.Size(18, 18);
            this._picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._picSearch.TabIndex = 7;
            this._picSearch.TabStop = false;
            // 
            // txtSearchSort
            // 
            this.txtSearchSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSort.Location = new System.Drawing.Point(438, 1);
            this.txtSearchSort.Name = "txtSearchSort";
            this.txtSearchSort.Size = new System.Drawing.Size(96, 21);
            this.txtSearchSort.TabIndex = 6;
            this.txtSearchSort.TextChanged += new System.EventHandler(this.OnSearchValueChanged);
            // 
            // mTLs
            // 
            this.mTLs.DataSource = typeof(Argix.Freight.TLs);
            // 
            // grpTotals
            // 
            this.grpTotals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.grpTotals.Controls.Add(this.updISAWeight);
            this.grpTotals.Controls.Add(this.grpLine12);
            this.grpTotals.Controls.Add(this.grpLine11);
            this.grpTotals.Controls.Add(this.grpLine);
            this.grpTotals.Controls.Add(this.lblSelectedTLs);
            this.grpTotals.Controls.Add(this._lblLoadCube);
            this.grpTotals.Controls.Add(this._lblLoadWeight);
            this.grpTotals.Controls.Add(this._lblTrailerLoad);
            this.grpTotals.Controls.Add(this._lblTotalCube);
            this.grpTotals.Controls.Add(this._lblTotalWeight);
            this.grpTotals.Controls.Add(this._lblTotals);
            this.grpTotals.Controls.Add(this._lblISAWeight);
            this.grpTotals.Controls.Add(this._lblISA);
            this.grpTotals.Controls.Add(this._TLS);
            this.grpTotals.Controls.Add(this.txtTrailerCubeRatio);
            this.grpTotals.Controls.Add(this.txtLoadWeightRatio);
            this.grpTotals.Controls.Add(this.txtGrandTotalCube);
            this.grpTotals.Controls.Add(this.txtGrandTotalWeight);
            this.grpTotals.Controls.Add(this.txtTLPallets);
            this.grpTotals.Controls.Add(this.txtISACube);
            this.grpTotals.Controls.Add(this.txtTLCartons);
            this.grpTotals.Controls.Add(this.txtTLCube);
            this.grpTotals.Controls.Add(this.txtTLWeight);
            this.grpTotals.Controls.Add(this._lblISACube);
            this.grpTotals.Controls.Add(this._lblTLCube);
            this.grpTotals.Controls.Add(this._lblTLWeight);
            this.grpTotals.Controls.Add(this._lblTLPallets);
            this.grpTotals.Controls.Add(this._lblTLCartons);
            this.grpTotals.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpTotals.Location = new System.Drawing.Point(6, 51);
            this.grpTotals.Name = "grpTotals";
            this.grpTotals.Size = new System.Drawing.Size(210, 409);
            this.grpTotals.TabIndex = 4;
            this.grpTotals.TabStop = false;
            this.grpTotals.Text = "Totals";
            // 
            // updISAWeight
            // 
            this.updISAWeight.Location = new System.Drawing.Point(105, 189);
            this.updISAWeight.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.updISAWeight.Name = "updISAWeight";
            this.updISAWeight.Size = new System.Drawing.Size(96, 21);
            this.updISAWeight.TabIndex = 29;
            this.updISAWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.updISAWeight.ThousandsSeparator = true;
            this.updISAWeight.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.updISAWeight.ValueChanged += new System.EventHandler(this.OnISAWeightChanged);
            // 
            // grpLine12
            // 
            this.grpLine12.BackColor = System.Drawing.SystemColors.Control;
            this.grpLine12.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLine12.Location = new System.Drawing.Point(9, 330);
            this.grpLine12.Name = "grpLine12";
            this.grpLine12.Size = new System.Drawing.Size(192, 6);
            this.grpLine12.TabIndex = 28;
            this.grpLine12.TabStop = false;
            // 
            // grpLine11
            // 
            this.grpLine11.BackColor = System.Drawing.SystemColors.Control;
            this.grpLine11.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLine11.Location = new System.Drawing.Point(9, 336);
            this.grpLine11.Name = "grpLine11";
            this.grpLine11.Size = new System.Drawing.Size(192, 6);
            this.grpLine11.TabIndex = 27;
            this.grpLine11.TabStop = false;
            // 
            // grpLine
            // 
            this.grpLine.BackColor = System.Drawing.SystemColors.Control;
            this.grpLine.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpLine.Location = new System.Drawing.Point(9, 240);
            this.grpLine.Name = "grpLine";
            this.grpLine.Size = new System.Drawing.Size(192, 6);
            this.grpLine.TabIndex = 26;
            this.grpLine.TabStop = false;
            // 
            // lblSelectedTLs
            // 
            this.lblSelectedTLs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelectedTLs.Location = new System.Drawing.Point(105, 21);
            this.lblSelectedTLs.Name = "lblSelectedTLs";
            this.lblSelectedTLs.Size = new System.Drawing.Size(96, 21);
            this.lblSelectedTLs.TabIndex = 17;
            this.lblSelectedTLs.Text = "0";
            this.lblSelectedTLs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblLoadCube
            // 
            this._lblLoadCube.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLoadCube.Location = new System.Drawing.Point(21, 399);
            this._lblLoadCube.Name = "_lblLoadCube";
            this._lblLoadCube.Size = new System.Drawing.Size(84, 18);
            this._lblLoadCube.TabIndex = 25;
            this._lblLoadCube.Text = "Cube (%)";
            this._lblLoadCube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLoadWeight
            // 
            this._lblLoadWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLoadWeight.Location = new System.Drawing.Point(21, 375);
            this._lblLoadWeight.Name = "_lblLoadWeight";
            this._lblLoadWeight.Size = new System.Drawing.Size(84, 18);
            this._lblLoadWeight.TabIndex = 24;
            this._lblLoadWeight.Text = "Weight (%)";
            this._lblLoadWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTrailerLoad
            // 
            this._lblTrailerLoad.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTrailerLoad.Location = new System.Drawing.Point(6, 348);
            this._lblTrailerLoad.Name = "_lblTrailerLoad";
            this._lblTrailerLoad.Size = new System.Drawing.Size(192, 18);
            this._lblTrailerLoad.TabIndex = 23;
            this._lblTrailerLoad.Text = "Trailer Load % (53 foot)";
            this._lblTrailerLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTotalCube
            // 
            this._lblTotalCube.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTotalCube.Location = new System.Drawing.Point(21, 303);
            this._lblTotalCube.Name = "_lblTotalCube";
            this._lblTotalCube.Size = new System.Drawing.Size(84, 18);
            this._lblTotalCube.TabIndex = 22;
            this._lblTotalCube.Text = "Cube (in3)";
            this._lblTotalCube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTotalWeight
            // 
            this._lblTotalWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTotalWeight.Location = new System.Drawing.Point(21, 276);
            this._lblTotalWeight.Name = "_lblTotalWeight";
            this._lblTotalWeight.Size = new System.Drawing.Size(84, 18);
            this._lblTotalWeight.TabIndex = 21;
            this._lblTotalWeight.Text = "Weight (lbs)";
            this._lblTotalWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTotals
            // 
            this._lblTotals.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTotals.Location = new System.Drawing.Point(6, 249);
            this._lblTotals.Name = "_lblTotals";
            this._lblTotals.Size = new System.Drawing.Size(192, 18);
            this._lblTotals.TabIndex = 20;
            this._lblTotals.Text = "= Total";
            this._lblTotals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblISAWeight
            // 
            this._lblISAWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblISAWeight.Location = new System.Drawing.Point(21, 189);
            this._lblISAWeight.Name = "_lblISAWeight";
            this._lblISAWeight.Size = new System.Drawing.Size(84, 18);
            this._lblISAWeight.TabIndex = 19;
            this._lblISAWeight.Text = "Weight (lbs)";
            this._lblISAWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblISA
            // 
            this._lblISA.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblISA.Location = new System.Drawing.Point(6, 162);
            this._lblISA.Name = "_lblISA";
            this._lblISA.Size = new System.Drawing.Size(192, 18);
            this._lblISA.TabIndex = 18;
            this._lblISA.Text = "+  ISA";
            this._lblISA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _TLS
            // 
            this._TLS.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._TLS.Location = new System.Drawing.Point(6, 21);
            this._TLS.Name = "_TLS";
            this._TLS.Size = new System.Drawing.Size(96, 18);
            this._TLS.TabIndex = 16;
            this._TLS.Text = "Selected TL\'s";
            this._TLS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTrailerCubeRatio
            // 
            this.txtTrailerCubeRatio.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrailerCubeRatio.Location = new System.Drawing.Point(105, 399);
            this.txtTrailerCubeRatio.Name = "txtTrailerCubeRatio";
            this.txtTrailerCubeRatio.ReadOnly = true;
            this.txtTrailerCubeRatio.Size = new System.Drawing.Size(96, 21);
            this.txtTrailerCubeRatio.TabIndex = 15;
            this.txtTrailerCubeRatio.TabStop = false;
            this.txtTrailerCubeRatio.Text = "0";
            this.txtTrailerCubeRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLoadWeightRatio
            // 
            this.txtLoadWeightRatio.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLoadWeightRatio.Location = new System.Drawing.Point(105, 375);
            this.txtLoadWeightRatio.Name = "txtLoadWeightRatio";
            this.txtLoadWeightRatio.ReadOnly = true;
            this.txtLoadWeightRatio.Size = new System.Drawing.Size(96, 21);
            this.txtLoadWeightRatio.TabIndex = 14;
            this.txtLoadWeightRatio.TabStop = false;
            this.txtLoadWeightRatio.Text = "0";
            this.txtLoadWeightRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrandTotalCube
            // 
            this.txtGrandTotalCube.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtGrandTotalCube.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrandTotalCube.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtGrandTotalCube.Location = new System.Drawing.Point(105, 303);
            this.txtGrandTotalCube.Name = "txtGrandTotalCube";
            this.txtGrandTotalCube.ReadOnly = true;
            this.txtGrandTotalCube.Size = new System.Drawing.Size(96, 21);
            this.txtGrandTotalCube.TabIndex = 13;
            this.txtGrandTotalCube.TabStop = false;
            this.txtGrandTotalCube.Text = "0";
            this.txtGrandTotalCube.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrandTotalWeight
            // 
            this.txtGrandTotalWeight.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtGrandTotalWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGrandTotalWeight.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtGrandTotalWeight.Location = new System.Drawing.Point(105, 276);
            this.txtGrandTotalWeight.Name = "txtGrandTotalWeight";
            this.txtGrandTotalWeight.ReadOnly = true;
            this.txtGrandTotalWeight.Size = new System.Drawing.Size(96, 21);
            this.txtGrandTotalWeight.TabIndex = 12;
            this.txtGrandTotalWeight.TabStop = false;
            this.txtGrandTotalWeight.Text = "0";
            this.txtGrandTotalWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLPallets
            // 
            this.txtTLPallets.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTLPallets.Location = new System.Drawing.Point(105, 75);
            this.txtTLPallets.Name = "txtTLPallets";
            this.txtTLPallets.ReadOnly = true;
            this.txtTLPallets.Size = new System.Drawing.Size(96, 21);
            this.txtTLPallets.TabIndex = 11;
            this.txtTLPallets.TabStop = false;
            this.txtTLPallets.Text = "0";
            this.txtTLPallets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtISACube
            // 
            this.txtISACube.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtISACube.Location = new System.Drawing.Point(105, 216);
            this.txtISACube.Name = "txtISACube";
            this.txtISACube.ReadOnly = true;
            this.txtISACube.Size = new System.Drawing.Size(96, 21);
            this.txtISACube.TabIndex = 10;
            this.txtISACube.TabStop = false;
            this.txtISACube.Text = "0";
            this.txtISACube.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLCartons
            // 
            this.txtTLCartons.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTLCartons.Location = new System.Drawing.Point(105, 48);
            this.txtTLCartons.Name = "txtTLCartons";
            this.txtTLCartons.ReadOnly = true;
            this.txtTLCartons.Size = new System.Drawing.Size(96, 21);
            this.txtTLCartons.TabIndex = 9;
            this.txtTLCartons.TabStop = false;
            this.txtTLCartons.Text = "0";
            this.txtTLCartons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLCube
            // 
            this.txtTLCube.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTLCube.Location = new System.Drawing.Point(105, 129);
            this.txtTLCube.Name = "txtTLCube";
            this.txtTLCube.ReadOnly = true;
            this.txtTLCube.Size = new System.Drawing.Size(96, 21);
            this.txtTLCube.TabIndex = 8;
            this.txtTLCube.TabStop = false;
            this.txtTLCube.Text = "0";
            this.txtTLCube.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLWeight
            // 
            this.txtTLWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTLWeight.Location = new System.Drawing.Point(105, 102);
            this.txtTLWeight.Name = "txtTLWeight";
            this.txtTLWeight.ReadOnly = true;
            this.txtTLWeight.Size = new System.Drawing.Size(96, 21);
            this.txtTLWeight.TabIndex = 7;
            this.txtTLWeight.TabStop = false;
            this.txtTLWeight.Text = "0";
            this.txtTLWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblISACube
            // 
            this._lblISACube.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblISACube.Location = new System.Drawing.Point(21, 216);
            this._lblISACube.Name = "_lblISACube";
            this._lblISACube.Size = new System.Drawing.Size(84, 18);
            this._lblISACube.TabIndex = 5;
            this._lblISACube.Text = "Cube (in3)";
            this._lblISACube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLCube
            // 
            this._lblTLCube.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTLCube.Location = new System.Drawing.Point(21, 129);
            this._lblTLCube.Name = "_lblTLCube";
            this._lblTLCube.Size = new System.Drawing.Size(84, 18);
            this._lblTLCube.TabIndex = 3;
            this._lblTLCube.Text = "Cube (in3)";
            this._lblTLCube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLWeight
            // 
            this._lblTLWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTLWeight.Location = new System.Drawing.Point(21, 102);
            this._lblTLWeight.Name = "_lblTLWeight";
            this._lblTLWeight.Size = new System.Drawing.Size(84, 18);
            this._lblTLWeight.TabIndex = 2;
            this._lblTLWeight.Text = "Weight (lbs)";
            this._lblTLWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLPallets
            // 
            this._lblTLPallets.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTLPallets.Location = new System.Drawing.Point(21, 75);
            this._lblTLPallets.Name = "_lblTLPallets";
            this._lblTLPallets.Size = new System.Drawing.Size(84, 18);
            this._lblTLPallets.TabIndex = 1;
            this._lblTLPallets.Text = "Pallets";
            this._lblTLPallets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLCartons
            // 
            this._lblTLCartons.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTLCartons.Location = new System.Drawing.Point(21, 48);
            this._lblTLCartons.Name = "_lblTLCartons";
            this._lblTLCartons.Size = new System.Drawing.Size(84, 18);
            this._lblTLCartons.TabIndex = 0;
            this._lblTLCartons.Text = "Cartons";
            this._lblTLCartons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(3, 305);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(661, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 15;
            this.stbMain.TerminalText = "Terminal";
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
            this.msMain.Size = new System.Drawing.Size(760, 24);
            this.msMain.TabIndex = 5;
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
            this.mnuFilePageSetup,
            this.mnuFilePreview,
            this.mnuFilePrint,
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
            this.mnuFileNew.Size = new System.Drawing.Size(152, 22);
            this.mnuFileNew.Text = "New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.OpenFolder;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(152, 22);
            this.mnuFileOpen.Text = "Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.mnuFileSaveAs.Text = "Save As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFilePageSetup
            // 
            this.mnuFilePageSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.mnuFilePageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePageSetup.Name = "mnuFilePageSetup";
            this.mnuFilePageSetup.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePageSetup.Text = "Page Setup...";
            this.mnuFilePageSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(152, 22);
            this.mnuFilePreview.Text = "Print Preveiw...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
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
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(152, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditSearch});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.Size = new System.Drawing.Size(118, 22);
            this.mnuEditSearch.Text = "Search...";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewAgentSummary,
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
            this.mnuViewRefresh.Size = new System.Drawing.Size(169, 22);
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(166, 6);
            // 
            // mnuViewAgentSummary
            // 
            this.mnuViewAgentSummary.Image = global::Argix.Properties.Resources.PieChart;
            this.mnuViewAgentSummary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewAgentSummary.Name = "mnuViewAgentSummary";
            this.mnuViewAgentSummary.Size = new System.Drawing.Size(169, 22);
            this.mnuViewAgentSummary.Text = "Agent Summary...";
            this.mnuViewAgentSummary.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(166, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(169, 22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(169, 22);
            this.mnuViewStatusBar.Text = "StatusBar";
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
            this.mnuToolsConfig.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
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
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(158, 22);
            this.mnuHelpAbout.Text = "About TLViewer";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(155, 6);
            this.mnuHelpSep1.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnSave,
            this.btnPrint,
            this.btnSep2,
            this.btnSearch,
            this.btnRefresh,
            this.btnAgentSummary});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(760, 25);
            this.tsMain.TabIndex = 6;
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.Document;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.ToolTipText = "New...";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.OpenFolder;
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
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.ToolTipText = "Print TLView...";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Find;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.ToolTipText = "Search...";
            this.btnSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23, 22);
            this.btnRefresh.ToolTipText = "Refresh TL\'s";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnAgentSummary
            // 
            this.btnAgentSummary.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAgentSummary.Image = global::Argix.Properties.Resources.PieChart;
            this.btnAgentSummary.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAgentSummary.Name = "btnAgentSummary";
            this.btnAgentSummary.Size = new System.Drawing.Size(23, 22);
            this.btnAgentSummary.ToolTipText = "Agent Summary...";
            this.btnAgentSummary.Click += new System.EventHandler(this.OnItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(760, 488);
            this.Controls.Add(this.grdTLs);
            this.Controls.Add(this.grpTotals);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TLViewer";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).EndInit();
            this.grdTLs.ResumeLayout(false);
            this.grdTLs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._picSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLs)).EndInit();
            this.grpTotals.ResumeLayout(false);
            this.grpTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updISAWeight)).EndInit();
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
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				this.mToolTip.SetToolTip(this.cboTerminals, "Select an enterprise terminal for the TL and Agent Summary views.");
				this.mToolTip.SetToolTip(this.updISAWeight, "Enter ISA weight in lbs.");
				#endregion
				
				//Set control defaults
				#region Grid initialization
				this.grdTLs.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdTLs.DisplayLayout.Bands[0].Columns["TLNumber"].SortIndicator = SortIndicator.Ascending;
				#endregion
                TerminalInfo t = TLViewerProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(), t.Description);
                this.stbMain.User1Panel.Width = 144;
                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(TLViewerProxy.ServiceState,TLViewerProxy.ServiceAddress)); 
                this.mTerminals.DataSource = TLViewerProxy.GetTerminals(App.Config.EnableAllTerminals ? 0 : App.Config.LocalTerminalID);
                //if(this.cboTerminals.Items.Count > 0) this.cboTerminals.SelectedValue = App.Config.LocalTerminalID;
                if(this.cboTerminals.Items.Count > 0) this.cboTerminals.SelectedIndex = 0;
                OnTerminalSelected(this.cboTerminals,EventArgs.Empty);
            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
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
        private void OnFormResize(object sender,System.EventArgs e) {
            //Event handler for form resized event
        }
		private void OnTerminalSelected(object sender, System.EventArgs e) {
			//Event handler for change in combobox terminal selection
			try {
				this.mMessageMgr.AddMessage("Changing the active terminal for TL's...");
                this.mnuViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnTLsSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Update totals
			UltraGridRow oRow=null;
			long lTotalWeight=0, lTotalCube=0, lTotalCartons=0, lTotalPallets=0;
			this.Cursor = Cursors.WaitCursor;
			try {
				//Steal focus from combobox
				this.grdTLs.Focus();
				
				//Set selected count; total selected column values
				this.mMessageMgr.AddMessage("Updating totals for the selected TL's...");
				this.lblSelectedTLs.Text = this.grdTLs.Selected.Rows.Count.ToString();
				for(int i=0; i<this.grdTLs.Selected.Rows.Count; i++) {
					oRow = this.grdTLs.Selected.Rows[i];
					lTotalWeight += Convert.ToInt64(oRow.Cells["Weight"].Value);
					lTotalCube += Convert.ToInt64(oRow.Cells["Cube"].Value);
					lTotalCartons += Convert.ToInt64(oRow.Cells["Cartons"].Value);
					lTotalPallets += Convert.ToInt64(oRow.Cells["Pallets"].Value);
				}
				this.txtTLWeight.Text = lTotalWeight.ToString("#,0");
				this.txtTLCube.Text = lTotalCube.ToString("#,0");
				this.txtTLPallets.Text = lTotalPallets.ToString("#,0");
				this.txtTLCartons.Text = lTotalCartons.ToString("#,0");
				updateGrandTotals();
			} 
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnISAWeightChanged(object sender, System.EventArgs e) {
			//Update totals and grand totlas with ISA weight changes
			float isaCube=0f;
			try {
                isaCube = ((float)App.Config.TrailerFullCube / (float)App.Config.TrailerFullWeight) * (float)this.updISAWeight.Value;
				this.txtISACube.Text = isaCube.ToString("#,0");
				updateGrandTotals();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void updateGrandTotals() {
			//Update totals for weight and cube to include ISA values
			float fGrandTotalWeight=0f, fGrandTotalCube=0f, fTrailerWeightPercent=0f, fTrailerCubePercent=0f;
			
			//Calculate
            fGrandTotalWeight = float.Parse(this.txtTLWeight.Text,System.Globalization.NumberStyles.AllowThousands) + (float)this.updISAWeight.Value;
			fGrandTotalCube = float.Parse(this.txtTLCube.Text, System.Globalization.NumberStyles.AllowThousands) + float.Parse(this.txtISACube.Text, System.Globalization.NumberStyles.AllowThousands);
            fTrailerWeightPercent = (fGrandTotalWeight / App.Config.TrailerFullWeight) * 100;
            fTrailerCubePercent = (fGrandTotalCube / App.Config.TrailerFullCube) * 100;
			
			//Update display
			this.txtGrandTotalWeight.Text = fGrandTotalWeight.ToString("#,0");
			this.txtGrandTotalCube.Text = fGrandTotalCube.ToString("#,0");
			this.txtLoadWeightRatio.Text = fTrailerWeightPercent.ToString("#0");
			this.txtTrailerCubeRatio.Text = fTrailerCubePercent.ToString("#0");
		}
		#region Grid Support: OnGridInitializeLayout(), OnGridInitializeRow(), OnSearchValueChanged()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//Calculate derived columns
                e.Row.Cells["WeightPercent"].Value = (Convert.ToSingle(e.Row.Cells["Weight"].Value) / App.Config.TrailerFullWeight) * 100;
                e.Row.Cells["CubePercent"].Value = (Convert.ToSingle(e.Row.Cells["Cube"].Value) / App.Config.TrailerFullCube) * 100;
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnSearchValueChanged(object sender, System.EventArgs e) {
			//Event handler for change in search text value
			try { this.grdTLs.Selected.Rows.Clear(); } 
			catch { }
		}
		#endregion
        #region User Services: OnMenuClicked(), OnHelpMenuClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Menu services
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
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
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Data Files (*.xml) | *.xml";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save TL View As...";
                        dlgSave.FileName = this.Text;
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(TLs));
                            FileStream fs = new FileStream(dlgSave.FileName,FileMode.Create,FileAccess.Write);
                            dcs.WriteObject(fs,this.mTLs.DataSource);
                            fs.Flush();
                            fs.Close();
                        }
                        break;
                    case "mnuFilePageSetup":    UltraGridPrinter.PageSettings(); break;
                    case "mnuFilePrint":    UltraGridPrinter.Print(this.grdTLs,"TL View for " + this.cboTerminals.Text + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),true); break;
                    case "btnPrint":    UltraGridPrinter.Print(this.grdTLs,"TL View for " + this.cboTerminals.Text + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),false); break;
                    case "mnuFilePreview":      UltraGridPrinter.PrintPreview(this.grdTLs,"TL View for " + this.cboTerminals.Text + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")); break;
					case "mnuFileExit":			this.Close(); break;
					case "mnuEditSearch":			
                    case "btnSearch":
                        this.mGridSvc.FindRow(0, this.grdTLs.Tag.ToString(), this.txtSearchSort.Text); 
                        break;
					case "mnuViewRefresh":
				    case "btnRefresh":
						this.Cursor = Cursors.WaitCursor;
                        this.mMessageMgr.AddMessage("Refreshing TL's...");
                        this.grdTLs.Text = "TL's for       " + this.cboTerminals.SelectedValue.ToString();
                        this.mTLs.DataSource = TLViewerProxy.GetTLView(Convert.ToInt32(this.cboTerminals.SelectedValue));
                        if(this.grdTLs.Rows.Count > 0)
                            this.grdTLs.ActiveRow = this.grdTLs.Rows[0];
                        OnTLsSelected(this.grdTLs,null);
                        break;
					case "mnuViewAgentSummary":
                    case "btnAgentSummary":
                        dlgAgentSummary dlgAS = new dlgAgentSummary(Convert.ToInt32(this.cboTerminals.SelectedValue),this.cboTerminals.Text);
						dlgAS.ShowDialog(this);
						break;
					case "mnuViewToolbar":
						this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked);
						this.tsMain.Visible = this.mnuViewToolbar.Checked;
						if(this.tsMain.Visible) {
							this.grpTotals.Top += this.tsMain.Height;
							this.grpTotals.Height -= this.tsMain.Height;
							this.grdTLs.Top += this.tsMain.Height;
							this.grdTLs.Height -= this.tsMain.Height;
						}
						else {
							this.grpTotals.Top -= this.tsMain.Height;
							this.grpTotals.Height += this.tsMain.Height;
							this.grdTLs.Top -= this.tsMain.Height;
							this.grdTLs.Height += this.tsMain.Height;
						}
						break;
					case "mnuViewStatusBar":
						this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked);
						this.stbMain.Visible = this.mnuViewStatusBar.Checked;
						if(this.stbMain.Visible) {
							this.grpTotals.Height -= this.stbMain.Height;
							this.grdTLs.Height -= this.stbMain.Height;
						}
						else {
							this.grpTotals.Height += this.stbMain.Height;
							this.grdTLs.Height += this.stbMain.Height;
						}
						break;
                    case "mnuToolsConfig": 
                        App.ShowConfig();
                        this.mTerminals.DataSource = TLViewerProxy.GetTerminals(App.Config.EnableAllTerminals ? 0 : App.Config.LocalTerminalID);
                        if(this.cboTerminals.Items.Count > 0) this.cboTerminals.SelectedIndex = 0;
                        OnTerminalSelected(this.cboTerminals,EventArgs.Empty);
                        break;
					case "mnuHelpAbout": new dlgAbout(App.Description, App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                MenuItem mnu = (MenuItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(mnu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu(), OnHelpMenuClick()
		private void configApplication() {
			try {				
				//Create business objects with configuration values
            }
			catch(Exception ex) { throw new ApplicationException("Configuration Failure", ex); } 
		}
		private void setUserServices() {
			//Set user services
			try {				
				//Set menu states
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
                this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = (this.grdTLs.Rows.Count > 0);
				this.mnuFilePageSetup.Enabled = true;
                this.mnuFilePrint.Enabled = (this.grdTLs.Rows.Count > 0);
                this.mnuFilePreview.Enabled = (this.grdTLs.Rows.Count > 0);
				this.mnuFileExit.Enabled = true;
				this.mnuEditSearch.Enabled = this.btnSearch.Enabled = (this.txtSearchSort.Text.Length > 0);
				this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = true;
                this.mnuViewAgentSummary.Enabled = this.btnAgentSummary.Enabled = App.Config.EnableAgentReport;
				this.mnuHelpAbout.Enabled = true;

                this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
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
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
        #endregion
	}
}
