//	File:	terminal.cs
//	Author:	J. Heary
//	Date:	08/11/08
//	Desc:	Enterprise Terminal persistent class.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using Argix;

namespace Argix.Enterprise {
	//
	public class EnterpriseTerminal {
		//Members
		private int mTerminalID=0;					//Application operating terminal (global or local)
		private string mDescription="";
				
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
			} 
			catch(Exception ex) { throw new ApplicationException("Could not create new Enterprise Terminal.", ex); }
		}
		#region Accessors\Modifiers: [Members...], ToDataSet()
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
	}
}