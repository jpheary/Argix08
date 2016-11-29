//	File:	dlgentitypaymentservice.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new entity-paymentservice association or edit an existing 
//			entity-paymentservice association.
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
	public class dlgCompanyPaymentService : System.Windows.Forms.Form {
		//Members
		private int mPaymentServiceID=0;
		private int mEntityID=0;
		private bool mParentIsActive=true;		
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboPaymentServices;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.TextBox txtComments;
		private Tsort.Windows.SelectionList mPaymentServicesDS;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		private Tsort.Enterprise.CompanyDS mAssociationDS;
		private System.Windows.Forms.Label _lblPaymentService;
		private System.Windows.Forms.CheckBox chkStatus;
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
		public dlgCompanyPaymentService(int entityID, bool parentIsActive, ref CompanyDS association) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mEntityID = entityID;
				this.mParentIsActive = parentIsActive;
				this.mAssociationDS = association;
				if(this.mAssociationDS.CompanyPaymentServiceTable.Count > 0) {
					this.mPaymentServiceID = this.mAssociationDS.CompanyPaymentServiceTable[0].PaymentServiceID;
					this.Text = (this.mPaymentServiceID > 0) ? "Payment Service Asociation (" + this.mPaymentServiceID + ")" : "Payment Service Asociation (New)";
				}
				else
					this.Text = "Payment Service Association (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgCompanyPaymentService));
			this.cboPaymentServices = new System.Windows.Forms.ComboBox();
			this.mPaymentServicesDS = new Tsort.Windows.SelectionList();
			this._lblPaymentService = new System.Windows.Forms.Label();
			this._lblComments = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.mAssociationDS = new Tsort.Enterprise.CompanyDS();
			((System.ComponentModel.ISupportInitialize)(this.mPaymentServicesDS)).BeginInit();
			this.grpDetails.SuspendLayout();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mAssociationDS)).BeginInit();
			this.SuspendLayout();
			// 
			// cboPaymentServices
			// 
			this.cboPaymentServices.DataSource = this.mPaymentServicesDS;
			this.cboPaymentServices.DisplayMember = "SelectionListTable.Description";
			this.cboPaymentServices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPaymentServices.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboPaymentServices.Location = new System.Drawing.Point(108, 24);
			this.cboPaymentServices.Name = "cboPaymentServices";
			this.cboPaymentServices.Size = new System.Drawing.Size(174, 21);
			this.cboPaymentServices.TabIndex = 0;
			this.cboPaymentServices.ValueMember = "SelectionListTable.ID";
			this.cboPaymentServices.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mPaymentServicesDS
			// 
			this.mPaymentServicesDS.DataSetName = "SelectionList";
			this.mPaymentServicesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblPaymentService
			// 
			this._lblPaymentService.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblPaymentService.Location = new System.Drawing.Point(6, 24);
			this._lblPaymentService.Name = "_lblPaymentService";
			this._lblPaymentService.Size = new System.Drawing.Size(96, 18);
			this._lblPaymentService.TabIndex = 2;
			this._lblPaymentService.Text = "Paymt Service";
			this._lblPaymentService.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblComments
			// 
			this._lblComments.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblComments.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblComments.Location = new System.Drawing.Point(6, 54);
			this._lblComments.Name = "_lblComments";
			this._lblComments.Size = new System.Drawing.Size(96, 18);
			this._lblComments.TabIndex = 13;
			this._lblComments.Text = "Comments";
			this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.grpDetails.Controls.Add(this._lblPaymentService);
			this.grpDetails.Controls.Add(this.cboPaymentServices);
			this.grpDetails.Controls.Add(this._lblComments);
			this.grpDetails.Controls.Add(this.txtComments);
			this.grpDetails.Location = new System.Drawing.Point(6, 6);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(348, 186);
			this.grpDetails.TabIndex = 0;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Entity-Payment Service Association";
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(108, 150);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 2;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtComments
			// 
			this.txtComments.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtComments.Location = new System.Drawing.Point(108, 54);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(228, 60);
			this.txtComments.TabIndex = 1;
			this.txtComments.Text = "";
			this.txtComments.TextChanged += new System.EventHandler(this.ValidateForm);
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
			// mAssociationDS
			// 
			this.mAssociationDS.DataSetName = "CompanyDS";
			this.mAssociationDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgCompanyPaymentService
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
			this.Name = "dlgCompanyPaymentService";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Entity-Payment Service Association Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mPaymentServicesDS)).EndInit();
			this.grpDetails.ResumeLayout(false);
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mAssociationDS)).EndInit();
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
				this.mPaymentServicesDS.Merge(EnterpriseFactory.GetAvailablePaymentServices(this.mEntityID));
				
				//Set control services
				if(this.mPaymentServiceID == 0) {
					//User selects vendor for a new association
					if(this.mAssociationDS.CompanyPaymentServiceTable[0].PaymentServiceID==0) {
						if(this.cboPaymentServices.Items.Count>0) 
							this.cboPaymentServices.SelectedIndex = 0;
					}
					else
						this.cboPaymentServices.SelectedValue = this.mAssociationDS.CompanyPaymentServiceTable[0].PaymentServiceID;
					this.cboPaymentServices.Enabled = (this.cboPaymentServices.Items.Count>0);
				}
				else {
					//Vendor cannot be changed on association updates
					this.mPaymentServicesDS.Clear();
					this.mPaymentServicesDS.SelectionListTable.AddSelectionListTableRow(this.mAssociationDS.CompanyPaymentServiceTable[0].PaymentServiceID.ToString(), "Payment Service");
					this.cboPaymentServices.Enabled = false;
				}
				this.txtComments.MaxLength = 30;
				if(!this.mAssociationDS.CompanyPaymentServiceTable[0].IsCommentsNull())
					this.txtComments.Text = this.mAssociationDS.CompanyPaymentServiceTable[0].Comments;
				
				this.chkStatus.Checked = this.mAssociationDS.CompanyPaymentServiceTable[0].IsActive;
				if(!mParentIsActive) {
					//If parent is inactive: 1. Status MUST be inactive for new
					//					     2. Status cannot be changed for new or existing
					if(this.mPaymentServiceID==0) this.chkStatus.Checked = false;
					this.chkStatus.Enabled = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
				if(this.mAssociationDS.CompanyPaymentServiceTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (this.cboPaymentServices.Text!="");
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
						this.mAssociationDS.CompanyPaymentServiceTable[0].PaymentServiceID = Convert.ToInt32(this.cboPaymentServices.SelectedValue);
						this.mAssociationDS.CompanyPaymentServiceTable[0].PaymentServiceName = this.cboPaymentServices.Text;
						this.mAssociationDS.CompanyPaymentServiceTable[0].Comments = this.txtComments.Text;
						this.mAssociationDS.CompanyPaymentServiceTable[0].IsActive = this.chkStatus.Checked;
						this.mAssociationDS.AcceptChanges();
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
