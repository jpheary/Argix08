using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argix.Customers;

public partial class IssueMgt:System.Web.UI.Page {
    //Members
    private DateTime mLastNewIssueTime=DateTime.Now;
    private System.Collections.Hashtable mOldItems=null;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            ViewState["LastNewIssueTime"] = this.mLastNewIssueTime;
            this.mOldItems = new System.Collections.Hashtable();
            ViewState["OldItems"] = this.mOldItems;

            //System.Diagnostics.Debug.WriteLine("ScreenPixelsWidth=" + Page.Request.Browser.ScreenPixelsWidth.ToString());
            this.pnlIssues.Width = this.tblIssuesToolbar.Width = this.lblTitle.Width = 980;
            this.pnlIssues.Height = 192;
            this.tmrRefresh.Enabled = Convert.ToBoolean(Session["AutoRefreshOn"]);

            this.cboView.Items.Add("Current Issues");
            if(this.cboView.Items.Count > 0) this.cboView.SelectedIndex = 0;
            OnViewChanged(null,EventArgs.Empty);
            this.grdIssues.Sort("LastActionCreated",SortDirection.Descending);

            //Select prior issue (if applicable)
            string issueID = Request.QueryString["issueID"];
            if(issueID != null && issueID.Trim().Length > 0) {
                for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                    if(this.grdIssues.Rows[i].Cells[1].Text == issueID) {
                        this.grdIssues.SelectedIndex = i;
                        OnIssueSelected(this.grdIssues,EventArgs.Empty);
                        break;
                    }
                }
            }
        }
        else {
            this.mLastNewIssueTime = (DateTime)ViewState["LastNewIssueTime"];
            this.mOldItems = (System.Collections.Hashtable)ViewState["OldItems"];
        }
    }
    protected void OnIssueTimerTick(object sender,EventArgs e) {
        //Event handler for issue timer tick event
        OnIssueToolbarClick(this.tmrRefresh,new CommandEventArgs("Refresh",null));
    }
    protected void OnViewChanged(object sender,EventArgs e) {
        //Event handler for change in combobox view selection
        switch(this.cboView.Text) {
            case "Search Results":
                this.tmrRefresh.Enabled = false;
                this.grdIssues.DataSourceID = "odsSearchIssues";
                this.grdIssues.DataBind();
                break;
            default:
                this.tmrRefresh.Enabled = true;
                this.grdIssues.DataSourceID = "odsIssues";
                this.grdIssues.DataBind();
                break;
        }
        if(this.grdIssues.Rows.Count > 0) this.grdIssues.SelectedIndex = 0;
        OnIssueSelected(null,EventArgs.Empty);
    }
    protected void OnIssueToolbarClick(object sender,CommandEventArgs e) {
        //Event handler for issue toolbar refresh button
        switch(e.CommandName) {
            case "New":
                Response.Redirect("~/IssueNew.aspx");
                break;
            case "Refresh":
                switch(this.cboView.Text) {
                    case "Search Results":
                        break;
                    default:
                        string key = this.grdIssues.SelectedDataKey[0].ToString();
                        this.grdIssues.DataBind();
                        for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                            if(this.grdIssues.Rows[i].Cells[1].Text == key) {
                                this.grdIssues.SelectedIndex = i;
                                break;
                            }
                        }
                        //OnIssueSelected(null,EventArgs.Empty);
                        break;
                }
                break;
        }
    }
    protected void OnIssueRowDataBound(Object sender,GridViewRowEventArgs e) {
        //Event handler for issue row data bound
        //Bold rows of new issues/actions
        int id=0;
        try { id = Convert.ToInt32(e.Row.Cells[1].Text); } catch { }
        if(id > 0) {
            DateTime dt1 = Convert.ToDateTime(e.Row.Cells[8].Text);
            if(!this.mOldItems.ContainsKey(id)) {
                //Not viewed or startup (i.e. collection is empty)
                if(dt1.CompareTo(this.mLastNewIssueTime) > 0)
                    e.Row.Font.Bold = true;
            }
            else {
                DateTime dt2 = Convert.ToDateTime(this.mOldItems[id]);
                if(dt1.CompareTo(dt2) > 0) {
                    //LastActionCreated is different then last time viewed
                    e.Row.Font.Bold = true;
                }
            }
        }
    }

    protected void OnIssueSelected(object sender,EventArgs e) {
        //Event handler for change in selected issue
        this.lblTitle.Text = "";
        if(this.grdIssues.SelectedRow != null) {
            //Unbold viewed issues/actions
            this.grdIssues.SelectedRow.Font.Bold = false;
            //int id = Convert.ToInt32(this.grdIssues.SelectedRow.Cells[1].Text);
            int id = Convert.ToInt32(this.grdIssues.SelectedDataKey[0]);
            DateTime dt1 = Convert.ToDateTime(this.grdIssues.SelectedRow.Cells[8].Text);
            if(this.mOldItems.ContainsKey(id))
                this.mOldItems[id] = dt1;
            else
                this.mOldItems.Add(id,dt1);

            Issue issue = new IssueMgtServiceClient().GetIssue(id);
            if(issue != null) {
                this.lblTitle.Text = issue.Type.Trim();
                if(issue.CompanyName.Trim() != "All") {
                    this.lblTitle.Text += ": " + issue.CompanyName.Trim();
                    if(issue.StoreNumber > 0)
                        this.lblTitle.Text += " #" + issue.StoreNumber.ToString();
                }
                else {
                    if(issue.AgentNumber.Trim() != "All")
                        this.lblTitle.Text += ": Agent#" + issue.AgentNumber.Trim();
                    else
                        this.lblTitle.Text += ": All Agents";
                }
                if(issue.Subject.Trim().Length > 0)
                    this.lblTitle.Text += " - " + issue.Subject.Trim();
            }
            this.lsvActions.SelectedIndex = 0;
            OnActionSelected(null,EventArgs.Empty);
        }
    }
    protected void OnActionToolbarClick(object sender,CommandEventArgs e) {
        //Event handler for action toolbar refresh button
        switch(e.CommandName) {
            case "New":
                Response.Redirect("~/ActionNew.aspx?issueID=" + this.grdIssues.SelectedDataKey[0].ToString());
                break;
            case "Refresh":
                this.lsvActions.DataBind();
                //OnActionSelected(null,EventArgs.Empty);
                break;
            case "NewAttachment":
                Response.Redirect("~/AttachmentNew.aspx?issueID=" + this.grdIssues.SelectedDataKey[0].ToString() + "&actionID=" + this.lsvActions.SelectedValue.ToString());
                break;
        }
    }
    protected void OnActionSelected(object sender,EventArgs e) {
        //Event handler for change in selected action
    }
}
