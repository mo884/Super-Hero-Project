
using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class CityVM
    {

        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        //Navegation Property
        public int? GovernorateID { get; set; }

        [ForeignKey("GovernorateID")]
        public Governorate? Governorate { get; set; }
        public List<District>? Districts { get; set; }
    }
}
