//	File:	dlgstationsetup.cs
//	Author:	MK
//	Date:	03/20/2003
//	Desc:	Provides ability to change station configuration.
//	Rev:	01/26/04 (jph)
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
//using Tsort.Devices;

namespace Tsort.Sort {
	//
	public class dlgStationSetup : System.Windows.Forms.Form {
		//Members
		private string mScaleType=SCALETYPE_AUTOMATIC;
		
		#region Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "&OK";
		
		public const string SCALETYPE_AUTOMATIC = "automatic";
		//public const string SCALETYPE_MANUAL = "manual";
		public const string SCALETYPE_CONSTANT = "constant";
		//public const string SCALETYPE_TABLE = "table";
		#endregion
		#region Controls
		private System.Windows.Forms.RadioButton rdoAutomatic;
		private System.Windows.Forms.RadioButton rdoManual;
		private System.Windows.Forms.RadioButton rdoConstant;
		private System.Windows.Forms.RadioButton rdoTable;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.GroupBox grpScale;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.NumericUpDown updConstantWeight;
		private System.Windows.Forms.Label _lblMaxWeight;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
		public dlgStationSetup(): this(SCALETYPE_AUTOMATIC) { }
		public dlgStationSetup(string scaleType) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.btnCancel.Text = CMD_CANCEL;
				this.btnOK.Text = CMD_OK;
				
				//Set members
				this.mScaleType = scaleType;
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgStationSetup instance.", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		public string ScaleType { get { return this.mScaleType; } }
		public decimal ConstantWeight { get { return this.updConstantWeight.Value; } }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.grpScale = new System.Windows.Forms.GroupBox();
			this.updConstantWeight = new System.Windows.Forms.NumericUpDown();
			this.rdoTable = new System.Windows.Forms.RadioButton();
			this.rdoConstant = new System.Windows.Forms.RadioButton();
			this.rdoManual = new System.Windows.Forms.RadioButton();
			this.rdoAutomatic = new System.Windows.Forms.RadioButton();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this._lblMaxWeight = new System.Windows.Forms.Label();
			this.grpScale.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.updConstantWeight)).BeginInit();
			this.SuspendLayout();
			// 
			// grpScale
			// 
			this.grpScale.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpScale.Controls.Add(this._lblMaxWeight);
			this.grpScale.Controls.Add(this.updConstantWeight);
			this.grpScale.Controls.Add(this.rdoTable);
			this.grpScale.Controls.Add(this.rdoConstant);
			this.grpScale.Controls.Add(this.rdoManual);
			this.grpScale.Controls.Add(this.rdoAutomatic);
			this.grpScale.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpScale.ForeColor = System.Drawing.SystemColors.ControlText;
			this.grpScale.Location = new System.Drawing.Point(6, 3);
			this.grpScale.Name = "grpScale";
			this.grpScale.Size = new System.Drawing.Size(366, 162);
			this.grpScale.TabIndex = 0;
			this.grpScale.TabStop = false;
			this.grpScale.Text = "Scale";
			// 
			// updConstantWeight
			// 
			this.updConstantWeight.DecimalPlaces = 2;
			this.updConstantWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.updConstantWeight.Location = new System.Drawing.Point(120, 81);
			this.updConstantWeight.Maximum = new System.Decimal(new int[] {
																			  99999,
																			  0,
																			  0,
																			  131072});
			this.updConstantWeight.Name = "updConstantWeight";
			this.updConstantWeight.Size = new System.Drawing.Size(72, 21);
			this.updConstantWeight.TabIndex = 5;
			this.updConstantWeight.ValueChanged += new System.EventHandler(this.OnConstantWeightChanged);
			// 
			// rdoTable
			// 
			this.rdoTable.BackColor = System.Drawing.SystemColors.Control;
			this.rdoTable.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoTable.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rdoTable.Location = new System.Drawing.Point(18, 108);
			this.rdoTable.Name = "rdoTable";
			this.rdoTable.Size = new System.Drawing.Size(96, 24);
			this.rdoTable.TabIndex = 3;
			this.rdoTable.Text = "Table";
			this.rdoTable.CheckedChanged += new System.EventHandler(this.OnScaleTypeChanged);
			// 
			// rdoConstant
			// 
			this.rdoConstant.BackColor = System.Drawing.SystemColors.Control;
			this.rdoConstant.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoConstant.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rdoConstant.Location = new System.Drawing.Point(18, 81);
			this.rdoConstant.Name = "rdoConstant";
			this.rdoConstant.Size = new System.Drawing.Size(96, 24);
			this.rdoConstant.TabIndex = 2;
			this.rdoConstant.Text = "Constant";
			this.rdoConstant.CheckedChanged += new System.EventHandler(this.OnScaleTypeChanged);
			// 
			// rdoManual
			// 
			this.rdoManual.BackColor = System.Drawing.SystemColors.Control;
			this.rdoManual.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoManual.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rdoManual.Location = new System.Drawing.Point(18, 54);
			this.rdoManual.Name = "rdoManual";
			this.rdoManual.Size = new System.Drawing.Size(96, 24);
			this.rdoManual.TabIndex = 1;
			this.rdoManual.Text = "Manual";
			this.rdoManual.CheckedChanged += new System.EventHandler(this.OnScaleTypeChanged);
			// 
			// rdoAutomatic
			// 
			this.rdoAutomatic.BackColor = System.Drawing.SystemColors.Control;
			this.rdoAutomatic.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.rdoAutomatic.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rdoAutomatic.Location = new System.Drawing.Point(18, 27);
			this.rdoAutomatic.Name = "rdoAutomatic";
			this.rdoAutomatic.Size = new System.Drawing.Size(96, 24);
			this.rdoAutomatic.TabIndex = 0;
			this.rdoAutomatic.Text = "Automatic";
			this.rdoAutomatic.CheckedChanged += new System.EventHandler(this.OnScaleTypeChanged);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.BackColor = System.Drawing.SystemColors.Control;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnOK.Location = new System.Drawing.Point(177, 177);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(96, 25);
			this.btnOK.TabIndex = 16;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnCancel.Location = new System.Drawing.Point(276, 177);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 25);
			this.btnCancel.TabIndex = 17;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// _lblMaxWeight
			// 
			this._lblMaxWeight.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblMaxWeight.Location = new System.Drawing.Point(6, 135);
			this._lblMaxWeight.Name = "_lblMaxWeight";
			this._lblMaxWeight.Size = new System.Drawing.Size(288, 18);
			this._lblMaxWeight.TabIndex = 6;
			this._lblMaxWeight.Text = "*Maximum item weight: 999.99lbs";
			this._lblMaxWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dlgStationSetup
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(378, 208);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.grpScale);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgStationSetup";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Station Setup";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.grpScale.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.updConstantWeight)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load controls
				this.rdoAutomatic.Enabled = this.rdoManual.Enabled = this.rdoConstant.Enabled = this.rdoTable.Enabled = false;
				string[] scaleTypes = new string[] { "Automatic", "Constant" };
				for(int i=0; i<scaleTypes.Length; i++) {
					if(scaleTypes[i].ToLower() == SCALETYPE_AUTOMATIC) this.rdoAutomatic.Enabled = true;
					this.rdoManual.Enabled = false;
					if(scaleTypes[i].ToLower() == SCALETYPE_CONSTANT) this.rdoConstant.Enabled = true;
					this.rdoTable.Enabled = false;
				}
				if(this.mScaleType.ToLower() == SCALETYPE_AUTOMATIC) this.rdoAutomatic.Checked = true;
				//if(this.mScaleType.ToLower() == SCALETYPE_MANUAL) this.rdoManual.Checked = true;
				if(this.mScaleType.ToLower() == SCALETYPE_CONSTANT) this.rdoConstant.Checked = true;
				//if(this.mScaleType.ToLower() == SCALETYPE_TABLE) this.rdoTable.Checked = true;
				//this.rdoTable.Visible = false;
				this.updConstantWeight.Maximum = new System.Decimal(SortedItem.WeightMax);
				this._lblMaxWeight.Text = "*Maximum item weight: " + SortedItem.WeightMax.ToString() + "lbs";
				setServices();
			} 
			catch(Exception) { }
			finally { this.btnOK.Enabled=false; this.Cursor = Cursors.Default; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) { }
		private void OnScaleTypeChanged(object sender, System.EventArgs e) {
			//Event handler for change in scale type
			if(this.rdoAutomatic.Checked) this.mScaleType = SCALETYPE_AUTOMATIC;
			else if(this.rdoConstant.Checked) this.mScaleType = SCALETYPE_CONSTANT;
			setServices();
		}
		private void OnConstantWeightChanged(object sender, System.EventArgs e) {
			//Event handler for change in constant weight
			setServices();
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:	
						this.DialogResult = DialogResult.Cancel; 
						break;
					case CMD_OK:		
						this.DialogResult = DialogResult.OK;
						if(rdoAutomatic.Checked) 
							this.mScaleType = SCALETYPE_AUTOMATIC;
						//else if(rdoManual.Checked) 
						//	this.mScaleType = SCALETYPE_MANUAL;
						//else if(rdoTable.Checked) 
						//	this.mScaleType = SCALETYPE_TABLE;
						else if(rdoConstant.Checked) {
							this.mScaleType = SCALETYPE_CONSTANT;
						}
						break;
				}
			} 
			catch(Exception) { }
			finally { this.Close(); }
		}
		#region Local services: setServices(), reportError()
		private void setServices() {
			//Set user services states
			try {
				this.updConstantWeight.Enabled = (this.mScaleType.ToLower() == SCALETYPE_CONSTANT);
				this.btnOK.Enabled =	(this.mScaleType.ToLower() == SCALETYPE_AUTOMATIC) ||
										(this.mScaleType.ToLower() == SCALETYPE_CONSTANT && this.updConstantWeight.Value > 0);
			}
			catch(Exception ex) { reportError(ex); }
		}
		private void reportError(Exception ex) {
			//Report an exception to the user
		}
		#endregion
	}
}
