using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

namespace Argix {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TrackingService: ITrackingService {
        //Members
        private const string USP_BYCARTON = "uspTrackingGetListForCartons2",TBL_BYCARTON = "TrackingTable";

        private const int SCANTYPE_NONE = 0, SCANTYPE_SORTED = 1, SCANTYPE_OSD = 2, SCANTYPE_POD = 3;
        private const string SQL_CONN = "Tracking";
      
        //Interface
        public TrackingService() { }
        public TrackingItems TrackCartons(string[] itemNumbers,string companyID) {
            //Get tracking information for the specified items and company
            TrackingItems items = null;

            //Get tracking response for the specified requests
            items = new TrackingItems();
            string numbers = "";
            for(int i = 0;i < itemNumbers.Length;i++) { if(i > 0) numbers += ","; numbers += itemNumbers[i]; }

            //Get tracking data response
            DataSet ds = new DataService().FillDataset(SQL_CONN, USP_BYCARTON,TBL_BYCARTON,new object[] { numbers,companyID,null });
            if(ds != null && ds.Tables[TBL_BYCARTON].Rows.Count > 0) {
                TrackingDS cartons = new TrackingDS();
                DataView view = ds.Tables[TBL_BYCARTON].DefaultView;
                view.Sort = "CTN,BL DESC,SCNTP DESC,SCD DESC,SCT DESC";
                DataTable dt = view.Table.Clone();
                Hashtable ht = new Hashtable();
                for(int i = 0;i < view.Count;i++) {
                    string key = view[i]["CTN"].ToString().Trim() + view[i]["BL"].ToString().Trim();
                    if(!ht.ContainsKey(key)) {
                        ht.Add(key,null);
                        dt.ImportRow(view[i].Row);
                    }
                }
                cartons.TrackingTable.Merge(dt);
                
                //Return records for all found items
                foreach(TrackingDS.TrackingTableRow carton in cartons.TrackingTable.Rows) {
                    //Keep the response item if it matches a requested item
                    for(int i = 0;i < itemNumbers.Length;i++) {
                        if(carton.CTN.Trim() == itemNumbers[i]) {
                            //Keep it
                            TrackingItem item = new TrackingItem(itemNumbers[i],carton);
                            items.Add(item);
                            itemNumbers[i] = "";    //Mark as found by nulling it
                            break;
                        }
                    }
                }
                
                //Return a record for all unfound requests
                for(int i = 0;i < itemNumbers.Length;i++) {
                    if(itemNumbers[i].Length > 0) items.Add(new TrackingItem(itemNumbers[i]));
                }
            }
            return items;
        }
    }
}
