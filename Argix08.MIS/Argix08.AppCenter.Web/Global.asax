<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) {
        //Code that runs on application startup
    }
    void Application_End(object sender, EventArgs e) {
        //Code that runs on application shutdown
    }
        
    void Application_Error(object sender, EventArgs e) { 
        //Code that runs when an unhandled error occurs
    }
    
    void Session_Start(object sender, EventArgs e) {
        //Code that runs when a new session is started
    }
    void Session_End(object sender, EventArgs e) {
        //Code that runs when a session ends. 
        //NOTE: The Session_End event is raised only when the sessionState mode = 'InProc'
        //      in the Web.config file. If sessionState mode = 'StateServer' or 'SQLServer', 
        //      the event is not raised.
    }
       
</script>
