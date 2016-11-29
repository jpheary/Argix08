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

public partial class CartonDetail:System.Web.UI.Page {
    //Members
    private string mCartonNumber="",mSearchByStoreTL="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Display tracking information for the specified carton
            this.mCartonNumber = Request.QueryString["ID"] == null ? "" : Request.QueryString["ID"].ToString();
            ViewState["CartonNumber"] = this.mCartonNumber;
            this.mSearchByStoreTL = Request.QueryString["TL"] == null ? "" : Request.QueryString["TL"].ToString();
            ViewState["SearchByStoreTL"] = this.mSearchByStoreTL;
            
            if(this.mCartonNumber.Length > 0)
                displayTrackingDetail(this.mCartonNumber);
            else
                Response.Redirect("~/Members/Default.aspx");
            
            if(Session["TrackBy"].ToString() == TrackingServices.SEARCHBY_STORE) {
                this.lnkTracking.PostBackUrl = "~/Members/TrackByStore.aspx";
                this.lnkStoreSummary.Visible = true;
                this.lnkSummary.Text = "Store Detail...";
                this.lnkSummary.PostBackUrl = "~/Members/StoreDetail.aspx?TL=" + HttpUtility.UrlEncode(this.mSearchByStoreTL);
            }
            else if(Session["TrackBy"].ToString() == TrackingServices.SEARCHBY_PO || Session["TrackBy"].ToString() == TrackingServices.SEARCHBY_PRO) {
                this.lnkTracking.PostBackUrl = "~/Members/TrackByPOPRO.aspx";
                this.lnkStoreSummary.Visible = false;
                this.lnkSummary.Text = "Carton Summary...";
                this.lnkSummary.PostBackUrl = "~/Members/CartonSummary.aspx";
            }
            else {
                this.lnkTracking.PostBackUrl = "~/Members/TrackByCarton.aspx";
                this.lnkStoreSummary.Visible = false;
                this.lnkSummary.Text = "Carton Summary...";
                this.lnkSummary.PostBackUrl = "~/Members/CartonSummary.aspx";
            }
            this.lnkFileClaim.NavigateUrl = "~/Members/FileClaim.aspx?ID=" + this.mCartonNumber;
            this.lnkFileClaim.ToolTip = "Submit a file claim";
            MembershipServices membership = new MembershipServices();
            this.lnkFileClaim.Visible = membership.IsAdmin || membership.IsArgix  || membership.IsFileClaims;
            this.lnkPODReq.Visible = true;
        }
        else {
            this.mCartonNumber = ViewState["CartonNumber"].ToString();
            this.mSearchByStoreTL = ViewState["SearchByStoreTL"].ToString();
        }
    }
    protected void OnSelectedCartonChanged(object sender,EventArgs e) { displayTrackingDetail(this.cboCartons.SelectedValue); }
    protected void OnNavigate(object sender,CommandEventArgs e) {
        //Event handler for carton navigation
        switch(e.CommandName) {
            case "First": this.cboCartons.SelectedIndex = 0; break;
            case "Prev": this.cboCartons.SelectedIndex -= 1; break;
            case "Next": this.cboCartons.SelectedIndex += 1; break;
            case "Last": this.cboCartons.SelectedIndex = this.cboCartons.Items.Count - 1; break;
        }
        this.btnNavFirst.Enabled = this.btnNavPrev.Enabled = this.cboCartons.SelectedIndex > 0;
        this.btnNavNext.Enabled = this.btnNavLast.Enabled = this.cboCartons.SelectedIndex < this.cboCartons.Items.Count - 1;
        displayTrackingDetail(this.cboCartons.SelectedValue);

    }
    protected void OnPODRequest(object sender,EventArgs e) { 
        //Send request to Customer Service and confirmation to user
        TrackingDS.CartonDetailTableRow carton=null;
        string cbol="";
        bool imageFound=false;
        MembershipServices membership = new MembershipServices();
        TrackingDS summary = (TrackingDS)Session["TrackingSummary"];
        TrackingDS.CartonSummaryTableRow summaryRow = summary.CartonSummaryTable.FindByID(this.CartonNumberValue.Text);
        if(summaryRow == null) summaryRow = summary.CartonSummaryTable.FindByID(this.LabelSeqValue.Text);
        if(summaryRow != null) {
            //Get all detail rows for this carton
            string filter = "[LBL]=" + summaryRow.LBLNumber;
            TrackingDS cartons = (TrackingDS)Session["TrackingDetail"];
            DataRow[] detailRows = cartons.CartonDetailTable.Select(filter);
            if(detailRows != null && detailRows.Length > 0) {
                //Get carton data
                carton = (TrackingDS.CartonDetailTableRow)detailRows[0];
                if(membership.IsAdmin || membership.IsArgix || membership.IsPODMember) {
                    //Check for a POD image for this CBOL
                    cbol = carton.IsCBOLNull() ? "" : carton.CBOL.Trim();
                    if(cbol.Length > 0) {
                        string cl = carton.CL.Trim().PadLeft(3,'0');
                        string div = "%";
                        string st = carton.S.ToString().PadLeft(5,'0');
                        Imaging.ImageService isvc = new Imaging.ImageService();
                        isvc.Url = Application["ImageService"].ToString();
                        isvc.UseDefaultCredentials = true;
                        DataSet ds = isvc.SearchSharePointImageStore("TBill","TBBarCode",cbol + cl + div + st + "%");
                        imageFound = (ds != null && ds.Tables[0].Rows.Count > 0);
                    }
                }
            }
        }

        if(imageFound) {
            //Image available- open image into another browser instance
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<script language=javascript>");
            sb.Append("window.open('PODImage.aspx?doc=TBill&prop=TBBarCode&search=" + cbol + "%', '_blank', 'width=480,height=576,menubar=yes,location=no,toolbar=no,status=yes,resizable=yes');");
            sb.Append("</script>");
            Page.ClientScript.RegisterStartupScript(typeof(Page),"POD Image",sb.ToString());
        }
        else {
            //No image; request a POD from Argix
            EmailServices svcs = new EmailServices();
            MembershipUser user = Membership.GetUser();
            string substore = Session["SubStore"] != null ? Session["SubStore"].ToString() : "";
            svcs.SendPODRequest(user,carton,substore);
            svcs.SendPODRequestConfirm(user,carton,substore);

            //Disply confirmation to user
            Response.Redirect("PODConfirmation.aspx");
        }
    }
    #region Local Services: displayTrackingDetail(), showCarton(), isAdminUser(), isArgixUser()
    private void displayTrackingDetail(string cartonNumber) {
        //Display tracking info for the specified carton
        TrackingDS summary = (TrackingDS)Session["TrackingSummary"];
        if(summary != null) {
            //Find the summary info for cartonNumber
            TrackingDS.CartonSummaryTableRow summaryRow = summary.CartonSummaryTable.FindByID(cartonNumber);
            if(summaryRow != null) {
                //Get all detail rows for this carton
                string filter = "[LBL]=" + summaryRow.LBLNumber;
                TrackingDS cartons = (TrackingDS)Session["TrackingDetail"];
                DataRow[] detailRows = cartons.CartonDetailTable.Select(filter);
                if(detailRows != null && detailRows.Length > 0) {
                    //Show tracking detail for the carton
                    showCarton((TrackingDS.CartonDetailTableRow)detailRows[0]);

                    //Update navigation
                    this.cboCartons.Visible = this.CartonLabel.Visible = false;
                    this.btnNavFirst.Visible = this.btnNavPrev.Visible = this.btnNavNext.Visible = this.btnNavLast.Visible = false;
                    if(summary.CartonSummaryTable.Rows.Count > 1) {
                        this.cboCartons.DataSource = summary;
                        this.cboCartons.DataMember = "CartonSummaryTable";
                        this.cboCartons.DataTextField = "DisplayNumber";
                        this.cboCartons.DataValueField = "ID";
                        this.cboCartons.DataBind();
                        this.cboCartons.SelectedValue = cartonNumber;

                        this.cboCartons.Visible = this.CartonLabel.Visible = summary.CartonSummaryTable.Rows.Count > 1;
                        this.btnNavFirst.Visible = this.btnNavPrev.Visible = this.btnNavNext.Visible = this.btnNavLast.Visible = summary.CartonSummaryTable.Rows.Count > 1;
                    } 
                } 
            } 
        } 
    }
    private void showCarton(TrackingDS.CartonDetailTableRow carton) {
        //Display details for the specified carton
        this.CartonNumberValue.Text = carton.CTN.Trim();
        this.ClientNameValue.Text = carton.CLNM.Trim();
        string storeAddLine = carton.SA1.Trim();
        if(!carton.IsSA2Null() && carton.SA2.Trim().Length > 0) storeAddLine += ", " + carton.SA2.Trim() + ", ";
        this.StoreNum.Text = (Session["SubStore"] != null ? Session["SubStore"].ToString() : carton.S.ToString().PadLeft(5, '0'));
        this.StoreName.Text = ": " + carton.SNM.Trim() + ", " + storeAddLine + carton.SCT.Trim() + ", " + carton.SST.Trim() + " " + carton.SZ.ToString();
        this.VendorNameValue.Text = carton.IsVNMNull() ? "" : carton.VNM.Trim();
        this.PickupDateValue.Text = carton.IsPUDNull() ? "" : carton.PUD.Trim();
        //this.CarrierValue.Text = carton["CarrierName"].ToString();
        this.BOLValue.Text = carton.IsBLNull() ? "" : carton.BL.ToString();
        this.TLValue.Text = carton.IsTLNull() ? "" : carton.TL.Trim();
        this.LabelSeqValue.Text = carton.LBL.ToString();
        this.PONumberValue.Text = carton.IsPONull() ? "" : carton.PO.Trim();
        this.WeightValue.Text = carton.IsWTNull() ? "" : carton.WT.ToString();
        this.ShipmentNumber.Text = carton.IsShipmentNumberNull() ? "" : carton.ShipmentNumber.Trim();
        this.DeliveryValue.Text = carton.IsACTSDDNull() ? "" : carton.ACTSDD.Trim();

        //Sort facility
        this.TDSDate.Text = this.TDSStatus.Text = this.TDSLocation.Text = "";
        if(!carton.IsASFDNull() && carton.ASFD.Trim().Length > 0) {
            this.TDSDate.Text = carton.ASFD.Trim() + " " + carton.ASFT.Trim();
            this.TDSStatus.Text = "Arrived At Sort Facility";
            this.TDSLocation.Text = carton.SRTLOC.Trim();
        }
        this.DepartureDate.Text = this.DepartureStatus.Text = this.DepartureLocation.Text = "";
        if(!carton.IsADPDNull() && carton.ADPD.Trim().Length > 0) {
            this.DepartureDate.Text = carton.ADPD.Trim() + " " + carton.ADPT.Trim();
            this.DepartureStatus.Text = "Departed Sort Facility";
            this.DepartureLocation.Text = carton.IsSHPRNull() ? "" : carton.SHPR.Trim();
        }
        
        //Delivery terminal 
        //1. BOL confirmed (trailer arrived in AS400): SCNTP=0, AARD!=null; 
        //2. Agent scan: SCNTP=1, AARD!=null, OM=Over(O)||Short(S)||MisRoute(A)||Match(M)
        this.ArrivalDate.Text = this.ArrivalStatus.Text = this.ArrivalLocation.Text = "";
        if(!carton.IsAARDNull() && carton.AARD.Trim().Length > 0) {
            this.ArrivalDate.Text = carton.AARD.Trim() + " " + carton.AART.Trim();
            if(carton.SCNTP == 1) {
                switch(carton.OM) {
                    case "M": this.ArrivalStatus.Text = "Scanned At Delivery Terminal"; break;
                    case "S": this.ArrivalStatus.Text = "Short At Delivery Terminal"; break;
                    case "O": this.ArrivalStatus.Text = "Over At Delivery Terminal"; break;
                    case "A": this.ArrivalStatus.Text = "MisRoute At Delivery Terminal"; break;
                }
            }
            else
                this.ArrivalStatus.Text = "Arrived At Delivery Terminal";
            if(!carton.IsSAGCTNull() && carton.SAGCT.Trim().Length > 0)
                this.ArrivalLocation.Text = carton.SAGCT.Trim() + "/" + carton.SAGST.Trim();
            else
                this.ArrivalLocation.Text = carton.IsAGCTNull() ? "" : carton.AGCT.Trim() + "/" + carton.AGST.Trim();
        }
        
        //Store delivery
        this.StoreDeliveryDate.Text = this.StoreDeliveryStatus.Text = this.StoreDeliveryLocation.Text = "";
        if(carton.SCNTP == 3 && !carton.IsACTSDDNull() && carton.ACTSDD.Trim().Length > 0) {
            this.StoreDeliveryDate.Text = carton.ACTSDD.Trim(); ;		
            this.StoreDeliveryStatus.Text = "Out For Delivery";
            this.StoreDeliveryLocation.Text = carton.SCT.Trim() + "/" + carton.SST.Trim();
        }
        
        //POD
        this.PODDate.Text = this.PODStatus.Text = this.PODLocation.Text = "";
        this.lnkPODReq.Enabled = false;
        this.lnkPODReq.ToolTip = "POD only available after carton delivery";
        if(carton.SCNTP == 3 && !carton.IsSCDNull() && carton.SCD.Trim().Length > 0) {
            //Check for mis-routed carton- podScan is estimated by UPS (or other agent)
            this.PODDate.Text = carton.SCD.Trim() + " " + carton.SCTM.Trim();
            if(carton.T.Trim().Length == 18 && carton.T.Trim().Substring(0,2).ToLower() == "1z")
                this.PODStatus.Text = "Rerouted: Tracking # " + carton.T.Trim();
            else {
                switch(carton.OM) {
                    case "M": this.PODStatus.Text = carton.ISMN == 1 ? "Delivered (Scan N/A - Manual Entry)" : "Delivered"; break;
                    case "S": this.PODStatus.Text = "Short At Delivery"; break;
                    case "O": this.PODStatus.Text = "Over At Delivery"; break;
                    case "A": this.PODStatus.Text = "MisRoute At Delivery"; break;
                }
            }
            this.PODLocation.Text = carton.SCT.Trim() + "/" + carton.SST.Trim();
            
            //POD Request link available if carton delivered
            this.lnkPODReq.Enabled = true;
            MembershipServices membership = new MembershipServices();
            if(membership.IsAdmin ||membership.IsArgix  || membership.IsPODMember)
                this.lnkPODReq.ToolTip = "Display POD image (if available; otherwise POD request will be submitted)";
            else 
                this.lnkPODReq.ToolTip = "Submit a POD request";
        }
        this.cartonPanel.Visible = true;
    }
    #endregion
}
