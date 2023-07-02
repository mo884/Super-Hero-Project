using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;

namespace SuperHero.PL.Controllers.PatientProfile
{
    public class PatientCourseController : Controller
    {
        #region Prop
        private readonly IBaseRepsoratory<Course> courses;
        private readonly IBaseRepsoratory<CoursesComment> comments;
        private readonly IBaseRepsoratory<Person> person;
        private readonly IMapper mapper;
        private readonly IServiesRep servise;
        private readonly SignInManager<Person> signInManager;
        #endregion

        #region ctor
        public PatientCourseController(IBaseRepsoratory<Course> courses, IBaseRepsoratory<CoursesComment> comments, IBaseRepsoratory<Person> person, IMapper mapper, IServiesRep servise, SignInManager<Person> signInManager)
        {
            this.courses = courses;
            this.comments = comments;
            this.person = person;
            this.mapper = mapper;
            this.servise = servise;
            this.signInManager = signInManager;
        }
        #endregion

        #region Get All Courses Ads
        public async Task<IActionResult> Course()
        {
            if (signInManager.IsSignedIn(User))
            {
                //Get Course with include trainer and category and Lessons
                var data = await courses.FindAsync("TrainerCourses", "Catogery", "Lessons");
                //Mapp course to CourseVM
                var course = mapper.Map<IEnumerable<CourseVM>>(data);
                return View(course);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

    }
}
