//	File:	IKronos.cs
//	Author:	J. Heary
//	Date:	03/18/10
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix {
    // 
    [ServiceContract]
    public interface IKronos {
        //
        [OperationContract]
        ArrayList GetIDTypes();
        
        [OperationContract]
        Employees GetEmployees(string idType);
    }

    [CollectionDataContract]
    public class Employees:BindingList<Employee> { }

    [DataContract]
    public class Employee {
        //Members
        private int _idnumber=0;
        private string _lastname="",_firstname="",_middle="",_suffix="";
        private string _organization="",_department="";
        private int _faccode=0;
        private string _location="",_sublocation="";
        private string _employeeid="";
        private int _badgenumber=0;
        private byte[] _photo=null,_signature=null;
        private string _status="";
        private DateTime _statusdate,_issuedate,_expirationdate,_dob,_hiredate;
        private bool _hasphoto=false,_hassignature=false;

        //Interface
        public Employee() : this(null) { }
        public Employee(CompanyDS.EmployeeTableRow employee) {
            //Constructor
            try {
                if(employee != null) {
                    this._idnumber = employee.IDNumber;
                    if(!employee.IsLastNameNull()) this._lastname = employee.LastName;
                    if(!employee.IsFirstNameNull()) this._firstname = employee.FirstName;
                    if(!employee.IsMiddleNull()) this._middle = employee.Middle;
                    if(!employee.IsSuffixNull()) this._suffix = employee.Suffix;
                    if(!employee.IsOrganizationNull()) this._organization = employee.Organization;
                    if(!employee.IsDepartmentNull()) this._department = employee.Department;
                    if(!employee.IsFaccodeNull()) this._faccode = employee.Faccode;
                    if(!employee.IsLocationNull()) this._location = employee.Location;
                    if(!employee.IsSubLocationNull()) this._sublocation = employee.SubLocation;
                    if(!employee.IsEmployeeIDNull()) this._employeeid = employee.EmployeeID;
                    if(!employee.IsBadgeNumberNull()) this._badgenumber = employee.BadgeNumber;
                    if(!employee.IsPhotoNull()) this._photo = employee.Photo;
                    if(!employee.IsSignatureNull()) this._signature = employee.Signature;
                    if(!employee.IsStatusNull()) this._status = employee.Status;
                    if(!employee.IsStatusDateNull()) this._statusdate = employee.StatusDate;
                    if(!employee.IsIssueDateNull()) this._issuedate = employee.IssueDate;
                    if(!employee.IsExpirationDateNull()) this._expirationdate = employee.ExpirationDate;
                    if(!employee.IsDOBNull()) this._dob = employee.DOB;
                    if(!employee.IsHireDateNull()) this._hiredate = employee.HireDate;
                    if(!employee.IsHasPhotoNull()) this._hasphoto = employee.HasPhoto;
                    if(!employee.IsHasSignatureNull()) this._hassignature = employee.HasSignature;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new Employee instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int IDNumber { get { return this._idnumber; } set { this._idnumber = value; } }
        [DataMember]
        public string LastName { get { return this._lastname; } set { this._lastname = value; } }
        [DataMember]
        public string FirstName { get { return this._firstname; } set { this._firstname = value; } }
        [DataMember]
        public string Middle { get { return this._middle; } set { this._middle = value; } }
        [DataMember]
        public string Suffix { get { return this._suffix; } set { this._suffix = value; } }
        [DataMember]
        public string Organization { get { return this._organization; } set { this._organization = value; } }
        [DataMember]
        public string Department { get { return this._department; } set { this._department = value; } }
        [DataMember]
        public int Faccode { get { return this._faccode; } set { this._faccode = value; } }
        [DataMember]
        public string Location { get { return this._location; } set { this._location = value; } }
        [DataMember]
        public string SubLocation { get { return this._sublocation; } set { this._sublocation = value; } }
        [DataMember]
        public string EmployeeID { get { return this._employeeid; } set { this._employeeid = value; } }
        [DataMember]
        public int BadgeNumber { get { return this._badgenumber; } set { this._badgenumber = value; } }
        [DataMember]
        public byte[] Photo { get { return this._photo; } set { this._photo = value; } }
        [DataMember]
        public byte[] Signature { get { return this._signature; } set { this._signature = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public DateTime StatusDate { get { return this._statusdate; } set { this._statusdate = value; } }
        [DataMember]
        public DateTime IssueDate { get { return this._issuedate; } set { this._issuedate = value; } }
        [DataMember]
        public DateTime ExpirationDate { get { return this._expirationdate; } set { this._expirationdate = value; } }
        [DataMember]
        public DateTime DOB { get { return this._dob; } set { this._dob = value; } }
        [DataMember]
        public DateTime HireDate { get { return this._hiredate; } set { this._hiredate = value; } }
        [DataMember]
        public bool HasPhoto { get { return this._hasphoto; } set { this._hasphoto = value; } }
        [DataMember]
        public bool HasSignature { get { return this._hassignature; } set { this._hassignature = value; } }
        #endregion
    }
}
