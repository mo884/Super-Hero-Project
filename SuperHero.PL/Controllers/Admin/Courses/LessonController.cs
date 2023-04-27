using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;

namespace SuperHero.PL.Controllers.Admin.Courses
{
    //[Authorize(Roles = AppRoles.Admin)]
    public class LessonController : Controller
    {
        #region prop
        private readonly IBaseRepsoratory<Lesson> lessons;
        private readonly IServiesRep servies;
        private readonly IBaseRepsoratory<Course> courses;
        private readonly IMapper mapper;
        #endregion

        #region Ctor
        public LessonController(IBaseRepsoratory<Lesson> lessons, IServiesRep servies , IBaseRepsoratory<Course> courses, IMapper mapper)
        {
            this.courses = courses;
            this.lessons = lessons;
            this.servies = servies;
            this.mapper = mapper;
        }
        #endregion

        #region GetAll Category
        public async Task<IActionResult> GetALL()
        {

            var data = await lessons.FindAsync("Course");
            var result = mapper.Map<IEnumerable<LessonVM>>(data);
            return View(result);
        }
        #endregion

        #region Edite Category
        [HttpGet]
        public async Task<IActionResult> Edite(int id)
        {
            var data = await lessons.GetByID(id);
            var result = mapper.Map<LessonVM>(data);
            ViewBag.courseList = new SelectList(await courses.GetAll(), "ID", "NameCourse");
            TempData["Message"] = null;
            return PartialView("Edite", result);
        }
        public async Task<IActionResult> Edite(LessonVM lessonVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    await servies.EditeLessonByID(lessonVM);
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
            ViewBag.courseList = new SelectList(await courses.GetAll(), "ID", "NameCourse");
            return PartialView("Edite", lessonVM);
        }
        #endregion

        #region Create Category
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
           
            TempData["CourseID"] = id;
            return PartialView("Create");
        }
        [HttpPost]
        public async Task<IActionResult> CreateLesson(LessonVM lessonVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    lessonVM.CourseID = (int)TempData["CourseID"];
                    if (lessonVM.videoName == null)
                        return RedirectToAction("GetAll", "Course");
                    lessonVM.video = FileUploader.UploadFile("Courses", lessonVM.videoName);
                    var result = mapper.Map<Lesson>(lessonVM);
                   
                    
                    await lessons.Create(result);
                    TempData["Message"] = "saved Successfuly";
                    return RedirectToAction("GetAll", "Course");
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }


            return RedirectToAction("GetAll", "Course");
        }
        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await lessons.GetByID(id);
            var result = mapper.Map<LessonVM>(data);
            return PartialView("Delete", result);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(LessonVM obj)
        {


            try
            {
               
                if (ModelState.IsValid)
                {

                    var result = mapper.Map<Lesson>(obj);


                    await lessons.Delete(result.ID);
                    return RedirectToAction("GetAll");
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("GetAll");

        }
        #endregion
    }
}
