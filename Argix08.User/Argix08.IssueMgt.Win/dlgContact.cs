//	File:	dlgContact.cs
//	Author:	J. Heary
//	Date:	01/08/09
//	Desc:	Dialog to create and edit Issue Management contacts.
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
    public partial class dlgContact :Form {
        //Members
        private Contact mContact = null;

        private const string CMD_CANCEL = "&Cancel";
        private const string CMD_OK = "O&K";		

        //Interface
        public dlgContact(Contact contact) {
            //Constructor
            try {
                InitializeComponent();
                this.btnOk.Text = CMD_OK;
                this.btnCancel.Text = CMD_CANCEL;
                this.mContact = contact;
                this.Text = (this.mContact.ID > 0) ? "Contact (" + this.mContact.ID.ToString() + ")" : "Contact (New)";
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgContact instance.",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Set initial service states
                this.Visible = true;
                
                //Set control services
                this.txtFName.MaxLength = 25;
                this.txtFName.Text = this.mContact.FirstName;
                this.txtLName.MaxLength = 25;
                this.txtLName.Text = this.mContact.LastName;
                this.txtPhone.Mask = "(999) 000-0000";
                this.txtPhone.Text = this.mContact.Phone;
                this.txtMobile.Mask = "(999) 000-0000";
                this.txtMobile.Text = this.mContact.Mobile;
                this.txtFax.Mask = "(999) 000-0000";
                this.txtFax.Text = this.mContact.Fax;
                this.txtEmail.Mask = "";
                this.txtEmail.Text = this.mContact.Email;
                this.txtFName.Focus();
            }
            catch(Exception ex) { reportError(ex); }
            finally { OnValidateForm(null,EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Event handler for control value changes
            this.btnOk.Enabled = (this.txtFName.Text.Length > 0);
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
                        this.mContact.FirstName = this.txtFName.Text;
                        this.mContact.LastName = this.txtLName.Text;
                        this.mContact.Phone = this.txtPhone.Text;
                        this.mContact.Mobile = this.txtMobile.Text;
                        this.mContact.Fax = this.txtFax.Text;
                        this.mContact.Email = this.txtEmail.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    default: break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        #region Local Services: reportError()
        private void reportError(Exception ex) { MessageBox.Show("Unexpected error:\n\n" + ex.ToString()); }
        #endregion
    }
}