using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
namespace Z119.ATK.Common
{
    public class ConfigData
    {
        static Dictionary<string, Dictionary<string, string>> config;
        static Dictionary<string, string> profiles;
        
        public static void LoadConfigFile()
        {
            config = new Dictionary<string, Dictionary<string, string>>() ;
            var doc = XDocument.Load(@"C:/ATK/Config/config.xml");
            var rootNodes = doc.Root.DescendantNodes().OfType<XElement>();
            config = rootNodes.ToDictionary(n => n.Name.ToString(), n => n.Value);
        }
        //new code
        public static void SaveIntoFile(object model, string folderPath)
        {
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.InitialDirectory = Z119.ATK.Common.Const.PATH_CUREENT + @"\" + folderPath;

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


        public static object OpenFile(string folderPath)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.InitialDirectory = Z119.ATK.Common.Const.PATH_CUREENT + @"\" + folderPath;

                if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    FileStream fs = new FileStream(ofd.FileName, FileMode.Open);
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
                File.WriteAllText("ProjectPath.txt", Const.PATH_CUREENT);
            }
            string s = File.ReadAllText("ProjectPath.txt");
            Const.PATH_ROOT = s;
            Const.PATH_CUREENT = s;
            Const.PROJECT_NAME = s;

            // Create new folder image liblary
            if (!Directory.Exists(Const.PATH_ROOT + @"\" + Const.IMAGES_LIBLARY_NAME))
            {
                Directory.CreateDirectory(Const.PATH_ROOT + @"\" + Const.IMAGES_LIBLARY_NAME);
            }
        }
        
    }
}
