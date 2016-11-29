using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Imaging;

public partial class _Image:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {

    }
    protected override void Render(HtmlTextWriter output) {
        //
        //Pull the image from a remote client
        WebClient wc = new WebClient();
        wc.UseDefaultCredentials = true;
        Stream stream = wc.OpenRead("http://rgxshpnt:9000/TBill/Document%20Library/0000019C.tif");
        Bitmap bitmap = new Bitmap(stream);
        
        //Render 
        Page.Response.Clear();
        Page.Response.ContentType = "image/jpeg";
        bitmap.Save(Page.Response.OutputStream,ImageFormat.Jpeg);
        
        //Clean up
        bitmap.Dispose();
    }
}
