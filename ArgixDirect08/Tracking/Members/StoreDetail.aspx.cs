//	File:	StoreDetail.aspx.cs
//	Author:	J. Heary
//	Date:	03/09/07
//	Desc:	
//	Rev:	
//	---------------------------------------------------------------------------
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

public partial class StoreDetail :System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        string tl = Request.QueryString["TL"];
        string lbl = Request.QueryString["LBL"];
        string ctn = Request.QueryString["CTN"];
        if(!Page.IsPostBack) {
            //Page request
            if(tl != null && tl.Length > 0 && lbl == null && ctn == null) {
                //Page request from StoreSummary.aspx (i.e. StoreDetail.aspx?TL=)
                //Show TL detail
                TrackingDS storeDetail = new TrackingDS();
                DataSet ds = (DataSet)Session["StoreDetail"];
                if(ds != null) storeDetail.Merge(ds.Tables["CartonDetailForStoreTable"].Select("TL='" + tl + "'"));
                if(storeDetail.CartonDetailForStoreTable.Rows.Count > 0) {
                    //Display store\substore, and cartons for the requested TL
                    this.lblTitle.Text = "Tracking Summary: Store#" + (Session["SubStore"] != null ? Session["SubStore"] : storeDetail.CartonDetailForStoreTable[0].Store.PadLeft(5,'0')) + "; TL#" + tl;
                    this.grdTLDetail.DataSourceID = "";
                    this.grdTLDetail.DataMember = "CartonDetailForStoreTable";
                    this.grdTLDetail.DataSource = storeDetail;
                    this.grdTLDetail.DataBind();
                }
                else
                    this.lblTitle.Text = "Tracking Summary: Store#?????" + "; TL#" + tl;
            }
            else if(lbl != null && lbl.Length > 0 && ctn != null && ctn.Length > 0 && tl != null && tl.Length > 0) {
                //Page request (NOT a postback) from this page (i.e. StoreDetail.aspx?CTN=&LBL=&TL=)
                //Build summary record for the specified carton (needed by CartonDetail.aspx)
                TrackingDS summary = new TrackingDS();
                summary.CartonSummaryTable.AddCartonSummaryTableRow(lbl,ctn,lbl,lbl,"","","","");

                //Build detail for the specified carton (needed by CartonDetail.aspx)
                TrackingDS cartons = new TrackingDS();
                MembershipServices membership = new MembershipServices();
                ProfileCommon profile = membership.MemberProfile;
                TrackingServices svcs = new TrackingServices();
                DataSet ds = svcs.GetCartons(lbl,TrackingServices.SEARCHBY_LABELNUMBER,profile.Type,profile.ClientVendorID);
                cartons.Merge(ds);

                Session["TrackingSummary"] = Session["TrackingDetail"] = null;
                if(cartons.CartonDetailTable.Rows.Count > 0) {
                    Session["TrackingSummary"] = summary;
                    Session["TrackingDetail"] = cartons;
                    Response.Redirect("CartonDetail.aspx?ID=" + lbl + "&TL=" + tl);
                }
                else
                    this.errorLabel.Text = "An error occured. System is unable to show details.";
            }
            else {
                //Something went wrong
                Response.Redirect("StoreSummary.aspx");
            }
        }
    }
}
