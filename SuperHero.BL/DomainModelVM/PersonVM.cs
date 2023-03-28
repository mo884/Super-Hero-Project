using Microsoft.AspNetCore.Http;
using SuperHero.DAL.Entities;
using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class PersonVM
    {
        public string? Id { get; set; }
        [Required, MaxLength(30)]
        public string FullName { get; set; }
        public bool ISDeleted { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string Email { get; set; }




        public Gender? gender { get; set; }

        public string? Image { get; set; }
        public DateTime? Birthdate { get; set; }


        public PersonType? personType { get; set; }
        //Nevegation Property

        public IFormFile? ImageName { get; set; }

        public int? GroupID { get; set; }

        public int? districtID { get; set; }
        public DoctorInfoVM? doctor { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<PersonCourses>? UserCourses { get; set; }
        public List<PersonGroup>? Personsgroup { get; set; }
        public District? district { get; set; }
        public UserInfoVM? patient { get; set; }
        public TrainerInfoVM? trainer { get; set; }
        public DonnerInfoVM? doner { get; set; }
    }
}
