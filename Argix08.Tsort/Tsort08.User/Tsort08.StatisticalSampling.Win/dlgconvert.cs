//	File:	dlgconvert.cs
//	Author:	jheary
//	Date:	04/09/09
//	Desc:	Tool to convert EAN to ISBN.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Argix {
	//
	public class dlgConvert : System.Windows.Forms.Form {
		//Members
        private System.ComponentModel.IContainer components;
		private System.Windows.Forms.TextBox txtData;
		private System.Windows.Forms.StatusBar stbMain;
        private System.Windows.Forms.StatusBarPanel pnlStatus;
        private System.Windows.Forms.Label _lblData;
        private System.Windows.Forms.Label _lblResult;
        private TextBox txtResult;
        private Label _lblType;
        private ComboBox cboType;
        private System.Windows.Forms.Button btnGo;
				
		//Interface
		public dlgConvert() {
			//Constructor
			InitializeComponent();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgConvert));
            this.txtData = new System.Windows.Forms.TextBox();
            this.stbMain = new System.Windows.Forms.StatusBar();
            this.pnlStatus = new System.Windows.Forms.StatusBarPanel();
            this.btnGo = new System.Windows.Forms.Button();
            this._lblResult = new System.Windows.Forms.Label();
            this._lblData = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this._lblType = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(6,75);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(168,21);
            this.txtData.TabIndex = 87;
            this.txtData.TextChanged += new System.EventHandler(this.OnDataChanged);
            // 
            // stbMain
            // 
            this.stbMain.Location = new System.Drawing.Point(0,194);
            this.stbMain.Name = "stbMain";
            this.stbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this.pnlStatus});
            this.stbMain.ShowPanels = true;
            this.stbMain.Size = new System.Drawing.Size(186,22);
            this.stbMain.SizingGrip = false;
            this.stbMain.TabIndex = 91;
            // 
            // pnlStatus
            // 
            this.pnlStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Width = 186;
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(99,162);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(72,24);
            this.btnGo.TabIndex = 99;
            this.btnGo.Text = "Convert";
            this.btnGo.Click += new System.EventHandler(this.OnGo);
            // 
            // _lblResult
            // 
            this._lblResult.Location = new System.Drawing.Point(6,105);
            this._lblResult.Name = "_lblResult";
            this._lblResult.Size = new System.Drawing.Size(72,18);
            this._lblResult.TabIndex = 98;
            this._lblResult.Text = "ISBN:";
            this._lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblData
            // 
            this._lblData.Location = new System.Drawing.Point(6,54);
            this._lblData.Name = "_lblData";
            this._lblData.Size = new System.Drawing.Size(72,18);
            this._lblData.TabIndex = 90;
            this._lblData.Text = "Data:";
            this._lblData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(6,126);
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(168,21);
            this.txtResult.TabIndex = 100;
            // 
            // _lblType
            // 
            this._lblType.Location = new System.Drawing.Point(6,6);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(99,18);
            this._lblType.TabIndex = 90;
            this._lblType.Text = "Convert to:";
            this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboType
            // 
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Items.AddRange(new object[] {
            "EAN",
            "ISBN"});
            this.cboType.Location = new System.Drawing.Point(6,27);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(96,21);
            this.cboType.TabIndex = 101;
            this.cboType.SelectionChangeCommitted += new System.EventHandler(this.OnTypeChanged);
            // 
            // dlgConvert
            // 
            this.AcceptButton = this.btnGo;
            this.AutoScaleBaseSize = new System.Drawing.Size(6,14);
            this.ClientSize = new System.Drawing.Size(186,216);
            this.Controls.Add(this.cboType);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.stbMain);
            this.Controls.Add(this._lblResult);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this._lblType);
            this.Controls.Add(this._lblData);
            this.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgConvert";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scan Converter";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
				
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				this.Visible = true;
                this.cboType.SelectedItem = "EAN";
                OnTypeChanged(null,EventArgs.Empty);
                this.btnGo.Enabled = false;
				this.txtData.Focus();
			}
            catch(Exception ex) { this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = ex.Message; }
			finally { this.Cursor = Cursors.Default; }
		}
        private void OnTypeChanged(object sender,EventArgs e) {
            //Event handler for change in  selected conversion type
            try {
                this._lblResult.Text = this.cboType.SelectedItem.ToString();
                this.txtResult.Text = "";
                OnDataChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = ex.Message; }
        }
        private void OnDataChanged(object sender,System.EventArgs e) {
			//Event handler for change in data
            this.btnGo.Enabled = this.txtData.Text.Length > 0;
        }
		private void OnGo(object sender, System.EventArgs e) {
            //Event handler for go button clicked
            this.Cursor = Cursors.WaitCursor;
            try {
                this.txtResult.Text = "";
                this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = "";
                if(this.cboType.SelectedItem.ToString() == "ISBN") {
                    if (this.txtData.Text.Length != global::Argix.Properties.Settings.Default.EANScanSize)
                        this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = "Invalid EAN length (i.e. " +global::Argix.Properties.Settings.Default.EANScanSize.ToString() + ")";
                    else if (this.txtData.Text.Substring(0, global::Argix.Properties.Settings.Default.EANPrefix.Length) != global::Argix.Properties.Settings.Default.EANPrefix)
                        this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = "Missing EAN prefix (i.e. " + global::Argix.Properties.Settings.Default.EANPrefix + ")";
                    else if(!Argix.Freight.Carton.ValidateEAN(this.txtData.Text))
                        this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = "Failed EAN check digit validation.";
                    else
                        this.txtResult.Text = Argix.Freight.Carton.ConvertEANtoISBN(this.txtData.Text);
                }
                else if(this.cboType.SelectedItem.ToString() == "EAN") {
                    if (this.txtData.Text.Length != global::Argix.Properties.Settings.Default.ISBNScanSize)
                        this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = "Invalid ISBN length (i.e. " + global::Argix.Properties.Settings.Default.ISBNScanSize.ToString() + ")";
                    else if(!Argix.Freight.Carton.ValidateISBN(this.txtData.Text))
                        this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = "Failed ISBN check digit validation.";
                    else
                        this.txtResult.Text = Argix.Freight.Carton.ConvertISBNtoEAN(this.txtData.Text);
                }
            }
            catch(Exception ex) { this.stbMain.Panels[0].Text = this.stbMain.Panels[0].ToolTipText = ex.Message; }
            finally { this.Cursor = Cursors.Default; }
        }
	}
}
