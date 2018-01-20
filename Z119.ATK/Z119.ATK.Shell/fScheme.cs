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
using Z119.ATK.Common;

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
        
        ContextMenu cmNoselect, cmSelectPoint,cmSelectEle;
        string filename;
        public fScheme(int type)
        {
            InitializeComponent();
            InitGui();
            mType = type;
            if (type == 1)
            {
                this.label9.Text = "Sơ đồ nguyên lý";
                filename = "sodoNL.png";
                cmNoselect = new ContextMenu();
                cmNoselect.MenuItems.Add("Đặt điểm đo", new EventHandler(fScheme_NewPoint));
                cmNoselect.MenuItems.Add("Lưu tất cả các điểm đo", new EventHandler(SavePointList));
                cmSelectPoint = new ContextMenu();
                cmSelectPoint.MenuItems.Add("Lấy giá trị điểm đo", new EventHandler(fScheme_SavePointData));
                cmSelectPoint.MenuItems.Add("Đặt giá trị tham chiếu", new EventHandler(fScheme_SavePointRefData));
                cmSelectPoint.MenuItems.Add("Xóa điểm đo", new EventHandler(fScheme_DelPointData));
                cmSelectPoint.MenuItems.Add("Đặt vị trí trên sơ đồ lắp ráp", new EventHandler(fScheme_SetLRPoint));
                
            }
            else if(type == 2)
            {
                filename = "sodoLR.png";
                this.label9.Text = "Sơ đồ lắp ráp";
                cmNoselect = new ContextMenu();
                //cmNoselect.MenuItems.Add("Đặt điểm đo", new EventHandler(fScheme_NewPoint));
                cmNoselect.MenuItems.Add("Lưu tất cả các điểm đo", new EventHandler(SavePointList));
                cmNoselect.MenuItems.Add("Thêm linh kiện mới", new EventHandler(fScheme_NewElement));
                cmSelectPoint = new ContextMenu();
                cmSelectPoint.MenuItems.Add("Lấy giá trị điểm đo", new EventHandler(fScheme_SavePointData));
                cmSelectPoint.MenuItems.Add("Đặt giá trị tham chiếu", new EventHandler(fScheme_SavePointRefData));
                cmSelectPoint.MenuItems.Add("Xóa điểm đo", new EventHandler(fScheme_DelPointData));
                cmSelectEle = new ContextMenu();
                cmSelectEle.MenuItems.Add("Xóa linh kiện", new EventHandler(fScheme_DelElement));
                cmSelectEle.MenuItems.Add("Đặt tên", new EventHandler(fScheme_SetEleName));
                
            }
            LoadschemePoints();
            

            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseWheel);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(fScheme_MouseDown);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(fScheme_MouseUp);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(fScheme_MouseMove);
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseDoubleClick);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox1_MouseClick);
            //
            this.LocationChanged += fScheme_LocationChanged;
            this.ResizeEnd += fScheme_LocationChanged;
            this.StartPosition = FormStartPosition.Manual;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Location = //Z119.ATK.Common.Const.proConf.fSchemeLocation;
            //this.Size = //Z119.ATK.Common.Const.proConf.fSchemeSize;
        }

        private void fScheme_SetEleName(object sender, EventArgs e)
        {
            
            foreach (schemeElement ele in Const.schemeElementList)
            {
                if (ele.selected)
                {
                    ele.mName = this.getInputString("Nhập tên linh kiện");
                    return;
                }
            } 
        }

        private void fScheme_DelElement(object sender, EventArgs e)
        {
            foreach (schemeElement ele in Const.schemeElementList)
            {
                if (ele.selected)
                {
                    Const.schemeElementList.Remove(ele);
                }
            } 
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (isAddingElement)
            {
                pointElementBR = this.panel1.PointToClient(Cursor.Position);
                schemeElement newelement = new schemeElement(toSchemePos(pointElementTL), toSchemePos(pointElementBR));
                
                Const.schemeElementList.Add(newelement);
                isAddingElement = false;
            }
        }

        private void fScheme_NewElement(object sender, EventArgs e)
        {
            isAddingElement = true;
            pointElementTL = pOld;
        }

        void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            pOld = this.panel1.PointToClient(Cursor.Position);
            if(!CheckPointSelection(pOld))
                CheckElementSelection(pOld);
        }

        private bool CheckElementSelection(Point pSelect)
        {
            if (mType != 2) return false;
            bool foundSeleted = false;
            foreach (schemeElement ele in Const.schemeElementList)
            {
                Point br, tl;
                tl = toFormPos(ele.PositionTL);
                br = toFormPos(ele.PositionBR);
                if (pSelect.X > tl.X
                    && pSelect.X < br.X
                    && pSelect.Y > tl.Y
                    && pSelect.Y < br.Y
                    && (!foundSeleted)
                )
                {
                    if (!ele.selected)
                    {
                        ele.selected = true;

                    }
                    foundSeleted = true;
                }
                else ele.selected = false;
            }
            return foundSeleted;
        }

        private void fScheme_SetLRPoint(object sender, EventArgs e)
        {
            Z119.ATK.Common.Const.IsWaitForLRLocation = true;
        }

        private void fScheme_LocationChanged(object sender, EventArgs e)
        {
            Z119.ATK.Common.Const.proConf.fSchemeLocation = this.Location;
            Z119.ATK.Common.Const.proConf.fSchemeSize = this.Size;
        }

        private void fScheme_SavePointRefData(object sender, EventArgs e)
        {
            foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
            {
                if (p.Selected)
                {
                    p.setRefData(fOxiloForm.dataArray);
                    p.ScaleX = fOxiloForm.getScaleX();
                    p.ScaleY = fOxiloForm.getScaleY();
                    p.DisplayDataToOscillo(fOxiloForm.dataArrayRef, fOxiloForm.dataArrayOld, fOxiloForm.dataArray);
                    SavePointList();
                    break;
                }

            }
        }

        private void LoadschemePoints()
        {
            Const.schemePointList = Z119.ATK.Common.ProjectManager.LoadObject<List<schemePoint>>(Z119.ATK.Common.Const.FILE_POINT_DATA);
            if (Z119.ATK.Common.Const.schemePointList == null) Z119.ATK.Common.Const.schemePointList = new List<schemePoint>();
            Const.schemeElementList = Z119.ATK.Common.ProjectManager.LoadObject<List<schemeElement>>(Z119.ATK.Common.Const.FILE_ELE_DATA);
            if (Z119.ATK.Common.Const.schemeElementList == null) Z119.ATK.Common.Const.schemeElementList = new List<schemeElement>();
        }

        private void SavePointList(object sender, EventArgs e)
        {
            SavePointList();
        }
        private void SavePointList()
        {
            Z119.ATK.Common.ProjectManager.SaveObject<List<schemePoint>>(Z119.ATK.Common.Const.schemePointList, Z119.ATK.Common.Const.FILE_POINT_DATA);
            Z119.ATK.Common.ProjectManager.SaveObject<List<schemeElement>>(Z119.ATK.Common.Const.schemeElementList, Z119.ATK.Common.Const.FILE_ELE_DATA);
        }

        private void fScheme_DelPointData(object sender, EventArgs e)
        {
            foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
            {
                if (p.Selected)
                {
                    Z119.ATK.Common.Const.schemePointList.Remove(p);
                    this.Refresh();
                    break;
                }

            }
        }


        private void fScheme_SavePointData(object sender, EventArgs e)
        {
            foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
            {
                if (p.Selected)
                {
                    p.setMesData(fOxiloForm.dataArray);
                    p.ScaleX = fOxiloForm.getScaleX();
                    p.ScaleY = fOxiloForm.getScaleY();
                    p.DisplayDataToOscillo(fOxiloForm.dataArrayRef, fOxiloForm.dataArrayOld, fOxiloForm.dataArray);
                    SavePointList();
                    break;
                }

            }
        
        }

        private void fScheme_NewPoint(object sender, EventArgs e)
        {
            
            var formPosition = pOld;
            schemePoint schemePos = new schemePoint(toSchemePos(formPosition), getInputString("Nhập tên điểm đo"));//
            Z119.ATK.Common.Const.schemePointList.Add(schemePos);

            this.Refresh();
        }

        private string getInputString(string strtitle)
        {
            Form textDialog = new Form();
            textDialog.Text = strtitle;
            TextBox textbox = new TextBox();
            textDialog.Controls.Add(textbox);
            textbox.Location = new System.Drawing.Point(50, 30);
            Button button1 = new Button();
            button1.Text = "OK";
            textDialog.Controls.Add(button1);
            textDialog.AcceptButton = button1;
            button1.DialogResult = System.Windows.Forms.DialogResult.OK;
            button1.Location = new System.Drawing.Point(50, 60);
            string text = "";
            // Show testDialog as a modal dialog and determine if DialogResult = OK.
            if (textDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Read the contents of testDialog's TextBox.
                text = textbox.Text;
            }
            else
            {
                //this.txtResult.Text = "Cancelled";
            }
            textDialog.Dispose();

            return text;
        }

        private Point toSchemePos(Point formPosition)
        {
            return new Point(Convert.ToInt32((formPosition.X - imgdx) / zoomscale), Convert.ToInt32((formPosition.Y - imgdy) / zoomscale));
        }

        private void fScheme_MouseMove(object sender, MouseEventArgs e)
        {
            if (isAddingElement)
            {
                pointElementBR = this.panel1.PointToClient(Cursor.Position);
                this.Refresh();
                return;
            }
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
            pOld = this.panel1.PointToClient(Cursor.Position);
            if (isAddingElement)
            {
                return;
            }
            if (mType == 2)//adding element
            {
                if (Z119.ATK.Common.Const.IsWaitForLRLocation) 
                {
                    Z119.ATK.Common.Const.IsWaitForLRLocation = false;
                    foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
                    {
                        if (p.Selected)
                        {
                            p.setPosLR(toSchemePos(pOld));//
                        }

                    }
                }
            }
            pictureBox1.ContextMenu = cmNoselect;
            if ((e.Button == System.Windows.Forms.MouseButtons.Right) )
            {
                if (CheckPointSelection(pOld)) pictureBox1.ContextMenu = cmSelectPoint;
                else if (CheckElementSelection(pOld)) pictureBox1.ContextMenu = cmSelectEle;
            }
            
                
            _mousePressed = true;
            pictureBox1.Focus();
            
            this.Refresh();
        }
        fOxiloForm foxilo;
        public bool isPointChanged;
        
        private int mType;
        private bool isAddingElement;
        private Point pointElementBR;
        private Point pointElementTL;
        private bool CheckPointSelection(Point checkPoint)
        {
            bool foundSeleted = false;
            foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
            {
                Point center;
                if(mType==1) center = toFormPos(p.PositionNL);
                else center = toFormPos(p.PositionLR);
                
                if ((Math.Abs(center.X - checkPoint.X) < 5)
                    & (Math.Abs(center.Y - checkPoint.Y) < 5)
                    &(!foundSeleted))
                {
                    if (!p.selected)
                    {
                        p.Selected = true;
                        isPointChanged = true;
                        Form fc = Application.OpenForms["fOxiloForm"];

                        if (fc == null)
                        {
                            foxilo = new fOxiloForm();

                            foxilo.Show();
                        }
                        p.DisplayDataToOscillo(fOxiloForm.dataArrayRef, fOxiloForm.dataArrayOld, fOxiloForm.dataArray);
                        fOxiloForm.setScaleX(p.ScaleX);
                        fOxiloForm.setScaleY(p.ScaleY);
                    }
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
           
            if (mType == 2)// draw elements
            {
                foreach (schemeElement ele in Const.schemeElementList)
                {
                    Point br, tl;
                    tl = toFormPos(ele.PositionTL);
                    br = toFormPos(ele.PositionBR);
                    if (ele.selected)
                    {
                        g.DrawRectangle(new Pen(Color.Cyan, 4), new Rectangle(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y));
                        g.FillRectangle(new SolidBrush(Color.FromArgb(200, 120, 180, 210)), new Rectangle(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y));
                        tl.Offset(0,-15);
                        
                        Size size = e.Graphics.MeasureString(ele.mName, new System.Drawing.Font("Times New Roman", 10)).ToSize();
                        g.FillRectangle(new SolidBrush(Color.Cyan), new Rectangle(tl,size));
                        g.DrawString(ele.mName, new System.Drawing.Font("Times New Roman", 10), new System.Drawing.SolidBrush(Color.Black), tl);
                    }
                    else
                    {
                        g.DrawRectangle(new Pen(Color.Cyan, 2), new Rectangle(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y));
                        g.FillRectangle(new SolidBrush(Color.FromArgb(150, 150, 150, 150)), new Rectangle(tl.X, tl.Y, br.X - tl.X, br.Y - tl.Y));
                        tl.Offset(0, -15);
                        Size size = e.Graphics.MeasureString(ele.mName, new System.Drawing.Font("Times New Roman", 10)).ToSize();
                        g.FillRectangle(new SolidBrush(Color.Cyan), new Rectangle(tl, size));
                        g.DrawString(ele.mName, new System.Drawing.Font("Times New Roman", 10), new System.Drawing.SolidBrush(Color.Black), tl);
                    }
                    
                }
            }
            // draw points
            foreach (schemePoint p in Z119.ATK.Common.Const.schemePointList)
            {
                Point center;
                if (mType == 1) center = toFormPos(p.PositionNL);
                else center = toFormPos(p.PositionLR);
                if (p.Selected)
                {
                    center.Offset(-5, -5);
                    g.DrawRectangle(new Pen(Color.Red, 4), new Rectangle(center, new Size(7, 7)));
                    center.Offset(5, 5);
                    g.DrawString(p.MName, new System.Drawing.Font("Times New Roman", 10), new System.Drawing.SolidBrush(Color.Red), center);
                }
                else
                {
                    center.Offset(-3, -3);
                    g.DrawRectangle(new Pen(Color.Red, 2), new Rectangle(center, new Size(5, 5)));
                    center.Offset(3, 3);
                    g.DrawString(p.MName, new System.Drawing.Font("Times New Roman", 10), new System.Drawing.SolidBrush(Color.Red), center);
                }
            }
            if(isAddingElement)
            {
                g.DrawRectangle(new Pen(Color.Black, 2), new Rectangle(pointElementTL.X,pointElementTL.Y, pointElementBR.X - pointElementTL.X, pointElementBR.Y - pointElementTL.Y));
            }
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
                string imgName = Z119.ATK.Common.Const.PATH_CURRENT + "\\" + filename;
                if(System.IO.File.Exists(imgName))
                {
                    image = (Bitmap)Image.FromFile(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + filename, true);
                }
                else
                {
                    Bitmap bmp = new Bitmap(1920, 1080);
                    using (Graphics graph = Graphics.FromImage(bmp))
                    {
                        Rectangle ImageSize = new Rectangle(0, 0, 1920, 1080);
                        graph.FillRectangle(Brushes.White, ImageSize);
                    }
                    bmp.Save(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + filename);
                    image = (Bitmap)Image.FromFile(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + filename, true);
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
                File.Copy(path, Z119.ATK.Common.Const.PATH_CURRENT + "\\" + filename, true);
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(mType==2)foreach (schemePoint p in Common.Const.schemePointList)
            {
                if (p.selected) CheckElementSelection(toFormPos(p.PositionLR));
            }
            Refresh(); 
        }

            
    }
    
}
