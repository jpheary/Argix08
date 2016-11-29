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

public partial class IssueMobile:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            long issueID = Convert.ToInt64(Request.QueryString["issueID"]);

            IssueMgtServiceClient client = new IssueMgtServiceClient();
            Issue issue = client.GetIssue(issueID);

            Response.Write("Issue Type:&nbsp;" + issue.Type + "<br />");
            Response.Write("Subject:&nbsp;" + issue.Subject + "<br />");
            Response.Write("Contact:&nbsp;" + issue.ContactName + "<br /><br />");
            Response.Write("Company:&nbsp;" + issue.CompanyName + "<br />");
            Response.Write("Store:&nbsp;" + issue.StoreNumber.ToString() + "<br />");
            Response.Write("Agent:&nbsp;" + issue.AgentNumber + "<br />");
            Response.Write("Zone:&nbsp;" + issue.Zone);

            CustomerProxy cp = new CustomerProxy();
            Argix.Customers.Action[] actions = cp.GetIssueActions(issueID);
            for(int i = 0;i < actions.Length;i++) {
                string cell = "<br /><br /><hr />";
                cell += actions[i].Created.ToString("g") + ", " + actions[i].UserID;
                cell += "<br />";
                cell += actions[i].TypeName;
                cell += "<br /><br />";
                cell += actions[i].Comment;
                Response.Write(cell);
            }
        }
    }
}
