using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewFreight:System.Web.UI.Page {
    //Members
    private string mFreightID = "", mFreightType="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.mFreightID = Request.QueryString["freightID"] != null ? Request.QueryString["freightID"] : "";
            ViewState.Add("FreightID",this.mFreightID);
            this.Master.FreightButtonFontColor = System.Drawing.Color.White;
            this.mFreightType = "Regular";
            ViewState.Add("FreightType","Regular");
            OnChangeView(null,new CommandEventArgs("Shipments",null));
        }
        else {
            this.mFreightID = ViewState["FreightID"].ToString();
            this.mFreightType = ViewState["FreightType"].ToString();
        }
        if(this.mFreightID.Trim().Length > 0) this.mvPage.SetActiveView(this.vwShipment);
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        this.lsvShipments.DataBind();
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        switch(e.CommandName) {
            case "Shipments":
                this.odsShipments.SelectParameters["freightType"].DefaultValue = this.mFreightType;
                this.lsvShipments.DataBind();
                this.btnRegular.BackColor = this.mFreightType == "Regular" ? System.Drawing.Color.White : System.Drawing.Color.LightSteelBlue;
                this.btnRegular.Style["border-bottom-style"] = this.mFreightType == "Regular" ? "none" : "solid";
                this.btnReturns.BackColor = this.mFreightType == "Returns" ? System.Drawing.Color.White : System.Drawing.Color.LightSteelBlue;
                this.btnReturns.Style["border-bottom-style"] = this.mFreightType == "Returns" ? "none" : "solid";
                this.mvPage.SetActiveView(this.vwShipments);
                break;
            case "Regular":
                ViewState["FreightType"] = "Regular";
                this.odsShipments.SelectParameters["freightType"].DefaultValue = "Regular";
                this.lsvShipments.DataBind();
                this.btnRegular.BackColor = System.Drawing.Color.White;
                this.btnRegular.Style["border-bottom-style"] = "none";
                this.btnReturns.BackColor = System.Drawing.Color.LightSteelBlue;
                this.btnReturns.Style["border-bottom-style"] = "solid";
                break;
            case "Returns":
                ViewState["FreightType"] = "Returns";
                this.odsShipments.SelectParameters["freightType"].DefaultValue = "Returns";
                this.lsvShipments.DataBind();
                this.btnRegular.BackColor = System.Drawing.Color.LightSteelBlue;
                this.btnRegular.Style["border-bottom-style"] = "solid";
                this.btnReturns.BackColor = System.Drawing.Color.White;
                this.btnReturns.Style["border-bottom-style"] = "none";
                break;
            case "Shipment":
                Argix.Freight.InboundShipment shipment = new Argix.Freight.FreightServiceProxy().GetInboundShipment(e.CommandArgument.ToString());
                this.odsShipment.SelectParameters["freightID"].DefaultValue = e.CommandArgument.ToString();
                this.dvShipment.DataBind();
                this.dvShipment1.DataBind();
                this.dvShipment2.DataBind();
                this.odsAssignments.SelectParameters["freightID"].DefaultValue = e.CommandArgument.ToString();
                this.lsvAssignments.DataBind();
                this.btnFreight.BackColor = System.Drawing.Color.White;
                this.btnFreight.Style["border-bottom-style"] = "none";
                this.btnAssignments.BackColor = System.Drawing.Color.LightSteelBlue;
                this.btnAssignments.Style["border-bottom-style"] = "solid";
                this.mvShipment.SetActiveView(this.vwDetail);
                this.mvPage.SetActiveView(this.vwShipment);
                break;
            case "Freight":
                this.btnFreight.BackColor = System.Drawing.Color.White;
                this.btnFreight.Style["border-bottom-style"] = "none";
                this.btnAssignments.BackColor = System.Drawing.Color.LightSteelBlue;
                this.btnAssignments.Style["border-bottom-style"] = "solid";
                this.mvShipment.SetActiveView(this.vwDetail);
                break;
            case "Assignments":
                this.btnFreight.BackColor = System.Drawing.Color.LightSteelBlue;
                this.btnFreight.Style["border-bottom-style"] = "solid";
                this.btnAssignments.BackColor = System.Drawing.Color.White;
                this.btnAssignments.Style["border-bottom-style"] = "none";
                this.mvShipment.SetActiveView(this.vwAssignments);
                break;
        }
    }
    public string GetItemCount(object cartons,object pallets) {
        return cartons != DBNull.Value ? cartons.ToString() + " CTNS" : pallets.ToString() + " PLTS";
    }
    public string GetClientInfo(object clientNumber,object clientName) {
        return clientNumber.ToString() + ", " + clientName.ToString();
    }
    public string GetShipperInfo(object shipperNumber,object shipperName) {
        return shipperNumber.ToString() + ", " + shipperName.ToString();
    }
}
