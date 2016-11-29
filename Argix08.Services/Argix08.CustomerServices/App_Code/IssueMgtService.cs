using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Customers {
	//
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class IssueMgtService:IIssueMgtService {
        //Members

        //Interface
        public IssueMgtService() { }

        public Argix.Enterprise.TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            return new Argix.Enterprise.EnterpriseService().GetTerminalInfo();
        }
        public Argix.Enterprise.Terminals GetTerminals() {
            //Get the operating enterprise terminal
            return new Argix.Enterprise.EnterpriseService().GetTerminals();
        }
        public IssueTypeDS GetIssueCategorys() {
            //Issue type category
            return new Argix.Customers.CustomerService().GetIssueCategorys();
        }
        public IssueTypeDS GetIssueTypes(string issueCategory) {
            //Issue types- all or filtered by category
            return new Argix.Customers.CustomerService().GetIssueTypes(issueCategory);
        }
        public string GetIssueType(int typeID) {
            //Get an issue type  for the specified id
            return new Argix.Customers.CustomerService().GetIssueType(typeID);
        }
        public ActionTypeDS GetActionTypes() { 
            //Action types
            return new Argix.Customers.CustomerService().GetActionTypes();
        }
        public ActionTypeDS GetActionTypes(long issueID) {
            //Action types for an issue (state driven)
            return new Argix.Customers.CustomerService().GetActionTypes(issueID);
        }
        public string GetActionType(int typeID) {
            //Get an action type  for the specified id
            return new Argix.Customers.CustomerService().GetActionType(typeID);
        }

        public IssueDS GetIssues() { return new Argix.Customers.CustomerService().GetIssues(); }
        public IssueDS GetIssues(DateTime fromDate) {
            //Get issues
            return new Argix.Customers.CustomerService().GetIssues(fromDate);
        }
        public IssueDS GetIssues(string agentNumber) {
            //Get issues
            IssueDS issues = new IssueDS();
            IssueDS _issues = new Argix.Customers.CustomerService().GetIssues();
            issues.Merge(_issues.IssueTable.Select("AgentNumber='" + agentNumber + "'"));
            return issues;
        }
        public Issue GetIssue(long issueID) {
            //Get an existing issue
            return new Argix.Customers.CustomerService().GetIssue(issueID);
        }
        public Actions GetIssueActions(long issueID) {
            //Get all actions for the specified issue
            return new Argix.Customers.CustomerService().GetIssueActions(issueID);
        }
        public Actions GetActions(long issueID,long actionID) {
            //Get all actions chronologically prior to and including the specified action for the specified issue
            return new Argix.Customers.CustomerService().GetActions(issueID,actionID);
        }
        public Attachments GetIssueAttachments(long issueID) {
            //Get attachments for the specified issue
            return new Argix.Customers.CustomerService().GetIssueAttachments(issueID);
        }
        public Attachments GetAttachments(long issueID,long actionID) {
            //Get attachments for the specified action
            return new Argix.Customers.CustomerService().GetAttachments(issueID,actionID);
        }
        public byte[] GetAttachment(int id) {
            //Get an existing file attachment from database
            return new Argix.Customers.CustomerService().GetAttachment(id);
        }
        public long CreateIssue(Issue issue) {
            //Create a new issue
            return new Argix.Customers.CustomerService().CreateIssue(issue);
        }
        public bool CreateIssueAction(byte typeID,long issueID, string userID, string comment) {
            //Create a new action
            return new Argix.Customers.CustomerService().CreateIssueAction(typeID,issueID,userID,comment);
        }
        public bool CreateIssueAttachment(string name,byte[] bytes,long actionID) {
            //Create a new issue
            return new Argix.Customers.CustomerService().CreateIssueAttachment(name,bytes,actionID);
        }
        public bool UpdateIssue(Issue issue) {
            //Update an existing issue
            return new Argix.Customers.CustomerService().UpdateIssue(issue);
        }
        public IssueDS SearchIssues(string searchText) {
            //Get issue search data
            return new Argix.Customers.CustomerService().SearchIssues(searchText);
        }
        public IssueDS SearchIssues(string agentNumber, string searchText) {
            //Get issue search data
            IssueDS issues = new IssueDS();
            IssueDS _issues = new Argix.Customers.CustomerService().SearchIssues(searchText);
            issues.Merge(_issues.IssueTable.Select("AgentNumber='" + agentNumber + "'"));
            return issues;
        }
        public IssueDS SearchIssuesAdvanced(object[] criteria) {
            //Get issue search data
            return new Argix.Customers.CustomerService().SearchIssuesAdvanced(criteria);
        }
        public IssueDS SearchIssuesAdvanced(string agentNumber, object[] criteria) {
            //Get issue search data
            IssueDS issues = new IssueDS();
            IssueDS _issues = new Argix.Customers.CustomerService().SearchIssuesAdvanced(criteria);
            issues.Merge(_issues.IssueTable.Select("AgentNumber='" + agentNumber + "'"));
            return issues;
        }
        public CompanyDS GetCompanies() {
            //Get a list of all companies
            return new Argix.Enterprise.EnterpriseService().GetCompanies();
        }
        public CompanyDS GetCompanies(bool activeOnly) {
            //Get a list of active companies
            return new Argix.Enterprise.EnterpriseService().GetCompanies(activeOnly);
        }
        public string GetCompany(int companyID) {
            //Get a company for the specified id
            return new Argix.Enterprise.EnterpriseService().GetCompany(companyID);
        }
        public LocationDS GetDistricts(string clientNumber) {
            //Get a list of client divisions
            return new Argix.Enterprise.EnterpriseService().GetDistricts(clientNumber);
        }
        public LocationDS GetRegions(string clientNumber) {
            //Get a list of client divisions
            return new Argix.Enterprise.EnterpriseService().GetRegions(clientNumber);
        }
        public AgentDS GetAgents() {
            //
            return new Argix.Enterprise.EnterpriseService().GetAgents();
        }
        public AgentDS GetAgents(string clientNumber) {
            //Get a list of agents for the specified client
            return new Argix.Enterprise.EnterpriseService().GetAgents(clientNumber);
        }
        public StoreDS GetStoreDetail(int companyID,int storeNumber) {
            //Get a list of store locations
            return new Argix.Enterprise.EnterpriseService().GetStoreDetail(companyID,storeNumber);
        }
        public StoreDS GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            return new Argix.Enterprise.EnterpriseService().GetStoreDetail(companyID,subStore);
        }
        
        public DeliveryDS GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            return new Argix.Freight.FreightService().GetDeliveries(companyID,storeNumber,from,to);
        }
        public DeliveryDS GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID) {
            //Get a list of store locations
            return new Argix.Freight.FreightService().GetDelivery(companyID,storeNumber,from,to,proID);
        }
        public ScanDS GetOSDScans(long cProID) {
            //Get a list of store locations
            return new Argix.Freight.FreightService().GetOSDScans(cProID);
        }
        public ScanDS GetPODScans(long cProID) {
            //Get a list of store locations
            return new Argix.Freight.FreightService().GetPODScans(cProID);
        }
        
        public ContactDS GetContacts() {
            //Get a list of contacts
            return new Argix.Enterprise.EnterpriseService().GetContacts();
        }
        public ContactDS GetContacts(int companyID,string regionNumber,string districtNumber,string agentNumber,string storeNumber) {
            return new Argix.Enterprise.EnterpriseService().GetContacts(companyID,regionNumber,districtNumber,agentNumber,storeNumber);
        }
        public Argix.Enterprise.Contact GetContact(int contactID) {
            //Get issue types
            return new Argix.Enterprise.EnterpriseService().GetContact(contactID);
        }
        public int CreateContact(Argix.Enterprise.Contact contact) {
            return new Argix.Enterprise.EnterpriseService().CreateContact(contact);
        }
        public bool UpdateContact(Argix.Enterprise.Contact contact) {
            return new Argix.Enterprise.EnterpriseService().UpdateContact(contact);
        }
    }
}