//	File:	pandaservice.cs
//	Author:	J. Heary
//	Date:	05/22/07
//	Desc:	The PandaService is responsible for processing PandA client requests
//			for label data and label verification. The service provides two modes
//			of access: 
//			1. messages (byte[]) that conform to the PandA message specification
//			2. PandA adapter interface
//	Rev:	12/03/07 (jph)- modified ProcessLabelDataRequest() to use getNowSeconds() 
//							that use 5 position time and 3 position station number.
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Argix;
using Argix.Windows;
using Tsort.Configuration;
using Tsort.Sort;

namespace Tsort.PandA {
	/// <summary>The PandaService is responsible for processing PandA client requests for label data and label verification.</summary>
	public class PandaService {
		//Members
		private frmPanda mPandaUI=null;
		private StationOperator mOperator=null;
		private PandaPacket mLastRequest=null;
		private TrayIcon mTrayIcon=null;
		private MenuItem ctxOpen=null;
		private bool mOperatorWorking=false;
		
		//Constants
		#region Message Code constants (i.e. the service requested or responded to)
		/// <summary>PandA protocol message code for a label data request = 1.</summary>
		public const int REQUEST_LABELDATA = 1;
		/// <summary>PandA protocol message code for a verify label request = 2.</summary>
		public const int REQUEST_VERIFYLABEL = 2;
		/// <summary>PandA protocol message code for a heartbeat request = 3.</summary>
		public const int REQUEST_HEARTBEAT = 3;
		/// <summary>PandA protocol message code for a resend message request = 999.</summary>
		public const int REQUEST_RESEND = 999;
		/// <summary>PandA protocol message code for a label data response = 101.</summary>
		public const int RESPONSE_LABELDATA = 101;
		/// <summary>PandA protocol message code for a verify label response = 102.</summary>
		public const int RESPONSE_VERIFYLABEL = 102;
		/// <summary>PandA protocol message code for a heartbeat response = 103.</summary>
		public const int RESPONSE_HEARTBEAT = 103;
		/// <summary>PandA protocol message code for an invalid [request] code response = 999.</summary>
		public const int RESPONSE_INVALIDCODE = 999;
		/// <summary>PandA protocol message code for an [request] error response = 999.</summary>
		public const int RESPONSE_MSGERROR = 999;
		#endregion
		#region Carton Status constants (carton processing options)
		/// <summary>andA protocol carton status code for an unprocessed carton = 0.</summary>
		public const int STATUS_NONE = 0;
		/// <summary>PandA protocol carton status code for a carton that processed without error = 1.</summary>
		public const int STATUS_CARTON_OK = 1;
		/// <summary>PandA protocol carton status code for a carton with no data from the scanner (label fields blank) = 2.</summary>
		public const int STATUS_SCANERROR_NODATA = 2;
		/// <summary>PandA protocol carton status code for a carton with a no read (??? in place of label) = 3.</summary>
		public const int STATUS_SCANERROR_NOREAD = 3;
		/// <summary>PandA protocol carton status code for a carton with a label conflict (### in place of label) = 4.</summary>
		public const int STATUS_SCANERROR_CONFLICT = 4;
		/// <summary>PandA protocol carton status code for a carton with an undetermined label (failed label data processing) = 5.</summary>
		public const int STATUS_SCANERROR_LABELFAILED = 5;
		/// <summary>PandA protocol carton status code for a carton with a bad weight = 8.</summary>
		public const int STATUS_SCANERROR_WEIGHTBAD = 8;
		/// <summary>PandA protocol carton status code for a carton that should ignore label printing = 6.</summary>
		public const int STATUS_CARTON_VERIFYONLY = 6;
		/// <summary>PandA protocol carton status code for a carton that should ignore label printing and label verification = 7.</summary>
		public const int STATUS_CARTON_IGNORE = 7;
		/// <summary>PandA protocol carton status code for a carton with a generic error = 9.</summary>
		public const int STATUS_ERROR_UNKNOWN = 9;
		#endregion
		#region Carton Verify constants (carton processing options)
		/// <summary>PandA carton verify code for a carton that passed label verification = '1'.</summary>
		public const string VERIFY_PASS = "1";
		/// <summary>PandA carton verify code for a carton that failed label verification = '2'.</summary>
		public const string VERIFY_FAIL = "2";
		/// <summary>PandA carton verify code for a carton that had a no read at label verification = '3'.</summary>
		public const string VERIFY_NOREAD = "3";
		
		/// <summary>PandA carton verify flag for a carton that completed processing = 'Y'.</summary>
		public const string VERIFY_YES = "Y";
		/// <summary>PandA carton verify flag for a carton that failed processing = 'N'.</summary>
		public const string VERIFY_NO = "N";
		#endregion
		private const int MNU_ICON_OPEN = 0;
		
		//Events
		/// <summary>Occurs when a PandA packet request is completed.</summary>
		public event PandaPacketEventHandler PandaRequestComplete=null;
		/// <summary>Occurs when a PandA packet request is resent.</summary>
		public event EventHandler PandaResendComplete=null;
		/// <summary>Occurs when a PandA packet heartbeat request is completed. </summary>
		public event EventHandler HeartbeatRequestComplete=null;
		/// <summary>Occurs when a label data request is started.</summary>
		public event EventHandler LabelDataRequest=null;
		/// <summary>Occurs when a label data request is completed.</summary>
		public event PandaLabelDataEventHandler LabelDataRequestComplete=null;
		/// <summary>Occurs when a verify label request is completed.</summary>
		public event PandaVerifyLabelEventHandler VerifyLabelRequestComplete=null;
		
		//Interface
		/// <summary>Creates a new instance of the Tsort.PandA.PandaService class with database connection from app.config and no user unterface.</summary>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		public PandaService(): this("", false) { }
		/// <summary>Creates a new instance of the Tsort.PandA.PandaService class with database connection from app.config and no user unterface.</summary>
		/// <param name="enableUI">Set to true to have a task tray icon displayed that provides access to the Panda Service user interface.</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		public PandaService(bool enableUI): this("", enableUI) { }
		/// <summary>Creates a new instance of the Tsort.PandA.PandaService class with no user unterface.</summary>
		/// <param name="databaseConnection">An ADO.Net database connection string.</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		public PandaService(string databaseConnection): this(databaseConnection, false) { }
		/// <summary>Creates a new instance of the Tsort.PandA.PandaService class.</summary>
		/// <param name="databaseConnection">An ADO.Net database connection string.</param>
		/// <param name="enableUI">Set to true to have a task tray icon displayed that provides access to the Panda Service user interface.</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		public PandaService(string databaseConnection, bool enableUI) { 
			//Constructor
			try {
				//Configure user access
				if(enableUI) {
					this.mTrayIcon = new TrayIcon("PandA Library", new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Tsort.PandA.panda.ico")));
					#region Tray Icon Menu Configuration
					ctxOpen = new MenuItem("Open", new System.EventHandler(this.OnIconMenuClicked));
					ctxOpen.Index = MNU_ICON_OPEN;
					ctxOpen.Enabled = true;
					ctxOpen.DefaultItem = true;
					this.mTrayIcon.MenuItems.AddRange(new MenuItem[] {ctxOpen});
					#endregion
					this.mTrayIcon.DoubleClick += new System.EventHandler(OnIconDoubleClicked);
				}
				
				//Get configuration
				PandaPacket.ValidateMessageLength = Convert.ToBoolean(DllConfig.AppSettings("ValidateMessageLength"));
				PandaPacket.CartonIDLength = Convert.ToInt32(DllConfig.AppSettings("CartonIDLength"));
				PandaPacket.LabelDataLength = Convert.ToInt32(DllConfig.AppSettings("LabelDataLength"));				
				if(databaseConnection.Trim().Length == 0) databaseConnection = DllConfig.AppSettings("SQLConnection");
				LogLevel level = (LogLevel)Convert.ToInt32(DllConfig.AppSettings("TraceLevel"));
				
				//Create objects
				ArgixTrace.AddListener(new ArgixEventLogTraceListener(level, AppLib.PRODUCTNAME));
				this.mOperator = new StationOperator(databaseConnection);
				if(enableUI) this.mPandaUI = new frmPanda(this, level);
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new PandaService instance.", ex); }
		}
		/// <summary>Destructor</summary>
		~PandaService() { try { Dispose(); } catch { } }
		/// <summary></summary>
		public void Dispose() { if(this.mPandaUI != null) this.mPandaUI.Close(); if(this.mTrayIcon != null) this.mTrayIcon.Dispose(); }
		/// <summary>Gets the underlying Tsort.Sort.StationOperator object.</summary>
		public StationOperator SortOperator { get { return this.mOperator; } }
		/// <summary>Forces the Sort Service to update it's freight sorting assignments. The Sort Service configures itself from database on the first refresh.</summary>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		public void RefreshSortService() { 
			//
			try {
				if(!this.mOperatorWorking) {
					ArgixTrace.WriteLine(new TraceMessage("Starting Sort Service Operator...", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc")); 
					this.mOperator.StartWork();
					this.mOperatorWorking = true;
				}
			}
			catch(ApplicationException ex) { 
				ArgixTrace.WriteLine(new TraceMessage("Error while starting the Sort Service Operator-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc")); 
				throw ex; 
			}
			catch(Exception ex) { 
				ArgixTrace.WriteLine(new TraceMessage("Unexpected error while starting the Sort Service Operator-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc")); 
				throw new ApplicationException("Unexpected error while starting the Sort Service Operator.", ex); 
			}
			
			try {
				ArgixTrace.WriteLine(new TraceMessage("Refreshing the Sort Service...", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc")); 
				this.mOperator.RefreshStationAssignments(); 
			}
			catch(ApplicationException ex) { 
				ArgixTrace.WriteLine(new TraceMessage("Error while refreshing the Sort Service-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc")); 
				throw ex; 
			}
			catch(Exception ex) { 
				ArgixTrace.WriteLine(new TraceMessage("Unexpected error while refreshing the Sort Service-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc")); 
				throw new ApplicationException("Unexpected error while refreshing the Sort Service.", ex); 
			}
		}
		/// <summary>Processes </summary>
		/// <param name="clientID">A unique identifier for the requestor (can be an empty string).</param>
		/// <param name="request">A Tsort.PandA.PandaPacket request.</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		/// <returns>An instance of Tsort.PandA.PandaPacket containing the response.</returns>
		public PandaPacket ProcessPandARequest(string clientID, PandaPacket request) {
			//Process a PandA request from a remote socket client
			PandaPacket response=null;
			DateTime started=DateTime.Now;
			try { 
				//Process the PandA message; Decode() always returns a non-null request object
				ArgixTrace.WriteLine(new TraceMessage("", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc")); 
				ArgixTrace.WriteLine(new TraceMessage("REQUEST:" + request.Message, AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				if(request.Valid) {
					//Check if this was a prior request
					PandaPacket priorResponse = isPriorRequest(clientID, request);
					if(priorResponse != null) {
						//This was a prior message; return prior response
						response = priorResponse;
						ArgixTrace.WriteLine(new TraceMessage("PRIOR RESPONSE: " +priorResponse.Message, AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
					}
					else {
						//New, valid message; persist request and route to message handler
						switch(request.MessageCode) {
							case REQUEST_LABELDATA:	
								DataSet ds=null;
								try {
									//Extract message body and process message request
									string barcode1 = request.MessageBody.Substring(0, 32).TrimEnd();
									string barcode2 = request.MessageBody.Substring(32, 32).TrimEnd();
									string barcode3 = request.MessageBody.Substring(64, 32).TrimEnd();
									string barcode4 = request.MessageBody.Substring(96, 32).TrimEnd();
									string barcode5 = request.MessageBody.Substring(128, 32).TrimEnd();
									string barcode6 = request.MessageBody.Substring(160, 32).TrimEnd();
									decimal weight = decimal.Parse(request.MessageBody.Substring(192, 5)) / 100;
									string[] inputs = new string[]{ barcode1,barcode2,barcode3,barcode4,barcode5,barcode6};
									ds = ProcessLabelDataRequest(inputs, weight);
									if(ds != null) 
										response = PandaPacket.Encode(clientID, RESPONSE_LABELDATA, 0, 1, PandaPacket.FormatLabelDataMessageBody(ds.Tables["PandATable"].Rows[0]["CartonID"].ToString(), int.Parse(ds.Tables["PandATable"].Rows[0]["StatusCode"].ToString()), ds.Tables["PandATable"].Rows[0]["LabelData"].ToString()), request.MessageNumber, request);
								}
								catch(Exception ex) { 
									if(ds != null) 
										response = PandaPacket.Encode(clientID, RESPONSE_LABELDATA, 0, 1, PandaPacket.FormatLabelDataMessageBody(ds.Tables["PandATable"].Rows[0]["CartonID"].ToString(), int.Parse(ds.Tables["PandATable"].Rows[0]["StatusCode"].ToString()), ds.Tables["PandATable"].Rows[0]["LabelData"].ToString()), request.MessageNumber, request);
									ArgixTrace.WriteLine(new TraceMessage("An error occurred while processing Panda label data request-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
								}
								this.mLastRequest = request;
								try { request.ProcessingTime = (float)((TimeSpan)DateTime.Now.Subtract(started)).TotalSeconds; } catch {}
								if(PandaRequestComplete != null) PandaRequestComplete(null, new PandaPacketEventArgs(request));
								break;
							case REQUEST_VERIFYLABEL: 
								try {
									//Find the carton
									string cartonID = request.MessageBody.Substring(0, PandaPacket.CartonIDLength).TrimEnd();
									string verifyCode = request.MessageBody.Substring(PandaPacket.CartonIDLength, 1).TrimEnd();
									string verifyFlag = ProcessVerifyLabelRequest(cartonID, verifyCode);
									response = PandaPacket.Encode(clientID, RESPONSE_VERIFYLABEL, 0, 1, verifyFlag, request.MessageNumber, request);
								}
								catch(Exception ex) { 
									response = PandaPacket.Encode(clientID, RESPONSE_VERIFYLABEL, 0, 1, VERIFY_NO, request.MessageNumber, request);
									ArgixTrace.WriteLine(new TraceMessage("An error occurred while processing Panda verify label request-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
								}
								this.mLastRequest = request;
								try { request.ProcessingTime = (float)((TimeSpan)DateTime.Now.Subtract(started)).TotalSeconds; } catch {}
								if(PandaRequestComplete != null) PandaRequestComplete(null, new PandaPacketEventArgs(request));
								break;
							case REQUEST_RESEND: 
								response = this.mLastRequest.Response;
								ArgixTrace.WriteLine(new TraceMessage("Resend requested.", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc")); 
								if(PandaResendComplete != null) PandaResendComplete(null, EventArgs.Empty);
								break;
							case REQUEST_HEARTBEAT:	
								try {
									response = PandaPacket.Encode(clientID, RESPONSE_HEARTBEAT, 0, 1, "", request.MessageNumber, request);
								}
								catch(Exception ex) { 
									ArgixTrace.WriteLine(new TraceMessage("An error occurred while processing Panda heartbeat request-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
								}
								if(HeartbeatRequestComplete != null) HeartbeatRequestComplete(null, EventArgs.Empty);
								break;
							default: 
								ArgixTrace.WriteLine(new TraceMessage("Unknown message code.", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc")); 
								break;
						}
					}
				}
				else { 
					//Invalid message packet
					response = PandaPacket.Encode(clientID, RESPONSE_MSGERROR, 0, 0, "", request.MessageNumber, request);
					ArgixTrace.WriteLine(new TraceMessage("Message failed (decode) validation...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
				}
			}
			catch(Exception ex) { 
				response = PandaPacket.Encode(clientID, RESPONSE_MSGERROR, 0, 0, "", request.MessageNumber, request);
				ArgixTrace.WriteLine(new TraceMessage("An error occurred while processing Panda message-->" + ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
			}
			finally { 
				if(response != null) 
					ArgixTrace.WriteLine(new TraceMessage("RESPONSE: " + new ASCIIEncoding().GetString(response.Bytes), AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				else 
					ArgixTrace.WriteLine(new TraceMessage("RESPONSE: NULL", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
			}
			return response;
		}
		/// <summary>Process a label data request for a scanned carton.</summary>
		/// <param name="inputs">Carton barcode data.</param>
		/// <param name="weight">Carton weight.</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		/// <returns>An instance of Tsort.PandA.PandaDS containing the label data response (in the Tsort.PandA.PandaDS.PandaTable).</returns>
		public PandaDS ProcessLabelDataRequest(string[] inputs, decimal weight) {
			//Create a new carton
			string cartonID="", zpl="";
			int statusCode=STATUS_NONE;
			ApplicationException aex=null;
			PandaDS ds=new PandaDS();

			//Validate for non-carton requests (scanner tripped)
			bool hasInputData=false;
			string _inputs="";
			for(int i=0; i<inputs.Length; i++) {
				if(inputs[i].Trim().Length > 0) hasInputData = true;
				_inputs+= (i+1).ToString() + "=" + inputs[i] + "; ";
			}
			ArgixTrace.WriteLine(new TraceMessage("LABEL DATA REQUEST: " + _inputs + "weight=" + weight.ToString(), AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
			if(!hasInputData && weight == 0) {
				ArgixTrace.WriteLine(new TraceMessage("LABEL DATA RESPONSE: no inputs; no weight; processing aborted...", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				return ds;
			}
			try {
				//Begin processing a new message request
				if(this.LabelDataRequest != null) this.LabelDataRequest(null, EventArgs.Empty);					
				try {
                    //Create a default carton identifier
                    DateTime dt = DateTime.Now;
                    int sec = ((3600 * dt.Hour) + (60 * dt.Minute) + dt.Second);
                    cartonID = dt.ToString("yyyy").Substring(3,1) + dt.ToString("MM") +  dt.ToString("dd") + sec.ToString("00000") + "0000";
                    
                    //Get an outbound label for this carton
					SortedItem sortedItem = this.mOperator.ProcessInputs(inputs, weight);
					if(sortedItem != null) {
						try {
							cartonID = sortedItem.LabelNumber;
							zpl = sortedItem.LabelFormat;
							sortedItem.ThrowException();
							statusCode = STATUS_CARTON_OK;
							ArgixTrace.WriteLine(new TraceMessage("Carton processing successful [label data]...", AppLib.PRODUCTNAME, LogLevel.Debug, "PandaSvc"));
						}
						catch(Tsort.InboundLabelException ex) { 
							//Test for various inbound label exceptions
							bool allInvalid=true;
							for(int i=0; i<inputs.Length; i++) { if(inputs[i].Trim().Length>0) { allInvalid=false; break; } }
							if(allInvalid) {
								statusCode = STATUS_SCANERROR_NODATA;
								ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [no data from scanner]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
							}
							allInvalid=true;
							for(int i=0; i<inputs.Length; i++) { if(inputs[i].Replace("?", "").Trim().Length>0) { allInvalid=false; break; } }
							if(allInvalid && statusCode == STATUS_NONE) {
								statusCode = STATUS_SCANERROR_NOREAD;
								ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [no read: ?]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
							}
							allInvalid=true;
							for(int i=0; i<inputs.Length; i++) { if(inputs[i].Replace("#", "").Trim().Length>0) { allInvalid=false; break; } }
							if(allInvalid && statusCode == STATUS_NONE) {
								statusCode = STATUS_SCANERROR_CONFLICT;
								ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [label conflict: #]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
							}
							if(statusCode == STATUS_NONE) {
								statusCode = STATUS_SCANERROR_LABELFAILED;
								ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [inbound label exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
							}							
						}
						catch(Tsort.ZeroWeightException ex) { 
							statusCode = STATUS_SCANERROR_WEIGHTBAD;
							ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [zero weight exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
						}
						catch(Tsort.OverWeightException ex) { 
							statusCode = STATUS_SCANERROR_WEIGHTBAD;
							ArgixTrace.WriteLine(new TraceMessage("Message data validation failed [over weight exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
						}
						catch(Tsort.DestinationRoutingException ex) { 
							statusCode = STATUS_SCANERROR_LABELFAILED;
							ArgixTrace.WriteLine(new TraceMessage("Sorted item processing failed [destination/routing exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
						}
						catch(Exception ex) { 
							statusCode = STATUS_SCANERROR_LABELFAILED;
							ArgixTrace.WriteLine(new TraceMessage("Sorted item processing failed [unexpected exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
						}
						zpl = zpl.Replace(Tsort.Labels.TokenLibrary.STATUSCODE, statusCode.ToString());
					}
					else {
						aex = new ApplicationException("Carton processing failed [no sorted item]...");
						statusCode = STATUS_SCANERROR_LABELFAILED;
						zpl = createErrorLabelFormat(STATUS_SCANERROR_LABELFAILED, aex);
						ArgixTrace.WriteLine(new TraceMessage(aex.Message, AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
					}
				}
				catch(Exception ex) { 
					aex = new ApplicationException("Carton processing failed [sorted item processing threw an exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "..." , ex);
					statusCode = STATUS_SCANERROR_LABELFAILED;
					zpl = createErrorLabelFormat(STATUS_SCANERROR_LABELFAILED, ex);
					ArgixTrace.WriteLine(new TraceMessage(aex.Message, AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
				}
				ds.PandaTable.AddPandaTableRow(cartonID, statusCode, zpl);
				ds.AcceptChanges();
			}
			catch(Exception ex) { 
				aex = new ApplicationException("Carton processing failed [unexpected exception]-->" + ex.Message + (ex.InnerException!=null?"-->" + ex.InnerException.Message:"") + "..." , ex);
				statusCode = STATUS_ERROR_UNKNOWN;
				zpl = createErrorLabelFormat(STATUS_ERROR_UNKNOWN, ex);
				ds.PandaTable.AddPandaTableRow(cartonID, statusCode, zpl);
				ds.AcceptChanges();
				ArgixTrace.WriteLine(new TraceMessage(aex.Message, AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
			}
			finally { 
				ArgixTrace.WriteLine(new TraceMessage("LABEL DATA RESPONSE: " + cartonID + "; " + statusCode.ToString(), AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				if(this.LabelDataRequestComplete != null) { 
					PandaDS _ds = new PandaDS();
					_ds.CartonTable.AddCartonTableRow(cartonID, (inputs.Length>0?inputs[0]:""), (inputs.Length>1?inputs[1]:""), (inputs.Length>2?inputs[2]:""), (inputs.Length>3?inputs[3]:""), (inputs.Length>4?inputs[4]:""), (inputs.Length>5?inputs[5]:""), weight, statusCode, zpl, "", (aex!=null?aex.Message:""), "");
					this.LabelDataRequestComplete(this, new PandaLabelDataEventArgs(_ds.CartonTable[0]));
				}
			}
			return ds;
		}
		/// <summary>Process a verify label request for a carton.</summary>
		/// <param name="cartonID">The carton's 24 position unique [Argix] identifier.</param>
		/// <param name="verifyCode">A verify code for the carton (Use PandaService verify constants: PandaService.VERIFY_PASS, PandaService.VERIFY_FAIL, PandaService.VERIFY_NOREAD).</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		/// <returns>Returns a verify flag for the carton (PandaService.VERIFY_YES, PandaService.VERIFY_NO).</returns>
		public string ProcessVerifyLabelRequest(string cartonID, string verifyCode) {
			//Verify the requested carton
			string verifyFlag=VERIFY_NO;
			ApplicationException aex=null;
			try {
				//Begin processing a new message request
				ArgixTrace.WriteLine(new TraceMessage("VERIFY LABEL REQUEST: ID=" + cartonID + "; verify code=" + verifyCode, AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				
				//Validate
				if(!(cartonID.Length == 14 || cartonID.Length == 24))
					throw new ApplicationException("Invalid cartonID length (" + cartonID.Length.ToString() + "; must be 14 0r 24)...");
				
				//Find the carton
				SortedItem item = this.mOperator.GetSortedItem(cartonID.Length == 24 ? cartonID.Substring(10, 13) : cartonID.Substring(0, 13));
				if(item != null) {
					//Item found
					if(verifyCode == VERIFY_PASS) {
						//Pass: Save carton
						try {
							this.mOperator.StoreSortedItem(item);
							verifyFlag = VERIFY_YES;
							ArgixTrace.WriteLine(new TraceMessage("Carton save succeeded...", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
						}
						catch(Exception ex) {
							verifyFlag = VERIFY_NO;
							aex = new ApplicationException("Carton save failed...-->" + ex.Message, ex);
							ArgixTrace.WriteLine(new TraceMessage(aex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
						}
					}
					else if(verifyCode == VERIFY_FAIL || verifyCode == VERIFY_NOREAD) {	
						//Fail, No Read: Cancel carton
						verifyFlag = VERIFY_NO;
						this.mOperator.CancelSortedItem(item.LabelSeqNumber);
						ArgixTrace.WriteLine(new TraceMessage("Verify flag was fail or no read; carton cancelled...", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
					}
					else {	
						//Fail, No Read: Cancel carton
						verifyFlag = VERIFY_NO;
						this.mOperator.CancelSortedItem(item.LabelSeqNumber);
						ArgixTrace.WriteLine(new TraceMessage("Verify flag was unknown; carton cancelled...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
					}
				}
				else {
					//Carton not found
					verifyFlag = VERIFY_NO;
					ArgixTrace.WriteLine(new TraceMessage("Carton not found...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
				}
			}
			catch(ApplicationException ex) { 
				verifyFlag = VERIFY_NO;
				aex = ex;
				ArgixTrace.WriteLine(new TraceMessage(ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
			}
			catch(Exception ex) { 
				verifyFlag = VERIFY_NO;
				aex = new ApplicationException("An unexpected error occurred while processing verify label request-->" + ex.Message + "...", ex);
				ArgixTrace.WriteLine(new TraceMessage("An unexpected error occurred while processing verify label request-->" + ex.Message + "...", AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
			}
			finally { 
				ArgixTrace.WriteLine(new TraceMessage("VERIFY LABEL RESPONSE: " + cartonID + "; " + verifyFlag, AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				if(this.VerifyLabelRequestComplete != null) this.VerifyLabelRequestComplete(this, new PandaVerifyLabelEventArgs(cartonID, verifyFlag, aex)); 
			}
			return verifyFlag;
		}
		/// <summary>Process a verify label request for a carton.</summary>
		/// <param name="cartonID">The carton's 24 position unique [Argix] identifier.</param>
		/// <param name="inputs">A array of barcode scans including the Argix outbound label barcode.</param>
		/// <param name="verifyCode">A verify code for the carton (Use PandaService verify constants: PandaService.VERIFY_PASS, PandaService.VERIFY_FAIL, PandaService.VERIFY_NOREAD).</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		/// <returns>Returns a verify flag for the carton (PandaService.VERIFY_YES, PandaService.VERIFY_NO).</returns>
		public string ProcessVerifyLabelRequest(string cartonID, string[] inputs, string verifyCode) {
			//Verify the requested carton
			string verifyFlag=VERIFY_NO;
			ApplicationException aex=null;
			try {
				//Begin processing a new message request
				string _inputs="";
				for(int i=0; i<inputs.Length; i++) _inputs+= (i+1).ToString() + "=" + inputs[i] + "; ";
				ArgixTrace.WriteLine(new TraceMessage("VERIFY LABEL REQUEST: ID=" + cartonID + "; " + _inputs + "verify code=" + verifyCode, AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				
				//Validate
				if(!(cartonID.Length == 14 || cartonID.Length == 24))
					throw new ApplicationException("Invalid cartonID length (" + cartonID.Length.ToString() + "; must be 14 0r 24)...");
				
				//Find the carton
				SortedItem item = this.mOperator.GetSortedItem(cartonID.Length == 24 ? cartonID.Substring(10, 13) : cartonID.Substring(0, 13));
				if(item != null) {
					//Item found
					if(verifyCode == VERIFY_PASS) {
						//Pass: Save carton
						try {
							this.mOperator.StoreSortedItem(item);
							verifyFlag = VERIFY_YES;
							ArgixTrace.WriteLine(new TraceMessage("Carton save succeeded...", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
						}
						catch(Exception ex) {
							verifyFlag = VERIFY_NO;
							aex = new ApplicationException("Carton save failed...-->" + ex.Message, ex);
							ArgixTrace.WriteLine(new TraceMessage(aex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
						}
					}
					else if(verifyCode == VERIFY_FAIL || verifyCode == VERIFY_NOREAD) {	
						//Fail, No Read: Cancel carton
						verifyFlag = VERIFY_NO;
						this.mOperator.CancelSortedItem(item.LabelSeqNumber);
						ArgixTrace.WriteLine(new TraceMessage("Verify flag was fail or no read; carton cancelled...", AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
					}
					else {	
						//Fail, No Read: Cancel carton
						verifyFlag = VERIFY_NO;
						this.mOperator.CancelSortedItem(item.LabelSeqNumber);
						ArgixTrace.WriteLine(new TraceMessage("Verify flag was unknown; carton cancelled...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
					}
				}
				else {
					//Carton not found
					verifyFlag = VERIFY_NO;
					ArgixTrace.WriteLine(new TraceMessage("Carton not found...", AppLib.PRODUCTNAME, LogLevel.Warning, "PandaSvc"));
				}
			}
			catch(ApplicationException ex) { 
				verifyFlag = VERIFY_NO;
				aex = ex;
				ArgixTrace.WriteLine(new TraceMessage(ex.Message, AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
			}
			catch(Exception ex) { 
				verifyFlag = VERIFY_NO;
				aex = new ApplicationException("An unexpected error occurred while processing verify label request-->" + ex.Message + "...", ex);
				ArgixTrace.WriteLine(new TraceMessage("An unexpected error occurred while processing verify label request-->" + ex.Message + "...", AppLib.PRODUCTNAME, LogLevel.Error, "PandaSvc"));
			}
			finally { 
				ArgixTrace.WriteLine(new TraceMessage("VERIFY LABEL RESPONSE: " + cartonID + "; " + verifyFlag, AppLib.PRODUCTNAME, LogLevel.Information, "PandaSvc"));
				if(this.VerifyLabelRequestComplete != null) this.VerifyLabelRequestComplete(this, new PandaVerifyLabelEventArgs(cartonID, verifyFlag, aex)); 
			}
			return verifyFlag;
		}
		#region Local Services: isPriorRequest(), createErrorLabelFormat(), getNowSeconds()
		private PandaPacket isPriorRequest(string clientID, PandaPacket request) {
			//Return prior response if was a prior message
			//Return null if not a prior message
			PandaPacket response=null;
			try {
				//Check if this was a prior message
				PandaPacket priorPacket=null;
				if(this.mLastRequest != null) {
					if(clientID == this.mLastRequest.ClientID && this.mLastRequest.MessageNumber == request.MessageNumber)
						priorPacket = this.mLastRequest;
				}
				//Prior request? return prior response
				if(priorPacket != null) response = priorPacket.Response;
			}
			catch(Exception ex) {  
				throw new ApplicationException("Error occurred while validating message", ex);
			}
			return response;
		}
		private string createErrorLabelFormat(int statusCode, Exception ex) {
			//Exception occurred durin carton processing; create an error
			//outbound label for this carton (this will help identify rejects)
			string labelFormat="";
			labelFormat =	"^XA" + 
							"^FWN^CFD,24^PW812" + 
							"^A0B,45,64^FO103,962^FR^FD<lanePrefix><zoneLane>^FS" + 
							"^A0B,23,16^FO752,360^FR^FDCOPYRIGHT <currentYear>FS" + 
							"^A0B,26,22^FO748,207^FR^FD ARGIX DIRECT ^FS" + 
							"^ADB,18,10^FO748,112^FR^FD88-PAX^FS" + 
							"^FO734,39^FR^GB45,1149,45,B,0^FS" + 
							"^A0B,112,72^FO91,498^FR^FDError!!!^FS" + 
							"^A0B,34,34^FO410,90^FR^FD<messageText_Line1> ^FS" + 
							"^A0B,34,34^FO451,90^FR^FD<messageText_Line2> ^FS" + 
							"^A0B,34,34^FO491,90^FR^FD<messageText_Line3> ^FS" + 
							"^A0B,34,34^FO532,90^FR^FD<messageText_Line4> ^FS" + 
							"^A0B,34,34^FO573,90^FR^FD<messageText_Line5> ^FS" + 
							"^A0B,112,72^FO89,211^FR^FD<statusCode>^FS" + 
							"^ADB,18,10^FO58,285^FR^FDMsg#:^FS" + 
							"^A0B,26,22^FO746,878^FR^FD<currentDate>  <currentTime>^FS" + 
							"^BY4,2.0^FO176,951^FR^B2B,227,N,N,N^FD<lanePrefix><zoneLane>^FS" + 
							"^ADB,18,10^FO207,407^FR^FDCarton#:^FS" + 
							"^A0B,45,46^FO236,78^FR^FD<cartonNumber>^FS" + 
							"^A0B,45,46^FO234,585^FR^FD<sortedItemLabelNumber>^FS" + 
							"^ADB,18,10^FO207,794^FR^FDLSeq#:^FS" + 
							"^A0B,26,22^FO467,1101^FR^FDClient:^FS" + 
							"^A0B,26,22^FO505,1101^FR^FDShipper:^FS" + 
							"^A0B,26,22^FO540,1101^FR^FDPU Date:^FS" + 
							"^A0B,26,22^FO575,1101^FR^FDPU #:^FS" + 
							"^A0B,26,22^FO467,1013^FR^FD<clientNumber>-<clientDivisionNumber>^FS" + 
							"^A0B,26,22^FO505,1033^FR^FD<shipperNumber>^FS" + 
							"^A0B,26,22^FO540,1010^FR^FD<freightPickupDate>^FS" + 
							"^A0B,26,22^FO575,1075^FR^FD<freightPickupNumber>^FS" + 
							"^XZ";
			labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.STATUSCODE, statusCode.ToString());
			string message = ex.Message;
			if(ex.InnerException != null) { 
				message += " -->" + ex.InnerException.Message;
				if(ex.InnerException.InnerException != null) message += " -->" + ex.InnerException.InnerException.Message;
			}
			labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.MESSAGETEXT, message);
			if(message.Length > 0) 
				labelFormat = labelFormat.Replace("<messageText_Line1>", (message.Length < 50 ? message.Substring(0).PadRight(50, ' ') : message.Substring(0, 50).PadRight(50, ' ')));
			else
				labelFormat = labelFormat.Replace("<messageText_Line1>", "");
			if(message.Length > 45) 
				labelFormat = labelFormat.Replace("<messageText_Line2>", (message.Length < 100 ?  message.Substring(50).PadRight(50, ' ') : message.Substring(50, 50).PadRight(50, ' ')));
			else
				labelFormat = labelFormat.Replace("<messageText_Line2>", "");
			if(message.Length > 90) 
				labelFormat = labelFormat.Replace("<messageText_Line3>", (message.Length < 150 ? message.Substring(100).PadRight(50, ' ') : message.Substring(100, 50).PadRight(50, ' ')));
			else
				labelFormat = labelFormat.Replace("<messageText_Line3>", "");
			if(message.Length > 135) 
				labelFormat = labelFormat.Replace("<messageText_Line4>", (message.Length < 200 ? message.Substring(150).PadRight(50, ' ') : message.Substring(150, 50).PadRight(50, ' ')));
			else
				labelFormat = labelFormat.Replace("<messageText_Line4>", "");
			if(message.Length > 180) 
				labelFormat = labelFormat.Replace("<messageText_Line5>", (message.Length < 250 ? message.Substring(200).PadRight(50, ' ') : message.Substring(200, 50).PadRight(50, ' ')));
			else
				labelFormat = labelFormat.Replace("<messageText_Line5>", "");
			labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.LANEPREFIX, "00");
			labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.ZONELANE, "00");
			labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.CURRENTDATE, DateTime.Today.ToString("MM/dd/yyyy"));
			labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.CURRENTTIME, DateTime.Now.ToString("HH:mm"));
			labelFormat = labelFormat.Replace(Tsort.Labels.TokenLibrary.CURRENTYEAR, DateTime.Today.ToString("yyyy"));
			return labelFormat;
		}
		private string getNowSeconds() {
			//Get the number of elasped seconds since midnight; format: 00000
			int sec = ((3600 * DateTime.Now.Hour) + (60 * DateTime.Now.Minute) + DateTime.Now.Second);
			return sec.ToString("00000");
		}
		#endregion
		#region Tray Icon: OnIconMenuClicked(), OnIconDoubleClicked()
		private void OnIconMenuClicked(object sender, System.EventArgs e) {
			//Event handler for menu item clicked
			try {
				MenuItem btn = (MenuItem)sender;
				switch(btn.Index) {
					case MNU_ICON_OPEN: this.mPandaUI.Show(); break;
				}
			}
			catch(Exception) { }
		}
		private void OnIconDoubleClicked(object Sender, EventArgs e) { this.ctxOpen.PerformClick(); }
		#endregion
	}
}

