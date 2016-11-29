//	File:	drivercomp.cs
//	Author:	jheary
//	Date:	08/20/08
//	Desc:	Determines driver compensation for a single agent terminal and pay period 
//          (i.e. Sun - Sat) including Roadshow route conversion, daily and summary
//          compensation, route rating, and compensation exporting.
//	Rev:	02/26/09 (jph)- revised method applyRates() to apply maximums to miles amount.
//          05/19/09 (jph)- revised method applyRates() to apply special carton rate for Ridgefield.
//          01/27/10 (jph)- revised method applyRates() to remove special carton rate for Ridgefield.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using Argix.Data;
using Argix.Enterprise;
using Microsoft.ApplicationBlocks.Data;

namespace Argix.Finance {
	//
	public class DriverComp {
		//Members
        private string mAgentNumber = "";
        private string mAgentName = "";
        private DateTime mStart = DateTime.Now, mEnd = DateTime.Now;
        private DriverCompDS mCompDS = null, mRoutesDS = null;
        private TerminalConfiguration mTerminalConfig;
        private DriverRates mRates = null;
        private Mediator mMediator = null;

        public event EventHandler DriverRoutesChanged = null;
        public event EventHandler RoadshowRoutesChanged = null;

		//Interface
        public DriverComp(Mediator mediator) : this("","",DateTime.Today.AddDays(-6),DateTime.Today,mediator) { }
        public DriverComp(string terminalID,string terminal,DateTime start,DateTime end,Mediator mediator) {
            //Constructor
            try {
                this.mMediator = mediator;
                this.mAgentNumber = terminalID;
                this.mAgentName = terminal;
                this.mStart = start;
                this.mEnd = end;
                this.mRoutesDS = new DriverCompDS();
                this.mCompDS = new DriverCompDS();
                this.mRates = new DriverRates(this.mEnd,this.mAgentNumber,this.mAgentName);
                this.mTerminalConfig = FinanceFactory.GetTerminalConfiguration(this.mAgentNumber);
                RefreshDriverRoutes();
                RefreshRoadshowRoutes();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to instantiate a new ship schedule.",ex); }
        }
        #region Accessors\Modifiers: [Members...], ToDataSet()
        public string AgentNumber { get { return this.mAgentNumber; } }
        public string AgentName { get { return this.mAgentName; } }
        public DateTime StartDate { get { return this.mStart; } }
        public DateTime EndDate { get { return this.mEnd; } }
        public DriverCompDS DriverRoutes { get { return this.mCompDS; } }
        public DriverCompDS RoadshowRoutes { get { return this.mRoutesDS; } }
        public DriverRates Rates { get { return this.mRates; } }
        public bool IsDirty { get { return this.mCompDS.HasChanges(); } }
        public string Title { get { return this.mAgentName.ToUpper().Trim() + " DRIVERS " + this.mStart.ToString("MMddyy") + "-" + this.mEnd.ToString("MMddyy"); } }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            return this.mCompDS;
        }
        #endregion
        public void RefreshDriverRoutes() {
            //Add all driver routes (from database) to the compensation
            try {
                this.mCompDS.Clear();
                this.mCompDS.DriverRouteTable.BeginLoadData();
                DriverCompDS routes = readDriverRoutes();
                for(int i = 0; i < routes.DriverRouteTable.Rows.Count; i++) {
                    //Iterate through each saved route and build the viewable dataset (this.mCompDS)
                    DriverCompDS.DriverRouteTableRow _route = (DriverCompDS.DriverRouteTableRow)routes.DriverRouteTable.Rows[i];
                    if(this.mCompDS.DriverCompTable.Select("Operator='" + _route.Operator + "'").Length == 0) {
                        //Operator not added yet- add the summary record for this operator
                        DriverCompDS.DriverCompTableRow sumComp = this.mCompDS.DriverCompTable.NewDriverCompTableRow();
                        #region Set members
                        sumComp.Select = _route.IsExportedNull();
                        sumComp.IsNew = sumComp.IsCombo = sumComp.IsAdjust = false;
                        sumComp.AgentNumber = _route.AgentNumber;
                        sumComp.FinanceVendorID = _route.FinanceVendorID;
                        sumComp.FinanceVendor = _route.Payee;
                        sumComp.Operator = _route.Operator;
                        //sumComp.EquipmentTypeID = _route.EquipmentTypeID;
                        sumComp.Miles = sumComp.Trip = sumComp.Stops = sumComp.Cartons = sumComp.Pallets = sumComp.PickupCartons = 0;
                        sumComp.MilesAmount = sumComp.DayAmount = sumComp.TripAmount = sumComp.StopsAmount = sumComp.CartonsAmount = sumComp.PalletsAmount = sumComp.PickupCartonsAmount = sumComp.Amount = 0.0M;
                        sumComp.FSCMiles = 0;
                        sumComp.FuelCost = sumComp.FSCGal = sumComp.FSCBaseRate = sumComp.FSC = 0.0M;
                        sumComp.MinimunAmount = sumComp.AdminCharge = sumComp.AdjustmentAmount1 = sumComp.AdjustmentAmount2 = sumComp.TotalAmount = 0.0M;
                        #endregion
                        this.mCompDS.DriverCompTable.AddDriverCompTableRow(sumComp);

                        //Add all routes for this operator
                        DriverCompDS.DriverRouteTableRow[] _routes = (DriverCompDS.DriverRouteTableRow[])routes.DriverRouteTable.Select("AgentNumber=" + _route.AgentNumber + " AND Operator='" + _route.Operator + "'");
                        for(int j = 0; j < _routes.Length; j++) {
                            DriverCompDS.DriverRouteTableRow dayComp = this.mCompDS.DriverRouteTable.NewDriverRouteTableRow();
                            #region Set members
                            dayComp.ID = _routes[j].ID;
                            dayComp.IsNew = _routes[j].IsNew;
                            dayComp.IsCombo = (routes.DriverRouteTable.Select("Operator='" + _route.Operator + "' AND RouteDate='" + _routes[j].RouteDate + "'").Length > 1);
                            dayComp.IsAdjust = _routes[j].RouteName.Contains("ADJUST");
                            dayComp.AgentNumber = _routes[j].AgentNumber;
                            dayComp.FinanceVendorID = _routes[j].FinanceVendorID;
                            dayComp.EquipmentTypeID = _routes[j].EquipmentTypeID;
                            dayComp.RouteDate = _routes[j].RouteDate;
                            dayComp.RouteName = _routes[j].RouteName;
                            dayComp.Operator = _routes[j].Operator;
                            dayComp.Payee = _routes[j].Payee;
                            dayComp.RateTypeID = _routes[j].RateTypeID;
                            dayComp.Miles = _routes[j].Miles;
                            dayComp.MilesBaseRate = _routes[j].MilesBaseRate;
                            dayComp.MilesRate = _routes[j].MilesRate;
                            dayComp.MilesAmount = _routes[j].MilesAmount;
                            dayComp.DayRate = _routes[j].DayRate;
                            dayComp.DayAmount = _routes[j].DayAmount;
                            dayComp.Trip = _routes[j].Trip;
                            dayComp.TripRate = _routes[j].TripRate;
                            dayComp.TripAmount = _routes[j].TripAmount;
                            dayComp.Stops = _routes[j].Stops;
                            dayComp.StopsRate = _routes[j].StopsRate;
                            dayComp.StopsAmount = _routes[j].StopsAmount;
                            dayComp.Cartons = _routes[j].Cartons;
                            dayComp.CartonsRate = _routes[j].CartonsRate;
                            dayComp.CartonsAmount = _routes[j].CartonsAmount;
                            dayComp.Pallets = _routes[j].Pallets;
                            dayComp.PalletsRate = _routes[j].PalletsRate;
                            dayComp.PalletsAmount = _routes[j].PalletsAmount;
                            dayComp.PickupCartons = _routes[j].PickupCartons;
                            dayComp.PickupCartonsRate = _routes[j].PickupCartonsRate;
                            dayComp.PickupCartonsAmount = _routes[j].PickupCartonsAmount;
                            dayComp.FSCMiles = _routes[j].IsFSCMilesNull() ? 0 : _routes[j].FSCMiles;
                            dayComp.FuelCost = _routes[j].FuelCost;
                            dayComp.FSCGal = _routes[j].FSCGal;
                            dayComp.FSCBaseRate = _routes[j].FSCBaseRate;
                            dayComp.FSC = _routes[j].FSC;
                            dayComp.MinimunAmount = _routes[j].MinimunAmount;
                            dayComp.AdminCharge = _routes[j].AdminCharge;
                            dayComp.AdjustmentAmount1 = _routes[j].AdjustmentAmount1;
                            dayComp.AdjustmentAmount1TypeID = _routes[j].AdjustmentAmount1TypeID;
                            dayComp.AdjustmentAmount2 = _routes[j].AdjustmentAmount2;
                            dayComp.AdjustmentAmount2TypeID = _routes[j].AdjustmentAmount2TypeID;
                            dayComp.TotalAmount = _routes[j].TotalAmount;
                            dayComp.Imported = _routes[j].Imported;
                            if(!_routes[j].IsExportedNull()) dayComp.Exported = _routes[j].Exported;
                            dayComp.LastUpdated = _routes[j].LastUpdated;
                            dayComp.UserID = _routes[j].UserID;
                            #endregion
                            this.mCompDS.DriverRouteTable.AddDriverRouteTableRow(dayComp);
                        }
                        //Compute summary compensation
                        UpdateSummary(_route.Operator);
                    }
                }
                this.mCompDS.AcceptChanges();
                this.mCompDS.DriverRouteTable.EndLoadData();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error...",ex); }
            finally { if(this.DriverRoutesChanged != null) this.DriverRoutesChanged(this,EventArgs.Empty); }
        }
        public void RefreshRoadshowRoutes() {
            //Get a list of all Roadshow routes for this terminal and date range
            try {
                this.mRoutesDS.Clear();
                DataSet ds = this.mMediator.FillDataset(App.USP_ROADSHOWROUTES,App.TBL_ROADSHOWROUTES,new object[] { this.AgentNumber,this.StartDate.ToString("yyyy-MM-dd"),this.EndDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[App.TBL_ROADSHOWROUTES].Rows.Count > 0) {
                    this.mRoutesDS.Merge(ds);

                    //Check-off new routes (those that don't exist in the driver's compensation)
                    for(int i = 0; i < this.mRoutesDS.RoadshowRouteTable.Rows.Count; i++) {
                        int termID = this.mRoutesDS.RoadshowRouteTable[i].DepotNumber;
                        string oper = this.mRoutesDS.RoadshowRouteTable[i].Operator;
                        string routeName = this.mRoutesDS.RoadshowRouteTable[i].Rt_Name;
                        DateTime routeDate = this.mRoutesDS.RoadshowRouteTable[i].Rt_Date;
                        DriverCompDS.DriverRouteTableRow[] _routes = (DriverCompDS.DriverRouteTableRow[])this.mCompDS.DriverRouteTable.Select("AgentNumber=" + termID + " AND Operator='" + oper + "' AND RouteName='" + routeName + "' AND RouteDate='" + routeDate + "'");
                        this.mRoutesDS.RoadshowRouteTable[i].New = (_routes.Length == 0);
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing Roadshow routes...",ex); }
            finally { if(this.RoadshowRoutesChanged != null) this.RoadshowRoutesChanged(this,EventArgs.Empty); }
        }
        public void ConvertRoadshowRoutes() {
            //Add all new (selected) Roadshow routes to the compensation package
            try {
                for(int i = 0; i < this.mRoutesDS.RoadshowRouteTable.Rows.Count; i++) {
                    //Check if new (i.e. selected by user)
                    DriverCompDS.RoadshowRouteTableRow _route = this.mRoutesDS.RoadshowRouteTable[i];
                    if(_route.New) {
                        //Validate route depot matches this agent
                        if(_route.DepotNumber.ToString("0000") == this.mAgentNumber) {
                            //Add operator summary (parent) record if required
                            if(this.mCompDS.DriverCompTable.Select("Operator='" + _route.Operator + "'").Length == 0) {
                                //Add the summary record for this operator
                                DriverCompDS.DriverCompTableRow sumComp = this.mCompDS.DriverCompTable.NewDriverCompTableRow();
                                #region Set members
                                sumComp.Select = true;
                                sumComp.IsNew = sumComp.IsCombo = sumComp.IsAdjust = false;
                                sumComp.AgentNumber = _route.DepotNumber.ToString("0000");
                                sumComp.FinanceVendorID = (!_route.IsFinanceVendIDNull() ? _route.FinanceVendID : "00000");
                                sumComp.FinanceVendor = (!_route.IsPayeeNull() ? _route.Payee : "");
                                sumComp.Operator = _route.Operator;
                                //sumComp.EquipmentTypeID = _route.EquipmentID;
                                sumComp.Miles = sumComp.Trip = sumComp.Stops = sumComp.Cartons = sumComp.Pallets = sumComp.PickupCartons = 0;
                                sumComp.MilesAmount = sumComp.DayAmount = sumComp.TripAmount = sumComp.StopsAmount = sumComp.CartonsAmount = sumComp.PalletsAmount = sumComp.PickupCartonsAmount = sumComp.Amount = 0.0M;
                                sumComp.FSCMiles = 0;
                                sumComp.FuelCost = sumComp.FSCGal = sumComp.FSCBaseRate = sumComp.FSC = 0.0M;
                                sumComp.MinimunAmount = sumComp.AdminCharge = sumComp.AdjustmentAmount1 = sumComp.AdjustmentAmount2 = sumComp.TotalAmount = 0.0M;
                                #endregion
                                this.mCompDS.DriverCompTable.AddDriverCompTableRow(sumComp);
                            }

                            //Validate daily route doesn't exist; add if doesn't exist
                            if(this.mCompDS.DriverRouteTable.Select("Operator='" + _route.Operator + "' AND RouteDate='" + _route.Rt_Date + "' AND RouteName='" + _route.Rt_Name + "'").Length == 0) {
                                //Create daily compensation from the Roadshow route
                                DriverCompDS.DriverRouteTableRow dayComp = this.mCompDS.DriverRouteTable.NewDriverRouteTableRow();
                                #region Set members
                                dayComp.ID = 0;
                                dayComp.IsNew = false;
                                dayComp.IsCombo = false;    //Do on refresh (this.mCompDS.DriverRouteTable.Select("Operator='" + _route.Operator + "' AND RouteDate='" + _route.Rt_Date + "'").Length > 1);
                                dayComp.IsAdjust = false;   //Do on refresh  _route.Rt_Name.Contains("ADJUST");
                                dayComp.AgentNumber = _route.DepotNumber.ToString("0000");
                                dayComp.FinanceVendorID = (!_route.IsFinanceVendIDNull() ? _route.FinanceVendID : "00000");
                                dayComp.EquipmentTypeID = _route.EquipmentID;
                                dayComp.RouteDate = _route.Rt_Date;
                                dayComp.RouteName = _route.Rt_Name;
                                dayComp.Operator = _route.Operator;
                                dayComp.Payee = (!_route.IsPayeeNull() ? _route.Payee : "");
                                dayComp.RateTypeID = RouteRatings.RATETYPE_NONE;
                                dayComp.Miles = (!_route.IsTtlMilesNull()) ? _route.TtlMiles : 0;
                                dayComp.MilesBaseRate = 0.0M;
                                dayComp.MilesRate = 0.0M;
                                dayComp.MilesAmount = 0.0M;
                                dayComp.DayRate = 0.0M;
                                dayComp.DayAmount = 0.0M;
                                dayComp.Trip = (!_route.IsMultiTrpNull()) ? _route.MultiTrp : 0;
                                dayComp.TripRate = 0.0M;
                                dayComp.TripAmount = 0.0M;
                                dayComp.Stops = (!_route.IsUniqueStopsNull()) ? _route.UniqueStops : 0;
                                dayComp.StopsRate = 0.0M;
                                dayComp.StopsAmount = 0.0M;
                                dayComp.Cartons = (!_route.IsDelCtnsNull()) ? (int)_route.DelCtns : 0;
                                dayComp.CartonsRate = 0.0M;
                                dayComp.CartonsAmount = 0.0M;
                                dayComp.Pallets = (!_route.IsDelPltsorRcksNull()) ? (int)_route.DelPltsorRcks : 0;
                                dayComp.PalletsRate = 0.0M;
                                dayComp.PalletsAmount = 0.0M;
                                dayComp.PickupCartons = (!_route.IsRtnCtnNull()) ? (int)_route.RtnCtn : 0;
                                dayComp.PickupCartonsRate = 0.0M;
                                dayComp.PickupCartonsAmount = 0.0M;
                                dayComp.FSCMiles = 0;
                                dayComp.FSCGal = 0.0M;
                                dayComp.FSCBaseRate = 0.0M;
                                dayComp.FSC = 0.0M;
                                dayComp.MinimunAmount = 0.0M;
                                dayComp.AdminCharge = 0.0M;
                                dayComp.AdjustmentAmount1 = 0.0M;
                                dayComp.AdjustmentAmount1TypeID = "";
                                dayComp.AdjustmentAmount2 = 0.0M;
                                dayComp.AdjustmentAmount2TypeID = "";
                                dayComp.TotalAmount = 0.0M;
                                //dayComp.Imported = DateTime.Today;
                                //dayComp.Exported = ;
                                dayComp.ArgixRtType = _route.ArgixRtType;
                                dayComp.LastUpdated = DateTime.Today;
                                dayComp.UserID = Environment.UserName;

                                #endregion

                                //Apply ratings and fuel surcharge to the daily compensation
                                RouteRatings ratings = this.mRates.GetRouteRatings(dayComp.EquipmentTypeID,dayComp.RouteName,dayComp.Miles);
                                if(!dayComp.IsAdjust) applyRates(dayComp,ratings);
                                calcFSC(dayComp,ratings);
                                this.mCompDS.DriverRouteTable.AddDriverRouteTableRow(dayComp);

                                //Update summary compensation
                                applyAdminFee(_route.Operator);
                                UpdateSummary(_route.Operator);
                            }
                            else {
                                //Route exists
                                System.Windows.Forms.MessageBox.Show("Route exists for " + _route.Operator + " on " + _route.Rt_Date.ToShortDateString());
                            }
                            _route.New = false;
                        }
                        else {
                            //Wrong terminal
                            System.Windows.Forms.MessageBox.Show("Route belongs to " + _route.Depot + " terminal.");
                        }
                    }
                }
                Save();
                RefreshRoadshowRoutes();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while converting Roadshow routes to driver compensation...",ex); }
        }
        public void UpdateSummary(string _operator) {
            //Update summary compensation for the specified operator
            try {
                //Find the summary and daily routes for the specified operator
                DriverCompDS.DriverCompTableRow[] sumComps = (DriverCompDS.DriverCompTableRow[])this.mCompDS.DriverCompTable.Select("Operator='" + _operator + "'");
                DriverCompDS.DriverRouteTableRow[] dayComps = (DriverCompDS.DriverRouteTableRow[])this.mCompDS.DriverRouteTable.Select("Operator='" + _operator + "'");

                //Reset summary fields
                if(sumComps.Length > 1) throw new ApplicationException("When calculating compensation for " + _operator + ", " + sumComps.Length.ToString() + " summary recordS were found.");
                DriverCompDS.DriverCompTableRow sumComp = sumComps[0];
                sumComp.IsNew = sumComp.IsCombo = sumComp.IsAdjust = false;
                sumComp.Miles = sumComp.Trip = sumComp.Stops = sumComp.Cartons = sumComp.Pallets = sumComp.PickupCartons = 0;
                sumComp.MilesAmount = sumComp.DayAmount = sumComp.TripAmount = sumComp.StopsAmount = sumComp.CartonsAmount = sumComp.PalletsAmount = sumComp.PickupCartonsAmount = sumComp.Amount = 0.0M;
                sumComp.FSC = 0.0M;
                sumComp.MinimunAmount = sumComp.AdminCharge = sumComp.AdjustmentAmount1 = sumComp.AdjustmentAmount2 = sumComp.TotalAmount = 0.0M;

                //Determine route ratings and compute operator summaries
                for(int i = 0; i < dayComps.Length; i++) {
                    //Apply ratings to daily route
                    DriverCompDS.DriverRouteTableRow dayComp = dayComps[i];
                    #region Sum all daily routes
                    sumComp.IsNew = dayComp.IsNew ? true : sumComp.IsNew;
                    sumComp.IsCombo = dayComp.IsCombo ? true : sumComp.IsCombo;
                    sumComp.IsAdjust = dayComp.IsAdjust ? true : sumComp.IsAdjust;
                    sumComp.Miles += dayComp.Miles;
                    sumComp.MilesAmount += dayComp.MilesAmount;
                    sumComp.DayAmount += dayComp.DayAmount;
                    sumComp.Trip += dayComp.Trip;
                    sumComp.TripAmount += dayComp.TripAmount;
                    sumComp.Stops += dayComp.Stops;
                    sumComp.StopsAmount += dayComp.StopsAmount;
                    sumComp.Cartons += dayComp.Cartons;
                    sumComp.CartonsAmount += dayComp.CartonsAmount;
                    sumComp.Pallets += dayComp.Pallets;
                    sumComp.PalletsAmount += dayComp.PalletsAmount;
                    sumComp.PickupCartons += dayComp.PickupCartons;
                    sumComp.PickupCartonsAmount += dayComp.PickupCartonsAmount;
                    sumComp.FSCMiles += dayComp.FSCMiles;
                    sumComp.FSCGal = dayComp.FSCGal;
                    sumComp.FuelCost = dayComp.FuelCost;
                    sumComp.FSCBaseRate = this.mTerminalConfig.FSBase;
                    sumComp.FSC += dayComp.FSC;
                    sumComp.MinimunAmount = dayComp.MinimunAmount;
                    sumComp.AdminCharge += dayComp.AdminCharge;
                    sumComp.AdjustmentAmount1 += dayComp.AdjustmentAmount1;
                    sumComp.AdjustmentAmount2 += dayComp.AdjustmentAmount2;
                    //sumComp.AdjustmentAmount3 += dayComp.AdjustmentAmount3;
                    sumComp.Amount += dayComp.TotalAmount;
                    #endregion
                }
                //Compute totals
                sumComp.TotalAmount = sumComp.Amount + sumComp.FSC + sumComp.AdjustmentAmount1 + sumComp.AdjustmentAmount2;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while calculating compensation for " + _operator + "...",ex); }
        }
        public bool Save() {
            //Save
            bool result = false;
            try {
                //Save all new routes
                for(int i = 0; i < this.mCompDS.DriverRouteTable.Rows.Count; i++) {
                    //Validate route as new (NULL Import date)
                    if(this.mCompDS.DriverRouteTable[i].IsImportedNull()) {
                        DriverCompDS.DriverRouteTableRow route = this.mCompDS.DriverRouteTable[i];
                        createDriverRoute(route);
                    }
                }
                result = true;
                RefreshDriverRoutes();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while saving driver routes...",ex); }
            return result;
        }
        public bool Update() {
            //Update 
            bool result = false;
            try {
                //Updated routes
                DriverCompDS routes = (DriverCompDS)this.mCompDS.GetChanges(DataRowState.Modified);
                if(routes != null && routes.DriverRouteTable.Rows.Count > 0) {
                    //Update all modified routes
                    foreach(DriverCompDS.DriverRouteTableRow route in routes.DriverRouteTable.Rows) {
                        try {
                            result = updateDriverRoute(route);
                            if(result) route.AcceptChanges(); else route.RejectChanges();
                        }
                        catch(Exception) {
                            route.RejectChanges();
                            System.Windows.Forms.MessageBox.Show("Failed to update route (" + route.RouteDate.ToShortDateString() + ", " + route.Operator,"Route Update",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }

                //Deleted routes
                routes = (DriverCompDS)this.mCompDS.GetChanges(DataRowState.Deleted);
                if(routes != null && routes.DriverRouteTable.Rows.Count > 0) {
                    //Delete all deleted routes
                    DriverCompDS.DriverRouteTableRow route = null;
                    for(int i=routes.DriverRouteTable.Rows.Count-1; i>=0; i--) {
                        try {
                            route = (DriverCompDS.DriverRouteTableRow)routes.DriverRouteTable.Rows[i];
                            route.RejectChanges();
                            if(!route.IsImportedNull())
                                result = deleteDriverRoute(route);
                            else
                                result = true;
                            if(result) { route.Delete(); route.AcceptChanges(); }
                        }
                        catch(Exception) {
                            System.Windows.Forms.MessageBox.Show("Failed to update route (" + route.RouteDate.ToShortDateString() + ", " + route.Operator,"Route Update",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
                this.mCompDS.AcceptChanges();
                RefreshRoadshowRoutes();
            }
            catch(Exception ex) {
                this.mCompDS.RejectChanges();
                throw new ApplicationException("Unexpected error while updating driver route...",ex);
            }
            return result;
        }
        public string Export(bool updateAsExported) {
            //Export this driver compensation to file
            StringBuilder export = null;
            try {
                //Setup
                export = new StringBuilder();
                PayPeriod payPeriod = DriverRatingFactory.GetPayPeriod(this.mEnd);

                //Export driver compensations and admin charges
                foreach(DriverCompDS.DriverCompTableRow comp in this.mCompDS.DriverCompTable.Rows) {
                    if(comp.Select && comp.TotalAmount > 0) {
                        //Export driver compensation
                        export.AppendLine(formatDriverRecord(payPeriod,comp));

                        //Export Admin fees if applicable
                        DriverCompDS.DriverRouteTableRow[] routes = (DriverCompDS.DriverRouteTableRow[])this.mCompDS.DriverRouteTable.Select("AgentNumber=" + this.mAgentNumber + " AND Operator='" + comp.Operator + "'");
                        decimal adminFee = 0.0M;
                        for(int i = 0; i < routes.Length; i++) { adminFee += routes[i].AdminCharge; }
                        if(adminFee != 0)
                            export.AppendLine(formatAdminRecord(payPeriod,comp,adminFee));
                        
                        if(updateAsExported) {
                            //Update all routes as exported
                            for(int i = 0; i < routes.Length; i++) {
                                routes[i].Exported = DateTime.Today;
                                updateDriverRoute(routes[i]);
                            }
                            //RefreshDriverRoutes();
                        }
                    }
                }
                //Refresh routes if they were updated (otherwise, don't change export selections)
                if(updateAsExported) RefreshDriverRoutes();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting driver compensation...",ex); }
            return export.ToString();
        }
        #region Route Data Services: createDriverRoute(), readDriverRoutes(), updateDriverRoute(), deleteDriverRoute()
        private bool createDriverRoute(DriverCompDS.DriverRouteTableRow route) {
            bool result = false;
            try {
                //Save driver route
                DateTime imported = DateTime.Now;
                result = this.mMediator.ExecuteNonQuery(App.USP_ROUTECREATE,new object[] {   
                            route.RouteDate, route.RouteName, route.AgentNumber,
                            route.Operator, route.Payee, 
                            route.FinanceVendorID,route.EquipmentTypeID,
                            route.Miles, route.Trip, route.Stops, route.Cartons, route.Pallets, route.PickupCartons, route.RateTypeID,
                            route.DayRate, route.DayAmount, route.MilesRate, route.MilesBaseRate, route.MilesAmount, 
                            route.TripRate, route.TripAmount, route.StopsRate, route.StopsAmount, route.CartonsRate, route.CartonsAmount,
                            route.PalletsRate, route.PalletsAmount, route.PickupCartonsRate, route.PickupCartonsAmount,
                            route.FSCMiles, route.FuelCost, route.FSCGal, route.FSCBaseRate, route.FSC,
                            route.MinimunAmount, route.AdjustmentAmount1, route.AdjustmentAmount1TypeID, route.AdjustmentAmount2, route.AdjustmentAmount2TypeID,
                            0.0M, 0, route.AdminCharge, route.TotalAmount, imported, null, 
                            DateTime.Now, Environment.UserName, route.ArgixRtType });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new driver route...",ex); }
            return result;
        }
        private DriverCompDS readDriverRoutes() {
            //Get a list of all driver routes for this terminal and date range
            DriverCompDS routes = null;
            try {
                routes = new DriverCompDS();
                DataSet ds = this.mMediator.FillDataset(App.USP_DRIVERROUTES,App.TBL_DRIVERROUTES,new object[] { this.AgentNumber,this.StartDate.ToString("yyyy-MM-dd"),this.EndDate.ToString("yyyy-MM-dd") });
                ds.Tables[App.TBL_DRIVERROUTES].Columns.Add("IsNew",typeof(bool),"IsNewRoute=1");
                if(ds.Tables[App.TBL_DRIVERROUTES].Rows.Count > 0) {
                    routes.Merge(ds);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading driver routes...",ex); }
            return routes;
        }
        private bool updateDriverRoute(DriverCompDS.DriverRouteTableRow route) {
            //Update a driver route
            bool result = false;
            try {
                //Update the specified route
                route.LastUpdated = DateTime.Now;
                route.UserID = Environment.UserName;
                object exported = null;
                if(!route.IsExportedNull()) exported = route.Exported;
                result = this.mMediator.ExecuteNonQuery(App.USP_ROUTEUPDATE,new object[] {   
                            route.RouteDate, route.RouteName,route.AgentNumber, route.Operator, 
                            route.AdjustmentAmount1, route.AdjustmentAmount1TypeID,
                            route.AdjustmentAmount2, route.AdjustmentAmount2TypeID,
                            0.0, 0,
                            route.AdminCharge, route.TotalAmount, exported, 
                            route.LastUpdated, route.UserID });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating driver route...",ex); }
            return result;
        }
        private bool deleteDriverRoute(DriverCompDS.DriverRouteTableRow route) {
            //Delete a driver route
            bool result = false;
            try {
                //result = this.mMediator.ExecuteNonQuery(App.USP_ROUTEDELETE,new object[] { route.RouteDate,route.AgentNumber,route.Operator });
                result = this.mMediator.ExecuteNonQuery(App.USP_ROUTEDELETE,new object[] { route.ID });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while deleting driver route...",ex); }
            return result;
        }
        #endregion
        #region Local Services: applyRates(), calcFSC(), applyAdminFee(), formatDriverRecord(), formatAdminRecord()
        private void applyRates(DriverCompDS.DriverRouteTableRow dayComp,RouteRatings ratings) {
            //Apply ratings to this route
            try {
                //1. Copy rates (for reference)
                dayComp.RateTypeID = ratings.RateTypeID;
                dayComp.MilesBaseRate = ratings.MileBaseRate;
                dayComp.MilesRate = ratings.MileRate;
                dayComp.DayRate = ratings.DayRate;
                dayComp.TripRate = ratings.TripRate;
                dayComp.StopsRate = ratings.StopRate;
                dayComp.CartonsRate = ratings.CartonRate;
                dayComp.PalletsRate = ratings.PalletRate;
                dayComp.PickupCartonsRate = ratings.PickupCartonRate;
                dayComp.MinimunAmount = ratings.MinimumAmount;

                //2. Calculate ratings -------------------------------------------------------------
                //2.1 Standard computations
                dayComp.MilesAmount = dayComp.MilesBaseRate + dayComp.Miles * dayComp.MilesRate;
                dayComp.DayAmount = dayComp.DayRate;
                dayComp.TripAmount = dayComp.Trip * dayComp.TripRate;
                dayComp.StopsAmount = dayComp.Stops * dayComp.StopsRate;
                dayComp.CartonsAmount = dayComp.Cartons * dayComp.CartonsRate;
                dayComp.PalletsAmount = dayComp.Pallets * dayComp.PalletsRate;
                dayComp.PickupCartonsAmount = dayComp.PickupCartons * dayComp.PickupCartonsRate;

                //2.2 Override: apply maximums to miles amount based upon trigger field (i.e. Trip, Stops, Cartons, Pallets)
                if(ratings.MaximumAmount > 0) {
                    //Maximum applies: find the MaximumTriggerField and compare it's value to MaximumTriggerValue
                    if(dayComp[ratings.MaximumTriggerField] != null) {
                        int trigVal = Convert.ToInt32(dayComp[ratings.MaximumTriggerField]);
                        if(trigVal < ratings.MaximumTriggerValue) dayComp.MilesAmount = ratings.MaximumAmount;
                    }
                }

                //2.3 Override: apply additional carton compensation for tractors in Ridgefield
                //01/27/10- remove per MK (as per Jean)
                //DateTime effDate = new DateTime(2009,5,17);
                //if(this.mStart.CompareTo(effDate) >= 0 && this.mAgentNumber == "0001" && dayComp.EquipmentTypeID == 4 && dayComp.Cartons > 699)
                //    dayComp.CartonsAmount += 0.15M * (dayComp.Cartons - 699);

                //3. Calculate totals and apply minimum amount
                decimal total = dayComp.DayAmount + dayComp.MilesAmount + dayComp.TripAmount + dayComp.StopsAmount + dayComp.CartonsAmount + dayComp.PalletsAmount + dayComp.PickupCartonsAmount;
                dayComp.TotalAmount = (total < dayComp.MinimunAmount) ? dayComp.MinimunAmount : total ;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while rating.",ex); }
        }
        private void calcFSC(DriverCompDS.DriverRouteTableRow dayComp,RouteRatings ratings) {
            //Calculate FSC if required
            try {
                //1. FSC applies only if miles rates are present in the rating
                if(ratings.MileBaseRate > 0 || ratings.MileRate > 0) dayComp.FSCMiles = dayComp.Miles;

                //2. Copy rates (for reference)
                dayComp.FuelCost = FinanceFactory.GetFuelCost(this.EndDate,this.mAgentNumber);
                dayComp.FSCGal = EnterpriseFactory.GetDriverEquipmentMPG(dayComp.EquipmentTypeID);
                if(dayComp.FSCGal <= 0.0M)
                    throw new ApplicationException("FSCGal (" + dayComp.FSCGal.ToString() + "MPG) is invalid.");
                dayComp.FSCBaseRate = this.mTerminalConfig.FSBase;
                
                //3. Calculate FSC
                dayComp.FSC = dayComp.FSCMiles / dayComp.FSCGal * (dayComp.FuelCost - dayComp.FSCBaseRate);
                if(dayComp.FSC < 0) dayComp.FSC = 0.0M;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while calculating FSC.",ex); }
        }
        private void applyAdminFee(string _operator) {
            //Apply admin fee to this route for the specified operator
            try {
                //Get all daily compensation for this operator and apply admin fee to one route only
                DriverCompDS.DriverRouteTableRow[] dayComps = (DriverCompDS.DriverRouteTableRow[])this.mCompDS.DriverRouteTable.Select("AgentNumber=" + this.mAgentNumber + " AND Operator='" + _operator + "'");
                dayComps[0].AdminCharge = FinanceFactory.GetRouteAdminFee(this.mAgentNumber,_operator);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while applying administrative fee.",ex); }
        }
        private string formatDriverRecord(PayPeriod payPeriod,DriverCompDS.DriverCompTableRow comp) {
            //
            #region Driver record
            //All fields fixed 12 position and left justified
            //022         13418       09/13/08    WE091308    Mi          484                                 012         08          1424.23     18505000    N
            //------------============------------============------------============------------============------------============------------============------------
            //|           |           |           |           |           |           |           |           |           |           |           |           |
            //a           b           c           d           e           f           g           h           i           j           k           l           m

            //a) Company Code [12]; always 022
            //b) VendorFinanceID [12];
            //c) Date [12, MM/dd/yyyy];
            //d) Invoice# [12, WE + Date(MMddyy)];
            //e) Desc1 [12]; always blank
            //f) Desc2 [12]; always blank
            //g) Desc3 [12]; always blank
            //h) Desc4 [12]; always blank
            //i) Pay period month [12, MM];
            //j) Pay period year [12, yy];
            //k) Total amount [12];
            //l) General LG# [12];
            //m) TaxID [12]; always N
            #endregion
            string s = "";
            try {
                s = "022".PadRight(12,' ') +
                    comp.FinanceVendorID.PadRight(12,' ').Substring(0,12) +
                    this.EndDate.ToString("MM/dd/yy").PadRight(12,' ').Substring(0,12) +
                    "WE" + this.EndDate.ToString("MMddyy").PadRight(10,' ').Substring(0,10) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    payPeriod.Month.PadLeft(3, '0').PadRight(12,' ').Substring(0,12) +
                    payPeriod.Year.Substring(2,2).PadRight(12,' ').Substring(0,12) +
                    comp.TotalAmount.ToString("#0.00").PadRight(12,' ').Substring(0,12) +
                    this.mTerminalConfig.GLNumber.PadRight(12,' ').Substring(0,12) +
                    "N".PadRight(12,' ').Substring(0,12);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting trailer export string.",ex); }
            return s;
        }
        private string formatAdminRecord(PayPeriod payPeriod,DriverCompDS.DriverCompTableRow comp,decimal adminFee) {
            //
            #region Admin record
            //All fields fixed 12 position and left justified
            //022         13418       09/13/08    AD091308    Ad                                              012         08          -10.00      18516000    N
            //------------============------------============------------============------------============------------============------------============------------
            //|           |           |           |           |           |           |           |           |           |           |           |           |
            //a           b           c           d           e           f           g           h           i           j           k           l           m

            //a) Company Code [12]; always 022
            //b) VendorFinanceID [12];
            //c) Date [12, MM/dd/yyyy];
            //d) Invoice# [12, AD + Date(MMddyy)];
            //e) Desc1 [12]; always blank
            //f) Desc2 [12]; always blank
            //g) Desc3 [12]; always blank
            //h) Desc4 [12]; always blank
            //i) Pay period month [12, mm];
            //j) Pay period year [12, yy];
            //k) Admin amount [12];
            //l) General LG# [12];
            //m) TaxID [12]; always N
            #endregion
            string s = "";
            try {
                s = "022".PadRight(12,' ') +
                    comp.FinanceVendorID.PadRight(12,' ').Substring(0,12) +
                    this.EndDate.ToString("MM/dd/yy").PadRight(12,' ').Substring(0,12) +
                    "AD" + this.EndDate.ToString("MMddyy").PadRight(10,' ').Substring(0,10) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    payPeriod.Month.PadLeft(3,'0').PadRight(12,' ').Substring(0,12) +
                    payPeriod.Year.Substring(2,2).PadRight(12,' ').Substring(0,12) +
                    adminFee.ToString("#0.00").PadRight(12,' ').Substring(0,12) +
                    this.mTerminalConfig.AdminGLNumber.PadRight(12,' ').Substring(0,12) +
                    "N".PadRight(12,' ').Substring(0,12);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting trailer export string.",ex); }
            return s;
        }
        #endregion
    }
}
