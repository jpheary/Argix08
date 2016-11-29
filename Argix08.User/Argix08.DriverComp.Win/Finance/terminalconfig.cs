//	File:	terminalconfig.cs
//	Author:	jheary
//	Date:	08/20/08
//	Desc:	
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;

namespace Argix.Finance {
	//
	public class TerminalConfiguration {
		//Members
        private string mAgentNumber = "";
        private string mAgentName = "";
        private string mGLNumber = "";
        private string mAdminGLNumber = "";
        private decimal mAdminFee=0.0M;
        private decimal mFSBase=0.0M;

		//Interface
        public TerminalConfiguration(DriverCompDS.TerminalConfigurationTableRow config) {
            //Constructor
            try {
                if(config != null) {
                    this.mAgentNumber = config.AgentNumber;
                    this.mAgentName = config.AgentName;
                    this.mGLNumber = config.GLNumber;
                    this.mAdminGLNumber = config.AdminGLNumber;
                    this.mAdminFee = config.AdminFee;
                    this.mFSBase = config.FSBase;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new terminal configuration instance.",ex); }
        }
        public TerminalConfiguration(string agentNumber,string agentName,string glNumber,string adminGLNumber,decimal adminFee,decimal fsBase) {
            //Constructor
            try {
                this.mAgentNumber = agentNumber;
                this.mAgentName = agentName;
                this.mGLNumber = glNumber;
                this.mAdminGLNumber = adminGLNumber;
                this.mAdminFee = adminFee;
                this.mFSBase = fsBase;
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new terminal configuration instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...], ToDataSet()
        public string AgentNumber { get { return this.mAgentNumber; } }
        public string AgentName { get { return this.mAgentName; } }
        public string GLNumber { get { return this.mGLNumber; } }
        public string AdminGLNumber { get { return this.mAdminGLNumber; } }
        public decimal AdminFee { get { return this.mAdminFee; } }
        public decimal FSBase { get { return this.mFSBase; } }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            DriverCompDS ds = new DriverCompDS();
            ds.TerminalConfigurationTable.AddTerminalConfigurationTableRow(this.mAgentNumber,this.mAgentName,this.mGLNumber,this.mAdminGLNumber,this.mAdminFee,this.mFSBase);
            ds.AcceptChanges();
            return ds;
        }
        #endregion
    }
}
