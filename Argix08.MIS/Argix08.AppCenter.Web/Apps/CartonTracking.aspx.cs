using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Apps_CartonTracking:System.Web.UI.Page {
    //Members
    private const string APP_NAME = "Carton Tracking";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.AppTitle = APP_NAME;
        }
    }
}
