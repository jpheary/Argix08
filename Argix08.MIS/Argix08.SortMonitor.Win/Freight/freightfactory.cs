//	File:	freightfactory.cs
//	Author:	J. Heary
//	Date:	04/24/07
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Tsort.Enterprise;
using Argix.Data;

namespace Tsort.Freight {
	//
	internal class FreightFactory {
		//Members
		public static Mediator Mediator=null;
        
        public static event EventHandler DataConnectionDropped=null;
		
		//Interface
		static FreightFactory() { 
            //Constructor
			try {
				//Init
			    Mediator = new SQLMediator();
                Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                RefreshCache();
            } 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating static FreightFactory instance.", ex); }
        }
		private FreightFactory() { }
        public static void RefreshCache() {
			//Refresh cached data
			try {
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while caching SortFactory data.", ex); }
		}
        #region Inbound Freight: GetInboundFreight(), CreateInboundFreight()
        public static DataSet GetInboundFreight() { return null; }
		public static InboundFreight CreateInboundFreight(int terminalID, string freightID, string freightType, int TDSNumber, string vendorKey, string trailerNumber, string pickupDate, string pickupNumber, decimal cubeRatio, Client client, Shipper shipper) {
			//
			InboundFreight freight=null;
			try {
				InboundFreightDS.DirectInboundFreightTableRow shipment = new InboundFreightDS().DirectInboundFreightTable.NewDirectInboundFreightTableRow();
				shipment.TerminalID = terminalID;
				shipment.FreightID = freightID;
				shipment.FreightType = freightType;
				shipment.TDSNumber = TDSNumber;
				shipment.VendorKey = vendorKey;
				shipment.TrailerNumber = trailerNumber;
				shipment.PickupDate = pickupDate;
				shipment.PickupNumber = pickupNumber;
				shipment.CubeRatio = cubeRatio;
				freight = new DirectInboundFreight(shipment, client, shipper);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating inbound freight.", ex); }
			return freight;
		}
		public static InboundFreight CreateInboundFreight(int terminalID, string freightID, string carrier, string trailerNumber, int cartons) {
			//
			InboundFreight freight=null;
			try {
				InboundFreightDS.IndirectInboundFreightTableRow trip = new InboundFreightDS().IndirectInboundFreightTable.NewIndirectInboundFreightTableRow();
				//trip.TerminalID = terminalID;
				trip.Number = freightID;
				trip.Carrier = carrier;
				trip.TrailerNumber = trailerNumber;
                trip.CartonCount = cartons;
				freight = new IndirectInboundFreight(trip);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating inbound freight.", ex); }
			return freight;
		}
        #endregion
        #region Outbound Freight: GetSortedItems(), GetScannedItems(), FindSortedItem(), FindScannedItem()
        public static OutboundFreightDS GetSortedItems(string stationNumber, DateTime start, DateTime end) {
			//Read sorted items for the specified station
            OutboundFreightDS sortedItems=null;
			try {
				sortedItems = new OutboundFreightDS();
				DataSet ds = Mediator.FillDataset(App.USP_ITEMSGETFORSTATION, App.TBL_ITEMSGET, new object[]{stationNumber, start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss")});
				if(ds != null) sortedItems.Merge(ds);
			}
			catch (Exception ex) { throw new ApplicationException("Unexpected error while reading sorted items for station# " + stationNumber + ".", ex); }
		    return sortedItems;
        }
        public static OutboundFreightDS GetScannedItems(string stationNumber, DateTime start, DateTime end) {
			//Read scanned items
            OutboundFreightDS scannedItems=null;
			try {
				scannedItems = new OutboundFreightDS();
				DataSet ds = Mediator.FillDataset(App.USP_SCANSGETFORSTATION, App.TBL_SCANSGET, new object[]{stationNumber, start.ToString("MM-dd-yyyy hh:mm:ss"), end.ToString("MM-dd-yyyy hh:mm:ss")});
				if(ds != null) scannedItems.Merge(ds);
			}
			catch (Exception ex) { throw new ApplicationException("Unexpected error while reading sorted items for station# " + stationNumber + ".", ex); }
		    return scannedItems;
        }
        public static string FindSortedItem(string labelSeqNum) {
			//Find a sorted item in the Sorted Items table
            DataSet ds = new DataSet();
            string item="";
            try {
                ds = Mediator.FillDataset(App.USP_SCANFINDINTABLE, App.TBL_SCANFINDINTABLE, new object[]{labelSeqNum});
                OutboundFreightDS itemDS = new OutboundFreightDS();
                itemDS.Merge(ds);
                if(itemDS.SortedItemTable.Rows.Count > 0) {
                    item += "SORT_DATE               STATION LABEL_SEQ_NUMBER VENDOR_ITEM_NUMBER        ScanString                     \n";
                    item += "----------------------- ------- ---------------- ------------------------- ------------------------------ \n";
                    item +=	itemDS.SortedItemTable[0].SORT_DATE.ToString("MM/dd/yyyy").PadRight(23, ' ') + " " + 
                            itemDS.SortedItemTable[0].STATION.PadRight(7, ' ') + " " + 
                            itemDS.SortedItemTable[0].LABEL_SEQ_NUMBER.PadRight(16, ' ') + " " + 
                            itemDS.SortedItemTable[0].VENDOR_ITEM_NUMBER.PadRight(25, ' ') + " " + 
                            itemDS.SortedItemTable[0].ScanString.PadRight(30, ' ') + "\n";
                }
                else {
                    item += "(0 row(s) returned)\n";
                }
                item += "\n";
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while searching for sorted item " + labelSeqNum + ".", ex); }
			return item;
		}
        public static string FindScannedItem(string scan) {
			//Find a carton scan in a Scan table
            DataSet ds = new DataSet();
            string item="";
            try {
                ds = Mediator.FillDataset(App.USP_SCANFINDINTABLE, App.TBL_SCANFINDINTABLE, new object[]{scan});
                OutboundFreightDS scanDS = new OutboundFreightDS();
                scanDS.Merge(ds);
                if(scanDS.BwareScanTable.Rows.Count > 0) {
                    item += "Scan                    TripNumber    Scanned                 Station Cube        \n";
                    item += "----------------------- ------------- ----------------------- ------- ----------- \n";
                    item +=	scanDS.BwareScanTable[0].Scan + " " + scanDS.BwareScanTable[0].TripNumber.PadRight(13, ' ') + " " + 
                            scanDS.BwareScanTable[0].Scanned.ToString("MM/dd/yyyy hh:mm:ss tt").PadRight(23, ' ') + " " + 
                            scanDS.BwareScanTable[0].Station.PadRight(7, ' ') + " " + scanDS.BwareScanTable[0].Cube.ToString().PadRight(11, ' ') + "\n";
                }
                else {
                    item += "(0 row(s) returned)\n";
                }
                item += "\n";
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while searching for scanned item " + scan + ".", ex); }
			return item;
		}
        #endregion
        #region Local Services: OnDataStatusUpdate()
        static void OnDataStatusUpdate(object source, DataStatusArgs e) {
            //Notifications from Mediator regarding connection status
            //Fire an event when connection drops
            if(!e.Online) 
                if(DataConnectionDropped != null) DataConnectionDropped(null, EventArgs.Empty);
        }
        #endregion
	}
}
