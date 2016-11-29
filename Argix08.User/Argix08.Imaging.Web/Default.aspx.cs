using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Schema;
using System.Diagnostics;
using System.Text;
using System.Web.Hosting;

public partial class _Default : System.Web.UI.Page {
    //Members
    
    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        Argix.Freight.ImagingService svc = new Argix.Freight.ImagingService();
        try {
            this.txtSearchInfo.Text = svc.GetPortalSearchInfo();
        }
        catch(ApplicationException ex) { this.txtSearchInfo.Text = ex.ToString(); }
        try {
            this.txtMetaData.Text = svc.GetSearchMetadata();
        }
        catch(ApplicationException ex) { this.txtMetaData.Text = ex.ToString(); }
    }
}
