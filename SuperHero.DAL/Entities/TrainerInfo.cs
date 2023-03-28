using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class TrainerInfo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Graduation { get; set; }
        public string? CV { get; set; }
      
        //Nevegation Property
        public string? TrainerID { get; set; }
        [ForeignKey("TrainerID")]
        public Person? Person { get; set; }
    }
}
