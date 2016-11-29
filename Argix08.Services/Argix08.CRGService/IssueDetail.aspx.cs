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

public partial class IssueDetail:System.Web.UI.Page {
    //Members
    private long mIssueID=0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
            ViewState["IssueID"] = this.mIssueID;

            this.lblTitle.Text = "Issue Detail (#" + this.mIssueID.ToString() + ")";
        }
        else {
            this.mIssueID = (long)ViewState["IssueID"];
        }
    }
    protected void OnButtonClick(object sender,EventArgs e) {
        //Event handler for command button clicked
        try {
            Button btn = (Button)sender;
            switch(btn.ID) {
                case "btnClose":
                    break;
            }
        }
        catch(Exception ex) { ; }
    }
}
