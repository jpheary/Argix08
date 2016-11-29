//	File:	dlgyardlocation.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new yard location or edit an existing yard location.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Tsort.Transportation;

namespace Tsort {
	//
	public class dlgYardLocationDetail : System.Windows.Forms.Form {
		//Members
		private int mYardLocationID=0;
		private bool mParentIsActive=true;
		#region Controls

		private System.Windows.Forms.Label lblTerminal;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.Label _lblYard;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label _lblYardSection;
		private System.Windows.Forms.Label _lblLocationID;
		private System.Windows.Forms.Label _lblLocationType;
		private System.Windows.Forms.Label lblYardSection;
		private System.Windows.Forms.Label lblYard;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.Button btnOk;
		private Tsort.Transportation.YardLocationDS mYardLocationDS;
		private System.Windows.Forms.ComboBox cboLocationType;
		private System.Windows.Forms.CheckBox chkStatus;
		private Tsort.Transportation.YardLocationTypeDS mYardLocationTypesDS;
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
		public dlgYardLocationDetail(string terminalName, string yardName, string sectionName, bool parentIsActive, ref YardLocationDS yardLocation) {
			//Constructor
			try {
				//
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set yardLocation data and titlebar caption
				this.lblTerminal.Text = terminalName;
				this.lblYard.Text = yardName;
				this.lblYardSection.Text = sectionName;
				this.mParentIsActive = parentIsActive;
				this.mYardLocationDS = yardLocation;
				if(this.mYardLocationDS.YardLocationDetailTable.Count > 0) {
					this.mYardLocationID = this.mYardLocationDS.YardLocationDetailTable[0].YardLocationID;
					this.Text = (this.mYardLocationID>0) ? "Yard Location (" + this.mYardLocationID + ")" : "Yard Location (New)";
				}
				else
					this.Text = "Yard Location (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgYardLocationDetail));
			this._lblTerminal = new System.Windows.Forms.Label();
			this._lblYard = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this._lblYardSection = new System.Windows.Forms.Label();
			this._lblLocationID = new System.Windows.Forms.Label();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.mYardLocationDS = new Tsort.Transportation.YardLocationDS();
			this._lblLocationType = new System.Windows.Forms.Label();
			this.lblYardSection = new System.Windows.Forms.Label();
			this.lblYard = new System.Windows.Forms.Label();
			this.lblTerminal = new System.Windows.Forms.Label();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.cboLocationType = new System.Windows.Forms.ComboBox();
			this.mYardLocationTypesDS = new Tsort.Transportation.YardLocationTypeDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.mYardLocationDS)).BeginInit();
			this.grpDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mYardLocationTypesDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblTerminal
			// 
			this._lblTerminal.Location = new System.Drawing.Point(12, 12);
			this._lblTerminal.Name = "_lblTerminal";
			this._lblTerminal.Size = new System.Drawing.Size(96, 18);
			this._lblTerminal.TabIndex = 1;
			this._lblTerminal.Text = "Terminal: ";
			this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblYard
			// 
			this._lblYard.Location = new System.Drawing.Point(12, 30);
			this._lblYard.Name = "_lblYard";
			this._lblYard.Size = new System.Drawing.Size(96, 18);
			this._lblYard.TabIndex = 3;
			this._lblYard.Text = "Yard: ";
			this._lblYard.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// _lblYardSection
			// 
			this._lblYardSection.Location = new System.Drawing.Point(12, 48);
			this._lblYardSection.Name = "_lblYardSection";
			this._lblYardSection.Size = new System.Drawing.Size(96, 18);
			this._lblYardSection.TabIndex = 5;
			this._lblYardSection.Text = "Section: ";
			this._lblYardSection.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblLocationID
			// 
			this._lblLocationID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLocationID.Location = new System.Drawing.Point(6, 24);
			this._lblLocationID.Name = "_lblLocationID";
			this._lblLocationID.Size = new System.Drawing.Size(96, 18);
			this._lblLocationID.TabIndex = 3;
			this._lblLocationID.Text = "ID";
			this._lblLocationID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLocation
			// 
			this.txtLocation.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLocation.Location = new System.Drawing.Point(108, 24);
			this.txtLocation.MaxLength = 5;
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(72, 21);
			this.txtLocation.TabIndex = 0;
			this.txtLocation.Text = "";
			this.txtLocation.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mYardLocationDS
			// 
			this.mYardLocationDS.DataSetName = "YardLocationDS";
			this.mYardLocationDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblLocationType
			// 
			this._lblLocationType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLocationType.Location = new System.Drawing.Point(6, 48);
			this._lblLocationType.Name = "_lblLocationType";
			this._lblLocationType.Size = new System.Drawing.Size(96, 16);
			this._lblLocationType.TabIndex = 4;
			this._lblLocationType.Text = "Type";
			this._lblLocationType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblYardSection
			// 
			this.lblYardSection.Location = new System.Drawing.Point(114, 48);
			this.lblYardSection.Name = "lblYardSection";
			this.lblYardSection.Size = new System.Drawing.Size(48, 18);
			this.lblYardSection.TabIndex = 6;
			this.lblYardSection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblYard
			// 
			this.lblYard.Location = new System.Drawing.Point(114, 30);
			this.lblYard.Name = "lblYard";
			this.lblYard.Size = new System.Drawing.Size(186, 18);
			this.lblYard.TabIndex = 4;
			this.lblYard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblTerminal
			// 
			this.lblTerminal.Location = new System.Drawing.Point(114, 12);
			this.lblTerminal.Name = "lblTerminal";
			this.lblTerminal.Size = new System.Drawing.Size(186, 18);
			this.lblTerminal.TabIndex = 2;
			this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpDetails
			// 
			this.grpDetails.Controls.Add(this.chkStatus);
			this.grpDetails.Controls.Add(this.cboLocationType);
			this.grpDetails.Controls.Add(this.txtLocation);
			this.grpDetails.Controls.Add(this._lblLocationID);
			this.grpDetails.Controls.Add(this._lblLocationType);
			this.grpDetails.Location = new System.Drawing.Point(6, 72);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(348, 120);
			this.grpDetails.TabIndex = 0;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Yard Location";
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(108, 90);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 2;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// cboLocationType
			// 
			this.cboLocationType.DataSource = this.mYardLocationTypesDS;
			this.cboLocationType.DisplayMember = "YardLocationTypeListTable.Description";
			this.cboLocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLocationType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboLocationType.Location = new System.Drawing.Point(108, 48);
			this.cboLocationType.Name = "cboLocationType";
			this.cboLocationType.Size = new System.Drawing.Size(168, 21);
			this.cboLocationType.TabIndex = 1;
			this.cboLocationType.ValueMember = "YardLocationTypeListTable.LocationTypeID";
			this.cboLocationType.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			this.cboLocationType.ValueMemberChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mYardLocationTypesDS
			// 
			this.mYardLocationTypesDS.DataSetName = "YardLocationTypeDS";
			this.mYardLocationTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
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
			this.tabGeneral.Controls.Add(this.lblYard);
			this.tabGeneral.Controls.Add(this.grpDetails);
			this.tabGeneral.Controls.Add(this._lblYard);
			this.tabGeneral.Controls.Add(this._lblTerminal);
			this.tabGeneral.Controls.Add(this.lblYardSection);
			this.tabGeneral.Controls.Add(this.lblTerminal);
			this.tabGeneral.Controls.Add(this._lblYardSection);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(361, 199);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// dlgYardLocationDetail
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
			this.Name = "dlgYardLocationDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Yard Location Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mYardLocationDS)).EndInit();
			this.grpDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mYardLocationTypesDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial services
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mYardLocationTypesDS.Merge(TransportationFactory.GetYardLocationTypes());
				
				//Set control services
				this.txtLocation.MaxLength = 5;
				this.txtLocation.Text = this.mYardLocationDS.YardLocationDetailTable[0].Number;
				if(this.mYardLocationDS.YardLocationDetailTable[0].LocationTypeID>0) 
					this.cboLocationType.SelectedValue = this.mYardLocationDS.YardLocationDetailTable[0].LocationTypeID;
				else
					if(this.cboLocationType.Items.Count>0) this.cboLocationType.SelectedIndex = 0;
				this.cboLocationType.Enabled = (this.cboLocationType.Items.Count>0);
				
				this.chkStatus.Checked = this.mYardLocationDS.YardLocationDetailTable[0].IsActive;
				if(!this.mParentIsActive) {
					//If parent is inactive: 1. Status MUST be inactive for new
					//					     2. Status cannot be changed for new or existing
					if(this.mYardLocationID==0) this.chkStatus.Checked = false;
					this.chkStatus.Enabled = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in control; values
			try {
				if(this.mYardLocationDS.YardLocationDetailTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (this.txtLocation.Text!="" && this.cboLocationType.Text!="");
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
						this.mYardLocationDS.YardLocationDetailTable[0].Number = this.txtLocation.Text;
						this.mYardLocationDS.YardLocationDetailTable[0].LocationTypeID = Convert.ToInt16(this.cboLocationType.SelectedValue);
						this.mYardLocationDS.YardLocationDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mYardLocationDS.AcceptChanges();
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
