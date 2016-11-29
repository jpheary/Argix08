using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewEmployees:System.Web.UI.Page {
    //Members
    private string mIDType = "";
    private int mIDNumber = 0;
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        if(!Page.IsPostBack) {
            this.mIDType = "";
            this.ViewState.Add("IDType",this.mIDType);
            this.mIDNumber = 0;
            this.ViewState.Add("IDNumber",this.mIDNumber);
            this.Master.ProfilesButtonFontColor = System.Drawing.Color.White;
            this.btnDrivers.BackColor = this.btnEmployees.BackColor = this.btnHelpers.BackColor = this.btnVendors.BackColor = System.Drawing.Color.LightSteelBlue;
        }
        else {
            this.mIDType = this.ViewState["IDType"].ToString();
            this.mIDNumber = int.Parse(this.ViewState["IDNumber"].ToString());
        }
    }
    protected void OnChangeIDType(object sender,CommandEventArgs e) {
        //
        this.btnDrivers.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnDrivers.Style["border-bottom-style"] = "solid";
        this.btnEmployees.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnEmployees.Style["border-bottom-style"] = "solid";
        this.btnHelpers.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnHelpers.Style["border-bottom-style"] = "solid";
        this.btnVendors.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnVendors.Style["border-bottom-style"] = "solid";
        this.ViewState["IDType"] = this.mIDType = e.CommandName;
        switch(e.CommandName) {
            case "Drivers":
                this.btnDrivers.BackColor = System.Drawing.Color.White;
                this.btnDrivers.Style["border-bottom-style"] = "none";
                break;
            case "Employees":
                this.btnEmployees.BackColor = System.Drawing.Color.White;
                this.btnEmployees.Style["border-bottom-style"] = "none";
                break;
            case "Helpers":
                this.btnHelpers.BackColor = System.Drawing.Color.White;
                this.btnHelpers.Style["border-bottom-style"] = "none";
                break;
            case "Vendors":
                this.btnVendors.BackColor = System.Drawing.Color.White;
                this.btnVendors.Style["border-bottom-style"] = "none";
                break;
        }
        this.odsEmployees.SelectParameters["idType"].DefaultValue = e.CommandName;
    }
    protected void OnEmployeeSelected(object sender,EventArgs e) {
        //Event handler for change in selected employee
        DataKey dataKey = (DataKey)this.grdEmployees.DataKeys[this.grdEmployees.SelectedIndex];
        this.ViewState["IDNumber"] = this.mIDNumber = int.Parse(dataKey["IDNumber"].ToString());
        
        this.odsEmployee.SelectParameters["idType"].DefaultValue = this.mIDType;
        this.odsEmployee.SelectParameters["idNumber"].DefaultValue = this.mIDNumber.ToString();
        this.btnDetail.BackColor = System.Drawing.Color.White;
        this.btnDetail.Style["border-bottom-style"] = "none";
        this.btnPhoto.BackColor = System.Drawing.Color.LightSteelBlue;

        this.imgPhoto.ImageUrl = "~/Photo.aspx?type=" + this.mIDType + "&id=" + this.mIDNumber;
        this.imgSignature.ImageUrl = "~/Signature.aspx?type=" + this.mIDType + "&id=" + this.mIDNumber;
        this.mvPage.SetActiveView(this.vwEmployee);
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        switch(e.CommandName) {
            case "Back":
                this.mvPage.SetActiveView(this.vwEmployees);
                break;
            case "Detail":
                this.btnDetail.BackColor = System.Drawing.Color.White;
                this.btnDetail.Style["border-bottom-style"] = "none";
                this.btnPhoto.BackColor = System.Drawing.Color.LightSteelBlue;
                this.btnPhoto.Style["border-bottom-style"] = "solid";
                this.mvEmployee.SetActiveView(this.vwDetail);
                break;
            case "Photo":
                this.btnDetail.BackColor = System.Drawing.Color.LightSteelBlue;
                this.btnDetail.Style["border-bottom-style"] = "solid";
                this.btnPhoto.BackColor = System.Drawing.Color.White;
                this.btnPhoto.Style["border-bottom-style"] = "none";
                this.mvEmployee.SetActiveView(this.vwPhoto);
                break;
        }
    }
}
