using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z119.ATK.Common;
using Z119.ATK.Model.BindingModel;

namespace Z119.ATK.Check
{
    public class CheckManager
    {
        public void SaveIntoFile(CheckBindingModel model)
        {
            Z119.ATK.Common.ProjectManager.SaveObject(model, Const.FD_HIENTHI);
        }

        public CheckBindingModel OpenFile()
        {
            return Z119.ATK.Common.ProjectManager.LoadObject<CheckBindingModel>(Const.FD_HIENTHI);
        }
    }
}
