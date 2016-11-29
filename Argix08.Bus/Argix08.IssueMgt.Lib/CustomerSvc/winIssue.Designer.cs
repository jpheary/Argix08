namespace Argix.CustomerSvc {
    partial class winIssue {
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
            this.issueInspector1 = new Argix.CustomerSvc.IssueInspector();
            this.SuspendLayout();
            // 
            // issueInspector1
            // 
            this.issueInspector1.Cursor = System.Windows.Forms.Cursors.Default;
            this.issueInspector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.issueInspector1.Location = new System.Drawing.Point(0,0);
            this.issueInspector1.Name = "issueInspector1";
            this.issueInspector1.Size = new System.Drawing.Size(848,444);
            this.issueInspector1.TabIndex = 0;
            this.issueInspector1.Error += new Argix.ControlErrorEventHandler(this.OnIssueInspError);
            // 
            // winIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848,444);
            this.Controls.Add(this.issueInspector1);
            this.Name = "winIssue";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "winIssue";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private Argix.CustomerSvc.IssueInspector issueInspector1;
    }
}