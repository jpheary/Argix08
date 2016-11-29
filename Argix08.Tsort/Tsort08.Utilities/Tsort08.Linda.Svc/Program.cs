//	File:	program.cs
//	Author:	J. Heary
//	Date:	07/08/10
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace Tsort.Utility {
    static class Program {
        /// <summary>The main entry point for the application. </summary>
        static void Main() {
#if DEBUG
			//Run the service in DEBUG (Win95) mode or NT service mode
            System.Windows.Forms.Application.Run(new frmMain(new LindaSvc()));
#else
            ServiceBase[] servicesToRun;
            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //   servicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            servicesToRun = new ServiceBase[] { new LindaSvc() };
            ServiceBase.Run(servicesToRun);
#endif
        }
    }
}