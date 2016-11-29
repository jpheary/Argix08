//	File:	frmpanda.cs
//	Author:	J. Heary
//	Date:	02/11/06
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Argix;
using Argix.Data;

namespace Tsort.PandA {
	//
	internal class frmPanda : System.Windows.Forms.Form {
		//Members
		private Icon icon_on=null;
		private Icon icon_off=null;
		
		//Constants
		#region Controls

		private System.Windows.Forms.StatusBar stbDialog;
		private System.Windows.Forms.StatusBarPanel stbStatus;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabSort;
		private Tsort.PandA.PandaUI pandaUI1;
		private Tsort.Sort.SortUI sortUI1;
		private System.Windows.Forms.TabPage tabPanda;
		private System.Windows.Forms.StatusBarPanel pnlOnline;
		private System.ComponentModel.IContainer components=null;
		#endregion
				
		//Events
		//Interface
		public frmPanda(PandaService pandaService): this(pandaService, LogLevel.Error) { }
		public frmPanda(PandaService pandaService, LogLevel level) {
			//Constructor
			try {
				//Designer required
				InitializeComponent();
				
				//Create online icons
				Stream online = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort.PandA.online.ico");
				Stream offline = Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort.PandA.offline.ico");
				this.icon_on = new Icon(online);
				this.icon_off = new Icon(offline);
				
				//Init user services
				this.pandaUI1.PandaSvc = pandaService;
				this.pandaUI1.TraceOn = level;
				this.sortUI1.Operator = pandaService.SortOperator;
				this.sortUI1.Operator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new frmPanda instance.", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if (components != null) components.Dispose(); } base.Dispose(disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmPanda));
			this.stbDialog = new System.Windows.Forms.StatusBar();
			this.stbStatus = new System.Windows.Forms.StatusBarPanel();
			this.pnlOnline = new System.Windows.Forms.StatusBarPanel();
			this.tabMain = new System.Windows.Forms.TabControl();
			this.tabSort = new System.Windows.Forms.TabPage();
			this.sortUI1 = new Tsort.Sort.SortUI();
			this.tabPanda = new System.Windows.Forms.TabPage();
			this.pandaUI1 = new Tsort.PandA.PandaUI();
			((System.ComponentModel.ISupportInitialize)(this.stbStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlOnline)).BeginInit();
			this.tabMain.SuspendLayout();
			this.tabSort.SuspendLayout();
			this.tabPanda.SuspendLayout();
			this.SuspendLayout();
			// 
			// stbDialog
			// 
			this.stbDialog.Location = new System.Drawing.Point(0, 320);
			this.stbDialog.Name = "stbDialog";
			this.stbDialog.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						 this.stbStatus,
																						 this.pnlOnline});
			this.stbDialog.ShowPanels = true;
			this.stbDialog.Size = new System.Drawing.Size(586, 24);
			this.stbDialog.SizingGrip = false;
			this.stbDialog.TabIndex = 8;
			this.stbDialog.Text = "stbDialog";
			// 
			// stbStatus
			// 
			this.stbStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.stbStatus.Width = 550;
			// 
			// pnlOnline
			// 
			this.pnlOnline.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.pnlOnline.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlOnline.Icon")));
			this.pnlOnline.Width = 36;
			// 
			// tabMain
			// 
			this.tabMain.Controls.Add(this.tabSort);
			this.tabMain.Controls.Add(this.tabPanda);
			this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.tabMain.Location = new System.Drawing.Point(0, 0);
			this.tabMain.Multiline = true;
			this.tabMain.Name = "tabMain";
			this.tabMain.SelectedIndex = 0;
			this.tabMain.Size = new System.Drawing.Size(586, 320);
			this.tabMain.TabIndex = 14;
			// 
			// tabSort
			// 
			this.tabSort.Controls.Add(this.sortUI1);
			this.tabSort.Location = new System.Drawing.Point(4, 22);
			this.tabSort.Name = "tabSort";
			this.tabSort.Size = new System.Drawing.Size(578, 294);
			this.tabSort.TabIndex = 2;
			this.tabSort.Text = "Sort";
			// 
			// sortUI1
			// 
			this.sortUI1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sortUI1.Location = new System.Drawing.Point(0, 0);
			this.sortUI1.Name = "sortUI1";
			this.sortUI1.Operator = null;
			this.sortUI1.RefreshCacheVisible = true;
			this.sortUI1.RefreshVisible = true;
			this.sortUI1.Size = new System.Drawing.Size(578, 294);
			this.sortUI1.TabIndex = 0;
			this.sortUI1.TraceOn = Argix.LogLevel.None;
			// 
			// tabPanda
			// 
			this.tabPanda.Controls.Add(this.pandaUI1);
			this.tabPanda.Location = new System.Drawing.Point(4, 22);
			this.tabPanda.Name = "tabPanda";
			this.tabPanda.Size = new System.Drawing.Size(578, 294);
			this.tabPanda.TabIndex = 1;
			this.tabPanda.Text = "Panda";
			this.tabPanda.Visible = false;
			// 
			// pandaUI1
			// 
			this.pandaUI1.CartonsOn = true;
			this.pandaUI1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pandaUI1.Location = new System.Drawing.Point(0, 0);
			this.pandaUI1.MessagesOn = false;
			this.pandaUI1.Name = "pandaUI1";
			this.pandaUI1.PandaSvc = null;
			this.pandaUI1.Size = new System.Drawing.Size(578, 294);
			this.pandaUI1.TabIndex = 0;
            this.pandaUI1.TraceOn = Argix.LogLevel.Debug;
			// 
			// frmPanda
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(586, 344);
			this.Controls.Add(this.tabMain);
			this.Controls.Add(this.stbDialog);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmPanda";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "PandA Library";
			this.Resize += new System.EventHandler(this.OnFormResize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.Closed += new System.EventHandler(this.OnFormClosed);
			((System.ComponentModel.ISupportInitialize)(this.stbStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlOnline)).EndInit();
			this.tabMain.ResumeLayout(false);
			this.tabSort.ResumeLayout(false);
			this.tabPanda.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			try { } catch(Exception ex) { reportError(ex); }
		}
		private void OnFormResize(object sender, System.EventArgs e) {
			//Event handler for change in form size
			try { if(this.WindowState == FormWindowState.Minimized) this.Visible = false; } catch(Exception ex) { reportError(ex); }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing event
				this.Hide(); e.Cancel = true;
		}
		private void OnFormClosed(object sender, System.EventArgs e) { }
		private void OnShowForm(object sender, EventArgs e) {
			//Event handler for request to make this form visible
			this.WindowState = FormWindowState.Maximized;
			this.Visible = true;
			this.Activate();
		}
		public void OnDataStatusUpdate(object sender, DataStatusArgs e) {
			//Event handler for notifications from mediator
			this.pnlOnline.Icon = (e.Online) ? this.icon_on : this.icon_off;
			this.pnlOnline.ToolTipText = e.Connection;
		}
		#region Local services: reportError()
		private void reportError(Exception ex) {
			//Report an exception to the user
			this.stbDialog.Text = ex.Message;
		}
		#endregion
	}
}