//	File:	ContactComboBox.cs
//	Author:	J. Heary
//	Date:	01/21/09
//	Desc:	Contact ComboBox control.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Argix.Customers {
    //
    [ComplexBindingProperties("DataSource")]
    [DefaultBindingProperty("Text")]
    public partial class ContactComboBox :UserControl {
        //Members
        private Contact mContact = null;
        private bool mReadOnly = false;
        private ToolTip mToolTip = null;
        
        public event ContactEventHandler ContactChanged = null;
        public event ContactEventHandler BeforeContactCreated = null;
        public event ContactEventHandler AfterContactCreated = null;
        public event ControlErrorEventHandler Error = null;

        //Interface
        public ContactComboBox() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch(Exception ex) { throw new ControlException("Unexpected error while creating new ContactComboBox control instance.",ex); }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Data")]
        [Description("Gets or sets the datasource for this Argix.Customers.ContactComboBox.")]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.DataSource")]
        public ContactDS DataSource {
            get { return (ContactDS)this.cboContact.DataSource;  }
            set { this.cboContact.DataSource = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Data")]
        [Description("Gets or sets the property to display for this Argix.Customers.ContactComboBox.")]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.DisplayMember")]
        public string DisplayMember {
            get { return this.cboContact.DisplayMember; }
            set { this.cboContact.DisplayMember = value; }
        }

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Category("Data")]
        [Description("Gets or sets the property to use as the actual value for the items in the Argix.Customers.ContactComboBox.")]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.ValueMember")]
        public string ValueMember {
            get { return this.cboContact.ValueMember; }
            set { this.cboContact.ValueMember = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets an object representing the collection of the items contained in the Argix.Customers.ContactComboBox.")]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.Items")]
        public ComboBox.ObjectCollection Items { get { return this.cboContact.Items; } }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the index specifying the currently selected contact.")]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.SelectedIndex")]
        [Localizable(false)]
        public int SelectedIndex { get { return this.cboContact.SelectedIndex; } set { this.cboContact.SelectedIndex = value; } }
        private bool ShouldSerializeSelectedIndex() { return false; }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the value of the member property specified by the System.Windows.Forms.ListControl.ValueMember property of the contact control.")]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.SelectedValue")]
        [Localizable(false)]
        public object SelectedValue { get { return this.cboContact.SelectedValue; } 
            set { if(value != null) this.cboContact.SelectedValue = value; } 
        }
        private bool ShouldSerializeSelectedValue() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the text associated with the contact control.")]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.Text")]
        public override string Text { get { return this.cboContact.Text; } set { this.cboContact.Text = value; } }
        private bool ShouldSerializeText() { return false; }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the read only state of the control.")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.ReadOnly")]
        public bool ReadOnly {
            get { return this.mReadOnly; } 
            set {
                this.mReadOnly = value;
                this.cboContact.DropDownStyle = this.mReadOnly ? ComboBoxStyle.Simple : ComboBoxStyle.DropDownList;
                this.cboContact.Enabled = this.btnContact.Enabled = !this.mReadOnly;
                this.btnContact.Visible = !this.mReadOnly;
            } 
        }
        public void ResetReadOnly() { this.mReadOnly = false; }
        private bool ShouldSerializeReadOnly() { return (this.mReadOnly != true); }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets font bold for the control label.")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.ContactComboBox.FontBold")]
        public bool FontBold {
            get { return this._lblContact.Font.Bold; }
            set { this._lblContact.Font = new Font(this._lblContact.Font,(value ? FontStyle.Bold : FontStyle.Regular));  }
        }
        public void ResetFontBold() { this._lblContact.Font = new Font(this._lblContact.Font, FontStyle.Regular); }
        private bool ShouldSerializeFontBold() { return (this._lblContact.Font.Bold != false); }
        
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mToolTip = new ToolTip();
                this.mToolTip.ShowAlways = true;
                this.mToolTip.SetToolTip(this.lblPhone,"phone (right click for more...)");
                this.mToolTip.SetToolTip(this.btnContact,"Create a new contact...");
                
                this.lblPhone.Text = this.lnkEmail.Text = "";
                this.ctxControl.Enabled = false;
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while loading ContactComboBox control.",ex)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnContactTextChanged(object sender,EventArgs e) {
            //Event handler for change in contact text
            try {
                if(this.cboContact.Text.Length == 0) {
                    //Clear prior contact info; disable menu
                    this.mContact = null;
                    this.lblPhone.Text = this.lnkEmail.Text = "";
                    this.lnkEmail.Links.Clear();
                    this.ctxControl.Enabled = false;
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error when contact text changed.",ex)); }
        }
        private void OnContactChanged(object sender,EventArgs e) {
            //Event handler for change in selected contact
            try {
                //Clear prior contact info; disable menu
                this.mContact = null;
                this.lblPhone.Text = this.lnkEmail.Text = "";
                this.lnkEmail.Links.Clear();
                this.ctxControl.Enabled = false;

                if(this.cboContact.SelectedValue != null) {
                    //Find the selected contact and display contact info
                    ContactDS.ContactTableRow[] cs = (ContactDS.ContactTableRow[])this.DataSource.ContactTable.Select("ID=" + this.cboContact.SelectedValue);
                    if(cs.Length > 0) {
                        this.mContact = new Contact();
                        if(!cs[0].IsIDNull()) this.mContact.ID = cs[0].ID;
                        if(!cs[0].IsFirstNameNull()) this.mContact.FirstName = cs[0].FirstName;
                        if(!cs[0].IsLastNameNull()) this.mContact.LastName = cs[0].LastName;
                        if(!cs[0].IsFullNameNull()) this.mContact.FullName = cs[0].FullName;
                        if(!cs[0].IsPhoneNull()) this.mContact.Phone = cs[0].Phone;
                        if(!cs[0].IsMobileNull()) this.mContact.Mobile = cs[0].Mobile;
                        if(!cs[0].IsFaxNull()) this.mContact.Fax = cs[0].Fax;
                        if(!cs[0].IsEmailNull()) this.mContact.Email = cs[0].Email;
                        this.ctxControl.Enabled = true;
                        if(this.mContact.Phone.Length > 0) 
                            this.ctxPhone.PerformClick();
                        else if(this.mContact.Mobile.Length > 0)
                            this.ctxMobile.PerformClick();
                        else if(this.mContact.Fax.Length > 0)
                            this.ctxFax.PerformClick();

                        if(this.mContact.Email.Trim().Length > 0) {
                            this.lnkEmail.Text = this.mContact.Email.Trim();
                            this.lnkEmail.LinkArea = new System.Windows.Forms.LinkArea(0,this.mContact.Email.Trim().Length);
                            this.lnkEmail.Links[0].LinkData = this.mContact.Email.Trim();
                        }
                        if(this.ContactChanged != null) this.ContactChanged(this,new ContactEventArgs(this.mContact));
                    }
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error when contact changed.",ex)); }
        }
        private void OnNewContact(object sender,EventArgs e) {
            //Event handler for new contact button clicked
            try {
                //Create a new (blank) contact and allow client to update it before displaying it
                Contact contact = new Contact();
                if(this.BeforeContactCreated != null) this.BeforeContactCreated(this,new ContactEventArgs(contact));
                dlgContact dlg = new dlgContact(contact);
                dlg.Font = this.Font;
                if(dlg.ShowDialog() == DialogResult.OK) {
                    //Notify client of new contact (client responsible for persistence)
                    if(this.AfterContactCreated != null) this.AfterContactCreated(this,new ContactEventArgs(contact));
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while creating new contact.",ex)); }
        }
        private void OnMenuClick(object sender,EventArgs e) {
            //Event handler for menu clicked
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                switch(menu.Text) {
                    case "Phone":
                        this.lblPhone.Text = "p " + this.mContact.Phone.Trim();
                        this.mToolTip.SetToolTip(this.lblPhone,"phone (right click for more...)");
                        this.ctxPhone.Checked = true;
                        this.ctxMobile.Checked = false;
                        this.ctxFax.Checked = false;
                        break;
                    case "Mobile":
                        this.lblPhone.Text = "m " + this.mContact.Mobile.Trim();
                        this.mToolTip.SetToolTip(this.lblPhone,"mobile (right click for more...)");
                        this.ctxPhone.Checked = false;
                        this.ctxMobile.Checked = true;
                        this.ctxFax.Checked = false;
                        break;
                    case "Fax":
                        this.lblPhone.Text = "f " + this.mContact.Fax.Trim();
                        this.mToolTip.SetToolTip(this.lblPhone,"fax (right click for more...)");
                        this.ctxPhone.Checked = false;
                        this.ctxMobile.Checked = false;
                        this.ctxFax.Checked = true;
                        break;
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while creating new contact.",ex)); }
        }
        private void OnLinkClicked(object sender,LinkLabelLinkClickedEventArgs e) {
            //Event handler for link clicked  
            try {
                //
                if(this.lnkEmail.Text.Length > 0)
                    System.Diagnostics.Process.Start("mailto:" + e.Link.LinkData.ToString());
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while launching email.",ex)); }
        }
        #region Local Services: setUserServices(), reportError()
        private void setUserServices() {
            //Set user services
            this.ctxPhone.Enabled = true;
            this.ctxMobile.Enabled = true;
            this.ctxFax.Enabled = true;
        }
        private void reportError(Exception ex) {
            //Error notification
            if(this.Error != null) this.Error(this,new ControlErrorEventArgs(ex));
        }
        #endregion
    }
}
