using System;
using System.Collections;
using System.Data;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Enterprise {
	//
	public class EnterpriseService: IEnterpriseService {
		//Members
        private string mConnectionID="";
        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        public const string USP_ENTERPRISEAGENTS = "uspDCLocalTerminalGetList",TBL_ENTERPRISEAGENTS = "LocalTerminalTable";
        public const string USP_EQUIPTYPE = "uspDCEquipmentTypeGetList",TBL_EQUIPTYPE = "EquipmentTypeTable";
        
        //Interface
        public EnterpriseService(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
        }
        public TerminalInfo GetTerminalInfo() {
            //Get information about the local terminal for this service
            TerminalInfo info = null;
            try {
                info = new TerminalInfo();
                info.Connection = DatabaseFactory.CreateDatabase(this.mConnectionID).ConnectionStringWithoutCredentials;
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[] { });
                if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                    info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading terminal info.",ex); }
            return info;
        }
        public Terminals GetEnterpriseAgents() {
            //
            Terminals agents = new Terminals();
            DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_ENTERPRISEAGENTS,TBL_ENTERPRISEAGENTS,null);
            if(ds.Tables[TBL_ENTERPRISEAGENTS].Rows.Count > 0) {
                for(int i=0;i<ds.Tables[TBL_ENTERPRISEAGENTS].Rows.Count;i++) {
                    agents.Add(new Terminal(ds.Tables[TBL_ENTERPRISEAGENTS].Rows[i]["AgentNumber"].ToString(),ds.Tables[TBL_ENTERPRISEAGENTS].Rows[i]["Description"].ToString()));
                }
            }
            return agents;
        }
        public EquipmentTypes GetDriverEquipmentTypes() {
            //
            EquipmentTypes types = new EquipmentTypes();
            DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_EQUIPTYPE,TBL_EQUIPTYPE,null);
            if(ds.Tables[TBL_EQUIPTYPE].Rows.Count > 0) {
                for(int i=0;i<ds.Tables[TBL_ENTERPRISEAGENTS].Rows.Count;i++) {
                    types.Add(new EquipmentType(int.Parse(ds.Tables[TBL_EQUIPTYPE].Rows[i]["ID"].ToString()),ds.Tables[TBL_EQUIPTYPE].Rows[i]["Description"].ToString(),int.Parse(ds.Tables[TBL_EQUIPTYPE].Rows[i]["MPG"].ToString())));
                }
            }
            return types; 
        }
        public int GetDriverEquipmentMPG(int equipmentTypeID) {
            //Get MPG rating for the specified driver equipment typeID
            int mpg = 0;
            EquipmentTypes types = GetDriverEquipmentTypes();
            if(types.Count > 0) {
                for(int i=0;i<types.Count;i++) {
                    if(types[i].ID == equipmentTypeID) {
                        mpg = types[i].MPG;
                        break;
                    }
                }
            }
            return mpg;
        }
    }
}
