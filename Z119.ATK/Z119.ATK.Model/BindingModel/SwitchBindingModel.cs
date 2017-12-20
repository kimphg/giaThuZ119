using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.ATK.Model.BindingModel
{
    [Serializable]
    public class SwitchBindingModel
    {
        public List<List<byte>> ListFrame { get; set; }

        public List<string> ListChan { get; set; }

    }
}
