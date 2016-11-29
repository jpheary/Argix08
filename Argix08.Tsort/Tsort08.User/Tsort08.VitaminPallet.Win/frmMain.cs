using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using Tsort.Devices.Printers;
namespace Argix.Freight
{
    public partial class frmMain : Form
    {
         
        private const string SP_TL_GET = "uspVitaminToteCurrentTLGet", TBL_TL = "TL";
        private const string SP_LABEL_GET = "uspVitaminTotePalletOutboundLabelGet", TBL_LABEL = "LABEL";
        private const string SP_ZONE_CLOSE = "uspVitaminToteZoneClose";
        private const string SP_TOTE_TL_GET = "uspVitaminToteTLGetForCartonNumber",TBL_TOTE_TL = "TL";
        private String _outboundLabelTemplate = "";

        public frmMain()
        {
            InitializeComponent();
        }

        private void ReportError(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        private void printTL(string aTL)
        {
            String output = this.getOutboundLabelTemplate().Replace("<zoneOutboundTrailerLoadNumber>", aTL);
            ZebraWin2.SendStringToPrinter("Zebra", output);
        }

        private string getTL()
        {
            DataSet ds = null;
            try
            {
                 ds = this.fillDataset(SP_TL_GET, TBL_TL);
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading current Pallet ID (TL).", ex); }
            if (ds == null || ds.Tables[0].Rows.Count == 0)
                throw new ApplicationException("Current Pallet ID (TL) not found.");
            return ds.Tables[TBL_TL].Rows[0]["TLNumber"].ToString();
        }

        private string getTLForTote(string aCartonNumber)
        {
            DataSet ds = null;
            try
            {
                ds = this.fillDataset(SP_TOTE_TL_GET, TBL_TOTE_TL, new object [] {aCartonNumber});
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading Pallet ID for TL (sorted_item).", ex); }
 //           if (ds == null)
 //               throw new ApplicationException("Current Pallet ID (TL) not found.");
            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("Pallet for " + aCartonNumber + " not found.");
                return "";
            }
            return ds.Tables[TBL_TL].Rows[0]["TRAILER_LOAD_NUM"].ToString();
        }

        private string getOutboundLabelTemplate()
        {
            if (_outboundLabelTemplate.Length == 0)
            {
                DataSet ds = null;
                try
                {
                    ds = this.fillDataset(SP_LABEL_GET, TBL_LABEL);
                }
                catch (Exception ex) { throw new ApplicationException("Unexpected error while reading outbound label template.", ex); }
                if (ds == null || ds.Tables[0].Rows.Count == 0)
                    throw new ApplicationException("Outbound label template not found.");
                _outboundLabelTemplate = "${" + ds.Tables[TBL_LABEL].Rows[0]["LABEL_STRING"].ToString() + "}$";
            }
            return _outboundLabelTemplate;
        }

        private void CloseZone()
        {
            try
            {
                this.executeNonQuery(SP_ZONE_CLOSE,  new object[] {});
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error creating new pallet ID(close zone).", ex); }
        }


        private void frmMain_Shown(object sender, EventArgs e)
        {
            if (!this.Enabled)
            {
                try
                {
                    lblPalletID.Text = this.getTL();
                    this.getOutboundLabelTemplate();
                    this.Enabled = true;
                }
                catch (Exception ex) { this.ReportError(ex); }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try { this.printTL(lblPalletID.Text); }
            catch (Exception ex) { this.ReportError(ex);}
        }

        private void btnNewPallet_Click(object sender, EventArgs e)
        {
            try
            {
                this.CloseZone();
                lblPalletID.Text = this.getTL();
            }
            catch (Exception ex) { this.ReportError(ex); }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            try { lblTotePalletID.Text = this.getTLForTote(txtToteNumber.Text);}
            catch (Exception ex) { this.ReportError(ex); }

        }

        private void txtToteNumber_Enter(object sender, EventArgs e)
        {
            txtToteNumber.Text = "";
            lblTotePalletID.Text = "";
        }

        private void btnTotePalletPrint_Click(object sender, EventArgs e)
        {
            try { this.printTL(lblTotePalletID.Text); }                
            catch (Exception ex) { this.ReportError(ex); }
        }

        private void lblTotePalletID_TextChanged(object sender, EventArgs e)
        {
            btnTotePalletPrint.Enabled = lblTotePalletID.Text.Trim().Length > 0;
        }

        #region Data Services: fillDataset(), executeNonQuery(), executeNonQueryWithReturn()
        private DataSet fillDataset(string spName, string table, object[] paramValues)
        {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(spName, paramValues);
            db.LoadDataSet(cmd, ds, table);
            return ds;
        }
        private DataSet fillDataset(string spName, string table)
        {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            DbCommand cmd = db.GetStoredProcCommand(spName);
            db.LoadDataSet(cmd, ds, table);
            return ds;
        }
        private bool executeNonQuery(string spName, object[] paramValues)
        {
            //
            bool ret = false;
            Database db = DatabaseFactory.CreateDatabase("SQLConnection");
            int i = db.ExecuteNonQuery(spName, paramValues);
            ret = i > 0;
            return ret;
        }
        private object executeNonQueryWithReturn(string spName, object[] paramValues)
        {
            //
            object ret = null;
            if ((paramValues != null) && (paramValues.Length > 0))
            {
                Database db = DatabaseFactory.CreateDatabase("SQLConnection");
                DbCommand cmd = db.GetStoredProcCommand(spName, paramValues);
                ret = db.ExecuteNonQuery(cmd);

                //Find the output parameter and return its value
                foreach (DbParameter param in cmd.Parameters)
                {
                    if ((param.Direction == ParameterDirection.Output) || (param.Direction == ParameterDirection.InputOutput))
                    {
                        ret = param.Value;
                        break;
                    }
                }
            }
            return ret;
        }
        #endregion





    }
    
}
