using System;
using System.Data;
using System.Web.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ManageGuests:System.Web.UI.Page {
    //Members
    private const string EMAIL_ERROR = "An error occured while sending welcome message to the approved user. Email was not sent.";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Refresh list of registered users
            //this.cboGuest.DataBind();
            refreshGuestList();
            if(this.cboGuest.Items.Count > 0) this.cboGuest.SelectedIndex = 0;
            OnGuestChanged(null,EventArgs.Empty);
        }
    }
    protected void OnGuestChanged(object sender,EventArgs e) {
        //Event handler for registered (guest) user changed
        MembershipUser user = Membership.GetUser(this.cboGuest.SelectedValue);
        if(user != null) {
            this.txtUserID.Text = user.UserName;
            this.txtEmail.Text = user.Email;
        }
        
        ProfileCommon profile = Profile.GetProfile(this.cboGuest.SelectedValue);
        if(profile != null) {
            this.txtUserName.Text = profile.UserFullName;
            this.txtCompany.Text = profile.Company;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnCompanyTypeChanged(object sender,EventArgs e) {
        //Event handler for cusotmer type option button changed
        this.cboCustomer.DataValueField = (this.cboType.SelectedValue == "Client" ? "ClientID" : "VendorID");
        this.cboCustomer.DataBind();
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        //Set services
        this.btnReject.Enabled = this.cboGuest.Items.Count > 0;
        this.btnApprove.Enabled = this.cboGuest.Items.Count > 0;
        this.btnClose.Enabled = true;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for cancel button clicked
        switch(e.CommandName) {
            case "Close":
                Response.Redirect("~/Members/Default.aspx",true);
                break;
            case "Reject":
                bool deleted=false, sent=false;
                try {
                    deleted = Membership.DeleteUser(this.cboGuest.SelectedValue,true);
                    if(deleted) {
                        if(this.txtComments.Text.Trim().Length > 0) {
                            sent = new EmailServices().SendRejectRegistrationMessage(this.txtUserID.Text,this.txtEmail.Text,this.txtComments.Text);
                        }
                        Master.ShowMsgBox(this.txtUserID.Text + " has been deleted" + (sent ? " and a rejection email has been sent." : "."));
                    }
                    else {
                        Master.ShowMsgBox(this.txtUserID.Text + " has not been deleted.");
                    }
                }
                catch(Exception ex) {
                    string msg = this.txtUserID.Text + (deleted ? " has been deleted but an unexpected error occurred." : " has not been deleted and an unexpected error occurred.");
                    Master.ReportError(new ApplicationException(msg,ex));
                }
                finally {
                    //this.cboGuest.DataBind();
                    refreshGuestList();
                    if(this.cboGuest.Items.Count > 0) this.cboGuest.SelectedIndex = 0;
                    OnGuestChanged(null,EventArgs.Empty);
                }
                break;
            case "Approve":
                try {
                    MembershipUser user = Membership.GetUser(this.cboGuest.SelectedValue);

                    ProfileCommon profile = Profile.GetProfile(user.UserName);
                    profile.UserFullName = this.txtUserName.Text;
                    profile.Company = this.cboCustomer.SelectedItem.Text;
                    profile.WebServiceUser = false;
                    profile.Type = this.cboType.SelectedValue.ToLower();
                    profile.ClientVendorID = this.cboCustomer.SelectedValue;
                    profile.Save();

                    Roles.RemoveUserFromRole(user.UserName,MembershipServices.GUESTROLE);
                    Roles.AddUserToRole(user.UserName,MembershipServices.TRACKINGROLE);

                    user.Email = this.txtEmail.Text;
                    user.IsApproved = true;
                    Membership.UpdateUser(user);

                    new EmailServices().SendWelcomeMessage(profile.UserFullName,this.cboGuest.SelectedValue,user.Email);
                    Master.ShowMsgBox(this.txtUserID.Text + " has been approved and a welcome email has been sent.");
                }
                catch(Exception ex) {
                    string msg = "An unexpected error occurred while approving " + this.txtUserID.Text + ".";
                    Master.ReportError(new ApplicationException(msg,ex));
                }
                finally {
                    //this.cboGuest.DataBind();
                    refreshGuestList();
                    if(this.cboGuest.Items.Count > 0) this.cboGuest.SelectedIndex = 0;
                    OnGuestChanged(null,EventArgs.Empty);
                }
                break;
        }
    }
    private void refreshGuestList() {
        //Get a list of registered users
        this.cboGuest.Items.Clear();
        string[] users = Roles.GetUsersInRole(MembershipServices.GUESTROLE);
        if(users.Length > 0) {
            for(int i=0;i<users.Length;i++)
                this.cboGuest.Items.Add(users[i]);
        }
    }
}
