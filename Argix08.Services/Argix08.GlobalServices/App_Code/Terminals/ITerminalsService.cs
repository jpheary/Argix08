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
    public interface ITerminalsService {
        //Interface

    }

    [CollectionDataContract]
    public class LocalTerminals:BindingList<LocalTerminal> {
        public LocalTerminals() { }
    }

    [DataContract]
    public class LocalTerminal {
        //Members
        private long _id=0;
        private string _name = "";

        //Interface
        public LocalTerminal() : this(0,"") { }
        public LocalTerminal(long id,string name) { this._id = id; this._name = name; }

        [DataMember]
        public long TerminalID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public string TerminalName { get { return this._name; } set { this._name = value; } }
    }

    [CollectionDataContract]
    public class LocalDrivers:BindingList<LocalDriver> {
        public LocalDrivers() { }
    }

    [DataContract]
    public class LocalDriver:Argix.Enterprise.Driver {
        //Members
        private int _numberofbatteries=0;
        private BatteryItemAssignments _assignments=null;

        //Interface
        public LocalDriver() : this(null) { }
        public LocalDriver(LocalDriverDS.LocalDriverTableRow localDriver) {
            //Constructor
            if(localDriver != null) {
                if(!localDriver.IsDriverIDNull()) base.DriverID = localDriver.DriverID;
                if(!localDriver.IsTerminalIDNull()) base.TerminalID = localDriver.TerminalID;
                if(!localDriver.IsTerminalNull()) base.Terminal = localDriver.Terminal;
                if(!localDriver.IsFirstNameNull()) base.FirstName = localDriver.FirstName;
                if(!localDriver.IsLastNameNull()) base.LastName = localDriver.LastName;
                if(!localDriver.IsFullNameNull())
                    base.FullName = localDriver.FullName;
                else {
                    base.FullName = (!localDriver.IsLastNameNull() ? localDriver.LastName.Trim() : "") + ", " + (!localDriver.IsFirstNameNull() ? localDriver.FirstName.Trim() : "");
                    if(base.FullName == ", ") base.FullName = "";
                }
                if(!localDriver.IsIsActiveNull()) base.IsActive = localDriver.IsActive;

                if(!localDriver.IsNumberOfBatteriesNull()) this._numberofbatteries = localDriver.NumberOfBatteries;
            }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int NumberOfBatteries { get { return this._numberofbatteries; } set { this._numberofbatteries = value; } }
        [DataMember]
        public BatteryItemAssignments Assignments { get { return this._assignments; } set { this._assignments = value; } }
        #endregion
    }


    [DataContract]
    public class Stop {
        //Members
        private string _Name="",_NickName="",_Phone="";
        private string _Address="",_City="",_State="",_Zip="";
        private string _Open="",_Close="",_Comment="";
        public Stop() : this(null) { }
        public Stop(StopDS.StopTableRow stop) {
            //Constructor
            try {
                if(stop != null) {
                    if(!stop.IsNameNull()) this._Name = stop.Name;
                    if(!stop.IsNickNameNull()) this._NickName = stop.NickName;
                    if(!stop.IsPhoneNull()) this._Phone = stop.Phone;
                    if(!stop.IsAddressNull()) this._Address = stop.Address;
                    if(!stop.IsCityNull()) this._City = stop.City;
                    if(!stop.IsStateNull()) this._State = stop.State;
                    if(!stop.IsZipNull()) this._Zip = stop.Zip;
                    if(!stop.IsOpenNull()) this._Open = stop.Open;
                    if(!stop.IsCloseNull()) this._Close = stop.Close;
                    if(!stop.IsCommentNull()) this._Comment = stop.Comment;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new delivery point.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string NickName { get { return this._NickName; } set { this._NickName = value; } }
        [DataMember]
        public string Phone { get { return this._Phone; } set { this._Phone = value; } }
        [DataMember]
        public string Address { get { return this._Address; } set { this._Address = value; } }
        [DataMember]
        public string City { get { return this._City; } set { this._City = value; } }
        [DataMember]
        public string State { get { return this._State; } set { this._State = value; } }
        [DataMember]
        public string Zip { get { return this._Zip; } set { this._Zip = value; } }
        [DataMember]
        public string Open { get { return this._Open; } set { this._Open = value; } }
        [DataMember]
        public string Close { get { return this._Close; } set { this._Close = value; } }
        [DataMember]
        public string Comment { get { return this._Comment; } set { this._Comment = value; } }
        #endregion
    }

    [DataContract]
    public class TerminalsFault {
        private Exception _ex=null;
        public TerminalsFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
