using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace Argix {
    [RunInstaller(true)]
    public partial class PCSInstaller :Installer {
        public PCSInstaller() {
            InitializeComponent();
        }
    }
}