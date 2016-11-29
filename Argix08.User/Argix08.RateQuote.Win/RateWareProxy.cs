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
	public class RateWareProxy {
		//Members
        private static RateWareServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static RateWareProxy() { 
            //
            _Client = new RateWareServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private RateWareProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the application configuration for the specified user
            UserConfiguration config=null;
            try {
                config = _Client.GetUserConfiguration(application,usernames);
            }
            catch(FaultException fe) { throw new ApplicationException("GetUserConfiguration() service error.",fe); }
            catch(TimeoutException te) { throw new ApplicationException("GetUserConfiguration() timeout error.",te); }
            catch(CommunicationException ce) { throw new ApplicationException("GetUserConfiguration() communication error.",ce); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Write an entry into the Argix log
            try {
                _Client.WriteLogEntry(m);
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public static TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            try {
                return _Client.GetTerminalInfo();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
        }

        public static ClassCodes GetClassCodes() {
            //
            ClassCodes codes=null;
            try {
                codes = _Client.GetClassCodes();
            }
            catch(FaultException fe) { throw new ApplicationException("GetClassCodes() service error.",fe); }
            catch(TimeoutException te) { throw new ApplicationException("GetClassCodes() timeout error.",te); }
            catch(CommunicationException ce) { throw new ApplicationException("GetClassCodes() communication error.",ce); }
            return codes;
        }
        public static string[] GetTariffs() {
            //
            string[] tariffs=null;
            try {
                tariffs = _Client.GetTariffs();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTariffs() service error.",fe); }
            catch(TimeoutException te) { throw new ApplicationException("GetTariffs() timeout error.",te); }
            catch(CommunicationException ce) { throw new ApplicationException("GetTariffs() communication error.",ce); }
            return tariffs;
        }
        public static Rates CalculateRates(string tariff,string originZip,string classCode,double discount,int floorMin,string[] destinationZips) {
            //
            Rates rates=null;
            try {
                rates = _Client.CalculateRates(tariff,originZip,classCode,discount,floorMin,destinationZips);
            }
            catch(FaultException fe) { throw new ApplicationException("CalculateRates() service error.",fe); }
            catch(TimeoutException te) { throw new ApplicationException("CalculateRates() timeout error.",te); }
            catch(CommunicationException ce) { throw new ApplicationException("CalculateRates() communication error.",ce); }
            return rates;
        }
    }
}