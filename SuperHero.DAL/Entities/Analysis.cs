using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Analysis
    {
        [Key]
        public int ID { get; set; }
        
        public string? AnalysisPDF { get; set; }
        public int? personID { get; set; }

        [ForeignKey("personID")]
        public UserInfo patient { get; set; }

    }
}
