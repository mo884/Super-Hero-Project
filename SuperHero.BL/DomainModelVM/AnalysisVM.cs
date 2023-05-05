﻿using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class AnalysisVM
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? AnalysisPDF { get; set; }
        public int? personID { get; set; }

        [ForeignKey("personID")]
        public UserInfo? patient { get; set; }
    }
}
