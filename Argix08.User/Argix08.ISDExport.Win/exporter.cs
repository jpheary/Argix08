using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Argix.Enterprise;

namespace Argix.AgentLineHaul {
	//
	public abstract class Exporter {
		//Members

		//Interface
		public Exporter() { }
        public abstract bool Export(string exportFile,string clientCode,string scanner,string userID,Pickup pickup, SortedItemDS sortedItems);
        protected void ExportSortedItems(SortedItemDS sortedItems,string xsltFile,string csvFile) {
            //UNUSED: This is for an xsl-based approach
            //XmlTextWriter writer = null;
            //try {
            //    //
            //    XmlDataDocument xml = new XmlDataDocument(sortedItems);
            //    XslTransform xslt = new XslTransform();
            //    xslt.Load(xsltFile);
            //    writer = new XmlTextWriter(csvFile,Encoding.ASCII);
            //    xslt.Transform(xml,null,writer,null);
            //}
            //catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting sorted items.",ex); }
            //finally { if(writer != null) writer.Close(); }
        }
	}

    public class RDS3Exporter :Exporter {
        //Members

        //Interface
        public RDS3Exporter() { }
        public override bool Export(string exportFile,string clientCode,string scanner,string userID,Pickup pickup,SortedItemDS sortedItems) {
            //Exports sorted items dataset to a file specified by the database
            bool exported = false;
            StreamWriter writer = null;
            try {
                    //Validate file is unique
                    if(File.Exists(exportFile))
                        throw new Exception("Export file " + exportFile + " already exists. ");

                    //Create the new file and save sorted items for this pickup to disk
                    writer = new StreamWriter(new FileStream(exportFile,FileMode.Create,FileAccess.ReadWrite));
                    writer.BaseStream.Seek(0,SeekOrigin.Begin);
                    for(int j = 0;j < sortedItems.SortedItemTable.Rows.Count;j++)
                        writer.WriteLine(formatSortedItemRecord((SortedItemDS.SortedItemTableRow)sortedItems.SortedItemTable.Rows[j]));
                    writer.Flush();
                    exported = true;
                }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting sorted items.",ex); }
            finally { if(writer != null) writer.Close(); }
            return exported;
        }
        private string formatSortedItemRecord(SortedItemDS.SortedItemTableRow sortedItem) {
            //Returns a string formatted by concatination of the Catons row columns
            //Note: Include time in pickup date string format (even though no time in data date field)
            const string DELIM = ",";
            const string QUOTE = "\"";
            string item = "";
            try {
                item =
                    QUOTE + sortedItem.CLIENT_NUMBER.Trim() + QUOTE + DELIM +
                    QUOTE + sortedItem.CLIENT_DIV_NUM.Trim() + QUOTE + DELIM +
                    QUOTE + sortedItem.VENDOR_NUMBER.Trim() + QUOTE + DELIM +
                    sortedItem.PICKUP_DATE.ToString("MM/dd/yyyy HH:mm:ss") + DELIM +
                    sortedItem.PICKUP_NUMBER + DELIM +
                    QUOTE + sortedItem.VENDOR_ITEM_NUMBER + QUOTE + DELIM +
                    QUOTE + sortedItem.LABEL_SEQ_NUMBER.Trim() + QUOTE + DELIM +
                    sortedItem.ITEM_WEIGHT + DELIM +
                    QUOTE + sortedItem.STORE.ToString("00000") + QUOTE + DELIM +
                    QUOTE + sortedItem.SORT_DATE.ToShortDateString() + QUOTE + DELIM +
                    QUOTE + sortedItem.END_TIME.ToLongTimeString() + QUOTE + DELIM +
                    QUOTE + (sortedItem.IsDAMAGE_CODENull() ? "" : sortedItem.DAMAGE_CODE.Trim()) + QUOTE + DELIM +
                    QUOTE + sortedItem.FreightType.Trim() + QUOTE;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting sortedItem export string.",ex); }
            return item;
        }
    }
    public class RDS4Exporter:Exporter {
        //Members

        //Interface
        public RDS4Exporter() { }
        public override bool Export(string exportFile,string clientCode,string scanner,string userID,Pickup pickup,SortedItemDS sortedItems) {
            //Exports sorted items dataset to a file specified by the database
            bool exported = false;
            StreamWriter writer = null;
            try {
                //Validate file is unique
                if(File.Exists(exportFile))
                    throw new ApplicationException("Export file " + exportFile + " already exists. ");

                //Create the new file and save sorted items for this pickup to disk
                writer = new StreamWriter(new FileStream(exportFile,FileMode.Create,FileAccess.ReadWrite));
                writer.BaseStream.Seek(0,SeekOrigin.Begin);
                writer.WriteLine(formatTrailerRecord(clientCode,pickup));
                for(int j = 0;j < sortedItems.SortedItemTable.Rows.Count;j++)
                    writer.WriteLine(formatSortedItemRecord(clientCode,(SortedItemDS.SortedItemTableRow)sortedItems.SortedItemTable.Rows[j]));
                writer.Flush();
                exported = true;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting sorted items.",ex); }
            finally { if(writer != null) writer.Close(); }
            return exported;
        }
        private string formatTrailerRecord(string clientCode,Pickup pickup) {
            //
            #region Trailer record
            //TPAYLES06JSBM00381786527    109873  DIGYI
            //TBLOCKBPPSODM0078233684A69A 0030108 BILTI
            //-======--===--------========--------====-
            //||     | |  |       |       |       |   |
            //ab     c d  e       f       g       h   i

            //a) Record Type [Required; 1]
            //   - Constant "T"
            //b) Company Code (See Company Code Table) [Max Length 6]
            //   - Client file = ANNTAY (Ann Taylor); LTA = Argix
            //     Argix file = LTA
            //c) Scanner Number [Required; 2]
            //   – Blank
            //d) Scanner UserID [Required; 3]
            //   – Blank
            //e) Trip Number [Required; 8]
            //   – Client file = pickup VendorKey (first 8 positions) 
            //   - Argix file = pickup TDSID (last 8 positions)
            //f) Trailer Number [8]
            //   – pickup Trailer# or blank
            //g) Seal Number [8]
            //   – pickup Seal# or blank
            //h) Carrier SCAC Code [4]
            //   – Blank
            //i) Scanner File Type [Required; 1]
            //   - I = Inbound Scan
            #endregion
            string s = "";
            try {
                string tds = pickup.TDSNumber.ToString().PadRight(8,' ');
                string vk = pickup.VendorKey.PadRight(8,' ');
                string tripNum = (clientCode.Trim().ToLower() == global::Argix.Properties.Settings.Default.ArgixClientCode.Trim().ToLower()) ? tds.Substring(tds.Length - 8, 8) : vk.Substring(vk.Length - 8, 8);
                s = "T" +
                    clientCode.Trim().PadRight(6,' ') +
                    "".PadRight(2,' ') +
                    "".PadRight(3,' ') +
                    parseNonAlphaNumeric(tripNum).PadRight(8,' ') +
                    parseNonAlphaNumeric(pickup.TrailerNumber).PadRight(8,' ').Substring(0,8) +
                    parseNonAlphaNumeric(pickup.SealNumber).PadRight(8,' ').Substring(0,8) +
                    "".PadRight(4,' ') +
                    "I";
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting trailer export string.",ex); }
            return s;
        }
        private string formatSortedItemRecord(string clientCode,SortedItemDS.SortedItemTableRow sortedItem) {
            //Returns a string representation of the sorted item
            #region Sorted Item record
            //S29127133277               020021220094806123456
            //S000744232835              020040809084200      
            //S1850200205252             020040809102100      
            //S060440839041028135854050  020041102084439      
            //S0000420018049386522806125 020061031082012      
            //-==========================-========------======
            //||                         ||       |     |
            //ab                         cd       e     f

            //a) Barcode Origin [1]
            //   – S = Scanned; M = Manual (always S for RDS Export application)
            //b) Barcode Stream [26]
            //   - sorted item vendor item# in Client file
            //   - 24 position sorted item# in Argix file
            //c) Damage Code [1]
            //   - sorted item damage code
            //d) Scan Date [8, YYYYMMDD]
            //   - sorted item sort date
            //e) Scan Time [6, HHMMSS]
            //   - sorted item end time
            //f) Override (Inbound) or From Store (Return, Transfer) [6]
            //   – sorted item weight (999900 = 9999.00, i.e. decimal understood)
            #endregion
            string s = "";
            try {
                string lsn24 = sortedItem.CLIENT_NUMBER + (sortedItem.FreightType.ToUpper() == "R" ? "88" : "44") + sortedItem.STORE.ToString("00000") + sortedItem.LABEL_SEQ_NUMBER + "0";
                string barcode = (clientCode.Trim().ToLower() == global::Argix.Properties.Settings.Default.ArgixClientCode.Trim().ToLower()) ? lsn24 : sortedItem.VENDOR_ITEM_NUMBER.Trim();
                string dc = sortedItem.IsDAMAGE_CODENull() ? "0" : sortedItem.DAMAGE_CODE.Trim().PadLeft(2,'0').Substring(1,1);
                s = "S" +
                    barcode.PadRight(26,' ') +
                    dc +
                    sortedItem.SORT_DATE.ToString("yyyyMMdd") +
                    sortedItem.END_TIME.ToString("HHmmss") +
                    (sortedItem.ITEM_WEIGHT.ToString() + "00").PadLeft(6, '0');
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting sorted item export string.",ex); }
            return s;
        }
        private string parseNonAlphaNumeric(string s) {
            string alphaNum="";
            s = s.Trim();
            for(int i=0;i<s.Length;i++) {
                string c = s.Substring(i,1);
                if(Regex.IsMatch(c,"[0-9A-Za-z]")) alphaNum += c;
            }
            return alphaNum;
        }
    }
    public class PCSExporter :Exporter {
        //Members

        //Interface
        public PCSExporter() { }
        public override bool Export(string exportFile,string clientCode,string scanner,string userID,Pickup pickup,SortedItemDS sortedItems) {
            //Exports sorted items dataset to a file specified by the database
            bool exported = false;
            StreamWriter writer = null;
            try {
                //Validate file is unique
                if(File.Exists(exportFile))
                    throw new ApplicationException("Export file " + exportFile + " already exists. ");

                //Create the new file and save sorted items for this pickup to disk
                writer = new StreamWriter(new FileStream(exportFile,FileMode.Create,FileAccess.ReadWrite));
                writer.BaseStream.Seek(0,SeekOrigin.Begin);
                writer.WriteLine(formatTrailerRecord(clientCode,pickup,scanner,userID));
                for(int j = 0;j < sortedItems.SortedItemTable.Rows.Count;j++)
                    writer.WriteLine(formatSortedItemRecord(clientCode,(SortedItemDS.SortedItemTableRow)sortedItems.SortedItemTable.Rows[j]));
                writer.Flush();
                exported = true;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting cartons.",ex); }
            finally { if(writer != null) writer.Close(); }
            return exported;
        }
        private string formatTrailerRecord(string clientCode,Pickup pickup,string scanner,string userID) {
            //
            #region Trailer record
            //TPAYLES06JSBM00381786527    109873  
            //TBLOCKBPPSODM0078233684A69A 0030108 
            //-======--===--------========--------
            //||     | |  |       |       |       
            //ab     c d  e       f       g       

            //a) Record Type [Required; 1]
            //   - Constant "T"
            //b) Company Code (See Company Code Table) [Max Length 6]
            //   - ISDClient::Client 
            //c) Scanner Number [Required; 2]
            //   – ISDClient::Scanner
            //d) Scanner UserID [Required; 3]
            //   – ISDClient::UserID
            //e) Trip Number [Required; 8]
            //   – pickup::VendorKey (first 8 positions) 
            //f) Trailer Number [8]
            //   – pickup::Trailer# or blank
            //g) Seal Number [8]
            //   – pickup::Seal# or blank
            #endregion
            string s = "";
            try {
                string tripNum = pickup.VendorKey.PadRight(8,' ').Substring(0,8);
                s = "T" +
                    clientCode.PadRight(6,' ').Substring(0,6) +
                    scanner.PadRight(2,' ').Substring(0,2) +
                    userID.PadRight(3,' ').Substring(0,3) +
                    tripNum.PadRight(8,' ').Substring(0,8) +
                    pickup.TrailerNumber.PadRight(8,' ').Substring(0,8) +
                    pickup.SealNumber.PadRight(8,' ').Substring(0,8);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting trailer export string.",ex); }
            return s;
        }
        private string formatSortedItemRecord(string clientCode,SortedItemDS.SortedItemTableRow sortedItem) {
            //Returns a string representation of the sorted item
            #region Sorted Item record
            //S29127133277               020021220094806
            //S000744232835              020040809084200
            //S1850200205252             020040809102100
            //S060440839041028135854050  020041102084439
            //S0000420018049386522806125 020061031082012
            //-==========================-========------
            //||                         ||       |     
            //ab                         cd       e     

            //a) Barcode Origin [1]
            //   – S = Scanned; M = Manual (always S)
            //b) Barcode Stream [25]
            //   - sorted item vendor item# in Client file
            //c) Damage Code [1]
            //   - sorted item damage code
            //d) Scan Date [8, YYYYMMDD]
            //   - sorted item sort date
            //e) Scan Time [6, HHMMSS]
            //   - sorted item end time
            #endregion
            string s = "";
            try {
                string barcode = sortedItem.VENDOR_ITEM_NUMBER.Trim();
                string dc = sortedItem.IsDAMAGE_CODENull() ? "0" : sortedItem.DAMAGE_CODE.Trim().PadLeft(2,'0').Substring(1,1);
                s = "S" +
                    barcode.PadRight(25,' ') +
                    dc +
                    sortedItem.SORT_DATE.ToString("yyyyMMdd") +
                    sortedItem.END_TIME.ToString("HHmmss");
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting sorted item export string.",ex); }
            return s;
        }
    }
}
