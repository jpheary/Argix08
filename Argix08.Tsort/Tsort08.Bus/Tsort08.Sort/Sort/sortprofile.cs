//	File:		sortprofile.cs
//	Author:		jheary
//	Date:		03/14/06
//	Desc:		Provides a profile of how an inbound shipment is specified by
//				the freight assignment to be sorted
//	Rev:		
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using Tsort.Freight;

namespace Tsort.Sort {
    //
    internal class SortProfile {
        //Members
        private string mFreightType="Tsort";		//ISA, Returns, Tsort
        private int mSortTypeID=2;					//1=SAN, 2=Regular, 3=...
        private string mSortType="Regular";			//SAN, Regular, JIT, SKU, Returns,...
        private string mClientNumber="";
        private string mClientDivision="";
        private string mVendorNumber="";
        private string mStatus="A";					//A=active; I=inactive
        private int mExceptionDeliveryLocation=0;	//Alternate delivery location
        private int mLabelID=0;						//New DB design label
        private InboundLabel mInboundLabel=null;	//Expected inbound label

        //Constants
        //Events
        //Interface
        public SortProfile(SortProfileDS.SortProfileTableRow profile) {
            //Constructor
            try {
                if(profile != null) {
                    this.mFreightType = profile.FreightType;
                    this.mSortTypeID = profile.SortTypeID;
                    this.mSortType = profile.SortType;
                    this.mClientNumber = profile.ClientNumber;
                    this.mClientDivision = profile.ClientDivision;
                    if(!profile.IsVendorNumberNull()) this.mVendorNumber = profile.VendorNumber;
                    this.mStatus = profile.Status;
                    if(!profile.IsExceptionLocationNull()) this.mExceptionDeliveryLocation = profile.ExceptionLocation;
                    if(!profile.IsLabelIDNull()) this.mLabelID = profile.LabelID;

                    //Create the inbound label for this profile;
                    this.mInboundLabel = FreightFactory.CreateInboundLabel(this.mLabelID);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Sort Profile instance.",ex); }
        }
        #region Accessors\Modifiers: [Members]..., ToDataSet()
        public string FreightType { get { return this.mFreightType; } }
        public int SortTypeID { get { return this.mSortTypeID; } }
        public string SortType { get { return this.mSortType; } }
        public string ClientNumber { get { return this.mClientNumber; } }
        public string ClientDivision { get { return this.mClientDivision; } }
        public string VendorNumber { get { return this.mVendorNumber; } }
        public string Status { get { return this.mStatus; } }
        public int ExceptionLocation { get { return this.mExceptionDeliveryLocation; } }
        public int LabelID { get { return this.mLabelID; } }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            SortProfileDS ds=null;
            try {
                ds = new SortProfileDS();
                SortProfileDS.SortProfileTableRow profile = ds.SortProfileTable.NewSortProfileTableRow();
                profile.FreightType = this.mFreightType;
                profile.SortTypeID = this.mSortTypeID;
                profile.SortType = this.mSortType;
                profile.ClientNumber = this.mClientNumber;
                profile.ClientDivision = this.mClientDivision;
                if(this.mVendorNumber.Length > 0) profile.VendorNumber = this.mVendorNumber;
                profile.Status = this.mStatus;
                if(this.mExceptionDeliveryLocation > 0) profile.ExceptionLocation = this.mExceptionDeliveryLocation;
                if(this.mLabelID > 0) profile.LabelID = this.mLabelID;
                ds.SortProfileTable.AddSortProfileTableRow(profile);
                ds.Merge(this.mInboundLabel.ToDataSet());
                ds.AcceptChanges();
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
        public InboundLabel InboundLabel { get { return this.mInboundLabel; } }
        public bool IsSAN { get { return this.mSortType.ToLower().IndexOf("san") != -1; } }
    }
}