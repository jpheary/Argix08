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

public partial class Photo:System.Web.UI.Page {
    //Members
    private string mIDType="";
    private int mIDNumber = 0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!this.IsPostBack) {
            //Get query params
            this.mIDType = Request.QueryString["type"];
            this.mIDNumber = int.Parse(Request.QueryString["id"]);
        }
    }
    protected override void Render(HtmlTextWriter output) {
        //
        Stream stream=null;
        Bitmap image=null;
        try {
            //Return an image in the web reponse
            Argix.KronosProxy kp = new Argix.KronosProxy();
            Argix.Employee employee = kp.GetEmployee(this.mIDType,this.mIDNumber);
            byte[] bytes = employee.Photo;
            stream = new MemoryStream(bytes);
            image = new Bitmap(stream);

            //Render as jpeg to browser
            HttpResponse response = this.Context.Response;
            response.ContentType = "image/jpeg";
            response.BufferOutput = true;
            response.Clear();
            image.Save(response.OutputStream,ImageFormat.Jpeg);
        }
        catch { }
        finally { if(stream != null) stream.Dispose(); if(image != null) image.Dispose(); }
    }
}
