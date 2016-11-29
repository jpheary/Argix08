<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e) {
        //Code that runs on application startup
        //Trace("Argix08.EnterpriseService web application startup",LogSvc.LogLevel.Information);
    }
    void Application_End(object sender, EventArgs e) {
        //Code that runs on application shutdown
        //Trace("Argix08.EnterpriseService web application shutdown",LogSvc.LogLevel.Information);
    }
    void Application_Error(object sender,EventArgs e) {
        //Code that runs when an unhandled error occurs
        //NOTES:
        //  When transferring control to an error page, use Transfer() method. This preserves 
        //  the current context so that error information from GetLastError()()() is available.
        //  After handling an error, clear it by calling ClearError().
        Exception exc = Server.GetLastError();
        if(exc != null && exc.GetType() == typeof(System.Web.HttpException)) {
            //Http processing exception
            Server.Transfer("Error.aspx");
        }
        else if(exc != null && exc.GetType() == typeof(System.Web.HttpCompileException)) {
            //Compiler exception
            Server.Transfer("Error.aspx");
        }
        else if(exc != null && exc.GetType() == typeof(HttpUnhandledException)) {
            //Generic exception
            Server.Transfer("Error.aspx");
        }
        else {
            //For other kinds of errors give the user some information but stay on the default page
            Response.Write("<h2>Enterprise Service Error</h2>\n");
            if(exc != null)
                Response.Write("<p>" + exc.ToString() + "</p>\n");
            else
                Response.Write("<p>Unknown error</p>\n");

            //Log, notify system operators, and clear
            //ExceptionUtility.LogException(exc, "DefaultPage");
            //ExceptionUtility.NotifySystemOps(exc);
            //Trace(exc.Message,LogSvc.LogLevel.Error);
            Server.ClearError();
        }
    }
    
    void Session_Start(object sender, EventArgs e)  {
        //Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e) {
        //Code that runs when a session ends. 
        //Note: The Session_End event is raised only when the sessionstate mode is set to InProc in Web.config. If session mode is set to StateServer or SQLServer, the event is not raised.
        Session.Clear();
    }

    //void Trace(string message,LogSvc.LogLevel level) {
    //    //Trace
    //    LogSvc.TraceMessage m = new LogSvc.TraceMessage();
    //    m.Name = "Argix08";
    //    m.Source = "Argix08.CRGService";
    //    m.User = Environment.UserName;
    //    m.Computer = Environment.MachineName;
    //    m.LogLevel = level;
    //    m.Message = message;
    //    LogSvc.AppLoggerClient appLog = new LogSvc.AppLoggerClient();
    //    try {
    //        appLog.Write(m);
    //        appLog.Close();
    //    }
    //    catch(TimeoutException ex) { appLog.Abort(); }
    //    catch(System.ServiceModel.CommunicationException ex) { appLog.Abort(); }
    //    catch(Exception ex) { appLog.Abort(); }
    //    finally { appLog.Close(); }
    //}
</script>
