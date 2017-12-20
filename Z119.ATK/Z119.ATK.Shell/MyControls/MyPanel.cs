using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Z119.ATK.Shell.MyControls
{
    [System.ComponentModel.DesignerCategory("Code")]
    public class MyPanel : Panel
    {
        public MyPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(Color.Black))
                e.Graphics.FillRectangle(brush, ClientRectangle);
            e.Graphics.DrawRectangle(new Pen(Color.Lime, 3), 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

            this.ForeColor = Color.Lime;

        }

    }
}
