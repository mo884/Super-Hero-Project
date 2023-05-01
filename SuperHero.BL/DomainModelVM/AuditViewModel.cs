using Microsoft.AspNetCore.Http;
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class AuditViewModel
    {
        [Key]
        public int? ID { get; set; }

        public string? Title { get; set; }
        [Required]
        public string Body { get; set; }

        public string? Image { get; set; }
        public DateTime? CreatedTime { get; set; }

        //Navegation Property
        public IFormFile? ImageName { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<ReactPost>? ReactPosts { get; set; }
        public string? PersonID { get; set; }
        public IEnumerable<CreatePerson>? nearDoctor { get; set; }
        public IEnumerable<Friends>? friends { get; set; }
        public IEnumerable<Friends>? Allfriends { get; set; }
        public List<CreatePerson>? Doctors { get; set; }
        [ForeignKey("PersonID")]
        public Person? person { get; set; }
        public IEnumerable<PostVM>? post { get; set; }
    }
}
