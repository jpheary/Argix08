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

public partial class CartonNumberDiscrepancyWithText :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Carton Number Discrepancy (Non-Manifest)";
    private const string REPORT_SRC = "/Customer Service/Carton Number Discrepancy From Text";
    private const string REPORT_DS = "CartonNumberDiscrepancyDS";
    private const string USP_REPORT = "uspRptCartonNumberDiscrepancyFromText",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            OnAllVendorsChecked(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        this.grdPickups.DataBind();
        OnPickupSelected(this.grdPickups,EventArgs.Empty);
    }
    protected void OnAllVendorsChecked(object sender,EventArgs e) {
        //Event handler for change in from date
        OnClientSelected(this.dgdClientVendor,EventArgs.Empty);
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        this.dgdClientVendor.VendorSelectedIndex = this.chkAllVendors.Checked ? -1 : 0;
        this.dgdClientVendor.VendorsEnabled = !this.chkAllVendors.Checked && this.dgdClientVendor.VendorCount > 0;
        OnVendorSelected(this.dgdClientVendor,EventArgs.Empty);
    }
    protected void OnVendorSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        this.grdPickups.DataBind();
        OnPickupSelected(this.grdPickups,EventArgs.Empty);
    }
    protected void OnPickupSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnNumbersChanged(object sender,EventArgs e) {
        //Event handler for change in numbers
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.txtNumbers.Text.Trim().Length > 0 && this.grdPickups.Rows.Count > 0 && this.grdPickups.SelectedRow != null);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _numbers = this.txtNumbers.Text.Replace("\r\n",",");
        string _client = this.dgdClientVendor.ClientNumber;
        string _div = this.dgdClientVendor.ClientDivsionNumber;
        string _clientName = this.dgdClientVendor.ClientName;

        DataKey dataKey = (DataKey)this.grdPickups.DataKeys[this.grdPickups.SelectedRow.RowIndex];
        string _vendor = dataKey["VendorNumber"].ToString();
        string _vendorName = dataKey["VendorName"].ToString();
        string _pudate = DateTime.Parse(dataKey["PUDate"].ToString()).ToString("yyyy-MM-dd");
        string _punum = dataKey["PUNumber"].ToString();

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _numbers,_client,_div,_vendor,_pudate,_punum });
        if (ds.Tables[TBL_REPORT] != null && ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter div = new ReportParameter("ClientDivision",_div);
                    ReportParameter vendor = new ReportParameter("VendorNumber",_vendor);
                    ReportParameter pudate = new ReportParameter("PUDate",_pudate);
                    ReportParameter punum = new ReportParameter("PuNumber",_punum);
                    ReportParameter vendorName = new ReportParameter("VendorName",_vendorName);
                    ReportParameter clientName = new ReportParameter("ClientName",_clientName);
                    report.SetParameters(new ReportParameter[] { client,div,vendor,pudate,punum,vendorName,clientName });

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
                    Response.AddHeader("Content-Disposition","inline; filename=CartonNumberDiscrepancyWithText.xls");
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
