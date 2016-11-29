using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CartonSummary :System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Set UI; hide links as required
            Master.GoSummaryVisible = false;
            Master.GoTrackVisible = true;

            Argix.TrackingItems items = (Argix.TrackingItems)Session["TrackData"];
            if(items != null && items.Count > 0) {
                //Title
                this.grdTrack.DataSource = items;
                this.grdTrack.DataBind();
            }
            else
                Master.ShowMsgBox("Could not find summary information. Please return to tracking page and try again.");
        }
    }
    protected virtual void OnItemDataBound(object sender,GridViewRowEventArgs e) {
        //
        if(e.Row.RowType == DataControlRowType.DataRow) {
            string labelNumber = this.grdTrack.DataKeys[e.Row.RowIndex].Value.ToString();
            if(labelNumber.Length == 0) ((HyperLink)e.Row.Cells[0].Controls[0]).NavigateUrl = "";
        }
    }
}
