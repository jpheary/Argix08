//	File:	dlgclient.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new client or edit an existing client.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Tsort.Enterprise;

namespace Tsort {
	//
	public class dlgClientDetail : System.Windows.Forms.Form {
		//Members
		private int mClientID=0;		
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
		private Tsort.Enterprise.ClientDS mClientDS;
		private System.Windows.Forms.Label _lbleMail;
		private System.Windows.Forms.TextBox txteMail;
		private Tsort.Enterprise.StateDS mStatesDS;
		private System.Windows.Forms.Label _lblCountry;
		private System.Windows.Forms.ComboBox cboCountries;
		private Tsort.Enterprise.CountryDS mCountriesDS;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabVendors;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdVendorLocs;
		private System.Windows.Forms.ContextMenu ctxVendors;
		private System.Windows.Forms.MenuItem ctxVendorAdd;
		private System.Windows.Forms.MenuItem ctxVendorEdit;
		private System.Windows.Forms.MenuItem ctxVendorRemove;
		private System.Windows.Forms.Button btnVendorAdd;
		private System.Windows.Forms.Button btnVendorEdit;
		private System.Windows.Forms.Button btnVendorRemove;
		private System.Windows.Forms.TabPage tabPaymentServices;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdCoPayServices;
		private System.Windows.Forms.Button btnPaymentServiceAdd;
		private System.Windows.Forms.Button btnPaymentServiceEdit;
		private System.Windows.Forms.Button btnPaymentServiceRemove;
		private System.Windows.Forms.ContextMenu ctxPaymentServices;
		private System.Windows.Forms.MenuItem ctxPaymentServiceAdd;
		private System.Windows.Forms.MenuItem ctxPaymentServiceEdit;
		private System.Windows.Forms.MenuItem ctxPaymentServiceRemove;
		private Tsort.Enterprise.CVLocationDS mCVLocationsDS;
		private System.Windows.Forms.GroupBox fraContact;
		private System.Windows.Forms.TextBox txtMnemonic;
		private System.Windows.Forms.Label _lblMnemonic;
		private System.Windows.Forms.TabPage tabFreight;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdFreight;
		private Tsort.Enterprise.ClientTerminalDS mClientTerminalDS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		private const string CMD_VENDOR_ADD = "&Add";
		private const string CMD_VENDOR_EDIT = "&Edit";
		private const string CMD_VENDOR_REMOVE = "&Remove";
		private const string CMD_PAYMENTSERVICE_ADD = "Add";
		private const string CMD_PAYMENTSERVICE_EDIT = "Edit";
		private const string CMD_PAYMENTSERVICE_REMOVE = "Remove";
		private const string MNU_VENDORLOCATION_ADD = "&Add Vendor Location Association";
		private const string MNU_VENDORLOCATION_EDIT = "&Edit Vendor Location Association";
		private const string MNU_VENDORLOCATION_REMOVE = "&Remove Vendor Location Association";
		private const string MNU_PAYMENTSERVICE_ADD = "&Add Payment Service Association";
		private const string MNU_PAYMENTSERVICE_EDIT = "&Edit Payment Service Association";
		private const string MNU_PAYMENTSERVICE_REMOVE = "&Remove Payment Service Association";
		private const string GRID_TYPE_VENDORS = "Vendor Location Associations";
		private const string GRID_TYPE_PAYMENTSERVICES = "Payment Service Associations";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgClientDetail(ref ClientDS client) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				this.btnVendorAdd.Text = CMD_VENDOR_ADD;
				this.btnVendorEdit.Text = CMD_VENDOR_EDIT;
				this.btnVendorRemove.Text = CMD_VENDOR_REMOVE;
				this.btnPaymentServiceAdd.Text = CMD_PAYMENTSERVICE_ADD;
				this.btnPaymentServiceEdit.Text = CMD_PAYMENTSERVICE_EDIT;
				this.btnPaymentServiceRemove.Text = CMD_PAYMENTSERVICE_REMOVE;
				this.ctxVendorAdd.Text = MNU_VENDORLOCATION_ADD;
				this.ctxVendorEdit.Text = MNU_VENDORLOCATION_EDIT;
				this.ctxVendorRemove.Text = MNU_VENDORLOCATION_REMOVE;
				this.ctxPaymentServiceAdd.Text = MNU_PAYMENTSERVICE_ADD;
				this.ctxPaymentServiceEdit.Text = MNU_PAYMENTSERVICE_EDIT;
				this.ctxPaymentServiceRemove.Text = MNU_PAYMENTSERVICE_REMOVE;
				
				//Set mediator service, data, and titlebar caption
				this.mClientDS = client;
				if(this.mClientDS.ClientDetailTable.Count>0) {
					this.mClientID = this.mClientDS.ClientDetailTable[0].ClientID;
					this.mCVLocationsDS.Merge(EnterpriseFactory.ViewCVLocations(this.mClientID));
					this.Text = (this.mClientID>0) ? "Client (" + this.mClientID + ")" : "Client (New)";
				}
				else
					this.Text = "Client (Data Unavailable)";
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
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ClientTerminalTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CVLocationViewTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LinkID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorID");
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorName");
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CompanyPaymentServiceTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaymentServiceID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaymentServiceName");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaymentServiceNumber", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comments");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RowVersion");
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgClientDetail));
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
			this.mClientDS = new Tsort.Enterprise.ClientDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.txtMnemonic = new System.Windows.Forms.TextBox();
			this._lblMnemonic = new System.Windows.Forms.Label();
			this.fraContact = new System.Windows.Forms.GroupBox();
			this.tabFreight = new System.Windows.Forms.TabPage();
			this.grdFreight = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.mClientTerminalDS = new Tsort.Enterprise.ClientTerminalDS();
			this.tabVendors = new System.Windows.Forms.TabPage();
			this.grdVendorLocs = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxVendors = new System.Windows.Forms.ContextMenu();
			this.ctxVendorAdd = new System.Windows.Forms.MenuItem();
			this.ctxVendorEdit = new System.Windows.Forms.MenuItem();
			this.ctxVendorRemove = new System.Windows.Forms.MenuItem();
			this.mCVLocationsDS = new Tsort.Enterprise.CVLocationDS();
			this.btnVendorAdd = new System.Windows.Forms.Button();
			this.btnVendorEdit = new System.Windows.Forms.Button();
			this.btnVendorRemove = new System.Windows.Forms.Button();
			this.tabPaymentServices = new System.Windows.Forms.TabPage();
			this.grdCoPayServices = new Infragistics.Win.UltraWinGrid.UltraGrid();
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
			((System.ComponentModel.ISupportInitialize)(this.mClientDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraContact.SuspendLayout();
			this.tabFreight.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdFreight)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mClientTerminalDS)).BeginInit();
			this.tabVendors.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdVendorLocs)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCVLocationsDS)).BeginInit();
			this.tabPaymentServices.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdCoPayServices)).BeginInit();
			this.SuspendLayout();
			// 
			// _lblName
			// 
			this._lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblName.Location = new System.Drawing.Point(171, 12);
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
			this._lbleMail.Location = new System.Drawing.Point(3, 66);
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
			this.fraAddress.Location = new System.Drawing.Point(6, 63);
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
			this._lblState.Location = new System.Drawing.Point(3, 93);
			this._lblState.Name = "_lblState";
			this._lblState.Size = new System.Drawing.Size(72, 18);
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
			this._lblContact.Location = new System.Drawing.Point(3, 18);
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
			this.chkStatus.Location = new System.Drawing.Point(249, 38);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 8;
			this.chkStatus.Text = "Active";
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
			this.txtName.Location = new System.Drawing.Point(249, 12);
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
			// mClientDS
			// 
			this.mClientDS.DataSetName = "ClientDS";
			this.mClientDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Controls.Add(this.tabFreight);
			this.tabDialog.Controls.Add(this.tabVendors);
			this.tabDialog.Controls.Add(this.tabPaymentServices);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(468, 324);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.txtMnemonic);
			this.tabGeneral.Controls.Add(this._lblMnemonic);
			this.tabGeneral.Controls.Add(this.fraContact);
			this.tabGeneral.Controls.Add(this.fraAddress);
			this.tabGeneral.Controls.Add(this.txtName);
			this.tabGeneral.Controls.Add(this._lblName);
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Controls.Add(this._lblNumber);
			this.tabGeneral.Controls.Add(this.txtNumber);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(460, 298);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// txtMnemonic
			// 
			this.txtMnemonic.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtMnemonic.Location = new System.Drawing.Point(87, 37);
			this.txtMnemonic.Name = "txtMnemonic";
			this.txtMnemonic.Size = new System.Drawing.Size(36, 21);
			this.txtMnemonic.TabIndex = 32;
			this.txtMnemonic.Text = "";
			this.txtMnemonic.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblMnemonic
			// 
			this._lblMnemonic.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblMnemonic.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblMnemonic.Location = new System.Drawing.Point(9, 39);
			this._lblMnemonic.Name = "_lblMnemonic";
			this._lblMnemonic.Size = new System.Drawing.Size(72, 16);
			this._lblMnemonic.TabIndex = 33;
			this._lblMnemonic.Text = "Mnemonic";
			this._lblMnemonic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// fraContact
			// 
			this.fraContact.Controls.Add(this.txteMail);
			this.fraContact.Controls.Add(this._lblContact);
			this.fraContact.Controls.Add(this.txtContact);
			this.fraContact.Controls.Add(this.lblPhone);
			this.fraContact.Controls.Add(this.mskFax);
			this.fraContact.Controls.Add(this._lbleMail);
			this.fraContact.Controls.Add(this._lblFax);
			this.fraContact.Controls.Add(this.mskPhone);
			this.fraContact.Controls.Add(this._lblExt);
			this.fraContact.Controls.Add(this.txtExt);
			this.fraContact.Location = new System.Drawing.Point(6, 189);
			this.fraContact.Name = "fraContact";
			this.fraContact.Size = new System.Drawing.Size(447, 96);
			this.fraContact.TabIndex = 31;
			this.fraContact.TabStop = false;
			this.fraContact.Text = "Contact";
			// 
			// tabFreight
			// 
			this.tabFreight.Controls.Add(this.grdFreight);
			this.tabFreight.Location = new System.Drawing.Point(4, 22);
			this.tabFreight.Name = "tabFreight";
			this.tabFreight.Size = new System.Drawing.Size(460, 298);
			this.tabFreight.TabIndex = 3;
			this.tabFreight.Text = "Freight";
			// 
			// grdFreight
			// 
			this.grdFreight.DataMember = "ClientTerminalTable";
			this.grdFreight.DataSource = this.mClientTerminalDS;
			this.grdFreight.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance1.BackColor = System.Drawing.Color.White;
			appearance1.FontData.Name = "Arial";
			appearance1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdFreight.DisplayLayout.Appearance = appearance1;
			ultraGridBand1.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn1.Header.Caption = "Select";
			ultraGridColumn1.Width = 50;
			ultraGridColumn2.Header.Caption = "Client ID";
			ultraGridColumn2.Hidden = true;
			ultraGridColumn3.Header.Caption = "Terminal ID";
			ultraGridColumn3.Hidden = true;
			ultraGridColumn4.Header.Caption = "Terminal";
			ultraGridColumn4.Width = 253;
			ultraGridColumn5.Hidden = true;
			ultraGridColumn6.Hidden = true;
			ultraGridBand1.Columns.Add(ultraGridColumn1);
			ultraGridBand1.Columns.Add(ultraGridColumn2);
			ultraGridBand1.Columns.Add(ultraGridColumn3);
			ultraGridBand1.Columns.Add(ultraGridColumn4);
			ultraGridBand1.Columns.Add(ultraGridColumn5);
			ultraGridBand1.Columns.Add(ultraGridColumn6);
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
			this.grdFreight.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance6.FontData.Name = "Verdana";
			appearance6.FontData.SizeInPoints = 8F;
			appearance6.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance6.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdFreight.DisplayLayout.CaptionAppearance = appearance6;
			this.grdFreight.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdFreight.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdFreight.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdFreight.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdFreight.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdFreight.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdFreight.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdFreight.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdFreight.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdFreight.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdFreight.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdFreight.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdFreight.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdFreight.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdFreight.Location = new System.Drawing.Point(6, 12);
			this.grdFreight.Name = "grdFreight";
			this.grdFreight.Size = new System.Drawing.Size(450, 282);
			this.grdFreight.TabIndex = 0;
			this.grdFreight.Text = "Processing Terminals";
			this.grdFreight.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnTerminalCellChange);
			// 
			// mClientTerminalDS
			// 
			this.mClientTerminalDS.DataSetName = "ClientTerminalDS";
			this.mClientTerminalDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabVendors
			// 
			this.tabVendors.Controls.Add(this.grdVendorLocs);
			this.tabVendors.Controls.Add(this.btnVendorAdd);
			this.tabVendors.Controls.Add(this.btnVendorEdit);
			this.tabVendors.Controls.Add(this.btnVendorRemove);
			this.tabVendors.Location = new System.Drawing.Point(4, 22);
			this.tabVendors.Name = "tabVendors";
			this.tabVendors.Size = new System.Drawing.Size(460, 298);
			this.tabVendors.TabIndex = 1;
			this.tabVendors.Text = "Vendor Locations";
			this.tabVendors.ToolTipText = "Client-Vendor Location Associations";
			// 
			// grdVendorLocs
			// 
			this.grdVendorLocs.ContextMenu = this.ctxVendors;
			this.grdVendorLocs.DataMember = "CVLocationViewTable";
			this.grdVendorLocs.DataSource = this.mCVLocationsDS;
			this.grdVendorLocs.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance7.BackColor = System.Drawing.Color.White;
			appearance7.FontData.Name = "Arial";
			appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdVendorLocs.DisplayLayout.Appearance = appearance7;
			ultraGridBand2.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn7.Header.VisiblePosition = 2;
			ultraGridColumn7.Hidden = true;
			ultraGridColumn8.Hidden = true;
			appearance8.TextHAlign = Infragistics.Win.HAlign.Right;
			ultraGridColumn9.CellAppearance = appearance8;
			appearance9.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance9.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridColumn9.Header.Appearance = appearance9;
			ultraGridColumn9.Header.VisiblePosition = 7;
			ultraGridColumn9.Hidden = true;
			appearance10.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance10.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance10.ImageHAlign = Infragistics.Win.HAlign.Left;
			ultraGridColumn10.Header.Appearance = appearance10;
			ultraGridColumn10.Header.Caption = "Vendor Name";
			ultraGridColumn10.Header.VisiblePosition = 0;
			ultraGridColumn10.Width = 131;
			ultraGridColumn11.Header.Caption = "Vendor Loc #";
			ultraGridColumn11.Header.VisiblePosition = 3;
			ultraGridColumn11.Width = 99;
			ultraGridColumn12.Header.VisiblePosition = 6;
			ultraGridColumn12.Hidden = true;
			ultraGridColumn13.Header.VisiblePosition = 4;
			ultraGridColumn13.Width = 161;
			ultraGridColumn14.Header.Caption = "Active";
			ultraGridColumn14.Header.VisiblePosition = 5;
			ultraGridColumn14.Width = 50;
			ultraGridColumn15.Hidden = true;
			ultraGridColumn16.Hidden = true;
			ultraGridColumn17.Hidden = true;
			ultraGridBand2.Columns.Add(ultraGridColumn7);
			ultraGridBand2.Columns.Add(ultraGridColumn8);
			ultraGridBand2.Columns.Add(ultraGridColumn9);
			ultraGridBand2.Columns.Add(ultraGridColumn10);
			ultraGridBand2.Columns.Add(ultraGridColumn11);
			ultraGridBand2.Columns.Add(ultraGridColumn12);
			ultraGridBand2.Columns.Add(ultraGridColumn13);
			ultraGridBand2.Columns.Add(ultraGridColumn14);
			ultraGridBand2.Columns.Add(ultraGridColumn15);
			ultraGridBand2.Columns.Add(ultraGridColumn16);
			ultraGridBand2.Columns.Add(ultraGridColumn17);
			appearance11.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance11.FontData.Name = "Verdana";
			appearance11.FontData.SizeInPoints = 8F;
			appearance11.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand2.Override.ActiveRowAppearance = appearance11;
			appearance12.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance12.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance12.FontData.Name = "Verdana";
			appearance12.FontData.SizeInPoints = 8F;
			appearance12.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance12.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand2.Override.HeaderAppearance = appearance12;
			appearance13.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance13.FontData.Name = "Verdana";
			appearance13.FontData.SizeInPoints = 8F;
			appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand2.Override.RowAlternateAppearance = appearance13;
			appearance14.BackColor = System.Drawing.Color.White;
			appearance14.FontData.Name = "Verdana";
			appearance14.FontData.SizeInPoints = 8F;
			appearance14.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance14.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand2.Override.RowAppearance = appearance14;
			this.grdVendorLocs.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
			appearance15.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance15.FontData.Name = "Verdana";
			appearance15.FontData.SizeInPoints = 8F;
			appearance15.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance15.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdVendorLocs.DisplayLayout.CaptionAppearance = appearance15;
			this.grdVendorLocs.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdVendorLocs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdVendorLocs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdVendorLocs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdVendorLocs.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdVendorLocs.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdVendorLocs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdVendorLocs.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdVendorLocs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdVendorLocs.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdVendorLocs.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdVendorLocs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdVendorLocs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdVendorLocs.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdVendorLocs.Location = new System.Drawing.Point(6, 12);
			this.grdVendorLocs.Name = "grdVendorLocs";
			this.grdVendorLocs.Size = new System.Drawing.Size(450, 249);
			this.grdVendorLocs.TabIndex = 30;
			this.grdVendorLocs.Text = "Vendor Location Associations";
			this.grdVendorLocs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			this.grdVendorLocs.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
			// 
			// ctxVendors
			// 
			this.ctxVendors.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.ctxVendorAdd,
																					   this.ctxVendorEdit,
																					   this.ctxVendorRemove});
			// 
			// ctxVendorAdd
			// 
			this.ctxVendorAdd.Index = 0;
			this.ctxVendorAdd.Text = "&Add Vendor Asociation";
			this.ctxVendorAdd.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxVendorEdit
			// 
			this.ctxVendorEdit.Index = 1;
			this.ctxVendorEdit.Text = "&Edit Vendor Asociation";
			this.ctxVendorEdit.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxVendorRemove
			// 
			this.ctxVendorRemove.Index = 2;
			this.ctxVendorRemove.Text = "&Remove Vendor Asociation";
			this.ctxVendorRemove.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mCVLocationsDS
			// 
			this.mCVLocationsDS.DataSetName = "CVLocationDS";
			this.mCVLocationsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// btnVendorAdd
			// 
			this.btnVendorAdd.BackColor = System.Drawing.SystemColors.Control;
			this.btnVendorAdd.Location = new System.Drawing.Point(156, 270);
			this.btnVendorAdd.Name = "btnVendorAdd";
			this.btnVendorAdd.Size = new System.Drawing.Size(96, 24);
			this.btnVendorAdd.TabIndex = 27;
			this.btnVendorAdd.Text = "&Add";
			this.btnVendorAdd.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnVendorEdit
			// 
			this.btnVendorEdit.BackColor = System.Drawing.SystemColors.Control;
			this.btnVendorEdit.Enabled = false;
			this.btnVendorEdit.Location = new System.Drawing.Point(258, 270);
			this.btnVendorEdit.Name = "btnVendorEdit";
			this.btnVendorEdit.Size = new System.Drawing.Size(96, 24);
			this.btnVendorEdit.TabIndex = 26;
			this.btnVendorEdit.Text = "&Edit";
			this.btnVendorEdit.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnVendorRemove
			// 
			this.btnVendorRemove.BackColor = System.Drawing.SystemColors.Control;
			this.btnVendorRemove.Enabled = false;
			this.btnVendorRemove.Location = new System.Drawing.Point(360, 270);
			this.btnVendorRemove.Name = "btnVendorRemove";
			this.btnVendorRemove.Size = new System.Drawing.Size(96, 24);
			this.btnVendorRemove.TabIndex = 25;
			this.btnVendorRemove.Text = "&Remove";
			this.btnVendorRemove.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// tabPaymentServices
			// 
			this.tabPaymentServices.Controls.Add(this.grdCoPayServices);
			this.tabPaymentServices.Controls.Add(this.btnPaymentServiceAdd);
			this.tabPaymentServices.Controls.Add(this.btnPaymentServiceEdit);
			this.tabPaymentServices.Controls.Add(this.btnPaymentServiceRemove);
			this.tabPaymentServices.Location = new System.Drawing.Point(4, 22);
			this.tabPaymentServices.Name = "tabPaymentServices";
			this.tabPaymentServices.Size = new System.Drawing.Size(460, 298);
			this.tabPaymentServices.TabIndex = 2;
			this.tabPaymentServices.Text = "Payment Services";
			this.tabPaymentServices.ToolTipText = "Payment Service Associations";
			// 
			// grdCoPayServices
			// 
			this.grdCoPayServices.ContextMenu = this.ctxPaymentServices;
			this.grdCoPayServices.DataMember = "CompanyPaymentServiceTable";
			this.grdCoPayServices.DataSource = this.mClientDS;
			this.grdCoPayServices.DisplayLayout.AddNewBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			appearance16.BackColor = System.Drawing.Color.White;
			appearance16.FontData.Name = "Arial";
			appearance16.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grdCoPayServices.DisplayLayout.Appearance = appearance16;
			ultraGridBand3.AddButtonCaption = "FreightAssignmentListForStationTable";
			ultraGridColumn18.Hidden = true;
			ultraGridColumn19.Hidden = true;
			ultraGridColumn20.Header.Caption = "PS Name";
			ultraGridColumn20.Width = 136;
			ultraGridColumn21.Header.Caption = "PS #";
			ultraGridColumn21.Width = 96;
			ultraGridColumn22.Header.Caption = "Active";
			ultraGridColumn22.Header.VisiblePosition = 5;
			ultraGridColumn22.Width = 58;
			ultraGridColumn23.Header.VisiblePosition = 4;
			ultraGridColumn23.Width = 152;
			ultraGridColumn24.Header.VisiblePosition = 8;
			ultraGridColumn24.Hidden = true;
			ultraGridColumn25.Hidden = true;
			ultraGridColumn26.Header.VisiblePosition = 6;
			ultraGridColumn26.Hidden = true;
			ultraGridBand3.Columns.Add(ultraGridColumn18);
			ultraGridBand3.Columns.Add(ultraGridColumn19);
			ultraGridBand3.Columns.Add(ultraGridColumn20);
			ultraGridBand3.Columns.Add(ultraGridColumn21);
			ultraGridBand3.Columns.Add(ultraGridColumn22);
			ultraGridBand3.Columns.Add(ultraGridColumn23);
			ultraGridBand3.Columns.Add(ultraGridColumn24);
			ultraGridBand3.Columns.Add(ultraGridColumn25);
			ultraGridBand3.Columns.Add(ultraGridColumn26);
			appearance17.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(220)), ((System.Byte)(255)), ((System.Byte)(200)));
			appearance17.FontData.Name = "Verdana";
			appearance17.FontData.SizeInPoints = 8F;
			appearance17.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.ActiveRowAppearance = appearance17;
			appearance18.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(192)), ((System.Byte)(255)));
			appearance18.FontData.Bold = Infragistics.Win.DefaultableBoolean.True;
			appearance18.FontData.Name = "Verdana";
			appearance18.FontData.SizeInPoints = 8F;
			appearance18.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance18.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.HeaderAppearance = appearance18;
			appearance19.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(240)), ((System.Byte)(240)), ((System.Byte)(255)));
			appearance19.FontData.Name = "Verdana";
			appearance19.FontData.SizeInPoints = 8F;
			appearance19.ForeColor = System.Drawing.SystemColors.ControlText;
			ultraGridBand3.Override.RowAlternateAppearance = appearance19;
			appearance20.BackColor = System.Drawing.Color.White;
			appearance20.FontData.Name = "Verdana";
			appearance20.FontData.SizeInPoints = 8F;
			appearance20.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance20.TextHAlign = Infragistics.Win.HAlign.Left;
			ultraGridBand3.Override.RowAppearance = appearance20;
			this.grdCoPayServices.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
			appearance21.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(128)), ((System.Byte)(255)));
			appearance21.FontData.Name = "Verdana";
			appearance21.FontData.SizeInPoints = 8F;
			appearance21.ForeColor = System.Drawing.SystemColors.ControlText;
			appearance21.TextHAlign = Infragistics.Win.HAlign.Left;
			this.grdCoPayServices.DisplayLayout.CaptionAppearance = appearance21;
			this.grdCoPayServices.DisplayLayout.GroupByBox.ButtonConnectorColor = System.Drawing.Color.Empty;
			this.grdCoPayServices.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdCoPayServices.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdCoPayServices.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdCoPayServices.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None;
			this.grdCoPayServices.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.None;
			this.grdCoPayServices.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			this.grdCoPayServices.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
			this.grdCoPayServices.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdCoPayServices.DisplayLayout.Override.MaxSelectedCells = 1;
			this.grdCoPayServices.DisplayLayout.Override.MaxSelectedRows = 1;
			this.grdCoPayServices.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdCoPayServices.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdCoPayServices.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdCoPayServices.Location = new System.Drawing.Point(6, 12);
			this.grdCoPayServices.Name = "grdCoPayServices";
			this.grdCoPayServices.Size = new System.Drawing.Size(450, 249);
			this.grdCoPayServices.TabIndex = 35;
			this.grdCoPayServices.Text = "Payment Service Associations";
			this.grdCoPayServices.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			this.grdCoPayServices.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
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
			// dlgClientDetail
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
			this.Name = "dlgClientDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Client Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraAddress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mClientDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraContact.ResumeLayout(false);
			this.tabFreight.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdFreight)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mClientTerminalDS)).EndInit();
			this.tabVendors.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdVendorLocs)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCVLocationsDS)).EndInit();
			this.tabPaymentServices.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdCoPayServices)).EndInit();
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
				this.mCountriesDS.Merge(EnterpriseFactory.GetCountries());
				this.mStatesDS.Merge(EnterpriseFactory.GetStates());
				
				//Set control services
				#region Default grid behavior
				this.grdVendorLocs.Text = GRID_TYPE_VENDORS;
				//this.grdVendorLocs.DataSource = this.mCVLocationsDS;
				this.grdVendorLocs.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
				//this.grdVendorLocs.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdVendorLocs.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
				this.grdCoPayServices.DisplayLayout.CaptionAppearance.FontData.SizeInPoints = 8;
				this.grdCoPayServices.Text = GRID_TYPE_PAYMENTSERVICES;
				this.grdCoPayServices.DataSource = this.mClientDS;
				this.grdCoPayServices.DisplayLayout.Override.AllowAddNew = AllowAddNew.Yes;
				//this.grdCoPayServices.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdCoPayServices.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
				#endregion
				#region Default grid behavior
				this.grdFreight.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdFreight.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdFreight.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdFreight.DisplayLayout.Bands[0].Columns["Selected"].CellActivation = Activation.AllowEdit;
				this.grdFreight.DisplayLayout.Bands[0].Columns["ClientID"].CellActivation = Activation.NoEdit;
				this.grdFreight.DisplayLayout.Bands[0].Columns["TerminalID"].CellActivation = Activation.NoEdit;
				this.grdFreight.DisplayLayout.Bands[0].Columns["TerminalName"].CellActivation = Activation.NoEdit;
				this.grdFreight.DisplayLayout.Bands[0].Columns["LastUpdated"].CellActivation = Activation.NoEdit;
				this.grdFreight.DisplayLayout.Bands[0].Columns["UserID"].CellActivation = Activation.NoEdit;
				#endregion
				this.txtNumber.MaxLength = 8;
				this.txtNumber.Text = this.mClientDS.ClientDetailTable[0].Number;
				this.txtNumber.Enabled = (mClientID==0);
				this.txtName.MaxLength = 30;
				this.txtName.Text = this.mClientDS.ClientDetailTable[0].ClientName;
				this.txtMnemonic.MaxLength = 3;
				this.txtMnemonic.Text = "";
				if(!this.mClientDS.ClientDetailTable[0].IsMnemonicNull())
					this.txtMnemonic.Text = this.mClientDS.ClientDetailTable[0].Mnemonic;
				
				this.txtLine1.MaxLength = 40;
				this.txtLine1.Text = this.mClientDS.ClientDetailTable[0].AddressLine1;
				this.txtLine2.MaxLength = 40;
				if(!this.mClientDS.ClientDetailTable[0].IsAddressLine2Null())
					this.txtLine2.Text = this.mClientDS.ClientDetailTable[0].AddressLine2;
				this.txtCity.MaxLength = 40;
				this.txtCity.Text = this.mClientDS.ClientDetailTable[0].City;
				if(!this.mClientDS.ClientDetailTable[0].IsStateOrProvinceNull()) 
					this.cboStates.SelectedValue = this.mClientDS.ClientDetailTable[0].StateOrProvince;
				else
					if(this.cboStates.Items.Count>0) this.cboStates.SelectedIndex = 0;
				this.cboStates.Enabled = (this.cboStates.Items.Count>0);
				this.txtZip.MaxLength = 15;
				if(!this.mClientDS.ClientDetailTable[0].IsPostalCodeNull())
					this.txtZip.Text = this.mClientDS.ClientDetailTable[0].PostalCode;
				if(this.mClientDS.ClientDetailTable[0].CountryID>0) 
					this.cboCountries.SelectedValue = this.mClientDS.ClientDetailTable[0].CountryID;
				else
					if(this.cboCountries.Items.Count>0) this.cboCountries.SelectedIndex = 0;
				this.cboCountries.Enabled = (this.cboCountries.Items.Count>0);
				
				this.txtContact.MaxLength = 30;
				if(!this.mClientDS.ClientDetailTable[0].IsContactNameNull())
					this.txtContact.Text = this.mClientDS.ClientDetailTable[0].ContactName;
				this.mskPhone.InputMask = "###-###-####";
				if(!this.mClientDS.ClientDetailTable[0].IsPhoneNull())
					this.mskPhone.Value = this.mClientDS.ClientDetailTable[0].Phone;
				this.txtExt.MaxLength = 4;
				if(!this.mClientDS.ClientDetailTable[0].IsExtensionNull())
					this.txtExt.Text = this.mClientDS.ClientDetailTable[0].Extension;
				this.mskFax.InputMask = "###-###-####";
				if(!this.mClientDS.ClientDetailTable[0].IsFaxNull())
					this.mskFax.Value = this.mClientDS.ClientDetailTable[0].Fax;
				if(!this.mClientDS.ClientDetailTable[0].IsEmailNull())
					this.txteMail.Text = this.mClientDS.ClientDetailTable[0].Email;
				
				this.chkStatus.Checked = this.mClientDS.ClientDetailTable[0].IsActive;
				
				this.mClientTerminalDS.Merge(EnterpriseFactory.GetFreightProcessingTerminalTemplate());
				if(this.mClientID>0) {
					//Copy data from each exisiting terminal into its corresponding row in the template
					for(int i=0; i<this.mClientDS.ClientTerminalTable.Rows.Count; i++) {
						//Find the row by terminal ID (unique for each row)
						int iTermID = this.mClientDS.ClientTerminalTable[i].TerminalID;
						ClientTerminalDS.ClientTerminalTableRow row = (ClientTerminalDS.ClientTerminalTableRow)this.mClientTerminalDS.ClientTerminalTable.Select("TerminalID='" + iTermID + "'")[0];
						if(row!=null) {
							row.Selected = true;
							row.ClientID = this.mClientDS.ClientTerminalTable[i].ClientID;
						}
					}
					
					//Set grid selection to first row (invoke click event)
					if(this.grdVendorLocs.Rows.Count>0) {
						this.grdVendorLocs.ActiveRow = this.grdVendorLocs.Rows[0];
						this.grdVendorLocs.ActiveRow.Selected = true;
					}
					else
						this.setUserServices(false, GRID_TYPE_VENDORS);

					//Set grid selection to first row (invoke click event)
					if(this.grdCoPayServices.Rows.Count>0) {
						this.grdCoPayServices.ActiveRow = this.grdCoPayServices.Rows[0];
						this.grdCoPayServices.ActiveRow.Selected = true;
					}
					else
						this.setUserServices(false, GRID_TYPE_PAYMENTSERVICES);
				
					//Do not allow associations until client is created
					this.tabVendors.Enabled = (this.mClientID!=0);
					this.tabPaymentServices.Enabled = (this.mClientID!=0);
					this.btnOk.Enabled = false;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnCountryChanged(object sender, System.EventArgs e) {
			//Event handler for user changing country selection
			try {
				//Update state list
				this.cboStates.Enabled = (this.cboCountries.Text=="USA" && this.cboStates.Items.Count>0);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnTerminalCellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
			//Event handler for user changing terminal selection
			try {
				//Validate form
				this.ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region Grid Control: OnGridSelectionChanged(), OnGridMouseDown(), SetGridRowActive()
		private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for after selection changes
			//This event will cover mouse left-click and keyboard changes
			try {
				//Set menu and toolbar sevices for selected row
				UltraGrid grd = (UltraGrid)sender;
				if(grd.Text==GRID_TYPE_VENDORS) 
					setUserServices(true, GRID_TYPE_VENDORS);
				else if(grd.Text==GRID_TYPE_PAYMENTSERVICES) 
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
				if(grd.Text==GRID_TYPE_VENDORS) {
					isItem = SetGridRowActive(e, GRID_TYPE_VENDORS);
					setUserServices(isItem, GRID_TYPE_VENDORS);
				}
				else if(grd.Text==GRID_TYPE_PAYMENTSERVICES) {
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
				if(type==GRID_TYPE_VENDORS) 
					formGrid = this.grdVendorLocs;
				else if(type==GRID_TYPE_PAYMENTSERVICES) 
					formGrid = this.grdCoPayServices;
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
			//Event handler for changes to control data
			try {
				if(this.mClientDS.ClientDetailTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtNumber.Text!="" && this.txtName.Text!="" && 
						this.txtMnemonic.Text!="" && this.txtLine1.Text!="" && this.txtCity.Text!="" && 
						this.cboStates.Text!="" && this.txtZip.Text!="" && this.cboCountries.Text!="" && 
						this.mskPhone.Value!=System.DBNull.Value);
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			bool bSelected=false, bHasLabelID=false;
			ClientDS.ClientTerminalTableRow row;
			this.Cursor = Cursors.WaitCursor;

			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_VENDOR_ADD:			this.ctxVendorAdd.PerformClick(); break;
					case CMD_VENDOR_EDIT:			this.ctxVendorEdit.PerformClick(); break;
					case CMD_VENDOR_REMOVE:			this.ctxVendorRemove.PerformClick(); break;
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
						this.mClientDS.ClientDetailTable[0].Number = this.txtNumber.Text;
						this.mClientDS.ClientDetailTable[0].ClientName = this.txtName.Text;

						if(this.txtMnemonic.Text!="")
							this.mClientDS.ClientDetailTable[0].Mnemonic = this.txtMnemonic.Text;
						else
							this.mClientDS.ClientDetailTable[0].SetMnemonicNull();
						
						this.mClientDS.ClientDetailTable[0].AddressLine1 = this.txtLine1.Text;
						if(this.txtLine2.Text!="")
							this.mClientDS.ClientDetailTable[0].AddressLine2 = this.txtLine2.Text;
						else
							this.mClientDS.ClientDetailTable[0].SetAddressLine2Null();
						this.mClientDS.ClientDetailTable[0].City = this.txtCity.Text;
						this.mClientDS.ClientDetailTable[0].StateOrProvince = this.cboStates.SelectedValue.ToString();
						this.mClientDS.ClientDetailTable[0].PostalCode = this.txtZip.Text;
						this.mClientDS.ClientDetailTable[0].CountryID = (int)this.cboCountries.SelectedValue;
						
						if(this.txtContact.Text!="")
							this.mClientDS.ClientDetailTable[0].ContactName = this.txtContact.Text;
						else
							this.mClientDS.ClientDetailTable[0].SetContactNameNull();
						this.mClientDS.ClientDetailTable[0].Phone = this.mskPhone.Value.ToString();
						if(this.txtExt.Text!="")
							this.mClientDS.ClientDetailTable[0].Extension = this.txtExt.Text;
						else
							this.mClientDS.ClientDetailTable[0].SetExtensionNull();
						if(this.mskFax.Value!=System.DBNull.Value)
							this.mClientDS.ClientDetailTable[0].Fax = this.mskFax.Value.ToString();
						else
							this.mClientDS.ClientDetailTable[0].SetFaxNull();
						if(this.txteMail.Text!="")
							this.mClientDS.ClientDetailTable[0].Email = this.txteMail.Text;
						else
							this.mClientDS.ClientDetailTable[0].SetEmailNull();
						this.mClientDS.ClientDetailTable[0].IsActive = this.chkStatus.Checked;

						for(int i=0; i<this.mClientTerminalDS.ClientTerminalTable.Rows.Count; i++) {
							bSelected = this.mClientTerminalDS.ClientTerminalTable[i].Selected;
							bHasLabelID = (this.mClientTerminalDS.ClientTerminalTable[i].ClientID>0) ? true : false;
							if(bSelected && !bHasLabelID) {
								//Add a new data element
								//*** RESULT MUST MARK ROW AS DataRowState.Added
								row = this.mClientDS.ClientTerminalTable.NewClientTerminalTableRow();
								row.ClientID = this.mClientID;
								row.TerminalID = this.mClientTerminalDS.ClientTerminalTable[i].TerminalID;
								row.TerminalName = this.mClientTerminalDS.ClientTerminalTable[i].TerminalName;
								row.LastUpdated = this.mClientTerminalDS.ClientTerminalTable[i].LastUpdated;
								row.UserID = this.mClientTerminalDS.ClientTerminalTable[i].UserID;
								this.mClientDS.ClientTerminalTable.AddClientTerminalTableRow(row);
							}
							else if(!bSelected && bHasLabelID) {
								//Remove current data element
								//*** RESULT MUST MARK ROW AS DataRowState.Deleted
								row = (ClientDS.ClientTerminalTableRow)this.mClientDS.ClientTerminalTable.Select("TerminalID='" + this.mClientTerminalDS.ClientTerminalTable[i].TerminalID + "'")[0];
								row.Delete();
							}
						}
						#region Test update to mClientDS.ClientTerminalTable
						Debug.Write("\n");
						for(int i=0; i<this.mClientDS.ClientTerminalTable.Rows.Count; i++) {
							switch(this.mClientDS.ClientTerminalTable[i].RowState) {
								case		DataRowState.Added: Debug.Write("Added terminal (labelID=)\n"); break;
								case		DataRowState.Modified: Debug.Write("Modified terminal (labelID=)\n"); break;
								case		DataRowState.Deleted: Debug.Write("Removed terminal (labelID=)\n"); break;
								default:	Debug.Write(this.mClientDS.ClientTerminalTable[i].RowState.ToString() + " terminal (labelID=)\n"); break;
							}
						}
						#endregion
						//this.mClientDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu item clicked - apply selected service
			int iCVLID = 0;
			dlgClientVendorDetail dlgCVL = null;
			CVLocationDS dsCVL = null;
			int iCPSID = 0;
			ClientDS.CompanyPaymentServiceTableRow rowCPS;
			dlgCompanyPaymentService dlgCPS = null;
			CompanyDS dsCPS = null;
			CompanyDS.CompanyPaymentServiceTableRow _rowCPS;
			string userID = Environment.UserName;
			DateTime updated = DateTime.Now;
			DialogResult res;
			bool val=false;

			this.Cursor = Cursors.WaitCursor;
			try {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text) {
					case MNU_VENDORLOCATION_ADD:
						//Add a new client-vendor association to the list
						iCVLID = 0;
						dsCVL = EnterpriseFactory.GetCVLocation(iCVLID);
						dsCVL.CVLocationDetailTable[0].ClientID = this.mClientID;
						dlgCVL = new dlgClientVendorDetail(this.mClientID, this.chkStatus.Checked, ref dsCVL);
						res = dlgCVL.ShowDialog(this);
						if(res==DialogResult.OK) {
							//Add new client-vendor location association
							iCVLID = EnterpriseFactory.CreateCVLocation(dsCVL);
							if(iCVLID > 0) {
								MessageBox.Show("Client-vendor location association " + iCVLID.ToString() + " was created.", this.Name, MessageBoxButtons.OK);
								this.mCVLocationsDS.Clear();
								this.mCVLocationsDS.Merge(EnterpriseFactory.ViewCVLocations(this.mClientID));
								this.grdVendorLocs.Refresh();
							}
							else
								MessageBox.Show("New client-vendor location association could not be created. Please try again.", this.Name, MessageBoxButtons.OK);
						}
						break;
					case MNU_VENDORLOCATION_EDIT:
						//Edit an existing client-vendor association in the list
						iCVLID = (int)this.grdVendorLocs.Selected.Rows[0].Cells["LinkID"].Value;
						dsCVL = EnterpriseFactory.GetCVLocation(iCVLID);
						dsCVL.CVLocationDetailTable[0].ClientID = this.mClientID;
						dsCVL.CVLocationDetailTable[0].VendorName = this.grdVendorLocs.Selected.Rows[0].Cells["VendorName"].Value.ToString();
						dsCVL.CVLocationDetailTable[0].Number = this.grdVendorLocs.Selected.Rows[0].Cells["Number"].Value.ToString();
						dsCVL.CVLocationDetailTable[0].Description = this.grdVendorLocs.Selected.Rows[0].Cells["Description"].Value.ToString();
						dlgCVL = new dlgClientVendorDetail(this.mClientID, this.chkStatus.Checked, ref dsCVL);
						res = dlgCVL.ShowDialog(this);
						if(res==DialogResult.OK) {
							//Update client-vendor location association- detail data does not exist in the
							val = EnterpriseFactory.UpdateCVLocation(dsCVL);
							if(val) {
								MessageBox.Show("Client-vendor location association " + iCVLID.ToString() + " was updated.", this.Name, MessageBoxButtons.OK);
								this.mCVLocationsDS.Clear();
								this.mCVLocationsDS.Merge(EnterpriseFactory.ViewCVLocations(this.mClientID));
								this.grdVendorLocs.Refresh();
							}
							else
								MessageBox.Show("Client-vendor location association " + iCVLID.ToString() + " could not be updated. Please try again.", this.Name, MessageBoxButtons.OK);
						}
						break;
					case MNU_VENDORLOCATION_REMOVE: break;
					case MNU_PAYMENTSERVICE_ADD:
						//Add a new item to the list
						iCPSID = 0;
						dsCPS = new CompanyDS();
						_rowCPS = dsCPS.CompanyPaymentServiceTable.NewCompanyPaymentServiceTableRow();
						_rowCPS.CompanyID = this.mClientID;
						_rowCPS.PaymentServiceID = iCPSID;
						_rowCPS.PaymentServiceName = "";
						_rowCPS.Comments = "";
						_rowCPS.IsActive = true;
						_rowCPS.LastUpdated = DateTime.Now;
						_rowCPS.UserID = Environment.UserName;
						_rowCPS.RowVersion = "";
						dsCPS.CompanyPaymentServiceTable.AddCompanyPaymentServiceTableRow(_rowCPS);
						dlgCPS = new dlgCompanyPaymentService(this.mClientID, this.chkStatus.Checked, ref dsCPS);
						res = dlgCPS.ShowDialog(this);
						if(res==DialogResult.OK) {
							//Added new association
							//*** RESULT MUST MARK ROW AS DataRowState.Added
							rowCPS = this.mClientDS.CompanyPaymentServiceTable.NewCompanyPaymentServiceTableRow();
							rowCPS.ItemArray = dsCPS.CompanyPaymentServiceTable[0].ItemArray;
							this.mClientDS.CompanyPaymentServiceTable.AddCompanyPaymentServiceTableRow(rowCPS);
						}
						break;
					case MNU_PAYMENTSERVICE_EDIT:
						//Update an existing item in the list
						iCPSID = (int)this.grdCoPayServices.Selected.Rows[0].Cells["PaymentServiceID"].Value;
						dsCPS = new CompanyDS();
						_rowCPS = dsCPS.CompanyPaymentServiceTable.NewCompanyPaymentServiceTableRow();
						_rowCPS.CompanyID = this.mClientID;
						_rowCPS.PaymentServiceID = iCPSID;
						_rowCPS.PaymentServiceName = this.grdCoPayServices.Selected.Rows[0].Cells["PaymentServiceName"].Value.ToString();
						if(this.grdCoPayServices.Selected.Rows[0].Cells["Comments"].Value!=DBNull.Value)
							_rowCPS.Comments = this.grdCoPayServices.Selected.Rows[0].Cells["Comments"].Value.ToString();
						_rowCPS.IsActive = Convert.ToBoolean(this.grdCoPayServices.Selected.Rows[0].Cells["IsActive"].Value);
						_rowCPS.LastUpdated = Convert.ToDateTime(this.grdCoPayServices.Selected.Rows[0].Cells["LastUpdated"].Value);
						_rowCPS.UserID = this.grdCoPayServices.Selected.Rows[0].Cells["UserID"].Value.ToString();
						_rowCPS.RowVersion = this.grdCoPayServices.Selected.Rows[0].Cells["RowVersion"].Value.ToString();
						dsCPS.CompanyPaymentServiceTable.AddCompanyPaymentServiceTableRow(_rowCPS);
						dlgCPS = new dlgCompanyPaymentService(this.mClientID, this.chkStatus.Checked, ref dsCPS);
						res = dlgCPS.ShowDialog(this);
						if(res==DialogResult.OK) {
							//Update in detail dataset m_dsPaymentServices.CompanyPaymentServiceTable
							//*** RESULT MUST MARK ROW AS DataRowState.Modified
							this.grdCoPayServices.Selected.Rows[0].Cells["Comments"].Value = dsCPS.CompanyPaymentServiceTable[0].Comments;
							this.grdCoPayServices.Selected.Rows[0].Cells["IsActive"].Value = dsCPS.CompanyPaymentServiceTable[0].IsActive;
							this.grdCoPayServices.Selected.Rows[0].Update();
						}
						break; 
					case MNU_PAYMENTSERVICE_REMOVE:
						//Delete an existing item in the list
						iCPSID = (int)this.grdCoPayServices.Selected.Rows[0].Cells["PaymentServiceID"].Value;
						res = MessageBox.Show(this, "Delete association with payment service " + iCPSID + "?", this.Name, MessageBoxButtons.OKCancel);
						if(res==DialogResult.OK) {
							//Removed association
							//*** RESULT MUST MARK ROW AS DataRowState.Deleted
							this.grdCoPayServices.Selected.Rows[0].Delete();
						}
						break;
					default:
						Debug.Write("Need handler for " + menu.Text + "\n");
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
			
			try {
				//Set main menu, context menu, and toolbar states
				if(type==GRID_TYPE_VENDORS) {
					this.btnVendorAdd.Enabled = this.ctxVendorAdd.Enabled = (!isItem && mClientID>0);
					this.btnVendorEdit.Enabled = this.ctxVendorEdit.Enabled = (isItem && mClientID>0);
					this.btnVendorRemove.Enabled = this.ctxVendorRemove.Enabled = false;
				}
				else if(type==GRID_TYPE_PAYMENTSERVICES) {
					this.btnPaymentServiceAdd.Enabled = this.ctxPaymentServiceAdd.Enabled = (!isItem && mClientID>0);
					this.btnPaymentServiceEdit.Enabled = this.ctxPaymentServiceEdit.Enabled = (isItem && mClientID>0);
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