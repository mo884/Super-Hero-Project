using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;
using SuperHero.BL.Seeds;

namespace SuperHero.PL.Controllers.User.Courses
{
    public class CommentOfCourseController : Controller
    {

        #region Prop
        private readonly IBaseRepsoratory<CoursesComment> comments;
        private readonly SignInManager<Person> signInManager;
        #endregion
        #region Ctor
        public CommentOfCourseController(IBaseRepsoratory<CoursesComment>comments, SignInManager<Person> signInManager)
        {
            this.comments = comments;
            this.signInManager = signInManager;
        }
        #endregion
        [HttpPost]
        public async Task<IActionResult> Create(Courseview comment)
        {
            try
            {
                if (signInManager.IsSignedIn(User))
                {
                    var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                    comment.CoursesComment.UserId = user.Id;
                    comment.CoursesComment.CreateTime = DateTime.Now;
                    await comments.Create(comment.CoursesComment);
                    return RedirectToAction("MyCourse", "CourseView", new {id=comment.CoursesComment.courseId});

                }
                    
                   
                
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            
            return RedirectToAction("MyCourse", "CourseView",new { id = comment.CoursesComment.courseId });
        }
    }
}
