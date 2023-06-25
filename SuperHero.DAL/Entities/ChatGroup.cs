
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class ChatGroup
    {
        [Key]
        public int Id { get; set; }
        public string? Message { get; set; }
        public Group? group { get; set; }
        public int? groupId { get; set; }
        public string? PersonId { get; set; }
        public Person? Person { get; set; }

    }
}
