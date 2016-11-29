//	File:	globals.cs
//	Author:	J. Heary
//	Date:	05/17/07
//	Desc:	Global (library-wide) definitions.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;

namespace Tsort {
	//Global application object
	internal class AppLib {
		//Members
		internal const string EVENTLOGNAME="Argix08";
        internal const string PRODUCTNAME="PandA";
		
		//Interface
		static AppLib() { }
		private AppLib() { }
	}
	
	namespace PandA {
		/// <summary>Defines an event type that returns a Tsort.PandA.PandaPacket with data about a PandA request packet.</summary>
		public delegate void PandaPacketEventHandler(object sender, PandaPacketEventArgs e);
		/// <summary>Provides data for events of type Tsort.PandA.PandaPacketEventHandler.</summary>
		public class PandaPacketEventArgs : EventArgs  {
			private PandaPacket _packet=null;
			/// <summary></summary>
			/// <param name="packet"></param>
			public PandaPacketEventArgs(PandaPacket packet) { this._packet = packet; }
			/// <summary></summary>
			public PandaPacket Packet { get { return this._packet; } set { _packet = value; } }
		}
		
		/// <summary>Defines an event type that returns a Tsort.PandA.PandaDS.CartonTableRow with data about a PandA label data request.</summary>
		public delegate void PandaLabelDataEventHandler(object sender, PandaLabelDataEventArgs e);
		/// <summary>Provides data for events of type Tsort.PandA.PandaLabelDataEventHandler.</summary>
		public class PandaLabelDataEventArgs : EventArgs  {
			private PandaDS.CartonTableRow _carton=null;
			/// <summary></summary>
			/// <param name="carton"></param>
			public PandaLabelDataEventArgs(PandaDS.CartonTableRow carton) { this._carton = carton; }
			/// <summary></summary>
			public PandaDS.CartonTableRow Carton { get { return this._carton; } set { _carton = value; } }
		}
		
		/// <summary>Defines an event type that returns data about a PandA verify label request.</summary>
		public delegate void PandaVerifyLabelEventHandler(object sender, PandaVerifyLabelEventArgs e);
		/// <summary>Provides data for events of type Tsort.PandA.PandaVerifyLabelEventHandler.</summary>
		public class PandaVerifyLabelEventArgs : EventArgs  {
			private string _cartonID="";
			private string _verifyFlag="";
			private Exception _exception=null;
			/// <summary></summary>
			/// <param name="cartonID"></param>
			/// <param name="verifyFlag"></param>
			/// <param name="exception"></param>
			public PandaVerifyLabelEventArgs(string cartonID, string verifyFlag, Exception exception) { this._cartonID = cartonID; this._verifyFlag = verifyFlag; this._exception = exception; }
			/// <summary></summary>
			public string CartonID { get { return this._cartonID; } set { _cartonID = value; } }
			/// <summary></summary>
			public string VerifyFlag { get { return this._verifyFlag; } set { _verifyFlag = value; } }
			/// <summary></summary>
			public Exception Exception { get { return this._exception; } set { _exception = value; } }
		}
	}
}
