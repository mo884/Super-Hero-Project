using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SuperHero.PL.Controllers.Admin.Adress
{
    public class CityController : Controller
    {

        #region Prop
        private readonly IBaseRepsoratory<City> city;
        private readonly IBaseRepsoratory<Governorate> governate;
        private readonly IMapper mapper;
        #endregion

        #region Ctor
        public CityController(IBaseRepsoratory<City> city, IBaseRepsoratory<Governorate> governate, IMapper mapper)
        {
            this.city = city;
            this.governate = governate;
            this.mapper = mapper;
        }
        #endregion

        #region Get All City
        public async Task<IActionResult> GetAll()
        {
            var cityList = await city.FindAsync("Governorate", "Districts");
            var CityListVM = mapper.Map<IEnumerable<CityVM>>(cityList);
            return View(CityListVM);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categoryList = new SelectList(await governate.GetAll(), "ID", "Name");
            TempData["Message"] = null;
            return PartialView("Create");
        }
        public async Task<IActionResult> Create(CityVM cityVM)
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
        #endregion

        #region Edit

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var data = await city.GetByID(id);
            var result = mapper.Map<CityVM>(data);
            ViewBag.categoryList = new SelectList(await governate.GetAll(), "ID", "Name");
            TempData["Message"] = null;
            return PartialView("Edite", result);
        }
        public async Task<IActionResult> Edite(CityVM cityVM)
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
        #endregion




    }
}
