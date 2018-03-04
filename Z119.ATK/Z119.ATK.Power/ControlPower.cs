using NationalInstruments.VisaNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Z119.ATK.Power
{
    public class ControlPower
    {
        #region Properties ===========
        public MessageBasedSession PowerControl { get; set; }
        
        // State and mode for one of chanel

        public State StateChanel1 { get; set; }
        public State StateChanel2 { get; set; }
        public State StateChanel3 { get; set; }

        public StateRang StateRangForPower1 { get; set; }

        #endregion


        #region Methods ====================
        /// <summary>
        /// Connect to Power Device
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public bool Connection(string address)
        {
            try
            {
                PowerControl = (MessageBasedSession)ResourceManager.GetLocalManager().Open(address);
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        /// <summary>
        /// Disconnect from power device
        /// </summary>
        /// <returns></returns>
        public bool Disconnect()
        {
            try
            {
                PowerControl.Dispose();
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }

        /// <summary>
        /// Turn on chanel of current power
        /// </summary>
        /// <param name="chanel">Chanel of current power</param>
        /// <returns></returns>
        public bool OnChanel(int chanel)
        {
            try
            {
                switch (chanel)
                {
                    case 1: PowerControl.Write(Z119.ATK.Common.Const.ON_CHANEL1);
                        break;
                    case 2: PowerControl.Write(Z119.ATK.Common.Const.ON_CHANEL2);
                        break;
                    case 3: PowerControl.Write(Z119.ATK.Common.Const.ON_CHANEL3);
                        break;
                    default: return false;
                        break;
                }
                return true;
            }
            catch (Exception exp)
            { return false; }
        }

        /// <summary>
        /// Turn off chanel of current power
        /// </summary>
        /// <param name="chanel">Chanel of current power</param>
        /// <returns></returns>
        public bool OffChanel(int chanel)
        {
            try
            {
                switch (chanel)
                {
                    case 1: PowerControl.Write(Z119.ATK.Common.Const.OFF_CHANEL1);
                        break;
                    case 2: PowerControl.Write(Z119.ATK.Common.Const.OFF_CHANEL2);
                        break;
                    case 3: PowerControl.Write(Z119.ATK.Common.Const.OFF_CHANEL3);
                        break;
                    default: return false;
                        
                }
                return true;
            }
            catch (Exception exp)
            { return false; }
        }

        public bool SendPower(string von, string ampe, int nguon)
        {
            try
            {
                switch (nguon)
                {
                    case 1: PowerControl.Write(Z119.ATK.Common.Const.SEND_POWER_CHANEL1 + von + "," + ampe); // Because both address to use this
                        break;
                    case 2: PowerControl.Write(Z119.ATK.Common.Const.SEND_POWER_CHANEL1 + von + "," + ampe);
                        break;
                    case 3: PowerControl.Write(Z119.ATK.Common.Const.SEND_POWER_CHANEL2 + von + "," + ampe);
                        break;
                    case 4: PowerControl.Write(Z119.ATK.Common.Const.SEND_POWER_CHANEL3 + von + "," + ampe);
                        break;
                    default: 
                        return false;
                        
                }
                return true;
            }
            catch (Exception exp)
            { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="von"></param>
        /// <param name="ampe"></param>
        /// <param name="nguon"></param>
        /// <param name="choose">1. ampe | 2 von</param>
        /// <returns></returns>
        public bool SendLimit(string von, string ampe, int nguon, int choose)
        {
            try
            {
                switch (nguon)
                {
                    case 1:
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH1");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_AMPE + " " + ampe);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_VON + " " + von);
                        break;
                    case 2: 
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH1");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_AMPE + " " + ampe);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_VON + " " + von);
                        break;
                    case 3: 
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH2");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_AMPE + " " + ampe);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_VON + " " + von);
                        break;
                    case 4: 
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH3");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_AMPE + " " + ampe);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.SEND_LIMIT_VON + " " + von);
                        break;
                    default: return false;
                        break;
                }
                return true;
            }
            catch (Exception exp)
            { return false; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="von"></param>
        /// <param name="ampe"></param>
        /// <param name="nguon"></param>
        /// <param name="choose">1: ampe, 2 von</param>
        /// <returns></returns>
        public bool OnLimit(int nguon, int choose)
        {
            try
            {
                switch (nguon)
                {
                    case 1: 
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH1");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_VON);
                        break;
                    case 2: 
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH1");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_VON);
                        break;
                    case 3: 
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH2");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_VON);
                        break;
                    case 4: 
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH3");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.ON_LIMIT_VON);
                        break;
                    default: return false;
                        break;
                }
                return true;
            }
            catch (Exception exp)
            { return false; }
        }

        public bool OffLimit(int nguon, int choose)
        {
            try
            {
                switch (nguon)
                {
                    case 1:
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH1");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_VON);
                        break;
                    case 2:
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH1");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_VON);
                        break;
                    case 3:
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH2");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_VON);
                        break;
                    case 4:
                        PowerControl.Write(Z119.ATK.Common.Const.SELECTED_SET_LIMIT + " CH3");
                        if (choose == 1)
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_AMPE);
                        else
                            PowerControl.Write(Z119.ATK.Common.Const.OFF_LIMIT_VON);
                        break;
                    default: return false;
                        break;
                }
                return true;
            }
            catch (Exception exp)
            { return false; }
        }


        public void ChangedRangeForPower1(int range)
        {
            switch (range)
            {
                case 1:
                    try
                    {
                        PowerControl.Write(Z119.ATK.Common.Const.RANGE1);
                    }
                    catch (Exception)
                    { }
                    break;
                case 2:
                    try
                    {
                        PowerControl.Write(Z119.ATK.Common.Const.RANGE2);
                    }
                    catch (Exception)
                    { }
                    break;
                default:
                    break;
            }
        }



        //public List<String> ReadPower(int chanel)
        //{
        //    List<String> lstPower = new List<string>();
            
        //    Task.Run(() =>
        //    {
        //        while (true)
        //        {

        //            PowerControl.Write(Z119.ATK.Common.Const.READVON + " " + chanel);
        //            lstPower.Add(PowerControl.ReadString());

        //            PowerControl.Write(Z119.ATK.Common.Const.READAMPE + " " + chanel);
        //            lstPower.Add(PowerControl.ReadString());


        //            Thread.Sleep(100);
        //        }
        //    });

        //    return lstPower;
        //}


        public void DefaultReset()
        {
            try
            {
                PowerControl.Write(Z119.ATK.Common.Const.LOAD_DEFAULT_RESET);
            }
            catch (Exception)
            { }
            
        }

        #endregion =======================

    }

    // State and mode for one of chanel

    public enum State
    { 
        ON,
        OFF
    }

    // for power1
    public enum StateRang
    { 
        RANG1,
        RANG2
    }
}
