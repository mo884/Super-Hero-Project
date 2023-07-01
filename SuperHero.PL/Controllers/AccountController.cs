using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;
using System.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace SuperHero.PL.Controllers
{
    public class AccountController : Controller
    {
        #region Prop
        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        #endregion
       

        #region Ctor
        public AccountController(UserManager<Person> userManager, SignInManager<Person> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
           
        }
        #endregion

        #region Registration 
        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {

            var user = new Person()
            {
                UserName = model.UserName,
                Email = model.Email,
                districtID = 1,
                //GroupID=1
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
              
               
             
                return RedirectToAction("Login");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }

            return View(model);
        }
        #endregion

        #region Login

        public IActionResult Login()
        {
            LoginVM log = new LoginVM();
            return View(log);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            // var userName = await userManager.FindByNameAsync(model.UserName);
            var userEmail = await userManager.FindByEmailAsync(model.Email);

            dynamic result;

            TempData["OFMessage"] = null;


            if (userEmail != null && userEmail.EmailConfirmed)
            {
                result = await signInManager.PasswordSignInAsync(userEmail, model.Password, model.RemberMe, false);
                if (result.Succeeded)
                {
                    if (userEmail.ISDeleted)
                    {

                        model.Message = "You Are Deleted";
                        TempData["OFMessage"] = "You Are Deleted";
                        TempData["modelShow"] = "false";
                        return View(model);

                    }
                    if(userEmail.PersonType == DAL.Enum.PersonType.User || userEmail.PersonType == DAL.Enum.PersonType.Doner)
                        return RedirectToAction("GetAll", "SocialPatient");
                    else
                        return RedirectToAction("GetAll", "Post");


                }


                else
                {
                    model.AccountNotFound = "Wrong Email or Passward !";
                    TempData["OFMessage"] = "Wrong Email or Passward !";
                    TempData["modelShow"] = "false";
                    return View(model);
                }
            }


            else
            {
                model.AccountNotFound = "Wrong Email or Passward !";
                TempData["OFMessage"] = "Wrong Email or Passward !";
                TempData["modelShow"] = "false";
                return View(model);

            }




        }

        #endregion

        #region Sign Out

        [HttpGet]
        public async Task<IActionResult> LogOff( string id)
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion


        [AllowAnonymous]
        public IActionResult AccessDenied()
        {



            return PartialView("AccessDenied");

        }


    }
}
