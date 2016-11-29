//	File:	enterprisefactory.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Factory for enterprise objects.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Argix.Data;

namespace Argix.Enterprise {
	//
	public class EnterpriseFactory {
		//Members
        private static EnterpriseTerminal _EntTerminal = null;
        private static DataSet _EntAgents = null;
        private static DataSet _EquipmentTypes = null;
        public static Mediator Mediator = null;

        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        public const string USP_ENTERPRISEAGENTS = "uspDCLocalTerminalGetList",TBL_ENTERPRISEAGENTS = "LocalTerminalTable";
        public const string USP_EQUIPTYPE = "uspDCEquipmentTypeGetList",TBL_EQUIPTYPE = "EquipmentTypeTable";
        
        //Interface
		static EnterpriseFactory() { }
		private EnterpriseFactory() { }
		public static void RefreshCache() {
			//Refresh cached data
			try {
                _EntAgents = new DataSet();
                DataSet ds = Mediator.FillDataset(USP_ENTERPRISEAGENTS,TBL_ENTERPRISEAGENTS,null);
                if(ds.Tables[TBL_ENTERPRISEAGENTS].Rows.Count > 0) _EntAgents.Merge(ds);

                _EquipmentTypes = new DataSet();
                ds = Mediator.FillDataset(USP_EQUIPTYPE,TBL_EQUIPTYPE,null);
                if(ds.Tables[TBL_EQUIPTYPE].Rows.Count > 0) _EquipmentTypes.Merge(ds);
            }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while caching EnterpriseFactory data.", ex); }
		}
        public static EnterpriseTerminal LocalTerminal { 
            get {
                if(_EntTerminal == null || _EntTerminal.TerminalID == 0) {
                    TerminalDS terminals = getLocalTerminal();
                    if(terminals.LocalTerminalTable.Rows.Count > 0) 
                        _EntTerminal = new EnterpriseTerminal(terminals.LocalTerminalTable[0]);
                    else
                        _EntTerminal = new EnterpriseTerminal();
                }
                return _EntTerminal; 
            } 
        }
        public static DataSet EnterpriseAgents { get { return _EntAgents; } }
        public static DataSet DriverEquipmentTypes { get { return _EquipmentTypes; } }
        public static int GetDriverEquipmentMPG(int equipmentTypeID) {
            //Get MPG rating for the specified driver equipment typeID
            int mpg = 0;
            if(_EquipmentTypes.Tables[TBL_EQUIPTYPE].Rows.Count > 0) {
                DataRow[] types = _EquipmentTypes.Tables[TBL_EQUIPTYPE].Select("ID=" + equipmentTypeID);
                if(types.Length > 0)
                    mpg = Convert.ToInt32(types[0]["MPG"]);
            }
            return mpg;
        }

        internal static TerminalDS getLocalTerminal() {
            //Get the operating enterprise terminal
            TerminalDS terminal=null;
            try {
                terminal = new TerminalDS();
                DataSet ds = Mediator.FillDataset(USP_LOCALTERMINAL,TBL_LOCALTERMINAL,null);
                if(ds != null) terminal.Merge(ds);
            }
            catch(Exception) { }
            return terminal;
        }
    }
}
