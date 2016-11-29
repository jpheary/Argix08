namespace Argix.CustomerSvc {
    partial class IssueSelector {
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
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("IssueTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RegionNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DistrictNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StoreNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD1FromDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD1ToDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PROID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionCreated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionCreated",-1,null,0,Infragistics.Win.UltraWinGrid.SortIndicator.Descending,false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            this.grdIssues = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.issueDSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.issueDS = new Argix.CustomerSvc.IssueDS();
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueDSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueDS)).BeginInit();
            this.SuspendLayout();
            // 
            // grdIssues
            // 
            this.grdIssues.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdIssues.DataMember = "IssueTable";
            this.grdIssues.DataSource = this.issueDSBindingSource;
            appearance29.BackColor = System.Drawing.SystemColors.Window;
            appearance29.FontData.Name = "Verdana";
            appearance29.FontData.SizeInPoints = 8F;
            appearance29.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance29.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.Appearance = appearance29;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.VisiblePosition = 22;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.VisiblePosition = 23;
            ultraGridColumn3.Width = 96;
            ultraGridColumn4.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 144;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 144;
            ultraGridColumn6.Header.Caption = "Contact";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 144;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Width = 144;
            ultraGridColumn8.Header.Caption = "Company";
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Width = 144;
            ultraGridColumn9.Header.Caption = "Region#";
            ultraGridColumn9.Header.VisiblePosition = 7;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn9.Width = 72;
            ultraGridColumn10.Header.Caption = "District#";
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 72;
            ultraGridColumn11.Header.Caption = "Agent#";
            ultraGridColumn11.Header.VisiblePosition = 18;
            ultraGridColumn11.Width = 72;
            ultraGridColumn12.Header.Caption = "Store#";
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn12.Width = 72;
            ultraGridColumn13.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn13.Header.VisiblePosition = 10;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn14.Header.VisiblePosition = 11;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn15.Header.VisiblePosition = 12;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 13;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.Caption = "Initial Action";
            ultraGridColumn17.Header.VisiblePosition = 15;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.VisiblePosition = 16;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.Caption = "Originator";
            ultraGridColumn19.Header.VisiblePosition = 14;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn19.Width = 96;
            ultraGridColumn20.Header.VisiblePosition = 17;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn20.Width = 96;
            ultraGridColumn21.Header.Caption = "Action";
            ultraGridColumn21.Header.VisiblePosition = 20;
            ultraGridColumn21.Width = 96;
            ultraGridColumn22.Format = "MM/dd/yyyy hh:mm tt";
            ultraGridColumn22.Header.Caption = "Received";
            ultraGridColumn22.Header.VisiblePosition = 21;
            ultraGridColumn22.Width = 144;
            ultraGridColumn23.Header.Caption = "Last User";
            ultraGridColumn23.Header.VisiblePosition = 19;
            ultraGridColumn23.Width = 96;
            ultraGridColumn24.Header.VisiblePosition = 1;
            ultraGridColumn24.Width = 60;
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
            ultraGridColumn24});
            this.grdIssues.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance30.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance30.FontData.BoldAsString = "True";
            appearance30.FontData.Name = "Verdana";
            appearance30.FontData.SizeInPoints = 8F;
            appearance30.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance30.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.CaptionAppearance = appearance30;
            this.grdIssues.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdIssues.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance31.BackColor = System.Drawing.SystemColors.Control;
            appearance31.FontData.BoldAsString = "True";
            appearance31.FontData.Name = "Verdana";
            appearance31.FontData.SizeInPoints = 8F;
            appearance31.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.Override.HeaderAppearance = appearance31;
            this.grdIssues.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdIssues.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance32.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdIssues.DisplayLayout.Override.RowAppearance = appearance32;
            this.grdIssues.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdIssues.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdIssues.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdIssues.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdIssues.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdIssues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdIssues.Location = new System.Drawing.Point(0,0);
            this.grdIssues.Name = "grdIssues";
            this.grdIssues.Size = new System.Drawing.Size(694,190);
            this.grdIssues.TabIndex = 124;
            this.grdIssues.UseAppStyling = false;
            this.grdIssues.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            // 
            // issueDSBindingSource
            // 
            this.issueDSBindingSource.DataSource = this.issueDS;
            this.issueDSBindingSource.Position = 0;
            // 
            // issueDS
            // 
            this.issueDS.DataSetName = "IssueDS";
            this.issueDS.Locale = new System.Globalization.CultureInfo("");
            this.issueDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // IssueSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdIssues);
            this.Name = "IssueSelector";
            this.Size = new System.Drawing.Size(694,190);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueDSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.issueDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdIssues;
        private System.Windows.Forms.BindingSource issueDSBindingSource;
        private IssueDS issueDS;
    }
}
