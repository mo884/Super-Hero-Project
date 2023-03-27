﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SuperHero.BL.Enum;

namespace SuperHero.BL.DomainModelVM
{
    public class RegistrationVM
    {
        [Required(ErrorMessage = "Name Required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName Required")]
        [MaxLength(30, ErrorMessage = "Please Enter Range in 30 ch")]
        public string UserName { get; set; }
     
        [Required(ErrorMessage = "Password is required")]
      
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Password is required")]
      
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password should be idintical")]
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        public PersonType persontype { get; set; }
        public int DistrictId { get; set; }


    }
}
