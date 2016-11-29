using System;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argix.Dispatch {
	//
	public class dlgMoveEntry : System.Windows.Forms.Form {
		//Members
		DispatchSchedule mSchedule=null;
		string mNodeName="";
		
		//Constants
		
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TreeView trvNav;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		public dlgMoveEntry() {
			//Required for Windows Form Designer support
			try {
				InitializeComponent();
			}
			catch(Exception) { }
		}
		protected override void Dispose( bool disposing ) {
			//Clean up any resources being used
			if( disposing ) {
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		public DispatchSchedule Schedule { get { return this.mSchedule; } }
		public string NodeName { get { return this.mNodeName; } }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgMoveEntry));
			this.trvNav = new System.Windows.Forms.TreeView();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// trvNav
			// 
			this.trvNav.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.trvNav.ImageIndex = -1;
			this.trvNav.Location = new System.Drawing.Point(6, 6);
			this.trvNav.Name = "trvNav";
			this.trvNav.SelectedImageIndex = -1;
			this.trvNav.Size = new System.Drawing.Size(147, 240);
			this.trvNav.TabIndex = 0;
			this.trvNav.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.OnNodeSelected);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(177, 42);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(177, 9);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 24);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "O&K";
			this.btnOK.Click += new System.EventHandler(this.OnButtonClick);
			// 
			// dlgMoveEntry
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(282, 256);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.trvNav);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgMoveEntry";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Move Entry";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Form load event handler
			try {
				TreeNode clone = (TreeNode)DispatchNode.ScheduleNodes.Clone();
				this.trvNav.Nodes.Add(clone);
				this.trvNav.Nodes[0].Expand();
			}
			catch(Exception ex) { Debug.Write(ex.Message + "\n"); }
			finally { this.btnOK.Enabled = false; }
		}
		private void OnButtonClick(object sender, System.EventArgs e) {
			//Button click event handler
			Button btn = (Button)sender;
			switch(btn.Text) {
				case "&Cancel":	this.DialogResult = DialogResult.Cancel; break;
				case "O&K":		this.DialogResult = DialogResult.OK; break;
			}
			this.Close();
		}
		private void OnNodeSelected(object sender, System.Windows.Forms.TreeViewEventArgs e) {
			//Tree node selected event handler
			try { 
				ScheduleNode node = (ScheduleNode)this.trvNav.SelectedNode;
				this.mSchedule = node.Schedule;
				this.mNodeName = node.Text;
			}
			catch(Exception) { }
			finally { OnValidateForm(null,null); }
		}
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Valiate state of user services
			this.btnOK.Enabled = (this.mSchedule != null || this.mNodeName != "");
		}
	}
}
