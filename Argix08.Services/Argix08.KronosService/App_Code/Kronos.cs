//	File:	Kronos.cs
//	Author:	J. Heary
//	Date:	03/18/10
//	Desc:	Provides workforce management services from Kronos databases.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Argix {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class Kronos:IKronos {
        //Members
        private const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "LocalTerminalTable";
        private const string USP_EMPLOYEEVIEW = "uspArgixIDViewerBadgedataGetList",TBL_EMPLOYEEVIEW = "EmployeeTable";

        //Interface
        public Kronos() { }
        public TerminalInfo GetTerminalInfo() {
            //Get information about the local terminal for this service
            TerminalInfo info = null;
            try {
                info = new TerminalInfo();
                info.Connection = DatabaseFactory.CreateDatabase("SQLConnection").ConnectionStringWithoutCredentials;
                DataSet ds = fillDataset(USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[] { });
                if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                    info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading terminal info.",ex); }
            return info;
        }
        public ArrayList GetIDTypes() {
            //Return a list of ID types
            ArrayList idTypes=null;
            try {
                idTypes = new ArrayList();
                Hashtable types = (Hashtable)ConfigurationManager.GetSection("idStores/idTypes");
                IDictionaryEnumerator oEnum = types.GetEnumerator();
                for(int i=0;i<types.Count;i++) {
                    oEnum.MoveNext();
                    DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
                    idTypes.Add(oEntry.Key.ToString());
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading IDTypes.",ex); }
            return idTypes;
        }
        public Employees GetEmployees(string idType) {
            //
            Employees employees=null;
            try {
                employees = new Employees();
                CompanyDS companyDS = new CompanyDS();
                DataSet ds = new DataSet();
                Database db = DatabaseFactory.CreateDatabase(idType);
                DbCommand cmd = db.GetStoredProcCommand(USP_EMPLOYEEVIEW,new object[]{});
                db.LoadDataSet(cmd,ds,TBL_EMPLOYEEVIEW);
                if(ds!=null) {
                    companyDS.Merge(ds,true,MissingSchemaAction.Ignore);
                    for(int i=0;i<companyDS.EmployeeTable.Rows.Count;i++) {
                        companyDS.EmployeeTable[i].HasPhoto = (!companyDS.EmployeeTable[i].IsPhotoNull());
                        companyDS.EmployeeTable[i].HasSignature = (!companyDS.EmployeeTable[i].IsSignatureNull());
                        Employee employee = new Employee(companyDS.EmployeeTable[i]);
                        employees.Add(employee);
                    }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading employees.",ex); }
            return employees;
        }

        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        private bool executeNonQuery(string spName,object[] paramValues) {
            //
            bool ret=false;
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            int i = db.ExecuteNonQuery(spName,paramValues);
            ret = i > 0;
            return ret;
        }
        private object executeNonQueryWithReturn(string spName,object[] paramValues) {
            //
            object ret=null;
            if((paramValues != null) && (paramValues.Length > 0)) {
                Database db = DatabaseFactory.CreateDatabase("SQLConnection");
                DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
                ret = db.ExecuteNonQuery(cmd);

                //Find the output parameter and return its value
                foreach(DbParameter param in cmd.Parameters) {
                    if((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput)) {
                        ret = param.Value;
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}
