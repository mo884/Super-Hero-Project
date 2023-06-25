using Microsoft.AspNetCore.Mvc;

namespace SuperHero.PL.Controllers
{
    public class ChatHubController : Controller
    {
        public IActionResult Index()
        {
            Random randomNumber = new Random();
            int RnadomSession = randomNumber.Next(0, 955121135); 
            HttpContext.Session.SetInt32("UserId", RnadomSession);
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }
    }
}
