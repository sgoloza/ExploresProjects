using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class AdminModel
    {
        public string AdminName { get; set; }
        public string AdminSurname { get; set; }
        public string AdminPhoneNo { get; set; }
        public string AdminEmail { get; set; }
        public string AdminIDNo { get; set; }
        public string AdminType { get; set; }
        public string AdminStatus { get; set; }
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
