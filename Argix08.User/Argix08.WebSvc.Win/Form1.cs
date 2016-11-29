using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WebSvcClient {
    public partial class frmMain :Form {
        //Members
        
        //Interface
        public frmMain() {
            //Constructor
            InitializeComponent();
        }
        private void OnLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Display the web service url from a Tracking carton instance
                this.txtService.Text = new Tracking.Tracker().Url;
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.btnTrack.Enabled = false; this.Cursor = Cursors.Default; }
        }
        private void OnValidate(object sender,EventArgs e) {
            //Validate post data
            this.btnTrack.Enabled = this.txtUserID.Text.Length > 0 &&
                                    this.txtPassword.Text.Length > 0 &&
                                    this.txtCartonNum.Text.Length > 0;
        }
        private void OnTrack(object sender,EventArgs e) {
            //Event handler to track a carton
            this.Cursor = Cursors.WaitCursor;
            try {
                //Clear prior results
                this.txtCartonDetail.ForeColor = System.Drawing.SystemColors.WindowText;
                this.txtCartonDetail.Text = "";

                //Create a custom SOAPHeader instance
                Tracking.SoapCredential sc = new Tracking.SoapCredential();
                sc.UserName = this.txtUserID.Text;
                sc.Password = this.txtPassword.Text;

                //Create Tracking web service client and add SOAP header credentials
                Tracking.Tracker tracker = new Tracking.Tracker();
                tracker.SoapCredentialValue = sc;

                //Track a carton and display the results
                Tracking.TrackDS ds = tracker.TrackCarton(this.txtCartonNum.Text);
                this.txtCartonDetail.Text = ds.GetXml();
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        #region Local Services: reportError()
        private void reportError(Exception ex) {
            //Report an error to the user
            this.txtCartonDetail.ForeColor = System.Drawing.Color.Red;
            this.txtCartonDetail.Text = ex.ToString();
        }
        #endregion
    }
}