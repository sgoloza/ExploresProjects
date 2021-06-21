using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class EnrollmentsModel
    {

        public int StudentNo { get; set; }
        public int ModuleCode { get; set; }
        public int E_Clearence { get; set; }
        public int E_Block { get; set; }
        public List<string> ModulesList { get; set; }
    }
}
