using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewIssue:System.Web.UI.Page {
    //Members
    private long mIssueID = 0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
            ViewState.Add("IssueID", this.mIssueID);
            this.Master.IssuesButtonFontColor = System.Drawing.Color.White;
        }
        else {
            this.mIssueID = (long)ViewState["IssueID"];
        }
        
        Argix.Customers.Issue issue = new Argix.Customers.CustomerProxy().GetIssue(this.mIssueID);
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
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        switch(e.CommandName) {
            case "Back":
                Response.Redirect("ViewIssues.aspx?issueID=" + this.mIssueID);
                break;
        }
    }
}
