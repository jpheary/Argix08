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
using System.IO;
using System.Windows.Forms;

namespace Argix.Windows.Printers {
	//
	public class WinPrinter {
		//Members
		protected PrintDocument mDoc=null;
		protected Font mFont=null;
		protected string mContent="";
		protected string[] mHeader=null, mBody=null;
		protected int mPageNumber=1, mTotalLines=0;
		protected PageSettings mPageSettings=null;
		
		//Constants
		private const string FONT_NAME = "Courier New";
		private const float FONT_SIZE = 8.25F;
		
		//Interface
		public WinPrinter() : this("", FONT_NAME, FONT_SIZE) { }
		public WinPrinter(string printerName) : this(printerName, FONT_NAME, FONT_SIZE) { }
		public WinPrinter(string printerName, string fontName, float fontSize) : this(printerName, new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Point, ((System.Byte)(0)))) { }
		public WinPrinter(string printerName, Font font) {
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
					for(int i=0; i<PrinterSettings.InstalledPrinters.Count; i++) {
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
		public virtual void Print(string documentName, string document, bool showDialog) {
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
					dlgPrint.AllowPrintToFile = dlgPrint.AllowSelection = dlgPrint.AllowSomePages = false;
					dlgPrint.Document = this.mDoc;
					dlgPrint.PrinterSettings = this.mDoc.PrinterSettings;
                    dlgPrint.UseEXDialog = true;
					res = dlgPrint.ShowDialog();
				}
				if(res==DialogResult.OK) this.mDoc.Print();
			} 
			catch(Exception ex) { throw ex; }
		}
		public virtual void Print(string documentName, string[] header, string[] body, bool showDialog) {
			//Print method
			DialogResult res = DialogResult.OK;
			try {
				//Retain header and body
				this.mHeader = header;
				this.mBody = body;
				
				//Create a print document and initialize; allow user modification if required
				this.mDoc.DocumentName = documentName;
				this.mDoc.OriginAtMargins = false;
				if(showDialog) {
					PrintDialog dlgPrint = new PrintDialog();
					dlgPrint.AllowPrintToFile = dlgPrint.AllowSelection = dlgPrint.AllowSomePages = false;
					dlgPrint.Document = this.mDoc;
					dlgPrint.PrinterSettings = this.mDoc.PrinterSettings;
                    dlgPrint.UseEXDialog = true;
					res = dlgPrint.ShowDialog();
				}
				if(res==DialogResult.OK) this.mDoc.Print();
			} 
			catch(Exception ex) { throw ex; }
		}
		protected virtual void OnBeginPrint(object sender, PrintEventArgs ev) { }
		protected virtual void OnPrintPage(object sender, PrintPageEventArgs ev) {
			//Provide the printing logic for the document
			float lineHeight=0, linesPerPage=0, fY=0;
			float leftMargin=ev.MarginBounds.Left, topMargin=ev.MarginBounds.Top;
			int iLine=0;
			try {
				if(this.mContent.Length > 0) {
					//Provide default printing logic for the document
					ev.Graphics.DrawString(this.mContent, this.mFont, Brushes.Black, ev.MarginBounds, new StringFormat());
					ev.HasMorePages = false;
				}
				else if(this.mBody.Length > 0) {
					//Iterate over the document, printing each line
					Rectangle rect = ev.MarginBounds;
					StringFormat format = new StringFormat();
					lineHeight = this.mFont.GetHeight(ev.Graphics);
					linesPerPage = (ev.MarginBounds.Height / lineHeight) - this.mHeader.Length - 8;
				
					//Header
					fY = topMargin;
					for(iLine=0; iLine<this.mHeader.Length; iLine++) {
						ev.Graphics.DrawString(this.mHeader[iLine], this.mFont, Brushes.Black, leftMargin, fY, format);
						fY += lineHeight;
					}
				
					//Body
					for(iLine=0; iLine<linesPerPage; iLine++) {
						if(this.mTotalLines + iLine>=this.mBody.Length) 
							break;
						ev.Graphics.DrawString(this.mBody[this.mTotalLines + iLine], this.mFont, Brushes.Black, leftMargin, fY, format);
						fY += lineHeight;
					}
				
					//Footer
					fY = ev.MarginBounds.Bottom - 5 * lineHeight;
					ev.Graphics.DrawString(this.mPageNumber.ToString(), this.mFont, Brushes.Black, leftMargin + (ev.MarginBounds.Width/2), fY, format);
				
					//Totals
					if((this.mTotalLines + iLine < this.mBody.Length)) {
						ev.HasMorePages = true;
						this.mTotalLines += iLine;
						this.mPageNumber += 1;
					}
					else {
						ev.HasMorePages = false;
						this.mTotalLines = 0;
						this.mPageNumber = 1;
					}
				}
			}
			catch(Exception ex) { throw ex; }
		}
		protected virtual void OnEndPrint(object sender, PrintEventArgs ev) {
			ev.Cancel = false;
		}
	}
}
