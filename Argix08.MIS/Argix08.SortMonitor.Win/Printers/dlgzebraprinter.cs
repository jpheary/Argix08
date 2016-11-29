//	File:	dlgzebraprinter.cs
//	Author:	J. Heary
//	Date:	11/18/04
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Tsort.Printers {
	//
	public class dlgZebraPrinter : System.Windows.Forms.Form {
		//Members
		private ZebraPrinter mPrinter=null;
		
		//Constants
		//Events
		#region Controls
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label lblBufferCount;
		private System.Windows.Forms.Label _lblBufferCount;
		private System.Windows.Forms.PictureBox picLabelWaiting;
		private System.Windows.Forms.PictureBox picRibbonOut;
		private System.Windows.Forms.PictureBox picHeadUp;
		private System.Windows.Forms.Label _lblRibbonOut;
		private System.Windows.Forms.Label _lblLabelWaiting;
		private System.Windows.Forms.Label _lblHeadUp;
		private System.Windows.Forms.PictureBox picOverTemp;
		private System.Windows.Forms.PictureBox picUnderTemp;
		private System.Windows.Forms.PictureBox picCorruptRAM;
		private System.Windows.Forms.PictureBox picPartialFormat;
		private System.Windows.Forms.Label _lblCorruptRAM;
		private System.Windows.Forms.Label _lblUnderTemp;
		private System.Windows.Forms.Label _lblOverTemp;
		private System.Windows.Forms.Label _lblPartialFormat;
		private System.Windows.Forms.PictureBox picBufferFull;
		private System.Windows.Forms.PictureBox picPauseOn;
		private System.Windows.Forms.PictureBox picPaperOut;
		private System.Windows.Forms.Label _lblPauseOn;
		private System.Windows.Forms.Label _lblBufferFull;
		private System.Windows.Forms.Label _lblPaperOut;
		private System.Windows.Forms.GroupBox fraHostID;
		private System.Windows.Forms.GroupBox fraHostStatus;
		private System.Windows.Forms.GroupBox fraHostMemoryStatus;
		private System.Windows.Forms.GroupBox fraComm;
		private System.Windows.Forms.GroupBox fraSettings;
		private System.Windows.Forms.Label lblPrintMode;
		private System.Windows.Forms.Label _lblPrintMode;
		private System.Windows.Forms.PictureBox picOn;
		private System.Windows.Forms.PictureBox picOff;
		private System.Windows.Forms.Label _lblRAMMax;
		private System.Windows.Forms.Label _lblRAMAvailable;
		private System.Windows.Forms.Label _lblRAMTotal;
		private System.Windows.Forms.Label lblRAMTotal;
		private System.Windows.Forms.Label lblRAMMax;
		private System.Windows.Forms.Label lblRAMAvailable;
		private System.Windows.Forms.Label _lblX;
		private System.Windows.Forms.Label _lblVersion;
		private System.Windows.Forms.Label _lblModel;
		private System.Windows.Forms.Label _lblResolution;
		private System.Windows.Forms.Label _lblMemory;
		private System.Windows.Forms.Label _lblPrintMode_;
		private System.Windows.Forms.Label _lblSensorProfile;
		private System.Windows.Forms.Label _lblMediaType;
		private System.Windows.Forms.Label _lblEnable;
		private System.Windows.Forms.Label _lblHandshake;
		private System.Windows.Forms.Label _lblStopBits;
		private System.Windows.Forms.Label _lblParity;
		private System.Windows.Forms.Label _lblDataBits;
		private System.Windows.Forms.Label _lblBaud;
		private System.Windows.Forms.Label lblModel;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblResolution;
		private System.Windows.Forms.Label lblMemory;
		private System.Windows.Forms.Label lblX;
		private System.Windows.Forms.Label lblBaud;
		private System.Windows.Forms.Label lblDataBits;
		private System.Windows.Forms.Label lblParity;
		private System.Windows.Forms.Label lblStopBits;
		private System.Windows.Forms.Label lblHandshake;
		private System.Windows.Forms.Label lblEnable;
		private System.Windows.Forms.Label lblMediaType;
		private System.Windows.Forms.Label lblSensorProfile;
		private System.Windows.Forms.Label lblPrintMode_;
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
		public dlgZebraPrinter(ZebraPrinter printer) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.mPrinter = printer;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Zebra Printer dialog instance.", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgZebraPrinter));
			this.btnClose = new System.Windows.Forms.Button();
			this.fraHostID = new System.Windows.Forms.GroupBox();
			this.lblX = new System.Windows.Forms.Label();
			this.lblMemory = new System.Windows.Forms.Label();
			this.lblResolution = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblModel = new System.Windows.Forms.Label();
			this._lblX = new System.Windows.Forms.Label();
			this._lblVersion = new System.Windows.Forms.Label();
			this._lblModel = new System.Windows.Forms.Label();
			this._lblResolution = new System.Windows.Forms.Label();
			this._lblMemory = new System.Windows.Forms.Label();
			this.fraHostStatus = new System.Windows.Forms.GroupBox();
			this.lblPrintMode = new System.Windows.Forms.Label();
			this._lblPrintMode = new System.Windows.Forms.Label();
			this.fraSettings = new System.Windows.Forms.GroupBox();
			this.lblPrintMode_ = new System.Windows.Forms.Label();
			this.lblSensorProfile = new System.Windows.Forms.Label();
			this.lblMediaType = new System.Windows.Forms.Label();
			this._lblPrintMode_ = new System.Windows.Forms.Label();
			this._lblSensorProfile = new System.Windows.Forms.Label();
			this._lblMediaType = new System.Windows.Forms.Label();
			this.fraComm = new System.Windows.Forms.GroupBox();
			this.lblEnable = new System.Windows.Forms.Label();
			this.lblHandshake = new System.Windows.Forms.Label();
			this.lblStopBits = new System.Windows.Forms.Label();
			this.lblParity = new System.Windows.Forms.Label();
			this.lblDataBits = new System.Windows.Forms.Label();
			this.lblBaud = new System.Windows.Forms.Label();
			this._lblEnable = new System.Windows.Forms.Label();
			this._lblHandshake = new System.Windows.Forms.Label();
			this._lblStopBits = new System.Windows.Forms.Label();
			this._lblParity = new System.Windows.Forms.Label();
			this._lblDataBits = new System.Windows.Forms.Label();
			this._lblBaud = new System.Windows.Forms.Label();
			this.lblBufferCount = new System.Windows.Forms.Label();
			this._lblBufferCount = new System.Windows.Forms.Label();
			this.picLabelWaiting = new System.Windows.Forms.PictureBox();
			this.picRibbonOut = new System.Windows.Forms.PictureBox();
			this.picHeadUp = new System.Windows.Forms.PictureBox();
			this._lblRibbonOut = new System.Windows.Forms.Label();
			this._lblLabelWaiting = new System.Windows.Forms.Label();
			this._lblHeadUp = new System.Windows.Forms.Label();
			this.picOverTemp = new System.Windows.Forms.PictureBox();
			this.picUnderTemp = new System.Windows.Forms.PictureBox();
			this.picCorruptRAM = new System.Windows.Forms.PictureBox();
			this.picPartialFormat = new System.Windows.Forms.PictureBox();
			this._lblCorruptRAM = new System.Windows.Forms.Label();
			this._lblUnderTemp = new System.Windows.Forms.Label();
			this._lblOverTemp = new System.Windows.Forms.Label();
			this._lblPartialFormat = new System.Windows.Forms.Label();
			this.picBufferFull = new System.Windows.Forms.PictureBox();
			this.picPauseOn = new System.Windows.Forms.PictureBox();
			this.picPaperOut = new System.Windows.Forms.PictureBox();
			this._lblPauseOn = new System.Windows.Forms.Label();
			this._lblBufferFull = new System.Windows.Forms.Label();
			this._lblPaperOut = new System.Windows.Forms.Label();
			this.fraHostMemoryStatus = new System.Windows.Forms.GroupBox();
			this.lblRAMAvailable = new System.Windows.Forms.Label();
			this.lblRAMMax = new System.Windows.Forms.Label();
			this.lblRAMTotal = new System.Windows.Forms.Label();
			this._lblRAMMax = new System.Windows.Forms.Label();
			this._lblRAMAvailable = new System.Windows.Forms.Label();
			this._lblRAMTotal = new System.Windows.Forms.Label();
			this.picOn = new System.Windows.Forms.PictureBox();
			this.picOff = new System.Windows.Forms.PictureBox();
			this.fraHostID.SuspendLayout();
			this.fraHostStatus.SuspendLayout();
			this.fraSettings.SuspendLayout();
			this.fraComm.SuspendLayout();
			this.fraHostMemoryStatus.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(468, 345);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 24);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.OnCommandClick);
			// 
			// fraHostID
			// 
			this.fraHostID.Controls.Add(this.lblX);
			this.fraHostID.Controls.Add(this.lblMemory);
			this.fraHostID.Controls.Add(this.lblResolution);
			this.fraHostID.Controls.Add(this.lblVersion);
			this.fraHostID.Controls.Add(this.lblModel);
			this.fraHostID.Controls.Add(this._lblX);
			this.fraHostID.Controls.Add(this._lblVersion);
			this.fraHostID.Controls.Add(this._lblModel);
			this.fraHostID.Controls.Add(this._lblResolution);
			this.fraHostID.Controls.Add(this._lblMemory);
			this.fraHostID.Location = new System.Drawing.Point(6, 9);
			this.fraHostID.Name = "fraHostID";
			this.fraHostID.Size = new System.Drawing.Size(168, 201);
			this.fraHostID.TabIndex = 118;
			this.fraHostID.TabStop = false;
			this.fraHostID.Text = "Host Identification";
			// 
			// lblX
			// 
			this.lblX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblX.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblX.Location = new System.Drawing.Point(90, 122);
			this.lblX.Name = "lblX";
			this.lblX.Size = new System.Drawing.Size(72, 18);
			this.lblX.TabIndex = 121;
			this.lblX.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMemory
			// 
			this.lblMemory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMemory.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblMemory.Location = new System.Drawing.Point(90, 98);
			this.lblMemory.Name = "lblMemory";
			this.lblMemory.Size = new System.Drawing.Size(72, 18);
			this.lblMemory.TabIndex = 120;
			this.lblMemory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblResolution
			// 
			this.lblResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblResolution.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblResolution.Location = new System.Drawing.Point(90, 74);
			this.lblResolution.Name = "lblResolution";
			this.lblResolution.Size = new System.Drawing.Size(72, 18);
			this.lblResolution.TabIndex = 119;
			this.lblResolution.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Location = new System.Drawing.Point(90, 50);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(72, 18);
			this.lblVersion.TabIndex = 118;
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblModel
			// 
			this.lblModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblModel.Location = new System.Drawing.Point(66, 26);
			this.lblModel.Name = "lblModel";
			this.lblModel.Size = new System.Drawing.Size(96, 18);
			this.lblModel.TabIndex = 117;
			this.lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblX
			// 
			this._lblX.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblX.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblX.Location = new System.Drawing.Point(6, 122);
			this._lblX.Name = "_lblX";
			this._lblX.Size = new System.Drawing.Size(72, 18);
			this._lblX.TabIndex = 116;
			this._lblX.Text = "X: ";
			this._lblX.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblVersion
			// 
			this._lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblVersion.Location = new System.Drawing.Point(6, 50);
			this._lblVersion.Name = "_lblVersion";
			this._lblVersion.Size = new System.Drawing.Size(72, 18);
			this._lblVersion.TabIndex = 115;
			this._lblVersion.Text = "Version: ";
			this._lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblModel
			// 
			this._lblModel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblModel.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblModel.Location = new System.Drawing.Point(6, 26);
			this._lblModel.Name = "_lblModel";
			this._lblModel.Size = new System.Drawing.Size(48, 18);
			this._lblModel.TabIndex = 112;
			this._lblModel.Text = "Model: ";
			this._lblModel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblResolution
			// 
			this._lblResolution.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblResolution.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblResolution.Location = new System.Drawing.Point(6, 74);
			this._lblResolution.Name = "_lblResolution";
			this._lblResolution.Size = new System.Drawing.Size(72, 18);
			this._lblResolution.TabIndex = 114;
			this._lblResolution.Text = "Resolution: ";
			this._lblResolution.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblMemory
			// 
			this._lblMemory.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblMemory.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblMemory.Location = new System.Drawing.Point(6, 98);
			this._lblMemory.Name = "_lblMemory";
			this._lblMemory.Size = new System.Drawing.Size(72, 18);
			this._lblMemory.TabIndex = 113;
			this._lblMemory.Text = "Memory: ";
			this._lblMemory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fraHostStatus
			// 
			this.fraHostStatus.Controls.Add(this.lblPrintMode);
			this.fraHostStatus.Controls.Add(this._lblPrintMode);
			this.fraHostStatus.Controls.Add(this.fraSettings);
			this.fraHostStatus.Controls.Add(this.fraComm);
			this.fraHostStatus.Controls.Add(this.lblBufferCount);
			this.fraHostStatus.Controls.Add(this._lblBufferCount);
			this.fraHostStatus.Controls.Add(this.picLabelWaiting);
			this.fraHostStatus.Controls.Add(this.picRibbonOut);
			this.fraHostStatus.Controls.Add(this.picHeadUp);
			this.fraHostStatus.Controls.Add(this._lblRibbonOut);
			this.fraHostStatus.Controls.Add(this._lblLabelWaiting);
			this.fraHostStatus.Controls.Add(this._lblHeadUp);
			this.fraHostStatus.Controls.Add(this.picOverTemp);
			this.fraHostStatus.Controls.Add(this.picUnderTemp);
			this.fraHostStatus.Controls.Add(this.picCorruptRAM);
			this.fraHostStatus.Controls.Add(this.picPartialFormat);
			this.fraHostStatus.Controls.Add(this._lblCorruptRAM);
			this.fraHostStatus.Controls.Add(this._lblUnderTemp);
			this.fraHostStatus.Controls.Add(this._lblOverTemp);
			this.fraHostStatus.Controls.Add(this._lblPartialFormat);
			this.fraHostStatus.Controls.Add(this.picBufferFull);
			this.fraHostStatus.Controls.Add(this.picPauseOn);
			this.fraHostStatus.Controls.Add(this.picPaperOut);
			this.fraHostStatus.Controls.Add(this._lblPauseOn);
			this.fraHostStatus.Controls.Add(this._lblBufferFull);
			this.fraHostStatus.Controls.Add(this._lblPaperOut);
			this.fraHostStatus.Location = new System.Drawing.Point(180, 9);
			this.fraHostStatus.Name = "fraHostStatus";
			this.fraHostStatus.Size = new System.Drawing.Size(384, 321);
			this.fraHostStatus.TabIndex = 120;
			this.fraHostStatus.TabStop = false;
			this.fraHostStatus.Text = "Host Status Return";
			// 
			// lblPrintMode
			// 
			this.lblPrintMode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPrintMode.Location = new System.Drawing.Point(327, 288);
			this.lblPrintMode.Name = "lblPrintMode";
			this.lblPrintMode.Size = new System.Drawing.Size(48, 18);
			this.lblPrintMode.TabIndex = 143;
			this.lblPrintMode.Text = "peel off";
			this.lblPrintMode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblPrintMode
			// 
			this._lblPrintMode.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblPrintMode.Location = new System.Drawing.Point(222, 288);
			this._lblPrintMode.Name = "_lblPrintMode";
			this._lblPrintMode.Size = new System.Drawing.Size(96, 18);
			this._lblPrintMode.TabIndex = 142;
			this._lblPrintMode.Text = "Print Mode";
			this._lblPrintMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fraSettings
			// 
			this.fraSettings.Controls.Add(this.lblPrintMode_);
			this.fraSettings.Controls.Add(this.lblSensorProfile);
			this.fraSettings.Controls.Add(this.lblMediaType);
			this.fraSettings.Controls.Add(this._lblPrintMode_);
			this.fraSettings.Controls.Add(this._lblSensorProfile);
			this.fraSettings.Controls.Add(this._lblMediaType);
			this.fraSettings.Location = new System.Drawing.Point(12, 210);
			this.fraSettings.Name = "fraSettings";
			this.fraSettings.Size = new System.Drawing.Size(192, 102);
			this.fraSettings.TabIndex = 141;
			this.fraSettings.TabStop = false;
			this.fraSettings.Text = "Function Settings";
			// 
			// lblPrintMode_
			// 
			this.lblPrintMode_.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPrintMode_.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblPrintMode_.Location = new System.Drawing.Point(90, 74);
			this.lblPrintMode_.Name = "lblPrintMode_";
			this.lblPrintMode_.Size = new System.Drawing.Size(96, 18);
			this.lblPrintMode_.TabIndex = 127;
			this.lblPrintMode_.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblSensorProfile
			// 
			this.lblSensorProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblSensorProfile.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSensorProfile.Location = new System.Drawing.Point(90, 50);
			this.lblSensorProfile.Name = "lblSensorProfile";
			this.lblSensorProfile.Size = new System.Drawing.Size(96, 18);
			this.lblSensorProfile.TabIndex = 126;
			this.lblSensorProfile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMediaType
			// 
			this.lblMediaType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblMediaType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblMediaType.Location = new System.Drawing.Point(90, 26);
			this.lblMediaType.Name = "lblMediaType";
			this.lblMediaType.Size = new System.Drawing.Size(96, 18);
			this.lblMediaType.TabIndex = 125;
			this.lblMediaType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblPrintMode_
			// 
			this._lblPrintMode_.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblPrintMode_.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblPrintMode_.Location = new System.Drawing.Point(6, 74);
			this._lblPrintMode_.Name = "_lblPrintMode_";
			this._lblPrintMode_.Size = new System.Drawing.Size(72, 18);
			this._lblPrintMode_.TabIndex = 121;
			this._lblPrintMode_.Text = "Print Mode: ";
			this._lblPrintMode_.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblSensorProfile
			// 
			this._lblSensorProfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblSensorProfile.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblSensorProfile.Location = new System.Drawing.Point(6, 50);
			this._lblSensorProfile.Name = "_lblSensorProfile";
			this._lblSensorProfile.Size = new System.Drawing.Size(72, 18);
			this._lblSensorProfile.TabIndex = 120;
			this._lblSensorProfile.Text = "Sensor: ";
			this._lblSensorProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblMediaType
			// 
			this._lblMediaType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblMediaType.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblMediaType.Location = new System.Drawing.Point(6, 26);
			this._lblMediaType.Name = "_lblMediaType";
			this._lblMediaType.Size = new System.Drawing.Size(72, 18);
			this._lblMediaType.TabIndex = 119;
			this._lblMediaType.Text = "Media: ";
			this._lblMediaType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fraComm
			// 
			this.fraComm.Controls.Add(this.lblEnable);
			this.fraComm.Controls.Add(this.lblHandshake);
			this.fraComm.Controls.Add(this.lblStopBits);
			this.fraComm.Controls.Add(this.lblParity);
			this.fraComm.Controls.Add(this.lblDataBits);
			this.fraComm.Controls.Add(this.lblBaud);
			this.fraComm.Controls.Add(this._lblEnable);
			this.fraComm.Controls.Add(this._lblHandshake);
			this.fraComm.Controls.Add(this._lblStopBits);
			this.fraComm.Controls.Add(this._lblParity);
			this.fraComm.Controls.Add(this._lblDataBits);
			this.fraComm.Controls.Add(this._lblBaud);
			this.fraComm.Location = new System.Drawing.Point(12, 24);
			this.fraComm.Name = "fraComm";
			this.fraComm.Size = new System.Drawing.Size(192, 177);
			this.fraComm.TabIndex = 140;
			this.fraComm.TabStop = false;
			this.fraComm.Text = "COM Settings";
			// 
			// lblEnable
			// 
			this.lblEnable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblEnable.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblEnable.Location = new System.Drawing.Point(90, 146);
			this.lblEnable.Name = "lblEnable";
			this.lblEnable.Size = new System.Drawing.Size(96, 18);
			this.lblEnable.TabIndex = 124;
			this.lblEnable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblHandshake
			// 
			this.lblHandshake.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblHandshake.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblHandshake.Location = new System.Drawing.Point(90, 122);
			this.lblHandshake.Name = "lblHandshake";
			this.lblHandshake.Size = new System.Drawing.Size(96, 18);
			this.lblHandshake.TabIndex = 123;
			this.lblHandshake.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblStopBits
			// 
			this.lblStopBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblStopBits.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblStopBits.Location = new System.Drawing.Point(90, 98);
			this.lblStopBits.Name = "lblStopBits";
			this.lblStopBits.Size = new System.Drawing.Size(96, 18);
			this.lblStopBits.TabIndex = 122;
			this.lblStopBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblParity
			// 
			this.lblParity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblParity.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblParity.Location = new System.Drawing.Point(90, 74);
			this.lblParity.Name = "lblParity";
			this.lblParity.Size = new System.Drawing.Size(96, 18);
			this.lblParity.TabIndex = 121;
			this.lblParity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblDataBits
			// 
			this.lblDataBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblDataBits.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDataBits.Location = new System.Drawing.Point(90, 50);
			this.lblDataBits.Name = "lblDataBits";
			this.lblDataBits.Size = new System.Drawing.Size(96, 18);
			this.lblDataBits.TabIndex = 120;
			this.lblDataBits.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblBaud
			// 
			this.lblBaud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblBaud.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblBaud.Location = new System.Drawing.Point(90, 26);
			this.lblBaud.Name = "lblBaud";
			this.lblBaud.Size = new System.Drawing.Size(96, 18);
			this.lblBaud.TabIndex = 119;
			this.lblBaud.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblEnable
			// 
			this._lblEnable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblEnable.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblEnable.Location = new System.Drawing.Point(6, 146);
			this._lblEnable.Name = "_lblEnable";
			this._lblEnable.Size = new System.Drawing.Size(72, 18);
			this._lblEnable.TabIndex = 118;
			this._lblEnable.Text = "Enable: ";
			this._lblEnable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblHandshake
			// 
			this._lblHandshake.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblHandshake.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblHandshake.Location = new System.Drawing.Point(6, 122);
			this._lblHandshake.Name = "_lblHandshake";
			this._lblHandshake.Size = new System.Drawing.Size(72, 18);
			this._lblHandshake.TabIndex = 117;
			this._lblHandshake.Text = "Handshake: ";
			this._lblHandshake.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblStopBits
			// 
			this._lblStopBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblStopBits.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblStopBits.Location = new System.Drawing.Point(6, 98);
			this._lblStopBits.Name = "_lblStopBits";
			this._lblStopBits.Size = new System.Drawing.Size(72, 18);
			this._lblStopBits.TabIndex = 116;
			this._lblStopBits.Text = "Stop Bits: ";
			this._lblStopBits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblParity
			// 
			this._lblParity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblParity.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblParity.Location = new System.Drawing.Point(6, 74);
			this._lblParity.Name = "_lblParity";
			this._lblParity.Size = new System.Drawing.Size(72, 18);
			this._lblParity.TabIndex = 115;
			this._lblParity.Text = "Parity: ";
			this._lblParity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblDataBits
			// 
			this._lblDataBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblDataBits.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblDataBits.Location = new System.Drawing.Point(6, 50);
			this._lblDataBits.Name = "_lblDataBits";
			this._lblDataBits.Size = new System.Drawing.Size(72, 18);
			this._lblDataBits.TabIndex = 114;
			this._lblDataBits.Text = "Data Bits: ";
			this._lblDataBits.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblBaud
			// 
			this._lblBaud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblBaud.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblBaud.Location = new System.Drawing.Point(6, 26);
			this._lblBaud.Name = "_lblBaud";
			this._lblBaud.Size = new System.Drawing.Size(72, 18);
			this._lblBaud.TabIndex = 113;
			this._lblBaud.Text = "Baud Rate: ";
			this._lblBaud.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblBufferCount
			// 
			this.lblBufferCount.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblBufferCount.Location = new System.Drawing.Point(345, 24);
			this.lblBufferCount.Name = "lblBufferCount";
			this.lblBufferCount.Size = new System.Drawing.Size(32, 18);
			this.lblBufferCount.TabIndex = 139;
			this.lblBufferCount.Text = "000";
			this.lblBufferCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// _lblBufferCount
			// 
			this._lblBufferCount.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblBufferCount.Location = new System.Drawing.Point(222, 24);
			this._lblBufferCount.Name = "_lblBufferCount";
			this._lblBufferCount.Size = new System.Drawing.Size(96, 18);
			this._lblBufferCount.TabIndex = 138;
			this._lblBufferCount.Text = "Buffer Count";
			this._lblBufferCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picLabelWaiting
			// 
			this.picLabelWaiting.Image = ((System.Drawing.Image)(resources.GetObject("picLabelWaiting.Image")));
			this.picLabelWaiting.Location = new System.Drawing.Point(354, 96);
			this.picLabelWaiting.Name = "picLabelWaiting";
			this.picLabelWaiting.Size = new System.Drawing.Size(18, 18);
			this.picLabelWaiting.TabIndex = 136;
			this.picLabelWaiting.TabStop = false;
			// 
			// picRibbonOut
			// 
			this.picRibbonOut.Image = ((System.Drawing.Image)(resources.GetObject("picRibbonOut.Image")));
			this.picRibbonOut.Location = new System.Drawing.Point(354, 264);
			this.picRibbonOut.Name = "picRibbonOut";
			this.picRibbonOut.Size = new System.Drawing.Size(18, 18);
			this.picRibbonOut.TabIndex = 135;
			this.picRibbonOut.TabStop = false;
			// 
			// picHeadUp
			// 
			this.picHeadUp.Image = ((System.Drawing.Image)(resources.GetObject("picHeadUp.Image")));
			this.picHeadUp.Location = new System.Drawing.Point(354, 216);
			this.picHeadUp.Name = "picHeadUp";
			this.picHeadUp.Size = new System.Drawing.Size(18, 18);
			this.picHeadUp.TabIndex = 134;
			this.picHeadUp.TabStop = false;
			// 
			// _lblRibbonOut
			// 
			this._lblRibbonOut.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblRibbonOut.Location = new System.Drawing.Point(222, 264);
			this._lblRibbonOut.Name = "_lblRibbonOut";
			this._lblRibbonOut.Size = new System.Drawing.Size(96, 18);
			this._lblRibbonOut.TabIndex = 133;
			this._lblRibbonOut.Text = "Ribbon Out";
			this._lblRibbonOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblLabelWaiting
			// 
			this._lblLabelWaiting.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblLabelWaiting.Location = new System.Drawing.Point(222, 96);
			this._lblLabelWaiting.Name = "_lblLabelWaiting";
			this._lblLabelWaiting.Size = new System.Drawing.Size(96, 18);
			this._lblLabelWaiting.TabIndex = 132;
			this._lblLabelWaiting.Text = "Label Waiting";
			this._lblLabelWaiting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblHeadUp
			// 
			this._lblHeadUp.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblHeadUp.Location = new System.Drawing.Point(222, 216);
			this._lblHeadUp.Name = "_lblHeadUp";
			this._lblHeadUp.Size = new System.Drawing.Size(96, 18);
			this._lblHeadUp.TabIndex = 130;
			this._lblHeadUp.Text = "Head Up";
			this._lblHeadUp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picOverTemp
			// 
			this.picOverTemp.Image = ((System.Drawing.Image)(resources.GetObject("picOverTemp.Image")));
			this.picOverTemp.Location = new System.Drawing.Point(354, 192);
			this.picOverTemp.Name = "picOverTemp";
			this.picOverTemp.Size = new System.Drawing.Size(18, 18);
			this.picOverTemp.TabIndex = 127;
			this.picOverTemp.TabStop = false;
			// 
			// picUnderTemp
			// 
			this.picUnderTemp.Image = ((System.Drawing.Image)(resources.GetObject("picUnderTemp.Image")));
			this.picUnderTemp.Location = new System.Drawing.Point(354, 168);
			this.picUnderTemp.Name = "picUnderTemp";
			this.picUnderTemp.Size = new System.Drawing.Size(18, 18);
			this.picUnderTemp.TabIndex = 126;
			this.picUnderTemp.TabStop = false;
			// 
			// picCorruptRAM
			// 
			this.picCorruptRAM.Image = ((System.Drawing.Image)(resources.GetObject("picCorruptRAM.Image")));
			this.picCorruptRAM.Location = new System.Drawing.Point(354, 144);
			this.picCorruptRAM.Name = "picCorruptRAM";
			this.picCorruptRAM.Size = new System.Drawing.Size(18, 18);
			this.picCorruptRAM.TabIndex = 125;
			this.picCorruptRAM.TabStop = false;
			// 
			// picPartialFormat
			// 
			this.picPartialFormat.Image = ((System.Drawing.Image)(resources.GetObject("picPartialFormat.Image")));
			this.picPartialFormat.Location = new System.Drawing.Point(354, 72);
			this.picPartialFormat.Name = "picPartialFormat";
			this.picPartialFormat.Size = new System.Drawing.Size(18, 18);
			this.picPartialFormat.TabIndex = 124;
			this.picPartialFormat.TabStop = false;
			// 
			// _lblCorruptRAM
			// 
			this._lblCorruptRAM.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblCorruptRAM.Location = new System.Drawing.Point(222, 144);
			this._lblCorruptRAM.Name = "_lblCorruptRAM";
			this._lblCorruptRAM.Size = new System.Drawing.Size(96, 18);
			this._lblCorruptRAM.TabIndex = 123;
			this._lblCorruptRAM.Text = "Corrupt RAM";
			this._lblCorruptRAM.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblUnderTemp
			// 
			this._lblUnderTemp.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblUnderTemp.Location = new System.Drawing.Point(222, 168);
			this._lblUnderTemp.Name = "_lblUnderTemp";
			this._lblUnderTemp.Size = new System.Drawing.Size(96, 18);
			this._lblUnderTemp.TabIndex = 122;
			this._lblUnderTemp.Text = "Under Temp";
			this._lblUnderTemp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblOverTemp
			// 
			this._lblOverTemp.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblOverTemp.Location = new System.Drawing.Point(222, 192);
			this._lblOverTemp.Name = "_lblOverTemp";
			this._lblOverTemp.Size = new System.Drawing.Size(96, 18);
			this._lblOverTemp.TabIndex = 121;
			this._lblOverTemp.Text = "Over Temp";
			this._lblOverTemp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblPartialFormat
			// 
			this._lblPartialFormat.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblPartialFormat.Location = new System.Drawing.Point(222, 72);
			this._lblPartialFormat.Name = "_lblPartialFormat";
			this._lblPartialFormat.Size = new System.Drawing.Size(96, 18);
			this._lblPartialFormat.TabIndex = 120;
			this._lblPartialFormat.Text = "Partial Format";
			this._lblPartialFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picBufferFull
			// 
			this.picBufferFull.Image = ((System.Drawing.Image)(resources.GetObject("picBufferFull.Image")));
			this.picBufferFull.Location = new System.Drawing.Point(354, 48);
			this.picBufferFull.Name = "picBufferFull";
			this.picBufferFull.Size = new System.Drawing.Size(18, 18);
			this.picBufferFull.TabIndex = 118;
			this.picBufferFull.TabStop = false;
			// 
			// picPauseOn
			// 
			this.picPauseOn.Image = ((System.Drawing.Image)(resources.GetObject("picPauseOn.Image")));
			this.picPauseOn.Location = new System.Drawing.Point(354, 120);
			this.picPauseOn.Name = "picPauseOn";
			this.picPauseOn.Size = new System.Drawing.Size(18, 18);
			this.picPauseOn.TabIndex = 117;
			this.picPauseOn.TabStop = false;
			// 
			// picPaperOut
			// 
			this.picPaperOut.Image = ((System.Drawing.Image)(resources.GetObject("picPaperOut.Image")));
			this.picPaperOut.Location = new System.Drawing.Point(354, 240);
			this.picPaperOut.Name = "picPaperOut";
			this.picPaperOut.Size = new System.Drawing.Size(18, 18);
			this.picPaperOut.TabIndex = 116;
			this.picPaperOut.TabStop = false;
			// 
			// _lblPauseOn
			// 
			this._lblPauseOn.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblPauseOn.Location = new System.Drawing.Point(222, 120);
			this._lblPauseOn.Name = "_lblPauseOn";
			this._lblPauseOn.Size = new System.Drawing.Size(96, 18);
			this._lblPauseOn.TabIndex = 115;
			this._lblPauseOn.Text = "Pause On";
			this._lblPauseOn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblBufferFull
			// 
			this._lblBufferFull.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblBufferFull.Location = new System.Drawing.Point(222, 48);
			this._lblBufferFull.Name = "_lblBufferFull";
			this._lblBufferFull.Size = new System.Drawing.Size(96, 18);
			this._lblBufferFull.TabIndex = 114;
			this._lblBufferFull.Text = "Buffer Full";
			this._lblBufferFull.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblPaperOut
			// 
			this._lblPaperOut.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblPaperOut.Location = new System.Drawing.Point(222, 240);
			this._lblPaperOut.Name = "_lblPaperOut";
			this._lblPaperOut.Size = new System.Drawing.Size(96, 18);
			this._lblPaperOut.TabIndex = 112;
			this._lblPaperOut.Text = "Paper Out";
			this._lblPaperOut.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fraHostMemoryStatus
			// 
			this.fraHostMemoryStatus.Controls.Add(this.lblRAMAvailable);
			this.fraHostMemoryStatus.Controls.Add(this.lblRAMMax);
			this.fraHostMemoryStatus.Controls.Add(this.lblRAMTotal);
			this.fraHostMemoryStatus.Controls.Add(this._lblRAMMax);
			this.fraHostMemoryStatus.Controls.Add(this._lblRAMAvailable);
			this.fraHostMemoryStatus.Controls.Add(this._lblRAMTotal);
			this.fraHostMemoryStatus.Location = new System.Drawing.Point(6, 219);
			this.fraHostMemoryStatus.Name = "fraHostMemoryStatus";
			this.fraHostMemoryStatus.Size = new System.Drawing.Size(168, 111);
			this.fraHostMemoryStatus.TabIndex = 119;
			this.fraHostMemoryStatus.TabStop = false;
			this.fraHostMemoryStatus.Text = "Host Memory Status";
			// 
			// lblRAMAvailable
			// 
			this.lblRAMAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblRAMAvailable.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRAMAvailable.Location = new System.Drawing.Point(90, 74);
			this.lblRAMAvailable.Name = "lblRAMAvailable";
			this.lblRAMAvailable.Size = new System.Drawing.Size(72, 18);
			this.lblRAMAvailable.TabIndex = 118;
			this.lblRAMAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblRAMMax
			// 
			this.lblRAMMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblRAMMax.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRAMMax.Location = new System.Drawing.Point(90, 50);
			this.lblRAMMax.Name = "lblRAMMax";
			this.lblRAMMax.Size = new System.Drawing.Size(72, 18);
			this.lblRAMMax.TabIndex = 117;
			this.lblRAMMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblRAMTotal
			// 
			this.lblRAMTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblRAMTotal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRAMTotal.Location = new System.Drawing.Point(90, 26);
			this.lblRAMTotal.Name = "lblRAMTotal";
			this.lblRAMTotal.Size = new System.Drawing.Size(72, 18);
			this.lblRAMTotal.TabIndex = 116;
			this.lblRAMTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblRAMMax
			// 
			this._lblRAMMax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblRAMMax.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblRAMMax.Location = new System.Drawing.Point(6, 50);
			this._lblRAMMax.Name = "_lblRAMMax";
			this._lblRAMMax.Size = new System.Drawing.Size(72, 18);
			this._lblRAMMax.TabIndex = 115;
			this._lblRAMMax.Text = "Max: ";
			this._lblRAMMax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblRAMAvailable
			// 
			this._lblRAMAvailable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblRAMAvailable.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblRAMAvailable.Location = new System.Drawing.Point(6, 74);
			this._lblRAMAvailable.Name = "_lblRAMAvailable";
			this._lblRAMAvailable.Size = new System.Drawing.Size(72, 18);
			this._lblRAMAvailable.TabIndex = 114;
			this._lblRAMAvailable.Text = "Available: ";
			this._lblRAMAvailable.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// _lblRAMTotal
			// 
			this._lblRAMTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblRAMTotal.ForeColor = System.Drawing.SystemColors.ControlText;
			this._lblRAMTotal.Location = new System.Drawing.Point(6, 26);
			this._lblRAMTotal.Name = "_lblRAMTotal";
			this._lblRAMTotal.Size = new System.Drawing.Size(72, 18);
			this._lblRAMTotal.TabIndex = 112;
			this._lblRAMTotal.Text = "Total: ";
			this._lblRAMTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// picOn
			// 
			this.picOn.Image = ((System.Drawing.Image)(resources.GetObject("picOn.Image")));
			this.picOn.Location = new System.Drawing.Point(9, 342);
			this.picOn.Name = "picOn";
			this.picOn.Size = new System.Drawing.Size(18, 18);
			this.picOn.TabIndex = 121;
			this.picOn.TabStop = false;
			this.picOn.Visible = false;
			// 
			// picOff
			// 
			this.picOff.Image = ((System.Drawing.Image)(resources.GetObject("picOff.Image")));
			this.picOff.Location = new System.Drawing.Point(33, 342);
			this.picOff.Name = "picOff";
			this.picOff.Size = new System.Drawing.Size(18, 18);
			this.picOff.TabIndex = 123;
			this.picOff.TabStop = false;
			this.picOff.Visible = false;
			// 
			// dlgZebraPrinter
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(568, 374);
			this.Controls.Add(this.picOff);
			this.Controls.Add(this.picOn);
			this.Controls.Add(this.fraHostID);
			this.Controls.Add(this.fraHostStatus);
			this.Controls.Add(this.fraHostMemoryStatus);
			this.Controls.Add(this.btnClose);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgZebraPrinter";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Zebra Printer";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraHostID.ResumeLayout(false);
			this.fraHostStatus.ResumeLayout(false);
			this.fraSettings.ResumeLayout(false);
			this.fraComm.ResumeLayout(false);
			this.fraHostMemoryStatus.ResumeLayout(false);
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
				
				//Load printer configuration
				this.lblModel.Text = this.mPrinter.Model;
				this.lblVersion.Text = this.mPrinter.Version;
				this.lblResolution.Text = this.mPrinter.Resolution;
				this.lblMemory.Text = this.mPrinter.Memory;
				this.lblX.Text = this.mPrinter.X;
				
				this.lblRAMTotal.Text = this.mPrinter.RAMTotal + "kB";
				this.lblRAMMax.Text = this.mPrinter.RAMMax + "kB";
				this.lblRAMAvailable.Text = this.mPrinter.RAMAvailable + "kB";
				
				this.picPaperOut.Image = (this.mPrinter.PaperOut=="1") ? this.picOn.Image : this.picOff.Image;
				this.picPauseOn.Image = (this.mPrinter.Pause=="1") ? this.picOn.Image : this.picOff.Image;
				this.lblBufferCount.Text = this.mPrinter.FormatsInReceiveBuffer;
				this.picBufferFull.Image = (this.mPrinter.BufferFull=="1") ? this.picOn.Image : this.picOff.Image;
				this.picPartialFormat.Image = (this.mPrinter.PartialFormat=="1") ? this.picOn.Image : this.picOff.Image;
				this.picCorruptRAM.Image = (this.mPrinter.CorruptRAM=="1") ? this.picOn.Image : this.picOff.Image;
				this.picUnderTemp.Image = (this.mPrinter.UnderTemp=="1") ? this.picOn.Image : this.picOff.Image;
				this.picOverTemp.Image = (this.mPrinter.OverTemp=="1") ? this.picOn.Image : this.picOff.Image;
				this.picHeadUp.Image = (this.mPrinter.HeadUp=="1") ? this.picOn.Image : this.picOff.Image;
				this.picRibbonOut.Image = (this.mPrinter.RibbonOut=="1") ? this.picOn.Image : this.picOff.Image;
				this.picLabelWaiting.Image = (this.mPrinter.LabelWaiting=="1") ? this.picOn.Image : this.picOff.Image;
				this.lblPrintMode.Text = this.mPrinter.PrintMode;
				
				this.lblBaud.Text = this.mPrinter.BaudRate.ToString();
				this.lblDataBits.Text = this.mPrinter.DataBits.ToString();
				this.lblParity.Text = this.mPrinter.Parity.ToString();
				this.lblStopBits.Text = this.mPrinter.StopBits.ToString();
				this.lblHandshake.Text = this.mPrinter.Handshake.ToString();
				//this.lblEnable.Text = this.mPrinter.Enable;
				
				this.lblMediaType.Text = this.mPrinter.MediaType;
				this.lblSensorProfile.Text = this.mPrinter.SensorProfile;
				this.lblPrintMode_.Text = this.mPrinter.PrintMode_;
			} 
			catch(Exception ex) { throw ex; }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnCommandClick(object sender, System.EventArgs e) { this.Close(); }
	}
}
