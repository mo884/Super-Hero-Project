using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class DoctorRating
    {

        [Key]
        public int ID { get; set; }
        [Required]
        public bool IsReating { get; set; }
        public string? description { get; set; }
        public float? reating { get; set; } = null!;
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Person Doctor { get; set; }
        public string PersonID { get; set; }
    }
}
