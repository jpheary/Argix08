//	File:	websvcmediator.cs
//	Author:	J. Heary
//	Date:	01/02/08
//	Desc:	Mediates data access with Sql data sources indirectly with the 
//			Microsoft.ApplicationBlocks.Data.SqlHelper class through the 
//			Argix.Data.DataWS.DataAccess web service proxy class.
//			
//			Mediator.WebClientProxyTimeout: controls web client proxy timeout.
//	Rev:	02/25/08 (jph)- Added WebSvcMediator(string) constructor.
//      	09/10/09 (jph)- added getLocalTerminal() override.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace Argix.Data {
	/// <summary></summary>
	public class WebSvcMediator : Mediator {
		//Members
        private Argix.Data.DataAccess mDataSvc = null;
		
        //Interface
		/// <summary></summary>
        public WebSvcMediator() : this(global::Argix.Properties.Settings.Default.DataAccessWS) { }
        /// <summary></summary>
		public WebSvcMediator(string url)  {
			//Constructor
			try {
				//Initialize members; set web service references
                this.mDataSvc = new Argix.Data.DataAccess();
				this.mDataSvc.Credentials = System.Net.CredentialCache.DefaultCredentials;
				this.mDataSvc.Timeout = Mediator.WebClientProxyTimeout;
                this.mDataSvc.Url = url;
				base.mConnection = this.mDataSvc.Url;
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new WebSvcMediator instance.",ex); }
        }
        #region Mediator Overrides
        /// <summary></summary>
        protected override bool online() {
            //Determine online status
            bool online=false;
            try {
                string webserver = "";
                string url = base.mConnection;
                int i = url.IndexOf("http://") + "http://".Length;
                for(int j=i;j<url.Length;j++) {
                    if(url.Substring(j,1) == "/") break;
                    webserver += url.Substring(j,1);
                }
                System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
                if(ping.Send(webserver,Mediator.PingTimeout).Status == System.Net.NetworkInformation.IPStatus.Success)
                    online = true;
            }
            catch { }
            return online;
        }
        /// <summary></summary>
		protected override DataSet executeDataset(string spName, object[] paramValues) { 
			return this.mDataSvc.ExecuteDataset(spName, paramValues);
		}
		/// <summary></summary>
		protected override bool executeNonQuery(string spName, object[] paramValues) { 
			return this.mDataSvc.ExecuteNonQuery(spName, paramValues);
		}
		/// <summary></summary>
		protected override object executeNonQueryWithReturn(string spName, object[] paramValues) { 
			return this.mDataSvc.ExecuteNonQueryWithReturn(spName, paramValues);
		}
		/// <summary></summary>
		protected override SqlDataReader executeReader(string spName, object[] paramValues) {
            throw new NotImplementedException("This method is not supported in a web service.");
        }
		/// <summary></summary>
		protected override object executeScalar(string spName, object[] paramValues) { 
			return this.mDataSvc.ExecuteScalar(spName, paramValues);
		}
		/// <summary></summary>
        protected override System.Xml.XmlReader executeXmlReader(string spName,object[] paramValues) {
            throw new NotImplementedException("This method is not supported in a web service.");
		}
		/// <summary></summary>
		protected override DataSet fillDataset(string spName, string tableName, object[] paramValues) { 
			return this.mDataSvc.FillDatasetWithTimeout(spName, tableName, paramValues);
		}
		/// <summary></summary>
		protected override bool updateDataset(string insertCommand, string deleteCommand, string updateCommand, DataSet ds, string tableName) { 
			return this.mDataSvc.UpdateDataset(insertCommand, deleteCommand, updateCommand, ds, tableName);
		}
        /// <summary></summary>
        protected override void getLocalTerminal() {
            //Get the operating enterprise terminal
            try {
                DataSet ds = this.mDataSvc.FillDataset(SQLMediator.USP_LOCALTERMINAL,SQLMediator.TBL_LOCALTERMINAL,null);
                if(ds!=null && ds.Tables[SQLMediator.TBL_LOCALTERMINAL].Rows.Count > 0) {
                    base.mTerminalID = Convert.ToInt32(ds.Tables[SQLMediator.TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    base.mDescription = ds.Tables[SQLMediator.TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception) { }
        }
        #endregion
	}
}
