using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Finance {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class RateWareService:IRateWareService {
        //Members
        private const string SQL_CONNID = "RateWare";
        public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "TerminalTable";

        private string[] WEIGHT_RANGES = { "1","501","1001","2001","5001","10001","20001" };
        private const string CLSCODE_XMLFILE = "~\\App_Data\\ClassCodes.xml";
        private const int WEIGHT_RANGE_COUNT = 7;

        //Interface
        public RateWareService() { }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(SQL_CONNID).WriteLogEntry(m);
        }
        public TerminalInfo GetTerminalInfo() {
            //Get information about the local terminal for this service
            TerminalInfo info = null;
            try {
                info = new TerminalInfo();
                info.Connection = DatabaseFactory.CreateDatabase(SQL_CONNID).ConnectionStringWithoutCredentials;
                DataSet ds = fillDataset(USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[] { });
                if(ds!=null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                    info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch(Exception ex) { throw new FaultException<RateWareFault>(new RateWareFault(new ApplicationException(ex.Message,ex))); }
            return info;
        }

        public ClassCodes GetClassCodes() {
            //Get class codes
            ClassCodes codes=null;
            try {
                codes = new ClassCodes();
                FileInfo fi = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(CLSCODE_XMLFILE));
                if(fi.Exists) {
                    ClassCodeDS _codes = new ClassCodeDS();
                    _codes.ReadXml(System.Web.Hosting.HostingEnvironment.MapPath(CLSCODE_XMLFILE));
                    for(int i=0;i<_codes.ClassCodeTable.Rows.Count;i++) {
                        ClassCode code = new ClassCode(_codes.ClassCodeTable[i].Class,_codes.ClassCodeTable[i].Description);
                        codes.Add(code);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return codes;
        }
        public string[] GetTariffs() {
            //Get available tariffs
            string[] tariffs;
            CZARCOMLib.czardllClass czarLib=null;
            try {
                czarLib = new CZARCOMLib.czardllClass();
                czarLib.InitializeCzar();
                tariffs = new string[czarLib.GetEffectiveTableSize];
                if(czarLib.GetEffectiveTableSize > 0) {
                    for(int i=0;i<czarLib.GetEffectiveTableSize;i++) {
                        tariffs[i] = czarLib.get_GetEffectiveTableRecord(i);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<RateWareFault>(new RateWareFault(new ApplicationException(ex.Message,ex))); }
            finally {
                czarLib.EndCzar();
            } 
            return tariffs;
        }
        public Rates CalculateRates(string tariff,string originZip,string classCode,double discount,int floorMin,string[] destinationZips) {
            //Calcualate rates
            Rates rates = new Rates();
            CZARCOMLib.czardllClass czarLib=null;
            try {
                czarLib = new CZARCOMLib.czardllClass();
                czarLib.InitializeCzar();
                czarLib.Clear();
                czarLib.tariff_name = tariff.Substring(0,8);
                czarLib.shipment_date = tariff.Substring(12,2) + "/" + tariff.Substring(14,2) + "/" + tariff.Substring(8,4);
                czarLib.orgzip = originZip;
                czarLib.set_cls(0,classCode);
                czarLib.set_wbdisc_in(0,discount); // min charge
                czarLib.set_wbdisc_in(1,discount); // < 500
                czarLib.set_wbdisc_in(2,discount); // > 500
                czarLib.set_wbdisc_in(3,discount); // > 1000
                czarLib.set_wbdisc_in(4,discount); // > 2000  
                czarLib.set_wbdisc_in(5,discount); // > 5000 
                czarLib.use_dscnts = "Y";
                czarLib.discount_type = "R";
                czarLib.single_shipment = "N";

                //bool isSuccess = false;
                ArrayList rateArray = new ArrayList(WEIGHT_RANGE_COUNT);
                for(int i=0;i < destinationZips.Length;i++) {
                    rateArray.Clear();
                    //Rate the shipment now - we are rating one shipment at a time - multiple shipments give same rates
                    for (int j = 0; j < WEIGHT_RANGE_COUNT; j++) {
                        int weight = Convert.ToInt32(WEIGHT_RANGES[j].ToString());
                        czarLib.dstzip = destinationZips[i].ToString();
                        czarLib.set_wgt(0,Convert.ToInt32(weight.ToString()));
                        
                        try { czarLib.RateShipment(); } catch { }
                        if (czarLib.error_status == 0)
                            rateArray.Add(czarLib.get_rte(0).ToString("C")); //format double to 2-digit number
                        else
                            rateArray.Add("");

                        //Quit the whole thing if we encounter error with fixed properties
                        //Check for error code 217 - Destination Tariff not found continue with the next if it's Destination related error
                        //if(!isSuccess && czarLib.error_status != 217) break;
                        //if (!(czarLib.error_status == 0 || czarLib.error_status == 217)) break;
                    }

                    //Take lesser of min charge from rate ware or set floor min
                    double minCharge =  Convert.ToDouble(czarLib.min_charge.ToString("F"));
                    if(minCharge < floorMin) minCharge = floorMin; 

                    //Add rates to the grid
                    RateDS.RateTableRow _rate = new RateDS().RateTable.NewRateTableRow();
                    _rate.OrgZip = czarLib.orgzip;
                    _rate.DestZip = czarLib.dstzip.Substring(0,3);
                    _rate.MinCharge = minCharge.ToString("C");
                    _rate.Rate1 = rateArray[0].ToString();
                    _rate.Rate501 = rateArray[1].ToString();
                    _rate.Rate1001 = rateArray[2].ToString();
                    _rate.Rate2001 = rateArray[3].ToString();
                    _rate.Rate5001 = rateArray[4].ToString();
                    _rate.Rate10001 = rateArray[5].ToString();
                    _rate.Rate20001 = rateArray[6].ToString();
                    rates.Add(new Rate(_rate));
                }
            }
            catch(Exception ex) { throw new FaultException<RateWareFault>(new RateWareFault(new ApplicationException(ex.Message,ex))); }
            finally {
                czarLib.EndCzar();
            }
            return rates;
        }

        #region Data Services: fillDataset()
        private DataSet fillDataset(string spName,string table,object[] paramValues) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
            DbCommand cmd = db.GetStoredProcCommand(spName,paramValues);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
        #endregion
    }
}
