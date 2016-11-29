using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;

namespace Argix.Terminals {
	//
	public class dlgComponentType : System.Windows.Forms.Form {
		//Members
		private ComponentType mType=null;
		#region Controls
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label _lblType;
		private System.Windows.Forms.TextBox txtType;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label _lblDescription;
		private System.Windows.Forms.GroupBox _fraComponentType;
		private System.Windows.Forms.ComboBox cboCategory;
		private Argix.Windows.SelectionList mCategoriesDS;
		private System.Windows.Forms.Label _lblCategory;
		private System.ComponentModel.Container components = null;
		#endregion
				
		//Inteface
		public dlgComponentType(ComponentType type) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.mType = type;
				this.Text = (this.mType.TypeID!="") ? "Component Type (" + this.mType.TypeID + ")" : "Component Type (New)";
			} 
			catch(Exception ex) { throw new ApplicationException("Failed to create new Component Type dialog", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgComponentType));
            this.btnOK = new System.Windows.Forms.Button();
            this._lblType = new System.Windows.Forms.Label();
            this._lblDescription = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this._fraComponentType = new System.Windows.Forms.GroupBox();
            this._lblCategory = new System.Windows.Forms.Label();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.mCategoriesDS = new Argix.Windows.SelectionList();
            this._fraComponentType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCategoriesDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(174,138);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(96,23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "O&K";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // _lblType
            // 
            this._lblType.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblType.Location = new System.Drawing.Point(12,60);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(96,18);
            this._lblType.TabIndex = 118;
            this._lblType.Text = "Type";
            this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDescription
            // 
            this._lblDescription.Location = new System.Drawing.Point(12,87);
            this._lblDescription.Name = "_lblDescription";
            this._lblDescription.Size = new System.Drawing.Size(96,18);
            this._lblDescription.TabIndex = 119;
            this._lblDescription.Text = "Description";
            this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtType
            // 
            this.txtType.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtType.Location = new System.Drawing.Point(114,60);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(144,21);
            this.txtType.TabIndex = 0;
            this.txtType.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtDescription
            // 
            this.txtDescription.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtDescription.Location = new System.Drawing.Point(114,87);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(240,21);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(276,138);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96,23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // _fraComponentType
            // 
            this._fraComponentType.Controls.Add(this._lblCategory);
            this._fraComponentType.Controls.Add(this.cboCategory);
            this._fraComponentType.Controls.Add(this.txtType);
            this._fraComponentType.Controls.Add(this._lblDescription);
            this._fraComponentType.Controls.Add(this._lblType);
            this._fraComponentType.Controls.Add(this.txtDescription);
            this._fraComponentType.Location = new System.Drawing.Point(6,6);
            this._fraComponentType.Name = "_fraComponentType";
            this._fraComponentType.Size = new System.Drawing.Size(366,120);
            this._fraComponentType.TabIndex = 121;
            this._fraComponentType.TabStop = false;
            this._fraComponentType.Text = "Component Type Details";
            // 
            // _lblCategory
            // 
            this._lblCategory.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this._lblCategory.Location = new System.Drawing.Point(12,27);
            this._lblCategory.Name = "_lblCategory";
            this._lblCategory.Size = new System.Drawing.Size(96,18);
            this._lblCategory.TabIndex = 121;
            this._lblCategory.Text = "Category";
            this._lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCategory
            // 
            this.cboCategory.DataSource = this.mCategoriesDS;
            this.cboCategory.DisplayMember = "SelectionListTable.Description";
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cboCategory.ItemHeight = 13;
            this.cboCategory.Location = new System.Drawing.Point(114,27);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(120,21);
            this.cboCategory.TabIndex = 120;
            this.cboCategory.ValueMember = "SelectionListTable.ID";
            // 
            // mCategoriesDS
            // 
            this.mCategoriesDS.DataSetName = "SelectionList";
            this.mCategoriesDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mCategoriesDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dlgComponentType
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(378,167);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this._fraComponentType);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgComponentType";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Component Type";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this._fraComponentType.ResumeLayout(false);
            this._fraComponentType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCategoriesDS)).EndInit();
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
				
				this.mCategoriesDS.Merge(MobileDevicesProxy.GetComponentTypeCategories());
				if(this.mType.CategoryID.Length != 0) 
					this.cboCategory.SelectedValue = this.mType.CategoryID;
				else
					if(this.cboCategory.Items.Count>0)  this.cboCategory.SelectedIndex = 0;
                this.cboCategory.Enabled = (this.mType.TypeID.Length == 0 && this.cboCategory.Items.Count > 0);
				this.txtType.MaxLength = 20;
				this.txtType.Text = this.mType.TypeID;
				this.txtType.Enabled = (this.mType.TypeID.Length == 0);
				this.txtDescription.MaxLength = 50;
				this.txtDescription.Text = this.mType.Description;
			}
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.btnOK.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnValidateForm(object sender, System.EventArgs e) { 
			//Event handler for changes to control data
			try {
				//Enable OK service if details have valid changes
				this.btnOK.Enabled = (this.cboCategory.Text != "" && this.txtType.Text!="");
			} 
			catch(Exception ex) { App.ReportError(ex, false, Argix.Terminals.LogLevel.Warning); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) { 
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Name) {
                    case "btnCancel": this.DialogResult = DialogResult.Cancel; break;
                    case "btnOK":
						//Update component type details with user values
						this.DialogResult = DialogResult.OK;
						this.mType.CategoryID = this.cboCategory.SelectedValue.ToString();
						this.mType.TypeID = this.txtType.Text;
						this.mType.Description = this.txtDescription.Text;
						this.mType.LastUpdated = DateTime.Now;
						this.mType.UserID = Environment.UserName;
						break;
				}
			} 
			catch(Exception ex) { App.ReportError(ex, true, Argix.Terminals.LogLevel.Error); }
			finally { this.Close(); }
		}
	}
}
