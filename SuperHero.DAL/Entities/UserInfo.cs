using SuperHero.DAL.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class UserInfo
    {

        [Key]
        public int ID { get; set; }

        [Required]
        public Reason Reason { get; set; }
        //Nevegation Property
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public Person Person { get; set; }
        public List<Analysis> Analyses { get; set; }
        public List<Treatment> Treatments { get; set; }
        public List<Radiology> radiologies { get; set; }
    }
}
