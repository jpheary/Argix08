//	File:	financefactory.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.Finance {
	//
	internal class FinanceFactory {
		//Members
        private static DriverCompDS _TerminalConfigs = null;
        private static DriverCompDS _DriverEquip = null;
        private static DriverCompDS _AdjustmentTypes = null;
        private static DriverCompDS _LastRoutes = null;
        
        public static Mediator Mediator = null;

        public const string USP_TERMCONFIG = "uspDCTerminalConfigurationGet",TBL_TERMCONFIG = "TerminalConfigurationTable";
        public const string USP_DRIVEREQUIP = "uspDCDriverEquipmentGet",TBL_DRIVEREQUIP = "DriverEquipmentTable";
        public const string USP_DRIVEREQUIPNEW = "uspDCDriverEquipmentNew";
        public const string USP_DRIVEREQUIPUPDATE = "uspDCDriverEquipmentUpdate";
        public const string USP_ADJUSTTYPE = "uspDCAdjustmentTypeGetList",TBL_ADJUSTTYPE = "AdjustmentTypeTable";
        public const string USP_FUELCOST = "uspDCFuelCostGet",TBL_FUELCOST = "FuelCostTable";
        public const string USP_DRIVERLASTROUTE = "uspDCDriverCompensationLatestGetList",TBL_DRIVERLASTROUTE = "DriverRouteTable";

        public static event EventHandler CacheChanged = null;

        //Interface
		static FinanceFactory() {
            _TerminalConfigs = new DriverCompDS();
            _DriverEquip = new DriverCompDS();
            _AdjustmentTypes = new DriverCompDS();
            _LastRoutes = new DriverCompDS();
        }
		private FinanceFactory() { }
        public static DataSet TerminalConfigurations { get { return _TerminalConfigs; } }
        public static DriverCompDS DriverEquipment { get { return _DriverEquip; } }
        public static DriverCompDS RouteAdjustmentTypes { get { return _AdjustmentTypes; } }
        
        public static void RefreshCache() {
			//Refresh cached data
            try {
                _TerminalConfigs.Clear();
                DataSet ds = Mediator.FillDataset(USP_TERMCONFIG,TBL_TERMCONFIG,null);
                if(ds.Tables[TBL_TERMCONFIG].Rows.Count > 0) _TerminalConfigs.Merge(ds);

                _DriverEquip.Clear();
                ds = Mediator.FillDataset(USP_DRIVEREQUIP,TBL_DRIVEREQUIP,null);
                if(ds.Tables[TBL_DRIVEREQUIP].Rows.Count > 0) _DriverEquip.Merge(ds);

                _AdjustmentTypes.Clear();
                ds = Mediator.FillDataset(USP_ADJUSTTYPE,TBL_ADJUSTTYPE,null);
                if(ds.Tables[TBL_ADJUSTTYPE].Rows.Count > 0) _AdjustmentTypes.Merge(ds);

                _LastRoutes.Clear();
                ds = Mediator.FillDataset(USP_DRIVERLASTROUTE,TBL_DRIVERLASTROUTE,null);
                if(ds.Tables[TBL_DRIVERLASTROUTE].Rows.Count > 0) _LastRoutes.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while caching Finance factory data.",ex); }
            finally { if(CacheChanged != null) CacheChanged(null,EventArgs.Empty); }
        }
        #region Terminal Configuration: GetTerminalConfiguration()
        public static TerminalConfiguration GetTerminalConfiguration(string agentNumber) {
            //Return a single terminal configuration
            TerminalConfiguration config = null;
            DriverCompDS.TerminalConfigurationTableRow[] configs = (DriverCompDS.TerminalConfigurationTableRow[])_TerminalConfigs.TerminalConfigurationTable.Select("AgentNumber='" + agentNumber + "'");
            if(configs.Length > 0) config = new TerminalConfiguration(configs[0]);
            return config;
        }
        public static decimal GetRouteAdminFee(string agentNumber,string _operator) {
            //Search for an admin fee on a prior route; else, use terminal default
            decimal charge = 0.0M;
            if(_LastRoutes.DriverRouteTable.Rows.Count > 0) {
                DriverCompDS.DriverRouteTableRow[] routes = (DriverCompDS.DriverRouteTableRow[])_LastRoutes.DriverRouteTable.Select("Operator='" + _operator + "'");
                if(routes.Length > 0) charge = routes[0].AdminCharge;
            }
            if(charge == 0) {
                //Use terminal default value
                charge = GetTerminalConfiguration(agentNumber).AdminFee;
            }
            return charge;
        }
        #endregion
        #region Driver Equipment: CreateDriverEquipment(), UpdateDriverEquipment()
        public static bool CreateDriverEquipment(string vendorID,string operatorName,int equipmentID) {
            bool result = false;
            try {
                //Validate
                DataRow[] rows = _DriverEquip.DriverEquipmentTable.Select("FinanceVendorID='" + vendorID + "' AND OperatorName='" + operatorName + "'");
                if(rows.Length > 0) throw new ApplicationException("Equipment already specified for " + operatorName + ".");
                
                //Save driver equipment
                result = Mediator.ExecuteNonQuery(USP_DRIVEREQUIPNEW,new object[] { vendorID, operatorName, equipmentID });
                RefreshCache();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new driver equipment...",ex); }
            return result;
        }
        public static bool UpdateDriverEquipment(string vendorID,string operatorName,int equipmentID) {
            bool result = false;
            try {
                //Save driver route
                result = Mediator.ExecuteNonQuery(USP_DRIVEREQUIPUPDATE,new object[] { vendorID,operatorName,equipmentID });
                RefreshCache();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new driver equipment...",ex); }
            return result;
        }
        #endregion
        public static decimal GetFuelCost(DateTime date, string agentNumber) {
            //
            decimal cost = 0.0M;
            DataSet ds = Mediator.FillDataset(USP_FUELCOST,TBL_FUELCOST,new object[] { date,agentNumber });
            if(ds.Tables[TBL_FUELCOST].Rows.Count > 0)
                cost = Convert.ToDecimal(ds.Tables[TBL_FUELCOST].Rows[0]["FuelCost"]);
            return cost;
        }
    }
}
