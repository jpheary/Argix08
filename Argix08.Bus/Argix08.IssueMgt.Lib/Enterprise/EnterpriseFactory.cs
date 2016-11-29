//	File:	EnterpriseFactory.cs
//	Author:	J. Heary
//	Date:	01/08/09
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.Enterprise {
	//
	public class EnterpriseFactory {
		//Members
		private static Mediator _Mediator=null;
        private static EnterpriseDS _Companies = null;
        private static EnterpriseDS _Agents = null;

        public const string USP_CONTACTS = "uspIMContactGetList",TBL_CONTACTS = "IssueContactTable";
        public const string USP_CONTACT_NEW = "uspIMContactNew";
        public const string USP_CONTACTS_BYLOC = "uspIMContactGetListForLocation";
        public const string USP_CONTACT_UPDATE = "uspIMContactUpdate2";
        public const string USP_COMPANIES = "uspIMCompanyGetList",TBL_COMPANIES = "CompanyTable";
        public const string USP_REGIONS_DISTRICTS = "uspIMRegionDistrictGetList",TBL_REGIONS = "RegionTable",TBL_DISTRICTS = "DistrictTable";
        public const string USP_AGENTS = "uspIMAgentGetList",TBL_AGENTS = "AgentTable";
        public const string USP_AGENTS_BYCLIENT = "uspIMAgentGetListForClient";
        public const string USP_STORE = "uspIMStoreGet1",TBL_STORE = "StoreTable";
        public const string USP_DELIVERY = "uspIMDeliveryGetList1 ",TBL_DELIVERY = "DeliveryTable";
        public const string USP_OSDSCANS = "uspIMDeliveryOSDScansGetList ",TBL_OSDSCANS = "OSDScanTable";
        public const string USP_PODSCANS = "uspIMDeliveryPODScansGetList ",TBL_PODSCANS = "PODScanTable";
        
		//Interface
        static EnterpriseFactory() {
            //Constructor
            try {
                _Companies = new EnterpriseDS();
                _Agents = new EnterpriseDS();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new EnterpriseFactory singleton.",ex); }
        }
        private EnterpriseFactory() { }
        public static Mediator Mediator { 
            get { return _Mediator; }
            set { _Mediator = value; RefreshCache(); } 
        }
        public static void RefreshCache() {
            //Refresh cached data
            DataSet ds = null;
            try {
                //Validate
                if(_Mediator == null) return;
                if(!_Mediator.OnLine) return;

                //Companies
                _Companies.Clear();
                ds = _Mediator.FillDataset(USP_COMPANIES,TBL_COMPANIES,null);
                if(ds.Tables[TBL_COMPANIES].Rows.Count > 0) {
                    EnterpriseDS _ds = new EnterpriseDS();
                    _ds.CompanyTable.AddCompanyTableRow(0,"All","000","");
                    _ds.Merge(ds.Tables[TBL_COMPANIES].Select("","CompanyName ASC"));
                    _Companies.Merge(_ds);
                }

                //Agents
                _Agents.Clear();
                ds = _Mediator.FillDataset(USP_AGENTS,TBL_AGENTS,new object[] { });
                if(ds.Tables[TBL_AGENTS].Rows.Count > 0) {
                    EnterpriseDS _ds = new EnterpriseDS();
                    _ds.Merge(ds);
                    for(int i = 0;i < _ds.AgentTable.Rows.Count;i++) {
                        _ds.AgentTable.Rows[i]["AgentSummary"] = (!_ds.AgentTable.Rows[i].IsNull("MainZone") ? _ds.AgentTable.Rows[i]["MainZone"].ToString().PadLeft(2,' ') : "  ") + " - " +
                                                                 (!_ds.AgentTable.Rows[i].IsNull("AgentNumber") ? _ds.AgentTable.Rows[i]["AgentNumber"].ToString() : "    ") + " - " +
                                                                 (!_ds.AgentTable.Rows[i].IsNull("AgentName") ? _ds.AgentTable.Rows[i]["AgentName"].ToString().Trim() : "");
                    }
                    _Agents.Merge(_ds.AgentTable.Select("","MainZone ASC"));
                    _Agents.AgentTable.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while caching EnterpriseFactory data.",ex); }
        }
        #region Company-Location Services: Companies, GetCompany(), GetDistricts(), GetRegions(), Agents, GetStoreDetail()
        public static EnterpriseDS Companies { get { return _Companies; } }
        public static string GetCompany(int companyID) {
            //Get a company for the specified id
            string company = "";
            try {
                EnterpriseDS.CompanyTableRow c = (EnterpriseDS.CompanyTableRow)_Companies.CompanyTable.Select("CompanyID=" + companyID)[0];
                company = c.CompanyName;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading contact.",ex); }
            return company;
        }
        public static EnterpriseDS GetDistricts(string clientNumber) {
            //Get a list of client divisions
            EnterpriseDS districts = null;
            try {
                districts = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_REGIONS_DISTRICTS,TBL_DISTRICTS,new object[] { clientNumber });
                    if(ds.Tables[TBL_DISTRICTS].Rows.Count != 0)
                        districts.Merge(ds);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating company districts list.",ex); }
            return districts;
        }
        public static EnterpriseDS GetRegions(string clientNumber) {
            //Get a list of client divisions
            EnterpriseDS regions = null;
            try {
                regions = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_REGIONS_DISTRICTS,TBL_REGIONS,new object[] { clientNumber });
                    if(ds.Tables[TBL_REGIONS].Rows.Count != 0)
                        regions.Merge(ds);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating company regions list.",ex); }
            return regions;
        }
        public static EnterpriseDS Agents { get { return _Agents; } }
        public static EnterpriseDS GetAgents(string clientNumber) {
            //Get a list of agents for the specified client
            EnterpriseDS agents = null;
            try {
                agents = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_AGENTS_BYCLIENT,TBL_AGENTS,new object[] { clientNumber });
                    if(ds.Tables[TBL_AGENTS].Rows.Count > 0) {
                        EnterpriseDS _ds = new EnterpriseDS();
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
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating agents list.",ex); }
            return agents;
        }
        public static EnterpriseDS GetStoreDetail(int companyID,int storeNumber) {
            //Get a list of store locations
            EnterpriseDS stores = null;
            try {
                stores = new EnterpriseDS();
                if(_Mediator.OnLine) { 
                    DataSet ds = _Mediator.FillDataset(USP_STORE,TBL_STORE,new object[] { companyID,storeNumber,null });
                    if(ds.Tables[TBL_STORE].Rows.Count > 0)
                        stores.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading store locations.",ex); }
            return stores;
        }
        public static EnterpriseDS GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            EnterpriseDS stores = null;
            try {
                stores = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_STORE,TBL_STORE,new object[] { companyID,null,subStore });
                    if(ds.Tables[TBL_STORE].Rows.Count > 0)
                        stores.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading store locations.",ex); }
            return stores;
        }
        #endregion
        #region Freight Services: GetDeliveries(), GetDelivery(), GetOSDScans(), GetPODScans()
        public static EnterpriseDS GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            EnterpriseDS deliveries = null;
            try {
                deliveries = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_DELIVERY,TBL_DELIVERY,new object[] { companyID,storeNumber,from,to });
                    if(ds.Tables[TBL_DELIVERY].Rows.Count > 0)
                        deliveries.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading deliveries.",ex); }
            return deliveries;
        }
        public static EnterpriseDS GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID) {
            //Get a list of store locations
            EnterpriseDS delivery = null;
            try {
                delivery = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    EnterpriseDS ds = new EnterpriseDS();
                    ds.Merge(_Mediator.FillDataset(USP_DELIVERY,TBL_DELIVERY,new object[] { companyID,storeNumber,from,to }));
                    if(ds.DeliveryTable.Rows.Count > 0) {
                        delivery.Merge(ds.DeliveryTable.Select("CPROID=" + proID));
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading deliveries.",ex); }
            return delivery;
        }
        public static EnterpriseDS GetOSDScans(long cProID) {
            //Get a list of store locations
            EnterpriseDS scans = null;
            try {
                scans = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_OSDSCANS,TBL_OSDSCANS,new object[] { cProID });
                    if(ds.Tables[TBL_OSDSCANS].Rows.Count > 0) scans.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading OS&D scans.",ex); }
            return scans;
        }
        public static EnterpriseDS GetPODScans(long cProID) {
            //Get a list of store locations
            EnterpriseDS scans = null;
            try {
                scans = new EnterpriseDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_PODSCANS,TBL_PODSCANS,new object[] { cProID });
                    if(ds.Tables[TBL_PODSCANS].Rows.Count > 0) scans.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading POD scans.",ex); }
            return scans;
        }
        #endregion
        #region Contact Services: GetContacts(), GetContact(), CreateContact(), UpdateContact()
        public static ContactDS GetContacts() { 
            //
            ContactDS contacts = null;
            try {
                //
                contacts = new ContactDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_CONTACTS,TBL_CONTACTS,null);
                    if(ds.Tables[TBL_CONTACTS].Rows.Count > 0) {
                        DataSet _ds = new DataSet();
                        _ds.Merge(ds.Tables[TBL_CONTACTS].Select("","FirstName ASC"));
                        contacts.Merge(_ds);
                        for(int i = 0;i < contacts.IssueContactTable.Rows.Count;i++)
                            contacts.IssueContactTable[i].FullName = contacts.IssueContactTable[i].FirstName + " " + contacts.IssueContactTable[i].LastName;
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return contacts;
        }
        public static ContactDS GetContacts(int companyID,string regionNumber,string districtNumber,string agentNumber,string storeNumber) {
            ContactDS contacts = null; 
            try {
                //
                contacts = new ContactDS();
                if(_Mediator.OnLine) {
                    DataSet ds = _Mediator.FillDataset(USP_CONTACTS_BYLOC,TBL_CONTACTS,new object[] { companyID,regionNumber,districtNumber,agentNumber,storeNumber });
                    if(ds.Tables[TBL_CONTACTS].Rows.Count > 0) {
                        contacts.Merge(ds);
                        for(int i = 0;i < contacts.IssueContactTable.Rows.Count;i++)
                            contacts.IssueContactTable[i].FullName = contacts.IssueContactTable[i].FirstName + " " + contacts.IssueContactTable[i].LastName;
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return contacts;
        }
        public static Contact GetContact(int contactID) {
            //Get issue types
            Contact contact = null;
            try {
                ContactDS contacts = GetContacts();
                ContactDS.IssueContactTableRow c = (ContactDS.IssueContactTableRow)contacts.IssueContactTable.Select("ID=" + contactID)[0];
                contact = new Contact(c);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading contact.",ex); }
            return contact;
        }
        public static int CreateContact(Contact contact) {
            int id = 0;
            try {
                //Save contact
                object o = _Mediator.ExecuteNonQueryWithReturn(USP_CONTACT_NEW,new object[] { null,contact.FirstName,contact.LastName,(contact.Phone.Length>0?contact.Phone:null),(contact.Mobile.Length>0?contact.Mobile:null),(contact.Fax.Length>0?contact.Fax:null),contact.Email });
                id = (int)o;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return id;
        }
        public static bool UpdateContact(Contact contact) {
            bool res = false;
            try {
                //Save contact
                object o = _Mediator.ExecuteNonQueryWithReturn(USP_CONTACT_UPDATE,new object[] { contact.ID,contact.FirstName,contact.LastName,(contact.Phone.Length>0?contact.Phone:null),(contact.Mobile.Length>0?contact.Mobile:null),(contact.Fax.Length>0?contact.Fax:null),contact.Email });
                res = true;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new contact.",ex); }
            return res;
        }
        #endregion
    }
}
