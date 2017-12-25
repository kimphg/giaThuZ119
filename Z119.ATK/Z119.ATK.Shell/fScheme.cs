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
    public partial class fScheme : Form
    {
        Bitmap image1;
        private PictureBox pictureBox1 = new PictureBox();
        private double zoomscale=1.0;
        private int imgdx=0, imgdy=0;
        bool _mousePressed=false;
        Point pOld,pNew;
        public fScheme()
        {
            InitializeComponent();
            InitGui();

            this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseWheel);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(fScheme_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(fScheme_MouseUp);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(fScheme_MouseMove);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(fScheme_NewPoint);
            ContextMenu cm = new ContextMenu();

            cm.MenuItems.Add("Đặt điểm đo", new EventHandler(fScheme_NewPoint));

            pictureBox1.ContextMenu = cm;
        }

        private void fScheme_NewPoint(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

       

        
        private void fScheme_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mousePressed)
            {
                pNew = e.Location;
                imgdx += pNew.X - pOld.X;
                imgdy += pNew.Y - pOld.Y;
                pOld = pNew;
                this.Refresh();
            }
        }

        private void fScheme_MouseUp(object sender, MouseEventArgs e)
        {
            _mousePressed = false;

        }

        private void fScheme_MouseDown(object sender, MouseEventArgs e)
        {
            _mousePressed = true;
            pOld = e.Location;

        }
        

        private void pictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0) zoomscale *= 1.2;
            else if (e.Delta < 0) zoomscale /= 1.2;
            this.Refresh();
        }

        private void InitGui()
        {

            // Dock the PictureBox to the form and set its background to white.
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.BackColor = Color.White;
            // Connect the Paint event of the PictureBox to the event handler method.
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.event_Paint);

            // Add the PictureBox control to the Form.
            this.Controls.Add(pictureBox1);
        }

        private void event_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            g.DrawImage(image1, new Rectangle(imgdx, imgdy, Convert.ToInt32(image1.Width * zoomscale), Convert.ToInt32(image1.Height * zoomscale)));
            /*
            // Draw a string on the PictureBox.
            g.DrawString("This is a diagonal line drawn on the control",
                new Font("Arial", 10), System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            g.DrawLine(System.Drawing.Pens.Red, pictureBox1.Left, pictureBox1.Top,
                pictureBox1.Right, pictureBox1.Bottom);*/
        }

        public void LoadScheme()
        {
            try
            {
                image1 = (Bitmap)Image.FromFile(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + "scheme.png", true);
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Không tìm thấy sơ đồ.");
            }

        }
    }
}
