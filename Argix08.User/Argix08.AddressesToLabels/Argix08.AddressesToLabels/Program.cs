using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Tsort.Labels;
using Tsort.Devices.Printers;

namespace Argix {
    //
    class Program {
        //Members

        //Interface        
        static void Main(string[] args) {
            //
            DirectoryLabelStore dls = new DirectoryLabelStore("C:\\Label Studio\\Backups\\");
            dls.Refresh();
            DirectoryLabelTemplate lt = (DirectoryLabelTemplate)dls.Item("999","110");
            lt.LabelString = lt.LabelString.Replace("lt;","<").Replace("gt;",">");
            
            StreamReader sr = new StreamReader("Address.txt");
            string record = "";
            while((record = sr.ReadLine()) != null) {
                //Get a label maker and format the template
                LabelMaker labelMaker = new AddressLabelMaker(record);
                string label = labelMaker.FormatLabelTemplate(lt.Template);
                
                //Print
                ILabelPrinter printer = (ILabelPrinter)new ConsolePrinter();
                printer.TurnOn();
                printer.Print(label);
            }
            sr.Close();
            
            Console.Read();
        }
    }
}
