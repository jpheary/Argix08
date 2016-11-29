using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class IPhoneMaster:System.Web.UI.MasterPage {
    //Members
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
    }
    protected void OnViewIssues(object sender,EventArgs e) {
        //Event handler for 
        Response.Redirect("~/ViewIssues.aspx");
    }
}
