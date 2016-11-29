using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class TLDetail:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,System.EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.Title = "TL# " + Request.QueryString["tl"];
           this.grdDetail.DataBind();
        }
    }
}
