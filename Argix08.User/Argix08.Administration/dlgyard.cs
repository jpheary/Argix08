//	File:	dlgyard.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new yard or edit an existing yard.
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
	public class dlgYardDetail : System.Windows.Forms.Form {
		//Members
		private int mYardID=0;
		private bool mParentIsActive=true;
		#region Controls
		private System.Windows.Forms.TextBox txtYardName;
		private System.Windows.Forms.Label _lblYardName;
		private System.Windows.Forms.Label lblTerminal;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.Label _lblSectionDescription;
		private System.Windows.Forms.TextBox txtYardDescripton;
		private Tsort.Transportation.YardDS mYardDS;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabGeneral;
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
		public dlgYardDetail(string terminal, bool parentIsActive, ref YardDS yard) {
			//Constructor
			try {
				//Set command button and menu identities (used for onclick handler)
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.lblTerminal.Text = terminal;
				this.mParentIsActive = parentIsActive;
				this.mYardDS = yard;
				if(this.mYardDS.YardDetailTable.Count>0) {
					this.mYardID = this.mYardDS.YardDetailTable[0].YardID;
					this.Text = (this.mYardID>0) ? "Yard (" + this.mYardID + ")" : "Yard (New)";
				}
				else
					this.Text = "Yard (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgYardDetail));
			this._lblYardName = new System.Windows.Forms.Label();
			this.txtYardName = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblTerminal = new System.Windows.Forms.Label();
			this._lblTerminal = new System.Windows.Forms.Label();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.txtYardDescripton = new System.Windows.Forms.TextBox();
			this._lblSectionDescription = new System.Windows.Forms.Label();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.mYardDS = new Tsort.Transportation.YardDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.grpDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mYardDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblYardName
			// 
			this._lblYardName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblYardName.Location = new System.Drawing.Point(6, 24);
			this._lblYardName.Name = "_lblYardName";
			this._lblYardName.Size = new System.Drawing.Size(96, 18);
			this._lblYardName.TabIndex = 3;
			this._lblYardName.Text = "Name";
			this._lblYardName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtYardName
			// 
			this.txtYardName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtYardName.Location = new System.Drawing.Point(108, 24);
			this.txtYardName.Name = "txtYardName";
			this.txtYardName.Size = new System.Drawing.Size(228, 21);
			this.txtYardName.TabIndex = 0;
			this.txtYardName.Text = "";
			this.txtYardName.TextChanged += new System.EventHandler(this.ValidateForm);
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
			// lblTerminal
			// 
			this.lblTerminal.Location = new System.Drawing.Point(114, 12);
			this.lblTerminal.Name = "lblTerminal";
			this.lblTerminal.Size = new System.Drawing.Size(186, 18);
			this.lblTerminal.TabIndex = 2;
			this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// grpDetails
			// 
			this.grpDetails.Controls.Add(this.txtYardDescripton);
			this.grpDetails.Controls.Add(this._lblSectionDescription);
			this.grpDetails.Controls.Add(this.chkStatus);
			this.grpDetails.Controls.Add(this.txtYardName);
			this.grpDetails.Controls.Add(this._lblYardName);
			this.grpDetails.Location = new System.Drawing.Point(6, 72);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(348, 120);
			this.grpDetails.TabIndex = 0;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Yard";
			// 
			// txtYardDescripton
			// 
			this.txtYardDescripton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtYardDescripton.Location = new System.Drawing.Point(108, 48);
			this.txtYardDescripton.Name = "txtYardDescripton";
			this.txtYardDescripton.Size = new System.Drawing.Size(228, 21);
			this.txtYardDescripton.TabIndex = 1;
			this.txtYardDescripton.Text = "";
			this.txtYardDescripton.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblSectionDescription
			// 
			this._lblSectionDescription.Location = new System.Drawing.Point(6, 48);
			this._lblSectionDescription.Name = "_lblSectionDescription";
			this._lblSectionDescription.Size = new System.Drawing.Size(96, 18);
			this._lblSectionDescription.TabIndex = 4;
			this._lblSectionDescription.Text = "Description";
			this._lblSectionDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// mYardDS
			// 
			this.mYardDS.DataSetName = "YardDS";
			this.mYardDS.Locale = new System.Globalization.CultureInfo("en-US");
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
			this.tabGeneral.Controls.Add(this._lblTerminal);
			this.tabGeneral.Controls.Add(this.lblTerminal);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(361, 199);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// dlgYardDetail
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
			this.Name = "dlgYardDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Yard Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.grpDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mYardDS)).EndInit();
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
				
				//Get lists
				//N/A
				
				//Set control services
				this.txtYardName.MaxLength = 30;
				this.txtYardName.Text = "";
				if(!this.mYardDS.YardDetailTable[0].IsNameNull())
					this.txtYardName.Text = this.mYardDS.YardDetailTable[0].Name;
				this.txtYardDescripton.MaxLength = 30;
				this.txtYardDescripton.Text = "";
				if(!this.mYardDS.YardDetailTable[0].IsDescriptionNull())
					this.txtYardDescripton.Text = this.mYardDS.YardDetailTable[0].Description;
				this.txtYardDescripton.Enabled = false;		//No current requirement
				
				this.chkStatus.Checked = this.mYardDS.YardDetailTable[0].IsActive;
				if(!this.mParentIsActive) {
					//If parent is inactive: 1. Status MUST be inactive for new
					//					     2. Status cannot be changed for new or existing
					if(this.mYardID==0) this.chkStatus.Checked = false;
					this.chkStatus.Enabled = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in control values
			try {
				if(this.mYardDS.YardDetailTable.Count>0) {
					//Enable OK service if appointment details have valid changes
					this.btnOk.Enabled = (this.txtYardName.Text!="");
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
						this.mYardDS.YardDetailTable[0].Name = this.txtYardName.Text;
						if(this.txtYardDescripton.Text!="")
							this.mYardDS.YardDetailTable[0].Description = this.txtYardDescripton.Text;
						else
							this.mYardDS.YardDetailTable[0].SetDescriptionNull();
						this.mYardDS.YardDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mYardDS.AcceptChanges();
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
