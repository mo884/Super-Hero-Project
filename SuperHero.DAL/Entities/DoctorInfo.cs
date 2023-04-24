using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class DoctorInfo
    {
        [Key]
        public int ID { get; set; }

    
        public string? CV { get; set; }
        public string? ClinicAdress { get; set; }
        public string? ClinicName { get; set; }
        public float? Rating { get; set; }
        //Nevegation Property

        public string? DectorID { get; set; }
        [ForeignKey("DectorID")]
        public Person? Person { get; set; } = null!;
    }
}
