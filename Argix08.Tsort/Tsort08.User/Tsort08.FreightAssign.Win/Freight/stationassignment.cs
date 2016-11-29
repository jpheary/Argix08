using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argix.Freight {
	//
	public class StationAssignment {
		//Members
		private Workstation mWorkStation=null;
		private IBShipment mInboundFreight=null;
		private int mSortTypeID=0;
		
		//Interface
		public StationAssignment() {}
		public StationAssignment(Workstation sortStation, IBShipment inboundFreight, int sortTypeID) {
			//Constructor
			try {
				//Configure this assignment from the assignment configuration information
				this.mWorkStation = sortStation;
				this.mInboundFreight = inboundFreight;
				this.mSortTypeID = sortTypeID;
			} 
			catch(Exception ex) { throw new ApplicationException("Could not create a new station assignment.", ex); }
		}
		#region Accessors\Modifiers: [Members...]
		public Workstation SortStation { get { return this.mWorkStation; } }
		public IBShipment InboundFreight { get { return this.mInboundFreight; } }
		public int SortTypeID { get { return this.mSortTypeID; } }
		#endregion
	}
}