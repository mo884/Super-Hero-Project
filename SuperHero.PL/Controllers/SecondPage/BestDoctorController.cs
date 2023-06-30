using Microsoft.AspNetCore.Mvc;

namespace SuperHero.PL.Controllers.SecondPage
{
    public class BestDoctorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
