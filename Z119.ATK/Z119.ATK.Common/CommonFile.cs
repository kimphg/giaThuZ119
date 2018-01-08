using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace Z119.ATK.Common
{
    public class ProjectManager
    {
         Dictionary<string, Dictionary<string, string>> allProfiles;
         Dictionary<string, string> profilesData;
         ListBox profileList;
         string[] dirEntries;
         private event EventHandler _selectedProject;
         public event EventHandler SelectedProject
        {
            add
            {
                _selectedProject += value;
            }
            remove
            {
                _selectedProject -= value;
            }
        }
        public void LoadProject()
        {
            dirEntries = Directory.GetFileSystemEntries(Const.PATH_PROJECT, "*", SearchOption.TopDirectoryOnly);
            using (Form form = new Form())
            {
                form.Text = "Chọn dự án";
                FlowLayoutPanel flp = new FlowLayoutPanel();
                // profile list
                flp.Controls.Add(new Label() { Text = "Danh sách dự án:" });
                profileList = new ListBox();
                profileList.DataSource = dirEntries;
                profileList.Size = new Size(200,200);
                flp.Controls.Add(profileList);
                //ok button
                Button button1 = new Button();
                button1.Text = "OK";
                button1.Click += new EventHandler(button1_Click);
                flp.Controls.Add(button1);
                form.AcceptButton = button1;
                button1.DialogResult = System.Windows.Forms.DialogResult.OK;
                //create profile button
                Button button2 = new Button();
                button2.Text = "Tạo mới";
                button2.Click += new EventHandler(button2_Click);
                
                flp.Controls.Add(button2);

                flp.Dock = DockStyle.Fill;
                form.Controls.Add(flp);
                form.ShowDialog();
            }
            
        }
        public void SaveProjectConfig()
        {
            SaveObject<ProjectConfiguration>(Const.proConf,Z119.ATK.Common.Const.FILE_CONFIG);
        }
        private  void initProject()
        {
            if (File.Exists(Z119.ATK.Common.Const.PATH_CURRENT + "\\" + Z119.ATK.Common.Const.FILE_CONFIG))
            {
                Const.proConf = LoadObject<ProjectConfiguration>(Z119.ATK.Common.Const.FILE_CONFIG);
            }
            // Tạo 3 Folder trong path current
            /*string fdNguon = Z119.ATK.Common.Const.PATH_CURRENT + @"\" + Z119.ATK.Common.Const.FD_NGUON;
            string fdChuyenMach = Z119.ATK.Common.Const.PATH_CURRENT + @"\" + Z119.ATK.Common.Const.FD_CHUYENMACH;
            string fdHienThi = Z119.ATK.Common.Const.PATH_CURRENT + @"\" + Z119.ATK.Common.Const.FD_HIENTHI;

            string fdTai = Z119.ATK.Common.Const.PATH_CURRENT + @"\" + Z119.ATK.Common.Const.FD_Tai;

            string fdHinhAnh = Z119.ATK.Common.Const.PATH_CURRENT + @"\" + Z119.ATK.Common.Const.FD_HIENTHI + @"\HinhAnh";
            string fSoDoNguyenLy = fdHinhAnh + @"\SoDoNguyenLy";
            string fSoDoLapRap = fdHinhAnh + @"\SoDoLapRap";

            // Kiểm tra tồn tại và tạo mới
            if (!Directory.Exists(fdNguon))
                Directory.CreateDirectory(fdNguon);
            if (!Directory.Exists(fdChuyenMach))
                Directory.CreateDirectory(fdChuyenMach);
            if (!Directory.Exists(fdHienThi))
                Directory.CreateDirectory(fdHienThi);

            if (!Directory.Exists(fdTai))
                Directory.CreateDirectory(fdTai);

            if (!Directory.Exists(fdHinhAnh))
                Directory.CreateDirectory(fdHinhAnh);
            if (!Directory.Exists(fSoDoNguyenLy))
                Directory.CreateDirectory(fSoDoNguyenLy);
            if (!Directory.Exists(fSoDoLapRap))
                Directory.CreateDirectory(fSoDoLapRap);
            */
            // Chọn xong prọect và tạo các folder xong
            // Tạo xong rồi thì phải roai một cái event lên để thông báo cho fmain
            if (_selectedProject != null)
            {
                _selectedProject(this, new EventArgs());
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            //System.Drawing.Size size = new System.Drawing.Size(200, 70);
            Form inputBox = new Form();
            FlowLayoutPanel flp = new FlowLayoutPanel();
            //inputBox.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //inputBox.ClientSize = size;
            inputBox.Text = "Nhập tên dự án mới";

            System.Windows.Forms.TextBox textBox = new TextBox();
            //textBox.Size = new System.Drawing.Size(size.Width - 10, 23);
            //textBox.Location = new System.Drawing.Point(5, 5);
            //textBox.Text = input;
            flp.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            okButton.Name = "okButton";
            //okButton.Size = new System.Drawing.Size(75, 23);
            okButton.Text = "&OK";
            //okButton.Location = new System.Drawing.Point(size.Width - 80 - 80, 39);
            flp.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancelButton.Name = "cancelButton";
            //cancelButton.Size = new System.Drawing.Size(75, 23);
            cancelButton.Text = "&Cancel";
            //cancelButton.Location = new System.Drawing.Point(size.Width - 80, 39);
            flp.Controls.Add(cancelButton);
            flp.Dock = DockStyle.Fill;
            inputBox.Controls.Add(flp);
            inputBox.AcceptButton = okButton;
            inputBox.CancelButton = cancelButton;

            if(inputBox.ShowDialog()==DialogResult.OK)
            {
                string projectDir = Const.PATH_PROJECT + "\\" + textBox.Text;
                Directory.CreateDirectory(projectDir);
                dirEntries = Directory.GetFileSystemEntries(Const.PATH_PROJECT, "*", SearchOption.TopDirectoryOnly);
                profileList.DataSource = dirEntries;
            }
            
        }

        private  void button1_Click(object sender, EventArgs e)
        {
            // Cập nhật lại path current
            Z119.ATK.Common.Const.PATH_CURRENT = profileList.SelectedItem.ToString();
            initProject();
        }
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public static void SaveObject<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, serializableObject);
                    stream.Position = 0;
                    xmlDocument.Load(stream);
                    xmlDocument.Save(Const.PATH_CURRENT+"\\"+fileName);
                    stream.Close();
                }
            }
            catch (Exception ex)
            {
                //Log exception here
                MessageBox.Show(ex.ToString());
            }
        }


        /// <summary>
        /// Deserializes an xml file into an object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T LoadObject<T>(string fileName)
        {
            if (string.IsNullOrEmpty(Const.PATH_CURRENT + "\\" + fileName)) { return default(T); }

            T objectOut = default(T);

            try
            {
                System.Xml.XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(Const.PATH_CURRENT + "\\" + fileName);
                string xmlString = xmlDocument.OuterXml;

                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }

                    read.Close();
                }
            }
            catch (Exception ex)
            {
                return (T)Activator.CreateInstance(typeof(T));
            }
            
            return objectOut;
        }
        //new code ends here-----------------------------------------------------------------------------
        public static void SaveIntoFile(object model, string folderPath)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Z119.ATK.Common.Const.PATH_ROOT + @"\" + folderPath;

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(sfd.FileName, FileMode.Create);
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, model);
                fs.Close();
            }
            else
            {
                // Do not something
            }
        }


        public static object  OpenFile(string fileName)
        {
            try
            {
                string fullFileName = Const.PATH_CURRENT + "\\" + fileName;
                //OpenFileDialog ofd = new OpenFileDialog();

                //ofd.InitialDirectory = Z119.ATK.Common.Const.PATH_PROJECT + @"\" + folderPath;

                //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                //{
                if(File.Exists(fullFileName))
                {
                    FileStream fs = new FileStream(fullFileName, FileMode.Open);
                    IFormatter formatter = new BinaryFormatter();
                    var data = formatter.Deserialize(fs);
                    fs.Close();
                    return data;
                }
                else { return null; }
            }
            catch (Exception)
            { return null; }
        }

        public static void ReadFolderPath()
        {
            if (!File.Exists(Const.FILE_NAME_PATH))
            {
                File.WriteAllText("ProjectPath.txt", Const.PATH_ROOT);
            }
            string s = File.ReadAllText("ProjectPath.txt");
            Const.PATH_ROOT = s;
            Const.PATH_ROOT = s;
            Const.PROJECT_NAME = s;

            // Create new folder image liblary
            if (!Directory.Exists(Const.PATH_ROOT + @"\" + Const.IMAGES_LIBLARY_NAME))
            {
                Directory.CreateDirectory(Const.PATH_ROOT + @"\" + Const.IMAGES_LIBLARY_NAME);
            }
        }


        
    }
}
