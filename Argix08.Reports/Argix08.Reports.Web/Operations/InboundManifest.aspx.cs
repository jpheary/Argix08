using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class InboundManifest :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Inbound Manifest";
    private const string REPORT_SRC = "/Operations/Inbound Manifest";
    private const string REPORT_DS = "InboundManifestDS";
    private const string USP_REPORT = "uspRptManifestDetailGetList",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            this.ddpPickups.ToDate = DateTime.Today;
            OnFromToDateChanged(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Update vendor list
        this.dgdClientVendorLog.StartDate = this.ddpPickups.ToDate;
        this.dgdClientVendorLog.EndDate = this.ddpPickups.ToDate.AddDays(1).AddMilliseconds(-1);
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnVendorLogEntrySelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor log
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = this.dgdClientVendorLog.VendorLogEntries.VendorLogTable.Rows.Count > 0;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for view button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _client = this.dgdClientVendorLog.ClientNumber;
        string _div = this.dgdClientVendorLog.ClientDivsionNumber;
        string _name = this.dgdClientVendorLog.ClientName;
        string _manifestID = "",_pickupDate = "",_pickupNum = "";

        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = new DataSet(REPORT_DS);
        for(int i=0;i<this.dgdClientVendorLog.VendorLogEntries.VendorLogTable.Rows.Count;i++) {
            _manifestID = this.dgdClientVendorLog.VendorLogEntries.VendorLogTable[i].ID.ToString();
            _pickupDate = this.dgdClientVendorLog.VendorLogEntries.VendorLogTable[i].PickupDate.ToString("MM-dd-yyyy");
            _pickupNum = this.dgdClientVendorLog.VendorLogEntries.VendorLogTable[i].PickupNumber.ToString();
            DataSet _ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _manifestID });
            _ds.Tables[TBL_REPORT].Columns.Add("ManifestID");
            _ds.Tables[TBL_REPORT].Columns.Add("PickupDate");
            _ds.Tables[TBL_REPORT].Columns.Add("PickupNumber");
            for(int j = 0;j < _ds.Tables[TBL_REPORT].Rows.Count;j++) {
                _ds.Tables[TBL_REPORT].Rows[j]["ManifestID"] = _manifestID;
                _ds.Tables[TBL_REPORT].Rows[j]["PickupDate"] = _pickupDate;
                _ds.Tables[TBL_REPORT].Rows[j]["PickupNumber"] = _pickupNum;
            }
            ds.Merge(_ds);
        }
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter manifestID = new ReportParameter("ManifestID", "");
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter div = new ReportParameter("ClientDivision",_div);
                    ReportParameter name = new ReportParameter("ClientName",_name);
                    ReportParameter punumber = new ReportParameter("PUNumber","");
                    ReportParameter pudate = new ReportParameter("PUDate", "");
                    report.SetParameters(new ReportParameter[] { manifestID,client,div,name,punumber,pudate });

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
                    Response.AddHeader("Content-Disposition","inline; filename=AuditTrailer.xls");
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
