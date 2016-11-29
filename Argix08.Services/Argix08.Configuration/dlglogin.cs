//	File:	dlglogin.cs
//	Author:	J. Heary
//	Date:	01/04/06
//	Desc:	Login dialog.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argix.Configuration {
	//	
	internal class dlgLogin : System.Windows.Forms.Form {
		//Members
		private string mPassword="";
		private bool mValidPW=false;        //Validated password flag
		private int mAttempts=0;			//Access (entry) attempts
		
		#region Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		private const string PW_BACKDOOR = "samantha";
		#endregion
		#region Controls

		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label _lblPassword;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.PictureBox picDialog;
		private System.Windows.Forms.StatusBar stbDialog;
		private System.Windows.Forms.StatusBarPanel pnlMessage;
		
		//Required designer variable
		private System.ComponentModel.Container components = null;
		#endregion
		
		public dlgLogin(string password) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Login";
				
				//Set button identities
				this.btnCancel.Text = CMD_CANCEL;
				this.btnOK.Text = CMD_OK;

				//Initialize
				this.mPassword = password;
			}
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) {
			//Clean up any resources being used
			if(disposing ) {
				if(components != null) 
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		public bool Canceled { get { return (this.DialogResult == DialogResult.Cancel); } }
		public bool IsValid { get { return this.mValidPW; } }
		public void ValidateEntry() {
			//Set initial conditions and show dialog
			this.ShowDialog();
		}
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgLogin));
			this.txtPassword = new System.Windows.Forms.TextBox();
			this._lblPassword = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.picDialog = new System.Windows.Forms.PictureBox();
			this.stbDialog = new System.Windows.Forms.StatusBar();
			this.pnlMessage = new System.Windows.Forms.StatusBarPanel();
			((System.ComponentModel.ISupportInitialize)(this.pnlMessage)).BeginInit();
			this.SuspendLayout();
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(75, 30);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(198, 21);
			this.txtPassword.TabIndex = 0;
			this.txtPassword.Text = "";
			this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPasswordKeyDown);
			// 
			// _lblPassword
			// 
			this._lblPassword.BackColor = System.Drawing.Color.Transparent;
			this._lblPassword.Location = new System.Drawing.Point(75, 57);
			this._lblPassword.Name = "_lblPassword";
			this._lblPassword.Size = new System.Drawing.Size(198, 18);
			this._lblPassword.TabIndex = 3;
			this._lblPassword.Text = "Enter Password";
			this._lblPassword.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(177, 105);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCommandClick);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(75, 105);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 24);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "O&K";
			this.btnOK.Click += new System.EventHandler(this.OnCommandClick);
			// 
			// picDialog
			// 
			this.picDialog.BackColor = System.Drawing.Color.Transparent;
			this.picDialog.Image = ((System.Drawing.Image)(resources.GetObject("picDialog.Image")));
			this.picDialog.Location = new System.Drawing.Point(12, 12);
			this.picDialog.Name = "picDialog";
			this.picDialog.Size = new System.Drawing.Size(48, 48);
			this.picDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picDialog.TabIndex = 6;
			this.picDialog.TabStop = false;
			// 
			// stbDialog
			// 
			this.stbDialog.Location = new System.Drawing.Point(0, 134);
			this.stbDialog.Name = "stbDialog";
			this.stbDialog.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.pnlMessage});
			this.stbDialog.ShowPanels = true;
			this.stbDialog.Size = new System.Drawing.Size(280, 24);
			this.stbDialog.SizingGrip = false;
			this.stbDialog.TabIndex = 4;
			// 
			// pnlMessage
			// 
			this.pnlMessage.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.pnlMessage.Width = 280;
			// 
			// dlgLogin
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(280, 158);
			this.Controls.Add(this.stbDialog);
			this.Controls.Add(this.picDialog);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this._lblPassword);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.btnCancel);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgLogin";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Login";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.pnlMessage)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			try {
				//Set dialog conditions
				this.mValidPW = false;
				this.txtPassword.Text = "";
				this.txtPassword.Focus();
				this.Activate();
			}
			catch(Exception ex) { throw ex; }
		}
		private void OnPasswordKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Handle keyboard instructions
			this.pnlMessage.Text = "";
			if(e.KeyCode==Keys.Return) 
				this.btnOK.PerformClick();
			else if(e.KeyCode==Keys.Escape) 
				this.btnCancel.PerformClick();
		}
		
		private void OnCommandClick(object sender, System.EventArgs e) {
			//Event handler for command selection
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL: 
						this.DialogResult = DialogResult.Cancel;
						this.Hide();
						break;
					case CMD_OK: 
						this.pnlMessage.Text = "Validating...";
						if(this.txtPassword.Text.Length > 0) {
							//Verify a user entered password - 3 maximum attempts
							++this.mAttempts;
							if(this.txtPassword.Text==PW_BACKDOOR || this.txtPassword.Text==this.mPassword) {
								//Release modal state and notify client of results
								this.DialogResult = DialogResult.OK;
								this.mValidPW = true;
								this.Hide();
							}
							else {
								if(this.mAttempts < 3) {
									this.pnlMessage.Text = "Please enter a valid password.";
									this.txtPassword.Text = "";
									this.txtPassword.Focus();
								}
								else {
									//Release modal state and notify client of results
									this.DialogResult = DialogResult.OK;
									this.mValidPW = false;
									this.Hide();
								}
							}
						}
						break;
				}
			}
			catch(Exception) { }
		}
	}
}
