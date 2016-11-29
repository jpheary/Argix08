using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Tsort.Utility {
    [RunInstaller(true)]
    public partial class DeviceInstaller :Installer {
        public DeviceInstaller() {
            InitializeComponent();
        }
    }
}