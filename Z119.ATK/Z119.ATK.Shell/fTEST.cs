using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Z119.ATK.Shell
{
    public partial class fTEST : Form
    {
        Image myImageOrigianl;
        int zoom = 0;

        private Image Zoom(Image img, Size size)
        {

            Bitmap bmp = new Bitmap(img, img.Width + (img.Width * size.Width / 100), img.Height + (img.Height * size.Height / 100));
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            return bmp;
        }

        public fTEST()
        {
            InitializeComponent();

            myImageOrigianl = myImage1.Image;
            panel1.MouseWheel += Panel1_MouseWheel;
        }

        private void Panel1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (myImage1.Image == null)
                return;

            if (e.Delta > 0)
                zoom += 10;
            else
                zoom -= 10;

            if (zoom <= -90)
            {
                zoom = -90;
                return;
            }
            else if (zoom > 200)
            {
                zoom = 200;
                return;
            }

            Point p = new Point(myImage1.Location.X + (myImage1.Width * zoom / 100), myImage1.Location.Y + (myImage1.Height * zoom / 100));
            myImage1.Location = p;

            String detail = myImage1.Width + "";
            myImage1.Image = Zoom(myImageOrigianl, new Size(zoom, zoom));
            detail += " - " + myImage1.Width;
            this.Text = detail;
        }
    }
}
