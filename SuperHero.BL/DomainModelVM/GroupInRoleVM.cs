using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class GroupInRoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? FullName { get; set; }
        public Gender? gender { get; set; }
        public string? Image { get; set; }
        public bool IsSelected { get; set; }
    }
}
