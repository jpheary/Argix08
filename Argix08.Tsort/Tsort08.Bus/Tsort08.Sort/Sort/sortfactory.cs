//	File:	sortfactory.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Creates Sort-related objects.
//	Rev:	07/14/08 (jph)- modified CreateBrain() to return ManifestXStoreBrain
//							and MultiManifestXStoreBrain sort type objects.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;
using Tsort.Enterprise;
using Tsort.Freight;
using Tsort.Labels;

namespace Tsort.Sort {
    //
    internal class SortFactory {
        //Members
        public static Mediator Mediator=null;
        private static Hashtable SpecialAgentsCached=null;
        private static Hashtable SpecialAgentsByZoneCached=null;
        private static SpecialAgent SpecialAgentDefault=null;
        private static string LabelType="";
        private static OBLabelTemplate LabelTemplateCached=null;

        //Constants		
        private const string USP_STATION_CONFIG = "uspSortWorkstationGet",TBL_STATION_CONFIG = "WorkstationDetailTable";
        private const string USP_FREIGHTCLIENTSHIPPER = "uspSortFreightClientShipperGetListForStation",TBL_FREIGHTCLIENTSHIPPER = "FreightClientShipperTable";
        private const string USP_DEST_ROUTING_REG = "uspSortRegularDestinationRoutingGet",TBL_DESTINATION_ROUTING = "DestinationRoutingTable";
        private const string USP_DEST_ROUTING_SAN = "uspSortSanDestinationRoutingGet";
        private const string USP_DEST_ROUTING_MANIFESTX = "uspSortDestinationRoutingManifestXStoreGet";
        private const string USP_DEST_ROUTING_MULTIMANIFESTX = "uspSortDestinationRoutingMultiManifestXStoreGet";
        private const string USP_SPECIALAGENT_DETAIL = "uspSortSpecialAgentGetList",TBL_SPECIALAGENT_DETAIL = "SpecialAgentTable";
        private const string USP_OBLABEL_DETAIL = "uspSortOutboundLabelGet",TBL_OBLABEL_DETAIL = "LabelDetailTable";
        public const string USP_CARTON_NEW = "uspSortSortedItemNew";

        //Interface
        static SortFactory() { }
        private SortFactory() { }
        public static void RefreshCache() {
            //Refresh cached data
            try {
                //Get special agents from data store and cache results
                DataSet ds = Mediator.FillDataset(USP_SPECIALAGENT_DETAIL,TBL_SPECIALAGENT_DETAIL,null);
                if(ds == null)
                    throw new ApplicationException("Failed to cache special agents.");
                else {
                    //Cache special agents
                    SpecialAgentsCached = new Hashtable();
                    SpecialAgentsByZoneCached = new Hashtable();
                    SpecialAgentDS specialAgentDS = new SpecialAgentDS();
                    specialAgentDS.Merge(ds);
                    for(int i=0;i<specialAgentDS.SpecialAgentTable.Rows.Count;i++) {
                        SpecialAgent agent=null;
                        if(specialAgentDS.SpecialAgentTable[i].ID == "0000")
                            agent = SpecialAgentDefault = new SpecialAgent(specialAgentDS.SpecialAgentTable[i]);
                        else if(specialAgentDS.SpecialAgentTable[i].ID == "0001")
                            agent = new StatSampleSpecialAgent(specialAgentDS.SpecialAgentTable[i]);
                        else if(specialAgentDS.SpecialAgentTable[i].ID.Substring(0,2) == "04")
                            agent = new UPSSpecialAgent(specialAgentDS.SpecialAgentTable[i]);
                        else
                            agent = new SpecialAgent(specialAgentDS.SpecialAgentTable[i]);
                        SpecialAgentsCached.Add(agent.CLIENT_NUMBER.Trim() + agent.CLIENT_DIVISION.Trim() + agent.SAN_ID.Trim(),agent);
                        SpecialAgentsByZoneCached.Add(agent.CLIENT_NUMBER.Trim() + agent.CLIENT_DIVISION.Trim() + agent.ZONE_CODE.Trim(),agent);
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while caching SortFactory data.",ex); }
        }
        public static Workstation CreateWorkstation(string machineName) {
            //Create a workstation that has an ILabelPrinter printer and IScale scale
            Workstation station=null;
            try {
                if(machineName.Length > 0) {
                    DataSet ds = Mediator.FillDataset(USP_STATION_CONFIG,TBL_STATION_CONFIG,new object[] { machineName });
                    if(ds.Tables[TBL_STATION_CONFIG].Rows.Count == 0)
                        throw new ApplicationException("Station for  " + machineName + " not found.");
                    else {
                        WorkstationDS stationDS = new WorkstationDS();
                        stationDS.Merge(ds);
                        station = new Workstation(stationDS.WorkstationDetailTable[0]);
                    }
                }
                else {
                    station = new Workstation(null);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating workstation.",ex); }
            return station;
        }
        public static StationAssignments GetStationAssignments(Workstation workstation) {
            //Return a collection of current freight assignments for the specified workstation
            StationAssignments assignments=null;
            try {
                if(workstation == null)
                    throw new ApplicationException("Workstation is null.");

                assignments = new StationAssignments();
                StationAssignmentDS dsAssignments = new StationAssignmentDS();
                dsAssignments.Merge(Mediator.FillDataset(USP_FREIGHTCLIENTSHIPPER,TBL_FREIGHTCLIENTSHIPPER,new object[] { workstation.WorkStationID }));
                foreach(StationAssignmentDS.FreightClientShipperTableRow row in dsAssignments.FreightClientShipperTable) {
                    Client client = EnterpriseFactory.CreateClient(row.ClientNumber,row.ClientDivision,row.Client,row.ClientAddressLine1,row.ClientAddressLine2,row.ClientAddressCity,row.ClientAddressState,row.ClientAddressZip);
                    Shipper shipper = EnterpriseFactory.CreateShipper(row.FreightType,row.ShipperNumber,row.Shipper,row.ShipperAddressLine1,row.ShipperAddressLine2,row.ShipperAddressCity,row.ShipperAddressState,row.ShipperAddressZip,row.ShipperUserData);
                    InboundFreight shipment = FreightFactory.CreateInboundFreight(row.TerminalID,row.FreightID,row.FreightType,row.TDSNumber,row.VendorKey,row.TrailerNumber,row.PickupDate,row.PickupNumber,row.CubeRatio,client,shipper);
                    SortProfile profile = CreateSortProfile(shipment,row.SortTypeID,row.SortType,row.LabelID,row.ExcLocation);
                    assignments.Add(new StationAssignment("",workstation,shipment,profile));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while getting station assignments (freight-client-shipper).",ex); }
            return assignments;
        }
        public static SortProfile CreateSortProfile(InboundFreight shipment,int sortTypeID,string sortType,int labelID,int excLocation) {
            //Create a sort profile for the specified freight based upon its' type (i.e. Tsort, Returns), 
            //the client/shipper relationship, and how it was scheduled by Freight Assign to be sorted (i.e. San, Regular, SKU, etc)
            SortProfile sortProfile=null;
            try {
                //The freight type of the shipment determines whether a regular or returns profile is needed
                //Create a sort profile that specifies freight type, sort type, and inbound label
                SortProfileDS sortProfileDS = new SortProfileDS();
                SortProfileDS.SortProfileTableRow profile = sortProfileDS.SortProfileTable.NewSortProfileTableRow();
                sortProfileDS.EnforceConstraints = false;
                profile.FreightType = shipment.FreightType;
                profile.SortTypeID = sortTypeID;
                profile.SortType = sortType;
                profile.ClientNumber = shipment.Client.Number;
                profile.ClientDivision = shipment.Client.Division;
                profile.VendorNumber = shipment.Shipper.NUMBER;
                profile.Status = "";
                profile.LabelID = labelID;
                profile.ExceptionLocation = excLocation;
                sortProfileDS.EnforceConstraints = true;
                sortProfile = new SortProfile(profile);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating sort profile.",ex); }
            return sortProfile;
        }
        public static Brain CreateBrain(SortProfile sortProfile) {
            //Create a station operator brain for the given sort type
            Brain brain=null;
            if(sortProfile == null)
                brain = new EmptyBrain();
            else {
                switch(sortProfile.SortTypeID) {
                    case 1: brain = new SANBrain(); break;
                    case 2: brain = new RegularBrain(); break;
                    case 14: brain = new ManifestXStoreBrain(); break;
                    case 18: brain = new MultiManifestXStoreBrain(); break;
                    default: brain = new EmptyBrain(); break;
                }
            }
            return brain;
        }
        public static DestinationRouting CreateDestinationRouting(string clientNumber,string clientDivision,string vendorNumber,string terminalLocation,int destinationNumber,string zoneCode,string cartonNumber,bool checkForDuplicates) {
            //
            DestinationRouting destinationRouting=null;
            try {
                //The freight type of the shipment determines whether a regular or returns profile is needed
                DataSet ds = Mediator.FillDataset(USP_DEST_ROUTING_REG,TBL_DESTINATION_ROUTING,new object[] { clientNumber,clientDivision,vendorNumber,terminalLocation,destinationNumber,cartonNumber,(checkForDuplicates?1:0),zoneCode });
                if(ds != null && ds.Tables[TBL_DESTINATION_ROUTING].Rows.Count > 0) {
                    //Create a sort profile that specifies freight type, sort type, and inbound label
                    DestinationRoutingDS destinationRoutingDS = new DestinationRoutingDS();
                    destinationRoutingDS.Merge(ds);
                    destinationRouting = new DestinationRouting(destinationRoutingDS.DestinationRoutingTable[0]);
                }
                else
                    throw new ApplicationException("Destination & routing information not found for destination# (i.e. store#) " + destinationNumber.ToString() +  ".");
            }
            catch(DataAccessException ex) { if(ex.InnerException != null) throw ex.InnerException; else throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating destination and routing.",ex); }
            return destinationRouting;
        }
        public static DestinationRouting CreateSANDestinationRouting(string clientNumber,string clientDivision,string vendorNumber,string terminalLocation,string sanNumber,string zoneCode,string cartonNumber,bool checkForDuplicates) {
            //
            DestinationRouting destinationRouting=null;
            try {
                //The freight type of the shipment determines whether a regular or returns profile is needed
                DataSet ds = Mediator.FillDataset(USP_DEST_ROUTING_SAN,TBL_DESTINATION_ROUTING,new object[] { clientNumber,clientDivision,vendorNumber,terminalLocation,sanNumber,cartonNumber,(checkForDuplicates?1:0),zoneCode });
                if(ds != null && ds.Tables[TBL_DESTINATION_ROUTING].Rows.Count > 0) {
                    //Create a sort profile that specifies freight type, sort type, and inbound label
                    DestinationRoutingDS destinationRoutingDS = new DestinationRoutingDS();
                    destinationRoutingDS.Merge(ds);
                    destinationRouting = new DestinationRouting(destinationRoutingDS.DestinationRoutingTable[0]);
                }
                else
                    throw new ApplicationException("Destination & routing information not found for SAN#" + sanNumber +  ".");
            }
            catch(DataAccessException ex) { if(ex.InnerException != null) throw ex.InnerException; else throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating destination and routing.",ex); }
            return destinationRouting;
        }
        public static DestinationRouting CreateDestinationRoutingManifestX(string clientNumber,string clientDivision,string vendorNumber,string terminalLocation,string TDSID,string inputString,string zoneCode,string cartonNumber,bool checkForDuplicates) {
            //
            DestinationRouting destinationRouting=null;
            try {
                //The freight type of the shipment determines whether a regular or returns profile is needed
                DataSet ds = Mediator.FillDataset(USP_DEST_ROUTING_MANIFESTX,TBL_DESTINATION_ROUTING,new object[] { clientNumber,clientDivision,vendorNumber,terminalLocation,TDSID,inputString,cartonNumber,(checkForDuplicates?1:0),zoneCode });
                if(ds != null && ds.Tables[TBL_DESTINATION_ROUTING].Rows.Count > 0) {
                    //Create a sort profile that specifies freight type, sort type, and inbound label
                    DestinationRoutingDS destinationRoutingDS = new DestinationRoutingDS();
                    destinationRoutingDS.Merge(ds);
                    destinationRouting = new DestinationRouting(destinationRoutingDS.DestinationRoutingTable[0]);
                }
                else
                    throw new ApplicationException("Destination & routing information not found for input string" + inputString +  ".");
            }
            catch(DataAccessException ex) { if(ex.InnerException != null) throw ex.InnerException; else throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating destination and routing.",ex); }
            return destinationRouting;
        }
        public static DestinationRouting CreateDestinationRoutingMultiManifestX(string clientNumber,string clientDivision,string vendorNumber,string terminalLocation,string TDSID,string inputString,string zoneCode,string cartonNumber,bool checkForDuplicates) {
            //
            DestinationRouting destinationRouting=null;
            try {
                //The freight type of the shipment determines whether a regular or returns profile is needed
                DataSet ds = Mediator.FillDataset(USP_DEST_ROUTING_MULTIMANIFESTX,TBL_DESTINATION_ROUTING,new object[] { clientNumber,clientDivision,vendorNumber,terminalLocation,TDSID,inputString,cartonNumber,(checkForDuplicates?1:0),zoneCode });
                if(ds != null && ds.Tables[TBL_DESTINATION_ROUTING].Rows.Count > 0) {
                    //Create a sort profile that specifies freight type, sort type, and inbound label
                    DestinationRoutingDS destinationRoutingDS = new DestinationRoutingDS();
                    destinationRoutingDS.Merge(ds);
                    destinationRouting = new DestinationRouting(destinationRoutingDS.DestinationRoutingTable[0]);
                }
                else
                    throw new ApplicationException("Destination & routing information not found for input string" + inputString +  ".");
            }
            catch(DataAccessException ex) { if(ex.InnerException != null) throw ex.InnerException; else throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating destination and routing.",ex); }
            return destinationRouting;
        }
        public static SpecialAgent CreateSpecialAgent(string clientNumber,string clientDivision,string shipOverride) {
            //Create a special agent
            SpecialAgent specialAgent=null;
            try {
                //Use cached data
                specialAgent = (SpecialAgent)SpecialAgentsCached[clientNumber.Trim() + clientDivision.Trim() + shipOverride.Trim()];
                if(specialAgent == null) specialAgent = SpecialAgentDefault;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating special agent.",ex); }
            return specialAgent;
        }
        public static SpecialAgent CreateSpecialAgentByZone(string clientNumber,string clientDivision,string zoneCode) {
            //Create a special agent by zone
            SpecialAgent specialAgent=null;
            try {
                //Use cached data
                specialAgent = (SpecialAgent)SpecialAgentsByZoneCached[clientNumber.Trim() + clientDivision.Trim() + zoneCode.Trim()];
                if(specialAgent == null) specialAgent = SpecialAgentDefault;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating special agent by zone.",ex); }
            return specialAgent;
        }
        public static LabelTemplate CreateOBLabelTemplate(string labelType,string printerType) {
            //Create an outbound label
            OBLabelTemplate labelTemplate=null;
            try {
                if(LabelTemplateCached != null && labelType == LabelType)
                    labelTemplate = LabelTemplateCached;
                else {
                    DataSet ds = Mediator.FillDataset(USP_OBLABEL_DETAIL,TBL_OBLABEL_DETAIL,new object[] { labelType,printerType });
                    if(ds.Tables[TBL_OBLABEL_DETAIL].Rows.Count == 0)
                        throw new ApplicationException("Outbound label " + labelType + " (" + printerType + ")" + " not found.");
                    else {
                        LabelDS labelDS = new LabelDS();
                        labelDS.Merge(ds);
                        labelTemplate = new OBLabelTemplate(labelDS.LabelDetailTable[0]);
                    }
                    LabelType = labelType;
                    LabelTemplateCached = labelTemplate;
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating outbound label.",ex); }
            return labelTemplate;
        }
        public static bool SaveSortedItem(SortedItem item) {
            //Save this sorted item to database
            bool result=false;
            try {
                //Save to database
                if(item.Freight.IsReturns) {
                    result = Mediator.ExecuteNonQuery(USP_CARTON_NEW,new object[]{	item.LabelSeqNumber,
																					  item.Freight.Client.Number,item.Freight.Client.Division,
																					  item.Freight.Shipper.NUMBER,item.DestinationRouting.DestinationNumber,
																					  item.Freight.TerminalID.ToString().PadLeft(2, '0'),item.DamageCode,
																					  item.Freight.PickupDate,item.Freight.PickupNumber,
																					  item.DestinationRouting.DestinationNumber, item.DestinationRouting.ZoneCode,item.DestinationRouting.ZoneTL,
																					  item.ItemType,item.Weight,
																					  item.Freight.VendorKey,item.CartonNumber,
																					  "Y",item.ReturnNumber,
																					  item.DestinationRouting.ShiftNumber,item.DestinationRouting.ShiftDate,
																					  DateTime.Now,null,
																					  item.SortStation.Number,item.Cube,DateTime.Today,
																					  item.SANNUmber,
																					  item.ElapsedSeconds,item.DownSeconds,
																					  item.PONumber,item.TrackingNumber,item.SpecialAgent.ID,
																					  null,item.InboundLabel.Inputs[0].InputData,item.SortProfile.SortTypeID,
																					  Environment.UserName,item.Freight.FreightID,false});
                }
                else {
                    result = Mediator.ExecuteNonQuery(USP_CARTON_NEW,new object[]{	item.LabelSeqNumber,
																					  item.Freight.Client.Number,item.Freight.Client.Division,
																					  "9999",item.Freight.Shipper.NUMBER,
																					  item.Freight.TerminalID.ToString().PadLeft(2, '0'),item.DamageCode,
																					  item.Freight.PickupDate,item.Freight.PickupNumber,
																					  item.DestinationRouting.DestinationNumber, item.DestinationRouting.ZoneCode,item.DestinationRouting.ZoneTL,
																					  item.ItemType,item.Weight,
																					  item.Freight.VendorKey,item.CartonNumber,
																					  "N","",
																					  item.DestinationRouting.ShiftNumber,item.DestinationRouting.ShiftDate,
																					  DateTime.Now,null,
																					  item.SortStation.Number,item.Cube,DateTime.Today,
																					  item.SANNUmber,
																					  item.ElapsedSeconds,item.DownSeconds,
																					  item.PONumber,item.TrackingNumber,item.SpecialAgent.ID,
																					  null,item.InboundLabel.Inputs[0].InputData,item.SortProfile.SortTypeID,
																					  Environment.UserName,item.Freight.FreightID,false});
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while saving sorted item to database.",ex); }
            return result;
        }
    }
}
