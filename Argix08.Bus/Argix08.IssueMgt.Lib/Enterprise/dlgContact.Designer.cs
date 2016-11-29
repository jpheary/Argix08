namespace Argix.Enterprise {
    partial class dlgContact {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this._lblPhone = new System.Windows.Forms.Label();
            this.txtFName = new System.Windows.Forms.TextBox();
            this._lblFName = new System.Windows.Forms.Label();
            this.txtLName = new System.Windows.Forms.TextBox();
            this._lblLName = new System.Windows.Forms.Label();
            this._lblEmail = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.MaskedTextBox();
            this.txtEmail = new System.Windows.Forms.MaskedTextBox();
            this.txtFax = new System.Windows.Forms.MaskedTextBox();
            this._lblFax = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.MaskedTextBox();
            this._lblMobile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(169,180);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82,24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(256,180);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82,24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // _lblPhone
            // 
            this._lblPhone.Location = new System.Drawing.Point(5,69);
            this._lblPhone.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblPhone.Name = "_lblPhone";
            this._lblPhone.Size = new System.Drawing.Size(82,18);
            this._lblPhone.TabIndex = 16;
            this._lblPhone.Text = "Phone";
            this._lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFName
            // 
            this.txtFName.Location = new System.Drawing.Point(92,13);
            this.txtFName.Margin = new System.Windows.Forms.Padding(4,4,4,4);
            this.txtFName.MaxLength = 25;
            this.txtFName.Name = "txtFName";
            this.txtFName.Size = new System.Drawing.Size(165,20);
            this.txtFName.TabIndex = 2;
            this.txtFName.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblFName
            // 
            this._lblFName.Location = new System.Drawing.Point(5,13);
            this._lblFName.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblFName.Name = "_lblFName";
            this._lblFName.Size = new System.Drawing.Size(82,18);
            this._lblFName.TabIndex = 14;
            this._lblFName.Text = "First Name";
            this._lblFName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtLName
            // 
            this.txtLName.Location = new System.Drawing.Point(92,41);
            this.txtLName.Margin = new System.Windows.Forms.Padding(4,4,4,4);
            this.txtLName.MaxLength = 25;
            this.txtLName.Name = "txtLName";
            this.txtLName.Size = new System.Drawing.Size(165,20);
            this.txtLName.TabIndex = 3;
            this.txtLName.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblLName
            // 
            this._lblLName.Location = new System.Drawing.Point(5,41);
            this._lblLName.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblLName.Name = "_lblLName";
            this._lblLName.Size = new System.Drawing.Size(82,18);
            this._lblLName.TabIndex = 19;
            this._lblLName.Text = "Last Name";
            this._lblLName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblEmail
            // 
            this._lblEmail.Location = new System.Drawing.Point(5,145);
            this._lblEmail.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblEmail.Name = "_lblEmail";
            this._lblEmail.Size = new System.Drawing.Size(82,18);
            this._lblEmail.TabIndex = 21;
            this._lblEmail.Text = "Email";
            this._lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPhone
            // 
            this.txtPhone.HidePromptOnLeave = true;
            this.txtPhone.Location = new System.Drawing.Point(92,70);
            this.txtPhone.Mask = "(999) 000-0000";
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(103,20);
            this.txtPhone.TabIndex = 4;
            this.txtPhone.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(92,146);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(247,20);
            this.txtEmail.TabIndex = 7;
            this.txtEmail.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtFax
            // 
            this.txtFax.HidePromptOnLeave = true;
            this.txtFax.Location = new System.Drawing.Point(92,120);
            this.txtFax.Mask = "(999) 000-0000";
            this.txtFax.Name = "txtFax";
            this.txtFax.Size = new System.Drawing.Size(103,20);
            this.txtFax.TabIndex = 6;
            this.txtFax.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblFax
            // 
            this._lblFax.Location = new System.Drawing.Point(5,119);
            this._lblFax.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblFax.Name = "_lblFax";
            this._lblFax.Size = new System.Drawing.Size(82,18);
            this._lblFax.TabIndex = 26;
            this._lblFax.Text = "Fax";
            this._lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtMobile
            // 
            this.txtMobile.HidePromptOnLeave = true;
            this.txtMobile.Location = new System.Drawing.Point(93,95);
            this.txtMobile.Mask = "(999) 000-0000";
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(103,20);
            this.txtMobile.TabIndex = 5;
            this.txtMobile.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblMobile
            // 
            this._lblMobile.Location = new System.Drawing.Point(5,94);
            this._lblMobile.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblMobile.Name = "_lblMobile";
            this._lblMobile.Size = new System.Drawing.Size(82,18);
            this._lblMobile.TabIndex = 28;
            this._lblMobile.Text = "Mobile";
            this._lblMobile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgContact
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345,208);
            this.Controls.Add(this.txtMobile);
            this.Controls.Add(this._lblMobile);
            this.Controls.Add(this.txtFax);
            this.Controls.Add(this._lblFax);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this._lblEmail);
            this.Controls.Add(this.txtLName);
            this.Controls.Add(this._lblLName);
            this.Controls.Add(this._lblPhone);
            this.Controls.Add(this.txtFName);
            this.Controls.Add(this._lblFName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgContact";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Contact";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label _lblPhone;
        private System.Windows.Forms.TextBox txtFName;
        private System.Windows.Forms.Label _lblFName;
        private System.Windows.Forms.TextBox txtLName;
        private System.Windows.Forms.Label _lblLName;
        private System.Windows.Forms.Label _lblEmail;
        private System.Windows.Forms.MaskedTextBox txtPhone;
        private System.Windows.Forms.MaskedTextBox txtEmail;
        private System.Windows.Forms.MaskedTextBox txtFax;
        private System.Windows.Forms.Label _lblFax;
        private System.Windows.Forms.MaskedTextBox txtMobile;
        private System.Windows.Forms.Label _lblMobile;
    }
}