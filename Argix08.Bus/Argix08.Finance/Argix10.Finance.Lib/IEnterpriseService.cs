using System;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace Argix.Enterprise {
	//
    public interface IEnterpriseService {
        //Interface
        TerminalInfo GetTerminalInfo();
        Terminals GetEnterpriseAgents();
        EquipmentTypes GetDriverEquipmentTypes();
        int GetDriverEquipmentMPG(int equipmentTypeID);
    }

    public class TerminalInfo {
        private int mTerminalID=0;
        private string mNumber="",mDescription="",mConnection="";

        public TerminalInfo() { }
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        public string Connection { get { return this.mConnection; } set { this.mConnection = value; } }
    }

    public class Terminals:BindingList<Terminal> {
        public Terminals() { }
    }

    public class Terminal {
        //Members
        private int _terminalID=0;
        private string _number = "",_description = "";
        private string _dbservername="",_linkedservername="",_dbtype="";
        private int _agentID = 0;
        private string _agentnumber="",_shipperid="",_clientdivision="",_mnemonic="";
        private long _locationID=0;
        private byte _isactive = (byte)1;

        //Interface
        public Terminal(int terminalID,string number,string description,int agentID,string agentNumber) {
            //Constructor
            this._terminalID = terminalID;
            this._number = number;
            this._description = description;
            this._agentID = agentID;
            this._agentnumber = agentNumber;
        }
        public Terminal(string agentnumber,string description) {
            //Constructor
            this._agentnumber = agentnumber;
            this._description = description;
        }
        #region Accessors\Modifiers: [Members...]
        public int TerminalID { get { return this._terminalID; } set { this._terminalID = value; } }
        public string Number { get { return this._number; } set { this._number = value; } }
        public string Description { get { return this._description; } set { this._description = value; } }
        public string DBServerName { get { return this._dbservername; } set { this._dbservername = value; } }
        public string LinkedServerName { get { return this._linkedservername; } set { this._linkedservername = value; } }
        public string DBtype { get { return this._dbtype; } set { this._dbtype = value; } }
        public int AgentID { get { return this._agentID; } set { this._agentID = value; } }
        public string AgentNumber { get { return this._agentnumber; } set { this._agentnumber = value; } }
        public string ShipperID { get { return this._shipperid; } set { this._shipperid = value; } }
        public string ClientDivision { get { return this._clientdivision; } set { this._clientdivision = value; } }
        public long LocationID { get { return this._locationID; } set { this._locationID = value; } }
        public string Mnemonic { get { return this._mnemonic; } set { this._mnemonic = value; } }
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        #endregion
    }

    public class EquipmentTypes:BindingList<EquipmentType> {
        public EquipmentTypes() { }
    }
    
    public class EquipmentType {
        private int mID=0,mMPG=0;
        private string mDescription="";

        public EquipmentType() { }
        public EquipmentType(int id, string description, int mpg) {
            this.mID = id;
            this.mDescription = description;
            this.mMPG = mpg;
        }
        #region Accessors\Modifiers: [Members...]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        public int MPG { get { return this.mMPG; } set { this.mMPG = value; } }
        #endregion
    }

}