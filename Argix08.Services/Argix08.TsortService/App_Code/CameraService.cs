using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class CameraService:ICameraService {
        //Members

        //Interface
        public CameraService() { }
        public DataSet ViewImages() {
            //
            DataSet images = new DataSet();
            try {
                images = new CameraGateway().ViewImages();
            }
            catch (Exception ex) { throw new FaultException<CameraFault>(new CameraFault(ex.Message), "Service Error"); }
            return images;
        }
        public bool SaveImage(CameraImage image) {
            //Create a new issue
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                //using (TransactionScope scope = new TransactionScope()) {
                    //
                    added = new CameraGateway().CreateImage(image.Filename, image.File);

                //    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                //    scope.Complete();
                //}
            }
            catch (Exception ex) { throw new FaultException<CameraFault>(new CameraFault(ex.Message), "Service Error"); }
            return added;
        }
        public CameraImage ReadImage(int id) {
            //Read an exoisting image
            CameraImage image = null;
            try {
                DataSet ds = new CameraGateway().ReadImage(id);
                if (ds.Tables["ImageTable"] != null) {
                    image = new CameraImage();
                    image.ID = int.Parse(ds.Tables["ImageTable"].Rows[0]["ID"].ToString());
                    image.Date = DateTime.Parse(ds.Tables["ImageTable"].Rows[0]["Date"].ToString());
                    image.Filename = ds.Tables["ImageTable"].Rows[0]["Filename"].ToString();
                    image.File = (byte[])ds.Tables["ImageTable"].Rows[0]["File"];
                }
            }
            catch (Exception ex) { throw new FaultException<CameraFault>(new CameraFault(ex.Message), "Service Error"); }
            return image;
        }
    }
}
