using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Kronos;
using Argix.Windows;
using Argix.Windows.Printers;

namespace Argix {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private Employee mEmployee=null;
		private UltraGridSvc mGridSvc=null;
		private WinPrinter mPrinter=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		#region Controls
		private System.ComponentModel.IContainer components;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdEmployees;
        private Argix.Windows.ArgixStatusBar stbMain;
		private System.Windows.Forms.PictureBox picPhoto;
		private System.Windows.Forms.PictureBox picSignature;
		private System.Windows.Forms.Label lblBadge;
		private System.Windows.Forms.Panel pnlProfile;
		private System.Windows.Forms.Label _lblOrganization;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label _lblDepartment;
		private System.Windows.Forms.Label _lblLocation;
		private System.Windows.Forms.Label _lblOffice;
		private System.Windows.Forms.Label lblOrganization;
		private System.Windows.Forms.Label lblDepartment;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblOffice;
        private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.ComboBox cboIDType;
        private MenuStrip msMain;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripMenuItem mnuFileExport;
        private ToolStripMenuItem mnuFileEmail;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFilePageSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePrintEmp;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripSeparator mnuFileSep4;
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
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStrip tsMain;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripButton btnExport;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnPrint;
        private ToolStripButton btnEmail;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnCut;
        private ToolStripButton btnCopy;
        private ToolStripButton btnPaste;
        private ToolStripButton btnSearch;
        private ToolStripButton btnRefresh;
        private ToolStripSeparator btnSep3;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem ctxCut;
        private ToolStripMenuItem ctxCopy;
        private ToolStripMenuItem ctxPaste;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem ctxSaveAs;
        private ToolStripSeparator ctxSep2;
        private ToolStripMenuItem ctxPrint;
        private BindingSource bsEmployees;
		private System.Windows.Forms.TextBox txtSearchSort;
		#endregion
		
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.Text = "Argix Logistics " + App.Product;
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);
				#region Set window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.grdEmployees.Dock = DockStyle.Fill;
				this.pnlProfile.Dock = DockStyle.Right;
				this.stbMain.Dock = DockStyle.Bottom;
				this.grdEmployees.Controls.AddRange(new Control[]{this.cboIDType, this.txtSearchSort});
				this.cboIDType.Top = this.txtSearchSort.Top = 1;
				this.cboIDType.Left = 1;
				this.txtSearchSort.Left = this.grdEmployees.Width - this.txtSearchSort.Width - 3;
				this.Controls.AddRange(new Control[]{this.grdEmployees, this.pnlProfile, this.tsMain, this.msMain, this.stbMain});
				#endregion
				
				//Create data and UI services
				this.mGridSvc = new UltraGridSvc(this.grdEmployees, this.txtSearchSort);
				this.mPrinter = new WinPrinter();
				this.mPrinter.Doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(OnPrintPage);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500, 3000);
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Employee", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BadgeNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DOB");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Department");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EmployeeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ExpirationDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Faccode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HasPhoto");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HasSignature");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HireDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IDNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IssueDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Middle");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Organization");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Photo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Signature");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StatusDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubLocation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Suffix");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grdEmployees = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.bsEmployees = new System.Windows.Forms.BindingSource(this.components);
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.txtSearchSort = new System.Windows.Forms.TextBox();
            this.lblBadge = new System.Windows.Forms.Label();
            this._lblOrganization = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblOffice = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblOrganization = new System.Windows.Forms.Label();
            this._lblOffice = new System.Windows.Forms.Label();
            this._lblLocation = new System.Windows.Forms.Label();
            this._lblDepartment = new System.Windows.Forms.Label();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            this.picSignature = new System.Windows.Forms.PictureBox();
            this.cboIDType = new System.Windows.Forms.ComboBox();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrintEmp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep4 = new System.Windows.Forms.ToolStripSeparator();
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
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnEmail = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCut = new System.Windows.Forms.ToolStripButton();
            this.btnCopy = new System.Windows.Forms.ToolStripButton();
            this.btnPaste = new System.Windows.Forms.ToolStripButton();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdEmployees)).BeginInit();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEmployees)).BeginInit();
            this.pnlProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).BeginInit();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdEmployees
            // 
            this.grdEmployees.ContextMenuStrip = this.csMain;
            this.grdEmployees.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdEmployees.DataSource = this.bsEmployees;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdEmployees.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn1.Header.Caption = "Badge#";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.VisiblePosition = 15;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.VisiblePosition = 8;
            ultraGridColumn3.Width = 120;
            ultraGridColumn4.Header.Caption = "ID";
            ultraGridColumn4.Header.VisiblePosition = 14;
            ultraGridColumn4.Width = 60;
            ultraGridColumn5.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn5.Format = "MM/dd/yyyy";
            ultraGridColumn5.Header.Caption = "Exp Date";
            ultraGridColumn5.Header.VisiblePosition = 19;
            ultraGridColumn5.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn5.Width = 96;
            ultraGridColumn6.Header.Caption = "Code";
            ultraGridColumn6.Header.VisiblePosition = 9;
            ultraGridColumn6.Width = 48;
            ultraGridColumn7.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn7.Header.Caption = "First Name";
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridColumn7.Width = 96;
            ultraGridColumn8.Header.VisiblePosition = 12;
            ultraGridColumn8.Width = 72;
            ultraGridColumn9.Header.VisiblePosition = 13;
            ultraGridColumn9.Width = 72;
            ultraGridColumn10.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn10.Format = "MM/dd/yyyy";
            ultraGridColumn10.Header.Caption = "Hire Date";
            ultraGridColumn10.Header.VisiblePosition = 17;
            ultraGridColumn10.Width = 96;
            ultraGridColumn11.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn11.Header.VisiblePosition = 0;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn11.Width = 60;
            ultraGridColumn12.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn12.Format = "MM/dd/yyyy";
            ultraGridColumn12.Header.Caption = "Issue Date";
            ultraGridColumn12.Header.VisiblePosition = 18;
            ultraGridColumn12.Width = 96;
            ultraGridColumn13.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn13.Header.Caption = "Last Name";
            ultraGridColumn13.Header.VisiblePosition = 2;
            ultraGridColumn13.Width = 96;
            ultraGridColumn14.Header.VisiblePosition = 10;
            ultraGridColumn14.Width = 96;
            ultraGridColumn15.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn15.Header.Caption = "Mid";
            ultraGridColumn15.Header.VisiblePosition = 4;
            ultraGridColumn15.Width = 48;
            ultraGridColumn16.Header.VisiblePosition = 7;
            ultraGridColumn16.Width = 120;
            ultraGridColumn17.Header.VisiblePosition = 20;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.VisiblePosition = 21;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.VisiblePosition = 6;
            ultraGridColumn19.Width = 72;
            ultraGridColumn20.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn20.Format = "MM/dd/yyyy";
            ultraGridColumn20.Header.Caption = "Status Date";
            ultraGridColumn20.Header.VisiblePosition = 16;
            ultraGridColumn20.Width = 96;
            ultraGridColumn21.Header.VisiblePosition = 11;
            ultraGridColumn21.Width = 96;
            ultraGridColumn22.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn22.Header.VisiblePosition = 5;
            ultraGridColumn22.Width = 60;
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
            ultraGridColumn22});
            this.grdEmployees.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdEmployees.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 9F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.grdEmployees.DisplayLayout.CaptionAppearance = appearance2;
            this.grdEmployees.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdEmployees.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdEmployees.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdEmployees.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdEmployees.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdEmployees.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdEmployees.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdEmployees.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdEmployees.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdEmployees.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdEmployees.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdEmployees.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdEmployees.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdEmployees.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdEmployees.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdEmployees.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdEmployees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdEmployees.Location = new System.Drawing.Point(0, 49);
            this.grdEmployees.Name = "grdEmployees";
            this.grdEmployees.Size = new System.Drawing.Size(522, 448);
            this.grdEmployees.TabIndex = 2;
            this.grdEmployees.Text = "Argix Employees";
            this.grdEmployees.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdEmployees.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnAfterRowFilterChanged);
            this.grdEmployees.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnBeforeRowFilterDropDownPopulate);
            this.grdEmployees.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnEmployeeSelected);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxCut,
            this.ctxCopy,
            this.ctxPaste,
            this.ctxSep1,
            this.ctxSaveAs,
            this.ctxSep2,
            this.ctxPrint});
            this.csMain.Name = "ctxMain";
            this.csMain.Size = new System.Drawing.Size(103, 126);
            // 
            // ctxCut
            // 
            this.ctxCut.Image = global::Argix.Properties.Resources.Cut;
            this.ctxCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCut.Name = "ctxCut";
            this.ctxCut.Size = new System.Drawing.Size(102, 22);
            this.ctxCut.Text = "Cut";
            this.ctxCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCopy
            // 
            this.ctxCopy.Image = global::Argix.Properties.Resources.Copy;
            this.ctxCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCopy.Name = "ctxCopy";
            this.ctxCopy.Size = new System.Drawing.Size(102, 22);
            this.ctxCopy.Text = "Copy";
            this.ctxCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxPaste
            // 
            this.ctxPaste.Image = global::Argix.Properties.Resources.Paste;
            this.ctxPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPaste.Name = "ctxPaste";
            this.ctxPaste.Size = new System.Drawing.Size(102, 22);
            this.ctxPaste.Text = "Paste";
            this.ctxPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(99, 6);
            // 
            // ctxSaveAs
            // 
            this.ctxSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.ctxSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxSaveAs.Name = "ctxSaveAs";
            this.ctxSaveAs.Size = new System.Drawing.Size(102, 22);
            this.ctxSaveAs.Text = "Save";
            this.ctxSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(99, 6);
            // 
            // ctxPrint
            // 
            this.ctxPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPrint.Name = "ctxPrint";
            this.ctxPrint.Size = new System.Drawing.Size(102, 22);
            this.ctxPrint.Text = "Print";
            this.ctxPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // bsEmployees
            // 
            this.bsEmployees.DataSource = typeof(Argix.Kronos.Employees);
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 497);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(819, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 5;
            this.stbMain.TerminalText = "Local Terminal";
            // 
            // txtSearchSort
            // 
            this.txtSearchSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSort.Location = new System.Drawing.Point(404, 30);
            this.txtSearchSort.Name = "txtSearchSort";
            this.txtSearchSort.Size = new System.Drawing.Size(96, 21);
            this.txtSearchSort.TabIndex = 6;
            // 
            // lblBadge
            // 
            this.lblBadge.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBadge.Location = new System.Drawing.Point(63, 360);
            this.lblBadge.Name = "lblBadge";
            this.lblBadge.Size = new System.Drawing.Size(172, 18);
            this.lblBadge.TabIndex = 9;
            this.lblBadge.Text = "Badge #00000";
            this.lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblOrganization
            // 
            this._lblOrganization.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOrganization.Location = new System.Drawing.Point(9, 48);
            this._lblOrganization.Name = "_lblOrganization";
            this._lblOrganization.Size = new System.Drawing.Size(96, 18);
            this._lblOrganization.TabIndex = 10;
            this._lblOrganization.Text = "Organization:";
            this._lblOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(287, 21);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "James P Heary";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlProfile
            // 
            this.pnlProfile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlProfile.Controls.Add(this.lblStatus);
            this.pnlProfile.Controls.Add(this.lblOffice);
            this.pnlProfile.Controls.Add(this.lblLocation);
            this.pnlProfile.Controls.Add(this.lblDepartment);
            this.pnlProfile.Controls.Add(this.lblOrganization);
            this.pnlProfile.Controls.Add(this._lblOffice);
            this.pnlProfile.Controls.Add(this._lblLocation);
            this.pnlProfile.Controls.Add(this._lblDepartment);
            this.pnlProfile.Controls.Add(this.picPhoto);
            this.pnlProfile.Controls.Add(this.picSignature);
            this.pnlProfile.Controls.Add(this._lblOrganization);
            this.pnlProfile.Controls.Add(this.lblBadge);
            this.pnlProfile.Controls.Add(this.lblName);
            this.pnlProfile.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlProfile.Location = new System.Drawing.Point(522, 49);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Padding = new System.Windows.Forms.Padding(3);
            this.pnlProfile.Size = new System.Drawing.Size(297, 448);
            this.pnlProfile.TabIndex = 12;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblStatus.Location = new System.Drawing.Point(244, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 13);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Active";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOffice
            // 
            this.lblOffice.Location = new System.Drawing.Point(117, 120);
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.Size = new System.Drawing.Size(168, 18);
            this.lblOffice.TabIndex = 18;
            this.lblOffice.Text = "Office";
            this.lblOffice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(117, 96);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(168, 18);
            this.lblLocation.TabIndex = 17;
            this.lblLocation.Text = "Jamesburg";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(117, 72);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(168, 18);
            this.lblDepartment.TabIndex = 16;
            this.lblDepartment.Text = "IT";
            this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrganization
            // 
            this.lblOrganization.Location = new System.Drawing.Point(117, 48);
            this.lblOrganization.Name = "lblOrganization";
            this.lblOrganization.Size = new System.Drawing.Size(168, 18);
            this.lblOrganization.TabIndex = 15;
            this.lblOrganization.Text = "Argix";
            this.lblOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblOffice
            // 
            this._lblOffice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOffice.Location = new System.Drawing.Point(9, 120);
            this._lblOffice.Name = "_lblOffice";
            this._lblOffice.Size = new System.Drawing.Size(96, 18);
            this._lblOffice.TabIndex = 14;
            this._lblOffice.Text = "Office:";
            this._lblOffice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLocation
            // 
            this._lblLocation.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLocation.Location = new System.Drawing.Point(9, 96);
            this._lblLocation.Name = "_lblLocation";
            this._lblLocation.Size = new System.Drawing.Size(96, 18);
            this._lblLocation.TabIndex = 13;
            this._lblLocation.Text = "Location:";
            this._lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblDepartment
            // 
            this._lblDepartment.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDepartment.Location = new System.Drawing.Point(9, 72);
            this._lblDepartment.Name = "_lblDepartment";
            this._lblDepartment.Size = new System.Drawing.Size(96, 18);
            this._lblDepartment.TabIndex = 12;
            this._lblDepartment.Text = "Department:";
            this._lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPhoto
            // 
            this.picPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPhoto.ContextMenuStrip = this.csMain;
            this.picPhoto.Location = new System.Drawing.Point(63, 153);
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.Size = new System.Drawing.Size(172, 196);
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPhoto.TabIndex = 7;
            this.picPhoto.TabStop = false;
            // 
            // picSignature
            // 
            this.picSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSignature.Location = new System.Drawing.Point(3, 390);
            this.picSignature.Name = "picSignature";
            this.picSignature.Size = new System.Drawing.Size(287, 72);
            this.picSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSignature.TabIndex = 8;
            this.picSignature.TabStop = false;
            // 
            // cboIDType
            // 
            this.cboIDType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIDType.ItemHeight = 13;
            this.cboIDType.Location = new System.Drawing.Point(0, 49);
            this.cboIDType.Name = "cboIDType";
            this.cboIDType.Size = new System.Drawing.Size(204, 21);
            this.cboIDType.TabIndex = 13;
            this.cboIDType.SelectionChangeCommitted += new System.EventHandler(this.OnIDTypeChanged);
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
            this.msMain.Size = new System.Drawing.Size(819, 24);
            this.msMain.TabIndex = 14;
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
            this.mnuFileExport,
            this.mnuFileEmail,
            this.mnuFileSep3,
            this.mnuFilePageSetup,
            this.mnuFilePrint,
            this.mnuFilePrintEmp,
            this.mnuFilePreview,
            this.mnuFileSep4,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.Size = new System.Drawing.Size(163, 22);
            this.mnuFileNew.Text = "New...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Document;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.Size = new System.Drawing.Size(163, 22);
            this.mnuFileOpen.Text = "Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(160, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = global::Argix.Properties.Resources.Save;
            this.mnuFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.Size = new System.Drawing.Size(163, 22);
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(163, 22);
            this.mnuFileSaveAs.Text = "Save As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(160, 6);
            // 
            // mnuFileExport
            // 
            this.mnuFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.mnuFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExport.Name = "mnuFileExport";
            this.mnuFileExport.Size = new System.Drawing.Size(163, 22);
            this.mnuFileExport.Text = "Export...";
            this.mnuFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileEmail
            // 
            this.mnuFileEmail.Image = global::Argix.Properties.Resources.Send;
            this.mnuFileEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileEmail.Name = "mnuFileEmail";
            this.mnuFileEmail.Size = new System.Drawing.Size(163, 22);
            this.mnuFileEmail.Text = "Send As Email";
            this.mnuFileEmail.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(160, 6);
            // 
            // mnuFilePageSetup
            // 
            this.mnuFilePageSetup.Image = global::Argix.Properties.Resources.PageSetup;
            this.mnuFilePageSetup.ImageTransparentColor = System.Drawing.Color.Black;
            this.mnuFilePageSetup.Name = "mnuFilePageSetup";
            this.mnuFilePageSetup.Size = new System.Drawing.Size(163, 22);
            this.mnuFilePageSetup.Text = "Print Setup...";
            this.mnuFilePageSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.mnuFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.Size = new System.Drawing.Size(163, 22);
            this.mnuFilePrint.Text = "Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrintEmp
            // 
            this.mnuFilePrintEmp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePrintEmp.Name = "mnuFilePrintEmp";
            this.mnuFilePrintEmp.Size = new System.Drawing.Size(163, 22);
            this.mnuFilePrintEmp.Text = "Print Employee...";
            this.mnuFilePrintEmp.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.mnuFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(163, 22);
            this.mnuFilePreview.Text = "Print Preview...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep4
            // 
            this.mnuFileSep4.Name = "mnuFileSep4";
            this.mnuFileSep4.Size = new System.Drawing.Size(160, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(163, 22);
            this.mnuFileExit.Text = "Exit";
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
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.mnuEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCut.Text = "Cut";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.mnuEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.Size = new System.Drawing.Size(109, 22);
            this.mnuEditCopy.Text = "Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.mnuEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
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
            this.mnuViewSep1,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.mnuViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.Size = new System.Drawing.Size(123, 22);
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(120, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(123, 22);
            this.mnuViewToolbar.Text = "Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(123, 22);
            this.mnuViewStatusBar.Text = "StatusBar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "Tools";
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
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(165, 22);
            this.mnuHelpAbout.Text = "&About IDViewer...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(162, 6);
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNew,
            this.btnOpen,
            this.btnSave,
            this.btnExport,
            this.btnSep1,
            this.btnPrint,
            this.btnEmail,
            this.btnSep2,
            this.btnCut,
            this.btnCopy,
            this.btnPaste,
            this.btnSearch,
            this.btnSep3,
            this.btnRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(819, 25);
            this.tsMain.TabIndex = 15;
            this.tsMain.Text = "tsMain";
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23, 22);
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Document;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23, 22);
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(23, 22);
            this.btnExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23, 22);
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnEmail
            // 
            this.btnEmail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEmail.Image = global::Argix.Properties.Resources.Send;
            this.btnEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEmail.Name = "btnEmail";
            this.btnEmail.Size = new System.Drawing.Size(23, 22);
            this.btnEmail.Click += new System.EventHandler(this.OnItemClick);
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
            this.btnCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnCopy
            // 
            this.btnCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCopy.Image = global::Argix.Properties.Resources.Copy;
            this.btnCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(23, 22);
            this.btnCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPaste
            // 
            this.btnPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPaste.Image = global::Argix.Properties.Resources.Paste;
            this.btnPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(23, 22);
            this.btnPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSearch.Image = global::Argix.Properties.Resources.Find;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(23, 22);
            this.btnSearch.Click += new System.EventHandler(this.OnItemClick);
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
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(819, 521);
            this.Controls.Add(this.txtSearchSort);
            this.Controls.Add(this.cboIDType);
            this.Controls.Add(this.grdEmployees);
            this.Controls.Add(this.pnlProfile);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix IDViewer";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closed += new System.EventHandler(this.OnFormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.grdEmployees)).EndInit();
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsEmployees)).EndInit();
            this.pnlProfile.ResumeLayout(false);
            this.pnlProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        
        private void OnFormLoad(object sender,System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				Splash.Close();
				this.Visible = true;
				Application.DoEvents();
				#region Set user preferences
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
                #endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
                #region Grid initialization
				this.grdEmployees.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdEmployees.DisplayLayout.Bands[0].Columns["LastName"].SortIndicator = SortIndicator.Ascending;
				this.grdEmployees.DisplayLayout.Bands[0].ColumnFilters["Status"].FilterConditions.Clear();
				this.grdEmployees.DisplayLayout.Bands[0].ColumnFilters["Status"].FilterConditions.Add(FilterComparisionOperator.Equals, "Active");
				this.grdEmployees.DisplayLayout.RefreshFilters();
                #endregion

                //Set control defaults
                TerminalInfo t = KronosProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.stbMain.User1Panel.Width = 144;
                object[] idTypes = null;
                idTypes = KronosProxy.GetIDTypes();
                if(this.grdEmployees.Rows.VisibleRowCount > 0) {
                    this.grdEmployees.Rows.GetRowAtVisibleIndex(0).Selected = true;
                    this.grdEmployees.Rows.GetRowAtVisibleIndex(0).Activate();
                }
                for(int i=0;i<idTypes.Length;i++) this.cboIDType.Items.Add(idTypes[i]);
				if(this.cboIDType.Items.Count > 0) this.cboIDType.SelectedIndex = 0;
				OnIDTypeChanged(null, EventArgs.Empty);
			}
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
		private void OnFormClosed(object sender, System.EventArgs e) {
            //Event handler for form closed event
            global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
            global::Argix.Properties.Settings.Default.Location = this.Location;
            global::Argix.Properties.Settings.Default.Size = this.Size;
            global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
            global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
            global::Argix.Properties.Settings.Default.LastVersion = App.Version;
            global::Argix.Properties.Settings.Default.Save();
        }
        private void OnIDTypeChanged(object sender,EventArgs e) {
            //Event handler for change in ID type (i.e. Driver, Employee, Vendor, etc.)
            this.Cursor = Cursors.WaitCursor;
            try {
                //
                this.mMessageMgr.AddMessage("Refreshing employees...");
                this.bsEmployees.DataSource = KronosProxy.GetEmployees(this.cboIDType.SelectedItem.ToString());
                TerminalInfo t = KronosProxy.GetTerminalInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.stbMain.User1Panel.Width = 144;
                if(this.grdEmployees.Rows.VisibleRowCount > 0) {
                    this.grdEmployees.Rows.GetRowAtVisibleIndex(0).Selected = true;
                    this.grdEmployees.Rows.GetRowAtVisibleIndex(0).Activate();
                }
                OnEmployeeSelected(null,null);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnEmployeeSelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Update totals
            this.Cursor = Cursors.WaitCursor;
            try {
                //Steal focus from search textbox
                this.grdEmployees.Focus();

                //Set current employee
                this.mEmployee = null;
                if(this.grdEmployees.Selected.Rows.Count > 0) {
                    this.mMessageMgr.AddMessage("Retrieving employee...");
                    this.mEmployee = (Employee)this.bsEmployees.Current;
                }
                OnEmployeeChanged(null,null);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnEmployeeChanged(object sender,EventArgs e) {
            //Event handler for change in employee
            try {
                if(this.mEmployee != null) {
                    //Set details of the selected employee
                    this.lblName.Text = this.mEmployee.FirstName.Trim() + " " + this.mEmployee.Middle.Trim() + " " + this.mEmployee.LastName.Trim();
                    this.lblStatus.Text = this.mEmployee.Status;
                    switch(this.mEmployee.Status) {
                        case "Active":
                            this.lblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
                            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                            break;
                        case "Terminated":
                            this.lblName.BackColor = System.Drawing.Color.Red;
                            this.lblStatus.ForeColor = System.Drawing.Color.Red;
                            break;
                        default:
                            this.lblName.BackColor = System.Drawing.Color.Yellow;
                            this.lblStatus.ForeColor = System.Drawing.Color.Yellow;
                            break;
                    }
                    this.lblOrganization.Text = this.mEmployee.Organization;
                    this.lblDepartment.Text = this.mEmployee.Department;
                    this.lblLocation.Text = this.mEmployee.Location;
                    this.lblOffice.Text = this.mEmployee.SubLocation;
                    this.lblBadge.Text = "Badge# " + this.mEmployee.BadgeNumber;
                    this.picPhoto.Image = this.mEmployee.Photo != null ? Image.FromStream(new MemoryStream(this.mEmployee.Photo)) : null;
                    this.picSignature.Image = this.mEmployee.Signature != null ? Image.FromStream(new MemoryStream(this.mEmployee.Signature)) : null;
                }
                else {
                    this.lblName.Text = "";
                    this.lblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
                    this.lblStatus.Text = "";
                    this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
                    this.lblOrganization.Text = this.lblDepartment.Text = "";
                    this.lblLocation.Text = this.lblOffice.Text = "";
                    this.lblBadge.Text = "Badge# " + "";
                    this.picPhoto.Image = this.picSignature.Image = null;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        #region Grid servics: OnBeforeRowFilterDropDownPopulate(), OnAfterRowFilterChanged(), OnGridMouseDown()
		private void OnBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
			//Event handler for before row filter drop down populates
			try {
				//Removes only blanks and non-blanks from default filter
				e.ValueList.ValueListItems.Remove(3);
				e.ValueList.ValueListItems.Remove(2);
				e.ValueList.ValueListItems.Remove(1);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnAfterRowFilterChanged(object sender, Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs e) {
			//Event handler for change in column row filtering
			try {
				//Set a valid selection
				if(this.grdEmployees.Rows.VisibleRowCount > 0) {
					this.grdEmployees.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdEmployees.Rows.GetRowAtVisibleIndex(0).Activate();
				}
				OnEmployeeSelected(null, null);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			try {
				//Set menu and toolbar services
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(oUIElement != null) {
					object oContext = oUIElement.GetContext(typeof(UltraGridRow));
					if(oContext != null) {
						if(e.Button == MouseButtons.Left) {
							//OnDragDropMouseDown(sender, e);
						}
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow oRow = (UltraGridRow)oContext;
							if(!oRow.Selected) grid.Selected.Rows.Clear();
							oRow.Selected = true;
							oRow.Activate();
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
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#endregion
        #region User Services: OnItemClick(), OnHelpMenuClick(), OnPrintPage()
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
				    case "ctxSaveAs":
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
                        dlgSave.Filter = "Photo files (*.jpg)|*.jpg|Image files (*.gif)|*.gif";
						dlgSave.FilterIndex = 1;
						dlgSave.Title = "Save Photo As...";
						dlgSave.OverwritePrompt = true;
                        dlgSave.FileName = this.mEmployee.LastName + ", " + this.mEmployee.FirstName;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
							if(dlgSave.FileName.Length > 0) {
								Image img = this.picPhoto.Image;
								Size size = new Size(img.Width, img.Height);
								Bitmap bmp = new Bitmap(size.Width, size.Height);
								Graphics g = Graphics.FromImage(bmp);
								g.SmoothingMode = SmoothingMode.HighQuality;
								g.InterpolationMode = InterpolationMode.HighQualityBicubic;
								g.PixelOffsetMode = PixelOffsetMode.HighQuality;
								Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);
								g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
								if(dlgSave.FilterIndex == 2) {
									foreach(PropertyItem item in img.PropertyItems) 
										bmp.SetPropertyItem(item);
									bmp.Save(dlgSave.FileName, System.Drawing.Imaging.ImageFormat.Gif);
								}
								else {
									ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
									ImageCodecInfo codec = null;
									for(int i=0; i<codecs.Length; i++) {
										if(codecs[i].MimeType.Equals("image/jpeg")) {
											codec = codecs[i];
											break;
										}
									}
									if (codec != null) {
										EncoderParameters encoderParams = new EncoderParameters(2);
										encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 80L);
										encoderParams.Param[1] = new EncoderParameter(Encoder.ColorDepth, 24L);
										bmp.Save(dlgSave.FileName, codec, encoderParams);
									}
									else
										bmp.Save(dlgSave.FileName, ImageFormat.Jpeg);
								}
							}
						}
						break;
					case "mnuFileExport":		
					case "btnExport":	
						break;
					case "mnuFilePageSetup":	
					    UltraGridPrinter.PageSettings(); 
					    break;
					case "mnuFilePrint":
					case "ctxPrint":
                        this.Cursor = Cursors.WaitCursor; 
                        UltraGridPrinter.Print(this.grdEmployees,"Argix Employees",true);
                        break;
                    case "btnPrint":
                        this.Cursor = Cursors.WaitCursor;
                        UltraGridPrinter.Print(this.grdEmployees,"Argix Employees",false);
                        break;
					case "mnuFilePrintEmp":		
                        this.Cursor = Cursors.WaitCursor; 
                        this.mPrinter.Print(this.mEmployee.IDNumber.ToString(), " ", true); 
                        break;
					case "mnuFilePreview":		UltraGridPrinter.PrintPreview(this.grdEmployees, "Argix Employees"); break;
					case "mnuFileEmail":
					case "btnEmail":
						Microsoft.Office.Interop.Outlook.ApplicationClass app = new Microsoft.Office.Interop.Outlook.ApplicationClass();
						Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
						//mail.Recipients.Add
						mail.Subject = this.mEmployee.LastName + ", " + this.mEmployee.FirstName;
						Bitmap _bmp = new Bitmap(this.picPhoto.Image);
						_bmp.Save("c:\\temp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
						mail.Attachments.Add("c:\\temp.bmp", Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, 1, this.mEmployee.LastName + ", " + this.mEmployee.FirstName);
						mail.Display(this);
						break;
					case "mnuFileExit":			
					    this.Close(); 
					    break;
					case "mnuEditCut":			
					case "btnCut":
				    case "ctxCut":
					    break;
					case "mnuEditCopy":
                    case "btnCopy":		
                    case "ctxCopy":
                        //Clipboard.SetDataObject(this.mEmployee.Photo, true);
                        Clipboard.SetImage(this.picPhoto.Image);
					    break;
					case "mnuEditPaste":
                    case "btnPaste":	
                    case "ctxPaste":
					    break;
					case "mnuEditSearch":
                    case "btnSearch": 
                        this.mGridSvc.FindRow(0,this.grdEmployees.Tag.ToString(),this.txtSearchSort.Text); 
                        break;
                    case "mnuViewRefresh":
                    case "btnRefresh": 
                        OnIDTypeChanged(null,EventArgs.Empty); 
                        break;
                    case "mnuViewToolbar":      
                        this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); 
                        break;
                    case "mnuViewStatusBar":    
                        this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); 
                        break;
                    case "mnuToolsConfig":      
                        App.ShowConfig(); 
                        break;
                    case "mnuHelpAbout": 
                        new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); 
                        break;
                }
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnPrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			//Provide the printing logic for the document
			try {
				//Print the current employee
				Font font = new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((System.Byte)(0)));
				float fX = e.MarginBounds.Left;
				float fY = e.MarginBounds.Top;
				StringFormat format = new StringFormat();
				float lineHeight = font.GetHeight(e.Graphics);
				
				e.Graphics.DrawString("Name:         " + this.mEmployee.FirstName.Trim() + " " + this.mEmployee.Middle.Trim() + " " + this.mEmployee.LastName.Trim(), font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Status:       " + this.mEmployee.Status, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				
				e.Graphics.DrawString("Organization: " + this.mEmployee.Organization, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Department:   " + this.mEmployee.Department, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Location:     " + this.mEmployee.Location, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Office:       " + this.mEmployee.SubLocation, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;

                e.Graphics.DrawImage(Image.FromStream(new MemoryStream(this.mEmployee.Photo)),fX,fY,Image.FromStream(new MemoryStream(this.mEmployee.Photo)).Width * 2,Image.FromStream(new MemoryStream(this.mEmployee.Photo)).Height * 2);
                fY += Image.FromStream(new MemoryStream(this.mEmployee.Photo)).Height * 2;
				e.Graphics.DrawString("", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Badge# " + this.mEmployee.BadgeNumber, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
                e.Graphics.DrawImage(Image.FromStream(new MemoryStream(this.mEmployee.Signature)),fX,fY);
				e.HasMorePages = false;
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
		}
		#endregion
		#region Local Services: configApplication(), setUserServices(), buildHelpMenu()
		private void configApplication() {
			try {
                //Create business objects with configuration values
                this.stbMain.SetTerminalPanel("","KRONOS");			
			}
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); }
        }
		private void setUserServices() {
			//Set user services
			try {				
				//Set menu states
				this.mnuFileNew.Enabled = this.btnNew.Enabled = false;
				this.mnuFileOpen.Enabled = this.btnOpen.Enabled = false;
				this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = this.ctxSaveAs.Enabled = (this.mEmployee != null && this.mEmployee.Photo != null);
                this.mnuFileExport.Enabled = this.btnExport.Enabled = false;    // (this.grdEmployees.Rows.Count > 0);
				this.mnuFilePageSetup.Enabled = true;
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = (this.grdEmployees.Rows.Count > 0);
                this.mnuFilePrintEmp.Enabled = this.ctxPrint.Enabled = (this.mEmployee != null && this.mEmployee.Photo != null);
                this.mnuFilePreview.Enabled = (this.mEmployee != null && this.mEmployee.Photo != null);
                this.mnuFileEmail.Enabled = this.btnEmail.Enabled = (this.mEmployee != null && this.mEmployee.Photo != null);
				this.mnuFileExit.Enabled = true;
				this.mnuEditCut.Enabled = this.btnCut.Enabled = this.ctxCut.Enabled = false;
                this.mnuEditCopy.Enabled = this.btnCopy.Enabled = this.ctxCopy.Enabled = (this.mEmployee != null && this.mEmployee.Photo != null);
                this.mnuEditPaste.Enabled = this.btnPaste.Enabled = this.ctxPaste.Enabled = false;
				this.mnuEditSearch.Enabled = this.btnSearch.Enabled = (this.txtSearchSort.Text.Length > 0);
				this.mnuViewRefresh.Enabled = this.btnRefresh.Enabled = true;
                this.mnuToolsConfig.Enabled = true;
                this.mnuHelpAbout.Enabled = true;

                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(KronosProxy.ServiceState,KronosProxy.ServiceAddress));
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
		#endregion
	}
}
