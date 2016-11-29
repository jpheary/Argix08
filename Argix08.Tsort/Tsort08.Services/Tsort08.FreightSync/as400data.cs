//	File:	as400data.cs
//	Author:	S. Virk
//	Date:	12/02/04
//	Desc:	Provides services with central AS400 Server database.
//	Rev:	02/18/05 (jph)- added INNER JOIN to SQL statements for methods
//			GetFreight() and GetUnsortedFreight().
//			10/17/06 (jph)- revised to accomodate changes in base class.
//			02/03/09 (jph)- updates to SQL per GM.
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Data.OleDb;

namespace Tsort.Sort {
	//
	internal class AS400Data : OleDBase {
		//Members
		private const string TBL_FREIGHTSYNC = "FreightSyncDSTable";
		
		//Interface
		public AS400Data(string as400Connection, string as400Login) {
			//Constructor
			try {
				base.Connection = as400Connection + ";" + as400Login;
			}
			catch (Exception ex) { throw new ApplicationException("Failed to create new AS400Data instance.", ex); }
		}
		public FreightSyncDS GetFreight(int date, int time) {	
			//Get freight from AS400 based on date and time fields
			FreightSyncDS ds=null;
			try {
				ds = new FreightSyncDS();
				string SQL =	"SELECT BELRCD AS ID, CAST(BECKNB AS INT) AS TDSNumber, CAST(BECHCD AS INT) AS TerminalID,BEAACD AS ClientNumber,  " + 
					"BEABCD AS DivisionNumber, BEB2CD AS VendorNumber, BEB4CD AS AgentNumber , CAST(BEAQDT + 19000000 AS CHAR(8)) AS PickupDate,  " + 
					"CAST(BEA2NB AS INT) AS PickupNumber, CAST(BECBDT + 19000000 AS CHAR(8)) AS ReceiveDate, BEEVCD AS VendorKey,  " + 
					"CAST(BEG4NB AS INT) AS Cartons, CAST(BEG5NB AS INT) AS Pallets, CAST(BECMNB AS INT) AS CarrierNumber, CAST(BECLNB AS INT) AS DriverNumber,  " + 
					"BEBGTX AS TrailerNumber, BEDYTX AS StorageTrailerNumber, BEBITX AS CurrentLocation, BEFKST AS FloorStatus, BEBHTX AS SealNumber, BEC5ST AS UnloadedStatus, " + 
					"CAST(CASE WHEN BEB9DT=0 THEN '' ELSE CAST(BEB9DT + 19000000 AS VARCHAR(8)) END AS CHAR(8)) AS StartSortDate,  " + 
					"CAST(CASE WHEN BEANTM=0 THEN '' ELSE CAST(BEANTM AS CHAR(6)) END AS CHAR(6)) AS StartSortTime,  " + 
					"CAST(CASE WHEN BECADT=0 THEN '' ELSE CAST(BECADT + 19000000 AS VARCHAR(8)) END AS CHAR(8)) AS StopSortDate,  " + 
					"CAST(CASE WHEN BEAOTM=0 THEN '' ELSE CAST(BEAOTM AS CHAR(6)) END AS CHAR(6)) AS StopSortTime,  " + 
					"A4LVCD AS PickupID, BEAADT AS LastUpdatedDate, BEAATM AS LastUpdatedTime,  " + 
					"CASE WHEN BEB2CD='99999' THEN 1 WHEN BEB2CD<>'99999' AND BEGYDT<>0 AND BEGJDT<>0 THEN 1 ELSE 0 END AS IsSortable " +
					"FROM TSRTDTA.TSBECPP " + 
					"INNER JOIN TSRTDTA.TSA4CPP ON BEB2CD=A4B2CD AND BEB4CD=A4B4CD AND BEAQDT=A4AQDT AND BEA2NB=A4A2NB AND BEAACD=A4AACD AND BEABCD=A4ABCD " + 
					"WHERE ((BEAADT=" + date + " AND BEAATM >=" + time + ") OR  " + 
					"BEAADT> " + date + ") AND NOT (BEB2CD='99999' AND BEB4CD='9999') ORDER BY LastUpdatedDate, LastUpdatedTime ";
				Debug.Write("GetFreight(): SQL=" + SQL + "\n");
				ds.Merge(base.FillDataSet(SQL, TBL_FREIGHTSYNC), false, MissingSchemaAction.Ignore);
			}
			catch (Exception ex) { throw new ApplicationException("Failed to get freight from AS400.", ex); }
			return ds;
		}
		public FreightSyncDS GetUnsortedFreight() {
			//Get all unsorted freight from AS400
			FreightSyncDS ds=null;
			try {
				ds = new FreightSyncDS();
				string SQL =	"SELECT BELRCD AS ID, CAST(BECKNB AS INT) AS TDSNumber, CAST(BECHCD AS INT) AS TerminalID,BEAACD AS ClientNumber,  " + 
					"BEABCD AS DivisionNumber, BEB2CD AS VendorNumber, BEB4CD AS AgentNumber , CAST(BEAQDT + 19000000 AS CHAR(8)) AS PickupDate,  " + 
					"CAST(BEA2NB AS INT) AS PickupNumber, CAST(BECBDT + 19000000 AS CHAR(8)) AS ReceiveDate, BEEVCD AS VendorKey,  " + 
					"CAST(BEG4NB AS INT) AS Cartons, CAST(BEG5NB AS INT) AS Pallets, CAST(BECMNB AS INT) AS CarrierNumber, CAST(BECLNB AS INT) AS DriverNumber,  " + 
					"BEBGTX AS TrailerNumber, BEDYTX AS StorageTrailerNumber, BEBITX AS CurrentLocation, BEFKST AS FloorStatus, BEBHTX AS SealNumber, BEC5ST AS UnloadedStatus, " + 
					"CAST(CASE WHEN BEB9DT=0 THEN '' ELSE CAST(BEB9DT + 19000000 AS VARCHAR(8)) END AS CHAR(8)) AS StartSortDate,  " + 
					"CAST(CASE WHEN BEANTM=0 THEN '' ELSE CAST(BEANTM AS CHAR(6)) END AS CHAR(6)) AS StartSortTime,  " + 
					"CAST(CASE WHEN BECADT=0 THEN '' ELSE CAST(BECADT + 19000000 AS VARCHAR(8)) END AS CHAR(8)) AS StopSortDate,  " + 
					"CAST(CASE WHEN BEAOTM=0 THEN '' ELSE CAST(BEAOTM AS CHAR(6)) END AS CHAR(6)) AS StopSortTime,  " + 
					"A4LVCD AS PickupID, BEAADT AS LastUpdatedDate, BEAATM AS LastUpdatedTime,  " + 
					"CASE WHEN BEB2CD='99999' THEN 1 WHEN BEB2CD<>'99999' AND BEGYDT<>0 AND BEGJDT<>0 THEN 1 ELSE 0 END AS IsSortable" + 
					"FROM TSRTDTA.TSBECPP " + 
					"INNER JOIN TSRTDTA.TSA4CPP ON BEB2CD=A4B2CD AND BEB4CD=A4B4CD AND BEAQDT=A4AQDT AND BEA2NB=A4A2NB AND BEAACD=A4AACD AND BEABCD=A4ABCD " + 
					"WHERE  (BEB9DT=0) " + 
					"AND NOT (BEB2CD='99999' AND BEB4CD='9999') ORDER BY LastUpdatedDate, LastUpdatedTime ";
				Debug.Write("GetUnsortedFreight(): SQL=" + SQL + "\n");
				ds.Merge(base.FillDataSet(SQL, TBL_FREIGHTSYNC), false, MissingSchemaAction.Ignore);
			}
			catch (Exception ex) { throw new ApplicationException("Failed to get unsorted freight from AS400.", ex); }
			return ds;
		}
		public FreightSyncDS GetDeletedFreight(int date, int time) {
			//Return all AS400 deleted freight
			FreightSyncDS ds=null;
			try {
				ds = new FreightSyncDS();
				string SQL =	"SELECT JILTCD AS ID " + 
					"FROM TSRTDTA.LTJICPP " + 
					"WHERE  ((JIAADT=" + date + " AND JIAATM >=" + time + ") OR  " + 
					"JIAADT> " + date + ")";
				Debug.Write("GetDeletedFreight(): SQL=" + SQL + "\n");
				ds.Merge(base.FillDataSet(SQL, TBL_FREIGHTSYNC), false, MissingSchemaAction.Ignore);
			}
			catch (Exception ex) { throw new ApplicationException("Failed to get deleted freight from AS400.", ex); }
			return ds;
		}
		public FreightSyncDS GetShipment(string freightID) {
			//Get details of a shipment from the AS400 database
			FreightSyncDS ds=null;
			try {
				ds = new FreightSyncDS();
				string SQL =	"SELECT BELRCD AS ID, " + 
					"CAST(CASE WHEN BEB9DT=0 THEN '' ELSE CAST(BEB9DT + 19000000 AS VARCHAR(8)) END AS CHAR(8)) AS StartSortDate,  " + 
					"CAST(CASE WHEN BEANTM=0 THEN '' ELSE CAST(BEANTM AS CHAR(6)) END AS CHAR(6)) AS StartSortTime,  " + 
					"CAST(CASE WHEN BECADT=0 THEN '' ELSE CAST(BECADT + 19000000 AS VARCHAR(8)) END AS CHAR(8)) AS StopSortDate,  " + 
					"CAST(CASE WHEN BEAOTM=0 THEN '' ELSE CAST(BEAOTM AS CHAR(6)) END AS CHAR(6)) AS StopSortTime  " + 
					"FROM TSRTDTA.TSBECPP " + 
					"WHERE BELRCD ='" + freightID + "'";
				Debug.Write("GetShipment(): SQL=" + SQL + "\n");
				ds.Merge(FillDataSet(SQL, TBL_FREIGHTSYNC), false, MissingSchemaAction.Ignore);
			}
			catch (Exception ex) { throw new ApplicationException("Failed to get shipment details for #" + freightID + " from AS400.", ex); }
			return ds;
		}
		public bool UpdateShipmentSortStart(string freightID, DateTime date) {
			//Updates start sort date\time; sets status = 1 (sorting) in AS400
			bool retVal=false;
			string sDate=date.ToString("1yyMMdd");
			string sTime=date.ToString("HHmmss");
			try {
				string sUpdateDate = "CAST('1' || SUBSTRING(CAST(CURRENT DATE AS CHAR(10)),3,2) || SUBSTRING(CAST(CURRENT DATE AS CHAR(10)),6,2) || SUBSTRING(CAST(CURRENT DATE AS CHAR(10)),9,2) AS INT)";
				string sUpdateTime = "CAST( SUBSTRING(CAST(CURRENT TIME AS CHAR(8)),1,2) || SUBSTRING(CAST(CURRENT TIME AS CHAR(8)),4,2) || SUBSTRING(CAST(CURRENT TIME AS CHAR(8)),7,2) AS INT) ";
				string SQL =	"UPDATE TSRTDTA.TSBECPP SET BEB9DT =" + sDate + ", BEANTM =" +  sTime + ", " +
					"BEAADT =" + sUpdateDate + ",BEAATM =" + sUpdateTime + ", BEFJST ='1', BEAUVN='ODBC', BEAAVN='ODBC' " +
					"WHERE BELRCD ='" + freightID + "' AND BEB9DT =0 AND BECADT =0"; 
				Debug.Write("UpdateShipmentSortStart(): SQL=" + SQL + "\n");
				int recAffected = base.ExecuteNonQuery(SQL);
				retVal = (recAffected > 0);
			}
			catch (Exception ex) { throw new ApplicationException("Failed to update sort start date for #" + freightID + " in AS400.", ex); }
			return retVal;
		}
		public bool UpdateShipmentSortStop(string freightID, DateTime date) {
			//Updates stop sort date\time; sets status = 3 (sorted) in AS400
			bool retVal=false;
			string sDate=date.ToString("1yyMMdd");
			string sTime=date.ToString("HHmmss");
			try {
				string sUpdateDate = "CAST('1' || SUBSTRING(CAST(CURRENT DATE AS CHAR(10)),3,2) || SUBSTRING(CAST(CURRENT DATE AS CHAR(10)),6,2) || SUBSTRING(CAST(CURRENT DATE AS CHAR(10)),9,2) AS INT)";
				string sUpdateTime = "CAST( SUBSTRING(CAST(CURRENT TIME AS CHAR(8)),1,2) || SUBSTRING(CAST(CURRENT TIME AS CHAR(8)),4,2) || SUBSTRING(CAST(CURRENT TIME AS CHAR(8)),7,2) AS INT) ";
				string SQL =	"UPDATE TSRTDTA.TSBECPP SET BECADT =" + sDate + ", BEAOTM =" +  sTime + ", " +
					"BEAADT =" + sUpdateDate + ",BEAATM =" + sUpdateTime + ", BEFJST ='3' , BEAVVN='ODBC', BEAAVN='ODBC' " +
					"WHERE BELRCD ='" + freightID + "' AND BECADT =0"; 
				Debug.Write("UpdateShipmentSortStop(): SQL=" + SQL + "\n");
				int recAffected = base.ExecuteNonQuery(SQL);
				retVal = (recAffected > 0);
			}
			catch (Exception ex) { throw new ApplicationException("Failed to update sort stop date for #" + freightID + " in AS400.", ex); }
			return retVal;
		}
	}
}
