using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Argix.AgentLineHaul {
    //
    public partial class frmMain:Form {
        //Members
        
        //Interface
        public frmMain() {
            //Constructor
            InitializeComponent();
            SvcLog.Device = this.txtLog;
        }
        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load
            try {
                SvcLog.LogMessage("UTILTIY STARTED");
                this.btnGo.Enabled = false;

                IDictionary oDict = (IDictionary)ConfigurationManager.GetSection("dirs");
                int dirs = Convert.ToInt32(oDict["count"]);
                for(int i=1;i<=dirs;i++) {
                    oDict = (IDictionary)ConfigurationManager.GetSection("dir" + i.ToString());
                    string name = oDict["name"].ToString();
                    string src = oDict["src"].ToString();
                    string pattern = oDict["pattern"].ToString();
                    string dest = oDict["dest"].ToString();
                    string terminal = oDict["terminal"].ToString();
                    FileMgr oSvc = new FileMgr(name,src,pattern,dest,terminal);
                    if(Program.TerminalCode.Trim() == terminal || Program.TerminalCode.Trim() == "All") 
                        this.cboTerminal.Items.Add(oSvc);
                }
                if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
                this.cboTerminal.Enabled = this.cboTerminal.Items.Count > 0;
                OnTerminalChanged(this.cboTerminal,EventArgs.Empty);
            }
            catch(Exception ex) { SvcLog.LogMessage("STARTUP ERROR\t" + ex.Message); }
        }
        private void OnFormClosed(object sender,FormClosedEventArgs e) {
            //Event handler for form closed
            try {
                //Close task tray icon if applicable; log application as stopped
            }
            catch(Exception) { }
            finally { Application.Exit(); }
        }
        private void OnTerminalChanged(object sender,EventArgs e) {
            //Event handler for change in terminal
            if(this.cboTerminal.SelectedItem == null) return;
            FileMgr svc = (FileMgr)this.cboTerminal.SelectedItem;
            this.lblSource.Text = svc.SourceFolder;
            this.lblPattern.Text = svc.SourceFilePattern;
            this.lblDestination.Text = svc.DestinationFolder;
            SvcLog.LogMessage("SOURCE FILES FOUND\t" + svc.SourceFiles.ToString());
            this.btnGo.Enabled = svc.SourceFiles > 0;
        }
        private void OnCommandClick(object sender,EventArgs e) {
            //Event handler for command button clicked
            Button btn = (Button)sender;
            switch(btn.Name) {
                case "btnClose":
                    this.Close();
                    break;
                case "btnGo":
                    FileMgr svc = (FileMgr)this.cboTerminal.SelectedItem;
                    svc.Execute();
                    this.btnGo.Enabled = false;
                    break;
            }
        }
    }
}
