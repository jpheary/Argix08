using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Customers {
	//
	public class CustomerProxy {
		//Members
        private static IssueMgtServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        public static int IssueDaysBack=30;
        public static string TempFolder="c:\\temp\\";
        private static IssueDS _IssueCache=null;
        private static DateTime _IssueCacheLastUpdate=DateTime.Now.AddDays(-IssueDaysBack);

		//Interface
        static CustomerProxy() { 
            //
            _Client = new IssueMgtServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private CustomerProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            TerminalInfo terminal=null;
            try {
                _Client = new IssueMgtServiceClient();
                terminal = _Client.GetTerminalInfo();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetTerminalInfo() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminalInfo() communication error.",ce); }
            return terminal;
        }
        
        #region Enterprise Services: GetCompanies(), GetCompany(), GetDistricts(), GetRegions(), GetAgents(), GetStoreDetail()
        public static CompanyDS GetCompanies() {
            //Companies
            CompanyDS companies=null;
            try {
                _Client = new IssueMgtServiceClient();
                companies = _Client.GetCompanies();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetCompanies() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCompanies() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCompanies() communication error.",ce); }
            return companies;
        }
        public static CompanyDS GetCompanies(bool activeOnly) {
            //Companies
            CompanyDS companies=null;
            try {
                _Client = new IssueMgtServiceClient();
                companies = _Client.GetActiveCompanies(activeOnly);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetCompanies() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCompanies() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCompanies() communication error.",ce); }
            return companies;
        }
        public static string GetCompany(int companyID) {
            //Get a company for the specified id
            string company = "";
            try {
                _Client = new IssueMgtServiceClient();
                company = _Client.GetCompany(companyID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetCompany() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCompany() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCompany() communication error.",ce); }
            return company;
        }
        public static LocationDS GetDistricts(string clientNumber) {
            //Get a list of client divisions
            LocationDS districts = null;
            try {
                _Client = new IssueMgtServiceClient();
                districts = _Client.GetDistricts(clientNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetDistricts() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDistricts() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDistricts() communication error.",ce); }
            return districts;
        }
        public static LocationDS GetRegions(string clientNumber) {
            //Get a list of client divisions
            LocationDS regions = null;
            try {
                _Client = new IssueMgtServiceClient();
                regions = _Client.GetRegions(clientNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetRegions() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetRegions() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetRegions() communication error.",ce); }
            return regions;
        }
        public static AgentDS GetAgents() {
            //Agents
            AgentDS agents=null;
            try {
                _Client = new IssueMgtServiceClient();
                agents = _Client.GetAgents();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetAgents() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetAgents() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetAgents() communication error.",ce); }
            return agents;
        }
        public static AgentDS GetAgents(string clientNumber) {
            //Get a list of agents for the specified client
            AgentDS agents = null;
            try {
                _Client = new IssueMgtServiceClient();
                agents = _Client.GetAgentsForClient(clientNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetAgents() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetAgents() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetAgents() communication error.",ce); }
            return agents;
        }
        public static StoreDS GetStoreDetail(int companyID,int storeNumber) {
            //Get a list of store locations
            StoreDS stores = null;
            try {
                _Client = new IssueMgtServiceClient();
                stores = _Client.GetStoreDetail(companyID,storeNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetStoreDetail() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetStoreDetail() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetStoreDetail() communication error.",ce); }
            return stores;
        }
        public static StoreDS GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            StoreDS stores = null;
            try {
                _Client = new IssueMgtServiceClient();
                stores = _Client.GetSubStoreDetail(companyID,subStore);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetStoreDetail() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetStoreDetail() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetStoreDetail() communication error.",ce); }
            return stores;
        }
        public static DeliveryDS GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            DeliveryDS deliveries = null;
            try {
                _Client = new IssueMgtServiceClient();
                deliveries = _Client.GetDeliveries(companyID,storeNumber,from,to);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetDeliveries() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDeliveries() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDeliveries() communication error.",ce); }
            return deliveries;
        }
        public static DeliveryDS GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID) {
            //Get a list of store locations
            DeliveryDS delivery = null;
            try {
                _Client = new IssueMgtServiceClient();
                delivery = _Client.GetDelivery(companyID,storeNumber,from,to,proID);
                _Client.Close();
            }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDelivery() timeout error.",te); }
            catch(FaultException fe) { throw new ApplicationException("GetDelivery() service error.",fe); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDelivery() communication error.",ce); }
            return delivery;
        }
        public static ScanDS GetOSDScans(long cProID) {
            //Get a list of store locations
            ScanDS scans = null;
            try {
                _Client = new IssueMgtServiceClient();
                scans = _Client.GetOSDScans(cProID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetOSDScans() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetOSDScans() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetOSDScans() communication error.",ce); }
            return scans;
        }
        public static ScanDS GetPODScans(long cProID) {
            //Get a list of store locations
            ScanDS scans = null;
            try {
                _Client = new IssueMgtServiceClient();
                scans = _Client.GetPODScans(cProID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetPODScans() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetPODScans() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetPODScans() communication error.",ce); }
            return scans;
        }
        
        public static ContactDS GetContacts() {
            //
            ContactDS contacts = null;
            try {
                _Client = new IssueMgtServiceClient();
                contacts = _Client.GetContacts();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetContacts() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetContacts() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetContacts() communication error.",ce); }
            return contacts;
        }
        public static ContactDS GetContacts(int companyID,string regionNumber,string districtNumber,string agentNumber,string storeNumber) {
            ContactDS contacts = null;
            try {
                _Client = new IssueMgtServiceClient();
                contacts = _Client.GetContactsForLocation(companyID,regionNumber,districtNumber,agentNumber,storeNumber);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetContacts() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetContacts() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetContacts() communication error.",ce); }
            return contacts;
        }
        public static Contact GetContact(int contactID) {
            //Get issue types
            Contact contact = null;
            try {
                _Client = new IssueMgtServiceClient();
                contact = _Client.GetContact(contactID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetContact() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetContact() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetContact() communication error.",ce); }
            return contact;
        }
        public static int CreateContact(Contact contact) {
            int id = 0;
            try {
                _Client = new IssueMgtServiceClient();
                id = _Client.CreateContact(contact);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("CreateContact() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("CreateContact() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("CreateContact() communication error.",ce); }
            return id;
        }
        public static bool UpdateContact(Contact contact) {
            bool res = false;
            try {
                _Client = new IssueMgtServiceClient();
                res = _Client.UpdateContact(contact);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UpdateContact() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UpdateContact() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UpdateContact() communication error.",ce); }
            return res;
        }
        #endregion
        
        public static IssueTypeDS GetIssueCategorys() {
            //Issue type category
            IssueTypeDS categorys = new IssueTypeDS();
            try {
                _Client = new IssueMgtServiceClient();
                categorys = _Client.GetIssueCategorys();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssueCategorys() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIssueCategorys() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIssueCategorys() communication error.",ce); }
            return categorys;
        }
        public static IssueTypeDS GetIssueTypes(string issueCategory) {
            //Issue types- all or filtered by category
            IssueTypeDS issueTypes=null;
            try {
                _Client = new IssueMgtServiceClient();
                issueTypes = _Client.GetIssueTypes(issueCategory);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssueTypes() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIssueTypes() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIssueTypes() communication error.",ce); }
            return issueTypes;
        }
        public static string GetIssueType(int typeID) {
            //Get an issue type  for the specified id
            string issueType = "";
            try {
                _Client = new IssueMgtServiceClient();
                    issueType = _Client.GetIssueType(typeID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssueType() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIssueType() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIssueType() communication error.",ce); }
            return issueType;
        }
        public static ActionTypeDS GetActionTypes() {
            //Action types
            ActionTypeDS actionTypes = new ActionTypeDS();
            try {
                _Client = new IssueMgtServiceClient();
                    actionTypes = _Client.GetActionTypes();
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetActionTypes() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetActionTypes() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetActionTypes() communication error.",ce); }
            return actionTypes;
        }
        public static ActionTypeDS GetActionTypes(long issueID) {
            //Action types for an issue (state driven)
            ActionTypeDS actionTypes=null;
            try {
                _Client = new IssueMgtServiceClient();
                    actionTypes = _Client.GetIssueActionTypes(issueID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetActionTypes() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetActionTypes() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetActionTypes() communication error.",ce); }
            return actionTypes;
        }
        public static string GetActionType(int typeID) {
            //Get an action type  for the specified id
            string actionType = "";
            try {
                _Client = new IssueMgtServiceClient();
                actionType = _Client.GetActionType(typeID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetActionType() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetActionType() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetActionType() communication error.",ce); }
            return actionType;
        }
        
        public static DataSet GetIssues() {
            //Get issues
            IssueDS issues=new IssueDS();
            try {
                if(_IssueCache == null) _IssueCacheLastUpdate = DateTime.Today.AddDays(-IssueDaysBack);
                DateTime fromDate = _IssueCacheLastUpdate;

                _Client = new IssueMgtServiceClient();
                DataSet ds = _Client.GetIssuesForDate(fromDate);
                _Client.Close();
                
                System.Diagnostics.Debug.WriteLine("PAYLOAD: fromDate=" + fromDate.ToString("MM/dd/yyyy HH:mm:ss") + "; bytes=" + ds.GetXml().Length);
                updateIssueCache(ds);
                if(_IssueCache != null) issues.Merge(_IssueCache);
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssueCategorys() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIssueCategorys() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIssueCategorys() communication error.",ce); }
            return issues;
        }
        private static void updateIssueCache(DataSet ds) {
            //Get last issue time
            if(ds.Tables["IssueTable"].Rows.Count > 0) {
                DateTime lastUpdated = _IssueCacheLastUpdate;
                if(_IssueCache == null) _IssueCache = new IssueDS();
                IssueDS issues = new IssueDS();
                issues.Merge(ds);
                for(int i=0;i<issues.IssueTable.Rows.Count;i++) {
                    IssueDS.IssueTableRow issue = issues.IssueTable[i];
                    IssueDS.IssueTableRow[] _issues = (IssueDS.IssueTableRow[])_IssueCache.IssueTable.Select("ID=" + issue.ID);
                    if(_issues.Length == 0) {
                        IssueDS.IssueTableRow _issue = _IssueCache.IssueTable.NewIssueTableRow();
                        #region New to cache
                        _issue.ID = issue.ID;
                        if(!issue.IsAgentNumberNull()) _issue.AgentNumber = issue.AgentNumber;
                        if(!issue.IsCompanyIDNull()) _issue.CompanyID = issue.CompanyID;
                        if(!issue.IsCompanyNameNull()) _issue.CompanyName = issue.CompanyName;
                        if(!issue.IsContactIDNull()) _issue.ContactID = issue.ContactID;
                        if(!issue.IsContactNameNull()) _issue.ContactName = issue.ContactName;
                        if(!issue.IsCoordinatorNull()) _issue.Coordinator = issue.Coordinator;
                        if(!issue.IsDistrictNumberNull()) _issue.DistrictNumber = issue.DistrictNumber;
                        if(!issue.IsFirstActionCreatedNull()) _issue.FirstActionCreated = issue.FirstActionCreated;
                        if(!issue.IsFirstActionDescriptionNull()) _issue.FirstActionDescription = issue.FirstActionDescription;
                        if(!issue.IsFirstActionIDNull()) _issue.FirstActionID = issue.FirstActionID;
                        if(!issue.IsFirstActionUserIDNull()) _issue.FirstActionUserID = issue.FirstActionUserID;
                        if(!issue.IsLastActionCreatedNull()) _issue.LastActionCreated = issue.LastActionCreated;
                        if(!issue.IsLastActionDescriptionNull()) _issue.LastActionDescription = issue.LastActionDescription;
                        if(!issue.IsLastActionIDNull()) _issue.LastActionID = issue.LastActionID;
                        if(!issue.IsLastActionUserIDNull()) _issue.LastActionUserID = issue.LastActionUserID;
                        if(!issue.IsOFD1FromDateNull()) _issue.OFD1FromDate = issue.OFD1FromDate;
                        if(!issue.IsOFD1ToDateNull()) _issue.OFD1ToDate = issue.OFD1ToDate;
                        if(!issue.IsPROIDNull()) _issue.PROID = issue.PROID;
                        if(!issue.IsStoreNumberNull()) _issue.StoreNumber = issue.StoreNumber;
                        if(!issue.IsSubjectNull()) _issue.Subject = issue.Subject;
                        if(!issue.IsTypeNull()) _issue.Type = issue.Type;
                        if(!issue.IsTypeIDNull()) _issue.TypeID = issue.TypeID;
                        if(!issue.IsZoneNull()) _issue.Zone = issue.Zone;
                        #endregion
                        _IssueCache.IssueTable.AddIssueTableRow(_issue);
                        Debug.WriteLine("CACHE: New issue#" + _issue.ID.ToString() + "; lastActionCreated=" + _issue.LastActionCreated.ToString("MM/dd/yyyy HH:mm:ss"));
                    }
                    else {
                        //Existing in cache
                        if(issue.LastActionCreated.CompareTo(_issues[0].LastActionCreated) > 0) {
                            #region Update existing
                            if(!issue.IsAgentNumberNull()) _issues[0].AgentNumber = issue.AgentNumber;
                            if(!issue.IsCompanyIDNull()) _issues[0].CompanyID = issue.CompanyID;
                            if(!issue.IsCompanyNameNull()) _issues[0].CompanyName = issue.CompanyName;
                            if(!issue.IsContactIDNull()) _issues[0].ContactID = issue.ContactID;
                            if(!issue.IsContactNameNull()) _issues[0].ContactName = issue.ContactName;
                            if(!issue.IsCoordinatorNull()) _issues[0].Coordinator = issue.Coordinator;
                            if(!issue.IsDistrictNumberNull()) _issues[0].DistrictNumber = issue.DistrictNumber;
                            if(!issue.IsFirstActionCreatedNull()) _issues[0].FirstActionCreated = issue.FirstActionCreated;
                            if(!issue.IsFirstActionDescriptionNull()) _issues[0].FirstActionDescription = issue.FirstActionDescription;
                            if(!issue.IsFirstActionIDNull()) _issues[0].FirstActionID = issue.FirstActionID;
                            if(!issue.IsFirstActionUserIDNull()) _issues[0].FirstActionUserID = issue.FirstActionUserID;
                            if(!issue.IsLastActionCreatedNull()) _issues[0].LastActionCreated = issue.LastActionCreated;
                            if(!issue.IsLastActionDescriptionNull()) _issues[0].LastActionDescription = issue.LastActionDescription;
                            if(!issue.IsLastActionIDNull()) _issues[0].LastActionID = issue.LastActionID;
                            if(!issue.IsLastActionUserIDNull()) _issues[0].LastActionUserID = issue.LastActionUserID;
                            if(!issue.IsOFD1FromDateNull()) _issues[0].OFD1FromDate = issue.OFD1FromDate;
                            if(!issue.IsOFD1ToDateNull()) _issues[0].OFD1ToDate = issue.OFD1ToDate;
                            if(!issue.IsPROIDNull()) _issues[0].PROID = issue.PROID;
                            if(!issue.IsStoreNumberNull()) _issues[0].StoreNumber = issue.StoreNumber;
                            if(!issue.IsSubjectNull()) _issues[0].Subject = issue.Subject;
                            if(!issue.IsTypeNull()) _issues[0].Type = issue.Type;
                            if(!issue.IsTypeIDNull()) _issues[0].TypeID = issue.TypeID;
                            if(!issue.IsZoneNull()) _issues[0].Zone = issue.Zone;
                            _issues[0].AcceptChanges();
                            #endregion
                            Debug.WriteLine("CACHE: Updated issue#" + _issues[0].ID.ToString() + "; lastActionCreated=" + _issues[0].LastActionCreated.ToString("MM/dd/yyyy HH:mm:ss"));
                        }
                    }
                    if(issue.LastActionCreated.CompareTo(lastUpdated) > 0 && issue.LastActionCreated.CompareTo(_IssueCacheLastUpdate) > 0)
                        _IssueCacheLastUpdate = issue.LastActionCreated;
                }
            }
        }
        public static Issue GetIssue(long issueID) {
            //Get an existing issue
            Issue issue = null;
            try {
                _Client = new IssueMgtServiceClient();
                issue = _Client.GetIssue(issueID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssue() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIssue() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIssue() communication error.",ce); }
            return issue;
        }
        public static Action[] GetIssueActions(long issueID) {
            //Get all actions for the specified issue
            Action[] actions = null;
            try {
                _Client = new IssueMgtServiceClient();
                actions = _Client.GetIssueActions(issueID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssueActions() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIssueActions() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIssueActions() communication error.",ce); }
            return actions;
        }
        public static Action[] GetActions(long issueID,long actionID) {
            //Get all actions chronologically prior to and including the specified action for the specified issue
            Action[] actions = null;
            try {
                _Client = new IssueMgtServiceClient();
                actions = _Client.GetActions(issueID,actionID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetActions() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetActions() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetActions() communication error.",ce); }
            return actions;
        }
        public static Attachment[] GetIssueAttachments(long issueID) {
            //Get attachments for the specified issue
            Attachment[] attachments = null;
            try {
                _Client = new IssueMgtServiceClient();
                attachments = _Client.GetIssueAttachments(issueID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssueAttachments() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetIssueAttachments() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetIssueAttachments() communication error.",ce); }
            return attachments;
        }
        public static Attachment[] GetAttachments(long issueID,long actionID) {
            //Get attachments for the specified action
            Attachment[] attachments = null;
            try {
                _Client = new IssueMgtServiceClient();
                attachments = _Client.GetAttachments(issueID,actionID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetAttachments() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetAttachments() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetAttachments() communication error.",ce); }
            return attachments;
        }
        public static byte[] GetAttachment(int id) {
            //Get an existing file attachment from database
            byte[] bytes=null;
            try {
                _Client = new IssueMgtServiceClient();
                bytes = _Client.GetAttachment(id);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetAttachment() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetAttachment() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetAttachment() communication error.",ce); }
            return bytes;
        }
        public static long CreateIssue(Issue issue) {
            //Create a new issue
            long iid = 0;
            try {
                _Client = new IssueMgtServiceClient();
                iid = _Client.CreateIssue(issue);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("CreateIssue() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("CreateIssue() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("CreateIssue() communication error.",ce); }
            return iid;
        }
        public static bool CreateIssueAction(byte typeID,long issueID,string userID,string comment) {
            //Create a new action
            bool b = false;
            try {
                _Client = new IssueMgtServiceClient();
                b = _Client.CreateIssueAction(typeID,issueID,userID,comment);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("CreateIssueAction() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("CreateIssueAction() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("CreateIssueAction() communication error.",ce); }
            return b;
        }
        public static bool CreateIssueAttachment(string name,byte[] bytes,long actionID) {
            //Create a new issue
            bool saved=false;
            try {
                _Client = new IssueMgtServiceClient();
                saved = _Client.CreateIssueAttachment(name,bytes,actionID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("CreateIssueAttachment() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("CreateIssueAttachment() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("CreateIssueAttachment() communication error.",ce); }
            return saved;
        }
        public static bool UpdateIssue(Issue issue) {
            //Update an existing issue
            bool b = false;
            try {
                _Client = new IssueMgtServiceClient();
                b = _Client.UpdateIssue(issue);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("UpdateIssue() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("UpdateIssue() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("UpdateIssue() communication error.",ce); }
            return b;
        }
        public static IssueDS IssueHistory(Issue issue) {
            //Get issue history data
            IssueDS history = new IssueDS();
            try {
                _Client = new IssueMgtServiceClient();
                history = new IssueDS(); //_Client.IssueHistory(issue);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("IssueHistory() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("IssueHistory() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("IssueHistory() communication error.",ce); }
            return history;
        }
        public static IssueDS SearchIssues(string searchText) {
            //Get issue search data
            IssueDS search = new IssueDS();
            try {
                _Client = new IssueMgtServiceClient();
                search = _Client.SearchIssues(searchText);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("SearchIssues() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("SearchIssues() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("SearchIssues() communication error.",ce); }
            return search;
        }
        public static IssueDS SearchIssuesAdvanced(object[] criteria) {
            //Get issue search data
            IssueDS search = new IssueDS();
            try {
                _Client = new IssueMgtServiceClient();
                search = _Client.SearchIssuesAdvanced(criteria);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("SearchIssuesAdvanced() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("SearchIssuesAdvanced() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("SearchIssuesAdvanced() communication error.",ce); }
            return search;
        }
    }
}