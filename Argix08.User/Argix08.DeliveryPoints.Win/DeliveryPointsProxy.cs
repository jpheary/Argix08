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
using Argix.Windows;

namespace Argix.Terminals {
	//
	public class DeliveryPointsProxy {
		//Members
        private static DeliveryPointsServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static DeliveryPointsProxy() { 
            //
            _Client = new DeliveryPointsServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private DeliveryPointsProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            try {
                _Client = new DeliveryPointsServiceClient();
                config = _Client.GetUserConfiguration(application,usernames);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetUserConfiguration() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetUserConfiguration() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetUserConfiguration() communication error.",ce); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            try {
                _Client = new DeliveryPointsServiceClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public static TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            TerminalInfo terminal=null;
            try {
                _Client = new DeliveryPointsServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }

        public static DeliveryPoints GetDeliveryPoints(DateTime startDate,DateTime lastUpated) {
            //Get delivery points
            DeliveryPoints points=null;
            try {
                _Client = new DeliveryPointsServiceClient();
                points = _Client.GetDeliveryPoints(startDate, lastUpated);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetDeliveryPoints() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDeliveryPoints() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDeliveryPoints() communication error.",ce); }
            return points;
        }
        public static DateTime GetExportDate() {
            //Get the latest delivery point LastUpdated date from the last export
            DateTime date=DateTime.Now;
            try {
                _Client = new DeliveryPointsServiceClient();
                date = _Client.GetExportDate();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetExportDate() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetExportDate() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetExportDate() communication error.",ce); }
            return date;
        }
        public static bool UpdateExportDate(DateTime lastUpdated) {
            //Update the latest delivery point LastUpdated date from the last export
            bool updated=false;
            try {
                _Client = new DeliveryPointsServiceClient();
                updated = _Client.UpdateExportDate(lastUpdated);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UpdateExportDate() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UpdateExportDate() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UpdateExportDate() communication error.",ce); }
            return updated;
        }
    }
}