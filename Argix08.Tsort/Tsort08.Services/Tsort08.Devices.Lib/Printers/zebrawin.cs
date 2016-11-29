//	File:	zebrawin.cs
//	Author:	J. Heary
//	Date:	11/10/08
//	Desc:	Provides printing services through a Windows driver to a Zebra
//			170XiIII+ label printer.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Tsort.Devices.Printers {
    /// <summary>Provides printing services through a Windows driver to a Zebra 170XiIII+ label printer.</summary>
	public class ZebraWin : ILabelPrinter {
		//Members
		private PrintDocument mDoc=null;
		private Font mFont=null;
		private string mDocument="";
		private PageSettings mPage=null;

        /// <summary></summary>
        public const string FONT_NAME = "Courier New";
        /// <summary></summary>
        public const float FONT_SIZE = 8F;
		
        /// <summary>Event notification for printer turned on.</summary>
        public event EventHandler PrinterTurnedOn = null;
        /// <summary>Event notification for printer turned off.</summary>
        public event EventHandler PrinterTurnedOff = null;
        /// <summary>Event notification for printer settings changed.</summary>
        public event EventHandler PrinterSettingsChanged = null;
        /// <summary>Event notification for printer pin and error events.</summary>
        public event PrinterEventHandler PrinterEventChanged = null;
		
		//Interface
        /// <summary>Constructor.</summary>
        public ZebraWin() : this("Zebra Windows") { }
        /// <summary>Constructor.</summary>
		/// <param name="printerName"></param>
        public ZebraWin(string printerName) : this(printerName, FONT_NAME, FONT_SIZE) { }
        /// <summary>Constructor.</summary>
		/// <param name="printerName"></param>
		/// <param name="fontName"></param>
		/// <param name="fontSize"></param>
        public ZebraWin(string printerName, string fontName, float fontSize) : this(printerName, new Font(fontName, fontSize, FontStyle.Regular, GraphicsUnit.Point, ((System.Byte)(0)))) { }
        /// <summary>Constructor.</summary>
		/// <param name="printerName"></param>
		/// <param name="font"></param>
        public ZebraWin(string printerName, Font font) {
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
				this.mDoc.DefaultPageSettings.Landscape = true;
				this.mDoc.DefaultPageSettings.Margins.Left = 0;
				this.mDoc.DefaultPageSettings.Margins.Top = 0;
				this.mDoc.DefaultPageSettings.Margins.Right = 0;
				this.mDoc.DefaultPageSettings.Margins.Bottom = 0;
				if(this.PrinterTurnedOn != null) this.PrinterTurnedOn(this, new EventArgs());
			}
			catch(Exception ex) { throw ex; }
		}
        /// <summary>Destructor.</summary>
        ~ZebraWin() { if(this.PrinterTurnedOff != null) this.PrinterTurnedOff(this,new EventArgs()); }

        /// <summary>Returns the underlying PrintDocument object.</summary>
        public PrintDocument Doc { get { return this.mDoc; } }
        /// <summary>Returns the underlting PageSettings object.</summary>
        public PageSettings evPageSettings { get { return this.mPage; } }
        /// <summary>Invokes the setup dialog.</summary>
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
		/// <summary>Prints the specified document.</summary>
		/// <param name="documentName">Document name for print que.</param>
		/// <param name="document">The document to print.</param>
		/// <param name="showDialog">True to show print dialog before printing.</param>
        public virtual void Print(string documentName, string document, bool showDialog) {
			//Print method
			DialogResult res = DialogResult.OK;
			try {
				//Retain document
				this.mDocument = document;
				
				//Create a print document and initialize; allow user modification if required
				mDoc.DocumentName = documentName;
				mDoc.OriginAtMargins = true;
				if(showDialog) {
					PrintDialog dlgPrint = new PrintDialog();
					dlgPrint.AllowPrintToFile = dlgPrint.AllowSelection = dlgPrint.AllowSomePages = false;
					dlgPrint.Document = mDoc;
					dlgPrint.PrinterSettings = mDoc.PrinterSettings;
					res = dlgPrint.ShowDialog();
				}
				if(res==DialogResult.OK) 
					mDoc.Print();
			} 
			catch(Exception ex) { throw ex; }
        }
        #region IDevice: DefaultSettings, Settings, On, TurnOn(), TurnOff()
        /// <summary>Returns a new PortSettings instance.</summary>
        public PortSettings DefaultSettings { get { return new PortSettings(); } }
        /// <summary>Returns a new PortSettings instance; invokes printer Page Setup dialog on set.</summary>
        public PortSettings Settings {
            get { return new PortSettings(); }
            set {
                PageSetup();
                if(this.PrinterSettingsChanged != null) this.PrinterSettingsChanged(this,new EventArgs());
            }
        }
        /// <summary>Always on; always returns true.</summary>
        public bool On { get { return true; } }
        /// <summary>Does nothing for this object.</summary>
        public void TurnOn() { }
        /// <summary>Does nothing for this object.</summary>
        public void TurnOff() { }
        #endregion
        #region ILabelPrinter: Type, Print()
        /// <summary>Gets the type of printer.</summary>
        public string Type { get { return "ZebraWin"; } }
        /// <summary>Sends labelFormat to the console.</summary>
        /// <param name="labelFormat">A string containing label data.</param>
        public void Print(string labelFormat) { Print("ZPL Label",labelFormat,true); }
        public bool CDHolding { get { return false; } }
        public bool CtsHolding { get { return false; } }
        public bool DsrHolding { get { return false; } }
        public bool DtrEnable { get { return false; } set { ; } }
        public bool RtsEnable { get { return false; } set { ; } }
        #endregion
        #region Printer Event Handlers: OnBeginPrint(), OnPrintPage(), OnEndPrint()
        private void OnBeginPrint(object sender,PrintEventArgs ev) { }
        private void OnPrintPage(object sender,PrintPageEventArgs ev) {
            //Provide the printing logic for the document
            ev.Graphics.DrawString(this.mDocument,this.mFont,Brushes.Black,ev.MarginBounds,new StringFormat());
            ev.HasMorePages = false;
        }
        private void OnEndPrint(object sender,PrintEventArgs ev) {
            ev.Cancel = false;
        }
        #endregion
    }
}
