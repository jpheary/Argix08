﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4961
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Argix {
    
    
    public partial class GPSDataSyncAdapter : Microsoft.Synchronization.Data.Server.SyncAdapter {
        
        partial void OnInitialized();
        
        public GPSDataSyncAdapter() {
            this.InitializeCommands();
            this.InitializeAdapterProperties();
            this.OnInitialized();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitializeCommands() {
            // GPSDataSyncTableInsertCommand command.
            this.InsertCommand = new System.Data.SqlClient.SqlCommand();
            this.InsertCommand.CommandText = @" SET IDENTITY_INSERT dbo.GPSData ON ;WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) INSERT INTO dbo.GPSData ([Longitude], [Latitude], [Altitude], [LocationDateTime], [CreationDate], [ID]) VALUES (@Longitude, @Latitude, @Altitude, @LocationDateTime, @CreationDate, @ID) SET @sync_row_count = @@rowcount; IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(N'dbo.GPSData')) > @sync_last_received_anchor RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. To recover from this error, the client must reinitialize its local database and try again',16,3,N'dbo.GPSData')  SET IDENTITY_INSERT dbo.GPSData OFF ";
            this.InsertCommand.CommandType = System.Data.CommandType.Text;
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_client_id_binary", System.Data.SqlDbType.VarBinary));
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Longitude", System.Data.SqlDbType.Int));
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Latitude", System.Data.SqlDbType.Int));
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Altitude", System.Data.SqlDbType.Int));
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LocationDateTime", System.Data.SqlDbType.DateTime));
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreationDate", System.Data.SqlDbType.DateTime));
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int));
            System.Data.SqlClient.SqlParameter insertcommand_sync_row_countParameter = new System.Data.SqlClient.SqlParameter("@sync_row_count", System.Data.SqlDbType.Int);
            insertcommand_sync_row_countParameter.Direction = System.Data.ParameterDirection.Output;
            this.InsertCommand.Parameters.Add(insertcommand_sync_row_countParameter);
            this.InsertCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_last_received_anchor", System.Data.SqlDbType.BigInt));
            // GPSDataSyncTableDeleteCommand command.
            this.DeleteCommand = new System.Data.SqlClient.SqlCommand();
            this.DeleteCommand.CommandText = @";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) DELETE dbo.GPSData FROM dbo.GPSData JOIN CHANGETABLE(VERSION dbo.GPSData, ([ID]), (@ID)) CT  ON CT.[ID] = dbo.GPSData.[ID] WHERE (@sync_force_write = 1 OR CT.SYS_CHANGE_VERSION IS NULL OR CT.SYS_CHANGE_VERSION <= @sync_last_received_anchor OR (CT.SYS_CHANGE_CONTEXT IS NOT NULL AND CT.SYS_CHANGE_CONTEXT = @sync_client_id_binary)) SET @sync_row_count = @@rowcount; IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(N'dbo.GPSData')) > @sync_last_received_anchor RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. To recover from this error, the client must reinitialize its local database and try again',16,3,N'dbo.GPSData') ";
            this.DeleteCommand.CommandType = System.Data.CommandType.Text;
            this.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_client_id_binary", System.Data.SqlDbType.VarBinary));
            this.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int));
            this.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_force_write", System.Data.SqlDbType.Bit));
            this.DeleteCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_last_received_anchor", System.Data.SqlDbType.BigInt));
            System.Data.SqlClient.SqlParameter deletecommand_sync_row_countParameter = new System.Data.SqlClient.SqlParameter("@sync_row_count", System.Data.SqlDbType.Int);
            deletecommand_sync_row_countParameter.Direction = System.Data.ParameterDirection.Output;
            this.DeleteCommand.Parameters.Add(deletecommand_sync_row_countParameter);
            // GPSDataSyncTableUpdateCommand command.
            this.UpdateCommand = new System.Data.SqlClient.SqlCommand();
            this.UpdateCommand.CommandText = @";WITH CHANGE_TRACKING_CONTEXT (@sync_client_id_binary) UPDATE dbo.GPSData SET [Longitude] = @Longitude, [Latitude] = @Latitude, [Altitude] = @Altitude, [LocationDateTime] = @LocationDateTime, [CreationDate] = @CreationDate FROM dbo.GPSData  JOIN CHANGETABLE(VERSION dbo.GPSData, ([ID]), (@ID)) CT  ON CT.[ID] = dbo.GPSData.[ID] WHERE (@sync_force_write = 1 OR CT.SYS_CHANGE_VERSION IS NULL OR CT.SYS_CHANGE_VERSION <= @sync_last_received_anchor OR (CT.SYS_CHANGE_CONTEXT IS NOT NULL AND CT.SYS_CHANGE_CONTEXT = @sync_client_id_binary)) SET @sync_row_count = @@rowcount; IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(N'dbo.GPSData')) > @sync_last_received_anchor RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. To recover from this error, the client must reinitialize its local database and try again',16,3,N'dbo.GPSData') ";
            this.UpdateCommand.CommandType = System.Data.CommandType.Text;
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Longitude", System.Data.SqlDbType.Int));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Latitude", System.Data.SqlDbType.Int));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@Altitude", System.Data.SqlDbType.Int));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@LocationDateTime", System.Data.SqlDbType.DateTime));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@CreationDate", System.Data.SqlDbType.DateTime));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_force_write", System.Data.SqlDbType.Bit));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_last_received_anchor", System.Data.SqlDbType.BigInt));
            this.UpdateCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_client_id_binary", System.Data.SqlDbType.VarBinary));
            System.Data.SqlClient.SqlParameter updatecommand_sync_row_countParameter = new System.Data.SqlClient.SqlParameter("@sync_row_count", System.Data.SqlDbType.Int);
            updatecommand_sync_row_countParameter.Direction = System.Data.ParameterDirection.Output;
            this.UpdateCommand.Parameters.Add(updatecommand_sync_row_countParameter);
            // GPSDataSyncTableSelectConflictDeletedRowsCommand command.
            this.SelectConflictDeletedRowsCommand = new System.Data.SqlClient.SqlCommand();
            this.SelectConflictDeletedRowsCommand.CommandText = "SELECT CT.[ID], CT.SYS_CHANGE_CONTEXT, CT.SYS_CHANGE_VERSION FROM CHANGETABLE(CHA" +
                "NGES dbo.GPSData, @sync_last_received_anchor) CT WHERE (CT.[ID] = @ID AND CT.SYS" +
                "_CHANGE_OPERATION = \'D\')";
            this.SelectConflictDeletedRowsCommand.CommandType = System.Data.CommandType.Text;
            this.SelectConflictDeletedRowsCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_last_received_anchor", System.Data.SqlDbType.BigInt));
            this.SelectConflictDeletedRowsCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int));
            // GPSDataSyncTableSelectConflictUpdatedRowsCommand command.
            this.SelectConflictUpdatedRowsCommand = new System.Data.SqlClient.SqlCommand();
            this.SelectConflictUpdatedRowsCommand.CommandText = "SELECT [Longitude], [Latitude], [Altitude], [LocationDateTime], [CreationDate], d" +
                "bo.GPSData.[ID], CT.SYS_CHANGE_CONTEXT, CT.SYS_CHANGE_VERSION FROM dbo.GPSData J" +
                "OIN CHANGETABLE(VERSION dbo.GPSData, ([ID]), (@ID)) CT  ON CT.[ID] = dbo.GPSData" +
                ".[ID]";
            this.SelectConflictUpdatedRowsCommand.CommandType = System.Data.CommandType.Text;
            this.SelectConflictUpdatedRowsCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@ID", System.Data.SqlDbType.Int));
            // GPSDataSyncTableSelectIncrementalInsertsCommand command.
            this.SelectIncrementalInsertsCommand = new System.Data.SqlClient.SqlCommand();
            this.SelectIncrementalInsertsCommand.CommandText = @"IF @sync_initialized = 0 SELECT [Longitude], [Latitude], [Altitude], [LocationDateTime], [CreationDate], dbo.GPSData.[ID] FROM dbo.GPSData LEFT OUTER JOIN CHANGETABLE(CHANGES dbo.GPSData, @sync_last_received_anchor) CT ON CT.[ID] = dbo.GPSData.[ID] WHERE (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary) ELSE  BEGIN SELECT [Longitude], [Latitude], [Altitude], [LocationDateTime], [CreationDate], dbo.GPSData.[ID] FROM dbo.GPSData JOIN CHANGETABLE(CHANGES dbo.GPSData, @sync_last_received_anchor) CT ON CT.[ID] = dbo.GPSData.[ID] WHERE (CT.SYS_CHANGE_OPERATION = 'I' AND CT.SYS_CHANGE_CREATION_VERSION  <= @sync_new_received_anchor AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(N'dbo.GPSData')) > @sync_last_received_anchor RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. To recover from this error, the client must reinitialize its local database and try again',16,3,N'dbo.GPSData')  END ";
            this.SelectIncrementalInsertsCommand.CommandType = System.Data.CommandType.Text;
            this.SelectIncrementalInsertsCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_initialized", System.Data.SqlDbType.Bit));
            this.SelectIncrementalInsertsCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_last_received_anchor", System.Data.SqlDbType.BigInt));
            this.SelectIncrementalInsertsCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_client_id_binary", System.Data.SqlDbType.VarBinary));
            this.SelectIncrementalInsertsCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_new_received_anchor", System.Data.SqlDbType.BigInt));
            // GPSDataSyncTableSelectIncrementalDeletesCommand command.
            this.SelectIncrementalDeletesCommand = new System.Data.SqlClient.SqlCommand();
            this.SelectIncrementalDeletesCommand.CommandText = @"IF @sync_initialized > 0  BEGIN SELECT CT.[ID] FROM CHANGETABLE(CHANGES dbo.GPSData, @sync_last_received_anchor) CT WHERE (CT.SYS_CHANGE_OPERATION = 'D' AND CT.SYS_CHANGE_VERSION <= @sync_new_received_anchor AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(N'dbo.GPSData')) > @sync_last_received_anchor RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. To recover from this error, the client must reinitialize its local database and try again',16,3,N'dbo.GPSData')  END ";
            this.SelectIncrementalDeletesCommand.CommandType = System.Data.CommandType.Text;
            this.SelectIncrementalDeletesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_initialized", System.Data.SqlDbType.Bit));
            this.SelectIncrementalDeletesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_last_received_anchor", System.Data.SqlDbType.BigInt));
            this.SelectIncrementalDeletesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_new_received_anchor", System.Data.SqlDbType.BigInt));
            this.SelectIncrementalDeletesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_client_id_binary", System.Data.SqlDbType.VarBinary));
            // GPSDataSyncTableSelectIncrementalUpdatesCommand command.
            this.SelectIncrementalUpdatesCommand = new System.Data.SqlClient.SqlCommand();
            this.SelectIncrementalUpdatesCommand.CommandText = @"IF @sync_initialized > 0  BEGIN SELECT [Longitude], [Latitude], [Altitude], [LocationDateTime], [CreationDate], dbo.GPSData.[ID] FROM dbo.GPSData JOIN CHANGETABLE(CHANGES dbo.GPSData, @sync_last_received_anchor) CT ON CT.[ID] = dbo.GPSData.[ID] WHERE (CT.SYS_CHANGE_OPERATION = 'U' AND CT.SYS_CHANGE_VERSION <= @sync_new_received_anchor AND (CT.SYS_CHANGE_CONTEXT IS NULL OR CT.SYS_CHANGE_CONTEXT <> @sync_client_id_binary)); IF CHANGE_TRACKING_MIN_VALID_VERSION(object_id(N'dbo.GPSData')) > @sync_last_received_anchor RAISERROR (N'SQL Server Change Tracking has cleaned up tracking information for table ''%s''. To recover from this error, the client must reinitialize its local database and try again',16,3,N'dbo.GPSData')  END ";
            this.SelectIncrementalUpdatesCommand.CommandType = System.Data.CommandType.Text;
            this.SelectIncrementalUpdatesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_initialized", System.Data.SqlDbType.Bit));
            this.SelectIncrementalUpdatesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_last_received_anchor", System.Data.SqlDbType.BigInt));
            this.SelectIncrementalUpdatesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_new_received_anchor", System.Data.SqlDbType.BigInt));
            this.SelectIncrementalUpdatesCommand.Parameters.Add(new System.Data.SqlClient.SqlParameter("@sync_client_id_binary", System.Data.SqlDbType.VarBinary));
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitializeAdapterProperties() {
            this.TableName = "GPSData";
        }
    }
    
    public partial class ScannerCacheServerSyncProvider : Microsoft.Synchronization.Data.Server.DbServerSyncProvider {
        
        private GPSDataSyncAdapter _gPSDataSyncAdapter;
        
        partial void OnInitialized();
        
        public ScannerCacheServerSyncProvider() {
            string connectionString = global::Argix.Properties.Settings.Default.ServerTsortConnectionString;
            this.InitializeConnection(connectionString);
            this.InitializeSyncAdapters();
            this.InitializeNewAnchorCommand();
            this.OnInitialized();
        }
        
        public ScannerCacheServerSyncProvider(string connectionString) {
            this.InitializeConnection(connectionString);
            this.InitializeSyncAdapters();
            this.InitializeNewAnchorCommand();
            this.OnInitialized();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public GPSDataSyncAdapter GPSDataSyncAdapter {
            get {
                return this._gPSDataSyncAdapter;
            }
            set {
                this._gPSDataSyncAdapter = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitializeConnection(string connectionString) {
            this.Connection = new System.Data.SqlClient.SqlConnection(connectionString);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitializeSyncAdapters() {
            // Create SyncAdapters.
            this._gPSDataSyncAdapter = new GPSDataSyncAdapter();
            this.SyncAdapters.Add(this._gPSDataSyncAdapter);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitializeNewAnchorCommand() {
            // selectNewAnchorCmd command.
            this.SelectNewAnchorCommand = new System.Data.SqlClient.SqlCommand();
            this.SelectNewAnchorCommand.CommandText = "Select @sync_new_received_anchor = CHANGE_TRACKING_CURRENT_VERSION()";
            this.SelectNewAnchorCommand.CommandType = System.Data.CommandType.Text;
            System.Data.SqlClient.SqlParameter selectnewanchorcommand_sync_new_received_anchorParameter = new System.Data.SqlClient.SqlParameter("@sync_new_received_anchor", System.Data.SqlDbType.BigInt);
            selectnewanchorcommand_sync_new_received_anchorParameter.Direction = System.Data.ParameterDirection.Output;
            this.SelectNewAnchorCommand.Parameters.Add(selectnewanchorcommand_sync_new_received_anchorParameter);
        }
    }
}
