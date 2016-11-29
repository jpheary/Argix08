//	File:	dlgyardsection.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new yard section or edit an existing yard section.
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
	public class dlgYardSectionDetail : System.Windows.Forms.Form {
		//Members
		private int mYardSectionID=0;
		private bool mParentIsActive=true;
		#region Controls
		private System.Windows.Forms.TextBox txtSectionDescripton;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.Label _lblYard;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label _lblSectionID;
		private System.Windows.Forms.Label _lblSectionDescription;
		private System.Windows.Forms.TextBox txtSection;
		private System.Windows.Forms.Label lblTerminal;
		private System.Windows.Forms.Label lblYard;
		private System.Windows.Forms.GroupBox grpDetails;
		private Tsort.Transportation.YardSectionDS mYardSectionDS;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox chkStatus;
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
		public dlgYardSectionDetail(string terminalName, string yardName, bool parentIsActive, ref YardSectionDS yardSection) {
			//Constructor
			try {
				//
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.lblTerminal.Text = terminalName;
				this.lblYard.Text = yardName;
				this.mParentIsActive = parentIsActive;
				this.mYardSectionDS = yardSection;
				if(this.mYardSectionDS.YardSectionDetailTable.Count > 0) {
					this.mYardSectionID = this.mYardSectionDS.YardSectionDetailTable[0].SectionID;
					this.Text = (this.mYardSectionID>0) ? "Yard Section (" + this.mYardSectionID + ")" : "Yard Section (New)";
				}
				else
					this.Text = "Yard Section (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgYardSectionDetail));
			this._lblTerminal = new System.Windows.Forms.Label();
			this._lblYard = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this._lblSectionID = new System.Windows.Forms.Label();
			this._lblSectionDescription = new System.Windows.Forms.Label();
			this.txtSection = new System.Windows.Forms.TextBox();
			this.txtSectionDescripton = new System.Windows.Forms.TextBox();
			this.lblTerminal = new System.Windows.Forms.Label();
			this.lblYard = new System.Windows.Forms.Label();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this.mYardSectionDS = new Tsort.Transportation.YardSectionDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.grpDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mYardSectionDS)).BeginInit();
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
			// _lblSectionID
			// 
			this._lblSectionID.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblSectionID.Location = new System.Drawing.Point(6, 24);
			this._lblSectionID.Name = "_lblSectionID";
			this._lblSectionID.Size = new System.Drawing.Size(96, 18);
			this._lblSectionID.TabIndex = 3;
			this._lblSectionID.Text = "ID";
			this._lblSectionID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// txtSection
			// 
			this.txtSection.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtSection.Location = new System.Drawing.Point(108, 24);
			this.txtSection.Name = "txtSection";
			this.txtSection.Size = new System.Drawing.Size(72, 21);
			this.txtSection.TabIndex = 0;
			this.txtSection.Text = "";
			this.txtSection.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtSectionDescripton
			// 
			this.txtSectionDescripton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtSectionDescripton.Location = new System.Drawing.Point(108, 48);
			this.txtSectionDescripton.Name = "txtSectionDescripton";
			this.txtSectionDescripton.Size = new System.Drawing.Size(228, 21);
			this.txtSectionDescripton.TabIndex = 1;
			this.txtSectionDescripton.Text = "";
			this.txtSectionDescripton.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// lblTerminal
			// 
			this.lblTerminal.Location = new System.Drawing.Point(114, 12);
			this.lblTerminal.Name = "lblTerminal";
			this.lblTerminal.Size = new System.Drawing.Size(186, 18);
			this.lblTerminal.TabIndex = 2;
			this.lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblYard
			// 
			this.lblYard.Location = new System.Drawing.Point(114, 30);
			this.lblYard.Name = "lblYard";
			this.lblYard.Size = new System.Drawing.Size(186, 18);
			this.lblYard.TabIndex = 4;
			this.lblYard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpDetails
			// 
			this.grpDetails.Controls.Add(this.chkStatus);
			this.grpDetails.Controls.Add(this.txtSection);
			this.grpDetails.Controls.Add(this.txtSectionDescripton);
			this.grpDetails.Controls.Add(this._lblSectionID);
			this.grpDetails.Controls.Add(this._lblSectionDescription);
			this.grpDetails.Location = new System.Drawing.Point(6, 72);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(348, 120);
			this.grpDetails.TabIndex = 0;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Yard Section";
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
			// mYardSectionDS
			// 
			this.mYardSectionDS.DataSetName = "YardSectionDS";
			this.mYardSectionDS.Locale = new System.Globalization.CultureInfo("en-US");
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
			this.tabGeneral.Controls.Add(this._lblTerminal);
			this.tabGeneral.Controls.Add(this._lblYard);
			this.tabGeneral.Controls.Add(this.lblYard);
			this.tabGeneral.Controls.Add(this.grpDetails);
			this.tabGeneral.Controls.Add(this.lblTerminal);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(361, 199);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// dlgYardSectionDetail
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
			this.Name = "dlgYardSectionDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Yard Section Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.grpDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mYardSectionDS)).EndInit();
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
								
				//Get lists
				//N/A
				
				//Set control services
				this.txtSection.MaxLength = 5;
				this.txtSection.Text = this.mYardSectionDS.YardSectionDetailTable[0].SectionNumber;
				this.txtSectionDescripton.MaxLength = 30;
				this.txtSectionDescripton.Text = "";
				if(!this.mYardSectionDS.YardSectionDetailTable[0].IsDescriptionNull())
					this.txtSectionDescripton.Text = this.mYardSectionDS.YardSectionDetailTable[0].Description;
				
				this.chkStatus.Checked = this.mYardSectionDS.YardSectionDetailTable[0].IsActive;
				if(!this.mParentIsActive) {
					//If parent is inactive: 1. Status MUST be inactive for new
					//					     2. Status cannot be changed for new or existing
					if(this.mYardSectionID==0) this.chkStatus.Checked = false;
					this.chkStatus.Enabled = false;
				}
				
				//Reset
				this.btnOk.Enabled = false;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in control values
			try {
				if(this.mYardSectionDS.YardSectionDetailTable.Count>0) {
					//Enable OK service if details have valid value
					this.btnOk.Enabled = (this.txtSection.Text!="");
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
						this.mYardSectionDS.YardSectionDetailTable[0].SectionNumber = this.txtSection.Text;
						if(this.txtSectionDescripton.Text!="")
							this.mYardSectionDS.YardSectionDetailTable[0].Description = this.txtSectionDescripton.Text;
						else
							this.mYardSectionDS.YardSectionDetailTable[0].SetDescriptionNull();
						this.mYardSectionDS.YardSectionDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mYardSectionDS.AcceptChanges();
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
