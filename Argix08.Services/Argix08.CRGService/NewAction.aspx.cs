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
using Argix.CustomerSvc;

public partial class NewAction:System.Web.UI.Page {
    //Members
    private long mIssueID=0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        //Get query params
        this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
        if(!Page.IsPostBack) {
            this.lblTitle.Text = "New Action (issue# " + this.mIssueID.ToString() + ")";
            if(this.cboActionType.Items.Count > 0) this.cboActionType.SelectedIndex = 0;
            OnTypeChanged(this.cboActionType,EventArgs.Empty);
            this.btnOk.Enabled = false;
            //this.btnOk.Attributes.Add("disabled","true");
        }
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        try {
            OnCommentsChanged(this.txtComments,EventArgs.Empty);
        }
        catch(Exception ex) { ; }
    }
    protected void OnCommentsChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        try {
            this.btnOk.Enabled = this.txtComments.Text.Length > 0;
        }
        catch(Exception ex) { ; }
    }
    protected void OnButtonClick(object sender,EventArgs e) {
        //Event handler for command button clicked
        try {
            Button btn = (Button)sender;
            switch(btn.ID) {
                case "btnCancel":
                    Response.Redirect("~/IssueMgt.aspx?issueID=" + this.mIssueID.ToString());
                    break;
                case "btnOk": 
                    CRGService crgService  = new CRGService();
                    bool ret = crgService.CreateIssueAction(Convert.ToByte(this.cboActionType.SelectedValue), this.mIssueID, Environment.UserName, this.txtComments.Text);
                    Response.Redirect("~/IssueMgt.aspx?issueID=" + this.mIssueID.ToString());
                    break;
            }
        }
        catch(Exception ex) { ; }
    }
}
