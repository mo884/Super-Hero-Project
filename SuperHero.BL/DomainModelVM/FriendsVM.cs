using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class FriendsVM
    {
       
        public string? FriendId { get; set; }
        public bool? IsFriends { get; set; }
        [ForeignKey("personId")]
        public Person? person { get; set; }
        public string? personId { get; set; }
    }
}
