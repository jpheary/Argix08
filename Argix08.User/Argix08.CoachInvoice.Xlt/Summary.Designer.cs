//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.3074
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
    public sealed partial class Summary : Microsoft.Office.Tools.Excel.Worksheet {
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToName;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Account;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToCityStateZip;
        
        internal Microsoft.Office.Tools.Excel.NamedRange InvoiceDate;
        
        internal Microsoft.Office.Tools.Excel.NamedRange InvoiceNumber;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Terms1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToAddressLine1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToAddressLine2;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToCityStateZip;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToName;
        
        internal Microsoft.Office.Tools.Excel.NamedRange DeliveryCharges;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Pieces;
        
        internal Microsoft.Office.Tools.Excel.NamedRange ShipmentDay;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Weight;
        
        internal Microsoft.Office.Tools.Excel.NamedRange LineHaulCharges;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToAddressLine1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToAddressLine2;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        private global::System.Object missing = global::System.Type.Missing;
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Summary() : 
                base("Sheet1", "Sheet1") {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void Initialize() {
            base.Initialize();
            Globals.Summary = this;
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
            this.BillToName.BeginInit();
            this.Account.BeginInit();
            this.BillToCityStateZip.BeginInit();
            this.InvoiceDate.BeginInit();
            this.InvoiceNumber.BeginInit();
            this.Terms1.BeginInit();
            this.RemitToAddressLine1.BeginInit();
            this.RemitToAddressLine2.BeginInit();
            this.RemitToCityStateZip.BeginInit();
            this.RemitToName.BeginInit();
            this.DeliveryCharges.BeginInit();
            this.Pieces.BeginInit();
            this.ShipmentDay.BeginInit();
            this.Weight.BeginInit();
            this.LineHaulCharges.BeginInit();
            this.BillToAddressLine1.BeginInit();
            this.BillToAddressLine2.BeginInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void EndInitialization() {
            this.BillToAddressLine2.EndInit();
            this.BillToAddressLine1.EndInit();
            this.LineHaulCharges.EndInit();
            this.Weight.EndInit();
            this.ShipmentDay.EndInit();
            this.Pieces.EndInit();
            this.DeliveryCharges.EndInit();
            this.RemitToName.EndInit();
            this.RemitToCityStateZip.EndInit();
            this.RemitToAddressLine2.EndInit();
            this.RemitToAddressLine1.EndInit();
            this.Terms1.EndInit();
            this.InvoiceNumber.EndInit();
            this.InvoiceDate.EndInit();
            this.BillToCityStateZip.EndInit();
            this.Account.EndInit();
            this.BillToName.EndInit();
            this.EndInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeControls() {
            this.BillToName = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToName", this, "BillToName");
            this.Account = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Account", this, "Account");
            this.BillToCityStateZip = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToCityStateZip", this, "BillToCityStateZip");
            this.InvoiceDate = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "InvoiceDate", this, "InvoiceDate");
            this.InvoiceNumber = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "InvoiceNumber", this, "InvoiceNumber");
            this.Terms1 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Terms", this, "Terms1");
            this.RemitToAddressLine1 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToAddressLine1", this, "RemitToAddressLine1");
            this.RemitToAddressLine2 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToAddressLine2", this, "RemitToAddressLine2");
            this.RemitToCityStateZip = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToCityStateZip", this, "RemitToCityStateZip");
            this.RemitToName = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToName", this, "RemitToName");
            this.DeliveryCharges = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "DeliveryCharges", this, "DeliveryCharges");
            this.Pieces = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Pieces", this, "Pieces");
            this.ShipmentDay = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "ShipmentDay", this, "ShipmentDay");
            this.Weight = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Weight", this, "Weight");
            this.LineHaulCharges = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "LineHaulCharges", this, "LineHaulCharges");
            this.BillToAddressLine1 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToAddressLine1", this, "BillToAddressLine1");
            this.BillToAddressLine2 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToAddressLine2", this, "BillToAddressLine2");
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeComponents() {
            ((System.ComponentModel.ISupportInitialize)(this.BillToName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Account)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToCityStateZip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Terms1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToCityStateZip)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pieces)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipmentDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Weight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineHaulCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Account)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToCityStateZip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InvoiceNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Terms1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToAddressLine2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToCityStateZip)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RemitToName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DeliveryCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Pieces)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ShipmentDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Weight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LineHaulCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BillToAddressLine2)).EndInit();
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
            this.BillToAddressLine2.Dispose();
            this.BillToAddressLine1.Dispose();
            this.LineHaulCharges.Dispose();
            this.Weight.Dispose();
            this.ShipmentDay.Dispose();
            this.Pieces.Dispose();
            this.DeliveryCharges.Dispose();
            this.RemitToName.Dispose();
            this.RemitToCityStateZip.Dispose();
            this.RemitToAddressLine2.Dispose();
            this.RemitToAddressLine1.Dispose();
            this.Terms1.Dispose();
            this.InvoiceNumber.Dispose();
            this.InvoiceDate.Dispose();
            this.BillToCityStateZip.Dispose();
            this.Account.Dispose();
            this.BillToName.Dispose();
            base.OnShutdown();
        }
    }
    
    internal sealed partial class Globals {
        
        private static Summary _Summary;
        
        internal static Summary Summary {
            get {
                return _Summary;
            }
            set {
                if ((_Summary == null)) {
                    _Summary = value;
                }
                else {
                    throw new System.NotSupportedException();
                }
            }
        }
    }
}
