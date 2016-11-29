using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Terminals {
    //Interface
    [ServiceContract(Namespace="http://Argix.Terminals")]
    public interface IRoadshowService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action="http://Argix.Terminals.RoadshowFault")]
        DataSet GetCustomers();
    }

    [DataContract]
    public class RoadshowFault {
        private Exception _ex = null;
        public RoadshowFault(Exception ex) { this._ex = ex; }
        [DataMember]
        public Exception Exception { get { return this._ex; } set { this._ex = value; } }
    }
}
