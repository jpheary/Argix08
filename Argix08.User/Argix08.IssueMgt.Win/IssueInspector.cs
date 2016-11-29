using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Data;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;
using Argix.Windows.Printers;
using Outlook=Microsoft.Office.Interop.Outlook; 

namespace Argix.Customers {
    //
    public partial class IssueInspector :UserControl {
        //Members
        public static Outlook.Application OutlookApp=null;
        private Issue mIssue = null;
        private ContactDS mContactDS=null;
        private int mDateDaysBack = 90, mDateDaysForward = 14, mDateDaysSpread = 14;
        private Action[] mActions=null;

        public event EventHandler IssueChanged = null;
        public event ControlErrorEventHandler Error = null;

        #region Constants
        private const string MNU_NEW = "&New Action...";
        private const string MNU_PRINT = "&Print...";
        private const string MNU_REFRESH = "&Refresh";
        private const string MNU_ARRANGEBYDATE = "&Date";
        private const string MNU_CUT = "Cu&t";
        private const string MNU_COPY = "&Copy";
        private const string MNU_PASTE = "&Paste";
        private const string MNU_FIND = "&Search...";
        #endregion

        //Interface
        public IssueInspector() {
            //Constructor
            try {
                InitializeComponent();
                this.mContactDS = new ContactDS();
                #region Menu identities
                this.ctxActionsNew.Text = MNU_NEW;
                this.ctxActionsPrint.Text = MNU_PRINT;
                this.ctxRefresh.Text = MNU_REFRESH;
                this.mnuArrangeByDate.Text = this.ctxArrangeByDate.Text = MNU_ARRANGEBYDATE;
                #endregion
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new IssueInspector control instance.",ex); }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the current Argix.Customers.Issue object for this control.")]
        [ReadOnly(false)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.IssueInspector.CurrentIssue")]
        public Issue CurrentIssue { 
            get { return this.mIssue; }
            set {
                try {
                    this.mIssue = value;
                    Refresh();
                }
                catch(ControlException ex) { throw ex; }
                catch(Exception ex) { throw new ControlException("Unexpected error on setting the IssueInspector issue reference.",ex); }
                finally { setUserServices(); }
            }
        }
        private bool ShouldSerializeCurrentIssue() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the current Argix.Customers.Issue object for this control.")]
        [ReadOnly(false)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.IssueInspector.CurrentIssue")]
        new public void Refresh() {
            //Event handler for change in the associated issue
            try {
                this.Text = this.lblTitle.Text = "";
                this.ctlCompany.CompanySelectedValue = null;
                this.ctlContact.SelectedValue = null;
                if(this.mIssue != null) {
                    this.lblTitle.Text = this.mIssue.Type.Trim();
                    if(this.mIssue.CompanyName.Trim() != "All") {
                        this.lblTitle.Text += ": " + this.mIssue.CompanyName.Trim();
                        if(this.mIssue.StoreNumber > 0)
                            this.lblTitle.Text += " #" + this.mIssue.StoreNumber.ToString();
                    }
                    else {
                        if(this.mIssue.AgentNumber.Trim() != "All")
                            this.lblTitle.Text += ": Agent#" + this.mIssue.AgentNumber.Trim();
                        else
                            this.lblTitle.Text += ": All Agents";
                    }
                    if(this.mIssue.Subject.Trim().Length > 0) this.lblTitle.Text += " - " + this.mIssue.Subject.Trim();
                    this.ctlCompany.CompanyText = this.mIssue.CompanyName;
                    if(this.mIssue.DistrictNumber != null) {
                        this.ctlCompany.ScopeText = CompanyLocation.SCOPE_DISTRICTS;
                        this.ctlCompany.LocationText = this.mIssue.DistrictNumber;
                    }
                    else if(this.mIssue.RegionNumber != null) {
                        this.ctlCompany.ScopeText = CompanyLocation.SCOPE_REGIONS;
                        this.ctlCompany.LocationText = this.mIssue.RegionNumber;
                    }
                    else if(this.mIssue.AgentNumber != null) {
                        this.ctlCompany.ScopeText = CompanyLocation.SCOPE_AGENTS;
                        this.ctlCompany.LocationText = this.mIssue.AgentNumber;
                    }
                    else {
                        this.ctlCompany.ScopeText = CompanyLocation.SCOPE_STORES;
                        this.ctlCompany.LocationText = this.mIssue.StoreNumber.ToString();
                    }

                    if(this.mContactDS.ContactTable.Select("ID=" + this.mIssue.ContactID).Length == 0)
                        refreshContacts();
                    this.ctlContact.SelectedValue = this.mIssue.ContactID;
                }
                showDeliveryInfo(false);
                loadActions();
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error loading the selected issue in the IssueInspector control.",ex)); }
            finally { setUserServices(); }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the Max View display for this control.")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.Customers.IssueInspector.MaxView")]
        public bool MaxView { get { return this.chkMax.Checked; } set { this.chkMax.Checked = value; } }
        public void ResetMaxView() { this.chkMax.Checked = false; }
        protected bool ShouldSerializeMaxView() { return false; }

        public void Search(string searchText) {
            //Highlight search text
            if(searchText.Trim().Length > 0) {
                int index  = -1;
                do {
                    index = this.txtAction.Find(searchText,index+1,this.txtAction.Text.Length,RichTextBoxFinds.None);
                    if(index > -1) {
                        this.txtAction.Select(index,searchText.Length);
                        this.txtAction.SelectionBackColor = Color.Yellow;
                    }
                } while(index > -1);
            }
        }
        
        #region Accessors/Modifiers: [Members...]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CanSave { get { return (this.mIssue.ID == 0); } }
        public void Save() { CustomerProxy.UpdateIssue(this.mIssue); }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CanCut { get { return false; } }
        public void Cut() { }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CanCopy { get { return false; } }
        public void Copy() { }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CanPaste { get { return false; } }
        public void Paste() { }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CanAddAction { get { return this.ctxActionsNew.Enabled; } }
        public void AddAction() { this.ctxActionsNew.PerformClick(); }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CanPrint { get { return false; } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
            }
            catch(Exception ex) { reportError(ex); }
            finally { this.Cursor = Cursors.Default; }
        }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public bool CanPreview { get { return false; } }
        public void PrintPreview() {
            //Print preview this schedule
            try {
            }
            catch(Exception ex) { reportError(ex); }

        }
        #endregion
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Init controls
                this.dtpFrom.MinDate = this.dtpTo.MinDate = DateTime.Today.AddDays(-this.mDateDaysBack);
                this.dtpFrom.MaxDate = this.dtpTo.MaxDate = DateTime.Today.AddDays(this.mDateDaysForward);
                this.dtpTo.Value = DateTime.Today;
                if(this.mDateDaysBack >= this.mDateDaysSpread)
                    this.dtpFrom.Value = DateTime.Today.AddDays(-this.mDateDaysSpread);
                else
                    this.dtpFrom.Value = DateTime.Today.AddDays(-this.mDateDaysBack);
                this.btnPRO.Enabled = false;
                OnMaxViewChecked(null,EventArgs.Empty);
                refreshContacts();
                this.ctlContact.DataSource = this.mContactDS;
            }
            catch(ControlException ex) { reportError(ex); }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while loading the IssueInspector control",ex)); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnContactChanged(object source,ContactEventArgs e) {
            //Event handler for contact changed
            if(this.mIssue != null) {
                this.mIssue.ContactID = e.Contact.ID;
                CustomerProxy.UpdateIssue(this.mIssue);
                if(this.IssueChanged != null) this.IssueChanged(this.mIssue,EventArgs.Empty);
            }
        }
        private void OnDeliveryDateChanged(object sender,EventArgs e) {
            //Event handler for change in delivery dates
            try {
                //From date cannot exceed to date- from pushes to forward; to pushes from backward
                DateTimePicker dtp = (DateTimePicker)sender;
                if(dtp == this.dtpFrom) {
                    if(this.dtpFrom.Value.CompareTo(this.dtpTo.Value) > 0)
                        this.dtpTo.Value = this.dtpFrom.Value;
                    else if(this.dtpFrom.Value.CompareTo(this.dtpTo.Value.AddDays(-this.mDateDaysSpread)) < 0)
                        this.dtpTo.Value = this.dtpFrom.Value.AddDays(this.mDateDaysSpread);
                }
                else if(dtp == this.dtpTo) {
                    if(this.dtpTo.Value.CompareTo(this.dtpFrom.Value) < 0)
                        this.dtpFrom.Value = this.dtpTo.Value;
                    else if(this.dtpTo.Value.CompareTo(this.dtpFrom.Value.AddDays(this.mDateDaysSpread)) > 0)
                        this.dtpFrom.Value = this.dtpTo.Value.AddDays(-this.mDateDaysSpread);
                }
            }
            catch { }
            finally { setUserServices(); }
        }
        private void OnFindPRONumber(object sender,EventArgs e) {
            //Event handler for change in selected company
            try {
                dlgDeliveries dlg = new dlgDeliveries(this.mIssue.CompanyID,Convert.ToInt32(this.mIssue.StoreNumber),this.dtpFrom.Value,this.dtpTo.Value);
                dlg.Font = this.Font;
                if(dlg.ShowDialog() == DialogResult.OK) {
                    this.mIssue.ContactID = Convert.ToInt32(this.ctlContact.SelectedValue);
                    this.mIssue.OFD1FromDate = this.dtpFrom.Value;
                    this.mIssue.OFD1ToDate = this.dtpTo.Value; 
                    this.mIssue.PROID = dlg.PROID;
                    CustomerProxy.UpdateIssue(this.mIssue);
                    showDeliveryInfo(true);
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error while determining delivery information in the IssueInspector control.",ex)); }
        }
        private void OnTabSelected(object sender,TabControlEventArgs e) {
            //Event handler for change in tab selection
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.mIssue == null) return;
                switch(e.TabPage.Name) {
                    case "tabDetail":
                        break;
                    case "tabOSD":
                        ScanDS osdscans = CustomerProxy.GetOSDScans(this.mIssue.PROID);
                        this.oSDScanTableBindingSource.DataSource = osdscans;
                        if(this.grdPOD.Selected.Rows.Count > 0) {
                            long id = Convert.ToInt64(this.grdPOD.Selected.Rows[0].Cells["LabelSequenceNumber"].Value);
                            for(int i=0;i<this.grdOSD.Rows.Count;i++) {
                                if(Convert.ToInt64(this.grdOSD.Rows[i].Cells["LabelSequenceNumber"].Value) == id) {
                                    this.grdOSD.Rows[0].Selected = true;
                                    break;
                                }
                            }
                        }
                        break;
                    case "tabPOD":
                        ScanDS podscans = CustomerProxy.GetPODScans(this.mIssue.PROID);
                        this.pODScanTableBindingSource.DataSource = podscans;
                        if(this.grdOSD.Selected.Rows.Count > 0) {
                            long id = Convert.ToInt64(this.grdOSD.Selected.Rows[0].Cells["LabelSequenceNumber"].Value);
                            for(int i=0;i<this.grdPOD.Rows.Count;i++) {
                                if(Convert.ToInt64(this.grdPOD.Rows[i].Cells["LabelSequenceNumber"].Value) == id) {
                                    this.grdPOD.Rows[0].Selected = true;
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnActionSelected(object sender,EventArgs e) {
            //Event handler for change in selected action
            try {
                this.txtAction.Text = "";
                this.lsvAttachments.Items.Clear();
                if(this.lsvActions.SelectedItems.Count > 0) {
                    //Action
                    long actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);

                    //Show the attachments
                    Attachment[] attachments = CustomerProxy.GetAttachments(this.mIssue.ID,actionID);
                    this.lsvAttachments.Items.Clear();
                    for(int i = 0;i < attachments.Length;i++) {
                        this.lsvAttachments.Items.Add(attachments[i].ID.ToString(), attachments[i].Filename, -1);
                    }
                    this.lsvAttachments.View = View.List;
                    
                    //Show the selected action
                    Action[] actions = this.mActions;
                    int start=0;
                    bool go=false;
                    for(int i = 0;i < actions.Length;i++) {
                        Action action = actions[i];
                        if(!go && action.ID == actionID) go = true;
                        if(go) {
                            string header = action.Created.ToString("f") + "     " + action.UserID + ", " + action.TypeName;
                            this.txtAction.AppendText(header);
                            this.txtAction.Select(start,header.Length);
                            this.txtAction.SelectionFont = new Font(this.txtAction.Font,FontStyle.Bold);
                            this.txtAction.AppendText("\r\n\r\n");
                            this.txtAction.AppendText(action.Comment);
                            this.txtAction.AppendText("\r\n");
                            this.txtAction.AppendText("".PadRight(75,'-'));
                            this.txtAction.AppendText("\r\n");
                            start = this.txtAction.Text.Length;
                        }
                    }
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error on change of selected issue action in the IssueInspector control.",ex)); }
            finally { setUserServices(); };
        }
        private void OnMaxViewChecked(object sender,EventArgs e) {
            try {
                if(this.chkMax.Checked) this.scMain.Panel1.Hide(); else this.scMain.Panel1.Show();
                this.scMain.SplitterDistance = this.chkMax.Checked ? 0 : 168;
                this.scMain.IsSplitterFixed = this.chkMax.Checked;
                //this.chkMax.Text = this.chkMax.Checked ? "=" : "-";
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnRunningChecked(object sender,EventArgs e) {
            //Event handler for running checked
            OnActionSelected(null,EventArgs.Empty);
        }
        private void OnAttachmentSelected(object sender,EventArgs e) {
            //Event handler for attachment selected
            setUserServices();
        }
        private void OnAttachmentDoubleClick(object sender,EventArgs e) {
            //Event handler for attachment double-clicked
            if(this.btnOpen.Enabled) this.btnOpen.PerformClick();
        }
        private void OnControlError(object source,ControlErrorEventArgs e) {
            //Event handler for error from custom control
            reportError(e.Exception);
        }
        #region Grid Drag/Drop Events: OnDragOver(), OnDragDrop()
        private void OnDragOver(object sender,System.Windows.Forms.DragEventArgs e) {
            //Event handler for dragging over the grid
            try {
                //Enable appropriate drag drop effect
                DataObject data = (DataObject)e.Data;
                if(data.GetDataPresent(DataFormats.FileDrop) || data.GetDataPresent("FileGroupDescriptor")) {
                    e.Effect = this.btnAttach.Enabled ? DragDropEffects.Copy : DragDropEffects.None;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnDragDrop(object sender,System.Windows.Forms.DragEventArgs e) {
            //Event handler for dropping onto the listview
            try {
                DataObject data = (DataObject)e.Data;
                if(data.GetDataPresent(DataFormats.FileDrop)) {
                    //Local file
                    string[] names = (string[])data.GetData(DataFormats.FileDrop);
                    if(names.Length > 0) {
                        //Save attachment
                        string name = new System.IO.FileInfo(names[0]).Name; 
                        System.IO.FileStream stream = new System.IO.FileStream(names[0],System.IO.FileMode.Open);
                        byte[] bytes = new byte[stream.Length];
                        //stream.Position = 0;
                        stream.Read(bytes,0,(int)stream.Length);
                        long actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);
                        CustomerProxy.CreateIssueAttachment(name,bytes,actionID);
                        stream.Close();
                        loadActions();
                    }
                }
                else if(data.GetDataPresent("FileGroupDescriptor")) {
                    //Outlook attachment
                    //Set the position within the current stream to the beginning of the file name
                    //return the file name in the fileName variable
                    System.IO.MemoryStream stream = (System.IO.MemoryStream)data.GetData("FileGroupDescriptor");
                    stream.Seek(76, System.IO.SeekOrigin.Begin);
                    byte[] fileName = new byte[256];
                    stream.Read(fileName, 0, 256);
                    System.Text.Encoding encoding = System.Text.Encoding.ASCII;
                    string name = encoding.GetString(fileName);
                    name = name.TrimEnd('\0');
                    stream.Close();

                    //Save attachment
                    stream = (System.IO.MemoryStream)e.Data.GetData("FileContents");
                    byte[] bytes = new byte[stream.Length];
                    //stream.Position = 0;
                    stream.Read(bytes,0,(int)stream.Length);
                    long actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);
                    CustomerProxy.CreateIssueAttachment(name,bytes,actionID);
                    stream.Close();
                    loadActions();
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        #endregion
        #region User Services: OnMenuClick(), OnToolbarItemClicked()
        private void OnMenuClick(object sender,EventArgs e) {
            //Event handler for mneu item clicked
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                switch(menu.Text) {
                    case MNU_NEW:
                        Action action = new Action();
                        action.IssueID = this.mIssue.ID;
                        action.TypeID = 0;
                        action.UserID = Environment.UserName;
                        action.Created = DateTime.Now;
                        action.Comment = "";
                        action.Attachments = 0;
                        dlgAction dlgNA = new dlgAction(action,this.mIssue);
                        dlgNA.Font = this.Font;
                        if(dlgNA.ShowDialog(this) == DialogResult.OK) {
                            //Add the action to the issue
                            CustomerProxy.CreateIssueAction(action.TypeID, action.IssueID, action.UserID, action.Comment);
                            if(this.IssueChanged != null) this.IssueChanged(this.mIssue,EventArgs.Empty);
                            loadActions();
                        }
                        break;
                    case MNU_PRINT:
                        WinPrinter2 wp = new WinPrinter2("", this.txtAction.Font);
                        string doc = "Issue Type: \t" + this.mIssue.Type + "\r\nSubject: \t\t" + this.mIssue.Subject + "\r\nContact: \t\t" + this.mIssue.ContactName + "\r\n" + "\r\nCompany: \t" + this.mIssue.CompanyName + "\r\nStore#: \t\t" + this.mIssue.StoreNumber.ToString() + "\r\nAgent#: \t" + this.mIssue.AgentNumber + "\r\nZone: \t\t" + this.mIssue.Zone;
                        doc += "\r\n\r\n\r\n";
                        Action[] actions = CustomerProxy.GetIssueActions(this.mIssue.ID);
                        for(int i = 0;i < actions.Length;i++) {
                            doc += actions[i].Created.ToString("f") + "     " + actions[i].UserID + ", " + actions[i].TypeName;
                            doc += "\r\n\r\n";
                            doc += actions[i].Comment;
                            doc += "\r\n";
                            doc += "".PadRight(75,'-');
                            doc += "\r\n";
                        }
                        wp.Print(this.mIssue.Subject,doc,true);
                        break;
                    case MNU_REFRESH:       Refresh(); break;
                    case MNU_ARRANGEBYDATE: break;
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error setting IssueInspector menu services.",ex)); }
        }
        private void OnToolbarItemClicked(object sender,ToolStripItemClickedEventArgs e) {
            //Toolbar handler - forward to main menu handler
            long actionID=0;
            Action action=null;
            System.IO.FileStream fs=null;
            byte[] bytes=null;
            try {
                //Get the current action
                actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);
                switch(e.ClickedItem.Name) {
                    case "btnNew":  this.ctxActionsNew.PerformClick(); break;
                    case "btnPrint": this.ctxActionsPrint.PerformClick();  break;
                    case "btnRefresh": this.ctxRefresh.PerformClick(); break;
                    case "btnOpen":
                        //Open the selected attachment
                        int attachmentID = Convert.ToInt32(this.lsvAttachments.SelectedItems[0].Name);
                        bytes = CustomerProxy.GetAttachment(attachmentID);
                        string file = CustomerProxy.TempFolder + this.lsvAttachments.SelectedItems[0].Text.Trim();
                        fs = new System.IO.FileStream(file,System.IO.FileMode.OpenOrCreate,System.IO.FileAccess.Write);
                        System.IO.BinaryWriter writer = new System.IO.BinaryWriter(fs);
                        writer.Write(bytes);
                        writer.Flush();
                        writer.Close();
                        System.Diagnostics.Process.Start(file);
                        break;
                    case "btnAttach":
                        //Save an attachment to the selected action
                        OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.AddExtension = true;
                        dlgOpen.Filter = "All Files (*.*) | *.*";
                        dlgOpen.FilterIndex = 0;
                        dlgOpen.Title = "Select Attachment to Save...";
                        dlgOpen.FileName = "";
                        if(dlgOpen.ShowDialog(this)==DialogResult.OK) {
                            string name = new System.IO.FileInfo(dlgOpen.FileName).Name; 
                            fs = new System.IO.FileStream(dlgOpen.FileName,System.IO.FileMode.Open,System.IO.FileAccess.Read);
                            System.IO.BinaryReader reader = new System.IO.BinaryReader(fs);
                            bytes = reader.ReadBytes((int)fs.Length);
                            reader.Close();
                            bool created = CustomerProxy.CreateIssueAttachment(name,bytes,actionID);
                            loadActions();
                        }
                        break;
                    case "btnSend": 
                        if(this.lsvActions.SelectedItems.Count > 0) {                            
                            //Create new mail item
                            if(OutlookApp == null) return;
                            Outlook.MailItem item = (Outlook.MailItem)OutlookApp.CreateItem(Outlook.OlItemType.olMailItem);
                            item.Subject = this.mIssue.Subject;
                            item.Body = action.Comment;
                            item.To = CustomerProxy.GetContact(this.mIssue.ContactID).Email;
                            item.Display(false);
                        }
                        break;
                }
            }
            catch(Exception ex) { reportError(new ControlException("Unexpected error in IssueInspector.",ex)); }
            finally { if(fs != null) fs.Close(); }
        }
        #endregion
        #region Local Services: showDeliveryInfo(), refreshContacts(), loadActions(), setUserServices(), reportError()
        private void showDeliveryInfo(bool showCartons) {
            //Clear and validate
            this.grdDelivery.SelectedObject = null;
            if(this.mIssue != null) {
                //Display delivery info
                DeliveryDS delivery = CustomerProxy.GetDelivery(this.mIssue.CompanyID,this.mIssue.StoreNumber,this.mIssue.OFD1FromDate,this.mIssue.OFD1ToDate,this.mIssue.PROID);
                if(delivery.DeliveryTable.Rows.Count > 0)
                    this.grdDelivery.SelectedObject = new DeliveryInfo(delivery.DeliveryTable[0]);
                if(showCartons) {
                    ScanDS osdscans = CustomerProxy.GetOSDScans(this.mIssue.PROID);
                    this.oSDScanTableBindingSource.DataSource = osdscans;
                    ScanDS podscans = CustomerProxy.GetPODScans(this.mIssue.PROID);
                    this.pODScanTableBindingSource.DataSource = podscans;
                }
            }
        }
        private void refreshContacts() {
            //
            this.mContactDS.Clear();
            this.mContactDS.Merge(CustomerProxy.GetContacts());
        }
        private void loadActions() {
            //Event handler for change in actions collection
            //Load actions for this issue
            this.lsvActions.Groups.Clear();
            this.lsvActions.Items.Clear();
            if(this.mIssue != null) {
                //Create action listitems sorted by date/time
                Action[] actions = CustomerProxy.GetIssueActions(this.mIssue.ID);
                this.mActions = actions;
                for(int i = 0;i < actions.Length;i++) {
                    //Add attachment symbol as required 
                    //Tag is used to enable attachement to newest action only
                    Action action = actions[i];
                    ListViewItem item = this.lsvActions.Items.Add(action.ID.ToString(),action.UserID,(action.Attachments > 0 ? 0 : -1));
                    item.Tag = i.ToString();
                    
                    //Assign to listitem group
                    DateTime created = action.Created;
                    bool useYesterday = DateTime.Today.DayOfWeek != DayOfWeek.Monday;
                    if(created.CompareTo(DateTime.Today) >= 0) {
                        this.lsvActions.Groups.Add("Today","Today");
                        item.SubItems.Add(created.ToString("ddd HH:mm"));
                        item.Group = this.lsvActions.Groups["Today"];
                    }
                    else if(useYesterday && created.CompareTo(DateTime.Today.AddDays(-1)) >= 0) {
                        this.lsvActions.Groups.Add("Yesterday","Yesterday");
                        item.SubItems.Add(created.ToString("ddd HH:mm"));
                        item.Group = this.lsvActions.Groups["Yesterday"];
                    }
                    else if(created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek)) >= 0) {
                        this.lsvActions.Groups.Add("This Week","This Week");
                        item.SubItems.Add(created.ToString("ddd HH:mm"));
                        item.Group = this.lsvActions.Groups["This Week"];
                    }
                    else if(created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek - 7)) >= 0) {
                        this.lsvActions.Groups.Add("Last Week","Last Week");
                        item.SubItems.Add(created.ToString("ddd MM/dd HH:mm"));
                        item.Group = this.lsvActions.Groups["Last Week"];
                    }
                    else {
                        this.lsvActions.Groups.Add("Other","Other");
                        item.SubItems.Add(created.ToString("ddd MM/dd/yyyy HH:mm"));
                        item.Group = this.lsvActions.Groups["Other"];
                    }
                }
            }
            if(this.lsvActions.Items.Count > 0) 
                this.lsvActions.Items[0].Selected = true;
            else
                OnActionSelected(null,EventArgs.Empty);
        }
        private void setUserServices() {
            //Set user services
            try {
                this.ctxActionsNew.Enabled = this.btnNew.Enabled = (this.mIssue != null && this.mIssue.ID > 0);
                this.ctxActionsPrint.Enabled = this.btnPrint.Enabled = this.mIssue != null;
                this.ctxRefresh.Enabled = this.btnRefresh.Enabled = this.mIssue != null;
                this.ctxArrangeBy.Enabled = this.btnArrangeBy.Enabled = this.lsvActions.Focused;
                this.btnOpen.Enabled = this.mIssue != null && this.lsvAttachments.Focused && this.lsvAttachments.SelectedItems.Count > 0;
                this.btnAttach.Enabled = (this.mIssue != null && this.mIssue.ID > 0 && this.lsvActions.SelectedItems.Count > 0 && this.lsvActions.SelectedItems[0].Tag.ToString() == "0");
                this.btnSend.Enabled = this.mIssue != null && OutlookApp != null && this.lsvActions.SelectedItems.Count > 0;
                this.btnPRO.Enabled = (this.mIssue != null && this.mIssue.ID > 0);  // && !this.mIssue.IsClosed);
            }
            catch { }
        }
        private void reportError(Exception ex) {
            //Error notification
            if(this.Error != null) this.Error(this,new ControlErrorEventArgs(ex));
        }
        #endregion
    }

    public class DeliveryInfo {
        //Class members
        DeliveryDS.DeliveryTableRow mDelivery=null;

        //Interface
        public DeliveryInfo(DeliveryDS.DeliveryTableRow d) { this.mDelivery = d; }
        [Category("Delivery"),Description("CBOL.")]
        public string CBOL { get { return (!this.mDelivery.IsCBOLNull() ? this.mDelivery.CBOL : ""); } }
        [Category("Delivery"),Description("CPRO.")]
        public long CPRO { get { return (!this.mDelivery.IsCPRONumberNull() ? this.mDelivery.CPRONumber : 0); } }
        [Category("Delivery"),Description("OFD1.")]
        public string OFD1 { get { return (!this.mDelivery.IsOFD1Null() ? this.mDelivery.OFD1.ToString("MM-dd-yyyy") : ""); } }
        [Category("Delivery"),Description("POD.")]
        public string POD { get { return (!this.mDelivery.IsPodDateNull() ? this.mDelivery.PodDate.ToString("MM-dd-yyyy") + " " + this.mDelivery.PodTime.ToString("HH:mm") : ""); } }
        [Category("Delivery"),Description("EST. DEL.")]
        public string EstimatedDelivery { get { return (!this.mDelivery.IsShouldBeDeliveredOnNull() ? this.mDelivery.ShouldBeDeliveredOn.ToString("MM-dd-yyyy") : ""); } }
        [Category("Delivery"),Description("CTNS.")]
        public int CTNS { get { return (!this.mDelivery.IsCartonsSortedNull() ? this.mDelivery.CartonsSorted : 0); } }
        [Category("Delivery"),Description("DEL.WIN.")]
        public string DeliveryWindow { get { return (!this.mDelivery.IsWindowStartTimeNull() ? this.mDelivery.WindowStartTime.ToString("HH:mm") + " - " + this.mDelivery.WindowEndTime.ToString("HH:mm") : ""); } }
        [Category("Delivery"),Description("TLs.")]
        public string TL { get { return (!this.mDelivery.IsTLSNull() ? this.mDelivery.TLS : ""); } }

        [Category("OS&D"),Description("OS&D Match.")]
        public int OSDMatch { get { return (!this.mDelivery.IsOSDCartonsMatchNull() ? this.mDelivery.OSDCartonsMatch : 0); } }
        [Category("OS&D"),Description("OS&D Short.")]
        public int OSDShort { get { return (!this.mDelivery.IsOSDCartonsShortNull() ? this.mDelivery.OSDCartonsShort : 0); } }
        [Category("OS&D"),Description("OS&D Over.")]
        public int OSDOver { get { return (!this.mDelivery.IsOSDCartonsOverNull() ? this.mDelivery.OSDCartonsOver : 0); } }
        [Category("OS&D"),Description("OS&D Misroute.")]
        public int OSDMisroute { get { return (!this.mDelivery.IsOSDCartonsMisrouteNull() ? this.mDelivery.OSDCartonsMisroute : 0); } }
        [Category("OS&D"),Description("OS&D Damage.")]
        public int OSDDamage { get { return (!this.mDelivery.IsOSDCartonsDamagedNull() ? this.mDelivery.OSDCartonsDamaged : 0); } }
        [Category("OS&D"),Description("OS&D Scan.")]
        public int OSDScan { get { return (!this.mDelivery.IsOSDCartonsScannedNull() ? this.mDelivery.OSDCartonsScanned : 0); } }
        [Category("OS&D"),Description("OS&D Manual.")]
        public int OSDManual { get { return (!this.mDelivery.IsOSDCartonsManualNull() ? this.mDelivery.OSDCartonsManual : 0); } }

        [Category("POD"),Description("POD Match.")]
        public int PODMatch { get { return (!this.mDelivery.IsPODCartonsMatchNull() ? this.mDelivery.PODCartonsMatch : 0); } }
        [Category("POD"),Description("POD Short.")]
        public int PODShort { get { return (!this.mDelivery.IsPODCartonsShortNull() ? this.mDelivery.PODCartonsShort : 0); } }
        [Category("POD"),Description("POD Over.")]
        public int PODOver { get { return (!this.mDelivery.IsPODCartonsOverNull() ? this.mDelivery.PODCartonsOver : 0); } }
        [Category("POD"),Description("POD Misroute.")]
        public int PODMisroute { get { return (!this.mDelivery.IsPODCartonsMisrouteNull() ? this.mDelivery.PODCartonsMisroute : 0); } }
        [Category("POD"),Description("POD Damage.")]
        public int PODDamage { get { return (!this.mDelivery.IsPODCartonsDamagedNull() ? this.mDelivery.PODCartonsDamaged : 0); } }
        [Category("POD"),Description("POD Scan.")]
        public int PODScan { get { return (!this.mDelivery.IsPODCartonsScannedNull() ? this.mDelivery.PODCartonsScanned : 0); } }
        [Category("POD"),Description("POD Manual.")]
        public int PODManual { get { return (!this.mDelivery.IsPODCartonsManualNull() ? this.mDelivery.PODCartonsManual : 0); } }
    }
}
