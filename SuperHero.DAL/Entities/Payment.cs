using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string NameOnCard { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public decimal ExpAmount { get; set; }
    }
}
