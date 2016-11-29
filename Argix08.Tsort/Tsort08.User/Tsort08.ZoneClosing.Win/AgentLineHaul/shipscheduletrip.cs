using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using Argix.Data;

namespace Argix.AgentLineHaul {
	//
	public class ShipScheduleTrip {
		//Members
		private string _scheduleid="";
		private long _sortcenterid=0;
		private string _sortcenter="";
		private DateTime _scheduledate=DateTime.Now;
		private string _tripid="", _templateid="";
		private int _bolnumber=0;
		private long _carrierserviceid=0;
		private string _carrier="", _loadnumber="";
		private int _trailerid=0;
		private string _trailernumber="", _tractornumber="";
		private DateTime _scheduledclose=DateTime.MinValue, _scheduleddeparture=DateTime.MinValue;
		private byte _ismandatory=1;
		private DateTime _freightassigned=DateTime.MinValue, _trailercomplete=DateTime.MinValue;
		private DateTime _paperworkcomplete=DateTime.MinValue, _trailerdispatched=DateTime.MinValue;
		private DateTime _canceled=DateTime.MinValue;
		private string _scdeuserid="",_scderowversion="";
		private DateTime _scdelastupdated=DateTime.MinValue;
		private string _stopid="", _stopnumber="";
		private long _agentterminalid=0;
		private string _agentnumber="", _mainzone="";
		private string _tag="", _notes="";
		private DateTime _scheduledarrival=DateTime.MinValue, _scheduledofd1=DateTime.MinValue;
        private string _s1userid="",_s1rowversion="";
		private DateTime _s1lastupdated=DateTime.Now;
		private string _s2stopid="", _s2stopnumber="";
		private long _s2agentterminalid=0;
		private string _s2agentnumber="", _s2mainzone="", _s2tag="", _s2notes="";
		private DateTime _s2scheduledarrival=DateTime.MinValue, _s2scheduledofd1=DateTime.MinValue;
        private string _s2userid="",_s2rowversion="";
		private DateTime _s2lastupdated=DateTime.Now;
		private string _nextcarrier="";
		private int _carrierid=0;
		private ShipScheduleDS mAssignedTLs=null;
       		
		//Interface
		public ShipScheduleTrip(): this(null) { }
		public ShipScheduleTrip(ShipScheduleDS.ShipScheduleMasterTableRow trip): this(trip, null) { }
		public ShipScheduleTrip(ShipScheduleDS.ShipScheduleMasterTableRow trip, ShipScheduleDS.ShipScheduleDetailTableRow[] tls) { 
			//Constructor
			try { 
				this.mAssignedTLs = new ShipScheduleDS();
				if(trip != null) { 
					this._scheduleid = trip.ScheduleID;
					this._sortcenterid = trip.SortCenterID;
					if(!trip.IsSortCenterNull()) this._sortcenter = trip.SortCenter;
					this._scheduledate = trip.ScheduleDate;
					this._tripid = trip.TripID;
					if(!trip.IsTemplateIDNull()) this._templateid = trip.TemplateID;
					if(!trip.IsBolNumberNull()) this._bolnumber = trip.BolNumber;
					if(!trip.IsCarrierServiceIDNull()) this._carrierserviceid = trip.CarrierServiceID;
					if(!trip.IsCarrierNull()) this._carrier = trip.Carrier;
					if(!trip.IsLoadNumberNull()) this._loadnumber = trip.LoadNumber;
					if(!trip.IsTrailerIDNull()) this._trailerid = trip.TrailerID;
					if(!trip.IsTrailerNumberNull()) this._trailernumber = trip.TrailerNumber;
					if(!trip.IsTractorNumberNull()) this._tractornumber = trip.TractorNumber;
					if(!trip.IsScheduledCloseNull()) this._scheduledclose = trip.ScheduledClose;
					if(!trip.IsScheduledDepartureNull()) this._scheduleddeparture = trip.ScheduledDeparture;
					if(!trip.IsIsMandatoryNull()) this._ismandatory = trip.IsMandatory;
					if(!trip.IsFreightAssignedNull()) this._freightassigned = trip.FreightAssigned;
					if(!trip.IsTrailerCompleteNull()) this._trailercomplete = trip.TrailerComplete;
					if(!trip.IsPaperworkCompleteNull()) this._paperworkcomplete = trip.PaperworkComplete;
					if(!trip.IsTrailerDispatchedNull()) this._trailerdispatched = trip.TrailerDispatched;
					if(!trip.IsCanceledNull()) this._canceled = trip.Canceled;
					if(!trip.IsSCDEUserIDNull()) this._scdeuserid = trip.SCDEUserID;
					if(!trip.IsSCDELastUpdatedNull()) this._scdelastupdated = trip.SCDELastUpdated;
					if(!trip.IsSCDERowVersionNull()) this._scderowversion = trip.SCDERowVersion;
					if(!trip.IsStopIDNull()) this._stopid = trip.StopID;
					if(!trip.IsStopNumberNull()) this._stopnumber = trip.StopNumber;
					if(!trip.IsAgentTerminalIDNull()) this._agentterminalid = trip.AgentTerminalID;
					if(!trip.IsAgentNumberNull()) this._agentnumber = trip.AgentNumber;
					if(!trip.IsMainZoneNull()) this._mainzone = trip.MainZone;
					if(!trip.IsTagNull()) this._tag = trip.Tag;
					if(!trip.IsNotesNull()) this._notes = trip.Notes;
					if(!trip.IsScheduledArrivalNull()) this._scheduledarrival = trip.ScheduledArrival;
					if(!trip.IsScheduledOFD1Null()) this._scheduledofd1 = trip.ScheduledOFD1;
					if(!trip.IsS1UserIDNull()) this._s1userid = trip.S1UserID;
					if(!trip.IsS1LastUpdatedNull()) this._s1lastupdated = trip.S1LastUpdated;
					if(!trip.IsS1RowVersionNull()) this._s1rowversion = trip.S1RowVersion;
					if(!trip.IsS2StopIDNull()) this._s2stopid = trip.S2StopID;
					if(!trip.IsS2StopNumberNull()) this._s2stopnumber = trip.S2StopNumber;
					if(!trip.IsS2AgentTerminalIDNull()) this._s2agentterminalid = trip.S2AgentTerminalID;
					if(!trip.IsS2AgentNumberNull()) this._s2agentnumber = trip.S2AgentNumber;
					if(!trip.IsS2MainZoneNull()) this._s2mainzone = trip.S2MainZone;
					if(!trip.IsS2TagNull()) this._s2tag = trip.S2Tag;
					if(!trip.IsS2NotesNull()) this._s2notes = trip.S2Notes;
					if(!trip.IsS2ScheduledArrivalNull()) this._s2scheduledarrival = trip.S2ScheduledArrival;
					if(!trip.IsS2ScheduledOFD1Null()) this._s2scheduledofd1 = trip.S2ScheduledOFD1;
					if(!trip.IsS2UserIDNull()) this._s2userid = trip.S2UserID;
					if(!trip.IsS2LastUpdatedNull()) this._s2lastupdated = trip.S2LastUpdated;
					if(!trip.IsS2RowVersionNull()) this._s2rowversion = trip.S2RowVersion;
					if(!trip.IsNextCarrierNull()) this._nextcarrier = trip.NextCarrier;
					if(!trip.IsCarrierIDNull()) this._carrierid = trip.CarrierID;
					if(tls != null) {
						ShipScheduleDS.ShipScheduleMasterTableRow _trip = this.mAssignedTLs.ShipScheduleMasterTable.NewShipScheduleMasterTableRow();
						_trip.ScheduleID = this.ScheduleID;
						_trip.SortCenterID = this.SortCenterID;
						_trip.TripID = this.TripID;
						this.mAssignedTLs.ShipScheduleMasterTable.AddShipScheduleMasterTableRow(_trip);
						this.mAssignedTLs.Merge(tls);
					}
				}
			}
			catch(Exception ex) { throw new ApplicationException("Could not create a new ship schedule trip.", ex); }
		}
		#region Accessors\Modifiers: [Members]..., AssignedTLs
		public string ScheduleID {  get { return this._scheduleid; } }
		public long SortCenterID { get { return this._sortcenterid; } }
		public string SortCenter { get { return this._sortcenter; } }
		public DateTime ScheduleDate { get { return this._scheduledate; } }
		public string TripID { get { return this._tripid; } }
		public string TemplateID { get { return this._templateid; } }
		public int BolNumber { get { return this._bolnumber; } }
		public long CarrierServiceID { get { return this._carrierserviceid; } }
		public string Carrier { get { return this._carrier; } }
		public string LoadNumber { get { return this._loadnumber; } }
		public int TrailerID { get { return this._trailerid; } }
		public string TrailerNumber { get { return this._trailernumber; } }
		public string TractorNumber { get { return this._tractornumber; } }
		public DateTime ScheduledClose { get { return this._scheduledclose; } }
		public DateTime ScheduledDeparture { get { return this._scheduleddeparture; } }
		public byte IsMandatory { get { return this._ismandatory; } }
		public DateTime FreightAssigned { get { return this._freightassigned; } }
		public DateTime TrailerComplete { get { return this._trailercomplete; } }
		public DateTime PaperworkComplete { get { return this._paperworkcomplete; } }
		public DateTime TrailerDispatched { get { return this._trailerdispatched; } }
		public DateTime Canceled { get { return this._canceled; } }
		public string SCDEUserID { get { return this._scdeuserid; } }
		public DateTime SCDELastUpdated { get { return this._scdelastupdated; } }
		public string SCDERowVersion { get { return this._scderowversion; } }
		public string StopID { get { return this._stopid; } }
		public string StopNumber { get { return this._stopnumber; } }
		public long AgentTerminalID { get { return this._agentterminalid; } }
		public string AgentNumber { get { return this._agentnumber; } }
		public string MainZone { get { return this._mainzone; } }
		public string Tag { get { return this._tag; } }
		public string Notes { get { return this._notes; } }
		public DateTime ScheduledArrival { get { return this._scheduledarrival; } }
		public DateTime ScheduledOFD1 { get { return this._scheduledofd1; } }
		public string S1UserID { get { return this._s1userid; } }
		public DateTime S1LastUpdated { get { return this._s1lastupdated; } }
		public string S1RowVersion { get { return this._s1rowversion; } }
		public string S2StopID { get { return this._s2stopid; } }
		public string S2StopNumber { get { return this._s2stopnumber; } }
		public long S2AgentTerminalID { get { return this._s2agentterminalid; } }
		public string S2AgentNumber { get { return this._s2agentnumber; } }
		public string S2MainZone { get { return this._s2mainzone; } }
		public string S2Tag { get { return this._s2tag; } }
		public string S2Notes { get { return this._s2notes; } }
		public DateTime S2ScheduledArrival { get { return this._s2scheduledarrival; } }
		public DateTime S2ScheduledOFD1 { get { return this._s2scheduledofd1; } }
		public string S2UserID { get { return this._s2userid; } }
		public DateTime S2LastUpdated { get { return this._s2lastupdated; } }
		public string S2RowVersion { get { return this._s2rowversion; } }
		public string NextCarrier { get { return this._nextcarrier; } }
		public int CarrierID { get { return this._carrierid; } }
		public ShipScheduleDS AssignedTLs { get { return this.mAssignedTLs; } }
		#endregion
		public bool IsOpen { get { return (DateTime.Compare(this._freightassigned, DateTime.MinValue) == 0); } }
		public bool IsComplete { get { return (DateTime.Compare(this._trailercomplete, DateTime.MinValue) > 0); } }
		public bool AllTLsClosed { 
			get { 
				bool anyOpen=false;
				for(int i=0; i<this.mAssignedTLs.ShipScheduleDetailTable.Rows.Count; i++) {
					if(this.mAssignedTLs.ShipScheduleDetailTable[i].IsCloseDateNull()) {
						anyOpen = true;
						break;
					}
				}
				return !anyOpen; 
			} 
		}
	}
}
