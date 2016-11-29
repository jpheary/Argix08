namespace Tsort.Devices.Printers {
    partial class LabelPrinters {
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
            this.lnkFindPrinter = new System.Windows.Forms.LinkLabel();
            this.cboStopBits = new System.Windows.Forms.ComboBox();
            this.cboDataBits = new System.Windows.Forms.ComboBox();
            this.cboBaud = new System.Windows.Forms.ComboBox();
            this.cboParity = new System.Windows.Forms.ComboBox();
            this.cboPort = new System.Windows.Forms.ComboBox();
            this._lblBaud = new System.Windows.Forms.Label();
            this._lblStopBits = new System.Windows.Forms.Label();
            this._lblDataBits = new System.Windows.Forms.Label();
            this._lblParity = new System.Windows.Forms.Label();
            this._lblPortName = new System.Windows.Forms.Label();
            this._lblPrinterType = new System.Windows.Forms.Label();
            this.cboPrinter = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lnkFindPrinter
            // 
            this.lnkFindPrinter.Location = new System.Drawing.Point(3,341);
            this.lnkFindPrinter.Name = "lnkFindPrinter";
            this.lnkFindPrinter.Size = new System.Drawing.Size(101,21);
            this.lnkFindPrinter.TabIndex = 132;
            this.lnkFindPrinter.TabStop = true;
            this.lnkFindPrinter.Text = "Auto Find Printer";
            this.lnkFindPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboStopBits
            // 
            this.cboStopBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cboStopBits.Location = new System.Drawing.Point(6,254);
            this.cboStopBits.MaxDropDownItems = 4;
            this.cboStopBits.Name = "cboStopBits";
            this.cboStopBits.Size = new System.Drawing.Size(150,21);
            this.cboStopBits.TabIndex = 131;
            this.cboStopBits.SelectionChangeCommitted += new System.EventHandler(this.OnPortStopBitsChanged);
            this.cboStopBits.Leave += new System.EventHandler(this.OnLeave);
            this.cboStopBits.Enter += new System.EventHandler(this.OnEnter);
            this.cboStopBits.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.cboStopBits.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // cboDataBits
            // 
            this.cboDataBits.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDataBits.Items.AddRange(new object[] {
            "7",
            "8"});
            this.cboDataBits.Location = new System.Drawing.Point(6,206);
            this.cboDataBits.MaxDropDownItems = 4;
            this.cboDataBits.Name = "cboDataBits";
            this.cboDataBits.Size = new System.Drawing.Size(150,21);
            this.cboDataBits.TabIndex = 130;
            this.cboDataBits.SelectionChangeCommitted += new System.EventHandler(this.OnPortDataBitsChanged);
            this.cboDataBits.Leave += new System.EventHandler(this.OnLeave);
            this.cboDataBits.Enter += new System.EventHandler(this.OnEnter);
            this.cboDataBits.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.cboDataBits.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // cboBaud
            // 
            this.cboBaud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboBaud.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaud.Items.AddRange(new object[] {
            "110",
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "57600"});
            this.cboBaud.Location = new System.Drawing.Point(6,158);
            this.cboBaud.MaxDropDownItems = 4;
            this.cboBaud.Name = "cboBaud";
            this.cboBaud.Size = new System.Drawing.Size(150,21);
            this.cboBaud.TabIndex = 129;
            this.cboBaud.SelectionChangeCommitted += new System.EventHandler(this.OnPortBaudRateChanged);
            this.cboBaud.Leave += new System.EventHandler(this.OnLeave);
            this.cboBaud.Enter += new System.EventHandler(this.OnEnter);
            this.cboBaud.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.cboBaud.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // cboParity
            // 
            this.cboParity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboParity.Items.AddRange(new object[] {
            "None",
            "Even",
            "Odd"});
            this.cboParity.Location = new System.Drawing.Point(6,302);
            this.cboParity.MaxDropDownItems = 4;
            this.cboParity.Name = "cboParity";
            this.cboParity.Size = new System.Drawing.Size(150,21);
            this.cboParity.TabIndex = 128;
            this.cboParity.SelectionChangeCommitted += new System.EventHandler(this.OnPortParityChanged);
            this.cboParity.Leave += new System.EventHandler(this.OnLeave);
            this.cboParity.Enter += new System.EventHandler(this.OnEnter);
            this.cboParity.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.cboParity.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // cboPort
            // 
            this.cboPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPort.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cboPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9"});
            this.cboPort.Location = new System.Drawing.Point(6,110);
            this.cboPort.MaxDropDownItems = 4;
            this.cboPort.Name = "cboPort";
            this.cboPort.Size = new System.Drawing.Size(150,21);
            this.cboPort.TabIndex = 127;
            this.cboPort.SelectionChangeCommitted += new System.EventHandler(this.OnPortNameChanged);
            this.cboPort.Leave += new System.EventHandler(this.OnLeave);
            this.cboPort.Enter += new System.EventHandler(this.OnEnter);
            this.cboPort.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.cboPort.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // _lblBaud
            // 
            this._lblBaud.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblBaud.Location = new System.Drawing.Point(6,140);
            this._lblBaud.Name = "_lblBaud";
            this._lblBaud.Size = new System.Drawing.Size(72,18);
            this._lblBaud.TabIndex = 126;
            this._lblBaud.Text = "Baud Rate";
            // 
            // _lblStopBits
            // 
            this._lblStopBits.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblStopBits.Location = new System.Drawing.Point(6,236);
            this._lblStopBits.Name = "_lblStopBits";
            this._lblStopBits.Size = new System.Drawing.Size(72,18);
            this._lblStopBits.TabIndex = 125;
            this._lblStopBits.Text = "Stop Bits";
            // 
            // _lblDataBits
            // 
            this._lblDataBits.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblDataBits.Location = new System.Drawing.Point(6,188);
            this._lblDataBits.Name = "_lblDataBits";
            this._lblDataBits.Size = new System.Drawing.Size(72,18);
            this._lblDataBits.TabIndex = 124;
            this._lblDataBits.Text = "Data Bits";
            // 
            // _lblParity
            // 
            this._lblParity.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblParity.Location = new System.Drawing.Point(6,284);
            this._lblParity.Name = "_lblParity";
            this._lblParity.Size = new System.Drawing.Size(72,18);
            this._lblParity.TabIndex = 123;
            this._lblParity.Text = "Parity";
            // 
            // _lblPortName
            // 
            this._lblPortName.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblPortName.Location = new System.Drawing.Point(6,92);
            this._lblPortName.Name = "_lblPortName";
            this._lblPortName.Size = new System.Drawing.Size(72,18);
            this._lblPortName.TabIndex = 122;
            this._lblPortName.Text = "Port";
            // 
            // _lblPrinterType
            // 
            this._lblPrinterType.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblPrinterType.Location = new System.Drawing.Point(6,9);
            this._lblPrinterType.Name = "_lblPrinterType";
            this._lblPrinterType.Size = new System.Drawing.Size(72,18);
            this._lblPrinterType.TabIndex = 133;
            this._lblPrinterType.Text = "Printer Type";
            // 
            // cboPrinter
            // 
            this.cboPrinter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPrinter.Font = new System.Drawing.Font("Verdana",8.25F,System.Drawing.FontStyle.Regular,System.Drawing.GraphicsUnit.Point,((byte)(0)));
            this.cboPrinter.Location = new System.Drawing.Point(6,30);
            this.cboPrinter.MaxDropDownItems = 4;
            this.cboPrinter.Name = "cboPrinter";
            this.cboPrinter.Size = new System.Drawing.Size(150,21);
            this.cboPrinter.TabIndex = 134;
            this.cboPrinter.SelectionChangeCommitted += new System.EventHandler(this.OnPrinterChanged);
            this.cboPrinter.Leave += new System.EventHandler(this.OnLeave);
            this.cboPrinter.Enter += new System.EventHandler(this.OnEnter);
            this.cboPrinter.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.cboPrinter.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            // 
            // LabelPrinters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboPrinter);
            this.Controls.Add(this._lblPrinterType);
            this.Controls.Add(this.lnkFindPrinter);
            this.Controls.Add(this.cboStopBits);
            this.Controls.Add(this.cboDataBits);
            this.Controls.Add(this.cboBaud);
            this.Controls.Add(this.cboParity);
            this.Controls.Add(this.cboPort);
            this.Controls.Add(this._lblBaud);
            this.Controls.Add(this._lblStopBits);
            this.Controls.Add(this._lblDataBits);
            this.Controls.Add(this._lblParity);
            this.Controls.Add(this._lblPortName);
            this.Name = "LabelPrinters";
            this.Size = new System.Drawing.Size(168,384);
            this.Load += new System.EventHandler(this.OnControlLoad);
            this.MouseLeave += new System.EventHandler(this.OnMouseLeave);
            this.Leave += new System.EventHandler(this.OnLeave);
            this.Enter += new System.EventHandler(this.OnEnter);
            this.MouseEnter += new System.EventHandler(this.OnMouseEnter);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.LinkLabel lnkFindPrinter;
        private System.Windows.Forms.ComboBox cboStopBits;
        private System.Windows.Forms.ComboBox cboDataBits;
        private System.Windows.Forms.ComboBox cboBaud;
        private System.Windows.Forms.ComboBox cboParity;
        private System.Windows.Forms.ComboBox cboPort;
        private System.Windows.Forms.Label _lblBaud;
        private System.Windows.Forms.Label _lblStopBits;
        private System.Windows.Forms.Label _lblDataBits;
        private System.Windows.Forms.Label _lblParity;
        private System.Windows.Forms.Label _lblPortName;
        private System.Windows.Forms.Label _lblPrinterType;
        private System.Windows.Forms.ComboBox cboPrinter;
    }
}
