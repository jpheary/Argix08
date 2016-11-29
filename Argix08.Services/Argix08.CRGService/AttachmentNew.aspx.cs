using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argix.Customers;

public partial class AttachmentNew:System.Web.UI.Page {
    //Members
    private long mIssueID=0;
    private long mActionID=0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
            ViewState["IssueID"] = this.mIssueID;
            this.mActionID = Convert.ToInt64(Request.QueryString["actionID"]);
            ViewState["ActionID"] = this.mActionID;

            this.lblTitle.Text = "New Attachment (issue# " + this.mIssueID.ToString() + ")";
            this.btnOk.Enabled = true;
        }
        else {
            this.mIssueID = (long)ViewState["IssueID"];
            this.mActionID = (long)ViewState["ActionID"];
        }
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
                    if(this.fuAttachment.HasFile) {
                        IssueMgtServiceClient crgService  = new IssueMgtServiceClient();
                        bool ret = crgService.CreateIssueAttachment(this.fuAttachment.FileName,this.fuAttachment.FileBytes,this.mActionID);
                        Response.Redirect("~/IssueMgt.aspx?issueID=" + this.mIssueID.ToString());
                    } 
                    break;
            }
        }
        catch(ApplicationException ex) { ; }
        catch(Exception ex) { ; }
    }
}
