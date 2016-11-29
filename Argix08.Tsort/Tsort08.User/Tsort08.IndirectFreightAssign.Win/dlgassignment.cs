using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argix.Freight {
	//
	public class dlgAssignment : System.Windows.Forms.Form {
		//Members
		private WorkstationDS mUnassignedStations=null;
		private WorkstationDS mSelectedStations=null;
		
		#region Controls
		public System.Windows.Forms.ListBox lstStations;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		
		private System.ComponentModel.Container components = null;
		#endregion
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		//Interface
		public dlgAssignment(WorkstationDS selectedStations) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				#region Set command button identities (used for onclick handler)
				this.btnCancel.Text = CMD_CANCEL;
				this.btnOK.Text = CMD_OK;
				#endregion
				
				//Set services
				this.mSelectedStations = selectedStations;
				this.mUnassignedStations = FreightFactory.GetUnassignedStations();
			}
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose(bool disposing) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgAssignment));
			this.lstStations = new System.Windows.Forms.ListBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstStations
			// 
			this.lstStations.Location = new System.Drawing.Point(6, 24);
			this.lstStations.Name = "lstStations";
			this.lstStations.ScrollAlwaysVisible = true;
			this.lstStations.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
			this.lstStations.Size = new System.Drawing.Size(162, 212);
			this.lstStations.TabIndex = 0;
			this.lstStations.SelectedValueChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(177, 24);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 24);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(177, 57);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// dlgAssignment
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(282, 256);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lstStations);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgAssignment";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Station Selections";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			try {
				//Set control defaults
				lstStations.DataSource = this.mUnassignedStations;
				lstStations.DisplayMember = "WorkstationDetailTable.Number";
				this.lstStations.SelectedIndex = -1;
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { this.btnOK.Enabled = false; }
		}
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Validate form data and set user services
			try {
				this.btnOK.Enabled = (this.lstStations.SelectedItems.Count>0);
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command services
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:
						this.DialogResult = DialogResult.Cancel;
						break;
					case CMD_OK:
						this.DialogResult = DialogResult.OK;
						foreach(DataRowView dr in lstStations.SelectedItems) {
							try {
								this.mSelectedStations.WorkstationDetailTable.AddWorkstationDetailTableRow("","",0,dr.Row["Number"].ToString(),"","","","","",false,false);
							}
							catch(Exception ex) { App.ReportError(ex); }
						}
						break;
				}
				this.Close();
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
	}
}