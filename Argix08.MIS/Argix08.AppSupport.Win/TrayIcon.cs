using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Windows.Forms;

namespace Argix.Windows {
	//
	public class TrayIcon {
		//Members
        private NotifyIcon mTrayIcon=null;
        //private ContextMenu mMenu=null;

        private const string MNU_ICON_SHOWNEWISSUEALERT = "Show Desktop Alert";
        private const string MNU_ICON_HIDEWHENMINIMIZED = "Hide When Minimized";
        private const string MNU_ICON_SHOW = "Open";

        public event EventHandler ShowNewIssueAlertChanged=null;
        public event EventHandler HideWhenMinimizedChanged=null;
        public event EventHandler Unhide=null;
        public event EventHandler TrayIconError=null;
        		
		//Interface
		public TrayIcon(string iconText, Icon icon) {
			//Constructor
			try {
                this.mTrayIcon = new NotifyIcon();
                this.mTrayIcon.Text = iconText;
                this.mTrayIcon.Icon = icon;
                this.mTrayIcon.Visible = true;
                MenuItem ctxAlert = new MenuItem(MNU_ICON_SHOWNEWISSUEALERT,new System.EventHandler(this.OnTrayIconItemClick));
                ctxAlert.Index = 0;
                ctxAlert.Checked = true;
                MenuItem ctxSep1 = new MenuItem("-");
                ctxSep1.Index = 1;
                MenuItem ctxHide = new MenuItem(MNU_ICON_HIDEWHENMINIMIZED,new System.EventHandler(this.OnTrayIconItemClick));
                ctxHide.Index = 2;
                ctxHide.Checked = true;
                MenuItem ctxShow = new MenuItem(MNU_ICON_SHOW,new System.EventHandler(this.OnTrayIconItemClick));
                ctxShow.Index = 3;
                ctxShow.DefaultItem = true;
                this.mTrayIcon.ContextMenu = new ContextMenu(new MenuItem[] { ctxAlert,ctxSep1,ctxHide,ctxShow });
                this.mTrayIcon.DoubleClick += new System.EventHandler(OnTrayIconDoubleClick);
                this.mTrayIcon.BalloonTipClicked += new EventHandler(OnTrayIconBalloonTipClick);
            }
            catch { if(this.TrayIconError != null) TrayIconError(this,EventArgs.Empty); }
		}
		public void Dispose() { this.mTrayIcon.Dispose(); }
		
        public string Text { get { return this.mTrayIcon.Text; } set { this.mTrayIcon.Text = value; } }
		public bool Visible { get { return this.mTrayIcon.Visible; } set { this.mTrayIcon.Visible = value; } }
        public bool ShowAlert { get { return this.mTrayIcon.ContextMenu.MenuItems[0].Checked; } set { this.mTrayIcon.ContextMenu.MenuItems[0].Checked = value; } }
        public bool HideWhenMinimized { get { return this.mTrayIcon.ContextMenu.MenuItems[2].Checked; } set { this.mTrayIcon.ContextMenu.MenuItems[2].Checked = value; } }
        public void ShowBalloonTip(int timeout) { this.mTrayIcon.ShowBalloonTip(timeout); }
        public void ShowBalloonTip(int timeout,string tipTitle,string tipText,ToolTipIcon tipIcon) { this.mTrayIcon.ShowBalloonTip(timeout,tipTitle,tipText,tipIcon); }
        
        private void OnTrayIconItemClick(object sender,System.EventArgs e) {
            //Menu itemclicked-apply selected service
            try {
                MenuItem menu = (MenuItem)sender;
                switch(menu.Text) {
                    case MNU_ICON_SHOWNEWISSUEALERT:
                        this.mTrayIcon.ContextMenu.MenuItems[0].Checked = !this.mTrayIcon.ContextMenu.MenuItems[0].Checked;
                        if(this.ShowNewIssueAlertChanged != null) ShowNewIssueAlertChanged(this,EventArgs.Empty);
                        break;
                    case MNU_ICON_HIDEWHENMINIMIZED:
                        this.mTrayIcon.ContextMenu.MenuItems[2].Checked = !this.mTrayIcon.ContextMenu.MenuItems[2].Checked;
                        if(this.HideWhenMinimizedChanged != null) HideWhenMinimizedChanged(this,EventArgs.Empty);
                        break;
                    case MNU_ICON_SHOW: 
                        if(this.Unhide != null) Unhide(this,EventArgs.Empty); 
                        break;
                }
            }
            catch { if(this.TrayIconError != null) TrayIconError(this,EventArgs.Empty); }
        }
        private void OnTrayIconDoubleClick(object Sender,EventArgs e) {
            //Show the form when the user double clicks on the notify icon
            // Set the WindowState to normal if the form is minimized.
            try {
                this.mTrayIcon.ContextMenu.MenuItems[3].PerformClick();
            }
            catch { if(this.TrayIconError != null) TrayIconError(this,EventArgs.Empty); }
        }
        private void OnTrayIconBalloonTipClick(object sender,EventArgs e) {
            //Event handler for balloon tip clicked
        }
    }
}
