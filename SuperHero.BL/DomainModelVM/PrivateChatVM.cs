using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class PrivateChatVM
    {
        public IEnumerable<PrivateChat> Chats { get; set; }
        public Person? Reciver { get; set; }
    }
}
