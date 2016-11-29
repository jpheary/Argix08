using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Synchronization.Data;
using Microsoft.Synchronization.Data.Server;

public class DriverScanService:IDriverScanService {
    // Instance the configured sync provider in the common dll used by WCF, Web Services and 2 tier configuration
    private Argix.TsortCacheServerSyncProvider mSyncProvider=null;

    //Interface
    public DriverScanService() {
        this.mSyncProvider = new Argix.TsortCacheServerSyncProvider();
    }
    public SyncContext ApplyChanges(SyncGroupMetadata groupMetaData,System.Data.DataSet changeSet,SyncSession syncSession) {
        return this.mSyncProvider.ApplyChanges(groupMetaData,changeSet,syncSession);
    }
    public SyncContext GetChanges(SyncGroupMetadata groupMetaData,SyncSession syncSession) {
        return this.mSyncProvider.GetChanges(groupMetaData,syncSession);
    }
    public SyncSchema GetSchema(System.Collections.ObjectModel.Collection<string> tableNames,SyncSession syncSession) {
        return this.mSyncProvider.GetSchema(tableNames,syncSession);
    }
    public SyncServerInfo GetServerInfo(SyncSession syncSession) {
        return this.mSyncProvider.GetServerInfo(syncSession);
    }
}
