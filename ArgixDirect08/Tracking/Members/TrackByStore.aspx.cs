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

public partial class TrackByStore:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Init controls
            this.dtpFromDate.SelectedDate = DateTime.Today.AddDays(-((int)Application["DateDaysSpread"]));
            this.dtpToDate.SelectedDate = DateTime.Today;
            this.cboClient.DataBind();

            MembershipServices membership = new MembershipServices();
            ProfileCommon profile = membership.MemberProfile;
            this.chkSubSearch.Checked = (profile.StoreSearchType == "Sub");
            this.chkSubSearch.Visible = (membership.IsAdmin || membership.IsArgix);

            this.msg2Label.Text = "Delivery date can't be more than " + Application["DateDaysForward"].ToString() + " days in the future.";
            this.msg3Label.Text = "From date can't be older than " + Application["DateDaysBack"].ToString() + " days and must be less than To date.";
            this.msg4Label.Text = "Date range can't be more than " + Application["DateDaysSpread"].ToString() + " days.";
            this.msg1Label.ForeColor = this.msg2Label.ForeColor = this.msg3Label.ForeColor = this.msg4Label.ForeColor = System.Drawing.Color.Black;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        //Validate business rules
        bool valid1 = true;
        if(this.cboDateType.SelectedValue == "Pickup") {
            //Pickups: can't be in the future
            valid1 = (this.dtpToDate.SelectedDate <= DateTime.Today);
            this.msg1Label.ForeColor = valid1 ? System.Drawing.Color.Black : System.Drawing.Color.Red;
            this.msg2Label.ForeColor = System.Drawing.Color.Black;
        } 
        else {
            //Deliveries: can't be more than 7 days in the future
            valid1 = (this.dtpToDate.SelectedDate <= DateTime.Today.AddDays((int)Application["DateDaysForward"]));
            this.msg1Label.ForeColor = System.Drawing.Color.Black;
            this.msg2Label.ForeColor = valid1 ? System.Drawing.Color.Black : System.Drawing.Color.Red;
        }

        //From date can't be greater than 30 days old and can't exceed To date
        bool valid2 = (this.dtpFromDate.SelectedDate >= DateTime.Today.AddDays(-(int)Application["DateDaysBack"]) && this.dtpFromDate.SelectedDate <= this.dtpToDate.SelectedDate);
        this.msg3Label.ForeColor = valid2 ? System.Drawing.Color.Black : System.Drawing.Color.Red;
        
        //Max date range is 7 days
        bool valid3 = (this.dtpFromDate.SelectedDate >= this.dtpToDate.SelectedDate.AddDays(-(int)Application["DateDaysSpread"]));
        this.msg4Label.ForeColor = valid3 ? System.Drawing.Color.Black : System.Drawing.Color.Red;

        //Validate report creation
        this.btnTrack.Enabled = (valid1 && valid2 && valid3 && this.cboClient.SelectedValue.Length > 0 && this.txtStore.Text.Length > 0);
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for user requested to track one or more cartons
        switch(e.CommandName) {
            case "Track":
                if(Page.IsValid) {
                    //Track by store or substore depending upon user profile specification
                    MembershipServices membership = new MembershipServices();
                    ProfileCommon profile = membership.MemberProfile;
                    if(profile.StoreSearchType == "Sub" || this.chkSubSearch.Checked) {
                        //Get list of Argix store selections for the requested substore
                        EnterpriseDS stores = getArgixStores(this.txtStore.Text);
                        if(stores.StoreTable.Rows.Count == 0) {
                            //No records; notify store not found
                            Master.ShowMsgBox("Store not found. Please try again.");
                        }
                        else if(stores.StoreTable.Rows.Count == 1) {
                            //Single substore; process without prompting the user
                            track(stores.StoreTable[0].NUMBER.ToString(), true);
                        }
                        else {
                            //Ambiguous substore selections; prompt user to select the desired substore
                            this.lstStores.DataSource = stores;
                            this.lstStores.DataTextField = "DESCRIPTION";
                            this.lstStores.DataValueField = "NUMBER";
                            this.lstStores.DataBind();
                            this.mvMain.SetActiveView(this.vwSelectStore);
                        }
                    }
                    else {
                        track(this.txtStore.Text, false);
                    }
                }
                break;
            case "Continue":
                track(this.lstStores.SelectedValue,true);
                break;
            case "Cancel":
                this.mvMain.SetActiveView(this.vwSearchStore);
                break;
        }
    }
    #region Data Services: getArgixStores(), track(), buildSummary()
    private EnterpriseDS getArgixStores(string subStoreNumber) {
        //Get a list of Argix-numbered stores for the specified sub-store number
        MembershipServices membership = new MembershipServices();
        ProfileCommon profile = membership.MemberProfile;
        string vendorID = (profile.Type.ToLower() == "vendor") ? profile.ClientVendorID : null;
        string clientID = this.cboClient.SelectedValue;
        
        TrackingServices svcs = new TrackingServices();
        EnterpriseDS stores = svcs.GetStoresForSubStore(subStoreNumber,clientID,vendorID);
        return stores;
    }
    private void track(string storeNumber, bool isSubStoreSearch) {
        //
        //Flag search by method
        Session["TrackBy"] = "Store";
        Session["SubStore"] = isSubStoreSearch ? this.txtStore.Text : null;

        //Track
        bool byPickup = this.cboDateType.SelectedValue == "Pickup";
        MembershipServices membership = new MembershipServices();
        ProfileCommon profile = membership.MemberProfile;
        string vendorID = (profile.Type.ToLower() == "vendor") ? profile.ClientVendorID : null;
        string clientID = this.cboClient.SelectedValue;

        TrackingServices svcs = new TrackingServices();
        TrackingDS cartons = new TrackingDS();
        cartons.Merge(svcs.GetCartonsForStore(clientID,storeNumber,this.dtpFromDate.SelectedDate,this.dtpToDate.SelectedDate,vendorID,byPickup));
        if(cartons.CartonDetailForStoreTable.Rows.Count > 0) {
            //Capture substore (if applicable); set search results into Session state; redirect
            //to the summary page
            TrackingDS summary = buildSummary(cartons);
            Session["StoreSummary"] = summary;
            Session["StoreDetail"] = cartons;
            Page.Response.Redirect("StoreSummary.aspx",false);
        } 
        else {
            //Notify user that there are no cartons for the specified store
            Master.ShowMsgBox("No records found. Please try again.");
        }
    }
    private TrackingDS buildSummary(TrackingDS cartonData) {
        //TL:0420603 has different dates - 26 and 28
        //TL:0510603 does not have any dates
        TrackingDS uniqueTLs = new TrackingDS();
        uniqueTLs.Merge(cartonData.CartonDetailForStoreTable.DefaultView.ToTable(true,new string[] { "TL" }));
        foreach(TrackingDS.CartonDetailForStoreTableRow row in uniqueTLs.CartonDetailForStoreTable.Rows) {
            row.CartonCount = cartonData.CartonDetailForStoreTable.Select("TL='" + row.TL + "'").Length;
            row.Weight = int.Parse(cartonData.CartonDetailForStoreTable.Compute("Sum(weight)","TL='" + row.TL + "'").ToString());
            object minDate = cartonData.CartonDetailForStoreTable.Compute("Min(PodDate)","TL='" + row.TL + "' AND (IsNull(PodDate,#01/01/1900#) <> #01/01/1900#)");

            TrackingDS.CartonDetailForStoreTableRow firstRow = (TrackingDS.CartonDetailForStoreTableRow)(cartonData.CartonDetailForStoreTable.Select("TL='" + row.TL + "'"))[0];
            //set pod/eta with pod date if available otherwise use ofdi date
            if(minDate != System.DBNull.Value)
                row.PodDate = DateTime.Parse(minDate.ToString());
            else {
                if(!firstRow.IsOFD1Null()) row.PodDate = firstRow.OFD1;
            }
            row.Store = firstRow.Store;
            row.CBOL = firstRow.IsCBOLNull() ? "" : firstRow.CBOL;
            if(firstRow.Trf == "N")
                row.AgName = firstRow.AgName;
            else
                row.AgName = firstRow.AgName + " (Transfer)";
            row.AcceptChanges();
        }
        return uniqueTLs;
    }
    #endregion
}
