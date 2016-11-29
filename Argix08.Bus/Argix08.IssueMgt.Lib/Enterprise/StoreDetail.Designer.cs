namespace Argix.Enterprise {
    partial class StoreDetail {
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
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtDetail
            // 
            this.txtDetail.BackColor = System.Drawing.SystemColors.Window;
            this.txtDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDetail.HideSelection = false;
            this.txtDetail.Location = new System.Drawing.Point(1,1);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.ReadOnly = true;
            this.txtDetail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtDetail.Size = new System.Drawing.Size(382,142);
            this.txtDetail.TabIndex = 0;
            this.txtDetail.WordWrap = false;
            // 
            // StoreDetail
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.txtDetail);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.Name = "StoreDetail";
            this.Size = new System.Drawing.Size(384,144);
            this.Load += new System.EventHandler(this.OnControlLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtDetail;

    }
}
