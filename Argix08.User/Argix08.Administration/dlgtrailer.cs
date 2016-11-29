//	File:	dlgtrailer.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	Dialog to create a new LTA Trailer or edit an existing LTA Trailer.
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
	public class dlgTrailerDetail : System.Windows.Forms.Form {
		//Members
		private int mTrailerID=0;
		#region Controls
		private System.Windows.Forms.TextBox txtTrailerNumber;
		private System.Windows.Forms.ComboBox cboTrailerOwner;
		private System.Windows.Forms.ComboBox cboTrailerType;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.Label _lblTrailerNumber;
		private System.Windows.Forms.Label _lblTrailerOwner;
		private System.Windows.Forms.Label _lblTrailerType;
		private Tsort.Transportation.TrailerDS mTrailerDS;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.CheckBox chkStorage;
		private Tsort.Transportation.CarrierDS mCarriersDS;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private Tsort.Windows.SelectionList mTrailerDefinitionsDS;
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
		public dlgTrailerDetail(ref TrailerDS trailer) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mTrailerDS = trailer;
				if(this.mTrailerDS.TrailerDetailTable.Count>0) {
					this.mTrailerID = this.mTrailerDS.TrailerDetailTable[0].TrailerID;
					this.Text = (this.mTrailerID>0) ? "Trailer (" + this.mTrailerID + ")" : "Trailer (New)";
				}
				else
					this.Text = "Trailer (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgTrailerDetail));
			this.txtTrailerNumber = new System.Windows.Forms.TextBox();
			this.mTrailerDS = new Tsort.Transportation.TrailerDS();
			this._lblTrailerNumber = new System.Windows.Forms.Label();
			this._lblTrailerOwner = new System.Windows.Forms.Label();
			this.cboTrailerOwner = new System.Windows.Forms.ComboBox();
			this.mCarriersDS = new Tsort.Transportation.CarrierDS();
			this.cboTrailerType = new System.Windows.Forms.ComboBox();
			this.mTrailerDefinitionsDS = new Tsort.Windows.SelectionList();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this._lblTrailerType = new System.Windows.Forms.Label();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this.chkStorage = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.mTrailerDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCarriersDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTrailerDefinitionsDS)).BeginInit();
			this.grpDetails.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtTrailerNumber
			// 
			this.txtTrailerNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtTrailerNumber.Location = new System.Drawing.Point(108, 24);
			this.txtTrailerNumber.Name = "txtTrailerNumber";
			this.txtTrailerNumber.Size = new System.Drawing.Size(174, 21);
			this.txtTrailerNumber.TabIndex = 0;
			this.txtTrailerNumber.Text = "";
			this.txtTrailerNumber.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mTrailerDS
			// 
			this.mTrailerDS.DataSetName = "TrailerDS";
			this.mTrailerDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblTrailerNumber
			// 
			this._lblTrailerNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTrailerNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblTrailerNumber.Location = new System.Drawing.Point(6, 24);
			this._lblTrailerNumber.Name = "_lblTrailerNumber";
			this._lblTrailerNumber.Size = new System.Drawing.Size(96, 18);
			this._lblTrailerNumber.TabIndex = 1;
			this._lblTrailerNumber.Text = "Number";
			this._lblTrailerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblTrailerOwner
			// 
			this._lblTrailerOwner.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTrailerOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblTrailerOwner.Location = new System.Drawing.Point(6, 84);
			this._lblTrailerOwner.Name = "_lblTrailerOwner";
			this._lblTrailerOwner.Size = new System.Drawing.Size(96, 18);
			this._lblTrailerOwner.TabIndex = 3;
			this._lblTrailerOwner.Text = "Owner";
			this._lblTrailerOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboTrailerOwner
			// 
			this.cboTrailerOwner.DataSource = this.mCarriersDS;
			this.cboTrailerOwner.DisplayMember = "CarrierListTable.CarrierName";
			this.cboTrailerOwner.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTrailerOwner.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboTrailerOwner.Location = new System.Drawing.Point(108, 84);
			this.cboTrailerOwner.Name = "cboTrailerOwner";
			this.cboTrailerOwner.Size = new System.Drawing.Size(174, 21);
			this.cboTrailerOwner.TabIndex = 2;
			this.cboTrailerOwner.ValueMember = "CarrierListTable.CarrierID";
			this.cboTrailerOwner.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mCarriersDS
			// 
			this.mCarriersDS.DataSetName = "CarrierDS";
			this.mCarriersDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// cboTrailerType
			// 
			this.cboTrailerType.DataSource = this.mTrailerDefinitionsDS;
			this.cboTrailerType.DisplayMember = "SelectionListTable.Description";
			this.cboTrailerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTrailerType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboTrailerType.Location = new System.Drawing.Point(108, 54);
			this.cboTrailerType.Name = "cboTrailerType";
			this.cboTrailerType.Size = new System.Drawing.Size(174, 21);
			this.cboTrailerType.TabIndex = 1;
			this.cboTrailerType.ValueMember = "SelectionListTable.ID";
			this.cboTrailerType.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mTrailerDefinitionsDS
			// 
			this.mTrailerDefinitionsDS.DataSetName = "SelectionList";
			this.mTrailerDefinitionsDS.Locale = new System.Globalization.CultureInfo("en-US");
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
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(108, 156);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 4;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblTrailerType
			// 
			this._lblTrailerType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTrailerType.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblTrailerType.Location = new System.Drawing.Point(6, 54);
			this._lblTrailerType.Name = "_lblTrailerType";
			this._lblTrailerType.Size = new System.Drawing.Size(96, 18);
			this._lblTrailerType.TabIndex = 18;
			this._lblTrailerType.Text = "Definition";
			this._lblTrailerType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grpDetails
			// 
			this.grpDetails.Controls.Add(this.chkStorage);
			this.grpDetails.Controls.Add(this._lblTrailerNumber);
			this.grpDetails.Controls.Add(this.txtTrailerNumber);
			this.grpDetails.Controls.Add(this._lblTrailerOwner);
			this.grpDetails.Controls.Add(this._lblTrailerType);
			this.grpDetails.Controls.Add(this.cboTrailerOwner);
			this.grpDetails.Controls.Add(this.cboTrailerType);
			this.grpDetails.Controls.Add(this.chkStatus);
			this.grpDetails.Location = new System.Drawing.Point(6, 6);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(348, 186);
			this.grpDetails.TabIndex = 0;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Trailer";
			// 
			// chkStorage
			// 
			this.chkStorage.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStorage.Location = new System.Drawing.Point(108, 114);
			this.chkStorage.Name = "chkStorage";
			this.chkStorage.Size = new System.Drawing.Size(96, 18);
			this.chkStorage.TabIndex = 3;
			this.chkStorage.Text = "Storage";
			this.chkStorage.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(369, 225);
			this.tabControl1.TabIndex = 1;
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
			// dlgTrailerDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(378, 263);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgTrailerDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Trailer Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mTrailerDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCarriersDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTrailerDefinitionsDS)).EndInit();
			this.grpDetails.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
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
				this.mCarriersDS.Merge(TransportationFactory.GetTrailerOwners());
				this.mTrailerDefinitionsDS.Merge(TransportationFactory.GetTrailerTypes());
				
				//Set control services
				this.txtTrailerNumber.MaxLength = 15;
				this.txtTrailerNumber.Text = "";
				if(!this.mTrailerDS.TrailerDetailTable[0].IsNumberNull())
					this.txtTrailerNumber.Text = this.mTrailerDS.TrailerDetailTable[0].Number.Trim();
				if(!this.mTrailerDS.TrailerDetailTable[0].IsCarrierIDNull()) 
					this.cboTrailerOwner.SelectedValue = this.mTrailerDS.TrailerDetailTable[0].CarrierID;
				else
					if(this.cboTrailerOwner.Items.Count>0) this.cboTrailerOwner.SelectedIndex = 0;
				this.cboTrailerOwner.Enabled = (this.cboTrailerOwner.Items.Count>0);
				if(!this.mTrailerDS.TrailerDetailTable[0].IsDefinitionIDNull()) 
					this.cboTrailerType.SelectedValue = this.mTrailerDS.TrailerDetailTable[0].DefinitionID;
				else
					if(this.cboTrailerType.Items.Count>0) this.cboTrailerType.SelectedIndex = 0;
				this.cboTrailerType.Enabled = (this.cboTrailerType.Items.Count>0);
				this.chkStorage.Checked = this.mTrailerDS.TrailerDetailTable[0].IsStorage;
				this.chkStatus.Checked = this.mTrailerDS.TrailerDetailTable[0].IsActive;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
				if(this.mTrailerDS.TrailerDetailTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtTrailerNumber.Text!="" && this.cboTrailerOwner.Text!="" && 
											this.cboTrailerType.Text!="");
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
						this.mTrailerDS.TrailerDetailTable[0].Number = this.txtTrailerNumber.Text;
						this.mTrailerDS.TrailerDetailTable[0].CarrierID = Convert.ToInt32(this.cboTrailerOwner.SelectedValue);
						this.mTrailerDS.TrailerDetailTable[0].DefinitionID = Convert.ToInt32(this.cboTrailerType.SelectedValue);
						this.mTrailerDS.TrailerDetailTable[0].IsStorage = this.chkStorage.Checked;
						this.mTrailerDS.TrailerDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mTrailerDS.AcceptChanges();
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
