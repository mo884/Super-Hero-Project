using FRYMA_SuperHero.BL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly IBaseRepsoratory<City> city;

        public HomeController(IBaseRepsoratory<City> city)
        {
            this.city = city;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
      
        public IActionResult Test2()
        {
            return View();
        }
        public async Task <IActionResult> Test3()
        {
            var data = await city.GetAll();
            return View(data);
        }
    }
}
