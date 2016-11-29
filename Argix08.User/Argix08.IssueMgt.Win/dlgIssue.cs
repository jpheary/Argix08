//	File:		dlgIssue.cs
//	Author:	    jheary
//	Date:		01/07/09
//	Desc:		Dialog to create a new issue.
//	Rev:		
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Customers {
    //
    public partial class dlgIssue :Form {
        //Members
        private Issue mIssue = null;
        private ContactDS mContacts = null;
        private IssueTypeDS mIssueCategorys = null,mIssueTypes = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
        
        private const string CMD_CANCEL = "&Cancel";
        private const string CMD_OK = "O&K";

        //Interface
        public dlgIssue(Issue issue) {
            //Constructor
            try {
                InitializeComponent();
                this.btnOk.Text = CMD_OK;
                this.btnCancel.Text = CMD_CANCEL;
                this.mIssue = issue;
                this.mIssueCategorys = new IssueTypeDS();
                this.mIssueTypes = new IssueTypeDS();
                this.mContacts = new ContactDS();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgIssue instance.",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mToolTip.ShowAlways = true;
                this.mToolTip.SetToolTip(this.cboIssueType,"Select an issue type.");
                this.cboIssueCategory.DataSource = this.mIssueCategorys;
                this.cboIssueCategory.DisplayMember = "IssueTypeTable.Category";
                this.cboIssueCategory.ValueMember = "IssueTypeTable.Category";
                this.mIssueCategorys.Merge(CustomerProxy.GetIssueCategorys());
                this.cboIssueType.DataSource = this.mIssueTypes;
                this.cboIssueType.DisplayMember = "IssueTypeTable.Type";
                this.cboIssueType.ValueMember = "IssueTypeTable.ID";
                this.cboIssueCategory.SelectedIndex = 0;
                OnIssueCategorySelected(this.cboIssueCategory,EventArgs.Empty);
                this.ctlContact.DataSource = this.mContacts;
                this.mContacts.Merge(CustomerProxy.GetContacts());
                this.txtTitle.Text = this.mIssue.Subject;
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
        }
        private void OnIssueCategorySelected(object sender,EventArgs e) {
            //Event handler for change in issue category
            this.mIssueTypes.Clear();
            this.mIssueTypes.Merge(CustomerProxy.GetIssueTypes(this.cboIssueCategory.SelectedValue.ToString()));
            this.cboIssueType.SelectedIndex = -1;
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnIssueTypeSelected(object sender,EventArgs e) {
            //Event handler for change in selected issue type
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnLocationChanged(object sender,EventArgs e) {
            //Event handler for change in company location 
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnContactChanged(object source,ContactEventArgs e) {
            //Event handler for new contact created
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnBeforeContactCreated(object source,ContactEventArgs e) {
            //Event handler for before new contact created
            //Pre-populate with AS400 Contact if applicable
            this.Cursor = Cursors.WaitCursor;
            try {
                //Add AS400 contact if scope=store and not blank and not in list
                string scope = this.ctlCompLoc.LocationScope;
                if((scope == CompanyLocation.SCOPE_STORES || scope == CompanyLocation.SCOPE_SUBSTORES)) {
                    //See if store contact is in list
                    bool inlist = false;
                    Contact contact = this.ctlCompLoc.StoreContact;
                    for(int i = 0; i < this.ctlContact.Items.Count; i++) {
                        if(this.ctlContact.Items[i].ToString() == contact.FullName) {
                            inlist = true;
                            break;
                        }
                    }
                    if(!inlist) {
                        //Update contact
                        e.Contact.FirstName = contact.FirstName;
                        e.Contact.LastName = contact.LastName;
                        e.Contact.Phone = contact.Phone;
                    }
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnAfterContactCreated(object source,ContactEventArgs e) {
            //Event handler for new contact created
            this.Cursor = Cursors.WaitCursor;
            try {
                //Persist the new contact
                int id = CustomerProxy.CreateContact(e.Contact);
                this.mContacts.Clear();
                this.mContacts.Merge(CustomerProxy.GetContacts());
                this.ctlContact.SelectedValue = id;
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnControlError(object source,ControlErrorEventArgs e) {
            //Event handler for error from custom control
            reportError(e.Exception);
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Event handler for control value changes
            try {
                bool locationValid = ((this.ctlCompLoc.LocationScope == CompanyLocation.SCOPE_AGENTS || this.ctlCompLoc.LocationScope == CompanyLocation.SCOPE_DISTRICTS || this.ctlCompLoc.LocationScope == CompanyLocation.SCOPE_REGIONS) && this.ctlCompLoc.LocationNumber.Length > 0) || 
                                     ((this.ctlCompLoc.LocationScope == CompanyLocation.SCOPE_STORES || this.ctlCompLoc.LocationScope == CompanyLocation.SCOPE_SUBSTORES) && this.ctlCompLoc.StoreDetail.Length > 0);
                this.btnOk.Enabled = (locationValid && 
                                        this.cboIssueType.Text.Length > 0 &&
                                        this.ctlContact.SelectedValue != null &&
                                        this.txtTitle.Text.Length > 0);
            }
            catch(Exception ex) { reportError(ex); }
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
                        //Create new issue
                        this.Cursor = Cursors.WaitCursor;
                        this.DialogResult = DialogResult.OK;
                        this.mIssue.CompanyID = (int)this.ctlCompLoc.CompanySelectedValue;
                        switch(this.ctlCompLoc.LocationScope) {
                            case CompanyLocation.SCOPE_AGENTS:
                                this.mIssue.AgentNumber = (this.ctlCompLoc.LocationNumber!="All" ? this.ctlCompLoc.LocationNumber : "");
                                break;
                            case CompanyLocation.SCOPE_DISTRICTS:
                                this.mIssue.DistrictNumber = (this.ctlCompLoc.LocationNumber != "All" ? this.ctlCompLoc.LocationNumber : "");
                                break;
                            case CompanyLocation.SCOPE_REGIONS:
                                this.mIssue.RegionNumber = (this.ctlCompLoc.LocationNumber != "All" ? this.ctlCompLoc.LocationNumber : "");
                                break;
                            case CompanyLocation.SCOPE_STORES:
                                this.mIssue.StoreNumber = Convert.ToInt32(this.ctlCompLoc.LocationNumber);
                                break;
                            case CompanyLocation.SCOPE_SUBSTORES:
                                this.mIssue.StoreNumber = Convert.ToInt32(this.ctlCompLoc.LocationNumber);
                                break;
                        }
                        this.mIssue.ContactID = (int)this.ctlContact.SelectedValue;
                        this.mIssue.TypeID = (int)this.cboIssueType.SelectedValue;
                        this.mIssue.Subject = this.txtTitle.Text;
                        this.Close();
                        break;
                    default: break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        #region Local Services: reportError()
        private void reportError(Exception ex) { MessageBox.Show("Unexpected error:\n\n" + ex.ToString());  }
        #endregion
    }
}