//	File:	USPSClient.cs
//	Author:	J. Heary
//	Date:	11/13/08
//	Desc:	Web service proxy for communicating with USPS Track and Confirm 
//          services using HTTP-GET/HTTP-PUT protocol bindings.
//	Rev:	12/01/09 (jph)- added USPSClient(string,string).
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.Services.Protocols;

public class USPSClient:HttpSimpleClientProtocol {
    //Members
    private string mUserID = "";
    private string mPassword = "";

    //Interface
    public USPSClient() {
        //Default constructor
        base.Url = ConfigurationManager.AppSettings["WebSvcUrl"];
        this.mUserID = ConfigurationManager.AppSettings["UserID"];
    }
    public USPSClient(string webSvcUrl,string userID) {
        //Constructor
        base.Url = webSvcUrl;
        this.mUserID = userID;
    }
    public WebResponse TrackRequest(string cartonNumber) {
        //Track the specified carton and return a simple tracking response
        WebResponse response = null;
        try {
            //Validate
            if(base.Url.Length == 0)
                throw new ApplicationException("Must specify a web service URL.");
            if(this.mUserID.Length == 0)
                throw new ApplicationException("Must specify a valid USPS UserID.");

            //Create URL request per USPS Track and Confirm specifications
            Uri uri = new Uri(base.Url + "?API=TrackV2&XML=<TrackRequest USERID='" + this.mUserID + "'><TrackID ID='" + cartonNumber + "'></TrackID></TrackRequest>");
            response = base.GetWebResponse(base.GetWebRequest(uri));
        }
        catch(Exception ex) { throw new ApplicationException("Unexpected error when requesting tracking data.",ex); }
        return response;
    }
    public WebResponse TrackFieldRequest(string cartonNumber) {
        //Track the specified carton and return a detailed tracking response
        WebResponse response = null;
        try {
            //Validate
            if(base.Url.Length == 0)
                throw new ApplicationException("Must specify a web service URL.");
            if(this.mUserID.Length == 0)
                throw new ApplicationException("Must specify a valid USPS UserID.");

            //Create URL request per USPS Track and Confirm specifications
            Uri uri = new Uri(base.Url + "?API=TrackV2&XML=<TrackFieldRequest USERID='" + this.mUserID + "'><TrackID ID='" + cartonNumber + "'></TrackID></TrackFieldRequest>");
            response = base.GetWebResponse(base.GetWebRequest(uri));
        }
        catch(Exception ex) { throw new ApplicationException("Unexpected error when requesting tracking data.",ex); }
        return response;
    }
    public WebResponse TrackFieldRequest(string[] cartonNumbers) {
        //Track the specified cartons
        WebResponse response = null;
        try {
            //Validate
            if(base.Url.Length == 0)
                throw new ApplicationException("Must specify a web service URL.");
            if(this.mUserID.Length == 0)
                throw new ApplicationException("Must specify a valid USPS UserID.");

            //Create URL request per USPS Track and Confirm specifications
            Uri uri = null;
            string trackIDs = "";
            for(int i = 0;i < cartonNumbers.Length;i++) {
                trackIDs += "<TrackID ID='" + cartonNumbers[i] + "'></TrackID>";
            }
            uri = new Uri(base.Url + "?API=TrackV2&XML=<TrackFieldRequest USERID='" + this.mUserID + "'>" + trackIDs + "</TrackFieldRequest>");
            response = base.GetWebResponse(base.GetWebRequest(uri));
        }
        catch(Exception ex) { throw new ApplicationException("Unexpected error when requesting tracking data.",ex); }
        return response;
    }
}

