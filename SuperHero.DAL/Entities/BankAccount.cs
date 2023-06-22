using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string CardNumber { get ; set; }
        public int CardCVC { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; } = 0;
        public decimal EnterAmount { get; set; }
    }
}
