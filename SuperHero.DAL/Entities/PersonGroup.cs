using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class PersonGroup
    {
        public int ID { get; set; }
        public string PersonId { get; set; } = null !;
        [ForeignKey("PersonId")]
        public Person Person { get; set; }
       
        public int? Group { get; set; }
        [ForeignKey("Group")]
        public Group group { get; set; }

    }
}
