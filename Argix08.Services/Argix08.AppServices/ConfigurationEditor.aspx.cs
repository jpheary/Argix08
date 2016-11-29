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

public partial class ConfigurationEditor:System.Web.UI.Page {
    //Members
    protected string mTerminal="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //
        if(!Page.IsPostBack) {
            //Display local terminal name
            Argix.AppService config = new Argix.AppService();
            Argix.TerminalInfo info = config.GetTerminalInfo();
            this.mTerminal = info.Description + " (" + info.Number + ") : " + info.Connection;
            ViewState["Terminal"] = this.mTerminal;
            
            //
            this.cboApp.DataBind();
            if(this.cboApp.Items.Count > 0) this.cboApp.SelectedIndex = 0;
            OnAppChanged(this.cboApp,EventArgs.Empty);
            setServices();
        }
        else {
            this.mTerminal = ViewState["Terminal"].ToString();
        }
    }
    protected void OnAppChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        this.cboUser.DataBind();
        this.cboUser.SelectedValue = Argix.AppService.USER_ALL;
        OnUserChanged(this.cboUser,EventArgs.Empty);
        setServices();
    }
    protected void OnUserChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        this.grdConfig.DataBind();
        setServices();
    }
    protected void OnConfigurationEntrySelected(object sender,EventArgs e) {
        //Event handler for change in selected configuration entry
        setServices();
    }
    protected void OnConfigurationEntryEditing(object sender,GridViewEditEventArgs e) {
        //Event handler for editing a configuration entry event
        this.grdConfig.SelectedIndex = e.NewEditIndex;
    }
    protected void OnConfigurationRowDataBound(object sender,GridViewRowEventArgs e) {
        //
        if(e.Row.RowType == DataControlRowType.Footer) {
            Label lblApplication=(Label)e.Row.FindControl("lblApplication");
            lblApplication.Text = this.cboApp.SelectedValue;
            Label lblUserName=(Label)e.Row.FindControl("lblUserName");
            lblUserName.Text = this.cboUser.SelectedValue;
        }
    }
    protected void OnConfigurationRowCommand(object sender,GridViewCommandEventArgs e) {
        //Event handler for row command event
        switch(e.CommandName) {
            case "Insert":
                Label lblApplication=(Label)this.grdConfig.FooterRow.FindControl("lblApplication");
                Label lblUserName=(Label)this.grdConfig.FooterRow.FindControl("lblUserName");
                TextBox txtKey=(TextBox)this.grdConfig.FooterRow.FindControl("txtKey");
                TextBox txtValue=(TextBox)this.grdConfig.FooterRow.FindControl("txtValue");
                Argix.AppService config = new Argix.AppService();
                bool created = config.CreateConfigurationEntry(lblApplication.Text,lblUserName.Text,txtKey.Text,txtValue.Text,"1");
                this.grdConfig.DataBind();
                this.grdConfig.ShowFooter = false;
                break;
            case "Cancel":
                this.grdConfig.ShowFooter = false;
                break;
        }
    } 
    protected void OnButtonClick(object sender,EventArgs e) {
        //Event handler for command button clicked
        Button btn = (Button)sender;
        switch(btn.ID) {
            case "btnAdd":
                this.grdConfig.ShowFooter = true;
                break;
        }
    }
    #region Local Services: setServices()
    private void setServices() {
        //Set user services
        this.btnAdd.Enabled = (this.cboUser.SelectedValue != Argix.UserConfiguration.USER_DEFAULT && this.cboUser.SelectedValue != Argix.AppService.USER_ALL);
    }
    #endregion
}
