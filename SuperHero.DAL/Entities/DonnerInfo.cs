using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class DonnerInfo
    {
        [Key]
        public int ID { get; set; }

        public string DonationType { get; set; }
        public string DonnerID { get; set; }
        [ForeignKey("DonnerID")]
        public Person Person { get; set; }
    }
}
