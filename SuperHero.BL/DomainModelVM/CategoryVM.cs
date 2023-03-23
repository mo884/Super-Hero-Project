
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperHero.DAL.Entities;

namespace SuperHero.BL.DomainModelVM
{
    public class CategoryVM
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public DateTime createdTime { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? UpdateTime { get; set; }

        //Navegation Property
        public List<Course>? Courses { get; set; }


    }
}
