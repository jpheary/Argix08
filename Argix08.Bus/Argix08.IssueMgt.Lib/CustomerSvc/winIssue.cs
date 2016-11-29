using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Argix.CustomerSvc {
    //
    public partial class winIssue:Form {
        //Members
        private Issue mIssue=null;

        //Interface
        public winIssue(Issue issue) {
            //Constructor
            try {
                InitializeComponent();
                this.mIssue = issue;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new winIssue instance.",ex); }
        }
        public Issue Issue { get { return this.mIssue; } }
        public void Search(string searchText) {
            //Search
            this.issueInspector1.Search(searchText);
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.Text = this.mIssue.Subject + " (" + this.mIssue.ID.ToString()+ ")";
                this.issueInspector1._Issue = this.mIssue;
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnIssueInspError(object source,Argix.ControlErrorEventArgs e) {
            //Event handler for error in Issue Inspector
            reportError(e.Exception);
        }
        #region Local Services: reportError()
        private void reportError(Exception ex) {
            MessageBox.Show("Unexpected error while showing the selected issue:\n\n" + ex.ToString());
        }
        #endregion
    }
}