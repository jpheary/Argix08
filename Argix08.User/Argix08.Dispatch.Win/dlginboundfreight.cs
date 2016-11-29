//	File:	dlgInboundFreight.cs
//	Author:	J. Heary
//	Date:	09/29/05
//	Desc:	Dialog to view/edit freight on the inbound schedule.
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
	public class dlgInboundFreight : Argix.Dispatch.dlgSchedule {
		//Members
		//Constants
		//Events
		#region Controls
		private System.Windows.Forms.Label _lblCreatedDate;
		private System.Windows.Forms.DateTimePicker dtpCreatedDate;
		private System.Windows.Forms.TextBox txtCreatedBy;
		private System.Windows.Forms.Label _lblCreatedBy;
		private System.Windows.Forms.Label _lblTitle;
		private System.Windows.Forms.GroupBox grpFreightInfo;
		private System.Windows.Forms.Label _lblFreightTo;
		private System.Windows.Forms.ComboBox cboFreightTo;
		private System.Windows.Forms.DateTimePicker dtpActualDelivery;
		private System.Windows.Forms.Label _lblActualDelivery;
		private System.Windows.Forms.DateTimePicker dtpActualDeparture;
		private System.Windows.Forms.Label _lblActualDeparture;
		private System.Windows.Forms.DateTimePicker dtpScheduledDeparture;
		private System.Windows.Forms.Label _lblScheduledDeparture;
		private System.Windows.Forms.Label _lblFreightFrom;
		private System.Windows.Forms.Label _lblDriver;
		private System.Windows.Forms.TextBox txtDriver;
		private System.Windows.Forms.DateTimePicker dtpScheduledDelivery;
		private System.Windows.Forms.Label _lblScheduledDelivery;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.GroupBox grpLine2;
		private System.Windows.Forms.GroupBox grpLine1;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.ComboBox cboFreightFrom;
		private System.Windows.Forms.CheckBox chkConfirmed;
		private System.Windows.Forms.TextBox txtTrailerNumber;
		private System.Windows.Forms.Label _lblTrailerNumber;
		private System.ComponentModel.IContainer components = null;
		#endregion

		public dlgInboundFreight(InboundFreight entry, Mediator mediator): base(entry, mediator) {
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
			this._lblCreatedDate = new System.Windows.Forms.Label();
			this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
			this.txtCreatedBy = new System.Windows.Forms.TextBox();
			this._lblCreatedBy = new System.Windows.Forms.Label();
			this._lblTitle = new System.Windows.Forms.Label();
			this.grpFreightInfo = new System.Windows.Forms.GroupBox();
			this._lblFreightTo = new System.Windows.Forms.Label();
			this.cboFreightTo = new System.Windows.Forms.ComboBox();
			this.dtpActualDelivery = new System.Windows.Forms.DateTimePicker();
			this._lblActualDelivery = new System.Windows.Forms.Label();
			this.dtpActualDeparture = new System.Windows.Forms.DateTimePicker();
			this._lblActualDeparture = new System.Windows.Forms.Label();
			this.dtpScheduledDeparture = new System.Windows.Forms.DateTimePicker();
			this._lblScheduledDeparture = new System.Windows.Forms.Label();
			this._lblFreightFrom = new System.Windows.Forms.Label();
			this._lblDriver = new System.Windows.Forms.Label();
			this.txtDriver = new System.Windows.Forms.TextBox();
			this.dtpScheduledDelivery = new System.Windows.Forms.DateTimePicker();
			this._lblScheduledDelivery = new System.Windows.Forms.Label();
			this._lblComments = new System.Windows.Forms.Label();
			this.grpLine2 = new System.Windows.Forms.GroupBox();
			this.grpLine1 = new System.Windows.Forms.GroupBox();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.cboFreightFrom = new System.Windows.Forms.ComboBox();
			this.chkConfirmed = new System.Windows.Forms.CheckBox();
			this.txtTrailerNumber = new System.Windows.Forms.TextBox();
			this._lblTrailerNumber = new System.Windows.Forms.Label();
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
			// _lblCreatedDate
			// 
			this._lblCreatedDate.Location = new System.Drawing.Point(354, 60);
			this._lblCreatedDate.Name = "_lblCreatedDate";
			this._lblCreatedDate.Size = new System.Drawing.Size(96, 18);
			this._lblCreatedDate.TabIndex = 131;
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
			this._lblCreatedBy.TabIndex = 128;
			this._lblCreatedBy.Text = "Created By: ";
			this._lblCreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblTitle
			// 
			this._lblTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this._lblTitle.Font = new System.Drawing.Font("Verdana", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTitle.Location = new System.Drawing.Point(230, 6);
			this._lblTitle.Name = "_lblTitle";
			this._lblTitle.Size = new System.Drawing.Size(207, 32);
			this._lblTitle.TabIndex = 127;
			this._lblTitle.Text = "Inbound Freight";
			this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpFreightInfo
			// 
			this.grpFreightInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpFreightInfo.Controls.Add(this._lblFreightTo);
			this.grpFreightInfo.Controls.Add(this.cboFreightTo);
			this.grpFreightInfo.Controls.Add(this.dtpActualDelivery);
			this.grpFreightInfo.Controls.Add(this._lblActualDelivery);
			this.grpFreightInfo.Controls.Add(this.dtpActualDeparture);
			this.grpFreightInfo.Controls.Add(this._lblActualDeparture);
			this.grpFreightInfo.Controls.Add(this.dtpScheduledDeparture);
			this.grpFreightInfo.Controls.Add(this._lblScheduledDeparture);
			this.grpFreightInfo.Controls.Add(this._lblFreightFrom);
			this.grpFreightInfo.Controls.Add(this._lblDriver);
			this.grpFreightInfo.Controls.Add(this.txtDriver);
			this.grpFreightInfo.Controls.Add(this.dtpScheduledDelivery);
			this.grpFreightInfo.Controls.Add(this._lblScheduledDelivery);
			this.grpFreightInfo.Controls.Add(this._lblComments);
			this.grpFreightInfo.Controls.Add(this.grpLine2);
			this.grpFreightInfo.Controls.Add(this.grpLine1);
			this.grpFreightInfo.Controls.Add(this.txtComments);
			this.grpFreightInfo.Controls.Add(this.cboFreightFrom);
			this.grpFreightInfo.Controls.Add(this.chkConfirmed);
			this.grpFreightInfo.Controls.Add(this.txtTrailerNumber);
			this.grpFreightInfo.Controls.Add(this._lblTrailerNumber);
			this.grpFreightInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpFreightInfo.Location = new System.Drawing.Point(5, 96);
			this.grpFreightInfo.Name = "grpFreightInfo";
			this.grpFreightInfo.Size = new System.Drawing.Size(654, 309);
			this.grpFreightInfo.TabIndex = 3;
			this.grpFreightInfo.TabStop = false;
			this.grpFreightInfo.Text = "Freight Information";
			// 
			// _lblFreightTo
			// 
			this._lblFreightTo.Location = new System.Drawing.Point(348, 111);
			this._lblFreightTo.Name = "_lblFreightTo";
			this._lblFreightTo.Size = new System.Drawing.Size(96, 18);
			this._lblFreightTo.TabIndex = 146;
			this._lblFreightTo.Text = "Freight To: ";
			this._lblFreightTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboFreightTo
			// 
			this.cboFreightTo.Location = new System.Drawing.Point(453, 111);
			this.cboFreightTo.Name = "cboFreightTo";
			this.cboFreightTo.Size = new System.Drawing.Size(192, 21);
			this.cboFreightTo.TabIndex = 6;
			this.cboFreightTo.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dtpActualDelivery
			// 
			this.dtpActualDelivery.CustomFormat = "hh:mm tt";
			this.dtpActualDelivery.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpActualDelivery.Location = new System.Drawing.Point(453, 141);
			this.dtpActualDelivery.Name = "dtpActualDelivery";
			this.dtpActualDelivery.ShowCheckBox = true;
			this.dtpActualDelivery.ShowUpDown = true;
			this.dtpActualDelivery.Size = new System.Drawing.Size(96, 21);
			this.dtpActualDelivery.TabIndex = 7;
			this.dtpActualDelivery.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblActualDelivery
			// 
			this._lblActualDelivery.Location = new System.Drawing.Point(348, 141);
			this._lblActualDelivery.Name = "_lblActualDelivery";
			this._lblActualDelivery.Size = new System.Drawing.Size(96, 18);
			this._lblActualDelivery.TabIndex = 143;
			this._lblActualDelivery.Text = "Act. Delivery: ";
			this._lblActualDelivery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpActualDeparture
			// 
			this.dtpActualDeparture.CustomFormat = "hh:mm tt";
			this.dtpActualDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpActualDeparture.Location = new System.Drawing.Point(453, 81);
			this.dtpActualDeparture.Name = "dtpActualDeparture";
			this.dtpActualDeparture.ShowCheckBox = true;
			this.dtpActualDeparture.ShowUpDown = true;
			this.dtpActualDeparture.Size = new System.Drawing.Size(96, 21);
			this.dtpActualDeparture.TabIndex = 5;
			this.dtpActualDeparture.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblActualDeparture
			// 
			this._lblActualDeparture.Location = new System.Drawing.Point(348, 81);
			this._lblActualDeparture.Name = "_lblActualDeparture";
			this._lblActualDeparture.Size = new System.Drawing.Size(96, 18);
			this._lblActualDeparture.TabIndex = 141;
			this._lblActualDeparture.Text = "Act. Departure: ";
			this._lblActualDeparture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dtpScheduledDeparture
			// 
			this.dtpScheduledDeparture.CustomFormat = "hh:mm tt";
			this.dtpScheduledDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpScheduledDeparture.Location = new System.Drawing.Point(129, 81);
			this.dtpScheduledDeparture.Name = "dtpScheduledDeparture";
			this.dtpScheduledDeparture.ShowCheckBox = true;
			this.dtpScheduledDeparture.ShowUpDown = true;
			this.dtpScheduledDeparture.Size = new System.Drawing.Size(96, 21);
			this.dtpScheduledDeparture.TabIndex = 2;
			this.dtpScheduledDeparture.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblScheduledDeparture
			// 
			this._lblScheduledDeparture.Location = new System.Drawing.Point(24, 81);
			this._lblScheduledDeparture.Name = "_lblScheduledDeparture";
			this._lblScheduledDeparture.Size = new System.Drawing.Size(96, 18);
			this._lblScheduledDeparture.TabIndex = 139;
			this._lblScheduledDeparture.Text = "Sch. Departure: ";
			this._lblScheduledDeparture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblFreightFrom
			// 
			this._lblFreightFrom.Location = new System.Drawing.Point(24, 111);
			this._lblFreightFrom.Name = "_lblFreightFrom";
			this._lblFreightFrom.Size = new System.Drawing.Size(96, 18);
			this._lblFreightFrom.TabIndex = 138;
			this._lblFreightFrom.Text = "Freight From: ";
			this._lblFreightFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblDriver
			// 
			this._lblDriver.Location = new System.Drawing.Point(24, 27);
			this._lblDriver.Name = "_lblDriver";
			this._lblDriver.Size = new System.Drawing.Size(96, 18);
			this._lblDriver.TabIndex = 137;
			this._lblDriver.Text = "Driver: ";
			this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDriver
			// 
			this.txtDriver.Location = new System.Drawing.Point(129, 27);
			this.txtDriver.Name = "txtDriver";
			this.txtDriver.Size = new System.Drawing.Size(192, 21);
			this.txtDriver.TabIndex = 0;
			this.txtDriver.Text = "";
			this.txtDriver.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dtpScheduledDelivery
			// 
			this.dtpScheduledDelivery.CustomFormat = "hh:mm tt";
			this.dtpScheduledDelivery.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpScheduledDelivery.Location = new System.Drawing.Point(129, 141);
			this.dtpScheduledDelivery.Name = "dtpScheduledDelivery";
			this.dtpScheduledDelivery.ShowCheckBox = true;
			this.dtpScheduledDelivery.ShowUpDown = true;
			this.dtpScheduledDelivery.Size = new System.Drawing.Size(96, 21);
			this.dtpScheduledDelivery.TabIndex = 4;
			this.dtpScheduledDelivery.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblScheduledDelivery
			// 
			this._lblScheduledDelivery.Location = new System.Drawing.Point(24, 141);
			this._lblScheduledDelivery.Name = "_lblScheduledDelivery";
			this._lblScheduledDelivery.Size = new System.Drawing.Size(96, 18);
			this._lblScheduledDelivery.TabIndex = 131;
			this._lblScheduledDelivery.Text = "Sch. Delivery: ";
			this._lblScheduledDelivery.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.grpLine2.Location = new System.Drawing.Point(6, 174);
			this.grpLine2.Name = "grpLine2";
			this.grpLine2.Size = new System.Drawing.Size(639, 9);
			this.grpLine2.TabIndex = 128;
			this.grpLine2.TabStop = false;
			// 
			// grpLine1
			// 
			this.grpLine1.Location = new System.Drawing.Point(6, 60);
			this.grpLine1.Name = "grpLine1";
			this.grpLine1.Size = new System.Drawing.Size(639, 9);
			this.grpLine1.TabIndex = 126;
			this.grpLine1.TabStop = false;
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(129, 195);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(516, 72);
			this.txtComments.TabIndex = 8;
			this.txtComments.Text = "";
			this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// cboFreightFrom
			// 
			this.cboFreightFrom.Location = new System.Drawing.Point(129, 111);
			this.cboFreightFrom.Name = "cboFreightFrom";
			this.cboFreightFrom.Size = new System.Drawing.Size(192, 21);
			this.cboFreightFrom.TabIndex = 3;
			this.cboFreightFrom.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// chkConfirmed
			// 
			this.chkConfirmed.Location = new System.Drawing.Point(129, 279);
			this.chkConfirmed.Name = "chkConfirmed";
			this.chkConfirmed.Size = new System.Drawing.Size(96, 18);
			this.chkConfirmed.TabIndex = 9;
			this.chkConfirmed.Text = "Confirmed?";
			this.chkConfirmed.CheckStateChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtTrailerNumber
			// 
			this.txtTrailerNumber.Location = new System.Drawing.Point(453, 27);
			this.txtTrailerNumber.Name = "txtTrailerNumber";
			this.txtTrailerNumber.Size = new System.Drawing.Size(192, 21);
			this.txtTrailerNumber.TabIndex = 1;
			this.txtTrailerNumber.Text = "";
			this.txtTrailerNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblTrailerNumber
			// 
			this._lblTrailerNumber.Location = new System.Drawing.Point(348, 27);
			this._lblTrailerNumber.Name = "_lblTrailerNumber";
			this._lblTrailerNumber.Size = new System.Drawing.Size(96, 18);
			this._lblTrailerNumber.TabIndex = 115;
			this._lblTrailerNumber.Text = "Trailer#: ";
			this._lblTrailerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dlgInboundFreight
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 446);
			this.Controls.Add(this._lblCreatedDate);
			this.Controls.Add(this.dtpCreatedDate);
			this.Controls.Add(this.txtCreatedBy);
			this.Controls.Add(this._lblCreatedBy);
			this.Controls.Add(this._lblTitle);
			this.Controls.Add(this.grpFreightInfo);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Name = "dlgInboundFreight";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Controls.SetChildIndex(this.btnOK, 0);
			this.Controls.SetChildIndex(this.btnCancel, 0);
			this.Controls.SetChildIndex(this.grpFreightInfo, 0);
			this.Controls.SetChildIndex(this._lblTitle, 0);
			this.Controls.SetChildIndex(this._lblCreatedBy, 0);
			this.Controls.SetChildIndex(this.txtCreatedBy, 0);
			this.Controls.SetChildIndex(this.dtpCreatedDate, 0);
			this.Controls.SetChildIndex(this._lblCreatedDate, 0);
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
					base.LoadSelections(base.mEntry.EntryType, this.cboFreightFrom);
					base.LoadSelections(base.mEntry.EntryType, this.cboFreightTo);
				} catch(Exception) { }
				
				//Load controls
				this.Text = base.mEntry.EntryType + "(" + base.mEntry.EntryID.ToString() + ")";
				InboundFreight freight = (InboundFreight)base.mEntry;
				this.txtCreatedBy.Enabled = this.dtpCreatedDate.Enabled = false;
				this.txtCreatedBy.Text = freight.CreatedBy;
				this.dtpCreatedDate.Value = freight.Created;
				this.txtTrailerNumber.Text = freight.TrailerNumber;
				this.txtDriver.Text = freight.DriverName;
				this.dtpScheduledDeparture.MinDate = new DateTime(2000,01,01);
				this.dtpScheduledDeparture.MaxDate = DateTime.Today.AddDays(7);
				if(freight.ScheduledDeparture.CompareTo(DateTime.MinValue) > 0) this.dtpScheduledDeparture.Value = freight.ScheduledDeparture;
				this.cboFreightFrom.Text = freight.FromLocation;
				this.dtpScheduledDelivery.MinDate = new DateTime(2000,01,01);
				this.dtpScheduledDelivery.MaxDate = DateTime.Today.AddDays(7);
				if(freight.ScheduledDelivery.CompareTo(DateTime.MinValue) > 0) this.dtpScheduledDelivery.Value = freight.ScheduledDelivery;
				this.dtpActualDeparture.MinDate = new DateTime(2000,01,01);
				this.dtpActualDeparture.MaxDate = DateTime.Today.AddDays(7);
				if(freight.ActualDeparture.CompareTo(DateTime.MinValue) > 0) this.dtpActualDeparture.Value = freight.ActualDeparture;
				this.cboFreightTo.Text = freight.ToLocation;
				this.dtpActualDelivery.MinDate = new DateTime(2000,01,01);
				this.dtpActualDelivery.MaxDate = DateTime.Today.AddDays(7);
				if(freight.ActualDelivery.CompareTo(DateTime.MinValue) > 0) this.dtpActualDelivery.Value = freight.ActualDelivery;
				this.txtComments.Text = freight.Comments;
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
					base.SaveSelections(base.mEntry.EntryType, this.cboFreightFrom);
					base.SaveSelections(base.mEntry.EntryType, this.cboFreightTo);
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
				InboundFreight freight = (InboundFreight)base.mEntry;
				freight.CreatedBy = this.txtCreatedBy.Text;
				freight.Created = this.dtpCreatedDate.Value;
				freight.TrailerNumber = this.txtTrailerNumber.Text;
				freight.DriverName = this.txtDriver.Text;
				freight.ScheduledDeparture = (this.dtpScheduledDeparture.Checked) ? this.dtpScheduledDeparture.Value : DateTime.MinValue;
				freight.FromLocation = this.cboFreightFrom.Text;
				freight.ScheduledDelivery = (this.dtpScheduledDelivery.Checked) ? this.dtpScheduledDelivery.Value : DateTime.MinValue;
				freight.ActualDeparture = (this.dtpActualDeparture.Checked) ? this.dtpActualDeparture.Value : DateTime.MinValue;
				freight.ToLocation = this.cboFreightTo.Text;
				freight.ActualDelivery = (this.dtpActualDelivery.Checked) ? this.dtpActualDelivery.Value : DateTime.MinValue;
				freight.Comments = this.txtComments.Text;
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
	}
}

