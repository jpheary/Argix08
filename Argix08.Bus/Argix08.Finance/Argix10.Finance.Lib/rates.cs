//	File:	rates.cs
//	Author:	jheary
//	Date:	08/20/08
//	Desc:	Static object caches latest rates for a specified rate date; instance 
//          exposes driver rates for one set of route parameters (i.e. agent#, 
//          equipment type, route, & miles)
//	Rev:	02/26/09 (jph)- added new members MaximumAmount, MaximumTriggerField, & 
//                          MaximumTriggerValue to RouteRatings; revised method 
//                          DriverRates::GetRouteRatings() to use new RouteRatings members.
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Argix.Finance {
    //DriverRates provides driver ratings for one or more agents for a single rating date
    //GetRouteRating returns a specific rating for a single route
    public class DriverRates {
        //Members
        private DateTime mRatesDate = DateTime.Today;
        private string mAgentNumber = null;
        private string mAgentName = "All Agents";
        private DriverRatesDS mRates = null;

        //Interface
        public DriverRates(DateTime ratesDate) : this(ratesDate,null,"All Agents") { }
        public DriverRates(DateTime ratesDate,string agentNumber,string agentName) { 
            //Constructor
            try {
                //Cache the rate tables for this rates date
                this.mRatesDate = ratesDate;
                this.mAgentNumber = agentNumber;
                this.mAgentName = agentName;
                this.mRates = new DriverRatesDS();
                Refresh();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new DriverRates instance.",ex); }
        }
        public void Refresh() {
            //Refresh the ratings cache for this instance
            try {
                this.mRates.Clear();
                this.mRates.Merge(DriverRatingFactory.GetMileageRates(this.mRatesDate,this.mAgentNumber));
                this.mRates.Merge(DriverRatingFactory.GetUnitRates(this.mRatesDate,this.mAgentNumber));
                this.mRates.Merge(DriverRatingFactory.GetMileageRouteRates(this.mRatesDate,this.mAgentNumber));
                this.mRates.Merge(DriverRatingFactory.GetUnitRouteRates(this.mRatesDate,this.mAgentNumber));
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing driver rates.",ex); }
    }
        public RouteRatings GetRouteRatings(int equipmentTypeID,string route,decimal miles) { return GetRouteRatings(this.mAgentNumber,equipmentTypeID,route,miles); }
        public RouteRatings GetRouteRatings(string agentNumber,int equipmentTypeID,string route,decimal miles) {
            //Determine route ratings
            RouteRatings rates = new RouteRatings();
            try {
                DriverRatesDS.RateMileageRouteTableRow ratesMR = getMileRouteRates(agentNumber,route,miles);
                if(ratesMR != null) {
                    rates.RateTypeID = RouteRatings.RATETYPE_ROUTE;
                    rates.MileBaseRate = ratesMR.BaseRate;
                    rates.MileRate = ratesMR.Rate;
                    rates.DayRate = 0.0M;
                    rates.TripRate = 0.0M;
                    rates.StopRate = 0.0M;
                    rates.CartonRate = 0.0M;
                    rates.PalletRate = 0.0M;
                    rates.PickupCartonRate = 0.0M;
                    rates.MinimumAmount = 0.0M;
                    rates.MaximumAmount = 0.0M;
                    rates.MaximumTriggerField = "";
                    rates.MaximumTriggerValue = 0;
                    rates.FSBase = 0.0M;
                }
                DriverRatesDS.RateUnitRouteTableRow ratesUR = getUnitRouteRates(agentNumber,route);
                if(ratesUR != null) {
                    rates.RateTypeID = RouteRatings.RATETYPE_ROUTE;
                    rates.DayRate = ratesUR.DayRate;
                    rates.TripRate = ratesUR.MultiTripRate;
                    rates.StopRate = ratesUR.StopRate;
                    rates.CartonRate = ratesUR.CartonRate;
                    rates.PalletRate = ratesUR.PalletRate;
                    rates.PickupCartonRate = ratesUR.PickupCartonRate;
                    rates.MinimumAmount = ratesUR.MinimumAmount;
                    rates.MaximumAmount = ratesUR.MaximumAmount;
                    rates.MaximumTriggerField = ratesUR.MaximumTriggerField;
                    rates.MaximumTriggerValue = ratesUR.MaximumTriggerValue;
                    rates.FSBase = ratesUR.FSBase;
                }
                if(rates.RateTypeID == RouteRatings.RATETYPE_NONE) {
                    rates.RateTypeID = RouteRatings.RATETYPE_VEHICLE;
                    DriverRatesDS.RateMileageTableRow ratesMV = getMileRates(agentNumber,equipmentTypeID,miles);
                    if(ratesMV != null) {
                        rates.MileBaseRate = ratesMV.BaseRate;
                        rates.MileRate = ratesMV.Rate;
                        rates.DayRate = 0.0M;
                        rates.TripRate = 0.0M;
                        rates.StopRate = 0.0M;
                        rates.CartonRate = 0.0M;
                        rates.PalletRate = 0.0M;
                        rates.PickupCartonRate = 0.0M;
                        rates.MinimumAmount = 0.0M;
                        rates.MaximumAmount = 0.0M;
                        rates.MaximumTriggerField = "";
                        rates.MaximumTriggerValue = 0;
                        rates.FSBase = 0.0M;
                    }
                    DriverRatesDS.RateUnitTableRow ratesUV = getUnitRates(agentNumber,equipmentTypeID);
                    if(ratesUV != null) {
                        rates.DayRate = ratesUV.DayRate;
                        rates.TripRate = ratesUV.MultiTripRate;
                        rates.StopRate = ratesUV.StopRate;
                        rates.CartonRate = ratesUV.CartonRate;
                        rates.PalletRate = ratesUV.PalletRate;
                        rates.PickupCartonRate = ratesUV.PickupCartonRate;
                        rates.MinimumAmount = ratesUV.MinimumAmount;
                        rates.MaximumAmount = ratesUV.MaximumAmount;
                        rates.MaximumTriggerField = ratesUV.MaximumTriggerField;
                        rates.MaximumTriggerValue = ratesUV.MaximumTriggerValue;
                        rates.FSBase = ratesUV.FSBase;
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating new DriverRates instance.",ex); }
            return rates;
        }
        #region Accessors\Modifiers: [Members...]
        public DateTime RatesDate { get { return this.mRatesDate; } }
        public string AgentNumber { get { return this.mAgentNumber; } }
        public string AgentName { get { return this.mAgentName; } }
        public DriverRatesDS Rates { get { return this.mRates; } }
        #endregion
        private DriverRatesDS.RateMileageTableRow getMileRates(string agentNumber,int equipmentTypeID,decimal miles) {
            //Get a mileage rate
            DriverRatesDS.RateMileageTableRow rate = null;
            try {
                string filter = "AgentNumber=" + agentNumber + " AND EquipmentTypeID=" + equipmentTypeID + " AND Mile <= " + miles;
                DriverRatesDS.RateMileageTableRow[] rates = (DriverRatesDS.RateMileageTableRow[])this.mRates.RateMileageTable.Select(filter,"Mile DESC");
                if(rates.Length > 0) {
                    //Take rate for largest mile
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error determining mileage rate.",ex); }
            return rate;
        }
        private DriverRatesDS.RateUnitTableRow getUnitRates(string agentNumber,int equipmentTypeID) {
            //Get all unit rates
            DriverRatesDS.RateUnitTableRow rate = null;
            try {
                string filter = "AgentNumber=" + agentNumber + " AND EquipmentTypeID=" + equipmentTypeID.ToString();
                DriverRatesDS.RateUnitTableRow[] rates = (DriverRatesDS.RateUnitTableRow[])this.mRates.RateUnitTable.Select(filter);
                if(rates.Length > 0) {
                    //Should be a single set
                    if(rates.Length > 1)
                        throw new RateRouteException("More than one rates exists for " + filter);
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error determining mileage rate.",ex); }
            return rate;
        }
        private DriverRatesDS.RateMileageRouteTableRow getMileRouteRates(string agentNumber,string route,decimal miles) {
            //Get a mileage rate
            DriverRatesDS.RateMileageRouteTableRow rate = null;
            try {
                string filter = "AgentNumber=" + agentNumber + " AND Route='" + route + "' AND Mile <= " + miles;
                DriverRatesDS.RateMileageRouteTableRow[] rates = (DriverRatesDS.RateMileageRouteTableRow[])this.mRates.RateMileageRouteTable.Select(filter,"Mile DESC");
                if(rates.Length > 0) {
                    //Take rate for largest mileage (assumes descending sort order)
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error determining mileage route rates.",ex); }
            return rate;
        }
        private DriverRatesDS.RateUnitRouteTableRow getUnitRouteRates(string agentNumber,string route) {
            //Get all unit rates
            DriverRatesDS.RateUnitRouteTableRow rate = null;
            try {
                string filter = "AgentNumber=" + agentNumber + " AND Route='" + route + "'";
                DriverRatesDS.RateUnitRouteTableRow[] rates = (DriverRatesDS.RateUnitRouteTableRow[])this.mRates.RateUnitRouteTable.Select(filter);
                if(rates.Length > 0) {
                    //Should be a single set
                    if(rates.Length > 1)
                        throw new RateRouteException("More than one rates exists for " + filter);
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error determining unit route rates.",ex); }
            return rate;
        }
    }

    //Ratings structure with ratings for a single route (i.e. date, agent, route, & equipID)
    public class RouteRatings {
        //Members
        private DateTime mRatesDate = DateTime.Today;
        private int mRateTypeID = RATETYPE_NONE;
        private decimal mMileBaseRate = 0.0M;
        private decimal mMileRate = 0.0M;
        private decimal mDayRate = 0.0M;
        private decimal mMultiTripRate = 0.0M;
        private decimal mStopRate = 0.0M;
        private decimal mCartonRate = 0.0M;
        private decimal mPalletRate = 0.0M;
        private decimal mPickupCartonRate = 0.0M;
        private decimal mMinimumAmount = 0.0M;
        private decimal mMaximumAmount = 0.0M;
        private string mMaximumTriggerField = "";
        private int mMaximumTriggerValue = 0;
        private decimal mFSBase = 0.0M;

        public const int RATETYPE_NONE = 0;
        public const int RATETYPE_VEHICLE = 1;
        public const int RATETYPE_ROUTE = 2;

        //Interface
        public RouteRatings() { }
        public int RateTypeID { get { return this.mRateTypeID; } set { this.mRateTypeID = value; } }
        public decimal MileBaseRate { get { return this.mMileBaseRate; } set { this.mMileBaseRate = value; } }
        public decimal MileRate { get { return this.mMileRate; } set { this.mMileRate = value; } }
        public decimal DayRate { get { return this.mDayRate; } set { this.mDayRate = value; } }
        public decimal TripRate { get { return this.mMultiTripRate; } set { this.mMultiTripRate = value; } }
        public decimal StopRate { get { return this.mStopRate; } set { this.mStopRate = value; } }
        public decimal CartonRate { get { return this.mCartonRate; } set { this.mCartonRate = value; } }
        public decimal PalletRate { get { return this.mPalletRate; } set { this.mPalletRate = value; } }
        public decimal PickupCartonRate { get { return this.mPickupCartonRate; } set { this.mPickupCartonRate = value; } }
        public decimal MinimumAmount { get { return this.mMinimumAmount; } set { this.mMinimumAmount = value; } }
        public decimal MaximumAmount { get { return this.mMaximumAmount; } set { this.mMaximumAmount = value; } }
        public string MaximumTriggerField { get { return this.mMaximumTriggerField; } set { this.mMaximumTriggerField = value; } }
        public int MaximumTriggerValue { get { return this.mMaximumTriggerValue; } set { this.mMaximumTriggerValue = value; } }
        public decimal FSBase { get { return this.mFSBase; } set { this.mFSBase = value; } }
    }
}
