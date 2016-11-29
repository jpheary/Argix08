<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(Object sender, EventArgs e) {
        //Code that runs on application startup
        //Read web.config parameters once at application startup
        Application.Add("TrackingNumbersMax",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["TrackingNumbersMax"]));
        Application.Add("TrackingCharsMax",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["TrackingCharsMax"]));
        Application.Add("CartonLenMin",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["CartonLenMin"]));
        Application.Add("CartonLenMax",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["CartonLenMax"]));
        Application.Add("ArgixLabelLen",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ArgixLabelLen"]));

        Application.Add("DateDaysBack",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["DateDaysBack"]));
        Application.Add("DateDaysForward",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["DateDaysForward"]));
        Application.Add("DateDaysSpread",int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["DateDaysSpread"]));

        Application.Add("ImageService",System.Web.Configuration.WebConfigurationManager.AppSettings["Imaging.imageservice"]);
    }
    void Application_End(Object sender, EventArgs e) {
        //Code that runs on application shutdown
    }
    void Application_Error(object sender,EventArgs e) {
        //Code that runs when an unhandled error occurs
        //NOTES:
        //  When transferring control to an error page, use the Transfer() method. This preserves 
        //  the current context so that error information from GetLastError() is available.
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
            //Page type no exception handler
            Response.Write("<h2>Page Error</h2>\n");
            if(exc != null) {
                Response.Write("<p>" + exc.Message + "</p>\n");
                if(exc.InnerException != null) {
                    Response.Write("<p>" + exc.InnerException.Message + "</p>\n");
                    if(exc.InnerException.InnerException != null) {
                        Response.Write("<p>" + exc.InnerException.InnerException.Message + "</p>\n");
                    }
                }
            }
            else
                Response.Write("<p>Unknown error.</p>\n");
            Response.Write("<br /><br />");
            Response.Write("Return to the <a href='../Members/Default.aspx'>" + "Tracking page</a>\n");
            Server.ClearError();
        }
        else {
            Server.Transfer("~/Error.aspx");
        }
    }

    void Session_Start(Object sender, EventArgs e) {
        //Code that runs when a new session is started
        Session.Add("TrackBy","");
        Session.Add("SubStore","");
        Session.Add("TrackingSearch","");
        Session.Add("TrackingSummary","");
        Session.Add("TrackingDetail","");
        Session.Add("StoreSummary", "");
        Session.Add("StoreDetail","");
    }
    void Session_End(Object sender, EventArgs e) {
        //Code that runs when a session ends
        //Note: The Session_End event is raised only when the sessionstate mode
        //is set to InProc in the web.config file. If session mode is set to StateServer 
        //or SQLServer, the event is not raised.
        Session.Clear();
    }
</script>
