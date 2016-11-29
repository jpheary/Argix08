//	File:	CRGFactory.cs
//	Author:	J. Heary
//	Date:	01/08/09
//	Desc:	A global object for building model objects and for database access.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using Argix.Data;

namespace Argix.CustomerSvc {
	//
	public class CRGFactory {
		//Members
        public static Mediator _Mediator = null;
        public static int IssueDaysBack=30;
        public static string TempFolder="c:\\temp\\";
        private static IssueDS _IssueTypes = null, _ActionTypes = null;

        public const string USP_ISSUES = "uspIMIssueGetList2",TBL_ISSUES = "IssueTable";
        public const string USP_ISSUES_HISTORY = "uspIMIssueHistoryGetList2";
        public const string USP_ISSUES_SEARCH = "uspIMIssueSearchGetList";
        public const string USP_ISSUE_GET = "uspIMIssueGet",TBL_ISSUE = "IssueTable";
        public const string USP_ISSUE_NEW = "uspIMIssueNew";
        public const string USP_ISSUE_UPDATE = "uspIMIssueUpdate2";
        public const string USP_ISSUETYPES = "uspIMIssueTypesGetList",TBL_ISSUETYPES = "IssueTypeTable";
        public const string USP_ACTIONS = "uspIMActionGetList",TBL_ACTIONS = "ActionTable";
        public const string USP_ACTION_NEW = "uspIMActionNew";
        public const string USP_ACTIONTYPES = "uspIMActionTypesGetList",TBL_ACTIONTYPES = "ActionTypeTable";
        public const string USP_ATTACHMENTS = "uspIMAttachmentGetList",TBL_ATTACHMENTS="AttachmentTable";
        public const string USP_ATTACHMENT_GET = "uspIMAttachmentGet";
        public const string USP_ATTACHMENT_NEW = "uspIMAttachmentNew";

        public static event EventHandler IssuesChanged = null;
        public static event DataStatusHandler DataStatusUpdate = null; 
		
		//Interface
        static CRGFactory() {
            //Constructor
            try {
                _IssueTypes = new IssueDS();
                _ActionTypes = new IssueDS();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new CRGFactory singleton.",ex); }
        }
        private CRGFactory() { }
        public static Mediator Mediator {
            get { return _Mediator; }
            set { 
                _Mediator = value;
                if(_Mediator != null) _Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                RefreshCache(); 
            }
        }

        public static void RefreshCache() {
            //Refresh cached data
            DataSet ds = null;
            try {
                //Validate
                if(_Mediator == null) return;
                if(!_Mediator.OnLine) return;

                //Issue types
                _IssueTypes.Clear();
                ds = _Mediator.FillDataset(USP_ISSUETYPES,TBL_ISSUETYPES,null);
                if(ds.Tables[TBL_ISSUETYPES].Rows.Count > 0) _IssueTypes.Merge(ds);

                //Action types
                _ActionTypes.Clear();
                ds = _Mediator.FillDataset(USP_ACTIONTYPES,TBL_ACTIONTYPES,null);
                if(ds.Tables[TBL_ACTIONTYPES].Rows.Count > 0) _ActionTypes.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while caching CRGFactory data.",ex); }
        }
        public static IssueDS IssueCategorys { 
            get {
                IssueDS issueDS = new IssueDS();
                Hashtable groups = new Hashtable();
                for(int i=0;i<_IssueTypes.IssueTypeTable.Rows.Count;i++) {
                    if(!groups.ContainsKey(_IssueTypes.IssueTypeTable[i].Category)) {
                        groups.Add(_IssueTypes.IssueTypeTable[i].Category,_IssueTypes.IssueTypeTable[i].Category);
                        issueDS.IssueCategoryTable.AddIssueCategoryTableRow(_IssueTypes.IssueTypeTable[i].Category);
                    }
                }
                issueDS.AcceptChanges();
                return issueDS;
            } 
        }
        public static IssueDS IssueTypes { get { return _IssueTypes; } }
        public static string GetIssueType(int typeID) {
            //Get an issue type  for the specified id
            string issueType = "";
            try {
                IssueDS.IssueTypeTableRow it = (IssueDS.IssueTypeTableRow)_IssueTypes.IssueTypeTable.Select("ID=" + typeID)[0];
                issueType = it.Type;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue type.",ex); }
            return issueType;
        }
        public static IssueDS ActionTypes { get { return _ActionTypes; } }
        public static string GetActionType(int typeID) {
            //Get an action type  for the specified id
            string actionType = "";
            try {
                IssueDS.ActionTypeTableRow at = (IssueDS.ActionTypeTableRow)_ActionTypes.ActionTypeTable.Select("ID=" + typeID)[0];
                actionType = at.Type;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading action type.",ex); }
            return actionType;
        }
        public static DateTime FromDate { get { return DateTime.Today.AddDays(-IssueDaysBack); } }
        #region Issue Services: GetIssues(), GetIssue(), CreateIssue(), UpdateIssue(), IssueHistory()
        public static DataSet GetIssues() {
            //Get issues
            DateTime fromDate = DateTime.Today.AddDays(-IssueDaysBack);
            return GetIssues(fromDate);
        }
        public static DataSet GetIssues(DateTime fromDate) {
            //
            IssueDS issues=new IssueDS();
            if(_Mediator.OnLine) {
                //Get issues
                DateTime toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                DataSet ds = _Mediator.FillDataset(USP_ISSUES,TBL_ISSUES,new object[] { fromDate,toDate });
                if(ds.Tables[TBL_ISSUES].Rows.Count > 0) issues.Merge(ds);
            }
            return issues;
        }
        public static Issue GetIssue(long issueID) {
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
                    DataSet ds = _Mediator.FillDataset(USP_ISSUE_GET,TBL_ISSUE,new object[] { issueID });
                    if(ds.Tables[TBL_ISSUE].Rows.Count == 0)
                        throw new ApplicationException("Issue not found.");
                    else {
                        issues.Merge(ds);
                    }
                    //Actions; skip the attachments (get on demand)
                    IssueDS actions = new IssueDS();
                    ds = _Mediator.FillDataset(USP_ACTIONS,TBL_ACTIONS,new object[] { issueID });
                    if(ds.Tables[TBL_ACTIONS].Rows.Count == 0)
                        throw new ApplicationException("No actions found for this issue.");
                    else 
                        actions.Merge(ds);

                    //Attachments
                    IssueDS attachments = new IssueDS();
                    ds = _Mediator.FillDataset(USP_ATTACHMENTS,TBL_ATTACHMENTS,new object[] { issueID });
                    if(ds.Tables[TBL_ATTACHMENTS].Rows.Count > 0) attachments.Merge(ds);

                    issue = new Issue(issues.IssueTable[0],actions,attachments);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issues.",ex); }
            return issue;
        }
        public static long CreateIssue(Issue issue) {
            //Create a new issue
            long iid = 0;
            try {
                //Save issue
                object store = null;
                if(issue.StoreNumber > 0) store = issue.StoreNumber;
                object io = _Mediator.ExecuteNonQueryWithReturn(USP_ISSUE_NEW,new object[] { null,issue.TypeID,issue.Subject,issue.ContactID,issue.CompanyID,issue.RegionNumber,issue.DistrictNumber,issue.AgentNumber,store,issue.OFD1FromDate,issue.OFD1ToDate,issue.PROID });
                iid = (long)io;

                //Add the single 'Open' action
                IssueDS.ActionTableRow a = issue.Actions.ActionTable[0];
                object ao = _Mediator.ExecuteNonQueryWithReturn(USP_ACTION_NEW,new object[] { null,a.TypeID,iid,a.UserID,a.Comment });
                long aid = (long)ao;

                //Add attachments (throw exception for a TODO reminder)
                if(issue.Attachments.AttachmentTable.Rows.Count > 0)
                    throw new System.NotSupportedException("Attachments are not supported for new issues.");
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new issue.",ex); }
            finally { if(IssuesChanged != null) IssuesChanged(null,EventArgs.Empty); }
            return iid;
        }
        public static bool UpdateIssue(Issue issue) {
            //Update an existing issue
            bool b = false;
            try {
                //Update the updateable issue attributes
                _Mediator.ExecuteNonQuery(USP_ISSUE_UPDATE,new object[] { issue.ID,issue.ContactID,issue.OFD1FromDate,issue.OFD1ToDate,issue.PROID });

                //Add any new actions and associated attachments
                DataSet ds = issue.Actions.GetChanges(DataRowState.Added);
                if(ds != null) {
                    //NOTE: Should never be more than 1 new action (constrained by the issue.Add() 
                    //      method which saves actions immediately)
                    IssueDS actions = new IssueDS();
                    actions.Merge(ds);
                    for(int i=0; i<actions.ActionTable.Rows.Count; i++) {
                        IssueDS.ActionTableRow a = actions.ActionTable[i];
                        object ao = _Mediator.ExecuteNonQueryWithReturn(USP_ACTION_NEW,new object[] { null,a.TypeID,issue.ID,a.UserID,a.Comment });
                        long aid = (long)ao;

                        //Add associated attachments
                        //NOTE: Attachments are held in the issue, but saved (associated) to the action
                        try {
                            DataSet _ds = issue.Attachments.GetChanges(DataRowState.Added);
                            if(_ds != null) {
                                IssueDS attachments = new IssueDS();
                                attachments.Merge(_ds);
                                for(int j=0;j<attachments.AttachmentTable.Rows.Count;j++) {
                                    IssueDS.AttachmentTableRow at = attachments.AttachmentTable[j];
                                    SaveFileAttachment(aid,at.FileName,at.File);
                                }
                            }
                        }
                        catch(Exception ex) { throw new ApplicationException("Unexpected error while saving file attachment to new action.",ex); }
                    }
                }
                b = true;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating issue.",ex); }
            finally { if(IssuesChanged != null) IssuesChanged(null,EventArgs.Empty); }
            return b;
        }
        public static IssueDS IssueHistory(Issue issue) {
            //Get issue history data
            IssueDS history = new IssueDS();
            try {
                //Validate data access
                if(_Mediator.OnLine) {
                    object store=null;
                    if(issue.StoreNumber > 0) store = issue.StoreNumber;
                    DataSet ds = _Mediator.FillDataset(USP_ISSUES_HISTORY,TBL_ISSUES,new object[] { issue.CompanyID,issue.RegionNumber,issue.DistrictNumber,issue.AgentNumber,store });
                    if(ds.Tables[TBL_ISSUES].Rows.Count > 0) history.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading issue history.",ex); }
            return history;
        }
        public static DataSet SearchIssues(string searchText) {
            //Get issue search data
            IssueDS search = new IssueDS();
            try {
                //Validate data access
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_ISSUES_SEARCH,TBL_ISSUES,new object[] { searchText });
                    if(ds.Tables[TBL_ISSUES].Rows.Count > 0) search.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while searching issues.",ex); }
            return search;
        }
        #endregion
        #region Action Attachment Services: GetFileAttachment(), SaveFileAttachment()
        public static string GetFileAttachment(long actionID,string filename) {
            //Get an existing file attachment from database, save to the c:\temp directory,
            //and return the full filename
            string file = TempFolder + filename.Trim();
            try {
                //Set CommandBehavior to SequentialAccess to use SqlDataReader.GetBytes() method
                DataSet ds = _Mediator.FillDataset(USP_ATTACHMENT_GET,"AttachmentTable",new object[] { filename.Trim(),actionID });
                if(ds.Tables["AttachmentTable"].Rows.Count > 0) {
                    FileStream fs = new FileStream(file,FileMode.OpenOrCreate,FileAccess.Write);
                    BinaryWriter writer = new BinaryWriter(fs);
                    writer.Write((byte[])ds.Tables["AttachmentTable"].Rows[0]["File"]);
                    writer.Flush();
                    writer.Close();
                    fs.Close();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading file attachment.",ex); }
            return file;
        }
        public static bool SaveFileAttachment(long actionID,string filename,byte[] bytes) {
            //Create a new issue
            bool saved=false;
            try {
                //Save issue
                string name = new FileInfo(filename).Name;
                object o = _Mediator.ExecuteNonQueryWithReturn(USP_ATTACHMENT_NEW,new object[] { null,name,bytes,actionID });
                int id = (int)o;
                saved = (id > 0);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new attachment.",ex); }
            return saved;
        }
        #endregion
        private static void OnDataStatusUpdate(object source,DataStatusArgs e) { if(DataStatusUpdate != null) DataStatusUpdate(source,e); }
    }
}