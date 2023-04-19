﻿using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Course()
        {
            var data = await courses.FindAsync("TrainerCourses", "Catogery", "Lessons");
            var course = mapper.Map<IEnumerable< CourseVM>>(data);
            return View(course);
        }
        public async Task<IActionResult> MyCourse(int id)
        {
            try
            {
                var data = await courses.GetByID(id);
               
                   
                    var model = mapper.Map<Courseview>(data);
                    model.lessons = await servise.GetLessonByID(id);
                    model.trainer = await person.GetByID(model.PersonId);
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
    }
}
