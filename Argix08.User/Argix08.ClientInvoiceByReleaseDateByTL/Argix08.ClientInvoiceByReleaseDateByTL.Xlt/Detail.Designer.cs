﻿//------------------------------------------------------------------------------
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
    public sealed partial class Detail : Microsoft.Office.Tools.Excel.Worksheet {
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToAddressLine1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToAddressLine2;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToCityStateZip;
        
        internal Microsoft.Office.Tools.Excel.NamedRange BillToName;
        
        internal Microsoft.Office.Tools.Excel.NamedRange ClientNumberDiv;
        
        internal Microsoft.Office.Tools.Excel.NamedRange InvoiceDate;
        
        internal Microsoft.Office.Tools.Excel.NamedRange InvoiceNumber;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Sheet2_Print_Titles;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToAddressLine1;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToCityStateZip;
        
        internal Microsoft.Office.Tools.Excel.NamedRange RemitToName;
        
        internal Microsoft.Office.Tools.Excel.NamedRange Telephone;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        private global::System.Object missing = global::System.Type.Missing;
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        public Detail() : 
                base("Sheet2", "Sheet2") {
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        protected override void Initialize() {
            base.Initialize();
            Globals.Detail = this;
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
            this.BillToAddressLine1.BeginInit();
            this.BillToAddressLine2.BeginInit();
            this.BillToCityStateZip.BeginInit();
            this.BillToName.BeginInit();
            this.ClientNumberDiv.BeginInit();
            this.InvoiceDate.BeginInit();
            this.InvoiceNumber.BeginInit();
            this.Sheet2_Print_Titles.BeginInit();
            this.RemitToAddressLine1.BeginInit();
            this.RemitToCityStateZip.BeginInit();
            this.RemitToName.BeginInit();
            this.Telephone.BeginInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void EndInitialization() {
            this.Telephone.EndInit();
            this.RemitToName.EndInit();
            this.RemitToCityStateZip.EndInit();
            this.RemitToAddressLine1.EndInit();
            this.Sheet2_Print_Titles.EndInit();
            this.InvoiceNumber.EndInit();
            this.InvoiceDate.EndInit();
            this.ClientNumberDiv.EndInit();
            this.BillToName.EndInit();
            this.BillToCityStateZip.EndInit();
            this.BillToAddressLine2.EndInit();
            this.BillToAddressLine1.EndInit();
            this.EndInit();
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeControls() {
            this.BillToAddressLine1 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToAddressLine1", this, "BillToAddressLine1");
            this.BillToAddressLine2 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToAddressLine2", this, "BillToAddressLine2");
            this.BillToCityStateZip = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToCityStateZip", this, "BillToCityStateZip");
            this.BillToName = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "BillToName", this, "BillToName");
            this.ClientNumberDiv = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "ClientNumberDiv", this, "ClientNumberDiv");
            this.InvoiceDate = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "InvoiceDate", this, "InvoiceDate");
            this.InvoiceNumber = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "InvoiceNumber", this, "InvoiceNumber");
            this.Sheet2_Print_Titles = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Sheet2!Print_Titles", this, "Sheet2_Print_Titles");
            this.RemitToAddressLine1 = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToAddressLine1", this, "RemitToAddressLine1");
            this.RemitToCityStateZip = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToCityStateZip", this, "RemitToCityStateZip");
            this.RemitToName = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "RemitToName", this, "RemitToName");
            this.Telephone = new Microsoft.Office.Tools.Excel.NamedRange(this.ItemProvider, this.HostContext, "Telephone", this, "Telephone");
        }
        
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Tools.Office.ProgrammingModel.dll", "9.0.0.0")]
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Never)]
        private void InitializeComponents() {
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
            this.Telephone.Dispose();
            this.RemitToName.Dispose();
            this.RemitToCityStateZip.Dispose();
            this.RemitToAddressLine1.Dispose();
            this.Sheet2_Print_Titles.Dispose();
            this.InvoiceNumber.Dispose();
            this.InvoiceDate.Dispose();
            this.ClientNumberDiv.Dispose();
            this.BillToName.Dispose();
            this.BillToCityStateZip.Dispose();
            this.BillToAddressLine2.Dispose();
            this.BillToAddressLine1.Dispose();
            base.OnShutdown();
        }
    }
    
    internal sealed partial class Globals {
        
        private static Detail _Detail;
        
        internal static Detail Detail {
            get {
                return _Detail;
            }
            set {
                if ((_Detail == null)) {
                    _Detail = value;
                }
                else {
                    throw new System.NotSupportedException();
                }
            }
        }
    }
}
