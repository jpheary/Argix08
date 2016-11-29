//	File:	Summary.aspx.cs
//	Author:	J. Heary
//	Date:	02/04/09
//	Desc:	Tracking summary page.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Summary :System.Web.UI.Page {
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event

        //Validate
        TrackDS ds = (TrackDS)Session["TrackData"];
        //if(ds == null)

        //Set UI; hide links as required
        this.btnImgTrackNew.Visible = this.btnTrackNew.Visible = true;

        //Display summary
        this.grdTrack.DataMember = "TrackingSummaryTable";
        this.grdTrack.DataSource = ds;
        this.grdTrack.DataBind();
        this.lblSumFootnote.Text += "  " + System.DateTime.Now;
    }
    protected void OnTrack(object sender,EventArgs e) { Response.Redirect("~/Default.aspx"); }
}
