using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.AppTitle = "Argix Direct Reports";
            Master.PageTitleBackImage = "App_Themes/Argix/Images/pagetitle.gif";
            Master.NavTitleBackImage = "App_Themes/Argix/Images/windowtitle.gif";
        }
    }
}
