using Microsoft.AspNetCore.Mvc;

namespace SuperHero.PL.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
