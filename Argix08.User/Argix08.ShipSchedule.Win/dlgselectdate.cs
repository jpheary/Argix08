using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Argix;
using Argix.Windows;
using Argix.Enterprise;

namespace Argix.AgentLineHaul {
	//
	public class dlgSelectDate : System.Windows.Forms.Form {
		//Members
		
		#region Controls
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.MonthCalendar calDate;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cboSortCenter;
		private System.Windows.Forms.Label _lblSortCenter;
		private System.Windows.Forms.Label _lblDate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
		public dlgSelectDate() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new dlgSelectDate.", ex); }
		}
        public long SortCenterID { get { return Convert.ToInt64(this.cboSortCenter.SelectedValue); } }
        public string SortCenter { get { return this.cboSortCenter.Text; } }
        public DateTime ScheduleDate { get { return this.calDate.SelectionStart; } }
        protected override void Dispose(bool disposing) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgSelectDate));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.calDate = new System.Windows.Forms.MonthCalendar();
			this.cboSortCenter = new System.Windows.Forms.ComboBox();
			this._lblSortCenter = new System.Windows.Forms.Label();
			this._lblDate = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnOK.Location = new System.Drawing.Point(126, 228);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 24);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "&OK ";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnCancel.Location = new System.Drawing.Point(228, 228);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			// 
			// calDate
			// 
			this.calDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.calDate.Location = new System.Drawing.Point(90, 42);
			this.calDate.MaxSelectionCount = 1;
			this.calDate.Name = "calDate";
			this.calDate.ShowTodayCircle = false;
			this.calDate.TabIndex = 2;
			this.calDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.OnDateSelected);
			// 
			// cboSortCenter
			// 
			this.cboSortCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSortCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cboSortCenter.Location = new System.Drawing.Point(90, 12);
			this.cboSortCenter.Name = "cboSortCenter";
			this.cboSortCenter.Size = new System.Drawing.Size(234, 21);
			this.cboSortCenter.TabIndex = 3;
			this.cboSortCenter.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
			// 
			// _lblSortCenter
			// 
			this._lblSortCenter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblSortCenter.Location = new System.Drawing.Point(6, 12);
			this._lblSortCenter.Name = "_lblSortCenter";
			this._lblSortCenter.Size = new System.Drawing.Size(72, 18);
			this._lblSortCenter.TabIndex = 4;
			this._lblSortCenter.Text = "Sort Center";
			this._lblSortCenter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblDate
			// 
			this._lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDate.Location = new System.Drawing.Point(6, 42);
			this._lblDate.Name = "_lblDate";
			this._lblDate.Size = new System.Drawing.Size(72, 19);
			this._lblDate.TabIndex = 5;
			this._lblDate.Text = "Date";
			this._lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dlgSelectDate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(330, 256);
			this.Controls.Add(this._lblDate);
			this.Controls.Add(this._lblSortCenter);
			this.Controls.Add(this.cboSortCenter);
			this.Controls.Add(this.calDate);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgSelectDate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "New Ship Schedule";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
                this.cboSortCenter.DisplayMember = "TerminalTable.Description";
                this.cboSortCenter.ValueMember = "TerminalTable.ID";
                this.cboSortCenter.DataSource = EnterpriseFactory.GetTerminals(global::Argix.Properties.ArgixSettings.Default.IsShipperSchedule);
                if (this.cboSortCenter.Items.Count > 0) this.cboSortCenter.SelectedIndex = 0;
				this.calDate.MinDate = System.DateTime.Today;
                OnValidateForm(null,null);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnDateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			//Event handler for date selected
			OnValidateForm(null, null);
		}
		private void OnValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes in form data- validate OK service
			try {
                long sortCenterID = this.cboSortCenter.SelectedValue != null ? Convert.ToInt64(this.cboSortCenter.SelectedValue) : 0;
				this.btnOK.Enabled = (sortCenterID > 0);
			}
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
		}		
	}
}
