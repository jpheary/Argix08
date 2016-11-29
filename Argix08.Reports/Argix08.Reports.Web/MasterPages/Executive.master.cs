using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Argix;
using Microsoft.Reporting.WebForms;

public partial class ExecutiveMaster:System.Web.UI.MasterPage {
    //Members
    public event EventHandler ClientChanged=null;

    //Interface
    public string ReportTitle { get { return Master.ReportTitle; } set { Master.ReportTitle = value; } }
    public bool NavigatorVisible { get { return Master.NavigatorVisible; } set { Master.NavigatorVisible = value; } }
    public ReportViewer Viewer { get { return Master.Viewer; } }
    public bool Validated { set { if(value) OnValidateForm(null,EventArgs.Empty); else Master.Validated = false; } }
    public string Status { set { Master.Status = value; } }
    public void ReportError(Exception ex) { Master.ReportError(ex); }
    public Stream GetReportDefinition(string report) { return Master.GetReportDefinition(report); }
    public Stream CreateExportRdl(DataSet ds,string dataSetName,string dataSourceName) { return Master.CreateExportRdl(ds,dataSetName,dataSourceName); }

    public bool ShowSubAgents { get { return (bool)ViewState["ShowSubAgents"]; } set { ViewState["ShowSubAgents"]  = value; } }
    public string ParamsSelectedValue { get { return this.cboParam.SelectedValue; } set { this.cboParam.SelectedValue  = value; OnScopeParamChanged(this.cboParam,EventArgs.Empty); } }
    public bool ParamsEnabled { get { return this.cboParam.Enabled; } set { this.cboParam.Enabled  = value; } }
    public string StartDate { get { return this.cboDateValue.SelectedValue.Split(',')[1].Split(':')[0].Trim(); } }
    public string EndDate { get { return this.cboDateValue.SelectedValue.Split(',')[1].Split(':')[1].Trim(); } }
    public string ClientNumber { get { if(this.cboClient.SelectedValue != "") return this.cboClient.SelectedValue; else return null; } }
    public string ClientName { get { return this.cboClient.SelectedItem.Text; } }
    public string Division { get { if(this.cboParam.SelectedValue == "Divisions" && this.cboValue.SelectedValue != "") return this.cboValue.SelectedValue; else return null; } }
    public string AgentNumber { get { if(this.cboParam.SelectedValue == "Agents" && this.cboValue.SelectedValue != "" && this.cboSubAgent.SelectedValue == "") return this.cboValue.SelectedValue; else return null; } }
    public string SubAgentNumber { get { if((bool)ViewState["ShowSubAgents"] && this.cboParam.SelectedValue == "Agents" && this.cboValue.SelectedValue != "" && this.cboSubAgent.SelectedValue != "") return this.cboSubAgent.SelectedValue; else return null; } }
    public string Region { get { if(this.cboParam.SelectedValue == "Regions" && this.cboValue.SelectedValue != "") return this.cboValue.SelectedValue; else return null; } }
    public string District { get { if(this.cboParam.SelectedValue == "Districts" && this.cboValue.SelectedValue != "") return this.cboValue.SelectedValue; else return null; } }
    public string StoreNumber { get { if(this.cboParam.SelectedValue == "Stores" && this.txtStore.Text.Length > 0) return this.txtStore.Text; else return null; } }

    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.cboClient.DataBind();
            if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
            OnClientChanged(this.cboClient,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnScopeParamChanged(this.cboParam,EventArgs.Empty);
        OnValidateForm(null,EventArgs.Empty);
        if(this.ClientChanged != null) this.ClientChanged(this.cboClient,EventArgs.Empty);
    }
    protected void OnActiveOnlyChecked(object sender,EventArgs e) {
        //Event handler for check state changed
        this.cboClient.DataBind();
        if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
        OnClientChanged(null,EventArgs.Empty);
    }
    protected void OnScopeParamChanged(object sender,EventArgs e) {
        //Event handler for change in selected filter parameter
        this.cboValue.Visible = true;
        this.cboValue.DataSourceID = null;
        this.cboSubAgent.Visible = this.txtStore.Visible = false;
        this.cboSubAgent.DataSourceID = null;
        switch(this.cboParam.SelectedValue) {
            case "Divisions":
                this.odsValues.SelectMethod = "GetClientDivisions";
                this.odsValues.SelectParameters.Clear();
                this.odsValues.SelectParameters.Add(new Parameter("number",DbType.String,this.cboClient.SelectedValue));
                this.cboValue.DataSourceID = "odsValues";
                this.cboValue.DataTextField = "Division";
                this.cboValue.DataValueField = "Division";
                this.cboValue.AppendDataBoundItems = true;
                this.cboValue.Items.Clear();
                this.cboValue.Items.Add(new ListItem("All",""));
                this.cboValue.DataBind();
                break;
            case "Agents":
                this.odsValues.SelectMethod = "GetAgents";
                this.odsValues.SelectParameters.Clear();
                this.odsValues.SelectParameters.Add(new Parameter("mainZoneOnly",DbType.Boolean,"false"));
                this.cboValue.DataSourceID = "odsValues";
                this.cboValue.DataTextField = "AgentSummary";
                this.cboValue.DataValueField = "AgentNumber";
                this.cboValue.AppendDataBoundItems = true;
                this.cboValue.Items.Clear();
                this.cboValue.Items.Add(new ListItem("All",""));
                this.cboValue.DataBind();
                break;
            case "Regions":
                this.odsValues.SelectMethod = "GetClientRegions";
                this.odsValues.SelectParameters.Clear();
                this.odsValues.SelectParameters.Add(new Parameter("number",DbType.String,this.cboClient.SelectedValue));
                this.cboValue.DataSourceID = "odsValues";
                this.cboValue.DataTextField = "RegionName";
                this.cboValue.DataValueField = "Region";
                this.cboValue.AppendDataBoundItems = true;
                this.cboValue.Items.Clear();
                this.cboValue.Items.Add(new ListItem("All",""));
                this.cboValue.DataBind();
                break;
            case "Districts":
                this.odsValues.SelectMethod = "GetClientDistricts";
                this.odsValues.SelectParameters.Clear();
                this.odsValues.SelectParameters.Add(new Parameter("number",DbType.String,this.cboClient.SelectedValue));
                this.cboValue.DataSourceID = "odsValues";
                this.cboValue.DataTextField = "DistrictName";
                this.cboValue.DataValueField = "District";
                this.cboValue.AppendDataBoundItems = true;
                this.cboValue.Items.Clear();
                this.cboValue.Items.Add(new ListItem("All",""));
                this.cboValue.DataBind();
                break;
            case "Stores":
                this.cboValue.Visible = false;
                this.txtStore.Visible = true;
                this.txtStore.Text = "";
                break;
        }
        if(this.cboValue.Items.Count > 0) this.cboValue.SelectedIndex = 0;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnScopeValueChanged(object sender,EventArgs e) {
        //Event handler for change in filter value
        if(this.cboParam.SelectedValue == null) return;
        bool bShowSubAgents = ViewState["ShowSubAgents"]==null ? false : (bool)ViewState["ShowSubAgents"];
        if(bShowSubAgents && this.cboParam.SelectedValue == "Agents" && this.cboValue.SelectedValue != "") {
            this.cboSubAgent.DataSourceID = "odsSubAgents";
            this.cboSubAgent.Items.Clear();
            this.cboSubAgent.Items.Add(new ListItem("All",""));
            this.cboSubAgent.DataBind();
            if(this.cboSubAgent.Items.Count > 0) this.cboSubAgent.SelectedIndex = 0;
        }
        this.cboSubAgent.Visible = (bShowSubAgents && this.cboParam.SelectedValue == "Agents" && this.cboValue.SelectedValue != "");
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnSubAgentChanged(object sender,EventArgs e) {
        //Event handler for change in filter value
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnStoreChanged(object sender,EventArgs e) {
        //Event handler for change in store selection
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnDateParamChanged(object sender,EventArgs e) {
        //Load a list of date range selections
        if(this.cboDateValue.Items.Count > 0) this.cboDateValue.SelectedIndex = 0;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = true;
    }
}
