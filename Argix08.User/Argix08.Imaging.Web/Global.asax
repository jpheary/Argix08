<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) {
        //Code that runs on application startup
    }
    void Application_End(object sender,EventArgs e) {
        //Code that runs on application shutdown
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
        else {
            //Generic exception
            Server.Transfer("Error.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e) {
        //Code that runs when a new session is started

    }
    void Session_End(object sender, EventArgs e) {
        //Code that runs when a session ends. 
        //Note: The Session_End event is raised only when the sessionstate mode
        //is set to InProc in the Web.config file. If session mode is set to StateServer 
        //or SQLServer, the event is not raised.

    }
    
</script>
