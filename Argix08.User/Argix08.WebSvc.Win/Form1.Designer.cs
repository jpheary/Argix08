namespace WebSvcClient {
    partial class frmMain {
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
            this.txtCartonDetail = new System.Windows.Forms.TextBox();
            this._lblService = new System.Windows.Forms.Label();
            this.txtService = new System.Windows.Forms.TextBox();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this._lblUserID = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this._lblPassword = new System.Windows.Forms.Label();
            this.txtCartonNum = new System.Windows.Forms.TextBox();
            this._lblCartonNum = new System.Windows.Forms.Label();
            this.btnTrack = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtCartonDetail
            // 
            this.txtCartonDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCartonDetail.Location = new System.Drawing.Point(0,87);
            this.txtCartonDetail.Multiline = true;
            this.txtCartonDetail.Name = "txtCartonDetail";
            this.txtCartonDetail.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCartonDetail.Size = new System.Drawing.Size(568,263);
            this.txtCartonDetail.TabIndex = 0;
            // 
            // _lblService
            // 
            this._lblService.AutoSize = true;
            this._lblService.Location = new System.Drawing.Point(12,9);
            this._lblService.Name = "_lblService";
            this._lblService.Size = new System.Drawing.Size(69,13);
            this._lblService.TabIndex = 1;
            this._lblService.Text = "Web Service";
            // 
            // txtService
            // 
            this.txtService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtService.Location = new System.Drawing.Point(87,6);
            this.txtService.Name = "txtService";
            this.txtService.ReadOnly = true;
            this.txtService.Size = new System.Drawing.Size(469,20);
            this.txtService.TabIndex = 2;
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(87,32);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(120,20);
            this.txtUserID.TabIndex = 4;
            this.txtUserID.TextChanged += new System.EventHandler(this.OnValidate);
            // 
            // _lblUserID
            // 
            this._lblUserID.AutoSize = true;
            this._lblUserID.Location = new System.Drawing.Point(41,32);
            this._lblUserID.Name = "_lblUserID";
            this._lblUserID.Size = new System.Drawing.Size(40,13);
            this._lblUserID.TabIndex = 3;
            this._lblUserID.Text = "UserID";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(284,32);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(120,20);
            this.txtPassword.TabIndex = 6;
            this.txtPassword.TextChanged += new System.EventHandler(this.OnValidate);
            // 
            // _lblPassword
            // 
            this._lblPassword.AutoSize = true;
            this._lblPassword.Location = new System.Drawing.Point(225,32);
            this._lblPassword.Name = "_lblPassword";
            this._lblPassword.Size = new System.Drawing.Size(53,13);
            this._lblPassword.TabIndex = 5;
            this._lblPassword.Text = "Password";
            // 
            // txtCartonNum
            // 
            this.txtCartonNum.Location = new System.Drawing.Point(87,58);
            this.txtCartonNum.Name = "txtCartonNum";
            this.txtCartonNum.Size = new System.Drawing.Size(317,20);
            this.txtCartonNum.TabIndex = 8;
            this.txtCartonNum.TextChanged += new System.EventHandler(this.OnValidate);
            // 
            // _lblCartonNum
            // 
            this._lblCartonNum.AutoSize = true;
            this._lblCartonNum.Location = new System.Drawing.Point(36,58);
            this._lblCartonNum.Name = "_lblCartonNum";
            this._lblCartonNum.Size = new System.Drawing.Size(45,13);
            this._lblCartonNum.TabIndex = 7;
            this._lblCartonNum.Text = "Carton#";
            // 
            // btnTrack
            // 
            this.btnTrack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTrack.Location = new System.Drawing.Point(481,55);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(75,23);
            this.btnTrack.TabIndex = 9;
            this.btnTrack.Text = "Track";
            this.btnTrack.UseVisualStyleBackColor = true;
            this.btnTrack.Click += new System.EventHandler(this.OnTrack);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568,350);
            this.Controls.Add(this.btnTrack);
            this.Controls.Add(this.txtCartonNum);
            this.Controls.Add(this._lblCartonNum);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this._lblPassword);
            this.Controls.Add(this.txtUserID);
            this.Controls.Add(this._lblUserID);
            this.Controls.Add(this.txtService);
            this.Controls.Add(this._lblService);
            this.Controls.Add(this.txtCartonDetail);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tracking Web Service Client";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCartonDetail;
        private System.Windows.Forms.Label _lblService;
        private System.Windows.Forms.TextBox txtService;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.Label _lblUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label _lblPassword;
        private System.Windows.Forms.TextBox txtCartonNum;
        private System.Windows.Forms.Label _lblCartonNum;
        private System.Windows.Forms.Button btnTrack;
    }
}

