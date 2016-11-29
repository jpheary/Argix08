<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e) {
        //Code that runs on application startup
        Trace("Argix08.IssueMgtServiceClient web application startup",Argix.Support.LogLevel.Information);
    }
    void Application_End(object sender, EventArgs e) {
        //Code that runs on application shutdown
        Trace("Argix08.IssueMgtServiceClient web application shutdown",Argix.Support.LogLevel.Information);
    }
    void Application_Error(object sender,EventArgs e) {
        //Code that runs when an unhandled error occurs
        //NOTES:
        //  When transferring control to an error page, use Transfer()()() method. This preserves 
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
            Response.Write("<h2>Global Page Error</h2>\n");
            if(exc != null)
                Response.Write("<p>" + exc.ToString() + "</p>\n");
            else
                Response.Write("<p>Unknown error</p>\n");
            Response.Write("Return to the <a href='IssueMgt.aspx'>" + "Issue Mgt page</a>\n");

            //Log, notify system operators, and clear
            //ExceptionUtility.LogException(exc, "DefaultPage");
            //ExceptionUtility.NotifySystemOps(exc);
            Trace(exc.Message,Argix.Support.LogLevel.Error);
            Server.ClearError();
        }
    }
    
    void Session_Start(object sender, EventArgs e)  {
        //Code that runs when a new session is started
        Argix.Support.AppServiceClient appConfig=null;
        try {
            Session.Add("AutoRefreshOn",true);
            Session.Add("IssueDaysBack",3);

            appConfig = new Argix.Support.AppServiceClient();
            Argix.Support.UserConfiguration config = appConfig.GetUserConfiguration2("IssueMgtServiceClient",new string[] { Environment.UserName,Environment.MachineName });
            Session["AutoRefreshOn"] = config["AutoRefreshOn"];
            Session["IssueDaysBack"] = config["IssueDaysBack"];
        }
        catch(TimeoutException ex) { appConfig.Abort(); Trace("Timeout when reading configuration data at session start",Argix.Support.LogLevel.Error); }
        catch(System.ServiceModel.FaultException<Argix.Support.ConfigurationFault> fex) { appConfig.Abort(); Trace("Configuration service error when reading configuration data at session start: " + fex.Message,Argix.Support.LogLevel.Error); }
        catch(System.ServiceModel.CommunicationException ex) { appConfig.Abort(); Trace("Communication error when reading configuration data at session start",Argix.Support.LogLevel.Error); }
        catch(Exception ex) { appConfig.Abort(); Trace("Unexpected error when reading configuration data at session start" + ex.Message,Argix.Support.LogLevel.Error); }
        finally { appConfig.Close(); }
    }
    void Session_End(object sender, EventArgs e) {
        //Code that runs when a session ends. 
        //Note: The Session_End event is raised only when the sessionstate mode is set to InProc in Web.config. If session mode is set to StateServer or SQLServer, the event is not raised.
        Session.Clear();
    }

    void Trace(string message,Argix.Support.LogLevel level) {
        //Trace
        Argix.Support.TraceMessage m = new Argix.Support.TraceMessage();
        m.Name = "Argix08";
        m.Source = "Argix08.IssueMgt.Web";
        m.User = Environment.UserName;
        m.Computer = Environment.MachineName;
        m.LogLevel = level;
        m.Message = message;
        Argix.Support.AppServiceClient appLog = new Argix.Support.AppServiceClient();
        try {
            appLog.WriteLogEntry(m);
            appLog.Close();
        }
        catch(TimeoutException ex) { appLog.Abort(); }
        catch(System.ServiceModel.CommunicationException ex) { appLog.Abort(); }
        catch(Exception ex) { appLog.Abort(); }
        finally { appLog.Close(); }
    }
</script>
