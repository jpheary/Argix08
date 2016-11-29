using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace Argix.Finance {
    //
    public partial class ThisWorkbook {
        //Members
        private const string USP_DETAIL = "uspInvInvoiceWithStoreBLNumbersGet", TBL_DETAIL = "ClientInvoiceWithStoreBLNumbersTable";
       
        private const int DETAIL_NUMBER_OF_COLUMNS = 17;
        private int detailFirstRow = 11;
        private int detailLastRow = 11;

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender, System.EventArgs e) {
            //Event handler for workbook startup event
            try {
                System.IntPtr p = GetCommandLine();
                string cmd = System.Runtime.InteropServices.Marshal.PtrToStringAuto(p);
                string clid="",invoice="";
                if(cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                DialogResult result=DialogResult.OK;
                if(invoice.Length == 0) {
                    dlgInvoice dlg = new dlgInvoice();
                    result = dlg.ShowDialog();
                    invoice = dlg.InvoiceNumber;
                }
                if(result == DialogResult.OK) {
                    //Display summary and detail data
                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_DETAIL, global::Argix.Finance.Settings.Default.SQLConnection);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber", invoice) });
                        adapter.TableMappings.Add("Table", TBL_DETAIL);
                        InvoiceDS detailDS = new InvoiceDS();
                        adapter.Fill(detailDS, TBL_DETAIL);
                        showSummary(detailDS);
                        showDetail(detailDS.ClientInvoiceWithStoreBLNumbersTable);
                    }
                    catch (Exception ex) { reportError(ex); }
                }
            }
            catch (Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_BeforeSave(bool SaveAsUI, ref bool Cancel)
        {
            //Event handler for before save
            try
            {
                //Remove customization so dll isn't called from a saved file (i.e. only from the template)
                this.RemoveCustomization();
            }
            catch (Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {
            //Event handler for workbook shutdown event
        }
        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(this.ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void showSummary(InvoiceDS ds)
        {
            //Set named range summary values
            Detail detail = global::Argix.Finance.Globals.Detail;
            InvoiceDS.ClientInvoiceWithStoreBLNumbersTableRow invoice = ds.ClientInvoiceWithStoreBLNumbersTable[0];

            //Bill To
            
            detail.BillToName.Value = invoice.BillToName.Trim();
            detail.BillToAddressLine1.Value = invoice.BillToAddressline1.Trim();
            detail.BillToAddressLine2.Value = invoice.BillToAddressline2.Trim();
            detail.BillToCityStateZip.Value = invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() + "-" + invoice.BillToZIP4.Trim();

            //Account
            detail.InvoiceNumber.Value = invoice.InvoiceNumber.Trim();
            detail.InvoiceDate.Value = invoice.InvoiceDate;
            //summary.Terms1.Value = invoice.Terms;

            //Remit To
            detail.RemitToName.Value = invoice.RemitToName.Trim();
            detail.RemitToAddressLine1.Value = invoice.RemitToAddressLine1.Trim();
  //          detail.RemitToAddressLine2.Value = invoice.RemitToAddressLine2.Trim();
            detail.RemitToCityStateZip.Value = invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();
            detail.Telephone.Value = invoice.Telephone.Trim();
            detail.ClientNumberDiv.Value = invoice.ClientNumber.Trim() + ' ' + invoice.ClientDivision.Trim();

        }
        private void showDetail(DataTable invoiceTable)
        {
            //Get worksheet
            Excel.Worksheet ws = (Excel.Worksheet)this.Worksheets["Detail"];
            Application.ScreenUpdating = false;
            detailLastRow = detailFirstRow + invoiceTable.Rows.Count - 1;
            object[,] valArray = getValues(invoiceTable);
            Excel.Range firstRow = getRange(ws, detailFirstRow + 1, 1).EntireRow;
            for (int i = 0; i < invoiceTable.Rows.Count - 1; i++) { firstRow.Insert(Excel.XlInsertShiftDirection.xlShiftDown, false); }
            getRange(ws,detailFirstRow, 1, detailLastRow, DETAIL_NUMBER_OF_COLUMNS).Value2 = valArray;
            Application.ScreenUpdating = true;
        }
   
    
        private Excel.Range getRange(Excel.Worksheet ws, int fromR, int fromC, int toR, int toC)
        {
            return ws.get_Range(ws.Cells[fromR, fromC], ws.Cells[toR, toC]);
        }

        private Excel.Range getRange(Excel.Worksheet ws, int row, int column)
        {
            return getRange(ws, row, column, row, column);
        }

        private object[,] getValues(DataTable tbl) 
        {
            int rows = tbl.Rows.Count;
            int cols = tbl.Columns.Count;
            int arrayColumn;
            object[,] valArray = new object[rows , DETAIL_NUMBER_OF_COLUMNS ]; 
          
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < DETAIL_NUMBER_OF_COLUMNS; j++)
                {
                  valArray[i, j] =  tbl.Rows[i][j].GetType().Equals(typeof(System.String)) ? "'" + tbl.Rows[i][j].ToString().Trim() : tbl.Rows[i][j];
                }
            }
            return valArray;
        }

        private void reportError(Exception ex)
        {
            MessageBox.Show("ERROR: " + ex.Message);
        }
    }
}
