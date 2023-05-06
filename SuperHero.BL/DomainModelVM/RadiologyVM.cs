using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class RadiologyVM
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? XRay { get; set; }
        public bool? IsAdd { get; set; }
        public string? DoctorID { get; set; }
        public Person? Doctor { get; set; }
        public int? personID { get; set; }


        [ForeignKey("personID")]
        public UserInfo? patient { get; set; }
    }
}
