//	File:	stationassignment.cs
//	Author:	J. Heary
//	Date:	11/12/03
//	Desc:	Represents the state and behavior of an indirect freight assignment.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Text;
using Tsort.Freight;

namespace Tsort.Enterprise {
	//
	public class StationFreightAssignment {
		//Members
		private SortStation mStation=null;
		private InboundFreight mFreight=null;
		private string mSortType="";
		
		//Interface
		public StationFreightAssignment() {}
		public StationFreightAssignment(SortStation station, InboundFreight freight, string sortType) {
			//Constructor
			try {
				//Configure this assignment from the assignment configuration information
				this.mStation = station;
				this.mFreight = freight;
				this.mSortType = sortType;
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Station Assignment instance.", ex); }
		}
		#region Accessors\Modifiers: Station, Freight, SortType, ToDataSet()
		public SortStation Station { get { return this.mStation; } }
		public InboundFreight Freight { get { return this.mFreight; } }
		public string SortType { get { return this.mSortType; } }
		public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			DataSet ds=null;
			try {
				ds = new DataSet();
				ds.Merge(this.mStation.ToDataSet());
				ds.Merge(this.mFreight.ToDataSet());
				//ds.Merge(new DataSet().Tables.Add("SortTypeTable").Rows.Add(new object[]{this.mSortType}));
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
	}
}
