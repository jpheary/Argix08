//	File:	dlgClientInboundFreight.cs
//	Author:	J. Heary
//	Date:	09/29/05
//	Desc:	Dialog to view/edit freight on the Client Inbound Schedule.
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
	public class dlgClientInboundFreight : Argix.Dispatch.dlgSchedule {
		//Members
		//Constants
		//Events
		#region Controls
		private System.Windows.Forms.Label _lblTitle;
		private System.Windows.Forms.GroupBox grpFreightInfo;
		private System.Windows.Forms.Label _lblOf;
		private System.Windows.Forms.ComboBox cboFreightType;
		private System.Windows.Forms.Label _lblVendor;
		private System.Windows.Forms.TextBox txtVendor;
		private System.Windows.Forms.Label _lblDriver;
		private System.Windows.Forms.TextBox txtDriver;
		private System.Windows.Forms.DateTimePicker dtpETA;
		private System.Windows.Forms.Label _lblETA;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.GroupBox grpLine2;
		private System.Windows.Forms.GroupBox grpLine1;
		private System.Windows.Forms.Label _lblAmount;
		private System.Windows.Forms.TextBox txtAmount;
		private System.Windows.Forms.ComboBox cboAmountType;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.CheckBox chkIn;
		private System.Windows.Forms.TextBox txtTrailerNumber;
		private System.Windows.Forms.Label _lblTrailerNumber;
		private System.Windows.Forms.Label _lblConsignee;
		private System.Windows.Forms.TextBox txtConsignee;
		private System.Windows.Forms.TextBox txtCreatedBy;
		private System.Windows.Forms.Label _lblCreatedBy;
		private System.Windows.Forms.Label _lblCreatedDate;
		private System.Windows.Forms.DateTimePicker dtpCreatedDate;
		private System.ComponentModel.IContainer components = null;
		#endregion

		public dlgClientInboundFreight(ClientInboundFreight entry, Mediator mediator): base(entry, mediator) {
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
			this._lblTitle = new System.Windows.Forms.Label();
			this.grpFreightInfo = new System.Windows.Forms.GroupBox();
			this._lblOf = new System.Windows.Forms.Label();
			this.cboFreightType = new System.Windows.Forms.ComboBox();
			this._lblVendor = new System.Windows.Forms.Label();
			this.txtVendor = new System.Windows.Forms.TextBox();
			this._lblDriver = new System.Windows.Forms.Label();
			this.txtDriver = new System.Windows.Forms.TextBox();
			this.dtpETA = new System.Windows.Forms.DateTimePicker();
			this._lblETA = new System.Windows.Forms.Label();
			this._lblComments = new System.Windows.Forms.Label();
			this.grpLine2 = new System.Windows.Forms.GroupBox();
			this.grpLine1 = new System.Windows.Forms.GroupBox();
			this._lblAmount = new System.Windows.Forms.Label();
			this.txtAmount = new System.Windows.Forms.TextBox();
			this.cboAmountType = new System.Windows.Forms.ComboBox();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.chkIn = new System.Windows.Forms.CheckBox();
			this.txtTrailerNumber = new System.Windows.Forms.TextBox();
			this._lblTrailerNumber = new System.Windows.Forms.Label();
			this._lblConsignee = new System.Windows.Forms.Label();
			this.txtConsignee = new System.Windows.Forms.TextBox();
			this.txtCreatedBy = new System.Windows.Forms.TextBox();
			this._lblCreatedBy = new System.Windows.Forms.Label();
			this._lblCreatedDate = new System.Windows.Forms.Label();
			this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
			this.grpFreightInfo.SuspendLayout();
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
			// _lblTitle
			// 
			this._lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTitle.Location = new System.Drawing.Point(185, 6);
			this._lblTitle.Name = "_lblTitle";
			this._lblTitle.Size = new System.Drawing.Size(294, 32);
			this._lblTitle.TabIndex = 123;
			this._lblTitle.Text = "Client Inbound Freight";
			this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpFreightInfo
			// 
			this.grpFreightInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpFreightInfo.Controls.Add(this._lblOf);
			this.grpFreightInfo.Controls.Add(this.cboFreightType);
			this.grpFreightInfo.Controls.Add(this._lblVendor);
			this.grpFreightInfo.Controls.Add(this.txtVendor);
			this.grpFreightInfo.Controls.Add(this._lblDriver);
			this.grpFreightInfo.Controls.Add(this.txtDriver);
			this.grpFreightInfo.Controls.Add(this.dtpETA);
			this.grpFreightInfo.Controls.Add(this._lblETA);
			this.grpFreightInfo.Controls.Add(this._lblComments);
			this.grpFreightInfo.Controls.Add(this.grpLine2);
			this.grpFreightInfo.Controls.Add(this.grpLine1);
			this.grpFreightInfo.Controls.Add(this._lblAmount);
			this.grpFreightInfo.Controls.Add(this.txtAmount);
			this.grpFreightInfo.Controls.Add(this.cboAmountType);
			this.grpFreightInfo.Controls.Add(this.txtComments);
			this.grpFreightInfo.Controls.Add(this.chkIn);
			this.grpFreightInfo.Controls.Add(this.txtTrailerNumber);
			this.grpFreightInfo.Controls.Add(this._lblTrailerNumber);
			this.grpFreightInfo.Controls.Add(this._lblConsignee);
			this.grpFreightInfo.Controls.Add(this.txtConsignee);
			this.grpFreightInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpFreightInfo.Location = new System.Drawing.Point(5, 96);
			this.grpFreightInfo.Name = "grpFreightInfo";
			this.grpFreightInfo.Size = new System.Drawing.Size(654, 309);
			this.grpFreightInfo.TabIndex = 3;
			this.grpFreightInfo.TabStop = false;
			this.grpFreightInfo.Text = "Freight Information";
			// 
			// _lblOf
			// 
			this._lblOf.Location = new System.Drawing.Point(330, 144);
			this._lblOf.Name = "_lblOf";
			this._lblOf.Size = new System.Drawing.Size(18, 18);
			this._lblOf.TabIndex = 146;
			this._lblOf.Text = "of";
			this._lblOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cboFreightType
			// 
			this.cboFreightType.Location = new System.Drawing.Point(357, 141);
			this.cboFreightType.MaxDropDownItems = 5;
			this.cboFreightType.Name = "cboFreightType";
			this.cboFreightType.Size = new System.Drawing.Size(96, 21);
			this.cboFreightType.TabIndex = 7;
			this.cboFreightType.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblVendor
			// 
			this._lblVendor.Location = new System.Drawing.Point(24, 27);
			this._lblVendor.Name = "_lblVendor";
			this._lblVendor.Size = new System.Drawing.Size(96, 18);
			this._lblVendor.TabIndex = 142;
			this._lblVendor.Text = "Vendor: ";
			this._lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtVendor
			// 
			this.txtVendor.Location = new System.Drawing.Point(129, 27);
			this.txtVendor.Multiline = true;
			this.txtVendor.Name = "txtVendor";
			this.txtVendor.Size = new System.Drawing.Size(192, 51);
			this.txtVendor.TabIndex = 0;
			this.txtVendor.Text = "";
			this.txtVendor.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblDriver
			// 
			this._lblDriver.Location = new System.Drawing.Point(24, 87);
			this._lblDriver.Name = "_lblDriver";
			this._lblDriver.Size = new System.Drawing.Size(96, 18);
			this._lblDriver.TabIndex = 137;
			this._lblDriver.Text = "Driver: ";
			this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDriver
			// 
			this.txtDriver.Location = new System.Drawing.Point(129, 87);
			this.txtDriver.Name = "txtDriver";
			this.txtDriver.Size = new System.Drawing.Size(192, 21);
			this.txtDriver.TabIndex = 1;
			this.txtDriver.Text = "";
			this.txtDriver.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dtpETA
			// 
			this.dtpETA.Checked = false;
			this.dtpETA.CustomFormat = "MMM dd, yyyy   hh:mm tt";
			this.dtpETA.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpETA.Location = new System.Drawing.Point(453, 57);
			this.dtpETA.Name = "dtpETA";
			this.dtpETA.ShowCheckBox = true;
			this.dtpETA.ShowUpDown = true;
			this.dtpETA.Size = new System.Drawing.Size(192, 21);
			this.dtpETA.TabIndex = 3;
			this.dtpETA.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblETA
			// 
			this._lblETA.Location = new System.Drawing.Point(354, 57);
			this._lblETA.Name = "_lblETA";
			this._lblETA.Size = new System.Drawing.Size(90, 18);
			this._lblETA.TabIndex = 131;
			this._lblETA.Text = "ETA Time: ";
			this._lblETA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblComments
			// 
			this._lblComments.Location = new System.Drawing.Point(24, 195);
			this._lblComments.Name = "_lblComments";
			this._lblComments.Size = new System.Drawing.Size(96, 18);
			this._lblComments.TabIndex = 129;
			this._lblComments.Text = "Comments: ";
			this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grpLine2
			// 
			this.grpLine2.Location = new System.Drawing.Point(6, 171);
			this.grpLine2.Name = "grpLine2";
			this.grpLine2.Size = new System.Drawing.Size(639, 9);
			this.grpLine2.TabIndex = 128;
			this.grpLine2.TabStop = false;
			// 
			// grpLine1
			// 
			this.grpLine1.Location = new System.Drawing.Point(6, 117);
			this.grpLine1.Name = "grpLine1";
			this.grpLine1.Size = new System.Drawing.Size(639, 9);
			this.grpLine1.TabIndex = 126;
			this.grpLine1.TabStop = false;
			// 
			// _lblAmount
			// 
			this._lblAmount.Location = new System.Drawing.Point(24, 144);
			this._lblAmount.Name = "_lblAmount";
			this._lblAmount.Size = new System.Drawing.Size(96, 18);
			this._lblAmount.TabIndex = 125;
			this._lblAmount.Text = "Amount: ";
			this._lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtAmount
			// 
			this.txtAmount.Location = new System.Drawing.Point(129, 141);
			this.txtAmount.Name = "txtAmount";
			this.txtAmount.Size = new System.Drawing.Size(72, 21);
			this.txtAmount.TabIndex = 5;
			this.txtAmount.Text = "";
			this.txtAmount.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// cboAmountType
			// 
			this.cboAmountType.Location = new System.Drawing.Point(207, 141);
			this.cboAmountType.MaxDropDownItems = 5;
			this.cboAmountType.Name = "cboAmountType";
			this.cboAmountType.Size = new System.Drawing.Size(114, 21);
			this.cboAmountType.TabIndex = 6;
			this.cboAmountType.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(129, 195);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(516, 51);
			this.txtComments.TabIndex = 8;
			this.txtComments.Text = "";
			this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// chkIn
			// 
			this.chkIn.Location = new System.Drawing.Point(129, 258);
			this.chkIn.Name = "chkIn";
			this.chkIn.Size = new System.Drawing.Size(96, 18);
			this.chkIn.TabIndex = 9;
			this.chkIn.Text = "In?";
			this.chkIn.CheckStateChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtTrailerNumber
			// 
			this.txtTrailerNumber.Location = new System.Drawing.Point(453, 87);
			this.txtTrailerNumber.Name = "txtTrailerNumber";
			this.txtTrailerNumber.Size = new System.Drawing.Size(144, 21);
			this.txtTrailerNumber.TabIndex = 4;
			this.txtTrailerNumber.Text = "";
			this.txtTrailerNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblTrailerNumber
			// 
			this._lblTrailerNumber.Location = new System.Drawing.Point(354, 87);
			this._lblTrailerNumber.Name = "_lblTrailerNumber";
			this._lblTrailerNumber.Size = new System.Drawing.Size(90, 18);
			this._lblTrailerNumber.TabIndex = 115;
			this._lblTrailerNumber.Text = "Trailer#: ";
			this._lblTrailerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblConsignee
			// 
			this._lblConsignee.Location = new System.Drawing.Point(354, 27);
			this._lblConsignee.Name = "_lblConsignee";
			this._lblConsignee.Size = new System.Drawing.Size(90, 18);
			this._lblConsignee.TabIndex = 126;
			this._lblConsignee.Text = "Consignee:";
			this._lblConsignee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtConsignee
			// 
			this.txtConsignee.Location = new System.Drawing.Point(453, 27);
			this.txtConsignee.Name = "txtConsignee";
			this.txtConsignee.Size = new System.Drawing.Size(192, 21);
			this.txtConsignee.TabIndex = 2;
			this.txtConsignee.Text = "";
			this.txtConsignee.TextChanged += new System.EventHandler(this.OnValidateForm);
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
			// _lblCreatedBy
			// 
			this._lblCreatedBy.Location = new System.Drawing.Point(30, 60);
			this._lblCreatedBy.Name = "_lblCreatedBy";
			this._lblCreatedBy.Size = new System.Drawing.Size(96, 18);
			this._lblCreatedBy.TabIndex = 122;
			this._lblCreatedBy.Text = "Created By: ";
			this._lblCreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblCreatedDate
			// 
			this._lblCreatedDate.Location = new System.Drawing.Point(354, 60);
			this._lblCreatedDate.Name = "_lblCreatedDate";
			this._lblCreatedDate.Size = new System.Drawing.Size(96, 18);
			this._lblCreatedDate.TabIndex = 125;
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
			// dlgClientInboundFreight
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 446);
			this.Controls.Add(this._lblTitle);
			this.Controls.Add(this.grpFreightInfo);
			this.Controls.Add(this._lblCreatedBy);
			this.Controls.Add(this.dtpCreatedDate);
			this.Controls.Add(this._lblCreatedDate);
			this.Controls.Add(this.txtCreatedBy);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Name = "dlgClientInboundFreight";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Controls.SetChildIndex(this.txtCreatedBy, 0);
			this.Controls.SetChildIndex(this._lblCreatedDate, 0);
			this.Controls.SetChildIndex(this.dtpCreatedDate, 0);
			this.Controls.SetChildIndex(this._lblCreatedBy, 0);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.grpFreightInfo, 0);
			this.Controls.SetChildIndex(this._lblTitle, 0);
			this.grpFreightInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load selection lists
				try {
					base.LoadSelections(base.mEntry.EntryType, this.cboAmountType);
					base.LoadSelections(base.mEntry.EntryType, this.cboFreightType);
				} catch(Exception) { }
				
				//Load controls
				this.Text = base.mEntry.EntryType + "(" + base.mEntry.EntryID.ToString() + ")";
				ClientInboundFreight freight = (ClientInboundFreight)base.mEntry;
				this.txtCreatedBy.Enabled = this.dtpCreatedDate.Enabled = false;
				this.txtCreatedBy.Text = freight.CreatedBy;
				this.dtpCreatedDate.Value = freight.Created;
				this.txtVendor.Text = freight.VendorName;
				this.txtDriver.Text = freight.DriverName;
				this.txtConsignee.Text = freight.ConsigneeName;
				this.dtpETA.MinDate = new DateTime(2000,01,01);
				this.dtpETA.MaxDate = DateTime.Today.AddDays(3);
				if(freight.ETATime.CompareTo(DateTime.MinValue) > 0) this.dtpETA.Value = freight.ETATime;
				this.txtTrailerNumber.Text = freight.TrailerNumber;
				this.txtAmount.Text = freight.Amount.ToString();
				this.cboAmountType.Text = freight.AmountType;
				this.cboFreightType.Text = freight.FreightType;
				this.txtComments.Text = freight.Comments;
				this.chkIn.Checked = freight.In;
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
					base.SaveSelections(base.mEntry.EntryType, this.cboAmountType);
					base.SaveSelections(base.mEntry.EntryType, this.cboFreightType);
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
				ClientInboundFreight freight = (ClientInboundFreight)base.mEntry;
				freight.CreatedBy = this.txtCreatedBy.Text;
				freight.Created = this.dtpCreatedDate.Value;
				freight.VendorName = this.txtVendor.Text;
				freight.DriverName = this.txtDriver.Text;
				freight.ConsigneeName = this.txtConsignee.Text;
				freight.ETATime = (this.dtpETA.Checked) ? this.dtpETA.Value : DateTime.MinValue;
				freight.TrailerNumber = this.txtTrailerNumber.Text;
				freight.Amount = Convert.ToInt32(this.txtAmount.Text);
				freight.AmountType = this.cboAmountType.Text;
				freight.FreightType = this.cboFreightType.Text;
				freight.Comments = this.txtComments.Text;
				freight.In = this.chkIn.Checked;
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
	}
}

