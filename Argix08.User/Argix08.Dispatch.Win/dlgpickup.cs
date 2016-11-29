//	File:	dlgpickup.cs
//	Author:	J. Heary
//	Date:	09/20/05
//	Desc:	Dialog to view/edit pickups on the Pickup Log.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Argix.Data;

namespace Argix.Dispatch {
	//
	public class dlgPickup : Argix.Dispatch.dlgSchedule {
		//Members
		//Constants
		//Events
		#region Controls

		private System.Windows.Forms.TextBox txtCaller;
		private System.Windows.Forms.TextBox txtCreatedBy;
		private System.Windows.Forms.Label _lblCaller;
		private System.Windows.Forms.Label _lblCreatedBy;
		private System.Windows.Forms.Label _lblTitle;
		private System.Windows.Forms.GroupBox grpPickupInfo;
		private System.Windows.Forms.Label _lblShipper;
		private System.Windows.Forms.TextBox txtShipper;
		private System.Windows.Forms.Label _lblShipperAddress;
		private System.Windows.Forms.TextBox txtShipperAddress;
		private System.Windows.Forms.Label _lblDeliveryWindow;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.Label _lblDriver;
		private System.Windows.Forms.TextBox txtDriver;
		private System.Windows.Forms.Label _lblAutoNumber;
		private System.Windows.Forms.TextBox txtAutoNumber;
		private System.Windows.Forms.DateTimePicker dtpPickedup;
		private System.Windows.Forms.DateTimePicker dtpRequested;
		private System.Windows.Forms.Label _lblRequested;
		private System.Windows.Forms.Label _lblPickedUp;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.GroupBox grpLine2;
		private System.Windows.Forms.CheckBox chkReady;
		private System.Windows.Forms.GroupBox grpLine1;
		private System.Windows.Forms.Label _lblAmount;
		private System.Windows.Forms.TextBox txtAmount;
		private System.Windows.Forms.ComboBox cboAmountType;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.ComboBox cboTerminal;
		private System.Windows.Forms.CheckBox chkUpdated;
		private System.Windows.Forms.Label _lblClient;
		private System.Windows.Forms.TextBox txtClient;
		private System.Windows.Forms.TextBox txtDeliveryWindow;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label _lblCreatedDate;
		private System.Windows.Forms.DateTimePicker dtpCreatedDate;
		private System.ComponentModel.IContainer components = null;
		#endregion

		public dlgPickup(Pickup entry, Mediator mediator): base(entry, mediator) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) {
			//Clean up any resources being used
			if( disposing ) {
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.txtCaller = new System.Windows.Forms.TextBox();
			this.txtCreatedBy = new System.Windows.Forms.TextBox();
			this.txtClient = new System.Windows.Forms.TextBox();
			this._lblCaller = new System.Windows.Forms.Label();
			this._lblCreatedDate = new System.Windows.Forms.Label();
			this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
			this._lblCreatedBy = new System.Windows.Forms.Label();
			this._lblTitle = new System.Windows.Forms.Label();
			this.grpPickupInfo = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this._lblShipper = new System.Windows.Forms.Label();
			this.txtShipper = new System.Windows.Forms.TextBox();
			this._lblShipperAddress = new System.Windows.Forms.Label();
			this.txtShipperAddress = new System.Windows.Forms.TextBox();
			this._lblDeliveryWindow = new System.Windows.Forms.Label();
			this.txtDeliveryWindow = new System.Windows.Forms.TextBox();
			this._lblTerminal = new System.Windows.Forms.Label();
			this._lblDriver = new System.Windows.Forms.Label();
			this.txtDriver = new System.Windows.Forms.TextBox();
			this._lblAutoNumber = new System.Windows.Forms.Label();
			this.txtAutoNumber = new System.Windows.Forms.TextBox();
			this.dtpPickedup = new System.Windows.Forms.DateTimePicker();
			this.dtpRequested = new System.Windows.Forms.DateTimePicker();
			this._lblRequested = new System.Windows.Forms.Label();
			this._lblPickedUp = new System.Windows.Forms.Label();
			this._lblComments = new System.Windows.Forms.Label();
			this.grpLine2 = new System.Windows.Forms.GroupBox();
			this.chkReady = new System.Windows.Forms.CheckBox();
			this.grpLine1 = new System.Windows.Forms.GroupBox();
			this._lblAmount = new System.Windows.Forms.Label();
			this.txtAmount = new System.Windows.Forms.TextBox();
			this.cboAmountType = new System.Windows.Forms.ComboBox();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.cboTerminal = new System.Windows.Forms.ComboBox();
			this.chkUpdated = new System.Windows.Forms.CheckBox();
			this._lblClient = new System.Windows.Forms.Label();
			this.grpPickupInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// mToolTip
			// 
			this.mToolTip.AutoPopDelay = 3000;
			this.mToolTip.InitialDelay = 500;
			this.mToolTip.ReshowDelay = 1000;
			this.mToolTip.ShowAlways = true;
			// 
			// btnOK
			// 
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 4;
			// 
			// btnCancel
			// 
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 0;
			// 
			// txtCaller
			// 
			this.txtCaller.Location = new System.Drawing.Point(453, 27);
			this.txtCaller.Name = "txtCaller";
			this.txtCaller.Size = new System.Drawing.Size(192, 21);
			this.txtCaller.TabIndex = 1;
			this.txtCaller.Text = "";
			this.txtCaller.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtCreatedBy
			// 
			this.txtCreatedBy.Location = new System.Drawing.Point(135, 60);
			this.txtCreatedBy.Name = "txtCreatedBy";
			this.txtCreatedBy.Size = new System.Drawing.Size(192, 21);
			this.txtCreatedBy.TabIndex = 1;
			this.txtCreatedBy.Text = "";
			this.txtCreatedBy.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtClient
			// 
			this.txtClient.Location = new System.Drawing.Point(129, 27);
			this.txtClient.Name = "txtClient";
			this.txtClient.Size = new System.Drawing.Size(192, 21);
			this.txtClient.TabIndex = 0;
			this.txtClient.Text = "";
			this.txtClient.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblCaller
			// 
			this._lblCaller.Location = new System.Drawing.Point(351, 27);
			this._lblCaller.Name = "_lblCaller";
			this._lblCaller.Size = new System.Drawing.Size(90, 18);
			this._lblCaller.TabIndex = 138;
			this._lblCaller.Text = "Caller:";
			this._lblCaller.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblCreatedDate
			// 
			this._lblCreatedDate.Location = new System.Drawing.Point(354, 60);
			this._lblCreatedDate.Name = "_lblCreatedDate";
			this._lblCreatedDate.Size = new System.Drawing.Size(96, 18);
			this._lblCreatedDate.TabIndex = 137;
			this._lblCreatedDate.Text = "Created: ";
			this._lblCreatedDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpCreatedDate
			// 
			this.dtpCreatedDate.CustomFormat = "MMM dd, yyyy   hh:mm tt";
			this.dtpCreatedDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpCreatedDate.Location = new System.Drawing.Point(459, 60);
			this.dtpCreatedDate.Name = "dtpCreatedDate";
			this.dtpCreatedDate.ShowUpDown = true;
			this.dtpCreatedDate.Size = new System.Drawing.Size(192, 21);
			this.dtpCreatedDate.TabIndex = 2;
			this.dtpCreatedDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblCreatedBy
			// 
			this._lblCreatedBy.Location = new System.Drawing.Point(30, 60);
			this._lblCreatedBy.Name = "_lblCreatedBy";
			this._lblCreatedBy.Size = new System.Drawing.Size(96, 18);
			this._lblCreatedBy.TabIndex = 134;
			this._lblCreatedBy.Text = "Created By: ";
			this._lblCreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblTitle
			// 
			this._lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTitle.Location = new System.Drawing.Point(236, 6);
			this._lblTitle.Name = "_lblTitle";
			this._lblTitle.Size = new System.Drawing.Size(192, 32);
			this._lblTitle.TabIndex = 133;
			this._lblTitle.Text = "Pick Up Log";
			this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpPickupInfo
			// 
			this.grpPickupInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpPickupInfo.Controls.Add(this.groupBox1);
			this.grpPickupInfo.Controls.Add(this._lblShipper);
			this.grpPickupInfo.Controls.Add(this.txtShipper);
			this.grpPickupInfo.Controls.Add(this._lblShipperAddress);
			this.grpPickupInfo.Controls.Add(this.txtShipperAddress);
			this.grpPickupInfo.Controls.Add(this._lblDeliveryWindow);
			this.grpPickupInfo.Controls.Add(this.txtDeliveryWindow);
			this.grpPickupInfo.Controls.Add(this._lblTerminal);
			this.grpPickupInfo.Controls.Add(this._lblDriver);
			this.grpPickupInfo.Controls.Add(this.txtDriver);
			this.grpPickupInfo.Controls.Add(this._lblAutoNumber);
			this.grpPickupInfo.Controls.Add(this.txtAutoNumber);
			this.grpPickupInfo.Controls.Add(this.dtpPickedup);
			this.grpPickupInfo.Controls.Add(this.dtpRequested);
			this.grpPickupInfo.Controls.Add(this._lblRequested);
			this.grpPickupInfo.Controls.Add(this._lblPickedUp);
			this.grpPickupInfo.Controls.Add(this._lblComments);
			this.grpPickupInfo.Controls.Add(this.grpLine2);
			this.grpPickupInfo.Controls.Add(this.chkReady);
			this.grpPickupInfo.Controls.Add(this.grpLine1);
			this.grpPickupInfo.Controls.Add(this._lblAmount);
			this.grpPickupInfo.Controls.Add(this.txtAmount);
			this.grpPickupInfo.Controls.Add(this.cboAmountType);
			this.grpPickupInfo.Controls.Add(this.txtComments);
			this.grpPickupInfo.Controls.Add(this.cboTerminal);
			this.grpPickupInfo.Controls.Add(this.chkUpdated);
			this.grpPickupInfo.Controls.Add(this._lblClient);
			this.grpPickupInfo.Controls.Add(this.txtCaller);
			this.grpPickupInfo.Controls.Add(this.txtClient);
			this.grpPickupInfo.Controls.Add(this._lblCaller);
			this.grpPickupInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpPickupInfo.Location = new System.Drawing.Point(5, 96);
			this.grpPickupInfo.Name = "grpPickupInfo";
			this.grpPickupInfo.Size = new System.Drawing.Size(654, 312);
			this.grpPickupInfo.TabIndex = 3;
			this.grpPickupInfo.TabStop = false;
			this.grpPickupInfo.Text = "Pick-Up Information";
			// 
			// groupBox1
			// 
			this.groupBox1.Location = new System.Drawing.Point(6, 213);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(639, 9);
			this.groupBox1.TabIndex = 145;
			this.groupBox1.TabStop = false;
			// 
			// _lblShipper
			// 
			this._lblShipper.Location = new System.Drawing.Point(24, 78);
			this._lblShipper.Name = "_lblShipper";
			this._lblShipper.Size = new System.Drawing.Size(96, 18);
			this._lblShipper.TabIndex = 144;
			this._lblShipper.Text = "Shipper Name:";
			this._lblShipper.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtShipper
			// 
			this.txtShipper.Location = new System.Drawing.Point(129, 78);
			this.txtShipper.Name = "txtShipper";
			this.txtShipper.Size = new System.Drawing.Size(195, 21);
			this.txtShipper.TabIndex = 2;
			this.txtShipper.Text = "";
			this.txtShipper.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblShipperAddress
			// 
			this._lblShipperAddress.Location = new System.Drawing.Point(15, 108);
			this._lblShipperAddress.Name = "_lblShipperAddress";
			this._lblShipperAddress.Size = new System.Drawing.Size(105, 18);
			this._lblShipperAddress.TabIndex = 142;
			this._lblShipperAddress.Text = "Shipper Address: ";
			this._lblShipperAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtShipperAddress
			// 
			this.txtShipperAddress.Location = new System.Drawing.Point(129, 108);
			this.txtShipperAddress.Multiline = true;
			this.txtShipperAddress.Name = "txtShipperAddress";
			this.txtShipperAddress.Size = new System.Drawing.Size(195, 51);
			this.txtShipperAddress.TabIndex = 3;
			this.txtShipperAddress.Text = "";
			this.txtShipperAddress.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblDeliveryWindow
			// 
			this._lblDeliveryWindow.Location = new System.Drawing.Point(336, 78);
			this._lblDeliveryWindow.Name = "_lblDeliveryWindow";
			this._lblDeliveryWindow.Size = new System.Drawing.Size(108, 18);
			this._lblDeliveryWindow.TabIndex = 140;
			this._lblDeliveryWindow.Text = "Delivery Window: ";
			this._lblDeliveryWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDeliveryWindow
			// 
			this.txtDeliveryWindow.Location = new System.Drawing.Point(453, 78);
			this.txtDeliveryWindow.Name = "txtDeliveryWindow";
			this.txtDeliveryWindow.Size = new System.Drawing.Size(192, 21);
			this.txtDeliveryWindow.TabIndex = 4;
			this.txtDeliveryWindow.Text = "";
			this.txtDeliveryWindow.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblTerminal
			// 
			this._lblTerminal.Location = new System.Drawing.Point(336, 108);
			this._lblTerminal.Name = "_lblTerminal";
			this._lblTerminal.Size = new System.Drawing.Size(108, 18);
			this._lblTerminal.TabIndex = 138;
			this._lblTerminal.Text = "Terminal for P/U: ";
			this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblDriver
			// 
			this._lblDriver.Location = new System.Drawing.Point(372, 138);
			this._lblDriver.Name = "_lblDriver";
			this._lblDriver.Size = new System.Drawing.Size(72, 18);
			this._lblDriver.TabIndex = 137;
			this._lblDriver.Text = "Driver: ";
			this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDriver
			// 
			this.txtDriver.Location = new System.Drawing.Point(453, 138);
			this.txtDriver.Name = "txtDriver";
			this.txtDriver.Size = new System.Drawing.Size(192, 21);
			this.txtDriver.TabIndex = 6;
			this.txtDriver.Text = "";
			this.txtDriver.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblAutoNumber
			// 
			this._lblAutoNumber.Location = new System.Drawing.Point(348, 186);
			this._lblAutoNumber.Name = "_lblAutoNumber";
			this._lblAutoNumber.Size = new System.Drawing.Size(96, 18);
			this._lblAutoNumber.TabIndex = 135;
			this._lblAutoNumber.Text = "Auto Number: ";
			this._lblAutoNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtAutoNumber
			// 
			this.txtAutoNumber.Location = new System.Drawing.Point(453, 186);
			this.txtAutoNumber.Name = "txtAutoNumber";
			this.txtAutoNumber.Size = new System.Drawing.Size(96, 21);
			this.txtAutoNumber.TabIndex = 9;
			this.txtAutoNumber.Text = "";
			this.txtAutoNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dtpPickedup
			// 
			this.dtpPickedup.CustomFormat = "MM/dd/yyyy   hh:mm tt";
			this.dtpPickedup.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpPickedup.Location = new System.Drawing.Point(129, 258);
			this.dtpPickedup.Name = "dtpPickedup";
			this.dtpPickedup.ShowCheckBox = true;
			this.dtpPickedup.Size = new System.Drawing.Size(108, 21);
			this.dtpPickedup.TabIndex = 11;
			this.dtpPickedup.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dtpRequested
			// 
			this.dtpRequested.CustomFormat = "MM/dd/yyyy   hh:mm tt";
			this.dtpRequested.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtpRequested.Location = new System.Drawing.Point(129, 231);
			this.dtpRequested.Name = "dtpRequested";
			this.dtpRequested.Size = new System.Drawing.Size(108, 21);
			this.dtpRequested.TabIndex = 10;
			this.dtpRequested.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblRequested
			// 
			this._lblRequested.Location = new System.Drawing.Point(6, 231);
			this._lblRequested.Name = "_lblRequested";
			this._lblRequested.Size = new System.Drawing.Size(114, 18);
			this._lblRequested.TabIndex = 131;
			this._lblRequested.Text = "Date Requested: ";
			this._lblRequested.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblPickedUp
			// 
			this._lblPickedUp.Location = new System.Drawing.Point(6, 258);
			this._lblPickedUp.Name = "_lblPickedUp";
			this._lblPickedUp.Size = new System.Drawing.Size(114, 18);
			this._lblPickedUp.TabIndex = 130;
			this._lblPickedUp.Text = "Date Pickup Made: ";
			this._lblPickedUp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblComments
			// 
			this._lblComments.Location = new System.Drawing.Point(255, 231);
			this._lblComments.Name = "_lblComments";
			this._lblComments.Size = new System.Drawing.Size(72, 18);
			this._lblComments.TabIndex = 129;
			this._lblComments.Text = "Comments: ";
			this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grpLine2
			// 
			this.grpLine2.Location = new System.Drawing.Point(6, 165);
			this.grpLine2.Name = "grpLine2";
			this.grpLine2.Size = new System.Drawing.Size(639, 9);
			this.grpLine2.TabIndex = 128;
			this.grpLine2.TabStop = false;
			// 
			// chkReady
			// 
			this.chkReady.Location = new System.Drawing.Point(336, 285);
			this.chkReady.Name = "chkReady";
			this.chkReady.Size = new System.Drawing.Size(309, 18);
			this.chkReady.TabIndex = 14;
			this.chkReady.Text = "All pickups Must be ready when called in.";
			this.chkReady.CheckStateChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// grpLine1
			// 
			this.grpLine1.Location = new System.Drawing.Point(6, 57);
			this.grpLine1.Name = "grpLine1";
			this.grpLine1.Size = new System.Drawing.Size(639, 9);
			this.grpLine1.TabIndex = 126;
			this.grpLine1.TabStop = false;
			// 
			// _lblAmount
			// 
			this._lblAmount.Location = new System.Drawing.Point(48, 189);
			this._lblAmount.Name = "_lblAmount";
			this._lblAmount.Size = new System.Drawing.Size(72, 18);
			this._lblAmount.TabIndex = 125;
			this._lblAmount.Text = "Amount: ";
			this._lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtAmount
			// 
			this.txtAmount.Location = new System.Drawing.Point(129, 186);
			this.txtAmount.Name = "txtAmount";
			this.txtAmount.Size = new System.Drawing.Size(96, 21);
			this.txtAmount.TabIndex = 7;
			this.txtAmount.Text = "";
			this.txtAmount.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// cboAmountType
			// 
			this.cboAmountType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboAmountType.Location = new System.Drawing.Point(231, 186);
			this.cboAmountType.MaxDropDownItems = 5;
			this.cboAmountType.Name = "cboAmountType";
			this.cboAmountType.Size = new System.Drawing.Size(96, 21);
			this.cboAmountType.TabIndex = 8;
			this.cboAmountType.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(336, 231);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(309, 45);
			this.txtComments.TabIndex = 12;
			this.txtComments.Text = "";
			this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// cboTerminal
			// 
			this.cboTerminal.Location = new System.Drawing.Point(453, 108);
			this.cboTerminal.Name = "cboTerminal";
			this.cboTerminal.Size = new System.Drawing.Size(192, 21);
			this.cboTerminal.TabIndex = 5;
			this.cboTerminal.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// chkUpdated
			// 
			this.chkUpdated.Location = new System.Drawing.Point(129, 285);
			this.chkUpdated.Name = "chkUpdated";
			this.chkUpdated.Size = new System.Drawing.Size(96, 18);
			this.chkUpdated.TabIndex = 13;
			this.chkUpdated.Text = "Updated?";
			this.chkUpdated.CheckStateChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblClient
			// 
			this._lblClient.Location = new System.Drawing.Point(21, 27);
			this._lblClient.Name = "_lblClient";
			this._lblClient.Size = new System.Drawing.Size(96, 18);
			this._lblClient.TabIndex = 130;
			this._lblClient.Text = "Client: ";
			this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dlgPickup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 446);
			this.Controls.Add(this.txtCreatedBy);
			this.Controls.Add(this._lblCreatedDate);
			this.Controls.Add(this.dtpCreatedDate);
			this.Controls.Add(this._lblCreatedBy);
			this.Controls.Add(this._lblTitle);
			this.Controls.Add(this.grpPickupInfo);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Name = "dlgPickup";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.grpPickupInfo, 0);
			this.Controls.SetChildIndex(this._lblTitle, 0);
			this.Controls.SetChildIndex(this._lblCreatedBy, 0);
			this.Controls.SetChildIndex(this.dtpCreatedDate, 0);
			this.Controls.SetChildIndex(this._lblCreatedDate, 0);
			this.Controls.SetChildIndex(this.txtCreatedBy, 0);
			this.grpPickupInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load selection lists
				try {
					base.LoadSelections(base.mEntry.EntryType, this.cboTerminal);
					base.LoadSelections(base.mEntry.EntryType, this.cboAmountType);
				} catch(Exception) { }
				
				//Load controls
				this.Text = base.mEntry.EntryType + "(" + base.mEntry.EntryID.ToString() + ")";
				Pickup pickup = (Pickup)base.mEntry;
				this.txtCreatedBy.Enabled = this.dtpCreatedDate.Enabled = false;
				this.txtCreatedBy.Text = pickup.CreatedBy;
				this.dtpCreatedDate.Value = pickup.Created;
				this.txtClient.Text = pickup.ClientName;
				this.txtCaller.Text = pickup.CallerName;
				this.txtShipper.Text = pickup.ShipperName;
				this.txtShipperAddress.Text = pickup.ShipperAddress;
				this.txtDeliveryWindow.Text = pickup.DeliveryWindow;
				this.cboTerminal.Text = pickup.Terminal;
				this.txtDriver.Text = pickup.DriverName;
				this.txtAmount.Text = pickup.Amount.ToString();
				this.cboAmountType.Text = pickup.AmountType;
				this.txtAutoNumber.Text = pickup.AutoNumber.ToString();
				this.dtpRequested.Value = pickup.RequestDate;
				if(pickup.PickUpDate.CompareTo(DateTime.MinValue) > 0) this.dtpPickedup.Value = pickup.PickUpDate;
				this.chkUpdated.Checked = pickup.Updated;
				this.chkReady.Checked = pickup.MustBeReady;
				this.txtComments.Text = pickup.Comments;
			} 
			catch(Exception ex) { base.reportError(ex); }
			finally { 
				OnValidateForm(null, null);
				base.btnOK.Enabled = false;
				this.Cursor = Cursors.Default; 
			}
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing event
			try {
				//Save selection lists
				try {
					base.SaveSelections(base.mEntry.EntryType, this.cboTerminal);
					base.SaveSelections(base.mEntry.EntryType, this.cboAmountType);
				} catch(Exception) { }
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
		protected override void OnValidateForm(object sender, EventArgs e) {
			//Set user services
			try {
				//Set menu/context menu states
				base.btnOK.Enabled = (this.txtCreatedBy.Text!="" && this.dtpCreatedDate.Value>DateTime.MinValue);
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
		protected override void UpdateEntry() { 
			//Update this entry
			try {
				//
				Pickup pickup = (Pickup)base.mEntry;
				pickup.CreatedBy = this.txtCreatedBy.Text;
				pickup.Created = this.dtpCreatedDate.Value;
				pickup.ClientName = this.txtClient.Text;
				pickup.CallerName = this.txtCaller.Text;
				pickup.ShipperName = this.txtShipper.Text;
				pickup.ShipperAddress = this.txtShipperAddress.Text;
				pickup.DeliveryWindow = this.txtDeliveryWindow.Text;
				pickup.Terminal = this.cboTerminal.Text;
				pickup.DriverName = this.txtDriver.Text;
				pickup.Amount = Convert.ToInt32(this.txtAmount.Text);
				pickup.AmountType = this.cboAmountType.Text;
				pickup.AutoNumber = Convert.ToInt32(this.txtAutoNumber.Text);
				pickup.RequestDate = this.dtpRequested.Value;
				pickup.PickUpDate = (this.dtpPickedup.Checked) ? this.dtpPickedup.Value : DateTime.MinValue;
				pickup.Updated = this.chkUpdated.Checked;
				pickup.MustBeReady = this.chkReady.Checked;
				pickup.Comments = this.txtComments.Text;
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
	}
}

