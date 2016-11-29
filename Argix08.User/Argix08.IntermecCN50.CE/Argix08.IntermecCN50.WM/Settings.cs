using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace Argix {
    //
    public class Settings {
        //Members
        private static Settings _defaultInstance = new Settings();
        private string _localConnectionString = "Data Source =" + (Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + @"\Tsort.sdf;");
        private string _webServiceURL = "http://tpjheary7/DriverScanService/DriverScanService.svc";

        public static Settings Default  { get  { return _defaultInstance; }  }
        public string LocalConnectionString { get { return _localConnectionString;  } }
        public string WebServiceURL {  get { return _webServiceURL;  } }
    }
}
