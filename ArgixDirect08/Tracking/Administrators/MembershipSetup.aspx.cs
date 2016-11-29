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
using System.Diagnostics;

public partial class MembershipSetup:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.txtAppName.Text = Membership.ApplicationName;
            this.txtProviderName.Text = Membership.Provider.Name;
            this.txtProviderDesc.Text = Membership.Provider.Description;
            this.chkUniqueEmail.Checked = Membership.Provider.RequiresUniqueEmail;
            this.chkQAndA.Checked = Membership.RequiresQuestionAndAnswer;
            this.chkPWRetrieval.Checked = Membership.EnablePasswordRetrieval;
            this.chkPWReset.Checked = Membership.EnablePasswordReset;
            this.txtMaxInvalidPWAttempts.Text = Membership.MaxInvalidPasswordAttempts.ToString();
            this.txtPasswordAttemptWindow.Text = Membership.PasswordAttemptWindow.ToString();
            this.txtPasswordFormat.Text = Membership.Provider.PasswordFormat.ToString();
            this.txtPWStrengthRegEx.Text = Membership.PasswordStrengthRegularExpression;
            this.txtHashAlgorithmType.Text = Membership.HashAlgorithmType;
            this.txtMinPWLength.Text = Membership.MinRequiredPasswordLength.ToString();
            this.txtMinNonAlphaChars.Text = Membership.MinRequiredNonAlphanumericCharacters.ToString();
            this.txtUserIsOnlineTimeWindow.Text = Membership.UserIsOnlineTimeWindow.ToString();
        }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for cancel button clicked
        switch(e.CommandName) {
            case "Close":
                Response.Redirect("~/Members/Default.aspx",true);
                break;
        }
    }
}
