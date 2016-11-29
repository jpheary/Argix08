using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Argix;
using Microsoft.Reporting.WebForms;

public partial class OnTimeMaster:System.Web.UI.MasterPage {
    //Members

    //Interface
    public string ReportTitle { get { return Master.ReportTitle; } set { Master.ReportTitle = value; } }
    public bool NavigatorVisible { get { return Master.NavigatorVisible; } set { Master.NavigatorVisible = value; } }
    public ReportViewer Viewer { get { return Master.Viewer; } }
    public bool Validated { set { if(value) OnValidateForm(null,EventArgs.Empty); else Master.Validated = false; } }
    public string Status { set { Master.Status = value; } }
    public void ReportError(Exception ex) { Master.ReportError(ex); }
    public Stream GetReportDefinition(string report) { return Master.GetReportDefinition(report); }
    public Stream CreateExportRdl(DataSet ds,string dataSetName,string dataSourceName) { return Master.CreateExportRdl(ds,dataSetName,dataSourceName); }

    public string ClientNumber { get { return this.dgdClientDivision.ClientNumber; } }
    public string ClientName { get { return this.dgdClientDivision.ClientName; } }
    public string DivisionNumber { get { return this.dgdClientDivision.DivisionNumber; } }
    public DateTime FromDate { get { return this.ddpPickups.FromDate; } }
    public DateTime ToDate { get { return this.ddpPickups.ToDate; } }
    public string AgentNumber { get { return this.cboAgent.SelectedValue; } }
    public bool AgentsEnabled { get { return this.cboAgent.Enabled; } set { this.cboAgent.Enabled = value; } }

    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.dgdClientDivision.ClientSelectedIndex = -1;
            OnClientSelected(null,EventArgs.Empty);
            this.cboAgent.DataBind();
            if(this.cboAgent.Items.Count > 0) this.cboAgent.SelectedIndex = 0;
            OnAgentChanged(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for change in from/to date
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAgentChanged(object sender,EventArgs e) {
        //Event handler for change in agent value
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        this.dgdClientDivision.DivisionSelectedIndex = -1;
        OnDivisionSelected(null,EventArgs.Empty);
    }
    protected void OnDivisionSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.dgdClientDivision.DivisionNumber.Length > 0);
    }
}