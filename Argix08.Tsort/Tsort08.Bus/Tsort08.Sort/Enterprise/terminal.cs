//	File:	terminal.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Represents an enterprise terminal.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Text;
using Tsort.Data;
using Tsort.Windows;

namespace Tsort.Enterprise {
	//
	public class EnterpriseTerminal {
		//Members
		private Mediator mMediator=null;
		private int mTerminalID=0;
		private string mDescription="?";
		
		//Constants
		private const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet", TBL_LOCALTERMINAL = "LocalTerminalTable";
		
		//Interface
		public EnterpriseTerminal(Mediator mediator) : this(null, mediator) { }
		public EnterpriseTerminal(TerminalDS.LocalTerminalTableRow terminal, Mediator mediator) {
			//Constructor
			this.mMediator = mediator;
			try {
				//Configure this terminal from the terminal configuration information
				if(terminal != null) {
					this.mTerminalID = terminal.TerminalID;
					this.mDescription = terminal.Description;
				}
				getLocalTerminal();
			} 
			catch(Exception ex) { throw ex; }
		}
		#region Accessors\Modifiers: ToString(), ToDataSet()
		public int TerminalID { get { return this.mTerminalID; } }
		public string Description { get { return this.mDescription; } }
		public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			TerminalDS ds=null;
			try {
				ds = new TerminalDS();
				TerminalDS.LocalTerminalTableRow terminal = ds.LocalTerminalTable.NewLocalTerminalTableRow();
				terminal.TerminalID = this.mTerminalID;
				terminal.Description = this.mDescription;
				ds.LocalTerminalTable.AddLocalTerminalTableRow(terminal);
			}
			catch(Exception) { }
			return ds;
		}
		public override string ToString() {
			//Custom ToString() method
			string sThis=base.ToString();
			try {
				//Form string detail of this object
				StringBuilder builder = new StringBuilder();
				builder.Append("TerminalID=" + this.TerminalID.ToString() + "\t");
				builder.Append("Description=" + this.mDescription + "\n");
				sThis = builder.ToString();
			} 
			catch(Exception) { }
			return sThis;
		}
		#endregion
		
		private void getLocalTerminal() {
			//Get the operating enterprise terminal
			try {
				TerminalDS terminal = new TerminalDS();
				DataSet ds = this.mMediator.FillDataset(USP_LOCALTERMINAL, TBL_LOCALTERMINAL, null);
				if(ds!=null) {
					terminal.Merge(ds);
					this.mTerminalID = terminal.LocalTerminalTable[0].TerminalID;
					this.mDescription = terminal.LocalTerminalTable[0].Description.Trim();
				}
			}
			catch (Exception) { }
		}
	}
}