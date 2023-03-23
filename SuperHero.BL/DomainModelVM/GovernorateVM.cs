
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class GovernorateVM
    {
        public int ID { get; set; }
        public string? Name { get; set; }

        //Navegation Property
        public List<CityVM> Cities { get; set; }
    }
}
