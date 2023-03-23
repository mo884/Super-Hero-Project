using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class PersonCourses
    {
        [Key]
        public int ID { get; set; }
        public PersonType PersonType { get; set; }
        public int CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course Course { get; set; }
        public string PersonID { get; set; }
        [ForeignKey("PersonID")]
        public Person Person { get; set; }
    }
}
