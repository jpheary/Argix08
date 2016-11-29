using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Argix {
	/// <summary> </summary>
	public class winTrace : System.Windows.Forms.Form {
		//Members, constants
		#region Controls

		private System.Windows.Forms.ContextMenu ctxMain;
		private System.Windows.Forms.MenuItem ctxRefresh;
		private System.Windows.Forms.MenuItem ctxClear;
		private System.Windows.Forms.MenuItem ctxSep1;
		private System.Windows.Forms.RichTextBox txtTrace;
		private System.Windows.Forms.MenuItem ctxClose;
		
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Interface
        /// <summary>Constructor</summary>
		public winTrace() {
			//Constructor
			InitializeComponent();
			ArgixTrace.AddListener(new ArgixTextBoxListener(LogLevel.Debug, this.txtTrace));
		}
        /// <summary>Disposes of the resources (other than memory) used by the System.Windows.Forms.Form.</summary>
        /// <param name="disposing"></param>
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.ctxMain = new System.Windows.Forms.ContextMenu();
			this.ctxRefresh = new System.Windows.Forms.MenuItem();
			this.ctxSep1 = new System.Windows.Forms.MenuItem();
			this.ctxClear = new System.Windows.Forms.MenuItem();
			this.txtTrace = new System.Windows.Forms.RichTextBox();
			this.ctxClose = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// ctxMain
			// 
			this.ctxMain.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.ctxRefresh,
																					this.ctxClear,
																					this.ctxSep1,
																					this.ctxClose});
			// 
			// ctxRefresh
			// 
			this.ctxRefresh.Index = 0;
			this.ctxRefresh.Text = "Refresh";
			this.ctxRefresh.Popup += new System.EventHandler(this.OnMenuPopup);
			this.ctxRefresh.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep1
			// 
			this.ctxSep1.Index = 2;
			this.ctxSep1.Text = "-";
			// 
			// ctxClear
			// 
			this.ctxClear.Index = 1;
			this.ctxClear.Text = "Clear";
			this.ctxClear.Popup += new System.EventHandler(this.OnMenuPopup);
			this.ctxClear.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// txtTrace
			// 
			this.txtTrace.AcceptsTab = true;
			this.txtTrace.ContextMenu = this.ctxMain;
			this.txtTrace.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtTrace.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTrace.Location = new System.Drawing.Point(0, 0);
			this.txtTrace.Name = "txtTrace";
			this.txtTrace.ReadOnly = true;
			this.txtTrace.Size = new System.Drawing.Size(463, 272);
			this.txtTrace.TabIndex = 2;
			this.txtTrace.Text = "";
			this.txtTrace.WordWrap = false;
			// 
			// ctxClose
			// 
			this.ctxClose.Index = 3;
			this.ctxClose.Text = "Close";
			this.ctxClose.Popup += new System.EventHandler(this.OnMenuPopup);
			this.ctxClose.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// winTrace
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(463, 272);
			this.Controls.Add(this.txtTrace);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "winTrace";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Trace Events";
			this.TopMost = true;
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//
			} 
			catch(Exception ex) { throw ex; }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Even handler for form closing event
			this.Hide();
			e.Cancel = true;
		}
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event hanlder for menu click events
			try {
				MenuItem mnu = (MenuItem)sender;
				switch(mnu.Text) {
					case "Clear":	this.txtTrace.Clear(); break;
					case "Close":	this.Hide(); break;
				}
			}
			catch {}
		}
		private void OnMenuPopup(object sender, System.EventArgs e) {
			//Event handler for menu popup event
			try {
				this.ctxClear.Enabled = this.txtTrace.Focus();
				this.ctxClose.Enabled = true;
			}
			catch {}
		}
	}
}
