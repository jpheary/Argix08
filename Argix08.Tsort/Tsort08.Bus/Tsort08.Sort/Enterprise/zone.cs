//	File:	zone.cs
//	Author:	jheary
//	Date:	02/27/07
//	Desc:	Represents state and behavior of an Argix zone.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;

namespace Tsort.Enterprise {
	//
	public class Zone {
		//Members
		private string _code="";
		private string _type="";
		private string _trailer_load_num="";
		private string _label_type="";
		private string _lane="";
		private string _status="";
		private string _rb_tl_number="";
		private int _agentid=0;
		private string _agent_number="";
		private string _description="";
		private string _location="";
		private int _sortcenterid=0;
		private DateTime _ship_date=DateTime.Now;
		private string _can_be_closed="";
		private string _osdstatus="";
		private string _labeltype="";
		private string _lane_small_sort="";
		private short _ismain=0;
		private short _shipscdetoclose=0;
		private DateTime _lastupdated=DateTime.Now;
		private string _userid="";
		
		//Constants
		public const string STATUS_ACTIVE = "A";
		public const string STATUS_INACTIVE = "I";
		
		//Interface
		public Zone(): this(null) { }
		public Zone(ZoneDS.ZoneTableRow zone) { 
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
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating new zone instance.", ex); }
		}
		#region Accessors\Modifiers: [Members]..., ToDataSet()
		public string CODE { get { return this._code; } }
		public string TYPE { get { return this._type; } }
		public string TRAILER_LOAD_NUM { get { return this._trailer_load_num; } }
		public string LABEL_TYPE { get { return this._label_type; } }
		public string LANE { get { return this._lane; } }
		public string STATUS { get { return this._status; } }
		public string RB_TL_NUMBER { get { return this._rb_tl_number; } }
		public int AgentID { get { return this._agentid; } }
		public string AGENT_NUMBER { get { return this._agent_number; } }
		public string DESCRIPTION { get { return this._description; } }
		public string LOCATION { get { return this._location; } }
		public int SortCenterID { get { return this._sortcenterid; } }
		public DateTime SHIP_DATE { get { return this._ship_date; } }
		public string CAN_BE_CLOSED { get { return this._can_be_closed; } }
		public string OSDStatus { get { return this._osdstatus; } }
		public string LABELTYPE { get { return this._labeltype; } }
		public string LANE_SMALL_SORT { get { return this._lane_small_sort; } }
		public short IsMain { get { return this._ismain; } }
		public short ShipScdeToClose { get { return this._shipscdetoclose; } }
		public DateTime LastUpdated { get { return this._lastupdated; } }
		public string UserID { get { return this._userid; } }
		public DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			ZoneDS ds=null;
			try { 
				ds = new ZoneDS();
				ZoneDS.ZoneTableRow zone = ds.ZoneTable.NewZoneTableRow();
				zone.CODE = this._code;
				zone.TYPE = this._type;
				zone.TRAILER_LOAD_NUM = this._trailer_load_num;
				zone.LABEL_TYPE = this._label_type;
				zone.LANE = this._lane;
				zone.STATUS = this._status;
				zone.RB_TL_NUMBER = this._rb_tl_number;
				zone.AgentID = this._agentid;
				zone.AGENT_NUMBER = this._agent_number;
				zone.DESCRIPTION = this._description;
				zone.LOCATION = this._location;
				zone.SortCenterID = this._sortcenterid;
				zone.SHIP_DATE = this._ship_date;
				zone.CAN_BE_CLOSED = this._can_be_closed;
				zone.OSDStatus = this._osdstatus;
				zone.LABELTYPE = this._labeltype;
				zone.LANE_SMALL_SORT = this._lane_small_sort;
				zone.IsMain = this._ismain;
				zone.ShipScdeToClose = this._shipscdetoclose;
				zone.LastUpdated = this._lastupdated;
				zone.UserID = this._userid;
				ds.ZoneTable.AddZoneTableRow(zone);
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		public bool IsActive { get { return (this._status.ToUpper() == STATUS_ACTIVE); } }
	}
}