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

namespace Argix.Kronos {
	//
	public class KronosProxy {
		//Members
        private static KronosClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static KronosProxy() { 
            //
            _Client = new KronosClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private KronosProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application, string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            try {
                _Client = new KronosClient();
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
                _Client = new KronosClient();
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
                _Client = new KronosClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }
        public static object[] GetIDTypes() {
            //Get invoices for the specified client
            object[] types = null;
            try {
                _Client = new KronosClient();
                types = _Client.GetIDTypes();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIDTypes() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIDTypes() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIDTypes() communication error.",ce); }
            return types;
        }
        public static Employees GetEmployees(string idType) {
            //Get client list
            Employees employees = null;
            try {
                _Client = new KronosClient();
                employees = _Client.GetEmployees(idType);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetEmployees() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetEmployees() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetEmployees() communication error.",ce); }
            return employees;
        }
    }
}