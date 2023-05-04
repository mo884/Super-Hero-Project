using Microsoft.AspNetCore.Http;
using SuperHero.DAL.Enum;
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class CreatePerson
    {
        public string? Id { get; set; }
        [Required, MaxLength(30,ErrorMessage = "FullName required and Length Max 30")]
        public string? FullName { get; set; }
        public bool ISDeleted { get; set; }

        [Required(ErrorMessage = "UserName required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage ="Password required")]
        public string? PasswordHash { get; set; }
        [DataType(DataType.Password)]
        [Compare("PasswordHash", ErrorMessage = "Password should be idintical")]
        public string? ConfirmPassword { get; set; }
        public string Email { get; set; }
       



        public Gender? gender { get; set; }

        public string? Image { get; set; }
        public DateTime? Birthdate { get; set; }


        public  PersonType? personType{ get; set; }
        //Nevegation Property

        public IFormFile? ImageName { get; set; }

        public int GroupID { get; set; }

        public DoctorRatingVM? doctorRating { get; set; } 
        public int districtID { get; set; } = 0;
        public DoctorInfoVM? doctor { get; set; }
        public string? body { get; set; }
       
        public IEnumerable<PostVM>? Posts { get; set; }
        public List<Friends>? friends { get; set; }

        public IEnumerable<Friends>? Friends { get; set; }
        public IEnumerable<Friends>? Allfriends { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<PersonCourses>? UserCourses { get; set; }
        public List<PersonGroup>? Personsgroup { get; set; }
        public District? district { get; set; }
        public UserInfo? patient { get; set; }
        public TrainerInfoVM? trainer { get; set; }
        public DonnerInfoVM? doner { get; set; }
    }
}
