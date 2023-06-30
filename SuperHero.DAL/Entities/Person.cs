using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SuperHero.DAL.Entities
{
    [Index(nameof(Email),IsUnique =true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class Person:IdentityUser
    {
       
        [Required, MaxLength(30)]
        public string FullName { get; set; }
        public bool ISDeleted { get; set; }

      


        [Required]
        public Gender gender { get; set; }
     
        public string? Image { get; set; }
        public DateTime? Birthdate { get; set; }

    
        public PersonType PersonType { get; set; }
        //Nevegation Property
      
        public UserInfo? patient { get; set; }
        public TrainerInfo? trainer { get; set; }
        public DonnerInfo? donner { get; set; }
        public DoctorInfo? doctor { get; set; }
        public List<Friends>? friends { get; set; }
        public List<Post>? Posts { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Recorder>? Recorder { get; set; }
        public List<Course>? Courses { get; set; }
        public List<PersonGroup>? Personsgroup { get; set; }
        public List<DoctorRating>? DoctorRating { get; set; }
        public List<ChatGroup>? ChatGroup { get; set; }
        public List<PrivateChat>? privateChats { get; set; }
        public List<NotificationMessage>? notificationMessages { get; set; }
        public District? district { get; set; }
        [ForeignKey("district")]
        public int? districtID { get; set; }
    }
}
