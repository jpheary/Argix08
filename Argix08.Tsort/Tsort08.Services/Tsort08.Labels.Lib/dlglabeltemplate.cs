//	File:	dlglabeltemplate.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Provides user interaction for administration (create, update) of 
//			label templates.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Tsort.Labels {
	//
	public class dlgLabelTemplate : System.Windows.Forms.Form {
		//Members
		private LabelTemplate mLabelTemplate=null;
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";
		
		//Events
		#region Controls
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtFormat;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label _lblFormat;
		private System.Windows.Forms.ComboBox cboPrinter;
		private System.Windows.Forms.Label _lblPrinter;
		private System.Windows.Forms.GroupBox _fraLabelTemplate;
		private System.Windows.Forms.Label _lblType;
		private System.Windows.Forms.TextBox txtType;
		private System.ComponentModel.Container components = null;
		#endregion
		
		public dlgLabelTemplate(LabelTemplate template) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				
				//Set command button and menu identities (used for onclick handler)
				this.btnOK.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Initialize members
				this.mLabelTemplate = template;
                this.Text = (this.mLabelTemplate.LabelType.Length > 0) ? "Label Template (" + this.mLabelTemplate.LabelType + ")" : "Label Template (New)";
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Label Template dialog instance.", ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgLabelTemplate));
			this.btnOK = new System.Windows.Forms.Button();
			this._lblType = new System.Windows.Forms.Label();
			this._lblFormat = new System.Windows.Forms.Label();
			this.txtType = new System.Windows.Forms.TextBox();
			this.txtFormat = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this._fraLabelTemplate = new System.Windows.Forms.GroupBox();
			this.cboPrinter = new System.Windows.Forms.ComboBox();
			this._lblPrinter = new System.Windows.Forms.Label();
			this._fraLabelTemplate.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.SystemColors.Control;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Enabled = false;
			this.btnOK.Location = new System.Drawing.Point(174, 228);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 23);
			this.btnOK.TabIndex = 2;
			this.btnOK.Text = "O&K";
			this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// _lblType
			// 
			this._lblType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblType.Location = new System.Drawing.Point(12, 27);
			this._lblType.Name = "_lblType";
			this._lblType.Size = new System.Drawing.Size(96, 18);
			this._lblType.TabIndex = 118;
			this._lblType.Text = "Type";
			this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblFormat
			// 
			this._lblFormat.Location = new System.Drawing.Point(12, 87);
			this._lblFormat.Name = "_lblFormat";
			this._lblFormat.Size = new System.Drawing.Size(96, 18);
			this._lblFormat.TabIndex = 119;
			this._lblFormat.Text = "Format";
			this._lblFormat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtType
			// 
			this.txtType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtType.Location = new System.Drawing.Point(114, 27);
			this.txtType.Name = "txtType";
			this.txtType.Size = new System.Drawing.Size(96, 21);
			this.txtType.TabIndex = 0;
			this.txtType.Text = "";
			this.txtType.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtFormat
			// 
			this.txtFormat.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtFormat.Location = new System.Drawing.Point(114, 87);
			this.txtFormat.Multiline = true;
			this.txtFormat.Name = "txtFormat";
			this.txtFormat.Size = new System.Drawing.Size(243, 117);
			this.txtFormat.TabIndex = 1;
			this.txtFormat.Text = "";
			this.txtFormat.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(276, 228);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// _fraLabelTemplate
			// 
			this._fraLabelTemplate.Controls.Add(this.cboPrinter);
			this._fraLabelTemplate.Controls.Add(this._lblFormat);
			this._fraLabelTemplate.Controls.Add(this.txtFormat);
			this._fraLabelTemplate.Controls.Add(this._lblPrinter);
			this._fraLabelTemplate.Controls.Add(this.txtType);
			this._fraLabelTemplate.Controls.Add(this._lblType);
			this._fraLabelTemplate.Location = new System.Drawing.Point(6, 6);
			this._fraLabelTemplate.Name = "_fraLabelTemplate";
			this._fraLabelTemplate.Size = new System.Drawing.Size(366, 213);
			this._fraLabelTemplate.TabIndex = 121;
			this._fraLabelTemplate.TabStop = false;
			this._fraLabelTemplate.Text = "Label Template Details";
			// 
			// cboPrinter
			// 
			this.cboPrinter.DisplayMember = "SelectionListTable.Description";
			this.cboPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboPrinter.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboPrinter.ItemHeight = 13;
			this.cboPrinter.Location = new System.Drawing.Point(114, 57);
			this.cboPrinter.Name = "cboPrinter";
			this.cboPrinter.Size = new System.Drawing.Size(120, 21);
			this.cboPrinter.TabIndex = 120;
			this.cboPrinter.ValueMember = "SelectionListTable.ID";
			// 
			// _lblPrinter
			// 
			this._lblPrinter.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblPrinter.Location = new System.Drawing.Point(12, 57);
			this._lblPrinter.Name = "_lblPrinter";
			this._lblPrinter.Size = new System.Drawing.Size(96, 18);
			this._lblPrinter.TabIndex = 121;
			this._lblPrinter.Text = "Printer";
			this._lblPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// dlgLabelTemplate
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(378, 256);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this._fraLabelTemplate);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgLabelTemplate";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Label Template";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.OnFormLoad);
			this._fraLabelTemplate.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				try { 
					//Get selection lists
					DataSet ds = new DataSet("SelectionList");
					ds.Tables.Add("PrintersTable");
					ds.Tables["PrintersTable"].Columns.Add("Name");
					ds.Tables["PrintersTable"].Columns.Add("Value");
					ds.Tables["PrintersTable"].Rows.Add(new object[]{LabelStore.PRINTER_170Xi2, LabelStore.PRINTER_170Xi2});
					ds.Tables["PrintersTable"].Rows.Add(new object[]{LabelStore.PRINTER_110, LabelStore.PRINTER_110});
					ds.Tables["PrintersTable"].Rows.Add(new object[]{LabelStore.PRINTER_110PAX4, LabelStore.PRINTER_110PAX4});
					this.cboPrinter.DataSource = ds.Tables["PrintersTable"];
					this.cboPrinter.DisplayMember = "Name";
					this.cboPrinter.ValueMember = "Value";
				}
				catch(Exception) { }
				
				//Set control values
				if(this.mLabelTemplate.PrinterType != "") 
					this.cboPrinter.SelectedValue = this.mLabelTemplate.PrinterType;
				else
					if(this.cboPrinter.Items.Count>0)  this.cboPrinter.SelectedIndex = 0;
				this.cboPrinter.Enabled = (this.mLabelTemplate.PrinterType == "" && this.cboPrinter.Items.Count > 0);
				this.txtType.MaxLength = 3;
				this.txtType.Text = this.mLabelTemplate.LabelType;
                this.txtType.Enabled = (this.mLabelTemplate.LabelType == "");
				this.txtFormat.MaxLength = 2500;
				this.txtFormat.Text = this.mLabelTemplate.LabelString;
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.ValidateForm(null, null); this.Cursor = Cursors.Default; }
		}
		private void ValidateForm(object sender, System.EventArgs e) { 
			//Event handler for changes to control data
			try {
				//Enable OK service if details have valid changes
                this.btnOK.Enabled = (this.cboPrinter.Text.Trim() != "" && this.txtType.Text.Trim() != "");
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
						break;
					case CMD_OK:
						//Update component type details with user values
						this.mLabelTemplate.LabelType = this.txtType.Text;
						this.mLabelTemplate.PrinterType = this.cboPrinter.SelectedValue.ToString();
						this.mLabelTemplate.LabelString = this.txtFormat.Text;
						this.DialogResult = DialogResult.OK;
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Close(); }
		}
		private void reportError(Exception ex) {
			//Report an exception to the user
		}
	}
}
