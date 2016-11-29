using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Services.Protocols;

public class USPSClient : HttpSimpleClientProtocol {
    public USPSClient() { }
    public WebResponse TrackResponse(Uri uri) {
        WebRequest request = base.GetWebRequest(uri);
        return base.GetWebResponse(request);
    }
}
public partial class _USPS : System.Web.UI.Page  {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for load event
        try {
            //Display the web service url from a Tracking carton instance
            this.txtService.Text = "http://testing.shippingapis.com//ShippingAPITest.dll";
            this.txtUserID.Text = "019ARGIX2902";
            this.txtPassword.Text = "*";
            this.txtCartonNum.Text = "EJ958083578US";
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnTrack(object sender,EventArgs e) {
        //Event handler to track a carton
        try {
            //Clear prior results
            this.txtCartonDetail.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCartonDetail.Text = "";

            //Create Tracking web service client and add SOAP header credentials
            USPSClient client = new USPSClient();
            client.Url = "http://testing.shippingapis.com//ShippingAPITest.dll";
            Uri uri = new Uri("http://testing.shippingapis.com//ShippingAPITest.dll?API=TrackV2&XML=<TrackRequest USERID='" + this.txtUserID.Text + "'><TrackID ID='" + this.txtCartonNum.Text + "'></TrackID></TrackRequest>");
            WebResponse response = client.TrackResponse(uri);
            System.IO.Stream stream = response.GetResponseStream();
            System.Text.Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            System.IO.StreamReader readStream = new System.IO.StreamReader(stream,encode);
            this.txtCartonDetail.Text = readStream.ReadToEnd();
        }
        catch(Exception ex) { reportError(ex); }
    }
    #region Local Services: reportError()
    private void reportError(Exception ex) {
        //Report an error to the user
        this.txtCartonDetail.ForeColor = System.Drawing.Color.Red;
        this.txtCartonDetail.Text = ex.ToString();
    }
    #endregion
}
