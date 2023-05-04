
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHero.DAL.Entities;
using Microsoft.AspNetCore.Http;

namespace SuperHero.BL.DomainModelVM
{
    public class PostVM
    {
        [Key]
        public int? ID { get; set; }

        public string? Title { get; set; } 
        [Required]
        public string Body { get; set; }

        public string? Image { get; set; }
        public DateTime? CreatedTime { get; set; }
        public int? Like { get; set; } = 0;
        public bool IsHiden { get; set; }
        public List<ReactPost>? ReactPosts { get; set; }
        //Navegation Property
        public IFormFile? ImageName { get; set; }
        public List<Comment>? Comments { get; set; }
        public string? PersonID { get; set; }
        [ForeignKey("PersonID")]
        public Person? person { get; set; }
        public CommentServise? CommentServise { get; set; }
    }
}
