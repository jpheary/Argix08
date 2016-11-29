using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DefaultMaster : System.Web.UI.MasterPage {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for load event
        MembershipUser member = Membership.GetUser();
        this._lblUserName.Visible = (member != null);
    }
    protected void OnLogout(object sender, LoginCancelEventArgs e) {
        Page.Session.Clear();
    }
    public void ReportError(Exception ex) {
        //Report an exception to the user
        try {
            string src = (ex.Source != null) ? ex.Source + "-\n" : "";
            string msg = src + ex.Message;
            if(ex.InnerException != null) {
                if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            }
            ShowMsgBox(msg);
        }
        catch(Exception) { }
    }
    public void ShowMsgBox(string message) { ShowMsgBox(message,false); }
    public void ShowMsgBox(string message,bool isStartup) {
        //
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language=javascript>");
        script.Append(" alert('" + message + "');");
        script.Append("</script>");
        if(isStartup)
            Page.ClientScript.RegisterStartupScript(typeof(Page),"Error",script.ToString());
        else
            System.Web.HttpContext.Current.Response.Write(script.ToString());
    }
}
