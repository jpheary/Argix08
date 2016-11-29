//	File:	terminal.cs
//	Author:	J. Heary
//	Date:	01/23/09
//	Desc:	Enteprise Terminal class. Can be used as the top object in the 
//          application design model, or to return enterprise objects (i.e. clients).
//          NOTE: Uses USP_LOCALTERMINAL defined in App class (globals.cs) to 
//          determine operating terminal (i.e. Jamesburg @ JARGXTS).
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Text;
using Argix.Data;

namespace Tsort.Enterprise {
	//
	public class EnterpriseTerminal {
		//Members
		protected int mTerminalID=0;
		protected string mDescription="???";
		
		//Interface
		public EnterpriseTerminal() : this(null) { }
		public EnterpriseTerminal(TerminalDS.LocalTerminalTableRow terminal) {
			//Constructor
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
		#region Accessors\Modifiers: TerminalID, Description, ToDataSet()
		public int TerminalID { get { return this.mTerminalID; } }
		public string Description { get { return this.mDescription; } }
		public virtual DataSet ToDataSet() {
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
				DataSet ds = App.Mediator.FillDataset(App.USP_LOCALTERMINAL, App.TBL_LOCALTERMINAL, null);
				if(ds != null) {
					terminal.Merge(ds);
					this.mTerminalID = terminal.LocalTerminalTable[0].TerminalID;
					this.mDescription = terminal.LocalTerminalTable[0].Description.Trim();
				}
			}
			catch (Exception) { }
		}
	}
}