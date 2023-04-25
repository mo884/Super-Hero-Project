using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Interface;
using SuperHero.DAL.Entities;

namespace SuperHero.PL.Controllers.User.Courses
{
    public class CourseViewController : Controller
    {

        #region Prop
        private readonly IBaseRepsoratory<Course> courses;
        private readonly IBaseRepsoratory<CoursesComment> comments;
        private readonly IBaseRepsoratory<Person> person;
        private readonly IMapper mapper;
        private readonly IServiesRep servise;
        #endregion

        #region ctor
        public CourseViewController(IBaseRepsoratory<Course> courses, IBaseRepsoratory<CoursesComment> comments, IBaseRepsoratory<Person> person, IMapper mapper, IServiesRep servise)
        {
            this.courses = courses;
            this.comments = comments;
            this.person = person;
            this.mapper = mapper;
            this.servise = servise;
        }
        #endregion

        #region Get All Courses Ads
        public async Task<IActionResult> Course()
        {
            //Get Course with include trainer and category and Lessons
            var data = await courses.FindAsync("TrainerCourses", "Catogery", "Lessons");
            //Mapp course to CourseVM
            var course = mapper.Map<IEnumerable<CourseVM>>(data);
            return View(course);
        }
        #endregion


        #region get Course with comment and PlayList and componant Course
        public async Task<IActionResult> MyCourse(int id)
        {
            try
            {
                //Get Course By Id 
                var data = await courses.GetByID(id);
                //Mapper
                var model = mapper.Map<Courseview>(data);
                //Get All Lessons of Course(PlayList Lesson of Course)
                model.lessons = await servise.GetLessonByID(id);
                //Get CourseId
                model.CourseId = id;
                //Get Trainer By Course 
                model.trainer = await person.GetByID(model.PersonId);
                //Get Comments By Include Person , Course 
                model.commnts = await servise.GetAllCoursesComment(data.ID, "person", "course");
                model.CoursesComment = model.commnts.FirstOrDefault();

                return PartialView("MyCourse", model);


                //return RedirectToAction("MyCourse");
            }
            catch (Exception)
            {

                return RedirectToAction("GetALL");
            }

        }
        #endregion

    }
}
