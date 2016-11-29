using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class App:System.Web.UI.MasterPage {
    //Members

    //Interface
    public string AppTitle { get { return Master.AppTitle; } set { Master.AppTitle = value; } }

    protected void Page_Load(object sender,EventArgs e) {

    }
}
