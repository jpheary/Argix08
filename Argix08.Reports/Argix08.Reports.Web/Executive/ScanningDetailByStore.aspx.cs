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

public partial class ScanningDetailByStore :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Scanning Detail By Store";
    private const string REPORT_SRC = "/Executive/Scanning Detail By Store";
    private const string REPORT_DS = "ScanningDetailByStoreDS";
    private const string USP_REPORT = "uspRptScanningDetailByStore",TBL_REPORT = "NewTable";
    
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
    protected void OnExceptionsOnlyChecked(object sender,EventArgs e) {
        //Eveny handler for change in Exceptions only state
        OnValidateForm(null,EventArgs.Empty);
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
        string _agent = Master.AgentNumber;
        string _region = Master.Region;
        string _district = Master.District;
        string _store = Master.StoreNumber;
        int _isException = this.chkExceptionsOnly.Checked ? 1 : 0;
        string _start = Master.StartDate;
        string _end = Master.EndDate;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _client,_division,_agent,_region,_district,_store,_start,_end,_isException });
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
                    ReportParameter agent = new ReportParameter("AgentNumber"); if(_agent != null) agent.Values.Add(_agent);
                    ReportParameter region = new ReportParameter("Region"); if(_region != null) region.Values.Add(_region);
                    ReportParameter district = new ReportParameter("District"); if(_district != null) district.Values.Add(_district);
                    ReportParameter store = new ReportParameter("StoreNumber"); if(_store != null) store.Values.Add(_store);
                    ReportParameter start = new ReportParameter("StartDate",_start);
                    ReportParameter end = new ReportParameter("EndDate",_end);
                    ReportParameter isException = new ReportParameter("IsExceptionOnly",_isException.ToString());
                    report.SetParameters(new ReportParameter[] { client,division,agent,region,district,store,start,end,isException,clientName });

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
                    Response.AddHeader("Content-Disposition","inline; filename=ScanningDetailByStore.xls");
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
