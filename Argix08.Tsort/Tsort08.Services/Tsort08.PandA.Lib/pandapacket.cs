//	File:	pandapacket.cs
//	Author:	J. Heary
//	Date:	10/18/06
//	Desc:	Defines a communications packet containing a message that conforms
//			to the PandA Message Protocol.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Reflection;
using System.Diagnostics;
using System.Data;
using System.Threading;
using System.Text;

namespace Tsort.PandA {	
	/// <summary>Defines a communications packet containing a message that conforms to the PandA Message Protocol.</summary>
	public class PandaPacket {
		//Members
		/// <summary>Flag to validate packet message length.</summary>
		public static bool ValidateMessageLength=true;
		/// <summary>Expected length of the left justified, blank padded carton identifier field.</summary>
		public static int CartonIDLength=50;
		/// <summary>Maximum length of the left justified, blank padded label data field.</summary>
		public static int LabelDataLength=2000;
		
		private byte[] mPacket=null;
		private string mMessage="";
		private DateTime mReceived=DateTime.Now;
		private int mMessageLength=0;
		private int mMessageCode=0;
		private int mMessageNumber=0;
		private int mOriginalMessageCode=0;
		private int mMessageFlags=0;
		private int mRecordIterator=1;
		private string mMessageBody="";
		private bool mValid=false;
		private Exception mException=null;
		private float mProcessingTime=0.0f;
		private string mClientID="";
		private PandaPacket mResponse=null;
		
		#region Constants
		/// <summary>ASCII SOH constant = 0x01.</summary>
		public const byte SOH = 0x01;
		/// <summary>ASCII STX  constant = 0x02.</summary>
		public const byte STX = 0x02;
		/// <summary>ASCII ETX  constant = 0x03.</summary>
		public const byte ETX = 0x03;
		/// <summary>ASCII EOT  constant = 0x04.</summary>
		public const byte EOT = 0x04;
		/// <summary>ASCII SUB  constant = 0x1a.</summary>
		public const byte SUB = 0x1a;
		
		private const int LEN_MIN = 23;
		private const int START_MSG = 1, LEN_MSG = 5;
		private const int START_CODE = 6, LEN_CODE = 4;
		private const int START_NUM = 10, LEN_NUM = 4;
		private const int START_ORIGCODE = 14;
		private const int START_FLAGS = 18, LEN_FLAGS = 1;
		private const int START_ITERATOR = 19, LEN_ITERATOR = 3;
		private const int START_BODY = 22;
		#endregion
		//Events		
		//Interface
		/// <summary>Creates a PandaPacket request with encoded PandA Message information.</summary>
		/// <param name="clientID">A unique identifier for the socket client; not required.</param>
		/// <param name="messageCode">Identifies the PandA request/response message type.</param>
		/// <param name="flag">Message status (0=message OK; 1=message error).</param>
		/// <param name="iterator">Count of records in the body (always 1).</param>
		/// <param name="messageBody">Message data.</param>
		/// <param name="messageNumber">Unique message identifier from 0001 - 9999.</param>
		/// <returns>An instance of Tsort.PandA.PandaPacket.</returns>
		public static PandaPacket Encode(string clientID, int messageCode, int flag, int iterator, string messageBody, int messageNumber) { return Encode(clientID, messageCode, flag, iterator, messageBody, messageNumber, null); }
		/// <summary>Creates a PandaPacket response to the specified PandaPacket request.</summary>
		/// <param name="clientID">A unique identifier for the socket client; not required.</param>
		/// <param name="messageCode">Identifies the PandA request/response message type.</param>
		/// <param name="flag">Message status (0=message OK; 1=message error).</param>
		/// <param name="iterator">Count of records in the body (always 1).</param>
		/// <param name="messageBody">Message data.</param>
		/// <param name="messageNumber">Unique message identifier from 0001 - 9999.</param>
		/// <param name="request">A reference to the original Tsort.PandA.PandaPacket request.</param>
		/// <returns>An instance of Tsort.PandA.PandaPacket.</returns>
		public static PandaPacket Encode(string clientID, int messageCode, int flag, int iterator, string messageBody, int messageNumber, PandaPacket request) {
			//Encode a PandA packet
			PandaPacket packet=null;
			try {
				//Form the message packet
				packet = new PandaPacket();
				packet.mClientID = clientID;
				packet.mReceived = DateTime.Now;
				packet.mMessageCode = messageCode;
				packet.mMessageNumber = messageNumber;
				packet.mOriginalMessageCode = (request != null) ? request.MessageCode : 0;
				packet.mMessageFlags = flag;
				packet.mRecordIterator = iterator;
				packet.mMessageBody = messageBody;
				if(request != null) request.Response = packet;
			
				//Determine message length and dimension message packet
				packet.mMessageLength = LEN_MIN + packet.mMessageBody.Length;
				packet.mPacket = new byte[packet.mMessageLength];
				byte[] bytes = makePacketBytes(packet);
				Array.Copy(bytes, 0 , packet.mPacket, 0, bytes.Length);
				packet.mMessage = new ASCIIEncoding().GetString(packet.mPacket);
				packet.mValid = true;
			}
			catch(ApplicationException ex) { packet.mException = ex; packet.mValid = false; }
			catch(Exception ex) { packet.mException = new ApplicationException("Message packet encoding failed.", ex); packet.mValid = false; }
			return packet;
		}
		/// <summary>Decodes a PandA Message packet into a PandaPacket object.</summary>
		/// <param name="clientID">A unique identifier for the socket client; not required.</param>
		/// <param name="message">A byte array containing a message that adheres to the PandA Message Protocol.</param>
		/// <returns>An instance of Tsort.PandA.PandaPacket.</returns>
		public static PandaPacket Decode(string clientID, byte[] message) {
			//Decode a PandA message packet
			//NOTE: This method never throws an exception; the exception is in the PandaPacket
			PandaPacket packet=null;
			Encoding oEncoding=new ASCIIEncoding();
			try {
				//Retain packet
				packet = new PandaPacket();
				packet.mClientID = clientID;
				packet.mReceived = DateTime.Now;
				packet.mPacket = new byte[message.Length];
				Array.Copy(message, 0, packet.mPacket, 0, message.Length);
				
				//Validate packet integrity
				if(packet.mPacket.Length < LEN_MIN) 
					throw new ApplicationException("Invalid message length; length less than minimum.");
				if((packet.mPacket[0] != STX) || (packet.mPacket[packet.mPacket.Length-1] != ETX)) 
					throw new ApplicationException("The message either did not begin with STX or did not end with ETX");
				
				//Extract message parameters
				packet.mMessage = oEncoding.GetString(packet.mPacket);
				packet.mMessageLength = Convert.ToInt32(packet.mMessage.Substring(START_MSG, LEN_MSG));
				packet.mMessageCode = Convert.ToInt32(packet.mMessage.Substring(START_CODE, LEN_CODE));
				packet.mMessageNumber = Convert.ToInt32(packet.mMessage.Substring(START_NUM, LEN_NUM));
				packet.mOriginalMessageCode = Convert.ToInt32(packet.mMessage.Substring(START_ORIGCODE, LEN_CODE));
				packet.mMessageFlags = Convert.ToInt32(packet.mMessage.Substring(START_FLAGS, LEN_FLAGS));
				packet.mRecordIterator = Convert.ToInt32(packet.mMessage.Substring(START_ITERATOR, LEN_ITERATOR));
				packet.mMessageBody = packet.mMessage.Substring(START_BODY, packet.mMessageLength - START_BODY - 1);
				
				//Validate packet integrity
				if(ValidateMessageLength && packet.mPacket.Length != packet.mMessageLength) 
					throw new ApplicationException("Invalid message length; length does not match header length member.");
				packet.mValid = true;
			}
			catch(ApplicationException ex) { packet.mException = ex; packet.mValid = false; }
			catch(Exception ex) { packet.mException = new ApplicationException("Message packet decoding failed.", ex); packet.mValid = false; }
			return packet;
		}
		/// <summary>Split multiple messages into seperate array elements.</summary>
		/// <param name="bytes"></param>
		/// <returns></returns>
		public static object[] Split(byte[] bytes) {
			//Split multiple messages into seperate array elements
			object[] messages = new object[6]{null,null,null,null,null,null};
			try {
				//Set default return (in case no STX, ETX, whatever)
				messages[0] = bytes;
				int index=0;
				int j=-1, k=-1;
				for(int i=0; i<bytes.Length; i++) {
					//Find a packet by start and end characters
					if(bytes[i] == STX) j = i;
					if(bytes[i] == ETX) k = i;
					if(j > -1 && k > -1) {
						//Packet found- parse packet
						byte[] _bytes = new byte[k-j+1];
						Array.Copy(bytes, j, _bytes, 0, k-j+1);
						messages[index++] = _bytes;
						j = k = -1;		//Reset for another packet search
					}
				}
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while splitting multi-packet message.", ex); }
			return messages;
		}
		internal static string FormatLabelDataMessageBody(string cartonID, int statusCode, string labelData) {
			//Create a messsage body for a label data response
			string body="";
			body =	cartonID.PadRight(CartonIDLength, ' ') + 
					statusCode.ToString().PadLeft(2, '0') + 
					labelData.PadRight(LabelDataLength, ' ');
			return body;
		}
		/// <summary>Creates a new instance of the Tsort.PandA.PandaPacket class with default data values.</summary>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		public PandaPacket(): this(null) { }
		/// <summary>Creates a new instance of the Tsort.PandA.PandaPacket class.</summary>
		/// <param name="packet">PandA Messsage data for encoding.</param>
		/// <exceptions cref="ApplicationException">Thrown for unexpected errors.</exceptions>
		public PandaPacket(PandaDS.PacketTableRow packet) {
			//Constructor
			try {
				if(packet != null) {
					this.mReceived = packet.Received;
					this.mMessageLength = packet.MessageLength;
					this.mMessageCode = packet.MessageCode;
					this.mMessageNumber = packet.MessageNumber;
					this.mOriginalMessageCode = packet.OriginalMessageCode;
					this.mMessageFlags = packet.MessageFlags;
					this.mRecordIterator = packet.RecordIterator;
					this.mMessageBody = packet.MessageBody;
					this.mValid = packet.Valid;
					this.mException = new Exception(packet.Exception);
					this.mProcessingTime = packet.ProcessingTime;
					this.mClientID = packet.ClientID;
					this.mPacket = new byte[this.mMessageLength];
					byte[] bytes = makePacketBytes(this);
					Array.Copy(bytes, 0 , this.mPacket, 0, bytes.Length);
					this.mMessage = new ASCIIEncoding().GetString(this.mPacket);
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new PandaPacket instance.", ex); }
		}
		/// <summary>Destructor</summary>
		~PandaPacket() { }
		#region Accessors/Modifiers: [Members]..., ToDataSet()
		/// <summary>Gets the PandA Message as a byte array.</summary>
		public byte[] Bytes { get { return this.mPacket; } }
		/// <summary>Gets the PandA Message as a string.</summary>
		public string Message { get { return this.mMessage; } }
		/// <summary>Gets the  packet creation date/time.</summary>
		public DateTime Received { get { return this.mReceived; } }
		/// <summary>Gets the  message length, including the start (STX) and end (ETX) characters, in bytes</summary>
		public int MessageLength { get { return this.mMessageLength; } }
		/// <summary>Gets a code describing the type of service (i.e. get label data) requested or responded to.</summary>
		public int MessageCode { get { return this.mMessageCode; } }
		/// <summary>Gets a unique identifier (i.e. 0001 - 9999) for the message.</summary>
		public int MessageNumber { get { return this.mMessageNumber; } }
		/// <summary>Gets the message code from the originating message request.</summary>
		public int OriginalMessageCode { get { return this.mOriginalMessageCode; } }
		/// <summary>Gets the message status (0=message OK; 1=message error).</summary>
		public int MessageFlags { get { return this.mMessageFlags; } }
		/// <summary>Gets the count of records in the body (always 1).</summary>
		public int RecordIterator { get { return this.mRecordIterator; } }
		/// <summary>Gets the message body.</summary>
		public string MessageBody { get { return this.mMessageBody; } }
		/// <summary>Gets the state of the PandA message (true- no exceptions; false- exception ocurred).</summary>
		public bool Valid { get { return this.mValid; } }
		/// <summary>Gets the exception for an invalid message.</summary>
		public Exception Exception { get { return this.mException; } }
		/// <summary>Gets the time (sec) for processing this message.</summary>
		public float ProcessingTime { get { return this.mProcessingTime; } set { this.mProcessingTime = value; } }
		/// <summary>Gets the unique identifier for the owner of this object (not required).</summary>
		public string ClientID { get { return this.mClientID; } }
		/// <summary>Gets and sets a response to associate with this as a request (not required).</summary>
		public PandaPacket Response { get { return this.mResponse; } set { this.mResponse = value; } }
		/// <summary>Returns this object as a PandaDS dataset.</summary>
		public DataSet ToDataSet() { 
			PandaDS ds=null;
			try { 
				ds = new PandaDS();
				ds.PacketTable.AddPacketTableRow(	this.mReceived,
													this.mMessageLength,
													this.mMessageCode,
													this.mMessageNumber,
													this.mOriginalMessageCode,
													this.mMessageFlags,
													this.mRecordIterator,
													this.mMessageBody,
													this.mValid,
													(this.mException!=null?this.mException.ToString():""),
													this.mProcessingTime,
													this.mClientID);
				ds.AcceptChanges();
			}
			catch(Exception ex) { Debug.Write(ex.ToString() + "\n");}
			return ds; 
		}
		#endregion
		private static byte[] makePacketBytes(PandaPacket packet) {
			//Create the byte array for the specified packet
			byte[] bytes = new byte[packet.mMessageLength];
			bytes[0] = STX;
			
			string msgLen = packet.mMessageLength.ToString().PadLeft(5, '0');
			bytes[1] = (byte)Char.Parse(msgLen.Substring(0, 1));
			bytes[2] = (byte)Char.Parse(msgLen.Substring(1, 1));
			bytes[3] = (byte)Char.Parse(msgLen.Substring(2, 1));
			bytes[4] = (byte)Char.Parse(msgLen.Substring(3, 1));
			bytes[5] = (byte)Char.Parse(msgLen.Substring(4, 1));
			
			string msgCode = packet.mMessageCode.ToString().PadLeft(4, '0');
			bytes[6] = (byte)Char.Parse(msgCode.Substring(0, 1));
			bytes[7] = (byte)Char.Parse(msgCode.Substring(1, 1));
			bytes[8] = (byte)Char.Parse(msgCode.Substring(2, 1));
			bytes[9] = (byte)Char.Parse(msgCode.Substring(3, 1));
			
			string msgNum = packet.mMessageNumber.ToString().PadLeft(4, '0');
			bytes[10] = (byte)Char.Parse(msgNum.Substring(0, 1));
			bytes[11] = (byte)Char.Parse(msgNum.Substring(1, 1));
			bytes[12] = (byte)Char.Parse(msgNum.Substring(2, 1));
			bytes[13] = (byte)Char.Parse(msgNum.Substring(3, 1));
			
			string msgOrigCode = packet.mOriginalMessageCode.ToString().PadLeft(4, '0');
			bytes[14] = (byte)Char.Parse(msgOrigCode.Substring(0, 1));
			bytes[15] = (byte)Char.Parse(msgOrigCode.Substring(1, 1));
			bytes[16] = (byte)Char.Parse(msgOrigCode.Substring(2, 1));
			bytes[17] = (byte)Char.Parse(msgOrigCode.Substring(3, 1));
			
			bytes[18] = (byte)Char.Parse(packet.mMessageFlags.ToString());
			
			string msgIterator = packet.mRecordIterator.ToString().PadLeft(3, '0');
			bytes[19] = (byte)Char.Parse(msgIterator.Substring(0, 1));
			bytes[20] = (byte)Char.Parse(msgIterator.Substring(1, 1));
			bytes[21] = (byte)Char.Parse(msgIterator.Substring(2, 1));
			
			for(int i=0; i<packet.mMessageBody.Length; i++) 
				bytes[i+LEN_MIN-1] = (byte)Char.Parse(packet.mMessageBody.Substring(i, 1));
			
			bytes[packet.mMessageLength-1] = ETX;
			return bytes;
		}
	}
}