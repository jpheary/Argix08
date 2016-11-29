using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;

namespace Tsort.Labels {
    /// <summary>Classes derived from Tsort.Labels.LabelMaker create formatted outbound labels using Tsort.Labels.TokenLibrary.</summary>
	public abstract class LabelMaker {
		//Members
        /// <summary>Label template tokens.</summary>
        protected Hashtable mTokens = null;
        /// <summary>The event that occurrs when label values change.</summary>
		public event EventHandler LabelValuesChanged=null;
		
		//Interface
        /// <summary>Constructor: creates a label template token library with default values.</summary>
		public LabelMaker() { 
			//Default token values
			this.mTokens = new Hashtable();
			#region Token Values (Initial)
            this.mTokens.Add(TokenLibrary.SAN,"");
            this.mTokens.Add(TokenLibrary.LOCALLANE,"");
            this.mTokens.Add(TokenLibrary.LOCALROUTELANE,"");
            this.mTokens.Add(TokenLibrary.FREIGHTTYPE,"");
            this.mTokens.Add(TokenLibrary.CURRENTDATE,DateTime.Now.ToShortDateString());
            this.mTokens.Add(TokenLibrary.CURRENTTIME,DateTime.Now.ToShortTimeString());
            this.mTokens.Add(TokenLibrary.TIMENOW,DateTime.Now.ToString("HHmmss"));
            this.mTokens.Add(TokenLibrary.CURRENTYEAR,DateTime.Now.Year);
            this.mTokens.Add(TokenLibrary.FREIGHTPICKUPDATE,"");
            this.mTokens.Add(TokenLibrary.FREIGHTPICKUPINFO,"");
            this.mTokens.Add(TokenLibrary.FREIGHTPICKUPNUMBER,"");
            this.mTokens.Add(TokenLibrary.FREIGHTPICKUPNUMBERSTRING,"");
            this.mTokens.Add(TokenLibrary.FREIGHTVENDORKEY,"");
            this.mTokens.Add(TokenLibrary.LANEPREFIX,"");

            this.mTokens.Add(TokenLibrary.CARTONNUMBER,"");
            this.mTokens.Add(TokenLibrary.CARTONNUMBERORPONUMBER,"");
            this.mTokens.Add(TokenLibrary.SORTEDITEMLABELNUMBER,"");
            this.mTokens.Add(TokenLibrary.SORTEDITEMWEIGHTSTRING,"");
            this.mTokens.Add(TokenLibrary.ITEMDAMAGECODE,"");
            this.mTokens.Add(TokenLibrary.ITEMTYPE,"");
            this.mTokens.Add(TokenLibrary.PONUMBER,"");
            this.mTokens.Add(TokenLibrary.RETURNNUMBER,"");
            this.mTokens.Add(TokenLibrary.TLDATE,"");
            this.mTokens.Add(TokenLibrary.TLCLOSENUMBER,"");

            this.mTokens.Add(TokenLibrary.WORKSTATIONID,"");
            this.mTokens.Add(TokenLibrary.WORKSTATIONNAME,"");
            this.mTokens.Add(TokenLibrary.WORKSTATIONNUMBER,"");
            this.mTokens.Add(TokenLibrary.WORKSTATIONNUMBER2,"");
            this.mTokens.Add(TokenLibrary.WORKSTATIONDESCRIPTION,"");

            this.mTokens.Add(TokenLibrary.CLIENTNUMBER,"");
            this.mTokens.Add(TokenLibrary.CLIENTNAME,"");
            this.mTokens.Add(TokenLibrary.CLIENTABBREVIATION,"");
            this.mTokens.Add(TokenLibrary.CLIENTDIVISIONNUMBER,"");
            this.mTokens.Add(TokenLibrary.CLIENTADDRESSLINE1,"");
            this.mTokens.Add(TokenLibrary.CLIENTADDRESSLINE2,"");
            this.mTokens.Add(TokenLibrary.CLIENTADDRESSCITY,"");
            this.mTokens.Add(TokenLibrary.CLIENTADDRESSSTATE,"");
            this.mTokens.Add(TokenLibrary.CLIENTADDRESSZIP,"");
            this.mTokens.Add(TokenLibrary.CLIENTADDRESSCOUNTRYCODE,"");

            this.mTokens.Add(TokenLibrary.STORENUMBER,"");
            this.mTokens.Add(TokenLibrary.STORENAME,"");
            this.mTokens.Add(TokenLibrary.STOREADDRESSLINE1,"");
            this.mTokens.Add(TokenLibrary.STOREADDRESSLINE2,"");
            this.mTokens.Add(TokenLibrary.STOREADDRESSCITY,"");
            this.mTokens.Add(TokenLibrary.STOREADDRESSSTATE,"");
            this.mTokens.Add(TokenLibrary.STOREADDRESSZIP,"");
            this.mTokens.Add(TokenLibrary.STOREZIP,"");
            this.mTokens.Add(TokenLibrary.STOREADDRESSCOUNTRYCODE,"");
            this.mTokens.Add(TokenLibrary.STOREPHONE,"");
            this.mTokens.Add(TokenLibrary.STOREROUTE,"");
            this.mTokens.Add(TokenLibrary.STOREALTROUTE,"");
            this.mTokens.Add(TokenLibrary.STOREROUTEFIRSTCHARACTER,"");
            this.mTokens.Add(TokenLibrary.STOREROUTEFIRSTTWO,"");
            this.mTokens.Add(TokenLibrary.STOREROUTELASTFOUR,"");
            this.mTokens.Add(TokenLibrary.STOREROUTELASTTHREE,"");
            this.mTokens.Add(TokenLibrary.STOREUSERLABELDATA,"");

            this.mTokens.Add(TokenLibrary.DESTINATIONNUMBER,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONNAME,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONADDRESSLINE1,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONADDRESSLINE2,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONADDRESSCITY,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONADDRESSSTATE,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONADDRESSZIP,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONZIP,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONADDRESSCOUNTRYCODE,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONPHONE,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONROUTE,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONALTROUTE,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONROUTEFIRSTCHARACTER,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONROUTEFIRSTTWO,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONROUTELASTFOUR,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONROUTELASTTHREE,"");
            this.mTokens.Add(TokenLibrary.DESTINATIONUSERLABELDATA,"");

            this.mTokens.Add(TokenLibrary.VENDORNUMBER,"");
            this.mTokens.Add(TokenLibrary.VENDORNAME,"");
            this.mTokens.Add(TokenLibrary.VENDORADDRESSLINE1,"");
            this.mTokens.Add(TokenLibrary.VENDORADDRESSLINE2,"");
            this.mTokens.Add(TokenLibrary.VENDORADDRESSCITY,"");
            this.mTokens.Add(TokenLibrary.VENDORADDRESSSTATE,"");
            this.mTokens.Add(TokenLibrary.VENDORADDRESSZIP,"");
            this.mTokens.Add(TokenLibrary.VENDORUSERDATA,"");

            this.mTokens.Add(TokenLibrary.SHIPPERNUMBER,"");
            this.mTokens.Add(TokenLibrary.SHIPPERNAME,"");
            this.mTokens.Add(TokenLibrary.SHIPPERADDRESSLINE1,"");
            this.mTokens.Add(TokenLibrary.SHIPPERADDRESSLINE2,"");
            this.mTokens.Add(TokenLibrary.SHIPPERADDRESSCITY,"");
            this.mTokens.Add(TokenLibrary.SHIPPERADDRESSSTATE,"");
            this.mTokens.Add(TokenLibrary.SHIPPERADDRESSZIP,"");
            this.mTokens.Add(TokenLibrary.SHIPPERUSERDATA,"");

            this.mTokens.Add(TokenLibrary.ZONECODE,"");
            this.mTokens.Add(TokenLibrary.ZONELABELTYPE,"");
            this.mTokens.Add(TokenLibrary.ZONELANE,"");
            this.mTokens.Add(TokenLibrary.ZONELANESMALLSORT,"");
            this.mTokens.Add(TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBER,"");
            this.mTokens.Add(TokenLibrary.ZONEOUTBOUNDTRAILERLOADNUMBERDIGITSONLY,"");

            this.mTokens.Add(TokenLibrary.OSSERVICETITLE,"");
            this.mTokens.Add(TokenLibrary.OSBARCODE1DATAHUMANFORMAT,"");
            this.mTokens.Add(TokenLibrary.OSDATAIDENTIFIER,"");
            this.mTokens.Add(TokenLibrary.OSBARCODE1DATA,"");
            this.mTokens.Add(TokenLibrary.OSROUTINGCODE,"");
            this.mTokens.Add(TokenLibrary.OSSERVICEINDICATOR,"");
            this.mTokens.Add(TokenLibrary.OSTRACKINGNUMBER10,"");
            this.mTokens.Add(TokenLibrary.OSSENDERACCOUNTNUMBER,"");
            this.mTokens.Add(TokenLibrary.OSJULIANDATE,"");
            this.mTokens.Add(TokenLibrary.OSSERVICEICON,"");

            this.mTokens.Add(TokenLibrary.STATUSCODE,"");
            this.mTokens.Add(TokenLibrary.MESSAGETEXT,"");
			#endregion
		}
        /// <summary>The collection of available tokens.</summary>
        public Hashtable Tokens { get { return this.mTokens; } }
        /// <summary>Replaces label template token values with actual data.</summary>
        /// <param name="template"></param>
		/// <returns>Returns a formatted lable template.</returns>
        public string FormatLabelTemplate(string template) { 
			//Format the label template (replace tokens)
			string format=template;
			try {
                setLabelTokenValues();
				foreach(DictionaryEntry token in this.mTokens) 
					format = format.Replace(token.Key.ToString(), token.Value.ToString());
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while formatting the label template.", ex); }
			return format;
		}
		
		//Virtual behavior
        /// <summary>The name of this LabelMaker.</summary>
		public abstract string Name { get; }
        /// <summary>Replaces label template token values with actual data.</summary>
        protected abstract void setLabelTokenValues();
        /// <summary>Fires the LabelValuesChanged event on request from derived classes</summary>
        protected void OnLabelValuesChanged(object sender,EventArgs e) { if(this.LabelValuesChanged != null) this.LabelValuesChanged(sender, e); }
	}
}