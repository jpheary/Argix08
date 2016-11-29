using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface IFreightService {
        [OperationContract]
        DeliveryDS GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to);
        
        [OperationContract]
        DeliveryDS GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID);
        
        [OperationContract]
        ScanDS GetOSDScans(long cProID);
        
        [OperationContract]
        ScanDS GetPODScans(long cProID);
    }
}
