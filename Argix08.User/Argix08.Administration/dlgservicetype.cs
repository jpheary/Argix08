//	File:	dlgservicetype
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	Dialog to create or edit Service Types for an outbound freight type.
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
	public class dlgServiceTypeDetail : System.Windows.Forms.Form {
		//Members
		private int mAgentID=0;
		private int mOBServiceTypeID=0;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.GroupBox fraDetails;
		private System.Windows.Forms.ComboBox cboNativeServiceType;
		private System.Windows.Forms.Label _lblNativeServType;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label _lblDescription;
		private System.Windows.Forms.Label _lblMnemonic;
		private System.Windows.Forms.TextBox txtMnemonic;
		private System.Windows.Forms.CheckBox chkIsPickup;
		private Tsort.Enterprise.OutboundServiceTypeDS mOutboundServiceTypeDS;
		private Tsort.Enterprise.OutboundAgentServiceDS mOutboundAgentServiceDS;
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
		public dlgServiceTypeDetail(int agentID, ref OutboundServiceTypeDS serviceType) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				                	
				//Set mediator service, data, and titlebar caption
				this.mAgentID = agentID;
				this.mOutboundServiceTypeDS = serviceType;
				if(this.mOutboundServiceTypeDS.OutboundServiceTypeTable.Count>0) {
					this.mOBServiceTypeID = this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].ServiceID;
					this.Text = (this.mOBServiceTypeID>0) ? "Outbound Service Type (" + this.mOBServiceTypeID + ")" : "Outbound Service Type (New)";
				}
				else
					this.Text = "Outbound Service Type (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgServiceTypeDetail));
			this.cboNativeServiceType = new System.Windows.Forms.ComboBox();
			this.mOutboundAgentServiceDS = new Tsort.Enterprise.OutboundAgentServiceDS();
			this._lblNativeServType = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.fraDetails = new System.Windows.Forms.GroupBox();
			this.chkIsPickup = new System.Windows.Forms.CheckBox();
			this._lblMnemonic = new System.Windows.Forms.Label();
			this.txtMnemonic = new System.Windows.Forms.TextBox();
			this._lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.mOutboundServiceTypeDS = new Tsort.Enterprise.OutboundServiceTypeDS();
			((System.ComponentModel.ISupportInitialize)(this.mOutboundAgentServiceDS)).BeginInit();
			this.fraDetails.SuspendLayout();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mOutboundServiceTypeDS)).BeginInit();
			this.SuspendLayout();
			// 
			// cboNativeServiceType
			// 
			this.cboNativeServiceType.DataSource = this.mOutboundAgentServiceDS;
			this.cboNativeServiceType.DisplayMember = "OutboundAgentServiceTable.Description";
			this.cboNativeServiceType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboNativeServiceType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboNativeServiceType.Location = new System.Drawing.Point(129, 75);
			this.cboNativeServiceType.Name = "cboNativeServiceType";
			this.cboNativeServiceType.Size = new System.Drawing.Size(144, 21);
			this.cboNativeServiceType.TabIndex = 2;
			this.cboNativeServiceType.ValueMember = "OutboundAgentServiceTable.AgentServiceID";
			this.cboNativeServiceType.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mOutboundAgentServiceDS
			// 
			this.mOutboundAgentServiceDS.DataSetName = "OutboundAgentServiceDS";
			this.mOutboundAgentServiceDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblNativeServType
			// 
			this._lblNativeServType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblNativeServType.Location = new System.Drawing.Point(3, 75);
			this._lblNativeServType.Name = "_lblNativeServType";
			this._lblNativeServType.Size = new System.Drawing.Size(120, 18);
			this._lblNativeServType.TabIndex = 2;
			this._lblNativeServType.Text = "Agent Service";
			this._lblNativeServType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(279, 237);
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
			this.btnOk.Location = new System.Drawing.Point(177, 237);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraDetails
			// 
			this.fraDetails.Controls.Add(this.chkIsPickup);
			this.fraDetails.Controls.Add(this._lblMnemonic);
			this.fraDetails.Controls.Add(this.txtMnemonic);
			this.fraDetails.Controls.Add(this._lblDescription);
			this.fraDetails.Controls.Add(this.txtDescription);
			this.fraDetails.Controls.Add(this.chkStatus);
			this.fraDetails.Controls.Add(this._lblNativeServType);
			this.fraDetails.Controls.Add(this.cboNativeServiceType);
			this.fraDetails.Location = new System.Drawing.Point(3, 3);
			this.fraDetails.Name = "fraDetails";
			this.fraDetails.Size = new System.Drawing.Size(357, 195);
			this.fraDetails.TabIndex = 0;
			this.fraDetails.TabStop = false;
			this.fraDetails.Text = "Service Type";
			// 
			// chkIsPickup
			// 
			this.chkIsPickup.Checked = true;
			this.chkIsPickup.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkIsPickup.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkIsPickup.Location = new System.Drawing.Point(129, 105);
			this.chkIsPickup.Name = "chkIsPickup";
			this.chkIsPickup.Size = new System.Drawing.Size(96, 16);
			this.chkIsPickup.TabIndex = 3;
			this.chkIsPickup.Text = "Is Pickup";
			this.chkIsPickup.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblMnemonic
			// 
			this._lblMnemonic.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblMnemonic.Location = new System.Drawing.Point(3, 48);
			this._lblMnemonic.Name = "_lblMnemonic";
			this._lblMnemonic.Size = new System.Drawing.Size(120, 18);
			this._lblMnemonic.TabIndex = 8;
			this._lblMnemonic.Text = "Mnemonic";
			this._lblMnemonic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtMnemonic
			// 
			this.txtMnemonic.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtMnemonic.Location = new System.Drawing.Point(129, 48);
			this.txtMnemonic.Name = "txtMnemonic";
			this.txtMnemonic.Size = new System.Drawing.Size(36, 21);
			this.txtMnemonic.TabIndex = 1;
			this.txtMnemonic.Text = "";
			this.txtMnemonic.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblDescription
			// 
			this._lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDescription.Location = new System.Drawing.Point(3, 21);
			this._lblDescription.Name = "_lblDescription";
			this._lblDescription.Size = new System.Drawing.Size(120, 18);
			this._lblDescription.TabIndex = 6;
			this._lblDescription.Text = "Description";
			this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDescription
			// 
			this.txtDescription.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtDescription.Location = new System.Drawing.Point(129, 21);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(222, 21);
			this.txtDescription.TabIndex = 0;
			this.txtDescription.Text = "";
			this.txtDescription.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(129, 129);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 16);
			this.chkStatus.TabIndex = 4;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(372, 228);
			this.tabDialog.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraDetails);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(364, 202);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// mOutboundServiceTypeDS
			// 
			this.mOutboundServiceTypeDS.DataSetName = "OutboundServiceTypeDS";
			this.mOutboundServiceTypeDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgServiceTypeDetail
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
			this.Name = "dlgServiceTypeDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Outbound Service Type Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mOutboundAgentServiceDS)).EndInit();
			this.fraDetails.ResumeLayout(false);
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mOutboundServiceTypeDS)).EndInit();
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
				this.mOutboundAgentServiceDS.Merge(EnterpriseFactory.GetOutboundAgentServices(this.mAgentID));
				
				//Set control services
				this.txtDescription.MaxLength = 40;
				this.txtDescription.Text = this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].Description ;
				this.txtMnemonic.MaxLength = 3;
				this.txtMnemonic.Text = this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].Mnemonic;
				if (!this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].IsAgentServiceIDNull())
					this.cboNativeServiceType.SelectedValue = this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].AgentServiceID;
				else
					if(this.cboNativeServiceType.Items.Count>0) this.cboNativeServiceType.SelectedIndex = 0;
				this.cboNativeServiceType.Enabled = (this.cboNativeServiceType.Items.Count>0);
				this.chkIsPickup.Checked = this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].IsPickup;
				this.chkStatus.Checked = this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].IsActive;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
				//Enable OK service if details have valid changes
				this.btnOk.Enabled = (this.txtDescription.Text!="" && this.txtMnemonic.Text!="" && this.cboNativeServiceType.Text!="");
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
						this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].Description = this.txtDescription.Text;
						this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].Mnemonic = this.txtMnemonic.Text;
						this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].AgentServiceID = Convert.ToInt32(this.cboNativeServiceType.SelectedValue);
						this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].IsPickup = this.chkIsPickup.Checked;
						this.mOutboundServiceTypeDS.OutboundServiceTypeTable[0].IsActive = this.chkStatus.Checked;
						this.mOutboundServiceTypeDS.AcceptChanges();
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
