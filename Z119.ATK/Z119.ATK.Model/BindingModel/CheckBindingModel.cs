using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z119.ATK.Model.BindingModel
{
    [Serializable]
    public class CheckBindingModel
    {
        public string Von { get; set; }
        public string VonDenta { get; set; }
        public string Ampe { get; set; }

        public string PrincipleDiagram { get; set; }
        public string AssemblyDiagram { get; set; }

        //public model GuideDocument { get; set; }
    }
}
