//	File:	ArgixStatusBar.cs
//	Author:	J. Heary
//	Date:	07/02/04
//	Desc:	Status bar for Argix applications.
//	Rev:	11/17/04 (jph)- increased Terminal panel width; exposed Panels 
//			collection.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Argix.Windows {
	//
	public class ArgixStatusBar : System.Windows.Forms.UserControl {
		//Members
		private Icon icon_on=null;
		private Icon icon_off=null;
		
		#region Controls
		private System.Windows.Forms.StatusBar stbMain;
		private System.Windows.Forms.StatusBarPanel pnlStatus;
		private System.Windows.Forms.StatusBarPanel pnlTerminal;
		private System.Windows.Forms.StatusBarPanel pnlOnline;
		private System.Windows.Forms.StatusBarPanel pnlUser1;
		private System.Windows.Forms.StatusBarPanel pnlUser2;
		
		private System.ComponentModel.Container components = null;	//Required designer variable
		#endregion
		
		//Interface
		public ArgixStatusBar() {
			//This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			
			//Create online icons
            Stream online = Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Windows.online.ico");
            Stream offline = Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Windows.offline.ico");
			this.icon_on = new Icon(online);
			this.icon_off = new Icon(offline);
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ArgixStatusBar));
			this.stbMain = new System.Windows.Forms.StatusBar();
			this.pnlStatus = new System.Windows.Forms.StatusBarPanel();
			this.pnlTerminal = new System.Windows.Forms.StatusBarPanel();
			this.pnlOnline = new System.Windows.Forms.StatusBarPanel();
			this.pnlUser1 = new System.Windows.Forms.StatusBarPanel();
			this.pnlUser2 = new System.Windows.Forms.StatusBarPanel();
			((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlTerminal)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlOnline)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlUser1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlUser2)).BeginInit();
			this.SuspendLayout();
			// 
			// stbMain
			// 
			this.stbMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stbMain.Location = new System.Drawing.Point(0, 0);
			this.stbMain.Name = "stbMain";
			this.stbMain.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					   this.pnlStatus,
																					   this.pnlUser1,
																					   this.pnlUser2,
																					   this.pnlTerminal,
																					   this.pnlOnline});
			this.stbMain.ShowPanels = true;
			this.stbMain.Size = new System.Drawing.Size(531, 24);
			this.stbMain.SizingGrip = false;
			this.stbMain.TabIndex = 12;
			this.stbMain.PanelClick += new System.Windows.Forms.StatusBarPanelClickEventHandler(this.OnPanelClick);
			// 
			// pnlStatus
			// 
			this.pnlStatus.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.pnlStatus.MinWidth = 96;
			this.pnlStatus.Width = 267;
			// 
			// pnlTerminal
			// 
			this.pnlTerminal.MinWidth = 96;
			this.pnlTerminal.Text = "Terminal";
			this.pnlTerminal.ToolTipText = "Terminal";
			this.pnlTerminal.Width = 192;
			// 
			// pnlOnline
			// 
			this.pnlOnline.Icon = ((System.Drawing.Icon)(resources.GetObject("pnlOnline.Icon")));
			this.pnlOnline.MinWidth = 24;
			this.pnlOnline.Width = 24;
			// 
			// pnlUser1
			// 
			this.pnlUser1.MinWidth = 24;
			this.pnlUser1.Width = 24;
			// 
			// pnlUser2
			// 
			this.pnlUser2.MinWidth = 24;
			this.pnlUser2.Width = 24;
			// 
			// ArgixStatusBar
			// 
			this.Controls.Add(this.stbMain);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Name = "ArgixStatusBar";
			this.Size = new System.Drawing.Size(531, 24);
			this.Resize += new System.EventHandler(this.OnControlResize);
			((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlTerminal)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlOnline)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlUser1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnlUser2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		public StatusBar.StatusBarPanelCollection Panels { get { return this.stbMain.Panels; } }
		public StatusBarPanel StatusPanel { get { return this.pnlStatus; } }
		public StatusBarPanel User1Panel { get { return this.pnlUser1; } }
		public StatusBarPanel User2Panel { get { return this.pnlUser2; } }
		public StatusBarPanel TerminalPanel { get { return this.pnlTerminal; } }
		public StatusBarPanel OnlinePanel { get { return this.pnlOnline; } }
		public string StatusText { get { return this.pnlStatus.Text; } set { this.pnlStatus.Text = this.pnlStatus.ToolTipText = value; } }
		public string TerminalText { get { return this.pnlTerminal.Text; } set { this.pnlTerminal.Text = value; } }
		public void SetTerminalPanel(string id, string name) { this.pnlTerminal.Text = name; this.pnlTerminal.ToolTipText = id; }
		public void SetOnlinePanel(OnlineIcon image, string tooltip) { this.pnlOnline.Icon = (image==OnlineIcon.On) ? this.icon_on : this.icon_off; this.pnlOnline.ToolTipText = tooltip; }
        public void OnOnlineStatusUpdate(object sender,OnlineStatusArgs e) {
            //Event handler for notifications from mediator
            this.pnlOnline.Icon = (e.OnLine) ? this.icon_on : this.icon_off;
            this.pnlOnline.ToolTipText = e.Url;
        }
        private void OnControlResize(object sender,System.EventArgs e) {
			//Event hanlder for control resize event
			this.pnlOnline.Width = this.stbMain.Height;
		}
		private void OnPanelClick(object sender, System.Windows.Forms.StatusBarPanelClickEventArgs e) {
			//Event handler for status panel clicked
			this.pnlStatus.Text = this.pnlStatus.ToolTipText = "";
		}
	}
}
