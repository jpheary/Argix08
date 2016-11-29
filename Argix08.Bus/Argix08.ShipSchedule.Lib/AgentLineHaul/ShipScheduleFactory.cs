using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Microsoft.ApplicationBlocks.Data;
using Argix.Data;

namespace Argix.AgentLineHaul {
    //
    public class ShipScheduleFactory {
        //Members
        public static Mediator Mediator = null;
        public static int PastBusinessDays = 2;         //Shows closed schedules for this many business days back.
        public static int ScheduleDaysBack = 30;        //Hides open schedules older than this many days
        public static int ScheduleDaysForward = 180;    //Hides open schedules dated beyond this many days
        private static ShipScheduleDS _Schedules=null;

        public static event EventHandler SchedulesChanged=null;

        //Interface
        static ShipScheduleFactory() {
            //Constructor
            _Schedules = new ShipScheduleDS();
        }
        private ShipScheduleFactory() { }
        public static ShipScheduleDS Schedules { get { return _Schedules; } }
        public static void RefreshSchedules() {
            //Update a collection (dataset) of all ship schedules for all terminals
            try {
                //Clear and update ship schedules
                _Schedules.Clear();
                DateTime date=DateTime.Today;
                for(int i=0;i<ShipScheduleFactory.PastBusinessDays;i++) {
                    date = date.AddDays(-1);
                    while(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) {
                        date = date.AddDays(-1);
                    }
                }
                DataSet ds = Mediator.FillDataset(Lib.USP_SCHEDULES,Lib.TBL_SCHEDULES,new object[] { date });
                if(ds.Tables[Lib.TBL_SCHEDULES].Rows.Count > 0) {
                    //Filter out old schedules
                    DateTime dateMin = DateTime.Today.AddDays(-ShipScheduleFactory.ScheduleDaysBack);
                    DateTime dateMax = DateTime.Today.AddDays(ShipScheduleFactory.ScheduleDaysForward);
                    //NOTE: Following statement not working since ported- replaced with next 3 lines
                    //_Schedules.Merge(ds.Tables[Lib.TBL_SCHEDULES].Select("ScheduleDate >= '" + dateMin + "'"), true, MissingSchemaAction.Ignore);
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[Lib.TBL_SCHEDULES].Select("ScheduleDate >= '" + dateMin + "' AND ScheduleDate <= '" + dateMax + "'"));
                    _Schedules.Merge(_ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh ship schedule list.",ex); }
            finally { if(SchedulesChanged != null) SchedulesChanged(null,EventArgs.Empty); }
        }
        public static ShipSchedule SchedulesAdd(long sortCenterID,string sortCenter,DateTime scheduleDate) {
            //Add a new ship schedule
            ShipSchedule schedule=null;
            try {
                //Add and update collection
                ShipScheduleDS ds = new ShipScheduleDS();
                ds.ShipScheduleViewTable.AddShipScheduleViewTableRow("",sortCenterID,sortCenter,scheduleDate,DateTime.Now,Environment.UserName);
                schedule = new ShipSchedule(ds.ShipScheduleViewTable[0],Mediator);
                schedule.Changed += new EventHandler(OnShipScheduleChanged);
                schedule.Create();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to add new ship schedule.",ex); }
            return schedule;
        }
        public static ShipSchedule SchedulesItem(string scheduleID) {
            //Return a new or an existing ship schedule
            ShipSchedule schedule=null;
            try {
                //Existing- import from collection
                DataRow[] rows = _Schedules.ShipScheduleViewTable.Select("ScheduleID='" + scheduleID + "'");
                if(rows.Length > 0) {
                    schedule = new ShipSchedule((ShipScheduleDS.ShipScheduleViewTableRow)rows[0],Mediator);
                    schedule.Changed += new EventHandler(OnShipScheduleChanged);
                }
            }
            catch(Exception ex) { throw ex; }
            return schedule;
        }
        public static ShipSchedule SchedulesItem(long sortCenterID,DateTime date) {
            //Return a new or an existing ship schedule
            ShipSchedule schedule=null;
            try {
                //Existing- import from collection (notice table change)
                DataRow[] rows = _Schedules.ShipScheduleViewTable.Select("SortCenterID=" + sortCenterID + " AND ScheduleDate='" + date + "'");
                if(rows.Length > 0) {
                    schedule = new ShipSchedule((ShipScheduleDS.ShipScheduleViewTableRow)rows[0],Mediator);
                    schedule.Changed += new EventHandler(OnShipScheduleChanged);
                }
            }
            catch(Exception ex) { throw ex; }
            return schedule;
        }
        public static ShipSchedule SchedulesArchiveItem(long sortCenterID,string sortCenter,DateTime scheduleDate) {
            //Return an archived ship schedule
            ShipSchedule schedule=null;
            try {
                //
                DataSet ds = Mediator.FillDataset(Lib.USP_SCHEDULES,Lib.TBL_SCHEDULES,new object[] { scheduleDate });
                if(ds.Tables[Lib.TBL_SCHEDULES].Rows.Count > 0) {
                    DataRow[] rows = ds.Tables[Lib.TBL_SCHEDULES].Select("SortCenterID=" + sortCenterID + " AND ScheduleDate='" + scheduleDate + "'");
                    if(rows.Length > 0) {
                        ShipScheduleDS scheduleDS = new ShipScheduleDS();
                        scheduleDS.ShipScheduleViewTable.AddShipScheduleViewTableRow("",sortCenterID,sortCenter,scheduleDate,DateTime.Now,Environment.UserName);
                        schedule = new ShipSchedule(scheduleDS.ShipScheduleViewTable[0],Mediator);
                        schedule.Changed += new EventHandler(OnShipScheduleChanged);
                    }
                }
            }
            catch(Exception ex) { throw ex; }
            return schedule;
        }
        public static int SchedulesCount { get { return _Schedules.ShipScheduleViewTable.Rows.Count; } }
        private static void OnShipScheduleChanged(object sender,EventArgs e) {
            //Event handler for change in a (child) ship schedule
            try { RefreshSchedules(); } catch(Exception) { }
        }
    }
}
