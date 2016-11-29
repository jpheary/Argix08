using System;
using System.Configuration;
using System.Reflection;

namespace Tsort.Configuration {
	//
	internal class DllConfig : IDisposable { 
		//Members
		private string mAppConfigFile="";
		private const string NEW_CONFIG_FILE = "Tsort.PandA.dll.config";
		public static string AppSettings(string key) {
			string val="";
			using (new DllConfig()) {
				val = ConfigurationSettings.AppSettings[key];
			}
			return val;
		}
		internal DllConfig() {
			//Constructor
			this.mAppConfigFile = AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString();
			Switch(NEW_CONFIG_FILE);
		}
		protected void Switch(string config) {   
			//
			AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", config);
			FieldInfo fiInit = typeof(System.Configuration.ConfigurationSettings).GetField("_configurationInitialized", BindingFlags.NonPublic|BindingFlags.Static);
			FieldInfo fiSystem = typeof(System.Configuration.ConfigurationSettings).GetField("_configSystem",BindingFlags.NonPublic|BindingFlags.Static);
			if(fiInit != null && fiSystem != null) {
				fiInit.SetValue(null, false);
				fiSystem.SetValue(null, null);
			}
		}
		public void Dispose() { Switch(this.mAppConfigFile); }
	}
}