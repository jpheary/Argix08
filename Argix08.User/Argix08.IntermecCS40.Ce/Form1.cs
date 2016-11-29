using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Intermec.Multimedia;

namespace Argix {
    //
    public partial class Form1 : Form {
        //Members
        private Camera mCamera=null;

        //Interface
        public Form1() {
            //Constructor
            InitializeComponent();

            //Init camera
            this.mCamera = new Camera(this.picImage, Camera.ImageResolutionType.Medium);
            this.mCamera.PictureBoxUpdate = Camera.PictureBoxUpdateType.Fit;
            this.mCamera.Streaming = false;

            //Setup the Snapshot variables
            this.mCamera.SnapshotFile.Directory = "\\My Documents\\My Pictures";
            this.mCamera.SnapshotFile.Filename = "";
            this.mCamera.SnapshotFile.FilenamePadding = Camera.FilenamePaddingType.DateTime;
            this.mCamera.SnapshotFile.ImageFormatType = Camera.ImageType.JPG;
            this.mCamera.SnapshotFile.JPGQuality = 80;

            //Hook the snapshot event
            this.mCamera.SnapshotEvent += new SnapshotEventHandler(OnSnapshot);
            //this.mCamera.MotionDetectionEvent += new MotionDetectionEventHandler(OnMotionDetection);

            //Setup caption string; add the date/time to the image
            this.mCamera.ImprintCaptionString = "";
            this.mCamera.ImprintCaptionPos = Camera.ImprintCaptionPosType.UpperLeft;
            this.mCamera.ImprintDateTimePos = Camera.ImprintDateTimePosType.LowerRight;

            //Do not display any camera information in the image (false by default for snapshots)
            this.mCamera.DisplayCameraInfo = false;

            //
            this.mnuSnap.Enabled = false;
        }
        private void OnCommentChanged(object sender, EventArgs e) {
            //
            this.mCamera.SnapshotFile.Filename = this.txtComment.Text;
            this.mCamera.ImprintCaptionString = this.txtComment.Text;
            this.mCamera.Streaming = this.txtComment.Text.Length > 0;
            this.mnuSnap.Enabled = this.txtComment.Text.Length > 0;
        }
        private void OnSnapClick(object sender, EventArgs e) {
            //
            this.mCamera.Snapshot(Camera.ImageResolutionType.Highest);
        }
        private void OnMotionDetection(object sender, Camera.MotionDetectionArgs MDArgs) {
            //Called when motion is detected; request a snapshot
            this.mCamera.Snapshot(Camera.ImageResolutionType.Medium);
        }
        private void OnSnapshot(object sender, Camera.SnapshotArgs sna) {
            //
            try {
                if (sna.Status == Camera.SnapshotStatus.Ok) {
                    //
                    System.IO.FileStream fs = new System.IO.FileStream(sna.Filename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    System.IO.BinaryReader reader = new System.IO.BinaryReader(fs);
                    byte[] bytes = reader.ReadBytes((int)fs.Length);
                    reader.Close();

                    string filename = sna.Filename.Substring(this.mCamera.SnapshotFile.Directory.Length + 1);
                    if (Freight.FreightGateway.SaveImage(filename, bytes)) {
                        this.mCamera.SnapshotFile.Filename = "";
                        this.mCamera.ImprintCaptionString = "";
                        this.mCamera.Streaming = false;
                        this.mnuSnap.Enabled = false;
                        this.txtComment.Text = "";
                        MessageBox.Show(sna.Filename + " was saved.");
                    }
                    else {
                        MessageBox.Show(sna.Filename + " was NOT saved.");
                    }
                }
                else {
                    MessageBox.Show("Snapshot Error!");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}