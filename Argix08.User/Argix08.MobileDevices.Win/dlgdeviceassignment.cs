using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Terminals;

namespace Argix.Terminals {
	//
	public class dlgDeviceAssignment : System.Windows.Forms.Form {
		//Members
        private DeviceItem mItem=null;
		#region Controls
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label _lblDriver;
		private System.Windows.Forms.ComboBox cboDriver;
		private System.Windows.Forms.Label _lblDeviceID;
		private System.Windows.Forms.Label lblDeviceID;
		private System.Windows.Forms.Label _lblAssigned;
		private System.Windows.Forms.Label _lblInstallType;
		private System.Windows.Forms.Label _lblInstallNumber;
		private System.Windows.Forms.TextBox txtInstallNumber;
		private System.Windows.Forms.ComboBox cboInstallType;
		private Argix.Windows.SelectionList mInstallTypes;
		private System.Windows.Forms.DateTimePicker dtpAssignedDate;
		private System.Windows.Forms.ComboBox cboTerminal;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.GroupBox _fraDeviceAssignment;
		private System.Windows.Forms.GroupBox _fraInstallation;
		private System.ComponentModel.Container components = null;
		#endregion
				
		//Interface
		public dlgDeviceAssignment(DeviceItem item) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.Text = "Assign Device to Driver";
                this.mItem = item;
			} 
			catch(Exception ex) { throw new ApplicationException("Failed to create new Device Assignment dialog", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		public int DriverID { get { return Convert.ToInt32(this.cboDriver.SelectedValue); } }
        public string DriverName { get { return this.cboDriver.Text.Trim(); } }
        public DateTime AssignedDate { get { return this.dtpAssignedDate.Value; } }
		public string InstallationType { get { return this.cboInstallType.SelectedValue.ToString(); } }
		public string InstallationNumber { get { return this.txtInstallNumber.Text; } }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgDeviceAssignment));
            this.btnOK = new System.Windows.Forms.Button();
            this._lblDriver = new System.Windows.Forms.Label();
            this._lblDeviceID = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboDriver = new System.Windows.Forms.ComboBox();
            this.lblDeviceID = new System.Windows.Forms.Label();
            this._lblAssigned = new System.Windows.Forms.Label();
            this._fraDeviceAssignment = new System.Windows.Forms.GroupBox();
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this._lblTerminal = new System.Windows.Forms.Label();
            this.dtpAssignedDate = new System.Windows.Forms.DateTimePicker();
            this._fraInstallation = new System.Windows.Forms.GroupBox();
            this._lblInstallNumber = new System.Windows.Forms.Label();
            this.cboInstallType = new System.Windows.Forms.ComboBox();
            this.mInstallTypes = new Argix.Windows.SelectionList();
            this.txtInstallNumber = new System.Windows.Forms.TextBox();
            this._lblInstallType = new System.Windows.Forms.Label();
            this._fraDeviceAssignment.SuspendLayout();
            this._fraInstallation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mInstallTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(174,231);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96,24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // _lblDriver
            // 
            this._lblDriver.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblDriver.Location = new System.Drawing.Point(9,78);
            this._lblDriver.Name = "_lblDriver";
            this._lblDriver.Size = new System.Drawing.Size(96,16);
            this._lblDriver.TabIndex = 118;
            this._lblDriver.Text = "Driver";
            this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDeviceID
            // 
            this._lblDeviceID.Location = new System.Drawing.Point(9,24);
            this._lblDeviceID.Name = "_lblDeviceID";
            this._lblDeviceID.Size = new System.Drawing.Size(96,16);
            this._lblDeviceID.TabIndex = 119;
            this._lblDeviceID.Text = "DeviceID";
            this._lblDeviceID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276,231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96,24);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // cboDriver
            // 
            this.cboDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDriver.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboDriver.ItemHeight = 13;
            this.cboDriver.Location = new System.Drawing.Point(111,78);
            this.cboDriver.Name = "cboDriver";
            this.cboDriver.Size = new System.Drawing.Size(240,21);
            this.cboDriver.TabIndex = 120;
            this.cboDriver.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // lblDeviceID
            // 
            this.lblDeviceID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDeviceID.Location = new System.Drawing.Point(111,24);
            this.lblDeviceID.Name = "lblDeviceID";
            this.lblDeviceID.Size = new System.Drawing.Size(144,16);
            this.lblDeviceID.TabIndex = 121;
            this.lblDeviceID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblAssigned
            // 
            this._lblAssigned.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblAssigned.Location = new System.Drawing.Point(9,105);
            this._lblAssigned.Name = "_lblAssigned";
            this._lblAssigned.Size = new System.Drawing.Size(96,16);
            this._lblAssigned.TabIndex = 122;
            this._lblAssigned.Text = "Assigned On";
            this._lblAssigned.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _fraDeviceAssignment
            // 
            this._fraDeviceAssignment.Controls.Add(this.cboTerminal);
            this._fraDeviceAssignment.Controls.Add(this._lblTerminal);
            this._fraDeviceAssignment.Controls.Add(this.dtpAssignedDate);
            this._fraDeviceAssignment.Controls.Add(this._fraInstallation);
            this._fraDeviceAssignment.Controls.Add(this.lblDeviceID);
            this._fraDeviceAssignment.Controls.Add(this.cboDriver);
            this._fraDeviceAssignment.Controls.Add(this._lblDeviceID);
            this._fraDeviceAssignment.Controls.Add(this._lblAssigned);
            this._fraDeviceAssignment.Controls.Add(this._lblDriver);
            this._fraDeviceAssignment.Location = new System.Drawing.Point(6,6);
            this._fraDeviceAssignment.Name = "_fraDeviceAssignment";
            this._fraDeviceAssignment.Size = new System.Drawing.Size(366,216);
            this._fraDeviceAssignment.TabIndex = 124;
            this._fraDeviceAssignment.TabStop = false;
            this._fraDeviceAssignment.Text = "Assignment Details";
            // 
            // cboTerminal
            // 
            this.cboTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboTerminal.ItemHeight = 13;
            this.cboTerminal.Location = new System.Drawing.Point(111,51);
            this.cboTerminal.Name = "cboTerminal";
            this.cboTerminal.Size = new System.Drawing.Size(192,21);
            this.cboTerminal.TabIndex = 132;
            this.cboTerminal.SelectionChangeCommitted += new System.EventHandler(this.OnTerminalChanged);
            // 
            // _lblTerminal
            // 
            this._lblTerminal.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblTerminal.Location = new System.Drawing.Point(9,51);
            this._lblTerminal.Name = "_lblTerminal";
            this._lblTerminal.Size = new System.Drawing.Size(96,16);
            this._lblTerminal.TabIndex = 131;
            this._lblTerminal.Text = "Terminal";
            this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpAssignedDate
            // 
            this.dtpAssignedDate.CustomFormat = "MM/dd/yyyy";
            this.dtpAssignedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpAssignedDate.Location = new System.Drawing.Point(111,105);
            this.dtpAssignedDate.Name = "dtpAssignedDate";
            this.dtpAssignedDate.Size = new System.Drawing.Size(120,21);
            this.dtpAssignedDate.TabIndex = 130;
            this.dtpAssignedDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _fraInstallation
            // 
            this._fraInstallation.Controls.Add(this._lblInstallNumber);
            this._fraInstallation.Controls.Add(this.cboInstallType);
            this._fraInstallation.Controls.Add(this.txtInstallNumber);
            this._fraInstallation.Controls.Add(this._lblInstallType);
            this._fraInstallation.Location = new System.Drawing.Point(9,129);
            this._fraInstallation.Name = "_fraInstallation";
            this._fraInstallation.Size = new System.Drawing.Size(348,78);
            this._fraInstallation.TabIndex = 129;
            this._fraInstallation.TabStop = false;
            this._fraInstallation.Text = "Installation";
            // 
            // _lblInstallNumber
            // 
            this._lblInstallNumber.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblInstallNumber.Location = new System.Drawing.Point(30,51);
            this._lblInstallNumber.Name = "_lblInstallNumber";
            this._lblInstallNumber.Size = new System.Drawing.Size(64,16);
            this._lblInstallNumber.TabIndex = 126;
            this._lblInstallNumber.Text = "Number";
            this._lblInstallNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboInstallType
            // 
            this.cboInstallType.DataSource = this.mInstallTypes;
            this.cboInstallType.DisplayMember = "SelectionListTable.Description";
            this.cboInstallType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInstallType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboInstallType.ItemHeight = 13;
            this.cboInstallType.Location = new System.Drawing.Point(102,24);
            this.cboInstallType.Name = "cboInstallType";
            this.cboInstallType.Size = new System.Drawing.Size(120,21);
            this.cboInstallType.TabIndex = 128;
            this.cboInstallType.ValueMember = "SelectionListTable.ID";
            this.cboInstallType.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // mInstallTypes
            // 
            this.mInstallTypes.DataSetName = "SelectionList";
            this.mInstallTypes.Locale = new System.Globalization.CultureInfo("en-US");
            this.mInstallTypes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtInstallNumber
            // 
            this.txtInstallNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtInstallNumber.Location = new System.Drawing.Point(102,51);
            this.txtInstallNumber.Name = "txtInstallNumber";
            this.txtInstallNumber.Size = new System.Drawing.Size(120,21);
            this.txtInstallNumber.TabIndex = 127;
            this.txtInstallNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblInstallType
            // 
            this._lblInstallType.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblInstallType.Location = new System.Drawing.Point(30,24);
            this._lblInstallType.Name = "_lblInstallType";
            this._lblInstallType.Size = new System.Drawing.Size(64,16);
            this._lblInstallType.TabIndex = 124;
            this._lblInstallType.Text = "Type";
            this._lblInstallType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgDeviceAssignment
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(378,259);
            this.Controls.Add(this._fraDeviceAssignment);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgDeviceAssignment";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mobile Device Assignment";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this._fraDeviceAssignment.ResumeLayout(false);
            this._fraInstallation.ResumeLayout(false);
            this._fraInstallation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mInstallTypes)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();

                //Set control services
                this.cboTerminal.DisplayMember = "TerminalName";
                this.cboTerminal.ValueMember = "TerminalID";
                this.cboTerminal.DataSource = MobileDevicesProxy.GetLocalTerminals();
                this.cboTerminal.SelectedValue = this.mItem.TerminalID;
                this.cboTerminal.Enabled = false;   //this.cboTerminal.Items.Count>0);
                OnTerminalChanged(null,null);
                
                this.mInstallTypes.Merge(MobileDevicesProxy.GetInstallationTypes());
                if(this.cboInstallType.Items.Count>0) this.cboInstallType.SelectedIndex = 0;
                this.cboInstallType.Enabled = (this.cboInstallType.Items.Count>0);

				this.lblDeviceID.Text = this.mItem.DeviceID;
				this.dtpAssignedDate.Value = DateTime.Today;
				this.dtpAssignedDate.Enabled = false;
				this.txtInstallNumber.MaxLength = 20;
				this.txtInstallNumber.Text = "";
				this.txtInstallNumber.Enabled = true;
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.OnValidateForm(null, null); this.Cursor = Cursors.Default; }
		}
		private void OnTerminalChanged(object sender, System.EventArgs e) {
			//Even handler for change in selected driver terminal
			try {
                this.cboDriver.DisplayMember = "FullName";
                this.cboDriver.ValueMember = "DriverID";
                this.cboDriver.DataSource = MobileDevicesProxy.GetDrivers(Convert.ToInt64(this.cboTerminal.SelectedValue),true);
                if(this.cboDriver.Items.Count>0) this.cboDriver.SelectedIndex = 0;
				this.cboDriver.Enabled = true;
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnValidateForm(object sender, System.EventArgs e) { 
			//Event handler for changes to control data
			try {
				//Enable OK service if details have valid changes
				this.btnOK.Enabled = (	this.cboTerminal.Text!="" && 
										this.cboDriver.Text!="" && 
										this.cboInstallType.Text!="" && 
										this.txtInstallNumber.Text!="");
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) { 
			//Command button handler
			try {
				Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnCancel":   this.DialogResult = DialogResult.Cancel; break;
					case "btnOK":	    this.DialogResult = DialogResult.OK; break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.Close(); }
		}
	}
}
