using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.IO;
//using System.Reflection;
//using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.Services;

namespace Argix.Freight {
	//
	public class FreightGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        
        //Interface
        static FreightGateway() { 
            //
            CameraService client = new CameraService();
            _state = true;
            _address = "";  // client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private FreightGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static bool SaveImage(string filename, byte[] bytes) {
            //
            bool res = false, resSpecified = false ;
            CameraService client = new CameraService();
            //client.Credentials = System.Net.CredentialCache.DefaultCredentials;
            try {
                //Create DTO and call service
                CameraImage image = new CameraImage();
                image.Date = DateTime.Now;
                image.Filename = filename;
                image.File = bytes;

                client.SaveImage(image, out res, out resSpecified);
                client.Dispose();   //Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            //catch(FaultException<CameraFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            //catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            //catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return res;
        }
    }
}