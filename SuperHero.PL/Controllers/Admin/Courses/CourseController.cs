﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Seeds;
using System.Runtime.Intrinsics.Arm;

namespace SuperHero.PL.Controllers.Admin.Courses
{
    [Authorize(Roles = AppRoles.Admin)]
    public class CourseController : Controller
    {

        #region Prop
        private readonly IBaseRepsoratory<Course> courses;
        private readonly IBaseRepsoratory<Catogery> category;
        private readonly IMapper mapper;
        #endregion

        #region Ctor
        public CourseController(IBaseRepsoratory<Course> courses, IBaseRepsoratory<Catogery> category, IMapper mapper)
        {
            this.courses = courses;
            this.category = category;
            this.mapper = mapper;
        }
        #endregion

        #region GetAll Course
        public async Task<IActionResult> GetALL()
        {

            var data = await courses.FindAsync("Catogery");
            var result = mapper.Map<IEnumerable<CourseVM>>(data);
            return View(result);
        }
        #endregion

        #region Edite Category
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            var data = await courses.GetByID(id);
            var result = mapper.Map<CourseVM>(data);
            ViewBag.ID = "Edite";

            ViewBag.categoryList = new SelectList(await category.GetAll(), "ID", "CategoryName");
            TempData["Message"] = null;
            return View("Form", result);
        }
        public async Task<IActionResult> Edite(CourseVM courseVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    courseVM.Image = FileUploader.UploadFile("Courses", courseVM.ImageName);
                    courseVM.UpdateTime = DateTime.Now;
                    var result = mapper.Map<Course>(courseVM);
                    await courses.Update(result);
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

            ViewBag.ID = "Edite";
            return View("Form", courseVM);
        }
        #endregion

        #region Create Category
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categoryList = new SelectList(await category.GetAll(), "ID", "CategoryName");
            ViewBag.ID = null;
            TempData["Message"] = null;
            return View("Form");
        }
        public async Task<IActionResult> Create(CourseVM CourseVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    CourseVM.Image = FileUploader.UploadFile("Courses", CourseVM.ImageName);
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
        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await courses.GetByID(id);
            var result = mapper.Map<CourseVM>(data);

            ViewBag.Delete = "Delete";
            ViewBag.ID = "Edite";
            return View("Form", result);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(CourseVM obj)
        {


            try
            {
                if (ModelState.IsValid)
                {

                    var result = mapper.Map<Course>(obj);


                    await courses.Delete(result.ID);
                    return RedirectToAction("GetAll");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }




            var Dpt = await courses.GetAll();
            ViewBag.Delete = "Delete";
            ViewBag.ID = "Edite";
            return View("Form", obj);

        }
        #endregion
    }
}
