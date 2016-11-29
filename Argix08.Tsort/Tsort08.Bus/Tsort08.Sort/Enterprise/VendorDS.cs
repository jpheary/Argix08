﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.4322.2032
//
//     Changes to this file may cause incorrect behavior and will be lost if 
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Tsort.Enterprise {
    using System;
    using System.Data;
    using System.Xml;
    using System.Runtime.Serialization;
    
    
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Diagnostics.DebuggerStepThrough()]
    [System.ComponentModel.ToolboxItem(true)]
    public class VendorDS : DataSet {
        
        private VendorDetailTableDataTable tableVendorDetailTable;
        
        public VendorDS() {
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        protected VendorDS(SerializationInfo info, StreamingContext context) {
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((strSchema != null)) {
                DataSet ds = new DataSet();
                ds.ReadXmlSchema(new XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["VendorDetailTable"] != null)) {
                    this.Tables.Add(new VendorDetailTableDataTable(ds.Tables["VendorDetailTable"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.InitClass();
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            this.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public VendorDetailTableDataTable VendorDetailTable {
            get {
                return this.tableVendorDetailTable;
            }
        }
        
        public override DataSet Clone() {
            VendorDS cln = ((VendorDS)(base.Clone()));
            cln.InitVars();
            return cln;
        }
        
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        protected override void ReadXmlSerializable(XmlReader reader) {
            this.Reset();
            DataSet ds = new DataSet();
            ds.ReadXml(reader);
            if ((ds.Tables["VendorDetailTable"] != null)) {
                this.Tables.Add(new VendorDetailTableDataTable(ds.Tables["VendorDetailTable"]));
            }
            this.DataSetName = ds.DataSetName;
            this.Prefix = ds.Prefix;
            this.Namespace = ds.Namespace;
            this.Locale = ds.Locale;
            this.CaseSensitive = ds.CaseSensitive;
            this.EnforceConstraints = ds.EnforceConstraints;
            this.Merge(ds, false, System.Data.MissingSchemaAction.Add);
            this.InitVars();
        }
        
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new XmlTextReader(stream), null);
        }
        
        internal void InitVars() {
            this.tableVendorDetailTable = ((VendorDetailTableDataTable)(this.Tables["VendorDetailTable"]));
            if ((this.tableVendorDetailTable != null)) {
                this.tableVendorDetailTable.InitVars();
            }
        }
        
        private void InitClass() {
            this.DataSetName = "VendorDS";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/VendorDS.xsd";
            this.Locale = new System.Globalization.CultureInfo("en-US");
            this.CaseSensitive = false;
            this.EnforceConstraints = true;
            this.tableVendorDetailTable = new VendorDetailTableDataTable();
            this.Tables.Add(this.tableVendorDetailTable);
        }
        
        private bool ShouldSerializeVendorDetailTable() {
            return false;
        }
        
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        public delegate void VendorDetailTableRowChangeEventHandler(object sender, VendorDetailTableRowChangeEvent e);
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VendorDetailTableDataTable : DataTable, System.Collections.IEnumerable {
            
            private DataColumn columnNUMBER;
            
            private DataColumn columnNAME;
            
            private DataColumn columnSTATUS;
            
            private DataColumn columnADDRESS_LINE1;
            
            private DataColumn columnADDRESS_LINE2;
            
            private DataColumn columnCITY;
            
            private DataColumn columnSTATE;
            
            private DataColumn columnZIP;
            
            private DataColumn columnZIP4;
            
            private DataColumn columnUSERDATA;
            
            internal VendorDetailTableDataTable() : 
                    base("VendorDetailTable") {
                this.InitClass();
            }
            
            internal VendorDetailTableDataTable(DataTable table) : 
                    base(table.TableName) {
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
                this.DisplayExpression = table.DisplayExpression;
            }
            
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            internal DataColumn NUMBERColumn {
                get {
                    return this.columnNUMBER;
                }
            }
            
            internal DataColumn NAMEColumn {
                get {
                    return this.columnNAME;
                }
            }
            
            internal DataColumn STATUSColumn {
                get {
                    return this.columnSTATUS;
                }
            }
            
            internal DataColumn ADDRESS_LINE1Column {
                get {
                    return this.columnADDRESS_LINE1;
                }
            }
            
            internal DataColumn ADDRESS_LINE2Column {
                get {
                    return this.columnADDRESS_LINE2;
                }
            }
            
            internal DataColumn CITYColumn {
                get {
                    return this.columnCITY;
                }
            }
            
            internal DataColumn STATEColumn {
                get {
                    return this.columnSTATE;
                }
            }
            
            internal DataColumn ZIPColumn {
                get {
                    return this.columnZIP;
                }
            }
            
            internal DataColumn ZIP4Column {
                get {
                    return this.columnZIP4;
                }
            }
            
            internal DataColumn USERDATAColumn {
                get {
                    return this.columnUSERDATA;
                }
            }
            
            public VendorDetailTableRow this[int index] {
                get {
                    return ((VendorDetailTableRow)(this.Rows[index]));
                }
            }
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowChanged;
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowChanging;
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowDeleted;
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowDeleting;
            
            public void AddVendorDetailTableRow(VendorDetailTableRow row) {
                this.Rows.Add(row);
            }
            
            public VendorDetailTableRow AddVendorDetailTableRow(string NUMBER, string NAME, string STATUS, string ADDRESS_LINE1, string ADDRESS_LINE2, string CITY, string STATE, string ZIP, string ZIP4, string USERDATA) {
                VendorDetailTableRow rowVendorDetailTableRow = ((VendorDetailTableRow)(this.NewRow()));
                rowVendorDetailTableRow.ItemArray = new object[] {
                        NUMBER,
                        NAME,
                        STATUS,
                        ADDRESS_LINE1,
                        ADDRESS_LINE2,
                        CITY,
                        STATE,
                        ZIP,
                        ZIP4,
                        USERDATA};
                this.Rows.Add(rowVendorDetailTableRow);
                return rowVendorDetailTableRow;
            }
            
            public System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            public override DataTable Clone() {
                VendorDetailTableDataTable cln = ((VendorDetailTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            protected override DataTable CreateInstance() {
                return new VendorDetailTableDataTable();
            }
            
            internal void InitVars() {
                this.columnNUMBER = this.Columns["NUMBER"];
                this.columnNAME = this.Columns["NAME"];
                this.columnSTATUS = this.Columns["STATUS"];
                this.columnADDRESS_LINE1 = this.Columns["ADDRESS_LINE1"];
                this.columnADDRESS_LINE2 = this.Columns["ADDRESS_LINE2"];
                this.columnCITY = this.Columns["CITY"];
                this.columnSTATE = this.Columns["STATE"];
                this.columnZIP = this.Columns["ZIP"];
                this.columnZIP4 = this.Columns["ZIP4"];
                this.columnUSERDATA = this.Columns["USERDATA"];
            }
            
            private void InitClass() {
                this.columnNUMBER = new DataColumn("NUMBER", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnNUMBER);
                this.columnNAME = new DataColumn("NAME", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnNAME);
                this.columnSTATUS = new DataColumn("STATUS", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnSTATUS);
                this.columnADDRESS_LINE1 = new DataColumn("ADDRESS_LINE1", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnADDRESS_LINE1);
                this.columnADDRESS_LINE2 = new DataColumn("ADDRESS_LINE2", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnADDRESS_LINE2);
                this.columnCITY = new DataColumn("CITY", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnCITY);
                this.columnSTATE = new DataColumn("STATE", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnSTATE);
                this.columnZIP = new DataColumn("ZIP", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnZIP);
                this.columnZIP4 = new DataColumn("ZIP4", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnZIP4);
                this.columnUSERDATA = new DataColumn("USERDATA", typeof(string), null, System.Data.MappingType.Element);
                this.Columns.Add(this.columnUSERDATA);
                this.columnNUMBER.AllowDBNull = false;
            }
            
            public VendorDetailTableRow NewVendorDetailTableRow() {
                return ((VendorDetailTableRow)(this.NewRow()));
            }
            
            protected override DataRow NewRowFromBuilder(DataRowBuilder builder) {
                return new VendorDetailTableRow(builder);
            }
            
            protected override System.Type GetRowType() {
                return typeof(VendorDetailTableRow);
            }
            
            protected override void OnRowChanged(DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.VendorDetailTableRowChanged != null)) {
                    this.VendorDetailTableRowChanged(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowChanging(DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.VendorDetailTableRowChanging != null)) {
                    this.VendorDetailTableRowChanging(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleted(DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.VendorDetailTableRowDeleted != null)) {
                    this.VendorDetailTableRowDeleted(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            protected override void OnRowDeleting(DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.VendorDetailTableRowDeleting != null)) {
                    this.VendorDetailTableRowDeleting(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            public void RemoveVendorDetailTableRow(VendorDetailTableRow row) {
                this.Rows.Remove(row);
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VendorDetailTableRow : DataRow {
            
            private VendorDetailTableDataTable tableVendorDetailTable;
            
            internal VendorDetailTableRow(DataRowBuilder rb) : 
                    base(rb) {
                this.tableVendorDetailTable = ((VendorDetailTableDataTable)(this.Table));
            }
            
            public string NUMBER {
                get {
                    return ((string)(this[this.tableVendorDetailTable.NUMBERColumn]));
                }
                set {
                    this[this.tableVendorDetailTable.NUMBERColumn] = value;
                }
            }
            
            public string NAME {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.NAMEColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.NAMEColumn] = value;
                }
            }
            
            public string STATUS {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.STATUSColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.STATUSColumn] = value;
                }
            }
            
            public string ADDRESS_LINE1 {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ADDRESS_LINE1Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ADDRESS_LINE1Column] = value;
                }
            }
            
            public string ADDRESS_LINE2 {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ADDRESS_LINE2Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ADDRESS_LINE2Column] = value;
                }
            }
            
            public string CITY {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.CITYColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.CITYColumn] = value;
                }
            }
            
            public string STATE {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.STATEColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.STATEColumn] = value;
                }
            }
            
            public string ZIP {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ZIPColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ZIPColumn] = value;
                }
            }
            
            public string ZIP4 {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ZIP4Column]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ZIP4Column] = value;
                }
            }
            
            public string USERDATA {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.USERDATAColumn]));
                    }
                    catch (InvalidCastException e) {
                        throw new StrongTypingException("Cannot get value because it is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.USERDATAColumn] = value;
                }
            }
            
            public bool IsNAMENull() {
                return this.IsNull(this.tableVendorDetailTable.NAMEColumn);
            }
            
            public void SetNAMENull() {
                this[this.tableVendorDetailTable.NAMEColumn] = System.Convert.DBNull;
            }
            
            public bool IsSTATUSNull() {
                return this.IsNull(this.tableVendorDetailTable.STATUSColumn);
            }
            
            public void SetSTATUSNull() {
                this[this.tableVendorDetailTable.STATUSColumn] = System.Convert.DBNull;
            }
            
            public bool IsADDRESS_LINE1Null() {
                return this.IsNull(this.tableVendorDetailTable.ADDRESS_LINE1Column);
            }
            
            public void SetADDRESS_LINE1Null() {
                this[this.tableVendorDetailTable.ADDRESS_LINE1Column] = System.Convert.DBNull;
            }
            
            public bool IsADDRESS_LINE2Null() {
                return this.IsNull(this.tableVendorDetailTable.ADDRESS_LINE2Column);
            }
            
            public void SetADDRESS_LINE2Null() {
                this[this.tableVendorDetailTable.ADDRESS_LINE2Column] = System.Convert.DBNull;
            }
            
            public bool IsCITYNull() {
                return this.IsNull(this.tableVendorDetailTable.CITYColumn);
            }
            
            public void SetCITYNull() {
                this[this.tableVendorDetailTable.CITYColumn] = System.Convert.DBNull;
            }
            
            public bool IsSTATENull() {
                return this.IsNull(this.tableVendorDetailTable.STATEColumn);
            }
            
            public void SetSTATENull() {
                this[this.tableVendorDetailTable.STATEColumn] = System.Convert.DBNull;
            }
            
            public bool IsZIPNull() {
                return this.IsNull(this.tableVendorDetailTable.ZIPColumn);
            }
            
            public void SetZIPNull() {
                this[this.tableVendorDetailTable.ZIPColumn] = System.Convert.DBNull;
            }
            
            public bool IsZIP4Null() {
                return this.IsNull(this.tableVendorDetailTable.ZIP4Column);
            }
            
            public void SetZIP4Null() {
                this[this.tableVendorDetailTable.ZIP4Column] = System.Convert.DBNull;
            }
            
            public bool IsUSERDATANull() {
                return this.IsNull(this.tableVendorDetailTable.USERDATAColumn);
            }
            
            public void SetUSERDATANull() {
                this[this.tableVendorDetailTable.USERDATAColumn] = System.Convert.DBNull;
            }
        }
        
        [System.Diagnostics.DebuggerStepThrough()]
        public class VendorDetailTableRowChangeEvent : EventArgs {
            
            private VendorDetailTableRow eventRow;
            
            private DataRowAction eventAction;
            
            public VendorDetailTableRowChangeEvent(VendorDetailTableRow row, DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            public VendorDetailTableRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            public DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
