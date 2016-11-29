using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using Argix.Data;
using Argix.Enterprise;

namespace Argix.AgentLineHaul {
	//
	public class ShipSchedule {
		//Members
        public static bool CanEditFreightAssigned = true;
        public static int ValidationWindow = 30;
		public static string CarrierReportPath="/Shipping/Carrier Ship Schedule";
		public static string AgentReportPath="/Shipping/Agent Ship Schedule";
		
		private string mScheduleID="";
		private long mSortCenterID=0;
		private string mSortCenter="";
		private DateTime mScheduleDate=DateTime.Today;
		private ShipScheduleDS mTrips=null;
		private ShipScheduleDS mTemplates=null;
		private Mediator mMediator=null;
		
		public event EventHandler Changed=null;
		
		//Interface
		public ShipSchedule(Mediator mediator): this(null, mediator) { }
		public ShipSchedule(ShipScheduleDS.ShipScheduleViewTableRow schedule, Mediator mediator) { 
			//Constructor
			try { 
				this.mMediator = mediator;
				if(schedule != null) {
					this.mScheduleID = schedule.ScheduleID;
					this.mSortCenterID = schedule.SortCenterID;
					this.mSortCenter = schedule.SortCenter;
					this.mScheduleDate = schedule.ScheduleDate;
				}
				this.mTrips = new ShipScheduleDS();
				this.mTemplates = new ShipScheduleDS();
				Refresh();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to instantiate a new ship schedule.", ex); }
		}
		#region Accessors\Modifiers: ScheduleID, SortCenterID, SortCenter, ScheduleDate, Trips, ToDataSet()
		public string ScheduleID { get { return this.mScheduleID; } }
		public long SortCenterID { get { return this.mSortCenterID; } }
		public string SortCenter { get { return this.mSortCenter.Trim(); } }
		public DateTime ScheduleDate { get { return this.mScheduleDate; } }
		public ShipScheduleDS Trips { get { return this.mTrips; } }
		public ShipScheduleDS Templates { get { return this.mTemplates; } }
		public DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			return this.mTrips;
		}
		#endregion
		public void Refresh() {
			//Update a collection (dataset) of all ship schedule trips for the terminal on the local LAN database
			try {
				//Clear and update trips for this schedule
				this.mTrips.Clear();
				this.mTemplates.Clear();
				DataSet ds = this.mMediator.FillDataset(Lib.USP_SCHEDULE, Lib.TBL_SCHEDULE, new object[]{this.mSortCenterID, this.mScheduleDate});
				if(ds.Tables[Lib.TBL_SCHEDULE].Rows.Count > 0) 
					this.mTrips.Merge(ds, true, MissingSchemaAction.Ignore);

                ds = this.mMediator.FillDataset(Lib.USP_TEMPLATES_AVAILABLE,Lib.TBL_TEMPLATES_AVAILABLE,new object[] { this.mSortCenterID,this.mScheduleDate });
                if(ds.Tables[Lib.TBL_TEMPLATES_AVAILABLE].Rows.Count > 0) { 
					this.mTemplates.Merge(ds);
					foreach(ShipScheduleDS.TemplateViewTableRow row in this.mTemplates.TemplateViewTable.Rows) 
						row.Selected = (row.IsMandatory == 1);
					this.mTemplates.AcceptChanges();
				}
			}
			catch(Exception ex) { throw new ApplicationException("Failed to refresh ship schedule.", ex); }
			finally { if(this.Changed != null) this.Changed(this, EventArgs.Empty); }
		}
		public void Create() {
			//Persist this definition
			try {
				//Validate user rights
				if(this.mSortCenterID == 0)
					throw new ApplicationException("Failed to create new ship schedule: must have a valid sort center ID.");
				
				//Create a new ship schedule for this SortCenterID and ScheduleDate
				this.mScheduleID = (string)this.mMediator.ExecuteNonQueryWithReturn(Lib.USP_SCHEDULENEW, new object[]{ this.mScheduleID, this.mSortCenterID, this.mScheduleDate, DateTime.Now, Environment.UserName });
				for(int i=0; i<this.mTemplates.TemplateViewTable.Rows.Count; i++) {
					//Add all mandatory loads only to the new schedule
					if(this.mTemplates.TemplateViewTable[i].IsMandatory == 1)
						this.mMediator.ExecuteNonQuery(Lib.USP_TRIPNEW, new object[]{ "", this.mScheduleID, this.mTemplates.TemplateViewTable[i].TemplateID, DateTime.Now, Environment.UserName });
				}
				Refresh();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to create ship schedule.", ex); }
		}
		public void Update() {
			//Update: Jan 2005
			//We are refreshing the dataset now. We are extracting the row versions and 
			//updating the updated rows. This will keep the selected row in the view.
			//Updated: August 29, 2005
			//Checks for duplicate load number within the same carrier during the past and future 7 days.
			try {
				//Determine changes made to the trips in this ship schedule
				ShipScheduleDS trips = (ShipScheduleDS)this.mTrips.GetChanges(DataRowState.Modified);
				if(trips != null && trips.ShipScheduleTable.Rows.Count > 0) {
					//Update each modified trip
					foreach(ShipScheduleDS.ShipScheduleTableRow row in trips.ShipScheduleTable.Rows) {
						//Check to see if load# or carrier has changed; if so, then make sure it's unique within the same 
						//carrier (updated once if it's updated along with load#) and during the past one week schedule
						if(isLoadNumberOrCarrierChanged(row)) {
							DataSet loadNumberDS = this.mMediator.FillDataset(Lib.USP_TRIP, Lib.TBL_TRIP, new object[]{row.ScheduleDate, System.DBNull.Value, row.LoadNumber.Trim()});
							if(loadNumberDS.Tables[0].Rows.Count > 0)
								throw new DuplicateLoadNumberException("Duplicate load# found in ship schedule for " + loadNumberDS.Tables[0].Rows[0][1].ToString()  + ".");
						}
						
						//Save trip details
						ShipScheduleDS _trip = updateTrip(populateTrip(row));
						try {
							//Refresh the details of the current trip (instead of a full refresh)
							ShipScheduleDS.ShipScheduleTableRow trip = this.mTrips.ShipScheduleTable.FindByTripID(row.TripID);
							trip.SCDERowVersion = _trip.ShipScheduleTripTable[0].RowVersion;
							trip.S1RowVersion = _trip.ShipScheduleStopTable[0].RowVersion;
							if(_trip.ShipScheduleStopTable.Rows.Count == 2) trip.S2RowVersion = _trip.ShipScheduleStopTable[1].RowVersion;
							this.mTrips.AcceptChanges();
						}
						catch(Exception ex) { throw new ApplicationException("Failed to partially refresh the ship schedule.", ex); }
					}
					//Refresh();		Doing partial refresh above for performance reasons (i.e. cell editing)
				}
			}
			catch(DuplicateLoadNumberException ex) { throw ex; }
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to update ship schedule.", ex); }
		}
		public void AddLoads() {
			//Add template loads to this schedule
			try {
				//Add loads
				for(int i=0; i<this.mTemplates.TemplateViewTable.Rows.Count; i++) {
					//Add all selected loads only to the new schedule
					if(this.mTemplates.TemplateViewTable[i].Selected == true)
						this.mMediator.ExecuteNonQuery(Lib.USP_TRIPNEW, new object[]{ "", this.mScheduleID, this.mTemplates.TemplateViewTable[i].TemplateID, DateTime.Now, Environment.UserName });
				}
				Refresh();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to add template loads to ship schedule.", ex); }
		}
		private ShipScheduleDS updateTrip(ShipScheduleDS trip) {
			//Updated: January 2005
			//Update a single trip on the schedule; return updated row (i.e. including new row version)
			//We were not updating Stop 2; there could be 2 rows in the stop table
			SqlParameter[] spTripParams=null, spStop1Params=null, spStop2Params=null;
			SqlConnection sqlConn=null;
			try {
				sqlConn = new SqlConnection(this.mMediator.Connection);
				sqlConn.Open();
				using(SqlTransaction sqlTrans = sqlConn.BeginTransaction()) {
					try {
						//Update trip details
						spTripParams = setTripParameters(trip.ShipScheduleTripTable[0]);
						SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, Lib.USP_TRIPUPDATE, spTripParams);
					}
					catch(Exception ex) { sqlTrans.Rollback(); throw new ApplicationException("Failed to update ship schedule trip details... Rolling back update transaction.", ex); }
					try {
						//Update stop 1 details
						spStop1Params = setStopParameters(trip.ShipScheduleStopTable[0]);
						SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, Lib.USP_STOPUPDATE, spStop1Params);
					}
					catch(Exception ex) { sqlTrans.Rollback(); throw new ApplicationException("Failed to update ship schedule stop 1 details... Rolling back update transaction.", ex); }
					try {
						//Update stop 2 details
						if(trip.ShipScheduleStopTable.Rows.Count == 2) {
							spStop2Params = setStopParameters(trip.ShipScheduleStopTable[1]);
							SqlHelper.ExecuteNonQuery(sqlTrans, CommandType.StoredProcedure, Lib.USP_STOPUPDATE, spStop2Params);
						}
					}
					catch(Exception ex) { sqlTrans.Rollback(); throw new ApplicationException("Failed to update ship schedule stop 2 details... Rolling back update transaction.", ex); }
					
					//Commit changes and update row versions
					sqlTrans.Commit();
					trip.ShipScheduleTripTable[0].RowVersion = spTripParams[15].Value.ToString();
					trip.ShipScheduleStopTable[0].RowVersion = spStop1Params[6].Value.ToString();
					if(spStop2Params != null) trip.ShipScheduleStopTable[1].RowVersion = spStop2Params[6].Value.ToString();
					trip.AcceptChanges();
				}
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to update ship schedule details.", ex); }
			finally { if(sqlConn != null) sqlConn.Dispose(); }
			return trip;
		}
		#region Schedule Services: isLoadNumberOrCarrierChanged(), populateTrip(), setTripParameters(), setStopParameters()
		public bool isLoadNumberOrCarrierChanged(DataRow row) {
			//New Method: August 2005
			//This method will check if we need to look for duplicate Load Number of not before saving the load
			string loadNumber = row["LoadNumber"].ToString().Trim();
			if(loadNumber.Length == 0)
				return false;
			else {
				string carrierSvcID = row["CarrierServiceID"].ToString().Trim();
				if(loadNumber == row["LoadNumber", DataRowVersion.Original].ToString().Trim() && carrierSvcID == row["CarrierServiceID", DataRowVersion.Original].ToString().Trim())
					return false;
				else
					return true;
			}
		}
		private ShipScheduleDS populateTrip(ShipScheduleDS.ShipScheduleTableRow sourceRow) {
			//Updated with new fields - TractorNumber and FreightAssinged
			ShipScheduleDS detailDS = new ShipScheduleDS();
			
			//Trip
			ShipScheduleDS.ShipScheduleTripTableRow tripRow = detailDS.ShipScheduleTripTable.NewShipScheduleTripTableRow();
			tripRow.CarrierServiceID = sourceRow.CarrierServiceID;
			tripRow.LastUpdated = System.DateTime.Now;
			tripRow.LoadNumber = sourceRow.LoadNumber.Trim();
			tripRow.TractorNumber = sourceRow.TractorNumber.Trim();
			tripRow.DriverName = sourceRow.DriverName.Trim();
			tripRow.RowVersion = sourceRow.SCDERowVersion;
			tripRow.ScheduledClose = sourceRow.ScheduledClose;
			tripRow.ScheduledDeparture = sourceRow.ScheduledDeparture;
			tripRow.TrailerNumber = sourceRow.TrailerNumber.Trim();
			tripRow.TripID = sourceRow.TripID;
			tripRow.UserID = Environment.UserName;
			if(!sourceRow.IsFreightAssignedNull()) tripRow.FreightAssigned = sourceRow.FreightAssigned;
			if(!sourceRow.IsTrailerCompleteNull()) tripRow.TrailerComplete = sourceRow.TrailerComplete;
			if(!sourceRow.IsTrailerDispatchedNull()) tripRow.TrailerDispatched = sourceRow.TrailerDispatched;
			if(!sourceRow.IsPaperworkCompleteNull()) tripRow.PaperworkComplete = sourceRow.PaperworkComplete;
			if(!sourceRow.IsCanceledNull()) tripRow.Canceled = sourceRow.Canceled;
			detailDS.ShipScheduleTripTable.AddShipScheduleTripTableRow(tripRow);

			//Associated stops
			ShipScheduleDS.ShipScheduleStopTableRow stop1Row = detailDS.ShipScheduleStopTable.NewShipScheduleStopTableRow();
			stop1Row.LastUpdated = System.DateTime.Now;
			stop1Row.Notes = sourceRow.Notes;
			stop1Row.RowVersion = sourceRow.S1RowVersion;
			stop1Row.ScheduledArrival = sourceRow.ScheduledArrival;
			stop1Row.ScheduledOFD1 = sourceRow.ScheduledOFD1;
			stop1Row.StopID = sourceRow.StopID;
			stop1Row.UserID = sourceRow.S1UserID;
			detailDS.ShipScheduleStopTable.AddShipScheduleStopTableRow(stop1Row);
			if(sourceRow.S2MainZone != null & sourceRow.S2MainZone.Trim() != "") {
				ShipScheduleDS.ShipScheduleStopTableRow stop2Row = detailDS.ShipScheduleStopTable.NewShipScheduleStopTableRow();
				stop2Row.LastUpdated = System.DateTime.Now;
				stop2Row.Notes = sourceRow.S2Notes;
				stop2Row.RowVersion = sourceRow.S2RowVersion;
				stop2Row.ScheduledArrival = sourceRow.S2ScheduledArrival;
				stop2Row.ScheduledOFD1 = sourceRow.S2ScheduledOFD1;
				stop2Row.StopID = sourceRow.S2StopID;
				stop2Row.UserID = sourceRow.S2UserID;
				detailDS.ShipScheduleStopTable.AddShipScheduleStopTableRow(stop2Row);
			}
			return detailDS;
		}
		private SqlParameter[] setTripParameters(ShipScheduleDS.ShipScheduleTripTableRow row) {
			SqlParameter [] parms = new SqlParameter[16];
			parms[0] = new SqlParameter("@TripID", SqlDbType.Char,11 ); 
			parms[0].Value = row.TripID;
			parms[1] = new SqlParameter("@CarrierServiceID", SqlDbType.BigInt,8 ); 
			if(!row.IsCarrierServiceIDNull()) parms[1].Value = row.CarrierServiceID;
			parms[2] = new SqlParameter("@LoadNumber", SqlDbType.VarChar, 15);
			parms[2].Value = row.LoadNumber;
			parms[3] = new SqlParameter("@TrailerNumber", SqlDbType.VarChar, 15);
			parms[3].Value = row.TrailerNumber;
			parms[4] = new SqlParameter("@TractorNumber", SqlDbType.VarChar, 15);
			parms[4].Value = row.TractorNumber;
			parms[5] = new SqlParameter("@DriverName", SqlDbType.VarChar, 30);
			parms[5].Value = row.DriverName;

			parms[6] = new SqlParameter("@ScheduledClose", SqlDbType.DateTime, 8);
			parms[6].Value = row.ScheduledClose;
			parms[7] = new SqlParameter("@ScheduledDeparture", SqlDbType.DateTime, 8);
			parms[7].Value = row.ScheduledDeparture;
			parms[8] = new SqlParameter("@FreightAssigned", SqlDbType.DateTime, 8);
			if(!row.IsFreightAssignedNull()) parms[8].Value = row.FreightAssigned;
			parms[9] = new SqlParameter("@TrailerComplete", SqlDbType.DateTime, 8);
			if(!row.IsTrailerCompleteNull()) parms[9].Value = row.TrailerComplete;
			parms[10] = new SqlParameter("@PaperworkComplete", SqlDbType.DateTime, 8);
			if(!row.IsPaperworkCompleteNull()) parms[10].Value = row.PaperworkComplete;
			parms[11] = new SqlParameter("@TrailerDispatched", SqlDbType.DateTime, 8);
			if(!row.IsTrailerDispatchedNull()) parms[11].Value = row.TrailerDispatched;
			parms[12] = new SqlParameter("@Canceled", SqlDbType.DateTime);
			if(!row.IsCanceledNull()) parms[12].Value = row.Canceled;
			parms[13] = new SqlParameter("@LastUpdated", SqlDbType.DateTime);
			parms[13].Value = row.LastUpdated;
			parms[14] = new SqlParameter("@UserID", SqlDbType.VarChar,50);
			parms[14].Value = row.UserID;
			parms[15] = new SqlParameter("@RowVersion", SqlDbType.Char,24 ); 
			parms[15].Value = row.RowVersion;
			parms[15].Direction = ParameterDirection.InputOutput;
			return parms;
		}
		private SqlParameter[] setStopParameters(ShipScheduleDS.ShipScheduleStopTableRow row) {
			SqlParameter [] parms = new SqlParameter[7];
			parms[0] = new SqlParameter("@StopID", SqlDbType.Char,13 ); 
			parms[0].Value = row.StopID;
			parms[1] = new SqlParameter("@Notes", SqlDbType.Char, 15);
			if(!row.IsNotesNull()) parms[1].Value = row.Notes;
			parms[2] = new SqlParameter("@ScheduledOFD1", SqlDbType.DateTime, 8);
			parms[2].Value = row.ScheduledOFD1;
			parms[3] = new SqlParameter("@ScheduledArrival", SqlDbType.DateTime, 8);
			parms[3].Value = row.ScheduledArrival;
			parms[4] = new SqlParameter("@LastUpdated", SqlDbType.DateTime);
			parms[4].Value = row.LastUpdated;
			parms[5] = new SqlParameter("@UserID", SqlDbType.VarChar,50);
			parms[5].Value = row.UserID;
			parms[6] = new SqlParameter("@RowVersion", SqlDbType.Char,24 ); 
			parms[6].Value = row.RowVersion;
			parms[6].Direction = ParameterDirection.InputOutput;
			return parms;
		}
		#endregion
	}
}
