using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Tsort.Devices.Printers {
	//
    public class ZebraWin2 {
        //Structure and API declarions
        [StructLayout(LayoutKind.Sequential,CharSet=CharSet.Ansi)]
        public class DOCINFOA {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
        [DllImport("winspool.Drv",EntryPoint="OpenPrinterA",SetLastError=true,CharSet=CharSet.Ansi,ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter,out IntPtr hPrinter,IntPtr pd);
        [DllImport("winspool.Drv",EntryPoint="ClosePrinter",SetLastError=true,ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv",EntryPoint="StartDocPrinterA",SetLastError=true,CharSet=CharSet.Ansi,ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter,Int32 level,[In,MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);
        [DllImport("winspool.Drv",EntryPoint="EndDocPrinter",SetLastError=true,ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv",EntryPoint="StartPagePrinter",SetLastError=true,ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv",EntryPoint="EndPagePrinter",SetLastError=true,ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv",EntryPoint="WritePrinter",SetLastError=true,ExactSpelling=true,CallingConvention=CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter,IntPtr pBytes,Int32 dwCount,out Int32 dwWritten);

        public static bool SendBytesToPrinter(string szPrinterName,IntPtr pBytes,Int32 dwCount) {
            //When the function is given a printer name and an unmanaged array of bytes, the 
            //function sends those bytes to the print queue.
            Int32 dwError = 0,dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool bSuccess = false;

            di.pDocName = "My C#.NET RAW Document";
            di.pDataType = "RAW";

            // Open the printer
            if(OpenPrinter(szPrinterName.Normalize(),out hPrinter,IntPtr.Zero)) {
                // Start a document
                if(StartDocPrinter(hPrinter,1,di)) {
                    // Start a page
                    if(StartPagePrinter(hPrinter)) {
                        //Write bytes
                        bSuccess = WritePrinter(hPrinter,pBytes,dwCount,out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            if(bSuccess == false) dwError = Marshal.GetLastWin32Error();
            return bSuccess;
        }
        public static bool SendStringToPrinter(string szPrinterName,string szString) {
            //Assume that the printer is expecting ANSI text, and then convert the string to ANSI text.
            IntPtr pBytes;
            Int32 dwCount;
            dwCount = szString.Length;
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            SendBytesToPrinter(szPrinterName,pBytes,dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }
    }
}
