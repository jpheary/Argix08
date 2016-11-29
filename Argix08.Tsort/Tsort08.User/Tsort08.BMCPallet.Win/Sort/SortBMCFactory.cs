using System;
using System.Collections.Generic;
using System.Text;
using Argix.Data;
using System.Data;
using Tsort;


namespace Tsort.Sort
{
    class SortBMCFactory
    {
        //Members
        public static Mediator Mediator = null;
                
        //Interface
		static SortBMCFactory() {
           
        }
        public static Pallet GetPallet(string labelSequenceNumber)
        {
            //Return a pallet
            Pallet pallet = null;
            PalletDS palletDS = new PalletDS();
            DataSet ds = null;
            try
            {
                ds = Mediator.FillDataset(App.USP_BMCPALLETGET, App.TBL_PALLET, new object[] { labelSequenceNumber });
                palletDS.Merge(ds, false, MissingSchemaAction.Ignore);
                if (palletDS.PalletTable.Rows.Count > 0)
                        pallet = new Pallet((PalletDS.PalletTableRow)palletDS.PalletTable.Rows[0]);
                
            }
            catch (Exception ex) { throw new ApplicationException("Failed to get pallet.", ex); }
            return pallet;
        }

        public static void PalletCartonNew(string aPalletLabelSequenceNumber, string aCartonLabelSequenceNumber)
        {
            //Return an existing shipment from the inbound freight collection
           try
            {
                Mediator.ExecuteNonQuery(App.USP_BMCPALLETCARTONNEW, new object[] { aPalletLabelSequenceNumber, aCartonLabelSequenceNumber });
        
            }
            catch (Exception ex) { throw new ApplicationException("Failed to record carton.", ex); }
        }

        public static void PalletCartonDelete(string aCartonLabelSequenceNumber)
        {
            //Return an existing shipment from the inbound freight collection
            try
            {
                Mediator.ExecuteNonQuery(App.USP_BMCPALLETCARTONDELETE, new object[] { aCartonLabelSequenceNumber });

            }
            catch (Exception ex) { throw new ApplicationException("Failed to delete carton.", ex); }
        }
        
    }
}
