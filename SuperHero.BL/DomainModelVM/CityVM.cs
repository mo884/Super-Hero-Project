
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class CityVM
    {

        public int ID { get; set; }
        public string? Name { get; set; }
        //Navegation Property
        public int GovernorateID { get; set; }

        
        public GovernorateVM Governorate { get; set; }
        public List<DistrictVM> Districts { get; set; }
    }
}
