//	File:	globals.cs
//	Author:	J. Heary
//	Date:	08/28/08
//	Desc:	Enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace Argix.Finance {
	//Interfaces
    public struct PayPeriod {
        public string Month;
        public string Year;
    }
	
    //Enumerators
    public enum RateUnit { Day = 1, Mulitrip, Stop, Carton, Pallet }
    
    //Event delegates and args
    public delegate void ErrorEventHandler(object source,ErrorEventArgs e);
    public class ErrorEventArgs :EventArgs {
        private Exception _ex = null;
        public ErrorEventArgs(Exception ex) { this._ex = ex; }
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
   
    //Exceptions
    public class DriverRateException :ApplicationException {
        public DriverRateException() : base() { }
        public DriverRateException(string message) : base(message) { }
        public DriverRateException(string message, Exception ex) : base(message, ex) { }
    }
    public class RateRouteException :ApplicationException {
        public RateRouteException() : base() { }
        public RateRouteException(string message) : base(message) { }
        public RateRouteException(string message,Exception ex) : base(message,ex) { }
    }
}