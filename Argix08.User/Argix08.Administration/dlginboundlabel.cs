//	File:	dlginboundlabel.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new or edit an existing Inbound Label.
//	Rev:	               
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Tsort.Enterprise;
using Tsort.Freight;

namespace Tsort {
	//
	public class dlgInboundLabel : System.Windows.Forms.Form {
		//Members
		private int mLabelID=0;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		private Tsort.Freight.InboundLabelDS mInboundLabelDS;
		private System.Windows.Forms.ComboBox cboFreightSortType;
		private System.Windows.Forms.Label lblFreightSortType;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDescription;
		private Tsort.Windows.SelectionList mSortTypesDS;
		private System.Windows.Forms.TextBox txtVal1;
		private System.Windows.Forms.Label lblVal1;
		private System.Windows.Forms.GroupBox fraDataElements;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdDataElements;
		private Tsort.Freight.InboundLabelDS mDataElementsDS;
		private System.Windows.Forms.Label lblLen1;
		private System.Windows.Forms.Label lblStart1;
		private System.Windows.Forms.NumericUpDown updLen1;
		private System.Windows.Forms.NumericUpDown updStart1;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.GroupBox fraSource;
		private System.Windows.Forms.CheckBox chkSource1;
		private System.Windows.Forms.CheckBox chkSource2;
		private System.Windows.Forms.CheckBox chkSource3;
		private System.Windows.Forms.NumericUpDown updStart3;
		private System.Windows.Forms.NumericUpDown updLen3;
		private System.Windows.Forms.Label lblVal3;
		private System.Windows.Forms.Label lblStart3;
		private System.Windows.Forms.Label lblLen3;
		private System.Windows.Forms.TextBox txtVal3;
		private System.Windows.Forms.NumericUpDown updStart2;
		private System.Windows.Forms.NumericUpDown updLen2;
		private System.Windows.Forms.Label lblVal2;
		private System.Windows.Forms.Label lblStart2;
		private System.Windows.Forms.Label lblLen2;
		private System.Windows.Forms.TextBox txtVal2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgInboundLabel(ref InboundLabelDS label) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mInboundLabelDS = label;
				if(this.mInboundLabelDS.InboundLabelDetailTable.Count>0) {
					this.mLabelID = this.mInboundLabelDS.InboundLabelDetailTable[0].LabelID;
					this.Text = (this.mLabelID>0) ? "Inbound Label (" + this.mLabelID + ")" : "Inbound Label (New)";
				}
				else
					this.Text = "Inbound Label (Data Unavailable)";
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("InboundLabelDataElementTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Required");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LabelID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ElementType");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InputNumber");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Start");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Length");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsValueRequired");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsDuplicateAllowed");
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsCheckDigitValidation");
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsUseAltNumber");
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgInboundLabel));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.fraDataElements = new System.Windows.Forms.GroupBox();
			this.grdDataElements = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.mDataElementsDS = new Tsort.Freight.InboundLabelDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.fraSource = new System.Windows.Forms.GroupBox();
			this.chkSource3 = new System.Windows.Forms.CheckBox();
			this.chkSource2 = new System.Windows.Forms.CheckBox();
			this.chkSource1 = new System.Windows.Forms.CheckBox();
			this.updStart3 = new System.Windows.Forms.NumericUpDown();
			this.updLen3 = new System.Windows.Forms.NumericUpDown();
			this.lblVal3 = new System.Windows.Forms.Label();
			this.lblStart3 = new System.Windows.Forms.Label();
			this.lblLen3 = new System.Windows.Forms.Label();
			this.txtVal3 = new System.Windows.Forms.TextBox();
			this.updStart2 = new System.Windows.Forms.NumericUpDown();
			this.updLen2 = new System.Windows.Forms.NumericUpDown();
			this.lblVal2 = new System.Windows.Forms.Label();
			this.lblStart2 = new System.Windows.Forms.Label();
			this.lblLen2 = new System.Windows.Forms.Label();
			this.txtVal2 = new System.Windows.Forms.TextBox();
			this.updStart1 = new System.Windows.Forms.NumericUpDown();
			this.updLen1 = new System.Windows.Forms.NumericUpDown();
			this.lblVal1 = new System.Windows.Forms.Label();
			this.lblStart1 = new System.Windows.Forms.Label();
			this.lblLen1 = new System.Windows.Forms.Label();
			this.txtVal1 = new System.Windows.Forms.TextBox();
			this.lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.lblFreightSortType = new System.Windows.Forms.Label();
			this.cboFreightSortType = new System.Windows.Forms.ComboBox();
			this.mSortTypesDS = new Tsort.Windows.SelectionList();
			this.mInboundLabelDS = new Tsort.Freight.InboundLabelDS();
			this.fraDataElements.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdDataElements)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mDataElementsDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraSource.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.updStart3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updLen3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updStart2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updLen2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updStart1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.updLen1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mSortTypesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mInboundLabelDS)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(567, 381);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(465, 381);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraDataElements
			// 
			this.fraDataElements.Controls.Add(this.grdDataElements);
			this.fraDataElements.Location = new System.Drawing.Point(6, 192);
			this.fraDataElements.Name = "fraDataElements";
			this.fraDataElements.Size = new System.Drawing.Size(636, 144);
			this.fraDataElements.TabIndex = 6;
			this.fraDataElements.TabStop = false;
			this.fraDataElements.Text = "Data Elements";
			// 
			// grdDataElements
			// 
			this.grdDataElements.DataMember = "InboundLabelDataElementTable";
			this.grdDataElements.DataSource = this.mDataElementsDS;
			this.grdDataElements.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdDataElements.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn1.Header.Caption = "Req\'d";
			ultraGridColumn1.Header.VisiblePosition = 1;
			ultraGridColumn1.Width = 45;
			ultraGridColumn2.Header.Caption = "Select";
			ultraGridColumn2.Header.VisiblePosition = 0;
			ultraGridColumn2.Width = 50;
			ultraGridColumn3.Hidden = true;
			ultraGridColumn3.Width = 58;
			ultraGridColumn4.Header.Caption = "Type";
			ultraGridColumn4.Width = 93;
			ultraGridColumn5.Header.Caption = "Input #";
			ultraGridColumn5.Width = 56;
			ultraGridColumn6.Header.Caption = "Start@";
			ultraGridColumn6.Width = 51;
			ultraGridColumn7.Header.Caption = "Len";
			ultraGridColumn7.Width = 44;
			appearance2.TextHAlign = Infragistics.Win.HAlign.Center;
			ultraGridColumn8.Header.Appearance = appearance2;
			ultraGridColumn8.Header.Caption = "Val Req\'d";
			ultraGridColumn8.Width = 72;
			appearance3.TextHAlign = Infragistics.Win.HAlign.Center;
			ultraGridColumn9.Header.Appearance = appearance3;
			ultraGridColumn9.Header.Caption = "Duplicates";
			ultraGridColumn9.Width = 73;
			appearance4.TextHAlign = Infragistics.Win.HAlign.Center;
			ultraGridColumn10.Header.Appearance = appearance4;
			ultraGridColumn10.Header.Caption = "CheckDigit";
			ultraGridColumn10.Width = 76;
			appearance5.TextHAlign = Infragistics.Win.HAlign.Center;
			ultraGridColumn11.Header.Appearance = appearance5;
			ultraGridColumn11.Header.Caption = "Alt #";
			ultraGridColumn11.Width = 61;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Columns.Add(ultraGridColumn4);
			ultraGridBand1.Columns.Add(ultraGridColumn5);
			ultraGridBand1.Columns.Add(ultraGridColumn6);
			ultraGridBand1.Columns.Add(ultraGridColumn7);
			ultraGridBand1.Columns.Add(ultraGridColumn8);
			ultraGridBand1.Columns.Add(ultraGridColumn9);
			ultraGridBand1.Columns.Add(ultraGridColumn10);
			ultraGridBand1.Columns.Add(ultraGridColumn11);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.ActiveRowAppearance = appearance6;
			appearance7.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance7.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance7.FontData.Name = "Verdana";
			appearance7.FontData.SizeInPoints = 8F;
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance7.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.HeaderAppearance = appearance7;
			appearance8.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance8.FontData.Name = "Verdana";
			appearance8.FontData.SizeInPoints = 8F;
			appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.RowAlternateAppearance = appearance8;
			appearance9.BackColor = System.Drawing.Color.White;
			appearance9.FontData.Name = "Verdana";
			appearance9.FontData.SizeInPoints = 8F;
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance9.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.RowAppearance = appearance9;
			this.grdDataElements.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance10.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance10.FontData.Name = "Verdana";
			appearance10.FontData.SizeInPoints = 8F;
			appearance10.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance10.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdDataElements.DisplayLayout.CaptionAppearance = appearance10;
			this.grdDataElements.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdDataElements.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdDataElements.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdDataElements.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdDataElements.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdDataElements.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdDataElements.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdDataElements.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdDataElements.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdDataElements.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdDataElements.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdDataElements.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdDataElements.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdDataElements.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdDataElements.Location = new System.Drawing.Point(6, 18);
			this.grdDataElements.Name = "grdDataElements";
			this.grdDataElements.Size = new System.Drawing.Size(624, 120);
			this.grdDataElements.TabIndex = 0;
			this.grdDataElements.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnCellChange;
			this.grdDataElements.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnDataElementRowUpdated);
			this.grdDataElements.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnBeforeDataElementCellActivated);
			this.grdDataElements.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnDataElementCellUpdated);
			// 
			// mDataElementsDS
			// 
			this.mDataElementsDS.DataSetName = "InboundLabelDS";
			this.mDataElementsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(660, 369);
			this.tabDialog.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Controls.Add(this.fraSource);
			this.tabGeneral.Controls.Add(this.lblDescription);
			this.tabGeneral.Controls.Add(this.txtDescription);
			this.tabGeneral.Controls.Add(this.lblFreightSortType);
			this.tabGeneral.Controls.Add(this.cboFreightSortType);
			this.tabGeneral.Controls.Add(this.fraDataElements);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(652, 343);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(132, 66);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 4;
			this.chkStatus.Text = "Active";
			this.chkStatus.Click += new System.EventHandler(this.ValidateForm);
			// 
			// fraSource
			// 
			this.fraSource.Controls.Add(this.chkSource3);
			this.fraSource.Controls.Add(this.chkSource2);
			this.fraSource.Controls.Add(this.chkSource1);
			this.fraSource.Controls.Add(this.updStart3);
			this.fraSource.Controls.Add(this.updLen3);
			this.fraSource.Controls.Add(this.lblVal3);
			this.fraSource.Controls.Add(this.lblStart3);
			this.fraSource.Controls.Add(this.lblLen3);
			this.fraSource.Controls.Add(this.txtVal3);
			this.fraSource.Controls.Add(this.updStart2);
			this.fraSource.Controls.Add(this.updLen2);
			this.fraSource.Controls.Add(this.lblVal2);
			this.fraSource.Controls.Add(this.lblStart2);
			this.fraSource.Controls.Add(this.lblLen2);
			this.fraSource.Controls.Add(this.txtVal2);
			this.fraSource.Controls.Add(this.updStart1);
			this.fraSource.Controls.Add(this.updLen1);
			this.fraSource.Controls.Add(this.lblVal1);
			this.fraSource.Controls.Add(this.lblStart1);
			this.fraSource.Controls.Add(this.lblLen1);
			this.fraSource.Controls.Add(this.txtVal1);
			this.fraSource.Location = new System.Drawing.Point(6, 84);
			this.fraSource.Name = "fraSource";
			this.fraSource.Size = new System.Drawing.Size(636, 102);
			this.fraSource.TabIndex = 5;
			this.fraSource.TabStop = false;
			this.fraSource.Text = "Input Sources";
			// 
			// chkSource3
			// 
			this.chkSource3.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkSource3.Location = new System.Drawing.Point(6, 72);
			this.chkSource3.Name = "chkSource3";
			this.chkSource3.Size = new System.Drawing.Size(32, 16);
			this.chkSource3.TabIndex = 14;
			this.chkSource3.Text = "3";
			this.chkSource3.CheckStateChanged += new System.EventHandler(this.OnInputSourceChecked);
			this.chkSource3.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// chkSource2
			// 
			this.chkSource2.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkSource2.Location = new System.Drawing.Point(6, 48);
			this.chkSource2.Name = "chkSource2";
			this.chkSource2.Size = new System.Drawing.Size(32, 16);
			this.chkSource2.TabIndex = 7;
			this.chkSource2.Text = "2";
			this.chkSource2.CheckStateChanged += new System.EventHandler(this.OnInputSourceChecked);
			this.chkSource2.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// chkSource1
			// 
			this.chkSource1.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.chkSource1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkSource1.Location = new System.Drawing.Point(6, 24);
			this.chkSource1.Name = "chkSource1";
			this.chkSource1.Size = new System.Drawing.Size(32, 16);
			this.chkSource1.TabIndex = 0;
			this.chkSource1.Text = "1";
			this.chkSource1.CheckStateChanged += new System.EventHandler(this.OnInputSourceChecked);
			this.chkSource1.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// updStart3
			// 
			this.updStart3.Location = new System.Drawing.Point(582, 72);
			this.updStart3.Maximum = new System.Decimal(new int[] {
																	  99,
																	  0,
																	  0,
																	  0});
			this.updStart3.Minimum = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			this.updStart3.Name = "updStart3";
			this.updStart3.Size = new System.Drawing.Size(48, 21);
			this.updStart3.TabIndex = 20;
			this.updStart3.Value = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.updStart3.ValueChanged += new System.EventHandler(this.OnValidationStringStartPositionChanged);
			this.updStart3.Leave += new System.EventHandler(this.OnValidationStringStartPositionChanged);
			// 
			// updLen3
			// 
			this.updLen3.Location = new System.Drawing.Point(96, 72);
			this.updLen3.Maximum = new System.Decimal(new int[] {
																	99,
																	0,
																	0,
																	0});
			this.updLen3.Minimum = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.updLen3.Name = "updLen3";
			this.updLen3.Size = new System.Drawing.Size(48, 21);
			this.updLen3.TabIndex = 16;
			this.updLen3.Value = new System.Decimal(new int[] {
																  1,
																  0,
																  0,
																  0});
			this.updLen3.ValueChanged += new System.EventHandler(this.OnInputSourceLengthChanged);
			this.updLen3.Leave += new System.EventHandler(this.OnInputSourceLengthChanged);
			// 
			// lblVal3
			// 
			this.lblVal3.Location = new System.Drawing.Point(150, 72);
			this.lblVal3.Name = "lblVal3";
			this.lblVal3.Size = new System.Drawing.Size(72, 16);
			this.lblVal3.TabIndex = 17;
			this.lblVal3.Text = "Validation";
			this.lblVal3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblStart3
			// 
			this.lblStart3.Location = new System.Drawing.Point(531, 72);
			this.lblStart3.Name = "lblStart3";
			this.lblStart3.Size = new System.Drawing.Size(48, 16);
			this.lblStart3.TabIndex = 19;
			this.lblStart3.Text = "Start@";
			this.lblStart3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLen3
			// 
			this.lblLen3.Location = new System.Drawing.Point(45, 72);
			this.lblLen3.Name = "lblLen3";
			this.lblLen3.Size = new System.Drawing.Size(48, 16);
			this.lblLen3.TabIndex = 15;
			this.lblLen3.Text = "Length";
			this.lblLen3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtVal3
			// 
			this.txtVal3.Location = new System.Drawing.Point(225, 72);
			this.txtVal3.Multiline = true;
			this.txtVal3.Name = "txtVal3";
			this.txtVal3.Size = new System.Drawing.Size(304, 21);
			this.txtVal3.TabIndex = 18;
			this.txtVal3.Text = "";
			this.txtVal3.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// updStart2
			// 
			this.updStart2.Location = new System.Drawing.Point(582, 48);
			this.updStart2.Maximum = new System.Decimal(new int[] {
																	  99,
																	  0,
																	  0,
																	  0});
			this.updStart2.Minimum = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			this.updStart2.Name = "updStart2";
			this.updStart2.Size = new System.Drawing.Size(48, 21);
			this.updStart2.TabIndex = 13;
			this.updStart2.Value = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.updStart2.ValueChanged += new System.EventHandler(this.OnValidationStringStartPositionChanged);
			this.updStart2.Leave += new System.EventHandler(this.OnValidationStringStartPositionChanged);
			// 
			// updLen2
			// 
			this.updLen2.Location = new System.Drawing.Point(96, 48);
			this.updLen2.Maximum = new System.Decimal(new int[] {
																	99,
																	0,
																	0,
																	0});
			this.updLen2.Minimum = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.updLen2.Name = "updLen2";
			this.updLen2.Size = new System.Drawing.Size(48, 21);
			this.updLen2.TabIndex = 9;
			this.updLen2.Value = new System.Decimal(new int[] {
																  1,
																  0,
																  0,
																  0});
			this.updLen2.ValueChanged += new System.EventHandler(this.OnInputSourceLengthChanged);
			this.updLen2.Leave += new System.EventHandler(this.OnInputSourceLengthChanged);
			// 
			// lblVal2
			// 
			this.lblVal2.Location = new System.Drawing.Point(150, 48);
			this.lblVal2.Name = "lblVal2";
			this.lblVal2.Size = new System.Drawing.Size(72, 16);
			this.lblVal2.TabIndex = 10;
			this.lblVal2.Text = "Validation";
			this.lblVal2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblStart2
			// 
			this.lblStart2.Location = new System.Drawing.Point(531, 48);
			this.lblStart2.Name = "lblStart2";
			this.lblStart2.Size = new System.Drawing.Size(48, 16);
			this.lblStart2.TabIndex = 12;
			this.lblStart2.Text = "Start@";
			this.lblStart2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLen2
			// 
			this.lblLen2.Location = new System.Drawing.Point(45, 48);
			this.lblLen2.Name = "lblLen2";
			this.lblLen2.Size = new System.Drawing.Size(48, 16);
			this.lblLen2.TabIndex = 8;
			this.lblLen2.Text = "Length";
			this.lblLen2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtVal2
			// 
			this.txtVal2.Location = new System.Drawing.Point(225, 48);
			this.txtVal2.Multiline = true;
			this.txtVal2.Name = "txtVal2";
			this.txtVal2.Size = new System.Drawing.Size(304, 21);
			this.txtVal2.TabIndex = 11;
			this.txtVal2.Text = "";
			this.txtVal2.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// updStart1
			// 
			this.updStart1.Location = new System.Drawing.Point(582, 24);
			this.updStart1.Maximum = new System.Decimal(new int[] {
																	  99,
																	  0,
																	  0,
																	  0});
			this.updStart1.Minimum = new System.Decimal(new int[] {
																	  1,
																	  0,
																	  0,
																	  0});
			this.updStart1.Name = "updStart1";
			this.updStart1.Size = new System.Drawing.Size(48, 21);
			this.updStart1.TabIndex = 6;
			this.updStart1.Value = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.updStart1.ValueChanged += new System.EventHandler(this.OnValidationStringStartPositionChanged);
			this.updStart1.Leave += new System.EventHandler(this.OnValidationStringStartPositionChanged);
			// 
			// updLen1
			// 
			this.updLen1.Location = new System.Drawing.Point(96, 24);
			this.updLen1.Maximum = new System.Decimal(new int[] {
																	99,
																	0,
																	0,
																	0});
			this.updLen1.Minimum = new System.Decimal(new int[] {
																	1,
																	0,
																	0,
																	0});
			this.updLen1.Name = "updLen1";
			this.updLen1.Size = new System.Drawing.Size(48, 21);
			this.updLen1.TabIndex = 2;
			this.updLen1.Value = new System.Decimal(new int[] {
																  1,
																  0,
																  0,
																  0});
			this.updLen1.ValueChanged += new System.EventHandler(this.OnInputSourceLengthChanged);
			this.updLen1.Leave += new System.EventHandler(this.OnInputSourceLengthChanged);
			// 
			// lblVal1
			// 
			this.lblVal1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVal1.Location = new System.Drawing.Point(150, 24);
			this.lblVal1.Name = "lblVal1";
			this.lblVal1.Size = new System.Drawing.Size(72, 16);
			this.lblVal1.TabIndex = 3;
			this.lblVal1.Text = "Validation";
			this.lblVal1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblStart1
			// 
			this.lblStart1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStart1.Location = new System.Drawing.Point(531, 24);
			this.lblStart1.Name = "lblStart1";
			this.lblStart1.Size = new System.Drawing.Size(48, 16);
			this.lblStart1.TabIndex = 5;
			this.lblStart1.Text = "Start@";
			this.lblStart1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLen1
			// 
			this.lblLen1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLen1.Location = new System.Drawing.Point(45, 24);
			this.lblLen1.Name = "lblLen1";
			this.lblLen1.Size = new System.Drawing.Size(48, 16);
			this.lblLen1.TabIndex = 1;
			this.lblLen1.Text = "Length";
			this.lblLen1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtVal1
			// 
			this.txtVal1.Location = new System.Drawing.Point(225, 24);
			this.txtVal1.Multiline = true;
			this.txtVal1.Name = "txtVal1";
			this.txtVal1.Size = new System.Drawing.Size(304, 21);
			this.txtVal1.TabIndex = 4;
			this.txtVal1.Text = "";
			this.txtVal1.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// lblDescription
			// 
			this.lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblDescription.Location = new System.Drawing.Point(6, 36);
			this.lblDescription.Name = "lblDescription";
			this.lblDescription.Size = new System.Drawing.Size(120, 16);
			this.lblDescription.TabIndex = 2;
			this.lblDescription.Text = "Description";
			this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(132, 36);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(510, 21);
			this.txtDescription.TabIndex = 3;
			this.txtDescription.Text = "";
			this.txtDescription.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// lblFreightSortType
			// 
			this.lblFreightSortType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFreightSortType.Location = new System.Drawing.Point(6, 12);
			this.lblFreightSortType.Name = "lblFreightSortType";
			this.lblFreightSortType.Size = new System.Drawing.Size(120, 16);
			this.lblFreightSortType.TabIndex = 0;
			this.lblFreightSortType.Text = "Freight Sort Type";
			this.lblFreightSortType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboFreightSortType
			// 
			this.cboFreightSortType.DataSource = this.mSortTypesDS;
			this.cboFreightSortType.DisplayMember = "SelectionListTable.Description";
			this.cboFreightSortType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFreightSortType.Location = new System.Drawing.Point(132, 12);
			this.cboFreightSortType.Name = "cboFreightSortType";
			this.cboFreightSortType.Size = new System.Drawing.Size(144, 21);
			this.cboFreightSortType.TabIndex = 1;
			this.cboFreightSortType.ValueMember = "SelectionListTable.ID";
			this.cboFreightSortType.SelectedValueChanged += new System.EventHandler(this.ValidateForm);
			this.cboFreightSortType.SelectionChangeCommitted += new System.EventHandler(this.OnFreightSortTypeChanged);
			// 
			// mSortTypesDS
			// 
			this.mSortTypesDS.DataSetName = "SelectionList";
			this.mSortTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mInboundLabelDS
			// 
			this.mInboundLabelDS.DataSetName = "InboundLabelDS";
			this.mInboundLabelDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgInboundLabel
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(666, 409);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgInboundLabel";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Inbound Label Format";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraDataElements.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdDataElements)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mDataElementsDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraSource.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.updStart3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updLen3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updStart2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updLen2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updStart1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.updLen1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mSortTypesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mInboundLabelDS)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial submission states
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mSortTypesDS.Merge(EnterpriseFactory.GetFreightSortTypes());
				
				//Set control services
				#region Default grid behavior
				this.grdDataElements.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdDataElements.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdDataElements.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdDataElements.DisplayLayout.Bands[0].Columns["Required"].CellActivation = Activation.Disabled;
				this.grdDataElements.DisplayLayout.Bands[0].Columns["Selected"].CellActivation = Activation.AllowEdit;
				this.grdDataElements.DisplayLayout.Bands[0].Columns["LabelID"].CellActivation = Activation.Disabled;
				this.grdDataElements.DisplayLayout.Bands[0].Columns["ElementType"].CellActivation = Activation.Disabled;
				#endregion
				this.txtDescription.MaxLength = 30;
				this.txtDescription.Text = this.mInboundLabelDS.InboundLabelDetailTable[0].Description.Trim();
				this.updLen1.Minimum = this.updStart1.Minimum = 0;
				this.updLen1.Maximum = 99;
				this.updLen2.Minimum = this.updStart2.Minimum = 0;
				this.updLen2.Maximum = 99;
				this.updLen3.Minimum = this.updStart3.Minimum = 0;
				this.updLen3.Maximum = 99;
				this.chkSource1.Checked = false;
				this.chkSource1.Checked = true;
				this.chkSource1.Enabled = false;
				this.chkSource2.Checked = (this.mInboundLabelDS.InboundLabelDetailTable[0].Input2Len==0);
				this.chkSource2.Checked = (this.mInboundLabelDS.InboundLabelDetailTable[0].Input2Len>0);
				this.chkSource2.Enabled = true;
				this.chkSource3.Checked = (this.mInboundLabelDS.InboundLabelDetailTable[0].Input3Len==0);
				this.chkSource3.Checked = (this.mInboundLabelDS.InboundLabelDetailTable[0].Input3Len>0);
				//this.chkSource3.Enabled = false;
				this.chkStatus.Checked = this.mInboundLabelDS.InboundLabelDetailTable[0].IsActive;
				
				//Set freight sort type after input sources since data elements are dependent upon freight 
				//sort type AND input source
				if(this.mInboundLabelDS.InboundLabelDetailTable[0].SortTypeID!=0) 
					this.cboFreightSortType.SelectedValue = this.mInboundLabelDS.InboundLabelDetailTable[0].SortTypeID;
				else 
					if(this.cboFreightSortType.Items.Count>0) this.cboFreightSortType.SelectedIndex = 0;
				this.cboFreightSortType.Enabled = (this.cboFreightSortType.Items.Count>0);
				OnFreightSortTypeChanged(null, null);
				this.cboFreightSortType.Enabled = (this.mInboundLabelDS.InboundLabelDetailTable[0].LabelID==0);
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnFreightSortTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change to freight sort type
			try {
				//Update data element selections
				int freightSortTypeID = Convert.ToInt16(this.cboFreightSortType.SelectedValue);
				this.mDataElementsDS.Merge(FreightFactory.GetDataElementsTemplate(freightSortTypeID));
				if(this.mLabelID > 0) {
					//Copy data from each exisiting data element into its corresponding row in the template
					for(int i=0; i<this.mInboundLabelDS.InboundLabelDataElementTable.Rows.Count; i++) {
						//Find the row by element type (unique for each label) and copy the data
						string elemType = this.mInboundLabelDS.InboundLabelDataElementTable[i].ElementType.ToUpper().Trim();
						InboundLabelDS.InboundLabelDataElementTableRow row = (InboundLabelDS.InboundLabelDataElementTableRow)this.mDataElementsDS.InboundLabelDataElementTable.Select("ElementType='" + elemType + "'")[0];
						if(row!=null) {
							row.Selected = true;
							switch(freightSortTypeID) {
								case 1:		//"san - tsort"
								switch(elemType) {
									case "SAN":		row.Required = true; break;
									default:		row.Required = false; break;
								}
									break;
								case 2:		//"regular - tsort":
								switch(elemType) {
									case "STORE":	row.Required = true; break;
									default:		row.Required = false; break;
								}
									break;
								case 3:		//"jit - tsort":
								switch(elemType) {
									case "CARTON":	row.Required = true; break;
									default:		row.Required = false; break;
								}
									break;
								case 4:		//"jit-san - tsort":
								switch(elemType) {
									case "CARTON":	row.Required = true; break;
									case "SAN":		row.Required = true; break;
									default:		row.Required = false; break;
								}
									break;
								case 5:		//"sku - tsort":
								switch(elemType) {
									case "SKU":		row.Required = true; break;
									default:		row.Required = false; break;
								}
									break;
								case 6:		//"returns - returns":
								switch(elemType) {
									case "SKU":		row.Required = true; break;
									default:		row.Required = false; break;
								}
									break;
							}
							row.LabelID = this.mInboundLabelDS.InboundLabelDataElementTable[i].LabelID;
							row.ElementType = this.mInboundLabelDS.InboundLabelDataElementTable[i].ElementType;
							row.Length = this.mInboundLabelDS.InboundLabelDataElementTable[i].Length;
							row.Start = this.mInboundLabelDS.InboundLabelDataElementTable[i].Start;
							row.IsCheckDigitValidation = this.mInboundLabelDS.InboundLabelDataElementTable[i].IsCheckDigitValidation;
							row.IsDuplicateAllowed = this.mInboundLabelDS.InboundLabelDataElementTable[i].IsDuplicateAllowed;
							row.IsUseAltNumber = this.mInboundLabelDS.InboundLabelDataElementTable[i].IsUseAltNumber;
							row.IsValueRequired = this.mInboundLabelDS.InboundLabelDataElementTable[i].IsValueRequired;
							row.InputNumber = this.mInboundLabelDS.InboundLabelDataElementTable[i].InputNumber;
						}
					}
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region Input Source: OnInputSourceChecked(), OnInputSourceLengthChanged(), OnValidationStringStartPositionChanged()
		private void OnInputSourceChecked(object sender, System.EventArgs e) {
			//Event handler for change to an input source as selected or not
			//Debug.Write("OnInputSourceChecked()\n");
			int iLen=0, iStart=0;
			UltraGridCell cellInputSrc = null;
			string sInputSrc = "";
			bool bInputSrcEnabled = false;
			
			try {
				//1.	Enable length, validation string, and start position
				//2.	Set input source values (assume valid values in datarow BUT
				//		as each value is set event handlers should fire and validate the values
				//3.	Validate data element source numbers
				CheckBox chk = (CheckBox)sender;
				switch(chk.Name) {
					case "chkSource1":
						bInputSrcEnabled = this.chkSource1.Checked;
						this.lblLen1.Enabled = this.updLen1.Enabled = this.lblVal1.Enabled = this.txtVal1.Enabled = this.lblStart1.Enabled = this.updStart1.Enabled = bInputSrcEnabled;
						iLen = (int)this.mInboundLabelDS.InboundLabelDetailTable[0].Input1Len;
						iStart = (int)this.mInboundLabelDS.InboundLabelDetailTable[0].Input1ValidStart;
						if(bInputSrcEnabled) {
							if(iLen<1) iLen = 1;
							if(iStart<1) iStart = 1;
							this.txtVal1.Text = this.mInboundLabelDS.InboundLabelDetailTable[0].Input1ValidString;
							this.updLen1.Value = iLen;
							this.updStart1.Value = iStart;
							this.updLen1.Minimum = this.updStart1.Minimum = 1;
						}
						else {
							this.updLen1.Minimum = this.updStart1.Minimum = 0;
							this.txtVal1.Text = "";
							this.updLen1.Value = this.updStart1.Value = 0;
						}
						OnInputSourceLengthChanged(this.updLen1, null);
						break;
					case "chkSource2":
						bInputSrcEnabled = this.chkSource2.Checked;
						this.lblLen2.Enabled = this.updLen2.Enabled = this.lblVal2.Enabled = this.txtVal2.Enabled = this.lblStart2.Enabled = this.updStart2.Enabled = bInputSrcEnabled;
						iLen = (int)this.mInboundLabelDS.InboundLabelDetailTable[0].Input2Len;
						iStart = (int)this.mInboundLabelDS.InboundLabelDetailTable[0].Input2ValidStart;
						if(bInputSrcEnabled) {
							if(iLen<1) iLen = 1;
							if(iStart<1) iStart = 1;
							this.txtVal2.Text = this.mInboundLabelDS.InboundLabelDetailTable[0].Input2ValidString;
							this.updLen2.Value = iLen;
							this.updStart2.Value = iStart;
							this.updLen2.Minimum = this.updStart2.Minimum = 1;
						}
						else {
							this.updLen2.Minimum = this.updStart2.Minimum = 0;
							this.txtVal2.Text = "";
							this.updLen2.Value = this.updStart2.Value = 0;
						}
						this.chkSource3.Checked = false;
						this.chkSource3.Enabled = bInputSrcEnabled;
						OnInputSourceLengthChanged(this.updLen2, null);
						break;
					case "chkSource3":
						bInputSrcEnabled = this.chkSource3.Checked;
						this.lblLen3.Enabled = this.updLen3.Enabled = this.lblVal3.Enabled = this.txtVal3.Enabled = this.lblStart3.Enabled = this.updStart3.Enabled = bInputSrcEnabled;
						iLen = (int)this.mInboundLabelDS.InboundLabelDetailTable[0].Input3Len;
						iStart = (int)this.mInboundLabelDS.InboundLabelDetailTable[0].Input3ValidStart;
						if(bInputSrcEnabled) {
							if(iLen<1) iLen = 1;
							if(iStart<1) iStart = 1;
							this.txtVal3.Text = this.mInboundLabelDS.InboundLabelDetailTable[0].Input3ValidString;
							this.updLen3.Value = iLen;
							this.updStart3.Value = iStart;
							this.updLen3.Minimum = this.updStart3.Minimum = 1;
						}
						else {
							this.updLen3.Minimum = this.updStart3.Minimum = 0;
							this.txtVal3.Text = "";
							this.updLen3.Value = this.updStart3.Value = 0;
						}
						OnInputSourceLengthChanged(this.updLen3, null);
						break;
				}
				
				for(int i=0; i<this.grdDataElements.Rows.Count; i++) {
					//Update source # for data elements if required
					cellInputSrc = this.grdDataElements.Rows[i].Cells["InputNumber"];
					sInputSrc = cellInputSrc.Value.ToString();
					switch(sInputSrc) {
						case "1": break;
						case "2": if(!this.chkSource2.Checked)  cellInputSrc.Value = "1"; break;
						case "3": if(!this.chkSource3.Checked)  cellInputSrc.Value = (this.chkSource2.Checked) ? "2" : "1"; break;
					}
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnInputSourceLengthChanged(object sender, System.EventArgs e) {
			//Event handler for changes to input source length
			//Debug.Write("OnInputSourceLengthChanged()\n");
			bool bSelected=false;
			string sInputSrc="";
			short iSrcLen=0, iElemStart=0, iElemLen=0;
			try {
				//Validate input source start position (validation string is corrected when event 
				//OnValidationStringStartPositionChanged() is fired)
				NumericUpDown upd = (NumericUpDown)sender;
				switch(upd.Name) {
					case "updLen1":
						if(this.updStart1.Value>this.updLen1.Value) 
							this.updStart1.Value = this.updLen1.Value;
						else
							OnValidationStringStartPositionChanged(this.updStart1, null);
						this.updStart1.Maximum = this.updLen1.Value;
						break;
					case "updLen2":
						if(this.updStart2.Value>this.updLen2.Value) 
							this.updStart2.Value = this.updLen2.Value;
						else
							OnValidationStringStartPositionChanged(this.updStart2, null);
						this.updStart2.Maximum = this.updLen2.Value;
						break;
					case "updLen3":
						if(this.updStart3.Value>this.updLen3.Value) 
							this.updStart3.Value = this.updLen3.Value;
						else
							OnValidationStringStartPositionChanged(this.updStart3, null);
						this.updStart3.Maximum = this.updLen3.Value;
						break;
				}

				//Validate each data element length and start position
				for(int i=0; i<this.grdDataElements.Rows.Count; i++) {
					//Update data element length as required
					bSelected = Convert.ToBoolean(this.grdDataElements.Rows[i].Cells["Selected"].Value);
					if(bSelected) {
						sInputSrc = this.grdDataElements.Rows[i].Cells["InputNumber"].Value.ToString();
						switch(sInputSrc) {
							case "1": iSrcLen = (short)this.updLen1.Value; break;
							case "2": iSrcLen = (short)this.updLen2.Value; break;
							case "3": iSrcLen = (short)this.updLen3.Value; break;
						}
						if(iSrcLen>0) {
							//Adjust data element length and start positon according to input src length
							iElemLen = Convert.ToInt16(this.grdDataElements.Rows[i].Cells["Length"].Value);
							iElemStart = Convert.ToInt16(this.grdDataElements.Rows[i].Cells["Start"].Value);
							if(iElemLen>iSrcLen) 
								this.grdDataElements.Rows[i].Cells["Length"].Value = iElemLen = iSrcLen;
							if(iElemStart>(iSrcLen + 1 - iElemLen)) 
								this.grdDataElements.Rows[i].Cells["Start"].Value = (iSrcLen + 1 - iElemLen);
						}
						else {
							//Input src not selected- deselect data elements for this input src
							this.grdDataElements.Rows[i].Cells["Selected"].Value = false;
						}
					}
				}
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnValidationStringStartPositionChanged(object sender, System.EventArgs e) {
			//Event handler for change to validation string start position
			int iMax = 0;
			try {
				//Validate validation string and reset string max length
				NumericUpDown upd = (NumericUpDown)sender;
				switch(upd.Name) {
					case "updStart1":
						iMax = Convert.ToInt32(this.updLen1.Value - this.updStart1.Value + 1);
						if(this.txtVal1.Text.Length>iMax) this.txtVal1.Text = this.txtVal1.Text.Substring(1, iMax);
						this.txtVal1.MaxLength = iMax;
						break;
					case "updStart2":
						iMax = Convert.ToInt32(this.updLen2.Value - this.updStart2.Value + 1);
						if(this.txtVal2.Text.Length>iMax) this.txtVal2.Text = this.txtVal2.Text.Substring(1, iMax);
						this.txtVal2.MaxLength = iMax;
						break;
					case "updStart3":
						iMax = Convert.ToInt32(this.updLen3.Value - this.updStart3.Value + 1);
						if(this.txtVal3.Text.Length>iMax) this.txtVal3.Text = this.txtVal3.Text.Substring(1, iMax);
						this.txtVal3.MaxLength = iMax;
						break;
				}
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region Data Elements Grid: OnBeforeDataElementCellActivated(), OnDataElementCellUpdated(), OnDataElementRowUpdated()
		private void OnBeforeDataElementCellActivated(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
			//Event handler for data element cell activated
			//Debug.Write("OnBeforeDataElementCellActivated()\n");
			bool bRequired = false, bSelected = true;
			try {
				//
				bRequired = Convert.ToBoolean(e.Cell.Row.Cells["Required"].Value);
				bSelected = Convert.ToBoolean(e.Cell.Row.Cells["Selected"].Value);
				switch(e.Cell.Column.Key.ToString()) {
					case "Required":	e.Cell.Activation = Activation.Disabled; break;
					case "Selected":	e.Cell.Activation = bRequired ? Activation.Disabled : Activation.AllowEdit; break;
					case "LabelID":		e.Cell.Activation = Activation.Disabled; break;
					case "ElementType": e.Cell.Activation = Activation.Disabled; break;
					case "InputNumber": e.Cell.Activation = bRequired ? Activation.Disabled : Activation.AllowEdit; break;
					default:			e.Cell.Activation = bSelected ? Activation.AllowEdit : Activation.NoEdit; break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDataElementCellUpdated(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for change in a data element cell value
			//Debug.Write("OnDataElementCellUpdated()\n");
			bool bRequired=false, bSelected=false;
			string sInputSrc="", elemType="";
			short iSrcLen=0, iElemStart=0, iElemLen=0, iMaxLen=0;
			try {
				//Validate input source, start position, and length
				bRequired = Convert.ToBoolean(e.Cell.Row.Cells["Required"].Value);
				bSelected = Convert.ToBoolean(e.Cell.Row.Cells["Selected"].Value);
				sInputSrc = e.Cell.Row.Cells["InputNumber"].Value.ToString();
				switch(sInputSrc) {
					case "1": iSrcLen = (short)this.updLen1.Value; break;
					case "2": iSrcLen = (short)this.updLen2.Value; break;
					case "3": iSrcLen = (short)this.updLen3.Value; break;
				}
				elemType = e.Cell.Row.Cells["ElementType"].Value.ToString().Trim();
				iElemLen = Convert.ToInt16(e.Cell.Row.Cells["Length"].Value);
				iElemStart = Convert.ToInt16(e.Cell.Row.Cells["Start"].Value);
				switch(e.Cell.Column.Key.ToString()) {
					case "InputNumber":
					switch(sInputSrc) {
						case "1":	break;
						case "2":	if(bRequired || !this.chkSource2.Checked) e.Cell.Row.Cells["InputNumber"].Value = "1";	break;
						case "3":	if(bRequired || !this.chkSource3.Checked) e.Cell.Row.Cells["InputNumber"].Value = (this.chkSource2.Checked) ? "2" : "1"; break;
						default:	e.Cell.Row.Cells["InputNumber"].Value = "1";	break;
					}
						break;
					case "Length":
						//13.1-13.7-data element max length
						if(iElemLen<1) 
							e.Cell.Row.Cells["Length"].Value = iElemLen = 1;
					switch(elemType) {
						case "CARTON":	iMaxLen = (iSrcLen>30) ? (short)30 : (short)iSrcLen; break;
						case "RETURN":	iMaxLen = (iSrcLen>30) ? (short)30 : (short)iSrcLen; break;
						case "PO":		iMaxLen = (iSrcLen>30) ? (short)30 : (short)iSrcLen; break;
						case "SKU":		iMaxLen = (iSrcLen>30) ? (short)30 : (short)iSrcLen; break;
						case "STORE":	iMaxLen = (iSrcLen>10) ? (short)10 : (short)iSrcLen; break;
						case "VENDOR":	iMaxLen = (iSrcLen>10) ? (short)10 : (short)iSrcLen; break;
						case "SAN":		iMaxLen = (iSrcLen>16) ? (short)16 : (short)iSrcLen; break;
						default:		iMaxLen = (short)10; break;
					}
						if(iElemLen>iMaxLen) 
							e.Cell.Row.Cells["Length"].Value = iElemLen = iMaxLen;
						if(iElemStart>(iSrcLen + 1 - iElemLen)) 
							e.Cell.Row.Cells["Start"].Value = (iSrcLen + 1 - iElemLen);
						break;
					case "Start":
						//13.11-element length + element start pos <= input source length
						//13.13-element start pos >= 1
						if(iElemStart<1) 
							e.Cell.Row.Cells["Start"].Value = iElemStart = 1;
						if(iElemStart>(iSrcLen + 1 - iElemLen)) 
							e.Cell.Row.Cells["Start"].Value = (iSrcLen + 1 - iElemLen);
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnDataElementRowUpdated(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
			//Event handler for change in a data element row
			try {
				//Validate row
				ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#endregion
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			//Debug.Write("ValidateForm()\n");
			bool bDataElementsValid=true, bSelected=false;
			short iElemStart=0, iElemLen=0;
			string[] sInputSrcs=null;
			string sDataElemInputSrc="";
			bool bDataElemSelected=false, bInputSrcHasDataElem=true;
			try {
				if(this.mInboundLabelDS.InboundLabelDetailTable.Count>0) {
					//Validate data elements
					bDataElementsValid=true;
					for(int i=0; i<this.grdDataElements.Rows.Count; i++) {
						//Update source # if required
						bSelected = Convert.ToBoolean(this.grdDataElements.Rows[i].Cells["Selected"].Value);
						iElemStart = Convert.ToInt16(this.grdDataElements.Rows[i].Cells["Start"].Value);
						iElemLen = Convert.ToInt16(this.grdDataElements.Rows[i].Cells["Length"].Value);
						bDataElementsValid = ((!bSelected) || (iElemLen>0 && iElemStart>0));
						if(!bDataElementsValid) break;
					}

					//Validate each source has a data element
					sInputSrcs = new string[]{"", "", ""};
					if(this.chkSource1.Checked) sInputSrcs[0] = "1";
					if(this.chkSource2.Checked) sInputSrcs[1] = "2";
					if(this.chkSource3.Checked) sInputSrcs[2] = "3";
					bInputSrcHasDataElem = true;
					for(int j=0; j<sInputSrcs.Length; j++) {
						if(sInputSrcs[j]!="") {
							for(int i=0; i<this.grdDataElements.Rows.Count; i++) {
								bDataElemSelected = Convert.ToBoolean(this.grdDataElements.Rows[i].Cells["Selected"].Value);
								if(bDataElemSelected) {
									sDataElemInputSrc = this.grdDataElements.Rows[i].Cells["InputNumber"].Value.ToString();
									if(sDataElemInputSrc==sInputSrcs[j])  break;
								}
								if(i==this.grdDataElements.Rows.Count-1) {
									bInputSrcHasDataElem = false;
									break;
								}
							}
						}
						if(!bInputSrcHasDataElem) break;
					}

					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (this.cboFreightSortType.Text!="" && this.txtDescription.Text!="" && 
						(this.updLen1.Value>0 && this.updStart1.Value>0) && 
						((!this.chkSource2.Checked) || (this.updLen2.Value>0 && this.updStart2.Value>0)) && 
						((!this.chkSource3.Checked) || (this.updLen3.Value>0 && this.updStart3.Value>0)) && 
						bDataElementsValid && bInputSrcHasDataElem);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			int iLabelID=0; 
			bool bSelected=false, bHasLabelID=false;
			InboundLabelDS.InboundLabelDataElementTableRow row;
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						//Update details with control values
						iLabelID = this.mInboundLabelDS.InboundLabelDetailTable[0].LabelID;
						this.mInboundLabelDS.InboundLabelDetailTable[0].SortTypeID = Convert.ToInt32(this.cboFreightSortType.SelectedValue);
						this.mInboundLabelDS.InboundLabelDetailTable[0].Description = this.txtDescription.Text;
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input1Len = Convert.ToInt16(this.updLen1.Value);
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input1ValidStart = Convert.ToInt16(this.updStart1.Value);
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input1ValidString = this.txtVal1.Text;
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input2Len = (this.chkSource2.Checked) ? Convert.ToInt16(this.updLen2.Value) : (short)0;
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input2ValidStart = (this.chkSource2.Checked) ? Convert.ToInt16(this.updStart2.Value) : (short)0;
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input2ValidString = (this.chkSource2.Checked) ? this.txtVal2.Text : "";
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input3Len = (this.chkSource3.Checked) ? Convert.ToInt16(this.updLen3.Value) : (short)0;
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input3ValidStart = (this.chkSource3.Checked) ? Convert.ToInt16(this.updStart3.Value) : (short)0;
						this.mInboundLabelDS.InboundLabelDetailTable[0].Input3ValidString = (this.chkSource3.Checked) ? this.txtVal3.Text : "";
						
						for(int i=0; i<this.mDataElementsDS.InboundLabelDataElementTable.Rows.Count; i++) {
							bSelected = this.mDataElementsDS.InboundLabelDataElementTable[i].Selected;
							bHasLabelID = (this.mDataElementsDS.InboundLabelDataElementTable[i].LabelID>0) ? true : false;
							if(bSelected && !bHasLabelID) {
								//Add a new data element
								//*** RESULT MUST MARK ROW AS DataRowState.Added
								row = this.mInboundLabelDS.InboundLabelDataElementTable.NewInboundLabelDataElementTableRow();
								row.LabelID = iLabelID;
								row.ElementType = this.mDataElementsDS.InboundLabelDataElementTable[i].ElementType;
								row.InputNumber = this.mDataElementsDS.InboundLabelDataElementTable[i].InputNumber;
								row.Start = this.mDataElementsDS.InboundLabelDataElementTable[i].Start;
								row.Length = this.mDataElementsDS.InboundLabelDataElementTable[i].Length;
								row.IsValueRequired = this.mDataElementsDS.InboundLabelDataElementTable[i].IsValueRequired;
								row.IsDuplicateAllowed = this.mDataElementsDS.InboundLabelDataElementTable[i].IsDuplicateAllowed;
								row.IsCheckDigitValidation = this.mDataElementsDS.InboundLabelDataElementTable[i].IsCheckDigitValidation;
								row.IsUseAltNumber = this.mDataElementsDS.InboundLabelDataElementTable[i].IsUseAltNumber;
								this.mInboundLabelDS.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(row);
							}
							else if(bSelected && bHasLabelID) {
								//Current data element- update as required
								row = (InboundLabelDS.InboundLabelDataElementTableRow)this.mInboundLabelDS.InboundLabelDataElementTable.Select("ElementType='" + this.mDataElementsDS.InboundLabelDataElementTable[i].ElementType.Trim() + "'")[0];
								//row.LabelID = N/A
								//row.ElementType = N/A
								row.InputNumber = this.mDataElementsDS.InboundLabelDataElementTable[i].InputNumber;
								row.Start = this.mDataElementsDS.InboundLabelDataElementTable[i].Start;
								row.Length = this.mDataElementsDS.InboundLabelDataElementTable[i].Length;
								row.IsValueRequired = this.mDataElementsDS.InboundLabelDataElementTable[i].IsValueRequired;
								row.IsDuplicateAllowed = this.mDataElementsDS.InboundLabelDataElementTable[i].IsDuplicateAllowed;
								row.IsCheckDigitValidation = this.mDataElementsDS.InboundLabelDataElementTable[i].IsCheckDigitValidation;
								row.IsUseAltNumber = this.mDataElementsDS.InboundLabelDataElementTable[i].IsUseAltNumber;
							}
							else if(!bSelected && bHasLabelID) {
								//Remove current data element
								//*** RESULT MUST MARK ROW AS DataRowState.Deleted
								row = (InboundLabelDS.InboundLabelDataElementTableRow)this.mInboundLabelDS.InboundLabelDataElementTable.Select("ElementType='" + this.mDataElementsDS.InboundLabelDataElementTable[i].ElementType.Trim() + "'")[0];
								row.Delete();
							}
						}
						#region Test update to mInboundLabelDS.InboundLabelDataElementTable
						Debug.Write("\n");
						for(int i=0; i<this.mInboundLabelDS.InboundLabelDataElementTable.Rows.Count; i++) {
							switch(this.mInboundLabelDS.InboundLabelDataElementTable[i].RowState) {
								case		DataRowState.Added: Debug.Write("Added data element (labelID=)\n"); break;
								case		DataRowState.Modified: Debug.Write("Modified data element (labelID=)\n"); break;
								case		DataRowState.Deleted: Debug.Write("Removed data element (labelID=)\n"); break;
								default:	Debug.Write(this.mInboundLabelDS.InboundLabelDataElementTable[i].RowState.ToString() + " data element (labelID=)\n"); break;
							}
						}
						#endregion
						this.mInboundLabelDS.InboundLabelDetailTable[0].IsActive = this.chkStatus.Checked;
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: reportError()
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
