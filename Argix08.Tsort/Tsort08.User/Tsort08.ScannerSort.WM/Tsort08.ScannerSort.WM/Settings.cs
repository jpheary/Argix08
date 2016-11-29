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
        private string _webServiceURL = "http://rgxvmweb/SortServices/ScannerService.asmx";
        private bool _UseSpecificCredentials=false;
        private string _username="jheary";
        private string _password="p@ssword";
        private string _domain="Argix";

        public static Settings Default  { get  { return _defaultInstance; }  }
        public string WebServiceURL {  get { return _webServiceURL;  } }
        public bool UseSpecificCredentials { get { return _UseSpecificCredentials; } }
        public string Username { get { return _username; } }
        public string Password { get { return _password; } }
        public string Domain { get { return _domain; } }
   }
}
