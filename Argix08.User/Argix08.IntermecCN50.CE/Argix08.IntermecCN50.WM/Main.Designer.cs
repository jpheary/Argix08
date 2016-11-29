namespace Argix {
    partial class Main {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.textBoxAltitude = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLongitude = new System.Windows.Forms.TextBox();
            this.textBoxLatitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.mainMenu2 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxAltitude
            // 
            this.textBoxAltitude.Location = new System.Drawing.Point(5,137);
            this.textBoxAltitude.Name = "textBoxAltitude";
            this.textBoxAltitude.Size = new System.Drawing.Size(147,21);
            this.textBoxAltitude.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(5,120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100,20);
            this.label4.Text = "Altitude";
            // 
            // textBoxTime
            // 
            this.textBoxTime.Location = new System.Drawing.Point(5,190);
            this.textBoxTime.Name = "textBoxTime";
            this.textBoxTime.Size = new System.Drawing.Size(147,21);
            this.textBoxTime.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(5,173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142,20);
            this.label3.Text = "Location Data Time";
            // 
            // textBoxLongitude
            // 
            this.textBoxLongitude.Location = new System.Drawing.Point(5,84);
            this.textBoxLongitude.Name = "textBoxLongitude";
            this.textBoxLongitude.Size = new System.Drawing.Size(147,21);
            this.textBoxLongitude.TabIndex = 16;
            this.textBoxLongitude.Text = " ";
            // 
            // textBoxLatitude
            // 
            this.textBoxLatitude.Location = new System.Drawing.Point(5,31);
            this.textBoxLatitude.Name = "textBoxLatitude";
            this.textBoxLatitude.Size = new System.Drawing.Size(147,21);
            this.textBoxLatitude.TabIndex = 15;
            this.textBoxLatitude.Text = " ";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(5,66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63,20);
            this.label2.Text = "Longitude";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(5,13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63,20);
            this.label1.Text = "Latitude";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(82,227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(77,28);
            this.button1.TabIndex = 14;
            this.button1.Text = "Update";
            this.button1.Click += new System.EventHandler(this.OnUpdateLocation);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F,96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(240,268);
            this.Controls.Add(this.textBoxAltitude);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxLongitude);
            this.Controls.Add(this.textBoxLatitude);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Menu = this.mainMenu1;
            this.MinimizeBox = false;
            this.Name = "Main";
            this.Text = "GPS Location";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxAltitude;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxLongitude;
        private System.Windows.Forms.TextBox textBoxLatitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MainMenu mainMenu2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

