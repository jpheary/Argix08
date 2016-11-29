using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //Enterprise Interfaces
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface ICameraService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Freight.CameraFault), Action = "http://Argix.Freight.CameraFault")]
        DataSet ViewImages();

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Freight.CameraFault), Action = "http://Argix.Freight.CameraFault")]
        bool SaveImage(CameraImage image);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Freight.CameraFault), Action = "http://Argix.Freight.CameraFault")]
        CameraImage ReadImage(int id);
    }

    [DataContract]
    public class CameraImage {
        //Members
        private int _id = 0;
        private DateTime _date;
        private string _filename = "";
        private byte[] _file = null;

        //Interface
        public CameraImage() {
            //Constructor
            this._id = 0;
            this._date = DateTime.MinValue;
            this._filename = "";
            this._file = null;
        }
        #region Members
        [DataMember]
        public int ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public DateTime Date { get { return this._date; } set { this._date = value; } }
        [DataMember]
        public string Filename { get { return this._filename; } set { this._filename = value; } }
        [DataMember]
        public byte[] File { get { return this._file; } set { this._file = value; } }
        #endregion
    }

    [DataContract]
    public class CameraFault {
        private string mMessage = "";
        public CameraFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}