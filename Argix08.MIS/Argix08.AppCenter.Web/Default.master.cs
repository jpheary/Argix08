using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //Members
 
    //Interface
    public string AppTitle { get { return this.lblTitle.Text; } set { this.lblTitle.Text = value; } }
    public string PageTitleBackImage { get { return this.tcPageTitle.Style[HtmlTextWriterStyle.BackgroundImage]; } set { this.tcPageTitle.Style[HtmlTextWriterStyle.BackgroundImage] = value; } }
    public string NavTitleBackImage { get { return this.tcNavTitle.Style[HtmlTextWriterStyle.BackgroundImage]; } set { this.tcNavTitle.Style[HtmlTextWriterStyle.BackgroundImage] = value; } }
    public bool NavigatorVisible {
        get { return !this.splMain.Panes[0].Collapsed; }
        set {
            if(value) {
                this.imgExplore.ImageUrl = "~/App_Themes/Argix/Images/explore_on.gif";
                this.tcExplore.Style["border-right-style"] = "none";
                this.splMain.Panes[0].Collapsed = false;
            }
            else {
                this.imgExplore.ImageUrl = "~/App_Themes/Argix/Images/explore_off.gif";
                this.tcExplore.Style["border-right-style"] = "solid";
                this.splMain.Panes[0].Collapsed = true;
            }
        } 
    }
    public string Status { set { ShowMsgBox(value,false); } }
    public void ReportError(Exception ex) { reportError(ex); }

    protected override void OnInit(EventArgs e) {
        //Event handler for page Init event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Get configuration values for this control
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {

            //Initialize control values
            this.imgExplore.Attributes.Add("onclick","javascript:document.body.style.cursor='wait';");
        }
        else {
        }
    }
    protected void OnExploreTabClicked(object sender,ImageClickEventArgs e) {
        NavigatorVisible = !NavigatorVisible;
        ScriptManager.RegisterStartupScript(this.imgExplore,typeof(ImageButton),"ClearCursor","document.body.style.cursor='default';",true);
    }
    protected void OnTreeNodeDataBound(object sender,TreeNodeEventArgs e) {
        //Event handler for treeview node data bounded
        string url = e.Node.NavigateUrl;
        if(url.Trim().Length > 0) {
            if(e.Node.Text == this.lblTitle.Text) {
                e.Node.Selected = true;
                e.Node.Parent.Expanded = true;
            }
        }
    }
    #region Local Services: reportError(), ShowMsgBox(), showRuntime()
    public void reportError(Exception ex) {
        //Report an exception to the user
        try {
            string src = (ex.Source != null) ? ex.Source + "-\n" : "";
            string msg = src + ex.Message;
            if(ex.InnerException != null) {
                if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            }
            ShowMsgBox(msg,true);
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
    #endregion
}
