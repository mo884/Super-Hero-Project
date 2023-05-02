using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class CommentServise
    {
        public int ID { get; set; }
        [Required(ErrorMessage = ("*"))]
        public string Body { get; set; }
        public DateTime? createdOn { get; set; }

        //Navegation Property


        public string? UserID { get; set; }

        public Person? person { get; set; }
        public int PostID { get; set; }

        public Post? post { get; set; }
        public PostVM? postvm { get; set; }
        public IEnumerable<CommentVM>? comment { get; set; }
    }
}
