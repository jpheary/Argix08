using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Enterprise {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class EnterpriseService:IEnterpriseService {
        //Members
        private string mConnectionID="";
        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "TerminalTable";
        public const string USP_TERMINALS = "uspTerminalsGetList",TBL_TERMINALS = "TerminalTable";

        //Interface
        public EnterpriseService(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
        }
        public TerminalInfo GetTerminalInfo() {
            //Get information about the local terminal for this service
            TerminalInfo info = null;
            try {
                info = new TerminalInfo();
                info.Connection = DatabaseFactory.CreateDatabase(this.mConnectionID).ConnectionStringWithoutCredentials;
                DataSet ds = fillDataset(USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[] { });
                if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                    info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(new ApplicationException("Unexpected error while reading terminal info.",ex))); }
            return info;
        }
        public Terminals GetTerminals() {
            //Returns a list of terminals
            Terminals terminals=null;
            try {
                terminals = new Terminals();
                DataSet ds = fillDataset(USP_TERMINALS,TBL_TERMINALS,new object[]{});
                if(ds!=null && ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                    for(int i=0;i<ds.Tables[TBL_TERMINALS].Rows.Count;i++) {
                        Terminal terminal = new Terminal();
                        terminal.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[i]["TerminalID"]);
                        terminal.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[i]["Number"].ToString().Trim();
                        terminal.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[i]["Description"].ToString().Trim();
                        terminals.Add(terminal);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading terminals.",ex); }
            return terminals;
        }

        #region Data Services: fillDataset()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        #endregion
    }
}
