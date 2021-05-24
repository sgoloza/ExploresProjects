using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class ResidenceModel
    {
        public int ResidenceID { get; set; }
        public string ResidenceName { get; set; }
        public string ResidenceRoomType { get; set; }
        public decimal ResidenceCost { get; set; }
        // Residence address
        public string ResidenceStreetName { get; set; }
        public string ResidenceCountry { get; set; }
        public string ResidenceProvince { get; set; }
        public string ResidenceCity { get; set; }
        public int ResidencePostalCode { get; set; }
    }
}
