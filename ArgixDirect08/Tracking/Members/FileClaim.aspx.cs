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

public partial class FileClaim : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for form load event
        if(!Page.IsPostBack) {
            //Prepopulate from carton detail
            string cartonNumber = Request.QueryString["ID"] == null ? "" : Request.QueryString["ID"].ToString();
            if(cartonNumber.Length > 0) {
                TrackingDS summary = (TrackingDS)Session["TrackingSummary"];
                if(summary != null && summary.CartonSummaryTable.Rows.Count > 0) {
                    //Find the summary info for cartonNumber
                    TrackingDS.CartonSummaryTableRow summaryRow = summary.CartonSummaryTable.FindByID(cartonNumber);
                    if(summaryRow != null) {
                        //Get all detail rows for this carton
                        string filter = "[LBL]=" + summaryRow.LBLNumber;
                        TrackingDS cartons = (TrackingDS)Session["TrackingDetail"];
                        DataRow[] detailRows = cartons.CartonDetailTable.Select(filter);
                        if(detailRows != null && detailRows.Length > 0) {
                            //Show tracking detail for the carton
                            TrackingDS.CartonDetailTableRow carton = (TrackingDS.CartonDetailTableRow)detailRows[0];
                            this.lblName.Text = "ATTN: Claims Department";
                            this.lblCarrier.Text = "Argix Direct, Inc.";
                            this.lblDate.Text = DateTime.Today.ToShortDateString();
                            this.lblAddress.Text = "100 Middlesex Center Blvd, Jamesburg, NJ 08831";
                            this.lblClaimant.Text = carton.CLNM.Trim(); //Client name
                            if(carton.CTN.Trim().Length > 0)
                                this.lblDescription.Text = "Carton# " + carton.CTN.Trim();    //Carton#
                            else
                                this.lblDescription.Text = "Label# " + carton.LBL.ToString().Trim();    //LabelSeq#
                            this.lblConsignor.Text = carton.CLNM.Trim();     //Client name, city, state
                            this.lblShipFrom.Text = carton.VNM.Trim() + " " + carton.VCT.Trim() + ", " + carton.VST.Trim();     //Vendor name, city, state
                            this.lblFinalDest.Text = carton.SNM.Trim() + " " + carton.SCT.Trim() + ", " + carton.SST.Trim();    //Store name, city, state
                            this.lblRoutedVia.Text = "Argix Direct, Inc.";
                            this.lblBOLBy.Text = carton.VNM.Trim(); //Vendor name
                            this.lblBOLDate.Text = carton.PUD.Trim();  //Pickup date
                            this.lblPRONum.Text = carton.VK.Trim();   //PRO#
                            this.lblTrailerNum.Text = "";   //Vendor trailer#
                            this.lblConsignee.Text = carton.SNM.Trim() + " " + carton.SCT.Trim() + ", " + carton.SST.Trim();    //Store name, city, state
                        } 
                    } 
                } 
            }
        }
    }
}
