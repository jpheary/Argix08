//	File:	freightfactory.cs
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

namespace Tsort.Freight {
	//
	public class FreightFactory {
		//Class members
		public static Mediator Mediator=null;
		
		//Class interface
		static FreightFactory() { }
		#region Damage Codes
		public static DamageCodeDS ViewDamageCodes() {
			//Get a list of damage codes
			DamageCodeDS codes = new DamageCodeDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseDamageGetList", "DamageCodeTable", null);
				if(ds != null) 
					codes.Merge(ds.Tables["DamageCodeTable"].Select("", "Code", DataViewRowState.CurrentRows));
			}
			catch(Exception ex) { throw ex; }
			return codes;
		}
		public static DamageCodeDS GetDamageCode(short codeID) {
			//Get a new or existing damage code
			DamageCodeDS code = new DamageCodeDS();
			try {
				if(codeID == 0) {
					//New
					DamageCodeDS.DamageCodeTableRow row = code.DamageCodeTable.NewDamageCodeTableRow();
					//row.CodeID = codeID;
					row.Description = "";
					row.LastUpdated = DateTime.Now;
					row.UserID = System.Environment.UserName;
					row.RowVersion = "";
					code.DamageCodeTable.AddDamageCodeTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("", "DamageCodeTable", new object[]{codeID});
					if(ds != null)
						code.Merge(ds.Tables["DamageCodeTable"].Select("Code=" + codeID));
				}
			} 
			catch(Exception ex) { throw ex; }
			return code;
		}
		public static short CreateDamageCode(DamageCodeDS code) {
			//Create a new damage code
			short result=0;
			try {
				result = (short)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseDamageNew", new object[]{code});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateDamageCode(DamageCodeDS code) {
			//Update an existing damage code
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseDamageUpdate", new object[]{code});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool DeleteDamageCode(short codeID, string rowVer) {
			//Delete an existing damage code
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseDamageDelete", new object[]{codeID, rowVer});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Inbound Label
		public static InboundLabelDS ViewInboundLabels() {
			//get a list of label formats
			InboundLabelDS labels = new InboundLabelDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseInboundLabelGetList", "InboundLabelViewTable", null);
				if(ds != null) 
					labels.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return labels;
		}
		public static InboundLabelDS GetInboundLabel(int labelID) {
			//Get a new or existing InboundLabel
			InboundLabelDS label = new InboundLabelDS();
			try {
				if(labelID == 0) {
					//New
					InboundLabelDS.InboundLabelDetailTableRow row = label.InboundLabelDetailTable.NewInboundLabelDetailTableRow();
					row.LabelID = labelID;
					row.SortTypeID = 0;
					row.Description = "";
					row.Input1Len = 1;
					row.Input1ValidStart = 0;
					row.Input1ValidString = "";
					row.Input2Len = 0;
					row.Input2ValidStart = 0;
					row.Input2ValidString = "";
					row.Input3Len = 0;
					row.Input3ValidStart = 0;
					row.Input3ValidString = "";
					row.IsActive = true;
					row.LastUpdated = DateTime.Now;
					row.RowVersion = "";
					row.UserID = System.Environment.UserName;
					label.InboundLabelDetailTable.AddInboundLabelDetailTableRow(row);
				}
				else {
					//Existing
					DataSet ds = Mediator.FillDataset("uspEnterpriseInboundLabelGet", "InboundLabelDetailTable", new object[]{labelID});
					if(ds != null)
						label.Merge(ds);
				}
			} 
			catch(Exception ex) { throw ex; }
			return label;
		}
		public static InboundLabelDS GetDataElementsTemplate(int freightSortTypeID) {
			//Create a template of data elements for the freight sort type
			InboundLabelDS elem = new InboundLabelDS();
			try {
				switch(freightSortTypeID) {
					case 1:		//SAN - Tsort
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(true, true, 0, "SAN", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "CARTON", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "PO", 1, 1, 1, false, false, false, false);
						break;
					case 2:		//Regular - Tsort
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(true, true, 0, "STORE", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "CARTON", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "PO", 1, 1, 1, false, false, false, false);
						break;
					case 3:		//JIT - Tsort
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(true, true, 0, "CARTON", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "STORE", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "PO", 1, 1, 1, false, false, false, false);
						break;
					case 4:		//JIT-SAN - Tsort
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(true, true, 0, "CARTON", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(true, true, 0, "SAN", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "PO", 1, 1, 1, false, false, false, false);
						break;
					case 5:		//SKU - Tsort
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(true, true, 0, "SKU", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "STORE", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "CARTON", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "PO", 1, 1, 1, false, false, false, false);
						break;
					case 6:		//Returns - Returns
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(true, true, 0, "VENDOR", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "CARTON", 1, 1, 1, false, false, false, false);
						elem.InboundLabelDataElementTable.AddInboundLabelDataElementTableRow(false, false, 0, "RETURN", 1, 1, 1, false, false, false, false);
						break;
				}
			}
			catch(Exception ex) { throw ex; }
			return elem;
		}
		public static int CreateInboundLabel(InboundLabelDS label) {
			//Create a new InboundLabel
			int result=0;
			try {
				result = (int)Mediator.ExecuteNonQueryWithReturn("uspEnterpriseInboundLabelNew", new object[]{label});
			}
			catch(Exception ex) { throw ex; }
			return result;
		}
		public static bool UpdateInboundLabel(InboundLabelDS label) {
			//Update an existing InboundLabel
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseInboundLabelUpdate", new object[]{label});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
		#region Shift Schedule
		public static ShiftDS ViewShiftSchedules(int terminalID) {
			//Get the daily shift schedule for one terminal
			ShiftDS schedule = new ShiftDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseShiftDayGetListForTerminal", "ShiftViewTable", new object[]{terminalID});
				if(ds != null) 
					schedule.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			return schedule;
		}
		public static ShiftDS GetDailyShiftSchedule(int terminalID, string weekday) {
			//Get a daily shift schedule for a specified terminal and a single day of the week
			ShiftDS schedule = new ShiftDS();
			try {
				DataSet ds = Mediator.FillDataset("uspEnterpriseShiftDayGetForTerminalAndDay", "ShiftDetailTable", new object[]{terminalID, weekday});
				if(ds != null)
					schedule.Merge(ds, true, MissingSchemaAction.Ignore);
			} 
			catch(Exception ex) { throw ex; }
			return schedule;
		}
		public static bool UpdateDailyShiftSchedule(ShiftDS schedule) {
			//Update a daily shift schedule
			bool result=false;
			try {
				result = Mediator.ExecuteNonQuery("uspEnterpriseShiftUpdate", new object[]{schedule});
			} 
			catch(Exception ex) { throw ex; }
			return result;
		}
		#endregion
	}
}
