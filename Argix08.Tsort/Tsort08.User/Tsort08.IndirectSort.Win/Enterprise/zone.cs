using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace Argix.Enterprise {
	//
	public class Zone {
		//Members
		private string _code="", _type="";
		private string _trailer_load_num="", _label_type="";
		private string _lane="", _status="", _rb_tl_number="";
		private int _agentid=0;
		private string _agent_number="", _description="", _location="";
		private int _sortcenterid=0;
		private DateTime _ship_date=DateTime.Now;
		private string _can_be_closed="", _osdstatus="";
		private string _labeltype="", _lane_small_sort="";
        private short _ismain=0, _shipscdetoclose=0;
		private DateTime _lastupdated=DateTime.Now;
		private string _userid="";
		
		public const string ZONE_STATUS_ACTIVE = "A";
		public const string ZONE_STATUS_INACTIVE = "I";
		
		//Interface
		public Zone(): this(null) { }
		public Zone(ZoneDS.ZoneDetailTableRow zone) { 
			//Constructor
			try { 
				if(zone != null) { 
					this._code = zone.CODE;
					this._type = zone.TYPE;
					if(!zone.IsTRAILER_LOAD_NUMNull()) this._trailer_load_num = zone.TRAILER_LOAD_NUM;
					if(!zone.IsLABEL_TYPENull()) this._label_type = zone.LABEL_TYPE;
					if(!zone.IsLANENull()) this._lane = zone.LANE;
					if(!zone.IsSTATUSNull()) this._status = zone.STATUS;
					if(!zone.IsRB_TL_NUMBERNull()) this._rb_tl_number = zone.RB_TL_NUMBER;
					if(!zone.IsAgentIDNull()) this._agentid = zone.AgentID;
					if(!zone.IsAGENT_NUMBERNull()) this._agent_number = zone.AGENT_NUMBER;
					if(!zone.IsDESCRIPTIONNull()) this._description = zone.DESCRIPTION;
					if(!zone.IsLOCATIONNull()) this._location = zone.LOCATION;
					if(!zone.IsSortCenterIDNull()) this._sortcenterid = zone.SortCenterID;
					if(!zone.IsSHIP_DATENull()) this._ship_date = zone.SHIP_DATE;
					if(!zone.IsCAN_BE_CLOSEDNull()) this._can_be_closed = zone.CAN_BE_CLOSED;
					if(!zone.IsOSDStatusNull()) this._osdstatus = zone.OSDStatus;
					if(!zone.IsLABELTYPENull()) this._labeltype = zone.LABELTYPE;
					if(!zone.IsLANE_SMALL_SORTNull()) this._lane_small_sort = zone.LANE_SMALL_SORT;
					if(!zone.IsIsMainNull()) this._ismain = zone.IsMain;
					if(!zone.IsShipScdeToCloseNull()) this._shipscdetoclose = zone.ShipScdeToClose;
					if(!zone.IsLastUpdatedNull()) this._lastupdated = zone.LastUpdated;
					if(!zone.IsUserIDNull()) this._userid = zone.UserID;
				}
			}
			catch(Exception ex) { throw ex; }
		}
		public Zone(string code, string type) {
			//Constructor
			try { 
				this._code = code;
				this._type = type;
				//...
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers: [Members...]
		public string Code { get { return this._code; } set { this._code = value; } }
		public string Type { get { return this._type; } set { this._type = value; } }
		public string TrailerLoadNumber { get { return this._trailer_load_num; } set { this._trailer_load_num = value; } }
		public string Label_Type { get { return this._label_type; } set { this._label_type = value; } }
		public string Lane { get { return this._lane; } set { this._lane = value; } }
		public string Status { get { return this._status; } set { this._status = value; } }
		public string RollbackTLNumber { get { return this._rb_tl_number; } set { this._rb_tl_number = value; } }
        public int AgentID { get { return this._agentid; } set { this._agentid = value; } }
		public string AgentNumber { get { return this._agent_number; } set { this._agent_number = value; } }
		public string Description { get { return this._description; } set { this._description = value; } }
		public string Location { get { return this._location; } set { this._location = value; } }
		public int SortCenterID { get { return this._sortcenterid; } set { this._sortcenterid = value; } }
		public DateTime ShipDate { get { return this._ship_date; } set { this._ship_date = value; } }
		public string CanBeClosed { get { return this._can_be_closed; } set { this._can_be_closed = value; } }
		public string OSDStatus { get { return this._osdstatus; } set { this._osdstatus = value; } }
		public string LabelType { get { return this._labeltype; } set { this._labeltype = value; } }
		public string SmallSortLane { get { return this._lane_small_sort; } set { this._lane_small_sort = value; } }
		public short IsMain { get { return this._ismain; } set { this._ismain = value; } }
        public short ShipScheduleToClose { get { return this._shipscdetoclose; } set { this._shipscdetoclose = value; } }
		public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
		public string UserID { get { return this._userid; } set { this._userid = value; } }
		#endregion
		public bool IsActive { get { return (this._status.ToUpper() == ZONE_STATUS_ACTIVE); } }
	}
}