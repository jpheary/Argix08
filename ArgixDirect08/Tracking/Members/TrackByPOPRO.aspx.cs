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
using System.Diagnostics;
using System.Xml;

public partial class TrackByPOPRO:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler fro page load event
        if(!Page.IsPostBack) {
            this.cboClient.DataBind();
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        this.btnTrack.Enabled = true;
    }
    protected void OnTrack(object sender,EventArgs e) {
        //Track by PO or PRO number
        if(Page.IsValid) {
            //Flag search by method
            Session["TrackBy"] = this.cboSearchType.Items[0].Selected ? TrackingServices.SEARCHBY_PRO : TrackingServices.SEARCHBY_PO;

            //Track
            TrackingServices trackSvc = new TrackingServices();
            TrackingDS cartons = new TrackingDS();
            if(this.cboSearchType.Items[0].Selected)
                cartons.Merge(trackSvc.GetCartonsForPRO(this.cboClient.SelectedValue,this.txtNumber.Text));
            else
                cartons.Merge(trackSvc.GetCartonsForPO(this.cboClient.SelectedValue,this.txtNumber.Text));
            if(cartons.CartonDetailTable.Rows.Count > 0) {
                //Build a summary of the cartons found
                TrackingDS summary = buildSummary(cartons,(this.cboSearchType.Items[0].Selected ? TrackingServices.SEARCHBY_PRO : TrackingServices.SEARCHBY_PO));
                Session["TrackingSearch"] = null;
                Session["TrackingSummary"] = summary;
                Session["TrackingDetail"] = cartons;
                Response.Redirect("CartonSummary.aspx");
            }
            else
                Master.ShowMsgBox("No records found. Please try again.");
        }
    }
    #region Data Services: buildSummary(), addSummaryRow()
    private TrackingDS buildSummary(TrackingDS trackingData,string searchBy) {
        //Build a summary for the specified carton detail
        TrackingDS summary = new TrackingDS();
        foreach(TrackingDS.CartonDetailTableRow carton in trackingData.CartonDetailTable.Rows) {
            //Duplicates: The result set contains duplicates
            string displayNumber="";
            switch(searchBy) {
                case TrackingServices.SEARCHBY_CARTONNUMBER:
                case TrackingServices.SEARCHBY_PLATENUMBER:
                case TrackingServices.SEARCHBY_PRO:
                case TrackingServices.SEARCHBY_PO:
                    displayNumber = carton.CTN.Trim();
                    break;
                case TrackingServices.SEARCHBY_LABELNUMBER:
                    displayNumber = carton.LBL.ToString();
                    break;
            }
            
            string id = carton.CTN.Trim();
            if(summary.CartonSummaryTable.FindByID(id) != null)
                id = carton.CTN.Trim() + DateTime.Now.Second;
            addSummaryRow(id,displayNumber,carton,summary);
        }
        return summary;
    }
    private void addSummaryRow(string id,string displayNumber,TrackingDS.CartonDetailTableRow carton,TrackingDS summary) {
        //Add a row to the carton summary
        TrackingDS.CartonSummaryTableRow record = summary.CartonSummaryTable.NewCartonSummaryTableRow();
        record.ID = id;
        record.DisplayNumber = displayNumber;
        record.CTNNumber = carton.CTN.Trim();
        record.LBLNumber = carton.LBL.ToString().Trim();
        record.CBOL = carton.IsCBOLNull() ? "" : carton.CBOL.Trim();
        if(carton.SCNTP == 3 && !carton.IsSCDNull() && carton.SCD.Trim().Length > 0) {
            record.DateTime = carton.SCD.Trim() + " " + carton.SCTM.Trim();
            if(carton.T.Trim().Length == 18 && carton.T.Trim().Substring(0,2).ToLower() == "1z")
                record.Status = "Rerouted: Tracking # " + carton.T.Trim();
            else {
                switch(carton.OM) {
                    case "M": record.Status = carton.ISMN == 1 ? "Delivered (Scan N/A - Manual Entry)" : "Delivered"; break;
                    case "S": record.Status = "Short At Delivery"; break;
                    case "O": record.Status = "Over At Delivery"; break;
                    case "A": record.Status = "MisRoute At Delivery"; break;
                }
            }
            record.Location = carton.SCT.Trim() + "/" + carton.SST.Trim();
        }
        else {
            if(carton.SCNTP == 3 && !carton.IsACTSDDNull() && carton.ACTSDD.Trim().Length > 0) {
                record.DateTime = carton.ACTSDD.Trim();
                record.Status = "Out For Delivery";
                record.Location = carton.SCT.Trim() + "/" + carton.SST.Trim();
            } 
            else {
                if(!carton.IsAARDNull() && carton.AARD.Trim().Length > 0) {
                    record.DateTime = carton.AARD.Trim() + " " + carton.AART.Trim();
                    if(carton.SCNTP == 1) {
                        switch(carton.OM) {
                            case "M": record.Status = "Scanned At Delivery Terminal"; break;
                            case "S": record.Status = "Short At Delivery Terminal"; break;
                            case "O": record.Status = "Over At Delivery Terminal"; break;
                            case "A": record.Status = "MisRoute At Delivery Terminal"; break;
                        }
                    }
                    else
                        record.Status = "Arrived At Delivery Terminal";
                    if(!carton.IsSAGCTNull()) 
                        record.Location = carton.SAGCT.Trim() + "/" + (carton.IsSAGSTNull() ? "" : carton.SAGST.Trim());
                    else 
                        record.Location = (carton.IsAGCTNull() ? "" : carton.AGCT.Trim()) + "/" + (carton.IsAGSTNull() ? "" : carton.AGST.Trim());
                } 
                else {
                    if(!carton.IsADPDNull() && carton.ADPD.Trim().Length > 0) {
                        record.DateTime = carton.ADPD.Trim() + " " + carton.ADPT.Trim();
                        record.Status = "Departed Sort Facility";
                        record.Location = carton.SRTLOC.Trim();
                    } 
                    else {	
                        if(!carton.IsASFDNull() && carton.ASFD.Trim().Length > 0) {
                            record.DateTime = carton.ASFD.Trim() + " " + carton.ASFT.Trim();
                            record.Status = "Arrived At Sort Facility";
                            record.Location = carton.SRTLOC.Trim();
                        }
                    }
                }
            }
        }
        summary.CartonSummaryTable.AddCartonSummaryTableRow(record);
    }
    #endregion
}
