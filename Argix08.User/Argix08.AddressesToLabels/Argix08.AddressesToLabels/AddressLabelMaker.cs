using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using Tsort.Labels;

namespace Argix {
	//
	public class AddressLabelMaker: LabelMaker {
		//Members
        private string mCSVRecord="";
        
		//Interface
        public AddressLabelMaker(string csvRecord) { this.mCSVRecord = csvRecord; }
		protected override void setLabelTokenValues() { 
			//Override token values with this objects data
			string[] tokens = this.mCSVRecord.Split(',');
			
			base.mTokens[TokenLibrary.CLIENTNAME] = tokens[1];
			base.mTokens[TokenLibrary.CLIENTADDRESSLINE1] = tokens[2];
		    base.mTokens[TokenLibrary.CLIENTADDRESSLINE2] = tokens[3];
		    base.mTokens[TokenLibrary.CLIENTADDRESSCITY] = tokens[4];
		    base.mTokens[TokenLibrary.CLIENTADDRESSSTATE] = tokens[5];
		    base.mTokens[TokenLibrary.CLIENTADDRESSZIP] = tokens[6];
		    
			base.mTokens[TokenLibrary.STORENAME] = tokens[7];
		    base.mTokens[TokenLibrary.STOREADDRESSLINE1] = tokens[8];
		    base.mTokens[TokenLibrary.STOREADDRESSLINE2] = tokens[9];
		    base.mTokens[TokenLibrary.STOREADDRESSCITY] = tokens[10];
		    base.mTokens[TokenLibrary.STOREADDRESSSTATE] = tokens[11];
		    base.mTokens[TokenLibrary.STOREADDRESSZIP] = tokens[12];
		}
		public override string Name { get { return "Address Label Maker"; } }
	}
}