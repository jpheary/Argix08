//	File:	globals.cs
//	Author:	J. Heary
//	Date:	06/09/09
//	Desc:	Global enumerators, event support, exceptions, etc.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;

namespace Argix {
    //
    public delegate void ControlErrorEventHandler(object source,ControlErrorEventArgs e);
    public class ControlErrorEventArgs:EventArgs {
        private Exception _ex = null;
        public ControlErrorEventArgs(Exception ex) { this._ex = ex; }
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }

    public class ControlException:ApplicationException {
        public ControlException() : this("Unspecified control exception.") { }
        public ControlException(string message) : base(message) { }
        public ControlException(string message,Exception innerException) : base(message,innerException) { }
    }

    public delegate void StatusEventHandler(object source,StatusEventArgs e);
    public class StatusEventArgs:EventArgs {
        private string _message = "";
        public StatusEventArgs(string message) { this._message = message; }
        public string Message { get { return this._message; } set { this._message = value; } }
    }

    namespace CustomerSvc {
        public delegate void OpenIssueWindowEventHandler(object source,OpenIssueWindowEventArgs e);
        public class OpenIssueWindowEventArgs:EventArgs {
            private Issue _issue = null;
            private bool _handled = false;
            private string _searchText="";
            public OpenIssueWindowEventArgs(Issue issue,string searchText,ref bool handled) { this._issue = issue; this._searchText = searchText; this._handled = handled; }
            public Issue Issue { get { return this._issue; } set { this._issue = value; } }
            public bool Handled { get { return this._handled; } set { this._handled = value; } }
            public string SearchText { get { return this._searchText; } set { this._searchText = value; } }
        }
        public delegate void NewIssueEventHandler(object source,NewIssueEventArgs e);
        public class NewIssueEventArgs:EventArgs {
            private Issue _issue = null;
            public NewIssueEventArgs(Issue issue) { this._issue = issue; }
            public Issue Issue { get { return this._issue; } set { this._issue = value; } }
        }
    }
    
    namespace Enterprise {
        public delegate void CompanyEventHandler(object source,CompanyEventArgs e);
        public class CompanyEventArgs :EventArgs {
            private int _companyid = 0;
            private string _company = "";
            public CompanyEventArgs(int companyid,string company) { this._companyid = companyid; this._company = company; }
            public int CompanyID { get { return this._companyid; } set { this._companyid = value; } }
            public string Company { get { return this._company; } set { this._company = value; } }
        }

        public delegate void LocationScopeEventHandler(object source,LocationScopeEventArgs e);
        public class LocationScopeEventArgs :EventArgs {
            private string _scope = "";
            public LocationScopeEventArgs(string locationScope) { this._scope = locationScope; }
            public string LocationScope { get { return this._scope; } set { this._scope = value; } }
        }

        public delegate void LocationEventHandler(object source,LocationEventArgs e);
        public class LocationEventArgs :EventArgs {
            private string _scope = "";
            private string _location = "";
            public LocationEventArgs(string locationScope,string location) { this._scope = locationScope; this._location = location; }
            public string LocationScope { get { return this._scope; } set { this._scope = value; } }
            public string CompanyLocation { get { return this._location; } set { this._location = value; } }
        }

        public delegate void ContactEventHandler(object source,ContactEventArgs e);
        public class ContactEventArgs :EventArgs {
            private Contact _contact = null;
            public ContactEventArgs(Contact contact) { this._contact = contact; }
            public Contact Contact { get { return this._contact; } set { this._contact = value; } }
        }
    }
}