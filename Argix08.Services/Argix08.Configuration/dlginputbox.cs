//	File:	inputbox.cs
//	Author:	J. Heary
//	Date:	01/04/06
//	Desc:	Input dialog box.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;

namespace Argix.Configuration {
	//
	internal class dlgInputBox : System.Windows.Forms.Form {
		//Members
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.Label lblMessage;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Events
		
		//Interface
		public dlgInputBox(string message, string data, string title) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set message, data, and titlebar caption
				this.lblMessage.Text = message;
				this.txtInput.Text = data;
				this.Text = title;
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components!=null) components.Dispose(); } base.Dispose(disposing); }
		public string Value { get {return this.txtInput.Text;} }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.lblMessage = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(183, 93);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(81, 93);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// txtInput
			// 
			this.txtInput.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtInput.HideSelection = false;
			this.txtInput.Location = new System.Drawing.Point(3, 63);
			this.txtInput.MaxLength = 24;
			this.txtInput.Name = "txtInput";
			this.txtInput.Size = new System.Drawing.Size(276, 21);
			this.txtInput.TabIndex = 0;
			this.txtInput.Text = "";
			this.txtInput.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(3, 3);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(276, 54);
			this.lblMessage.TabIndex = 3;
			this.lblMessage.Text = "Message";
			// 
			// dlgInputBox
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(282, 119);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.txtInput);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgInputBox";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Input Box";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			try {
				//Set initial service states
				this.txtInput.Focus();
				ValidateForm(null,null);
			}
			catch(Exception) { }
		}
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			this.btnOk.Enabled = (this.txtInput.Text.Length > 0);
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:	this.DialogResult = DialogResult.Cancel; break;
					case CMD_OK:		this.DialogResult = DialogResult.OK; break;
				}
				this.Close();
			} 
			catch(Exception) { }
		}
	}
}
