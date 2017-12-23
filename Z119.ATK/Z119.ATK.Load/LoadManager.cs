using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.ATK.Common;
using Z119.ATK.Model.BindingModel;

namespace Z119.ATK.Load
{
    public class LoadManager
    {
        public bool IsOn { get; set; }

        public LoadManager()
        {
            IsOn = false;
        }

        public void SaveIntoFile(LoadBindingModel model)
        {
            Z119.ATK.Common.ConfigData.SaveIntoFile(model, Const.FD_Tai);
        }

        public LoadBindingModel OpenFile()
        {
            return Z119.ATK.Common.ConfigData.OpenFile(Const.FD_Tai) as LoadBindingModel;
        }
    }
}
