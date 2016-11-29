using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

namespace Argix.Terminals {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class RoadshowService:IRoadshowService {
        //Members

        //Interface
        public RoadshowService() { }

        public DataSet GetCustomers() {
            //
            DataSet customers = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetCustomers();
                if (ds != null) customers.Merge(ds);
            }
            catch(Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(new ApplicationException("Unexpected error while reading customers.",ex))); }
            return customers;
        }
    }
}
