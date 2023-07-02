using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SuperHero.BL.Helper;
using SuperHero.BL.Interface;
using SuperHero.BL.Seeds;
using SuperHero.DAL.Entities;
using System.Data;

namespace SuperHero.PL.Controllers.PatientProfile
{
    //[Authorize(Roles = $"{AppRoles.User}")]
    [Route("/api/Social")]
    public class SocialPatientController : Controller
    {
        #region Prop
        private readonly IBaseRepsoratory<Person> person;
        private readonly IBaseRepsoratory<Friends> allfriends;
        private readonly IBaseRepsoratory<Comment> comment;
        private readonly IBaseRepsoratory<ReactPost> react;
        private readonly IBaseRepsoratory<Post> post;
        private readonly IMapper mapper;
        private readonly IServiesRep servies;
        private readonly SignInManager<Person> signInManager;
        #endregion


        #region ctor
        public SocialPatientController(IBaseRepsoratory<Person> person, IBaseRepsoratory<Friends> allfriends, IBaseRepsoratory<Comment> comment, IBaseRepsoratory<ReactPost> react, IBaseRepsoratory<Post> post, IMapper mapper, IServiesRep servies, SignInManager<Person> signInManager)
        {
            this.person = person;
            this.allfriends = allfriends;
            this.comment = comment;
            this.react = react;
            this.post = post;
            this.mapper = mapper;
            this.servies = servies;
            this.signInManager = signInManager;
        }
        #endregion

        #region GetAll
        [HttpGet("Communication")]
        public async Task<IActionResult> GetALL()
        {
            if (signInManager.IsSignedIn(User))
            {
                var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                var SocialGetAll = await servies.GetAllSocial(PersonProfile);
                return View(SocialGetAll);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion

        #region GetComment
        [HttpGet("Comments")]
        public async Task<IActionResult> Comments(int id)
        {
            if (signInManager.IsSignedIn(User))
            {
                //Get Comment With Include Person And Post
                var comment = await servies.GetAll(id, "person", "post");
                //Mapper
                var comments = mapper.Map<IEnumerable<CommentVM>>(comment);
                //Use Class To Send The PostId and The Comments To send to Partial View 
                return PartialView("GetComments", new CommentServise
                {
                    PostID = id,
                    comment = comments
                });
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion


        #region Add Comment
        public async Task<IActionResult> Create(CommentServise model)
        {
            if (signInManager.IsSignedIn(User))
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        var PersonProfile = await signInManager.UserManager.FindByNameAsync(User.Identity.Name);
                        var Comment = await Service.Createcomment(model, PersonProfile);
                        if (ModelState.IsValid)
                        {
                            var result = mapper.Map<Comment>(Comment);
                            await comment.Create(result);
                            return RedirectToAction("GetAll");
                        }
                    }

                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }

                return View("GetComments", comment);
            }
            return RedirectToAction("AccessDenied", "Account");
        }
        #endregion



    }
}
