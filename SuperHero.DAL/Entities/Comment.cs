using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Comment
    {
        [Key]
        public int ID { get; set; }
        public string Body { get; set; }
        public DateTime createdOn { get; set; }
        //Navegation Property


        public string? UserID { get; set; }
        [ForeignKey("UserID")]
        public Person? person { get; set; }
        public int PostID { get; set; }
        [ForeignKey("PostID")]
        public Post? post { get; set; }
    }
}
