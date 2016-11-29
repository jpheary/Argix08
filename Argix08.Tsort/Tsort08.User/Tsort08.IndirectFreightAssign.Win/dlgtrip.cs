using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Argix.Freight {
	//
	public class dlgTrip : System.Windows.Forms.Form {
		//Members
		private BearwareTrip mTrip=null;
		
		#region Controls
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtTrip;
		private System.Windows.Forms.TextBox txtCartons;
		private System.Windows.Forms.TextBox txtCarrier;
		private System.Windows.Forms.TextBox txtTrailer;
		private System.Windows.Forms.Label _lblTrip;
		private System.Windows.Forms.Label _lblCartons;
		private System.Windows.Forms.Label _lblCarrier;
		private System.Windows.Forms.Label _lblTrailer;
		
		private System.ComponentModel.Container components = null;
		#endregion
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		//Interface
		public dlgTrip(ref BearwareTrip trip) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				#region Set command button identities (used for onclick handler)
				this.btnCancel.Text = CMD_CANCEL;
				this.btnOK.Text = CMD_OK;
				#endregion
				
				//Set members
				this.mTrip = trip;
				this.Text = "Add New Trip";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgTrip));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtTrip = new System.Windows.Forms.TextBox();
			this.txtCartons = new System.Windows.Forms.TextBox();
			this.txtCarrier = new System.Windows.Forms.TextBox();
			this.txtTrailer = new System.Windows.Forms.TextBox();
			this._lblTrip = new System.Windows.Forms.Label();
			this._lblCartons = new System.Windows.Forms.Label();
			this._lblCarrier = new System.Windows.Forms.Label();
			this._lblTrailer = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(174, 177);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 24);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(276, 177);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// txtTrip
			// 
			this.txtTrip.Location = new System.Drawing.Point(108, 24);
			this.txtTrip.MaxLength = 13;
			this.txtTrip.Name = "txtTrip";
			this.txtTrip.Size = new System.Drawing.Size(144, 21);
			this.txtTrip.TabIndex = 2;
			this.txtTrip.Text = "";
			this.txtTrip.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtCartons
			// 
			this.txtCartons.Location = new System.Drawing.Point(108, 54);
			this.txtCartons.MaxLength = 5;
			this.txtCartons.Name = "txtCartons";
			this.txtCartons.Size = new System.Drawing.Size(60, 21);
			this.txtCartons.TabIndex = 3;
			this.txtCartons.Text = "";
			this.txtCartons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtCartons.TextChanged += new System.EventHandler(this.OnCartonsChanged);
			this.txtCartons.Leave += new System.EventHandler(this.OnCartonsChanged);
			// 
			// txtCarrier
			// 
			this.txtCarrier.Location = new System.Drawing.Point(108, 84);
			this.txtCarrier.MaxLength = 20;
			this.txtCarrier.Name = "txtCarrier";
			this.txtCarrier.Size = new System.Drawing.Size(240, 21);
			this.txtCarrier.TabIndex = 4;
			this.txtCarrier.Text = "";
			this.txtCarrier.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// txtTrailer
			// 
			this.txtTrailer.Location = new System.Drawing.Point(108, 114);
			this.txtTrailer.MaxLength = 10;
			this.txtTrailer.Name = "txtTrailer";
			this.txtTrailer.Size = new System.Drawing.Size(60, 21);
			this.txtTrailer.TabIndex = 5;
			this.txtTrailer.Text = "";
			this.txtTrailer.TextChanged += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblTrip
			// 
			this._lblTrip.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTrip.Location = new System.Drawing.Point(6, 24);
			this._lblTrip.Name = "_lblTrip";
			this._lblTrip.Size = new System.Drawing.Size(96, 18);
			this._lblTrip.TabIndex = 6;
			this._lblTrip.Text = "Trip #:";
			this._lblTrip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblCartons
			// 
			this._lblCartons.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCartons.Location = new System.Drawing.Point(6, 54);
			this._lblCartons.Name = "_lblCartons";
			this._lblCartons.Size = new System.Drawing.Size(96, 18);
			this._lblCartons.TabIndex = 7;
			this._lblCartons.Text = "# Of Cartons:";
			this._lblCartons.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblCarrier
			// 
			this._lblCarrier.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCarrier.Location = new System.Drawing.Point(6, 84);
			this._lblCarrier.Name = "_lblCarrier";
			this._lblCarrier.Size = new System.Drawing.Size(96, 18);
			this._lblCarrier.TabIndex = 8;
			this._lblCarrier.Text = "Carrier:";
			this._lblCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblTrailer
			// 
			this._lblTrailer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblTrailer.Location = new System.Drawing.Point(6, 114);
			this._lblTrailer.Name = "_lblTrailer";
			this._lblTrailer.Size = new System.Drawing.Size(96, 18);
			this._lblTrailer.TabIndex = 9;
			this._lblTrailer.Text = "Trailer #:";
			this._lblTrailer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dlgTrip
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(378, 208);
			this.Controls.Add(this._lblTrailer);
			this.Controls.Add(this._lblCarrier);
			this.Controls.Add(this._lblCartons);
			this.Controls.Add(this._lblTrip);
			this.Controls.Add(this.txtTrailer);
			this.Controls.Add(this.txtCarrier);
			this.Controls.Add(this.txtCartons);
			this.Controls.Add(this.txtTrip);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgTrip";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Trip";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			try {
				//Set control defaults
				this.txtTrip.Text = this.mTrip.Number;
				this.txtCartons.Text = this.mTrip.CartonCount.ToString();
				this.txtCarrier.Text = this.mTrip.Carrier;
				this.txtTrailer.Text = this.mTrip.TrailerNumber;
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { OnValidateForm(null,null); }
		}
		private void OnCartonsChanged(object sender, System.EventArgs e) {
			//Event handler when focus leaves the cartons texbox
			try {
				int.Parse(this.txtCartons.Text);
			}
			catch(Exception) {
				MessageBox.Show("Carton quantity must be numeric.");
				txtCartons.Focus(); 
			}
			finally { OnValidateForm(null,null); }
		}
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Event handler for validating user entry
			try {
				this.btnOK.Enabled = (this.txtTrip.Text.Trim() != "" && this.txtCartons.Text.Trim() != "");
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
						this.mTrip.Number = this.txtTrip.Text;
						this.mTrip.CartonCount = int.Parse(this.txtCartons.Text);
						this.mTrip.Carrier = this.txtCarrier.Text;
						this.mTrip.TrailerNumber = this.txtTrailer.Text;
						break;
				}
				this.Close();
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
	}
}