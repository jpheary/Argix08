namespace Argix.Enterprise {
    partial class ContactComboBox {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this._lblContact = new System.Windows.Forms.Label();
            this.cboContact = new System.Windows.Forms.ComboBox();
            this.btnContact = new System.Windows.Forms.Button();
            this.lblPhone = new System.Windows.Forms.Label();
            this.ctxControl = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxPhone = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxMobile = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxFax = new System.Windows.Forms.ToolStripMenuItem();
            this.lnkEmail = new System.Windows.Forms.LinkLabel();
            this.ctxControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblContact
            // 
            this._lblContact.Location = new System.Drawing.Point(3,6);
            this._lblContact.Name = "_lblContact";
            this._lblContact.Size = new System.Drawing.Size(72,17);
            this._lblContact.TabIndex = 8;
            this._lblContact.Text = "Contact";
            this._lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboContact
            // 
            this.cboContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboContact.DisplayMember = "IssueContactTable.FullName";
            this.cboContact.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboContact.FormattingEnabled = true;
            this.cboContact.Location = new System.Drawing.Point(82,6);
            this.cboContact.Margin = new System.Windows.Forms.Padding(2);
            this.cboContact.Name = "cboContact";
            this.cboContact.Size = new System.Drawing.Size(192,21);
            this.cboContact.TabIndex = 0;
            this.cboContact.ValueMember = "IssueContactTable.ID";
            this.cboContact.SelectionChangeCommitted += new System.EventHandler(this.OnContactChanged);
            this.cboContact.TextChanged += new System.EventHandler(this.OnContactTextChanged);
            // 
            // btnContact
            // 
            this.btnContact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnContact.Location = new System.Drawing.Point(278,6);
            this.btnContact.Margin = new System.Windows.Forms.Padding(2);
            this.btnContact.Name = "btnContact";
            this.btnContact.Size = new System.Drawing.Size(26,21);
            this.btnContact.TabIndex = 1;
            this.btnContact.Text = "...";
            this.btnContact.UseVisualStyleBackColor = true;
            this.btnContact.Click += new System.EventHandler(this.OnNewContact);
            // 
            // lblPhone
            // 
            this.lblPhone.ContextMenuStrip = this.ctxControl;
            this.lblPhone.Location = new System.Drawing.Point(79,32);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(93,16);
            this.lblPhone.TabIndex = 15;
            this.lblPhone.Text = "p (732) 656-2599";
            this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctxControl
            // 
            this.ctxControl.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxPhone,
            this.ctxMobile,
            this.ctxFax});
            this.ctxControl.Name = "ctxControl";
            this.ctxControl.Size = new System.Drawing.Size(112,70);
            // 
            // ctxPhone
            // 
            this.ctxPhone.Checked = true;
            this.ctxPhone.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ctxPhone.Name = "ctxPhone";
            this.ctxPhone.Size = new System.Drawing.Size(111,22);
            this.ctxPhone.Text = "Phone";
            this.ctxPhone.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxMobile
            // 
            this.ctxMobile.Name = "ctxMobile";
            this.ctxMobile.Size = new System.Drawing.Size(111,22);
            this.ctxMobile.Text = "Mobile";
            this.ctxMobile.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // ctxFax
            // 
            this.ctxFax.Name = "ctxFax";
            this.ctxFax.Size = new System.Drawing.Size(111,22);
            this.ctxFax.Text = "Fax";
            this.ctxFax.Click += new System.EventHandler(this.OnMenuClick);
            // 
            // lnkEmail
            // 
            this.lnkEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lnkEmail.Location = new System.Drawing.Point(177,32);
            this.lnkEmail.Margin = new System.Windows.Forms.Padding(2,0,2,0);
            this.lnkEmail.Name = "lnkEmail";
            this.lnkEmail.Size = new System.Drawing.Size(126,16);
            this.lnkEmail.TabIndex = 17;
            this.lnkEmail.TabStop = true;
            this.lnkEmail.Text = "jheary@argixdirect.com";
            this.lnkEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lnkEmail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnLinkClicked);
            // 
            // ContactComboBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboContact);
            this.Controls.Add(this.lnkEmail);
            this.Controls.Add(this._lblContact);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.btnContact);
            this.Name = "ContactComboBox";
            this.Padding = new System.Windows.Forms.Padding(6);
            this.Size = new System.Drawing.Size(312,48);
            this.Load += new System.EventHandler(this.OnControlLoad);
            this.ctxControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label _lblContact;
        private System.Windows.Forms.Button btnContact;
        private System.Windows.Forms.ComboBox cboContact;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.LinkLabel lnkEmail;
        private System.Windows.Forms.ContextMenuStrip ctxControl;
        private System.Windows.Forms.ToolStripMenuItem ctxPhone;
        private System.Windows.Forms.ToolStripMenuItem ctxMobile;
        private System.Windows.Forms.ToolStripMenuItem ctxFax;
    }
}
