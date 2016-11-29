using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewEmployee:System.Web.UI.Page {
    //Members
    private string mIDType="";
    private int mIDNumber=0;
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!this.IsPostBack) {
            this.mIDType = Request.QueryString["type"];
            this.ViewState.Add("IDType",this.mIDType);
            this.mIDNumber = int.Parse(Request.QueryString["id"]);
            this.ViewState.Add("IDNumber",this.mIDNumber);
            
            this.Master.ProfilesButtonFontColor = System.Drawing.Color.White;
            this.btnDetail.BackColor = System.Drawing.Color.White;
            this.btnDetail.Style["border-bottom-style"] = "none";
            this.btnBack.BackColor = this.btnPhoto.BackColor = this.btnSpare.BackColor = System.Drawing.Color.LightSteelBlue;
        }
        else {
            this.mIDType = this.ViewState["IDType"].ToString();
            this.mIDNumber = int.Parse(this.ViewState["IDNumber"].ToString());
        }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        this.btnDetail.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnDetail.Style["border-bottom-style"] = "solid";
        this.btnPhoto.BackColor = System.Drawing.Color.LightSteelBlue;
        this.btnPhoto.Style["border-bottom-style"] = "solid";
        switch(e.CommandName) {
            case "Back":
                Response.Redirect("ViewEmployees.aspx?type=" + this.mIDType + "&id=" + this.mIDNumber);
                break;
            case "Detail":
                this.btnDetail.BackColor = System.Drawing.Color.White;
                this.btnDetail.Style["border-bottom-style"] = "none";
                this.mvEmployee.SetActiveView(this.vwDetail);
                break;
            case "Photo":
                this.btnPhoto.BackColor = System.Drawing.Color.White;
                this.btnPhoto.Style["border-bottom-style"] = "none";
                this.mvEmployee.SetActiveView(this.vwPhoto);
                this.imgPhoto.ImageUrl = "~/Photo.aspx?type=" + this.mIDType + "&id=" + this.mIDNumber;
                this.imgSignature.ImageUrl = "~/Signature.aspx?type=" + this.mIDType + "&id=" + this.mIDNumber;
                break;
        }
    }
}
