namespace Argix {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mnuMain;

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.mnuMain = new System.Windows.Forms.MainMenu();
            this.mnuFile = new System.Windows.Forms.MenuItem();
            this.mnuFileNew = new System.Windows.Forms.MenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.MenuItem();
            this.mnuFileSave = new System.Windows.Forms.MenuItem();
            this.mnuFileSync = new System.Windows.Forms.MenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.MenuItem();
            this.mnuFileClose = new System.Windows.Forms.MenuItem();
            this.mnuView = new System.Windows.Forms.MenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.MenuItem();
            this.mnuHelp = new System.Windows.Forms.MenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.MenuItem();
            this.bsClients = new System.Windows.Forms.BindingSource(this.components);
            this.mClients = new Argix.TsortDataSet();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this._lblClient = new System.Windows.Forms.Label();
            this._lblStore = new System.Windows.Forms.Label();
            this.bsStores = new System.Windows.Forms.BindingSource(this.components);
            this.mStores = new Argix.TsortDataSet();
            this.cboStore = new System.Windows.Forms.ComboBox();
            this._lblScan = new System.Windows.Forms.Label();
            this.txtScan = new System.Windows.Forms.TextBox();
            this.sbMain = new System.Windows.Forms.StatusBar();
            ((System.ComponentModel.ISupportInitialize)(this.bsClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStores)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mStores)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.Add(this.mnuFile);
            this.mnuMain.MenuItems.Add(this.mnuView);
            this.mnuMain.MenuItems.Add(this.mnuHelp);
            // 
            // mnuFile
            // 
            this.mnuFile.MenuItems.Add(this.mnuFileNew);
            this.mnuFile.MenuItems.Add(this.mnuFileSep1);
            this.mnuFile.MenuItems.Add(this.mnuFileSave);
            this.mnuFile.MenuItems.Add(this.mnuFileSync);
            this.mnuFile.MenuItems.Add(this.mnuFileSep2);
            this.mnuFile.MenuItems.Add(this.mnuFileClose);
            this.mnuFile.Text = "File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Text = "New";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Text = "-";
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Text = "Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSync
            // 
            this.mnuFileSync.Text = "Sync";
            this.mnuFileSync.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Text = "-";
            // 
            // mnuFileClose
            // 
            this.mnuFileClose.Text = "Close";
            this.mnuFileClose.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.MenuItems.Add(this.mnuViewRefresh);
            this.mnuView.Text = "View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Text = "Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.MenuItems.Add(this.mnuHelpAbout);
            this.mnuHelp.Text = "Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Text = "About";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // bsClients
            // 
            this.bsClients.DataMember = "Client";
            this.bsClients.DataSource = this.mClients;
            // 
            // mClients
            // 
            this.mClients.DataSetName = "TsortDataSet";
            this.mClients.Prefix = "";
            this.mClients.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboClient
            // 
            this.cboClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClient.DataSource = this.mClients;
            this.cboClient.DisplayMember = "Client.NAME";
            this.cboClient.Location = new System.Drawing.Point(57,40);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(234,23);
            this.cboClient.TabIndex = 0;
            this.cboClient.ValueMember = "Client.NUMBER";
            this.cboClient.SelectedValueChanged += new System.EventHandler(this.OnClientChanged);
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(3,42);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(48,20);
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _lblStore
            // 
            this._lblStore.Location = new System.Drawing.Point(3,80);
            this._lblStore.Name = "_lblStore";
            this._lblStore.Size = new System.Drawing.Size(48,20);
            this._lblStore.Text = "Store";
            this._lblStore.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // bsStores
            // 
            this.bsStores.DataMember = "Store";
            this.bsStores.DataSource = this.mStores;
            // 
            // mStores
            // 
            this.mStores.DataSetName = "TsortDataSet";
            this.mStores.Prefix = "";
            this.mStores.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboStore
            // 
            this.cboStore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStore.DataSource = this.mStores;
            this.cboStore.DisplayMember = "Store.NUMBER";
            this.cboStore.Location = new System.Drawing.Point(57,78);
            this.cboStore.Name = "cboStore";
            this.cboStore.Size = new System.Drawing.Size(234,23);
            this.cboStore.TabIndex = 3;
            this.cboStore.ValueMember = "Store.NUMBER";
            this.cboStore.SelectedValueChanged += new System.EventHandler(this.OnStoreChanged);
            // 
            // _lblScan
            // 
            this._lblScan.Location = new System.Drawing.Point(3,120);
            this._lblScan.Name = "_lblScan";
            this._lblScan.Size = new System.Drawing.Size(48,20);
            this._lblScan.Text = "Scan";
            this._lblScan.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtScan
            // 
            this.txtScan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtScan.Location = new System.Drawing.Point(57,116);
            this.txtScan.Name = "txtScan";
            this.txtScan.Size = new System.Drawing.Size(234,23);
            this.txtScan.TabIndex = 7;
            this.txtScan.TextChanged += new System.EventHandler(this.OnScanChanged);
            // 
            // sbMain
            // 
            this.sbMain.Location = new System.Drawing.Point(0,143);
            this.sbMain.Name = "sbMain";
            this.sbMain.Size = new System.Drawing.Size(310,24);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(310,167);
            this.Controls.Add(this.sbMain);
            this.Controls.Add(this.txtScan);
            this.Controls.Add(this._lblScan);
            this.Controls.Add(this._lblStore);
            this.Controls.Add(this.cboStore);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.cboClient);
            this.Menu = this.mnuMain;
            this.Name = "Main";
            this.Text = "Driver Scan";
            this.Deactivate += new System.EventHandler(this.OnFormDeactivated);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bsClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsStores)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mStores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuFile;
        private System.Windows.Forms.MenuItem mnuFileNew;
        private System.Windows.Forms.MenuItem mnuFileSep1;
        private System.Windows.Forms.MenuItem mnuFileSave;
        private System.Windows.Forms.MenuItem mnuFileSep2;
        private System.Windows.Forms.MenuItem mnuFileClose;
        private System.Windows.Forms.MenuItem mnuHelp;
        private System.Windows.Forms.MenuItem mnuHelpAbout;
        private System.Windows.Forms.ComboBox cboClient;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.Label _lblStore;
        private System.Windows.Forms.ComboBox cboStore;
        private System.Windows.Forms.Label _lblScan;
        private System.Windows.Forms.TextBox txtScan;
        private System.Windows.Forms.BindingSource bsClients;
        private TsortDataSet mClients;
        private System.Windows.Forms.BindingSource bsStores;
        private TsortDataSet mStores;
        private System.Windows.Forms.StatusBar sbMain;
        private System.Windows.Forms.MenuItem mnuFileSync;
        private System.Windows.Forms.MenuItem mnuView;
        private System.Windows.Forms.MenuItem mnuViewRefresh;

    }
}

