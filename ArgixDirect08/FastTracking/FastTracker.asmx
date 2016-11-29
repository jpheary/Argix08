<%@ WebService Language="C#" Class="FastTracker" %>

using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;

[WebService(Namespace = "http://www.argixdirect.com/Tracking/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class FastTracker:System.Web.Services.WebService {
    //Members
    public SoapCredential Credentials;
    
    private const int CARTON_LENGTH_MAX = 30;
    private const int CARTON_LENGTH_MIN = 6;
    private const string INVALID_CARTON_LENGTH = "Carton number must be 6-30 characters.";
    private const string CARTON_NOT_FOUND = "No carton information found. Make sure your tracking number is correct.";
    private const string INVALID_CREDENTIALS = "Please pass valid credentials using SoapCredential class to access this web-service.";

    //Interface
    [WebMethod]
    [SoapHeader("Credentials")]
    public Argix.TrackingItems TrackCarton(string cartonNumber) {
        //Return track details for a single carton
        Argix.TrackingItems items = new Argix.TrackingItems();
        try {            
            //Verify credentials
            string username="";
            if(this.Credentials != null) {
                //Authenticate
                switch(Credentials.UserName) {
                    case "username":
                        if(Credentials.Password == "password") username = Credentials.UserName;
                        break;
                }
            }
            if(username.Length > 0) {
                //Groom carton number
                string number = cartonNumber.Trim();
                number = number.Replace("'","''");
                number = number.Replace("[","[[]");
                number = number.Replace("%","[%]");
                number = number.Replace("_","[_]");
                
                //Determine client
                string companyID="";
                switch(username) {
                    case "username": companyID = "136"; break;
                }
                if(companyID.Length > 0) {
                    //Validate and track
                    if(number.Length < CARTON_LENGTH_MAX && number.Length > CARTON_LENGTH_MIN) {
                        string[] numbers = new string[] { number };
                        items = new Argix.TrackingProxy().TrackCartons(numbers,companyID);
                        if(items.Count == 0)
                            addErrorMessage(items,CARTON_NOT_FOUND);
                    }
                    else
                        addErrorMessage(items,INVALID_CARTON_LENGTH);
                }
                else
                    addErrorMessage(items,"A company ID could not be found.");
            }
            else
                addErrorMessage(items,INVALID_CREDENTIALS);
        }
        catch(Exception ex) { addErrorMessage(items,ex.Message); }
        return items;
    }

    private void addErrorMessage(Argix.TrackingItems items,string message) {
        Argix.TrackingItem item = new Argix.TrackingItem();
        item.ErrorMessage = message;
        items.Add(item);
    }
}

public class SoapCredential:SoapHeader {
    //Members
    public string UserName="";
    public string Password="";
}
