using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Error:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        Exception exc = Server.GetLastError();
        this.lblError.Text = "Unknown Error";
        if(exc != null) {
            this.lblError.Text = exc.Message;
            if(exc.InnerException != null) {
                this.lblError.Text += "\r\n\r\n" + exc.InnerException.Message;
                if(exc.InnerException.InnerException != null) {
                    this.lblError.Text += "\r\n\r\n" + exc.InnerException.InnerException.Message;
                }
            }
        }
        Server.ClearError();
    }
}
