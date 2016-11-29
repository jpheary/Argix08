using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class DailyStoreShipments :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Daily Store Shipments";
    private const string REPORT_SRC = "/Customer Service/Daily Store Shipping";
    private const string REPORT_DS = "DailyShipmentDS";
    private const string USP_REPORT = "uspRptDailyShipmentsGetList",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            this.dgdClientDivision.ClientSelectedIndex = -1;
            OnClientSelected(null,EventArgs.Empty);
            OnFromToDateChanged(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for change in from/to date
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAgentChanged(object sender,EventArgs e) {
        //Event handler for change in agent value
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        this.dgdClientDivision.DivisionSelectedIndex = -1;
        OnDivisionSelected(null,EventArgs.Empty);
    }
    protected void OnDivisionSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.dgdClientDivision.DivisionNumber.Length > 0);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _startDate = this.ddpShipping.FromDate.ToString("yyyy-MM-dd");
        string _endDate = this.ddpShipping.ToDate.ToString("yyyy-MM-dd");
        string _startTime = "07:00:01",_endTime = "07:00:00";
        string _client = this.dgdClientDivision.ClientNumber;
        string _clientName = this.dgdClientDivision.ClientName;
        string _clientDiv = this.dgdClientDivision.DivisionNumber;
        string _div = this.txtDivision.Text.Trim().Length > 0 ? this.txtDivision.Text : null;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _client,_clientDiv,_startDate,_endDate,_startTime,_endTime,_div });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter clientName = new ReportParameter("ClientName",_clientName);
                    ReportParameter clientDiv = new ReportParameter("ClientDivision",_clientDiv);
                    ReportParameter startDate = new ReportParameter("StartShipDate",_startDate);
                    ReportParameter endDate = new ReportParameter("EndShipDate",_endDate);
                    ReportParameter startTime = new ReportParameter("StartShipTime",_startTime);
                    ReportParameter endTime = new ReportParameter("EndShipTime",_endTime);
                    ReportParameter div = new ReportParameter("Division",_div);
                    report.SetParameters(new ReportParameter[] { client,clientName,clientDiv,startDate,endDate,startTime,endTime,div });

                    //Update report rendering with new data
                    report.Refresh();
                    
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
                case "Data":
                    //Set local export report and data source
                    report.LoadReportDefinition(Master.CreateExportRdl(ds,REPORT_DS,"RGXSQLR.TSORT"));
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                    report.Refresh();
                    break;
                case "Excel":
                    //Create Excel mime-type page
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition","inline; filename=AmscanImperialDelivery.xls");
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
