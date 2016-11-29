using System;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.IO;

public class EmailServices {
    //Members
    private string mSMTPServer = "";
    private string mEmailFrom = "";
    private string mEmailAdmin = "";
    private string mEmailPODReq = "";
    
    private const string HTML_PASSWORD_RESET = "\\EmailMsgs\\PasswordReset.htm";
    private const string HTML_REGISTER_RECEIPT = "\\EmailMsgs\\RegistrationReceipt.htm";
    private const string HTML_REGISTER_REJECT = "\\EmailMsgs\\RejectRegistration.htm";
    private const string HTML_WELCOME = "\\EmailMsgs\\Welcome.htm";
    private const string HTML_POD_REQUEST = "\\EmailMsgs\\PODRequest.htm";
    private const string HTML_POD_REQCONFIRM = "\\EmailMsgs\\PODRequestConfirm.htm";

    //Interface
    public EmailServices() {
        //Constructor
        //Read configuration values for this service
        this.mSMTPServer = WebConfigurationManager.AppSettings["SMTPServer"].ToString();
        this.mEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"].ToString();
        this.mEmailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"].ToString();
        this.mEmailPODReq = WebConfigurationManager.AppSettings["EmailPODReq"].ToString();
    }
    #region Accessors: SMTPServer, FromEmailAddress, AdminEmailAddress, PODReqEmailAddress
    public string SMTPServer { get { return this.mSMTPServer; } }
    public string FromEmailAddress { get { return this.mEmailFrom; } }
    public string AdminEmailAddress { get { return this.mEmailAdmin; } }
    public string PODReqEmailAddress { get { return this.mEmailPODReq; } }
    #endregion
    public bool SendRegisterReceiptEmail(string userName,string toEmailAddress) {
        //
        bool retValue = false;
        //MailMessage msg = getRegisterReceiptMessage(userName, toEmailAddress);
        MailMessage email = new MailMessage(this.mEmailFrom,toEmailAddress);
        email.Subject = "Argix Direct Tracking System - Registration";
        MailAddress copy = new MailAddress(this.mEmailAdmin);
        email.Bcc.Add(copy);
        email.BodyEncoding = System.Text.Encoding.UTF8;
        email.IsBodyHtml = true;
        email.Body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_REGISTER_RECEIPT).Replace("##",userName);
        if(email.Body.Length > 0) {
            SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
            smtpClient.Send(email);
            retValue = true;
        }
        return retValue;
    }
    public bool SendWelcomeMessage(string userFullName,string userID,string toEmailAddress) {
        //
        bool retValue = false;
        //MailMessage msg = getWelcomeMessage(userFullName,userID,toEmailAddress);
        MailMessage email = new MailMessage(this.mEmailFrom, toEmailAddress);
        email.Subject = "Welcome to Argix Direct Tracking System";
        email.BodyEncoding = System.Text.Encoding.UTF8;
        email.IsBodyHtml = true;
        email.Body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_WELCOME).Replace("**",userID).Replace("##",userFullName);
        if(email.Body.Length > 0) {
            SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
            smtpClient.Send(email);
            retValue = true;
        }
        return retValue;
    }
    public void SendPendingApprovalAlert() {
        //
        MailMessage email = new MailMessage(this.mEmailFrom, this.mEmailAdmin);
        email.Subject = "New User Pending Approval Alert";
        email.Body = "A new user has registered from the site. Please go to the Tracking site for approval.";
        email.BodyEncoding = System.Text.Encoding.UTF8;
        email.IsBodyHtml = true;
        SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
        smtpClient.Send(email);
    }
    public bool SendRejectRegistrationMessage(string userName,string toEmailAddress,string rejectReason) {
        //
        bool retValue = false;
        //MailMessage msg = getRejectRegistrationMessage(toEmailAddress,rejectReason);
        MailMessage email = new MailMessage(this.mEmailFrom, toEmailAddress);
        email.Subject = "Argix Direct Tracking System - Registration";
        email.BodyEncoding = System.Text.Encoding.UTF8;
        email.IsBodyHtml = true;
        email.Body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_REGISTER_REJECT).Replace("****",rejectReason);
        if(email.Body.Length > 0) {
            SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
            smtpClient.Send(email);
            retValue = true;
        }
        return retValue;
    }
    public bool SendPasswordResetMessage(string userName,string toEmailAddress,string password) {
        //
        bool retValue = false;
        //MailMessage msg = getPasswordResetMessage(userName,toEmailAddress,password);
        MailMessage email = new MailMessage(this.mEmailFrom, toEmailAddress);
        email.Subject = "Argix Direct Tracking System notification";
        email.BodyEncoding = System.Text.Encoding.UTF8;
        email.IsBodyHtml = true;
        email.Body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_PASSWORD_RESET).Replace("**",userName).Replace("##",password);
        if(email.Body.Length > 0) {
            SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
            smtpClient.Send(email);
            retValue = true;
        }
        return retValue;
    }
    public bool SendPODRequest(MembershipUser user,TrackingDS.CartonDetailTableRow carton,string substore) {
        //
        bool retValue = false;
        MailMessage email = new MailMessage(this.mEmailFrom,this.mEmailPODReq);
        email.Subject = "Argix Direct POD Request";
        email.BodyEncoding = System.Text.Encoding.UTF8;
        email.IsBodyHtml = true;
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_POD_REQUEST);
        body = body.Replace("*carton*",carton.CTN.Trim());
        body = body.Replace("*user*",user.UserName);
        body = body.Replace("*email*",user.Email);
        body = body.Replace("*client*",carton.CL.Trim() + "-" + carton.CLNM.Trim());
        body = body.Replace("*store*",carton.S.ToString());
        body = body.Replace("*substore*",substore);
        body = body.Replace("*storeaddress*",carton.SA1.Trim() + " " + carton.SA2.Trim() + " " + carton.SCT.Trim() + ", " + carton.SST.Trim() + " " + carton.SZ.ToString());
        body = body.Replace("*pickupdate*",carton.PUD.Trim());
        body = body.Replace("*scheduleddelivery*",carton.SCD.Trim());
        string podScan="";
        if(Convert.ToInt32(carton.SCNTP) == 3) {
            if(carton.SCD.Trim().Length > 0) podScan = carton.SCD.Trim() + " " + carton.SCTM.Trim();
        }
        body = body.Replace("*actualdelivery*",podScan);
        body = body.Replace("*tl*",carton.TL.Trim());
        body = body.Replace("*cbol*",carton.CBOL.Trim());
        body = body.Replace("*po*",carton.PO.Trim());
        body = body.Replace("*pro*","");
        body = body.Replace("*shipment*",carton.VK.Trim());
        body = body.Replace("*bol*",carton.BL.ToString());
        body = body.Replace("*weight*",carton.WT.ToString());
        body = body.Replace("*label*",carton.LBL.ToString());
        email.Body = body;
        if(email.Body.Length > 0) {
            SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
            smtpClient.Send(email);
            retValue = true;
        }
        return retValue;
    }
    public bool SendPODRequestConfirm(MembershipUser user,TrackingDS.CartonDetailTableRow carton,string substore) {
        //
        bool retValue = false;
        MailMessage email = new MailMessage(this.mEmailFrom,user.Email);
        email.Subject = "POD Request Confirmation";
        email.BodyEncoding = System.Text.Encoding.UTF8;
        email.IsBodyHtml = true;
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_POD_REQCONFIRM);
        body = body.Replace("*user*",user.UserName);
        body = body.Replace("*email*",user.Email);
        body = body.Replace("*store*",substore.Length > 0 ? substore : carton.S.ToString());
        body = body.Replace("*storename*",carton.SNM.Trim());
        body = body.Replace("*carton*",carton.CTN.Trim());
        body = body.Replace("*client*",carton.CL.Trim() + "-" + carton.CLNM.Trim());
        body = body.Replace("*vendor*",carton.V.Trim() + "-" + carton.VNM.Trim());
        body = body.Replace("*pickupdate*",carton.PUD.Trim());
        body = body.Replace("*scheduleddelivery*",carton.SCD.Trim());
        body = body.Replace("*shipment*",carton.VK.Trim());
        body = body.Replace("*bol*",carton.BL.ToString());
        body = body.Replace("*tl*",carton.TL.Trim());
        body = body.Replace("*label*",carton.LBL.ToString());
        body = body.Replace("*po*",carton.PO.Trim());
        body = body.Replace("*weight*",carton.WT.ToString());
        email.Body = body;
        if(email.Body.Length > 0) {
            SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
            smtpClient.Send(email);
            retValue = true;
        }
        return retValue;
    }
    #region Local Services: getHTMLBody()
    private string getHTMLBody(string filePath) {
        //
        string bodyText = "";
        FileInfo mailFileInfo = new FileInfo(filePath);
        StreamReader reader = mailFileInfo.OpenText();
        bodyText = reader.ReadToEnd();
        return bodyText;
    }
    #endregion
}
