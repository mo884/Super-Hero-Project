using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.DomainModelVM;
using SuperHero.BL.Seeds;

namespace SuperHero.PL.Controllers.Admin.Courses
{
    [Authorize(Roles = AppRoles.Admin)]
    public class LessonController : Controller
    {
        #region prop
        private readonly IBaseRepsoratory<Lesson> lessons;
        private readonly IBaseRepsoratory<Course> courses;
        private readonly IMapper mapper;
        #endregion

        #region Ctor
        public LessonController(IBaseRepsoratory<Lesson> lessons, IBaseRepsoratory<Course> courses, IMapper mapper)
        {
            this.courses = courses;
            this.lessons = lessons;
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
            ViewBag.ID = "Edite";

            ViewBag.courseList = new SelectList(await courses.GetAll(), "ID", "NameCourse");
            TempData["Message"] = null;
            return View("Form", result);
        }
        public async Task<IActionResult> Edite(LessonVM lessonVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    lessonVM.video = FileUploader.UploadFile("Courses", lessonVM.videoName);
                    var result = mapper.Map<Lesson>(lessonVM);
                    await lessons.Update(result);
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

            ViewBag.ID = "Edite";
            return View("Form", lessonVM);
        }
        #endregion

        #region Create Category
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.courseList = new SelectList(await courses.GetAll(), "ID", "NameCourse");
            ViewBag.ID = null;
            TempData["Message"] = null;
            return View("Form");
        }
        public async Task<IActionResult> Create(LessonVM lessonVM)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    lessonVM.video = FileUploader.UploadFile("Courses", lessonVM.videoName);
                    var result = mapper.Map<Lesson>(lessonVM);
                    await lessons.Create(result);
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

            ViewBag.ID = null;
            return View("Form", lessonVM);
        }
        #endregion

        #region Delete

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await lessons.GetByID(id);
            var result = mapper.Map<CourseVM>(data);

            ViewBag.Delete = "Delete";
            ViewBag.ID = "Edite";
            return View("Form", result);
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




            var Dpt = await lessons.GetAll();
            ViewBag.Delete = "Delete";
            ViewBag.ID = "Edite";
            return View("Form", obj);

        }
        #endregion
    }
}
