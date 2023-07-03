using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SuperHero.PL.Controllers.Admin.Adress
{

    public class CityController : Controller
    {

        #region Prop
        private readonly IBaseRepsoratory<City> city;
        private readonly IBaseRepsoratory<Governorate> governate;
        private readonly SignInManager<Person> signInManager;
        private readonly IMapper mapper;
        #endregion

        #region Ctor
        public CityController(IBaseRepsoratory<City> city, SignInManager<Person> signInManager, IBaseRepsoratory<Governorate> governate, IMapper mapper)
        {
            this.city = city;
            this.governate = governate;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }
        #endregion

        #region Get All City
        public async Task<IActionResult> GetAll()
        {
            if (signInManager.IsSignedIn(User))
            {
                var cityList = await city.FindAsync("Governorate", "Districts");
                var CityListVM = mapper.Map<IEnumerable<CityVM>>(cityList);
                return View(CityListVM);
            }
            return RedirectToAction("Login", "Account");

        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (signInManager.IsSignedIn(User))
            {
                ViewBag.categoryList = new SelectList(await governate.GetAll(), "ID", "Name");
                TempData["Message"] = null;
                return PartialView("Create");
            }
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Create(CityVM cityVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        var result = mapper.Map<City>(cityVM);
                        await city.Create(result);
                        TempData["Message"] = "saved Successfuly";
                        return RedirectToAction("GetAll");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

                //ModelState.Clear();
                TempData["Message"] = null;
                ViewBag.categoryList = new SelectList(await governate.GetAll(), "ID", "Name");

                ViewBag.ID = null;
                return View("Form", cityVM);
            }
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await city.GetByID(id);
                var result = mapper.Map<CityVM>(data);
                ViewBag.categoryList = new SelectList(await governate.GetAll(), "ID", "Name");
                TempData["Message"] = null;
                return PartialView("Edite", result);
            }
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> Edite(CityVM cityVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        var result = mapper.Map<City>(cityVM);
                        await city.Update(result);
                        TempData["Message"] = "saved Successfuly";
                        return RedirectToAction("GetAll");
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

                //ModelState.Clear();
                TempData["Message"] = null;
                ViewBag.categoryList = new SelectList(await governate.GetAll(), "ID", "Name");
                return PartialView("Edite", cityVM);
            }
            return RedirectToAction("Login", "Account");
        }
        #endregion




    }
}
