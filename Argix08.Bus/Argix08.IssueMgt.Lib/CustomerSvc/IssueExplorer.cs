using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Data;
using Argix.Enterprise;
using Argix.Windows;

namespace Argix.CustomerSvc {
    //
    public partial class IssueExplorer:UserControl {
        //Members
        private Issue mIssue=null,mIssueH=null;
        private IssueDS mIssues=null,mIssueHistory=null,mIssueSearch=null;
        private UltraGridSvc mGridSvcIssues=null;
        private bool mReadOnly=false;
        private System.Windows.Forms.Timer mTimer=null;
        private BackgroundWorker mWorker=null;
        private DateTime mLastIssueUpdateTime=DateTime.Now,mLastNewIssueTime=DateTime.Now;
        private System.Collections.Hashtable mOldItems=null;
        private MemoryStream mColumnHeaders=null,mColumnFilters=null;
        #region Menu Constants
        private const string MNU_NEW = "&New Issue...";
        private const string MNU_OPEN = "&Open...";
        private const string MNU_SAVE = "&Save...";
        private const string MNU_SAVEAS = "Save &As...";
        private const string MNU_SETUP = "Page Set&up...";
        private const string MNU_PRINT = "&Print...";
        private const string MNU_PREVIEW = "Print Pre&view...";
        private const string MNU_REFRESH = "&Refresh";
        private const string MNU_REFRESHCACHE = "Refresh &Cache";
        private const string MNU_AUTOREFRESHON = "&Auto Refresh On";
        private const string MNU_CONTACTS = "&Contacts";
        private const string MNU_PROPERTIES = "&Properties";
        #endregion
        public event EventHandler IssueSelected=null;
        public event ControlErrorEventHandler Error=null;
        public event EventHandler ServiceStatesChanged=null;
        public event NewIssueEventHandler NewIssue=null;

        [System.Runtime.InteropServices.DllImport("user32.dll",EntryPoint="FindWindow",CallingConvention=System.Runtime.InteropServices.CallingConvention.StdCall,CharSet=System.Runtime.InteropServices.CharSet.Unicode,SetLastError=true)]
        public static extern IntPtr FindWindow(string lpClassName,string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll",EntryPoint="ShowWindow",CallingConvention=System.Runtime.InteropServices.CallingConvention.StdCall,CharSet=System.Runtime.InteropServices.CharSet.Unicode,SetLastError=true)]
        public static extern bool ShowWindow(IntPtr handle,Int32 nCmd);
        [System.Runtime.InteropServices.DllImport("user32",CharSet=System.Runtime.InteropServices.CharSet.Ansi,SetLastError=true,ExactSpelling=true)]
        public static extern bool SetForegroundWindow(IntPtr hwnd); 
        public const int SW_RESTORE = 9;

        //Interface
        public IssueExplorer() {
            //Constructor
            try {
                //Init control
                InitializeComponent();
                #region Menu identities
                this.ctxNew.Text = MNU_NEW;
                this.ctxOpen.Text = MNU_OPEN;
                this.ctxSaveAs.Text = MNU_SAVEAS;
                this.ctxPageSetup.Text = MNU_SETUP;
                this.ctxPrint.Text = MNU_PRINT;
                this.ctxPreview.Text = MNU_PREVIEW;
                this.ctxRefresh.Text = MNU_REFRESH;
                this.ctxRefreshCache.Text = MNU_REFRESHCACHE;
                this.mnuCtxAutoRefreshOn.Text = MNU_AUTOREFRESHON;
                this.ctxContacts.Text = MNU_CONTACTS;
                this.ctxProperties.Text = MNU_PROPERTIES;
                #endregion
                this.mIssues = new IssueDS();
                this.mIssueHistory = new IssueDS();
                this.mIssueSearch = new IssueDS();
                this.mGridSvcIssues = new UltraGridSvc(this.grdIssues);
                CRGFactory.IssuesChanged += new EventHandler(OnIssuesChanged);
                //CRGFactory.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);                
                this.mTimer = new System.Windows.Forms.Timer();
                this.mTimer.Interval = 15000;
                this.mTimer.Tick += new EventHandler(OnTick);
                this.mWorker = new BackgroundWorker();
                this.mWorker.DoWork += new DoWorkEventHandler(OnDoWork);
                this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnRunWorkerCompleted);
                this.mOldItems = new System.Collections.Hashtable();
                this.mColumnHeaders = new MemoryStream();
                this.mColumnFilters = new MemoryStream();
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error while creating new IssueExplorer control instance.",ex)); }
        }
        public void StartAuto() { this.mTimer.Start(); this.mnuCtxAutoRefreshOn.Checked = true; }
        public void StopAuto() { this.mTimer.Stop(); this.mnuCtxAutoRefreshOn.Checked = false; }
        #region Accessors/Modifiers: [Members...]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the selected issue.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.SelectedIssue")]
        [Localizable(false)]
        public Issue SelectedIssue { get { return this.mIssue; } }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the index of the selected issue.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.SelectedID")]
        [Localizable(false)]
        public long SelectedID {
            get {
                long id=0;
                if(this.grdIssues.Selected.Rows.Count > 0)
                    id = Convert.ToInt32(this.grdIssues.Selected.Rows[0].Cells["ID"].Value);
                return id;
            }
            set {
                for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                    long id = Convert.ToInt32(this.grdIssues.Rows[i].Cells["ID"].Value);
                    if(id == value) {
                        this.grdIssues.Rows[i].Selected = true;
                        this.grdIssues.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                        OnGridSelectionChanged(this.grdIssues,null);
                        break;
                    }
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the visible property of the ToolStrip control.")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.ToolStripVisible")]
        public bool ToolStripVisible { get { return this.tlsCtl.Visible; } set { this.tlsCtl.Visible = value; } }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Gets or sets the context menu of the UltraGrid control.")]
        [DefaultValue(typeof(System.Boolean),null)]
        [ReadOnly(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Localizable(false)]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.ContextMenuStrip")]
        public override ContextMenuStrip ContextMenuStrip { get { return this.grdIssues.ContextMenuStrip; } set { this.grdIssues.ContextMenuStrip = value; } }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the read only state.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.ReadOnly")]
        [Localizable(false)]
        public bool ReadOnly { get { return this.mReadOnly; } set { this.mReadOnly = value; } }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the state of the new issue service.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.CanNew")]
        [Localizable(false)]
        public bool CanNew { get { return this.ctxNew.Enabled; } }
        public void New() { this.ctxNew.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the state of the open issue service.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.CanOpen")]
        [Localizable(false)]
        public bool CanOpen { get { return this.ctxOpen.Enabled; } }
        public void Open() { this.ctxOpen.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the state of the save issues service.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.CanSaveAs")]
        [Localizable(false)]
        public bool CanSaveAs { get { return this.ctxSaveAs.Enabled; } }
        public void SaveAs() { this.ctxSaveAs.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the state of the print page setup service.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.CanPageSetup")]
        [Localizable(false)]
        public bool CanPageSetup { get { return this.ctxPageSetup.Enabled; } }
        public void PageSetup() { this.ctxPageSetup.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the state of the print service.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.CanPrint")]
        [Localizable(false)]
        public bool CanPrint { get { return this.ctxPrint.Enabled; } }
        public void Print() { this.ctxPrint.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the state of the print preview service.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.CanPreview")]
        [Localizable(false)]
        public bool CanPreview { get { return this.ctxPreview.Enabled; } }
        public void Preview() { this.ctxPreview.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the state of the search service.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.CanSearch")]
        [Localizable(false)]
        public bool CanSearch { get { return this.cboSearch.Enabled; } }
        public void Search() { OnSearch(this.cboSearch, new KeyPressEventArgs((char)Keys.Enter)); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets the current issue search string.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.SearchText")]
        [Localizable(false)]
        public string SearchText { get { return this.cboView.Text == "Search Results" ? this.cboSearch.Text.Trim() : ""; } }
        
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Refresh issues.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.Refresh")]
        [Localizable(false)]
        public override void Refresh() { this.ctxRefresh.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Refresh Issue Mgt cache.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.RefreshCache")]
        [Localizable(false)]
        public void RefreshCache() { this.ctxRefreshCache.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Issue Mgt properties.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.Properties")]
        [Localizable(false)]
        public void Properties() { this.ctxProperties.PerformClick(); }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Last time new issues were posted.")]
        [DefaultValue(typeof(System.Boolean),"False")]
        [ReadOnly(false)]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.LastNewIssueTime")]
        [Localizable(false)]
        public DateTime LastNewIssueTime { get { return this.mLastNewIssueTime; } set { this.mLastNewIssueTime = value; } }
        private bool ShouldSerializeLastNewIssueTime() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Issue grid column header positions.")]
        //[DefaultValue(typeof(System.IO.MemoryStream),"")]
        //[ReadOnly(false)]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.ColumnHeaders")]
        [Localizable(false)]
        public MemoryStream ColumnHeaders {
            get { return this.mColumnHeaders; }
            set {
                if(value != null) {
                    this.mColumnHeaders = value;
                    try {
                        this.mColumnHeaders.Seek(0,SeekOrigin.Begin);
                        this.grdIssues.DisplayLayout.Load(this.mColumnHeaders,PropertyCategories.SortedColumns);
                    }
                    catch { }
                }
            }
        }
        private bool ShouldSerializeColumnHeaders() { return false; }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Issue grid column filters.")]
        //[DefaultValue(typeof(System.IO.MemoryStream),"")]
        //[ReadOnly(false)]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.ColumnFilters")]
        [Localizable(false)]
        public MemoryStream ColumnFilters {
            get { return this.mColumnFilters; }
            set {
                if(value != null) {
                    this.mColumnFilters = value;
                    try {
                        this.mColumnFilters.Seek(0,SeekOrigin.Begin);
                        this.grdIssues.DisplayLayout.Load(this.mColumnFilters,PropertyCategories.ColumnFilters);
                        this.grdIssues.DisplayLayout.RefreshFilters();
                    }
                    catch { }
                }
            }
        }
        private bool ShouldSerializeColumnFilters() { return false; }
        #endregion
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Init controls
                this.grdIssues.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdIssues.DisplayLayout.Save(this.mColumnHeaders,PropertyCategories.SortedColumns);
                this.grdIssues.DisplayLayout.Save(this.mColumnFilters,PropertyCategories.ColumnFilters);
                this.cboView.SelectedIndex = 0;
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error while loading the IssueExplorer control",ex)); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnViewChanged(object sender,EventArgs e) {
            //Event handler for change in combobox view selection
            try {
                switch(this.cboView.Text) {
                    case "Search Results":
                        this.grdIssues.DataSource = this.mIssueSearch;
                        this.grdIssues.DataBind();
                        this.grdIssues.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                        break;
                    case "Issue History":
                        this.grdIssues.DataSource = this.mIssueHistory;
                        this.grdIssues.DataBind();
                        this.grdIssues.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                        this.ctxRefresh.PerformClick();
                        break;
                    default:
                        Issue issue = this.mIssue;
                        this.grdIssues.DataSource = this.mIssues;
                        this.grdIssues.DataBind();
                        this.grdIssues.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        try {
                            this.mColumnFilters.Seek(0,SeekOrigin.Begin);
                            this.grdIssues.DisplayLayout.Load(this.mColumnFilters,PropertyCategories.ColumnFilters);
                            this.grdIssues.DisplayLayout.RefreshFilters();
                        }
                        catch { }
                        this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                        this.ctxRefresh.PerformClick();
                        if(issue != null) {
                            for(int i = 0;i < this.grdIssues.Rows.VisibleRowCount;i++) {
                                if(Convert.ToInt32(this.grdIssues.Rows[i].Cells["ID"].Value) == issue.ID) {
                                    this.grdIssues.Rows[i].Selected = true;
                                    this.grdIssues.Rows[i].Activate();
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); }
        }
        private void OnIssuesChanged(object sender,EventArgs e) {
            //Event handler for change in issues
            this.ctxRefresh.PerformClick();
        }
        #region Grid Support: OnGridBeforeRowFilterDropDownPopulate(), OnColumnPositionChanged(), OnRowFilterChanged(), OnInitializeRow(), OnGridSelectionChanged(), GridMouseDown(), OnGridDoubleClick()
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                //e.ValueList.ValueListItems.Remove(1);
            }
            catch { }
        }
        private void OnColumnPositionChanged(object sender,AfterColPosChangedEventArgs e) {
            try {
                this.grdIssues.DisplayLayout.Save(this.mColumnHeaders,PropertyCategories.SortedColumns);
            }
            catch { }
        }
        private void OnRowFilterChanged(object sender,AfterRowFilterChangedEventArgs e) {
            try {
                if(this.cboView.Text == "Current Issues")
                    this.grdIssues.DisplayLayout.Save(this.mColumnFilters,PropertyCategories.ColumnFilters);
            }
            catch { }
        }
        private void OnInitializeRow(object sender,InitializeRowEventArgs e) {
            //Event handler for row intialize event
            try {
                //Bold rows of new issues/actions
                int id = Convert.ToInt32(e.Row.Cells["ID"].Value);
                DateTime dt1 = Convert.ToDateTime(e.Row.Cells["LastActionCreated"].Value);
                if(!this.mOldItems.ContainsKey(id)) {
                    //Not viewed or startup (i.e. collection is empty)
                    if(dt1.CompareTo(this.mLastNewIssueTime) > 0)
                        e.Row.CellAppearance.FontData.Bold = DefaultableBoolean.True;
                }
                else {
                    DateTime dt2 = Convert.ToDateTime(this.mOldItems[id]);
                    if(dt1.CompareTo(dt2) > 0) {
                        //LastActionCreated is different then last time viewed
                        e.Row.CellAppearance.FontData.Bold = DefaultableBoolean.True;
                    }
                }
            }
            catch { }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for after selection changes
            this.Cursor = Cursors.WaitCursor;
            try {
                //Update the selected issue if not looking at another issue view
                this.mIssue = null;
                UltraGrid grid = (UltraGrid)sender;
                if(grid.Selected.Rows.Count > 0) {
                    switch(this.cboView.Text) {
                        case "Search Results":
                            this.mIssue = CRGFactory.GetIssue(Convert.ToInt32(grid.Selected.Rows[0].Cells["ID"].Value));
                            break;
                        case "Issue History":
                            this.mIssue = CRGFactory.GetIssue(Convert.ToInt32(grid.Selected.Rows[0].Cells["ID"].Value));
                            break;
                        default:
                            this.mIssue = CRGFactory.GetIssue(Convert.ToInt32(grid.Selected.Rows[0].Cells["ID"].Value));
                            this.mIssueH = this.mIssue; 
                            break;
                    }
                    try {
                        //Unbold viewed issues/actions
                        grid.Selected.Rows[0].CellAppearance.FontData.Bold = DefaultableBoolean.False;
                        int id = Convert.ToInt32(grid.Selected.Rows[0].Cells["ID"].Value);
                        DateTime dt1 = Convert.ToDateTime(grid.Selected.Rows[0].Cells["LastActionCreated"].Value);
                        if(this.mOldItems.ContainsKey(id)) 
                            this.mOldItems[id] = dt1;
                        else
                            this.mOldItems.Add(id,dt1);
                    }
                    catch { }
                }
                if(this.IssueSelected != null) this.IssueSelected(this,EventArgs.Empty);
            }
            catch { }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(uiElement != null) {
                    object oContext = uiElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) grid.Selected.Rows.Clear();
                            oRow.Selected = true;
                            oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch { }
            finally { setUserServices(); }
        }
        private void OnGridDoubleClick(object sender,EventArgs e) {
            //Event handler for grid double-clicked
            try {
                if(this.grdIssues.ContextMenuStrip != null && this.ctxOpen.Enabled) this.ctxOpen.PerformClick();
            }
            catch { }
        }
        #endregion
        #region User Services: OnMenuClick(), OnToolbarItemClicked(), OnViewChanged()
        private void OnMenuClick(object sender,System.EventArgs e) {
            //Event handler for menu selection
            Issue issue=null;
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                switch(menu.Text) {
                    case MNU_NEW:
                        issue = CRGFactory.GetIssue(0);
                        dlgIssue dlg = new dlgIssue(issue);
                        dlg.Font = this.Font;
                        if(dlg.ShowDialog() == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            if(issue.Save()) {
                                for(int i = 0;i < this.grdIssues.Rows.Count;i++) {
                                    int id = Convert.ToInt32(this.grdIssues.Rows[i].Cells["ID"].Value);
                                    if(id == issue.ID) {
                                        this.grdIssues.Rows[i].Selected = true;
                                        this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                                        OnGridSelectionChanged(this.grdIssues,null);
                                        break;
                                    }
                                }
                            }
                        }
                        break;
                    case MNU_OPEN:      break;
                    case MNU_SAVE:      break;
                    case MNU_SAVEAS:
                        
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Export Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Issues As...";
                        dlgSave.FileName = "";
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            if(dlgSave.FileName.EndsWith("xls")) {
                                new Argix.ExcelFormat().Transform(this.mIssues,dlgSave.FileName);
                            }
                            else {
                                this.mIssues.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
                            }
                        }
                        break;
                    case MNU_SETUP: UltraGridPrinter.PageSettings(); break;
                    case MNU_PRINT: UltraGridPrinter.Print(this.grdIssues,this.grdIssues.Text,true); break;
                    case MNU_PREVIEW: UltraGridPrinter.PrintPreview(this.grdIssues,this.grdIssues.Text); break;
                    case MNU_REFRESH:
                        this.Cursor = Cursors.WaitCursor;
                        this.mGridSvcIssues.CaptureState("ID");
                        switch(this.cboView.Text) {
                            case "Search Results": break;
                            case "Issue History":
                                this.mIssueHistory.Clear();
                                this.mIssueHistory.Merge(CRGFactory.IssueHistory(this.mIssueH));
                                break;
                            case "Draft Issues":    break;
                            default:
                                DataSet ds = CRGFactory.GetIssues();
                                lock(this.mIssues) {
                                    this.mIssues.Clear();
                                    this.mIssues.Merge(ds);
                                }
                                postIssueUpdates();
                                break;
                        }
                        this.mGridSvcIssues.RestoreState();
                        break;
                    case MNU_REFRESHCACHE: EnterpriseFactory.RefreshCache(); CRGFactory.RefreshCache(); break;
                    case MNU_AUTOREFRESHON: 
                            this.mnuCtxAutoRefreshOn.Checked = !this.mnuCtxAutoRefreshOn.Checked;
                            if(this.mnuCtxAutoRefreshOn.Checked) StartAuto(); else StopAuto();
                            break;
                    case MNU_CONTACTS:
                        winContacts winC = new winContacts();
                        winC.Font = this.Font;
                        winC.ShowDialog();
                        break;
                    case MNU_PROPERTIES: break;
                }
            }
            catch(Exception ex) { reportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnToolbarItemClicked(object sender,ToolStripItemClickedEventArgs e) {
            //Event handler for toolbar item clicked
            try {
                switch(e.ClickedItem.Name) {
                    case "btnNew": this.ctxNew.PerformClick(); break;
                    case "btnOpen": this.ctxOpen.PerformClick(); break;
                    case "btnSaveAs": this.ctxSaveAs.PerformClick(); break;
                    case "btnSetup": this.ctxPageSetup.PerformClick(); break;
                    case "btnPreview": this.ctxPreview.PerformClick(); break;
                    case "btnPrint": this.ctxPrint.PerformClick(); break;
                    case "btnRefresh": this.ctxRefresh.PerformClick(); break;
                    case "btnContacts": this.ctxContacts.PerformClick(); break;
                    case "btnProperties": this.ctxProperties.PerformClick(); break;
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void OnSearch(object sender,KeyPressEventArgs e) {
            //Search
            try {
                this.mIssueSearch.Clear();
                if(e.KeyChar == (char)Keys.Enter && this.cboSearch.Text.Trim().Length > 0) {
                    //this.cboSearch.Items.Add(this.cboSearch.Text);
                    this.mIssueSearch.Merge(CRGFactory.SearchIssues(this.cboSearch.Text));
                    this.cboView.SelectedItem = "Search Results";
                    OnViewChanged(this.cboView,EventArgs.Empty);
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        #endregion
        #region Local Services: setUserServices(), reportError()
        private void setUserServices() {
            //Set user services
            try {
                this.cboView.Enabled = ((this.cboView.Text=="Current Issues" && this.mIssue!=null) || (this.cboView.Text=="Issue History") || this.cboView.Text=="Search Results");
                this.ctxNew.Enabled = this.btnNew.Enabled = !this.mReadOnly && (this.cboView.Text=="Current Issues");
                this.ctxOpen.Enabled = this.btnOpen.Enabled = false;
                this.ctxSaveAs.Enabled = this.btnSaveAs.Enabled = this.grdIssues.Focused && this.grdIssues.Rows.Count > 0;
                this.ctxPageSetup.Enabled = this.btnSetup.Enabled = true;
                this.ctxPrint.Enabled = this.btnPrint.Enabled = this.grdIssues.Focused && this.grdIssues.Rows.Count > 0;
                this.ctxPreview.Enabled = this.btnPreview.Enabled = this.grdIssues.Focused && this.grdIssues.Rows.Count > 0;
                this.ctxRefresh.Enabled = this.btnRefresh.Enabled = true;
                this.ctxRefreshCache.Enabled = true;
                this.ctxContacts.Enabled = this.btnContacts.Enabled = true;
                this.ctxProperties.Enabled = this.btnProperties.Enabled = false;
                this.cboSearch.Enabled = true;
            }
            catch { }
            finally { if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this,new EventArgs()); }
        }
        private void reportError(Exception ex) {
            //Error notification
            if(this.Error != null) this.Error(this,new ControlErrorEventArgs(ex));
        }
        #endregion
        #region Auto Refresh Services: OnTick(), OnDoWork(), OnRunWorkerCompleted()
        private void OnTick(object sender,EventArgs e) {
            //Event handler for timer tick event
            try { if(!this.mWorker.IsBusy) this.mWorker.RunWorkerAsync(); } catch { }
        }
        private void OnDoWork(object sender,DoWorkEventArgs e) {
            //
            try { e.Result = CRGFactory.GetIssues(); } catch { }
        }
        private void OnRunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
            //
            try {
                DataSet ds=null;
                if(this.InvokeRequired) {
                    this.Invoke(new RunWorkerCompletedEventHandler(OnRunWorkerCompleted),new object[] { sender,e });
                }
                else {
                        ds = (DataSet)e.Result;
                        this.mGridSvcIssues.CaptureState("ID");
                        lock(this.mIssues) {
                            this.mIssues.Clear();
                            this.mIssues.Merge(ds);
                        }
                        this.mGridSvcIssues.RestoreState();
                        postIssueUpdates();
                }
            }
            catch { }
        }
        private void postIssueUpdates() {
            //Check for new issue actions and fire an event for each one found
            DateTime lastUpdated = this.mLastIssueUpdateTime;
            IssueDS issues = new IssueDS();
            issues.Merge(this.mIssues);
            for(int i=0;i<issues.IssueTable.Rows.Count;i++) {
                //Find issues with LastAction that has not been posted yet
                //Skip 'New' actions and actions from creator
                IssueDS.IssueTableRow issue = issues.IssueTable[i];
                if(issue.LastActionCreated.CompareTo(lastUpdated) > 0 && issue.LastActionDescription != "New" && issue.LastActionUserID != Environment.UserName) {
                    //Post a NewIssue event with an issue instance that includes the last action
                    IssueDS actions = new IssueDS();
                    actions.ActionTable.AddActionTableRow(issue.LastActionID,(byte)0,issue.ID,issue.LastActionUserID,issue.LastActionCreated,issue.LastActionDescription);
                    Issue _issue = new Issue(issue,actions,null);
                    if(this.NewIssue != null) this.NewIssue(this,new NewIssueEventArgs(_issue));

                    //Update mLastIssueUpdateTime time to keep notification to once for an updated issue
                    if(issue.LastActionCreated.CompareTo(this.mLastIssueUpdateTime) > 0)
                        this.mLastIssueUpdateTime = issue.LastActionCreated;
                }
            }
        }
        #endregion
    }
}
