using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class PaymentModel
    {
        public int paymentID { get; set; }

        [Display(Name = "Student No")]
        [DataType(DataType.Text)]
        public int StudentNo { get; set; }


        [Display(Name = "Payment type")]
        [DataType(DataType.Text)]
        public string paymenttype { get; set; }


        [Display(Name = "Amount ")]
        [DataType(DataType.Currency)]
        public decimal paymentAmount { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.Text)]
        public string paymentDescription { get; set; }

        [Display(Name = "Bank name")]
        [DataType(DataType.Text)]
        public string bankName { get; set; }

        [Display(Name = " Date ")]
        [DataType(DataType.DateTime)]
        public DateTime paymentDate { get; set; }


        [Display(Name = "Now date")]
        [DataType(DataType.DateTime)]
        public DateTime PayemntNowdate { get; set; }

        [Display(Name = "Choose paymen proofy")]
        [Required]
        public IFormFile paymentProof { get; set; }
        public string paymentProofUrl { get; set; }
    }
}
