using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Office=Microsoft.Office.Core;
using Outlook=Microsoft.Office.Interop.Outlook;
using Tools=Microsoft.Office.Tools;
using Argix.Enterprise;

namespace Argix.CustomerSvc {
    //11/11/09- change to load IssueSelector issues only when control is visible (this way
    //no load error notifications until using Issue Mgt Ribbon for the opened email item)
    [ComVisible(true)]
    public class IssueMgtRibbon: Office.IRibbonExtensibility {
        //Members
        private Office.IRibbonUI mRibbonUI=null;  //Ref to th esingle ribbon

        private const string RIBBON_XML = "Argix.IssueMgtRibbon.xml";
        private const string RIBBONID_MAILREAD = "Microsoft.Outlook.Mail.Read";
        private const string RIBBONID_MAILCOMPOSE = "Microsoft.Outlook.Mail.Compose";
        
        //Interface
        public IssueMgtRibbon() { }
        public void Refresh() { if(this.mRibbonUI != null) this.mRibbonUI.Invalidate(); }
        public void Refresh(string controlID) { if(this.mRibbonUI != null) this.mRibbonUI.InvalidateControl(controlID); }
        #region IRibbonExtensibility: GetCustomUI()
        public string GetCustomUI(string ribbonID) {
            //Return ribbon (definition) xml for the appropriate inspector
            //NOTE: This method is called once for each type of inspector (the first time the inspector is called)
            string xml = string.Empty;
            try {
                //Add custom ribbon for mail messages only
                switch(ribbonID) {
                    case RIBBONID_MAILREAD:
                    case RIBBONID_MAILCOMPOSE:
                        xml = GetResourceText(RIBBON_XML); 
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while returning custom UI for the Issue Mgt ribbon extension.",ex),true,LogLevel.Error); }
            return xml;
        }
        #endregion
        #region Ribbon Callbacks: OnLoad(), OnNavClicked(), OnNavGetPressed(), OnFileAction(), OnViewAction(), OnSetServices()
        public void OnLoad(Office.IRibbonUI ribbonUI) {
            //Event handler for ribbon load event
            //NOTE: This method is called once for the first inspector of any type
            try {
                this.mRibbonUI = ribbonUI;
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while loading the Issue Mgt ribbon extension.",ex),true,LogLevel.Error); }
            finally { this.mRibbonUI.Invalidate(); }
        }
        public void OnNavClicked(Office.IRibbonControl control,bool pressed) {
            //Event handler for nav toggle button action event
            try {
                Outlook.Inspector inspector = (Outlook.Inspector)control.Context;
                if(Globals.ThisAddIn.InspectorWrappers.ContainsKey(inspector)) {
                    InspectorWrapper iw = Globals.ThisAddIn.InspectorWrappers[inspector];
                    Tools.CustomTaskPane ctp = iw.CustomTaskPane;
                    if(ctp != null) {
                        ctp.Visible = pressed;
                        if(ctp.Visible) {
                            IssueSelector s = (IssueSelector)ctp.Control;
                            s.Refresh();
                        }
                    }
                }
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while handling button click in the Issue Mgt ribbon extension.",ex),true,LogLevel.Error); }
            finally { this.mRibbonUI.Invalidate(); }
        }
        public bool OnNavGetPressed(Office.IRibbonControl control) {
            //Event handler for toggl button getPressed event
            bool visible=false;
            try {
            Outlook.Inspector inspector = (Outlook.Inspector)control.Context;
            if(Globals.ThisAddIn.InspectorWrappers.ContainsKey(inspector)) {
                InspectorWrapper iw = Globals.ThisAddIn.InspectorWrappers[inspector];
                Tools.CustomTaskPane ctp = iw.CustomTaskPane;
                if(ctp != null)
                    visible = ctp.Visible;
            }
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while handling button pressed state in the Issue Mgt ribbon extension.",ex),true,LogLevel.Error); }
            finally { this.mRibbonUI.Invalidate(); }
            return visible;
        }
        public void OnFileAction(Office.IRibbonControl control) {
            //Event handler for view contacts clicked
            Outlook.Inspector insp=null;
            Outlook.MailItem m=null;
            Outlook.Attachments attachments=null;
            Issue issue=null;
            try {
                Tools.CustomTaskPane ctp=null;
                IssueSelector ie=null;
                Outlook.Inspector inspector = (Outlook.Inspector)control.Context;
                if(Globals.ThisAddIn.InspectorWrappers.ContainsKey(inspector)) {
                    InspectorWrapper iw = Globals.ThisAddIn.InspectorWrappers[inspector];
                    ctp = iw.CustomTaskPane;
                    if(ctp != null) {
                        ie = (IssueSelector)ctp.Control;
                    }
                }
                switch(control.Id) {
                    case "btnNew":
                        //Add the current outlook message as an issue action in a new issue
                        //Show explorer if not visible
                        ctp.Visible = true;

                        //Get the current email item and any associated attachments
                        insp = Globals.ThisAddIn.Application.ActiveInspector();
                        m = (Outlook.MailItem)insp.CurrentItem;
                        attachments = m.Attachments;

                        //Get a new issue; allow user interaction
                        issue = CRGFactory.GetIssue(0);
                        issue.Subject = m.Subject;
                        issue.Actions.ActionTable[0].Comment = m.Body;
                        if(new dlgIssue(issue).ShowDialog() == DialogResult.OK) {
                            //Save (create) the new issue
                            issue.Save();
                            if(ie != null) ie.SelectedID = issue.ID;
                        }
                        break;
                    case "btnAdd":
                        //Add the current outlook message as an issue action
                        issue = ie != null ? ie.SelectedIssue : null;
                        if(issue == null) {
                            App.ReportError(new ApplicationException("Could not determine selected issue."), true, LogLevel.Error);
                        }
                        else {
                            //Get the current email item and any associated attachments
                            insp = Globals.ThisAddIn.Application.ActiveInspector();
                            m = (Outlook.MailItem)insp.CurrentItem;
                            attachments = m.Attachments;

                            //Get a new action and add attachments
                            Issue.Action action = issue.Item(0);
                            action.Comment = m.Body;
                            foreach(Outlook.Attachment a in attachments) {
                                Issue.Attachment attachment = action.Item(0);
                                attachment.Filename = CRGFactory.TempFolder + a.FileName;
                                action.Add(attachment);
                            }

                            //Allow user interaction
                            if(new dlgAction(action).ShowDialog() == DialogResult.OK) {
                                //Save selected attachments to TEMP dir
                                foreach(Outlook.Attachment a in attachments) {
                                    try {
                                        if(action.Item(CRGFactory.TempFolder + a.FileName) != null)
                                            a.SaveAsFile(CRGFactory.TempFolder + a.FileName);
                                    }
                                    catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while saving mail attachments to temporary storage; continuing...",ex),true,LogLevel.Error); }
                                }

                                //Save the action
                                issue.Add(action);
                                ie.SelectedID = issue.ID;
                            }
                        }
                        break;
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while handling File actions in the Issue Mgt ribbon extension.",ex),true,LogLevel.Error); }
            finally { this.mRibbonUI.Invalidate(); }
        }
        public void OnViewAction(Office.IRibbonControl control) {
            //Event handler for view contacts clicked
            try {
                IssueSelector ie=null;
                Outlook.Inspector inspector = (Outlook.Inspector)control.Context;
                if(Globals.ThisAddIn.InspectorWrappers.ContainsKey(inspector)) {
                    InspectorWrapper iw = Globals.ThisAddIn.InspectorWrappers[inspector];
                    Tools.CustomTaskPane ctp = iw.CustomTaskPane;
                    if(ctp != null) {
                        ie = (IssueSelector)ctp.Control;
                    }
                }
                switch(control.Id) {
                    case "btnRef": if(ie != null) ie.Refresh(); break;
                    case "btnRefC": if(ie != null) ie.RefreshCache(); break;
                }
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error while handling View actions in the Issue Mgt ribbon extension.",ex),true,LogLevel.Error); }
            finally { this.mRibbonUI.Invalidate(); }
        }
        public bool OnSetServices(Office.IRibbonControl control) {
            //Event handler for contacts enabled event
            bool enabled = false;
            try {
                Tools.CustomTaskPane ctp=null;
                IssueSelector ie=null;
                Outlook.Inspector inspector = (Outlook.Inspector)control.Context;
                if(Globals.ThisAddIn.InspectorWrappers.ContainsKey(inspector)) {
                    InspectorWrapper iw = Globals.ThisAddIn.InspectorWrappers[inspector];
                    ctp = iw.CustomTaskPane;
                    if(ctp != null) {
                        ie = (IssueSelector)ctp.Control;
                    }
                }
                switch(control.Id) {
                    case "btnNav": enabled = true; break;
                    case "btnNew": enabled = true; break;
                    case "btnAdd": enabled = (ctp != null && ctp.Visible && ie != null && ie.SelectedID > 0); break;
                    case "btnRef": enabled = (ctp != null && ctp.Visible); break;
                    case "btnRefC": enabled = (ctp != null && ctp.Visible); break;
                    case "btnCon": enabled = true; break;
                }
            }
            catch { }
            return enabled;
        }
        #endregion
        #region Local Services: GetResourceText()
        private static string GetResourceText(string resourceName) {
            //
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for(int i = 0;i < resourceNames.Length;++i) {
                if(string.Compare(resourceName,resourceNames[i],StringComparison.OrdinalIgnoreCase) == 0) {
                    using(StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i]))) {
                        if(resourceReader != null) {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }
        #endregion
    }
}
