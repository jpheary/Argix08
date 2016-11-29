using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace Argix.Freight {
    //
    public partial class Main:Form {
        //Members
        private ScannerAssignmentDS mAssignment=null;
        private const string SCANNER_ID = "00001";

        [System.Runtime.InteropServices.DllImport("itc50.dll")]
        public static extern int ITCGetSerialNumber(ref string number, int buffSize);

        //Interface
        public Main() {
            //Constructor
            InitializeComponent();
        }

        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form laod event
            Cursor.Current = Cursors.WaitCursor;
            try {
                this.lblTerminal.Text = "";
                this.lblFreight.Text = "";
                this.lblClient.Text = "";
                this.lblCartons.Text = "";
                this.lblSortType.Text = "";
                this.lblUser.Text = "jheary";
                
                string number="";
                int ret = ITCGetSerialNumber(ref number, 25);

                this.stbMain.Text = "Scanner: # (id=" + number + ")";
                this.txtItem.Text = "";
                this.txtItem.Enabled = false;
                
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { Cursor.Current = Cursors.Default; }
        }
        private void OnClosing(object sender,CancelEventArgs e) {
            e.Cancel = false;
        }
        private void OnClosed(object sender,EventArgs e) {
            //Event handler for form closed event
            Application.Exit();
        }
        private void OnScanChanged(object sender,EventArgs e) {
            //Event handler for change in scan text
            Cursor.Current = Cursors.WaitCursor;
            try {
                if(this.txtItem.Text.Trim().Length > 0) {
                    //Generate a sorted item number for this object; format: yMMddssssswww
                    //Get the number of elasped seconds since midnight; format: 00000
                    //***Scanning faster than 1sec produces duplicates: limt to 1sec/scan (Thread.Sleep) or change formula???
                    DateTime dt = DateTime.Now;
                    int sec = ((3600 * dt.Hour) + (60 * dt.Minute) + dt.Second);

                    ScannedItemDS items = new ScannedItemDS();
                    ScannedItemDS.ScannedItemTableRow item = items.ScannedItemTable.NewScannedItemTableRow();
                    item.ItemNumber = dt.ToString("yyyy").Substring(3,1) + dt.ToString("MM") +  dt.ToString("dd") + sec + this.mAssignment.ScannerAssignmentTable[0].ScannerNumber.Trim().PadLeft(3,'0');
                    item.TerminalID = this.mAssignment.ScannerAssignmentTable[0].TerminalID;
                    item.ScannerID = this.mAssignment.ScannerAssignmentTable[0].ScannerID;
                    item.FreightID = this.mAssignment.ScannerAssignmentTable[0].FreightID;
                    item.SortDate = DateTime.Now;
                    item.ScanString = this.txtItem.Text;
                    item.UserID = this.lblUser.Text;
                    items.ScannedItemTable.AddScannedItemTableRow(item);

                    Argix.Freight.ScannerService scannerSvc = new ScannerService();
                    scannerSvc.Url = Settings.Default.WebServiceURL;
                    bool ret = scannerSvc.CreateSortedItem(items);
                    if(ret) this.txtItem.Text = "";
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { Cursor.Current = Cursors.Default; }
        }
        private void OnRefresh(object sender,EventArgs e) {
            //Event handler for refresh menu item clicked
            Cursor.Current = Cursors.WaitCursor;
            try {
                ScannerService scannerSvc = new ScannerService();
                scannerSvc.Url = Settings.Default.WebServiceURL;
                //scannerSvc.Credentials =  getCredentials();
                scannerSvc.Timeout = 60000;
                this.mAssignment = scannerSvc.GetScannerAssignment(SCANNER_ID);
                if(this.mAssignment.ScannerAssignmentTable.Rows.Count > 0) {
                    this.lblTerminal.Text = this.mAssignment.ScannerAssignmentTable[0].TerminalID.ToString();
                    this.lblFreight.Text = this.mAssignment.ScannerAssignmentTable[0].FreightID;
                    this.lblClient.Text = this.mAssignment.ScannerAssignmentTable[0].ClientNumber + " " + this.mAssignment.ScannerAssignmentTable[0].ClientDivisionNumber;
                    this.lblCartons.Text = this.mAssignment.ScannerAssignmentTable[0].Cartons.ToString();
                    this.lblSortType.Text = this.mAssignment.ScannerAssignmentTable[0].SortTypeID.ToString();
                    this.stbMain.Text = "Scanner: #" + this.mAssignment.ScannerAssignmentTable[0].ScannerNumber + " (id=" + this.mAssignment.ScannerAssignmentTable[0].ScannerID + ")";
                }
                else
                    MessageBox.Show("No assignments found.","Scanner Sort",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
                this.txtItem.Enabled = this.mAssignment.ScannerAssignmentTable.Rows.Count > 0;
                this.txtItem.Focus();
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { Cursor.Current = Cursors.Default; }
        }
        private void OnDelete(object sender,EventArgs e) {
            //Event handler for delete menu item clicked
            ScannerService scannerSvc = new ScannerService();
            MessageBox.Show(scannerSvc.HelloWorld(),"Scanner Sort",MessageBoxButtons.OK,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1);
        }
        private ICredentials getCredentials() {
            //Determine credentials for web service access
            ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            if(Settings.Default.UseSpecificCredentials) {
                string username = Settings.Default.Username;
                string password = Settings.Default.Password;
                string domain = Settings.Default.Domain;
                credentials = new NetworkCredential(username,password,domain);
            }
            return credentials;
        }

    }
}