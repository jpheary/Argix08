//	File:	asynctsorttcpclient.cs
//	Author:	J. Heary
//	Date:	10/30/08
//	Desc:	.
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
	//
	internal class Message {
		//Members
		public NetworkStream ServerStream=null;
		public byte[] Buffer=new byte[BUFFERSIZE];
		public const int BUFFERSIZE = 5120;
	}
	
	//
	internal class AsyncArgixTcpClient: ArgixTcpClient {
		//Members		
		public event EventHandler ResponseReceived=null;
		
		//Interface
		public AsyncArgixTcpClient(): base() { 
			//Constructor
			try {
			}
			catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new AsyncArgixTcpClient instance.", ex); }
		}
		~AsyncArgixTcpClient() { 
			try { 
			}
			catch(Exception ex) { Debug.Write("AsyncArgixTcpClient::~AsyncArgixTcpClient() exception- " + ex.ToString() + "\n"); }
		}
		public override byte[] Write(byte[] bytes) {
			//Begin an asynchronous write operation
			try { 
				//Validate connection before writing
				NetworkStream stream = base.GetStream();
				if(stream == null) 
					throw new ApplicationException("No data stream to remote server.");
				
				//Begin sending the data to the remote server
				if(stream.CanWrite) {
					//Write a request and read the response
					stream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(writeCallback), stream);
				}
				else
					throw new ApplicationException("The network stream is not readable.");
			}
			catch(ApplicationException ex) { throw ex; }
			catch(Exception ex) { throw new ApplicationException("AsyncArgixTcpClient::Write() exception.", ex); }
			return null;
		}
		private void writeCallback(IAsyncResult ar) {
			//
			try {
				//Retrieve the stream from the state object and 
				//complete sending the data to the remote server
				NetworkStream stream = (NetworkStream)ar.AsyncState;
				stream.EndWrite(ar);
				read();
			} 
			catch (Exception e) { Debug.Write(e.ToString() + "\n"); }
		}
		private void read() {
			//
			try { 
				//Validate connection before reading
				NetworkStream stream = base.GetStream();
				if(stream == null) 
					throw new ApplicationException("The network stream was not returned from the socket.");
				
				//Read the data from the stream
				if(stream.CanRead) {
					//The Read() method blocks (async???) until data is available or the ReceiveTimeout occurs
					Message message = new Message();
					message.ServerStream = stream;
					stream.BeginRead(message.Buffer, 0, message.Buffer.Length, new AsyncCallback(readCallback), message);
				}
				else
					throw new ApplicationException("The network stream is not readable.");
			}
			catch (Exception e) { Debug.Write(e.ToString() + "\n"); }
		}
		private void readCallback(IAsyncResult ar) {
			//
			byte[] data=null;
			try {
				//Retrieve the stream from the state object and 
				//complete reading data to the remote server
				Message message = (Message)ar.AsyncState;
				NetworkStream stream = message.ServerStream;
				int bytesRead = stream.EndRead(ar);
				if(bytesRead > 0) {
					data = new byte[bytesRead];
					Array.Copy(message.Buffer, 0, data, 0, bytesRead);
					if(this.ResponseReceived != null) this.ResponseReceived(data, EventArgs.Empty);
				}
			} 
			catch (Exception e) { Debug.Write(e.ToString() + "\n"); }
		}
	}
}