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

namespace Argix.AgentLineHaul {
	//
	public class ShipScheduleProxy {
		//Members
        private static ShipScheduleServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static ShipScheduleProxy() { 
            //
            _Client = new ShipScheduleServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private ShipScheduleProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application, string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            try {
                _Client = new ShipScheduleServiceClient();
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
                _Client = new ShipScheduleServiceClient();
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
                _Client = new ShipScheduleServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }
        public static Terminals GetShippersAndTerminals() {
            //Get terminals list
            Terminals terminals = null;
            try {
                _Client = new ShipScheduleServiceClient();
                terminals = _Client.GetShippersAndTerminals();
                _Client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetShippersAndTerminals() service error.", fe); }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetShippersAndTerminals() timeout error.", te); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetShippersAndTerminals() communication error.", ce); }
            return terminals;
        }
        public static Carriers GetCarriers() {
            //Get carriers list
            Carriers carriers = null;
            try {
                _Client = new ShipScheduleServiceClient();
                carriers = _Client.GetCarriers();
                _Client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetCarriers() service error.", fe); }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCarriers() timeout error.", te); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCarriers() communication error.", ce); }
            return carriers;
        }
        public static Agents GetAgents() {
            //Get agents list
            Agents agents = null;
            try {
                _Client = new ShipScheduleServiceClient();
                agents = _Client.GetAgents();
                _Client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetAgents() service error.", fe); }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetAgents() timeout error.", te); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetAgents() communication error.", ce); }
            return agents;
        }
        public static Shippers GetShippers() {
            //Get client list
            Shippers shippers = null;
            try {
                _Client = new ShipScheduleServiceClient();
                shippers = _Client.GetShippers();
                _Client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetShippers() service error.", fe); }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetShippers() timeout error.", te); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetShippers() communication error.", ce); }
            return shippers;
        }
        public static DataSet GetDaysOfWeek() {
            //Get days of the week list
            DataSet days = null;
            try {
                _Client = new ShipScheduleServiceClient();
                days = _Client.GetDaysOfWeek();
                _Client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetDaysOfWeek() service error.", fe); }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDaysOfWeek() timeout error.", te); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDaysOfWeek() communication error.", ce); }
            return days;
        }
        public static int GetWeekday(string weekdayName) {
            //Get weekday as a number
            int weekday = 0;
            try {
                _Client = new ShipScheduleServiceClient();
                weekday = _Client.GetWeekday(weekdayName);
                _Client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetWeekday() service error.", fe); }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetWeekday() timeout error.", te); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetWeekday() communication error.", ce); }
            return weekday;
        }

        public static TemplateDS GetTemplates() {
            //Get templates list
            TemplateDS templates = null;
            try {
                templates = new TemplateDS();
                _Client = new ShipScheduleServiceClient();
                DataSet ds = _Client.GetTemplates();
                templates.Merge(ds);
                _Client.Close();
            }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch(FaultException<ShipScheduleFault> ssf) { throw new ApplicationException(ssf.Reason.ToString(),ssf.InnerException); }
            catch(FaultException fe) { throw new ApplicationException(fe.Message,fe.InnerException); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message,ce.InnerException); }
            return templates;
        }
        public static string AddTemplate(ShipScheduleTemplate template) {
            //
            string templateID="";
            try {
                _Client = new ShipScheduleServiceClient();
                templateID = _Client.AddTemplate(template);
                _Client.Close();
            }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch(FaultException<ShipScheduleFault> ssf) { throw new ApplicationException(ssf.Reason.ToString(),ssf.InnerException); }
            catch(FaultException fe) { throw new ApplicationException(fe.Message,fe.InnerException); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message,ce.InnerException); }
            return templateID;
        }
        public static bool UpdateTemplate(ShipScheduleTemplate template) {
            //
            bool ret = false;
            try {
                _Client = new ShipScheduleServiceClient();
                ret = _Client.UpdateTemplate(template);
                _Client.Close();
            }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch(FaultException<ShipScheduleFault> ssf) { throw new ApplicationException(ssf.Reason.ToString(),ssf.InnerException); }
            catch(FaultException fe) { throw new ApplicationException(fe.Message, fe.InnerException); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message,ce.InnerException); }
            return ret;
        }

        public static System.IO.Stream GetExportDefinition() {
            //
            System.IO.Stream xml = null;
            try {
                _Client = new ShipScheduleServiceClient();
                xml = _Client.GetExportDefinition();
                _Client.Close();
            }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch(FaultException fe) { throw new ApplicationException(fe.Message,fe.InnerException); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message,ce.InnerException); }
            return xml;
        }
    }
}