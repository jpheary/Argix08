using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Customers {
    //
    [ServiceContract(Namespace="http://Argix.Customers")]
     public interface IIssueMgtService {
        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();
        [OperationContract]
        Argix.Enterprise.Terminals GetTerminals();
        
        [OperationContract]
        IssueTypeDS GetIssueCategorys();
        [OperationContract]
        IssueTypeDS GetIssueTypes(string issueCategory);
        [OperationContract]
        string GetIssueType(int typeID);
        [OperationContract]
        ActionTypeDS GetActionTypes();
        [OperationContract(Name="GetIssueActionTypes")]
        ActionTypeDS GetActionTypes(long issueID);
        [OperationContract]
        string GetActionType(int typeID);

        [OperationContract]
        IssueDS GetIssues();
        [OperationContract(Name="GetIssuesForDate")]
        IssueDS GetIssues(DateTime fromDate);
        [OperationContract(Name = "GetIssuesForAgent")]
        IssueDS GetIssues(string agentNumber);
        [OperationContract]
        Issue GetIssue(long issueID);
        [OperationContract]
        Actions GetIssueActions(long issueID);
        [OperationContract]
        Actions GetActions(long issueID,long actionID);
        [OperationContract]
        Attachments GetIssueAttachments(long issueID);
        [OperationContract]
        Attachments GetAttachments(long issueID,long actionID);
        [OperationContract]
        byte[] GetAttachment(int id);
        [OperationContract]
        long CreateIssue(Issue issue);
        [OperationContract]
        bool CreateIssueAction(byte typeID,long issueID,string userID,string comment);
        [OperationContract]
        bool CreateIssueAttachment(string filename,byte[] bytes,long issueID);
        [OperationContract]
        bool UpdateIssue(Issue issue);
        [OperationContract]
        IssueDS SearchIssues(string searchText);
        [OperationContract(Name = "SearchIssuesForAgent")]
        IssueDS SearchIssues(string agentNumber, string searchText);
        [OperationContract]
        IssueDS SearchIssuesAdvanced(object[] criteria);
        [OperationContract(Name = "SearchIssuesAdvancedForAgent")]
        IssueDS SearchIssuesAdvanced(string agentNumber, object[] criteria);

        [OperationContract]
        CompanyDS GetCompanies();
        [OperationContract(Name="GetActiveCompanies")]
        CompanyDS GetCompanies(bool activeOnly);
        [OperationContract]
        string GetCompany(int companyID);
        [OperationContract]
        LocationDS GetDistricts(string clientNumber);
        [OperationContract]
        LocationDS GetRegions(string clientNumber);
        [OperationContract]
        AgentDS GetAgents();
        [OperationContract(Name="GetAgentsForClient")]
        AgentDS GetAgents(string clientNumber);
        [OperationContract]
        StoreDS GetStoreDetail(int companyID,int storeNumber);
        [OperationContract(Name="GetSubStoreDetail")]
        StoreDS GetStoreDetail(int companyID,string subStore);

        [OperationContract]
        DeliveryDS GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to);
        [OperationContract]
        DeliveryDS GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID);
        [OperationContract]
        ScanDS GetOSDScans(long cProID);
        [OperationContract]
        ScanDS GetPODScans(long cProID);

        [OperationContract]
        ContactDS GetContacts();
        [OperationContract(Name="GetContactsForLocation")]
        ContactDS GetContacts(int companyID,string regionNumber,string districtNumber,string agentNumber,string storeNumber);
        [OperationContract]
        Argix.Enterprise.Contact GetContact(int contactID);
        [OperationContract]
        int CreateContact(Argix.Enterprise.Contact contact);
        [OperationContract]
        bool UpdateContact(Argix.Enterprise.Contact contact);
    }
}
