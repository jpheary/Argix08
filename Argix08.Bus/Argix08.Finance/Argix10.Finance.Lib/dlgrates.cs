//	File:	dlgrates.cs
//	Author:	jheary
//	Date:	08/27/08
//	Desc:	
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix;
using Argix.Windows;

namespace Argix.Finance {
	//
	public class dlgRates : System.Windows.Forms.Form {
		//Members
        public event ErrorEventHandler ErrorMessage = null;
		
		#region Controls
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private DriverRatesDS mDriverRatesDS;
        private ctlRates ctlRates1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
        public dlgRates(DriverRates rates) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.ctlRates1.Rates = rates;
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new dlgRates.", ex); }
		}
        protected override void Dispose(bool disposing) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgRates));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.mDriverRatesDS = new Argix.Finance.DriverRatesDS();
            this.ctlRates1 = new Argix.Finance.ctlRates();
            ((System.ComponentModel.ISupportInitialize)(this.mDriverRatesDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(366,324);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96,24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK ";
            this.btnOK.Visible = false;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(468,324);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96,24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Close";
            // 
            // mDriverRatesDS
            // 
            this.mDriverRatesDS.DataSetName = "DriverCompRatesDS";
            this.mDriverRatesDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ctlRates1
            // 
            this.ctlRates1.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctlRates1.Location = new System.Drawing.Point(1,2);
            this.ctlRates1.Name = "ctlRates1";
            this.ctlRates1.Rates = null;
            this.ctlRates1.Size = new System.Drawing.Size(571,316);
            this.ctlRates1.TabIndex = 2;
            this.ctlRates1.ErrorMessage += new Argix.Finance.ErrorEventHandler(this.OnErrorMessage);
            // 
            // dlgRates
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
            this.ClientSize = new System.Drawing.Size(570,352);
            this.Controls.Add(this.ctlRates1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgRates";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Driver Rates";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mDriverRatesDS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
            }
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOK.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in form data- validate OK service
			try {
                this.btnOK.Enabled = false;
			}
			catch(Exception ex) { reportError(ex); }
        }
        private void OnErrorMessage(object source,ErrorEventArgs e) { reportError(e.Exception); }
        #region Local Services: reportError()
		private void reportError(Exception ex) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex));
        }
        #endregion
    }
}
