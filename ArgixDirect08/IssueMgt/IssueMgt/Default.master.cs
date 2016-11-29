using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //Members
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
    }
    public System.Drawing.Color IssuesButtonFontColor { get { return this.lblIssues.ForeColor; } set { this.lblIssues.ForeColor = value; } }
    public System.Drawing.Color SearchButtonFontColor { get { return this.lblSearch.ForeColor; } set { this.lblSearch.ForeColor = value; } }
    protected void OnViewIssues(object sender,EventArgs e) {
        //Event handler for 
        Response.Redirect("~/ViewIssues.aspx");
    }
    protected void OnViewSearch(object sender,EventArgs e) {
        //Event handler for 
        Response.Redirect("~/ViewSearch.aspx");
    }
}
