//	File:	enterpriseterminal.cs
//	Author:	J. Heary
//	Date:	10/28/04
//	Desc:	Enteprise Terminal class.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Text;
using Argix.Data;
using Tsort.Freight;

namespace Tsort.Enterprise {
	//
	public class EnterpriseTerminal {
		//Members
		protected Mediator mMediator=null;
		protected int mTerminalID=0;
		protected string mDescription="";
				
		//Interface
		public EnterpriseTerminal() : this(null) { }
		public EnterpriseTerminal(TerminalDS.DBATerminalTableRow terminal) {
			//Constructor
			try {
				//Configure this terminal from the terminal configuration information
				if(terminal != null) {
					this.mTerminalID = terminal.TerminalID;
					this.mDescription = terminal.Description;
                    this.mMediator = new SQLMediator(terminal.SQLConnection);
				}
				//getLocalTerminal();
			} 
			catch(Exception ex) { throw ex; }
		}
        #region Accessors\Modifiers: TerminalID, Description, ToDataSet()
		public int TerminalID { get { return this.mTerminalID; } }
		public string Description { get { return this.mDescription; } }
        public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			TerminalDS ds=null;
			try {
				ds = new TerminalDS();
				TerminalDS.DBATerminalTableRow terminal = ds.DBATerminalTable.NewDBATerminalTableRow();
				terminal.TerminalID = this.mTerminalID;
				terminal.Description = this.mDescription;
				ds.DBATerminalTable.AddDBATerminalTableRow(terminal);
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
        #region Trace Log: GetLogEvents(), FindLogEvents(), FindSortedItem()
		public ArgixLogDS GetLogEvents(DateTime startDate, DateTime endDate) {
			//Read log events
            ArgixLogDS logDS=null;
			try {
				logDS = new ArgixLogDS();
				DataSet ds = this.mMediator.FillDataset(App.USP_LOGGET, App.TBL_LOGGET, new object[]{startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss")});
				if(ds != null) logDS.Merge(ds);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while reading log events.", ex); }
		    return logDS;
        }
        public ArgixLogDS GetLogEvents(DateTime startDate, DateTime endDate, string stationName) {
			//Read log events
            ArgixLogDS logDS=null;
			try {
				logDS = new ArgixLogDS();
				DataSet ds = this.mMediator.FillDataset(App.USP_LOGGETFORSTATION, App.TBL_LOGGET, new object[]{stationName, startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss")});
				if(ds != null) logDS.Merge(ds);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while reading log events for station " + stationName + ".", ex); }
		    return logDS;
        }
		public string FindLogEvents(DateTime startDate, DateTime endDate, string logEvent) {
			//Find all occurrences of a log event
			DataSet ds = new DataSet();
			string sEvents="";
			try {
				ds = this.mMediator.FillDataset(App.USP_LOGGETFOREVENT, App.TBL_LOGGET, new object[]{logEvent, startDate.ToString("yyyy-MM-dd HH:mm:ss"), endDate.ToString("yyyy-MM-dd HH:mm:ss")});
				ArgixLogDS dsLog = new ArgixLogDS();
				dsLog.Merge(ds);
				if(dsLog.ArgixLogTable.Rows.Count > 0) {
					sEvents += "ID                    Name                           Level       Date                    Source                         Category                       Event                          User                           Computer                       Keyword1                       Keyword2                       Keyword3                       Message                                                                                                                                                                                                                                                          \n";
					sEvents += "--------------------- ------------------------------ ----------- ----------------------- ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- \n";
					for(int i=0; i<dsLog.ArgixLogTable.Rows.Count; i++) {
						sEvents +=	dsLog.ArgixLogTable[i].ID.ToString().PadRight(21, ' ') + " " + dsLog.ArgixLogTable[i].Name.PadRight(30, ' ') + " " + 
									dsLog.ArgixLogTable[i].Level.ToString().PadRight(11, ' ') + " " + dsLog.ArgixLogTable[i].Date.ToString("MM/dd/yyyy HH:mm:ss tt").PadRight(23, ' ') + " " + 
									dsLog.ArgixLogTable[i].Source.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Category.PadRight(30, ' ') + " " + 
									dsLog.ArgixLogTable[i].Event.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].User.PadRight(30, ' ') + " " + 
									dsLog.ArgixLogTable[i].Computer.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Keyword1.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Keyword2.PadRight(30, ' ') + " " + 
									dsLog.ArgixLogTable[i].Keyword3.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Message + "\n";
					}
				}
				else {
					sEvents += "(0 row(s) returned)\n";
				}
				sEvents += "\n";
			}
			catch(Exception ex) { throw ex; }
			return sEvents;
		}
        public string FindScanEvent(string labelSeqNum) {
			//Find a carton scan in the Argix Log table
            DataSet ds = new DataSet();
            string sScans="";
            try {
                ds = this.mMediator.FillDataset(App.USP_SCANFINDINLOG, App.TBL_SCANFINDINLOG, new object[]{labelSeqNum});
                ArgixLogDS dsLog = new ArgixLogDS();
                dsLog.Merge(ds);
                if(dsLog.ArgixLogTable.Rows.Count > 0) {
                    sScans += "ID                    Name                           Level       Date                    Source                         Category                       Event                          User                           Computer                       Keyword1                       Keyword2                       Keyword3                       Message                                                                                                                                                                                                                                                          \n";
                    sScans += "--------------------- ------------------------------ ----------- ----------------------- ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ------------------------------ ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- \n";
                    for(int i=0; i<dsLog.ArgixLogTable.Rows.Count; i++) {
                        sScans +=	dsLog.ArgixLogTable[i].ID.ToString().PadRight(21, ' ') + " " + dsLog.ArgixLogTable[i].Name.PadRight(30, ' ') + " " + 
                                    dsLog.ArgixLogTable[i].Level.ToString().PadRight(11, ' ') + " " + dsLog.ArgixLogTable[i].Date.ToString("MM/dd/yyyy hh:mm:ss tt").PadRight(23, ' ') + " " + 
                                    dsLog.ArgixLogTable[i].Source.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Category.PadRight(30, ' ') + " " + 
                                    dsLog.ArgixLogTable[i].Event.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].User.PadRight(30, ' ') + " " + 
                                    dsLog.ArgixLogTable[i].Computer.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Keyword1.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Keyword2.PadRight(30, ' ') + " " + 
                                    dsLog.ArgixLogTable[i].Keyword3.PadRight(30, ' ') + " " + dsLog.ArgixLogTable[i].Message + "\n";
                    }
                }
                else {
                    sScans += "(0 row(s) returned)\n";
                }
                sScans += "\n";
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while searching for sorted item " + labelSeqNum + ".", ex); }
			return sScans;
		}
        #endregion
        #region Local Services: getLocalTerminal()
        private void getLocalTerminal() {
			//Get the operating enterprise terminal
			try {
				TerminalDS terminal = new TerminalDS();
				DataSet ds = this.mMediator.FillDataset(App.USP_LOCALTERMINAL, App.TBL_LOCALTERMINAL, null);
				if(ds != null) {
					terminal.Merge(ds);
					this.mTerminalID = terminal.DBATerminalTable[0].TerminalID;
					this.mDescription = terminal.DBATerminalTable[0].Description.Trim();
				}
			}
			catch (Exception) { }
        }
        #endregion
    }

    public class TsortTerminal: EnterpriseTerminal {
		//Members		
		private Hashtable mDirectAssignments=null;
                
        public event EventHandler DirectAssignmentsChanged=null;
		
        //Interface
		public TsortTerminal() : this(null) { }
		public TsortTerminal(TerminalDS.DBATerminalTableRow terminal): base(terminal) {
			//Constructor
			try {
				//Configure this terminal from the terminal configuration information
                this.mDirectAssignments = new Hashtable();
            } 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new TsortTerminal instance.", ex); }
		}
		#region Accessors\Modifiers: DirectAssignments
        public Hashtable DirectAssignments { get { return this.mDirectAssignments; } }
		#endregion
		public void RefreshDirectAssignments() {
			//Load direct freight assignments
			try {
				this.mDirectAssignments.Clear();
				StationAssignmentDS assignments = new StationAssignmentDS();
				assignments.Merge(this.mMediator.FillDataset(App.USP_DIRECTASSIGNMENTS, App.TBL_DIRECTASSIGNMENTS, null));
            	foreach(StationAssignmentDS.DirectAssignmentTableRow row in assignments.DirectAssignmentTable) {
					SortStation station = EnterpriseFactory.GetStation(row.StationNumber);
                    Client client = EnterpriseFactory.CreateClient(row.ClientNumber, row.ClientDivision, row.Client, "", "", "", "", "");
					Shipper shipper = EnterpriseFactory.CreateShipper(row.FreightType, row.ShipperNumber, row.Shipper, "", "", "", "", "", "");
					InboundFreight freight = FreightFactory.CreateInboundFreight(row.TerminalID, row.FreightID, row.FreightType, row.TDSNumber, "", row.TrailerNumber, row.Pickup, row.Pickup, 0, client, shipper);
					this.mDirectAssignments.Add(station.Number + freight.FreightID, new StationFreightAssignment(station, freight, row.SortType));
				}
			}
			catch(Exception ex) { throw ex; }
			finally { if(DirectAssignmentsChanged!=null) DirectAssignmentsChanged(this, EventArgs.Empty); }
		}
	    #region Outbound Freight: GetSortedItems(), GetScannedItems(), FindSortedItem(), FindScannedItem()
        public OutboundFreightDS GetSortedItems(string stationNumber, DateTime start, DateTime end) {
			//Read sorted items for the specified station
            OutboundFreightDS sortedItems=null;
			try {
				sortedItems = new OutboundFreightDS();
				DataSet ds = this.mMediator.FillDataset(App.USP_ITEMSGETFORSTATION, App.TBL_ITEMSGET, new object[]{stationNumber, start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss")});
				if(ds != null) sortedItems.Merge(ds);
			}
			catch (Exception ex) { throw new ApplicationException("Unexpected error while reading sorted items for station# " + stationNumber + ".", ex); }
		    return sortedItems;
        }
        public string FindSortedItem(string labelSeqNum) {
			//Find a sorted item in the Sorted Items table
            DataSet ds = new DataSet();
            string item="";
            try {
                ds = this.mMediator.FillDataset(App.USP_SCANFINDINTABLE, App.TBL_SCANFINDINTABLE, new object[]{labelSeqNum});
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
        #endregion
}

    public class LocalTerminal: TsortTerminal {
		//Members		
		private Hashtable mIndirectAssignments=null;

        public event EventHandler IndirectAssignmentsChanged=null;
		
        //Interface
		public LocalTerminal() : this(null) { }
		public LocalTerminal(TerminalDS.DBATerminalTableRow terminal): base(terminal) {
			//Constructor
			try {
				//Configure this terminal from the terminal configuration information
                this.mIndirectAssignments = new Hashtable();
            } 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new LocalTerminal instance.", ex); }
		}
		#region Accessors\Modifiers: IndirectAssignments
        public Hashtable IndirectAssignments { get { return this.mIndirectAssignments; } }
        #endregion
		public void RefreshIndirectAssignments() {
			//Load trip assignments
			try {
				this.mIndirectAssignments.Clear();
				StationAssignmentDS assignments = new StationAssignmentDS();
				assignments.Merge(this.mMediator.FillDataset(App.USP_INDIRECTASSIGNMENTS, App.TBL_INDIRECTASSIGNMENTS, null));
				foreach(StationAssignmentDS.IndirectAssignmentTableRow row in assignments.IndirectAssignmentTable) {
					SortStation station = EnterpriseFactory.GetStation(row.StationNumber);
					InboundFreight freight = FreightFactory.CreateInboundFreight(0, row.TripNumber, "", "", 0);
					this.mIndirectAssignments.Add(station.Number + freight.FreightID, new StationFreightAssignment(station, freight, ""));
				}
			}
			catch(Exception ex) { throw ex; }
			finally { if(IndirectAssignmentsChanged!=null) IndirectAssignmentsChanged(this, EventArgs.Empty); }
		}
	    public OutboundFreightDS GetScannedItems(string stationNumber, DateTime start, DateTime end) {
			//Read scanned items
            OutboundFreightDS scannedItems=null;
			try {
				scannedItems = new OutboundFreightDS();
				DataSet ds = this.mMediator.FillDataset(App.USP_SCANSGETFORSTATION, App.TBL_SCANSGET, new object[]{stationNumber, start.ToString("yyyy-MM-dd HH:mm:ss"), end.ToString("yyyy-MM-dd HH:mm:ss")});
				if(ds != null) scannedItems.Merge(ds);
			}
			catch (Exception ex) { throw new ApplicationException("Unexpected error while reading sorted items for station# " + stationNumber + ".", ex); }
		    return scannedItems;
        }
        public string FindScannedItem(string scan) {
			//Find a carton scan in a Scan table
            DataSet ds = new DataSet();
            string item="";
            try {
                ds = this.mMediator.FillDataset(App.USP_SCANFINDINTABLE, App.TBL_SCANFINDINTABLE, new object[]{scan});
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
    }
}