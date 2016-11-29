//	File:	filemediator.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Mediates data access with xml file sources.
//			File directory:	represented by base.mConnection
//			File name:		represented by stored procedure names (i.e. spName.xml)
//	Rev:	02/25/08 (jph)- changed configuration from AppSettings to Settings.Default.
//	    	09/10/09 (jph)- added getLocalTerminal() override.
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Argix.Data {
	/// <summary>
	/// Mediates data access with xml file sources. 
	///	  File directory:	represented by base.mConnection; 
	///	  File name:		represented by stored procedure names (i.e. spName.xml)
	/// </summary>
	public class FileMediator : Mediator {
		//Members
		
        //Interface
		/// <summary></summary>
        public FileMediator() : this(global::Argix.Properties.Settings.Default.FileStore) { }
		/// <summary></summary>
		/// <param name="fileStore"></param>
		public FileMediator(string fileStore) {
			//Constructor
			try {
				//Initialize members; set data connection
				base.mConnection = fileStore;
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new FileMediator instance.",ex); }
        }
        #region Mediator Overrides
        /// <summary></summary>
        protected override bool online() { return true; }
        /// <summary></summary>
		/// <param name="spName"></param>
		/// <param name="tableName"></param>
		/// <param name="applicationName"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
        protected override DataSet readConfig(string spName,string tableName,string applicationName,string userName) {
			DataSet ds = new DataSet();
            ds.ReadXml(base.mConnection + spName + ".xml");
			return ds;
		}
		/// <summary></summary>
		protected override DataSet executeDataset(string spName, object[] paramValues) { 
			DataSet ds = new DataSet();
			ds.ReadXml(base.mConnection + spName + ".xml");
			return ds;
		}
		/// <summary></summary>
		protected override bool executeNonQuery(string spName, object[] paramValues) { 
			DataSet ds = (DataSet)paramValues[0];
			ds.WriteXml(base.mConnection + spName + ".xml", XmlWriteMode.WriteSchema);
			return true;
		}
		/// <summary></summary>
		protected override object executeNonQueryWithReturn(string spName, object[] paramValues) { throw new ApplicationException("Unsupported"); }
		/// <summary></summary>
		protected override SqlDataReader executeReader(string spName, object[] paramValues) { throw new ApplicationException("Unsupported"); }
		/// <summary></summary>
		protected override object executeScalar(string spName, object[] paramValues) { throw new ApplicationException("Unsupported"); }
		/// <summary></summary>
        protected override System.Xml.XmlReader executeXmlReader(string spName,object[] paramValues) { throw new ApplicationException("Unsupported"); }
		/// <summary></summary>
		protected override DataSet fillDataset(string spName, string tableName, object[] paramValues) { 
			DataSet ds = new DataSet();
			ds.ReadXml(base.mConnection + spName + ".xml");
			return ds;
		}
		/// <summary></summary>
		protected override bool updateDataset(string spInsert, string spDelete, string spUpdate, DataSet ds, string tableName) { throw new ApplicationException("Unsupported"); }
        /// <summary></summary>
        protected override void getLocalTerminal() {
            //Get the operating enterprise terminal
            try {
                base.mTerminalID = 0;
                base.mDescription = Environment.MachineName;
            }
            catch(Exception) { }
        }
        #endregion
    }
}
