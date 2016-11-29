//	File:	dlgterminallane.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create, edit, and delete terminal lanes.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Tsort.Enterprise;

namespace Tsort {
	//
	public class dlgTerminalLaneDetail : System.Windows.Forms.Form {
		//Members
		private string mLaneID="";
		#region Controls

		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label _lblNumber;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.GroupBox fraDetails;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.TextBox txtNumber;
		private System.Windows.Forms.Label _lblLaneType;
		private System.Windows.Forms.Label _lblDescription;
		private Tsort.Enterprise.TerminalLaneDS mLaneDS;
		private Tsort.Windows.SelectionList mLaneTypesDS;
		private Tsort.Windows.SelectionList mTerminalsDS;
		private System.Windows.Forms.ComboBox cboLaneTypes;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabControl tabDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgTerminalLaneDetail(ref TerminalLaneDS lane) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mLaneDS = lane;
				if(this.mLaneDS.TerminalLaneTable.Count>0) {
					this.mLaneID = this.mLaneDS.TerminalLaneTable[0].LaneID;
					this.Text = (this.mLaneID!="") ? "Terminal Lane (" + this.mLaneID.Trim() + ")" : "Terminal Lane (New)";
				}
				else
					this.Text = "Terminal Lane (Data Unavailable)";
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgTerminalLaneDetail));
			this.mTerminalsDS = new Tsort.Windows.SelectionList();
			this._lblNumber = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.fraDetails = new System.Windows.Forms.GroupBox();
			this._lblDescription = new System.Windows.Forms.Label();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.txtNumber = new System.Windows.Forms.TextBox();
			this._lblLaneType = new System.Windows.Forms.Label();
			this.cboLaneTypes = new System.Windows.Forms.ComboBox();
			this.mLaneTypesDS = new Tsort.Windows.SelectionList();
			this.mLaneDS = new Tsort.Enterprise.TerminalLaneDS();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			((System.ComponentModel.ISupportInitialize)(this.mTerminalsDS)).BeginInit();
			this.fraDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mLaneTypesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mLaneDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.SuspendLayout();
			// 
			// mTerminalsDS
			// 
			this.mTerminalsDS.DataSetName = "SelectionList";
			this.mTerminalsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblNumber
			// 
			this._lblNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblNumber.Location = new System.Drawing.Point(6, 24);
			this._lblNumber.Name = "_lblNumber";
			this._lblNumber.Size = new System.Drawing.Size(96, 18);
			this._lblNumber.TabIndex = 3;
			this._lblNumber.Text = "Number";
			this._lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(276, 234);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(174, 234);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// fraDetails
			// 
			this.fraDetails.Controls.Add(this._lblDescription);
			this.fraDetails.Controls.Add(this.txtDescription);
			this.fraDetails.Controls.Add(this._lblNumber);
			this.fraDetails.Controls.Add(this.txtNumber);
			this.fraDetails.Controls.Add(this._lblLaneType);
			this.fraDetails.Controls.Add(this.cboLaneTypes);
			this.fraDetails.Location = new System.Drawing.Point(6, 6);
			this.fraDetails.Name = "fraDetails";
			this.fraDetails.Size = new System.Drawing.Size(348, 186);
			this.fraDetails.TabIndex = 0;
			this.fraDetails.TabStop = false;
			this.fraDetails.Text = "Terminal Lane";
			// 
			// _lblDescription
			// 
			this._lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDescription.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblDescription.Location = new System.Drawing.Point(6, 84);
			this._lblDescription.Name = "_lblDescription";
			this._lblDescription.Size = new System.Drawing.Size(96, 18);
			this._lblDescription.TabIndex = 5;
			this._lblDescription.Text = "Description";
			this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtDescription
			// 
			this.txtDescription.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtDescription.Location = new System.Drawing.Point(108, 84);
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.Size = new System.Drawing.Size(228, 21);
			this.txtDescription.TabIndex = 2;
			this.txtDescription.Text = "";
			this.txtDescription.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtNumber
			// 
			this.txtNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtNumber.Location = new System.Drawing.Point(108, 24);
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(96, 21);
			this.txtNumber.TabIndex = 0;
			this.txtNumber.Text = "";
			this.txtNumber.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblLaneType
			// 
			this._lblLaneType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLaneType.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLaneType.Location = new System.Drawing.Point(6, 54);
			this._lblLaneType.Name = "_lblLaneType";
			this._lblLaneType.Size = new System.Drawing.Size(96, 18);
			this._lblLaneType.TabIndex = 4;
			this._lblLaneType.Text = "Lane Type";
			this._lblLaneType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboLaneTypes
			// 
			this.cboLaneTypes.DataSource = this.mLaneTypesDS;
			this.cboLaneTypes.DisplayMember = "SelectionListTable.Description";
			this.cboLaneTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboLaneTypes.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboLaneTypes.Location = new System.Drawing.Point(108, 54);
			this.cboLaneTypes.Name = "cboLaneTypes";
			this.cboLaneTypes.Size = new System.Drawing.Size(174, 21);
			this.cboLaneTypes.TabIndex = 1;
			this.cboLaneTypes.ValueMember = "SelectionListTable.ID";
			this.cboLaneTypes.SelectionChangeCommitted += new System.EventHandler(this.ValidateForm);
			// 
			// mLaneTypesDS
			// 
			this.mLaneTypesDS.DataSetName = "SelectionList";
			this.mLaneTypesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mLaneDS
			// 
			this.mLaneDS.DataSetName = "TerminalLaneDS";
			this.mLaneDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(369, 225);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.Controls.Add(this.fraDetails);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(361, 199);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// dlgTerminalLaneDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(378, 263);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgTerminalLaneDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Terminal Lane Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.mTerminalsDS)).EndInit();
			this.fraDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mLaneTypesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mLaneDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mLaneTypesDS.Merge(EnterpriseFactory.GetTerminalLaneTypes());
				this.mTerminalsDS.Merge(EnterpriseFactory.GetEntTerminals());
				
				//Set control services
				this.txtNumber.MaxLength = 10;
				this.txtNumber.Text = "";
				if(!this.mLaneDS.TerminalLaneTable[0].IsLaneNumberNull())
					this.txtNumber.Text = this.mLaneDS.TerminalLaneTable[0].LaneNumber.Trim();
				if(!this.mLaneDS.TerminalLaneTable[0].IsLaneTypeNull()) 
					this.cboLaneTypes.SelectedValue = this.mLaneDS.TerminalLaneTable[0].LaneType;
				else
					if(this.cboLaneTypes.Items.Count>0) this.cboLaneTypes.SelectedIndex = 0;
				this.cboLaneTypes.Enabled = (this.cboLaneTypes.Items.Count>0);
				this.txtDescription.MaxLength = 40;
				this.txtDescription.Text = "";
				if(!this.mLaneDS.TerminalLaneTable[0].IsDescriptionNull())
					this.txtDescription.Text = this.mLaneDS.TerminalLaneTable[0].Description.Trim();
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes to control data
			try {
				if(this.mLaneDS.TerminalLaneTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtNumber.Text!="" && this.cboLaneTypes.Text!="");
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						//Update details with control values
						this.Cursor = Cursors.WaitCursor;
						this.mLaneDS.TerminalLaneTable[0].LaneNumber = this.txtNumber.Text;
						this.mLaneDS.TerminalLaneTable[0].LaneType = this.cboLaneTypes.SelectedValue.ToString();
						this.mLaneDS.TerminalLaneTable[0].Description = this.txtDescription.Text;
						this.mLaneDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: reportError()
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
