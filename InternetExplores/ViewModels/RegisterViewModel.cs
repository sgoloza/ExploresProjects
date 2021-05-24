using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string StudentName { get; set; }

        
       
        public int StudentNo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        public string StudentSurname { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 13)]
        [Display(Name = "ID Number")]
        public string StudetntIdNo { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string StudentPhoneNo { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Gender")]
        public string StudentGender { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime StudentDateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and confirmation password not match.")]
        public string ConfirmPassword { get; set; }
    }
}
