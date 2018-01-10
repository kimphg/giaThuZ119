using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Z119.ATK.Model.BindingModel;
using Z119.ATK.Switch;

namespace Z119.ATK.Shell
{
	public partial class fSwitchForm : Form
	{
        SwitchManager _switchManager;
        SwitchBindingModel Model;
        string portName;
        bool isConnection = false;

		public fSwitchForm()
		{
			InitializeComponent();
            this.portName = Z119.ATK.Common.Const.proConf.switchCtrl;
            _switchManager = new SwitchManager();
            _switchManager.mode = Mode.None;
            _switchManager.IsOn = false;

            Model = new SwitchBindingModel();
            Model.ListFrame = new List<List<byte>>();
            Model.ListChan = new List<string>();
            
            initialize_DataGridView1();

            btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
            btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
            this.LoadData();
            ConnectCOMPort();
        }

        #region Methods *************************

        public void initialize_DataGridView1()
        {
            // dgvSwitch
            dgvSwitch.AutoSize = true;
            dgvSwitch.Location = new Point(10, 10);
            dgvSwitch.Font = new System.Drawing.Font("Tohoma", 12, FontStyle.Bold); // Set Font
            dgvSwitch.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // căn giữa cột header
            dgvSwitch.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Căn giữa cell
            dgvSwitch.RowHeadersVisible = false; // Not Desplayed First columns header
            dgvSwitch.AllowUserToResizeColumns = false; // Không cho phép thay đổi kích thước cột
            dgvSwitch.AllowUserToResizeRows = false; // Không cho phép thay đổi kích thước dòng
            dgvSwitch.RowTemplate.Height = 30; // Set chiều cao từng dòng trong datagridview
            dgvSwitch.ColumnHeadersHeight = 35; // // Set chiều cao cột tiêu đề
            dgvSwitch.AllowUserToAddRows = false; // Bỏ dòng cuối trong DataGridview

            this.dgvSwitch.MultiSelect = false; // Không cho chọn nhiều dòng trong dataGridView

            DataGridViewTextBoxColumn col1 = new DataGridViewTextBoxColumn()
            {
                HeaderText = "Stt",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 32,
                ReadOnly = true

            };
            DataGridViewImageColumn col2 = new DataGridViewImageColumn()
            {
                HeaderText = "Data",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                Width = 100,
                Image = imageList1.Images["value6.png"],
                ImageLayout = DataGridViewImageCellLayout.Stretch
            };

            //DataGridViewTextBoxColumn col2 = new DataGridViewTextBoxColumn()
            //{
            //    HeaderText = "Data",
            //    AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
            //    Width = 70
            //};

            dgvSwitch.Columns.AddRange(new DataGridViewColumn[] { col1, col2 });

            foreach (DataGridViewColumn item in this.dgvSwitch.Columns) // Tắt tính năng sort của dataGridView
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            LoadFootForDataGridView(int.Parse(txbSoChan.Text));
        }

        public void LoadFootForDataGridView(int chan)
        {
            dgvSwitch.ClearSelection();
            
            dgvSwitch.RowCount = chan;

            // Set order for foot
            for (int i = 0; i < chan; i++)
            {
                dgvSwitch.Rows[i].Cells[0].Value = (i + 1).ToString();
            }

        } 

        public void ConnectCOMPort()
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = Z119.ATK.Common.Const.proConf.switchCtrl;
                    serialPort1.Open();
                    _switchManager.IsOn = true;
                }
            }
            catch
            {
                //MessageBox.Show("Không thể mở cổng " + serialPort1.PortName, "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnOnOff.Enabled = false;
                _switchManager.IsOn = false;
            }

        }

        public void DisConnectCOMPort()
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    _switchManager.IsOn = false;
                }
            }
            catch (Exception)
            { }
        }

        public List<string> GetDataStringFromDataGridView()
        {
            List<string> lstString = new List<string>();
            int row = dgvSwitch.Rows.Count;

            for (int i = 0; i < row; i++)
            {
                lstString.Add(dgvSwitch[1, i].Tag + "");
            }

            return lstString;
        }

        public List<List<byte>> GetDataByteFromDataGridView()
        {
            List<List<byte>> frame = new List<List<byte>>();

            byte port1 = 0x00;
            byte port21 = 0x00;
            byte port22 = 0x00;
            byte port31 = 0x00;
            byte port32 = 0x00;
            byte port4 = 0x00;
            byte port5 = 0x00;

            int Dong = dgvSwitch.RowCount;

            int NFrame = Dong / 5;// =>> Số frame có đủ 5 chân được sét

            // Xét fram đều đủ 5 chân
            int index = 0; // Số frame đã set đủ 5 chân
            for (int i = 0; i < Dong; i++)
            {
                string result = dgvSwitch.Rows[i].Cells[1].Tag == null ? "" : dgvSwitch.Rows[i].Cells[1].Tag.ToString();
                
                #region Set giá trị cho chân đủ 5 chân

                int Chan = i % 5;

                switch (Chan)
                {
                    case 0: //  Chân 1
                        if (result.Equals("U4"))
                        {
                            port1 = 0x01;
                        }
                        else if (result.Equals("U2"))
                        {
                            port1 = 0x02;
                        }
                        else if (result.Equals("U1"))
                        {
                            port1 = 0x04;
                        }
                        else if (result.Equals("Đất"))
                        {
                            port1 = 0x08;
                        }
                        else if (result.Equals("U3"))
                        {
                            port1 = 0x10;
                        }
                        else if (result.Equals("U ra"))
                        {
                            port1 = 0x20;
                        }
                        else if (result.Equals(""))
                        {
                            port1 = 0x00;
                        }
                        break;
                    case 1: // Chân 2
                        if (result.Equals("U4"))
                        {
                            port21 = 0x04;
                        }
                        else if (result.Equals("U2"))
                        {
                            port21 = 0x08;
                        }
                        else if (result.Equals("U1"))
                        {
                            port21 = 0x10;
                        }
                        else if (result.Equals("Đất"))
                        {
                            port21 = 0x20;
                        }
                        else if (result.Equals("U3"))
                        {
                            port21 = 0x40;
                        }
                        else if (result.Equals("U ra"))
                        {
                            port21 = 0x80;
                        }
                        else if (result.Equals(""))
                        {
                            port21 = 0x00;
                        }
                        break;
                    case 2: // Chân 3
                        if (result.Equals("U4"))
                        {
                            port22 = 0x00;
                            port31 = 0x10;
                        }
                        else if (result.Equals("U2"))
                        {
                            port22 = 0x00;
                            port31 = 0x20;
                        }
                        else if (result.Equals("U1"))
                        {
                            port22 = 0x00;
                            port31 = 0x40;
                        }
                        else if (result.Equals("Đất"))
                        {
                            port22 = 0x00;
                            port31 = 0x80;
                        }
                        else if (result.Equals("U3"))
                        {
                            port22 = 0x01;
                            port31 = 0x00;
                        }
                        else if (result.Equals("U ra"))
                        {
                            port22 = 0x02;
                            port31 = 0x00;
                        }
                        else if (result.Equals(""))
                        {
                            port22 = 0x00;
                            port31 = 0x00;
                        }
                        break;
                    case 3: // Chân 4
                        if (result.Equals("U4"))
                        {
                            port32 = 0x00;
                            port4 = 0x40;
                        }
                        else if (result.Equals("U2"))
                        {
                            port32 = 0x00;
                            port4 = 0x80;
                        }
                        else if (result.Equals("U1"))
                        {
                            port32 = 0x01;
                            port4 = 0x00;
                        }
                        else if (result.Equals("Đất"))
                        {
                            port32 = 0x02;
                            port4 = 0x00;
                        }
                        else if (result.Equals("U3"))
                        {
                            port32 = 0x04;
                            port4 = 0x00;
                        }
                        else if (result.Equals("U ra"))
                        {
                            port32 = 0x08;
                            port4 = 0x00;
                        }
                        else if (result.Equals(""))
                        {
                            port4 = 0x00;
                        }
                        break;
                    case 4: // Chân 5
                        if (result.Equals("U4"))
                        {
                            port5 = 0x01;
                        }
                        else if (result.Equals("U2"))
                        {
                            port5 = 0x02;
                        }
                        else if (result.Equals("U1"))
                        {
                            port5 = 0x04;
                        }
                        else if (result.Equals("Đất"))
                        {
                            port5 = 0x08;
                        }
                        else if (result.Equals("U3"))
                        {
                            port5 = 0x10;
                        }
                        else if (result.Equals("U ra"))
                        {
                            port5 = 0x20;
                        }
                        else if (result.Equals(""))
                        {
                            port5 = 0x00;
                        }
                        break;
                    default:
                        break;
                }

                if ((i + 1) % 5 == 0 && i != 0) // Hết 1 frame
                {
                    frame.Add(new List<byte>());

                    byte data1 = port1;
                    byte data2 = (byte)(port22 | port21);
                    byte data3 = (byte)(port32 | port31);
                    byte data4 = (byte)(port5 | port4);

                    frame[index].Add(data1);
                    frame[index].Add(data2);
                    frame[index].Add(data3);
                    frame[index].Add(data4);
                    index++;
                }
            }

            #endregion
            // index : Số frame đã set đủ 5 chân

            #region Set gia trị cho frame thiếu chân

            int SoChanConLai = Dong - (index * 5); // Số chân còn lại luôn luôn nhỏ hơn 5
            if (SoChanConLai > 0)
            {
                for (int i = 0; i < SoChanConLai; i++)
                {
                    string result = dgvSwitch.Rows[(index * 5) + i].Cells[1].Tag == null ? "" : dgvSwitch.Rows[(index * 5) + i].Cells[1].Tag.ToString();

                    #region Set giá trị cho chân

                    int Chan = i % 5;

                    switch (Chan)
                    {
                        case 0: //  Chân 1
                            if (result.Equals("U4"))
                            {
                                port5 = 0x01;
                            }
                            else if (result.Equals("U2"))
                            {
                                port5 = 0x02;
                            }
                            else if (result.Equals("U1"))
                            {
                                port5 = 0x04;
                            }
                            else if (result.Equals("Đất"))
                            {
                                port5 = 0x08;
                            }
                            else if (result.Equals("U3"))
                            {
                                port5 = 0x10;
                            }
                            else if (result.Equals("U ra"))
                            {
                                port5 = 0x20;
                            }
                            else if (result.Equals(""))
                            {
                                port5 = 0x00;
                            }
                            break;
                        case 1: // Chân 2
                            if (result.Equals("U4"))
                            {
                                port21 = 0x04;
                            }
                            else if (result.Equals("U2"))
                            {
                                port21 = 0x08;
                            }
                            else if (result.Equals("U1"))
                            {
                                port21 = 0x10;
                            }
                            else if (result.Equals("Đất"))
                            {
                                port21 = 0x20;
                            }
                            else if (result.Equals("U3"))
                            {
                                port21 = 0x40;
                            }
                            else if (result.Equals("U ra"))
                            {
                                port21 = 0x80;
                            }
                            else if (result.Equals(""))
                            {
                                port21 = 0x00;
                            }
                            break;
                        case 2: // Chân 3
                            if (result.Equals("U4"))
                            {
                                port22 = 0x00;
                                port31 = 0x10;
                            }
                            else if (result.Equals("U2"))
                            {
                                port22 = 0x00;
                                port31 = 0x20;
                            }
                            else if (result.Equals("U1"))
                            {
                                port22 = 0x00;
                                port31 = 0x40;
                            }
                            else if (result.Equals("Đất"))
                            {
                                port22 = 0x00;
                                port31 = 0x80;
                            }
                            else if (result.Equals("U3"))
                            {
                                port22 = 0x01;
                                port31 = 0x00;
                            }
                            else if (result.Equals("U ra"))
                            {
                                port22 = 0x02;
                                port31 = 0x00;
                            }
                            else if (result.Equals(""))
                            {
                                port22 = 0x00;
                                port31 = 0x00;
                            }
                            break;
                        case 3: // Chân 4
                            if (result.Equals("U4"))
                            {
                                port32 = 0x00;
                                port4 = 0x40;
                            }
                            else if (result.Equals("U2"))
                            {
                                port32 = 0x00;
                                port4 = 0x80;
                            }
                            else if (result.Equals("U1"))
                            {
                                port32 = 0x01;
                                port4 = 0x00;
                            }
                            else if (result.Equals("Đất"))
                            {
                                port32 = 0x02;
                                port4 = 0x00;
                            }
                            else if (result.Equals("U3"))
                            {
                                port32 = 0x04;
                                port4 = 0x00;
                            }
                            else if (result.Equals("U ra"))
                            {
                                port32 = 0x08;
                                port4 = 0x00;
                            }
                            else if (result.Equals(""))
                            {
                                port4 = 0x00;
                            }
                            break;
                        case 4: // Chân 5
                            if (result.Equals("U4"))
                            {
                                port5 = 0x01;
                            }
                            else if (result.Equals("U2"))
                            {
                                port5 = 0x02;
                            }
                            else if (result.Equals("U1"))
                            {
                                port5 = 0x04;
                            }
                            else if (result.Equals("Đất"))
                            {
                                port5 = 0x08;
                            }
                            else if (result.Equals("U3"))
                            {
                                port5 = 0x10;
                            }
                            else if (result.Equals("U ra"))
                            {
                                port5 = 0x20;
                            }
                            else if (result.Equals(""))
                            {
                                port5 = 0x00;
                            }
                            break;
                        default:
                            break;
                    }

                    
                }
                int ChanFF = 5 - SoChanConLai;

                for (int i = 0; i < ChanFF; i++) // Đều là FF
                {
                    int Chan = (SoChanConLai + i) % 5;

                    switch (Chan)
                    {
                        case 0: //  Chân 1
                            port5 = 0x00;
                            break;
                        case 1: // Chân 2
                            port21 = 0x00;
                            port22 = 0x00;
                            break;
                        case 2: // Chân 3
                            port31 = 0x00;
                            port32 = 0x00;
                            break;
                        case 3: // Chân 4
                            port4 = 0x00;
                            break;
                        case 4: // Chân 5
                            //port5 = 0x00;
                            port1 = 0x00;
                            break;
                        default:
                            break;
                    }
                }

                // Xử lý ở đây Frame cuối cùng trong datagridview

                frame.Add(new List<byte>());

                byte data1 = port1;
                byte data2 = (byte)(port22 | port21);
                byte data3 = (byte)(port32 | port31);
                byte data4 = (byte)(port5 | port4);

                frame[index].Add(data1);
                frame[index].Add(data2);
                frame[index].Add(data3);
                frame[index].Add(data4);
            }
            #endregion
            #endregion
            byte address = 0x51;

            // Thêm địa chỉ đầu và cuối cho các frame trong datagridview
            for (int i = 0; i < frame.Count; i++)
            {
                frame[i].Insert(0, 0xAA);
                frame[i].Insert(1, address);// khác nhau ở đây
                frame[i].Add(0x55);
                address++;
            }

            // Xét Frame thiếu vì có tất cả là 7Frame mà số chân tối đa là 31
            int NfareHienTai = frame.Count; // Tất cả chỉ có 7 frame
            for (int i = NfareHienTai; i < 7; i++)
            {
                frame.Add(new List<byte>());
                for (int j = 0; j < 4; j++)
                {
                    frame[i].Add(0x00);
                }
            }

            // Thêm địa chỉ đầu và cuối
            for (int i = NfareHienTai; i < 7; i++)
            {
                frame[i].Insert(0, 0xAA);
                frame[i].Insert(1, address);// khác nhau ở đây
                frame[i].Add(0x55);
                address++;
            }

            return frame;
        }

        public void InputDataFromDataGridView(SwitchBindingModel model)
        {
            model.ListFrame = GetDataByteFromDataGridView();
            model.ListChan = GetDataStringFromDataGridView();
        }

        public void InputDataIntoDGVFromModel(SwitchBindingModel model)
        {
            int row = model.ListChan.Count;
            for (int i = 0; i < row; i++)
            { 
                dgvSwitch.Rows[i].Cells[1].Tag = model.ListChan[i];
                switch (model.ListChan[i])
                {
                    case "":
                        dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value6.png"];
                        break;
                    case "U1":
                        dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value0.png"];
                        break;
                    case "U2":
                        dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value1.png"];
                        break;
                    case "U3":
                        dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value2.png"];
                        break;
                    case "U4":
                        dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value3.png"];
                        break;
                    case "Đất":
                        dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value4.png"];
                        break;
                    case "U ra":
                        dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value5.png"];
                        break;
                    default:
                        break;
                }
            }
        }

        public bool IsOnPower = false;
        public void OnOffPower()
        {
            if (IsOnPower)
            {
                #region GUI *************************

                btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
                IsOnPower = false;

                #endregion End GUI ***************************

                #region Handling **********************

                if (!_switchManager.IsOn)
                    return;

                List<List<byte>> dataOff = new List<List<byte>>();

                for (int i = 0; i < 7; i++)
                {
                    dataOff.Add(new List<byte>());
                    for (int j = 0; j < 4; j++)
                    {
                        dataOff[i].Add(0x00);
                    }
                }
                // Thêm địa chỉ đầu và cuối
                byte address = 0x51;
                for (int i = 0; i < 7; i++)
                {
                    dataOff[i].Insert(0, 0xAA);
                    dataOff[i].Insert(1, address);// khác nhau ở đây
                    dataOff[i].Add(0x55);
                    address++;
                }

                try
                {
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            try
                            {
                                serialPort1.Write(dataOff[i].ToArray(), 0, 7);
                            }
                            catch (Exception)
                            {
                                btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                                btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
                                DisConnectCOMPort();
                                IsOnPower = false;
                                return;
                            }
                            
                            Thread.Sleep(10);
                        }
                    });
                }
                catch (Exception)
                {
                    btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                    btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
                    DisConnectCOMPort();
                    IsOnPower = false;
                    return;
                }

                #endregion End Handling *******************

            }
            else
            {
                #region GUI *******************

                btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_ON);
                btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
                IsOnPower = true;

                #endregion End GUI *******************

                #region Handling *******************
                if (!_switchManager.IsOn)
                    return;

                InputDataFromDataGridView(Model);

                try
                {
                    Task.Run(() =>
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            try
                            {
                                serialPort1.Write(Model.ListFrame[i].ToArray(), 0, 7);
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Có lỗi trong khi gửi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                DisConnectCOMPort();
                                btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                                btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
                                IsOnPower = false;
                                return;
                            }
                            
                            Thread.Sleep(10);
                        }
                    });


                }
                catch (Exception)
                {
                    MessageBox.Show("Có lỗi trong khi gửi", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DisConnectCOMPort();
                    btnOnOff.BackgroundImage = Image.FromFile(Z119.ATK.Common.Const.ICON_POWER_OFF);
                    btnOnOff.BackgroundImageLayout = ImageLayout.Stretch;
                    IsOnPower = false;
                    return;
                }
                #endregion End Handling ***********************


            }
        }

        #endregion End Methods ************************

        #region Events *************************

        private void btnU1Am_Click(object sender, EventArgs e)
        {
            foreach (var item in groupBox1.Controls)
            {
                Button button = item as Button;
                //button.BackColor = Color.LightGray;
            }

            Button btn = sender as Button;
            //btn.BackColor = Color.GreenYellow;
            if (btn.Text.Equals("U3"))
            {
                _switchManager.mode = Mode.U3;
                //btn.BackColor = Color.FromArgb(189, 228, 101);
                this.Cursor = CreateCursor((Bitmap)imageList1.Images[2], new Size(70, 70));
            }
            else if (btn.Text.Equals("U2"))
            {
                _switchManager.mode = Mode.U2;
                //btn.BackColor = Color.FromArgb(189, 228, 101);
                this.Cursor = CreateCursor((Bitmap)imageList1.Images[1], new Size(70, 70));
            }
            else if (btn.Text.Equals("+U1"))
            {
                _switchManager.mode = Mode.U1;
                //btn.BackColor = Color.FromArgb(189, 228, 101);
                this.Cursor = CreateCursor((Bitmap)imageList1.Images[0], new Size(70, 70));
            }
            else if (btn.Text.Equals("Đất"))
            {
                _switchManager.mode = Mode.DAT;
                //btn.BackColor = Color.FromArgb(189, 228, 101);
                this.Cursor = CreateCursor((Bitmap)imageList1.Images[4], new Size(70, 70));
            }
            else if (btn.Text.Equals("U4"))
            {
                _switchManager.mode = Mode.U4;
                //btn.BackColor = Color.FromArgb(189, 228, 101);
                this.Cursor = CreateCursor((Bitmap)imageList1.Images[3], new Size(70, 70));
            }
            else if (btn.Text.Equals("-U1"))
            {
                _switchManager.mode = Mode.URA;
                //btn.BackColor = Color.FromArgb(189, 228, 101);
                this.Cursor = CreateCursor((Bitmap)imageList1.Images[5], new Size(70, 70));
            }
            else if (btn.Text.Equals("Bỏ chọn"))
            {
                _switchManager.mode = Mode.None;
                //btn.BackColor = Color.FromArgb(189, 228, 101);
                this.Cursor = CreateCursor((Bitmap)imageList1.Images[6], new Size(70, 70));
            }
        }

        private void dgvSwitch_Click_1(object sender, EventArgs e)
        {
            
            int row;
            try
            {
                // Lấy ra ô được chọn
                row = this.dgvSwitch.CurrentCell.RowIndex; // Dòng được chọn

                switch (_switchManager.mode)
                {
                    case Mode.None:
                        dgvSwitch.Rows[row].Cells[1].Value = imageList1.Images["value6.png"];
                        dgvSwitch.Rows[row].Cells[1].Tag = "";
                        dgvSwitch.Rows[row].Cells[1].Style.BackColor = Color.White;
                        dgvSwitch.Rows[row].Cells[0].Style.ForeColor = Color.Black;
                        
                        break;
                    case Mode.U1:
                        dgvSwitch.Rows[row].Cells[1].Value = imageList1.Images["value0.png"];
                        dgvSwitch.Rows[row].Cells[1].Tag = "U1";
                        dgvSwitch.Rows[row].Cells[1].Style.BackColor = Color.FromArgb(64, 0, 64);
                        dgvSwitch.Rows[row].Cells[0].Style.ForeColor = Color.FromArgb(64,0,64);
                        break;
                    case Mode.U2:
                        dgvSwitch.Rows[row].Cells[1].Value = imageList1.Images["value1.png"];
                        dgvSwitch.Rows[row].Cells[1].Tag = "U2";
                        dgvSwitch.Rows[row].Cells[1].Style.BackColor = Color.FromArgb(0, 192, 0);
                        dgvSwitch.Rows[row].Cells[0].Style.ForeColor = Color.FromArgb(0, 192, 0);
                        break;
                    case Mode.U3:
                        dgvSwitch.Rows[row].Cells[1].Value = imageList1.Images["value2.png"];
                        dgvSwitch.Rows[row].Cells[1].Tag = "U3";
                        dgvSwitch.Rows[row].Cells[1].Style.BackColor = Color.Blue;
                        dgvSwitch.Rows[row].Cells[0].Style.ForeColor = Color.Blue;
                        break;
                    case Mode.U4:
                        dgvSwitch.Rows[row].Cells[1].Value = imageList1.Images["value3.png"];
                        dgvSwitch.Rows[row].Cells[1].Tag = "U4";
                        dgvSwitch.Rows[row].Cells[1].Style.BackColor = Color.FromArgb(192,0,192);
                       dgvSwitch.Rows[row].Cells[0].Style.ForeColor = Color.FromArgb(192, 0, 192);
                        break;
                    case Mode.DAT:
                        dgvSwitch.Rows[row].Cells[1].Value = imageList1.Images["value4.png"];
                        dgvSwitch.Rows[row].Cells[1].Tag = "Đất";
                        dgvSwitch.Rows[row].Cells[1].Style.BackColor = Color.Olive;
                        dgvSwitch.Rows[row].Cells[0].Style.ForeColor = Color.Olive;
                        break;
                    case Mode.URA:
                        dgvSwitch.Rows[row].Cells[1].Value = imageList1.Images["value5.png"];
                        dgvSwitch.Rows[row].Cells[1].Tag = "U ra";
                        dgvSwitch.Rows[row].Cells[1].Style.BackColor = Color.Red;
                        dgvSwitch.Rows[row].Cells[0].Style.ForeColor = Color.Red;
                        break;
                    default:
                        return;
                }

            }
            catch { }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            int rowCount = dgvSwitch.Rows.Count;
            for (int i = 0; i < rowCount; i++)
            {
                dgvSwitch.Rows[i].Cells[1].Value = imageList1.Images["value6.png"];
                dgvSwitch.Rows[i].Cells[1].Tag = "";
                dgvSwitch.Rows[i].Cells[1].Style.BackColor = Color.LightGray;
                dgvSwitch.Rows[i].Cells[0].Style.ForeColor = Color.Black;
            }
        }

        private void btnOnOff_Click(object sender, EventArgs e)
        {
            OnOffPower();
        }


        private void fSwitchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsOnPower = true;
            OnOffPower();
            DisConnectCOMPort();
        }

        #endregion End Events *******************************

        private void mởTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadData();
        }
        public void SaveData()
        {
            SwitchBindingModel model = new SwitchBindingModel();
            InputDataFromDataGridView(model);
            _switchManager.SaveIntoFile(model);
        }
        public void LoadData()
        {
            SwitchBindingModel model = _switchManager.OpenFile();
            if (model != null)
                InputDataIntoDGVFromModel(model);//????
        }
        private void lưuTậpTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        public bool isRunServer = false;

        public void RunFromServer()
        {
            if (isRunServer) // dang on thi off
            {
                IsOnPower = true;
                OnOffPower();
                isRunServer = false;
            }
            else
            {
                IsOnPower = false;
                OnOffPower();
                isRunServer = true;
            }
        }

        public static Cursor CreateCursor(Bitmap bm, Size size)
        {
            bm = new Bitmap(bm, size);
            bm.MakeTransparent();
            return new Cursor(bm.GetHicon());
        }

        private void thuNhỏToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((sender as ToolStripMenuItem).Text == "Thu nhỏ")
            {
                this.Height = 480;
                (sender as ToolStripMenuItem).Text = "Mở rộng";
            }
            else
            {
                this.Height = 700;
                (sender as ToolStripMenuItem).Text = "Thu nhỏ";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchBindingModel model = new SwitchBindingModel();
            InputDataFromDataGridView(model);
            _switchManager.SaveIntoFile(model);
        }
    }
}
