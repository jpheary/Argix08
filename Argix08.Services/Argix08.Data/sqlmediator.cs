//	File:	sqlmediator.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Mediates data access with Sql data sources directly through the 
//			Microsoft.ApplicationBlocks.Data.SqlHelper class.
//
//			Mediator.SqlCommandTimeout: controls SqlCommand timeout.
//	Rev:	02/25/08 (jph)- changed configuration from AppSettings to Settings.Default.
//	        09/10/09 (jph)- added getLocalTerminal() override.
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Microsoft.ApplicationBlocks.Data;

namespace Argix.Data {
	/// <summary></summary>
	public class SQLMediator : Mediator {
		//Members
        /// <summary></summary>
        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet";
        /// <summary></summary>
        public const string TBL_LOCALTERMINAL = "LocalTerminalTable";

		//Interface
		/// <summary></summary>
		public SQLMediator() : this(global::Argix.Properties.Settings.Default.SQLConnection) { }
		/// <summary></summary>
		/// <param name="connectionString"></param>
		public SQLMediator(string connectionString) {
			//Constructor
            try {
                //Initialize members; set data connection
                if(connectionString == null || connectionString.Length == 0)
                    throw new ApplicationException("Database connection string is invalid (null).");
                base.mConnection = connectionString;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new SQLMediator instance.",ex); }
        }
        #region Mediator Overrides
        /// <summary></summary>
        protected override bool online() {
            //Determine online status
            bool online=false;
            try {
                string dbserver = "";
                string connection = base.mConnection;
                string[] tokens = connection.Split(';');
                foreach(string token in tokens) {
                    if(token.Contains("data source=")) {
                        dbserver = token.Substring(token.IndexOf("=") + 1);
                        break;
                    }
                }
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                if(ping.Send(dbserver,Mediator.PingTimeout).Status == System.Net.NetworkInformation.IPStatus.Success)
                    online = true;
            }
            catch { }
            return online;
        }
        /// <summary></summary>
		protected override DataSet executeDataset(string spName, object[] paramValues) { 
			return SqlHelper.ExecuteDataset(base.mConnection, spName, paramValues);
		}
		/// <summary></summary>
		protected override bool executeNonQuery(string spName, object[] paramValues) { 
			return (SqlHelper.ExecuteNonQuery(base.mConnection, spName, paramValues) > 0);
		}
		/// <summary></summary>
		protected override object executeNonQueryWithReturn(string spName, object[] paramValues) {
			SqlConnection oConn=null;
			object oRet=null;
			try {
				oConn = new SqlConnection(base.mConnection);
				if(oConn.State == ConnectionState.Closed) oConn.Open();
				if((paramValues != null) && (paramValues.Length > 0)) {
					//Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
					//Assign the provided values to these parameters based on parameter order
					SqlParameter[] oParams = SqlHelperParameterCache.GetSpParameterSet(this.mConnection, spName);
					assignParameterValues(oParams, paramValues);
					SqlHelper.ExecuteNonQuery(oConn, CommandType.StoredProcedure, spName, oParams);
					
					//Find the output parameter and return its value
					for(int i=0, j=oParams.Length; i<j; i++) {
						if((oParams[i].Direction == ParameterDirection.Output) || (oParams[i].Direction == ParameterDirection.InputOutput)) {
							oRet = oParams[i].Value;
							break;
						}
					}
				}
			}
			catch(Exception ex) { handleException(ex); }
			finally { if(oConn.State == ConnectionState.Open) oConn.Close(); }
			return oRet;
		}
		/// <summary></summary>
		protected override SqlDataReader executeReader(string spName, object[] paramValues) {
			return SqlHelper.ExecuteReader(base.mConnection, spName, paramValues);
		}
		/// <summary></summary>
		protected override object executeScalar(string spName, object[] paramValues) {
			return SqlHelper.ExecuteScalar(base.mConnection, spName, paramValues);
		}
		/// <summary></summary>
        protected override System.Xml.XmlReader executeXmlReader(string spName,object[] paramValues) {
			SqlConnection oConn=null;
			System.Xml.XmlReader oReader=null;
			try {
				oConn = new SqlConnection(base.mConnection);
				oConn.Open();
				oReader = SqlHelper.ExecuteXmlReader(oConn, spName, paramValues);
				base.setStatusNotification(true);
			}
			catch(Exception ex) { throw ex; }
			finally { if(oConn.State==ConnectionState.Open) oConn.Close(); }
			return oReader;
		}
		/// <summary></summary>
		protected override DataSet fillDataset(string spName, string tableName, object[] paramValues) { 
			DataSet ds = new DataSet();
			SqlHelper.FillDataset(base.mConnection, spName, ds, new string[]{tableName}, Mediator.SqlCommandTimeout, paramValues);
			return ds;
		}
		/// <summary></summary>
		protected override bool updateDataset(string spInsert, string spDelete, string spUpdate, DataSet ds, string tableName) {
			SqlConnection oConn=null;
			bool bRet=false;
			try {
				oConn = new SqlConnection(base.mConnection);
				SqlCommand oCMDInsert = createCommand(oConn, spInsert, null);
				SqlCommand oCMDDelete = createCommand(oConn, spDelete, null);
				SqlCommand oCMDUpdate = createCommand(oConn, spUpdate, null);
				oConn.Open();
				SqlHelper.UpdateDataset(oCMDInsert, oCMDDelete, oCMDUpdate, ds, tableName);
				bRet = true;
			}
			catch(Exception ex) { throw ex; }
			finally { if(oConn.State==ConnectionState.Open) oConn.Close(); }
			return bRet;
        }
        /// <summary></summary>
        protected override void getLocalTerminal() {
            //Get the operating enterprise terminal
            try {
                DataSet ds = new DataSet();
                SqlHelper.FillDataset(base.mConnection,USP_LOCALTERMINAL,ds,new string[] { TBL_LOCALTERMINAL },Mediator.SqlCommandTimeout,null);
                if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    base.mTerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    base.mDescription = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception) { }
        }
        #endregion
        #region Local Services: createCommand(), assignParameterValues()
        private SqlCommand createCommand(SqlConnection oConn, string spName, string[]sourceColumns) {
			//This method simplifies the creation of a SQL command object by allowing a stored procedure and 
			//optional parameters to be provided. This method is typically used with UpdateDataset.
			return SqlHelper.CreateCommand(oConn, spName, sourceColumns);
		}
		private void assignParameterValues(SqlParameter[] commandParameters, object[] parameterValues) {
			//Do nothing if we get no data
			if((commandParameters == null) || (parameterValues == null)) 	
				return;
			
			//We must have the same number of values as we pave parameters to put them in
			if(commandParameters.Length != parameterValues.Length) 
				throw new ArgumentException("Parameter count does not match Parameter Value count.");

			//Iterate through the SqlParameters, assigning the values from the corresponding position in the value array
			for(int i=0, j=commandParameters.Length; i<j; i++) {
				//If the current array value derives from IDbDataParameter, then assign its Value property
				if(parameterValues[i] is IDbDataParameter) {
					IDbDataParameter paramInstance = (IDbDataParameter)parameterValues[i];
					if( paramInstance.Value == null ) 
						commandParameters[i].Value = DBNull.Value; 
					else 
						commandParameters[i].Value = paramInstance.Value;
				}
				else if(parameterValues[i] == null) 
					commandParameters[i].Value = DBNull.Value;
				else 
					commandParameters[i].Value = parameterValues[i];
			}
		}
		#endregion
	}
}
