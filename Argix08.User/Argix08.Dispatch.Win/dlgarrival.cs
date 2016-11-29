//	File:	dlgarrival.cs
//	Author:	J. Heary
//	Date:	09/29/05
//	Desc:	Dialog to view/edit arrivals on the Line Haul Schedule.
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
	public class dlgArrival : Argix.Dispatch.dlgSchedule {
		//Members
		//Constants
		//Events
		#region Controls
		private System.Windows.Forms.Label _lblCreatedDate;
		private System.Windows.Forms.DateTimePicker dtpCreatedDate;
		private System.Windows.Forms.Label _lblCreatedBy;
		private System.Windows.Forms.Label _lblTitle;
		private System.Windows.Forms.GroupBox grpFreightInfo;
		private System.Windows.Forms.Label _lblTerminal;
		private System.Windows.Forms.Label _lblTrailerNumber;
		private System.Windows.Forms.TextBox txtTrailerNumber;
		private System.Windows.Forms.DateTimePicker dtpActualArrival;
		private System.Windows.Forms.DateTimePicker dtpScheduledArrival;
		private System.Windows.Forms.Label _lblScheduledArrival;
		private System.Windows.Forms.Label _lblActualArrival;
		private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.GroupBox grpLine2;
		private System.Windows.Forms.GroupBox grpLine1;
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.ComboBox cboTerminal;
		private System.Windows.Forms.TextBox txtCreatedBy;
		private System.ComponentModel.IContainer components = null;
		#endregion
		
		public dlgArrival(Arrival entry, Mediator mediator): base(entry, mediator) {
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
			this._lblTerminal = new System.Windows.Forms.Label();
			this._lblTrailerNumber = new System.Windows.Forms.Label();
			this.txtTrailerNumber = new System.Windows.Forms.TextBox();
			this.dtpActualArrival = new System.Windows.Forms.DateTimePicker();
			this.dtpScheduledArrival = new System.Windows.Forms.DateTimePicker();
			this._lblScheduledArrival = new System.Windows.Forms.Label();
			this._lblActualArrival = new System.Windows.Forms.Label();
			this._lblComments = new System.Windows.Forms.Label();
			this.grpLine2 = new System.Windows.Forms.GroupBox();
			this.grpLine1 = new System.Windows.Forms.GroupBox();
			this.txtComments = new System.Windows.Forms.TextBox();
			this.cboTerminal = new System.Windows.Forms.ComboBox();
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
			this.btnOK.Location = new System.Drawing.Point(462, 318);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 4;
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(564, 318);
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
			this._lblTitle.Location = new System.Drawing.Point(206, 6);
			this._lblTitle.Name = "_lblTitle";
			this._lblTitle.Size = new System.Drawing.Size(252, 32);
			this._lblTitle.TabIndex = 127;
			this._lblTitle.Text = "Line Haul Schedule";
			this._lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// grpFreightInfo
			// 
			this.grpFreightInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpFreightInfo.Controls.Add(this._lblTerminal);
			this.grpFreightInfo.Controls.Add(this._lblTrailerNumber);
			this.grpFreightInfo.Controls.Add(this.txtTrailerNumber);
			this.grpFreightInfo.Controls.Add(this.dtpActualArrival);
			this.grpFreightInfo.Controls.Add(this.dtpScheduledArrival);
			this.grpFreightInfo.Controls.Add(this._lblScheduledArrival);
			this.grpFreightInfo.Controls.Add(this._lblActualArrival);
			this.grpFreightInfo.Controls.Add(this._lblComments);
			this.grpFreightInfo.Controls.Add(this.grpLine2);
			this.grpFreightInfo.Controls.Add(this.grpLine1);
			this.grpFreightInfo.Controls.Add(this.txtComments);
			this.grpFreightInfo.Controls.Add(this.cboTerminal);
			this.grpFreightInfo.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpFreightInfo.Location = new System.Drawing.Point(5, 96);
			this.grpFreightInfo.Name = "grpFreightInfo";
			this.grpFreightInfo.Size = new System.Drawing.Size(654, 213);
			this.grpFreightInfo.TabIndex = 3;
			this.grpFreightInfo.TabStop = false;
			this.grpFreightInfo.Text = "Freight Information";
			// 
			// _lblTerminal
			// 
			this._lblTerminal.Location = new System.Drawing.Point(336, 27);
			this._lblTerminal.Name = "_lblTerminal";
			this._lblTerminal.Size = new System.Drawing.Size(108, 18);
			this._lblTerminal.TabIndex = 138;
			this._lblTerminal.Text = "Terminal: ";
			this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblTrailerNumber
			// 
			this._lblTrailerNumber.Location = new System.Drawing.Point(24, 27);
			this._lblTrailerNumber.Name = "_lblTrailerNumber";
			this._lblTrailerNumber.Size = new System.Drawing.Size(96, 18);
			this._lblTrailerNumber.TabIndex = 137;
			this._lblTrailerNumber.Text = "Trailer#: ";
			this._lblTrailerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtTrailerNumber
			// 
			this.txtTrailerNumber.Location = new System.Drawing.Point(129, 27);
			this.txtTrailerNumber.Name = "txtTrailerNumber";
			this.txtTrailerNumber.Size = new System.Drawing.Size(192, 21);
			this.txtTrailerNumber.TabIndex = 0;
			this.txtTrailerNumber.Text = "";
			this.txtTrailerNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dtpActualArrival
			// 
			this.dtpActualArrival.CustomFormat = "MMM dd, yyyy   hh:mm tt";
			this.dtpActualArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpActualArrival.Location = new System.Drawing.Point(453, 81);
			this.dtpActualArrival.Name = "dtpActualArrival";
			this.dtpActualArrival.ShowUpDown = true;
			this.dtpActualArrival.Size = new System.Drawing.Size(192, 21);
			this.dtpActualArrival.TabIndex = 3;
			this.dtpActualArrival.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dtpScheduledArrival
			// 
			this.dtpScheduledArrival.CustomFormat = "MMM dd, yyyy   hh:mm tt";
			this.dtpScheduledArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.dtpScheduledArrival.Location = new System.Drawing.Point(129, 81);
			this.dtpScheduledArrival.Name = "dtpScheduledArrival";
			this.dtpScheduledArrival.ShowUpDown = true;
			this.dtpScheduledArrival.Size = new System.Drawing.Size(192, 21);
			this.dtpScheduledArrival.TabIndex = 2;
			this.dtpScheduledArrival.ValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblScheduledArrival
			// 
			this._lblScheduledArrival.Location = new System.Drawing.Point(6, 81);
			this._lblScheduledArrival.Name = "_lblScheduledArrival";
			this._lblScheduledArrival.Size = new System.Drawing.Size(114, 18);
			this._lblScheduledArrival.TabIndex = 131;
			this._lblScheduledArrival.Text = "Scheduled Arrival: ";
			this._lblScheduledArrival.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblActualArrival
			// 
			this._lblActualArrival.Location = new System.Drawing.Point(330, 81);
			this._lblActualArrival.Name = "_lblActualArrival";
			this._lblActualArrival.Size = new System.Drawing.Size(114, 18);
			this._lblActualArrival.TabIndex = 130;
			this._lblActualArrival.Text = "Actual Arrival: ";
			this._lblActualArrival.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblComments
			// 
			this._lblComments.Location = new System.Drawing.Point(6, 132);
			this._lblComments.Name = "_lblComments";
			this._lblComments.Size = new System.Drawing.Size(114, 18);
			this._lblComments.TabIndex = 129;
			this._lblComments.Text = "Comments: ";
			this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grpLine2
			// 
			this.grpLine2.Location = new System.Drawing.Point(6, 111);
			this.grpLine2.Name = "grpLine2";
			this.grpLine2.Size = new System.Drawing.Size(639, 9);
			this.grpLine2.TabIndex = 128;
			this.grpLine2.TabStop = false;
			// 
			// grpLine1
			// 
			this.grpLine1.Location = new System.Drawing.Point(6, 57);
			this.grpLine1.Name = "grpLine1";
			this.grpLine1.Size = new System.Drawing.Size(639, 9);
			this.grpLine1.TabIndex = 126;
			this.grpLine1.TabStop = false;
			// 
			// txtComments
			// 
			this.txtComments.Location = new System.Drawing.Point(129, 132);
			this.txtComments.Multiline = true;
			this.txtComments.Name = "txtComments";
			this.txtComments.Size = new System.Drawing.Size(516, 69);
			this.txtComments.TabIndex = 4;
			this.txtComments.Text = "";
			this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// cboTerminal
			// 
			this.cboTerminal.Location = new System.Drawing.Point(453, 27);
			this.cboTerminal.Name = "cboTerminal";
			this.cboTerminal.Size = new System.Drawing.Size(192, 21);
			this.cboTerminal.TabIndex = 1;
			this.cboTerminal.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// dlgArrival
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(664, 350);
			this.Controls.Add(this._lblCreatedDate);
			this.Controls.Add(this.dtpCreatedDate);
			this.Controls.Add(this.txtCreatedBy);
			this.Controls.Add(this._lblCreatedBy);
			this.Controls.Add(this._lblTitle);
			this.Controls.Add(this.grpFreightInfo);
			this.Cursor = System.Windows.Forms.Cursors.Default;
			this.Name = "dlgArrival";
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
					base.LoadSelections(base.mEntry.EntryType, this.cboTerminal);
				} catch(Exception) { }
				
				//Load controls
				this.Text = base.mEntry.EntryType + "(" + base.mEntry.EntryID.ToString() + ")";
				Arrival arrival = (Arrival)base.mEntry;
				this.txtCreatedBy.Enabled = this.dtpCreatedDate.Enabled = false;
				this.txtCreatedBy.Text = arrival.CreatedBy;
				this.dtpCreatedDate.Value = arrival.Created;
				this.txtTrailerNumber.Text = arrival.TrailerNumber;
				this.cboTerminal.Text = arrival.Terminal;
				this.dtpScheduledArrival.Value = arrival.ScheduledArrival;
				this.dtpActualArrival.Value = arrival.ActualArrival;
				this.txtComments.Text = arrival.Comments;
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
				//
				try {
					base.SaveSelections(base.mEntry.EntryType, this.cboTerminal);
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
				Arrival arrival = (Arrival)base.mEntry;
				arrival.CreatedBy = this.txtCreatedBy.Text;
				arrival.Created = this.dtpCreatedDate.Value;
				arrival.TrailerNumber = this.txtTrailerNumber.Text;
				arrival.Terminal = this.cboTerminal.Text;
				arrival.ScheduledArrival = this.dtpScheduledArrival.Value;
				arrival.ActualArrival = this.dtpActualArrival.Value;
				arrival.Comments = this.txtComments.Text;
			}
			catch(Exception ex)  { base.reportError(ex); }
		}
	}
}

