using Microsoft.AspNetCore.Http;
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class Courseview
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
        public int CourseId { get; set; } = 0;
        public string? Description { get; set; } = null!;
        public string? Image { get; set; }

        //Navegation  Property
        public IFormFile? ImageName { get; set; }
        public IEnumerable<Lesson>? lessons { get; set; } 
        public IEnumerable<CoursesComment>? commnts { get; set; }
        public int? CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public Catogery? Catogery { get; set; } = null!;
        public CoursesComment? CoursesComment { get; set; } = null!;
        public string? PersonId { get; set; }
        public Person? trainer { get; set; }
    }
}
