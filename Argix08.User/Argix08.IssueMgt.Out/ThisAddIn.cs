using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Outlook=Microsoft.Office.Interop.Outlook;
using Office=Microsoft.Office.Core;
using Tools=Microsoft.Office.Tools;
using Argix.Data;
using Argix.Enterprise;

namespace Argix.CustomerSvc {
    //
    public partial class ThisAddIn {
        //Members        
        private IssueMgtRibbon mRibbon = null;
        private Outlook.Inspectors mInspectors=null;
        public Dictionary<Outlook.Inspector,InspectorWrapper> InspectorWrappers = new Dictionary<Outlook.Inspector,InspectorWrapper>();

        public const string TASKPANE_TITLE = "Issue Mgt Explorer v3.0.2.4";

        //Interface
        public IssueMgtRibbon Ribbon { get { return this.mRibbon; } }
        protected override Office.IRibbonExtensibility CreateRibbonExtensibilityObject() {
            //Override for creating a custom ribbon
            //NOTE: This method is called once for the AddIn (before _Startup)
            if(this.mRibbon == null)
                this.mRibbon = new IssueMgtRibbon();
            return this.mRibbon;
        }
        private void ThisAddIn_Startup(object sender,System.EventArgs e) {
            try {
                //Setup Issue Mgt
                try {
                    LogLevel level = App.Config != null ? (LogLevel)App.Config.TraceLevel : LogLevel.None;
                    ArgixTrace.AddListener(new DBTraceListener(level,App.Mediator,App.USP_TRACE,App.EventLogName));
                    IssueInspector.OutlookApp = this.Application;
                    CRGFactory.TempFolder = global::Argix.CustomerSvc.Settings.Default.TempFolder;
                    EnterpriseFactory.Mediator = CRGFactory.Mediator = App.Mediator;
                }
                catch(Argix.Data.DataAccessException ex) { App.ReportError(ex,true,LogLevel.Warning); }

                //Set references to Outlook Inspectors
                this.mInspectors = this.Application.Inspectors;
                this.mInspectors.NewInspector += new Outlook.InspectorsEvents_NewInspectorEventHandler(OnNewInspector);
                foreach(Outlook.Inspector inspector in this.mInspectors) { OnNewInspector(inspector); }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void ThisAddIn_Shutdown(object sender,System.EventArgs e) {
            //Remove custom task pane
            //Remove ref
            IssueInspector.OutlookApp = null;

            try {
                //Remove inspector ribbon support
                this.mRibbon = null;
                this.mInspectors.NewInspector -= new Outlook.InspectorsEvents_NewInspectorEventHandler(OnNewInspector);
                this.mInspectors = null;
                this.InspectorWrappers.Clear();
                this.InspectorWrappers = null;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup() {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
        private void OnNewInspector(Outlook.Inspector inspector) {
            //Event handler for new Inspector event
            try {
                if(inspector.CurrentItem is Outlook.MailItem) {
                    this.InspectorWrappers.Add(inspector,new InspectorWrapper(inspector));
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
    }
    public class InspectorWrapper {
        //Members
        private Outlook.Inspector mInspector=null;
        private Tools.CustomTaskPane mTaskPane=null;

        //Interface
        public InspectorWrapper(Outlook.Inspector inspector) {
            //Constructor
            this.mInspector = inspector;
            ((Outlook.InspectorEvents_Event)this.mInspector).Close += new Outlook.InspectorEvents_CloseEventHandler(OnInspectorClose);

            IssueSelector issueSel = new IssueSelector();
            issueSel.IssueSelected += new EventHandler(OnIssueCtlIssueSelected);
            issueSel.Error += new ControlErrorEventHandler(OnIssueCtlError);
            this.mTaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(issueSel,ThisAddIn.TASKPANE_TITLE,this.mInspector);
            this.mTaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionLeft;
            this.mTaskPane.Visible = false;
            this.mTaskPane.VisibleChanged += new EventHandler(OnTaskPaneVisibleChanged);
        }
        public Outlook.Inspector Inspector { get { return this.mInspector; } }
        public Tools.CustomTaskPane CustomTaskPane { get { return this.mTaskPane; } }
        private void OnTaskPaneVisibleChanged(object sender,EventArgs e) {
            //Event handler for task pane visible property changed
            Globals.ThisAddIn.Ribbon.Refresh("btnNav");
        }
        private void OnInspectorClose() {
            //Event handler for Inspector closing event
            if(this.mTaskPane != null) 
                Globals.ThisAddIn.CustomTaskPanes.Remove(this.mTaskPane);
            this.mTaskPane = null;
            
            Globals.ThisAddIn.InspectorWrappers.Remove(this.mInspector);
            ((Outlook.InspectorEvents_Event)this.mInspector).Close -= new Outlook.InspectorEvents_CloseEventHandler(OnInspectorClose);
            this.mInspector = null;
        }
        private void OnIssueCtlIssueSelected(object sender,EventArgs e) {
            //Event handler for issue selected in Issue Explorer
            Globals.ThisAddIn.Ribbon.Refresh();
        }
        private void OnIssueCtlError(object source,ControlErrorEventArgs e) {
            //Event handler for errors in Issue Explorer
            App.ReportError(e.Exception,true,LogLevel.Error);
            Globals.ThisAddIn.Ribbon.Refresh();
        }
    }
}
