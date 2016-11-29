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
using System.Text;

public partial class CartonSummary:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        if(!Page.IsPostBack) {
            TrackingDS summary = (TrackingDS)Session["TrackingSummary"];
            if(summary != null && summary.CartonSummaryTable.Rows.Count > 0) {
                //Title
                this.lblSubTitle.Text = "     " + summary.CartonSummaryTable.Rows.Count.ToString() + " cartons";
                try {
                    TrackingDS cartons = (TrackingDS)Session["TrackingDetail"];
                    if(Session["TrackBy"].ToString() == TrackingServices.SEARCHBY_PRO) {
                        this.lblTitle.Text += " for PRO# ";
                        this.lblTitle.Text += cartons.CartonDetailTable[0].IsNull("ShipmentNumber") ? "?" : cartons.CartonDetailTable[0]["ShipmentNumber"].ToString();
                    }
                    if(Session["TrackBy"].ToString() == TrackingServices.SEARCHBY_PO) {
                        this.lblTitle.Text += " for PO# ";
                        this.lblTitle.Text += cartons.CartonDetailTable[0].IsPONull() ? "?" : cartons.CartonDetailTable[0].PO;
                    }
                }
                catch { }

                //Summary
                this.SummaryGridView.DataSourceID = "";
                this.SummaryGridView.DataMember = "CartonSummaryTable";
                this.SummaryGridView.DataSource = summary;
                this.SummaryGridView.DataBind();

                //List cartons 'not found' or 'not searched'
                StringBuilder lst = new StringBuilder();
                TrackingDS cartonsSearch = (TrackingDS)Session["TrackingSearch"];
                if(cartonsSearch != null) {
                    DataRow[] notValid = cartonsSearch.CartonSearchTable.Select("Valid = false");
                    for(int i=0;i<notValid.Length;i++)
                        lst.Append(notValid[i]["SearchID"]).Append(Convert.ToChar(13));

                    DataRow[] notFound = cartonsSearch.CartonSearchTable.Select("Found = false");
                    for(int i=0;i<notFound.Length;i++)
                        lst.Append(notFound[i]["SearchID"]).Append(Convert.ToChar(13));
                }
                this.cartonNotFoundTextBox.Text = lst.ToString();
                this.cartonNotFoundPanel.Visible = (lst.Length > 0);
            }
            else
                Master.ShowMsgBox("Could not find summary information. Please return to tracking page and try again.");
        }
    }
}
