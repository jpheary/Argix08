using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using Argix.Enterprise;

namespace Argix.Freight {
	//
	public class dlgCartonDelete : System.Windows.Forms.Form {
		//Members
		private StationOperator mStationOperator=null;
		
		#region Controls
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label _lblScan;
		private System.Windows.Forms.TextBox txtScan;
		private System.Windows.Forms.Button btnDelete;
		
		private System.ComponentModel.Container components = null;		//Required designer variable
		#endregion
		
		//Interface
		public dlgCartonDelete(StationOperator stationOperator) {
			//Constructor
			InitializeComponent();

			//Initialize members
			this.mStationOperator = stationOperator;
			this.mStationOperator.CartonDeleted += new CartonEventHandler(OnCartonDeleted);
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgCartonDelete));
			this.btnClose = new System.Windows.Forms.Button();
			this._lblScan = new System.Windows.Forms.Label();
			this.txtScan = new System.Windows.Forms.TextBox();
			this.btnDelete = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(312, 138);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 24);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// _lblScan
			// 
			this._lblScan.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblScan.Location = new System.Drawing.Point(6, 24);
			this._lblScan.Name = "_lblScan";
			this._lblScan.Size = new System.Drawing.Size(96, 18);
			this._lblScan.TabIndex = 1;
			this._lblScan.Text = "Barcode";
			this._lblScan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtScan
			// 
			this.txtScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtScan.Location = new System.Drawing.Point(6, 48);
			this.txtScan.Name = "txtScan";
			this.txtScan.Size = new System.Drawing.Size(402, 38);
			this.txtScan.TabIndex = 9;
			this.txtScan.Text = "";
			this.txtScan.TextChanged += new System.EventHandler(this.OnScanChanged);
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Location = new System.Drawing.Point(159, 93);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(96, 24);
			this.btnDelete.TabIndex = 10;
			this.btnDelete.Text = "Delete";
			this.btnDelete.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// dlgCartonDelete
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(414, 167);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.txtScan);
			this.Controls.Add(this._lblScan);
			this.Controls.Add(this.btnClose);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgCartonDelete";
			this.Text = "Delete Carton";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			try {				
				//Set initial control states
                this.txtScan.MaxLength = App.Config.ScanSize;
                this.txtScan.Text = "";
				this.txtScan.Focus();
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnScanChanged(object sender, System.EventArgs e) {
			//Event handler for change in scan text
			try {
				this.btnDelete.Enabled = false;
				if(this.txtScan.Text.Length == this.txtScan.MaxLength) {
					this.btnDelete.Enabled = true;
					this.btnDelete.Focus();
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnCartonDeleted(object sender, CartonEventArgs e) {
			//Event handler for a deleted carton
			try {
				//
				this.txtScan.Text = "";
			} 
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Name) {
					case "btnClose":
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case "btnDelete":
						//Update data and close the dialog
						Cursor.Current = Cursors.WaitCursor;
						try {
							this.mStationOperator.DeleteCarton(this.txtScan.Text);
						}
						catch (Exception ex) { MessageBox.Show(ex.Message); }
						break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex); } finally { Cursor.Current = Cursors.Default; }
		}
	}
}
