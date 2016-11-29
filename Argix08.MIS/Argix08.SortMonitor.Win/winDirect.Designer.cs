namespace Tsort.Tools {
    partial class winDirect {
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
            this._lblScanned = new System.Windows.Forms.Label();
            this.lblScanned = new System.Windows.Forms.Label();
            this._lblCartons = new System.Windows.Forms.Label();
            this._lblFrieghtID = new System.Windows.Forms.Label();
            this.lblCartons = new System.Windows.Forms.Label();
            this.lblFreightID = new System.Windows.Forms.Label();
            this.dataGridTableStyle1 = new System.Windows.Forms.DataGridTableStyle();
            this.dataGridTextBoxColumn3 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn4 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn1 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn5 = new System.Windows.Forms.DataGridTextBoxColumn();
            this.dataGridTextBoxColumn2 = new System.Windows.Forms.DataGridTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mLogDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mItemsDS)).BeginInit();
            this.tabDialog.SuspendLayout();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLog)).BeginInit();
            this.SuspendLayout();
            // 
            // grdItems
            // 
            this.grdItems.DataMember = "SortedItemTable";
            this.grdItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // _lblScanned
            // 
            this._lblScanned.Location = new System.Drawing.Point(159,38);
            this._lblScanned.Name = "_lblScanned";
            this._lblScanned.Size = new System.Drawing.Size(48,18);
            this._lblScanned.TabIndex = 34;
            this._lblScanned.Text = "Scans";
            this._lblScanned.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblScanned
            // 
            this.lblScanned.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScanned.Location = new System.Drawing.Point(159,56);
            this.lblScanned.Name = "lblScanned";
            this.lblScanned.Size = new System.Drawing.Size(48,18);
            this.lblScanned.TabIndex = 33;
            this.lblScanned.Text = "0";
            this.lblScanned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblCartons
            // 
            this._lblCartons.Location = new System.Drawing.Point(105,38);
            this._lblCartons.Name = "_lblCartons";
            this._lblCartons.Size = new System.Drawing.Size(48,18);
            this._lblCartons.TabIndex = 32;
            this._lblCartons.Text = "Cartns";
            this._lblCartons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblFrieghtID
            // 
            this._lblFrieghtID.Location = new System.Drawing.Point(3,38);
            this._lblFrieghtID.Name = "_lblFrieghtID";
            this._lblFrieghtID.Size = new System.Drawing.Size(96,18);
            this._lblFrieghtID.TabIndex = 31;
            this._lblFrieghtID.Text = "FreightID";
            this._lblFrieghtID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCartons
            // 
            this.lblCartons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCartons.Location = new System.Drawing.Point(105,56);
            this.lblCartons.Name = "lblCartons";
            this.lblCartons.Size = new System.Drawing.Size(48,18);
            this.lblCartons.TabIndex = 30;
            this.lblCartons.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFreightID
            // 
            this.lblFreightID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFreightID.Location = new System.Drawing.Point(3,56);
            this.lblFreightID.Name = "lblFreightID";
            this.lblFreightID.Size = new System.Drawing.Size(96,18);
            this.lblFreightID.TabIndex = 29;
            this.lblFreightID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dataGridTableStyle1
            // 
            this.dataGridTableStyle1.DataGrid = this.grdItems;
            this.dataGridTableStyle1.GridColumnStyles.AddRange(new System.Windows.Forms.DataGridColumnStyle[] {
            this.dataGridTextBoxColumn3,
            this.dataGridTextBoxColumn4,
            this.dataGridTextBoxColumn1,
            this.dataGridTextBoxColumn5,
            this.dataGridTextBoxColumn2});
            this.dataGridTableStyle1.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.dataGridTableStyle1.MappingName = "SortedItemTable";
            this.dataGridTableStyle1.ReadOnly = true;
            this.dataGridTableStyle1.RowHeadersVisible = false;
            // 
            // dataGridTextBoxColumn3
            // 
            this.dataGridTextBoxColumn3.Format = "";
            this.dataGridTextBoxColumn3.FormatInfo = null;
            this.dataGridTextBoxColumn3.HeaderText = "Station";
            this.dataGridTextBoxColumn3.MappingName = "STATION";
            this.dataGridTextBoxColumn3.Width = 48;
            // 
            // dataGridTextBoxColumn4
            // 
            this.dataGridTextBoxColumn4.Format = "";
            this.dataGridTextBoxColumn4.FormatInfo = null;
            this.dataGridTextBoxColumn4.HeaderText = "Sort Date";
            this.dataGridTextBoxColumn4.MappingName = "SORT_DATE";
            this.dataGridTextBoxColumn4.Width = 72;
            // 
            // dataGridTextBoxColumn1
            // 
            this.dataGridTextBoxColumn1.Format = "";
            this.dataGridTextBoxColumn1.FormatInfo = null;
            this.dataGridTextBoxColumn1.HeaderText = "Label Seq#";
            this.dataGridTextBoxColumn1.MappingName = "LABEL_SEQ_NUMBER";
            this.dataGridTextBoxColumn1.Width = 120;
            // 
            // dataGridTextBoxColumn5
            // 
            this.dataGridTextBoxColumn5.Format = "";
            this.dataGridTextBoxColumn5.FormatInfo = null;
            this.dataGridTextBoxColumn5.HeaderText = "Scan";
            this.dataGridTextBoxColumn5.MappingName = "ScanString";
            this.dataGridTextBoxColumn5.Width = 144;
            // 
            // dataGridTextBoxColumn2
            // 
            this.dataGridTextBoxColumn2.Format = "";
            this.dataGridTextBoxColumn2.FormatInfo = null;
            this.dataGridTextBoxColumn2.HeaderText = "Carton#";
            this.dataGridTextBoxColumn2.MappingName = "VENDOR_ITEM_NUMBER";
            this.dataGridTextBoxColumn2.Width = 168;
            // 
            // winDirect
            // 
            this.ClientSize = new System.Drawing.Size(664,350);
            this.Controls.Add(this._lblScanned);
            this.Controls.Add(this.lblScanned);
            this.Controls.Add(this._lblCartons);
            this.Controls.Add(this._lblFrieghtID);
            this.Controls.Add(this.lblCartons);
            this.Controls.Add(this.lblFreightID);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "winDirect";
            this.Text = "Direct Sort Station";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Controls.SetChildIndex(this.lblPrinter,0);
            this.Controls.SetChildIndex(this.lblStation,0);
            this.Controls.SetChildIndex(this.tabDialog,0);
            this.Controls.SetChildIndex(this.lblFreightID,0);
            this.Controls.SetChildIndex(this.lblCartons,0);
            this.Controls.SetChildIndex(this._lblFrieghtID,0);
            this.Controls.SetChildIndex(this._lblCartons,0);
            this.Controls.SetChildIndex(this.lblScanned,0);
            this.Controls.SetChildIndex(this._lblScanned,0);
            ((System.ComponentModel.ISupportInitialize)(this.mLogDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mItemsDS)).EndInit();
            this.tabDialog.ResumeLayout(false);
            this.tabItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.tabLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _lblScanned;
        private System.Windows.Forms.Label lblScanned;
        private System.Windows.Forms.Label _lblCartons;
        private System.Windows.Forms.Label _lblFrieghtID;
        private System.Windows.Forms.Label lblCartons;
        private System.Windows.Forms.Label lblFreightID;
        private System.Windows.Forms.DataGridTableStyle dataGridTableStyle1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn1;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn2;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn3;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn4;
        private System.Windows.Forms.DataGridTextBoxColumn dataGridTextBoxColumn5;
    }
}
