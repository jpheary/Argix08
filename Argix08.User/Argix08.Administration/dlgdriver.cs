//	File:	dlgdriver.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	Dialog to create a new LTA Driver or edit an existing LTA Driver.
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
using Tsort.Transportation;

namespace Tsort {
	//
	public class dlgDriverDetail : System.Windows.Forms.Form {
		//Members
		private int mDriverID=0;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboTerminal;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.Label _lblNameFirst;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.TextBox txtNameLast;
		private System.Windows.Forms.TextBox txtNameFirst;
		private System.Windows.Forms.Label _lblPhone;
		private System.Windows.Forms.Label _lblNameLast;
		private Tsort.Transportation.DriverDS mDriverDS;
		private Tsort.Windows.SelectionList mTerminalsDS;
		private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit mskPhone;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
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
		public dlgDriverDetail(ref DriverDS driver) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mDriverDS = driver;
				if(this.mDriverDS.DriverDetailTable.Count>0) {
					this.mDriverID = this.mDriverDS.DriverDetailTable[0].DriverID;
					this.Text = (this.mDriverID>0) ? "Driver (" + this.mDriverID + ")" : "Driver (New)";
				}
				else
					this.Text = "Driver (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgDriverDetail));
			this.cboTerminal = new System.Windows.Forms.ComboBox();
			this.mTerminalsDS = new Tsort.Windows.SelectionList();
			this._lblTerminal = new System.Windows.Forms.Label();
			this._lblNameFirst = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this._lblNameLast = new System.Windows.Forms.Label();
			this.txtNameLast = new System.Windows.Forms.TextBox();
			this.txtNameFirst = new System.Windows.Forms.TextBox();
			this._lblPhone = new System.Windows.Forms.Label();
			this.mskPhone = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
			this.mDriverDS = new Tsort.Transportation.DriverDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.mTerminalsDS)).BeginInit();
			this.grpDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mDriverDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboTerminal
			// 
			this.cboTerminal.DataSource = this.mTerminalsDS;
			this.cboTerminal.DisplayMember = "SelectionListTable.Description";
			this.cboTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTerminal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboTerminal.Location = new System.Drawing.Point(108, 84);
			this.cboTerminal.Name = "cboTerminal";
			this.cboTerminal.Size = new System.Drawing.Size(174, 21);
			this.cboTerminal.TabIndex = 2;
			this.cboTerminal.ValueMember = "SelectionListTable.ID";
			this.cboTerminal.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mTerminalsDS
			// 
			this.mTerminalsDS.DataSetName = "SelectionList";
			this.mTerminalsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblTerminal
			// 
			this._lblTerminal.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTerminal.Location = new System.Drawing.Point(6, 84);
			this._lblTerminal.Name = "_lblTerminal";
			this._lblTerminal.Size = new System.Drawing.Size(96, 18);
			this._lblTerminal.TabIndex = 2;
			this._lblTerminal.Text = "Terminal";
			this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblNameFirst
			// 
			this._lblNameFirst.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblNameFirst.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblNameFirst.Location = new System.Drawing.Point(6, 24);
			this._lblNameFirst.Name = "_lblNameFirst";
			this._lblNameFirst.Size = new System.Drawing.Size(96, 18);
			this._lblNameFirst.TabIndex = 13;
			this._lblNameFirst.Text = "First Name";
			this._lblNameFirst.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// grpDetails
			// 
			this.grpDetails.Controls.Add(this.chkStatus);
			this.grpDetails.Controls.Add(this._lblTerminal);
			this.grpDetails.Controls.Add(this.cboTerminal);
			this.grpDetails.Controls.Add(this._lblNameLast);
			this.grpDetails.Controls.Add(this.txtNameLast);
			this.grpDetails.Controls.Add(this._lblNameFirst);
			this.grpDetails.Controls.Add(this.txtNameFirst);
			this.grpDetails.Controls.Add(this._lblPhone);
			this.grpDetails.Controls.Add(this.mskPhone);
			this.grpDetails.Location = new System.Drawing.Point(6, 6);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(348, 186);
			this.grpDetails.TabIndex = 0;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Driver";
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(108, 156);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 21);
			this.chkStatus.TabIndex = 4;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblNameLast
			// 
			this._lblNameLast.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblNameLast.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblNameLast.Location = new System.Drawing.Point(6, 54);
			this._lblNameLast.Name = "_lblNameLast";
			this._lblNameLast.Size = new System.Drawing.Size(96, 18);
			this._lblNameLast.TabIndex = 15;
			this._lblNameLast.Text = "Last Name";
			this._lblNameLast.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtNameLast
			// 
			this.txtNameLast.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtNameLast.Location = new System.Drawing.Point(108, 54);
			this.txtNameLast.Name = "txtNameLast";
			this.txtNameLast.Size = new System.Drawing.Size(228, 21);
			this.txtNameLast.TabIndex = 1;
			this.txtNameLast.Text = "";
			this.txtNameLast.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtNameFirst
			// 
			this.txtNameFirst.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtNameFirst.Location = new System.Drawing.Point(108, 24);
			this.txtNameFirst.Name = "txtNameFirst";
			this.txtNameFirst.Size = new System.Drawing.Size(228, 21);
			this.txtNameFirst.TabIndex = 0;
			this.txtNameFirst.Text = "";
			this.txtNameFirst.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblPhone
			// 
			this._lblPhone.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblPhone.Location = new System.Drawing.Point(6, 114);
			this._lblPhone.Name = "_lblPhone";
			this._lblPhone.Size = new System.Drawing.Size(96, 18);
			this._lblPhone.TabIndex = 17;
			this._lblPhone.Text = "Phone #";
			this._lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mskPhone
			// 
			this.mskPhone.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
			this.mskPhone.Location = new System.Drawing.Point(108, 114);
			this.mskPhone.Name = "mskPhone";
			this.mskPhone.TabIndex = 3;
			this.mskPhone.ValueChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mDriverDS
			// 
			this.mDriverDS.DataSetName = "DriverDS";
			this.mDriverDS.Locale = new System.Globalization.CultureInfo("en-US");
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
			this.tabGeneral.Controls.Add(this.grpDetails);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(361, 199);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// dlgDriverDetail
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
			this.Name = "dlgDriverDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Driver Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mTerminalsDS)).EndInit();
			this.grpDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mDriverDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial service states
				this.Visible = true;
				Application.DoEvents();
								
				//Get selection lists
				this.mTerminalsDS.Merge(EnterpriseFactory.GetEntTerminals());
				
				//Set control services
				this.txtNameFirst.MaxLength = 30;
				this.txtNameFirst.Text = "";
				if(!this.mDriverDS.DriverDetailTable[0].IsFirstNameNull())
					this.txtNameFirst.Text = this.mDriverDS.DriverDetailTable[0].FirstName.Trim();
				this.txtNameLast.MaxLength = 40;
				this.txtNameLast.Text = "";
				if(!this.mDriverDS.DriverDetailTable[0].IsLastNameNull())
					this.txtNameLast.Text = this.mDriverDS.DriverDetailTable[0].LastName.Trim();
				this.mskPhone.InputMask = "###-###-####";
				if(!this.mDriverDS.DriverDetailTable[0].IsPhoneNull())
					this.mskPhone.Value = this.mDriverDS.DriverDetailTable[0].Phone;
				if(this.mDriverDS.DriverDetailTable[0].TerminalID!=0) 
					this.cboTerminal.SelectedValue = this.mDriverDS.DriverDetailTable[0].TerminalID;
				else
					if(this.cboTerminal.Items.Count>0) this.cboTerminal.SelectedIndex = 0;
				this.cboTerminal.Enabled = (this.cboTerminal.Items.Count>0);
				this.chkStatus.Checked = this.mDriverDS.DriverDetailTable[0].IsActive;

				//Reset
				
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
				if(this.mDriverDS.DriverDetailTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtNameLast.Text!="" && this.txtNameFirst.Text!="" && 
						this.cboTerminal.Text!="");
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
						this.Cursor = Cursors.WaitCursor;
						this.mDriverDS.DriverDetailTable[0].FirstName = this.txtNameFirst.Text;
						this.mDriverDS.DriverDetailTable[0].LastName = this.txtNameLast.Text;
						if(this.mskPhone.Value!=System.DBNull.Value)
							this.mDriverDS.DriverDetailTable[0].Phone = this.mskPhone.Value.ToString();
						else
							this.mDriverDS.DriverDetailTable[0].SetPhoneNull();
						this.mDriverDS.DriverDetailTable[0].TerminalID = Convert.ToInt32(this.cboTerminal.SelectedValue);
						this.mDriverDS.DriverDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mDriverDS.AcceptChanges();
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
