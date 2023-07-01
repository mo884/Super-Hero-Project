using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class MailVM
    {
        [Required(ErrorMessage ="Email Required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Message Required")]

        public string Message { get; set; }
    }
}
