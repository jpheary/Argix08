//	File:	about.cs
//	Author:	J. Heary
//	Date:	01/04/06
//	Desc:	About dialog.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Argix.Windows {
	//
	public class dlgAbout : System.Windows.Forms.Form {
		//Members
		
		//Constants
		private const string CMD_CLOSE = "&Close";
		#region Controls
		private System.Windows.Forms.PictureBox picDialog;
		private System.Windows.Forms.Label lblApp;
		private System.Windows.Forms.Label lblDesc;
		private System.Windows.Forms.Label lblVer;
		private System.Windows.Forms.Label lblCopy;
		private System.Windows.Forms.Label lblDisc;
		private System.Windows.Forms.Button cmdDialog;
		private System.Windows.Forms.GroupBox grpLine;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblUpdated;
		private System.ComponentModel.Container components = null;		//Required designer variable
		#endregion
		
		//Interface
		public dlgAbout() : this("Unknown Application","Version: Unknown","Copyright: Unknown","") {}
		public dlgAbout(string description, string version, string copyright) : this(description,version,copyright,"") { }
		public dlgAbout(string description, string version, string copyright, string updated) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
			
				//Set command identities
				this.cmdDialog.Text = CMD_CLOSE;
				
				//Set labels for app description, version, and copyright, and update
				this.Text = "About " + description;
                this.lblDesc.Text = description;
                this.lblVer.Text = version;
				this.lblCopy.Text = copyright;
				this.lblUpdated.Text = updated;
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) {
			//Clean up any resources being used
			if(disposing) {
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAbout));
            this.picDialog = new System.Windows.Forms.PictureBox();
            this.lblApp = new System.Windows.Forms.Label();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.lblCopy = new System.Windows.Forms.Label();
            this.lblDisc = new System.Windows.Forms.Label();
            this.cmdDialog = new System.Windows.Forms.Button();
            this.grpLine = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUpdated = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picDialog
            // 
            this.picDialog.Image = ((System.Drawing.Image)(resources.GetObject("picDialog.Image")));
            this.picDialog.Location = new System.Drawing.Point(6,6);
            this.picDialog.Name = "picDialog";
            this.picDialog.Size = new System.Drawing.Size(207,27);
            this.picDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDialog.TabIndex = 0;
            this.picDialog.TabStop = false;
            // 
            // lblApp
            // 
            this.lblApp.BackColor = System.Drawing.Color.Transparent;
            this.lblApp.Font = new System.Drawing.Font("Verdana",20.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblApp.ForeColor = System.Drawing.Color.White;
            this.lblApp.Location = new System.Drawing.Point(204,33);
            this.lblApp.Name = "lblApp";
            this.lblApp.Size = new System.Drawing.Size(168,52);
            this.lblApp.TabIndex = 1;
            this.lblApp.Text = "Argix";
            this.lblApp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDesc.Location = new System.Drawing.Point(6,3);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(348,18);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "Argix Application";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVer
            // 
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            this.lblVer.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblVer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVer.Location = new System.Drawing.Point(6,27);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(348,18);
            this.lblVer.TabIndex = 3;
            this.lblVer.Text = "Version: 1.0.0.0";
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopy
            // 
            this.lblCopy.BackColor = System.Drawing.Color.Transparent;
            this.lblCopy.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCopy.Location = new System.Drawing.Point(6,51);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(348,18);
            this.lblCopy.TabIndex = 4;
            this.lblCopy.Text = "Copyright 2003-2004 Argix Direct";
            this.lblCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDisc
            // 
            this.lblDisc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDisc.BackColor = System.Drawing.Color.Transparent;
            this.lblDisc.Font = new System.Drawing.Font("Arial",6.75F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblDisc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDisc.Location = new System.Drawing.Point(6,199);
            this.lblDisc.Name = "lblDisc";
            this.lblDisc.Size = new System.Drawing.Size(256,62);
            this.lblDisc.TabIndex = 5;
            this.lblDisc.Text = resources.GetString("lblDisc.Text");
            // 
            // cmdDialog
            // 
            this.cmdDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDialog.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDialog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDialog.Location = new System.Drawing.Point(276,233);
            this.cmdDialog.Name = "cmdDialog";
            this.cmdDialog.Size = new System.Drawing.Size(96,24);
            this.cmdDialog.TabIndex = 6;
            this.cmdDialog.Text = "O&K";
            this.cmdDialog.UseVisualStyleBackColor = false;
            this.cmdDialog.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // grpLine
            // 
            this.grpLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpLine.BackColor = System.Drawing.Color.Transparent;
            this.grpLine.Location = new System.Drawing.Point(8,188);
            this.grpLine.Name = "grpLine";
            this.grpLine.Size = new System.Drawing.Size(364,4);
            this.grpLine.TabIndex = 7;
            this.grpLine.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.lblCopy);
            this.panel1.Controls.Add(this.lblDesc);
            this.panel1.Controls.Add(this.lblVer);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(6,88);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366,72);
            this.panel1.TabIndex = 9;
            // 
            // lblUpdated
            // 
            this.lblUpdated.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdated.Font = new System.Drawing.Font("Verdana",6F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblUpdated.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUpdated.Location = new System.Drawing.Point(15,163);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(348,18);
            this.lblUpdated.TabIndex = 10;
            this.lblUpdated.Text = "Updated 00/00/2004, jph";
            this.lblUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(378,262);
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.picDialog);
            this.Controls.Add(this.grpLine);
            this.Controls.Add(this.cmdDialog);
            this.Controls.Add(this.lblDisc);
            this.Controls.Add(this.lblApp);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Argix";
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			Button btn = (Button)sender;
			switch(btn.Text) {
				case CMD_CLOSE:
					//Close the dialog
					this.DialogResult = DialogResult.Cancel;
					this.Close();
					break;
			}
		}
	}
}