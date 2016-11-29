//	File:	transportationfactory.cs
//	Author:	J. Heary
//	Date:	05/01/06
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using Tsort.Data;
using Tsort.Enterprise;
using Tsort.Windows;

namespace Tsort.Transportation {
	//
	public class TransportationFactory {
		//Class members
		public static Mediator Mediator=null;
		
		//Class interface
		static TransportationFactory() { }
		#region Carriers
		public static CarrierDS ViewCarriers() {
			//Get a list of carriers
			CarrierDS carriers = new CarrierDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseCarrierGetList", "CarrierViewTable", null);
				if(ds != null) 
					carriers.Merge(ds.Tables["CarrierViewTable"].Select("", "CarrierName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return carriers;
		}
		public static CarrierDS GetCarrier(int carrierID) {
			//Get a new or existing carrier
			CarrierDS carrier = new CarrierDS();
			try {
				if(carrierID == 0) {
					//New
					CarrierDS.CarrierDetailTableRow row = carrier.CarrierDetailTable.NewCarrierDetailTableRow();
					row.CarrierID = carrierID;
					row.Number = "";
					row.CarrierName = "";
					row.Phone = "";
					row.Extension = "";
					row.AddressLine1 = "";
					row.AddressLine2 = "";
					row.City = "";
					row.StateOrProvince = "NJ";
					row.PostalCode = "";
					row.CountryID = 1;
					row.ContactName = "";
					row.Fax = "";
					row.Email = "";
					row.ControlDrivers = false;
					row.ControlTrailers = false;
					row.SCAC = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					carrier.CarrierDetailTable.AddCarrierDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseCarrierGet", "CarrierDetailTable", new object[]{carrierID});
					if(ds != null)
						carrier.Merge(ds, true, MissingSchemaAction.Ignore);
				}
			} 
			catch(Exception ex) { throw ex; }
			return carrier;
		}
		public static int CreateCarrier(CarrierDS carrier) {
			//Create a new carrier
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseCarrierNew", new object[]{carrier});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateCarrier(CarrierDS carrier) {
			//Update an existing carrier
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseCarrierUpdate", new object[]{carrier});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Drivers
		public static DriverDS ViewDrivers() {
			//Get a view of drivers
			DriverDS drivers = new DriverDS();
			try {
				DataSet ds = Mediator.FillDataset("", "DriverListTable", null);
				if(ds != null) 
					drivers.Merge(ds.Tables["DriverListTable"].Select("", "LastName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return drivers;
		}
		public static SelectionList GetDrivers() {
			//Get a list of drivers
			SelectionList drivers = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "SelectionListTable", null);
				if(ds != null) 
					drivers.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return drivers;
		}
		public static DriverDS GetDriver(int driverID) {
			//Get a new or existing driver
			DriverDS driver = new DriverDS();
			try {
				if(driverID == 0) {
					//New
					DriverDS.DriverDetailTableRow row = driver.DriverDetailTable.NewDriverDetailTableRow();
					row.DriverID = 0;
					row.LastName = "";
					row.FirstName = "";
					row.Phone = "";
					row.TerminalID = 0;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					driver.DriverDetailTable.AddDriverDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "DriverDetailTable", new object[]{driverID});
					if(ds != null)
						driver.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return driver;
		}
		public static int CreateDriver(DriverDS driver) {
			//Create a new driver
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{driver});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateDriver(DriverDS driver) {
			//Update an existing driver
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{driver});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Switchers
		public static SwitcherDS ViewSwitchers(int terminalID) {
			//Get a list of switchers
			SwitcherDS switchers = new SwitcherDS();
			try {
				DataSet ds = Mediator.FillDataset("", "SwitcherViewTable", new object[]{terminalID});
				if(ds != null) 
					switchers.Merge(ds.Tables["SwitcherViewTable"].Select("TerminalID=" + terminalID, "LastName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return switchers;
		}
		public static SwitcherDS GetSwitcher(int switcherID) {
			//Get a new or existing switcher
			SwitcherDS switcher = new SwitcherDS();
			try {
				if(switcherID == 0) {
					//New
					SwitcherDS.SwitcherDetailTableRow row = switcher.SwitcherDetailTable.NewSwitcherDetailTableRow();
					row.SwitcherID = switcherID;
					row.LastName = "";
					row.FirstName = "";
					row.Phone = "";
					row.TerminalID = 0;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					switcher.SwitcherDetailTable.AddSwitcherDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "SwitcherDetailTable", new object[]{switcherID});
					if(ds != null)
						switcher.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return switcher;
		}
		public static int CreateSwitcher(SwitcherDS switcher) {
			//Create a new switcher
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{switcher});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateSwitcher(SwitcherDS switcher) {
			//Update an existing switcher
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{switcher});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Trailers
		public static TrailerDS ViewTrailers() {
			//Get a list of trailers
			TrailerDS trailers = new TrailerDS();
			try {
				DataSet ds = Mediator.FillDataset("", "TrailerListTable", null);
				if(ds != null) 
					trailers.Merge(ds.Tables["TrailerListTable"].Select("", "Number", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return trailers;
		}
		public static TrailerDS GetTrailer(int trailerID) {
			//Get a new or existing trailer
			TrailerDS trailer = new TrailerDS();
			try {
				if(trailerID == 0) {
					//New
					Transportation.TrailerDS.TrailerDetailTableRow row = trailer.TrailerDetailTable.NewTrailerDetailTableRow();
					row.TrailerID = trailerID;
					row.Number = "";
					//row.DefinitionID = 0;
					row.LicPlateNumber = "";
					//row.CarrierID = 0;
					row.IsStorage = false;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					trailer.TrailerDetailTable.AddTrailerDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "TrailerDetailTable", new object[]{trailerID});
					if(ds != null)
						trailer.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return trailer;
		}
		public static CarrierDS GetTrailerOwners() {
			//Get a list of trailer types
			CarrierDS owners = new CarrierDS();
			try {
				DataSet ds = Mediator.FillDataset("", "CarrierListTable", null);
				if(ds != null) 
					owners.Merge(ds.Tables["CarrierListTable"].Select("", "CarrierName", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return owners;
		}
		public static SelectionList GetTrailerTypes() {
			//Get a list of trailer types
			SelectionList trailerTypes = new SelectionList();
			try {
				DataSet ds = Mediator.FillDataset("", "SelectionListTable", null);
				if(ds != null) 
					trailerTypes.Merge(ds.Tables["SelectionListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return trailerTypes;
		}
		public static int CreateTrailer(TrailerDS trailer) {
			//Create a new trailer
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{trailer});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateTrailer(TrailerDS trailer) {
			//Update an existing trailer
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{trailer});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Vehicles
		public static VehicleDS ViewDriverVehicles() {
			//Get a list of driver vehicles
			VehicleDS vehicles = new VehicleDS();
			try {
				DataSet ds = Mediator.FillDataset("", "VehicleListTable", null);
				if(ds != null) 
					vehicles.Merge(ds.Tables["VehicleListTable"].Select("", "Description", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return vehicles;
		}
		public static VehicleDS GetDriverVehicle(int vehicleID, string vehicleType) {
			//Get a new or existing vehicle
			VehicleDS vehicle = new VehicleDS();
			try {
				if(vehicleID == 0) {
					//New
					VehicleDS.VehicleDetailTableRow row = vehicle.VehicleDetailTable.NewVehicleDetailTableRow();
					row.VehicleID = vehicleID;
					row.Description = "";
					row.VehicleType = vehicleType;
					row.LicPlateNumber = "";
					//row.State = "";
					row.CarrierID = EnterpriseFactory.CompanyID;
					//row.DriverID = 0;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					vehicle.VehicleDetailTable.AddVehicleDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "VehicleDetailTable", new object[]{vehicleID, vehicleType});
					if(ds != null)
						vehicle.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return vehicle;
		}
		public static VehicleTypeDS GetVehicleTypes() {
			//Get a list of vehicle types
			VehicleTypeDS vehicleTypes = new VehicleTypeDS();
			try {
				DataSet ds = Mediator.FillDataset("", "VehicleTypeListTable", null);
				if(ds != null) 
					vehicleTypes.Merge(ds.Tables["VehicleTypeListTable"].Select("", "VehicleType", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return vehicleTypes;
		}
		public static int CreateDriverVehicle(VehicleDS vehicle) {
			//Create a new driver vehicle
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{vehicle});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateDriverVehicle(VehicleDS vehicle) {
			//Update an existing driver vehicle
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{vehicle});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Yards
		public static YardDS ViewYards(int terminalID) {
			//Get a list of yards
			YardDS yards = new YardDS();
			try {
				DataSet ds = Mediator.FillDataset("", "YardListTable", new object[]{terminalID});
				if(ds != null) 
					yards.Merge(ds.Tables["YardListTable"].Select("", "Name", DataViewRowState.CurrentRows));
			}
			catch(Exception ex) { throw ex; }
			return yards;
		}
		public static YardDS GetYard(int terminalID, int yardID) {
			//Get a new or existing yard
			YardDS yard = new YardDS();
			try {
				if(yardID == 0) {
					//New
					YardDS.YardDetailTableRow row = yard.YardDetailTable.NewYardDetailTableRow();
					row.YardID = 0;
					row.Description = "";
					row.TerminalID = terminalID;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					yard.YardDetailTable.AddYardDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "YardDetailTable", new object[]{terminalID, yardID});
					if(ds != null)
						yard.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return yard;
		}
		public static int CreateYard(YardDS yard) {
			//Create a new yard
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{yard});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateYard(YardDS yard) {
			//Update an existing yard
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{yard});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Yard Sections
		public static YardSectionDS ViewYardSections(int yardID) {
			//Get a list of yard sections
			YardSectionDS sections = new YardSectionDS();
			try {
				DataSet ds = Mediator.FillDataset("", "YardSectionListTable", new object[]{yardID});
				if(ds != null) 
					sections.Merge(ds.Tables["YardSectionListTable"].Select("", "SectionNumber", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return sections;
		}
		public static YardSectionDS GetYardSection(int yardID, int yardSectionID) {
			//Get a new or existing yard section
			YardSectionDS yardSection = new YardSectionDS();
			try {
				if(yardSectionID == 0) {
					//New
					YardSectionDS.YardSectionDetailTableRow row = yardSection.YardSectionDetailTable.NewYardSectionDetailTableRow();
					row.SectionID = 0;
					row.SectionNumber = "";
					row.Description = "";
					row.YardID = yardID;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					yardSection.YardSectionDetailTable.AddYardSectionDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "YardSectionDetailTable", new object[]{yardID, yardSectionID});
					if(ds != null)
						yardSection.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return yardSection;
		}
		public static int CreateYardSection(YardSectionDS yardSection) {
			//Create a new yard section
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{yardSection});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateYardSection(YardSectionDS yardSection) {
			//Update an existing yard section
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{yardSection});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Yard Locations
		public static YardLocationDS ViewYardLocations(int sectionID) {
			//Get a list of yard locations
			YardLocationDS yardLocation = new YardLocationDS();
			try {
				DataSet ds = Mediator.FillDataset("", "YardLocationViewTable", new object[]{sectionID});
				if(ds != null) 
					yardLocation.Merge(ds.Tables["YardLocationViewTable"].Select("", "Number", DataViewRowState.CurrentRows));
			} 
			catch(Exception ex) { throw ex; }
			return yardLocation;
		}
		public static YardLocationDS GetYardLocation(int yardSectionID, int yardLocationID) {
			//Get a new or existing yard
			YardLocationDS yardLocation = new YardLocationDS();
			try {
				if(yardLocationID == 0) {
					//New
					YardLocationDS.YardLocationDetailTableRow row = yardLocation.YardLocationDetailTable.NewYardLocationDetailTableRow();
					row.YardLocationID = yardLocationID;
					row.Number = "";
					row.LocationTypeID = 0;
					row.SectionID = yardSectionID;
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					yardLocation.YardLocationDetailTable.AddYardLocationDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "YardLocationDetailTable", new object[]{yardSectionID, yardLocationID});
					if(ds != null)
						yardLocation.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return yardLocation;
		}
		public static YardLocationTypeDS GetYardLocationTypes() {
			//Geta list of yard location types
			YardLocationTypeDS yardLocationTypes = new YardLocationTypeDS();
			try {
				yardLocationTypes.YardLocationTypeListTable.AddYardLocationTypeListTableRow(1, "Door", true);
				yardLocationTypes.YardLocationTypeListTable.AddYardLocationTypeListTableRow(2, "Yard", true);
			} 
			catch(Exception ex) { throw ex; }
			return yardLocationTypes;
		}
		public static int CreateYardLocation(YardLocationDS yardLocation) {
			//Create a new yard location
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("", new object[]{yardLocation});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateYardLocation(YardLocationDS yardLocation) {
			//Update an existing yard location
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("", new object[]{yardLocation});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
	}
}
