//	File:	dlgterminal.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new LTA Terminal or edit an existing LTA Terminal.
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
	public class dlgEnterpriseTerminalDetail : System.Windows.Forms.Form {
		//Members
		private int mTerminalID=0;
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
		private Tsort.Enterprise.EnterpriseDS mTerminalDS;
		private System.Windows.Forms.TextBox txtNumber;
		private System.Windows.Forms.Label _lblExt;
		private System.Windows.Forms.TextBox txtExt;
		private System.Windows.Forms.ContextMenu mnuAddress;
		private System.Windows.Forms.MenuItem mnuAddressAdd;
		private System.Windows.Forms.MenuItem mnuAddressSep1;
		private System.Windows.Forms.MenuItem mnuAddressEdit;
		private System.Windows.Forms.MenuItem mnuAddressRemove;
		private System.Windows.Forms.ListView lsvAddress;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.GroupBox fraAddress;
		private System.Windows.Forms.Label _lbleMail;
		private System.Windows.Forms.TextBox txteMail;
		private System.Windows.Forms.GroupBox fraContact;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		private const string MNU_ADDRESS_ADD = "&Add";
		private const string MNU_ADDRESS_EDIT = "&Edit";
		private const string MNU_ADDRESS_REMOVE = "&Remove";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgEnterpriseTerminalDetail(ref EnterpriseDS terminal) {
			//Constructor
			try {
				//
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				this.mnuAddressAdd.Text = MNU_ADDRESS_ADD;
				this.mnuAddressEdit.Text = MNU_ADDRESS_EDIT;
				this.mnuAddressRemove.Text = MNU_ADDRESS_REMOVE;
				
				//Set mediator service, data, and titlebar caption
				this.mTerminalDS = terminal;
				if(this.mTerminalDS.EntTerminalDetailTable.Count>0) {
					this.mTerminalID = this.mTerminalDS.EntTerminalDetailTable[0].LocationID;
					this.Text = (this.mTerminalID>0) ? "Terminal (" + this.mTerminalID + ")" : "Terminal (New)";
				}
				else
					this.Text = "Terminal (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgEnterpriseTerminalDetail));
			this._lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.lsvAddress = new System.Windows.Forms.ListView();
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
			this.mnuAddress = new System.Windows.Forms.ContextMenu();
			this.mnuAddressAdd = new System.Windows.Forms.MenuItem();
			this.mnuAddressSep1 = new System.Windows.Forms.MenuItem();
			this.mnuAddressEdit = new System.Windows.Forms.MenuItem();
			this.mnuAddressRemove = new System.Windows.Forms.MenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraContact = new System.Windows.Forms.GroupBox();
			this._lbleMail = new System.Windows.Forms.Label();
			this.txteMail = new System.Windows.Forms.TextBox();
			this.fraAddress = new System.Windows.Forms.GroupBox();
			this.tabControl1.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraContact.SuspendLayout();
			this.fraAddress.SuspendLayout();
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
			// lsvAddress
			// 
			this.lsvAddress.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lsvAddress.Location = new System.Drawing.Point(6, 21);
			this.lsvAddress.Name = "lsvAddress";
			this.lsvAddress.Size = new System.Drawing.Size(432, 93);
			this.lsvAddress.TabIndex = 0;
			this.lsvAddress.DoubleClick += new System.EventHandler(this.OnAddressDblClick);
			this.lsvAddress.SelectedIndexChanged += new System.EventHandler(this.OnSelectedItem);
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
			this._lblContact.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
			this._lblFax.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
			// mnuAddress
			// 
			this.mnuAddress.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mnuAddressAdd,
																					   this.mnuAddressSep1,
																					   this.mnuAddressEdit,
																					   this.mnuAddressRemove});
			// 
			// mnuAddressAdd
			// 
			this.mnuAddressAdd.Index = 0;
			this.mnuAddressAdd.Text = "&New";
			this.mnuAddressAdd.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuAddressSep1
			// 
			this.mnuAddressSep1.Index = 1;
			this.mnuAddressSep1.Text = "-";
			// 
			// mnuAddressEdit
			// 
			this.mnuAddressEdit.DefaultItem = true;
			this.mnuAddressEdit.Index = 2;
			this.mnuAddressEdit.Text = "&Modify";
			this.mnuAddressEdit.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mnuAddressRemove
			// 
			this.mnuAddressRemove.Enabled = false;
			this.mnuAddressRemove.Index = 3;
			this.mnuAddressRemove.Text = "&Remove";
			this.mnuAddressRemove.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabGeneral);
			this.tabControl1.Location = new System.Drawing.Point(3, 3);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(468, 324);
			this.tabControl1.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraContact);
			this.tabGeneral.Controls.Add(this.txtName);
			this.tabGeneral.Controls.Add(this._lblName);
			this.tabGeneral.Controls.Add(this._lblNumber);
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Controls.Add(this.txtNumber);
			this.tabGeneral.Controls.Add(this.fraAddress);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(460, 298);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// fraContact
			// 
			this.fraContact.Controls.Add(this.lblPhone);
			this.fraContact.Controls.Add(this.txtContact);
			this.fraContact.Controls.Add(this._lblFax);
			this.fraContact.Controls.Add(this._lblExt);
			this.fraContact.Controls.Add(this.mskFax);
			this.fraContact.Controls.Add(this._lblContact);
			this.fraContact.Controls.Add(this.txtExt);
			this.fraContact.Controls.Add(this.mskPhone);
			this.fraContact.Controls.Add(this._lbleMail);
			this.fraContact.Controls.Add(this.txteMail);
			this.fraContact.Location = new System.Drawing.Point(6, 165);
			this.fraContact.Name = "fraContact";
			this.fraContact.Size = new System.Drawing.Size(447, 96);
			this.fraContact.TabIndex = 33;
			this.fraContact.TabStop = false;
			this.fraContact.Text = "Contact";
			// 
			// _lbleMail
			// 
			this._lbleMail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lbleMail.Location = new System.Drawing.Point(3, 69);
			this._lbleMail.Name = "_lbleMail";
			this._lbleMail.Size = new System.Drawing.Size(72, 18);
			this._lbleMail.TabIndex = 32;
			this._lbleMail.Text = "eMail";
			this._lbleMail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txteMail
			// 
			this.txteMail.Enabled = false;
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
			this.fraAddress.Controls.Add(this.lsvAddress);
			this.fraAddress.Location = new System.Drawing.Point(6, 39);
			this.fraAddress.Name = "fraAddress";
			this.fraAddress.Size = new System.Drawing.Size(447, 120);
			this.fraAddress.TabIndex = 2;
			this.fraAddress.TabStop = false;
			this.fraAddress.Text = "Addresses";
			// 
			// dlgEnterpriseTerminalDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(474, 359);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgEnterpriseTerminalDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Terminal Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.tabControl1.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraContact.ResumeLayout(false);
			this.fraAddress.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set default services
				this.Visible = true;
				Application.DoEvents();
								
				//Get selection lists
				//N/A
				
				//Configure address column headers
				this.lsvAddress.AutoArrange = false;
				this.lsvAddress.FullRowSelect = true;
				this.lsvAddress.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
				this.lsvAddress.HideSelection = false;
				this.lsvAddress.MultiSelect = false;
				this.lsvAddress.View = System.Windows.Forms.View.Details;
				this.lsvAddress.ContextMenu = this.mnuAddress;
				this.lsvAddress.Columns.Clear();
				ColumnHeader[] headers = null;
				ColumnHeader colID = new ColumnHeader(); colID.Text = "AddressID"; colID.Width = 0;
				ColumnHeader colType = new ColumnHeader(); colType.Text = "Type"; colType.Width = 60;
				ColumnHeader colLine1 = new ColumnHeader(); colLine1.Text = "Line1"; colLine1.Width = 144;
				ColumnHeader colLine2 = new ColumnHeader(); colLine2.Text = "Line2"; colLine2.Width = 48;
				ColumnHeader colCity = new ColumnHeader(); colCity.Text = "City"; colCity.Width = 72;
				ColumnHeader colState = new ColumnHeader(); colState.Text = "State"; colState.Width = 48;
				ColumnHeader colZip = new ColumnHeader(); colZip.Text = "Zip"; colZip.Width = 72;
				headers = new ColumnHeader[] {colID, colType, colLine1, colLine2, colCity, colState, colZip};
				this.lsvAddress.Columns.AddRange(headers);
				
				//Set control services
				this.txtNumber.MaxLength = 8;
				this.txtNumber.Text = "";
				if(!this.mTerminalDS.EntTerminalDetailTable[0].IsNumberNull())
					this.txtNumber.Text = this.mTerminalDS.EntTerminalDetailTable[0].Number.Trim();
				this.txtNumber.Enabled = (this.mTerminalID==0);
				this.txtName.MaxLength = 30;
				this.txtName.Text = "";
				if(!this.mTerminalDS.EntTerminalDetailTable[0].IsDescriptionNull())
					this.txtName.Text = this.mTerminalDS.EntTerminalDetailTable[0].Description.Trim();
				
				this.txtContact.MaxLength = 30;
				this.txtContact.Text = "";
				if(!this.mTerminalDS.EntTerminalDetailTable[0].IsContactNameNull())
					this.txtContact.Text = this.mTerminalDS.EntTerminalDetailTable[0].ContactName.Trim();
				this.showAddressList();
				this.mskPhone.InputMask = "###-###-####";
				if(!this.mTerminalDS.EntTerminalDetailTable[0].IsPhoneNull())
					this.mskPhone.Value = this.mTerminalDS.EntTerminalDetailTable[0].Phone;
				this.txtExt.MaxLength = 4;
				this.txtExt.Text = "";
				if(!this.mTerminalDS.EntTerminalDetailTable[0].IsExtensionNull())
					this.txtExt.Text = this.mTerminalDS.EntTerminalDetailTable[0].Extension.Trim();
				this.mskFax.InputMask = "###-###-####";
				if(!this.mTerminalDS.EntTerminalDetailTable[0].IsFaxNull())
					this.mskFax.Value = this.mTerminalDS.EntTerminalDetailTable[0].Fax;
				this.chkStatus.Checked = this.mTerminalDS.EntTerminalDetailTable[0].IsActive;
				
				//Reset
				
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnAddressDblClick(object sender, System.EventArgs e) {
			//Event handler for listview double click
			try {
				this.mnuAddressEdit.PerformClick();
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnSelectedItem(object sender, System.EventArgs e) { 
			//Event handler for change in drid selection
			try {
				//Set menu services
				this.mnuAddressAdd.Enabled = (this.lsvAddress.SelectedItems.Count==0 && this.mTerminalDS.AddressDetailTable.Rows.Count<2);
				this.mnuAddressEdit.Enabled = (this.lsvAddress.SelectedItems.Count>0);
				this.mnuAddressRemove.Enabled = false;
			} 
			catch(Exception ex) { reportError(ex); }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
				if(this.mTerminalDS.EntTerminalDetailTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtNumber.Text!="" && this.txtName.Text!="" && 
						this.mTerminalDS.AddressDetailTable[0].AddressLine1!="" && 
						this.txtContact.Text!="" && this.mskPhone.Value!=System.DBNull.Value);
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
						this.mTerminalDS.EntTerminalDetailTable[0].Number = this.txtNumber.Text;
						this.mTerminalDS.EntTerminalDetailTable[0].Description = this.txtName.Text;
						
						if(this.txtContact.Text!="")
							this.mTerminalDS.EntTerminalDetailTable[0].ContactName = this.txtContact.Text;
						else
							this.mTerminalDS.EntTerminalDetailTable[0].SetContactNameNull();
						//TODO: Address list handled in OnMenuClick
						this.mTerminalDS.EntTerminalDetailTable[0].Phone = this.mskPhone.Value.ToString();
						if(this.txtExt.Text!="")
							this.mTerminalDS.EntTerminalDetailTable[0].Extension = this.txtExt.Text;
						else
							this.mTerminalDS.EntTerminalDetailTable[0].SetExtensionNull();
						if(this.mskFax.Value!=System.DBNull.Value)
							this.mTerminalDS.EntTerminalDetailTable[0].Fax = this.mskFax.Value.ToString();
						else
							this.mTerminalDS.EntTerminalDetailTable[0].SetFaxNull();
						if(this.txteMail.Text!="")
							this.mTerminalDS.EntTerminalDetailTable[0].Email = this.txteMail.Text;
						else
							this.mTerminalDS.EntTerminalDetailTable[0].SetEmailNull();
						this.mTerminalDS.EntTerminalDetailTable[0].IsActive = this.chkStatus.Checked;
						this.mTerminalDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
					default:
						Debug.Write("Tsort.Administration.dlgEnterpriseTerminalDetail::OnCmdClick()-No click handler for " + btn.Text + "\n");
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Menu item clicked-apply selected service
			AddressDS addressDS;
			AddressDS.AddressViewTableRow rowAddress;
			EnterpriseDS dsAdd;
			EnterpriseDS.AddressDetailTableRow rowAdd;
			dlgAddressDetail dlgAddress;
			int addressID = 0;
			DialogResult res=DialogResult.Cancel;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_ADDRESS_ADD:
						//Add a new mailing address
						addressID = 0;
						addressDS = new AddressDS();
						rowAddress = addressDS.AddressViewTable.NewAddressViewTableRow();
						rowAddress.AddressID = 0;
						rowAddress.LocationID = this.mTerminalDS.EntTerminalDetailTable[0].LocationID;
						rowAddress.AddressType = "Mailing";
						rowAddress.AddressLine1 = "";
						rowAddress.AddressLine2 = "";
						rowAddress.City = "";
						rowAddress.StateOrProvince = "NJ";
						rowAddress.PostalCode = "";
						rowAddress.CountryID = 1;
						rowAddress.IsActive = true;
						rowAddress.LastUpdated = DateTime.Now;
						rowAddress.UserID = System.Environment.UserName;
						rowAddress.RowVersion = "";
						addressDS.AddressViewTable.AddAddressViewTableRow(rowAddress);
						dlgAddress = new dlgAddressDetail(ref addressDS);
						res = dlgAddress.ShowDialog();
						if(res==DialogResult.OK) {
							//Update listview
							rowAdd = this.mTerminalDS.AddressDetailTable.NewAddressDetailTableRow();
							rowAdd.AddressID = addressDS.AddressViewTable[0].AddressID;
							rowAdd.LocationID = addressDS.AddressViewTable[0].LocationID;
							rowAdd.AddressType = addressDS.AddressViewTable[0].AddressType;
							rowAdd.AddressLine1 = addressDS.AddressViewTable[0].AddressLine1;
							rowAdd.AddressLine2 = addressDS.AddressViewTable[0].AddressLine2;
							rowAdd.City = addressDS.AddressViewTable[0].City;
							if(!addressDS.AddressViewTable[0].IsStateOrProvinceNull())
								rowAdd.StateOrProvince = addressDS.AddressViewTable[0].StateOrProvince;
							rowAdd.PostalCode = addressDS.AddressViewTable[0].PostalCode;
							rowAdd.CountryID = addressDS.AddressViewTable[0].CountryID;
							rowAdd.IsActive = addressDS.AddressViewTable[0].IsActive;
							rowAdd.LastUpdated = addressDS.AddressViewTable[0].LastUpdated;
							rowAdd.UserID = addressDS.AddressViewTable[0].UserID;
							rowAdd.RowVersion = addressDS.AddressViewTable[0].RowVersion;
							this.mTerminalDS.AddressDetailTable.AddAddressDetailTableRow(rowAdd);
							this.mTerminalDS.AcceptChanges();
							this.showAddressList();
							this.mnuAddressAdd.Enabled = (this.lsvAddress.Items.Count<2);
						}
						break;
					case MNU_ADDRESS_EDIT:
						//Read existing terminal details, forward to dlgTerminal for update
						addressID = Convert.ToInt32(this.lsvAddress.SelectedItems[0].SubItems[0].Text);
						dsAdd = new EnterpriseDS();
						dsAdd.Merge(this.mTerminalDS.AddressDetailTable.Select("AddressID=" + addressID));
						addressDS = new AddressDS();
						rowAddress = addressDS.AddressViewTable.NewAddressViewTableRow();
						rowAddress.AddressID = dsAdd.AddressDetailTable[0].AddressID;
						rowAddress.LocationID = dsAdd.AddressDetailTable[0].LocationID;
						rowAddress.AddressType = dsAdd.AddressDetailTable[0].AddressType;
						rowAddress.AddressLine1 = dsAdd.AddressDetailTable[0].AddressLine1;
						rowAddress.AddressLine2 = dsAdd.AddressDetailTable[0].AddressLine2;
						rowAddress.City = dsAdd.AddressDetailTable[0].City;
						rowAddress.StateOrProvince = dsAdd.AddressDetailTable[0].StateOrProvince;
						rowAddress.PostalCode = dsAdd.AddressDetailTable[0].PostalCode;
						rowAddress.CountryID = dsAdd.AddressDetailTable[0].CountryID;
						rowAddress.IsActive = dsAdd.AddressDetailTable[0].IsActive;
						rowAddress.LastUpdated = DateTime.Now;
						rowAddress.UserID = System.Environment.UserName;
						rowAddress.RowVersion = dsAdd.AddressDetailTable[0].RowVersion;
						addressDS.AddressViewTable.AddAddressViewTableRow(rowAddress);
						dlgAddress = new dlgAddressDetail(ref addressDS);
						res = dlgAddress.ShowDialog();
						if(res==DialogResult.OK) {
							//Update listview
							rowAdd = this.mTerminalDS.AddressDetailTable[this.lsvAddress.SelectedItems[0].Index];
							rowAdd.AddressID = addressDS.AddressViewTable[0].AddressID;
							rowAdd.LocationID = addressDS.AddressViewTable[0].LocationID;
							rowAdd.AddressType = addressDS.AddressViewTable[0].AddressType;
							rowAdd.AddressLine1 = addressDS.AddressViewTable[0].AddressLine1;
							rowAdd.AddressLine2 = addressDS.AddressViewTable[0].AddressLine2;
							rowAdd.City = addressDS.AddressViewTable[0].City;
							if(!addressDS.AddressViewTable[0].IsStateOrProvinceNull())
								rowAdd.StateOrProvince = addressDS.AddressViewTable[0].StateOrProvince;
							rowAdd.PostalCode = addressDS.AddressViewTable[0].PostalCode;
							rowAdd.CountryID = addressDS.AddressViewTable[0].CountryID;
							rowAdd.IsActive = addressDS.AddressViewTable[0].IsActive;
							rowAdd.LastUpdated = addressDS.AddressViewTable[0].LastUpdated;
							rowAdd.UserID = addressDS.AddressViewTable[0].UserID;
							rowAdd.RowVersion = addressDS.AddressViewTable[0].RowVersion;
							this.mTerminalDS.AcceptChanges();
							this.showAddressList();
						}
						break;
					case MNU_ADDRESS_REMOVE:
						addressID = Convert.ToInt32(this.lsvAddress.SelectedItems[0].SubItems[0].Text);
						break;
					default: Debug.Write("Need handler for " + menu.Text + "\n"); break;
				}
			}
			catch(Exception ex) { reportError(ex); }
			finally  { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: showAddressList(), reportError()
		private void showAddressList() { 
			//
			this.lsvAddress.Items.Clear();
			ListViewItem[] items = new ListViewItem[this.mTerminalDS.AddressDetailTable.Rows.Count];
			for(int i=0; i<this.mTerminalDS.AddressDetailTable.Rows.Count; i++) {
				string[] subitems = {	this.mTerminalDS.AddressDetailTable[i].AddressID.ToString(), 
										this.mTerminalDS.AddressDetailTable[i].AddressType, 
										this.mTerminalDS.AddressDetailTable[i].AddressLine1, 
										this.mTerminalDS.AddressDetailTable[i].AddressLine2, 
										this.mTerminalDS.AddressDetailTable[i].City, 
										this.mTerminalDS.AddressDetailTable[i].StateOrProvince, 
										this.mTerminalDS.AddressDetailTable[i].PostalCode };
				ListViewItem item = new ListViewItem(subitems);
				items[i] = item;
			}
			this.lsvAddress.Items.AddRange(items);
			if(this.lsvAddress.Items.Count>0)
				this.lsvAddress.Items[0].Selected = true;
			this.ValidateForm(null, null);
		}
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
