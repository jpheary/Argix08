//	File:	Default.aspx.cs
//	Author:	J. Heary
//	Date:	11/13/08
//	Desc:	Argix Direct BN tracking default page.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default :System.Web.UI.Page {
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.txtNumbers.Text = "9102184860831400005712";
            this.txtNumbers.Text += "\n9102184860831400006375";
            this.txtNumbers.Text += "\n9102184860831400008690";
            this.txtNumbers.Text += "\n9102932495864415807290";
        }
    }
    protected void OnTrackImg(object sender,ImageClickEventArgs e) { OnTrack(null,null); }
    protected void OnTrack(object sender,EventArgs e) {
        //Event handler to track a carton
        try {
            //Set flag
            Session["FromDefault"] = true;

            //Validate carton numbers
            string cn = this.txtNumbers.Text.Trim();
            cn = cn.Replace("\n","");
            cn = cn.Replace("-","");
            cn = cn.Replace(Convert.ToChar(','),Convert.ToChar(13));
            string[] numbers = cn.Split(Convert.ToChar(13));

            //Package carton numbers for Tracker (comma delimited)
            string query = "";
            for(int i = 0; i < numbers.Length; i++) {
                if(i > 0) query += ",";
                query += numbers[i];
            }
            
            //Forward to Tracker for tracking
            Response.Redirect("~/Tracker.aspx?ID=" + query,false);
        }
        catch(Exception ex) { reportError(ex); }
    }
    #region Local Services: reportError()
    private void reportError(Exception ex) {
        //Report an error to the user
        Session["Error"] = ex;
        Response.Redirect("~/Error.aspx",false);
    }
    #endregion
}
