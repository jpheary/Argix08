//	File:	dispatchnode.cs
//	Author:	J. Heary
//	Date:	09/20/05
//	Desc:	Top-level node; children are pickup logs.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Argix.Data;

namespace Argix.Dispatch {
	//Class and interface definitions	
	public class DispatchNode: TsortNode {
		//Class members
		public static TreeNode ScheduleNodes=null;
		
		//Members
		private Mediator mMediator=null;
		
		//Constants
		
		//Interface
		public DispatchNode() : base() { }
		public DispatchNode(string text, int imageIndex, int selectedImageIndex, Mediator mediator) : base(text, imageIndex, selectedImageIndex) { 
			this.mMediator = mediator;
		}
		#region TsortNode Implementations: LoadChildNodes(), CanOpen, Properties(), Refresh()
		public override void LoadChildNodes() {
			//Load child nodes of this node (data)
			try {
				//Clear existing nodes
				base.Nodes.Clear();
				
				//Read schedules from config file (key=schedule name, value=)
				Hashtable oDict = (Hashtable)ConfigurationSettings.GetConfig("dispatch/schedules");
				IDictionaryEnumerator oEnum = oDict.GetEnumerator();
				for(int i=0; i<oDict.Count; i++) {
					//Create a schedule for each entry; wrap in a custom treenode
					oEnum.MoveNext();
					DictionaryEntry oEntry = (DictionaryEntry)oEnum.Current;
					try {
						ScheduleNode scheduleNode=null, advanceNode=null, archiveNode=null;
						DispatchSchedule schedule=null, advancedSchedule=null, archiveSchedule=null;
						if(Convert.ToBoolean(oEntry.Value)) {
							switch(oEntry.Key.ToString()) {
								case "Client Inbound Schedule": 
									schedule = new ClientInboundSchedule("Argix Client Inbound Sheet","ClientInboundTable","_clientinbound",this.mMediator); 
									scheduleNode = new ScheduleNode(schedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, schedule);
									advancedSchedule = new ClientInboundSchedule("Advance Inbound Sheet","ClientInboundTable","_clientinboundadvance",this.mMediator); 
									advanceNode = new ScheduleNode(advancedSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, advancedSchedule);
									scheduleNode.Nodes.Add(advanceNode);
									advanceNode.LoadChildNodes();
									archiveSchedule = new ClientInboundSchedule("Archive of Inbound Sheet","ClientInboundTable","_clientinboundarchive",this.mMediator); 
									archiveNode = new ScheduleNode(archiveSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, archiveSchedule);
									scheduleNode.Nodes.Add(archiveNode);
									archiveNode.LoadChildNodes();
									break;
								case "Inbound Schedule":		
									schedule = new InboundSchedule("Scheduled Inbound","ScheduledInboundTable","_inbound",this.mMediator); 
									scheduleNode = new ScheduleNode(schedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, schedule);
									advancedSchedule = new InboundSchedule("Advance Inbound","ScheduledInboundTable","_inboundadvance",this.mMediator); 
									advanceNode = new ScheduleNode(advancedSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, advancedSchedule);
									scheduleNode.Nodes.Add(advanceNode);
									advanceNode.LoadChildNodes();
									archiveSchedule = new InboundSchedule("Archive of Inbound Freight","ScheduledInboundTable","_inboundarchive",this.mMediator); 
									archiveNode = new ScheduleNode(archiveSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, archiveSchedule);
									scheduleNode.Nodes.Add(archiveNode);
									archiveNode.LoadChildNodes();
									break;
								case "Pickup Log":				
									schedule = new PickupLog("Pickup Log","PickupLogTable","_pickup",this.mMediator); 
									scheduleNode = new ScheduleNode(schedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, schedule);
									archiveSchedule = new PickupLog("Pickup Log Archive","PickupLogTable","_pickuparchive",this.mMediator); 
									archiveNode = new ScheduleNode(archiveSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, archiveSchedule);
									scheduleNode.Nodes.Add(archiveNode);
									archiveNode.LoadChildNodes();
									break;
								case "Outbound Schedule":		
									schedule = new OutboundSchedule("Scheduled Outbound","ScheduledOutboundTable","_outbound",this.mMediator); 
									scheduleNode = new ScheduleNode(schedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, schedule);
									advancedSchedule = new OutboundSchedule("Advance Outbound","ScheduledOutboundTable","_outboundadvance",this.mMediator); 
									advanceNode = new ScheduleNode(advancedSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, advancedSchedule);
									scheduleNode.Nodes.Add(advanceNode);
									advanceNode.LoadChildNodes();
									archiveSchedule = new OutboundSchedule("Archive of Outbound Freight","ScheduledOutboundTable","_outboundarchive",this.mMediator); 
									archiveNode = new ScheduleNode(archiveSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, archiveSchedule);
									scheduleNode.Nodes.Add(archiveNode);
									archiveNode.LoadChildNodes();
									break;
								case "LineHaul Schedule":		
									schedule = new LineHaulSchedule("Line Haul Schedule","LineHaulTable","_linehaul",this.mMediator); 
									scheduleNode = new ScheduleNode(schedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, schedule);
									advancedSchedule = new LineHaulSchedule("Advance Line Haul Schedule","LineHaulTable","_linehauladvance",this.mMediator); 
									advanceNode = new ScheduleNode(advancedSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, advancedSchedule);
									scheduleNode.Nodes.Add(advanceNode);
									advanceNode.LoadChildNodes();
									archiveSchedule = new LineHaulSchedule("Archive of Line Haul Schedule","LineHaulTable","_linehaularchive",this.mMediator); 
									archiveNode = new ScheduleNode(archiveSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, archiveSchedule);
									scheduleNode.Nodes.Add(archiveNode);
									archiveNode.LoadChildNodes();
									break;
								case "Trailer Tracking":				
									schedule = new TrailerLog("Trailer Tracking","TrailerLogTable","_trailerentry",this.mMediator); 
									scheduleNode = new ScheduleNode(schedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, schedule);
									archiveSchedule = new TrailerLog("Trailer Tracking Archive","TrailerLogTable","_trailerentryarchive",this.mMediator); 
									archiveNode = new ScheduleNode(archiveSchedule.ScheduleName, App.ICON_CLOSED, App.ICON_CLOSED, archiveSchedule);
									scheduleNode.Nodes.Add(archiveNode);
									archiveNode.LoadChildNodes();
									break;
							}
							base.Nodes.Add(scheduleNode);
							scheduleNode.LoadChildNodes();
							ScheduleNodes = this;
						}
					}
					catch(Exception) { }
				}				
			} 
			catch(Exception ex) { throw ex; }
		}
		public override bool CanOpen { get { return false; } }
		public override void Properties() { }
		#endregion
		public override object Clone() {
			//Clone this object for display in another treeview
			TreeNode node = (TreeNode)base.Clone();
			return node;
		}
	}
}
