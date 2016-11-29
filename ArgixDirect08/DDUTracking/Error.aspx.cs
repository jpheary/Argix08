//	File:	Error.aspx.cs
//	Author:	J. Heary
//	Date:	02/04/09
//	Desc:	Error page.
//	Rev:	
//	---------------------------------------------------------------------------
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

public partial class _Error :System.Web.UI.Page {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            Exception ex = (Exception)Session["Error"];
            if(ex != null) 
                this.lblMsg.Text = ex.ToString();
            else
                this.lblMsg.Text = "Could not find the error.";
        }
        finally { Session["Error"] = null; }
    }
}
