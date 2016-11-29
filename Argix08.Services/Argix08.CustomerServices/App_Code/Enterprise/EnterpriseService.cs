//	File:	EnterpriseFactory.cs
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

namespace Argix.Enterprise {
	//
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class EnterpriseService:IEnterpriseService {
        //Members
        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        public const string USP_TERMINALS = "uspTerminalsGetList",TBL_TERMINALS = "TerminalTable";
        
        public const string USP_COMPANIES = "uspIMCompanyGetList1",TBL_COMPANIES = "CompanyTable";
        public const string USP_REGIONS_DISTRICTS = "uspIMRegionDistrictGetList",TBL_REGIONS = "LocationTable",TBL_DISTRICTS = "LocationTable";
        public const string USP_AGENTS = "uspIMAgentGetList",TBL_AGENTS = "AgentTable";
        public const string USP_AGENTS_BYCLIENT = "uspIMAgentGetListForClient";
        public const string USP_STORE = "uspIMStoreGet1",TBL_STORE = "StoreTable";

        public const string USP_CONTACTS = "uspIMContactGetList",TBL_CONTACTS = "ContactTable";
        public const string USP_CONTACT_NEW = "uspIMContactNew";
        public const string USP_CONTACTS_BYLOC = "uspIMContactGetListForLocation";
        public const string USP_CONTACT_UPDATE = "uspIMContactUpdate2";

        //Interface
        public EnterpriseService() { }

        public TerminalInfo GetTerminalInfo() {
            //Get information about the local terminal for this service
            TerminalInfo info = null;
            try {
                info = new TerminalInfo();
                info.Connection = DatabaseFactory.CreateDatabase("SQLConnection").ConnectionStringWithoutCredentials;
                DataSet ds = fillDataset(USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[] { });
                if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                    info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(new ApplicationException("Unexpected error while reading terminal info.",ex))); }
            return info;
        }
        public Terminals GetTerminals() {
            //Get Argix terminals
            Terminals terminals=null;
            try {
                terminals = new Terminals();
                DataSet ds = fillDataset(USP_TERMINALS, TBL_TERMINALS, new object[] { });
                if (ds != null && ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                    EnterpriseDS _ds = new EnterpriseDS();
                    _ds.Merge(ds);
                    for(int i=0; i<_ds.TerminalTable.Rows.Count; i++) {
                        Terminal t = new Terminal();
                        t.TerminalID = Convert.ToInt32(ds.Tables[TBL_TERMINALS].Rows[i]["TerminalID"]);
                        t.Number = ds.Tables[TBL_TERMINALS].Rows[i]["Number"].ToString().Trim();
                        t.Description = ds.Tables[TBL_TERMINALS].Rows[i]["Description"].ToString().Trim();
                        t.AgentID = ds.Tables[TBL_TERMINALS].Rows[i]["AgentID"].ToString().Trim();
                        terminals.Add(t);
                    }
                }
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(new ApplicationException("Unexpected error while reading Argix terminals.", ex))); }
            return terminals;
        }
       
        public CompanyDS GetCompanies() {  
            //Get a list of all companies
            return GetCompanies(false);
        }
        public CompanyDS GetCompanies(bool activeOnly) {
            //Get a list of active companies
            CompanyDS companies = new CompanyDS();
            string filter = activeOnly ? "IsActive = 1" : "";
            DataSet ds = fillDataset(USP_COMPANIES,TBL_COMPANIES,new object[] { });
            if(ds.Tables[TBL_COMPANIES].Rows.Count > 0) {
                CompanyDS _ds = new CompanyDS();
                _ds.CompanyTable.AddCompanyTableRow(0,"All","000","",1);
                _ds.Merge(ds.Tables[TBL_COMPANIES].Select(filter,"CompanyName ASC"));
                companies.Merge(_ds);
            }
            return companies;
        }
        public string GetCompany(int companyID) {
            //Get a company for the specified id
            string company = "";
            try {
                CompanyDS companies = GetCompanies();
                CompanyDS.CompanyTableRow c = (CompanyDS.CompanyTableRow)companies.CompanyTable.Select("CompanyID=" + companyID)[0];
                company = c.CompanyName;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading contact.",ex); }
            return company;
        }
        public LocationDS GetDistricts(string clientNumber) {
            //Get a list of client divisions
            LocationDS districts = null;
            try {
                districts = new LocationDS();
                districts.LocationTable.AddLocationTableRow("All","All");
                if(clientNumber.Length > 3) clientNumber = clientNumber.Substring(clientNumber.Length - 3,3);
                if(clientNumber == "000") clientNumber = null;
                DataSet ds = fillDataset(USP_REGIONS_DISTRICTS,TBL_DISTRICTS,new object[] { clientNumber });
                if(ds.Tables[TBL_DISTRICTS].Rows.Count > 0)
                    districts.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating company districts list.",ex); }
            return districts;
        }
        public LocationDS GetRegions(string clientNumber) {
            //Get a list of client divisions
            LocationDS regions = null;
            try {
                regions = new LocationDS();
                regions.LocationTable.AddLocationTableRow("All","All");
                if(clientNumber.Length > 3) clientNumber = clientNumber.Substring(clientNumber.Length - 3,3);
                if(clientNumber == "000") clientNumber = null;
                DataSet ds = fillDataset(USP_REGIONS_DISTRICTS,TBL_REGIONS,new object[] { clientNumber });
                if(ds.Tables[TBL_REGIONS].Rows.Count > 0)
                    regions.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating company regions list.",ex); }
            return regions;
        }
        public AgentDS GetAgents() { 
            //
            AgentDS agents = new AgentDS();
            DataSet ds = fillDataset(USP_AGENTS,TBL_AGENTS,new object[] { });
            if(ds.Tables[TBL_AGENTS].Rows.Count > 0) {
                AgentDS _ds = new AgentDS();
                _ds.Merge(ds);
                for(int i = 0;i < _ds.AgentTable.Rows.Count;i++) {
                    _ds.AgentTable.Rows[i]["AgentSummary"] = (!_ds.AgentTable.Rows[i].IsNull("MainZone") ? _ds.AgentTable.Rows[i]["MainZone"].ToString().PadLeft(2,' ') : "  ") + " - " +
                                                                 (!_ds.AgentTable.Rows[i].IsNull("AgentNumber") ? _ds.AgentTable.Rows[i]["AgentNumber"].ToString() : "    ") + " - " +
                                                                 (!_ds.AgentTable.Rows[i].IsNull("AgentName") ? _ds.AgentTable.Rows[i]["AgentName"].ToString().Trim() : "");
                }
                agents.Merge(_ds.AgentTable.Select("","MainZone ASC"));
                agents.AgentTable.AcceptChanges();
            }
            return agents;
        }
        public AgentDS GetAgents(string clientNumber) {
            //Get a list of agents for the specified client
            AgentDS agents = null;
            try {
                agents = new AgentDS();
                agents.AgentTable.AddAgentTableRow("All","All","","","All");
                if(clientNumber.Length > 3) clientNumber = clientNumber.Substring(clientNumber.Length - 3,3);
                if(clientNumber == "000") clientNumber = null;
                DataSet ds = fillDataset(USP_AGENTS_BYCLIENT,TBL_AGENTS,new object[] { clientNumber });
                if(ds.Tables[TBL_AGENTS].Rows.Count > 0) {
                    AgentDS _ds = new AgentDS();
                    _ds.Merge(ds);
                    for(int i = 0;i < _ds.AgentTable.Rows.Count;i++) {
                        _ds.AgentTable.Rows[i]["AgentSummary"] = (!_ds.AgentTable.Rows[i].IsNull("MainZone") ? _ds.AgentTable.Rows[i]["MainZone"].ToString().PadLeft(2,' ') : "  ") + " - " +
                                                             (!_ds.AgentTable.Rows[i].IsNull("AgentNumber") ? _ds.AgentTable.Rows[i]["AgentNumber"].ToString() : "    ") + " - " +
                                                             (!_ds.AgentTable.Rows[i].IsNull("AgentName") ? _ds.AgentTable.Rows[i]["AgentName"].ToString().Trim() : "");
                    }
                    agents.Merge(_ds.AgentTable.Select("","MainZone ASC"));
                    agents.AgentTable.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating agents list.",ex); }
            return agents;
        }
        public StoreDS GetStoreDetail(int companyID,int storeNumber) {
            //Get a list of store locations
            StoreDS stores = null;
            try {
                stores = new StoreDS();
                DataSet ds = fillDataset(USP_STORE,TBL_STORE,new object[] { companyID,storeNumber,null });
                if(ds.Tables[TBL_STORE].Rows.Count > 0)
                    stores.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading store locations.",ex); }
            return stores;
        }
        public StoreDS GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            StoreDS stores = null;
            try {
                stores = new StoreDS();
                DataSet ds = fillDataset(USP_STORE,TBL_STORE,new object[] { companyID,null,subStore });
                if(ds.Tables[TBL_STORE].Rows.Count > 0)
                    stores.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading store locations.",ex); }
            return stores;
        }

        #region Contact Services: GetContacts(), GetContact(), CreateContact(), UpdateContact()
        public ContactDS GetContacts() {
            //
            ContactDS contacts = null;
            try {
                contacts = new ContactDS();
                DataSet ds = fillDataset(USP_CONTACTS,TBL_CONTACTS,new object[] { });
                if(ds.Tables[TBL_CONTACTS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_CONTACTS].Select("","FirstName ASC"));
                    contacts.Merge(_ds);
                    for(int i = 0;i < contacts.ContactTable.Rows.Count;i++)
                        contacts.ContactTable[i].FullName = contacts.ContactTable[i].FirstName + " " + contacts.ContactTable[i].LastName;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return contacts;
        }
        public ContactDS GetContacts(int companyID,string regionNumber,string districtNumber,string agentNumber,string storeNumber) {
            ContactDS contacts = null;
            try {
                //
                contacts = new ContactDS();
                DataSet ds = fillDataset(USP_CONTACTS_BYLOC,TBL_CONTACTS,new object[] { companyID,regionNumber,districtNumber,agentNumber,storeNumber });
                if(ds.Tables[TBL_CONTACTS].Rows.Count > 0) {
                    contacts.Merge(ds);
                    for(int i = 0;i < contacts.ContactTable.Rows.Count;i++)
                        contacts.ContactTable[i].FullName = contacts.ContactTable[i].FirstName + " " + contacts.ContactTable[i].LastName;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return contacts;
        }
        public Contact GetContact(int contactID) {
            //Get issue types
            Contact contact = null;
            try {
                ContactDS contacts = GetContacts();
                ContactDS.ContactTableRow c = (ContactDS.ContactTableRow)contacts.ContactTable.Select("ID=" + contactID)[0];
                contact = new Contact(c);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading contact.",ex); }
            return contact;
        }
        public int CreateContact(Contact contact) {
            int id = 0;
            try {
                //Save contact
                object o = executeNonQueryWithReturn(USP_CONTACT_NEW,new object[] { null,contact.FirstName,contact.LastName,(contact.Phone.Length>0?contact.Phone:null),(contact.Mobile.Length>0?contact.Mobile:null),(contact.Fax.Length>0?contact.Fax:null),contact.Email });
                id = (int)o;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return id;
        }
        public bool UpdateContact(Contact contact) {
            bool res = false;
            try {
                //Save contact
                object o = executeNonQueryWithReturn(USP_CONTACT_UPDATE,new object[] { contact.ID,contact.FirstName,contact.LastName,(contact.Phone.Length>0?contact.Phone:null),(contact.Mobile.Length>0?contact.Mobile:null),(contact.Fax.Length>0?contact.Fax:null),contact.Email });
                res = true;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return res;
        }
        #endregion
        
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