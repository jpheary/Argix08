using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Terminals {
    //Shipping Interfaces
    [ServiceContract(Namespace="http://Argix.Terminals")]
    public interface IDeliveryPointsService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();

        [OperationContract]
        DeliveryPoints GetDeliveryPoints(DateTime startDate,DateTime lastUpated);
        
        [OperationContract]
        DateTime GetExportDate();
        
        [OperationContract]
        bool UpdateExportDate(DateTime lastUpdated);
    }

    [CollectionDataContract]
    public class DeliveryPoints: BindingList<DeliveryPoint> {
        public DeliveryPoints() { }
    }

    [DataContract]
    public class DeliveryPoint {
        //Members
        private string _Command="", _Account="";
        private string _NickName="", _Name="";
        private string _Building="", _Route="", _Phone="";
        private DateTime _OpenDate;
        private string _OpenTime="", _CloseTime="";
        private decimal _ServiceTimeFactor=0.0M;
        private string _Unit="";
        private int _SetupTime=0;
        private string _Appt="";
        private Stop _Stop=null;
        private DateTime _LastUpdated;
        public DeliveryPoint(): this(null) { }
        public DeliveryPoint(DeliveryPointDS.DeliveryPointTableRow point) { 
            //Constructor
            try {
                if(point != null) {
                    if(!point.IsCommandNull()) this._Command = point.Command;
                    if(!point.IsCommandNull()) this._Account = point.Account;
                    if(!point.IsNickNameNull()) this._NickName = point.NickName;
                    if(!point.IsNameNull()) this._Name = point.Name;
                    if(!point.IsBuildingNull()) this._Building = point.Building;
                    if(!point.IsRouteNull()) this._Route = point.Route;
                    if(!point.IsPhoneNull()) this._Phone = point.Phone;
                    if(!point.IsOpenDateNull()) this._OpenDate = point.OpenDate;
                    if(!point.IsOpenTimeNull()) this._OpenTime = point.OpenTime;
                    if(!point.IsCloseTimeNull()) this._CloseTime = point.CloseTime;
                    if(!point.IsServiceTimeFactorNull()) this._ServiceTimeFactor = point.ServiceTimeFactor;
                    if(!point.IsUnitNull()) this._Unit = point.Unit;
                    if(!point.IsSetupTimeNull()) this._SetupTime = point.SetupTime;
                    if(!point.IsApptNull()) this._Appt = point.Appt;
                    if(!point.IsLastUpdatedNull()) this._LastUpdated = point.LastUpdated;

                    StopDS.StopTableRow stop = new StopDS().StopTable.NewStopTableRow();
                    if(!point.IsStopNameNull()) stop.Name = point.StopName;
                    if(!point.IsStopNickNameNull()) stop.NickName = point.StopNickName;
                    if(!point.IsStopPhoneNull()) stop.Phone = point.StopPhone;
                    if(!point.IsAddressNull()) stop.Address = point.Address;
                    if(!point.IsCityNull()) stop.City = point.City;
                    if(!point.IsStateNull()) stop.State = point.State;
                    if(!point.IsZipNull()) stop.Zip = point.Zip;
                    if(!point.IsStopOpenNull()) stop.Open = point.StopOpen;
                    if(!point.IsStopCloseNull()) stop.Close = point.StopClose;
                    if(!point.IsStopCommentNull()) stop.Comment = point.StopComment;
                    this._Stop = new Stop(stop);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new delivery point.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Command { get { return this._Command; } set { this._Command = value; } }
        [DataMember]
        public string Account { get { return this._Account; } set { this._Account = value; } }
        [DataMember]
        public string NickName { get { return this._NickName; } set { this._NickName = value; } }
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string Building { get { return this._Building; } set { this._Building = value; } }
        [DataMember]
        public string Route { get { return this._Route; } set { this._Route = value; } }
        [DataMember]
        public string Phone { get { return this._Phone; } set { this._Phone = value; } }
        [DataMember]
        public DateTime OpenDate { get { return this._OpenDate; } set { this._OpenDate = value; } }
        [DataMember]
        public string OpenTime { get { return this._OpenTime; } set { this._OpenTime = value; } }
        [DataMember]
        public string CloseTime { get { return this._CloseTime; } set { this._CloseTime = value; } }
        [DataMember]
        public decimal ServiceTimeFactor { get { return this._ServiceTimeFactor; } set { this._ServiceTimeFactor = value; } }
        [DataMember]
        public string Unit { get { return this._Unit; } set { this._Unit = value; } }
        [DataMember]
        public int SetupTime { get { return this._SetupTime; } set { this._SetupTime = value; } }
        [DataMember]
        public string Appt { get { return this._Appt; } set { this._Appt = value; } }
        [DataMember]
        public Stop Stop { get { return this._Stop; } set { this._Stop = value; } }
        [DataMember]
        public string StopName { get { return this._Stop.Name; } set { } }
        [DataMember]
        public string StopNickName { get { return this._Stop.NickName; } set { } }
        [DataMember]
        public string StopPhone { get { return this._Stop.Phone; } set { } }
        [DataMember]
        public string StopAddress { get { return this._Stop.Address; } set { } }
        [DataMember]
        public string StopCity { get { return this._Stop.City; } set { } }
        [DataMember]
        public string StopState { get { return this._Stop.State; } set { } }
        [DataMember]
        public string StopZip { get { return this._Stop.Zip; } set { } }
        [DataMember]
        public string StopOpen { get { return this._Stop.Open; } set { } }
        [DataMember]
        public string StopClose { get { return this._Stop.Close; } set { } }
        [DataMember]
        public string StopComment { get { return this._Stop.Comment; } set { } }
        [DataMember]
        public DateTime LastUpdated { get { return this._LastUpdated; } set { this._LastUpdated = value; } }
        #endregion
    }
}
