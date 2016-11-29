using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

public partial class ManageUsers:System.Web.UI.Page {
    //Members
    private string mCompanyType="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            if(Cache["Users"] == null) Cache["Users"] = new object();
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnCustomersDataBinding(object sender,EventArgs e) {
        //Event handler from customer dropdown list binding to a data source
        //NOTE: this.grdUsers.SelectedRow throws ArgumentOutOfRange exception
        //      so use this.mCompanyType set in OnGridRowEditing()
        DropDownList cboCustomers = (DropDownList)sender;
        if(this.mCompanyType.Length > 0 && this.mCompanyType == "vendor") 
            cboCustomers.DataValueField = "VendorID";
        else 
            cboCustomers.DataValueField = "ClientID";
        this.odsCustomers.SelectParameters["companyType"].DefaultValue = this.mCompanyType;
    }
    protected void OnUserChanged(object sender,EventArgs e) {
        //Event handler for grid row (user) selected
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnGridRowEditing(object sender,GridViewEditEventArgs e) {
        //Event handler for grid row editing
        this.grdUsers.SelectedIndex = e.NewEditIndex;
        this.mCompanyType = this.grdUsers.SelectedRow.Cells[6].Text.Trim().ToLower();
        OnValidateForm(null,EventArgs.Empty);

        //System.Text.StringBuilder script = new System.Text.StringBuilder();
        //script.Append("<script language=javascript>");
        //script.Append("scroll('" + this.grdUsers.SelectedRow.Cells[1].Text.Trim() + "')");
        //script.Append("</script>");
        //Page.ClientScript.RegisterStartupScript(typeof(Page),"Scroll",script.ToString());
    }
    protected void OnGridRowCancelingEdit(object sender,GridViewCancelEditEventArgs e) {
        //Event handler for grid row editing canceled
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnGridRowUpdating(object sender,GridViewUpdateEventArgs e) {
        //Event handler for grid row updating
        string companyID = e.NewValues["CompanyID"].ToString();
        string companyType = this.grdUsers.SelectedRow.Cells[6].Text.Trim();
        string filter = companyType.ToLower()=="client" ? "ClientID='" + companyID + "'" : "VendorID='" + companyID + "'";
        string company = new TrackingServices().GetCustomers(companyType).ClientTable.Select(filter)[0]["CompanyName"].ToString().Trim();
        this.odsUsers.UpdateParameters["Company"].DefaultValue = company;
    }
    protected void OnGridRowUpdated(object sender,GridViewUpdatedEventArgs e) {
        //Event handler for grid row updated
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnGridRowDeleting(object sender,GridViewDeleteEventArgs e) {
        //Event handler for grid row deleting
        this.grdUsers.SelectedIndex = e.RowIndex;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        //Set services
        this.btnWelcomeMessage.Enabled = this.grdUsers.SelectedDataKey != null;
        this.btnResetPassword.Enabled = this.grdUsers.SelectedDataKey != null;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        string userID="";
        switch(e.CommandName) {
            case "Refresh":
                Cache["Users"] = new object();
                this.grdUsers.DataBind();
                break;
            case "Welcome":
                //Send a welcome message to the selected user
                userID = this.grdUsers.SelectedValue.ToString();
                string userName = this.grdUsers.SelectedRow.Cells[2].Text;
                string email = this.grdUsers.SelectedRow.Cells[3].Text;
                if(new EmailServices().SendWelcomeMessage(userName,userID,email))
                    Master.ShowMsgBox("Welcome email has been sent to " + userID + ".");
                else
                    Master.ShowMsgBox("System Error: Welcome email could NOT be sent to " + userID + ".");
                break;
            case "Reset":
                //Reset the selected users password
                userID = this.grdUsers.SelectedValue.ToString();
                MembershipUser user = Membership.GetUser(userID);
                if(user.IsLockedOut) {
                    //If user's account is locked out then unlock it first
                    if(!user.UnlockUser()) {
                        Master.ShowMsgBox("System could not unlock the user. Password was not reset.");
                        return;
                    }
                }
                string pwd = Membership.GeneratePassword(6,0);
                user.ChangePassword(user.GetPassword(),pwd);
                ProfileCommon profile = new ProfileCommon().GetProfile(userID);
                profile.PasswordReset = true;
                profile.Save();

                new EmailServices().SendPasswordResetMessage(user.UserName,user.Email,pwd);
                Master.ShowMsgBox("Password has been reset and emailed to the user.");
                break;
        }
    }
}
