using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace Argix.Finance {
	//
	public interface IDriverRatingService {
		//Interface
        DriverRatesDS RateTypes { get; }
        PayPeriod GetPayPeriod(DateTime date);
        DriverRatesDS GetMileageRates(DateTime date);
        DriverRatesDS GetMileageRates(DateTime date,string terminalAgent);
        DriverRatesDS GetMileageRates(DateTime date,string terminalAgent,int equipmentTypeID);
        bool CreateMileageRate(string agentNumber,int equipmentID,DateTime effectiveDate,double mile,decimal baseRate,decimal rate);
        bool UpdateMileageRate(int id,int equipmentID,double mile,decimal baseRate,decimal rate);

        DriverRatesDS GetUnitRates(DateTime date);
        DriverRatesDS GetUnitRates(DateTime date,string terminalAgent);
        DriverRatesDS GetUnitRates(DateTime date,string terminalAgent,int equipmentTypeID);
        bool CreateUnitRate(string agentNumber,int equipmentID,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase);
        bool UpdateUnitRate(int id,int equipmentID,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase);

        DriverRatesDS GetMileageRouteRates(DateTime date);
        DriverRatesDS GetMileageRouteRates(DateTime date,string terminalAgent);
        DriverRatesDS GetMileageRouteRates(DateTime date,string terminalAgent,string route);
        bool CreateMileageRouteRate(string agentNumber,string route,DateTime effectiveDate,double mile,decimal baseRate,decimal rate,int status);
        bool UpdateMileageRouteRate(int id,string route,double mile,decimal baseRate,decimal rate,int status);

        DriverRatesDS GetUnitRouteRates(DateTime date);
        DriverRatesDS GetUnitRouteRates(DateTime date,string terminalAgent);
        DriverRatesDS GetUnitRouteRates(DateTime date,string terminalAgent,string route);
        bool CreateUnitRouteRate(string agentNumber,string route,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status);
        bool UpdateUnitRouteRate(int id,string route,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status);
    }


}
