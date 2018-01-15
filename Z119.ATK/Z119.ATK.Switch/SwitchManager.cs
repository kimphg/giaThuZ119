using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.ATK.Common;
using Z119.ATK.Model.BindingModel;

namespace Z119.ATK.Switch
{
    public class SwitchManager
    {
        public Mode mode;
        public bool IsOn { get; set; }

        public SwitchManager()
        {
            mode = Mode.None;
            IsOn = true;
        }

        public void SaveIntoFile(SwitchBindingModel model)
        {
            Z119.ATK.Common.ProjectManager.SaveObject(model, Const.FD_CHUYENMACH);
        }

        public SwitchBindingModel OpenFile()
        {
            SwitchBindingModel res =  Z119.ATK.Common.ProjectManager.LoadObject<SwitchBindingModel>(Const.FD_CHUYENMACH);
            if (res == null) res = new SwitchBindingModel();
            return res;
        }
    }

    public enum Mode
    {
        U1, // +U2
        U2co, // +U1
        U3, // -U1
        U4, // BH
        DAT, // GND
        U2tr, // Ura
        None
    }
}
