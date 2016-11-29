//	File:	argixtcplistener.cs
//	Author:	J. Heary
//	Date:	10/30/08
//	Desc:	Listens for socket connections and socket messages from TCP network clients.
//			Notes:
//			1. Derived from	System.Net.Sockets.TcpListener class
//			2. Maintains a collection of connected Socket clients; clients are removed by timeout
//			3. Uses a seperate thread to listen for client connections
//			4. Uses a seperate thread to poll all connected clients for messages
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
	/// <summary>Listens for socket connections and socket messages from TCP network clients.</summary>
	public class ArgixTcpListener: TcpListener {
		//Members
		private Hashtable mClients=null;
		private Thread mListeningThread=null, mMessageThread=null;
		private bool mListening=false, mReading=false;
		private EventHandler mPendingDelegate=null;
		private ClientEventHandler mConnectedDelegate=null;
		private ErrorEventHandler mConnectErrorDelegate=null;
		private ClientMessageEventHandler mReceivedDelegate=null;
		private ClientEventHandler mTimeoutDelegate=null;
		private ErrorEventHandler mReceiveErrorDelegate=null;
		private EventHandler mListenerHeartbeatDelegate=null, mMessageHeartbeatDelegate=null;
		
		/// <summary>Default port number = 10000</summary>
		public const int DEFAULT_PORT = 10000;
		
		/// <summary>Occurs when the ArgixTcpListener is started, stopped, paused, or continued.</summary>
		public event EventHandler ListeningStateChanged=null;
		/// <summary>Occurs when a pending client connection is present.</summary>
		public event EventHandler ClientConnectionPending=null;
		/// <summary>Occurs when a client connection is made.</summary>
		public event ClientEventHandler ClientConnected=null;
		/// <summary>Occurs when a client message is received.</summary>
		public event ClientMessageEventHandler MessageReceived=null;
		/// <summary>Occurs when a client has been inactive longer than the Argix.Net.TcpClientProxy.SessionTimeout value.</summary>
		public event ClientEventHandler ClientTimeout=null;
		/// <summary>Occurs for any exception that occurrs on the connection and message threads.</summary>
		public event ErrorEventHandler Error=null;
		/// <summary>Occurs when the listener thread beats.</summary>
		public event EventHandler ListenerHeartbeat=null;
		/// <summary>Occurs when the message thread beats.</summary>
		public event EventHandler MessageHeartbeat=null;
		
		//Interface
		/// <summary>Initializes a new instance of the Argix.Net.ArgixTcpListener class that listens for incoming connection attempts on the loopback IP address and default port number.</summary>
		public ArgixTcpListener(): this(IPAddress.Loopback, DEFAULT_PORT) { }
		/// <summary>Initializes a new instance of the Argix.Net.ArgixTcpListener class that listens for incoming connection attempts on the specified local IP address and port number.</summary>
		/// <param name="ipAddress">An System.Net.IPAddress that represents the local IP address.</param>
		/// <param name="ipPort">The port on which to listen for incoming connection attempts.</param>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public ArgixTcpListener(IPAddress ipAddress, int ipPort): base(ipAddress, ipPort) {
			//Constructor
			try {
				this.mClients = new Hashtable();
				this.mPendingDelegate = new EventHandler(OnClientConnectionPending);
				this.mConnectedDelegate = new ClientEventHandler(OnConnected);
				this.mConnectErrorDelegate = new ErrorEventHandler(OnConnectionFailed);
				this.mReceivedDelegate = new ClientMessageEventHandler(OnClientMessageReceived);
				this.mTimeoutDelegate = new ClientEventHandler(OnClientTimeout);
				this.mReceiveErrorDelegate = new ErrorEventHandler(OnClientMessageError);
				this.mListenerHeartbeatDelegate = new EventHandler(OnListenerHeartbeat);
				this.mMessageHeartbeatDelegate = new EventHandler(OnMessageHeartbeat);
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating a new ArgixTcpListener instance.", ex); }
		}
		/// <summary>Destructor</summary>
		~ArgixTcpListener() { }
		/// <summary>Gets a value that indicates whether ArgixTcpListener is actively listening for client connections.</summary>
		public bool Listening { get { return this.mListening; } }
		/// <summary>Determines if there are pending connection requests. </summary>
		public bool ConnectionPending { get { return (this.mListening?base.Pending():false); } }
		/// <summary>Gets a value indicating whether a System.Net.Sockets.Socket is connected to a remote host.</summary>
		public bool Connected { get { base.Server.Poll(500, SelectMode.SelectWrite); return base.Server.Connected; } }
		/// <summary>Gets the local endpoint. </summary>
		public EndPoint LocalEndPoint { get { return base.Server.LocalEndPoint; } }
		/// <summary>Gets the remote endpoint.</summary>
		public EndPoint RemoteEndPoint { get { return base.Server.RemoteEndPoint; } }
		/// <summary> Gets the type of the underlying System.Net.Sockets.Socket. </summary>
		public SocketType SocketType { get { return base.Server.SocketType; } }
		/// <summary>Gets the address family of the underlying System.Net.Sockets.Socket.</summary>
		public AddressFamily SocketAddressFamily { get { return base.Server.AddressFamily; } }
		/// <summary> </summary>
		public ProtocolType SocketProtocolType { get { return base.Server.ProtocolType; } }
		/// <summary>Gets a value that indicates whether the underlying System.Net.Sockets.Socket is in blocking mode.</summary>
		public bool Blocking { get { return base.Server.Blocking; } }
		/// <summary>Gets the amount of data that has been received from the network and is available to be read.</summary>
		public int Available { get { return base.Server.Available; } }
		#region Server Services: StartServer(), PauseServer(), ContinueServer(), StopServer()
		/// <summary>Starts listening for incoming client connection requests; starts listening for client messages.</summary>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public void StartServer() {
			try { 
				if(!this.mListening) {
					base.Start();
					this.mListening = true;
					this.mListeningThread = new Thread(new ThreadStart(listenForConnections));
					this.mListeningThread.Name = "ConnectionListener";
					this.mListeningThread.IsBackground = true;
					this.mListeningThread.Priority = ThreadPriority.Lowest;
					this.mListeningThread.Start();
				}
				if(!this.mReading) {
					this.mReading = true;
					this.mMessageThread = new Thread(new ThreadStart(listenForMessages));
					this.mMessageThread.Name = "MessageListener";
					this.mMessageThread.IsBackground = true;
					this.mMessageThread.Start();
				}
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while starting the listener.", ex); }
			finally { if(this.ListeningStateChanged != null) this.ListeningStateChanged(this, EventArgs.Empty); }
		}
		/// <summary>Pauses the connection and message listeners.</summary>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public void PauseServer() {
			try { 
				this.mReading = false;
				this.mMessageThread.Suspend();
				this.mListening = false;
				this.mListeningThread.Suspend();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while pausing the listener.", ex); }
			finally { if(this.ListeningStateChanged != null) this.ListeningStateChanged(this, EventArgs.Empty); }
		}
		/// <summary>Resumes the connection and message listeners.</summary>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public void ContinueServer() {
			try { 
				this.mListening = true;
				this.mListeningThread.Resume();
				this.mReading = true;
				this.mMessageThread.Resume();
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while resuming (continuing) the listener.", ex); }
			finally { if(this.ListeningStateChanged != null) this.ListeningStateChanged(this, EventArgs.Empty); }
		}
		/// <summary>Stops the connection and message listeners.</summary>
		/// <exception cref="ApplicationException">Thrown for any unexpected exception.</exception>
		public void StopServer() {
			try { 
				try {
					this.mReading = false;
					Thread.Sleep(2000);
					if(this.mMessageThread.IsAlive) {
						this.mMessageThread.Abort();
						this.mMessageThread.Join(1000);
					}
					this.mMessageThread = null;
				}
				catch(ThreadStateException) { this.mMessageThread.Resume(); }
				catch(Exception) { }
				
				try {
					this.mListening = false;
					Thread.Sleep(2000);
					if(this.mListeningThread.IsAlive) {
						this.mListeningThread.Abort();
						this.mListeningThread.Join(1000);
					}
					this.mListeningThread = null;
				}
				catch(ThreadStateException) { this.mListeningThread.Resume();}
				catch(Exception) { }
				
				try {
					//Clear out all client connections
					foreach(DictionaryEntry entry in this.mClients) {
						TcpClientProxy client = (TcpClientProxy)entry.Value;
						client.Close();
					}
				}
				catch(Exception) { }
				this.mClients.Clear();
				
				//Stop the base listener
				base.Stop();
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("Unexpected error while stopping (closing) the listener.", ex); }
			finally { if(this.ListeningStateChanged != null) this.ListeningStateChanged(this, EventArgs.Empty); }
		}
		
		#endregion
		#region Server Services: listenForConnections(), OnClientConnectionPending(), OnConnected(), OnConnectionFailed()
		private void listenForConnections() {
			//Listen for client connections
			while(this.mListening) {
				try { if(this.mListeningThread.IsAlive) Thread.Sleep(3000); } catch {}
				try { 
					//Check for pending connection requests
					if(base.Pending()) {
						//Create a client to handle server requests
						this.mPendingDelegate.BeginInvoke(null, EventArgs.Empty, null, null);
						TcpClientProxy client = new TcpClientProxy(base.AcceptTcpClient());
						this.mConnectedDelegate.BeginInvoke(null, new ClientEventArgs(client), null, null);
					}
					this.mListenerHeartbeatDelegate.BeginInvoke(null, EventArgs.Empty, null, null);
				}
				catch(Exception ex) { this.mConnectErrorDelegate.BeginInvoke(null, new ErrorEventArgs(ex), null, null); }
			}
		}
		private void OnClientConnectionPending(object sender, EventArgs e) {
			//Raise an event that there is a client connection pending
			try { if(this.ClientConnectionPending != null) this.ClientConnectionPending(this, EventArgs.Empty); } catch(Exception) { }
		}
		private void OnConnected(object sender, ClientEventArgs e) {
			//Raise an event that a client is connected
			try { 
				//Add the new client to the collection of attached clients
				TcpClientProxy client = e.Client;
				lock(this.mClients) {
					this.mClients.Add(client.SessionID, client);
				}
			} 
			catch(Exception ex) { if(this.Error != null) this.Error(this, new ErrorEventArgs(ex)); }
			finally { if(this.ClientConnected != null) this.ClientConnected(this, e); }
		}
		private void OnConnectionFailed(object sender, ErrorEventArgs e) {
			//Raise an event that a client connection failed
			try { if(this.Error != null) this.Error(this, e); } catch(Exception) { }
		}
		private void OnListenerHeartbeat(object sender, EventArgs e) {
			//Raise an event for the listener heartbeat
			try { if(this.ListenerHeartbeat != null) this.ListenerHeartbeat(this, EventArgs.Empty); } catch(Exception) { }
		}
		#endregion
		#region Client Services: listenForMessages(), OnClientMessageReceived(), OnClientTimeout(), OnClientMessageError()
		private void listenForMessages() {
			//Listen for client messages
			while(this.mReading) {
				try { if(this.mMessageThread.IsAlive) Thread.Sleep(TcpClientProxy.ReadTimeout); } catch {}
				try { 	
					//Request a read for each connected clients
					lock(this.mClients) {
						foreach(DictionaryEntry entry in this.mClients) {
							//Validate client is still active (no session timeout)
							TcpClientProxy client = (TcpClientProxy)entry.Value;
							if(client.SessionTimedOut) {
								//Client timed-out; dispose the object
								this.mTimeoutDelegate.BeginInvoke(null, new ClientEventArgs(client), null, null);
							}
							else {
								//Check for a message from this client
								byte[] message = client.Read();
								if(message != null) 
									this.mReceivedDelegate.BeginInvoke(null, new ClientMessageEventArgs(client, message), null, null);
							}
						}
					}
					this.mMessageHeartbeatDelegate.BeginInvoke(this, EventArgs.Empty, null, null);
				}
				catch(Exception ex) { this.mReceiveErrorDelegate.BeginInvoke(this, new ErrorEventArgs(new ApplicationException("ArgixTcpListener::listenForMessages() exception.", ex)), null, null); }
			}
		}
		private void OnClientMessageReceived(object sender, ClientMessageEventArgs e) {
			//Event handler for message posted from socket client
			try {
				//Signal message processing required
				if(this.MessageReceived != null) this.MessageReceived(this, e);
			}
			catch(Exception ex) { if(this.Error != null) this.Error(this, new ErrorEventArgs(ex)); }
		}
		private void OnClientTimeout(object sender, ClientEventArgs e) {
			//Raise an event that a client has timed out
			try { 
				//Notify clients for last chance to use client object before it is closed
				if(this.ClientTimeout != null) this.ClientTimeout(this, e);
				lock(this.mClients) {
					this.mClients.Remove(e.Client.SessionID);
				}
				e.Client.Close();
			} 
			catch(Exception ex) { if(this.Error != null) this.Error(this, new ErrorEventArgs(ex)); }
		}
		private void OnClientMessageError(object sender, ErrorEventArgs e) {
			//Raise an event that a client connection failed
			try { if(this.Error != null) this.Error(this, e); } catch(Exception) { }
		}
		private void OnMessageHeartbeat(object sender, EventArgs e) {
			//Raise an event for the listener heartbeat
			try { if(this.MessageHeartbeat != null) this.MessageHeartbeat(this, EventArgs.Empty); } catch(Exception) { }
		}
		#endregion
	}
}
