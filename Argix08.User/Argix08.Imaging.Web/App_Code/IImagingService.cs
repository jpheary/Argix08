using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //Freight Interfaces
    [ServiceContract(Namespace = "http://Argix.Freight")]
    public interface IImagingService {
        //General Interface
        [OperationContract]
        string GetPortalSearchInfo();
        
        [OperationContract]
        string GetSearchMetadata();
        
        [OperationContract]
        DocumentClasses GetDocumentClasses();
        
        [OperationContract(Name="GetDocumentClassesByDepartment")]
        DocumentClasses GetDocumentClasses(string department);
        
        [OperationContract]
        MetaDatas GetMetaData();
        
        [OperationContract(Name="GetMetaDataByClassName")]
        MetaDatas GetMetaData(string className);
        
        //[OperationContract]
        //public void GetSharePointImage(string docClass,string propertyName,string searchText);
        
        [OperationContract]
        byte[] GetSharePointImageStream(SearchDS search);
        
        [OperationContract]
        DataSet SearchSharePointImageStore(SearchDS search);
    }

    [CollectionDataContract]
    public class DocumentClasses:BindingList<DocumentClass> {
        public DocumentClasses() { }
    }

    [DataContract]
    public class DocumentClass {
        //Members
        private string mDepartment = "", mClassName = "";

        //Interface
        public DocumentClass() { }
        public DocumentClass(string department,string classname) { this.mDepartment = department; this.mClassName = classname; }

        [DataMember]
        public string Department { get { return this.mDepartment; } set { this.mDepartment = value; } }
        [DataMember]
        public string ClassName { get { return this.mClassName; } set { this.mClassName = value; } }
    }

    [CollectionDataContract]
    public class MetaDatas:BindingList<MetaData> {
        public MetaDatas() { }
    }

    [DataContract]
    public class MetaData {
        //Members
        private string mProperty = "", mClassName = "";

        //Interface
        public MetaData() { }
        public MetaData(string classname,string property) { this.mClassName = classname; this.mProperty = property; }

        [DataMember]
        public string ClassName { get { return this.mClassName; } set { this.mClassName = value; } }
        [DataMember]
        public string Property { get { return this.mProperty; } set { this.mProperty = value; } }
    }
}
