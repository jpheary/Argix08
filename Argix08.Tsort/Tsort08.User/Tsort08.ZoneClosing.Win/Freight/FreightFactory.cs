using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.Freight {
    //
    public class FreightFactory {
        //Members
        private static ZoneDS _Zones=null,_TLs=null;

        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        public const string USP_LANES = "uspZoneCloseLaneGetList",TBL_LANES = "LaneTable";
        public const string USP_SMALLLANES = "uspZoneCloseLaneSmallSortGetList",TBL_SMALLLANES = "SmallLaneTable";
        public const string USP_ZONES = "uspZoneCloseZoneGetList",TBL_ZONES = "ZoneTable";
        public const string USP_ZONES_ASSIGNABLE = "uspZoneCloseUnassignedFreightGetList",TBL_ZONES_ASSIGNABLE = "ZoneTable";
        public const string USP_TLS_CLOSED = "uspZoneCloseFreightUnassignedClosedGetList",TBL_TLS_CLOSED = "ZoneTable";
        public const string USP_LANE_UPDATE = "uspZoneCloseLaneUpdateTL";
        public const string USP_ZONE_CLOSE = "uspZoneCloseZoneCloseTL";
        
        public static event EventHandler ZonesChanged=null;
        public static event EventHandler TLsChanged=null;

        //Interface
        static FreightFactory() { 
            //Constructor
            _Zones = new ZoneDS();
            _TLs = new ZoneDS();
        }
        private FreightFactory() { }
        public static ZoneDS Zones { get { return _Zones; } }
        public static ZoneDS TLs { get { return _TLs; } }

        public static void RefreshZones(bool assignableOnly) {
            //Update a collection (dataset) of all open TLs for the terminal on the local LAN database
            try {
                _Zones.Clear();
                DataSet ds=null;
                if(assignableOnly)
                    ds = App.Mediator.FillDataset(USP_ZONES_ASSIGNABLE,TBL_ZONES_ASSIGNABLE,null);
                else
                    ds = App.Mediator.FillDataset(USP_ZONES,TBL_ZONES,null);
                if(ds != null) _Zones.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh the list of zones.",ex); }
            finally { if(ZonesChanged != null) ZonesChanged(null,EventArgs.Empty); }
        }
        public static void RefreshTLs() {
            //Update a collection (dataset) of all closed TLs for the terminal on the local LAN database
            try {
                _TLs.Clear();
                DataSet ds = App.Mediator.FillDataset(USP_TLS_CLOSED,TBL_TLS_CLOSED,new object[] { App.Config.ClosedTLsDays });
                if(ds != null) _TLs.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh the list of closed TLs.",ex); }
            finally { if(TLsChanged != null) TLsChanged(null,EventArgs.Empty); }
        }
        public static LaneDS GetLanes() {
            //Get lists of all sort/small sort lanes for this terminal
            LaneDS lanes=null;
            try {
                lanes = new LaneDS();
                DataSet ds = App.Mediator.FillDataset(USP_LANES,TBL_LANES,null);
                if(ds != null) lanes.Merge(ds);
                ds = App.Mediator.FillDataset(USP_SMALLLANES,TBL_SMALLLANES,null);
                if(ds != null) lanes.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Failed to read sort lanes.",ex); }
            return lanes;
        }
        public static bool ChangeLanes(Zone zone) {
            //Update the lane assignments for this zone
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_LANE_UPDATE,new object[] { zone.TL,zone.NewLane,zone.NewSmallSortLane });
            }
            catch(Exception ex) { throw new ApplicationException("Failed to change lane assignments for zone " + zone.ZoneCode + ".",ex); }
            return ret;
        }
        public static bool Close(Zone zone) {
            //Close this zone and update the lane assignments
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_ZONE_CLOSE,new object[] { zone.ZoneCode,zone.TL,null,zone.NewLane,zone.NewSmallSortLane,zone.ClientNumber });
            }
            catch(Exception ex) { throw new ApplicationException("Failed to close zone " + zone.ZoneCode + ".",ex); }
            return ret;
        }
    }
}
