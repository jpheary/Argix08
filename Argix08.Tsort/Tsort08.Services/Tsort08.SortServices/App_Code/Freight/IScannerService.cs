using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //
    [ServiceContract]
    public interface IScannerService {
        //Interface
        //[OperationContract(IsOneWay=true)]
        [OperationContract]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        ScannerAssignment GetScannerAssignment(string scannerNumber);
        [OperationContract]
        bool CreateSortedItem(ScannedItem scannedItem);
        [OperationContract]
        bool DeleteSortedItem(ScannedItem scannedItem);
    }

    [DataContract]
    public class ScannerAssignment {
        //Members
        private string mScannerID="";
        private string mScannerNumber="";
		private string mFreightID="";
        private int mTerminalID=0;
        private string mClientNumber="";
		private string mClientDivisionNumber="";
		private int mCartons=0;
        private int mSortTypeID=0;

        [DataMember]
        public string ScannerID { get { return this.mScannerID; } set { this.mScannerID = value; } }
        [DataMember]
        public string ScannerNumber { get { return this.mScannerNumber; } set { this.mScannerNumber = value; } }
        [DataMember]
        public string FreightID { get { return this.mFreightID; } set { this.mFreightID = value; } }
        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        [DataMember]
        public string ClientDivisionNumber { get { return this.mClientDivisionNumber; } set { this.mClientDivisionNumber = value; } }
        [DataMember]
        public int Cartons { get { return this.mCartons; } set { this.mCartons = value; } }
        [DataMember]
        public int SortTypeID { get { return this.mSortTypeID; } set { this.mSortTypeID = value; } }
    }

    [DataContract]
    //[KnownType(typeof(ScannerAssignment))]
    public class ScannedItem {
        //Members
        private string mItemNumber="";
        private int mTerminalID=0;
        private string mScannerID="";
        private string mFreightID="";       //Or pass ScannerAssignment
        //private ScannerAssignment mScannerAssignment=null;
        private DateTime mSortDate;
        private string mScanString="";
        private string mUserID="";

        [DataMember]
        public string ItemNumber { get { return this.mItemNumber; } set { this.mItemNumber = value; } }
        [DataMember]
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        [DataMember]
        public string ScannerID { get { return this.mScannerID; } set { this.mScannerID = value; } }
        [DataMember]
        public string FreightID { get { return this.mFreightID; } set { this.mFreightID = value; } }
        //[DataMember]
        //public ScannerAssignment Assignment { get { return this.mAssignment; } set { this.mAssignment = value; } }
        [DataMember]
        public DateTime SortDate { get { return this.mSortDate; } set { this.mSortDate = value; } }
        [DataMember]
        public string ScanString { get { return this.mScanString; } set { this.mScanString = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
    }
}