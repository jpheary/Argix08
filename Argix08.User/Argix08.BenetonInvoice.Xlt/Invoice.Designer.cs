//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3615
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

#pragma warning disable 414
namespace Argix.Finance {
    
    
    /// 
    [Microsoft.VisualStudio.Tools.Applications.Runtime.StartupObjectAttribute(1)]
    [global::System.Security.Permissions.PermissionSetAttribute(global::System.Security.Permissions.SecurityAction.Demand, Name="FullTrust")]
    public sealed partial class Invoice : Microsoft.Office.Tools.Excel.Worksheet {
        
        internal Microsoft.Office.Tools.Excel.NamedRange Sheet1_Print_Titles;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToName;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToAddressLine1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToCityStateZip;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Telephone;
        
        internal Microsoft.Office.Tools.Excel.NamedRange InvoiceNumber;
        
        internal Microsoft.Office.Tools.Excel.NamedRange InvoiceDate;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToName;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToAddressLine1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToAddressLine2;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToCityStateZip;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToAddressLine2;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        private global::System.Object missing = global::System.Type.Missing;
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Invoice() : 
                base("Sheet1", "Sheet1") {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void Initialize() {
            base.Initialize();
            Globals.Invoice = this;
            global::System.Windows.Forms.Application.EnableVisualStyles();
            this.InitializeCachedData();
            this.InitializeControls();
            this.InitializeComponents();
            this.InitializeData();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void FinishInitialization() {
            this.InternalStartup();
            this.OnStartup();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void InitializeDataBindings() {
            this.BeginInitialization();
            this.BindToData();
            this.EndInitialization();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeCachedData() {
            if ((this.DataHost == null)) {
                return;
            }
            if (this.DataHost.IsCacheInitialized) {
                this.DataHost.FillCachedData(this);
            }
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeData() {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void BindToData() {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private void StartCaching(string MemberName) {
            this.DataHost.StartCaching(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private void StopCaching(string MemberName) {
            this.DataHost.StopCaching(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private bool IsCached(string MemberName) {
            return this.DataHost.IsCached(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void BeginInitialization() {
            this.BeginInit();
            this.Sheet1_Print_Titles.BeginInit();
            this.RemitToName.BeginInit();
            this.RemitToAddressLine1.BeginInit();
            this.RemitToCityStateZip.BeginInit();
            this.Telephone.BeginInit();
            this.InvoiceNumber.BeginInit();
            this.InvoiceDate.BeginInit();
            this.BillToName.BeginInit();
            this.BillToAddressLine1.BeginInit();
            this.BillToAddressLine2.BeginInit();
            this.BillToCityStateZip.BeginInit();
            this.RemitToAddressLine2.BeginInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void EndInitialization() {
            this.RemitToAddressLine2.EndInit();
            this.BillToCityStateZip.EndInit();
            this.BillToAddressLine2.EndInit();
            this.BillToAddressLine1.EndInit();
            this.BillToName.EndInit();
            this.InvoiceDate.EndInit();
            this.InvoiceNumber.EndInit();
            this.Telephone.EndInit();
            this.RemitToCityStateZip.EndInit();
            this.RemitToAddressLine1.EndInit();
            this.RemitToName.EndInit();
            this.Sheet1_Print_Titles.EndInit();
            this.EndInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeControls() {
            this.Sheet1_Print_Titles = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Sheet1!Print_Titles", this, "Sheet1_Print_Titles");
            this.RemitToName = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToName", this, "RemitToName");
            this.RemitToAddressLine1 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToAddressLine1", this, "RemitToAddressLine1");
            this.RemitToCityStateZip = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToCityStateZip", this, "RemitToCityStateZip");
            this.Telephone = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Telephone", this, "Telephone");
            this.InvoiceNumber = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "InvoiceNumber", this, "InvoiceNumber");
            this.InvoiceDate = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "InvoiceDate", this, "InvoiceDate");
            this.BillToName = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToName", this, "BillToName");
            this.BillToAddressLine1 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToAddressLine1", this, "BillToAddressLine1");
            this.BillToAddressLine2 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToAddressLine2", this, "BillToAddressLine2");
            this.BillToCityStateZip = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToCityStateZip", this, "BillToCityStateZip");
            this.RemitToAddressLine2 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToAddressLine2", this, "RemitToAddressLine2");
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeComponents() {
            ((System.ComponentModel.ISupportInitialize)(this.Sheet1_Print_Titles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToCityStateZip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Telephone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToCityStateZip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Invoice
            // 
            ((System.ComponentModel.ISupportInitialize)(this.Sheet1_Print_Titles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToCityStateZip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Telephone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToCityStateZip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        private bool NeedsFill(string MemberName) {
            return this.DataHost.NeedsFill(this, MemberName);
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void OnShutdown() {
            this.RemitToAddressLine2.Dispose();
            this.BillToCityStateZip.Dispose();
            this.BillToAddressLine2.Dispose();
            this.BillToAddressLine1.Dispose();
            this.BillToName.Dispose();
            this.InvoiceDate.Dispose();
            this.InvoiceNumber.Dispose();
            this.Telephone.Dispose();
            this.RemitToCityStateZip.Dispose();
            this.RemitToAddressLine1.Dispose();
            this.RemitToName.Dispose();
            this.Sheet1_Print_Titles.Dispose();
            base.OnShutdown();
        }
    }
    
    internal sealed partial class Globals {
        
        private static Invoice _Invoice;
        
        internal static Invoice Invoice {
            get {
                return _Invoice;
            }
            set {
                if ((_Invoice == null)) {
                    _Invoice = value;
                }
                else {
                    throw new System.NotSupportedException();
                }
            }
        }
    }
}
