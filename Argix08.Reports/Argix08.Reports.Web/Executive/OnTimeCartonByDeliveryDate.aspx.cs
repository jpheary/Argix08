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

public partial class OnTimeCartonByDeliveryDate :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "On Time Carton By Delivery Date";
    private const string REPORT_SRC = "/Executive/On Time Carton By Delivery Date";
    private const string REPORT_DS = "OnTimeCartonByDeliveryDateDS";
    private const string USP_REPORT = "uspRptOnTimeCartonByDelivery",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = REPORT_NAME + " Report";
            this.dgdClientAgent.ClientSelectedIndex = -1;
            this.dgdClientAgent.AgentSort("AgentNumber",SortDirection.Ascending);
            OnAllAgentsChecked(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void Page_LoadComplete(object sender,EventArgs e) {
        if(!Page.IsPostBack) {
            OnAllAgentsChecked(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for change in from/to date
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAllAgentsChecked(object sender,EventArgs e) {
        //Event handler for change in from date
        this.dgdClientAgent.AgentSelectedIndex = this.chkAllAgents.Checked ? -1 : 0;
        this.dgdClientAgent.AgentsEnabled = !this.chkAllAgents.Checked;
        OnAgentSelected(null,EventArgs.Empty);
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAgentSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = this.dgdClientAgent.ClientSelectedRow != null && (this.chkAllAgents.Checked || this.dgdClientAgent.AgentSelectedRow != null);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        //Change view to Viewer and reset to clear existing data
        Master.Viewer.Reset();

        //Get parameters for the query
        string _from = this.ddpDelivery.FromDate.ToString("yyyy-MM-dd");
        string _to = this.ddpDelivery.ToDate.ToString("yyyy-MM-dd");
        string _client = this.dgdClientAgent.ClientNumber;
        string _agent = this.chkAllAgents.Checked ? null : this.dgdClientAgent.AgentNumber;

        //Initialize control values
        LocalReport report = Master.Viewer.LocalReport;
        report.DisplayName = REPORT_NAME;
        report.EnableExternalImages = true;
        EnterpriseGateway enterprise = new EnterpriseGateway();
        DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _from,_to,_client,_agent });
        if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                    //Set the report parameters for the report
                    ReportParameter from = new ReportParameter("FromDate",_from);
                    ReportParameter to = new ReportParameter("ToDate",_to);
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter agent = new ReportParameter("AgentNumber",_agent);
                    report.SetParameters(new ReportParameter[] { from,to,client,agent });

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
                    Response.AddHeader("Content-Disposition","inline; filename=OnTimeCartonByDeliveryDate.xls");
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
