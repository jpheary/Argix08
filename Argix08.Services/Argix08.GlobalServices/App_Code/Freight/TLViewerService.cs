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

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class TLViewerService: ITLViewerService {
        //Members
        private const string SQL_CONNID = "TLViewer";
        private const string USP_TLVIEW = "uspTLViewer2",TBL_TLVIEW = "TLTable";
        private const string USP_TLDETAIL = "uspTLViewerTLDetailGet",TBL_TLDETAIL = "TLTable";
        private const string USP_AGENTSUMMARY = "uspTLViewerAgentSummary2",TBL_AGENTSUMMARY = "TLTable";

        //Interface
        public TLViewerService() { }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(SQL_CONNID).WriteLogEntry(m);
        }
        public Argix.Enterprise.TerminalInfo GetTerminalInfo() {
            //Get the operating enterprise terminal
            return new Argix.Enterprise.EnterpriseService(SQL_CONNID).GetTerminalInfo();
        }

        public Argix.Enterprise.Terminals GetTerminals() {
            //Returns a list of terminals
            return new Argix.Enterprise.EnterpriseService(SQL_CONNID).GetTerminals();
        }

        public TLs GetTLView(int terminalID) {
            //Get a view of TLs for the specified terminal
            TLs tls=null;
            try {
                tls = new TLs();
                DataSet ds = fillDataset(USP_TLVIEW,TBL_TLVIEW,new object[] { terminalID });
                if(ds != null) {
                    TLViewDS tlDS = new TLViewDS();
                    tlDS.Merge(ds);
                    for(int i=0;i<tlDS.TLTable.Rows.Count;i++) {
                        TL tl = new TL(tlDS.TLTable[i]);
                        tls.Add(tl);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading TL's.",ex); }
            return tls;
        }
        public TLs GetTLDetail(int terminalID,string tlNumber) {
            //Get TL detail for the specified TL#
            TLs tls=null;
            try {
                tls = new TLs();
                DataSet ds = fillDataset(USP_TLDETAIL,TBL_TLDETAIL,new object[] { terminalID,tlNumber });
                if(ds != null) {
                    TLViewDS tlDS = new TLViewDS();
                    tlDS.Merge(ds);
                    for(int i=0;i<tlDS.TLTable.Rows.Count;i++) {
                        TL tl = new TL(tlDS.TLTable[i]);
                        tls.Add(tl);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading TL detail.",ex); }
            return tls;
        }
        public TLs GetAgentSummary(int terminalID) {
            //Get an agent summary view for the specified terminal
            TLs tls=null;
            try {
                tls = new TLs();
                DataSet ds = fillDataset(USP_AGENTSUMMARY,TBL_AGENTSUMMARY,new object[] { terminalID });
                if(ds != null) {
                    TLViewDS tlDS = new TLViewDS();
                    tlDS.Merge(ds);
                    for(int i=0;i<tlDS.TLTable.Rows.Count;i++) {
                        TL tl = new TL(tlDS.TLTable[i]);
                        tls.Add(tl);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading agent summary.",ex); }
            return tls;
        }

        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        private object executeNonQueryWithReturn(string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
                DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
                ret = db.ExecuteNonQuery(cmd);

                //Find the output parameter and return its value
                foreach(DbParameter param in cmd.Parameters) {
                    if((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                        ret = param.Value;
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}
