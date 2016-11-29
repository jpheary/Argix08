//	File:		contact.cs
//	Author:	    jheary
//	Date:		01/08/09
//	Desc:		Issue Management contact class.
//	Rev:		
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace Argix.Enterprise {
	//Bindable list of contacts (can be used instead of ContactDS)
    public class Contacts:BindingList<Contact> { }

    //CRG contact
	public class Contact {
		//Members
		private int _id=0;
        private string _firstname = "", _lastname = "", _fullname = "";
        private string _phone = "", _mobile = "";
        private string _fax = "", _email = "";
				
		//Interface
		public Contact(): this(null) { }
        public Contact(ContactDS.IssueContactTableRow contact) { 
			//Constructor
			try {
                if(contact != null) {
                    if(!contact.IsIDNull()) this._id = contact.ID;
                    if(!contact.IsFirstNameNull()) this._firstname = contact.FirstName;
                    if(!contact.IsLastNameNull()) this._lastname = contact.LastName;
                    if(!contact.IsFullNameNull()) 
                        this._fullname = contact.FullName;
                    else
                        this._fullname = this._firstname + " " + this._lastname;
                    if(!contact.IsPhoneNull()) this._phone = contact.Phone;
                    if(!contact.IsMobileNull()) this._mobile = contact.Mobile;
                    if(!contact.IsFaxNull()) this._fax = contact.Fax;
                    if(!contact.IsEmailNull()) this._email = contact.Email;
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Contact instance.", ex); }
		}
		#region Accessors\Modifiers: [Members...], ToDataSet()
        public int ID { get { return this._id; } }
        public string FirstName { get { return this._firstname; } set { this._firstname = value; this._fullname = this._firstname + " " + this._lastname; } }
        public string LastName { get { return this._lastname; } set { this._lastname = value; this._fullname = this._firstname + " " + this._lastname; } }
        public string FullName { get { return (this._fullname.Trim().Length>0?this._fullname:this._firstname+" "+this._lastname); } set { this._fullname = value; } }
        public string Phone { get { return this._phone; } set { this._phone = value; } }
        public string Mobile { get { return this._mobile; } set { this._mobile = value; } }
        public string Fax { get { return this._fax; } set { this._fax = value; } }
        public string Email { get { return this._email; } set { this._email = value; } }
		public DataSet ToDataSet() { 
			//Return a dataset containing values for this object
            ContactDS ds = null;
			try {
                ds = new ContactDS();
                ContactDS.IssueContactTableRow contact = ds.IssueContactTable.NewIssueContactTableRow();
                contact.ID = this._id;
                contact.FirstName = this._firstname;
                contact.LastName = this._lastname;
                contact.FullName = this._fullname;
                contact.Phone = this._phone;
                contact.Mobile = this._mobile;
                contact.Fax = this._fax;
                contact.Email = this._email;
                ds.IssueContactTable.AddIssueContactTableRow(contact);
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
	}
}
