namespace Argix.CustomerSvc {
    partial class IssueExplorer {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("IssueTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RegionNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DistrictNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StoreNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD1FromDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD1ToDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PROID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionCreated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionCreated",-1,null,0,Infragistics.Win.UltraWinGrid.SortIndicator.Descending,false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Coordinator");
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IssueExplorer));
            this.grdIssues = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxCtl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxPageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxRefreshCache = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCtxAutoRefreshOn = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxContacts = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.issueDSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.issueDS = new Argix.CustomerSvc.IssueDS();
            this.tlsCtl = new System.Windows.Forms.ToolStrip();
            this.cboView = new System.Windows.Forms.ToolStripComboBox();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSaveAs = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSetup = new System.Windows.Forms.ToolStripButton();
            this.btnPreview = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnContacts = new System.Windows.Forms.ToolStripButton();
            this.btnProperties = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.cboSearch = new System.Windows.Forms.ToolStripComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).BeginInit();
            this.ctxCtl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.issueDSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueDS)).BeginInit();
            this.tlsCtl.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdIssues
            // 
            this.grdIssues.ContextMenuStrip = this.ctxCtl;
            this.grdIssues.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdIssues.DataMember = "IssueTable";
            this.grdIssues.DataSource = this.issueDSBindingSource;
            appearance33.BackColor = System.Drawing.SystemColors.Window;
            appearance33.FontData.Name = "Verdana";
            appearance33.FontData.SizeInPoints = 8F;
            appearance33.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance33.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.Appearance = appearance33;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.VisiblePosition = 22;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.VisiblePosition = 5;
            ultraGridColumn3.Width = 96;
            ultraGridColumn4.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn4.Header.VisiblePosition = 8;
            ultraGridColumn4.Width = 144;
            ultraGridColumn5.Header.VisiblePosition = 9;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 144;
            ultraGridColumn6.Header.Caption = "Contact";
            ultraGridColumn6.Header.VisiblePosition = 10;
            ultraGridColumn6.Width = 144;
            ultraGridColumn7.Header.VisiblePosition = 11;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Width = 144;
            ultraGridColumn8.Header.Caption = "Company";
            ultraGridColumn8.Header.VisiblePosition = 4;
            ultraGridColumn8.Width = 144;
            ultraGridColumn9.Header.Caption = "Region#";
            ultraGridColumn9.Header.VisiblePosition = 12;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn9.Width = 72;
            ultraGridColumn10.Header.Caption = "District#";
            ultraGridColumn10.Header.VisiblePosition = 13;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 72;
            ultraGridColumn11.Header.Caption = "Agent#";
            ultraGridColumn11.Header.VisiblePosition = 3;
            ultraGridColumn11.Width = 72;
            ultraGridColumn12.Header.Caption = "Store#";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn12.Width = 72;
            ultraGridColumn13.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn13.Header.VisiblePosition = 14;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn14.Header.VisiblePosition = 15;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn15.Header.VisiblePosition = 16;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 17;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.Caption = "Initial Action";
            ultraGridColumn17.Header.VisiblePosition = 18;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.VisiblePosition = 19;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.Caption = "Originator";
            ultraGridColumn19.Header.VisiblePosition = 23;
            ultraGridColumn19.Width = 96;
            ultraGridColumn20.Header.VisiblePosition = 20;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn20.Width = 96;
            ultraGridColumn21.Header.Caption = "Action";
            ultraGridColumn21.Header.VisiblePosition = 6;
            ultraGridColumn21.Width = 96;
            ultraGridColumn22.Format = "MM/dd/yyyy hh:mm tt";
            ultraGridColumn22.Header.Caption = "Received";
            ultraGridColumn22.Header.VisiblePosition = 7;
            ultraGridColumn22.Width = 144;
            ultraGridColumn23.Header.Caption = "Last User";
            ultraGridColumn23.Header.VisiblePosition = 21;
            ultraGridColumn23.Width = 96;
            ultraGridColumn24.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn24.Header.VisiblePosition = 1;
            ultraGridColumn24.Width = 60;
            ultraGridColumn25.Header.VisiblePosition = 24;
            ultraGridColumn25.Width = 96;
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
            ultraGridColumn25});
            this.grdIssues.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance34.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance34.FontData.BoldAsString = "True";
            appearance34.FontData.Name = "Verdana";
            appearance34.FontData.SizeInPoints = 8F;
            appearance34.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance34.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.CaptionAppearance = appearance34;
            this.grdIssues.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdIssues.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdIssues.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance35.BackColor = System.Drawing.SystemColors.Control;
            appearance35.FontData.BoldAsString = "True";
            appearance35.FontData.Name = "Verdana";
            appearance35.FontData.SizeInPoints = 8F;
            appearance35.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.Override.HeaderAppearance = appearance35;
            this.grdIssues.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdIssues.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance36.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdIssues.DisplayLayout.Override.RowAppearance = appearance36;
            this.grdIssues.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdIssues.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdIssues.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdIssues.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdIssues.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdIssues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdIssues.Location = new System.Drawing.Point(0,25);
            this.grdIssues.Name = "grdIssues";
            this.grdIssues.Size = new System.Drawing.Size(694,165);
            this.grdIssues.TabIndex = 124;
            this.grdIssues.UseAppStyling = false;
            this.grdIssues.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdIssues.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdIssues.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            this.grdIssues.AfterColPosChanged += new Infragistics.Win.UltraWinGrid.AfterColPosChangedEventHandler(this.OnColumnPositionChanged);
            this.grdIssues.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnRowFilterChanged);
            this.grdIssues.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnInitializeRow);
            this.grdIssues.DoubleClick += new System.EventHandler(this.OnGridDoubleClick);
            // 
            // ctxCtl
            // 
            this.ctxCtl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxNew,
            this.ctxOpen,
            this.ctxSaveAs,
            this.ctxSep1,
            this.ctxPageSetup,
            this.ctxPrint,
            this.ctxPreview,
            this.ctxSep2,
            this.ctxRefresh,
            this.ctxRefreshCache,
            this.mnuCtxAutoRefreshOn,
            this.ctxSep3,
            this.ctxContacts,
            this.ctxProperties});
            this.ctxCtl.Name = "ctxMain";
            this.ctxCtl.Size = new System.Drawing.Size(162,264);
            // 
            // ctxNew
            // 
            this.ctxNew.Image = ((System.Drawing.Image)(resources.GetObject("ctxNew.Image")));
            this.ctxNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxNew.Name = "ctxNew";
            this.ctxNew.Size = new System.Drawing.Size(161,22);
            this.ctxNew.Text = "New";
            this.ctxNew.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxOpen
            // 
            this.ctxOpen.Image = ((System.Drawing.Image)(resources.GetObject("ctxOpen.Image")));
            this.ctxOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxOpen.Name = "ctxOpen";
            this.ctxOpen.Size = new System.Drawing.Size(161,22);
            this.ctxOpen.Text = "Open";
            this.ctxOpen.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxSaveAs
            // 
            this.ctxSaveAs.Name = "ctxSaveAs";
            this.ctxSaveAs.Size = new System.Drawing.Size(161,22);
            this.ctxSaveAs.Text = "Save As";
            this.ctxSaveAs.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(158,6);
            // 
            // ctxPageSetup
            // 
            this.ctxPageSetup.Image = ((System.Drawing.Image)(resources.GetObject("ctxPageSetup.Image")));
            this.ctxPageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPageSetup.Name = "ctxPageSetup";
            this.ctxPageSetup.Size = new System.Drawing.Size(161,22);
            this.ctxPageSetup.Text = "Page Setup";
            this.ctxPageSetup.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxPrint
            // 
            this.ctxPrint.Image = ((System.Drawing.Image)(resources.GetObject("ctxPrint.Image")));
            this.ctxPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPrint.Name = "ctxPrint";
            this.ctxPrint.Size = new System.Drawing.Size(161,22);
            this.ctxPrint.Text = "Print";
            this.ctxPrint.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxPreview
            // 
            this.ctxPreview.Image = ((System.Drawing.Image)(resources.GetObject("ctxPreview.Image")));
            this.ctxPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPreview.Name = "ctxPreview";
            this.ctxPreview.Size = new System.Drawing.Size(161,22);
            this.ctxPreview.Text = "Print Preview";
            this.ctxPreview.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(158,6);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(161,22);
            this.ctxRefresh.Text = "Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxRefreshCache
            // 
            this.ctxRefreshCache.Name = "ctxRefreshCache";
            this.ctxRefreshCache.Size = new System.Drawing.Size(161,22);
            this.ctxRefreshCache.Text = "Refresh Cache";
            this.ctxRefreshCache.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // mnuCtxAutoRefreshOn
            // 
            this.mnuCtxAutoRefreshOn.Name = "mnuCtxAutoRefreshOn";
            this.mnuCtxAutoRefreshOn.Size = new System.Drawing.Size(161,22);
            this.mnuCtxAutoRefreshOn.Text = "Auto Refresh On";
            this.mnuCtxAutoRefreshOn.Visible = false;
            this.mnuCtxAutoRefreshOn.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxSep3
            // 
            this.ctxSep3.Name = "ctxSep3";
            this.ctxSep3.Size = new System.Drawing.Size(158,6);
            // 
            // ctxContacts
            // 
            this.ctxContacts.Image = ((System.Drawing.Image)(resources.GetObject("ctxContacts.Image")));
            this.ctxContacts.Name = "ctxContacts";
            this.ctxContacts.Size = new System.Drawing.Size(161,22);
            this.ctxContacts.Text = "Contacts";
            this.ctxContacts.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxProperties
            // 
            this.ctxProperties.Image = global::Argix.Properties.Resources.Properties;
            this.ctxProperties.Name = "ctxProperties";
            this.ctxProperties.Size = new System.Drawing.Size(161,22);
            this.ctxProperties.Text = "&Properties";
            this.ctxProperties.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // issueDSBindingSource
            // 
            this.issueDSBindingSource.DataSource = this.issueDS;
            this.issueDSBindingSource.Position = 0;
            // 
            // issueDS
            // 
            this.issueDS.DataSetName = "IssueDS";
            this.issueDS.Locale = new System.Globalization.CultureInfo("");
            this.issueDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tlsCtl
            // 
            this.tlsCtl.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlsCtl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboView,
            this.btnNew,
            this.btnOpen,
            this.btnSaveAs,
            this.btnSep1,
            this.btnSetup,
            this.btnPreview,
            this.btnPrint,
            this.btnSep2,
            this.btnRefresh,
            this.btnContacts,
            this.btnProperties,
            this.btnSep3,
            this.cboSearch});
            this.tlsCtl.Location = new System.Drawing.Point(0,0);
            this.tlsCtl.Name = "tlsCtl";
            this.tlsCtl.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tlsCtl.Size = new System.Drawing.Size(694,25);
            this.tlsCtl.TabIndex = 130;
            this.tlsCtl.Text = "Main";
            this.tlsCtl.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OnToolbarItemClicked);
            // 
            // cboView
            // 
            this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboView.Items.AddRange(new object[] {
            "Current Issues",
            "Issue History",
            "Search Results"});
            this.cboView.Name = "cboView";
            this.cboView.Size = new System.Drawing.Size(121,25);
            this.cboView.SelectedIndexChanged += new System.EventHandler(this.OnViewChanged);
            // 
            // btnNew
            // 
            this.btnNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(23,22);
            this.btnNew.ToolTipText = "New issue...";
            // 
            // btnOpen
            // 
            this.btnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(23,22);
            this.btnOpen.ToolTipText = "Open selected issue...";
            // 
            // btnSaveAs
            // 
            this.btnSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSaveAs.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveAs.Image")));
            this.btnSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveAs.Name = "btnSaveAs";
            this.btnSaveAs.Size = new System.Drawing.Size(23,22);
            this.btnSaveAs.ToolTipText = "Save issues to file...";
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6,25);
            // 
            // btnSetup
            // 
            this.btnSetup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSetup.Image = ((System.Drawing.Image)(resources.GetObject("btnSetup.Image")));
            this.btnSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(23,22);
            this.btnSetup.ToolTipText = "Print page setup...";
            // 
            // btnPreview
            // 
            this.btnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnPreview.Image")));
            this.btnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(23,22);
            this.btnPreview.ToolTipText = "Print preview...";
            // 
            // btnPrint
            // 
            this.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(23,22);
            this.btnPrint.ToolTipText = "Print all issues...";
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6,25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23,22);
            this.btnRefresh.ToolTipText = "Refresh issues";
            // 
            // btnContacts
            // 
            this.btnContacts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnContacts.Image = ((System.Drawing.Image)(resources.GetObject("btnContacts.Image")));
            this.btnContacts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnContacts.Name = "btnContacts";
            this.btnContacts.Size = new System.Drawing.Size(23,22);
            this.btnContacts.ToolTipText = "Contacts...";
            // 
            // btnProperties
            // 
            this.btnProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProperties.Image = global::Argix.Properties.Resources.Properties;
            this.btnProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProperties.Name = "btnProperties";
            this.btnProperties.Size = new System.Drawing.Size(23,22);
            this.btnProperties.ToolTipText = "Configuration properties...";
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6,25);
            // 
            // cboSearch
            // 
            this.cboSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(121,25);
            this.cboSearch.ToolTipText = "Search...";
            this.cboSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSearch);
            // 
            // IssueExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdIssues);
            this.Controls.Add(this.tlsCtl);
            this.Name = "IssueExplorer";
            this.Size = new System.Drawing.Size(694,190);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).EndInit();
            this.ctxCtl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.issueDSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueDS)).EndInit();
            this.tlsCtl.ResumeLayout(false);
            this.tlsCtl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdIssues;
        private System.Windows.Forms.ContextMenuStrip ctxCtl;
        private System.Windows.Forms.ToolStripMenuItem ctxRefresh;
        private System.Windows.Forms.ToolStripSeparator ctxSep1;
        private System.Windows.Forms.ToolStripMenuItem ctxOpen;
        private System.Windows.Forms.ToolStripSeparator ctxSep2;
        private System.Windows.Forms.ToolStripMenuItem ctxProperties;
        private System.Windows.Forms.ToolStripMenuItem ctxNew;
        private System.Windows.Forms.ToolStripMenuItem ctxSaveAs;
        private System.Windows.Forms.ToolStripMenuItem ctxPageSetup;
        private System.Windows.Forms.ToolStripMenuItem ctxPrint;
        private System.Windows.Forms.ToolStripMenuItem ctxPreview;
        private System.Windows.Forms.ToolStripSeparator ctxSep3;
        private System.Windows.Forms.ToolStripMenuItem ctxRefreshCache;
        private System.Windows.Forms.ToolStrip tlsCtl;
        private System.Windows.Forms.ToolStripComboBox cboView;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ToolStripButton btnNew;
        private System.Windows.Forms.ToolStripButton btnOpen;
        private System.Windows.Forms.ToolStripSeparator btnSep1;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripSeparator btnSep2;
        private System.Windows.Forms.ToolStripButton btnSetup;
        private System.Windows.Forms.ToolStripButton btnPreview;
        private System.Windows.Forms.ToolStripButton btnSaveAs;
        private System.Windows.Forms.ToolStripMenuItem ctxContacts;
        private System.Windows.Forms.ToolStripButton btnContacts;
        private System.Windows.Forms.ToolStripButton btnProperties;
        private System.Windows.Forms.ToolStripMenuItem mnuCtxAutoRefreshOn;
        private System.Windows.Forms.BindingSource issueDSBindingSource;
        private IssueDS issueDS;
        private System.Windows.Forms.ToolStripSeparator btnSep3;
        private System.Windows.Forms.ToolStripComboBox cboSearch;
    }
}
