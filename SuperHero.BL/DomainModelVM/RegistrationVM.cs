using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SuperHero.BL.Enum;
using Microsoft.AspNetCore.Http;
using SuperHero.DAL.Entities;

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
        public int DistrictId { get; set; }

        public Gender gender { get; set; }
        public PersonType personType { get; set; }
        //Nevegation Property
        public int districtID { get; set; } = 0;
        public DoctorInfoVM? doctor { get; set; }
        public IEnumerable<PostVM>? Posts { get; set; }
        public List<Friends>? friends { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<PersonCourses>? UserCourses { get; set; }
        public List<PersonGroup>? Personsgroup { get; set; }
        public District? district { get; set; }
        public UserInfoVM? patient { get; set; }
        public TrainerInfoVM? trainer { get; set; }
        public DonnerInfoVM? doner { get; set; }

    }
}
