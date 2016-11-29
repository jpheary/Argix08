namespace Tsort.Sort {
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
            this.txtInput1 = new System.Windows.Forms.TextBox();
            this.txtInput2 = new System.Windows.Forms.TextBox();
            this.txtInput3 = new System.Windows.Forms.TextBox();
            this.txtInput5 = new System.Windows.Forms.TextBox();
            this.txtInput4 = new System.Windows.Forms.TextBox();
            this.txtInput6 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.cboInputCount = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.lblPrinterStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblScaleStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.sortUI1 = new Tsort.Sort.SortUI();
            this.stbMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtInput1
            // 
            this.txtInput1.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtInput1.Location = new System.Drawing.Point(57,12);
            this.txtInput1.Name = "txtInput1";
            this.txtInput1.Size = new System.Drawing.Size(163,26);
            this.txtInput1.TabIndex = 0;
            this.txtInput1.TextChanged += new System.EventHandler(this.OnInputChanged);
            // 
            // txtInput2
            // 
            this.txtInput2.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtInput2.Location = new System.Drawing.Point(57,44);
            this.txtInput2.Name = "txtInput2";
            this.txtInput2.Size = new System.Drawing.Size(163,26);
            this.txtInput2.TabIndex = 1;
            this.txtInput2.TextChanged += new System.EventHandler(this.OnInputChanged);
            // 
            // txtInput3
            // 
            this.txtInput3.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtInput3.Location = new System.Drawing.Point(57,76);
            this.txtInput3.Name = "txtInput3";
            this.txtInput3.Size = new System.Drawing.Size(163,26);
            this.txtInput3.TabIndex = 2;
            this.txtInput3.TextChanged += new System.EventHandler(this.OnInputChanged);
            // 
            // txtInput5
            // 
            this.txtInput5.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtInput5.Location = new System.Drawing.Point(286,44);
            this.txtInput5.Name = "txtInput5";
            this.txtInput5.Size = new System.Drawing.Size(163,26);
            this.txtInput5.TabIndex = 3;
            this.txtInput5.TextChanged += new System.EventHandler(this.OnInputChanged);
            // 
            // txtInput4
            // 
            this.txtInput4.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtInput4.Location = new System.Drawing.Point(286,12);
            this.txtInput4.Name = "txtInput4";
            this.txtInput4.Size = new System.Drawing.Size(163,26);
            this.txtInput4.TabIndex = 4;
            this.txtInput4.TextChanged += new System.EventHandler(this.OnInputChanged);
            // 
            // txtInput6
            // 
            this.txtInput6.Font = new System.Drawing.Font("Microsoft Sans Serif",12F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtInput6.Location = new System.Drawing.Point(286,76);
            this.txtInput6.Name = "txtInput6";
            this.txtInput6.Size = new System.Drawing.Size(163,26);
            this.txtInput6.TabIndex = 5;
            this.txtInput6.TextChanged += new System.EventHandler(this.OnInputChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12,12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40,17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Input 1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.UseCompatibleTextRendering = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12,44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40,17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Input 2";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label2.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12,76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40,17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Input 3";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.UseCompatibleTextRendering = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.label4.Location = new System.Drawing.Point(241,12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40,17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Input 4";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label4.UseCompatibleTextRendering = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.label5.Location = new System.Drawing.Point(241,44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40,17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Input 5";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label5.UseCompatibleTextRendering = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.label6.Location = new System.Drawing.Point(241,76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40,17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Input 6";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label6.UseCompatibleTextRendering = true;
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.txtWeight.Font = new System.Drawing.Font("Microsoft Sans Serif",24F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.txtWeight.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtWeight.Location = new System.Drawing.Point(468,12);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(96,44);
            this.txtWeight.TabIndex = 12;
            this.txtWeight.Text = "999.9";
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.label7.Location = new System.Drawing.Point(494,59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41,17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Weight";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.UseCompatibleTextRendering = true;
            // 
            // stbMain
            // 
            this.stbMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.cboInputCount,
            this.lblPrinterStatus,
            this.lblScaleStatus});
            this.stbMain.Location = new System.Drawing.Point(0,401);
            this.stbMain.Name = "stbMain";
            this.stbMain.ShowItemToolTips = true;
            this.stbMain.Size = new System.Drawing.Size(576,24);
            this.stbMain.SizingGrip = false;
            this.stbMain.TabIndex = 14;
            // 
            // lblStatus
            // 
            this.lblStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(434,19);
            this.lblStatus.Spring = true;
            this.lblStatus.ToolTipText = "Status";
            // 
            // cboInputCount
            // 
            this.cboInputCount.AutoSize = false;
            this.cboInputCount.AutoToolTip = false;
            this.cboInputCount.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.cboInputCount.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItem1,
            this.tsmItem2,
            this.tsmItem3,
            this.tsmItem4,
            this.tsmItem5,
            this.tsmItem6});
            this.cboInputCount.Image = ((System.Drawing.Image)(resources.GetObject("cboInputCount.Image")));
            this.cboInputCount.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cboInputCount.Name = "cboInputCount";
            this.cboInputCount.Size = new System.Drawing.Size(48,22);
            this.cboInputCount.Text = "1";
            this.cboInputCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.cboInputCount.ToolTipText = "Input count";
            // 
            // tsmItem1
            // 
            this.tsmItem1.Name = "tsmItem1";
            this.tsmItem1.Size = new System.Drawing.Size(80,22);
            this.tsmItem1.Text = "1";
            this.tsmItem1.Click += new System.EventHandler(this.OnInputSelected);
            // 
            // tsmItem2
            // 
            this.tsmItem2.Name = "tsmItem2";
            this.tsmItem2.Size = new System.Drawing.Size(80,22);
            this.tsmItem2.Text = "2";
            this.tsmItem2.Click += new System.EventHandler(this.OnInputSelected);
            // 
            // tsmItem3
            // 
            this.tsmItem3.Name = "tsmItem3";
            this.tsmItem3.Size = new System.Drawing.Size(80,22);
            this.tsmItem3.Text = "3";
            this.tsmItem3.Click += new System.EventHandler(this.OnInputSelected);
            // 
            // tsmItem4
            // 
            this.tsmItem4.Name = "tsmItem4";
            this.tsmItem4.Size = new System.Drawing.Size(80,22);
            this.tsmItem4.Text = "4";
            this.tsmItem4.Click += new System.EventHandler(this.OnInputSelected);
            // 
            // tsmItem5
            // 
            this.tsmItem5.Name = "tsmItem5";
            this.tsmItem5.Size = new System.Drawing.Size(80,22);
            this.tsmItem5.Text = "5";
            this.tsmItem5.Click += new System.EventHandler(this.OnInputSelected);
            // 
            // tsmItem6
            // 
            this.tsmItem6.Name = "tsmItem6";
            this.tsmItem6.Size = new System.Drawing.Size(80,22);
            this.tsmItem6.Text = "6";
            this.tsmItem6.Click += new System.EventHandler(this.OnInputSelected);
            // 
            // lblPrinterStatus
            // 
            this.lblPrinterStatus.AutoSize = false;
            this.lblPrinterStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblPrinterStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lblPrinterStatus.Name = "lblPrinterStatus";
            this.lblPrinterStatus.Size = new System.Drawing.Size(24,19);
            this.lblPrinterStatus.ToolTipText = "Printer Off";
            this.lblPrinterStatus.Click += new System.EventHandler(this.OnPrinterClick);
            // 
            // lblScaleStatus
            // 
            this.lblScaleStatus.AutoSize = false;
            this.lblScaleStatus.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblScaleStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.lblScaleStatus.Name = "lblScaleStatus";
            this.lblScaleStatus.Size = new System.Drawing.Size(24,19);
            this.lblScaleStatus.ToolTipText = "Scale Off";
            // 
            // sortUI1
            // 
            this.sortUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sortUI1.Location = new System.Drawing.Point(0,108);
            this.sortUI1.Name = "sortUI1";
            this.sortUI1.Operator = null;
            this.sortUI1.RefreshCacheVisible = true;
            this.sortUI1.RefreshVisible = true;
            this.sortUI1.Size = new System.Drawing.Size(576,288);
            this.sortUI1.TabIndex = 15;
            this.sortUI1.TraceOn = Argix.LogLevel.Debug;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576,425);
            this.Controls.Add(this.sortUI1);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInput6);
            this.Controls.Add(this.txtInput4);
            this.Controls.Add(this.txtInput5);
            this.Controls.Add(this.txtInput3);
            this.Controls.Add(this.txtInput2);
            this.Controls.Add(this.txtInput1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Station Program";
            this.Load += new System.EventHandler(this.OnLoad);
            this.stbMain.ResumeLayout(false);
            this.stbMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInput1;
        private System.Windows.Forms.TextBox txtInput2;
        private System.Windows.Forms.TextBox txtInput3;
        private System.Windows.Forms.TextBox txtInput5;
        private System.Windows.Forms.TextBox txtInput4;
        private System.Windows.Forms.TextBox txtInput6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.StatusStrip stbMain;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblPrinterStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblScaleStatus;
        private System.Windows.Forms.ToolStripDropDownButton cboInputCount;
        private System.Windows.Forms.ToolStripMenuItem tsmItem1;
        private System.Windows.Forms.ToolStripMenuItem tsmItem2;
        private System.Windows.Forms.ToolStripMenuItem tsmItem3;
        private System.Windows.Forms.ToolStripMenuItem tsmItem4;
        private System.Windows.Forms.ToolStripMenuItem tsmItem5;
        private System.Windows.Forms.ToolStripMenuItem tsmItem6;
        private SortUI sortUI1;
    }
}