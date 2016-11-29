//	File:	Detail.aspx.cs
//	Author:	J. Heary
//	Date:	02/04/09
//	Desc:	Tracking detail page.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Detail : System.Web.UI.Page
{
    //Interface
    protected void Page_Load(object sender, EventArgs e)
    {
        //Event handler for page load event
        //Validate
        string itemNumber = Request.QueryString["ID"];
        TrackDS ds = (TrackDS)Session["TrackData"];

        //Set UI; hide links as required
        bool fromDefault = (bool)Session["FromDefault"];
        this.btnGoSummary.Visible = this.btnImgGoSummary.Visible = (ds.TrackingSummaryTable.Rows.Count > 1);
        this.btnTrackNew.Visible = this.btnImgTrackNew.Visible = (fromDefault || ds.TrackingSummaryTable.Rows.Count > 1);

        //Display detail
        this.grdDetail.DataMember = "TrackingDetailTable";
        this.grdDetail.DataSource = ds.TrackingDetailTable.Select("ItemNumber='" + itemNumber + "'", "Date DESC, Time DESC");
        this.grdDetail.DataBind();

        //Display summary
        TrackDS.TrackingSummaryTableRow[] summaryInfo = (TrackDS.TrackingSummaryTableRow[])ds.TrackingSummaryTable.Select("ItemNumber='" + itemNumber + "'");
        if (summaryInfo[0].IsShipperNameNull() || summaryInfo[0].ShipperName == "N/A")
        {
            this.detailPanel.Visible = false;
        }
        this.lblDetail_ID.Text += itemNumber;
        this.lblDetail_Status.Text = summaryInfo[0].Status.ToString();
        this.lblDetailSum.Text = "in " + summaryInfo[0].LocationName.ToString() + " on " + summaryInfo[0].Date.ToString("MM-dd-yyyy") + " at " + summaryInfo[0].Time.ToString("hh:mm tt");
        this.lblFromInfo.Text = summaryInfo[0].ShipperName.ToString();
        this.lblFromInfo.Text += "\n" + summaryInfo[0].ShipperCity.ToString();
        this.lblFromInfo.Text += ", " + summaryInfo[0].ShipperState.ToString();
        this.lblFromInfo.Text += " " + summaryInfo[0].ShipperZip.ToString();
        this.lblFromInfo.Text += "\n" + summaryInfo[0].ShipperCountry.ToString();
        this.lblToInfo.Text = summaryInfo[0].ConsigneeName.ToString();
        this.lblToInfo.Text += "\n" + summaryInfo[0].ConsigneeCity.ToString();
        this.lblToInfo.Text += ", " + summaryInfo[0].ConsigneeState.ToString();
        this.lblToInfo.Text += " " + summaryInfo[0].ConsigneeZip.ToString();
        this.lblToInfo.Text += "\n" + summaryInfo[0].ConsigneeCountry.ToString();
        this.lbShipInfo.Text = "Ship date: " + (summaryInfo[0].IsShipDateNull() ? "" : summaryInfo[0].ShipDate.ToShortDateString());
        this.lbShipInfo.Text += "\nPieces: " + summaryInfo[0].Pieces.ToString();
        this.lbShipInfo.Text += "\nTotal weight: " + summaryInfo[0].Weight.ToString() + " lbs";
        //this.lbShipInfo.Text += "\nReference: " + summaryInfo[0].LabelSequenceNumber.ToString();
        //this.lbShipInfo.Text += "\nDescription:: " + "?????????????";
       // this.lblDetailFootnote.Text += "  " + System.DateTime.Now;

    }
    protected void GoSummary(object sender, EventArgs e) { Response.Redirect("~/Summary.aspx"); }
    protected void GoSummaryImg(object sender, ImageClickEventArgs e) { Response.Redirect("~/Summary.aspx"); }
    protected void TrackNew(object sender, EventArgs e) { Response.Redirect("~/Default.aspx"); }
    protected void TrackNewImg(object sender, ImageClickEventArgs e) { Response.Redirect("~/Default.aspx"); }
}
