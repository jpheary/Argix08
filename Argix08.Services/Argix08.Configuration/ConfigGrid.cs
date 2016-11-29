using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Argix.Configuration {
	/// <summary>Configuration grid control</summary>
	public class ConfigGrid : System.Windows.Forms.UserControl {
		//Members
		private AppConfigFactory mAppConfigs=null;
		#region Controls
		private System.Windows.Forms.PropertyGrid grdConfig;
		private System.Windows.Forms.ComboBox cboUsers;
		private System.Windows.Forms.Button btnRefresh;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
        /// <summary>Constructor</summary>
		public ConfigGrid() {
			//Constructor
			try {
				//
				InitializeComponent();
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new ConfigGrid instance.", ex); }
		}
		/// <summary></summary>
        /// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
        protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		/// <summary>Gets or sets the underlying Argix.Configuration.AppConfigFactory; refreshes configuration on set</summary>
        public AppConfigFactory Configuration { 
			get { return this.mAppConfigs; } 
			set { 
				if(value == null) return;
				this.mAppConfigs = value; 
				this.mAppConfigs.UsersChanged += new EventHandler(OnUsersChanged);
				this.mAppConfigs.Refresh();
			} 
		}
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConfigGrid));
			this.grdConfig = new System.Windows.Forms.PropertyGrid();
			this.cboUsers = new System.Windows.Forms.ComboBox();
			this.btnRefresh = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// grdConfig
			// 
			this.grdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grdConfig.CommandsVisibleIfAvailable = true;
			this.grdConfig.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdConfig.LargeButtons = false;
			this.grdConfig.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.grdConfig.Location = new System.Drawing.Point(0, 21);
			this.grdConfig.Name = "grdConfig";
			this.grdConfig.Size = new System.Drawing.Size(288, 363);
			this.grdConfig.TabIndex = 3;
			this.grdConfig.Text = "propertyGrid1";
			this.grdConfig.ViewBackColor = System.Drawing.SystemColors.Window;
			this.grdConfig.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.grdConfig.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.OnValueChanged);
			// 
			// cboUsers
			// 
			this.cboUsers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cboUsers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboUsers.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboUsers.Location = new System.Drawing.Point(0, 0);
			this.cboUsers.Name = "cboUsers";
			this.cboUsers.Size = new System.Drawing.Size(261, 21);
			this.cboUsers.TabIndex = 2;
			this.cboUsers.SelectionChangeCommitted += new System.EventHandler(this.OnUserChanged);
			// 
			// btnRefresh
			// 
			this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRefresh.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
			this.btnRefresh.Location = new System.Drawing.Point(267, 0);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(21, 21);
			this.btnRefresh.TabIndex = 4;
			this.btnRefresh.Click += new System.EventHandler(this.OnRefresh);
			// 
			// ConfigGrid
			// 
			this.Controls.Add(this.btnRefresh);
			this.Controls.Add(this.grdConfig);
			this.Controls.Add(this.cboUsers);
			this.Font = new System.Drawing.Font("Vrinda", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ConfigGrid";
			this.Size = new System.Drawing.Size(288, 384);
			this.Load += new System.EventHandler(this.OnControlLoad);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnControlLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				this.cboUsers.Sorted = true;
				this.btnRefresh.Enabled = true;
			} 
			catch(Exception ex) { throw ex; }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnUsersChanged(object sender, EventArgs e) {
			//Event handler for change in configuration users
			this.Cursor = Cursors.WaitCursor;
			try {
				//Re-populate user selections
				this.cboUsers.Items.Clear();
				foreach(DictionaryEntry user in this.mAppConfigs.Users) 
					this.cboUsers.Items.Add(user.Value);
				OnUserChanged(this.cboUsers, EventArgs.Empty);
			} 
			catch(Exception ex) { throw ex; }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnUserChanged(object sender, System.EventArgs e) {
			//Event handler for user changed
			this.Cursor = Cursors.WaitCursor;
			try {
				//Point grid at the selected user configuration object
				if(this.cboUsers.Text.Trim().Length > 0) {
					if(this.cboUsers.Text == AppConfigFactory.USER_NEW) {
						//New user: get a name and create a profile
						dlgInputBox ib = new dlgInputBox("Enter a username or machine name for the user.", Environment.UserName, "New User");
						ib.TopMost = true;
						ib.BringToFront();
						if(ib.ShowDialog() == DialogResult.OK) 
							this.grdConfig.SelectedObject = this.mAppConfigs.Add(ib.Value);
						else
							this.grdConfig.SelectedObject = null;
					}
					else
						this.grdConfig.SelectedObject = this.mAppConfigs.Item(this.cboUsers.Text);
				}
				else
					this.grdConfig.SelectedObject = null;
			} 
			catch(Exception ex) { throw ex; }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnRefresh(object sender, System.EventArgs e) {
			//Event handler for refresh button clicked
			this.mAppConfigs.Refresh();
		}
		private void OnValueChanged(object s, System.Windows.Forms.PropertyValueChangedEventArgs e) {
			//Event handler for property value changed
			Debug.Write("OnValueChanged()\n");
		}
	}
}
