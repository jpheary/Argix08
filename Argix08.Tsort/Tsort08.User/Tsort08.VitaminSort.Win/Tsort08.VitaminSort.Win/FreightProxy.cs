//	File:	FreightProxy.cs
//	Author:	J. Heary
//	Date:	07/22/10
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Argix.Freight {
    //
    public class FreightProxy {
        //Members
        private static TsortServiceClient _Client=null;
        public static event EventHandler CommunicationChanged=null;

        //Interface
        static FreightProxy() {
            //
            _Client = new TsortServiceClient();
            _Client.InnerChannel.Opening += new EventHandler(OnStateChanged);
            _Client.InnerChannel.Opened += new EventHandler(OnStateChanged);
            _Client.InnerChannel.Closing += new EventHandler(OnStateChanged);
            _Client.InnerChannel.Closed += new EventHandler(OnStateChanged);
            _Client.InnerChannel.Faulted += new EventHandler(OnStateChanged);
            _Client.InnerChannel.UnknownMessageReceived += new EventHandler<System.ServiceModel.UnknownMessageReceivedEventArgs>(OnUnknownMessageReceived);
        }
        private FreightProxy() { }
        public static void OnStateChanged(object sender,EventArgs e) {
            Debug.WriteLine(_Client.State.ToString());
            if(CommunicationChanged != null) CommunicationChanged(null,EventArgs.Empty);
        }
        public static void OnUnknownMessageReceived(object sender,System.ServiceModel.UnknownMessageReceivedEventArgs e) {
            throw new NotImplementedException();
        }
        public static System.ServiceModel.CommunicationState FactoryState { get { return _Client.State; } }

        public static Workstation GetStation(string machinName) {
            //Get a view of TLs for the specified terminal
            Workstation workstation=null;
            try {
                workstation = _Client.GetStation(machinName);
            }
            catch(TimeoutException te) { _Client.Abort(); }
            catch(System.ServiceModel.CommunicationException ce) { _Client.Abort(); }
            return workstation;
        }
        public static StationAssignmentDS GetFreightAssignments(string worhstationID) {
            //Get a view of TLs for the specified terminal
            StationAssignmentDS assignments=null;
            try {
                assignments = new StationAssignmentDS();
                DataSet ds = _Client.GetFreightAssignments(worhstationID);
                if(ds != null) {
                    assignments.Merge(ds);
                }
            }
            catch(TimeoutException te) { _Client.Abort(); }
            catch(System.ServiceModel.CommunicationException ce) { _Client.Abort(); }
            return assignments;
        }
        public static SortedItem ProcessInputs(string[] inputs,decimal weight,string damageCode,string storeOverride,string freightID) {
            //
            SortedItem item=null;
            try {
                item = _Client.ProcessInputs(inputs,weight,damageCode,storeOverride,freightID);
            }
            catch(TimeoutException te) { _Client.Abort(); }
            catch(System.ServiceModel.CommunicationException ce) { _Client.Abort(); }
            return item;
        }
    }
}