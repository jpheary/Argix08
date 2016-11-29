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

public partial class KronosEditor:System.Web.UI.Page {
    //Members
    protected string mTerminal="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //
        if(!Page.IsPostBack) {
            //Display local terminal name
            Argix.Kronos kronos = new Argix.Kronos();
            Argix.TerminalInfo info = kronos.GetTerminalInfo();
            this.mTerminal = info.Description + " (" + info.Number + ") : " + info.Connection;
            ViewState["Terminal"] = this.mTerminal;

            //
            this.cboType.DataBind();
            if(this.cboType.Items.Count > 0) this.cboType.SelectedIndex = 0;
            OnTypeChanged(this.cboType,EventArgs.Empty);
            setServices();
        }
        else {
            this.mTerminal = ViewState["Terminal"].ToString();
        }
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        this.grdEmployees.DataBind();
    }
    protected void OnEmployeeSelected(object sender,EventArgs e) {
        //Event handler for change in selected employee
        setServices();
    }
    #region Local Services: setServices()
    private void setServices() {
        //Set user services
    }
    #endregion
}
