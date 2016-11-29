//	File:	stationassignment.cs
//	Author:	J. Heary
//	Date:	03/05/05
//	Desc:	A freight assignment specifies a sort job on a single workstation
//			for a single inbound shipment using a sort method (i.e. SAN, JIT, etc.).
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Configuration;
using System.Text;
using Tsort.Freight;

namespace Tsort.Sort {
    //
    internal class StationAssignment {
        //Members
        private string mAssignmentID="";
        private Workstation mStation=null;
        private InboundFreight mFreight=null;
        private SortProfile mProfile=null;

        //Interface
        public StationAssignment() { }
        public StationAssignment(string assignmentID,Workstation sortStation,InboundFreight inboundFreight,SortProfile sortProfile) {
            //Constructor
            try {
                //Configure this assignment from the assignment configuration information
                this.mAssignmentID = assignmentID;
                this.mStation = sortStation;
                this.mFreight = inboundFreight;
                this.mProfile = sortProfile;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Station Assignment instance.",ex); }
        }
        #region Accessors\Modifiers: AssignmentID, SortStation, InboundFreight, SortProfile, ToDataSet()
        public string AssignmentID { get { return this.mAssignmentID; } }
        public Workstation SortStation { get { return this.mStation; } }
        public InboundFreight InboundFreight { get { return this.mFreight; } }
        public SortProfile SortProfile { get { return this.mProfile; } }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            StationAssignmentDS ds=null;
            try {
                ds = new StationAssignmentDS();
                StationAssignmentDS.FreightClientShipperTableRow assignment = ds.FreightClientShipperTable.NewFreightClientShipperTableRow();
                assignment.StationNumber = this.mStation.Number;
                assignment.WorkStationID = this.mStation.WorkStationID;
                assignment.SortTypeID = this.SortProfile.SortTypeID;
                assignment.FreightID = this.mFreight.FreightID;
                assignment.SortType = this.SortProfile.SortType;
                assignment.FreightType = this.mFreight.FreightType;
                assignment.TerminalID = this.mStation.TerminalID;
                assignment.TDSNumber = this.mFreight.TDSNumber;
                assignment.TrailerNumber = this.mFreight.TrailerNumber;
                assignment.VendorKey = this.mFreight.VendorKey;
                assignment.ClientNumber = this.mFreight.Client.Number;
                assignment.ClientDivision = this.mFreight.Client.Division;
                assignment.PickupDate = this.mFreight.PickupDate;
                assignment.PickupNumber = this.mFreight.PickupNumber;
                assignment.Client = this.mFreight.Client.Name;
                assignment.ClientAddressLine1 = this.mFreight.Client.AddressLine1;
                assignment.ClientAddressLine2 = this.mFreight.Client.AddressLine2;
                assignment.ClientAddressCity = this.mFreight.Client.City;
                assignment.ClientAddressState = this.mFreight.Client.State;
                assignment.ClientAddressZip = this.mFreight.Client.ZIP;
                assignment.CubeRatio = 0.0m;
                assignment.ShipperNumber = this.mFreight.Shipper.NUMBER;
                assignment.Shipper = this.mFreight.Shipper.NAME;
                assignment.ShipperAddressLine1 = this.mFreight.Shipper.ADDRESS_LINE1;
                assignment.ShipperAddressLine2 = this.mFreight.Shipper.ADDRESS_LINE2;
                assignment.ShipperAddressCity = this.mFreight.Shipper.CITY;
                assignment.ShipperAddressZip = this.mFreight.Shipper.ZIP;
                assignment.ShipperAddressState = this.mFreight.Shipper.STATE;
                assignment.LabelID = this.mProfile.LabelID;
                assignment.ExcLocation = this.mProfile.ExceptionLocation;
                ds.FreightClientShipperTable.AddFreightClientShipperTableRow(assignment);
                ds.AcceptChanges();
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
    }
}