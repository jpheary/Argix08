using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argix.Customers;

public partial class Attachment:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            //Get attachment id and filename from query
            int id = Convert.ToInt32(Request.QueryString["id"]);
            string filename = Request.QueryString["name"];

            //Return attachment as byte array from database (BLOB)
            IssueMgtServiceClient crgService  = new IssueMgtServiceClient();
            byte[] bytes = crgService.GetAttachment(id);

            //Render to browser
            HttpResponse response = this.Context.Response;
            response.BufferOutput = true;
            response.AddHeader("Content-Disposition","attachment;filename=" + filename);
            response.ContentType = "application/octet-stream";
            try {
                string ext = filename.Substring(filename.IndexOf(".") + 1,3).ToLower();
                switch(ext) {
                    case "doc": response.ContentType = "application/msword"; break;
                    case "jpeg": response.ContentType = "image/jpeg"; break;
                    case "msg": response.ContentType = "application/vnd.ms-outlook"; break;
                    case "ppt": response.ContentType = "application/ppt"; break;
                    case "pdf": response.ContentType = "application/pdf"; break;
                    case "tif": response.ContentType = "image/tiff"; break;
                    case "txt": response.ContentType = "text/plain"; break;
                    case "xls": response.ContentType = "application/vnd.ms-excel"; break;
                    case "xlsx": response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; break;
                    default: response.ContentType = "application/octet-stream"; break;
                }
            }
            catch { }
            response.BinaryWrite(bytes);
        }
        catch(Exception ex) { this.Context.Response.Write(ex.ToString()); }
    }
}
