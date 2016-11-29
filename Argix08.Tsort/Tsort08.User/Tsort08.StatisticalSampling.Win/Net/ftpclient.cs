//	File:	ftpclient.cs
//	Author:	J. Heary
//	Date:	11/21/08
//	Desc:	A file utility that renames files on an FTP mServerName.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;

namespace Argix.Net {
    //
    public class FtpClient {
        //Members
        private string mServerName = DEFAULT_SERVERNAME;
        private string mRemotePath = ".";
        private int mPortNumber = DEFAULT_PORT;
        private int mTimeoutSec = DEFAULT_TIMEOUT;
        private string mUsername = DEFAULT_USERNAME;
        private string mPassword = DEFAULT_PASSWORD;
        private bool mBinMode = false;

        private Socket mClientSocket = null;
        //private int mResultCode = 0;
        //private string mResult = null;

        internal struct FTPResponse {
            public int Code;
            public string Message;
            public FTPResponse(string response) { Message = response.Substring(4); Code = int.Parse(response.Substring(0,3)); }
            public override string ToString() { return Code.ToString() + " " + Message; }
        }
        
        public const string DEFAULT_SERVERNAME = "localhost";
        public const int DEFAULT_PORT = 21;
        public const int DEFAULT_TIMEOUT = 10;
        public const string DEFAULT_USERNAME = "anonymous";
        public const string DEFAULT_PASSWORD = "jheary@argixdirect.com";
        private const int BUFFER_SIZE = 512;

        //Interface
        public FtpClient() : this(DEFAULT_SERVERNAME,DEFAULT_USERNAME,DEFAULT_PASSWORD) { }
        public FtpClient(string serverName,string username,string password) : this(serverName,username,password,DEFAULT_PORT,DEFAULT_TIMEOUT) { }
        public FtpClient(string serverName,string username,string password,int portNumber,int timeoutSec) {
            //Constructor
            try {
                this.mServerName = serverName;
                this.mUsername = username;
                this.mPassword = password;
                this.mPortNumber = portNumber;
                this.mTimeoutSec = timeoutSec;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new FtpClient instance.",ex); }
        }
        ~FtpClient() {
            //Destructor
            cleanup();
        }
        #region Accessors: [Members]...
        public string Server { get { return this.mServerName; } set { this.mServerName = value; } }
        public string RemotePath { get { return this.mRemotePath; } set { this.mRemotePath = value; } }
        public int PortNumber { get { return this.mPortNumber; } set { this.mPortNumber = value; } }
        public int Timeout { get { return this.mTimeoutSec;  } set { this.mTimeoutSec = value; }  }
        public string Username { get { return this.mUsername; } set { this.mUsername = value; } }
        public string Password { get { return this.mPassword; } set { this.mPassword = value; } }
        public bool BinaryMode {
            get { return this.mBinMode; }
            set {
                if(this.mBinMode == value) return;
                FTPResponse response = new FTPResponse();
                if(value)
                    response = sendCommand("TYPE I");
                else
                    response = sendCommand("TYPE A");
                if(response.Code != 200) throw new ApplicationException(response.Message);
            }
        }
        public bool LoggedIn { get { return this.mClientSocket != null && this.mClientSocket.Connected; } }
        #endregion
        public void Login() {
            //
            if(LoggedIn) Close();
            FTPLog.LogMessage("Opening connection to " + this.mServerName);
            IPHostEntry iphe = Dns.GetHostEntry(this.mServerName); 
            foreach(IPAddress address in iphe.AddressList) {
                IPEndPoint ipe = new IPEndPoint(address, this.mPortNumber);
                Socket socket = new Socket(ipe.AddressFamily,SocketType.Stream,ProtocolType.Tcp);
                try { socket.Connect(ipe); } catch { }
                if(socket.Connected) {
                    this.mClientSocket = socket;
                    break;
                }
                else
                    continue;
            }
            if(this.mClientSocket == null) throw new ApplicationException("Couldn't connect to remote server " + this.mServerName + ".");

            FTPResponse response = receive();
            if(response.Code != 0) {
                if(response.Code != 220) {
                    Close();
                    throw new ApplicationException(response.Message);
                }
            }
            else 
                throw new ApplicationException("No response from ftp sever on connecting.");
            
            if(this.mUsername.Length > 0) {
                response = sendCommand("USER " + this.mUsername);
                if(response.Code != 0) {
                    if(!(response.Code == 331 || response.Code == 230)) {
                        cleanup();
                        throw new ApplicationException(response.Message);
                    }
                }
                else 
                    throw new ApplicationException("No response from ftp sever on getting username.");
            }
            if(response.Code != 230) {
                response = sendCommand("PASS " + this.mPassword);
                if(response.Code != 0) {
                    if(!(response.Code == 230 || response.Code == 202)) {
                        cleanup();
                        throw new ApplicationException(response.Message);
                    }
                }
                else
                    throw new ApplicationException("No response from ftp sever on setting password.");
            }
            
            FTPLog.LogMessage("Connected to " + this.mServerName);
            ChangeDir(this.mRemotePath);
        }
        public void Close() {
            FTPLog.LogMessage("Closing connection to " + this.mServerName);
            sendCommand("QUIT");
            cleanup();
        }
        public string[] GetFileList() { return GetFileList("*.*"); }
        public string[] GetFileList(string mask) {
            //
            if(!LoggedIn) Login();
            Socket cSocket = createDataSocket();
            FTPResponse response = sendCommand("NLST " + mask);
            if(!(response.Code == 150 || response.Code == 125)) throw new ApplicationException(response.Message);
            string message = "";
            byte[] buffer = new byte[BUFFER_SIZE];
            DateTime timeout = DateTime.Now.AddSeconds(this.mTimeoutSec);
            while(timeout > DateTime.Now) {
                int bytes = cSocket.Receive(buffer,buffer.Length,0);
                message += Encoding.ASCII.GetString(buffer,0,bytes);
                if(bytes < buffer.Length) break;
            }
            string[] msg = message.Replace("\r","").Split('\n');
            cSocket.Close();
            if(message.IndexOf("No such file or directory") != -1)
                msg = new string[] { };
            response = receive();
            if(response.Code != 226)
                msg = new string[] { };
            return msg;
        }
        public long GetFileSize(string fileName) {
            //
            if(!LoggedIn) Login();
            FTPResponse response = sendCommand("SIZE " + fileName);
            long size = 0;
            if(response.Code == 213)
                size = long.Parse(response.Message);
            else
                throw new ApplicationException(response.Message);
            return size;
        }
        public void Download(string remFileName) { this.Download(remFileName,"",false); }
        public void Download(string remFileName,Boolean resume) { this.Download(remFileName,"",resume);  }
        public void Download(string remFileName,string locFileName) { this.Download(remFileName,locFileName,false); }
        public void Download(string remFileName,string locFileName,Boolean resume) {
            if(!LoggedIn) Login();
            BinaryMode = true;
            FTPLog.LogMessage("Downloading file " + remFileName + " from " + this.mServerName + "/" + this.mRemotePath);
            if(locFileName.Equals("")) 
                locFileName = remFileName;
            FileStream output = null;
            if(!File.Exists(locFileName))
                output = File.Create(locFileName);
            else
                output = new FileStream(locFileName,FileMode.Open);
            Socket cSocket = createDataSocket();
            long offset = 0;
            if(resume) {
                offset = output.Length;
                if(offset > 0) {
                    FTPResponse r = sendCommand("REST " + offset);
                    if(r.Code != 350) {
                        //Server dosnt support resuming
                        offset = 0;
                        FTPLog.LogMessage("Resuming not supported:" + r.Message);
                    }
                    else {
                        FTPLog.LogMessage("Resuming at offset " + offset);
                        output.Seek(offset,SeekOrigin.Begin);
                    }
                }
            }
            FTPResponse response = sendCommand("RETR " + remFileName);
            if(response.Code != 150 && response.Code != 125) 
                throw new ApplicationException(response.Message);
            DateTime timeout = DateTime.Now.AddSeconds(this.mTimeoutSec);
            byte[] buffer = new byte[BUFFER_SIZE];
            while(timeout > DateTime.Now) {
                int bytes = cSocket.Receive(buffer,buffer.Length,0);
                output.Write(buffer,0,bytes);
                if(bytes <= 0) break;
            }
            output.Close();
            if(cSocket.Connected) cSocket.Close();
            response = receive();
            if(response.Code != 226 && response.Code != 250)
                throw new ApplicationException(response.Message);
        }
        public void Upload(string fileName) { this.Upload(fileName,false); }
        public void Upload(string fileName,bool resume) {
            //
            if(!LoggedIn) Login();
            Socket cSocket = null;
            FileStream input = null;
            long offset = 0;
            if(resume) {
                try {
                    BinaryMode = true;
                    offset = GetFileSize(Path.GetFileName(fileName));
                }
                catch(Exception) { offset = 0; }
            }
            try {
                //Open stream to read file
                input = new FileStream(fileName,FileMode.Open);
                if(resume && input.Length < offset) {
                    //Different file size
                    offset = 0;
                    FTPLog.LogMessage("Overwriting " + fileName);
                }
                else if(resume && input.Length == offset) {
                    // file done
                    input.Close();
                    FTPLog.LogMessage("Skipping completed " + fileName + " - turn resume off to not detect.");
                    return;
                }
                // dont create untill we know that we need it
                cSocket = createDataSocket();
                FTPResponse r;
                if(offset > 0) {
                    r = sendCommand("REST " + offset);
                    if(r.Code != 350) {
                        FTPLog.LogMessage("Resuming not supported");
                        offset = 0;
                    }
                }
                r = sendCommand("STOR " + Path.GetFileName(fileName));
                if(r.Code != 125 && r.Code != 150) throw new ApplicationException(r.Message);
                if(offset != 0) {
                    FTPLog.LogMessage("Resuming at offset " + offset);
                    input.Seek(offset,SeekOrigin.Begin);
                }
                FTPLog.LogMessage("Uploading file " + fileName + " to " + this.mRemotePath);
                byte[] buffer = new byte[BUFFER_SIZE];
                int bytes = 0;
                while((bytes = input.Read(buffer,0,buffer.Length)) > 0)
                    cSocket.Send(buffer,bytes,0);
            }
            catch(Exception ex) { throw ex; }
            finally { input.Close(); if(cSocket.Connected) cSocket.Close(); }
            FTPResponse response = receive();
            if(response.Code != 226 && response.Code != 250) throw new ApplicationException(response.Message);
        }
        public void UploadDirectory(string path,bool recurse) { this.UploadDirectory(path,recurse,"*.*"); }
        public void UploadDirectory(string path,bool recurse,string mask) {
            //
            string[] dirs = path.Replace("/",@"\").Split('\\');
            string rootDir = dirs[dirs.Length - 1];

            //make the root dir if it doed not exist
            if(this.GetFileList(rootDir).Length < 1) this.MakeDir(rootDir);
            this.ChangeDir(rootDir);
            foreach(string file in Directory.GetFiles(path,mask)) 
                this.Upload(file,true);
            if(recurse) {
                foreach(string directory in Directory.GetDirectories(path)) 
                    this.UploadDirectory(directory,recurse,mask);
            }
            this.ChangeDir("..");
        }
        public void DeleteFile(string fileName) {
            //
            if(!LoggedIn) Login();
            FTPResponse response = sendCommand("DELE " + fileName);
            if(response.Code != 250) throw new ApplicationException(response.Message);
            FTPLog.LogMessage("Deleted file " + fileName);
        }
        public void RenameFile(string oldFileName,string newFileName,bool overwrite) {
            //
            if(!LoggedIn) Login();
            FTPResponse response = sendCommand("RNFR " + oldFileName);
            if(response.Code != 350) throw new ApplicationException(response.Message);
            if(!overwrite && GetFileList(newFileName).Length > 0) throw new ApplicationException("File already exists");
            response = sendCommand("RNTO " + newFileName);
            if(response.Code != 250) throw new ApplicationException(response.Message);
            FTPLog.LogMessage("Renamed file " + oldFileName + " to " + newFileName);
        }
        public void MakeDir(string dirName) {
            //
            if(!LoggedIn) Login();
            FTPResponse response = sendCommand("MKD " + dirName);
            if(response.Code != 250 && response.Code != 257) throw new ApplicationException(response.Message);
            FTPLog.LogMessage("Created directory " + dirName);
        }
        public void RemoveDir(string dirName) {
            //
            if(!LoggedIn) Login();
            FTPResponse response = sendCommand("RMD " + dirName);
            if(response.Code != 250) throw new ApplicationException(response.Message);
            FTPLog.LogMessage("Removed directory " + dirName);
        }
        public void ChangeDir(string dirName) {
            //
            if(dirName == null || dirName.Equals(".") || dirName.Length == 0) 
                return;
            if(!LoggedIn) Login();
            FTPResponse response = sendCommand("CWD " + dirName);
            if(response.Code != 250) throw new ApplicationException(response.Message);
            response = sendCommand("PWD");
            if(response.Code != 257) throw new ApplicationException(response.Message);
            this.mRemotePath = response.Message.Split('"')[1];
            FTPLog.LogMessage("Current directory is " + this.mRemotePath);
        }
        #region Local Services: sendCommand(), receive(), createDataSocket(), cleanup()
        private FTPResponse sendCommand(string command) {
            //
            FTPLog.LogMessage("COMMAND\t" + command);
            Byte[] bytes = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
            this.mClientSocket.Send(bytes,bytes.Length,0);
            FTPResponse response = receive();
            FTPLog.LogMessage("RESPONSE\t" + response);
            return response;
        }
        private FTPResponse receive() {
            //Read a response from the socket receive buffer
            FTPResponse response = new FTPResponse();
            string message = "";
            do {
                byte[] buffer = new byte[BUFFER_SIZE];
                int bytes = this.mClientSocket.Receive(buffer,buffer.Length,SocketFlags.None);
                message += Encoding.ASCII.GetString(buffer,0,bytes);
                Thread.Sleep(100);
            }
            while(this.mClientSocket.Available > 0);
            FTPLog.LogMessage("MESSAGE\t" + message);
            if(message.Length > 0) {
                string[] msgs = message.Split('\n');
                message = (msgs.Length > 2) ? msgs[msgs.Length - 2] : msgs[0];
                if(message.Length > 4 && (message.Substring(3,1).Equals(" ") || message.Substring(3,1).Equals("-"))) 
                    response = new FTPResponse(message);
            }
            return response;
        }
        private Socket createDataSocket() {
            //
            FTPResponse response = sendCommand("PASV");
            if(response.Code != 227) throw new ApplicationException(response.Message);
            int index1 = response.ToString().IndexOf('(');
            int index2 = response.ToString().IndexOf(')');
            string ipData = response.ToString().Substring(index1 + 1,index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for(int i = 0; i < len && partCount <= 6; i++) {
                char ch = char.Parse(ipData.Substring(i,1));
                if(char.IsDigit(ch))
                    buf += ch;
                else if(ch != ',')
                    throw new ApplicationException("Malformed PASV result: " + response.ToString());
                if(ch == ',' || i + 1 == len) {
                    try {
                        parts[partCount++] = int.Parse(buf);
                        buf = "";
                    }
                    catch(Exception ex) { throw new ApplicationException("Malformed PASV result (not supported?): " + response.ToString(),ex); }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
            int portNumber = (parts[4] << 8) + parts[5];
            Socket socket = null;
            try {
                socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                IPHostEntry iphe = Dns.GetHostEntry(this.mServerName);
                IPAddress[] addresses = iphe.AddressList;
                IPAddress address = addresses.Length > 1 ? addresses[1] : addresses[0];
                IPEndPoint ep = new IPEndPoint(address,portNumber);
                socket.Connect(ep);
            }
            catch(Exception ex) {
                if(socket != null && socket.Connected) socket.Close();
                throw new ApplicationException("Can't connect to remote server.",ex);
            }
            return socket;
        }
        private void cleanup() {
            //
            if(this.mClientSocket != null && this.mClientSocket.Connected) {
                this.mClientSocket.Close();
            }
            this.mClientSocket = null;
        }
        #endregion
    }
}