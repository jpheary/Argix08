using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web.Security;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    public enum FreightType { Regular=0, Returns }
    
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class EnterpriseService:IEnterpriseService {
        //Members
        public const string USP_ARGIXTERMINALS = "uspRptTerminalsGetList",TBL_ARGIXTERMINALS = "TerminalTable";
        public const string USP_TERMINALS = "uspRptTerminalGetList",TBL_TERMINALS = "TerminalTable";
        public const string USP_CLIENTS = "uspRptClientGetList",TBL_CLIENTS = "ClientTable";
        public const string USP_CLIENTDIVS = "uspRptClientCustomerDivisionGetList",TBL_CLIENTDIVS = "ClientDivisionTable";
        public const string USP_CLIENTTERMINALS = "uspRptTerminalGetListForClient",TBL_CLIENTTERMINALS = "ClientTerminalTable";
        public const string USP_CONSUMERDIRECTCUSTOMERS = "uspRptConsumerDirectCustomerGetList",TBL_CONSUMERDIRECTCUSTOMERS="CustomersTable";
        public const string USP_VENDORS = "uspRptVendorGetlistForClient",TBL_VENDORS = "VendorTable";
        public const string USP_AGENTS = "uspRptAgentGetList",TBL_AGENTS = "AgentTable";
        public const string USP_ZONES = "uspRptZoneMainGetList",TBL_ZONES = "ZoneTable";
        public const string USP_REGIONS_DISTRICTS = "uspRptRegionDistrictGetList",TBL_REGIONS = "RegionTable",TBL_DISTRICTS = "DistrictTable";
        public const string USP_EXCEPTIONS = "uspRptDeliveryExceptionGetList",TBL_EXCEPTIONS = "ExceptionTable";
        public const string USP_PICKUPS = "uspRptPickupsGetListFromToDate",TBL_PICKUPS = "PickupViewTable";
        public const string USP_INDIRECTTRIPS = "uspRptIndirectTripGetList",TBL_INDIRECTTRIPS = "IndirectTripTable";
        public const string USP_TLS = "uspRptTLGetListClosedForDateRange",TBL_TLS = "TLListViewTable";
        public const string USP_SHIFTS = "uspRptTerminalShiftGetListForDate",TBL_SHIFTS = "ShiftTable";
        public const string USP_DAMAGECODES = "uspRptDamageCodeGetList",TBL_DAMAGECODES = "DamageDetailTable";
        public const string USP_LABELSEQNUMBERS = "uspRptSortedItemGetForCartonNumber",TBL_LABELSEQNUMBERS = "LabelStationTable";
        public const string USP_VENDORLOG = "uspRptManifestGetListForClient",TBL_VENDORLOG = "VendorLogTable";

        public const string DAMAGEDESCRIPTON_ALL = "All";
        public const string DAMAGEDESCRIPTON_ALL_EXCEPT_NC = "All, except NON-CONVEYABLE";

        //Interface
        public TerminalDS GetArgixTerminals() {
            //Get a list of Argix terminals
            TerminalDS terminals = null;
            try {
                terminals = new TerminalDS();
                DataSet ds = FillDataset(USP_ARGIXTERMINALS,TBL_ARGIXTERMINALS,new object[] { });
                if(ds.Tables[TBL_ARGIXTERMINALS].Rows.Count != 0) {
                    TerminalDS _terminals = new TerminalDS();
                    _terminals.Merge(ds);
                    terminals.Merge(_terminals.TerminalTable.Select("", "Description ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating Argix terminal list.",ex); }
            return terminals;
        }
        public TerminalDS GetTerminals() {
            //Get a list of Argix terminals
            TerminalDS terminals = null;
            try {
                terminals = new TerminalDS();
                DataSet ds = FillDataset(USP_TERMINALS,TBL_TERMINALS,new object[] { });
                if(ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                    TerminalDS _terminals = new TerminalDS();
                    _terminals.Merge(ds);
                    terminals.Merge(_terminals.TerminalTable.Select("","Description ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating Argix terminal list.",ex); }
            return terminals;
        }
        public ClientDS GetClientsList(bool activeOnly) {
            //Get a list of clients
            ClientDS clients = null;
            try {
                string filter = "DivisionNumber='01'";
                if(activeOnly) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "Status = 'A'";
                }
                clients = new ClientDS();
                DataSet ds = FillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    ClientDS _clients = new ClientDS();
                    _clients.Merge(ds);
                    clients.Merge(_clients.ClientTable.Select(filter,"ClientName ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public ClientDS GetClients() { return GetClients(null,null,false); }
        public ClientDS GetClients(string number, string division, bool activeOnly) {
            //Get a list of clients filtered for a specific division
            ClientDS clients = null;
            try {
                string filter = "";
                if(number != null && number.Length > 0) filter = "ClientNumber='" + number + "'";
                if(division != null && division.Length > 0) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "DivisionNumber='" + division + "'";
                }
                if(activeOnly) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "Status = 'A'";
                }
                clients = new ClientDS();
                DataSet ds = FillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    ClientDS _clients = new ClientDS();
                    _clients.Merge(ds);
                    clients.Merge(_clients.ClientTable.Select(filter,"ClientNumber ASC, DivisionNumber ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public ClientDS GetSecureClients(bool activeOnly) {
            //Load a list of client selections
            ClientDS clients = null;
            try {
                clients = new ClientDS();
                bool isAdmin=false;
                ProfileCommon profile=null;
                string clientVendorID = "000";
                MembershipUser user = Membership.GetUser();
                if(user != null) {
                    //Internal\external member logged in
                    isAdmin = Roles.IsUserInRole(user.UserName,"administrators");
                    profile = new ProfileCommon().GetProfile(user.UserName);
                    if(profile != null) clientVendorID =  profile.ClientVendorID;
                }
                if(isAdmin || clientVendorID == "000") {
                    //Internal user- get list of all clients
                    clients.ClientTable.AddClientTableRow("","","All","","");
                    ClientDS _clients = GetClients(null,"01",activeOnly);
                    clients.Merge(_clients.ClientTable.Select("","ClientName ASC"));
                }
                else {
                    //Client- list this client only; Vendor: list all of it's clients
                    if(profile.Type.ToLower() == "vendor")
                        clients.Merge(GetClientsForVendor(profile.ClientVendorID));
                    else
                        clients.ClientTable.AddClientTableRow(profile.ClientVendorID,"",profile.Company,"","");
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public ClientDS GetClientsForVendor(string vendorID) {
            //
            ClientDS clients = new ClientDS();
            DataSet ds = FillDataset("[uspRptClientGetListForVendor]",TBL_CLIENTS,new object[] { vendorID });
            if(ds.Tables[TBL_CLIENTS].Rows.Count > 0)
                clients.Merge(ds.Tables[TBL_CLIENTS].Select("","ClientName ASC"));
            return clients;
        }
        public DataSet GetClientDivisions(string number) {
            //Get a list of client divisions
            DataSet divisions = null;
            try {
                divisions = FillDataset(USP_CLIENTDIVS,TBL_CLIENTDIVS,new object[] { number });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client division list.",ex); }
            return divisions;
        }
        public DataSet GetClientTerminals(string number) {
            //Get a list of client terminals
            DataSet terminals = null;
            try {
                terminals = new DataSet();
                DataSet ds = FillDataset(USP_CLIENTTERMINALS,TBL_CLIENTTERMINALS,new object[] { number });
                terminals.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client terminal list.",ex); }
            return terminals;
        }
        public DataSet GetClientRegions(string number) {
            //Get a list of client divisions
            DataSet regions = null;
            try {
                regions = new DataSet();
                DataSet ds = FillDataset(USP_REGIONS_DISTRICTS,TBL_REGIONS,new object[] { number });
                Hashtable table = new Hashtable();
                for(int i = 0;i < ds.Tables[TBL_REGIONS].Rows.Count;i++) {
                    string region = ds.Tables[TBL_REGIONS].Rows[i]["Region"].ToString().Trim();
                    if(region.Length == 0)
                        ds.Tables[TBL_REGIONS].Rows[i].Delete();
                    else {
                        if(table.ContainsKey(region))
                            ds.Tables[TBL_REGIONS].Rows[i].Delete();
                        else {
                            table.Add(region,ds.Tables[TBL_REGIONS].Rows[i]["RegionName"].ToString().Trim());
                            ds.Tables[TBL_REGIONS].Rows[i]["Region"] = region;
                            ds.Tables[TBL_REGIONS].Rows[i]["RegionName"] = ds.Tables[TBL_REGIONS].Rows[i]["RegionName"].ToString().Trim();
                        }
                    }
                }
                regions.Merge(ds,true);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client regions list.",ex); }
            return regions;
        }
        public DataSet GetClientDistricts(string number) {
            //Get a list of client divisions
            DataSet districts = null;
            try {
                districts = FillDataset(USP_REGIONS_DISTRICTS,TBL_DISTRICTS,new object[] { number });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client districts list.",ex); }
            return districts;
        }
        public DataSet GetConsumerDirectCustomers() {
            //Get a list of consumer direct customers
            DataSet customers = null;
            try {
                customers = new DataSet();
                DataSet ds = FillDataset(USP_CONSUMERDIRECTCUSTOMERS,TBL_CONSUMERDIRECTCUSTOMERS,new object[] { });
                if(ds.Tables[TBL_CONSUMERDIRECTCUSTOMERS] != null && ds.Tables[TBL_CONSUMERDIRECTCUSTOMERS].Rows.Count > 0) {
                    customers.Merge(ds);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating customers list.",ex); }
            return customers;
        }
        public VendorDS GetVendorsList(string clientNumber,string clientTerminal) {
            //Get a list of vendors
            VendorDS vendors = null;
            try {
                vendors = new VendorDS();
                DataSet ds = FillDataset(USP_VENDORS,TBL_VENDORS,new object[] { clientNumber,clientTerminal });
                if(ds.Tables[TBL_VENDORS] != null && ds.Tables[TBL_VENDORS].Rows.Count > 0) {
                    VendorDS _vendors = new VendorDS();
                    _vendors.Merge(ds);
                    vendors.Merge(_vendors.VendorTable.Select("","VendorName ASC"));
                    vendors.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return vendors;
        }
        public VendorDS GetVendors(string clientNumber,string clientTerminal) {
            //Get a list of vendors
            VendorDS vendors = null;
            try {
                vendors = new VendorDS();
                DataSet ds = FillDataset(USP_VENDORS,TBL_VENDORS,new object[] { clientNumber,clientTerminal });
                if(ds.Tables[TBL_VENDORS] != null && ds.Tables[TBL_VENDORS].Rows.Count > 0) {
                    VendorDS _vendors = new VendorDS();
                    _vendors.Merge(ds);
                    for(int i = 0;i < _vendors.VendorTable.Rows.Count;i++) {
                        _vendors.VendorTable.Rows[i]["VendorSummary"] = (!_vendors.VendorTable.Rows[i].IsNull("VendorNumber") ? _vendors.VendorTable.Rows[i]["VendorNumber"].ToString() : "     ") + " - " +
                                                                          (!_vendors.VendorTable.Rows[i].IsNull("VendorName") ? _vendors.VendorTable.Rows[i]["VendorName"].ToString().Trim() : "");
                    }
                    vendors.Merge(_vendors.VendorTable.Select("","VendorNumber ASC"));
                    vendors.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return vendors;
        }
        public VendorDS GetParentVendors(string clientNumber,string clientTerminal) {
            //Get a list of parent vendors
            System.Diagnostics.Debug.WriteLine("GetParentVendors");
            VendorDS vendors = null;
            try {
                vendors = new VendorDS();
                VendorDS ds = GetVendors(clientNumber,clientTerminal);
                if(clientNumber != null && ds.VendorTable.Rows.Count > 0) {
                    vendors.Merge(ds.VendorTable.Select("VendorParentNumber = ''"));
                    vendors.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return vendors;
        }
        public VendorDS GetVendorLocations(string clientNumber,string clientTerminal,string vendorNumber) {
            //Get a list of vendor locations (child vendors) for the specified client-vendor
            System.Diagnostics.Debug.WriteLine("GetVendorLocations");
            VendorDS locs=null;
            try {
                locs = new VendorDS();
                VendorDS ds = GetVendors(clientNumber,clientTerminal);
                if(clientNumber != null && vendorNumber != null && ds.VendorTable.Rows.Count > 0) 
                    locs.Merge(ds.VendorTable.Select("VendorParentNumber = '" + vendorNumber + "'"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return locs;
        }
        public AgentDS GetAgents(bool mainZoneOnly) {
            //Get a list of agents
            AgentDS agents = null;
            try {
                agents = new AgentDS();
                DataSet ds = FillDataset(USP_AGENTS,TBL_AGENTS,new object[] { });
                if(ds.Tables[TBL_AGENTS].Rows.Count != 0) {
                    AgentDS _ds = new AgentDS();
                    if(mainZoneOnly) {
                        AgentDS __ds = new AgentDS();
                        __ds.Merge(ds);
                        _ds.Merge(__ds.AgentTable.Select("MainZone <> ''"));
                    }
                    else
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
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating agent list.",ex); }
            return agents;
        }
        public AgentDS GetParentAgents() {
            //Get a list of parent agent
            System.Diagnostics.Debug.WriteLine("GetParentAgents");
            AgentDS agents = null;
            try {
                agents = new AgentDS();
                AgentDS ds = GetAgents(false);
                if(ds.AgentTable.Rows.Count > 0)
                    agents.Merge(ds.AgentTable.Select("AgentParentNumber = ''"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating subagent list.",ex); }
            return agents;
        }
        public AgentDS GetAgentLocations(string agent) {
            //Get a list of agents
            System.Diagnostics.Debug.WriteLine("GetAgentLocations");
            AgentDS subagents = null;
            try {
                subagents = new AgentDS();
                AgentDS ds = GetAgents(false);
                if(agent != null && ds.AgentTable.Rows.Count > 0) 
                    subagents.Merge(ds.AgentTable.Select("AgentParentNumber = '" + agent + "'"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating subagent list.",ex); }
            return subagents;
        }

        public ShipperDS GetShippers(FreightType freightType,string clientNumber,string clientTerminal) {
            //Get a list of shippers
            ShipperDS shippers = null;
            try {
                shippers = new ShipperDS();
                if(freightType == FreightType.Regular) {
                    VendorDS vendors = GetVendors(clientNumber,clientTerminal);
                    shippers = new ShipperDS();
                    for(int i = 0;i < vendors.VendorTable.Rows.Count;i++)
                        shippers.ShipperTable.AddShipperTableRow(vendors.VendorTable[i].VendorNumber,vendors.VendorTable[i].VendorName,vendors.VendorTable[i].VendorParentNumber,vendors.VendorTable[i].VendorSummary);
                    shippers.AcceptChanges();
                }
                else if(freightType == FreightType.Returns) {
                    AgentDS agents = GetAgents(false);
                    ShipperDS _shippers = new ShipperDS();
                    for(int i = 0;i < agents.AgentTable.Rows.Count;i++)
                        _shippers.ShipperTable.AddShipperTableRow(agents.AgentTable[i].AgentNumber,agents.AgentTable[i].AgentName,agents.AgentTable[i].AgentParentNumber,agents.AgentTable[i].AgentSummary);
                    shippers.Merge(_shippers.ShipperTable.Select("","ShipperNumber ASC")); 
                    shippers.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating shipper list.",ex); }
            return shippers;
        }
        public ZoneDS GetZones() {
            //Get a list of zones
            ZoneDS zones = null;
            try {
                zones = new ZoneDS();
                DataSet ds = FillDataset(USP_ZONES,TBL_ZONES,new object[] { });
                if(ds.Tables[TBL_ZONES].Rows.Count != 0)
                    zones.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating zone list.",ex); }
            return zones;
        }

        public PickupDS GetPickups(string client,string division,DateTime startDate,DateTime endDate,string vendor) {
            //Get a list of pickups
            PickupDS pickups = null;
            try {
                pickups = new PickupDS();
                DataSet ds = FillDataset(USP_PICKUPS,TBL_PICKUPS,new object[] { client,division,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd"),vendor });
                if(ds.Tables[TBL_PICKUPS].Rows.Count != 0)
                    pickups.Merge(ds);
                for(int i=0;i<pickups.PickupViewTable.Rows.Count;i++) {
                    pickups.PickupViewTable[i].ManifestNumbers = pickups.PickupViewTable[i].ManifestNumbers.Replace(",",", ");
                    pickups.PickupViewTable[i].TrailerNumbers = pickups.PickupViewTable[i].TrailerNumbers.Replace(",",", ");
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating pickup list.",ex); }
            return pickups;
        }
        public DataSet GetDeliveryExceptions() {
            //Get a list of delivery exceptions
            DataSet exceptions = null;
            try {
                exceptions = new DataSet();
                DataSet ds = FillDataset(USP_EXCEPTIONS,TBL_EXCEPTIONS,new object[] { });
                if(ds.Tables[TBL_EXCEPTIONS].Rows.Count != 0)
                    exceptions.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating delivery exceptions list.",ex); }
            return exceptions;
        }
        public IndirectTripDS GetIndirectTrips(string terminal,int daysBack) {
            //Get a list of indirect trips
            IndirectTripDS trips = null;
            try {
                trips = new IndirectTripDS();
                DateTime startDate = DateTime.Today.AddDays(-daysBack);
                DateTime endDate = DateTime.Today;
                DataSet ds = FillDataset(USP_INDIRECTTRIPS,TBL_INDIRECTTRIPS,new object[] { terminal,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_INDIRECTTRIPS] != null && ds.Tables[TBL_INDIRECTTRIPS].Rows.Count != 0)
                    trips.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating indirect trips list.",ex); }
            return trips;
        }
        public DataSet GetTLs(string terminal,int daysBack) {
            //Event handler for change in selected terminal
            DataSet tls = null;
            try {
                tls = new DataSet();
                DateTime startDate = DateTime.Today.AddDays(-daysBack);
                DateTime endDate = DateTime.Today;
                DataSet ds = FillDataset(USP_TLS,TBL_TLS,new object[] { terminal,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_TLS].Rows.Count > 0)
                    tls.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating TL list.",ex); }
            return tls;
        }
        public ShiftDS GetShifts(string terminal,DateTime date) {
            //Event handler for change in selected terminal
            ShiftDS shifts = null;
            try {
                shifts = new ShiftDS();
                DataSet ds = FillDataset(USP_SHIFTS,TBL_SHIFTS,new object[] { terminal,date.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_SHIFTS] != null && ds.Tables[TBL_SHIFTS].Rows.Count > 0)
                    shifts.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating shift list.",ex); }
            return shifts;
        }
        public VendorLogDS GetVendorLog(string client,string clientDivision,DateTime startDate,DateTime endDate) {
            //Event handler for change in selected terminal
            VendorLogDS log = null;
            try {
                log = new VendorLogDS();
                DataSet ds = FillDataset(USP_VENDORLOG,TBL_VENDORLOG,new object[] { client,clientDivision,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_VENDORLOG].Rows.Count != 0)
                    log.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating Vendor Log list.",ex); }
            return log;
        }

        public DataSet GetRetailDates(string scope) {
            //Create a list of retail dates
            DataSet ds = new DataSet();
            try {
                ds.Tables.Add("DateRangeTable");
                ds.Tables["DateRangeTable"].Columns.Add("Year",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Quarter",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Month",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Name");
                ds.Tables["DateRangeTable"].Columns.Add("Week",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("StartDate",Type.GetType("System.DateTime"));
                ds.Tables["DateRangeTable"].Columns.Add("EndDate",Type.GetType("System.DateTime"));
                ds.Tables["DateRangeTable"].Columns.Add("Value");
                DataSet _ds = null;
                string field = "";
                switch(scope.ToLower()) {
                    case "week":
                        field = "Week";
                        _ds = FillDataset("uspRptRetailCalendarWeekGetList","DateRangeTable",new object[] { });
                        break;
                    case "month":
                        field = "Month";
                        _ds = FillDataset("uspRptRetailCalendarMonthGetList","DateRangeTable",new object[] { });
                        break;
                    case "quarter":
                        field = "Quarter";
                        _ds = FillDataset("uspRptRetailCalendarQuarterGetList","DateRangeTable",new object[] { });
                        break;
                    case "ytd":
                        field = "Year";
                        _ds = FillDataset("uspRptRetailCalendarYearGetList","DateRangeTable",new object[] { });
                        break;
                }
                for(int i = _ds.Tables["DateRangeTable"].Rows.Count;i > 0;i--)
                    ds.Tables["DateRangeTable"].ImportRow(_ds.Tables["DateRangeTable"].Rows[i - 1]);
                for(int i = 0;i < ds.Tables["DateRangeTable"].Rows.Count;i++) {
                    string val = ds.Tables["DateRangeTable"].Rows[i][field].ToString().Trim();
                    string start = ((DateTime)ds.Tables["DateRangeTable"].Rows[i]["StartDate"]).ToString("yyyy-MM-dd");
                    string end = ((DateTime)ds.Tables["DateRangeTable"].Rows[i]["EndDate"]).ToString("yyyy-MM-dd");
                    ds.Tables["DateRangeTable"].Rows[i]["Value"] = val + ", " + start + " : " + end + "";
                }
                ds.AcceptChanges();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating retail dates.",ex); }
            return ds;
        }
        public DataSet GetSortDates() {
            //Create a list of sort dates
            DataSet ds = new DataSet();
            try {
                ds.Tables.Add("DateRangeTable");
                ds.Tables["DateRangeTable"].Columns.Add("Value");
                int d = (int)DateTime.Today.DayOfWeek;
                DateTime _end = DateTime.Today.AddDays(-d);
                for(int i=-1;i<52;i++) {
                    DateTime end = _end.AddDays(-7 * i);
                    DateTime start = end.AddDays(-6);
                    ds.Tables["DateRangeTable"].Rows.Add(new object[] { start.ToString("yyyy-MM-dd") + " : " + end.ToString("yyyy-MM-dd") });
                }
                ds.AcceptChanges();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating sort dates.",ex); }
            return ds;
        }
        public DamageDS GetDamageCodes() {
            //Get a list of Argix damage codes
            DamageDS codes = null;
            try {
                codes = new DamageDS();
                codes.DamageDetailTable.AddDamageDetailTableRow("0",DAMAGEDESCRIPTON_ALL);
                codes.DamageDetailTable.AddDamageDetailTableRow("00",DAMAGEDESCRIPTON_ALL_EXCEPT_NC);
                DataSet ds = FillDataset(USP_DAMAGECODES,TBL_DAMAGECODES,new object[] { });
                if(ds.Tables[TBL_DAMAGECODES].Rows.Count != 0)
                    codes.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating damage codes list.",ex); }
            return codes;
        }
        public LabelStationDS GetCartons(string cartonNumber,string terminalCode,string clientNumber) {
            //Get all cartons that have the specified carton number
            LabelStationDS cartons = null;
            try {
                cartons = new LabelStationDS();
                if(cartonNumber != null) {
                    DataSet ds = FillDataset(USP_LABELSEQNUMBERS,TBL_LABELSEQNUMBERS,new object[] { cartonNumber,terminalCode,clientNumber });
                    if(ds.Tables[TBL_LABELSEQNUMBERS].Rows.Count > 0)
                        cartons.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating list of cartons.",ex); }
            return cartons;
        }

        public DataSet FillDataset(string sp,string table,object[] o) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(sp,o);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
    }
}