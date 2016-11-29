namespace Tsort.Tools {
    partial class winPanda {
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.lblLDRes09 = new System.Windows.Forms.Label();
            this.lblLDRes08 = new System.Windows.Forms.Label();
            this.lblLDRes07 = new System.Windows.Forms.Label();
            this.lblLDRes06 = new System.Windows.Forms.Label();
            this.lblLDRes05 = new System.Windows.Forms.Label();
            this.lblLDRes04 = new System.Windows.Forms.Label();
            this.lblLDRes03 = new System.Windows.Forms.Label();
            this.lblLDRes02 = new System.Windows.Forms.Label();
            this.lblLDRes01 = new System.Windows.Forms.Label();
            this.lblLDRes = new System.Windows.Forms.Label();
            this.lblLDReq = new System.Windows.Forms.Label();
            this._lblLDReq = new System.Windows.Forms.Label();
            this._lblLDRes03 = new System.Windows.Forms.Label();
            this._lblLDRes04 = new System.Windows.Forms.Label();
            this._lblLDRes06 = new System.Windows.Forms.Label();
            this._lblLDRes05 = new System.Windows.Forms.Label();
            this._lblLDRes07 = new System.Windows.Forms.Label();
            this._lblLDRes08 = new System.Windows.Forms.Label();
            this._lblLDRes09 = new System.Windows.Forms.Label();
            this._lblLDRes = new System.Windows.Forms.Label();
            this._lblLDRes01 = new System.Windows.Forms.Label();
            this._lblLDRes02 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblVLReq = new System.Windows.Forms.Label();
            this._lblVLReq = new System.Windows.Forms.Label();
            this._lblVLReq1 = new System.Windows.Forms.Label();
            this._lblVLReq2 = new System.Windows.Forms.Label();
            this._lblVLResY = new System.Windows.Forms.Label();
            this._lblVLResN = new System.Windows.Forms.Label();
            this._lblVLRes = new System.Windows.Forms.Label();
            this._lblVLReq3 = new System.Windows.Forms.Label();
            this.lblVLReq3 = new System.Windows.Forms.Label();
            this.lblVLReq1 = new System.Windows.Forms.Label();
            this.lblVLReq2 = new System.Windows.Forms.Label();
            this.lblVLResY = new System.Windows.Forms.Label();
            this.lblVLResN = new System.Windows.Forms.Label();
            this.lblVLRes = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mLogDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mItemsDS)).BeginInit();
            this.tabDialog.SuspendLayout();
            this.tabItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            this.tabLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdLog)).BeginInit();
            this.tabStats.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdItems
            // 
            this.grdItems.DataMember = "SortedItemTable";
            this.grdItems.TableStyles.AddRange(new System.Windows.Forms.DataGridTableStyle[] {
            this.dataGridTableStyle1});
            // 
            // tabStats
            // 
            this.tabStats.Controls.Add(this.groupBox3);
            this.tabStats.Controls.Add(this.groupBox2);
            // 
            // _lblScanned
            // 
            this._lblScanned.Location = new System.Drawing.Point(158,38);
            this._lblScanned.Name = "_lblScanned";
            this._lblScanned.Size = new System.Drawing.Size(48,18);
            this._lblScanned.TabIndex = 40;
            this._lblScanned.Text = "Scans";
            this._lblScanned.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblScanned
            // 
            this.lblScanned.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblScanned.Location = new System.Drawing.Point(158,56);
            this.lblScanned.Name = "lblScanned";
            this.lblScanned.Size = new System.Drawing.Size(48,18);
            this.lblScanned.TabIndex = 39;
            this.lblScanned.Text = "0";
            this.lblScanned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblCartons
            // 
            this._lblCartons.Location = new System.Drawing.Point(104,38);
            this._lblCartons.Name = "_lblCartons";
            this._lblCartons.Size = new System.Drawing.Size(48,18);
            this._lblCartons.TabIndex = 38;
            this._lblCartons.Text = "Cartns";
            this._lblCartons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblFrieghtID
            // 
            this._lblFrieghtID.Location = new System.Drawing.Point(2,38);
            this._lblFrieghtID.Name = "_lblFrieghtID";
            this._lblFrieghtID.Size = new System.Drawing.Size(96,18);
            this._lblFrieghtID.TabIndex = 37;
            this._lblFrieghtID.Text = "FreightID";
            this._lblFrieghtID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCartons
            // 
            this.lblCartons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCartons.Location = new System.Drawing.Point(104,56);
            this.lblCartons.Name = "lblCartons";
            this.lblCartons.Size = new System.Drawing.Size(48,18);
            this.lblCartons.TabIndex = 36;
            this.lblCartons.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFreightID
            // 
            this.lblFreightID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFreightID.Location = new System.Drawing.Point(2,56);
            this.lblFreightID.Name = "lblFreightID";
            this.lblFreightID.Size = new System.Drawing.Size(96,18);
            this.lblFreightID.TabIndex = 35;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tableLayoutPanel2);
            this.groupBox2.Location = new System.Drawing.Point(3,3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324,226);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Label Data";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,144F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,96F));
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes09,1,10);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes08,1,9);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes07,1,8);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes06,1,7);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes05,1,6);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes04,1,5);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes03,1,4);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes02,1,3);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes01,1,2);
            this.tableLayoutPanel2.Controls.Add(this.lblLDRes,1,1);
            this.tableLayoutPanel2.Controls.Add(this.lblLDReq,1,0);
            this.tableLayoutPanel2.Controls.Add(this._lblLDReq,0,0);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes03,0,4);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes04,0,5);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes06,0,7);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes05,0,6);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes07,0,8);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes08,0,9);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes09,0,10);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes,0,1);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes01,0,2);
            this.tableLayoutPanel2.Controls.Add(this._lblLDRes02,0,3);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6,20);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 11;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(286,200);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // lblLDRes09
            // 
            this.lblLDRes09.Location = new System.Drawing.Point(147,180);
            this.lblLDRes09.Name = "lblLDRes09";
            this.lblLDRes09.Size = new System.Drawing.Size(136,16);
            this.lblLDRes09.TabIndex = 21;
            this.lblLDRes09.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes08
            // 
            this.lblLDRes08.Location = new System.Drawing.Point(147,162);
            this.lblLDRes08.Name = "lblLDRes08";
            this.lblLDRes08.Size = new System.Drawing.Size(136,16);
            this.lblLDRes08.TabIndex = 20;
            this.lblLDRes08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes07
            // 
            this.lblLDRes07.Location = new System.Drawing.Point(147,144);
            this.lblLDRes07.Name = "lblLDRes07";
            this.lblLDRes07.Size = new System.Drawing.Size(136,16);
            this.lblLDRes07.TabIndex = 19;
            this.lblLDRes07.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes06
            // 
            this.lblLDRes06.Location = new System.Drawing.Point(147,126);
            this.lblLDRes06.Name = "lblLDRes06";
            this.lblLDRes06.Size = new System.Drawing.Size(136,16);
            this.lblLDRes06.TabIndex = 18;
            this.lblLDRes06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes05
            // 
            this.lblLDRes05.Location = new System.Drawing.Point(147,108);
            this.lblLDRes05.Name = "lblLDRes05";
            this.lblLDRes05.Size = new System.Drawing.Size(136,16);
            this.lblLDRes05.TabIndex = 17;
            this.lblLDRes05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes04
            // 
            this.lblLDRes04.Location = new System.Drawing.Point(147,90);
            this.lblLDRes04.Name = "lblLDRes04";
            this.lblLDRes04.Size = new System.Drawing.Size(136,16);
            this.lblLDRes04.TabIndex = 16;
            this.lblLDRes04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes03
            // 
            this.lblLDRes03.Location = new System.Drawing.Point(147,72);
            this.lblLDRes03.Name = "lblLDRes03";
            this.lblLDRes03.Size = new System.Drawing.Size(136,16);
            this.lblLDRes03.TabIndex = 15;
            this.lblLDRes03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes02
            // 
            this.lblLDRes02.Location = new System.Drawing.Point(147,54);
            this.lblLDRes02.Name = "lblLDRes02";
            this.lblLDRes02.Size = new System.Drawing.Size(136,16);
            this.lblLDRes02.TabIndex = 14;
            this.lblLDRes02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes01
            // 
            this.lblLDRes01.Location = new System.Drawing.Point(147,36);
            this.lblLDRes01.Name = "lblLDRes01";
            this.lblLDRes01.Size = new System.Drawing.Size(136,16);
            this.lblLDRes01.TabIndex = 13;
            this.lblLDRes01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDRes
            // 
            this.lblLDRes.Location = new System.Drawing.Point(147,18);
            this.lblLDRes.Name = "lblLDRes";
            this.lblLDRes.Size = new System.Drawing.Size(136,16);
            this.lblLDRes.TabIndex = 12;
            this.lblLDRes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLDReq
            // 
            this.lblLDReq.Location = new System.Drawing.Point(147,0);
            this.lblLDReq.Name = "lblLDReq";
            this.lblLDReq.Size = new System.Drawing.Size(136,16);
            this.lblLDReq.TabIndex = 11;
            this.lblLDReq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDReq
            // 
            this._lblLDReq.Location = new System.Drawing.Point(3,0);
            this._lblLDReq.Name = "_lblLDReq";
            this._lblLDReq.Size = new System.Drawing.Size(138,16);
            this._lblLDReq.TabIndex = 1;
            this._lblLDReq.Text = "Requests:";
            this._lblLDReq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes03
            // 
            this._lblLDRes03.Location = new System.Drawing.Point(3,72);
            this._lblLDRes03.Name = "_lblLDRes03";
            this._lblLDRes03.Size = new System.Drawing.Size(138,16);
            this._lblLDRes03.TabIndex = 4;
            this._lblLDRes03.Text = "   03- No Read:";
            this._lblLDRes03.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes04
            // 
            this._lblLDRes04.Location = new System.Drawing.Point(3,90);
            this._lblLDRes04.Name = "_lblLDRes04";
            this._lblLDRes04.Size = new System.Drawing.Size(138,16);
            this._lblLDRes04.TabIndex = 6;
            this._lblLDRes04.Text = "   04- Label Conf:";
            this._lblLDRes04.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes06
            // 
            this._lblLDRes06.Location = new System.Drawing.Point(3,126);
            this._lblLDRes06.Name = "_lblLDRes06";
            this._lblLDRes06.Size = new System.Drawing.Size(138,16);
            this._lblLDRes06.TabIndex = 7;
            this._lblLDRes06.Text = "   06- Ignore:";
            this._lblLDRes06.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes05
            // 
            this._lblLDRes05.Location = new System.Drawing.Point(3,108);
            this._lblLDRes05.Name = "_lblLDRes05";
            this._lblLDRes05.Size = new System.Drawing.Size(138,16);
            this._lblLDRes05.TabIndex = 5;
            this._lblLDRes05.Text = "   05- Label Undet:";
            this._lblLDRes05.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes07
            // 
            this._lblLDRes07.Location = new System.Drawing.Point(3,144);
            this._lblLDRes07.Name = "_lblLDRes07";
            this._lblLDRes07.Size = new System.Drawing.Size(138,16);
            this._lblLDRes07.TabIndex = 8;
            this._lblLDRes07.Text = "   07- Ignore:";
            this._lblLDRes07.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes08
            // 
            this._lblLDRes08.Location = new System.Drawing.Point(3,162);
            this._lblLDRes08.Name = "_lblLDRes08";
            this._lblLDRes08.Size = new System.Drawing.Size(138,16);
            this._lblLDRes08.TabIndex = 9;
            this._lblLDRes08.Text = "   08- Bad Weight:";
            this._lblLDRes08.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes09
            // 
            this._lblLDRes09.Location = new System.Drawing.Point(3,180);
            this._lblLDRes09.Name = "_lblLDRes09";
            this._lblLDRes09.Size = new System.Drawing.Size(138,16);
            this._lblLDRes09.TabIndex = 10;
            this._lblLDRes09.Text = "   09- Misc:";
            this._lblLDRes09.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes
            // 
            this._lblLDRes.Location = new System.Drawing.Point(3,18);
            this._lblLDRes.Name = "_lblLDRes";
            this._lblLDRes.Size = new System.Drawing.Size(138,16);
            this._lblLDRes.TabIndex = 2;
            this._lblLDRes.Text = "Responses:";
            this._lblLDRes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes01
            // 
            this._lblLDRes01.Location = new System.Drawing.Point(3,36);
            this._lblLDRes01.Name = "_lblLDRes01";
            this._lblLDRes01.Size = new System.Drawing.Size(138,16);
            this._lblLDRes01.TabIndex = 2;
            this._lblLDRes01.Text = "   01- OK:";
            this._lblLDRes01.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLDRes02
            // 
            this._lblLDRes02.Location = new System.Drawing.Point(3,54);
            this._lblLDRes02.Name = "_lblLDRes02";
            this._lblLDRes02.Size = new System.Drawing.Size(138,16);
            this._lblLDRes02.TabIndex = 3;
            this._lblLDRes02.Text = "   02- No Data:";
            this._lblLDRes02.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel1);
            this.groupBox3.Location = new System.Drawing.Point(333,3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(316,226);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Verify Label";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,144F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute,96F));
            this.tableLayoutPanel1.Controls.Add(this.lblVLReq,1,0);
            this.tableLayoutPanel1.Controls.Add(this._lblVLReq,0,0);
            this.tableLayoutPanel1.Controls.Add(this._lblVLReq1,0,1);
            this.tableLayoutPanel1.Controls.Add(this._lblVLReq2,0,2);
            this.tableLayoutPanel1.Controls.Add(this._lblVLRes,0,5);
            this.tableLayoutPanel1.Controls.Add(this._lblVLReq3,0,3);
            this.tableLayoutPanel1.Controls.Add(this.lblVLReq3,1,3);
            this.tableLayoutPanel1.Controls.Add(this.lblVLReq1,1,1);
            this.tableLayoutPanel1.Controls.Add(this.lblVLReq2,1,2);
            this.tableLayoutPanel1.Controls.Add(this.lblVLRes,1,5);
            this.tableLayoutPanel1.Controls.Add(this._lblVLResY,0,6);
            this.tableLayoutPanel1.Controls.Add(this._lblVLResN,0,7);
            this.tableLayoutPanel1.Controls.Add(this.lblVLResY,1,6);
            this.tableLayoutPanel1.Controls.Add(this.lblVLResN,1,7);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(9,20);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 11;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute,18F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(286,200);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lblVLReq
            // 
            this.lblVLReq.Location = new System.Drawing.Point(147,0);
            this.lblVLReq.Name = "lblVLReq";
            this.lblVLReq.Size = new System.Drawing.Size(136,16);
            this.lblVLReq.TabIndex = 12;
            this.lblVLReq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVLReq
            // 
            this._lblVLReq.Location = new System.Drawing.Point(3,0);
            this._lblVLReq.Name = "_lblVLReq";
            this._lblVLReq.Size = new System.Drawing.Size(138,16);
            this._lblVLReq.TabIndex = 1;
            this._lblVLReq.Text = "Requests:";
            this._lblVLReq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVLReq1
            // 
            this._lblVLReq1.Location = new System.Drawing.Point(3,18);
            this._lblVLReq1.Name = "_lblVLReq1";
            this._lblVLReq1.Size = new System.Drawing.Size(138,16);
            this._lblVLReq1.TabIndex = 5;
            this._lblVLReq1.Text = "   1: Pass";
            this._lblVLReq1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVLReq2
            // 
            this._lblVLReq2.Location = new System.Drawing.Point(3,36);
            this._lblVLReq2.Name = "_lblVLReq2";
            this._lblVLReq2.Size = new System.Drawing.Size(138,16);
            this._lblVLReq2.TabIndex = 6;
            this._lblVLReq2.Text = "   2: Fail";
            this._lblVLReq2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVLResY
            // 
            this._lblVLResY.Location = new System.Drawing.Point(3,108);
            this._lblVLResY.Name = "_lblVLResY";
            this._lblVLResY.Size = new System.Drawing.Size(138,18);
            this._lblVLResY.TabIndex = 4;
            this._lblVLResY.Text = "   Y: Saved";
            this._lblVLResY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVLResN
            // 
            this._lblVLResN.Location = new System.Drawing.Point(3,126);
            this._lblVLResN.Name = "_lblVLResN";
            this._lblVLResN.Size = new System.Drawing.Size(138,16);
            this._lblVLResN.TabIndex = 3;
            this._lblVLResN.Text = "   N: Cancelled";
            this._lblVLResN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVLRes
            // 
            this._lblVLRes.Location = new System.Drawing.Point(3,90);
            this._lblVLRes.Name = "_lblVLRes";
            this._lblVLRes.Size = new System.Drawing.Size(138,16);
            this._lblVLRes.TabIndex = 2;
            this._lblVLRes.Text = "Responses:";
            this._lblVLRes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVLReq3
            // 
            this._lblVLReq3.Location = new System.Drawing.Point(3,54);
            this._lblVLReq3.Name = "_lblVLReq3";
            this._lblVLReq3.Size = new System.Drawing.Size(138,16);
            this._lblVLReq3.TabIndex = 18;
            this._lblVLReq3.Text = "   3: No Read";
            this._lblVLReq3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVLReq3
            // 
            this.lblVLReq3.Location = new System.Drawing.Point(147,54);
            this.lblVLReq3.Name = "lblVLReq3";
            this.lblVLReq3.Size = new System.Drawing.Size(136,16);
            this.lblVLReq3.TabIndex = 13;
            this.lblVLReq3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVLReq1
            // 
            this.lblVLReq1.Location = new System.Drawing.Point(147,18);
            this.lblVLReq1.Name = "lblVLReq1";
            this.lblVLReq1.Size = new System.Drawing.Size(136,16);
            this.lblVLReq1.TabIndex = 14;
            this.lblVLReq1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVLReq2
            // 
            this.lblVLReq2.Location = new System.Drawing.Point(147,36);
            this.lblVLReq2.Name = "lblVLReq2";
            this.lblVLReq2.Size = new System.Drawing.Size(136,16);
            this.lblVLReq2.TabIndex = 19;
            this.lblVLReq2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVLResY
            // 
            this.lblVLResY.Location = new System.Drawing.Point(147,108);
            this.lblVLResY.Name = "lblVLResY";
            this.lblVLResY.Size = new System.Drawing.Size(136,16);
            this.lblVLResY.TabIndex = 17;
            this.lblVLResY.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVLResN
            // 
            this.lblVLResN.Location = new System.Drawing.Point(147,126);
            this.lblVLResN.Name = "lblVLResN";
            this.lblVLResN.Size = new System.Drawing.Size(136,16);
            this.lblVLResN.TabIndex = 16;
            this.lblVLResN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVLRes
            // 
            this.lblVLRes.Location = new System.Drawing.Point(147,90);
            this.lblVLRes.Name = "lblVLRes";
            this.lblVLRes.Size = new System.Drawing.Size(136,16);
            this.lblVLRes.TabIndex = 15;
            this.lblVLRes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // winPanda
            // 
            this.ClientSize = new System.Drawing.Size(664,350);
            this.Controls.Add(this._lblScanned);
            this.Controls.Add(this.lblScanned);
            this.Controls.Add(this._lblCartons);
            this.Controls.Add(this._lblFrieghtID);
            this.Controls.Add(this.lblCartons);
            this.Controls.Add(this.lblFreightID);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Name = "winPanda";
            this.Text = "PandA Sort Station";
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
            this.tabStats.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
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
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label _lblVLRes;
        private System.Windows.Forms.Label _lblVLReq;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label _lblLDRes04;
        private System.Windows.Forms.Label _lblLDRes05;
        private System.Windows.Forms.Label _lblLDRes03;
        private System.Windows.Forms.Label _lblLDRes09;
        private System.Windows.Forms.Label _lblLDRes08;
        private System.Windows.Forms.Label _lblLDRes07;
        private System.Windows.Forms.Label _lblLDRes06;
        private System.Windows.Forms.Label _lblLDRes01;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label _lblVLResN;
        private System.Windows.Forms.Label _lblVLResY;
        private System.Windows.Forms.Label _lblVLReq1;
        private System.Windows.Forms.Label _lblVLReq2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label _lblLDReq;
        private System.Windows.Forms.Label _lblLDRes;
        private System.Windows.Forms.Label lblVLReq1;
        private System.Windows.Forms.Label lblVLReq3;
        private System.Windows.Forms.Label lblVLReq;
        private System.Windows.Forms.Label lblVLRes;
        private System.Windows.Forms.Label lblVLResN;
        private System.Windows.Forms.Label lblVLResY;
        private System.Windows.Forms.Label lblLDRes09;
        private System.Windows.Forms.Label lblLDRes08;
        private System.Windows.Forms.Label lblLDRes07;
        private System.Windows.Forms.Label lblLDRes06;
        private System.Windows.Forms.Label lblLDRes05;
        private System.Windows.Forms.Label lblLDRes04;
        private System.Windows.Forms.Label lblLDRes03;
        private System.Windows.Forms.Label lblLDRes02;
        private System.Windows.Forms.Label lblLDRes01;
        private System.Windows.Forms.Label lblLDRes;
        private System.Windows.Forms.Label lblLDReq;
        private System.Windows.Forms.Label _lblLDRes02;
        private System.Windows.Forms.Label _lblVLReq3;
        private System.Windows.Forms.Label lblVLReq2;
    }
}
