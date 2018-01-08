using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
        List<schemePoint> schemePointList = new List<schemePoint>();
        ContextMenu cmNoselect, cmSelect;
        public fScheme()
        {
            InitializeComponent();
            InitGui();
            LoadschemePoints();
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseWheel);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(fScheme_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(fScheme_MouseUp);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(fScheme_MouseMove);
            
            cmNoselect = new ContextMenu();
            cmNoselect.MenuItems.Add("Đặt điểm đo", new EventHandler(fScheme_NewPoint));
            cmNoselect.MenuItems.Add("Lưu tất cả các điểm đo", new EventHandler(SavePointList));
            cmSelect = new ContextMenu();
            cmSelect.MenuItems.Add("Lấy giá trị điểm đo", new EventHandler(fScheme_SavePointData ));
            cmSelect.MenuItems.Add("Đặt giá trị tham chiếu", new EventHandler(fScheme_SavePointRefData));
            cmSelect.MenuItems.Add("Xóa điểm đo", new EventHandler(fScheme_DelPointData));
            //
            this.LocationChanged += fScheme_LocationChanged;
            this.ResizeEnd += fScheme_LocationChanged;
            this.StartPosition = FormStartPosition.Manual;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Location = //Z119.ATK.Common.Const.proConf.fSchemeLocation;
            //this.Size = //Z119.ATK.Common.Const.proConf.fSchemeSize;
        }

        private void fScheme_LocationChanged(object sender, EventArgs e)
        {
            Z119.ATK.Common.Const.proConf.fSchemeLocation = this.Location;
            Z119.ATK.Common.Const.proConf.fSchemeSize = this.Size;
        }

        private void fScheme_SavePointRefData(object sender, EventArgs e)
        {
            foreach (schemePoint p in schemePointList)
            {
                if (p.Selected)
                {
                    p.setRefData();
                    SavePointList();
                    break;
                }

            }
        }

        private void LoadschemePoints()
        {
            schemePointList = Z119.ATK.Common.ProjectManager.LoadObject<List<schemePoint>>(Z119.ATK.Common.Const.FILE_POINT_DATA);
            if (schemePointList == null) schemePointList = new List<schemePoint>();
        }

        private void SavePointList(object sender, EventArgs e)
        {
            SavePointList();
        }
        private void SavePointList()
        {
            Z119.ATK.Common.ProjectManager.SaveObject<List<schemePoint>>(schemePointList, Z119.ATK.Common.Const.FILE_POINT_DATA);
        }

        private void fScheme_DelPointData(object sender, EventArgs e)
        {
            foreach (schemePoint p in schemePointList)
            {
                if (p.Selected)
                {
                    schemePointList.Remove(p);
                    this.Refresh();
                    break;
                }

            }
        }


        private void fScheme_SavePointData(object sender, EventArgs e)
        {
            foreach (schemePoint p in schemePointList)
            {
                if (p.Selected)
                {
                    p.setMesData();
                    SavePointList();
                    break;
                }

            }
        
        }

        private void fScheme_NewPoint(object sender, EventArgs e)
        {
            var formPosition = pOld;
            schemePoint schemePos = new schemePoint(toSchemePos(formPosition));//
            schemePointList.Add(schemePos);
            this.Refresh();
        }

        private Point toSchemePos(Point formPosition)
        {
            return new Point(Convert.ToInt32((formPosition.X - imgdx) / zoomscale), Convert.ToInt32((formPosition.Y - imgdy) / zoomscale));
        }

        private void fScheme_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mousePressed)
            {
                pNew = this.panel1.PointToClient(Cursor.Position);
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
            pictureBox1.Focus();
            pOld = this.panel1.PointToClient(Cursor.Position);
            if ((e.Button == System.Windows.Forms.MouseButtons.Right)& CheckSelection(pOld))
            {
                pictureBox1.ContextMenu = cmSelect;
            }
            else 
            pictureBox1.ContextMenu = cmNoselect;
            this.Refresh();
        }

        private bool CheckSelection(Point checkPoint)
        {
            bool foundSeleted = false;
            foreach (schemePoint p in schemePointList)
            {
                Point center = toFormPos(p.Position);
                
                if ((Math.Abs(center.X - checkPoint.X) < 4)
                    & (Math.Abs(center.Y - checkPoint.Y) < 4)
                    &(!foundSeleted))
                {
                    p.Selected = true;
                    foundSeleted = true;
                    
                }
                else p.Selected = false;
                
            }
            return foundSeleted;
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
            this.panel1.Controls.Add(pictureBox1);
        }

        private void event_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Create a local version of the graphics object for the PictureBox.
            Graphics g = e.Graphics;
            g.DrawImage(image1, new Rectangle(imgdx, imgdy, Convert.ToInt32(image1.Width * zoomscale), Convert.ToInt32(image1.Height * zoomscale)));
            foreach (schemePoint p in schemePointList)
            {
                if (p.Selected)
                {
                    Point center = toFormPos(p.Position);
                    center.Offset(-5, -5);
                    g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(center, new Size(7, 7)));
                }
                else
                {
                    Point center = toFormPos(p.Position);
                    center.Offset(-3, -3);
                    g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(center, new Size(5, 5)));

                }
                
            }
            /*
            // Draw a string on the PictureBox.
            g.DrawString("This is a diagonal line drawn on the control",
                new Font("Arial", 10), System.Drawing.Brushes.Blue, new Point(30, 30));
            // Draw a line in the PictureBox.
            g.DrawLine(System.Drawing.Pens.Red, pictureBox1.Left, pictureBox1.Top,
                pictureBox1.Right, pictureBox1.Bottom);*/
        }

        private Point toFormPos(Point p)
        {
            return new Point(Convert.ToInt32(p.X * zoomscale + imgdx),Convert.ToInt32( p.Y * zoomscale + imgdy));
        }

        public void LoadScheme()
        {
            try
            {
                Bitmap image;
                string imgName = Z119.ATK.Common.Const.PATH_CURRENT + "\\" + "scheme.png";
                if(System.IO.File.Exists(imgName))
                {
                    image = (Bitmap)Image.FromFile(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + "scheme.png", true);
                }
                else
                {
                    Bitmap bmp = new Bitmap(1920, 1080);
                    using (Graphics graph = Graphics.FromImage(bmp))
                    {
                        Rectangle ImageSize = new Rectangle(0, 0, 1920, 1080);
                        graph.FillRectangle(Brushes.White, ImageSize);
                    }
                    bmp.Save(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + "scheme.png");
                    image = (Bitmap)Image.FromFile(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + "scheme.png", true);
                }
                this.image1 = new Bitmap(image);
                
            }
            catch (System.IO.FileNotFoundException)
            {
                MessageBox.Show("Không tìm thấy sơ đồ");
                
            }

        }
        public void LoadScheme(string path)
        {
            try
            {
                Bitmap image = (Bitmap)Image.FromFile(path, true);
                this.image1 = new Bitmap(image);
                File.Copy(path, Z119.ATK.Common.Const.PATH_CURRENT + "\\" + "scheme.png", true);
                //image1.Save(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + "scheme.png", ImageFormat.Png);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open image File";
            theDialog.Filter = "PNG files|*.png";
            theDialog.InitialDirectory = Z119.ATK.Common.Const.PATH_CURRENT;
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                LoadScheme(theDialog.FileName);
            }
        }

            
    }
    public class schemePoint
    {
        public Point Position;
        public bool selected;
        public int[] refData = new int[600];
        public int[] mesData = new int[600];
        public string scaleX = "";
        public string scaleY = "";

        private void DisplayDataToOscillo()
        {
            Array.Copy(refData, fOxiloForm.dataArrayRef, 600);
            Array.Copy(mesData, fOxiloForm.dataArrayOld, 600);

        }

        private void setParamToOscillo()
        {
            fOxiloForm.setScaleX(scaleX);
            fOxiloForm.setScaleY(scaleY);
        }
        public void setMesData()
        {
            Array.Copy( fOxiloForm.dataArray,mesData, 600);
            getParamFronOscillo();
            
            DisplayDataToOscillo();
        }

        private void getParamFronOscillo()
        {
            scaleX = fOxiloForm.getScaleX();
            scaleY = fOxiloForm.getScaleY();
        }
        public void setRefData()
        {
            Array.Copy(fOxiloForm.dataArray, refData, 600);
            getParamFronOscillo();
            DisplayDataToOscillo();
        }
        public bool Selected
        {
            get { return selected; }
            set 
            {
                if (selected == value) return;
                selected = value;
                if (selected)
                {
                    Form fc = Application.OpenForms["fOxiloForm"];

                    if (fc == null)
                    {
                        fOxiloForm foxilo = new fOxiloForm();
                        
                        foxilo.Show();
                    }
                    DisplayDataToOscillo();
                    setParamToOscillo();
                }
            }
        }
        public schemePoint(Point pos)
        {
            Position = pos;
        }
        public schemePoint()
        {

        }
    }
}
