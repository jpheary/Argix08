﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3615
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Argix.Finance {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="UserConfiguration", Namespace="http://schemas.datacontract.org/2004/07/Argix", ItemName="KeyValueOfstringstring", KeyName="Key", ValueName="Value")]
    [System.SerializableAttribute()]
    public class UserConfiguration : System.Collections.Generic.Dictionary<string, string> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TraceMessage", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    [System.SerializableAttribute()]
    public partial class TraceMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ComputerField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string EventField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Keyword1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Keyword2Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Keyword3Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Argix.Finance.LogLevel LogLevelField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SourceField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Category {
            get {
                return this.CategoryField;
            }
            set {
                if ((object.ReferenceEquals(this.CategoryField, value) != true)) {
                    this.CategoryField = value;
                    this.RaisePropertyChanged("Category");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Computer {
            get {
                return this.ComputerField;
            }
            set {
                if ((object.ReferenceEquals(this.ComputerField, value) != true)) {
                    this.ComputerField = value;
                    this.RaisePropertyChanged("Computer");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime Date {
            get {
                return this.DateField;
            }
            set {
                if ((this.DateField.Equals(value) != true)) {
                    this.DateField = value;
                    this.RaisePropertyChanged("Date");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Event {
            get {
                return this.EventField;
            }
            set {
                if ((object.ReferenceEquals(this.EventField, value) != true)) {
                    this.EventField = value;
                    this.RaisePropertyChanged("Event");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keyword1 {
            get {
                return this.Keyword1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Keyword1Field, value) != true)) {
                    this.Keyword1Field = value;
                    this.RaisePropertyChanged("Keyword1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keyword2 {
            get {
                return this.Keyword2Field;
            }
            set {
                if ((object.ReferenceEquals(this.Keyword2Field, value) != true)) {
                    this.Keyword2Field = value;
                    this.RaisePropertyChanged("Keyword2");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Keyword3 {
            get {
                return this.Keyword3Field;
            }
            set {
                if ((object.ReferenceEquals(this.Keyword3Field, value) != true)) {
                    this.Keyword3Field = value;
                    this.RaisePropertyChanged("Keyword3");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Argix.Finance.LogLevel LogLevel {
            get {
                return this.LogLevelField;
            }
            set {
                if ((this.LogLevelField.Equals(value) != true)) {
                    this.LogLevelField = value;
                    this.RaisePropertyChanged("LogLevel");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Source {
            get {
                return this.SourceField;
            }
            set {
                if ((object.ReferenceEquals(this.SourceField, value) != true)) {
                    this.SourceField = value;
                    this.RaisePropertyChanged("Source");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string User {
            get {
                return this.UserField;
            }
            set {
                if ((object.ReferenceEquals(this.UserField, value) != true)) {
                    this.UserField = value;
                    this.RaisePropertyChanged("User");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LogLevel", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    public enum LogLevel : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        None = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Debug = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Information = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Warning = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 4,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TerminalInfo", Namespace="http://schemas.datacontract.org/2004/07/Argix.Enterprise")]
    [System.SerializableAttribute()]
    public partial class TerminalInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ConnectionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TerminalIDField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Connection {
            get {
                return this.ConnectionField;
            }
            set {
                if ((object.ReferenceEquals(this.ConnectionField, value) != true)) {
                    this.ConnectionField = value;
                    this.RaisePropertyChanged("Connection");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Number {
            get {
                return this.NumberField;
            }
            set {
                if ((object.ReferenceEquals(this.NumberField, value) != true)) {
                    this.NumberField = value;
                    this.RaisePropertyChanged("Number");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TerminalID {
            get {
                return this.TerminalIDField;
            }
            set {
                if ((this.TerminalIDField.Equals(value) != true)) {
                    this.TerminalIDField = value;
                    this.RaisePropertyChanged("TerminalID");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="Clients", Namespace="http://schemas.datacontract.org/2004/07/Argix.Enterprise", ItemName="Client")]
    [System.SerializableAttribute()]
    public class Clients : System.Collections.Generic.List<Argix.Finance.Client> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Client", Namespace="http://schemas.datacontract.org/2004/07/Argix.Enterprise")]
    [System.SerializableAttribute()]
    public partial class Client : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ARNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DivisionNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string TerminalCodeField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ARNumber {
            get {
                return this.ARNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ARNumberField, value) != true)) {
                    this.ARNumberField = value;
                    this.RaisePropertyChanged("ARNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClientName {
            get {
                return this.ClientNameField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientNameField, value) != true)) {
                    this.ClientNameField = value;
                    this.RaisePropertyChanged("ClientName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClientNumber {
            get {
                return this.ClientNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientNumberField, value) != true)) {
                    this.ClientNumberField = value;
                    this.RaisePropertyChanged("ClientNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DivisionNumber {
            get {
                return this.DivisionNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.DivisionNumberField, value) != true)) {
                    this.DivisionNumberField = value;
                    this.RaisePropertyChanged("DivisionNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Status {
            get {
                return this.StatusField;
            }
            set {
                if ((object.ReferenceEquals(this.StatusField, value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string TerminalCode {
            get {
                return this.TerminalCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.TerminalCodeField, value) != true)) {
                    this.TerminalCodeField = value;
                    this.RaisePropertyChanged("TerminalCode");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="Invoices", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance", ItemName="Invoice")]
    [System.SerializableAttribute()]
    public class Invoices : System.Collections.Generic.List<Argix.Finance.Invoice> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Invoice", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance")]
    [System.SerializableAttribute()]
    public partial class Invoice : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private decimal AmountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string BillToField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CartonsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime InvoiceDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InvoiceNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InvoiceTypeCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InvoiceTypeDescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InvoiceTypeTargetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int PalletsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime PostToARDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime ReleaseDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int WeightField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public decimal Amount {
            get {
                return this.AmountField;
            }
            set {
                if ((this.AmountField.Equals(value) != true)) {
                    this.AmountField = value;
                    this.RaisePropertyChanged("Amount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string BillTo {
            get {
                return this.BillToField;
            }
            set {
                if ((object.ReferenceEquals(this.BillToField, value) != true)) {
                    this.BillToField = value;
                    this.RaisePropertyChanged("BillTo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Cartons {
            get {
                return this.CartonsField;
            }
            set {
                if ((this.CartonsField.Equals(value) != true)) {
                    this.CartonsField = value;
                    this.RaisePropertyChanged("Cartons");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime InvoiceDate {
            get {
                return this.InvoiceDateField;
            }
            set {
                if ((this.InvoiceDateField.Equals(value) != true)) {
                    this.InvoiceDateField = value;
                    this.RaisePropertyChanged("InvoiceDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InvoiceNumber {
            get {
                return this.InvoiceNumberField;
            }
            set {
                if ((object.ReferenceEquals(this.InvoiceNumberField, value) != true)) {
                    this.InvoiceNumberField = value;
                    this.RaisePropertyChanged("InvoiceNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InvoiceTypeCode {
            get {
                return this.InvoiceTypeCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.InvoiceTypeCodeField, value) != true)) {
                    this.InvoiceTypeCodeField = value;
                    this.RaisePropertyChanged("InvoiceTypeCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InvoiceTypeDescription {
            get {
                return this.InvoiceTypeDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.InvoiceTypeDescriptionField, value) != true)) {
                    this.InvoiceTypeDescriptionField = value;
                    this.RaisePropertyChanged("InvoiceTypeDescription");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string InvoiceTypeTarget {
            get {
                return this.InvoiceTypeTargetField;
            }
            set {
                if ((object.ReferenceEquals(this.InvoiceTypeTargetField, value) != true)) {
                    this.InvoiceTypeTargetField = value;
                    this.RaisePropertyChanged("InvoiceTypeTarget");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Pallets {
            get {
                return this.PalletsField;
            }
            set {
                if ((this.PalletsField.Equals(value) != true)) {
                    this.PalletsField = value;
                    this.RaisePropertyChanged("Pallets");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime PostToARDate {
            get {
                return this.PostToARDateField;
            }
            set {
                if ((this.PostToARDateField.Equals(value) != true)) {
                    this.PostToARDateField = value;
                    this.RaisePropertyChanged("PostToARDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime ReleaseDate {
            get {
                return this.ReleaseDateField;
            }
            set {
                if ((this.ReleaseDateField.Equals(value) != true)) {
                    this.ReleaseDateField = value;
                    this.RaisePropertyChanged("ReleaseDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Weight {
            get {
                return this.WeightField;
            }
            set {
                if ((this.WeightField.Equals(value) != true)) {
                    this.WeightField = value;
                    this.RaisePropertyChanged("Weight");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ConfigurationFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
    [System.SerializableAttribute()]
    public partial class ConfigurationFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Exception ExceptionField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Exception Exception {
            get {
                return this.ExceptionField;
            }
            set {
                if ((object.ReferenceEquals(this.ExceptionField, value) != true)) {
                    this.ExceptionField = value;
                    this.RaisePropertyChanged("Exception");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Argix.Finance", ConfigurationName="Finance.IInvoicingService")]
    public interface IInvoicingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IInvoicingService/GetUserConfiguration", ReplyAction="http://Argix.Finance/IInvoicingService/GetUserConfigurationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Finance.ConfigurationFault), Action="http://Argix.ConfigurationFault", Name="ConfigurationFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        Argix.Finance.UserConfiguration GetUserConfiguration(string application, string[] usernames);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://Argix.Finance/IInvoicingService/WriteLogEntry")]
        void WriteLogEntry(Argix.Finance.TraceMessage m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IInvoicingService/GetTerminalInfo", ReplyAction="http://Argix.Finance/IInvoicingService/GetTerminalInfoResponse")]
        Argix.Finance.TerminalInfo GetTerminalInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IInvoicingService/GetClients", ReplyAction="http://Argix.Finance/IInvoicingService/GetClientsResponse")]
        Argix.Finance.Clients GetClients();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IInvoicingService/GetClientInvoices", ReplyAction="http://Argix.Finance/IInvoicingService/GetClientInvoicesResponse")]
        Argix.Finance.Invoices GetClientInvoices(string clientNumber, string clientDivision, string startDate);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IInvoicingServiceChannel : Argix.Finance.IInvoicingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class InvoicingServiceClient : System.ServiceModel.ClientBase<Argix.Finance.IInvoicingService>, Argix.Finance.IInvoicingService {
        
        public InvoicingServiceClient() {
        }
        
        public InvoicingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public InvoicingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InvoicingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public InvoicingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public Argix.Finance.UserConfiguration GetUserConfiguration(string application, string[] usernames) {
            return base.Channel.GetUserConfiguration(application, usernames);
        }
        
        public void WriteLogEntry(Argix.Finance.TraceMessage m) {
            base.Channel.WriteLogEntry(m);
        }
        
        public Argix.Finance.TerminalInfo GetTerminalInfo() {
            return base.Channel.GetTerminalInfo();
        }
        
        public Argix.Finance.Clients GetClients() {
            return base.Channel.GetClients();
        }
        
        public Argix.Finance.Invoices GetClientInvoices(string clientNumber, string clientDivision, string startDate) {
            return base.Channel.GetClientInvoices(clientNumber, clientDivision, startDate);
        }
    }
}
