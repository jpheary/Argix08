using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DefaultMaster : System.Web.UI.MasterPage {
    //Members

    //Interface
    public bool GoSummaryVisible { get { return this.btnGoSummary.Visible; } set { this.btnGoSummary.Visible = this.btnImgGoSummary.Visible = value; } }
    public bool GoTrackVisible { get { return this.btnTrackNew.Visible; } set { this.btnTrackNew.Visible = this.btnImgTrackNew.Visible = value; } }
    public void ReportError(Exception ex) {
        //Report an exception to the user
        try {
            string src = (ex.Source != null) ? ex.Source + "-\n" : "";
            string msg = src + ex.Message;
            if(ex.InnerException != null) {
                if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            }
            ShowMsgBox(msg);
        }
        catch(Exception) { }
    }
    public void ShowMsgBox(string message) {
        //
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language=javascript>");
        script.Append(" alert('" + message + "');");
        script.Append("</script>");
        Page.ClientScript.RegisterStartupScript(typeof(Page),"Message",script.ToString());
    }
    
    protected void Page_Init(object sender,EventArgs e) {
        //Event handler for load event
        if(!Page.IsPostBack) {
            this.btnGoSummary.Visible = this.btnImgGoSummary.Visible = false;
            this.btnTrackNew.Visible = this.btnImgTrackNew.Visible = false;
        }
    }
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for load event
        if(!Page.IsPostBack) {
            this.lblDetailFootnote.Text += "   " + System.DateTime.Now + ".";
        }
    }
    protected void GoSummary(object sender,EventArgs e) { Response.Redirect("~/CartonSummary.aspx"); }
    protected void GoSummaryImg(object sender,ImageClickEventArgs e) { Response.Redirect("~/CartonSummary.aspx"); }
    protected void TrackNew(object sender,EventArgs e) { Response.Redirect("~/Default.aspx"); }
    protected void TrackNewImg(object sender,ImageClickEventArgs e) { Response.Redirect("~/Default.aspx"); }
}
