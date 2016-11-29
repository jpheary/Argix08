//	File:	dlgaddress.cs
//	Author:	J. Heary
//	Date:	04/27/06
//	Desc:	Dialog to create a new address or edit an existing address.
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
	public class dlgAddressDetail : System.Windows.Forms.Form {
		//Members
		private int mAddressID=0;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox grpDetails;
		private System.Windows.Forms.Label _lblState;
		private System.Windows.Forms.Label _lblLine1;
		private System.Windows.Forms.Label _lblLine2;
		private System.Windows.Forms.TextBox txtLine2;
		private System.Windows.Forms.TextBox txtLine1;
		private System.Windows.Forms.Label _lblCity;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.Label _lblZip;
		private System.Windows.Forms.ComboBox cboStates;
		private System.Windows.Forms.TextBox txtZip;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.Label _lblType;
		private System.Windows.Forms.ComboBox cboTypes;
		private Tsort.Enterprise.AddressDS mAddressDS;
		private Tsort.Enterprise.AddressTypeDS mTypesDS;
		private Tsort.Enterprise.StateDS mStatesDS;
		private Tsort.Enterprise.CountryDS mCountriesDS;
		private System.Windows.Forms.Label _lblCountry;
		private System.Windows.Forms.ComboBox cboCountries;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		private const string COUNTRY_USA = "USA";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgAddressDetail(ref AddressDS address) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mAddressDS = address;
				if(this.mAddressDS.AddressViewTable.Count > 0) {
					this.mAddressID = this.mAddressDS.AddressViewTable[0].AddressID;
					this.Text = (this.mAddressID>0) ? "Address (" + this.mAddressID + ")" : "Address (New)";
				}
				else
					this.Text = "Address (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgAddressDetail));
			this.cboStates = new System.Windows.Forms.ComboBox();
			this.mStatesDS = new Tsort.Enterprise.StateDS();
			this._lblState = new System.Windows.Forms.Label();
			this._lblLine1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.grpDetails = new System.Windows.Forms.GroupBox();
			this._lblCountry = new System.Windows.Forms.Label();
			this.cboCountries = new System.Windows.Forms.ComboBox();
			this.mCountriesDS = new Tsort.Enterprise.CountryDS();
			this._lblType = new System.Windows.Forms.Label();
			this.cboTypes = new System.Windows.Forms.ComboBox();
			this.mTypesDS = new Tsort.Enterprise.AddressTypeDS();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this._lblZip = new System.Windows.Forms.Label();
			this.txtZip = new System.Windows.Forms.TextBox();
			this._lblCity = new System.Windows.Forms.Label();
			this.txtCity = new System.Windows.Forms.TextBox();
			this._lblLine2 = new System.Windows.Forms.Label();
			this.txtLine2 = new System.Windows.Forms.TextBox();
			this.txtLine1 = new System.Windows.Forms.TextBox();
			this.mAddressDS = new Tsort.Enterprise.AddressDS();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).BeginInit();
			this.grpDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mTypesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mAddressDS)).BeginInit();
			this.SuspendLayout();
			// 
			// cboStates
			// 
			this.cboStates.DataSource = this.mStatesDS;
			this.cboStates.DisplayMember = "StateListTable.STATE";
			this.cboStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboStates.Location = new System.Drawing.Point(84, 123);
			this.cboStates.Name = "cboStates";
			this.cboStates.Size = new System.Drawing.Size(72, 21);
			this.cboStates.TabIndex = 5;
			this.cboStates.ValueMember = "StateListTable.STATE";
			this.cboStates.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mStatesDS
			// 
			this.mStatesDS.DataSetName = "StateDS";
			this.mStatesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblState
			// 
			this._lblState.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblState.Location = new System.Drawing.Point(6, 123);
			this._lblState.Name = "_lblState";
			this._lblState.Size = new System.Drawing.Size(72, 16);
			this._lblState.TabIndex = 2;
			this._lblState.Text = "State";
			this._lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblLine1
			// 
			this._lblLine1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLine1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLine1.Location = new System.Drawing.Point(6, 51);
			this._lblLine1.Name = "_lblLine1";
			this._lblLine1.Size = new System.Drawing.Size(72, 16);
			this._lblLine1.TabIndex = 13;
			this._lblLine1.Text = "Line 1";
			this._lblLine1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.grpDetails.Controls.Add(this._lblCountry);
			this.grpDetails.Controls.Add(this.cboCountries);
			this.grpDetails.Controls.Add(this._lblType);
			this.grpDetails.Controls.Add(this.cboTypes);
			this.grpDetails.Controls.Add(this.chkStatus);
			this.grpDetails.Controls.Add(this._lblZip);
			this.grpDetails.Controls.Add(this.txtZip);
			this.grpDetails.Controls.Add(this._lblCity);
			this.grpDetails.Controls.Add(this.txtCity);
			this.grpDetails.Controls.Add(this._lblState);
			this.grpDetails.Controls.Add(this.cboStates);
			this.grpDetails.Controls.Add(this._lblLine2);
			this.grpDetails.Controls.Add(this.txtLine2);
			this.grpDetails.Controls.Add(this._lblLine1);
			this.grpDetails.Controls.Add(this.txtLine1);
			this.grpDetails.Location = new System.Drawing.Point(6, 6);
			this.grpDetails.Name = "grpDetails";
			this.grpDetails.Size = new System.Drawing.Size(366, 222);
			this.grpDetails.TabIndex = 1;
			this.grpDetails.TabStop = false;
			this.grpDetails.Text = "Address";
			// 
			// _lblCountry
			// 
			this._lblCountry.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCountry.Location = new System.Drawing.Point(6, 150);
			this._lblCountry.Name = "_lblCountry";
			this._lblCountry.Size = new System.Drawing.Size(72, 16);
			this._lblCountry.TabIndex = 23;
			this._lblCountry.Text = "Country";
			this._lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboCountries
			// 
			this.cboCountries.DataSource = this.mCountriesDS;
			this.cboCountries.DisplayMember = "CountryDetailTable.Country";
			this.cboCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCountries.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboCountries.Location = new System.Drawing.Point(84, 150);
			this.cboCountries.Name = "cboCountries";
			this.cboCountries.Size = new System.Drawing.Size(144, 21);
			this.cboCountries.TabIndex = 4;
			this.cboCountries.ValueMember = "CountryDetailTable.CountryID";
			this.cboCountries.SelectedValueChanged += new System.EventHandler(this.OnCountryChanged);
			this.cboCountries.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mCountriesDS
			// 
			this.mCountriesDS.DataSetName = "CountryDS";
			this.mCountriesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblType
			// 
			this._lblType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblType.Location = new System.Drawing.Point(6, 24);
			this._lblType.Name = "_lblType";
			this._lblType.Size = new System.Drawing.Size(72, 16);
			this._lblType.TabIndex = 21;
			this._lblType.Text = "Type";
			this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboTypes
			// 
			this.cboTypes.DataSource = this.mTypesDS;
			this.cboTypes.DisplayMember = "AddressTypeListTable.AddressType";
			this.cboTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTypes.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboTypes.Location = new System.Drawing.Point(84, 24);
			this.cboTypes.Name = "cboTypes";
			this.cboTypes.Size = new System.Drawing.Size(96, 21);
			this.cboTypes.TabIndex = 0;
			this.cboTypes.ValueMember = "AddressTypeListTable.AddressType";
			this.cboTypes.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mTypesDS
			// 
			this.mTypesDS.DataSetName = "AddressDS";
			this.mTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(84, 195);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(66, 18);
			this.chkStatus.TabIndex = 7;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblZip
			// 
			this._lblZip.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblZip.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblZip.Location = new System.Drawing.Point(252, 123);
			this._lblZip.Name = "_lblZip";
			this._lblZip.Size = new System.Drawing.Size(24, 16);
			this._lblZip.TabIndex = 19;
			this._lblZip.Text = "Zip";
			this._lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtZip
			// 
			this.txtZip.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtZip.Location = new System.Drawing.Point(282, 123);
			this.txtZip.MaxLength = 5;
			this.txtZip.Name = "txtZip";
			this.txtZip.Size = new System.Drawing.Size(72, 21);
			this.txtZip.TabIndex = 6;
			this.txtZip.Text = "";
			this.txtZip.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblCity
			// 
			this._lblCity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCity.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblCity.Location = new System.Drawing.Point(6, 99);
			this._lblCity.Name = "_lblCity";
			this._lblCity.Size = new System.Drawing.Size(72, 16);
			this._lblCity.TabIndex = 17;
			this._lblCity.Text = "City";
			this._lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCity
			// 
			this.txtCity.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtCity.Location = new System.Drawing.Point(84, 99);
			this.txtCity.Name = "txtCity";
			this.txtCity.Size = new System.Drawing.Size(144, 21);
			this.txtCity.TabIndex = 3;
			this.txtCity.Text = "";
			this.txtCity.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblLine2
			// 
			this._lblLine2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLine2.Location = new System.Drawing.Point(6, 75);
			this._lblLine2.Name = "_lblLine2";
			this._lblLine2.Size = new System.Drawing.Size(72, 16);
			this._lblLine2.TabIndex = 15;
			this._lblLine2.Text = "Line 2";
			this._lblLine2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLine2
			// 
			this.txtLine2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine2.Location = new System.Drawing.Point(84, 75);
			this.txtLine2.Name = "txtLine2";
			this.txtLine2.Size = new System.Drawing.Size(273, 21);
			this.txtLine2.TabIndex = 2;
			this.txtLine2.Text = "";
			this.txtLine2.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtLine1
			// 
			this.txtLine1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine1.Location = new System.Drawing.Point(84, 51);
			this.txtLine1.Name = "txtLine1";
			this.txtLine1.Size = new System.Drawing.Size(273, 21);
			this.txtLine1.TabIndex = 1;
			this.txtLine1.Text = "";
			this.txtLine1.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mAddressDS
			// 
			this.mAddressDS.DataSetName = "AddressList";
			this.mAddressDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// dlgAddressDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(378, 263);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.grpDetails);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgAddressDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Address Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).EndInit();
			this.grpDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mTypesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mAddressDS)).EndInit();
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
				
				//Get selection lists
				this.mTypesDS.Merge(EnterpriseFactory.GetAddressTypes());
				this.mCountriesDS.Merge(EnterpriseFactory.GetCountries());
				this.mStatesDS.Merge(EnterpriseFactory.GetStates());
				
				//Load details
				if(this.mAddressDS.AddressViewTable[0].AddressType!="") 
					this.cboTypes.SelectedValue = this.mAddressDS.AddressViewTable[0].AddressType;
				else
					if(this.cboTypes.Items.Count>0) this.cboTypes.SelectedIndex = 0;
				this.cboTypes.Enabled = false;
				this.txtLine1.MaxLength = 40;
				this.txtLine1.Text = this.mAddressDS.AddressViewTable[0].AddressLine1;
				this.txtLine2.MaxLength = 40;
				this.txtLine2.Text = "";
				if(!this.mAddressDS.AddressViewTable[0].IsAddressLine2Null())
					this.txtLine2.Text = this.mAddressDS.AddressViewTable[0].AddressLine2;
				this.txtCity.MaxLength = 40;
				this.txtCity.Text = this.mAddressDS.AddressViewTable[0].City;
				if(!this.mAddressDS.AddressViewTable[0].IsStateOrProvinceNull()) 
					this.cboStates.SelectedValue = this.mAddressDS.AddressViewTable[0].StateOrProvince;
				else
					if(this.cboStates.Items.Count>0) this.cboStates.SelectedIndex = 0;
				this.cboStates.Enabled = (this.cboStates.Items.Count>0);
				this.txtZip.MaxLength = 15;
				this.txtZip.Text = "";
				if(!this.mAddressDS.AddressViewTable[0].IsPostalCodeNull())
					this.txtZip.Text = this.mAddressDS.AddressViewTable[0].PostalCode;
				if(!this.mAddressDS.AddressViewTable[0].IsCountryIDNull()) 
					this.cboCountries.SelectedValue = this.mAddressDS.AddressViewTable[0].CountryID;
				else
					if(this.cboCountries.Items.Count>0) this.cboCountries.SelectedIndex = 0;
				this.cboCountries.Enabled = (this.cboCountries.Items.Count>0);
				this.chkStatus.Checked = this.mAddressDS.AddressViewTable[0].IsActive;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnCountryChanged(object sender, System.EventArgs e) {
			//Event handler for change in country selection
			try {
				//Update state list
				this.cboStates.Enabled = (this.cboCountries.Text==COUNTRY_USA && this.cboStates.Items.Count>0);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler to re-validate form values when any control value is changed
			try {
				//Enable OK service if details have valid changes
				if(this.mAddressDS.AddressViewTable.Count>0) {
					this.btnOk.Enabled = (this.cboTypes.Text!="" && this.txtLine1.Text!="" && this.txtCity.Text!="" && 
										  this.cboStates.Text!="" && this.txtZip.Text!="" && this.cboCountries.Text!="");
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
						this.mAddressDS.AddressViewTable[0].AddressType = this.cboTypes.SelectedValue.ToString();
						this.mAddressDS.AddressViewTable[0].AddressLine1 = this.txtLine1.Text;
						if(this.txtLine2.Text!="")
							this.mAddressDS.AddressViewTable[0].AddressLine2 = this.txtLine2.Text;
						else
							this.mAddressDS.AddressViewTable[0].SetAddressLine2Null();
						this.mAddressDS.AddressViewTable[0].City = this.txtCity.Text;
						this.mAddressDS.AddressViewTable[0].StateOrProvince = this.cboStates.SelectedValue.ToString();
						this.mAddressDS.AddressViewTable[0].PostalCode = this.txtZip.Text;
						this.mAddressDS.AddressViewTable[0].CountryID = (int)this.cboCountries.SelectedValue;
						this.mAddressDS.AddressViewTable[0].IsActive = this.chkStatus.Checked;
						this.mAddressDS.AcceptChanges();
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
