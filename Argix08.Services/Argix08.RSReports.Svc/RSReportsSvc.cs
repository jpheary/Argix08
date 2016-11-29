using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using Argix.SQLReportServer;

namespace Argix.Terminals {
    //
    public partial class RSReportsSvc: ServiceBase {
        //Members
        private System.Timers.Timer mTimer = null;
        private bool mStartup = true;
        private bool mShutdown = false;
        private const string USP_EVENTS = "uspRouteEventGetList",TBL_EVENTS = "EventTable";
        private const string PARAM_ROUTEDATE = "RouteDate";
        private const string PARAM_ROUTECLASS = "RouteClass";

        //Interface
        public RSReportsSvc() {
            //Constructor
            try {
                //Required by the Windows.Forms Component Designer.
                InitializeComponent();

                //Read configuration parameters
                try {
                    SvcLog.Enabled = Argix.Properties.Settings.Default.LogOn;
                    SvcLog.Filepath =  Argix.Properties.Settings.Default.LogPath;
                    SvcLog.FilesMax =  Argix.Properties.Settings.Default.LogFilesMax;
                }
                catch(Exception ex) { reportError(new ApplicationException("Unexpected error while reading configuration parameters.",ex)); }

                //Create services
                this.mTimer = new System.Timers.Timer();
                this.mTimer.Enabled = false;
                this.mTimer.Elapsed += new System.Timers.ElapsedEventHandler(onTimerElapsed);
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error while creating new RSReportsSvc instance.",ex)); }
        }
        #region Service Overrides: OnStart(), OnStop(), OnPause(), OnContinue()
        public void Start() { checkPublications(); }
        //protected override void OnStart(string[] args) {
        //    //Set things in motion so your service can do its work
        //    try {
        //        //Timed or one shot
        //        this.mStartup = true;
        //        this.mTimer.Interval = 100;
        //        this.mTimer.Enabled = true;
        //    }
        //    catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnStart.",ex)); }
        //}
        //protected override void OnStop() {
        //    //Stop this service
        //    try {
        //        //Stop the service
        //        this.mShutdown = true;
        //        this.mTimer.Enabled = false;
        //        this.mTimer.Interval = 100;
        //        this.mTimer.Enabled = true;
        //    }
        //    catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnStop.",ex)); }
        //}
        //protected override void OnPause() {
        //    //Pause this service
        //    try {
        //        //Stop the service
        //        this.mTimer.Enabled = false;
        //    }
        //    catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnPause.",ex)); }
        //}
        //protected override void OnContinue() {
        //    //Continue this service
        //    try {
        //        //Stop the service
        //        this.mTimer.Interval = getTimerInterval();
        //        this.mTimer.Enabled = true;
        //    }
        //    catch(Exception ex) { reportError(new ApplicationException("Unexpected error in OnContinue.",ex)); }
        //}
        #endregion
        private void onTimerElapsed(object sender,System.Timers.ElapsedEventArgs e) {
            //Event handler for self timed operation
            try {
                if(this.mStartup) {
                    //Set startup condition- create utility service
                    this.mStartup = false;
                    this.mTimer.Interval = getTimerInterval();
                }
                if(!this.mShutdown) {
                    //Run the service
                    checkPublications();
                }
                else {
                    //Shutdown condition
                    this.mTimer.Enabled = false;
                }
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in timer interval operations.",ex)); }
        }
        private void checkPublications() {
            //
            SqlDataAdapter adapter = new SqlDataAdapter(USP_EVENTS,global::Argix.Properties.Settings.Default.SQLConnection);
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            adapter.TableMappings.Add("Table",TBL_EVENTS);
            EventsDS ds = new EventsDS();
            adapter.Fill(ds,TBL_EVENTS);
            if(ds.Tables[TBL_EVENTS].Rows.Count > 0) {
                //Fire subscriptions and update table
                for(int i=0;i<ds.EventTable.Rows.Count;i++) {
                    try {
                        bool published=false;
                        if(ds.EventTable[i].Routing_Event == "A" && ds.EventTable[i].IsNotifiedNull())
                            published = publish("/Terminals/Roadshow Routes Auto",ds.EventTable[i].Routing_Date,ds.EventTable[0].Routing_Class);
                        else if(ds.EventTable[i].Routing_Event == "E" && ds.EventTable[i].IsNotifiedNull())
                            published = publish("/Terminals/Roadshow Routes",ds.EventTable[i].Routing_Date,ds.EventTable[0].Routing_Class);

                        if(published) {
                            //Update
                        }
                    }
                    catch { }
                }
            }
        }
        private bool publish(string reportName,DateTime routeDate,string routeClass) {
            //
            bool published=false;
            bool subscriptionsExist=false;
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

                    //Update subscription to "TimedSubscription" and a future date so it does not run on a snapshot update
                    rs.SetSubscriptionProperties(sub.SubscriptionID,extSettings,desc,"TimedSubscription",setMatchData(),paramValues);

                    //Setup applicable subscriptions for snapshot update
                    if(paramValues != null) {
                        rs.SetSubscriptionProperties(sub.SubscriptionID,extSettings,desc,"SnapshotUpdated",null,paramValues);
                        if(!subscriptionsExist) subscriptionsExist = true;
                    }
                }

                if(subscriptionsExist) {
                    //Set report parameters
                    ItemParameter[] itemParams = rs.GetItemParameters(reportName,null,false,null,null);
                    for(int i=0;i<itemParams.Length;i++) {
                        if(itemParams[i].Name == PARAM_ROUTEDATE) itemParams[i].DefaultValues = new string[1] { routeDate.ToString("yyyy-MM-dd") };
                        if(itemParams[i].Name == PARAM_ROUTECLASS) itemParams[i].DefaultValues = new string[1] { routeClass };
                    }
                    rs.SetItemParameters(reportName,itemParams);

                    //Update the snapshot so that subscriptions will be executed
                    rs.UpdateItemExecutionSnapshot(reportName);
                    published = true;
                }
            }
            return published;
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
        private int getTimerInterval() {
            //Read configuration timer parameter
            int timerInterval=600000;
            try {
                timerInterval = Convert.ToInt32(ConfigurationManager.AppSettings.Get("TimerInterval"));
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error in reading timer interval.",ex)); }
            return timerInterval;
        }
        private void reportError(Exception ex) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                EventLog.WriteEntry("RSReportsSvc",ex.ToString(),EventLogEntryType.Error);
            }
            catch(Exception _ex) { EventLog.WriteEntry("RSReportsSvc",_ex.ToString(),EventLogEntryType.Error); }
        }
    }
}
