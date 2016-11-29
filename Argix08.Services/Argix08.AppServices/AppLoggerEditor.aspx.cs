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

public partial class AppLoggerEditor:System.Web.UI.Page {
    //Members
    private string mLogName="",mSource="";
    protected string mTerminal="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //
        if(!Page.IsPostBack) {
            //Get query info
            this.mLogName = Request.QueryString["log"];
            this.mSource = Request.QueryString["src"];

            //Display local terminal name
            Argix.AppService logger = new Argix.AppService();
            Argix.TerminalInfo info = logger.GetTerminalInfo();
            this.mTerminal = info.Description + " (" + info.Number + ") : " + info.Connection;
            ViewState["Terminal"] = this.mTerminal;

            //Setup the log
            this.cboLog.DataBind();
            if(this.mLogName != null && this.mLogName.Trim().Length > 0) {
                if(this.cboLog.Items.Count > 0) this.cboLog.SelectedValue = this.mLogName;
            }
            else {
                if(this.cboLog.Items.Count > 0) this.cboLog.SelectedIndex = 0;
            }
            OnLogNameChanged(this.cboLog,EventArgs.Empty);
            setServices();
        }
        else {
            this.mTerminal = ViewState["Terminal"].ToString();
        }
    }
    protected void OnLogNameChanged(object sender,EventArgs e) {
        //Event handler for change in slected log name
        this.cboSource.DataBind();
        if(this.mSource != null && this.mSource.Trim().Length > 0) {
            if(this.cboSource.Items.Count > 0) this.cboSource.SelectedValue = this.mSource;
        }
        else {
            if(this.cboSource.Items.Count > 0) this.cboSource.SelectedIndex = 0;
        }
        OnLogSourceChanged(this.cboSource,EventArgs.Empty);
    }
    protected void OnLogSourceChanged(object sender,EventArgs e) {
        //Event handler for change in selected log source
        this.grdLog.DataBind();
    }
    protected void OnLogEntrySelected(object sender,EventArgs e) {
        //Event handler for change in selected configuration entry
        setServices();
    }
    protected void OnButtonClick(object sender,EventArgs e) {
        //Event handler for command button clicked
        Button btn = (Button)sender;
        switch(btn.ID) {
            case "btnDelete":
                long id = Convert.ToInt64(this.grdLog.SelectedDataKey.Value);
                Argix.AppService logger = new Argix.AppService();
                bool ret = logger.DeleteLogEntry(id);
                if(ret) this.grdLog.DataBind();
                break;
        }
    }
    #region Local Services: setServices()
    private void setServices() {
        //Set user services
        this.btnDelete.Enabled = this.grdLog.SelectedRow != null;
    }
    #endregion
}
