using SuperHero.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class ListGroupVM
    {
        public IEnumerable<ChatGroup>? Chat { get; set; }
        public IEnumerable<PersonGroup>? Groups { get; set; }
    }
}
