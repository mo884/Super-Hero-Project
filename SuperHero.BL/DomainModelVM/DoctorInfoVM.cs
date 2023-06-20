
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SuperHero.DAL.Entities;

namespace SuperHero.BL.DomainModelVM
{
    public class DoctorInfoVM
    {
        public int ID { get; set; }
        public string? CV { get; set; } = null!;
        public string? ClinicAdress { get; set; }
        public string? ClinicName { get; set; }
        public string? MedicalSyndicate { get; set; }
        public int? Rating { get; set; }
        //Nevegation Property
        public IFormFile? Cv_Name { get; set; }
        public string? DectorID { get; set; }
        [ForeignKey("DectorID")]
        public Person ?Person { get; set; }
    }
}
