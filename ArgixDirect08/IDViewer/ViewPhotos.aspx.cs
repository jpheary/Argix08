using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewPhotos:System.Web.UI.Page {
    //Members
    private string mIDType = "";
    private int mIndex = 0;
    private Argix.Employees mEmployees=null;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        if(!Page.IsPostBack) {
            this.mIDType = Request.QueryString["type"];
            this.ViewState.Add("IDType",this.mIDType);
            this.ViewState.Add("Index",this.mIndex);
            this.ViewState.Add("Employees",this.mEmployees);

            this.Master.PhotosButtonFontColor = System.Drawing.Color.White;
            this.btnDrivers.BackColor = this.btnEmployees.BackColor = this.btnHelpers.BackColor = this.btnVendors.BackColor = System.Drawing.Color.LightSteelBlue;
            if(this.mIDType != null) OnChangeView(null,new CommandEventArgs(this.mIDType,null));
        }
        else {
            this.mIDType = this.ViewState["IDType"] != null ? this.ViewState["IDType"].ToString() : "";
            this.mIndex = this.ViewState["Index"] != null ? int.Parse(this.ViewState["Index"].ToString()) : 0;
            this.mEmployees = this.ViewState["Employees"] != null ? (Argix.Employees)this.ViewState["Employees"] : null;
        }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        this.btnDrivers.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnDrivers.Style["border-bottom-style"] = "solid";
        this.btnEmployees.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnEmployees.Style["border-bottom-style"] = "solid";
        this.btnHelpers.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnHelpers.Style["border-bottom-style"] = "solid";
        this.btnVendors.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnVendors.Style["border-bottom-style"] = "solid";
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
        this.mIDType = e.CommandName;
        this.ViewState["IDType"] = this.mIDType;
        this.mIndex = 0;
        this.ViewState["Index"] = this.mIndex;
        Argix.KronosProxy kp = new Argix.KronosProxy();
        this.mEmployees = kp.GetEmployeeList(this.mIDType);
        this.ViewState["Employees"] = this.mEmployees;
        OnChangePhoto(null,new CommandEventArgs(this.mIDType,null));
    }

    protected void OnChangePhoto(object sender,CommandEventArgs e) {
        //
        if(this.mIDType.Length > 0) {
            switch(e.CommandName) {
                case "Back":
                    if(this.mIndex > 0) this.mIndex--; else this.mIndex = this.mEmployees.Count - 1;
                    break;
                case "Next":
                    if(this.mIndex < this.mEmployees.Count - 1) this.mIndex++; else this.mIndex = 0;
                    break;
            }
            this.ViewState["Index"] = this.mIndex;
            Argix.KronosProxy kp = new Argix.KronosProxy();
            Argix.Employee employee = kp.GetEmployee(this.mIDType, this.mEmployees[this.mIndex].IDNumber);
            this.lblName.Text = employee.FirstName + " " + employee.LastName;
            this.imgPhoto.ImageUrl = "~/Photo.aspx?type=" + this.mIDType + "&id=" + employee.IDNumber;
        }
    }
}
