using System;
using System.Web.Services.Protocols;

/// <summary>
/// SoapCredential inherits from SoapHeader and defines what to expect in Soap Header for credentials.
/// </summary>
public class SoapCredential : SoapHeader {
    //Members
    public string UserName="";
    public string Password="";
}
