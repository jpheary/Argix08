//	File:	mediator.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Abstract base class with a Microsoft.ApplicationBlocks.Data.SqlHelper 
//			interface for creating concrete mediators to data stores (i.e. database, 
//			web service, file system, etc).
//	Rev:	09/10/09 (jph)- added getLocalTerminal() override.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Argix.Data {
	/// <summary>Abstract base class with a Microsoft.ApplicationBlocks.Data.SqlHelper interface for creating concrete mediators to data stores (i.e. database, web service, file system, etc).</summary>
	public abstract class Mediator  {
		//Members
        /// <summary> </summary>
        protected int mTerminalID=0;
        /// <summary> </summary>
        protected string mDescription="";

		/// <summary>SQL command timeout for every Argix.Data.Mediator instance.</summary>
		public static int SqlCommandTimeout=COMMAND_TIMEOUT_DEFAULT;
		/// <summary>Web client proxy timeout for every Argix.Data.Mediator instance.</summary>
		public static int WebClientProxyTimeout=WEBCLIENTPROXY_TIMEOUT_DEFAULT;
        /// <summary>Ping timeout for every Argix.Data.Mediator instance.</summary>
        public static int PingTimeout=PING_TIMEOUT_DEFAULT;
        /// <summary></summary>
		protected string mConnection="";
		
		/// <summary>Default SQL command timeout = 30sec.</summary>
		public const int COMMAND_TIMEOUT_DEFAULT = 30;
		/// <summary>Infinite SQL command timeout = 0sec.</summary>
		public const int COMMAND_TIMEOUT_INFINITE = 0;
		/// <summary>Default web client proxy timeout = 30000msec.</summary>
		public const int WEBCLIENTPROXY_TIMEOUT_DEFAULT = 30000;
		/// <summary>Infinite web client proxy timeout = 0msec.</summary>
		public const int WEBCLIENTPROXY_TIMEOUT_INFINITE = System.Threading.Timeout.Infinite;
        /// <summary>Default Ping timeout = 100msec.</summary>
        public const int PING_TIMEOUT_DEFAULT = 100;
		
		/// <summary>Occurs whenever a mediator operation completes.</summary>
		public event DataStatusHandler DataStatusUpdate=null;
		
		//Interface
        /// <summary></summary>
        public int TerminalID { get { if(this.mTerminalID == 0 && this.mDescription.Length == 0) getLocalTerminal(); return this.mTerminalID; } }
        /// <summary></summary>
        public string Description { get { if(this.mTerminalID == 0 && this.mDescription.Length == 0) getLocalTerminal(); return this.mDescription; } }
		/// <summary>Gets the online status, which is the result of pinging the connection server.</summary>
        public bool OnLine { get { return online(); } }
		/// <summary>Gets connection information for the data store.</summary>
		public string Connection { get { return this.mConnection; } }
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
		/// the connection string using the provided parameter values. This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <param name="spName">Stored procedure to call.</param>
		/// <param name="tableName">Return dataset table name.</param>
		/// <param name="applicationName">Application name filter of configuration entries.</param>
		/// <param name="userName">User name filter of configuration entries.</param>
		/// <returns>A System.Data.DataSet containing the returned configuration data.</returns>
		public DataSet ReadConfig(string spName, string tableName, string applicationName, string userName) {
			//Returns application configuration data
			DataSet ds=null;
			try {
				//Read database table configuration parameters
				ds = readConfig(spName, tableName, applicationName, userName);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return ds;
		}
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  DataSet ds = ExecuteDataset("GetOrders", new object[]{24, 36});
		/// </remarks>
		/// <param name="spName">The name of the stored procedure</param>
		/// <param name="paramValues">An array of objects to be assigned as the input values of the stored procedure.</param>
		/// <returns>A System.Data.DataSet containing the resultset generated by the command.</returns>
		public DataSet ExecuteDataset(string spName, object[] paramValues) { 
			DataSet ds=null;
			try {
				ds = executeDataset(spName, paramValues);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return ds;
		}
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns no resultset) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// Ensure the stored procedure does not use SET NO COUNT.
		/// e.g.:  
		///  bool res = ExecuteNonQuery("PublishOrders", new object[]{24, 36});
		/// </remarks>
		/// <param name="spName">The name of the stored prcedure.</param>
		/// <param name="paramValues">An array of objects to be assigned as the input values of the stored procedure.</param>
		/// <returns>True if the number of rows affected by the command > 0.</returns>
		public bool ExecuteNonQuery(string spName, object[] paramValues) { 
			bool ret=false;
			try {
				ret = executeNonQuery(spName, paramValues);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return ret;
		}
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a primitive type) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <param name="spName">The name of the stored prcedure.</param>
		/// <param name="paramValues">An array of objects to be assigned as the input values of the stored procedure.</param>
		/// <returns>An object containing the return value.</returns>
		public object ExecuteNonQueryWithReturn(string spName, object[] paramValues) {
			object oRet=null;
			try {
				oRet = executeNonQueryWithReturn(spName, paramValues);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return oRet;
		}
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  SqlDataReader dr = ExecuteReader("GetOrders", new object[]{24, 36});
		/// </remarks>
		/// <param name="spName">The name of the stored procedure.</param>
		/// <param name="paramValues">An array of objects to be assigned as the input values of the stored procedure.</param>
		/// <returns>A System.Data.SqlClient.SqlDataReader containing the resultset generated by the command.</returns>
		public SqlDataReader ExecuteReader(string spName, object[] paramValues) {
			SqlDataReader oReader=null;
			try {
				oReader = executeReader(spName, paramValues);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return oReader;
		}
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a 1x1 resultset) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  int orderCount = (int)ExecuteScalar("GetOrderCount", new object[]{24, 36});
		/// </remarks>
		/// <param name="spName">The name of the stored prcedure.</param>
		/// <param name="paramValues">An array of objects to be assigned as the input values of the stored procedure.</param>
		/// <returns>An object containing the value in the 1x1 resultset generated by the command.</returns>
		public object ExecuteScalar(string spName, object[] paramValues) {
			object o=null;
			try {
				o = executeScalar(spName, paramValues);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return o;
		}
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the specified SqlConnection 
		/// using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  XmlReader r = ExecuteXmlReader("GetOrders", new object[]{24, 36});
		/// </remarks>
		/// <param name="spName">The name of the stored prcedure.</param>
		/// <param name="paramValues">An array of objects to be assigned as the input values of the stored procedure.</param>
		/// <returns>An System.Xml.XmlReader containing the resultset generated by the command.</returns>
        public System.Xml.XmlReader ExecuteXmlReader(string spName,object[] paramValues) {
            System.Xml.XmlReader oReader = null;
			try {
				oReader = executeXmlReader(spName, paramValues);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return oReader;
		}
		/// <summary>
		/// Execute a stored procedure via a SqlCommand (that returns a resultset) against the database specified in 
		/// the connection string using the provided parameter values.  This method will query the database to discover the parameters for the 
		/// stored procedure (the first time each stored procedure is called), and assign the values based on parameter order.
		/// </summary>
		/// <remarks>
		/// This method provides no access to output parameters or the stored procedure's return value parameter.
		/// 
		/// e.g.:  
		///  FillDataset("GetOrders", new object[]{24, 36});
		/// </remarks>
		/// <param name="spName">The name of the stored prcedure.</param>
		/// <param name="tableName">Return dataset table name.</param>
		/// <param name="paramValues">An array of objects to be assigned as the input values of the stored procedure.</param>
		/// <returns>A System.Data.DataSet containing the resultset generated by the command.</returns>
		public DataSet FillDataset(string spName, string tableName, object[] paramValues) {
			DataSet ds=null;
			try {
				ds = fillDataset(spName, tableName, paramValues);
				setStatusNotification(true);
			}
			catch(Exception ex) { handleException(ex); }
			return ds;
		}
		/// <summary>
		/// Executes the respective command for each inserted, updated, or deleted row in the DataSet.
		/// </summary>
		/// <remarks>
		/// e.g.:  
		///  UpdateDataset(insertCommand, deleteCommand, updateCommand, dataSet, "OrderTable");
		/// </remarks>
		/// <param name="spInsert">The name of the stored procedure to insert new records into the data source.</param>
		/// <param name="spDelete">The name of the stored procedure to delete records from the data source.</param>
		/// <param name="spUpdate">The name of the stored procedure used to update records in the data source.</param>
		/// <param name="ds">The DataSet used to update the data source</param>
		/// <param name="tableName">The DataTable used to update the data source.</param>
		/// <returns>True if the update completed successfully.</returns>
		public bool UpdateDataset(string spInsert, string spDelete, string spUpdate, DataSet ds, string tableName) {
			bool ret=false;
			try {
				ret = updateDataset(spInsert, spDelete, spUpdate, ds, tableName);
				setStatusNotification(true);
			}
			catch(Exception ex) { this.handleException(ex); }
			return ret;
		}

        /// <summary></summary>
        protected abstract bool online();
		/// <summary></summary>
		protected virtual DataSet readConfig(string spName, string tableName, string applicationName, string userName) {
			return FillDataset(spName, tableName, new object[]{applicationName,userName});
		}
		/// <summary></summary>
		protected abstract DataSet executeDataset(string spName, object[] paramValues);
		/// <summary></summary>
		protected abstract bool executeNonQuery(string spName, object[] paramValues);
		/// <summary></summary>
		protected abstract object executeNonQueryWithReturn(string spName, object[] paramValues);
		/// <summary></summary>
		protected abstract SqlDataReader executeReader(string spName, object[] paramValues);
		/// <summary></summary>
		protected abstract object executeScalar(string spName, object[] paramValues);
		/// <summary></summary>
        protected abstract System.Xml.XmlReader executeXmlReader(string spName,object[] paramValues);
		/// <summary></summary>
		protected abstract DataSet fillDataset(string spName, string tableName, object[] paramValues);
		/// <summary></summary>
		protected abstract bool updateDataset(string insertCommand, string deleteCommand, string updateCommand, DataSet ds, string tableName);
        /// <summary></summary>
        protected abstract void getLocalTerminal();
        /// <summary></summary>
		protected void handleException(Exception exc) {
			//Take appropriate action for these specific exceptions
			setStatusNotification(false);
			try {
				throw exc;		//Throw to handlers
			}
			catch(System.Data.SqlClient.SqlException ex) { throw new DataAccessException("SQL Server exception --> " + ex.Message, ex); }
			catch(System.Net.WebException ex) { throw new DataAccessException("Web Service exception --> " + ex.Message, ex); }
			catch(System.Web.Services.Protocols.SoapException ex) { throw new DataAccessException("SOAP exception --> " + ex.Message, ex); }
			catch(Exception ex) { throw new ApplicationException("Unexpected exception --> " + ex.Message, ex); }
		}
		/// <summary></summary>
		protected void setStatusNotification(bool onLine) {
			//Send status notification to client listeners; hide secure values in connection string
            string connection = "";
            if(this.mConnection != null && this.mConnection.Length > 0) {
                connection = this.mConnection.Replace("=sa","=**");
                connection = connection.Replace("=objects","=*******");
                connection = connection.Replace("=userid","=******");
                connection = connection.Replace("=password","=********");
            }
			if(this.DataStatusUpdate!=null) this.DataStatusUpdate(this, new DataStatusArgs(onLine, connection));
		}
		
	}
}
