
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class DonnerInfoVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "*")]
        public string DonationType { get; set; }
        public int? DonnerID { get; set; }

        public CreatePerson? Person { get; set; }
    }
}
