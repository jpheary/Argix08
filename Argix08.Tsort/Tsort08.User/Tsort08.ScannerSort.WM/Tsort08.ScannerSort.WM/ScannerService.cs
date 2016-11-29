﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4961
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Argix
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    public partial class TraceMessage : object
    {
        
        
        private string CategoryField;
        
        private string ComputerField;
        
        private System.DateTime DateField;
        
        private string EventField;
        
        private long IDField;
        
        private string Keyword1Field;
        
        private string Keyword2Field;
        
        private string Keyword3Field;
        
        private Argix.LogLevel LogLevelField;
        
        private string MessageField;
        
        private string NameField;
        
        private string SourceField;
        
        private string UserField;
        
        
        public string Category
        {
            get
            {
                return this.CategoryField;
            }
            set
            {
                this.CategoryField = value;
            }
        }
        
        public string Computer
        {
            get
            {
                return this.ComputerField;
            }
            set
            {
                this.ComputerField = value;
            }
        }
        
        public System.DateTime Date
        {
            get
            {
                return this.DateField;
            }
            set
            {
                this.DateField = value;
            }
        }
        
        public string Event
        {
            get
            {
                return this.EventField;
            }
            set
            {
                this.EventField = value;
            }
        }
        
        public long ID
        {
            get
            {
                return this.IDField;
            }
            set
            {
                this.IDField = value;
            }
        }
        
        public string Keyword1
        {
            get
            {
                return this.Keyword1Field;
            }
            set
            {
                this.Keyword1Field = value;
            }
        }
        
        public string Keyword2
        {
            get
            {
                return this.Keyword2Field;
            }
            set
            {
                this.Keyword2Field = value;
            }
        }
        
        public string Keyword3
        {
            get
            {
                return this.Keyword3Field;
            }
            set
            {
                this.Keyword3Field = value;
            }
        }
        
        public Argix.LogLevel LogLevel
        {
            get
            {
                return this.LogLevelField;
            }
            set
            {
                this.LogLevelField = value;
            }
        }
        
        public string Message
        {
            get
            {
                return this.MessageField;
            }
            set
            {
                this.MessageField = value;
            }
        }
        
        public string Name
        {
            get
            {
                return this.NameField;
            }
            set
            {
                this.NameField = value;
            }
        }
        
        public string Source
        {
            get
            {
                return this.SourceField;
            }
            set
            {
                this.SourceField = value;
            }
        }
        
        public string User
        {
            get
            {
                return this.UserField;
            }
            set
            {
                this.UserField = value;
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    public enum LogLevel : int
    {
        
        None = 0,
        
        Debug = 1,
        
        Information = 2,
        
        Warning = 3,
        
        Error = 4,
    }
}
namespace Argix.Freight
{
    using System;
    using System.Runtime.Serialization;
    using System.ServiceModel.Channels;
    using System.Xml;
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    public partial class ScannerAssignment : object
    {
        
        
        private int CartonsField;
        
        private string ClientDivisionNumberField;
        
        private string ClientNumberField;
        
        private string FreightIDField;
        
        private string ScannerIDField;
        
        private string ScannerNumberField;
        
        private int SortTypeIDField;
        
        private int TerminalIDField;
                
        public int Cartons
        {
            get
            {
                return this.CartonsField;
            }
            set
            {
                this.CartonsField = value;
            }
        }
        
        public string ClientDivisionNumber
        {
            get
            {
                return this.ClientDivisionNumberField;
            }
            set
            {
                this.ClientDivisionNumberField = value;
            }
        }
        
        public string ClientNumber
        {
            get
            {
                return this.ClientNumberField;
            }
            set
            {
                this.ClientNumberField = value;
            }
        }
        
        public string FreightID
        {
            get
            {
                return this.FreightIDField;
            }
            set
            {
                this.FreightIDField = value;
            }
        }
        
        public string ScannerID
        {
            get
            {
                return this.ScannerIDField;
            }
            set
            {
                this.ScannerIDField = value;
            }
        }
        
        public string ScannerNumber
        {
            get
            {
                return this.ScannerNumberField;
            }
            set
            {
                this.ScannerNumberField = value;
            }
        }
        
        public int SortTypeID
        {
            get
            {
                return this.SortTypeIDField;
            }
            set
            {
                this.SortTypeIDField = value;
            }
        }
        
        public int TerminalID
        {
            get
            {
                return this.TerminalIDField;
            }
            set
            {
                this.TerminalIDField = value;
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "3.0.0.0")]
    public partial class ScannedItem : object
    {
        
        
        private string FreightIDField;
        
        private string ItemNumberField;
        
        private string ScanStringField;
        
        private string ScannerIDField;
        
        private System.DateTime SortDateField;
        
        private int TerminalIDField;
        
        private string UserIDField;
                
        public string FreightID
        {
            get
            {
                return this.FreightIDField;
            }
            set
            {
                this.FreightIDField = value;
            }
        }
        
        public string ItemNumber
        {
            get
            {
                return this.ItemNumberField;
            }
            set
            {
                this.ItemNumberField = value;
            }
        }
        
        public string ScanString
        {
            get
            {
                return this.ScanStringField;
            }
            set
            {
                this.ScanStringField = value;
            }
        }
        
        public string ScannerID
        {
            get
            {
                return this.ScannerIDField;
            }
            set
            {
                this.ScannerIDField = value;
            }
        }
        
        public System.DateTime SortDate
        {
            get
            {
                return this.SortDateField;
            }
            set
            {
                this.SortDateField = value;
            }
        }
        
        public int TerminalID
        {
            get
            {
                return this.TerminalIDField;
            }
            set
            {
                this.TerminalIDField = value;
            }
        }
        
        public string UserID
        {
            get
            {
                return this.UserIDField;
            }
            set
            {
                this.UserIDField = value;
            }
        }
    }


    public interface IScannerService
    {
        
        void WriteLogEntry(Argix.TraceMessage m);
        
        Argix.Freight.ScannerAssignment GetScannerAssignment(string scannerNumber);
        
        bool CreateSortedItem(Argix.Freight.ScannedItem scannedItem);
        
        bool DeleteSortedItem(Argix.Freight.ScannedItem scannedItem);
    }


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    public partial class ScannerServiceClient : Argix.Freight.ClientBase<IScannerService>, IScannerService
    {

        public static System.ServiceModel.EndpointAddress ServiceEndPoint = new System.ServiceModel.EndpointAddress("http://tpjheary7/SortServices/ScannerService.svc");

        public ScannerServiceClient(System.ServiceModel.Channels.Binding binding,System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public void WriteLogEntry(Argix.TraceMessage m)
        {
            base.Call("WriteLogEntry","http://tpjheary7/SortServices/IScannerService/WriteLogEntry",new string[] { "m" },new object[] { m },null);
        }
        
        public Argix.Freight.ScannerAssignment GetScannerAssignment(string scannerNumber)
        {
            return (ScannerAssignment)base.Call("GetScannerAssignment","http://tpjheary7/SortServices/IScannerService/GetScannerAssignment",new string[] { "scannerNumber" },new object[] { scannerNumber },typeof(ScannerAssignment));
        }
        
        public bool CreateSortedItem(Argix.Freight.ScannedItem scannedItem)
        {
            return (bool)base.Call("CreateSortedItem","http://tpjheary7/SortServices/IScannerService/CreateSortedItem",new string[] { "scannedItem" },new object[] { scannedItem },typeof(bool));
        }
        
        public bool DeleteSortedItem(Argix.Freight.ScannedItem scannedItem)
        {
            return (bool)base.Call("DeleteSortedItem","http://tpjheary7/SortService/IScannerService/DeleteSortedItem",new string[] { "scannedItem" },new object[] { scannedItem },typeof(bool));
        }
    }


    public class ClientBase<TChannel>
        where TChannel:class {
        private IRequestChannel requestChannel;
        private MessageVersion messageVersion;

        public ClientBase(System.ServiceModel.Channels.Binding binding,System.ServiceModel.EndpointAddress remoteAddress) {
            //this.remoteAddress = remoteAddress;
            this.messageVersion = binding.MessageVersion;

            IChannelFactory<IRequestChannel> channelFactory = binding.BuildChannelFactory<IRequestChannel>(
                new BindingParameterCollection());
            channelFactory.Open();
            this.requestChannel = channelFactory.CreateChannel(remoteAddress);
        }

        public object Call(string op,string action,string[] varnames,object[] varvals,Type returntype) {
            requestChannel.Open(TimeSpan.MaxValue);

            //Message msg = 					
            //Message.CreateMessage(MessageVersion.<FromBinding>,
            //      action,
            //      new CustomBodyWriter(op, varnames, varvals, 				
            //"<ns passed in from Proxy>"));

            Message msg = 					
            Message.CreateMessage(this.messageVersion,action, new CustomBodyWriter(op,varnames,varvals, "<ns passed in from Proxy>"));

            Message reply = requestChannel.Request(msg,TimeSpan.MaxValue);
            System.Xml.XmlDictionaryReader reader = reply.GetReaderAtBodyContents();
            reader.ReadToFollowing(op + "Result");
            return reader.ReadElementContentAs(returntype,null);
        }

    }

    internal class CustomBodyWriter:BodyWriter {
        private string op;
        private string[] varnames;
        private object[] varvals;
        private string ns;

        public CustomBodyWriter(string op,string[] varnames,object[] varvals,string ns)
            : base(true) {
            this.op = op;
            this.varnames = varnames;
            this.varvals = varvals;
            this.ns = ns;
        }

        protected override void OnWriteBodyContents(XmlDictionaryWriter writer) {
            writer.WriteStartElement(op,ns);
            for(int i = 0;i < varnames.Length;i++)
                writer.WriteElementString(varnames[i],varvals[i].ToString());
            writer.WriteEndElement();
        }
    }

}
