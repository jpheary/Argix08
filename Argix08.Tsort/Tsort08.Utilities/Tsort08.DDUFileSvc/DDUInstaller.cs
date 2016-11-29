using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Tsort05.Utility {
    [RunInstaller(true)]
    public partial class DDUInstaller :Installer {
        public DDUInstaller() {
            InitializeComponent();
        }
    }
}