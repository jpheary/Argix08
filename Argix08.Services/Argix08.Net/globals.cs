//	File:	globals.cs
//	Author:	J. Heary
//	Date:	10/30/08
//	Desc:	Defines library-wide constructs.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;

namespace Argix {
	//
	namespace Net {
		//Data structurs, enumerators
		
		//Event support
		/// <summary>Defines an event type that returns a Tsort.Net.TcpClientProxy for communications with a remote socket client.</summary>
		public delegate void ClientEventHandler(object sender, ClientEventArgs e);
		/// <summary>Provides data for events of type Tsort.Net.ClientEventHandler.</summary>
		public class ClientEventArgs : EventArgs {
			private TcpClientProxy _client=null;
			/// <summary></summary>
			/// <param name="client"></param>
			public ClientEventArgs(TcpClientProxy client) { this._client = client; }
			/// <summary></summary>
			public TcpClientProxy Client { get { return _client; } set { _client = value; } }
		}
	
		/// <summary>Defines an event type that returns a message received from a Tsort.Net.TcpClientProxy.</summary>
		public delegate void ClientMessageEventHandler(object sender, ClientMessageEventArgs e);
		/// <summary>Provides data for events of type Tsort.Net.ClientMessageEventHandler.</summary>
		public class ClientMessageEventArgs : EventArgs {
			private TcpClientProxy _client=null;
			private byte[] _message=null;
			/// <summary></summary>
			/// <param name="client"></param>
			/// <param name="message"></param>
			public ClientMessageEventArgs(TcpClientProxy client, byte[] message) { this._client = client; this._message = message; }
			/// <summary></summary>
			public TcpClientProxy Client { get { return _client; } set { _client = value; } }
			/// <summary></summary>
			public byte[] Message { get { return _message; } set { _message = value; } }
		}	
		/// <summary>Defines an event type that returns a System.Exception for error reporting.</summary>
		public delegate void ErrorEventHandler(object sender, ErrorEventArgs e);
		/// <summary>Provides data for events of type Tsort.Net.ErrorEventHandler.</summary>
		public class ErrorEventArgs : EventArgs {
			private Exception _ex=null;
			/// <summary></summary>
			/// <param name="ex"></param>
			public ErrorEventArgs(Exception ex) { this._ex = ex; }
			/// <summary></summary>
			public Exception Exception { get { return _ex; } set { _ex = value; } }
		}
	}
}
