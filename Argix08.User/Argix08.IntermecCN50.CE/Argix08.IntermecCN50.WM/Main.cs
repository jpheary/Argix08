using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.LocationService.Consumer;

namespace Argix {
    //
    public partial class Main:Form {
        //
		private LocationReceiver mReceiver=null;

		public Main() {
            try {
                InitializeComponent();
                this.mReceiver = new LocationReceiver(this); 
                this.mReceiver.LocationNotify += new LocationNotifyEventHandler(OnLocationNotify);
                this.mReceiver.LocationServerShutdown += new LocationServerShutdownHandler(OnLocationServerShutdown);
            }
            catch(LocationServicesException e1) { MessageBox.Show(e1.Message); }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
		}
        private void OnUpdateLocation(object sender,EventArgs e) {
			try {
                this.mReceiver.UpdateLocationData();
                LocationData ld = this.mReceiver.CurrentLocation;
                this.textBoxLatitude.Text = ld.LatitudeString;
                this.textBoxLongitude.Text = ld.LongitudeString;
                this.textBoxAltitude.Text = ld.Altitude.ToString();
                this.textBoxTime.Text = ld.LocationTime.ToString();
                TerminalsFactory.SaveGPSData(convertLatitude(ld),convertLongitude(ld),Convert.ToInt32(ld.Altitude),ld.LocationTime);
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
		}
        void OnLocationNotify(object sender,LocationData ld) {
            try {
                this.textBoxLatitude.Text = ld.LatitudeString;
                this.textBoxLongitude.Text = ld.LongitudeString;
                this.textBoxAltitude.Text = ld.Altitude.ToString();
                this.textBoxTime.Text = ld.LocationTime.ToString();
                TerminalsFactory.SaveGPSData(convertLatitude(ld),convertLongitude(ld),Convert.ToInt32(ld.Altitude),ld.LocationTime); 
            }
            catch(LocationServicesException e2) { MessageBox.Show(e2.Message); }
        }
        void OnLocationServerShutdown(object sender,EventArgs dummyObject) {
            MessageBox.Show("Demo Timeout, Location Server shutdown.  Warm boot device for new demo run.");
            Application.Exit();
        }
        private int convertLongitude(LocationData ld) {
            return 360000 * ld.LongitudeDegree + 6000 + ld.LongitudeMinute + 100 * ld.LongitudeSecond;
        }
        private int convertLatitude(LocationData ld) {
            return 360000 * ld.LatitudeDegree + 6000 + ld.LatitudeMinute + 100 * ld.LatitudeSecond;
        }
    }
}