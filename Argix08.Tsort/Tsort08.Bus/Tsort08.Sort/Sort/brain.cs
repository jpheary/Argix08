//	File:	brain.cs
//	Author:	J. Heary
//	Date:	02/07/05
//	Desc:	Base class for all subtypes that define freight processing
//			workflow for specific freight sort types.
//	Rev:	11/08/07 (jph)- corrected error in SANBrain():DetermineDestinationAndRounting()-
//			parameter to SortFactory.CreateSANDestinationRouting() changed from 
//			label.IsDuplicateElementAllowed("CARTON") to !label.IsDuplicateElementAllowed("CARTON"); 
//			added implementation for RegularBrain.
//			02/18/08 (jph)- moved sortedItem.CartonNumber = label.GetElementValue("CARTON")
//			from DetermineDestinationAndRounting() to CreateSortedItem()- need carton# to 
//			check for duplicates.
//			07/14/08 (jph)- added sort types ManifestXStoreBrain and MultiManifestXStoreBrain objects.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;
using Argix;
using Argix.Data;
using Tsort.Enterprise;
using Tsort.Freight;
using Tsort.Labels;

namespace Tsort.Sort {
    //
    public class Brain {
        //Members
        internal static StationOperator Self=null;
        internal static bool UPSAllowed=true;
        internal static string ShipOverride="";

        //Constants		
        //Events
        //Interface
        public Brain() { }
        #region Accessors\Modifiers: Name, ToDataSet()
        public virtual string Name { get { return "Brain"; } }
        internal DataSet ToDataSet() {
            //Return a dataset containing values for this object
            DataSet ds=null;
            try {
                ds = new DataSet();
                DataTable table = ds.Tables.Add("BrainTable");
                table.Columns.Add("Name");
                table.Rows.Add(new object[] { this.Name });
                table.AcceptChanges();
            }
            catch(Exception) { }
            return ds;
        }
        #endregion
        public virtual SortedItem CreateSortedItem(string[] inputs,int weight) {
            //
            SortedItem sortedItem = Self.NewSortedItem();
            try {
                ArgixTrace.WriteLine(new TraceMessage("Determine assignment...",AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                DetermineAssignment(inputs,sortedItem);
                ArgixTrace.WriteLine(new TraceMessage("Get inbound label with data...",AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                InboundLabel label = getInboundLabelWithData(sortedItem,inputs);
                sortedItem.CartonNumber = label.GetElementValue("CARTON");
                if(!label.IsDuplicateElementAllowed("CARTON")) Self.DuplicateCartonValidation(sortedItem);
                ArgixTrace.WriteLine(new TraceMessage("Determine destination and rounting...",AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                DetermineDestinationAndRounting(sortedItem,label);
                if(weight == 0) sortedItem.ThrowException(new ZeroWeightException());
                if(weight > SortedItem.WeightMax) sortedItem.ThrowException(new OverWeightException(weight));
                sortedItem.Weight = weight;
                sortedItem.ApplyOutboundLabel();
            }
            catch(Exception ex) {
                if(!sortedItem.IsError()) sortedItem.SortException = new HaveNoIdeaWhatItIsException(ex);
                sortedItem.ApplyOutboundLabel(); //Apply error label
            }
            return sortedItem;
        }
        protected virtual void DetermineAssignment(string[] inputs,SortedItem sortedItem) {
            //Default implementation to determine the current freight assignment-
            //typically a station has one assignment
            try {
                //Associate freight and sort information
                sortedItem.Freight = Self.Assignments.Item(0).InboundFreight;
                sortedItem.SortProfile = Self.Assignments.Item(0).SortProfile;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while determining the current freight assignment.",ex); }
        }
        internal virtual void DetermineDestinationAndRounting(SortedItem sortedItem,InboundLabel label) { }
        internal virtual void OnHasDataChanged(object sender,LabelDataEventArgs e) { ArgixTrace.WriteLine(new TraceMessage(e.Element + " " + e.Data + " received.",AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    ")); }
        internal virtual void OnInputReceived(object sender,LabelDataEventArgs e) { ArgixTrace.WriteLine(new TraceMessage("Label input " + e.Element + " received [input: " + e.Data + " ].",AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    ")); }
        internal virtual void OnAllInputsReceived(object sender,EventArgs e) { ArgixTrace.WriteLine(new TraceMessage("All label inputs received.",AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    ")); }
        #region Local Services: getInboundLabelWithData()
        private InboundLabel getInboundLabelWithData(SortedItem sortedItem,string[] inputs) {
            //Create a copy of the inbound label for the current assignment
            InboundLabel label = sortedItem.SortProfile.InboundLabel.Copy();
            sortedItem.InboundLabel = label;
            label.HasDataChanged += new LabelDataEventHandler(OnHasDataChanged);
            label.InputReceived += new LabelDataEventHandler(OnInputReceived);
            label.AllInputsReceived += new EventHandler(OnAllInputsReceived);
            label.ClearData();

            //Map the inputs to the inbound label and complete sorted item processing
            try { label.DetermineInputs(inputs); }
            catch(Exception ex) { sortedItem.ThrowException(new InboundLabelException(ex)); }
            return label;
        }
        #endregion
    }
    public class EmptyBrain:Brain {
        //Members		
        //Interface
        public EmptyBrain() { }
        public override string Name { get { return "EmptyBrain"; } }
        public override SortedItem CreateSortedItem(string[] inputs,int weight) {
            //
            SortedItem sortedItem = Self.NewSortedItem();
            sortedItem.ThrowException(new NoAssignmentException());
            sortedItem.ApplyOutboundLabel();
            return sortedItem;
        }
    }
    public class RegularBrain:Brain {
        //Members		
        //Interface
        public RegularBrain() { }
        public override string Name { get { return "RegularBrain"; } }
        internal override void DetermineDestinationAndRounting(SortedItem sortedItem,InboundLabel label) {
            //
            int destNumber = int.Parse(label.GetElementValue("STORE"));
            sortedItem.CartonNumber = label.GetElementValue("CARTON");
            sortedItem.PONumber = label.GetElementValue("PO");
            string shipOverride = (ShipOverride.Length > 0 ? ShipOverride : label.GetElementValue("OSOVERRIDE"));
            try {
                sortedItem.SpecialAgent = SortFactory.CreateSpecialAgent(sortedItem.Client.Number,sortedItem.Client.Division,shipOverride);
                ArgixTrace.WriteLine(new TraceMessage("Special Agent: " + sortedItem.SpecialAgent.Type,AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                sortedItem.DestinationRouting = SortFactory.CreateDestinationRouting(sortedItem.Client.Number,sortedItem.Client.Division,sortedItem.Freight.Shipper.NUMBER,Self.Station.TerminalID.ToString().PadLeft(2,'0'),destNumber,sortedItem.SpecialAgent.ZONE_CODE.Trim(),sortedItem.CartonNumber,!label.IsDuplicateElementAllowed("CARTON"));
                if(sortedItem.SpecialAgent.IsDefault)
                    sortedItem.SpecialAgent = SortFactory.CreateSpecialAgentByZone(sortedItem.Client.Number,sortedItem.Client.Division,sortedItem.DestinationRouting.ZoneCode.Trim());
                ArgixTrace.WriteLine(new TraceMessage("Special Agent: " + sortedItem.SpecialAgent.Type,AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                if(!UPSAllowed && sortedItem.SpecialAgent.Type == "UPSSpecialAgent") {
                    sortedItem.SpecialAgent = null;
                    throw new ApplicationException("UPS processing disabled.");
                }
                sortedItem.TrackingNumber = sortedItem.SpecialAgent.MakeTrackingNumber(sortedItem.DestinationRouting.OSSequence);
                sortedItem.LabelTemplate = SortFactory.CreateOBLabelTemplate(sortedItem.DestinationRouting.OutboundLabelType,Self.Station.PrinterType);
            }
            catch(Exception ex) { sortedItem.ThrowException(new DestinationRoutingException(ex)); }
        }
    }
    public class SANBrain:Brain {
        //Members		
        //Interface
        public SANBrain() { }
        public override string Name { get { return "SANBrain"; } }
        protected override void DetermineAssignment(string[] inputs,SortedItem sortedItem) {
            //Override the default implementation to determine the correct assignment
            //dynamically; if station has only one assignment, default implementation is OK
            try {
                if(Self.Assignments.Count == 1) {
                    //One assignment: use base implementation
                    base.DetermineAssignment(inputs,sortedItem);
                }
                else {
                    //Multiple assignments: determine assignment by associating the assignment 
                    //with the client who has a store with the input SAN number
                    //Create a default SAN label (for the purpose of extracting the SAN number only)
                    //and determine the SAN number from the inputs
                    InboundLabel label = FreightFactory.DefaultSanInboundLabel.Copy();
                    label.ClearData();
                    try { label.DetermineInputs(inputs); }
                    catch(Exception ex) { sortedItem.ThrowException(new InboundLabelException(ex)); }
                    string sanNumber = label.GetElementValue("SAN").Substring(0,6);
                    sanNumber += Helper.CheckDigitMod11(sanNumber);

                    //Determine the client for this SAN number and associate with an assignment
                    Client client = EnterpriseFactory.CreateClientForStoreSAN(Self.Assignments.Item(0).InboundFreight.Client.Division,sanNumber);
                    StationAssignment assignment = Self.Assignments.Item(client);
                    if(assignment == null) sortedItem.ThrowException(new ClientForSanException());

                    //Associate freight and sort information
                    sortedItem.Freight = assignment.InboundFreight;
                    sortedItem.SortProfile = assignment.SortProfile;
                }
            }
            catch(InboundLabelException ex) { throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while determining freight assignment for multiple SAN freight assignments.",ex); }
        }
        internal override void DetermineDestinationAndRounting(SortedItem sortedItem,InboundLabel label) {
            //
            string sanNumber = label.GetElementValue("SAN").Substring(0,6);
            sanNumber += Helper.CheckDigitMod11(sanNumber);
            sortedItem.SANNUmber = sanNumber;
            //sortedItem.CartonNumber = label.GetElementValue("CARTON");
            sortedItem.PONumber = label.GetElementValue("PO");
            string shipOverride = (ShipOverride.Length > 0 ? ShipOverride : label.GetElementValue("OSOVERRIDE"));
            try {
                sortedItem.SpecialAgent = SortFactory.CreateSpecialAgent(sortedItem.Client.Number,sortedItem.Client.Division,shipOverride);
                ArgixTrace.WriteLine(new TraceMessage("Special Agent: " + sortedItem.SpecialAgent.Type,AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                sortedItem.DestinationRouting = SortFactory.CreateSANDestinationRouting(sortedItem.Client.Number,sortedItem.Client.Division,sortedItem.Freight.Shipper.NUMBER,Self.Station.TerminalID.ToString().PadLeft(2,'0'),sanNumber,sortedItem.SpecialAgent.ZONE_CODE.Trim(),sortedItem.CartonNumber,!label.IsDuplicateElementAllowed("CARTON"));
                if(sortedItem.SpecialAgent.IsDefault)
                    sortedItem.SpecialAgent = SortFactory.CreateSpecialAgentByZone(sortedItem.Client.Number,sortedItem.Client.Division,sortedItem.DestinationRouting.ZoneCode.Trim());
                ArgixTrace.WriteLine(new TraceMessage("Special Agent: " + sortedItem.SpecialAgent.Type,AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                if(!UPSAllowed && sortedItem.SpecialAgent.Type == "UPSSpecialAgent") {
                    sortedItem.SpecialAgent = null;
                    throw new ApplicationException("UPS processing disabled.");
                }
                sortedItem.TrackingNumber = sortedItem.SpecialAgent.MakeTrackingNumber(sortedItem.DestinationRouting.OSSequence);
                sortedItem.LabelTemplate = SortFactory.CreateOBLabelTemplate(sortedItem.DestinationRouting.OutboundLabelType,Self.Station.PrinterType);
            }
            catch(Exception ex) { sortedItem.ThrowException(new DestinationRoutingException(ex)); }
        }
    }

    public class ManifestXStoreBrain:Brain {
        //Members		
        //Interface
        public ManifestXStoreBrain() { }
        public override string Name { get { return "ManifestXStoreBrain"; } }
        internal override void DetermineDestinationAndRounting(SortedItem sortedItem,InboundLabel label) {
            //
            string inputString = label.GetElementValue("STORE");
            sortedItem.CartonNumber = label.GetElementValue("CARTON");
            sortedItem.PONumber = label.GetElementValue("PO");
            sortedItem.TrackingNumber = "";
            string shipOverride = (ShipOverride.Length > 0 ? ShipOverride : label.GetElementValue("OSOVERRIDE"));
            try {
                sortedItem.SpecialAgent = SortFactory.CreateSpecialAgent(sortedItem.Client.Number,sortedItem.Client.Division,shipOverride);
                ArgixTrace.WriteLine(new TraceMessage("Special Agent: " + sortedItem.SpecialAgent.Type,AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                //sortedItem.DestinationRouting = SortFactory.CreateDestinationRoutingManifestX(sortedItem.Client.Number, sortedItem.Client.Division, sortedItem.Freight.Shipper.NUMBER, Self.Station.TerminalID.ToString().PadLeft(2, '0'), sortedItem.Freight.FreightID, inputString, sortedItem.SpecialAgent.ZONE_CODE.Trim(), sortedItem.CartonNumber, !label.IsDuplicateElementAllowed("CARTON"));
                sortedItem.DestinationRouting = CreateDestinationRoutingManifest(sortedItem,inputString,!label.IsDuplicateElementAllowed("CARTON"));
                if(sortedItem.DestinationRouting.ManifestCartonNumber.Length > 0) sortedItem.CartonNumber = sortedItem.DestinationRouting.ManifestCartonNumber;
                if(sortedItem.DestinationRouting.ManifestPONumber.Length > 0) sortedItem.PONumber = sortedItem.DestinationRouting.ManifestPONumber;
                if(sortedItem.DestinationRouting.ManifestTrackingNumber.Length > 0) sortedItem.TrackingNumber = sortedItem.DestinationRouting.ManifestTrackingNumber;

                if(sortedItem.SpecialAgent.IsDefault)
                    sortedItem.SpecialAgent = SortFactory.CreateSpecialAgentByZone(sortedItem.Client.Number,sortedItem.Client.Division,sortedItem.DestinationRouting.ZoneCode.Trim());
                ArgixTrace.WriteLine(new TraceMessage("Special Agent: " + sortedItem.SpecialAgent.Type,AppLib.EVENTLOGNAME,LogLevel.Debug,"Brain    "));
                if(!UPSAllowed && sortedItem.SpecialAgent.Type == "UPSSpecialAgent") {
                    sortedItem.SpecialAgent = null;
                    throw new ApplicationException("UPS processing disabled.");
                }
                string trackingNum = sortedItem.SpecialAgent.MakeTrackingNumber(sortedItem.DestinationRouting.OSSequence);
                if(trackingNum.Trim().Length > 0) sortedItem.TrackingNumber = trackingNum;
                sortedItem.LabelTemplate = SortFactory.CreateOBLabelTemplate(sortedItem.DestinationRouting.OutboundLabelType,Self.Station.PrinterType);
            }
            catch(Exception ex) { sortedItem.ThrowException(new DestinationRoutingException(ex)); }
        }
        internal virtual DestinationRouting CreateDestinationRoutingManifest(SortedItem sortedItem,string inputString,bool checkForDuplicates) {
            //Polymorphic calls to SortFactory.CreateDestinationRouting???()
            return SortFactory.CreateDestinationRoutingManifestX(sortedItem.Client.Number,sortedItem.Client.Division,sortedItem.Freight.Shipper.NUMBER,Self.Station.TerminalID.ToString().PadLeft(2,'0'),sortedItem.Freight.FreightID,inputString,sortedItem.SpecialAgent.ZONE_CODE.Trim(),sortedItem.CartonNumber,checkForDuplicates);
        }
    }
    public class MultiManifestXStoreBrain:ManifestXStoreBrain {
        //Members		
        //Interface
        public MultiManifestXStoreBrain() { }
        public override string Name { get { return "MultiManifestXStoreBrain"; } }
        internal override DestinationRouting CreateDestinationRoutingManifest(SortedItem sortedItem,string inputString,bool checkForDuplicates) {
            //Polymorphic calls to SortFactory.CreateDestinationRouting???()
            return SortFactory.CreateDestinationRoutingMultiManifestX(sortedItem.Client.Number,sortedItem.Client.Division,sortedItem.Freight.Shipper.NUMBER,Self.Station.TerminalID.ToString().PadLeft(2,'0'),sortedItem.Freight.FreightID,inputString,sortedItem.SpecialAgent.ZONE_CODE.Trim(),sortedItem.CartonNumber,checkForDuplicates);
        }
    }
    public class JITBrain:Brain { public JITBrain() { } public override string Name { get { return "JITBrain"; } } }
    public class JITSANBrain:Brain { public JITSANBrain() { } public override string Name { get { return "JITSANBrain"; } } }
    public class SKUBrain:Brain { public SKUBrain() { } public override string Name { get { return "SKUBrain"; } } }
    public class ReturnsBrain:Brain { public ReturnsBrain() { } public override string Name { get { return "ReturnsBrain"; } } }
    public class ALTVBrain:Brain { public ALTVBrain() { } public override string Name { get { return "ALTVBrain"; } } }
    public class DDUBrain:Brain { public DDUBrain() { } public override string Name { get { return "DDUBrain"; } } }
    public class ALTSBrain:Brain { public ALTSBrain() { } public override string Name { get { return "ALTSBrain"; } } }
    public class UniqueStoreBrain:Brain { public UniqueStoreBrain() { } public override string Name { get { return "UniqueStoreBrain"; } } }
}
