using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.Admin.Social
{
    public class SearchController : Controller
    {
        private readonly IServiesRep _userManager;
        private readonly SignInManager<Person> signInManager;
        public SearchController(IServiesRep _userManager, SignInManager<Person> signInManager)
        {
            this._userManager = _userManager;
            this.signInManager = signInManager;

        }
        [HttpGet]
        public IActionResult Index()
        {
            if (signInManager.IsSignedIn(User))
            {
                return View();
            }
            return RedirectToAction("AccessDenied", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            if (signInManager.IsSignedIn(User))
            {
                var searchResults = await _userManager.Search(query);



                return PartialView("_SearchResults", searchResults);
            }
            return RedirectToAction("AccessDenied", "Account");

        }
    }
}
