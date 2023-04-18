using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Course
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string NameCourse { get; set; }
        public string? Description { get; set; } = null!;
        public DateTime DateOfPuplish { get; set; }
        [Required]
        public int Hours { get; set; }
        public string? Image { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
        //Navegation  Property

        public List<Lesson>? Lessons { get; set; }
        
        public string? PersonId { get; set; }
        [ForeignKey("PersonId")]
        public Person? TrainerCourses { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Catogery? Catogery { get; set; }
        public List<CoursesComment>? CoursesComments { get; set; }
    }
}
