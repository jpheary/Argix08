namespace Argix.Finance {
    partial class ctlDriverEquipment {
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DriverEquipmentTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendorID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OperatorName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentName");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mDriverCompDS = new Argix.Finance.DriverCompDS();
            this.tlbMain = new System.Windows.Forms.ToolStrip();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.uddEquipType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.ctxMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mDriverCompDS)).BeginInit();
            this.tlbMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.ContextMenuStrip = this.ctxMain;
            this.grdMain.DataMember = "DriverEquipmentTable";
            this.grdMain.DataSource = this.mDriverCompDS;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Appearance = appearance13;
            ultraGridColumn5.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn5.Header.Caption = "VendorID";
            ultraGridColumn5.Header.VisiblePosition = 0;
            ultraGridColumn5.Width = 78;
            ultraGridColumn6.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn6.Header.Caption = "Operator";
            ultraGridColumn6.Header.VisiblePosition = 1;
            ultraGridColumn6.Width = 192;
            ultraGridColumn7.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn7.Header.Caption = "Equip Type";
            ultraGridColumn7.Header.VisiblePosition = 2;
            ultraGridColumn7.Width = 120;
            ultraGridColumn8.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn8.Header.Caption = "Equipment";
            ultraGridColumn8.Header.VisiblePosition = 3;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn8.Width = 120;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8});
            this.grdMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.CaptionAppearance = appearance14;
            this.grdMain.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdMain.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.grdMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdMain.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdMain.DisplayLayout.Override.RowAppearance = appearance16;
            this.grdMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdMain.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdMain.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdMain.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdMain.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0,25);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(576,71);
            this.grdMain.TabIndex = 0;
            this.grdMain.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdMain.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdMain.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdMain.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellChange);
            // 
            // ctxMain
            // 
            this.ctxMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRefresh});
            this.ctxMain.Name = "ctxMain";
            this.ctxMain.Size = new System.Drawing.Size(153,48);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxRefresh.ImageTransparentColor = System.Drawing.Color.Maroon;
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(152,22);
            this.ctxRefresh.Text = "Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mDriverCompDS
            // 
            this.mDriverCompDS.DataSetName = "DriverCompDS";
            this.mDriverCompDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tlbMain
            // 
            this.tlbMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tlbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnRefresh});
            this.tlbMain.Location = new System.Drawing.Point(0,0);
            this.tlbMain.Name = "tlbMain";
            this.tlbMain.Size = new System.Drawing.Size(576,25);
            this.tlbMain.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(23,22);
            this.btnRefresh.ToolTipText = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // uddEquipType
            // 
            this.uddEquipType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddEquipType.Location = new System.Drawing.Point(0,78);
            this.uddEquipType.Name = "uddEquipType";
            this.uddEquipType.Size = new System.Drawing.Size(104,18);
            this.uddEquipType.TabIndex = 8;
            this.uddEquipType.Text = "ultraDropDown1";
            this.uddEquipType.Visible = false;
            // 
            // ctlDriverEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uddEquipType);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.tlbMain);
            this.Name = "ctlDriverEquipment";
            this.Size = new System.Drawing.Size(576,96);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.ctxMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mDriverCompDS)).EndInit();
            this.tlbMain.ResumeLayout(false);
            this.tlbMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
        private System.Windows.Forms.ToolStrip tlbMain;
        private DriverCompDS mDriverCompDS;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.ContextMenuStrip ctxMain;
        private System.Windows.Forms.ToolStripMenuItem ctxRefresh;
        private Infragistics.Win.UltraWinGrid.UltraDropDown uddEquipType;
    }
}
