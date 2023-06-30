using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.DAL.Entities
{
    public class NotificationMessage
    {
        [Key]
        public int Id { get; set; }
        public string? Notification { get; set; }
       
        public string? SenderId { get; set; }
        [ForeignKey("SenderId")]
        public Person? person { get; set; }

        public string? ReciverID { get; set; }
        public bool Show { get; set; }
    }
}
