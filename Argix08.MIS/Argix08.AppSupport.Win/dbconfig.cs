using System;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using Argix.Configuration;
using Argix.Data;
using Argix.Windows;

namespace Argix.MIS {
	//
	public class DBConfig {
		//Members
		private string mExeFilename="";				//The file from which the product name is extracted
		private string mProductName="";				//Application key in database configuration table
		private Config mFileConfig=null;		//For database connection string
		private Mediator mMediator=null;			//Database connectivity
		private AppConfigDS mConfigEntries=null;	//Collection of database configuration entries 

        public const string USP_CONFIGURATION = "uspToolsConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        
        public event EventHandler Refreshed=null;
		
		//Interface
		public DBConfig(string exeName, Config fileConfig) {
			//Constructor
			try {
				//Initialize members
				this.mExeFilename = exeName;
				this.mFileConfig = fileConfig;
				this.mFileConfig.Changed += new EventHandler(OnConfigurationChanged);
				this.mConfigEntries = new AppConfigDS();
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating new DBConfig instance.",ex); }
        }
		#region Accessors\Modifiers
		public string ExeFilename { get { return this.mExeFilename; } }
		public string ProductName { 
			get { 
				//Return the application product name for specified exe
				try {
					//Do this once to avoid multiple network access calls
					if(this.mProductName == "") {
						Assembly oAssy = Assembly.LoadFile(this.mExeFilename);
						object[] o = oAssy.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
						AssemblyProductAttribute att = (AssemblyProductAttribute)o[0];
						this.mProductName = att.Product;
					}
				}
				catch(Exception) { }
				return this.mProductName;
			} 
		}
		private Config FileConfiguration { get { return this.mFileConfig; } }
		public string ConnectionInfo { 
			get { 
				//Return connection informaton
				string conn="";
				try {
					string[] tokens = this.mFileConfig.DBConnectionString.Split((char)';');
					conn = (tokens.Length > 0) ? tokens[0] : "Data Source=?";
					conn += "; " + ((tokens.Length > 1) ? tokens[1] : "Initial Catalog=?");
				}
				catch(Exception) { }
				return conn; 
			} 
		}
		#endregion
		public AppConfigDS ConfigurationEntries { get { return this.mConfigEntries; } }
		public void Refresh() { 
			//Refresh data configuration for this object
			try {
				//Clear existing entries and refresh
				this.mConfigEntries.Clear();
				this.mMediator = new SQLMediator(this.mFileConfig.DBConnectionString);
				this.mMediator.DataStatusUpdate += new DataStatusHandler(frmMain.OnDataStatusUpdate);
				this.mConfigEntries.Merge(this.mMediator.FillDataset(USP_CONFIGURATION, TBL_CONFIGURATION, new object[]{this.ProductName}));
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error refreshing configuration entries.",ex); }
            finally { if(this.Refreshed != null)this.Refreshed(this,EventArgs.Empty); }
		}
		public bool Add(DBConfigEntry entry) {
			//Add a new configuration entry
			bool bRet=false;
			try { 
				bRet = entry.Create();
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error adding configuration entry.",ex); }
            return bRet;
		}
		public int Count { get { return this.mConfigEntries.ConfigTable.Rows.Count; } }
		public DBConfigEntry Item() {
			//Return a new blank configuration entry object
			DBConfigEntry entry=null;
			try { 
				entry = new DBConfigEntry(this.mMediator);
				entry.Application = this.mProductName;
				entry.Changed +=new EventHandler(OnEntryChanged);
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error getting configuration entry.",ex); }
            return entry;
		}
		public DBConfigEntry Item(string pcName, string key) {
			//Return an existing entry object from the database entries collection
			DBConfigEntry entry=null;
			try { 
				//Merge from collection (dataset)
				if(pcName != "" && key != "") {
					AppConfigDS.ConfigTableRow row = (AppConfigDS.ConfigTableRow)this.mConfigEntries.ConfigTable.Select("Application='" + this.mProductName + "'" + " AND PCName='" + pcName + "'" + " AND Key='" + key + "'")[0];
					entry = new DBConfigEntry(row, this.mMediator);
					entry.Changed +=new EventHandler(OnEntryChanged);
				}
				else 
					entry = Item();
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error getting configuration entry.",ex); }
            return entry;
		}
		public bool Remove(DBConfigEntry entry) {
			//Remove the specified configuration entry
			bool bRet=false;
			try { 
				bRet = entry.Delete();
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error removing configuration entry.",ex); }
            return bRet;
		}
		
		private void OnEntryChanged(object sender, EventArgs e) {
			//Event handler for change in a chile entry
			try { Refresh(); } catch(Exception) { }
		}
		private void OnConfigurationChanged(object sender, EventArgs e) {
			//Event handler for configuration file rollbacks
			try { Refresh(); } catch(Exception) { }
		}
	}

    public class DBConfigEntry {
        //Members
        private string _application="";
        private string _pcname="";
        private string _key="";
        private string _value="";
        private string _security="";
        private Mediator mMediator=null;

        public const string USP_CONFIGENTRY_CREATE = "uspToolsConfigurationCreate";
        public const string USP_CONFIGENTRY_UPDATE = "uspToolsConfigurationUpdate";
        public const string USP_CONFIGENTRY_DELETE = "uspToolsConfigurationDelete";

        public event EventHandler Changed=null;

        //Interface
        public DBConfigEntry(Mediator mediator) : this(null,mediator) { }
        public DBConfigEntry(AppConfigDS.ConfigTableRow entry,Mediator mediator) {
            //Constructor
            try {
                this.mMediator = mediator;
                if(entry != null) {
                    if(!entry.IsApplicationNull()) this._application = entry.Application;
                    if(!entry.IsPCNameNull()) this._pcname = entry.PCName;
                    if(!entry.IsKeyNull()) this._key = entry.Key;
                    if(!entry.IsValueNull()) this._value = entry.Value;
                    if(!entry.IsSecurityNull()) this._security = entry.Security;
                }
            }
            catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...], ToDataSet()
        public string Application { get { return this._application; } set { this._application = value; } }
        public string PCName { get { return this._pcname; } set { this._pcname = value; } }
        public string Key { get { return this._key; } set { this._key = value; } }
        public string Value { get { return this._value; } set { this._value = value; } }
        public string Security { get { return this._security; } set { this._security = value; } }
        #endregion
        public bool Create() {
            //Save this configuration entry
            bool bRet=false;
            try {
                bRet = this.mMediator.ExecuteNonQuery(USP_CONFIGENTRY_CREATE,new object[] { this._application,this._pcname,this._key,this._value,this._security });
                if(this.Changed != null) this.Changed(this,new EventArgs());
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error creating configuration entry.",ex); }
            return bRet;
        }
        public bool Update() {
            //Update this configuration entry
            bool bRet=false;
            try {
                bRet = this.mMediator.ExecuteNonQuery(USP_CONFIGENTRY_UPDATE,new object[] { this._application,this._pcname,this._key,this._value,this._security });
                if(this.Changed != null) this.Changed(this,new EventArgs());
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error updating configuration entry.",ex); }
            return bRet;
        }
        public bool Delete() {
            //Delete this configuration entry
            bool bRet=false;
            try {
                bRet = this.mMediator.ExecuteNonQuery(USP_CONFIGENTRY_DELETE,new object[] { this._application,this._pcname,this._key });
                if(this.Changed != null) this.Changed(this,new EventArgs());
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error deleting configuration entry.",ex); }
            return bRet;
        }
    }
}
