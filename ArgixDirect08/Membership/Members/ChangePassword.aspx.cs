using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ChangePassword:System.Web.UI.Page {
    //Members
    private const string PASSWORD_MIN_LENGHT = "Password should be minimum 6 characters long.";
    private const string PASSWORD_CHANGE_ERROR = "An error occured and password could not be changed. Please try again.";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.lblExpired.Visible = true;     //Membership.IsPasswordExpired;
            this.txtUserID.Text = Membership.GetUser().UserName;
            this.txtUserID.Enabled = false;
        }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for button command events
        switch(e.CommandName) {
            case "Cancel":
                Response.Redirect("Default.aspx",true);
                break;
            case "Submit":
                try {
                    //Validate new password
                    if(this.txtNewPassword.Text.Trim().Length >= Membership.MinRequiredPasswordLength) {
                        //Change user password to the new value
                        MembershipUser user = Membership.GetUser();
                        if(user.ChangePassword(this.txtOldPassword.Text,this.txtNewPassword.Text)) {
                            //Reset user password reset flag; update cached profile
                            ProfileCommon profile = new ProfileCommon().GetProfile(this.txtUserID.Text);
                            profile.PasswordReset = false;
                            profile.Save();
                            FormsAuthentication.SetAuthCookie(this.txtUserID.Text,false);

                            System.Text.StringBuilder script = new System.Text.StringBuilder();
                            script.Append("<script language=javascript>");
                            script.Append("\talert('Your password has been changed.');");
                            script.Append("\twindow.navigate('Default.aspx');");
                            script.Append("</script>");
                            Page.ClientScript.RegisterStartupScript(typeof(Page),"PasswordChanged",script.ToString());
                        }
                        else {
                            rfvUserID.IsValid = false;
                            rfvUserID.ErrorMessage = PASSWORD_CHANGE_ERROR;
                        }
                    }
                    else {
                        rfvUserID.IsValid = false;
                        rfvUserID.ErrorMessage = PASSWORD_MIN_LENGHT;
                    }
                }
                catch(Exception ex) {
                    rfvUserID.IsValid = false;
                    rfvUserID.ErrorMessage = "Unexpected error: " + ex.Message;
                }
                break;
        }
    }
}
