//	File:	sqldata.cs
//	Author:	J. Heary, S. Virk
//	Date:	01/15/04
//	Desc:	Provides synchronization services with local LAN SQL Server database.
//	Rev:	02/18/05 (jph)- added PickupID parameter to SQL statements for methods
//			CreateFreight(), UpdateFreight() per additional Pickup field in Freight2
//			table.
//			10/17/06 (jph)- minor revisions per latest standards and practices.
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.ApplicationBlocks.Data;

namespace Tsort.Sort {
	//
	internal class SQLData {
		//Members
		public static int FreightSyncDays=7;
		public static string DBConnection="";
		
		//Constants
		private const string TBL_FREIGHTSYNC = "FreightSyncDSTable";
		
		//Interface
		public SQLData() {}
		public static DateTime GetLastRefresh() {
			//Returns datetime of last refresh between AS/400 and LAN freight
			DateTime dtRefresh=DateTime.Now;
			string sLastUpdatedDate="", sLastUpdatedTime="";
			SqlDataReader oReader=null;
			try { 
				//Get max lastupdated date\time from LAN Freight table
				string sSQL = "SELECT MAX(LastUpdatedDate) AS LastUpdatedDate, MAX(LastUpdatedTime) AS LastUpdatedTime FROM Freight2 " + "WHERE (LastUpdatedDate = (SELECT MAX(LastUpdatedDate) FROM Freight2))";
				oReader = SqlHelper.ExecuteReader(SQLData.DBConnection, CommandType.Text, sSQL);
				oReader.Read();
				if(oReader[0].ToString() != "") {
					//Format the Lastupdated (format 'CyyMMdd') date to DateTime type 
					sLastUpdatedDate = oReader[0].ToString();
					sLastUpdatedTime = oReader[1].ToString();
					int hr = (sLastUpdatedTime.Length == 5) ? Convert.ToInt32(sLastUpdatedTime.Substring(0,1)) : Convert.ToInt32(sLastUpdatedTime.Substring(0,2));
					int min = (sLastUpdatedTime.Length == 5) ? Convert.ToInt32(sLastUpdatedTime.Substring(1,2)) : Convert.ToInt32(sLastUpdatedTime.Substring(2,2));
					int sec = (sLastUpdatedTime.Length == 5) ? Convert.ToInt32(sLastUpdatedTime.Substring(3,2)) : Convert.ToInt32(sLastUpdatedTime.Substring(4,2));
					dtRefresh = new DateTime(Convert.ToInt32("20" + sLastUpdatedDate.Substring(1,2)),Convert.ToInt32(sLastUpdatedDate.Substring(3,2)),Convert.ToInt32(sLastUpdatedDate.Substring(5,2)),hr,min,sec);
				}
				else {
					//This will happen if there are is no Freight in the LAN DB; return date based on the configuration
					dtRefresh = DateTime.Today.Subtract(new TimeSpan(SQLData.FreightSyncDays,0,0,0));
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while determining date and time of last refresh.",ex); }
			finally { if(oReader != null) oReader.Close(); }
			return dtRefresh;
		}		
		public static void GetLastUpdatedDateTime(ref int date, ref int time) {
			//Returns last updated date and time from the Freight table
			string sLastUpdatedDate="", sLastUpdatedTime="";
			SqlDataReader oReader=null;
			int iFreightSyncDays=0;			
			try { 
				//Build the synchronization date range; convert to 'CyyMMdd' AS400 format
				DateTime dtTemp = DateTime.Today.Subtract(new TimeSpan(SQLData.FreightSyncDays,0,0,0));
				iFreightSyncDays = Convert.ToInt32(dtTemp.ToString("1yyMMdd"));
				
				//Get max lastupdated date\time from LAN Freight table
				string sSQL = "SELECT MAX(LastUpdatedDate) AS LastUpdatedDate, MAX(LastUpdatedTime) AS LastUpdatedTime FROM Freight2 " + "WHERE (LastUpdatedDate = (SELECT MAX(LastUpdatedDate) FROM Freight2))";
				oReader = SqlHelper.ExecuteReader(SQLData.DBConnection, CommandType.Text, sSQL);
				
				//Because of Max statement, there will always be a row even if there is no freight
				oReader.Read();
				if(oReader[0].ToString() != "" ) {
					//Format the Lastupdated (format 'CyyMMdd') date to DateTime type 
					sLastUpdatedDate = oReader[0].ToString();
					sLastUpdatedTime = oReader[1].ToString();
					DateTime freightDate = new DateTime(Convert.ToInt32("20" + sLastUpdatedDate.Substring(1,2)),Convert.ToInt32(sLastUpdatedDate.Substring(3,2)),Convert.ToInt32(sLastUpdatedDate.Substring(5,2)));
					
					//Make sure freightDate is not longer than what's specified in the configuration
					TimeSpan timeSpan = DateTime.Today.Subtract(freightDate);
					if(timeSpan.Days > SQLData.FreightSyncDays ) {
						//Set the last updateddate and time based on the configuration
						date = iFreightSyncDays;
						time = 0;
					}
					else {
						//Otherwise take the max date and time from the LAN Freight table
						date = Convert.ToInt32(sLastUpdatedDate);
						time =  Convert.ToInt32(sLastUpdatedTime);
					}
				}
				else {
					//This will happen if there are is no Freight in the LAN DB. Let's return dates based on the configuration
					date = iFreightSyncDays;
					time = 0;
				}
			}
			catch (Exception ex) { throw new ApplicationException("Failed to get last update date and time.", ex); }
			finally { if(oReader != null) oReader.Close(); }
		}		
		public bool CreateFreight(FreightSyncDS.FreightSyncDSTableRow row) {
			SqlConnection oConn=null;
			bool bVal=false;
			try {
				//
				oConn = new SqlConnection(SQLData.DBConnection);
				oConn.Open();
				string sSQL =	"INSERT INTO Freight2(ID, TDSNumber, TerminalID, ClientNumber, DivisionNumber, VendorNumber, " +
					"AgentNumber, PickupDate, PickupNumber, ReceiveDate, VendorKey, Cartons, Pallets, CarrierNumber, DriverNumber, " +
					"TrailerNumber, StorageTrailerNumber, CurrentLocation, FloorStatus, SealNumber, UnloadedStatus, " +
					"StartSortDate, StartSortTime, StopSortDate, StopSortTime, PickupID, LastUpdatedDate, LastUpdatedTime, IsSortable) " +
					"VALUES (@ID, @TDSNumber, @TerminalID, @ClientNumber, @DivisionNumber, @VendorNumber, @AgentNumber, " +
					"@PickupDate, @PickupNumber, @ReceiveDate, @VendorKey, @Cartons, @Pallets, @CarrierNumber, @DriverNumber, " +
					"@TrailerNumber, @StorageTrailerNumber, @CurrentLocation, @FloorStatus, @SealNumber, @UnloadedStatus, " +
					"@StartSortDate, @StartSortTime, @StopSortDate, @StopSortTime, @PickupID, @LastUpdatedDate, @LastUpdatedTime, @IsSortable)";
				SqlParameter[] oParams = new SqlParameter[]{getParameter("@ID",SqlDbType.Char,row.ID,10), 
															   getParameter("@TerminalID", System.Data.SqlDbType.Int, row.TerminalID,4), 
															   getParameter("@TDSNumber", System.Data.SqlDbType.Int, row.TDSNumber,4), 
															   getParameter("@ClientNumber", System.Data.SqlDbType.VarChar, row.ClientNumber, 3), 
															   getParameter("@DivisionNumber", System.Data.SqlDbType.VarChar, row.DivisionNumber, 2), 
															   getParameter("@VendorNumber", System.Data.SqlDbType.VarChar, row.VendorNumber, 5), 
															   getParameter("@AgentNumber", System.Data.SqlDbType.VarChar, row.AgentNumber,4), 
															   getParameter("@PickupDate", System.Data.SqlDbType.VarChar, row.PickupDate,8), 
															   getParameter("@PickupNumber", System.Data.SqlDbType.Int, row.PickupNumber,4), 
															   getParameter("@ReceiveDate", System.Data.SqlDbType.VarChar, row.ReceiveDate,8), 
															   getParameter("@VendorKey", System.Data.SqlDbType.VarChar, row.VendorKey,20), 
															   getParameter("@Cartons", System.Data.SqlDbType.Int, row.Cartons,4), 
															   getParameter("@Pallets", System.Data.SqlDbType.Int, row.Pallets,4), 
															   getParameter("@CarrierNumber", System.Data.SqlDbType.Int, row.CarrierNumber,4), 
															   getParameter("@DriverNumber", System.Data.SqlDbType.Int, row.DriverNumber,4), 
															   getParameter("@TrailerNumber", System.Data.SqlDbType.VarChar, row.TrailerNumber,15), 
															   getParameter("@StorageTrailerNumber", System.Data.SqlDbType.VarChar, row.StorageTrailerNumber,15), 
															   getParameter("@CurrentLocation", System.Data.SqlDbType.VarChar, row.CurrentLocation,8), 
															   getParameter("@FloorStatus", System.Data.SqlDbType.VarChar, row.FloorStatus,1), 
															   getParameter("@SealNumber", System.Data.SqlDbType.VarChar, row.SealNumber,15), 
															   getParameter("@UnloadedStatus", System.Data.SqlDbType.VarChar, row.UnloadedStatus,1), 
															   getParameter("@StartSortDate", System.Data.SqlDbType.VarChar, row.StartSortDate,8), 
															   getParameter("@StartSortTime", System.Data.SqlDbType.VarChar, row.StartSortTime,6), 
															   getParameter("@StopSortDate", System.Data.SqlDbType.VarChar, row.StopSortDate,8), 
															   getParameter("@StopSortTime", System.Data.SqlDbType.VarChar, row.StopSortTime,6), 
															   getParameter("@PickupID", System.Data.SqlDbType.VarChar, row.PickupID,10), 
															   getParameter("@LastUpdatedDate", System.Data.SqlDbType.Int, row.LastUpdatedDate,4), 
															   getParameter("@LastUpdatedTime", System.Data.SqlDbType.Int, row.LastUpdatedTime,4), 
															   getParameter("@IsSortable", System.Data.SqlDbType.TinyInt, row.IsSortable,1) };
				int iRec = SqlHelper.ExecuteNonQuery(oConn, CommandType.Text, sSQL, oParams);
				bVal = (iRec==1);
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create freight.", ex); }
			finally { if (oConn != null) oConn.Dispose(); }
			return bVal;
		}
		public DataSet ReadFreight(string freightID) {
			SqlConnection oConn=null;
			DataSet ds=null;
			try {
				//
				string sSQL = @"SELECT ID FROM freight2 WHERE ID = '" + freightID + "'";
				oConn = new SqlConnection(SQLData.DBConnection);
				oConn.Open();
				ds = new DataSet();
				SqlHelper.FillDataset(oConn, CommandType.Text, sSQL, ds, new string[]{TBL_FREIGHTSYNC});
			}
			catch(Exception ex) { throw new ApplicationException("Failed to read freight #" + freightID + ".", ex); }
			finally { if (oConn != null) oConn.Dispose(); }
			return ds;
		}
		public bool UpdateFreight(FreightSyncDS.FreightSyncDSTableRow row) {
			SqlConnection oConn=null;
			bool bVal=false;
			try {
				//
				oConn = new SqlConnection(SQLData.DBConnection);
				oConn.Open();
				string sSQL =	"UPDATE Freight2 SET TDSNumber = @TDSNumber, TerminalID = @TerminalID, " +
					"ClientNumber = @ClientNumber, DivisionNumber = @DivisionNumber, VendorNumber = @VendorNumber, " +
					"AgentNumber = @AgentNumber, PickupDate = @PickupDate, PickupNumber= @PickupNumber, " +
					"ReceiveDate = @ReceiveDate, VendorKey = @VendorKey, Cartons = @Cartons, " +
					"Pallets = @Pallets, CarrierNumber = @CarrierNumber, DriverNumber = @DriverNumber, " +
					"TrailerNumber = @TrailerNumber, StorageTrailerNumber = @StorageTrailerNumber, " +
					"CurrentLocation = @CurrentLocation, FloorStatus = @FloorStatus, SealNumber = @SealNumber, " +
					"UnloadedStatus = @UnloadedStatus, StartSortDate = @StartSortDate, " +
					"StartSortTime = @StartSortTime, StopSortDate = @StopSortDate, StopSortTime= @StopSortTime, " +
					"PickupID = @PickupID, LastUpdatedDate = @LastUpdatedDate, LastUpdatedTime = @LastUpdatedTime, IsSortable = @IsSortable " +
					"WHERE (ID = @Original_ID)";
				SqlParameter[] oParams = new SqlParameter[]{getParameter("@Original_ID",SqlDbType.Char,row.ID,10), 
															   getParameter("@TerminalID", System.Data.SqlDbType.Int, row.TerminalID,4), 
															   getParameter("@TDSNumber", System.Data.SqlDbType.Int, row.TDSNumber,4), 
															   getParameter("@ClientNumber", System.Data.SqlDbType.VarChar, row.ClientNumber, 3), 
															   getParameter("@DivisionNumber", System.Data.SqlDbType.VarChar, row.DivisionNumber, 2), 
															   getParameter("@VendorNumber", System.Data.SqlDbType.VarChar, row.VendorNumber, 5), 
															   getParameter("@AgentNumber", System.Data.SqlDbType.VarChar, row.AgentNumber,4), 
															   getParameter("@PickupDate", System.Data.SqlDbType.VarChar, row.PickupDate,8), 
															   getParameter("@PickupNumber", System.Data.SqlDbType.Int, row.PickupNumber,4), 
															   getParameter("@ReceiveDate", System.Data.SqlDbType.VarChar, row.ReceiveDate,8), 
															   getParameter("@VendorKey", System.Data.SqlDbType.VarChar, row.VendorKey,20), 
															   getParameter("@Cartons", System.Data.SqlDbType.Int, row.Cartons,4), 
															   getParameter("@Pallets", System.Data.SqlDbType.Int, row.Pallets,4), 
															   getParameter("@CarrierNumber", System.Data.SqlDbType.Int, row.CarrierNumber,4), 
															   getParameter("@DriverNumber", System.Data.SqlDbType.Int, row.DriverNumber,4), 
															   getParameter("@TrailerNumber", System.Data.SqlDbType.VarChar, row.TrailerNumber,15), 
															   getParameter("@StorageTrailerNumber", System.Data.SqlDbType.VarChar, row.StorageTrailerNumber,15), 
															   getParameter("@CurrentLocation", System.Data.SqlDbType.VarChar, row.CurrentLocation,8), 
															   getParameter("@FloorStatus", System.Data.SqlDbType.VarChar, row.FloorStatus,1), 
															   getParameter("@SealNumber", System.Data.SqlDbType.VarChar, row.SealNumber,15), 
															   getParameter("@UnloadedStatus", System.Data.SqlDbType.VarChar, row.UnloadedStatus,1), 
															   getParameter("@StartSortDate", System.Data.SqlDbType.VarChar, row.StartSortDate,8), 
															   getParameter("@StartSortTime", System.Data.SqlDbType.VarChar, row.StartSortTime,6), 
															   getParameter("@StopSortDate", System.Data.SqlDbType.VarChar, row.StopSortDate,8), 
															   getParameter("@StopSortTime", System.Data.SqlDbType.VarChar, row.StopSortTime,6), 
															   getParameter("@PickupID", System.Data.SqlDbType.VarChar, row.PickupID,10), 
															   getParameter("@LastUpdatedDate", System.Data.SqlDbType.Int, row.LastUpdatedDate,4), 
															   getParameter("@LastUpdatedTime", System.Data.SqlDbType.Int, row.LastUpdatedTime,4), 
															   getParameter("@IsSortable", System.Data.SqlDbType.TinyInt, row.IsSortable,1) };
				int iRec = SqlHelper.ExecuteNonQuery(oConn, CommandType.Text, sSQL, oParams);
				bVal = (iRec==1);
			}
			catch(Exception ex) { throw new ApplicationException("Failed to update freight.", ex); }
			finally { if (oConn != null) oConn.Dispose(); }
			return bVal;
		}
		public bool DeleteFreight(string shipmentID) {
			//
			SqlConnection oConn=null;
			bool bVal=false;
			try {
				oConn = new SqlConnection(SQLData.DBConnection);
				oConn.Open();
				string sSQL = "DELETE FROM freight2 WHERE ID = '" + shipmentID + "'";
				int iRec = SqlHelper.ExecuteNonQuery(oConn, CommandType.Text, sSQL);
				bVal = (iRec==1);
			}
			catch(Exception ex) { throw new ApplicationException("Failed to delete freight #" + shipmentID + ".", ex); }
			finally { if (oConn != null) oConn.Dispose(); }
			return bVal;
		}
		#region Parameter Creators
		private SqlParameter getParameter(string paramName, System.Data.SqlDbType sqlDBType, int paramValue, int paramSize) {
			SqlParameter parameter = new System.Data.SqlClient.SqlParameter(paramName, sqlDBType, paramSize);
			parameter.Value = paramValue;
			return parameter;
		}
		private SqlParameter getParameter(string paramName, System.Data.SqlDbType sqlDBType, DateTime paramValue, int paramSize) {
			SqlParameter parameter = new System.Data.SqlClient.SqlParameter(paramName, sqlDBType, paramSize);
			parameter.Value = paramValue;
			return parameter;
		}
		private SqlParameter getParameter(string paramName, System.Data.SqlDbType sqlDBType, string paramValue, int paramSize) {
			SqlParameter parameter = new System.Data.SqlClient.SqlParameter(paramName, sqlDBType, paramSize);
			parameter.Value = paramValue;
			return parameter;
		}
		#endregion
	}
}
