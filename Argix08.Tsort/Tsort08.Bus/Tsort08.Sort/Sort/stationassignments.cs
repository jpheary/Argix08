//	File:	stationassignments.cs
//	Author:	J. Heary
//	Date:	02/15/05
//	Desc:	Client side class representing a collection of freight assignments for 
//			a sorting station.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;

namespace Tsort.Sort {
    //
    internal class StationAssignments:CollectionBase {
        //Members
        //Constants
        //Events
        //Interface
        public StationAssignments() {
            //Constructor
            try {
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Station Assignment instance.",ex); }
        }
        public void Add(StationAssignment assignment) {
            base.InnerList.Add(assignment);
        }
        public StationAssignment Item(int index) {
            return (StationAssignment)base.List[index];
        }
        public StationAssignment Item(string stationID,int sortTypeID,string freightID) {
            StationAssignment assignment=null;
            for(int i=0;i<base.Count;i++) {
                StationAssignment a = (StationAssignment)base.List[i];
                if(a.SortStation.WorkStationID == stationID && a.SortProfile.SortTypeID == sortTypeID && a.InboundFreight.FreightID == freightID) {
                    assignment = a;
                    break;
                }
            }
            return assignment;
        }
        public StationAssignment Item(Tsort.Enterprise.Client aClient) {
            StationAssignment assignment=null;
            for(int i=0;i<base.Count;i++) {
                StationAssignment a = (StationAssignment)base.List[i];
                if(a.InboundFreight.Client.IsSameAs(aClient)) {
                    assignment = a;
                    break;
                }
            }
            return assignment;
        }
        #region Accessors\Modifiers: ToDataSet()
        public DataSet ToDataSet() {
            //Custom ToString() method
            StationAssignmentDS ds=null;
            try {
                //
                ds = new StationAssignmentDS();
                for(int i=0;i<base.InnerList.Count;i++) {
                    StationAssignment a = (StationAssignment)base.InnerList[i];
                    ds.Merge(a.ToDataSet());
                }
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
    }
}


