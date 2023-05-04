using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class ReactPost
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public bool IsLike { get; set; }
        public int PostID { get; set; }
        public Post Post { get; set; }
        public string PersonID { get; set; }
        public bool IsHiden { get; set; }
    }
}
