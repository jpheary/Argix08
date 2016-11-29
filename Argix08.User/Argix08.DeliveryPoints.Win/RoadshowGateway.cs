using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using Argix.Roadshow;

namespace Argix.Terminals {
	//
	public class RoadshowGateway {
		//Members
        private static RoadshowServiceClient _Client = null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static RoadshowGateway() { 
            //
            _Client = new RoadshowServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private RoadshowGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static RoadshowDS GetCustomers() {
            //Get roadshow customers
            RoadshowDS customers = null;
            try {
                customers = new RoadshowDS();
                
                _Client = new RoadshowServiceClient();
                DataSet ds = _Client.GetCustomers();
                _Client.Close();

                if (ds != null) customers.Merge(ds);
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return customers;
        }
    }
}