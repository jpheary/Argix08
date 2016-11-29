using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = "Load Tenders";
            this.ddpTenders.ToDate = DateTime.Today;
            this.ddpTenders.FromDate = DateTime.Today.AddDays(-7);
            if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
            OnClientChanged(this.cboClient,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        OnClientChanged(this.cboClient,EventArgs.Empty);
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTenderSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAllTendersSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        CheckBox chkAll = (CheckBox)this.grdTenders.HeaderRow.FindControl("chkAll");
        foreach(GridViewRow row in this.grdTenders.Rows) {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            chk.Checked = chkAll.Checked;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
    }
    protected void OnOK(object sender,EventArgs e) {
        //Event handler for export button clicked
        //
        Session["LoadTenders"] = null;
        if(this.grdTenders.DataKeys.Count > 0) {
            //Get parameters for the query
            string _load = "";
            string client = this.cboClient.SelectedValue;
            DateTime start = this.ddpTenders.FromDate;
            DateTime end = this.ddpTenders.ToDate;
            EnterpriseService enterprise = new EnterpriseService();
            LoadTenderDS ds = new LoadTenderDS();
            LoadTenderDS _ds = enterprise.GetLoadTenders(client,start,end);
            foreach(GridViewRow row in SelectedRows) {
                DataKey dataKey = (DataKey)this.grdTenders.DataKeys[row.RowIndex];
                _load = dataKey["Load"].ToString();
                ds.Merge(_ds.Tables[EnterpriseService.TBL_LOADTENDERS].Select("Load='" + _load + "'"));
            }
            Session["LoadTenders"] = ds;
            Response.Redirect("LoadTender.aspx");
        }
    }

    private GridViewRow[] SelectedRows {
        get {
            GridViewRow[] rows=new GridViewRow[] { };
            int i=0;
            foreach(GridViewRow row in this.grdTenders.Rows) {
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if(isChecked) i++;
            }
            if(i > 0) {
                rows = new GridViewRow[i];
                int j=0;
                foreach(GridViewRow row in this.grdTenders.Rows) {
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if(isChecked) rows[j++] = row;
                }
            }
            return rows;
        }
    }
}
