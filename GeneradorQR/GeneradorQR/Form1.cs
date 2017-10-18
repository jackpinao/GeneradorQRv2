using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace GeneradorQR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            qrEncoder.TryEncode(txtValve.Text, out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400,QuietZoneModules.Zero),Brushes.Black,Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);
            var imageTemporal = new Bitmap(ms);
            var imagen = new Bitmap(imageTemporal, new Size(new Point(200, 200)));
            ResultPanel.BackgroundImage = imagen;

            //Guardar imagen en disco duro
            /*
            imagen.Save("imagen.png", ImageFormat.Png);
            */
            btnSave.Enabled = true;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Image imgFinal = (Image)ResultPanel.BackgroundImage.Clone();
            SaveFileDialog CajaDedialogoGuardar = new SaveFileDialog();
            CajaDedialogoGuardar.AddExtension = true;
            CajaDedialogoGuardar.Filter = "Image PNG (*.png)|*.png";
            CajaDedialogoGuardar.ShowDialog();
            if (!string.IsNullOrEmpty(CajaDedialogoGuardar.FileName))
            {
                imgFinal.Save(CajaDedialogoGuardar.FileName, ImageFormat.Png);
            }
            imgFinal.Dispose();
        }
    }
}
