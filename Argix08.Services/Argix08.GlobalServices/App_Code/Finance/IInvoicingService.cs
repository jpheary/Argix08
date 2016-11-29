using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Finance {
    //Finance Interfaces
    [ServiceContract(Namespace="http://Argix.Finance")]
    public interface IInvoicingService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);
        
        [OperationContract]
        Argix.Enterprise.TerminalInfo GetTerminalInfo();

        [OperationContract]
        Argix.Enterprise.Clients GetClients();
        [OperationContract]
        Invoices GetClientInvoices(string clientNumber,string clientDivision,string startDate);
    }

    [CollectionDataContract]
    public class Invoices:BindingList<Invoice> {
        public Invoices() { }
    }

    [DataContract]
    public class Invoice {
        //Members
        private string mInvoiceNumber = "";
        private DateTime mInvoiceDate;
        private DateTime mPostToARDate;
        private int mCartons = 0;
        private int mPallets = 0;
        private int mWeight = 0;
        private decimal mAmount = 0.0M;
        private string mDescription = "";
        private DateTime mReleaseDate;
        private string mInvoiceTypeCode = "";
        private string mInvoiceTypeDescription = "";
        private string mInvoiceTypeTarget = "";
        private string mBillTo = "";

        //Interface
        public Invoice() : this(null) { }
        public Invoice(InvoiceDS.ClientInvoiceTableRow invoice) {
            //Constructor
            try {
                if(invoice != null) {
                    if(!invoice.IsInvoiceNumberNull()) this.mInvoiceNumber = invoice.InvoiceNumber;
                    if(!invoice.IsInvoiceDateNull()) this.mInvoiceDate = invoice.InvoiceDate;
                    if(!invoice.IsPostToARDateNull()) this.mPostToARDate = invoice.PostToARDate;
                    if(!invoice.IsCartonsNull()) this.mCartons = invoice.Cartons;
                    if(!invoice.IsPalletsNull()) this.mPallets = invoice.Pallets;
                    if(!invoice.IsWeightNull()) this.mWeight = invoice.Weight;
                    if(!invoice.IsAmountNull()) this.mAmount = invoice.Amount;
                    if(!invoice.IsDescriptionNull()) this.mDescription = invoice.Description;
                    if(!invoice.IsReleaseDateNull()) this.mReleaseDate = invoice.ReleaseDate;
                    if(!invoice.IsInvoiceTypeCodeNull()) this.mInvoiceTypeCode = invoice.InvoiceTypeCode;
                    if(!invoice.IsInvoiceTypeDescriptionNull()) this.mInvoiceTypeDescription = invoice.InvoiceTypeDescription;
                    if(!invoice.IsInvoiceTypeTargetNull()) this.mInvoiceTypeTarget = invoice.InvoiceTypeTarget;
                    if(!invoice.IsBillToNull()) this.mBillTo = invoice.BillTo;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Client instance",ex); }
        }

        [DataMember]
        public string InvoiceNumber { get { return this.mInvoiceNumber; } set { this.mInvoiceNumber = value; } }
        [DataMember]
        public DateTime InvoiceDate { get { return this.mInvoiceDate; } set { this.mInvoiceDate = value; } }
        [DataMember]
        public DateTime PostToARDate { get { return this.mPostToARDate; } set { this.mPostToARDate = value; } }
        [DataMember]
        public int Cartons { get { return this.mCartons; } set { this.mCartons = value; } }
        [DataMember]
        public int Pallets { get { return this.mPallets; } set { this.mPallets = value; } }
        [DataMember]
        public int Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public decimal Amount { get { return this.mAmount; } set { this.mAmount = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public DateTime ReleaseDate { get { return this.mReleaseDate; } set { this.mReleaseDate = value; } }
        [DataMember]
        public string InvoiceTypeCode { get { return this.mInvoiceTypeCode; } set { this.mInvoiceTypeCode = value; } }
        [DataMember]
        public string InvoiceTypeDescription { get { return this.mInvoiceTypeDescription; } set { this.mInvoiceTypeDescription = value; } }
        [DataMember]
        public string InvoiceTypeTarget { get { return this.mInvoiceTypeTarget; } set { this.mInvoiceTypeTarget = value; } }
        [DataMember]
        public string BillTo { get { return this.mBillTo; } set { this.mBillTo = value; } }
    }
}
