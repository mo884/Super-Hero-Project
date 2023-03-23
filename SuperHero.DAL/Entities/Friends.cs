using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class Friends
    {
        public int Id { get; set; }
        public string? FriendId { get; set; }
        [ForeignKey("FriendId")]
        public Person? person { get; set; }
        public string personId { get; set; }
        public bool IsFriend { get; set; }

    }
}
