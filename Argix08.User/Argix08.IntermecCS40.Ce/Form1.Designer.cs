namespace Argix {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mnuMain;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            this.mnuMain = new System.Windows.Forms.MainMenu();
            this.mnuSnap = new System.Windows.Forms.MenuItem();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.txtComment = new System.Windows.Forms.TextBox();
            this._lblComment = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mnuMain
            // 
            this.mnuMain.MenuItems.Add(this.mnuSnap);
            // 
            // mnuSnap
            // 
            this.mnuSnap.Text = "Snap";
            this.mnuSnap.Click += new System.EventHandler(this.OnSnapClick);
            // 
            // picImage
            // 
            this.picImage.Dock = System.Windows.Forms.DockStyle.Top;
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(240, 210);
            // 
            // txtComment
            // 
            this.txtComment.Location = new System.Drawing.Point(3, 244);
            this.txtComment.Name = "txtComment";
            this.txtComment.Size = new System.Drawing.Size(234, 21);
            this.txtComment.TabIndex = 1;
            this.txtComment.TextChanged += new System.EventHandler(this.OnCommentChanged);
            // 
            // _lblComment
            // 
            this._lblComment.Location = new System.Drawing.Point(4, 217);
            this._lblComment.Name = "_lblComment";
            this._lblComment.Size = new System.Drawing.Size(115, 20);
            this._lblComment.Text = "Comment";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this._lblComment);
            this.Controls.Add(this.txtComment);
            this.Controls.Add(this.picImage);
            this.Menu = this.mnuMain;
            this.Name = "Form1";
            this.Text = "Camera SDK";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MenuItem mnuSnap;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.Label _lblComment;
    }
}

