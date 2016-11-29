using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1 {
    //
    public partial class Form1 : Form {
        //Members
        private char[] mBarcode;

        //Interface
        public Form1() {
            InitializeComponent();
            this.mBarcode = this.textBox1.Text.ToCharArray();
            this.textBox1.Text = new Argix.Barcode().Encode128AB("L01440000102000001018880", "Code128A",Argix.Barcode128.A);
            this.picBarcode.Image = new Argix.Barcode().Encode128ABToImage("L01440000102000001018880", "Code128A", Argix.Barcode128.A);
        }
    }
}
