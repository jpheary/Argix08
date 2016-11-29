namespace Argix.Freight {
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
            this.mnuMain = new System.Windows.Forms.MainMenu();
            this.mnuRefresh = new System.Windows.Forms.MenuItem();
            this.mnuDelete = new System.Windows.Forms.MenuItem();
            this.pnlFreight = new System.Windows.Forms.Panel();
            this._lblSortType = new System.Windows.Forms.Label();
            this._lblCartons = new System.Windows.Forms.Label();
            this._lblClient = new System.Windows.Forms.Label();
            this._lblFreight = new System.Windows.Forms.Label();
            this._lblTerminal = new System.Windows.Forms.Label();
            this.lblSortType = new System.Windows.Forms.Label();
            this.lblCartons = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblFreight = new System.Windows.Forms.Label();
            this.lblTerminal = new System.Windows.Forms.Label();
            this.pnlItem = new System.Windows.Forms.Panel();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.lblItem = new System.Windows.Forms.Label();
            this.stbMain = new System.Windows.Forms.StatusBar();
            this._lblUser = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.pnlFreight.SuspendLayout();
            this.pnlItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.Add(this.mnuRefresh);
            this.mnuMain.MenuItems.Add(this.mnuDelete);
            // 
            // mnuRefresh
            // 
            this.mnuRefresh.Text = "Refresh";
            this.mnuRefresh.Click += new System.EventHandler(this.OnRefresh);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Text = "Delete Item";
            this.mnuDelete.Click += new System.EventHandler(this.OnDelete);
            // 
            // pnlFreight
            // 
            this.pnlFreight.Controls.Add(this._lblUser);
            this.pnlFreight.Controls.Add(this.lblUser);
            this.pnlFreight.Controls.Add(this._lblSortType);
            this.pnlFreight.Controls.Add(this._lblCartons);
            this.pnlFreight.Controls.Add(this._lblClient);
            this.pnlFreight.Controls.Add(this._lblFreight);
            this.pnlFreight.Controls.Add(this._lblTerminal);
            this.pnlFreight.Controls.Add(this.lblSortType);
            this.pnlFreight.Controls.Add(this.lblCartons);
            this.pnlFreight.Controls.Add(this.lblClient);
            this.pnlFreight.Controls.Add(this.lblFreight);
            this.pnlFreight.Controls.Add(this.lblTerminal);
            this.pnlFreight.Location = new System.Drawing.Point(3,3);
            this.pnlFreight.Name = "pnlFreight";
            this.pnlFreight.Size = new System.Drawing.Size(234,154);
            // 
            // _lblSortType
            // 
            this._lblSortType.Location = new System.Drawing.Point(2,101);
            this._lblSortType.Name = "_lblSortType";
            this._lblSortType.Size = new System.Drawing.Size(72,20);
            this._lblSortType.Text = "Sort Type:";
            this._lblSortType.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _lblCartons
            // 
            this._lblCartons.Location = new System.Drawing.Point(2,77);
            this._lblCartons.Name = "_lblCartons";
            this._lblCartons.Size = new System.Drawing.Size(72,20);
            this._lblCartons.Text = "Cartons:";
            this._lblCartons.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(2,53);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(72,20);
            this._lblClient.Text = "Client:";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _lblFreight
            // 
            this._lblFreight.Location = new System.Drawing.Point(2,29);
            this._lblFreight.Name = "_lblFreight";
            this._lblFreight.Size = new System.Drawing.Size(72,20);
            this._lblFreight.Text = "Freight: ";
            this._lblFreight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // _lblTerminal
            // 
            this._lblTerminal.Location = new System.Drawing.Point(2,5);
            this._lblTerminal.Name = "_lblTerminal";
            this._lblTerminal.Size = new System.Drawing.Size(72,20);
            this._lblTerminal.Text = "Terminal:";
            this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblSortType
            // 
            this.lblSortType.Location = new System.Drawing.Point(80,101);
            this.lblSortType.Name = "lblSortType";
            this.lblSortType.Size = new System.Drawing.Size(151,20);
            this.lblSortType.Text = "1";
            // 
            // lblCartons
            // 
            this.lblCartons.Location = new System.Drawing.Point(80,77);
            this.lblCartons.Name = "lblCartons";
            this.lblCartons.Size = new System.Drawing.Size(151,20);
            this.lblCartons.Text = "20";
            // 
            // lblClient
            // 
            this.lblClient.Location = new System.Drawing.Point(80,53);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(151,20);
            this.lblClient.Text = "001 01";
            // 
            // lblFreight
            // 
            this.lblFreight.Location = new System.Drawing.Point(80,29);
            this.lblFreight.Name = "lblFreight";
            this.lblFreight.Size = new System.Drawing.Size(151,20);
            this.lblFreight.Text = "00000001";
            // 
            // lblTerminal
            // 
            this.lblTerminal.Location = new System.Drawing.Point(80,5);
            this.lblTerminal.Name = "lblTerminal";
            this.lblTerminal.Size = new System.Drawing.Size(151,20);
            this.lblTerminal.Text = "Jamesburg";
            // 
            // pnlItem
            // 
            this.pnlItem.Controls.Add(this.txtItem);
            this.pnlItem.Controls.Add(this.lblItem);
            this.pnlItem.Location = new System.Drawing.Point(3,163);
            this.pnlItem.Name = "pnlItem";
            this.pnlItem.Size = new System.Drawing.Size(234,77);
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(3,36);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(228,21);
            this.txtItem.TabIndex = 1;
            this.txtItem.TextChanged += new System.EventHandler(this.OnScanChanged);
            // 
            // lblItem
            // 
            this.lblItem.Location = new System.Drawing.Point(3,9);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(228,20);
            this.lblItem.Text = "Scan Item";
            // 
            // stbMain
            // 
            this.stbMain.Location = new System.Drawing.Point(0,246);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(240,22);
            this.stbMain.Text = "Scanner 00001";
            // 
            // _lblUser
            // 
            this._lblUser.Location = new System.Drawing.Point(2,126);
            this._lblUser.Name = "_lblUser";
            this._lblUser.Size = new System.Drawing.Size(72,20);
            this._lblUser.Text = "User:";
            this._lblUser.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblUser
            // 
            this.lblUser.Location = new System.Drawing.Point(80,126);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(151,20);
            this.lblUser.Text = "jheary";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240,268);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.pnlItem);
            this.Controls.Add(this.pnlFreight);
            this.Menu = this.mnuMain;
            this.Name = "Main";
            this.Text = "Scanner Sort";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Closed += new System.EventHandler(this.OnClosed);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnClosing);
            this.pnlFreight.ResumeLayout(false);
            this.pnlItem.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuRefresh;
        private System.Windows.Forms.MenuItem mnuDelete;
        private System.Windows.Forms.Panel pnlFreight;
        private System.Windows.Forms.Panel pnlItem;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.StatusBar stbMain;
        private System.Windows.Forms.Label lblSortType;
        private System.Windows.Forms.Label lblCartons;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblFreight;
        private System.Windows.Forms.Label lblTerminal;
        private System.Windows.Forms.Label _lblSortType;
        private System.Windows.Forms.Label _lblCartons;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.Label _lblFreight;
        private System.Windows.Forms.Label _lblTerminal;
        private System.Windows.Forms.Label _lblUser;
        private System.Windows.Forms.Label lblUser;
    }
}

