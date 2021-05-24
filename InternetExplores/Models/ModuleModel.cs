using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class ModuleModel
    {
        public string ModuleCode { get; set; }
        public string  Modulename { get; set; }
        public string ModuleDescription { get; set; }
        public decimal ModuleCost { get; set; }
        public int ModuleCredit { get; set; }
        public string ModulePre_requisites { get; set; }

    }   
}
