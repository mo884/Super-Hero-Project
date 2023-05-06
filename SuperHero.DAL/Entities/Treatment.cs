using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Treatment
    {
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? personID { get; set; }
        public bool? IsAdd { get; set; }

        [ForeignKey("personID")]
        public UserInfo? patient { get; set; }
        public string? DoctorID { get; set; }
    }
}
