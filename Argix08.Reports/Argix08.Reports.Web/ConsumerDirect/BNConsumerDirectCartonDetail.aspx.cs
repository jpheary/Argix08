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

public partial class BNConsumerDirectCartonDetail:System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "B&N Consumer Direct Carton Detail";
    private const string REPORT_SRC = "/Consumer Direct/BN Consumer Direct Carton Detail";
    private const string REPORT_DS = "ConsumerDirectCartonDetailDS";
    private const string USP_REPORT = "uspRptConsumerDirectCartonDetail",TBL_REPORT = "NewTable";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            OnDateParamChanged(this.cboDateParam,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnDateParamChanged(object sender,EventArgs e) {
        //Load a list of date range selections
        this.cboDateValue.DataBind();
        if(this.cboDateValue.Items.Count > 0) this.cboDateValue.SelectedIndex = 4;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnCustomerChanged(object sender,EventArgs e) {
        //Load a list of date range selections
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = this.cboCustomer.SelectedValue != null;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string dates = this.cboDateValue.SelectedValue;
        string _start = dates.Split(':')[0].Trim();
        string _end = dates.Split(':')[1].Trim();
        int _delivDays = Convert.ToInt32(this.cboDelivDays.Text);
        string _locType = this.cboLocType.Text;
        string _vendor = this.cboCustomer.SelectedValue;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _start,_end,_delivDays,_locType,_vendor });
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
                    ReportParameter delivDays = new ReportParameter("DeliveryDays",_delivDays.ToString());
                    ReportParameter locType = new ReportParameter("LocationType",_locType);
                    report.SetParameters(new ReportParameter[] { start,end,delivDays,locType });

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
                    Response.AddHeader("Content-Disposition","inline; filename=BNConsumerDirectCartonDetail.xls");
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
