using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Argix.Terminals {
	//
	public class frmBatteryAssignReport : System.Windows.Forms.Form {
        //Members
		#region Controls

        private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnCancel;
        private Microsoft.Reporting.WinForms.ReportViewer rsViewer;
        private System.Windows.Forms.Button btnOK;

		#endregion
				
		//Interface
		public frmBatteryAssignReport() {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new Battery Assign Report window", ex); }
		}
		protected override void Dispose( bool disposing )  { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBatteryAssignReport));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.rsViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.ForeColor = System.Drawing.Color.Navy;
            this.btnCancel.Location = new System.Drawing.Point(72,256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(64,23);
            this.btnCancel.TabIndex = 153;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.ForeColor = System.Drawing.Color.Navy;
            this.btnOK.Location = new System.Drawing.Point(8,256);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(64,23);
            this.btnOK.TabIndex = 152;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = false;
            // 
            // rsViewer
            // 
            this.rsViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsViewer.Location = new System.Drawing.Point(0,0);
            this.rsViewer.Name = "rsViewer";
            this.rsViewer.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsViewer.ServerReport.DisplayName = "Battery Assignments";
            this.rsViewer.ServerReport.ReportPath = "/Terminals/Mobile Devices Battery Assignments";
            this.rsViewer.ServerReport.ReportServerUrl = new System.Uri("http://rgxsqlrpts05/reportserver",System.UriKind.Absolute);
            this.rsViewer.ServerReport.Timeout = 60000;
            this.rsViewer.Size = new System.Drawing.Size(472,233);
            this.rsViewer.TabIndex = 0;
            // 
            // frmBatteryAssignReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(472,233);
            this.Controls.Add(this.rsViewer);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBatteryAssignReport";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sample Report";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

		}
		#endregion
				
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions			
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				this.Visible = true;
				Application.DoEvents();
                this.rsViewer.RefreshReport();
            }
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
	}
}
