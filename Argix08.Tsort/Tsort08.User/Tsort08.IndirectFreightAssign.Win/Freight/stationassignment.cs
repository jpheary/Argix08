using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Text;
using Argix.Data;

namespace Argix.Freight {
	//
	public class StationAssignment {
		//Members
		private string mAssignmentID="";
		private Workstation mStation=null;
		private BearwareTrip mFreight=null;
		
		public event EventHandler Changed=null; 
		
		//Interface
		public StationAssignment() {}
		public StationAssignment(string assignmentID, Workstation sortStation, BearwareTrip inboundFreight) {
			//Constructor
			try {
				//Configure this assignment from the assignment configuration information
				this.mAssignmentID = assignmentID;
				this.mStation = sortStation;
				this.mFreight = inboundFreight;
			} 
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new StationAssignment instance.", ex); }
		}
		#region Accessors\Modifiers: AssignmentID, SortStation, InboundFreight, ToString(), ToDataSet()
		public string AssignmentID { get { return this.mAssignmentID; } }
		public Workstation SortStation { get { return this.mStation; } }
		public BearwareTrip InboundFreight { get { return this.mFreight; } }
		public DataSet ToDataSet() {
			//Return a dataset containing values for this object
			BearwareDS ds=null;
			try {
				ds = new BearwareDS();
				ds.Merge(this.mStation.ToDataSet());
				ds.Merge(this.mFreight.ToDataSet());
				ds.AcceptChanges();
			}
			catch(Exception) { }
			return ds;
		}
		public override string ToString() {
			//Custom ToString() method
			string sThis=base.ToString();
			try {
				//Form string detail of this object
				StringBuilder builder = new StringBuilder();
				builder.Append("Station Assignment -----------\n");
				builder.Append("\tAssignmentID=" + this.mAssignmentID + "\n");
				builder.Append("\t" + this.mStation.ToString() + "\n");
				builder.Append("\t" + this.mFreight.ToString() + "\n");
				builder.Append("------------------------------\n");
				sThis = builder.ToString();
			} 
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public bool Create() {
			//Assign this trip to the specified sort station
			bool bRet=false;
			try {
                bRet = App.Mediator.ExecuteNonQuery(FreightFactory.USP_ASSIGNMENTCREATE, new object[] { this.InboundFreight.Number, this.SortStation.Number });
				if(bRet)
					if(this.Changed != null) this.Changed(this, EventArgs.Empty);
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		public bool Delete() {
			//
			bool bRet=false;
			try {
                bRet = App.Mediator.ExecuteNonQuery(FreightFactory.USP_ASSIGNMENTDELETE, new object[] { this.SortStation.Number });
				if(bRet)
					if(this.Changed != null) this.Changed(this, EventArgs.Empty);
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
	}
}