namespace Argix.Freight {
    partial class dlgVendors {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.grdVendors = new System.Windows.Forms.DataGridView();
            this.colStatus = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colZip = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mVendorDS = new Argix.ShipperDS();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdVendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mVendorDS)).BeginInit();
            this.SuspendLayout();
            // 
            // grdVendors
            // 
            this.grdVendors.AllowUserToDeleteRows = false;
            this.grdVendors.AllowUserToResizeRows = false;
            this.grdVendors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVendors.AutoGenerateColumns = false;
            this.grdVendors.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdVendors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdVendors.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colStatus,
            this.colNumber,
            this.colName,
            this.colAddress,
            this.colCity,
            this.colState,
            this.colZip});
            this.grdVendors.DataMember = "VendorTable";
            this.grdVendors.DataSource = this.mVendorDS;
            this.grdVendors.Location = new System.Drawing.Point(0,0);
            this.grdVendors.MultiSelect = false;
            this.grdVendors.Name = "grdVendors";
            this.grdVendors.RowHeadersWidth = 24;
            this.grdVendors.Size = new System.Drawing.Size(666,218);
            this.grdVendors.TabIndex = 0;
            this.grdVendors.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnRowValidated);
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "STATUS";
            this.colStatus.HeaderText = "Status";
            this.colStatus.Items.AddRange(new object[] {
            "A",
            "I"});
            this.colStatus.Name = "colStatus";
            this.colStatus.Width = 48;
            // 
            // colNumber
            // 
            this.colNumber.DataPropertyName = "NUMBER";
            this.colNumber.HeaderText = "Number";
            this.colNumber.Name = "colNumber";
            this.colNumber.Width = 72;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "NAME";
            this.colName.HeaderText = "Name";
            this.colName.Name = "colName";
            this.colName.Width = 288;
            // 
            // colAddress
            // 
            this.colAddress.DataPropertyName = "ADDRESS";
            this.colAddress.HeaderText = "Address";
            this.colAddress.Name = "colAddress";
            this.colAddress.Width = 192;
            // 
            // colCity
            // 
            this.colCity.DataPropertyName = "CITY";
            this.colCity.HeaderText = "City";
            this.colCity.Name = "colCity";
            this.colCity.Width = 96;
            // 
            // colState
            // 
            this.colState.DataPropertyName = "STATE";
            this.colState.HeaderText = "State";
            this.colState.Name = "colState";
            this.colState.Width = 60;
            // 
            // colZip
            // 
            this.colZip.DataPropertyName = "ZIP";
            this.colZip.HeaderText = "Zip";
            this.colZip.Name = "colZip";
            this.colZip.Width = 48;
            // 
            // mVendorDS
            // 
            this.mVendorDS.DataSetName = "ShipperDS";
            this.mVendorDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(588,225);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72,24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(510,225);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72,24);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.OnRefresh);
            // 
            // dlgVendors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664,254);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grdVendors);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgVendors";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Samples for Export";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdVendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mVendorDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdVendors;
        private System.Windows.Forms.Button btnClose;
        private ShipperDS mVendorDS;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewComboBoxColumn colStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colState;
        private System.Windows.Forms.DataGridViewTextBoxColumn colZip;
    }
}