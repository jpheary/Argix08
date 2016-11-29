namespace Argix.Customers {
    partial class winContacts {
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ContactTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FullName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Phone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Mobile");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Fax");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Email");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.mContactDS = new Argix.Customers.ContactDS();
            this.grdContacts = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ctxDialog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.mContactDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdContacts)).BeginInit();
            this.ctxDialog.SuspendLayout();
            this.SuspendLayout();
            // 
            // mContactDS
            // 
            this.mContactDS.DataSetName = "ContactDS";
            this.mContactDS.Locale = new System.Globalization.CultureInfo("");
            this.mContactDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdContacts
            // 
            this.grdContacts.ContextMenuStrip = this.ctxDialog;
            this.grdContacts.DataMember = "ContactTable";
            this.grdContacts.DataSource = this.mContactDS;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdContacts.DisplayLayout.Appearance = appearance13;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "First Name";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.Caption = "Last Name";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 120;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 113;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 107;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 110;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 178;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8});
            this.grdContacts.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdContacts.DisplayLayout.CaptionAppearance = appearance14;
            this.grdContacts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdContacts.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdContacts.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdContacts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.grdContacts.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.grdContacts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdContacts.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdContacts.DisplayLayout.Override.RowAppearance = appearance16;
            this.grdContacts.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdContacts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdContacts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdContacts.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdContacts.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdContacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdContacts.Location = new System.Drawing.Point(0,0);
            this.grdContacts.Name = "grdContacts";
            this.grdContacts.Size = new System.Drawing.Size(666,252);
            this.grdContacts.TabIndex = 3;
            this.grdContacts.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdContacts.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            this.grdContacts.DoubleClick += new System.EventHandler(this.OnGridDoubleClick);
            // 
            // ctxDialog
            // 
            this.ctxDialog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxNew,
            this.ctxOpen,
            this.ctxSep1,
            this.ctxRefresh});
            this.ctxDialog.Name = "ctxDialog";
            this.ctxDialog.Size = new System.Drawing.Size(114,76);
            // 
            // ctxNew
            // 
            this.ctxNew.Name = "ctxNew";
            this.ctxNew.Size = new System.Drawing.Size(113,22);
            this.ctxNew.Text = "New...";
            this.ctxNew.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxOpen
            // 
            this.ctxOpen.Name = "ctxOpen";
            this.ctxOpen.Size = new System.Drawing.Size(113,22);
            this.ctxOpen.Text = "Open...";
            this.ctxOpen.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(110,6);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(113,22);
            this.ctxRefresh.Text = "Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // winContacts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(666,252);
            this.Controls.Add(this.grdContacts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "winContacts";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Contacts";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mContactDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdContacts)).EndInit();
            this.ctxDialog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ContactDS mContactDS;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdContacts;
        private System.Windows.Forms.ContextMenuStrip ctxDialog;
        private System.Windows.Forms.ToolStripMenuItem ctxNew;
        private System.Windows.Forms.ToolStripMenuItem ctxOpen;
        private System.Windows.Forms.ToolStripSeparator ctxSep1;
        private System.Windows.Forms.ToolStripMenuItem ctxRefresh;
    }
}