<%@ WebService Language="C#" Class="Carton" %>

using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;

/// <summary>
/// Web-service for tracking single carton.
/// Notes: 
/// Tasks:
/// 1. Validate Carton number - there should be only one - check min and max before executing.
/// 2. Convert TrackingDS into CartonWSDetail.
/// 3. Return error message in the ErrorMessage field on CartonWSDetail.
/// </summary>
[WebService(Namespace = "https://extranet.argixdirect.com/Tracking/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Carton :System.Web.Services.WebService {
    //Members
    private int mCartonLenMax = 25;
    private int mCartonLenMin = 6;
    public SoapCredential Credentials;
    
    private const string MAX_CHAR_LIMIT_EXCEEDED = "Length of the carton number exceeded the maximum allowed.";
    private const string MIN_CHAR_LIMIT_EXCEEDED = "Length of the carton number is less than the minimum required.";
    private const string MSG_PROFILE_NOT_FOUND = "Your registration information not found. Please contact technical support.";
    private const string TRACKING_CARTONS_NOT_FOUND = "No carton information found. Make sure your tracking number is correct.";
    private const string TRACKINGWS_USER_NOT_AUTH = "Your account has not been given access to use webservice.";
    private const string TRACKINGWS_UNKNOWN_USER = "You will need to pass valid credentials using SoapCredential class to access this web-service.";
    
    //Interface
    [WebMethod]
    [SoapHeader("Credentials")]
    public CartonWSDetail TrackCarton(string cartonNumber) {
        //Return track details for a single carton
        TrackingDS cartonDetail = new TrackingDS();
        CartonWSDetail ds = new CartonWSDetail();
        string cartonNum;
        try {
            //Get config values
            string val = WebConfigurationManager.AppSettings["maxCartonLen"];
            if(val != null && val.Length > 0)
                this.mCartonLenMax = Convert.ToInt32(val);
            val = WebConfigurationManager.AppSettings["minCartonLen"];
            if(val != null && val.Length > 0)
                this.mCartonLenMin = Convert.ToInt32(val);
            
            //First verify if user has passed valid credentials
            bool verified = false;
            if(this.Credentials != null)
                verified = Membership.ValidateUser(Credentials.UserName,Credentials.Password);
            if(!verified) {
                addErrorMessage(ds,TRACKINGWS_UNKNOWN_USER);
                return ds;
            }
            //Verify if user belongs to the allowed group/roles
            if(!isAdmin(Credentials.UserName)) {
                if(!verifyRole()) {
                    addErrorMessage(ds,TRACKINGWS_USER_NOT_AUTH);
                    return ds;
                }
            }
            //Validate if carton number satisfies rules
            cartonNum = validateCarton(cartonNumber);
            if(cartonNum.Length < mCartonLenMin) {
                addErrorMessage(ds,MIN_CHAR_LIMIT_EXCEEDED);
                return ds;
            }
            if(cartonNum.Length > mCartonLenMax) {
                addErrorMessage(ds,MAX_CHAR_LIMIT_EXCEEDED);
                return ds;
            }
            
            //Now retrieve user profile properties
            ProfileCommon profile = new ProfileCommon().GetProfile(Credentials.UserName);
            if(profile != null && (isAdmin(Credentials.UserName) || profile.ClientVendorID != "")) {
                //We have company ID and Type from profile object if user exists.
                cartonDetail.Merge(getCartonDetail(cartonNumber,false,profile.Type,profile.ClientVendorID));
                if(cartonDetail.CartonDetailTable.Rows.Count > 0)
                    ds.Merge(prepareCartonWSDetail(cartonDetail));
                else
                    addErrorMessage(ds,TRACKING_CARTONS_NOT_FOUND);
            } else {
                addErrorMessage(ds,MSG_PROFILE_NOT_FOUND);
            }
        }
        catch(Exception ex) { addErrorMessage(ds, ex.Message); }
        return ds;
    }
    
    //Validation: verifyRole(), validateCarton()
    private bool verifyRole() {
        //Verify user has access rights to this web service
        bool match = false;
        string roles = MembershipServices.TRACKINGWSROLE;
        string[] wsRoles = roles.Split(Convert.ToChar(','));
        string[] userRoles = Roles.GetRolesForUser(Credentials.UserName);
        for(int i = 0; i < userRoles.Length; i++) {
            for(int r = 0; r < wsRoles.Length; r++) {
                if(userRoles[i].ToLower() == wsRoles[r].ToLower()) {
                    match = true;
                    break;
                }
            }
            if(match) break;
        }
        return match;
    }
    private string validateCarton(string cartonNumber) {
        //This method makes sure no invalid chars exist in the user input
        // Make the following replacements:
        // ' becomes ''
        // [ becomes [[]
        // % becomes [%]
        // _ becomes [_]
        // , becomes carriageReturn
        string cn = cartonNumber.Trim();
        cn = cn.Replace("'","''");
        cn = cn.Replace("[","[[]");
        cn = cn.Replace("%","[%]");
        cn = cn.Replace("_","[_]");
        return cn;
    }
    
    //Tracking: getCartonDetail(), prepareCartonWSDetail(), insertCartonDetailRow(), addErrorMessage()
    private TrackingDS getCartonDetail(string cartonNumber,bool lblSequenceNumber,string companyType,string companyID) {
        //
        TrackingServices tracking = new TrackingServices();
        return tracking.GetCartons(cartonNumber,TrackingServices.SEARCHBY_CARTONNUMBER,companyType,companyID);
    }
    private CartonWSDetail prepareCartonWSDetail(TrackingDS cartonDetail) {
        //
        CartonWSDetail cartonWSDetail = new CartonWSDetail();
        foreach(TrackingDS.CartonDetailTableRow row in cartonDetail.CartonDetailTable.Rows) {
            insertCartonDetailRow(cartonWSDetail,row);
        }
        return cartonWSDetail;
    }
    private void insertCartonDetailRow(CartonWSDetail cartonDS,TrackingDS.CartonDetailTableRow cartonRow) {
        //
        CartonWSDetail.CartonWSDetailTableRow detailRow = cartonDS.CartonWSDetailTable.NewCartonWSDetailTableRow();
        string storeAddLine = cartonRow.SA1.Trim();
        if(!cartonRow.IsSA2Null() && cartonRow.SA2.Trim().Length > 0) storeAddLine += ", " + cartonRow.SA2.Trim() + ", ";
        detailRow.CartonNumber = cartonRow.CTN.Trim();
        detailRow.Client = cartonRow.CLNM.Trim();
        detailRow.StoreNumber = cartonRow.S.ToString();
        detailRow.StoreName = cartonRow.SNM.Trim() + ", " + storeAddLine + cartonRow.SCT.Trim() + ", " + cartonRow.SST.Trim() + " " + cartonRow.SZ.ToString();
        detailRow.Vendor = cartonRow.IsVNMNull() ? "" : cartonRow.VNM.Trim();
        if(!cartonRow.IsPUDNull() && cartonRow.PUD.Trim().Length > 0) detailRow.PickupDate = Convert.ToDateTime(cartonRow.PUD.Trim());
        detailRow.BLNumber = cartonRow.IsBLNull() ? "" : cartonRow.BL.ToString();
        detailRow.TLNumber = cartonRow.IsTLNull() ? "" : cartonRow.TL.Trim();
        detailRow.LblSeqNumber = cartonRow.LBL.ToString();
        detailRow.PONumber = cartonRow.IsPONull() ? "" : cartonRow.PO.Trim();
        detailRow.Weight = cartonRow.WT;
        if(!cartonRow.IsACTSDDNull() && cartonRow.ACTSDD.Trim().Length > 0) detailRow.ActualStoreDeliveryDate = Convert.ToDateTime(cartonRow.ACTSDD.Trim());

        //Sort facility
        if(!cartonRow.IsASFDNull() && cartonRow.ASFD.Trim().Length > 0) {
            detailRow.SortFacilityArrivalDate = Convert.ToDateTime(cartonRow.ASFD.Trim() + " " + cartonRow.ASFT.Trim());
            detailRow.SortFacilityArrivalStatus = "Arrived At Sort Facility";
            detailRow.SortFacilityLocation = cartonRow.IsSRTLOCNull() ? "" : cartonRow.SRTLOC.Trim();
        }
        if(!cartonRow.IsADPDNull() && cartonRow.ADPD.Trim().Length > 0) {
            detailRow.ActualDepartureDate = Convert.ToDateTime(cartonRow.ADPD.Trim() + " " + cartonRow.ADPT.Trim());
            detailRow.ActualDepartureStatus = "Departed Sort Facility";
            detailRow.ActualDepartureLocation = cartonRow.IsSRTLOCNull() ? "" : cartonRow.SRTLOC.Trim();
        }

        //Delivery terminal 
        //1. BOL confirmed (trailer arrived in AS400): SCNTP=0, AARD!=null; 
        //2. Agent scan: SCNTP=1, AARD!=null, OM=Over(O)||Short(S)||MisRoute(A)||Match(M)
        if(!cartonRow.IsAARDNull() && cartonRow.AARD.Trim().Length > 0) {
            detailRow.ActualArrivalDate = Convert.ToDateTime(cartonRow.AARD.Trim() + " " + cartonRow.AART.Trim());
            if(cartonRow.SCNTP == 1) {
                switch(cartonRow.OM) {
                    case "M": detailRow.ActualArrivalStatus = "Scanned At Delivery Terminal"; break;
                    case "S": detailRow.ActualArrivalStatus = "Short At Delivery Terminal"; break;
                    case "O": detailRow.ActualArrivalStatus = "Over At Delivery Terminal"; break;
                    case "A": detailRow.ActualArrivalStatus = "MisRoute At Delivery Terminal"; break;
                }
            }
            else
                detailRow.ActualArrivalStatus = "Arrived At Delivery Terminal";
            string SAGCity = cartonRow.IsSAGCTNull() ? "" : cartonRow.SAGCT.Trim();
            if(!cartonRow.IsSAGCTNull() && cartonRow.SAGCT.Trim().Length > 0)
                detailRow.ActualArrivalLocation = cartonRow.SAGCT.Trim() + "/" + cartonRow.SAGST.Trim();
            else
                detailRow.ActualArrivalLocation = cartonRow.IsAGCTNull() ? "" : cartonRow.AGCT.Trim() + "/" + cartonRow.AGST.Trim();
        }
        
        //Store Delivery
        if(!cartonRow.IsACTSDDNull() && cartonRow.ACTSDD.Trim().Length > 0) {
            detailRow.ActualStoreDeliveryDate = Convert.ToDateTime(cartonRow.ACTSDD.Trim());
            detailRow.ActualStoreDeliveryStatus = "Out For Delivery";
            detailRow.ActualStoreDeliveryLocation = cartonRow.IsSCTNull() ? "" : cartonRow.SCT.Trim() + "/" + cartonRow.SST.Trim();
            detailRow.ScheduledDeliveryDate = Convert.ToDateTime(cartonRow.ACTSDD.Trim()).Date;
        }
        
        //POD Scan
        if(cartonRow.SCNTP == 3 && !cartonRow.IsSCDNull() && cartonRow.SCD.Trim().Length > 0) {
            //Check for mis-routed carton- podScan is estimated by UPS (or other agent)
            detailRow.PODScanDate = Convert.ToDateTime(cartonRow.SCD.Trim() + " " + cartonRow.SCTM.Trim());
            if(cartonRow.T.Trim().Length == 18 && cartonRow.T.Trim().Substring(0,2).ToLower() == "1z")
                detailRow.PODScanStatus = "Rerouted: Tracking # " + cartonRow.T.Trim();
            else {
                switch(cartonRow.OM) {
                    case "M": detailRow.PODScanStatus = cartonRow.ISMN == 1 ? "Delivered (Scan N/A - Manual Entry)" : "Delivered"; break;
                    case "S": detailRow.PODScanStatus = "Short At Delivery"; break;
                    case "O": detailRow.PODScanStatus = "Over At Delivery"; break;
                    case "A": detailRow.PODScanStatus = "MisRoute At Delivery"; break;
                }
            }
            detailRow.PODScanLocation = cartonRow.SCT.Trim() + "/" + cartonRow.SST.Trim();
        }
        cartonDS.CartonWSDetailTable.AddCartonWSDetailTableRow(detailRow);
        cartonDS.AcceptChanges();
    }
    private void addErrorMessage(CartonWSDetail data,string message) {
        //
        data.Clear();
        CartonWSDetail.CartonWSDetailTableRow row = data.CartonWSDetailTable.NewCartonWSDetailTableRow();
        row.ErrorMessage = message;
        data.CartonWSDetailTable.AddCartonWSDetailTableRow(row);
        data.AcceptChanges();
    }
    private bool isAdmin(string userName) {
        bool result = false;
        string[] userRoles = Roles.GetRolesForUser(userName);
        for(int i=0; i<userRoles.Length; i++) {
            if(userRoles[i].ToLower() == MembershipServices.ADMINROLE) {
                result = true;
                break;
            }
        }
        return result;
    }
}