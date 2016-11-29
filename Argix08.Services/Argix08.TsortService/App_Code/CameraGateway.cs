using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Freight {
    //
    public class CameraGateway {
        //Members
        public const string SQL_CONNID = "Camera";

        public const string USP_IMAGES_VIEW = "uspDevCameraImagesView", TBL_IMAGES = "ImageTable";
        public const string USP_IMAGE_CREATE = "uspDevCameraImageNew";
        public const string USP_IMAGE_READ = "uspDevCameraImageRead";

        //Interface
        public CameraGateway() { }

        public DataSet ViewImages() {
            //Get an existing issue
            DataSet images = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_IMAGES_VIEW, TBL_IMAGES, new object[] { });
                if (ds != null && ds.Tables[TBL_IMAGES].Rows.Count > 0) images.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return images;
        }
        public bool CreateImage(string name, byte[] bytes) {
            //Create a new issue
            bool saved = false;
            try {
                //Save issue
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_IMAGE_CREATE, new object[] { null, name, bytes });
                int id = (int)o;
                saved = (id > 0);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return saved;
        }
        public DataSet ReadImage(int id) {
            //Get an existing image
            DataSet image = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_IMAGE_READ, TBL_IMAGES, new object[] { id });
                if (ds != null && ds.Tables[TBL_IMAGES].Rows.Count > 0) image.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return image;
        }
    }
}
