using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class Propak : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            Session["CompanyID"] = "136";
            string item = Request.QueryString["item"] == null ? "" : Request.QueryString["item"].ToString();
            if(item.Length > 0) {
                this.txtNumbers.Text = item;
                OnTrack(null,EventArgs.Empty);
            }
        }
    }
    protected void OnTrackImg(object sender, ImageClickEventArgs e) { OnTrack(null, null); }
    protected void OnTrack(object sender,EventArgs e) {
        //Event handler to track a carton
        //Validate
        if(sender != null && !Page.IsValid) return;

        //Reset session state
        Session["TrackData"] = null;

        //Prepare the request
        string cn = this.txtNumbers.Text.Trim();
        cn = cn.Replace("\n","");
        cn = cn.Replace("-","");
        cn = cn.Replace(Convert.ToChar(','),Convert.ToChar(13));
        string[] numbers = cn.Split(Convert.ToChar(13));

        //Get tracking details for all cartons and retain in Session state
        Argix.TrackingItems items = new Argix.TrackingProxy().TrackCartons(numbers,Session["CompanyID"].ToString());
        Session["TrackData"] = items;

        //Redirect to appropriate UI
        if(items.Count == 0) {
            this.rfvTrack.IsValid = false;
            this.rfvTrack.ErrorMessage = "Carton(s) not found. Please verify the tracking number(s) and retry.";
        }
        else if(items.Count == 1)
            Response.Redirect("~/CartonDetail.aspx?item=" + items[0].LabelNumber.Trim(),false);
        else
            Response.Redirect("~/CartonSummary.aspx",false);
    }
}
