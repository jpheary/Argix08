using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace Argix.Finance {
    //
    public partial class ThisWorkbook {
        //Members
        private const string USP_SHIPMENT = "uspInvAmscanInvoiceByReleaseDateGet",TBL_SHIPMENT = "InvoiceShipmentTable";

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender,System.EventArgs e) {
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
                    //Display shipment data
                    SqlDataAdapter adapter = new SqlDataAdapter(USP_SHIPMENT,global::Argix.Finance.Settings.Default.SQLConnection);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                    adapter.TableMappings.Add("Table",TBL_SHIPMENT);
                    InvoiceDS shipmentDS = new InvoiceDS();
                    adapter.Fill(shipmentDS,TBL_SHIPMENT);

                    int rows = 0;
                    rows = rows + showHeader(shipmentDS.InvoiceShipmentTable[0]);
                    rows = rows + showShipments(shipmentDS.InvoiceShipmentTable,rows);
                    showTotals(shipmentDS.InvoiceShipmentTable, rows);
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
	    private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }
        
        #endregion
        private int showHeader(InvoiceDS.InvoiceShipmentTableRow shipment) {
            //Get worksheet
            Invoice invoice = global::Argix.Finance.Globals.Invoice;
            float w = (float)(5 * invoice.StandardWidth),h = (float)invoice.StandardHeight;
            invoice.Shapes.AddLine(0,h / 2,16 * w,h / 2);
            invoice.get_Range(invoice.Cells[2,1],invoice.Cells[2,11]).Value2 = new object[1,11] { { shipment.RemitToName.Trim(),"","",shipment.RemitToAddressLine1.Trim() + " " + shipment.RemitToAddressLine2.Trim() + " " + shipment.RemitToCity.Trim() + ", " + shipment.RemitToState + " " + shipment.RemitToZip + "-" + shipment.RemitToZip4,"","","","","",shipment.Telephone,"" } };
            invoice.get_Range(invoice.Cells[2,10],invoice.Cells[2,11]).Merge(null);
            invoice.get_Range(invoice.Cells[2,10],invoice.Cells[2,10]).NumberFormat = "(###)_ ###-####";
            invoice.Shapes.AddLine(0,5 * h / 2,16 * w,5 * h / 2);
            invoice.get_Range(invoice.Cells[4,1],invoice.Cells[4,11]).Value2 = new object[1,11] { { "","","","","","---- INVOICE ----","","","","Invoice#: ",shipment.InvoiceNumber } };
            invoice.get_Range(invoice.Cells[4,10],invoice.Cells[4,10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[4,11],invoice.Cells[4,11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[5,1],invoice.Cells[5,11]).Value2 = new object[1,11] { { shipment.ClientNumber + " " + shipment.ClientDivision + " - ",shipment.BillToName.Trim(),"","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[5,1],invoice.Cells[5,1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[5,10],invoice.Cells[5,10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[5,11],invoice.Cells[5,11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[6,1],invoice.Cells[6,11]).Value2 = new object[1,11] { { "",shipment.BillToAddressline1.Trim(),"","","","","","","","Invoice Date: ",shipment.InvoiceDate.ToString("MM/dd/yyyy") } };
            invoice.get_Range(invoice.Cells[6,10],invoice.Cells[6,10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[6,11],invoice.Cells[6,11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[7,1],invoice.Cells[7,11]).Value2 = new object[1,11] { { "",shipment.BillToAddressline2.Trim(),"","","","","","","","Release Date: ",shipment.ReleaseDate.ToString("MM/dd/yyyy") } };
            invoice.get_Range(invoice.Cells[7,10],invoice.Cells[7,10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[7,11],invoice.Cells[7,11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[8,1],invoice.Cells[8,11]).Value2 = new object[1,11] { { "",shipment.BillToCity.Trim() + ", " + shipment.BillToState + " " + shipment.BillToZip + "-" + shipment.BillToZIP4,"","","","","","","","","" } };
            return 8;
        }
        private int showShipments(InvoiceDS.InvoiceShipmentTableDataTable shipments,int row0) {
            //Get worksheet
            int _rows = 0;
            Invoice invoice = global::Argix.Finance.Globals.Invoice;
            float w = (float)(5 * invoice.StandardWidth),h = (float)invoice.StandardHeight;

            //Header
            _rows += 4;
            invoice.Shapes.AddLine(0,(row0 + 0.5f) * h,16 * w,(row0 + 0.5f) * h);
            invoice.get_Range(invoice.Cells[row0 + 2,1],invoice.Cells[row0 + 2,16]).Value2 = new object[1,16] { { "","","","Location","Ctn","Ctn","Plt","Plt","","Rated","Weight","Sur","Consolid","Fuel","Fuel","Delivery" } };
            invoice.get_Range(invoice.Cells[row0 + 3,1],invoice.Cells[row0 + 3,16]).Value2 = new object[1,16] { { "Store Name","State","Zip","Code","Qty","Rate","Qty","Rate","Weight","Weight","Rate","Charge","Charge","Rate","Surcharge","Total" } };
            invoice.Shapes.AddLine(0,(row0 + 3 + 0.5f) * h,16 * w,(row0 + 3 + 0.5f) * h);

            //Set named range summary values
            for(int i=0;i<shipments.Rows.Count;i++) {
                _rows += 1;
                object[,] _shipment = new object[1,16];
                _shipment[0,0] = shipments[i].StoreName.Trim();
                _shipment[0,1] = shipments[i].StoreState;
                _shipment[0,2] = shipments[i].StoreZip;
                _shipment[0,3] = shipments[i].LocationCode;
                _shipment[0,4] = shipments[i].CtnQty;
                _shipment[0,5] = shipments[i].CartonRate;
                _shipment[0,6] = shipments[i].PltQty;
                _shipment[0,7] = shipments[i].PalletRate;
                _shipment[0,8] = shipments[i].Weight;
                _shipment[0,9] = shipments[i].RatedWeight;
                _shipment[0,10] = shipments[i].WeightRate;
                _shipment[0,11] = shipments[i].Surcharge;
                _shipment[0,12] = shipments[i].ConsolidationCharge;
                _shipment[0,13] = shipments[i].FuelRate;
                _shipment[0,14] = shipments[i].FuelSurcharge;
                _shipment[0,15] = shipments[i].DeliveryTotal;
                invoice.get_Range(invoice.Cells[row0 + _rows,1],invoice.Cells[row0 + _rows,16]).Value2 = _shipment;
            }
            #region Cell Formats
            invoice.get_Range(invoice.Cells[row0 + 5,1],invoice.Cells[row0 + _rows,1]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[row0 + 5,2],invoice.Cells[row0 + _rows,2]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[row0 + 5,3],invoice.Cells[row0 + _rows,3]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[row0 + 5,4],invoice.Cells[row0 + _rows,4]).HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            invoice.get_Range(invoice.Cells[row0 + 5,5],invoice.Cells[row0 + _rows,5]).NumberFormat = "#,###_);(#,###)";
            invoice.get_Range(invoice.Cells[row0 + 5,5],invoice.Cells[row0 + _rows,5]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,6],invoice.Cells[row0 + _rows,6]).NumberFormat = "#,###.##_);(#,###.##);_(* _)";
            invoice.get_Range(invoice.Cells[row0 + 5,6],invoice.Cells[row0 + _rows,6]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,7],invoice.Cells[row0 + _rows,7]).NumberFormat = "#,###_);(#,###)";
            invoice.get_Range(invoice.Cells[row0 + 5,7],invoice.Cells[row0 + _rows,7]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,8],invoice.Cells[row0 + _rows,8]).NumberFormat = "#,###.##_);(#,###.##);_(* _)";
            invoice.get_Range(invoice.Cells[row0 + 5,8],invoice.Cells[row0 + _rows,8]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,9],invoice.Cells[row0 + _rows,9]).NumberFormat = "#,##0_);(#,##0)";
            invoice.get_Range(invoice.Cells[row0 + 5,9],invoice.Cells[row0 + _rows,9]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,10],invoice.Cells[row0 + _rows,10]).NumberFormat = "#,##0_);(#,##0)";
            invoice.get_Range(invoice.Cells[row0 + 5,10],invoice.Cells[row0 + _rows,10]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,11],invoice.Cells[row0 + _rows,11]).NumberFormat = "#,##0.00_);(#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 5,11],invoice.Cells[row0 + _rows,11]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,12],invoice.Cells[row0 + _rows,12]).NumberFormat = "$#,##0.00_);($#,##0.00);_(* _)";
            invoice.get_Range(invoice.Cells[row0 + 5,12],invoice.Cells[row0 + _rows,12]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,13],invoice.Cells[row0 + _rows,13]).NumberFormat = "$#,##0.00_);($#,##0.00);_(* _)";
            invoice.get_Range(invoice.Cells[row0 + 5,13],invoice.Cells[row0 + _rows,13]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,14],invoice.Cells[row0 + _rows,14]).NumberFormat = "#,##0.0000_);(#,##0.0000)";
            invoice.get_Range(invoice.Cells[row0 + 5,14],invoice.Cells[row0 + _rows,14]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,15],invoice.Cells[row0 + _rows,15]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 5,15],invoice.Cells[row0 + _rows,15]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 5,16],invoice.Cells[row0 + _rows,16]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 5,16],invoice.Cells[row0 + _rows,16]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            #endregion
            return _rows;
        }
        private int showTotals(InvoiceDS.InvoiceShipmentTableDataTable shipments, int row0) {
            //Get worksheet
            int _rows = 0;
            Invoice invoice = global::Argix.Finance.Globals.Invoice;
            float w = (float)(5 * invoice.StandardWidth),h = (float)invoice.StandardHeight;

            //Header
            _rows += 1;
            invoice.Shapes.AddLine(0,(row0 + 0.5f) * h,16 * w,(row0 + 0.5f) * h);

            //Totals
            _rows += 1;
            object[,] totals = new object[1,16];
            int qty=0,weight=0;
            decimal fs=0.0M,dt=0.0M;
            for(int i=0;i<shipments.Rows.Count;i++) {
                qty += shipments[i].CtnQty;
                weight += shipments[i].Weight;
                fs += shipments[i].FuelSurcharge;
                dt += shipments[i].DeliveryTotal;
            }
            totals[0,0] = "TOTAL " + shipments.Rows.Count.ToString() + " DELIVERIES";
            totals[0,1] = totals[0,2] = totals[0,3] = "";
            totals[0,4] = qty;
            totals[0,5] = totals[0,6] = totals[0,7] = "";
            totals[0,8] = weight;
            totals[0,9] = totals[0,10] = totals[0,11] = totals[0,12] = totals[0,13] = "";
            totals[0,14] = fs;
            totals[0,15] = dt;
            invoice.get_Range(invoice.Cells[row0 + _rows,1],invoice.Cells[row0 + _rows,16]).Value2 = totals;

            invoice.get_Range(invoice.Cells[row0 + 2,5],invoice.Cells[row0 + _rows,5]).NumberFormat = "#,###_);(#,###)";
            invoice.get_Range(invoice.Cells[row0 + 2,5],invoice.Cells[row0 + _rows,5]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 2,9],invoice.Cells[row0 + _rows,9]).NumberFormat = "#,##0_);(#,##0)";
            invoice.get_Range(invoice.Cells[row0 + 2,9],invoice.Cells[row0 + _rows,9]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 2,15],invoice.Cells[row0 + _rows,15]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 2,15],invoice.Cells[row0 + _rows,15]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 2,16],invoice.Cells[row0 + _rows,16]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 2,16],invoice.Cells[row0 + _rows,16]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;


            //Footer
            _rows += 1;
            invoice.get_Range(invoice.Cells[row0 + _rows,1],invoice.Cells[row0 + _rows,16]).Value2 = new object[1,16] { { "","","","","","","","","","","","","","","","" } };
            _rows += 1;
            invoice.get_Range(invoice.Cells[row0 + _rows,1],invoice.Cells[row0 + _rows,16]).Value2 = new object[1,16] { { "PLEASE REFERENCE INVOICE# " + shipments[0].InvoiceNumber + " WHEN REMITTING PAYMENT I.C.C. REGULATIONS REQUIRE THAT THIS BILL BE PAID WITHIN 7 DAYS","","","","","","","","","","","","","","","" } };
            _rows += 1;
            invoice.get_Range(invoice.Cells[row0 + _rows,1],invoice.Cells[row0 + _rows,16]).Value2 = new object[1,16] { { "","","A SERVICE CHARGE OF 1.5% PER MONTH IS ADDED TO ALL PAST DUE INVOICES","","","","","","","","","","","","","" } };
            _rows += 1;
            invoice.get_Range(invoice.Cells[row0 + _rows,1],invoice.Cells[row0 + _rows,16]).Value2 = new object[1,16] { { "","","REMIT TO: " + shipments[0].RemitToName.Trim() + " " + shipments[0].RemitToAddressLine1.Trim() + " " + shipments[0].RemitToAddressLine2.Trim() + " " + shipments[0].RemitToCity.Trim() + ", " + shipments[0].RemitToState + " " + shipments[0].RemitToZip + "-" + shipments[0].RemitToZip4,"","","","","","","","","","","","","" } };
           
            return _rows;
        }
        private void reportError(Exception ex) {
            MessageBox.Show("ERROR: " + ex.Message);
        }
    }
}
