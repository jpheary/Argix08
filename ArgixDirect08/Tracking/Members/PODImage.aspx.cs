using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PODImage:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            //Get query params
            string docClass = Request.QueryString["doc"];
            string propertyName = Request.QueryString["prop"];
            string searchText = Request.QueryString["search"];

            //Return an image from SharePoint in the web reponse
            Imaging.ImageService svc = new Imaging.ImageService();
            svc.Url = Application["ImageService"].ToString();
            svc.UseDefaultCredentials = true;
            byte[] bytes = svc.GetSharePointImageStream(docClass,propertyName,searchText);
            Stream stream = new MemoryStream(bytes);
            Bitmap image = new Bitmap(stream);

            //Render as jpeg to browser
            HttpResponse response = this.Context.Response;
            response.ContentType = "image/jpeg";
            response.BufferOutput = true;
            response.Clear();
            image.Save(response.OutputStream,ImageFormat.Jpeg);
        }
        catch(Exception ex) { this.Context.Response.AppendToLog(ex.ToString()); }
    }
}
