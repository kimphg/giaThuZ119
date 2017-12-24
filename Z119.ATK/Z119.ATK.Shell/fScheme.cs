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
        public fScheme()
        {
            InitializeComponent();
            LoadScheme();
        }

        private void LoadScheme()
        {
            try
            {
                Bitmap image1 = (Bitmap)Image.FromFile(Z119.ATK.Common.Const.PATH_CURRENT+"\\"+"scheme.png", true);

                TextureBrush texture = new TextureBrush(image1);
                texture.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
                Graphics formGraphics = this.CreateGraphics();
                //formGraphics.FillEllipse(texture,
                  //  new RectangleF(90.0F, 110.0F, 100, 100));
                formGraphics.DrawImage(image1, new Rectangle(0, 0, image1.Width, image1.Height));
                //formGraphics.Dispose();

            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("There was an error opening the bitmap." +
                    "Please check the path.");
            }

        }
    }
}
