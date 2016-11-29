using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Argix.Data;

namespace Argix.Configuration {
	/// <summary>Abstract base class for an application configuration factory.</summary>
	public abstract class AppConfigFactory {
		//Members
		private string mProductName="";
		private AppConfigDS mConfigDS=null;
		private Hashtable mUsers=null;

        /// <summary>Stored procedure for configuration entries.</summary>
        public const string USP_CONFIG_GETLIST = "uspToolsConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        /// <summary>Stored procedure to create a configuration entry.</summary>
        public const string USP_CONFIG_CREATE = "uspToolsConfigurationCreate";
        /// <summary>Stored procedure to update a configuration entry.</summary>
        public const string USP_CONFIG_UPDATE = "uspToolsConfigurationUpdate";
        /// <summary>Name of the user for the default configuration entries.</summary>
        public const string USER_DEFAULT = "Default";
        internal const string USER_NEW = "<New User>";

        internal event ConfigEventHandler ConfigsChanged = null;
        internal event EventHandler UsersChanged = null;
		
		//Interface
        /// <summary>Constructor.</summary>
        /// <param name="productName">Application fieldname in the database Configuration table.</param>
		public AppConfigFactory(string productName) { 
			try {
				this.mProductName = productName;
				this.mConfigDS = new AppConfigDS();
				this.mUsers = new Hashtable();
				Refresh();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new AppConfigFactory instance.", ex); }
		}
        /// <summary>Refresh configuration entry and user lists.</summary>
		public void Refresh() { 
			//Refresh the configuration and users
			refresh();
			refreshUsers();
		}
        /// <summary>Add a new user to the Configuration database.</summary>
        /// <param name="name">>PCName fieldname in the database Configuration table.</param>
        /// <returns>Argix.Configuration.AppConfig object for the new user.</returns>
		public AppConfig Add(string name) {
			//Add a new user to the configuration
			//Give all defaults and set ReadOnly=false
			DataSet ds = new DataSet();
			DataRow[] rows = this.mConfigDS.ConfigTable.Select("PCName = '" + USER_DEFAULT + "'");
            if(rows.Length > 0) ds.Merge(rows);
			for(int i=0; i<rows.Length; i++) {
				if(rows[i]["PCName"].ToString() == AppConfig.KEY_READONLY) rows[i]["PCName"] = name;
			}
			ConfigMediator.ExecuteNonQuery(USP_CONFIG_CREATE, new object[]{this.mProductName, name, AppConfig.KEY_READONLY, "false", "1"});
			AppConfig ac = NewAppConfig(name, ds);
			ac.Changed += new EventHandler(OnAppConfigChanged);
			this.ConfigsChanged += new ConfigEventHandler(ac.OnConfigsChanged);
			Refresh();
			return ac;
		}
		/// <summary>Count of configuration entries.</summary>
        public int Count { get { return this.mConfigDS.ConfigTable.Rows.Count; } }
        /// <summary>Get a configuration for the speciified user.</summary>
        /// <param name="name">PCName fieldname in the database Configuration table.</param>
        /// <returns>Argix.Configuration.AppConfig object for the specified user.</returns>
		public AppConfig Item(string name) {
			//Get a configuration for the specified name
            AppConfigDS ds = new AppConfigDS();
			DataRow[] rows = this.mConfigDS.ConfigTable.Select("PCName = '" + USER_DEFAULT + "'");
            if(rows.Length > 0) ds.Merge(rows);
            if(name != USER_DEFAULT) {
                rows = this.mConfigDS.ConfigTable.Select("PCName = '" + name + "'");
                if(rows.Length > 0) ds.Merge(rows);
            }
			AppConfig ac = NewAppConfig(name, ds);
			ac.Changed += new EventHandler(OnAppConfigChanged);
			this.ConfigsChanged += new ConfigEventHandler(ac.OnConfigsChanged);
			return ac;
		}
        /// <summary>Get a configuration for the speciified users.</summary>
        /// <param name="names">PCName fieldname in the database Configuration table.</param>
        /// <returns>Argix.Configuration.AppConfig object for the specified user.</returns>
		public AppConfig Item(string[] names) { 
			//Get a configuration for all specified names
            AppConfigDS ds = new AppConfigDS();
            DataRow[] rows = this.mConfigDS.ConfigTable.Select("PCName = '" + USER_DEFAULT + "'");
            if(rows.Length > 0) ds.Merge(rows);
            foreach(string name in names) {
                if(name != USER_DEFAULT) {
                    rows = this.mConfigDS.ConfigTable.Select("PCName = '" + name + "'");
                    if(rows.Length > 0) ds.Merge(rows);
                }
			}
            AppConfig ac = NewAppConfig("",ds);
			ac.Changed += new EventHandler(OnAppConfigChanged);
			this.ConfigsChanged += new ConfigEventHandler(ac.OnConfigsChanged);
			return ac;
		}
        /// <summary>A hashtable of all users in the database Configuration table for the specified application.</summary>
		public Hashtable Users { get { return this.mUsers; } }
		/// <summary>Shows the configuration dialog.</summary>
		/// <param name="password">Password that user must specify to access the dialog.</param>
        public void ShowDialog(string password) {
			dlgLogin login = new dlgLogin(password);
			login.ValidateEntry();
			if(login.IsValid) 
				new dlgConfig(this).ShowDialog();
		}
        /// <summary>An Argix.Data.Mediator for access to the configuration database.</summary>
		protected abstract Mediator ConfigMediator { get; }
        /// <summary>Create a new instance of the Argix.Configuration.AppConfig sub-class.</summary>
        /// <param name="PCName">PCName fieldname in the database Configuration table.</param>
        /// <param name="ds">Configuration data.</param>
        /// <returns>Argix.Configuration.AppConfig object for the specified user.</returns>
		protected abstract AppConfig NewAppConfig(string PCName, DataSet ds);
		private void refresh() { 
			//Refresh the configuration data
			try {
				//Cache the configuration for the specified product, and build a list of current users
				this.mConfigDS.Clear();
				DataSet ds = ConfigMediator.FillDataset(USP_CONFIG_GETLIST, TBL_CONFIGURATION, new object[]{this.mProductName});
				if(ds!=null) {
					//Cache application configuration
					this.mConfigDS.Merge(ds);
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing the application configuration.", ex); }
			finally { if(this.ConfigsChanged != null) this.ConfigsChanged(this, new ConfigEventArgs(this.mConfigDS)); }
		}
		private void refreshUsers() { 
			//Refresh current user list
			try {
				//Cache the list of current users
				this.mUsers.Clear();
				this.mUsers.Add(USER_NEW, USER_NEW);
				for(int i=0; i<this.mConfigDS.ConfigTable.Rows.Count; i++) {
					string user = this.mConfigDS.ConfigTable.Rows[i]["PCName"].ToString();
					if(!this.mUsers.ContainsKey(user)) this.mUsers.Add(user, user);
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing the application configuration.", ex); }
			finally { if(this.UsersChanged != null) this.UsersChanged(this, EventArgs.Empty); }			
		}
		private void OnAppConfigChanged(object sender, EventArgs e) {
			//Event handler for change in a config object
			refresh();
		}
	}

    /// <summary>Abstract base class for an application configuration.</summary>
	public abstract class AppConfig {
		//Members
		private string mPCName="";
        /// <summary>Configuration data cache.</summary>
        protected AppConfigDS mConfigDS = null;
        /// <summary>An Argix.Data.Mediator instance for access to the configuration database.</summary>
        protected Mediator mMediator = null;
        /// <summary>Name of the MIS Password configuration key.</summary>
		public const string KEY_MISPASSWORD = "MISPassword";
        /// <summary>Name of the Read Only configuration key.</summary>
        public const string KEY_READONLY = "ReadOnly";
        /// <summary>Name of the Trace Level configuration key.</summary>
        public const string KEY_TRACELEVEL = "TraceLevel";

        /// <summary></summary>
        public event EventHandler Changed = null;
		
		//Interface
        /// <summary>Constructor</summary>
        /// <param name="PCName">A configuration user- can be username or machine name.</param>
        /// <param name="ds">Configuration data associated with PCName.</param>
        /// <param name="mediator">An Argix.Data.Mediator instance for access to the configuration database.</param>
        public AppConfig(string PCName,DataSet ds,Mediator mediator) { 
			try {
				this.mPCName = PCName;
				this.mConfigDS = new AppConfigDS();
				if(ds!=null)
					this.mConfigDS.Merge(ds);
				this.mMediator = mediator;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new AppConfig instance.", ex); }
		}
        /// <summary>Get a configuration value by key and return as a string.</summary>
        public string GetValue(string key) {
			string sValue="";
			try {
				//Get a value: can be more than one value for the same key if mConfigDS
				//was populated by multiple calls to the stored proc (i.e. by username & by machine name)
				DataRow[] rows = this.mConfigDS.ConfigTable.Select("Key = '" + key + "'");
				if(rows.Length > 0) {
					//At least one value exists; check for multiple values
					if(rows.Length > 1) {
						//Look for non-default value
						DataRow[] _rows = this.mConfigDS.ConfigTable.Select("Key = '" + key + "' AND PCName <> 'Default'");
						if(_rows.Length > 0) {
							//Non-default values exist- take first one
							sValue = _rows[0]["Value"].ToString();
						}
						else {
							//Only 'default' values exist- take first one (they all have the same value)
							sValue = rows[0]["Value"].ToString();
						}
					}
					else
						sValue = rows[0]["Value"].ToString();
				}
				else
					throw new ApplicationException("Configuration value not found for key " + key + ".");
			}
			catch(IndexOutOfRangeException ex) { throw new ApplicationException("Configuration key " + key + " not found.", ex); }
			catch(Exception ex) { throw ex; }
			return sValue;
		}
        /// <summary>Get a configuration value by key and return as an integer.</summary>
        public int GetValueAsInteger(string key) {
			int iValue=0;
			try {
				iValue = Convert.ToInt32(GetValue(key));
			}
			catch(FormatException ex) { throw new ApplicationException("Configuration key " + key + " does not have a value.", ex); }
			catch(Exception ex) { throw ex; }
			return iValue;
		}
        /// <summary>Get a configuration value by key and return as a TimeSpan.</summary>
        public TimeSpan GetValueAsTimeSpan(string key) {
			TimeSpan tsValue;
			try {
				tsValue = TimeSpan.FromDays(GetValueAsInteger(key));
			}
			catch(Exception ex) { throw ex; }
			return tsValue;
		}
        /// <summary>Get a configuration value by key and return as a boolean.</summary>
        public bool GetValueAsBoolean(string key) {
			bool bValue=false;
			try {
				bValue = Convert.ToBoolean(GetValue(key));
			}
			catch(FormatException ex) { throw new ApplicationException("Configuration key " + key + " has an invalid value.", ex); }
			catch(Exception ex) { throw ex; }
			return bValue;
		}
        /// <summary>Set a configuration value by key.</summary>
        public void SetValue(string key,string _value) {
			//
			try {
				//Validate config object identity 
				//NOTE: PCName="" for config object w/multiple names (i.e. using AppConfigFactory::Item(string[])
				if(this.mPCName.Trim().Length == 0) return;

				//Update key/value pair for this object
				DataRow[] rows = this.mConfigDS.ConfigTable.Select("PCName='" + this.mPCName + "' AND Key = '" + key + "'");
				if(rows.Length > 0) {
					//Update existing key for PCName
					if(rows.Length > 0) rows[0]["Value"] = _value;
					this.mMediator.ExecuteNonQuery(AppConfigFactory.USP_CONFIG_UPDATE, new object[]{this.mConfigDS.ConfigTable[0].Application, this.mPCName, key, _value, "1"});
				}
				else {
					//Check if this is a 'Default' key/value
					DataRow[] _rows = this.mConfigDS.ConfigTable.Select("PCName='Default' AND Key = '" + key + "'");
					if(_rows.Length > 0) {
						//Create new key/value for PCName
						this.mMediator.ExecuteNonQuery(AppConfigFactory.USP_CONFIG_CREATE, new object[]{this.mConfigDS.ConfigTable[0].Application, this.mPCName, key, _value, "1"});
					}
					else
						throw new ApplicationException("Configuration value not found for key " + key + ".");
				}
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while setting value for key " + key + ".", ex); }
			finally { if(this.Changed != null) this.Changed(this, EventArgs.Empty); }
		}

        /// <summary>Configuration changed event.</summary>
        public void OnConfigsChanged(object sender,ConfigEventArgs e) {
			//Event handler for change in configuration data (cached in parent)
			Hashtable names = new Hashtable();
			if(this.mPCName.Trim().Length > 0) {
				if(this.mPCName != AppConfigFactory.USER_DEFAULT) names.Add(AppConfigFactory.USER_DEFAULT, AppConfigFactory.USER_DEFAULT);
				names.Add(this.mPCName, this.mPCName);
			}
			else {
				//Determine list of names from current data
				for(int i=0; i<this.mConfigDS.ConfigTable.Rows.Count; i++) {
					string name = this.mConfigDS.ConfigTable.Rows[i]["PCName"].ToString();
					if(!names.ContainsKey(name)) names.Add(name, name);
				}
			}
			this.mConfigDS.Clear();
			foreach(DictionaryEntry name in names) {
				DataRow[] rows = e.Data.Tables["ConfigTable"].Select("PCName = '" + name.Value + "'");
				if(rows.Length > 0) this.mConfigDS.Merge(rows);
			}
		}

        /// <summary>Password to MIS-related services.</summary>
        [Category("Accessibility"),Description("Password to MIS-related services.")]
		public string MISPassword { get { return GetValue(KEY_MISPASSWORD); } }
        /// <summary>Read only rights.</summary>
        [Category("Accessibility"),Description("Read only rights.")]
		public bool ReadOnly { get { return GetValueAsBoolean(KEY_READONLY); } set { SetValue(KEY_READONLY, value.ToString()); } }
        /// <summary>Application trace logging level.</summary>
        [Category("Behavior"),Description("Application trace logging level.")]
		public int TraceLevel { get { return GetValueAsInteger(KEY_TRACELEVEL); } }
	}

    /// <summary>Configuration event handler.</summary>
    /// <param name="source">Configuration source.</param>
    /// <param name="e">Configuration data for the event.</param>
    public delegate void ConfigEventHandler(object source,ConfigEventArgs e);

    /// <summary>Configuration event args for ConfigEventHandler.</summary>
    public class ConfigEventArgs :EventArgs {
		private DataSet _ds=null;
        /// <summary>Constructor</summary>
        public ConfigEventArgs(DataSet ds) { this._ds = ds; }
        /// <summary>Gets and sets configuration data.</summary>
        public DataSet Data { get { return this._ds; } set { this._ds = value; } }
	}
}
