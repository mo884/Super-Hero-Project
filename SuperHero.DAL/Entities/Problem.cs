using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Problem
    {
        [Key]
        public int Id { get; set; }
        
        public string? PathImage { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Title { get; set; }
    }
}
