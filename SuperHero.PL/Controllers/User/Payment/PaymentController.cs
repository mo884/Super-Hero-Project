using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;

namespace SuperHero.PL.Controllers.User.Payment
{

    public class PaymentController : Controller
    {
        #region Prop
        private IConfiguration Configuration;
        private readonly UserManager<Person> userManager;
        
        private readonly SignInManager<Person> signInManager;
        #endregion
        #region ctor
        public PaymentController(IConfiguration Configuration , SignInManager<Person> signInManager, UserManager<Person> userManager)
        {
              this.Configuration = Configuration;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        #endregion
        [HttpGet]
        public IActionResult Pay(int id)
        {
            return PartialView("Pay");
        }
        [HttpPost]
        public async Task<IActionResult>  Pay(BankAccountVM bank)
        {
            //Check if User Sign In or No
            if (signInManager.IsSignedIn(User))
            {
                //Get User Sign In
                var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                if(user.Email == bank.EmailAddress)
                {
                    if (await SendConfitmEmail(user.Email))
                    {
                        TempData["Bank"] = bank;
                        return RedirectToAction("SuccessRegistration");
                    }
                }
            }
            return RedirectToAction("SuccessRegistration");
        }
        public IActionResult Payment()
        {
            return PartialView("Payment", TempData["Bank"]);
        }
        #region ConfirmEmail

        public IActionResult SuccessRegistration()
        {
            return PartialView("SuccessRegistration");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {

               
                return RedirectToAction("Payment", "Payment");
            }
            else
            {
                return RedirectToAction(nameof(AccessDenied));
            }


        }
        #endregion

        #region AccessDenied
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
        #endregion
        #region Private Methods

        private async Task<bool> SendConfitmEmail(string Email)
        {
            var usr = await userManager.FindByEmailAsync(Email);
            if (usr != null)
            {
                var host = Configuration.GetValue<string>("Smtp:Server");
                var Port = Configuration.GetValue<int>("Smtp:Port");
                var fromEmail = Configuration.GetValue<string>("Smtp:UserName");
                var Password = Configuration.GetValue<string>("Smtp:Password");


                var token = await userManager.GenerateEmailConfirmationTokenAsync(usr);
                var confiramtionLink = Url.Action(nameof(ConfirmEmail), "Doctor", new { token, email = usr.Email }, Request.Scheme);
                EmailSetting email = new EmailSetting
                {
                    ToEmail = usr.Email,
                    Name = usr.FullName,



                };
                var TempHtml = $"<a href='{confiramtionLink}'>ConfrmLink</a>";
                var res = MaullSetting.MailSender(host, Port, fromEmail, Password, email, TempHtml);
                if (res != null)
                {

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
