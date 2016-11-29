//	File:	freightfactory.cs
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
using Tsort.Enterprise;

namespace Tsort.Freight {
    //
    internal class FreightFactory {
        //Members
        public static Mediator Mediator=null;
        public static int DefaultIBLabelID = 999;
        public static InboundLabel DefaultSanInboundLabel=null;

        //Constants		
        public const string USP_IBLABEL_RET = "uspSortInboundLabelReturnsGet",TBL_IBLABEL_RET = "InboundLabelTable";
        public const string USP_IBLABEL_ELEMS = "uspSortInboundLabelDataElementsGet",TBL_IBLABEL_ELEMS = "InboundLabelDataElementTable";

        //Interface
        static FreightFactory() { }
        private FreightFactory() { }
        public static void RefreshCache() {
            //Refresh cached data
            try {
                DefaultSanInboundLabel = CreateInboundLabel(DefaultIBLabelID);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while caching FreightFactory data.",ex); }
        }
        public static InboundFreight CreateInboundFreight(int terminalID,string freightID,string freightType,int TDSNumber,string vendorKey,string trailerNumber,string pickupDate,string pickupNumber,decimal cubeRatio,Client client,Shipper shipper) {
            //
            InboundFreight shipment=null;
            try {
                InboundFreightDS freight = new InboundFreightDS();
                InboundFreightDS.InboundFreightTableRow row = freight.InboundFreightTable.NewInboundFreightTableRow();
                row.TerminalID = terminalID;
                row.FreightID = freightID;
                row.FreightType = freightType;
                row.TDSNumber = TDSNumber;
                row.VendorKey = vendorKey;
                row.TrailerNumber = trailerNumber;
                row.PickupDate = pickupDate;
                row.PickupNumber = pickupNumber;
                row.CubeRatio = cubeRatio;
                freight.InboundFreightTable.AddInboundFreightTableRow(row);
                shipment = new InboundFreight(freight.InboundFreightTable[0],client,shipper);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating inbound freight.",ex); }
            return shipment;
        }
        public static InboundLabel CreateInboundLabel(int labelID) {
            //Create an inbound label for returns freight
            InboundLabel label=null;
            try {
                DataSet ds = Mediator.FillDataset(USP_IBLABEL_RET,TBL_IBLABEL_RET,new object[] { labelID });
                if(ds.Tables[TBL_IBLABEL_RET].Rows.Count == 0)
                    throw new ApplicationException("Inbound label " + labelID.ToString() + " not found.");
                else {
                    InboundLabelDS labelDS = new InboundLabelDS();
                    labelDS.Merge(ds);
                    ds = Mediator.FillDataset(USP_IBLABEL_ELEMS,TBL_IBLABEL_ELEMS,new object[] { labelID });
                    if(ds != null) labelDS.Merge(ds);
                    label = new InboundLabel(labelDS);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating regular inbound label.",ex); }
            return label;
        }
    }
}
