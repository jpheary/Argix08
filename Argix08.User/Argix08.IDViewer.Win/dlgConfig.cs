using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argix.Support {
    //Provides management of application configuration settings
	public class dlgConfig : System.Windows.Forms.Form {
		//Members
        private Button btnRefresh;
        private ComboBox cboUsers;
        private PropertyGrid grdConfig;
        private System.ComponentModel.Container components = null;
		
		//Interface
        public dlgConfig(object configuration) {
			//Constructor
			InitializeComponent();
            this.cboUsers.Items.Add(Environment.UserName);
            this.grdConfig.SelectedObject = configuration;
		}
        protected override void Dispose(bool disposing) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgConfig));
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cboUsers = new System.Windows.Forms.ComboBox();
            this.grdConfig = new System.Windows.Forms.PropertyGrid();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.Location = new System.Drawing.Point(267,1);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(21,21);
            this.btnRefresh.TabIndex = 6;
            // 
            // cboUsers
            // 
            this.cboUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboUsers.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cboUsers.Location = new System.Drawing.Point(0,1);
            this.cboUsers.Name = "cboUsers";
            this.cboUsers.Size = new System.Drawing.Size(261,21);
            this.cboUsers.TabIndex = 5;
            // 
            // grdConfig
            // 
            this.grdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdConfig.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.grdConfig.LineColor = System.Drawing.SystemColors.ScrollBar;
            this.grdConfig.Location = new System.Drawing.Point(0,28);
            this.grdConfig.Name = "grdConfig";
            this.grdConfig.Size = new System.Drawing.Size(288,339);
            this.grdConfig.TabIndex = 3;
            // 
            // dlgConfig
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(289,368);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grdConfig);
            this.Controls.Add(this.cboUsers);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgConfig";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
            this.cboUsers.Sorted = true;
            this.cboUsers.SelectedIndex = 0;
            this.btnRefresh.Enabled = true;
        }
        private void OnUserChanged(object sender,System.EventArgs e) {
            //Event handler for user changed
        }
        private void OnRefresh(object sender,System.EventArgs e) {
            //Event handler for refresh button clicked
        }
        private void OnValueChanged(object s,System.Windows.Forms.PropertyValueChangedEventArgs e) {
            //Event handler for property value changed
        }
    }
}
