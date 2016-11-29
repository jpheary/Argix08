using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Synchronization;
using Microsoft.Synchronization.Data;
using System.Data;
using System.Collections.ObjectModel;

namespace Argix.Sync {
    //
    public class ServerSyncProviderProxy: ServerSyncProvider {
        //Members
        private DriverScanService mServiceProxy;

        //Interface
        public ServerSyncProviderProxy(object serviceProxy) {
            this.mServiceProxy = (DriverScanService)serviceProxy;
        }
        public ServerSyncProviderProxy() {  }
        public override SyncContext ApplyChanges(SyncGroupMetadata groupMetadata,DataSet dataSet,SyncSession syncSession) {
            return this.mServiceProxy.ApplyChanges(groupMetadata,dataSet,syncSession);
        }
        public override void Dispose() { }
        public override SyncContext GetChanges(SyncGroupMetadata groupMetadata,SyncSession syncSession) {
            return this.mServiceProxy.GetChanges(groupMetadata,syncSession);
        }
        public override SyncSchema GetSchema(Collection<string> tableNames,SyncSession syncSession) {
            string[] strTableNames = new string[tableNames.Count];
            tableNames.CopyTo(strTableNames,0);
            return this.mServiceProxy.GetSchema(strTableNames,syncSession);
        }
        public override SyncServerInfo GetServerInfo(SyncSession syncSession) {
            return this.mServiceProxy.GetServerInfo(syncSession);
        }
    }
}

