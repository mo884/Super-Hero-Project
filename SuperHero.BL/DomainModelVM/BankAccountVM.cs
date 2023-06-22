using SuperHero.BL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class BankAccountVM
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? NameOnCard { get; set; }
        public string? EmailAddress { get; set; }
        public string? Address { get; set; } 
        public string? City { get; set; }
        public string? DD { get; set; }
        public string? MM { get; set; }
        public string? YYYY { get; set; }

      
       

        public string? CardNumber { get; set; }
        public int? CardCVC { get; set; }
        public int? ExpMonth { get; set; }
        public int? ExpYear { get; set; } = 0;
        public decimal? EnterAmount { get; set; }
    }
}
