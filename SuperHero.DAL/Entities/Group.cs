using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Group
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public string? Image { get; set; }
        //Nevegation Property
        public List<PersonGroup> Personsgroup { get; set; }
        public List<ChatGroup>? ChatGroup { get; set; }
    }
}
