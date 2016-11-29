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

public partial class MembershipUserPage :System.Web.UI.Page {
    //Members
    private string mUserName="";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query request and setup for new or existing user
            this.mUserName = Request.QueryString["username"] == null ? "" : Request.QueryString["username"].ToString();
            ViewState.Add("username",this.mUserName);
            this.lnkMembership.PostBackUrl += HttpUtility.UrlEncode(this.mUserName);
            if(this.mUserName.Length == 0) {
                //New member
                this.txtUserName.Enabled = true;
                this.chkLockedOut.Checked = false;
                this.chkLockedOut.Enabled = false;
                OnTypeChanged(this.cboType,EventArgs.Empty);
                this.optRole.SelectedValue = MembershipServices.GUESTROLE;
                OnRoleChanged(this.optRole,EventArgs.Empty);
            }
            else {
                //Existing member
                //  Membership
                MembershipUser member = Membership.GetUser(this.mUserName,false);
                this.txtUserName.Text = member.UserName;
                this.txtUserName.Enabled = false;
                this.txtEmail.Text = member.Email;
                try { if(!member.IsLockedOut) this.txtPassword.Text = member.GetPassword(); } catch(Exception ex) { Master.ReportError(ex); }
                this.txtComments.Text = member.Comment;
                this.chkApproved.Checked = member.IsApproved;
                this.chkApproved.Enabled = true;
                this.chkLockedOut.Checked = member.IsLockedOut;
                this.chkLockedOut.Enabled = false;

                //  Profile
                ProfileCommon profileCommon = new ProfileCommon();
                ProfileCommon profile = profileCommon.GetProfile(this.mUserName);
                this.txtFullName.Text = profile.UserFullName;
                this.txtCompany.Text = profile.Company;
                if(profile.Type.Length > 0) this.cboType.SelectedValue = profile.Type;
                OnTypeChanged(this.cboType,EventArgs.Empty);
                if(this.cboCustomer.Items.Count > 0 && profile.ClientVendorID.Length > 0) this.cboCustomer.SelectedValue = profile.ClientVendorID;
                this.cboStoreSearchType.SelectedValue = profile.StoreSearchType;
                this.chkPWReset.Checked = profile.PasswordReset;

                //  Roles
                if(Roles.IsUserInRole(this.mUserName,MembershipServices.GUESTROLE))
                    this.optRole.SelectedValue = MembershipServices.GUESTROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.ADMINROLE))
                    this.optRole.SelectedValue = MembershipServices.ADMINROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.TRACKINGROLE))
                    this.optRole.SelectedValue = MembershipServices.TRACKINGROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.TRACKINGWSROLE))
                    this.optRole.SelectedValue = MembershipServices.TRACKINGWSROLE;
                for(int i=0;i<this.chkRoles.Items.Count;i++) {
                    this.chkRoles.Items[i].Selected = Roles.IsUserInRole(this.mUserName,this.chkRoles.Items[i].Value);
                }
                OnRoleChanged(this.optRole,EventArgs.Empty);
            }
            OnValidateForm(null,EventArgs.Empty);
        }
        else {
            this.mUserName = ViewState["username"].ToString();
        }
    }
    protected void OnUserNameChanged(object sender,EventArgs e) {
        //Event handler for change in username
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnEmailChanged(object sender,EventArgs e) {
        //Event handler for change in email
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnPasswordChanged(object sender,EventArgs e) {
        //Event handler for change in password
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnCommentsChanged(object sender,EventArgs e) {
        //Event handler for change in comments
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnApprovedChanged(object sender,EventArgs e) {
        //Event handler for change in approved status
        //Cannot be a Guest once approved
        if(this.chkApproved.Checked) this.optRole.Items[0].Selected = false;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnFullNameChanged(object sender,EventArgs e) {
        //Event handler for change in full name
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for change in type
        this.cboCustomer.DataValueField = (this.cboType.SelectedValue == "Client" ? "ClientID" : "VendorID");
        this.cboCustomer.DataBind();
        OnCustomerChanged(null,EventArgs.Empty);
    }
    protected void OnCustomerChanged(object sender,EventArgs e) {
        //Event handler for change in client-vendor
        this.txtCompany.Text = this.cboCustomer.SelectedItem.Text;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnPWResetChanged(object sender,EventArgs e) {
        //Event handler for change in password reset status
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnRoleChanged(object sender,EventArgs e) {
        //Event handler for change in roles
        switch(this.optRole.SelectedValue) {
            case MembershipServices.GUESTROLE:
                //Guest member- cannot be in approved state, no supplemental roles
                this.chkApproved.Checked = false;
                this.chkApproved.Enabled = false;
                this.optRole.Items[1].Selected = this.optRole.Items[2].Selected = this.optRole.Items[3].Selected = false;
                for(int i=0;i<this.chkRoles.Items.Count;i++) {
                    this.chkRoles.Items[i].Selected = false;
                    this.chkRoles.Items[i].Enabled = false;
                }
                break;
            case MembershipServices.ADMINROLE:
                //Administrator role- automatically approved, no supplemental roles required
                //this.chkApproved.Checked = true;
                this.chkApproved.Enabled = true;
                this.optRole.Items[0].Selected = this.optRole.Items[2].Selected = this.optRole.Items[3].Selected = false;
                for(int i=0;i<this.chkRoles.Items.Count;i++) {
                    this.chkRoles.Items[i].Selected = false;
                    this.chkRoles.Items[i].Enabled = false;
                }
                break;
            case MembershipServices.TRACKINGWSROLE:
                //Web service role- automatically approved, no supplemental roles
                //this.chkApproved.Checked = true;
                this.chkApproved.Enabled = true;
                this.optRole.Items[0].Selected = this.optRole.Items[1].Selected = this.optRole.Items[2].Selected = false;
                for(int i=0;i<this.chkRoles.Items.Count;i++) {
                    this.chkRoles.Items[i].Selected = false;
                    this.chkRoles.Items[i].Enabled = false;
                }
                break;
            case MembershipServices.TRACKINGROLE:
                //Member role- automatically approved, supplemental roles as needed
                //this.chkApproved.Checked = true;
                this.chkApproved.Enabled = true;
                this.optRole.Items[0].Selected = this.optRole.Items[1].Selected = this.optRole.Items[3].Selected = false;
                for(int i=0;i<this.chkRoles.Items.Count;i++) this.chkRoles.Items[i].Enabled = true;
                break;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        //Set services
        this.btnSubmit.Enabled = this.txtUserName.Text.Length > 0 &&
                             this.txtEmail.Text.Length > 0 &&
                             this.txtPassword.Text.Length > 0 &&
                             this.txtFullName.Text.Length > 0 &&
                             this.txtCompany.Text.Length > 0 &&
                             this.cboType.SelectedValue != null &&
                             this.cboCustomer.SelectedValue != null &&
                             (this.optRole.Items[0].Selected || this.optRole.Items[1].Selected || this.optRole.Items[2].Selected || this.optRole.Items[3].Selected);
        this.btnUnlock.Enabled = this.chkLockedOut.Checked;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for cancel button clicked
        MembershipUser member=null;
        switch(e.CommandName) {
            case "Close":
                Response.Redirect("~/Administrators/Memberships.aspx?username=" + HttpUtility.UrlEncode(this.mUserName),true);
                break;
            case "OK":
                if(!Page.IsValid) return;
                bool isNewMember = this.mUserName.Length == 0;
                if(isNewMember) {
                    //Create a new user
                    MembershipCreateStatus status;
                    member = Membership.CreateUser(this.txtUserName.Text.Trim(),this.txtPassword.Text.Trim(),this.txtEmail.Text.Trim(),null,null,this.chkApproved.Checked,out status);
                    member.Comment = this.txtComments.Text;
                    Membership.UpdateUser(member);
                    switch(status) {
                        case MembershipCreateStatus.Success:
                            //Update profile (add user to guest role 'cause anonymous user cannot have a profile)
                            ProfileCommon profileCommon = new ProfileCommon();
                            ProfileCommon profile = profileCommon.GetProfile(this.txtUserName.Text);
                            profile.Company = this.cboCustomer.SelectedItem.Text;
                            profile.UserFullName = this.txtFullName.Text.Trim();
                            profile.Type = this.cboType.SelectedValue;
                            profile.ClientVendorID = this.cboCustomer.SelectedValue;
                            profile.StoreSearchType = this.cboStoreSearchType.SelectedValue;
                            profile.PasswordReset = this.chkPWReset.Checked;
                            profile.WebServiceUser = this.optRole.Items[3].Selected;
                            profile.Save();

                            //Update roles
                            if(this.optRole.Items[0].Selected) Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.GUESTROLE);
                            if(this.optRole.Items[1].Selected) {
                                Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.ADMINROLE);
                                for(int i=0;i<this.chkRoles.Items.Count;i++) {
                                    if(this.chkRoles.Items[i].Selected) Roles.AddUserToRole(this.mUserName,this.chkRoles.Items[i].Value);
                                }
                            }
                            if(this.optRole.Items[2].Selected) Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.TRACKINGROLE);
                            if(this.optRole.Items[3].Selected) Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.TRACKINGWSROLE);
                            Master.ShowMsgBox(this.txtUserName.Text + " was created successfully.");
                            this.btnSubmit.Enabled = false;
                            break;
                        case MembershipCreateStatus.DuplicateEmail: Master.ShowMsgBox("Failed to create new member- DuplicateEmail."); break;
                        case MembershipCreateStatus.DuplicateProviderUserKey: Master.ShowMsgBox("Failed to create new member- DuplicateProviderUserKey"); break;
                        case MembershipCreateStatus.DuplicateUserName: Master.ShowMsgBox("Failed to create new member- DuplicateUserName"); break;
                        case MembershipCreateStatus.InvalidAnswer: Master.ShowMsgBox("Failed to create new member- InvalidAnswer"); break;
                        case MembershipCreateStatus.InvalidEmail: Master.ShowMsgBox("Failed to create new member- InvalidEmail"); break;
                        case MembershipCreateStatus.InvalidPassword: Master.ShowMsgBox("Failed to create new member- InvalidPassword"); break;
                        case MembershipCreateStatus.InvalidProviderUserKey: Master.ShowMsgBox("Failed to create new member- InvalidProviderUserKey"); break;
                        case MembershipCreateStatus.InvalidQuestion: Master.ShowMsgBox("Failed to create new member- InvalidQuestion"); break;
                        case MembershipCreateStatus.InvalidUserName: Master.ShowMsgBox("Failed to create new member- InvalidUserName"); break;
                        case MembershipCreateStatus.ProviderError: Master.ShowMsgBox("Failed to create new member- ProviderError"); break;
                        case MembershipCreateStatus.UserRejected: Master.ShowMsgBox("Failed to create new member- UserRejected"); break;
                    }
                }
                else {
                    //Update existing user if account is not locked
                    member = Membership.GetUser(this.mUserName);
                    if(member.IsLockedOut) {
                        Master.ShowMsgBox(this.mUserName + " account must be unlocked before updating.");
                        return;
                    }
                    //Membership
                    if(member.GetPassword() != this.txtPassword.Text) member.ChangePassword(member.GetPassword(),this.txtPassword.Text);
                    member.Comment = this.txtComments.Text;
                    member.IsApproved = this.chkApproved.Checked;
                    member.Email = this.txtEmail.Text;
                    Membership.UpdateUser(member);

                    //Profile
                    ProfileCommon profileCommon = new ProfileCommon();
                    ProfileCommon profile = profileCommon.GetProfile(this.mUserName);
                    profile.ClientVendorID = this.cboCustomer.SelectedValue;
                    profile.StoreSearchType = this.cboStoreSearchType.SelectedValue;
                    profile.Company = this.cboCustomer.SelectedItem.Text;
                    profile.PasswordReset = this.chkPWReset.Checked;
                    profile.Type = this.cboType.SelectedValue;
                    profile.UserFullName = this.txtFullName.Text;
                    profile.WebServiceUser = this.optRole.Items[3].Selected;
                    profile.Save();

                    //Roles
                    for(int i=0;i<this.optRole.Items.Count;i++) {
                        if(this.optRole.Items[i].Selected && !Roles.IsUserInRole(this.mUserName,this.optRole.Items[i].Value)) Roles.AddUserToRole(this.mUserName,this.optRole.Items[i].Value);
                        if(!this.optRole.Items[i].Selected && Roles.IsUserInRole(this.mUserName,this.optRole.Items[i].Value)) Roles.RemoveUserFromRole(this.mUserName,this.optRole.Items[i].Value);
                    }
                    for(int i=0;i<this.chkRoles.Items.Count;i++) {
                        if(this.chkRoles.Items[i].Selected && !Roles.IsUserInRole(this.mUserName,this.chkRoles.Items[i].Value)) Roles.AddUserToRole(this.mUserName,this.chkRoles.Items[i].Value);
                        if(!this.chkRoles.Items[i].Selected && Roles.IsUserInRole(this.mUserName,this.chkRoles.Items[i].Value)) Roles.RemoveUserFromRole(this.mUserName,this.chkRoles.Items[i].Value);
                    }
                    this.btnSubmit.Enabled = false;
                    Master.ShowMsgBox(this.txtUserName.Text + " was updated successfully.");
                }
                break;
            case "Unlock":
                //Unlock user if locked out
                member = Membership.GetUser(this.txtUserName.Text,false);
                if(member.IsLockedOut) {
                    if(member.UnlockUser()) {
                        Master.ShowMsgBox(this.txtUserName.Text + " account was unlocked successfully.");
                        try {
                            if(!member.IsLockedOut) this.txtPassword.Text = member.GetPassword();
                        }
                        catch(Exception ex) { Master.ReportError(ex); }
                        this.chkLockedOut.Checked = member.IsLockedOut;
                    }
                    else {
                        Master.ShowMsgBox(this.txtUserName.Text + " account failed to unlock.");
                    }
                }
                OnValidateForm(null,EventArgs.Empty);
                break;
        }
    }
}
