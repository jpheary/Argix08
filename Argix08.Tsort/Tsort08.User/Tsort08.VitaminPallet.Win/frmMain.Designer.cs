namespace Argix.Freight
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.lblPalletID = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnNewPallet = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnTotePalletPrint = new System.Windows.Forms.Button();
            this.btnFind = new System.Windows.Forms.Button();
            this.lblTotePalletID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtToteNumber = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(153, 83);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Pallet ID:";
            // 
            // lblPalletID
            // 
            this.lblPalletID.AutoSize = true;
            this.lblPalletID.BackColor = System.Drawing.SystemColors.Control;
            this.lblPalletID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblPalletID.Location = new System.Drawing.Point(271, 80);
            this.lblPalletID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPalletID.Name = "lblPalletID";
            this.lblPalletID.Size = new System.Drawing.Size(71, 22);
            this.lblPalletID.TabIndex = 1;
            this.lblPalletID.Text = "Pallet ID";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(417, 80);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnNewPallet
            // 
            this.btnNewPallet.Location = new System.Drawing.Point(6, 37);
            this.btnNewPallet.Name = "btnNewPallet";
            this.btnNewPallet.Size = new System.Drawing.Size(75, 66);
            this.btnNewPallet.TabIndex = 3;
            this.btnNewPallet.Text = "New Pallet";
            this.btnNewPallet.UseVisualStyleBackColor = true;
            this.btnNewPallet.Click += new System.EventHandler(this.btnNewPallet_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(531, 168);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btnNewPallet);
            this.tabPage1.Controls.Add(this.btnPrint);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lblPalletID);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(523, 135);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Current Pallet";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnTotePalletPrint);
            this.tabPage2.Controls.Add(this.btnFind);
            this.tabPage2.Controls.Add(this.lblTotePalletID);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.txtToteNumber);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(523, 135);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Tote Pallet";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnTotePalletPrint
            // 
            this.btnTotePalletPrint.Location = new System.Drawing.Point(285, 67);
            this.btnTotePalletPrint.Name = "btnTotePalletPrint";
            this.btnTotePalletPrint.Size = new System.Drawing.Size(75, 25);
            this.btnTotePalletPrint.TabIndex = 5;
            this.btnTotePalletPrint.Text = "Print";
            this.btnTotePalletPrint.UseVisualStyleBackColor = true;
            this.btnTotePalletPrint.Click += new System.EventHandler(this.btnTotePalletPrint_Click);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(442, 15);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 26);
            this.btnFind.TabIndex = 4;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // lblTotePalletID
            // 
            this.lblTotePalletID.BackColor = System.Drawing.SystemColors.Control;
            this.lblTotePalletID.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTotePalletID.Location = new System.Drawing.Point(120, 67);
            this.lblTotePalletID.Name = "lblTotePalletID";
            this.lblTotePalletID.Size = new System.Drawing.Size(103, 22);
            this.lblTotePalletID.TabIndex = 3;
            this.lblTotePalletID.Text = "                       ";
            this.lblTotePalletID.TextChanged += new System.EventHandler(this.lblTotePalletID_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pallet ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tote Number:";
            // 
            // txtToteNumber
            // 
            this.txtToteNumber.Location = new System.Drawing.Point(120, 15);
            this.txtToteNumber.Name = "txtToteNumber";
            this.txtToteNumber.Size = new System.Drawing.Size(309, 26);
            this.txtToteNumber.TabIndex = 0;
            this.txtToteNumber.Enter += new System.EventHandler(this.txtToteNumber_Enter);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 186);
            this.Controls.Add(this.tabControl1);
            this.Enabled = false;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmMain";
            this.Text = "Vitamin Tote Pallet 3.5.0.1";
            this.Shown += new System.EventHandler(this.frmMain_Shown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPalletID;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnNewPallet;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtToteNumber;
        private System.Windows.Forms.Button btnTotePalletPrint;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label lblTotePalletID;
        private System.Windows.Forms.Label label3;
    }
}

