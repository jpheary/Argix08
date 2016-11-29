using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class StoreSummary :System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Display search by store summary information
            TrackingDS ds = (TrackingDS)Session["StoreSummary"];
            if(ds != null && ds.CartonDetailForStoreTable.Rows.Count > 0) {
                //Summary found- show store\substore and set summary data in grid
                this.lblTitle.Text = "Store Summary: Store #" + (Session["SubStore"] != null ? Session["SubStore"] : ds.CartonDetailForStoreTable[0].Store.PadLeft(5,'0'));
                this.grdSummary.DataSourceID = "";
                this.grdSummary.DataMember = "CartonDetailForStoreTable";
                this.grdSummary.DataSource = ds;
                this.grdSummary.DataBind();
            }
            else
                Response.Redirect("TrackByStore.aspx");
        }
    }
}