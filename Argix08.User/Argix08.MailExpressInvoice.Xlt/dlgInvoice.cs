using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Argix.Finance {
    //
    public partial class dlgInvoice :Form {
        //Members
        private const string USP_CLIENT = "uspInvClientGetList",TBL_CLIENT = "ClientTable";
        private const string USP_INVOICE = "uspInvClientInvoiceGetList",TBL_INVOICE = "ClientInvoiceTable";
        private const string CMD_CANCEL = "&Cancel";
        private const string CMD_OK = "O&K";		

        //Interface
        public dlgInvoice() {
            //Constructor
            try {
                InitializeComponent();
                this.btnOK.Text = CMD_OK;
                this.btnCancel.Text = CMD_CANCEL;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgInvoice instance.",ex); }
        }
        public string InvoiceNumber { get { return this.grdInvoice.SelectedRows[0].Cells["invoiceNumberDataGridViewTextBoxColumn"].Value.ToString(); } }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try
            {
                //Get client list
                SqlDataAdapter adapter = new SqlDataAdapter(USP_CLIENT, global::Argix.Finance.Settings.Default.SQLConnection);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.TableMappings.Add("Table", TBL_CLIENT);
                DataSet _ds = new DataSet();
                adapter.Fill(_ds, TBL_CLIENT);
                DataSet ds = new DataSet();
                string clientID = global::Argix.Finance.Settings.Default.ClientID;
                if (clientID.Length > 0)
                    ds.Merge(_ds.Tables[TBL_CLIENT].Select("DivisionNumber = '01' AND (" + clientID + ")" ));
                else
                    ds.Merge(_ds);
                this.mInvoiceDS.Merge(ds);
                //if (this.cboClient.Items.Count = 1)
                //cboClient.SelectedItem = null; 
                
                OnClientSelected(null, EventArgs.Empty);
                this.cboClient.Enabled = this.cboClient.Items.Count > 0;
            }
            catch (Exception ex) { reportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnClientSelected(object sender,EventArgs e) {
            //Event handler for change in selected client
            this.Cursor = Cursors.WaitCursor;
            try {
                //Get invoice list for selected client
                this.mInvoiceDS.ClientInvoiceTable.Clear();
                if(this.cboClient.Items.Count > 0) {
                    SqlDataAdapter adapter = new SqlDataAdapter(USP_INVOICE,global::Argix.Finance.Settings.Default.SQLConnection);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@ClientNumber",this.cboClient.SelectedValue),new SqlParameter("@ClientDivision",null),new SqlParameter("@StartDate",null) });
                    adapter.TableMappings.Add("Table",TBL_INVOICE);
                    adapter.Fill(this.mInvoiceDS,TBL_INVOICE);
                }
                if(this.grdInvoice.Rows.Count > 0) this.grdInvoice.Rows[0].Selected = true;
                OnInvoiceSelected(null,EventArgs.Empty);
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnInvoiceSelected(object sender,EventArgs e) {
            //Event handler for change in selected invoice
            setUserServices();
        }
        private void OnButtonClick(object sender,EventArgs e) {
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
                        this.Close();
                        break;
                    default: break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void setUserServices() { this.btnOK.Enabled = this.grdInvoice.SelectedRows.Count > 0; }
        private void reportError(Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
    }
}