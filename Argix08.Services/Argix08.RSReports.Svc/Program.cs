using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace Argix {
    //
    static class Program {
        //
        static void Main() {
            //The main entry point for the application
            //ServiceBase[] ServicesToRun = new ServiceBase[] { new Argix.Terminals.RSReportsSvc() };
            //ServiceBase.Run(ServicesToRun);
            new Argix.Terminals.RSReportsSvc().Start();
        }
    }
}
