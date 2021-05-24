using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class PaymentModel
    {
        public int paymentID { get; set; }
        public StudentModel StudentNo { get; set; }
        public string paymenttype { get; set; }
        public decimal paymentAmount { get; set; }
        public string paymentDescription { get; set; }
        public int bankName { get; set; }
        public DateTime paymentDate { get; set; }

    }
}
