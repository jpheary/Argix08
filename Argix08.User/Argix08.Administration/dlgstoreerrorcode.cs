//	File:	dlgstoreerrorcode.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create, edit, and delete store error codes.
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
	public class dlgStoreErrorCodeDetail : System.Windows.Forms.Form {
		//Members
		private int mCode=0;
		#region Controls
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.GroupBox fraDetails;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtCodeDescription;
		private Tsort.Enterprise.StoreErrorCodeDS mCodeDS;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.Label _lblCode;
		private System.Windows.Forms.Label _lblCodeDescription;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgStoreErrorCodeDetail(ref StoreErrorCodeDS code) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mCodeDS = code;
				if(this.mCodeDS.StoreErrorCodeTable.Count > 0) {
					this.mCode = this.mCodeDS.StoreErrorCodeTable[0].CodeID;
					this.Text = (this.mCode>0) ? "Store Error Code (" + this.mCode + ")" : "Store Error Code (New)";
				}
				else
					this.Text = "Store Error Code (Data Unavailable)";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgStoreErrorCodeDetail));
			this._lblCode = new System.Windows.Forms.Label();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.fraDetails = new System.Windows.Forms.GroupBox();
			this.txtCodeDescription = new System.Windows.Forms.TextBox();
			this._lblCodeDescription = new System.Windows.Forms.Label();
			this.mCodeDS = new Tsort.Enterprise.StoreErrorCodeDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mCodeDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblCode
			// 
			this._lblCode.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCode.Location = new System.Drawing.Point(6, 24);
			this._lblCode.Name = "_lblCode";
			this._lblCode.Size = new System.Drawing.Size(96, 18);
			this._lblCode.TabIndex = 2;
			this._lblCode.Text = "Name";
			this._lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCode
			// 
			this.txtCode.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtCode.Location = new System.Drawing.Point(108, 24);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(228, 21);
			this.txtCode.TabIndex = 0;
			this.txtCode.Text = "";
			this.txtCode.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(174, 234);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(276, 234);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraDetails
			// 
			this.fraDetails.Controls.Add(this.txtCodeDescription);
			this.fraDetails.Controls.Add(this._lblCodeDescription);
			this.fraDetails.Controls.Add(this.txtCode);
			this.fraDetails.Controls.Add(this._lblCode);
			this.fraDetails.Location = new System.Drawing.Point(6, 6);
			this.fraDetails.Name = "fraDetails";
			this.fraDetails.Size = new System.Drawing.Size(348, 186);
			this.fraDetails.TabIndex = 0;
			this.fraDetails.TabStop = false;
			this.fraDetails.Text = "Store Error Code";
			// 
			// txtCodeDescription
			// 
			this.txtCodeDescription.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtCodeDescription.Location = new System.Drawing.Point(108, 54);
			this.txtCodeDescription.Multiline = true;
			this.txtCodeDescription.Name = "txtCodeDescription";
			this.txtCodeDescription.Size = new System.Drawing.Size(228, 22);
			this.txtCodeDescription.TabIndex = 1;
			this.txtCodeDescription.Text = "";
			this.txtCodeDescription.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblCodeDescription
			// 
			this._lblCodeDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCodeDescription.Location = new System.Drawing.Point(6, 54);
			this._lblCodeDescription.Name = "_lblCodeDescription";
			this._lblCodeDescription.Size = new System.Drawing.Size(96, 18);
			this._lblCodeDescription.TabIndex = 19;
			this._lblCodeDescription.Text = "Description";
			this._lblCodeDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mCodeDS
			// 
			this.mCodeDS.DataSetName = "StoreErrorCodeDS";
			this.mCodeDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(369, 225);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraDetails);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(361, 199);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// dlgStoreErrorCodeDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(378, 263);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgStoreErrorCodeDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Store Error Code Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mCodeDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Set initial services
				this.Visible = true;
				Application.DoEvents();
				
				//Get lists
				//N/A
				
				//Set control services
				this.txtCode.MaxLength = 10;
				this.txtCode.Text = "";
				if(!this.mCodeDS.StoreErrorCodeTable[0].IsCodeNull())
					this.txtCode.Text = this.mCodeDS.StoreErrorCodeTable[0].Code;
				this.txtCodeDescription.MaxLength = 30;
				this.txtCodeDescription.Text = "";
				if(!this.mCodeDS.StoreErrorCodeTable[0].IsDescriptionNull())
					this.txtCodeDescription.Text = this.mCodeDS.StoreErrorCodeTable[0].Description;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in control values
			try {
				if(this.mCodeDS.StoreErrorCodeTable.Count>0) {
					//Enable OK service if appointment details have valid changes
					this.btnOk.Enabled = (this.txtCode.Text!="" && this.txtCodeDescription.Text!="");
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				this.Cursor = Cursors.WaitCursor;
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						//Update details with control values
						this.mCodeDS.StoreErrorCodeTable[0].Code = this.txtCode.Text;
						this.mCodeDS.StoreErrorCodeTable[0].Description = this.txtCodeDescription.Text;
						this.mCodeDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: reportError()
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
