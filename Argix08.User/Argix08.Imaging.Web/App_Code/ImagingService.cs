using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Xml;
using Microsoft.SharePoint.Utilities;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ImagingService:IImagingService {
        //Members

        //Interface
        public ImagingService() { }
        public string GetPortalSearchInfo() {
            //Make sure we have removed the last result from the session
            string psi="";
            try {
                rgxvmsp.QueryService qs = new rgxvmsp.QueryService();
                qs.Credentials = getCredentials();
                qs.PreAuthenticate = true;
                qs.Url = WebConfigurationManager.AppSettings["rgxvmsp.search"].ToString();
                //psi = qs.GetPortalSearchInfo();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in GetPortalSearchInfo().",ex); }
            return psi;
        }
        public string GetSearchMetadata() {
            //Make sure we have removed the last result from the session
            string xml="";
            try {
                rgxvmsp.QueryService qs = new rgxvmsp.QueryService();
                qs.Credentials = getCredentials();
                qs.PreAuthenticate = true;
                qs.Url = WebConfigurationManager.AppSettings["rgxvmsp.search"].ToString();
                DataSet ds = null;  //qs.GetSearchMetadata();
                if(ds != null) xml = ds.GetXml();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in GetSearchMetadata().",ex); }
            return xml;
        }
        public DocumentClasses GetDocumentClasses() {
            //Retrieve document classes
            DocumentClasses docs=null;
            try {
                docs = new DocumentClasses();
                System.Xml.XmlDataDocument xmlDoc = new System.Xml.XmlDataDocument();
                xmlDoc.DataSet.ReadXml(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\App_Data\\documentclass.xml");
                for(int i=0;i<xmlDoc.DataSet.Tables["DocumentClassTable"].Rows.Count;i++) {
                    DocumentClass dc = new DocumentClass(xmlDoc.DataSet.Tables["DocumentClassTable"].Rows[i]["Department"].ToString(),xmlDoc.DataSet.Tables["DocumentClassTable"].Rows[i]["ClassName"].ToString());
                    docs.Add(dc);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in GetDocumentClasses().",ex); }
            return docs;
        }
        public DocumentClasses GetDocumentClasses(string department) {
            //Retrieve document classes
            DocumentClasses docs=null;
            try {
                docs = new DocumentClasses();
                DocumentClasses _docs = GetDocumentClasses();
                foreach(DocumentClass dc in _docs) {
                    if(dc.Department == department) docs.Add(dc);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in GetDocumentClasses(string).",ex); }
            return docs;
        }
        public MetaDatas GetMetaData() {
            //Retrieve document class metadata
            MetaDatas metas=null;
            try {
                metas = new MetaDatas();
                System.Xml.XmlDataDocument xmlMeta = new System.Xml.XmlDataDocument();
                xmlMeta.DataSet.ReadXml(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\App_Data\\metadata.xml");
                for(int i=0;i<xmlMeta.DataSet.Tables["MetaDataTable"].Rows.Count;i++) {
                    MetaData md = new MetaData(xmlMeta.DataSet.Tables["MetaDataTable"].Rows[i]["ClassName"].ToString(),xmlMeta.DataSet.Tables["MetaDataTable"].Rows[i]["Property"].ToString());
                    metas.Add(md);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in GetMetaData().",ex); }
            return metas;
        }
        public MetaDatas GetMetaData(string className) {
            //Retrieve document class metadata for the specified className
            MetaDatas metas = null;
            try {
                metas = new MetaDatas();
                MetaDatas _metas = GetMetaData();
                foreach(MetaData md in _metas) {
                    if(md.ClassName == className) metas.Add(md);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in GetMetaData(string).",ex); }
            return metas;
        }
        public byte[] GetSharePointImageStream(SearchDS search) {
            //Return an image from SharePoint as a byte[]
            byte[] bytes=null;
            try {
                //Search for image
                DataSet ds = SearchSharePointImageStore(search);
                if(ds.Tables[0].Rows.Count > 0) {
                    //Get image url
                    string url = ds.Tables[0].Rows[0]["DAV%3ahref"].ToString();

                    //Pull the image from a remote client and load into an image object
                    WebClient wc = new WebClient();
                    wc.Credentials = getCredentials();
                    bytes = wc.DownloadData(url);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in GetSharePointImageStream(SearchDS).",ex); }
            return bytes;
        }
        public DataSet SearchSharePointImageStore(SearchDS search) {
            //
            DataSet ds=null;
            try {
                string SQL = getSQLQuery(search.SearchTable[0].ScopeName);
                SQL = SQL.Replace("selectStmnt",getSelectClause(search.SearchTable[0].DocumentClass)).Replace("whereClause",getWhereClause(search)).Replace("MaxSearchResults",search.SearchTable[0].MaxResults.ToString());
                rgxvmsp.QueryService qs = new rgxvmsp.QueryService();
                qs.Credentials = getCredentials();
                qs.PreAuthenticate = true;
                //qs.Timeout = 500000;    //Default 100000 msec
                qs.Url = WebConfigurationManager.AppSettings["rgxvmsp.spsearch"].ToString();
                qs.Credentials = getCredentials();
                ds = qs.QueryEx(SQL);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error in SearchSharePointImageStore(SearchDS).",ex); }
            return ds;
        }

        private string getSQLQuery(string scopeName) {
            //<![CDATA[SELECT selectStmnt FROM SCOPE() WHERE (\"isdocument\" = 1 AND whereClause) ORDER BY BatchDate DESC ]]>
            //<![CDATA[SELECT selectStmnt FROM SCOPE() WHERE (\"scope\"='" + scopeName + "' AND \"isdocument\" = 1 AND whereClause) ORDER BY BatchDate DESC ]]>" +
            //<![CDATA[SELECT DAV:displayname, DAV:href, TBBarCode, TL, CL, DIV, Store, BatchDate FROM SCOPE() WHERE (isdocument=1) AND (TL LIKE '%') ]]>
            string query = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" +
                "<QueryPacket xmlns=\"urn:Microsoft.Search.Query\" Revision=\"1000\">" +
                   "<Query domain=\"QDomain\">" +
                       "<QueryId>Client Query ID</QueryId>" +
                       "<OriginatorId>Tsort Imaging Search v3.5</OriginatorId>" +
                       "<SupportedFormats>" +
                           "<Format>urn:Microsoft.Search.Response.Document.Document</Format>" +
                       "</SupportedFormats>" +
                       "<Context>" +
                           "<QueryText language=\"en-US\" type=\"MSSQLFT\">" +
                                   "<![CDATA[SELECT selectStmnt FROM SCOPE() WHERE (\"isdocument\" = 1 AND whereClause) ORDER BY BatchDate DESC ]]>" +
                           "</QueryText>" +
                           "<OriginatorContext>" +
                               "<Argix xmlns=\"urn:www.ArgixDirect.com\">SearchReturn</Argix>" +
                           "</OriginatorContext>" +
                       "</Context>" +
                       "<Range>" +
                           "<StartAt>1</StartAt>" +
                           "<Count>MaxSearchResults</Count>" +
                       "</Range>" +
                   "</Query>" +
               "</QueryPacket>";
            return query;
        }
        private string getSelectClause(string docClass) {
            //Build a SELECT clause that includes all properties (fields) for the specified Document Class
            StringBuilder sb = new StringBuilder("\"DAV:displayname\", \"DAV:href\"");
            MetaDatas metas = GetMetaData(docClass);
            foreach(MetaData md in metas) {
                sb.Append(", \"" + XmlConvert.EncodeName(md.Property) + "\"");
            }
            return sb.ToString();
        }
        private string getWhereClause(SearchDS search) {
            //Build a WHERE clause
            string propertyName;
            StringBuilder sb = new StringBuilder();
            if(search.SearchTable[0].PropertyValue.Trim().Length > 0) {
                propertyName = XmlConvert.EncodeName(search.SearchTable[0].PropertyName);
                if(propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,search.SearchTable[0].PropertyValue.Trim()));
                else
                    sb.Append("Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",search.SearchTable[0].PropertyValue.Trim()));
            }
            if(!search.SearchTable[0].IsPropertyName1Null() && !search.SearchTable[0].IsPropertyValue1Null() && !search.SearchTable[0].IsOperand1Null() && search.SearchTable[0].PropertyValue1.Trim().Length > 0) {
                sb.Append(" " + search.SearchTable[0].Operand1);
                propertyName = XmlConvert.EncodeName(search.SearchTable[0].PropertyName1);
                if(propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,search.SearchTable[0].PropertyValue1.Trim()));
                else
                    sb.Append(" " + "Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",search.SearchTable[0].PropertyValue1.Trim()));
            }
            if(!search.SearchTable[0].IsPropertyName2Null() && !search.SearchTable[0].IsPropertyValue2Null() && !search.SearchTable[0].IsOperand2Null() && search.SearchTable[0].PropertyValue2.Trim().Length > 0) {
                sb.Append(" " + search.SearchTable[0].Operand2);
                propertyName = XmlConvert.EncodeName(search.SearchTable[0].PropertyName2);
                if(propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,search.SearchTable[0].PropertyValue2.Trim()));
                else
                    sb.Append(" " + "Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",search.SearchTable[0].PropertyValue2.Trim()));
            }
            if(!search.SearchTable[0].IsPropertyName3Null() && !search.SearchTable[0].IsPropertyValue3Null() && !search.SearchTable[0].IsOperand3Null() && search.SearchTable[0].PropertyValue3.Trim().Length > 0) {
                sb.Append(" " + search.SearchTable[0].Operand3);
                propertyName = XmlConvert.EncodeName(search.SearchTable[0].PropertyName3);
                if(propertyName.ToLower().Contains("date") || propertyName.ToLower() == "created")
                    sb.Append(getDateClause(propertyName,search.SearchTable[0].PropertyValue3.Trim()));
                else
                    sb.Append(" " + "Property LIKE 'SearchText' ".Replace("Property",propertyName).Replace("SearchText",search.SearchTable[0].PropertyValue3.Trim()));
            }
            return sb.ToString();
        }
        private string getDateClause(string propertyName,string propertyValue) {
            //Build a WHERE clause for a datetime field
            string from = SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(propertyValue));
            string to = SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(propertyValue).AddDays(1).AddSeconds(-1));
            return " ((" + propertyName + " >= '" + from + "') AND (" + propertyName + " <= '" + to + "'))";
        }
        private ICredentials getCredentials() {
            //Determine credentials for web service access
            ICredentials credentials = System.Net.CredentialCache.DefaultCredentials;
            if(WebConfigurationManager.AppSettings["UseSpecificCredentials"] == "1") {
                string username = WebConfigurationManager.AppSettings["username"];
                string password = WebConfigurationManager.AppSettings["password"];
                string domain = WebConfigurationManager.AppSettings["domain"];
                credentials = new NetworkCredential(username,password,domain);
            }
            return credentials;
        }
    }
}
