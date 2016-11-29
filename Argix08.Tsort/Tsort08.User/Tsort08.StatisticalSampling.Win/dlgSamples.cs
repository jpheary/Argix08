using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.ApplicationBlocks.Data;

namespace Argix.Freight {
    //
    public partial class dlgSamples :Form {
        //Members
        private int mDateDaysBack = 365;
        private int mDateDaysForward = 0;
        private int mDateDaysSpread = 30;

        //Interface
        public dlgSamples() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch(Exception ex) { throw new ApplicationException("Error while creating new dlgSamples instance.", ex); }
        }

        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            try {
                this.dtpFrom.MinDate = this.dtpTo.MinDate = DateTime.Today.AddDays(-this.mDateDaysBack);
                this.dtpFrom.MaxDate = this.dtpTo.MaxDate = DateTime.Today.AddDays(this.mDateDaysForward + 1).AddSeconds(-1);
                this.dtpTo.Value = DateTime.Today.AddDays(1).AddSeconds(-1);
                if(this.mDateDaysBack >= this.mDateDaysSpread)
                    this.dtpFrom.Value = DateTime.Today.AddDays(-this.mDateDaysSpread);
                else
                    this.dtpFrom.Value = DateTime.Today.AddDays(-this.mDateDaysBack);
                OnRefresh(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error on loading dlgSample.", ex), true); }
        }
        private void OnDateChanged(object sender,EventArgs e) {
            //Event handler for change in selected date
            try {
                //From date cannot exceed to date- from pushes to forward; to pushes from backward
                DateTimePicker dtp = (DateTimePicker)sender;
                if(dtp == this.dtpFrom) {
                    if(this.dtpFrom.Value.CompareTo(this.dtpTo.Value) > 0)
                        this.dtpTo.Value = this.dtpFrom.Value;
                    else if(this.dtpFrom.Value.CompareTo(this.dtpTo.Value.AddDays(-this.mDateDaysSpread)) < 0)
                        this.dtpTo.Value = this.dtpFrom.Value.AddDays(this.mDateDaysSpread);
                }
                else if(dtp == this.dtpTo) {
                    if(this.dtpTo.Value.CompareTo(this.dtpFrom.Value) < 0)
                        this.dtpFrom.Value = this.dtpTo.Value;
                    else if(this.dtpTo.Value.CompareTo(this.dtpFrom.Value.AddDays(this.mDateDaysSpread)) > 0)
                        this.dtpFrom.Value = this.dtpTo.Value.AddDays(-this.mDateDaysSpread);
                }
                OnRefresh(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex,true); }
        }
        private void OnRefresh(object sender,EventArgs e) {
            //Event handler for refresh button clicked
            try {
                //Refresh
                this.mSampleDS.Clear();
                DataSet ds = App.Mediator.FillDataset(App.USP_SAMPLES_GETLIST,App.TBL_SAMPLES_GETLIST,new object[] { this.dtpFrom.Value,this.dtpTo.Value.AddDays(1).AddSeconds(-1) });
                this.mSampleDS.Merge(ds);

                //Tally ASNs and units
                System.Collections.Hashtable asns = new System.Collections.Hashtable();
                int totalUnits = 0;
                for (int i = 0; i < this.mSampleDS.SampleTable.Rows.Count; i++) {
                    string vendorCartonNumber = this.mSampleDS.SampleTable[i].VendorCartonNumber;
                    int units = this.mSampleDS.SampleTable[i].ItemCount + this.mSampleDS.SampleTable[i].DamageCount;
                    totalUnits += units;
                    if(!asns.ContainsKey(vendorCartonNumber)) {
                        asns.Add(vendorCartonNumber, units);
                    }
                    else {
                        asns[vendorCartonNumber] = (int)asns[vendorCartonNumber] + units;
                    }
                }
                this.slASNs.Text = asns.Count.ToString();
                this.slUnits.Text = totalUnits.ToString();
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error when refreshing sample data.",ex),true); }
        }
        private void OnClose(object sender,EventArgs e) {
            //Event handler for close button clicked
            try {
                this.Close();
            }
            catch(Exception ex) { App.ReportError(ex,true); }
        }
        private void OnExport(object sender,EventArgs e) {
            //Event handler for close button clicked
            try {
                SaveFileDialog dlgSave = new SaveFileDialog();
                dlgSave.AddExtension = true;
                dlgSave.Filter = "Export Files (*.txt) | *.txt";
                dlgSave.FilterIndex = 0;
                dlgSave.Title = "Export Samples...";
                dlgSave.FileName = "statsamp-" + DateTime.Today.ToString("yyyyMMdd") + "-" + this.slASNs.Text + "asns-" + this.slUnits.Text + "units";
                dlgSave.OverwritePrompt = true;
                if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                    if(export(dlgSave.FileName)) 
                        MessageBox.Show("Sample export completed.");
                    else 
                        MessageBox.Show("Sample export failed.");
                }
                OnRefresh(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error on sample export.",ex),true); }
        }

        private bool export(string exportFile) {
            //Export samples to CSV text file
            bool exported = false;
            StreamWriter writer = null;
            try {
                //Validate file is unique
                if(File.Exists(exportFile))
                    throw new Exception("Export file " + exportFile + " already exists. ");

                //Create the new file and save sorted items for this pickup to disk
                writer = new StreamWriter(new FileStream(exportFile,FileMode.Create,FileAccess.ReadWrite));
                writer.BaseStream.Seek(0,SeekOrigin.Begin);
                for(int j = 0; j < this.mSampleDS.SampleTable.Rows.Count; j++) {
                    //Only non-transacted?
                    //if(this.mSampleDS.SampleTable[j].IsTransactionDateNull())
                    writer.WriteLine(formatSampleRecord((StatSampleDS.SampleTableRow)this.mSampleDS.SampleTable.Rows[j]));
                }
                writer.Flush();
                exported = true;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting carton samples.",ex); }
            finally { if(writer != null) writer.Close(); }
            return exported;
        }
        private string formatSampleRecord(StatSampleDS.SampleTableRow sample) {
            //Returns a string formatted by concatination of the Catons row columns
            //ASN           20  
            //EAN/UPC       13  
            //Sampled       8   yyyymmdd
            //Good          5   
            //Damaged       5   
            //Argix Vendor  25    
            const string DELIM = ",";
            string item = "";
            bool ret = false;
            try {
                ret = App.Mediator.ExecuteNonQuery(App.USP_SAMPLES_UPDATE,new object[] { sample.ID });
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while updating sample transaction date.",ex); }

            try {
                if(ret) {
                    item = sample.VendorCartonNumber.Trim().PadLeft(20, '0') + DELIM +
                            sample.ItemNumber.Trim().PadLeft(13,'0') + DELIM +
                            sample.SampleDate.ToString("yyyyMMdd") + DELIM +
                            sample.ItemCount.ToString().Trim() + DELIM +
                            sample.DamageCount.ToString().Trim() + DELIM +
                            sample.VendorName.Trim();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting sample export string.",ex); }
            return item;
        }
    }
}