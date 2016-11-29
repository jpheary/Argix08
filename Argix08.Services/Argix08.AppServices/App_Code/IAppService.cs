using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix {
    //
    [ServiceContract(Namespace="http://Argix")]
    public interface IAppService {
        [OperationContract]
        TerminalInfo GetTerminalInfo();

        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string username);

        [OperationContract(Name="GetUserConfiguration2")]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);

        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);
    }

    [DataContract]
    public class TerminalInfo {
        private int mTerminalID=0;
        private string mNumber="",mDescription="",mConnection="";

        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public string Connection { get { return this.mConnection; } set { this.mConnection = value; } }
    }

    public class ApplicationConfiguration:BindingList<ConfigurationEntry> {
        //Members
        private string mApplication="";

        //Interface
        public ApplicationConfiguration() { }
        public ApplicationConfiguration(string application) { this.mApplication = application; }
        public string Application { get { return this.mApplication; } }
    }

    [CollectionDataContract]
    public class UserConfiguration:Hashtable {
        //Members
        private string mApplication="";
        public const string USER_DEFAULT = "Default";
        public const string USER_NEW = "<New User>";

        //Interface
        public UserConfiguration() { }
        public UserConfiguration(string application) { this.mApplication = application; }
    }

    [DataContract]
    public class ConfigurationEntry {
        //Members
        private string mApplication="",mUserName="",mKey="",mValue="",mSecurity="";
        public const string KEY_MISPASSWORD = "MISPassword";
        public const string KEY_READONLY = "ReadOnly";
        public const string KEY_TRACELEVEL = "TraceLevel";

        //Interface
        public ConfigurationEntry() { }
        public ConfigurationEntry(string application,string userName,string key,string value,string security) {
            //Constructor
            this.mApplication = application;
            this.mUserName = userName;
            this.mKey = key;
            this.mValue = value;
            this.mSecurity = security;
        }
        public ConfigurationEntry(AppConfigDS.ConfigTableRow entry) {
            //Constructor
            if(entry != null) {
                this.mApplication = entry.Application;
                this.mUserName = entry.PCName;
                this.mKey = entry.Key;
                this.mValue = entry.Value;
                this.mSecurity = entry.Security;
            }
        }
        [DataMember]
        public string Application { get { return this.mApplication; } set { this.mApplication = value; } }
        [DataMember]
        public string UserName { get { return this.mUserName; } set { this.mUserName = value; } }
        [DataMember]
        public string Key { get { return this.mKey; } set { this.mKey = value; } }
        [DataMember]
        public string Value { get { return this.mValue; } set { this.mValue = value; } }
        [DataMember]
        public string Security { get { return this.mSecurity; } set { this.mSecurity = value; } }
    }

    [DataContract]
    public class ConfigurationFault {
        private Exception _ex;
        public ConfigurationFault(Exception ex) { this._ex = ex; }

        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }

    [DataContract]
    public enum LogLevel {
        [EnumMember]
        None=0,
        [EnumMember]
        Debug=1,
        [EnumMember]
        Information=2,
        [EnumMember]
        Warning=3,
        [EnumMember]
        Error=4
    }

    public class AppLog:BindingList<TraceMessage> {
        //Members
        private string mName="";

        //Interface
        public AppLog() { }
        public AppLog(string name) { this.mName = name; }
        public string Name { get { return this.mName; } }
    }

    [DataContract]
    public class TraceMessage {
        //Members
        private long mID=0;
        private string mName="";
        private DateTime mDate;
        private string mCategory="",mEvent="";
        private string mUser=Environment.UserName,mComputer=Environment.MachineName;
        private string mSource="",mMessage="";
        private LogLevel mLogLevel=LogLevel.None;
        private string mKeyword1="",mKeyword2="",mKeyword3="";

        //Interface
        public TraceMessage(AppLogDS.AppLogTableRow logEntry) {
            if(logEntry != null) {
                if(!logEntry.IsIDNull()) this.mID = logEntry.ID;
                if(!logEntry.IsNameNull()) this.mName = logEntry.Name;
                if(!logEntry.IsLevelNull()) this.mLogLevel = (LogLevel)logEntry.Level;
                if(!logEntry.IsDateNull()) this.mDate = logEntry.Date;
                if(!logEntry.IsSourceNull()) this.mSource = logEntry.Source;
                if(!logEntry.IsCategoryNull()) this.mCategory = logEntry.Category;
                if(!logEntry.IsEventNull()) this.mEvent = logEntry.Event;
                if(!logEntry.IsUserNull()) this.mUser = logEntry.User;
                if(!logEntry.IsComputerNull()) this.mComputer = logEntry.Computer;
                if(!logEntry.IsKeyword1Null()) this.mKeyword1 = logEntry.Keyword1;
                if(!logEntry.IsKeyword2Null()) this.mKeyword2 = logEntry.Keyword2;
                if(!logEntry.IsKeyword3Null()) this.mKeyword3 = logEntry.Keyword3;
                if(!logEntry.IsMessageNull()) this.mMessage = logEntry.Message;
            }
        }
        public TraceMessage(string user,string computer,string message,string source,LogLevel logLevel) : this(user,computer,message,source,logLevel,"","","") { }
        public TraceMessage(string user,string computer,string message,string source,LogLevel logLevel,string keyData1,string keyData2,string keyData3) {
            //Constructor
            this.mUser = user;
            this.mComputer = computer;
            this.mMessage = message;
            this.mSource = source;
            this.mLogLevel = logLevel;
            this.mKeyword1 = keyData1;
            this.mKeyword2 = keyData2;
            this.mKeyword3 = keyData3;
        }
        [DataMember]
        public long ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public string Name { get { return this.mName; } set { this.mName = value; } }
        [DataMember]
        public DateTime Date { get { return this.mDate; } set { this.mDate = value; } }
        [DataMember]
        public string Category { get { return this.mCategory; } set { this.mCategory = value; } }
        [DataMember]
        public string Event { get { return this.mEvent; } set { this.mEvent = value; } }
        [DataMember]
        public string User { get { return this.mUser; } set { this.mUser = value; } }
        [DataMember]
        public string Computer { get { return this.mComputer; } set { this.mComputer = value; } }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
        [DataMember]
        public string Source { get { return this.mSource; } set { this.mSource = value; } }
        [DataMember]
        public LogLevel LogLevel { get { return this.mLogLevel; } set { this.mLogLevel = value; } }
        [DataMember]
        public string Keyword1 { get { return this.mKeyword1; } set { this.mKeyword1 = value; } }
        [DataMember]
        public string Keyword2 { get { return this.mKeyword2; } set { this.mKeyword2 = value; } }
        [DataMember]
        public string Keyword3 { get { return this.mKeyword3; } set { this.mKeyword3 = value; } }
    }
}
