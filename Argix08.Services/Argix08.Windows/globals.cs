//	File:	exceptions.cs
//	Author:	J. Heary
//	Date:	12/01/04
//	Desc:	
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;

namespace Argix {
	public class ConfigurationException : Exception {
		//Interface
		public ConfigurationException() : base() { }
		public ConfigurationException(string message) : base(message) { }
		public ConfigurationException(string message, Exception innerException) : base(message, innerException) { }
	}

	namespace Windows {
		//Global application object template
		public abstract class AppBase {
			//Members
			public static Assembly Assy=Assembly.GetEntryAssembly();
			
			//Global exception constants
			public const string EX_UNEXPECTED = "Unexpected Error";
			
			//Interface
			static AppBase() { }
			#region Assembly Properties: 
			public static string Title { 
				get {
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
					AssemblyTitleAttribute att = (AssemblyTitleAttribute)o[0];
					return att.Title;
				}
			}
			public static string Description { 
				get {
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
					AssemblyDescriptionAttribute att = (AssemblyDescriptionAttribute)o[0];
					return att.Description;
				}
			}
			public static string Configuration { 
				get { 			
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
					AssemblyConfigurationAttribute att = (AssemblyConfigurationAttribute)o[0];
					return att.Configuration;
				}
			}
			public static string Company { 
				get { 
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
					AssemblyCompanyAttribute att = (AssemblyCompanyAttribute)o[0];
					return att.Company;
				}
			}
			public static string Product { 
				get { 
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
					AssemblyProductAttribute att = (AssemblyProductAttribute)o[0];
					return att.Product;
				}
			}
			public static string Copyright { 
				get { 
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
					AssemblyCopyrightAttribute att = (AssemblyCopyrightAttribute)o[0];
					return att.Copyright;
				}
			}
			public static string Trademark { 
				get { 
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyTrademarkAttribute), false);
					AssemblyTrademarkAttribute att = (AssemblyTrademarkAttribute)o[0];
					return att.Trademark;
				}
			}
			public static string Culture { 
				get { 
					object[] o = Assy.GetCustomAttributes(typeof(AssemblyCultureAttribute), false);
					AssemblyCultureAttribute att = (AssemblyCultureAttribute)o[0];
					return att.Culture;
				}
			}
			public static string Version { 
				get {
					Version ver = Assy.GetName().Version;
					return "Version " + ver.Major + "." + ver.Minor + "." + ver.Build + "." + ver.Revision;
				}
			}
			#endregion
		}
		
		public enum OnlineIcon { On=0, Off=1 }
		
		public delegate void OnlineStatusHandler(object source, OnlineStatusArgs e);
		public class OnlineStatusArgs : EventArgs {
			private bool _onLine=false;
			private string _url="";
			public OnlineStatusArgs(bool onLine, string url) {
				//Constructor
				this._onLine = onLine;
				this._url = url;
			}
			public bool OnLine { get { return this._onLine; } set { this._onLine = value; } }
			public string Url { get { return this._url; } set { this._url = value; } }
		}
	    
        namespace Printers {
		    public enum Barcode128 { A = 103, B = 104, C = 105 }
	    }
	}
}
