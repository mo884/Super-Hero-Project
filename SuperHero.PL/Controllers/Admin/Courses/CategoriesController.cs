using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.Admin.Courses
{
    public class CategoriesController : Controller
    {


        #region Prop
        private readonly IBaseRepsoratory<Catogery> categories;
        private readonly IMapper mapper;
        #endregion

        #region Ctor
        public CategoriesController(IBaseRepsoratory<Catogery> categories, IMapper mapper)
        {
            this.categories = categories;
            this.mapper = mapper;
        }
        #endregion

        #region GetAll Category
        public async Task<IActionResult> GetALL()
        {

            var data = await categories.GetAll();
            var result = mapper.Map<IEnumerable<CategoryVM>>(data);
            return View(result);
        }
        #endregion

        #region Edite Category
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            var data = await categories.GetByID(id);
            var result = mapper.Map<CategoryVM>(data);
            ViewBag.ID = "Edite";
            TempData["Message"] = null;
            return View("Form", result);
        }
        public async Task<IActionResult> Edite(CategoryVM categoryVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    categoryVM.UpdateTime = DateTime.Now;
                    var result = mapper.Map<Catogery>(categoryVM);
                    await categories.Update(result);
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


            ViewBag.ID = "Edite";
            return View("Form", categoryVM);
        }
        #endregion

        #region Create Category
        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.ID = "Edite";
            TempData["Message"] = null;
            return View("Form");
        }
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = mapper.Map<Catogery>(categoryVM);
                    await categories.Create(result);
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


            ViewBag.ID = "Edite";
            return View("Form", categoryVM);
        }
        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await categories.GetByID(id);
            var result = mapper.Map<CategoryVM>(data);

            ViewBag.Delete = "Delete";
            ViewBag.ID = null;
            return View("Form", result);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(CategoryVM obj)
        {


            try
            {
                if (ModelState.IsValid)
                {

                    var result = mapper.Map<Catogery>(obj);


                    await categories.Delete(result.ID);
                    return RedirectToAction("GetAll");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }




            var Dpt = await categories.GetAll();
            ViewBag.Delete = "Delete";
            ViewBag.ID = null;
            return View("Form", obj);

        }
        #endregion
    }
}
