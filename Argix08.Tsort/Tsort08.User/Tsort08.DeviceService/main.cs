//	File:	main.cs
//	Author:	J. Heary
//	Date:	10/14/08
//	Desc:	Utility interface and scheduler.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;
using Tsort.Devices.Scales;
using Tsort.Devices.Printers;

namespace Tsort.Devices {
	//
    public class frmMain:System.Windows.Forms.Form {
		//Members
        private DeviceService mService = null;
        private NotifyIcon mTrayIcon=null;
		private const int CTX_ON=0, CTX_OFF=1, CTX_SEP1=2, CTX_UI=3;
        private Label lblWeight;
		private System.ComponentModel.Container components = null;
		
		//Interface
        public frmMain(DeviceService svc) {
			//Constructor
			try {
				//Required for designer support
				InitializeComponent();
                this.mService = svc;
                this.mService.WeightChanged += new ScaleEventHandler(OnWeightChanged);
                #region Tray Icon
                this.mTrayIcon = new NotifyIcon();
                this.mTrayIcon.Text = "Scale Service";
                this.mTrayIcon.Icon = this.Icon;
                this.mTrayIcon.Visible = true;
                MenuItem ctxOn = new MenuItem("Turn On",new System.EventHandler(this.OnMenuItemClicked));
                ctxOn.Index = CTX_ON;
                ctxOn.Enabled = true;
                ctxOn.Visible = true;
                MenuItem ctxOff = new MenuItem("Turn Off",new System.EventHandler(this.OnMenuItemClicked));
                ctxOff.Index = CTX_OFF;
                ctxOff.Enabled = false;
                ctxOff.Visible = true;
                MenuItem ctxSep1 = new MenuItem("-",new System.EventHandler(this.OnMenuItemClicked));
                ctxSep1.Index = CTX_SEP1;
                ctxSep1.Visible = true;
                MenuItem ctxUI = new MenuItem("View Scale",new System.EventHandler(this.OnMenuItemClicked));
                ctxUI.Index = CTX_UI;
                ctxUI.Enabled = true;
                ctxUI.Visible = true;
                ctxUI.DefaultItem = true;
                this.mTrayIcon.ContextMenu = new ContextMenu(new MenuItem[] { ctxOn,ctxOff,ctxSep1,ctxUI });
                this.mTrayIcon.DoubleClick += new System.EventHandler(OnIconDoubleClick);
                #endregion
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new frmMain instance.",ex); }
		}
		protected override void Dispose(bool disposing) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lblWeight = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWeight
            // 
            this.lblWeight.BackColor = System.Drawing.SystemColors.Highlight;
            this.lblWeight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblWeight.Font = new System.Drawing.Font("Microsoft Sans Serif",36F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.lblWeight.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblWeight.Location = new System.Drawing.Point(0,0);
            this.lblWeight.Name = "lblWeight";
            this.lblWeight.Size = new System.Drawing.Size(127,84);
            this.lblWeight.TabIndex = 0;
            this.lblWeight.Text = "0.0";
            this.lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5,13);
            this.ClientSize = new System.Drawing.Size(127,84);
            this.Controls.Add(this.lblWeight);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AutoScale";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnClosed);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load
            try {
                this.Visible = false;
                this.mService.Start();
                this.mTrayIcon.ContextMenu.MenuItems[CTX_ON].PerformClick();
            }
            catch(Exception ex) { reportError(ex,true,false); }
            finally { setServices(); }
		}
        private void OnClosing(object sender,FormClosingEventArgs e) {
            //
            if(e.CloseReason == CloseReason.UserClosing) {
                this.Visible = false;
                e.Cancel = true;
            }
        }
        private void OnClosed(object sender,FormClosedEventArgs e) {
            //Event handler for form closed
            try {
                //Close task tray icon if applicable; log application as stopped
                if(this.mTrayIcon != null) this.mTrayIcon.Visible = false;
                this.Visible = false;
            }
            catch(Exception ex) { reportError(ex,true,false); }
            finally { Application.Exit(); }
        }
        private void OnWeightChanged(object source,ScaleEventArgs e) {
            //
            try {
                if(this.InvokeRequired) 
                    this.Invoke(new ScaleEventHandler(OnWeightChanged),new object[] { source,e });
                else 
                    this.lblWeight.Text = e.Weight.ToString();
            }
            catch(Exception ex) { reportError(ex,true,false); }
        }
        #region Tasktray Icon: OnMenuClick(), OnIconDoubleClick()
        private void OnMenuItemClicked(object sender,System.EventArgs e) {
			//Event handler for user clicks on the notify icon
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Index)  {
                    case CTX_ON:    
                        this.mService.Scale.TurnOn();
                        this.mService.Printer.TurnOn();
                        break;
                    case CTX_OFF:
                        this.mService.Scale.TurnOff();
                        this.mService.Printer.TurnOff();
                        break;
                    case CTX_UI:    this.Visible = true; break;
                }
			}
            catch(Exception ex) { reportError(ex,true,false); }
            finally { setServices(); }
		}
		private void OnIconDoubleClick(object Sender, EventArgs e) {
			//Event handler for user double clicks on the notify icon
            this.mTrayIcon.ContextMenu.MenuItems[CTX_UI].PerformClick();
		}
        #endregion
        #region Local Services: setServices(), reportError()
        private void setServices() {
			//Set menu states
			try {
				if(this.mTrayIcon != null) {
                    this.mTrayIcon.ContextMenu.MenuItems[CTX_ON].Enabled = (this.mService.Scale != null ? !this.mService.Scale.IsOn() : false);
                    this.mTrayIcon.ContextMenu.MenuItems[CTX_OFF].Enabled = (this.mService.Scale != null ? this.mService.Scale.IsOn() : false);
                    this.mTrayIcon.ContextMenu.MenuItems[CTX_UI].Enabled = true;
                }
			}
            catch(Exception ex) { reportError(ex,true,false); }
        }
        private void reportError(Exception ex,bool displayMessage,bool logMessage) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage)
                    MessageBox.Show(msg,"Scale Service",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch(Exception) { }
        }
        #endregion
    }
}
