using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class EnrollmentsModel
    {

        public int StudentNo { get; set; }
        public string ModuleCode0 { get; set; }
        public string ModuleCode1 { get; set; }
        public string ModuleCode2 { get; set; }
        public string ModuleCode3 { get; set; }
        public int E_Clearence { get; set; }
        public int E_Block { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> ModulesList { get; set; }


    }
}
