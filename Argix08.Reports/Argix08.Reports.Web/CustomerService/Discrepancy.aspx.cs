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

public partial class Discrepancy :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Discrepancy";
    private const string REPORT_SRC_TSORT_STATE = "/Customer Service/Discrepancy For Tsort By State";
    private const string REPORT_SRC_TSORT_STORE = "/Customer Service/Discrepancy For Tsort By Store";
    private const string REPORT_SRC_TSORT_ZONE = "/Customer Service/Discrepancy For Tsort By Zone";
    private const string REPORT_SRC_RETURNS_STORE = "/Customer Service/Discrepancy For Returns By Store";
    private const string REPORT_SRC_RETURNS_VENDOR = "/Customer Service/Discrepancy For Returns By Vendor";
    private const string REPORT_DS_TSORT = "DiscrepancyTsortDS";
    private const string REPORT_DS_RETURNS = "DiscrepancyReturnsDS";
    private const string USP_REPORT_VENDOR = "uspRptVendorDiscrepancyGetList",TBL_REPORT = "NewTable";
    private const string USP_REPORT_AGENT = "uspRptAgentDiscrepancyGetList";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            this.ddpPickups.FromDate = this.ddpPickups.ToDate = DateTime.Today;
            OnFreightTypeChanged(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handlre for change in from/to date
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnFreightTypeChanged(object sender,EventArgs e) {
        //Event handler for change in selected freight type
        try {
            this.cboViewBy.Items.Clear();
            if(this.cboFreightType.SelectedValue == "0") {
                //Configure for vendors as shippers
                this.cboViewBy.Items.Add("Store");
                this.cboViewBy.Items.Add("Zone");
                this.cboViewBy.Items.Add("State");
                this.dgdClientShipper.FreightType = FreightType.Regular;
            }
            else if(this.cboFreightType.SelectedValue == "1") {
                //Configure for agents as shippers
                this.cboViewBy.Items.Add("Store");
                this.cboViewBy.Items.Add("Vendor");
                this.dgdClientShipper.FreightType = FreightType.Returns;
            }
            this.cboViewBy.SelectedIndex = 0;
            OnClientSelected(null,null);
        }
        catch(Exception ex) { Master.ReportError(ex); }
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(this,EventArgs.Empty);
    }
    protected void OnShipperSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnControlError(object sender,EventArgs e) {
        Master.ReportError(new ApplicationException("Unexpected error in the ClientShipper control."));
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.dgdClientShipper.ShipperSelectedRow != null && this.cboViewBy.SelectedItem.Text.Length > 0);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _fromDate = this.ddpPickups.FromDate.ToString("yyyy-MM-dd");
        string _toDate = this.ddpPickups.ToDate.ToString("yyyy-MM-dd");
        string _client = this.dgdClientShipper.ClientNumber;
        string _div = this.dgdClientShipper.ClientDivsionNumber;
        string _clientName = this.dgdClientShipper.ClientName;
        string _shipper = this.dgdClientShipper.ShipperNumber;
        string _shipperName = this.dgdClientShipper.ShipperName;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = new DataSet();
        string reportFile = "", reportDS = "";
        if(this.cboFreightType.SelectedValue == "0") {
            //Tsort- set dataset and report types
            reportDS = REPORT_DS_TSORT;
            switch(this.cboViewBy.SelectedItem.Text) {
                case "State": reportFile = REPORT_SRC_TSORT_STATE; break;
                case "Zone": reportFile = REPORT_SRC_TSORT_ZONE; break;
                case "Store": reportFile = REPORT_SRC_TSORT_STORE; break;
            }
            ds = enterprise.FillDataset(USP_REPORT_VENDOR,TBL_REPORT,new object[] { _client,_div,_shipper,_fromDate,_toDate });
        }
        else if(this.cboFreightType.SelectedValue == "1") {
            //Returns- set dataset and report types
            reportDS = REPORT_DS_RETURNS;
            switch(this.cboViewBy.SelectedItem.Text) {
                case "Vendor": reportFile = REPORT_SRC_RETURNS_VENDOR; break;
                case "Store": reportFile = REPORT_SRC_RETURNS_STORE; break;
            }
            ds = enterprise.FillDataset(USP_REPORT_AGENT,TBL_REPORT,new object[] { _client,_div,_shipper,_fromDate,_toDate });
        }
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(reportFile);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(reportDS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter fromDate = new ReportParameter("StartPUDate",_fromDate);
                    ReportParameter toDate = new ReportParameter("EndPUDate",_toDate);
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter div = new ReportParameter("ClientDivision",_div);
                    ReportParameter clientName = new ReportParameter("ClientName",_clientName);
                    ReportParameter shipper = new ReportParameter((this.dgdClientShipper.FreightType == FreightType.Regular ? "VendorNumber" : "AgentNumber"),_shipper);
                    ReportParameter shipperName = new ReportParameter((this.dgdClientShipper.FreightType == FreightType.Regular ? "VendorName" : "AgentName"),_shipperName);
                    report.SetParameters(new ReportParameter[] { client,div,fromDate,toDate,clientName,shipper,shipperName });

                    //Update report rendering with new data
                    report.Refresh();
                    
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
                case "Data":
                    //Set local export report and data source
                    report.LoadReportDefinition(Master.CreateExportRdl(ds,reportDS,"RGXSQLR.TSORT"));
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(reportDS,ds.Tables[TBL_REPORT]));
                    report.Refresh();
                    break;
                case "Excel":
                    //Create Excel mime-type page
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition","inline; filename=Discrepancy.xls");
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
