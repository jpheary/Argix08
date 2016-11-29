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
        private const string USP_SHIPMENT = "uspInvBenettonShipmentGetList",TBL_SHIPMENT = "InvoiceShipmentTable";
        private const string USP_DETAIL = "uspInvBenettonDetailGetList",TBL_DETAIL = "InvoiceDetailTable";
        private const string USP_TOTALS = "uspInvBenettonHeaderGet",TBL_TOTALS = "InvoiceTotalTable";
        private const int ROW0_DETAIL=6;

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender,System.EventArgs e) {
            //Event handler for workbook startup event
            try {
                System.IntPtr p = GetCommandLine();
                string cmd = System.Runtime.InteropServices.Marshal.PtrToStringAuto(p);
                string clid="",invoice="";  //L15427
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
                    //Get shipments and shipment details
                    SqlDataAdapter adapter = new SqlDataAdapter(USP_SHIPMENT,global::Argix.Finance.Settings.Default.SQLConnection);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                    InvoiceDS ds = new InvoiceDS();
                    adapter.Fill(ds,TBL_SHIPMENT);
                    adapter.SelectCommand.CommandText = USP_DETAIL;
                    adapter.Fill(ds,TBL_DETAIL);
                    adapter.SelectCommand.CommandText = USP_TOTALS;
                    adapter.Fill(ds,TBL_TOTALS);

                    int rows = ROW0_DETAIL;
                    createInvoiceHeader(invoice,ds.InvoiceTotalTable[0]);
                    for(int i=0; i<ds.InvoiceShipmentTable.Rows.Count; i++) {
                        //Show each shipment
                        rows = rows + createInvoiceBodyShipments(ds.InvoiceShipmentTable[i],rows);

                        //Show details for each shipment
                        InvoiceDS _ds = new InvoiceDS();
                        _ds.Merge(ds.InvoiceDetailTable.Select("ShipmentNumber='" + ds.InvoiceShipmentTable[i].ShipmentNumber + "'"));
                        rows = rows + createInvoiceBodyDetails(_ds,rows);
                        rows++;     //Add break
                    }
                    createInvoiceTotals(ds.InvoiceTotalTable[0],rows);
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
        private void createInvoiceHeader(string invoiceNumber, InvoiceDS.InvoiceTotalTableRow total) {
            Invoice invoice = global::Argix.Finance.Globals.Invoice;

            //Remit To
            invoice.RemitToName.Value = total.RemitToName.Trim();
            invoice.RemitToAddressLine1.Value = total.RemitToAddressLine1.Trim();
            invoice.RemitToAddressLine2.Value = total.IsRemitToAddressLine2Null() ? "" : total.RemitToAddressLine2.Trim();
            invoice.RemitToCityStateZip.Value = total.RemitToCity.Trim() + ", " + total.RemitToState.Trim() + " " + total.RemitToZip.Trim() + "-" + total.RemitToZip4;
            invoice.Telephone.Value = (total.IsTelephoneNull() ? "" : total.Telephone.ToString());

            //Bill To
            invoice.BillToName.Value = "Benetton";
            invoice.BillToAddressLine1.Value = invoice.BillToAddressLine2.Value = invoice.BillToCityStateZip.Value = "";

            //Account
            invoice.InvoiceNumber.Value = invoiceNumber;
            invoice.InvoiceDate.Value = total.InvoiceDate;
        }
        private int createInvoiceBodyShipments(InvoiceDS.InvoiceShipmentTableRow shipment,int row0) {
            //Get worksheet
            int _rows = 0;
            Invoice invoice = global::Argix.Finance.Globals.Invoice;

            //Column headers
            _rows += 5;
            Excel.Range r = invoice.get_Range(invoice.Cells[row0 + 1,1],invoice.Cells[row0 + 1,12]);
            r.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black.ToArgb();
            invoice.get_Range(invoice.Cells[row0 + 1,2],invoice.Cells[row0 + 1,12]).Value2 = new object[1,11] { { "Inland Charges","","","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 1,2],invoice.Cells[row0 + 1,2]).Font.Bold = true;
            invoice.get_Range(invoice.Cells[row0 + 2,2],invoice.Cells[row0 + 2,12]).Value2 = new object[1,11] { { "","","","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 3,2],invoice.Cells[row0 + 3,12]).Value2 = new object[1,11] { { "","","Trucking","Fuel","Devanning","","Airline","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 4,2],invoice.Cells[row0 + 4,12]).Value2 = new object[1,11] { { "","Custom DOC","Rate","Surcharge","Unite","Insurance","Import Fee","House B/L","Miscellaneous","Total Inland","" } };
            invoice.get_Range(invoice.Cells[row0 + 5,2],invoice.Cells[row0 + 5,12]).Value2 = new object[1,11] { { "Shipment#","Charges","Charges","Charges","Charges","Charges","Charges","Charges","Charges","Charges","" } };
            r = invoice.get_Range(invoice.Cells[row0 + 5,2],invoice.Cells[row0 + 5,11]);
            r.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black.ToArgb();
            
            //Set named range summary values
            _rows += 1;
            object[,] _shipment = new object[1,10];
            _shipment[0,0] = shipment.ShipmentNumber;
            _shipment[0,1] = shipment.CustomDocCharges;
            _shipment[0,2] = shipment.TruckingRatesCharges;
            _shipment[0,3] = shipment.FSCCharges;
            _shipment[0,4] = shipment.DevanningUniteChargtes;
            _shipment[0,5] = shipment.InsuranceCharges;
            _shipment[0,6] = shipment.AirLineImportFeeCharges;
            _shipment[0,7] = shipment.HouseBLCharges;
            _shipment[0,8] = shipment.MiscellaneousCharges;
            _shipment[0,9] = shipment.TotalInlandCharges;
            invoice.get_Range(invoice.Cells[row0 + 6,2],invoice.Cells[row0 + 6,11]).Value2 = _shipment;
            invoice.get_Range(invoice.Cells[row0 + 6,2],invoice.Cells[row0 + 6,2]).NumberFormat = "@";
            invoice.get_Range(invoice.Cells[row0 + 6,3],invoice.Cells[row0 + 6,3]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,4],invoice.Cells[row0 + 6,4]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,5],invoice.Cells[row0 + 6,5]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,6],invoice.Cells[row0 + 6,6]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,7],invoice.Cells[row0 + 6,7]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,8],invoice.Cells[row0 + 6,8]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,9],invoice.Cells[row0 + 6,9]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,10],invoice.Cells[row0 + 6,10]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 6,11],invoice.Cells[row0 + 6,11]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            return _rows;
        }
        private int createInvoiceBodyDetails(InvoiceDS ds, int row0) {
            //Get worksheet
            int _rows = 0;
            Invoice invoice = global::Argix.Finance.Globals.Invoice;

            //Column headers
            _rows += 6;
            invoice.get_Range(invoice.Cells[row0 + 1,2],invoice.Cells[row0 + 1,12]).Value2 = new object[1,11] { { "","","","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 2,2],invoice.Cells[row0 + 2,12]).Value2 = new object[1,11] { { "Distribution Charges","","","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 2,2],invoice.Cells[row0 + 2,2]).Font.Bold = true;
            invoice.get_Range(invoice.Cells[row0 + 3,2],invoice.Cells[row0 + 3,12]).Value2 = new object[1,11] { { "","","","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 4,2],invoice.Cells[row0 + 4,12]).Value2 = new object[1,11] { { "","Delivery","","","","","","","Fuel","Fuel","Total Distrib." } };
            invoice.get_Range(invoice.Cells[row0 + 5,2],invoice.Cells[row0 + 5,12]).Value2 = new object[1,11] { { "Zone","Code","Store","Cartons","Weight(kg)","Weight(lbs)","Rate","Amount","Surcharge %","Surcharge","Charges" } };
            Excel.Range r = invoice.get_Range(invoice.Cells[row0 + 5,2],invoice.Cells[row0 + 5,12]);
            r.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black.ToArgb();

            //Detail
            decimal totalDistributionCharges = 0.0M;
            int rows = ds.InvoiceDetailTable.Rows.Count;
            if(rows > 0) {
                _rows += rows;
                object[,] details = new object[rows,11];
                for(int i = 0; i < rows; i++) {
                    details[i,0] = ds.InvoiceDetailTable[i].Zone;
                    details[i,1] = ds.InvoiceDetailTable[i].DeliveryCode;
                    details[i,2] = ds.InvoiceDetailTable[i].Store;
                    details[i,3] = ds.InvoiceDetailTable[i].Cartons;
                    details[i,4] = ds.InvoiceDetailTable[i].WeightKg;
                    details[i,5] = ds.InvoiceDetailTable[i].Weight;
                    details[i,6] = ds.InvoiceDetailTable[i].Rate;
                    details[i,7] = ds.InvoiceDetailTable[i].Amount;
                    details[i,8] = ds.InvoiceDetailTable[i].FSCSurchargePCT / 100;
                    details[i,9] = ds.InvoiceDetailTable[i].FuelSurcharge;
                    details[i,10] = ds.InvoiceDetailTable[i].TotalDistributionCharges;
                    totalDistributionCharges += ds.InvoiceDetailTable[i].TotalDistributionCharges;
                }
                invoice.get_Range(invoice.Cells[row0 + 6,2],invoice.Cells[row0 + 6 + rows - 1,12]).Value2 = details;
                invoice.get_Range(invoice.Cells[row0 + 6,2],invoice.Cells[row0 + 6 + rows - 1,2]).NumberFormat = "@";
                invoice.get_Range(invoice.Cells[row0 + 6,3],invoice.Cells[row0 + 6 + rows - 1,3]).NumberFormat = "@";
                invoice.get_Range(invoice.Cells[row0 + 6,4],invoice.Cells[row0 + 6 + rows - 1,4]).NumberFormat = "@";
                invoice.get_Range(invoice.Cells[row0 + 6,5],invoice.Cells[row0 + 6 + rows - 1,5]).NumberFormat = "#0";
                invoice.get_Range(invoice.Cells[row0 + 6,6],invoice.Cells[row0 + 6 + rows - 1,6]).NumberFormat = "#0.00";
                invoice.get_Range(invoice.Cells[row0 + 6,7],invoice.Cells[row0 + 6 + rows - 1,7]).NumberFormat = "#,##0";
                invoice.get_Range(invoice.Cells[row0 + 6,8],invoice.Cells[row0 + 6 + rows - 1,8]).NumberFormat = "$#,##0.00_);($#,##0.00)";
                invoice.get_Range(invoice.Cells[row0 + 6,9],invoice.Cells[row0 + 6 + rows - 1,9]).NumberFormat = "$#,##0.00_);($#,##0.00)";
                invoice.get_Range(invoice.Cells[row0 + 6,10],invoice.Cells[row0 + 6 + rows - 1,10]).NumberFormat = "#0.0000 %";
                invoice.get_Range(invoice.Cells[row0 + 6,11],invoice.Cells[row0 + 6 + rows - 1,11]).NumberFormat = "$#,##0.00_);($#,##0.00)";
                invoice.get_Range(invoice.Cells[row0 + 6,12],invoice.Cells[row0 + 6 + rows - 1,12]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            }
            invoice.get_Range(invoice.Cells[row0 + 6 + rows,2],invoice.Cells[row0 + 6 + rows,12]).Value2 = new object[1,11] { { "","","","","","","","","","Total:",totalDistributionCharges } };
            invoice.get_Range(invoice.Cells[row0 + 6 + rows,11],invoice.Cells[row0 + 6 + rows,11]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            invoice.get_Range(invoice.Cells[row0 + 6 + rows,12],invoice.Cells[row0 + 6 + rows,12]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            return _rows;
        }
        private int createInvoiceTotals(InvoiceDS.InvoiceTotalTableRow total,int row0) {
            //Get worksheet
            int _rows = 0;
            Invoice invoice = global::Argix.Finance.Globals.Invoice;

            //Header
            _rows += 4;
            Excel.Range r = invoice.get_Range(invoice.Cells[row0 + 1,1],invoice.Cells[row0 + 1,12]);
            r.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black.ToArgb();
            invoice.get_Range(invoice.Cells[row0 + 1,2],invoice.Cells[row0 + 1,12]).Value2 = new object[1,11] { { "Invoice Total","","","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 1,2],invoice.Cells[row0 + 1,2]).Font.Bold = true;
            invoice.get_Range(invoice.Cells[row0 + 2,2],invoice.Cells[row0 + 2,12]).Value2 = new object[1,11] { { "","","","","","","","","","","" } };
            invoice.get_Range(invoice.Cells[row0 + 3,2],invoice.Cells[row0 + 3,12]).Value2 = new object[1,11] { { "","","","Inland","Distribution","Total","","","","","" } };
            r = invoice.get_Range(invoice.Cells[row0 + 4,2],invoice.Cells[row0 + 4,12]);
            r.Value2 = new object[1,11] { { "Cartons","Weight(kg)","Weight(lbs)","Charges","Charges","Charges","","","","","" } };
            r.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Black.ToArgb();

            //Set named range summary values
            _rows += 1;
            object[,] totals = new object[1,6];
            totals[0,0] = total.Cartons;
            totals[0,1] = total.WeightKg;
            totals[0,2] = total.Weight;
            totals[0,3] = total.InlandCharges;
            totals[0,4] = total.DistributionCharges;
            totals[0,5] = total.TotalCharges;
            invoice.get_Range(invoice.Cells[row0 + 5,2],invoice.Cells[row0 + 5,7]).Value2 = totals;
            invoice.get_Range(invoice.Cells[row0 + 5,2],invoice.Cells[row0 + 5,2]).NumberFormat = "#,##0";
            invoice.get_Range(invoice.Cells[row0 + 5,3],invoice.Cells[row0 + 5,3]).NumberFormat = "#,##0.00";
            invoice.get_Range(invoice.Cells[row0 + 5,4],invoice.Cells[row0 + 5,4]).NumberFormat = "#,##0";
            invoice.get_Range(invoice.Cells[row0 + 5,5],invoice.Cells[row0 + 5,5]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 5,6],invoice.Cells[row0 + 5,6]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            invoice.get_Range(invoice.Cells[row0 + 5,7],invoice.Cells[row0 + 5,7]).NumberFormat = "$#,##0.00_);($#,##0.00)";
            return _rows;
        }
        private void reportError(Exception ex) { MessageBox.Show("UNEXPECTED ERROR: " + ex.Message); }
    }
}
