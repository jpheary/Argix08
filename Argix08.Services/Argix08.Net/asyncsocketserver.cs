//	File:	asyncsocketserver.cs
//	Author:	J. Heary
//	Date:	10/30/08
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Argix.Net {
	//State object for reading client data asynchronously
	internal class StateObject {
		//Members
		public Socket ClientSocket=null;
		public byte[] Buffer=new byte[BUFFERSIZE];
		public StringBuilder Message=new StringBuilder();
		public const int BUFFERSIZE = 1024;
	}

	//Socker Server
	internal class AsyncSocketServer {
		//Members
		public static IPAddress IPAddress=IPAddress.Loopback;
		public static int IPPort = 10000;
		public static ManualResetEvent evDone=new ManualResetEvent(false);
	
		//Interface
		static AsyncSocketServer() { }
		private AsyncSocketServer() { }
		public static void StartListening() {
			//Data buffer for incoming data
			Socket listener=null;
			byte[] bytes = new Byte[1024];
			try {
				//Create a TCP/IP socket
				listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
			
				//Bind the socket to the local endpoint and listen for incoming connections.
				IPEndPoint localEndPoint = new IPEndPoint(AsyncSocketServer.IPAddress, AsyncSocketServer.IPPort);
				listener.Bind(localEndPoint);
				listener.Listen(100);
				while(true) {
					//Set the event to nonsignaled state
					evDone.Reset();
				
					//Start an asynchronous socket to listen for connections
					Debug.Write("Waiting for a connection...\n");
					listener.BeginAccept( new AsyncCallback(acceptConnection), listener );
				
					//Wait until a connection is made before continuing
					evDone.WaitOne();
				}
			} 
			catch (Exception e) { Debug.Write(e.ToString() + "\n"); }
		}
		private static void acceptConnection(IAsyncResult ar) {
			//Signal the main thread to continue
			evDone.Set();
		
			//Get the socket that handles the client request
			Socket listener = (Socket)ar.AsyncState;
			Socket client = listener.EndAccept(ar);
		
			//Create the client state object
			StateObject state = new StateObject();
			state.ClientSocket = client;
			client.BeginReceive( state.Buffer, 0, StateObject.BUFFERSIZE, 0, new AsyncCallback(listenForMessages), state);
		}
		private static void listenForMessages(IAsyncResult ar) {
			//
			String message = String.Empty;
        
			//Retrieve the state object and the client socket from the asynchronous state object
			StateObject state = (StateObject) ar.AsyncState;
			Socket client = state.ClientSocket;
		
			//Read data from the client socket. 
			int bytesRead = client.EndReceive(ar);
			if (bytesRead > 0) {
				//There  might be more data, so store the data received so far
				state.Message.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));

				//Check for end-of-file tag; if it is not there, read more data
				message = state.Message.ToString();
				if(message.IndexOf("ETX") > -1) {
					//All the data has been read from the client
					Debug.Write("Read " + message.Length.ToString() + " bytes from client socket. \n Message: " + message + "\n");
					//TODO: Process message; then return a response
					string response=message;	//Echo
					send(client, response);
				} 
				else {
					//Not all data received; get more
					client.BeginReceive(state.Buffer, 0, StateObject.BUFFERSIZE, 0, new AsyncCallback(listenForMessages), state);
				}
			}
		}
		private static void send(Socket client, string response) {
			//
			try {
				//Convert the string data to byte data using ASCII encoding
				byte[] bytes = Encoding.ASCII.GetBytes(response);
		
				//Begin sending the data to the remote device
				client.BeginSend(bytes, 0, bytes.Length, 0, new AsyncCallback(sendCallback), client);
			} 
			catch (Exception e) { Debug.Write(e.ToString() + "\n"); }
		}
		private static void sendCallback(IAsyncResult ar) {
			//
			try {
				//Retrieve the socket from the state object and 
				//complete sending the data to the remote device
				Socket client = (Socket)ar.AsyncState;
				int bytesSent = client.EndSend(ar);
				Debug.Write("Sent " + bytesSent.ToString() + " bytes to client.\n");
				client.Shutdown(SocketShutdown.Both);
				client.Close();
			} 
			catch (Exception e) { Debug.Write(e.ToString() + "\n"); }
		}
	}
}