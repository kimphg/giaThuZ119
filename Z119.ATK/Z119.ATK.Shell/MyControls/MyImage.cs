using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Z119.ATK.Shell.MyControls
{
    public class MyImage : PictureBox
    {
        private Point firstPonint = new Point();
        public MyImage()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            Init();
        }

        public void Init()
        {
            this.Size = new Size(100, 50);

            this.MouseDown += (ss, ee) =>
            {
                if (ee.Button == MouseButtons.Left) { firstPonint = Control.MousePosition; }
            };

            this.MouseMove += (ss, ee) =>
            {
                if (ee.Button == MouseButtons.Left)
                {
                    // Create a temp ponint
                    Point temp = Control.MousePosition;
                    Point res = new Point(firstPonint.X - temp.X, firstPonint.Y - temp.Y);

                    // Apply value to object
                    this.Location = new Point(this.Location.X - res.X, this.Location.Y - res.Y);

                    // update first point
                    firstPonint = temp;
                }
            };
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    using (SolidBrush brush = new SolidBrush(Color.Black))
        //        //e.Graphics.FillRectangle(brush, ClientRectangle);
        //        e.Graphics.DrawRectangle(new Pen(Color.Black, 1), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);
        //}
    }
}

