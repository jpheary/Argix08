using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewIssues:System.Web.UI.Page {
    //Members
    private long mIssueID = 0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
            ViewState.Add("IssueID",this.mIssueID);
            this.Master.IssuesButtonFontColor = System.Drawing.Color.White;
            this.cboTerminal.DataBind();
            if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
            OnTerminalChanged(null,EventArgs.Empty);
        }
        else {
            this.mIssueID = (long)ViewState["IssueID"];
        }
        if(this.mIssueID > 0) this.mvPage.SetActiveView(this.vwIssues);
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for change in selected terminal
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        this.lsvIssues.DataBind();
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        switch(e.CommandName) {
            case "Issues":
                this.mvPage.SetActiveView(this.vwIssues);
                break;
            case "Issue":
                Argix.Customers.Issue issue = new Argix.Customers.CustomerProxy().GetIssue(long.Parse(e.CommandArgument.ToString()));
                if(issue != null) {
                    this.lblType.Text = issue.Type.Trim();
                    this.lblCompany.Text = "";
                    if(issue.CompanyName.Trim() != "All") {
                        this.lblCompany.Text += issue.CompanyName.Trim();
                        if(issue.StoreNumber > 0)
                            this.lblCompany.Text += " #" + issue.StoreNumber.ToString();
                    }
                    else {
                        if(issue.AgentNumber.Trim() != "All")
                            this.lblCompany.Text += ": Agent#" + issue.AgentNumber.Trim();
                        else
                            this.lblCompany.Text += ": All Agents";
                    }
                    this.lblSubject.Text = issue.Subject.Trim();
                }
                this.odsActions.SelectParameters["issueID"].DefaultValue = e.CommandArgument.ToString();
                this.odsActions.DataBind();
                this.mvPage.SetActiveView(this.vwIssue);
                break;
        }
    }
    public string GetDateInfo(object lastActionCreated) {
        //
        string dateInfo="";
        DateTime created = (DateTime)lastActionCreated;
        bool useYesterday = DateTime.Today.DayOfWeek != DayOfWeek.Monday;
        if(created.CompareTo(DateTime.Today) >= 0) {
            //Today
            dateInfo = "Today, " + created.ToString("ddd HH:mm");
        }
        else if(useYesterday && created.CompareTo(DateTime.Today.AddDays(-1)) >= 0) {
            //Yesterday
            dateInfo = "Yesterday, " + created.ToString("ddd HH:mm");
        }
        else if(created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek)) >= 0) {
            //This Week
            dateInfo = created.ToString("ddd HH:mm");
        }
        else if(created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek - 7)) >= 0) {
            //Last Week
            dateInfo = created.ToString("ddd MM/dd HH:mm");
        }
        else {
            //Other
            dateInfo = created.ToString("ddd MM/dd/yyyy HH:mm");
        }
        return dateInfo;
    }
    public string GetCompanyInfo(object companyName,object storeNumber,object agentNumber) {
        string companyInfo="";
        if(companyName.ToString().Trim() != "All") {
            companyInfo += companyName.ToString().Trim();
            if(storeNumber != DBNull.Value && Convert.ToInt32(storeNumber) > 0)
                companyInfo += " #" + storeNumber.ToString();
        }
        else {
            if(agentNumber.ToString().Trim() != "All")
                companyInfo += ": Agent#" + agentNumber.ToString().Trim();
            else
                companyInfo += ": All Agents";
        }
        return companyInfo;
    }
}
