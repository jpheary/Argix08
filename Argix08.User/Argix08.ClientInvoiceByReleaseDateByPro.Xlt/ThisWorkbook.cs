using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel=Microsoft.Office.Interop.Excel;
using Office=Microsoft.Office.Core;

namespace Argix.Finance {
    //
    public partial class ThisWorkbook {
        //Members
        private const string USP_DETAIL = "uspInvTsortClientInvoiceByReleaseDateByProGet",TBL_DETAIL = "ClientInvoiceTable";
        private const int ROW0_DETAIL=12;

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender,System.EventArgs e) {
            //Event handler for workbook startup event
            try {
                System.IntPtr p = GetCommandLine();
                string cmd = System.Runtime.InteropServices.Marshal.PtrToStringAuto(p);
                string clid="",invoice="";  //320914600
                if(cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Create detail worksheet
                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_DETAIL,global::Argix.Finance.Settings.Default.SQLConnection);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                        adapter.TableMappings.Add("Table",TBL_DETAIL);
                        InvoiceDS ds = new InvoiceDS();
                        adapter.Fill(ds,TBL_DETAIL);
                        if(ds.Tables[TBL_DETAIL].Rows.Count > 0) {
                            createDetailHeader(ds.ClientInvoiceTable[0]);
                            createDetailBody(ds.ClientInvoiceTable);
                        }
                        else
                            MessageBox.Show("No data found for invoice #" + invoice + ".");
                    }
                    catch(Exception ex) { reportError(ex); }
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
            this.Startup += new System.EventHandler(this.ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void createDetailHeader(InvoiceDS.ClientInvoiceTableRow invoice) {
            //Create header of detail worksheet
            Detail detail = global::Argix.Finance.Globals.Detail;

            //Remit To
            detail.RemitToName.Value = invoice.RemitToName.Trim();
            detail.RemitToAddressLine1.Value = invoice.RemitToAddressLine1.Trim();
            //detail.RemitToAddressLine2.Value = invoice.IsRemitToAddressLine2Null() ? "" : invoice.RemitToAddressLine2.Trim();
            detail.RemitToCityStateZip.Value = invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();
            detail.Telephone.Value = "Tel# " + (invoice.IsTelephoneNull() ? "" : invoice.Telephone.ToString());

            //Bill To
            detail.BillToName.Value = invoice.BillToName.Trim();
            detail.BillToAddressLine1.Value = invoice.BillToAddressline1.Trim();
            detail.BillToAddressLine2.Value = invoice.IsBillToAddressline2Null() ? "" : invoice.BillToAddressline2.Trim();
            detail.BillToCityStateZip.Value = invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() + "-" + invoice.BillToZIP4.Trim();

            //Account
            detail.InvoiceNumber.Value = "";    // invoice.InvoiceNumber.Trim();
            detail.InvoiceDate.Value = invoice.InvoiceDate;
            detail.ShippedDate.Value = invoice.ReleaseDate;
            detail.FuelSurcharge.Value = "Charges Include a Fuel Surcharge of " + invoice.FuelRate.ToString("#0.00");
        }
        private void createDetailBody(InvoiceDS.ClientInvoiceTableDataTable invoiceTable) {
            //Create body of detail worksheet
            Excel.Worksheet ws = (Excel.Worksheet)this.Worksheets["Detail"];
            Application.ScreenUpdating = false;
                        
            //Insert a row at row0 + 1 (pushes down) for every row of data
            int rowCount = invoiceTable.Rows.Count;
            Excel.Range row0 = ws.get_Range(ws.Cells[ROW0_DETAIL + 1,1],ws.Cells[ROW0_DETAIL + 1,14]).EntireRow;
            for(int i=0; i<rowCount - 1; i++) 
                row0.Insert(Excel.XlInsertShiftDirection.xlShiftDown, false);

            //Populate entire data table into a range of worksheet cells
            object[,] values = new object[rowCount, 14];
            for(int i=0; i<rowCount; i++) {
                values[i,0] = "'" + invoiceTable[i].InvoiceNumber.Trim();
                values[i,1] = "'" + invoiceTable[i].PRONumber;
                values[i,2] = "'" + invoiceTable[i].StoreNumber.ToString();
                values[i,3] = "'" + invoiceTable[i].StoreZip.Trim();
                values[i,4] = "'" + invoiceTable[i].Zone.Trim();
                values[i,5] = "'" + invoiceTable[i].TLNumber.Trim();
                values[i,6] = invoiceTable[i].CtnQty;
                values[i,7] = invoiceTable[i].PltQty;
                values[i,8] = invoiceTable[i].Weight;
                values[i,9] = invoiceTable[i].RatedWeight;
                values[i,10] = invoiceTable[i].WeightRate;
                values[i,11] = invoiceTable[i].FuelSurcharge;
                values[i,12] = invoiceTable[i].DeliveryTotal;
                values[i,13] = "'" + invoiceTable[i].RateNote;
            }
            ws.get_Range(ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,14]).Value2 = values;
            
            Application.ScreenUpdating = true;
        }
        private void reportError(Exception ex) { MessageBox.Show("UNEXPECTED ERROR: " + ex.Message); }
    }
}
