using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace Tsort.Devices {
    //
    static class globals {
        //The main entry point for the application
        static void Main() {
#if DEBUG
            //Run the service in DEBUG (Win95) mode or NT service mode
            System.Windows.Forms.Application.Run(new frmMain(new DeviceService()));
#else
            ServiceBase[] servicesToRun;
            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //   servicesToRun = new ServiceBase[] {new Service1(), new MySecondUserService()};
            servicesToRun = new ServiceBase[] { new DeviceService() };
            ServiceBase.Run(servicesToRun);
#endif
        }
    }
}
