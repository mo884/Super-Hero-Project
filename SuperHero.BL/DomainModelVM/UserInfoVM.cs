

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHero.DAL.Enum;
using SuperHero.DAL.Entities;

namespace SuperHero.BL.DomainModelVM
{
    public class UserInfoVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "*")]
        public Reason Reason { get; set; }
        
        public int? UserID { get; set; }
       
        public Person? Person { get; set; }
    }
}
