
using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class PerssonCourseVM
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="*")]
        public PersonType PersonType { get; set; }
        public int CourseID { get; set; }
       
        public CourseVM Course { get; set; }
        public int PersonID { get; set; }
       
        public CreatePerson Person { get; set; }
    }
}
