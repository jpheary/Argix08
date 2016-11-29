//	File:	enterprisefactory.cs
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

namespace Tsort.Enterprise {
    //
    internal class EnterpriseFactory {
        //Members
        public static Mediator Mediator=null;

        //Constants
        private const string USP_CLIENT_DETAIL_SAN = "uspSortClientGetForSan",TBL_CLIENT_DETAIL="ClientDetailTable";

        //Interface
        static EnterpriseFactory() { }
        private EnterpriseFactory() { }
        public static void RefreshCache() {
            //Refresh cached data
            try {
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while caching EnterpriseFactory data.",ex); }
        }
        public static Client CreateClient(string number,string division,string name,string addressLine1,string addressLine2,string addressCity,string addressState,string addressZip) {
            //Create a client
            Client client=null;
            try {
                client = new Client(number,division,name,addressLine1,addressLine2,addressCity,addressState,addressZip);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client.",ex); }
            return client;
        }
        public static Client CreateClientForStoreSAN(string division,string sanNumber) {
            //Create a client based on Store san
            Client client=null;
            try {
                DataSet ds = Mediator.FillDataset(USP_CLIENT_DETAIL_SAN,TBL_CLIENT_DETAIL,new object[] { division,sanNumber });
                if(ds.Tables[TBL_CLIENT_DETAIL].Rows.Count == 0)
                    throw new ApplicationException("Client for san number " + sanNumber + " not found.");
                else {
                    ClientDS clientDS = new ClientDS();
                    clientDS.Merge(ds);
                    client = new Client(clientDS.ClientDetailTable[0]);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client.",ex); }
            return client;
        }
        public static Shipper CreateShipper(string freightType,string number,string name,string addressLine1,string addressLine2,string addressCity,string addressState,string addressZip,string userData) {
            //Create a shipper
            Shipper oShipper=null;
            switch(freightType.ToLower()) {
                case "tsort": oShipper = CreateVendor(number,name,addressLine1,addressLine2,addressCity,addressState,addressZip,userData); break;
                case "returns": oShipper = CreateAgent(number,name,addressLine1,addressLine2,addressCity,addressState,addressZip); break;
            }
            return oShipper;
        }
        public static Vendor CreateVendor(string number,string name,string addressLine1,string addressLine2,string addressCity,string addressState,string addressZip,string userData) {
            //Create a vendor
            Vendor vendor=null;
            try {
                vendor = new Vendor(number,name,addressLine1,addressLine2,addressCity,addressState,addressZip,userData);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor.",ex); }
            return vendor;
        }
        public static Agent CreateAgent(string number,string name,string addressLine1,string addressLine2,string addressCity,string addressState,string addressZip) {
            //Create an agent
            Agent agent=null;
            try {
                agent = new Agent(number,name,addressLine1,addressLine2,addressCity,addressState,addressZip);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating agent.",ex); }
            return agent;
        }
    }
}
