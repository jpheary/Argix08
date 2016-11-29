//	File:	brotherpt2300.cs
//	Author:	J. Heary
//	Date:	03/27/04
//	Desc:	Provides printing services through a Windows driver to a Brother
//			PT-2300 printer.
//	Rev:	09/22/04 (jph)- modified to inherit from WinPrinter.
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace Argix.Windows.Printers {
	//
	public class BrotherPT2300 : WinPrinter {
		//Members
		private Barcode128 m_eSubset = Barcode128.C;
		
		//Constants
		private const string PRINTER_NAME = "Brother PT-2300/2310";
		private const string FONT_NAME = "Code128A";
		private const float FONT_SIZE = 20F;
		
		//Interface
		public BrotherPT2300() : this(PRINTER_NAME) { }
		public BrotherPT2300(string printerName) : this(printerName, FONT_NAME, FONT_SIZE) { }
		public BrotherPT2300(string printerName, string fontName, float fontSize) : base(printerName, fontName, fontSize) {
			//Constructor
			try {
				//Configure page settings for this printer
				base.mDoc.DefaultPageSettings.Landscape = true;
				base.mDoc.DefaultPageSettings.Margins.Left = 12;
				base.mDoc.DefaultPageSettings.Margins.Top = 2;
				base.mDoc.DefaultPageSettings.Margins.Right = 12;
				base.mDoc.DefaultPageSettings.Margins.Bottom = 2;
			}
			catch(Exception ex) { throw ex; }
		}
		public Barcode128 Subset { 
			get { return this.m_eSubset; }
			set { this.m_eSubset = value; }
		}
		
		protected override void OnBeginPrint(object sender, PrintEventArgs ev) { }
		protected override void OnPrintPage(object sender, PrintPageEventArgs ev) {
			//Provide the printing logic for the document
			
			//Encode the label for barcode printing
			string sLabel = "";
			switch(base.mFont.Name) {
				case "Code128A":			sLabel = BarCode.Encode128AB(base.mContent, base.mFont.Name, this.m_eSubset); break;
				case "Bar Sample 128AB":	sLabel = BarCode.Encode128AB(base.mContent, base.mFont.Name, this.m_eSubset); break;
				case "Bar Sample 128AB HR": sLabel = BarCode.Encode128AB(base.mContent, base.mFont.Name, this.m_eSubset); break;
				case "IDAutomationSC128S":	sLabel = BarCode.Encode128AB(base.mContent, base.mFont.Name, this.m_eSubset); break;
				default:					sLabel = BarCode.Encode128AB(base.mContent, base.mFont.Name, this.m_eSubset); break;
			}
			
			//Print barcode
			RectangleF rectTop = new RectangleF(ev.MarginBounds.Left, ev.MarginBounds.Top, ev.MarginBounds.Width, ev.MarginBounds.Height/2);
			ev.Graphics.DrawString(sLabel, base.mFont, Brushes.Black, rectTop, new StringFormat());
			
			//Print text
			Font f = new Font("Courier New", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((System.Byte)(0)));
			RectangleF rectBot = new RectangleF(ev.MarginBounds.Left, ev.MarginBounds.Top + ev.MarginBounds.Height/2, ev.MarginBounds.Width, ev.MarginBounds.Height/2);
			ev.Graphics.DrawString(base.mContent, f, Brushes.Black, rectBot, new StringFormat());
			
			ev.HasMorePages = false;
		}
		protected override void OnEndPrint(object sender, PrintEventArgs ev) {
			ev.Cancel = false;
		}
	}
}
