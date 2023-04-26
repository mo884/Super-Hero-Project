
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperHero.BL.DomainModelVM
{
    public class LoginVM
    {

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
      
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Message { get; set; } = "";
        public string AccountNotFound { get; set; } = "";
        public string UserName { get; set; }
        public bool RemberMe { get; set; } = true;
    }
}
