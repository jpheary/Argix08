//	File:	dlgvehicle.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new vehicle vehicle or edit an existing vehicle vehicle.
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
	public class dlgVehicleDetail : System.Windows.Forms.Form {
		//Members
		private int mVehicleID=0;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboDrivers;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.TextBox txtVehicle;
		private System.Windows.Forms.TextBox txtLicense;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.Label _lblDriver;
		private System.Windows.Forms.Label _lblLicense;
		private System.Windows.Forms.Label _lblVehicle;
		private System.Windows.Forms.Label _lblType;
		private System.Windows.Forms.ComboBox cboTypes;
		private System.Windows.Forms.ComboBox cboStates;
		private System.Windows.Forms.Label _lblState;
		private Tsort.Transportation.VehicleDS mVehicleDS;
		private Tsort.Enterprise.StateDS mStatesDS;
		private Tsort.Transportation.VehicleTypeDS mTypesDS;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private Tsort.Windows.SelectionList mDriversDS;
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
		public dlgVehicleDetail(ref VehicleDS vehicle) {
			//Constructor
			try {
				//
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mVehicleDS = vehicle;
				if(this.mVehicleDS.VehicleDetailTable.Count>0) {
					this.mVehicleID = this.mVehicleDS.VehicleDetailTable[0].VehicleID;
					this.Text = (this.mVehicleID>0) ? "Driver Vehicle (" + this.mVehicleID + ")" : "Driver Vehicle (New)";
				}
				else
					this.Text = "Driver Vehicle (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgVehicleDetail));
			this.cboDrivers = new System.Windows.Forms.ComboBox();
			this.mDriversDS = new Tsort.Windows.SelectionList();
			this._lblDriver = new System.Windows.Forms.Label();
			this._lblLicense = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this._lblState = new System.Windows.Forms.Label();
			this.cboStates = new System.Windows.Forms.ComboBox();
			this.mStatesDS = new Tsort.Enterprise.StateDS();
			this._lblType = new System.Windows.Forms.Label();
			this.cboTypes = new System.Windows.Forms.ComboBox();
			this.mTypesDS = new Tsort.Transportation.VehicleTypeDS();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this._lblVehicle = new System.Windows.Forms.Label();
			this.txtVehicle = new System.Windows.Forms.TextBox();
			this.txtLicense = new System.Windows.Forms.TextBox();
			this.mVehicleDS = new Tsort.Transportation.VehicleDS();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.mDriversDS)).BeginInit();
			this.grpDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTypesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mVehicleDS)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// cboDrivers
			// 
			this.cboDrivers.DataSource = this.mDriversDS;
			this.cboDrivers.DisplayMember = "SelectionListTable.Description";
			this.cboDrivers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDrivers.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboDrivers.Location = new System.Drawing.Point(108, 114);
			this.cboDrivers.Name = "cboDrivers";
			this.cboDrivers.Size = new System.Drawing.Size(228, 21);
			this.cboDrivers.TabIndex = 4;
			this.cboDrivers.ValueMember = "SelectionListTable.ID";
			this.cboDrivers.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mDriversDS
			// 
			this.mDriversDS.DataSetName = "SelectionList";
			this.mDriversDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblDriver
			// 
			this._lblDriver.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDriver.Location = new System.Drawing.Point(6, 114);
			this._lblDriver.Name = "_lblDriver";
			this._lblDriver.Size = new System.Drawing.Size(96, 18);
			this._lblDriver.TabIndex = 2;
			this._lblDriver.Text = "Driver";
			this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblLicense
			// 
			this._lblLicense.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLicense.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLicense.Location = new System.Drawing.Point(6, 84);
			this._lblLicense.Name = "_lblLicense";
			this._lblLicense.Size = new System.Drawing.Size(96, 18);
			this._lblLicense.TabIndex = 13;
			this._lblLicense.Text = "License #";
			this._lblLicense.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.grpDetails.Controls.Add(this._lblState);
			this.grpDetails.Controls.Add(this.cboStates);
			this.grpDetails.Controls.Add(this._lblType);
			this.grpDetails.Controls.Add(this.cboTypes);
			this.grpDetails.Controls.Add(this.chkStatus);
			this.grpDetails.Controls.Add(this._lblDriver);
			this.grpDetails.Controls.Add(this.cboDrivers);
			this.grpDetails.Controls.Add(this._lblVehicle);
			this.grpDetails.Controls.Add(this.txtVehicle);
			this.grpDetails.Controls.Add(this._lblLicense);
			this.grpDetails.Controls.Add(this.txtLicense);
			this.grpDetails.Location = new System.Drawing.Point(6, 6);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(348, 186);
			this.grpDetails.TabIndex = 0;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Vehicle";
			// 
			// _lblState
			// 
			this._lblState.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblState.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblState.Location = new System.Drawing.Point(228, 84);
			this._lblState.Name = "_lblState";
			this._lblState.Size = new System.Drawing.Size(48, 16);
			this._lblState.TabIndex = 24;
			this._lblState.Text = "State";
			this._lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboStates
			// 
			this.cboStates.DataSource = this.mStatesDS;
			this.cboStates.DisplayMember = "StateListTable.STATE";
			this.cboStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboStates.Location = new System.Drawing.Point(282, 84);
			this.cboStates.Name = "cboStates";
			this.cboStates.Size = new System.Drawing.Size(54, 21);
			this.cboStates.TabIndex = 3;
			this.cboStates.ValueMember = "StateListTable.STATE";
			this.cboStates.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mStatesDS
			// 
			this.mStatesDS.DataSetName = "StateDS";
			this.mStatesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblType
			// 
			this._lblType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblType.Location = new System.Drawing.Point(6, 54);
			this._lblType.Name = "_lblType";
			this._lblType.Size = new System.Drawing.Size(96, 18);
			this._lblType.TabIndex = 21;
			this._lblType.Text = "Type";
			this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboTypes
			// 
			this.cboTypes.DataSource = this.mTypesDS;
			this.cboTypes.DisplayMember = "VehicleTypeListTable.VehicleType";
			this.cboTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTypes.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboTypes.Location = new System.Drawing.Point(108, 54);
			this.cboTypes.Name = "cboTypes";
			this.cboTypes.Size = new System.Drawing.Size(144, 21);
			this.cboTypes.TabIndex = 1;
			this.cboTypes.ValueMember = "VehicleTypeListTable.VehicleType";
			this.cboTypes.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mTypesDS
			// 
			this.mTypesDS.DataSetName = "VehicleTypeDS";
			this.mTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(108, 156);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 21);
			this.chkStatus.TabIndex = 5;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblVehicle
			// 
			this._lblVehicle.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblVehicle.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblVehicle.Location = new System.Drawing.Point(6, 24);
			this._lblVehicle.Name = "_lblVehicle";
			this._lblVehicle.Size = new System.Drawing.Size(96, 18);
			this._lblVehicle.TabIndex = 15;
			this._lblVehicle.Text = "Description";
			this._lblVehicle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtVehicle
			// 
			this.txtVehicle.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtVehicle.Location = new System.Drawing.Point(108, 24);
			this.txtVehicle.Name = "txtVehicle";
			this.txtVehicle.Size = new System.Drawing.Size(228, 21);
			this.txtVehicle.TabIndex = 0;
			this.txtVehicle.Text = "";
			this.txtVehicle.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtLicense
			// 
			this.txtLicense.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLicense.Location = new System.Drawing.Point(108, 84);
			this.txtLicense.Name = "txtLicense";
			this.txtLicense.Size = new System.Drawing.Size(72, 21);
			this.txtLicense.TabIndex = 2;
			this.txtLicense.Text = "";
			this.txtLicense.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mVehicleDS
			// 
			this.mVehicleDS.DataSetName = "VehicleDS";
			this.mVehicleDS.Locale = new System.Globalization.CultureInfo("en-US");
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
			// dlgVehicleDetail
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
			this.Name = "dlgVehicleDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Driver Vehicle Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mDriversDS)).EndInit();
			this.grpDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTypesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mVehicleDS)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set inital services
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mTypesDS.Merge(TransportationFactory.GetVehicleTypes());
				this.mStatesDS.Merge(EnterpriseFactory.GetStates());
				this.mDriversDS.Merge(TransportationFactory.GetDrivers());
				
				//Set control services
				this.txtVehicle.MaxLength = 30;
				this.txtVehicle.Text = "";
				if(!this.mVehicleDS.VehicleDetailTable[0].IsDescriptionNull())
					this.txtVehicle.Text = this.mVehicleDS.VehicleDetailTable[0].Description;
				if(this.mVehicleDS.VehicleDetailTable[0].VehicleType!="") 
					this.cboTypes.SelectedValue = this.mVehicleDS.VehicleDetailTable[0].VehicleType;
				else
					if(this.cboTypes.Items.Count>0) this.cboTypes.SelectedIndex = 0;
				this.cboTypes.Enabled = (this.cboTypes.Items.Count>0);
				this.txtLicense.MaxLength = 8;
				this.txtLicense.Text = "";
				if(!this.mVehicleDS.VehicleDetailTable[0].IsLicPlateNumberNull())
					this.txtLicense.Text = this.mVehicleDS.VehicleDetailTable[0].LicPlateNumber;
				if(!this.mVehicleDS.VehicleDetailTable[0].IsStateNull()) 
					this.cboStates.SelectedValue = this.mVehicleDS.VehicleDetailTable[0].State;
				else
					if(this.cboStates.Items.Count>0) this.cboStates.SelectedIndex = 0;
				this.cboStates.Enabled = (this.cboStates.Items.Count>0);
				if(!this.mVehicleDS.VehicleDetailTable[0].IsDriverIDNull()) 
					this.cboDrivers.SelectedValue = this.mVehicleDS.VehicleDetailTable[0].DriverID;
				else
					if(this.cboDrivers.Items.Count>0) this.cboDrivers.SelectedIndex = 0;
				this.cboDrivers.Enabled = (this.cboDrivers.Items.Count>0);				
				this.chkStatus.Checked = this.mVehicleDS.VehicleDetailTable[0].IsActive;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in control data
			try {
				if(this.mVehicleDS.VehicleDetailTable.Count>0) {
					//Enable OK service if appointment details have valid changes
					this.btnOk.Enabled = (	this.txtVehicle.Text!="" && this.cboTypes.Text!="" && 
											this.txtLicense.Text!="" && this.cboStates.Text!="" &&
											this.cboDrivers.Text!="");
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
						this.mVehicleDS.VehicleDetailTable[0].Description = this.txtVehicle.Text;
						this.mVehicleDS.VehicleDetailTable[0].VehicleType = Convert.ToString(this.cboTypes.SelectedValue);
						this.mVehicleDS.VehicleDetailTable[0].LicPlateNumber = this.txtLicense.Text;
						this.mVehicleDS.VehicleDetailTable[0].State = Convert.ToString(this.cboStates.SelectedValue);
						this.mVehicleDS.VehicleDetailTable[0].DriverID = Convert.ToInt32(this.cboDrivers.SelectedValue);
						this.mVehicleDS.VehicleDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mVehicleDS.AcceptChanges();
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
