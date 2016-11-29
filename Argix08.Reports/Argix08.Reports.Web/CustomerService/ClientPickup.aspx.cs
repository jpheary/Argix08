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

public partial class ClientPickup :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Client Pickup";
    private const string REPORT_SRC_DETAILBYSTORE = "/Customer Service/Client Pickup Detail By Store";
    private const string REPORT_SRC_DETAILBYZONE = "/Customer Service/Client Pickup Detail By Zone";
    private const string REPORT_SRC_SUMMARYBYSTORE = "/Customer Service/Client Pickup Summary By Store";
    private const string REPORT_SRC_SUMMARYBYZONE = "/Customer Service/Client Pickup Summary By Zone";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptPickupDetailsGetList",TBL_REPORT = "NewTable";
    
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
    protected void OnFromToDateChanged(object sender, EventArgs e) {
        //Event handler for change in from/to dates
        this.grdPickups.DataBind();
        OnPickupSelected(this.grdPickups,EventArgs.Empty);
    }
    protected void OnAllVendorsChecked(object sender,EventArgs e) {
        //Event handler for change in from date
        OnClientSelected(null,EventArgs.Empty);
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
    protected void OnAllPickupsSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        CheckBox chkAll = (CheckBox)this.grdPickups.HeaderRow.FindControl("chkAll");
        foreach(GridViewRow row in this.grdPickups.Rows) {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            chk.Checked = chkAll.Checked;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnPickupSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = SelectedRows.Length > 0;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _type = this.cboType.SelectedValue.ToLower();
        string _sort = this.cboSortBy.SelectedValue.ToLower();
        string _client = this.dgdClientVendor.ClientNumber;
        string _div = this.dgdClientVendor.ClientDivsionNumber;
        string _clientName = this.dgdClientVendor.ClientName;
        string _vendor = "",_vendorName = "",_pudate = "",_punum = "",_manifests = "",_trailers = "";

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = new DataSet(REPORT_DS);
        foreach(GridViewRow row in SelectedRows) {
            DataKey dataKey = (DataKey)this.grdPickups.DataKeys[row.RowIndex];
            _vendor = dataKey["VendorNumber"].ToString();
            _vendorName = dataKey["VendorName"].ToString();
            _pudate = DateTime.Parse(dataKey["PUDate"].ToString()).ToString("yyyy-MM-dd");
            _punum = dataKey["PUNumber"].ToString();
            _manifests = dataKey["ManifestNumbers"].ToString();
            _trailers = dataKey["TrailerNumbers"].ToString();
            DataSet _ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _client,_div,_vendor,_pudate,_punum,_vendorName,_manifests,_trailers });
            ds.Merge(_ds);
        }
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    string reportSrc="";
                    switch(_type.ToLower()) {
                        case "detail": reportSrc = (_sort == "store") ? REPORT_SRC_DETAILBYSTORE : REPORT_SRC_DETAILBYZONE; break;
                        case "summary": reportSrc = (_sort == "store") ? REPORT_SRC_SUMMARYBYSTORE : REPORT_SRC_SUMMARYBYZONE; break;
                    }
                    System.IO.Stream stream = Master.GetReportDefinition(reportSrc);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter div = new ReportParameter("ClientDivision",_div);
                    ReportParameter vendor = new ReportParameter("VendorNumber");
                    ReportParameter pudate = new ReportParameter("PUDate");
                    ReportParameter punum = new ReportParameter("PUNumber");
                    ReportParameter clientName = new ReportParameter("ClientName",_clientName);
                    ReportParameter vendorName = new ReportParameter("VendorName", "");
                    ReportParameter manifests = new ReportParameter("ManifestNumbers","");
                    ReportParameter trailers = new ReportParameter("TrailerNumbers","");
                    report.SetParameters(new ReportParameter[] { client,div,vendor,pudate,punum,clientName,vendorName,manifests,trailers });

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
                    Response.AddHeader("Content-Disposition","inline; filename=ClientPickup.xls");
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
    private GridViewRow[] SelectedRows {
        get {
            GridViewRow[] rows=new GridViewRow[] { };
            int i=0;
            foreach(GridViewRow row in this.grdPickups.Rows) {
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if(isChecked) i++;
            }
            if(i > 0) {
                rows = new GridViewRow[i];
                int j=0;
                foreach(GridViewRow row in this.grdPickups.Rows) {
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if(isChecked) rows[j++] = row;
                }
            }
            return rows;
        }
    }
}
