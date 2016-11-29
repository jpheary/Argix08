namespace Argix {
    partial class PCSInstaller {
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
            this.mServiceInstaller.Description = "Moves PCS EDI files into an FTP-related folder for AS/400 pickup, and moves PCS s" +
                "canner files into Bearware RDS-related folder for Bearware pickup. ";
            this.mServiceInstaller.DisplayName = "PCS File Service";
            this.mServiceInstaller.ServiceName = "PCSFileSvc";
            this.mServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Manual;
            //
            // mEventLogInstaller
            //
            this.mEventLogInstaller.Log = "PCSServices";
            this.mEventLogInstaller.Source = "PCSFileSvc";
            // 
            // PCSInstaller
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