using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class CoursesComment
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Write Comment")]
        public string Body { get; set; }
        public DateTime CreateTime { get; set; }
        [ForeignKey("course")]
        public int courseId { get; set; }
        public Course course{ get; set; }
        [ForeignKey("person")]
        public string? UserId { get; set; }
        public Person? person { get; set; }
    }
}
