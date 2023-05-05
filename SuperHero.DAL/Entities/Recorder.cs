using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public  class Recorder
    {
        public int ID { get; set; }
        [ForeignKey("Patient")]
        public string PatientID { get; set; }
        public string DoctorID { get; set; }
        public DateTime RecodDate { get; set; }
        public bool IsCheck { get; set; }

        public Person? Patient { get; set; }
    }
}
