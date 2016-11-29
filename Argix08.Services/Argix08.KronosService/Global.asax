<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e) {
        // Code that runs on application startup
    }
    void Application_End(object sender, EventArgs e) {
        //  Code that runs on application shutdown
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
        else if(exc == null) {
            Server.ClearError();
        }
        else {
            //For other kinds of errors give the user some information but stay on the default page
            Response.Write("<h2>Global Page Error</h2>\n");
            if(exc != null)
                Response.Write("<p>" + exc.ToString() + "</p>\n");
            else
                Response.Write("<p>Unknown error</p>\n");
            Response.Write("Return to the <a href='AppLoggerEditor.aspx'>" + "App Logger Editor page</a>\n");

            //Log, notify system operators, and clear
            //ExceptionUtility.LogException(exc, "DefaultPage");
            //ExceptionUtility.NotifySystemOps(exc);
            Server.ClearError();
        }
    }

    void Session_Start(object sender, EventArgs e) {
        //Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e) {
        //Code that runs when a session ends. 
        //Note: The Session_End event is raised only when the sessionstate mode is set to InProc in the Web.config file. If session mode is set to StateServer or SQLServer, the event is not raised.
    }
</script>
