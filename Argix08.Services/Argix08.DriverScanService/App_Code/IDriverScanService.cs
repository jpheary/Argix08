using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.Server;

[ServiceContract,XmlSerializerFormat]
public interface IDriverScanService {

    [OperationContract]
    SyncContext ApplyChanges(SyncGroupMetadata groupMetaData,System.Data.DataSet changeSet,SyncSession syncSession);

    [OperationContract]
    SyncContext GetChanges(SyncGroupMetadata groupMetaData,SyncSession syncSession);

    [OperationContract]
    SyncSchema GetSchema(System.Collections.ObjectModel.Collection<string> tableNames,SyncSession syncSession);

    [OperationContract]
    SyncServerInfo GetServerInfo(SyncSession syncSession);
}
