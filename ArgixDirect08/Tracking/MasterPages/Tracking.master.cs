using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class TrackingMaster : System.Web.UI.MasterPage {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        if(!Page.IsPostBack) {
            //Set reports:  Admin\Argix role get everything (00000.xml);
            //              Reports role gets custom file if exists or default file otherwise
            MembershipServices membership = new MembershipServices();
            if(membership.IsAdmin || membership.IsArgix)
                this.xmlReports.DataFile = "~/App_Data/00000.xml";
            else if(membership.IsRSMember) {
                if(membership.MemberProfile != null) {
                    string vfile = "~/App_Data/" + membership.MemberProfile.ClientVendorID.Trim().PadLeft(5,'0') + ".xml";
                    string file = Server.MapPath(vfile);
                    if(System.IO.File.Exists(file))
                        this.xmlReports.DataFile = vfile;
                    else
                        this.xmlReports.DataFile = "~/App_Data/default.xml";
                }
            }
            else
                this.xmlReports.DataFile = "~/App_Data/blank.xml";

            //Set UI element states
            this.imgTrackByPOPRO.Visible = this.lnkTrackByPOPRO.Visible = membership.IsAdmin || membership.IsArgix || membership.IsPOMember;
            this.imgManageGuests.Visible = this.lnkManageGuests.Visible = membership.IsAdmin;
            this.imgManageUsers.Visible = this.lnkManageUsers.Visible = membership.IsAdmin;
            this.imgSetup.Visible = this.lnkSetup.Visible = membership.IsAdmin;
            this.imgManageMembership.Visible = this.lnkManageMembership.Visible = membership.IsAdmin;
        }
    }
    public void ShowMsgBox(string message) {
        //
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language='javascript'>");
        script.Append(" alert('" + message + "');");
        script.Append("</script>");
        ScriptManager.RegisterStartupScript(Page,typeof(Page),"ErrorMsg",script.ToString(),false);
    }
    public void ReportError(Exception ex) { Master.ReportError(ex); }
}
