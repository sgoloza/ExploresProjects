using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class AdminModel
    {
        public int AdminID { get; set; }
        public string AdminName { get; set; }
        public string AdminSurname { get; set; }
        [Required]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]
        public string AdminPhoneNo { get; set; }
        [Required]
        [EmailAddress]
        public string AdminEmail { get; set; }
        [Required]
        [RegularExpression("([0-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 13)]
        public string AdminIDNo { get; set; }
        public string AdminType { get; set; }
        public string AdminStatus { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("password", ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

    }
}
