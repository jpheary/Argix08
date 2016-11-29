using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Devices;

public partial class _Default : System.Web.UI.Page {
    //
    protected void Page_Load(object sender, EventArgs e) {
        //
        ScaleClient scale=null;
        if(!Page.IsPostBack) {
            scale = new ScaleClient(new InstanceContext(new ScaleCallbacks()),"WSDualHttpBinding_IScale");
            Session["Scale"] = scale;
            try {
                //Service
                this.lblService.Text = scale.Endpoint.Address.Uri.ToString();
                this.lblIsOn.Text = scale.IsOn().ToString();
                
                //Settings
                this.lblBornOn.Text = scale.BornOn().ToLongTimeString();
                PortSettings ps = scale.GetSettings();
                this.lblPort.Text = ps.PortName;
                this.lblBaud.Text = ps.BaudRate.ToString();
                this.lblData.Text = ps.DataBits.ToString();
                this.lblStop.Text = ps.StopBits.ToString();
                this.lblParity.Text = ps.Parity.ToString();
                this.lblHandshake.Text = ps.Handshake.ToString();

                this.btnStart.Enabled = true;
                this.btnStop.Enabled = false;
                this.btnWeight.Enabled = false;
                this.btnZero.Enabled = false;
            }
            catch(TimeoutException ex) {
                reportError(new ApplicationException("The service operation timed out.",ex));
                scale.Abort();
            }
            catch(FaultException<Devices.ScaleFault> f) {
                reportError(new ApplicationException("There was a scale fault- " + f.ToString()));
                scale.Abort();
            }
            catch(CommunicationException ex) {
                reportError(new ApplicationException("There was a communication problem.",ex));
                scale.Abort();
            }
        }
    }
    protected void TurnOn(object sender,EventArgs e) { 
        //
        ScaleClient scale = (ScaleClient)Session["Scale"];
        try {
            if(!scale.IsOn()) scale.TurnOn();
            this.lblIsOn.Text = scale.IsOn().ToString();
            this.btnStart.Enabled = !scale.IsOn();
            this.btnStop.Enabled = scale.IsOn();
            this.btnWeight.Enabled = scale.IsOn();
            this.btnZero.Enabled = scale.IsOn();
        }
        catch(TimeoutException ex) {
            reportError(new ApplicationException("The service operation timed out.",ex));
            scale.Abort();
        }
        catch(FaultException<Devices.ScaleFault> f) {
            reportError(new ApplicationException("There was a scale fault- " + f.ToString()));
            scale.Abort();
        }
        catch(CommunicationException ex) {
            reportError(new ApplicationException("There was a communication problem.", ex));
            scale.Abort();
        }
    }
    protected void TurnOff(object sender,EventArgs e) {
        //
        ScaleClient scale = (ScaleClient)Session["Scale"];
        try {
            if(scale.IsOn()) scale.TurnOff();
            this.lblIsOn.Text = scale.IsOn().ToString();
            this.btnStart.Enabled = !scale.IsOn();
            this.btnStop.Enabled = scale.IsOn();
            this.btnWeight.Enabled = scale.IsOn();
            this.btnZero.Enabled = scale.IsOn();
        }
        catch(TimeoutException ex) {
            reportError(new ApplicationException("The service operation timed out.",ex));
            scale.Abort();
        }
        catch(FaultException<Devices.ScaleFault> f) {
            reportError(new ApplicationException("There was a scale fault- " + f.ToString()));
            scale.Abort();
        }
        catch(CommunicationException ex) {
            reportError(new ApplicationException("There was a communication problem.",ex));
            scale.Abort();
        }
    }
    protected void GetWeight(object sender,EventArgs e) {
        //
        ScaleClient scale = (ScaleClient)Session["Scale"];
        try {
            bool isStable = false;
            this.lblWeight.Text = scale.GetWeight(ref isStable).ToString();
        }
        catch(TimeoutException ex) {
            reportError(new ApplicationException("The service operation timed out.",ex));
            scale.Abort();
        }
        catch(FaultException<Devices.ScaleFault> f) {
            reportError(new ApplicationException("There was a scale fault- " + f.ToString()));
            scale.Abort();
        }
        catch(CommunicationException ex) {
            reportError(new ApplicationException("There was a communication problem.",ex));
            scale.Abort();
        }
    }
    protected void Zero(object sender,EventArgs e) {
        //
        ScaleClient scale = (ScaleClient)Session["Scale"];
        try {
            scale.Zero();
            bool isStable = false;
            this.lblWeight.Text = scale.GetWeight(ref isStable).ToString();
        }
        catch(TimeoutException ex) {
            reportError(new ApplicationException("The service operation timed out.",ex));
            scale.Abort();
        }
        catch(FaultException<Devices.ScaleFault> f) {
            reportError(new ApplicationException("There was a scale fault- " + f.ToString()));
            scale.Abort();
        }
        catch(CommunicationException ex) {
            reportError(new ApplicationException("There was a communication problem.",ex));
            scale.Abort();
        }
    }
    private void reportError(Exception ex) {
        //Report an exception to the user
        try {
            string src = (ex.Source != null) ? ex.Source + "-\n" : "";
            string msg = src + ex.Message;
            if(ex.InnerException != null) {
                if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            }
            showMsgBox((msg));
        }
        catch(Exception) { }
    }
    private void showMsgBox(string message) { showMsgBox(message,false); }
    private void showMsgBox(string message,bool isStartup) {
        //
        System.Text.StringBuilder script = new System.Text.StringBuilder();
        script.Append("<script language='javascript' type='text/javascript'>");
        script.Append("\talert('" + message + "');");
        script.Append("</script>");
        if(isStartup)
            Page.ClientScript.RegisterStartupScript(typeof(Page),"Error",script.ToString());
        else
            System.Web.HttpContext.Current.Response.Write(script.ToString());
    }

}
