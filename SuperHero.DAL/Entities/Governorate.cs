using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Governorate
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }

        //Navegation Property
        public List<City> Cities { get; set; }
    }
}
