using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Reflection;

namespace Argix.Finance {
    //
    public class RateWareFactory {
        //Members
        private static CZARCOMLib.czardllClass _CzarLib;
        private static string[] WEIGHT_RANGES = { "1","501","1001","2001","5001","10001","20001" };

        private const string CLSCODE_XMLFILE = "ClassCodes.xml";
        private const int WEIGHT_RANGE_COUNT = 7;

        //Interface
        static RateWareFactory() { 
            //Constructor
            _CzarLib = new CZARCOMLib.czardllClass();
            _CzarLib.InitializeCzar();
        }
        private RateWareFactory() { }
        ~RateWareFactory() {
            //Destructor
            _CzarLib.EndCzar();
        }

        public static string[] GetTariffs() {
            //Get available tariffs
            string[] tariffs = new string[_CzarLib.GetEffectiveTableSize];
            try {
                if(_CzarLib.GetEffectiveTableSize > 0) {
                    for(int i=0;i<_CzarLib.GetEffectiveTableSize;i++) {
                        tariffs[i] = _CzarLib.get_GetEffectiveTableRecord(i);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to get object.",ex); }
            return tariffs;
        }
        public static ClassCodeDS GetClassCodes() {
            //Get class codes
            ClassCodeDS codes=null;
            try {
                codes = new ClassCodeDS();
                FileInfo fi = new FileInfo(CLSCODE_XMLFILE);
                if(fi.Exists) codes.ReadXml(CLSCODE_XMLFILE);
            }
            catch(Exception ex) { throw new ApplicationException("Error while reading class codes.",ex); }
            return codes;
        }
        public static RateDS CalculateRates(string tariff,string originZip,string classCode,double discount,int floorMin,string[] destinationZips) {
            //Calcualate rates
            RateDS rates=null;

            rates = new RateDS();
            _CzarLib.Clear();
            _CzarLib.tariff_name = tariff.Substring(0,8);
            _CzarLib.shipment_date = tariff.Substring(12,2) + "/" + tariff.Substring(14,2) + "/" + tariff.Substring(8,4);
            _CzarLib.orgzip = originZip;
            _CzarLib.set_cls(0,classCode);
            _CzarLib.set_wbdisc_in(0,discount); // min charge
            _CzarLib.set_wbdisc_in(1,discount); // < 500
            _CzarLib.set_wbdisc_in(2,discount); // > 500
            _CzarLib.set_wbdisc_in(3,discount); // > 1000
            _CzarLib.set_wbdisc_in(4,discount); // > 2000  
            _CzarLib.set_wbdisc_in(5,discount); // > 5000 
            _CzarLib.use_dscnts = "Y";
            _CzarLib.discount_type = "R";
            _CzarLib.single_shipment = "N";

            bool isSuccess = false;
            ArrayList rateArray = new ArrayList(WEIGHT_RANGE_COUNT);
            for(int i=0;i < destinationZips.Length;i++) {
                rateArray.Clear();
                for(int j=0;j < WEIGHT_RANGE_COUNT;j++) {
                    //Rate the shipment now - we are rating one shipment at a time - multiple shipments give same rates
                    int weight = Convert.ToInt32(WEIGHT_RANGES[j].ToString());
                    _CzarLib.dstzip = destinationZips[i].ToString();
                    _CzarLib.set_wgt(0,Convert.ToInt32(weight.ToString()));
                    _CzarLib.RateShipment();
                    if(_CzarLib.error_status != 0)
                        throw new ApplicationException("Error occured when rating using destination zip " + destinationZips[i].ToString() + ". (" + _CzarLib.get_GetErrorString(_CzarLib.error_status) + ")");
                    else
                        isSuccess = true;
                    if(isSuccess)
                        rateArray.Add(_CzarLib.get_rte(0).ToString("C")); //format double to 2-digit number
                    else
                        break;
                }
                //Quit the whole thing if we encounter error with fixed properties
                //Check for error code 217 - Destination Tariff not found
                //continue with the next if it's Destination related error
                if(!isSuccess && _CzarLib.error_status != 217)
                    break;
                double minCharge =  Convert.ToDouble(_CzarLib.min_charge.ToString("F"));
                if(minCharge < floorMin) minCharge = floorMin; //take lesser of min charge from rate ware or set floor min

                //Add Rates to the grid
                RateDS.RateTableRow row = rates.RateTable.NewRateTableRow();
                row.OrgZip = _CzarLib.orgzip;
                row.DestZip = _CzarLib.dstzip.Substring(0,3);
                row.MinCharge = minCharge.ToString("C");
                if(rateArray.Count > 0) {
                    row.Rate1 = rateArray[0].ToString();
                    row.Rate501 =  rateArray[1].ToString();
                    row.Rate1001 =  rateArray[2].ToString();
                    row.Rate2001 =  rateArray[3].ToString();
                    row.Rate5001 =  rateArray[4].ToString();
                    row.Rate10001 =  rateArray[5].ToString();
                    row.Rate20001 =  rateArray[6].ToString();
                }
                rates.RateTable.AddRateTableRow(row);
            }
            return rates;
        }
    }
}
