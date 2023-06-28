using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Message { get; set; }
        [ForeignKey("person")]
        public string? SenderID { get; set; }
        public Person? person { get; set; }
        public string? RecieverID { get; set; }
    }
}
