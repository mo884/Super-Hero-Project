using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class PrivateChat
    {
        [Key]
        public int ID { get; set; }
        public string Message { get; set; }
        public string RecivierID { get; set; }
        [ForeignKey("Sender")]
        public string SenderID { get; set; }
        public Person Sender { get; set; }
    }
}
