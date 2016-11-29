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
using System.IO;

public partial class Upload:System.Web.UI.Page {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Page load event handler
        if(!Page.IsPostBack) {
            //Enumerate folders
            string[] files = Directory.GetDirectories(Server.MapPath("~"));
            for(int i=0;i<files.Length;i++) {
                FileInfo fi = new FileInfo(files[i]);
                if(fi.Name != "App_Data" && fi.Name != "MasterPages" && fi.Name != "App_Themes" && fi.Name != "bin")
                    this.cboFolder.Items.Add(files[i]);
            }
            if(this.cboFolder.Items.Count > 0) this.cboFolder.SelectedIndex = 0;
        }
    }
    protected void OnUpload(object sender, EventArgs e) {
        //Event handler for upload click event
        try {
            if(this.FileUpload1.HasFile) {
                string dir = this.cboFolder.SelectedItem.Text + "\\";
                FileInfo fi = new FileInfo(dir + this.FileUpload1.FileName);
                this.FileUpload1.SaveAs(fi.FullName);
                //this.lblStatus.Text =   "File name: " + this.FileUpload1.PostedFile.FileName + "<br>" +
                //                        "File size: " + this.FileUpload1.PostedFile.ContentLength + " kb<br>" +
                //                        "File type: " + this.FileUpload1.PostedFile.ContentType + 
                //                        "File location: " + fi.FullName;
                Response.Redirect("Downloads.aspx?id=" + fi.Directory.Name);
            } 
            else
                Page.ClientScript.RegisterStartupScript(typeof(Page),"Please",getClientAlertScript("Please select a file."));
        }
        catch(Exception ex) { Page.ClientScript.RegisterStartupScript(typeof(Page),"Failed",getClientAlertScript("Failed: " + ex.Message)); }
    }
    private string getClientAlertScript(string message) {
        //Client-side alert message
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language='javascript'>");
        script.Append(" alert('" + Server.HtmlEncode(message) + "')");
        script.Append("</script>");
        return script.ToString();
    }
}
