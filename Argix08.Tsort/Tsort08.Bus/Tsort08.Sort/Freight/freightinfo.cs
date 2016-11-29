//	File:	freightinfo.cs
//	Author:	J. Heary
//	Date:	02/07/05
//	Desc:	Represents the state and behavior of an Argix Inbound Label for
//			regular freight (old design).
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Tsort.Freight {
	//
	public class FreightInfo {
		//Members
		private string mType="";
		private short mInput1Length=0;
		private short mInput2Length=0;
		private short mInput3Length=0;
		private short mStoreBeginPos=0;
		private short mStoreEndPos=0;
		private short mCartonBeginPos=0;
		private short mCartonEndPos=0;
		private string mCartonInInput1="";
		private string mDupCartonsOK="";
		private string mCartonsNbrReq="";
		private string mCartonValidation="";
		private string mUseAltStore="";
		private string mValidationString="";
		private int mValidationBegin=0;
		private int mValidationEnd=0;
		private int mPOStart=0;
		private int mPOLength=0;
		private int mPOInputNumber=0;
		
		//Constants
		//Events
		//Interface
		public FreightInfo(InboundLabelDS.FreightInfoRegTableRow label) { 
			//Constructor
			try { 
				this.mType = label.TYPE;
				this.mInput1Length = label.INPUT1LENGTH;
				if(!label.IsINPUT2LENGTHNull()) this.mInput2Length = label.INPUT2LENGTH;
				if(!label.IsINPUT3LENGTHNull()) this.mInput3Length = label.INPUT3LENGTH;
				this.mStoreBeginPos = label.STORE_BEGIN_POS;
				this.mStoreEndPos = label.STORE_END_POS;
				if(!label.IsCARTON_BEGIN_POSNull()) this.mCartonBeginPos = label.CARTON_BEGIN_POS;
				if(!label.IsCARTON_END_POSNull()) this.mCartonEndPos = label.CARTON_END_POS;
				if(!label.IsCARTON_IN_INPUT1Null()) this.mCartonInInput1 = label.CARTON_IN_INPUT1;
				if(!label.IsDUP_CARTONS_OKNull()) this.mDupCartonsOK = label.DUP_CARTONS_OK;
				if(!label.IsCARTONS_NBR_REQNull()) this.mCartonsNbrReq = label.CARTONS_NBR_REQ;
				if(!label.IsCRTN_VALIDATIONNull()) this.mCartonValidation = label.CRTN_VALIDATION;
				if(!label.IsUSE_ALT_STORENull()) this.mUseAltStore = label.USE_ALT_STORE;
				if(!label.IsVALIDATION_STRINGNull()) this.mValidationString = label.VALIDATION_STRING.Trim();
				if(!label.IsVALIDATION_BEGINNull()) this.mValidationBegin = label.VALIDATION_BEGIN;
				if(!label.IsVALIDATION_ENDNull()) this.mValidationEnd = label.VALIDATION_END;
				if(!label.IsPOStartNull()) this.mPOStart = label.POStart;
				if(!label.IsPOLengthNull()) this.mPOLength = label.POLength;
				if(!label.IsPOInputNumberNull()) this.mPOInputNumber = label.POInputNumber;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Freight Info instance.", ex); }
		}
		#region Accessors\Modifiers: [Members]..., ToDataSet()
		public string Type { get { return this.mType; } }
		public short Input1Length { get { return this.mInput1Length; } }
		public short Input2Length { get { return this.mInput2Length; } }
		public short Input3Length { get { return this.mInput3Length; } }
		public short StoreBeginPos { get { return this.mStoreBeginPos; } }
		public short StoreEndPos { get { return this.mStoreEndPos; } }
		public short CartonBeginPos { get { return this.mCartonBeginPos; } }
		public short CartonEndPos { get { return this.mCartonEndPos; } }
		public string CartonInInput1 { get { return this.mCartonInInput1; } }
		public string DupCartonsOK { get { return this.mDupCartonsOK; } }
		public string CartonsNbrReq { get { return this.mCartonsNbrReq; } }
		public string CartonValidation { get { return this.mCartonValidation; } }
		public string UseAltStore { get { return this.mUseAltStore; } }
		public string ValidationString { get { return this.mValidationString; } }
		public int ValidationBegin { get { return this.mValidationBegin; } }
		public int ValidationEnd { get { return this.mValidationEnd; } }
		public int POStart { get { return this.mPOStart; } }
		public int POLength { get { return this.mPOLength; } }
		public int POInputNumber { get { return this.mPOInputNumber; } }
		public DataSet ToDataSet() {
			//Return a dataset containing values for this object
			InboundLabelDS ds=null;
			try {
				ds = new InboundLabelDS();
				InboundLabelDS.FreightInfoRegTableRow label = ds.FreightInfoRegTable.NewFreightInfoRegTableRow();
				label.TYPE = this.mType;
				label.INPUT1LENGTH = this.mInput1Length;
				label.INPUT2LENGTH = this.mInput2Length;
				label.INPUT3LENGTH = this.mInput3Length;
				label.STORE_BEGIN_POS = this.mStoreBeginPos;
				label.STORE_END_POS = this.mStoreEndPos;
				label.CARTON_BEGIN_POS = this.mCartonBeginPos;
				label.CARTON_END_POS = this.mCartonEndPos;
				label.CARTON_IN_INPUT1 = this.mCartonInInput1;
				label.DUP_CARTONS_OK = this.mDupCartonsOK;
				label.CARTONS_NBR_REQ = this.mCartonsNbrReq;
				label.CRTN_VALIDATION = this.mCartonValidation;
				label.USE_ALT_STORE = this.mUseAltStore;
				label.VALIDATION_STRING = this.mValidationString;
				label.VALIDATION_BEGIN = this.mValidationBegin;
				label.VALIDATION_END = this.mValidationEnd;
				label.POStart = this.mPOStart;
				label.POLength = this.mPOLength;
				label.POInputNumber = this.mPOInputNumber;
				ds.FreightInfoRegTable.AddFreightInfoRegTableRow(label);
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
	}
}
