using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class DoctorRatingVM
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public bool? IsReating { get; set;}
        public string? star { get; set; }
        public float? reating { get; set; } = null!;
        public bool PersonIsReating { get; set; } = false ;
        public string? description { get; set; }
        [ForeignKey("Doctor")]
        public string? DoctorId { get; set; }
        public Person? Doctor { get; set; }
        public string? PersonID { get; set; }
    }
}
