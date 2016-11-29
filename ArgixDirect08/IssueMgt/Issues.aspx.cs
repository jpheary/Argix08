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

public partial class Issues:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            IssueDS issues = new CustomerProxy().GetIssues();
            for(int i = 0;i < issues.IssueTable.Rows.Count;i++) {
                IssueDS.IssueTableRow issue = issues.IssueTable[i];
                TableCell zone = new TableCell(); zone.BorderStyle=BorderStyle.Solid; zone.BorderWidth=1; zone.BorderColor=System.Drawing.Color.LightGray; zone.Text = !issue.IsZoneNull()?issue.Zone:"";
                TableCell store = new TableCell(); store.BorderStyle=BorderStyle.Solid; store.BorderWidth=1; store.BorderColor=System.Drawing.Color.LightGray; store.Text = !issue.IsStoreNumberNull()?issue.StoreNumber.ToString():"";
                TableCell agent = new TableCell(); agent.BorderStyle=BorderStyle.Solid; agent.BorderWidth=1; agent.BorderColor=System.Drawing.Color.LightGray; agent.Text = !issue.IsAgentNumberNull()?issue.AgentNumber:"";
                TableCell company = new TableCell(); company.BorderStyle=BorderStyle.Solid; company.BorderWidth=1; company.BorderColor=System.Drawing.Color.LightGray; company.Text = !issue.IsCompanyNameNull()?issue.CompanyName:"";
                TableCell type = new TableCell(); type.BorderStyle=BorderStyle.Solid; type.BorderWidth=1; type.BorderColor=System.Drawing.Color.LightGray; type.Text = issue.Type;
                TableCell action = new TableCell(); action.BorderStyle=BorderStyle.Solid; action.BorderWidth=1; action.BorderColor=System.Drawing.Color.LightGray; action.Text = issue.LastActionDescription;
                TableCell received = new TableCell(); received.BorderStyle=BorderStyle.Solid; received.BorderWidth=1; received.BorderColor=System.Drawing.Color.LightGray; received.Text = issue.LastActionCreated.ToString("MM/dd/yyyy");
                TableCell subject = new TableCell(); subject.BorderStyle=BorderStyle.Solid; subject.BorderWidth=1; subject.BorderColor=System.Drawing.Color.LightGray; subject.Text = !issue.IsSubjectNull()?issue.Subject:"";
                TableCell contact = new TableCell(); contact.BorderStyle=BorderStyle.Solid; contact.BorderWidth=1; contact.BorderColor=System.Drawing.Color.LightGray; contact.Text = !issue.IsContactNameNull()?issue.ContactName:"";
                TableCell lastuser = new TableCell(); lastuser.BorderStyle=BorderStyle.Solid; lastuser.BorderWidth=1; lastuser.BorderColor=System.Drawing.Color.LightGray; lastuser.Text = issue.LastActionUserID;
                TableCell coordinator = new TableCell(); coordinator.BorderStyle=BorderStyle.Solid; coordinator.BorderWidth=1; coordinator.BorderColor=System.Drawing.Color.LightGray; coordinator.Text = issue.Coordinator;

                TableRow tr = new TableRow();
                tr.Cells.AddRange(new TableCell[] { zone,store,agent,company,type,action,received,subject,contact,lastuser,coordinator });
                this.tblPage.Rows.Add(tr);
            }
        }
    }
}
