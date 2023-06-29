using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.Admin.Social
{
    public class SearchController : Controller
    {
        private readonly IServiesRep _userManager;
        public SearchController(IServiesRep _userManager)
        {
            this._userManager = _userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string query)
        {
            var searchResults = await _userManager.Search(query);



            return PartialView("_SearchResults", searchResults);
        }
    }
}
