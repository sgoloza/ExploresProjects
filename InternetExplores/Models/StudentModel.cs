﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Models
{
    public class StudentModel
    {
       
        [Display(Name = "Student Number")]
        public int StudentNo { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string StudentName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Surname")]
        public string StudentSurname { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 13)]
        [Display(Name = "ID Number")]
        public string StudentIdNo { get; set; }

      
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        public string StudentPhoneNo { get; set; }
        
        [EmailAddress]
        [Display(Name = "Email")]
        public string StudentEmail { get; set; }
      
        [EmailAddress]
        [Display(Name = "Gender")]
        public string StudentGender { get; set; }
        
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        public DateTime StudentDateOfBirth { get; set; }

        [Display(Name = "Home language")]
        public string StudentHomeLanguage { get; set; }

       
        [Display(Name = "Finincial status")]
        public string StudentFinincialStatus { get; set; }

        // Student Academic details
     
        [Display(Name = "Degree")]
        public string StudentDegree { get; set; }

       
        [Display(Name = "Level of study")]
        public string StudentlevelOfStudy { get; set; }

        
        [Display(Name = "Faculty")]
        public string StudentFaculty { get; set; }

        
        [Display(Name = "Accommodation")]
        public string StudentNeedAccommodation { get; set; }

        
        [Display(Name = "Risk status")]
        public string StudentRiskStatus { get; set; }

        // Student Address
       
        [Display(Name = "Street name")]
        public string StreetName { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        
        [Display(Name = "Province")]
        public string Province { get; set; }
      
        [EmailAddress]
        [Display(Name = "City")]
        public string City { get; set; }
       
        [EmailAddress]
        [Display(Name = "Postal code")]
        public int PostalCode { get; set; }

        // Acommodation details
        
        [Display(Name = "Room ID")]
        public int RoomID { get; set; }
        
        [EmailAddress]
        [Display(Name = "Residence ID")]
        public int ResidenceID { get; set; }
       
        [EmailAddress]
        [Display(Name = "Residence status")]
        public string ResidenceStatus { get; set; }
        public decimal StudentBalance { get; set; }
       
    }
}
