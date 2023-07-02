using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.SecondPage
{

    public class BestDoctorController : Controller
    {
        private readonly IServiesRep servies;
        private readonly UserManager<Person> userManager;
        private readonly SignInManager<Person> signInManager;
        public BestDoctorController(IServiesRep servies, UserManager<Person> userManager, SignInManager<Person> signInManager)
        {
            this.servies = servies;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public async Task<IActionResult>  Index()
        {
            var data = await servies.BestDoctor();
            return View(data);
        }

       
    }
}
