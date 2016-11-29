//	File:	CustomerService.cs
//	Author:	J. Heary
//	Date:	01/08/09
//	Desc:	A global object for building model objects and for database access.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
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
    public class CustomerService:ICustomerService {
        //Members
        public const string USP_ISSUES = "uspIMIssueGetList3",TBL_ISSUES = "IssueTable";
        public const string USP_ISSUETYPES = "uspIMIssueTypesGetList",TBL_ISSUETYPES = "IssueTypeTable";
        public const string USP_ISSUE_GET = "uspIMIssueGet2",TBL_ISSUE = "IssueTable";
        public const string USP_ISSUES_SEARCH = "uspIMIssueSearchGetList";
        public const string USP_ISSUES_SEARCHADVANCED = "uspIMIssueSearchAdvancedGetList";
        public const string USP_ISSUE_NEW = "uspIMIssueNew";
        public const string USP_ISSUE_UPDATE = "uspIMIssueUpdate2";
        public const string USP_ACTIONS = "uspIMActionGetList3",TBL_ACTIONS = "ActionTable";
        public const string USP_ACTIONTYPES = "uspIMActionTypesGetList",TBL_ACTIONTYPES = "ActionTypeTable";
        public const string USP_ACTION_NEW = "uspIMActionNew";
        public const string USP_ATTACHMENTS = "uspIMAttachmentGetList",TBL_ATTACHMENTS="AttachmentTable";
        public const string USP_ATTACHMENT_GET = "uspIMAttachmentGet2";
        public const string USP_ATTACHMENT_NEW = "uspIMAttachmentNew";

        //Interface
        public CustomerService() { }

        public IssueTypeDS GetIssueCategorys() {
            //Issue type category
            IssueTypeDS categorys = new IssueTypeDS();
            try {
                IssueTypeDS issueTypes = GetIssueTypes("");
                Hashtable groups = new Hashtable();
                for(int i=0;i<issueTypes.IssueTypeTable.Rows.Count;i++) {
                    if(!groups.ContainsKey(issueTypes.IssueTypeTable[i].Category)) {
                        groups.Add(issueTypes.IssueTypeTable[i].Category,issueTypes.IssueTypeTable[i].Category);
                        categorys.IssueTypeTable.AddIssueTypeTableRow(0, "", issueTypes.IssueTypeTable[i].Category, "");
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue categories.",ex); }
            return categorys;
        }
        public IssueTypeDS GetIssueTypes(string issueCategory) {
            //Issue types- all or filtered by category
            IssueTypeDS issueTypes = new IssueTypeDS();
            try {
                DataSet ds = fillDataset(USP_ISSUETYPES,TBL_ISSUETYPES,new object[] { });
                if(ds.Tables[TBL_ISSUETYPES].Rows.Count > 0) {
                    if(issueCategory != null && issueCategory.Trim().Length > 0) {
                        IssueTypeDS _ds = new IssueTypeDS();
                        _ds.Merge(ds.Tables[TBL_ISSUETYPES].Select("Category='" + issueCategory + "'"));
                        issueTypes.Merge(_ds);
                    }
                    else
                        issueTypes.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue types.",ex); }
            return issueTypes;
        }
        public string GetIssueType(int typeID) {
            //Get an issue type  for the specified id
            string issueType = "";
            try {
                IssueTypeDS issueTypes = GetIssueTypes("");
                IssueTypeDS.IssueTypeTableRow it = (IssueTypeDS.IssueTypeTableRow)issueTypes.IssueTypeTable.Select("ID=" + typeID)[0];
                issueType = it.Type;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue type.",ex); }
            return issueType;
        }
        public ActionTypeDS GetActionTypes() { 
            //Action types
            ActionTypeDS actionTypes = new ActionTypeDS();
            try {
                DataSet ds = fillDataset(USP_ACTIONTYPES,TBL_ACTIONTYPES,new object[]{});
                if(ds.Tables[TBL_ACTIONTYPES].Rows.Count > 0) actionTypes.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading action types.",ex); }
            return actionTypes;
        }
        public ActionTypeDS GetActionTypes(long issueID) {
            //Action types for an issue (state driven)
            ActionTypeDS actionTypes=null;
            try {
                //Get full list
                actionTypes = GetActionTypes();

                //Remove actions that don't apply
                Actions actions = GetIssueActions(issueID);
                if(actions.Count == 0) {
                    //New: Open only
                    for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++)
                        if(actionTypes.ActionTypeTable[i].ID != 1) actionTypes.ActionTypeTable[i].Delete();
                }
                else if(actions.Count == 1) {
                    //Open: Dismiss, Notify All, Notify Agent Systems, Notify CRG
                    for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++) {
                        if(actionTypes.ActionTypeTable[i].ID == 1) actionTypes.ActionTypeTable[i].Delete();
                        else if(actionTypes.ActionTypeTable[i].ID == 3) actionTypes.ActionTypeTable[i].Delete();
                    }
                }
                else if(actions.Count > 1) {
                    //Unresolved: Close, Notify All, Notify Agent Systems, Notify CRG, Other
                    for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++) {
                        if(actionTypes.ActionTypeTable[i].ID == 1) actionTypes.ActionTypeTable[i].Delete();
                        else if(actionTypes.ActionTypeTable[i].ID == 2) actionTypes.ActionTypeTable[i].Delete();
                    }
                }
                actionTypes.AcceptChanges();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading action types.",ex); }
            return actionTypes;
        }
        public string GetActionType(int typeID) {
            //Get an action type  for the specified id
            string actionType = "";
            try {
                ActionTypeDS actionTypes = GetActionTypes();
                ActionTypeDS.ActionTypeTableRow at = (ActionTypeDS.ActionTypeTableRow)actionTypes.ActionTypeTable.Select("ID=" + typeID)[0];
                actionType = at.Type;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading action type.",ex); }
            return actionType;
        }

        public IssueDS GetIssues() { return GetIssues(DateTime.Today.AddDays(-Convert.ToInt32(ConfigurationManager.AppSettings["IssueDaysBack"]))); }
        public IssueDS GetIssues(DateTime fromDate) {
            //Get issues
            IssueDS issues=new IssueDS();
            try {
                DateTime toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                DataSet ds = fillDataset(USP_ISSUES,TBL_ISSUES,new object[] { fromDate,toDate });
                if(ds.Tables[TBL_ISSUES].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_ISSUES].Select("","LastActionCreated DESC"));
                    issues.Merge(_ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issues.",ex); }
            return issues;
        }
        public Issue GetIssue(long issueID) {
            //Get an existing issue
            Issue issue = null;
            try {
                //New or existing?
                if(issueID == 0) {
                    //Build a new issue object
                    issue = new Issue();
                }
                else {
                    //Build an issue object (including actions and attachments)
                    IssueDS issues = new IssueDS();
                    DataSet ds = fillDataset(USP_ISSUE_GET,TBL_ISSUE,new object[] { issueID });
                    if(ds.Tables[TBL_ISSUE].Rows.Count == 0)
                        throw new ApplicationException("Issue not found.");
                    else {
                        issues.Merge(ds);
                    }
                    issue = new Issue(issues.IssueTable[0]);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue.",ex); }
            return issue;
        }
        public Actions GetIssueActions(long issueID) {
            //Get all actions for the specified issue
            Actions actions = null;
            try {
                //New or existing?
                actions = new Actions();
                if(issueID > 0) {
                    DataSet ds = fillDataset(USP_ACTIONS,TBL_ACTIONS,new object[] { issueID });
                    if(ds.Tables[TBL_ACTIONS].Rows.Count == 0)
                        throw new ApplicationException("No actions found for this issue.");
                    else {
                        DataSet _ds = new DataSet();
                        _ds.Merge(ds.Tables[TBL_ACTIONS].Select("","Created DESC"));
                        IssueDS actionsDS = new IssueDS();
                        actionsDS.Merge(_ds);
                        for(int i=0;i<actionsDS.ActionTable.Rows.Count;i++) {
                            Action action = new Action(actionsDS.ActionTable[i]);
                            actions.Add(action);
                        }
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue actions.",ex); }
            return actions;
        }
        public Actions GetActions(long issueID,long actionID) {
            //Get all actions chronologically prior to and including the specified action for the specified issue
            Actions actions = null;
            try {
                actions = new Actions();
                if(issueID > 0) {
                    DataSet ds = fillDataset(USP_ACTIONS,TBL_ACTIONS,new object[] { issueID });
                    if(ds.Tables[TBL_ACTIONS].Rows.Count == 0)
                        throw new ApplicationException("No actions found for this issue.");
                    else {
                        DataSet _ds = new DataSet();
                        _ds.Merge(ds.Tables[TBL_ACTIONS].Select("","Created DESC"));
                        IssueDS actionsDS = new IssueDS();
                        actionsDS.Merge(_ds);

                        //Select all actions chronologically prior to and including specified action
                        //Note: Return all actions if specified action is not found
                        IssueDS.ActionTableRow[] rows = (IssueDS.ActionTableRow[])actionsDS.ActionTable.Select("ID=" + actionID.ToString());
                        Action _action = rows.Length > 0 ? new Action(rows[0]) : null;
                        for(int i=0;i<actionsDS.ActionTable.Rows.Count;i++) {
                            Action action = new Action(actionsDS.ActionTable[i]);
                            if(_action == null || DateTime.Compare(action.Created,_action.Created) <= 0) actions.Add(action);
                        }
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue actions.",ex); }
            return actions;
        }
        public Attachments GetIssueAttachments(long issueID) {
            //Get attachments for the specified issue
            Attachments attachments = null;
            try {
                //New or existing?
                attachments = new Attachments();
                if(issueID != 0) {
                    DataSet ds = fillDataset(USP_ATTACHMENTS,TBL_ATTACHMENTS,new object[] { issueID });
                    if(ds.Tables[TBL_ATTACHMENTS].Rows.Count != 0) {
                        IssueDS attachmentsDS = new IssueDS();
                        attachmentsDS.Merge(ds);
                        for(int i=0;i<attachmentsDS.AttachmentTable.Rows.Count;i++) {
                            Attachment attachment = new Attachment(attachmentsDS.AttachmentTable[i]);
                            attachments.Add(attachment);
                        }
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue attachments.",ex); }
            return attachments;
        }
        public Attachments GetAttachments(long issueID,long actionID) {
            //Get attachments for the specified action
            Attachments attachments = null;
            try {
                //Filter attachments
                attachments = new Attachments();
                if(issueID != 0) {
                    DataSet ds = fillDataset(USP_ATTACHMENTS,TBL_ATTACHMENTS,new object[] { issueID });
                    if(ds.Tables[TBL_ATTACHMENTS].Rows.Count != 0) {
                        IssueDS attachmentsDS = new IssueDS();
                        attachmentsDS.Merge(ds);
                        for(int i=0;i<attachmentsDS.AttachmentTable.Rows.Count;i++) {
                            Attachment attachment = new Attachment(attachmentsDS.AttachmentTable[i]);
                            if(attachment.ActionID == actionID) attachments.Add(attachment);
                        }
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue attachments.",ex); }
            return attachments;
        }
        public byte[] GetAttachment(int id) {
            //Get an existing file attachment from database
            byte[] bytes=null;
            try {
                //Search for image
                DataSet ds = fillDataset(USP_ATTACHMENT_GET,"AttachmentTable",new object[] { id });
                if(ds.Tables["AttachmentTable"].Rows.Count > 0) 
                    bytes = (byte[])ds.Tables["AttachmentTable"].Rows[0]["File"];
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading file attachment.",ex); }
            return bytes;
        }
        public long CreateIssue(Issue issue) {
            //Create a new issue
            long iid = 0;
            try {
                //Save issue
                object store = null;
                if(issue.StoreNumber > 0) store = issue.StoreNumber;
                object io = executeNonQueryWithReturn(USP_ISSUE_NEW,new object[] { null,issue.TypeID,issue.Subject,issue.ContactID,issue.CompanyID,issue.RegionNumber,issue.DistrictNumber,issue.AgentNumber,store,issue.OFD1FromDate,issue.OFD1ToDate,issue.PROID });
                iid = (long)io;

                //Add the single 'Open' action
                object ao = executeNonQueryWithReturn(USP_ACTION_NEW,new object[] { null,Action.ACTIONTYPE_OPEN,iid,issue.FirstActionUserID,"" });
                long aid = (long)ao;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new issue.",ex); }
            return iid;
        }
        public bool CreateIssueAction(byte typeID,long issueID, string userID, string comment) {
            //Create a new action
            bool b = false;
            try {
                //Validate
                if(issueID == 0) return false;

                //Add any new actions and associated attachments
                object ao = executeNonQueryWithReturn(USP_ACTION_NEW,new object[] { null,typeID,issueID,userID,comment });
                b = true;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while adding action.",ex); }
            return b;
        }
        public bool CreateIssueAttachment(string name,byte[] bytes,long actionID) {
            //Create a new issue
            bool saved=false;
            try {
                //Save issue
                object o = executeNonQueryWithReturn(USP_ATTACHMENT_NEW,new object[] { null,name,bytes,actionID });
                int id = (int)o;
                saved = (id > 0);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new attachment.",ex); }
            return saved;
        }
        public bool UpdateIssue(Issue issue) {
            //Update an existing issue
            bool b = false;
            try {
                //Update the updateable issue attributes
                executeNonQuery(USP_ISSUE_UPDATE,new object[] { issue.ID,issue.ContactID,issue.OFD1FromDate,issue.OFD1ToDate,issue.PROID });
                b = true;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating issue.",ex); }
            return b;
        }
        public IssueDS SearchIssues(string searchText) {
            //Get issue search data
            IssueDS search = new IssueDS();
            try {
                //Validate data access
                DataSet ds = fillDataset(USP_ISSUES_SEARCH,TBL_ISSUES,new object[] { searchText });
                if(ds.Tables[TBL_ISSUES].Rows.Count > 0) search.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while searching issues.",ex); }
            return search;
        }
        public IssueDS SearchIssuesAdvanced(object[] criteria) {
            //Get issue search data
            IssueDS search = new IssueDS();
            try {
                //Validate data access
                DataSet ds = fillDataset(USP_ISSUES_SEARCHADVANCED,TBL_ISSUES,criteria);
                if(ds.Tables[TBL_ISSUES].Rows.Count > 0) search.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while searching issues.",ex); }
            return search;
        }
        
        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        private object executeNonQueryWithReturn(string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase("SQLConnection");
                DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
                ret = db.ExecuteNonQuery(cmd);

                //Find the output parameter and return its value
                foreach(DbParameter param in cmd.Parameters) {
                    if((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                        ret = param.Value;
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}