//	File:		Issue.cs
//	Author:	    jheary
//	Date:		01/07/09
//	Desc:		
//	Rev:		
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using Argix.Enterprise;

namespace Argix.CustomerSvc {
	//
	public class Issue {
		//Members
		protected long _id=0;
        protected int _typeid = 0;
        protected string _subject = "";
        protected int _contactid = 0;
        private int _companyid = 0;
        private string _regionnumber = null, _districtnumber = null, _agentnumber = null;
        private int _storenumber = 0;
        private DateTime _ofd1datefrom = DateTime.Today, _ofd1dateto = DateTime.Today;
        private long _proid = 0;
        private string _zone="";
        protected IssueDS mActions = null, mAttachments = null;

        public event EventHandler Changed = null;
				
		//Interface
		public Issue(): this(null,null,null) { }
        public Issue(IssueDS.IssueTableRow issue,IssueDS actions,IssueDS attachments) { 
			//Constructor
			try {
                this.mActions = new IssueDS();
                this.mAttachments = new IssueDS();
                if(issue == null) {
                    //New issue- add an 'Open' action
                    this.mActions.ActionTable.AddActionTableRow(0,Action.ACTIONTYPE_OPEN,0,Environment.UserName,DateTime.Now,"");
                }
                else {
                    //Existing issue
					if(!issue.IsIDNull()) this._id = issue.ID;
                    if(!issue.IsTypeIDNull()) this._typeid = issue.TypeID;
                    if(!issue.IsSubjectNull()) this._subject = issue.Subject.Trim();
                    if(!issue.IsContactIDNull()) this._contactid = issue.ContactID;
                    if(!issue.IsCompanyIDNull()) this._companyid = issue.CompanyID;
                    if(!issue.IsRegionNumberNull()) this._regionnumber = issue.RegionNumber.Trim();
                    if(!issue.IsDistrictNumberNull()) this._districtnumber = issue.DistrictNumber.Trim();
                    if(!issue.IsAgentNumberNull()) this._agentnumber = issue.AgentNumber.Trim();
                    if(!issue.IsStoreNumberNull()) this._storenumber = issue.StoreNumber;
                    if(!issue.IsOFD1FromDateNull()) this._ofd1datefrom = issue.OFD1FromDate;
                    if(!issue.IsOFD1ToDateNull()) this._ofd1dateto = issue.OFD1ToDate;
                    if(!issue.IsPROIDNull()) this._proid = issue.PROID;
                    if(!issue.IsZoneNull()) this._zone = issue.Zone;

                    if(actions != null) this.mActions.Merge(actions);
                    if(attachments != null) this.mAttachments.Merge(attachments);
                }
            }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Issue instance", ex); }
		}
		#region Accessors\Modifiers: [members...], ToDataSet()
		public long ID { get { return this._id; } set { this._id = value; } }
        public int TypeID { get { return this._typeid; } set { this._typeid = value; } }
        public string Type { get { return CRGFactory.GetIssueType(this._typeid); } }
        public string Subject { get { return this._subject; } set { this._subject = value; } }
        public int ContactID { get { return this._contactid; } set { this._contactid = value; } }
        public string Contact { get { return EnterpriseFactory.GetContact(this._contactid).FullName; } }
        public int CompanyID { get { return this._companyid; } set { this._companyid = value; } }
        public string Company { get { return EnterpriseFactory.GetCompany(this._companyid); } }
        public string RegionNumber { get { return this._regionnumber; } set { this._regionnumber = value; } }
        public string DistrictNumber { get { return this._districtnumber; } set { this._districtnumber = value; } }
        public string AgentNumber { get { return this._agentnumber; } set { this._agentnumber = value; } }
        public int StoreNumber { get { return this._storenumber; } set { this._storenumber = value; } }
        public DateTime OFD1FromDate { get { return this._ofd1datefrom; } set { this._ofd1datefrom = value; } }
        public DateTime OFD1ToDate { get { return this._ofd1dateto; } set { this._ofd1dateto = value; } }
        public long PROID { get { return this._proid; } set { this._proid = value; } }
        public string Zone { get { return this._zone; } }
        public IssueDS Actions { get { return this.mActions; } }
        public string LastAction { get { return CRGFactory.GetActionType(this.mActions.ActionTable[this.mActions.ActionTable.Rows.Count - 1].TypeID); } }
        public DateTime LastCreated { get { return this.mActions.ActionTable[this.mActions.ActionTable.Rows.Count - 1].Created; } }
        public string LastUser { get { return this.mActions.ActionTable[this.mActions.ActionTable.Rows.Count - 1].UserID; } }
        public IssueDS Attachments { get { return this.mAttachments; } }
        public virtual DataSet ToDataSet() {
            //Return a dataset containing values for this object
            IssueDS ds=null;
            try {
                ds = new IssueDS();
                IssueDS.IssueTableRow issue = ds.IssueTable.NewIssueTableRow();
                issue.ID = this._id;
                issue.TypeID = this._typeid;
                issue.Subject = this._subject;
                issue.ContactID = this._contactid;
                issue.CompanyID = this._companyid;
                issue.RegionNumber = this._regionnumber;
                issue.DistrictNumber = this._districtnumber;
                issue.AgentNumber = this._agentnumber;
                issue.StoreNumber = this._storenumber;
                issue.OFD1FromDate = this._ofd1datefrom;
                issue.OFD1ToDate = this._ofd1dateto;
                issue.PROID = this._proid;
                issue.Zone = this._zone;
                ds.IssueTable.AddIssueTableRow(issue);
                ds.Merge(this.mActions);
            }
            catch(Exception) { }
            return ds;
        }
        public DataSet ToDraft() {
            //Return a dataset containing values for this object
            IssueDS ds=null;
            try {
                ds = new IssueDS();
                IssueDS.IssueTableRow issue = ds.IssueTable.NewIssueTableRow();
                issue.ID = this._id;
                issue.TypeID = this._typeid;
                issue.Type = this.Type;
                issue.Subject = this._subject;
                issue.ContactID = this._contactid;
                issue.ContactName = this.Contact;
                issue.CompanyID = this._companyid;
                issue.CompanyName = this.Company;
                issue.RegionNumber = this._regionnumber;
                issue.DistrictNumber = this._districtnumber;
                issue.AgentNumber = this._agentnumber;
                issue.StoreNumber = this._storenumber;
                issue.OFD1FromDate = this._ofd1datefrom;
                issue.OFD1ToDate = this._ofd1dateto;
                issue.PROID = this._proid;
                issue.Zone = this._zone;
                issue.FirstActionID = issue.LastActionID = 0;
                issue.FirstActionDescription = issue.LastActionDescription = this.LastAction;
                issue.FirstActionCreated = issue.LastActionCreated = this.LastCreated;
                issue.FirstActionUserID = issue.LastActionUserID = this.LastUser;
                ds.IssueTable.AddIssueTableRow(issue);
                ds.Merge(this.mActions);
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
        public void Refresh() {
            //Refresh this object
            try {
                //Get latest
                Issue issue = this.ID > 0 ? CRGFactory.GetIssue(this.ID) : null;
                if(issue != null) {
                    //Updatable fields only
                    this._ofd1datefrom = issue.OFD1FromDate;
                    this._ofd1dateto = issue.OFD1ToDate;
                    this._proid = issue.PROID;

                    this.mActions.Clear();
                    this.mActions.Merge(issue.Actions);

                    this.mAttachments.Clear();
                    this.mAttachments.Merge(issue.Attachments);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing issue.",ex); }
            finally { if(this.Changed != null) this.Changed(this,EventArgs.Empty); }
        }
        public bool IsClosed {
            get {
                bool closed=false;
                IssueDS.ActionTableRow[] rows = (IssueDS.ActionTableRow[])this.mActions.ActionTable.Select("","Created DESC");
                closed = (rows[0].TypeID == Action.ACTIONTYPE_DISMISS || rows[0].TypeID == Action.ACTIONTYPE_CLOSE);
                return closed;
            }
        }
        public bool Save() {
            //Save this object
            bool ret=false;
            try {
                if(this._id == 0) {
                    //Create a new delivery issue
                    this._id = CRGFactory.CreateIssue(this);
                    ret = (this._id > 0);
                }
                else {
                    //Update this existing issue
                    ret = CRGFactory.UpdateIssue(this);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating new issue.",ex); }
            //finally { if(this.Changed != null) this.Changed(this,EventArgs.Empty); }
            
            //Refresh this object (are ya sure ya wanna do this?)
            if(ret) Refresh();
            return ret;
        }
        public string GetAllActionComments() {
            //Get a running comment for this action
            string comments="";
            IssueDS actions = new IssueDS();
            actions.Merge(this.mActions.ActionTable.Select("","Created DESC"));
            for(int i = 0;i < actions.ActionTable.Rows.Count;i++) {
                if(i > 0) {
                    comments += "\r\n\r\n";
                    comments += "".PadRight(75,'-');
                    comments += "\r\n";
                }
                comments += actions.ActionTable[i].Created.ToString("f") + ", " + actions.ActionTable[i].UserID + ", " + Action.GetActionTypeName(actions.ActionTable[i].TypeID);
                comments += "\r\n\r\n";
                comments += actions.ActionTable[i].Comment;
            }
            return comments;
        }
        #region Action Services: Add(), Count, Item(), Remove()
        public void Add(Action action) { 
            //Add a new action to this issue
            try {
                //Validate
                if(action.IssueID != this.ID) throw new ApplicationException("This action belongs to another issue (" + action.IssueID + ").");
                
                //Copy new attachments to this collection for the save
                for(int i=0;i<action.Attachments.AttachmentTable.Rows.Count;i++) {
                    Attachment at = new Attachment(action, action.Attachments.AttachmentTable[i]);
                    this.mAttachments.AttachmentTable.AddAttachmentTableRow(at.ID, at.Filename, at.File, at.ActionID);
                }

                //Save to preserve model integrity
                this.mActions.ActionTable.AddActionTableRow(action.ID,action.TypeID,action.IssueID,action.UserID,action.Created,action.Comment);
                Save();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while adding action to issue.",ex); }
        }
        public int Count { get { return this.mActions.ActionTable.Rows.Count; } }
        public Action Item(long id) { 
            //Get a new or existing action from this issue
            Action action=null;
            if(id == 0) {
                //Return the new 'Open' action if this is a new issue; otherwise, return a new action
                if(this._id == 0)
                    action = new Action(this, this.mActions.ActionTable[0]);
                else
                    action = new Action(this);
            }
            else {
                //Return an existing action
                IssueDS.ActionTableRow[] actions = (IssueDS.ActionTableRow[])this.mActions.ActionTable.Select("ID=" + id);
                if(actions.Length == 0) throw new ApplicationException("Action (" + id + ") could not be found in this issue (" + this.ID + ".");
                action = new Action(this, actions[0]);
            }
            return action;
        }
        public void Remove(Action action) { 
            //Remove an existing action from this issue
            throw new NotSupportedException("Removing action from issues is unsupported.");
        }
        #endregion

        public class Action {
            //Members
            private long _id=0;
            private byte _typeid = (byte)ACTIONTYPE_OPEN;
            private long _issueid=0;
            private string _userid=Environment.UserName;
            private DateTime _created=DateTime.Now;
            private string _comment="";
            protected IssueDS mAttachments = null;
            private Issue _parent=null;

            public const int ACTIONTYPE_OPEN = 1;
            public const int ACTIONTYPE_DISMISS = 2;
            public const int ACTIONTYPE_CLOSE = 3;

            //Interface
            public static string GetActionTypeName(int actionTypeID) {
                //Get action type name for typeID
                string name = "";
                try {
                    IssueDS.ActionTypeTableRow[] types = (IssueDS.ActionTypeTableRow[])CRGFactory.ActionTypes.ActionTypeTable.Select("ID=" + actionTypeID);
                    if(types.Length > 0) name = types[0].Type;
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while reading action type name.",ex); }
                return name;
            }
            internal Action(Issue parent) : this(parent,null) { }
            internal Action(Issue parent, IssueDS.ActionTableRow action) {
                //Constructor
                try {
                    this.mAttachments = new IssueDS();
                    this._parent = parent;
                    this._issueid = parent.ID;
                    if(action != null) {
                        if(!action.IsIDNull()) this._id = action.ID;
                        if(!action.IsTypeIDNull()) this._typeid = action.TypeID;
                        if(!action.IsIssueIDNull()) this._issueid = action.IssueID;
                        if(!action.IsUserIDNull()) this._userid = action.UserID;
                        if(!action.IsCreatedNull()) this._created = action.Created;
                        if(!action.IsCommentNull()) this._comment = action.Comment;
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Action instance",ex); }
            }
            #region Accessors\Modifiers: [Members...], ToDataSet()
            public long ID { get { return this._id; } }
            public byte TypeID { 
                get { return this._typeid; } 
                set {   
                    if(this._id == 0) 
                        this._typeid = value; 
                    else 
                        throw new ApplicationException("Action type cannot be changed on existing actions."); 
                } 
            }
            public string TypeName { get { return Action.GetActionTypeName(this._typeid); } }
            public long IssueID { get { return this._issueid; } }
            public string UserID { get { return this._userid; } }
            public DateTime Created { get { return this._created; } }
            public string Comment { 
                get { return this._comment; } 
                set {
                    if(this._id == 0)
                        this._comment = value;
                    else
                        throw new ApplicationException("Action comment cannot be changed on existing actions.");
                } 
            }
            public IssueDS Attachments { 
                get {
                    IssueDS attachments = new IssueDS();
                    if(this._id != 0) {
                        DataRow[] _attachments = this._parent.mAttachments.AttachmentTable.Select("ActionID=" + this._id);
                        if(_attachments.Length > 0) attachments.Merge(_attachments);
                    }
                    else {
                        attachments = this.mAttachments;
                    }
                    return attachments;
                } 
            }
            public Issue Parent { get { return this._parent; } }
            public DataSet ToDataSet() {
                //Return a dataset containing values for this object
                IssueDS ds = null;
                try {
                    ds = new IssueDS();
                    IssueDS.ActionTableRow action = ds.ActionTable.NewActionTableRow();
                    action.ID = this._id;
                    action.TypeID = this._typeid;
                    action.IssueID = this._issueid;
                    action.UserID = this._userid;
                    action.Created = this._created;
                    action.Comment = this._comment;
                    ds.ActionTable.AddActionTableRow(action);
                }
                catch(Exception) { }
                return ds;
            }
            #endregion
            #region Attachment Services: Add(), Count, Item(), Remove()
            public void Add(Attachment attachment) {
                //Add a new attachment to this action
                try {
                    //Validate
                    if(attachment.ActionID != this._id) throw new ApplicationException("This attachment belongs to another action (" + attachment.ActionID + ").");

                    //Save to preserve model integrity if this is an existing action
                    if(this._id != 0) {
                        this._parent.mAttachments.AttachmentTable.AddAttachmentTableRow(attachment.ID,attachment.Filename,attachment.File,attachment.ActionID);
                        this._parent.Save();
                    }
                    else {
                        this.mAttachments.AttachmentTable.AddAttachmentTableRow(attachment.ID,attachment.Filename,attachment.File,attachment.ActionID);
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while adding attachment to action.",ex); }
            }
            public int Count { 
                get {
                    int count=0;
                    //Get attachment counts for this action
                    IssueDS.AttachmentTableRow[] attachments=null;
                    if(this._id != 0) {
                        attachments = (IssueDS.AttachmentTableRow[])this._parent.mAttachments.AttachmentTable.Select("ActionID=" + this._id);
                        count = attachments.Length;
                    }
                    else {
                        count = this.mAttachments.AttachmentTable.Rows.Count;
                    }
                    return count; 
                } 
            }
            public Attachment Item(int id) {
                //Get an existing attachment from this action
                Attachment attachment=null;
                if(id == 0) {
                    //Return a new attachment
                    attachment = new Attachment(this);
                }
                else {
                    if(this._id != 0) {
                        //Return an existing attachment from an existing action
                        IssueDS.AttachmentTableRow[] attachments = (IssueDS.AttachmentTableRow[])this._parent.mAttachments.AttachmentTable.Select("ID=" + id);
                        if(attachments.Length == 0) throw new ApplicationException("Attachment (" + id + ") could not be found in this action (" + this._id + ".");
                        attachment = new Attachment(this,attachments[0]);
                    }
                    else {
                        //Return an existing attachment from a new action
                        IssueDS.AttachmentTableRow[] attachments = (IssueDS.AttachmentTableRow[])this.mAttachments.AttachmentTable.Select("ID=" + id);
                        if(attachments.Length == 0) throw new ApplicationException("Attachment (" + id + ") could not be found in this action (" + this._id + ".");
                        attachment = new Attachment(this,attachments[0]);
                    }
                }
                return attachment;
            }
            public Attachment Item(string filename) {
                //Get an existing attachment from this action
                Attachment attachment=null;
                if(filename.Length == 0) {
                    //Return a new attachment
                    attachment = new Attachment(this);
                }
                else {
                    if(this._id != 0) {
                        //Return an existing attachment from an existing action
                        IssueDS.AttachmentTableRow[] attachments = (IssueDS.AttachmentTableRow[])this._parent.mAttachments.AttachmentTable.Select("FileName='" + filename + "' AND ActionID=" + this._id);
                        if(attachments.Length == 0) throw new ApplicationException("Attachment (" + filename + ") could not be found in this action (" + this._id + ".");
                        attachment = new Attachment(this,attachments[0]);
                    }
                    else {
                        //Return an existing attachment from a new action
                        IssueDS.AttachmentTableRow[] attachments = (IssueDS.AttachmentTableRow[])this.mAttachments.AttachmentTable.Select("FileName='" + filename + "'");
                        if(attachments.Length == 0) throw new ApplicationException("Attachment (" + filename + ") could not be found in this action (" + this._id + ".");
                        attachment = new Attachment(this,attachments[0]);
                    }
                }
                return attachment;
            }
            public void Remove(Attachment attachment) {
                //Remove an existing attachment from this action
                if(this._id != 0)
                    throw new NotSupportedException("Removing attachments from existing actions is unsupported.");
                else {
                    for(int i=0;i<this.mAttachments.AttachmentTable.Rows.Count;i++) {
                        if(this.mAttachments.AttachmentTable[i].FileName == attachment.Filename)
                            this.mAttachments.AttachmentTable[i].Delete();
                    }
                }
            }
            public void Remove(string filename) {
                //Remove an existing attachment from this action
                if(this._id != 0)
                    throw new NotSupportedException("Removing attachments from existing actions is unsupported.");
                else {
                    for(int i=0;i<this.mAttachments.AttachmentTable.Rows.Count;i++) {
                        if(this.mAttachments.AttachmentTable[i].FileName == filename)
                            this.mAttachments.AttachmentTable[i].Delete();
                    }
                }
            }
            #endregion
            internal void Refresh() {
                //Refresh this object- requires parent to refresh (re-build)
                this._parent.Refresh();
            }
            public IssueDS GetValidActionTypes() {
                //Get valid action types for this issue
                //1	Open	                New
                //2	Dismiss	                Closed
                //3	Close	                Closed
                //4	Notify All	            All
                //5	Notify Agent Systems	Agent Systems
                //6	Notify CRG	            CRG
                //7 Other                   Pending
                IssueDS actionTypes = null;
                try {
                    //Get full list
                    actionTypes = new IssueDS();
                    actionTypes.Merge(CRGFactory.ActionTypes);

                    //Remove actions that don't apply
                    IssueDS actions = this._parent.Actions;
                    if(actions.ActionTable.Rows.Count == 0) {
                        //New: Open only
                        for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++)
                            if(actionTypes.ActionTypeTable[i].ID != 1) actionTypes.ActionTypeTable[i].Delete();
                    }
                    else if(actions.ActionTable.Rows.Count == 1) {
                        //Open: Dismiss, Notify All, Notify Agent Systems, Notify CRG
                        for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++) {
                            if(actionTypes.ActionTypeTable[i].ID == 1) actionTypes.ActionTypeTable[i].Delete();
                            else if(actionTypes.ActionTypeTable[i].ID == 3) actionTypes.ActionTypeTable[i].Delete();
                        }
                    }
                    else if(actions.ActionTable.Rows.Count > 1) {
                        //Unresolved: Close, Notify All, Notify Agent Systems, Notify CRG, Other
                        for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++) {
                            if(actionTypes.ActionTypeTable[i].ID == 1) actionTypes.ActionTypeTable[i].Delete();
                            else if(actionTypes.ActionTypeTable[i].ID == 2) actionTypes.ActionTypeTable[i].Delete();
                        }
                    }


                    //Commit changes
                    actionTypes.AcceptChanges();
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while reading action types.",ex); }
                return actionTypes;
            }
        }

        public class Attachment {
            //Members
            private int _id=0;
            private string _filename="";
            private byte[] _file=null;
            private long _actionid=0;
            private Action _parent=null;

            //Interface
            internal Attachment(Action parent) : this(parent,null) { }
            internal Attachment(Action parent,IssueDS.AttachmentTableRow attachment) {
                //Constructor
                try {
                    this._parent = parent;
                    this._actionid = parent.ID;
                    if(attachment != null) {
                        if(!attachment.IsIDNull()) this._id = attachment.ID;
                        if(!attachment.IsFileNameNull()) this._filename = attachment.FileName;
                        if(!attachment.IsFileNull()) this._file = attachment.File;
                        if(!attachment.IsActionIDNull()) this._actionid = attachment.ActionID;
                    }
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Action instance",ex); }
            }
            #region Accessors\Modifiers: [Members...], ToDataSet()
            public int ID { get { return this._id; } }
            public string Filename { get { return this._filename; } set { this._filename = value; } }
            public string Name { get { FileInfo fi = new FileInfo(this._filename); return fi.Name; } }
            public byte[] File { 
                get {
                    if(this._file == null) {
                        //Validate exists (if it doesn't, let it bomb on save)
                        FileInfo fi = new FileInfo(this._filename);
                        if(fi.Exists) {
                            //Get file from disk
                            FileStream fs=null;
                            BinaryReader reader=null;
                            try {
                                fs = new FileStream(this._filename,FileMode.Open,FileAccess.Read);
                                reader = new BinaryReader(fs);
                                this._file = reader.ReadBytes((int)fs.Length);
                            }
                            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading file attachment.",ex); }
                            finally { fs.Close(); reader.Close(); }
                        }
                    }
                    return this._file; 
                } 
                set { this._file = value; } }
            public long ActionID { get { return this._actionid; } }
            public Action Parent { get { return this._parent; } }
            public DataSet ToDataSet() {
                //Return a dataset containing values for this object
                IssueDS ds = null;
                try {
                    ds = new IssueDS();
                    IssueDS.AttachmentTableRow attachment = ds.AttachmentTable.NewAttachmentTableRow();
                    attachment.ID = this._id;
                    attachment.FileName = this._filename;
                    attachment.File = this._file;
                    attachment.ActionID = this._actionid;
                    ds.AttachmentTable.AddAttachmentTableRow(attachment);
                }
                catch(Exception) { }
                return ds;
            }
            #endregion
            public void Open() {
                //Open this attachment
                try {
                    string file = CRGFactory.GetFileAttachment(this._actionid,this._filename);
                    System.Diagnostics.Process.Start(file);
                }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while opening file attachment.",ex); }
            }
            public bool Save() {
                //Save an attachment from an existing file
                bool ret=false;
                try {
                    //Use File instead of this._file File will open from disk if not done so)
                    if(this._actionid == 0) throw new ApplicationException("Attachments cannot be saved to new (unsaved) Actions.");
                    ret = CRGFactory.SaveFileAttachment(this._actionid,this._filename,File);
                    this._parent.Refresh();
                }
                catch(ApplicationException ex) { throw ex; }
                catch(Exception ex) { throw new ApplicationException("Unexpected error while saving file attachment.",ex); }
                return ret;
            }
        }
    }

    public class Issues:BindingList<Issue> {
        //Members

        //Interface
        public Issues() { }
    }

}
