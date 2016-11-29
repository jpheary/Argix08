using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class CartonDetail :System.Web.UI.Page {
    //Members
    private string mLabelNumber="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.mLabelNumber = Request.QueryString["item"] == null ? "" : Request.QueryString["item"].ToString();
            ViewState["LabelNumber"] = this.mLabelNumber;

            //Set UI; hide links as required
            Argix.TrackingItems items = (Argix.TrackingItems)Session["TrackData"];
            Master.GoSummaryVisible = items != null && items.Count > 1;
            Master.GoTrackVisible = true;

            if(this.mLabelNumber.Length > 0) {
                if(items != null) {
                    //Find the items info for labelNumber
                    foreach(Argix.TrackingItem item in items) {
                        if(item.LabelNumber == this.mLabelNumber) {
                            showItem(item);
                            break;
                        }
                    }
                }
            }
            else
                Master.ShowMsgBox("Could not find tracking information. Please return to tracking page and try again.");
        }
        else {
            this.mLabelNumber = ViewState["LabelNumber"].ToString();
        }
    }
    private void showItem(Argix.TrackingItem item) {
        //Display detail
        Argix.TrackingItems detail = new Argix.TrackingItems();
        Argix.TrackingItem row = null;
        if(item.PODScanDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.PODScanDate;
            row.Status = item.PODScanStatus;
            row.Location = item.PODScanLocation;
            detail.Add(row);
        }
        if(item.ActualStoreDeliveryDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualStoreDeliveryDate;
            row.Status = item.ActualStoreDeliveryStatus;
            row.Location = item.ActualStoreDeliveryLocation;
            detail.Add(row);
        }
        if(item.ActualArrivalDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualArrivalDate;
            row.Status = item.ActualArrivalStatus;
            row.Location = item.ActualArrivalLocation;
            detail.Add(row);
        }
        if(item.ActualDepartureDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualDepartureDate;
            row.Status = item.ActualDepartureStatus;
            row.Location = item.ActualDepartureLocation;
            detail.Add(row);
        }
        if(item.SortFacilityArrivalDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.SortFacilityArrivalDate;
            row.Status = item.SortFacilityArrivalStatus;
            row.Location = item.SortFacilityLocation;
            detail.Add(row);
        }
        this.grdDetail.DataSource = detail;
        this.grdDetail.DataBind();

        //Display summary
        this.lblDetail_ID.Text += item.ItemNumber;
        this.lblDetail_Status.Text = item.Status.ToString();
        this.lblDetailSum.Text = "in " + item.Location + " on " + item.DateTime;
        this.lblFromInfo.Text = item.VendorName;
        this.lblFromInfo.Text += "\nPickup " + item.PickupDate;
        this.lblFromInfo.Text += "\nBOL# " + item.BOLNumber;
        this.lblFromInfo.Text += "\nLabel# " + item.LabelNumber + " on TL# " + item.TLNumber;
        this.lblToInfo.Text = item.StoreName;
        this.lblToInfo.Text += "\n" + item.StoreAddress1 +  ", " + item.StoreAddress2;
        this.lblToInfo.Text += "\n" + item.StoreCity + ", " + item.StoreState + " " + item.StoreZip;
        this.lblToInfo.Text += "\nSigner: " + item.Signer;
        this.lbShipInfo.Text = "Ship date: " + item.DateTime;
        this.lbShipInfo.Text += "\nPieces: ";
        this.lbShipInfo.Text += "\nTotal weight: " + item.Weight.ToString() + " lbs";
        this.lbShipInfo.Text += "\n";
    }
}
