//	File:	dlgterminalworkstation.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create, edit, and delete terminal workstations.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Tsort.Enterprise;

namespace Tsort {
	//
	public class dlgTerminalWorkstationDetail : System.Windows.Forms.Form {
		//Members
		private string m_sWorkstationName = "";
		#region Controls

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label _lblNumber;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox fraDetails;
		private System.Windows.Forms.TextBox txtLocationDescription;
		private System.Windows.Forms.TextBox txtNumber;
		private System.Windows.Forms.Label _lblDescription;
		private Tsort.Enterprise.WorkstationlDS mWorkstationDS;
		private System.Windows.Forms.ComboBox cboScaleTypes;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.Label _lblScaleType;
		private Tsort.Windows.SelectionList mScaleTypesDS;
		private System.Windows.Forms.Label _lblName;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label _lblPrinterType;
		private System.Windows.Forms.ComboBox cboPrinterTypes;
		private Tsort.Windows.SelectionList mPrinterTypesDS;
		private System.Windows.Forms.Label _lblScalePort;
		private System.Windows.Forms.Label _lblPrinterPort;
		private System.Windows.Forms.CheckBox chkTrace;
		private Tsort.Windows.SelectionList mScalePortsDS;
		private Tsort.Windows.SelectionList mPrinterPortsDS;
		private System.Windows.Forms.ComboBox cboScalePort;
		private System.Windows.Forms.ComboBox cboPrinterPort;
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
		public dlgTerminalWorkstationDetail(ref WorkstationlDS workstation) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mWorkstationDS = workstation;
				if(this.mWorkstationDS.TerminalWorkstationTable.Count>0) {
					this.m_sWorkstationName = this.mWorkstationDS.TerminalWorkstationTable[0].Name;
					this.Text = (this.m_sWorkstationName!="") ? "Terminal Workstation (" + this.m_sWorkstationName.Trim() + ")" : "Terminal Workstation (New)";
				}
				else
					this.Text = "Terminal Workstation (Data Unavailable)";
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgTerminalWorkstationDetail));
			this._lblNumber = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.fraDetails = new System.Windows.Forms.GroupBox();
			this.cboPrinterPort = new System.Windows.Forms.ComboBox();
			this.mPrinterPortsDS = new Tsort.Windows.SelectionList();
			this.cboScalePort = new System.Windows.Forms.ComboBox();
			this.mScalePortsDS = new Tsort.Windows.SelectionList();
			this.chkTrace = new System.Windows.Forms.CheckBox();
			this._lblPrinterPort = new System.Windows.Forms.Label();
			this._lblScalePort = new System.Windows.Forms.Label();
			this._lblPrinterType = new System.Windows.Forms.Label();
			this.cboPrinterTypes = new System.Windows.Forms.ComboBox();
			this.mPrinterTypesDS = new Tsort.Windows.SelectionList();
			this._lblName = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this._lblDescription = new System.Windows.Forms.Label();
			this.txtLocationDescription = new System.Windows.Forms.TextBox();
			this.txtNumber = new System.Windows.Forms.TextBox();
			this._lblScaleType = new System.Windows.Forms.Label();
			this.cboScaleTypes = new System.Windows.Forms.ComboBox();
			this.mScaleTypesDS = new Tsort.Windows.SelectionList();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mPrinterPortsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mScalePortsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mPrinterTypesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mScaleTypesDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblNumber
			// 
			this._lblNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblNumber.Location = new System.Drawing.Point(6, 24);
			this._lblNumber.Name = "_lblNumber";
			this._lblNumber.Size = new System.Drawing.Size(96, 18);
			this._lblNumber.TabIndex = 8;
			this._lblNumber.Text = "Number";
			this._lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(276, 234);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(174, 234);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraDetails
			// 
			this.fraDetails.Controls.Add(this.cboPrinterPort);
			this.fraDetails.Controls.Add(this.cboScalePort);
			this.fraDetails.Controls.Add(this.chkTrace);
			this.fraDetails.Controls.Add(this._lblPrinterPort);
			this.fraDetails.Controls.Add(this._lblScalePort);
			this.fraDetails.Controls.Add(this._lblPrinterType);
			this.fraDetails.Controls.Add(this.cboPrinterTypes);
			this.fraDetails.Controls.Add(this._lblName);
			this.fraDetails.Controls.Add(this.txtName);
			this.fraDetails.Controls.Add(this._lblDescription);
			this.fraDetails.Controls.Add(this.txtLocationDescription);
			this.fraDetails.Controls.Add(this._lblNumber);
			this.fraDetails.Controls.Add(this.txtNumber);
			this.fraDetails.Controls.Add(this._lblScaleType);
			this.fraDetails.Controls.Add(this.cboScaleTypes);
			this.fraDetails.Location = new System.Drawing.Point(6, 6);
			this.fraDetails.Name = "fraDetails";
			this.fraDetails.Size = new System.Drawing.Size(348, 186);
			this.fraDetails.TabIndex = 0;
			this.fraDetails.TabStop = false;
			this.fraDetails.Text = "Terminal Workstation";
			// 
			// cboPrinterPort
			// 
			this.cboPrinterPort.DataSource = this.mPrinterPortsDS;
			this.cboPrinterPort.DisplayMember = "SelectionListTable.Description";
			this.cboPrinterPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPrinterPort.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboPrinterPort.Location = new System.Drawing.Point(276, 126);
			this.cboPrinterPort.Name = "cboPrinterPort";
			this.cboPrinterPort.Size = new System.Drawing.Size(60, 21);
			this.cboPrinterPort.TabIndex = 16;
			this.cboPrinterPort.ValueMember = "SelectionListTable.ID";
			this.cboPrinterPort.SelectionChangeCommitted += new System.EventHandler(this.ValidateForm);
			// 
			// mPrinterPortsDS
			// 
			this.mPrinterPortsDS.DataSetName = "SelectionList";
			this.mPrinterPortsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// cboScalePort
			// 
			this.cboScalePort.DataSource = this.mScalePortsDS;
			this.cboScalePort.DisplayMember = "SelectionListTable.Description";
			this.cboScalePort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboScalePort.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboScalePort.Location = new System.Drawing.Point(276, 102);
			this.cboScalePort.Name = "cboScalePort";
			this.cboScalePort.Size = new System.Drawing.Size(60, 21);
			this.cboScalePort.TabIndex = 15;
			this.cboScalePort.ValueMember = "SelectionListTable.ID";
			this.cboScalePort.SelectionChangeCommitted += new System.EventHandler(this.ValidateForm);
			// 
			// mScalePortsDS
			// 
			this.mScalePortsDS.DataSetName = "SelectionList";
			this.mScalePortsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// chkTrace
			// 
			this.chkTrace.Location = new System.Drawing.Point(108, 156);
			this.chkTrace.Name = "chkTrace";
			this.chkTrace.Size = new System.Drawing.Size(120, 18);
			this.chkTrace.TabIndex = 7;
			this.chkTrace.Text = "Trace";
			this.chkTrace.CheckStateChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblPrinterPort
			// 
			this._lblPrinterPort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblPrinterPort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblPrinterPort.Location = new System.Drawing.Point(234, 126);
			this._lblPrinterPort.Name = "_lblPrinterPort";
			this._lblPrinterPort.Size = new System.Drawing.Size(32, 18);
			this._lblPrinterPort.TabIndex = 14;
			this._lblPrinterPort.Text = "Port";
			this._lblPrinterPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblScalePort
			// 
			this._lblScalePort.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblScalePort.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblScalePort.Location = new System.Drawing.Point(234, 102);
			this._lblScalePort.Name = "_lblScalePort";
			this._lblScalePort.Size = new System.Drawing.Size(32, 18);
			this._lblScalePort.TabIndex = 12;
			this._lblScalePort.Text = "Port";
			this._lblScalePort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblPrinterType
			// 
			this._lblPrinterType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblPrinterType.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblPrinterType.Location = new System.Drawing.Point(6, 126);
			this._lblPrinterType.Name = "_lblPrinterType";
			this._lblPrinterType.Size = new System.Drawing.Size(96, 18);
			this._lblPrinterType.TabIndex = 13;
			this._lblPrinterType.Text = "Printer Type";
			this._lblPrinterType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboPrinterTypes
			// 
			this.cboPrinterTypes.DataSource = this.mPrinterTypesDS;
			this.cboPrinterTypes.DisplayMember = "SelectionListTable.Description";
			this.cboPrinterTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPrinterTypes.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboPrinterTypes.Location = new System.Drawing.Point(108, 126);
			this.cboPrinterTypes.Name = "cboPrinterTypes";
			this.cboPrinterTypes.Size = new System.Drawing.Size(120, 21);
			this.cboPrinterTypes.TabIndex = 5;
			this.cboPrinterTypes.ValueMember = "SelectionListTable.ID";
			this.cboPrinterTypes.SelectionChangeCommitted += new System.EventHandler(this.ValidateForm);
			// 
			// mPrinterTypesDS
			// 
			this.mPrinterTypesDS.DataSetName = "SelectionList";
			this.mPrinterTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblName
			// 
			this._lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblName.Location = new System.Drawing.Point(6, 48);
			this._lblName.Name = "_lblName";
			this._lblName.Size = new System.Drawing.Size(96, 18);
			this._lblName.TabIndex = 9;
			this._lblName.Text = "Name";
			this._lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtName
			// 
			this.txtName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtName.Location = new System.Drawing.Point(108, 48);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(144, 21);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			this.txtName.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblDescription
			// 
			this._lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDescription.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblDescription.Location = new System.Drawing.Point(6, 72);
			this._lblDescription.Name = "_lblDescription";
			this._lblDescription.Size = new System.Drawing.Size(96, 18);
			this._lblDescription.TabIndex = 10;
			this._lblDescription.Text = "Description";
			this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLocationDescription
			// 
			this.txtLocationDescription.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLocationDescription.Location = new System.Drawing.Point(108, 72);
			this.txtLocationDescription.Name = "txtLocationDescription";
			this.txtLocationDescription.Size = new System.Drawing.Size(228, 21);
			this.txtLocationDescription.TabIndex = 2;
			this.txtLocationDescription.Text = "";
			this.txtLocationDescription.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtNumber
			// 
			this.txtNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtNumber.Location = new System.Drawing.Point(108, 24);
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(72, 21);
			this.txtNumber.TabIndex = 0;
			this.txtNumber.Text = "";
			this.txtNumber.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblScaleType
			// 
			this._lblScaleType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblScaleType.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblScaleType.Location = new System.Drawing.Point(6, 102);
			this._lblScaleType.Name = "_lblScaleType";
			this._lblScaleType.Size = new System.Drawing.Size(96, 18);
			this._lblScaleType.TabIndex = 11;
			this._lblScaleType.Text = "Scale Type";
			this._lblScaleType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboScaleTypes
			// 
			this.cboScaleTypes.DataSource = this.mScaleTypesDS;
			this.cboScaleTypes.DisplayMember = "SelectionListTable.Description";
			this.cboScaleTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboScaleTypes.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboScaleTypes.Location = new System.Drawing.Point(108, 102);
			this.cboScaleTypes.Name = "cboScaleTypes";
			this.cboScaleTypes.Size = new System.Drawing.Size(120, 21);
			this.cboScaleTypes.TabIndex = 3;
			this.cboScaleTypes.ValueMember = "SelectionListTable.ID";
			this.cboScaleTypes.SelectionChangeCommitted += new System.EventHandler(this.ValidateForm);
			// 
			// mScaleTypesDS
			// 
			this.mScaleTypesDS.DataSetName = "SelectionList";
			this.mScaleTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(369, 225);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraDetails);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(361, 199);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// dlgTerminalWorkstationDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(378, 263);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgTerminalWorkstationDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Terminal Workstation Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mPrinterPortsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mScalePortsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mPrinterTypesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mScaleTypesDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mScaleTypesDS.Merge(EnterpriseFactory.GetWorkstationScaleTypes());
				this.mPrinterTypesDS.Merge(EnterpriseFactory.GetWorkstationPrinterTypes());
				this.mScalePortsDS.Merge(EnterpriseFactory.GetWorkstationPorts());
				this.mPrinterPortsDS.Merge(EnterpriseFactory.GetWorkstationPorts());

				//Set control services
				this.txtName.MaxLength = 20;
				this.txtName.Text = this.mWorkstationDS.TerminalWorkstationTable[0].Name.Trim();
				this.txtNumber.MaxLength = 10;
				this.txtNumber.Text = "";
				if(!this.mWorkstationDS.TerminalWorkstationTable[0].IsNumberNull())
					this.txtNumber.Text = this.mWorkstationDS.TerminalWorkstationTable[0].Number.Trim();
				this.txtLocationDescription.MaxLength = 40;
				this.txtLocationDescription.Text = "";
				if(!this.mWorkstationDS.TerminalWorkstationTable[0].IsDescriptionNull())
					this.txtLocationDescription.Text = this.mWorkstationDS.TerminalWorkstationTable[0].Description.Trim();
				if(!this.mWorkstationDS.TerminalWorkstationTable[0].IsScaleTypeNull()) 
					this.cboScaleTypes.SelectedValue = this.mWorkstationDS.TerminalWorkstationTable[0].ScaleType;
				else
					if(this.cboScaleTypes.Items.Count>0) this.cboScaleTypes.SelectedIndex = 0;
				this.cboScaleTypes.Enabled = (this.cboScaleTypes.Items.Count>0);
				if(!this.mWorkstationDS.TerminalWorkstationTable[0].IsScalePortNull()) 
					this.cboScalePort.SelectedValue = this.mWorkstationDS.TerminalWorkstationTable[0].ScalePort;
				else
					if(this.cboScalePort.Items.Count>0) this.cboScalePort.SelectedIndex = 0;
				this.cboScalePort.Enabled = (this.cboScalePort.Items.Count>0);
				if(!this.mWorkstationDS.TerminalWorkstationTable[0].IsPrinterTypeNull()) 
					this.cboPrinterTypes.SelectedValue = this.mWorkstationDS.TerminalWorkstationTable[0].PrinterType;
				else
					if(this.cboPrinterTypes.Items.Count>0) this.cboPrinterTypes.SelectedIndex = 0;
				this.cboPrinterTypes.Enabled = (this.cboPrinterTypes.Items.Count>0);
				if(!this.mWorkstationDS.TerminalWorkstationTable[0].IsPrinterPortNull()) 
					this.cboPrinterPort.SelectedValue = this.mWorkstationDS.TerminalWorkstationTable[0].PrinterPort;
				else
					if(this.cboPrinterPort.Items.Count>0) this.cboPrinterPort.SelectedIndex = 0;
				this.cboPrinterPort.Enabled = (this.cboPrinterPort.Items.Count>0);
				this.chkTrace.Checked = this.mWorkstationDS.TerminalWorkstationTable[0].IsTraceNull() ? false : this.mWorkstationDS.TerminalWorkstationTable[0].Trace;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
				if(this.mWorkstationDS.TerminalWorkstationTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtNumber.Text!="" && this.txtName.Text!="" && this.txtLocationDescription.Text!="");
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
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
						this.Cursor = Cursors.WaitCursor; this.mWorkstationDS.TerminalWorkstationTable[0].Name = this.txtName.Text;
						this.mWorkstationDS.TerminalWorkstationTable[0].Number = this.txtNumber.Text;
						this.mWorkstationDS.TerminalWorkstationTable[0].Description = this.txtLocationDescription.Text;
						this.mWorkstationDS.TerminalWorkstationTable[0].ScaleType = this.cboScaleTypes.SelectedValue.ToString();
						this.mWorkstationDS.TerminalWorkstationTable[0].ScalePort = this.cboScalePort.SelectedValue.ToString();
						this.mWorkstationDS.TerminalWorkstationTable[0].PrinterType = this.cboPrinterTypes.SelectedValue.ToString();
						this.mWorkstationDS.TerminalWorkstationTable[0].PrinterPort = this.cboPrinterPort.SelectedValue.ToString();
						this.mWorkstationDS.TerminalWorkstationTable[0].Trace = this.chkTrace.Checked;
						this.mWorkstationDS.AcceptChanges();
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
