﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.832
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 1591

namespace Tsort.Enterprise {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
    [Serializable()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.ComponentModel.ToolboxItem(true)]
    [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [System.Xml.Serialization.XmlRootAttribute("VendorDS")]
    [System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class VendorDS : System.Data.DataSet {
        
        private VendorDetailTableDataTable tableVendorDetailTable;
        
        private System.Data.SchemaSerializationMode _schemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public VendorDS() {
            this.BeginInit();
            this.InitClass();
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected VendorDS(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
                if ((ds.Tables["VendorDetailTable"] != null)) {
                    base.Tables.Add(new VendorDetailTableDataTable(ds.Tables["VendorDetailTable"]));
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
                this.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content)]
        public VendorDetailTableDataTable VendorDetailTable {
            get {
                return this.tableVendorDetailTable;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.BrowsableAttribute(true)]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.DesignerSerializationVisibilityAttribute(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public override System.Data.DataSet Clone() {
            VendorDS cln = ((VendorDS)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override void ReadXmlSerializable(System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                System.Data.DataSet ds = new System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["VendorDetailTable"] != null)) {
                    base.Tables.Add(new VendorDetailTableDataTable(ds.Tables["VendorDetailTable"]));
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
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        protected override System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            this.WriteXmlSchema(new System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return System.Xml.Schema.XmlSchema.Read(new System.Xml.XmlTextReader(stream), null);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        internal void InitVars(bool initTable) {
            this.tableVendorDetailTable = ((VendorDetailTableDataTable)(base.Tables["VendorDetailTable"]));
            if ((initTable == true)) {
                if ((this.tableVendorDetailTable != null)) {
                    this.tableVendorDetailTable.InitVars();
                }
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void InitClass() {
            this.DataSetName = "VendorDS";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/VendorDS.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableVendorDetailTable = new VendorDetailTableDataTable();
            base.Tables.Add(this.tableVendorDetailTable);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private bool ShouldSerializeVendorDetailTable() {
            return false;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private void SchemaChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(System.Xml.Schema.XmlSchemaSet xs) {
            VendorDS ds = new VendorDS();
            System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
            System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
            xs.Add(ds.GetSchemaSerializable());
            System.Xml.Schema.XmlSchemaAny any = new System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            return type;
        }
        
        public delegate void VendorDetailTableRowChangeEventHandler(object sender, VendorDetailTableRowChangeEvent e);
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        [System.Serializable()]
        [System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class VendorDetailTableDataTable : System.Data.DataTable, System.Collections.IEnumerable {
            
            private System.Data.DataColumn columnNUMBER;
            
            private System.Data.DataColumn columnNAME;
            
            private System.Data.DataColumn columnSTATUS;
            
            private System.Data.DataColumn columnADDRESS_LINE1;
            
            private System.Data.DataColumn columnADDRESS_LINE2;
            
            private System.Data.DataColumn columnCITY;
            
            private System.Data.DataColumn columnSTATE;
            
            private System.Data.DataColumn columnZIP;
            
            private System.Data.DataColumn columnZIP4;
            
            private System.Data.DataColumn columnUSERDATA;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public VendorDetailTableDataTable() {
                this.TableName = "VendorDetailTable";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal VendorDetailTableDataTable(System.Data.DataTable table) {
                this.TableName = table.TableName;
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
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected VendorDetailTableDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn NUMBERColumn {
                get {
                    return this.columnNUMBER;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn NAMEColumn {
                get {
                    return this.columnNAME;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn STATUSColumn {
                get {
                    return this.columnSTATUS;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn ADDRESS_LINE1Column {
                get {
                    return this.columnADDRESS_LINE1;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn ADDRESS_LINE2Column {
                get {
                    return this.columnADDRESS_LINE2;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn CITYColumn {
                get {
                    return this.columnCITY;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn STATEColumn {
                get {
                    return this.columnSTATE;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn ZIPColumn {
                get {
                    return this.columnZIP;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn ZIP4Column {
                get {
                    return this.columnZIP4;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataColumn USERDATAColumn {
                get {
                    return this.columnUSERDATA;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public VendorDetailTableRow this[int index] {
                get {
                    return ((VendorDetailTableRow)(this.Rows[index]));
                }
            }
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowChanging;
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowChanged;
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowDeleting;
            
            public event VendorDetailTableRowChangeEventHandler VendorDetailTableRowDeleted;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void AddVendorDetailTableRow(VendorDetailTableRow row) {
                this.Rows.Add(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
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
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public virtual System.Collections.IEnumerator GetEnumerator() {
                return this.Rows.GetEnumerator();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public override System.Data.DataTable Clone() {
                VendorDetailTableDataTable cln = ((VendorDetailTableDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataTable CreateInstance() {
                return new VendorDetailTableDataTable();
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal void InitVars() {
                this.columnNUMBER = base.Columns["NUMBER"];
                this.columnNAME = base.Columns["NAME"];
                this.columnSTATUS = base.Columns["STATUS"];
                this.columnADDRESS_LINE1 = base.Columns["ADDRESS_LINE1"];
                this.columnADDRESS_LINE2 = base.Columns["ADDRESS_LINE2"];
                this.columnCITY = base.Columns["CITY"];
                this.columnSTATE = base.Columns["STATE"];
                this.columnZIP = base.Columns["ZIP"];
                this.columnZIP4 = base.Columns["ZIP4"];
                this.columnUSERDATA = base.Columns["USERDATA"];
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            private void InitClass() {
                this.columnNUMBER = new System.Data.DataColumn("NUMBER", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnNUMBER);
                this.columnNAME = new System.Data.DataColumn("NAME", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnNAME);
                this.columnSTATUS = new System.Data.DataColumn("STATUS", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSTATUS);
                this.columnADDRESS_LINE1 = new System.Data.DataColumn("ADDRESS_LINE1", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnADDRESS_LINE1);
                this.columnADDRESS_LINE2 = new System.Data.DataColumn("ADDRESS_LINE2", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnADDRESS_LINE2);
                this.columnCITY = new System.Data.DataColumn("CITY", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnCITY);
                this.columnSTATE = new System.Data.DataColumn("STATE", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnSTATE);
                this.columnZIP = new System.Data.DataColumn("ZIP", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnZIP);
                this.columnZIP4 = new System.Data.DataColumn("ZIP4", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnZIP4);
                this.columnUSERDATA = new System.Data.DataColumn("USERDATA", typeof(string), null, System.Data.MappingType.Element);
                base.Columns.Add(this.columnUSERDATA);
                this.columnNUMBER.AllowDBNull = false;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public VendorDetailTableRow NewVendorDetailTableRow() {
                return ((VendorDetailTableRow)(this.NewRow()));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) {
                return new VendorDetailTableRow(builder);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override System.Type GetRowType() {
                return typeof(VendorDetailTableRow);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanged(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.VendorDetailTableRowChanged != null)) {
                    this.VendorDetailTableRowChanged(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowChanging(System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.VendorDetailTableRowChanging != null)) {
                    this.VendorDetailTableRowChanging(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleted(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.VendorDetailTableRowDeleted != null)) {
                    this.VendorDetailTableRowDeleted(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            protected override void OnRowDeleting(System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.VendorDetailTableRowDeleting != null)) {
                    this.VendorDetailTableRowDeleting(this, new VendorDetailTableRowChangeEvent(((VendorDetailTableRow)(e.Row)), e.Action));
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void RemoveVendorDetailTableRow(VendorDetailTableRow row) {
                this.Rows.Remove(row);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public static System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(System.Xml.Schema.XmlSchemaSet xs) {
                System.Xml.Schema.XmlSchemaComplexType type = new System.Xml.Schema.XmlSchemaComplexType();
                System.Xml.Schema.XmlSchemaSequence sequence = new System.Xml.Schema.XmlSchemaSequence();
                VendorDS ds = new VendorDS();
                xs.Add(ds.GetSchemaSerializable());
                System.Xml.Schema.XmlSchemaAny any1 = new System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                System.Xml.Schema.XmlSchemaAny any2 = new System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                System.Xml.Schema.XmlSchemaAttribute attribute1 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                System.Xml.Schema.XmlSchemaAttribute attribute2 = new System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "VendorDetailTableDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                return type;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public partial class VendorDetailTableRow : System.Data.DataRow {
            
            private VendorDetailTableDataTable tableVendorDetailTable;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal VendorDetailTableRow(System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableVendorDetailTable = ((VendorDetailTableDataTable)(this.Table));
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string NUMBER {
                get {
                    return ((string)(this[this.tableVendorDetailTable.NUMBERColumn]));
                }
                set {
                    this[this.tableVendorDetailTable.NUMBERColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string NAME {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.NAMEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'NAME\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.NAMEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string STATUS {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.STATUSColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'STATUS\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.STATUSColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ADDRESS_LINE1 {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ADDRESS_LINE1Column]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'ADDRESS_LINE1\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ADDRESS_LINE1Column] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ADDRESS_LINE2 {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ADDRESS_LINE2Column]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'ADDRESS_LINE2\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ADDRESS_LINE2Column] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string CITY {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.CITYColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'CITY\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.CITYColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string STATE {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.STATEColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'STATE\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.STATEColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ZIP {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ZIPColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'ZIP\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ZIPColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string ZIP4 {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.ZIP4Column]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'ZIP4\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.ZIP4Column] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public string USERDATA {
                get {
                    try {
                        return ((string)(this[this.tableVendorDetailTable.USERDATAColumn]));
                    }
                    catch (System.InvalidCastException e) {
                        throw new System.Data.StrongTypingException("The value for column \'USERDATA\' in table \'VendorDetailTable\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableVendorDetailTable.USERDATAColumn] = value;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsNAMENull() {
                return this.IsNull(this.tableVendorDetailTable.NAMEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetNAMENull() {
                this[this.tableVendorDetailTable.NAMEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSTATUSNull() {
                return this.IsNull(this.tableVendorDetailTable.STATUSColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSTATUSNull() {
                this[this.tableVendorDetailTable.STATUSColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsADDRESS_LINE1Null() {
                return this.IsNull(this.tableVendorDetailTable.ADDRESS_LINE1Column);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetADDRESS_LINE1Null() {
                this[this.tableVendorDetailTable.ADDRESS_LINE1Column] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsADDRESS_LINE2Null() {
                return this.IsNull(this.tableVendorDetailTable.ADDRESS_LINE2Column);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetADDRESS_LINE2Null() {
                this[this.tableVendorDetailTable.ADDRESS_LINE2Column] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsCITYNull() {
                return this.IsNull(this.tableVendorDetailTable.CITYColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetCITYNull() {
                this[this.tableVendorDetailTable.CITYColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsSTATENull() {
                return this.IsNull(this.tableVendorDetailTable.STATEColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetSTATENull() {
                this[this.tableVendorDetailTable.STATEColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsZIPNull() {
                return this.IsNull(this.tableVendorDetailTable.ZIPColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetZIPNull() {
                this[this.tableVendorDetailTable.ZIPColumn] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsZIP4Null() {
                return this.IsNull(this.tableVendorDetailTable.ZIP4Column);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetZIP4Null() {
                this[this.tableVendorDetailTable.ZIP4Column] = System.Convert.DBNull;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public bool IsUSERDATANull() {
                return this.IsNull(this.tableVendorDetailTable.USERDATAColumn);
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public void SetUSERDATANull() {
                this[this.tableVendorDetailTable.USERDATAColumn] = System.Convert.DBNull;
            }
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "2.0.0.0")]
        public class VendorDetailTableRowChangeEvent : System.EventArgs {
            
            private VendorDetailTableRow eventRow;
            
            private System.Data.DataRowAction eventAction;
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public VendorDetailTableRowChangeEvent(VendorDetailTableRow row, System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public VendorDetailTableRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}

#pragma warning restore 1591