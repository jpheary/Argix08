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

namespace Argix.Finance {
	//
	public class InvoicingProxy {
		//Members
        private static InvoicingServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static InvoicingProxy() { 
            //
            _Client = new InvoicingServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private InvoicingProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application, string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            try {
                _Client = new InvoicingServiceClient();
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
                _Client = new InvoicingServiceClient();
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
                _Client = new InvoicingServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }
        public static Clients GetClients() {
            //Get client list
            Clients clients=null;
            try {
                _Client = new InvoicingServiceClient();
                clients = _Client.GetClients();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetClients() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetClients() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetClients() communication error.",ce); }
            return clients;
        }
        public static Invoices GetClientInvoices(string clientNumber,string clientDivision,string startDate) {
            //Get invoices for the specified client
            Invoices invoices=null;
            try {
                _Client = new InvoicingServiceClient();
                invoices = _Client.GetClientInvoices(clientNumber, clientDivision, startDate);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetClientInvoices() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetClientInvoices() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetClientInvoices() communication error.",ce); }
            return invoices;
        }
    }
}