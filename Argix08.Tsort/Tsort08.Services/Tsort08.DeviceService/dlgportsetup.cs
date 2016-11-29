//	File:	dlgportsetup.cs
//	Author:	J. Heary
//	Date:	07/20/04
//	Desc:	Lets users select serial port settings.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO.Ports;

namespace Tsort.Devices {
    /// <summary>Lets users select serial printer port settings.</summary>
    public class dlgPortSetup:System.Windows.Forms.Form {
		//Members
        private PortSettings mSettings;
		private const string CMD_CLOSE = "&Close";
		private const string CMD_OK = "O&K";
		#region Controls
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabAdvanced;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox fraDataConnection;
		private System.Windows.Forms.GroupBox fraHardware;
		private System.Windows.Forms.Label _lblPort;
		private System.Windows.Forms.ComboBox cboPort;
		private System.Windows.Forms.ComboBox cboBaud;
		private System.Windows.Forms.Label _lblPortSpeed;
		private System.Windows.Forms.ComboBox cboHandshake;
		private System.Windows.Forms.Label _lblFlowControl;
		private System.Windows.Forms.ComboBox cboStopBits;
		private System.Windows.Forms.Label _lblStopBits;
		private System.Windows.Forms.ComboBox cboParity;
		private System.Windows.Forms.Label _lblParity;
		private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.Label _lblDataBits;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion

        /// <summary>Initializes a new instance of the Tsort.Devices.dlgPortSetup class.</summary>
		public dlgPortSetup() { 
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.mSettings=new PortSettings("COM1",19200,7,Parity.None,StopBits.One,Handshake.None);
				this.btnClose.Text = CMD_CLOSE;
				this.btnOK.Text = CMD_OK;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Port Setup dialog instance.", ex); }
		}
        /// <summary>Gets or sets the port settings the dialog box modifies.</summary>
        public PortSettings PortSettings { get { return this.mSettings; } set { this.mSettings = value; } }
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.fraDataConnection = new System.Windows.Forms.GroupBox();
            this.cboHandshake = new System.Windows.Forms.ComboBox();
            this._lblFlowControl = new System.Windows.Forms.Label();
            this.cboBaud = new System.Windows.Forms.ComboBox();
            this._lblPortSpeed = new System.Windows.Forms.Label();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this._lblPort = new System.Windows.Forms.Label();
            this.tabAdvanced = new System.Windows.Forms.TabPage();
            this.fraHardware = new System.Windows.Forms.GroupBox();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this._lblStopBits = new System.Windows.Forms.Label();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this._lblParity = new System.Windows.Forms.Label();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this._lblDataBits = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.tabMain.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.fraDataConnection.SuspendLayout();
            this.tabAdvanced.SuspendLayout();
            this.fraHardware.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabGeneral);
            this.tabMain.Controls.Add(this.tabAdvanced);
            this.tabMain.Location = new System.Drawing.Point(6,6);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(366,306);
            this.tabMain.TabIndex = 0;
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.fraDataConnection);
            this.tabGeneral.Location = new System.Drawing.Point(4,22);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(358,280);
            this.tabGeneral.TabIndex = 0;
            this.tabGeneral.Text = "General";
            // 
            // fraDataConnection
            // 
            this.fraDataConnection.Controls.Add(this.cboHandshake);
            this.fraDataConnection.Controls.Add(this._lblFlowControl);
            this.fraDataConnection.Controls.Add(this.cboBaud);
            this.fraDataConnection.Controls.Add(this._lblPortSpeed);
            this.fraDataConnection.Controls.Add(this.cboPort);
            this.fraDataConnection.Controls.Add(this._lblPort);
            this.fraDataConnection.Location = new System.Drawing.Point(6,6);
            this.fraDataConnection.Name = "fraDataConnection";
            this.fraDataConnection.Size = new System.Drawing.Size(348,144);
            this.fraDataConnection.TabIndex = 0;
            this.fraDataConnection.TabStop = false;
            this.fraDataConnection.Text = "Data Connection Preferences";
            // 
            // cboHandshake
            // 
            this.cboHandshake.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboHandshake.Location = new System.Drawing.Point(108,81);
            this.cboHandshake.Name = "cboHandshake";
            this.cboHandshake.Size = new System.Drawing.Size(96,21);
            this.cboHandshake.TabIndex = 5;
            this.cboHandshake.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblFlowControl
            // 
            this._lblFlowControl.Location = new System.Drawing.Point(6,81);
            this._lblFlowControl.Name = "_lblFlowControl";
            this._lblFlowControl.Size = new System.Drawing.Size(96,18);
            this._lblFlowControl.TabIndex = 4;
            this._lblFlowControl.Text = "FlowControl";
            this._lblFlowControl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboBaud
            // 
            this.cboBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaud.Location = new System.Drawing.Point(108,54);
            this.cboBaud.Name = "cboBaud";
            this.cboBaud.Size = new System.Drawing.Size(96,21);
            this.cboBaud.TabIndex = 3;
            this.cboBaud.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblPortSpeed
            // 
            this._lblPortSpeed.Location = new System.Drawing.Point(6,54);
            this._lblPortSpeed.Name = "_lblPortSpeed";
            this._lblPortSpeed.Size = new System.Drawing.Size(96,18);
            this._lblPortSpeed.TabIndex = 2;
            this._lblPortSpeed.Text = "Port Speed";
            this._lblPortSpeed.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboPort
            // 
            this.cboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort.Location = new System.Drawing.Point(108,27);
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(96,21);
            this.cboPort.TabIndex = 1;
            this.cboPort.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblPort
            // 
            this._lblPort.Location = new System.Drawing.Point(6,27);
            this._lblPort.Name = "_lblPort";
            this._lblPort.Size = new System.Drawing.Size(96,18);
            this._lblPort.TabIndex = 0;
            this._lblPort.Text = "Port";
            this._lblPort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabAdvanced
            // 
            this.tabAdvanced.Controls.Add(this.fraHardware);
            this.tabAdvanced.Location = new System.Drawing.Point(4,22);
            this.tabAdvanced.Name = "tabAdvanced";
            this.tabAdvanced.Size = new System.Drawing.Size(358,280);
            this.tabAdvanced.TabIndex = 1;
            this.tabAdvanced.Text = "Advanced";
            // 
            // fraHardware
            // 
            this.fraHardware.Controls.Add(this.cboStopBits);
            this.fraHardware.Controls.Add(this._lblStopBits);
            this.fraHardware.Controls.Add(this.cboParity);
            this.fraHardware.Controls.Add(this._lblParity);
            this.fraHardware.Controls.Add(this.cboDataBits);
            this.fraHardware.Controls.Add(this._lblDataBits);
            this.fraHardware.Location = new System.Drawing.Point(6,6);
            this.fraHardware.Name = "fraHardware";
            this.fraHardware.Size = new System.Drawing.Size(348,144);
            this.fraHardware.TabIndex = 1;
            this.fraHardware.TabStop = false;
            this.fraHardware.Text = "Hardware Settings";
            // 
            // cboStopBits
            // 
            this.cboStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStopBits.Location = new System.Drawing.Point(108,81);
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(48,21);
            this.cboStopBits.TabIndex = 11;
            this.cboStopBits.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblStopBits
            // 
            this._lblStopBits.Location = new System.Drawing.Point(6,81);
            this._lblStopBits.Name = "_lblStopBits";
            this._lblStopBits.Size = new System.Drawing.Size(96,18);
            this._lblStopBits.TabIndex = 10;
            this._lblStopBits.Text = "Stop Bits";
            this._lblStopBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboParity
            // 
            this.cboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParity.Location = new System.Drawing.Point(108,54);
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(96,21);
            this.cboParity.TabIndex = 9;
            this.cboParity.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblParity
            // 
            this._lblParity.Location = new System.Drawing.Point(6,54);
            this._lblParity.Name = "_lblParity";
            this._lblParity.Size = new System.Drawing.Size(96,18);
            this._lblParity.TabIndex = 8;
            this._lblParity.Text = "Parity";
            this._lblParity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDataBits
            // 
            this.cboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBits.Location = new System.Drawing.Point(108,27);
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(48,21);
            this.cboDataBits.TabIndex = 7;
            this.cboDataBits.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblDataBits
            // 
            this._lblDataBits.Location = new System.Drawing.Point(6,27);
            this._lblDataBits.Name = "_lblDataBits";
            this._lblDataBits.Size = new System.Drawing.Size(96,18);
            this._lblDataBits.TabIndex = 6;
            this._lblDataBits.Text = "Data Bits";
            this._lblDataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(276,321);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96,24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(174,321);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96,24);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "O&K";
            this.btnOK.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // dlgPortSetup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(378,352);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabMain);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgPortSetup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Printer Properties";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.tabMain.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.fraDataConnection.ResumeLayout(false);
            this.tabAdvanced.ResumeLayout(false);
            this.fraHardware.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
                this.cboPort.Items.AddRange(new object[] { "COM1","COM2","COM3","COM4","COM5","COM6","COM7","COM8" });
				this.cboPort.SelectedItem = (this.mSettings.PortName != null) ? this.mSettings.PortName : "COM1";

                this.cboBaud.Items.AddRange(new object[] { "2400","4800","9600","19200","38400","76800" });
				this.cboBaud.SelectedItem = (this.mSettings.BaudRate > 0) ? this.mSettings.BaudRate.ToString() : "19200";
				
				this.cboDataBits.Items.AddRange(new object[] {"7","8"});
				this.cboDataBits.SelectedItem = (this.mSettings.DataBits > 0) ? this.mSettings.DataBits.ToString() : "7";
				
				this.cboParity.Items.AddRange(new object[] {Parity.None,Parity.Even,Parity.Odd,Parity.Mark,Parity.Space});
				this.cboParity.SelectedItem = (this.mSettings.Parity.ToString() != "") ? this.mSettings.Parity : Parity.None;
				
				this.cboStopBits.Items.AddRange(new object[] {StopBits.One,StopBits.Two});
				this.cboStopBits.SelectedItem = (this.mSettings.StopBits.ToString() != "") ? this.mSettings.StopBits : StopBits.One;

                this.cboHandshake.Items.AddRange(new object[] { Handshake.XOnXOff,Handshake.RequestToSend,Handshake.None });
                this.cboHandshake.SelectedItem = (this.mSettings.Handshake.ToString() != "") ? this.mSettings.Handshake : Handshake.None;
            }
			catch(Exception ex) { throw ex; }
			finally { OnValidateForm(null, null); this.Cursor = Cursors.Default; }
		}
		private void OnValidateForm(object sender, EventArgs e) {
			//Validate changes to form data and set OK service
			this.btnOK.Enabled = (	this.cboPort.Text!="" && this.cboBaud.Text!="" && this.cboHandshake.Text!="" && 
									this.cboDataBits.Text!="" && this.cboParity.Text!="" && this.cboStopBits.Text!="");
		}
		private void OnCommandClick(object sender, System.EventArgs e) {
			//Event handler for command selection
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CLOSE: 
						this.DialogResult = DialogResult.Cancel;
						break;
					case CMD_OK:
                        this.DialogResult = DialogResult.OK;
                        this.mSettings.PortName = this.cboPort.Text;
						this.mSettings.BaudRate = Convert.ToInt32(this.cboBaud.Text);
						this.mSettings.DataBits = Convert.ToInt32(this.cboDataBits.Text);
						switch(this.cboParity.Text.ToLower()) {
							case "none":	this.mSettings.Parity = Parity.None; break;
							case "even":	this.mSettings.Parity = Parity.Even; break;
							case "odd":		this.mSettings.Parity = Parity.Odd; break;
							case "mark":	this.mSettings.Parity = Parity.Mark; break;
							case "space":	this.mSettings.Parity = Parity.Space; break;
							default: this.mSettings.Parity = Parity.None; break;
						}
						switch(this.cboStopBits.Text.ToLower()) {
							case "one":		this.mSettings.StopBits = StopBits.One; break;
							case "two":		this.mSettings.StopBits = StopBits.Two; break;
							default:		this.mSettings.StopBits = StopBits.One; break;
						}
                        switch(this.cboHandshake.Text.ToLower()) {
                            case "none": this.mSettings.Handshake = Handshake.None; break;
                            case "requesttosend": this.mSettings.Handshake = Handshake.RequestToSend; break;
                            case "xonxoff": this.mSettings.Handshake = Handshake.XOnXOff; break;
                            default: this.mSettings.Handshake = Handshake.None; break;
                        }
                        break;
				}
				this.Close();
			}
			catch(Exception ex) {
				MessageBox.Show(ex.Message + "\n\n" + "Please try again, or cancel and notify the IT Dept.");
			}
		}
	}
}
