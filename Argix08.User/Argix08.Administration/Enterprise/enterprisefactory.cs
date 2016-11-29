//	File:	enterprisefactory.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Tsort.Data;
using Tsort.Windows;

namespace Tsort.Enterprise {
	//
	public class EnterpriseFactory {
		//Members
		public static Mediator Mediator=null;
		public static int CompanyID=0;
		
		//Constants
		private const string USP_COUNTRIES="uspEnterpriseCountryGetList", TBL_COUNTRIES="CountryDetailTable";
		private const string USP_STATES="uspEnterpriseStateGetList", TBL_STATES="StateListTable";
		private const string USP_ADDRESSTYPES="uspEnterpriseAddressTypeGetList", TBL_ADDRESSTYPES="AddressTypeListTable";
		private const string USP_FREIGHTSORTTYPES="uspEnterpriseFreightSortTypeGetList", TBL_FREIGHTSORTTYPES="SelectionListTable";
		
		//Interface
		static EnterpriseFactory() { }
		#region Common
		public static CountryDS GetCountries() {
			//Get a list of countries
			CountryDS countries=null;
			try {
				countries = new CountryDS();
				DataSet ds = Mediator.FillDataset(USP_COUNTRIES, TBL_COUNTRIES, null);
				if(ds != null) 
					countries.Merge(ds.Tables[TBL_COUNTRIES].Select("", "Country", DataViewRowState.CurrentRows));
			} 
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while getting countries.", ex); }
			return countries;
		}
		public static StateDS GetStates() {
			//Get a list of US states
			StateDS states=null;
			try {
				states = new StateDS();
				DataSet ds = Mediator.FillDataset(USP_STATES, TBL_STATES, null);
				if(ds != null) 
					states.Merge(ds.Tables[TBL_STATES].Select("", "State", DataViewRowState.CurrentRows));
			} 
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while getting states.", ex); }
			return states;
		}
		public static AddressTypeDS GetAddressTypes() {
			//Get a list of adddress types (ie location, mailing)
			AddressTypeDS addressTypes=null;
			try {
				addressTypes = new AddressTypeDS();
				DataSet ds = Mediator.FillDataset(USP_ADDRESSTYPES, TBL_ADDRESSTYPES, null);
				if(ds != null) 
					addressTypes.Merge(ds.Tables[TBL_ADDRESSTYPES].Select("", "AddressType", DataViewRowState.CurrentRows));
			} 
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while getting addresse types.", ex); }
			return addressTypes;
		}
		public static SelectionList GetFreightSortTypes() {
			//Get a selection list for terminal lane types
			SelectionList sortTypes = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset(USP_FREIGHTSORTTYPES, TBL_FREIGHTSORTTYPES, null);
				if(ds != null) 
					sortTypes.Merge(ds);
			} 
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while getting addresse types.", ex); }
			return sortTypes;
		}
		#endregion
		#region Agents
		public static AgentDS ViewAgents() {
			//Get a list of agents
			AgentDS agents = new AgentDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseAgentGetList", "AgentViewTable", null);
				if(ds != null) 
					agents.Merge(ds.Tables["AgentViewTable"].Select("", "AgentName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return agents;
		}
		public static AgentDS GetAgent(int agentID) {
			//Get a new or existing agent
			AgentDS agent = new AgentDS();
			try {
				if(agentID == 0) {
					//New
					AgentDS.AgentDetailTableRow row = agent.AgentDetailTable.NewAgentDetailTableRow();
					row.AgentID = agentID;
					row.Number = "";
					row.AgentName = "";
					row.Phone = "";
					row.Extension = "";
					row.AddressLine1 = "";
					row.AddressLine2 = "";
					row.City = "";
					row.StateOrProvince = "NJ";
					row.PostalCode = "";
					row.CountryID = 1;
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					agent.AgentDetailTable.AddAgentDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseAgentGet", "AgentDetailTable", new object[]{agentID});
					if(ds != null)
						agent.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return agent;
		}
		public static int CreateAgent(AgentDS agent) {
			//Create a new agent
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseAgentNew", new object[]{agent});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateAgent(AgentDS agent) {
			//Update an existing agent
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseAgentUpdate", new object[]{agent});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Clients
		public static ClientDS ViewClients() {
			//Get a list of clients
			ClientDS clients = new ClientDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseClientGetList", "ClientViewTable", null);
				if(ds != null) 
					clients.Merge(ds.Tables["ClientViewTable"].Select("", "ClientName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return clients;
		}
		public static SelectionList GetClients() {
			//Get a list of clients
			SelectionList clients = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseClientGetList", "SelectionListTable", null);
				if(ds != null) 
					clients.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return clients;
		}
		public static SelectionList GetClients(int terminalID) {
			//Get a list of clients that are associated with the specified terminal
			SelectionList clients = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseClientGetList", "SelectionListTable", new object[]{terminalID});
				if(ds != null) 
					clients.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return clients;
		}
		public static ClientDS GetClient(int id) {
			//Get a new or existing switcher
			ClientDS client = new ClientDS();
			try {
				if(id == 0) {
					//New
					ClientDS.ClientDetailTableRow row = client.ClientDetailTable.NewClientDetailTableRow();
					row.ClientID = id;
					row.Number = "";
					row.ClientName = "";
					row.Phone = "";
					row.Extension = "";
					row.AddressLine1 = "";
					row.AddressLine2 = "";
					row.City = "";
					row.StateOrProvince = "NJ";
					row.PostalCode = "";
					row.CountryID = 1;
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					client.ClientDetailTable.AddClientDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseClientGet", "ClientDetailTable", new object[]{id});
					if(ds != null)
						client.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return client;
		}
		public static int CreateClient(ClientDS client) {
			//Create a new client
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseClientNew", new object[]{client});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateClient(ClientDS client) {
			//Update an existing client
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseClientUpdate", new object[]{client});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		
		public static CVLocationDS ViewCVLocations(int clientID) {
			//Get a list of client-vendor location associations
			CVLocationDS locations = new CVLocationDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseCVLGetListForClient", "CVLocationViewTable", new object[]{clientID});
				if(ds != null) 
					locations.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return locations;
		}
		public static CVLocationDS GetCVLocation(int assocID) {
			//Get a new or existing client-vendor location association
			CVLocationDS location = new CVLocationDS();
			try {
				if(assocID == 0) {
					//New location
					CVLocationDS.CVLocationDetailTableRow row = location.CVLocationDetailTable.NewCVLocationDetailTableRow();
					row.LinkID = assocID;
					//row.ClientID = row.VendorID = row.LocationID = 0;
					//row.VendorName = row.Number = ""; 
					row.Description = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = Environment.UserName;
					row.RowVersion = "";
					location.CVLocationDetailTable.AddCVLocationDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseCVLGet", "CVLocationDetailTable", new object[]{assocID});
					if(ds != null)
						location.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return location;
		}
		public static SortProfileDS GetCVLocationSortProfileTemplate(int terminalID, int assocID) {
			//Get a template for client-vendor location association sort profiles
			SortProfileDS profile = new SortProfileDS();
			try {
				//Build the template with a unique row for each freight sort type
				//and a list of enterprise terminals within each row
				SelectionList dsSortTypes = EnterpriseFactory.GetFreightSortTypes();
				SelectionList dsTerminals = EnterpriseFactory.GetEntTerminals(terminalID);
				for(int i=0; i<dsSortTypes.SelectionListTable.Rows.Count; i++) {
					SortProfileDS.SortProfileTableRow row = profile.SortProfileTable.NewSortProfileTableRow();
					row.Selected = false;
					row.LinkID = assocID;
					row.ProfileID = row.LabelID = 0;
					row.SortType = dsSortTypes.SelectionListTable[i].Description;
					row.SortTypeID = Convert.ToInt32(dsSortTypes.SelectionListTable[i].ID);
					row.IsElectronic = false;
					row.ManifestPerTrailer = "N";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = Environment.UserName;
					row.RowVersion = "";
					profile.SortProfileTable.AddSortProfileTableRow(row);
					
					//Create embedded terminal list
					for(int j=0; j<dsTerminals.SelectionListTable.Rows.Count; j++) 
						profile.SortProfileTerminalTable.AddSortProfileTerminalTableRow(false, row.ProfileID, Convert.ToInt32(dsTerminals.SelectionListTable[j].ID), dsTerminals.SelectionListTable[j].Description, row);
				}
			} 
			catch(Exception ex) { throw ex; }
			return profile;
		}
		public static ClientTerminalDS GetFreightProcessingTerminalTemplate() {
			//Get a template for client-vendor location association sort profiles
			ClientTerminalDS template = new ClientTerminalDS();
			try {
				//Build the template with a unique row for each enterprise terminal
				SelectionList dsTerminals = GetEntTerminals();
				for(int j=0; j<dsTerminals.SelectionListTable.Rows.Count; j++) 
					template.ClientTerminalTable.AddClientTerminalTableRow(false, 0, Convert.ToInt32(dsTerminals.SelectionListTable[j].ID), dsTerminals.SelectionListTable[j].Description, DateTime.Now, Environment.UserName); 
			} 			
			catch(Exception ex) { throw ex; }
			return template;
		}
		public static SelectionList GetAvailableVendors(int clientID) {
			//Get a list of vendors for the specified client
			SelectionList vendors = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseVendorGetListAvailableForCVL", "SelectionListTable", new object[]{clientID});
				if(ds != null) 
					vendors.Merge(ds);	//.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return vendors;
		}
		public static LocationDS GetAvailableVendorLocations(int clientID, int vendorID) {
			//Get a list of vendor locations for user selection
			LocationDS locations = new LocationDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseLocationGetListAvailableForCVL", "LocationListTable", new object[]{clientID, vendorID});
				if(ds != null) 
					locations.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return locations;
		}
		public static SelectionList GetInboundLabels(int sortTypeID) {
			//Get a list of inbound labels that have the same freight sort type for user selection
			SelectionList labels = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "", new object[]{sortTypeID});
				if(ds != null) 
					labels.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return labels;
		}
		public static int CreateCVLocation(CVLocationDS association) {
			//Create a new client-vendor location association
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseCVLNew", new object[]{association});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateCVLocation(CVLocationDS association) {
			//Update an existing client-vendor location association
			bool result = false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseCVLUpdate", new object[]{association});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Vendors
		public static VendorDS ViewVendors() {
			//Get a list of vendors
			VendorDS vendors = new VendorDS();	
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseVendorGetList", "VendorViewTable", null);
				if(ds != null && ds.Tables["VendorViewTable"] != null) 
					vendors.Merge(ds.Tables["VendorViewTable"].Select("", "VendorName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return vendors;
		}
		public static SelectionList GetVendors() {
			//Get a list of vendors for user selection
			SelectionList vendors = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseVendorGetList", "SelectionListTable", null);
				if(ds != null) 
					vendors.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return vendors;
		}
		public static VendorDS GetVendor(int vendorID) {
			//Get a new or existing vendor
			VendorDS vendor = new VendorDS();
			try {
				if(vendorID == 0) {
					//New
					VendorDS.VendorDetailTableRow row = vendor.VendorDetailTable.NewVendorDetailTableRow();
					row.VendorID = vendorID;
					row.Number = "";
					row.VendorName = "";
					row.Phone = "";
					row.Extension = "";
					row.AddressLine1 = "";
					row.AddressLine2 = "";
					row.City = "";
					row.StateOrProvince = "NJ";
					row.PostalCode = "";
					row.CountryID = 1;
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.Region = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					vendor.VendorDetailTable.AddVendorDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseVendorGet", "VendorDetailTable", new object[]{vendorID});
					if(ds != null)
						vendor.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return vendor;
		}
		public static int CreateVendor(VendorDS vendor) {
			//Create a new vendor
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseVendorNew", new object[]{vendor});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateVendor(VendorDS vendor) {
			//Update an existing vendor
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseVendorUpdate", new object[]{vendor});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region PaymentServices
		public static PaymentServiceDS ViewPaymentServices() {
			//Get a view of all payment services
			PaymentServiceDS services = new PaymentServiceDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterprisePaymentServiceGetList", "PaymentServiceViewTable", null);
				if(ds != null) 
					services.Merge(ds.Tables["PaymentServiceViewTable"].Select("", "PaymentServiceName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return services;
		}
		public static PaymentServiceDS GetPaymentService(int serviceID) {
			//Get a new or existing payment service
			PaymentServiceDS service = new PaymentServiceDS();
			try {
				if(serviceID == 0) {
					//New
					PaymentServiceDS.PaymentServiceDetailTableRow row = service.PaymentServiceDetailTable.NewPaymentServiceDetailTableRow();
					row.PaymentServiceID = serviceID;
					row.PaymentServiceNumber = "";
					row.PaymentServiceName = "";
					row.Phone = "";
					row.Extension = "";
					row.AddressLine1 = "";
					row.AddressLine2 = "";
					row.City = "";
					row.StateOrProvince = "NJ";
					row.PostalCode = "";
					row.CountryID = 1;
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					service.PaymentServiceDetailTable.AddPaymentServiceDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterprisePaymentServiceGet", "PaymentServiceDetailTable", new object[]{serviceID});
					if(ds != null)
						service.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return service;
		}
		public static SelectionList GetAvailablePaymentServices(int entityID) {
			//Get a list of payment service that are not associated with the referenced entity
			SelectionList services = new SelectionList();
			try {
				//Existing
				DataSet ds = Mediator.FillDataset("uspEnterprisePaymentServiceGetListNotAssociatedToCompany", "SelectionListTable", new object[]{entityID});
				if(ds != null) 
					services.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return services;
		}
		public static int CreatePaymentService(PaymentServiceDS service) {
			//Create a new payment service
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterprisePaymentServiceNew", new object[]{service});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdatePaymentService(PaymentServiceDS service) {
			//Update an existing payment service
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterprisePaymentServiceUpdate", new object[]{service});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Stores
		public static StoreDS ViewStores(int clientID) {
			//Get a list of stores
			StoreDS stores = new StoreDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseStoreGetList", "StoreViewTable", new object[]{clientID});
				if(ds != null) 
					stores.Merge(ds.Tables["StoreViewTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return stores;
		}
		public static StoreDS GetStore(int clientID, int storeID) {
			//Get a new or existing store
			StoreDS store = new StoreDS();
			try {
				if(storeID == 0) {
					//New
					StoreDS.StoreDetailTableRow row = store.StoreDetailTable.NewStoreDetailTableRow();
					row.CompanyID = clientID;
					row.LocationTypeID = 1;		//Store
					row.LocationID = storeID;
					row.Number = "";
					row.Description = "";
					row.Phone = "";
					row.Extension = "";
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.SanNumber = "";
					row.OpenTime = Convert.ToDateTime("08:00:00");
					row.CloseTime = Convert.ToDateTime("17:00:00");
					row.SpecialInstructions = "";
					row.UserLabelData = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					store.StoreDetailTable.AddStoreDetailTableRow(row);

					StoreDS.AddressDetailTableRow rowA = store.AddressDetailTable.NewAddressDetailTableRow();
					rowA.AddressID = 1;
					rowA.LocationID = storeID;
					rowA.AddressType = "Street";
					rowA.AddressLine1 = "";
					rowA.AddressLine2 = "";
					rowA.City = "";
					rowA.StateOrProvince = "NJ";
					rowA.PostalCode = "";
					rowA.CountryID = 1;
					rowA.IsActive = true;
					rowA.LastUpdated = DateTime.Now;
					rowA.UserID = System.Environment.UserName;
					rowA.RowVersion = "";
					store.AddressDetailTable.AddAddressDetailTableRow(rowA);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseStoreGet", "StoreDetailTable", new object[]{clientID, storeID});
					if(ds != null)
						store.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return store;
		}
		public static int CreateStore(StoreDS store) {
			//Create a new store
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseStoreNew", new object[]{store});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateStore(StoreDS store) {
			//Update an existing store
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseStoreUpdate", new object[]{store});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Warehouses
		public static WarehouseDS ViewWarehouses(int entityID) {
			//Get a list of locations
			WarehouseDS warehouses = new WarehouseDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseWarehouseGetList", "WarehouseViewTable", new object[]{entityID});
				if(ds != null) 
					warehouses.Merge(ds.Tables["WarehouseViewTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return warehouses;
		}
		public static WarehouseDS GetWarehouse(int entityID, int locationID) {
			//Get a new or existing location
			WarehouseDS warehouse = new WarehouseDS();
			try {
				if(locationID==0) {
					//New
					WarehouseDS.WarehouseDetailTableRow row = warehouse.WarehouseDetailTable.NewWarehouseDetailTableRow();
					row.CompanyID = entityID;
					row.LocationTypeID = 1;		//Location
					row.LocationID = locationID;
					row.Number = "";
					row.Description = "";
					row.Phone = "";
					row.Extension = "";
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.OpenTime = Convert.ToDateTime("08:00:00");
					row.CloseTime = Convert.ToDateTime("17:00:00");
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					warehouse.WarehouseDetailTable.AddWarehouseDetailTableRow(row);

					WarehouseDS.AddressDetailTableRow rowA = warehouse.AddressDetailTable.NewAddressDetailTableRow();
					rowA.AddressID = 1;
					rowA.LocationID = locationID;
					rowA.AddressType = "Street";
					rowA.AddressLine1 = "";
					rowA.AddressLine2 = "";
					rowA.City = "";
					rowA.StateOrProvince = "NJ";
					rowA.PostalCode = "";
					rowA.CountryID = 1;
					rowA.IsActive = true;
					rowA.LastUpdated = DateTime.Now;
					rowA.UserID = System.Environment.UserName;
					rowA.RowVersion = "";
					warehouse.AddressDetailTable.AddAddressDetailTableRow(rowA);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseWarehouseGet", "WarehouseDetailTable", new object[]{entityID, locationID});
					if(ds != null) 
						warehouse.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return warehouse;
		}
		public static int CreateWarehouse(WarehouseDS warehouse) {
			//Create a new location
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseWarehouseNew", new object[]{warehouse});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateWarehouse(WarehouseDS warehouse) {
			//Update an existing location
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseWarehouseUpdate", new object[]{warehouse});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Terminals
		public static TerminalDS ViewTerminals(int companyID) {
			//Get a list of terminals
			TerminalDS terminals = new TerminalDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseTerminalGetList", "TerminalViewTable", new object[]{companyID});
				if(ds != null) 
					terminals.Merge(ds.Tables["TerminalViewTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return terminals;
		}
		public static TerminalDS GetTerminal(int companyID, int terminalID) {
			//Get a new or existing location
			TerminalDS terminal = new TerminalDS();
			try {
				if(terminalID == 0) {
					//New
					TerminalDS.TerminalDetailTableRow row = terminal.TerminalDetailTable.NewTerminalDetailTableRow();
					row.CompanyID = companyID;
					row.LocationTypeID = 1;		//Location
					row.LocationID = terminalID;
					row.Number = "";
					row.Description = "";
					row.Phone = "";
					row.Extension = "";
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.OpenTime = Convert.ToDateTime("08:00:00");
					row.CloseTime = Convert.ToDateTime("17:00:00");
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					terminal.TerminalDetailTable.AddTerminalDetailTableRow(row);

					TerminalDS.AddressDetailTableRow rowA = terminal.AddressDetailTable.NewAddressDetailTableRow();
					rowA.AddressID = 1;
					rowA.LocationID = terminalID;
					rowA.AddressType = "Street";
					rowA.AddressLine1 = "";
					rowA.AddressLine2 = "";
					rowA.City = "";
					rowA.StateOrProvince = "NJ";
					rowA.PostalCode = "";
					rowA.CountryID = 1;
					rowA.IsActive = true;
					rowA.LastUpdated = DateTime.Now;
					rowA.UserID = System.Environment.UserName;
					rowA.RowVersion = "";
					terminal.AddressDetailTable.AddAddressDetailTableRow(rowA);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseTerminalGet", "TerminalDetailTable", new object[]{companyID, terminalID});
					if(ds != null) 
						terminal.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return terminal;
		}
		public static int CreateTerminal(TerminalDS terminal) {
			//Create a new terminal
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseTerminalNew", new object[]{terminal});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateTerminal(TerminalDS terminal) {
			//Update an existing terminal
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseTerminalUpdate", new object[]{terminal});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region EntTerminals
		public static EnterpriseDS ViewEntTerminals() {
			//Get a list of enterprise terminals
			EnterpriseDS terminals = new EnterpriseDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseCompanyGetEnterprise", "EntTerminalViewTable", null);
				if(ds != null) 
					terminals.Merge(ds.Tables["EntTerminalViewTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return terminals;
		}
		public static SelectionList GetEntTerminals() {
			//Get a selection list for enterprise terminals
			SelectionList terminals = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseCompanyGetEnterprise", "SelectionListTable", null);
				if(ds != null) 
					terminals.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return terminals;
		}
		public static SelectionList GetEntTerminals(int clientID) {
			//Get a selection list for enterprise terminals for a client
			SelectionList terminals = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "SelectionListTable", new object[]{clientID});
				if(ds != null) 
					terminals.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return terminals;
		}
		public static EnterpriseDS GetEntTerminal(int terminalID) {
			//Get a new or existing terminal
			EnterpriseDS terminal = new EnterpriseDS();
			try {
				if(terminalID == 0) {
					//New
					EnterpriseDS.EntTerminalDetailTableRow row = terminal.EntTerminalDetailTable.NewEntTerminalDetailTableRow();
					row.CompanyID = EnterpriseFactory.CompanyID;
					row.LocationTypeID = 2;		//Terminal
					row.LocationID = terminalID;
					row.Number = "";
					row.Description = "";
					row.Phone = "";
					row.Extension = "";
					row.ContactName = "";
					row.Fax = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					terminal.EntTerminalDetailTable.AddEntTerminalDetailTableRow(row);
					
					EnterpriseDS.AddressDetailTableRow rowA = terminal.AddressDetailTable.NewAddressDetailTableRow();
					rowA.AddressID = 1;
					rowA.LocationID = terminalID;
					rowA.AddressType = "Street";
					rowA.AddressLine1 = "";
					rowA.AddressLine2 = "";
					rowA.City = "";
					rowA.StateOrProvince = "NJ";
					rowA.PostalCode = "";
					rowA.CountryID = 1;
					rowA.IsActive = true;
					rowA.LastUpdated = DateTime.Now;
					rowA.UserID = System.Environment.UserName;
					rowA.RowVersion = "";
					terminal.AddressDetailTable.AddAddressDetailTableRow(rowA);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "EntTerminalDetailTable", new object[]{terminalID});
					if(ds != null)
						terminal.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return terminal;
		}
		public static int CreateEntTerminal(EnterpriseDS terminal) {
			//Create a new terminal
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{terminal});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateEntTerminal(EnterpriseDS terminal) {
			//Update an existing terminal
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{terminal});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion

		#region Freight Paths
		public static FreightPathDS ViewFreightPaths() {
			//Get a view of all freight paths
			FreightPathDS paths = new FreightPathDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseFreightPathGetList", "FreightPathViewTable", null);
				if(ds != null) 
					paths.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return paths;
		}
		public static SelectionList GetFreightPaths(int terminalID) {
			//Get a list of freight paths for the requested terminal
			SelectionList paths = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseFreightPathGetList", "SelectionListTable", new object[]{terminalID});
				if(ds != null) 
					paths.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return paths;
		}
		public static FreightPathDS GetFreightPath(string pathID) {
			//Get a new or existing freight path
			FreightPathDS path = new FreightPathDS();
			try {
				if(pathID.Length == 0) {
					//New
					FreightPathDS.FreightPathDetailTableRow row = path.FreightPathDetailTable.NewFreightPathDetailTableRow();
					row.PathID = pathID;
					row.SortCenterID = 0;
					row.PathType = "";
					row.Description = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					path.FreightPathDetailTable.AddFreightPathDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseFreightPathGet", "FreightPathDetailTable", new object[]{pathID});
					if(ds != null)
						path.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return path;
		}
		public static SelectionList GetFreightPathTypes() {
			//Get a selection list of valid freight path types
			SelectionList pathTypes = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseFreightPathTypeGetList", "SelectionListTable", null);
				if(ds != null) 
					pathTypes.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return pathTypes;
		}
		public static SelectionList GetSortCenters() {
			//Get a selection list for sort centers (Argix terminals)
			SelectionList sortCenters = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseTerminalGetList", "SelectionListTable", new object[]{EnterpriseFactory.CompanyID});
				if(ds != null) 
					sortCenters.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return sortCenters;
		}
		public static string CreateFreightPath(FreightPathDS path) {
			//Create a new terminal freigth path
			string result="";
			try {
				result = (string)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseFreightPathNew", new object[]{path});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateFreightPath(FreightPathDS path) {
			//Update an existing freigth path
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseFreightPathUpdate", new object[]{path});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Store Error Codes
		public static StoreErrorCodeDS ViewStoreErrorCodes() {
			//Get a list of store error codes
			StoreErrorCodeDS codes = new StoreErrorCodeDS();
			try {
				DataSet ds = Mediator.FillDataset("", "StoreErrorCodeTable", null);
				if(ds != null) 
					codes.Merge(ds.Tables["StoreErrorCodeTable"].Select("", "Code", DataViewRowState.CurrentRows));
			}
			catch(Exception ex) { throw ex; }
			return codes;
		}
		public static StoreErrorCodeDS GetStoreErrorCode(int codeID) {
			//Get a new or existing damage code
			StoreErrorCodeDS code = new StoreErrorCodeDS();
			try {
				if(codeID == 0) {
					//New
					StoreErrorCodeDS.StoreErrorCodeTableRow row = code.StoreErrorCodeTable.NewStoreErrorCodeTableRow();
					row.CodeID = 0;
					row.Code = "";
					row.Description = "";
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					code.StoreErrorCodeTable.AddStoreErrorCodeTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "StoreErrorCodeTable", new object[]{codeID});
					if(ds != null)
						code.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return code;
		}
		public static int CreateStoreErrorCode(StoreErrorCodeDS code) {
			//Create a new store error code
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{code});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		
		public static bool UpdateStoreErrorCode(StoreErrorCodeDS code) {
			//Update an existing store error code
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{code});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool DeleteStoreErrorCode(int codeID) {
			//Update an existing store error code
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{codeID});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Terminal Lanes
		public static TerminalLaneDS ViewTerminalLanes(int terminalID) {
			//Get a view of terminal lanes
			TerminalLaneDS lanes = new TerminalLaneDS();
			try {
				DataSet ds = Mediator.FillDataset("", "TerminalLaneTable", new object[]{terminalID});
				if(ds != null) 
					lanes.Merge(ds.Tables["TerminalLaneTable"].Select("TerminalID=" + terminalID, "LaneNumber", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return lanes;
		}
		public static SelectionList GetTerminalLaneTypes() {
			//Get a selection list for terminal lane types
			SelectionList laneTypes = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "", null);
				if(ds != null) 
					laneTypes.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return laneTypes;
		}
		public static TerminalLaneDS GetTerminalLane(int terminalID, string laneID) {
			//Get a new or existing terminal lane
			TerminalLaneDS lane = new TerminalLaneDS();
			try {
				if(laneID=="") {
					//New
					TerminalLaneDS.TerminalLaneTableRow row = lane.TerminalLaneTable.NewTerminalLaneTableRow();
					row.LaneID = laneID;
					row.LaneNumber = "";
					//row.LaneTypeID = 0;
					//row.LaneType = "";
					row.Description = "";
					row.TerminalID = terminalID;
					//row.Terminal = "";
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					lane.TerminalLaneTable.AddTerminalLaneTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "TerminalLaneTable", new object[]{terminalID, laneID});
					if(ds != null)
						lane.Merge(ds.Tables["TerminalLaneTable"].Select("LaneID='" + laneID + "'"));
				}
			} 
			catch(Exception ex) { throw ex; }
			return lane;
		}
		public static string CreateTerminalLane(TerminalLaneDS lane) {
			//Create a new terminal lane
			string result="";
			try {
				result = (string)Mediator.ExecuteNonQueryWithReturn("", new object[]{lane});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateTerminalLane(TerminalLaneDS lane) {
			//Update an existing terminal lane
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{lane});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool DeleteTerminalLane(string laneID, string rowVer) {
			//Delete an existing terminal lane
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{laneID, rowVer});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Terminal Workstations
		public static WorkstationlDS ViewTerminalWorkstations(int terminalID) {
			//Get a view of terminal workstations
			WorkstationlDS workstations = new WorkstationlDS();
			try {
				DataSet ds = Mediator.FillDataset("", "TerminalWorkstationTable", new object[]{terminalID});
				if(ds != null) 
					workstations.Merge(ds.Tables["TerminalWorkstationTable"].Select("TerminalID=" + terminalID, "Number", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return workstations;
		}
		public static SelectionList GetWorkstationScaleTypes() {
			//Get a selection list for workstation scale types
			SelectionList scaleTypes = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "", null);
				if(ds != null) 
					scaleTypes.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return scaleTypes;
		}
		public static SelectionList GetWorkstationPrinterTypes() {
			//Get a selection list for workstation printer types
			SelectionList printerTypes = new SelectionList();
			try {
				printerTypes.SelectionListTable.AddSelectionListTableRow("1", "170xiII");
				printerTypes.SelectionListTable.AddSelectionListTableRow("2", "170xiIII");
				printerTypes.SelectionListTable.AddSelectionListTableRow("3", "170xiIII Plus");
			} 
			catch(Exception ex) { throw ex; }
			return printerTypes;
		}
		public static SelectionList GetWorkstationPorts() {
			//Get a selection list for workstation ports
			SelectionList ports = new SelectionList();
			try {
				ports.SelectionListTable.AddSelectionListTableRow("COM1","COM1");
				ports.SelectionListTable.AddSelectionListTableRow("COM2","COM2");
				ports.SelectionListTable.AddSelectionListTableRow("COM3","COM3");
				ports.SelectionListTable.AddSelectionListTableRow("COM4","COM4");
			} 
			catch(Exception ex) { throw ex; }
			return ports;
		}
		public static WorkstationlDS GetTerminalWorkstation(int terminalID, string workstationName) {
			//Get a new or existing terminal workstation
			WorkstationlDS workstation = new WorkstationlDS();
			try {
				if(workstationName.Length == 0) {
					//New
					WorkstationlDS.TerminalWorkstationTableRow row = workstation.TerminalWorkstationTable.NewTerminalWorkstationTableRow();
					row.Name = row.Original_Name = workstationName;
					row.Number = "";
					row.Description = "";
					//row.ScaleType = row.ScalePort = row.PrinterType = row.PrinterPort = "";
					row.TerminalID = terminalID;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					workstation.TerminalWorkstationTable.AddTerminalWorkstationTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "TerminalWorkstationTable", new object[]{terminalID, workstationName});
					if(ds != null) {
						workstation.Merge(ds.Tables["TerminalWorkstationTable"].Select("Name='" + workstationName + "'"));
						workstation.TerminalWorkstationTable[0].Original_Name = workstationName;
					}
				}
			} 
			catch(Exception ex) { throw ex; }
			return workstation;
		}
		public static string CreateTerminalWorkstation(WorkstationlDS workstation) {
			//Create a new terminal workstation
			string result="";
			try {
				result = (string)Mediator.ExecuteNonQueryWithReturn("", new object[]{workstation});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateTerminalWorkstation(WorkstationlDS workstation) {
			//Update an existing terminal workstation
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{workstation});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool DeleteTerminalWorkstation(string workstationName, string rowVer) {
			//Delete an existing terminal workstation
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{workstationName, rowVer});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Outbound Service Types
		public static OutboundFreightTypeDS ViewOutboundFreightTypes() {
			//Get a view of all OB freight types
			OutboundFreightTypeDS freightTypes = new OutboundFreightTypeDS();
			try {
				DataSet ds = Mediator.FillDataset("", "", null);
				if(ds != null) 
					freightTypes.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return freightTypes;
		}
		public static OutboundServiceTypeDS ViewOutboundServiceTypes(string OBFreightTypeID) {
			//Get a view of all OB service types
			OutboundServiceTypeDS serviceTypes = new OutboundServiceTypeDS();
			try {
				DataSet ds = Mediator.FillDataset("", "", new object[]{OBFreightTypeID});
				if(ds != null) 
					serviceTypes.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return serviceTypes;
		}
		public static SelectionList GetRegularOutboundServiceTypes() {
			//Get a list of OB service types for regular freight
			SelectionList serviceTypes = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "SelectionListTable", null);
				if(ds != null) 
					serviceTypes.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return serviceTypes;
		}
		public static SelectionList GetReturnOutboundServiceTypes() {
			//Get a list of OB service types for regular freight
			SelectionList serviceTypes = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "SelectionListTable", null);
				if(ds != null) 
					serviceTypes.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return serviceTypes;
		}
		public static OutboundServiceTypeDS GetOutboundServiceType(int serviceID, string OBFreightTypeID) {
			//Get a new or existing OB service type
			OutboundServiceTypeDS serviceType = new OutboundServiceTypeDS();
			try {
				if(serviceID == 0) {
					//New
					OutboundServiceTypeDS.OutboundServiceTypeTableRow row = serviceType.OutboundServiceTypeTable.NewOutboundServiceTypeTableRow();
					row.ServiceID = serviceID;
					row.FreightType = OBFreightTypeID;
					row.Mnemonic = "";
					row.IsPickup = true;
					row.Description = "";
					//row.AgentServiceID = 0;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					serviceType.OutboundServiceTypeTable.AddOutboundServiceTypeTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "OutboundServiceTypeTable", new object[]{serviceID, OBFreightTypeID});
					if(ds != null)
						serviceType.Merge(ds.Tables["OutboundServiceTypeTable"].Select("ServiceID=" + serviceID));
				}
			} 
			catch(Exception ex) { throw ex; }
			return serviceType;
		}
		public static int CreateOutboundServiceType(OutboundServiceTypeDS serviceType) {
			//Create a new OB service type
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{serviceType});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateOutboundServiceType(OutboundServiceTypeDS serviceType) {
			//Update an existing OB service type
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{serviceType});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static OutboundAgentServiceDS GetOutboundAgentServices(int agentID) {
			//Get a list outbound agent services for the specified agent
			OutboundAgentServiceDS services = new OutboundAgentServiceDS();
			try {
				DataSet ds = Mediator.FillDataset("", "", new object[]{agentID});
				if(ds != null) 
					services.Merge(ds);  
			} 
			catch(Exception ex) { throw ex; }
			return services;
		}
		#endregion
		#region Manage Consignee Override
		public static DeliveryLocationDS ViewClientLocations(int id) {
			//Get a view of all (override and non-override) client locations- includes stores 
			//and warehouses (that are associated to the client)
			DeliveryLocationDS locations = new DeliveryLocationDS();
			try {
				DataSet ds = Mediator.FillDataset("", "", new object[]{id});
				if(ds != null) 
					locations.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return locations;
		}
		public static DeliveryLocationDS ViewClientLocationMappings(int id, int locationID) {
			//Get a view of all mappings for the requested client and location
			DeliveryLocationDS mappings = new DeliveryLocationDS();
			try {
				DataSet ds = Mediator.FillDataset("", "", new object[]{id, locationID});
				if(ds != null) 
					mappings.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return mappings;
		}
		public static DeliveryLocationDS GetConsigneeOverrides(int id, int locationID) {
			//Get overrides for 
			DeliveryLocationDS overrides = new DeliveryLocationDS();
			try {
				DataSet ds = Mediator.FillDataset("", "", new object[]{id, locationID});
				if(ds != null) 
					overrides.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return overrides;
		}
		public static DeliveryLocationDS GetConsigneeOverridesTemplate(int id, string clientName) {
			//Get a view (detail) of consignee overrides for the requested client and location
			DeliveryLocationDS overrides = new DeliveryLocationDS();
			try {
				//Build the overrides with a unique row for each enterprise terminal
				SelectionList dsTerminals = GetEntTerminals(id);
				for(int j=0; j<dsTerminals.SelectionListTable.Rows.Count; j++) 
					overrides.DeliveryLocationOverrideDetailTable.AddDeliveryLocationOverrideDetailTableRow(false, Convert.ToInt32(dsTerminals.SelectionListTable[j].ID), dsTerminals.SelectionListTable[j].Description, id, clientName, 0, "", "", "", "", "", "", "", "", 0, "", DateTime.Now, Environment.UserName, ""); 
			} 			
			catch(Exception ex) { throw ex; }
			return overrides;
		}
		public static DeliveryLocationDS GetConsigneeOverrides() { return null; }
		public static bool	UpdateConsigneeOverrides(DeliveryLocationDS overrides) {
			//Update overrides
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{overrides});
			} 			
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Maps
		public static MapDS ViewMaps() {
			//Get a view of all maps
			MapDS maps = new MapDS();
			try {
				DataSet ds = Mediator.FillDataset("", "", null);
				if(ds != null) 
					maps.Merge(ds);
			} 
			catch(Exception ex) { throw ex; }
			return maps;
		}
		public static MapDS GetMap(string mapID, int sortCenterID) {
			//Get a new or existing map
			MapDS map = new MapDS();
			try {
				if(mapID == "") {
					//New- header only
					MapDS.MapDetailTableRow row = map.MapDetailTable.NewMapDetailTableRow();
					row.MapID = mapID;
					row.SortCenterID = sortCenterID;
					//row.SortCenter;
					//row.ClientID;
					//row.ClientName;
					row.Description = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					map.MapDetailTable.AddMapDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "", new object[]{mapID, sortCenterID});
					if(ds != null) {
						MapDS _map = new MapDS();
						_map.Merge(ds, true, MissingSchemaAction.Ignore);
						map.Merge(_map.MapDetailTable);
						map.Merge(_map.PostalCodeMappingTable.Select("", "PostalCode"));
					}
				}
			} 
			catch(Exception ex) { throw ex; }
			return map;
		}
		public static MapDS GetPostalCodeMap(int countryID, string postalCode) {
			//Get a view of all mappings the requested postal code
			MapDS maps = new MapDS();
			try {
				DataSet ds = Mediator.FillDataset("", "PostalCodeMappingTable", new object[]{countryID, postalCode});
				if(ds != null) 
					maps.Merge(ds);	//.Tables["PostalCodeMappingTable"].Select("", "PostalCode"));
			} 
			catch(Exception ex) { throw ex; }
			return maps;
		}
		public static string CreateTerminalMap(MapDS mapping) {
			//Create a new terminal map (header only)
			string result="";
			try {
				result = (string)Mediator.ExecuteNonQueryWithReturn("", new object[]{mapping});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static string CreateClientMapHeader(MapDS mapping) {
			//Create a new client map (header only)
			string result="";
			try {
				result = (string)Mediator.ExecuteNonQueryWithReturn("", new object[]{mapping});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateMapHeader(MapDS map) {
			//Update a client or terminal map (header only)
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{map});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool CreateMapping(MapDS mapping) {
			//Create a new mapping
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{mapping});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateMapping(MapDS mapping) {
			//Update an exisiting mapping
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{mapping});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool DeleteMapping(string mapID, int countryID, string postalCode, string rowVer) {
			//Delete an existing mapping
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{mapID, countryID, postalCode, rowVer});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
	}
}
