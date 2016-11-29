using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using Argix.Enterprise;
using Argix.Windows;
using Tsort.Labels;

namespace Argix.Freight {
	//
	public class StationOperator {	
		//Members
		public static int ScanSize=0;
		public static string LabelTypeOverrideRegular="", LabelTypeOverrideReturns="";
		public static bool ValidateLane=false, ValidateSmallLane=false;
		
		private Workstation mStation=null;
		private BearwareTrip mAssignment=null;
		private Carton mPreviousCarton=new Carton("");
        private int mCartonsScanned=0;
		
		public event EventHandler AssignmentChanged=null;
		public event CartonEventHandler CartonCreated=null;
		public event CartonEventHandler CartonDeleted=null;
		
		//Interface
		public StationOperator() {
			//Constructor
			try {
				//Get station configuration and current freight assignment
				this.mStation = FreightService.GetWorkstation(Environment.MachineName);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Station Operator instance.", ex); }
		}
		#region Accessors\Modifiers: Station, TripAssignment, isTripAssigned, CartonsScanned
		public Workstation Station { get { return this.mStation; } }
		public BearwareTrip TripAssignment { get { return this.mAssignment; } }
		public bool isTripAssigned { get { return (this.mAssignment!=null); } }
		public int CartonsScanned  { get { return this.mCartonsScanned; } }
		#endregion
		public void StartWork() {
			//Fire-up the engines
			try {
				this.mStation.SetDefaultPrinter();
				RefreshTripAssignment();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while Station Operator starting work.", ex); }
		}
		public void RefreshTripAssignment() {
			//Update trip assignment for this station
			string priorTripNumber="";
			try {
				this.mCartonsScanned = 0;
				priorTripNumber = (this.mAssignment != null) ? this.mAssignment.Number : "";
				this.mAssignment = null;
				this.mAssignment = FreightService.GetTripAssignment(this.mStation.Number);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while refreshing trip assignments.", ex); }
			finally { if(this.AssignmentChanged != null) this.AssignmentChanged(this, EventArgs.Empty); }
		}
		public void ProcessCarton(string labelScan) {  
			//Check for valid assignment
			if(!isTripAssigned) throw new WorkflowException("There is no Trip assignment on this station.");
			
			//Create a new carton object and attach associated objects
			Carton carton=null;
			try {				
				carton = createCarton(labelScan);
			}
			catch(LabelException ex) { throw new WorkflowException("Label did not format correctly. PLEASE RESCAN CARTON.", ex); }
			catch(WorkflowException ex) { throw ex; }
			catch(Exception ex) { throw new WorkflowException("Unexpected error. PLEASE RESCAN CARTON.", ex); }

            //Print an outbound label for this carton
            try {
                string labelTypeOverride="",labelType="";
                switch(carton.FreightType) {
                    case "44":
                        labelTypeOverride = (StationOperator.LabelTypeOverrideRegular.Length > 0) ? StationOperator.LabelTypeOverrideRegular : "";
                        labelType = (carton.Store.LabelType.Trim().Length > 0) ? carton.Store.LabelType.Trim() : carton.Zone.LabelType.Trim();
                        break;
                    case "88":
                        labelTypeOverride = (StationOperator.LabelTypeOverrideReturns.Trim().Length > 0) ? StationOperator.LabelTypeOverrideReturns.Trim() : "";
                        labelType = carton.Zone.LabelType.Trim();
                        break;
                }
                if(labelTypeOverride.Length > 0) labelType = labelTypeOverride;
                OutboundLabel label = FreightService.GetOutboundLabel(labelType,this.mStation.PrinterType);
                string zpl = carton.FormatLabelTemplate(label.Template);
				this.mStation.Printer.Print(zpl);
			}
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new WorkflowException("Label Error",ex); }

            //Save carton to database (generates label seq number)
            try {
                if(FreightService.CreateCarton(carton, this.mAssignment.Number,this.mStation.Number)) {
					this.mPreviousCarton = carton;
					this.mCartonsScanned++;
					if(this.CartonCreated != null) CartonCreated(this, new CartonEventArgs(carton.ScanData));
				}
			}
			catch(Exception ex) { throw new WorkflowException("Carton may not have saved. PLEASE RESCAN CARTON.", ex); }
		}
		public void DeleteCarton(string labelScan) {
			//Delete an existing carton from the system
			try {
				Carton carton = new Carton(labelScan);
				if(!carton.isValid) throw new ApplicationException("Invalid carton.");
                if(FreightService.DeleteCarton(carton)) {
					if(this.CartonDeleted != null) CartonDeleted(this, new CartonEventArgs(labelScan));
				}
			}
			catch(Exception ex) { throw new WorkflowException("Carton was not deleted.", ex); }
		}
		#region Local Services: createCarton(), tripValidToSortCheck()
		private Carton createCarton(string labelScan) {
			//Create a new carton
			Carton carton=null;
            try {
                //Create a scanned carton; check for a valid scan, then check if carton exists in db
                carton = new Carton(labelScan);
                tripValidToSortCheck();
                if(!carton.isValid) throw new WorkflowException("Invalid scan " + carton.ScanData + ".");

                if(FreightService.CartonExists(carton)) {
                    //Log a duplicate carton that was not the previous carton
                    if((carton.ScanData != this.mPreviousCarton.ScanData) && (this.mPreviousCarton.ScanData != ""))
                        throw new WorkflowException("Duplicate carton.");
                    else
                        throw new WorkflowException("Carton exists.");
                }

                //Set carton objects for label creation
                carton.Workstation = this.mStation;
				carton.InboundFreight = this.mAssignment;
				carton.Client = EnterpriseService.GetClient(carton.ClientNumber);
				switch(carton.FreightType) {
					case "44": 
						//Regular freight- get a label configuration based upon client and store
						carton.Store = EnterpriseService.GetStore(carton.ClientNumber, carton.StoreNumber);
						carton.Zone = EnterpriseService.GetZone(carton.Store.Zone, carton.Store.ZoneType);
						break;
					case "88":
						//Return freight- get a label configuration based upon client and vendor
						carton.ClientVendor = EnterpriseService.GetClientVendor(carton.ClientNumber, carton.VendorNumber);
						carton.Zone = EnterpriseService.GetZone(carton.ClientVendor.ZONE_CODE, carton.ClientVendor.ZONE_TYPE);
						break;
				}
				
                //Validate the lanes (if applicable)
				if(StationOperator.ValidateLane) {
					if(carton.Zone.Lane.Length == 0) throw new ApplicationException("The lane for zone " + carton.Zone.Code + " is missing");
                    if(carton.Zone.Lane.CompareTo("00") <= 0) throw new ApplicationException("The lane for zone " +carton.Zone.Code + " is invalid (" + carton.Zone.Lane.Trim() + ")");
				}
				if(StationOperator.ValidateSmallLane) {
                    if(carton.Zone.SmallSortLane.Length == 0) throw new ApplicationException("The small lane for zone " + carton.Zone.Code + " is missing");
                    if(carton.Zone.SmallSortLane.CompareTo("00") <= 0) throw new ApplicationException("The small lane for zone " + carton.Zone.Code + " is invalid (" + carton.Zone.Lane.Trim() + ")");
				}
				//carton.TrailerLoad = CreateTrailerLoad(carton.Zone.TRAILER_LOAD_NUM);
            }
            catch(WorkflowException ex) { throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new carton instance.",ex); }

			return carton;
		}
		private void tripValidToSortCheck() {
			//Can implement later if needed
			//return isTripAssigned && !isTripOSND(this.mAssignment.Number);
		}
		#endregion
	}
}
