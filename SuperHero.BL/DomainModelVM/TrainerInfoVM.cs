using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SuperHero.BL.DomainModelVM
{
    public class TrainerInfoVM
    {
        [Key]
        public int? ID { get; set; }


        public string? Graduation { get; set; }
        public string? CV { get; set; } = null!;

        //Nevegation Property
        public IFormFile? Cv_Name { get; set; }

    }
}
