using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page  {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for load event
        try {
            if(!Page.IsPostBack) {
                //Display the web service url from a Tracking carton instance
                this.cboService.Items.Add(new Tracking.Carton().Url);
                this.cboService.SelectedIndex = 0;
                OnServiceSelected(null,null);
            }
        }
        catch(Exception ex) { reportError(ex); }
    }
    protected void OnServiceSelected(object sender,EventArgs e) {
        //Event handler for change in selected service
        this.txtUserID.Enabled = this.txtPassword.Enabled = this.txtCartonNum.Enabled = this.cboService.SelectedItem.Text == new Tracking.Carton().Url;
        this.rfvUserID.Enabled = this.rfvPassword.Enabled = this.rfvCartonNum.Enabled = this.cboService.SelectedItem.Text == new Tracking.Carton().Url;
    }
    protected void OnTrack(object sender,EventArgs e) {
        //Event handler to track a carton
        try {
            //Clear prior results
            this.txtCartonDetail.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCartonDetail.Text = "";

            if(this.cboService.SelectedItem.Text == new Tracking.Carton().Url) {
                //Create a custom SOAPHeader instance
                Tracking.SoapCredential sc = new Tracking.SoapCredential();
                sc.UserName = this.txtUserID.Text;
                sc.Password = this.txtPassword.Text;

                //Create Tracking web service client and add SOAP header credentials
                Tracking.Carton carton = new Tracking.Carton();
                carton.SoapCredentialValue = sc;

                //Track a carton and display results
                Tracking.CartonWSDetail ds = carton.TrackCarton(this.txtCartonNum.Text);
                this.txtCartonDetail.Text = ds.GetXml();
            }
        }
        catch(Exception ex) { reportError(ex); }
    }
    #region Local Services: reportError()
    private void reportError(Exception ex) {
        //Report an error to the user
        this.txtCartonDetail.ForeColor = System.Drawing.Color.Red;
        this.txtCartonDetail.Text = ex.ToString();
    }
    #endregion
}
