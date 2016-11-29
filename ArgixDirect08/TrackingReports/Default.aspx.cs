using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

public partial class Default :System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = "Argix Direct Reports";
            Master.PageTitleBackImage = "App_Themes/Reports/Images/pagetitle.gif";
            Master.NavTitleBackImage = "App_Themes/Reports/Images/windowtitle.gif";
        }
        Master.Validated = false;
    }
}