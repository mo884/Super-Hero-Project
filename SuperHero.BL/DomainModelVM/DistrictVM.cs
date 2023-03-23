
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class DistrictVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        public int CityId { get; set; }
        public CityVM City { get; set; }
        public List<CreatePerson> Person { get; set; }
    }
}
