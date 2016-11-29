using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Data;
using Argix.Enterprise;
using Argix.Windows;

namespace Argix.CustomerSvc {
    //
    //TODO: Modal?: How about a draft feature (dropdown shows Open, History, Draft)
    public partial class IssueSelector:UserControl {
        //Members
        private IssueDS mIssueDS=null;
        private UltraGridSvc mGridSvcIssues=null;
        public event EventHandler IssueSelected=null;
        public event ControlErrorEventHandler Error=null;

        //Interface
        public IssueSelector() {
            //Constructor
            try {
                //Init control
                InitializeComponent();
                this.mIssueDS = new IssueDS();
                this.mGridSvcIssues = new UltraGridSvc(this.grdIssues);
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error while creating new IssueSelector control instance.",ex)); }
        }
        #region Accessors/Modifiers: [Members...]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the selected issue.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueSelector.SelectedIssue")]
        [Localizable(false)]
        public Issue SelectedIssue {
            get {
                Issue issue=null;
                if(this.grdIssues.Selected.Rows.Count > 0) {
                    long id = Convert.ToInt32(this.grdIssues.Selected.Rows[0].Cells["ID"].Value);
                    issue = CRGFactory.GetIssue(id);
                }
                return issue;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Gets or sets the index of the selected issue.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueSelector.SelectedID")]
        [Localizable(false)]
        public long SelectedID {
            get {
                long id=0;
                if(this.grdIssues.Selected.Rows.Count > 0)
                    id = Convert.ToInt32(this.grdIssues.Selected.Rows[0].Cells["ID"].Value);
                return id;
            }
            set {
                for(int i = 0;i < this.grdIssues.Rows.Count;i++) {
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

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Refresh issues.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueSelector.Refresh")]
        [Localizable(false)]
        public override void Refresh() {
            this.mGridSvcIssues.CaptureState();
            this.mIssueDS.Clear();
            this.mIssueDS.Merge(CRGFactory.GetIssues());
            this.mGridSvcIssues.RestoreState();
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Description("Refresh Issue Mgt cache.")]
        [HelpKeywordAttribute("Argix.CustomerSvc.IssueExplorer.RefreshCache")]
        [Localizable(false)]
        public void RefreshCache() { CRGFactory.RefreshCache(); }
        #endregion
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Init controls
                this.grdIssues.DataSource = this.mIssueDS;
                this.grdIssues.DataBind();
                this.mIssueDS.Clear();
                //this.mIssueDS.Merge(CRGFactory.GetIssues());
                this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
            }
            catch(Exception ex) { reportError(new ApplicationException("Unexpected error while loading the IssueSelector control",ex)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for after selection changes
            try {
                if(this.IssueSelected != null) this.IssueSelected(this,EventArgs.Empty);
            }
            catch { }
        }
        #region Local Services: reportError()
        private void reportError(Exception ex) {
            //Error notification
            if(this.Error != null) this.Error(this,new ControlErrorEventArgs(ex));
        }
        #endregion
    }
}
