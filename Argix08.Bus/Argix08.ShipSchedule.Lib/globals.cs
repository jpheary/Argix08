using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix {
	//Global library object
	public class Lib {
		//Members
        public static Mediator Mediator = null;
		
		//Global configuration and database constants		        
        public const string USP_SCHEDULES = "uspShipScdeScheduleGetListForDate",TBL_SCHEDULES = "ShipScheduleViewTable";
		public const string USP_SCHEDULE = "dbo.uspShipScdeScheduleGetList", TBL_SCHEDULE = "ShipScheduleTable";
        public const string USP_TEMPLATES_AVAILABLE = "dbo.uspShipScdeTemplateGetListAvailableForShipSchedule",TBL_TEMPLATES_AVAILABLE = "TemplateViewTable";
	    public const string USP_SCHEDULENEW = "dbo.uspShipScdeScheduleNew";
	    public const string USP_TRIPNEW = "dbo.uspShipScdeTripNew";
	    public const string USP_TRIP = "dbo.uspShipScdeTripGetForCarrierLoad", TBL_TRIP = "DuplicateLoadNumberTable";
	    public const string USP_TRIPUPDATE = "dbo.uspShipScdeTripUpdate";
	    public const string USP_STOPUPDATE = "dbo.uspShipScdeTripStopUpdate";

		//Interface
        static Lib() { }
        ~Lib() { }
    }
	public class DuplicateLoadNumberException : ApplicationException {
		public const string default_Message = "Duplicate load number found for the same carrier within the past and future one week schedule.";
		public DuplicateLoadNumberException():this (DuplicateLoadNumberException.default_Message){}
		public DuplicateLoadNumberException(string message):base(message){}
	}
}