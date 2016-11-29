using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

//
namespace Argix {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class AppService:IAppService {
        //Members
        private LogLevel mLogLevelFloor=LogLevel.None;

        private const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        private const string USP_CONFIG_GETLIST = "uspToolsConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        private const string USP_CONFIG_CREATE = "uspToolsConfigurationCreate";
        private const string USP_CONFIG_UPDATE = "uspToolsConfigurationUpdate";
        public const string USER_ALL = "All";
        
        private const string USP_LOG_GETLIST = "uspToolsTraceLogGetList",TBL_LOG = "AppLogTable";
        private const string USP_LOG_NEW = "uspLogEntryNew";
        private const string USP_LOG_DELETE = "uspToolsTraceLogDelete";
        public const string SRCS_ALL = "All";

        //Interface
        public AppService() {
            //Constructor
            this.mLogLevelFloor = (LogLevel)Convert.ToInt32(ConfigurationManager.AppSettings["LogLevelFloor"]);
        }
        public TerminalInfo GetTerminalInfo() {
            //Get information about the local terminal for this service
            TerminalInfo info = new TerminalInfo();
            info.Connection = DatabaseFactory.CreateDatabase("SQLConnection").ConnectionStringWithoutCredentials;
            DataSet ds = fillDataset(USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[]{});
            if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
            }
            return info;
        }
        public Hashtable GetApplications() {
            //Get a list of applications
            Hashtable apps=null;
            try {
                apps = new Hashtable();
                AppConfigDS config = new AppConfigDS();
                DataSet ds = fillDataset(USP_CONFIG_GETLIST,TBL_CONFIGURATION,new object[] { null });
                if(ds!=null)
                    config.Merge(ds);
                for(int i=0;i<config.ConfigTable.Rows.Count;i++) {
                    string app = config.ConfigTable.Rows[i]["Application"].ToString();
                    if(app.Length > 0 && !apps.ContainsKey(app)) apps.Add(app,app);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading applications.",ex); }
            return apps;
        }
        public Hashtable GetUsers(string application) {
            //Get user list for an application
            Hashtable users=null;
            try {
                users = new Hashtable();
                users.Add(USER_ALL,USER_ALL);
                AppConfigDS config = new AppConfigDS();
                DataSet ds = fillDataset(USP_CONFIG_GETLIST,TBL_CONFIGURATION,new object[] { application });
                if(ds!=null)
                    config.Merge(ds);
                for(int i=0;i<config.ConfigTable.Rows.Count;i++) {
                    string user = config.ConfigTable.Rows[i]["PCName"].ToString();
                    if(!users.ContainsKey(user)) users.Add(user,user);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading users from the application configuration.",ex); }
            return users;
        }
        public ApplicationConfiguration GetConfiguration(string application,string username) {
            //Get configuration data for the specified application
            ApplicationConfiguration config=null;
            try {
                config = new ApplicationConfiguration(application);
                AppConfigDS configDS = new AppConfigDS();
                DataSet ds = fillDataset(USP_CONFIG_GETLIST,TBL_CONFIGURATION,new object[] { application });
                if(ds!=null) {
                    configDS.Merge(ds);
                    for(int i=0;i<configDS.ConfigTable.Rows.Count;i++) {
                        ConfigurationEntry entry = new ConfigurationEntry(configDS.ConfigTable[i]);
                        if(username.ToLower() == USER_ALL.ToLower() || entry.UserName.ToLower() == username.ToLower()) config.Add(entry);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading the user configuration.",ex); }
            return config;
        }
        
        public UserConfiguration GetUserConfiguration(string application,string username) {
            //Get configuration data for the specified application and username
            UserConfiguration config=null;
            try {
                config = new UserConfiguration(application);
                AppConfigDS configDS = new AppConfigDS();
                DataSet ds = fillDataset(USP_CONFIG_GETLIST,TBL_CONFIGURATION,new object[] { application });
                if(ds!=null) {
                    configDS.Merge(ds);
                    for(int i=0;i<configDS.ConfigTable.Rows.Count;i++) {
                        ConfigurationEntry entry = new ConfigurationEntry(configDS.ConfigTable[i]);
                        if(!config.ContainsKey(entry.Key)) {
                            if(entry.UserName.ToLower() == UserConfiguration.USER_DEFAULT.ToLower() || entry.UserName.ToLower() == username.ToLower())
                                config.Add(entry.Key,entry.Value);
                        }
                        else {
                            if(entry.UserName.ToLower() == username.ToLower())
                                config[entry.Key] = entry.Value;
                        }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<ConfigurationFault>(new ConfigurationFault(new ApplicationException("Unexpected error while reading the user configuration.",ex))); }
            return config;
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            UserConfiguration config=null;
            try {
                //
                config = new UserConfiguration(application);
                AppConfigDS configDS = new AppConfigDS();
                DataSet ds = fillDataset(USP_CONFIG_GETLIST,TBL_CONFIGURATION,new object[] { application });
                if(ds!=null) {
                    configDS.Merge(ds,true,MissingSchemaAction.Ignore);
                    for(int i=0;i<configDS.ConfigTable.Rows.Count;i++) {
                        ConfigurationEntry entry = new ConfigurationEntry(configDS.ConfigTable[i]);
                        if(!config.ContainsKey(entry.Key)) {
                            if(entry.UserName.ToLower() == UserConfiguration.USER_DEFAULT.ToLower())
                                config.Add(entry.Key,entry.Value);
                            else {
                                for(int j=0;j<usernames.Length;j++) {
                                    if(entry.UserName.ToLower() == usernames[j].ToLower())
                                        config.Add(entry.Key,entry.Value);
                                }
                            }
                        }
                        else {
                            for(int j=0;j<usernames.Length;j++) {
                                if(entry.UserName.ToLower() == usernames[j].ToLower())
                                    config[entry.Key] = entry.Value;
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<ConfigurationFault>(new ConfigurationFault(new ApplicationException("Unexpected error while reading the user configuration.",ex))); }
            return config;
        }

        public bool CreateConfigurationEntry(ConfigurationEntry entry) {
            //Create a new configuration entry
            bool created=false;
            try {
                created = executeNonQuery(USP_CONFIG_CREATE,new object[] { entry.Application,entry.UserName,entry.Key,entry.Value,entry.Security });
            }
            catch(Exception ex) { throw new FaultException<ConfigurationFault>(new ConfigurationFault(new ApplicationException("Unexpected error while creating new configuration entry.",ex))); }
            return created;
        }
        public bool CreateConfigurationEntry(string application, string username, string key, string value, string security) {
            //Create a new configuration entry
            bool created=false;
            try {
                created = executeNonQuery(USP_CONFIG_CREATE,new object[] { application,username,key,value,security });
            }
            catch(Exception ex) { throw new FaultException<ConfigurationFault>(new ConfigurationFault(new ApplicationException("Unexpected error while creating new configuration entry.",ex))); }
            return created;
        }
        public bool UpdateConfigurationEntry(ConfigurationEntry entry) {
            //Create a new configuration entry
            bool updated=false;
            try {
                updated = executeNonQuery(USP_CONFIG_UPDATE,new object[] { entry.Application,entry.UserName,entry.Key,entry.Value,entry.Security });
            }
            catch(Exception ex) { throw new FaultException<ConfigurationFault>(new ConfigurationFault(new ApplicationException("Unexpected error while updating existing configuration entry.",ex))); }
            return updated;
        }
        public bool UpdateConfigurationEntry(string application,string username,string key,string value,string security) {
            //Create a new configuration entry
            bool updated=false;
            try {
                updated = executeNonQuery(USP_CONFIG_UPDATE,new object[] { application,username,key,value,security });
            }
            catch(Exception ex) { throw new FaultException<ConfigurationFault>(new ConfigurationFault(new ApplicationException("Unexpected error while updating existing configuration entry.",ex))); }
            return updated;
        }

        public SortedList GetLogNames() {
            //Get a list of logs
            SortedList logs=null;
            try {
                logs = new SortedList();
                DataSet ds = fillDataset(USP_LOG_GETLIST,TBL_LOG,new object[] { null });
                if(ds!=null) {
                    AppLogDS logDS = new AppLogDS();
                    logDS.Merge(ds);
                    for(int i=0;i<logDS.AppLogTable.Rows.Count;i++) {
                        string log = logDS.AppLogTable.Rows[i]["Name"].ToString();
                        if(log.Length > 0 && !logs.ContainsKey(log)) {
                            logs.Add(log,log);
                        }
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading logs.",ex); }
            return logs;
        }
        public SortedList GetLogSources(string logName) {
            //Get a list of log sources fro the specified log name
            SortedList srcs=null;
            try {
                srcs = new SortedList();
                srcs.Add(SRCS_ALL,SRCS_ALL);
                DataSet ds = fillDataset(USP_LOG_GETLIST,TBL_LOG,new object[] { logName });
                if(ds!=null) {
                    AppLogDS logDS = new AppLogDS();
                    logDS.Merge(ds);
                    for(int i=0;i<logDS.AppLogTable.Rows.Count;i++) {
                        string src = logDS.AppLogTable.Rows[i]["Source"].ToString();
                        if(src.Length > 0 && !srcs.ContainsKey(src)) srcs.Add(src,src);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading logs.",ex); }
            return srcs;
        }
        public AppLog GetAppLog(string logName) {
            //Get the application log for the specified log name
            AppLog log=null;
            try {
                log = new AppLog(logName);
                AppLogDS logDS = new AppLogDS();
                DataSet ds = fillDataset(USP_LOG_GETLIST,TBL_LOG,new object[] { logName });
                if(ds!=null) {
                    logDS.Merge(ds);
                    for(int i=0;i<logDS.AppLogTable.Rows.Count;i++) {
                        log.Add(new TraceMessage(logDS.AppLogTable[i]));
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading the application log.",ex); }
            return log;
        }
        public AppLog GetAppLog(string logName,string source) {
            //Get the application log for the specified log name
            AppLog log=null;
            try {
                log = new AppLog(logName);
                AppLogDS logDS = new AppLogDS();
                DataSet ds = fillDataset(USP_LOG_GETLIST,TBL_LOG,new object[] { logName });
                if(ds!=null) {
                    logDS.Merge(ds);
                    for(int i=0;i<logDS.AppLogTable.Rows.Count;i++) {
                        TraceMessage msg = new TraceMessage(logDS.AppLogTable[i]);
                        if(source == SRCS_ALL || msg.Source.Trim() == source.Trim()) log.Add(msg);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading the application log.",ex); }
            return log;
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            try {
                if(m.LogLevel >= this.mLogLevelFloor) {
                    string message = (m.Message != null) ? m.Message : "";
                    message = message.Replace("\\n"," ");
                    message = message.Replace("\r","");
                    message = message.Replace("\n","");
                    string category = (m.Category != null) ? m.Category : "";
                    string _event = (m.Event != null) ? m.Event : "";
                    string keyData1 = (m.Keyword1 != null) ? m.Keyword1 : "";
                    string keyData2 = (m.Keyword2 != null) ? m.Keyword2 : "";
                    string keyData3 = (m.Keyword3 != null) ? m.Keyword3 : "";

                    executeNonQuery(USP_LOG_NEW,new object[] { m.Name,Convert.ToInt32(m.LogLevel),DateTime.Now,m.Source,category,_event,m.User,m.Computer,keyData1,keyData2,keyData3,message });
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while logging trace message.",ex); }
        }
        public bool DeleteLogEntry(long id) {
            //Delete this log entry
            bool ret=false;
            try {
                ret = executeNonQuery(USP_LOG_DELETE,new object[] { id });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while deleting log entry- " + ex.Message.Replace("'",""),ex); }
            return ret;
        }
        
        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        private object executeNonQueryWithReturn(string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase("SQLConnection");
                DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
                ret = db.ExecuteNonQuery(cmd);

                //Find the output parameter and return its value
                foreach(DbParameter param in cmd.Parameters) {
                    if((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                        ret = param.Value;
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}
