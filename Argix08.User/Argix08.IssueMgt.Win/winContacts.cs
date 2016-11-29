using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Argix.Customers {
    //
    public partial class winContacts:Form {
        //Members
        private Contact mContact=null;
        private const string MNU_NEW = "&New...";
        private const string MNU_OPEN = "&Open...";
        private const string MNU_REFRESH = "&Refresh";

        //Interface
        public winContacts() {
            //Constructor
            InitializeComponent();
            this.ctxNew.Text = MNU_NEW;
            this.ctxOpen.Text = MNU_OPEN;
            this.ctxRefresh.Text = MNU_REFRESH;
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form laod event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mContactDS.Merge(CustomerProxy.GetContacts());
                this.grdContacts.DataBind();
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for after selection changes
            this.Cursor = Cursors.WaitCursor;
            try {
                //Update the selected contact
                this.mContact=null;
                if(this.grdContacts.Selected.Rows.Count > 0) {
                    this.mContact = CustomerProxy.GetContact(Convert.ToInt32(this.grdContacts.Selected.Rows[0].Cells["ID"].Value));
                }
            }
            catch { }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnGridDoubleClick(object sender,EventArgs e) {
            //Event handler for grid selection double-clicked
            if(this.ctxOpen.Enabled) this.ctxOpen.PerformClick();
        }
        #region User Services: OnMenuItemClicked()
        private void OnMenuClick(object sender,System.EventArgs e) {
            //Event handler fo menu item clicked
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                switch(menu.Text) {
                    case MNU_NEW: break;
                    case MNU_OPEN:
                        dlgContact dlg = new dlgContact(this.mContact);
                        dlg.Font = this.Font;
                        if(dlg.ShowDialog() == DialogResult.OK) {
                            if(CustomerProxy.UpdateContact(this.mContact))
                                this.ctxRefresh.PerformClick();
                        }
                        break;
                    case MNU_REFRESH:
                        this.Cursor = Cursors.WaitCursor;
                        this.mContactDS.Clear();
                        this.mContactDS.Merge(CustomerProxy.GetContacts());
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        #endregion
        #region Local Services: setUserServices(), reportError()
        private void setUserServices() {
            //Set user services
            this.ctxNew.Enabled = false;
            this.ctxOpen.Enabled = this.grdContacts.Selected.Rows.Count > 0;
            this.ctxRefresh.Enabled = true;
        }
        private void reportError(Exception ex) { MessageBox.Show("Unexpected error:\n\n" + ex.ToString()); }
        #endregion
    }
}