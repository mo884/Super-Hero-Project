using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class ProblemVM
    {
        [Key]
        public int Id { get; set; }
        
        public string? PathImage { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]

        public string? Title { get; set; }
        public IFormFile Image { get; set; }
    }
}
