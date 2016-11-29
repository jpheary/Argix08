namespace Argix.Customers {
    partial class dlgIssue {
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
            this.cboIssueType = new System.Windows.Forms.ComboBox();
            this._lblType = new System.Windows.Forms.Label();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this._lblSubject = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.ctlCompLoc = new Argix.Customers.CompanyLocation();
            this.ctlContact = new Argix.Customers.ContactComboBox();
            this.cboIssueCategory = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cboIssueType
            // 
            this.cboIssueType.DisplayMember = "IssueTypeTable.Type";
            this.cboIssueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIssueType.FormattingEnabled = true;
            this.cboIssueType.Location = new System.Drawing.Point(184,266);
            this.cboIssueType.Margin = new System.Windows.Forms.Padding(2,3,2,3);
            this.cboIssueType.Name = "cboIssueType";
            this.cboIssueType.Size = new System.Drawing.Size(124,21);
            this.cboIssueType.TabIndex = 5;
            this.cboIssueType.ValueMember = "IssueTypeTable.ID";
            this.cboIssueType.SelectionChangeCommitted += new System.EventHandler(this.OnIssueTypeSelected);
            // 
            // _lblType
            // 
            this._lblType.Location = new System.Drawing.Point(22,266);
            this._lblType.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(58,18);
            this._lblType.TabIndex = 4;
            this._lblType.Text = "Type";
            this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Location = new System.Drawing.Point(84,345);
            this.txtTitle.Margin = new System.Windows.Forms.Padding(4);
            this.txtTitle.MaxLength = 200;
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(386,20);
            this.txtTitle.TabIndex = 6;
            this.txtTitle.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblSubject
            // 
            this._lblSubject.Location = new System.Drawing.Point(22,344);
            this._lblSubject.Margin = new System.Windows.Forms.Padding(4,0,4,0);
            this._lblSubject.Name = "_lblSubject";
            this._lblSubject.Size = new System.Drawing.Size(58,18);
            this._lblSubject.TabIndex = 2;
            this._lblSubject.Text = "Subject";
            this._lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(389,373);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2,3,2,3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82,24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(303,373);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2,3,2,3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82,24);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // ctlCompLoc
            // 
            this.ctlCompLoc.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctlCompLoc.Location = new System.Drawing.Point(3,3);
            this.ctlCompLoc.Margin = new System.Windows.Forms.Padding(2,3,2,3);
            this.ctlCompLoc.Name = "ctlCompLoc";
            this.ctlCompLoc.Padding = new System.Windows.Forms.Padding(6);
            this.ctlCompLoc.ReadOnly = false;
            this.ctlCompLoc.Size = new System.Drawing.Size(467,257);
            this.ctlCompLoc.TabIndex = 2;
            this.ctlCompLoc.CompanyLocationChanged += new System.EventHandler(this.OnLocationChanged);
            this.ctlCompLoc.Error += new Argix.ControlErrorEventHandler(this.OnControlError);
            // 
            // ctlContact
            // 
            this.ctlContact.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ctlContact.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctlContact.DataSource = null;
            this.ctlContact.DisplayMember = "ContactTable.FullName";
            this.ctlContact.Location = new System.Drawing.Point(3,292);
            this.ctlContact.Margin = new System.Windows.Forms.Padding(4);
            this.ctlContact.Name = "ctlContact";
            this.ctlContact.Padding = new System.Windows.Forms.Padding(5,6,5,6);
            this.ctlContact.Size = new System.Drawing.Size(344,47);
            this.ctlContact.TabIndex = 4;
            this.ctlContact.ValueMember = "ContactTable.ID";
            this.ctlContact.ContactChanged += new Argix.Customers.ContactEventHandler(this.OnContactChanged);
            this.ctlContact.BeforeContactCreated += new Argix.Customers.ContactEventHandler(this.OnBeforeContactCreated);
            this.ctlContact.AfterContactCreated += new Argix.Customers.ContactEventHandler(this.OnAfterContactCreated);
            this.ctlContact.Error += new Argix.ControlErrorEventHandler(this.OnControlError);
            // 
            // cboIssueCategory
            // 
            this.cboIssueCategory.DisplayMember = "IssueTypeTable.Category";
            this.cboIssueCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIssueCategory.FormattingEnabled = true;
            this.cboIssueCategory.Location = new System.Drawing.Point(84,266);
            this.cboIssueCategory.Margin = new System.Windows.Forms.Padding(2,3,2,3);
            this.cboIssueCategory.Name = "cboIssueCategory";
            this.cboIssueCategory.Size = new System.Drawing.Size(96,21);
            this.cboIssueCategory.TabIndex = 7;
            this.cboIssueCategory.ValueMember = "IssueTypeTable.ID";
            this.cboIssueCategory.SelectionChangeCommitted += new System.EventHandler(this.OnIssueCategorySelected);
            // 
            // dlgIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476,403);
            this.Controls.Add(this.cboIssueCategory);
            this.Controls.Add(this.ctlCompLoc);
            this.Controls.Add(this.ctlContact);
            this.Controls.Add(this.cboIssueType);
            this.Controls.Add(this._lblType);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this._lblSubject);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2,3,2,3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgIssue";
            this.Padding = new System.Windows.Forms.Padding(2,3,2,3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Issue";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboIssueType;
        private System.Windows.Forms.Label _lblType;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label _lblSubject;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Argix.Customers.ContactComboBox ctlContact;
        private Argix.Customers.CompanyLocation ctlCompLoc;
        private System.Windows.Forms.ComboBox cboIssueCategory;

    }
}