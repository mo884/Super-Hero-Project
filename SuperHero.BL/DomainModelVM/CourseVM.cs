
using Microsoft.AspNetCore.Http;
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
    public class CourseVM
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string NameCourse { get; set; }
        public DateTime DateOfPuplish { get; set; }
        [Required]
        public int Hours { get; set; }
        public DateTime? UpdateTime { get; set; }
       
        public bool IsDelete { get; set; }

        public List<Lesson>? Lessons { get; set; } = null!;
        public string? Image { get; set; }

        //Navegation  Property
        public IFormFile? ImageName { get; set; }
        public string? PersonId { get; set; }
        public string? Description { get; set; } = null!;
        public Person? TrainerCourses { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Catogery? Catogery { get; set; } = null!;
    }
}
