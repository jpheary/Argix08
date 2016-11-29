//	File:	filesvc.cs
//	Author:	J. Heary
//	Date:	07/08/10
//	Desc:	.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.ServiceProcess;
using System.ServiceModel;
using System.Windows.Forms;
using Tsort.Devices.Scales;
using Tsort.Devices.Printers;

namespace Tsort.Devices {
	//
	public class DeviceService : System.ServiceProcess.ServiceBase {
		//Members
        private ServiceHost mScaleHost = null;
        private AutoScale mScale=null;
        private ServiceHost mLabelPrinterHost = null;
        private Zebra110 mLabeler=null;
        public event ScaleEventHandler WeightChanged=null;
        private System.ComponentModel.Container components = null;
		
		//Interface
        public DeviceService() {
			//Constructor
            //Required by the Windows.Forms Component Designer.
            InitializeComponent();
        }
        public AutoScale Scale { get { return this.mScale; } }
        public Zebra110 Printer { get { return this.mLabeler; } }
        private void InitializeComponent() {
            // 
            // LindaSvc
            // 
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.ServiceName = "DeviceService";

		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
        #region Non-Win Service: Start(), Stop(), Pause(), Continue()
        public void Start() { OnStart(null); }
        public void Stop() { OnStop(); }
        public void Pause() { OnPause(); }
        public void Continue() { OnContinue(); }
        #endregion
        #region Service Overrides: OnStart(), OnStop(), OnPause(), OnContinue()
        protected override void OnStart(string[] args) {
			//Set things in motion so your service can do its work
            this.mScale = new AutoScale();
            this.mScale.WeightChanged += new ScaleEventHandler(OnWeightChanged);
            this.mScaleHost = new ServiceHost(this.mScale);
            this.mScaleHost.Open();

            this.mLabeler = new Zebra110();
            this.mLabelPrinterHost = new ServiceHost(this.mLabeler);
            this.mLabelPrinterHost.Open();
        }
		protected override void OnStop() {
			//Stop this service
            if(this.mScaleHost != null) {
                this.mScale.TurnOff();
                this.mScaleHost.Close();
                this.mScaleHost = null;
            }
            if(this.mLabelPrinterHost != null) {
                this.mLabeler.TurnOff();
                this.mLabelPrinterHost.Close();
                this.mLabelPrinterHost = null;
            }
        }
		protected override void OnPause() {
			//Pause this service
        }
		protected override void OnContinue() {
			//Continue this service
        }
        #endregion
        private void OnWeightChanged(object source,ScaleEventArgs e) {
            //
            if(this.WeightChanged != null) this.WeightChanged(this, e);
        }
    }
}
