using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix {
    //
    [ServiceContract(Namespace="http://Argix")]
    public interface IEnterpriseService {
        [OperationContract]
        TerminalDS GetArgixTerminals();
        [OperationContract]
        TerminalDS GetTerminals();
        [OperationContract]
        ClientDS GetClientsList(bool activeOnly);
        [OperationContract]
        ClientDS GetClients();
        [OperationContract(Name="GetFilteredClients")]
        ClientDS GetClients(string number,string division,bool activeOnly);
        [OperationContract]
        ClientDS GetClientsForVendor(string vendorID);
        [OperationContract]
        DataSet GetClientDivisions(string number);
        [OperationContract]
        DataSet GetClientTerminals(string number);
        [OperationContract]
        DataSet GetClientRegions(string number);
        [OperationContract]
        DataSet GetClientDistricts(string number);
        [OperationContract]
        DataSet GetConsumerDirectCustomers();
        [OperationContract]
        VendorDS GetVendorsList(string clientNumber,string clientTerminal);
        [OperationContract]
        VendorDS GetVendors(string clientNumber,string clientTerminal);
        [OperationContract]
        VendorDS GetParentVendors(string clientNumber,string clientTerminal);
        [OperationContract]
        VendorDS GetVendorLocations(string clientNumber,string clientTerminal,string vendorNumber);
        [OperationContract]
        AgentDS GetAgents(bool mainZoneOnly);
        [OperationContract]
        AgentDS GetParentAgents();
        [OperationContract]
        AgentDS GetAgentLocations(string agent);
        [OperationContract]
        ShipperDS GetShippers(FreightType freightType,string clientNumber,string clientTerminal);
        [OperationContract]
        ZoneDS GetZones();

        [OperationContract]
        PickupDS GetPickups(string client,string division,DateTime startDate,DateTime endDate,string vendor);
        [OperationContract]
        DataSet GetDeliveryExceptions();
        [OperationContract]
        IndirectTripDS GetIndirectTrips(string terminal,int daysBack);
        [OperationContract]
        DataSet GetTLs(string terminal,int daysBack);
        [OperationContract]
        ShiftDS GetShifts(string terminal,DateTime date);
        [OperationContract]
        VendorLogDS GetVendorLog(string client,string clientDivision,DateTime startDate,DateTime endDate);

        [OperationContract]
        DataSet GetRetailDates(string scope);
        [OperationContract]
        DataSet GetSortDates();
        [OperationContract]
        DamageDS GetDamageCodes();
        [OperationContract]
        LabelStationDS GetCartons(string cartonNumber,string terminalCode,string clientNumber);
    }
}