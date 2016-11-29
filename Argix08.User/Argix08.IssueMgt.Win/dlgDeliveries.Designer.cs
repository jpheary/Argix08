namespace Argix.Customers {
    partial class dlgDeliveries {
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeliveryTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CBOL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CPROID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CPRONumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StoreNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PodDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PodTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShouldBeDeliveredOn");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WindowStartTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WindowEndTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsSorted");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsMatch");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsShort");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsOver");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsMisroute");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsDamaged");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsScanned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsManual");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsMatch");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsShort");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsOver");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsMisroute");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsDamaged");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsScanned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsManual");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.btnOk = new System.Windows.Forms.Button();
            this.grdDeliveries = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mDeliveryDS = new Argix.Customers.DeliveryDS();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeliveries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDeliveryDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(483,138);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82,24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // grdDeliveries
            // 
            this.grdDeliveries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDeliveries.DataMember = "DeliveryTable";
            this.grdDeliveries.DataSource = this.mDeliveryDS;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdDeliveries.DisplayLayout.Appearance = appearance13;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Caption = "CPRO#";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 72;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Format = "MM-dd-yyyy";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Width = 96;
            ultraGridColumn6.Header.VisiblePosition = 7;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Format = "MM-dd-yyyy";
            ultraGridColumn7.Header.Caption = "Pod Date";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 96;
            ultraGridColumn8.Format = "HH:mm";
            ultraGridColumn8.Header.Caption = "Pod Time";
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Width = 72;
            ultraGridColumn9.Format = "MM-dd-yyyy";
            ultraGridColumn9.Header.Caption = "Est Delivery On";
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.Width = 120;
            ultraGridColumn10.Format = "HH:mm";
            ultraGridColumn10.Header.Caption = "Window Start";
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn10.Width = 96;
            ultraGridColumn11.Format = "HH:mm";
            ultraGridColumn11.Header.Caption = "Window End";
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.Width = 96;
            ultraGridColumn12.Header.Caption = "Cartons";
            ultraGridColumn12.Header.VisiblePosition = 4;
            ultraGridColumn12.Width = 72;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn14.Header.VisiblePosition = 13;
            ultraGridColumn14.Width = 192;
            ultraGridColumn15.Header.Caption = "POD Match";
            ultraGridColumn15.Header.VisiblePosition = 14;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.Header.Caption = "POD Short";
            ultraGridColumn16.Header.VisiblePosition = 15;
            ultraGridColumn16.Width = 72;
            ultraGridColumn17.Header.Caption = "POD Over";
            ultraGridColumn17.Header.VisiblePosition = 16;
            ultraGridColumn17.Width = 72;
            ultraGridColumn18.Header.Caption = "POD Misroute";
            ultraGridColumn18.Header.VisiblePosition = 17;
            ultraGridColumn18.Width = 72;
            ultraGridColumn19.Header.Caption = "POD Damaged";
            ultraGridColumn19.Header.VisiblePosition = 18;
            ultraGridColumn19.Width = 72;
            ultraGridColumn20.Header.Caption = "POD Scanned";
            ultraGridColumn20.Header.VisiblePosition = 19;
            ultraGridColumn20.Width = 72;
            ultraGridColumn21.Header.Caption = "POD Manual";
            ultraGridColumn21.Header.VisiblePosition = 20;
            ultraGridColumn21.Width = 72;
            ultraGridColumn22.Header.Caption = "OSD Match";
            ultraGridColumn22.Header.VisiblePosition = 21;
            ultraGridColumn22.Width = 72;
            ultraGridColumn23.Header.Caption = "OSD Short";
            ultraGridColumn23.Header.VisiblePosition = 22;
            ultraGridColumn23.Width = 72;
            ultraGridColumn24.Header.Caption = "OSD Over";
            ultraGridColumn24.Header.VisiblePosition = 23;
            ultraGridColumn24.Width = 72;
            ultraGridColumn25.Header.Caption = "OSD Misroute";
            ultraGridColumn25.Header.VisiblePosition = 24;
            ultraGridColumn25.Width = 72;
            ultraGridColumn26.Header.Caption = "OSD Damaged";
            ultraGridColumn26.Header.VisiblePosition = 25;
            ultraGridColumn26.Width = 72;
            ultraGridColumn27.Header.Caption = "OSD Scanned";
            ultraGridColumn27.Header.VisiblePosition = 26;
            ultraGridColumn27.Width = 72;
            ultraGridColumn28.Header.Caption = "OSD Manual";
            ultraGridColumn28.Header.VisiblePosition = 27;
            ultraGridColumn28.Width = 72;
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
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28});
            this.grdDeliveries.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdDeliveries.DisplayLayout.CaptionAppearance = appearance14;
            this.grdDeliveries.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDeliveries.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDeliveries.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDeliveries.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.grdDeliveries.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.grdDeliveries.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdDeliveries.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdDeliveries.DisplayLayout.Override.RowAppearance = appearance16;
            this.grdDeliveries.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDeliveries.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDeliveries.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDeliveries.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdDeliveries.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDeliveries.Location = new System.Drawing.Point(0,0);
            this.grdDeliveries.Name = "grdDeliveries";
            this.grdDeliveries.Size = new System.Drawing.Size(654,129);
            this.grdDeliveries.TabIndex = 2;
            this.grdDeliveries.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdDeliveries.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnDeliverySelected);
            // 
            // mDeliveryDS
            // 
            this.mDeliveryDS.DataSetName = "DeliveryDS";
            this.mDeliveryDS.Locale = new System.Globalization.CultureInfo("");
            this.mDeliveryDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(568,138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82,24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // dlgDeliveries
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653,168);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.grdDeliveries);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dlgDeliveries";
            this.Text = "Deliveries";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdDeliveries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDeliveryDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDeliveries;
        private System.Windows.Forms.Button btnCancel;
        private DeliveryDS mDeliveryDS;
    }
}