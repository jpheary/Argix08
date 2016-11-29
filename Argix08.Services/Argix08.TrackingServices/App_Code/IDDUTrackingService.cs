using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;

namespace Argix {
    // 
    [ServiceContract(Namespace = "http://Argix")]
    public interface IDDUTrackingService {
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.TrackingFault")]
        TrackDS TrackDDUCartons(string[] cartonNumbers,string clientNumber,string vendorNumber);
    }
}
