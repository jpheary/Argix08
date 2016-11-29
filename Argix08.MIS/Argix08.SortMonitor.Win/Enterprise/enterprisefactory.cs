//	File:	enterprisefactory.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Tsort.Enterprise {
	//
	internal class EnterpriseFactory {
		//Members
		public static Mediator Mediator=null;
		public static Hashtable Terminals=null;
        public static Hashtable SortStations=null;

		private const string KEY_TERMINALS = "enterprise/terminal";
        
        public static event EventHandler TerminalsChanged=null;
        public static event EventHandler SortStationsChanged=null;
		public static event EventHandler DataConnectionDropped=null;

		//Interface
		static EnterpriseFactory() { 
        	//Constructor
			try {
				//Init
                Mediator = new SQLMediator();
                Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                Terminals = new Hashtable();
        	    SortStations = new Hashtable();
                RefreshCache();
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating static EnterpriseFactory instance.", ex); }
        }
		private EnterpriseFactory() { }
		public static void RefreshCache() {
			//Refresh cached data
			try {
                RefreshTerminals();
                RefreshSortStations();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while caching Enterprise data.", ex); }
		}
        public static void RefreshTerminals() {
			//Load enterprise terminals
			try {
				Terminals.Clear();
				IDictionary oDict = (IDictionary)ConfigurationManager.GetSection(KEY_TERMINALS + "s");
				int terminals = Convert.ToInt32(oDict["Count"]);
				for(int i=1; i<=terminals; i++) {
					oDict = (IDictionary)ConfigurationManager.GetSection(KEY_TERMINALS + i.ToString());
                    TerminalDS.DBATerminalTableRow row = new TerminalDS().DBATerminalTable.NewDBATerminalTableRow();
                    row.TerminalID = int.Parse(oDict["TerminalID"].ToString());
					row.Description = oDict["Description"].ToString();
					row.SQLConnection = oDict["SQLConnection"].ToString();
                    string ttype = oDict["TerminalType"].ToString();
                    EnterpriseTerminal terminal=null;
                    switch(ttype) {
                        case "Tsort": terminal = new TsortTerminal(row); break;
                        case "Local": terminal = new LocalTerminal(row); break;
                    }
				    Terminals.Add(terminal.TerminalID, terminal);
				}
            }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing enterprise terminals.", ex); }
			finally { if(TerminalsChanged!=null) TerminalsChanged(null, EventArgs.Empty); }
        }		
        public static EnterpriseTerminal GetTerminal(int terminalID) { 
			//Get a terminal
			EnterpriseTerminal terminal=null;
			IDictionaryEnumerator oEnum = Terminals.GetEnumerator();
			while(oEnum.MoveNext()) {
				DictionaryEntry entry = (DictionaryEntry)oEnum.Current;
				if(int.Parse(entry.Key.ToString()) == terminalID) {
					terminal = (EnterpriseTerminal)entry.Value;
					break;
				}
			}
			return terminal;
		}
        public static void RefreshSortStations() {
			//Load sort stations for this terminal
			try {
        	    SortStations.Clear();
				WorkstationDS workstations = new WorkstationDS();
				workstations.Merge(Mediator.FillDataset(App.USP_SORTSTATIONS, App.TBL_SORTSTATIONS, null));
				for(int i=0; i<workstations.WorkstationDetailTable.Rows.Count; i++) {
                    EnterpriseTerminal terminal = GetTerminal(workstations.WorkstationDetailTable[i].TerminalID);
				    SortStation station = new SortStation(workstations.WorkstationDetailTable[i], terminal);
				    SortStations.Add(station.Number.Trim().PadLeft(3, '0'), station);
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing sort stations.", ex); }
			finally { if(SortStationsChanged!=null) SortStationsChanged(null, EventArgs.Empty); }
        }		
        public static SortStation GetStation(string number) { 
			//Get a station
			SortStation station=null;
			IDictionaryEnumerator oEnum = SortStations.GetEnumerator();
			while(oEnum.MoveNext()) {
				DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
				if(oEntry.Key.ToString() == number.Trim().PadLeft(3, '0')) {
					station = (SortStation)oEntry.Value;
					break;
				}
			}
			return station;
		}
		public static Client CreateClient(string number, string division, string name, string addressLine1, string addressLine2, string addressCity, string addressState, string addressZip) {
			//Create a client
			Client client=null;
			try {
				client = new Client(number, division, name, addressLine1, addressLine2, addressCity, addressState, addressZip);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client.", ex); }
			return client;
		}
		public static Shipper CreateShipper(string freightType, string number, string name, string addressLine1, string addressLine2, string addressCity, string addressState, string addressZip, string userData) { 
			//Create a shipper
			Shipper oShipper=null;
			switch(freightType.ToLower()) {
				case "tsort":	oShipper = CreateVendor(number, name, addressLine1, addressLine2, addressCity, addressState, addressZip, userData); break;
				case "returns":	oShipper = CreateAgent(number, name, addressLine1, addressLine2, addressCity, addressState, addressZip); break;
			}
			return oShipper;
		}
		public static Vendor CreateVendor(string number, string name, string addressLine1, string addressLine2, string addressCity, string addressState, string addressZip, string userData) { 
			//Create a vendor
			Vendor vendor=null;
			try {
				vendor = new Vendor(number, name, addressLine1, addressLine2, addressCity, addressState, addressZip, userData);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor.", ex); }
			return vendor;
		}
		public static Agent CreateAgent(string number, string name, string addressLine1, string addressLine2, string addressCity, string addressState, string addressZip) {
			//Create an agent
			Agent agent=null;
			try {
				agent = new Agent(number, name, addressLine1, addressLine2, addressCity, addressState, addressZip);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected exception creating agent.", ex); }
			return agent;
            }
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
