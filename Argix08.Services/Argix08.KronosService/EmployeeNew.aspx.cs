using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeNew:System.Web.UI.Page {
    //Members
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.Title = "New Employee";
        }
    }
    protected void OnCancel(object sender,EventArgs e) {
        //Event handler for export button clicked
        Response.Redirect("HelpersEditor.aspx");
    }
    protected void OnOk(object sender,EventArgs e) {
        //Event handler for export button clicked
        Argix.Employee employee = new Argix.Employee();
        employee.LastName =  this.txtLastName.Text;
        employee.FirstName = this.txtFirstName.Text;
        employee.Middle = this.txtMiddle.Text;
        employee.Suffix = this.txtSuffix.Text;
        employee.Location = this.cdoLocation.SelectedValue;
        Argix.Kronos kronos = new Argix.Kronos();
        bool res = kronos.AddEmployee("Helpers",employee);
        
        Response.Redirect("HelpersEditor.aspx");
    }
    public void ShowMsgBox(string message) {
        //
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language=javascript>");
        script.Append(" alert('" + message + "');");
        script.Append("</script>");
        Page.ClientScript.RegisterStartupScript(typeof(Page),"Message",script.ToString());
    }
}
