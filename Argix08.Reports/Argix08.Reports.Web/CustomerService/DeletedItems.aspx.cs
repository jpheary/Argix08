using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class DeletedItems :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Deleted Items";
    private const string REPORT_SRC = "/Customer Service/Deleted Items";
    private const string REPORT_DS = "DeletedItemsDS";
    private const string USP_REPORT = "uspRptDeletedSortedItemsGetListForSortDate",TBL_REPORT = "NewTable";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            this.odsTerminals.DataBind();
            if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
            OnTerminalSelected(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTerminalSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.cboTerminal.SelectedValue.Length > 0);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _date = this.ddpPickups.ToDate.ToString("yyyy-MM-dd");
        string _terminal = this.cboTerminal.SelectedValue;
        string _terminalName = this.cboTerminal.SelectedItem.Text;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _terminal,_date, });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter terminalID = new ReportParameter("TerminalID",_terminal);
                    ReportParameter terminal = new ReportParameter("TerminalName",_terminalName);
                    ReportParameter date = new ReportParameter("SortDate",_date);
                    report.SetParameters(new ReportParameter[] { terminalID,date,terminal });

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
                    Response.AddHeader("Content-Disposition","inline; filename=DamagedCartons.xls");
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