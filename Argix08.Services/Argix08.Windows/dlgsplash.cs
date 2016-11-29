//	File:	dlgsplash.cs
//	Author:	J. Heary
//	Date:	01/04/06
//	Desc:	Splash screen.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace Argix.Windows {
	//
	internal class dlgSplash : System.Windows.Forms.Form {
		//Members
        private bool mClosed = false;
		
		public event EventHandler ITEvent=null;
		#region Controls
		public System.Windows.Forms.Label lblSplash;
		public System.Windows.Forms.Label lblCopyRight;
		public System.Windows.Forms.Label lblTitle;
		public System.Windows.Forms.Label lblCopyright2;
		public System.Windows.Forms.Label lblVersion;
		public System.Windows.Forms.PictureBox picApp;
		private System.Windows.Forms.PictureBox picDialog;
		public System.Windows.Forms.PictureBox pictureBox1;
		public System.Windows.Forms.PictureBox pictureBox2;
		public System.Windows.Forms.PictureBox pictureBox3;
		
		private System.ComponentModel.Container components = null;	//Required designer variable
		#endregion
		
		//Interface
		public dlgSplash(string title, string version, string copyright) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.lblTitle.Text = title;
				this.lblVersion.Text = version;
				this.lblCopyRight.Text = copyright;
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgSplash instance.",ex); }
		}
		protected override void Dispose(bool disposing) { if(disposing) { if(components!=null) components.Dispose(); } base.Dispose(disposing); }
        public bool Closed { get { return this.mClosed; } }
        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgSplash));
			this.lblSplash = new System.Windows.Forms.Label();
			this.lblCopyRight = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.lblCopyright2 = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.picApp = new System.Windows.Forms.PictureBox();
			this.picDialog = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.SuspendLayout();
			// 
			// lblSplash
			// 
			this.lblSplash.BackColor = System.Drawing.Color.Transparent;
			this.lblSplash.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblSplash.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSplash.ForeColor = System.Drawing.Color.White;
			this.lblSplash.Location = new System.Drawing.Point(294, 57);
			this.lblSplash.Name = "lblSplash";
			this.lblSplash.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblSplash.Size = new System.Drawing.Size(162, 36);
			this.lblSplash.TabIndex = 17;
			this.lblSplash.Text = "Argix";
			this.lblSplash.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblCopyRight
			// 
			this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
			this.lblCopyRight.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblCopyRight.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCopyRight.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCopyRight.Location = new System.Drawing.Point(237, 218);
			this.lblCopyRight.Name = "lblCopyRight";
			this.lblCopyRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCopyRight.Size = new System.Drawing.Size(228, 18);
			this.lblCopyRight.TabIndex = 11;
			this.lblCopyRight.Text = "Copyright 2004 Argix Direct";
			this.lblCopyRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// lblTitle
			// 
			this.lblTitle.BackColor = System.Drawing.Color.Transparent;
			this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblTitle.Font = new System.Drawing.Font("Verdana", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTitle.Location = new System.Drawing.Point(237, 117);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblTitle.Size = new System.Drawing.Size(228, 72);
			this.lblTitle.TabIndex = 14;
			this.lblTitle.Text = "Application Name";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCopyright2
			// 
			this.lblCopyright2.BackColor = System.Drawing.Color.Transparent;
			this.lblCopyright2.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblCopyright2.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCopyright2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblCopyright2.Location = new System.Drawing.Point(18, 257);
			this.lblCopyright2.Name = "lblCopyright2";
			this.lblCopyright2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblCopyright2.Size = new System.Drawing.Size(447, 18);
			this.lblCopyright2.TabIndex = 13;
			this.lblCopyright2.Text = "This program is protected by US and international copyright laws as described in " +
				"Help About.";
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.Transparent;
			this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblVersion.Location = new System.Drawing.Point(237, 189);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblVersion.Size = new System.Drawing.Size(228, 18);
			this.lblVersion.TabIndex = 12;
			this.lblVersion.Text = "Version #";
			// 
			// picApp
			// 
			this.picApp.BackColor = System.Drawing.Color.Transparent;
			this.picApp.Cursor = System.Windows.Forms.Cursors.Default;
			this.picApp.Image = ((System.Drawing.Image)(resources.GetObject("picApp.Image")));
			this.picApp.Location = new System.Drawing.Point(81, 36);
			this.picApp.Name = "picApp";
			this.picApp.Size = new System.Drawing.Size(48, 48);
			this.picApp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picApp.TabIndex = 15;
			this.picApp.TabStop = false;
			// 
			// picDialog
			// 
			this.picDialog.Image = ((System.Drawing.Image)(resources.GetObject("picDialog.Image")));
			this.picDialog.Location = new System.Drawing.Point(18, 18);
			this.picDialog.Name = "picDialog";
			this.picDialog.Size = new System.Drawing.Size(207, 27);
			this.picDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picDialog.TabIndex = 18;
			this.picDialog.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(108, 144);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 19;
			this.pictureBox1.TabStop = false;
			// 
			// pictureBox2
			// 
			this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(141, 78);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(48, 48);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox2.TabIndex = 20;
			this.pictureBox2.TabStop = false;
			// 
			// pictureBox3
			// 
			this.pictureBox3.BackColor = System.Drawing.Color.Transparent;
			this.pictureBox3.Cursor = System.Windows.Forms.Cursors.Default;
			this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
			this.pictureBox3.Location = new System.Drawing.Point(24, 165);
			this.pictureBox3.Name = "pictureBox3";
			this.pictureBox3.Size = new System.Drawing.Size(48, 48);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox3.TabIndex = 21;
			this.pictureBox3.TabStop = false;
			// 
			// dlgSplash
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.ClientSize = new System.Drawing.Size(474, 282);
			this.ControlBox = false;
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.picDialog);
			this.Controls.Add(this.lblSplash);
			this.Controls.Add(this.lblCopyRight);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.lblCopyright2);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.picApp);
			this.Controls.Add(this.pictureBox3);
			this.Controls.Add(this.pictureBox1);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgSplash";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try { 
				this.Refresh();
				this.BringToFront();
				this.Focus();
			}
			catch { }
            finally { this.Cursor = Cursors.WaitCursor; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form load event
			try { this.SendToBack(); } catch { }
		}
		private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for form keydown event
			try {
				if(e.Control && e.KeyCode == Keys.Enter) {
					this.picApp.Visible = false;
					if(this.ITEvent != null) this.ITEvent(this, new EventArgs());
				}
			} 
			catch { }
		}
		private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			//Event handler for form keypress eventt
			try {
                switch(e.KeyChar) {
                    case (char)13: e.Handled = true; this.mClosed = true;  this.Close(); break;
                    case (char)32: e.Handled = true; this.mClosed = true; this.Close(); break;
					default:        e.Handled = true; break;
				}
			} 
			catch { }
		}
	}
}
