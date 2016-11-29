//	File:	winprinter.cs
//	Author:	J. Heary
//	Date:	09/22/04
//	Desc:	Provides printing services through a Windows.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Argix.Windows.Printers {
    //
    public class WinPrinter2 {
        //Members
        protected PrintDocument mDoc=null;
        protected Font mFont=null;
        protected string mContent="";
        protected string[] mHeader=null,mBody=null;
        protected int mPageNumber=1,mTotalLines=0;
        protected PageSettings mPageSettings=null;

        private const string FONT_NAME = "Courier New";
        private const float FONT_SIZE = 8.25F;

        //Convert the unit used by the .NET framework (1/100 inch) 
        //and the unit used by Win32 API calls (twips 1/1440 inch)
        private const double anInch = 14.4;
        private const int WM_USER  = 0x0400;
        private const int EM_FORMATRANGE  = WM_USER + 57;

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct CHARRANGE {
            public int cpMin;         //First character of range (0 for start of doc)
            public int cpMax;           //Last character of range (-1 for end of doc)
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct FORMATRANGE {
            public IntPtr hdc;             //Actual DC to draw on
            public IntPtr hdcTarget;       //Target DC for determining text formatting
            public RECT rc;                //Region of the DC to draw to (in twips)
            public RECT rcPage;            //Region of the whole DC (page size) (in twips)
            public CHARRANGE chrg;         //Range of text to draw (see earlier declaration)
        }

        [DllImport("USER32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd,int msg,IntPtr wp,IntPtr lp); 


        //Interface
        public WinPrinter2() : this("",FONT_NAME,FONT_SIZE) { }
        public WinPrinter2(string printerName) : this(printerName,FONT_NAME,FONT_SIZE) { }
        public WinPrinter2(string printerName,string fontName,float fontSize) : this(printerName,new Font(fontName,fontSize,FontStyle.Regular,GraphicsUnit.Point,((System.Byte)(0)))) { }
        public WinPrinter2(string printerName,Font font) {
            //Constructor
            try {
                this.mDoc = new PrintDocument();
                this.mDoc.BeginPrint += new PrintEventHandler(this.OnBeginPrint);
                this.mDoc.EndPrint += new PrintEventHandler(this.OnEndPrint);
                this.mDoc.PrintPage += new PrintPageEventHandler(this.OnPrintPage);
                this.mFont = font;
                if(printerName == "") {
                    //Setup for the default printer
                    this.mDoc.PrinterSettings = new PageSettings().PrinterSettings;
                }
                else {
                    //Setup for the specified printer; save a reference to the
                    //printer object and the page settings object of that printer
                    for(int i=0;i<PrinterSettings.InstalledPrinters.Count;i++) {
                        PrinterSettings p = new PrinterSettings();
                        p.PrinterName = PrinterSettings.InstalledPrinters[i];
                        if(p.PrinterName==printerName) {
                            this.mDoc.PrinterSettings = p;
                            this.mDoc.DefaultPageSettings = p.DefaultPageSettings;
                            break;
                        }
                    }
                }
            }
            catch(Exception ex) { throw ex; }
        }

        public PrintDocument Doc { get { return this.mDoc; } }
        public PageSettings evPageSettings { get { return this.mPageSettings; } }
        public void PageSetup() {
            //Page setup method
            try {
                PageSetupDialog dlgSetup = new PageSetupDialog();
                dlgSetup.AllowMargins = dlgSetup.AllowOrientation = dlgSetup.AllowPaper = true;
                dlgSetup.AllowPrinter = true;
                dlgSetup.Document = this.mDoc;
                dlgSetup.ShowDialog();
            }
            catch(Exception ex) { throw ex; }
        }
        public virtual void Print(string documentName,string document,bool showDialog) {
            //Print method
            DialogResult res = DialogResult.OK;
            try {
                //Retain document
                this.mContent = document;

                //Create a print document and initialize; allow user modification if required
                this.mDoc.DocumentName = documentName;
                this.mDoc.OriginAtMargins = false;
                if(showDialog) {
                    PrintDialog dlgPrint = new PrintDialog();
                    dlgPrint.UseEXDialog = true;
                    dlgPrint.AllowPrintToFile = dlgPrint.AllowSelection = dlgPrint.AllowSomePages = false;
                    dlgPrint.Document = this.mDoc;
                    dlgPrint.PrinterSettings = this.mDoc.PrinterSettings;
                    res = dlgPrint.ShowDialog();
                }
                if(res==DialogResult.OK) this.mDoc.Print();
            }
            catch(Exception ex) { throw ex; }
        }
        protected virtual void OnBeginPrint(object sender,PrintEventArgs ev) { }
        protected virtual void OnPrintPage(object sender,PrintPageEventArgs e) {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // Sets the value of charactersOnPage to the number of characters 
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(this.mContent,this.mFont,e.MarginBounds.Size,StringFormat.GenericTypographic,out charactersOnPage,out linesPerPage);

            // Draws the string within the bounds of the page
            e.Graphics.DrawString(this.mContent,this.mFont,Brushes.Black, e.MarginBounds,StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            this.mContent = this.mContent.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (this.mContent.Length > 0);
        }
        protected virtual void OnEndPrint(object sender,PrintEventArgs ev) {
            ev.Cancel = false;
        }
    }
}
