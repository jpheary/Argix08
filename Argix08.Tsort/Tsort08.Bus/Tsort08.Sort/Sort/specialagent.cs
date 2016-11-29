//	File:	specialagent.cs
//	Author:	J. Heary
//	Date:	05/08/07
//	Desc:	
//	Rev:	09/24/07 (jph)- modified method MakeTrackingNumber() to use UPS check
//							digit calculation CheckDigitMod10_2().
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using Argix;
using Tsort.Enterprise;

namespace Tsort.Sort {
    //
    internal class SpecialAgent {
        //Members
        protected string _id="";
        protected string _client_number="";
        protected string _client_division="";
        protected string _location="";
        protected string _account_number="";
        protected string _san_id="";
        protected string _zone_code="";
        protected string _is_record="";
        protected string _title="";
        protected string _instructions="";
        protected string _indicator="";
        protected string _icon="";

        //Constants
        //Events
        //Interface
        public SpecialAgent() : this(null) { }
        public SpecialAgent(SpecialAgentDS.SpecialAgentTableRow specialagent) {
            //Constructor
            try {
                if(specialagent != null) {
                    this._id = specialagent.ID;
                    this._client_number = specialagent.CLIENT_NUMBER;
                    this._client_division = specialagent.CLIENT_DIVISION;
                    this._location = specialagent.LOCATION;
                    if(!specialagent.IsACCOUNT_NUMBERNull()) this._account_number = specialagent.ACCOUNT_NUMBER;
                    if(!specialagent.IsSAN_IDNull()) this._san_id = specialagent.SAN_ID;
                    if(!specialagent.IsZONE_CODENull()) this._zone_code = specialagent.ZONE_CODE;
                    if(!specialagent.IsIS_RECORDNull()) this._is_record = specialagent.IS_RECORD;
                    if(!specialagent.IsTITLENull()) this._title = specialagent.TITLE;
                    if(!specialagent.IsINSTRUCTIONSNull()) this._instructions = specialagent.INSTRUCTIONS;
                    if(!specialagent.IsINDICATORNull()) this._indicator = specialagent.INDICATOR;
                    if(!specialagent.IsICONNull()) this._icon = specialagent.ICON;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Special Agent instance.",ex); }
        }
        #region Accessors\Modifiers: [Members], ToDataSet()
        public string ID { get { return this._id; } }
        public string CLIENT_NUMBER { get { return this._client_number; } }
        public string CLIENT_DIVISION { get { return this._client_division; } }
        public string LOCATION { get { return this._location; } }
        public string ACCOUNT_NUMBER { get { return this._account_number; } }
        public string SAN_ID { get { return this._san_id; } }
        public string ZONE_CODE { get { return this._zone_code; } }
        public string IS_RECORD { get { return this._is_record; } }
        public string TITLE { get { return this._title; } }
        public string INSTRUCTIONS { get { return this._instructions; } }
        public string INDICATOR { get { return this._indicator; } }
        public string ICON { get { return this._icon; } }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            SpecialAgentDS ds=null;
            try {
                ds = new SpecialAgentDS();
                SpecialAgentDS.SpecialAgentTableRow specialagent = ds.SpecialAgentTable.NewSpecialAgentTableRow();
                specialagent.ID = this._id;
                specialagent.CLIENT_NUMBER = this._client_number;
                specialagent.CLIENT_DIVISION = this._client_division;
                specialagent.LOCATION = this._location;
                if(this._account_number.Length > 0) specialagent.ACCOUNT_NUMBER = this._account_number;
                if(this._san_id.Length > 0) specialagent.SAN_ID = this._san_id;
                if(this._zone_code.Length > 0) specialagent.ZONE_CODE = this._zone_code;
                if(this._is_record.Length > 0) specialagent.IS_RECORD = this._is_record;
                if(this._title.Length > 0) specialagent.TITLE = this._title;
                if(this._instructions.Length > 0) specialagent.INSTRUCTIONS = this._instructions;
                if(this._indicator.Length > 0) specialagent.INDICATOR = this._indicator;
                if(this._icon.Length > 0) specialagent.ICON = this._icon;
                ds.SpecialAgentTable.AddSpecialAgentTableRow(specialagent);
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
        public virtual string Type { get { return "SpecialAgent"; } }
        public virtual string OSTitle { get { return Tsort.Labels.TokenLibrary.OSSERVICETITLE; } }
        public virtual string OSBarcode1DataHumanFormat(string trackingNumber) { return Tsort.Labels.TokenLibrary.OSBARCODE1DATAHUMANFORMAT; }
        public virtual string OSDataIdentifier { get { return Tsort.Labels.TokenLibrary.OSDATAIDENTIFIER; } }
        public virtual string OSBarcode1Data(string trackingNumber) { return Tsort.Labels.TokenLibrary.OSBARCODE1DATA; }
        public virtual string OSRoutingCode(string routingCode) { return Tsort.Labels.TokenLibrary.OSROUTINGCODE; }
        public virtual string OSServiceIndicator { get { return Tsort.Labels.TokenLibrary.OSSERVICEINDICATOR; } }
        public virtual string OSTrackingNumber10(string trackingNumber) { return Tsort.Labels.TokenLibrary.OSTRACKINGNUMBER10; }
        public virtual string OSSenderAccountNumber { get { return Tsort.Labels.TokenLibrary.OSSENDERACCOUNTNUMBER; } }
        public virtual string OSJulianDate { get { return DateTime.Today.DayOfYear.ToString().PadLeft(3,'0'); } }
        public virtual string OSServiceIcon { get { return Tsort.Labels.TokenLibrary.OSSERVICEICON; } }
        public virtual string MakeTrackingNumber(int sequenceNumber) { return ""; }
        public virtual bool IsDefault { get { return true; } }
    }
    internal class StatSampleSpecialAgent:SpecialAgent {
        //Members
        //Interface
        public StatSampleSpecialAgent(SpecialAgentDS.SpecialAgentTableRow specialagent) : base(specialagent) { }
        public override string Type { get { return "StatSampleSpecialAgent"; } }
        public override bool IsDefault { get { return false; } }
    }
    internal class UPSSpecialAgent:SpecialAgent {
        //Members
        //Interface
        public UPSSpecialAgent(SpecialAgentDS.SpecialAgentTableRow specialagent) : base(specialagent) { }
        public override string Type { get { return "UPSSpecialAgent"; } }
        public override string OSTitle { get { return base._title; } }
        public override string OSBarcode1DataHumanFormat(string trackingNumber) {
            //Format tracking number into a human readable form
            string number = trackingNumber.Substring(0,2) + " ";
            number += trackingNumber.Substring(2,3) + " ";
            number += trackingNumber.Substring(5,3) + " ";
            number += trackingNumber.Substring(8,2) + " ";
            number += trackingNumber.Substring(10,4) + " ";
            number += trackingNumber.Substring(14,4) + " ";
            return number;
        }
        public override string OSDataIdentifier { get { return "1Z"; } }
        public override string OSBarcode1Data(string trackingNumber) { return trackingNumber.Substring(2); }
        public override string OSRoutingCode(string routingCode) { return routingCode; }
        public override string OSServiceIndicator { get { return base._indicator; } }
        public override string OSTrackingNumber10(string trackingNumber) { return OSDataIdentifier + trackingNumber.Substring(trackingNumber.Length - 8,8); }
        public override string OSSenderAccountNumber { get { return base._account_number.Trim(); } }
        public override string OSServiceIcon { get { return base._icon; } }
        public override string MakeTrackingNumber(int sequenceNumber) {
            //Make a tracking number from the specified sequence number
            string accNumberinDigits="",trackingNumber="";

            //Convert alpha chars to numeric according to UPS gross ref table
            foreach(char c in base._account_number.Trim()) {
                if(char.IsNumber(c))
                    accNumberinDigits += c;
                else
                    accNumberinDigits += ((Helper.DigitValue(c) + 2) % 10);
            }
            ArgixTrace.WriteLine(new TraceMessage("account#: " + base._account_number.Trim() + ";\taccount# (all numeric): " + accNumberinDigits.ToString(),AppLib.EVENTLOGNAME,LogLevel.Debug,"SpecAgent"));

            //Calculate check digit using UPS Mod 10 calculation
            string checkDigit = Tsort.Enterprise.Helper.CheckDigitMod10_2(accNumberinDigits + base._indicator.Trim() + sequenceNumber.ToString().PadLeft(7,'0'));
            trackingNumber = OSDataIdentifier + base._account_number.Trim() + base._indicator.Trim() + sequenceNumber.ToString().PadLeft(7,'0') + checkDigit;
            return trackingNumber;
        }
        public override bool IsDefault { get { return false; } }
    }
}