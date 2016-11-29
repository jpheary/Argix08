//	File:	stationoperator.cs
//	Author:	J. Heary
//	Date:	09/06/07
//	Desc:	Represents the behavior and interactions of a sort station 
//			operator including processing and deleting cartons.
//	Rev:	02/18/08 (jph)- modified StoreSortedItem() to NOT remove sorted item on save;
//			item will be removed by collection class cleanup.
//			06/03/08 (jph)- modified StartWork() to use SortMediator for data access.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Argix;
using Argix.Data;
using Tsort.Enterprise;
using Tsort.Freight;

namespace Tsort.Sort {
	//
	public class StationOperator {	
		//Members
		private Brain mBrain=null;
		private Workstation mStation=null;
		private StationAssignments mAssignments=null;
		internal InboundLabel CurrentInboundLabel=null;
		private SortedItems mSortedItems = null;
		private dlgStationSetup mSetup = null;
		private string mErrorLabelNumber="88";
		private bool mOperatorWorking=false;
		
		//Events
		public event EventHandler StationAssignmentsChanged=null;
		public event SortedItemEventHandler SortedItemCreated=null;
		public event SortedItemEventHandler SortedItemComplete=null;
		public event DataStatusHandler DataStatusUpdate=null;
		
		//Interface
		public StationOperator(string connectionString): this(connectionString, false)  { }
		public StationOperator(string connectionString, bool useConnectionState)  {
			//Constructor - this is done for dll purposes, NEED to think more, maybe move to config file, but will need dll start up 
			try {
				//Init this object
                Config._ConnectionString = connectionString;
                Config._WebSvcUrl = "";
                Config._UseWebSvc = false;
                Config._UseConnState = useConnectionState;
                AppLib.Mediator.DataStatusUpdate += new DataStatusHandler(OnDataStatusUpdate);
                EnterpriseFactory.Mediator = FreightFactory.Mediator = SortFactory.Mediator = AppLib.Mediator;

				Brain.Self = this;
				this.mBrain = SortFactory.CreateBrain(null);
				//this.mStation = SortFactory.CreateWorkstation("");
                this.mStation = SortFactory.CreateWorkstation(Environment.MachineName);
                this.mAssignments = new StationAssignments();
				this.mSortedItems = new SortedItems();
				this.mSetup = new dlgStationSetup(dlgStationSetup.SCALETYPE_AUTOMATIC);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating Station Operator instance.", ex); }
		}
		#region Accessors\Modifiers: Name, BrainName, ScaleType, UseConnectionState, ConnectionString, Station, Assignments, Working, ToDataSet()
		public string Name { get { return Environment.UserName; } }
		public string BrainName { get { return this.mBrain.Name; } }
		public Brain Brain { get { return this.mBrain; } }
		public string ScaleType { get { return this.mSetup.ScaleType; } }
		public Workstation Station { get { return this.mStation; } }
		internal StationAssignments Assignments { get { return this.mAssignments; } }
		internal bool Working { get { return this.mOperatorWorking; } }
		public DataSet ToDataSet() { 
			//Return a dataset containing values for this object
			DataSet ds=null;
			try { 
				ds = new DataSet();
				ds.Merge(this.mBrain.ToDataSet());
				ds.Merge(this.mStation.ToDataSet());
				ds.Merge(this.mAssignments.ToDataSet());
			}
			catch(Exception) { }
			return ds;
		}
		#endregion
		#region Operator Services: StartWork(), RefreshCache(), RefreshStationAssignments(), ResetStatistics(), ShowStationSetup()
		public void StartWork() {
			//
			try {
				//Apply configuration parameters and update object model
				FreightFactory.DefaultIBLabelID = AppLib.Config.IBLabelDefault;
                Brain.UPSAllowed = AppLib.Config.UPSAllowed;
                Brain.ShipOverride = AppLib.Config.ShipOverride;
                SortedItems.MaxItems = AppLib.Config.SortedItemsLength;
                SortedItems.DuplicateCheckCount = AppLib.Config.SortedItemsCheckDuplicatesLength;
				this.mSortedItems = new SortedItems();
                SortedItem.WeightMax = AppLib.Config.WeightMax;
				SortedItem.LanePrefix = AppLib.Config.LanePrefix;
                this.mErrorLabelNumber = AppLib.Config.OBLabelError;
                //this.mStation = SortFactory.CreateWorkstation(Environment.MachineName);
				SortedItem.ErrorLabelTemplate = SortFactory.CreateOBLabelTemplate(this.mErrorLabelNumber, this.Station.PrinterType);
				this.mOperatorWorking = true;
				RefreshCache();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error when starting Station Operator.", ex); }
		}
		public void RefreshCache() {
			//Refresh cached data
			try { 
				//Validate operator state
				if(!this.mOperatorWorking) 
					throw new ApplicationException("Station Operator not started for work; call StartWork() before RefreshCache().");
				
				//Refresh all factory caches
				SortedItem.ErrorLabelTemplate = SortFactory.CreateOBLabelTemplate(this.mErrorLabelNumber, this.Station.PrinterType);
				EnterpriseFactory.RefreshCache();
				FreightFactory.RefreshCache();
				SortFactory.RefreshCache();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing the factory caches.", ex); }
		}
		public void RefreshStationAssignments() { 
			//Refresh current station assignments
			try {
				//Validate operator state
				if(!this.mOperatorWorking) 
					throw new ApplicationException("Station Operator not started for work; call StartWork() before RefreshStationAssignments().");
				
				//Clear assignment info
				SortFactory.CreateBrain(null);
				this.mAssignments.Clear();
				
				//Refresh station assignments; select first assignment as current and select the appropriate Brain
				this.mAssignments = SortFactory.GetStationAssignments(this.mStation);
				ArgixTrace.WriteLine(new TraceMessage("Assignments changed... " + this.mAssignments.Count.ToString() + " new assignments...", AppLib.EVENTLOGNAME, LogLevel.Warning, "SortOperator"));
				if(this.mAssignments.Count > 0) {
					this.mBrain = SortFactory.CreateBrain(this.mAssignments.Item(0).SortProfile);
					CurrentInboundLabel = this.mAssignments.Item(0).SortProfile.InboundLabel;
				}
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing station assignments.", ex); }
			finally { if(this.StationAssignmentsChanged != null) this.StationAssignmentsChanged(this, EventArgs.Empty); }
		}
		internal StationAssignments PreviewStationAssignments() { 
			//Preview upcoming freight assignments
			StationAssignments assignments=null;
			try {
				assignments = null;	//SortFactory.PreviewStationAssignments(this.mStation);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while previewing assignments.", ex); }
			return assignments;
		}
		public void ResetStatistics(){ 
			this.mStation.SortStatistics.Reset();
			if(this.SortedItemComplete != null) this.SortedItemComplete(this, new SortedItemEventArgs(null));
		}
		public void ShowStationSetup() { this.mSetup.ShowDialog(); }
		#endregion
		#region Sorted Item Services: ProcessInputs(), SortedItemCount, GetSortedItem(), CancelSortedItem(), StoreSortedItem()
		public SortedItem ProcessInputs(string[] inputs, decimal weight) { 
			//Process the first input scan
			SortedItem sortedItem=null;
			try {				
				//Create a sorted item and re-initialize the inbound label template
				if(this.mSetup.ScaleType == dlgStationSetup.SCALETYPE_CONSTANT)
					weight = this.mSetup.ConstantWeight;
				int w = decimal.ToInt32(decimal.Round(weight + 0.5m, 0));
				ArgixTrace.WriteLine(new TraceMessage("Weight rounded from " + weight.ToString() + " to " + w.ToString() + "...", AppLib.EVENTLOGNAME, LogLevel.Debug, "SortOperator"));
				sortedItem = this.mBrain.CreateSortedItem(inputs, w);
				if(this.SortedItemCreated != null) this.SortedItemCreated(this, new SortedItemEventArgs(sortedItem));
				
				//Determine elapsed & down times for this sorted item
				int duration = this.mStation.SortStatistics.CalcDuration();
				sortedItem.ElapsedSeconds = (duration < Statistics.DOWN_TIME_MAX ? duration : Statistics.DOWN_TIME_MAX);
				sortedItem.DownSeconds = (duration < Statistics.DOWN_TIME_MAX ? 0 : duration - Statistics.DOWN_TIME_MAX);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while processing carton inputs.", ex); }
			return sortedItem;
		}
		public int SortedItemCount { get { return this.mSortedItems.Count; } }
		public SortedItem GetSortedItem (string labelSeqNumber) {
			//Get an existing sorted item from the collection
			return this.mSortedItems.Item(labelSeqNumber);
		}
		public bool CancelSortedItem (string labelSeqNumber) {
			//Remove the sorted item from the collection
			try {
				this.mSortedItems.Remove(labelSeqNumber);
				this.mStation.SortStatistics.AddUnsortedCarton();
			}
			catch(Exception) {}
			finally { if(this.SortedItemComplete != null) this.SortedItemComplete(this, new SortedItemEventArgs(null)); }
			return true;
		}
		public bool StoreSortedItem (string labelSeqNumber) {
			//Save the sorted item and remove from the collection
			SortedItem sortedItem = this.mSortedItems.Item(labelSeqNumber);
			if(sortedItem == null)
				throw new SavingException("Error saving sorted item (not in sorted items list) " + labelSeqNumber );
			return StoreSortedItem(sortedItem);
		}
		public bool StoreSortedItem (SortedItem sortedItem)	{
			//
			if(sortedItem.IsError()) 
				throw new SavingException("Item was marked as error during processing - saving stopped " + sortedItem.LabelNumber);
			try {
				SortFactory.SaveSortedItem(sortedItem);
				this.mStation.SortStatistics.AddSortedCarton();
				//02/18/08 (jph): leave this sorted item in the collection for duplicate carton checks
				//this.mSortedItems.Remove(sortedItem.LabelSeqNumber);
			}
			catch(Exception ex) { throw new SavingException("Error saving sorted item " + sortedItem.LabelNumber, ex); }
			finally { if(this.SortedItemComplete != null) this.SortedItemComplete(this, new SortedItemEventArgs(sortedItem)); }
			return true;
		}
		#region Internal: NewSortedItem(), DuplicateCartonValidation()
		internal SortedItem NewSortedItem () {
			//Get a new sorted item from the collection
			SortedItem sortedItem = new SortedItem(this.mStation);
			this.mSortedItems.Add(sortedItem);
			return sortedItem;
		}
		internal void DuplicateCartonValidation (SortedItem sortedItem) {
			//Determine if this is a duplicate carton
			if(this.mSortedItems.IsDuplicateCarton(sortedItem))
				sortedItem.ThrowException(new DuplicateCartonException());
		}
		#endregion
		#endregion
        private void OnDataStatusUpdate(object source,DataStatusArgs e) {
            //Mediator data status update
            if(this.DataStatusUpdate != null) this.DataStatusUpdate(source,e);
        }
    }
}
