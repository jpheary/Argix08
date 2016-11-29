namespace Argix.Finance {
    //
    partial class dlgInvoice {
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
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.mInvoiceDS = new Argix.Finance.InvoiceDS();
            this._lblClient = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.grdInvoice = new System.Windows.Forms.DataGridView();
            this.invoiceNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.invoiceDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postToARDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cartonsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.palletsDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.weightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.releaseDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._lblInvoice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mInvoiceDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoice)).BeginInit();
            this.SuspendLayout();
            // 
            // cboClient
            // 
            this.cboClient.DataSource = this.mInvoiceDS;
            this.cboClient.DisplayMember = "ClientTable.ClientName";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(87,6);
            this.cboClient.Margin = new System.Windows.Forms.Padding(4);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(471,24);
            this.cboClient.TabIndex = 0;
            this.cboClient.ValueMember = "ClientTable.ClientNumber";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientSelected);
            // 
            // mInvoiceDS
            // 
            this.mInvoiceDS.DataSetName = "InvoiceDS";
            this.mInvoiceDS.Locale = new System.Globalization.CultureInfo("");
            this.mInvoiceDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(8,7);
            this._lblClient.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(72,22);
            this._lblClient.TabIndex = 1;
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(582,225);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75,23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(501,225);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75,23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "O&k";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // grdInvoice
            // 
            this.grdInvoice.AllowUserToAddRows = false;
            this.grdInvoice.AllowUserToDeleteRows = false;
            this.grdInvoice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdInvoice.AutoGenerateColumns = false;
            this.grdInvoice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.grdInvoice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.grdInvoice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.invoiceNumberDataGridViewTextBoxColumn,
            this.invoiceDateDataGridViewTextBoxColumn,
            this.postToARDateDataGridViewTextBoxColumn,
            this.cartonsDataGridViewTextBoxColumn,
            this.palletsDataGridViewTextBoxColumn,
            this.weightDataGridViewTextBoxColumn,
            this.amountDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.releaseDateDataGridViewTextBoxColumn});
            this.grdInvoice.DataMember = "ClientInvoiceTable";
            this.grdInvoice.DataSource = this.mInvoiceDS;
            this.grdInvoice.Location = new System.Drawing.Point(87,39);
            this.grdInvoice.MultiSelect = false;
            this.grdInvoice.Name = "grdInvoice";
            this.grdInvoice.ReadOnly = true;
            this.grdInvoice.RowHeadersVisible = false;
            this.grdInvoice.RowHeadersWidth = 24;
            this.grdInvoice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdInvoice.Size = new System.Drawing.Size(567,180);
            this.grdInvoice.TabIndex = 4;
            this.grdInvoice.SelectionChanged += new System.EventHandler(this.OnInvoiceSelected);
            // 
            // invoiceNumberDataGridViewTextBoxColumn
            // 
            this.invoiceNumberDataGridViewTextBoxColumn.DataPropertyName = "InvoiceNumber";
            this.invoiceNumberDataGridViewTextBoxColumn.HeaderText = "Invoice#";
            this.invoiceNumberDataGridViewTextBoxColumn.Name = "invoiceNumberDataGridViewTextBoxColumn";
            this.invoiceNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.invoiceNumberDataGridViewTextBoxColumn.Width = 96;
            // 
            // invoiceDateDataGridViewTextBoxColumn
            // 
            this.invoiceDateDataGridViewTextBoxColumn.DataPropertyName = "InvoiceDate";
            this.invoiceDateDataGridViewTextBoxColumn.HeaderText = "Invoiced";
            this.invoiceDateDataGridViewTextBoxColumn.Name = "invoiceDateDataGridViewTextBoxColumn";
            this.invoiceDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.invoiceDateDataGridViewTextBoxColumn.Width = 96;
            // 
            // postToARDateDataGridViewTextBoxColumn
            // 
            this.postToARDateDataGridViewTextBoxColumn.DataPropertyName = "PostToARDate";
            this.postToARDateDataGridViewTextBoxColumn.HeaderText = "Posted To AR";
            this.postToARDateDataGridViewTextBoxColumn.Name = "postToARDateDataGridViewTextBoxColumn";
            this.postToARDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.postToARDateDataGridViewTextBoxColumn.Width = 96;
            // 
            // cartonsDataGridViewTextBoxColumn
            // 
            this.cartonsDataGridViewTextBoxColumn.DataPropertyName = "Cartons";
            this.cartonsDataGridViewTextBoxColumn.HeaderText = "Ctns";
            this.cartonsDataGridViewTextBoxColumn.Name = "cartonsDataGridViewTextBoxColumn";
            this.cartonsDataGridViewTextBoxColumn.ReadOnly = true;
            this.cartonsDataGridViewTextBoxColumn.Width = 48;
            // 
            // palletsDataGridViewTextBoxColumn
            // 
            this.palletsDataGridViewTextBoxColumn.DataPropertyName = "Pallets";
            this.palletsDataGridViewTextBoxColumn.HeaderText = "Pllts";
            this.palletsDataGridViewTextBoxColumn.Name = "palletsDataGridViewTextBoxColumn";
            this.palletsDataGridViewTextBoxColumn.ReadOnly = true;
            this.palletsDataGridViewTextBoxColumn.Width = 48;
            // 
            // weightDataGridViewTextBoxColumn
            // 
            this.weightDataGridViewTextBoxColumn.DataPropertyName = "Weight";
            this.weightDataGridViewTextBoxColumn.HeaderText = "Wt";
            this.weightDataGridViewTextBoxColumn.Name = "weightDataGridViewTextBoxColumn";
            this.weightDataGridViewTextBoxColumn.ReadOnly = true;
            this.weightDataGridViewTextBoxColumn.Width = 60;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            this.amountDataGridViewTextBoxColumn.HeaderText = "Amt";
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            this.amountDataGridViewTextBoxColumn.ReadOnly = true;
            this.amountDataGridViewTextBoxColumn.Width = 60;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Desc";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
            this.descriptionDataGridViewTextBoxColumn.Width = 96;
            // 
            // releaseDateDataGridViewTextBoxColumn
            // 
            this.releaseDateDataGridViewTextBoxColumn.DataPropertyName = "ReleaseDate";
            this.releaseDateDataGridViewTextBoxColumn.HeaderText = "Released";
            this.releaseDateDataGridViewTextBoxColumn.Name = "releaseDateDataGridViewTextBoxColumn";
            this.releaseDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.releaseDateDataGridViewTextBoxColumn.Width = 96;
            // 
            // _lblInvoice
            // 
            this._lblInvoice.Location = new System.Drawing.Point(9,39);
            this._lblInvoice.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblInvoice.Name = "_lblInvoice";
            this._lblInvoice.Size = new System.Drawing.Size(72,22);
            this._lblInvoice.TabIndex = 5;
            this._lblInvoice.Text = "Invoice";
            this._lblInvoice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F,16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(664,254);
            this.Controls.Add(this._lblInvoice);
            this.Controls.Add(this.grdInvoice);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.cboClient);
            this.Font = new System.Drawing.Font("Verdana",9.75F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "dlgInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select An Invoice";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mInvoiceDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdInvoice)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboClient;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private InvoiceDS mInvoiceDS;
        private System.Windows.Forms.DataGridView grdInvoice;
        private System.Windows.Forms.Label _lblInvoice;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn invoiceDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn postToARDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cartonsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn palletsDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn weightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn releaseDateDataGridViewTextBoxColumn;
    }
}