using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Text;
using Argix.Data;

namespace Argix.Freight {
	//
	public class FreightFactory {
		//Members
        private static BearwareDS _InboundFreight = null;
        private static BearwareDS _Assignments = null;
        private static int _SortedDays = 2;

        public const string USP_TRIPVIEW = "uspIndFATripGetListForImportDate", TBL_TRIPVIEW = "BwareTripTable";
        public const string USP_ASSIGNMENTVIEW = "uspIndFAAssignmentGetList", TBL_ASSIGNMENTVIEW = "BwareStationTripTable";
        public const string USP_STATIONSUNASSIGNED = "uspIndFAStationGetListNotAssigned", TBL_STATIONS = "WorkstationDetailTable";
        public const string USP_ASSIGNMENTCREATE = "uspIndFAAssignmentNew";
        public const string USP_ASSIGNMENTDELETE = "uspIndFAAssignmentDelete";
        public const string USP_TRIPCREATE = "uspIndFATripUpdateOrNew";
        public const string USP_TRIPUPDATE = "uspIndFATripUpdateExport";
        public const string USP_TRIPSTARTSORT = "uspIndFATripUpdateStart";
        public const string USP_TRIPSTOPSORT = "uspIndFATripUpdateStop";
        public const string USP_SCANDETAIL = "uspIndFAScanGetListforTrip", TBL_SCANDETAIL = "Carton";

        public static event EventHandler FreightChanged = null;
        public static event EventHandler AssignmentsChanged = null;
		
		//Interface
        static FreightFactory() {
			_InboundFreight = new BearwareDS();
			_Assignments = new BearwareDS();
		}
        private FreightFactory() { }
        public static BearwareDS InboundFreight { get { return _InboundFreight; } }
        public static BearwareDS StationAssignments { get { return _Assignments; } }
        public static int SortedDays { get { return _SortedDays; } set { _SortedDays = value; RefreshFreight(); } }
        public static void RefreshFreight() {
			//Refresh the list of inbound trips for the local terminal
			try {
				_InboundFreight.Clear();
				DateTime date = DateTime.Today.AddDays(-(_SortedDays - 1));
				DataSet ds = App.Mediator.FillDataset(USP_TRIPVIEW, TBL_TRIPVIEW, new object[]{date});
				if(ds!=null) 
					_InboundFreight.Merge(ds, false, MissingSchemaAction.Ignore);
			}
			catch(Exception ex) { throw ex; }
			finally { if(FreightChanged != null) FreightChanged(null, new EventArgs()); }
		}
        public static BearwareTrip GetFreight(string tripNumber) {
			//Get a Bearware trip
			BearwareTrip trip=null;
			try {
				if(tripNumber.Length > 0) {
					//Merge from collection (dataset)
					BearwareDS.BwareTripTableRow row = (BearwareDS.BwareTripTableRow)_InboundFreight.BwareTripTable.Select("Number='" + tripNumber + "'")[0];
					trip = new BearwareTrip(row);
				}
				else
					trip = new BearwareTrip();
				trip.Changed += new EventHandler(OnTripChanged);
			}
			catch(Exception ex) { throw ex; }
			return trip;
		}
        private static void OnTripChanged(object sender, EventArgs e) {
			//Event handler for change in a trip (i.e. create, start, stop, export)
			try { RefreshFreight(); } catch(Exception) { }
		}
        public static void RefreshStationAssignments() {
			//Refresh the list of freight assignments for the local terminal
			try {
				_Assignments.Clear();
				DataSet ds = App.Mediator.FillDataset(USP_ASSIGNMENTVIEW, TBL_ASSIGNMENTVIEW, null);
				if(ds!=null) 
					_Assignments.Merge(ds, false, MissingSchemaAction.Ignore);
			}
			catch(Exception ex) { throw ex; }
			finally { if(AssignmentsChanged != null) AssignmentsChanged(null, new EventArgs()); }
		}
        public static StationAssignment GetAssignment(string stationNumber, string tripNumber) {
			//Get a station assignment
			StationAssignment assignment=null;
			int cartonCount=0;
			string carrier="", trailerNumber="";
			try {
				//Merge from collection (dataset)
				DataRow[] rows =_Assignments.BwareStationTripTable.Select("StationNumber='" + stationNumber + "' AND TripNumber='" + tripNumber + "'");
				if(rows.Length > 0) {
					//Existing assignment
					BearwareDS.BwareStationTripTableRow row = (BearwareDS.BwareStationTripTableRow)rows[0];
					cartonCount = row.CartonCount;
					carrier = row.Carrier;
					trailerNumber = row.TrailerNumber;
				}
				else {
					//New assignment
					cartonCount = 0;
					carrier = trailerNumber = "";
				}
				Workstation station = new Workstation(stationNumber);
				BearwareTrip trip = new BearwareTrip(tripNumber, cartonCount, carrier, trailerNumber);
				assignment = new StationAssignment("", station, trip);
				assignment.Changed += new EventHandler(OnAssignmentChanged);
			}
			catch(Exception ex) { throw ex; }
			return assignment;
		}
        private static void OnAssignmentChanged(object sender, EventArgs e) {
			//Event handler for change in a station assignment (i.e. create, delete)
			try { RefreshStationAssignments(); } catch(Exception) { }
		}
        public static WorkstationDS GetUnassignedStations() {
			//Returns a list of sort stations that are available for station assignments
			WorkstationDS workstations=null;
			try {
				workstations = new WorkstationDS();
                DataSet ds = App.Mediator.FillDataset(USP_STATIONSUNASSIGNED, TBL_STATIONS, null);
				if(ds!=null) 
					workstations.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return workstations;
		}
	}
}