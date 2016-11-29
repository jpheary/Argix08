using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        this.odsItemsT.SelectParameters["startSortDate"].DefaultValue = "2011-09-01";
        this.odsItemsC.SelectParameters["startSortDate"].DefaultValue = "2011-09-01";
        this.odsOrdersT.SelectParameters["startRoutingDate"].DefaultValue = "2011-09-01";
        this.odsOrdersC.SelectParameters["startRoutingDate"].DefaultValue = "2011-09-01";
    }
}
