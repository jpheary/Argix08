//	File:	terminal.cs
//	Author:	J. Heary
//	Date:	09/06/07
//	Desc:	Enteprise Terminal class.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Text;
using Tsort.Data;

namespace Tsort.Enterprise {
	//
	public class EnterpriseTerminal {
		//Members
		private Mediator mMediator=null;
		private int mTerminalID=0;
		private string mDescription="";
		
		//Constants
		//Events
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
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new EnterpriseTerminal instance.", ex); }
		}
		#region Accessors\Modifiers: [Members]... ToDataSet()
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
		#endregion
		private void getLocalTerminal() {
			//Get the operating enterprise terminal
			try {
				TerminalDS terminal = new TerminalDS();
				DataSet ds = this.mMediator.FillDataset(App.USP_LOCALTERMINAL, App.TBL_LOCALTERMINAL, null);
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