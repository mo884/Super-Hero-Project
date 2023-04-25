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

        #region Create Comment in Course
        [HttpPost]
        public async Task<IActionResult> Create(Courseview comment)
        {
            try
            {
                //Check if User Sign In or No
                if (signInManager.IsSignedIn(User))
                {
                    //Get User Sign In
                    var user = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                    //Create Comment by using method get two parameter 1- object type of Courseview 2-object of User and return Comment
                    await comments.Create(await Service.Createcomment(comment, user));
                    //return to the get all comment 
                    return RedirectToAction("MyCourse", "CourseView", new {id= comment.CourseId });
                }
                    
                   
                
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            
            return RedirectToAction("MyCourse", "CourseView",new { id = comment.CoursesComment.courseId });
        }
        #endregion
    }
}
