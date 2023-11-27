using HNUDIP;
using ImageProcess2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebCamLib;

namespace DynamicImageProcessing
{
    public partial class Form1 : Form
    {
        Bitmap imageB, imageA, colorgreen;
        Device[] mgadevice;
        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            imageA = new Bitmap(openFileDialog1.FileName);
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            imageB = new Bitmap(openFileDialog2.FileName);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
            pictureBox2.Image = imageB;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Color mygreen = Color.FromArgb(0,255,0);
            int greygreen = (mygreen.R+mygreen.G+mygreen.B)/3;
            int threshold = 5;
            Bitmap resultImage = new Bitmap(imageA.Width,imageA.Height);
            for (int i = 0; i < imageA.Width; i++)
            {
                for (int j = 0; j < imageA.Height; j++)
                {
                    Color pixel = imageA.GetPixel(i, j);
                    Color backpixel = imageB.GetPixel(i, j);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);
                    if (subtractvalue > threshold)
                        resultImage.SetPixel(i, j, pixel);
                    else
                        resultImage.SetPixel(i, j, backpixel);
                }
            }
            pictureBox3.Image = resultImage;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mgadevice[0].ShowWindow(pictureBox1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mgadevice = DeviceManager.GetAllDevices();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            IDataObject data;
            Image bmap;
            mgadevice[0].Sendmessage();
            data = Clipboard.GetDataObject();
            bmap = (Image)(data.GetData("System.Drawing.Bitmap", true));
            Bitmap b = new Bitmap(bmap);
            // Subtract 
            Color mygreen = Color.FromArgb(0, 255, 0);
            int greygreen = (mygreen.R + mygreen.G + mygreen.B) / 3;
            int threshold = 15;
            Bitmap resultImage = new Bitmap(b.Width, b.Height);
            for (int i = 0; i < b.Width; i++)
            {
                for (int j = 0; j < b.Height; j++)
                {
                    Color pixel = b.GetPixel(i, j);
                    Color backpixel = imageB.GetPixel(i, j);
                    int grey = (pixel.R + pixel.G + pixel.B) / 3;
                    int subtractvalue = Math.Abs(grey - greygreen);
                    if (subtractvalue > threshold)
                        resultImage.SetPixel(i, j, pixel);
                    else
                        resultImage.SetPixel(i, j, backpixel);
                }
            }
            pictureBox3.Image = resultImage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mgadevice[0].Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.Image = imageA;
        }

    }
}
