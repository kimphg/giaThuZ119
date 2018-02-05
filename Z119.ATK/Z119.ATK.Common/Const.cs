using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Z119.ATK.Shell.Project;
using System.Drawing;
namespace Z119.ATK.Common
{
    public class ProjectConfiguration
    {
        public Size fSchemeSize;
        public string loadCtrl;
        public string switchCtrl;
        public string powerCtrl;
        public string oscilloCtrl;
        //public fMainWindow 
        public ProjectConfiguration()
        {
            fOsciloLocation = new Point(0, 0);
            fSchemeLocation = new Point(0, 0);
            fSchemeSize = new Size(800,600);
        }

        public Point fOsciloLocation;
        public Point fSchemeLocation;
    }
    public class Const
    {
        public static List<schemePoint> schemePointList = new List<schemePoint>();
        public static List<schemeElement> schemeElementList = new List<schemeElement>();
        public static SortedDictionary<String, StepItem> stepList = new SortedDictionary<String, StepItem>();
        // path Main
        public static string PATH_ROOT =  @"C:\ATK\Config";
        public static string PATH_PROJECT = @"C:\ATK\Projects";
        public static string PATH_CURRENT = @"C:\ATK\";
       // public static string FOLDER_ROOT = "DuAn";
        public static ProjectConfiguration proConf = new ProjectConfiguration(); 
        public static string PROJECT_NAME = @"C:\ATK\Config";
        public static bool isAdmin = false;
        public static string FILE_NAME_PATH = "ProjectPath.txt";
        public static string IMAGES_LIBLARY_NAME = "HinhAnhLinhKien";
        public static Common.ProjectManager projMan = new ProjectManager();
        // *********************************************************************************************//
        // *********************************************************************************************//

        // Foder Name
        public static string FD_NGUON = "Nguon.xml";
        public static string FD_CHUYENMACH = "ChuyenMach.xml";
        public static string FD_HIENTHI = "KiemTra.xml";
        public static string FD_Tai = "Tai.xml";

        // *********************************************************************************************//
        // *********************************************************************************************//

        // For form Power
        public static string Address1 = "USB0::0x1AB1::0x0E11::DP8H182500033::INSTR"; // Nguồn có 2 range đều sử dụng kênh 1
        public static string Address2 = "USB0::0x1AB1::0x0E11::DP8C183402940::INSTR"; // Nguồn có 3 kênh

        // Rang for power1
        public static string RANGE1 = ":OUTP:RANG P20V";
        public static string RANGE2 = ":OUTP:RANG P40V";

        public static string QUERYRANG = ":OUTP:RANG?";

        // Realtime Read All
        public static string READALL = ":MEAS:ALL?"; // (8.50,0.000,0.000) ~ (V - A - W)

        // Realtime 
        public static string READAMPE = ":MEAS:CURR?";
        public static string READVON = ":MEAS?";

        // Reset
        public static string DEFAULR_RESERT = "*RST";

        // Cả hai nguồn đều sử dụng được kênh CH1 Address1, Address2
		public static string ON_CHANEL1 = ":OUTP CH1,ON"; // Both of address
        public static string OFF_CHANEL1 = ":OUTP CH1,OFF"; // Both of address
        public static string SEND_POWER_CHANEL1 = ":APPL CH1,"; // Both of address

        // Danh riêng cho nguồn có 3 kênh
		public static string ON_CHANEL2 = ":OUTP CH2,ON"; // Only use for address 2
        public static string OFF_CHANEL2 = ":OUTP CH2,OFF"; // Only use for address 2
        public static string SEND_POWER_CHANEL2 = ":APPL CH2,"; // Only use for address 2

        public static string ON_CHANEL3 = ":OUTP CH3,ON"; // Only use for address 2
        public static string OFF_CHANEL3 = ":OUTP CH3,OFF"; // Only use for address 2
        public static string SEND_POWER_CHANEL3 = ":APPL CH3,"; // Only use for address 2

        // Limit
        public static string SELECTED_SET_LIMIT = ":INST"; // :INST CH1(2,3)
        public static string SEND_LIMIT_AMPE = ":CURR:PROT"; // :CURR:PROT 2.3
        public static string ON_LIMIT_AMPE = ":CURR:PROT:STAT ON"; // :CURR:PROT:STAT ON
        public static string OFF_LIMIT_AMPE = ":CURR:PROT:STAT OFF"; // :CURR:PROT:STAT OFF
        public static string SEND_LIMIT_VON = ":VOLT:PROT"; // :VOLT:PROT 8.8
        public static string ON_LIMIT_VON = ":VOLT:PROT:STAT ON"; // :VOLT:PROT:STAT ON
        public static string OFF_LIMIT_VON = ":VOLT:PROT:STAT OFF"; // :VOLT:PROT:STAT OFF

        public static string ICON_POWER_ON = @"..\..\Resources\On.png";
        public static string ICON_POWER_OFF = @"..\..\Resources\Off.png";
        public static string ICON_POWER_ERROR = @"..\..\Resources\Error.png";

        // *********************************************************************************************//
        // *********************************************************************************************//
        // For form Program
        //public static string OpenNguon = "Nguon.Bat_Nguon";
        //public static string CloseNguon = "Nguon.Tat_Nguon";

        //public static string OpenChuyenMach = "Chuyen_Mach.Bat_Chuyen_Mach";
        //public static string CloseChuyenMach = "Chuyen_Mach.Tat_Chuyen_Mach";

        //public static string OpenHienThi = "Hien_Thi.Bat_Hien_Thi";
        //public static string CloseHienThi = "Hien_Thi.Tat_Hien_Thi";

        // Form Tai
        public static string MODE_CC = ":MODE CC";
        public static string CURR_VA = ":CURR:VA";
        public static string CURR_VB = ":CURR:VB";

        public static string ONTAI = ":INP ON";
        public static string OFFTAI = ":INP OFF";

        public static string RECIVE_VON_TAI = ":MEAS:VOLT?";
        public static string RECIVE_AMPE_TAI = ":MEAS:CURR?";
        public static string RECIVE_WOAT_TAI = ":MEAS:POW?";

        // Binding data from Tai to Check
        public static string VON_RA = "0";
        public static string AMPE_RA = "0";
        public static string FILE_CONFIG = "config.xml";
        public static string FILE_POINT_DATA = "point_data.xml";
        public static string FILE_MAINWINDOW = "fMainWindow.xml";
        public static bool IsWaitForLRLocation = false;
        public static string FILE_ELE_DATA = "element_data.xml";
        public static System.Windows.Forms.Form mainForm;
    }
}
