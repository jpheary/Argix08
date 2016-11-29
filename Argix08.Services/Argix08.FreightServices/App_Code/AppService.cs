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
        private string mConnectionID="";
        private LogLevel mLogLevelFloor=LogLevel.None;

        private const string USP_CONFIG_GETLIST = "uspToolsConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        private const string USP_LOG_NEW = "uspLogEntryNew";

        //Interface
        public AppService(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
            this.mLogLevelFloor = (LogLevel)Convert.ToInt32(ConfigurationManager.AppSettings["LogLevelFloor"]);
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            UserConfiguration config=null;
            try {
                //
                config = new UserConfiguration(application);
                DataSet ds = fillDataset(USP_CONFIG_GETLIST,TBL_CONFIGURATION,new object[] { application });
                if(ds != null && ds.Tables[TBL_CONFIGURATION] != null) {
                    for(int i=0;i<ds.Tables[TBL_CONFIGURATION].Rows.Count;i++) {
                        string userName = ds.Tables[TBL_CONFIGURATION].Rows[i]["PCName"].ToString();
                        string key = ds.Tables[TBL_CONFIGURATION].Rows[i]["Key"].ToString();
                        string value = ds.Tables[TBL_CONFIGURATION].Rows[i]["Value"].ToString();
                        if(!config.ContainsKey(key)) {
                            if(userName.ToLower() == UserConfiguration.USER_DEFAULT.ToLower())
                                config.Add(key,value);
                            else {
                                for(int j=0;j<usernames.Length;j++) {
                                    if(userName.ToLower() == usernames[j].ToLower())
                                        config.Add(key,value);
                                }
                            }
                        }
                        else {
                            for(int j=0;j<usernames.Length;j++) {
                                if(userName.ToLower() == usernames[j].ToLower())
                                    config[key] = value;
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<ConfigurationFault>(new ConfigurationFault(new ApplicationException("Unexpected error while reading the user configuration.",ex))); }
            return config;
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
        
        #region Data Services: fillDataset(), executeNonQuery()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        #endregion
    }
}
