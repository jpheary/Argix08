namespace Argix.Freight {
    partial class frmMain {
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
            this.grdAssignments = new System.Windows.Forms.DataGridView();
            this.stationNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.workStationIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sortTypeIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freightIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sortTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.freightTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.terminalIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tDSNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.trailerNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vendorKeyDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientDivisionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pickupDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pickupNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAddressLine1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAddressLine2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAddressCityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAddressStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clientAddressZipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cubeRatioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperAddressLine1DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperAddressLine2DataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperAddressCityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperAddressZipDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperAddressStateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shipperUserDataDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.excLocationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.input1LengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.input2LengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.input3LengthDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mAssignmentDS = new Argix.Freight.StationAssignmentDS();
            this._lblInput1 = new System.Windows.Forms.Label();
            this.txtInput1 = new System.Windows.Forms.TextBox();
            this.txtInput2 = new System.Windows.Forms.TextBox();
            this._lblInput2 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentDS)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAssignments
            // 
            this.grdAssignments.AutoGenerateColumns = false;
            this.grdAssignments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAssignments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stationNumberDataGridViewTextBoxColumn,
            this.workStationIDDataGridViewTextBoxColumn,
            this.sortTypeIDDataGridViewTextBoxColumn,
            this.freightIDDataGridViewTextBoxColumn,
            this.sortTypeDataGridViewTextBoxColumn,
            this.freightTypeDataGridViewTextBoxColumn,
            this.terminalIDDataGridViewTextBoxColumn,
            this.tDSNumberDataGridViewTextBoxColumn,
            this.trailerNumberDataGridViewTextBoxColumn,
            this.vendorKeyDataGridViewTextBoxColumn,
            this.clientNumberDataGridViewTextBoxColumn,
            this.clientDivisionDataGridViewTextBoxColumn,
            this.pickupDateDataGridViewTextBoxColumn,
            this.pickupNumberDataGridViewTextBoxColumn,
            this.clientDataGridViewTextBoxColumn,
            this.clientAddressLine1DataGridViewTextBoxColumn,
            this.clientAddressLine2DataGridViewTextBoxColumn,
            this.clientAddressCityDataGridViewTextBoxColumn,
            this.clientAddressStateDataGridViewTextBoxColumn,
            this.clientAddressZipDataGridViewTextBoxColumn,
            this.cubeRatioDataGridViewTextBoxColumn,
            this.shipperNumberDataGridViewTextBoxColumn,
            this.shipperDataGridViewTextBoxColumn,
            this.shipperAddressLine1DataGridViewTextBoxColumn,
            this.shipperAddressLine2DataGridViewTextBoxColumn,
            this.shipperAddressCityDataGridViewTextBoxColumn,
            this.shipperAddressZipDataGridViewTextBoxColumn,
            this.shipperAddressStateDataGridViewTextBoxColumn,
            this.shipperUserDataDataGridViewTextBoxColumn,
            this.labelIDDataGridViewTextBoxColumn,
            this.excLocationDataGridViewTextBoxColumn,
            this.input1LengthDataGridViewTextBoxColumn,
            this.input2LengthDataGridViewTextBoxColumn,
            this.input3LengthDataGridViewTextBoxColumn});
            this.grdAssignments.DataMember = "AssignmentsTable";
            this.grdAssignments.DataSource = this.mAssignmentDS;
            this.grdAssignments.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdAssignments.Location = new System.Drawing.Point(0,0);
            this.grdAssignments.Name = "grdAssignments";
            this.grdAssignments.Size = new System.Drawing.Size(653,149);
            this.grdAssignments.TabIndex = 0;
            // 
            // stationNumberDataGridViewTextBoxColumn
            // 
            this.stationNumberDataGridViewTextBoxColumn.DataPropertyName = "StationNumber";
            this.stationNumberDataGridViewTextBoxColumn.HeaderText = "StationNumber";
            this.stationNumberDataGridViewTextBoxColumn.Name = "stationNumberDataGridViewTextBoxColumn";
            // 
            // workStationIDDataGridViewTextBoxColumn
            // 
            this.workStationIDDataGridViewTextBoxColumn.DataPropertyName = "WorkStationID";
            this.workStationIDDataGridViewTextBoxColumn.HeaderText = "WorkStationID";
            this.workStationIDDataGridViewTextBoxColumn.Name = "workStationIDDataGridViewTextBoxColumn";
            // 
            // sortTypeIDDataGridViewTextBoxColumn
            // 
            this.sortTypeIDDataGridViewTextBoxColumn.DataPropertyName = "SortTypeID";
            this.sortTypeIDDataGridViewTextBoxColumn.HeaderText = "SortTypeID";
            this.sortTypeIDDataGridViewTextBoxColumn.Name = "sortTypeIDDataGridViewTextBoxColumn";
            // 
            // freightIDDataGridViewTextBoxColumn
            // 
            this.freightIDDataGridViewTextBoxColumn.DataPropertyName = "FreightID";
            this.freightIDDataGridViewTextBoxColumn.HeaderText = "FreightID";
            this.freightIDDataGridViewTextBoxColumn.Name = "freightIDDataGridViewTextBoxColumn";
            // 
            // sortTypeDataGridViewTextBoxColumn
            // 
            this.sortTypeDataGridViewTextBoxColumn.DataPropertyName = "SortType";
            this.sortTypeDataGridViewTextBoxColumn.HeaderText = "SortType";
            this.sortTypeDataGridViewTextBoxColumn.Name = "sortTypeDataGridViewTextBoxColumn";
            // 
            // freightTypeDataGridViewTextBoxColumn
            // 
            this.freightTypeDataGridViewTextBoxColumn.DataPropertyName = "FreightType";
            this.freightTypeDataGridViewTextBoxColumn.HeaderText = "FreightType";
            this.freightTypeDataGridViewTextBoxColumn.Name = "freightTypeDataGridViewTextBoxColumn";
            // 
            // terminalIDDataGridViewTextBoxColumn
            // 
            this.terminalIDDataGridViewTextBoxColumn.DataPropertyName = "TerminalID";
            this.terminalIDDataGridViewTextBoxColumn.HeaderText = "TerminalID";
            this.terminalIDDataGridViewTextBoxColumn.Name = "terminalIDDataGridViewTextBoxColumn";
            // 
            // tDSNumberDataGridViewTextBoxColumn
            // 
            this.tDSNumberDataGridViewTextBoxColumn.DataPropertyName = "TDSNumber";
            this.tDSNumberDataGridViewTextBoxColumn.HeaderText = "TDSNumber";
            this.tDSNumberDataGridViewTextBoxColumn.Name = "tDSNumberDataGridViewTextBoxColumn";
            // 
            // trailerNumberDataGridViewTextBoxColumn
            // 
            this.trailerNumberDataGridViewTextBoxColumn.DataPropertyName = "TrailerNumber";
            this.trailerNumberDataGridViewTextBoxColumn.HeaderText = "TrailerNumber";
            this.trailerNumberDataGridViewTextBoxColumn.Name = "trailerNumberDataGridViewTextBoxColumn";
            // 
            // vendorKeyDataGridViewTextBoxColumn
            // 
            this.vendorKeyDataGridViewTextBoxColumn.DataPropertyName = "VendorKey";
            this.vendorKeyDataGridViewTextBoxColumn.HeaderText = "VendorKey";
            this.vendorKeyDataGridViewTextBoxColumn.Name = "vendorKeyDataGridViewTextBoxColumn";
            // 
            // clientNumberDataGridViewTextBoxColumn
            // 
            this.clientNumberDataGridViewTextBoxColumn.DataPropertyName = "ClientNumber";
            this.clientNumberDataGridViewTextBoxColumn.HeaderText = "ClientNumber";
            this.clientNumberDataGridViewTextBoxColumn.Name = "clientNumberDataGridViewTextBoxColumn";
            // 
            // clientDivisionDataGridViewTextBoxColumn
            // 
            this.clientDivisionDataGridViewTextBoxColumn.DataPropertyName = "ClientDivision";
            this.clientDivisionDataGridViewTextBoxColumn.HeaderText = "ClientDivision";
            this.clientDivisionDataGridViewTextBoxColumn.Name = "clientDivisionDataGridViewTextBoxColumn";
            // 
            // pickupDateDataGridViewTextBoxColumn
            // 
            this.pickupDateDataGridViewTextBoxColumn.DataPropertyName = "PickupDate";
            this.pickupDateDataGridViewTextBoxColumn.HeaderText = "PickupDate";
            this.pickupDateDataGridViewTextBoxColumn.Name = "pickupDateDataGridViewTextBoxColumn";
            // 
            // pickupNumberDataGridViewTextBoxColumn
            // 
            this.pickupNumberDataGridViewTextBoxColumn.DataPropertyName = "PickupNumber";
            this.pickupNumberDataGridViewTextBoxColumn.HeaderText = "PickupNumber";
            this.pickupNumberDataGridViewTextBoxColumn.Name = "pickupNumberDataGridViewTextBoxColumn";
            // 
            // clientDataGridViewTextBoxColumn
            // 
            this.clientDataGridViewTextBoxColumn.DataPropertyName = "Client";
            this.clientDataGridViewTextBoxColumn.HeaderText = "Client";
            this.clientDataGridViewTextBoxColumn.Name = "clientDataGridViewTextBoxColumn";
            // 
            // clientAddressLine1DataGridViewTextBoxColumn
            // 
            this.clientAddressLine1DataGridViewTextBoxColumn.DataPropertyName = "ClientAddressLine1";
            this.clientAddressLine1DataGridViewTextBoxColumn.HeaderText = "ClientAddressLine1";
            this.clientAddressLine1DataGridViewTextBoxColumn.Name = "clientAddressLine1DataGridViewTextBoxColumn";
            // 
            // clientAddressLine2DataGridViewTextBoxColumn
            // 
            this.clientAddressLine2DataGridViewTextBoxColumn.DataPropertyName = "ClientAddressLine2";
            this.clientAddressLine2DataGridViewTextBoxColumn.HeaderText = "ClientAddressLine2";
            this.clientAddressLine2DataGridViewTextBoxColumn.Name = "clientAddressLine2DataGridViewTextBoxColumn";
            // 
            // clientAddressCityDataGridViewTextBoxColumn
            // 
            this.clientAddressCityDataGridViewTextBoxColumn.DataPropertyName = "ClientAddressCity";
            this.clientAddressCityDataGridViewTextBoxColumn.HeaderText = "ClientAddressCity";
            this.clientAddressCityDataGridViewTextBoxColumn.Name = "clientAddressCityDataGridViewTextBoxColumn";
            // 
            // clientAddressStateDataGridViewTextBoxColumn
            // 
            this.clientAddressStateDataGridViewTextBoxColumn.DataPropertyName = "ClientAddressState";
            this.clientAddressStateDataGridViewTextBoxColumn.HeaderText = "ClientAddressState";
            this.clientAddressStateDataGridViewTextBoxColumn.Name = "clientAddressStateDataGridViewTextBoxColumn";
            // 
            // clientAddressZipDataGridViewTextBoxColumn
            // 
            this.clientAddressZipDataGridViewTextBoxColumn.DataPropertyName = "ClientAddressZip";
            this.clientAddressZipDataGridViewTextBoxColumn.HeaderText = "ClientAddressZip";
            this.clientAddressZipDataGridViewTextBoxColumn.Name = "clientAddressZipDataGridViewTextBoxColumn";
            // 
            // cubeRatioDataGridViewTextBoxColumn
            // 
            this.cubeRatioDataGridViewTextBoxColumn.DataPropertyName = "CubeRatio";
            this.cubeRatioDataGridViewTextBoxColumn.HeaderText = "CubeRatio";
            this.cubeRatioDataGridViewTextBoxColumn.Name = "cubeRatioDataGridViewTextBoxColumn";
            // 
            // shipperNumberDataGridViewTextBoxColumn
            // 
            this.shipperNumberDataGridViewTextBoxColumn.DataPropertyName = "ShipperNumber";
            this.shipperNumberDataGridViewTextBoxColumn.HeaderText = "ShipperNumber";
            this.shipperNumberDataGridViewTextBoxColumn.Name = "shipperNumberDataGridViewTextBoxColumn";
            // 
            // shipperDataGridViewTextBoxColumn
            // 
            this.shipperDataGridViewTextBoxColumn.DataPropertyName = "Shipper";
            this.shipperDataGridViewTextBoxColumn.HeaderText = "Shipper";
            this.shipperDataGridViewTextBoxColumn.Name = "shipperDataGridViewTextBoxColumn";
            // 
            // shipperAddressLine1DataGridViewTextBoxColumn
            // 
            this.shipperAddressLine1DataGridViewTextBoxColumn.DataPropertyName = "ShipperAddressLine1";
            this.shipperAddressLine1DataGridViewTextBoxColumn.HeaderText = "ShipperAddressLine1";
            this.shipperAddressLine1DataGridViewTextBoxColumn.Name = "shipperAddressLine1DataGridViewTextBoxColumn";
            // 
            // shipperAddressLine2DataGridViewTextBoxColumn
            // 
            this.shipperAddressLine2DataGridViewTextBoxColumn.DataPropertyName = "ShipperAddressLine2";
            this.shipperAddressLine2DataGridViewTextBoxColumn.HeaderText = "ShipperAddressLine2";
            this.shipperAddressLine2DataGridViewTextBoxColumn.Name = "shipperAddressLine2DataGridViewTextBoxColumn";
            // 
            // shipperAddressCityDataGridViewTextBoxColumn
            // 
            this.shipperAddressCityDataGridViewTextBoxColumn.DataPropertyName = "ShipperAddressCity";
            this.shipperAddressCityDataGridViewTextBoxColumn.HeaderText = "ShipperAddressCity";
            this.shipperAddressCityDataGridViewTextBoxColumn.Name = "shipperAddressCityDataGridViewTextBoxColumn";
            // 
            // shipperAddressZipDataGridViewTextBoxColumn
            // 
            this.shipperAddressZipDataGridViewTextBoxColumn.DataPropertyName = "ShipperAddressZip";
            this.shipperAddressZipDataGridViewTextBoxColumn.HeaderText = "ShipperAddressZip";
            this.shipperAddressZipDataGridViewTextBoxColumn.Name = "shipperAddressZipDataGridViewTextBoxColumn";
            // 
            // shipperAddressStateDataGridViewTextBoxColumn
            // 
            this.shipperAddressStateDataGridViewTextBoxColumn.DataPropertyName = "ShipperAddressState";
            this.shipperAddressStateDataGridViewTextBoxColumn.HeaderText = "ShipperAddressState";
            this.shipperAddressStateDataGridViewTextBoxColumn.Name = "shipperAddressStateDataGridViewTextBoxColumn";
            // 
            // shipperUserDataDataGridViewTextBoxColumn
            // 
            this.shipperUserDataDataGridViewTextBoxColumn.DataPropertyName = "ShipperUserData";
            this.shipperUserDataDataGridViewTextBoxColumn.HeaderText = "ShipperUserData";
            this.shipperUserDataDataGridViewTextBoxColumn.Name = "shipperUserDataDataGridViewTextBoxColumn";
            // 
            // labelIDDataGridViewTextBoxColumn
            // 
            this.labelIDDataGridViewTextBoxColumn.DataPropertyName = "LabelID";
            this.labelIDDataGridViewTextBoxColumn.HeaderText = "LabelID";
            this.labelIDDataGridViewTextBoxColumn.Name = "labelIDDataGridViewTextBoxColumn";
            // 
            // excLocationDataGridViewTextBoxColumn
            // 
            this.excLocationDataGridViewTextBoxColumn.DataPropertyName = "ExcLocation";
            this.excLocationDataGridViewTextBoxColumn.HeaderText = "ExcLocation";
            this.excLocationDataGridViewTextBoxColumn.Name = "excLocationDataGridViewTextBoxColumn";
            // 
            // input1LengthDataGridViewTextBoxColumn
            // 
            this.input1LengthDataGridViewTextBoxColumn.DataPropertyName = "Input1Length";
            this.input1LengthDataGridViewTextBoxColumn.HeaderText = "Input1Length";
            this.input1LengthDataGridViewTextBoxColumn.Name = "input1LengthDataGridViewTextBoxColumn";
            // 
            // input2LengthDataGridViewTextBoxColumn
            // 
            this.input2LengthDataGridViewTextBoxColumn.DataPropertyName = "Input2Length";
            this.input2LengthDataGridViewTextBoxColumn.HeaderText = "Input2Length";
            this.input2LengthDataGridViewTextBoxColumn.Name = "input2LengthDataGridViewTextBoxColumn";
            // 
            // input3LengthDataGridViewTextBoxColumn
            // 
            this.input3LengthDataGridViewTextBoxColumn.DataPropertyName = "Input3Length";
            this.input3LengthDataGridViewTextBoxColumn.HeaderText = "Input3Length";
            this.input3LengthDataGridViewTextBoxColumn.Name = "input3LengthDataGridViewTextBoxColumn";
            // 
            // mAssignmentDS
            // 
            this.mAssignmentDS.DataSetName = "StationAssignmentDS";
            this.mAssignmentDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblInput1
            // 
            this._lblInput1.Location = new System.Drawing.Point(25,171);
            this._lblInput1.Name = "_lblInput1";
            this._lblInput1.Size = new System.Drawing.Size(72,23);
            this._lblInput1.TabIndex = 1;
            this._lblInput1.Text = "Input 1";
            this._lblInput1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInput1
            // 
            this.txtInput1.DataBindings.Add(new System.Windows.Forms.Binding("MaxLength",this.mAssignmentDS,"AssignmentsTable.Input1Length",true,System.Windows.Forms.DataSourceUpdateMode.Never));
            this.txtInput1.Location = new System.Drawing.Point(107,171);
            this.txtInput1.Name = "txtInput1";
            this.txtInput1.Size = new System.Drawing.Size(192,20);
            this.txtInput1.TabIndex = 2;
            this.txtInput1.TextChanged += new System.EventHandler(this.OnInput1Changed);
            // 
            // txtInput2
            // 
            this.txtInput2.DataBindings.Add(new System.Windows.Forms.Binding("MaxLength",this.mAssignmentDS,"AssignmentsTable.Input2Length",true,System.Windows.Forms.DataSourceUpdateMode.Never));
            this.txtInput2.Location = new System.Drawing.Point(107,197);
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.Size = new System.Drawing.Size(192,20);
            this.txtInput2.TabIndex = 4;
            this.txtInput2.TextChanged += new System.EventHandler(this.OnInput2Changed);
            // 
            // _lblInput2
            // 
            this._lblInput2.Location = new System.Drawing.Point(25,197);
            this._lblInput2.Name = "_lblInput2";
            this._lblInput2.Size = new System.Drawing.Size(72,23);
            this._lblInput2.TabIndex = 3;
            this._lblInput2.Text = "Input 2";
            this._lblInput2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(370,171);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75,46);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnOk);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653,241);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtInput2);
            this.Controls.Add(this._lblInput2);
            this.Controls.Add(this.txtInput1);
            this.Controls.Add(this._lblInput1);
            this.Controls.Add(this.grdAssignments);
            this.Name = "frmMain";
            this.Text = "Vitamin Tote Sort";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grdAssignments;
        private Argix.Freight.StationAssignmentDS mAssignmentDS;
        private System.Windows.Forms.DataGridViewTextBoxColumn stationNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn workStationIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sortTypeIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn freightIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sortTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn freightTypeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn terminalIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tDSNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn trailerNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn vendorKeyDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientDivisionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pickupDateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pickupNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientAddressLine1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientAddressLine2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientAddressCityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientAddressStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn clientAddressZipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cubeRatioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperAddressLine1DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperAddressLine2DataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperAddressCityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperAddressZipDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperAddressStateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn shipperUserDataDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn labelIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn excLocationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn input1LengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn input2LengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn input3LengthDataGridViewTextBoxColumn;
        private System.Windows.Forms.Label _lblInput1;
        private System.Windows.Forms.TextBox txtInput1;
        private System.Windows.Forms.TextBox txtInput2;
        private System.Windows.Forms.Label _lblInput2;
        private System.Windows.Forms.Button btnOk;
    }
}

