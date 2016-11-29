﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4961
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
    [System.Runtime.Serialization.DataContractAttribute(Name="TerminalInfo", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance")]
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
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ClassCodes", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance", ItemName="ClassCode")]
    [System.SerializableAttribute()]
    public class ClassCodes : System.Collections.Generic.List<Argix.Finance.ClassCode> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ClassCode", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance")]
    [System.SerializableAttribute()]
    public partial class ClassCode : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClassField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
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
        public string Class {
            get {
                return this.ClassField;
            }
            set {
                if ((object.ReferenceEquals(this.ClassField, value) != true)) {
                    this.ClassField = value;
                    this.RaisePropertyChanged("Class");
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
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="Rates", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance", ItemName="Rate")]
    [System.SerializableAttribute()]
    public class Rates : System.Collections.Generic.List<Argix.Finance.Rate> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Rate", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance")]
    [System.SerializableAttribute()]
    public partial class Rate : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DestZipField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MinChargeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string OrgZipField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Rate1Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Rate10001Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Rate1001Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Rate20001Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Rate2001Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Rate5001Field;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string Rate501Field;
        
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
        public string DestZip {
            get {
                return this.DestZipField;
            }
            set {
                if ((object.ReferenceEquals(this.DestZipField, value) != true)) {
                    this.DestZipField = value;
                    this.RaisePropertyChanged("DestZip");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MinCharge {
            get {
                return this.MinChargeField;
            }
            set {
                if ((object.ReferenceEquals(this.MinChargeField, value) != true)) {
                    this.MinChargeField = value;
                    this.RaisePropertyChanged("MinCharge");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string OrgZip {
            get {
                return this.OrgZipField;
            }
            set {
                if ((object.ReferenceEquals(this.OrgZipField, value) != true)) {
                    this.OrgZipField = value;
                    this.RaisePropertyChanged("OrgZip");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Rate1 {
            get {
                return this.Rate1Field;
            }
            set {
                if ((object.ReferenceEquals(this.Rate1Field, value) != true)) {
                    this.Rate1Field = value;
                    this.RaisePropertyChanged("Rate1");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Rate10001 {
            get {
                return this.Rate10001Field;
            }
            set {
                if ((object.ReferenceEquals(this.Rate10001Field, value) != true)) {
                    this.Rate10001Field = value;
                    this.RaisePropertyChanged("Rate10001");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Rate1001 {
            get {
                return this.Rate1001Field;
            }
            set {
                if ((object.ReferenceEquals(this.Rate1001Field, value) != true)) {
                    this.Rate1001Field = value;
                    this.RaisePropertyChanged("Rate1001");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Rate20001 {
            get {
                return this.Rate20001Field;
            }
            set {
                if ((object.ReferenceEquals(this.Rate20001Field, value) != true)) {
                    this.Rate20001Field = value;
                    this.RaisePropertyChanged("Rate20001");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Rate2001 {
            get {
                return this.Rate2001Field;
            }
            set {
                if ((object.ReferenceEquals(this.Rate2001Field, value) != true)) {
                    this.Rate2001Field = value;
                    this.RaisePropertyChanged("Rate2001");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Rate5001 {
            get {
                return this.Rate5001Field;
            }
            set {
                if ((object.ReferenceEquals(this.Rate5001Field, value) != true)) {
                    this.Rate5001Field = value;
                    this.RaisePropertyChanged("Rate5001");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Rate501 {
            get {
                return this.Rate501Field;
            }
            set {
                if ((object.ReferenceEquals(this.Rate501Field, value) != true)) {
                    this.Rate501Field = value;
                    this.RaisePropertyChanged("Rate501");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RateWareFault", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance")]
    [System.SerializableAttribute()]
    public partial class RateWareFault : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://Argix.Finance", ConfigurationName="Finance.IRateWareService")]
    public interface IRateWareService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IRateWareService/GetUserConfiguration", ReplyAction="http://Argix.Finance/IRateWareService/GetUserConfigurationResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Finance.ConfigurationFault), Action="http://Argix.ConfigurationFault", Name="ConfigurationFault", Namespace="http://schemas.datacontract.org/2004/07/Argix")]
        Argix.Finance.UserConfiguration GetUserConfiguration(string application, string[] usernames);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://Argix.Finance/IRateWareService/WriteLogEntry")]
        void WriteLogEntry(Argix.Finance.TraceMessage m);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IRateWareService/GetTerminalInfo", ReplyAction="http://Argix.Finance/IRateWareService/GetTerminalInfoResponse")]
        Argix.Finance.TerminalInfo GetTerminalInfo();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IRateWareService/GetClassCodes", ReplyAction="http://Argix.Finance/IRateWareService/GetClassCodesResponse")]
        Argix.Finance.ClassCodes GetClassCodes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IRateWareService/GetTariffs", ReplyAction="http://Argix.Finance/IRateWareService/GetTariffsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Finance.RateWareFault), Action="http://Argix.Finance.RateWareFault", Name="RateWareFault", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance")]
        string[] GetTariffs();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://Argix.Finance/IRateWareService/CalculateRates", ReplyAction="http://Argix.Finance/IRateWareService/CalculateRatesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(Argix.Finance.RateWareFault), Action="http://Argix.Finance.RateWareFault", Name="RateWareFault", Namespace="http://schemas.datacontract.org/2004/07/Argix.Finance")]
        Argix.Finance.Rates CalculateRates(string tariff, string originZip, string classCode, double discount, int floorMin, string[] destinationZips);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public interface IRateWareServiceChannel : Argix.Finance.IRateWareService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "3.0.0.0")]
    public partial class RateWareServiceClient : System.ServiceModel.ClientBase<Argix.Finance.IRateWareService>, Argix.Finance.IRateWareService {
        
        public RateWareServiceClient() {
        }
        
        public RateWareServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RateWareServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RateWareServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RateWareServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
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
        
        public Argix.Finance.ClassCodes GetClassCodes() {
            return base.Channel.GetClassCodes();
        }
        
        public string[] GetTariffs() {
            return base.Channel.GetTariffs();
        }
        
        public Argix.Finance.Rates CalculateRates(string tariff, string originZip, string classCode, double discount, int floorMin, string[] destinationZips) {
            return base.Channel.CalculateRates(tariff, originZip, classCode, discount, floorMin, destinationZips);
        }
    }
}