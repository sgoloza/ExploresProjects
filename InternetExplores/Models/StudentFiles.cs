using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InternetExplores.Models
{
    public class StudentFiles
    {

        [Display(Name = "Choose id copy")]
        [Required]
        public IFormFile idcopy { get; set; }
        public string idcopyUrl { get; set; }

        [Display(Name = "Choose next of Kin id copy")]
        [Required]
        public IFormFile matricResult { get; set; }
        public string matricResultUrl { get; set; }

        [Display(Name = "Choose the cover photo of your book")]
        [Required]
        public IFormFile nextofKin { get; set; }
        public string nextofKinUrl { get; set; }

        [Display(Name = "Choose financial proof")]
        [Required]
        public IFormFile financialProof { get; set; }
        public string financialProofUrl { get; set; }

    }
}
