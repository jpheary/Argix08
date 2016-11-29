//	File:	globals.cs
//	Author:	J. Heary
//	Date:	02/27/07
//	Desc:	Global (library-wide) definitions.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix;
using Argix.Configuration;
using Argix.Data;

namespace Tsort {
	//Global application object
	internal class AppLib {
        //Members
        private static Mediator _Mediator = null;
        private static ConfigFactory _ConfigFactory = null;
        private static Config _Config = null;

        internal const string EVENTLOGNAME="Tsort08";
				
		//Interface
		static AppLib() {
            //Class constructor: get application configuration
            Mediator.SqlCommandTimeout = 5;
            UseWebSvc = Config._UseWebSvc;
            _ConfigFactory = new ConfigFactory(AppLib.Product);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
        }
		private AppLib() { }
        internal static Mediator Mediator { get { return _Mediator; } }
        internal static bool UseWebSvc {
            get { return Config._UseWebSvc; }
            set {
                Config._UseWebSvc = value;
                if(Config._UseWebSvc)
                    _Mediator = (Mediator)new WebSvcMediator(Config._WebSvcUrl);
                //else if(Config._UseConnState)
                //    _Mediator = (Mediator)new SortMediator(Config._ConnectionString);
                else
                    _Mediator = (Mediator)new SQLMediator(Config._ConnectionString);
            }
        }
        internal static Config Config { get { return _Config; } }
        internal static void ShowConfig() {
            _ConfigFactory.ShowDialog(_Config.MISPassword);
            _Config = (Config)_ConfigFactory.Item(new string[] { Environment.UserName,Environment.MachineName });
        }
        internal static string Product { get { return "Sort Library"; } }
	}

    internal class ConfigFactory:AppConfigFactory {
        //Members

        //Interface
        public ConfigFactory(string productName) : base(productName) { }
        protected override Mediator ConfigMediator { get { return AppLib.Mediator; } }
        protected override AppConfig NewAppConfig(string PCName,DataSet ds) { return new Config(PCName,ds,AppLib.Mediator); }
    }
    internal class Config:AppConfig {
        //Members        
        public static bool _UseWebSvc = false;
        public static bool _UseConnState = false;
        public static string _ConnectionString="";
        public static string _WebSvcUrl="";

        internal const string KEY_LANEPREFIX = "LanePrefix";
        internal const string KEY_ERRORLABELNUMBER = "OBLabelError";
        internal const string KEY_SANLABELID_DEFAULT = "IBLabelDefault";
        internal const string KEY_SORTEDITEMS_LENGTH = "SortedItemsLength";
        internal const string KEY_SORTEDITEMS_CHECKDUPLICATESLENGTH = "SortedItemsCheckDuplicatesLength";
        internal const string KEY_WEIGHT_MAX = "WeightMax";
        internal const string KEY_UPSALLOWED = "UPSAllowed";
        internal const string KEY_SHIPOVERRIDE = "ShipOverride";

        //Interface
        public Config(string PCName,DataSet ds,Mediator mediator) : base(PCName,ds,mediator) { }

        [Category("Data"),Description("Lane prefix.")]
        public string LanePrefix {
            get { return GetValue(KEY_LANEPREFIX); }
            set { SetValue(KEY_LANEPREFIX,value); }
        }
        [Category("Data"),Description("Label number for the default error label.")]
        public string OBLabelError {
            get { return GetValue(KEY_ERRORLABELNUMBER); }
            set { SetValue(KEY_ERRORLABELNUMBER,value); }
        }
        [Category("Data"),Description("Label number for the default inbound label.")]
        public int IBLabelDefault {
            get { return GetValueAsInteger(KEY_SANLABELID_DEFAULT); }
            set { SetValue(KEY_SANLABELID_DEFAULT,value.ToString()); }
        }
        [Category("Data"),Description("Length of sorted item string.")]
        public int SortedItemsLength {
            get { return GetValueAsInteger(KEY_SORTEDITEMS_LENGTH); }
            set { SetValue(KEY_SORTEDITEMS_LENGTH,value.ToString()); }
        }
        [Category("Data"),Description("")]
        public int SortedItemsCheckDuplicatesLength {
            get { return GetValueAsInteger(KEY_SORTEDITEMS_CHECKDUPLICATESLENGTH); }
            set { SetValue(KEY_SORTEDITEMS_CHECKDUPLICATESLENGTH,value.ToString()); }
        }
        [Category("Data"),Description("Maximum allowed carton weight.")]
        public int WeightMax {
            get { return GetValueAsInteger(KEY_WEIGHT_MAX); }
            set { SetValue(KEY_WEIGHT_MAX,value.ToString()); }
        }
        [Category("Data"),Description("Flag to allow UPS agent delivery.")]
        public bool UPSAllowed {
            get { return GetValueAsBoolean(KEY_UPSALLOWED); }
            set { SetValue(KEY_UPSALLOWED,value.ToString()); }
        }
        [Category("Data"),Description("Shipment override.")]
        public string ShipOverride {
            get { return GetValue(KEY_SHIPOVERRIDE); }
            set { SetValue(KEY_SHIPOVERRIDE,value); }
        }
    }
}
