using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default:System.Web.UI.MasterPage {
    //Members
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
    }
    public System.Drawing.Color ProfilesButtonFontColor { get { return this.lblProfiles.ForeColor; } set { this.lblProfiles.ForeColor = value; } }
    public System.Drawing.Color PhotosButtonFontColor { get { return this.lblPhotos.ForeColor; } set { this.lblPhotos.ForeColor = value; } }
    public System.Drawing.Color SearchButtonFontColor { get { return this.lblSearch.ForeColor; } set { this.lblSearch.ForeColor = value; } }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //Event handler for change in view
        switch(e.CommandName) {
            case "Employees":   Response.Redirect("~/ViewEmployees.aspx"); break;
            case "Photos":      Response.Redirect("~/ViewPhotos.aspx"); break;
            case "Search":      Response.Redirect("~/ViewSearch.aspx"); break;
        }
    }
}
