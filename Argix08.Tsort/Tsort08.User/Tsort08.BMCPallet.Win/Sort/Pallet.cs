using System;
using System.Collections.Generic;
using System.Text;


namespace Tsort.Sort
{
    //	File:	pellet.cs
    //	Author:	MK
    //	Date:	3/30/09
    //	Desc:	User layer business class for a Pallet.
    //	Rev:	
    //
    class Pallet
    {
        //Members
        private string _labelSequenceNumber = "";
        private string _clientNumber = "";
        private string _vendorNumber = "";
        private DateTime _pickupDate = Convert.ToDateTime(null);
        private int _pickupNumber = 0;
        private string _vendorKey = "";
        private string _zone = "";
        private string _tl = "";
        private int _storeNumber = 0;
        private string _cartonNumber = "";
        private float _weight = 0;
        private DateTime _sortDate = Convert.ToDateTime(null);


        //Interface
        public Pallet(PalletDS.PalletTableRow pallet)
        {
            //Constructor
            try
            {
                if (pallet != null)
                {
                    this._labelSequenceNumber = pallet.LABEL_SEQ_NUMBER;
                    this._clientNumber = pallet.CLIENT_NUMBER;
                    this._vendorNumber = pallet.VENDOR_NUMBER;
                    this._pickupDate = pallet.PICKUP_DATE;
                    this._pickupNumber = pallet.PICKUP_NUMBER;
                    this._zone = pallet.ZONE_CODE;
                    this._tl = pallet.TRAILER_LOAD_NUM;
                    this._vendorKey = pallet.VENDOR_KEY;
                    this._cartonNumber = pallet.VENDOR_ITEM_NUMBER;
                    this._storeNumber = pallet.STORE;
                    this._sortDate = pallet.SORT_DATE;
                }
            } catch (Exception ex) { throw new ApplicationException("Could not create a new pallet.", ex); }
         }
        // Accessors
		public string LabelSequenceNumber { get { return this._labelSequenceNumber; } }
        public string ClientNumber { get { return this._clientNumber; } }
        public string VendorNumber { get { return this._vendorNumber; } }
        public DateTime PickupDate { get { return this._pickupDate; } }
        public int PickupNumber { get { return this._pickupNumber; } }
        public string Zone { get { return this._zone; } }
        public string TL { get { return this._tl; } }
        public string VendorKey { get { return this._vendorKey; } }
        public int Store { get { return this._storeNumber; } }
        public DateTime SortDate { get { return this._sortDate; } }
        public float Weight { get { return this._weight; } }

        


    }
}
