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

public partial class ActionNew:System.Web.UI.Page {
    //Members
    private long mIssueID=0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
            ViewState["IssueID"] = this.mIssueID;

            this.lblTitle.Text = "New Action (issue# " + this.mIssueID.ToString() + ")";
            if(this.cboActionType.Items.Count > 0) this.cboActionType.SelectedIndex = 0;
            OnTypeChanged(this.cboActionType,EventArgs.Empty);
            this.btnOk.Enabled = false;
        }
        else {
            this.mIssueID = (long)ViewState["IssueID"];
        }
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        OnCommentsChanged(this.txtComments,EventArgs.Empty);
    }
    protected void OnCommentsChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        this.btnOk.Enabled = this.txtComments.Text.Length > 0;
    }
    protected void OnButtonClick(object sender,EventArgs e) {
        //Event handler for command button clicked
        Button btn = (Button)sender;
        switch(btn.ID) {
            case "btnCancel":
                Response.Redirect("~/IssueMgt.aspx?issueID=" + this.mIssueID.ToString());
                break;
            case "btnOk": 
                IssueMgtServiceClient crgService  = new IssueMgtServiceClient();
                bool ret = crgService.CreateIssueAction(Convert.ToByte(this.cboActionType.SelectedValue), this.mIssueID, Environment.UserName, this.txtComments.Text);
                Response.Redirect("~/IssueMgt.aspx?issueID=" + this.mIssueID.ToString());
                break;
        }
    }
}
