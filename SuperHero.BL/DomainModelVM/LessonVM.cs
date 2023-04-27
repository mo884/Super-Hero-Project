
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
    public class LessonVM
    {

        public int ID { get; set; }
        [Required(ErrorMessage ="*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public int Num { get; set; }
      
        public string? video { get; set; }

        //Navegation Property
        public int? CourseID { get; set; }
     
        public IFormFile? videoName { get; set; }
        public Course? Course { get; set; }
    }
}
