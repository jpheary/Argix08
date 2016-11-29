//	File:	ultragridsvcs.cs
//	Author:	J. Heary
//	Date:	01/03/08
//	Desc:	Encapsultes services for Infragistics UltraGrid.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Windows {
    //
    public class UltraGridSvc {
        //Class members
        private static PageSettings Settings = null;
        private static bool ShowDialog = true;

        //Instance members
        private UltraGrid mGrid=null;
        private TextBox mText=null;
        private Label mLabel=null;
        private System.Windows.Forms.ToolTip mToolTip=null;

        //Constants

        //Interface
        public UltraGridSvc(UltraGrid grid) : this(grid,null,null) { }
        public UltraGridSvc(UltraGrid grid,TextBox textBox) : this(grid,textBox,null) { }
        public UltraGridSvc(UltraGrid grid,TextBox textBox,Label label) {
            //Constructor
            try {
                //Set grid and class attributes
                this.mGrid = grid;
                this.mText = textBox;
                this.mLabel = label;
                UltraGridSvc.Settings = new PageSettings();
                UltraGridSvc.Settings.Landscape = true;
                this.mToolTip = new System.Windows.Forms.ToolTip();

                //Initialize appearance to override designer values
                this.mGrid.DisplayLayout.CaptionAppearance.BackColor = SystemColors.InactiveCaption;
                this.mGrid.DisplayLayout.CaptionAppearance.ForeColor = SystemColors.InactiveCaptionText;
                foreach(Control ctl in this.mGrid.Controls) {
                    if(ctl.BackColor == SystemColors.ActiveCaption)
                        ctl.BackColor = SystemColors.InactiveCaption;
                    if(ctl.ForeColor == SystemColors.ActiveCaptionText)
                        ctl.ForeColor = SystemColors.InactiveCaptionText;
                }

                //Set event handlers
                this.mGrid.AfterSortChange += new Infragistics.Win.UltraWinGrid.BandEventHandler(this.OnSortColumnChanged);
                this.mGrid.Enter += new System.EventHandler(this.OnGridEnter);
                this.mGrid.Leave += new System.EventHandler(this.OnGridLeave);
                this.mGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyDown);
                if(this.mText!=null) {
                    this.mText.Enter += new System.EventHandler(this.OnSearchBoxEnter);
                    this.mText.TextChanged += new System.EventHandler(this.OnSearchValueChanged);
                }
            }
            catch(Exception ex) { throw ex; }
        }

        #region Event handlers for common look and feel
        private void OnGridEnter(object sender,System.EventArgs e) {
            //Event handler for grid received focus
            try {
                UltraGrid oGrid = (UltraGrid)sender;
                oGrid.DisplayLayout.CaptionAppearance.BackColor = SystemColors.ActiveCaption;
                oGrid.DisplayLayout.CaptionAppearance.ForeColor = SystemColors.ActiveCaptionText;
                foreach(Control ctl in oGrid.Controls) {
                    //Only participating controls
                    if(ctl.BackColor == SystemColors.InactiveCaption)
                        ctl.BackColor = SystemColors.ActiveCaption;
                    if(ctl.ForeColor == SystemColors.InactiveCaptionText)
                        ctl.ForeColor = SystemColors.ActiveCaptionText;
                }
            }
            catch(Exception) { }
        }
        private void OnGridLeave(object sender,System.EventArgs e) {
            //Event handler for grid lost focus
            try {
                UltraGrid oGrid = (UltraGrid)sender;
                oGrid.DisplayLayout.CaptionAppearance.BackColor = SystemColors.InactiveCaption;
                oGrid.DisplayLayout.CaptionAppearance.ForeColor = SystemColors.InactiveCaptionText;
                foreach(Control ctl in oGrid.Controls) {
                    //Only participating controls
                    if(ctl.BackColor == SystemColors.ActiveCaption)
                        ctl.BackColor = SystemColors.InactiveCaption;
                    if(ctl.ForeColor == SystemColors.ActiveCaptionText)
                        ctl.ForeColor = SystemColors.InactiveCaptionText;
                }
            }
            catch(Exception) { }
        }
        private void OnSortColumnChanged(object sender,Infragistics.Win.UltraWinGrid.BandEventArgs e) {
            //Event handler for change in grid sort column
            try {
                UltraGrid oGrid = (UltraGrid)sender;
                oGrid.Tag  = "";
                if(this.mLabel != null) this.mLabel.Text = "";
                for(int i=0;i<e.Band.Columns.Count;i++) {
                    if(e.Band.Columns[i].SortIndicator!=SortIndicator.None) {
                        if(this.mLabel != null) {
                            this.mLabel.Text = e.Band.Columns[i].Header.Caption;
                            this.mLabel.Text += (e.Band.Columns[i].SortIndicator==SortIndicator.Ascending) ? " (asc)" : " (desc)";
                        }
                        oGrid.Tag  = e.Band.Columns[i].Key;
                        oGrid.DisplayLayout.Bands[0].ScrollTipField = e.Band.Columns[i].Key;
                        this.mToolTip.SetToolTip(this.mText,"Search by " + e.Band.Columns[i].Header.Caption);
                        this.mText.Focus();
                        this.mText.SelectAll();
                    }
                }
            }
            catch(Exception) { }
        }
        private void OnGridKeyDown(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key down
            try {
                //Provide multi-select keyboard support using CTL-SPACEBAR
                UltraGrid oGrid = (UltraGrid)sender;
                if(!e.Control && e.KeyValue==32)
                    e.Handled = true;
                else if(e.Control && e.KeyValue==32) {
                    oGrid.ActiveRow.Selected = (!oGrid.ActiveRow.Selected);
                    e.Handled = true;
                }
                else
                    e.Handled = false;
            }
            catch(Exception) { }
        }
        protected void OnSearchBoxEnter(object sender,System.EventArgs e) {
            //Event handler for text box change event- provides common search code
            TextBox oTxtBox = (TextBox)sender;
            try {
                this.mText.SelectionStart = 0;
                oTxtBox.SelectAll();
            }
            catch(Exception) {
                oTxtBox.Text = "";
                oTxtBox.Focus();
            }
        }
        private void OnSearchValueChanged(object sender,System.EventArgs e) {
            //Event handler for change in search text value
            try {
                //Get specifics for search word and grid
                FindRow(0,this.mGrid.Tag.ToString(),this.mText.Text);
                this.mText.Focus();
                this.mText.SelectionStart = this.mText.Text.Length;
            }
            catch(Exception) { }
        }
        #endregion
        #region General services
        public void FindRow(int bandIndex,string colKey,string searchWord) {
            //Event handler for change in search text value
            UltraGridRow oRow=null,oRowSimiliar=null,oRowMatch=null;
            string sCellText="";
            long lCellValue=0,lSearchValue=0;
            int iLword=0,iL=0,iRows=0,i=0,j=0;
            bool bASC=true,bIsNumeric=false,bHigher=false;

            Cursor.Current = Cursors.WaitCursor;
            try {
                //Get specifics for search word and grid
                iLword = searchWord.Length;
                iRows = this.mGrid.Rows.VisibleRowCount;
                if(this.mGrid.DisplayLayout.Bands[bandIndex].Columns.Exists(colKey) && searchWord.Length>0) {
                    //Initial search conditions
                    bASC = (this.mGrid.DisplayLayout.Bands[bandIndex].Columns[colKey].SortIndicator==SortIndicator.Ascending);
                    bIsNumeric = (this.mGrid.DisplayLayout.Bands[bandIndex].Columns[colKey].DataType==Type.GetType("System.Int32"));
                    i = 0;
                    while(i<iRows) {
                        //Get next row, cell value, and cell length
                        oRow = this.mGrid.Rows[this.mGrid.Rows.GetRowAtVisibleIndex(i).Index];
                        if(bIsNumeric) {
                            lCellValue = Convert.ToInt64(oRow.Cells[colKey].Value);
                            try {
                                lSearchValue = Convert.ToInt64(searchWord);
                            }
                            catch(FormatException) {
                                lSearchValue = 0;
                            }
                            if(bASC) {
                                if(lSearchValue==lCellValue)
                                    oRowMatch = oRow;
                                else if(lSearchValue>lCellValue)
                                    oRowSimiliar = oRow;
                            }
                            else {
                                if(lSearchValue==lCellValue)
                                    oRowMatch = oRow;
                                else if(lSearchValue<lCellValue)
                                    oRowSimiliar = oRow;
                            }
                        }
                        else {
                            sCellText = oRow.Cells[colKey].Value.ToString();
                            iL = sCellText.Length;
                            if(iL>0) {
                                //Compare a substring of the cell text with the search word
                                for(j=1;j<=iL;j++) {
                                    if(sCellText.Substring(0,j).ToUpper()==searchWord.Substring(0,j).ToUpper()) {
                                        if(j==iLword) {
                                            //Exact match
                                            oRowMatch = oRow;
                                            break;
                                        }
                                        else {
                                            if(j==iL) {
                                                //Close match
                                                oRowSimiliar = oRow;
                                                break;
                                            }
                                        }
                                    }
                                    else {
                                        //Is search word alphabetically higher than cell?
                                        if(bASC)
                                            bHigher = (searchWord.ToUpper().ToCharArray()[j-1]>sCellText.ToUpper().ToCharArray()[j-1]);
                                        else
                                            bHigher = (searchWord.ToUpper().ToCharArray()[j-1]<sCellText.ToUpper().ToCharArray()[j-1]);
                                        if(bHigher)
                                            oRowSimiliar = oRow;
                                        break;
                                    }
                                }
                            }
                        }
                        if(oRowMatch!=null) break;
                        i++;
                    }
                    //Select match or closest row
                    if(iRows>0 && oRowMatch==null)
                        oRowMatch = (oRowSimiliar!=null) ? oRowSimiliar : this.mGrid.Rows[this.mGrid.Rows.GetRowAtVisibleIndex(0).Index];
                    this.mGrid.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(oRowMatch);
                    this.mGrid.ActiveRow = oRowMatch;
                    this.mGrid.ActiveRow.Selected = true;
                }
            }
            catch(Exception ex) {
                throw ex;
            }
            finally {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion
        #region Printing services
        public static void PageSettings() {
            //Read and modify document page settings
            PageSetupDialog dlg=null;
            try {
                //Display page settings dialog
                dlg = new PageSetupDialog();
                dlg.AllowMargins = dlg.AllowOrientation = dlg.AllowPaper = true;
                dlg.AllowPrinter = false;		//PrinterSettings=null disables AllowPrinter
                dlg.PrinterSettings = null;
                dlg.PageSettings = UltraGridSvc.Settings;
                dlg.ShowDialog();
            }
            catch(Exception ex) { throw ex; }
        }
        public static void PrintPreview(UltraGrid grid) {
            //Print preview the grid data
            try {
                //Display print preview dialog
                grid.InitializePrintPreview += new InitializePrintPreviewEventHandler(UltraGridSvc.OnGridInitializePrintPreview);
                grid.InitializePrint += new InitializePrintEventHandler(UltraGridSvc.OnGridInitializePrint);
                grid.BeforePrint += new BeforePrintEventHandler(UltraGridSvc.OnGridBeforePrint);
                grid.InitializeLogicalPrintPage += new InitializeLogicalPrintPageEventHandler(UltraGridSvc.OnGridInitializeLogicalPrintPage);
                grid.PrintPreview(RowPropertyCategories.Hidden);
            }
            catch(Exception ex) { throw ex; }
        }
        public static void Print(UltraGrid grid,bool showDialog) {
            //Print the grid rows
            try {
                //Remove and reset event handlers for this grid object, then start print process
                grid.InitializePrintPreview -= new InitializePrintPreviewEventHandler(UltraGridSvc.OnGridInitializePrintPreview);
                grid.InitializePrintPreview += new InitializePrintPreviewEventHandler(UltraGridSvc.OnGridInitializePrintPreview);
                grid.InitializePrint -= new InitializePrintEventHandler(UltraGridSvc.OnGridInitializePrint);
                grid.InitializePrint += new InitializePrintEventHandler(UltraGridSvc.OnGridInitializePrint);
                grid.BeforePrint -= new BeforePrintEventHandler(UltraGridSvc.OnGridBeforePrint);
                grid.BeforePrint += new BeforePrintEventHandler(UltraGridSvc.OnGridBeforePrint);
                grid.InitializeLogicalPrintPage -= new InitializeLogicalPrintPageEventHandler(UltraGridSvc.OnGridInitializeLogicalPrintPage);
                grid.InitializeLogicalPrintPage += new InitializeLogicalPrintPageEventHandler(UltraGridSvc.OnGridInitializeLogicalPrintPage);
                UltraGridSvc.ShowDialog = showDialog;
                grid.Print(RowPropertyCategories.Hidden);
            }
            catch(Exception ex) { throw ex; }
        }

        #region Grid Printing Event Handlers
        private static void OnGridInitializePrintPreview(object sender,Infragistics.Win.UltraWinGrid.CancelablePrintPreviewEventArgs e) {
            //Event handler when a print preview is first initiated
            UltraGrid oGrid = (UltraGrid)sender;
            try {
                //Set print preview dialog attributes
                e.PrintPreviewSettings.DialogLeft = SystemInformation.WorkingArea.X + 192;
                e.PrintPreviewSettings.DialogTop = SystemInformation.WorkingArea.Y + 192;
                e.PrintPreviewSettings.DialogWidth = 672;	//SystemInformation.WorkingArea.Width;
                e.PrintPreviewSettings.DialogHeight = 480;	//SystemInformation.WorkingArea.Height;
                e.PrintPreviewSettings.Zoom = 1.0;

                //Set layout attributes
                setLogicalPageLayout(oGrid,e.DefaultLogicalPageLayoutInfo);

                //Set print document attributes including defualt page settings
                e.PrintDocument.DocumentName = oGrid.Text;
                e.PrintDocument.DefaultPageSettings = UltraGridSvc.Settings;
            }
            catch(Exception ex) { throw ex; }
        }
        private static void OnGridInitializePrint(object sender,Infragistics.Win.UltraWinGrid.CancelablePrintEventArgs e) {
            //Event handler when a print job is first initiated
            UltraGrid oGrid = (UltraGrid)sender;
            try {
                //Set layout attributes
                setLogicalPageLayout(oGrid,e.DefaultLogicalPageLayoutInfo);

                //Set print document attributes including defualt page settings
                e.PrintDocument.DocumentName = oGrid.Text;
                e.PrintDocument.DefaultPageSettings = UltraGridSvc.Settings;
            }
            catch(Exception ex) { throw ex; }
        }
        private static void OnGridBeforePrint(object sender,Infragistics.Win.UltraWinGrid.CancelablePrintEventArgs e) {
            //Event handler when after a print job has been initaited and configured by
            //a user, just before data is sent to the printer
            PrintDialog dlg=null;
            DialogResult res;

            try {
                if(UltraGridSvc.ShowDialog) {
                    //Configure printer properties page
                    dlg = new PrintDialog();
                    dlg.AllowPrintToFile = dlg.AllowSelection = dlg.AllowSomePages = false;
                    dlg.Document = e.PrintDocument;
                    //dlg.PrinterSettings = e.PrintDocument.PrinterSettings;
                    res = dlg.ShowDialog();
                    if(res==DialogResult.OK) {
                        //Retain current settings and print
                        e.Cancel = false;
                    }
                    else
                        e.Cancel = true;
                }
                else
                    e.Cancel = false;
            }
            catch(Exception ex) { throw ex; }
        }
        private static void OnGridInitializeLogicalPrintPage(object sender,Infragistics.Win.UltraWinGrid.CancelableLogicalPrintPageEventArgs e) {
            //Event handler that occurs when a logical print page is being formatted
            //for printing or previewing
            try {
                //Configure page
            }
            catch(Exception ex) { throw ex; }
        }
        #endregion

        private static void setLogicalPageLayout(UltraGrid oGrid,LogicalPageLayoutInfo layoutInfo) {
            //Set layout attributes
            try {
                layoutInfo.FitWidthToPages = 0;
                layoutInfo.ClippingOverride = ClippingOverride.Yes;
                layoutInfo.PageHeader = oGrid.Text + "\t" + DateTime.Now.ToString("MM-dd-yyyy HH:mm tt");
                layoutInfo.PageHeaderHeight = 48;
                layoutInfo.PageHeaderAppearance.FontData.SizeInPoints = 10;
                layoutInfo.PageHeaderAppearance.TextHAlign = HAlign.Left;
                layoutInfo.PageHeaderAppearance.TextVAlign = VAlign.Middle;
                layoutInfo.PageHeaderBorderStyle = UIElementBorderStyle.None;
                layoutInfo.PageFooter = "Page <#>.";
                layoutInfo.PageFooterHeight= 24;
                layoutInfo.PageFooterAppearance.TextHAlign = HAlign.Center;
                layoutInfo.PageFooterAppearance.FontData.Italic = DefaultableBoolean.True;
                layoutInfo.PageFooterBorderStyle = UIElementBorderStyle.None;
            }
            catch(Exception ex) {
                throw ex;
            }
        }
        #endregion
    }
    public class UltraGridPrinter {
        //Members
        private static PageSettings Settings = null;
        private static bool ShowDialog = true;
        private static string ReportHeader = "";

        //Constants
        //Events
        //Interface
        static UltraGridPrinter() {
            //Constructor
            try {
                //Set members
                UltraGridPrinter.Settings = new PageSettings();
                UltraGridPrinter.Settings.Landscape = true;
                UltraGridPrinter.Settings.Margins.Left = 50;
                UltraGridPrinter.Settings.Margins.Right = 50;
                UltraGridPrinter.Settings.Margins.Top = 50;
                UltraGridPrinter.Settings.Margins.Bottom = 50;
            }
            catch(Exception ex) { throw ex; }
        }
        public static DialogResult PageSettings() {
            //Read and modify document page settings
            PageSetupDialog dlg = null;
            try {
                //Display page settings dialog
                dlg = new PageSetupDialog();
                dlg.AllowMargins = dlg.AllowOrientation = dlg.AllowPaper = true;
                dlg.AllowPrinter = false;		//PrinterSettings=null disables AllowPrinter
                //dlg.PrinterSettings = null;

                dlg.PageSettings = UltraGridPrinter.Settings;
                return dlg.ShowDialog();
            }
            catch(Exception ex) { throw ex; }
        }
        public static void PrintPreview(UltraGrid grid,string reportCaption) {
            //Print preview the grid data
            try {
                //Display print preview dialog
                grid.InitializePrintPreview += new InitializePrintPreviewEventHandler(UltraGridPrinter.OnGridInitializePrintPreview);
                grid.InitializePrint += new InitializePrintEventHandler(UltraGridPrinter.OnGridInitializePrint);
                grid.BeforePrint += new BeforePrintEventHandler(UltraGridPrinter.OnGridBeforePrint);
                grid.InitializeLogicalPrintPage += new InitializeLogicalPrintPageEventHandler(UltraGridPrinter.OnGridInitializeLogicalPrintPage);
                UltraGridPrinter.ReportHeader = reportCaption;
                grid.PrintPreview(RowPropertyCategories.Hidden);
            }
            catch(Exception ex) { throw ex; }
        }
        public static void Print(UltraGrid grid,string reportCaption,bool showDialog) {
            //Print the grid rows
            try {
                //Remove and reset event handlers for this grid object, then start print process
                grid.InitializePrintPreview -= new InitializePrintPreviewEventHandler(UltraGridPrinter.OnGridInitializePrintPreview);
                grid.InitializePrintPreview += new InitializePrintPreviewEventHandler(UltraGridPrinter.OnGridInitializePrintPreview);
                grid.InitializePrint -= new InitializePrintEventHandler(UltraGridPrinter.OnGridInitializePrint);
                grid.InitializePrint += new InitializePrintEventHandler(UltraGridPrinter.OnGridInitializePrint);
                grid.BeforePrint -= new BeforePrintEventHandler(UltraGridPrinter.OnGridBeforePrint);
                grid.BeforePrint += new BeforePrintEventHandler(UltraGridPrinter.OnGridBeforePrint);
                grid.InitializeLogicalPrintPage -= new InitializeLogicalPrintPageEventHandler(UltraGridPrinter.OnGridInitializeLogicalPrintPage);
                grid.InitializeLogicalPrintPage += new InitializeLogicalPrintPageEventHandler(UltraGridPrinter.OnGridInitializeLogicalPrintPage);
                UltraGridPrinter.ShowDialog = showDialog;
                UltraGridPrinter.ReportHeader = reportCaption;
                grid.Print(RowPropertyCategories.Hidden);
            }
            catch(Exception ex) { throw ex; }
        }
        #region Print Event Handlers: OnGridInitializePrintPreview(), OnGridInitializePrint(), OnGridBeforePrint(), OnGridInitializeLogicalPrintPage()
        private static void OnGridInitializePrintPreview(object sender,Infragistics.Win.UltraWinGrid.CancelablePrintPreviewEventArgs e) {
            //Event handler when a print preview is first initiated
            //Update: Feb.01, 2005 - Top and Bottom margin was increased to 1.25 to fix repeating of last row in the second page.
            UltraGrid oGrid = (UltraGrid)sender;
            try {
                //Set print preview dialog attributes
                e.PrintPreviewSettings.DialogLeft = SystemInformation.WorkingArea.X + 192;
                e.PrintPreviewSettings.DialogTop = SystemInformation.WorkingArea.Y + 192;
                e.PrintPreviewSettings.DialogWidth = 672;	//SystemInformation.WorkingArea.Width;
                e.PrintPreviewSettings.DialogHeight = 480;	//SystemInformation.WorkingArea.Height;
                e.PrintPreviewSettings.Zoom = 1.0;

                //Set layout attributes and print document attributes including defualt page settings
                setLogicalPageLayout(oGrid,e.DefaultLogicalPageLayoutInfo);
                e.PrintDocument.DocumentName = oGrid.Text;
                e.PrintLayout.Override.RowSelectors = DefaultableBoolean.False;
                e.PrintLayout.BorderStyle = UIElementBorderStyle.None;
                e.PrintDocument.DefaultPageSettings = UltraGridPrinter.Settings;
            }
            catch(Exception ex) { throw ex; }
        }
        private static void OnGridInitializePrint(object sender,Infragistics.Win.UltraWinGrid.CancelablePrintEventArgs e) {
            //Event handler when a print job is first initiated
            UltraGrid oGrid = (UltraGrid)sender;
            try {
                //Set layout attributes
                setLogicalPageLayout(oGrid,e.DefaultLogicalPageLayoutInfo);

                //Set print document attributes including defualt page settings
                e.PrintDocument.DocumentName = oGrid.Text;
                e.PrintLayout.Override.RowSelectors = DefaultableBoolean.False;
                e.PrintLayout.BorderStyle = UIElementBorderStyle.None;
                e.PrintLayout.Scrollbars = Scrollbars.None;
                e.PrintDocument.DefaultPageSettings = UltraGridPrinter.Settings;
            }
            catch(Exception ex) { throw ex; }
        }
        private static void OnGridBeforePrint(object sender,Infragistics.Win.UltraWinGrid.CancelablePrintEventArgs e) {
            //Event handler when after a print job has been initaited and configured by
            //a user, just before data is sent to the printer
            PrintDialog dlg = null;
            DialogResult res;
            try {
                if(UltraGridPrinter.ShowDialog) {
                    //Configure printer properties page
                    dlg = new PrintDialog();
                    dlg.AllowPrintToFile = dlg.AllowSelection = dlg.AllowSomePages = false;
                    dlg.Document = e.PrintDocument;
                    //dlg.PrinterSettings = e.PrintDocument.PrinterSettings;
                    res = dlg.ShowDialog();
                    if(res == DialogResult.OK) {
                        //Retain current settings and print
                        e.Cancel = false;
                    }
                    else
                        e.Cancel = true;
                }
                else
                    e.Cancel = false;
            }
            catch(Exception ex) { throw ex; }
        }
        private static void OnGridInitializeLogicalPrintPage(object sender,Infragistics.Win.UltraWinGrid.CancelableLogicalPrintPageEventArgs e) {
            //Event handler that occurs when a logical print page is being formatted
            //for printing or previewing
            try {
                //Configure page
            }
            catch(Exception ex) { throw ex; }
        }
        #endregion
        private static void setLogicalPageLayout(UltraGrid oGrid,LogicalPageLayoutInfo layoutInfo) {
            //
            try {
                //Set layout attributes
                layoutInfo.FitWidthToPages = 1;
                layoutInfo.ClippingOverride = ClippingOverride.Yes;
                //layoutInfo.PageHeader = "SHIP SCHEDULE FOR " + DateTime.Now.ToString("dd-MMM-yyyy");
                layoutInfo.PageHeader = ReportHeader;
                layoutInfo.PageHeaderHeight = 48;
                layoutInfo.PageHeaderAppearance.FontData.SizeInPoints = 12;
                layoutInfo.PageHeaderAppearance.FontData.Bold = DefaultableBoolean.True;
                layoutInfo.PageHeaderAppearance.TextHAlign = HAlign.Center;
                layoutInfo.PageHeaderAppearance.TextVAlign = VAlign.Middle;
                layoutInfo.PageHeaderBorderStyle = UIElementBorderStyle.Default;
                layoutInfo.PageFooter = "Page <#>.";
                layoutInfo.PageFooterHeight = 24;
                layoutInfo.PageFooterAppearance.TextHAlign = HAlign.Center;
                layoutInfo.PageFooterAppearance.FontData.Italic = DefaultableBoolean.True;
                layoutInfo.PageFooterBorderStyle = UIElementBorderStyle.None;

            }
            catch(Exception ex) { throw ex; }
        }
    }
}
