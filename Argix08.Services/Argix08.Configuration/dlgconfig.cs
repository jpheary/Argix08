using System;
using System.Data;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argix.Configuration {
    /// <summary>Provides management of application configuration settings.</summary>
	public class dlgConfig : System.Windows.Forms.Form {
		//Members
		private ConfigGrid grdConfig;
		private System.ComponentModel.Container components = null;
		
		//Interface
        /// <summary>Constructor</summary>
        /// <param name="appConfigs">Sets a Argix.Configuration.AppConfigFactory object in the dialogs Argix.Configuration.ConfigGrid::Configuration control member.</param>
        public dlgConfig(AppConfigFactory appConfigs) {
			//Constructor
			try {
				//
				InitializeComponent();
				this.grdConfig.Configuration = appConfigs;
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgConfig instance.", ex); }
		}
        /// <summary>Disposes of the resources (other than memory) used by the System.Windows.Forms.Form.</summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.grdConfig = new Argix.Configuration.ConfigGrid();
			this.SuspendLayout();
			// 
			// grdConfig
			// 
			this.grdConfig.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdConfig.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdConfig.Location = new System.Drawing.Point(0, 0);
			this.grdConfig.Name = "grdConfig";
			this.grdConfig.Size = new System.Drawing.Size(289, 368);
			this.grdConfig.TabIndex = 0;
			this.grdConfig.Configuration = null;
			// 
			// dlgConfig
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(289, 368);
			this.Controls.Add(this.grdConfig);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
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
		}
	}
}
