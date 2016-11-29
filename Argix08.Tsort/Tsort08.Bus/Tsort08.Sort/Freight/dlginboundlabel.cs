//	File:	dlginboundlabel.cs
//	Author:	J. Heary
//	Date:	07/10/08
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

namespace Tsort.Freight {
	//
	internal class dlgInboundLabel : System.Windows.Forms.Form {
		//Members
		private InboundLabel mLabel=null;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.ComboBox cboFreightSortType;
		private System.Windows.Forms.Label lblFreightSortType;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label lblDescription;
		private System.Windows.Forms.TextBox txtVal1;
		private System.Windows.Forms.Label lblVal1;
		private System.Windows.Forms.GroupBox fraDataElements;
		private System.Windows.Forms.DataGrid grdDataElements;
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
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		//Interface
		public dlgInboundLabel(InboundLabel label) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mLabel = label;
				this.Text = (this.mLabel!=null) ? "Inbound Label (" + this.mLabel.LabelID.ToString() + ")" : "Inbound Label (New)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgInboundLabel));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.fraDataElements = new System.Windows.Forms.GroupBox();
			this.grdDataElements = new System.Windows.Forms.DataGrid();
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
			this.grdDataElements.CaptionVisible = false;
			this.grdDataElements.DataMember = "InboundLabelDataElementTable";
			this.grdDataElements.DataSource = this.mDataElementsDS;
			this.grdDataElements.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.grdDataElements.Location = new System.Drawing.Point(6, 18);
			this.grdDataElements.Name = "grdDataElements";
			this.grdDataElements.Size = new System.Drawing.Size(624, 120);
			this.grdDataElements.TabIndex = 0;
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
								
				//Set control services
				this.cboFreightSortType.Enabled = false;
				#region Default grid behavior
				#endregion
				this.txtDescription.MaxLength = 30;
				this.txtDescription.Text = this.mLabel.Description.Trim();
				this.updLen1.Minimum = this.updStart1.Minimum = 0;
				this.updLen1.Maximum = 99;
				this.updLen2.Minimum = this.updStart2.Minimum = 0;
				this.updLen2.Maximum = 99;
				this.updLen3.Minimum = this.updStart3.Minimum = 0;
				this.updLen3.Maximum = 99;
				this.chkSource1.Checked = false;
				this.chkSource1.Checked = true;
				this.chkSource1.Enabled = false;
				this.chkSource2.Checked = (this.mLabel.Inputs[1].Length==0);
				this.chkSource2.Checked = (this.mLabel.Inputs[1].Length>0);
				this.chkSource2.Enabled = true;
				this.chkSource3.Checked = (this.mLabel.Inputs[2].Length==0);
				this.chkSource3.Checked = (this.mLabel.Inputs[2].Length>0);
				//this.chkSource3.Enabled = false;
				this.chkStatus.Checked = true;	//this.mLabel.IsActive;
				
				//Set freight sort type after input sources since data elements are dependent upon freight 
				//sort type AND input source
				if(this.mLabel.SortTypeID!=0) 
					this.cboFreightSortType.SelectedValue = this.mLabel.SortTypeID;
				else 
					if(this.cboFreightSortType.Items.Count>0) this.cboFreightSortType.SelectedIndex = 0;
				this.cboFreightSortType.Enabled = (this.cboFreightSortType.Items.Count>0);
				OnFreightSortTypeChanged(null, null);
				this.cboFreightSortType.Enabled = (this.mLabel.LabelID==0);
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnFreightSortTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change to freight sort type
			try {
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region Input Source: OnInputSourceChecked(), OnInputSourceLengthChanged(), OnValidationStringStartPositionChanged()
		private void OnInputSourceChecked(object sender, System.EventArgs e) {
			//Event handler for change to an input source as selected or not
			int iLen=0, iStart=0;
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
						iLen = (int)this.mLabel.Inputs[0].Length;
						iStart = (int)this.mLabel.Inputs[0].ValidationStart;
						if(bInputSrcEnabled) {
							if(iLen<1) iLen = 1;
							if(iStart<1) iStart = 1;
							this.txtVal1.Text = this.mLabel.Inputs[0].ValidationString;
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
						iLen = (int)this.mLabel.Inputs[1].Length;
						iStart = (int)this.mLabel.Inputs[1].ValidationStart;
						if(bInputSrcEnabled) {
							if(iLen<1) iLen = 1;
							if(iStart<1) iStart = 1;
							this.txtVal2.Text = this.mLabel.Inputs[1].ValidationString;
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
						iLen = (int)this.mLabel.Inputs[2].Length;
						iStart = (int)this.mLabel.Inputs[2].ValidationStart;
						if(bInputSrcEnabled) {
							if(iLen<1) iLen = 1;
							if(iStart<1) iStart = 1;
							this.txtVal3.Text = this.mLabel.Inputs[2].ValidationString;
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
				
				for(int i=0; i<this.grdDataElements.VisibleRowCount; i++) {
					//Update source # for data elements if required
					sInputSrc = this.grdDataElements[0, 0].ToString();
					switch(sInputSrc) {
						case "1": break;
						case "2": if(!this.chkSource2.Checked)  this.grdDataElements[0, 0] = "1"; break;
						case "3": if(!this.chkSource3.Checked)  this.grdDataElements[0, 0] = (this.chkSource2.Checked) ? "2" : "1"; break;
					}
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnInputSourceLengthChanged(object sender, System.EventArgs e) {
			//Event handler for changes to input source length
//			bool bSelected=false;
//			string sInputSrc="";
//			short iSrcLen=0, iElemStart=0, iElemLen=0;
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
//				for(int i=0; i<this.grdDataElements.VisibleRowCount; i++) {
//					//Update data element length as required
//					bSelected = Convert.ToBoolean(this.grdDataElements.Rows[i].Cells["Selected"].Value);
//					if(bSelected) {
//						sInputSrc = this.grdDataElements.Rows[i].Cells["InputNumber"].Value.ToString();
//						switch(sInputSrc) {
//							case "1": iSrcLen = (short)this.updLen1.Value; break;
//							case "2": iSrcLen = (short)this.updLen2.Value; break;
//							case "3": iSrcLen = (short)this.updLen3.Value; break;
//						}
//						if(iSrcLen>0) {
//							//Adjust data element length and start positon according to input src length
//							iElemLen = Convert.ToInt16(this.grdDataElements.Rows[i].Cells["Length"].Value);
//							iElemStart = Convert.ToInt16(this.grdDataElements.Rows[i].Cells["Start"].Value);
//							if(iElemLen>iSrcLen) 
//								this.grdDataElements.Rows[i].Cells["Length"].Value = iElemLen = iSrcLen;
//							if(iElemStart>(iSrcLen + 1 - iElemLen)) 
//								this.grdDataElements.Rows[i].Cells["Start"].Value = (iSrcLen + 1 - iElemLen);
//						}
//						else {
//							//Input src not selected- deselect data elements for this input src
//							this.grdDataElements.Rows[i].Cells["Selected"].Value = false;
//						}
//					}
//				}
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
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL: this.DialogResult = DialogResult.Cancel; this.Close(); break;
					case CMD_OK:	this.DialogResult = DialogResult.OK; this.Close(); break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: reportError()
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
//			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
