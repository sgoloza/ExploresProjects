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
        public string levelOdstudy { get; set; }
        public string faculty { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ModulesList { get; set; }
        public string ModulesStatus { get; set; }

    }   
}
