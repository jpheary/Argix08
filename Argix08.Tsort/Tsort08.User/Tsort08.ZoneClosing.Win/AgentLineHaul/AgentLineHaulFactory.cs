using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.AgentLineHaul {
    //
    public class AgentLineHaulFactory {
        //Members
        private static string _ScheduleID="";
        private static DateTime _ScheduleDate=DateTime.Today;
        private static ShipScheduleDS _Trips=null;
        private static long _AgentTerminalID=0;				//Trip filter condition

        public const string USP_SHIPSCHEDULE_SCHEDULE = "uspZoneCloseShipScdeScheduleGet",TBL_SHIPSCHEDULE_SCHEDULE = "ShipScheduleMasterTable";
        public const string USP_SHIPSCHEDULE_FREIGHT = "uspZoneCloseAssignedFreightGetList",TBL_SHIPSCHEDULE_FREIGHT = "ShipScheduleDetailTable";
        public const string USP_SHIPSCHEDULE_PRIORTRIPS = "uspZoneCloseTripPriorGetList",TBL_SHIPSCHEDULE_PRIORTRIPS = "ShipScheduleMasterTable";
        public const string USP_TL_SEARCH = "uspZoneCloseShipScheduleGetForTL",TBL_TL_SEARCH = "TLSearchTable";
        public const string USP_SHIPSCHEDULE_ASSIGNFREIGHT = "uspZoneCloseFreightAssign";
        public const string USP_SHIPSCHEDULE_UNASSIGNFREIGHT = "uspZoneCloseFreightUnAssign";
        public const string USP_SHIPSCHEDULE_MOVEFREIGHT = "uspZoneCloseFreightMove";
        public const string USP_SHIPSCHEDULE_ALLASSIGNED = "uspZoneCloseTripIsAssignedUpdate";

        public static event EventHandler Changed=null;
        
        //Interface
        static AgentLineHaulFactory() { 
            //Constructor
            _Trips = new ShipScheduleDS();
        }
        private AgentLineHaulFactory() { }
        public static string ScheduleID { get { return _ScheduleID; } }
        public static DateTime ScheduleDate {
            get { return _ScheduleDate; }
            set {
                if(DateTime.Compare(value,_ScheduleDate) != 0) {
                    _ScheduleDate = value;
                    RefreshTrips();
                }
            }
        }
        public static ShipScheduleDS Trips { get { return _Trips; } }
        public static long AgentTerminalID {
            get { return _AgentTerminalID; }
            set {
                if(value != _AgentTerminalID) {
                    _AgentTerminalID = value;
                    RefreshTrips();
                }
            }
        }

        public static void RefreshTrips() {
            //Update a collection (dataset) of all ship schedule trips for the terminal on the local LAN database
            try {
                //Clear and update cached trips/stops for current schedule date
                _ScheduleID = "";
                _Trips.Clear();
                DataSet trips = new DataSet();
                string filter1 = _AgentTerminalID > 0 ? "AgentTerminalID=" + _AgentTerminalID + " OR S2AgentTerminalID=" + _AgentTerminalID : "";
                string filter2 = _AgentTerminalID > 0 ? "AgentTerminalID=" + _AgentTerminalID : "";
                DataSet ds = App.Mediator.FillDataset(USP_SHIPSCHEDULE_SCHEDULE,TBL_SHIPSCHEDULE_SCHEDULE,new object[] { _ScheduleDate });
                if(ds.Tables[TBL_SHIPSCHEDULE_SCHEDULE].Rows.Count > 0) {
                    //Capture scheduleID; then merge in trips- filter as required
                    _ScheduleID = ds.Tables[TBL_SHIPSCHEDULE_SCHEDULE].Rows[0]["ScheduleID"].ToString();
                    if(filter1.Length > 0) 
                        trips.Merge(ds.Tables[TBL_SHIPSCHEDULE_SCHEDULE].Select(filter1));
                    else
                        trips.Merge(ds);
                    if(trips.Tables[TBL_SHIPSCHEDULE_SCHEDULE] != null && trips.Tables[TBL_SHIPSCHEDULE_SCHEDULE].Rows.Count > 0) {
                        //Merge in stops- filter as required
                        ds = App.Mediator.FillDataset(USP_SHIPSCHEDULE_FREIGHT,TBL_SHIPSCHEDULE_FREIGHT,new object[] { _ScheduleID });
                        if(ds.Tables[TBL_SHIPSCHEDULE_FREIGHT].Rows.Count > 0) {
                            if(filter2.Length > 0)
                                trips.Merge(ds.Tables[TBL_SHIPSCHEDULE_FREIGHT].Select(filter2));
                            else
                                trips.Merge(ds);
                        }
                    }
                }
                _Trips.Merge(trips);
            }
            catch(ConstraintException ex) { throw new ApplicationException("Failed to refresh ship schedule- constraint exception.",ex); }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh ship schedule.",ex); }
            finally { if(Changed != null) Changed(null,EventArgs.Empty); }
        }
        public static ShipScheduleDS GetAvailableTrips(long agentTerminalID,DateTime dt) {
            //Get trips for the specified main zone and date that are available for assignment
            ShipScheduleDS trips=null;
            try {
                //Get the schedule for the specified date
                trips = new ShipScheduleDS();
                DataSet ds = App.Mediator.FillDataset(USP_SHIPSCHEDULE_SCHEDULE,TBL_SHIPSCHEDULE_SCHEDULE,new object[] { dt });
                if(ds.Tables[TBL_SHIPSCHEDULE_SCHEDULE].Rows.Count > 0) {
                    //Filter for trips (both stops) matching the specified agent terminal and that are open for assignment
                    ShipScheduleDS s = new ShipScheduleDS();
                    s.Merge(ds);
                    ShipScheduleDS schedule = new ShipScheduleDS();
                    schedule.Merge(s.ShipScheduleMasterTable.Select("AgentTerminalID='" + agentTerminalID + "' OR S2AgentTerminalID=" + agentTerminalID));
                    trips.Merge(schedule.ShipScheduleMasterTable.Select("IsNull(FreightAssigned, #08/02/61#) = #08/02/61#"));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to get ship schedule.",ex); }
            return trips;
        }
        public static ShipScheduleTrip GetTrip(string tripID) {
            //Return an existing trip from this ship schedule
            ShipScheduleTrip trip=null;
            try {
                //Merge from collection (dataset)
                ShipScheduleDS.ShipScheduleMasterTableRow _trip = (ShipScheduleDS.ShipScheduleMasterTableRow)_Trips.ShipScheduleMasterTable.Select("TripID='" + tripID + "'")[0];
                ShipScheduleDS.ShipScheduleDetailTableRow[] _tls = (ShipScheduleDS.ShipScheduleDetailTableRow[])_Trips.ShipScheduleDetailTable.Select("TripID='" + tripID + "'");
                trip = new ShipScheduleTrip(_trip,_tls);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to get trip " + tripID + ".",ex); }
            return trip;
        }
        public static ShipScheduleTrip GetEarlierTripFromAPriorSchedule(string tripID,string freightID) {
            //Return an earlier trip from a schedule prior to the one specified
            ShipScheduleTrip earlierTrip=null;
            try {
                //Get all earlier trips (open, not cancelled)
                DataSet ds = App.Mediator.FillDataset(USP_SHIPSCHEDULE_PRIORTRIPS,TBL_SHIPSCHEDULE_PRIORTRIPS,new object[] { tripID,freightID });
                if(ds.Tables[TBL_SHIPSCHEDULE_PRIORTRIPS].Rows.Count > 0) {
                    ShipScheduleDS schedule = new ShipScheduleDS();
                    schedule.Merge(ds);
                    DateTime date=_ScheduleDate.AddYears(-5);
                    int tag=0;
                    for(int i=0;i<schedule.ShipScheduleMasterTable.Rows.Count;i++) {
                        //Select a trip with the most recent schedule date (not including this.mScheduleDate)
                        ShipScheduleDS.ShipScheduleMasterTableRow trip = (ShipScheduleDS.ShipScheduleMasterTableRow)schedule.ShipScheduleMasterTable.Rows[i];
                        if(trip.ScheduleDate.CompareTo(_ScheduleDate) < 0) {
                            //Capture the most recent trip date
                            if(trip.ScheduleDate.CompareTo(date) > 0) { date = trip.ScheduleDate; tag = 0; }
                            if(trip.ScheduleDate.CompareTo(date) == 0) {
                                //Capture the trip taht is most recent and with the largest tag #
                                if(int.Parse(trip.Tag.Trim()) > tag) {
                                    tag = int.Parse(trip.Tag.Trim());
                                    earlierTrip = new ShipScheduleTrip(trip);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed determine if an earlier ship schedule trip exists.",ex); }
            return earlierTrip;
        }
        public static ShipScheduleTrip GetEarlierTripFromThisSchedule(string tripID,string freightID) {
            //Return an earlier trip from the current schedule than the one specified if one exists
            ShipScheduleTrip earlierTrip=null;
            try {
                //Get all earlier trips (open, not cancelled)
                DataSet ds = App.Mediator.FillDataset(USP_SHIPSCHEDULE_PRIORTRIPS,TBL_SHIPSCHEDULE_PRIORTRIPS,new object[] { tripID,freightID });
                if(ds.Tables[TBL_SHIPSCHEDULE_PRIORTRIPS].Rows.Count > 0) {
                    ShipScheduleDS schedule = new ShipScheduleDS();
                    schedule.Merge(ds);
                    int tag=0;
                    for(int i=0;i<schedule.ShipScheduleMasterTable.Rows.Count;i++) {
                        //Select only trips with the same schedule date as this.mScheduleDate
                        ShipScheduleDS.ShipScheduleMasterTableRow trip = (ShipScheduleDS.ShipScheduleMasterTableRow)schedule.ShipScheduleMasterTable.Rows[i];
                        if(trip.ScheduleDate.CompareTo(_ScheduleDate) == 0) {
                            //Capture the trip with the largest tag #
                            if(int.Parse(trip.Tag.Trim()) > tag) {
                                tag = int.Parse(trip.Tag.Trim());
                                earlierTrip = new ShipScheduleTrip(trip);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed determine if an earlier ship schedule trip exists.",ex); }
            return earlierTrip;
        }
        public static DataSet FindTL(string TLNumber) {
            //Find a TL on an existing ship schedule
            DataSet ds=null;
            try {
                ds = App.Mediator.FillDataset(USP_TL_SEARCH,TBL_TL_SEARCH,new object[] { TLNumber });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while searching for TL# " + TLNumber + ".",ex); }
            return ds;
        }

        public static bool AssignTL(ShipScheduleTrip trip,string tl) {
            //Assign an open TL to this trip
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_SHIPSCHEDULE_ASSIGNFREIGHT,new object[] { tl,trip.TripID });
                RefreshTrips();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to assign open TL#" + tl + " to ship schedule trip#" + trip.TripID + ".",ex); }
            return ret;
        }
        public static bool UnassignTL(ShipScheduleTrip trip,string tl) {
            //Unassign an open TL from this trip
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_SHIPSCHEDULE_UNASSIGNFREIGHT,new object[] { tl });
                RefreshTrips();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to unassign open TL#" + tl + " to ship schedule trip#" + trip.TripID + ".",ex); }
            return ret;
        }
        public static bool MoveTL(ShipScheduleTrip trip,string tl) {
            //Move a closed TL to this trip
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_SHIPSCHEDULE_MOVEFREIGHT,new object[] { tl,trip.TripID });
                RefreshTrips();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to move open TL#" + tl + " to ship schedule trip#" + trip.TripID + ".",ex); }
            return ret;
        }
        public static bool Open(ShipScheduleTrip trip) {
            //Open a trip to further TL assignments
            bool ret=false;
            try {
                //Validate, then open
                if(trip.IsOpen) throw new ApplicationException("Trip#" + trip.TripID + " is already open!");
                if(trip.IsComplete) throw new ApplicationException("Trip#" + trip.TripID + " is complete and cannot be opened!");
                ret = App.Mediator.ExecuteNonQuery(USP_SHIPSCHEDULE_ALLASSIGNED,new object[] { trip.TripID,0 });
                RefreshTrips();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to open trip#" + trip.TripID + ".",ex); }
            return ret;
        }
        public static bool Close(ShipScheduleTrip trip) {
            //Close a trip from further TL assignments
            bool ret=false;
            try {
                //Validate, then close
                if(!trip.IsOpen) throw new ApplicationException("Trip#" + trip.TripID + " is already closed!");
                if(trip.IsComplete) throw new ApplicationException("Trip#" + trip.TripID + " is complete and cannot be closed!");
                ret = App.Mediator.ExecuteNonQuery(USP_SHIPSCHEDULE_ALLASSIGNED,new object[] { trip.TripID,1 });
                RefreshTrips();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to close trip#" + trip.TripID + ".",ex); }
            return ret;
        }
    }
}
