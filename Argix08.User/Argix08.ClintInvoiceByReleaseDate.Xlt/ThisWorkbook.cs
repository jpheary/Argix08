using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel=Microsoft.Office.Interop.Excel;
using Office=Microsoft.Office.Core;

namespace Argix.Finance {
    //
    public partial class ThisWorkbook {
        //Members
        private const string USP_INVOICE = "uspInvTsortClientInvoiceByReleaseDateGet",TBL_INVOICE = "ClientInvoiceTable";
        private const int ROW0_DETAIL=12;

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender,System.EventArgs e) {
            //Event handler for workbook startup event
            try {
                System.IntPtr p = GetCommandLine();
                string cmd = System.Runtime.InteropServices.Marshal.PtrToStringAuto(p);
                string clid="",invoice="";  //320913500
                if(cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Create detail worksheet
                    SqlDataAdapter adapter = new SqlDataAdapter(USP_INVOICE,global::Argix.Finance.Settings.Default.SQLConnection);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                    adapter.TableMappings.Add("Table",TBL_INVOICE);
                    InvoiceDS ds = new InvoiceDS();
                    adapter.Fill(ds,TBL_INVOICE);
                    createDetailHeader(ds.ClientInvoiceTable[0]);
                    createDetailBody(ds.ClientInvoiceTable);
                    createDetailTotals(ds.ClientInvoiceTable);
                }
                else {
                    MessageBox.Show("Invoice unspecified.");
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_BeforeSave(bool SaveAsUI,ref bool Cancel) {
            //Event handler for before save
            try {
                //Remove customization so dll isn't called from a saved file (i.e. only from the template)
                this.RemoveCustomization();
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_Shutdown(object sender,System.EventArgs e) {
            //Event handler for workbook shutdown event
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup() {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void createDetailHeader(InvoiceDS.ClientInvoiceTableRow invoice) {
            //Set named range summary values
            Detail detail = global::Argix.Finance.Globals.Detail;

            //Remit To
            detail.ClientNumberDiv.Value = (invoice.IsClientNumberNull()? "" : invoice.ClientNumber.Trim()) + " " + (invoice.IsClientDivisionNull()? "" : invoice.ClientDivision.Trim()) + " - ";
            detail.RemitToName.Value = invoice.RemitToName.Trim();
            detail.RemitToAddressLine1.Value = invoice.RemitToAddressLine1.Trim();
            //detail.RemitToAddressLine2.Value = invoice.IsRemitToAddressLine2Null() ? "" : invoice.RemitToAddressLine2.Trim();
            detail.RemitToCityStateZip.Value = invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();
            detail.Telephone.Value = (invoice.IsTelephoneNull() ? "" : invoice.Telephone.ToString());

            //Bill To
            detail.BillToName.Value = invoice.BillToName.Trim();
            detail.BillToAddressLine1.Value = invoice.BillToAddressline1.Trim();
            detail.BillToAddressLine2.Value = invoice.IsBillToAddressline2Null() ? "" : invoice.BillToAddressline2.Trim();
            detail.BillToCityStateZip.Value = invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() + "-" + invoice.BillToZIP4.Trim();

            //Account
            detail.InvoiceNumber.Value = invoice.InvoiceNumber.Trim();
            detail.InvoiceDate.Value = invoice.InvoiceDate;
            detail.ReleaseDate.Value = invoice.ReleaseDate;
        }
        private void createDetailBody(InvoiceDS.ClientInvoiceTableDataTable invoiceTable) {
            //Get worksheet
            Detail ws = global::Argix.Finance.Globals.Detail;
            Application.ScreenUpdating = false;

            //Insert a row at row0 + 1 (pushes down) for every row of data
            int rowCount = invoiceTable.Rows.Count;
            Excel.Range row0 = ws.get_Range(ws.Cells[ROW0_DETAIL + 1,1],ws.Cells[ROW0_DETAIL + 1,16]).EntireRow;
            for(int i=0; i<rowCount - 1; i++)
                row0.Insert(Excel.XlInsertShiftDirection.xlShiftDown,false);

            //Populate entire data table into a range of worksheet cells
            object[,] values = new object[rowCount,16];
            for(int i=0; i<rowCount; i++) {
                values[i,0] = "'" + invoiceTable[i].StoreName.Trim();
                values[i,1] = "'" + invoiceTable[i].StoreState;
                values[i,2] = "'" + invoiceTable[i].StoreZip;
                values[i,3] = "'" + invoiceTable[i].LocationCode;
                values[i,4] = invoiceTable[i].CtnQty;
                values[i,5] = invoiceTable[i].CartonRate;
                values[i,6] = invoiceTable[i].PltQty;
                values[i,7] = invoiceTable[i].PalletRate;
                values[i,8] = invoiceTable[i].Weight;
                values[i,9] = invoiceTable[i].RatedWeight;
                values[i,10] = invoiceTable[i].WeightRate;
                values[i,11] = invoiceTable[i].Surcharge;
                values[i,12] = invoiceTable[i].ConsolidationCharge;
                values[i,13] = invoiceTable[i].FuelRate;
                values[i,14] = invoiceTable[i].FuelSurcharge;
                values[i,15] = invoiceTable[i].DeliveryTotal;
            }
            ws.get_Range(ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,16]).Value2 = values;

            #region Column Formats
            ws.get_Range(ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            ws.get_Range(ws.Cells[ROW0_DETAIL,2],ws.Cells[ROW0_DETAIL + rowCount - 1,2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            ws.get_Range(ws.Cells[ROW0_DETAIL,3],ws.Cells[ROW0_DETAIL + rowCount - 1,3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            ws.get_Range(ws.Cells[ROW0_DETAIL,4],ws.Cells[ROW0_DETAIL + rowCount - 1,4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            ws.get_Range(ws.Cells[ROW0_DETAIL,5],ws.Cells[ROW0_DETAIL + rowCount - 1,5]).NumberFormat = "#,###_);(#,###)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,5],ws.Cells[ROW0_DETAIL + rowCount - 1,5]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,6],ws.Cells[ROW0_DETAIL + rowCount - 1,6]).NumberFormat = "#,###.##_);(#,###.##);_(* _)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,6],ws.Cells[ROW0_DETAIL + rowCount - 1,6]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,7],ws.Cells[ROW0_DETAIL + rowCount - 1,7]).NumberFormat = "#,###_);(#,###)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,7],ws.Cells[ROW0_DETAIL + rowCount - 1,7]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,8],ws.Cells[ROW0_DETAIL + rowCount - 1,8]).NumberFormat = "#,###.##_);(#,###.##);_(* _)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,8],ws.Cells[ROW0_DETAIL + rowCount - 1,8]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,9],ws.Cells[ROW0_DETAIL + rowCount - 1,9]).NumberFormat = "#,##0_);(#,##0)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,9],ws.Cells[ROW0_DETAIL + rowCount - 1,9]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,10],ws.Cells[ROW0_DETAIL + rowCount - 1,10]).NumberFormat = "#,##0_);(#,##0)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,10],ws.Cells[ROW0_DETAIL + rowCount - 1,10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,11],ws.Cells[ROW0_DETAIL + rowCount - 1,11]).NumberFormat = "#,##0.00_);(#,##0.00)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,11],ws.Cells[ROW0_DETAIL + rowCount - 1,11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,12],ws.Cells[ROW0_DETAIL + rowCount - 1,12]).NumberFormat = "$#,##0.00_);($#,##0.00);_(* _)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,12],ws.Cells[ROW0_DETAIL + rowCount - 1,12]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,13],ws.Cells[ROW0_DETAIL + rowCount - 1,13]).NumberFormat = "$#,##0.00_);($#,##0.00);_(* _)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,13],ws.Cells[ROW0_DETAIL + rowCount - 1,13]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,14],ws.Cells[ROW0_DETAIL + rowCount - 1,14]).NumberFormat = "#,##0.0000_);(#,##0.0000)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,14],ws.Cells[ROW0_DETAIL + rowCount - 1,14]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,15],ws.Cells[ROW0_DETAIL + rowCount - 1,15]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,15],ws.Cells[ROW0_DETAIL + rowCount - 1,15]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL,16],ws.Cells[ROW0_DETAIL + rowCount - 1,16]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.get_Range(ws.Cells[ROW0_DETAIL,16],ws.Cells[ROW0_DETAIL + rowCount - 1,16]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            #endregion
            Application.ScreenUpdating = true;
        }
        private void createDetailTotals(InvoiceDS.ClientInvoiceTableDataTable invoiceTable) {
            //Get worksheet
            Detail ws = global::Argix.Finance.Globals.Detail;

            //Totals
            int rowCount = invoiceTable.Rows.Count;
            object[,] totals = new object[1,16];
            int qty=0,weight=0;
            decimal fs=0.0M,dt=0.0M;
            for(int i=0; i<rowCount; i++) {
                qty += invoiceTable[i].CtnQty;
                weight += invoiceTable[i].Weight;
                fs += invoiceTable[i].FuelSurcharge;
                dt += invoiceTable[i].DeliveryTotal;
            }
            totals[0,0] = "TOTAL " + rowCount.ToString() + " DELIVERIES";
            totals[0,1] = totals[0,2] = totals[0,3] = "";
            totals[0,4] = qty;
            totals[0,5] = totals[0,6] = totals[0,7] = "";
            totals[0,8] = weight;
            totals[0,9] = totals[0,10] = totals[0,11] = totals[0,12] = totals[0,13] = "";
            totals[0,14] = fs;
            totals[0,15] = dt;
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,1],ws.Cells[ROW0_DETAIL + rowCount + 1,16]).Value2 = totals;

            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,5],ws.Cells[ROW0_DETAIL + rowCount + 1,5]).NumberFormat = "#,###_);(#,###)";
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,5],ws.Cells[ROW0_DETAIL + rowCount + 1,5]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,9],ws.Cells[ROW0_DETAIL + rowCount + 1,9]).NumberFormat = "#,##0_);(#,##0)";
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,9],ws.Cells[ROW0_DETAIL + rowCount + 1,9]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,15],ws.Cells[ROW0_DETAIL + rowCount + 1,15]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,15],ws.Cells[ROW0_DETAIL + rowCount + 1,15]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,16],ws.Cells[ROW0_DETAIL + rowCount + 1,16]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 1,16],ws.Cells[ROW0_DETAIL + rowCount + 1,16]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;


            //Footer
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 3,1],ws.Cells[ROW0_DETAIL + rowCount + 3,16]).Value2 = new object[1,16] { { "PLEASE REFERENCE INVOICE# " + invoiceTable[0].InvoiceNumber + " WHEN REMITTING PAYMENT I.C.C. REGULATIONS REQUIRE THAT THIS BILL BE PAID WITHIN 7 DAYS","","","","","","","","","","","","","","","" } };
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 4,1],ws.Cells[ROW0_DETAIL + rowCount + 4,16]).Value2 = new object[1,16] { { "","","A SERVICE CHARGE OF 1.5% PER MONTH IS ADDED TO ALL PAST DUE INVOICES","","","","","","","","","","","","","" } };
            ws.get_Range(ws.Cells[ROW0_DETAIL + rowCount + 5,1],ws.Cells[ROW0_DETAIL + rowCount + 5,16]).Value2 = new object[1,16] { { "","","REMIT TO: " + invoiceTable[0].RemitToName.Trim() + " " + invoiceTable[0].RemitToAddressLine1.Trim() + " " + invoiceTable[0].RemitToAddressLine2.Trim() + " " + invoiceTable[0].RemitToCity.Trim() + ", " + invoiceTable[0].RemitToState + " " + invoiceTable[0].RemitToZip + "-" + invoiceTable[0].RemitToZip4,"","","","","","","","","","","","","" } };
        }
        private void reportError(Exception ex) { MessageBox.Show("UNEXPECTED ERROR: " + ex.Message); }
    }
}
