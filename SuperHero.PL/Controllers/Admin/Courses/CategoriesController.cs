using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;
using System.Data;

namespace SuperHero.PL.Controllers.Admin.Courses
{
    [Authorize(Roles = @$"{AppRoles.Admin},{AppRoles.Trainer}")]
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
            //Get All Category
            var data = await categories.GetAll();
            //Mapper
            var result = mapper.Map<IEnumerable<CategoryVM>>(data);
            return View(result);
        }
        #endregion

        #region Edite Category
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            //Get Category by Id
            var data = await categories.GetByID(id);
            //Mapper
            var result = mapper.Map<CategoryVM>(data);
            TempData["Message"] = null;
            return PartialView("Edite", result);
        }
        public async Task<IActionResult> Edite(CategoryVM categoryVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //Make Update Time Now
                    categoryVM.UpdateTime = DateTime.Now;
                    //Mapper
                    var result = mapper.Map<Catogery>(categoryVM);
                    //Make Update
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
            return RedirectToAction("GetAll");
        }
        #endregion

        #region Create Category
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            TempData["Message"] = null;
            return PartialView("Create");
        }
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    //MAke Time of create Now
                    categoryVM.createdTime = DateTime.Now;
                    //Mapper
                    var result = mapper.Map<Catogery>(categoryVM);
                    //Create Category
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
            return RedirectToAction("GetAll");
        }
        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            //Get Category By Id
            var data = await categories.GetByID(id);

            if (data is null)
                return NotFound();
            //Make Is Deleted
            var Course = await Service.Delete(data);
            //Make Update
            await categories.Update(Course);
            return Ok();
        }
        #endregion
    }
}
