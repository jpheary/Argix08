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

public partial class IssueDetail:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            long issueID = Convert.ToInt64(Request.QueryString["issueID"]);

            IssueMgtServiceClient client = new IssueMgtServiceClient();
            Issue issue = client.GetIssue(issueID);
            this.lblType.Text = issue.Type;
            this.lblSubject.Text = issue.Subject;
            this.lblContact.Text = issue.ContactName;
            this.lblCompany.Text = issue.CompanyName;
            this.lblStore.Text = issue.StoreNumber.ToString();
            this.lblAgent.Text = issue.AgentNumber;
            this.lblZone.Text = issue.Zone;

            CustomerProxy cp = new CustomerProxy();
            Argix.Customers.Action[] actions = cp.GetIssueActions(issueID);
            for(int i = 0;i < actions.Length;i++) {
                string cell = actions[i].Created.ToString("f") + "     " + actions[i].UserID + ", " + actions[i].TypeName;
                cell += "<br /><br />";
                cell += actions[i].Comment;
                cell += "<br />";
                cell += "<hr />";
                cell += "<br />";

                TableCell tc = new TableCell();
                tc.Text = cell;
                TableRow tr = new TableRow();
                tr.Cells.Add(tc);
                this.tblPage.Rows.Add(tr);
            }
        }
    }
}
