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

public partial class ASNTracking :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "ASN Tracking";
    private const string REPORT_SRC = "/Customer Service/ASN Tracking";
    private const string REPORT_DS = "ASNTrackingDS";
    private const string USP_REPORT = "uspRptCartonsInfoForPickup",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            OnAllVendorsChecked(this.dgdClientVendor,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for chnage in from/to date
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
        this.dgdClientVendor.VendorsEnabled = !this.chkAllVendors.Checked;
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
        string _clientName = this.dgdClientVendor.ClientName;
        string _pickupID = "",_termCode = "";

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = new DataSet(REPORT_DS);
        foreach(GridViewRow row in SelectedRows) {
            DataKey dataKey = (DataKey)this.grdPickups.DataKeys[row.RowIndex];
            _pickupID += (_pickupID.Length > 0 ? ", " : "") + dataKey["PickupID"].ToString();
            _termCode = dataKey["TerminalCode"].ToString();
            DataSet _ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { dataKey["TerminalCode"].ToString(),dataKey["PickupID"].ToString() });
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
                    ReportParameter clientName = new ReportParameter("ClientName",_clientName);
                    ReportParameter puID = new ReportParameter("PickupID",_pickupID);
                    ReportParameter termCode = new ReportParameter("TerminalCode",_termCode);
                    report.SetParameters(new ReportParameter[] { termCode,puID,clientName });

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
                    Response.AddHeader("Content-Disposition","inline; filename=ASNTracking.xls");
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
