using Microsoft.AspNetCore.Http;
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class PatientInfo
    {
        public int ID { get; set; }
        public int? ProfileID { get; set; }
        public UserInfo? patient { get; set; }
        public Person? User { get; set; }
        public AnalysisVM? analysis { get; set; }
        public List<AnalysisVM>? AnalysisVMs { get; set; }
        public List<TreatmentVM>? TreatmentVMs { get; set; }
        public List<RadiologyVM>? RadiologyVMs { get; set; }
        [Required(ErrorMessage ="Upload File")]
        public IFormFile uploade { get; set; }
        
        public string? Name { get; set; }
    }
}
