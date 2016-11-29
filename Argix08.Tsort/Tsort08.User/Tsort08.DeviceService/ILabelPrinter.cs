using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO.Ports;

namespace Tsort.Devices.Printers {
    //
    [ServiceContract(Namespace="http://Tsort.Devices.Printers")]
    public interface ILabelPrinter {
        [OperationContract]
        DateTime BornOn();
        
        [OperationContract] 
        string GetLabelPrinterType();
        
        [OperationContract]
        [FaultContractAttribute(typeof(LabelPrinterFault),Action="http://Tsort.Devices.Printers/LabelPrinterFault")]
        PortSettings GetSettings();
        
        [OperationContract] 
        bool IsOn();
        
        [OperationContract]
        [FaultContractAttribute(typeof(LabelPrinterFault),Action="http://Tsort.Devices.Printers/LabelPrinterFault")]
        void TurnOn();
        
        [OperationContract]
        [FaultContractAttribute(typeof(LabelPrinterFault),Action="http://Tsort.Devices.Printers/LabelPrinterFault")]
        void TurnOff();
        
        [OperationContract]
        [FaultContractAttribute(typeof(LabelPrinterFault),Action="http://Tsort.Devices.Printers/LabelPrinterFault")]
        string Print(string labelFormat);

    }
    
    [DataContract]
    public class LabelPrinterFault  {
        private Exception _ex;
        public LabelPrinterFault(Exception ex) { this._ex = ex; }

        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
