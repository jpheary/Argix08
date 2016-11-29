using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Tsort.Devices;
using Argix.Data;
using Tsort.Devices.Printers;
using Argix.Enterprise;
using Tsort.Labels;

namespace Argix.Freight {
	//
	public class FreightService {
		//Members
        public const string USP_STATION_CONFIG = "uspIndSortWorkstationGet",TBL_STATION_CONFIG = "WorkstationDetailTable";
        public const string USP_TRIP_ASSIGNMENT = "uspIndSortTripAssignmentGetForStation",TBL_TRIP_ASSIGNMENT = "BwareTripTable";
        public const string USP_LABEL_TEMPLATE = "uspIndSortOutboundLabelGet",TBL_LABEL_DETAIL = "LabelDetailTable";

        public const string USP_CARTON_DETAIL = "uspIndSortScanGet",TBL_CARTON_DETAIL = "CartonDetailTable";
        public const string USP_CARTON_CREATE = "uspIndSortScanNew";
        public const string USP_CARTON_DELETE = "uspIndSortScanDelete";
			
		//Interface
        static FreightService() { }
		public static Workstation GetWorkstation(string machineName) {
			//Create a workstation that has an ILabelPrinter printer and IScale scale
			Workstation station=null;
			try {
				DataSet ds = App.Mediator.FillDataset(USP_STATION_CONFIG, TBL_STATION_CONFIG, new object[]{machineName});
				if(ds.Tables[TBL_STATION_CONFIG].Rows.Count == 0) 
					throw new ApplicationException("Station for  " + machineName + " not found.");
				else {
					WorkstationDS stationDS = new WorkstationDS();
					stationDS.Merge(ds);
					station = new Workstation(stationDS.WorkstationDetailTable[0]);
				}
			}
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error getting the workstation.", ex); }
			return station;
		}
		public static BearwareTrip GetTripAssignment(string stationNumber) {
			//Get the trip assigned to the specified station
			BearwareTrip trip=null;
			try {
				DataSet ds = App.Mediator.FillDataset(USP_TRIP_ASSIGNMENT, TBL_TRIP_ASSIGNMENT, new object[]{stationNumber});
                if(ds.Tables[TBL_TRIP_ASSIGNMENT].Rows.Count == 0) 
                    throw new ApplicationException("There are no trips assigned to station " + stationNumber + ".");
                else if(ds.Tables[TBL_TRIP_ASSIGNMENT].Rows.Count > 0) {
					BearwareDS dsAssignment = new BearwareDS();
					dsAssignment.Merge(ds);
					BearwareDS.BwareTripTableRow row = (BearwareDS.BwareTripTableRow)dsAssignment.BwareTripTable.Rows[0];
					trip = new BearwareTrip();
					trip.Number = row.Number.TrimEnd();
					trip.CartonCount = row.IsCartonCountNull() ? 0 : row.CartonCount;
					trip.Carrier = row.IsCarrierNull() ? "" : row.Carrier.TrimEnd();
					trip.TrailerNumber = row.IsTrailerNumberNull() ? "" : row.TrailerNumber.TrimEnd();
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error reading trip assignments for station " + stationNumber + ".", ex); }
			return trip;
		}
		public static OutboundLabel GetOutboundLabel(string labelType, string printerType) {
			//Get an outbound label
			OutboundLabel label=null;
			try {
				DataSet ds = App.Mediator.FillDataset(USP_LABEL_TEMPLATE, TBL_LABEL_DETAIL, new object[]{labelType, printerType});
				if(ds == null || ds.Tables[TBL_LABEL_DETAIL].Rows.Count == 0) 
					throw new ApplicationException("Label " + labelType + " not found for a " + printerType + "printer.");
				else {
					LabelDS labelDS = new LabelDS();
					labelDS.Merge(ds);
					label = new OutboundLabel(labelDS.LabelDetailTable[0]);
				}
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error getting an outbound label for label " + labelType + " on a " + printerType + " printer.",ex); }
			return label;
		}

        public static bool CartonExists(Carton carton) {
            //Determine if this carton already exists in the system
            bool bRet=false;
            try {
                DataSet ds = App.Mediator.FillDataset(USP_CARTON_DETAIL,TBL_CARTON_DETAIL,new object[] { carton.ScanDataFirst23 });
                bRet = (ds.Tables[TBL_CARTON_DETAIL].Rows.Count > 0);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error reading carton details.", ex); }
            return bRet;
        }
        public static bool CreateCarton(Carton carton,string tripNumber,string stationNumber) {
            //Create a new carton
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_CARTON_CREATE,new object[] { carton.ScanDataFirst23,tripNumber,DateTime.Now,stationNumber });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating a carton in the database.",ex); }
            return ret;
        }
        public static bool DeleteCarton(Carton carton) {
            //Delete an existing carton
            bool ret=false;
            try {
                ret = App.Mediator.ExecuteNonQuery(USP_CARTON_DELETE,new object[] { carton.ScanDataFirst23 });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error deleting a carton from the database.",ex); }
            return ret;
        }
    }
}
