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
    
    private const string HTML_PASSWORD_RESET = "\\EmailMsgs\\PasswordReset.htm";
    private const string HTML_REGISTER_RECEIPT = "\\EmailMsgs\\RegistrationReceipt.htm";
    private const string HTML_REGISTER_REJECT = "\\EmailMsgs\\RejectRegistration.htm";
    private const string HTML_WELCOME = "\\EmailMsgs\\Welcome.htm";

    //Interface
    public EmailServices() {
        //Constructor
        //Read configuration values for this service
        this.mSMTPServer = WebConfigurationManager.AppSettings["SMTPServer"].ToString();
        this.mEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"].ToString();
        this.mEmailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"].ToString();
    }
    #region Accessors: SMTPServer, FromEmailAddress, AdminEmailAddress
    public string SMTPServer { get { return this.mSMTPServer; } }
    public string FromEmailAddress { get { return this.mEmailFrom; } }
    public string AdminEmailAddress { get { return this.mEmailAdmin; } }
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
