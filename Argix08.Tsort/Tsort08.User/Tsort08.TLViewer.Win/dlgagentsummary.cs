//	File:	dlgagentsummary.cs
//	Author:	J. Heary
//	Date:	10/28/08
//	Desc:	Modal dialog for agent summary views.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Argix.Windows;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Freight {
	//
	public class dlgAgentSummary : System.Windows.Forms.Form {
		//Members
        private int mTerminalID=0;
        private string mTerminalName="";
		private UltraGridSvc mGridSvc=null;
		
        #region Controls

        private Infragistics.Win.UltraWinGrid.UltraGrid grdAgentSummary;
		private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdClose;
        private BindingSource mSummary;
        private IContainer components;
        #endregion

        public dlgAgentSummary(int terminalID, string terminalName) {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
                this.mTerminalID = terminalID;
                this.mTerminalName = terminalName;
				this.mGridSvc = new UltraGridSvc(this.grdAgentSummary);
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TL",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubePercent");
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WeightPercent");
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAgentSummary));
            this.grdAgentSummary = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSummary = new System.Windows.Forms.BindingSource(this.components);
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdAgentSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSummary)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAgentSummary
            // 
            this.grdAgentSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAgentSummary.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdAgentSummary.DataSource = this.mSummary;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "ClientViewTable";
            ultraGridColumn1.Header.Caption = "Name";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 192;
            ultraGridColumn2.Header.Caption = "Number";
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Width = 72;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn3.CellAppearance = appearance2;
            ultraGridColumn3.Format = "#,0";
            ultraGridColumn3.Header.VisiblePosition = 6;
            ultraGridColumn3.Width = 60;
            ultraGridColumn4.Header.VisiblePosition = 11;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Header.VisiblePosition = 12;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn6.Header.Caption = "Close#";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 51;
            ultraGridColumn7.Header.VisiblePosition = 13;
            ultraGridColumn7.Hidden = true;
            appearance12.TextHAlignAsString = "Right";
            ultraGridColumn8.CellAppearance = appearance12;
            ultraGridColumn8.Format = "#0";
            ultraGridColumn8.Header.Caption = "Cube%";
            ultraGridColumn8.Header.VisiblePosition = 10;
            ultraGridColumn8.Width = 60;
            ultraGridColumn9.Header.VisiblePosition = 14;
            ultraGridColumn9.Hidden = true;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn10.CellAppearance = appearance3;
            ultraGridColumn10.Format = "#,0";
            ultraGridColumn10.Header.VisiblePosition = 7;
            ultraGridColumn10.Width = 60;
            ultraGridColumn11.Header.VisiblePosition = 15;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 16;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 17;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Format = "MM/dd/yyyy";
            ultraGridColumn14.Header.Caption = "TL Date";
            ultraGridColumn14.Header.VisiblePosition = 4;
            ultraGridColumn14.Width = 75;
            ultraGridColumn15.Header.Caption = "TL#";
            ultraGridColumn15.Header.VisiblePosition = 3;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.Header.VisiblePosition = 18;
            ultraGridColumn16.Hidden = true;
            appearance4.TextHAlignAsString = "Right";
            ultraGridColumn17.CellAppearance = appearance4;
            ultraGridColumn17.Format = "#,0";
            ultraGridColumn17.Header.VisiblePosition = 8;
            ultraGridColumn17.Width = 60;
            appearance13.TextHAlignAsString = "Right";
            ultraGridColumn18.CellAppearance = appearance13;
            ultraGridColumn18.Format = "#0";
            ultraGridColumn18.Header.Caption = "Weight%";
            ultraGridColumn18.Header.VisiblePosition = 9;
            ultraGridColumn18.Width = 60;
            ultraGridColumn19.Header.Caption = "Main Zone";
            ultraGridColumn19.Header.VisiblePosition = 2;
            ultraGridColumn19.Width = 72;
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
            ultraGridColumn19});
            appearance5.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance5.FontData.Name = "Arial";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            ultraGridBand1.Override.ActiveRowAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Control;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Arial";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
            appearance6.TextHAlignAsString = "Left";
            ultraGridBand1.Override.HeaderAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            appearance7.FontData.Name = "Arial";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.ForeColor = System.Drawing.SystemColors.WindowText;
            ultraGridBand1.Override.RowAlternateAppearance = appearance7;
            appearance8.BackColor = System.Drawing.SystemColors.Window;
            appearance8.FontData.Name = "Arial";
            appearance8.FontData.SizeInPoints = 8F;
            appearance8.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance8.TextHAlignAsString = "Left";
            ultraGridBand1.Override.RowAppearance = appearance8;
            this.grdAgentSummary.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdAgentSummary.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance9.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance9.FontData.BoldAsString = "True";
            appearance9.FontData.Name = "Verdana";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance9.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.CaptionAppearance = appearance9;
            this.grdAgentSummary.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAgentSummary.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Verdana";
            appearance10.FontData.SizeInPoints = 8F;
            appearance10.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.grdAgentSummary.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAgentSummary.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance11.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdAgentSummary.DisplayLayout.Override.RowAppearance = appearance11;
            this.grdAgentSummary.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAgentSummary.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAgentSummary.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAgentSummary.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAgentSummary.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAgentSummary.Location = new System.Drawing.Point(0,0);
            this.grdAgentSummary.Name = "grdAgentSummary";
            this.grdAgentSummary.Size = new System.Drawing.Size(760,305);
            this.grdAgentSummary.TabIndex = 1;
            this.grdAgentSummary.Text = "Agent Summary View for ";
            this.grdAgentSummary.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mSummary
            // 
            this.mSummary.DataSource = typeof(Argix.Freight.TLs);
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Location = new System.Drawing.Point(550,314);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(96,24);
            this.cmdPrint.TabIndex = 2;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(652,314);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96,24);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // dlgAgentSummary
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(760,350);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.grdAgentSummary);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAgentSummary";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TLViewer";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdAgentSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSummary)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Form load event handler
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				
				//Load agent summarymEntTerminal
                this.grdAgentSummary.Text = "Agent Summary for " + this.mTerminalName;
				this.grdAgentSummary.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdAgentSummary.DisplayLayout.Bands[0].Columns["AgentNumber"].SortIndicator = SortIndicator.Ascending;
                this.mSummary.DataSource = TLViewerProxy.GetAgentSummary(this.mTerminalID);
				if(this.grdAgentSummary.Rows.Count > 0) 
					this.grdAgentSummary.Rows[0].Selected = this.grdAgentSummary.Rows[0].Activate();
				this.cmdPrint.Enabled = (this.grdAgentSummary.Rows.Count > 0);
			} 
			catch(Exception ex) { throw ex; }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Name) {
					case "cmdClose":
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case "cmdPrint":
                        UltraGridPrinter.Print(this.grdAgentSummary,"Agent Summary View for " + this.mTerminalName + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),true);
						break;
				}
			} 
			catch(Exception) { }
		}
	}
}
