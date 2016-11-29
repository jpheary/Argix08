using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewSorting:System.Web.UI.Page {
    //Members
    private string mFreightID = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.mFreightID = Request.QueryString["freightID"] != null ? Request.QueryString["freightID"] : "";
            ViewState.Add("FreightID",this.mFreightID);
            this.Master.SortingButtonFontColor = System.Drawing.Color.White;
        }
        else {
            this.mFreightID = ViewState["FreightID"].ToString();
        }
        if(this.mFreightID.Trim().Length > 0) this.mvPage.SetActiveView(this.vwAssignment);
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        this.lsvAssignments.DataBind();
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        switch(e.CommandName) {
            case "Assignments":
                this.mvPage.SetActiveView(this.vwAssignments);
                break;
            case "Assignment":
                this.mvPage.SetActiveView(this.vwAssignment);
                break;
        }
    }
    public string GetClientInfo(object client) {
        return client.ToString();
    }
    public string GetShipperInfo(object shipper) {
        return shipper.ToString();
    }
}
