//	File:	tcpclientproxy.cs
//	Author:	J. Heary
//	Date:	10/30/08
//	Desc:	Proxy for the System.Net.Sockets.TcpClient object that is returned from 
//			the System.Net.Sockets.TcpListener.AcceptTcpClient() method.
//			Notes:
//			1. This class wraps a TcpClient object.
//			2. Maintains information about session state.
//			3. Provides Read() and Write() methods for communicating with the 
//			   connected TCP client.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Argix.Net {
	/// <summary>Proxy for the System.Net.Sockets.TcpClient object that is returned from the System.Net.Sockets.TcpListener.AcceptTcpClient() method.</summary>
	public class TcpClientProxy {
		//Members
		/// <summary>Client read buffer size for every Tsort.Net.TsortTcpClient instance</summary>
		public static int ReadBufferSize=READBUFFERSIZE;
		/// <summary>Client read timeout for every Tsort.Net.TsortTcpClient instance</summary>
		public static int ReadTimeout=READTIMEOUT;
		/// <summary>Client write buffer size for every Tsort.Net.TsortTcpClient instance</summary>
		public static int WriteBufferSize=WRITEBUFFERSIZE;
		/// <summary>Client write timeout for every Tsort.Net.TsortTcpClient instance</summary>
		public static int WriteTimeout=WRITETIMEOUT;
		/// <summary>Disables read/write delay when buffers not full for every Tsort.Net.TsortTcpClient instance</summary>
		public static bool DelayOff=true;
		/// <summary>Client session timeout (seconds)</summary>
		public static int SessionTimeout=SESSIONTIMEOUT;
		
		private string mSessionID="";
		private TcpClient mClient=null;
		private DateTime mLastRead=DateTime.Now;			//For session timouts
		
		//Constants
		/// <summary>Default read buffer size = 1024 bytes</summary>
		public const int READBUFFERSIZE=1024;
		/// <summary>Default read timeout = 1000msec</summary>
		public const int READTIMEOUT=1000;
		/// <summary>Default write buffer size = 1024 bytes</summary>
		public const int WRITEBUFFERSIZE=1024;
		/// <summary>Default write timeout = 1000msec</summary>
		public const int WRITETIMEOUT=1000;
		/// <summary>Default session timeout = 600sec</summary>
		public const int SESSIONTIMEOUT=600;
		
		//Events		
		//Interface
		/// <summary>Initializes a new instance of the Tsort.Net.TcpClientProxy class that wraps a System.Net.Sockets.TcpClient object.</summary>
		/// <param name="client">A System.Net.Sockets.TcpClient object.</param>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public TcpClientProxy(TcpClient client): this(client, ReadBufferSize, ReadTimeout, WriteBufferSize, WriteTimeout, DelayOff) { }
		/// <summary>Initializes a new instance of the Tsort.Net.TcpClientProxy class that wraps a System.Net.Sockets.TcpClient object.</summary>
		/// <param name="client">A System.Net.Sockets.TcpClient object.</param>
		/// <param name="readBufferSize">The size of the receive buffer.</param>
		/// <param name="readTimeout">Tthe amount of time a System.Net.Sockets.TcpClient will wait to receive data once a read operation is initiated.</param>
		/// <param name="writeBufferSize">The size of the send buffer.</param>
		/// <param name="writeTimeout">The amount of time a System.Net.Sockets.TcpClient will wait for a send operation to complete successfully.</param>
		/// <param name="delayOff">A value that disables a delay when send or receive buffers are not full.</param>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public TcpClientProxy(TcpClient client, int readBufferSize, int readTimeout, int writeBufferSize, int writeTimeout, bool delayOff) { 
			//Constructor
			try { 
				//Validate
				if(client == null) throw new ApplicationException("TcpClient object cannot be null.");
				
				this.mSessionID = DateTime.Now.Ticks.ToString();
				this.mClient = client;
				this.mClient.ReceiveBufferSize = readBufferSize;
				this.mClient.ReceiveTimeout = readTimeout;
				this.mClient.SendBufferSize = writeBufferSize;
				this.mClient.SendTimeout = writeTimeout;
				this.mClient.NoDelay = delayOff;
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) {  throw new ApplicationException("An error occurred while creating a new TCP client proxy.", ex); }
		}
		/// <summary>Destructor</summary>
		~TcpClientProxy() { 
			try { 
				//Cleanup resources
				Close();
			}
			catch(Exception) { }
		}
		/// <summary>Gets the session identifier for the client.</summary>
		public string SessionID { get { return this.mSessionID; } }
		/// <summary>Gets the size of the receive buffer.</summary>
		public int ReceiveBufferSize { get { return this.mClient.ReceiveBufferSize; } }
		/// <summary>Gets the amount of time a System.Net.Sockets.TcpClient will wait to receive data once a read operation is initiated.</summary>
		public int ReceiveTimeout { get { return this.mClient.ReceiveTimeout; } }
		/// <summary>Gets the size of the send buffer.</summary>
		public int SendBufferSize { get { return this.mClient.SendBufferSize; } }
		/// <summary>Gets the amount of time a System.Net.Sockets.TcpClient will wait for a send operation to complete successfully.</summary>
		public int SendTimeout { get { return this.mClient.SendTimeout; } }
		/// <summary>Gets a value that disables a delay when send or receive buffers are not full.</summary>
		public bool NoDelay { get { return this.mClient.NoDelay; } }
		/// <summary></summary>
		public bool SessionTimedOut { get { return DateTime.Compare(DateTime.Now, this.mLastRead.AddSeconds(SessionTimeout)) > 0 ; } }
		/// <summary>Read data from the remote host via the socket's nework stream.</summary>
		/// <exception cref="ApplicationException">Thrown if the network stream is null or not readable.</exception>
		public byte[] Read() {
			//Read data from the socket stream
			byte[] data=null;
			try { 
				//Validate connection before reading
				NetworkStream stream = this.mClient.GetStream();
				if(stream == null) 
					throw new ApplicationException("No network stream (null) with remote host.");
				if(stream.CanRead) {					
					while(stream.DataAvailable) {
						//The Read() won't block because data IS available
						//Read until data is unavailable (data may be larger than buffer size)
						byte[] buffer = new byte[this.mClient.ReceiveBufferSize];
						int i = stream.Read(buffer, 0, buffer.Length);
						if(data == null) {
							data = new byte[i];
							Array.Copy(buffer, 0, data, 0, i);
						}
						else {
							byte[] _data = new byte[data.Length];
							data.CopyTo(_data, 0);
							data = new byte[_data.Length + i];
							_data.CopyTo(data, 0);
							Array.Copy(buffer, 0, data, _data.Length, i);
						}
					}
					
					//Update session state
					this.mLastRead = DateTime.Now;
				}
				else
					throw new ApplicationException("The network stream is not readable.");
			}
			catch(ApplicationException ex) { throw ex; }
			catch(System.IO.IOException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("An unexpected error occurred while reading data from the remote host.", ex); }
			return data;
		}
		/// <summary>Writes data to the remote host via the socket's nework stream.</summary>
		/// <exception cref="ApplicationException">Thrown if the network stream is null or not writeable.</exception>
		public void Write(byte[] message) {
			//Write a packet to the socket stream
			try { 
				//Send a message to the client
				NetworkStream stream = this.mClient.GetStream();
				if(stream == null) 
					throw new ApplicationException("No network stream (null) with remote host.");
				if(stream.CanWrite) {
					//Write a response to the current request
					//Q: If the message length is larger than the buffer, is the message truncated?
					stream.Write(message, 0, message.Length);
				}
				else
					throw new ApplicationException("The network stream is not writeable.");
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("An unexpected error occurred while writing data to the remote host.", ex); }
		}
		/// <summary>Closes the TCP connection and releases all resources associated with the System.Net.Sockets.TcpClient object.</summary>
		/// <exception cref="ApplicationException">Thrown if an error occurs when accessing the socket.</exception>
		public void Close() {
			//Close this client
			try { 
				//Cleanup resources
				if(this.mClient.GetStream() != null) this.mClient.GetStream().Close(); 
				this.mClient.Close();
			}
			catch(ObjectDisposedException) { }
			catch(Exception ex) { throw new ApplicationException("An error occurred while closing the client socket connection.", ex); }
		}
	}
}
