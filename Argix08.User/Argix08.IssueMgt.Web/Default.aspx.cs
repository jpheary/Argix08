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
using System.Xml;
using System.Xml.Linq;
using Argix.Customers;

public partial class Default:System.Web.UI.Page {
    //Members
    private const int CELL_ID=1, CELL_LASTACTIONCREATED=8;
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            ViewState.Add("SearchType", "Standard");
            ViewState.Add("LastNewIssueTime",Session["LastNewIssueTime"]);
            ViewState.Add("OldItems",new Hashtable());
            this.tblSearch.Visible = !(bool)Session["SearchTabVisible"];

            if (Session["AgentNumber"] == null) Session["AgentNumber"] = Request.QueryString["agentNumber"];
            this.cboTerminal.DataBind();
            if (this.cboTerminal.Items.Count > 0) {
                string agentNumber = Session["AgentNumber"] != null ? Session["AgentNumber"].ToString() : "";
                if (agentNumber != null && agentNumber.Length > 0)
                    this.cboTerminal.SelectedValue = agentNumber;
                else
                    this.cboTerminal.SelectedIndex = 0;
            }
            OnTerminalChanged(null, EventArgs.Empty);
            OnSearchTabClicked(null, null);

            //Select prior issue (if applicable)
            string issueID = Request.QueryString["issueID"];
            if(issueID != null && issueID.Trim().Length > 0) {
                for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                    if(this.grdIssues.Rows[i].Cells[CELL_ID].Text == issueID) {
                        this.grdIssues.SelectedIndex = i;
                        break;
                    }
                }
            }
            OnIssueSelected(this.grdIssues,null);
        }
    }
    protected void OnIssueTimerTick(object sender,EventArgs e) {
        //Event handler for issue timer tick event
        OnIssueToolbarClick(this.tmrRefresh,new CommandEventArgs("Refresh",null));
    }
    protected void OnSearchTabClicked(object sender,ImageClickEventArgs e) {
        if(!this.tblSearch.Visible) {
            this.tcSearchTab.Style["border-right-style"] = "none";
            this.tblSearch.Visible = true;
        }
        else {
            this.tcSearchTab.Style["border-right-style"] = "solid";
            this.tblSearch.Visible = false;
        }
        Session["SearchTabVisible"] = this.tblSearch.Visible;
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //
        this.upnlSearch.Update();
        OnViewChanged(null, EventArgs.Empty);
        OnSearch(null, new CommandEventArgs("Reset", ""));
    }
    protected void OnViewChanged(object sender,EventArgs e) {
        //Event handler for change in combobox view selection
        switch(this.cboView.SelectedValue) {
            case "Current": this.tmrRefresh.Enabled = Convert.ToBoolean(Session["AutoRefreshOn"]); break;
            case "Search":  this.tmrRefresh.Enabled = false; break;
        }
        OnIssueToolbarClick(null,new CommandEventArgs("Refresh","ViewChange"));
        this.grdIssues.Sort("LastActionCreated", SortDirection.Descending);
    }
    protected void OnIssueToolbarClick(object sender,CommandEventArgs e) {
        //Event handler for issue toolbar refresh button
        switch(e.CommandName) {
            case "New":     Response.Redirect("~/IssueNew.aspx"); break;
            case "Refresh":
                string key = this.grdIssues.SelectedDataKey != null ? this.grdIssues.SelectedDataKey[0].ToString() : "";
                switch(this.cboView.SelectedValue) {
                    case "Current":
                        this.grdIssues.DataSourceID = "odsIssues";
                        break;
                    case "Search":
                        if(ViewState["SearchType"].ToString() == "Standard")
                            this.grdIssues.DataSourceID = "odsSearch";
                        else
                            this.grdIssues.DataSourceID = "odsSearchAdv";
                        break;
                }
                this.grdIssues.DataBind();
                for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                    if(key.Length == 0 || this.grdIssues.Rows[i].Cells[CELL_ID].Text == key) {
                        this.grdIssues.SelectedIndex = i;
                        break;
                    }
                }
                OnIssueSelected(this.grdIssues,null);
                break;
            case "Print":   break;
            case "Search":
                ViewState["SearchType"] = "Standard";
                this.cboView.SelectedValue = "Search"; OnViewChanged(null,EventArgs.Empty);
                break;
        }
    }
    protected void OnIssueRowDataBound(object sender, GridViewRowEventArgs e) {
        //Event handler for issue row data bound
        //Bold rows of new issues/actions
        int id=0;
        try { id = Convert.ToInt32(e.Row.Cells[CELL_ID].Text); } catch { }
        if(id > 0) {
            //Init
            DateTime lastNewIssueTime = (DateTime)ViewState["LastNewIssueTime"];
            Hashtable oldItems = (Hashtable)ViewState["OldItems"];
            DateTime dt1;
            if (DateTime.TryParse(e.Row.Cells[CELL_LASTACTIONCREATED].Text, out dt1)) {
                if(!oldItems.ContainsKey(id)) {
                    e.Row.Font.Bold = dt1.CompareTo(lastNewIssueTime) > 0;      //Not viewed or startup (i.e. collection is empty)
                } 
                else {
                    DateTime dt2 = Convert.ToDateTime(oldItems[id]);
                    if(dt1.CompareTo(dt2) > 0) {
                        e.Row.Font.Bold = dt1.CompareTo(dt2) > 0;               //LastActionCreated is different then last time viewed
                    }
                }
            }
        }
    }
    protected void OnIssueSelected(object sender,EventArgs e) {
        //Event handler for change in selected issue
        this.lblSubject.Text = "";
        if (this.grdIssues.Rows.Count > 0 && this.grdIssues.SelectedRow != null) {
            //Unbold viewed issues/actions
            this.grdIssues.SelectedRow.Font.Bold = false;
            int id = Convert.ToInt32(this.grdIssues.SelectedDataKey[0]);
            DateTime dt1;
            if (DateTime.TryParse(this.grdIssues.SelectedRow.Cells[CELL_LASTACTIONCREATED].Text, out dt1)) {
                Hashtable oldItems = (Hashtable)ViewState["OldItems"];
                if(oldItems.ContainsKey(id)) 
                    oldItems[id] = dt1;
                else 
                    oldItems.Add(id,dt1);
                ViewState["OldItems"] = oldItems;
            }

            //Toolbar print buttons
            this.btnIssuesPrint.OnClientClick = "javascript:window.open('Issues.aspx','_blank','width=700px,height=440px,toolbar=yes,scrollbars=yes,resizable=yes');return false;";
            this.btnActionsPrint.OnClientClick = "javascript:window.open('IssueDetail.aspx?issueID=" + this.grdIssues.SelectedDataKey[0].ToString() + "','_blank','width=700px,height=440px,toolbar=yes,scrollbars=yes,resizable=yes');return false;";

            //Update subject
            Issue issue = new IssueMgtServiceClient().GetIssue(id);
            if(issue != null) {
                this.lblSubject.Text = issue.Type.Trim();
                if(issue.CompanyName.Trim() != "All") {
                    this.lblSubject.Text += ": " + issue.CompanyName.Trim();
                    if(issue.StoreNumber > 0)
                        this.lblSubject.Text += " #" + issue.StoreNumber.ToString();
                }
                else {
                    if(issue.AgentNumber.Trim() != "All")
                        this.lblSubject.Text += ": Agent#" + issue.AgentNumber.Trim();
                    else
                        this.lblSubject.Text += ": All Agents";
                }
                if(issue.Subject.Trim().Length > 0)
                    this.lblSubject.Text += " - " + issue.Subject.Trim();
            }
        }
        this.lsvActions.DataBind();
        if(this.lsvActions.Items.Count > 0) this.lsvActions.SelectedIndex = 0;
        OnActionSelected(null,EventArgs.Empty);
        this.lsvAttachments.DataBind();
        this.upnlIssues.Update();
    }
    protected void OnIssuesSorting(object sender, GridViewSortEventArgs e) { }
    protected void OnIssuesSorted(object sender, EventArgs e) {
        this.upnlIssues.Update();
    }
    protected void OnSearch(object sender,CommandEventArgs e) {
        //Event handler for search buttons
        switch(e.CommandName) {
            case "Reset":
                this.txtZone.Text=this.txtStore.Text="";
                this.txtCompany.Text=this.txtType.Text=this.txtAction.Text=this.txtReceived.Text="";
                this.txtSubject.Text=this.txtContact.Text=this.txtOriginator.Text=this.txtCoordinator.Text="";
                this.txtAgent.Text = this.cboTerminal.SelectedValue.Length > 0 ? this.cboTerminal.SelectedValue : "";
                this.txtAgent.Enabled = this.cboTerminal.SelectedValue.Length == 0;
                break;
            case "Search":
                ViewState["SearchType"] = "Advanced";
                this.cboView.SelectedValue = "Search"; OnViewChanged(null,EventArgs.Empty);
                break;
        }
    }
    protected void OnActionToolbarClick(object sender,CommandEventArgs e) {
        //Event handler for action toolbar refresh button
        switch(e.CommandName) {
            case "New": Response.Redirect("~/ActionNew.aspx?issueID=" + this.grdIssues.SelectedDataKey[0].ToString()); break;
            case "Refresh":         this.lsvActions.DataBind(); break;
            case "NewAttachment": Response.Redirect("~/AttachmentNew.aspx?issueID=" + this.grdIssues.SelectedDataKey[0].ToString() + "&actionID=" + this.lsvActions.SelectedValue.ToString()); break;
            case "Print":           break;
        }
    }
    protected void OnActionSelected(object sender,EventArgs e) {
        //Event handler for change in selected action
    }
}
