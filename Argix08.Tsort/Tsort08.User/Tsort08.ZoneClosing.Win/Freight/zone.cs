using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace Argix.Freight {
	//
	public class Zone {
		//Members
		private string _zone="",_tl="";
        private string _clientnumber="",_clientname="";
		private string _newlane="", _lane="", _newsmallsortlane="", _smallsortlane="";
		private string _description="", _type="", _typeid="";
		private string _status="", _rollbacktl="";
		private int _isexclusive=0;
		private string _can_be_closed="T", _assignedtoshipscde="";
		
		public const string NEWTL_ERROR="Error";
				
		//Interface
		public Zone(ZoneDS.ZoneTableRow zone) { 
			//Constructor
			try { 
				if(zone != null) { 
					this._zone = zone.Zone;
					if(!zone.Is_TL_Null()) this._tl = zone._TL_;
                    if(!zone.IsClientNumberNull()) this._clientnumber = zone.ClientNumber;
                    if(!zone.IsClientNameNull()) this._clientname = zone.ClientName;
                    if(!zone.IsNewLaneNull()) this._newlane = zone.NewLane;
					if(!zone.IsLaneNull()) this._lane = zone.Lane;
					if(!zone.IsNewSmallSortLaneNull()) this._newsmallsortlane = zone.NewSmallSortLane;
					if(!zone.IsSmallSortLaneNull()) this._smallsortlane = zone.SmallSortLane;
					if(!zone.IsDescriptionNull()) this._description = zone.Description;
					if(!zone.IsTypeNull()) this._type = zone.Type;
					if(!zone.IsTypeIDNull()) this._typeid = zone.TypeID;
					if(!zone.IsStatusNull()) this._status = zone.Status;
					if(!zone.Is_RollbackTL_Null()) this._rollbacktl = zone._RollbackTL_;
					if(!zone.IsIsExclusiveNull()) this._isexclusive = zone.IsExclusive;
					if(!zone.IsCAN_BE_CLOSEDNull()) this._can_be_closed = zone.CAN_BE_CLOSED;
					if(!zone.IsAssignedToShipScdeNull()) this._assignedtoshipscde = zone.AssignedToShipScde;
				}
			}
			catch(Exception ex) { throw new ApplicationException("Could not create a new zone.", ex); }
		}
		#region Accessors\Modifiers: [Members...]
		public string ZoneCode { get { return this._zone; } }
        public string TL { get { return this._tl; } set { this._tl = value; } }
        public string ClientNumber { get { return this._clientnumber; } set { this._clientnumber = value; } }
        public string ClientName { get { return this._clientname; } set { this._clientname = value; } }
		public string NewLane { get { return this._newlane; } set { this._newlane = value; } }
		public string Lane { get { return this._lane; } set { this._lane = value; } }
		public string NewSmallSortLane { get { return this._newsmallsortlane; } set { this._newsmallsortlane = value; } }
		public string SmallSortLane { get { return this._smallsortlane; } set { this._smallsortlane = value; } }
		public string Description { get { return this._description; } set { this._description = value; } }
		public string Type { get { return this._type; } set { this._type = value; } }
		public string TypeID { get { return this._typeid; } set { this._typeid = value; } }
		public string Status { get { return this._status; } set { this._status = value; } }
		public string RollbackTL { get { return this._rollbacktl; } set { this._rollbacktl = value; } }
		public int IsExclusive { get { return this._isexclusive; } set { this._isexclusive = value; } }
		public string CanBeClosed { get { return this._can_be_closed; } set { this._can_be_closed = value; } }
		public string AssignedToShipScde { get { return this._assignedtoshipscde; } set { this._assignedtoshipscde = value; } }
		#endregion
	}
}