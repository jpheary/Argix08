namespace Tsort.Utility {
    partial class DeviceInstaller {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.mServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            this.mEventLogInstaller = new System.Diagnostics.EventLogInstaller();
            // 
            // mServiceProcessInstaller
            // 
            this.mServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.mServiceProcessInstaller.Password = null;
            this.mServiceProcessInstaller.Username = null;
            // 
            // mServiceInstaller
            // 
            this.mServiceInstaller.Description = "Provides scale and printer singleton services.";
            this.mServiceInstaller.DisplayName = "Device Service";
            this.mServiceInstaller.ServiceName = "DeviceService";
            // 
            // mEventLogInstaller
            // 
            this.mEventLogInstaller.CategoryCount = 0;
            this.mEventLogInstaller.CategoryResourceFile = null;
            this.mEventLogInstaller.Log = "Tsort08";
            this.mEventLogInstaller.MessageResourceFile = null;
            this.mEventLogInstaller.ParameterResourceFile = null;
            this.mEventLogInstaller.Source = "DeviceService";
            // 
            // DeviceInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.mServiceProcessInstaller,
            this.mServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller mServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller mServiceInstaller;
        private System.Diagnostics.EventLogInstaller mEventLogInstaller;
    }
}