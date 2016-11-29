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
    public System.Drawing.Color FreightButtonFontColor { get { return this.lblFreight.ForeColor; } set { this.lblFreight.ForeColor = value; } }
    public System.Drawing.Color SortingButtonFontColor { get { return this.lblSorting.ForeColor; } set { this.lblSorting.ForeColor = value; } }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //Event handler for change in view
        switch(e.CommandName) {
            case "Freight": Response.Redirect("~/ViewFreight.aspx"); break;
            case "Sorting": Response.Redirect("~/ViewSorting.aspx"); break;
        }
    }
}
