//	File:	dlgcarrier.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new carrier or edit an existing carrier.
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
	public class dlgCarrierDetail : System.Windows.Forms.Form {
		//Members
		private int mCarrierID = 0;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit mskPhone;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.Label _lblName;
		private System.Windows.Forms.Label _lblNumber;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label _lblFax;
		private System.Windows.Forms.Label lblPhone;
		private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit mskFax;
		private System.Windows.Forms.Label _lblContact;
		private System.Windows.Forms.TextBox txtContact;
		private System.Windows.Forms.TextBox txtNumber;
		private System.Windows.Forms.Label _lblExt;
		private System.Windows.Forms.TextBox txtExt;
		private System.Windows.Forms.GroupBox fraAddress;
		private System.Windows.Forms.Label _lblZip;
		private System.Windows.Forms.TextBox txtZip;
		private System.Windows.Forms.Label _lblCity;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.Label _lblState;
		private System.Windows.Forms.ComboBox cboStates;
		private System.Windows.Forms.Label _lblLine2;
		private System.Windows.Forms.TextBox txtLine2;
		private System.Windows.Forms.Label _lblLine1;
		private System.Windows.Forms.TextBox txtLine1;
		private Tsort.Transportation.CarrierDS mCarriersDS;
		private System.Windows.Forms.Label _lbleMail;
		private System.Windows.Forms.TextBox txteMail;
		private Tsort.Enterprise.StateDS mStatesDS;
		private System.Windows.Forms.Label _lblCountry;
		private System.Windows.Forms.ComboBox cboCountries;
		private Tsort.Enterprise.CountryDS mCountriesDS;
		private System.Windows.Forms.CheckBox chkCtlDrivers;
		private System.Windows.Forms.CheckBox chkCtlTrailers;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabTrans;
		private System.Windows.Forms.GroupBox fraControl;
		private System.Windows.Forms.Label _lblControl;
		private System.Windows.Forms.GroupBox fraContact;
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
		public dlgCarrierDetail(ref CarrierDS carrier) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mCarriersDS = carrier;
				if(this.mCarriersDS.CarrierDetailTable.Count>0) {
					this.mCarrierID = this.mCarriersDS.CarrierDetailTable[0].CarrierID;
					this.Text = (this.mCarrierID > 0) ? "Carrier (" + this.mCarrierID + ")" : "Carrier (New)";
				}
				else
					this.Text = "Carrier (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgCarrierDetail));
			this._lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.chkCtlTrailers = new System.Windows.Forms.CheckBox();
			this.chkCtlDrivers = new System.Windows.Forms.CheckBox();
			this._lbleMail = new System.Windows.Forms.Label();
			this.txteMail = new System.Windows.Forms.TextBox();
			this.fraAddress = new System.Windows.Forms.GroupBox();
			this._lblCountry = new System.Windows.Forms.Label();
			this.cboCountries = new System.Windows.Forms.ComboBox();
			this.mCountriesDS = new Tsort.Enterprise.CountryDS();
			this._lblZip = new System.Windows.Forms.Label();
			this.txtZip = new System.Windows.Forms.TextBox();
			this._lblCity = new System.Windows.Forms.Label();
			this.txtCity = new System.Windows.Forms.TextBox();
			this._lblState = new System.Windows.Forms.Label();
			this.cboStates = new System.Windows.Forms.ComboBox();
			this.mStatesDS = new Tsort.Enterprise.StateDS();
			this._lblLine2 = new System.Windows.Forms.Label();
			this.txtLine2 = new System.Windows.Forms.TextBox();
			this._lblLine1 = new System.Windows.Forms.Label();
			this.txtLine1 = new System.Windows.Forms.TextBox();
			this._lblExt = new System.Windows.Forms.Label();
			this.txtExt = new System.Windows.Forms.TextBox();
			this._lblContact = new System.Windows.Forms.Label();
			this.txtContact = new System.Windows.Forms.TextBox();
			this.lblPhone = new System.Windows.Forms.Label();
			this.mskFax = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this._lblNumber = new System.Windows.Forms.Label();
			this.txtNumber = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this._lblFax = new System.Windows.Forms.Label();
			this.mskPhone = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
			this.mCarriersDS = new Tsort.Transportation.CarrierDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraContact = new System.Windows.Forms.GroupBox();
			this.tabTrans = new System.Windows.Forms.TabPage();
			this.fraControl = new System.Windows.Forms.GroupBox();
			this._lblControl = new System.Windows.Forms.Label();
			this.fraAddress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCarriersDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraContact.SuspendLayout();
			this.tabTrans.SuspendLayout();
			this.fraControl.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblName
			// 
			this._lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblName.Location = new System.Drawing.Point(174, 12);
			this._lblName.Name = "_lblName";
			this._lblName.Size = new System.Drawing.Size(72, 18);
			this._lblName.TabIndex = 13;
			this._lblName.Text = "Name";
			this._lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(375, 330);
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
			this.btnOk.Location = new System.Drawing.Point(273, 330);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// chkCtlTrailers
			// 
			this.chkCtlTrailers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkCtlTrailers.Location = new System.Drawing.Point(12, 54);
			this.chkCtlTrailers.Name = "chkCtlTrailers";
			this.chkCtlTrailers.Size = new System.Drawing.Size(126, 18);
			this.chkCtlTrailers.TabIndex = 16;
			this.chkCtlTrailers.Text = "Control Trailers";
			this.chkCtlTrailers.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// chkCtlDrivers
			// 
			this.chkCtlDrivers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkCtlDrivers.Location = new System.Drawing.Point(12, 24);
			this.chkCtlDrivers.Name = "chkCtlDrivers";
			this.chkCtlDrivers.Size = new System.Drawing.Size(120, 18);
			this.chkCtlDrivers.TabIndex = 15;
			this.chkCtlDrivers.Text = "Control Drivers";
			this.chkCtlDrivers.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lbleMail
			// 
			this._lbleMail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lbleMail.Location = new System.Drawing.Point(3, 69);
			this._lbleMail.Name = "_lbleMail";
			this._lbleMail.Size = new System.Drawing.Size(72, 18);
			this._lbleMail.TabIndex = 30;
			this._lbleMail.Text = "eMail";
			this._lbleMail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txteMail
			// 
			this.txteMail.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txteMail.Location = new System.Drawing.Point(81, 69);
			this.txteMail.Name = "txteMail";
			this.txteMail.Size = new System.Drawing.Size(192, 21);
			this.txteMail.TabIndex = 6;
			this.txteMail.Text = "";
			this.txteMail.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// fraAddress
			// 
			this.fraAddress.Controls.Add(this._lblCountry);
			this.fraAddress.Controls.Add(this.cboCountries);
			this.fraAddress.Controls.Add(this._lblZip);
			this.fraAddress.Controls.Add(this.txtZip);
			this.fraAddress.Controls.Add(this._lblCity);
			this.fraAddress.Controls.Add(this.txtCity);
			this.fraAddress.Controls.Add(this._lblState);
			this.fraAddress.Controls.Add(this.cboStates);
			this.fraAddress.Controls.Add(this._lblLine2);
			this.fraAddress.Controls.Add(this.txtLine2);
			this.fraAddress.Controls.Add(this._lblLine1);
			this.fraAddress.Controls.Add(this.txtLine1);
			this.fraAddress.Location = new System.Drawing.Point(6, 39);
			this.fraAddress.Name = "fraAddress";
			this.fraAddress.Size = new System.Drawing.Size(447, 120);
			this.fraAddress.TabIndex = 2;
			this.fraAddress.TabStop = false;
			this.fraAddress.Text = "Mailing Address";
			// 
			// _lblCountry
			// 
			this._lblCountry.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCountry.Location = new System.Drawing.Point(261, 93);
			this._lblCountry.Name = "_lblCountry";
			this._lblCountry.Size = new System.Drawing.Size(54, 18);
			this._lblCountry.TabIndex = 31;
			this._lblCountry.Text = "Country";
			this._lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboCountries
			// 
			this.cboCountries.DataSource = this.mCountriesDS;
			this.cboCountries.DisplayMember = "CountryDetailTable.Country";
			this.cboCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCountries.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboCountries.Location = new System.Drawing.Point(318, 93);
			this.cboCountries.Name = "cboCountries";
			this.cboCountries.Size = new System.Drawing.Size(120, 21);
			this.cboCountries.TabIndex = 3;
			this.cboCountries.ValueMember = "CountryDetailTable.CountryID";
			this.cboCountries.SelectedValueChanged += new System.EventHandler(this.OnCountryChanged);
			this.cboCountries.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mCountriesDS
			// 
			this.mCountriesDS.DataSetName = "CountryDS";
			this.mCountriesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblZip
			// 
			this._lblZip.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblZip.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblZip.Location = new System.Drawing.Point(147, 93);
			this._lblZip.Name = "_lblZip";
			this._lblZip.Size = new System.Drawing.Size(24, 18);
			this._lblZip.TabIndex = 29;
			this._lblZip.Text = "Zip";
			this._lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtZip
			// 
			this.txtZip.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtZip.Location = new System.Drawing.Point(177, 93);
			this.txtZip.MaxLength = 5;
			this.txtZip.Name = "txtZip";
			this.txtZip.Size = new System.Drawing.Size(72, 21);
			this.txtZip.TabIndex = 5;
			this.txtZip.Text = "";
			this.txtZip.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblCity
			// 
			this._lblCity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCity.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblCity.Location = new System.Drawing.Point(27, 69);
			this._lblCity.Name = "_lblCity";
			this._lblCity.Size = new System.Drawing.Size(48, 18);
			this._lblCity.TabIndex = 28;
			this._lblCity.Text = "City";
			this._lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCity
			// 
			this.txtCity.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtCity.Location = new System.Drawing.Point(81, 69);
			this.txtCity.Name = "txtCity";
			this.txtCity.Size = new System.Drawing.Size(168, 21);
			this.txtCity.TabIndex = 2;
			this.txtCity.Text = "";
			this.txtCity.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblState
			// 
			this._lblState.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblState.Location = new System.Drawing.Point(33, 93);
			this._lblState.Name = "_lblState";
			this._lblState.Size = new System.Drawing.Size(48, 18);
			this._lblState.TabIndex = 20;
			this._lblState.Text = "State";
			this._lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboStates
			// 
			this.cboStates.DataSource = this.mStatesDS;
			this.cboStates.DisplayMember = "StateListTable.STATE";
			this.cboStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboStates.Location = new System.Drawing.Point(81, 93);
			this.cboStates.Name = "cboStates";
			this.cboStates.Size = new System.Drawing.Size(60, 21);
			this.cboStates.TabIndex = 4;
			this.cboStates.ValueMember = "StateListTable.STATE";
			this.cboStates.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mStatesDS
			// 
			this.mStatesDS.DataSetName = "StateDS";
			this.mStatesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblLine2
			// 
			this._lblLine2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLine2.Location = new System.Drawing.Point(27, 45);
			this._lblLine2.Name = "_lblLine2";
			this._lblLine2.Size = new System.Drawing.Size(48, 18);
			this._lblLine2.TabIndex = 27;
			this._lblLine2.Text = "Line 2";
			this._lblLine2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLine2
			// 
			this.txtLine2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine2.Location = new System.Drawing.Point(81, 45);
			this.txtLine2.Name = "txtLine2";
			this.txtLine2.Size = new System.Drawing.Size(270, 21);
			this.txtLine2.TabIndex = 1;
			this.txtLine2.Text = "";
			this.txtLine2.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblLine1
			// 
			this._lblLine1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLine1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLine1.Location = new System.Drawing.Point(27, 21);
			this._lblLine1.Name = "_lblLine1";
			this._lblLine1.Size = new System.Drawing.Size(48, 18);
			this._lblLine1.TabIndex = 26;
			this._lblLine1.Text = "Line 1";
			this._lblLine1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLine1
			// 
			this.txtLine1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine1.Location = new System.Drawing.Point(81, 21);
			this.txtLine1.Name = "txtLine1";
			this.txtLine1.Size = new System.Drawing.Size(270, 21);
			this.txtLine1.TabIndex = 0;
			this.txtLine1.Text = "";
			this.txtLine1.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblExt
			// 
			this._lblExt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblExt.Location = new System.Drawing.Point(192, 45);
			this._lblExt.Name = "_lblExt";
			this._lblExt.Size = new System.Drawing.Size(24, 18);
			this._lblExt.TabIndex = 27;
			this._lblExt.Text = "Ext";
			this._lblExt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtExt
			// 
			this.txtExt.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtExt.Location = new System.Drawing.Point(225, 45);
			this.txtExt.Name = "txtExt";
			this.txtExt.Size = new System.Drawing.Size(48, 21);
			this.txtExt.TabIndex = 4;
			this.txtExt.Text = "";
			this.txtExt.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblContact
			// 
			this._lblContact.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblContact.Location = new System.Drawing.Point(3, 21);
			this._lblContact.Name = "_lblContact";
			this._lblContact.Size = new System.Drawing.Size(72, 18);
			this._lblContact.TabIndex = 23;
			this._lblContact.Text = "Name";
			this._lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtContact
			// 
			this.txtContact.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtContact.Location = new System.Drawing.Point(81, 21);
			this.txtContact.Name = "txtContact";
			this.txtContact.Size = new System.Drawing.Size(192, 21);
			this.txtContact.TabIndex = 5;
			this.txtContact.Text = "";
			this.txtContact.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// lblPhone
			// 
			this.lblPhone.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPhone.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblPhone.Location = new System.Drawing.Point(3, 45);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(72, 18);
			this.lblPhone.TabIndex = 22;
			this.lblPhone.Text = "Phone #";
			this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mskFax
			// 
			this.mskFax.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
			this.mskFax.Location = new System.Drawing.Point(342, 45);
			this.mskFax.Name = "mskFax";
			this.mskFax.Size = new System.Drawing.Size(96, 21);
			this.mskFax.TabIndex = 7;
			this.mskFax.ValueChanged += new System.EventHandler(this.ValidateForm);
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(87, 270);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 8;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblNumber
			// 
			this._lblNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblNumber.Location = new System.Drawing.Point(9, 12);
			this._lblNumber.Name = "_lblNumber";
			this._lblNumber.Size = new System.Drawing.Size(72, 18);
			this._lblNumber.TabIndex = 15;
			this._lblNumber.Text = "Number";
			this._lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtNumber
			// 
			this.txtNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtNumber.Location = new System.Drawing.Point(87, 12);
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(72, 21);
			this.txtNumber.TabIndex = 1;
			this.txtNumber.Text = "";
			this.txtNumber.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtName
			// 
			this.txtName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtName.Location = new System.Drawing.Point(252, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(192, 21);
			this.txtName.TabIndex = 0;
			this.txtName.Text = "";
			this.txtName.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblFax
			// 
			this._lblFax.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblFax.Location = new System.Drawing.Point(288, 45);
			this._lblFax.Name = "_lblFax";
			this._lblFax.Size = new System.Drawing.Size(48, 18);
			this._lblFax.TabIndex = 17;
			this._lblFax.Text = "Fax #";
			this._lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mskPhone
			// 
			this.mskPhone.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
			this.mskPhone.Location = new System.Drawing.Point(81, 45);
			this.mskPhone.Name = "mskPhone";
			this.mskPhone.Size = new System.Drawing.Size(96, 21);
			this.mskPhone.TabIndex = 3;
			this.mskPhone.ValueChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mCarriersDS
			// 
			this.mCarriersDS.DataSetName = "CarrierDS";
			this.mCarriersDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Controls.Add(this.tabTrans);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(468, 321);
			this.tabDialog.TabIndex = 0;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraContact);
			this.tabGeneral.Controls.Add(this._lblName);
			this.tabGeneral.Controls.Add(this.txtName);
			this.tabGeneral.Controls.Add(this.txtNumber);
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Controls.Add(this._lblNumber);
			this.tabGeneral.Controls.Add(this.fraAddress);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(460, 295);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// fraContact
			// 
			this.fraContact.Controls.Add(this._lbleMail);
			this.fraContact.Controls.Add(this._lblExt);
			this.fraContact.Controls.Add(this.txteMail);
			this.fraContact.Controls.Add(this._lblContact);
			this.fraContact.Controls.Add(this.txtContact);
			this.fraContact.Controls.Add(this.lblPhone);
			this.fraContact.Controls.Add(this.mskFax);
			this.fraContact.Controls.Add(this.txtExt);
			this.fraContact.Controls.Add(this._lblFax);
			this.fraContact.Controls.Add(this.mskPhone);
			this.fraContact.Location = new System.Drawing.Point(6, 165);
			this.fraContact.Name = "fraContact";
			this.fraContact.Size = new System.Drawing.Size(447, 96);
			this.fraContact.TabIndex = 31;
			this.fraContact.TabStop = false;
			this.fraContact.Text = "Contact";
			// 
			// tabTrans
			// 
			this.tabTrans.Controls.Add(this.fraControl);
			this.tabTrans.Location = new System.Drawing.Point(4, 22);
			this.tabTrans.Name = "tabTrans";
			this.tabTrans.Size = new System.Drawing.Size(460, 295);
			this.tabTrans.TabIndex = 1;
			this.tabTrans.Text = "Transportation";
			this.tabTrans.ToolTipText = "Transportation related information";
			this.tabTrans.Visible = false;
			// 
			// fraControl
			// 
			this.fraControl.Controls.Add(this._lblControl);
			this.fraControl.Controls.Add(this.chkCtlDrivers);
			this.fraControl.Controls.Add(this.chkCtlTrailers);
			this.fraControl.Location = new System.Drawing.Point(6, 12);
			this.fraControl.Name = "fraControl";
			this.fraControl.Size = new System.Drawing.Size(447, 84);
			this.fraControl.TabIndex = 17;
			this.fraControl.TabStop = false;
			this.fraControl.Text = "Administration";
			// 
			// _lblControl
			// 
			this._lblControl.Location = new System.Drawing.Point(192, 24);
			this._lblControl.Name = "_lblControl";
			this._lblControl.Size = new System.Drawing.Size(246, 48);
			this._lblControl.TabIndex = 17;
			this._lblControl.Text = "Selecting to control drivers or trailers will place these entities under administ" +
				"rative control only.";
			this._lblControl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dlgCarrierDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(474, 359);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgCarrierDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Carrier Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraAddress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCarriersDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraContact.ResumeLayout(false);
			this.tabTrans.ResumeLayout(false);
			this.fraControl.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load early
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mCountriesDS.Merge(EnterpriseFactory.GetCountries());
				this.mStatesDS.Merge(EnterpriseFactory.GetStates());
				
				//Set control services
				this.txtNumber.MaxLength = 8;
				this.txtNumber.Text = this.mCarriersDS.CarrierDetailTable[0].Number;
				this.txtNumber.Enabled = (this.mCarrierID==0);
				this.txtName.MaxLength = 30;
				if(!this.mCarriersDS.CarrierDetailTable[0].IsCarrierNameNull())
					this.txtName.Text = this.mCarriersDS.CarrierDetailTable[0].CarrierName;
				
				this.txtLine1.MaxLength = 40;
				this.txtLine1.Text = this.mCarriersDS.CarrierDetailTable[0].AddressLine1;
				this.txtLine2.MaxLength = 40;
				if(!this.mCarriersDS.CarrierDetailTable[0].IsAddressLine2Null())
					this.txtLine2.Text = this.mCarriersDS.CarrierDetailTable[0].AddressLine2;
				this.txtCity.MaxLength = 40;
				//if(!this.mCarriersDS.CarrierDetailTable[0].IsCityNull())
					this.txtCity.Text = this.mCarriersDS.CarrierDetailTable[0].City;
				if(!this.mCarriersDS.CarrierDetailTable[0].IsStateOrProvinceNull()) 
					this.cboStates.SelectedValue = this.mCarriersDS.CarrierDetailTable[0].StateOrProvince;
				else
					if(this.cboStates.Items.Count>0) this.cboStates.SelectedIndex = 0;
				this.cboStates.Enabled = (this.cboStates.Items.Count>0);
				this.txtZip.MaxLength = 15;
				if(!this.mCarriersDS.CarrierDetailTable[0].IsPostalCodeNull())
					this.txtZip.Text = this.mCarriersDS.CarrierDetailTable[0].PostalCode;
				if(this.mCarriersDS.CarrierDetailTable[0].CountryID>0) 
					this.cboCountries.SelectedValue = this.mCarriersDS.CarrierDetailTable[0].CountryID;
				else
					if(this.cboCountries.Items.Count>0) this.cboCountries.SelectedIndex = 0;
				this.cboCountries.Enabled = (this.cboCountries.Items.Count>0);
				
				this.txtContact.MaxLength = 30;
				if(!this.mCarriersDS.CarrierDetailTable[0].IsContactNameNull())
					this.txtContact.Text = this.mCarriersDS.CarrierDetailTable[0].ContactName;
				this.mskPhone.InputMask = "###-###-####";
				if(!this.mCarriersDS.CarrierDetailTable[0].IsPhoneNull())
					this.mskPhone.Value = this.mCarriersDS.CarrierDetailTable[0].Phone;
				this.txtExt.MaxLength = 4;
				if(!this.mCarriersDS.CarrierDetailTable[0].IsExtensionNull())
					this.txtExt.Text = this.mCarriersDS.CarrierDetailTable[0].Extension;
				this.mskFax.InputMask = "###-###-####";
				if(!this.mCarriersDS.CarrierDetailTable[0].IsFaxNull())
					this.mskFax.Value = this.mCarriersDS.CarrierDetailTable[0].Fax;
				this.txteMail.MaxLength = 50;
				if(!this.mCarriersDS.CarrierDetailTable[0].IsEmailNull())
					this.txteMail.Text = this.mCarriersDS.CarrierDetailTable[0].Email;
				
				this.chkCtlDrivers.Checked = this.mCarriersDS.CarrierDetailTable[0].ControlDrivers;
				this.chkCtlTrailers.Checked = this.mCarriersDS.CarrierDetailTable[0].ControlTrailers;
				this.chkStatus.Checked = this.mCarriersDS.CarrierDetailTable[0].IsActive;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnCountryChanged(object sender, System.EventArgs e) {
			//Event handler for user changing country selection
			try {
				//Update state list
				this.cboStates.Enabled = (this.cboCountries.Text=="USA" && this.cboStates.Items.Count>0);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//
			try {
				if(this.mCarriersDS.CarrierDetailTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtNumber.Text!="" && this.txtName.Text!="" && 
						this.txtLine1.Text!="" && this.txtCity.Text!="" && 
						this.cboStates.Text!="" && this.txtZip.Text!="" && this.cboCountries.Text!="" && 
						this.mskPhone.Value!=System.DBNull.Value);
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
						//General
						this.Cursor = Cursors.WaitCursor;
						this.mCarriersDS.CarrierDetailTable[0].Number = this.txtNumber.Text;
						this.mCarriersDS.CarrierDetailTable[0].CarrierName = this.txtName.Text;
						
						this.mCarriersDS.CarrierDetailTable[0].AddressLine1 = this.txtLine1.Text;
						if(this.txtLine2.Text!="")
							this.mCarriersDS.CarrierDetailTable[0].AddressLine2 = this.txtLine2.Text;
						else
							this.mCarriersDS.CarrierDetailTable[0].SetAddressLine2Null();
						this.mCarriersDS.CarrierDetailTable[0].City = this.txtCity.Text;
						this.mCarriersDS.CarrierDetailTable[0].StateOrProvince = this.cboStates.SelectedValue.ToString();
						this.mCarriersDS.CarrierDetailTable[0].PostalCode = this.txtZip.Text;
						this.mCarriersDS.CarrierDetailTable[0].CountryID = (int)this.cboCountries.SelectedValue;
						
						if(this.txtContact.Text!="")
							this.mCarriersDS.CarrierDetailTable[0].ContactName = this.txtContact.Text;
						else
							this.mCarriersDS.CarrierDetailTable[0].SetContactNameNull();
						this.mCarriersDS.CarrierDetailTable[0].Phone = this.mskPhone.Value.ToString();
						if(this.txtExt.Text!="")
							this.mCarriersDS.CarrierDetailTable[0].Extension = this.txtExt.Text;
						else
							this.mCarriersDS.CarrierDetailTable[0].SetExtensionNull();
						if(this.mskFax.Value!=System.DBNull.Value)
							this.mCarriersDS.CarrierDetailTable[0].Fax = this.mskFax.Value.ToString();
						else
							this.mCarriersDS.CarrierDetailTable[0].SetFaxNull();
						if(this.txteMail.Text!="")
							this.mCarriersDS.CarrierDetailTable[0].Email = this.txteMail.Text;
						else
							this.mCarriersDS.CarrierDetailTable[0].SetEmailNull();
						
						this.mCarriersDS.CarrierDetailTable[0].IsActive = this.chkStatus.Checked;

						//Transportation
						this.mCarriersDS.CarrierDetailTable[0].ControlDrivers = this.chkCtlDrivers.Checked;
						this.mCarriersDS.CarrierDetailTable[0].ControlTrailers = this.chkCtlTrailers.Checked;
						this.mCarriersDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
					default:
						Debug.Write("Tsort.Administration.dlgCarrier::OnCmdClick()-No click handler for " + btn.Text + "\n");
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
