using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO.Ports;

namespace Tsort.Devices.Scales {
    //
    [ServiceContract(Namespace="http://Tsort.Devices.Scales",CallbackContract=typeof(IScaleEvents))]
    public interface IScale {
        [OperationContract]
        DateTime BornOn();
        
        [OperationContract] 
        string GetScaleType();
        
        [OperationContract]
        [FaultContractAttribute(typeof(ScaleFault),Action="http://Tsort.Devices.Scales/ScaleFault")]
        PortSettings GetSettings();
        
        [OperationContract] 
        bool IsOn();
        
        [OperationContract]
        [FaultContractAttribute(typeof(ScaleFault),Action="http://Tsort.Devices.Scales/ScaleFault")]
        void TurnOn();
        
        [OperationContract]
        [FaultContractAttribute(typeof(ScaleFault),Action="http://Tsort.Devices.Scales/ScaleFault")]
        void TurnOff();
        
        [OperationContract]
        [FaultContractAttribute(typeof(ScaleFault),Action="http://Tsort.Devices.Scales/ScaleFault")]
        decimal GetWeight(ref bool isStable);
        
        [OperationContract]
        [FaultContractAttribute(typeof(ScaleFault),Action="http://Tsort.Devices.Scales/ScaleFault")]
        void Zero();
    }
    
    public interface IScaleEvents {
        [OperationContract(IsOneWay=true)]
        void WeightReading(decimal weight,ScaleError error);
    }

    [DataContract]
    [Flags]
    public enum ScaleError {
        [EnumMember] None=0x0,
        [EnumMember] RS232=0x00000001,
        [EnumMember] ScaleStatus=0x00000002,
        [EnumMember] ScaleUnstable=0x00000004
    }

    [DataContract]
    public class ScaleEventArgs:EventArgs {
        private decimal _weight=0;
        private ScaleError _error;
        public ScaleEventArgs(decimal weight,ScaleError error) { this._weight = weight; this._error = error; }
        [DataMember] public decimal Weight { get { return this._weight; } set { this._weight = value; } }
        [DataMember] public ScaleError Error { get { return this._error; } set { this._error = value; } }
    }
    public delegate void ScaleEventHandler(object source,ScaleEventArgs e);

    [DataContract]
    public class ScaleFault  {
        private Exception _ex;
        public ScaleFault(Exception ex) { this._ex = ex; }

        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
