using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class InternalDeliveryWindowByStore:System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Internal Delivery Window By Store";
    private const string REPORT_SRC = "/Internal/Internal Delivery Window By Store";
    private const string REPORT_DS = "InternalDeliveryWindowByStoreDS";
    private const string USP_REPORT = "uspRptDeliveryWindowByStoreInternal",TBL_REPORT = "NewTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            this.cboDateValue.DataBind();
            if(this.cboDateValue.Items.Count > 0) this.cboDateValue.SelectedIndex = 1;
            this.txtFilter.Text = "2";
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnScopeParamChanged(this.cboParam,EventArgs.Empty);
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnActiveOnlyChecked(object sender,EventArgs e) {
        //Event handler for check state changed
        if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
        OnClientChanged(null,EventArgs.Empty);
    }
    protected void OnScopeParamChanged(object sender,EventArgs e) {
        //Event handler for change in selected filter parameter
        this.cboValue.Visible = true;
        this.cboValue.DataSourceID = null;
        this.txtStore.Visible = false;
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
        Master.Validated = (this.cboDateValue.SelectedValue.Length > 0 && this.cboClient.SelectedValue != null && this.cboValue.SelectedValue != null);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _client = this.cboClient.SelectedValue != "" ? this.cboClient.SelectedValue : null;
        string _division = this.cboParam.SelectedValue == "Divisions" && this.cboValue.SelectedValue != "" ? this.cboValue.SelectedValue : null;
        string _agent = this.cboParam.SelectedValue == "Agents" && this.cboValue.SelectedValue != "" ? this.cboValue.SelectedValue : null;
        string _region = this.cboParam.SelectedValue == "Regions" && this.cboValue.SelectedValue != "" ? this.cboValue.SelectedValue : null;
        string _district = this.cboParam.SelectedValue == "Districts" && this.cboValue.SelectedValue != "" ? this.cboValue.SelectedValue : null;
        string _store = this.cboParam.SelectedValue == "Stores" && this.txtStore.Text.Length > 0 ? this.txtStore.Text : null;
        string _start = this.cboDateValue.SelectedValue.Split(',')[0].Split(':')[0].Trim();
        string _end = this.cboDateValue.SelectedValue.Split(',')[0].Split(':')[1].Trim();
        string _filter = this.txtFilter.Text;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _client,_division,_agent,_region,_district,_store,_end,_filter });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter clientName = new ReportParameter("ClientName",this.cboClient.SelectedItem.Text);
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter division = new ReportParameter("Division",_division);
                    ReportParameter agent = new ReportParameter("AgentNumber",_agent);
                    ReportParameter region = new ReportParameter("Region",_region);
                    ReportParameter district = new ReportParameter("District",_district);
                    ReportParameter store = new ReportParameter("StoreNumber",_store);
                    ReportParameter start = new ReportParameter("StartDate",_start);
                    ReportParameter end = new ReportParameter("EndDate",_end);
                    ReportParameter filter = new ReportParameter("NotOnTimeDeliveriesFilter",_filter);
                    Master.Viewer.LocalReport.SetParameters(new ReportParameter[] { client,division,agent,region,district,store,end,filter });

                    //Update report rendering with new data
                    report.Refresh();
                    
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
                case "Data":
                    //Set local export report and data source
                    report.LoadReportDefinition(Master.CreateExportRdl(ds,REPORT_DS,"RGXSQLR.TSORT"));
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                    report.Refresh(); break;
                case "Excel":
                    //Create Excel mime-type page
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition","inline; filename=InternalDeliveryWindowByStore.xls");
                    Response.ContentType = "application/vnd.ms-excel";  //application/octet-stream";
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                    DataGrid dg = new DataGrid();
                    dg.DataSource = ds.Tables[TBL_REPORT];
                    dg.DataBind();
                    dg.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                    break;
            }
        }
        else {
            Master.Status = "There were no records found.";
        }
    }
}
