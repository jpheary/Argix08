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
            Response.Write("Return to the <a href='Default.aspx'>" + "Issue Mgt page</a>\n");
            Server.ClearError();
        }
        else {
            Server.Transfer("~/Error.aspx");
        }
    }

    void Session_Start(object sender,EventArgs e) {
        //Code that runs when a new session is started
            Session.Add("AutoRefreshOn",true);
            Session.Add("IssueDaysBack",3);
            Session.Add("AgentNumber", null);
            
            Session.Add("SearchTabVisible",false);
            if(Response.Cookies["SearchTabVisible"].Value != null && Response.Cookies["SearchTabVisible"].Value.ToLower() == "true")
                Session["SearchTabVisible"] = true;
            Session.Add("LastNewIssueTime",DateTime.Now);
            if(Response.Cookies["LastNewIssueTime"].Value != null)
                Session["LastNewIssueTime"] = DateTime.Parse(Response.Cookies["LastNewIssueTime"].Value);
    }
    void Session_End(object sender, EventArgs e) {
        //Code that runs when a session ends. 
        //Note: The Session_End event is raised only when the sessionstate mode is set to InProc in Web.config. If session mode is set to StateServer or SQLServer, the event is not raised.
        Response.Cookies.Add(new HttpCookie("SearchTabVisible",Session["SearchTabVisible"].ToString().ToLower()));
        Response.Cookies.Add(new HttpCookie("LastNewIssueTime",Session["LastNewIssueTime"].ToString()));
        Session.Clear();
    }
</script>
