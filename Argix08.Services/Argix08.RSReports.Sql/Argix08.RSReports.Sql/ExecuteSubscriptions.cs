using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Threading;
using Microsoft.SqlServer.Server;
using Argix.SQLReportSever;

public partial class StoredProcedures {
    //Notes on deployment
    /*
    exec sp_configure 'clr enabled', '1'
    reconfigure
    
    ALTER DATABASE RSReport SET TRUSTWORTHY ON;
    
    EXEC sp_changedbowner 'sa', false;
     
    sp_dbcmptlevel 'RSReport', 90

    CREATE PROCEDURE uspExecuteSubscriptions 
    AS EXTERNAL NAME RSReports.StoredProcedures.ExecuteSubscriptions
    GO
     */
    private const string PARAM_ROUTEDATE = "RouteDate";
    private const string PARAM_ROUTECLASS = "RouteClass";
    private delegate void SubscriptionEventHandler(string reportName,DateTime routeDate,string routeClass);

    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void ExecuteSubscriptions(string reportName,DateTime routeDate,string routeClass) {
        //
        SubscriptionEventHandler publisher = new SubscriptionEventHandler(OnPublish);
        publisher.BeginInvoke(reportName,routeDate,routeClass,null,null);
    }
    private static void OnPublish(string reportName,DateTime routeDate,string routeClass) {
        ReportingService2010 rs = new ReportingService2010();
        rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

        //Request all subscriptions for the specified report
        Subscription[] subscriptions = rs.ListSubscriptions(reportName);
        if(subscriptions != null) {
            //Enumerate all subscriptions
            for(int i=0;i<subscriptions.Length;i++) {
                //Get subscription properties
                Subscription sub = subscriptions[i];
                ExtensionSettings extSettings=null;
                ActiveState active=null;
                string desc="",status="",eventType="",matchData="";
                ParameterValue[] paramValues=null;
                rs.GetSubscriptionProperties(sub.SubscriptionID,out extSettings,out desc,out active,out status,out eventType,out matchData,out paramValues);
                rs.SetSubscriptionProperties(sub.SubscriptionID,extSettings,desc,"SnapshotUpdated",null,paramValues);
            }

            //Set report parameters and update the snapshot so that subscriptions will be executed
            ItemParameter[] itemParams = rs.GetItemParameters(reportName,null,false,null,null);
            for(int i=0;i<itemParams.Length;i++) {
                if(itemParams[i].Name == PARAM_ROUTEDATE) itemParams[i].DefaultValues = new string[1] { routeDate.ToString("yyyy-MM-dd") };
                if(itemParams[i].Name == PARAM_ROUTECLASS) itemParams[i].DefaultValues = new string[1] { routeClass };
            }
            rs.SetItemParameters(reportName,itemParams);
            rs.UpdateItemExecutionSnapshot(reportName);
        }
    }

    private static string setMatchData() {
        //Set the schedule in the future so that subscription does not execute. 
        //Alternate way to disable a schedule without losing default parameters.
        //<ScheduleDefinition>
        //	<StartDateTime>2008-08-23T09:00:00-08:00</StartDateTime>
        //	<WeeklyRecurrence>
        //		<WeeksInterval>1</WeeksInterval>
        //		<DaysOfWeek><Monday>true</Monday></DaysOfWeek>
        //	</WeeklyRecurrence>
        //</ScheduleDefinition>
        string startDate = DateTime.Today.AddYears(1).ToString("yyyy-MM-dd") + "T09:00:00-08:00";
        string scheduleXml = @"<ScheduleDefinition>";
        scheduleXml += @"<StartDateTime>" + startDate + "</StartDateTime><WeeklyRecurrence><WeeksInterval>1</WeeksInterval>";
        scheduleXml += @"<DaysOfWeek><Monday>True</Monday></DaysOfWeek>";
        scheduleXml += @"</WeeklyRecurrence></ScheduleDefinition>";
        return scheduleXml;
    }
};
