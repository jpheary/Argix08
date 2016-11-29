namespace Argix.Customers {
    partial class CompanyLocation {
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
            this.mCompanyDS = new Argix.Customers.CompanyDS();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.cboScope = new System.Windows.Forms.ComboBox();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this._lblScope = new System.Windows.Forms.Label();
            this._lblCompany = new System.Windows.Forms.Label();
            this.btnStoreDetail = new System.Windows.Forms.Button();
            this._lblLocation = new System.Windows.Forms.Label();
            this.txtStore = new System.Windows.Forms.MaskedTextBox();
            this.txtStoreDetail = new System.Windows.Forms.TextBox();
            this._lblStoreDetail = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mCompanyDS)).BeginInit();
            this.SuspendLayout();
            // 
            // mCompanyDS
            // 
            this.mCompanyDS.DataSetName = "CompanyDS";
            this.mCompanyDS.Locale = new System.Globalization.CultureInfo("");
            this.mCompanyDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboLocation
            // 
            this.cboLocation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(82,60);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(197,21);
            this.cboLocation.TabIndex = 2;
            this.cboLocation.SelectionChangeCommitted += new System.EventHandler(this.OnCompanyLocationChanged);
            // 
            // cboScope
            // 
            this.cboScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScope.FormattingEnabled = true;
            this.cboScope.Items.AddRange(new object[] {
            "Districts",
            "Regions",
            "Stores",
            "Substores",
            "Agents"});
            this.cboScope.Location = new System.Drawing.Point(82,33);
            this.cboScope.Name = "cboScope";
            this.cboScope.Size = new System.Drawing.Size(91,21);
            this.cboScope.TabIndex = 1;
            this.cboScope.SelectionChangeCommitted += new System.EventHandler(this.OnScopeChanged);
            // 
            // cboCompany
            // 
            this.cboCompany.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCompany.DataSource = this.mCompanyDS;
            this.cboCompany.DisplayMember = "CompanyTable.CompanyName";
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(82,6);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(197,21);
            this.cboCompany.TabIndex = 0;
            this.cboCompany.ValueMember = "CompanyTable.Number";
            this.cboCompany.SelectionChangeCommitted += new System.EventHandler(this.OnCompanySelected);
            // 
            // _lblScope
            // 
            this._lblScope.Location = new System.Drawing.Point(3,33);
            this._lblScope.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblScope.Name = "_lblScope";
            this._lblScope.Size = new System.Drawing.Size(72,18);
            this._lblScope.TabIndex = 7;
            this._lblScope.Text = "Location";
            this._lblScope.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblCompany
            // 
            this._lblCompany.Location = new System.Drawing.Point(3,6);
            this._lblCompany.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblCompany.Name = "_lblCompany";
            this._lblCompany.Size = new System.Drawing.Size(72,18);
            this._lblCompany.TabIndex = 6;
            this._lblCompany.Text = "Company";
            this._lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnStoreDetail
            // 
            this.btnStoreDetail.Location = new System.Drawing.Point(157,60);
            this.btnStoreDetail.Name = "btnStoreDetail";
            this.btnStoreDetail.Size = new System.Drawing.Size(51,21);
            this.btnStoreDetail.TabIndex = 4;
            this.btnStoreDetail.Text = "Detail...";
            this.btnStoreDetail.UseVisualStyleBackColor = true;
            this.btnStoreDetail.Visible = false;
            this.btnStoreDetail.MouseLeave += new System.EventHandler(this.OnHideStoreDetail);
            this.btnStoreDetail.Click += new System.EventHandler(this.OnViewStoreDetail);
            // 
            // _lblLocation
            // 
            this._lblLocation.Location = new System.Drawing.Point(3,60);
            this._lblLocation.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblLocation.Name = "_lblLocation";
            this._lblLocation.Size = new System.Drawing.Size(72,18);
            this._lblLocation.TabIndex = 9;
            this._lblLocation.Text = "#";
            this._lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStore
            // 
            this.txtStore.AllowPromptAsInput = false;
            this.txtStore.HidePromptOnLeave = true;
            this.txtStore.Location = new System.Drawing.Point(82,60);
            this.txtStore.Mask = "#########";
            this.txtStore.Name = "txtStore";
            this.txtStore.Size = new System.Drawing.Size(60,20);
            this.txtStore.TabIndex = 3;
            this.txtStore.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtStore.Visible = false;
            this.txtStore.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnStoreKeyUp);
            this.txtStore.TextChanged += new System.EventHandler(this.OnCompanyLocationChanged);
            // 
            // txtStoreDetail
            // 
            this.txtStoreDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStoreDetail.BackColor = System.Drawing.SystemColors.Window;
            this.txtStoreDetail.HideSelection = false;
            this.txtStoreDetail.Location = new System.Drawing.Point(82,87);
            this.txtStoreDetail.Multiline = true;
            this.txtStoreDetail.Name = "txtStoreDetail";
            this.txtStoreDetail.ReadOnly = true;
            this.txtStoreDetail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtStoreDetail.Size = new System.Drawing.Size(197,96);
            this.txtStoreDetail.TabIndex = 10;
            this.txtStoreDetail.WordWrap = false;
            // 
            // _lblStoreDetail
            // 
            this._lblStoreDetail.Location = new System.Drawing.Point(3,87);
            this._lblStoreDetail.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblStoreDetail.Name = "_lblStoreDetail";
            this._lblStoreDetail.Size = new System.Drawing.Size(72,18);
            this._lblStoreDetail.TabIndex = 11;
            this._lblStoreDetail.Text = "Store";
            this._lblStoreDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CompanyLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboLocation);
            this.Controls.Add(this._lblStoreDetail);
            this.Controls.Add(this.txtStoreDetail);
            this.Controls.Add(this.txtStore);
            this.Controls.Add(this._lblLocation);
            this.Controls.Add(this.btnStoreDetail);
            this.Controls.Add(this.cboCompany);
            this.Controls.Add(this.cboScope);
            this.Controls.Add(this._lblCompany);
            this.Controls.Add(this._lblScope);
            this.Name = "CompanyLocation";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(288,192);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mCompanyDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Argix.Customers.CompanyDS mCompanyDS;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.Label _lblScope;
        private System.Windows.Forms.Label _lblCompany;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.ComboBox cboScope;
        private System.Windows.Forms.Button btnStoreDetail;
        private System.Windows.Forms.Label _lblLocation;
        private System.Windows.Forms.MaskedTextBox txtStore;
        private System.Windows.Forms.TextBox txtStoreDetail;
        private System.Windows.Forms.Label _lblStoreDetail;
    }
}
