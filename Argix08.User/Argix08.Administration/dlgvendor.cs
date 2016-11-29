//	File:	dlgvendor.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new vendor or edit an existing vendor.
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
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Tsort {
	//
	public class dlgVendorDetail : System.Windows.Forms.Form {
		//Members
		private int mVendorID = 0;
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
		private Tsort.Enterprise.VendorDS mVendorsDS;
		private System.Windows.Forms.Label _lbleMail;
		private System.Windows.Forms.TextBox txteMail;
		private Tsort.Enterprise.StateDS mStatesDS;
		private System.Windows.Forms.Label _lblCountry;
		private System.Windows.Forms.ComboBox cboCountries;
		private Tsort.Enterprise.CountryDS mCountriesDS;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabPaymentServices;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdPaymentServices;
		private System.Windows.Forms.Button btnPaymentServiceAdd;
		private System.Windows.Forms.Button btnPaymentServiceEdit;
		private System.Windows.Forms.Button btnPaymentServiceRemove;
		private System.Windows.Forms.ContextMenu ctxPaymentServices;
		private System.Windows.Forms.MenuItem ctxPaymentServiceAdd;
		private System.Windows.Forms.MenuItem ctxPaymentServiceEdit;
		private System.Windows.Forms.MenuItem ctxPaymentServiceRemove;
		private System.Windows.Forms.GroupBox fraContact;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		private const string CMD_PAYMENTSERVICE_ADD = "&Add";
		private const string CMD_PAYMENTSERVICE_EDIT = "&Edit";
		private const string CMD_PAYMENTSERVICE_REMOVE = "&Remove";
		private const string MNU_PAYMENTSERVICE_ADD = "&Add Payment Service Association";
		private const string MNU_PAYMENTSERVICE_EDIT = "&Edit Payment Service Association";
		private const string MNU_PAYMENTSERVICE_REMOVE = "&Remove Payment Service Association";
		private const string GRID_TYPE_PAYMENTSERVICES = "Payment Service Associations";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgVendorDetail(ref VendorDS vendor) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				this.btnPaymentServiceAdd.Text = CMD_PAYMENTSERVICE_ADD;
				this.btnPaymentServiceEdit.Text = CMD_PAYMENTSERVICE_EDIT;
				this.btnPaymentServiceRemove.Text = CMD_PAYMENTSERVICE_REMOVE;
				this.ctxPaymentServiceAdd.Text = MNU_PAYMENTSERVICE_ADD;
				this.ctxPaymentServiceEdit.Text = MNU_PAYMENTSERVICE_EDIT;
				this.ctxPaymentServiceRemove.Text = MNU_PAYMENTSERVICE_REMOVE;
				
				//Set mediator service, data, and titlebar caption
				this.mVendorsDS = vendor;
				if(this.mVendorsDS.VendorDetailTable.Count>0) {
					this.mVendorID = this.mVendorsDS.VendorDetailTable[0].VendorID;
					this.Text = (this.mVendorID>0) ? "Vendor (" + this.mVendorID + ")" : "Vendor (New)";
				}
				else
					this.Text = "Vendor (Data Unavailable)";
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
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CompanyPaymentServiceTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaymentServiceID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaymentServiceName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaymentServiceNumber");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comments");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgVendorDetail));
			this._lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
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
			this.mVendorsDS = new Tsort.Enterprise.VendorDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraContact = new System.Windows.Forms.GroupBox();
			this.tabPaymentServices = new System.Windows.Forms.TabPage();
			this.grdPaymentServices = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxPaymentServices = new System.Windows.Forms.ContextMenu();
			this.ctxPaymentServiceAdd = new System.Windows.Forms.MenuItem();
			this.ctxPaymentServiceEdit = new System.Windows.Forms.MenuItem();
			this.ctxPaymentServiceRemove = new System.Windows.Forms.MenuItem();
			this.btnPaymentServiceAdd = new System.Windows.Forms.Button();
			this.btnPaymentServiceEdit = new System.Windows.Forms.Button();
			this.btnPaymentServiceRemove = new System.Windows.Forms.Button();
			this.fraAddress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mVendorsDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraContact.SuspendLayout();
			this.tabPaymentServices.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdPaymentServices)).BeginInit();
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
			this.btnCancel.Location = new System.Drawing.Point(375, 333);
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
			this.btnOk.Location = new System.Drawing.Point(273, 333);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
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
			this._lblCountry.Location = new System.Drawing.Point(258, 93);
			this._lblCountry.Name = "_lblCountry";
			this._lblCountry.Size = new System.Drawing.Size(60, 18);
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
			this._lblZip.Location = new System.Drawing.Point(150, 93);
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
			this._lblCity.Location = new System.Drawing.Point(3, 69);
			this._lblCity.Name = "_lblCity";
			this._lblCity.Size = new System.Drawing.Size(72, 18);
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
			this._lblState.Location = new System.Drawing.Point(27, 93);
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
			this._lblLine2.Location = new System.Drawing.Point(3, 45);
			this._lblLine2.Name = "_lblLine2";
			this._lblLine2.Size = new System.Drawing.Size(72, 18);
			this._lblLine2.TabIndex = 27;
			this._lblLine2.Text = "Line 2";
			this._lblLine2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLine2
			// 
			this.txtLine2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine2.Location = new System.Drawing.Point(81, 45);
			this.txtLine2.Name = "txtLine2";
			this.txtLine2.Size = new System.Drawing.Size(357, 21);
			this.txtLine2.TabIndex = 1;
			this.txtLine2.Text = "";
			this.txtLine2.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblLine1
			// 
			this._lblLine1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLine1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLine1.Location = new System.Drawing.Point(3, 21);
			this._lblLine1.Name = "_lblLine1";
			this._lblLine1.Size = new System.Drawing.Size(72, 18);
			this._lblLine1.TabIndex = 26;
			this._lblLine1.Text = "Line 1";
			this._lblLine1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLine1
			// 
			this.txtLine1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine1.Location = new System.Drawing.Point(81, 21);
			this.txtLine1.Name = "txtLine1";
			this.txtLine1.Size = new System.Drawing.Size(357, 21);
			this.txtLine1.TabIndex = 0;
			this.txtLine1.Text = "";
			this.txtLine1.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblExt
			// 
			this._lblExt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblExt.Location = new System.Drawing.Point(195, 45);
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
			this.mskFax.Location = new System.Drawing.Point(339, 45);
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
			this.chkStatus.Location = new System.Drawing.Point(87, 273);
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
			this._lblFax.Location = new System.Drawing.Point(285, 45);
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
			// mVendorsDS
			// 
			this.mVendorsDS.DataSetName = "VendorDS";
			this.mVendorsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Controls.Add(this.tabPaymentServices);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(468, 324);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraContact);
			this.tabGeneral.Controls.Add(this.txtName);
			this.tabGeneral.Controls.Add(this.fraAddress);
			this.tabGeneral.Controls.Add(this._lblName);
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Controls.Add(this.txtNumber);
			this.tabGeneral.Controls.Add(this._lblNumber);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(460, 298);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// fraContact
			// 
			this.fraContact.Controls.Add(this._lblFax);
			this.fraContact.Controls.Add(this.mskPhone);
			this.fraContact.Controls.Add(this._lblExt);
			this.fraContact.Controls.Add(this.txtExt);
			this.fraContact.Controls.Add(this._lblContact);
			this.fraContact.Controls.Add(this.txtContact);
			this.fraContact.Controls.Add(this.lblPhone);
			this.fraContact.Controls.Add(this.mskFax);
			this.fraContact.Controls.Add(this._lbleMail);
			this.fraContact.Controls.Add(this.txteMail);
			this.fraContact.Location = new System.Drawing.Point(6, 165);
			this.fraContact.Name = "fraContact";
			this.fraContact.Size = new System.Drawing.Size(447, 96);
			this.fraContact.TabIndex = 31;
			this.fraContact.TabStop = false;
			this.fraContact.Text = "Contact";
			// 
			// tabPaymentServices
			// 
			this.tabPaymentServices.Controls.Add(this.grdPaymentServices);
			this.tabPaymentServices.Controls.Add(this.btnPaymentServiceAdd);
			this.tabPaymentServices.Controls.Add(this.btnPaymentServiceEdit);
			this.tabPaymentServices.Controls.Add(this.btnPaymentServiceRemove);
			this.tabPaymentServices.Location = new System.Drawing.Point(4, 22);
			this.tabPaymentServices.Name = "tabPaymentServices";
			this.tabPaymentServices.Size = new System.Drawing.Size(460, 298);
			this.tabPaymentServices.TabIndex = 3;
			this.tabPaymentServices.Text = "Payment Services";
			this.tabPaymentServices.ToolTipText = "Payment Service Associations";
			// 
			// grdPaymentServices
			// 
			this.grdPaymentServices.ContextMenu = this.ctxPaymentServices;
			this.grdPaymentServices.DataMember = "CompanyPaymentServiceTable";
			this.grdPaymentServices.DataSource = this.mVendorsDS;
			this.grdPaymentServices.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdPaymentServices.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn1.Hidden = true;
			ultraGridColumn2.Hidden = true;
			ultraGridColumn3.Header.Caption = "PS Name";
			ultraGridColumn3.Width = 153;
			ultraGridColumn4.Header.Caption = "PS #";
			ultraGridColumn4.Width = 67;
			ultraGridColumn5.Header.Caption = "Active";
			ultraGridColumn5.Width = 52;
			ultraGridColumn6.Header.VisiblePosition = 7;
			ultraGridColumn6.Width = 80;
			ultraGridColumn7.Header.VisiblePosition = 8;
			ultraGridColumn7.Hidden = true;
			ultraGridColumn8.Header.VisiblePosition = 5;
			ultraGridColumn8.Hidden = true;
			ultraGridColumn9.Header.VisiblePosition = 6;
			ultraGridColumn9.Hidden = true;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Columns.Add(ultraGridColumn4);
			ultraGridBand1.Columns.Add(ultraGridColumn5);
			ultraGridBand1.Columns.Add(ultraGridColumn6);
			ultraGridBand1.Columns.Add(ultraGridColumn7);
			ultraGridBand1.Columns.Add(ultraGridColumn8);
			ultraGridBand1.Columns.Add(ultraGridColumn9);
			appearance2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance2.FontData.Name = "Verdana";
			appearance2.FontData.SizeInPoints = 8F;
			appearance2.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.ActiveRowAppearance = appearance2;
			appearance3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance3.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance3.FontData.Name = "Verdana";
			appearance3.FontData.SizeInPoints = 8F;
			appearance3.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance3.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.HeaderAppearance = appearance3;
			appearance4.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance4.FontData.Name = "Verdana";
			appearance4.FontData.SizeInPoints = 8F;
			appearance4.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand1.Override.RowAlternateAppearance = appearance4;
			appearance5.BackColor = System.Drawing.Color.White;
			appearance5.FontData.Name = "Verdana";
			appearance5.FontData.SizeInPoints = 8F;
			appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance5.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand1.Override.RowAppearance = appearance5;
			this.grdPaymentServices.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdPaymentServices.DisplayLayout.CaptionAppearance = appearance6;
			this.grdPaymentServices.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdPaymentServices.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdPaymentServices.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdPaymentServices.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdPaymentServices.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdPaymentServices.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdPaymentServices.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdPaymentServices.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdPaymentServices.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdPaymentServices.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdPaymentServices.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdPaymentServices.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdPaymentServices.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdPaymentServices.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdPaymentServices.Location = new System.Drawing.Point(6, 18);
			this.grdPaymentServices.Name = "grdPaymentServices";
			this.grdPaymentServices.Size = new System.Drawing.Size(450, 243);
			this.grdPaymentServices.TabIndex = 35;
			this.grdPaymentServices.Text = "Payment Service Associations";
			this.grdPaymentServices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			this.grdPaymentServices.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
			// 
			// ctxPaymentServices
			// 
			this.ctxPaymentServices.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.ctxPaymentServiceAdd,
																							   this.ctxPaymentServiceEdit,
																							   this.ctxPaymentServiceRemove});
			// 
			// ctxPaymentServiceAdd
			// 
			this.ctxPaymentServiceAdd.Index = 0;
			this.ctxPaymentServiceAdd.Text = "&Add Payment Service Association";
			this.ctxPaymentServiceAdd.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxPaymentServiceEdit
			// 
			this.ctxPaymentServiceEdit.Index = 1;
			this.ctxPaymentServiceEdit.Text = "&Edit Payment Service Association";
			this.ctxPaymentServiceEdit.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxPaymentServiceRemove
			// 
			this.ctxPaymentServiceRemove.Index = 2;
			this.ctxPaymentServiceRemove.Text = "&Remove Payment Service Association";
			this.ctxPaymentServiceRemove.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// btnPaymentServiceAdd
			// 
			this.btnPaymentServiceAdd.BackColor = System.Drawing.SystemColors.Control;
			this.btnPaymentServiceAdd.Location = new System.Drawing.Point(156, 270);
			this.btnPaymentServiceAdd.Name = "btnPaymentServiceAdd";
			this.btnPaymentServiceAdd.Size = new System.Drawing.Size(96, 24);
			this.btnPaymentServiceAdd.TabIndex = 34;
			this.btnPaymentServiceAdd.Text = "&Add";
			this.btnPaymentServiceAdd.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnPaymentServiceEdit
			// 
			this.btnPaymentServiceEdit.BackColor = System.Drawing.SystemColors.Control;
			this.btnPaymentServiceEdit.Enabled = false;
			this.btnPaymentServiceEdit.Location = new System.Drawing.Point(258, 270);
			this.btnPaymentServiceEdit.Name = "btnPaymentServiceEdit";
			this.btnPaymentServiceEdit.Size = new System.Drawing.Size(96, 24);
			this.btnPaymentServiceEdit.TabIndex = 33;
			this.btnPaymentServiceEdit.Text = "&Edit";
			this.btnPaymentServiceEdit.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnPaymentServiceRemove
			// 
			this.btnPaymentServiceRemove.BackColor = System.Drawing.SystemColors.Control;
			this.btnPaymentServiceRemove.Enabled = false;
			this.btnPaymentServiceRemove.Location = new System.Drawing.Point(360, 270);
			this.btnPaymentServiceRemove.Name = "btnPaymentServiceRemove";
			this.btnPaymentServiceRemove.Size = new System.Drawing.Size(96, 24);
			this.btnPaymentServiceRemove.TabIndex = 32;
			this.btnPaymentServiceRemove.Text = "&Remove";
			this.btnPaymentServiceRemove.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// dlgVendorDetail
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
			this.Name = "dlgVendorDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Vendor Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraAddress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mVendorsDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraContact.ResumeLayout(false);
			this.tabPaymentServices.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdPaymentServices)).EndInit();
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
				this.mCountriesDS.Merge(EnterpriseFactory.GetCountries());
				this.mStatesDS.Merge(EnterpriseFactory.GetStates());
				
				//Set control services
				#region Default grid behavior
				this.grdPaymentServices.Text = GRID_TYPE_PAYMENTSERVICES;
				this.grdPaymentServices.DataSource = this.mVendorsDS;
				this.grdPaymentServices.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
				this.grdPaymentServices.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdPaymentServices.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
				#endregion
				this.txtNumber.MaxLength = 8;
				this.txtNumber.Text = this.mVendorsDS.VendorDetailTable[0].Number;
				this.txtNumber.Enabled = (mVendorID==0);
				this.txtName.MaxLength = 30;
				this.txtName.Text = this.mVendorsDS.VendorDetailTable[0].VendorName;
				
				this.txtLine1.MaxLength = 40;
				this.txtLine1.Text = this.mVendorsDS.VendorDetailTable[0].AddressLine1;
				this.txtLine2.MaxLength = 40;
				if(!this.mVendorsDS.VendorDetailTable[0].IsAddressLine2Null())
					this.txtLine2.Text = this.mVendorsDS.VendorDetailTable[0].AddressLine2;
				this.txtCity.MaxLength = 40;
				this.txtCity.Text = this.mVendorsDS.VendorDetailTable[0].City;
				if(this.mVendorsDS.VendorDetailTable[0].IsStateOrProvinceNull()) 
					this.cboStates.SelectedIndex = 0;
				else
					this.cboStates.SelectedValue = this.mVendorsDS.VendorDetailTable[0].StateOrProvince;
				this.cboStates.Enabled = (this.cboStates.Items.Count>0);
				this.txtZip.MaxLength = 15;
				if(!this.mVendorsDS.VendorDetailTable[0].IsPostalCodeNull())
					this.txtZip.Text = this.mVendorsDS.VendorDetailTable[0].PostalCode;
				if(this.mVendorsDS.VendorDetailTable[0].CountryID==0) 
					this.cboCountries.SelectedIndex = 0;
				else
					this.cboCountries.SelectedValue = this.mVendorsDS.VendorDetailTable[0].CountryID;
				this.cboCountries.Enabled = (this.cboCountries.Items.Count>0);
				
				this.txtContact.MaxLength = 30;
				this.txtContact.Text = "";
				if(!this.mVendorsDS.VendorDetailTable[0].IsContactNameNull())
					this.txtContact.Text = this.mVendorsDS.VendorDetailTable[0].ContactName;
				this.mskPhone.InputMask = "###-###-####";
				if(!this.mVendorsDS.VendorDetailTable[0].IsPhoneNull())
					this.mskPhone.Value = this.mVendorsDS.VendorDetailTable[0].Phone;
				this.txtExt.MaxLength = 4;
				if(!this.mVendorsDS.VendorDetailTable[0].IsExtensionNull())
					this.txtExt.Text = this.mVendorsDS.VendorDetailTable[0].Extension;
				this.mskFax.InputMask = "###-###-####";
				if(!this.mVendorsDS.VendorDetailTable[0].IsFaxNull())
					this.mskFax.Value = this.mVendorsDS.VendorDetailTable[0].Fax;
				if(!this.mVendorsDS.VendorDetailTable[0].IsEmailNull())
					this.txteMail.Text = this.mVendorsDS.VendorDetailTable[0].Email;
				
				this.chkStatus.Checked = this.mVendorsDS.VendorDetailTable[0].IsActive;
				
				//Set grid selection to first row (invoke click event)
				if(this.grdPaymentServices.Rows.Count>0) {
					this.grdPaymentServices.ActiveRow = this.grdPaymentServices.Rows[0];
					this.grdPaymentServices.ActiveRow.Selected = true;
				}
				else
					this.setUserServices(false, GRID_TYPE_PAYMENTSERVICES);
				this.tabPaymentServices.Enabled = (this.mVendorID != 0);
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
		#region Grid Control: OnGridSelectionChanged(), OnGridMouseDown(), SetGridRowActive()
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			//This event will cover mouse left-click and keyboard changes
			try  {
				//Set menu and toolbar sevices for selected row
				UltraGrid grd = (UltraGrid)sender;
				if(grd.Text==GRID_TYPE_PAYMENTSERVICES) 
					setUserServices(true, GRID_TYPE_PAYMENTSERVICES);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			bool isItem = false;
			try {
				//Set menu and toolbar services
				UltraGrid grd = (UltraGrid)sender;
				if(grd.Text==GRID_TYPE_PAYMENTSERVICES) {
					isItem = SetGridRowActive(e, GRID_TYPE_PAYMENTSERVICES);
					setUserServices(isItem, GRID_TYPE_PAYMENTSERVICES);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private bool SetGridRowActive(System.Windows.Forms.MouseEventArgs e, string type) {
			//Set the active row in a grid
			bool isItem = false;
			object oContext = null;
			UltraGridCell oCell;
			UIElement infraUIElement;
			UltraGrid formGrid = null;
			try {
				//Set active grid row per right mouse
				if(type==GRID_TYPE_PAYMENTSERVICES) 
					formGrid = this.grdPaymentServices;
				infraUIElement = formGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				oContext = infraUIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
				if (oContext != null) {
					oCell = (Infragistics.Win.UltraWinGrid.UltraGridCell)oContext;
					formGrid.ActiveRow = oCell.Row;
					formGrid.ActiveRow.Selected = true;
					isItem = true;
				}
				else {
					//This happens when you click on the divider area between rows and cells;
					//if the active row changes, make sure it's also selected
					if(formGrid.ActiveRow!=null)
						formGrid.ActiveRow.Selected = true;
					isItem = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
			return isItem;
		}
		#endregion
		#region User Services: ValidateForm(), OnCmdClick(), OnMenuClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in control data
			try {
				//Enable OK service if details have valid changes
				if(this.mVendorsDS.VendorDetailTable.Count>0) {
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
					case CMD_PAYMENTSERVICE_ADD:	this.ctxPaymentServiceAdd.PerformClick(); break;
					case CMD_PAYMENTSERVICE_EDIT:	this.ctxPaymentServiceEdit.PerformClick(); break;
					case CMD_PAYMENTSERVICE_REMOVE:	this.ctxPaymentServiceRemove.PerformClick(); break;
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						//Update details with control values
						this.Cursor = Cursors.WaitCursor;
						this.mVendorsDS.VendorDetailTable[0].Number = this.txtNumber.Text;
						this.mVendorsDS.VendorDetailTable[0].VendorName = this.txtName.Text;
						
						this.mVendorsDS.VendorDetailTable[0].AddressLine1 = this.txtLine1.Text;
						if(this.txtLine2.Text!="")
							this.mVendorsDS.VendorDetailTable[0].AddressLine2 = this.txtLine2.Text;
						else
							this.mVendorsDS.VendorDetailTable[0].SetAddressLine2Null();
						this.mVendorsDS.VendorDetailTable[0].City = this.txtCity.Text;
						this.mVendorsDS.VendorDetailTable[0].StateOrProvince = this.cboStates.SelectedValue.ToString();
						this.mVendorsDS.VendorDetailTable[0].PostalCode = this.txtZip.Text;
						this.mVendorsDS.VendorDetailTable[0].CountryID = (int)this.cboCountries.SelectedValue;
						
						if(this.txtContact.Text!="")
							this.mVendorsDS.VendorDetailTable[0].ContactName = this.txtContact.Text;
						else
							this.mVendorsDS.VendorDetailTable[0].SetContactNameNull();
						this.mVendorsDS.VendorDetailTable[0].Phone = this.mskPhone.Value.ToString();
						if(this.txtExt.Text!="")
							this.mVendorsDS.VendorDetailTable[0].Extension = this.txtExt.Text;
						else
							this.mVendorsDS.VendorDetailTable[0].SetExtensionNull();
						if(this.mskFax.Value!=System.DBNull.Value)
							this.mVendorsDS.VendorDetailTable[0].Fax = this.mskFax.Value.ToString();
						else
							this.mVendorsDS.VendorDetailTable[0].SetFaxNull();
						if(this.txteMail.Text!="")
							this.mVendorsDS.VendorDetailTable[0].Email = this.txteMail.Text;
						else
							this.mVendorsDS.VendorDetailTable[0].SetEmailNull();
						this.mVendorsDS.VendorDetailTable[0].IsActive = this.chkStatus.Checked;
						
						//this.mVendorsDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
					default:
						Debug.Write("Tsort.Administration.dlgVendor::OnCmdClick()-No click handler for " + btn.Text + "\n");
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu item clicked - apply selected service
			dlgCompanyPaymentService dlgCompanyPaymentService = null;
			CompanyDS dsCompanyPaymentService = null;
			CompanyDS.CompanyPaymentServiceTableRow rowEPS;
			VendorDS.CompanyPaymentServiceTableRow row;
			int paymentServiceID = 0;
			string userID = Environment.UserName;
			DateTime updated = DateTime.Now;
			DialogResult res;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_PAYMENTSERVICE_ADD:
						//Add a new item to the list
						paymentServiceID = 0;
						dsCompanyPaymentService = new CompanyDS();
						rowEPS = dsCompanyPaymentService.CompanyPaymentServiceTable.NewCompanyPaymentServiceTableRow();
						rowEPS.CompanyID = this.mVendorID;
						rowEPS.PaymentServiceID = paymentServiceID;
						rowEPS.PaymentServiceName = "";
						rowEPS.Comments = "";
						rowEPS.IsActive = true;
						rowEPS.LastUpdated = DateTime.Now;
						rowEPS.UserID = Environment.UserName;
						rowEPS.RowVersion = "";
						dsCompanyPaymentService.CompanyPaymentServiceTable.AddCompanyPaymentServiceTableRow(rowEPS);
						dlgCompanyPaymentService = new dlgCompanyPaymentService(this.mVendorID, this.chkStatus.Checked, ref dsCompanyPaymentService);
						res = dlgCompanyPaymentService.ShowDialog(this);
						if(res==DialogResult.OK) {
							//Added new association
							//*** RESULT MUST MARK ROW AS DataRowState.Added
							row = this.mVendorsDS.CompanyPaymentServiceTable.NewCompanyPaymentServiceTableRow();
							row.ItemArray = dsCompanyPaymentService.CompanyPaymentServiceTable[0].ItemArray;
							this.mVendorsDS.CompanyPaymentServiceTable.AddCompanyPaymentServiceTableRow(row);
						}
						break;
					case MNU_PAYMENTSERVICE_EDIT:
						//Update an existing item in the list
						paymentServiceID = (int)this.grdPaymentServices.Selected.Rows[0].Cells["PaymentServiceID"].Value;
						dsCompanyPaymentService = new CompanyDS();
						rowEPS = dsCompanyPaymentService.CompanyPaymentServiceTable.NewCompanyPaymentServiceTableRow();
						rowEPS.CompanyID = this.mVendorID;
						rowEPS.PaymentServiceID = paymentServiceID;
						rowEPS.PaymentServiceName = this.grdPaymentServices.Selected.Rows[0].Cells["PaymentServiceName"].Value.ToString();
						if(this.grdPaymentServices.Selected.Rows[0].Cells["Comments"].Value!=DBNull.Value)
							rowEPS.Comments = this.grdPaymentServices.Selected.Rows[0].Cells["Comments"].Value.ToString();
						rowEPS.IsActive = Convert.ToBoolean(this.grdPaymentServices.Selected.Rows[0].Cells["IsActive"].Value);
						rowEPS.LastUpdated = Convert.ToDateTime(this.grdPaymentServices.Selected.Rows[0].Cells["LastUpdated"].Value);
						rowEPS.UserID = this.grdPaymentServices.Selected.Rows[0].Cells["UserID"].Value.ToString();
						rowEPS.RowVersion = this.grdPaymentServices.Selected.Rows[0].Cells["RowVersion"].Value.ToString();
						dsCompanyPaymentService.CompanyPaymentServiceTable.AddCompanyPaymentServiceTableRow(rowEPS);
						dlgCompanyPaymentService = new dlgCompanyPaymentService(this.mVendorID, this.chkStatus.Checked, ref dsCompanyPaymentService);
						res = dlgCompanyPaymentService.ShowDialog(this);
						if(res==DialogResult.OK) {
							//Update in detail dataset m_dsPaymentServices.CompanyPaymentServiceTable
							//*** RESULT MUST MARK ROW AS DataRowState.Modified
							this.grdPaymentServices.Selected.Rows[0].Cells["Comments"].Value = dsCompanyPaymentService.CompanyPaymentServiceTable[0].Comments;
							this.grdPaymentServices.Selected.Rows[0].Cells["IsActive"].Value = dsCompanyPaymentService.CompanyPaymentServiceTable[0].IsActive;
							this.grdPaymentServices.Selected.Rows[0].Update();
						}
						break; 
					case MNU_PAYMENTSERVICE_REMOVE:
						//Delete an existing item in the list
						paymentServiceID = (int)this.grdPaymentServices.Selected.Rows[0].Cells["PaymentServiceID"].Value;
						res = MessageBox.Show(this, "Delete association with payment service " + paymentServiceID + "?", this.Name, MessageBoxButtons.OKCancel);
						if(res==DialogResult.OK) {
							//Removed association
							//*** RESULT MUST MARK ROW AS DataRowState.Deleted
							this.grdPaymentServices.Selected.Rows[0].Delete();
						}
						break;
				}
				ValidateForm(null, null);
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: setUserServices(), reportError()
		private void setUserServices(bool isItem, string type) {
			//Set user services depending upon an item selected in the grid
			try  {
				if(type==GRID_TYPE_PAYMENTSERVICES) {
					this.btnPaymentServiceAdd.Enabled = this.ctxPaymentServiceAdd.Enabled = !isItem;
					this.btnPaymentServiceEdit.Enabled = this.ctxPaymentServiceEdit.Enabled = isItem;
					this.btnPaymentServiceRemove.Enabled = this.ctxPaymentServiceRemove.Enabled = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
