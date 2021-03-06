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

public partial class PODCompliance :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "POD Compliance";
    private const string REPORT_SRC = "/Executive/POD Compliance";
    private const string REPORT_DS = "PODComplianceDS";
    private const string USP_REPORT = "uspRptPODCompliance",TBL_REPORT = "NewTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = true;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _start = Master.FromDate.ToString("yyyy-MM-dd");
        string _end = Master.ToDate.ToString("yyyy-MM-dd");
        string _client = Master.ClientNumber;
        string _vendor = Master.VendorNumber;
        string _vendorLoc = Master.VendorLocationNumber;
        string _agent = Master.AgentNumber;
        string _agentLoc = Master.AgentLocationNumber;
        string _cutoffTime = DateTime.Now.ToString("yyyy-MM-dd");

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _start,_end,_agentLoc,_agent,_client,_vendorLoc,_vendor,_cutoffTime });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter start = new ReportParameter("StartDate",_start);
                    ReportParameter end = new ReportParameter("EndDate",_end);
                    ReportParameter client = new ReportParameter("ClientNumber"); if(_client != null) client.Values.Add(Master.ClientNumber + "- " + Master.ClientName);
                    ReportParameter vendor = new ReportParameter("VendorParentNumber"); if(_vendor != null) vendor.Values.Add(Master.VendorName);
                    ReportParameter vendorLoc = new ReportParameter("VendorNumber"); if(_vendorLoc != null) vendorLoc.Values.Add(Master.VendorName);
                    ReportParameter agent = new ReportParameter("AgentParentNumber"); if(_agent != null) agent.Values.Add(Master.AgentName);
                    ReportParameter agentLoc = new ReportParameter("AgentNumber"); if(_agentLoc != null) agentLoc.Values.Add(Master.AgentName);
                    report.SetParameters(new ReportParameter[] { start,end,vendor,vendorLoc,client,agent,agentLoc });

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
                    Response.AddHeader("Content-Disposition","inline; filename=ScanningSummaryByStore.xls");
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
