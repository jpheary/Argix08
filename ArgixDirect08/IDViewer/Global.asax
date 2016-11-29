<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e) {
        //Code that runs on application startup
    }
    void Application_End(object sender, EventArgs e) {
        //Code that runs on application shutdown
    }
    void Application_Error(object sender,EventArgs e) {
        //Code that runs when an unhandled error occurs
        //NOTES:
        //  When transferring control to an error page, use Transfer()()() method. This preserves 
        //  the current context so that error information from GetLastError()()() is available.
        //  After handling an error, clear it by calling ClearError().
        Exception exc = Server.GetLastError();
        if(exc == null) {
            Server.ClearError();
        }
        else if(exc != null && exc.GetType() == typeof(System.Web.HttpException)) {
            //Http processing exception
            Server.Transfer("~/Error.aspx");
        }
        else if(exc != null && exc.GetType() == typeof(System.Web.HttpCompileException)) {
            //Compiler exception
            Server.Transfer("~/Error.aspx");
        }
        else if(exc != null && exc.GetType() == typeof(HttpUnhandledException)) {
            Response.Write("<h2>Oops!</h2>\n");
            if(exc != null) {
                Response.Write("<p>" + exc.Message + "</p>\n");
                if(exc.InnerException != null) {
                    Response.Write("<p>" + exc.InnerException.Message + "</p>\n");
                    if(exc.InnerException.InnerException != null) {
                        Response.Write("<p>" + exc.InnerException.InnerException.Message + "</p>\n");
                        if(exc.InnerException.InnerException.InnerException != null) {
                            Response.Write("<p>" + exc.InnerException.InnerException.InnerException.Message + "</p>\n");
                        }
                    }
                }
            }
            else
                Response.Write("<p>Unknown error</p>\n");
            Response.Write("<br /><br />");
            Response.Write("Return to the <a href='../Default.aspx'>" + "Home</a>\n");
            Server.ClearError();
        }
        else {
            //ExceptionUtility.LogException(exc, "DefaultPage");
            //ExceptionUtility.NotifySystemOps(exc);
            Server.Transfer("~/Error.aspx");
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

    void Trace(string message,Argix.LogLevel level) {
        //Trace
        Argix.TraceMessage m = new Argix.TraceMessage();
        m.Name = "Argix08";
        m.Source = "Argix08.IDViewer";
        m.User = HttpContext.Current.User != null ? HttpContext.Current.User.Identity.Name : "";
        m.Computer = Environment.MachineName;
        m.LogLevel = level;
        m.Message = message;
        Argix.KronosProxy fp = new Argix.KronosProxy();
        fp.WriteLogEntry(m);
    }
</script>
