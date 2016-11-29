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

public partial class OnTimeDeliverySummaryByAgent :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "On Time Delivery Summary By Agent";
    private const string REPORT_SRC = "/Executive/On Time Delivery Summary By Agent";
    private const string REPORT_DS = "OnTimeDeliverySummaryByAgentDS";
    private const string USP_REPORT = "uspRptOnTimeDeliverySummaryByAgent",TBL_REPORT = "NewTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            Master.ShowSubAgents = true;
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
        string _client = Master.ClientNumber;
        string _division = Master.Division;
        string _agentnumber = Master.AgentNumber;
        string _subagentnumber = Master.SubAgentNumber;
        string _region = Master.Region;
        string _district = Master.District;
        string _store = Master.StoreNumber;
        string _start = Master.StartDate;
        string _end = Master.EndDate;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _client,_division,_agentnumber,_subagentnumber,_region,_district,_store,_start,_end });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter clientName = new ReportParameter("ClientName",Master.ClientName);
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter division = new ReportParameter("Division",_division);
                    ReportParameter agentnumber = new ReportParameter("AgentParentNumber",_agentnumber);
                    ReportParameter subagentnumber = new ReportParameter("AgentNumber",_subagentnumber);
                    ReportParameter region = new ReportParameter("Region"); if(_region != null) region.Values.Add(_region);
                    ReportParameter district = new ReportParameter("District"); if(_district != null) district.Values.Add(_district);
                    ReportParameter store = new ReportParameter("StoreNumber"); if(_store != null) store.Values.Add(_store);
                    ReportParameter start = new ReportParameter("StartDate",_start);
                    ReportParameter end = new ReportParameter("EndDate",_end);
                    report.SetParameters(new ReportParameter[] { client,division,agentnumber,subagentnumber,region,district,store,start,end,clientName });

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
                    Response.AddHeader("Content-Disposition","inline; filename=OnTimeDeliverySummaryByAgent.xls");
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
