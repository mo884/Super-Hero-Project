using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Post
    {
        [Key]
        public int ID { get; set; }

        public string? Title { get; set; }
        [Required]
        public string Body { get; set; }

        public string? Image { get; set; }
        public DateTime CreatedTime { get; set; }
        public int Like { get; set; }=0;

        //Navegation Property
        public List<Comment> Comments { get; set; }
        public List<ReactPost>? ReactPosts { get; set; }
        public string? PersonID { get; set; }
        [ForeignKey("PersonID")]
        public Person? person { get; set; }
    }
}
