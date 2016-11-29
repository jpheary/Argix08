using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Argix.Terminals {
	//
	public class dlgBatteryItem : System.Windows.Forms.Form {
		//Members
        private BatteryItem mItem=null;
		#region Controls

		private System.Windows.Forms.Label _lblStatus;
		private System.Windows.Forms.ComboBox cboStatus;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.Label _lblDeviceIDNumber;
		private System.Windows.Forms.TextBox txtBatteryID;
		private System.Windows.Forms.Label _lblComponentType;
		private System.Windows.Forms.ComboBox cboComponentType;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.DateTimePicker dtpInServiceDate;
		private System.Windows.Forms.Label _lblInServiceDate;
		private System.Windows.Forms.ComboBox cboTerminal;
		private System.Windows.Forms.Label _lblTerminal;
        private BindingSource mTypes;
        private BindingSource mTerminals;
        private Argix.Windows.SelectionList mStatusDS;
		private System.Windows.Forms.GroupBox _fraBattery;
		#endregion
				
		//Interface
		public dlgBatteryItem(BatteryItem item) {
			//Constructor
			try {
				InitializeComponent();
                this.mItem = item;
				this.Text = (this.mItem.ItemID.Length > 0) ? this.mItem.Terminal.Trim() + " Battery (" + this.mItem.ItemID + ")" : this.mItem.Terminal.Trim() + " Battery (New)";
			} 
			catch(Exception ex) { throw new ApplicationException("Failed to create new Battery Item dialog", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgBatteryItem));
            this._lblStatus = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this._lblComments = new System.Windows.Forms.Label();
            this._lblDeviceIDNumber = new System.Windows.Forms.Label();
            this.txtBatteryID = new System.Windows.Forms.TextBox();
            this._lblComponentType = new System.Windows.Forms.Label();
            this.cboComponentType = new System.Windows.Forms.ComboBox();
            this.mTypes = new System.Windows.Forms.BindingSource(this.components);
            this.txtComments = new System.Windows.Forms.TextBox();
            this._fraBattery = new System.Windows.Forms.GroupBox();
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this.mTerminals = new System.Windows.Forms.BindingSource(this.components);
            this._lblTerminal = new System.Windows.Forms.Label();
            this._lblInServiceDate = new System.Windows.Forms.Label();
            this.dtpInServiceDate = new System.Windows.Forms.DateTimePicker();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mStatusDS = new Argix.Windows.SelectionList();
            ((System.ComponentModel.ISupportInitialize)(this.mTypes)).BeginInit();
            this._fraBattery.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mStatusDS)).BeginInit();
            this.SuspendLayout();
            // 
            // _lblStatus
            // 
            this._lblStatus.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblStatus.Location = new System.Drawing.Point(6,96);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(120,16);
            this._lblStatus.TabIndex = 101;
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
            this.cboStatus.TabIndex = 3;
            this.cboStatus.ValueMember = "SelectionListTable.ID";
            this.cboStatus.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblComments
            // 
            this._lblComments.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblComments.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._lblComments.Location = new System.Drawing.Point(6,168);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(75,16);
            this._lblComments.TabIndex = 4;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblDeviceIDNumber
            // 
            this._lblDeviceIDNumber.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblDeviceIDNumber.Location = new System.Drawing.Point(6,48);
            this._lblDeviceIDNumber.Name = "_lblDeviceIDNumber";
            this._lblDeviceIDNumber.Size = new System.Drawing.Size(120,16);
            this._lblDeviceIDNumber.TabIndex = 92;
            this._lblDeviceIDNumber.Text = "Battery ID #";
            this._lblDeviceIDNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBatteryID
            // 
            this.txtBatteryID.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtBatteryID.Location = new System.Drawing.Point(132,48);
            this.txtBatteryID.Name = "txtBatteryID";
            this.txtBatteryID.Size = new System.Drawing.Size(144,21);
            this.txtBatteryID.TabIndex = 1;
            this.txtBatteryID.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblComponentType
            // 
            this._lblComponentType.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblComponentType.Location = new System.Drawing.Point(6,24);
            this._lblComponentType.Name = "_lblComponentType";
            this._lblComponentType.Size = new System.Drawing.Size(120,16);
            this._lblComponentType.TabIndex = 94;
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
            this.cboComponentType.Size = new System.Drawing.Size(216,21);
            this.cboComponentType.TabIndex = 2;
            this.cboComponentType.ValueMember = "TypeID";
            this.cboComponentType.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // mTypes
            // 
            this.mTypes.DataSource = typeof(Argix.Terminals.ComponentTypes);
            // 
            // txtComments
            // 
            this.txtComments.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtComments.Location = new System.Drawing.Point(6,186);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(366,36);
            this.txtComments.TabIndex = 1;
            this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _fraBattery
            // 
            this._fraBattery.Controls.Add(this._lblDeviceIDNumber);
            this._fraBattery.Controls.Add(this.txtBatteryID);
            this._fraBattery.Controls.Add(this._lblComponentType);
            this._fraBattery.Controls.Add(this.cboComponentType);
            this._fraBattery.Controls.Add(this.cboTerminal);
            this._fraBattery.Controls.Add(this._lblTerminal);
            this._fraBattery.Controls.Add(this._lblInServiceDate);
            this._fraBattery.Controls.Add(this.dtpInServiceDate);
            this._fraBattery.Controls.Add(this.cboStatus);
            this._fraBattery.Controls.Add(this._lblStatus);
            this._fraBattery.Location = new System.Drawing.Point(6,6);
            this._fraBattery.Name = "_fraBattery";
            this._fraBattery.Size = new System.Drawing.Size(366,156);
            this._fraBattery.TabIndex = 0;
            this._fraBattery.TabStop = false;
            this._fraBattery.Text = "Battery Details";
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
            this.cboTerminal.TabIndex = 104;
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
            this._lblTerminal.TabIndex = 103;
            this._lblTerminal.Text = "Terminal";
            this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblInServiceDate
            // 
            this._lblInServiceDate.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblInServiceDate.Location = new System.Drawing.Point(6,123);
            this._lblInServiceDate.Name = "_lblInServiceDate";
            this._lblInServiceDate.Size = new System.Drawing.Size(120,16);
            this._lblInServiceDate.TabIndex = 102;
            this._lblInServiceDate.Text = "In Service Date";
            this._lblInServiceDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpInServiceDate
            // 
            this.dtpInServiceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInServiceDate.Location = new System.Drawing.Point(132,123);
            this.dtpInServiceDate.MaxDate = new System.DateTime(2028,8,2,0,0,0,0);
            this.dtpInServiceDate.MinDate = new System.DateTime(2000,1,1,0,0,0,0);
            this.dtpInServiceDate.Name = "dtpInServiceDate";
            this.dtpInServiceDate.Size = new System.Drawing.Size(144,21);
            this.dtpInServiceDate.TabIndex = 0;
            this.dtpInServiceDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(159,228);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(104,23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(270,228);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(104,23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // mStatusDS
            // 
            this.mStatusDS.DataSetName = "SelectionList";
            this.mStatusDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mStatusDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dlgBatteryItem
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(378,256);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this._fraBattery);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this._lblComments);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgBatteryItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Battery";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mTypes)).EndInit();
            this._fraBattery.ResumeLayout(false);
            this._fraBattery.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mStatusDS)).EndInit();
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
                this.mTypes.DataSource = MobileDevicesProxy.GetComponentTypeList(true,"Battery"); //ComponentType.CATEGORYID_BATTERY);
                this.mStatusDS.Merge(this.mItem.StatusList);
                this.mTerminals.DataSource = MobileDevicesProxy.GetLocalTerminals();
				if(this.mItem.TypeID.Length > 0) 
					this.cboComponentType.SelectedValue = this.mItem.TypeID;
				else
					if(this.cboComponentType.Items.Count > 0)  this.cboComponentType.SelectedIndex = 0;
				this.cboComponentType.Enabled = (this.mItem.ItemID.Length == 0 && this.cboComponentType.Items.Count>0);
				this.txtBatteryID.MaxLength = 20;
				this.txtBatteryID.Text = this.mItem.ItemID;
				this.txtBatteryID.Enabled = false;
				this.cboTerminal.SelectedValue = this.mItem.TerminalID;
                this.cboTerminal.Enabled = (this.cboTerminal.Items.Count > 0 && App.Config.AllowTerminalChange);
				this.cboStatus.SelectedIndex = 0;
				this.cboStatus.Enabled = (this.mItem.ItemID.Length > 0);
				this.dtpInServiceDate.Value = this.mItem.InServiceDate;
                this.dtpInServiceDate.Enabled = (this.mItem.ItemID.Length == 0 && App.Config.AllowBatteryInService);
				this.txtComments.MaxLength = 100;
				this.txtComments.Text = this.mItem.Comments;
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.btnOK.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnTerminalChanged(object sender, System.EventArgs e) {
			//Even handler for change in selected terminal
			this.OnValidateForm(null, null);
		}
		private void OnValidateForm(object sender, System.EventArgs e) { 			
			//Event handler for changes to control data
			try {
				this.btnOK.Enabled = (	this.cboComponentType.Text.Length > 0 && 
										(this.mItem.ItemID.Length == 0 || this.txtBatteryID.Text.Length > 0) && 
										this.cboTerminal.Text.Length > 0 && this.cboStatus.Text.Length > 0 );
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
						this.mItem.TerminalID = Convert.ToInt64(this.cboTerminal.SelectedValue);
						this.mItem.Terminal = this.cboTerminal.Text;
						this.mItem.InServiceDate = this.dtpInServiceDate.Value;	//No support in database
						this.mItem.Comments = this.txtComments.Text;
                        this.mItem.IsActive = (this.cboStatus.SelectedValue.ToString() != MobileDevicesProxy.INACTIVE) ? (byte)1 : (byte)0;
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