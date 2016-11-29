//	File:	dataaccess.asmx
//	Author:	J. Heary
//	Date:	05/13/04
//	Desc:	Web service front end to Microsoft Applications block SqlHelper data
//			access class.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ApplicationBlocks.Data;

namespace Argix.Data {
    /// <summary>
    /// Argix Direct data access web service.
    /// </summary>
    [WebService(Namespace = "http://www.argixdirect.com/webservices",Description = "Generic Data Access Web Service")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class DataAccess :System.Web.Services.WebService {
		//Members
		private string mDBConn="";
		
		//Interface
		public DataAccess() {
			//Get connection string from web.config
            this.mDBConn = global::Argix.Data.Properties.Settings.Default.SQLConnection;
		}
		
		[WebMethod(Description="This method returns a DataSet object that contains the resultset returned by a command.")]
		public DataSet ExecuteDataset(string spName, object[] paramValues) {
			//
			DataSet ds=null;
			try {
				ds = SqlHelper.ExecuteDataset(this.mDBConn, spName, paramValues);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::ExecuteDataset();\t",ex); }
			return ds;
		}
		[WebMethod(Description="This method is used to execute commands that do not return any rows or values. They are generally used to perform database updates, but they can also be used to return output parameters from stored procedures.")]
		public bool ExecuteNonQuery(string spName, object[] paramValues) {
			//
			bool bRet=false;
			try {
				int iRet = SqlHelper.ExecuteNonQuery(this.mDBConn, spName, paramValues);
				bRet = (iRet>0);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::ExecuteNonQuery();\t",ex); }
            return bRet;
		}
		[WebMethod(Description="Same as ExecuteNonQuery except will return a single value for an output parameter.")]
		public object ExecuteNonQueryWithReturn(string spName, object[] paramValues) {
			//
			SqlConnection oConn=null;
			object oRet=null;
			try {
				oConn = new SqlConnection(this.mDBConn);
				if(oConn.State == ConnectionState.Closed) oConn.Open();
				if((paramValues != null) && (paramValues.Length > 0)) {
					//Pull the parameters for this stored procedure from the parameter cache (or discover them & populate the cache)
					SqlParameter[] oParams = SqlHelperParameterCache.GetSpParameterSet(this.mDBConn, spName);

					//Assign the provided values to these parameters based on parameter order
					assignParameterValues(oParams, paramValues);

					//Call the overload that takes an array of SqlParameters
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
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::ExecuteNonQueryWithReturn();\t",ex); }
            return oRet;
		}
		[WebMethod(Description="This method returns a single value. The value is always the first column of the first row returned by the command.")]
		public object ExecuteScalar(string spName, object[] paramValues) {
			//
			object o=null;
			try {
				o = SqlHelper.ExecuteScalar(this.mDBConn, spName, paramValues);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::ExecuteScalar();\t",ex); }
            return o;
		}
        [WebMethod(Description="Returns XML in tableName table of an untyped dataset. Provide parameter values in the correct sequence e.g. new object[]{value1,value2}")]
		public DataSet FillData(string spName, string tableName, object[] paramValues) {
			//Backward compatability- use FillDataset() for new stuff
			DataSet ds=null;
			try {
				ds = new DataSet();
				SqlHelper.FillDataset(this.mDBConn, spName, ds, new string[]{tableName}, paramValues);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::FillData();\t",ex); }
            return ds;
		}
        [WebMethod(Description="This method is similar to ExecuteDataset, except that a pre-existing DataSet can be passed in, allowing additional tables to be added.")]
		public DataSet FillDataset(string spName, string tableName, object[] paramValues) {
			//
			DataSet ds=null;
			try {
				ds = new DataSet();
				SqlHelper.FillDataset(this.mDBConn, spName, ds, new string[]{tableName}, paramValues);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::FillDataset();\t",ex); }
            return ds;
		}
        [WebMethod(Description="This method is similar to ExecuteDataset, except that a pre-existing DataSet can be passed in, allowing additional tables to be added. LongQuery increases the query timeout value.")]
		public DataSet FillDatasetWithTimeout(string spName, string tableName, object[] paramValues)  {
			//
			DataSet ds=null;
			int commandTimeout = 0;
			try {
				//get query timeout value from the configuration (web.config)
				if (ConfigurationManager.AppSettings["CommandTimeout"] != null)
					commandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"].ToString());
				ds = new DataSet();
				SqlHelper.FillDataset(this.mDBConn, spName, ds, new string[]{tableName},commandTimeout, paramValues);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::FillDatasetWithTimeout();\t",ex); }
            return ds;
		}
		[WebMethod(Description="This method updates a DataSet using an existing connection and user-specified update commands. It is typically used with CreateCommand.")]
		public bool UpdateDataset(string spInsert, string spDelete, string spUpdate, DataSet ds, string tableName) {
			//
			SqlConnection oConn=null;
			bool bRet=false;
			try {
				oConn = new SqlConnection(this.mDBConn);
				SqlCommand oCMDInsert = createCommand(oConn, spInsert, null);
				SqlCommand oCMDDelete = createCommand(oConn, spDelete, null);
				SqlCommand oCMDUpdate = createCommand(oConn, spUpdate, null);
				oConn.Open();
				SqlHelper.UpdateDataset(oCMDInsert, oCMDDelete, oCMDUpdate, ds, tableName);
				bRet = true;
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error in DataAccess::UpdateDataset();\t",ex); }
            finally { if(oConn.State==ConnectionState.Open) oConn.Close(); }
			return bRet;
        }

        #region Local Services: createCommand(), assignParameterValues()
        private SqlCommand createCommand(SqlConnection oConn, string spName, string[]sourceColumns) {
			//This method simplifies the creation of a SQL command object by allowing a stored procedure and 
			//optional parameters to be provided. This method is typically used with UpdateDataset.
			SqlCommand oCmd = SqlHelper.CreateCommand(oConn, spName, sourceColumns);
			return oCmd;
		}
		private static void assignParameterValues(SqlParameter[] commandParameters, object[] parameterValues) {
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
				else if(parameterValues[i] == null) {
					commandParameters[i].Value = DBNull.Value;
				}
				else {
					commandParameters[i].Value = parameterValues[i];
				}
			}
        }
        #endregion
    }
}
