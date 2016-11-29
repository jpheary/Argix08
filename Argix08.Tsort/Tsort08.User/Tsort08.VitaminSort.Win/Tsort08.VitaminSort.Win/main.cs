using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Windows;

namespace Argix.Freight {
    //
    public partial class frmMain:Form {
        //
        public frmMain() {
            InitializeComponent();
        }
        private void OnFormLoad(object sender,System.EventArgs e) {
            //Load conditions
            this.Cursor = Cursors.WaitCursor;
            try {
                //Initialize controls
                Splash.Close();
                this.Visible = true;
                Application.DoEvents();

                //Set control defaults
                Workstation w = FreightProxy.GetStation("JAST001");
                this.Text += w.Number;
                this.mAssignmentDS.Merge(FreightProxy.GetFreightAssignments(w.WorkStationID));
            }
            catch(Exception ex) { App.ReportError(ex,true); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Event handler for form closing event
            if(!e.Cancel) {
                //global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                //global::Argix.Properties.Settings.Default.Location = this.Location;
                //global::Argix.Properties.Settings.Default.Size = this.Size;
                //global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                //global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                //global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                //global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnFormResize(object sender,System.EventArgs e) {
            //Event handler for form resized event
        }
        private void OnInput1Changed(object sender,EventArgs e) {
            //Event handler for change in Input 1
            setUserServices();
        }
        private void OnInput2Changed(object sender,EventArgs e) {
            //Event handler for change in Input 1
            setUserServices();
        }
        private void OnOk(object sender,EventArgs e) {
            //Event handler for Ok button clicked
            string[] inputs = new string[] { this.txtInput1.Text };
            decimal weight = 10;
            string freightID = this.mAssignmentDS.AssignmentsTable[0].FreightID;
            SortedItem si = FreightProxy.ProcessInputs(inputs,weight,"00","",freightID);
            //Print
        }
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu(), OnHelpMenuClick()
        private void configApplication() {
            try {
                //Create business objects with configuration values
            }
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); }
        }
        private void setUserServices() {
            //Set user services
            try {
                //Set menu states
                this.btnOk.Enabled = this.txtInput1.Text.Length == this.txtInput1.MaxLength;
            }
            catch(Exception ex) { App.ReportError(ex,false); }
            finally { Application.DoEvents(); }
        }
        #endregion
    }
}
