using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Login : System.Web.UI.Page {
    //Members
    private const string USER_LOGIN_NOACCOUNT = "Your account does not exist. Make sure you have entered a correct user id. If you never registered before then register first.";
    private const string USER_LOGIN_FAILED = "Login attempt failed. Your user id or password did not match.";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        this.txtUserID.Focus();
    }
    protected void OnLogin(object sender,EventArgs e) {
        //Event handler for login request
        string username = this.txtUserID.Text.Trim();
        string pwd = this.txtPassword.Text;
        try {
            //Validate login
            if(FormsAuthentication.Authenticate(username, pwd)) {
                //FormsAuthentication.SetAuthCookie(username,false);
                FormsAuthentication.RedirectFromLoginPage(username, false);
            }
            else {
                rfvUserID.IsValid = false; 
                rfvUserID.ErrorMessage = USER_LOGIN_FAILED; 
                return;
            }
        }
        catch(Exception ex) { rfvUserID.IsValid = false; rfvUserID.ErrorMessage = ex.Message; }
    }
}
