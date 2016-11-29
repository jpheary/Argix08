namespace Argix.AgentLineHaul {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this._lblTerminal = new System.Windows.Forms.Label();
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this._lblSource = new System.Windows.Forms.Label();
            this._lblDestination = new System.Windows.Forms.Label();
            this._lblPattern = new System.Windows.Forms.Label();
            this.lblPattern = new System.Windows.Forms.Label();
            this.lblDestination = new System.Windows.Forms.Label();
            this.lblSource = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this._lblLog = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _lblTerminal
            // 
            this._lblTerminal.Location = new System.Drawing.Point(12,9);
            this._lblTerminal.Name = "_lblTerminal";
            this._lblTerminal.Size = new System.Drawing.Size(73,23);
            this._lblTerminal.TabIndex = 0;
            this._lblTerminal.Text = "Terminal";
            this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTerminal
            // 
            this.cboTerminal.DisplayMember = "Name";
            this.cboTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminal.FormattingEnabled = true;
            this.cboTerminal.Location = new System.Drawing.Point(91,9);
            this.cboTerminal.Name = "cboTerminal";
            this.cboTerminal.Size = new System.Drawing.Size(192,21);
            this.cboTerminal.TabIndex = 1;
            this.cboTerminal.SelectionChangeCommitted += new System.EventHandler(this.OnTerminalChanged);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(580,40);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75,23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(580,7);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75,23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "&Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // _lblSource
            // 
            this._lblSource.Location = new System.Drawing.Point(12,40);
            this._lblSource.Name = "_lblSource";
            this._lblSource.Size = new System.Drawing.Size(73,23);
            this._lblSource.TabIndex = 4;
            this._lblSource.Text = "Source";
            this._lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDestination
            // 
            this._lblDestination.Location = new System.Drawing.Point(12,75);
            this._lblDestination.Name = "_lblDestination";
            this._lblDestination.Size = new System.Drawing.Size(73,23);
            this._lblDestination.TabIndex = 5;
            this._lblDestination.Text = "Destination";
            this._lblDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblPattern
            // 
            this._lblPattern.Location = new System.Drawing.Point(316,40);
            this._lblPattern.Name = "_lblPattern";
            this._lblPattern.Size = new System.Drawing.Size(73,23);
            this._lblPattern.TabIndex = 6;
            this._lblPattern.Text = "Pattern";
            this._lblPattern.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPattern
            // 
            this.lblPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPattern.Location = new System.Drawing.Point(395,40);
            this.lblPattern.Name = "lblPattern";
            this.lblPattern.Size = new System.Drawing.Size(136,23);
            this.lblPattern.TabIndex = 9;
            this.lblPattern.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDestination
            // 
            this.lblDestination.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDestination.Location = new System.Drawing.Point(91,75);
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.Size = new System.Drawing.Size(217,23);
            this.lblDestination.TabIndex = 8;
            this.lblDestination.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSource
            // 
            this.lblSource.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSource.Location = new System.Drawing.Point(91,40);
            this.lblSource.Name = "lblSource";
            this.lblSource.Size = new System.Drawing.Size(217,23);
            this.lblSource.TabIndex = 7;
            this.lblSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Location = new System.Drawing.Point(91,113);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(564,172);
            this.txtLog.TabIndex = 10;
            this.txtLog.WordWrap = false;
            // 
            // _lblLog
            // 
            this._lblLog.Location = new System.Drawing.Point(12,113);
            this._lblLog.Name = "_lblLog";
            this._lblLog.Size = new System.Drawing.Size(73,23);
            this._lblLog.TabIndex = 11;
            this._lblLog.Text = "Log";
            this._lblLog.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667,297);
            this.Controls.Add(this._lblLog);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblPattern);
            this.Controls.Add(this.lblDestination);
            this.Controls.Add(this.lblSource);
            this.Controls.Add(this._lblPattern);
            this.Controls.Add(this._lblDestination);
            this.Controls.Add(this._lblSource);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cboTerminal);
            this.Controls.Add(this._lblTerminal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bearware Route Import";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lblTerminal;
        private System.Windows.Forms.ComboBox cboTerminal;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label _lblSource;
        private System.Windows.Forms.Label _lblDestination;
        private System.Windows.Forms.Label _lblPattern;
        private System.Windows.Forms.Label lblPattern;
        private System.Windows.Forms.Label lblDestination;
        private System.Windows.Forms.Label lblSource;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label _lblLog;
    }
}

