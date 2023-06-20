using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class MedicalSyndicate
    {
        [Key] 
        public int ID { get; set; }
        [Required]
        public string NUMMBERSYNDICATE { get; set; }
    }
}
