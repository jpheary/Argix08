//	File:	dlgDeliveries.cs
//	Author:	J. Heary
//	Date:	01/22/09
//	Desc:	Dialog to display deliveries.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Argix.Customers {
    //
    public partial class dlgDeliveries :Form {
        //Members
        private int mCompanyID=0;
        private int mStoreNumber=0;
        private DateTime mFrom=DateTime.Today;
        private DateTime mTo = DateTime.Now;
        private long mPROID = 0;
        private long mPRONumber = 0;
        
        private const string CMD_CANCEL = "&Cancel";
        private const string CMD_OK = "O&K";

        //Interface
        public dlgDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Constructor
            try {
                InitializeComponent();
                this.mCompanyID = companyID;
                this.mStoreNumber = storeNumber;
                this.mFrom = from;
                this.mTo = to;

                this.btnOk.Text = CMD_OK;
                this.btnCancel.Text = CMD_CANCEL;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgStoreLocations instance.",ex); }
        }
        public long PROID { get { return this.mPROID; } }
        public long PRONumber { get { return this.mPRONumber; } }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Set initial service states
                this.Visible = true;
                this.mDeliveryDS.Merge(CustomerProxy.GetDeliveries(this.mCompanyID,this.mStoreNumber,this.mFrom,this.mTo));
            }
            catch(Exception ex) { reportError(ex); }
            finally { OnValidateForm(null,EventArgs.Empty);  this.Cursor = Cursors.Default; }
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Event handler for control value changes
            try {
                this.btnOk.Enabled = (this.grdDeliveries.Selected != null && this.grdDeliveries.Selected.Rows.Count > 0);
            }
            catch { }
        }
        private void OnDeliverySelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for delivery selection
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnCmdClick(object sender,System.EventArgs e) {
            //Command button handler
            try {
                Button btn = (Button)sender;
                switch(btn.Text) {
                    case CMD_CANCEL:
                        //Close the dialog
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                    case CMD_OK:
                        //General
                        this.Cursor = Cursors.WaitCursor;
                        this.DialogResult = DialogResult.OK;
                        this.mPROID = Convert.ToInt64(this.grdDeliveries.Selected.Rows[0].Cells["CPROID"].Value);
                        this.mPRONumber = Convert.ToInt64(this.grdDeliveries.Selected.Rows[0].Cells["CPRONumber"].Value);
                        this.Close();
                        break;
                    default: break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        #region Local Services: reportError()
        private void reportError(Exception ex) { MessageBox.Show("Unexpected error:\n\n" + ex.ToString()); }
        #endregion
    }
}