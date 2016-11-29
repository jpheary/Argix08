using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argix.Customers;

public partial class IssueNew:System.Web.UI.Page {
    //Members
    private const string SCOPE_NONE = "";
    private const string SCOPE_DISTRICTS = "Districts", SCOPE_REGIONS = "Regions", SCOPE_AGENTS = "Agents";
    private const string SCOPE_STORES = "Stores", SCOPE_SUBSTORES = "Substores";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //this.cboCompany.Enabled = this.cboCompany.Items.Count > 0;
            //this.cboScope.Enabled = this.cboLocation.Enabled = this.txtStore.Enabled = this.lnkStoreDetail.Enabled = this.txtStoreDetail.Enabled = false;
            this.cboCompany.DataBind();
            if(this.cboCompany.Items.Count > 0) this.cboCompany.SelectedIndex = 0;
            OnCompanyChanged(this.cboCompany,EventArgs.Empty);
            this.btnOk.Enabled = false;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnCompanyChanged(object sender,EventArgs e) {
        //Event handler for change in selected company
        //Validate
        if(this.cboCompany.SelectedItem == null) return;

        //Set applicable scopes for companies (i.e. client, pre-paid vendor)
        string number = this.cboCompany.SelectedValue;
        CompanyDS companies = new IssueMgtServiceClient().GetCompanies();
        CompanyDS.CompanyTableRow[] rows = (CompanyDS.CompanyTableRow[])companies.CompanyTable.Select("Number='" + number + "'");
        this.cboScope.Items.Clear();
        this.cboScope.Items.AddRange(new ListItem[] { new ListItem(SCOPE_AGENTS) });
        this.cboScope.SelectedValue = SCOPE_AGENTS;
        if(rows[0].CompanyType == "20") {
            this.cboScope.Items.AddRange(new ListItem[] { new ListItem(SCOPE_DISTRICTS),new ListItem(SCOPE_REGIONS),new ListItem(SCOPE_STORES),new ListItem(SCOPE_SUBSTORES) });
            this.cboScope.SelectedValue = SCOPE_STORES;
        }

        //Update locations since company changed
        this.cboScope.Enabled = true;
        OnScopeChanged(null,EventArgs.Empty);
    }
    protected void OnScopeChanged(object sender,EventArgs e) {
        //Event handler for change in selected scope
        //Validate
        if(this.cboScope.SelectedItem == null) return;

        //Prepare a location selector for the specified scope
        switch(this.cboScope.SelectedItem.ToString()) {
            case SCOPE_DISTRICTS:
                this.mvLocation.SetActiveView(this.vwOther);
                this.cboLocation.DataSourceID = "odsDistricts";
                this.cboLocation.DataTextField="LocationName";
                this.cboLocation.DataValueField="Location";
                this.cboLocation.DataBind();
                this.cboLocation.Enabled = this.cboLocation.Items.Count > 0;
                if(this.cboLocation.Items.Count > 0) this.cboLocation.SelectedIndex = 0;
                break;
            case SCOPE_REGIONS:
                this.mvLocation.SetActiveView(this.vwOther);
                this.cboLocation.DataSourceID = "odsRegions";
                this.cboLocation.DataTextField="LocationName";
                this.cboLocation.DataValueField="Location";
                this.cboLocation.DataBind();
                this.cboLocation.Enabled = this.cboLocation.Items.Count > 0;
                if(this.cboLocation.Items.Count > 0) this.cboLocation.SelectedIndex = 0;
                break;
            case SCOPE_STORES:
            case SCOPE_SUBSTORES:
                this.mvLocation.SetActiveView(this.vwStore);
                this.txtStore.Text = "";
                showStoreDetail();
                break;
            case SCOPE_AGENTS:
                this.mvLocation.SetActiveView(this.vwOther);
                this.cboLocation.DataSourceID = "odsAgents";
                this.cboLocation.DataTextField="AgentName";
                this.cboLocation.DataValueField="AgentNumber";
                this.cboLocation.DataBind();
                this.cboLocation.Enabled = this.cboLocation.Items.Count > 0;
                if(this.cboLocation.Items.Count > 0) this.cboLocation.SelectedIndex = 0;
                break;
        }
        OnLocationChanged(null,EventArgs.Empty);
    }
    protected void OnLocationChanged(object sender,EventArgs e) {
        //Event handler for change in selected location
        switch(this.cboScope.SelectedItem.ToString()) {
            case SCOPE_DISTRICTS:
            case SCOPE_REGIONS:
            case SCOPE_AGENTS:
                break;
            case SCOPE_STORES:
            case SCOPE_SUBSTORES:
                this.txtStoreDetail.Text = ""; ;
                break;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnStoreChanged(object sender,EventArgs e) {
        //Event handler for change in store
        if(this.txtStore.Text.Length > 0) {
            switch(this.cboScope.SelectedItem.ToString()) {
                case SCOPE_DISTRICTS:
                case SCOPE_REGIONS:
                case SCOPE_AGENTS:
                    break;
                case SCOPE_STORES:
                case SCOPE_SUBSTORES:
                    showStoreDetail();
                    break;
            }
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnIssueCategoryChanged(object sender,EventArgs e) {
        //Event handler for change in issue category
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnIssueTypeChanged(object sender,EventArgs e) {
        //Event handler for change in selected issue type
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnContactChanged(object sender,EventArgs e) {
        //Event handler for change in selected contact
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnSubjectChanged(object sender,EventArgs e) {
        //Event handler for change in subject
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for control value changes
        bool locationValid = ((this.cboScope.SelectedValue == SCOPE_AGENTS || this.cboScope.SelectedValue == SCOPE_DISTRICTS || this.cboScope.SelectedValue == SCOPE_REGIONS) && this.cboLocation.SelectedValue.Length > 0) || 
                                 ((this.cboScope.SelectedValue == SCOPE_STORES || this.cboScope.SelectedValue == SCOPE_SUBSTORES) && this.txtStoreDetail.Text.Length > 0);
        this.btnOk.Enabled = (locationValid && 
                            this.cboIssueType.SelectedValue.Length > 0 &&
                            this.cboContact.SelectedValue.Length > 0 &&
                            this.txtSubject.Text.Length > 0);
    }
    protected void OnButtonClick(object sender,EventArgs e) {
        //Event handler for command button clicked
        Button btn = (Button)sender;
        switch(btn.ID) {
            case "btnCancel":
                Response.Redirect("~/IssueMgt.aspx");
                break;
            case "btnOk":
                Issue issue = new Issue();
                CompanyDS companies = new IssueMgtServiceClient().GetCompanies();
                CompanyDS.CompanyTableRow[] rows = (CompanyDS.CompanyTableRow[])companies.CompanyTable.Select("Number='" + this.cboCompany.SelectedValue + "'");
                issue.CompanyID = rows[0].CompanyID;
                switch(this.cboScope.SelectedValue) {
                    case SCOPE_AGENTS: issue.AgentNumber = (this.cboLocation.SelectedValue!="All" ? this.cboLocation.SelectedValue : ""); break;
                    case SCOPE_DISTRICTS: issue.DistrictNumber = (this.cboLocation.SelectedValue != "All" ? this.cboLocation.SelectedValue : ""); break;
                    case SCOPE_REGIONS: issue.RegionNumber = (this.cboLocation.SelectedValue != "All" ? this.cboLocation.SelectedValue : ""); break;
                    case SCOPE_STORES: issue.StoreNumber = Convert.ToInt32(this.txtStore.Text); break;
                    case SCOPE_SUBSTORES: issue.StoreNumber = Convert.ToInt32(this.txtStore.Text); break;
                }
                issue.ContactID = Convert.ToInt32(this.cboContact.SelectedValue);
                issue.TypeID = Convert.ToInt32(this.cboIssueType.SelectedValue);
                issue.Subject = this.txtSubject.Text;
                //issue.OFD1FromDate = null;
                //issue.OFD1ToDate = null;
                //issue.PROID = null;
                IssueMgtServiceClient crgService  = new IssueMgtServiceClient();
                long id = crgService.CreateIssue(issue);
                Response.Redirect("~/IssueMgt.aspx?issueID=" + id.ToString());
                break;
        }
    }
    #region Local Services: showStoreDetail(), getStoreDetailString(), getDeliveryDays(), getOFD()
    private void showStoreDetail() {
        //
        this.txtStoreDetail.Text = "";
        if(this.cboCompany.SelectedValue.Length > 0 && this.txtStore.Text.Length > 0) {
            StoreDS ds = new StoreDS();
            CompanyDS companies = new IssueMgtServiceClient().GetCompanies();
            CompanyDS.CompanyTableRow[] rows = (CompanyDS.CompanyTableRow[])companies.CompanyTable.Select("Number='" + this.cboCompany.SelectedValue + "'");
            if(rows.Length > 0) {
                int companyID = rows[0].CompanyID;
                switch(this.cboScope.SelectedItem.ToString()) {
                    case SCOPE_STORES: ds.Merge(new IssueMgtServiceClient().GetStoreDetail(companyID,Convert.ToInt32(this.txtStore.Text))); break;
                    case SCOPE_SUBSTORES: ds.Merge(new IssueMgtServiceClient().GetSubStoreDetail(companyID,this.txtStore.Text)); break;
                }
                if(ds.StoreTable.Rows.Count > 0) this.txtStoreDetail.Text = getStoreDetailString(ds);
            }
        }
    }
    private string getStoreDetailString(StoreDS storeDS) {
        //Return a string of store detail
        StringBuilder detail = new StringBuilder();
        if(storeDS.StoreTable.Rows.Count > 0) {
            StoreDS.StoreTableRow store = storeDS.StoreTable[0];
            detail.AppendLine(store.StoreName.Trim() + " (store #" + store.StoreNumber.ToString() + "; substore #" + store.SubStoreNumber.Trim() + ")");
            detail.AppendLine((!store.IsStoreAddressline1Null() ? store.StoreAddressline1.Trim() : ""));
            detail.AppendLine((!store.IsStoreAddressline2Null() ? store.StoreAddressline2.Trim() : ""));
            detail.AppendLine((!store.IsStoreCityNull() ? store.StoreCity.Trim() : "") + ", " + 
                                                (!store.IsStoreStateNull() ? store.StoreState.Trim() : "") + " " +
                                                (!store.IsStoreZipNull() ? store.StoreZip.Trim() : ""));
            detail.AppendLine((!store.IsContactNameNull() ? store.ContactName.Trim() : "") + ", " + (!store.IsPhoneNumberNull() ? store.PhoneNumber.Trim() : ""));
            detail.AppendLine((!store.IsRegionDescriptionNull() ? store.RegionDescription.Trim() : "") + 
                                                " (" + (!store.IsRegionNull() ? store.Region.Trim() : "") + "), " + 
                                                (!store.IsDistrictNameNull() ? store.DistrictName.Trim() : "") + 
                                                " (" + (!store.IsDistrictNull() ? store.District.Trim() : "") + ")");
            detail.AppendLine("Zone " + (!store.IsZoneNull() ? store.Zone.Trim() : "") + ", " +
                                                "Agent " + (!store.IsAgentNumberNull() ? store.AgentNumber.Trim() : "") + " " +
                                                (!store.IsAgentNameNull() ? store.AgentName.Trim() : ""));
            detail.AppendLine("Window " + (!store.IsWindowTimeStartNull() ? store.WindowTimeStart.ToString("HH:mm") : "") + " - " +
                                                (!store.IsWindowTimeEndNull() ? store.WindowTimeEnd.ToString("HH:mm") : "") + ", " +
                                                "Del Days " + getDeliveryDays(store) + ", " + 
                                                (!store.IsScanStatusDescrptionNull() ? store.ScanStatusDescrption.Trim() : ""));
            detail.AppendLine("JA Transit " + (!store.IsStandardTransitNull() ? store.StandardTransit.ToString() : "") + ", " + "OFD " + getOFD(store));
            detail.AppendLine("Special Inst: " + (!store.IsSpecialInstructionsNull() ? store.SpecialInstructions.Trim() : ""));
        }
        return detail.ToString();
    }
    private string getDeliveryDays(StoreDS.StoreTableRow store) {
        //Return delivery days from the dataset
        string ddays = "";

        //Check for overrides
        if(!store.IsIsDeliveryDayMondayNull()) ddays += (store.IsDeliveryDayMonday.Trim() == "Y" ? "M" : "");
        if(!store.IsIsDeliveryDayTuesdayNull()) ddays += (store.IsDeliveryDayTuesday.Trim() == "Y" ? "T" : "");
        if(!store.IsIsDeliveryDayWednesdayNull()) ddays += (store.IsDeliveryDayWednesday.Trim() == "Y" ? "W" : "");
        if(!store.IsIsDeliveryDayThursdayNull()) ddays += (store.IsDeliveryDayThursday.Trim() == "Y" ? "R" : "");
        if(!store.IsIsDeliveryDayFridayNull()) ddays += (store.IsDeliveryDayFriday.Trim() == "Y" ? "F" : "");

        //If no overrides, then all days are valid
        if(ddays.Trim().Length == 0) ddays = "MTWRF";
        return ddays;
    }
    private string getOFD(StoreDS.StoreTableRow store) {
        //Return delivery days from the dataset
        string ofd = "";

        //OFD1, OFD2, or NONE
        if(!store.IsIsOutforDeliveryDay1Null())
            ofd += (store.IsOutforDeliveryDay1.Trim() == "Y" ? "DAY1" : "");
        else if(!store.IsIsOutforDeliveryDay2Null())
            ofd += (store.IsOutforDeliveryDay2.Trim() == "Y" ? "DAY2" : "");
        else
            ofd += "";
        return ofd;
    }
    #endregion
}
