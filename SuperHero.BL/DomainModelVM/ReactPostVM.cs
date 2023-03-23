using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class ReactPostVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "*")   ]
        public bool IsLike { get; set; }
        public int PostID { get; set; }
        public string PersonID { get; set; }
        public PostVM post { get; set; }
    }
}
