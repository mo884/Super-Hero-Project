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
        private readonly SignInManager<Person> signInManager;
        #endregion

        #region Ctor
        public CategoriesController(IBaseRepsoratory<Catogery> categories, IMapper mapper, SignInManager<Person> signInManager)
        {
            this.categories = categories;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }
        #endregion

        #region GetAll Category
        public async Task<IActionResult> GetALL()
        {
            if (signInManager.IsSignedIn(User))
            {
                //Get All Category
                var data = await categories.GetAll();
                //Mapper
                var result = mapper.Map<IEnumerable<CategoryVM>>(data);
                return View(result);

            }
            return RedirectToAction("AccessDenied", "Account");

        }
        #endregion

        #region Edite Category
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                //Get Category by Id
                var data = await categories.GetByID(id);
                //Mapper
                var result = mapper.Map<CategoryVM>(data);
                TempData["Message"] = null;
                return PartialView("Edite", result);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Edite(CategoryVM categoryVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        var oldcategory = await categories.GetByID(categoryVM.ID);
                        //Make Update Time Now
                        oldcategory.UpdateTime = DateTime.Now;
                        oldcategory.CategoryName = categoryVM.CategoryName;
                       
                        
                        
                        //Make Update
                        await categories.Update(oldcategory);
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
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Create Category
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (signInManager.IsSignedIn(User))
            {
                TempData["Message"] = null;
                return PartialView("Create");
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Create(CategoryVM categoryVM)
        {
            if (signInManager.IsSignedIn(User))
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
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (signInManager.IsSignedIn(User))
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
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion
    }
}
