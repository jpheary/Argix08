//	File:	freightsyncsvc.cs
//	Author:	S. Virk
//	Date:	01/15/04
//	Desc:	Provides freight synchronization services from AS400 to LAN SQL Server 
//			database including shipment record synchronization and shipment start 
//			sort date\time, stop sort date\time, and freight status.
//	Rev:	02/17/04 (sv)- Added method SynchUnsortedFreight to synchronize all 
//			unsorted freight from AS400; modified method SynchFreight() to read 
//			AS400 Delete Log table and delete corresponding shipments on LAN 
//			database every; it also goes by last updated date and time.
//			10/17/06 (jph)- minor revisions per latest standards and practices.
//			02/03/09 (jph)- revised error handlers.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;

namespace Tsort.Sort {
	//
	public enum SyncType { All=0, Unsorted=2 }
	public enum SortStatus { Start=1, Stop=2 }
	
	public class FreightSyncSvc {
		//Members
		public static string AS400Connection="";
		public static string AS400Login=AS400LOGIN;
        public static bool AS400Sync=true;
        public static string SQLConnection="";
		public static int SyncDaysMax=7;
		private SQLData mSQLData=null;
		private AS400Data mAS400Data=null;
		
		private const string AS400LOGIN = "User ID=ODBC;Password=ODBC;";		
		private const string TBL_FREIGHTSYNC = "FreightSyncDSTable";
		private const string EX_FREIGHT_NOTFOUND = "Freight not found in AS/400. It may have been removed.";
		private const string EX_FREIGHT_STATUS_UPDATE_FAILED = "Freight Status could not be changed. Please contact Administrator.";
		private const string EX_FREIGHT_SYNC_FAILED = "Freight sychronization from AS/400 failed. Please contact Administrator.";
		private const string EX_FREIGHT_UNSORT_SYNC_FAILED = "Unsorted Freight sychronization from AS/400 failed. Please contact Administrator.";
		private const string EX_FREIGHT_DELETE_SYNC_FAILED = "Freight sychronization (Delete) from AS/400 failed. Please contact Administrator.";
		
		//Interface
		public FreightSyncSvc() {
			//Constructor
			try {
				SQLData.FreightSyncDays = FreightSyncSvc.SyncDaysMax;
				SQLData.DBConnection = FreightSyncSvc.SQLConnection;
				this.mSQLData = new SQLData();
				this.mAS400Data = new AS400Data(FreightSyncSvc.AS400Connection, FreightSyncSvc.AS400Login);
			}
			catch (ApplicationException ex) { throw ex; }
			catch (Exception ex) { throw new ApplicationException("Failed to create new FreightSyncSvc instance.", ex); }
		}
		public DateTime StartSortingShipment(string shipmentID) {
			//Set start sort date and status for this shipment
			//Update freight status with StartSort date/time in the AS400 database; if freight is
			//found, then check StartSort; if it exists, then don't change anything in AS400 but  
			//update StartSort in LAN database.
			//If StartSort date/time in AS400 is updated, then update LastUpdate date/time; if 
			//successfull, then update StartSort in LAN database.
			//NOTE: Don't touch LastUpdate date/time in LAN- synchronization takes care of this.
			//Assignment allowed only after status is changed successfully in both AS400 & LAN databases.
			FreightSyncDS ds=null;
			bool bStarted=false;
			DateTime dt=DateTime.Now;
			try {
				//On/off switch
				if(FreightSyncSvc.AS400Login == "")
					return DateTime.Now;
				
				//Get the shipment from ths AS400 database
				ds = this.mAS400Data.GetShipment(shipmentID);
				if(ds.FreightSyncDSTable.Rows.Count == 0) {
					//Shipment doesn't exist in AS400 (removed manually?); remove from LAN database
					this.mSQLData.DeleteFreight(shipmentID);
					throw new ApplicationException(EX_FREIGHT_NOTFOUND);
				}
				else { 
					//Shipment exists in AS400- update startsort and lastupdated if not set
					if(!ds.FreightSyncDSTable[0].IsStartSortDateNull()) {
						if(ds.FreightSyncDSTable[0].StartSortDate != "") {
							//Sort started in AS400 record; ensure same date/time in LAN
							string sDate = ds.FreightSyncDSTable[0].StartSortDate;
							string sTime = ds.FreightSyncDSTable[0].StartSortTime.PadLeft(6, '0');
							dt = new DateTime(Convert.ToInt32(sDate.Substring(0,4)), Convert.ToInt32(sDate.Substring(4,2)), Convert.ToInt32(sDate.Substring(6,2)), Convert.ToInt32(sTime.Substring(0,2)), Convert.ToInt32(sTime.Substring(2,2)), Convert.ToInt32(sTime.Substring(4,2)));
							bStarted = true;
						}
					}
					if(!bStarted)
						this.mAS400Data.UpdateShipmentSortStart(shipmentID, dt);
				}
			}
			catch (ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to start sorting shipment #" + shipmentID + ".", ex); }
			return dt;
		}
		public DateTime StopSortingShipment(string shipmentID) {
			//
			FreightSyncDS ds;
			bool bStopped=false;
			DateTime dt=DateTime.Now;
			try {
				//On/off switch
				if(FreightSyncSvc.AS400Login == "")
					return DateTime.Now;
				
				//Get the shipment from the AS400 database
				ds = this.mAS400Data.GetShipment(shipmentID);
				if(ds.FreightSyncDSTable.Rows.Count == 0) {
					//Shipment doesn't exist in AS400 (removed manually?)
					//DO NOT DELETE THE SHIPMENT FROM THE LAN
					throw new ApplicationException(EX_FREIGHT_NOTFOUND);
				}
				else { 
					//Shipment exists in AS400- update stopsort and last updated 
					if(!ds.FreightSyncDSTable[0].IsStopSortDateNull()) {
						if(ds.FreightSyncDSTable[0].StopSortDate != "") {
							//Sort stopped in AS400 record; ensure same date/time in LAN
							string sDate = ds.FreightSyncDSTable[0].StopSortDate;
							string sTime = ds.FreightSyncDSTable[0].StopSortTime.PadLeft(6, '0');
							dt = new DateTime(Convert.ToInt32(sDate.Substring(0,4)), Convert.ToInt32(sDate.Substring(4,2)), Convert.ToInt32(sDate.Substring(6,2)), Convert.ToInt32(sTime.Substring(0,2)), Convert.ToInt32(sTime.Substring(2,2)), Convert.ToInt32(sTime.Substring(4,2)));
							bStopped = true;
						}
					}
					if(!bStopped)
						this.mAS400Data.UpdateShipmentSortStop(shipmentID, dt);
				}
			}
			catch (ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Failed to stop sorting shipment #" + shipmentID + ".", ex); }
			return dt;
		}
		public DateTime GetLastRefresh() { 
			//Get date and time of last refresh
			DateTime dt=DateTime.Now;
			try {
				//On/off switch
				if(FreightSyncSvc.AS400Login == "")
					dt = DateTime.Now;
				else
					dt = SQLData.GetLastRefresh();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while requesting last refresh date and time.",ex); }
			return dt;
		}
		public void SyncFreight(SyncType syncType) {	
			//Sync all freight
			int date=0; int time=0;
			
			//On/off switch
            if(!AS400Sync || FreightSyncSvc.AS400Login == "")
                return;
			
			switch(syncType) {
				case SyncType.All:
					try {
						//Get latest date\time from the local Freight table
						SQLData.GetLastUpdatedDateTime(ref date, ref time);
				
						//Fetch data from AS400 Freight table; and create a typed FreightSyncDS dataset which will 
						//be used to update LAN Freight table
						FreightSyncDS dsUpdates = this.mAS400Data.GetFreight(date, time);
						foreach(FreightSyncDS.FreightSyncDSTableRow row in dsUpdates.FreightSyncDSTable.Rows) {
							if(this.mSQLData.ReadFreight(row.ID).Tables[TBL_FREIGHTSYNC].Rows.Count > 0)
								this.mSQLData.UpdateFreight(row);
							else
								this.mSQLData.CreateFreight(row);
						}
					}
					catch (ApplicationException ex) { throw ex; }
					catch (Exception ex) { throw new ApplicationException(EX_FREIGHT_SYNC_FAILED, ex); }
					
					try {
						//Check AS400 for deleted freight; if found, then delete from local Freight table
						FreightSyncDS dsDeletes = this.mAS400Data.GetDeletedFreight(date, time);
						foreach(FreightSyncDS.FreightSyncDSTableRow row in dsDeletes.FreightSyncDSTable.Rows) 
							this.mSQLData.DeleteFreight(row.ID);
					}
					catch (ApplicationException ex) { throw ex; }
					catch (Exception ex) { throw new ApplicationException(EX_FREIGHT_DELETE_SYNC_FAILED, ex); }
					break;
				case SyncType.Unsorted:
					try {
						//Fetch data from AS400 Freight table and create a typed FreightSyncDS dataset which will be used to update LAN Freight table
						FreightSyncDS dsUnsorted = this.mAS400Data.GetUnsortedFreight();
						foreach(FreightSyncDS.FreightSyncDSTableRow row in dsUnsorted.FreightSyncDSTable.Rows) {
							if(this.mSQLData.ReadFreight(row.ID).Tables[TBL_FREIGHTSYNC].Rows.Count > 0)
								this.mSQLData.UpdateFreight(row);
							else
								this.mSQLData.CreateFreight(row);
						}
					}
					catch (ApplicationException ex) { throw ex; }
					catch (Exception ex) { throw new ApplicationException(EX_FREIGHT_UNSORT_SYNC_FAILED, ex); }
					break;
			}
		}
	}
}
