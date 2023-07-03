using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Helper;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;
using System.Runtime.Intrinsics.Arm;

namespace SuperHero.PL.Controllers.Admin.Courses
{
    [Authorize(Roles = @$"{AppRoles.Admin},{AppRoles.Trainer}")]
    public class CourseController : Controller
    {


        #region Prop
        private readonly IBaseRepsoratory<Course> courses;
        private readonly IBaseRepsoratory<Catogery> category;
        private readonly IMapper mapper;
        private readonly SignInManager<Person> signInManager;
        #endregion

        #region Ctor
        public CourseController(SignInManager<Person> signInManager, IBaseRepsoratory<Course> courses, IBaseRepsoratory<Catogery> category, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.courses = courses;
            this.category = category;
            this.mapper = mapper;
        }
        #endregion

        #region GetAll Course
        public async Task<IActionResult> GetALL()
        {
            if (signInManager.IsSignedIn(User))
            {
                var data = await courses.FindAsync("Catogery", "Lessons");
                var result = mapper.Map<IEnumerable<CourseVM>>(data);
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
                var data = await courses.GetByID(id);
                var result = mapper.Map<CourseVM>(data);
                ViewBag.categoryList = new SelectList(await category.GetAll(), "ID", "CategoryName");
                TempData["Message"] = null;
                return PartialView("Edite", result);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Edite(CourseVM courseVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        var oldCourse = await courses.GetByID(courseVM.ID);
                        if(courseVM.ImageName != null)
                        {
                            oldCourse.Image = FileUploader.UploadFile("Courses", courseVM.ImageName);

                        }

                        oldCourse.PersonId = oldCourse.PersonId;
                        oldCourse.UpdateTime = DateTime.Now;
                       
                        await courses.Update(oldCourse);
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
                ViewBag.categoryList = await category.GetAll();
                return View("Edite", courseVM);
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
                ViewBag.categoryList = new SelectList(await category.GetAll(), "ID", "CategoryName");
                ViewBag.ID = null;
                TempData["Message"] = null;
                return PartialView("Create");
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        public async Task<IActionResult> Create(CourseVM CourseVM)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        CourseVM.Image = FileUploader.UploadFile("Courses", CourseVM.ImageName);
                        var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                        CourseVM.PersonId = PersonProfile.Id;
                        var result = mapper.Map<Course>(CourseVM);
                        await courses.Create(result);
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
                ViewBag.categoryList = new SelectList(await category.GetAll(), "ID", "CategoryName");

                ViewBag.ID = null;
                return View("Form", CourseVM);
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
                var data = await courses.GetByID(id);

                if (data is null)
                    return NotFound();
                var Course = await Service.Delete(data);
                await courses.Update(Course);
                return Ok();
            }
            return RedirectToAction("AccessDenied", "Account");
        }



        #endregion
    }
}
