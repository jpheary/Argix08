//	File:	ftpclient.cs
//	Author:	J. Heary
//	Date:	11/21/08
//	Desc:	A file utility that renames files on an FTP mServerName.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using Argix;

namespace Tsort05.Utility {
    //
    public class FtpClient {
        //Members
        private static Encoding ASCII = Encoding.ASCII;
        private string mServerName = DEFAULT_SERVERNAME;
        private string mRemotePath = ".";
        private int mPortNumber = DEFAULT_PORT;
        private int mTimeoutSec = 10;
        private string mUsername = DEFAULT_USERNAME;
        private string mPassword = DEFAULT_PASSWORD;

        private Socket mClientSocket = null;
        private bool mLoggedIn = false;
        private int mResultCode = 0;
        private string mResult = null;
        private string mMessage = null;
        private int mBytes = 0;
        private bool mBinMode = false;
        private Byte[] mBuffer = new Byte[BUFFER_SIZE];
        
        public const string DEFAULT_SERVERNAME = "localhost";
        public const string DEFAULT_USERNAME = "anonymous";
        public const string DEFAULT_PASSWORD = "anonymous@argixdirect.com";
        public const int DEFAULT_PORT = 21;
        public const int DEFAULT_TIMEOUT = 10;
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
                if(value)
                    sendCommand("TYPE I");
                else
                    sendCommand("TYPE A");
                if(this.mResultCode != 200) throw new ApplicationException(this.mResult.Substring(4));
            }
        }
        #endregion
        public void Login() {
            //
            if(this.mLoggedIn) this.Close();
            Debug.WriteLine("Opening connection to " + this.mServerName,"FtpClient");
            IPAddress addr = null;
            IPEndPoint ep = null;
            try {
                this.mClientSocket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                IPHostEntry iphe = Dns.GetHostEntry(this.mServerName);
                IPAddress[] addresses = iphe.AddressList;
                addr = addresses[0];
                ep = new IPEndPoint(addr,this.mPortNumber);
                this.mClientSocket.Connect(ep);
            }
            catch(Exception ex) {
                if(this.mClientSocket != null && this.mClientSocket.Connected) this.mClientSocket.Close();
                throw new ApplicationException("Couldn't connect to remote server " + this.mServerName + ".",ex);
            }
            readResponse();
            if(this.mResultCode != 220) {
                this.Close();
                throw new ApplicationException(this.mResult.Substring(4));
            }
            if(this.mUsername.Length > 0) {
                sendCommand("USER " + this.mUsername);
                if(!(this.mResultCode == 331 || this.mResultCode == 230)) {
                    cleanup();
                    throw new ApplicationException(this.mResult.Substring(4));
                }
            }
            if(this.mResultCode != 230) {
                sendCommand("PASS " + this.mPassword);

                if(!(this.mResultCode == 230 || this.mResultCode == 202)) {
                    cleanup();
                    throw new ApplicationException(this.mResult.Substring(4));
                }
            }
            this.mLoggedIn = true;
            Debug.WriteLine("Connected to " + this.mServerName,"FtpClient");
            ChangeDir(this.mRemotePath);
        }
        public void Close() {
            Debug.WriteLine("Closing connection to " + this.mServerName,"FtpClient");
            if(this.mClientSocket != null) sendCommand("QUIT");
            cleanup();
        }
        public string[] GetFileList() { return GetFileList("*.*"); }
        public string[] GetFileList(string mask) {
            //
            if(!this.mLoggedIn) Login();
            Socket cSocket = createDataSocket();
            sendCommand("NLST " + mask);
            if(!(this.mResultCode == 150 || this.mResultCode == 125)) throw new ApplicationException(this.mResult.Substring(4));
            this.mMessage = "";
            DateTime timeout = DateTime.Now.AddSeconds(this.mTimeoutSec);
            while(timeout > DateTime.Now) {
                int _bytes = cSocket.Receive(mBuffer,mBuffer.Length,0);
                this.mMessage += ASCII.GetString(mBuffer,0,_bytes);
                if(_bytes < this.mBuffer.Length) break;
            }
            string[] msg = this.mMessage.Replace("\r","").Split('\n');
            cSocket.Close();
            if(this.mMessage.IndexOf("No such file or directory") != -1)
                msg = new string[] { };
            readResponse();
            if(this.mResultCode != 226)
                msg = new string[] { };
            return msg;
        }
        public long GetFileSize(string fileName) {
            //
            if(!this.mLoggedIn) Login();
            sendCommand("SIZE " + fileName);
            long size = 0;
            if(this.mResultCode == 213)
                size = long.Parse(this.mResult.Substring(4));
            else
                throw new ApplicationException(this.mResult.Substring(4));
            return size;
        }
        public void Download(string remFileName) { this.Download(remFileName,"",false); }
        public void Download(string remFileName,Boolean resume) { this.Download(remFileName,"",resume);  }
        public void Download(string remFileName,string locFileName) { this.Download(remFileName,locFileName,false); }
        public void Download(string remFileName,string locFileName,Boolean resume) {
            if(!this.mLoggedIn) Login();
            BinaryMode = true;
            Debug.WriteLine("Downloading file " + remFileName + " from " + this.mServerName + "/" + this.mRemotePath,"FtpClient");
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
                    sendCommand("REST " + offset);
                    if(this.mResultCode != 350) {
                        //Server dosnt support resuming
                        offset = 0;
                        Debug.WriteLine("Resuming not supported:" + mResult.Substring(4),"FtpClient");
                    }
                    else {
                        Debug.WriteLine("Resuming at offset " + offset,"FtpClient");
                        output.Seek(offset,SeekOrigin.Begin);
                    }
                }
            }
            sendCommand("RETR " + remFileName);
            if(this.mResultCode != 150 && this.mResultCode != 125) 
                throw new ApplicationException(this.mResult.Substring(4));
            DateTime timeout = DateTime.Now.AddSeconds(this.mTimeoutSec);
            while(timeout > DateTime.Now) {
                this.mBytes = cSocket.Receive(mBuffer,mBuffer.Length,0);
                output.Write(this.mBuffer,0,this.mBytes);
                if(this.mBytes <= 0) break;
            }
            output.Close();
            if(cSocket.Connected) cSocket.Close();
            readResponse();
            if(this.mResultCode != 226 && this.mResultCode != 250)
                throw new ApplicationException(this.mResult.Substring(4));
        }
        public void Upload(string fileName) { this.Upload(fileName,false); }
        public void Upload(string fileName,bool resume) {
            //
            if(!this.mLoggedIn) Login();
            Socket cSocket = null;
            long offset = 0;
            if(resume) {
                try {
                    BinaryMode = true;
                    offset = GetFileSize(Path.GetFileName(fileName));
                }
                catch(Exception) { offset = 0; }
            }
            //open stream to read file
            FileStream input = new FileStream(fileName,FileMode.Open);
            if(resume && input.Length < offset) {
                // different file size
                Debug.WriteLine("Overwriting " + fileName,"FtpClient");
                offset = 0;
            }
            else if(resume && input.Length == offset) {
                // file done
                input.Close();
                Debug.WriteLine("Skipping completed " + fileName + " - turn resume off to not detect.","FtpClient");
                return;
            }
            // dont create untill we know that we need it
            cSocket = this.createDataSocket();
            if(offset > 0) {
                sendCommand("REST " + offset);
                if(this.mResultCode != 350) {
                    Debug.WriteLine("Resuming not supported","FtpClient");
                    offset = 0;
                }
            }
            sendCommand("STOR " + Path.GetFileName(fileName));
            if(this.mResultCode != 125 && this.mResultCode != 150) throw new ApplicationException(mResult.Substring(4));
            if(offset != 0) {
                Debug.WriteLine("Resuming at offset " + offset,"FtpClient");
                input.Seek(offset,SeekOrigin.Begin);
            }
            Debug.Write("Uploading file " + fileName + " to " + this.mRemotePath,"FtpClient");
            while((mBytes = input.Read(mBuffer,0,mBuffer.Length)) > 0) 
                cSocket.Send(mBuffer,mBytes,0);
            input.Close();
            if(cSocket.Connected) cSocket.Close();
            readResponse();
            if(this.mResultCode != 226 && this.mResultCode != 250) throw new ApplicationException(this.mResult.Substring(4));
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
            if(!this.mLoggedIn) Login();
            sendCommand("DELE " + fileName);
            if(this.mResultCode != 250) throw new ApplicationException(this.mResult.Substring(4));
            Debug.WriteLine("Deleted file " + fileName,"FtpClient");
        }
        public void RenameFile(string oldFileName,string newFileName,bool overwrite) {
            //
            if(!this.mLoggedIn) Login();
            sendCommand("RNFR " + oldFileName);
            if(this.mResultCode != 350) throw new ApplicationException(this.mResult.Substring(4));
            if(!overwrite && GetFileList(newFileName).Length > 0) throw new ApplicationException("File already exists");
            sendCommand("RNTO " + newFileName);
            if(this.mResultCode != 250) throw new ApplicationException(this.mResult.Substring(4));
            Debug.WriteLine("Renamed file " + oldFileName + " to " + newFileName,"FtpClient");
        }
        public void MakeDir(string dirName) {
            //
            if(!this.mLoggedIn) Login();
            sendCommand("MKD " + dirName);
            if(this.mResultCode != 250 && this.mResultCode != 257) throw new ApplicationException(this.mResult.Substring(4));
            Debug.WriteLine("Created directory " + dirName,"FtpClient");
        }
        public void RemoveDir(string dirName) {
            //
            if(!this.mLoggedIn) Login();
            sendCommand("RMD " + dirName);
            if(this.mResultCode != 250) throw new ApplicationException(this.mResult.Substring(4));
            Debug.WriteLine("Removed directory " + dirName,"FtpClient");
        }
        public void ChangeDir(string dirName) {
            //
            if(dirName == null || dirName.Equals(".") || dirName.Length == 0) 
                return;
            if(!this.mLoggedIn) Login();
            sendCommand("CWD " + dirName);
            if(this.mResultCode != 250) throw new ApplicationException(mResult.Substring(4));
            sendCommand("PWD");
            if(this.mResultCode != 257) throw new ApplicationException(mResult.Substring(4));
            // gonna have to do better than this....
            this.mRemotePath = this.mMessage.Split('"')[1];
            Debug.WriteLine("Current directory is " + this.mRemotePath,"FtpClient");
        }
        #region Local Services: readResponse(), readLine(), sendCommand(), createDataSocket(), cleanup()
        private void readResponse() {
            //
            this.mMessage = "";
            this.mResult = readLine();
            if(this.mResult.Length > 3)
                this.mResultCode = int.Parse(this.mResult.Substring(0,3));
            else
                this.mResult = null;
        }
        private string readLine() {
            //
            while(true) {
                this.mBytes = this.mClientSocket.Receive(this.mBuffer,this.mBuffer.Length,0);
                this.mMessage += ASCII.GetString(this.mBuffer,0,this.mBytes);
                if(this.mBytes < this.mBuffer.Length) 
                    break;
            }
            string[] msg = this.mMessage.Split('\n');
            if(this.mMessage.Length > 2)
                this.mMessage = msg[msg.Length - 2];
            else
                this.mMessage = msg[0];
            if(this.mMessage.Length > 4 && !this.mMessage.Substring(3,1).Equals(" ")) return this.readLine();
            return mMessage;
        }
        private void sendCommand(String command) {
            //
            Byte[] cmdBytes = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());
            this.mClientSocket.Send(cmdBytes,cmdBytes.Length,0);
            readResponse();
        }
        private Socket createDataSocket() {
            //
            sendCommand("PASV");
            if(this.mResultCode != 227) throw new ApplicationException(this.mResult.Substring(4));
            int index1 = this.mResult.IndexOf('(');
            int index2 = this.mResult.IndexOf(')');
            string ipData = this.mResult.Substring(index1 + 1,index2 - index1 - 1);
            int[] parts = new int[6];
            int len = ipData.Length;
            int partCount = 0;
            string buf = "";
            for(int i = 0; i < len && partCount <= 6; i++) {
                char ch = char.Parse(ipData.Substring(i,1));
                if(char.IsDigit(ch))
                    buf += ch;
                else if(ch != ',')
                    throw new ApplicationException("Malformed PASV result: " + this.mResult);
                if(ch == ',' || i + 1 == len) {
                    try {
                        parts[partCount++] = int.Parse(buf);
                        buf = "";
                    }
                    catch(Exception ex) { throw new ApplicationException("Malformed PASV result (not supported?): " + this.mResult,ex); }
                }
            }
            string ipAddress = parts[0] + "." + parts[1] + "." + parts[2] + "." + parts[3];
            int portNumber = (parts[4] << 8) + parts[5];
            Socket socket = null;
            IPEndPoint ep = null;
            try {
                socket = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
                ep = new IPEndPoint(Dns.GetHostEntry(ipAddress).AddressList[0],portNumber);
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
            if(this.mClientSocket != null) {
                this.mClientSocket.Close();
                this.mClientSocket = null;
            }
            this.mLoggedIn = false;
        }
        #endregion
    }
}