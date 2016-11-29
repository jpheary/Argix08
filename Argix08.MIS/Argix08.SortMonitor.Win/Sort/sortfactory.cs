//	File:	sortfactory.cs
//	Author:	J. Heary
//	Date:	04/24/07
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Tsort.Enterprise;
using Tsort.Freight;
using Argix.Data;

namespace Tsort.Sort {
	//
	internal class SortFactory {
		//Members
		public static Mediator Mediator=null;
		public static event EventHandler DataConnectionDropped=null;
		
		//Interface
		static SortFactory() { 
        	//Constructor
			try {
				//Init
				Mediator = new SQLMediator();
                Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                RefreshCache();
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating static SortFactory instance.", ex); }
        }
		private SortFactory() { }
		public static void RefreshCache() {
			//Refresh cached data
			try {
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while caching SortFactory data.", ex); }
		}
        #region Local Services: OnDataStatusUpdate()
        static void OnDataStatusUpdate(object source, DataStatusArgs e) {
            //Notifications from Mediator regarding connection status
            //Fire an event when connection drops
            if(!e.Online) 
                if(DataConnectionDropped != null) DataConnectionDropped(null, EventArgs.Empty);
        }
        #endregion
    }
}
