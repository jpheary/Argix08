using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix.Terminals;

namespace Argix.Terminals {
	//
	public class dlgDeviceItem : System.Windows.Forms.Form {
		//Members
        private DeviceItem mItem=null;
		#region Controls
		private System.Windows.Forms.Label _lblStatus;
		private System.Windows.Forms.ComboBox cboStatus;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.Label _lblDeviceID;
		private System.Windows.Forms.TextBox txtDeviceID;
		private System.Windows.Forms.Label _lblComponentType;
		private System.Windows.Forms.ComboBox cboComponentType;
		private System.Windows.Forms.TextBox txtFirmwareVersion;
		private System.Windows.Forms.Label _lblFirmwareVersion;
		private System.Windows.Forms.TextBox txtPriorMGAID;
		private System.Windows.Forms.TextBox txtSoftwareVersion;
		private System.Windows.Forms.Label _lblCurrentMGAID;
		private System.Windows.Forms.Label _lblServiceAgreementExpiration;
		private System.Windows.Forms.Label _lblPriorMGAID;
		private System.Windows.Forms.TextBox txtCurrentMGAID;
		private System.Windows.Forms.Label _lblSoftwareVersion;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ComboBox cboPriorComponentType;
		private System.Windows.Forms.Label _lblPriorComponentType;
		private System.Windows.Forms.Label _lblPriorID;
		private System.Windows.Forms.ComboBox cboPriorDeviceID;
		private System.Windows.Forms.DateTimePicker dtpServiceExpiration;
		private System.Windows.Forms.TextBox txtModel;
		private System.Windows.Forms.Button btnAddComponentType;
		private System.Windows.Forms.ComboBox cboTerminal;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.GroupBox _fraDevice;
		private System.Windows.Forms.GroupBox _fraVendor;
		private System.Windows.Forms.GroupBox _fraMobileGateway;
		private System.Windows.Forms.GroupBox _fraPriorDevice;
		private System.Windows.Forms.Label _lblModel;
        private BindingSource mTypes;
        private BindingSource mPriorTypes;
        private BindingSource mTerminals;
        private BindingSource mPriorDevices;
        private Argix.Windows.SelectionList mStatusDS;
		private System.ComponentModel.IContainer components = null;
		#endregion
				
		//Interface
		public dlgDeviceItem(DeviceItem item) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
                this.mItem = item;
				this.Text = (this.mItem.ItemID!="") ? this.mItem.Terminal.Trim() + " Device (" + this.mItem.ItemID + ")" : this.mItem.Terminal.Trim() + " Device (New)";
			} 
			catch(Exception ex) { throw new ApplicationException("Failed to create new Device Item dialog", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgDeviceItem));
            this._lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.mStatusDS = new Argix.Windows.SelectionList();
            this._lblModel = new System.Windows.Forms.Label();
            this._lblComments = new System.Windows.Forms.Label();
            this._lblDeviceID = new System.Windows.Forms.Label();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this._lblComponentType = new System.Windows.Forms.Label();
            this.cboComponentType = new System.Windows.Forms.ComboBox();
            this.mTypes = new System.Windows.Forms.BindingSource(this.components);
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this._lblFirmwareVersion = new System.Windows.Forms.Label();
            this.txtPriorMGAID = new System.Windows.Forms.TextBox();
            this.txtSoftwareVersion = new System.Windows.Forms.TextBox();
            this._lblCurrentMGAID = new System.Windows.Forms.Label();
            this._lblServiceAgreementExpiration = new System.Windows.Forms.Label();
            this._lblPriorMGAID = new System.Windows.Forms.Label();
            this.txtCurrentMGAID = new System.Windows.Forms.TextBox();
            this._lblSoftwareVersion = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this._fraDevice = new System.Windows.Forms.GroupBox();
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this.mTerminals = new System.Windows.Forms.BindingSource(this.components);
            this._lblTerminal = new System.Windows.Forms.Label();
            this.btnAddComponentType = new System.Windows.Forms.Button();
            this._fraVendor = new System.Windows.Forms.GroupBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.dtpServiceExpiration = new System.Windows.Forms.DateTimePicker();
            this._fraMobileGateway = new System.Windows.Forms.GroupBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._fraPriorDevice = new System.Windows.Forms.GroupBox();
            this.cboPriorDeviceID = new System.Windows.Forms.ComboBox();
            this.mPriorDevices = new System.Windows.Forms.BindingSource(this.components);
            this._lblPriorID = new System.Windows.Forms.Label();
            this.cboPriorComponentType = new System.Windows.Forms.ComboBox();
            this.mPriorTypes = new System.Windows.Forms.BindingSource(this.components);
            this._lblPriorComponentType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mStatusDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTypes)).BeginInit();
            this._fraDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).BeginInit();
            this._fraVendor.SuspendLayout();
            this._fraMobileGateway.SuspendLayout();
            this._fraPriorDevice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPriorDevices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPriorTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // _lblStatus
            // 
            this._lblStatus.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblStatus.Location = new System.Drawing.Point(6,96);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(120,16);
            this._lblStatus.TabIndex = 5;
            this._lblStatus.Text = "Status";
            this._lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboStatus
            // 
            this.cboStatus.DataSource = this.mStatusDS;
            this.cboStatus.DisplayMember = "SelectionListTable.Description";
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboStatus.ItemHeight = 13;
            this.cboStatus.Location = new System.Drawing.Point(132,96);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(144,21);
            this.cboStatus.TabIndex = 6;
            this.cboStatus.ValueMember = "SelectionListTable.ID";
            this.cboStatus.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // mStatusDS
            // 
            this.mStatusDS.DataSetName = "SelectionList";
            this.mStatusDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mStatusDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblModel
            // 
            this._lblModel.Location = new System.Drawing.Point(6,24);
            this._lblModel.Name = "_lblModel";
            this._lblModel.Size = new System.Drawing.Size(120,16);
            this._lblModel.TabIndex = 0;
            this._lblModel.Text = "Model #";
            this._lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComments
            // 
            this._lblComments.Location = new System.Drawing.Point(6,225);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(75,16);
            this._lblComments.TabIndex = 7;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblDeviceID
            // 
            this._lblDeviceID.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblDeviceID.Location = new System.Drawing.Point(9,48);
            this._lblDeviceID.Name = "_lblDeviceID";
            this._lblDeviceID.Size = new System.Drawing.Size(120,16);
            this._lblDeviceID.TabIndex = 3;
            this._lblDeviceID.Text = "Device ID #";
            this._lblDeviceID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDeviceID.Location = new System.Drawing.Point(132,48);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(144,21);
            this.txtDeviceID.TabIndex = 4;
            this.txtDeviceID.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblComponentType
            // 
            this._lblComponentType.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblComponentType.Location = new System.Drawing.Point(6,24);
            this._lblComponentType.Name = "_lblComponentType";
            this._lblComponentType.Size = new System.Drawing.Size(120,16);
            this._lblComponentType.TabIndex = 0;
            this._lblComponentType.Text = "Component Type";
            this._lblComponentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboComponentType
            // 
            this.cboComponentType.DataSource = this.mTypes;
            this.cboComponentType.DisplayMember = "TypeID";
            this.cboComponentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboComponentType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboComponentType.ItemHeight = 13;
            this.cboComponentType.Location = new System.Drawing.Point(132,24);
            this.cboComponentType.Name = "cboComponentType";
            this.cboComponentType.Size = new System.Drawing.Size(192,21);
            this.cboComponentType.TabIndex = 1;
            this.cboComponentType.ValueMember = "TypeID";
            this.cboComponentType.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            this.cboComponentType.SelectedIndexChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // mTypes
            // 
            this.mTypes.DataSource = typeof(Argix.Terminals.ComponentTypes);
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtFirmwareVersion.Location = new System.Drawing.Point(132,48);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.Size = new System.Drawing.Size(144,21);
            this.txtFirmwareVersion.TabIndex = 3;
            this.txtFirmwareVersion.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblFirmwareVersion
            // 
            this._lblFirmwareVersion.Location = new System.Drawing.Point(6,48);
            this._lblFirmwareVersion.Name = "_lblFirmwareVersion";
            this._lblFirmwareVersion.Size = new System.Drawing.Size(120,16);
            this._lblFirmwareVersion.TabIndex = 2;
            this._lblFirmwareVersion.Text = "Firmware Version";
            this._lblFirmwareVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPriorMGAID
            // 
            this.txtPriorMGAID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtPriorMGAID.Location = new System.Drawing.Point(132,48);
            this.txtPriorMGAID.Name = "txtPriorMGAID";
            this.txtPriorMGAID.Size = new System.Drawing.Size(144,21);
            this.txtPriorMGAID.TabIndex = 3;
            this.txtPriorMGAID.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtSoftwareVersion
            // 
            this.txtSoftwareVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtSoftwareVersion.Location = new System.Drawing.Point(132,72);
            this.txtSoftwareVersion.Name = "txtSoftwareVersion";
            this.txtSoftwareVersion.Size = new System.Drawing.Size(144,21);
            this.txtSoftwareVersion.TabIndex = 5;
            this.txtSoftwareVersion.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblCurrentMGAID
            // 
            this._lblCurrentMGAID.Location = new System.Drawing.Point(6,24);
            this._lblCurrentMGAID.Name = "_lblCurrentMGAID";
            this._lblCurrentMGAID.Size = new System.Drawing.Size(120,16);
            this._lblCurrentMGAID.TabIndex = 0;
            this._lblCurrentMGAID.Text = "Account ID";
            this._lblCurrentMGAID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblServiceAgreementExpiration
            // 
            this._lblServiceAgreementExpiration.Location = new System.Drawing.Point(6,96);
            this._lblServiceAgreementExpiration.Name = "_lblServiceAgreementExpiration";
            this._lblServiceAgreementExpiration.Size = new System.Drawing.Size(120,16);
            this._lblServiceAgreementExpiration.TabIndex = 6;
            this._lblServiceAgreementExpiration.Text = "Service Expires";
            this._lblServiceAgreementExpiration.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblPriorMGAID
            // 
            this._lblPriorMGAID.Location = new System.Drawing.Point(6,48);
            this._lblPriorMGAID.Name = "_lblPriorMGAID";
            this._lblPriorMGAID.Size = new System.Drawing.Size(120,16);
            this._lblPriorMGAID.TabIndex = 2;
            this._lblPriorMGAID.Text = "Prior Account ID";
            this._lblPriorMGAID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCurrentMGAID
            // 
            this.txtCurrentMGAID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtCurrentMGAID.Location = new System.Drawing.Point(132,24);
            this.txtCurrentMGAID.Name = "txtCurrentMGAID";
            this.txtCurrentMGAID.Size = new System.Drawing.Size(144,21);
            this.txtCurrentMGAID.TabIndex = 1;
            this.txtCurrentMGAID.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblSoftwareVersion
            // 
            this._lblSoftwareVersion.Location = new System.Drawing.Point(6,72);
            this._lblSoftwareVersion.Name = "_lblSoftwareVersion";
            this._lblSoftwareVersion.Size = new System.Drawing.Size(120,16);
            this._lblSoftwareVersion.TabIndex = 4;
            this._lblSoftwareVersion.Text = "Software Version";
            this._lblSoftwareVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtComments.Location = new System.Drawing.Point(6,246);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(660,21);
            this.txtComments.TabIndex = 4;
            this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _fraDevice
            // 
            this._fraDevice.Controls.Add(this.cboTerminal);
            this._fraDevice.Controls.Add(this._lblTerminal);
            this._fraDevice.Controls.Add(this.btnAddComponentType);
            this._fraDevice.Controls.Add(this.cboComponentType);
            this._fraDevice.Controls.Add(this._lblComponentType);
            this._fraDevice.Controls.Add(this.cboStatus);
            this._fraDevice.Controls.Add(this._lblStatus);
            this._fraDevice.Controls.Add(this.txtDeviceID);
            this._fraDevice.Controls.Add(this._lblDeviceID);
            this._fraDevice.ForeColor = System.Drawing.SystemColors.ControlText;
            this._fraDevice.Location = new System.Drawing.Point(6,6);
            this._fraDevice.Name = "_fraDevice";
            this._fraDevice.Size = new System.Drawing.Size(366,126);
            this._fraDevice.TabIndex = 0;
            this._fraDevice.TabStop = false;
            this._fraDevice.Text = "Device Identification";
            // 
            // cboTerminal
            // 
            this.cboTerminal.DataSource = this.mTerminals;
            this.cboTerminal.DisplayMember = "TerminalName";
            this.cboTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboTerminal.ItemHeight = 13;
            this.cboTerminal.Location = new System.Drawing.Point(132,72);
            this.cboTerminal.Name = "cboTerminal";
            this.cboTerminal.Size = new System.Drawing.Size(192,21);
            this.cboTerminal.TabIndex = 8;
            this.cboTerminal.ValueMember = "TerminalID";
            this.cboTerminal.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // mTerminals
            // 
            this.mTerminals.DataSource = typeof(Argix.Terminals.LocalTerminals);
            // 
            // _lblTerminal
            // 
            this._lblTerminal.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblTerminal.Location = new System.Drawing.Point(6,72);
            this._lblTerminal.Name = "_lblTerminal";
            this._lblTerminal.Size = new System.Drawing.Size(120,16);
            this._lblTerminal.TabIndex = 7;
            this._lblTerminal.Text = "Terminal";
            this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnAddComponentType
            // 
            this.btnAddComponentType.BackColor = System.Drawing.SystemColors.Control;
            this.btnAddComponentType.Enabled = false;
            this.btnAddComponentType.Location = new System.Drawing.Point(330,24);
            this.btnAddComponentType.Name = "btnAddComponentType";
            this.btnAddComponentType.Size = new System.Drawing.Size(27,21);
            this.btnAddComponentType.TabIndex = 2;
            this.btnAddComponentType.Text = "...";
            this.btnAddComponentType.UseVisualStyleBackColor = false;
            // 
            // _fraVendor
            // 
            this._fraVendor.Controls.Add(this.txtModel);
            this._fraVendor.Controls.Add(this.dtpServiceExpiration);
            this._fraVendor.Controls.Add(this.txtFirmwareVersion);
            this._fraVendor.Controls.Add(this._lblSoftwareVersion);
            this._fraVendor.Controls.Add(this._lblFirmwareVersion);
            this._fraVendor.Controls.Add(this.txtSoftwareVersion);
            this._fraVendor.Controls.Add(this._lblServiceAgreementExpiration);
            this._fraVendor.Controls.Add(this._lblModel);
            this._fraVendor.Location = new System.Drawing.Point(378,6);
            this._fraVendor.Name = "_fraVendor";
            this._fraVendor.Size = new System.Drawing.Size(288,126);
            this._fraVendor.TabIndex = 2;
            this._fraVendor.TabStop = false;
            this._fraVendor.Text = "Vendor Details";
            // 
            // txtModel
            // 
            this.txtModel.Location = new System.Drawing.Point(132,24);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(144,21);
            this.txtModel.TabIndex = 1;
            this.txtModel.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // dtpServiceExpiration
            // 
            this.dtpServiceExpiration.CustomFormat = "MM/dd/yyyy";
            this.dtpServiceExpiration.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpServiceExpiration.Location = new System.Drawing.Point(132,96);
            this.dtpServiceExpiration.MaxDate = new System.DateTime(2028,8,2,0,0,0,0);
            this.dtpServiceExpiration.MinDate = new System.DateTime(2000,1,1,0,0,0,0);
            this.dtpServiceExpiration.Name = "dtpServiceExpiration";
            this.dtpServiceExpiration.Size = new System.Drawing.Size(120,21);
            this.dtpServiceExpiration.TabIndex = 7;
            this.dtpServiceExpiration.ValueChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _fraMobileGateway
            // 
            this._fraMobileGateway.Controls.Add(this.txtPriorMGAID);
            this._fraMobileGateway.Controls.Add(this._lblPriorMGAID);
            this._fraMobileGateway.Controls.Add(this._lblCurrentMGAID);
            this._fraMobileGateway.Controls.Add(this.txtCurrentMGAID);
            this._fraMobileGateway.Location = new System.Drawing.Point(378,135);
            this._fraMobileGateway.Name = "_fraMobileGateway";
            this._fraMobileGateway.Size = new System.Drawing.Size(288,84);
            this._fraMobileGateway.TabIndex = 3;
            this._fraMobileGateway.TabStop = false;
            this._fraMobileGateway.Text = "Mobile Gateway";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(456,276);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104,23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(564,276);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104,23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // _fraPriorDevice
            // 
            this._fraPriorDevice.Controls.Add(this.cboPriorDeviceID);
            this._fraPriorDevice.Controls.Add(this._lblPriorID);
            this._fraPriorDevice.Controls.Add(this.cboPriorComponentType);
            this._fraPriorDevice.Controls.Add(this._lblPriorComponentType);
            this._fraPriorDevice.Location = new System.Drawing.Point(6,135);
            this._fraPriorDevice.Name = "_fraPriorDevice";
            this._fraPriorDevice.Size = new System.Drawing.Size(366,84);
            this._fraPriorDevice.TabIndex = 1;
            this._fraPriorDevice.TabStop = false;
            this._fraPriorDevice.Text = "Prior Device Identification";
            // 
            // cboPriorDeviceID
            // 
            this.cboPriorDeviceID.DataSource = this.mPriorDevices;
            this.cboPriorDeviceID.DisplayMember = "DeviceID";
            this.cboPriorDeviceID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriorDeviceID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboPriorDeviceID.ItemHeight = 13;
            this.cboPriorDeviceID.Location = new System.Drawing.Point(132,48);
            this.cboPriorDeviceID.Name = "cboPriorDeviceID";
            this.cboPriorDeviceID.Size = new System.Drawing.Size(192,21);
            this.cboPriorDeviceID.TabIndex = 4;
            this.cboPriorDeviceID.ValueMember = "ItemID";
            this.cboPriorDeviceID.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // mPriorDevices
            // 
            this.mPriorDevices.DataSource = typeof(Argix.Terminals.DeviceItems);
            // 
            // _lblPriorID
            // 
            this._lblPriorID.Location = new System.Drawing.Point(6,51);
            this._lblPriorID.Name = "_lblPriorID";
            this._lblPriorID.Size = new System.Drawing.Size(120,16);
            this._lblPriorID.TabIndex = 3;
            this._lblPriorID.Text = "Device ID #";
            this._lblPriorID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPriorComponentType
            // 
            this.cboPriorComponentType.DataSource = this.mPriorTypes;
            this.cboPriorComponentType.DisplayMember = "TypeID";
            this.cboPriorComponentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPriorComponentType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboPriorComponentType.ItemHeight = 13;
            this.cboPriorComponentType.Location = new System.Drawing.Point(132,24);
            this.cboPriorComponentType.Name = "cboPriorComponentType";
            this.cboPriorComponentType.Size = new System.Drawing.Size(192,21);
            this.cboPriorComponentType.TabIndex = 1;
            this.cboPriorComponentType.ValueMember = "TypeID";
            this.cboPriorComponentType.SelectionChangeCommitted += new System.EventHandler(this.OnPriorComponentTypeChanged);
            // 
            // mPriorTypes
            // 
            this.mPriorTypes.DataSource = typeof(Argix.Terminals.ComponentTypes);
            // 
            // _lblPriorComponentType
            // 
            this._lblPriorComponentType.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblPriorComponentType.Location = new System.Drawing.Point(6,24);
            this._lblPriorComponentType.Name = "_lblPriorComponentType";
            this._lblPriorComponentType.Size = new System.Drawing.Size(120,16);
            this._lblPriorComponentType.TabIndex = 0;
            this._lblPriorComponentType.Text = "Component Type";
            this._lblPriorComponentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgDeviceItem
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(672,304);
            this.Controls.Add(this._fraPriorDevice);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this._fraMobileGateway);
            this.Controls.Add(this._fraVendor);
            this.Controls.Add(this._fraDevice);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this._lblComments);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgDeviceItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobile Device";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mStatusDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTypes)).EndInit();
            this._fraDevice.ResumeLayout(false);
            this._fraDevice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).EndInit();
            this._fraVendor.ResumeLayout(false);
            this._fraVendor.PerformLayout();
            this._fraMobileGateway.ResumeLayout(false);
            this._fraMobileGateway.PerformLayout();
            this._fraPriorDevice.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mPriorDevices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPriorTypes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {				
				//Show early
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists, and set control values	
				this.mTypes.DataSource = MobileDevicesProxy.GetComponentTypeList(true, "MobilDevice"); //ComponentType.CATEGORYID_DEVICE);
                this.mStatusDS.Merge(this.mItem.StatusList);
                this.mTerminals.DataSource = MobileDevicesProxy.GetLocalTerminals();
                this.mPriorTypes.DataSource = MobileDevicesProxy.GetComponentTypeList(false,"MobilDevice"); //ComponentType.CATEGORYID_DEVICE);
				if(this.mItem.TypeID.Length > 0)
					this.cboComponentType.SelectedValue = this.mItem.TypeID;
				else 
					if(this.cboComponentType.Items.Count>0) this.cboComponentType.SelectedIndex = 0;
				this.cboComponentType.Enabled = (this.mItem.ItemID.Length == 0);
				
                this.btnAddComponentType.Enabled = false;
				this.txtDeviceID.MaxLength = 20;
				this.txtDeviceID.Text = this.mItem.DeviceID;
				this.cboTerminal.SelectedValue = this.mItem.TerminalID;
                this.cboTerminal.Enabled = (this.cboTerminal.Items.Count>0 && App.Config.AllowTerminalChange);
				this.cboStatus.SelectedValue = this.mItem.Status;
				this.cboStatus.Enabled = true;
				
				if(this.mItem.TypeID.Length > 0) 
					this.cboPriorComponentType.SelectedValue = this.mItem.TypeID;
				else 
					if(this.cboPriorComponentType.Items.Count>0) this.cboPriorComponentType.SelectedIndex = 0;
                this.cboPriorComponentType.Enabled = (this.mItem.ItemID.Length > 0);
				OnPriorComponentTypeChanged(null, null);
				
				this.txtModel.MaxLength = 20;
				this.txtModel.Text = this.mItem.ModelNumber;
				this.txtFirmwareVersion.MaxLength = 20;
				this.txtFirmwareVersion.Text = this.mItem.FirmWareVersion;
				this.txtSoftwareVersion.MaxLength = 20;
				this.txtSoftwareVersion.Text = this.mItem.SoftWareVersion;
				this.dtpServiceExpiration.Value = this.mItem.ServiceExpiration;
				
				this.txtCurrentMGAID.MaxLength = 20;
				this.txtCurrentMGAID.Text = this.mItem.AccountID;
				this.txtPriorMGAID.MaxLength = 20;
				this.txtPriorMGAID.Text = this.mItem.PriorAccountID;
				this.txtComments.MaxLength = 100;
				this.txtComments.Text = this.mItem.Comments;				
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.btnOK.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnPriorComponentTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change in prior comonent type
			try {
				//Create filtered subset for the selected component type
				//Add a blank entry since a selection is not required
				//Add the current entry to keep current selection
                this.mPriorDevices.Clear();
                DeviceItem di = new DeviceItem();
                di.DeviceID = "";
                di.ItemID = "";
                this.mPriorDevices.Add(di);
                if((this.cboPriorComponentType.SelectedValue.ToString() == this.mItem.TypeID) && this.mItem.PriorItemID != null) {
                    di = new DeviceItem();
                    di.ItemID = this.mItem.PriorItemID;
                    di.DeviceID = this.mItem.PriorDeviceID;
                    this.mPriorDevices.Add(di);
                }

                string typeID = this.cboPriorComponentType.SelectedValue != null ? this.cboPriorComponentType.SelectedValue.ToString() : "";
                DeviceItems priorDeviceItems = MobileDevicesProxy.GetPriorDeviceItems();
                for(int i=0;i<priorDeviceItems.Count;i++) {
                    if(typeID.Length == 0 || priorDeviceItems[i].TypeID == typeID)
                        this.mPriorDevices.Add(priorDeviceItems[i]);
                }
				
				//Sort and select
                this.mPriorDevices.Sort = "DeviceID";
				if(this.mItem.PriorItemID != null) 
					this.cboPriorDeviceID.SelectedValue = this.mItem.PriorItemID;
				else
					this.cboPriorDeviceID.SelectedIndex = 0;
				this.cboPriorDeviceID.Enabled = (this.mItem.ItemID !="");
			}
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
			finally { this.OnValidateForm(null, null); }
		}
		private void OnValidateForm(object sender, System.EventArgs e) { 			
			//Event handler for changes to control data
			try {
				//Enable OK service if details have valid changes
				this.btnOK.Enabled = (	this.cboComponentType.Text!="" && 
										this.txtDeviceID.Text!="" && 
										this.cboTerminal.Text!="" && 
										this.cboStatus.Text!="" );
                if(this.cboComponentType.SelectedValue.ToString() == MobileDevicesProxy.MOBILE_GATEWAY_DEVICE) 
					this._fraMobileGateway.Enabled = true;
				else {
					this.txtCurrentMGAID.Text = this.txtPriorMGAID.Text = "";
					this._fraMobileGateway.Enabled = false;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Name) {
					case "btnCancel":	this.DialogResult = DialogResult.Cancel; break;
					case "btnOK":
						//Update details with control values
						this.DialogResult = DialogResult.OK;
						this.mItem.TypeID = this.cboComponentType.SelectedValue.ToString();
						//this.mItem.ItemID: Assigned by database; cannot be changed
						this.mItem.DeviceID = this.txtDeviceID.Text;
						this.mItem.TerminalID = Convert.ToInt64(this.cboTerminal.SelectedValue);
						this.mItem.Terminal = this.cboTerminal.Text;
						this.mItem.Status = this.cboStatus.SelectedValue.ToString();
						this.mItem.PriorItemID = (this.cboPriorDeviceID.Text!="") ? this.cboPriorDeviceID.SelectedValue.ToString() : null;
						this.mItem.ModelNumber = this.txtModel.Text;
						this.mItem.FirmWareVersion = this.txtFirmwareVersion.Text;
						this.mItem.SoftWareVersion = this.txtSoftwareVersion.Text;
						this.mItem.ServiceExpiration = this.dtpServiceExpiration.Value;
						this.mItem.AccountID = this.txtCurrentMGAID.Text;
						this.mItem.PriorAccountID = this.txtPriorMGAID.Text;
						this.mItem.Comments = this.txtComments.Text;
                        this.mItem.IsActive = (this.cboStatus.SelectedValue.ToString()!=MobileDevicesProxy.INACTIVE) ? (byte)1 : (byte)0;
						this.mItem.LastUpdated = DateTime.Now;
						this.mItem.UserID = Environment.UserName;
						break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.Close(); }
		}
	}
}
