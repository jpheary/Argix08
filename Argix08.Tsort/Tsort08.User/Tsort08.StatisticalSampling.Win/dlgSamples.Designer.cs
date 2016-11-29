namespace Argix.Freight {
    partial class dlgSamples {
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this._lblFromDate = new System.Windows.Forms.Label();
            this._lblToDate = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.grdSamples = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorCartonNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VendorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.damageCountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sampleDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transactionDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mSampleDS = new Argix.StatSampleDS();
            this.ssDialog = new System.Windows.Forms.StatusStrip();
            this.slInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.slASNs = new System.Windows.Forms.ToolStripStatusLabel();
            this.slUnits = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.grdSamples)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSampleDS)).BeginInit();
            this.ssDialog.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(684, 230);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(72, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(516, 230);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(72, 24);
            this.btnExport.TabIndex = 2;
            this.btnExport.Text = "E&xport";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.OnExport);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(594, 230);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(72, 24);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.OnRefresh);
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(81, 4);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 4;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.OnDateChanged);
            // 
            // _lblFromDate
            // 
            this._lblFromDate.Location = new System.Drawing.Point(3, 6);
            this._lblFromDate.Name = "_lblFromDate";
            this._lblFromDate.Size = new System.Drawing.Size(72, 18);
            this._lblFromDate.TabIndex = 5;
            this._lblFromDate.Text = "From: ";
            this._lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblToDate
            // 
            this._lblToDate.Location = new System.Drawing.Point(288, 5);
            this._lblToDate.Name = "_lblToDate";
            this._lblToDate.Size = new System.Drawing.Size(72, 18);
            this._lblToDate.TabIndex = 7;
            this._lblToDate.Text = "To: ";
            this._lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(366, 3);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 6;
            this.dtpTo.ValueChanged += new System.EventHandler(this.OnDateChanged);
            // 
            // grdSamples
            // 
            this.grdSamples.AllowUserToAddRows = false;
            this.grdSamples.AllowUserToDeleteRows = false;
            this.grdSamples.AllowUserToResizeRows = false;
            this.grdSamples.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSamples.AutoGenerateColumns = false;
            this.grdSamples.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdSamples.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.vendorCartonNumberDataGridViewTextBoxColumn,
            this.VendorName,
            this.itemNumberDataGridViewTextBoxColumn,
            this.itemCountDataGridViewTextBoxColumn,
            this.damageCountDataGridViewTextBoxColumn,
            this.sampleDateDataGridViewTextBoxColumn,
            this.transactionDateDataGridViewTextBoxColumn});
            this.grdSamples.DataMember = "SampleTable";
            this.grdSamples.DataSource = this.mSampleDS;
            this.grdSamples.Location = new System.Drawing.Point(0, 27);
            this.grdSamples.MultiSelect = false;
            this.grdSamples.Name = "grdSamples";
            this.grdSamples.ReadOnly = true;
            this.grdSamples.RowHeadersVisible = false;
            this.grdSamples.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grdSamples.Size = new System.Drawing.Size(760, 189);
            this.grdSamples.TabIndex = 0;
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            this.iDDataGridViewTextBoxColumn.ReadOnly = true;
            this.iDDataGridViewTextBoxColumn.Width = 32;
            // 
            // vendorCartonNumberDataGridViewTextBoxColumn
            // 
            this.vendorCartonNumberDataGridViewTextBoxColumn.DataPropertyName = "VendorCartonNumber";
            this.vendorCartonNumberDataGridViewTextBoxColumn.HeaderText = "Carton#";
            this.vendorCartonNumberDataGridViewTextBoxColumn.Name = "vendorCartonNumberDataGridViewTextBoxColumn";
            this.vendorCartonNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.vendorCartonNumberDataGridViewTextBoxColumn.Width = 216;
            // 
            // VendorName
            // 
            this.VendorName.DataPropertyName = "VendorName";
            this.VendorName.HeaderText = "Vendor";
            this.VendorName.Name = "VendorName";
            this.VendorName.ReadOnly = true;
            this.VendorName.Width = 192;
            // 
            // itemNumberDataGridViewTextBoxColumn
            // 
            this.itemNumberDataGridViewTextBoxColumn.DataPropertyName = "ItemNumber";
            this.itemNumberDataGridViewTextBoxColumn.HeaderText = "EAN/UPC";
            this.itemNumberDataGridViewTextBoxColumn.Name = "itemNumberDataGridViewTextBoxColumn";
            this.itemNumberDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemNumberDataGridViewTextBoxColumn.Width = 120;
            // 
            // itemCountDataGridViewTextBoxColumn
            // 
            this.itemCountDataGridViewTextBoxColumn.DataPropertyName = "ItemCount";
            this.itemCountDataGridViewTextBoxColumn.HeaderText = "Good";
            this.itemCountDataGridViewTextBoxColumn.Name = "itemCountDataGridViewTextBoxColumn";
            this.itemCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.itemCountDataGridViewTextBoxColumn.Width = 60;
            // 
            // damageCountDataGridViewTextBoxColumn
            // 
            this.damageCountDataGridViewTextBoxColumn.DataPropertyName = "DamageCount";
            this.damageCountDataGridViewTextBoxColumn.HeaderText = "Dmgd";
            this.damageCountDataGridViewTextBoxColumn.Name = "damageCountDataGridViewTextBoxColumn";
            this.damageCountDataGridViewTextBoxColumn.ReadOnly = true;
            this.damageCountDataGridViewTextBoxColumn.Width = 60;
            // 
            // sampleDateDataGridViewTextBoxColumn
            // 
            this.sampleDateDataGridViewTextBoxColumn.DataPropertyName = "SampleDate";
            this.sampleDateDataGridViewTextBoxColumn.HeaderText = "Sampled";
            this.sampleDateDataGridViewTextBoxColumn.Name = "sampleDateDataGridViewTextBoxColumn";
            this.sampleDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.sampleDateDataGridViewTextBoxColumn.Width = 120;
            // 
            // transactionDateDataGridViewTextBoxColumn
            // 
            this.transactionDateDataGridViewTextBoxColumn.DataPropertyName = "TransactionDate";
            this.transactionDateDataGridViewTextBoxColumn.HeaderText = "Exported";
            this.transactionDateDataGridViewTextBoxColumn.Name = "transactionDateDataGridViewTextBoxColumn";
            this.transactionDateDataGridViewTextBoxColumn.ReadOnly = true;
            this.transactionDateDataGridViewTextBoxColumn.Width = 120;
            // 
            // mSampleDS
            // 
            this.mSampleDS.DataSetName = "StatSampleDS";
            this.mSampleDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ssDialog
            // 
            this.ssDialog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.slInfo,
            this.slASNs,
            this.slUnits});
            this.ssDialog.Location = new System.Drawing.Point(0, 258);
            this.ssDialog.Name = "ssDialog";
            this.ssDialog.ShowItemToolTips = true;
            this.ssDialog.Size = new System.Drawing.Size(760, 25);
            this.ssDialog.SizingGrip = false;
            this.ssDialog.TabIndex = 8;
            // 
            // slInfo
            // 
            this.slInfo.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.slInfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.slInfo.Name = "slInfo";
            this.slInfo.Size = new System.Drawing.Size(564, 20);
            this.slInfo.Spring = true;
            this.slInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // slASNs
            // 
            this.slASNs.AutoSize = false;
            this.slASNs.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.slASNs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.slASNs.Name = "slASNs";
            this.slASNs.Size = new System.Drawing.Size(75, 20);
            this.slASNs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.slASNs.ToolTipText = "Count of ASN\'s";
            // 
            // slUnits
            // 
            this.slUnits.AutoSize = false;
            this.slUnits.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.slUnits.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.slUnits.Name = "slUnits";
            this.slUnits.Size = new System.Drawing.Size(75, 20);
            this.slUnits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.slUnits.ToolTipText = "Sum of good+bad units";
            // 
            // dlgSamples
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 283);
            this.Controls.Add(this.ssDialog);
            this.Controls.Add(this._lblToDate);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this._lblFromDate);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grdSamples);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSamples";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Samples for Export";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdSamples)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSampleDS)).EndInit();
            this.ssDialog.ResumeLayout(false);
            this.ssDialog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdSamples;
        private StatSampleDS mSampleDS;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label _lblFromDate;
        private System.Windows.Forms.Label _lblToDate;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorCartonNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VendorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn damageCountDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sampleDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn transactionDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.StatusStrip ssDialog;
        private System.Windows.Forms.ToolStripStatusLabel slInfo;
        private System.Windows.Forms.ToolStripStatusLabel slASNs;
        private System.Windows.Forms.ToolStripStatusLabel slUnits;
    }
}