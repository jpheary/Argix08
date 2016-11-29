//	File:	ratingfactory.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Factory for creating Driver Rating objects and accessing database services.
//	Rev:	02/26/09 (jph)- revised methods CreateUnitRate(), UpdateUnitRate(), 
//                          CreateUnitRouteRate(), and UpdateUnitRouteRate() to add new 
//                          parameters maxAmt, maxTrigFld, maxTrigVal
//                  NOTE:   The corresponding stored procedures have not been revised
//                          to accept these new params, so they are unused at present.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Finance {
	//
	public class DriverRatingService {
		//Members
        private string mConnectionID="";
        private DriverRatesDS _RateTypes = null;
        
        public const string USP_RATETYPE = "uspDCRateTypeGetList",TBL_RATETYPE = "RateTypeTable";
        public const string USP_PAYPERIOD = "uspDCPayPeriodGet",TBL_PAYPERIOD = "PayPeriodTable";
        
        public const string USP_RATEMILEAGE = "uspDCRateMilageGetList",TBL_RATEMILEAGE = "RateMileageTable";
        public const string USP_RATEMILEAGENEW = "uspDCRateMilageNew";
        public const string USP_RATEMILEAGEUPDATE = "uspDCRateMilageUpdate";
        public const string USP_RATEUNIT = "uspDCRateUnitGetList",TBL_RATEUNIT = "RateUnitTable";
        public const string USP_RATEUNITNEW = "uspDCRateUnitNew";
        public const string USP_RATEUNITUPDATE = "uspDCRateUnitUpdate";
        public const string USP_RATEMILEAGEROUTE = "uspDCRateMilageRouteGetList",TBL_RATEMILEAGEROUTE = "RateMileageRouteTable";
        public const string USP_RATEMILEAGEROUTENEW = "uspDCRateMilageRouteNew";
        public const string USP_RATEMILEAGEROUTEUPDATE = "uspDCRateMilageRouteUpdate";
        public const string USP_RATEUNITROUTE = "uspDCRateUnitRouteGetList",TBL_RATEUNITROUTE = "RateUnitRouteTable";
        public const string USP_RATEUNITROUTENEW = "uspDCRateUnitRouteNew";
        public const string USP_RATEUNITROUTEUPDATE = "uspDCRateUnitRouteUpdate";

        //Interface
        public DriverRatingService(string connectionID) { 
            //Constructor
            this.mConnectionID = connectionID;
        }
        
        public DriverRatesDS RateTypes { 
            get {
                try {
                    if(_RateTypes == null) {
                        _RateTypes = new DriverRatesDS();
                        DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_RATETYPE,TBL_RATETYPE,new object[] { });
                        if(ds.Tables[TBL_RATETYPE].Rows.Count > 0)
                            _RateTypes.Merge(ds);
                    }
                }
                catch(ApplicationException ex) { throw ex; }
                catch(Exception ex) { _RateTypes = null;  throw new ApplicationException("Unexpected error while reading rate types.",ex); }
                return _RateTypes; 
            } 
        }
        public PayPeriod GetPayPeriod(DateTime date) {
            //Get the Argix pay period for the specified date
            PayPeriod pp = new PayPeriod();
            try {
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_PAYPERIOD,TBL_PAYPERIOD,new object[] { date });
                if(ds.Tables[TBL_PAYPERIOD].Rows.Count > 0) {
                    pp.Month = ds.Tables[TBL_PAYPERIOD].Rows[0]["Month"].ToString().PadLeft(2, '0');
                    pp.Year = ds.Tables[TBL_PAYPERIOD].Rows[0]["Year"].ToString();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading Argix pay periods.",ex); }
            return pp;
        }
        #region Mileage Rates
        public DriverRatesDS GetMileageRates(DateTime date) { return GetMileageRates(date,null,-1); }
        public DriverRatesDS GetMileageRates(DateTime date,string terminalAgent) { return GetMileageRates(date,terminalAgent,-1); }
        public DriverRatesDS GetMileageRates(DateTime date,string terminalAgent, int equipmentTypeID) {
            //Return applicable mileage rates for the specified date
            DriverRatesDS rates = new DriverRatesDS();
            try {
                object id = null; if(equipmentTypeID > -1) id = equipmentTypeID;
                DataSet ds = new DataService().FillDataset(this.mConnectionID, USP_RATEMILEAGE,TBL_RATEMILEAGE,new object[] { date,terminalAgent,id });
                if(ds.Tables[TBL_RATEMILEAGE].Rows.Count > 0)
                    rates.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new DriverRateException("Unexpected error while reading rates.",ex); }
            return rates;
        }
        public bool CreateMileageRate(string agentNumber,int equipmentID,DateTime effectiveDate,double mile, decimal baseRate, decimal rate) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEMILEAGENEW,new object[] { agentNumber,equipmentID,effectiveDate,mile,baseRate,rate });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new mileage rate...",ex); }
            return result;
        }
        public bool UpdateMileageRate(int id,int equipmentID,double mile,decimal baseRate,decimal rate) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEMILEAGEUPDATE,new object[] { id,equipmentID,mile,baseRate,rate });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating mileage rate...",ex); }
            return result;
        }
        #endregion
        #region Unit Rates
        public DriverRatesDS GetUnitRates(DateTime date) { return GetUnitRates(date,null,-1); }
        public DriverRatesDS GetUnitRates(DateTime date,string terminalAgent) { return GetUnitRates(date,terminalAgent,-1); }
        public DriverRatesDS GetUnitRates(DateTime date,string terminalAgent,int equipmentTypeID) {
            //Return applicable unit rates (i.e. multi-trip, carton, pallet) for the specified date
            DriverRatesDS rates = new DriverRatesDS();
            try {
                object id = null; if(equipmentTypeID > -1) id = equipmentTypeID;
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_RATEUNIT,TBL_RATEUNIT,new object[] { date,terminalAgent,id });
                if(ds.Tables[TBL_RATEUNIT].Rows.Count > 0)
                    rates.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new DriverRateException("Unexpected error while reading rates.",ex); }
            return rates;
        }
        public bool CreateUnitRate(string agentNumber,int equipmentID,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEUNITNEW,new object[] { agentNumber,equipmentID,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new unit rate...",ex); }
            return result;
        }
        public bool UpdateUnitRate(int id,int equipmentID,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEUNITUPDATE,new object[] { id,equipmentID,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating unit rate...",ex); }
            return result;
        }
        #endregion
        #region Milage Route Rates
        public DriverRatesDS GetMileageRouteRates(DateTime date) { return GetMileageRouteRates(date,null,null); }
        public DriverRatesDS GetMileageRouteRates(DateTime date,string terminalAgent) { return GetMileageRouteRates(date,terminalAgent,null); }
        public DriverRatesDS GetMileageRouteRates(DateTime date,string terminalAgent,string route) {
            //Return applicable route-based mileage rates for the specified date
            DriverRatesDS rates = new DriverRatesDS();
            try {
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_RATEMILEAGEROUTE,TBL_RATEMILEAGEROUTE,new object[] { date,terminalAgent,route });
                if(ds.Tables[TBL_RATEMILEAGEROUTE].Rows.Count > 0)
                    rates.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new DriverRateException("Unexpected error while reading rates.",ex); }
            return rates;
        }
        public bool CreateMileageRouteRate(string agentNumber,string route,DateTime effectiveDate,double mile,decimal baseRate,decimal rate,int status) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEMILEAGEROUTENEW,new object[] { agentNumber,route,effectiveDate,mile,baseRate,rate,status });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new mileage route rate...",ex); }
            return result;
        }
        public bool UpdateMileageRouteRate(int id,string route,double mile,decimal baseRate,decimal rate,int status) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEMILEAGEROUTEUPDATE,new object[] { id,route,mile,baseRate,rate });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating mileage route rate...",ex); }
            return result;
        }
        #endregion
        #region Unit Route Rates
        public DriverRatesDS GetUnitRouteRates(DateTime date) { return GetUnitRouteRates(date,null,null); }
        public DriverRatesDS GetUnitRouteRates(DateTime date,string terminalAgent) { return GetUnitRouteRates(date,terminalAgent,null); }
        public DriverRatesDS GetUnitRouteRates(DateTime date,string terminalAgent,string route) {
            //Return applicable route-based unit rates (i.e. multi-trip, carton, pallet) for the specified date
            DriverRatesDS rates = new DriverRatesDS();
            try {
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_RATEUNITROUTE,TBL_RATEUNITROUTE,new object[] { date,terminalAgent,route });
                if(ds.Tables[TBL_RATEUNITROUTE].Rows.Count > 0)
                    rates.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new DriverRateException("Unexpected error while reading rates.",ex); }
            return rates;
        }
        public bool CreateUnitRouteRate(string agentNumber,string route,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEUNITROUTENEW,new object[] { agentNumber,route,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase,status });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new unit route rate...",ex); }
            return result;
        }
        public bool UpdateUnitRouteRate(int id,string route,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(this.mConnectionID,USP_RATEUNITROUTEUPDATE,new object[] { id,route,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating unit route rate...",ex); }
            return result;
        }
        #endregion
    }
}
