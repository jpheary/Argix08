//	File:	argixtcpclient.cs
//	Author:	J. Heary
//	Date:	10/30/08
//	Desc:	Provides a client socket connection for TCP network services.
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
	/// <summary>Provides a client socket connection for TCP network services.</summary>
	public class ArgixTcpClient: TcpClient {
		//Members		
		private IPAddress mIPAddress=IPAddress.Loopback;
		private int mIPPort = DEFAULT_PORT;
		
		/// <summary>Default port number = 10000</summary>
		public const int DEFAULT_PORT = 10000;
		/// <summary>Default read buffer size = 1024 bytes</summary>
		public const int READBUFFERSIZE=1024;
		/// <summary>Default read timeout = 1000msec</summary>
		public const int READTIMEOUT=1000;
		/// <summary>Default write buffer size = 1024 bytes</summary>
		public const int WRITEBUFFERSIZE=1024;
		/// <summary>Default write timeout = 1000msec</summary>
		public const int WRITETIMEOUT=1000;
		
		//Interface
		/// <summary>Initializes a new instance of the Argix.Net.ArgixTcpClient class that listens for incoming connection attempts on the loopback IP address and default port number.</summary>
		public ArgixTcpClient(): this(IPAddress.Loopback, DEFAULT_PORT) { }
		/// <summary>Initializes a new instance of the Argix.Net.ArgixTcpClient class that provides client connections for TCP network services on the specified local IP address and port number.</summary>
		/// <param name="ipAddress">An System.Net.IPAddress that represents the local IP address.</param>
		/// <param name="ipPort">The port on which to listen for incoming connection attempts.</param>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public ArgixTcpClient(IPAddress ipAddress, int ipPort): this(ipAddress, ipPort, READBUFFERSIZE, READTIMEOUT, WRITEBUFFERSIZE, WRITETIMEOUT, true) { }
		/// <summary>Initializes a new instance of the Argix.Net.ArgixTcpClient class that provides client connections for TCP network services on the specified local IP address and port number.</summary>
		/// <param name="ipAddress">An System.Net.IPAddress that represents the local IP address.</param>
		/// <param name="ipPort">The port on which to listen for incoming connection attempts.</param>
		/// <param name="readBufferSize">The size of the receive buffer.</param>
		/// <param name="readTimeout">Tthe amount of time a System.Net.Sockets.TcpClient will wait to receive data once a read operation is initiated.</param>
		/// <param name="writeBufferSize">The size of the send buffer.</param>
		/// <param name="writeTimeout">The amount of time a System.Net.Sockets.TcpClient will wait for a send operation to complete successfully.</param>
		/// <param name="noDelay">Disables read/write delay when buffers not full.</param>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public ArgixTcpClient(IPAddress ipAddress, int ipPort, int readBufferSize, int readTimeout, int writeBufferSize, int writeTimeout, bool noDelay): base() { 
			//Constructor
			try {
				this.mIPAddress = ipAddress;
				this.mIPPort = ipPort;
				base.ReceiveBufferSize = readBufferSize;
				base.ReceiveTimeout = readTimeout;
				base.SendBufferSize = writeBufferSize;
				base.SendTimeout = writeTimeout;
				base.NoDelay = noDelay;
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new SocketClient instance.", ex); }
		}
		/// <summary>Destructor</summary>
		~ArgixTcpClient() { 
			try { 
				base.Close(); 
			}
			catch(Exception ex) { Debug.Write("ArgixTcpClient::~ArgixTcpClient() exception- " + ex.ToString() + "\n"); }
		}
		/// <summary>Gets the IP address</summary>
		public System.Net.IPAddress IPAddress { get { return this.mIPAddress; } }
		/// <summary>Gets the IP port</summary>
		public int IPPort { get { return this.mIPPort; } }
		/// <summary>Gets or sets the client read buffer size.</summary>
		public int ReadBufferSize { get { return base.ReceiveBufferSize; } set { base.ReceiveBufferSize = value; } }
		/// <summary>Gets or sets the client read timeout.</summary>
		public int ReadTimeout { get { return base.ReceiveTimeout; } set { base.ReceiveTimeout = value; } }
		/// <summary>Gets or sets the client write buffer.</summary>
		public int WriteBufferSize { get { return base.SendBufferSize; } set { base.SendBufferSize = value; } }
		/// <summary>Gets or sets the client write timeout.</summary>
		public int WriteTimeout { get { return base.SendTimeout; } set { base.SendTimeout = value; } }
		/// <summary>Gets or sets the disable read/write delay when buffers not full.</summary>
		public bool DelayOff { get { return base.NoDelay; } set { base.NoDelay = value; } }
		/// <summary>Gets the connection status</summary>
		public bool Connected { get { return base.Client.Connected; } }
		/// <summary>Esatblishes a connection to a remote host.</summary>
		/// <exception cref="ApplicationException">Thrown if the connection is not valid.</exception>
		public void Connect() {
			if(!base.Client.Connected) {
				base.Client.Connect(new IPEndPoint(this.mIPAddress, this.mIPPort));
			}
			if(!base.Client.Connected) throw new ApplicationException("No connection to remote server.");
		}
		/// <summary>Closes the System.Net.Sockets.Socket connection and releases all associated resources</summary>
		public void Disconnect() { base.Client.Close(); }
		/// <summary>Writes data to the remote host via the underlyiong socket's nework stream and returns any response.</summary>
		/// <exception cref="ApplicationException">Thrown if the network stream is null or not writeable.</exception>
		public virtual byte[] Write(byte[] message) {
			//Write a request packet to a remote socket server and return a response
			byte[] response=null;
			try { 
				//Validate connection before writing
				NetworkStream stream = base.GetStream();
				if(stream == null) 
					throw new ApplicationException("No network stream (null) with remote host.");
				if(stream.CanWrite) {
					//Write a request and read the response
					//Q: If the message length is larger than the buffer, is the message truncated?
					stream.Write(message, 0, message.Length);
					response = read();
				}
				else
					throw new ApplicationException("The network stream is not writeable.");
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("An unexpected error occurred while writing data to the remote host.", ex); }
			return response;
		}
		private byte[] read() {
			//Read data from a remote socket
			byte[] data=null;
			try { 
				//Validate connection before reading
				NetworkStream stream = base.GetStream();
				if(stream == null) 
					throw new ApplicationException("No network stream (null) with remote host.");
				if(stream.CanRead) {
					//The Read() method blocks until data is available or the ReceiveTimeout occurs
					do {
						//Read until data is unavailable (data may be larger than buffer size)
						byte[] buffer = new byte[base.ReceiveBufferSize];
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
					while(stream.DataAvailable);
				}
				else
					throw new ApplicationException("The network stream is not readable.");
			}
			catch(ApplicationException ex) { throw ex; }
			catch(System.IO.IOException ex) { throw new ApplicationException("Read timeout.", ex); }
			catch(Exception ex) { throw new ApplicationException("An unexpected error occurred while reading data from the remote host.", ex); }
			return data;
		}
	}
}