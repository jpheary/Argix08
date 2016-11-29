//	File:	globals.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Globals consumed and exposed by this component.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Argix {
	//Argix namespace
	
	namespace Data {
		//Argix.Data namespace
		//Event support
		/// <summary>Defines an event type that returns data connection and status information.</summary>
		public delegate void DataStatusHandler(object source, DataStatusArgs e);
		/// <summary>Provides data for events of type Argix.Data.DataStatusHandler.</summary>
		public class DataStatusArgs : EventArgs {
			private bool _online=false;
			private string _connection="";
			/// <summary>Creates a new instance.</summary>
			/// <param name="online">True if the connection is online.</param>
			/// <param name="connection">Information about the data connection.</param>
			public DataStatusArgs(bool online, string connection) {
				this._online = online;
				this._connection = connection;
			}
			/// <summary>Gets the online status.</summary>
			public bool Online { get { return this._online; } }
			/// <summary>Gets information about the data the connection.</summary>
			public string Connection { get { return this._connection; } }
		}
		
		//Custom Exceptions
		/// <summary>The exception thrown when a data communication exception (i.e. System.Data.SqlClient.SqlException, System.Net.WebException, System.Web.Services.Protocols.SoapException) occurrs.</summary>
		public class DataAccessException : ApplicationException {
			/// <summary>Create a new instance.</summary>
			public DataAccessException() : base() { }
			/// <summary>Create a new instance.</summary>
			/// <param name="message">Exception message.</param>
			public DataAccessException(string message) : base(message) {}
			/// <summary>Create a new instance.</summary>
			/// <param name="message">Exception message.</param>
			/// <param name="innerExc">Inner exception.</param>
			public DataAccessException(string message, Exception innerExc) : base(message, innerExc) {}
		}
	}
}
