using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class RecorderVM
    {
        public int ID { get; set; }
        [ForeignKey("Patient")]
        public string? PatientID { get; set; }
        public string? DoctorID { get; set; }
        [Required]
        public DateTime RecodDate { get; set; }
        public bool? IsCheck { get; set; }

        public Person? Patient { get; set; }
    }
}
