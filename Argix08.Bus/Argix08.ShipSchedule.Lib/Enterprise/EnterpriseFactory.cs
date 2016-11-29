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
        public static Mediator Mediator = null;
        private string mConnectionID = "";
        public const string USP_TERMINALS = "uspShipScdeTerminalGetList", TBL_TERMINALS = "Selection3Table";
        public const string USP_SHIPPERS = "uspShipScdeShipperGetList", TBL_SHIPPERS = "Selection3Table";
        public const string USP_SHIPPERTERMINALS = "uspShipScdeShipperTerminalGetList", TBL_SHIPPERTERMINALS = "Selection3Table";
        public const string USP_CARRIERS = "uspShipScdeCarrierGetList", TBL_CARRIERS = "Selection3Table";
        public const string USP_AGENTS = "uspShipScdeAgentGetList", TBL_AGENTS = "Selection3Table";

        //Interface
        static EnterpriseFactory() { }
        private EnterpriseFactory(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
        }
        public static SelectionDS GetTerminals(bool isShipperSchedule) {
            //Get a list of terminals
            SelectionDS terminals=null;
            try {
                terminals = new SelectionDS();
                DataSet ds=null;
                DataSet _ds = new DataSet();
                if(isShipperSchedule) {
                    ds = fillDataset(USP_SHIPPERS,TBL_SHIPPERS,new object[]{});
                    if(ds.Tables[TBL_SHIPPERS].Rows.Count > 0) {
                        _ds.Merge(ds.Tables[TBL_SHIPPERS].Select("","Description ASC"));
                        terminals.Merge(_ds);
                    }
                }
                else {
                    ds = fillDataset(USP_TERMINALS, TBL_TERMINALS, new object[] { });
                    if(ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                        _ds.Merge(ds.Tables[TBL_TERMINALS].Select("","Description ASC"));
                        terminals.Merge(_ds);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading terminals.",ex); }
            return terminals;
        }
        public static SelectionDS GetCarriers() {
            //
            SelectionDS carriers=null;
            try {
                carriers = new SelectionDS();
                DataSet ds = fillDataset(USP_CARRIERS, TBL_CARRIERS, new object[] { });
                if(ds.Tables[TBL_CARRIERS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_CARRIERS].Select("","Description ASC"));
                    carriers.Merge(_ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading carriers.",ex); }
            return carriers;
        }
        public static SelectionDS GetAgents() {
            //
            SelectionDS agents=null;
            try {
                agents = new SelectionDS();
                DataSet ds = fillDataset(USP_AGENTS, TBL_AGENTS, new object[] { });
                if(ds.Tables[TBL_AGENTS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_AGENTS].Select("","Description ASC"));
                    agents.Merge(_ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading agents.",ex); }
            return agents;
        }
        public static SelectionDS GetShippers() {
            //
            SelectionDS shippers=null;
            try {
                shippers = new SelectionDS();
                DataSet ds = fillDataset(USP_SHIPPERTERMINALS, TBL_SHIPPERTERMINALS, new object[] { });
                if(ds.Tables[TBL_SHIPPERS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_SHIPPERTERMINALS].Select("","Description ASC"));
                    shippers.Merge(_ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading shippers.",ex); }
            return shippers;
        }
        public static SelectionDS GetDaysOfWeek() {
            SelectionDS dayOfWeek = new SelectionDS();
            dayOfWeek.Selection3Table.AddSelection3TableRow(1,"Mon");
            dayOfWeek.Selection3Table.AddSelection3TableRow(2,"Tue");
            dayOfWeek.Selection3Table.AddSelection3TableRow(3,"Wed");
            dayOfWeek.Selection3Table.AddSelection3TableRow(4,"Thu");
            dayOfWeek.Selection3Table.AddSelection3TableRow(5,"Fri");
            dayOfWeek.Selection3Table.AddSelection3TableRow(6,"Sat");
            dayOfWeek.Selection3Table.AddSelection3TableRow(7,"Sun");
            dayOfWeek.AcceptChanges();
            return dayOfWeek;
        }
        public static int GetWeekday(string weekdayName) {
            int weekday = 0;
            switch(weekdayName.ToLower()) {
                case "mon": weekday = 1; break;
                case "tue": weekday = 2; break;
                case "wed": weekday = 3; break;
                case "thu": weekday = 4; break;
                case "fri": weekday = 5; break;
                case "sat": weekday = 6; break;
                case "sun": weekday = 7; break;
            }
            return weekday;
        }

        #region Data Services: fillDataset()
        private static DataSet fillDataset(string spName, string table, object[] paramValues) {
            //
            //DataSet ds = new DataSet();
            //Database db = DatabaseFactory.CreateDatabase(this.mConnectionID);
            //DbCommand cmd = db.GetStoredProcCommand(spName, paramValues);
            //db.LoadDataSet(cmd, ds, table);
            //return ds;
            return Mediator.FillDataset(spName, table, paramValues);
        }
        #endregion
    }
}
