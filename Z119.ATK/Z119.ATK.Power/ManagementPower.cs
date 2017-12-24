using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Z119.ATK.Power
{
    public class ManagementPower
    {
        public ControlPower PowerControlAddress1 { get; set; } // Có hai nguồn (2 - range) <=> tương ứng với GUI Nguồn 1: Hai rang này đểu sử dụng Kênh 1
        public ControlPower PowerControlAddress2 { get; set; } // Có 3 kênh <=> tương ứng với GUI Nguồn 2, Nguồn 3, Nguồn 4

        public ManagementPower()
        {
            PowerControlAddress1 = new ControlPower();
            PowerControlAddress2 = new ControlPower();

            PowerControlAddress1.StateChanel1 = State.OFF; // Nguồn 1
            PowerControlAddress2.StateChanel1 = State.OFF; // Nguồn 2
            PowerControlAddress2.StateChanel2 = State.OFF; // Nguồn 3
            PowerControlAddress2.StateChanel3 = State.OFF; // Nguồn 4

            PowerControlAddress1.StateRangForPower1 = StateRang.RANG1; // defaule rang of power1 is rang1

        }

        public bool ConnectionFromAddress1()
        {
            try
            {
                return PowerControlAddress1.Connection(Z119.ATK.Common.Const.Address1); // Nguồn 1 có hai Range đều sử dụng Chanel1
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool ConnectionFromAddress2()
        {
            try
            {
                return PowerControlAddress2.Connection(Z119.ATK.Common.Const.Address2); // Nguồn 2 - Nguồn 3 - Nguồn 4 (Nguồn 2: Chanel1 - Chanel2 - Chanel3)
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public void Disconnection(int power)
        {
            switch (power)  
            {
                case 1:
                    if (PowerControlAddress1.Disconnect())
                    {
                        PowerControlAddress1.StateChanel1 = State.OFF;
                    }
                    break;
                case 2:
                    if (PowerControlAddress2.Disconnect())
                    {
                        PowerControlAddress2.StateChanel1 = State.OFF;
                        PowerControlAddress2.StateChanel2 = State.OFF;
                        PowerControlAddress2.StateChanel3 = State.OFF;
                    }
                    break;
                default:
                    break;
            }
        }

        public void ChangedRangeForPower1(int range)
        {
            PowerControlAddress1.ChangedRangeForPower1(range);
        }

        public void SendPower(string von, string ampe, int power)
        {
            PowerControlAddress1.SendPower(von, ampe, power);
        }


        public void SaveIntoFile(Z119.ATK.Model.BindingModel.PowerBindingModel model)
        {
            Z119.ATK.Common.ProjectManager.SaveIntoFile(model, Z119.ATK.Common.Const.FD_NGUON);
        }

        public Z119.ATK.Model.BindingModel.PowerBindingModel OpenFile()
        {
            return Z119.ATK.Common.ProjectManager.OpenFile(Z119.ATK.Common.Const.FD_NGUON) as Z119.ATK.Model.BindingModel.PowerBindingModel;
        }


        /// <summary>
        /// Set limit for power
        /// </summary>
        /// <param name="von"></param>
        /// <param name="ampe"></param>
        /// <param name="power"></param>
        /// <param name="choose">1: ampe, 2 von</param>
        public void SendLimit(string von, string ampe, int power, int choose)
        {
            PowerControlAddress1.SendLimit(von, ampe, power, choose);
        }

        public void OnLimit(int power, int choose)
        {
            PowerControlAddress1.OnLimit(power, choose);
        }

        public void OffLimit(int power, int choose)
        {
            PowerControlAddress1.OffLimit(power, choose);
        }


        //public void DefauleReset(int power)
        //{
        //    if (power == 1)
        //        PowerControlAddress1.DefaultReset();
        //    else
        //        PowerControlAddress2.DefaultReset();
        //}
    }
}
