using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;
//using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
//using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Enterprise {
    //
    public class EnterpriseFactory {
        //Members
        public const string USP_TERMINALS = "uspShipScdeTerminalGetList", TBL_TERMINALS = "TerminalTable";
        public const string USP_SHIPPERS = "uspShipScdeShipperGetList", TBL_SHIPPERS = "TerminalTable";
        public const string USP_CARRIERS = "uspShipScdeCarrierGetList", TBL_CARRIERS = "CarrierTable";

        //Interface
        static EnterpriseFactory() { }
        private EnterpriseFactory() { }
        public static DataSet GetTerminals(bool isShipperSchedule) {
            //Get a list of terminals
            DataSet terminals = null;
            try {
                terminals = new DataSet();
                DataSet ds = null;
                if(isShipperSchedule) {
                    ds =  App.Mediator.FillDataset(USP_SHIPPERS,TBL_SHIPPERS,new object[]{});
                    if(ds.Tables[TBL_SHIPPERS].Rows.Count > 0) {
                        terminals.Merge(ds.Tables[TBL_SHIPPERS].Select("", "Description ASC"));
                    }
                }
                else {
                    ds =  App.Mediator.FillDataset(USP_TERMINALS, TBL_TERMINALS, new object[] { });
                    if(ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                        terminals.Merge(ds.Tables[TBL_TERMINALS].Select("", "Description ASC"));
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading terminals.",ex); }
            return terminals;
        }
        public static DataSet GetCarriers() {
            //
            DataSet carriers = null;
            try {
                carriers = new DataSet();
                DataSet ds =  App.Mediator.FillDataset(USP_CARRIERS, TBL_CARRIERS, new object[] { });
                if(ds.Tables[TBL_CARRIERS].Rows.Count > 0) {
                    carriers.Merge(ds.Tables[TBL_CARRIERS].Select("","Description ASC"));
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading carriers.",ex); }
            return carriers;
        }
    }
}
