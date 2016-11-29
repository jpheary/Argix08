//	File:	pandapacketmgr.cs
//	Author:	J. Heary
//	Date:	10/19/06
//	Desc:	The PandaPacketMgr is responsible for processing message requests
//			from PandA clients that conform to the PandA message specification. 
//			Services include:
//			1. Convert a client socket request (bytes) into a valid PandA packet
//			2. Manage a collection (dataset) of all PandA requests
//			3. Handle resent request messages (last message only)
//			4. Form a PandA response packet for a valid client request; deliver 
//			   the response through the requesting socket client
//			NOTES:
//			1. This class is implemented as a singleton
//			2. TODO: Manage PandA packet dataset
//			3. Find an existing carton for re-circs (ASSUMPTION: cartonID is in one 
//			   of the barcode scans); currently not supported by PandA system
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Text;
using Tsort.Sort;

namespace Tsort.PandA {
	//
	public class PandaPacketMgr {
		//Members
		private static PandaPacket LastRequest=null;
		
		//Constants
		//Message Code constants (i.e. the service requested or responded to)
		public const int REQUEST_LABELDATA = 1;
		public const int REQUEST_VERIFYLABEL = 2;
		public const int REQUEST_HEARTBEAT = 3;
		public const int REQUEST_RESEND = 999;
		public const int RESPONSE_LABELDATA = 101;
		public const int RESPONSE_VERIFYLABEL = 102;
		public const int RESPONSE_HEARTBEAT = 103;
		public const int RESPONSE_INVALIDCODE = 999;
		public const int RESPONSE_MSGERROR = 999;
		
		//Events
		public static event PandaPacketEventHandler LabelDataRequestComplete=null;
		public static event PandaPacketEventHandler VerifyLabelRequestComplete=null;
		public static event EventHandler HeartbeatRequestComplete=null;
		public static event EventHandler ResendRequestComplete=null;
		
		//Interface
		static PandaPacketMgr() { }
		private PandaPacketMgr() { }
		public static byte[] ProcessPandARequest(string clientID, byte[] bytes) {
			//Process a PandA request from a remote socket client
			PandaPacket request=null;
			PandaPacket response=null;
			DateTime started=DateTime.Now;
			try {				
				//Process the message
				request = PandaPacket.Decode(clientID, bytes);
				AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("REQUEST:" + request.Message, AppLib.EVENTLOGNAME, LogLevel.Information, "PacketMgr"));
				if(request.Valid) {
					//Check if this was a prior request
					PandaPacket priorResponse = isPriorRequest(clientID, request);
					if(priorResponse != null) {
						//This was a prior message; return prior response
						response = priorResponse;
						AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("PRIOR RESPONSE: " +priorResponse.Message, AppLib.EVENTLOGNAME, LogLevel.Information, "PacketMgr"));
					}
					else {
						//New, valid message; persist request and route to message handler
						switch(request.MessageCode) {
							case REQUEST_LABELDATA:	
								response = getLabelDataResponse(clientID, request); 
								LastRequest = request;
								try { request.ProcessingTime = ((TimeSpan)DateTime.Now.Subtract(started)).TotalSeconds; } catch {}
								if(LabelDataRequestComplete != null) LabelDataRequestComplete(null, new PandaPacketEventArgs(request));
								break;
							case REQUEST_VERIFYLABEL: 
								response = getVerifyLabelResponse(clientID, request);
								LastRequest = request;
								try { request.ProcessingTime = ((TimeSpan)DateTime.Now.Subtract(started)).TotalSeconds; } catch {}
								if(VerifyLabelRequestComplete != null) VerifyLabelRequestComplete(null, new PandaPacketEventArgs(request));
								break;
							case REQUEST_HEARTBEAT:	
								response = getHeartbeatResponse(clientID, request); 
								if(HeartbeatRequestComplete != null) HeartbeatRequestComplete(null, EventArgs.Empty);
								break;
							case REQUEST_RESEND: 
								response = LastRequest.Response;
								AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("Resend requested.", AppLib.EVENTLOGNAME, LogLevel.Warning, "PacketMgr")); 
								if(ResendRequestComplete != null) ResendRequestComplete(null, EventArgs.Empty);
								break;
							default: 
								AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("Unknown message code.", AppLib.EVENTLOGNAME, LogLevel.Warning, "PacketMgr")); 
								break;
						}
					}
				}
				else { 
					//Invalid message packet
					response = PandaPacket.Encode(clientID, RESPONSE_MSGERROR, request, 0, 0, "");
					AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("Message failed (decode) validation...", AppLib.EVENTLOGNAME, LogLevel.Warning, "PacketMgr"));
				}
			}
			catch(Exception ex) { 
				response = PandaPacket.Encode(clientID, RESPONSE_MSGERROR, request, 0, 0, "");
				AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("Error occurred while processing message: " + ex.ToString(), AppLib.EVENTLOGNAME, LogLevel.Error, "PacketMgr"));
			}
			return (response!=null?response.Bytes:null);
		}
		#region Local Services: getLabelDataResponse(), getVerifyLabelResponse(), getHeartbeatResponse(), isPriorRequest()
		private static PandaPacket getLabelDataResponse(string clientID, PandaPacket request) {
			//
			PandaPacket response=null;
			DataSet ds=null;
			try {
				//Extract message body
				string barcode1 = request.MessageBody.Substring(0, 32).TrimEnd();
				string barcode2 = request.MessageBody.Substring(32, 32).TrimEnd();
				string barcode3 = request.MessageBody.Substring(64, 32).TrimEnd();
				string barcode4 = request.MessageBody.Substring(96, 32).TrimEnd();
				string barcode5 = request.MessageBody.Substring(128, 32).TrimEnd();
				string barcode6 = request.MessageBody.Substring(160, 32).TrimEnd();
				float weight = Convert.ToInt32(request.MessageBody.Substring(192, 5)) / 100;
				
				//Process the label data request
				ds = CartonMgr.ProcessLabelDataRequest(barcode1, barcode2, barcode3, barcode4, barcode5, barcode6, weight);
				if(ds != null) {
					//New carton
					response = PandaPacket.Encode(clientID, RESPONSE_LABELDATA, request, 0, 1, PandaPacket.FormatLabelDataMessageBody(ds.Tables["PandATable"].Rows[0]["CartonID"].ToString(), int.Parse(ds.Tables["PandATable"].Rows[0]["StatusCode"].ToString()), ds.Tables["PandATable"].Rows[0]["LabelData"].ToString()));
				}
			}
			catch(Exception ex) { 
				AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("Error occurred while processing request for label data- " + ex.ToString(), AppLib.EVENTLOGNAME, LogLevel.Error, "PacketMgr"));
				if(ds != null) 
					response = PandaPacket.Encode(clientID, RESPONSE_LABELDATA, request, 0, 1, PandaPacket.FormatLabelDataMessageBody(ds.Tables["PandATable"].Rows[0]["CartonID"].ToString(), int.Parse(ds.Tables["PandATable"].Rows[0]["StatusCode"].ToString()), ds.Tables["PandATable"].Rows[0]["LabelData"].ToString()));
			}
			return response;
		}
		private static PandaPacket getVerifyLabelResponse(string clientID, PandaPacket request) {
			//Verify the requested carton
			PandaPacket response=null;
			try {
				//Find the carton
				string cartonID = request.MessageBody.Substring(0, PandaPacket.CartonIDLength).TrimEnd();
				string verifyCode = request.MessageBody.Substring(PandaPacket.CartonIDLength, 1).TrimEnd();
				string verifyFlag = CartonMgr.ProcessVerifyLabelRequest(cartonID, verifyCode);
				response = PandaPacket.Encode(clientID, RESPONSE_VERIFYLABEL, request, 0, 1, verifyFlag);
			}
			catch(Exception ex) { 
				AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("An error occurred while processing request to verify label." + ex.ToString(), AppLib.EVENTLOGNAME, LogLevel.Error, "PacketMgr"));
				response = PandaPacket.Encode(clientID, RESPONSE_VERIFYLABEL, request, 0, 1, "N");
			}
			return response;
		}
		private static PandaPacket getHeartbeatResponse(string clientID, PandaPacket request) {
			//Reply with a heartbeat signal
			PandaPacket response=null;
			try {
				response = PandaPacket.Encode(clientID, RESPONSE_HEARTBEAT, request, 0, 1, "");
			}
			catch(Exception ex) { 
				AppTrace.Instance().TheTrace.WriteLine(new TraceMessage("An error occurred while processing request for heartbeat." + ex.ToString(), AppLib.EVENTLOGNAME, LogLevel.Error, "PacketMgr"));
			}
			return response;
		}
		private static PandaPacket isPriorRequest(string clientID, PandaPacket request) {
			//Return prior response if was a prior message
			//Return null if not a prior message
			PandaPacket response=null;
			try {
				//Check if this was a prior message
				PandaPacket priorPacket=null;
				if(LastRequest != null) {
					if(clientID == LastRequest.ClientID && LastRequest.MessageNumber == request.MessageNumber)
						priorPacket = LastRequest;
				}
				//Prior request? return prior response
				if(priorPacket != null) response = priorPacket.Response;
			}
			catch(Exception ex) {  
				throw new ApplicationException("Error occurred while validating message", ex);
			}
			return response;
		}
		#endregion
	}
}


