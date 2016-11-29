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
        
		//Interface
        public CustomerProxy() { }

        public Terminal[] GetTerminals(string agentNumber) {
            //Returns a list of terminals
            Terminal[] terminals = null;
            IssueMgtServiceClient _Client = null;
            try {
                _Client = new IssueMgtServiceClient();
                Terminal[] ts = _Client.GetTerminals();
                                
                if (agentNumber == null) {
                    terminals = new Terminal[ts.Length + 1];

                    Terminal all = new Terminal();
                    all.AgentID = "";
                    all.Description = "All";
                    terminals[0] = all;

                    for (int i = 0; i < ts.Length; i++) {  terminals[i+1] = ts[i];  }
                }
                else {
                    terminals = new Terminal[1];
                    for (int i = 0; i < ts.Length; i++) {
                        if (ts[i].AgentID == agentNumber) { terminals[0] = ts[i]; break; }
                    }
                }
                _Client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetTerminals() service error.", fe); }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetTerminals() timeout error.", te); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetTerminals() communication error.", ce); }
            return terminals;
        }
        public IssueDS GetIssues() {
            //Get issue search data
            IssueMgtServiceClient client=null;
            IssueDS issues=null;
            try {
                client = new IssueMgtServiceClient();
                issues = client.GetIssues();
                client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssues() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("GetIssues() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("GetIssues() communication error.",ce); }
            return issues;
        }
        public IssueDS GetIssues(string agentNumber) {
            //Get issue search data
            IssueMgtServiceClient client = null;
            IssueDS issues = null;
            try {
                client = new IssueMgtServiceClient();
                if(agentNumber == null)
                    issues = client.GetIssues();
                else
                    issues = client.GetIssuesForAgent(agentNumber);
                client.Close();
            }
            catch (FaultException fe) { throw new ApplicationException("GetIssues() service error.", fe); }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException("GetIssues() timeout error.", te); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException("GetIssues() communication error.", ce); }
            return issues;
        }
        public IssueDS SearchIssues(string agentNumber, string searchText) {
            //Get issue search data
            IssueMgtServiceClient client=null;
            IssueDS issues = new IssueDS();
            try {
                if(searchText.Trim().Length > 0) {
                    client = new IssueMgtServiceClient();
                    if(agentNumber == null)
                        issues = client.SearchIssues(searchText);
                    else
                        issues = client.SearchIssuesForAgent(agentNumber, searchText);
                    client.Close();
                }
            }
            catch(FaultException fe) { throw new ApplicationException("SearchIssues() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("SearchIssues() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("SearchIssues() communication error.",ce); }
            return issues;
        }
        public IssueDS SearchIssuesAdvanced(string agentNumber, object[] criteria) {
            //Get issue search data
            IssueMgtServiceClient client=null;
            IssueDS issues = new IssueDS();
            try {
                client = new IssueMgtServiceClient();
                if (agentNumber == null)
                    issues = client.SearchIssuesAdvanced(criteria);
                else
                    issues = client.SearchIssuesAdvancedForAgent(agentNumber, criteria);
                client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("SearchIssuesAdvanced() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("SearchIssuesAdvanced() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("SearchIssuesAdvanced() communication error.",ce); }
            return issues;
        }
        public IssueDS SearchIssuesAdvanced(string agentNumber, string zone, string store, string agent, string company, string type, string action, string received, string subject, string contact, string originator, string coordinator) {
            //Get issue search data
            string _zone = (zone != null && zone.Trim().Length > 0) ? zone : null;
            string _store = (store != null && store.Trim().Length > 0) ? store : null;
            string _agent = (agent != null && agent.Trim().Length > 0) ? agent : null;
            string _company = (company != null && company.Trim().Length > 0) ? company : null;
            string _type = (type != null && type.Trim().Length > 0) ? type : null;
            string _action = (action != null && action.Trim().Length > 0) ? action : null;
            string _received = (received != null && received.Trim().Length > 0) ? received : null;
            string _subject = (subject != null && subject.Trim().Length > 0) ? subject : null;
            string _contact = (contact != null && contact.Trim().Length > 0) ? contact : null;
            string _originator = (originator != null && originator.Trim().Length > 0) ? originator : null;
            string _coordinator = (coordinator != null && coordinator.Trim().Length > 0) ? coordinator : null;
            if(_zone==null && _store==null && _agent==null && _company==null && _type==null && _action==null && _received==null && _subject==null && _contact==null && _originator==null && _coordinator==null)
                return new IssueDS();
            object[] criteria = new object[] { _zone,_store,_agent,_company,_type,_action,_received,_subject,_contact,_originator,_coordinator };
            return SearchIssuesAdvanced(agentNumber, criteria);
        }
        public Action[] GetActions(long issueID,long actionID) {
            //Get issue actions
            IssueMgtServiceClient client=null;
            Action[] actions=null;
            try {
                client = new IssueMgtServiceClient();
                actions = client.GetActions(issueID,actionID);
                for(int i=0;i<actions.Length;i++) {
                    actions[i].Comment = actions[i].Comment.Replace("\n","<br />");
                }
                client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetActions() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("GetActions() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("GetActions() communication error.",ce); }
            return actions;
        }
        public Action[] GetIssueActions(long issueID) {
            //Get issue actions
            IssueMgtServiceClient client=null;
            Action[] actions=null;
            try {
                client = new IssueMgtServiceClient();
                actions = client.GetIssueActions(issueID);
                for(int i=0;i<actions.Length;i++) {
                    actions[i].Comment = actions[i].Comment.Replace("\n","<br />");
                }
                client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetActions() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("GetActions() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("GetActions() communication error.",ce); }
            return actions;
        }
        public AgentDS GetAgentsForClient(string clientNumber, string agentNumber) {
            //Get issue search data
            IssueMgtServiceClient client = null;
            AgentDS agents = null;
            try {
                agents = new AgentDS();
                client = new IssueMgtServiceClient();
                AgentDS _agents = client.GetAgentsForClient(clientNumber);
                if(agentNumber == null)
                    agents.Merge(_agents);
                else
                    agents.Merge(_agents.AgentTable.Select("AgentNumber='" + agentNumber + "'"));
                client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetAgentsForClient() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("GetAgentsForClient() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("GetAgentsForClient() communication error.",ce); }
            return agents;
        }
        public IssueTypeDS GetIssueCategorys(string agentNumber) {
            //Get issue categories
            IssueMgtServiceClient client = null;
            IssueTypeDS cats = null;
            try {
                cats = new IssueTypeDS();
                client = new IssueMgtServiceClient();
                IssueTypeDS _cats = client.GetIssueCategorys();
                if(agentNumber == null)
                    cats.Merge(_cats);
                else
                    cats.Merge(_cats.IssueTypeTable.Select("Category='Agent/Local'"));
                client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("GetIssueCategorys() service error.",fe); }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException("GetIssueCategorys() timeout error.",te); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException("GetIssueCategorys() communication error.",ce); }
            return cats;
        }
    }
}