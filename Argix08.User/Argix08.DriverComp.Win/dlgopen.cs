//	File:	dlgopen.cs
//	Author:	jheary
//	Date:	08/26/08
//	Desc:	Allows user to select a sort center and schedule date for a new 
//			or existing pay sheet.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using Argix;
using Argix.Enterprise;
using Argix.Windows;

namespace Argix.Finance {
	//
	public class dlgOpen : System.Windows.Forms.Form {
		//Members
		
		#region Controls
        private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboTerminal;
		private System.Windows.Forms.Label _lblTerminal;
        private System.Windows.Forms.Label _lblSDate;
        private Label _lblEDate;
        private DateTimePicker dtpStart;
        private DateTimePicker dtpEnd;
        private TerminalDS mTerminalDS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
        public dlgOpen() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new dlgOpen instance.", ex); }
		}
        public string AgentNumber { get { return this.cboTerminal.SelectedValue.ToString(); } }
        public string AgentName { get { return this.cboTerminal.Text; } }
        public DateTime StartDate { get { return this.dtpStart.Value; } }
        public DateTime EndDate { get { return this.dtpEnd.Value; } }
        protected override void Dispose(bool disposing) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgOpen));
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this.mTerminalDS = new Argix.Enterprise.TerminalDS();
            this._lblTerminal = new System.Windows.Forms.Label();
            this._lblSDate = new System.Windows.Forms.Label();
            this._lblEDate = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminalDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(126,132);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96,24);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "&OK ";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(228,132);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96,24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "&Cancel";
            // 
            // cboTerminal
            // 
            this.cboTerminal.DataSource = this.mTerminalDS;
            this.cboTerminal.DisplayMember = "LocalTerminalTable.Description";
            this.cboTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cboTerminal.Location = new System.Drawing.Point(82,12);
            this.cboTerminal.Name = "cboTerminal";
            this.cboTerminal.Size = new System.Drawing.Size(240,21);
            this.cboTerminal.TabIndex = 3;
            this.cboTerminal.ValueMember = "LocalTerminalTable.AgentNumber";
            this.cboTerminal.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // mTerminalDS
            // 
            this.mTerminalDS.DataSetName = "TerminalDS";
            this.mTerminalDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblTerminal
            // 
            this._lblTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblTerminal.Location = new System.Drawing.Point(6,12);
            this._lblTerminal.Name = "_lblTerminal";
            this._lblTerminal.Size = new System.Drawing.Size(72,18);
            this._lblTerminal.TabIndex = 4;
            this._lblTerminal.Text = "Terminal";
            this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblSDate
            // 
            this._lblSDate.Enabled = false;
            this._lblSDate.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblSDate.Location = new System.Drawing.Point(6,48);
            this._lblSDate.Name = "_lblSDate";
            this._lblSDate.Size = new System.Drawing.Size(72,19);
            this._lblSDate.TabIndex = 5;
            this._lblSDate.Text = "Start Date";
            this._lblSDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblEDate
            // 
            this._lblEDate.Font = new System.Drawing.Font("Microsoft Sans Serif",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblEDate.Location = new System.Drawing.Point(6,75);
            this._lblEDate.Name = "_lblEDate";
            this._lblEDate.Size = new System.Drawing.Size(72,19);
            this._lblEDate.TabIndex = 6;
            this._lblEDate.Text = "End Date";
            this._lblEDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpStart
            // 
            this.dtpStart.Enabled = false;
            this.dtpStart.Location = new System.Drawing.Point(82,48);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.ShowUpDown = true;
            this.dtpStart.Size = new System.Drawing.Size(192,20);
            this.dtpStart.TabIndex = 7;
            this.dtpStart.ValueChanged += new System.EventHandler(this.OnStartDateChanged);
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(82,75);
            this.dtpEnd.MaxDate = new System.DateTime(2108,12,31,0,0,0,0);
            this.dtpEnd.MinDate = new System.DateTime(1961,8,2,0,0,0,0);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.ShowUpDown = true;
            this.dtpEnd.Size = new System.Drawing.Size(192,20);
            this.dtpEnd.TabIndex = 8;
            this.dtpEnd.Value = new System.DateTime(2008,8,29,0,0,0,0);
            this.dtpEnd.ValueChanged += new System.EventHandler(this.OnEndDateChanged);
            // 
            // dlgOpen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
            this.ClientSize = new System.Drawing.Size(330,160);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this._lblEDate);
            this.Controls.Add(this._lblSDate);
            this.Controls.Add(this._lblTerminal);
            this.Controls.Add(this.cboTerminal);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgOpen";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Open Driver Compensation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mTerminalDS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				this.cboTerminal.DataSource = EnterpriseFactory.EnterpriseAgents;
				this.cboTerminal.SelectedIndex = 0;
				this.dtpEnd.Value = getLastSaturday();
                OnEndDateChanged(this.dtpEnd,EventArgs.Empty); ;
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.btnOK.Enabled = false; this.Cursor = Cursors.Default; }
		}
        private void OnStartDateChanged(object sender,EventArgs e) {
            //Event handler for change in start date
            OnValidateForm(null,null);
        }
        private void OnEndDateChanged(object sender,EventArgs e) {
            //Event handler for change in end date
            if(this.dtpEnd.Value.DayOfWeek != DayOfWeek.Saturday) {
                DateTime dte = this.dtpStart.Value.AddDays(6);
                DateTime dt = this.dtpEnd.Value;
                if(this.dtpEnd.Value.CompareTo(dte) < 0) {
                    while(dt.DayOfWeek != DayOfWeek.Saturday) dt = dt.AddDays(-1);
                }
                else {
                    while(dt.DayOfWeek != DayOfWeek.Saturday) dt = dt.AddDays(1);
                }
                this.dtpEnd.Value = dt;
            }
            this.dtpStart.Value = this.dtpEnd.Value.AddDays(-6);
        }
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in form data- validate OK service
			try {
				string terminalID = this.cboTerminal.SelectedValue.ToString();
                this.btnOK.Enabled = (terminalID.Trim().Length > 0);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #region Local Services: getLastSaturday()
        private DateTime getLastSaturday() {
            //
            DateTime dt = DateTime.Today;
            while(dt.DayOfWeek != DayOfWeek.Saturday) {
                dt = dt.AddDays(-1);
            }
            return dt;
        }
        #endregion
    }
}
