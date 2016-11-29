using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.Enterprise {
	//
	public class EnterpriseService {
		//Members
        public const string USP_CLIENT_DETAIL = "uspIndSortClientGet",TBL_CLIENT_DETAIL = "ClientDetailTable";
        public const string USP_STORE_DETAIL = "uspIndSortStoreGetForClient",TBL_STORE_DETAIL = "StoreDetailTable";
        public const string USP_VENDOR_DETAIL = "uspIndSortVendorGetForClient",TBL_VENDOR_DETAIL = "ClientVendorViewTable";
        public const string USP_ZONE_DETAIL = "uspIndSortZoneGet",TBL_ZONE_DETAIL = "ZoneDetailTable";
		
		//Interface
        static EnterpriseService() { }
		public static Client GetClient(string clientNumber) {
			//Create a client
			ClientDS client=null;
			try {
				client = new ClientDS();
                DataSet ds = App.Mediator.FillDataset(USP_CLIENT_DETAIL,TBL_CLIENT_DETAIL,new object[] { clientNumber });
				if(ds.Tables[TBL_CLIENT_DETAIL].Rows.Count == 0)
					throw new Exception("Client number " + clientNumber + " not found.");
				else
					client.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return new Client(client.ClientDetailTable[0]);
		}
		public static Store GetStore(string clientNumber, string storeNumber) { 
			//Create a store
			StoreDS store=null;
			try {
				store = new StoreDS();
                DataSet ds = App.Mediator.FillDataset(USP_STORE_DETAIL,TBL_STORE_DETAIL,new object[] { storeNumber,clientNumber });
				if(ds.Tables[TBL_STORE_DETAIL].Rows.Count == 0) 
					throw new Exception("Store number " + storeNumber + " for client number " + clientNumber + " not found.");
				else
					store.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return new Store(store.StoreDetailTable[0]);
		}
		public static ClientVendor GetClientVendor(string clientNumber, string vendorNumber) { 
			//Get vendor data
			ClientVendorDS clientVendor=null;
			try {
				clientVendor = new ClientVendorDS();
                DataSet ds = App.Mediator.FillDataset(USP_VENDOR_DETAIL,TBL_VENDOR_DETAIL,new object[] { vendorNumber,clientNumber });
				if(ds.Tables[TBL_VENDOR_DETAIL].Rows.Count == 0) 
					throw new Exception("Client number " + clientNumber + " vendor " + vendorNumber + " not found.");
				else
					clientVendor.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return new ClientVendor(clientVendor.ClientVendorViewTable[0]);
		}
		public static Zone GetZone(string code, string type) {
			//Create a zone
			ZoneDS zone=null;
			try {
				zone = new ZoneDS();
                DataSet ds = App.Mediator.FillDataset(USP_ZONE_DETAIL,TBL_ZONE_DETAIL,new object[] { code,type });
				if(ds.Tables[TBL_ZONE_DETAIL].Rows.Count == 0) 
					throw new Exception("Zone  " + code + " not found.");
				else
					zone.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return new Zone(zone.ZoneDetailTable[0]);
		}
	}
}
