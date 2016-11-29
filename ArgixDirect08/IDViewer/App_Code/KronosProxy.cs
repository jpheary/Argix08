using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix {
	//
	public class KronosProxy {
		//Members

		//Interface
        public KronosProxy() { }
        public CommunicationState ServiceState { get { return new KronosClient().State; } }
        public string ServiceAddress { get { return new KronosClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            KronosClient _Client=null;
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
        public void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            KronosClient _Client=null;
            try {
                _Client = new KronosClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("WriteLogEntry() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("WriteLogEntry() communication error.",ce); }
        }
        public TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            TerminalInfo terminal=null;
            KronosClient _Client=null;
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

        public object[] GetIDTypes() {
            //Get invoices for the specified client
            object[] types = null;
            KronosClient _Client = null;
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
        public Employees GetEmployeeList(string idType) {
            //Get a list all employees from idType database
            Employees employees = null;
            KronosClient _Client = null;
            try {
                _Client = new KronosClient();
                employees = _Client.GetEmployeeList(idType);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetEmployeeList() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetEmployeeList() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetEmployeeList() communication error.",ce); }
            return employees;
        }
        public Employees GetEmployees(string idType) {
            //Get all employees from idType database
            Employees employees = null;
            KronosClient _Client = null;
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
        public Employee GetEmployee(string idType, int idNumber) {
            //Get an individual employee from idType database
            Employee employee = null;
            KronosClient _Client = null;
            try {
                _Client = new KronosClient();
                employee = _Client.GetEmployee(idType,idNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetEmployee() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetEmployee() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetEmployee() communication error.",ce); }
            return employee;
        }
        public Employees SearchEmployees(string idType,string lastName,string firstName,string location,string badgeNumber) {
            //Get issue search data
            KronosClient client = null;
            Employees employees = new Employees();
            try {
                string _lastName = (lastName != null && lastName.Trim().Length > 0) ? lastName : null;
                string _firstName = (firstName != null && firstName.Trim().Length > 0) ? firstName : null;
                string _location = (location != null && location.Trim().Length > 0) ? location : null;
                string _badgeNumber = (badgeNumber != null && badgeNumber.Trim().Length > 0) ? badgeNumber : null;
                if(_lastName == null && _firstName == null && _location == null && _badgeNumber == null)
                    return new Employees();
                
                client = new KronosClient();
                employees = client.SearchEmployees(idType, _lastName,_firstName,_location,_badgeNumber);
                client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("SearchEmployees() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("SearchEmployees() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("SearchEmployees() communication error.",ce); }
            return employees;
        }
    }
}