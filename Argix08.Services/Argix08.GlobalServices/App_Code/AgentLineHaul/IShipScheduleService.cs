using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.AgentLineHaul {
    //Enterprise Interfaces
    [ServiceContract(Namespace="http://Argix.AgentLineHaul")]
    public interface IShipScheduleService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);

        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();

        [OperationContract]
        Argix.Enterprise.Terminals GetTerminals();
        
        [OperationContract]
        Argix.Enterprise.Terminals GetShippersAndTerminals();
        
        [OperationContract]
        Argix.Enterprise.Carriers GetCarriers();
        
        [OperationContract]
        Argix.Enterprise.Agents GetAgents();
        
        [OperationContract]
        Argix.Enterprise.Shippers GetShippers();
        
        [OperationContract]
        DataSet GetDaysOfWeek();
        
        [OperationContract]
        int GetWeekday(string weekdayName);

        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action="http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetTemplates();
        
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action="http://Argix.AgentLineHaul.ShipScheduleFault")]
        //[TransactionFlow(TransactionFlowOption.NotAllowed)]
        string AddTemplate(ShipScheduleTemplate template);
        
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action="http://Argix.AgentLineHaul.ShipScheduleFault")]
        //[TransactionFlow(TransactionFlowOption.NotAllowed)]
        bool UpdateTemplate(ShipScheduleTemplate template);

        [OperationContract]
        System.IO.Stream GetExportDefinition();
    }

    [CollectionDataContract]
    public class ShipScheduleTemplates : BindingList<ShipScheduleTemplate> {
        public ShipScheduleTemplates() { }
    }

    [DataContract]
    public class ShipScheduleTemplate {
        //Members
        private string mTemplateID="";
        private long mSortCenterID=0;
        private string mSortCenter="";
        private byte mDayOfTheWeek=0;
        private long mCarrierServiceID=0;
        private string mCarrier="";
        private byte mScheduledCloseDateOffset=0;
        private DateTime mScheduledCloseTime;
        private byte mScheduledDepartureDateOffset=0;
        private DateTime mScheduledDepartureTime;
        private byte mIsMandatory=0,  mIsActive=0;
        private DateTime mTemplateLastUpdated;
        private string mTemplateUser="", mTemplateRowVersion="", mStopID="",  mStopNumber="", mMainZone="", mTag="";
        private long mAgentTerminalID=0;
        private string mAgentNumber="";
        private byte mScheduledArrivalDateOffset=0;
        private DateTime mScheduledArrivalTime;
        private byte mScheduledOFD1Offset=0;
        private string mNotes="";
        private DateTime mStop1LastUpdated;
        private string mStop1User="", mStop1RowVersion="", mS2StopID="", mS2StopNumber="", mS2MainZone="", mS2Tag="";
        private long mS2AgentTerminalID=0;
        private string mS2AgentNumber="";
        private byte mS2ScheduledArrivalDateOffset=0;
        private DateTime mS2ScheduledArrivalTime;
        private byte mS2ScheduledOFD1Offset=0;
        private string mS2Notes="";
        private DateTime mStop2LastUpdated;
        private string mStop2User="", mStop2RowVersion="";

        //Interface
        public ShipScheduleTemplate() : this(null) { }
        public ShipScheduleTemplate(TemplateDS.TemplateTableRow template) {
            //Constructor
            if(template != null) {
                #region Set members
                if(!template.IsTemplateIDNull()) this.mTemplateID = template.TemplateID;
                if(!template.IsSortCenterIDNull()) this.mSortCenterID = template.SortCenterID;
                if(!template.IsSortCenterNull()) this.mSortCenter = template.SortCenter;
                if(!template.IsDayOfTheWeekNull()) this.mDayOfTheWeek = template.DayOfTheWeek;
                if(!template.IsCarrierServiceIDNull()) this.mCarrierServiceID = template.CarrierServiceID;
                if(!template.IsCarrierNull()) this.mCarrier = template.Carrier;
                if(!template.IsScheduledCloseDateOffsetNull()) this.mScheduledCloseDateOffset = template.ScheduledCloseDateOffset;
                if(!template.IsScheduledCloseTimeNull()) this.mScheduledCloseTime = template.ScheduledCloseTime;
                if(!template.IsScheduledDepartureDateOffsetNull()) this.mScheduledDepartureDateOffset = template.ScheduledDepartureDateOffset;
                if(!template.IsScheduledDepartureTimeNull()) this.mScheduledDepartureTime = template.ScheduledDepartureTime;
                if(!template.IsIsMandatoryNull()) this.mIsMandatory = template.IsMandatory;
                if(!template.IsIsActiveNull()) this.mIsActive = template.IsActive;
                if(!template.IsTemplateLastUpdatedNull()) this.mTemplateLastUpdated = template.TemplateLastUpdated;
                if(!template.IsTemplateUserNull()) this.mTemplateUser = template.TemplateUser;
                if(!template.IsTemplateRowVersionNull()) this.mTemplateRowVersion = template.TemplateRowVersion;
                if(!template.IsStopIDNull()) this.mStopID = template.StopID;
                if(!template.IsStopNumberNull()) this.mStopNumber = template.StopNumber;
                if(!template.IsMainZoneNull()) this.mMainZone = template.MainZone;
                if(!template.IsTagNull()) this.mTag = template.Tag;
                if(!template.IsAgentTerminalIDNull()) this.mAgentTerminalID = template.AgentTerminalID;
                if(!template.IsAgentNumberNull()) this.mAgentNumber = template.AgentNumber;
                if(!template.IsScheduledArrivalDateOffsetNull()) this.mScheduledArrivalDateOffset = template.ScheduledArrivalDateOffset;
                if(!template.IsScheduledArrivalTimeNull()) this.mScheduledArrivalTime = template.ScheduledArrivalTime;
                if(!template.IsScheduledOFD1OffsetNull()) this.mScheduledOFD1Offset = template.ScheduledOFD1Offset;
                if(!template.IsNotesNull()) this.mNotes = template.Notes;
                if(!template.IsStop1LastUpdatedNull()) this.mStop1LastUpdated = template.Stop1LastUpdated;
                if(!template.IsStop1UserNull()) this.mStop1User = template.Stop1User;
                if(!template.IsStop1RowVersionNull()) this.mStop1RowVersion = template.Stop1RowVersion;
                if(!template.IsS2StopIDNull()) this.mS2StopID = template.S2StopID;
                if(!template.IsS2StopNumberNull()) this.mS2StopNumber = template.S2StopNumber;
                if(!template.IsS2MainZoneNull()) this.mS2MainZone = template.S2MainZone;
                if(!template.IsS2TagNull()) this.mS2Tag = template.S2Tag;
                if(!template.IsS2AgentTerminalIDNull()) this.mS2AgentTerminalID = template.S2AgentTerminalID;
                if(!template.IsS2AgentNumberNull()) this.mS2AgentNumber = template.S2AgentNumber;
                if(!template.IsS2ScheduledArrivalDateOffsetNull()) this.mS2ScheduledArrivalDateOffset = template.S2ScheduledArrivalDateOffset;
                if(!template.IsS2ScheduledArrivalTimeNull()) this.mS2ScheduledArrivalTime = template.S2ScheduledArrivalTime;
                if(!template.IsS2ScheduledOFD1OffsetNull()) this.mS2ScheduledOFD1Offset = template.S2ScheduledOFD1Offset;
                if(!template.IsS2NotesNull()) this.mS2Notes = template.S2Notes;
                if(!template.IsStop2LastUpdatedNull()) this.mStop2LastUpdated = template.Stop2LastUpdated;
                if(!template.IsStop2UserNull()) this.mStop2User = template.Stop2User;
                if(!template.IsStop2RowVersionNull()) this.mStop2RowVersion = template.Stop2RowVersion;
                #endregion
            }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string TemplateID { get { return this.mTemplateID; } set { this.mTemplateID = value; } }
        [DataMember]
        public long SortCenterID { get { return this.mSortCenterID; } set { this.mSortCenterID = value; } }
        [DataMember]
        public string SortCenter { get { return this.mSortCenter; } set { this.mSortCenter = value; } }
        [DataMember]
        public byte DayOfTheWeek { get { return this.mDayOfTheWeek; } set { this.mDayOfTheWeek = value; } }
        [DataMember]
        public long CarrierServiceID { get { return this.mCarrierServiceID; } set { this.mCarrierServiceID = value; } }
        [DataMember]
        public string Carrier { get { return this.mCarrier; } set { this.mCarrier = value; } }
        [DataMember]
        public byte ScheduledCloseDateOffset { get { return this.mScheduledCloseDateOffset; } set { this.mScheduledCloseDateOffset = value; } }
        [DataMember]
        public DateTime ScheduledCloseTime { get { return this.mScheduledCloseTime; } set { this.mScheduledCloseTime = value; } }
        [DataMember]
        public byte ScheduledDepartureDateOffset { get { return this.mScheduledDepartureDateOffset; } set { this.mScheduledDepartureDateOffset = value; } }
        [DataMember]
        public DateTime ScheduledDepartureTime { get { return this.mScheduledDepartureTime; } set { this.mScheduledDepartureTime = value; } }
        [DataMember]
        public byte IsMandatory { get { return this.mIsMandatory; } set { this.mIsMandatory = value; } }
        [DataMember]
        public byte IsActive { get { return this.mIsActive; } set { this.mIsActive = value; } }
        [DataMember]
        public DateTime TemplateLastUpdated { get { return this.mTemplateLastUpdated; } set { this.mTemplateLastUpdated = value; } }
        [DataMember]
        public string TemplateUser { get { return this.mTemplateUser; } set { this.mTemplateUser = value; } }
        [DataMember]
        public string TemplateRowVersion { get { return this.mTemplateRowVersion; } set { this.mTemplateRowVersion = value; } }
        [DataMember]
        public string StopID { get { return this.mStopID; } set { this.mStopID = value; } }
        [DataMember]
        public string StopNumber { get { return this.mStopNumber; } set { this.mStopNumber = value; } }
        [DataMember]
        public string MainZone { get { return this.mMainZone; } set { this.mMainZone = value; } }
        [DataMember]
        public string Tag { get { return this.mTag; } set { this.mTag = value; } }
        [DataMember]
        public long AgentTerminalID { get { return this.mAgentTerminalID; } set { this.mAgentTerminalID = value; } }
        [DataMember]
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        [DataMember]
        public byte ScheduledArrivalDateOffset { get { return this.mScheduledArrivalDateOffset; } set { this.mScheduledArrivalDateOffset = value; } }
        [DataMember]
        public DateTime ScheduledArrivalTime { get { return this.mScheduledArrivalTime; } set { this.mScheduledArrivalTime = value; } }
        [DataMember]
        public byte ScheduledOFD1Offset { get { return this.mScheduledOFD1Offset; } set { this.mScheduledOFD1Offset = value; } }
        [DataMember]
        public string Notes { get { return this.mNotes; } set { this.mNotes = value; } }
        [DataMember]
        public DateTime Stop1LastUpdated { get { return this.mStop1LastUpdated; } set { this.mStop1LastUpdated = value; } }
        [DataMember]
        public string Stop1User { get { return this.mStop1User; } set { this.mStop1User = value; } }
        [DataMember]
        public string Stop1RowVersion { get { return this.mStop1RowVersion; } set { this.mStop1RowVersion = value; } }
        [DataMember]
        public string S2StopID { get { return this.mS2StopID; } set { this.mS2StopID = value; } }
        [DataMember]
        public string S2StopNumber { get { return this.mS2StopNumber; } set { this.mS2StopNumber = value; } }
        [DataMember]
        public string S2MainZone { get { return this.mS2MainZone; } set { this.mS2MainZone = value; } }
        [DataMember]
        public string S2Tag { get { return this.mS2Tag; } set { this.mS2Tag = value; } }
        [DataMember]
        public long S2AgentTerminalID { get { return this.mS2AgentTerminalID; } set { this.mS2AgentTerminalID = value; } }
        [DataMember]
        public string S2AgentNumber { get { return this.mS2AgentNumber; } set { this.mS2AgentNumber = value; } }
        [DataMember(EmitDefaultValue=false,IsRequired=false)]
        public byte S2ScheduledArrivalDateOffset { get { return this.mS2ScheduledArrivalDateOffset; } set { this.mS2ScheduledArrivalDateOffset = value; } }
        [DataMember(EmitDefaultValue=false,IsRequired=false)]
        public DateTime S2ScheduledArrivalTime { get { return this.mS2ScheduledArrivalTime; } set { this.mS2ScheduledArrivalTime = value; } }
        [DataMember(EmitDefaultValue=false,IsRequired=false)]
        public byte S2ScheduledOFD1Offset { get { return this.mS2ScheduledOFD1Offset; } set { this.mS2ScheduledOFD1Offset = value; } }
        [DataMember]
        public string S2Notes { get { return this.mS2Notes; } set { this.mS2Notes = value; } }
        [DataMember]
        public DateTime Stop2LastUpdated { get { return this.mStop2LastUpdated; } set { this.mStop2LastUpdated = value; } }
        [DataMember]
        public string Stop2User { get { return this.mStop2User; } set { this.mStop2User = value; } }
        [DataMember]
        public string Stop2RowVersion { get { return this.mStop2RowVersion; } set { this.mStop2RowVersion = value; } }
        #endregion
    }

    [DataContract]
    public class ShipScheduleFault {
        private string _messsage;
        public ShipScheduleFault(string message) { this._messsage = message; }

        [DataMember]
        public string Message { get { return this._messsage; } set { this._messsage = value; } }
    }
}
