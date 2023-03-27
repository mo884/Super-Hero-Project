using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;
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
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            // var userName = await userManager.FindByNameAsync(model.UserName);
            var userEmail = await userManager.FindByEmailAsync(model.Email);

            dynamic result;




            if (userEmail != null)
            {
                result = await signInManager.PasswordSignInAsync(userEmail, model.Password, model.RemberMe, false);
                if (result.Succeeded)
                {
                   
                    return RedirectToAction("GetAll", "Person");
                }


                else
                {
                    ModelState.AddModelError("", "Invalid UserName Or Password");

                }
            }


            else
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");

            }



            return View(model);
        }

        #endregion
   
        #region Sign Out

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        #endregion

    }
}
