using System;
using System.Windows.Forms;
using Symbol.Barcode;

namespace Symbol {
	//The class which communicates with the EMDK for .NET scanner API - Symbol.Barcode. 
    class BarcodeAPI {
        private Reader mReader=null;
        private ReaderData mReaderData=null;
        private EventHandler mReadNotifyHandler=null;
        private EventHandler mStatusNotifyHandler=null;

        public bool InitReader() {
            //Initialize the reader
            if(this.mReader != null) {
                return false;
            }
            else {
                try {
                    //Get the device selected by the user
                    Symbol.Generic.Device device = Symbol.StandardForms.SelectDevice.Select( Symbol.Barcode.Device.Title, Symbol.Barcode.Device.AvailableDevices);
                    if(device == null) {
                        MessageBox.Show("No Device Selected","SelectDevice");
                        return false;
                    }

                    //Create the reader based on selected device, and the reader data
                    this.mReader = new Symbol.Barcode.Reader(device);
                    this.mReaderData = new Symbol.Barcode.ReaderData(Symbol.Barcode.ReaderDataTypes.Text,Symbol.Barcode.ReaderDataLengths.MaximumLabel);

                    //Enable the Reader, and set the aim type to trigger.
                    this.mReader.Actions.Enable();
                    switch(this.mReader.ReaderParameters.ReaderType) {
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_IMAGER:
                            this.mReader.ReaderParameters.ReaderSpecific.ImagerSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER;
                            break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_LASER:
                            this.mReader.ReaderParameters.ReaderSpecific.LaserSpecific.AimType = Symbol.Barcode.AIM_TYPE.AIM_TYPE_TRIGGER;
                            break;
                        case Symbol.Barcode.READER_TYPE.READER_TYPE_CONTACT:
                            // AimType is not supported by the contact readers.
                            break;
                    }
                    this.mReader.Actions.SetParameters();
                }
                catch(Symbol.Exceptions.OperationFailureException ex) {
                    MessageBox.Show("InitReader"+"\n" + "OperationFailure" + "\n" + ex.Message + "\n" + "Result" +" = " + (Symbol.Results)((uint)ex.Result) );
                    return false;
                }
                catch(Symbol.Exceptions.InvalidRequestException ex) { MessageBox.Show("InitReader" + "\n" + "InvalidRequest" + "\n" + ex.Message);
                    return false;
                }
                catch(Symbol.Exceptions.InvalidIndexerException ex) { 
                    MessageBox.Show("InitReader" + "\n" +  "InvalidIndexer" + "\n" + ex.Message);
                    return false;
                }
                return true;
            }
        }
        public void TermReader() {
            //Stop reading and disable/close the reader
            if(this.mReader != null) {
                try {
                    //Stop all the notifications and detach all the notification handler if the user has not done it already
                    StopRead();
                    DetachReadNotify();
                    DetachStatusNotify();

                    //Disable the reader, and free it up
                    this.mReader.Actions.Disable();
                    this.mReader.Dispose();
                    this.mReader = null;
                }
                catch(Symbol.Exceptions.OperationFailureException ex) { MessageBox.Show("TermReader" + "\n" + "OperationFailure" + "\n" + ex.Message + "\n" + "Result" + " = " + (Symbol.Results)((uint)ex.Result)  ); }
                catch(Symbol.Exceptions.InvalidRequestException ex) { MessageBox.Show("TermReader" + "\n" +  "InvalidRequest" + "\n" + ex.Message); }
                catch(Symbol.Exceptions.InvalidIndexerException ex) { MessageBox.Show("TermReader" + "\n" + "InvalidIndexer" + "\n" + ex.Message);  }
            }

            // After disposing the reader, dispose the reader data. 
            if(this.mReaderData != null) {
                try {
                    //Free it up.
                    this.mReaderData.Dispose();
                    this.mReaderData = null;
                }
                catch(Symbol.Exceptions.OperationFailureException ex) { MessageBox.Show("TermReader" + "\n" +  "OperationFailure" + "\n" + ex.Message + "\n" + "Result" + " = " + (Symbol.Results)((uint)ex.Result) );  }
                catch(Symbol.Exceptions.InvalidRequestException ex) { MessageBox.Show("TermReader" + "\n" + "InvalidRequest" + "\n" + ex.Message); }
                catch(Symbol.Exceptions.InvalidIndexerException ex) { MessageBox.Show("TermReader" + "\n" + "InvalidIndexer" + "\n" + ex.Message); }
            }
        }
        public void StartRead(bool toggleSoftTrigger) {
            // Start a read on the reader
            if(this.mReader != null && this.mReaderData != null) {
                try {
                    if(!this.mReaderData.IsPending) {
                        //Submit a read
                        this.mReader.Actions.Read(mReaderData);
                        if(toggleSoftTrigger && this.mReader.Info.SoftTrigger == false)
                            this.mReader.Info.SoftTrigger = true;
                    }
                }
                catch(Symbol.Exceptions.OperationFailureException ex) { MessageBox.Show("StartRead" + "\n" + "OperationFailure" + "\n" + ex.Message + "\n" + "Result" + " = " + (Symbol.Results)((uint)ex.Result)); }
                catch(Symbol.Exceptions.InvalidRequestException ex) { MessageBox.Show("StartRead" + "\n" +  "InvalidRequest" + "\n" + ex.Message); }
                catch(Symbol.Exceptions.InvalidIndexerException ex) { MessageBox.Show("StartRead" + "\n" + "InvalidIndexer" + "\n" + ex.Message); }
            }
        }
        public void StopRead() {
            //Stop all reads on the reader
            if(this.mReader != null) {
                try {
                    //Flush (Cancel all pending reads)
                    if(this.mReader.Info.SoftTrigger == true)
                        this.mReader.Info.SoftTrigger = false;
                    this.mReader.Actions.Flush();
                }
                catch(Symbol.Exceptions.OperationFailureException ex) { MessageBox.Show("StopRead" + "\n" + "OperationFailure" + "\n" + ex.Message + "\n" + "Result" + " = " + (Symbol.Results)((uint)ex.Result)); }
                catch(Symbol.Exceptions.InvalidRequestException ex) { MessageBox.Show("StopRead" + "\n" +  "InvalidRequest" + "\n" + ex.Message); }
                catch(Symbol.Exceptions.InvalidIndexerException ex) { MessageBox.Show("StopRead" + "\n" + "InvalidIndexer" + "\n" + ex.Message); }
            }
        }
        public Symbol.Barcode.Reader Reader { get { return this.mReader; } }
        public void AttachReadNotify(System.EventHandler ReadNotifyHandler) {
            //Attach a ReadNotify handler
            if(this.mReader != null) {
                //Attach the read notification handler
                this.mReader.ReadNotify += ReadNotifyHandler;
                this.mReadNotifyHandler = ReadNotifyHandler;
            }
        }
        public void DetachReadNotify() {
            //Detach the ReadNotify handler
            if(this.mReader != null && this.mReadNotifyHandler!=null) {
                //Detach the read notification handler
                this.mReader.ReadNotify -= this.mReadNotifyHandler;
                this.mReadNotifyHandler = null;
            }
        }
        public void AttachStatusNotify(System.EventHandler StatusNotifyHandler) {
            //Attach a StatusNotify handler
            if(this.mReader != null) {
                //Attach status notification handler
                this.mReader.StatusNotify += StatusNotifyHandler;
                this.mStatusNotifyHandler = StatusNotifyHandler;
            }
        }
        public void DetachStatusNotify() {
            //Detach a StatusNotify handler
            //If we have a reader registered for receiving the status notifications
            if(this.mReader != null && this.mStatusNotifyHandler!=null) {
                //Detach the status notification handler
                this.mReader.StatusNotify -= this.mStatusNotifyHandler;
                this.mStatusNotifyHandler = null;
            }
        }
    }
}
 