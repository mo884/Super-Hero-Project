using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Lesson
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Num { get; set; }
        public string? video { get; set; }

        //Navegation Property
        public int? CourseID { get; set; }
        [ForeignKey("CourseID")]
        public Course? Course { get; set; } = null!;
    }
}
