using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.Freight {
	//
	public class FreightFactory {
		//Members
        private static InboundFreightDS _InboundFreight = null;
        private static FreightAssignDS _Assignments = null;
        private static FreightAssignDS _AssignmentHistory = null;
        private static int _SortedRange = 1;

        private const string USP_LOCALTERMINAL = "uspFACurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        private const string USP_FREIGHT = "uspFAInboundFreightGetList",TBL_FREIGHT = "InboundFreightTable";
        private const string USP_SHIPMENTREAD = "uspFAShipmentGet",TBL_SHIPMENTREAD = "Shipments";
        private const string USP_SHIPMENTUPDATESTART = "uspFAShipmentUpdateStart";
        private const string USP_SHIPMENTUPDATESTOP = "uspFAShipmentUpdateStop";
        private const string USP_SHIPMENTDELETE = "uspFAShipmentDelete";
        private const string USP_ASSIGNMENTS = "uspFAAssignmentGetList",TBL_ASSIGNMENTS = "StationFreightAssignmentTable";
        private const string USP_ASSIGNMENTCREATE = "uspFAAssignmentNew";
        private const string USP_ASSIGNMENTDELETE = "uspFAAssignmentDelete";
        private const string USP_SORTSTATIONS = "uspFAStationsGetListAssignable",TBL_SORTSTATIONS = "WorkstationTable";
        private const string USP_SORTTYPES = "uspFASortTypesGetList",TBL_SORTTYPES = "SelectionList2Table";

        public static event EventHandler FreightChanged = null;
        public static event EventHandler AssignmentsChanged = null;
        public static event EventHandler AssignmentHistoryChanged = null;

        //Interface
		static FreightFactory() {
            _InboundFreight = new InboundFreightDS();
            _Assignments = new FreightAssignDS();
            _AssignmentHistory = new FreightAssignDS();
        }
		private FreightFactory() { }
        public static InboundFreightDS InboundFreight { get { return _InboundFreight; } }
        public static FreightAssignDS StationAssignments { get { return _Assignments; } }
        public static FreightAssignDS StationAssignmentHistory { get { return _AssignmentHistory; } }
        public static int SortedRange {
            get { return _SortedRange; }
            set { if(value != _SortedRange) { _SortedRange = value; RefreshFreight(); } }
        }
        public static void RefreshFreight() {
            //Refresh the list of inbound shipments for the specified terminal
            DataSet ds = null;
            try {
                _InboundFreight.Clear();
                ds = App.Mediator.FillDataset(USP_FREIGHT,TBL_FREIGHT,new object[] { App.Mediator.TerminalID,DateTime.Today.AddDays(-_SortedRange) });
                if(ds != null)
                    _InboundFreight.Merge(ds,false,MissingSchemaAction.Ignore);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh inbound shipments.",ex); }
            finally { if(FreightChanged != null) FreightChanged(null,EventArgs.Empty); }
        }
        public static IBShipment GetShipment(string freightID) {
            //Return an existing shipment from the inbound freight collection
            IBShipment shipment = null;
            try {
                //Merge from collection (dataset)
                InboundFreightDS.InboundFreightTableRow row = (InboundFreightDS.InboundFreightTableRow)_InboundFreight.InboundFreightTable.Select("FreightID='" + freightID + "'")[0];
                shipment = new IBShipment(row);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to get shipment.",ex); }
            return shipment;
        }
        public static void RefreshStationAssignments() {
            //Refresh the list of freight assignments for the local terminal
            DataSet ds = null;
            try {
                _Assignments.Clear();
                ds = App.Mediator.FillDataset(USP_ASSIGNMENTS,TBL_ASSIGNMENTS,null);
                if(ds != null) _Assignments.Merge(ds,false,MissingSchemaAction.Ignore);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh station assignments.",ex); }
            finally { if(AssignmentsChanged != null) AssignmentsChanged(null,EventArgs.Empty); }
        }
        public static FreightAssignDS GetAssignments(string freightID,string workStationID) {
            //Return a dataset of all assignments from the assignments grid dataset
            FreightAssignDS dsAssignments = null;
            try {
                dsAssignments = new FreightAssignDS();
                FreightAssignDS dsSelectedAssignments = new FreightAssignDS();
                if(workStationID.Trim() == "")
                    dsSelectedAssignments.Merge(_Assignments.StationFreightAssignmentTable.Select("FreightID = '" + freightID + "'"));
                else
                    dsSelectedAssignments.Merge(_Assignments.StationFreightAssignmentTable.Select("FreightID = '" + freightID + "' AND WorkStationID = '" + workStationID + "'"));
                for(int i = 0; i < dsSelectedAssignments.StationFreightAssignmentTable.Rows.Count; i++)
                    dsAssignments.StationFreightAssignmentTable.ImportRow(dsSelectedAssignments.StationFreightAssignmentTable[i]);
            }
            catch(Exception) { }
            return dsAssignments;
        }
        public static StationAssignment CreateAssignment(Workstation workstation,IBShipment freight,int sortTypeID,string initials) {
            //Assign this freight to the specified sort station
            StationAssignment assignment = null;
            try {
                //Create the station assignment
                App.Mediator.ExecuteNonQuery(USP_ASSIGNMENTCREATE,new object[] { workstation.WorkStationID,freight.FreightID,sortTypeID });
                assignment = new StationAssignment(workstation,freight,sortTypeID);
                try { RefreshStationAssignments(); }
                catch { }

                //Add to station assignment history
                _AssignmentHistory.FreightAssignmentHistoryTable.AddFreightAssignmentHistoryTableRow(DateTime.Today,freight.TDSNumber,freight.ClientNumber + "-" + freight.ClientName,workstation.Number,DateTime.Now,initials);
                if(AssignmentHistoryChanged != null) AssignmentHistoryChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to assign freight TDS#" + freight.TDSNumber + " to station " + workstation.Number + " (sorttypeID= " + sortTypeID.ToString() + ").",ex); }
            return assignment;
        }
        public static bool DeleteAssignment(StationAssignment assignment,string initials) {
            //Delete the specified freight assignment
            bool ret = false;
            try {
                //Update local LAN database
                ret = App.Mediator.ExecuteNonQuery(USP_ASSIGNMENTDELETE,new object[] { assignment.SortStation.WorkStationID,assignment.InboundFreight.FreightID });

                //Add to station unassignment history
                _AssignmentHistory.FreightAssignmentHistoryTable.AddFreightAssignmentHistoryTableRow(DateTime.Today,assignment.InboundFreight.TDSNumber,assignment.InboundFreight.ClientName,assignment.SortStation.Number,DateTime.Now,initials);
                if(AssignmentHistoryChanged != null) AssignmentHistoryChanged(null,EventArgs.Empty);

                try { RefreshStationAssignments(); }
                catch { }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to unassign freight " + assignment.InboundFreight.FreightID + " from station " + assignment.SortStation.WorkStationID + " (sorttypeID= " + assignment.SortTypeID.ToString() + ").",ex); }
            return ret;
        }
        public static DataSet GetFreightSortTypes(string freightID) {
            //Get a list of freight sort types for the specified freight
            DataSet ds = null;
            try {
                ds = new DataSet();
                DataSet _ds = App.Mediator.FillDataset(USP_SORTTYPES,TBL_SORTTYPES,new object[] { freightID });
                if(_ds != null) ds.Merge(_ds);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to get freight sort types.",ex); }
            return ds;
        }
        public static WorkstationDS GetAssignableSortStations(string freightID,int sortTypeID) {
            //Get a list of assignable sort stations (stations without assignments) for the 
            //specified freight and sort type; if sortTypeID = SAN, all those stations that 
            //are sorting SAN for a different client are eligible
            WorkstationDS workstations = null;
            try {
                workstations = new WorkstationDS();
                DataSet ds = App.Mediator.FillDataset(USP_SORTSTATIONS,TBL_SORTSTATIONS,new object[] { freightID,App.Mediator.TerminalID,sortTypeID });
                if(ds != null) workstations.Merge(ds,false,MissingSchemaAction.Ignore);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to get assignable sort stations.",ex); }
            return workstations;
        }
        public static bool HasAssignments(string freightID) {
            //Determine if the specified fright has assignments
            FreightAssignDS assignments = new FreightAssignDS();
            assignments.Merge(FreightFactory.StationAssignments.StationFreightAssignmentTable.Select("FreightID = '" + freightID + "'"));
            return (assignments.StationFreightAssignmentTable.Count > 0);
        }
        public static bool IsSortStarted(IBShipment shipment) {
            //Determine if a shipment has started sort
            DataSet ds=null;
            bool ret=false;
            try {
                ds = App.Mediator.FillDataset(USP_SHIPMENTREAD,TBL_SHIPMENTREAD,new object[] { shipment.FreightID });
                if(ds.Tables[TBL_SHIPMENTREAD].Rows.Count > 0) {
                    if(ds.Tables[TBL_SHIPMENTREAD].Rows[0]["StartSortDate"] != null)
                        ret = (ds.Tables[TBL_SHIPMENTREAD].Rows[0]["StartSortDate"].ToString().Trim() != "");
                }
                else
                    throw new ApplicationException("Failed to determine if sort was started for freight ID#" + shipment.FreightID + " because the freight was not found.");
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to determine if sort was started for freight ID#" + shipment.FreightID + ".",ex); }
            return ret;
        }
        public static bool StartSort(IBShipment shipment) {
            //Set start sort date and status for this shipment
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_SHIPMENTUPDATESTART,new object[] { shipment.FreightID,DateTime.Now.ToString("yyyyMMdd"),DateTime.Now.ToString("HHmmss") });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to start sort for freight ID#" + shipment.FreightID + ".",ex); }
            return ret;
        }
        public static bool IsSortStopped(IBShipment shipment) {
            //Determine if a shipment has completed sort
            DataSet ds=null;
            bool ret=false;
            try {
                ds = App.Mediator.FillDataset(USP_SHIPMENTREAD,TBL_SHIPMENTREAD,new object[] { shipment.FreightID });
                if(ds.Tables[TBL_SHIPMENTREAD].Rows.Count > 0) {
                    if(ds.Tables[TBL_SHIPMENTREAD].Rows[0]["StopSortDate"] != null)
                        ret = (ds.Tables[TBL_SHIPMENTREAD].Rows[0]["StopSortDate"].ToString().Trim() != "");
                }
                else
                    throw new ApplicationException("Failed to determine if sort was stopped for freight ID#" + shipment.FreightID + " because the freight was not found.");
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to determine if sort was stopped for freight ID#" + shipment.FreightID + ".",ex); }
            return ret;
        }
        public static bool StopSort(IBShipment shipment) {
            //Set stop sort date and status for this shipment
            bool ret = false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_SHIPMENTUPDATESTOP,new object[] { shipment.FreightID,DateTime.Now.ToString("yyyyMMdd"),DateTime.Now.ToString("HHmmss") });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to stop sort for freight ID#" + shipment.FreightID + ".",ex); }
            return ret;
        }
        public static bool DeleteShipment(IBShipment shipment) {
            //Delete an inbound shipment from the local LAN database
            bool bVal=false;
            try {
                bVal = App.Mediator.ExecuteNonQuery(USP_SHIPMENTDELETE,new object[] { shipment.FreightID });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to delete freight ID#" + shipment.FreightID + " from the local LAN database.",ex); }
            return bVal;
        }
    }
}
