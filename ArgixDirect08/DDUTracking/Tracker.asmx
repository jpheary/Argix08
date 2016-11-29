<%@ WebService Language="C#" Class="Tracker" %>

using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;

[WebService(Namespace="http://ddu.argixdirect.com")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Tracker: System.Web.Services.WebService {
    //Members
    public SoapCredential Credentials;

    private const int CARTON_LENGTH = 22;
    private const string INVALID_CARTON_LENGTH = "Carton number must be 22 characters.";
    private const string CARTON_NOT_FOUND = "No carton information found. Make sure your tracking number is correct.";
    private const string INVALID_CREDENTIALS = "Please pass valid credentials using SoapCredential class to access this web-service.";
    
    //Interface
    [WebMethod]
    [SoapHeader("Credentials")]
    public TrackDS TrackCarton(string cartonNumber) {
        //Return track details for a single carton
        TrackDS trackDS = new TrackDS();
        try {            
            //Verify credentials
            if(this.Credentials != null && (Credentials.UserName == "dduuser" && Credentials.Password == "bn2011")) {
                //Validate carton number
                string number = cartonNumber.Trim();
                number = number.Replace("'","''");
                number = number.Replace("[","[[]");
                number = number.Replace("%","[%]");
                number = number.Replace("_","[_]");

                if(number.Length == CARTON_LENGTH) {
                    string[] numbers = new string[] { number };
                    TrackDS ds = new EnterpriseFactory().TrackCartons(numbers,null,null);
                    if(ds.TrackingSummaryTable.Rows.Count > 0 || ds.TrackingDetailTable.Rows.Count > 0)
                        trackDS.Merge(ds);
                    else
                        addErrorMessage(trackDS, cartonNumber, CARTON_NOT_FOUND);
                } 
                else
                    addErrorMessage(trackDS,cartonNumber,INVALID_CARTON_LENGTH);
            }
            else
                addErrorMessage(trackDS,cartonNumber,INVALID_CREDENTIALS);
        }
        catch(Exception ex) { addErrorMessage(trackDS,cartonNumber,ex.Message); }
        return trackDS;
    }

    private void addErrorMessage(TrackDS ds,string cartonNumber, string message) {
        //
        ds.Clear();
        TrackDS.TrackingSummaryTableRow row = ds.TrackingSummaryTable.NewTrackingSummaryTableRow();
        row.ItemNumber = cartonNumber;
        row.Status = message;
        ds.TrackingSummaryTable.AddTrackingSummaryTableRow(row);
        ds.AcceptChanges();
    }
}

public class SoapCredential:SoapHeader {
    //Members
    public string UserName="";
    public string Password="";
}
