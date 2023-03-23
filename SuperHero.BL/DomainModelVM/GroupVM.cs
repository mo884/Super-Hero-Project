
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class GroupVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="*")]
        public string? Name { get; set; }
        public DateTime? CreatedTime { get; set; }
        //Nevegation Property
        public List<CreatePerson>? Persons { get; set; }
    }
}
