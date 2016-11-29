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

public partial class EmployeesEditor:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.Title = "Argix Direct Employees";
            if(this.cboType.Items.Count > 0) this.cboType.SelectedIndex = 0;
            OnTypeChanged(this.cboType,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnEmployeeSelected(object sender,EventArgs e) {
        //Event handler for change in selected employee
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
    }
    protected void OnAdd(object sender,EventArgs e) {
        //Event handler for export button clicked
        Response.Redirect("AddEmployee.aspx");
    }
}
