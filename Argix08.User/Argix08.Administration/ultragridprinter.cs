//	File:	ultragridprinter.cs
//	Author:	J. Heary
//	Date:	02/27/04
//	Desc:	Encapsultes printing services using Infragistics UltraGrid and
//			.NET printing services.
//	Rev:	03/11/04- (jph) modified default page orientation to landscape.
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Tsort {
	//
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
			PageSetupDialog dlg=null;
			try {
				//Display page settings dialog
				dlg = new PageSetupDialog();
				dlg.AllowMargins = dlg.AllowOrientation = dlg.AllowPaper = true;
				dlg.AllowPrinter = false;		//PrinterSettings=null disables AllowPrinter
				//dlg.PrinterSettings = null;
				
				dlg.PageSettings = UltraGridPrinter.Settings;
				#region Debug
				Debug.Write("dlg.PageSettings.PrinterSettings.PrinterName=" + dlg.PageSettings.PrinterSettings.PrinterName + "\n");
				Debug.Write("dlg.PageSettings.PrinterSettings.DefaultPageSettings.Margins.Left=" + dlg.PageSettings.PrinterSettings.DefaultPageSettings.Margins.Left.ToString() + "\n");
				Debug.Write("dlg.PageSettings.PrinterSettings.DefaultPageSettings.Landscape=" + dlg.PageSettings.PrinterSettings.DefaultPageSettings.Landscape.ToString() + "\n");
				Debug.Write("dlg.PageSettings.Margins.Left=" + dlg.PageSettings.Margins.Left.ToString() + "\n");
				Debug.Write("dlg.PageSettings.Landscape=" + dlg.PageSettings.Landscape.ToString() + "\n");
				Debug.Write("UltraGridPrinter.Settings.PrinterSettings.PrinterName=" + UltraGridPrinter.Settings.PrinterSettings.PrinterName + "\n");
				Debug.Write("UltraGridPrinter.Settings.PrinterSettings.DefaultPageSettings.Margins.Left=" + UltraGridPrinter.Settings.PrinterSettings.DefaultPageSettings.Margins.Left.ToString() + "\n");
				Debug.Write("UltraGridPrinter.Settings.PrinterSettings.DefaultPageSettings.Landscape=" + UltraGridPrinter.Settings.PrinterSettings.DefaultPageSettings.Landscape.ToString() + "\n");
				Debug.Write("UltraGridPrinter.Settings.Margins.Left=" + UltraGridPrinter.Settings.Margins.Left.ToString() + "\n");
				Debug.Write("UltraGridPrinter.Settings.Landscape=" + UltraGridPrinter.Settings.Landscape.ToString() + "\n");
				#endregion
				return dlg.ShowDialog();
			}
			catch(Exception ex) { throw ex; }
		}
		public static void PrintPreview(UltraGrid grid, string reportCaption) {
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
		public static void Print(UltraGrid grid,string reportCaption, bool showDialog) {
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
		private static void OnGridInitializePrintPreview(object sender, Infragistics.Win.UltraWinGrid.CancelablePrintPreviewEventArgs e) {
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
				setLogicalPageLayout(oGrid, e.DefaultLogicalPageLayoutInfo);
				e.PrintDocument.DocumentName = oGrid.Text;
				e.PrintLayout.Override.RowSelectors = DefaultableBoolean.False;
				e.PrintLayout.BorderStyle = UIElementBorderStyle.None;
				e.PrintDocument.DefaultPageSettings = UltraGridPrinter.Settings;
			} 
			catch(Exception ex) { throw ex; }
		}
		private static void OnGridInitializePrint(object sender, Infragistics.Win.UltraWinGrid.CancelablePrintEventArgs e) {
			//Event handler when a print job is first initiated
			UltraGrid oGrid = (UltraGrid)sender;
			try {
				//Set layout attributes
				setLogicalPageLayout(oGrid, e.DefaultLogicalPageLayoutInfo);
				
				//Set print document attributes including defualt page settings
				e.PrintDocument.DocumentName = oGrid.Text;
				e.PrintLayout.Override.RowSelectors = DefaultableBoolean.False;
				e.PrintLayout.BorderStyle = UIElementBorderStyle.None;
				e.PrintLayout.Scrollbars = Scrollbars.None;
				e.PrintDocument.DefaultPageSettings = UltraGridPrinter.Settings;
			} 
			catch(Exception ex) { throw ex; }
		}
		private static void OnGridBeforePrint(object sender, Infragistics.Win.UltraWinGrid.CancelablePrintEventArgs e) {
			//Event handler when after a print job has been initaited and configured by
			//a user, just before data is sent to the printer
			PrintDialog dlg=null;
			DialogResult res;
			try {
				if(UltraGridPrinter.ShowDialog) {
					//Configure printer properties page
					dlg = new PrintDialog();
					dlg.AllowPrintToFile = dlg.AllowSelection = dlg.AllowSomePages = false;
					dlg.Document = e.PrintDocument;
					//dlg.PrinterSettings = e.PrintDocument.PrinterSettings;
					res = dlg.ShowDialog();
					if(res==DialogResult.OK) {
						//Retain current settings and print
						#region Debug
						Debug.Write("dlg.Document.PrinterSettings.PrinterName=" + dlg.Document.PrinterSettings.PrinterName + "\n");
						Debug.Write("dlg.Document.PrinterSettings.DefaultPageSettings.Margins.Left=" + dlg.Document.PrinterSettings.DefaultPageSettings.Margins.Left.ToString() + "\n");
						Debug.Write("dlg.Document.PrinterSettings.DefaultPageSettings.Landscape=" + dlg.Document.PrinterSettings.DefaultPageSettings.Landscape.ToString() + "\n");
						Debug.Write("dlg.Document.DefaultPageSettings.Margins.Left=" + dlg.Document.DefaultPageSettings.Margins.Left.ToString() + "\n");
						Debug.Write("dlg.Document.DefaultPageSettings.Landscape=" + dlg.Document.DefaultPageSettings.Landscape.ToString() + "\n");
						Debug.Write("e.PrintDocument.PrinterSettings.PrinterName=" + e.PrintDocument.PrinterSettings.PrinterName + "\n");
						Debug.Write("e.PrintDocument.PrinterSettings.DefaultPageSettings.Margins.Left=" + e.PrintDocument.PrinterSettings.DefaultPageSettings.Margins.Left.ToString() + "\n");
						Debug.Write("e.PrintDocument.PrinterSettings.DefaultPageSettings.Landscape=" + e.PrintDocument.PrinterSettings.DefaultPageSettings.Landscape.ToString() + "\n");
						Debug.Write("e.PrintDocument.DefaultPageSettings.Margins.Left=" + e.PrintDocument.DefaultPageSettings.Margins.Left.ToString() + "\n");
						Debug.Write("e.PrintDocument.DefaultPageSettings.Landscape=" + e.PrintDocument.DefaultPageSettings.Landscape.ToString() + "\n");
						#endregion
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
		private static void OnGridInitializeLogicalPrintPage(object sender, Infragistics.Win.UltraWinGrid.CancelableLogicalPrintPageEventArgs e) {
			//Event handler that occurs when a logical print page is being formatted
			//for printing or previewing
			try {
				//Configure page
			} 
			catch(Exception ex) { throw ex; }
		}
		#endregion
		private static void setLogicalPageLayout(UltraGrid oGrid, LogicalPageLayoutInfo layoutInfo) {
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
				layoutInfo.PageFooterHeight= 24;
				layoutInfo.PageFooterAppearance.TextHAlign = HAlign.Center;
				layoutInfo.PageFooterAppearance.FontData.Italic = DefaultableBoolean.True;
				layoutInfo.PageFooterBorderStyle = UIElementBorderStyle.None;
				
			} 
			catch(Exception ex) { throw ex; }
		}
	}
}
